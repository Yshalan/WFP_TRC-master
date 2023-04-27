Imports System.Data
Imports TA.Lookup
Imports TA.Definitions
Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports TA.Admin
Imports SmartV.Security
Imports SmartV.Version
Imports TA.Security
Imports TA.Employees
Imports TA.ScheduleGroups
Imports TA.DailyTasks
Imports System.Reflection
Imports SmartV.UTILITIES.ProjectCommon

Partial Class DailyTasks_DefineScheduleGroups
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Public Lang As CtlCommon.Lang
    Public AddButtonValue As String
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
    Private objScheduleGroups_Managers As ScheduleGroups_Managers
    Private objScheduleGroups_Employees As ScheduleGroups_Employees
    Private objEmployee_Manager As Employee_Manager

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

    Public Property FK_CompanyId() As Integer
        Get
            Return ViewState("FK_CompanyId")
        End Get
        Set(ByVal value As Integer)
            ViewState("FK_CompanyId") = value
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

    Public Property ManagerId() As Integer
        Get
            Return ViewState("ManagerId")
        End Get
        Set(ByVal value As Integer)
            ViewState("ManagerId") = value
        End Set
    End Property

#End Region

#Region "Page Events"

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
                Me.dtpFromdate.SelectedDate = Today
                Me.dtpEndDate.SelectedDate = Today

                FillCompanies()
                FillGrid()
            End If
            If chckTemporary.Checked = True Then
                showHide(True)
            Else
                showHide(False)
            End If

            pageheader1.HeaderText = ResourceManager.GetString("ScheduleGroup", CultureInfo)
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
                .WorkDayNo = radtxtWorkDayNo.Text
                .RestDayNo = radtxtRestDayNo.Text
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
        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
            Dim item As GridDataItem
            item = e.Item
            If Lang = CtlCommon.Lang.AR Then
                item("EntityName").Text = DirectCast(item.FindControl("hdnEntityArabicName"), HiddenField).Value
                item("GroupNameEn").Text = DirectCast(item.FindControl("hdnGroupNameAr"), HiddenField).Value
            End If
        End If
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Clearall()
        TabGroupManager.Visible = False
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
        CompanyChanged()
        TabGroupManager.Visible = True
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

    Protected Sub ibtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnSave.Click
        objScheduleGroups_Managers = New ScheduleGroups_Managers

        Dim err As Integer
        Dim flag As Boolean = False
        With objScheduleGroups_Managers
            For Each item As ListItem In cblEmpList.Items
                If item.Selected Then
                    flag = True
                    .FK_EmployeeId = item.Value
                    .FK_GroupId = GroupID
                    .FromDate = dtpFromdate.SelectedDate
                    .IsTemp = chckTemporary.Checked
                    .ToDate = IIf(dtpEndDate.SelectedDate Is Nothing Or Not chckTemporary.Checked, DateTime.MinValue, dtpEndDate.SelectedDate)
                    err = .Add
                End If
            Next
            If err = 0 Then
                AssignManager()
            End If
            If err = 0 Then
                If flag = False Then
                    CtlCommon.ShowMessage(Page, ResourceManager.GetString("EmployeeSelect", CultureInfo), "info")
                Else
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
                    Clearall()
                End If
            ElseIf err = -99 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DateExist", CultureInfo), "info")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If


        End With
    End Sub

    Protected Sub ibtnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnClear.Click
        ClearMgr()
    End Sub


#End Region

#Region "Methods"

    Private Function IS_Temporary() As Boolean
        Return chckTemporary.Checked
    End Function

    Private Sub showHide(ByVal status As Boolean, Optional ByVal order As Integer = 0)
        pnlEndDate.Visible = status
        CVDate.Visible = status
    End Sub

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
            'GroupID = 0
            txtGroupNameAr.Text = String.Empty
            txtGroupNameEn.Text = String.Empty
            txtGroupCode.Text = String.Empty
            RadCmbBxCompany.SelectedValue = -1
            RadCmbBxEntity.SelectedValue = -1
            radtxtWorkDayNo.Text = String.Empty
            radtxtRestDayNo.Text = String.Empty
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
        objScheduleGroups_Managers = New ScheduleGroups_Managers
        objEmployee = New Employee

        objScheduleGroups.GroupId = GroupID
        objScheduleGroups.GetByPK()
        With objScheduleGroups
            txtGroupCode.Text = .GroupCode
            txtGroupNameEn.Text = .GroupNameEn
            txtGroupNameAr.Text = .GroupNameAr
            radtxtWorkDayNo.Text = .WorkDayNo
            radtxtRestDayNo.Text = .RestDayNo
            chkActive.Checked = .IsActive

            EntityId = .FK_EntityId
            objOrgEntity.EntityId = EntityId
            objOrgEntity.GetByPK()
            FK_CompanyId = objOrgEntity.FK_CompanyId
            RadCmbBxCompany.SelectedValue = objOrgEntity.FK_CompanyId
            FillCompanies()
            FillEntity()
            RadCmbBxEntity.SelectedValue = EntityId
        End With
        Dim dtManager As DataTable
        objScheduleGroups_Managers.FK_GroupId = GroupID
        dtManager = objScheduleGroups_Managers.GetGroup_ActiveManager
        If Not dtManager Is Nothing Then
            If dtManager.Rows.Count > 0 Then
                objEmployee.EmployeeId = dtManager.Rows(0)("FK_EmployeeId")
                objEmployee.GetByPK()
                If Lang = CtlCommon.Lang.AR Then
                    lblActiveManagerVal.Text = objEmployee.EmployeeArabicName
                Else
                    lblActiveManagerVal.Text = objEmployee.EmployeeName
                End If
                dvActiveManager.Visible = True
            End If
        End If

    End Sub

    Public Sub FillEmployee()
        cblEmpList.Items.Clear()
        cblEmpList.Text = String.Empty

        If MultiEmployeeFilterUC.CompanyID <> 0 Then
            Dim objEmployee As New Employee
            objEmployee.FK_CompanyId = MultiEmployeeFilterUC.CompanyID

            If (Not MultiEmployeeFilterUC.EntityID = 0) AndAlso MultiEmployeeFilterUC.SearchType = "1" Then
                objEmployee.FK_EntityId = MultiEmployeeFilterUC.EntityID
            Else
                objEmployee.FK_EntityId = -1
            End If

            If (Not MultiEmployeeFilterUC.WorkGroupID = 0) AndAlso MultiEmployeeFilterUC.SearchType = "2" Then
                objEmployee.FK_LogicalGroup = MultiEmployeeFilterUC.WorkGroupID
                objEmployee.FilterType = "L"
            Else
                objEmployee.FK_LogicalGroup = -1
            End If

            If (Not MultiEmployeeFilterUC.WorkLocationsID) AndAlso MultiEmployeeFilterUC.SearchType = "3" Then
                objEmployee.FK_WorkLocation = MultiEmployeeFilterUC.WorkLocationsID
                objEmployee.FilterType = "W"
            Else
                objEmployee.FK_WorkLocation = -1
            End If
            Dim dt As DataTable = objEmployee.GetManagersByCompany
            If (dt IsNot Nothing) Then
                Dim dtEmployees = dt
                If (dtEmployees IsNot Nothing) Then
                    If (dtEmployees.Rows.Count > 0) Then
                        Dim dtSource As New DataTable
                        dtSource.Columns.Add("EmployeeId")
                        dtSource.Columns.Add("EmployeeName")
                        Dim drRow As DataRow
                        drRow = dtSource.NewRow()
                        For Item As Integer = 0 To dtEmployees.Rows.Count - 1
                            Dim drSource As DataRow
                            drSource = dtSource.NewRow
                            Dim dcCell1 As New DataColumn
                            Dim dcCell2 As New DataColumn
                            dcCell1.ColumnName = "EmployeeId"
                            dcCell2.ColumnName = "EmployeeName"
                            dcCell1.DefaultValue = dtEmployees.Rows(Item)(0)
                            dcCell2.DefaultValue = dtEmployees.Rows(Item)(3) + "-" + IIf(Lang = CtlCommon.Lang.EN, dtEmployees.Rows(Item)(1), dtEmployees.Rows(Item)(2))
                            drSource("EmployeeId") = dcCell1.DefaultValue
                            drSource("EmployeeName") = dcCell2.DefaultValue
                            dtSource.Rows.Add(drSource)
                        Next
                        Dim dv As New DataView(dtSource)
                        If SessionVariables.CultureInfo = "ar-JO" Then
                            'dv.Sort = "EmployeeName"
                            For Each row As DataRowView In dv
                                If (Not MultiEmployeeFilterUC.EmployeeID = 0) AndAlso MultiEmployeeFilterUC.SearchType = "4" Then
                                    If MultiEmployeeFilterUC.EmployeeID = row("EmployeeId") Then
                                        cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
                                        Exit For
                                    End If
                                Else
                                    cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
                                End If
                            Next
                        Else
                            For Each row As DataRowView In dv
                                If (Not MultiEmployeeFilterUC.EmployeeID = 0) AndAlso MultiEmployeeFilterUC.SearchType = "4" Then
                                    If MultiEmployeeFilterUC.EmployeeID = row("EmployeeId") Then
                                        cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
                                        Exit For
                                    End If
                                Else
                                    cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
                                End If
                            Next
                        End If
                    End If
                End If
            End If
        End If

    End Sub

    Public Sub CompanyChanged()
        'EmployeeFilterUC.FillEntity()
        MultiEmployeeFilterUC.CompanyID = FK_CompanyId
        MultiEmployeeFilterUC.FillList()
        FillEmployee()
    End Sub

    Public Sub EntityChanged()

        FillEmployee()

    End Sub

    Public Sub WorkGroupChanged()

        FillEmployee()

    End Sub

    Public Sub WorkLocationsChanged()

        FillEmployee()

    End Sub

    Private Sub ClearMgr()
        MultiEmployeeFilterUC.ClearValues()
        dtpFromdate.SelectedDate = Date.Today
        chckTemporary.Checked = False
        dtpEndDate.SelectedDate = Date.Today
        cblEmpList.SelectedValue = Nothing
        pnlEndDate.Visible = False
        GroupID = 0
    End Sub

    Private Sub AssignManager()
        Dim dt As DataTable
        Dim dtEmp As DataTable

        objEmployee_Manager = New Employee_Manager
        objScheduleGroups_Managers = New ScheduleGroups_Managers
        objScheduleGroups_Employees = New ScheduleGroups_Employees
        objScheduleGroups_Employees.FK_GroupId = GroupID
        dtEmp = objScheduleGroups_Employees.GetAll_ByFK_GroupId

        If Not dtEmp Is Nothing Then
            If dtEmp.Rows.Count > 0 Then
                objScheduleGroups_Managers.FK_GroupId = GroupID
                dt = objScheduleGroups_Managers.GetByGroupId()
                If dt.Rows.Count > 0 Then
                    ManagerId = Convert.ToInt32(dt.Rows(0)("FK_EmployeeId"))
                    With objEmployee_Manager
                        For Each emprow In dtEmp.Rows
                            .FK_ManagerId = ManagerId
                            .FK_EmployeeId = emprow("FK_EmployeeId")
                            .FromDate = dtpFromdate.DbSelectedDate
                            If Not chckTemporary.Checked = False Then
                                .ToDate = dtpEndDate.DbSelectedDate
                            End If
                            .IsTemporary = chckTemporary.Checked

                            .AssignManager()
                        Next
                    End With
                Else
                    CtlCommon.ShowMessage(Page, ResourceManager.GetString("ScheduleGroupManager"), "info")
                End If
            End If
        End If
    End Sub

#End Region

End Class
