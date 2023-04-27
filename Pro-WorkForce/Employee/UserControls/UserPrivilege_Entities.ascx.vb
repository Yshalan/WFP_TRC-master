Imports SmartV.UTILITIES
Imports TA.Lookup
Imports System.Data
Imports TA.Employees
Imports TA.Definitions
Imports TA.Admin
Imports TA.Security
Imports Telerik.Web.UI
Imports TA.Employees.Employee

Partial Class Employee_UserControls_UserPrevileges_Entities
    Inherits System.Web.UI.UserControl

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Dim objSYSUsers As SYSUsers
    Private objVersion As SmartV.Version.version
    Shared SortExepression As String
    Shared dtCurrent As DataTable
    Private objUserPrivileg_Companies As New UserPrivileg_Companies
    Private objUserPrivileg_Entities As New UserPrivileg_Entities
    Private objCoordinator_Type As Coordinator_Type
    Private objAPP_Settings As APP_Settings

    '------------------ Adding Dashes For Entity Levels----------------'
    Private dtCustomizedRecordsOrder As New DataTable
    Private cmb As CheckBoxList
    Private ddl As DropDownList
    Private dtDataSource As DataTable
    Private valField As String
    Private EngNameTextField As String
    Private ArNameTextField As String
    Private ParentField As String
    Private Sequence As String
    '------------------ Adding Dashes For Entity Levels----------------'
    Private objOrgLevel As OrgLevel
    Public searchlang As String

#End Region

#Region "Properties"

    Private Property EmployeeId() As Integer
        Get
            Return ViewState("EmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeId") = value
        End Set
    End Property

    Private Property CompanyId() As Integer
        Get
            Return ViewState("CompanyId")
        End Get
        Set(ByVal value As Integer)
            ViewState("CompanyId") = value
        End Set
    End Property

    Private Property EntityId() As Integer
        Get
            Return ViewState("EntityId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EntityId") = value
        End Set
    End Property

    Public Property dtCurrentControls() As DataTable
        Get
            Return ViewState("dtCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
        End Set
    End Property

    Public Property SelectedID() As Integer
        Get
            Return ViewState("ID")
        End Get
        Set(ByVal value As Integer)
            ViewState("ID") = value
        End Set
    End Property

    Public Property IsCoordinator() As Boolean
        Get
            Return ViewState("IsCoordinator")
        End Get
        Set(ByVal value As Boolean)
            ViewState("IsCoordinator") = value
        End Set
    End Property

    Public Property dt() As DataTable
        Get
            Return ViewState("dt")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dt") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Employee_UserControls_UserPrevilege_Entities_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            objEmp_FilterEntity.IsEmployeeRequired = True
            objEmp_FilterEntity.CompanyRequiredValidationGroup = btnSave.ValidationGroup
            objEmp_FilterEntity.EmployeeRequiredValidationGroup = btnSave.ValidationGroup

            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                searchlang = "ar"
            Else
                Lang = CtlCommon.Lang.EN
                searchlang = "en"
            End If
            If (objVersion.HasMultiCompany() = False) Then
                RadCmbBxCompaniesEntity.SelectedValue = objVersion.GetCompanyId()
                RadCmbBxCompaniesEntity.Visible = False
                lblCompany.Visible = False
                FillEntity()
            End If
            fillCompanies()
            fillgrid()
            FillCoordinatorTypes()
            FillLevel_Radio()
             
        End If
        objEmp_FilterEntity.IsEntityClick = "True"
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        ElseIf SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
        Else
            Lang = CtlCommon.Lang.EN

        End If

        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdManagers.ClientID + "');")

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrentControls = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrentControls.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not Upanel1.FindControl(row("AddBtnName")) Is Nothing Then
                        Upanel1.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not Upanel1.FindControl(row("DeleteBtnName")) Is Nothing Then
                        Upanel1.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not Upanel1.FindControl(row("EditBtnName")) Is Nothing Then
                        Upanel1.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not Upanel1.FindControl(row("PrintBtnName")) Is Nothing Then
                        Upanel1.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

    End Sub

    Protected Sub dgrdManagers_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs) Handles dgrdManagers.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdManagers.Skin))
    End Function

    Protected Sub dgrdManagers_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdManagers.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item) Then
            Dim Itm As GridDataItem = e.Item
        End If
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ClearAll()
        If (objVersion.HasMultiCompany() = False) Then
            RadCmbBxCompaniesEntity.SelectedValue = objVersion.GetCompanyId()
            RadCmbBxCompaniesEntity.Visible = False
            lblCompany.Visible = False
            FillEntity()
        End If
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim objUserPrivileg_Entities = New UserPrivileg_Entities
        Dim errNum As Integer = -1
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdManagers.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim strCode As String = row.GetDataKeyValue("Id").ToString()
                objUserPrivileg_Entities = New UserPrivileg_Entities
                objUserPrivileg_Entities.Id = Convert.ToInt32(row.GetDataKeyValue("Id").ToString())
                errNum = objUserPrivileg_Entities.Delete()
            End If
        Next
        ClearAll()
        If (objVersion.HasMultiCompany() = False) Then
            RadCmbBxCompaniesEntity.SelectedValue = objVersion.GetCompanyId()
            RadCmbBxCompaniesEntity.Visible = False
            lblCompany.Visible = False
            FillEntity()
        End If
        fillgrid()
        If errNum = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim err As Integer = 0
        Dim flag As Boolean = False
        Dim UserPrivileg_Entities As New UserPrivileg_Entities
        With UserPrivileg_Entities
            EmployeeId = objEmp_FilterEntity.EmployeeId
            .FK_CompanyId = RadCmbBxCompaniesEntity.SelectedValue

            .FK_EmployeeId = EmployeeId
            .IsCoordinator = chkIsCoordinator.Checked
            objAPP_Settings = New APP_Settings
            objAPP_Settings.GetByPK()

            For Each item As ListItem In cblEntitylist.Items
                If item.Selected Then
                    flag = True
                    .FK_EntityId = item.Value
                    If objAPP_Settings.IncludeNotifications_CoordinatorTypes = True Then
                        .CoordinatorType = radcmbxCoordinatorType.SelectedValue
                    Else
                        .CoordinatorType = Nothing
                    End If
                    If SelectedID = 0 Then
                        err = .Add()
                    Else
                        .Id = SelectedID
                        err = .Update()
                    End If

                End If
            Next
           
            If err = 0 Then
                If flag = False Then
                    CtlCommon.ShowMessage(Page, ResourceManager.GetString("SelectEntity", CultureInfo), "info")
                Else
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
                    ClearAll()
                    If (objVersion.HasMultiCompany() = False) Then
                        RadCmbBxCompaniesEntity.SelectedValue = objVersion.GetCompanyId()
                        RadCmbBxCompaniesEntity.Visible = False
                        lblCompany.Visible = False
                        FillEntity()
                    End If
                    fillgrid()
                End If
            ElseIf err = -5 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NameExist", CultureInfo), "info")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")

            End If
        End With
    End Sub

    Protected Sub RadCmbBxCompanies_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxCompaniesEntity.SelectedIndexChanged
        If (RadCmbBxCompaniesEntity.SelectedValue <> -1) Then
            CompanyId = RadCmbBxCompaniesEntity.SelectedValue
        Else
            CompanyId = 0
        End If
        FillLevel_Radio()
        FillEntity()
    End Sub

    'Protected Sub RadcmbBxEntity_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadcmbBxEntity.SelectedIndexChanged
    '    If RadcmbBxEntity.SelectedValue <> -1 Then
    '        EntityId = RadcmbBxEntity.SelectedValue
    '    Else
    '        EntityId = 0
    '    End If
    'End Sub

    Protected Sub dgrdManagers_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdManagers.NeedDataSource
        Dim objUserPrivileg_Entities = New UserPrivileg_Entities
        dtCurrent = objUserPrivileg_Entities.GetManagerEntity()
        Dim dv As New DataView(dtCurrent)
        dv.Sort = SortExepression
        dgrdManagers.DataSource = dv
    End Sub

    Protected Sub dgrdManagers_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdManagers.SelectedIndexChanged
        FillControls()
    End Sub

    Protected Sub chkIsCoordinator_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsCoordinator.CheckedChanged
        objAPP_Settings = New APP_Settings
        objAPP_Settings.GetByPK()
        If objAPP_Settings.IncludeNotifications_CoordinatorTypes = True Then
            If chkIsCoordinator.Checked = True Then
                dvCoordinatorType.Visible = True
            Else
                dvCoordinatorType.Visible = False
                radcmbxCoordinatorType.SelectedValue = -1
            End If
        End If
    End Sub

    Protected Sub rblLevels_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblLevels.SelectedIndexChanged
        FillEntity()
    End Sub

#End Region

#Region "Methods"

    Private Sub fillCompanies()
        If SessionVariables.LoginUser IsNot Nothing Then
            objSYSUsers = New SYSUsers
            objSYSUsers.ID = SessionVariables.LoginUser.ID
            objSYSUsers.GetUser()

            If Not objSYSUsers.FK_EmployeeId = Nothing Then
                objUserPrivileg_Companies.FK_EmployeeId = objSYSUsers.FK_EmployeeId
                objUserPrivileg_Companies.GetByEmpId()
            End If

            Dim objOrgCompany = New OrgCompany
            If (objUserPrivileg_Companies.FK_CompanyId <> 0) Then
                FillCompanyForUserSecurity()
                FillEntity()
            Else
                CtlCommon.FillTelerikDropDownList(RadCmbBxCompaniesEntity, objOrgCompany.GetAllforddl, Lang)
            End If
        End If
    End Sub

    Private Sub FillCompanyForUserSecurity()
        Dim objOrgCompany As New OrgCompany
        objOrgCompany.CompanyId = objUserPrivileg_Companies.FK_CompanyId
        Dim CompanyInfo = objOrgCompany.GetByPK()

        Dim dtCompanyInfo As New DataTable
        Dim dcCompanyValue As New DataColumn
        dcCompanyValue.ColumnName = "Value"
        dcCompanyValue.DataType = GetType(Integer)

        Dim dcCompanyText As New DataColumn
        dcCompanyText.ColumnName = "Text"
        dcCompanyText.DataType = GetType(String)

        dtCompanyInfo.Columns.Add(dcCompanyValue)
        dtCompanyInfo.Columns.Add(dcCompanyText)
        Dim drCompanyRow = dtCompanyInfo.NewRow()
        drCompanyRow("Value") = CompanyInfo.CompanyId
        drCompanyRow("Text") = CompanyInfo.CompanyName
        dtCompanyInfo.Rows.Add(drCompanyRow)

        CtlCommon.FillTelerikDropDownList(RadCmbBxCompaniesEntity, dtCompanyInfo, Lang)
        RadCmbBxCompaniesEntity.SelectedIndex = 1
        RadCmbBxCompaniesEntity.Enabled = False
    End Sub

    Private Sub FillEntity()
        If RadCmbBxCompaniesEntity.SelectedValue <> -1 Then
            objUserPrivileg_Companies = New UserPrivileg_Companies
            objUserPrivileg_Entities = New UserPrivileg_Entities
            Dim objProjectCommon = New ProjectCommon()
            objSYSUsers = New SYSUsers
            objSYSUsers.ID = SessionVariables.LoginUser.ID
            objSYSUsers.GetUser()

            If Not objSYSUsers.FK_EmployeeId = Nothing Then

                objUserPrivileg_Companies.FK_EmployeeId = objSYSUsers.FK_EmployeeId
                objUserPrivileg_Companies.GetByEmpId()
                objUserPrivileg_Entities.FK_EmployeeId = objSYSUsers.FK_EmployeeId
                objUserPrivileg_Entities.GetByEmpId()
            End If

            Dim objOrgEntity = New OrgEntity()
            objOrgEntity.FK_CompanyId = RadCmbBxCompaniesEntity.SelectedValue

            If rblLevels.SelectedValue = Nothing Then
                dt = objOrgEntity.GetAllEntityByCompany()
            Else
                objOrgEntity.FK_LevelId = rblLevels.SelectedValue
                dt = objOrgEntity.GetAllEntityBy_CompanyandLevel()
            End If

            'FillMultiLevelCheckBoxList(cblEntitylist, dt, "EntityId", "EntityName", "EntityArabicName", "FK_ParentId")
            'If (dtCustomizedRecordsOrder IsNot Nothing) Then
            '    Dim dtEmployees = dtCustomizedRecordsOrder
            If (dt IsNot Nothing) Then
                cblEntitylist.Items.Clear()
                Dim dtEmployees = dt
                If (dtEmployees IsNot Nothing) Then
                    If (dtEmployees.Rows.Count > 0) Then
                        Repeater1.Visible = True
                        Dim dtSource As New DataTable
                        dtSource.Columns.Add("EntityId")
                        dtSource.Columns.Add("EntityName")
                        Dim drRow As DataRow
                        drRow = dtSource.NewRow()
                        For Item As Integer = 0 To dtEmployees.Rows.Count - 1

                            Dim drSource As DataRow
                            drSource = dtSource.NewRow
                            Dim dcCell1 As New DataColumn
                            Dim dcCell2 As New DataColumn
                            dcCell1.ColumnName = "EntityId"
                            dcCell2.ColumnName = "EntityName"
                            dcCell1.DefaultValue = dtEmployees.Rows(Item)("EntityId")
                            dcCell2.DefaultValue = IIf(Lang = CtlCommon.Lang.EN, dtEmployees.Rows(Item)("EntityName"), dtEmployees.Rows(Item)("EntityArabicName"))
                            drSource("EntityId") = dcCell1.DefaultValue
                            drSource("EntityName") = dcCell2.DefaultValue
                            dtSource.Rows.Add(drSource)

                        Next
                        Dim dv As New DataView(dtSource)
                        If SessionVariables.CultureInfo = "ar-JO" Then
                            'dv.Sort = "EmployeeName"
                            For Each row As DataRowView In dv
                                If (Not EntityId = 0) Then
                                    If EntityId = row("EntityId") Then
                                        cblEntitylist.Items.Add(New ListItem(row("EntityName").ToString(), row("EntityId").ToString()))
                                        Exit For
                                    End If
                                Else
                                    cblEntitylist.Items.Add(New ListItem(row("EntityName").ToString(), row("EntityId").ToString()))
                                End If
                            Next
                        Else
                            For Each row As DataRowView In dv
                                If (Not EntityId = 0) Then
                                    If EntityId = row("EntityId") Then
                                        cblEntitylist.Items.Add(New ListItem(row("EntityName").ToString(), row("EntityId").ToString()))
                                        Exit For
                                    End If
                                Else
                                    cblEntitylist.Items.Add(New ListItem(row("EntityName").ToString(), row("EntityId").ToString()))
                                End If
                            Next
                        End If
                    Else
                        cblEntitylist.Items.Clear()
                    End If
                End If
            End If

            'If (objUserPrivileg_Companies.FK_CompanyId <> 0 And objUserPrivileg_Entities.FK_EntityId = 0) Then
            '    Dim objOrgEntity = New OrgEntity()
            '    objOrgEntity.FK_CompanyId = RadCmbBxCompaniesEntity.SelectedValue
            '    Dim dt As DataTable = objOrgEntity.GetAllEntityByCompany()
            '    objProjectCommon.FillMultiLevelRadComboBox(RadcmbBxEntity, dt, "EntityId", _
            '                                             "EntityName", "EntityArabicName", "FK_ParentId")
            '    RadcmbBxEntity.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem(ResourceManager.GetString("PleaseSelect", CultureInfo), -1))
            'ElseIf (objUserPrivileg_Companies.FK_CompanyId <> 0 And objUserPrivileg_Entities.FK_EntityId <> 0) Then
            '    CreateEntityTable()
            'Else
            '    Dim objOrgEntity = New OrgEntity()
            '    objOrgEntity.FK_CompanyId = RadCmbBxCompaniesEntity.SelectedValue
            '    Dim dt As DataTable = objOrgEntity.GetAllEntityByCompany()
            '    objProjectCommon.FillMultiLevelRadComboBox(RadcmbBxEntity, dt, "EntityId", _
            '                                         "EntityName", "EntityArabicName", "FK_ParentId")
            '    RadcmbBxEntity.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem(ResourceManager.GetString("PleaseSelect", CultureInfo), -1))
            'End If
        End If
    End Sub

    'Private Sub CreateEntityTable()
    '    Dim objOrgEntity = New OrgEntity()
    '    Dim objProjectCommon = New ProjectCommon()
    '    objOrgEntity.EntityId = objUserPrivileg_Entities.FK_EntityId
    '    Dim dt = objOrgEntity.GetEntityAndChilds()

    '    objProjectCommon.FillMultiLevelRadComboBox(RadcmbBxEntity, dt, "EntityId", _
    '                                                  "EntityName", "EntityArabicName", "FK_ParentId")
    '    RadcmbBxEntity.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem(ResourceManager.GetString("PleaseSelect", CultureInfo), -1))
    'End Sub

    Private Sub ClearAll()
        'If (objVersion.HasMultiCompany() = False) Then
        '    RadcmbBxEntity.SelectedValue = -1
        'Else
        '    If RadCmbBxCompaniesEntity.Enabled Then
        RadCmbBxCompaniesEntity.ClearSelection()
        '        RadcmbBxEntity.Items.Clear()
        '        RadcmbBxEntity.Text = String.Empty
        '    Else
        '        RadcmbBxEntity.ClearSelection()
        '    End If
        'End If
        chkIsCoordinator.Checked = False
        dvCoordinatorType.Visible = False
        objEmp_FilterEntity.ClearValues()
        SelectedID = 0
        dgrdManagers.SelectedIndexes.Clear()
        cblEntitylist.Items.Clear()
        radcmbxCoordinatorType.SelectedValue = -1
        EntityId = 0
        CompanyId = 0
        EmployeeId = 0
    End Sub

    Public Sub fillgrid()
        Dim objUserPrivileg_Entities = New UserPrivileg_Entities
        dgrdManagers.DataSource = objUserPrivileg_Entities.GetManagerEntity()
        dgrdManagers.DataBind()
    End Sub

    Private Sub FillControls()

        CompanyId = CInt(CType(dgrdManagers.SelectedItems(0), GridDataItem).GetDataKeyValue("CompanyId").ToString())
        EntityId = CInt(CType(dgrdManagers.SelectedItems(0), GridDataItem).GetDataKeyValue("EntityId").ToString())
        EmployeeId = CInt(CType(dgrdManagers.SelectedItems(0), GridDataItem).GetDataKeyValue("EmployeeId").ToString())
        SelectedID = CInt(CType(dgrdManagers.SelectedItems(0), GridDataItem).GetDataKeyValue("Id").ToString())
        IsCoordinator = CBool(CType(dgrdManagers.SelectedItems(0), GridDataItem).GetDataKeyValue("IsCoordinator").ToString())
        chkIsCoordinator.Checked = IsCoordinator
        objEmp_FilterEntity.IsEntityClick = "True"
        objEmp_FilterEntity.GetEmployeeInfo(EmployeeId)
        RadCmbBxCompaniesEntity.SelectedValue = CompanyId
        FillEntity()
        cblEntitylist.Items.FindByValue(EntityId).Selected = True
        cblEntitylist.Items.FindByValue(EntityId).Enabled = False
    End Sub

    '------------------ Adding Dashes For Entity Levels----------------'
    Public Sub FillMultiLevelCheckBoxList(ByVal cmb As CheckBoxList, ByVal dtDataSource As DataTable, _
                                             ByVal valField As String, ByVal EngNameTextField As String, _
                                             ByVal ArNameTextField As String, ByVal ParentField As String, _
                                             Optional ByVal OrderByField As String = Nothing)

        If dtDataSource IsNot Nothing Then

            cmb.Items.Clear()


            Try
                Me.dtDataSource = dtDataSource
                Me.cmb = cmb
                Me.valField = valField
                Me.EngNameTextField = EngNameTextField
                Me.ArNameTextField = ArNameTextField
                Me.ParentField = ParentField
                dtCustomizedRecordsOrder.Columns.Add(New DataColumn(valField))
                dtCustomizedRecordsOrder.Columns.Add(New DataColumn(EngNameTextField))
                dtCustomizedRecordsOrder.Columns.Add(New DataColumn(ArNameTextField))
                dtCustomizedRecordsOrder.Columns.Add(New DataColumn(ParentField))
                Dim drChilds As DataRow() = Nothing

                If OrderByField Is Nothing Then
                    Dim drRoots() As DataRow = dtDataSource.Select(ParentField + " is null")
                    addNode(drRoots, drChilds, (drRoots.Count - 1), 0)
                Else
                    Me.Sequence = OrderByField
                    Dim drRoots() As DataRow = dtDataSource.Select(ParentField + " is null", Me.Sequence & " DESC")
                    addNodeOrderBy(drRoots, drChilds, (drRoots.Count - 1), 0)
                End If
            Catch ex As Exception

            End Try
        Else

        End If
    End Sub

    Private Sub addNode(ByVal drRoot() As DataRow, _
                           ByVal foundChilds As DataRow(), _
                           ByVal count As Integer, _
                           ByVal levelNo As Integer)
        If count >= 0 Then
            If levelNo = 0 Then
                ' Get the current root 
                Dim dr As DataRow = drRoot(count)
                dtCustomizedRecordsOrder.Rows.Add(dr(valField), dr(EngNameTextField), dr(ArNameTextField), dr(ParentField))
                ' Check if the root has childs , if exist get ordered 
                ' by Sequence
                foundChilds = dtDataSource.Select(ParentField + "=" & _
                                     dr(valField))
                If foundChilds.Count <> 0 Then
                    ' Increase the level to add node on higher 
                    ' level
                    levelNo = levelNo + 1
                    addNode(drRoot, foundChilds, count, levelNo)
                    ' Return to add at lower level
                    levelNo = levelNo - 1
                End If
                count = count - 1
                addNode(drRoot, _
                    foundChilds, _
                    count, _
                    levelNo)
            Else
                For Each row As DataRow In foundChilds
                    ' Prepare the Id and the name of the child 
                    Dim id As Integer = row(valField)
                    Dim EngName As String = row(EngNameTextField)
                    Dim ArName As String = row(ArNameTextField)
                    Dim ParentId As String = row(ParentField)
                    ' Add the new child
                    dtCustomizedRecordsOrder.Rows.Add(id, getPrefix(levelNo) & EngName, _
                                                      getPrefix(levelNo) & ArName, ParentId)

                    ' Check if the child have sub-childs , if exist will return
                    ' Order By Sequence number

                    Dim childs As DataRow() = _
                        dtDataSource.Select(ParentField & "=" & id)
                    If childs.Length <> 0 Then
                        levelNo = levelNo + 1
                        addNode(drRoot, childs, count, levelNo)
                        levelNo = levelNo - 1
                    End If
                Next
            End If
        Else
            Me.cmb.DataSource = dtCustomizedRecordsOrder
            setLocalizedTextField(cmb, Me.EngNameTextField, Me.ArNameTextField)
            Me.cmb.DataValueField = valField
        End If
    End Sub

    Private Sub addNodeOrderBy(ByVal drRoot() As DataRow, _
                        ByVal foundChilds As DataRow(), _
                        ByVal count As Integer, _
                        ByVal levelNo As Integer)
        If count >= 0 Then
            If levelNo = 0 Then
                ' Get the current root 
                Dim dr As DataRow = drRoot(count)
                dtCustomizedRecordsOrder.Rows.Add(dr(valField), dr(EngNameTextField), dr(ArNameTextField), dr(ParentField))
                ' Check if the root has childs , if exist get ordered 
                ' by Sequence
                foundChilds = dtDataSource.Select(ParentField + "=" & _
                                     dr(valField), Sequence & " DESC")

                If foundChilds.Count <> 0 Then
                    ' Increase the level to add node on higher 
                    ' level
                    levelNo = levelNo + 1
                    addNodeOrderBy(drRoot, foundChilds, count, levelNo)
                    ' Return to add at lower level
                    levelNo = levelNo - 1
                End If
                count = count - 1
                addNodeOrderBy(drRoot, _
                    foundChilds, _
                    count, _
                    levelNo)
            Else
                For Each row As DataRow In foundChilds
                    ' Prepare the Id and the name of the child 
                    Dim id As Integer = row(valField)
                    Dim EngName As String = row(EngNameTextField)
                    Dim ArName As String = row(ArNameTextField)
                    Dim ParentId As String = row(ParentField)
                    ' Add the new child
                    dtCustomizedRecordsOrder.Rows.Add(id, getPrefix(levelNo) & EngName, _
                                                      getPrefix(levelNo) & ArName, ParentId)
                    ' Check if the child have sub-childs , if exist will return
                    ' Order By Sequence number
                    Dim childs As DataRow() = _
                        dtDataSource.Select(ParentField & "=" & id, Sequence & " DESC")
                    If childs.Length <> 0 Then
                        levelNo = levelNo + 1
                        addNodeOrderBy(drRoot, childs, count, levelNo)
                        levelNo = levelNo - 1
                    End If
                Next
            End If
        Else
            Me.cmb.DataSource = dtCustomizedRecordsOrder
            setLocalizedTextField(Me.cmb, Me.EngNameTextField, Me.ArNameTextField)
            Me.cmb.DataValueField = valField
        End If

    End Sub

    Public Function getPrefix(ByVal level As Integer) As String
        ' Generate strings identify a class at a level
        Dim strPrefix As New StringBuilder
        For index As Integer = 0 To level
            strPrefix.Append("-")
        Next
        Return strPrefix.ToString()

    End Function

    Private Sub setLocalizedTextField(ByVal comb As CheckBoxList, _
                           ByVal EnName As String, ByVal ArName As String)
        comb.DataTextField = IIf(SessionVariables.CultureInfo = "en-US", _
                                                   EnName, ArName)
    End Sub
    '------------------ Adding Dashes For Entity Levels----------------'
    Private Sub FillCoordinatorTypes()
        Dim dt As DataTable
        objCoordinator_Type = New Coordinator_Type
        With objCoordinator_Type
            dt = .GetAll
            If (dt IsNot Nothing) Then
                Dim dtCoordinators As DataTable = dt
                If (dtCoordinators IsNot Nothing) Then
                    If (dtCoordinators.Rows.Count > 0) Then

                        Dim dtSource As New DataTable
                        dtSource.Columns.Add("CoordinatorTypeId")
                        dtSource.Columns.Add("CoordinatorTypeName")

                        Dim drRow As DataRow
                        drRow = dtSource.NewRow()
                        For Item As Integer = 0 To dtCoordinators.Rows.Count - 1
                            Dim drSource As DataRow
                            drSource = dtSource.NewRow
                            Dim dcCell1 As New DataColumn
                            Dim dcCell2 As New DataColumn
                            dcCell1.ColumnName = "CoordinatorTypeId"
                            dcCell2.ColumnName = "CoordinatorTypeName"
                            dcCell1.DefaultValue = dtCoordinators.Rows(Item)(0)
                            dcCell2.DefaultValue = dtCoordinators.Rows(Item)("CoordinatorShortName") + "-" + IIf(Lang = CtlCommon.Lang.EN, dtCoordinators.Rows(Item)("CoordinatorTypeName"), dtCoordinators.Rows(Item)("CoordinatorTypeArabicName"))
                            drSource("CoordinatorTypeId") = dcCell1.DefaultValue
                            drSource("CoordinatorTypeName") = dcCell2.DefaultValue

                            dtSource.Rows.Add(drSource)
                        Next
                        ProjectCommon.FillRadComboBox(radcmbxCoordinatorType, dtSource, "CoordinatorTypeName", "CoordinatorTypeName")
                    End If
                End If
            End If







        End With
    End Sub

    Private Sub FillLevel_Radio()
        objOrgLevel = New OrgLevel
        Dim dt As DataTable
        With objOrgLevel
            If (objVersion.HasMultiCompany() = False) Then
                .FK_CompanyId = objVersion.GetCompanyId()
            Else
                .FK_CompanyId = RadCmbBxCompaniesEntity.SelectedValue
            End If
            dt = .GetAllByComapany()
        End With
        If dt.Rows.Count > 0 Then
            rblLevels.DataSource = dt
            rblLevels.DataTextField = IIf(Lang = CtlCommon.Lang.AR, "LevelArabicName", "LevelName")
            rblLevels.DataValueField = "LevelId"
            rblLevels.DataBind()
        End If


    End Sub

#End Region

    
End Class
