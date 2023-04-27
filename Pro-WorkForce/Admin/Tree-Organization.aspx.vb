Imports System.Data
Imports TA.Lookup
Imports TA.Admin
Imports TA.DailyTasks
Imports System.Drawing
Imports Telerik.Web.UI
Imports SmartV.UTILITIES
Imports SmartV.Version
Imports TA.Security
Imports TA.Employees

Partial Class Admin_Tree_Organization
    Inherits System.Web.UI.Page

#Region "Declaration"

    Private objOrgCompany As New OrgCompany
    Private objOrgEntity As New OrgEntity
    Dim dtCompanies As DataTable
    Dim dtEntities As DataTable
    Private objOrgLevel As New OrgLevel
    Dim dtLevels As DataTable
    Dim dtAllLevels As DataTable
    Dim dtAllEntities As DataTable
    Private Lang As CtlCommon.Lang
    Private objVersion As SmartV.Version.version
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo

#End Region

#Region "Propereties"

    Public Property dtCurrent() As DataTable
        Get
            Return ViewState("dtCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
        End Set
    End Property

#Region "Enum"

    Public Enum DisplayModeEnum
        Add
        Edit
        View
        ViewAll
        ViewAddEdit
    End Enum

#End Region

#End Region

#Region "Page Events"

    Protected Sub OrgCompany_FillTreeOrganization() Handles OrgCompany.FillTreeOrganization
        filltree()
        OrgCompany.clearall()
        OrgCompany.Visible = False
    End Sub

    Protected Sub OrgEntity_FillTreeOrganization() Handles OrgEntity.FillTreeOrganization
        filltree()
        OrgEntity.clearall()
        OrgEntity.Visible = False
    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        Else
            If SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                Page.MasterPageFile = "~/default/ArabicMaster.master"
            Else
                Lang = CtlCommon.Lang.EN
                Page.MasterPageFile = "~/default/NewMaster.master"
            End If
            Page.UICulture = SessionVariables.CultureInfo
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            PageHeader1.HeaderText = ResourceManager.GetString("OrgStructure", CultureInfo)
            filltree()
        End If

        OrgCompany.AllowSave = True
        OrgCompany.AllowDelete = True

        OrgEntity.AllowSave = True
        OrgEntity.AllowDelete = True

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not OrgCompany.FindControl(row("AddBtnName")) Is Nothing Then
                        OrgCompany.FindControl(row("AddBtnName")).Visible = False
                        OrgCompany.AllowSave = False
                        OrgEntity.FindControl(row("AddBtnName")).Visible = False
                        OrgEntity.AllowSave = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not OrgCompany.FindControl(row("DeleteBtnName")) Is Nothing Then
                        OrgCompany.FindControl(row("DeleteBtnName")).Visible = False
                        OrgCompany.AllowSave = False
                        OrgEntity.FindControl(row("DeleteBtnName")).Visible = False
                        OrgEntity.AllowSave = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not OrgCompany.FindControl(row("EditBtnName")) Is Nothing Then
                        OrgCompany.FindControl(row("EditBtnName")).Visible = False
                        OrgCompany.AllowDelete = False
                        OrgEntity.FindControl(row("EditBtnName")).Visible = False
                        OrgEntity.AllowDelete = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not OrgCompany.FindControl(row("PrintBtnName")) Is Nothing Then
                        OrgCompany.FindControl(row("PrintBtnName")).Visible = False
                        OrgEntity.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

    End Sub

    Protected Sub RadTreeView1_NodeClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadTreeView1.NodeClick

        Select Case RadTreeView1.SelectedNode.Value.Substring(0, 3)
            Case "amc" 'add main company
                OrgCompany.Visible = True
                OrgEntity.Visible = False
                OrgCompany.clearall()
                OrgCompany.CompanyID = 0
                OrgCompany.Adddisplaymode()
                OrgCompany.Companypageload()
                OrgCompany.ManagedByTree("amc", 0)
            Case "acc" 'add child company
                OrgCompany.Visible = True
                OrgEntity.Visible = False
                OrgCompany.clearall()
                OrgCompany.CompanyID = 0
                OrgCompany.Adddisplaymode()
                OrgCompany.Companypageload()
                OrgCompany.ManagedByTree("acc", CInt(RadTreeView1.SelectedNode.ParentNode.Value.Substring(3)))

            Case "mcm" 'company modification
                OrgCompany.Visible = True
                OrgEntity.Visible = False
                OrgCompany.CompanyID = RadTreeView1.SelectedNode.Value.Substring(3)
                ' OrgCompany.fillParent()
                OrgCompany.Companypageload()
                OrgCompany.ManagedByTree("mcm", 0)
            Case "men" 'entity modification
                OrgEntity.clearall()
                OrgCompany.Visible = False
                OrgEntity.Visible = True
                OrgEntity.EntityId = RadTreeView1.SelectedNode.Value.Substring(3)
                ' OrgEntity.fillParent()
                OrgEntity.Entitypageload()
                OrgEntity.ManagedByTree("men", 0, 0, 0)
            Case "ame" 'add main entity 
                OrgEntity.clearall()
                OrgCompany.Visible = False
                OrgEntity.Visible = True
                OrgEntity.EntityId = 0
                ' OrgEntity.fillParent()
                OrgEntity.Entitypageload()
                OrgEntity.Adddisplaymode()
                Dim strlevel As String = RadTreeView1.SelectedNode.Value.Substring(3, 2)
                If strlevel.Substring(0, 1) = "0" Then
                    strlevel = strlevel.Substring(1)
                End If

                OrgEntity.ManagedByTree("ame", 0, RadTreeView1.SelectedNode.ParentNode.Value.Substring(3), CInt(strlevel))
            Case "ace" 'add child entity 
                OrgCompany.Visible = False
                OrgEntity.Visible = True
                OrgEntity.clearall()
                OrgEntity.EntityId = 0 'RadTreeView1.SelectedNode.ParentNode.Value.Substring(3)
                ' OrgEntity.fillParent()
                OrgEntity.Entitypageload()
                OrgEntity.Adddisplaymode()

                'OrgEntity.CompanyID 
                Dim strlevel As String = RadTreeView1.SelectedNode.Value.Substring(3, 2)
                If strlevel.Substring(0, 1) = "0" Then
                    strlevel = strlevel.Substring(1)
                End If
                OrgEntity.ManagedByTree("ace", RadTreeView1.SelectedNode.ParentNode.Value.Substring(3), 0, CInt(strlevel))
            Case Else
        End Select

    End Sub

#End Region

#Region "Methods"

    Sub filltree()
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim currentlevel As Integer = 1
        Dim AddChildcompany As New RadTreeNode
        Dim LicNoOfCompanies As Integer = version.GetNoOfCompanies
        Dim objEmployee As New Employee
        Dim dt As DataTable = objEmployee.GetCountEmployeesAndUsers

        If (objVersion.HasMultiCompany = False) Then
            Dim dtTempCompanies As New DataTable
            dtCompanies = New DataTable
            dtTempCompanies = objOrgCompany.GetAll()

            Dim drCompanyRow As DataRow
            'drCompanyRow = dtTempCompanies.Select()(0)
            drCompanyRow = dtTempCompanies.Select("CompanyId =" & objVersion.GetCompanyId())(0)
            'Dim CompanyId As Integer = drCompanyRow(0)
            dtCompanies = dtTempCompanies.Clone
            dtCompanies.ImportRow(drCompanyRow)


            Dim dtTempLevels As New DataTable
            dtAllLevels = New DataTable
            dtTempLevels = objOrgLevel.GetAll()
            dtAllLevels = dtTempLevels.Clone

            Dim drLevelRow As DataRow()
            If (dtTempLevels.Select("FK_CompanyId =" & objVersion.GetCompanyId()).Length > 0) Then
                drLevelRow = dtTempLevels.Select("FK_CompanyId =" & objVersion.GetCompanyId())

                For drRow As Integer = 0 To drLevelRow.Length - 1
                    dtAllLevels.ImportRow(drLevelRow(drRow))
                Next
            End If


            Dim dtTempEntities As New DataTable
            dtAllEntities = New DataTable
            dtTempEntities = objOrgEntity.GetAll()
            dtAllEntities = dtTempEntities.Clone

            Dim drEntityRow As DataRow()
            If (dtTempEntities.Select("FK_CompanyId =" & objVersion.GetCompanyId()).Length > 0) Then
                drEntityRow = dtTempEntities.Select("FK_CompanyId =" & objVersion.GetCompanyId())

                For drRow As Integer = 0 To drEntityRow.Length - 1
                    dtAllEntities.ImportRow(drEntityRow(drRow))
                Next
            End If
        Else
            dtCompanies = objOrgCompany.GetAll
            dtAllLevels = objOrgLevel.GetAll
            dtAllEntities = objOrgEntity.GetAll
        End If

        RadTreeView1.Nodes.Clear()
        Dim Companies() As DataRow = dtCompanies.Select("FK_ParentId is NULL")
        For Each r As DataRow In Companies
            Dim nod As New RadTreeNode
            If (SessionVariables.CultureInfo = "en-US") Then
                nod.Text = r(3).ToString
            Else
                nod.Text = r(3).ToString
            End If

            nod.Value = "mcm" & r(0).ToString
            nod.Font.Bold = True
            nod.ImageUrl = "~\Images\Company.png"

            Dim levels() As DataRow = dtAllLevels.Select("LevelId = " & currentlevel & " and " & "FK_CompanyId=" & r(0).ToString)
            If levels.Count > 0 Then
                Dim levelname As String = levels(0)("LevelName").ToString 'objOrgLevel.GetByPK.LevelName
                Dim levelArabicname As String = levels(0)("LevelArabicName").ToString
                If Companies.Count > 0 Then
                    Dim AddMainEntity As New RadTreeNode
                    If SessionVariables.CultureInfo = "en-US" Then
                        AddMainEntity.Text = "Add " + levelname
                    Else
                        AddMainEntity.Text = "إضافة " + levelArabicname
                    End If

                    Dim t As String = ""
                    If currentlevel.ToString.Length = 1 Then
                        t = "0" & currentlevel.ToString
                    Else
                        t = currentlevel.ToString
                    End If
                    AddMainEntity.Value = "ame" & t & r(0).ToString
                    nod.Nodes.Add(AddMainEntity)
                End If
            End If

            RadTreeView1.CollapseAllNodes()
            AddEntities(nod)


            If (objVersion.HasMultiCompany = True) Then
                If LicNoOfCompanies > dt.Rows(0)("CompaniesCount").ToString Then
                    If (SessionVariables.CultureInfo = "en-US") Then
                        AddChildcompany.Text = "Add Company"
                    Else
                        AddChildcompany.Text = "إضافة شركة"
                    End If
                Else
                    AddChildcompany.Visible = False
                End If
            End If


            AddChildcompany.Value = "acc"
            If (objVersion.HasMultiCompany()) Then
                If LicNoOfCompanies > dt.Rows(0)("CompaniesCount").ToString Then
                    nod.Nodes.Add(AddChildcompany)
                End If
            End If
            addChildCompanies(nod)

            RadTreeView1.Nodes.Add(nod)
        Next
        Dim AddMaincompanyNode As New RadTreeNode
        If (objVersion.HasMultiCompany = True) Then
            If LicNoOfCompanies > dt.Rows(0)("CompaniesCount").ToString Then
                AddMaincompanyNode.Text = ResourceManager.GetString("AddCom", CultureInfo)
            Else
                AddMaincompanyNode.Visible = False
            End If
        End If

        AddMaincompanyNode.Value = "amc"
        If (objVersion.HasMultiCompany()) Then
            RadTreeView1.Nodes.Add(AddMaincompanyNode)
        End If
        RadTreeView1.CollapseAllNodes()
    End Sub

    Sub addChildCompanies(ByVal Parant_Node As RadTreeNode)
        Dim currentlevel As Integer = 1
        Dim ChildCompanies() As DataRow = dtCompanies.Select("FK_ParentId =" & Parant_Node.Value.Substring(3))
        For Each r As DataRow In ChildCompanies
            Dim nod As New RadTreeNode
            nod.Text = r(3).ToString
            nod.Font.Name = "Arial"
            nod.ImageUrl = "~\Images\Company.png"
            nod.Value = "mcm" & r(0).ToString


            Dim levels() As DataRow = dtAllLevels.Select("LevelId = " & currentlevel & " and " & "FK_CompanyId=" & r(0).ToString)
            If levels.Count > 0 Then
                Dim levelname As String = levels(0)("LevelName").ToString
                Dim LevelArabicName As String = levels(0)("LevelArabicName").ToString
                If ChildCompanies.Count > 0 Then
                    Dim AddMainEntity As New RadTreeNode
                    If (SessionVariables.CultureInfo = "en-US") Then
                        AddMainEntity.Text = ResourceManager.GetString("btnAdd", CultureInfo) + " " + levelname
                    Else
                        AddMainEntity.Text = ResourceManager.GetString("btnAdd", CultureInfo) + " " + LevelArabicName
                    End If

                    Dim t As String = ""
                    If currentlevel.ToString.Length = 1 Then
                        t = "0" & currentlevel.ToString
                    Else
                        t = currentlevel.ToString
                    End If
                    AddMainEntity.Value = "ame" & t & r(0).ToString
                    nod.Nodes.Add(AddMainEntity)
                End If
            End If
            AddEntities(nod)
            Dim AddChildcompany As New RadTreeNode
            If (objVersion.HasMultiCompany()) Then
                AddChildcompany.Text = ResourceManager.GetString("AddCom", CultureInfo)
            Else
                AddChildcompany.Visible = False
            End If


            AddChildcompany.Value = "acc"
            nod.Nodes.Add(AddChildcompany)
            addChildCompanies(nod)

            Parant_Node.Nodes.Add(nod)
            nod.Font.Bold = True
        Next

    End Sub

    Sub AddEntities(ByVal parent_Node As RadTreeNode)
        Dim currentlevel As Integer = 0

        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim ParentEntities() As DataRow = dtAllEntities.Select("FK_ParentId is NULL and FK_CompanyId =" & parent_Node.Value.Substring(3))
        For Each r As DataRow In ParentEntities
            Dim nod As New RadTreeNode
            If (SessionVariables.CultureInfo = "en-US") Then
                nod.Text = r("EntityName").ToString
            Else
                nod.Text = r("EntityArabicName").ToString
            End If

            nod.Value = "men" & r(0).ToString
            nod.Font.Name = "Times New Roman"
            nod.ForeColor = Color.Blue
            nod.ImageUrl = "~\Images\department.gif"
            parent_Node.Nodes.Add(nod)
            Dim entities() As DataRow = dtAllEntities.Select("EntityId = " & r(0).ToString)
            Dim totallevels() As DataRow = dtAllLevels.Select("FK_CompanyId=" & parent_Node.Value.Substring(3))
            currentlevel = entities(0)("FK_LevelId") + 1

            If totallevels.Count >= currentlevel Then
                Dim levels() As DataRow = dtAllLevels.Select("LevelId = " & currentlevel & " and " & "FK_CompanyId=" & parent_Node.Value.Substring(3))
                If levels.Count > 0 Then
                    Dim levelname As String = levels(0)("LevelName")
                    Dim leveArabiclname As String = levels(0)("LevelArabicname")

                    Dim AddChildEntitiesNode As New RadTreeNode
                    If (SessionVariables.CultureInfo = "en-US") Then
                        AddChildEntitiesNode.Text = ResourceManager.GetString("btnAdd", CultureInfo) + " " + levelname
                    Else
                        AddChildEntitiesNode.Text = ResourceManager.GetString("btnAdd", CultureInfo) + " " + leveArabiclname
                    End If

                    Dim t As String = ""
                    If currentlevel.ToString.Length = 1 Then
                        t = "0" & currentlevel.ToString
                    Else
                        t = currentlevel.ToString
                    End If
                    AddChildEntitiesNode.Value = "ace" & t & r(0).ToString
                    nod.Nodes.Add(AddChildEntitiesNode)
                    nod.Font.Size = 8
                    nod.ForeColor = Color.Blue
                    AddChildEntitiesNode.Font.Size = 8
                End If

            End If
            addChildEntities(nod)

        Next
    End Sub

    Sub addChildEntities(ByVal Parant_Node As RadTreeNode)
        Dim currentlevel As Integer = 0
        Dim companyid As Integer = 0


        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim entitiesTemp() As DataRow = dtAllEntities.Select("EntityId =" & Parant_Node.Value.Substring(3))

        companyid = entitiesTemp(0)("FK_CompanyId")
        Dim ChildEntities() As DataRow = dtAllEntities.Select("FK_ParentId =" & Parant_Node.Value.Substring(3) & " and " & "FK_CompanyId =" & companyid)
        Dim totallevels() As DataRow = dtAllLevels.Select("FK_CompanyId=" & companyid)

        For Each r As DataRow In ChildEntities
            Dim nod As New RadTreeNode
            If (SessionVariables.CultureInfo = "en-US") Then
                nod.Text = r("EntityName").ToString
            Else
                nod.Text = r("EntityArabicName").ToString
            End If

            nod.Value = "men" & r(0).ToString
            Dim entities() As DataRow = dtAllEntities.Select("EntityId = " & r(0).ToString)
            currentlevel = entities(0)("FK_LevelId") + 1
            If totallevels.Count >= currentlevel Then
                Dim levels() As DataRow = dtAllLevels.Select("LevelId = " & currentlevel & " and " & "FK_CompanyId=" & companyid)
                If levels.Count > 0 Then
                    Dim levelname As String = levels(0)("LevelName")
                    Dim levelArabicname As String = levels(0)("LevelArabicName")

                    Dim AddChildEntitiesNode As New RadTreeNode
                    If (SessionVariables.CultureInfo = "en-US") Then
                        AddChildEntitiesNode.Text = ResourceManager.GetString("btnAdd", CultureInfo) + " " + levelname
                    Else
                        AddChildEntitiesNode.Text = ResourceManager.GetString("btnAdd", CultureInfo) + " " + levelArabicname
                    End If

                    Dim t As String = ""
                    If currentlevel.ToString.Length = 1 Then
                        t = "0" & currentlevel.ToString
                    Else
                        t = currentlevel.ToString
                    End If
                    AddChildEntitiesNode.Value = "ace" & t & r(0).ToString
                    nod.Nodes.Add(AddChildEntitiesNode)
                    AddChildEntitiesNode.Font.Size = 8
                End If

            End If

            addChildEntities(nod)
            Parant_Node.Nodes.Add(nod)
        Next
    End Sub

    Sub addMainentities(ByVal Parant_Node As TreeNode)
        Dim dtCurrent As DataTable
        objOrgLevel.FK_CompanyId = CInt(Parant_Node.Value.Substring(3))
        dtCurrent = objOrgLevel.GetAllByComapany()

    End Sub

#End Region

End Class
