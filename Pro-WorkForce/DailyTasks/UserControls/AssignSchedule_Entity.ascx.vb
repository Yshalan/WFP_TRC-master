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
Imports TA.DailyTasks

Partial Class DailyTasks_UserControls_AssignSchedule_Entity
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
    Private objRecalculateRequest As RecalculateRequest
    Public MsgLang As String
    Private objOrgEntity As OrgEntity
    Private objAPP_Settings As APP_Settings

#End Region

#Region "Properties"
    Public Property EmpWorkScheduleId() As Integer
        Get
            Return ViewState("EmpWorkScheduleId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmpWorkScheduleId") = value
        End Set
    End Property
    Public Property CompanyId() As Integer
        Get
            Return ViewState("CompanyId")
        End Get
        Set(ByVal value As Integer)
            ViewState("CompanyId") = value
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
    Public Property IncludeSubEntity() As Boolean
        Get
            Return ViewState("IncludeSubEntity")
        End Get
        Set(ByVal value As Boolean)
            ViewState("IncludeSubEntity") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        showHide(chckTemporary.Checked)
        If Not Page.IsPostBack Then
            If (objVersion.HasMultiCompany() = False) Then
                RadCmbBxCompany.SelectedValue = objVersion.GetCompanyId()
                RadCmbBxCompany_SelectedIndexChanged(Nothing, Nothing)
                lblCompany.Visible = False
                RadCmbBxCompany.Visible = False
                FillEntity()
            End If

            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                MsgLang = "ar"
                Dim item1 As New RadComboBoxItem
                Dim item2 As New RadComboBoxItem
                Dim item3 As New RadComboBoxItem
                Dim item4 As New RadComboBoxItem
                Dim item5 As New RadComboBoxItem
                item1.Value = -1
                item1.Text = "--الرجاء الاختيار--"
                RadCmbBxScheduletype.Items.Add(item1)
                item2.Value = 1
                item2.Text = "عادي"
                RadCmbBxScheduletype.Items.Add(item2)
                item3.Value = 2
                item3.Text = "مرن"
                RadCmbBxScheduletype.Items.Add(item3)
                item4.Value = 3
                item4.Text = "مناوبات"
                RadCmbBxScheduletype.Items.Add(item4)
                item5.Value = 5
                item5.Text = "مفتوح"
                RadCmbBxScheduletype.Items.Add(item5)
                rfvCompany.InitialValue = "--الرجاء الاختيار--"
                rfvScheduletype.InitialValue = "--الرجاء الاختيار--"
            Else
                Lang = CtlCommon.Lang.EN
                MsgLang = "en"
                Dim item1 As New RadComboBoxItem
                Dim item2 As New RadComboBoxItem
                Dim item3 As New RadComboBoxItem
                Dim item4 As New RadComboBoxItem
                Dim item5 As New RadComboBoxItem
                item1.Value = -1
                item1.Text = "--Please Select--"
                RadCmbBxScheduletype.Items.Add(item1)
                item2.Value = 1
                item2.Text = "Normal"
                RadCmbBxScheduletype.Items.Add(item2)
                item3.Value = 2
                item3.Text = "Flexible"
                RadCmbBxScheduletype.Items.Add(item3)
                item4.Value = 3
                item4.Text = "Advanced"
                RadCmbBxScheduletype.Items.Add(item4)
                item5.Value = 5
                item5.Text = "Open"
                RadCmbBxScheduletype.Items.Add(item5)
                rfvCompany.InitialValue = "--Please Select--"
                rfvScheduletype.InitialValue = "--Please Select--"
            End If
            FillCompanies()
            Me.dtpFromdate.SelectedDate = Today
            Me.dtpEndDate.SelectedDate = Today
            FillGrid()
            'Assign_Entity.HeaderText = ResourceManager.GetString("Assign_Entity", CultureInfo)
            ArcivingMonths_DateValidation()
        End If

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not trControls.FindControl(row("AddBtnName")) Is Nothing Then
                        trControls.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not trControls.FindControl(row("DeleteBtnName")) Is Nothing Then
                        trControls.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not trControls.FindControl(row("EditBtnName")) Is Nothing Then
                        trControls.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not trControls.FindControl(row("PrintBtnName")) Is Nothing Then
                        trControls.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next


    End Sub

    Protected Sub RadCmbBxScheduletype_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxScheduletype.SelectedIndexChanged
        FillSchedule()
    End Sub

    Private Sub showHide(ByVal status As Boolean, Optional ByVal order As Integer = 0)
        pnlEndDate.Visible = status
        CVDate.Visible = status
    End Sub

    Protected Sub RadCmbBxCompany_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxCompany.SelectedIndexChanged
        FillEntity()
    End Sub

    Protected Sub btnYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYes.Click
        IncludeSubEntity = True
        SaveSchedule()
    End Sub

    Protected Sub btnNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNo.Click
        IncludeSubEntity = False
        SaveSchedule()
    End Sub

    Public Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Page.Validate("grpSave_entity")
        If Page.IsValid Then
            mpeSave.Show()
        End If
    End Sub

    Protected Sub dgrdSchedule_Entity_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdSchedule_Entity.SelectedIndexChanged
        objSchedule_Entity = New Schedule_Entity
        EmpWorkScheduleId = CInt(CType(dgrdSchedule_Entity.SelectedItems(0), GridDataItem).GetDataKeyValue("EmpWorkScheduleId").ToString())
        CompanyId = CInt(CType(dgrdSchedule_Entity.SelectedItems(0), GridDataItem).GetDataKeyValue("CompanyId").ToString())
        EntityId = CInt(CType(dgrdSchedule_Entity.SelectedItems(0), GridDataItem).GetDataKeyValue("EntityId").ToString())
        ScheduleId = CInt(CType(dgrdSchedule_Entity.SelectedItems(0), GridDataItem).GetDataKeyValue("ScheduleId").ToString())

        With objSchedule_Entity
            .EmpWorkScheduleId = EmpWorkScheduleId
            .GetByPK()
            RadCmbBxCompany.SelectedValue = CompanyId
            FillEntity()
            RadCmbBxEntity.SelectedValue = .FK_EntityId
            RadCmbBxScheduletype.SelectedValue = .ScheduleType
            FillSchedule()
            RadCmbBxSchedules.SelectedValue = .FK_ScheduleId.ToString + "," + .ScheduleType.ToString
            dtpFromdate.SelectedDate = .FromDate
            chckTemporary.Checked = .IsTemporary
            showHide(chckTemporary.Checked)
            If chckTemporary.Checked = True Then
                If Not .ToDate = Nothing Then
                    showHide(chckTemporary.Checked)
                    dtpEndDate.SelectedDate = .ToDate
                Else
                    showHide(chckTemporary.Checked)
                End If
            End If
        End With
        ManageControls(False)
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Sub dgrdSchedule_Entity_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdSchedule_Entity.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub dgrdSchedule_Entity_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdSchedule_Entity.NeedDataSource
        Try
            objSchedule_Entity = New Schedule_Entity
            dgrdSchedule_Entity.DataSource = objSchedule_Entity.Get_Entity_Schedule_Details
        Catch ex As Exception
        End Try
    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdSchedule_Entity.Skin))
    End Function

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        ClearAll()
        ManageControls(True)
    End Sub

#End Region

#Region "Methods"

    Protected Sub SaveSchedule()

        If ValidateEndDate() = False Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DateRange", CultureInfo), "info")
            Return
        End If

        Dim errornum As Integer
        Dim errornum2 As Integer
        objRECALC_REQUEST = New RECALC_REQUEST
        Dim temp_str_date As String
        Dim err2 As Integer
        If IncludeSubEntity = False Then
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            If RadCmbBxSchedules.SelectedValue <> -1 Then
                Dim arr As String()
                arr = RadCmbBxSchedules.SelectedValue.Split(",")
                Dim objSchedule_Entity As New Schedule_Entity
                objSchedule_Entity.FK_EntityId = RadCmbBxEntity.SelectedValue
                objSchedule_Entity.FK_ScheduleId = RadCmbBxSchedules.SelectedValue.ToString().Split(",")(0)
                objSchedule_Entity.ScheduleType = RadCmbBxScheduletype.SelectedValue
                objSchedule_Entity.FromDate = dtpFromdate.SelectedDate
                objSchedule_Entity.ToDate = IIf(dtpEndDate.SelectedDate Is Nothing Or Not chckTemporary.Checked, DateTime.MinValue, dtpEndDate.SelectedDate)
                objSchedule_Entity.IsTemporary = chckTemporary.Checked
                objSchedule_Entity.CREATED_BY = SessionVariables.LoginUser.ID
                If EmpWorkScheduleId <> 0 Then
                    objSchedule_Entity.EmpWorkScheduleId = EmpWorkScheduleId
                    objSchedule_Entity.LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                    errornum = objSchedule_Entity.Update
                Else
                    errornum = objSchedule_Entity.AssignSchedule_Entity()
                End If


            End If


            If errornum = 0 Then

                If Not dtpFromdate.SelectedDate > Date.Today Then
                    objRecalculateRequest = New RecalculateRequest
                    objRecalculateRequest.Fk_CompanyId = RadCmbBxCompany.SelectedValue
                    objRecalculateRequest.Fk_EntityId = RadCmbBxEntity.SelectedValue
                    objRecalculateRequest.ImmediatelyStart = True
                    objRecalculateRequest.FromDate = dtpFromdate.SelectedDate
                    objRecalculateRequest.Remarks = "Assign Schedule - SYSTEM"
                    If chckTemporary.Checked = True Then
                        If Not dtpEndDate.SelectedDate Is Nothing Or Not dtpEndDate.SelectedDate > Date.Today Then
                            objRecalculateRequest.ToDate = dtpEndDate.SelectedDate
                        End If
                    Else
                        objRecalculateRequest.ToDate = Date.Today
                    End If
                    objRecalculateRequest.CREATED_BY = SessionVariables.LoginUser.FK_EmployeeId
                    errornum2 = objRecalculateRequest.Add()
                End If
            End If
        Else
            objOrgEntity = New OrgEntity
            Dim dt As DataTable
            With objOrgEntity
                .EntityId = RadCmbBxEntity.SelectedValue
                .FK_ParentId = RadCmbBxEntity.SelectedValue
                dt = .GetAllchildsByParent()
            End With
            Dim objSchedule_Entity As New Schedule_Entity
            For Each row In dt.Rows
                objSchedule_Entity.FK_EntityId = row("EntityId")
                objSchedule_Entity.FK_ScheduleId = RadCmbBxSchedules.SelectedValue.ToString().Split(",")(0)
                objSchedule_Entity.ScheduleType = RadCmbBxScheduletype.SelectedValue
                objSchedule_Entity.FromDate = dtpFromdate.SelectedDate
                objSchedule_Entity.ToDate = IIf(dtpEndDate.SelectedDate Is Nothing Or Not chckTemporary.Checked, DateTime.MinValue, dtpEndDate.SelectedDate)
                objSchedule_Entity.IsTemporary = chckTemporary.Checked
                objSchedule_Entity.CREATED_BY = SessionVariables.LoginUser.ID
                If EmpWorkScheduleId <> 0 Then
                    objSchedule_Entity.EmpWorkScheduleId = EmpWorkScheduleId
                    objSchedule_Entity.LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                    errornum = objSchedule_Entity.Update
                Else
                    errornum = objSchedule_Entity.AssignSchedule_Entity()
                End If

                If errornum = 0 Then
                    If Not dtpFromdate.SelectedDate > Date.Today Then
                        objRecalculateRequest = New RecalculateRequest
                        objRecalculateRequest.Fk_CompanyId = RadCmbBxCompany.SelectedValue
                        objRecalculateRequest.Fk_EntityId = RadCmbBxEntity.SelectedValue
                        objRecalculateRequest.ImmediatelyStart = True
                        objRecalculateRequest.FromDate = dtpFromdate.SelectedDate
                        objRecalculateRequest.Remarks = "Assign Schedule - SYSTEM"
                        If chckTemporary.Checked = True Then
                            If Not dtpEndDate.SelectedDate Is Nothing Or Not dtpEndDate.SelectedDate > Date.Today Then
                                objRecalculateRequest.ToDate = dtpEndDate.SelectedDate
                            End If
                        Else
                            objRecalculateRequest.ToDate = Date.Today
                        End If
                        objRecalculateRequest.CREATED_BY = SessionVariables.LoginUser.FK_EmployeeId
                        errornum2 = objRecalculateRequest.Add()
                    End If
                End If
            Next
        End If

        If errornum = 0 And errornum2 = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            ClearAll()
        ElseIf errornum = -99 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DateExist", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord", CultureInfo), "error")

        End If
        FillGrid()
    End Sub

    Private Sub FillCompanies()
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
    End Sub

    'Private Sub FillEntity()

    '    If RadCmbBxCompany.SelectedValue <> -1 Then
    '        Dim objProjectCommon = New ProjectCommon()
    '        objUserPrivileg_Companies = New UserPrivileg_Companies
    '        objUserPrivileg_Entities = New UserPrivileg_Entities
    '        objSYSUsers = New SYSUsers
    '        objSYSUsers.ID = SessionVariables.LoginUser.ID
    '        objSYSUsers.GetUser()

    '        If Not objSYSUsers.FK_EmployeeId = Nothing Then
    '            objUserPrivileg_Companies.FK_EmployeeId = objSYSUsers.FK_EmployeeId
    '            objUserPrivileg_Companies.GetByEmpId()

    '            objUserPrivileg_Entities.FK_EmployeeId = objSYSUsers.FK_EmployeeId
    '            objUserPrivileg_Entities.GetByEmpId()


    '            If (objUserPrivileg_Companies.FK_CompanyId <> 0 And objUserPrivileg_Entities.FK_EntityId = 0) Then
    '                Dim objOrgEntity = New OrgEntity()
    '                objOrgEntity.FK_CompanyId = RadCmbBxCompany.SelectedValue
    '                Dim dt As DataTable = objOrgEntity.GetAllEntityByCompany()

    '                objProjectCommon.FillMultiLevelRadComboBox(RadCmbBxEntity, dt, "EntityId", _
    '                                                         "EntityName", "EntityArabicName", "FK_ParentId")
    '                RadCmbBxEntity.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem(ResourceManager.GetString("PleaseSelect", CultureInfo), -1))

    '                ''RadCmbBxEntity.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem("--Please Select--", -1))

    '            ElseIf (objUserPrivileg_Companies.FK_CompanyId <> 0 And objUserPrivileg_Entities.FK_EntityId <> 0) Then
    '                CreateEntityTable()
    '            Else
    '                Dim objOrgEntity = New OrgEntity()
    '                objOrgEntity.FK_CompanyId = RadCmbBxCompany.SelectedValue
    '                Dim dt As DataTable = objOrgEntity.GetAllEntityByCompany()
    '                objProjectCommon.FillMultiLevelRadComboBox(RadCmbBxEntity, dt, "EntityId", _
    '                                                     "EntityName", "EntityArabicName", "FK_ParentId")
    '                RadCmbBxEntity.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem(ResourceManager.GetString("PleaseSelect", CultureInfo), -1))
    '                ''RadCmbBxEntity.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem("--Please Select--", -1))
    '            End If
    '        End If
    '    End If
    'End Sub

    Private Sub FillEntity()

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

        CtlCommon.FillTelerikDropDownList(RadCmbBxCompany, dtCompanyInfo, Lang)
        RadCmbBxCompany.SelectedIndex = 1
        RadCmbBxCompany.Enabled = False
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

    Private Sub FillSchedule()
        If SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
            rfvSchedule.InitialValue = "--الرجاء الاختيار--"
        Else
            Lang = CtlCommon.Lang.EN
            rfvSchedule.InitialValue = "--Please Select--"
        End If

        Dim objWorkSchedule As New WorkSchedule()
        If (RadCmbBxScheduletype.SelectedValue = 1) Then
            CtlCommon.FillTelerikDropDownList(RadCmbBxSchedules, objWorkSchedule.GetByType(1), Lang)
        ElseIf (RadCmbBxScheduletype.SelectedValue = 2) Then
            CtlCommon.FillTelerikDropDownList(RadCmbBxSchedules, objWorkSchedule.GetByType(2), Lang)
        ElseIf (RadCmbBxScheduletype.SelectedValue = 3) Then
            CtlCommon.FillTelerikDropDownList(RadCmbBxSchedules, objWorkSchedule.GetByType(3), Lang)
        ElseIf (RadCmbBxScheduletype.SelectedValue = 5) Then
            CtlCommon.FillTelerikDropDownList(RadCmbBxSchedules, objWorkSchedule.GetByType(5), Lang)
        End If


    End Sub

    Private Sub FillGrid()
        objSchedule_Entity = New Schedule_Entity
        dgrdSchedule_Entity.DataSource = objSchedule_Entity.Get_Entity_Schedule_Details
        dgrdSchedule_Entity.DataBind()
    End Sub

    Private Sub ClearAll()
        ' Clear the controls
        If (objVersion.HasMultiCompany() = False) Then
            RadCmbBxEntity.SelectedValue = -1
        Else
            RadCmbBxCompany.SelectedValue = -1
            RadCmbBxEntity.Text = String.Empty
            RadCmbBxEntity.Items.Clear()
        End If

        RadCmbBxCompany.SelectedValue = -1
        RadCmbBxEntity.Text = String.Empty
        RadCmbBxEntity.Items.Clear()
        RadCmbBxSchedules.Text = String.Empty
        RadCmbBxSchedules.Items.Clear()
        RadCmbBxScheduletype.SelectedValue = -1
        chckTemporary.Checked = False
        pnlEndDate.Visible = False
        dtpFromdate.SelectedDate = Today
        dtpEndDate.SelectedDate = Today
        EmpWorkScheduleId = 0
        CompanyId = 0
        EntityId = 0
        ScheduleId = 0
        ManageControls(True)
    End Sub

    Private Sub ManageControls(ByVal status As Boolean)
        RadCmbBxCompany.Enabled = status
        RadCmbBxEntity.Enabled = status
        RadCmbBxSchedules.Enabled = status
        RadCmbBxScheduletype.Enabled = status
    End Sub

    Private Function DateToString(ByVal TempDate As Date) As String
        Dim tempDay As String
        Dim tempMonth As String

        If TempDate.Month.ToString.Length = 1 Then
            tempMonth = "0" + TempDate.Month.ToString
        Else
            tempMonth = TempDate.Month.ToString
        End If
        If TempDate.Day.ToString.Length = 1 Then
            tempDay = "0" + TempDate.Day.ToString
        Else
            tempDay = TempDate.Day.ToString
        End If
        Return TempDate.Year.ToString() + tempMonth + tempDay
    End Function

    Private Sub ArcivingMonths_DateValidation()
        objAPP_Settings = New APP_Settings
        objAPP_Settings.GetByPK()
        Dim ArchivingMonths As Integer = objAPP_Settings.ArchivingMonths

        If Not ArchivingMonths = 0 Then
            ArchivingMonths = ArchivingMonths
            Dim NewDate As Date = DateAdd(DateInterval.Month, ArchivingMonths, Date.Today)
            dtpFromdate.MinDate = New Date(NewDate.Year, NewDate.Month, NewDate.Day)
            dtpEndDate.MinDate = New Date(NewDate.Year, NewDate.Month, NewDate.Day)
        End If
    End Sub

#End Region

#Region "Start & End date validation"

    Private Function ValidateEndDate() As Boolean
        If IS_Temporary() And dtpEndDate.SelectedDate() IsNot Nothing Then
            Return StartEndDateComparison(dtpFromdate.SelectedDate, dtpEndDate.SelectedDate)
        End If
        Return True
    End Function

    Private Function StartEndDateComparison(ByVal dateStartdate As DateTime, _
                                           ByVal dateEndDate As DateTime) As Boolean
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        If IsDate(dtpFromdate.SelectedDate) And IsDate(dtpEndDate.SelectedDate) Then
            ' Get the start and end date from the calenders
            dateStartdate = New DateTime(dtpFromdate.SelectedDate.Value.Year, _
                                         dtpFromdate.SelectedDate.Value.Month, _
                                         dtpFromdate.SelectedDate.Value.Day)
            dateEndDate = New DateTime(dtpEndDate.SelectedDate.Value.Year, _
                                       dtpEndDate.SelectedDate.Value.Month, _
                                       dtpEndDate.SelectedDate.Value.Day)
            Dim result As Integer = DateTime.Compare(dateEndDate, dateStartdate)
            If result < 0 Then
                dtpEndDate.Focus()
                Return False
                'ElseIf result = 0 Then
                '    dtpEndDate.Focus()
                '    Return False
            Else
                ' Do nothing
                Return True
            End If
        End If
    End Function

    Private Function IS_Temporary() As Boolean
        Return chckTemporary.Checked
    End Function

#End Region

    
End Class
