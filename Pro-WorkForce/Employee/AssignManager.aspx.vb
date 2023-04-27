Imports SmartV.UTILITIES
Imports TA.Employees
Imports System.Data
Imports TA.Security
Imports TA.Admin

Partial Class Admin_AssignManager
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private objVersion As SmartV.Version.version
    Private objAPP_Settings As APP_Settings
#End Region

#Region "Properties"

    Public Property dtCurrent() As DataTable
        Get
            Return ViewState("dtCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
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

        showHide(chckTemporary.Checked)

        If Not Page.IsPostBack Then
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            EmployeeFilter1.IsEmployeeRequired = True
            EmployeeFilter1.EmployeeRequiredValidationGroup = btnAssign.ValidationGroup
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")

            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                PageHeader1.HeaderText = ResourceManager.GetString("AssignMgr", CultureInfo)
            Else
                Lang = CtlCommon.Lang.EN
                PageHeader1.HeaderText = ResourceManager.GetString("AssignMgr", CultureInfo)

            End If
            Me.dtpFromdate.SelectedDate = Today
            Me.dtpEndDate.SelectedDate = Today
            'FillSchedule()

            'EmployeeFilterUC.CompanyRequiredFieldValidationGroup = btnAssign.ValidationGroup


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

                'MultiEmployeeFilterUC.RadCmbBxCompanies.Visible = False
                'MultiEmployeeFilterUC.lblCompany.Visible = False
                'MultiEmployeeFilterUC.rfvCompanies.Enabled = False
            Else
                CompanyChanged()
            End If
        End If
        If chckTemporary.Checked = True Then

        Else

        End If



        'EmployeeFilterUC.CompanyRequiredFieldValidationGroup = btnSave.ValidationGroup
        'EmployeeFilterUC.CompanyValidationGroup = btnSave.ValidationGroup


        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not pnlAssignEmployees.FindControl(row("AddBtnName")) Is Nothing Then
                        pnlAssignEmployees.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not pnlAssignEmployees.FindControl(row("DeleteBtnName")) Is Nothing Then
                        pnlAssignEmployees.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not pnlAssignEmployees.FindControl(row("EditBtnName")) Is Nothing Then
                        pnlAssignEmployees.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not pnlAssignEmployees.FindControl(row("PrintBtnName")) Is Nothing Then
                        pnlAssignEmployees.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

    End Sub

    Protected Sub btnAssign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAssign.Click
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim errornum As Integer

        Dim flag As Boolean = False

        For Each item As ListItem In cblEmpList.Items
            If item.Selected = True Then
                flag = True
                Dim objEmployee_Manager As New Employee_Manager
                objEmployee_Manager.FK_EmployeeId = item.Value
                objEmployee_Manager.FK_ManagerId = EmployeeFilter1.EmployeeId
                objEmployee_Manager.FromDate = dtpFromdate.SelectedDate
                objEmployee_Manager.ToDate = IIf(dtpEndDate.SelectedDate Is Nothing Or Not chckTemporary.Checked, DateTime.MinValue, dtpEndDate.SelectedDate)
                objEmployee_Manager.IsTemporary = chckTemporary.Checked

                errornum = objEmployee_Manager.AssignManager()

            End If
        Next


        If errornum = 0 Then

            If flag = False Then
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("EmployeeSelect", CultureInfo), "info")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
                ClearAll()
            End If


        ElseIf errornum = -99 Then

            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DateExist", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")

        End If
    End Sub

#End Region

#Region "Methods"

    Public Sub CompanyChanged()
        'EmployeeFilterUC.FillEntity()
        'MultiEmployeeFilterUC.CompanyID = EmployeeFilter1.CompanyId
        'MultiEmployeeFilterUC.FillList()
        'FillEmployee()
        MultiEmployeeFilterUC.FillCompany()
    End Sub

    Public Sub OtherCompanyChanged()

        MultiEmployeeFilterUC.FillList()
        objAPP_Settings = New APP_Settings
        With objAPP_Settings
            .GetByPK()
            If .FillCheckBoxList = 1 Then
                FillEmployee()
            End If
        End With

    End Sub

    Public Sub EntityChanged()
        If Not MultiEmployeeFilterUC.CompanyID = -1 Then
            FillEmployee()
        Else
            MultiEmployeeFilterUC.ClearValues()
        End If


    End Sub

    Public Sub WorkGroupChanged()
        If Not MultiEmployeeFilterUC.CompanyID = -1 Then
            FillEmployee()
        Else
            MultiEmployeeFilterUC.ClearValues()
        End If

    End Sub

    Public Sub WorkLocationsChanged()
        If Not MultiEmployeeFilterUC.CompanyID = -1 Then
            FillEmployee()
        Else
            MultiEmployeeFilterUC.ClearValues()
        End If

    End Sub

    Public Sub FillEmployee()
        If PageNo = 0 Then
            PageNo = 1
        End If
        Repeater1.Visible = False
        cblEmpList.Items.Clear()
        cblEmpList.Text = String.Empty
        hlViewEmployee.Visible = False
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
            'Dim dt As DataTable =  = objEmployee.GetEmpByCompany
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
            End If
            If MultiEmployeeFilterUC.EmployeeID > 0 Then
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

                        Dim dv As New DataView(dtSource)
                        If SessionVariables.CultureInfo = "ar-JO" Then
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

    Private Sub showHide(ByVal status As Boolean, Optional ByVal order As Integer = 0)

        pnlEndDate.Visible = status
    End Sub

    Private Sub ClearAll()
        ' Clear the controls
        MultiEmployeeFilterUC.ClearValues()
        chckTemporary.Checked = False
        cblEmpList.Items.Clear()
        pnlEndDate.Visible = False
        dtpFromdate.SelectedDate = Today
        dtpEndDate.SelectedDate = Today
        EmployeeFilter1.ClearValues()

    End Sub

    Protected Sub Repeater1_ItemCommand(source As Object, e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles Repeater1.ItemCommand

        Dim page As Integer = CInt(CType(e.CommandSource, LinkButton).Text)
        PageNo = page
        FillEmployee()

    End Sub
#End Region


End Class
