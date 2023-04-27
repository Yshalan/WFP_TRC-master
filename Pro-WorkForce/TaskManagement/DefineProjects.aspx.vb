Imports System.Data
Imports TA.Definitions
Imports SmartV.UTILITIES
Imports Telerik.Web.UI.DatePickerPopupDirection
Imports TA.Security
Imports TA.Admin
Imports TA.Employees
Imports TA.TaskManagement

Partial Class Definitions_DefineProjects
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Private objProjects As Projects
    Private objVersion As SmartV.Version.version
    Private objOrgCompany As OrgCompany
    Private objAPP_Settings As APP_Settings
    Private objSYSUsers As SYSUsers
    Private objEmployee As Employee
    Private objProject_Resources As Project_Resources
    Private objEmp_Designation As Emp_Designation

#End Region

#Region "Public Properties"

    Public Property ProjectId() As Integer
        Get
            Return ViewState("ProjectId")
        End Get
        Set(ByVal value As Integer)
            ViewState("ProjectId") = value
        End Set
    End Property

    Public Property PageNo() As Integer
        Get
            Return ViewState("PageNo")
        End Get
        Set(ByVal value As Integer)
            ViewState("PageNo") = value
        End Set
    End Property

    Public Property Emp_RecordCount() As Integer
        Get
            Return ViewState("Emp_RecordCount")
        End Get
        Set(ByVal value As Integer)
            ViewState("Emp_RecordCount") = value
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

    Public Property CompanyId() As Integer
        Get
            Return ViewState("CompanyId")
        End Get
        Set(ByVal value As Integer)
            ViewState("CompanyId") = value
        End Set
    End Property

    Public Property dtResources() As DataTable
        Get
            Return ViewState("dtResources")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtResources") = value
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

    Protected Sub Definitions_DefineProjects_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            dtpPlannedStart.SelectedDate = Today.Date
            dtpPlannedEnd.SelectedDate = Today.Date

            'dtpActualStart.SelectedDate = Today.Date
            'dtpActualEnd.SelectedDate = Today.Date

            If (objVersion.HasMultiCompany() = False) Then
                MultiEmployeeFilterUC.CompanyID = objVersion.GetCompanyId()
                MultiEmployeeFilterUC.FillList()
                objAPP_Settings = New APP_Settings
                With objAPP_Settings
                    .GetByPK()
                    If .FillCheckBoxList = 1 Then
                        FillEmployee()
                    End If
                End With

                RadCmbBxCompanies.Visible = False
                lblCompany.Visible = False
                rfvCompanies.Enabled = False
            Else
                FillCompany()
                Dim RadComboboxCompany As RadComboBox = DirectCast(MultiEmployeeFilterUC.FindControl("RadCmbBxCompany"), RadComboBox)
                Dim RadComboboxCompanyLabel As Label = DirectCast(MultiEmployeeFilterUC.FindControl("lblCompany"), Label)
                RadComboboxCompany.Visible = False
                RadComboboxCompanyLabel.Visible = False
            End If

            FillDesignation()
            FillGrid()
        End If
        PageHeader1.HeaderText = ResourceManager.GetString("DefineProjects", CultureInfo)
    End Sub

    Protected Sub dgrdProjects_NeedDataSource1(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdProjects.NeedDataSource
        Dim dt As DataTable
        objProjects = New Projects
        dt = objProjects.GetAll
        dgrdProjects.DataSource = dt
    End Sub

    Protected Sub dgrdProjects_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgrdProjects.SelectedIndexChanged
        ProjectId = CInt(CType(dgrdProjects.SelectedItems(0), GridDataItem).GetDataKeyValue("ProjectId").ToString())
        FillControls()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        objProjects = New Projects
        Dim err As Integer = -1
        Dim err2 As Integer = -1

        With objProjects
            .ProjectName = txtProjectName.Text
            .ProjectArabicName = txtProjectArabicName.Text
            .Project_Description = txtProjectDesc.Text
            .Project_ArabicDescription = txtProjectArabicDesc.Text
            .PlannedStartDate = dtpPlannedStart.SelectedDate
            .PlannedEndDate = dtpPlannedEnd.SelectedDate
            .ActualStartDate = dtpActualStart.DbSelectedDate
            .ActualEndDate = dtpActualEnd.DbSelectedDate


            If ProjectId = 0 Then
                .CREATED_BY = SessionVariables.LoginUser.ID
                err = .Add
                ProjectId = .ProjectId
            Else
                .ProjectId = ProjectId
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                err = .Update
            End If

        End With

        If Not dtResources Is Nothing Then
            If dtResources.Rows.Count > 0 Then
                objProject_Resources = New Project_Resources
                For Each item In dtResources.Rows
                    objProject_Resources.FK_EmployeeId = item("FK_EmployeeId")
                    objProject_Resources.FK_ProjectId = ProjectId
                    objProject_Resources.FK_DesignationId = item("FK_DesignationId")
                    objProject_Resources.RoleId = item("FK_DesignationId")
                    err2 = objProject_Resources.Add
                Next
            End If
        End If

        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            FillGrid()
            ClearAll()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearAll()
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim err As Integer = -1
        objProjects = New Projects

        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdProjects.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim intProjectId As Integer = Convert.ToInt32(row.GetDataKeyValue("ProjectId").ToString())
                objProjects.ProjectId = intProjectId
                err = objProjects.Delete()
            End If
        Next
        If err = 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillGrid()
        Else
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")
        End If
        ClearAll()
        FillGrid()
    End Sub

    Protected Sub RadCmbBxCompanies_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxCompanies.SelectedIndexChanged
        If Not RadCmbBxCompanies.SelectedValue = -1 Then
            CompanyChanged()
        Else
            MultiEmployeeFilterUC.EmployeeID = 0
            CompanyChanged()
        End If

    End Sub

    Protected Sub Repeater1_ItemCommand(source As Object, e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles Repeater1.ItemCommand

        Dim page As Integer = CInt(CType(e.CommandSource, LinkButton).Text)
        PageNo = page
        FillEmployee()

    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim dr As DataRow
        objEmployee = New Employee
        objEmp_Designation = New Emp_Designation
        dtResources = New DataTable

        Dim err As Integer = -1
        Dim flag As Boolean = False

        For Each item As ListItem In cblEmpList.Items
            Dim EmployeeName As String = String.Empty
            Dim EmployeeArabicName As String = String.Empty
            Dim EmployeeNo As String = String.Empty
            Dim FK_DesignationId As Integer = 0
            Dim DesignationName As String = String.Empty
            Dim DesignationArabicName As String = String.Empty
            If item.Selected Then
                flag = True

                objEmployee.EmployeeId = item.Value
                objEmployee.GetByPK()
                EmployeeNo = objEmployee.EmployeeNo
                EmployeeName = objEmployee.EmployeeName
                EmployeeArabicName = objEmployee.EmployeeArabicName

                If Not radcmbxResourceRole.SelectedValue = -1 Then
                    objEmp_Designation.DesignationId = radcmbxResourceRole.SelectedValue
                    objEmp_Designation.GetByPK()
                    FK_DesignationId = objEmp_Designation.DesignationId
                    DesignationName = objEmp_Designation.DesignationName
                    DesignationArabicName = objEmp_Designation.DesignationArabicName
                End If


                If dtResources.Rows.Count < 0 Then
                    dtResources.Columns.Add("Index")
                    dtResources.Columns.Add("FK_EmployeeId")
                    dtResources.Columns.Add("EmployeeNo")
                    dtResources.Columns.Add("EmployeeName")
                    dtResources.Columns.Add("EmployeeArabicName")
                    dtResources.Columns.Add("FK_DesignationId")
                    dtResources.Columns.Add("DesignationName")
                    dtResources.Columns.Add("DesignationArabicName")
                End If

                dr = dtResources.NewRow
                dr("FK_EmployeeId") = item.Value
                dr("EmployeeNo") = EmployeeNo
                dr("EmployeeName") = EmployeeName
                dr("EmployeeArabicName") = EmployeeArabicName
                dr("FK_DesignationId") = FK_DesignationId
                dr("DesignationName") = DesignationName
                dr("DesignationArabicName") = DesignationArabicName
                dtResources.Rows.Add(dr)

            End If
        Next
        FillResourcesGrid()
    End Sub

    Protected Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        Dim err As Integer = -1
        objProjects = New Projects

        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdResources.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim intFK_EmployeeId As Integer = Convert.ToInt32(row.GetDataKeyValue("FK_EmployeeId").ToString())
                objProjects.ProjectId = intFK_EmployeeId
                dtResources.Rows.Remove(dtResources.Select("FK_EmployeeId = " & intFK_EmployeeId)(0))
            End If
        Next
        FillResourcesGrid()
    End Sub

#End Region

#Region "Methods"

    Private Sub FillControls()
        objProjects = New Projects
        With objProjects
            .ProjectId = ProjectId
            .GetByPK()
            txtProjectName.Text = .ProjectName
            txtProjectArabicName.Text = .ProjectArabicName
            txtProjectDesc.Text = .Project_Description
            txtProjectArabicDesc.Text = .Project_ArabicDescription
            dtpPlannedStart.SelectedDate = .PlannedStartDate
            dtpPlannedEnd.SelectedDate = .PlannedEndDate
            dtpActualStart.SelectedDate = IIf(.ActualStartDate = DateTime.MinValue, Nothing, .ActualStartDate)
            dtpActualEnd.SelectedDate = IIf(.ActualEndDate = DateTime.MinValue, Nothing, .ActualEndDate)
        End With
    End Sub

    Private Sub ClearAll()
        txtProjectName.Text = String.Empty
        txtProjectArabicName.Text = String.Empty
        txtProjectDesc.Text = String.Empty
        txtProjectArabicDesc.Text = String.Empty
        dtpPlannedStart.SelectedDate = Date.Today
        dtpPlannedEnd.SelectedDate = Date.Today
        dtpActualStart.SelectedDate = Nothing
        dtpActualEnd.SelectedDate = Nothing
        RadCmbBxCompanies.SelectedValue = -1
        cblEmpList.Items.Clear()
        MultiEmployeeFilterUC.ClearValues()
        ProjectId = 0
        dtResources.Clear()
        FillResourcesGrid()
        TabContainer1.ActiveTabIndex = 0
        radcmbxResourceRole.SelectedValue = -1
    End Sub

    Private Sub FillGrid()

        Dim dt As DataTable
        objProjects = New Projects
        dt = objProjects.GetAll
        dgrdProjects.DataSource = dt
        dgrdProjects.DataBind()

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdProjects.Skin))
    End Function

    Protected Function GetFilterIcon1() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdResources.Skin))
    End Function

    Public Sub FillEmployee()
        If PageNo = 0 Then
            PageNo = 1
        End If
        Repeater1.Visible = False
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
            objEmployee.EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            objEmployee.Get_EmployeeRecordCount()
            Emp_RecordCount = objEmployee.Emp_RecordCount
            Dim dt As DataTable

            If MultiEmployeeFilterUC.EmployeeID = 0 Then
                objEmployee.PageNo = PageNo
                objEmployee.PageSize = 1000
                dt = objEmployee.GetEmpByCompany
            Else
                objEmployee.EmployeeId = MultiEmployeeFilterUC.EmployeeID
                dt = objEmployee.GetByEmpId()
            End If

            If Emp_RecordCount > 1000 And EmployeeId = 0 Then

                Dim dtpaging As New DataTable
                dtpaging.Columns.Add("pageNo")
                Dim index As Integer
                Dim empcount As Integer
                empcount = Emp_RecordCount
                For index = 0 To empcount Step 1000
                    Dim drpaging As DataRow
                    drpaging = dtpaging.NewRow
                    Dim dcCell3 As New DataColumn
                    dcCell3.ColumnName = "PageNo"
                    drpaging(0) = index / 1000 + 1
                    dtpaging.Rows.Add(drpaging)

                Next
                Repeater1.DataSource = dtpaging
                Repeater1.DataBind()
                For Each item2 As RepeaterItem In Repeater1.Items
                    Dim lnk As LinkButton = DirectCast(item2.FindControl("LinkButton1"), LinkButton)
                    If lnk.Text = PageNo Then
                        lnk.Attributes.Add("style", "color:Blue;font-weight:bold;text-decoration: underline")
                    End If
                Next
            End If


            If EmployeeId > 0 Then
                cblEmpList.DataSource = Nothing
                Repeater1.DataSource = Nothing
                Repeater1.DataBind()
            End If

            If (dt IsNot Nothing) Then
                Dim dtEmployees = dt
                If (dtEmployees IsNot Nothing) Then
                    If (dtEmployees.Rows.Count > 0) Then
                        Repeater1.Visible = True
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
                            For Each row As DataRowView In dv
                                If (Not MultiEmployeeFilterUC.EmployeeID = 0) AndAlso MultiEmployeeFilterUC.SearchType = "4" Then
                                    If MultiEmployeeFilterUC.EmployeeID = row("EmployeeId") Then
                                        cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString()))
                                        Exit For
                                    End If
                                Else
                                    cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString()))
                                End If
                            Next
                        Else
                            For Each row As DataRowView In dv
                                If (Not MultiEmployeeFilterUC.EmployeeID = 0) AndAlso MultiEmployeeFilterUC.SearchType = "4" Then
                                    If MultiEmployeeFilterUC.EmployeeID = row("EmployeeId") Then
                                        cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString()))
                                        Exit For
                                    End If
                                Else
                                    cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString()))
                                End If
                            Next
                        End If
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub FillCompany()
        'Dim dt As DataTable
        'objOrgCompany = New OrgCompany
        'dt = objOrgCompany.GetAllforddl
        'CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies, dt, Lang)

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

    End Sub

    Private Sub FillCompanyForUserSecurity()
        Try
            Dim objOrgCompany As New OrgCompany
            Dim dtCompanies As New DataTable

            objOrgCompany.FK_UserId = objSYSUsers.ID
            objOrgCompany.FilterType = "C"
            dtCompanies = objOrgCompany.GetAllforddl_ByUserId

            CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies, dtCompanies, Lang)


        Catch ex As Exception

        End Try
    End Sub

    Public Sub CompanyChanged()
        'EmployeeFilterUC.FillEntity()
        MultiEmployeeFilterUC.CompanyID = RadCmbBxCompanies.SelectedValue
        MultiEmployeeFilterUC.FillList()
        FillEmployee()
    End Sub

    Public Sub EntityChanged()

        If Not MultiEmployeeFilterUC.CompanyID = Nothing Or Not MultiEmployeeFilterUC.CompanyID = -1 Then
            PageNo = 0
            FillEmployee()
        Else
            Repeater1.Visible = False
        End If

    End Sub

    Public Sub WorkGroupChanged()

        If Not MultiEmployeeFilterUC.CompanyID = Nothing Or Not MultiEmployeeFilterUC.CompanyID = -1 Then
            PageNo = 0
            FillEmployee()
        Else
            Repeater1.Visible = False
        End If

    End Sub

    Public Sub WorkLocationsChanged()

        If Not MultiEmployeeFilterUC.CompanyID = Nothing Or Not MultiEmployeeFilterUC.CompanyID = -1 Then
            PageNo = 0
            FillEmployee()
        Else
            Repeater1.Visible = False
        End If

    End Sub

    Private Sub FillResourcesGrid_FromDB()
        objProject_Resources = New Project_Resources
        With objProject_Resources
            'get by project id

        End With
    End Sub

    Private Sub FillDesignation()
        Dim dt As DataTable
        objEmp_Designation = New Emp_Designation
        dt = objEmp_Designation.GetAll
        CtlCommon.FillTelerikDropDownList(radcmbxResourceRole, dt, Lang)
    End Sub

    Private Sub FillResourcesGrid()
        dgrdResources.DataSource = dtResources
        dgrdResources.DataBind()

    End Sub

#End Region

End Class
