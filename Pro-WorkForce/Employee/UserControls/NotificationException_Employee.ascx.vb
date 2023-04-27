Imports SmartV.UTILITIES
Imports TA.Employees
Imports System.Data
Imports TA.Admin

Partial Class Employee_UserControls_NotificationException_Employee
    Inherits System.Web.UI.UserControl

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private objVersion As SmartV.Version.version
    Private objOrgCompany As OrgCompany
    Private objAPP_Settings As APP_Settings
    Private objNotification_Exception As Notification_Exception
    Private objEmployee As Employee

#End Region

#Region "Public Properties"

    Public Property NotificationExceptionId() As Integer
        Get
            Return ViewState("NotificationExceptionId")
        End Get
        Set(ByVal value As Integer)
            ViewState("NotificationExceptionId") = value
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

    Public Property FromDate() As DateTime
        Get
            Return ViewState("FromDate")
        End Get
        Set(ByVal value As DateTime)
            ViewState("FromDate") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        ElseIf SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
        Else
            Lang = CtlCommon.Lang.EN
        End If
    End Sub

    Protected Sub NotificationExceptions_Employee_Load(sender As Object, e As EventArgs) Handles Me.Load
        showHide(chckTemporary.Checked)
        If Not Page.IsPostBack Then
            Me.dtpFromdate.SelectedDate = Today
            Me.dtpEndDate.SelectedDate = Today

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

        End If
    End Sub

    Protected Sub Repeater1_ItemCommand(source As Object, e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles Repeater1.ItemCommand

        Dim page As Integer = CInt(CType(e.CommandSource, LinkButton).Text)
        PageNo = page
        FillEmployee()

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        objNotification_Exception = New Notification_Exception
        Dim errornum As Integer = -1
        Dim flag As Boolean = False
        With objNotification_Exception
            If NotificationExceptionId = 0 Then
                For Each item As ListItem In cblEmpList.Items
                    If item.Selected = True Then
                        flag = True
                        .FK_EmployeeId = item.Value
                        .FromDate = dtpFromdate.SelectedDate
                        If chckTemporary.Checked Then
                            .ToDate = dtpEndDate.SelectedDate
                            pnlEndDate.Visible = True
                        Else
                            .ToDate = Nothing
                            pnlEndDate.Visible = False
                        End If

                        .Reason = txtReason.Text
                        .CREATED_BY = SessionVariables.LoginUser.ID
                        .FK_LogicalGroupId = Nothing
                        .FK_WorkLocationId = Nothing
                        .FK_EntityId = Nothing
                        errornum = .Add()
                        If errornum <> 0 Then
                            Exit For
                        End If
                    End If

                Next
            Else
                flag = True
                .NotificationExceptionId = NotificationExceptionId
                .FK_EmployeeId = EmployeeId
                .FromDate = dtpFromdate.SelectedDate
                If chckTemporary.Checked Then
                    .ToDate = dtpEndDate.SelectedDate
                    pnlEndDate.Visible = True
                Else
                    .ToDate = Nothing
                    pnlEndDate.Visible = False
                End If
                .Reason = txtReason.Text
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                .FK_LogicalGroupId = Nothing
                .FK_WorkLocationId = Nothing
                .FK_EntityId = Nothing
                errornum = .Update()
            End If

        End With
        If flag = False Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("EmployeeSelect", CultureInfo), "info")
        Else
            If errornum = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
                ClearAll()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "close", "Refresh();", True)
                'Response.Redirect("../Employee/NotificationExceptions_MultiSelection.aspx")
            ElseIf errornum = -99 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DateExist", CultureInfo), "info")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If
        End If

    End Sub

    Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearAll()
    End Sub

#End Region

#Region "Methods"

    Public Sub FillEmployee()
        If PageNo = 0 Then
            PageNo = 1
        End If
        Repeater1.Visible = False
        cblEmpList.Items.Clear()
        cblEmpList.Text = String.Empty
        Dim EmpEditable As Boolean = True
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
                EmpEditable = False
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
                            For Each row As DataRowView In dv
                                If (Not MultiEmployeeFilterUC.EmployeeID = 0) AndAlso MultiEmployeeFilterUC.SearchType = "4" Then
                                    If MultiEmployeeFilterUC.EmployeeID = row("EmployeeId") Then
                                        cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString()))
                                        Exit For
                                    End If
                                Else
                                    cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString()))
                                End If
                                If EmpEditable = False Then
                                    cblEmpList.Enabled = False
                                    For Each item In cblEmpList.Items
                                        If item.value = EmployeeId Then
                                            item.selected = True
                                        End If
                                    Next
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
                                If EmpEditable = False Then
                                    cblEmpList.Enabled = False
                                    For Each item In cblEmpList.Items
                                        If item.value = EmployeeId Then
                                            item.selected = True
                                        End If
                                    Next
                                End If
                            Next
                        End If
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub FillCompany()
        Dim dt As DataTable
        objOrgCompany = New OrgCompany
        dt = objOrgCompany.GetAllforddl
        CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies, dt, Lang)
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

    Private Sub ClearAll()
        RadCmbBxCompanies.SelectedValue = -1
        RadCmbBxCompanies.Enabled = True
        MultiEmployeeFilterUC.ClearValues()
        dtpFromdate.SelectedDate = Date.Today
        dtpEndDate.SelectedDate = Date.Today
        pnlEndDate.Visible = False
        chckTemporary.Checked = False
        txtReason.Text = String.Empty
        cblEmpList.Items.Clear()

    End Sub

    Public Sub FillControls()
        objNotification_Exception = New Notification_Exception
        objEmployee = New Employee
        objEmployee.EmployeeId = EmployeeId
        objEmployee.GetByPK()
        With objNotification_Exception
            .NotificationExceptionId = NotificationExceptionId
            .GetByPK()
            dtpFromdate.SelectedDate = .FromDate
            If Not .ToDate = Nothing Then
                dtpEndDate.SelectedDate = .ToDate
                chckTemporary.Checked = True
            Else
                chckTemporary.Checked = False
            End If
            txtReason.Text = .Reason
            CompanyId = objEmployee.FK_CompanyId
            RadCmbBxCompanies.SelectedValue = CompanyId
            MultiEmployeeFilterUC.CompanyID = objEmployee.FK_CompanyId
            MultiEmployeeFilterUC.EmployeeID = EmployeeId
            FillEmployee()
            ManipulateControls(False)
        End With
    End Sub

    Private Sub showHide(ByVal status As Boolean, Optional ByVal order As Integer = 0)

        pnlEndDate.Visible = status

        If (status) Then
            rfvToDate.Enabled = True
            ValidatorCalloutExtender2.Enabled = True
        Else
            rfvToDate.Enabled = False
            ValidatorCalloutExtender2.Enabled = False
        End If

    End Sub

    Private Sub ManipulateControls(ByVal Status As Boolean)

        RadCmbBxCompanies.Enabled = Status

    End Sub

#End Region

    
End Class
