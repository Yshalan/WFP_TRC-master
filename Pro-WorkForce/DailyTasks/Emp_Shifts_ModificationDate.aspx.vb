
Imports System.Data
Imports System.Globalization
Imports SmartV.UTILITIES
Imports TA.DailyTasks
Imports TA.Security

Partial Class DailyTasks_Emp_Shifts_ModificationDate
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objEmp_Shifts_ModificationDate As Emp_Shifts_ModificationDate
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang

#End Region

#Region "Public Properties"

    Public Property ModificationId() As String
        Get
            Return ViewState("ModificationId")
        End Get
        Set(ByVal value As String)
            ViewState("ModificationId") = value
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

    Private Sub DailyTasks_Emp_Shifts_ModificationDate_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                dtpFromDate.Culture = New CultureInfo("ar-EG", False)
                dtpToDate.Culture = New CultureInfo("ar-EG", False)
            Else
                Lang = CtlCommon.Lang.EN
                dtpFromDate.Culture = New CultureInfo("en-US", False)
                dtpToDate.Culture = New CultureInfo("en-US", False)
            End If

            dtpFromDate.DbSelectedDate = Date.Today
            dtpToDate.DbSelectedDate = Date.Today

            PageHeader1.HeaderText = ResourceManager.GetString("ModificationDate", CultureInfo)

        End If

        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdModificationDate.ClientID + "');")

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not Update1.FindControl(row("AddBtnName")) Is Nothing Then
                        Update1.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not Update1.FindControl(row("DeleteBtnName")) Is Nothing Then
                        Update1.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not Update1.FindControl(row("EditBtnName")) Is Nothing Then
                        Update1.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not Update1.FindControl(row("PrintBtnName")) Is Nothing Then
                        Update1.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next


    End Sub

    Protected Sub dgrdModificationDate_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdModificationDate.Skin))
    End Function

    Private Sub rblModificationType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblModificationType.SelectedIndexChanged
        If rblModificationType.SelectedValue = 1 Then

            dvDays.Visible = True
            rfvFromDay.Enabled = True
            rfvToDay.Enabled = True

            dvSpecificDate.Visible = False
            rfvFromDate.Enabled = False
            rfvToDate.Enabled = False
            cvDate.Enabled = False
        Else
            dvDays.Visible = False
            rfvFromDay.Enabled = False
            rfvToDay.Enabled = False

            dvSpecificDate.Visible = True
            rfvFromDate.Enabled = True
            rfvToDate.Enabled = True
            cvDate.Enabled = True
        End If
    End Sub

    Private Sub dgrdModificationDate_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdModificationDate.NeedDataSource
        objEmp_Shifts_ModificationDate = New Emp_Shifts_ModificationDate
        With objEmp_Shifts_ModificationDate
            dgrdModificationDate.DataSource = .GetAll
        End With
    End Sub

    Private Sub dgrdModificationDate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgrdModificationDate.SelectedIndexChanged
        ModificationId = Convert.ToInt32(DirectCast(dgrdModificationDate.SelectedItems(0), GridDataItem).GetDataKeyValue("ModificationId").ToString())
        FillControls()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearAll()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1

        For Each row As GridDataItem In dgrdModificationDate.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                objEmp_Shifts_ModificationDate = New Emp_Shifts_ModificationDate
                objEmp_Shifts_ModificationDate.ModificationId = Convert.ToInt32(row.GetDataKeyValue("ModificationId").ToString())
                errNum = objEmp_Shifts_ModificationDate.Delete()

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
        objEmp_Shifts_ModificationDate = New Emp_Shifts_ModificationDate
        With objEmp_Shifts_ModificationDate
            .DateOption = rblModificationType.SelectedValue
            If rblModificationType.SelectedValue = 1 Then
                .FromDay = cmbxFromDay.SelectedValue
                .ToDay = cmbxToDay.SelectedValue
            Else
                .FromDate = dtpFromDate.DbSelectedDate
                .ToDate = dtpToDate.DbSelectedDate
            End If

            If ModificationId = 0 Then
                .CREATED_BY = SessionVariables.LoginUser.ID
                err = .Add
            Else
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                .ModificationId = ModificationId
                err = .Update
            End If
        End With

        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            FillGrid()
            ClearAll()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If

    End Sub

    Private Sub dgrdModificationDate_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles dgrdModificationDate.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim item As GridDataItem
            item = e.Item


            If Lang = CtlCommon.Lang.AR Then
                If Not item("DateOption").Text = "nbsp;" Then
                    If item("DateOption").Text = "1" Then
                        item("DateOption").Text = "تكرار شهري"
                    Else
                        item("DateOption").Text = "مدة محددة"
                    End If

                End If
            Else
                If Not item("DateOption").Text = "nbsp;" Then
                    If item("DateOption").Text = "1" Then
                        item("DateOption").Text = "Repeated Monthly"
                    Else
                        item("DateOption").Text = "Specific Period"
                    End If

                End If
            End If

        End If
    End Sub

#End Region

#Region "Methods"

    Private Sub FillGrid()
        objEmp_Shifts_ModificationDate = New Emp_Shifts_ModificationDate
        With objEmp_Shifts_ModificationDate
            dgrdModificationDate.DataSource = .GetAll
            dgrdModificationDate.DataBind()
        End With
    End Sub

    Private Sub ClearAll()
        rblModificationType.SelectedValue = 1
        dvDays.Visible = True
        dvSpecificDate.Visible = False
        cmbxFromDay.SelectedValue = -1
        cmbxToDay.SelectedValue = -1

        dtpFromDate.DbSelectedDate = Date.Today
        dtpToDate.DbSelectedDate = Date.Today
        ModificationId = 0
    End Sub

    Private Sub FillControls()
        objEmp_Shifts_ModificationDate = New Emp_Shifts_ModificationDate
        With objEmp_Shifts_ModificationDate
            .ModificationId = ModificationId
            .GetByPK()
            rblModificationType.SelectedValue = .DateOption
            If .DateOption = 1 Then
                dvDays.Visible = True
                rfvFromDay.Enabled = True
                rfvToDay.Enabled = True

                cmbxFromDay.SelectedValue = .FromDay
                cmbxToDay.SelectedValue = .ToDay

                dvSpecificDate.Visible = False
                rfvFromDate.Enabled = False
                rfvToDate.Enabled = False
                cvDate.Enabled = False
            Else
                dvDays.Visible = False
                rfvFromDay.Enabled = False
                rfvToDay.Enabled = False

                dvSpecificDate.Visible = True
                rfvFromDate.Enabled = True
                rfvToDate.Enabled = True
                cvDate.Enabled = True

                dtpFromDate.DbSelectedDate = .FromDate
                dtpToDate.DbSelectedDate = .ToDate

            End If
        End With
    End Sub

#End Region






End Class
