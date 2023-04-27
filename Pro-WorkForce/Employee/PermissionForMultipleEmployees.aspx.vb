Imports TA.Employees
Imports System.Data
Imports SmartV.UTILITIES
Imports Telerik.Web
Imports Telerik.Web.UI
Imports Telerik.Web.UI.DatePickerPopupDirection
Imports System.Web
Imports Telerik.Web.UI.Calendar
Imports SmartV.UTILITIES.ProjectCommon
Imports TA.Lookup
Imports TA.Definitions
Imports System.IO
Imports TA.Security
Imports TA.DailyTasks
Imports TA.Admin

Partial Class Employee_PermissionForMultipleEmployees
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Enum DisplayModeEnum
        Add
        Edit
        View
        ViewAll
        ViewAddEdit
    End Enum

    Public Enum PermissionOption
        Normal = 1
        Nursing = 2
        Study = 3
    End Enum

    Private Lang As CtlCommon.Lang
    Private objEmployee As Employee
    Private objPermissionsTypes As PermissionsTypes
    Private objEmpPermissions As Emp_Permissions
    Shared SortDir As String
    Shared SortExep As String
    Shared SortExepression As String
    Shared dtCurrent As New DataTable
    Private objPermissionTypeOccurance As New PermissionTypeOccurance
    Private objPermissionTypeDuration As New PermissionTypeDuration
    Private objWorkSchedule_NormalTime As WorkSchedule_NormalTime
    Private objWorkSchedule_Flexible As WorkSchedule_Flexible
    Private objEmpWorkSchedule As Emp_WorkSchedule
    Private objWorkSchedule As WorkSchedule
    Private objEmp_Shifts As Emp_Shifts
    Private objHoliday As New Holiday
    Private objEmp_Leaves_BalanceHistory As New Emp_Leaves_BalanceHistory
    Private objEmp_Leaves As Emp_Leaves
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Dim objWeekDays As New WeekDays
    Dim babyBirthDate As DateTime
    Private objRECALC_REQUEST As RECALC_REQUEST
    Private objVersion As SmartV.Version.version
    Private objOrgCompany As OrgCompany
    Private objSYSUsers As SYSUsers
    Private objAPP_Settings As APP_Settings
    Private objRecalculateRequest As RecalculateRequest

#End Region

#Region "Properties"


    Public Property PermissionIdProp() As Integer
        Get
            Return ViewState("PersmissionId")
        End Get
        Set(ByVal value As Integer)
            ViewState("PersmissionId") = value
        End Set
    End Property

    Public Property AllowedTime() As Integer
        Get
            Return ViewState("AllowedTime")
        End Get
        Set(ByVal value As Integer)
            ViewState("AllowedTime") = value
        End Set
    End Property

    Public Property DisplayMode() As DisplayModeEnum
        Get
            Return ViewState("DisplayMode")
        End Get
        Set(ByVal value As DisplayModeEnum)
            ViewState("DisplayMode") = value
        End Set
    End Property

    Public Property dtOffDays() As DataTable
        Get
            Return ViewState("dtOffDays")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtOffDays") = value
        End Set
    End Property

    Public Property OffAndHolidayDays() As Integer
        Get
            Return ViewState("OffAndHolidayDays")
        End Get
        Set(ByVal value As Integer)
            ViewState("OffAndHolidayDays") = value
        End Set
    End Property

    Public Property PermissionDaysCount() As Double
        Get
            Return ViewState("PermissionDaysCount")
        End Get
        Set(ByVal value As Double)
            ViewState("PermissionDaysCount") = value
        End Set
    End Property

    Public Property EmpLeaveTotalBalance() As Double
        Get
            Return ViewState("EmpLeaveTotalBalance")
        End Get
        Set(ByVal value As Double)
            ViewState("EmpLeaveTotalBalance") = value
        End Set
    End Property

    Public Property LeaveID() As Integer
        Get
            Return ViewState("LeaveID")
        End Get
        Set(ByVal value As Integer)
            ViewState("LeaveID") = value
        End Set
    End Property

    Public Property ValidationGroup() As String
        Get
            Return ViewState("ValidationGroup")
        End Get
        Set(ByVal value As String)
            ViewState("ValidationGroup") = value
        End Set
    End Property

    Public Property PermissionType() As PermissionOption
        Get
            Return ViewState("PermissionType")
        End Get

        Set(ByVal value As PermissionOption)
            ViewState("PermissionType") = value
        End Set
    End Property

    Public Property FileExtension() As String
        Get
            Return ViewState("FileExtension")
        End Get
        Set(ByVal value As String)
            ViewState("FileExtension") = value
        End Set
    End Property

    Public Property dtCurrentControls() As DataTable
        Get
            Return ViewState("dtCurrentControls")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrentControls") = value
        End Set
    End Property

    Public Property LeaveTypeDuration() As Integer
        Get
            Return ViewState("LeaveTypeDuration")
        End Get
        Set(ByVal value As Integer)
            ViewState("LeaveTypeDuration") = value
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

        If Not Page.IsPostBack Then
            ValidationGroup = btnSave.ValidationGroup
            SetRadDateTimePickerPeoperties()
            ManageDisplayMode_ExceptionalCases()
            rmtFlexibileTime.TextWithLiterals = "0000"
            PermissionType = PermissionOption.Normal

            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            PageHeader1.HeaderText = ResourceManager.GetString("MultiplePermissions", CultureInfo)
            reqPermission.InitialValue = ResourceManager.GetString("PleaseSelect", CultureInfo)
            FillLists()
            ManageFunctionalities()

            CtlCommon.FillCheckBox(chkWeekDays, objWeekDays.GetAll())

            For i As Integer = 0 To chkWeekDays.Items.Count - 1
                chkWeekDays.Items(i).Text = String.Format("{0} {1}", " ", chkWeekDays.Items(i).Text)
            Next

            rdlTimeOption.Items.FindByValue("0").Selected = True
            trFlixibleTime.Style("display") = "none"
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
            'EmployeeFilterUC.CompanyRequiredFieldValidationGroup = btnSave.ValidationGroup
            'EmployeeFilterUC.CompanyValidationGroup = btnSave.ValidationGroup
        End If

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrentControls = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrentControls.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not pnlMultiPerm.FindControl(row("AddBtnName")) Is Nothing Then
                        pnlMultiPerm.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not pnlMultiPerm.FindControl(row("DeleteBtnName")) Is Nothing Then
                        pnlMultiPerm.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not pnlMultiPerm.FindControl(row("EditBtnName")) Is Nothing Then
                        pnlMultiPerm.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not pnlMultiPerm.FindControl(row("PrintBtnName")) Is Nothing Then
                        pnlMultiPerm.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

        ' You can put the suitable caption for the TimeView
        RadTPfromTime.TimeView.HeaderText = String.Empty
        RadTPtoTime.TimeView.HeaderText = String.Empty
        RadTPfromTime.TimeView.TimeFormat = "HH:mm"
        RadTPfromTime.TimeView.DataBind()
        RadTPtoTime.TimeView.TimeFormat = "HH:mm"
        RadTPtoTime.TimeView.DataBind()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)

        'If hdnIsvalid.Value <> String.Empty Then
        '    If (hdnIsvalid.Value) Then

        '    Else
        '        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("TimeRange", CultureInfo))
        '        Return
        '    End If
        'End If

        'If dtpEndDatePerm.SelectedDate IsNot Nothing And IS_Period() Then
        '    If Not StartEndDateComparison(dtpStartDatePerm.SelectedDate, _
        '                                    dtpEndDatePerm.SelectedDate) Then
        '        Return
        '    End If
        'End If

        Select Case DisplayMode.ToString
            Case "Add"
                saveUpdate()
            Case "Edit"
                updateOnly()
            Case "ViewAddEdit"
                saveUpdate()
            Case Else
        End Select
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, _
                                 ByVal e As System.EventArgs) Handles btnClear.Click
        ClearAll()
        ' EmployeeFilterUC.ClearValues()
    End Sub

    Protected Sub chckFullDay_CheckedChanged(ByVal sender As Object, _
                                             ByVal e As System.EventArgs) _
                                             Handles chckFullDay.CheckedChanged
        DisableEnableTimeView(Not chckFullDay.Checked)
        rdlTimeOption.Enabled = Not chckFullDay.Checked

    End Sub

    Protected Sub rdlTimeOption_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If (rdlTimeOption.SelectedValue = "1") Then
            trSpecificTime.Style("display") = "none"
            trFlixibleTime.Style("display") = "block"

            reqFlexibiletime.Enabled = True
            ExtenderFlexibileTime.Enabled = True
            reqFromtime.Enabled = False
            ExtenderFromTime.Enabled = False
            reqToTime.Enabled = False
            ExtenderreqToTime.Enabled = False
            lblPeriodInterval.Visible = False
            txtTimeDifference.Visible = False
            CustomValidator1.Enabled = False
            CustomValidator2.Enabled = False
        Else
            trSpecificTime.Style("display") = "block"
            trFlixibleTime.Style("display") = "none"

            reqFlexibiletime.Enabled = False
            ExtenderFlexibileTime.Enabled = False
            reqFromtime.Enabled = True
            ExtenderFromTime.Enabled = True
            reqToTime.Enabled = True
            ExtenderreqToTime.Enabled = True
            lblPeriodInterval.Visible = True
            txtTimeDifference.Visible = True
            CustomValidator1.Enabled = True
            CustomValidator2.Enabled = True
        End If
    End Sub

    Protected Sub RadCmbBxCompanies_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxCompanies.SelectedIndexChanged
        CompanyChanged()
    End Sub

    Protected Sub RadCmpPermissions_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadCmpPermissions.SelectedIndexChanged
        If Not RadCmpPermissions.SelectedValue = -1 Then
            objPermissionsTypes = New PermissionsTypes()
            With objPermissionsTypes
                .PermId = RadCmpPermissions.SelectedValue
                .GetByPK()
                If Not .FK_LeaveIdtoallowduration = 0 Then
                    LeaveTypeDuration = .DurationAllowedwithleave
                End If
            End With
        End If
    End Sub

#End Region

#Region "Methods"
    Private Sub FillCompany()
        Dim dt As DataTable
        objOrgCompany = New OrgCompany
        dt = objOrgCompany.GetAllforddl
        CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies, dt, Lang)

    End Sub

    'Public Sub FillEmployee()
    '    If PageNo = 0 Then
    '        PageNo = 1
    '    End If

    '    cblEmpList.Items.Clear()
    '    cblEmpList.Text = String.Empty

    '    If MultiEmployeeFilterUC.CompanyID <> 0 Then
    '        Dim objEmployee As New Employee
    '        objEmployee.FK_CompanyId = MultiEmployeeFilterUC.CompanyID

    '        If (Not MultiEmployeeFilterUC.EntityID = 0) AndAlso MultiEmployeeFilterUC.SearchType = "1" Then
    '            objEmployee.FK_EntityId = MultiEmployeeFilterUC.EntityID
    '        Else
    '            objEmployee.FK_EntityId = -1
    '        End If

    '        If (Not MultiEmployeeFilterUC.WorkGroupID = 0) AndAlso MultiEmployeeFilterUC.SearchType = "2" Then
    '            objEmployee.FK_LogicalGroup = MultiEmployeeFilterUC.WorkGroupID
    '            objEmployee.FilterType = "L"
    '        Else
    '            objEmployee.FK_LogicalGroup = -1
    '        End If

    '        If (Not MultiEmployeeFilterUC.WorkLocationsID) AndAlso MultiEmployeeFilterUC.SearchType = "3" Then
    '            objEmployee.FK_WorkLocation = MultiEmployeeFilterUC.WorkLocationsID
    '            objEmployee.FilterType = "W"
    '        Else
    '            objEmployee.FK_WorkLocation = -1
    '        End If

    '        Dim dt As DataTable = objEmployee.GetEmpByCompany


    '        ' fill pager 
    '        Dim pagefrom, pageto As Integer
    '        If dt.Rows.Count > 1000 And MultiEmployeeFilterUC.EmployeeID = 0 Then


    '            Dim dtpaging As New DataTable
    '            dtpaging.Columns.Add("pageNo")
    '            Dim index As Integer
    '            Dim empcount As Integer
    '            empcount = dt.Rows.Count

    '            For index = 0 To empcount Step 1000
    '                Dim drpaging As DataRow
    '                drpaging = dtpaging.NewRow
    '                Dim dcCell3 As New DataColumn
    '                dcCell3.ColumnName = "PageNo"
    '                drpaging(0) = index / 1000 + 1
    '                dtpaging.Rows.Add(drpaging)

    '            Next



    '            Repeater1.DataSource = dtpaging
    '            Repeater1.DataBind()


    '            For Each item2 As RepeaterItem In Repeater1.Items
    '                Dim lnk As LinkButton = DirectCast(item2.FindControl("LinkButton1"), LinkButton)
    '                If lnk.Text = PageNo Then
    '                    lnk.Attributes.Add("style", "color:Blue;font-weight:bold;text-decoration: underline")
    '                End If
    '            Next

    '            pagefrom = ((PageNo - 1) * 1000) + 1
    '            pageto = PageNo * 1000
    '        End If
    '        If MultiEmployeeFilterUC.EmployeeID > 0 Then
    '            cblEmpList.DataSource = Nothing
    '            Repeater1.DataSource = Nothing
    '            Repeater1.DataBind()
    '        End If
    '        ' end fill pager

    '        If (dt IsNot Nothing) Then
    '            Dim dtEmployees = dt
    '            If (dtEmployees IsNot Nothing) Then
    '                If (dtEmployees.Rows.Count > 0) Then
    '                    Dim dtSource As New DataTable
    '                    dtSource.Columns.Add("EmployeeId")
    '                    dtSource.Columns.Add("EmployeeName")
    '                    Dim drRow As DataRow
    '                    drRow = dtSource.NewRow()
    '                    For Item As Integer = 0 To dtEmployees.Rows.Count - 1
    '                        If Item + 1 >= pagefrom And Item + 1 <= pageto Then


    '                            Dim drSource As DataRow
    '                            drSource = dtSource.NewRow
    '                            Dim dcCell1 As New DataColumn
    '                            Dim dcCell2 As New DataColumn
    '                            dcCell1.ColumnName = "EmployeeId"
    '                            dcCell2.ColumnName = "EmployeeName"
    '                            dcCell1.DefaultValue = dtEmployees.Rows(Item)(0)
    '                            dcCell2.DefaultValue = dtEmployees.Rows(Item)(3) + "-" + IIf(Lang = CtlCommon.Lang.EN, dtEmployees.Rows(Item)(1), dtEmployees.Rows(Item)(2))
    '                            drSource("EmployeeId") = dcCell1.DefaultValue
    '                            drSource("EmployeeName") = dcCell2.DefaultValue
    '                            dtSource.Rows.Add(drSource)
    '                        ElseIf MultiEmployeeFilterUC.EmployeeID > 0 Then
    '                            Dim drSource As DataRow
    '                            drSource = dtSource.NewRow
    '                            Dim dcCell1 As New DataColumn
    '                            Dim dcCell2 As New DataColumn
    '                            dcCell1.ColumnName = "EmployeeId"
    '                            dcCell2.ColumnName = "EmployeeName"
    '                            dcCell1.DefaultValue = dtEmployees.Rows(Item)(0)
    '                            dcCell2.DefaultValue = dtEmployees.Rows(Item)(3) + "-" + IIf(Lang = CtlCommon.Lang.EN, dtEmployees.Rows(Item)(1), dtEmployees.Rows(Item)(2))
    '                            drSource("EmployeeId") = dcCell1.DefaultValue
    '                            drSource("EmployeeName") = dcCell2.DefaultValue
    '                            dtSource.Rows.Add(drSource)
    '                        Else
    '                            Dim drSource As DataRow
    '                            drSource = dtSource.NewRow
    '                            Dim dcCell1 As New DataColumn
    '                            Dim dcCell2 As New DataColumn
    '                            dcCell1.ColumnName = "EmployeeId"
    '                            dcCell2.ColumnName = "EmployeeName"
    '                            dcCell1.DefaultValue = dtEmployees.Rows(Item)(0)
    '                            dcCell2.DefaultValue = dtEmployees.Rows(Item)(3) + "-" + IIf(Lang = CtlCommon.Lang.EN, dtEmployees.Rows(Item)(1), dtEmployees.Rows(Item)(2))
    '                            drSource("EmployeeId") = dcCell1.DefaultValue
    '                            drSource("EmployeeName") = dcCell2.DefaultValue
    '                            dtSource.Rows.Add(drSource)
    '                        End If

    '                    Next
    '                    Dim dv As New DataView(dtSource)
    '                    If SessionVariables.CultureInfo = "ar-JO" Then
    '                        'dv.Sort = "EmployeeName"
    '                        For Each row As DataRowView In dv
    '                            If (Not MultiEmployeeFilterUC.EmployeeID = 0) AndAlso MultiEmployeeFilterUC.SearchType = "4" Then
    '                                If MultiEmployeeFilterUC.EmployeeID = row("EmployeeId") Then
    '                                    cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
    '                                    Exit For
    '                                End If
    '                            Else
    '                                cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
    '                            End If
    '                        Next
    '                    Else
    '                        For Each row As DataRowView In dv
    '                            If (Not MultiEmployeeFilterUC.EmployeeID = 0) AndAlso MultiEmployeeFilterUC.SearchType = "4" Then
    '                                If MultiEmployeeFilterUC.EmployeeID = row("EmployeeId") Then
    '                                    cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
    '                                    Exit For
    '                                End If
    '                            Else
    '                                cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
    '                            End If
    '                        Next
    '                    End If
    '                End If
    '            End If
    '        End If
    '    End If

    'End Sub

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

            If (dt IsNot Nothing) Then
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
        MultiEmployeeFilterUC.CompanyID = RadCmbBxCompanies.SelectedValue
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

    Private Sub ManageDisplayMode_ExceptionalCases()
        '  Current function handle the cases where the displayMode 
        '  associated with the in appropriate value for 
        '  the PermissionId
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim _Exist As Integer = 0
        '_Exist = IS_Exist()
        If DisplayMode = DisplayModeEnum.View Or _
            DisplayMode = DisplayModeEnum.Edit Then
            If _Exist = -1 Then
                ' If the display mode is View or edit.But, the 
                ' Id is not a valid one
                CtlCommon.ShowMessage( _
                    Me.Page, _
                    ResourceManager.GetString("ErrorPermitProcessing", CultureInfo), "error")
                DisplayMode = DisplayModeEnum.ViewAll
            End If
        ElseIf DisplayMode = DisplayModeEnum.ViewAddEdit Or _
            DisplayModeEnum.ViewAll = DisplayMode Then
            PermissionIdProp = 0
        ElseIf DisplayMode() = DisplayModeEnum.Add Then
            If PermissionIdProp > 0 Then
                If _Exist = 0 Then
                    DisplayMode = DisplayModeEnum.Edit
                Else
                    ' PermissionId>0 and does not matched a data base record
                    PermissionIdProp = 0
                End If
            Else
                ' if PermissionId=0 or PermissionId<0
                PermissionIdProp = 0
            End If
        End If
    End Sub

    Private Sub SetRadDateTimePickerPeoperties()

        ' This function set properties for terlerik controls

        'Imports Telerik.Web.UI.DatePickerPopupDirection

        ' Set TimeView properties 
        Me.RadTPfromTime.TimeView.StartTime = New TimeSpan(0, 0, 0)
        Me.RadTPfromTime.TimeView.EndTime = New TimeSpan(23.5, 0, 0)
        Me.RadTPfromTime.TimeView.HeaderText = String.Empty
        Me.RadTPfromTime.TimeView.Interval = New TimeSpan(0, 30, 0)
        Me.RadTPfromTime.TimeView.Columns = 5

        ' Set Popup window properties
        Me.RadTPfromTime.PopupDirection = TopRight
        Me.RadTPfromTime.ShowPopupOnFocus = True

        ' Set default value
        Me.RadTPfromTime.SelectedDate = Now

        ' Set TimeView properties 
        Me.RadTPtoTime.TimeView.StartTime = New TimeSpan(0, 0, 0)
        Me.RadTPtoTime.TimeView.EndTime = New TimeSpan(23.5, 0, 0)
        Me.RadTPtoTime.TimeView.Interval = New TimeSpan(0, 30, 0)
        Me.RadTPtoTime.TimeView.HeaderText = String.Empty
        Me.RadTPtoTime.TimeView.Columns = 5
        ' Set Popup window properties
        Me.RadTPtoTime.PopupDirection = TopRight
        Me.RadTPtoTime.ShowPopupOnFocus = True
        ' Set default value
        Me.RadTPtoTime.SelectedDate = Now

        ' Set Data input properties
        Me.dtpStartDatePerm.SelectedDate = Today.Date
        ' Set popup properties
        Me.dtpStartDatePerm.PopupDirection = TopRight
        Me.dtpStartDatePerm.ShowPopupOnFocus = True
        ' Set calender boundaries
        Me.dtpStartDatePerm.MinDate = New Date(2006, 1, 1)
        Me.dtpStartDatePerm.MaxDate = New Date(2040, 1, 1)

        ' Set Data input properties
        Me.dtpEndDatePerm.SelectedDate = Today.Date
        ' Set popup properties
        Me.dtpEndDatePerm.PopupDirection = TopRight
        Me.dtpEndDatePerm.ShowPopupOnFocus = True
        ' Set calender boundaries
        Me.dtpEndDatePerm.MinDate = New Date(2006, 1, 1)
        Me.dtpEndDatePerm.MaxDate = New Date(2040, 1, 1)

        ' Set Data input properties
        Me.dtpPermissionDate.SelectedDate = Today.Date
        ' Set Popup properties
        Me.dtpPermissionDate.PopupDirection = TopRight
        Me.dtpPermissionDate.ShowPopupOnFocus = True
        ' Set calender boundaries
        Me.dtpPermissionDate.MinDate = New Date(2006, 1, 1)
        Me.dtpPermissionDate.MaxDate = New Date(2040, 1, 1)

    End Sub

    Private Function IS_Exist() As Integer
        ' The View , and Edit modes require to have a valid Leave Id 
        objEmpPermissions = New Emp_Permissions
        objEmpPermissions.PermissionId = PermissionIdProp
        Dim _EXIT As Integer = 0
        If PermissionIdProp <= 0 Then
            _EXIT = -1
        ElseIf objEmpPermissions.Find_Existing() = False Then
            _EXIT = -1
        End If
        Return _EXIT
    End Function

    Private Sub ClearAll()
        ' Clear Controls and reset to default values
        ' Reset combo boxes
        'RadCmbEmployee.SelectedIndex = 0
        RadCmpPermissions.SelectedIndex = 0
        ' Reset checkBoxes
        chckFullDay.Checked = False
        chckIsDividable.Checked = False
        chckIsFlexible.Checked = False
        chckSpecifiedDays.Checked = False
        ' Reset the time Views
        RadTPfromTime.SelectedDate = DateTime.Now
        RadTPtoTime.SelectedDate = DateTime.Now
        ' Default values for the periodical leave type
        dtpStartDatePerm.SelectedDate = Today.Date
        dtpEndDatePerm.SelectedDate = Today.Date
        ' Default value for a day leave type  
        dtpPermissionDate.SelectedDate = Today
        txtRemarks.Text = String.Empty
        txtDays.Text = String.Empty
        txtTimeDifference.Text = String.Empty
        ' Reset to prepare for next add 
        PermissionIdProp = 0
        ' Reset mode to one day permission 
        ShowHide(False)
        'rdlPermissionOption.Items.FindByValue("1").Selected = True
        radBtnOneDay.Checked = True
        radBtnPeriod.Checked = False
        radBtnSpecificDays.Checked = False
        ' Remove sorting and sorting arrow 
        SortExepression = Nothing
        SortDir = Nothing
        For i As Integer = 0 To chkWeekDays.Items.Count - 1
            chkWeekDays.Items(i).Selected = False
        Next

        rdlTimeOption.Items.FindByValue("0").Selected = True
        rdlTimeOption.Items.FindByValue("1").Selected = False
        rmtFlexibileTime.TextWithLiterals = "0000"
        trSpecificTime.Style("display") = "block"
        trFlixibleTime.Style("display") = "none"

        reqFlexibiletime.Enabled = False
        ExtenderFlexibileTime.Enabled = False
        reqFromtime.Enabled = True
        ExtenderFromTime.Enabled = True
        reqToTime.Enabled = True
        ExtenderreqToTime.Enabled = True
        lblPeriodInterval.Visible = True
        txtTimeDifference.Visible = True
        cblEmpList.Items.Clear()
        rdlTimeOption.Enabled = True
        DisableEnableTimeView(True)
        'EmployeeFilterUC.ClearValues()
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

    Private Sub FillLists()
        ' Fill telerik combo boxes
        Dim dt As DataTable = Nothing
        ' Get the permissions
        objPermissionsTypes = New PermissionsTypes()
        dt = objPermissionsTypes.GetAll()
        If dt IsNot Nothing Then
            CtlCommon.FillTelerikDropDownList(RadCmpPermissions, dt, Lang)
        End If
        dt = Nothing
        ' Get the employees 
        objEmployee = New Employee()
        dt = objEmployee.GetAll()
        If dt IsNot Nothing Then

            'RadCmbEmployee.DataSource = dt
            'setLocalizedTextField(RadCmbEmployee, "EmployeeName", "EmployeeArabicName")
            'RadCmbEmployee.DataTextField = "EmployeeName"
            'RadCmbEmployee.DataValueField = "EmployeeId"
            'RadCmbEmployee.DataBind()
        End If
    End Sub

    Private Sub FillControls()
        objEmpPermissions = New Emp_Permissions
        objEmpPermissions.PermissionId = PermissionIdProp
        ' If the PermissionId is not a valid one , the below line will through 
        ' an exception
        objEmpPermissions.GetByPK()
        With objEmpPermissions
            ' Fill combo boxes
            'RadCmbEmployee.SelectedValue = .FK_EmployeeId
            MultiEmployeeFilterUC.EmployeeID = .FK_EmployeeId
            RadCmpPermissions.SelectedValue = .FK_PermId
            ' Fill checkBoxes
            chckIsDividable.Checked = .IsDividable
            FileExtension = .AttachedFile
            fuAttachFile.Visible = False
            lnbLeaveFile.Visible = True
            lnbLeaveFile.HRef = "..\..\PermissionFiles\" + PermissionIdProp.ToString() + .AttachedFile
            If (.IsFlexible) Then
                rdlTimeOption.Items.FindByValue("1").Selected = True
                trFlixibleTime.Style("display") = "block"
                trSpecificTime.Style("display") = "none"
                rmtFlexibileTime.Text = CtlCommon.GetFullTimeString(.FlexibilePermissionDuration)
                lblPeriodInterval.Visible = False
                txtTimeDifference.Visible = False
            Else
                rdlTimeOption.Items.FindByValue("0").Selected = True
                trFlixibleTime.Style("display") = "none"
                trSpecificTime.Style("display") = "block"
                lblPeriodInterval.Visible = True
                txtTimeDifference.Visible = True
            End If
            chckIsFlexible.Checked = .IsFlexible
            radBtnSpecificDays.Checked = .IsSpecificDays
            'chckSpecifiedDays.Checked = .IsSpecificDays
            ' Fill the time & date pickers
            RadTPfromTime.SelectedDate = .FromTime
            RadTPtoTime.SelectedDate = .ToTime
            txtRemarks.Text = .Remark
            txtDays.Text = .Days

            If (Not String.IsNullOrEmpty(.Days)) Then
                If (.Days.Contains(",")) Then
                    Dim daysArr() As String = .Days.Split(",")
                    For j As Integer = 0 To daysArr.Length - 1
                        If (Not String.IsNullOrEmpty(daysArr(j))) Then
                            chkWeekDays.Items.FindByValue(daysArr(j)).Selected = True
                        End If
                    Next
                End If
            End If

            '''''''''''''''''''''''''''''''''''''''''''
            ' Enable / Disable time pickers according to the full day 
            ' value
            chckFullDay.Checked = .IsFullDay
            ManageSomeControlsStatus(.IsForPeriod, .PermDate, .PermEndDate, .IsFullDay)
        End With
    End Sub

    Private Sub ManageSomeControlsStatus(ByVal IsForPeriod As Boolean, ByVal PermDate As DateTime, ByVal PermEndDate As DateTime, ByVal FullDay As Boolean)
        ' Manage the DatePickers according 
        ' to the permission type
        ShowHide(IsForPeriod)
        radBtnOneDay.Checked = Not IsForPeriod
        radBtnPeriod.Checked = IsForPeriod

        If IsForPeriod = False Then
            dtpPermissionDate.SelectedDate = PermDate
        Else
            dtpStartDatePerm.SelectedDate = PermDate
            dtpEndDatePerm.SelectedDate = _
                IIf(CheckDate(PermEndDate) = Nothing, Nothing, PermEndDate)
        End If
        ' If the permission for full day , means to disable the TimeView(s)
        ' otherwise means to enable or keep it enable
        If FullDay = False Then
            DisableEnableTimeView(True)
            rdlTimeOption.Enabled = True
            setTimeDifference()
        Else
            DisableEnableTimeView(False)
            rdlTimeOption.Enabled = False
            txtTimeDifference.Text = String.Empty
        End If
        ' Disable all controls in some display modes 
        If DisplayMode.ToString() = "ViewAll" Or DisplayMode.ToString() = "View" Then
            txtTimeDifference.Enabled = False
            RadTPfromTime.Enabled = False
            RadTPtoTime.Enabled = False
        End If
    End Sub

    Private Function CheckDate(ByVal myDate As Object) As Object
        ' input : DateTime object
        ' Output : Nothing or a DateTime greater tha date time 
        ' minimum value
        ' Processing : Check the input if a valid date time to be used
        ' valid means greater than the minimum value and in valid format
        If IsDate(myDate) Then
            myDate = _
                IIf(myDate > DateTime.MinValue, myDate, Nothing)
            Return (myDate)
        Else
            Return Nothing
        End If
    End Function

    Private Function IS_Period() As Boolean
        'Return rdlPermissionOption.Items.FindByValue("2").Selected
        Return radBtnPeriod.Checked
    End Function

    Private Sub DisableEnableTimeView(ByVal status As Boolean)
        If DisplayMode.ToString() = "ViewAll" Or _
            DisplayMode.ToString() = "View" Then
            RadTPfromTime.Enabled = False
            RadTPtoTime.Enabled = False
            ' Read only text box , no matter to enable or disable
            ' txtTimeDifference.Enabled = False
        Else
            ' Enable or disable time view
            RadTPfromTime.Enabled = status
            RadTPtoTime.Enabled = status
            reqFromtime.Enabled = status
            reqToTime.Enabled = status
            CustomValidator1.Enabled = status
            CustomValidator2.Enabled = status
            If status = True Then
                ' Do nothing
                'RadTPfromTime.TimeView.HeaderText = "Start Time"
                'RadTPtoTime.TimeView.HeaderText = "End Time"
            Else
            End If

        End If
    End Sub

    Private Function setTimeDifference() As TimeSpan
        Try
            Dim temp1 As DateTime = Nothing
            Dim temp2 As DateTime = Nothing

            temp1 = RadTPfromTime.SelectedDate.Value
            temp2 = RadTPtoTime.SelectedDate.Value
            Dim startTime As New DateTime(2011, 1, 1, _
                                          temp1.Hour(), temp1.Minute(), temp1.Second)

            Dim endTime As New DateTime(2011, 1, 1, _
                                          temp2.Hour(), temp2.Minute(), temp2.Second)


            Dim c As TimeSpan = (endTime.Subtract(startTime))
            Dim result As Integer = _
                DateTime.Compare(endTime, startTime)

            Dim hours As Integer = 0
            Dim minutes As Integer = 0

            If result = 0 Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    txtTimeDifference.Text = 0 & " ساعات," & _
                      0 & " دقائق"
                    txtTimeDifference.Style("text-align") = "right"
                Else
                    txtTimeDifference.Text = 0 & " hours," & _
                      0 & " minuts"
                End If

            ElseIf result > 0 Then
                Dim TotalMinutes = c.TotalMinutes()
                If (TotalMinutes > 59) Then
                    TotalMinutes = Math.Ceiling(TotalMinutes - (60 * c.Hours()))
                Else
                    TotalMinutes = Math.Ceiling(TotalMinutes)
                End If

                If SessionVariables.CultureInfo = "ar-JO" Then
                    txtTimeDifference.Text = c.Hours() & " ساعات," & _
                     TotalMinutes & " دقائق"
                    txtTimeDifference.Style("text-align") = "right"
                Else
                    txtTimeDifference.Text = c.Hours() & " hours," & _
                     TotalMinutes & " minutes"

                End If

            ElseIf result < 0 Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    txtTimeDifference.Text = c.Hours() & " ساعات," & _
                       Math.Ceiling(c.TotalMinutes()) & " دقائق"
                    txtTimeDifference.Style("text-align") = "right"
                Else
                    txtTimeDifference.Text = c.Hours() & " hours," & _
                       Math.Ceiling(c.TotalMinutes()) & " minutes"
                End If

                If c.Hours() <= 0 And c.Minutes() <= 0 Then
                    hours = c.Hours() * -1
                    '''''''''''''''''''''
                    If c.Hours() <> 0 Then
                        hours = 24 - hours
                    Else
                        hours = 23
                    End If
                    '''''''''''''''''''''''''
                    minutes = Math.Ceiling(c.TotalMinutes()) * -1
                    If minutes <> 0 Then
                        minutes = 60 - minutes
                        If minutes = 60 Then
                            hours = hours + 1
                            minutes = 0
                        End If
                    Else
                        minutes = 0

                    End If
                    '''''''''''
                    If SessionVariables.CultureInfo = "ar-JO" Then
                        txtTimeDifference.Text = hours & " ساعات," & _
                          Math.Ceiling(c.TotalMinutes()) & " دقائق"
                        txtTimeDifference.Style("text-align") = "right"
                    Else
                        txtTimeDifference.Text = hours & " hours," & _
                          Math.Ceiling(c.TotalMinutes()) & " minutes"

                    End If
                End If
            End If
        Catch ex As Exception

            MsgBox(ex.Message)

        End Try
    End Function

    Private Sub setLocalizedTextField(ByVal comb As RadComboBox, ByVal EnName As String, ByVal ArName As String)
        comb.DataTextField = IIf(SessionVariables.CultureInfo = "en-US", EnName, ArName)
    End Sub

    Private Function CheckLeaveTypeDuration(ByRef EmployeeName As String) As Boolean
        For Each EmpItem As ListItem In cblEmpList.Items
            If EmpItem.Selected Then
                If Not LeaveTypeDuration = 0 Then
                    Dim EmpDt As DataTable
                    Dim intFromtime As Integer = 0
                    Dim intToTime As Integer = 0
                    Dim studyFound As Boolean = False
                    EmployeeName = EmpItem.Text

                    objEmpPermissions = New Emp_Permissions
                    With objEmpPermissions
                        .FK_EmployeeId = EmpItem.Value
                        .FK_PermId = RadCmpPermissions.SelectedValue

                        If radBtnOneDay.Checked Then
                            .FromTime = DateSerial(dtpPermissionDate.SelectedDate.Value.Year, dtpPermissionDate.SelectedDate.Value.Month, 1)
                            .ToTime = DateSerial(dtpPermissionDate.SelectedDate.Value.Year, dtpPermissionDate.SelectedDate.Value.Month + 1, 0)
                        Else
                            .FromTime = DateSerial(dtpStartDatePerm.SelectedDate.Value.Year, dtpStartDatePerm.SelectedDate.Value.Month, 1)
                            .ToTime = DateSerial(dtpEndDatePerm.SelectedDate.Value.Year, dtpEndDatePerm.SelectedDate.Value.Month + 1, 0)
                        End If

                        EmpDt = .GetAllDurationByEmployee()
                    End With

                    If EmpDt IsNot Nothing Then
                        If EmpDt.Rows.Count > 0 Then
                            For Each row As DataRow In EmpDt.Rows
                                If row("PermissionOption") = 3 Then
                                    studyFound = True
                                End If

                                If Not row("PermissionOption") = 2 Then
                                    intFromtime += (Convert.ToDateTime(row("FromTime")).Hour / 60) + (Convert.ToDateTime(row("FromTime")).Minute)
                                    intToTime += (Convert.ToDateTime(row("ToTime")).Hour / 60) + (Convert.ToDateTime(row("ToTime")).Minute)
                                End If
                            Next

                            intFromtime += (RadTPfromTime.SelectedDate.Value.Hour / 60) + (RadTPfromTime.SelectedDate.Value.Minute)
                            intToTime += (RadTPtoTime.SelectedDate.Value.Hour / 60) + (RadTPtoTime.SelectedDate.Value.Minute)

                            If studyFound Then
                                If (intFromtime + intToTime) > LeaveTypeDuration Then
                                    Return False
                                Else
                                    Return True
                                End If
                            Else
                                Return True
                            End If
                        End If
                    End If

                Else
                    Return True
                End If
            End If

        Next

        Return True
    End Function

    Protected Sub Repeater1_ItemCommand(source As Object, e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles Repeater1.ItemCommand

        Dim page As Integer = CInt(CType(e.CommandSource, LinkButton).Text)
        PageNo = page
        FillEmployee()

    End Sub
#End Region

#Region "Mnage periodical option overhead"
    Protected Sub radBtnPeriod_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radBtnPeriod.CheckedChanged
        ShowHide(True)
    End Sub

    Protected Sub radBtnOneDay_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radBtnOneDay.CheckedChanged
        ShowHide(False)
    End Sub

    Protected Sub radBtnSpecificDays_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radBtnSpecificDays.CheckedChanged
        ShowHide(True)
    End Sub

    Private Sub ShowHide(ByVal IsPeriod As Boolean)
        pnlPeriodLeave.Visible = IsPeriod
        PnlOneDayLeave.Visible = Not IsPeriod
        reqEndDate.Enabled = IsPeriod
        CVDate.Enabled = IsPeriod
        'radBtnOneDay.Checked = Not IsPeriod
        'radBtnPeriod.Checked = IsPeriod

        If (radBtnSpecificDays.Checked) Then
            trWeekDays.Style("display") = "block"
        Else
            trWeekDays.Style("display") = "none"
        End If

        'If Lang = CtlCommon.Lang.EN Then
        '    tdWeekDays.Style("margin-right") = "-470px"
        '    tdWeekDays.Style("float") = "right"
        'Else
        '    tdWeekDays.Style("margin-left") = "-390px"
        '    tdWeekDays.Style("float") = "left"
        'End If

    End Sub

#End Region

#Region "Display Mode Functions"
    Sub ManageFunctionalities()
        Select Case DisplayMode.ToString
            Case "Add"
                Adddisplaymode()
            Case "Edit"
                ViewEditmode()
            Case "View"
                Viewdisplaymode()
            Case "ViewAll"
                ViewAllDisplaymode()
            Case "ViewAddEdit"
                ViewAddEditDisplaymode()
            Case Else
        End Select
    End Sub

    Public Sub LoadData()
        ManageFunctionalities()
    End Sub

    Sub ViewAddEditDisplaymode()
        RefreshControls(True)
    End Sub

    Sub Adddisplaymode()
        RefreshControls(True)
        btnClear.Visible = False
    End Sub

    Sub Viewdisplaymode()
        RefreshControls(True)
        ManageControls(False)
        btnClear.Visible = False
        btnSave.Visible = False
        btnClear.Visible = False
        FillControls()
    End Sub

    Sub ViewAllDisplaymode()
        RefreshControls(True)
        ManageControls(False)
        btnClear.Visible = False
        btnSave.Visible = False
        btnSave.Visible = False
        btnClear.Visible = False
    End Sub

    Sub ViewEditmode()
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        RefreshControls(True)
        btnSave.Text = ResourceManager.GetString("btnUpdate", CultureInfo)

        FillControls()
        btnClear.Visible = False
    End Sub

    Sub ManageControls(ByVal Status As Boolean)

        ' Get Values from the combo boxes
        'RadCmbEmployee.Enabled = Status
        cblEmpList.Enabled = Status
        RadCmpPermissions.Enabled = Status
        ' Get values from the check boxes
        chckIsDividable.Enabled = Status
        chckIsFlexible.Enabled = Status
        chckFullDay.Enabled = Status
        chckSpecifiedDays.Enabled = Status
        txtRemarks.Enabled = Status
        ' Get values from rad controls
        dtpStartDatePerm.Enabled = Status
        dtpEndDatePerm.Enabled = Status
        RadTPfromTime.Enabled = Status
        RadTPtoTime.Enabled = Status
        dtpStartDatePerm.Enabled = Status
        dtpEndDatePerm.Enabled = Status
        dtpPermissionDate.Enabled = Status
        radBtnOneDay.Enabled = Status
        radBtnPeriod.Enabled = Status
    End Sub

    Sub RefreshControls(ByVal status As Boolean)
        ManageControls(True)
        btnSave.Text = GetLocalResourceObject("btnSaveResource1.Text")
        ' Toggle the status of the buttons
        btnClear.Visible = status
        btnSave.Visible = status
    End Sub

    Function saveUpdate() As Integer
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim errNo As Integer
        Dim check As Boolean = False
        objEmpPermissions = New Emp_Permissions
        OffAndHolidayDays = 0
        EmpLeaveTotalBalance = 0
        Dim errorNum As Integer = -1
        Dim ErrorMessage As String = String.Empty
        Dim tpFromTime As DateTime?
        Dim tpToTime As DateTime?
        Dim isFlixible As Boolean
        Dim strFlexibileDuration As String = (CInt(rmtFlexibileTime.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtFlexibileTime.TextWithLiterals.Split(":")(1))

        Dim temp_date As Date
        Dim temp_str_date As String
        Dim err2 As Integer

        If ((PermissionType = PermissionOption.Nursing)) Then
            tpFromTime = CType(Nothing, DateTime?)
            tpToTime = CType(Nothing, DateTime?)
        Else
            tpFromTime = RadTPfromTime.SelectedDate
            tpToTime = RadTPtoTime.SelectedDate
        End If

        If (rdlTimeOption.Items.FindByValue("0").Selected) Then
            isFlixible = False
        Else
            isFlixible = True
        End If

        Dim EmpName As String = String.Empty
        'If Not ValidatePermissionWorkingTime(EmpName) Then
        '    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("WorkScheduleTiemRange") + " " + EmpName, "info")
        '    Return -1
        'End If

        'If Not CheckLeaveTypeDuration(EmpName) Then
        '    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("PermissionTypeDurationLimit") + " " + EmpName, "info")
        '    Return -1
        'End If

        For Each item As ListItem In cblEmpList.Items
            If (item.Selected) Then
                check = True
                EmpName = item.Text
                objEmpPermissions = New Emp_Permissions
                If rdlTimeOption.SelectedValue = 0 Then
                    If (objEmpPermissions.ValidateEmployeePermission(item.Value, PermissionIdProp, dtCurrent, radBtnOneDay.Checked, dtpStartDatePerm.SelectedDate, dtpEndDatePerm.SelectedDate, tpFromTime, _
                                                                     tpToTime, dtpPermissionDate.SelectedDate, objPermissionsTypes, RadCmpPermissions.SelectedValue, chckFullDay.Checked, ErrorMessage, OffAndHolidayDays, EmpLeaveTotalBalance) = False) Then
                        If (ErrorMessage <> String.Empty) Then
                            CtlCommon.ShowMessage(Me.Page, ErrorMessage + " - " + EmpName, "info")
                            Return -1
                        End If
                    End If
                ElseIf rdlTimeOption.SelectedValue = 1 Then
                    tpFromTime = DateTime.MinValue
                    tpToTime = DateTime.MinValue
                    If (objEmpPermissions.ValidateEmployeeFlexiblePermission(item.Value, PermissionIdProp, dtCurrent, radBtnOneDay.Checked, _
                                                                            dtpStartDatePerm.SelectedDate, dtpEndDatePerm.SelectedDate, _
                                                                             dtpPermissionDate.SelectedDate, tpFromTime, tpToTime, objPermissionsTypes, RadCmpPermissions.SelectedValue, _
                                                                            chckFullDay.Checked, ErrorMessage, OffAndHolidayDays, strFlexibileDuration, EmpLeaveTotalBalance) = False) Then
                        If (ErrorMessage <> String.Empty) Then
                            CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
                            Return -1
                        End If

                    End If
                End If



                Dim days As String = String.Empty

                If (chckFullDay.Checked) Then
                    EmpLeaveTotalBalance = EmpLeaveTotalBalance - 1
                End If

                For i As Integer = 0 To chkWeekDays.Items.Count - 1
                    If (chkWeekDays.Items(i).Selected) Then
                        days &= chkWeekDays.Items(i).Value & ","
                    End If
                Next

                ' Set data into object for Add / UpdateIf

                objEmpPermissions.PermissionId = PermissionIdProp
                objEmpPermissions.LeaveId = LeaveID
                If (PermissionIdProp = 0) Then
                    FileExtension = String.Empty
                End If
                If Not objEmpPermissions.PermissionOption = PermissionOption.Nursing Then
                    AllowedTime = Nothing
                Else
                    AllowedTime = objEmpPermissions.AllowedTime
                End If
                objEmpPermissions.AddPermAllProcess(item.Value, RadCmpPermissions.SelectedValue, chckIsDividable.Checked, isFlixible, radBtnSpecificDays.Checked, _
                                                 txtRemarks.Text, dtpStartDatePerm.SelectedDate, dtpEndDatePerm.SelectedDate, radBtnOneDay.Checked, tpFromTime, _
                                                 tpToTime, dtpPermissionDate.SelectedDate, PermissionDaysCount, OffAndHolidayDays, days, EmpLeaveTotalBalance, LeaveID, _
                                                 Integer.Parse(PermissionType), Integer.Parse(strFlexibileDuration), FileExtension, chckFullDay.Checked, AllowedTime, ErrorMessage)

                objRECALC_REQUEST = New RECALC_REQUEST
                objAPP_Settings = New APP_Settings()
                objAPP_Settings = objAPP_Settings.GetByPK()
                If objAPP_Settings.ApprovalRecalMethod = 1 Then
                    If radBtnOneDay.Checked AndAlso PermissionType = PermissionOption.Normal Then
                        temp_date = dtpPermissionDate.SelectedDate
                        temp_str_date = DateToString(temp_date)
                        objRECALC_REQUEST.EMP_NO = item.Value
                        objRECALC_REQUEST.VALID_FROM_NUM = Integer.Parse(temp_str_date)

                        If Not temp_date = Date.Now.AddDays(1).ToShortDateString() Then
                            Dim count As Integer
                            While count < 5
                                err2 = objRECALC_REQUEST.RECALCULATE()
                                If err2 = 0 Then
                                    Exit While
                                End If
                                count += 1
                            End While
                        End If

                    Else

                        Dim dteFrom As DateTime = dtpStartDatePerm.DbSelectedDate
                        Dim dteTo As DateTime = dtpEndDatePerm.DbSelectedDate

                        While dteFrom <= dteTo
                            If Not dteFrom = Date.Now.AddDays(1).ToShortDateString() Then
                                temp_str_date = DateToString(dteFrom)
                                objRECALC_REQUEST = New RECALC_REQUEST()
                                objRECALC_REQUEST.EMP_NO = item.Value
                                objRECALC_REQUEST.VALID_FROM_NUM = Integer.Parse(temp_str_date)
                                err2 = objRECALC_REQUEST.RECALCULATE()
                                If Not err2 = 0 Then
                                    Exit While
                                End If
                                dteFrom = dteFrom.AddDays(1)
                            Else
                                Exit While
                            End If
                        End While
                    End If

                Else
                    If radBtnOneDay.Checked AndAlso PermissionType = PermissionOption.Normal Then
                        temp_date = dtpPermissionDate.SelectedDate
                        temp_str_date = DateToString(temp_date)
                        objRECALC_REQUEST.EMP_NO = item.Value
                        objRECALC_REQUEST.VALID_FROM_NUM = Integer.Parse(temp_str_date)

                        objRecalculateRequest = New RecalculateRequest
                        With objRecalculateRequest
                            .Fk_EmployeeId = item.Value
                            .FromDate = temp_date
                            .ToDate = temp_date
                            .ImmediatelyStart = True
                            .RecalStatus = 0
                            .CREATED_BY = SessionVariables.LoginUser.ID
                            .Remarks = "Employee Permission - SYSTEM"
                            err2 = .Add
                        End With

                    Else
                        Dim dteFrom As DateTime = dtpStartDatePerm.DbSelectedDate
                        Dim dteTo As DateTime = dtpEndDatePerm.DbSelectedDate
                        If Not dteFrom > Date.Today Then
                            objRecalculateRequest = New RecalculateRequest
                            With objRecalculateRequest
                                .Fk_EmployeeId = item.Value
                                .FromDate = dteFrom
                                .ToDate = dteTo
                                .ImmediatelyStart = True
                                .RecalStatus = 0
                                .CREATED_BY = SessionVariables.LoginUser.ID

                                If PermissionType = PermissionOption.Nursing Then
                                    .Remarks = "Employee Nursing Permission - SYSTEM"
                                ElseIf PermissionType = PermissionOption.Study Then
                                    .Remarks = "Employee Study Permission - SYSTEM"
                                Else
                                    .Remarks = "Employee Permission - SYSTEM"
                                End If

                                err2 = .Add
                            End With
                        End If
                    End If


                End If

            End If
        Next

        If Not check Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("PleaseSelectEmployee", CultureInfo).ToString(), "info")
            Return -1
        End If

        showResultMessage(Me.Page, ErrorMessage)
        If errNo = 0 Then

            If fuAttachFile.HasFile Then
                Dim extention As String = Path.GetExtension(fuAttachFile.PostedFile.FileName)
                Dim fileName As String = objEmpPermissions.PermissionId.ToString()
                Dim fPath As String = String.Empty
                fPath = Server.MapPath("..\PermissionFiles\\" + fileName + extention)
                fuAttachFile.PostedFile.SaveAs(fPath)
            End If

            For Each item As ListItem In cblEmpList.Items
                If (item.Selected) Then

                End If
            Next

            ClearAll()

        End If

    End Function

    Function updateOnly() As Integer
        Dim errNo As Integer = -1
        objEmpPermissions = New Emp_Permissions
        Dim tpFromTime As DateTime
        Dim tpToTime As DateTime
        Dim isFlixible As Boolean
        Dim strFlexibileDuration As String = (Val(rmtFlexibileTime.Text.Substring(0, 2)) * 60) + Val(rmtFlexibileTime.Text.Substring(2, 2))

        If ((PermissionType = PermissionOption.Nursing) Or (chckFullDay.Checked)) Then
            tpFromTime = CType(Nothing, DateTime?)
            tpToTime = CType(Nothing, DateTime?)
        Else
            tpFromTime = RadTPfromTime.SelectedDate
            tpToTime = RadTPtoTime.SelectedDate
        End If

        Dim days As String = String.Empty

        For i As Integer = 0 To chkWeekDays.Items.Count - 1
            If (chkWeekDays.Items(i).Selected) Then
                days &= chkWeekDays.Items(i).Value & ","
            End If
        Next

        If (rdlTimeOption.Items.FindByValue("0").Selected) Then
            isFlixible = False
        Else
            isFlixible = True
        End If

        If Not objEmpPermissions.PermissionOption = PermissionOption.Nursing Then
            AllowedTime = Nothing
        Else
            AllowedTime = objEmpPermissions.AllowedTime
        End If

        Dim errorNum As Integer = -1
        objEmpPermissions.FillObjectData(MultiEmployeeFilterUC.EmployeeID, RadCmpPermissions.SelectedValue, chckIsDividable.Checked, isFlixible, radBtnSpecificDays.Checked, _
                                 txtRemarks.Text, dtpStartDatePerm.SelectedDate, dtpEndDatePerm.SelectedDate, radBtnOneDay.Checked, tpFromTime, _
                                 tpToTime, dtpPermissionDate.SelectedDate, PermissionDaysCount, PermissionType, OffAndHolidayDays, days, Integer.Parse(strFlexibileDuration), FileExtension, chckFullDay.Checked, AllowedTime)
        If PermissionIdProp > 0 Then
            ' Update a data base record , on update mode
            objEmpPermissions.PermissionId = PermissionIdProp
            errNo = objEmpPermissions.Update()

            If errorNum = 0 Then
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("UpdatedSuccessfully", CultureInfo), "success")
            Else
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")
            End If


        End If

        Return errNo
    End Function

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

    Private Function ValidatePermissionWorkingTime(ByRef EmployeeName As String) As Boolean
        For Each EmpItem As ListItem In cblEmpList.Items
            If EmpItem.Selected Then
                objEmpWorkSchedule = New Emp_WorkSchedule()
                Dim intWorkScheduleType As Integer
                Dim intWorkScheduleId As Integer
                Dim dtEmpWorkSchedule As DataTable
                Dim found As Boolean = False
                Dim objApp_Settings As New APP_Settings
                Dim returnShiftResult As Boolean = False
                EmployeeName = EmpItem.Text

                objEmpWorkSchedule.FK_EmployeeId = EmpItem.Value
                objEmpWorkSchedule = objEmpWorkSchedule.GetByEmpId()
                If Not objEmpWorkSchedule Is Nothing Then
                    intWorkScheduleType = objEmpWorkSchedule.ScheduleType
                    intWorkScheduleId = objEmpWorkSchedule.FK_ScheduleId
                End If

                If objEmpWorkSchedule Is Nothing Then
                    objWorkSchedule = New WorkSchedule()
                    With objWorkSchedule
                        .GetByDefault()
                        intWorkScheduleType = .ScheduleType
                        intWorkScheduleId = .ScheduleId
                    End With
                End If

                If intWorkScheduleType = Convert.ToInt32(CtlCommon.ScheduleType.Normal) Then

                    objWorkSchedule_NormalTime = New WorkSchedule_NormalTime()
                    objWorkSchedule_NormalTime.FK_ScheduleId = intWorkScheduleId
                    dtEmpWorkSchedule = objWorkSchedule_NormalTime.GetAll()


                    Dim DayId As Integer

                    If PermissionType = PermissionOption.Normal Then
                        If radBtnOneDay.Checked Then
                            Dim strDay As String = dtpPermissionDate.SelectedDate.Value.DayOfWeek.ToString()
                            Select Case strDay
                                Case "Monday"
                                    DayId = 2
                                Case "Tuesday"
                                    DayId = 3
                                Case "Wednesday"
                                    DayId = 4
                                Case "Thursday"
                                    DayId = 5
                                Case "Friday"
                                    DayId = 6
                                Case "Saturday"
                                    DayId = 7
                                Case "Sunday"
                                    DayId = 1
                            End Select

                            Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                            Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()

                            For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                                If Convert.ToInt32(row("DayId")) = DayId Then
                                    found = True
                                    Exit For
                                End If
                            Next

                            If found Then

                            Else
                                Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                                Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                                If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) <> "00:00") AndAlso _
                                    ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) <> "00:00") Then

                                    If (((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                        (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm"))) Or _
                                        ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) <= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                                        (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")))) Then

                                        Return True
                                    Else
                                        Return False
                                    End If
                                Else
                                    If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                        (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm"))) Then

                                        Return True
                                    Else
                                        Return False
                                    End If
                                End If
                            End If
                        ElseIf radBtnPeriod.Checked Then
                            Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                            Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()
                            Dim dteStartDate As DateTime = dtpStartDatePerm.SelectedDate
                            Dim dteEndDate As DateTime = dtpEndDatePerm.SelectedDate
                            Dim returnPeriodValue As Boolean = False

                            While dteStartDate < dteEndDate
                                Dim strDay As String = dteStartDate.DayOfWeek.ToString()
                                Select Case strDay
                                    Case "Monday"
                                        DayId = 2
                                    Case "Tuesday"
                                        DayId = 3
                                    Case "Wednesday"
                                        DayId = 4
                                    Case "Thursday"
                                        DayId = 5
                                    Case "Friday"
                                        DayId = 6
                                    Case "Saturday"
                                        DayId = 7
                                    Case "Sunday"
                                        DayId = 1
                                End Select

                                For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                                    If Convert.ToInt32(row("DayId")) = DayId Then
                                        found = True
                                        Exit For
                                    End If
                                Next

                                If found Then
                                Else
                                    Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                                    Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                                    If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) <> "00:00") AndAlso _
                                        ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) <> "00:00") Then

                                        If (((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm"))) Or _
                                            ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) <= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")))) Then

                                            returnPeriodValue = True
                                        Else
                                            returnPeriodValue = False
                                        End If
                                    Else
                                        If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm"))) Then

                                            returnPeriodValue = True
                                        Else
                                            returnPeriodValue = False
                                        End If
                                    End If
                                End If
                                dteStartDate = dteStartDate.AddDays(1)
                            End While

                            Return returnPeriodValue
                        Else
                            Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                            Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()
                            Dim returnSpecificDaysValue As Boolean = False
                            For Each item As ListItem In chkWeekDays.Items
                                If item.Selected Then
                                    DayId = Convert.ToInt32(item.Value)
                                    For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                                        If Convert.ToInt32(row("DayId")) = DayId Then
                                            found = True
                                            Exit For
                                        End If
                                    Next

                                    If found Then
                                    Else
                                        Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                                        Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                                        If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) <> "00:00") AndAlso _
                                            ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) <> "00:00") Then

                                            If (((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm"))) Or _
                                                ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) <= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")))) Then

                                                returnSpecificDaysValue = True
                                            Else
                                                returnSpecificDaysValue = False
                                            End If
                                        Else
                                            If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm"))) Then

                                                returnSpecificDaysValue = True
                                            Else
                                                returnSpecificDaysValue = False
                                            End If
                                        End If
                                    End If

                                End If
                            Next
                            Return returnSpecificDaysValue
                        End If
                    ElseIf PermissionType = PermissionOption.Nursing Then
                        Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                        Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()
                        Dim dteStartDate As DateTime = dtpStartDatePerm.SelectedDate
                        Dim dteEndDate As DateTime = dtpEndDatePerm.SelectedDate
                        Dim returnNursingValue As Boolean = False

                        While dteStartDate < dteEndDate
                            Dim strDay As String = dteStartDate.DayOfWeek.ToString()
                            Select Case strDay
                                Case "Monday"
                                    DayId = 2
                                Case "Tuesday"
                                    DayId = 3
                                Case "Wednesday"
                                    DayId = 4
                                Case "Thursday"
                                    DayId = 5
                                Case "Friday"
                                    DayId = 6
                                Case "Saturday"
                                    DayId = 7
                                Case "Sunday"
                                    DayId = 1
                            End Select

                            For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                                If Convert.ToInt32(row("DayId")) = DayId Then
                                    found = True
                                    Exit For
                                End If
                            Next

                            If found Then
                            Else
                                Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                                Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                                If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) <> "00:00") AndAlso _
                                    ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) <> "00:00") Then

                                    If (((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                        (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm"))) Or _
                                        ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) <= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                                        (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")))) Then

                                        returnNursingValue = True
                                    Else
                                        returnNursingValue = False
                                    End If
                                Else
                                    If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                        (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm"))) Then

                                        returnNursingValue = True
                                    Else
                                        returnNursingValue = False
                                    End If
                                End If
                            End If
                            dteStartDate = dteStartDate.AddDays(1)
                        End While

                        Return returnNursingValue
                    ElseIf PermissionType = PermissionOption.Study Then
                        Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                        Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()
                        Dim returnStudyValue As Boolean = False
                        For Each item As ListItem In chkWeekDays.Items
                            If item.Selected Then
                                DayId = Convert.ToInt32(item.Value)
                                For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                                    If Convert.ToInt32(row("DayId")) = DayId Then
                                        found = True
                                        Exit For
                                    End If
                                Next

                                If found Then
                                Else
                                    Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                                    Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                                    If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) <> "00:00") AndAlso _
                                        ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) <> "00:00") Then

                                        If (((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm"))) Or _
                                            ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) <= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")))) Then

                                            returnStudyValue = True
                                        Else
                                            returnStudyValue = False
                                        End If
                                    Else
                                        If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm"))) Then

                                            returnStudyValue = True
                                        Else
                                            returnStudyValue = False
                                        End If
                                    End If
                                End If

                            End If
                        Next

                        Return returnStudyValue
                    End If
                ElseIf intWorkScheduleType = Convert.ToInt32(CtlCommon.ScheduleType.Flexible) Then
                    objWorkSchedule_Flexible = New WorkSchedule_Flexible()
                    objWorkSchedule_Flexible.FK_ScheduleId = intWorkScheduleId
                    dtEmpWorkSchedule = objWorkSchedule_Flexible.GetAll()

                    Dim dtDuration1 As Integer = (Convert.ToDateTime(dtEmpWorkSchedule(0)("Duration1").ToString().Insert(2, ":")).Hour * 60) + _
                                                    (Convert.ToDateTime(dtEmpWorkSchedule(0)("Duration1").ToString().Insert(2, ":")).Minute)
                    Dim dtDuration2 As Integer = (Convert.ToDateTime(dtEmpWorkSchedule(0)("Duration2").ToString().Insert(2, ":")).Hour * 60) + _
                                                    (Convert.ToDateTime(dtEmpWorkSchedule(0)("Duration2").ToString().Insert(2, ":")).Minute)

                    Dim DayId As Integer

                    If PermissionType = PermissionOption.Normal Then
                        If radBtnOneDay.Checked Then
                            Dim strDay As String = dtpPermissionDate.SelectedDate.Value.DayOfWeek.ToString()
                            Dim dtEmpWorkScheduleOffDay As New DataTable
                            Select Case strDay
                                Case "Monday"
                                    DayId = 2
                                Case "Tuesday"
                                    DayId = 3
                                Case "Wednesday"
                                    DayId = 4
                                Case "Thursday"
                                    DayId = 5
                                Case "Friday"
                                    DayId = 6
                                Case "Saturday"
                                    DayId = 7
                                Case "Sunday"
                                    DayId = 1
                            End Select

                            Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                            If Not drEmpWorkScheduleOffDay.Length = 0 Then
                                dtEmpWorkScheduleOffDay = drEmpWorkScheduleOffDay.CopyToDataTable()
                            End If

                            For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                                If Convert.ToInt32(row("DayId")) = DayId Then
                                    found = True
                                    Exit For
                                End If
                            Next

                            If found Then
                            Else
                                Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                                Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                                If (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm") <> "00:00") AndAlso _
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).ToString("HH:mm") <> "00:00") Then

                                    If (((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                        (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).AddMinutes(dtDuration1).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Or _
                                        ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                        (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).AddMinutes(dtDuration2).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")))) Then

                                        Return True
                                    Else
                                        Return False
                                    End If
                                Else
                                    If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                        (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).AddMinutes(dtDuration1).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then
                                        Return True
                                    Else
                                        Return False
                                    End If
                                End If
                            End If
                        ElseIf radBtnPeriod.Checked Then
                            Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                            Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()
                            Dim dteStartDate As DateTime = dtpStartDatePerm.SelectedDate
                            Dim dteEndDate As DateTime = dtpEndDatePerm.SelectedDate
                            Dim returnPeriodFlexValue As Boolean = False

                            While dteStartDate < dteEndDate
                                Dim strDay As String = dteStartDate.DayOfWeek.ToString()
                                Select Case strDay
                                    Case "Monday"
                                        DayId = 2
                                    Case "Tuesday"
                                        DayId = 3
                                    Case "Wednesday"
                                        DayId = 4
                                    Case "Thursday"
                                        DayId = 5
                                    Case "Friday"
                                        DayId = 6
                                    Case "Saturday"
                                        DayId = 7
                                    Case "Sunday"
                                        DayId = 1
                                End Select

                                For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                                    If Convert.ToInt32(row("DayId")) = DayId Then
                                        found = True
                                        Exit For
                                    End If
                                Next

                                If found Then
                                Else
                                    Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                                    Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                                    If (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm") <> "00:00") AndAlso _
                                        (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).ToString("HH:mm") <> "00:00") Then

                                        If (((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).AddMinutes(dtDuration1).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Or _
                                            ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).AddMinutes(dtDuration2).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")))) Then

                                            returnPeriodFlexValue = True
                                        Else
                                            returnPeriodFlexValue = False
                                        End If
                                    Else
                                        If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).AddMinutes(dtDuration1).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then
                                            returnPeriodFlexValue = True
                                        Else
                                            returnPeriodFlexValue = False
                                        End If
                                    End If
                                End If

                                dteStartDate = dteStartDate.AddDays(1)
                            End While

                            Return returnPeriodFlexValue
                        Else
                            Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                            Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()
                            Dim returnSpecificFlexValue As Boolean = False

                            For Each item As ListItem In chkWeekDays.Items
                                If item.Selected Then
                                    DayId = Convert.ToInt32(item.Value)
                                    For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                                        If Convert.ToInt32(row("DayId")) = DayId Then
                                            found = True
                                            Exit For
                                        End If
                                    Next

                                    If found Then
                                    Else
                                        Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                                        Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                                        If (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm") <> "00:00") AndAlso _
                                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).ToString("HH:mm") <> "00:00") Then

                                            If (((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).AddMinutes(dtDuration1).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Or _
                                                ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).AddMinutes(dtDuration2).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")))) Then

                                                returnSpecificFlexValue = True
                                            Else
                                                returnSpecificFlexValue = False
                                            End If
                                        Else
                                            If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).AddMinutes(dtDuration1).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then
                                                returnSpecificFlexValue = True
                                            Else
                                                returnSpecificFlexValue = False
                                            End If
                                        End If
                                    End If

                                End If
                            Next

                            Return returnSpecificFlexValue
                        End If
                    ElseIf PermissionType = PermissionOption.Nursing Then
                        Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                        Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()
                        Dim dteStartDate As DateTime = dtpStartDatePerm.SelectedDate
                        Dim dteEndDate As DateTime = dtpEndDatePerm.SelectedDate
                        Dim returnFlexNursingValue As Boolean = False

                        While dteStartDate < dteEndDate
                            Dim strDay As String = dteStartDate.DayOfWeek.ToString()
                            Select Case strDay
                                Case "Monday"
                                    DayId = 2
                                Case "Tuesday"
                                    DayId = 3
                                Case "Wednesday"
                                    DayId = 4
                                Case "Thursday"
                                    DayId = 5
                                Case "Friday"
                                    DayId = 6
                                Case "Saturday"
                                    DayId = 7
                                Case "Sunday"
                                    DayId = 1
                            End Select

                            For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                                If Convert.ToInt32(row("DayId")) = DayId Then
                                    found = True
                                    Exit For
                                End If
                            Next

                            If found Then
                            Else
                                Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                                Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                                If (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm") <> "00:00") AndAlso _
                                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).ToString("HH:mm") <> "00:00") Then

                                    If (((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                        (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).AddMinutes(dtDuration1).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Or _
                                        ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                        (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).AddMinutes(dtDuration2).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")))) Then

                                        returnFlexNursingValue = True
                                    Else
                                        returnFlexNursingValue = False
                                    End If
                                Else
                                    If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                        (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).AddMinutes(dtDuration1).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then
                                        returnFlexNursingValue = True
                                    Else
                                        returnFlexNursingValue = False
                                    End If
                                End If
                            End If
                            dteStartDate = dteStartDate.AddDays(1)
                        End While

                        Return returnFlexNursingValue
                    ElseIf PermissionType = PermissionOption.Study Then
                        Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                        Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()
                        Dim returnStudyFlexValue As Boolean = False
                        For Each item As ListItem In chkWeekDays.Items
                            If item.Selected Then
                                DayId = Convert.ToInt32(item.Value)
                                For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                                    If Convert.ToInt32(row("DayId")) = DayId Then
                                        found = True
                                        Exit For
                                    End If
                                Next

                                If found Then
                                Else
                                    Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                                    Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                                    If (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm") <> "00:00") AndAlso _
                                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).ToString("HH:mm") <> "00:00") Then

                                        If (((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).AddMinutes(dtDuration1).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Or _
                                            ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).AddMinutes(dtDuration2).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")))) Then

                                            returnStudyFlexValue = True
                                        Else
                                            returnStudyFlexValue = False
                                        End If
                                    Else
                                        If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).AddMinutes(dtDuration1).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then
                                            returnStudyFlexValue = True
                                        Else
                                            returnStudyFlexValue = False
                                        End If
                                    End If
                                End If

                            End If
                        Next

                        Return returnStudyFlexValue
                    End If
                ElseIf intWorkScheduleType = Convert.ToInt32(CtlCommon.ScheduleType.Advance) Then
                    Dim dteStartDate As DateTime = dtpStartDatePerm.SelectedDate
                    Dim dteEndDate As DateTime = dtpEndDatePerm.SelectedDate
                    Dim dtShiftSchedule As DataTable

                    If (radBtnOneDay.Checked) AndAlso (PermissionType = PermissionOption.Normal) Then
                        objEmp_Shifts = New Emp_Shifts
                        objEmp_Shifts.FK_EmployeeId = EmpItem.Value
                        objEmp_Shifts.WorkDate = dtpPermissionDate.SelectedDate
                        Dim AdvancedSchedule = objEmp_Shifts.GetShiftsByDate()

                        If (AdvancedSchedule IsNot Nothing And AdvancedSchedule.Rows.Count > 0) Then
                            Dim objWorkSchedule_Shifts As New WorkSchedule_Shifts
                            objWorkSchedule_Shifts.FK_ScheduleId = intWorkScheduleId
                            dtShiftSchedule = objWorkSchedule_Shifts.GetByFKScheduleID()

                            'Commented To Include Overnight Shifts
                            'If ((Convert.ToDateTime(dtShiftSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                            '    (Convert.ToDateTime(dtShiftSchedule(0)("FromTime2")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) Or _
                            '    (Convert.ToDateTime(dtShiftSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                            '    (Convert.ToDateTime(dtShiftSchedule(0)("ToTime2")).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then

                            returnShiftResult = True
                            'Else
                            '    returnShiftResult = False
                            'End If

                        Else
                            'FadiH:: check event when no shift
                            With objApp_Settings
                                .GetByPK()
                                If .NoShiftShcedule = Convert.ToInt32(CtlCommon.NoShiftSchedule.ConsideritOffDay) Then
                                    returnShiftResult = True
                                ElseIf .NoShiftShcedule = Convert.ToInt32(CtlCommon.NoShiftSchedule.ConsiderDefaultSchedule) Then
                                    returnShiftResult = CheckDefaultScheduleWorkingTime()
                                End If
                            End With
                        End If

                        Return returnShiftResult

                    ElseIf (radBtnPeriod.Checked) Or (PermissionType = PermissionOption.Nursing) Then
                        While dteStartDate < dteEndDate
                            objEmp_Shifts = New Emp_Shifts
                            objEmp_Shifts.FK_EmployeeId = EmpItem.Value
                            objEmp_Shifts.WorkDate = dteStartDate
                            Dim AdvancedSchedule = objEmp_Shifts.GetShiftsByDate()

                            If (AdvancedSchedule IsNot Nothing And AdvancedSchedule.Rows.Count > 0) Then
                                Dim objWorkSchedule_Shifts As New WorkSchedule_Shifts
                                objWorkSchedule_Shifts.FK_ScheduleId = intWorkScheduleId
                                dtShiftSchedule = objWorkSchedule_Shifts.GetByFKScheduleID()

                                If Not PermissionType = PermissionOption.Nursing Then
                                    If ((Convert.ToDateTime(dtShiftSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                        (Convert.ToDateTime(dtShiftSchedule(0)("FromTime2")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) Or _
                                        (Convert.ToDateTime(dtShiftSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                                        (Convert.ToDateTime(dtShiftSchedule(0)("ToTime2")).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then

                                        returnShiftResult = True
                                    Else
                                        returnShiftResult = False
                                    End If
                                Else
                                    returnShiftResult = True
                                End If
                            Else
                                'FadiH:: check event when no shift
                                With objApp_Settings
                                    .GetByPK()
                                    If .NoShiftShcedule = Convert.ToInt32(CtlCommon.NoShiftSchedule.ConsideritOffDay) Then
                                        returnShiftResult = True
                                    ElseIf .NoShiftShcedule = Convert.ToInt32(CtlCommon.NoShiftSchedule.ConsiderDefaultSchedule) Then
                                        returnShiftResult = CheckDefaultScheduleWorkingTime()
                                    End If
                                End With
                            End If

                            dteStartDate = dteStartDate.AddDays(1)
                        End While

                        Return returnShiftResult
                    ElseIf (radBtnSpecificDays.Checked) Or (PermissionType = PermissionOption.Study) Then
                        While dteStartDate < dteEndDate
                            objEmp_Shifts = New Emp_Shifts
                            objEmp_Shifts.FK_EmployeeId = EmpItem.Value
                            objEmp_Shifts.WorkDate = dteStartDate
                            Dim AdvancedSchedule = objEmp_Shifts.GetShiftsByDate()
                            Dim foundDay As Boolean = False
                            Dim DayId As Integer

                            Dim strDay As String = dteStartDate.DayOfWeek.ToString()
                            Select Case strDay
                                Case "Monday"
                                    DayId = 2
                                Case "Tuesday"
                                    DayId = 3
                                Case "Wednesday"
                                    DayId = 4
                                Case "Thursday"
                                    DayId = 5
                                Case "Friday"
                                    DayId = 6
                                Case "Saturday"
                                    DayId = 7
                                Case "Sunday"
                                    DayId = 1
                            End Select

                            For Each item As ListItem In chkWeekDays.Items
                                If item.Selected Then
                                    If item.Value = DayId Then
                                        foundDay = True
                                        Exit For
                                    End If
                                End If
                            Next

                            If foundDay Then
                                If (AdvancedSchedule IsNot Nothing And AdvancedSchedule.Rows.Count > 0) Then
                                    Dim objWorkSchedule_Shifts As New WorkSchedule_Shifts
                                    objWorkSchedule_Shifts.FK_ScheduleId = intWorkScheduleId
                                    dtShiftSchedule = objWorkSchedule_Shifts.GetByFKScheduleID()

                                    If ((Convert.ToDateTime(dtShiftSchedule(0)("FromTime1")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                        (Convert.ToDateTime(dtShiftSchedule(0)("FromTime2")).ToString("HH:mm")) <= (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) Or _
                                        (Convert.ToDateTime(dtShiftSchedule(0)("ToTime1")).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                                        (Convert.ToDateTime(dtShiftSchedule(0)("ToTime2")).ToString("HH:mm")) >= (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then

                                        returnShiftResult = True
                                    Else
                                        returnShiftResult = False
                                    End If

                                Else
                                    'FadiH:: check event when no shift
                                    With objApp_Settings
                                        .GetByPK()
                                        If .NoShiftShcedule = Convert.ToInt32(CtlCommon.NoShiftSchedule.ConsideritOffDay) Then
                                            returnShiftResult = True
                                        ElseIf .NoShiftShcedule = Convert.ToInt32(CtlCommon.NoShiftSchedule.ConsiderDefaultSchedule) Then
                                            returnShiftResult = CheckDefaultScheduleWorkingTime()
                                        End If
                                    End With
                                End If
                            End If

                            dteStartDate = dteStartDate.AddDays(1)
                        End While

                        Return returnShiftResult
                    End If

                End If
            End If
        Next
        Return True

    End Function

    Function CheckDefaultScheduleWorkingTime() As Boolean
        objEmpWorkSchedule = New Emp_WorkSchedule()
        Dim intWorkScheduleType As Integer
        Dim intWorkScheduleId As Integer
        Dim dtEmpWorkSchedule As DataTable
        Dim found As Boolean = False

        objWorkSchedule = New WorkSchedule()
        With objWorkSchedule
            .GetByDefault()
            intWorkScheduleType = .ScheduleType
            intWorkScheduleId = .ScheduleId
        End With

        If intWorkScheduleType = Convert.ToInt32(CtlCommon.ScheduleType.Normal) Then

            objWorkSchedule_NormalTime = New WorkSchedule_NormalTime()
            objWorkSchedule_NormalTime.FK_ScheduleId = intWorkScheduleId
            dtEmpWorkSchedule = objWorkSchedule_NormalTime.GetAll()


            Dim DayId As Integer

            If PermissionType = PermissionOption.Normal Then
                If radBtnOneDay.Checked Then
                    Dim strDay As String = dtpPermissionDate.SelectedDate.Value.DayOfWeek.ToString()
                    Select Case strDay
                        Case "Monday"
                            DayId = 2
                        Case "Tuesday"
                            DayId = 3
                        Case "Wednesday"
                            DayId = 4
                        Case "Thursday"
                            DayId = 5
                        Case "Friday"
                            DayId = 6
                        Case "Saturday"
                            DayId = 7
                        Case "Sunday"
                            DayId = 1
                    End Select

                    Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                    Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()

                    For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                        If Convert.ToInt32(row("DayId")) = DayId Then
                            found = True
                            Exit For
                        End If
                    Next

                    If found Then

                    Else
                        Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                        Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                        If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then

                            Return True
                        Else
                            Return False
                        End If
                    End If
                ElseIf radBtnPeriod.Checked Then
                    Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                    Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()
                    Dim dteStartDate As DateTime = dtpStartDatePerm.SelectedDate
                    Dim dteEndDate As DateTime = dtpEndDatePerm.SelectedDate

                    While dteStartDate < dteEndDate
                        Dim strDay As String = dteStartDate.DayOfWeek.ToString()
                        Select Case strDay
                            Case "Monday"
                                DayId = 2
                            Case "Tuesday"
                                DayId = 3
                            Case "Wednesday"
                                DayId = 4
                            Case "Thursday"
                                DayId = 5
                            Case "Friday"
                                DayId = 6
                            Case "Saturday"
                                DayId = 7
                            Case "Sunday"
                                DayId = 1
                        End Select

                        For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                            If Convert.ToInt32(row("DayId")) = DayId Then
                                found = True
                                Exit For
                            End If
                        Next

                        If found Then
                        Else
                            Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                            Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                            If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then

                                Return True
                            Else
                                Return False
                            End If
                        End If
                        dteStartDate.AddDays(1)
                    End While
                Else
                    Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                    Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()
                    For Each item As ListItem In chkWeekDays.Items
                        If item.Selected Then
                            DayId = Convert.ToInt32(item.Value)
                            For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                                If Convert.ToInt32(row("DayId")) = DayId Then
                                    found = True
                                    Exit For
                                End If
                            Next

                            If found Then
                            Else
                                Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                                Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                                If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                                    (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then

                                    Return True
                                Else
                                    Return False
                                End If
                            End If

                        End If
                    Next
                End If
            ElseIf PermissionType = PermissionOption.Nursing Then
                Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()
                Dim dteStartDate As DateTime = dtpStartDatePerm.SelectedDate
                Dim dteEndDate As DateTime = dtpEndDatePerm.SelectedDate

                While dteStartDate < dteEndDate
                    Dim strDay As String = dteStartDate.DayOfWeek.ToString()
                    Select Case strDay
                        Case "Monday"
                            DayId = 2
                        Case "Tuesday"
                            DayId = 3
                        Case "Wednesday"
                            DayId = 4
                        Case "Thursday"
                            DayId = 5
                        Case "Friday"
                            DayId = 6
                        Case "Saturday"
                            DayId = 7
                        Case "Sunday"
                            DayId = 1
                    End Select

                    For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                        If Convert.ToInt32(row("DayId")) = DayId Then
                            found = True
                            Exit For
                        End If
                    Next

                    If found Then
                    Else
                        Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                        Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                        If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then

                            Return True
                        Else
                            Return False
                        End If
                    End If
                    dteStartDate.AddDays(1)
                End While
            ElseIf PermissionType = PermissionOption.Study Then
                Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()
                For Each item As ListItem In chkWeekDays.Items
                    If item.Selected Then
                        DayId = Convert.ToInt32(item.Value)
                        For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                            If Convert.ToInt32(row("DayId")) = DayId Then
                                found = True
                                Exit For
                            End If
                        Next

                        If found Then
                        Else
                            Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                            Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                            If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then

                                Return True
                            Else
                                Return False
                            End If
                        End If

                    End If
                Next
            End If
        ElseIf intWorkScheduleType = Convert.ToInt32(CtlCommon.ScheduleType.Flexible) Then
            objWorkSchedule_Flexible = New WorkSchedule_Flexible()
            objWorkSchedule_Flexible.FK_ScheduleId = intWorkScheduleId
            dtEmpWorkSchedule = objWorkSchedule_Flexible.GetAll()


            Dim DayId As Integer

            If PermissionType = PermissionOption.Normal Then
                If radBtnOneDay.Checked Then
                    Dim strDay As String = dtpPermissionDate.SelectedDate.Value.DayOfWeek.ToString()
                    Select Case strDay
                        Case "Monday"
                            DayId = 2
                        Case "Tuesday"
                            DayId = 3
                        Case "Wednesday"
                            DayId = 4
                        Case "Thursday"
                            DayId = 5
                        Case "Friday"
                            DayId = 6
                        Case "Saturday"
                            DayId = 7
                        Case "Sunday"
                            DayId = 1
                    End Select

                    Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                    Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()

                    For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                        If Convert.ToInt32(row("DayId")) = DayId Then
                            found = True
                            Exit For
                        End If
                    Next

                    If found Then
                    Else
                        Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                        Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()


                        If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then

                            Return True
                        Else
                            Return False
                        End If
                    End If
                ElseIf radBtnPeriod.Checked Then
                    Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                    Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()
                    Dim dteStartDate As DateTime = dtpStartDatePerm.SelectedDate
                    Dim dteEndDate As DateTime = dtpEndDatePerm.SelectedDate

                    While dteStartDate < dteEndDate
                        Dim strDay As String = dteStartDate.DayOfWeek.ToString()
                        Select Case strDay
                            Case "Monday"
                                DayId = 2
                            Case "Tuesday"
                                DayId = 3
                            Case "Wednesday"
                                DayId = 4
                            Case "Thursday"
                                DayId = 5
                            Case "Friday"
                                DayId = 6
                            Case "Saturday"
                                DayId = 7
                            Case "Sunday"
                                DayId = 1
                        End Select

                        For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                            If Convert.ToInt32(row("DayId")) = DayId Then
                                found = True
                                Exit For
                            End If
                        Next

                        If found Then
                        Else
                            Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                            Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                            If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then

                                Return True
                            Else
                                Return False
                            End If
                        End If
                        dteStartDate.AddDays(1)
                    End While
                Else
                    Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                    Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()
                    For Each item As ListItem In chkWeekDays.Items
                        If item.Selected Then
                            DayId = Convert.ToInt32(item.Value)
                            For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                                If Convert.ToInt32(row("DayId")) = DayId Then
                                    found = True
                                    Exit For
                                End If
                            Next

                            If found Then
                            Else
                                Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                                Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                                If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                     (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                     (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                                     (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then

                                    Return True
                                Else
                                    Return False
                                End If
                            End If

                        End If
                    Next
                End If
            ElseIf PermissionType = PermissionOption.Nursing Then
                Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()
                Dim dteStartDate As DateTime = dtpStartDatePerm.SelectedDate
                Dim dteEndDate As DateTime = dtpEndDatePerm.SelectedDate

                While dteStartDate < dteEndDate
                    Dim strDay As String = dteStartDate.DayOfWeek.ToString()
                    Select Case strDay
                        Case "Monday"
                            DayId = 2
                        Case "Tuesday"
                            DayId = 3
                        Case "Wednesday"
                            DayId = 4
                        Case "Thursday"
                            DayId = 5
                        Case "Friday"
                            DayId = 6
                        Case "Saturday"
                            DayId = 7
                        Case "Sunday"
                            DayId = 1
                    End Select

                    For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                        If Convert.ToInt32(row("DayId")) = DayId Then
                            found = True
                            Exit For
                        End If
                    Next

                    If found Then
                    Else
                        Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                        Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                        If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                            (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then

                            Return True
                        Else
                            Return False
                        End If
                    End If
                    dteStartDate.AddDays(1)
                End While
            ElseIf PermissionType = PermissionOption.Study Then
                Dim drEmpWorkScheduleOffDay() As DataRow = dtEmpWorkSchedule.Select("IsOffDay = 1")
                Dim dtEmpWorkScheduleOffDay As DataTable = drEmpWorkScheduleOffDay.CopyToDataTable()
                For Each item As ListItem In chkWeekDays.Items
                    If item.Selected Then
                        DayId = Convert.ToInt32(item.Value)
                        For Each row As DataRow In dtEmpWorkScheduleOffDay.Rows
                            If Convert.ToInt32(row("DayId")) = DayId Then
                                found = True
                                Exit For
                            End If
                        Next

                        If found Then
                        Else
                            Dim drEmpWorkSchedule() As DataRow = dtEmpWorkSchedule.Select("DayId = " + DayId.ToString())
                            Dim dtTempEmpWorkSchedule As DataTable = drEmpWorkSchedule.CopyToDataTable()

                            If ((Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime1").ToString().Insert(2, ":")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("FromTime2").ToString().Insert(2, ":")).ToString("HH:mm")) < (RadTPfromTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime1").ToString().Insert(2, ":")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm")) And _
                                (Convert.ToDateTime(dtTempEmpWorkSchedule(0)("ToTime2").ToString().Insert(2, ":")).ToString("HH:mm")) > (RadTPtoTime.SelectedDate.Value.ToString("HH:mm"))) Then

                                Return True
                            Else
                                Return False
                            End If
                        End If

                    End If
                Next
            End If
        End If

        Return True
    End Function

#End Region

#Region "Start & End date validation"

    Private Function StartEndDateComparison(ByVal dateStartdate As DateTime, _
                                           ByVal dateEndDate As DateTime) As Boolean
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        If IsDate(dtpStartDatePerm.SelectedDate) And IsDate(dtpEndDatePerm.SelectedDate) Then
            ' Get the start and end date from the calenders
            dateStartdate = New DateTime(dtpStartDatePerm.SelectedDate.Value.Year, _
                                         dtpStartDatePerm.SelectedDate.Value.Month, _
                                         dtpStartDatePerm.SelectedDate.Value.Day)
            dateEndDate = New DateTime(dtpEndDatePerm.SelectedDate.Value.Year, _
                                       dtpEndDatePerm.SelectedDate.Value.Month, _
                                       dtpEndDatePerm.SelectedDate.Value.Day)
            Dim result As Integer = DateTime.Compare(dateEndDate, dateStartdate)
            If result < 0 Then
                ' show message and set focus on end date picker
                ' CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DateRangeEarly", CultureInfo))

                dtpEndDatePerm.Focus()
                Return False
            ElseIf result = 0 Then
                ' Show message and focus on the end date picker
                ' CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EndEqualStart", CultureInfo))


                dtpEndDatePerm.Focus()
                Return False
            Else
                ' Do nothing
                Return True
            End If
        End If
    End Function

    Protected Sub dtpEndDatePerm_SelectedDateChanged(ByVal sender As Object, _
                                                ByVal e As SelectedDateChangedEventArgs) _
                                                Handles dtpEndDatePerm.SelectedDateChanged

        If IS_Period() And dtpEndDatePerm.SelectedDate() IsNot Nothing Then
            StartEndDateComparison(dtpStartDatePerm.SelectedDate, dtpEndDatePerm.SelectedDate)

        End If
    End Sub

#End Region

#Region "Handle Selected Time changed"

    Protected Sub RadTPtoTim_SelectedDateChanged(ByVal sender As Object, _
                                                         ByVal e As SelectedDateChangedEventArgs) _
                                                         Handles RadTPtoTime.SelectedDateChanged
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        If (hdnIsvalid.Value) Then
            setTimeDifference()
        Else
            'CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("TimeRange", CultureInfo))

        End If

    End Sub

    Protected Sub RadTPfromTime_SelectedDateChanged(ByVal sender As Object, _
                                                             ByVal e As SelectedDateChangedEventArgs) _
                                                     Handles RadTPfromTime.SelectedDateChanged
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        If (hdnIsvalid.Value) Then
            setTimeDifference()
        Else
            'CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("TimeRange", CultureInfo))
        End If

    End Sub

#End Region

End Class
