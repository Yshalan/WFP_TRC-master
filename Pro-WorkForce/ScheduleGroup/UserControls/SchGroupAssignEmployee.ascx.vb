Imports TA.Admin.OrgCompany
Imports SmartV.UTILITIES
Imports TA.Lookup
Imports System.Data
Imports TA.Employees
Imports TA.Definitions
Imports TA.Security
Imports Telerik.Web.UI
Imports TA.DailyTasks
Imports TA.ScheduleGroups
Imports System.Reflection
Imports TA.Admin

Partial Class ScheduleGroup_UserControls_SchGroupAssignEmployee
    Inherits System.Web.UI.UserControl

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private objEmp_WorkSchedule As Emp_WorkSchedule
    Private objRECALC_REQUEST As RECALC_REQUEST
    Private objEmployee As Employee
    Private objScheduleGroups_Employees As ScheduleGroups_Employees
    Private objEmployee_Manager As Employee_Manager
    Private objScheduleGroups_Managers As ScheduleGroups_Managers
    Private objAPP_Settings As APP_Settings
    Private objRecalculateRequest As RecalculateRequest
    Private objVersion As SmartV.Version.version

#End Region

#Region "Properties"
    Public Property GroupEmployeeId() As Integer
        Get
            Return ViewState("GroupEmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("GroupEmployeeId") = value
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
    Public Property ManagerId() As Integer
        Get
            Return ViewState("ManagerId")
        End Get
        Set(ByVal value As Integer)
            ViewState("ManagerId") = value
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
    Public Property PageNo() As Integer
        Get
            Return ViewState("PageNo")
        End Get
        Set(ByVal value As Integer)
            ViewState("PageNo") = value
        End Set
    End Property
#End Region

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            showHide(chckTemporary.Checked)
            If Not Page.IsPostBack Then
                If (SessionVariables.CultureInfo Is Nothing) Then
                    Response.Redirect("~/default/Login.aspx")
                ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                    Lang = CtlCommon.Lang.AR
                    dgrdSchedule_EmployeeGroups.Columns(5).Visible = False
                    dgrdSchedule_EmployeeGroups.Columns(7).Visible = False
                    dgrdSchedule_EmployeeGroups.Columns(11).Visible = False
                    dgrdSchedule_EmployeeGroups.Columns(13).Visible = False
                Else
                    Lang = CtlCommon.Lang.EN
                    dgrdSchedule_EmployeeGroups.Columns(6).Visible = False
                    dgrdSchedule_EmployeeGroups.Columns(8).Visible = False
                    dgrdSchedule_EmployeeGroups.Columns(12).Visible = False
                    dgrdSchedule_EmployeeGroups.Columns(14).Visible = False

                End If
                Me.dtpFromdate.SelectedDate = Today
                Me.dtpEndDate.SelectedDate = Today
                EmployeeFilterUC.CompanyRequiredFieldValidationGroup = btnSave.ValidationGroup
                FillSchGroup()

                FillGrid()
                objAPP_Settings = New APP_Settings
                With objAPP_Settings
                    .GetByPK()
                    If .FillCheckBoxList = 1 Then
                        FillEmployee()
                    End If
                End With
            End If
            btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdSchedule_EmployeeGroups.ClientID + "')")
            Assign_Emp.HeaderText = ResourceManager.GetString("AssignEmp_ScheduleGroup", CultureInfo)
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
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
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub FillSchGroup()
        Try
            Dim obj As New ScheduleGroups
            If SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            CtlCommon.FillTelerikDropDownList(CmbSchGroup, obj.GetAllForFill(Lang), Lang)
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If ValidateEndDate() = False Then
            CtlCommon.ShowMessage(Page, "End Date should be greater than or equal to From Date", "info")
            Return
        End If

        Dim errornum As Integer

        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        objScheduleGroups_Employees = New ScheduleGroups_Employees

        Dim flag As Boolean = False

        For Each item As ListItem In cblEmpList.Items
            If item.Selected = True Then
                flag = True

                EmployeeId = item.Value
                objScheduleGroups_Employees.FK_EmployeeId = EmployeeId
                objScheduleGroups_Employees.FK_GroupId = CmbSchGroup.SelectedValue
                objScheduleGroups_Employees.FromDate = dtpFromdate.SelectedDate
                objScheduleGroups_Employees.ToDate = IIf(dtpEndDate.SelectedDate Is Nothing Or Not chckTemporary.Checked, DateTime.MinValue, dtpEndDate.SelectedDate)
                objScheduleGroups_Employees.IsTemp = chckTemporary.Checked
                objScheduleGroups_Employees.CREATED_BY = SessionVariables.LoginUser.ID
                If GroupEmployeeId <> 0 Then
                    objScheduleGroups_Employees.GroupEmployeeId = GroupEmployeeId
                    errornum = objScheduleGroups_Employees.Update
                Else
                    errornum = objScheduleGroups_Employees.Add()
                End If

                If errornum = 0 Then
                    Dim err2 As Integer = -1

                    If Not dtpFromdate.SelectedDate > Date.Today Then
                        Dim FromDate As DateTime = dtpFromdate.SelectedDate
                        objRecalculateRequest = New RecalculateRequest
                        With objRecalculateRequest
                            .Fk_EmployeeId = EmployeeId
                            .FromDate = FromDate.AddDays(-1)
                            .ToDate = IIf(dtpEndDate.SelectedDate Is Nothing Or Not chckTemporary.Checked, Date.Today, dtpEndDate.SelectedDate)
                            .ImmediatelyStart = True
                            .RecalStatus = 0
                            .CREATED_BY = SessionVariables.LoginUser.ID
                            .Remarks = "Assign Schedule Group - SYSTEM"
                            err2 = .Add
                        End With
                    End If
                End If

                If errornum = 0 Then
                    AssignManager()
                ElseIf errornum = -99 Then
                    CtlCommon.ShowMessage(Page, ResourceManager.GetString("DateExist", CultureInfo) & ", " & GetEmp_Info(EmployeeId), "info")
                End If
            End If
        Next

        If errornum = 0 Then
            If flag = False Then
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("EmployeeSelect", CultureInfo), "info")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully"), "success")
                ClearAll()

                FillGrid()
            End If
        ElseIf errornum = -99 Then
            '    CtlCommon.ShowMessage(Page, ResourceManager.GetString("DateExist", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave"), "error")
        End If

    End Sub

    Protected Sub dgrdSchedule_Employee_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdSchedule_EmployeeGroups.SelectedIndexChanged
        Try
            Literal1.Visible = False
            Literal2.Visible = False

            GroupEmployeeId = CInt(CType(dgrdSchedule_EmployeeGroups.SelectedItems(0), GridDataItem).GetDataKeyValue("GroupEmployeeId"))
            CompanyId = CInt(CType(dgrdSchedule_EmployeeGroups.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_CompanyId"))
            EntityId = CInt(CType(dgrdSchedule_EmployeeGroups.SelectedItems(0), GridDataItem).GetDataKeyValue("EntityId"))
            EmployeeId = CInt(CType(dgrdSchedule_EmployeeGroups.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_EmployeeId"))
            Dim EmpName As String = CType(dgrdSchedule_EmployeeGroups.SelectedItems(0), GridDataItem).GetDataKeyValue("EmployeeName").ToString()
            Dim EmpArName As String = CType(dgrdSchedule_EmployeeGroups.SelectedItems(0), GridDataItem).GetDataKeyValue("EmployeeArabicName").ToString()

            Dim obj As New ScheduleGroups_Employees

            With obj
                .GroupEmployeeId = GroupEmployeeId
                .GetByPK()
                dtpFromdate.SelectedDate = .FromDate
                chckTemporary.Checked = .IsTemp
                CmbSchGroup.SelectedValue = .FK_GroupId
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
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Sub dgrdSchedule_EmployeeGroups_Employee_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdSchedule_EmployeeGroups.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub dgrdSchedule_EmployeeGroups_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdSchedule_EmployeeGroups.NeedDataSource
        Try
            Dim obj As New ScheduleGroups_Employees
            If Not EmployeeFilterUC.EntityId = 0 Then
                obj.FK_EntityId = EmployeeFilterUC.EntityId
                obj.UserID = SessionVariables.LoginUser.ID
                dgrdSchedule_EmployeeGroups.DataSource = obj.GetAll_ByFk_EntityId()
            End If
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        ClearAll()
        ManageControls(True)
        EmployeeFilterUC.ManageControls(True)
        FillEmployee()
    End Sub

    Protected Sub btnRetrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRetrieve.Click
        Try
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
            End If
            FillEmployee()
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1
        Dim err2 As Integer
        objRECALC_REQUEST = New RECALC_REQUEST
        objAPP_Settings = New APP_Settings()
        objAPP_Settings = objAPP_Settings.GetByPK()

        Dim temp_str_date As String

        For Each item As GridDataItem In dgrdSchedule_EmployeeGroups.Items
            If DirectCast(item.FindControl("chk"), CheckBox).Checked Then
                Dim intGroupEmployeeId As Integer = Convert.ToInt32(item.GetDataKeyValue("GroupEmployeeId"))
                EmployeeId = Convert.ToInt32(item.GetDataKeyValue("FK_EmployeeId"))
                objScheduleGroups_Employees = New ScheduleGroups_Employees
                objScheduleGroups_Employees.GroupEmployeeId = intGroupEmployeeId
                errNum = objScheduleGroups_Employees.Delete()

                If errNum = 0 Then
                    Dim dteFrom As DateTime = Convert.ToDateTime(item("FromDate").Text)
                    Dim dteTo As DateTime
                    If Not item("ToDate").Text = "&nbsp;" Then
                        dteTo = Convert.ToDateTime(item("ToDate").Text)
                    Else
                        dteFrom = Convert.ToDateTime(Date.Today)
                    End If
                    If objAPP_Settings.ApprovalRecalMethod = 1 Then
                        While dteFrom <= dteTo
                            If Not dteFrom = Date.Now.AddDays(1).ToShortDateString() Then
                                temp_str_date = DateToString(dteFrom)
                                objRECALC_REQUEST = New RECALC_REQUEST()
                                objRECALC_REQUEST.EMP_NO = EmployeeId
                                objRECALC_REQUEST.VALID_FROM_NUM = Integer.Parse(temp_str_date)
                                err2 = objRECALC_REQUEST.RECALCULATE()
                                If Not err2 = 0 Then
                                    Exit While
                                End If
                                dteFrom = dteFrom.AddDays(1)
                            End If
                        End While
                    Else
                        If Not dteFrom > Date.Today Then
                            objRecalculateRequest = New RecalculateRequest
                            With objRecalculateRequest
                                .Fk_EmployeeId = EmployeeId
                                .FromDate = dteFrom
                                .ToDate = IIf(dteTo = DateTime.MinValue, Date.Today, dteTo)
                                .ImmediatelyStart = True
                                .RecalStatus = 0
                                .CREATED_BY = SessionVariables.LoginUser.ID
                                .Remarks = "Delete Group Schedule - SYSTEM"
                                err2 = .Add
                            End With
                        End If

                    End If

                End If

            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillGrid()
        Else
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")
        End If
    End Sub

    Private Function GetEmp_Info(ByVal FK_EmployeeId As Integer) As String
        objEmployee = New Employee
        Dim EmployeeInfo As String = ""
        With objEmployee
            .EmployeeId = FK_EmployeeId
            .GetByPK()
            EmployeeInfo = .EmployeeNo.ToString & "-" & IIf(Lang = CtlCommon.Lang.AR, (.EmployeeArabicName.ToString), (.EmployeeName.ToString))
        End With
        Return EmployeeInfo
    End Function

#End Region

#Region "Methods"

    Private Sub AssignManager()
        Dim dt As DataTable
        objEmployee_Manager = New Employee_Manager
        objScheduleGroups_Managers = New ScheduleGroups_Managers
        objScheduleGroups_Managers.FK_GroupId = CmbSchGroup.SelectedValue
        dt = objScheduleGroups_Managers.GetByGroupId()
        If dt.Rows.Count > 0 Then
            ManagerId = Convert.ToInt32(dt.Rows(0)("FK_EmployeeId"))
            With objEmployee_Manager
                .FK_ManagerId = ManagerId
                .FK_EmployeeId = EmployeeId
                .FromDate = dtpFromdate.DbSelectedDate
                If Not chckTemporary.Checked = False Then
                    .ToDate = dtpEndDate.DbSelectedDate
                End If
                .IsTemporary = chckTemporary.Checked
                '.FromDate = dt.Rows(0)("FromDate").ToString()
                'If Not String.IsNullOrEmpty(dt.Rows(0)("ToDate").ToString()) Then
                '    .ToDate = dt.Rows(0)("ToDate").ToString()
                'End If
                '.IsTemporary = dt.Rows(0)("IsTemp").ToString()

                .AssignManager()
            End With
        Else
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("ScheduleGroupManager"), "info")
        End If

    End Sub

    Public Sub FillEmployee()
        Try
            If PageNo = 0 Then
                PageNo = 1
            End If
            Repeater1.Visible = False
            cblEmpList.Items.Clear()
            cblEmpList.Text = String.Empty
            hlViewEmployee.Visible = False

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
                Dim dt As DataTable = objEmployee.GetEmpByStatus
                If EmployeeId = 0 Then
                    objEmployee.PageNo = PageNo
                    objEmployee.PageSize = 1000
                    dt = objEmployee.GetEmpByStatus
                Else
                    objEmployee.EmployeeId = EmployeeId
                    dt = objEmployee.GetByEmpId()
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
                    Dim dtEmployees = dt
                    If (dtEmployees IsNot Nothing) Then
                        If (dtEmployees.Rows.Count > 0) Then
                            'Repeater1.Visible = True
                            Dim dtSource As New DataTable
                            dtSource.Columns.Add("EmployeeId")
                            dtSource.Columns.Add("EmployeeName")
                            Dim drRow As DataRow
                            drRow = dtSource.NewRow()
                            For Item As Integer = 0 To dtEmployees.Rows.Count - 1
                                '  If Item + 1 >= pagefrom And Item + 1 <= pageto Then


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
                                '  End If
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
            End If
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub showHide(ByVal status As Boolean, Optional ByVal order As Integer = 0)
        pnlEndDate.Visible = status
        CVDate.Visible = status
    End Sub

    Private Sub FillGrid()
        Try
            Dim obj As New ScheduleGroups_Employees
            If Not EmployeeFilterUC.EntityId = 0 Then
                obj.FK_EntityId = EmployeeFilterUC.EntityId
                obj.UserID = SessionVariables.LoginUser.ID
                dgrdSchedule_EmployeeGroups.DataSource = obj.GetAll_ByFk_EntityId()
                dgrdSchedule_EmployeeGroups.DataBind()
            End If

        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

    Private Sub ClearAll()
        ' Clear the controls
        Literal1.Visible = True
        Literal2.Visible = True
        EmployeeFilterUC.ClearValues()

        CmbSchGroup.SelectedValue = -1
        chckTemporary.Checked = False
        cblEmpList.Items.Clear()
        pnlEndDate.Visible = False
        dtpFromdate.SelectedDate = Today
        dtpEndDate.SelectedDate = Today
        txtEmployeeNo.Text = String.Empty
        GroupEmployeeId = 0
        CompanyId = 0
        EntityId = 0
        EmployeeId = 0
        ScheduleId = 0
        ManageControls(True)
        cblEmpList.Items.Clear()
        Repeater1.Visible = False
        EmployeeFilterUC.ClearValues()
        EmployeeFilterUC.EntityId = -1

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
        FillGrid()
    End Sub

    Private Function GetNavigateURL(ByVal CompanyId As String, ByVal EntityId As String) As String
        Dim res As String = "javascript:open_window('../Reports/EmployeeInfo.aspx?company={0}&entity={1}','',700,400)"

        Return String.Format(res, CompanyId, IIf(String.IsNullOrEmpty(EntityId), "-1", EntityId))
    End Function

    Public Sub ManageControls(ByVal status As Boolean)
        EmployeeFilterUC.ManageControls(status)
        ' CmbSchGroup.Enabled = status
        cblEmpList.Enabled = status
    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdSchedule_EmployeeGroups.Skin))
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

    Protected Sub Repeater1_ItemCommand(source As Object, e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles Repeater1.ItemCommand

        Dim page As Integer = CInt(CType(e.CommandSource, LinkButton).Text)
        PageNo = page
        FillEmployee()

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
            ElseIf result = 0 Then
                ' Show message and focus on the end date picker
                'CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EndEqualStart", CultureInfo))


                dtpEndDate.Focus()
                Return False
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
