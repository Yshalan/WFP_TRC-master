Imports System.Data
Imports SmartV.UTILITIES
Imports TA.Lookup
Imports TA.Admin
Imports Telerik.Web.UI
Imports SmartV.Security
Imports SmartV.Version
Imports TA.Security
Imports TA.Definitions
Imports TA.Employees
Imports TA.ScheduleGroups
Imports TA.DailyTasks
Imports System.Reflection
Imports SmartV.UTILITIES.ProjectCommon

Partial Class DailyTasks_UserControls_ScheduleGroup
    Inherits System.Web.UI.UserControl

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Dim objSYSUsers As SYSUsers
    Private objVersion As SmartV.Version.version
    Private objSchedule_Entity As Schedule_Entity
    Dim objUserPrivileg_Entities As UserPrivileg_Entities
    Dim objUserPrivileg_Companies As UserPrivileg_Companies
    Dim objEmployee As Employee
    Private objRECALC_REQUEST As RECALC_REQUEST
    Private objScheduleGroups As ScheduleGroups
    Private objOrgCompany As OrgCompany
    Private objOrgEntity As OrgEntity

#End Region

#Region "Properties"

    Public Property GroupID() As Integer
        Get
            Return ViewState("GroupID")
        End Get
        Set(ByVal value As Integer)
            ViewState("GroupID") = value
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

#End Region

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                If (SessionVariables.CultureInfo Is Nothing) Then
                    Response.Redirect("~/default/Login.aspx")
                ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                    Lang = CtlCommon.Lang.AR
                Else
                    Lang = CtlCommon.Lang.EN
                End If

                If (objVersion.HasMultiCompany() = False) Then
                    RadCmbBxCompany.SelectedValue = objVersion.GetCompanyId()
                    RadCmbBxCompany_SelectedIndexChanged(Nothing, Nothing)
                    lblCompany.Visible = False
                    RadCmbBxCompany.Visible = False
                    FillEntity()
                End If
                FillCompanies()
                FillGrid()

            End If
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Protected Sub RadCmbBxCompany_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxCompany.SelectedIndexChanged
        FillEntity()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            objScheduleGroups = New ScheduleGroups
            Dim err As Integer

            With objScheduleGroups
                .GroupId = GroupID
                .GroupNameEn = txtGroupNameEn.Text.Trim()
                .GroupNameAr = txtGroupNameAr.Text.Trim()
                .IsActive = chkActive.Checked
                .FK_EntityId = RadCmbBxEntity.SelectedValue
                .GroupCode = txtGroupCode.Text.Trim()
                If GroupID = 0 Then
                    .CREATED_BY = SessionVariables.LoginUser.ID
                    err = .Add()
                Else
                    .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                    err = .Update
                End If

                If err = 0 Then
                    FillGrid()
                    Clearall()
                    CtlCommon.ShowMessage(Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
                ElseIf err = -10 Then
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CodeExist", CultureInfo), "info")
                Else
                    CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
                End If
            End With
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Protected Sub dgrdGroupDetails_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdGroupDetails.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item) Then
            Dim item As GridDataItem
            item = e.Item
            If Lang = CtlCommon.Lang.AR Then
                item("GroupNameEn").Text = DirectCast(item.FindControl("hdnEntityArabicName"), HiddenField).Value
                item("EntityName").Text = DirectCast(item.FindControl("hdnGroupNameAr"), HiddenField).Value
            End If
        End If
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Clearall()
    End Sub

    Protected Sub dgrdGroupDetails_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdGroupDetails.Skin))
    End Function

    Protected Sub dgrdGroupDetails_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdGroupDetails.NeedDataSource
        objScheduleGroups = New ScheduleGroups
        With objScheduleGroups
            dtCurrent = .GetAll()
            dgrdGroupDetails.DataSource = dtCurrent
        End With
    End Sub

    Protected Sub dgrdGroupDetails_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdGroupDetails.SelectedIndexChanged
        GroupID = DirectCast(dgrdGroupDetails.SelectedItems(0), GridDataItem).GetDataKeyValue("GroupId").ToString().Trim
        FillControls()
        FillGrid()
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1

        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdGroupDetails.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim strCode As String = row.GetDataKeyValue("GroupCode").ToString()
                Dim intGroupId As Integer = Convert.ToInt32(row.GetDataKeyValue("GroupId"))
                objScheduleGroups = New ScheduleGroups
                objScheduleGroups.GroupId = intGroupId
                errNum = objScheduleGroups.Delete()
                With strBuilder

                End With
            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully"), "success")
            FillGrid()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord", CultureInfo), "error")
        End If
        Clearall()

    End Sub

#End Region

#Region "Methods"

    Private Sub FillCompanies()
        Try
            'Get user info and check the user security
            If SessionVariables.LoginUser IsNot Nothing Then
                objUserPrivileg_Companies = New UserPrivileg_Companies
                objSYSUsers = New SYSUsers
                objSYSUsers.ID = SessionVariables.LoginUser.ID
                objSYSUsers.GetUser()

                If Not objSYSUsers.FK_EmployeeId = Nothing Then
                    objUserPrivileg_Companies.FK_EmployeeId = objSYSUsers.FK_EmployeeId
                    objUserPrivileg_Companies.GetByEmpId()
                End If

                If (objUserPrivileg_Companies.FK_CompanyId <> 0) Then
                    FillCompanyForUserSecurity()
                    FillEntity()

                Else
                    Dim objOrgCompany As New OrgCompany
                    CtlCommon.FillTelerikDropDownList(RadCmbBxCompany, objOrgCompany.GetAllforddl, Lang)
                End If
            End If
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub FillEntity()
        Try
            If RadCmbBxCompany.SelectedValue <> -1 Then
                Dim objProjectCommon = New ProjectCommon()
                objUserPrivileg_Companies = New UserPrivileg_Companies
                objUserPrivileg_Entities = New UserPrivileg_Entities
                objSYSUsers = New SYSUsers
                objSYSUsers.ID = SessionVariables.LoginUser.ID
                objSYSUsers.GetUser()

                If Not objSYSUsers.FK_EmployeeId = Nothing Then
                    objUserPrivileg_Companies.FK_EmployeeId = objSYSUsers.FK_EmployeeId
                    objUserPrivileg_Companies.GetByEmpId()

                    objUserPrivileg_Entities.FK_EmployeeId = objSYSUsers.FK_EmployeeId
                    objUserPrivileg_Entities.GetByEmpId()


                    If (objUserPrivileg_Companies.FK_CompanyId <> 0 And objUserPrivileg_Entities.FK_EntityId = 0) Then
                        Dim objOrgEntity = New OrgEntity()
                        objOrgEntity.FK_CompanyId = RadCmbBxCompany.SelectedValue
                        Dim dt As DataTable = objOrgEntity.GetAllEntityByCompany()

                        objProjectCommon.FillMultiLevelRadComboBox(RadCmbBxEntity, dt, "EntityId", _
                                                                 "EntityName", "EntityArabicName", "FK_ParentId")
                        RadCmbBxEntity.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem(ResourceManager.GetString("PleaseSelect", CultureInfo), -1))

                        ''RadCmbBxEntity.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem("--Please Select--", -1))

                    ElseIf (objUserPrivileg_Companies.FK_CompanyId <> 0 And objUserPrivileg_Entities.FK_EntityId <> 0) Then
                        CreateEntityTable()
                    Else
                        Dim objOrgEntity = New OrgEntity()
                        objOrgEntity.FK_CompanyId = RadCmbBxCompany.SelectedValue
                        Dim dt As DataTable = objOrgEntity.GetAllEntityByCompany()
                        objProjectCommon.FillMultiLevelRadComboBox(RadCmbBxEntity, dt, "EntityId", _
                                                             "EntityName", "EntityArabicName", "FK_ParentId")
                        RadCmbBxEntity.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem(ResourceManager.GetString("PleaseSelect", CultureInfo), -1))
                        ''RadCmbBxEntity.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem("--Please Select--", -1))
                    End If
                End If
            End If
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub FillCompanyForUserSecurity()
        Try
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

            CtlCommon.FillTelerikDropDownList(RadCmbBxCompany, dtCompanyInfo, Lang)
            RadCmbBxCompany.SelectedIndex = 1
            RadCmbBxCompany.Enabled = False
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub CreateEntityTable()
        Try
            Dim objOrgEntity = New OrgEntity()
            Dim objProjectCommon = New ProjectCommon()
            objOrgEntity.EntityId = objUserPrivileg_Entities.FK_EntityId
            Dim dt = objOrgEntity.GetEntityAndChilds()

            objProjectCommon.FillMultiLevelRadComboBox(RadCmbBxEntity, dt, "EntityId", _
                                                          "EntityName", "EntityArabicName", "FK_ParentId")
            RadCmbBxEntity.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem(ResourceManager.GetString("PleaseSelect", CultureInfo), -1))
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub Clearall()
        Try
            GroupID = 0
            txtGroupNameAr.Text = String.Empty
            txtGroupNameEn.Text = String.Empty
            txtGroupCode.Text = String.Empty
            RadCmbBxCompany.SelectedValue = -1
            RadCmbBxEntity.SelectedValue = -1
            chkActive.Checked = False

        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub FillGrid()
        Try
            objScheduleGroups = New ScheduleGroups
            With objScheduleGroups
                dtCurrent = .GetAll()
                dgrdGroupDetails.DataSource = dtCurrent
                dgrdGroupDetails.DataBind()
            End With
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub FillControls()
        ' Get the data by the PK and display on the controls
        objScheduleGroups = New ScheduleGroups
        objOrgEntity = New OrgEntity
        objScheduleGroups.GroupId = GroupID
        objScheduleGroups.GetByPK()
        With objScheduleGroups
            txtGroupCode.Text = .GroupCode
            txtGroupNameEn.Text = .GroupNameEn
            txtGroupNameAr.Text = .GroupNameAr
            chkActive.Checked = .IsActive

            EntityId = .FK_EntityId
            objOrgEntity.EntityId = EntityId
            objOrgEntity.GetByPK()
            RadCmbBxCompany.SelectedValue = objOrgEntity.FK_CompanyId
            FillCompanies()
            FillEntity()
            RadCmbBxEntity.SelectedValue = EntityId

        End With
    End Sub

#End Region

End Class
