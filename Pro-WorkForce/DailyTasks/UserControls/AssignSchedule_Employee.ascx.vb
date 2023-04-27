Imports TA.Admin.OrgCompany
Imports SmartV.UTILITIES
Imports TA.Lookup
Imports System.Data
Imports TA.Employees
Imports TA.Definitions
Imports TA.Security
Imports Telerik.Web.UI
Imports TA.DailyTasks
Imports SmartV.UTILITIES.ProjectCommon
Imports TA.Admin

Partial Class DailyTasks_UserControls_AssignSchedule_Employee
    Inherits System.Web.UI.UserControl

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private objEmp_WorkSchedule As Emp_WorkSchedule
    Private objRECALC_REQUEST As RECALC_REQUEST
    Private objEmployee As Employee
    Private objAPP_Settings As APP_Settings
    Private objRecalculateRequest As RecalculateRequest
    Private objVersion As SmartV.Version.version

#End Region

#Region "Properties"
    Public Property EmpWorkScheduleId() As Integer
        Get
            Return ViewState("EmpWorkScheduleId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmpWorkScheduleId") = value
        End Set
    End Property
    Public Property CompanyId() As Integer
        Get
            Return ViewState("CompanyId")
        End Get
        Set(ByVal value As Integer)
            ViewState("CompanyId") = value
        End Set
    End Property
    Public Property EntityId() As Integer
        Get
            Return ViewState("EntityId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EntityId") = value
        End Set
    End Property
    Public Property ScheduleId() As Integer
        Get
            Return ViewState("ScheduleId")
        End Get
        Set(ByVal value As Integer)
            ViewState("ScheduleId") = value
        End Set
    End Property
    Public Property EmployeeId() As Integer
        Get
            Return ViewState("EmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeId") = value
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
    Public Property IsAdvanced() As Integer
        Get
            Return ViewState("IsAdvanced")
        End Get
        Set(ByVal value As Integer)
            ViewState("IsAdvanced") = value
        End Set
    End Property
    Public Property IsManager() As Integer
        Get
            Return ViewState("IsManager")
        End Get
        Set(ByVal value As Integer)
            ViewState("IsManager") = value
        End Set
    End Property
    Public Property FK_ManagerId() As Integer
        Get
            Return ViewState("FK_ManagerId")
        End Get
        Set(ByVal value As Integer)
            ViewState("FK_ManagerId") = value
        End Set
    End Property
    Public Property PageNo() As Integer
        Get
            Return ViewState("PageNo")
        End Get
        Set(ByVal value As Integer)
            ViewState("PageNo") = value
        End Set
    End Property
    Public Property Emp_RecordCount() As Integer
        Get
            Return ViewState("Emp_RecordCount")
        End Get
        Set(ByVal value As Integer)
            ViewState("Emp_RecordCount") = value
        End Set
    End Property
    Public Property FromDate() As DateTime
        Get
            Return ViewState("FromDate")
        End Get
        Set(ByVal value As DateTime)
            ViewState("FromDate") = value
        End Set
    End Property
    Public Property ToDate() As DateTime
        Get
            Return ViewState("ToDate")
        End Get
        Set(ByVal value As DateTime)
            ViewState("ToDate") = value
        End Set
    End Property
    Public Property dtGridCurrent() As DataTable
        Get
            Return ViewState("dtGridCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtGridCurrent") = value
        End Set
    End Property
#End Region

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        showHide(chckTemporary.Checked)
        If Not Page.IsPostBack Then
            If IsAdvanced = 1 Then
                FillSchedule()
            End If
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                dgrdSchedule_Employee.Columns(7).Visible = False
                Dim item1 As New RadComboBoxItem
                Dim item2 As New RadComboBoxItem
                Dim item3 As New RadComboBoxItem
                Dim item4 As New RadComboBoxItem
                Dim item5 As New RadComboBoxItem
                item1.Value = -1
                item1.Text = "--الرجاء الاختيار--"
                RadCmbBxScheduletype.Items.Add(item1)
                item2.Value = 1
                item2.Text = "عادي"
                RadCmbBxScheduletype.Items.Add(item2)
                item3.Value = 2
                item3.Text = "مرن"
                RadCmbBxScheduletype.Items.Add(item3)
                item4.Value = 3
                item4.Text = "مناوبات"
                RadCmbBxScheduletype.Items.Add(item4)
                item5.Value = 5
                item5.Text = "مفتوح"
                RadCmbBxScheduletype.Items.Add(item5)
            Else
                Lang = CtlCommon.Lang.EN
                dgrdSchedule_Employee.Columns(8).Visible = False
                Dim item1 As New RadComboBoxItem
                Dim item2 As New RadComboBoxItem
                Dim item3 As New RadComboBoxItem
                Dim item4 As New RadComboBoxItem
                Dim item5 As New RadComboBoxItem
                item1.Value = -1
                item1.Text = "--Please Select--"
                RadCmbBxScheduletype.Items.Add(item1)
                item2.Value = 1
                item2.Text = "Normal"
                RadCmbBxScheduletype.Items.Add(item2)
                item3.Value = 2
                item3.Text = "Flexible"
                RadCmbBxScheduletype.Items.Add(item3)
                item4.Value = 3
                item4.Text = "Advanced"
                RadCmbBxScheduletype.Items.Add(item4)
                item5.Value = 5
                item5.Text = "Open"
                RadCmbBxScheduletype.Items.Add(item5)
            End If
            Me.dtpFromdate.SelectedDate = Today
            Me.dtpEndDate.SelectedDate = Today
            EmployeeFilterUC.CompanyRequiredFieldValidationGroup = btnSave.ValidationGroup

            'Assign_Emp.HeaderText = ResourceManager.GetString("Assign_Emp", CultureInfo)
            'FillGrid()
            objAPP_Settings = New APP_Settings
            With objAPP_Settings
                .GetByPK()
                If .FillCheckBoxList = 1 Then
                    FillEmployee()
                End If
                If .AllowDeleteSchedule = False Then
                    btnDelete.Visible = False
                Else
                    btnDelete.Visible = True
                End If
            End With
            ArcivingMonths_DateValidation()
        End If

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not trControls.FindControl(row("AddBtnName")) Is Nothing Then
                        trControls.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not trControls.FindControl(row("DeleteBtnName")) Is Nothing Then
                        trControls.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not trControls.FindControl(row("EditBtnName")) Is Nothing Then
                        trControls.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not trControls.FindControl(row("PrintBtnName")) Is Nothing Then
                        trControls.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next
    End Sub

    Protected Sub RadCmbBxScheduletype_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxScheduletype.SelectedIndexChanged
        FillSchedule()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If ValidateEndDate() = False Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DateRange", CultureInfo), "info")
            Return
        End If

        Dim errornum As Integer
        objRECALC_REQUEST = New RECALC_REQUEST
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)

        If RadCmbBxSchedules.SelectedValue <> -1 Then
            Dim arr As String()
            arr = RadCmbBxSchedules.SelectedValue.Split(",")
            Dim flag As Boolean = False
            If cblEmpList.SelectedItem Is Nothing Then
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("EmployeeSelect", CultureInfo), "info")
                Return
            End If
            For Each item As ListItem In cblEmpList.Items
                If item.Selected = True Then
                    flag = True
                    Dim objEmp_WorkSchedule As New Emp_WorkSchedule
                    If EmployeeId <> 0 Then
                        objEmp_WorkSchedule.FK_EmployeeId = EmployeeId
                    Else
                        objEmp_WorkSchedule.FK_EmployeeId = item.Value
                    End If
                    objEmp_WorkSchedule.FK_ScheduleId = RadCmbBxSchedules.SelectedValue.ToString().Split(",")(0)
                    If IsAdvanced = 1 Then

                        objEmp_WorkSchedule.ScheduleType = 3
                    Else
                        objEmp_WorkSchedule.ScheduleType = RadCmbBxScheduletype.SelectedValue
                    End If


                    objEmp_WorkSchedule.FromDate = dtpFromdate.SelectedDate
                    objEmp_WorkSchedule.ToDate = IIf(dtpEndDate.SelectedDate Is Nothing Or Not chckTemporary.Checked, DateTime.MinValue, dtpEndDate.SelectedDate)
                    objEmp_WorkSchedule.IsTemporary = chckTemporary.Checked
                    objEmp_WorkSchedule.CREATED_BY = SessionVariables.LoginUser.ID
                    If EmpWorkScheduleId <> 0 Then
                        objEmp_WorkSchedule.EmpWorkScheduleId = EmpWorkScheduleId
                        objEmp_WorkSchedule.LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                        errornum = objEmp_WorkSchedule.Update
                    Else
                        errornum = objEmp_WorkSchedule.AssignSchedule()
                    End If


                End If
            Next
            Dim errornum2 As Integer
            For Each list As ListItem In cblEmpList.Items
                If errornum = 0 Then
                    If Not dtpFromdate.SelectedDate > Date.Today Then
                        If list.Selected = True Then
                            objRecalculateRequest = New RecalculateRequest
                            objEmployee = New Employee
                            EmployeeId = list.Value
                            objEmployee.EmployeeId = EmployeeId
                            objEmployee.GetByPK()
                            objRecalculateRequest.Fk_CompanyId = objEmployee.FK_CompanyId
                            objRecalculateRequest.Fk_EntityId = objEmployee.FK_EntityId
                            objRecalculateRequest.Fk_EmployeeId = EmployeeId
                            objRecalculateRequest.ImmediatelyStart = True
                            objRecalculateRequest.FromDate = dtpFromdate.SelectedDate
                            If EmpWorkScheduleId <> 0 Then
                                objRecalculateRequest.Remarks = "Assign Schedule - SYSTEM"
                            Else
                                objRecalculateRequest.Remarks = "Update Schedule - SYSTEM"
                            End If
                            If chckTemporary.Checked = True Then
                                If Not dtpEndDate.SelectedDate Is Nothing Or Not dtpEndDate.SelectedDate > Date.Today Then
                                    objRecalculateRequest.ToDate = dtpEndDate.SelectedDate
                                End If
                            Else
                                objRecalculateRequest.ToDate = Date.Today
                            End If
                            objRecalculateRequest.CREATED_BY = SessionVariables.LoginUser.ID
                            errornum2 = objRecalculateRequest.Add()
                        End If
                    End If
                End If
            Next

            If errornum = 0 And errornum2 = 0 Then

                If flag = False Then
                    CtlCommon.ShowMessage(Page, ResourceManager.GetString("EmployeeSelect", CultureInfo), "info")
                Else
                    CtlCommon.ShowMessage(Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
                    ClearAll()
                End If
            ElseIf errornum = -99 Then
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("DateExist", CultureInfo), "info")
            Else
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")

            End If

        End If

        FillGrid()
    End Sub

    Protected Sub dgrdSchedule_Employee_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdSchedule_Employee.SelectedIndexChanged
        Literal1.Visible = False
        Literal2.Visible = False
        objEmp_WorkSchedule = New Emp_WorkSchedule
        EmpWorkScheduleId = CInt(CType(dgrdSchedule_Employee.SelectedItems(0), GridDataItem).GetDataKeyValue("EmpWorkScheduleId").ToString())
        ScheduleId = CInt(CType(dgrdSchedule_Employee.SelectedItems(0), GridDataItem).GetDataKeyValue("ScheduleId").ToString())
        CompanyId = CInt(CType(dgrdSchedule_Employee.SelectedItems(0), GridDataItem).GetDataKeyValue("CompanyId").ToString())
        EntityId = CInt(CType(dgrdSchedule_Employee.SelectedItems(0), GridDataItem).GetDataKeyValue("EntityId").ToString())
        EmployeeId = CInt(CType(dgrdSchedule_Employee.SelectedItems(0), GridDataItem).GetDataKeyValue("EmployeeId").ToString())
        Dim EmpName As String = CType(dgrdSchedule_Employee.SelectedItems(0), GridDataItem).GetDataKeyValue("EmployeeName").ToString()
        Dim EmpArName As String = CType(dgrdSchedule_Employee.SelectedItems(0), GridDataItem).GetDataKeyValue("EmployeeArabicName").ToString()

        'Dim flag As Boolean = False
        'For Each item As ListItem In cblEmpList.Items
        '    If item.Selected = True Then
        '    flag = True
        '    objEmp_WorkSchedule = New Emp_WorkSchedule

        With objEmp_WorkSchedule
            .EmpWorkScheduleId = EmpWorkScheduleId
            .GetByPK()
            RadCmbBxScheduletype.SelectedValue = .ScheduleType
            FillSchedule()
            RadCmbBxSchedules.SelectedValue = .FK_ScheduleId.ToString + "," + .ScheduleType.ToString
            dtpFromdate.SelectedDate = .FromDate
            chckTemporary.Checked = .IsTemporary
            showHide(chckTemporary.Checked)
            If chckTemporary.Checked = True Then
                If Not .ToDate = Nothing Then
                    showHide(chckTemporary.Checked)
                    dtpEndDate.SelectedDate = .ToDate
                Else
                    showHide(chckTemporary.Checked)
                End If
            End If
            EmployeeFilterUC.CompanyId = CompanyId
            EmployeeFilterUC.FillCompanies()
            cblEmpList.Items.Clear()
            If SessionVariables.CultureInfo = "ar-JO" Then
                'cblEmpList.Items.Add(EmpArName)
                cblEmpList.Items.Add(New ListItem(EmpArName.ToString(), EmployeeId.ToString()))
            Else
                cblEmpList.Items.Add(New ListItem(EmpName.ToString(), EmployeeId.ToString()))
                'cblEmpList.Items.Add(EmpName)
            End If
            cblEmpList.Items(0).Selected = True

            EmployeeFilterUC.SelectByID("Company")
            EmployeeFilterUC.EntityId = EntityId
            EmployeeFilterUC.FillEntity()
            EmployeeFilterUC.SelectByID("Entity")
        End With
        ManageControls(False)
        'End If
        'Next
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Sub dgrdSchedule_Employee_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdSchedule_Employee.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub dgrdSchedule_Employee_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdSchedule_Employee.NeedDataSource
        Try
            objEmp_WorkSchedule = New Emp_WorkSchedule
            If IsAdvanced = 1 Then
                If IsManager = 1 Then
                    objEmp_WorkSchedule.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
                    dgrdSchedule_Employee.DataSource = objEmp_WorkSchedule.Get_Emp_Schedule_Details_Advanced_Mgr()
                Else
                    dgrdSchedule_Employee.DataSource = objEmp_WorkSchedule.Get_Emp_Schedule_Details_Advanced

                End If
            ElseIf EmployeeFilterUC_Grid.EmployeeId <> 0 Then
                objEmp_WorkSchedule.FK_EmployeeId = EmployeeFilterUC_Grid.EmployeeId
                dgrdSchedule_Employee.DataSource = objEmp_WorkSchedule.Get_Emp_Schedule_Details
            Else
                dgrdSchedule_Employee.DataSource = dtGridCurrent
            End If



        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        ClearAll()
        ManageControls(True)
        EmployeeFilterUC.ManageControls(True)
        FillEmployee()
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1
        Dim errNum2 As Integer = -1
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdSchedule_Employee.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim intEmpWorkScheduleId As Integer = Convert.ToInt32(row.GetDataKeyValue("EmpWorkScheduleId").ToString())
                objEmp_WorkSchedule = New Emp_WorkSchedule()
                objEmp_WorkSchedule.EmpWorkScheduleId = intEmpWorkScheduleId
                objEmp_WorkSchedule.GetByPK()
                EmployeeId = objEmp_WorkSchedule.FK_EmployeeId
                FromDate = objEmp_WorkSchedule.FromDate
                ToDate = objEmp_WorkSchedule.ToDate
                errNum = objEmp_WorkSchedule.Delete()
                With strBuilder
                    If errNum = 0 Then
                        objRecalculateRequest = New RecalculateRequest
                        objEmployee = New Employee
                        objEmployee.EmployeeId = EmployeeId
                        objEmployee.GetByPK()
                        objRecalculateRequest.Fk_CompanyId = objEmployee.FK_CompanyId
                        objRecalculateRequest.Fk_EntityId = objEmployee.FK_EntityId
                        objRecalculateRequest.Fk_EmployeeId = EmployeeId
                        objRecalculateRequest.ImmediatelyStart = True
                        objRecalculateRequest.FromDate = FromDate
                        objRecalculateRequest.Remarks = "Delete Schedule - SYSTEM"
                        If Not ToDate = Nothing Then
                            objRecalculateRequest.ToDate = ToDate
                        Else
                            objRecalculateRequest.ToDate = Date.Today
                        End If
                        objRecalculateRequest.CREATED_BY = SessionVariables.LoginUser.FK_EmployeeId
                        errNum2 = objRecalculateRequest.Add()
                    End If
                End With
            End If
        Next
        If errNum = 0 And errNum2 = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillGrid()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord", CultureInfo), "error")
            'showResult(CodeResultMessage.CodeDeleteFail)

        End If

        ClearAll()
    End Sub

    Protected Sub btnRetrieve_Click(sender As Object, e As System.EventArgs) Handles btnRetrieve.Click
        objEmployee = New Employee()
        objEmployee.EmployeeNo = txtEmployeeNo.Text
        objEmployee.UserId = SessionVariables.LoginUser.ID
        If objVersion.HasMultiCompany Then
            objEmployee.FK_CompanyId = EmployeeFilterUC.CompanyId
        Else
            objEmployee.FK_CompanyId = objVersion.GetCompanyId
        End If
        Dim dtEmployee As DataTable = objEmployee.GetEmpByEmpNo()
        If dtEmployee.Rows.Count > 0 Then
            EmployeeId = dtEmployee.Rows(0)("EmployeeId")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EmpNonotinlevel", CultureInfo), "info")
            Exit Sub
        End If
        FillEmployee()
    End Sub

    Protected Sub Repeater1_ItemCommand(source As Object, e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles Repeater1.ItemCommand

        Dim page As Integer = CInt(CType(e.CommandSource, LinkButton).Text)
        PageNo = page
        FillEmployee()

    End Sub

    Protected Sub btnFilter_Click(sender As Object, e As System.EventArgs) Handles btnFilter.Click
        FillGrid_ByFilter()
    End Sub

#End Region

#Region "Methods"

    'Public Sub FillEmployee2()
    '    Dim dt As DataTable
    '    cblEmpList.Items.Clear()
    '    cblEmpList.Text = String.Empty
    '    hlViewEmployee.Visible = False
    '    If SessionVariables.CultureInfo = "ar-JO" Then
    '        Lang = CtlCommon.Lang.AR
    '    Else
    '        Lang = CtlCommon.Lang.EN
    '    End If
    '    If EmployeeFilterUC.CompanyId <> 0 Then
    '        Dim objEmployee As New Employee
    '        objEmployee.FK_CompanyId = EmployeeFilterUC.CompanyId
    '        If (EmployeeFilterUC.EntityId <> 0) Then
    '            objEmployee.FK_EntityId = EmployeeFilterUC.EntityId
    '        Else
    '            objEmployee.FK_EntityId = -1
    '        End If
    '        objEmployee.Status = 1
    '        objEmployee.FK_EntityId = EmployeeFilterUC.EntityId
    '        If IsManager = 1 Then
    '            objEmployee.FK_ManagerID = FK_ManagerId
    '            dt = objEmployee.GetEmployeeByManagerIdForEntity
    '        Else
    '            dt = objEmployee.GetEmpByStatus
    '        End If


    '        If (dt IsNot Nothing) Then
    '            Dim dtEmployees As DataTable = dt
    '            If (dtEmployees IsNot Nothing) Then
    '                If (dtEmployees.Rows.Count > 0) Then
    '                    Dim dtSource As New DataTable
    '                    dtSource.Columns.Add("EmployeeId")
    '                    dtSource.Columns.Add("EmployeeName")
    '                    Dim drRow As DataRow
    '                    drRow = dtSource.NewRow()

    '                    Dim counter As Integer = 0
    '                    For Item As Integer = 0 To dtEmployees.Rows.Count - 1


    '                        Dim drSource As DataRow
    '                        drSource = dtSource.NewRow
    '                        Dim dcCell1 As New DataColumn
    '                        Dim dcCell2 As New DataColumn
    '                        dcCell1.ColumnName = "EmployeeId"
    '                        dcCell2.ColumnName = "EmployeeName"
    '                        dcCell1.DefaultValue = dtEmployees.Rows(Item)(0)
    '                        dcCell2.DefaultValue = dtEmployees.Rows(Item)(3) + "-" + IIf(Lang = CtlCommon.Lang.EN, dtEmployees.Rows(Item)(1), dtEmployees.Rows(Item)(2))
    '                        drSource("EmployeeId") = dcCell1.DefaultValue
    '                        drSource("EmployeeName") = dcCell2.DefaultValue
    '                        dtSource.Rows.Add(drSource)
    '                    Next



    '                    If (dt IsNot Nothing) Then
    '                        Dim dv As New DataView(dtSource)
    '                        If SessionVariables.CultureInfo = "ar-JO" Then
    '                            'dv.Sort = "EmployeeArabicName"
    '                            counter = 0

    '                            For Each row As DataRowView In dv
    '                                If counter >= 1000 Then
    '                                    Exit For
    '                                End If
    '                                counter = counter + 1

    '                                If (Not EmployeeId = 0) Then
    '                                    If EmployeeId = row("EmployeeId") Then
    '                                        cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
    '                                        Exit For
    '                                    End If
    '                                Else
    '                                    cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
    '                                End If
    '                            Next
    '                        Else
    '                            For Each row As DataRowView In dv
    '                                If (Not EmployeeId = 0) Then
    '                                    If EmployeeId = row("EmployeeId") Then
    '                                        cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
    '                                        Exit For
    '                                    End If
    '                                Else
    '                                    cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
    '                                End If
    '                            Next
    '                        End If
    '                    End If
    '                End If
    '            End If
    '        End If
    '    End If

    'End Sub

    Public Sub FillEmployee()
        If PageNo = 0 Then
            PageNo = 1
        End If
        Repeater1.Visible = False
        Dim dt As DataTable
        cblEmpList.Items.Clear()
        cblEmpList.Text = String.Empty
        hlViewEmployee.Visible = False
        If SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
        Else
            Lang = CtlCommon.Lang.EN
        End If
        If EmployeeFilterUC.CompanyId <> 0 Then
            Dim objEmployee As New Employee
            objEmployee.FK_CompanyId = EmployeeFilterUC.CompanyId
            If (EmployeeFilterUC.EntityId <> 0) Then
                objEmployee.FK_EntityId = EmployeeFilterUC.EntityId
            Else
                objEmployee.FK_EntityId = -1
            End If

            objEmployee.EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            If Not EmployeeFilterUC.EntityId = -1 Then
                objEmployee.FK_EntityId = EmployeeFilterUC.EntityId
            Else
                objEmployee.FK_EntityId = 0
            End If
            objEmployee.Get_EmployeeRecordCount()
            Emp_RecordCount = objEmployee.Emp_RecordCount

            objEmployee.Status = 1
            objEmployee.FK_EntityId = EmployeeFilterUC.EntityId

            If IsManager = 1 Then
                objEmployee.FK_ManagerID = FK_ManagerId
                dt = objEmployee.GetEmployeeByManagerIdForEntity
            Else
                If EmployeeId = 0 Then
                    objEmployee.PageNo = PageNo
                    objEmployee.PageSize = 1000
                    dt = objEmployee.GetEmpByStatus
                Else
                    objEmployee.EmployeeId = EmployeeId
                    dt = objEmployee.GetByEmpId()
                End If
                'dt = objEmployee.GetEmpByStatus
            End If

            ' fill pager 
            'Dim pagefrom, pageto As Integer
            If Emp_RecordCount > 1000 And EmployeeId = 0 Then

                Dim dtpaging As New DataTable
                dtpaging.Columns.Add("pageNo")
                Dim index As Integer
                Dim empcount As Integer
                'empcount = dt.Rows.Count
                empcount = Emp_RecordCount
                For index = 0 To empcount Step 1000
                    Dim drpaging As DataRow
                    drpaging = dtpaging.NewRow
                    Dim dcCell3 As New DataColumn
                    dcCell3.ColumnName = "PageNo"
                    drpaging(0) = index / 1000 + 1
                    dtpaging.Rows.Add(drpaging)

                Next
                Repeater1.DataSource = dtpaging
                Repeater1.DataBind()
                For Each item2 As RepeaterItem In Repeater1.Items
                    Dim lnk As LinkButton = DirectCast(item2.FindControl("LinkButton1"), LinkButton)
                    If lnk.Text = PageNo Then
                        lnk.Attributes.Add("style", "color:Blue;font-weight:bold;text-decoration: underline")
                    End If
                Next
                'pagefrom = ((PageNo - 1) * 1000) + 1
                'pageto = PageNo * 1000
                Repeater1.Visible = True
            Else
                Repeater1.Visible = False
            End If

            If EmployeeId > 0 Then
                cblEmpList.DataSource = Nothing
                Repeater1.DataSource = Nothing
                Repeater1.DataBind()
            End If
            ' end fill pager

            If (dt IsNot Nothing) Then
                Dim dtEmployees As DataTable = dt
                If (dtEmployees IsNot Nothing) Then
                    If (dtEmployees.Rows.Count > 0) Then
                        'Repeater1.Visible = True
                        Dim dtSource As New DataTable
                        dtSource.Columns.Add("EmployeeId")
                        dtSource.Columns.Add("EmployeeName")
                        Dim drRow As DataRow
                        drRow = dtSource.NewRow()

                        For Item As Integer = 0 To dtEmployees.Rows.Count - 1
                            ' If Item + 1 >= pagefrom And Item + 1 <= pageto Then

                            Dim drSource As DataRow
                            drSource = dtSource.NewRow
                            Dim dcCell1 As New DataColumn
                            Dim dcCell2 As New DataColumn
                            dcCell1.ColumnName = "EmployeeId"
                            dcCell2.ColumnName = "EmployeeName"
                            dcCell1.DefaultValue = dtEmployees.Rows(Item)(0)
                            dcCell2.DefaultValue = dtEmployees.Rows(Item)(3) + "-" + IIf(Lang = CtlCommon.Lang.EN, dtEmployees.Rows(Item)(1), dtEmployees.Rows(Item)(2))
                            drSource("EmployeeId") = dcCell1.DefaultValue
                            drSource("EmployeeName") = dcCell2.DefaultValue
                            dtSource.Rows.Add(drSource)

                            'ElseIf EmployeeId > 0 Then

                            '    Dim drSource As DataRow
                            '    drSource = dtSource.NewRow
                            '    Dim dcCell1 As New DataColumn
                            '    Dim dcCell2 As New DataColumn
                            '    dcCell1.ColumnName = "EmployeeId"
                            '    dcCell2.ColumnName = "EmployeeName"
                            '    dcCell1.DefaultValue = dtEmployees.Rows(Item)(0)
                            '    dcCell2.DefaultValue = dtEmployees.Rows(Item)(3) + "-" + IIf(Lang = CtlCommon.Lang.EN, dtEmployees.Rows(Item)(1), dtEmployees.Rows(Item)(2))
                            '    drSource("EmployeeId") = dcCell1.DefaultValue
                            '    drSource("EmployeeName") = dcCell2.DefaultValue
                            '    dtSource.Rows.Add(drSource)
                            'Else
                            '    Dim drSource As DataRow
                            '    drSource = dtSource.NewRow
                            '    Dim dcCell1 As New DataColumn
                            '    Dim dcCell2 As New DataColumn
                            '    dcCell1.ColumnName = "EmployeeId"
                            '    dcCell2.ColumnName = "EmployeeName"
                            '    dcCell1.DefaultValue = dtEmployees.Rows(Item)(0)
                            '    dcCell2.DefaultValue = dtEmployees.Rows(Item)(3) + "-" + IIf(Lang = CtlCommon.Lang.EN, dtEmployees.Rows(Item)(1), dtEmployees.Rows(Item)(2))
                            '    drSource("EmployeeId") = dcCell1.DefaultValue
                            '    drSource("EmployeeName") = dcCell2.DefaultValue
                            '    dtSource.Rows.Add(drSource)
                            'End If
                        Next
                        If (dt IsNot Nothing) Then
                            Dim dv As New DataView(dtSource)
                            If SessionVariables.CultureInfo = "ar-JO" Then
                                'dv.Sort = "EmployeeArabicName"
                                For Each row As DataRowView In dv
                                    If (Not EmployeeId = 0) Then
                                        If EmployeeId = row("EmployeeId") Then
                                            cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"

                                            Exit For
                                        End If
                                    Else
                                        cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
                                    End If
                                Next
                            Else
                                For Each row As DataRowView In dv

                                    If (Not EmployeeId = 0) Then
                                        If EmployeeId = row("EmployeeId") Then
                                            cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
                                            Exit For
                                        End If
                                    Else
                                        cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
                                    End If
                                Next
                            End If
                        End If
                    End If
                End If
            End If
        Else
            objEmployee = New Employee()
            objEmployee.EmployeeNo = txtEmployeeNo.Text
            objEmployee.UserId = SessionVariables.LoginUser.ID
            If objVersion.HasMultiCompany Then
                objEmployee.FK_CompanyId = EmployeeFilterUC.CompanyId
            Else
                objEmployee.FK_CompanyId = objVersion.GetCompanyId
            End If
            Dim dtEmployees As DataTable = objEmployee.GetEmpByEmpNo()
            If (dtEmployees IsNot Nothing) Then
                If (dtEmployees.Rows.Count > 0) Then
                    Dim dtSource As New DataTable
                    dtSource.Columns.Add("EmployeeId")
                    dtSource.Columns.Add("EmployeeName")
                    Dim drRow As DataRow
                    drRow = dtSource.NewRow()

                    Dim drSource As DataRow
                    drSource = dtSource.NewRow
                    Dim dcCell1 As New DataColumn
                    Dim dcCell2 As New DataColumn
                    dcCell1.ColumnName = "EmployeeId"
                    dcCell2.ColumnName = "EmployeeName"
                    dcCell1.DefaultValue = dtEmployees.Rows(0)(0)
                    dcCell2.DefaultValue = dtEmployees.Rows(0)(1) + "-" + IIf(Lang = CtlCommon.Lang.EN, dtEmployees.Rows(0)(4), dtEmployees.Rows(0)(5))
                    drSource("EmployeeId") = dcCell1.DefaultValue
                    drSource("EmployeeName") = dcCell2.DefaultValue
                    dtSource.Rows.Add(drSource)

                    Dim dv As New DataView(dtSource)
                    If SessionVariables.CultureInfo = "ar-JO" Then
                        'dv.Sort = "EmployeeArabicName"


                        For Each row As DataRowView In dv


                            If (Not EmployeeId = 0) Then
                                If EmployeeId = row("EmployeeId") Then
                                    cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"

                                    Exit For
                                End If
                            Else
                                cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
                            End If


                        Next
                    Else
                        For Each row As DataRowView In dv

                            If (Not EmployeeId = 0) Then
                                If EmployeeId = row("EmployeeId") Then
                                    cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
                                    Exit For
                                End If
                            Else
                                cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
                            End If

                        Next
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub showHide(ByVal status As Boolean, Optional ByVal order As Integer = 0)
        pnlEndDate.Visible = status
        CVDate.Visible = status
    End Sub

    Private Sub FillGrid()
        objEmp_WorkSchedule = New Emp_WorkSchedule
        If IsAdvanced = 1 Then
            If IsManager = 1 Then
                objEmp_WorkSchedule.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
                dgrdSchedule_Employee.DataSource = objEmp_WorkSchedule.Get_Emp_Schedule_Details_Advanced_Mgr()
            Else
                dgrdSchedule_Employee.DataSource = objEmp_WorkSchedule.Get_Emp_Schedule_Details_Advanced

            End If
        Else
            dgrdSchedule_Employee.DataSource = objEmp_WorkSchedule.Get_Emp_Schedule_Details
        End If

        dgrdSchedule_Employee.DataBind()
    End Sub

    Private Sub FillSchedule()
        If SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
        Else
            Lang = CtlCommon.Lang.EN
        End If

        Dim objWorkSchedule As New WorkSchedule()
        If IsAdvanced = 1 Then
            RadCmbBxScheduletype.Visible = False
            RadCmbBxScheduletype.SelectedValue = 3
            Label7.Visible = False
            CtlCommon.FillTelerikDropDownList(RadCmbBxSchedules, objWorkSchedule.GetByType(3), Lang)
            RadCmbBxSchedules.Items.Remove(0)
        Else
            If (RadCmbBxScheduletype.SelectedValue = 1) Then
                CtlCommon.FillTelerikDropDownList(RadCmbBxSchedules, objWorkSchedule.GetByType(1), Lang)
            ElseIf (RadCmbBxScheduletype.SelectedValue = 2) Then
                CtlCommon.FillTelerikDropDownList(RadCmbBxSchedules, objWorkSchedule.GetByType(2), Lang)
            ElseIf (RadCmbBxScheduletype.SelectedValue = 3) Then
                CtlCommon.FillTelerikDropDownList(RadCmbBxSchedules, objWorkSchedule.GetByType(3), Lang)
            ElseIf (RadCmbBxScheduletype.SelectedValue = 5) Then
                CtlCommon.FillTelerikDropDownList(RadCmbBxSchedules, objWorkSchedule.GetByType(5), Lang)
            End If
        End If

    End Sub

    Private Sub ClearAll()
        ' Clear the controls
        Literal1.Visible = True
        Literal2.Visible = True
        EmployeeFilterUC.ClearValues()

        RadCmbBxSchedules.Text = String.Empty
        RadCmbBxScheduletype.SelectedValue = -1
        chckTemporary.Checked = False
        cblEmpList.Items.Clear()
        pnlEndDate.Visible = False
        dtpFromdate.SelectedDate = Today
        dtpEndDate.SelectedDate = Today
        txtEmployeeNo.Text = String.Empty
        EmpWorkScheduleId = 0
        CompanyId = 0
        EntityId = 0
        EmployeeId = 0
        ScheduleId = 0
        ManageControls(True)
        'Repeater1.Visible = False
    End Sub

    Public Sub CompanyChanged()
        EmployeeFilterUC.FillEntity()
        objAPP_Settings = New APP_Settings
        With objAPP_Settings
            .GetByPK()
            If .FillCheckBoxList = 1 Then
                If Not EmployeeFilterUC.CompanyId = -1 Then
                    FillEmployee()
                Else
                    EmployeeId = 0
                    txtEmployeeNo.Text = String.Empty
                    FillEmployee()
                    Repeater1.Visible = False
                    EmployeeFilterUC.ClearValues()
                End If
            Else
                If EmployeeFilterUC.CompanyId = -1 Then
                    EmployeeId = 0
                    txtEmployeeNo.Text = String.Empty
                    FillEmployee()
                    Repeater1.Visible = False
                    EmployeeFilterUC.ClearValues()
                End If
            End If
        End With
    End Sub

    Public Sub EntityChanged()
        If Not EmployeeFilterUC.CompanyId = Nothing Or Not EmployeeFilterUC.CompanyId = -1 Then
            PageNo = 0
            FillEmployee()
        Else
            Repeater1.Visible = False

        End If

    End Sub

    Private Function GetNavigateURL(ByVal CompanyId As String, ByVal EntityId As String) As String
        Dim res As String = "javascript:open_window('../Reports/EmployeeInfo.aspx?company={0}&entity={1}','',700,400)"

        Return String.Format(res, CompanyId, IIf(String.IsNullOrEmpty(EntityId), "-1", EntityId))
    End Function

    Public Sub ManageControls(ByVal status As Boolean)
        EmployeeFilterUC.ManageControls(status)
        RadCmbBxSchedules.Enabled = status
        RadCmbBxScheduletype.Enabled = status
        cblEmpList.Enabled = status
    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdSchedule_Employee.Skin))
    End Function

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
        Return TempDate.Year.ToString() + tempMonth + tempDay
    End Function

    Private Sub FillGrid_ByFilter()
        objEmp_WorkSchedule = New Emp_WorkSchedule
        With objEmp_WorkSchedule
            objEmp_WorkSchedule.FK_EmployeeId = EmployeeFilterUC_Grid.EmployeeId
            dtGridCurrent = objEmp_WorkSchedule.Get_Emp_Schedule_Details
            dgrdSchedule_Employee.DataSource = dtGridCurrent
            dgrdSchedule_Employee.DataBind()
        End With
    End Sub

    Private Sub ArcivingMonths_DateValidation()
        objAPP_Settings = New APP_Settings
        objAPP_Settings.GetByPK()
        Dim ArchivingMonths As Integer = objAPP_Settings.ArchivingMonths

        If Not ArchivingMonths = 0 Then
            ArchivingMonths = ArchivingMonths
            Dim NewDate As Date = DateAdd(DateInterval.Month, ArchivingMonths, Date.Today)
            dtpFromdate.MinDate = New Date(NewDate.Year, NewDate.Month, NewDate.Day)
            dtpEndDate.MinDate = New Date(NewDate.Year, NewDate.Month, NewDate.Day)
        End If
    End Sub

#End Region

#Region "Start & End date validation"

    Private Function ValidateEndDate() As Boolean
        If IS_Temporary() And dtpEndDate.SelectedDate() IsNot Nothing Then
            Return StartEndDateComparison(dtpFromdate.SelectedDate, dtpEndDate.SelectedDate)
        End If
        Return True
    End Function

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
                'CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DateRangeEarly", CultureInfo))

                dtpEndDate.Focus()
                Return False
                'ElseIf result = 0 Then
                '    dtpEndDate.Focus()
                '    Return False
            Else
                ' Do nothing
                Return True
            End If
        End If
    End Function

    Private Function IS_Temporary() As Boolean
        Return chckTemporary.Checked
    End Function

#End Region

End Class
