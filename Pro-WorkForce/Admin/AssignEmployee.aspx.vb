Imports TA.Employees
Imports System.Data
Imports SmartV.UTILITIES
Imports TA.Lookup
Imports TA.Admin
Imports Telerik.Web.UI
Imports SmartV.Security
Imports SmartV.Version
Imports TA.Security
Imports TA.Definitions
Imports System.Data.SqlClient
Imports TA_AssignEmployee
Imports System
Imports System.Net.Mail
Imports System.Configuration

Partial Class Admin_AssignEmployee
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Delegate Sub EmployeeSelectedChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs)
    Public Event eventEmployeeSelect As EmployeeSelectedChanged
    Private objEmployee As Employee
    Private objCompany As OrgCompany
    Private objEntity As OrgEntity
    Dim objSYSUsers As SYSUsers
    Private objVersion As SmartV.Version.version
    Dim objUserPrivileg_Entities As UserPrivileg_Entities
    Dim objUserPrivileg_Companies As UserPrivileg_Companies
    Dim objApp_Settings As APP_Settings
    Dim objAssignEmp As AssignEmployee

    Public Fk_EmployeeId As Integer
    Public LeaveId As Integer

#End Region

#Region "Properties"
#Region "Property :: EmpId"
    Public Property EmployeeId() As Integer
        Get
            Return ViewState("EmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeId") = value
        End Set
    End Property
    Public Property EmployeeIdtxt() As Integer
        Get
            Return ViewState("EmployeeIdtxt")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeIdtxt") = value
        End Set
    End Property

    Public Property EmployeeName() As String
        Get
            Return ViewState("EmployeeName")
        End Get
        Set(ByVal value As String)
            ViewState("EmployeeName") = value
        End Set
    End Property
    Public Property LeaveEmployeeName() As String
        Get
            Return ViewState("LeaveEmployeeName")
        End Get
        Set(ByVal value As String)
            ViewState("LeaveEmployeeName") = value
        End Set
    End Property
    Public Property LeaveFrom() As String
        Get
            Return ViewState("LeaveFrom")
        End Get
        Set(ByVal value As String)
            ViewState("LeaveFrom") = value
        End Set
    End Property
    Public Property LeaveTo() As String
        Get
            Return ViewState("LeaveTo")
        End Get
        Set(ByVal value As String)
            ViewState("LeaveTo") = value
        End Set
    End Property
#End Region
#Region "Property :: Hdn EmpId"
    Public Property hEmployeeId() As Integer
        Get
            'Return hdnEmployeeId.Value
            If Not (String.IsNullOrEmpty(hdnEmployeeId.Value)) Then
                Return Convert.ToInt32(hdnEmployeeId.Value)
            Else
                Return 0

            End If
        End Get
        Set(ByVal value As Integer)
            hdnEmployeeId.Value = value
        End Set
    End Property
#End Region
#Region "Property :: EntityId"
    Public Property EntityId() As Integer
        Get
            Return ViewState("EntityId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EntityId") = value
        End Set
    End Property
#End Region
#Region "Property :: CompanyId"
    Public Property CompanyId() As Integer
        Get
            Return ViewState("CompanyId")
        End Get
        Set(ByVal value As Integer)
            ViewState("CompanyId") = value
        End Set
    End Property
#End Region
#Region "Property :: IsEmployeeRequired"
    Public Property IsEmployeeRequired() As Boolean
        Get
            Return ViewState("IsEmployeeRequired")
        End Get
        Set(ByVal value As Boolean)
            ViewState("IsEmployeeRequired") = value
        End Set
    End Property
#End Region
#Region "Property :: IsLevelRequired"
    Public Property IsLevelRequired() As Boolean
        Get
            Return ViewState("IsLevelRequired")
        End Get
        Set(ByVal value As Boolean)
            ViewState("IsLevelRequired") = value
        End Set
    End Property
#End Region
#Region "Property :: EmpNo"
    Public Property EmpNo() As String
        Get
            Return TxtEmpNo.Text
        End Get
        Set(ByVal value As String)
            TxtEmpNo.Text = value
        End Set
    End Property
#End Region
#Region "Property :: EmpDesignation"
    Public Property EmpDesignation() As Integer
        Get
            Return ViewState("EmpDesignation")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmpDesignation") = value
        End Set
    End Property
#End Region
#Region "Property :: EmpEmail"
    Public Property EmpEmail() As String
        Get
            Return ViewState("EmpEmail")
        End Get
        Set(ByVal value As String)
            ViewState("EmpEmail") = value
        End Set
    End Property
#End Region
#Region "Property :: ddlEmployeeEnabled"
    Public Property ddlEmployeeEnabled() As Boolean
        Get
            Return RadCmbBxEmployees.Enabled
        End Get
        Set(ByVal value As Boolean)
            RadCmbBxEmployees.Enabled = value
        End Set
    End Property
#End Region
#Region "Property :: EmployeeValidationGroup"
    Public Property EmployeeValidationGroup() As String
        Get
            Return RadCmbBxEmployees.ValidationGroup
        End Get
        Set(ByVal value As String)
            RadCmbBxEmployees.ValidationGroup = value
        End Set
    End Property
#End Region
#Region "Property :: EmployeeRequiredValidationGroup"
    Public Property EmployeeRequiredValidationGroup() As String
        Get
            Return rfvEmployee.ValidationGroup
        End Get
        Set(ByVal value As String)
            rfvEmployee.ValidationGroup = value
        End Set
    End Property

    Public Property EntityRequiredValidationGroup() As String
        Get
            Return rfvEntity.ValidationGroup
        End Get
        Set(ByVal value As String)
            rfvEntity.ValidationGroup = value
        End Set
    End Property

    Public Property CompanyRequiredValidationGroup() As String
        Get
            Return rfvCompanies.ValidationGroup
        End Get
        Set(ByVal value As String)
            rfvCompanies.ValidationGroup = value
        End Set
    End Property
#End Region
#Region "Property :: ddlEmployeeEnabled"
    Public Property SelectedEmployee() As String
        Get
            Return RadCmbBxEmployees.SelectedValue
        End Get
        Set(ByVal value As String)
            RadCmbBxEmployees.SelectedValue = value
            'Add Space before and after the Dash (-) for search Emp No that contains (-)
            TxtEmpNo.Text = RadCmbBxEmployees.Text.Split(" - ")(0)
        End Set
    End Property
#End Region
#Region "Property :: ShowOnlyManagers"
    Public Property ShowOnlyManagers() As Boolean
        Get
            Return ViewState("ShowOnlyManagers")
        End Get
        Set(ByVal value As Boolean)
            ViewState("ShowOnlyManagers") = value
        End Set
    End Property
#End Region
#Region "Property :: ShowRadioSearch"
    Public Property ShowRadioSearch() As Boolean
        Get
            Return ViewState("ShowRadioSearch")
        End Get
        Set(ByVal value As Boolean)
            ViewState("ShowRadioSearch") = value
        End Set
    End Property
#End Region
#Region "Property :: ShowOnlyActiveEmp"
    Public Property ShowOnlyActiveEmp() As Boolean
        Get
            Return ViewState("ShowOnlyActiveEmp")
        End Get
        Set(ByVal value As Boolean)
            ViewState("ShowOnlyActiveEmp") = value
        End Set
    End Property
#End Region
#Region "Property :: FilterType"
    Public Property FilterType() As String
        Get
            Return ViewState("FilterType")
        End Get
        Set(ByVal value As String)
            ViewState("FilterType") = value
        End Set
    End Property
#End Region
#Region "Property :: ValidationGroup"
    Public Property ValidationGroup() As String
        Get
            Return ViewState("ValidationGroup")
        End Get
        Set(ByVal value As String)
            ViewState("ValidationGroup") = value
        End Set
    End Property
#End Region
#Region "Property :: LogicalGroup"
    Public Property LogicalGroupId() As Integer
        Get
            Return ViewState("LogicalGroupId")
        End Get
        Set(ByVal value As Integer)
            ViewState("LogicalGroupId") = value
        End Set
    End Property
#End Region
#Region "Property :: ForceCompanyFilter"
    Public Property ForceCompanyFilter() As Boolean
        Get
            Return ViewState("ForceCompanyFilter")
        End Get
        Set(ByVal value As Boolean)
            ViewState("ForceCompanyFilter") = value
        End Set
    End Property
#End Region
#Region "Property :: ShowDirectStaffCheck"
    Public Property ShowDirectStaffCheck() As Boolean
        Get
            Return ViewState("ShowDirectStaffCheck")
        End Get
        Set(ByVal value As Boolean)
            ViewState("ShowDirectStaffCheck") = value
        End Set
    End Property
#End Region
    Public Property IsEntityClick() As String
        Get
            Return hdnIsEntityClick.Value
        End Get
        Set(ByVal value As String)
            hdnIsEntityClick.Value = value
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

    Public Property DirectStaffOnly() As Boolean
        Get
            Return ViewState("DirectStaffOnly")
        End Get
        Set(ByVal value As Boolean)
            ViewState("DirectStaffOnly") = value
        End Set
    End Property

    Public Property SourceSearch_Page() As String
        Get
            Return ViewState("SourceSearch_Page")
        End Get
        Set(ByVal value As String)
            ViewState("SourceSearch_Page") = value
        End Set
    End Property

#Region "Property :: LogicalGroup"
    Public Property WorkLocationId() As Integer
        Get
            Return ViewState("WorkLocationId")
        End Get
        Set(ByVal value As Integer)
            ViewState("WorkLocationId") = value
        End Set
    End Property
#End Region

#End Region

#Region "Events"

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        Else
            If SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                'Page.MasterPageFile = "~/default/ArabicMaster.master"
            Else
                Lang = CtlCommon.Lang.EN
                'Page.MasterPageFile = "~/default/NewMaster.master"
            End If

            Page.UICulture = SessionVariables.CultureInfo
        End If
        
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            rfvCompanies.Enabled = True

            If Not IsPostBack Then
                If (SessionVariables.CultureInfo Is Nothing) Then
                    Response.Redirect("~/default/Login.aspx")
                ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                    Lang = CtlCommon.Lang.AR
                    lblCompany.Text = "شركة"
                    lblLevels.Text = "شخصية"
                    lblEmployees.Text = "الموظف"
                    lblEmpNo.Text = "رقم الموظف"
                    btnSearch.Text = "يسترد"
                    btnAssignEmployee.Text = "تعيين"
                Else
                    Lang = CtlCommon.Lang.EN
                    lblCompany.Text = "Company"
                    lblLevels.Text = "Entity"
                    lblEmployees.Text = "Employees"
                    lblEmpNo.Text = "Employee No."
                    btnSearch.Text = "Retrive"
                    btnAssignEmployee.Text = "Assign"
                End If
                objApp_Settings = New APP_Settings
                With objApp_Settings
                    .GetByPK()
                    If .ShowEmployeeList = False Then
                        lblEmployees.Visible = False
                        RadCmbBxEmployees.Visible = False
                        EmployeestxtBox.Visible = True
                        txtEmployee.Enabled = False
                        rfvtxtEmpNo.Enabled = True
                        rfvtxtEmpNo.Enabled = IsEmployeeRequired
                        rfvtxtEmpNo.ValidationGroup = ValidationGroup
                    Else
                        lblEmployees.Visible = True
                        RadCmbBxEmployees.Visible = True
                        EmployeestxtBox.Visible = False
                        rfvEmployee.InitialValue = ResourceManager.GetString("PleaseSelect", CultureInfo)
                        rfvEmployee.Enabled = IsEmployeeRequired
                        rfvEmployee.ValidationGroup = ValidationGroup
                    End If
                End With

                If FilterType = "L" Then
                    rblSearchType.SelectedValue = "L"
                    ChangeSearchCriteria()
                Else
                    FilterType = "C"
                End If
                'If IsManager = 1 Then
                '    ShowOnlyManagers = True
                'End If
                CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
                rfvCompanies.InitialValue = ResourceManager.GetString("PleaseSelect", CultureInfo)
                rfvEntity.InitialValue = ResourceManager.GetString("PleaseSelect", CultureInfo)
                rfvEntity.Enabled = IsLevelRequired
                FillCompanies()

                If (objVersion.HasMultiCompany() = False) Then
                    RadCmbBxCompanies.SelectedValue = objVersion.GetCompanyId()
                    RadCmbBxCompanies_SelectedIndexChanged(Nothing, Nothing)
                    trcompany.Visible = False
                End If

                If ShowRadioSearch Then
                    rblSearchType.Visible = True
                Else
                    rblSearchType.Visible = False
                End If
                If objApp_Settings.ShowDirectStaffChk = True Then '---FROM APP_SETTINGS
                    If ShowDirectStaffCheck = True Then '---FROM THE PAGE IN CODE BEHIND
                        chkDirectStaff.Visible = True
                    Else
                        chkDirectStaff.Visible = False
                    End If
                Else
                    chkDirectStaff.Visible = False
                End If
                If ValidationGroup = "" Then
                    ValidationGroup = "ValidateComp"
                End If
                SetValidationGroup(ValidationGroup)

                objSYSUsers = New SYSUsers
                objSYSUsers.ID = SessionVariables.LoginUser.ID
                objSYSUsers.GetUser()

                'If objSYSUsers.UserStatus = 3 And ForceCompanyFilter = False Then
                '    rfvCompanies.Enabled = True
                'Else
                '    rfvCompanies.Enabled = False
                'End If

                'If objSYSUsers.UserStatus = 2 And ForceCompanyFilter = False Then
                '    If Not objApp_Settings.ShowLGWithEntityPrivilege = True Then
                '        rblSearchType.Items.RemoveAt(1)
                '    End If
                '    rblSearchType.Items.RemoveAt(1)
                'End If
                If objSYSUsers.UserStatus = 2 And ForceCompanyFilter = False Then

                    If objSYSUsers.HasLogicalGroup_Privilege = False Then
                        rblSearchType.Items.Remove(rblSearchType.Items.FindByValue("L"))
                    End If
                    If objSYSUsers.HasWorkLocation_Privilege = False Then
                        rblSearchType.Items.Remove(rblSearchType.Items.FindByValue("W"))
                    End If
                    If objSYSUsers.HasEntity_Privilege = False Then
                        rblSearchType.Items.Remove(rblSearchType.Items.FindByValue("C"))
                    End If
                    rblSearchType.Items(0).Selected = True
                    ChangeSearchCriteria()
                End If

                'If objSYSUsers.UserStatus = 3 And ForceCompanyFilter = False Then
                '    rblSearchType.Items.Remove(rblSearchType.Items.FindByValue("W"))
                'End If

                If objSYSUsers.UserStatus = 2 And ForceCompanyFilter = False Then
                    objUserPrivileg_Entities = New UserPrivileg_Entities()
                    With objUserPrivileg_Entities
                        .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
                        .GetByEmpId()
                    End With

                    If Not objUserPrivileg_Entities Is Nothing Then
                        rfvEntity.Enabled = True
                        rfvEntity.InitialValue = ResourceManager.GetString("PleaseSelect", CultureInfo)
                        rfvEntity.ValidationGroup = ValidationGroup
                        IsLevelRequired = True
                    End If
                ElseIf objSYSUsers.UserStatus = 3 And ForceCompanyFilter = False Then
                    objUserPrivileg_Companies = New UserPrivileg_Companies()
                    With objUserPrivileg_Companies
                        .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
                        rfvCompanies.ValidationGroup = ValidationGroup
                        .GetByEmpId()
                    End With
                    If Not objUserPrivileg_Companies Is Nothing Then
                        rfvCompanies.Enabled = True
                        rfvCompanies.InitialValue = ResourceManager.GetString("PleaseSelect", CultureInfo)
                        rfvCompanies.ValidationGroup = ValidationGroup
                        IsLevelRequired = True

                    End If
                End If

            End If

            ' btnSearchEmployee.Attributes.Add("onclick", "javascript:SearchDialog(); return false;")
            RadCmbBxEntity.Attributes.Add("onclick", "javascript:EntityClick(); return false;")
            If Not Request.QueryString("FK_EmployeeId") Is Nothing Then
                Fk_EmployeeId = Request.QueryString("FK_EmployeeId")
            End If
            If Not Request.QueryString("LeaveId") Is Nothing Then
                LeaveId = Request.QueryString("LeaveId")
            End If
            If Not Request.QueryString("LeaveFrom") Is Nothing Then
                LeaveFrom = DateTime.Parse(Request.QueryString("LeaveFrom")).ToString("dd/MM/yyyy")

            End If
            If Not Request.QueryString("LeaveTo") Is Nothing Then
                LeaveTo = DateTime.Parse(Request.QueryString("LeaveTo")).ToString("dd/MM/yyyy")


            End If
            If Not Request.QueryString("EmployeeName") Is Nothing Then
                LeaveEmployeeName = Request.QueryString("EmployeeName")
            End If
            'If Not Page.IsPostBack Then 
            If Not Session("EmpSearchResult") Is Nothing Then
                ' If Not Session("SourceControl_Session").ToString.Contains(Me.Page.ClientID) Then
                'If Session("SourceControl_Session") = "" Then

                'End If
                FillEmpBySearch()
                'RequiredFieldValidator2.Enabled = False
                rfvEmployee.Enabled = False
                'End If
            End If
            'End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ChangeSearchCriteria()
        'ClearValues()
        'RadCmbBxCompanies.SelectedIndex = 0
        RadCmbBxEntity.Items.Clear()
        RadCmbBxEntity.Text = ""
        RadCmbBxEmployees.Items.Clear()
        RadCmbBxEmployees.Text = ""
        TxtEmpNo.Text = ""
        txtEmployee.Text = ""

        EmployeeId = 0
        EntityId = 0
        hEmployeeId = 0
        Select Case rblSearchType.SelectedValue
            Case "C" ' Company
                If Lang = CtlCommon.Lang.AR Then
                    lblLevels.Text = "وحدة العمل"
                Else
                    lblLevels.Text = "Entity"
                End If
                FillCompanies()
                FillEntity()
            Case "W" ' Work Location
                If Lang = CtlCommon.Lang.AR Then
                    lblLevels.Text = "موقع العمل"
                Else
                    lblLevels.Text = "Work Location"
                End If
                FillCompanies()
                FillWorkLocations()
            Case "L" ' Logical Group
                If Lang = CtlCommon.Lang.AR Then
                    lblLevels.Text = "المجموعة"
                Else
                    lblLevels.Text = "Logical Group"
                End If
                FillCompanies()
                FillLogicalGroups()
        End Select
        FilterType = rblSearchType.SelectedValue

        FillEmployee()
    End Sub

    Private Sub FillForMultiCompany()
        If (RadCmbBxCompanies.SelectedValue <> -1) Then
            CompanyId = RadCmbBxCompanies.SelectedValue
        Else
            CompanyId = 0
        End If
        If (objVersion.HasMultiCompany() = False) Then
            CompanyId = objVersion.GetCompanyId()
            RadCmbBxCompanies.SelectedValue = objVersion.GetCompanyId()
        End If

        Select Case rblSearchType.SelectedValue
            Case "C" ' Company
                FillEntity()
            Case "W" ' Work Location
                FillWorkLocations()
            Case "L" ' Logical Group
                FillLogicalGroups()
        End Select
        FilterType = rblSearchType.SelectedValue


        Dim dtEmployees = GetEmployeez()


        If Not FilterType = Nothing Then
            If (dtEmployees IsNot Nothing) Then
                If (dtEmployees.Rows.Count > 0) Then
                    Dim dtSource As New DataTable
                    dtSource.Columns.Add("EmployeeId")
                    dtSource.Columns.Add("EmployeeName")

                    Dim drRow As DataRow
                    drRow = dtSource.NewRow()

                    drRow(1) = ResourceManager.GetString("PleaseSelect", CultureInfo)
                    dtSource.Rows.Add(drRow)
                    For Item As Integer = 0 To dtEmployees.Rows.Count - 1
                        Dim drSource As DataRow
                        drSource = dtSource.NewRow
                        Dim dcCell1 As New DataColumn
                        Dim dcCell2 As New DataColumn
                        dcCell1.ColumnName = "EmployeeId"
                        dcCell2.ColumnName = "EmployeeName"
                        dcCell1.DefaultValue = dtEmployees.Rows(Item)(0)
                        'Add Space before and after the Dash (-) for search Emp No that contains (-)
                        dcCell2.DefaultValue = dtEmployees.Rows(Item)(3) + " - " + IIf(Lang = CtlCommon.Lang.EN, dtEmployees.Rows(Item)(1), dtEmployees.Rows(Item)(2))
                        drSource("EmployeeId") = dcCell1.DefaultValue
                        drSource("EmployeeName") = dcCell2.DefaultValue
                        dtSource.Rows.Add(drSource)
                    Next

                    With RadCmbBxEmployees
                        .DataSource = dtSource
                        .DataValueField = dtSource.Columns(0).ColumnName
                        .DataTextField = dtSource.Columns(1).ColumnName
                        .DataBind()
                    End With
                Else
                    CtlCommon.FillTelerikDropDownList(RadCmbBxEmployees, dtEmployees, Lang)
                End If
            End If
            'FillEmployee()
        End If


    End Sub

    Protected Sub RadCmbBxCompanies_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxCompanies.SelectedIndexChanged
        FillForMultiCompany()
    End Sub

    Protected Sub RadCmbBxEntity_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxEntity.SelectedIndexChanged
        If SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
        Else
            Lang = CtlCommon.Lang.EN
        End If

        If Not hdnIsEntityClick.Value = "True" Then
            Return
        End If

        'If RadCmbBxEntity.SelectedIndex < 0 Then
        EmployeeId = 0
        TxtEmpNo.Text = String.Empty
        txtEmployee.Text = String.Empty
        'End If

        If (RadCmbBxEntity.SelectedValue <> -1) Then
            EntityId = RadCmbBxEntity.SelectedValue
        Else
            EntityId = 0
        End If

        Dim dtEmployees = GetEmployeez()

        If (dtEmployees IsNot Nothing) Then
            If (dtEmployees.Rows.Count > 0) Then
                Dim dtSource As New DataTable
                dtSource.Columns.Add("EmployeeId")
                dtSource.Columns.Add("EmployeeName")

                Dim drRow As DataRow
                drRow = dtSource.NewRow()

                drRow(1) = ResourceManager.GetString("PleaseSelect", CultureInfo)
                dtSource.Rows.Add(drRow)
                For Item As Integer = 0 To dtEmployees.Rows.Count - 1
                    Dim drSource As DataRow
                    drSource = dtSource.NewRow
                    Dim dcCell1 As New DataColumn
                    Dim dcCell2 As New DataColumn
                    dcCell1.ColumnName = "EmployeeId"
                    dcCell2.ColumnName = "EmployeeName"
                    dcCell1.DefaultValue = dtEmployees.Rows(Item)(0)
                    'Add Space before and after the Dash (-) for search Emp No that contains (-)
                    dcCell2.DefaultValue = dtEmployees.Rows(Item)(3) + " - " + IIf(Lang = CtlCommon.Lang.EN, dtEmployees.Rows(Item)(1), dtEmployees.Rows(Item)(2))
                    drSource("EmployeeId") = dcCell1.DefaultValue
                    drSource("EmployeeName") = dcCell2.DefaultValue
                    dtSource.Rows.Add(drSource)
                Next

                With RadCmbBxEmployees
                    .DataSource = dtSource
                    .DataValueField = dtSource.Columns(0).ColumnName
                    .DataTextField = dtSource.Columns(1).ColumnName
                    .DataBind()
                End With
            Else
                CtlCommon.FillTelerikDropDownList(RadCmbBxEmployees, dtEmployees, Lang)
            End If
        Else
            CtlCommon.FillTelerikDropDownList(RadCmbBxEmployees, dtEmployees, Lang)
        End If

        hdnIsEntityClick.Value = String.Empty
    End Sub

    Protected Sub RadCmbBxEmployees_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxEmployees.SelectedIndexChanged
        If (RadCmbBxEmployees.SelectedIndex <> 0) Then
            EmployeeId = CType(RadCmbBxEmployees.SelectedValue, Integer)
            'Add Space before and after the Dash (-) for search Emp No that contains (-)
            TxtEmpNo.Text = RadCmbBxEmployees.Text.Split(" - ")(0)
            FillFilterControls()
            'RaiseEvent eventEmployeeSelect(Nothing, Nothing)

        Else
            EmployeeId = 0
        End If
        hEmployeeId = EmployeeId

        RaiseEvent eventEmployeeSelect(sender, e)
    End Sub

    Protected Sub ddlSearchType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblSearchType.SelectedIndexChanged
        ChangeSearchCriteria()
    End Sub

    Private Sub FillFilterControls()
        objApp_Settings = New APP_Settings
        Dim dtEmp As DataTable
        dtEmp = New DataTable
        objEmployee = New Employee
        With objApp_Settings
            .GetByPK()
            If .ShowEmployeeList = False Then
                objEmployee.EmployeeNo = TxtEmpNo.Text.Trim
                objEmployee.UserId = SessionVariables.LoginUser.ID
                If SmartV.Version.version.HasMultiCompany Then
                    objEmployee.FK_CompanyId = RadCmbBxCompanies.SelectedValue
                Else
                    objEmployee.FK_CompanyId = SmartV.Version.version.GetCompanyId
                End If
                dtEmp = objEmployee.GetEmpByEmpNo()

                If dtEmp Is Nothing Or dtEmp.Rows.Count = 0 Then
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EmpNonotinlevel", CultureInfo), "info")
                    Return
                End If

                EmployeeIdtxt = dtEmp.Rows(0)("EmployeeId")
                EmployeeId = dtEmp.Rows(0)("EmployeeId")
                RadCmbBxEntity.SelectedValue = dtEmp.Rows(0)("FK_EntityId")
                EmployeeName = dtEmp.Rows(0)("EmployeeName")
                If SessionVariables.CultureInfo = "ar-JO" Then
                    txtEmployee.Text = dtEmp.Rows(0)("EmployeeArabicName")
                    EmployeeName = dtEmp.Rows(0)("EmployeeArabicName")
                Else
                    txtEmployee.Text = dtEmp.Rows(0)("EmployeeName")
                End If


            Else
                Dim NotIn As Boolean = False
                objEmployee = New Employee()
                If (TxtEmpNo.Text <> String.Empty) Then

                    For Index As Integer = 0 To RadCmbBxEmployees.Items.Count - 1
                        'Add Space before and after the Dash (-) for search Emp No that contains (-)
                        'If RadCmbBxEmployees.Items(Index).Text.Split(" - ")(0).ToLower = TxtEmpNo.Text.ToLower Or RadCmbBxEmployees.Items(Index).Text.Split(" - ")(0).ToUpper = TxtEmpNo.Text.ToUpper Then 'To Search Upper and Lower Case Letters
                        'If String.Compare(RadCmbBxEmployees.Items(Index).Text.Split(" - ")(0), TxtEmpNo.Text, True) = 0 Then
                        If RadCmbBxEmployees.Items(Index).Text.Split(" - ")(0).ToLower = TxtEmpNo.Text.ToLower.Split(" - ")(0) Then
                            RadCmbBxEmployees.SelectedValue = RadCmbBxEmployees.Items(Index).Value
                            EmployeeId = RadCmbBxEmployees.SelectedValue
                            EmployeeName = RadCmbBxEmployees.Items(Index).Text.ToString()

                            objEmployee.EmployeeId = EmployeeId
                            objEmployee.GetByPK()
                            EmpDesignation = objEmployee.FK_Designation
                            EmpEmail = objEmployee.Email
                            If FilterType = "C" Then
                                RadCmbBxEntity.SelectedValue = objEmployee.FK_EntityId
                            ElseIf FilterType = "W" Then
                                RadCmbBxEntity.SelectedValue = objEmployee.FK_WorkLocation
                            ElseIf FilterType = "L" Then
                                RadCmbBxEntity.SelectedValue = objEmployee.FK_LogicalGroup
                            End If


                            EntityId = objEmployee.FK_EntityId


                            hEmployeeId = EmployeeId
                            NotIn = True
                        End If
                    Next

                End If

                If NotIn = False Then
                    objEmployee.EmployeeNo = TxtEmpNo.Text.Trim
                    objEmployee.FK_CompanyId = RadCmbBxCompanies.SelectedValue
                    dtEmp = objEmployee.GetAll_ByEmplyeeNo()

                    If dtEmp Is Nothing Or dtEmp.Rows.Count = 0 Then
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EmpNonotinlevel", CultureInfo), "info")
                        Return
                    End If

                    EmployeeIdtxt = dtEmp.Rows(0)("EmployeeId")
                    EmployeeId = dtEmp.Rows(0)("EmployeeId")
                    EmployeeName = dtEmp.Rows(0)("EmployeeName")
                    RadCmbBxEntity.SelectedValue = dtEmp.Rows(0)("FK_EntityId")
                    If SessionVariables.CultureInfo = "ar-JO" Then
                        txtEmployee.Text = dtEmp.Rows(0)("EmployeeArabicName")
                    Else
                        txtEmployee.Text = dtEmp.Rows(0)("EmployeeName")
                    End If
                    Dim item1 As New RadComboBoxItem
                    item1.Value = dtEmp.Rows(0)("EmployeeId")
                    item1.Text = dtEmp.Rows(0)("EmployeeNo") + (" - ") + dtEmp.Rows(0)("EmployeeName")

                    RadCmbBxEmployees.Items.Add(item1)
                    hEmployeeId = EmployeeId
                    RadCmbBxEmployees.SelectedValue = EmployeeId
                    NotIn = True
                End If

                If RadCmbBxEntity.SelectedIndex = -1 Then
                    Try
                        objEmployee.EmployeeId = EmployeeId
                        objEmployee.GetByPK()
                        hEmployeeId = EmployeeId
                    Catch ex As Exception

                    End Try
                    If Not objEmployee Is Nothing Then
                        EntityId = objEmployee.FK_EntityId
                        RadCmbBxEntity.SelectedValue = EntityId
                    Else
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EmpNotManager", CultureInfo), "info")
                    End If

                End If


                If NotIn = False Then
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EmpNonotinlevel", CultureInfo), "info")
                End If
                RaiseEvent eventEmployeeSelect(Nothing, Nothing)
                Return

            End If
        End With

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        FillFilterControls()
    End Sub



    Protected Sub FillEmpBySearch()
        Dim SearchValues As ArrayList = DirectCast(Session("EmpSearchResult"), ArrayList)
        EmployeeId = CInt(SearchValues(0))
        CompanyId = CInt(SearchValues(1))
        EmpNo = SearchValues(2)
        FillCompanies()
        RadCmbBxCompanies.SelectedValue = CompanyId
        rfvCompanies.Enabled = False
        FillForMultiCompany()
        FillEmployee()
        TxtEmpNo.Text = EmpNo
        FillFilterControls()
        Session("EmpSearchResult") = Nothing

    End Sub

    Protected Sub chkDirectStaff_CheckedChanged(sender As Object, e As EventArgs) Handles chkDirectStaff.CheckedChanged
        If chkDirectStaff.Checked = True Then
            DirectStaffOnly = True
        Else
            DirectStaffOnly = False
        End If
    End Sub

#End Region

#Region "Methods"

    Private Sub FillEntityByManagerId()
        If RadCmbBxCompanies.SelectedValue <> -1 Then
            Dim objProjectCommon = New ProjectCommon()
            objSYSUsers = New SYSUsers
            objSYSUsers.ID = SessionVariables.LoginUser.ID
            objSYSUsers.GetUser()
            If objSYSUsers.UserStatus = 1 Or ForceCompanyFilter = True Then
                Dim objOrgEntity = New OrgEntity()
                objOrgEntity.FK_CompanyId = RadCmbBxCompanies.SelectedValue
                objOrgEntity.FK_ManagerId = FK_ManagerId

                Dim dt As DataTable = objOrgEntity.GetAllEntityByCompanyAndMangerforforce()

                'objProjectCommon.FillMultiLevelRadComboBox(RadCmbBxEntity, dt, "EntityId", _
                '                                         "EntityName", "EntityArabicName", "FK_ParentId")

                RadCmbBxEntity.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem(ResourceManager.GetString("PleaseSelect", CultureInfo), -1))
                CtlCommon.FillTelerikDropDownList(RadCmbBxEntity, dt, Lang)
                'ElseIf objSYSUsers.UserStatus = 2 Then
                '    Dim objOrgEntity = New OrgEntity()
                '    Dim dtEntity As New DataTable
                '    objOrgEntity.FK_CompanyId = RadCmbBxCompanies.SelectedValue
                '    objOrgEntity.FK_UserId = objSYSUsers.ID
                '    dtEntity = objOrgEntity.GetAllEntityByCompanyAndByUserId
                '    If DTable.IsValidDataTable(dtEntity) Then
                '        CtlCommon.FillTelerikDropDownList(RadCmbBxEntity, dtEntity, Lang)
                '    End If
            Else
                Dim objOrgEntity = New OrgEntity()
                objOrgEntity.FK_CompanyId = RadCmbBxCompanies.SelectedValue
                objOrgEntity.FK_ManagerId = FK_ManagerId
                Dim dt As DataTable = objOrgEntity.GetAllEntityByCompanyAndManger()

                objProjectCommon.FillMultiLevelRadComboBox(RadCmbBxEntity, dt, "EntityId",
                                                         "EntityName", "EntityArabicName", "FK_ParentId")
                RadCmbBxEntity.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem(ResourceManager.GetString("PleaseSelect", CultureInfo), -1))
            End If
        End If
    End Sub

    Private Sub FillEntity()
        If IsManager = 1 Then
            FillEntityByManagerId()
        Else
            If RadCmbBxCompanies.SelectedValue <> -1 Then
                Dim objProjectCommon = New ProjectCommon()
                objSYSUsers = New SYSUsers
                objSYSUsers.ID = SessionVariables.LoginUser.ID
                objSYSUsers.GetUser()
                If objSYSUsers.UserStatus = 1 Then
                    Dim objOrgEntity = New OrgEntity()
                    objOrgEntity.FK_CompanyId = RadCmbBxCompanies.SelectedValue
                    Dim dt As DataTable = objOrgEntity.GetAllEntityByCompany()

                    objProjectCommon.FillMultiLevelRadComboBox(RadCmbBxEntity, dt, "EntityId",
                                                             "EntityName", "EntityArabicName", "FK_ParentId")
                    RadCmbBxEntity.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem(ResourceManager.GetString("PleaseSelect", CultureInfo), -1))
                ElseIf objSYSUsers.UserStatus = 2 Then
                    Dim objOrgEntity = New OrgEntity()
                    Dim dtEntity As New DataTable
                    objOrgEntity.FK_CompanyId = RadCmbBxCompanies.SelectedValue
                    objOrgEntity.FK_UserId = objSYSUsers.ID
                    dtEntity = objOrgEntity.GetAllEntityByCompanyAndByUserId
                    If DTable.IsValidDataTable(dtEntity) Then
                        CtlCommon.FillTelerikDropDownList(RadCmbBxEntity, dtEntity, Lang)
                    End If
                Else
                    Dim objOrgEntity = New OrgEntity()
                    objOrgEntity.FK_CompanyId = RadCmbBxCompanies.SelectedValue
                    Dim dt As DataTable = objOrgEntity.GetAllEntityByCompany()

                    objProjectCommon.FillMultiLevelRadComboBox(RadCmbBxEntity, dt, "EntityId",
                                                             "EntityName", "EntityArabicName", "FK_ParentId")
                    RadCmbBxEntity.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem(ResourceManager.GetString("PleaseSelect", CultureInfo), -1))
                End If
            End If
        End If
    End Sub

    Private Sub FillWorkLocations()
        Try
            Dim objEmp_WorkLocation As New Emp_WorkLocation
            Dim dtWorkLoc As New DataTable
            objSYSUsers = New SYSUsers
            objSYSUsers.ID = SessionVariables.LoginUser.ID
            objSYSUsers.GetUser()
            If objSYSUsers.UserStatus = 1 Or objSYSUsers.UserStatus = 3 Then '  System User, Company User
                If RadCmbBxCompanies.SelectedIndex > 0 Then
                    objEmp_WorkLocation.FK_CompanyId = RadCmbBxCompanies.SelectedValue
                    dtWorkLoc = objEmp_WorkLocation.GetAllByCompany()
                    CtlCommon.FillTelerikDropDownList(RadCmbBxEntity, dtWorkLoc, Lang)
                End If
            Else
                If RadCmbBxCompanies.SelectedIndex > 0 Then
                    objEmp_WorkLocation.FK_CompanyId = RadCmbBxCompanies.SelectedValue
                    objEmp_WorkLocation.FK_UserId = objSYSUsers.ID
                    dtWorkLoc = objEmp_WorkLocation.GetAllByCompanyAndUserId()
                    CtlCommon.FillTelerikDropDownList(RadCmbBxEntity, dtWorkLoc, Lang)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FillLogicalGroups()
        Try
            Dim objEmpLogicalGroup = New Emp_logicalGroup()
            Dim dt As New DataTable
            objApp_Settings = New APP_Settings
            objApp_Settings.GetByPK()
            objSYSUsers = New SYSUsers
            objSYSUsers.ID = SessionVariables.LoginUser.ID
            objSYSUsers.GetUser()
            If objSYSUsers.UserStatus = 1 Or objSYSUsers.UserStatus = 3 Then ' Full, System User
                If RadCmbBxCompanies.SelectedValue <> -1 Then
                    objEmpLogicalGroup.FK_CompanyId = RadCmbBxCompanies.SelectedValue
                    dt = objEmpLogicalGroup.GetAllByCompany()
                    CtlCommon.FillTelerikDropDownList(RadCmbBxEntity, dt, Lang)
                End If
            ElseIf (objSYSUsers.UserStatus = 2 And objApp_Settings.ShowLGWithEntityPrivilege = True) Then
                If RadCmbBxCompanies.SelectedIndex > 0 Then
                    objEmpLogicalGroup.FK_CompanyId = RadCmbBxCompanies.SelectedValue
                    objEmpLogicalGroup.FK_UserId = objSYSUsers.ID
                    dt = objEmpLogicalGroup.GetAllByCompanyAndUserId()
                    CtlCommon.FillTelerikDropDownList(RadCmbBxEntity, dt, Lang)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FillEmployee()

        objApp_Settings = New APP_Settings
        objEmployee = New Employee
        objEmployee.EmployeeNo = ""
        Dim dtEmp As DataTable
        dtEmp = New DataTable
        With objApp_Settings
            .GetByPK()
            Dim ShowEmployeeList = .ShowEmployeeList
            If ShowEmployeeList = False Then
                'objEmployee.EmployeeNo = EmpNo
                'objEmployee.UserId = SessionVariables.LoginUser.ID
                'objEmployee.UserId = SessionVariables.LoginUser.ID
                'EmployeeIdtxt = dtEmp.Rows(0)("EmployeeId")
                'EmployeeId = dtEmp.Rows(0)("EmployeeId")
                'RadCmbBxEntity.SelectedValue = dtEmp.Rows(0)("FK_EntityId")
                'If SessionVariables.CultureInfo = "ar-JO" Then
                '    txtEmployee.Text = dtEmp.Rows(0)("EmployeeArabicName")
                'Else
                '    txtEmployee.Text = dtEmp.Rows(0)("EmployeeName")
                'End If
                Exit Sub
            Else


                If RadCmbBxCompanies.SelectedValue <> "-1" And RadCmbBxEntity.SelectedValue <> "-1" Then
                    Dim objEmployee As New Employee
                    objEmployee.FK_CompanyId = RadCmbBxCompanies.SelectedValue
                    objEmployee.FK_EntityId = RadCmbBxEntity.SelectedValue
                    objEmployee.FK_ManagerID = FK_ManagerId
                    If IsManager = 1 Then
                        Dim dt As DataTable = objEmployee.GetEmployeeByManagerId
                    Else

                        If ShowOnlyManagers Then
                            Dim dt As DataTable = objEmployee.GetManagersByCompany
                        Else
                            Dim dt As DataTable = objEmployee.GetEmpByCompany
                        End If
                    End If
                End If


            End If

        End With
    End Sub

    Private Sub FillCompanies()
        'Get user info and check the user security
        If SessionVariables.LoginUser IsNot Nothing Then
            objApp_Settings = New APP_Settings
            objSYSUsers = New SYSUsers
            objSYSUsers.ID = SessionVariables.LoginUser.ID
            objSYSUsers.GetUser()
            objApp_Settings.GetByPK()
            If objSYSUsers.UserStatus = 1 Then 'System User - Full 
                Dim objOrgCompany As New OrgCompany
                CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies, objOrgCompany.GetAllforddl, Lang)
            ElseIf objSYSUsers.UserStatus = 2 Then
                If objApp_Settings.ShowLGWithEntityPrivilege = False Then
                    FillCompanyForUserSecurity()
                Else
                    Dim objOrgCompany As New OrgCompany
                    CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies, objOrgCompany.GetAllforddl, Lang)
                End If

            ElseIf objSYSUsers.UserStatus = 3 Then
                FillCompanyForUserSecurity()
            Else
                Dim objOrgCompany As New OrgCompany
                CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies, objOrgCompany.GetAllforddl, Lang)
            End If
        End If
        If (objVersion.HasMultiCompany() = False) Then
            CompanyId = objVersion.GetCompanyId()
            RadCmbBxCompanies.SelectedValue = objVersion.GetCompanyId()
        End If
    End Sub

    Private Sub FillCompanyForUserSecurity()
        Try
            Dim objOrgCompany As New OrgCompany
            Dim dtCompanies As New DataTable

            objOrgCompany.FK_UserId = objSYSUsers.ID
            objOrgCompany.FilterType = rblSearchType.SelectedValue
            dtCompanies = objOrgCompany.GetAllforddl_ByUserId

            CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies, dtCompanies, Lang)


        Catch ex As Exception

        End Try
    End Sub

    Private Sub CreateEntityTable()
        Dim objOrgEntity = New OrgEntity()
        Dim objProjectCommon = New ProjectCommon()
        objOrgEntity.EntityId = objUserPrivileg_Entities.FK_EntityId
        Dim dt = objOrgEntity.GetEntityAndChilds()

        objProjectCommon.FillMultiLevelRadComboBox(RadCmbBxEntity, dt, "EntityId",
                                                      "EntityName", "EntityArabicName", "FK_ParentId")
        RadCmbBxEntity.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem(ResourceManager.GetString("PleaseSelect", CultureInfo), -1))
    End Sub

    Public Sub ClearValues()
        If (objVersion.HasMultiCompany()) Then
            EmployeeId = 0
            EntityId = 0
            If (RadCmbBxCompanies.SelectedValue <> -1) Then
                CompanyId = RadCmbBxCompanies.SelectedValue
            Else
                CompanyId = 0
            End If

            RadCmbBxEntity.Items.Clear()
            RadCmbBxEntity.Text = String.Empty
            RadCmbBxEmployees.Items.Clear()
            RadCmbBxEmployees.Text = String.Empty
            RadCmbBxCompanies.SelectedIndex = 0
            TxtEmpNo.Text = String.Empty
            txtEmployee.Text = String.Empty
            FillCompanies()
            rblSearchType.SelectedIndex = 0
        Else
            EmployeeId = 0
            EntityId = 0
            If (RadCmbBxCompanies.SelectedValue <> -1) Then
                CompanyId = RadCmbBxCompanies.SelectedValue
            Else
                CompanyId = 0
            End If
            RadCmbBxEntity.SelectedIndex = 0
            'RadCmbBxEmployees.Items.Clear()
            RadCmbBxEmployees.Text = String.Empty
            RadCmbBxEmployees.ClearSelection()
            TxtEmpNo.Text = String.Empty
            txtEmployee.Text = String.Empty
            rblSearchType.SelectedIndex = 0

        End If
        If FilterType = "L" Then
            rblSearchType.SelectedValue = "L"
            ChangeSearchCriteria()
            FillLogicalGroups()
            FillEmployee()
        End If
    End Sub

    Public Sub SetSelectedCompanyAndEmployee(ByVal CompanyId As Integer, ByVal EmployeeId As Integer)
        RadCmbBxCompanies.SelectedValue = CompanyId
        RadCmbBxCompanies_SelectedIndexChanged(Nothing, Nothing)
        RadCmbBxEmployees.SelectedValue = EmployeeId
        RadCmbBxEmployees_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Public Sub SetValidationGroup(ByVal strValidationGroup As String)
        Try
            ValidationGroup = strValidationGroup
            rfvCompanies.ValidationGroup = ValidationGroup
            Dim strVG2 As String = strValidationGroup
            strVG2 = strVG2 + "2"
            'RequiredFieldValidator2.ValidationGroup = strVG2
            btnSearch.ValidationGroup = strVG2

        Catch ex As Exception

        End Try
    End Sub

    Public Sub EnabledDisbaledControls(ByVal enabled As Boolean)
        RadCmbBxCompanies.Enabled = enabled
        RadCmbBxEmployees.Enabled = enabled
        RadCmbBxEntity.Enabled = enabled
        TxtEmpNo.Enabled = enabled
        btnSearch.Enabled = enabled
        'btnSearchEmployee.Enabled = enabled
    End Sub

#Region "GetEmployees"
    Public Function GetEmployeez() As DataTable
        Dim dtEmployees As DataTable = New DataTable
        objApp_Settings = New APP_Settings
        With objApp_Settings
            .GetByPK()
            If .ShowEmployeeList = False Then
                dtEmployees = Nothing
                Return dtEmployees
            Else


                objEmployee = New Employee()
                objEmployee.FK_CompanyId = RadCmbBxCompanies.SelectedValue
                objEmployee.FK_ManagerID = FK_ManagerId
                If (RadCmbBxEntity.SelectedIndex <> -1) Then
                    objEmployee.FK_EntityId = RadCmbBxEntity.SelectedValue
                Else
                    objEmployee.FK_EntityId = -1
                End If
                objEmployee.FilterType = rblSearchType.SelectedValue


                If IsManager = 1 Then
                    dtEmployees = objEmployee.GetEmployeeByManagerId
                Else
                    If ShowOnlyManagers Then
                        dtEmployees = objEmployee.GetManagersByCompany
                    Else
                        dtEmployees = objEmployee.GetEmployeeIDz()
                    End If
                End If
                If (dtEmployees IsNot Nothing And dtEmployees.Rows.Count > 0) Then
                    Return dtEmployees
                End If
                Return dtEmployees
            End If
        End With
    End Function
#End Region

    Public Function GetEmployeeInfo(ByVal EmpId As Integer)
        objEmployee = New Employee()
        objEmployee.EmployeeId = EmpId
        objEmployee.GetByPK()
        EmployeeId = EmpId
        hEmployeeId = EmpId
        Dim CompanyId As Integer
        CompanyId = objEmployee.FK_CompanyId
        RadCmbBxCompanies.SelectedValue = CompanyId

        Dim EntityId As Integer

        If FilterType = "L" Then
            LogicalGroupId = objEmployee.FK_LogicalGroup
            FillLogicalGroups()
            RadCmbBxEntity.SelectedValue = LogicalGroupId
        Else
            EntityId = objEmployee.FK_EntityId
            FillEntity()
            RadCmbBxEntity.SelectedValue = EntityId
        End If
        FillEmployee()
        hdnIsEntityClick.Value = True
        RadCmbBxEntity_SelectedIndexChanged(Nothing, Nothing)
        RadCmbBxEmployees.SelectedValue = EmpId
        EmployeeId = EmpId
        'TxtEmpNo.Text = RadCmbBxEmployees.SelectedItem.Text.Split(" - ")(0)
        objApp_Settings = New APP_Settings
        With objApp_Settings
            .GetByPK()
            If .ShowEmployeeList = False Then
                objEmployee.EmployeeId = EmpId
                objEmployee.GetByPK()
                EmployeeIdtxt = objEmployee.EmployeeId
                EmployeeId = objEmployee.EmployeeId

                RadCmbBxEntity.SelectedValue = objEmployee.FK_EntityId
                If SessionVariables.CultureInfo = "ar-JO" Then
                    txtEmployee.Text = objEmployee.EmployeeArabicName
                Else
                    txtEmployee.Text = objEmployee.EmployeeName
                End If
                TxtEmpNo.Text = objEmployee.EmployeeNo
            Else
                TxtEmpNo.Text = RadCmbBxEmployees.SelectedItem.Text.Split(" - ")(0)
            End If
        End With

    End Function

    Public Function GetEmployeeAllInfo() As Employee
        objEmployee = New Employee()
        objEmployee.EmployeeId = EmployeeId
        objEmployee.GetByPK()
        Return objEmployee
    End Function
    Public Sub GetSelectedCompany(ByVal CompanyId As Integer)
        objCompany = New OrgCompany
        objCompany.CompanyId = CompanyId
        objCompany.GetCompanyNameByID()
        FillCompanies()
        RadCmbBxCompanies.SelectedValue = CompanyId
        RadCmbBxCompanies_SelectedIndexChanged(Nothing, Nothing)

    End Sub
    Public Sub GetSelectedEntity(ByVal EntityId As Integer)
        objEntity = New OrgEntity
        objEntity.EntityId = EntityId
        objEntity.GetParentNameByEntityID()
        FillEntity()
        RadCmbBxEntity.SelectedValue = EntityId
        RadCmbBxEntity_SelectedIndexChanged(Nothing, Nothing)

    End Sub
    Public Function GetEmployeeNameWithoutEntity(ByVal EmpId As Integer)
        objEmployee = New Employee()
        objEmployee.EmployeeId = EmpId
        objEmployee.GetByPK()
        EmployeeId = EmpId
        hEmployeeId = EmpId
        Dim CompanyId As Integer
        CompanyId = objEmployee.FK_CompanyId
        RadCmbBxCompanies.SelectedValue = CompanyId

        Dim EntityId As Integer

        objEmployee.FK_CompanyId = RadCmbBxCompanies.SelectedValue

        If ShowOnlyManagers Then
            Dim dt As DataTable = objEmployee.GetManagersByCompany
        Else
            Dim dt As DataTable = objEmployee.GetEmpByCompany
        End If

        RadCmbBxEmployees.SelectedValue = EmpId
        'Add Space before and after the Dash (-) for search Emp No that contains (-)
        TxtEmpNo.Text = RadCmbBxEmployees.SelectedItem.Text.Split(" - ")(0)
        ''Modified By A.Samadi''
    End Function
    Public Sub ClearValuesAnnouncements()
        If (objVersion.HasMultiCompany()) Then
            EmployeeId = 0
            EntityId = 0
            CompanyId = 0
            hEmployeeId = 0


            RadCmbBxEntity.Items.Clear()
            RadCmbBxEntity.Text = String.Empty
            RadCmbBxEmployees.Items.Clear()
            RadCmbBxEmployees.Text = String.Empty
            RadCmbBxCompanies.SelectedIndex = 0
            TxtEmpNo.Text = String.Empty
            txtEmployee.Text = String.Empty
            FillCompanies()
            rblSearchType.SelectedIndex = 0
        Else
            EmployeeId = 0
            EntityId = 0
            hEmployeeId = 0

            CompanyId = 0

            RadCmbBxEntity.SelectedIndex = 0
            'RadCmbBxEmployees.Items.Clear()
            RadCmbBxEmployees.Text = String.Empty
            RadCmbBxEmployees.ClearSelection()
            TxtEmpNo.Text = String.Empty
            txtEmployee.Text = String.Empty
            rblSearchType.SelectedIndex = 0

        End If
        If FilterType = "L" Then
            rblSearchType.SelectedValue = "L"
            ChangeSearchCriteria()
            FillLogicalGroups()
            FillEmployee()
        End If
    End Sub

#End Region

    Protected Sub btnAssignEmployee_Click(sender As Object, e As EventArgs) Handles btnAssignEmployee.Click

        objAssignEmp = New AssignEmployee
        Dim IntError As Integer = -1

        With objAssignEmp
            .FK_EmployeeId = Fk_EmployeeId 'From other page
            .AssignedEmployee = EmployeeId
            .LeaveId = LeaveId 'From other page

            IntError = .Add()

            If IntError = 0 Then
                SendEmail(EmpEmail)
                Dim strMessage As String = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)
                CtlCommon.ShowMessage(Me.Page, strMessage, "success")
            Else
                CtlCommon.ShowMessage(Me.Page, "Employee Not Assigned!", "")
            End If
        End With

    End Sub
    Sub SendEmail(ToEmail As String)

        Dim Port As String = ConfigurationManager.AppSettings.Get("Port").ToString()
        Dim SenderEmailID As String = ConfigurationManager.AppSettings.Get("SenderEmailID").ToString()
        Dim SenderEmailPassword As String = ConfigurationManager.AppSettings.Get("SenderEmailPassword").ToString()
        Dim SmtpServer_con As String = ConfigurationManager.AppSettings.Get("SmtpServer").ToString()
        Dim MailFrom As String = ConfigurationManager.AppSettings.Get("MailFrom").ToString()


        Dim htmlstr As String = ""
        Dim mail As MailMessage = New MailMessage()
        Dim SmtpServer As SmtpClient = New SmtpClient(SmtpServer_con)
        SmtpServer.UseDefaultCredentials = False
        mail.From = New MailAddress(MailFrom)

        mail.To.Add(ToEmail)
        If Lang = CtlCommon.Lang.AR Then
            '  htmlstr = $"<html dir='rtl' lang='ar'><body> <p>عزيزي  " + EmployeeName + ",</p> <p>  تم تعيينك لتنوب عن الموظف" + LeaveEmployeeName +" خلال فترة إجازته بتاريخ" + LeaveTo +"-"+ LeaveFrom +" .</p></body></html>"

              htmlstr = $"<html dir='rtl' lang='ar'><body> <p>السيد/ة  " + EmployeeName + ",</p> <p>  تم تعيينك لتنوب عن الموظف/ة" +  LeaveEmployeeName +" خلال فترة إجازته من تاريخ" + LeaveTo +" الى تاريخ "+ LeaveFrom +" .</p></body></html>"

        Else
                  htmlstr = $"<html><body><p>Dear " + EmployeeName +", </p><p>You have been appointed to represent the employee " + LeaveEmployeeName +" during his leave period on the "+ LeaveFrom + "-" + LeaveTo +"</p></body></html>"
        End If


        mail.Subject = "Notification"
        mail.Body = htmlstr
        mail.IsBodyHtml = True
        SmtpServer.Port = CType(Port, Integer)
        SmtpServer.Credentials = New System.Net.NetworkCredential(SenderEmailID, SenderEmailPassword)
        SmtpServer.EnableSsl = False
        SmtpServer.Send(mail)


    End Sub
End Class
