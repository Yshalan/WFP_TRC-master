Imports TA.Admin
Imports SmartV.UTILITIES
Imports TA.Employees
Imports SmartV.Security
Imports System.Data
Imports TA.Lookup
Imports SmartV.Version
Imports TA.Security

Partial Class Admin_UserControls_UserSecurityFilter
    Inherits System.Web.UI.UserControl

#Region "Class Variables"
    Private Lang As CtlCommon.Lang
    Private objEmployee As Employee
    Dim objSYSUsers As SYSUsers
    Private objVersion As version
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Dim objUserPrivileg_Entities As UserPrivileg_Entities
    Dim objUserPrivileg_Companies As UserPrivileg_Companies
    Private objAPP_Settings As APP_Settings

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
#Region "Property :: CompanyValidationGroup"
    Public Property CompanyValidationGroup() As String
        Get
            Return RadCmbBxCompanies.ValidationGroup
        End Get
        Set(ByVal value As String)
            RadCmbBxCompanies.ValidationGroup = value
        End Set
    End Property
#End Region
#Region "Property :: CompanyRequiredFieldValidationGroup"
    Public Property CompanyRequiredFieldValidationGroup() As String
        Get
            Return rfvCompanies.ValidationGroup
        End Get
        Set(ByVal value As String)
            rfvCompanies.ValidationGroup = value
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
#Region "Property :: IsEntityPostBack"
    Public Property IsEntityPostBack() As Boolean
        Get
            Return RadCmbBxEntity.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            RadCmbBxEntity.AutoPostBack = value
        End Set
    End Property
#End Region
#Region "Property :: IsCompanyRequired"
    Public Property IsCompanyRequired() As Boolean
        Get
            Return ViewState("IsCompanyRequired")
        End Get
        Set(ByVal value As Boolean)
            ViewState("IsCompanyRequired") = value
        End Set
    End Property
#End Region
#Region "Property :: IsEntityRequired"
    Public Property IsEntityRequired() As Boolean
        Get
            Return ViewState("IsEntityRequired")
        End Get
        Set(ByVal value As Boolean)
            ViewState("IsEntityRequired") = value
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
    Public Property DirectStaffOnly() As Boolean
        Get
            Return ViewState("DirectStaffOnly")
        End Get
        Set(ByVal value As Boolean)
            ViewState("DirectStaffOnly") = value
        End Set
    End Property
#End Region

#Region "Events"

    Delegate Sub CompanySelectedChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs)
    Public Event eventCompanySelect As CompanySelectedChanged

    Delegate Sub EntitySelectedChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs)
    Public Event eventEntitySelected As EntitySelectedChanged

#End Region

#Region "Page Events"

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        ElseIf SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
        Else
            Lang = CtlCommon.Lang.EN
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        rfvCompanies.Enabled = True
        If Not IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            rfvCompanies.InitialValue = ResourceManager.GetString("PleaseSelect", CultureInfo)

            FillCompanies()

            If (objVersion.HasMultiCompany() = False) Then

                RadCmbBxCompanies.SelectedValue = objVersion.GetCompanyId()
                CompanyId = RadCmbBxCompanies.SelectedValue
                RadCmbBxCompanies_SelectedIndexChanged(Nothing, Nothing)
                trCompany.Visible = False
            End If

            If Not IsCompanyRequired Then
                rfvCompanies.Enabled = False
                rfvCompanies.Visible = False
            End If

            If Not IsEntityRequired Then
                rfvEntity.Enabled = False
            End If
            rfvCompanies.ValidationGroup = CompanyRequiredFieldValidationGroup
            objSYSUsers = New SYSUsers
            objSYSUsers.ID = SessionVariables.LoginUser.ID
            objSYSUsers.GetUser()
            If objSYSUsers.UserStatus = 2 Then
                objUserPrivileg_Entities = New UserPrivileg_Entities()
                With objUserPrivileg_Entities
                    .FK_EmployeeId = SessionVariables.LoginUser.EmployeeID
                    .GetByEmpId()
                End With

                If Not objUserPrivileg_Entities Is Nothing Then
                    rfvEntity.Enabled = True
                    rfvEntity.InitialValue = ResourceManager.GetString("PleaseSelect", CultureInfo)
                    rfvEntity.ValidationGroup = ValidationGroup
                End If

                objUserPrivileg_Companies = New UserPrivileg_Companies()
                With objUserPrivileg_Companies
                    .FK_EmployeeId = SessionVariables.LoginUser.EmployeeID
                    rfvCompanies.ValidationGroup = ValidationGroup
                    .GetByEmpId()
                End With

                If Not objUserPrivileg_Companies Is Nothing Then
                    rfvCompanies.Enabled = True
                    rfvCompanies.InitialValue = ResourceManager.GetString("PleaseSelect", CultureInfo)
                End If
            End If
            If ShowDirectStaffCheck = True Then
                chkDirectStaff.Visible = True
            Else
                chkDirectStaff.Visible = False
            End If
        End If
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Page.UICulture = SessionVariables.CultureInfo

    End Sub

    Protected Sub RadCmbBxCompanies_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxCompanies.SelectedIndexChanged
        If (RadCmbBxCompanies.SelectedValue <> -1) Then
            CompanyId = RadCmbBxCompanies.SelectedValue
        Else
            CompanyId = 0
        End If

        FillEntity()
        RadCmbBxEntity_SelectedIndexChanged(Nothing, Nothing)

        RaiseEvent eventCompanySelect(sender, e)

    End Sub

    Protected Sub RadCmbBxEntity_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxEntity.SelectedIndexChanged
        If RadCmbBxCompanies.Items.Count > 0 Then
            CompanyId = RadCmbBxCompanies.SelectedValue
        End If

        If RadCmbBxEntity.Items.Count > 0 Then
            EntityId = RadCmbBxEntity.SelectedValue
        End If

        RaiseEvent eventEntitySelected(sender, e)
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

    Public Sub FillCompanies()
        'Get user info and check the user security
        'If SessionVariables.LoginUser IsNot Nothing Then
        '    objSYS_Users_Security = New SYS_Users_Security
        '    objSYS_Users_Security.FK_UserId = SessionVariables.LoginUser.ID
        '    objSYS_Users_Security.GetByPK()

        '    If (objSYS_Users_Security.FK_CompanyId <> 0) Then
        '        FillCompanyForUserSecurity()
        '        FillEntity()
        '        CompanyId = RadCmbBxCompanies.SelectedValue
        '    Else
        '        Dim objOrgCompany As New OrgCompany
        '        CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies, objOrgCompany.GetAllforddl, Lang)
        '    End If
        'End If
        If SessionVariables.LoginUser IsNot Nothing Then
            objAPP_Settings = New APP_Settings
            objSYSUsers = New SYSUsers
            objSYSUsers.ID = SessionVariables.LoginUser.ID
            objSYSUsers.GetUser()
            objAPP_Settings.GetByPK()
            If objSYSUsers.UserStatus = 1 Then 'System User - Full 
                Dim objOrgCompany As New OrgCompany
                CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies, objOrgCompany.GetAllforddl, Lang)
            ElseIf objSYSUsers.UserStatus = 2 Then
                If objAPP_Settings.ShowLGWithEntityPrivilege = False Then
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
        RaiseEvent eventCompanySelect(Nothing, Nothing)
    End Sub

    Public Sub FillEntity()

      
        If RadCmbBxCompanies.SelectedValue <> -1 Then
            Dim objProjectCommon = New ProjectCommon()
            objSYSUsers = New SYSUsers
            objSYSUsers.ID = SessionVariables.LoginUser.ID
            objSYSUsers.GetUser()
            If objSYSUsers.UserStatus = 1 Then
                Dim objOrgEntity = New OrgEntity()
                objOrgEntity.FK_CompanyId = RadCmbBxCompanies.SelectedValue
                Dim dt As DataTable = objOrgEntity.GetAllEntityByCompany()

                objProjectCommon.FillMultiLevelRadComboBox(RadCmbBxEntity, dt, "EntityId", _
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
                    'objProjectCommon.FillMultiLevelRadComboBox(RadCmbBxEntity, dtEntity, "EntityId", _
                    '                                      "EntityName", "EntityArabicName", "FK_ParentId")
                    'RadCmbBxEntity.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem(ResourceManager.GetString("PleaseSelect", CultureInfo), -1))
                End If
            Else
                Dim objOrgEntity = New OrgEntity()
                objOrgEntity.FK_CompanyId = RadCmbBxCompanies.SelectedValue
                Dim dt As DataTable = objOrgEntity.GetAllEntityByCompany()

                objProjectCommon.FillMultiLevelRadComboBox(RadCmbBxEntity, dt, "EntityId", _
                                                         "EntityName", "EntityArabicName", "FK_ParentId")
                RadCmbBxEntity.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem(ResourceManager.GetString("PleaseSelect", CultureInfo), -1))
            End If
        End If
    End Sub

    Private Sub FillCompanyForUserSecurity()
        'Dim objOrgCompany As New OrgCompany
        'objOrgCompany.CompanyId = objSYS_Users_Security.FK_CompanyId
        'If objOrgCompany.CompanyId > 0 Then
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

        '    CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies, dtCompanyInfo)
        '    RadCmbBxCompanies.SelectedIndex = 1
        '    RadCmbBxCompanies.Enabled = False
        'End If
        Dim objOrgCompany As New OrgCompany
        Dim dtCompanies As New DataTable

        objOrgCompany.FK_UserId = objSYSUsers.ID
        objOrgCompany.FilterType = "C"
        dtCompanies = objOrgCompany.GetAllforddl_ByUserId

        CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies, dtCompanies, Lang)
    End Sub

    Private Sub CreateEntityTable()
        Dim objOrgEntity = New OrgEntity()
        Dim objProjectCommon = New ProjectCommon()
        objOrgEntity.EntityId = objUserPrivileg_Entities.FK_EntityId
        Dim dt = objOrgEntity.GetEntityAndChilds()

        objProjectCommon.FillMultiLevelRadComboBox(RadCmbBxEntity, dt, "EntityId", _
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
            RadCmbBxCompanies.SelectedIndex = 0
            FillCompanies()
        Else
            EmployeeId = 0
            EntityId = 0
            If (RadCmbBxCompanies.SelectedValue <> -1) Then
                CompanyId = RadCmbBxCompanies.SelectedValue
            Else
                CompanyId = 0
            End If
            RadCmbBxEntity.SelectedIndex = 0
        End If

    End Sub

    Public Sub GetEntity()
        If Not String.IsNullOrEmpty(RadCmbBxCompanies.SelectedValue) Then
            CompanyId = RadCmbBxCompanies.SelectedValue
            If (RadCmbBxEntity.Text <> String.Empty) Then
                EntityId = IIf(RadCmbBxEntity.SelectedValue = -1, 0, RadCmbBxEntity.SelectedValue)
            End If
        End If
    End Sub

    Public Sub ManageControls(ByVal status As Boolean)
        RadCmbBxCompanies.Enabled = status
        RadCmbBxEntity.Enabled = status
    End Sub

    Public Sub SelectByID(ByVal Type As String)
        Select Case Type
            Case "Company"
                RadCmbBxCompanies.SelectedValue = CompanyId
            Case "Entity"
                RadCmbBxEntity.SelectedValue = EntityId
        End Select

    End Sub

#End Region


End Class
