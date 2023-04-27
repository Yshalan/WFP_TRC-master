Imports TA.OrgCompany
Imports SmartV.UTILITIES
Imports TA.Lookup
Imports System.Data
Imports TA.Employees
Imports TA.Definitions
Imports TA.DailyTasks
Imports TA.Security
Imports Telerik.Web.UI
Imports TA.Admin

Partial Class DailyTasks_UserControls_AssignSchedule_LogicalGroup
    Inherits System.Web.UI.UserControl

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private objSchedule_LogicalGroup As Schedule_LogicalGroup
    Dim objSYSUsers As SYSUsers
    Dim objEmployee As Employee
    Private objRECALC_REQUEST As RECALC_REQUEST
    Private objRecalculateRequest As RecalculateRequest
    Private objAPP_Settings As APP_Settings

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
    Public Property GroupId() As Integer
        Get
            Return ViewState("GroupId")
        End Get
        Set(ByVal value As Integer)
            ViewState("GroupId") = value
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
    Public Property dtCurrent() As DataTable
        Get
            Return ViewState("dtCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
        End Set
    End Property
    Public Property FK_CompanyId() As Integer
        Get
            Return ViewState("FK_CompanyId")
        End Get
        Set(ByVal value As Integer)
            ViewState("FK_CompanyId") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        showHide(chckTemporary.Checked)
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
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
                RFVLogic_Group.InitialValue = "--الرجاء الاختيار--"
                rfvScheduletype.InitialValue = "--الرجاء الاختيار--"
            Else
                Lang = CtlCommon.Lang.EN
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
                RFVLogic_Group.InitialValue = "--Please Select--"
                rfvScheduletype.InitialValue = "--Please Select--"
            End If
            Fill_LogicalGroup()
            'FillCompany()
            Me.dtpFromdate.SelectedDate = Today
            Me.dtpEndDate.SelectedDate = Today
            FillGrid()
            'Assign_LogicalGrp.HeaderText = ResourceManager.GetString("Assign_LogicalGrp", CultureInfo)
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

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        ClearAll()
        ManageControls(True)
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Sub dgrdSchedule_LogicalGroup_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdSchedule_LogicalGroup.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub dgrdSchedule_LogicalGroup_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdSchedule_LogicalGroup.NeedDataSource
        Try
            objSchedule_LogicalGroup = New Schedule_LogicalGroup
            dgrdSchedule_LogicalGroup.DataSource = objSchedule_LogicalGroup.Get_LogicalGroup_Schedule_details
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If ValidateEndDate() = False Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DateRange", CultureInfo), "info")
            Return
        End If

        Dim errornum As Integer
        objRECALC_REQUEST = New RECALC_REQUEST
        Dim temp_str_date As String
        Dim err2 As Integer

        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        If RadCmbBxSchedules.SelectedValue <> -1 Then
            Dim arr As String()
            arr = RadCmbBxSchedules.SelectedValue.Split(",")
            Dim flag As Boolean = False


            Dim objSchedule_LogicalGroup As New Schedule_LogicalGroup
            objSchedule_LogicalGroup.FK_LogicalGroupId = RadCmbBxLogicalGroup.SelectedValue
            objSchedule_LogicalGroup.FK_ScheduleId = RadCmbBxSchedules.SelectedValue.ToString().Split(",")(0)
            objSchedule_LogicalGroup.ScheduleType = RadCmbBxScheduletype.SelectedValue
            objSchedule_LogicalGroup.FromDate = dtpFromdate.SelectedDate
            objSchedule_LogicalGroup.ToDate = IIf(dtpEndDate.SelectedDate Is Nothing Or Not chckTemporary.Checked, DateTime.MinValue, dtpEndDate.SelectedDate)
            objSchedule_LogicalGroup.IsTemporary = chckTemporary.Checked
            objSchedule_LogicalGroup.CREATED_BY = SessionVariables.LoginUser.ID
            If EmpWorkScheduleId <> 0 Then
                objSchedule_LogicalGroup.EmpWorkScheduleId = EmpWorkScheduleId
                objSchedule_LogicalGroup.LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                errornum = objSchedule_LogicalGroup.Update
            Else
                errornum = objSchedule_LogicalGroup.AssignSchedule_LogicalGroup()
            End If


        End If
        Dim errornum2 As Integer
        If errornum = 0 Then

            If Not dtpFromdate.SelectedDate > Date.Today Then
                objRecalculateRequest = New RecalculateRequest
                objRecalculateRequest.FK_LogicalGroupId = RadCmbBxLogicalGroup.SelectedValue
                objRecalculateRequest.ImmediatelyStart = True
                objRecalculateRequest.FromDate = dtpFromdate.SelectedDate
                objRecalculateRequest.Remarks = "Assign Schedule - SYSTEM"
                If chckTemporary.Checked = True Then
                    If Not dtpEndDate.SelectedDate Is Nothing Or Not dtpEndDate.SelectedDate > Date.Today Then
                        objRecalculateRequest.ToDate = dtpEndDate.SelectedDate
                    End If
                Else
                    objRecalculateRequest.ToDate = Date.Today
                End If
                objRecalculateRequest.CREATED_BY = SessionVariables.LoginUser.FK_EmployeeId
                errornum2 = objRecalculateRequest.Add()
            End If
        End If
        If errornum = 0 And errornum2 = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            ClearAll()
        ElseIf errornum = -99 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DateExist", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord", CultureInfo), "error")

        End If
        FillGrid()
    End Sub

    Protected Sub dgrdSchedule_LogicalGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdSchedule_LogicalGroup.SelectedIndexChanged
        objSchedule_LogicalGroup = New Schedule_LogicalGroup
        EmpWorkScheduleId = CInt(CType(dgrdSchedule_LogicalGroup.SelectedItems(0), GridDataItem).GetDataKeyValue("EmpWorkScheduleId").ToString())
        GroupId = CInt(CType(dgrdSchedule_LogicalGroup.SelectedItems(0), GridDataItem).GetDataKeyValue("GroupId").ToString())
        ScheduleId = CInt(CType(dgrdSchedule_LogicalGroup.SelectedItems(0), GridDataItem).GetDataKeyValue("ScheduleId").ToString())

        With objSchedule_LogicalGroup
            .EmpWorkScheduleId = EmpWorkScheduleId
            .GetByPK()
            RadCmbBxLogicalGroup.SelectedValue = .FK_LogicalGroupId
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
        End With
        ManageControls(False)
    End Sub

    Protected Sub RadCmbBxScheduletype_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxScheduletype.SelectedIndexChanged
        FillSchedule()
    End Sub

#End Region

#Region "Methods"
    'Private Sub FillCompany()
    '    If SessionVariables.LoginUser IsNot Nothing Then
    '        objSYS_Users_Security = New SYS_Users_Security
    '        objSYS_Users_Security.FK_UserId = SessionVariables.LoginUser.ID
    '        objSYS_Users_Security.GetByPK()
    '        If (objSYS_Users_Security.FK_CompanyId <> 0) Then
    '            FillCompanyForUserSecurity()
    '            Fill_LogicalGroup()
    '        Else
    '            Dim objOrgCompany As New OrgCompany
    '            CtlCommon.FillTelerikDropDownList(RadCmbBxCompany, objOrgCompany.GetAllforddl, Lang)
    '        End If
    '    End If


    'End Sub
    'Private Sub FillCompanyForUserSecurity()
    '    Dim objOrgCompany As New OrgCompany
    '    objOrgCompany.CompanyId = objSYS_Users_Security.FK_CompanyId
    '    Dim CompanyInfo = objOrgCompany.GetByPK()

    '    Dim dtCompanyInfo As New DataTable
    '    Dim dcCompanyValue As New DataColumn
    '    dcCompanyValue.ColumnName = "Value"
    '    dcCompanyValue.DataType = GetType(Integer)

    '    Dim dcCompanyText As New DataColumn
    '    dcCompanyText.ColumnName = "Text"
    '    dcCompanyText.DataType = GetType(String)

    '    dtCompanyInfo.Columns.Add(dcCompanyValue)
    '    dtCompanyInfo.Columns.Add(dcCompanyText)
    '    Dim drCompanyRow = dtCompanyInfo.NewRow()
    '    drCompanyRow("Value") = CompanyInfo.CompanyId
    '    drCompanyRow("Text") = CompanyInfo.CompanyName
    '    dtCompanyInfo.Rows.Add(drCompanyRow)

    '    CtlCommon.FillTelerikDropDownList(RadCmbBxCompany, dtCompanyInfo, Lang)
    '    RadCmbBxCompany.SelectedIndex = 1
    '    RadCmbBxCompany.Enabled = False
    'End Sub
    'Private Sub Fill_LogicalGroup()
    '    If RadCmbBxCompany.SelectedValue <> -1 Or RadCmbBxCompany.SelectedValue <> Nothing Then
    '        Dim objProjectCommon = New ProjectCommon()
    '        objSYS_Users_Security = New SYS_Users_Security
    '        objSYS_Users_Security.FK_UserId = SessionVariables.LoginUser.ID
    '        objSYS_Users_Security.GetByPK()
    '        Dim objEmp_logicalGroup = New Emp_logicalGroup()
    '        objEmp_logicalGroup.FK_CompanyId = RadCmbBxCompany.SelectedValue
    '        Dim dt As DataTable = objEmp_logicalGroup.GetAllByCompany()
    '        CtlCommon.FillComboBox(RadCmbBxLogicalGroup, objEmp_logicalGroup.GetAllByCompany, Lang)
    '    End If
    'End Sub

    Private Sub Fill_LogicalGroup()
        Dim objEmp_logicalGroup = New Emp_logicalGroup()
        Dim dt As DataTable = objEmp_logicalGroup.GetAll()
        CtlCommon.FillComboBox(RadCmbBxLogicalGroup, objEmp_logicalGroup.GetAll, Lang)
        'Try
        '    Dim objEmpLogicalGroup = New Emp_logicalGroup()
        '    Dim dt As New DataTable
        '    objApp_Settings = New APP_Settings
        '    objApp_Settings.GetByPK()
        '    objSYSUsers = New SYSUsers
        '    objSYSUsers.ID = SessionVariables.LoginUser.ID
        '    objSYSUsers.GetUser()
        '    If objSYSUsers.UserStatus = 1 Then ' Full, System User
        '        If FK_CompanyId <> -1 Then
        '            objEmpLogicalGroup.FK_CompanyId = FK_CompanyId
        '            dt = objEmpLogicalGroup.GetAllByCompany()
        '            CtlCommon.FillTelerikDropDownList(RadCmbBxLogicalGroup, dt, Lang)
        '        End If
        '    ElseIf (objSYSUsers.UserStatus = 2 And objApp_Settings.ShowLGWithEntityPrivilege = True) Then
        '        If FK_CompanyId > 0 Then
        '            objEmpLogicalGroup.FK_CompanyId = FK_CompanyId
        '            objEmpLogicalGroup.FK_UserId = objSYSUsers.ID
        '            dt = objEmpLogicalGroup.GetAllByCompanyAndUserId()
        '            CtlCommon.FillTelerikDropDownList(RadCmbBxLogicalGroup, dt, Lang)
        '        End If
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub FillGrid()
        objSchedule_LogicalGroup = New Schedule_LogicalGroup
        dgrdSchedule_LogicalGroup.DataSource = objSchedule_LogicalGroup.Get_LogicalGroup_Schedule_details
        dgrdSchedule_LogicalGroup.DataBind()
    End Sub

    Private Sub ClearAll()
        ' Clear the controls
        RadCmbBxLogicalGroup.SelectedValue = -1
        RadCmbBxSchedules.Text = String.Empty
        RadCmbBxSchedules.Items.Clear()
        RadCmbBxScheduletype.SelectedValue = -1
        chckTemporary.Checked = False
        pnlEndDate.Visible = False
        dtpFromdate.SelectedDate = Today
        dtpEndDate.SelectedDate = Today
        EmpWorkScheduleId = 0
        GroupId = 0
        ScheduleId = 0
        ManageControls(True)
    End Sub

    Private Sub showHide(ByVal status As Boolean, Optional ByVal order As Integer = 0)
        pnlEndDate.Visible = status
        CVDate.Visible = status
    End Sub

    'Protected Sub RadCmbBxCompany_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxCompany.SelectedIndexChanged
    '    Fill_LogicalGroup()
    'End Sub

    Private Sub FillSchedule()
        If SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
            rfvSchedule.InitialValue = "--الرجاء الاختيار--"
        Else
            Lang = CtlCommon.Lang.EN
            rfvSchedule.InitialValue = "--Please Select--"
        End If
        Dim objWorkSchedule As New WorkSchedule()
        If (RadCmbBxScheduletype.SelectedValue = 1) Then
            CtlCommon.FillTelerikDropDownList(RadCmbBxSchedules, objWorkSchedule.GetByType(1), Lang)
        ElseIf (RadCmbBxScheduletype.SelectedValue = 2) Then
            CtlCommon.FillTelerikDropDownList(RadCmbBxSchedules, objWorkSchedule.GetByType(2), Lang)
        ElseIf (RadCmbBxScheduletype.SelectedValue = 3) Then
            CtlCommon.FillTelerikDropDownList(RadCmbBxSchedules, objWorkSchedule.GetByType(3), Lang)
        ElseIf (RadCmbBxScheduletype.SelectedValue = 5) Then
            CtlCommon.FillTelerikDropDownList(RadCmbBxSchedules, objWorkSchedule.GetByType(5), Lang)
        End If
    End Sub

    Private Sub ManageControls(ByVal status As Boolean)
        RadCmbBxLogicalGroup.Enabled = status
        RadCmbBxSchedules.Enabled = status
        RadCmbBxScheduletype.Enabled = status
    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdSchedule_LogicalGroup.Skin))
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
