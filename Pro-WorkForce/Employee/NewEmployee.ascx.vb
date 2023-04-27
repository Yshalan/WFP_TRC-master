Imports TA.Employees
Imports TA.Definitions
Imports SmartV.UTILITIES
Imports System.Data
Imports Telerik.Web.UI.DatePickerPopupDirection
Imports Telerik.Web.UI
Imports TA.Lookup
Imports TA.DailyTasks
Imports System.Web.UI
Imports TA.Admin
Imports Telerik.Web.UI.Upload
Imports Telerik.Web.UI.UploadedFile
Imports System.IO
Imports System.Collections.Generic

Partial Class NewEmployee_WebUserControl
    Inherits System.Web.UI.UserControl

#Region "Class Variables"

    ' Instances will be used to fill combo boxes
    Private objEmpStatus As Emp_Status
    Private objOrgEntity As OrgEntity
    Private objEmpNationality As Emp_Nationality
    Private objEmpReligion As Emp_Religion
    Private objEmpMaritalStatus As EmpMaritalStatus
    Private objEmpWorkLocation As Emp_WorkLocation
    Private objEmpGrade As Emp_Grade
    Private objEmpDesignation As Emp_Designation
    Private objEmpLogicalGroup As Emp_logicalGroup
    Private objProjectCommon As New ProjectCommon
    Private objEmployee As Employee
    Private objOrgCompany As OrgCompany
    Private objOvertimeRules As OvertimeRules
    Private objEmp_ATTCard As Emp_ATTCard
    ' Shared variables of main Gridview
    Shared dtCurrent As DataTable
    ' shared variables of Ta policy grid
    Shared dtTaPolicyCurrent As DataTable
    Private objTAPolicy As TAPolicy
    Private objEmployeeTAPolicy As New Employee_TAPolicy
    Private objEmp_OverTimeRule As New Emp_OverTimeRule
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Public Event Submit As System.EventHandler
    Private objVersion As SmartV.Version.version

#End Region

#Region "Properties"

    Public Property EmployeeId() As Integer
        Get
            Return ViewState("EmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeId") = value
        End Set
    End Property

    Public Property IsGradeOvertimeRule() As Boolean
        Get
            Return ViewState("IsGradeOvertimeRule")
        End Get
        Set(ByVal value As Boolean)
            ViewState("IsGradeOvertimeRule") = value
        End Set
    End Property

    Public Property CompanyID() As Integer
        Get
            Return ViewState("CompanyID")
        End Get
        Set(ByVal value As Integer)
            ViewState("CompanyID") = value
        End Set
    End Property

    Public Property FileFullPath() As String
        Get
            Return ViewState("FileFullPath")
        End Get
        Set(ByVal value As String)
            ViewState("FileFullPath") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'FadiH:: Wizard validation.

        Dim startNavTemplate As WebControl = Employee.FindControl("StartNavigationTemplateContainerID")
        If Not startNavTemplate Is Nothing Then
            Dim btnStartNext As Button = startNavTemplate.FindControl("StartNextButton")
            If Not btnStartNext Is Nothing Then
                btnStartNext.CausesValidation = True
                btnStartNext.ValidationGroup = "gpNext"
            End If
        End If

        Dim stepNavTemplate As WebControl = Employee.FindControl("StepNavigationTemplateContainerID")
        If Not stepNavTemplate Is Nothing Then
            Dim btnStepNext As Button = stepNavTemplate.FindControl("StepNextButton")
            If Not btnStepNext Is Nothing Then
                btnStepNext.CausesValidation = True
                btnStepNext.ValidationGroup = "gpNext"
            End If
        End If

        'End
        If Page.IsPostBack <> True Then
            Me.dtpBirthDate.MinDate = "01/01/1930"
            'Me.dtpBirthDate.SelectedDate = Today
            Me.dtpJoinDate.SelectedDate = Today
            Me.dtpStartDate.SelectedDate = Today
            Me.dtpEndDate.SelectedDate = Today
            Me.dtpOTEndDate.SelectedDate = Today
            Me.dtpOTStartDate.SelectedDate = Today

            FillLists()
            If CompanyID > 0 Then
                RadCmbOrgCompany.SelectedValue = CompanyID
                FillEntity()
                FillWorklocation()
                RadCmbOrgCompany.Visible = False
                lblOrgCompany.Visible = False
            End If
            'Dim oitem As New PostBackTrigger
            'oitem.ControlID = FileUpload1.ID
            'upNE.Triggers.Add(oitem)
        End If
        If (Page.IsPostBack) Then
            If (dtpBirthDate.SelectedDate.HasValue) Then
                If dtpBirthDate.SelectedDate < Date.Today Then
                    If (dtpJoinDate.SelectedDate.HasValue) Then
                        If dtpBirthDate.SelectedDate < dtpJoinDate.SelectedDate Then
                            txtAge.Text = (Math.Round(DateTime.Now.Subtract(dtpBirthDate.SelectedDate).Days / 365)).ToString()
                        Else
                            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("BirthDateValidation2", CultureInfo), "info")
                            dtpBirthDate.Clear()
                            txtAge.Text = String.Empty
                        End If
                    End If
                Else
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("BirthDateValidation", CultureInfo), "info")
                    dtpBirthDate.Clear()
                    txtAge.Text = String.Empty
                End If
            End If
        End If


        Dim objAPP_Settings As New APP_Settings
        objAPP_Settings = New APP_Settings
        With objAPP_Settings
            .GetByPK()
            txtCardNumber.MaxLength = objAPP_Settings.EmployeeCardLength
            txtEmployeeNumber.MaxLength = objAPP_Settings.EmployeeNoLength
        End With

    End Sub

    Protected Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chckTemporary.CheckedChanged
        showHide(chckTemporary.Checked)
    End Sub

    Protected Sub RadCmbEntity_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbEntity.SelectedIndexChanged
        If RadCmbEntity.SelectedIndex > 0 Then
            RadCmbPolicies.SelectedValue = GetPolicyId(0, 0, 0, RadCmbEntity.SelectedValue)
        End If
    End Sub

    Protected Sub RadCmbOrgCompany_SelectedIndexChanged(ByVal o As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbOrgCompany.SelectedIndexChanged
        If RadCmbOrgCompany.SelectedIndex > 0 Then
            FillEntity()
            FillWorklocation()
            RadCmbPolicies.SelectedValue = GetPolicyId(0, 0, RadCmbOrgCompany.SelectedValue, 0)
        End If
    End Sub

    Protected Sub RadCmbLogicalGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbLogicalGroup.SelectedIndexChanged
        If RadCmbLogicalGroup.SelectedIndex > 0 Then
            RadCmbPolicies.SelectedValue = GetPolicyId(RadCmbLogicalGroup.SelectedValue, 0, 0, 0)
        End If
    End Sub

    Protected Sub RadCmbWorkLocation_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbWorkLocation.SelectedIndexChanged
        If RadCmbWorkLocation.SelectedIndex > 0 Then
            RadCmbPolicies.SelectedValue = GetPolicyId(0, RadCmbWorkLocation.SelectedValue, 0, 0)
        End If
    End Sub

    Protected Sub RadCmbDesignation_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbDesignation.SelectedIndexChanged
        If RadCmbDesignation.SelectedIndex > 0 Then
            If IsGradeOvertimeRule = False Then
                RadCmbOvertime.SelectedValue = GetOvertimeID(0, RadCmbDesignation.SelectedValue)
            End If
        End If
    End Sub

    Protected Sub RadCmbGrade_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbGrade.SelectedIndexChanged
        If RadCmbGrade.SelectedIndex > 0 Then
            If IsGradeOvertimeRule Then
                RadCmbOvertime.SelectedValue = GetOvertimeID(RadCmbGrade.SelectedValue, 0)
            End If
        End If
    End Sub

    Protected Sub chckOvrTemporary_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chckOvrTemporary.CheckedChanged
        showHide(chckOvrTemporary.Checked, 1)
    End Sub

    Protected Sub Employee_FinishButtonClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.WizardNavigationEventArgs) Handles Employee.FinishButtonClick
        Try
            If CreateEmployee() = 0 Then
                RaiseEvent Submit(Me, New EventArgs())

            End If
        Catch ex As Exception

        End Try

    End Sub

    'Protected Sub Employee_NextButtonClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.WizardNavigationEventArgs) Handles Employee.NextButtonClick
    '    If Employee.ActiveStepIndex > 0 Then
    '        If txtEmployeeNumber.Text = "" Or txtEnglishName.Text = "" Or _
    '            RadCmbOrgCompany.SelectedValue = -1 Or _
    '            RadCmbEntity.SelectedValue = "" Or _
    '            RadCmbEntity.SelectedValue = Nothing Or _
    '            RadCmbEntity.SelectedValue = -1 Or _
    '            RadCmbStatus.SelectedValue = -1 Then
    '            CtlCommon.ShowMessage(Me.Page, "")
    '            Employee.ActiveStepIndex = 0
    '        End If
    '    End If

    'End Sub

    Protected Sub Employee_SideBarButtonClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.WizardNavigationEventArgs) Handles Employee.SideBarButtonClick
        If e.NextStepIndex > Employee.ActiveStepIndex + 1 Then
            e.Cancel = True
        End If
    End Sub

    Protected Sub RadCmbStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbStatus.SelectedIndexChanged
        If RadCmbStatus.SelectedValue = 3 Then
            Termdate.Visible = True
        Else
            Termdate.Visible = False
        End If
    End Sub

#End Region

#Region "Methods"

    Private Sub FillEntity()
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        If RadCmbOrgCompany.SelectedValue <> -1 Then
            objProjectCommon = New ProjectCommon()
            objOrgEntity = New OrgEntity()
            If (objVersion.HasMultiCompany() = False) Then
                'objOrgEntity.FK_CompanyId = 1
                objOrgEntity.FK_CompanyId = CompanyID

            Else
                objOrgEntity.FK_CompanyId = RadCmbOrgCompany.SelectedValue
            End If
            Dim dt As DataTable = objOrgEntity.GetAllEntityByCompany()
            If dt.Rows.Count > 0 Then
                objProjectCommon.FillMultiLevelRadComboBox(RadCmbEntity, dt, "EntityId", _
                                                         "EntityName", "EntityArabicName", "FK_ParentId")
            Else
                RadCmbEntity.Items.Clear()
                RadCmbEntity.Text = String.Empty
                RadCmbEntity.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem(ResourceManager.GetString("PleaseSelect", CultureInfo), -1))
            End If
        End If

    End Sub

    Private Sub FillWorklocation()
        If RadCmbOrgCompany.SelectedValue <> -1 Then
            objEmpWorkLocation = New Emp_WorkLocation()
            objEmpWorkLocation.FK_CompanyId = RadCmbOrgCompany.SelectedValue
            Dim dt As DataTable = objEmpWorkLocation.GetAllByCompany()
            If dt.Rows.Count > 0 Then
                ProjectCommon.FillRadComboBox(RadCmbWorkLocation, dt, _
                                      "WorkLocationName", "WorkLocationArabicName", _
                                      "WorkLocationId")
            Else
                RadCmbWorkLocation.Items.Clear()
                RadCmbWorkLocation.Text = String.Empty
                RadCmbWorkLocation.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem(ResourceManager.GetString("PleaseSelect", CultureInfo), -1))
            End If
        End If
    End Sub

    Private Sub FillLists()
        objProjectCommon = New ProjectCommon()
        ' Fill the combo boxes without using the ctlCommon 
        ' standard function
        Dim dt As DataTable = Nothing
        objEmpStatus = New Emp_Status()
        dt = objEmpStatus.GetAll()
        ProjectCommon.FillRadComboBox(RadCmbStatus, dt, "StatusName", "StatusArabicName", "StatusId")
        If (objVersion.HasMultiCompany() = False) Then
            dt = Nothing
            objOrgCompany = New OrgCompany()
            dt = objOrgCompany.GetAll()
            objProjectCommon.FillMultiLevelRadComboBox(RadCmbOrgCompany, dt, _
                                                       "CompanyId", "CompanyName", _
                                                       "CompanyArabicName", "FK_ParentId")
            If RadCmbOrgCompany.Items.Count > 0 Then
                'RadCmbOrgCompany.SelectedValue = 1
                RadCmbOrgCompany.Items(1).Selected = True
                'CompanyID = 1
                CompanyID = RadCmbOrgCompany.SelectedValue
            Else
                RadCmbOrgCompany.SelectedValue = 1
                CompanyID = 1
            End If
        Else
            dt = Nothing
            objOrgCompany = New OrgCompany()
            dt = objOrgCompany.GetAll()
            objProjectCommon.FillMultiLevelRadComboBox(RadCmbOrgCompany, dt, _
                                                       "CompanyId", "CompanyName", _
                                                       "CompanyArabicName", "FK_ParentId")
        End If

        dt = Nothing
        objEmpNationality = New Emp_Nationality()
        dt = objEmpNationality.GetAll()
        ProjectCommon.FillRadComboBox(RadCmbNationality, dt, "NationalityName", _
                                      "NationalityArabicName")
        dt = Nothing
        objEmpReligion = New Emp_Religion()
        dt = objEmpReligion.GetAll()
        ProjectCommon.FillRadComboBox(RadCmbReligion, dt, "ReligionName", _
                                     "ReligionArabicName", "ReligionId")
        dt = Nothing
        objEmpMaritalStatus = New EmpMaritalStatus()
        dt = objEmpMaritalStatus.GetAll()
        ProjectCommon.FillRadComboBox(RadCmbMaritalStatus, dt, "MatitalStatusName", _
                                     "MaritalStatusArabicName", "MaritalStatusId")

        dt = Nothing
        objEmpGrade = New Emp_Grade()
        dt = objEmpGrade.GetAll()
        ProjectCommon.FillRadComboBox(RadCmbGrade, dt, _
                                      "GradeName", "GradeArabicName", "GradeId")
        dt = Nothing
        objEmpDesignation = New Emp_Designation()
        dt = objEmpDesignation.GetAll()
        ProjectCommon.FillRadComboBox(RadCmbDesignation, dt, _
                                      "DesignationName", "DesignationArabicName")
        dt = Nothing
        objEmpLogicalGroup = New Emp_logicalGroup()
        dt = objEmpLogicalGroup.GetAll()
        ProjectCommon.FillRadComboBox(RadCmbLogicalGroup, dt, _
                                      "GroupName", "GroupArabicName", "GroupId")
        ' Fill list of the second tab
        dt = Nothing
        objTAPolicy = New TAPolicy()
        dt = objTAPolicy.GetAll()
        ProjectCommon.FillRadComboBox(RadCmbPolicies, dt, _
                                      "TAPolicyName", "TAPolicyArabicName", "TAPolicyId")
        dt = Nothing
        objOvertimeRules = New OvertimeRules
        dt = objOvertimeRules.GetAll()
        ProjectCommon.FillRadComboBox(RadCmbOvertime, dt, _
                                      "RuleName", "RuleArabicName", "OvertimeRuleId")
        If IsGradeOvertimeRule Then

        Else

        End If

    End Sub

    Private Function GetPolicyId(ByVal LogicalGroupId As Integer, _
                                 ByVal WorkLocationId As Integer, _
                                 ByVal CompanyId As Integer, _
                                 ByVal EntityId As Integer) As Integer
        Dim intTaPolicyId As Integer = -1
        Dim ValueIsSet As Boolean = False
        If LogicalGroupId > 0 Then
            objEmpLogicalGroup = New Emp_logicalGroup()
            objEmpLogicalGroup.GroupId = RadCmbLogicalGroup.SelectedValue
            objEmpLogicalGroup.GetByPK()
            If objEmpLogicalGroup.FK_TAPolicyId <> 0 Then
                intTaPolicyId = objEmpLogicalGroup.FK_TAPolicyId
                ValueIsSet = True
            End If
        End If
        If Not ValueIsSet Then
            If WorkLocationId > 0 Then
                objEmpWorkLocation = New Emp_WorkLocation()
                objEmpWorkLocation.WorkLocationId = RadCmbWorkLocation.SelectedValue
                objEmpWorkLocation.GetByPK()
                If objEmpWorkLocation.FK_TAPolicyId <> 0 Then
                    intTaPolicyId = objEmpWorkLocation.FK_TAPolicyId
                    ValueIsSet = True
                End If
            End If
        End If
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''

        If Not ValueIsSet Then
            If EntityId > 0 Then
                objOrgEntity = New OrgEntity()
                objOrgEntity.EntityId = RadCmbEntity.SelectedValue
                objOrgEntity.GetByPK()
                If objOrgEntity.FK_DefaultPolicyId <> 0 Then
                    intTaPolicyId = objOrgEntity.FK_DefaultPolicyId
                End If
            End If
        End If


        If Not ValueIsSet Then
            If CompanyId > 0 Then
                objOrgCompany = New OrgCompany()
                objOrgCompany.CompanyId = RadCmbOrgCompany.SelectedValue
                objOrgCompany.GetByPK()
                If objOrgCompany.FK_DefaultPolicyId <> 0 Then
                    intTaPolicyId = objOrgCompany.FK_DefaultPolicyId
                End If
            End If
        End If


        Return intTaPolicyId
    End Function

    Private Function GetOvertimeID(ByVal GradeId As Integer, ByVal DesignationId As Integer) As Integer
        Dim intOvertimeId As Integer = -1
        Dim ValueIsSet As Boolean = False
        If GradeId > 0 Then
            objEmpGrade = New Emp_Grade
            objEmpGrade.GradeId = GradeId
            objEmpGrade.GetByPK()
            If objEmpGrade.FK_OvertimeRuleId <> 0 Then
                intOvertimeId = objEmpGrade.FK_OvertimeRuleId
                ValueIsSet = True
            End If
        End If
        If Not ValueIsSet Then
            If DesignationId > 0 Then
                objEmpDesignation = New Emp_Designation
                objEmpDesignation.DesignationId = DesignationId
                objEmpDesignation.GetByPK()
                If objEmpDesignation.FK_OvertimeRuleId <> 0 Then
                    intOvertimeId = objEmpDesignation.FK_OvertimeRuleId
                    ValueIsSet = True
                End If
            End If
        End If
        Return intOvertimeId
    End Function

    Function CreateEmployee() As Integer
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        objEmployee = New Employee()
        Dim errorNum1 As Integer = -1
        Dim errNum2 As Integer = -1
        Dim errNum3 As Integer = -1
        ' Add new employee
        errorNum1 = Add_Employee()
        If errorNum1 = 0 Then
            EmployeeId = objEmployee.EmployeeId
            errNum2 = AddEmployeeTAPolicyRecord()
            errNum3 = AddEmployeeOvertimeRecord()
            UploadImg()
            objEmployee.EmployeeId = EmployeeId
            objEmployee.EmpImagePath = FileFullPath
            objEmployee.EmpImagePath_Insert()

            If errorNum1 = 0 And errNum2 = 0 And errNum3 = 0 Then
                'showResult(ProjectCommon.CodeResultMessage.CodeSaveSucess)
                Dim strMsg = "Saved Successfully"
                Dim strURL = "Employees.aspx"
                ScriptManager.RegisterStartupScript(Me.Page, Page.GetType(), "", "ChangePage('" & strURL & "','" & strMsg & "')", True)
                ClearAll()
                Return errorNum1
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If
        ElseIf errorNum1 = -5 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EmpCodeExist", CultureInfo), "info")
        ElseIf errorNum1 = -6 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EmpNameExist", CultureInfo), "info")
        ElseIf errorNum1 = -7 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EmpCardNoExist", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
        Return errorNum1
    End Function

    Private Function AddEmployeeTAPolicyRecord() As Integer
        Dim errNum As Integer = -1
        objEmployeeTAPolicy = New Employee_TAPolicy()
        objEmployeeTAPolicy.FK_EmployeeId = EmployeeId
        objEmployeeTAPolicy.StartDate = dtpStartDate.SelectedDate
        If chckTemporary.Checked AndAlso dtpEndDate.SelectedDate IsNot Nothing Then
            objEmployeeTAPolicy.EndDate = dtpEndDate.SelectedDate
        Else
            objEmployeeTAPolicy.EndDate = DateTime.MinValue
        End If
        objEmployeeTAPolicy.FK_TAPolicyId = RadCmbPolicies.SelectedValue
        objEmployeeTAPolicy.LAST_UPDATE_BY = SessionVariables.LoginUser.ID
        objEmployeeTAPolicy.CREATED_BY = SessionVariables.LoginUser.ID
        objEmployeeTAPolicy.LAST_UPDATE_DATE = Today.Date
        objEmployeeTAPolicy.CREATED_DATE = Today.Date
        errNum = objEmployeeTAPolicy.Add()
        Return errNum
    End Function

    Private Function AddEmployeeOvertimeRecord() As Integer
        Dim errNum As Integer = -1
        objEmp_OverTimeRule.FK_EmployeeId = EmployeeId
        objEmp_OverTimeRule.FromDate = dtpOTStartDate.SelectedDate
        If chckOvrTemporary.Checked AndAlso dtpOTEndDate.SelectedDate IsNot Nothing Then
            objEmp_OverTimeRule.ToDate = dtpOTEndDate.SelectedDate
        Else
            objEmp_OverTimeRule.ToDate = DateTime.MinValue
        End If
        objEmp_OverTimeRule.FK_RuleId = RadCmbOvertime.SelectedValue
        objEmp_OverTimeRule.CREATED_DATE = Date.Now
        objEmp_OverTimeRule.LAST_UPDATE_DATE = Date.Now
        errNum = objEmp_OverTimeRule.Add()
        Return errNum
    End Function

    Public Sub ClearAll()
        ' Clear text boxes
        txtEmployeeNumber.Text = String.Empty
        txtEnglishName.Text = String.Empty
        txtArabicName.Text = String.Empty
        txtCardNumber.Text = String.Empty
        'RadtxtAnnualBalance.Text = String.Empty
        txtEmailAddress.Text = String.Empty
        txtRemarks.Text = String.Empty
        txtAge.Text = String.Empty
        txtNationalId.Text = String.Empty
        txtMobileNo.Text = String.Empty
        ' Set Data input properties
        'Me.dtpBirthDate.SelectedDate = Today
        Me.dtpJoinDate.SelectedDate = Today
        Me.dtpStartDate.SelectedDate = Today
        Me.dtpEndDate.SelectedDate = Today
        Me.dtpOTEndDate.SelectedDate = Today
        Me.dtpOTStartDate.SelectedDate = Today
        Me.dtpBirthDate.SelectedDate = Nothing
        Me.dtpTerminationDate.SelectedDate = Nothing
        RadioBtnFemale.Checked = False
        RadioBtnMale.Checked = True
        ' Reset rad combo boxes
        RadCmbOrgCompany.SelectedIndex = 0
        RadCmbDesignation.SelectedIndex = 0
        RadCmbEntity.SelectedIndex = 0
        RadCmbGrade.SelectedIndex = 0
        RadCmbLogicalGroup.SelectedIndex = 0
        RadCmbMaritalStatus.SelectedIndex = 0
        RadCmbNationality.SelectedIndex = 0
        RadCmbReligion.SelectedIndex = 0
        RadCmbStatus.SelectedIndex = 0
        RadCmbWorkLocation.SelectedIndex = 0
        RadCmbPolicies.SelectedIndex = 0
        ' Reset variables
        EmployeeId = 0
        ' Set active tab to be the first one
        chckTemporary.Checked = False
        chckOvrTemporary.Checked = False
        Termdate.Visible = False
        pnlEndDate.Visible = False
        PnlOTEnddate.Visible = False
    End Sub

    Private Sub showHide(ByVal status As Boolean, Optional ByVal order As Integer = 0)
        If order = 0 Then
            pnlEndDate.Visible = status
        Else
            PnlOTEnddate.Visible = status
        End If

    End Sub

    Private Function GetHierarchy(ByVal EntityId As String, ByVal result As String) As String
        objOrgEntity = New OrgEntity()
        objOrgEntity.EntityId = EntityId
        objOrgEntity.GetByPK()
        If result <> String.Empty Then
            result = result & "," & EntityId
        Else
            result = EntityId
        End If
        If objOrgEntity.FK_ParentId = 0 Then

            Return result
        Else

            Return GetHierarchy(objOrgEntity.FK_ParentId, result)
        End If
    End Function

    Private Function Add_Employee() As Integer
        Dim IntError As Integer = -1
        With objEmployee
            .EmployeeNo = txtEmployeeNumber.Text.Trim()
            .EmployeeCardNo = txtCardNumber.Text.Trim()
            .EmployeeName = txtEnglishName.Text
            .EmployeeArabicName = txtArabicName.Text
            .AnnualLeaveBalance = 0
            .Email = txtEmailAddress.Text
            .EntityHierarchy = GetHierarchy(RadCmbEntity.SelectedValue, String.Empty)
            .Remarks = txtRemarks.Text
            If dtpBirthDate.SelectedDate IsNot Nothing Then
                .DOB = dtpBirthDate.SelectedDate
            Else
                .DOB = DateTime.MinValue
            End If
            If dtpJoinDate.SelectedDate IsNot Nothing Then
                .JoinDate = dtpJoinDate.SelectedDate
            Else
                .JoinDate = DateTime.MinValue
            End If
            If (objVersion.HasMultiCompany() = False) Then
                .FK_CompanyId = objVersion.GetCompanyId()
            Else
                .FK_CompanyId = RadCmbOrgCompany.SelectedValue
            End If

            .FK_Designation = RadCmbDesignation.SelectedValue
            .FK_EntityId = RadCmbEntity.SelectedValue
            .FK_Grade = RadCmbGrade.SelectedValue
            .FK_LogicalGroup = RadCmbLogicalGroup.SelectedValue
            .FK_MaritalStatus = RadCmbMaritalStatus.SelectedValue
            .FK_Nationality = RadCmbNationality.SelectedValue
            .FK_Religion = RadCmbReligion.SelectedValue
            .FK_Status = RadCmbStatus.SelectedValue
            .FK_WorkLocation = RadCmbWorkLocation.SelectedValue
            .Gender = IIf(RadioBtnFemale.Checked, "f", "m")
            .IsTerminated = False
            .TerminateDate = dtpTerminationDate.DbSelectedDate
            .EmpImagePath = ""
            .NationalId = txtNationalId.Text
            .MobileNo = txtMobileNo.Text
            .CREATED_BY = SessionVariables.LoginUser.ID
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            .LAST_UPDATE_DATE = DateTime.Today
            .CREATED_DATE = DateTime.Today
            IntError = .Add()
            EmployeeId = .EmployeeId
            '----------------------'
            objEmp_ATTCard = New Emp_ATTCard
            objEmp_ATTCard.CardId = txtCardNumber.Text
            objEmp_ATTCard.FK_EmployeeId = EmployeeId
            Me.objEmp_ATTCard.FromDate = Today
            objEmp_ATTCard.Active = True
            objEmp_ATTCard.CREATED_BY = SessionVariables.LoginUser.ID
            objEmp_ATTCard.Add()
            '----------------------'
        End With
        Return IntError
    End Function

    Public Sub EnableValidation(ByVal status As Boolean)
        reqEmpEntity.Enabled = status
        reqEmployeeNumber.Enabled = status
        reqEmpStatus.Enabled = status
        reqEnglishName.Enabled = status
        reqOrgCompany.Enabled = status
        RequiredFieldValidator1.Enabled = status
        RequiredFieldValidator2.Enabled = status
        RequiredFieldValidator3.Enabled = status
        RequiredFieldValidator4.Enabled = status
        Comparevalidator1.Enabled = status
        Comparevalidator2.Enabled = status

    End Sub

    Protected Sub UploadImg()
        If FileUpload1.HasFile Then
            Dim fileName As String = EmployeeId
            Dim filepath As String = ConfigurationManager.AppSettings("EmpImages").ToString
            Dim validFileTypes As String() = {"bmp", "gif", "png", "jpg", "jpeg"}
            Dim ext As String = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName)
            Dim isValidFile As Boolean = False
            For i As Integer = 0 To validFileTypes.Length - 1
                If ext = "." & validFileTypes(i) Then
                    isValidFile = True
                    Exit For
                End If
            Next
            If isValidFile Then
                If EmployeeId <> 0 Then
                    FileFullPath = filepath + fileName + ext
                    FileUpload1.PostedFile.SaveAs(FileFullPath)
                    'CtlCommon.ShowMessage(Me.Page, "Uploaded Successfully")
                Else

                End If
            Else
                CtlCommon.ShowMessage(Me.Page, (ResourceManager.GetString("ErrorImageExtension ", CultureInfo)) & String.Join(", ", validFileTypes), "info")
            End If
        End If
    End Sub

#End Region

End Class
