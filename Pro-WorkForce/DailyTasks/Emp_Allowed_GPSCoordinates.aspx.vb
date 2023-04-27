
Imports System.Data
Imports System.Globalization
Imports SmartV.UTILITIES
Imports TA.DailyTasks
Imports TA.Definitions
Imports TA.Security

Partial Class DailyTasks_Emp_Allowed_GPSCoordinates
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objEmp_logicalGroup As Emp_logicalGroup
    Private objEmp_Allowed_GPSCoordinates As Emp_Allowed_GPSCoordinates
    Private objEmp_LogicalGroup_Allowed_GPSCoordinates As Emp_LogicalGroup_Allowed_GPSCoordinates
    Private objVersion As SmartV.Version.version
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang

#End Region


#Region "Public Properties"

    Public Property AllowedGPSId() As Integer
        Get
            Return ViewState("AllowedGPSId")
        End Get
        Set(ByVal value As Integer)
            ViewState("AllowedGPSId") = value
        End Set
    End Property

    Public Property AllowedLGGPSId() As Integer
        Get
            Return ViewState("AllowedLGGPSId")
        End Get
        Set(ByVal value As Integer)
            ViewState("AllowedLGGPSId") = value
        End Set
    End Property

    Public Property dtCurrent() As DataTable
        Get
            Return ViewState("dtCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        Else
            If SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                Page.MasterPageFile = "~/default/ArabicMaster.master"
            Else
                Lang = CtlCommon.Lang.EN
                Page.MasterPageFile = "~/default/NewMaster.master"
            End If
            Page.UICulture = SessionVariables.CultureInfo
        End If
    End Sub

    Private Sub DailyTasks_Emp_Allowed_GPSCoordinates_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                dtpFromdate.Culture = New CultureInfo("ar-EG", False)
                dtpToDate.Culture = New CultureInfo("ar-EG", False)
            Else
                Lang = CtlCommon.Lang.EN
                dtpFromdate.Culture = New CultureInfo("en-US", False)
                dtpToDate.Culture = New CultureInfo("en-US", False)

            End If

            dtpFromdate.SelectedDate = Date.Today
            dtpToDate.SelectedDate = Date.Today

            dtpLGFromDate.SelectedDate = Date.Today
            dtpLGToDate.SelectedDate = Date.Today

            FillGrid()
            FillLGGrid()
            FillLogicalGroup()

            PageHeader1.HeaderText = ResourceManager.GetString("EmpAllowedGPS", CultureInfo)
        End If

        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdAllowedGPS.ClientID + "')")
        btnLGDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdAllowedLGGPS.ClientID + "')")

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not UpdatePanel1.FindControl(row("AddBtnName")) Is Nothing Then
                        UpdatePanel1.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not UpdatePanel1.FindControl(row("DeleteBtnName")) Is Nothing Then
                        UpdatePanel1.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not UpdatePanel1.FindControl(row("EditBtnName")) Is Nothing Then
                        UpdatePanel1.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not UpdatePanel1.FindControl(row("PrintBtnName")) Is Nothing Then
                        UpdatePanel1.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

    End Sub

    Protected Sub dgrdAllowedGPS_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdAllowedGPS.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub dgrdAllowedLGGPS_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdAllowedLGGPS.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter2.FireApplyCommand()
        End If
    End Sub

    Private Sub dgrdAllowedGPS_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdAllowedGPS.NeedDataSource
        objEmp_Allowed_GPSCoordinates = New Emp_Allowed_GPSCoordinates
        With objEmp_Allowed_GPSCoordinates
            dgrdAllowedGPS.DataSource = .GetAll_Inner
        End With
    End Sub

    Private Sub dgrdAllowedLGGPS_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdAllowedLGGPS.NeedDataSource
        objEmp_LogicalGroup_Allowed_GPSCoordinates = New Emp_LogicalGroup_Allowed_GPSCoordinates
        With objEmp_LogicalGroup_Allowed_GPSCoordinates
            dgrdAllowedLGGPS.DataSource = .GetAll_Inner
        End With
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearAll()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1

        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdAllowedGPS.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                objEmp_Allowed_GPSCoordinates = New Emp_Allowed_GPSCoordinates
                objEmp_Allowed_GPSCoordinates.AllowedGPSId = Convert.ToInt32(row.GetDataKeyValue("AllowedGPSId").ToString())
                errNum = objEmp_Allowed_GPSCoordinates.Delete()

            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillGrid()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord", CultureInfo), "error")
        End If

        ClearAll()

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim err As Integer = -1
        objEmp_Allowed_GPSCoordinates = New Emp_Allowed_GPSCoordinates
        With objEmp_Allowed_GPSCoordinates
            .FK_EmployeeId = EmployeeFilter1.EmployeeId
            .LocationName = txtEmpLocationName.Text
            .LocationArabicName = txtEmpLocationArabicName.Text
            .GPS_Coordinates = txtGPSCoordinates.Text.Trim.Replace(" ", "")
            .Radius = txtRadius.Text
            .FromDate = dtpFromdate.DbSelectedDate
            .IsTemporary = chckTemporary.Checked
            If chckTemporary.Checked = True Then
                .ToDate = dtpToDate.DbSelectedDate
            Else
                .ToDate = Nothing
            End If

            If AllowedGPSId = 0 Then
                .CREATED_BY = SessionVariables.LoginUser.ID
                err = .Add
            Else
                .AllowedGPSId = AllowedGPSId
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                err = .Update
            End If
        End With

        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            FillGrid()
            ClearAll()
        ElseIf err = -2 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("GPSCoordinateExists", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
    End Sub

    Private Sub btnLGClear_Click(sender As Object, e As EventArgs) Handles btnLGClear.Click
        ClearAll_LG()
    End Sub

    Private Sub btnLGDelete_Click(sender As Object, e As EventArgs) Handles btnLGDelete.Click
        Dim errNum As Integer = -1

        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdAllowedLGGPS.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                objEmp_LogicalGroup_Allowed_GPSCoordinates = New Emp_LogicalGroup_Allowed_GPSCoordinates
                objEmp_LogicalGroup_Allowed_GPSCoordinates.AllowedLGGPSId = Convert.ToInt32(row.GetDataKeyValue("AllowedLGGPSId").ToString())
                errNum = objEmp_LogicalGroup_Allowed_GPSCoordinates.Delete()

            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillLGGrid()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord", CultureInfo), "error")
        End If

        ClearAll()
    End Sub

    Private Sub btnLGSave_Click(sender As Object, e As EventArgs) Handles btnLGSave.Click
        Dim err As Integer = -1
        objEmp_LogicalGroup_Allowed_GPSCoordinates = New Emp_LogicalGroup_Allowed_GPSCoordinates
        With objEmp_LogicalGroup_Allowed_GPSCoordinates
            .FK_LogicalGroupId = ddlLogicalGroup.SelectedValue
            .LocationName = txtLGLocationName.Text
            .LocationArabicName = txtLGLocationArabicName.Text
            .GPS_Coordinates = txtLGGPSCoordinates.Text.Trim.Replace(" ", "")
            .Radius = txtLGRadius.Text
            .FromDate = dtpLGFromDate.DbSelectedDate
            .IsTemporary = chkLGIsTemporary.Checked
            If chkLGIsTemporary.Checked = True Then
                .ToDate = dtpLGToDate.DbSelectedDate
            Else
                .ToDate = Nothing
            End If

            If AllowedGPSId = 0 Then
                .CREATED_BY = SessionVariables.LoginUser.ID
                err = .Add
            Else
                .AllowedLGGPSId = AllowedLGGPSId
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                err = .Update
            End If
        End With

        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            FillLGGrid()
            ClearAll_LG()
        ElseIf err = -2 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("GPSCoordinateExists", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Sub RadToolBar2_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Private Sub dgrdAllowedGPS_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles dgrdAllowedGPS.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
            Dim item As GridDataItem
            item = e.Item
            If Lang = CtlCommon.Lang.AR Then
                item("EmployeeName").Text = DirectCast(item.FindControl("hdnEmployeeNameAr"), HiddenField).Value
            End If

            Dim hdnIsTemporary As HiddenField = CType(e.Item.FindControl("hdnIsTemporary"), HiddenField)
            Dim chkIsTemporary As CheckBox = CType(e.Item.FindControl("chkIsTemporary"), CheckBox)

            If hdnIsTemporary.Value = "True" Then
                chkIsTemporary.Checked = True
            End If

        End If
    End Sub

    Private Sub dgrdAllowedLGGPS_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles dgrdAllowedLGGPS.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
            Dim item As GridDataItem
            item = e.Item
            If Lang = CtlCommon.Lang.AR Then
                item("GroupName").Text = DirectCast(item.FindControl("hdnGroupNameAr"), HiddenField).Value
            End If

            Dim hdnLGIsTemporary As HiddenField = CType(e.Item.FindControl("hdnLGIsTemporary"), HiddenField)
            Dim chkLGIsTemporary As CheckBox = CType(e.Item.FindControl("chkLGIsTemporary"), CheckBox)

            If hdnLGIsTemporary.Value = "True" Then
                chkLGIsTemporary.Checked = True
            End If

        End If
    End Sub

    Private Sub dgrdAllowedGPS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgrdAllowedGPS.SelectedIndexChanged
        AllowedGPSId = Convert.ToInt32(DirectCast(dgrdAllowedGPS.SelectedItems(0), GridDataItem).GetDataKeyValue("AllowedGPSId").ToString())
        FillControls()
    End Sub

    Private Sub dgrdAllowedLGGPS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgrdAllowedLGGPS.SelectedIndexChanged
        AllowedLGGPSId = Convert.ToInt32(DirectCast(dgrdAllowedLGGPS.SelectedItems(0), GridDataItem).GetDataKeyValue("AllowedLGGPSId").ToString())
        FillLGControls()
    End Sub

    Private Sub chckTemporary_CheckedChanged(sender As Object, e As EventArgs) Handles chckTemporary.CheckedChanged
        If chckTemporary.Checked = True Then
            pnlToDate.Visible = True
        Else
            pnlToDate.Visible = False
            dtpToDate.SelectedDate = Date.Today
        End If


    End Sub

    Private Sub chkLGIsTemporary_CheckedChanged(sender As Object, e As EventArgs) Handles chkLGIsTemporary.CheckedChanged
        If chkLGIsTemporary.Checked = True Then
            pnlLGToDate.Visible = True
        Else
            pnlLGToDate.Visible = False
            dtpLGToDate.SelectedDate = Date.Today
        End If

    End Sub

#End Region

#Region "Methods"

    Private Sub FillLogicalGroup()
        objEmp_logicalGroup = New Emp_logicalGroup
        With objEmp_logicalGroup
            CtlCommon.FillTelerikDropDownList(ddlLogicalGroup, .GetAll, Lang)
        End With
    End Sub

    Private Sub FillGrid()
        objEmp_Allowed_GPSCoordinates = New Emp_Allowed_GPSCoordinates
        With objEmp_Allowed_GPSCoordinates
            dgrdAllowedGPS.DataSource = .GetAll_Inner
            dgrdAllowedGPS.DataBind()
        End With

    End Sub

    Private Sub FillLGGrid()
        objEmp_LogicalGroup_Allowed_GPSCoordinates = New Emp_LogicalGroup_Allowed_GPSCoordinates
        With objEmp_LogicalGroup_Allowed_GPSCoordinates
            dgrdAllowedLGGPS.DataSource = .GetAll_Inner
            dgrdAllowedLGGPS.DataBind()
        End With
    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdAllowedGPS.Skin))
    End Function

    Protected Function GetFilterIcon1() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdAllowedLGGPS.Skin))
    End Function

    Private Sub ClearAll()
        EmployeeFilter1.ClearValues()
        txtEmpLocationName.Text = String.Empty
        txtEmpLocationArabicName.Text = String.Empty
        txtGPSCoordinates.Text = String.Empty
        txtRadius.Text = String.Empty
        dtpFromdate.SelectedDate = Date.Today
        dtpToDate.SelectedDate = Date.Today
        chckTemporary.Checked = False
        pnlToDate.Visible = False
        AllowedGPSId = 0
        dgrdAllowedGPS.Rebind()
    End Sub

    Private Sub ClearAll_LG()
        ddlLogicalGroup.SelectedValue = -1
        txtLGLocationName.Text = String.Empty
        txtLGLocationArabicName.Text = String.Empty
        txtLGGPSCoordinates.Text = String.Empty
        txtLGRadius.Text = String.Empty
        dtpLGFromDate.SelectedDate = Date.Today
        dtpLGToDate.SelectedDate = Date.Today
        chkLGIsTemporary.Checked = False
        pnlLGToDate.Visible = False
        AllowedLGGPSId = 0
        dgrdAllowedLGGPS.Rebind()
    End Sub

    Private Sub FillControls()
        objEmp_Allowed_GPSCoordinates = New Emp_Allowed_GPSCoordinates
        With objEmp_Allowed_GPSCoordinates
            .AllowedGPSId = AllowedGPSId
            .GetByPK()
            EmployeeFilter1.EmployeeId = .FK_EmployeeId
            txtEmpLocationName.Text = .LocationName
            txtEmpLocationArabicName.Text = .LocationArabicName
            EmployeeFilter1.GetEmployeeInfo(.FK_EmployeeId)
            txtGPSCoordinates.Text = .GPS_Coordinates
            txtRadius.Text = .Radius
            dtpFromdate.SelectedDate = .FromDate
            chckTemporary.Checked = .IsTemporary
            If .IsTemporary = True Then
                pnlToDate.Visible = True
                dtpToDate.SelectedDate = .ToDate
            Else
                pnlToDate.Visible = False
                dtpToDate.SelectedDate = Date.Today
            End If

        End With
    End Sub

    Private Sub FillLGControls()
        objEmp_LogicalGroup_Allowed_GPSCoordinates = New Emp_LogicalGroup_Allowed_GPSCoordinates
        With objEmp_LogicalGroup_Allowed_GPSCoordinates
            .AllowedLGGPSId = AllowedLGGPSId
            .GetByPK()
            ddlLogicalGroup.SelectedValue = .FK_LogicalGroupId
            txtLGLocationName.Text = .LocationName
            txtLGLocationArabicName.Text = .LocationArabicName
            txtLGGPSCoordinates.Text = .GPS_Coordinates
            txtLGRadius.Text = .Radius
            dtpLGFromDate.SelectedDate = .FromDate

            chkLGIsTemporary.Checked = .IsTemporary
            If .IsTemporary = True Then
                pnlLGToDate.Visible = True
                dtpLGToDate.SelectedDate = .ToDate
            Else
                pnlLGToDate.Visible = False
                dtpLGToDate.SelectedDate = Date.Today
            End If

        End With
    End Sub


#End Region


End Class
