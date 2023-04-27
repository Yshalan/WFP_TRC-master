Imports TA.Employees
Imports TA.Definitions
Imports SmartV.UTILITIES
Imports System.Data
Imports Telerik.Web.UI.DatePickerPopupDirection
Imports Telerik.Web.UI
Imports TA.Lookup
Imports TA.Admin
Imports System.Web.UI
Imports SmartV.UTILITIES.ProjectCommon
Imports System.IO
Imports TA.Security

Partial Class Employee_Emp_DeputyManager
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objEmployee As New Employee
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Dim objEmpDeputyManager As New Emp_DeputyManager
    Shared SortExepression As String
    Shared dtCurrent As DataTable

#End Region

#Region "Properties"

    Public Property DeuputyManagerId() As Integer
        Get
            Return ViewState("DeuputyManagerId")
        End Get
        Set(ByVal value As Integer)
            ViewState("DeuputyManagerId") = value
        End Set
    End Property

    Public Property FK_EmployeeId() As Integer
        Get
            Return ViewState("FK_EmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("FK_EmployeeId") = value
        End Set
    End Property

    Public Property dtCurrentControls() As DataTable
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If chckTemporary.Checked Then
            trEndDate.Visible = True
        Else
            trEndDate.Visible = False
        End If

        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            PageHeader1.HeaderText = ResourceManager.GetString("EmpDeputyManager", CultureInfo)
            EmployeeFilterUC.IsEmployeeRequired = True
            EmployeeFilterUC.CompanyRequiredValidationGroup = btnSave.ValidationGroup
            EmployeeFilterUC.EmployeeRequiredValidationGroup = btnSave.ValidationGroup
            dtpFromdate.SelectedDate = Date.Today
            dtpEndDate.SelectedDate = Date.Today
            FillGrid()
        End If

        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + grdEmpDeputyManager.ClientID + "');")

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrentControls = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrentControls.Rows
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

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click

        If chckTemporary.Checked Then
            If dtpEndDate.SelectedDate IsNot Nothing Then
                If Not StartEndDateComparison(dtpFromdate.SelectedDate, _
                                                dtpEndDate.SelectedDate) Then
                    Return
                End If
            End If
        End If

        Dim errornum As Integer
        objEmpDeputyManager = New Emp_DeputyManager()
        With objEmpDeputyManager
            .FK_CompanyId = EmployeeFilterUC.CompanyId
            .FK_DeputyManagerId = FK_EmployeeId
            .FK_EntityId = EmployeeFilterUC.EntityId
            .FK_ManagerId = EmployeeFilterUC.EmployeeId
            .FromDate = dtpFromdate.SelectedDate
            If chckTemporary.Checked Then
                .ToDate = dtpEndDate.SelectedDate
            End If

            If DeuputyManagerId = 0 Then
                errornum = .Add()
                DeuputyManagerId = .DeuputyManagerID
                If errornum = 0 Then
                    CtlCommon.ShowMessage(Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
                    ClearAll()
                    FillGrid()
                Else
                    CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
                End If
            Else
                .DeuputyManagerID = DeuputyManagerId
                errornum = .Update()

                If errornum = 0 Then
                    CtlCommon.ShowMessage(Page, ResourceManager.GetString("UpdatedSuccessfully", CultureInfo), "success")
                    ClearAll()
                    FillGrid()
                Else
                    CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")
                End If
            End If
        End With

    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        Dim counter As Integer = 0
        Dim errNum As Integer
        For Each Item As GridDataItem In grdEmpDeputyManager.Items
            Dim cb As CheckBox = DirectCast(Item.FindControl("chk"), CheckBox)
            If cb.Checked Then
                counter = counter + 1
                Dim Id As Integer = Item.GetDataKeyValue("Id")
                objEmpDeputyManager = New Emp_DeputyManager()
                With objEmpDeputyManager
                    .DeuputyManagerID = Id
                    errNum += .Delete()
                End With
            End If
        Next
        If counter = 0 Then
            CtlCommon.ShowMessage(Me.Page, IIf(Lang = CtlCommon.Lang.AR, "الرجاء اختيار الموظف المراد حذفه", " Please Select Employee to delete"), "info")
            Return
        End If
        If errNum = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            FillGrid()
        Else
           CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
        ClearAll()

    End Sub

    Protected Sub grdEmpDeputyManager_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles grdEmpDeputyManager.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim item As GridDataItem
            item = e.Item

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("FromDate").ToString())) And (Not item.GetDataKeyValue("FromDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("FromDate")
                item("FromDate").Text = fromDate.ToShortDateString()
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("ToDate").ToString())) And (Not item.GetDataKeyValue("ToDate").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("ToDate")
                item("ToDate").Text = fromDate.ToShortDateString()
            End If

            If Lang = CtlCommon.Lang.AR Then
                item("CompanyName").Text = item.GetDataKeyValue("CompanyArabicName").ToString()
                item("EntityName").Text = item.GetDataKeyValue("EntityArabicName").ToString()
                item("ManagerName").Text = item.GetDataKeyValue("ManagerArabicName").ToString()
                item("DeputyName").Text = item.GetDataKeyValue("DeputyArabicName").ToString()
            End If

        End If
    End Sub

    Protected Sub grdEmpDeputyManager_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdEmpDeputyManager.SelectedIndexChanged
        DeuputyManagerId = Convert.ToInt32(DirectCast(grdEmpDeputyManager.SelectedItems(0), GridDataItem).GetDataKeyValue("Id"))
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        objEmpDeputyManager = New Emp_DeputyManager()
        With objEmpDeputyManager
            .DeuputyManagerID = DeuputyManagerId
            .GetByPK()
            dtpFromdate.SelectedDate = .FromDate
            If Not .ToDate = DateTime.MinValue Then
                chckTemporary.Checked = True
                trEndDate.Visible = True
                dtpEndDate.SelectedDate = .ToDate
            End If

            FK_EmployeeId = .FK_DeputyManagerId
            objEmployee = New Employee()
            With objEmployee
                .EmployeeId = FK_EmployeeId
                .GetByPK()
                txtEmpNo.Text = .EmployeeNo
                If Lang = CtlCommon.Lang.AR Then
                    txtEmpName.Text = .EmployeeArabicName
                Else
                    txtEmpName.Text = .EmployeeName
                End If
            End With
            Dim FK_ManagerId As Integer = Convert.ToInt32(DirectCast(grdEmpDeputyManager.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_ManagerId"))
            FK_ManagerId = objEmpDeputyManager.FK_ManagerId
            EmployeeFilterUC.GetEmployeeInfo(FK_ManagerId)
            btnSave.Text = ResourceManager.GetString("btnUpdate", CultureInfo)
        End With
    End Sub

    Protected Sub btnRetrieve_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRetrieve.Click

        txtEmpName.Text = String.Empty
        objEmployee = New Employee()
        'objEmpDeputyManager = New Emp_DeputyManager()
        Dim dtEmployee As DataTable
        With objEmployee
            .FK_CompanyId = EmployeeFilterUC.CompanyId
            '.FK_EntityId = EmployeeFilterUC.EntityId
            'dtEmployee = .GetEmpByCompany
            .EmployeeNo = txtEmpNo.Text
            dtEmployee = .GetEmpByEmpNo

            For Each row As DataRow In dtEmployee.Rows
                If (row("EmployeeNo") = txtEmpNo.Text) Then
                    If Lang = CtlCommon.Lang.AR Then
                        txtEmpName.Text = row("EmployeeArabicName")
                    Else
                        txtEmpName.Text = row("EmployeeName")
                    End If

                    FK_EmployeeId = row("EmployeeId")
                    Exit For
                End If
            Next
        End With

        If String.IsNullOrEmpty(txtEmpName.Text) Then

            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EmpNonotinlevel", CultureInfo), "info")
        End If

    End Sub

    Protected Sub grdEmpDeputyManager_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", grdEmpDeputyManager.Skin))
    End Function

    Protected Sub grdEmpDeputyManager_NeedDataSource(ByVal sender As Object, ByVal e As GridNeedDataSourceEventArgs) Handles grdEmpDeputyManager.NeedDataSource
        objEmpDeputyManager = New Emp_DeputyManager()
        objEmpDeputyManager.FK_ManagerId = EmployeeFilterUC.EmployeeId
        dtCurrent = objEmpDeputyManager.GetAll()
        Dim dv As New DataView(dtCurrent)
        dv.Sort = SortExepression
        grdEmpDeputyManager.DataSource = dv
    End Sub

#End Region

#Region "Methods"

    Private Sub ClearAll()
        dtpFromdate.SelectedDate = Date.Today
        dtpEndDate.SelectedDate = Date.Today
        trEndDate.Visible = False
        chckTemporary.Checked = False
        EmployeeFilterUC.ClearValues()
        txtEmpName.Text = String.Empty
        txtEmpNo.Text = String.Empty
        Dim dtEmpty As New DataTable
        grdEmpDeputyManager.DataSource = dtEmpty
        grdEmpDeputyManager.DataBind()
        DeuputyManagerId = 0
    End Sub

    Public Sub FillGrid()
        objEmpDeputyManager = New Emp_DeputyManager()
        Dim dt As DataTable
        objEmpDeputyManager.FK_ManagerId = EmployeeFilterUC.EmployeeId
        dt = objEmpDeputyManager.GetAll()
        grdEmpDeputyManager.DataSource = dt
        grdEmpDeputyManager.DataBind()
    End Sub

    Private Function StartEndDateComparison(ByVal dateStartdate As DateTime, _
                                           ByVal dateEndDate As DateTime) As Boolean
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        If IsDate(dtpFromdate.SelectedDate) And IsDate(dtpEndDate.SelectedDate) Then
            ' Get the start and end date from the calenders
            dateStartdate = New DateTime(dtpFromdate.SelectedDate.Value.Year, _
                                         dtpFromdate.SelectedDate.Value.Month, _
                                         dtpFromdate.SelectedDate.Value.Day)
            dateEndDate = New DateTime(dtpEndDate.SelectedDate.Value.Year, _
                                       dtpEndDate.SelectedDate.Value.Month, _
                                       dtpEndDate.SelectedDate.Value.Day)
            Dim result As Integer = DateTime.Compare(dateEndDate, dateStartdate)
            If result < 0 Then
                ' show message and set focus on end date picker
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DateRangeEarly", CultureInfo), "info")

                dtpEndDate.Focus()
                Return False
            ElseIf result = 0 Then
                ' Show message and focus on the end date picker
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EndEqualStart", CultureInfo), "info")


                dtpEndDate.Focus()
                Return False
            Else
                ' Do nothing
                Return True
            End If
        End If
    End Function



#End Region

End Class
