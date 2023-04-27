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

Partial Class Admin_UserControls_MultiEmployeeFilter
    Inherits System.Web.UI.UserControl

#Region "Page Declerations"

    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private objEmployee As Employee
    Private objVersion As SmartV.Version.version
    Dim objUserPrivileg_Entities As UserPrivileg_Entities
    Dim objUserPrivileg_Companies As UserPrivileg_Companies
    Dim objSYSUsers As SYSUsers
    Private objOrgCompany As OrgCompany
    Private objAPP_Settings As APP_Settings

    'RadCmp events select
    Delegate Sub CompanySelectedChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs)
    Public Event eventCompanySelect As CompanySelectedChanged

    Delegate Sub WorkGroupSelectedChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs)
    Public Event eventWorkGroupSelect As WorkGroupSelectedChanged

    Delegate Sub EntitySelectedChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs)
    Public Event eventEntitySelected As EntitySelectedChanged

    Delegate Sub WorkLocationSelectedChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs)
    Public Event eventWorkLocationsSelected As WorkLocationSelectedChanged

#End Region

#Region "Page Propreties"

    Public Property EntityID() As Integer
        Get
            Return ViewState("EntityID")
        End Get
        Set(ByVal value As Integer)
            ViewState("EntityID") = value
        End Set
    End Property

    Public Property WorkGroupID() As Integer
        Get
            Return ViewState("WorkGroupID")
        End Get
        Set(ByVal value As Integer)
            ViewState("WorkGroupID") = value
        End Set
    End Property

    Public Property WorkLocationsID() As Integer
        Get
            Return ViewState("WorkLocationsID")
        End Get
        Set(ByVal value As Integer)
            ViewState("WorkLocationsID") = value
        End Set
    End Property

    Public Property EmployeeNo() As String
        Get
            Return txtEmpNo.Text
        End Get
        Set(ByVal value As String)
            txtEmpNo.Text = value
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

    Public Property EmployeeID() As Integer
        Get
            Return ViewState("EmployeeID")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeID") = value
        End Set
    End Property

    Public Property SearchType() As String
        Get
            Return rlsSearchCriteria.SelectedValue
        End Get
        Set(ByVal value As String)
            rlsSearchCriteria.SelectedValue = value
        End Set
    End Property

    Public Property ShowOtherCompany() As Boolean
        Get
            Return ViewState("ShowOtherCompany")
        End Get
        Set(ByVal value As Boolean)
            ViewState("ShowOtherCompany") = value
        End Set
    End Property

    Public Property ShowDirectStaffCheck() As Boolean
        Get
            Return ViewState("ShowDirectStaffCheck")
        End Get
        Set(ByVal value As Boolean)
            ViewState("ShowDirectStaffCheck") = value
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

#End Region

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        'If Not IsPostBack Then
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        ElseIf SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
        Else
            Lang = CtlCommon.Lang.EN
        End If
        'End If
        If Not Page.IsPostBack Then
            'FillCompany()
            If (objVersion.HasMultiCompany() = False) Then
                RadCmbBxCompany.SelectedValue = objVersion.GetCompanyId()
                RadCmbBxCompany_SelectedIndexChanged(Nothing, Nothing)
                trCompany.Visible = False
            Else
                trCompany.Visible = ShowOtherCompany
            End If
            If ShowDirectStaffCheck = True Then
                chkDirectStaff.Visible = True
            Else
                chkDirectStaff.Visible = False
            End If

            objSYSUsers = New SYSUsers
            objSYSUsers.ID = SessionVariables.LoginUser.ID
            objSYSUsers.GetUser()
            If objSYSUsers.UserStatus = 2 Then

                If objSYSUsers.HasEntity_Privilege = False Then
                    rlsSearchCriteria.Items.Remove(rlsSearchCriteria.Items.FindByValue(1))
                End If

                If objSYSUsers.HasWorkLocation_Privilege = False Then
                    rlsSearchCriteria.Items.Remove(rlsSearchCriteria.Items.FindByValue(3)) '---WL
                End If

                If objSYSUsers.HasLogicalGroup_Privilege = False Then
                    rlsSearchCriteria.Items.Remove(rlsSearchCriteria.Items.FindByValue(2)) '---LG
                End If

                rlsSearchCriteria.Items(0).Selected = True
            End If

            If objSYSUsers.UserStatus = 3 Then
                rlsSearchCriteria.Items.RemoveAt(1)
            End If


        End If

        HideShowControls()
    End Sub

    Protected Sub rlsSearchCriteria_SelectedIndexChanged(ByVal sende As Object, ByVal e As EventArgs) Handles rlsSearchCriteria.SelectedIndexChanged
        If rlsSearchCriteria.SelectedIndex < 3 Then
            EmployeeID = 0

        End If
        HideShowControls()
    End Sub

    Protected Sub btnRetrieve_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRetrieve.Click

        objEmployee = New Employee()
        objAPP_Settings = New APP_Settings
        With objAPP_Settings
            .GetByPK()

            objEmployee.EmployeeNo = txtEmpNo.Text
            objEmployee.UserId = SessionVariables.LoginUser.ID
            If objVersion.HasMultiCompany Then
                If CompanyID = Nothing Then
                    objEmployee.FK_CompanyId = RadCmbBxCompany.SelectedValue
                Else
                    objEmployee.FK_CompanyId = CompanyID
                End If
            Else
                objEmployee.FK_CompanyId = objVersion.GetCompanyId
            End If
            Dim dtEmployee As DataTable = objEmployee.GetEmpByEmpNo()
            If dtEmployee.Rows.Count > 0 Then
                EmployeeID = dtEmployee.Rows(0)("EmployeeId")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EmpNonotinlevel", CultureInfo), "info")
            End If

            RaiseEvent eventEntitySelected(Nothing, Nothing)

        End With

    End Sub

    Protected Sub RadCmbBxCompany_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadCmbBxCompany.SelectedIndexChanged

        CompanyID = RadCmbBxCompany.SelectedValue
        RaiseEvent eventCompanySelect(sender, e)

    End Sub

    Protected Sub RadCmbBxEntity_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadCmbBxEntity.SelectedIndexChanged

        EntityID = RadCmbBxEntity.SelectedValue
        RaiseEvent eventEntitySelected(sender, e)

    End Sub

    Protected Sub RadCmbBxWorkGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadCmbBxWorkGroup.SelectedIndexChanged

        WorkGroupID = RadCmbBxWorkGroup.SelectedValue
        RaiseEvent eventWorkGroupSelect(sender, e)

    End Sub

    Protected Sub RadCmbBxWorkLocations_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadCmbBxWorkLocations.SelectedIndexChanged

        WorkLocationsID = RadCmbBxWorkLocations.SelectedValue
        RaiseEvent eventWorkLocationsSelected(sender, e)

    End Sub

    Protected Sub chkDirectStaff_CheckedChanged(sender As Object, e As EventArgs) Handles chkDirectStaff.CheckedChanged
        If chkDirectStaff.Checked = True Then
            DirectStaffOnly = True
        Else
            DirectStaffOnly = False
        End If
    End Sub

#End Region

#Region "Page Methods"

    Public Sub FillCompany()
        objOrgCompany = New OrgCompany
        With objOrgCompany
            CtlCommon.FillTelerikDropDownList(RadCmbBxCompany, .GetAll, Lang)
        End With
    End Sub

    Sub HideShowControls()


        If rlsSearchCriteria.SelectedValue = "1" Then
            trEntity.Visible = True
            trWorkGroup.Visible = False
            trWorkLocation.Visible = False
            trEmployee.Visible = False
        ElseIf rlsSearchCriteria.SelectedValue = "2" Then
            trEntity.Visible = False
            trWorkGroup.Visible = True
            trWorkLocation.Visible = False
            trEmployee.Visible = False
        ElseIf rlsSearchCriteria.SelectedValue = "3" Then
            trEntity.Visible = False
            trWorkGroup.Visible = False
            trWorkLocation.Visible = True
            trEmployee.Visible = False
        ElseIf rlsSearchCriteria.SelectedValue = "4" Then
            trEntity.Visible = False
            trWorkGroup.Visible = False
            trWorkLocation.Visible = False
            trEmployee.Visible = True
        Else
            trEntity.Visible = False
            trWorkGroup.Visible = False
            trWorkLocation.Visible = False
            trEmployee.Visible = False
        End If

    End Sub

    Private Sub FillEntity()
        If CompanyID <> 0 Then
            Dim objProjectCommon = New ProjectCommon()
            objSYSUsers = New SYSUsers
            objSYSUsers.ID = SessionVariables.LoginUser.ID
            objSYSUsers.GetUser()
            If objSYSUsers.UserStatus = 1 Then
                Dim objOrgEntity = New OrgEntity()
                objOrgEntity.FK_CompanyId = CompanyID
                Dim dt As DataTable = objOrgEntity.GetAllEntityByCompany()

                objProjectCommon.FillMultiLevelRadComboBox(RadCmbBxEntity, dt, "EntityId", _
                                                         "EntityName", "EntityArabicName", "FK_ParentId")
                RadCmbBxEntity.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem(ResourceManager.GetString("PleaseSelect", CultureInfo), -1))
            ElseIf objSYSUsers.UserStatus = 2 Then
                Dim objOrgEntity = New OrgEntity()
                Dim dtEntity As New DataTable
                objOrgEntity.FK_CompanyId = CompanyID
                objOrgEntity.FK_UserId = objSYSUsers.ID
                dtEntity = objOrgEntity.GetAllEntityByCompanyAndByUserId
                If DTable.IsValidDataTable(dtEntity) Then
                    CtlCommon.FillTelerikDropDownList(RadCmbBxEntity, dtEntity, Lang)
                End If
            Else
                Dim objOrgEntity = New OrgEntity()
                objOrgEntity.FK_CompanyId = CompanyID
                Dim dt As DataTable = objOrgEntity.GetAllEntityByCompany()

                objProjectCommon.FillMultiLevelRadComboBox(RadCmbBxEntity, dt, "EntityId", _
                                                         "EntityName", "EntityArabicName", "FK_ParentId")
                RadCmbBxEntity.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem(ResourceManager.GetString("PleaseSelect", CultureInfo), -1))
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
            If objSYSUsers.UserStatus = 1 Then ' Full, System User
                If CompanyID > 0 Then
                    objEmp_WorkLocation.FK_CompanyId = CompanyID
                    dtWorkLoc = objEmp_WorkLocation.GetAllByCompany()
                    CtlCommon.FillTelerikDropDownList(RadCmbBxWorkLocations, dtWorkLoc, Lang)
                End If
            ElseIf objSYSUsers.UserStatus = 2 Then
                If CompanyID > 0 Then
                    objEmp_WorkLocation.FK_CompanyId = CompanyID
                    objEmp_WorkLocation.FK_UserId = objSYSUsers.ID
                    dtWorkLoc = objEmp_WorkLocation.GetAllByCompanyAndUserId()
                    CtlCommon.FillTelerikDropDownList(RadCmbBxWorkLocations, dtWorkLoc, Lang)
                End If
            Else
                If CompanyID > 0 Then
                    objEmp_WorkLocation.FK_CompanyId = CompanyID
                    dtWorkLoc = objEmp_WorkLocation.GetAllByCompany()
                    CtlCommon.FillTelerikDropDownList(RadCmbBxWorkLocations, dtWorkLoc, Lang)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FillLogicalGroups()
        Try
            Dim objEmpLogicalGroup = New Emp_logicalGroup()
            Dim dt As New DataTable
            objSYSUsers = New SYSUsers
            objSYSUsers.ID = SessionVariables.LoginUser.ID
            objSYSUsers.GetUser()
            If objSYSUsers.UserStatus = 1 Then ' Full, System User
                If CompanyID > 0 Then
                    objEmpLogicalGroup.FK_CompanyId = CompanyID
                    dt = objEmpLogicalGroup.GetAllByCompany()
                    CtlCommon.FillTelerikDropDownList(RadCmbBxWorkGroup, dt, Lang)
                End If
            ElseIf objSYSUsers.UserStatus = 2 Then
                If CompanyID > 0 Then
                    objEmpLogicalGroup.FK_CompanyId = CompanyID
                    objEmpLogicalGroup.FK_UserId = objSYSUsers.ID
                    dt = objEmpLogicalGroup.GetAllByCompanyAndUserId()
                    CtlCommon.FillTelerikDropDownList(RadCmbBxWorkGroup, dt, Lang)
                End If
            Else
                If CompanyID > 0 Then
                    objEmpLogicalGroup.FK_CompanyId = CompanyID
                    dt = objEmpLogicalGroup.GetAllByCompany()
                    CtlCommon.FillTelerikDropDownList(RadCmbBxWorkGroup, dt, Lang)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub FillList()
        If Not CompanyID = 0 Then
            FillEntity()
            FillLogicalGroups()
            FillWorkLocations()
        End If
    End Sub

    Public Sub ClearValues()
        rlsSearchCriteria.ClearSelection()
        HideShowControls()
        EntityID = 0
        WorkGroupID = 0
        WorkLocationsID = 0
        EmployeeID = 0
        txtEmpNo.Text = String.Empty
        CompanyID = 0
    End Sub

#End Region


End Class
