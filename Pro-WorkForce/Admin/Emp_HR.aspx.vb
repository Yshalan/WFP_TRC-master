Imports System.Data
Imports TA.Lookup
Imports TA.Definitions
Imports SmartV.DB
Imports SmartV.UTILITIES
Imports TA.Employees
Imports Telerik.Web.UI
Imports TA.Admin
Imports TA.Security
Imports SmartV.UTILITIES.ProjectCommon

Partial Class Admin_Emp_HR
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Dim objEmpHR As Emp_HR
    Dim objNotificationType As NotificationType
    Dim objEmpHRNotificationType As Emp_HR_NotificationTypes
    Private objEmpDesignation As Emp_Designation
    Shared SortExepression As String
    Shared dtCurrent As DataTable
    Private objEMP_HR_Entity As EMP_HR_Entity
    Private objOrgCompany As OrgCompany
    Private objEmp_HR_Company As Emp_HR_Company
    Private objVersion As SmartV.Version.version

#End Region

#Region "Properties"

    Public Property HREmployeeId() As Integer
        Get
            Return ViewState("HREmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("HREmployeeId") = value
        End Set
    End Property

    Public Property Action() As Integer
        Get
            Return ViewState("Action")
        End Get
        Set(ByVal value As Integer)
            ViewState("Action") = value
        End Set
    End Property

    Public Property HRDesignation() As Integer
        Get
            Return ViewState("HRDesignation")
        End Get
        Set(ByVal value As Integer)
            ViewState("HRDesignation") = value
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
        If Not Page.IsPostBack Then
            EmployeeFilterUC.IsLevelRequired = True
            EmployeeFilterUC.IsEmployeeRequired = True
            EmployeeFilterUC.CompanyRequiredValidationGroup = btnSubmit.ValidationGroup
            EmployeeFilterUC.EntityRequiredValidationGroup = btnSubmit.ValidationGroup
            EmployeeFilterUC.EmployeeRequiredValidationGroup = btnSubmit.ValidationGroup

            PageHeader1.HeaderText = ResourceManager.GetString("EmpHR", CultureInfo)
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            mvEmp_HR.SetActiveView(viewEmpHRList)
            mvEmp_HR.ActiveViewIndex = 0
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                dgrdEmpHR.Columns(2).Visible = False
                dgrdEmpHR.Columns(4).Visible = False
            Else
                Lang = CtlCommon.Lang.EN
                dgrdEmpHR.Columns(3).Visible = False
                dgrdEmpHR.Columns(5).Visible = False
            End If
            FillGrid()
            FillList()
            FillCompanies()
            FillEntities()
            'FillEntities()
            If (objVersion.HasMultiCompany() = False) Then
                rdlIsSpecific.Items.RemoveAt(1)
            End If
        End If

        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdEmpHR.ClientID + "')")
        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not divControls.FindControl(row("AddBtnName")) Is Nothing Then
                        divControls.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not divControls.FindControl(row("DeleteBtnName")) Is Nothing Then
                        divControls.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not divControls.FindControl(row("EditBtnName")) Is Nothing Then
                        divControls.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not divControls.FindControl(row("PrintBtnName")) Is Nothing Then
                        divControls.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

    End Sub

    Protected Sub btn_Click(ByVal sender As Object, ByVal e As EventArgs)

        btnSubmit.Text = ResourceManager.GetString("btnUpdate", CultureInfo)

        Action = 2
        mvEmp_HR.SetActiveView(viewEmpHREdit)
        mvEmp_HR.ActiveViewIndex = 1
        EmployeeFilterUC.EnabledDisbaledControls(False)
        Dim dt As DataTable
        objEmpHR = New Emp_HR()
        objEmpHRNotificationType = New Emp_HR_NotificationTypes()
        Dim gvr As GridDataItem = CType(sender, LinkButton).Parent.Parent
        HREmployeeId = dgrdEmpHR.Items(gvr.ItemIndex).GetDataKeyValue("HREmployeeId")
        'HRDesignation = dgrdEmpHR.Items(gvr.ItemIndex)("HRDesignation").Text
        'RadCmbDesignation.Items.FindItemByValue(HRDesignation.ToString()).Selected = True
        EmployeeFilterUC.IsEntityClick = "True"

        EmployeeFilterUC.GetEmployeeInfo(HREmployeeId)

        'FillList()
        With objEmpHRNotificationType
            .FK_HREmployeeId = HREmployeeId
            dt = .GetByHREmpID()

            For Each row As DataRow In dt.Rows
                chlNotificationType.Items.FindByValue(row("FK_NotificationTypeId")).Selected = True
            Next
        End With

        objEmpHR = New Emp_HR
        objEMP_HR_Entity = New EMP_HR_Entity
        objEmp_HR_Company = New Emp_HR_Company
        With objEmpHR
            .HREmployeeId = HREmployeeId
            .GetByPK()


            If .IsSpecificEntity = True Then
                rdlIsSpecific.SelectedValue = 1 '---Specific Entity---'
                divEntities.Visible = True
                divEntitiesSelect.Visible = True
                With objEMP_HR_Entity
                    Dim dtEMP_HR_Entity As New DataTable
                    .FK_HREmployeeId = HREmployeeId
                    dtEMP_HR_Entity = .GetByHREmployeeId()
                    cblEntities.Items.Clear()
                    FillEntities()
                    If Not dtEMP_HR_Entity Is Nothing And dtEMP_HR_Entity.Rows.Count > 0 Then
                        For Each dr As DataRow In dtEMP_HR_Entity.Rows
                            For Each EntityItem As ListItem In cblEntities.Items
                                If EntityItem.Value = dr(1) Then
                                    EntityItem.Selected = True
                                End If
                            Next
                        Next

                    End If
                End With
            ElseIf .IsSpecificCompany = True Then
                If Not (objVersion.HasMultiCompany() = False) Then
                    rdlIsSpecific.SelectedValue = 2 '---Specific Company---'
                    divCompanies.Visible = True
                    divCompaniesSelect.Visible = True
                    With objEmp_HR_Company
                        Dim dtEmp_HR_Company As New DataTable
                        .FK_HREmployeeId = HREmployeeId
                        dtEmp_HR_Company = .GetBy_FK_HREmployeeId()
                        cblCompanies.Items.Clear()
                        FillCompanies()
                        If Not dtEmp_HR_Company Is Nothing And dtEmp_HR_Company.Rows.Count > 0 Then
                            For Each dr As DataRow In dtEmp_HR_Company.Rows
                                For Each CompanyItem As ListItem In cblCompanies.Items
                                    If CompanyItem.Value = dr(1) Then
                                        CompanyItem.Selected = True
                                    End If
                                Next
                            Next

                        End If
                    End With
                End If
            Else
                rdlIsSpecific.SelectedValue = 0
            End If
        End With

    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click
        Dim errNo As Integer

        objEMP_HR_Entity = New EMP_HR_Entity
        objEmp_HR_Company = New Emp_HR_Company

        If rdlIsSpecific.SelectedValue = 2 Then '---Specific Company---'
            Dim CompanyIsChecked As Boolean = False
            For Each CompanyItem As ListItem In cblCompanies.Items
                If CompanyItem.Selected Then
                    CompanyIsChecked = True
                    Exit For
                End If
            Next

            If Not CompanyIsChecked Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    CtlCommon.ShowMessage(Me.Page, "الرجاء اختيار شركة واحدة او اكثر", "info")
                Else
                    CtlCommon.ShowMessage(Me.Page, "Please Select Company(s)", "info")
                End If

                Return
            End If
        End If

        If rdlIsSpecific.SelectedValue = 1 Then '---Specific Entity---'
            Dim EntityIsChecked As Boolean = False
            For Each EntityItem As ListItem In cblEntities.Items
                If EntityItem.Selected Then
                    EntityIsChecked = True
                    Exit For
                End If
            Next

            If Not EntityIsChecked Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    CtlCommon.ShowMessage(Me.Page, "الرجاء اختيار وحدة عمل او اكثر", "info")
                Else
                    CtlCommon.ShowMessage(Me.Page, "Please Select Entity(s)", "info")
                End If

                Return
            End If
        End If

        If (Action = 1) Then
            objEmpHR = New Emp_HR
            objEmpHRNotificationType = New Emp_HR_NotificationTypes
            With objEmpHR
                .HREmployeeId = EmployeeFilterUC.EmployeeId
                .HRDesignation = RadCmbDesignation.SelectedValue
                If rdlIsSpecific.SelectedValue = 1 Then '---Specific Entity---'
                    .IsSpecificEntity = True
                ElseIf rdlIsSpecific.SelectedValue = 2 Then '---Specific Company---'
                    .IsSpecificCompany = True
                Else
                    .IsSpecificCompany = False
                    .IsSpecificEntity = False
                End If
                errNo = .Insert()
            End With
            ''ADD ON EMP_HR_ENTITY
            With objEmpHRNotificationType
                For i As Integer = 0 To chlNotificationType.Items.Count - 1
                    .FK_HREmployeeId = EmployeeFilterUC.EmployeeId
                    If chlNotificationType.Items(i).Selected Then
                        .FK_NotificationTypeId = chlNotificationType.Items(i).Value
                        .Add()
                    End If
                Next
            End With

            If errNo = 0 Then
                If rdlIsSpecific.SelectedValue = 1 Then '---Specific Entity---'
                    For Each EntityItem As ListItem In cblEntities.Items
                        If EntityItem.Selected Then
                            With objEMP_HR_Entity
                                .FK_Entity = CInt(EntityItem.Value)
                                .FK_HREmployeeId = EmployeeFilterUC.EmployeeId
                                .Add()
                            End With
                        End If
                    Next

                ElseIf rdlIsSpecific.SelectedValue = 2 Then '---Specific Company---'

                    For Each CompanyItem As ListItem In cblCompanies.Items
                        If CompanyItem.Selected Then
                            With objEmp_HR_Company
                                .FK_CompanyId = CInt(CompanyItem.Value)
                                .FK_HREmployeeId = EmployeeFilterUC.EmployeeId
                                .Add()
                            End With
                        End If

                    Next

                End If
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
                Clear()
                FillGrid()
            ElseIf errNo = -5 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CodeExist", CultureInfo), "info")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If
        ElseIf Action = 2 Then
            objEmpHR = New Emp_HR
            objEmpHRNotificationType = New Emp_HR_NotificationTypes
            rfvEmployeeDesigion.Enabled = False
            With objEmpHR
                .HREmployeeId = EmployeeFilterUC.EmployeeId
                If rdlIsSpecific.SelectedValue = 1 Then '---Specific Entity---'
                    .IsSpecificEntity = True
                ElseIf rdlIsSpecific.SelectedValue = 2 Then '---Specific Company---'
                    .IsSpecificCompany = True
                Else
                    .IsSpecificCompany = False
                    .IsSpecificEntity = False
                End If

                'If (Not RadCmbDesignation.SelectedValue = "-1") Then
                '.HRDesignation = RadCmbDesignation.SelectedValue
                errNo = .Update()
                'End If
            End With

            With objEmpHRNotificationType
                .FK_HREmployeeId = EmployeeFilterUC.EmployeeId
                .Delete()
                For i As Integer = 0 To chlNotificationType.Items.Count - 1
                    If (chlNotificationType.Items(i).Selected) Then
                        .FK_NotificationTypeId = chlNotificationType.Items(i).Value
                        .Add()
                    End If
                Next
            End With
            If errNo = 0 Then
                If rdlIsSpecific.SelectedValue = 1 Then
                    objEmp_HR_Company.FK_HREmployeeId = EmployeeFilterUC.EmployeeId
                    objEmp_HR_Company.DeleteBy_FK_HREmployeeId()
                    objEMP_HR_Entity.FK_HREmployeeId = EmployeeFilterUC.EmployeeId
                    objEMP_HR_Entity.DeleteBy_FK_HREmployeeId()
                    For Each EntityItem As ListItem In cblEntities.Items
                        If EntityItem.Selected Then
                            With objEMP_HR_Entity
                                .FK_Entity = CInt(EntityItem.Value)
                                .FK_HREmployeeId = EmployeeFilterUC.EmployeeId

                                .Add()
                            End With
                        End If
                    Next
                ElseIf rdlIsSpecific.SelectedValue = 2 Then
                    objEmp_HR_Company.FK_HREmployeeId = EmployeeFilterUC.EmployeeId
                    objEmp_HR_Company.DeleteBy_FK_HREmployeeId()
                    objEMP_HR_Entity.FK_HREmployeeId = EmployeeFilterUC.EmployeeId
                    objEMP_HR_Entity.DeleteBy_FK_HREmployeeId()
                    For Each CompanyItem As ListItem In cblCompanies.Items
                        If CompanyItem.Selected Then
                            With objEmp_HR_Company
                                .FK_CompanyId = CInt(CompanyItem.Value)
                                .FK_HREmployeeId = EmployeeFilterUC.EmployeeId
                                .Add()
                            End With
                        End If

                    Next
                End If
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
                Clear()
                FillGrid()
            ElseIf errNo = -5 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CodeExist", CultureInfo), "info")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If
        End If

    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click

        btnSubmit.Text = ResourceManager.GetString("btnAdd", CultureInfo)

        Action = 1
        mvEmp_HR.SetActiveView(viewEmpHREdit)
        mvEmp_HR.ActiveViewIndex = 1
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        Dim err As Integer
        Dim counter As Integer = 0
        objEmpHR = New Emp_HR
        objEmp_HR_Company = New Emp_HR_Company
        objEMP_HR_Entity = New EMP_HR_Entity

        objEmpHRNotificationType = New Emp_HR_NotificationTypes
        For Each row As GridDataItem In dgrdEmpHR.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                counter = counter + 1
                HREmployeeId = Convert.ToInt32(row.GetDataKeyValue("HREmployeeId"))
                With objEmpHRNotificationType
                    .FK_HREmployeeId = HREmployeeId ' EmployeeFilterUC.EmployeeId
                    .Delete()
                End With

                With objEmp_HR_Company
                    .FK_HREmployeeId = HREmployeeId
                    .DeleteBy_FK_HREmployeeId()
                End With

                With objEMP_HR_Entity
                    .FK_HREmployeeId = HREmployeeId
                    .DeleteBy_FK_HREmployeeId()
                End With

                With objEmpHR
                    .HREmployeeId = HREmployeeId ' EmployeeFilterUC.EmployeeId
                    err = .Delete()
                End With
            End If
        Next
        If counter = 0 Then
            CtlCommon.ShowMessage(Me.Page, IIf(Lang = CtlCommon.Lang.AR, "الرجاء اختيار الموظف المراد حذفه", " Please Select Employee to delete"), "info")
            Return
        End If
        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            Clear()
            FillGrid()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")
        End If
    End Sub

    Protected Sub dgrdEmpHR_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdEmpHR.NeedDataSource
        objEmpHR = New Emp_HR
        dtCurrent = objEmpHR.GetAll()
        Dim dv As New DataView(dtCurrent)
        dv.Sort = SortExepression
        dgrdEmpHR.DataSource = dv
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Clear()
    End Sub

    Protected Sub rdlIsSpecificCompany_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rdlIsSpecific.SelectedIndexChanged
        If rdlIsSpecific.SelectedValue = 1 Then '---Specific Entity---'
            divEntities.Visible = True
            divEntitiesSelect.Visible = True
            divCompanies.Visible = False
            divCompaniesSelect.Visible = False
            cblEntities.Items.Clear()
            FillEntities()

        ElseIf rdlIsSpecific.SelectedValue = 2 Then '---Specific Company---'
            divCompanies.Visible = True
            divCompaniesSelect.Visible = True
            divEntities.Visible = False
            divEntitiesSelect.Visible = False
            cblCompanies.Items.Clear()
            FillCompanies()
        Else
            divCompanies.Visible = False
            divCompaniesSelect.Visible = False
            divEntities.Visible = False
            divEntitiesSelect.Visible = False
        End If
    End Sub

#End Region

#Region "Method"

    Private Sub FillGrid()
        objEmpHR = New Emp_HR
        dgrdEmpHR.DataSource = objEmpHR.GetAll()
        dgrdEmpHR.DataBind()
    End Sub

    Public Sub FilllDesigion()
        Try
            objEmpHR = New Emp_HR
            With objEmpHR
                HREmployeeId = EmployeeFilterUC.EmployeeId
                .HREmployeeId = EmployeeFilterUC.EmployeeId
                .GetByPK()
                RadCmbDesignation.Items.FindItemByValue(.HRDesignation).Selected = True
            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FillList()
        objNotificationType = New NotificationType
        CtlCommon.FillCheckBox(chlNotificationType, objNotificationType.GetAll())

        For i As Integer = 0 To chlNotificationType.Items.Count - 1
            chlNotificationType.Items(i).Text = " " + chlNotificationType.Items(i).Text
        Next

        objEmpDesignation = New Emp_Designation()
        ProjectCommon.FillRadComboBox(RadCmbDesignation, objEmpDesignation.GetAll(), _
                                      "DesignationName", "DesignationArabicName")
    End Sub

    Public Sub Clear()
        RadCmbDesignation.ClearSelection()
        chlNotificationType.ClearSelection()
        mvEmp_HR.SetActiveView(viewEmpHRList)
        mvEmp_HR.ActiveViewIndex = 0
        Action = 0
        HREmployeeId = 0
        EmployeeFilterUC.ClearValues()
        rdlIsSpecific.SelectedValue = 0
        cblCompanies.Items.Clear()
        cblEntities.Items.Clear()
        divEntities.Visible = False
        divEntitiesSelect.Visible = False
        divCompanies.Visible = False
        divCompaniesSelect.Visible = False
    End Sub

    Public Sub FillEntities()
        'cblEmpList.Items.Clear()
        'cblEmpList.Text = String.Empty

        Dim objOrgEntity As New OrgEntity

        If (objVersion.HasMultiCompany() = False) Then
            objOrgEntity.FK_CompanyId = objVersion.GetCompanyId()
        End If

        Dim dt As DataTable = objOrgEntity.GetAll_WithCompanyName
        If (dt IsNot Nothing) Then
            Dim dtEntities = dt
            If (dtEntities IsNot Nothing) Then
                If (dtEntities.Rows.Count > 0) Then
                    Dim dtSource As New DataTable
                    dtSource.Columns.Add("EntityId")
                    dtSource.Columns.Add("EntityName")
                    Dim drRow As DataRow
                    drRow = dtSource.NewRow()
                    For Item As Integer = 0 To dtEntities.Rows.Count - 1
                        Dim drSource As DataRow
                        drSource = dtSource.NewRow
                        Dim dcCell1 As New DataColumn
                        Dim dcCell2 As New DataColumn
                        dcCell1.ColumnName = "EntityId"
                        dcCell2.ColumnName = "EntityName"
                        dcCell1.DefaultValue = dtEntities.Rows(Item)(0)
                        dcCell2.DefaultValue = IIf(Lang = CtlCommon.Lang.EN, dtEntities.Rows(Item)(1), dtEntities.Rows(Item)(2))
                        drSource("EntityId") = dcCell1.DefaultValue
                        drSource("EntityName") = dcCell2.DefaultValue
                        dtSource.Rows.Add(drSource)
                    Next
                    Dim dv As New DataView(dtSource)
                    If SessionVariables.CultureInfo = "ar-JO" Then
                        For Each row As DataRowView In dv
                            cblEntities.Items.Add(New ListItem(row("EntityName").ToString(), row("EntityId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
                        Next
                    Else
                        For Each row As DataRowView In dv
                            cblEntities.Items.Add(New ListItem(row("EntityName").ToString(), row("EntityId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
                        Next
                    End If
                End If
            End If
        End If


    End Sub

    Public Sub FillCompanies()
        'cblEmpList.Items.Clear()
        'cblEmpList.Text = String.Empty

        Dim objOrgCompany As New OrgCompany

        Dim dt As DataTable = objOrgCompany.GetAll
        If (dt IsNot Nothing) Then
            Dim dtCompanies = dt
            If (dtCompanies IsNot Nothing) Then
                If (dtCompanies.Rows.Count > 0) Then
                    Dim dtSource As New DataTable
                    dtSource.Columns.Add("CompanyId")
                    dtSource.Columns.Add("CompanyName")
                    Dim drRow As DataRow
                    drRow = dtSource.NewRow()
                    For Item As Integer = 0 To dtCompanies.Rows.Count - 1
                        Dim drSource As DataRow
                        drSource = dtSource.NewRow
                        Dim dcCell1 As New DataColumn
                        Dim dcCell2 As New DataColumn
                        dcCell1.ColumnName = "CompanyId"
                        dcCell2.ColumnName = "CompanyName"
                        dcCell1.DefaultValue = dtCompanies.Rows(Item)(0)
                        dcCell2.DefaultValue = IIf(Lang = CtlCommon.Lang.EN, dtCompanies.Rows(Item)(1), dtCompanies.Rows(Item)(2))
                        drSource("CompanyId") = dcCell1.DefaultValue
                        drSource("CompanyName") = dcCell2.DefaultValue
                        dtSource.Rows.Add(drSource)
                    Next
                    Dim dv As New DataView(dtSource)
                    If SessionVariables.CultureInfo = "ar-JO" Then
                        For Each row As DataRowView In dv
                            cblCompanies.Items.Add(New ListItem(row("CompanyName").ToString(), row("CompanyId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
                        Next
                    Else
                        For Each row As DataRowView In dv
                            cblCompanies.Items.Add(New ListItem(row("CompanyName").ToString(), row("CompanyId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
                        Next
                    End If
                End If
            End If
        End If


    End Sub

#End Region

#Region "Grid Filter"

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdEmpHR.Skin))
    End Function

    Protected Sub dgrdEmpHR_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdEmpHR.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

#End Region

End Class
