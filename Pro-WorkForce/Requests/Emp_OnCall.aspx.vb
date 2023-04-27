Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports TA.Definitions
Imports TA.Security
Imports System.Data
Imports TA_Emp_OnCall
Imports SmartV.UTILITIES.ProjectCommon
Imports TA.Admin

Partial Class Requests_Emp_OnCall
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Shared SortExepression As String
    Dim objEmp_OnCall As Emp_OnCall
    Dim objEmpDesignation As Emp_Designation
#End Region
#Region "Properties"
    Public Property dtCurrent() As DataTable
        Get
            Return ViewState("dtCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
        End Set
    End Property
    Private Property EmployeeId() As Integer
        Get
            Return ViewState("EmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeId") = value
        End Set
    End Property
    Private Property OnCallId() As Integer
        Get
            Return ViewState("OnCallId")
        End Get
        Set(ByVal value As Integer)
            ViewState("OnCallId") = value
        End Set
    End Property

    Private Property IsFilter() As Integer
        Get
            Return ViewState("IsFilter")
        End Get
        Set(ByVal value As Integer)
            ViewState("IsFilter") = value
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

        If Not Page.IsPostBack Then
            'EmployeeFilterUC.IsManager = 1
            'EmployeeFilterUC.FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
            FillGridView()
            FillEntityByManagerId()
            FillDesignationByManagerId()
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            mvEmpLastBalance.SetActiveView(viewEmpLastBalance)
            dtpDutyDate.SelectedDate = Date.Today
            rmtFromTime.Text = "0730"
            rmtToTime.Text = "0730"
            RadDatePicker1.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd As New Date
            dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)

            RadDatePicker2.SelectedDate = dd
            PageHeader1.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "الموظفين تحت الطلب", "Employees On Call")
            'EmployeeFilterUC.CompanyRequiredFieldValidationGroup = btnGet.ValidationGroup
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If


            EmployeeFilterUC.IsEmployeeRequired = True
        End If

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
    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1

        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdEmployeesEmpOnCall.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim intOnCallId As Integer = Convert.ToInt32(row.GetDataKeyValue("OnCallId"))
                objEmp_OnCall = New Emp_OnCall()
                objEmp_OnCall.OnCallId = intOnCallId
                errNum = objEmp_OnCall.Delete()
                With strBuilder

                End With
            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully"), "success")
            FillGridView()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord", CultureInfo), "error")
            'showResult(CodeResultMessage.CodeDeleteFail)

        End If

        ClearAll()
    End Sub
    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdEmployeesEmpOnCall.Skin))
    End Function
    Protected Sub dgrdEmployeesEmpOnCall_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdEmployeesEmpOnCall.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub dgrdEmployeesEmpOnCall_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdEmployeesEmpOnCall.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim item As GridDataItem
            item = e.Item

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("DutyDate").ToString())) And (Not item.GetDataKeyValue("DutyDate").ToString() = "")) Then
                Dim DutyDate As DateTime = item.GetDataKeyValue("DutyDate")
                item("DutyDate").Text = DutyDate.ToShortDateString()
            End If


        End If
    End Sub
    Protected Sub grdEmpLeaves_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdEmployeesEmpOnCall.SelectedIndexChanged
        OnCallId = Convert.ToInt32(DirectCast(dgrdEmployeesEmpOnCall.SelectedItems(0), GridDataItem).GetDataKeyValue("OnCallId"))
        EmployeeId = Convert.ToInt32(DirectCast(dgrdEmployeesEmpOnCall.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_EmployeeId"))
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        objEmp_OnCall = New Emp_OnCall
        With objEmp_OnCall
            .OnCallId = OnCallId

            .GetByPK()
            'chckFromHome.Checked = .FromHome
            'dtpDutyDate.SelectedDate = .DutyDate
            'rmtFromTime.Text = CtlCommon.GetFullTimeString(.FromTime)
            'rmtToTime.Text = CtlCommon.GetFullTimeString(.ToTime)

            'btnSave.Text = ResourceManager.GetString("btnUpdate", CultureInfo)

        End With
    End Sub
    Protected Sub dgrdEmployeesEmpOnCall_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdEmployeesEmpOnCall.NeedDataSource
        'grdEmpLeaves.DataSource = EmpDt

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
        Dim err As Integer = 0
        objEmp_OnCall = New Emp_OnCall
        With objEmp_OnCall
            .FK_EmployeeId = EmployeeFilterUC.EmployeeId
            .DutyDate = dtpDutyDate.SelectedDate
            .FromHome = chckFromHome.Checked
            Dim FromTime As String = (CInt(rmtFromTime.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtFromTime.TextWithLiterals.Split(":")(1))
            .FromTime = FromTime
            Dim ToTime As String = (CInt(rmtToTime.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtToTime.TextWithLiterals.Split(":")(1))
            .ToTime = ToTime

            If EmployeeId = 0 Then
                .CREATED_BY = SessionVariables.LoginUser.ID
                .CREATED_DATE = Date.Now
                err = .Add()
                If err = 0 Then
                    FillGridView()
                    ClearAll()
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully"), "success")
                Else
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave"), "error")
                End If
            Else
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                .LAST_UPDATE_DATE = Date.Now
                err = .Update()
                If err = 0 Then
                    FillGridView()
                    ClearAll()
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("UpdatedSuccessfully"), "success")
                Else
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate"), "error")
                End If
            End If

        End With

    End Sub

    Protected Sub btnClear_Click(sender As Object, e As System.EventArgs) Handles btnClear.Click
        ClearAll()
    End Sub
#End Region
#Region "Methods"
    Private Sub ClearAll()
        ' Clear the controls

        EmployeeFilterUC.ClearValues()
        RadCmbBxEntity.SelectedValue = -1
        RadCmbDesignation.SelectedValue = -1
        rmtFromTime.Text = "0730"
        rmtToTime.Text = "0730"

        btnSave.Text = ResourceManager.GetString("btnSave", CultureInfo)
    End Sub
    Private Sub FillGridView()
        Try
            Dim dt As New DataTable
            objEmp_OnCall = New Emp_OnCall
            With objEmp_OnCall
                If IsFilter = 1 Then
                    .FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
                    .Fk_EntityId = RadCmbBxEntity.SelectedValue
                    .FK_DesginationId = RadCmbDesignation.SelectedValue
                    .FromDate = DateToString(RadDatePicker1.SelectedDate)
                    .ToDate = DateToString(RadDatePicker2.SelectedDate)
                    dt = .GetAllbyFilter()
                Else
                    dt = .GetAll()
                End If

                dgrdEmployeesEmpOnCall.DataSource = dt
                dgrdEmployeesEmpOnCall.DataBind()
            End With
            
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FillEntityByManagerId()

        Dim objProjectCommon = New ProjectCommon()

        Dim objOrgEntity = New OrgEntity()
        objOrgEntity.FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId

        Dim dt As DataTable = objOrgEntity.GetAllEntityByManger()

        objProjectCommon.FillMultiLevelRadComboBox(RadCmbBxEntity, dt, "EntityId", _
                                                 "EntityName", "EntityArabicName", "FK_ParentId")
        RadCmbBxEntity.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem(ResourceManager.GetString("PleaseSelect", CultureInfo), -1))



    End Sub
    Private Sub FillDesignationByManagerId()
        Dim dt As DataTable
        dt = Nothing
        objEmpDesignation = New Emp_Designation()
        objEmpDesignation.FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
        dt = objEmpDesignation.GetAllDesignationByManger()
        ProjectCommon.FillRadComboBox(RadCmbDesignation, dt, _
                                      "DesignationName", "DesignationArabicName")
    End Sub
    Private Function DateToString(ByVal TempDate As Date) As String
        Dim tempDay As String
        Dim tempMonth As String

        If TempDate.Month.ToString.Length = 1 Then
            tempMonth = "0" + TempDate.Month.ToString
        Else
            tempMonth = TempDate.Month.ToString
        End If
        If TempDate.Day.ToString.Length = 1 Then
            tempDay = "0" + TempDate.Day.ToString
        Else
            tempDay = TempDate.Day.ToString
        End If
        Return tempDay + "/" + tempMonth + "/" + TempDate.Year.ToString()
    End Function
#End Region

    Protected Sub btnGetByFilter_Click(sender As Object, e As System.EventArgs) Handles btnGetByFilter.Click
        IsFilter = 1
        FillGridView()

    End Sub
End Class
