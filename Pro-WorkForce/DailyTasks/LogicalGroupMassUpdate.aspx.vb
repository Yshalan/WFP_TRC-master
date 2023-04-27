﻿Imports SmartV.UTILITIES
Imports TA.Admin
Imports TA.DailyTasks
Imports System.Data
Imports TA.Employees
Imports TA.Lookup
Imports TA.Definitions
Imports TA.Security
Imports SmartV.Version
Imports System.Reflection

Partial Class DailyTasks_LogicalGroupMassUpdate
    Inherits System.Web.UI.Page

#Region "ClassVariables"

    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Dim objEmpLogicalGroup As New Emp_logicalGroup
    Dim objEmaolyee As New Employee
    Private objVersion As SmartV.Version.version
    Private objOrgCompany As OrgCompany
    Private objSYSUsers As SYSUsers
    Private objAPP_Settings As APP_Settings
    Private objEmployee As Employee

#End Region

#Region "Page Propereties"

    Public Property dtCurrent() As DataTable
        Get
            Return ViewState("dtCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
        End Set
    End Property

    Public Property CheckedEmployees() As List(Of Boolean)
        Get
            Return ViewState("CheckedEmployees")
        End Get
        Set(ByVal value As List(Of Boolean))
            ViewState("CheckedEmployees") = value
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
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                rfvLogicalGroup.InitialValue = "--الرجاء الاختيار--"
            Else
                Lang = CtlCommon.Lang.EN
            End If
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            PageHeader1.HeaderText = ResourceManager.GetString("LogicalGroup", CultureInfo)
            rfvCompanies.InitialValue = ResourceManager.GetString("PleaseSelect", CultureInfo)
            FillLogicalGroup()

            ' EmployeeFilterUC.CompanyRequiredFieldValidationGroup = btnSave.ValidationGroup
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
            End If
        End If

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not UpdatePanel1.FindControl(row("AddBtnName")) Is Nothing Then
                        UpdatePanel1.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not UpdatePanel1.FindControl(row("DeleteBtnName")) Is Nothing Then
                        UpdatePanel1.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not UpdatePanel1.FindControl(row("EditBtnName")) Is Nothing Then
                        UpdatePanel1.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not UpdatePanel1.FindControl(row("PrintBtnName")) Is Nothing Then
                        UpdatePanel1.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next


    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click

        'To check if the user didn't change any checkbox selection
        Dim checkBoxChanged As Integer = 0
        For x As Integer = 0 To CheckedEmployees.Count - 1
            If CheckedEmployees(x) <> cblEmpList.Items(x).Selected Then
                checkBoxChanged += 1
            End If
        Next

        'To show a message if the user didn't change anything and clicked assign
        If checkBoxChanged = 0 Then
            If SessionVariables.CultureInfo = "ar-JO" Then
                CtlCommon.ShowMessage(Page, "لم تقم بآي تعدييل!")
            Else
                CtlCommon.ShowMessage(Page, "You didn't make any change!")
            End If
            Return
        End If


        Dim errornum As Integer
        Dim dt As DataTable
        objEmaolyee = New Employee()
        With objEmaolyee
            .FK_LogicalGroup = RadCmbBxLogicalGroup.SelectedValue
            '.FK_EntityId = EmployeeFilterUC.EntityID
            .FK_EntityId = MultiEmployeeFilterUC.EntityID
            dt = .GetByLogicalGroup
        End With

        For Each item As ListItem In cblEmpList.Items
            With objEmaolyee
                .EmployeeId = item.Value
                If (item.Selected) Then
                    .FK_LogicalGroup = RadCmbBxLogicalGroup.SelectedValue
                    errornum += .AssignEmployeeLogicalGroup()
                Else
                    'this is for checkbox that was selected and then unselected before saving
                    For Each row In dt.Rows
                        If item.Value = row("EmployeeId") Then
                            .FK_LogicalGroup = Nothing
                            errornum += .AssignEmployeeLogicalGroup()
                        End If
                    Next
                End If
            End With
        Next

        If errornum = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("UpdatedSuccessfully"), "success")
            ClearAll()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate"), "Error")
        End If

    End Sub

    Protected Sub RadComboLogicalGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadCmbBxLogicalGroup.SelectedIndexChanged
        FillEmployeeByLogicalGroup()
    End Sub

    Protected Sub RadCmbBxCompanies_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxCompanies.SelectedIndexChanged
        If Not RadCmbBxCompanies.SelectedValue = -1 Then
            CompanyChanged()
        Else
            MultiEmployeeFilterUC.EmployeeID = 0
            CompanyChanged()
        End If
    End Sub

#End Region

#Region "Methods"
    'Private Sub FillCompanies()
    '    'Get user info and check the user security
    '    If SessionVariables.LoginUser IsNot Nothing Then
    '        objSYSUsers = New SYSUsers
    '        objSYSUsers.ID = SessionVariables.LoginUser.ID
    '        objSYSUsers.GetUser()
    '        If objSYSUsers.UserStatus = 1 Then 'System User - Full 
    '            Dim objOrgCompany As New OrgCompany
    '            CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies, objOrgCompany.GetAllforddl, Lang)
    '        ElseIf objSYSUsers.UserStatus = 2 Then
    '            FillCompanyForUserSecurity()
    '        Else
    '            Dim objOrgCompany As New OrgCompany
    '            CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies, objOrgCompany.GetAllforddl, Lang)
    '        End If
    '    End If
    'End Sub
    'Private Sub FillCompanyForUserSecurity()
    '    Try
    '        Dim objOrgCompany As New OrgCompany
    '        Dim dtCompanies As New DataTable

    '        objOrgCompany.FK_UserId = objSYSUsers.ID
    '        objOrgCompany.FilterType = "C"
    '        'objOrgCompany.FilterType = rblSearchType.SelectedValue
    '        dtCompanies = objOrgCompany.GetAllforddl_ByUserId

    '        CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies, dtCompanies, Lang)


    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub FillCompany()
        Dim dt As DataTable
        objOrgCompany = New OrgCompany
        dt = objOrgCompany.GetAllforddl
        CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies, dt, Lang)

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
            objEmployee.Status = 1
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

            'fill pager 
            'Dim pagefrom, pageto As Integer
            If Emp_RecordCount > 1000 And MultiEmployeeFilterUC.EmployeeID = 0 Then
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
                Repeater1.Visible = True
            Else
                Repeater1.Visible = False
            End If
            If MultiEmployeeFilterUC.EmployeeID > 0 Then
                cblEmpList.DataSource = Nothing
                Repeater1.DataSource = Nothing
                Repeater1.DataBind()
            End If
            ' end fill pager
            Dim dtEmployees = dt
            If (dtEmployees IsNot Nothing) Then
                If (dtEmployees.Rows.Count > 0) Then
                    'Repeater1.Visible = True
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

                        'ElseIf MultiEmployeeFilterUC.EmployeeID > 0 Then
                        '    Dim drSource As DataRow
                        '    drSource = dtSource.NewRow
                        '    Dim dcCell1 As New DataColumn
                        '    Dim dcCell2 As New DataColumn
                        '    dcCell1.ColumnName = "EmployeeId"
                        '    dcCell2.ColumnName = "EmployeeName"
                        '    dcCell1.DefaultValue = dtEmployees.Rows(Item)(0)
                        '    dcCell2.DefaultValue = dtEmployees.Rows(Item)(3) + "-" + IIf(Lang = CtlCommon.Lang.EN, dtEmployees.Rows(Item)(1), dtEmployees.Rows(Item)(2))
                        '    drSource("EmployeeId") = dcCell1.DefaultValue
                        '    drSource("EmployeeName") = dcCell2.DefaultValue
                        '    dtSource.Rows.Add(drSource)

                        'Else
                        '    Dim drSource As DataRow
                        '    drSource = dtSource.NewRow
                        '    Dim dcCell1 As New DataColumn
                        '    Dim dcCell2 As New DataColumn
                        '    dcCell1.ColumnName = "EmployeeId"
                        '    dcCell2.ColumnName = "EmployeeName"
                        '    dcCell1.DefaultValue = dtEmployees.Rows(Item)(0)
                        '    dcCell2.DefaultValue = dtEmployees.Rows(Item)(3) + "-" + IIf(Lang = CtlCommon.Lang.EN, dtEmployees.Rows(Item)(1), dtEmployees.Rows(Item)(2))
                        '    drSource("EmployeeId") = dcCell1.DefaultValue
                        '    drSource("EmployeeName") = dcCell2.DefaultValue
                        '    dtSource.Rows.Add(drSource)

                        'End If
                    Next

                    If (dt IsNot Nothing) Then
                        Dim dv As New DataView(dtSource)
                        If SessionVariables.CultureInfo = "ar-JO" Then
                            'dv.Sort = "EmployeeArabicName"
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
            FillEmployeeByLogicalGroup()
        End If
    End Sub

    Private Sub FillLogicalGroup()

        objEmpLogicalGroup = New Emp_logicalGroup()
        Dim dt As New DataTable
        dt = objEmpLogicalGroup.GetAll()
        CtlCommon.FillTelerikDropDownList(RadCmbBxLogicalGroup, dt, Lang)

    End Sub

    Public Sub CompanyChanged()
        MultiEmployeeFilterUC.CompanyID = RadCmbBxCompanies.SelectedValue
        If Not MultiEmployeeFilterUC.CompanyID = -1 Then
            'EmployeeFilterUC.FillEntity()
            MultiEmployeeFilterUC.FillList()
            FillEmployee()
        Else
            ClearAll()
            MultiEmployeeFilterUC.ClearValues()

        End If
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

    Private Sub ClearAll()
        ' Clear the controls
        MultiEmployeeFilterUC.ClearValues()
        'EmployeeFilterUC.ClearValues()
        RadCmbBxLogicalGroup.SelectedValue = -1
        cblEmpList.Items.Clear()
        MultiEmployeeFilterUC.ClearValues()
        If (objVersion.HasMultiCompany() = False) Then
            MultiEmployeeFilterUC.CompanyID = objVersion.GetCompanyId()
            MultiEmployeeFilterUC.FillList()
            FillEmployee()

            RadCmbBxCompanies.Visible = False
            lblCompany.Visible = False
            rfvCompanies.Enabled = False
        Else
            FillCompany()
        End If
    End Sub

    Sub FillEmployeeByLogicalGroup()

        objEmaolyee = New Employee()
        Dim dt As DataTable

        With objEmaolyee

            If RadCmbBxLogicalGroup.Items.Count > 0 Then
                If Not String.IsNullOrEmpty(RadCmbBxLogicalGroup.SelectedValue) Then
                    .FK_LogicalGroup = RadCmbBxLogicalGroup.SelectedValue
                End If
            End If

            '.FK_EntityId = EmployeeFilterUC.EntityId
            .FK_EntityId = MultiEmployeeFilterUC.EntityID

            dt = .GetByLogicalGroup()
        End With

        cblEmpList.ClearSelection()
        For Each row As DataRow In dt.Rows
            If cblEmpList.Items.FindByValue(row("EmployeeId")) IsNot Nothing Then
                cblEmpList.Items.FindByValue(row("EmployeeId")).Selected = True
            End If
        Next

        'To fill the CheckedEmployees property
        CheckedEmployees = New List(Of Boolean)
        For Each item As ListItem In cblEmpList.Items
            If item.Selected = True Then
                CheckedEmployees.Add(True)
            Else
                CheckedEmployees.Add(False)
            End If
        Next

    End Sub

    Protected Sub Repeater1_ItemCommand(source As Object, e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles Repeater1.ItemCommand

        Dim page As Integer = CInt(CType(e.CommandSource, LinkButton).Text)
        PageNo = page
        FillEmployee()

    End Sub
#End Region

End Class