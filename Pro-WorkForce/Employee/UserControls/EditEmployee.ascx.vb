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
Imports System.IO
Imports TA.Security

Partial Class EditEmployee_WebUserControl
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
    ' Shared variables of main Gridview
    Shared dtCurrent As DataTable
    ' shared variables of Ta policy grid
    Shared dtTaPolicyCurrent As DataTable
    Private objTAPolicy As TAPolicy
    Private objEmployeeTAPolicy As Employee_TAPolicy
    Private objEmp_OverTimeRule As Emp_OverTimeRule
    Private objEmp_ATTCard As Emp_ATTCard
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Public Lang As String
    Private objEmployee_Type As Employee_Type
    Private objEmployee_Manager As Employee_Manager
    Public MsgLang As String
    Private objSYSUsers As SYSUsers
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

    Public Property ActiveCard() As Boolean
        Get
            Return ViewState("ActiveCard")
        End Get
        Set(ByVal value As Boolean)
            ViewState("ActiveCard") = value
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

    Public Property CardId() As String
        Get
            Return ViewState("CardId")
        End Get
        Set(ByVal value As String)
            ViewState("CardId") = value
        End Set
    End Property

    Public Property Sch_Status() As Boolean
        Get
            Return ViewState("Sch_Status")
        End Get
        Set(ByVal value As Boolean)
            ViewState("Sch_Status") = value
        End Set
    End Property

    Public Property AllowEdit() As Boolean
        Get
            Return ViewState("AllowEdit")
        End Get
        Set(ByVal value As Boolean)
            ViewState("AllowEdit") = value
        End Set
    End Property

    Public Property AllowDelete() As Boolean
        Get
            Return ViewState("AllowDelete")
        End Get
        Set(ByVal value As Boolean)
            ViewState("AllowDelete") = value
        End Set
    End Property

    Public Property TAPolictDT() As DataTable
        Get
            Return ViewState("TAPolicyDT")
        End Get
        Set(ByVal value As DataTable)
            ViewState("TAPolicyDT") = value
        End Set
    End Property

    Public Property OverTimeDT() As DataTable
        Get
            Return ViewState("OverTimeDT")
        End Get
        Set(ByVal value As DataTable)
            ViewState("OverTimeDT") = value
        End Set
    End Property

    Public Property EmpCardDT() As DataTable
        Get
            Return ViewState("EmpCardDT")
        End Get
        Set(ByVal value As DataTable)
            ViewState("EmpCardDT") = value
        End Set
    End Property

    Public Property IsGTReader() As Boolean
        Get
            Return ViewState("IsGTReader")
        End Get
        Set(ByVal value As Boolean)
            ViewState("IsGTReader") = value
        End Set
    End Property

    Public Property EmployeeTypeId() As Integer
        Get
            Return ViewState("EmployeeTypeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeTypeId") = value
        End Set
    End Property

    Public Property TAPolicyId() As Integer
        Get
            Return ViewState("TAPolicyId ")
        End Get
        Set(ByVal value As Integer)
            ViewState("TAPolicyId ") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If SessionVariables.CultureInfo = "en-US" Then
            'Lang = "en"
            Lang = CtlCommon.Lang.EN
            MsgLang = "en"
        ElseIf SessionVariables.CultureInfo = "ar-JO" Then
            'Lang = "ar"
            Lang = CtlCommon.Lang.AR
            MsgLang = "ar"
        End If

        If Page.IsPostBack <> True Then

            FillLists()
            If EmployeeId > 0 Then
                FillControlsForEditing()

            End If
            dtpBirthDate.MinDate = "01/01/1930"
            Me.dtpTerminationDate.SelectedDate = Today
            Me.dtpStartDate.SelectedDate = Today
            Me.dtpEndDate.SelectedDate = Today
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
            '.GetByPK()
            'txtEmployeeCardNo.MaxLength = objAPP_Settings.EmployeeCardLength
            'txtEmployeeNumber.MaxLength = objAPP_Settings.EmployeeNoLength
        End With
        'If Sch_Status = False Then
        '    EmpActiveSchedule.EnableControls(False)
        'Else
        '    TabPanel6.Visible = False
        'End If
        btnDeleteCard.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdEmpCards.ClientID + "')")



        IsGTReader = CBool(CBool(ConfigurationManager.AppSettings("IsGTReader")))
        If IsGTReader Then
            trGTReaders.Visible = True
        Else
            trGTReaders.Visible = False
        End If

    End Sub

    Private Sub FillGTReaderValues()
        Dim objGTReaders As New TA.Lookup.GTReaderValues()
        CtlCommon.FillTelerikDropDownList(radCmbGTReaders, objGTReaders.GetAll(), Lang)
    End Sub

    Protected Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chckTemporary.CheckedChanged
        showHide(chckTemporary.Checked)
    End Sub

    Protected Sub RadCmbEntity_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbEntity.SelectedIndexChanged
        If RadCmbEntity.SelectedIndex > 0 Then
            RadCmbPolicies.SelectedValue = GetPolicyId(0, 0, 0, RadCmbEntity.SelectedValue)
        End If

    End Sub

    Protected Sub RadCmbOrgCompany_SelectedIndexChanged(ByVal o As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs)
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

    Private Function CheckMaxLengthCharacters() As Boolean
        Dim errorDescription As String = ""
        Dim objAPP_Settings = New APP_Settings()
        objAPP_Settings = objAPP_Settings.GetByPK()
        If txtEmployeeNumber.Text.Trim.Length() > objAPP_Settings.EmployeeNoLength Then
            errorDescription += String.Format("Employee Number should not exceed {0} characters.", objAPP_Settings.EmployeeNoLength) + vbNewLine

        End If
        If txtEmployeeCardNo.Text.Trim.Length() > objAPP_Settings.EmployeeCardLength Then
            errorDescription += String.Format("Employee Card No should not exceed {0} characters.", objAPP_Settings.EmployeeCardLength)

        End If
        If (Not String.IsNullOrEmpty(errorDescription)) Then
            CtlCommon.ShowMessage(Page, errorDescription, "info")
            Return False
        End If

        Return True
    End Function


    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ' Fergeeno
        ' Added the Validation for Characters (Employee Number and Employee Card Number)
        If CheckMaxLengthCharacters() Then
            UpdateEmployee()
        End If
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim err As Integer = -1
        objEmployee = New Employee()
        With objEmployee
            .EmployeeId = EmployeeId
            err = .Delete
        End With
        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            ClearAll()
        ElseIf err = -5 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EmpLeaveExist", CultureInfo))
        ElseIf err = -6 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EmpPermissionExist", CultureInfo))
        ElseIf err = -7 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EmpMoveExist", CultureInfo))
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
    End Sub

    Protected Sub btnChangeTaPolicy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChangeTaPolicy.Click
        Dim errNo As Integer = -1
        errNo = AddUpdateTAPolicyRecord()
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        If errNo = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("TAChange", CultureInfo), "success")
        ElseIf errNo = -99 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrTAEdit", CultureInfo), "info")
        End If
    End Sub

    Protected Sub btnChangeOT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChangeOT.Click
        Dim errNo As Integer = -1
        errNo = AddUpdateOTRuleRecord()
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        If errNo = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("OvertimeChange", CultureInfo), "success")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrOvertimeEdit", CultureInfo), "info")
        End If
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If chkIsActive.Checked = True Then
            CheckActiveCard()
        Else
            AddEmployeeExtraCards()
        End If

    End Sub

    Protected Sub btnYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYes.Click
        If ActiveCard = True Then
            AddEmployeeExtraCards()
            FillGridEmp_Cards()
        End If
    End Sub

    Protected Sub btnDeleteCard_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteCard.Click
        Dim err As Integer = -1

        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdEmpCards.Items
            objEmp_ATTCard = New Emp_ATTCard
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim CardId As String = Convert.ToString(row.GetDataKeyValue("CardId").ToString())
                objEmp_ATTCard.FK_EmployeeId = EmployeeId
                objEmp_ATTCard.CardId = CardId
                err = objEmp_ATTCard.Delete()

            End If
        Next
        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            ClearEmp_Cards()
            FillGridEmp_Cards()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")
        End If

    End Sub

    Protected Sub dgrdEmpCards_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdEmpCards.SelectedIndexChanged
        CardId = Convert.ToString(DirectCast(dgrdEmpCards.SelectedItems(0), GridDataItem).GetDataKeyValue("CardId"))
        fillCardControls()
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ClearEmp_Cards()
    End Sub

    Protected Sub grdVwTaPolicy_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles grdVwTaPolicy.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim item As GridDataItem
            item = e.Item
            'If Not item("EndDate").Text = "&nbsp;" Then
            '    If Convert.ToDateTime(item("EndDate").Text).ToString("dd/MM/yyyy") < Convert.ToDateTime(item("StartDate").Text).ToString("dd/MM/yyyy") Then
            '        If Lang = CtlCommon.Lang.AR Then
            '            item("EndDate").Text = Convert.ToString("تم تغييرها")
            '        Else
            '            item("EndDate").Text = Convert.ToString("Changed")
            '        End If
            '    End If
            'End If

        End If

    End Sub

    Protected Sub grdVwTaPolicy_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdVwTaPolicy.NeedDataSource

        grdVwTaPolicy.DataSource = TAPolictDT

    End Sub

    Protected Sub grdVwTaPolicy_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVwTaPolicy.SelectedIndexChanged
        Dim intTAPolicyId As Integer = Convert.ToInt32(DirectCast(grdVwTaPolicy.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_TAPolicyId"))
        TAPolicyId = intTAPolicyId
        Dim intEmployeeId As Integer = Convert.ToInt32(DirectCast(grdVwTaPolicy.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_EmployeeId"))
        Dim dateStartDate As DateTime = Convert.ToDateTime(DirectCast(grdVwTaPolicy.SelectedItems(0), GridDataItem).GetDataKeyValue("StartDate").ToString())
        objEmployeeTAPolicy = New Employee_TAPolicy
        With objEmployeeTAPolicy
            .FK_EmployeeId = intEmployeeId
            .FK_TAPolicyId = intTAPolicyId
            .StartDate = dateStartDate
            .GetByPK()

            RadCmbPolicies.Items.FindItemByValue(.FK_TAPolicyId).Selected = True
            dtpStartDate.SelectedDate = .StartDate
            If Not .EndDate = Nothing Then
                dtpEndDate.SelectedDate = .EndDate
                chckTemporary.Checked = True
                pnlEndDate.Visible = True
            Else
                chckTemporary.Checked = False
                pnlEndDate.Visible = False
            End If

        End With

    End Sub

    Protected Sub grdVwOverTimeRule_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdVwOverTimeRule.NeedDataSource

        grdVwOverTimeRule.DataSource = OverTimeDT

    End Sub

    Protected Sub grdVwOverTimeRule_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVwOverTimeRule.SelectedIndexChanged
        Dim intRuleId As Integer = Convert.ToInt32(DirectCast(grdVwOverTimeRule.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_RuleId"))
        Dim intEmployeeId As Integer = Convert.ToInt32(DirectCast(grdVwOverTimeRule.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_EmployeeId"))
        Dim dateFromDate As DateTime = Convert.ToDateTime(DirectCast(grdVwOverTimeRule.SelectedItems(0), GridDataItem).GetDataKeyValue("FromDate").ToString())
        objEmp_OverTimeRule = New Emp_OverTimeRule
        With objEmp_OverTimeRule
            .FK_EmployeeId = intEmployeeId
            .FK_RuleId = intRuleId
            .FromDate = dateFromDate
            .GetByPK()
            RadCmbOvertime.SelectedValue = .FK_RuleId
            dtpOTStartDate.SelectedDate = .FromDate
            If Not .ToDate = Nothing Then
                chckOvrTemporary.Checked = True
                dtpOTEndDate.SelectedDate = .ToDate
                PnlOTEnddate.Visible = True
            Else
                chckOvrTemporary.Checked = False
                PnlOTEnddate.Visible = False
            End If
        End With

    End Sub

    Protected Sub dgrdEmpCards_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdEmpCards.NeedDataSource

        dgrdEmpCards.DataSource = EmpCardDT

    End Sub

    Protected Sub RadCmbStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbStatus.SelectedIndexChanged
        If RadCmbStatus.SelectedValue = 3 Then
            Termdate.Visible = True
        Else
            Termdate.Visible = False
        End If
    End Sub

    Protected Sub radCmbGTReaders_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radCmbGTReaders.SelectedIndexChanged
        If radCmbGTReaders.SelectedItem.Text = "Card and PIN" Then
            lblPinNo.Visible = True
            txtPinNo.Visible = True
        Else
            lblPinNo.Visible = False
            txtPinNo.Visible = False
        End If
    End Sub

    Protected Sub radEmployeeType_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radEmployeeType.SelectedIndexChanged
        If Not radEmployeeType.SelectedValue = -1 Then
            objEmployee_Type = New Employee_Type
            With objEmployee_Type
                EmployeeTypeId = radEmployeeType.SelectedValue
                .EmployeeTypeId = EmployeeTypeId
                .GetByPK()
                If .IsInternaltype = False Then
                    trContractEndDate.Visible = True
                    rfvContractEndDate.Enabled = True
                    trExternalParty.Visible = True
                    rfvtxtExternalPartyName.Enabled = True
                Else
                    trContractEndDate.Visible = False
                    rfvContractEndDate.Enabled = False
                    trExternalParty.Visible = False
                    rfvtxtExternalPartyName.Enabled = False
                    'txtExternalPartyName.Text = String.Empty
                    'rdpContractEndDate.SelectedDate = Nothing
                End If
            End With
        Else
            trContractEndDate.Visible = False
            rfvContractEndDate.Enabled = False
            trExternalParty.Visible = False
            rfvtxtExternalPartyName.Enabled = False
            'txtExternalPartyName.Text = String.Empty
            'rdpContractEndDate.SelectedDate = Nothing
        End If
    End Sub

#End Region

#Region "Methods"

    Private Sub FillEntity()
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        If RadCmbOrgCompany.SelectedValue <> -1 Then
            objProjectCommon = New ProjectCommon()
            objOrgEntity = New OrgEntity()
            objOrgEntity.FK_CompanyId = RadCmbOrgCompany.SelectedValue
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
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
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

    Sub FillLists()
        objProjectCommon = New ProjectCommon()
        ' Fill the combo boxes without using the ctlCommon 
        ' standard function
        Dim dt As DataTable = Nothing
        objEmpStatus = New Emp_Status()
        dt = objEmpStatus.GetAll()
        'ProjectCommon.FillRadComboBox(RadCmbStatus, dt, "StatusName", "StatusArabicName", "StatusId")
        CtlCommon.FillTelerikDropDownList(RadCmbStatus, dt, Lang)
        dt = Nothing
        objOrgCompany = New OrgCompany()
        dt = objOrgCompany.GetAll()
        'objProjectCommon.FillMultiLevelRadComboBox(RadCmbOrgCompany, dt, _
        '                                           "CompanyId", "CompanyName", _
        '                                           "CompanyArabicName", "FK_ParentId")
        CtlCommon.FillTelerikDropDownList(RadCmbOrgCompany, dt, Lang)

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
        dt = Nothing
        objEmployee_Type = New Employee_Type
        dt = objEmployee_Type.GetAll()
        ProjectCommon.FillRadComboBox(radEmployeeType, dt, _
                                      "TypeName_En", "TypeName_Ar", "EmployeeTypeId")


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

    Function UpdateEmployee() As Integer
        objEmployee = New Employee()
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim errorNum1 As Integer = -1
        ' Add new employee
        errorNum1 = Edit_Employee()
        If errorNum1 = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            'ClearAll()
        ElseIf errorNum1 = -5 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EmpCodeExist", CultureInfo), "info")
        ElseIf errorNum1 = -6 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EmpNameExist", CultureInfo), "info")
        ElseIf errorNum1 = -7 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EmpCardNoExist", CultureInfo), "info")
        ElseIf errorNum1 = -99 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DateExist", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
        Return errorNum1
    End Function

    Public Sub FillControlsForEditing()
        objEmployee = New Employee()
        objEmp_ATTCard = New Emp_ATTCard
        objEmployee.EmployeeId = EmployeeId
        objEmp_ATTCard.FK_EmployeeId = EmployeeId

        EmpLeave_List1.EmployeeID = EmployeeId
        EmpLeave_List1.LoadData()

        EmpPermissions_List.EmployeeID = EmployeeId
        EmpPermissions_List.LoadData()

        EmpActiveSchedule.EmployeeID = EmployeeId
        EmpActiveSchedule.fillCmbSchedule()
        EmpActiveSchedule.fillGridSchedule()

        objEmployee.GetByPK()
        With objEmployee
            txtEmployeeNumber.Text = .EmployeeNo
            txtEmployeeCardNo.Text = .EmployeeCardNo
            txtEnglishName.Text = .EmployeeName
            txtArabicName.Text = .EmployeeArabicName
            txtEmailAddress.Text = .Email
            txtRemarks.Text = .Remarks
            FillGTReaderValues()
            If IsGTReader Then
                radCmbGTReaders.SelectedValue = .GT_VERIFYBY
                If radCmbGTReaders.SelectedItem.Text = "Card and PIN" Then
                    txtPinNo.Text = .Pin
                    lblPinNo.Visible = True
                    txtPinNo.Visible = True
                Else
                    lblPinNo.Visible = False
                    txtPinNo.Visible = False
                End If
            End If

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
            FillEntity()
            FillWorklocation()
            RadCmbEntity.SelectedValue = .FK_EntityId
            RadCmbGrade.SelectedValue = .FK_Grade
            RadCmbLogicalGroup.SelectedValue = .FK_LogicalGroup
            RadCmbMaritalStatus.SelectedValue = .FK_MaritalStatus
            RadCmbNationality.SelectedValue = .FK_Nationality
            RadCmbReligion.SelectedValue = .FK_Religion
            RadCmbStatus.SelectedValue = .FK_Status
            If .FK_Status = 3 Then
                Termdate.Visible = True
                If Not .TerminateDate = Nothing Then
                    dtpTerminationDate.DbSelectedDate = .TerminateDate
                End If
            End If
            RadCmbWorkLocation.SelectedValue = .FK_WorkLocation
            txtNationalId.Text = .NationalId
            txtMobileNo.Text = .MobileNo
            Try
                If .EmpImagePath <> Nothing Then
                    FileFullPath = .EmpImagePath
                    Dim imgURL As String = Convert.ToBase64String(ConvertImageFiletoBytes(.EmpImagePath))
                    Dim imgSrc As String = String.Format("data:image/gif;base64,{0}", imgURL)
                    EmpImg.ImageUrl = imgSrc
                Else
                    If .Gender = "f" Then
                        EmpImg.ImageUrl = "~/images/no_photo_female.jpg"
                    Else
                        EmpImg.ImageUrl = "~/images/nophoto.jpg"
                    End If

                End If
            Catch ex As Exception
                If .Gender = "f" Then
                    EmpImg.ImageUrl = "~/images/no_photo_female.jpg"
                Else
                    EmpImg.ImageUrl = "~/images/nophoto.jpg"
                End If
            End Try
            txtPayRollNumber.Text = .PayRollNumber
            radEmployeeType.SelectedValue = .FK_EmployeeTypeId
            EmployeeTypeId = .FK_EmployeeTypeId
            objEmployee_Type = New Employee_Type
            With objEmployee_Type
                .EmployeeTypeId = EmployeeTypeId
                .GetByPK()
                If .IsInternaltype = False Then
                    trContractEndDate.Visible = True
                    trExternalParty.Visible = True
                Else
                    trContractEndDate.Visible = False
                    trExternalParty.Visible = False
                End If
            End With
            If Not .ContractEndDate = Date.MinValue Or Not .ContractEndDate = Nothing Then
                rdpContractEndDate.SelectedDate = .ContractEndDate
            End If
            txtExternalPartyName.Text = .ExternalPartyName
            txtENROLL_ID.Text = .EnRoll_ID
        End With

        objEmployeeTAPolicy = New Employee_TAPolicy
        With objEmployeeTAPolicy
            .FK_EmployeeId = EmployeeId
            .GetEmployeeLastStartedTAPolicy()
            RadCmbPolicies.SelectedValue = .FK_TAPolicyId
            If (objEmployeeTAPolicy.StartDate <> Date.MinValue) Then
                dtpStartDate.SelectedDate = .StartDate
            End If

            If IsDate(.EndDate) AndAlso CDate(.EndDate) <> DateTime.MinValue Then
                chckTemporary.Checked = True
                showHide(True)
                dtpEndDate.SelectedDate = CDate(.EndDate)
            Else
                chckTemporary.Checked = False
                showHide(False)
            End If
            TAPolictDT = .GetByEmployeeId
            grdVwTaPolicy.DataSource = TAPolictDT
            grdVwTaPolicy.DataBind()
        End With
        objEmp_OverTimeRule = New Emp_OverTimeRule
        With objEmp_OverTimeRule
            .FK_EmployeeId = EmployeeId
            .GetEmployeeLastStartedOTRule()
            RadCmbOvertime.SelectedValue = .FK_RuleId
            If IsDate(.FromDate) AndAlso CDate(.FromDate) <> DateTime.MinValue Then
                dtpOTStartDate.SelectedDate = .FromDate
            End If
            If IsDate(.ToDate) AndAlso CDate(.ToDate) <> DateTime.MinValue Then
                chckOvrTemporary.Checked = True
                showHide(True, 1)
                dtpOTEndDate.SelectedDate = CDate(.ToDate)
            End If
            OverTimeDT = .GetByEmployeeId
            grdVwOverTimeRule.DataSource = OverTimeDT
            grdVwOverTimeRule.DataBind()
        End With
        FillGridEmp_Cards()
    End Sub

    'Private Function EditEmployeeTAPolicyRecord() As Integer
    '    Dim errNum As Integer = -1
    '    objEmployeeTAPolicy = New Employee_TAPolicy()
    '    objEmployeeTAPolicy.FK_EmployeeId = EmployeeId
    '    objEmployeeTAPolicy.StartDate = dtpStartDate.SelectedDate
    '    If chckTemporary.Checked AndAlso dtpEndDate.SelectedDate IsNot Nothing Then
    '        objEmployeeTAPolicy.EndDate = dtpEndDate.SelectedDate
    '    Else
    '        objEmployeeTAPolicy.EndDate = DateTime.MinValue
    '    End If
    '    objEmployeeTAPolicy.FK_TAPolicyId = RadCmbPolicies.SelectedValue
    '    objEmployeeTAPolicy.LAST_UPDATE_BY = 1
    '    objEmployeeTAPolicy.CREATED_BY = 1
    '    objEmployeeTAPolicy.LAST_UPDATE_DATE = Today.Date
    '    objEmployeeTAPolicy.CREATED_DATE = Today.Date
    '    errNum = objEmployeeTAPolicy.Update
    '    Return errNum
    'End Function
    'Private Function EditEmployeeOvertimeRecord() As Integer
    '    Dim errNum As Integer = -1
    '    objEmp_OverTimeRule.FK_EmployeeId = EmployeeId
    '    objEmp_OverTimeRule.FromDate = dtpOTStartDate.SelectedDate
    '    If chckOvrTemporary.Checked AndAlso dtpOTEndDate.SelectedDate IsNot Nothing Then
    '        objEmp_OverTimeRule.ToDate = dtpOTEndDate.SelectedDate
    '    Else
    '        objEmp_OverTimeRule.ToDate = DateTime.MinValue
    '    End If
    '    objEmp_OverTimeRule.FK_RuleId = RadCmbOvertime.SelectedValue
    '    errNum = objEmp_OverTimeRule.Update
    '    Return errNum
    'End Function

    Private Sub ClearAll()
        ' Clear text boxes
        txtEmployeeNumber.Text = String.Empty
        txtEnglishName.Text = String.Empty
        txtArabicName.Text = String.Empty
        'RadtxtAnnualBalance.Text = String.Empty
        txtEmailAddress.Text = String.Empty
        txtRemarks.Text = String.Empty
        ' Set Data input properties
        Me.dtpBirthDate.SelectedDate = Nothing
        Me.dtpJoinDate.SelectedDate = Today
        Me.dtpStartDate.SelectedDate = Today
        Me.dtpEndDate.SelectedDate = Today
        Me.dtpOTEndDate.SelectedDate = Today
        Me.dtpOTStartDate.SelectedDate = Today
        Me.dtpTerminationDate.SelectedDate = Nothing
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
        RadCmbPolicies.SelectedIndex = 0
        RadCmbOvertime.SelectedIndex = 0
        ' Reset variables
        EmployeeId = 0
        ' Set active tab to be the first one
        EmpImg.ImageUrl = "~/images/nophoto.jpg"
        chckTemporary.Checked = False
        chckOvrTemporary.Checked = False
        pnlEndDate.Visible = False
        PnlOTEnddate.Visible = False
        btnChangeOT.Visible = False
        btnChangeTaPolicy.Visible = False
        grdVwOverTimeRule.Visible = False
        grdVwTaPolicy.Visible = False
        Termdate.Visible = False
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

    Private Function Edit_Employee() As Integer
        Dim IntError As Integer = 0
        objEmp_ATTCard = New Emp_ATTCard
        With objEmployee
            .EmployeeId = EmployeeId
            .EmployeeNo = txtEmployeeNumber.Text.Trim()
            .EmployeeCardNo = txtEmployeeCardNo.Text.Trim()
            .EmployeeName = txtEnglishName.Text
            .EmployeeArabicName = txtArabicName.Text
            .AnnualLeaveBalance = 0
            .Email = txtEmailAddress.Text
            .MobileNo = txtMobileNo.Text
            .EntityHierarchy = GetHierarchy(RadCmbEntity.SelectedValue, String.Empty)
            .Remarks = txtRemarks.Text

            If IsGTReader Then
                .GT_VERIFYBY = radCmbGTReaders.SelectedValue
                If radCmbGTReaders.SelectedItem.Text = "Card and PIN" Then
                    .Pin = txtPinNo.Text
                End If
            End If

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

            Dim err As Integer = -1
            With objEmp_ATTCard
                .CardId = txtExtraCardNo.Text
                If Not dtpFromDate.DbSelectedDate = Nothing Then
                    .FromDate = dtpFromDate.SelectedDate
                End If
                If Not dtpToDate.DbSelectedDate = Nothing Then
                    .ToDate = dtpToDate.SelectedDate
                End If
                .Active = chkIsActive.Checked
                err = .Update
                If err = -1 Then
                    err = .Add()
                End If

            End With
            .FK_CompanyId = RadCmbOrgCompany.SelectedValue
            .FK_Designation = RadCmbDesignation.SelectedValue
            .FK_EntityId = RadCmbEntity.SelectedValue
            .FK_Grade = RadCmbGrade.SelectedValue
            .FK_LogicalGroup = RadCmbLogicalGroup.SelectedValue
            .FK_MaritalStatus = RadCmbMaritalStatus.SelectedValue
            .FK_Nationality = RadCmbNationality.SelectedValue
            .NationalId = txtNationalId.Text
            .FK_Religion = RadCmbReligion.SelectedValue
            .FK_Status = RadCmbStatus.SelectedValue
            .FK_WorkLocation = RadCmbWorkLocation.SelectedValue
            .Gender = IIf(RadioBtnFemale.Checked, "f", "m")
            .PayRollNumber = txtPayRollNumber.Text
            .FK_EmployeeTypeId = radEmployeeType.SelectedValue
            .ContractEndDate = rdpContractEndDate.DbSelectedDate
            .ExternalPartyName = txtExternalPartyName.Text

            '-------Assign Manager To Employee--------'
            'objEmployee_Manager = New Employee_Manager
            'objOrgEntity.EntityId = RadCmbEntity.SelectedValue
            'objOrgEntity.GetByPK()
            'If Not objOrgEntity.FK_ManagerId = 0 Then
            '    objEmployee_Manager.FK_ManagerId = objOrgEntity.FK_ManagerId
            '    objEmployee_Manager.FK_EmployeeId = EmployeeId
            '    objEmployee_Manager.FromDate = dtpJoinDate.DbSelectedDate
            '    objEmployee_Manager.ToDate = Nothing
            '    objEmployee_Manager.IsTemporary = False
            '    IntError = objEmployee_Manager.Assign_EmployeesManager()
            'End If
            '-------Assign Manager To Employee--------'

            If .FK_Status = 3 Then
                .TerminateDate = dtpTerminationDate.DbSelectedDate
                .IsTerminated = True
            Else
                .IsTerminated = False
            End If

            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            If Not FileFullPath = "" Then
                .EmpImagePath = FileFullPath
            Else
                .EmpImagePath = ""
            End If
            If IntError = 0 Then
                IntError = .Update
            End If
        End With
        If IntError = 0 Then
            If objEmployee.IsTerminated = True Then
                objSYSUsers = New SYSUsers
                objSYSUsers.Active = False
                objSYSUsers.FK_EmployeeId = objEmployee.EmployeeId
                objSYSUsers.UpdateActive_Users()
            End If
        End If
        Return IntError
    End Function

    Public Sub ManageControls(ByVal status As Boolean)
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        'txtEmployeeNumber.Enabled = status
        txtEnglishName.Enabled = status
        txtArabicName.Enabled = status
        txtEmailAddress.Enabled = status
        txtMobileNo.Enabled = status
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
        txtNationalId.Enabled = status
        RadCmbReligion.Enabled = status
        RadCmbStatus.Enabled = status
        RadCmbWorkLocation.Enabled = status
        RadCmbOrgCompany.Enabled = status
        RadCmbPolicies.Enabled = status
        dtpStartDate.Enabled = status
        dtpEndDate.Enabled = status
        chckTemporary.Enabled = status
        RadCmbOvertime.Enabled = status
        dtpOTStartDate.Enabled = status
        dtpOTEndDate.Enabled = status
        dtpTerminationDate.Enabled = status
        chckOvrTemporary.Enabled = status
        txtEmployeeCardNo.Enabled = status
        btnSave.Text = ResourceManager.GetString("btnUpdate", CultureInfo)
        'btnSave.Text = "Update"
        If AllowDelete AndAlso status = True Then
            btnDelete.Visible = status
        ElseIf status = False Then
            btnDelete.Visible = status
        Else
            btnDelete.Visible = status
        End If

        'btnDelete.Visible = True

        If AllowEdit AndAlso status = True Then
            btnSave.Visible = status
        ElseIf status = False Then
            btnSave.Visible = status
        Else
            btnSave.Visible = status
        End If

        'btnSave.Visible = True

        btnChangeOT.Visible = status
        btnChangeTaPolicy.Visible = status
        FileUpload1.Visible = status
        btnUpload1.Visible = status
        txtExtraCardNo.Enabled = status
        dtpFromDate.Enabled = status
        dtpToDate.Enabled = status
        chkIsActive.Enabled = status
        btnAdd.Visible = status
        btnClear.Visible = status
        btnDeleteCard.Visible = status
        radCmbGTReaders.Enabled = status
        radEmployeeType.Enabled = status
        txtPayRollNumber.Enabled = status
        rdpContractEndDate.Enabled = status
        txtExternalPartyName.Enabled = status
    End Sub

    Private Function AddUpdateTAPolicyRecord() As Integer
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        If chckTemporary.Checked = True And dtpEndDate.SelectedDate Is Nothing Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("Pleaseselectenddate", CultureInfo), "info")
            Return -3
        End If
        Dim errNum1 As Integer = -1
        Try 

            Dim objEmployee_TAPolicy As New Employee_TAPolicy
            objEmployee_TAPolicy.FK_EmployeeId = EmployeeId
            objEmployee_TAPolicy.FK_TAPolicyId = RadCmbPolicies.SelectedValue
            objEmployee_TAPolicy.StartDate = dtpStartDate.SelectedDate
            objEmployee_TAPolicy.EndDate = IIf(dtpEndDate.SelectedDate Is Nothing Or Not chckTemporary.Checked, DateTime.MinValue, dtpEndDate.SelectedDate)
            objEmployee_TAPolicy.CREATED_BY = SessionVariables.LoginUser.ID
            objEmployee_TAPolicy.CREATED_DATE = Date.Now
            objEmployee_TAPolicy.LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            objEmployee_TAPolicy.LAST_UPDATE_DATE = Date.Now

            errNum1 = objEmployee_TAPolicy.AssignTAPolicy()

            'If TAPolicyId = 0 Then
            '    errNum1 = objEmployee_TAPolicy.AssignTAPolicy()
            'Else
            '    objEmployee_TAPolicy.FK_TAPolicyId = TAPolicyId
            '    errNum1 = objEmployee_TAPolicy.Update
            'End If


            If errNum1 = 0 Then
                grdVwTaPolicy.DataSource = objEmployee_TAPolicy.GetByEmployeeId
                grdVwTaPolicy.DataBind()
                Return 0
            ElseIf errNum1 = -99 Then
                Return -99
            Else
                Return -1
            End If
        Catch ex As Exception
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrTAEdit", CultureInfo), "info")
            Return errNum1
        End Try
    End Function

    Private Function AddUpdateOTRuleRecord() As Integer
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        If chckOvrTemporary.Checked = True And dtpOTEndDate.SelectedDate Is Nothing Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("Pleaseselecttodate", CultureInfo), "info")
            Return -3
        End If
        Dim errNum1 As Integer = -1
        Try
            Dim objEmp_OverTimeRule As New Emp_OverTimeRule
            objEmp_OverTimeRule.FK_EmployeeId = EmployeeId
            objEmp_OverTimeRule.FK_RuleId = RadCmbOvertime.SelectedValue
            objEmp_OverTimeRule.FromDate = dtpOTStartDate.SelectedDate
            objEmp_OverTimeRule.ToDate = IIf(dtpOTEndDate.SelectedDate Is Nothing Or Not chckOvrTemporary.Checked, DateTime.MinValue, dtpOTEndDate.SelectedDate)
            objEmp_OverTimeRule.CREATED_BY = SessionVariables.LoginUser.ID
            objEmp_OverTimeRule.CREATED_DATE = Date.Now
            objEmp_OverTimeRule.LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            objEmp_OverTimeRule.LAST_UPDATE_DATE = Date.Now

            errNum1 = objEmp_OverTimeRule.AssignTAPolicy()

            If errNum1 = 0 Then
                grdVwOverTimeRule.DataSource = objEmp_OverTimeRule.GetByEmployeeId
                grdVwOverTimeRule.DataBind()
                Return 0
            ElseIf errNum1 = -99 Then
                Return -99
            Else
                Return -1
            End If
        Catch ex As Exception
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrOvertimeEdit", CultureInfo), "info")
            Return errNum1
        End Try
    End Function

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
                    If (Not System.IO.Directory.Exists("c:\STSupremeFiles\EmpImages\")) Then
                        System.IO.Directory.CreateDirectory("c:\STSupremeFiles\")
                        System.IO.Directory.CreateDirectory("c:\STSupremeFiles\EmpImages\")

                    End If


                    FileFullPath = filepath + fileName + ext
                    FileUpload1.PostedFile.SaveAs(FileFullPath)


                    CtlCommon.ShowMessage(Me.Page, "Uploaded Successfully", "success")
                    Dim imgURL As String = Convert.ToBase64String(ConvertImageFiletoBytes(FileFullPath))
                    Dim imgSrc As String = String.Format("data:/image/gif;base64,{0}", imgURL)
                    EmpImg.ImageUrl = imgSrc


                End If
            Else
                CtlCommon.ShowMessage(Me.Page, "File Extension is not valid. Valid Extensions are " & String.Join(", ", validFileTypes), "info")
            End If
        End If
    End Sub

    Public Function ConvertImageFiletoBytes(ByVal ImageFilePath As String) As Byte()
        Dim _tempByte() As Byte = Nothing
        If ImageFilePath <> Nothing Then
            If String.IsNullOrEmpty(ImageFilePath) = True Then
                Throw New ArgumentNullException("Image File Name Cannot be Null or Empty", "ImageFilePath")
                Return Nothing
            End If
        End If
        Try
            Dim _fileInfo As New IO.FileInfo(ImageFilePath)
            Dim _NumBytes As Long = _fileInfo.Length
            Dim _FStream As New IO.FileStream(ImageFilePath, IO.FileMode.Open, IO.FileAccess.Read)
            Dim _BinaryReader As New IO.BinaryReader(_FStream)
            _tempByte = _BinaryReader.ReadBytes(Convert.ToInt32(_NumBytes))
            _fileInfo = Nothing
            _NumBytes = 0
            _FStream.Close()
            _FStream.Dispose()
            _BinaryReader.Close()
            Return _tempByte
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Sub FillGridEmp_Cards()
        Dim objEmp_ATTCard = New Emp_ATTCard
        objEmp_ATTCard.FK_EmployeeId = EmployeeId
        EmpCardDT = objEmp_ATTCard.GetEmp_ATTCardsDetails
        dgrdEmpCards.DataSource = EmpCardDT
        dgrdEmpCards.DataBind()
    End Sub

    Private Sub fillCardControls()
        objEmp_ATTCard = New Emp_ATTCard
        With objEmp_ATTCard
            .FK_EmployeeId = EmployeeId
            .CardId = CardId
            .GetByPK()
            txtExtraCardNo.Text = .CardId
            dtpFromDate.SelectedDate = .FromDate
            If Not .ToDate = Nothing Then
                dtpToDate.SelectedDate = .ToDate
            End If
            chkIsActive.Checked = .Active
        End With
    End Sub

    Private Function AddEmployeeExtraCards() As Integer
        Dim objEmp_ATTCard = New Emp_ATTCard
        Dim err As Integer = -1
        With objEmp_ATTCard
            .FK_EmployeeId = EmployeeId
            .CardId = txtExtraCardNo.Text
            .Active = chkIsActive.Checked
            .FromDate = dtpFromDate.DbSelectedDate
            .ToDate = dtpToDate.DbSelectedDate
            .CREATED_BY = SessionVariables.LoginUser.ID
            If CardId = 0 Then
                err = .Add()
            Else
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                err = .Update
            End If
            If err = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            ElseIf err = -99 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EmpCardNoExist", CultureInfo), "info")
            ElseIf err = -98 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EmpCardNoAssinged", CultureInfo), "info")
            End If
        End With
        ClearEmp_Cards()
        FillGridEmp_Cards()
    End Function

    Private Sub ClearEmp_Cards()
        txtExtraCardNo.Text = String.Empty
        chkIsActive.Checked = False
        Me.dtpFromDate.SelectedDate = Nothing
        Me.dtpToDate.SelectedDate = Nothing
        CardId = 0
    End Sub

    Protected Sub CheckActiveCard()
        Dim err As Integer
        objEmp_ATTCard = New Emp_ATTCard
        err = objEmp_ATTCard.GetActiveCard()
        If err = -99 Then
            mpeSave.Show()
            ActiveCard = True
        Else
            ActiveCard = False
            AddEmployeeExtraCards()
        End If
    End Sub

#End Region

    'Protected Sub txtEmployeeCardNo_TextChanged(sender As Object, e As EventArgs) Handles txtEmployeeCardNo.TextChanged
    '    Dim objEmployee As New Employee
    '    Dim IsExistCard As Boolean
    '    Dim dt As DataTable

    '    objEmployee = New Employee
    '    With objEmployee
    '        .EmployeeCardNo = txtEmployeeCardNo.Text
    '        dt = .CheckCardNo
    '        If Not dt Is Nothing Then
    '            If dt.Rows.Count > 0 Then
    '                If dt(0)("RESULT").ToString = 0 Then
    '                    IsExistCard = False
    '                    dvCardNoExists.Visible = False
    '                Else
    '                    IsExistCard = True
    '                    dvCardNoExists.Visible = True
    '                End If
    '            End If
    '        End If
    '    End With
    'End Sub
End Class
