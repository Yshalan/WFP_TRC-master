Imports SmartV.UTILITIES
Imports System.Data
Imports SmartV.Security
Imports SmartV.DB
Imports System.Resources
Imports System.Reflection
Imports System.Threading
Imports Telerik.Web.UI
Imports TA.Lookup
Imports TA.Admin
Imports TA.Employees
Imports TA.DailyTasks
Imports TA.Security

Partial Class UserControls_OrgEntity
    Inherits System.Web.UI.UserControl

#Region "Declarations"

    Private objOrgEntity As OrgEntity
    Private objOrgCompany As New OrgCompany
    Private objTAPolicy As New TAPolicy
    Private objOrgLevel As New OrgLevel
    Private objEmp_Designation As New Emp_Designation
    Private _DisplayMode As DisplayModeEnum
    Private objEmployee As Employee
    Private objEmployee_Manager As Employee_Manager
    Dim dtForParent As New DataTable
    Public Event FillTreeOrganization()
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Public MsgLang As String

#End Region

#Region "Properties"

    Public Enum Locations
        TreeOrgn
    End Enum

    Public Enum DisplayModeEnum
        Add
        Edit
        View
        ViewAll
        ViewAddEdit
    End Enum

    Public Property DisplayMode() As DisplayModeEnum
        Get
            Return _DisplayMode
        End Get
        Set(ByVal value As DisplayModeEnum)
            _DisplayMode = value
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

    Public Property Location() As Locations
        Get
            Return ViewState("Location")
        End Get
        Set(ByVal value As Locations)
            ViewState("Location") = value
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

    Public Property LevelID() As Integer
        Get
            Return ViewState("LevelID")
        End Get
        Set(ByVal value As Integer)
            ViewState("LevelID") = value
        End Set
    End Property

    Public Property ParentID() As Integer
        Get
            Return ViewState("ParentID")
        End Get
        Set(ByVal value As Integer)
            ViewState("ParentID") = value
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

    Public Property AllowSave() As Boolean
        Get
            Return ViewState("AllowSave")
        End Get
        Set(ByVal value As Boolean)
            ViewState("AllowSave") = value
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

    Public Property EmployeeId() As Integer
        Get
            Return ViewState("EmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeId") = value
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

    Public Property JoinDate() As DateTime
        Get
            Return ViewState("JoinDate")
        End Get
        Set(ByVal value As DateTime)
            ViewState("JoinDate") = value
        End Set
    End Property

    Public Property ParentManagerId() As Integer
        Get
            Return ViewState("ParentManagerId")
        End Get
        Set(ByVal value As Integer)
            ViewState("ParentManagerId") = value
        End Set
    End Property

#End Region

#Region "Page events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If SessionVariables.CultureInfo = "ar-JO" Then
            MsgLang = "ar"
        Else
            MsgLang = "en"
        End If
        Entitypageload()
    End Sub

    Protected Sub ibtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        SaveUpdate()

    End Sub

    Protected Sub ibtnRest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnRest.Click
        clearall()
    End Sub

    Protected Sub ibtnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnDelete.Click
        Dim err As Integer
        If Not DisplayMode = DisplayModeEnum.ViewAddEdit Then
            objOrgEntity = New OrgEntity
            With objOrgEntity
                .EntityId = EntityId
                err = .Delete()
            End With

        ElseIf DisplayMode = DisplayModeEnum.ViewAddEdit Then
            For Each row As GridDataItem In dgrdOrg_Entity.SelectedItems
                Dim intcompanyIDId As Integer = Convert.ToInt32(row.GetDataKeyValue("EntityId").ToString())
                objOrgEntity = New OrgEntity
                With objOrgEntity
                    .EntityId = intcompanyIDId
                    err = .Delete()
                End With
            Next

        End If
        If err = 0 Then
            clearall()
            If DisplayMode = DisplayModeEnum.ViewAddEdit Then
                fillgridview()
            End If
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EntityCouldnotDeleted", CultureInfo), "error")
        End If

        RaiseEvent FillTreeOrganization()
    End Sub

    'Protected Sub ddlCompany_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlCompany.SelectedIndexChanged
    '    fillleveldropdowns()
    'End Sub

    Protected Sub dgrdOrg_Entity_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdOrg_Entity.NeedDataSource
        objOrgEntity = New OrgEntity
        dgrdOrg_Entity.DataSource = objOrgEntity.GetAll
    End Sub

    Protected Sub dgrdOrg_Entity_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdOrg_Entity.SelectedIndexChanged

        EntityId = CInt(DirectCast(dgrdOrg_Entity.SelectedItems(0), GridDataItem).GetDataKeyValue("EntityId").ToString().Trim)
        ManageDeleteButton()
        fillcontrolsforediting()
    End Sub

    Protected Sub btnRetrieve_Click(sender As Object, e As System.EventArgs) Handles btnRetrieve.Click
        objEmployee = New Employee()
        objEmployee.FK_CompanyId = CompanyID
        objEmployee.EmployeeNo = txtEmpNo.Text
        objEmployee.UserId = SessionVariables.LoginUser.ID
        Dim dtEmployee As DataTable = objEmployee.GetEmpByEmpNo()
        If dtEmployee.Rows.Count > 0 Then
            ManagerId = dtEmployee.Rows(0)("EmployeeId")
            objEmployee.EmployeeId = ManagerId
            objEmployee.GetByPK()
            If SessionVariables.CultureInfo = "ar-JO" Then
                lblManager.Text = objEmployee.EmployeeArabicName
            Else
                lblManager.Text = objEmployee.EmployeeName
            End If
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EmpNonotinlevel", CultureInfo), "info")
            lblManager.Text = String.Empty
            ManagerId = 0
            txtEmpNo.Text = String.Empty
        End If
    End Sub

#End Region

#Region "Methods"

    Sub Entitypageload()
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            End If
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            PageHeader1.HeaderText = ResourceManager.GetString("OrgEntity", CultureInfo)
            filldropdowns()
            fillgridview()
            ManageFunctionalities()

        End If
        If checkBxHasParent.Checked = True Then
            lblComanyParent.Visible = True
            txtParent.Visible = True
            'RequiredFieldValidator6.Enabled = True
        Else
            lblComanyParent.Visible = False
            txtParent.Visible = False
            'RequiredFieldValidator6.Enabled = False
        End If
        If Location = Locations.TreeOrgn Then
            checkBxHasParent.Visible = False
            PageHeader1.Visible = False
            lblHasParent.Visible = False

        End If

        ibtnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdOrg_Entity.ClientID + "')")

    End Sub

    Sub Adddisplaymode()
        RefreshControls(True)
        dgrdOrg_Entity.Visible = False
        ibtnDelete.Visible = False

        ibtnRest.Visible = False
        ReqHighestPost.Enabled = False
    End Sub

    Sub Viewdisplaymode()
        RefreshControls(True)
        ManageControls(False)
        ibtnDelete.Visible = False

        ibtnRest.Visible = False
        ibtnSave.Visible = False
        fillcontrolsforediting()
        dgrdOrg_Entity.Visible = False
    End Sub

    Sub ViewAllDisplaymode()
        RefreshControls(True)
        ManageControls(False)
        ibtnDelete.Visible = False

        ibtnRest.Visible = False
        ibtnSave.Visible = False
        dgrdOrg_Entity.AllowMultiRowSelection = False
        dgrdOrg_Entity.Columns(0).Visible = False
    End Sub

    Sub ViewEditmode()
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        RefreshControls(True)
        ibtnSave.Text = ResourceManager.GetString("btnUpdate", CultureInfo)
        'ibtnSave.Text = "Update"
        fillcontrolsforediting()
        dgrdOrg_Entity.Visible = False
        ibtnRest.Visible = False
        ibtnDelete.Visible = False
        ManageDeleteButton()
        ReqHighestPost.Enabled = False
    End Sub

    Sub ViewAddEditDisplaymode()
        RefreshControls(True)
        ReqHighestPost.Enabled = True
        ManageDeleteButton()
    End Sub

    Sub ManageControls(ByVal Status As Boolean)
        txtArName.Enabled = Status
        txtEnName.Enabled = Status
        txtEntityCode.Enabled = Status
        txtCompany.Enabled = Status
        txtLevel.Enabled = Status
        'ddlCompany.Enabled = Status
        'ddlLevel.Enabled = Status
        txtParent.Enabled = Status
        ddlDefaultPolicy.Enabled = Status
        ddlHighestPost.Enabled = Status
        checkBxHasParent.Enabled = Status
        'ddlCompany.Enabled = Status
        'ddlLevel.Enabled = Status
    End Sub

    Sub RefreshControls(ByVal Status As Boolean)
        txtArName.Enabled = Status
        txtEnName.Enabled = Status
        txtEntityCode.Enabled = Status
        txtCompany.Enabled = Status
        txtLevel.Enabled = Status
        'ddlCompany.Enabled = Status
        'ddlLevel.Enabled = Status
        txtParent.Enabled = Status
        ddlDefaultPolicy.Enabled = Status
        ddlHighestPost.Enabled = Status
        checkBxHasParent.Enabled = Status
        'ddlCompany.Enabled = Status
        'ddlLevel.Enabled = Status
        ibtnSave.Text = ResourceManager.GetString("btnSave", CultureInfo)
        ibtnDelete.Visible = Status

        ibtnRest.Visible = Status
        If AllowSave AndAlso Status = True Then
            ibtnSave.Visible = Status
        ElseIf Status = False Then
            ibtnSave.Visible = Status
        End If

        If AllowDelete AndAlso Status = True Then
            ibtnDelete.Enabled = Status
        ElseIf Status = False Then
            ibtnDelete.Enabled = Status
        End If

        ibtnRest.Enabled = Status
        ibtnSave.Enabled = Status
        dgrdOrg_Entity.Visible = Status
        For Each row As GridDataItem In dgrdOrg_Entity.Items
            row.Enabled = Status
        Next


    End Sub

    Sub ManageDeleteButton()
        If Not EntityId > 0 Then
            Exit Sub
        End If
        objOrgEntity = New OrgEntity
        With objOrgEntity
            .EntityId = EntityId
            Select Case .CheckChildEntityexists
                Case 1
                    ibtnDelete.Visible = False
                Case 0
                    ibtnDelete.Visible = True
                Case Else
            End Select
        End With
    End Sub

    Sub ManageFunctionalities()
        Select Case DisplayMode.ToString
            Case "Add"
                Adddisplaymode()
                EntityId = 0
            Case "Edit"
                ViewEditmode()
            Case "View"
                Viewdisplaymode()
            Case "ViewAll"
                ViewAllDisplaymode()
                EntityId = 0
            Case "ViewAddEdit"
                ViewAddEditDisplaymode()
                EntityId = 0
            Case Else
        End Select
    End Sub

    Sub fillgridview()
        objOrgEntity = New OrgEntity

        dgrdOrg_Entity.DataSource = objOrgEntity.GetAll
        dgrdOrg_Entity.DataBind()
    End Sub

    Sub filldropdowns()
        CtlCommon.FillTelerikDropDownList(ddlDefaultPolicy, objTAPolicy.GetAll)
        CtlCommon.FillTelerikDropDownList(ddlHighestPost, objEmp_Designation.GetAll)
        'CtlCommon.FillTelerikDropDownList(ddlCompany, objOrgCompany.GetAllforddl)
        '  fillleveldropdowns()
    End Sub

    Sub fillleveldropdowns()
        Dim dt As DataTable
        objOrgLevel = New OrgLevel
        With objOrgLevel
            .FK_CompanyId = CompanyID
            .LevelId = LevelID
            dt = .GetAllByCompanyAndLevel
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then txtLevel.Text = dt(0)("LevelName").ToString
        End With
    End Sub

    'Sub fillParent()
    '    objOrgEntity = New OrgEntity
    '    Dim dt As DataTable = objOrgEntity.GetAllForDDL
    '    CtlCommon.FillTelerikDropDownList(ddlBxParentEntity, dt)
    '    If EntityId > 0 Then
    '        ddlBxParentEntity.Items.Remove(ddlBxParentEntity.Items.FindItemByValue(EntityId))
    '        FilterParentddl()
    '    End If


    'End Sub
    'Sub FilterParentddl()
    '    objOrgEntity = New OrgEntity
    '    Dim dt As New DataTable
    '    dt = objOrgEntity.GetAllForDDL()
    '    dtForParent = dt.Clone
    '    getChilds(EntityId)
    '    If dtForParent IsNot Nothing Then
    '        If dtForParent.Rows.Count > 0 Then
    '            For Each dr As DataRow In dtForParent.Rows
    '                ddlBxParentEntity.Items.Remove(ddlBxParentEntity.Items.FindItemByValue(dr("EntityId")))
    '            Next
    '        End If
    '    End If
    'End Sub

    Sub getChilds(ByVal intEntityId As Integer)
        objOrgEntity = New OrgEntity
        Dim dt As New DataTable
        dt = objOrgEntity.GetAllForDDL()
        If Not dt Is Nothing And dt.Rows.Count > 0 Then
            Dim ChildClasses() As DataRow = dt.Select("FK_ParentId =" & intEntityId)
            For Each r As DataRow In ChildClasses
                dtForParent.ImportRow(r)
                getChilds(r("EntityId"))

            Next
        End If
    End Sub

    Sub fillcontrolsforediting()
        objOrgEntity = New OrgEntity

        If EntityId > 0 Then
            With objOrgEntity
                .EntityId = EntityId
                .GetByPK()
                txtArName.Text = .EntityArabicName
                txtEnName.Text = .EntityName
                txtEntityCode.Text = .EntityCode
                CompanyID = .FK_CompanyId
                LevelID = .FK_LevelId
                ParentID = .FK_ParentId
                txtCompany.Text = New OrgCompany() With {.CompanyId = CompanyID}.GetCompanyNameByID()
                '----Fill Manager Name-----'
                ManagerId = .FK_ManagerId
                objEmployee = New Employee
                objEmployee.EmployeeId = ManagerId
                objEmployee.GetByPK()
                txtEmpNo.Text = objEmployee.EmployeeNo
                If SessionVariables.CultureInfo = "ar-JO" Then
                    lblManager.Text = objEmployee.EmployeeArabicName
                Else
                    lblManager.Text = objEmployee.EmployeeName
                End If
                '----Fill Manager Name-----'

                ddlDefaultPolicy.SelectedValue = .FK_DefaultPolicyId
                ddlHighestPost.SelectedValue = .FK_HighestPost
                If Not IsDBNull(.FK_ParentId) And .FK_ParentId <> 0 Then
                    checkBxHasParent.Checked = True
                    lblComanyParent.Visible = True
                    txtParent.Visible = True
                    txtParent.Text = .GetParentNameByEntityID
                    'RequiredFieldValidator6.Enabled = True
                Else
                    checkBxHasParent.Checked = False
                    lblComanyParent.Visible = False
                    txtParent.Visible = False
                    txtParent.Text = ""
                    'RequiredFieldValidator6.Enabled = False
                End If
            End With
        End If
    End Sub

    Sub SaveUpdate()
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim ERRNO As Integer = -1
        objOrgEntity = New OrgEntity
        With objOrgEntity
            .EntityArabicName = txtArName.Text
            .EntityName = txtEnName.Text
            .EntityCode = txtEntityCode.Text
            .CREATED_BY = SessionVariables.LoginUser.ID 'SessionVariables.LoginUser.fullName
            .CREATED_DATE = Now
            .FK_CompanyId = CompanyID   '.FK_CompanyId = ddlCompany.SelectedValue
            .FK_LevelId = LevelID  '.FK_LevelId = ddlLevel.SelectedValue
            .LAST_UPDATE_DATE = Now

            .FK_DefaultPolicyId = ddlDefaultPolicy.SelectedValue
            .FK_HighestPost = ddlHighestPost.SelectedValue
            .FK_ManagerId = ManagerId
            If checkBxHasParent.Checked = True Then
                .FK_ParentId = ParentID
            Else
                .FK_ParentId = 0
            End If

            If EntityId = 0 Then
                .EntityId = 0
                .LAST_UPDATE_BY = ""
                ERRNO = .Add()
                If ERRNO = 0 Then
                    EntityId = .EntityId
                    Select Case DisplayMode.ToString
                        Case "Add"
                            ParentID = .FK_ParentId
                        Case Else
                            clearall()
                    End Select
                End If
            Else
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID 'SessionVariables.LoginUser.fullName
                .EntityId = EntityId
                ERRNO = .Update()
                If ERRNO = 0 Then

                    ParentID = .FK_ParentId
                End If
            End If

        End With
        If ERRNO = 0 Then
            UpdateEmpManager()
            'AssignManager_EntityManagers_UpWard()
            'AssignManager_EntityManagers_DownWard()

            Select Case DisplayMode.ToString
                Case "ViewAddEdit"
                    fillgridview()
            End Select
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            RaiseEvent FillTreeOrganization()
        ElseIf ERRNO = -11 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EntityNameExist", CultureInfo), "info")
        ElseIf ERRNO = -12 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EntityCodeExist", CultureInfo), "info")
        ElseIf ERRNO = -13 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("MismatchCompany", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
    End Sub

    Sub clearall()
        txtArName.Text = ""
        txtEnName.Text = ""
        txtEntityCode.Text = ""
        ddlDefaultPolicy.SelectedIndex = -1
        txtCompany.Text = ""
        'ddlCompany.SelectedValue = -1
        fillleveldropdowns()
        ddlHighestPost.SelectedValue = -1
        EntityId = 0
        txtParent.Text = ""
        dgrdOrg_Entity.SelectedIndexes.Clear()
        txtEmpNo.Text = String.Empty
        lblManager.Text = String.Empty
        ManagerId = 0
        EmployeeId = 0
        ParentManagerId = 0
    End Sub

    Public Sub ManagedByTree(ByVal Type As String, ByVal intParent As Integer, ByVal intCompany As Integer, ByVal intlevel As Integer)
        Select Case Type
            Case "men"
                ViewEditmode()
                txtCompany.Enabled = False
                fillleveldropdowns()
                txtLevel.Enabled = False
                checkBxHasParent.Enabled = False
                txtParent.Enabled = False
            Case "ame"
                ibtnSave.Text = "Save"
                checkBxHasParent.Checked = False
                checkBxHasParent.Enabled = False
                lblComanyParent.Visible = False
                txtParent.Visible = False
                'CtlCommon.FillTelerikDropDownList(ddlCompany, objOrgCompany.GetAllforddl)
                'txtCompany.Text = intCompany 'ddlCompany.SelectedValue = intCompany
                txtCompany.Text = New OrgCompany() With {.CompanyId = intCompany}.GetCompanyNameByID()
                CompanyID = intCompany
                LevelID = intlevel
                txtCompany.Enabled = False  'ddlCompany.Enabled = False
                fillleveldropdowns()
                'FillLevel(intCompany)
                ParentID = 0
                'ddlLevel.SelectedValue = intlevel
                txtLevel.Enabled = False 'ddlLevel.Enabled = False

                If LevelID = 1 Then
                    ddlDefaultPolicy.SelectedValue = New OrgEntity() With {.FK_CompanyId = CompanyID}.GetDefaultPolicy
                End If

            Case "ace"
                objOrgEntity = New OrgEntity
                ibtnSave.Text = "Save"
                checkBxHasParent.Checked = True
                checkBxHasParent.Enabled = False
                lblComanyParent.Visible = True
                txtParent.Visible = True
                txtParent.Enabled = False
                ParentID = intParent
                objOrgEntity.EntityId = intParent
                CompanyID = objOrgEntity.GetByPK.FK_CompanyId
                LevelID = intlevel
                txtCompany.Text = New OrgCompany() With {.CompanyId = CompanyID}.GetCompanyNameByID
                txtCompany.Enabled = False 'ddlCompany.Enabled = False
                fillleveldropdowns()
                ''ddlLevel.SelectedValue = intlevel

                If (intParent <> 0) Then
                    With objOrgEntity
                        .EntityId = EntityId
                        .GetByPK()
                        ddlDefaultPolicy.SelectedValue = .FK_DefaultPolicyId
                    End With
                End If
                ' FillLevel(CompanyID)
                txtLevel.Enabled = False 'ddlLevel.Enabled = False
                txtParent.Text = objOrgEntity.GetByPK.EntityName
                'ddlDefaultPolicy.SelectedValue = objOrgEntity.FK_DefaultPolicyId

        End Select

    End Sub

    Public Sub UpdateEmpManager()
        objEmployee_Manager = New Employee_Manager
        objOrgEntity = New OrgEntity
        objEmployee = New Employee
        Dim dt As DataTable

        objEmployee.FK_EntityId = EntityId
        objEmployee.FK_CompanyId = CompanyID
        dt = objEmployee.GetByEntityId()
        If Not ManagerId = 0 Then
            For Each row In dt.Rows
                EmployeeId = row("EmployeeId")
                JoinDate = row("JoinDate")
                objEmployee_Manager.FK_ManagerId = ManagerId
                objEmployee_Manager.FK_EmployeeId = EmployeeId
                If JoinDate = DateTime.MinValue Or JoinDate = Nothing Then
                    objEmployee_Manager.FromDate = Date.Today
                Else
                    objEmployee_Manager.FromDate = JoinDate
                End If
                objEmployee_Manager.ToDate = Nothing
                objEmployee_Manager.IsTemporary = False
                objEmployee_Manager.Assign_EmployeesManager()
            Next
        End If

    End Sub

    Private Sub AssignManager_EntityManagers_DownWard()
        objOrgEntity = New OrgEntity
        objEmployee_Manager = New Employee_Manager
        objEmployee = New Employee
        Dim dt As DataTable

        With objOrgEntity
            .EntityId = EntityId
            dt = .GetChildManager_ByEntityId() 'Get Child Entites Managers
        End With

        With objEmployee_Manager
            ParentManagerId = ManagerId 'ManagerId From the Interface will be the parent manager
            If dt.Rows.Count > 0 Then
                For Each row In dt.Rows
                    EmployeeId = row("FK_ManagerId") 'Set Child Managers to be as Employees
                    .FK_ManagerId = ParentManagerId ' Set Parent Manager To be assigned for Employees
                    If Not EmployeeId = 0 Then
                        .FK_EmployeeId = EmployeeId
                        objEmployee.EmployeeId = EmployeeId
                        objEmployee.GetByPK()
                        If objEmployee.JoinDate = DateTime.MinValue Or objEmployee.JoinDate = Nothing Then
                            .FromDate = Date.Today
                        Else
                            .FromDate = objEmployee.JoinDate
                        End If
                        .ToDate = Nothing
                        .IsTemporary = False
                        .Assign_EmployeesManager()
                    End If
                Next
            End If
        End With
    End Sub

    Private Sub AssignManager_EntityManagers_UpWard()
        objOrgEntity = New OrgEntity
        objEmployee_Manager = New Employee_Manager
        objEmployee = New Employee

        With objOrgEntity
            .EntityId = EntityId
            .GetParentManager_ByEntityId()
            ParentManagerId = .FK_ManagerId
        End With
        If Not ParentManagerId = 0 Then
            With objEmployee_Manager
                .FK_ManagerId = ParentManagerId
                .FK_EmployeeId = ManagerId
                objEmployee.EmployeeId = ManagerId
                objEmployee.GetByPK()
                If objEmployee.JoinDate = DateTime.MinValue Or objEmployee.JoinDate = Nothing Then
                    objEmployee_Manager.FromDate = Date.Today
                Else
                    objEmployee_Manager.FromDate = objEmployee.JoinDate
                End If
                objEmployee_Manager.ToDate = Nothing
                objEmployee_Manager.IsTemporary = False
                objEmployee_Manager.Assign_EmployeesManager()
            End With
        End If
    End Sub
#End Region

    'Public Sub FillLevel(ByVal companyId As Integer)
    '    Dim dt As DataTable
    '    'Dim levelID As Integer
    '    objOrgLevel = New OrgLevel
    '    With objOrgLevel
    '        .FK_CompanyId = companyId
    '        dt = .GetAllByComapany
    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then txtLevel.Text = dt(0)("LevelName").ToString

    '    End With
    'End Sub
    'Public Sub FillparentName()
    '    objOrgEntity = New OrgEntity
    '    Dim dt As DataTable
    '    With objOrgEntity
    '        .EntityId = EntityId
    '        dt = .GetParentNameByEntityID
    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            txtParent.Text = dt(0)("ParentName").ToString
    '        End If

    '    End With
    'End Sub

    
End Class
