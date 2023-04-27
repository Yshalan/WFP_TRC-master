Imports SmartV.UTILITIES
Imports TA.Security
Imports TA.Admin
Imports TA.Employees
Imports System.Data

Partial Class Employee_ChangeEmployeeEntity
    Inherits System.Web.UI.Page

#Region "Class Variable"
    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private objEmployee As Employee
    Private objCompany As OrgCompany
    Private objEntity As OrgEntity
    Dim objSYSUsers As SYSUsers
    Private objVersion As SmartV.Version.version
    Dim objUserPrivileg_Entities As UserPrivileg_Entities
    Dim objUserPrivileg_Companies As UserPrivileg_Companies
    Private objAPP_Settings As APP_Settings

#End Region

    Public Property EmployeeId() As Integer
        Get
            Return ViewState("EmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeId") = value
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

    Public Property CompanyId() As Integer
        Get
            Return ViewState("CompanyId")
        End Get
        Set(ByVal value As Integer)
            ViewState("CompanyId") = value
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

#Region "Page Events"

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Try
            objEmployee = New Employee
            Dim flag As Boolean = False
            Dim errNumber As Integer = -1

            For Each item As ListItem In cblEmpList.Items
                If item.Selected Then
                    flag = True
                    objEmployee.LAST_UPDATE_BY = SessionVariables.LoginUser.FK_EmployeeId
                    objEmployee.FK_EntityId = RadCmbBxEntity.SelectedValue
                    objEmployee.EmployeeId = item.Value
                    errNumber = objEmployee.ChangeEntity()
                End If
            Next

            If (errNumber < 1) Then
                If flag = False Then
                    CtlCommon.ShowMessage(Page, ResourceManager.GetString("EmployeeSelect", CultureInfo), "info")
                    Exit Sub
                End If
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            Else

                CtlCommon.ShowMessage(Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            End If
        Catch ex As Exception
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End Try
    End Sub

    Protected Sub EmployeeFilter_eventEmployeeSelect(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)

    End Sub

    Protected Sub RadCmbBxCompanies_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxCompanies.SelectedIndexChanged
        FillEntity(Convert.ToInt32(RadCmbBxCompanies.SelectedValue))
    End Sub

    Protected Sub RadToolBar1_ButtonClick(sender As Object, e As RadToolBarEventArgs)

    End Sub

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
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

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                PageHeader1.HeaderText = ResourceManager.GetString("ChangeEmployeeEntity", CultureInfo)
            Else
                Lang = CtlCommon.Lang.EN
                PageHeader1.HeaderText = ResourceManager.GetString("ChangeEmployeeEntity", CultureInfo)

            End If

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

                RadCmbBxCompanies1.Visible = False
                lblCompany.Visible = False
                rfvCompanies.Enabled = False
            Else
                FillCompany()
                Dim RadComboboxCompany As RadComboBox = DirectCast(MultiEmployeeFilterUC.FindControl("RadCmbBxCompany"), RadComboBox)
                Dim RadComboboxCompanyLabel As Label = DirectCast(MultiEmployeeFilterUC.FindControl("lblCompany"), Label)
                RadComboboxCompany.Visible = False
                RadComboboxCompanyLabel.Visible = False
            End If

            FillCompanies()
        End If
    End Sub

    Protected Sub RadCmbBxCompanies1_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxCompanies1.SelectedIndexChanged
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

#End Region

#Region "Methods"

    Private Sub FillCompanies()
        'Get user info and check the user security
        If SessionVariables.LoginUser IsNot Nothing Then
            objSYSUsers = New SYSUsers
            objSYSUsers.ID = SessionVariables.LoginUser.ID
            objSYSUsers.GetUser()
            If objSYSUsers.UserStatus = 1 Then 'System User - Full 
                Dim objOrgCompany As New OrgCompany
                CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies, objOrgCompany.GetAllforddl, Lang)
            ElseIf objSYSUsers.UserStatus = 2 Then
                FillCompanyForUserSecurity()
            Else
                Dim objOrgCompany As New OrgCompany
                CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies, objOrgCompany.GetAllforddl, Lang)
            End If
        End If
        If (objVersion.HasMultiCompany() = False) Then
            RadCmbBxCompanies.SelectedValue = objVersion.GetCompanyId()
        End If
    End Sub

    Private Sub FillCompanyForUserSecurity()
        Try
            Dim objOrgCompany As New OrgCompany
            Dim dtCompanies As New DataTable
            objOrgCompany.FK_UserId = objSYSUsers.ID
            dtCompanies = objOrgCompany.GetAllforddl_ByUserId
            CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies, dtCompanies, Lang)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FillCompanyForUserSecurity1()
        Try
            Dim objOrgCompany As New OrgCompany
            Dim dtCompanies As New DataTable

            objOrgCompany.FK_UserId = objSYSUsers.ID
            objOrgCompany.FilterType = "C"
            dtCompanies = objOrgCompany.GetAllforddl_ByUserId

            CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies1, dtCompanies, Lang)


        Catch ex As Exception

        End Try
    End Sub

    Private Sub FillEntity(ByVal companyId As Integer)
        Dim objProjectCommon = New ProjectCommon()
        Dim objOrgEntity = New OrgEntity()
        objOrgEntity.FK_CompanyId = RadCmbBxCompanies.SelectedValue
        Dim dt As DataTable = objOrgEntity.GetAllEntityByCompany()
        objProjectCommon.FillMultiLevelRadComboBox(RadCmbBxEntity, dt, "EntityId", _
                                                 "EntityName", "EntityArabicName", "FK_ParentId")
        RadCmbBxEntity.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem(ResourceManager.GetString("PleaseSelect", CultureInfo), -1))
    End Sub

    Public Sub CompanyChanged()
        'EmployeeFilterUC.FillEntity()
        MultiEmployeeFilterUC.CompanyID = RadCmbBxCompanies1.SelectedValue
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


            ' fill pager 
            'Dim pagefrom, pageto As Integer
            If Emp_RecordCount > 1000 And EmployeeId = 0 Then

                Dim dtpaging As New DataTable
                dtpaging.Columns.Add("pageNo")
                Dim index As Integer
                Dim empcount As Integer
                'empcount = dt.Rows.Count
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
                'pagefrom = ((PageNo - 1) * 1000) + 1
                'pageto = PageNo * 1000
            End If


            If EmployeeId > 0 Then
                cblEmpList.DataSource = Nothing
                Repeater1.DataSource = Nothing
                Repeater1.DataBind()
            End If
            ' end fill pager


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
                            'If Item + 1 >= pagefrom And Item + 1 <= pageto Then


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
                CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies1, objOrgCompany.GetAllforddl, Lang)
            ElseIf objSYSUsers.UserStatus = 2 Then
                If objAPP_Settings.ShowLGWithEntityPrivilege = False Then
                    FillCompanyForUserSecurity1()
                Else
                    Dim objOrgCompany As New OrgCompany
                    CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies1, objOrgCompany.GetAllforddl, Lang)
                End If

            ElseIf objSYSUsers.UserStatus = 3 Then
                FillCompanyForUserSecurity1()
            Else
                Dim objOrgCompany As New OrgCompany
                CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies1, objOrgCompany.GetAllforddl, Lang)
            End If
        End If
        If (objVersion.HasMultiCompany() = False) Then
            CompanyId = objVersion.GetCompanyId()
            RadCmbBxCompanies.SelectedValue = objVersion.GetCompanyId()
        End If

    End Sub

#End Region

End Class
