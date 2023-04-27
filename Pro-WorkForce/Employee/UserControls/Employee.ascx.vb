Imports TA.Employees
Imports TA.Definitions
Imports SmartV.UTILITIES
Imports System.Data
Imports Telerik.Web.UI.DatePickerPopupDirection
Imports Telerik.Web.UI
Imports TA.Lookup
Imports TA.Admin
Imports TA.DailyTasks
Imports System.Web.UI

Partial Class Employee_WebUserControl
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
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Enum DisplayModeEnum
        Add
        Edit
        View
        ViewAll
        ViewAddEdit
    End Enum
    ' Shared variables of main Gridview
    Shared SortDir As String
    Shared SortExep As String
    Shared SortExepression As String
    Shared dtCurrent As DataTable
    ' shared variables of Ta policy grid
    Shared dtTaPolicyCurrent As DataTable
    Private objTAPolicy As TAPolicy
    Private objEmployeeTAPolicy As New Employee_TAPolicy
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
    Public Property DisplayMode() As DisplayModeEnum
        Get
            Return ViewState("DisplayMode")
        End Get
        Set(ByVal value As DisplayModeEnum)
            ViewState("DisplayMode") = value
        End Set
    End Property
#End Region

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack <> True Then
            dtpJoinDate.SelectedDate = DateTime.Today
            dtpBirthDate.SelectedDate = DateTime.Today
            ManageDisplayMode_ExceptionalCases()
            FillGridView()
            FillLists()
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            End If
            If (Page.IsPostBack) Then
                If (dtpBirthDate.SelectedDate.HasValue) Then
                    txtAge.Text = (Math.Round(DateTime.Now.Subtract(dtpBirthDate.SelectedDate).Days / 365)).ToString()
                End If
            End If
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            PageHeader1.HeaderText = ResourceManager.GetString("E,ployees", CultureInfo)
            btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + _
                                    grdVwEmployees.ClientID + "');")
            ManageFunctionalities()
            TabContainer1.ActiveTabIndex = 0
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, _
                                ByVal e As System.EventArgs) Handles btnSave.Click
        Select Case DisplayMode.ToString
            Case "Add"
                saveUpdate()
            Case "Edit"
                updateOnly()
            Case "ViewAddEdit"
                saveUpdate()
            Case Else
        End Select
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, _
                                  ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1

        For Each row As GridViewRow In grdVwEmployees.Rows
            Dim cb As CheckBox = DirectCast(row.FindControl("chkRow"), CheckBox)
            If cb.Checked Then
                ' Get LeaveId from hidden label
                Dim intEmployeeId As Integer = _
                    Convert.ToInt32(DirectCast(row.FindControl("lblEmployeeId"), Label).Text)
                ' Delete current checked item
                objEmployee = New Employee()
                objEmployee.EmployeeId = intEmployeeId
                errNum = objEmployee.Delete()
            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")

            FillGridView()
        Else
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")
        End If
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ClearAll()
        FillGridView()
    End Sub

    Protected Sub lnkEmployeeName_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ' Get the row where link button send the event 
        Dim gv As GridViewRow = (CType(sender, LinkButton).Parent).Parent
        ' Get the PK from the hidden field
        EmployeeId = CType(gv.FindControl("lblEmployeeId"), Label).Text
        FillControlsForEditing()

        btnChangeTaPolicy.Enabled = True

        dtpStartDate.Enabled = True

    End Sub

    Protected Sub TabContainer1_ActiveTabChanged(ByVal sender As Object, _
                                                 ByVal e As System.EventArgs) Handles TabContainer1.ActiveTabChanged
        If TabContainer1.ActiveTabIndex = 1 And EmployeeId = 0 Then
            Dim logicalGroupId As Integer = Val(RadCmbLogicalGroup.SelectedValue)
            Dim worklocationId As Integer = Val(RadCmbWorkLocation.SelectedValue)
            Dim companyId As Integer = Val(RadCmbOrgCompany.SelectedValue)
            Dim entityId As Integer = Val(RadCmbEntity.SelectedValue)
            RadCmbPolicies.SelectedValue = _
                GetPolicyId(logicalGroupId, worklocationId, companyId, entityId)
            If RadCmbPolicies.SelectedValue = -1 Then
                TabContainer1.ActiveTabIndex = 0
            End If
            If dtpJoinDate.SelectedDate IsNot Nothing Then
                dtpStartDate.SelectedDate = dtpJoinDate.SelectedDate
            End If
        End If
    End Sub

    Protected Sub RadCmbOrgCompany_SelectedIndexChanged(ByVal o As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs)
        Entitylist()
    End Sub

    Protected Sub btnChangeTaPolicy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChangeTaPolicy.Click
        ' Update last policy , add new one
        Dim errNo As Integer = -1
        errNo = AddUpdateTAPolicyRecord()

        If errNo = -1 Then
            ' Script errors already shown
        Else
            ClearChangePolicyTab()
            FillGridView(EmployeeId)
        End If

    End Sub

    Protected Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chckTemporary.CheckedChanged
        showHide(chckTemporary.Checked)
    End Sub

#End Region

#Region "Methods"

#Region "GridView related methods"

    Private Sub FillGridView()
        Try
            objEmployee = New Employee()
            dtCurrent = objEmployee.GetAll()
            Dim dv As New DataView(dtCurrent)
            dv.Sort = SortExepression
            grdVwEmployees.DataSource = dv
            grdVwEmployees.DataBind()


        Catch ex As Exception
            ' MsgBox("FillGridView error" & ex.Message)
        End Try
    End Sub

    Private Sub FillGridView(ByVal EmpId As Integer)
        Try
            ' Fill Ta Policy GridView
            objEmployeeTAPolicy = New Employee_TAPolicy()
            objEmployeeTAPolicy.FK_EmployeeId = EmpId
            dtTaPolicyCurrent = objEmployeeTAPolicy.GetByEmployeeId()
            Dim dvTaPolicy As New DataView(dtTaPolicyCurrent)
            dvTaPolicy.Sort = SortExepression
            grdVwTaPolicy.DataSource = dvTaPolicy
            grdVwTaPolicy.DataBind()
        Catch ex As Exception

        End Try

    End Sub

#End Region

    Private Sub FillLists()
        objProjectCommon = New ProjectCommon()
        ' Fill the combo boxes without using the ctlCommon 
        ' standard function
        Dim dt As DataTable = Nothing
        objEmpStatus = New Emp_Status()
        dt = objEmpStatus.GetAll()
        ProjectCommon.FillRadComboBox(RadCmbStatus, dt, "StatusName", "StatusArabicName", "StatusId")
        dt = Nothing
        objOrgCompany = New OrgCompany()
        dt = objOrgCompany.GetAll()
        objProjectCommon.FillMultiLevelRadComboBox(RadCmbOrgCompany, dt, _
                                                   "CompanyId", "CompanyName", _
                                                   "CompanyArabicName", "FK_ParentId")
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
        objEmpWorkLocation = New Emp_WorkLocation()
        dt = objEmpWorkLocation.GetAll()
        ProjectCommon.FillRadComboBox(RadCmbWorkLocation, dt, _
                                      "WorkLocationName", "WorkLocationArabicName", _
                                      "WorkLocationId")
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
    End Sub

    Private Sub Entitylist()
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        If RadCmbOrgCompany.SelectedValue <> -1 Then
            objOrgEntity = New OrgEntity()
            objProjectCommon = New ProjectCommon()

            ' CompanyId is the foreign key .And so expected a datatable 
            '  to be binded to the combo box
            objOrgEntity.FK_CompanyId = RadCmbOrgCompany.SelectedValue
            Dim dt As DataTable = objOrgEntity.GetAll()

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

    Private Sub ManageDisplayMode_ExceptionalCases()
        '  Current function handle the cases where the displayMode 
        '  associated with the in appropriate value for 
        '  the EmployeeId
        Dim _Exist As Integer = 0
        _Exist = IS_Exist()
        If DisplayMode = DisplayModeEnum.View Or _
            DisplayMode = DisplayModeEnum.Edit Then
            If _Exist = -1 Then
                ' If the display mode is View or edit.But, the 
                ' Id is not a valid one
                CtlCommon.ShowMessage( _
                    Me.Page, _
                    ResourceManager.GetString("ErrorEmpProcessing", CultureInfo), "error")
                DisplayMode = DisplayModeEnum.ViewAll
            End If
        ElseIf DisplayMode = DisplayModeEnum.ViewAddEdit Or _
            DisplayModeEnum.ViewAll = DisplayMode Then
            EmployeeId = 0
        ElseIf DisplayMode() = DisplayModeEnum.Add Then
            If EmployeeId > 0 Then
                If _Exist = 0 Then
                    DisplayMode = DisplayModeEnum.Edit
                Else
                    ' EmployeeId>0 and does not matched a data base record
                    EmployeeId = 0
                End If
            Else
                ' if EmployeeId=0 or EmployeeId<0
                EmployeeId = 0
            End If
        End If
    End Sub

    Private Function IS_Exist() As Integer
        ' The View , and Edit modes require to have a valid Leave Id 
        objEmployee = New Employee()
        objEmployee.EmployeeId = EmployeeId
        Dim _EXIT As Integer = 0
        If EmployeeId <= 0 Then
            _EXIT = -1
        ElseIf objEmployee.FindExisting() = False Then
            _EXIT = -1
        End If
        Return _EXIT
    End Function

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

    Private Sub showHide(ByVal status As Boolean)
        pnlEndDate.Visible = status
    End Sub

#Region "Add / Update Employee_TAPolicy record"

    Private Function AddUpdateTAPolicyRecord() As Integer
        ' This function will neither add new policy 
        ' nor updated last updated policy if 
        ' 1 - Try to insert same policy for same employee at same start date 
        ' 2 - Try to insert already activated policy
        ' 2 - Leave the start date empty
        ' 3 - Leave the end date empty for temporary policies

        If chckTemporary.Checked = True And dtpEndDate.SelectedDate Is Nothing Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("Pleaseselectenddate", CultureInfo), "info")
            Return -1
        End If
        Try
            Dim errNum1 As Integer = -1
            Dim errNum2 As Integer = -1
            ' Update the old TAPolicy
            objEmployeeTAPolicy = New Employee_TAPolicy()
            ' Get last started TAPolicy for an Employee 
            objEmployeeTAPolicy.FK_EmployeeId = EmployeeId
            objEmployeeTAPolicy.GetActivePolicyId()


            If objEmployeeTAPolicy.FK_TAPolicyId <> RadCmbPolicies.SelectedValue Then
                ' Set the End date for the suppose to be old policy
                Dim strtDate As DateTime = dtpStartDate.SelectedDate
                objEmployeeTAPolicy.EndDate = strtDate.AddDays(-1)
                ' Update the last started TAPolicy to have the new end date
                errNum1 = objEmployeeTAPolicy.Update()
                ' Add the new TAPolicy
                objEmployeeTAPolicy = New Employee_TAPolicy()
                objEmployeeTAPolicy.FK_EmployeeId = EmployeeId
                objEmployeeTAPolicy.FK_TAPolicyId = RadCmbPolicies.SelectedValue
                objEmployeeTAPolicy.StartDate = dtpStartDate.SelectedDate
                If dtpEndDate.SelectedDate IsNot Nothing Then
                    objEmployeeTAPolicy.EndDate = dtpEndDate.SelectedDate
                Else

                    objEmployeeTAPolicy.EndDate = DateTime.MinValue

                End If
                objEmployeeTAPolicy.LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                objEmployeeTAPolicy.CREATED_BY = SessionVariables.LoginUser.ID
                objEmployeeTAPolicy.CREATED_DATE = Today.Date
                objEmployeeTAPolicy.LAST_UPDATE_DATE = Today.Date
                errNum2 = objEmployeeTAPolicy.Add()
                If errNum1 = 0 And errNum2 = 0 Then
                    ClearChangePolicyTab()
                    Return 0
                Else
                    Return -1
                End If
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("TAActivated", CultureInfo), "info")
                Return -1
            End If


        Catch ex As Exception
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrTAEdit", CultureInfo), "error")
            Return -1
        End Try
    End Function


    Private Sub ClearChangePolicyTab()

        dtpStartDate.SelectedDate = Nothing
        dtpBirthDate.SelectedDate = Nothing

    End Sub

#End Region

#Region "Display Mode Functions"

    Sub ManageFunctionalities()
        Select Case DisplayMode.ToString
            Case "Add"
                Adddisplaymode()
            Case "Edit"
                ViewEditmode()
            Case "View"
                Viewdisplaymode()
            Case "ViewAll"
                ViewAllDisplaymode()
            Case "ViewAddEdit"
                ViewAddEditDisplaymode()
            Case Else
        End Select
    End Sub

    Public Sub LoadData()
        FillGridView()
        ManageFunctionalities()
    End Sub

    Sub ViewAddEditDisplaymode()
        RefreshControls(True)
    End Sub

    Sub Adddisplaymode()
        RefreshControls(True)
        grdVwEmployees.Visible = False
        btnDelete.Visible = False
        btnClear.Visible = False
    End Sub

    Sub Viewdisplaymode()

        RefreshControls(True)
        ManageControls(False)
        btnDelete.Visible = False
        btnClear.Visible = False
        btnSave.Visible = False
        btnClear.Visible = False
        FillControlsForEditing()
        grdVwEmployees.Visible = False

        For Each row As GridViewRow In grdVwEmployees.Rows
            Dim lnb As LinkButton = DirectCast(row.FindControl("lnkEmployeeName"), LinkButton)
            lnb.Visible = True
            Dim cb As CheckBox = DirectCast(row.FindControl("chkRow"), CheckBox)
            cb.Visible = False
        Next
    End Sub

    Sub ViewAllDisplaymode()
        RefreshControls(True)
        ManageControls(False)
        btnDelete.Visible = False
        btnClear.Visible = False
        btnSave.Visible = False
        btnSave.Visible = False
        btnClear.Visible = False
        For Each row As GridViewRow In grdVwEmployees.Rows
            Dim lb As LinkButton = DirectCast(row.FindControl("lnkEmployeeName"), LinkButton)
            Dim cb As CheckBox = DirectCast(row.FindControl("chkRow"), CheckBox)
            cb.Visible = False
            lb.Enabled = True
        Next
    End Sub

    Sub ViewEditmode()
        RefreshControls(True)
        btnSave.Text = "Update"
        FillControlsForEditing()
        dtpStartDate.Enabled = True
        grdVwEmployees.Visible = False
        btnClear.Visible = False
        btnDelete.Visible = False
    End Sub

    Private Sub FillControlsForEditing()
        objEmployee = New Employee()
        objEmployee.EmployeeId = EmployeeId
        objEmployee.GetByPK()
        With objEmployee
            ' set the values to the text boxes
            txtEmployeeNumber.Text = .EmployeeNo
            txtEnglishName.Text = .EmployeeName
            txtArabicName.Text = .EmployeeArabicName
            RadtxtAnnualBalance.Text = .AnnualLeaveBalance
            txtEmailAddress.Text = .Email
            txtRemarks.Text = .Remarks
            ' set the dates to date pickers
            If .DOB <> DateTime.MinValue Then
                dtpBirthDate.SelectedDate = .DOB
                txtAge.Text = Math.Round(DateTime.Now.Subtract(dtpBirthDate.SelectedDate).Days / 365).ToString()
            End If
            If .JoinDate <> DateTime.MinValue Then
                dtpJoinDate.SelectedDate = .JoinDate
            End If
            RadioBtnMale.Checked = IIf(.Gender = "m", True, False)
            RadioBtnFemale.Checked = IIf(.Gender = "m", False, True)
            RadCmbDesignation.SelectedValue = .FK_Designation
            RadCmbOrgCompany.SelectedValue = .FK_CompanyId
            Entitylist()
            RadCmbEntity.SelectedValue = .FK_EntityId
            RadCmbGrade.SelectedValue = .FK_Grade
            RadCmbLogicalGroup.SelectedValue = .FK_LogicalGroup
            RadCmbMaritalStatus.SelectedValue = .FK_MaritalStatus
            RadCmbNationality.SelectedValue = .FK_Nationality
            RadCmbReligion.SelectedValue = .FK_Religion
            RadCmbStatus.SelectedValue = .FK_Status
            RadCmbWorkLocation.SelectedValue = .FK_WorkLocation
            '''''''''''''''''''''''''''''''''''''
            Dim intPolicyId As Integer = _
                GetPolicyId(.FK_LogicalGroup, .FK_WorkLocation, .FK_CompanyId, .FK_EntityId)
            objEmployeeTAPolicy = New Employee_TAPolicy()
            objEmployeeTAPolicy.FK_TAPolicyId = intPolicyId
            objEmployeeTAPolicy.FK_EmployeeId = EmployeeId

        End With
        FillGridView(EmployeeId)
    End Sub

    Sub ManageControls(ByVal Status As Boolean)
        ' set controls to be enabled / disabled
        ' Set text boxes status
        txtEmployeeNumber.Enabled = Status
        txtEnglishName.Enabled = Status
        txtArabicName.Enabled = Status
        RadtxtAnnualBalance.Enabled = Status
        txtEmailAddress.Enabled = Status
        txtRemarks.Enabled = Status
        ' Set the rad calenders status
        dtpBirthDate.Enabled = Status
        txtAge.Enabled = Status
        dtpJoinDate.Enabled = Status
        dtpStartDate.Enabled = Status
        dtpEndDate.Enabled = Status
        ' set the rad combo boxes status
        RadioBtnFemale.Enabled = Status
        RadioBtnMale.Enabled = Status
        RadCmbPolicies.Enabled = Status
        RadCmbOrgCompany.Enabled = Status
        RadCmbDesignation.Enabled = Status
        RadCmbEntity.Enabled = Status
        RadCmbGrade.Enabled = Status
        RadCmbLogicalGroup.Enabled = Status
        RadCmbMaritalStatus.Enabled = Status
        RadCmbNationality.Enabled = Status
        RadCmbReligion.Enabled = Status
        RadCmbStatus.Enabled = Status
        RadCmbWorkLocation.Enabled = Status
        btnChangeTaPolicy.Enabled = Status
        chckTemporary.Enabled = Status
        ' Toggle the status of the check boxes at the GridView column
        For Each row As GridViewRow In grdVwEmployees.Rows
            DirectCast(row.FindControl("chkRow"), CheckBox).Enabled = Status
        Next
    End Sub

    Sub RefreshControls(ByVal status As Boolean)
        txtEmployeeNumber.Enabled = status
        txtEnglishName.Enabled = status
        txtArabicName.Enabled = status
        RadtxtAnnualBalance.Enabled = status
        txtEmailAddress.Enabled = status

        txtRemarks.Enabled = status
        dtpBirthDate.Enabled = status
        txtAge.Enabled = status
        dtpJoinDate.Enabled = status
        RadioBtnFemale.Enabled = status

        RadCmbDesignation.Enabled = status
        RadCmbEntity.Enabled = status
        RadCmbGrade.Enabled = status
        RadCmbLogicalGroup.Enabled = status
        RadCmbMaritalStatus.Enabled = status
        RadCmbNationality.Enabled = status
        RadCmbReligion.Enabled = status
        RadCmbStatus.Enabled = status
        RadCmbWorkLocation.Enabled = status
        RadCmbOrgCompany.Enabled = status
        RadCmbPolicies.Enabled = status

        dtpStartDate.Enabled = status
        dtpEndDate.Enabled = status
        btnChangeTaPolicy.Enabled = status
        chckTemporary.Enabled = status

        For Each row As GridViewRow In grdVwEmployees.Rows
            ' Show the select field of the GridView
            Dim lnb As LinkButton = DirectCast(row.FindControl("lnkEmployeeName"), LinkButton)
            lnb.Visible = True
            ' Hide check boxes at check box column
            Dim cb As CheckBox = DirectCast(row.FindControl("chkRow"), CheckBox)
            cb.Visible = True
        Next
        btnSave.Text = "Save"
        ' Toggle the status of the buttons
        btnDelete.Visible = status
        btnClear.Visible = status
        btnSave.Visible = status
    End Sub

    Function saveUpdate() As Integer

        ' Define variables
        objEmployee = New Employee()
        Dim errorNum1 As Integer = -1
        Dim errNum2 As Integer = -1
        ' Set data into object for Add / Update
        FillObjectsData()
        If EmployeeId = 0 Then
            ' Add new employee
            errorNum1 = objEmployee.Add()
            EmployeeId = objEmployee.EmployeeId
            ' Add related TA Policy
            errNum2 = AddEmployeeTaPolicyRecord()

            If errorNum1 = 0 And errNum2 = 0 Then
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
                ClearTAPolicyTab(grdVwTaPolicy)
            Else
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If
        Else
            objEmployee.EmployeeId = EmployeeId
            errorNum1 = objEmployee.Update()
            If errorNum1 = 0 Then
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("UpdatedSuccessfully", CultureInfo), "success")
            Else
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "success")
            End If



        End If
        If errorNum1 = 0 Then
            Select Case DisplayMode.ToString
                Case "Add"
                    btnSave.Text = "Update"
                Case Else
                    FillGridView()
                    ClearAll()

                    grdVwTaPolicy.DataSource = Nothing
                    grdVwTaPolicy.DataBind()

            End Select
        End If
        Return errorNum1
    End Function

    Private Function AddEmployeeTaPolicyRecord() As Integer
        Dim errNum As Integer = -1
        objEmployeeTAPolicy = New Employee_TAPolicy()
        objEmployeeTAPolicy.FK_EmployeeId = EmployeeId
        ' In Employee add employee join date = start date
        objEmployeeTAPolicy.StartDate = dtpJoinDate.SelectedDate
        objEmployeeTAPolicy.EndDate = DateTime.MinValue
        RadCmbPolicies.SelectedValue = GetPolicyId(RadCmbLogicalGroup.SelectedValue, _
                                                   RadCmbWorkLocation.SelectedValue, RadCmbOrgCompany.SelectedValue, RadCmbEntity.SelectedValue)
        If RadCmbPolicies.SelectedValue <> -2 And RadCmbPolicies.SelectedValue <> -1 Then
            objEmployeeTAPolicy.FK_TAPolicyId = RadCmbPolicies.SelectedValue
        End If
        objEmployeeTAPolicy.LAST_UPDATE_BY = SessionVariables.LoginUser.ID
        objEmployeeTAPolicy.CREATED_BY = SessionVariables.LoginUser.ID
        objEmployeeTAPolicy.LAST_UPDATE_DATE = Today.Date
        objEmployeeTAPolicy.CREATED_DATE = Today.Date
        errNum = objEmployeeTAPolicy.Add()
        Return errNum
    End Function

    Function updateOnly() As Integer
        Dim errNo As Integer
        objEmployee = New Employee()
        Dim errorNum As Integer = -1
        ' Set data into object for Add / Update
        FillObjectsData()
        If EmployeeId > 0 Then
            ' Update a data base record , on update mode
            objEmployee.EmployeeId = EmployeeId
            objEmployee.Update()
        End If
        If errNo = 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("UpdatedSuccessfully", CultureInfo), "success")
            ' Update last policy , add new one
            FillGridView(EmployeeId)
        Else
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")
        End If
        Return errNo
    End Function

    Private Sub FillObjectsData()
        ' After creating a new object , and needs to fill with data 
        ' you can call this function 

        ' This function intended to add code scalability , 
        ' and it is called at SaveUpdate() and UpdateOnly() methods
        With objEmployee
            ' Get the values from the text boxes
            .EmployeeNo = txtEmployeeNumber.Text
            .EmployeeName = txtEnglishName.Text
            .EmployeeArabicName = txtArabicName.Text
            .AnnualLeaveBalance = Val(RadtxtAnnualBalance.Text)
            .Email = txtEmailAddress.Text
            .EntityHierarchy = GetHierarchy(RadCmbEntity.SelectedValue, String.Empty)
            .Remarks = txtRemarks.Text
            ' Get the values from date pickers
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
            .FK_CompanyId = RadCmbOrgCompany.SelectedValue
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
            ' Set default values
            .IsTerminated = False
            .TerminateDate = DateTime.MinValue
            .CREATED_BY = SessionVariables.LoginUser.ID
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            .LAST_UPDATE_DATE = DateTime.Today
            .CREATED_DATE = DateTime.Today
        End With
    End Sub

    Private Sub ClearAll()
        ' Clear text boxes
        txtEmployeeNumber.Text = String.Empty
        txtEnglishName.Text = String.Empty
        txtArabicName.Text = String.Empty
        RadtxtAnnualBalance.Text = String.Empty
        txtEmailAddress.Text = String.Empty
        txtRemarks.Text = String.Empty
        ' Set Data input properties
        Me.dtpBirthDate.SelectedDate = Today
        Me.dtpJoinDate.SelectedDate = Today
        Me.dtpStartDate.SelectedDate = Today
        Me.dtpEndDate.SelectedDate = Today
        RadioBtnFemale.Checked = False
        RadioBtnMale.Checked = True
        ' Reset rad combo boxes
        RadCmbDesignation.SelectedIndex = 0
        RadCmbEntity.SelectedIndex = 0
        RadCmbGrade.SelectedIndex = 0
        RadCmbLogicalGroup.SelectedIndex = 0
        RadCmbMaritalStatus.SelectedIndex = 0
        RadCmbNationality.SelectedIndex = 0
        RadCmbReligion.SelectedIndex = 0
        RadCmbStatus.SelectedIndex = 0
        RadCmbWorkLocation.SelectedIndex = 0
        RadCmbOrgCompany.SelectedIndex = 0
        RadCmbPolicies.SelectedIndex = 0
        ' Reset variables
        EmployeeId = 0
        SortExepression = Nothing
        SortDir = Nothing

        ' Set active tab to be the first one
        TabContainer1.ActiveTabIndex = 0
        ClearTAPolicyTab(grdVwTaPolicy)
    End Sub

    Private Sub ClearTAPolicyTab(ByVal dgrd As GridView)
        dgrd.DataSource = Nothing
        dgrd.DataBind()
        dtpEndDate.SelectedDate = Nothing
        chckTemporary.Checked = False
        pnlEndDate.Visible = False

    End Sub

#End Region

#Region "Sorting and paging methods"

    Protected Sub grdVwEmployees_PageIndexChanging(ByVal sender As Object, _
                                                    ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) _
                                                    Handles grdVwEmployees.PageIndexChanging
        Try
            grdVwEmployees.SelectedIndex = -1
            grdVwEmployees.PageIndex = e.NewPageIndex
            Dim dv As New DataView(dtCurrent)
            dv.Sort = SortExepression
            grdVwEmployees.DataSource = dv
            grdVwEmployees.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetArrowDirection()
        Dim img As System.Web.UI.WebControls.Image = New System.Web.UI.WebControls.Image()
        If Not SortDir = Nothing AndAlso Not SortDir = String.Empty Then
            If SortDir = "ASC" Then
                img.ImageUrl = "~/images/ar-down.png"
            Else
                img.ImageUrl = "~/images/ar-up.png"
            End If
            Select Case SortExep
                Case "EmployeeName"
                    grdVwEmployees.HeaderRow.Cells(1).Controls.Add(New LiteralControl(" "))
                    grdVwEmployees.HeaderRow.Cells(1).Controls.Add(img)
                Case "EmployeeArabicName"
                    grdVwEmployees.HeaderRow.Cells(2).Controls.Add(New LiteralControl(" "))
                    grdVwEmployees.HeaderRow.Cells(2).Controls.Add(img)
                Case "EmployeeNo"
                    grdVwEmployees.HeaderRow.Cells(3).Controls.Add(New LiteralControl(" "))
                    grdVwEmployees.HeaderRow.Cells(3).Controls.Add(img)
            End Select
        End If
    End Sub

    Protected Sub grdVwEmployees_Sorting(ByVal sender As Object, _
                                          ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) _
                                          Handles grdVwEmployees.Sorting
        Try
            If SortDir = "ASC" Then
                SortDir = "DESC"
            Else
                SortDir = "ASC"
            End If
            SortExepression = e.SortExpression & Space(1) & SortDir
            Dim dv As New DataView(dtCurrent)

            dv.Sort = SortExepression
            SortExep = e.SortExpression
            grdVwEmployees.DataSource = dv
            grdVwEmployees.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdVwEmployees_DataBound(ByVal sender As Object, _
                                               ByVal e As System.EventArgs) _
                                               Handles grdVwEmployees.DataBound
        SetArrowDirection()
    End Sub

#End Region

#End Region

#Region "TA Policy GridView : Paging and Sorting"

    Protected Sub grdVwTaPolicy_PageIndexChanging(ByVal sender As Object, _
                                                  ByVal e As WebControls.GridViewPageEventArgs) Handles grdVwTaPolicy.PageIndexChanging
        Try
            grdVwTaPolicy.SelectedIndex = -1
            grdVwTaPolicy.PageIndex = e.NewPageIndex
            Dim dv As New DataView(dtCurrent)
            dv.Sort = SortExepression
            grdVwTaPolicy.DataSource = dv
            grdVwTaPolicy.DataBind()
        Catch ex As Exception

        End Try

    End Sub



    Private Sub SetTAPolicyArrowDirection()
        Dim img As System.Web.UI.WebControls.Image = New System.Web.UI.WebControls.Image()
        If Not SortDir = Nothing AndAlso Not SortDir = String.Empty Then
            If SortDir = "ASC" Then
                img.ImageUrl = "~/images/ar-down.png"
            Else
                img.ImageUrl = "~/images/ar-up.png"
            End If
            Select Case SortExep
                Case "TAPolicyName"
                    grdVwTaPolicy.HeaderRow.Cells(0).Controls.Add(New LiteralControl(" "))
                    grdVwTaPolicy.HeaderRow.Cells(0).Controls.Add(img)
                Case "EndDate"
                    grdVwTaPolicy.HeaderRow.Cells(1).Controls.Add(New LiteralControl(" "))
                    grdVwTaPolicy.HeaderRow.Cells(1).Controls.Add(img)
            End Select
        End If
    End Sub
    Protected Sub grdVwTaPolicy_Sorting(ByVal sender As Object, _
                                          ByVal e As WebControls.GridViewSortEventArgs) Handles grdVwTaPolicy.Sorting
        Try
            If SortDir = "ASC" Then
                SortDir = "DESC"
            Else
                SortDir = "ASC"
            End If
            SortExepression = e.SortExpression & Space(1) & SortDir
            Dim dv As New DataView(dtCurrent)

            dv.Sort = SortExepression
            SortExep = e.SortExpression
            grdVwTaPolicy.DataSource = dv
            grdVwTaPolicy.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub grdVwTaPolicy_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVwTaPolicy.DataBound

        SetTAPolicyArrowDirection()

    End Sub

#End Region

End Class
