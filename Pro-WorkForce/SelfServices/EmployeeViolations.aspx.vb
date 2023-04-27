Imports TA.Employees
Imports System.Data
Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports TA.LookUp
Imports TA.Definitions
Imports TA.SelfServices
Imports System.IO
Imports TA.Security
Imports SmartV.UTILITIES.ProjectCommon
Imports Telerik.Web.UI.Calendar
Imports Telerik.Web.UI.DatePickerPopupDirection
Imports TA.Admin
Imports TA.DailyTasks
Imports TA_Announcements
Imports TA.DashBoard
Imports System.Web.Services

Partial Class SelfServices_EmployeeViolations
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objEmp_LeavesRequest As Emp_LeavesRequest
    Private objEmp_PermissionsRequest As Emp_PermissionsRequest
    Private objProjectCommon As New ProjectCommon
    Private objRequestStatus As New RequestStatus
    Private objLeavesTypes As LeavesTypes
    Private Lang As CtlCommon.Lang
    Private objEmp_Leaves As Emp_Leaves
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Shared SortExepression As String
    Shared dtCurrent As DataTable
    Private objEmployeeViolations As EmployeeViolations
    Private objEmp_Permissions As Emp_Permissions
    Private objPermissionsTypes As PermissionsTypes
    Private objEmp_WorkSchedule As Emp_WorkSchedule
    Private objAppSettings As APP_Settings
    Private objRECALC_REQUEST As RECALC_REQUEST
    Private objEmployee_Manager As Employee_Manager
    Private objAnnouncements As Announcements
    Dim DrillDownDatalvl1 As String = ""
    Dim Str As String
    Dim STRsPLIT() As String
    Dim StrDrill As String
    Dim StrDrillsPLIT() As String
    Dim data As String = "" ' "{name:   'الحضور',  y: 16.11}, {name:   'عدم الحضور',y: 83.89},"
    Dim drills As String = ""
    Private objOrgLevel As New OrgLevel
    Dim dtOrgLevel As New DataTable
    Private objEmployee As Employee
    Private objAPP_Settings As APP_Settings
    Public CalendarLang As String
    Private objDashBoard As DashBoard
    Private objversion As SmartV.Version.version

    Dim EmployeeWorkingHoursCountdata As String = ""

    Dim UK_Culture As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("en-GB")

    Private Enum WeekDayName
        Sunday = 1
        Monday = 2
        Tuesday = 3
        Wednesday = 4
        Thursday = 5
        Friday = 6
        Saturday = 7
    End Enum
#End Region

#Region "Public Properties"

    Public Property LeaveRequestID() As Integer
        Get
            Return ViewState("LeaveRequestID")
        End Get
        Set(ByVal value As Integer)
            ViewState("LeaveRequestID") = value
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
            Return ViewState("dtCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
        End Set
    End Property

    Public Property PermissionId() As Integer
        Get
            Return ViewState("PersmissionId")
        End Get
        Set(ByVal value As Integer)
            ViewState("PersmissionId") = value
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

    Public Property OffAndHolidayDays() As Integer
        Get
            Return ViewState("OffAndHolidayDays")
        End Get
        Set(ByVal value As Integer)
            ViewState("OffAndHolidayDays") = value
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

    Public Property EmployeeViolationType() As String
        Get
            Return ViewState("ViolationType")
        End Get
        Set(ByVal value As String)
            ViewState("ViolationType") = value
        End Set
    End Property

    Public Property IsDevidedPermission() As Boolean
        Get
            Return ViewState("IsDevided")
        End Get
        Set(value As Boolean)
            ViewState("IsDevided") = value
        End Set
    End Property

    Public Property LeaveTypeDuration() As Integer
        Get
            Return ViewState("LeaveTypeDuration")
        End Get
        Set(value As Integer)
            ViewState("LeaveTypeDuration") = value
        End Set
    End Property

    Public Property ApprovedRequired() As Boolean
        Get
            Return ViewState("ApprovedRequired")
        End Get
        Set(value As Boolean)
            ViewState("ApprovedRequired") = value
        End Set
    End Property

    Public Property DevideApprovedRequired() As Boolean
        Get
            Return ViewState("DevideApprovedRequired")
        End Get
        Set(value As Boolean)
            ViewState("DevideApprovedRequired") = value
        End Set
    End Property

    Public Property EmpPermissionRemainingBalance() As Decimal
        Get
            Return ViewState("RemainingBalance")
        End Get
        Set(value As Decimal)
            ViewState("RemainingBalance") = value
        End Set
    End Property

    Public Property EmpDevidePermissionRemainingBalance() As Decimal
        Get
            Return ViewState("DevideRemainingBalance")
        End Get
        Set(value As Decimal)
            ViewState("DevideRemainingBalance") = value
        End Set
    End Property

    Public Property dt() As DataTable
        Get
            Return ViewState("dt")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dt") = value
        End Set
    End Property

    Private Property Gender() As String
        Get
            Return ViewState("Gender")
        End Get
        Set(ByVal value As String)
            ViewState("Gender") = value
        End Set
    End Property

    Private Property OldFromTime() As DateTime '----Represent the orginal violation time before applying any policy or rule
        Get
            Return ViewState("OldFromTime")
        End Get
        Set(ByVal value As DateTime)
            ViewState("OldFromTime") = value
        End Set
    End Property

    Private Property OldToTime() As DateTime '----Represent the orginal violation time before applying any policy or rule
        Get
            Return ViewState("OldToTime")
        End Get
        Set(ByVal value As DateTime)
            ViewState("OldToTime") = value
        End Set
    End Property

    Private Property PermStart() As String
        Get
            Return ViewState("PermStart")
        End Get
        Set(ByVal value As String)
            ViewState("PermStart") = value
        End Set
    End Property

    Private Property PermEnd() As String
        Get
            Return ViewState("PermEnd")
        End Get
        Set(ByVal value As String)
            ViewState("PermEnd") = value
        End Set
    End Property

    Private Property BreakStart() As Integer
        Get
            Return ViewState("BreakStart")
        End Get
        Set(ByVal value As Integer)
            ViewState("BreakStart") = value
        End Set
    End Property

    Private Property BreakEnd() As Integer
        Get
            Return ViewState("BreakEnd")
        End Get
        Set(ByVal value As Integer)
            ViewState("BreakEnd") = value
        End Set
    End Property

    Private Property ShowLeaveLnk_ViolationCorrection() As Boolean
        Get
            Return ViewState("ShowLeaveLnk_ViolationCorrection")
        End Get
        Set(ByVal value As Boolean)
            ViewState("ShowLeaveLnk_ViolationCorrection") = value
        End Set
    End Property

    Private Property ShowPermissionLnk_ViolationCorrection() As Boolean
        Get
            Return ViewState("ShowPermissionLnk_ViolationCorrection")
        End Get
        Set(ByVal value As Boolean)
            ViewState("ShowPermissionLnk_ViolationCorrection") = value
        End Set
    End Property

    Private Property ViolationJustificationDays() As String
        Get
            Return ViewState("ViolationJustificationDays")
        End Get
        Set(ByVal value As String)
            ViewState("ViolationJustificationDays") = value
        End Set
    End Property

    Private Property ViolationJustificationDaysPolicy() As String
        Get
            Return ViewState("ViolationJustificationDaysPolicy")
        End Get
        Set(ByVal value As String)
            ViewState("ViolationJustificationDaysPolicy") = value
        End Set
    End Property

    Private Property intHoliday() As Integer
        Get
            Return ViewState("intHoliday")
        End Get
        Set(ByVal value As Integer)
            ViewState("intHoliday") = value
        End Set
    End Property

    Private Property intLeave() As Integer
        Get
            Return ViewState("intLeave")
        End Get
        Set(ByVal value As Integer)
            ViewState("intLeave") = value
        End Set
    End Property

    Private Property intRestDay() As Integer
        Get
            Return ViewState("intRestDay")
        End Get
        Set(ByVal value As Integer)
            ViewState("intRestDay") = value
        End Set
    End Property
#End Region

#Region "PageEvents"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        Else
            If SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                Page.MasterPageFile = "~/default/ArabicMaster.master"
                CalendarLang = "ar"
            Else
                Lang = CtlCommon.Lang.EN
                Page.MasterPageFile = "~/default/NewMaster.master"
                CalendarLang = "en"
            End If
            Page.UICulture = SessionVariables.CultureInfo
        End If
    End Sub

    Private Sub getPageTitle()
        Dim objSysForms As New SYSForms
        Dim dt As New DataTable
        dt = objSysForms.GetByPK(857)
        If SessionVariables.CultureInfo = "ar-JO" Then
            PageHeader1.HeaderText = dt.Rows(0)("Desc_Ar")
        Else
            PageHeader1.HeaderText = dt.Rows(0)("Desc_En")

        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try


            Dim ShowViolationCorrection As Boolean
            If Not Page.IsPostBack Then

                objAppSettings = New APP_Settings
                objAppSettings.GetByPK()
                ShowViolationCorrection = objAppSettings.ShowViolationCorrection
                ShowLeaveLnk_ViolationCorrection = objAppSettings.ShowLeaveLnk_ViolationCorrection
                ShowPermissionLnk_ViolationCorrection = objAppSettings.ShowPermissionLnk_ViolationCorrection
                ViolationJustificationDays = objAppSettings.ViolationJustificationDays
                ViolationJustificationDaysPolicy = objAppSettings.ViolationJustificationDaysPolicy

                If (Not String.IsNullOrEmpty(ViolationJustificationDaysPolicy)) Then
                    objEmp_Permissions = New Emp_Permissions
                    Dim dt As DataTable = Nothing
                    objEmp_Permissions.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
                    objEmp_Permissions.PermDate = DateAdd(DateInterval.Day, (Convert.ToInt32(ViolationJustificationDays) * -1), DateAdd(DateInterval.Day, (Convert.ToInt32(ViolationJustificationDays)) * -1, DateTime.Now))
                    objEmp_Permissions.PermEndDate = DateTime.Now
                    dt = objEmp_Permissions.Get_DayStatus
                    If (ViolationJustificationDaysPolicy.Contains("1")) Then
                        intHoliday = dt.Rows(0)("HolidayCount")
                    End If
                    If (ViolationJustificationDaysPolicy.Contains("2")) Then
                        intLeave = dt.Rows(0)("LeaveCount")
                    End If
                    If (ViolationJustificationDaysPolicy.Contains("3")) Then
                        intRestDay = dt.Rows(0)("RestCount")
                    End If
                    ViolationJustificationDays = (Convert.ToInt32(ViolationJustificationDays) + intHoliday + intLeave + intRestDay) * -1

                End If


                If ShowViolationCorrection = False Then
                    dgrdEmpViolations.Visible = False
                    RadFilter1.Visible = False
                    lblViolationCorrection.Visible = False
                    dvViolationCorrection.Visible = False
                End If
                If objAppSettings.DivideTwoPermission = False Then
                    lnbDevideTwoPermission.Visible = False
                Else
                    lnbDevideTwoPermission.Visible = True
                End If
                If objAppSettings.ShowAnnouncementSelfService = False Then
                    divAnnouncements.Visible = False

                Else
                    divAnnouncements.Visible = True
                End If


                Dim browserType As String = Request.Browser.Type
                Dim browserVersion As String = Request.Browser.Version

                If browserType.Contains("IE") Then

                End If

                IsDevidedPermission = False
                mvEmpViolations.SetActiveView(viewEmpViolationsList)
                getPageTitle()
                'PageHeader1.HeaderText = ResourceManager.GetString("EmployeeViolations", CultureInfo)

                dteFromDate.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
                Dim dd As New Date
                dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)

                dteToDate.SelectedDate = dd

                FillGridView()
                FillData()
                spanheader.InnerText = IIf(Lang = CtlCommon.Lang.AR, "إعلانات", "Announcements")
                dgrdEmpViolations.Columns(4).Visible = False

                CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)

                ':::Permission Request
                SetRadDateTimePickerPeoperties()
                FillPermissionTypes()
                dtpPermissionDate.SelectedDate = Date.Today
                dtpDevidePermissionDate.SelectedDate = Date.Today

                RadTPfromTime.TimeView.HeaderText = String.Empty
                RadTPtoTime.TimeView.HeaderText = String.Empty
                RadTPfromTime.TimeView.TimeFormat = "HH:mm"
                RadTPfromTime.TimeView.DataBind()
                RadTPtoTime.TimeView.TimeFormat = "HH:mm"
                RadTPtoTime.TimeView.DataBind()

                RadTPDevidefromTime.TimeView.HeaderText = String.Empty
                RadTPDevidetoTime.TimeView.HeaderText = String.Empty
                RadTPDevidefromTime.TimeView.TimeFormat = "HH:mm"
                RadTPDevidefromTime.TimeView.DataBind()
                RadTPDevidetoTime.TimeView.TimeFormat = "HH:mm"
                RadTPDevidetoTime.TimeView.DataBind()
                ':::End Permission request

                ':::Leave Request
                FillLeaveTypes()
                dtpRequestDate.SelectedDate = Date.Today
                RequiredFieldValidator4.InitialValue = ResourceManager.GetString("PleaseSelect", CultureInfo)
                '::End Leave Request

                FillHighCharts()
                fillChart()
                FillEmployeesWorkingHoursDash()
                Dim ChartTitleText As String = ""
                JSONChartSubTitleText.Text = "<script>var Var_ChartSubtitleText =''; </Script>"
                JSONChartTitleText.Text = "<script>var Var_ChartTitleText ='" & ChartTitleText & "' ; </Script>"
                JSONChartType.Text = "<script>var Var_ChartType ='pie' ; </Script>"
                JSONSeries.Text = "<script>var Var_Series = [{name:   'Overview',colorByPoint: true,data: [" & data & "]  }]; </Script>"

                ArcivingMonths_DateValidation()

            End If

            If ViolationJustificationDays = 0 Then
                ViolationJustificationDays = Nothing
            End If

            ' You can put the suitable caption for the TimeView
            RadTPfromTime.TimeView.HeaderText = String.Empty
            RadTPtoTime.TimeView.HeaderText = String.Empty
            RadTPfromTime.TimeView.TimeFormat = "HH:mm"
            RadTPfromTime.TimeView.DataBind()
            RadTPtoTime.TimeView.TimeFormat = "HH:mm"
            RadTPtoTime.TimeView.DataBind()
            lblRemainingBalance.Visible = False
            lblRemainingBalanceHours.Visible = False
            lblRemainingBalanceValue.Visible = False

            VisibleChart()
            VisibleCalendar()
            '----------------SHOW HIDE DIVs-------------------'
            'If SummaryPage1.VisibleInforCount = 0 Then
            '    dvSummaryPage.Visible = False
            'Else
            '    dvSummaryPage.Visible = True
            'End If
            'If container.Visible = False Then
            '    dvDashboard.Visible = False
            'End If
            'If dvDashboard.Visible = False And dvSummaryPage.Visible = False Then
            '    dvSummaryandDashboard.Visible = False
            'Else
            '    dvSummaryandDashboard.Visible = True
            'End If
            '----------------SHOW HIDE DIVs-------------------'


            'Dim formPath As String = Request.Url.AbsoluteUri
            'Dim strArr() As String = formPath.Split("/")
            'Dim objSysForms As New SYSForms
            'dtCurrentControls = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
            'For Each row As DataRow In dtCurrentControls.Rows
            '    If Not row("AllowAdd") = 1 Then
            '        If Not IsDBNull(row("AddBtnName")) Then
            '            If Not pnlFilter.FindControl(row("AddBtnName")) Is Nothing Then
            '                pnlFilter.FindControl(row("AddBtnName")).Visible = False
            '            End If
            '        End If
            '    End If

            '    If Not row("AllowDelete") = 1 Then
            '        If Not IsDBNull(row("DeleteBtnName")) Then
            '            If Not pnlFilter.FindControl(row("DeleteBtnName")) Is Nothing Then
            '                pnlFilter.FindControl(row("DeleteBtnName")).Visible = False
            '            End If
            '        End If
            '    End If

            '    If Not row("AllowSave") = 1 Then
            '        If Not IsDBNull(row("EditBtnName")) Then
            '            If Not pnlFilter.FindControl(row("EditBtnName")) Is Nothing Then
            '                pnlFilter.FindControl(row("EditBtnName")).Visible = False
            '            End If
            '        End If
            '    End If

            '    If Not row("AllowPrint") = 1 Then
            '        If Not IsDBNull(row("PrintBtnName")) Then
            '            If Not pnlFilter.FindControl(row("PrintBtnName")) Is Nothing Then
            '                pnlFilter.FindControl(row("PrintBtnName")).Visible = False
            '            End If
            '        End If
            '    End If
            'Next


        Catch ex As Exception

        End Try
    End Sub

    Private Sub VisibleChart()
        Dim RequestGridToAppear As String = ""
        objAppSettings = New APP_Settings
        With objAppSettings
            .GetByPK()
            For Each i As String In .DurationTotalsToAppear.Split(",")

                If i = "14" Then
                    container.Visible = True
                ElseIf i = "17" Then
                    WorkHourscontainer.Visible = True
                End If
            Next
        End With
    End Sub

    Private Sub VisibleCalendar()
        Dim RequestGridToAppear As String = ""
        objAppSettings = New APP_Settings
        With objAppSettings
            .GetByPK()
            For Each i As String In .DurationTotalsToAppear.Split(",")

                If i = "16" Then
                    dvCalendar.Visible = True
                End If
            Next
        End With
    End Sub

    Protected Sub dgrdEmpViolations_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles dgrdEmpViolations.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim item As GridDataItem
            item = e.Item
            Dim strContactDuration As New StringBuilder
            Dim strContactType As New StringBuilder

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("M_DATE").ToString())) And (Not item.GetDataKeyValue("M_DATE").ToString() = "")) Then
                Dim violationDate As DateTime = item.GetDataKeyValue("M_DATE")
                item("M_DATE").Text = Convert.ToDateTime(violationDate, UK_Culture).ToString("dd/MM/yyyy") 'violationDate.ToShortDateString()
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("M_DATE").ToString())) And (Not item.GetDataKeyValue("M_DATE").ToString() = "")) Then
                Dim violationDate As DateTime = Convert.ToDateTime(item.GetDataKeyValue("M_DATE"), UK_Culture) '.ToString("dd/MM/yyyy")
                Dim Diff_violationDate As DateTime = DateAdd(DateInterval.Day, Convert.ToInt32(ViolationJustificationDays * -1), Date.Today)
                If Not ViolationJustificationDays = Nothing Then

                    If violationDate <= Diff_violationDate Then
                        DirectCast(item.FindControl("lnbPermissionRequest"), LinkButton).Enabled = True

                        If Lang = CtlCommon.Lang.AR Then
                            DirectCast(item.FindControl("lnbPermissionRequest"), LinkButton).Text = "تأخر تقديم"
                        Else
                            DirectCast(item.FindControl("lnbPermissionRequest"), LinkButton).Text = "Overdue"
                        End If

                    End If
                End If
            End If
            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("Status").ToString())) And (Not item.GetDataKeyValue("Status").ToString() = "")) Then
                If item.GetDataKeyValue("Status").ToString() = "A" Then
                    item("Type").Text = ResourceManager.GetString("Absent", CultureInfo)
                    item.FindControl("lnbPermissionRequest").Visible = False
                    If ShowLeaveLnk_ViolationCorrection = True Then
                        item.FindControl("lnbLeaveRequest").Visible = True
                    Else
                        item.FindControl("lnbLeaveRequest").Visible = False
                    End If

                End If
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("Description").ToString())) And (Not item.GetDataKeyValue("Description").ToString() = "")) Then
                If item.GetDataKeyValue("Description").ToString().Contains("Delay") Then
                    Dim strDelay() As String = item.GetDataKeyValue("Description").ToString().Split(":")
                    item("Type").Text = ResourceManager.GetString("Delay", CultureInfo)

                    If ShowPermissionLnk_ViolationCorrection = True Then
                        item.FindControl("lnbPermissionRequest").Visible = True
                    Else
                        item.FindControl("lnbPermissionRequest").Visible = False
                    End If

                    item.FindControl("lnbLeaveRequest").Visible = False

                    item("Delay").Text = strDelay(1).ToString() + ":" + strDelay(2).ToString()
                    item("Duration").Text = item.GetDataKeyValue("Delay").ToString()

                    strContactType.AppendLine(item("Type").Text & "<br/>")
                    strContactDuration.AppendLine(item.GetDataKeyValue("Duration").ToString() & "<br/>")
                End If
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("Description").ToString())) And (Not item.GetDataKeyValue("Description").ToString() = "")) Then
                If item.GetDataKeyValue("Description").ToString().Contains("Early Out") Then
                    Dim strEarlyOut() As String = item.GetDataKeyValue("Description").ToString().Split(":")
                    item("Type").Text = ResourceManager.GetString("EarlyOut", CultureInfo)

                    If ShowPermissionLnk_ViolationCorrection = True Then
                        item.FindControl("lnbPermissionRequest").Visible = True
                    Else
                        item.FindControl("lnbPermissionRequest").Visible = False
                    End If

                    item.FindControl("lnbLeaveRequest").Visible = False
                    item("Early_Out").Text = strEarlyOut(1).ToString() + ":" + strEarlyOut(2).ToString()
                    item("Duration").Text = item("Early_Out").Text
                    If Not String.IsNullOrEmpty(strContactType.ToString()) AndAlso Not String.IsNullOrEmpty(strContactDuration.ToString()) Then
                        strContactType.AppendLine(item("Type").Text)
                        strContactDuration.AppendLine(item.GetDataKeyValue("Duration").ToString())

                        item("Type").Text = strContactType.ToString()
                        item("Duration").Text = strContactDuration.ToString()

                    End If
                End If
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("Description").ToString())) And (Not item.GetDataKeyValue("Description").ToString() = "")) Then
                If item.GetDataKeyValue("Description").ToString().Contains("Out Time") Then
                    Dim strOutTime() As String = item.GetDataKeyValue("Description").ToString().Split(":")
                    item("Type").Text = ResourceManager.GetString("OutTime", CultureInfo)

                    If ShowPermissionLnk_ViolationCorrection = True Then
                        item.FindControl("lnbPermissionRequest").Visible = True
                    Else
                        item.FindControl("lnbPermissionRequest").Visible = False
                    End If

                    item.FindControl("lnbLeaveRequest").Visible = False

                    item("OutTime").Text = strOutTime(1).ToString() + ":" + strOutTime(2).ToString()
                    item("Duration").Text = item.GetDataKeyValue("OutTime").ToString()

                    If Not String.IsNullOrEmpty(strContactType.ToString()) AndAlso Not String.IsNullOrEmpty(strContactDuration.ToString()) Then
                        strContactType.AppendLine(item("Type").Text)
                        strContactDuration.AppendLine(item.GetDataKeyValue("Duration").ToString())

                        item("Type").Text = strContactType.ToString()
                        item("Duration").Text = strContactDuration.ToString()

                    End If
                End If
            End If

            Dim statusName As String = String.Empty
            If Not item.GetDataKeyValue("PermStart").ToString() = "" Then



                If (PermissionRequestIsExist(Convert.ToDateTime(item.GetDataKeyValue("M_DATE")), Convert.ToDateTime(item.GetDataKeyValue("PermStart")), Convert.ToDateTime(item.GetDataKeyValue("PermEnd")))) Then

                    If item.FindControl("lnbPermissionRequest").Visible = True Then

                        DirectCast(item.FindControl("lnbPermissionRequest"), LinkButton).Text = PermissionRequeststatus(Convert.ToDateTime(item.GetDataKeyValue("M_DATE")), Convert.ToDateTime(item.GetDataKeyValue("PermStart")), Convert.ToDateTime(item.GetDataKeyValue("PermEnd")))
                        If (DirectCast(item.FindControl("lnbPermissionRequest"), LinkButton).Text = ResourceManager.GetString("RejectedByDM", CultureInfo)) Or (DirectCast(item.FindControl("lnbPermissionRequest"), LinkButton).Text = ResourceManager.GetString("RejectedByHR", CultureInfo)) Or (DirectCast(item.FindControl("lnbPermissionRequest"), LinkButton).Text = ResourceManager.GetString("RejectedByGM", CultureInfo)) Then
                            DirectCast(item.FindControl("lnbPermissionRequest"), LinkButton).Enabled = True
                        Else
                            DirectCast(item.FindControl("lnbPermissionRequest"), LinkButton).Enabled = False
                        End If

                    End If
                End If

                If (PermissionIsExist(Convert.ToDateTime(item.GetDataKeyValue("M_DATE")), Convert.ToDateTime(item.GetDataKeyValue("PermStart")), Convert.ToDateTime(item.GetDataKeyValue("PermEnd")))) Then

                    item.Display = False

                End If
            End If
            Dim leaveStatusName As String = String.Empty

            If (LeaveIsExsist(item.GetDataKeyValue("M_DATE"), leaveStatusName)) Then
                If item.FindControl("lnbLeaveRequest").Visible = True Then
                    DirectCast(item.FindControl("lnbLeaveRequest"), LinkButton).Text = ResourceManager.GetString("Pending", CultureInfo)
                    DirectCast(item.FindControl("lnbLeaveRequest"), LinkButton).Enabled = False
                End If
            End If

        End If
    End Sub

    Protected Sub dgrdEmpViolations_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdEmpViolations.NeedDataSource

        objEmployeeViolations = New EmployeeViolations
        With objEmployeeViolations
            .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            .FromDate = dteFromDate.SelectedDate
            .ToDate = dteToDate.SelectedDate
            dtCurrent = .GetEmployeeViolations()
        End With

        Dim dv As New DataView(dtCurrent)
        dv.Sort = SortExepression
        dgrdEmpViolations.DataSource = dv

    End Sub

    Protected Sub btnRetrieve_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRetrieve.Click
        'SummaryPage1.FromDate = Date.ParseExact(Convert.ToDateTime(dteFromDate.SelectedDate).Month.ToString("00") + "/01/" + Convert.ToDateTime(dteFromDate.SelectedDate).Year.ToString("0000"), "MM/dd/yyyy", Nothing)
        'Dim dd As New Date
        'dd = Date.ParseExact(Convert.ToDateTime(dteFromDate.SelectedDate).Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Convert.ToDateTime(dteFromDate.SelectedDate).Month).ToString("00") + "/" + Convert.ToDateTime(dteFromDate.SelectedDate).Year.ToString("0000"), "MM/dd/yyyy", Nothing)
        'SummaryPage1.ToDate = dd
        SummaryPage1.FromDate = dteFromDate.SelectedDate
        SummaryPage1.ToDate = dteToDate.SelectedDate
        SummaryPage1.FillInfo()
        FillGridView()
        FillHighCharts()
        fillChart()
        FillEmployeesWorkingHoursDash()
        Dim ChartTitleText As String = ""
        JSONChartSubTitleText.Text = "<script>var Var_ChartSubtitleText =''; </Script>"
        JSONChartTitleText.Text = "<script>var Var_ChartTitleText ='" & ChartTitleText & "' ; </Script>"
        JSONChartType.Text = "<script>var Var_ChartType ='pie' ; </Script>"
        JSONSeries.Text = "<script>var Var_Series = [{name:   'Overview',colorByPoint: true,data: [" & data & "]  }]; </Script>"
    End Sub

    Protected Sub lnbPermissionRequest_Click(ByVal sender As Object, ByVal e As EventArgs)

        Dim gvr As GridDataItem = CType(sender, LinkButton).Parent.Parent
        dtpPermissionDate.SelectedDate = Convert.ToDateTime(dgrdEmpViolations.Items(gvr.ItemIndex).GetDataKeyValue("M_DATE"))
        dtpDevidePermissionDate.SelectedDate = Convert.ToDateTime(dgrdEmpViolations.Items(gvr.ItemIndex).GetDataKeyValue("M_DATE"))
        RadTPfromTime.SelectedDate = Convert.ToDateTime(dgrdEmpViolations.Items(gvr.ItemIndex)("PermStart").Text.Trim)
        RadTPtoTime.SelectedDate = Convert.ToDateTime(dgrdEmpViolations.Items(gvr.ItemIndex)("PermEnd").Text.Trim)

        If Not dgrdEmpViolations.Items(gvr.ItemIndex)("BreakStart").Text.Trim = "&nbsp;" Then
            BreakStart = Convert.ToInt32(dgrdEmpViolations.Items(gvr.ItemIndex)("BreakStart").Text.Trim)
            BreakEnd = Convert.ToInt32(dgrdEmpViolations.Items(gvr.ItemIndex)("BreakEnd").Text.Trim)
            PermStart = dgrdEmpViolations.Items(gvr.ItemIndex)("PermStart").Text.Trim
            PermEnd = dgrdEmpViolations.Items(gvr.ItemIndex)("PermEnd").Text.Trim
            Dim intPermStart As Integer = (CInt(PermStart.ToString.Split(":")(0)) * 60) + CInt(PermStart.ToString.Split(":")(1))
            Dim intPermEnd As Integer = (CInt(PermEnd.ToString.Split(":")(0)) * 60) + CInt(PermEnd.ToString.Split(":")(1))
            Dim IntBrkDuration As Integer = BreakEnd - BreakStart
            Dim rslDatePermStart As DateTime = Convert.ToDateTime(CtlCommon.GetFullTimeString(intPermStart + IntBrkDuration))

            If intPermEnd < BreakEnd Then
                RadTPDevidefromTime.SelectedDate = Convert.ToDateTime(dgrdEmpViolations.Items(gvr.ItemIndex)("PermStart").Text.Trim)
                RadTPDevidetoTime.SelectedDate = Convert.ToDateTime(dgrdEmpViolations.Items(gvr.ItemIndex)("PermEnd").Text.Trim)
                setTimeDifference()

            ElseIf intPermStart < BreakStart Then
                RadTPfromTime.SelectedDate = rslDatePermStart
                setTimeDifference()
                RadTPfromTime.SelectedDate = Convert.ToDateTime(PermStart)
            Else
                RadTPDevidefromTime.SelectedDate = Convert.ToDateTime(dgrdEmpViolations.Items(gvr.ItemIndex)("PermStart").Text.Trim)
                RadTPDevidetoTime.SelectedDate = Convert.ToDateTime(dgrdEmpViolations.Items(gvr.ItemIndex)("PermEnd").Text.Trim)
                setTimeDifference()
            End If
        ElseIf Not dgrdEmpViolations.Items(gvr.ItemIndex)("BreakTime").Text.Trim = "&nbsp;" And (dgrdEmpViolations.Items(gvr.ItemIndex)("OutTime").Text.Trim = "&nbsp;" And dgrdEmpViolations.Items(gvr.ItemIndex)("Delay").Text.Trim = "&nbsp;" And dgrdEmpViolations.Items(gvr.ItemIndex)("Early_Out").Text.Trim = "&nbsp;") Then 'added for MOFA to be tested 
            Dim BreakTime As Integer = Convert.ToInt32(dgrdEmpViolations.Items(gvr.ItemIndex)("BreakTime").Text.Trim)
            PermStart = dgrdEmpViolations.Items(gvr.ItemIndex)("PermStart").Text.Trim
            Dim intPermStart As Integer = (CInt(PermStart.ToString.Split(":")(0)) * 60) + CInt(PermStart.ToString.Split(":")(1))


            RadTPfromTime.SelectedDate = CtlCommon.ConvertintToTime(intPermStart + BreakTime)
            RadTPtoTime.SelectedDate = Convert.ToDateTime(dgrdEmpViolations.Items(gvr.ItemIndex)("PermEnd").Text.Trim)
            setTimeDifference()
        Else
            RadTPDevidefromTime.SelectedDate = Convert.ToDateTime(dgrdEmpViolations.Items(gvr.ItemIndex)("PermStart").Text.Trim)
            RadTPDevidetoTime.SelectedDate = Convert.ToDateTime(dgrdEmpViolations.Items(gvr.ItemIndex)("PermEnd").Text.Trim)
            setTimeDifference()
        End If
        OldFromTime = RadTPfromTime.SelectedDate
        OldToTime = RadTPtoTime.SelectedDate
        mvEmpViolations.SetActiveView(viewPermissionRequest)
        RadTPtoTime.Enabled = False
    End Sub

    Protected Sub lnbLeaveRequest_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim gvr As GridDataItem = CType(sender, LinkButton).Parent.Parent
        dtpFromDate.SelectedDate = Convert.ToDateTime(dgrdEmpViolations.Items(gvr.ItemIndex).GetDataKeyValue("M_DATE"))
        dtpToDate.SelectedDate = Convert.ToDateTime(dgrdEmpViolations.Items(gvr.ItemIndex).GetDataKeyValue("M_DATE"))

        dtpFromDate.Enabled = False
        dtpToDate.Enabled = False

        objEmp_LeavesRequest = New Emp_LeavesRequest()
        With objEmp_LeavesRequest
            .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            .FromDate = Convert.ToDateTime(dgrdEmpViolations.Items(gvr.ItemIndex).GetDataKeyValue("M_DATE"))
            .GetForEmpViolations()
            LeaveRequestID = .LeaveId
            If Not LeaveRequestID = 0 Then
                FillLeaveControl()
            End If
        End With

        mvEmpViolations.SetActiveView(viewLeaveRequest)
    End Sub

    '::Permission Reuest

    Protected Sub btnSavePermission_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSavePermission.Click
        Dim errNo As Integer
        EmpLeaveTotalBalance = 0
        OffAndHolidayDays = 0
        Dim ErrorMessage As String = String.Empty
        objEmp_Leaves = New Emp_Leaves
        objEmp_PermissionsRequest = New Emp_PermissionsRequest
        objLeavesTypes = New LeavesTypes
        objPermissionsTypes = New PermissionsTypes
        objEmp_Permissions = New Emp_Permissions

        objEmp_PermissionsRequest = New Emp_PermissionsRequest()
        objEmp_PermissionsRequest.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
        objEmp_PermissionsRequest.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
        objEmp_PermissionsRequest.FK_PermId = RadCmpPermissions.SelectedValue
        objEmp_PermissionsRequest.PermDate = dtpPermissionDate.SelectedDate

        If SessionVariables.CultureInfo = "ar-JO" Then
            objEmp_PermissionsRequest.Lang = 0
        Else
            objEmp_PermissionsRequest.Lang = 1
        End If


        dtCurrent = objEmp_PermissionsRequest.GetByEmployee()

        If IsDevidedPermission Then

            If (RadTPDevidefromTime.SelectedDate = RadTPfromTime.SelectedDate) Or
               (RadTPDevidetoTime.SelectedDate = RadTPtoTime.SelectedDate) Or
               (RadCmpDevidePermissions.SelectedValue = RadCmpPermissions.SelectedValue) Then

                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DevidedPermission", CultureInfo), "info")
                Return

            End If

        End If

        'If Not CheckPermissionTypeDuration() Then
        '    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("PermissionTypeDurationLimit"))
        '    Return
        'End If

        'If IsDevidedPermission Then
        '    If Not CheckDevidePermissionTypeDuration() Then
        '        If Lang = CtlCommon.Lang.EN Then
        '            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("PermissionTypeDurationLimitDevide"))
        '        End If
        '        Return
        '    End If
        'End If

        With objPermissionsTypes

            .PermId = RadCmpPermissions.SelectedValue
            .GetByPK()

            Dim ErrMessage As String
            Dim IsManager As Boolean
            'objEmployee_Manager.FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
            'If objEmployee_Manager.IsManager = False Then
            '    IsManager = False
            'Else
            '    IsManager = True
            'End If
            If .ExcludeManagers_FromAfterBefore And IsManager Then

            Else


                If Not objPermissionsTypes.AllowedAfterDays = Nothing Then
                    If (IIf(radBtnPeriod.Checked, dtpStartDatePerm.SelectedDate, dtpPermissionDate.SelectedDate)) > DateTime.Today.AddDays(.AllowedAfterDays) Then
                        If Lang = CtlCommon.Lang.AR Then
                            ErrMessage = "نوع المغادرة التي قمت باختيارها لايمكن طلبها لتاريخ بعد : " & .AllowedAfterDays.ToString() & " أيام"
                            CtlCommon.ShowMessage(Me.Page, ErrMessage, "info")
                        Else
                            ErrMessage = "The Selected Permission Type Not Allowed After : " & .AllowedAfterDays & " Day(s)"
                            CtlCommon.ShowMessage(Me.Page, ErrMessage, "info")
                        End If

                        Return
                    End If
                End If

                If Not objPermissionsTypes.AllowedBeforeDays = Nothing Then
                    If IIf(radBtnPeriod.Checked, dtpEndDatePerm.SelectedDate, dtpPermissionDate.SelectedDate) <= DateTime.Today.AddDays(.AllowedBeforeDays * -1) Then
                        If Lang = CtlCommon.Lang.AR Then
                            ErrMessage = "نوع المغادرة التي قمت باختيارها لايمكن طلبها لتاريخ قبل : " & .AllowedBeforeDays.ToString() + " أيام "
                            CtlCommon.ShowMessage(Me.Page, ErrMessage, "info")
                        Else
                            ErrMessage = "The Selected Permission Type Not Allowed Before : " & .AllowedBeforeDays & " Day(s)"
                            CtlCommon.ShowMessage(Me.Page, ErrMessage, "info")
                        End If

                        Return
                    End If
                End If
            End If
        End With

        If (objEmp_PermissionsRequest.CheckAllowedPermissionTypeOccurance(ErrorMessage)) = False Then
            If (ErrorMessage <> String.Empty) Then
                CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
                Return
            End If
        End If

        If (objEmp_PermissionsRequest.IsAllowedToRequest(ErrorMessage)) = False Then
            If (ErrorMessage <> String.Empty) Then
                CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
                Return
            End If
        End If

        If (objEmp_PermissionsRequest.ValidateEmployeePermission(SessionVariables.LoginUser.FK_EmployeeId, PermissionId, dtCurrent, radBtnOneDay.Checked,
                                                           dtpStartDatePerm.SelectedDate, dtpEndDatePerm.SelectedDate, RadTPfromTime.SelectedDate,
                                                           RadTPtoTime.SelectedDate, dtpPermissionDate.SelectedDate, objPermissionsTypes, RadCmpPermissions.SelectedValue,
                                                           chckFullDay.Checked, ErrorMessage, OffAndHolidayDays, EmpLeaveTotalBalance) = False) Then
            If (ErrorMessage <> String.Empty) Then
                CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
                Return
            End If
        End If


        Dim validateEmployeePermission As Boolean = objEmp_Permissions.ValidateEmployeePermission(SessionVariables.LoginUser.FK_EmployeeId, PermissionId, dtCurrent, radBtnOneDay.Checked,
                                                        dtpStartDatePerm.SelectedDate, dtpEndDatePerm.SelectedDate, RadTPfromTime.SelectedDate,
                                                        RadTPtoTime.SelectedDate, dtpPermissionDate.SelectedDate, objPermissionsTypes, RadCmpPermissions.SelectedValue,
                                                        chckFullDay.Checked, ErrorMessage, OffAndHolidayDays, EmpLeaveTotalBalance)

        Dim validateEmployeeDevidePermission As Boolean = False
        If IsDevidedPermission Then
            validateEmployeeDevidePermission = objEmp_Permissions.ValidateEmployeePermission(SessionVariables.LoginUser.FK_EmployeeId, PermissionId, dtCurrent, radDevideBtnOneDay.Checked,
                                                            dtpDevideStartDatePerm.SelectedDate, dtpDevideEndDatePerm.SelectedDate, RadTPDevidefromTime.SelectedDate,
                                                            RadTPDevidetoTime.SelectedDate, dtpDevidePermissionDate.SelectedDate, objPermissionsTypes, RadCmpDevidePermissions.SelectedValue,
                                                            chckDevideFullDay.Checked, ErrorMessage, OffAndHolidayDays, EmpLeaveTotalBalance)

        End If

        If (validateEmployeePermission = False AndAlso IsDevidedPermission = False) Or
           ((validateEmployeePermission = False) Or (validateEmployeeDevidePermission = False AndAlso IsDevidedPermission = True)) Then

            If (ErrorMessage <> String.Empty) Then
                CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
                Return
            End If
        Else

            radBtnOneDay.Checked = True
            radDevideBtnOneDay.Checked = True

            FillObjectData()

            'For Check if Remarks is Manadetory
            If txtRemarks.Text.Trim() = "" Then
                If objPermissionsTypes.RemarksIsMandatory = True Then
                    If Lang = CtlCommon.Lang.AR Then
                        CtlCommon.ShowMessage(Me.Page, "يرجى ادخال الملاحظات", "info")
                    Else
                        CtlCommon.ShowMessage(Me.Page, "Please Enter Remarks", "info")
                    End If
                    Exit Sub
                End If
            End If
            ''''''
            'For Check if Attachment is Manadetory
            If fuAttachFile.HasFile = False Then
                If objPermissionsTypes.AttachmentIsMandatory = True Then
                    If Lang = CtlCommon.Lang.AR Then
                        CtlCommon.ShowMessage(Me.Page, "يرجى ارفاق ملف!", "info")
                    Else
                        CtlCommon.ShowMessage(Me.Page, "Please Attach File!", "info")
                    End If
                    Exit Sub
                End If
            End If
            ''''''


            If PermissionId = 0 Then
                If ApprovedRequired Then
                    objEmp_PermissionsRequest.FK_StatusId = Convert.ToInt32(CtlCommon.RequestStatusEnum.Pending)
                    errNo = objEmp_PermissionsRequest.Add()
                    If IsDevidedPermission = False Then
                        PermissionId = objEmp_PermissionsRequest.PermissionId
                    End If
                Else
                    objEmp_PermissionsRequest.FK_StatusId = Convert.ToInt32(CtlCommon.RequestStatusEnum.AutomaticApproved)
                    errNo = objEmp_PermissionsRequest.Add()
                    If IsDevidedPermission = False Then
                        PermissionId = objEmp_PermissionsRequest.PermissionId
                    End If
                    SaveEmployeePermission(ErrorMessage)
                End If

                If fuAttachFile.HasFile Then
                    Dim extention As String = Path.GetExtension(fuAttachFile.PostedFile.FileName)
                    Dim fileName As String = objEmp_PermissionsRequest.PermissionId.ToString()
                    Dim fPath As String = String.Empty
                    fPath = Server.MapPath("..\PermissionRequestFiles\\" + fileName + extention)
                    fuAttachFile.PostedFile.SaveAs(fPath)
                End If

                If errNo = 0 AndAlso IsDevidedPermission = False Then
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully"), "success")
                    'ScriptManager.RegisterStartupScript(Page, Page.GetType, Guid.NewGuid.ToString, "RefreshPage()", True)
                ElseIf IsDevidedPermission = False Then
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave"), "error")
                End If

            Else
                objEmp_PermissionsRequest.PermissionId = PermissionId
                objEmp_PermissionsRequest.FK_StatusId = 1
                errNo = objEmp_PermissionsRequest.Update()
                If errNo = 0 AndAlso IsDevidedPermission = False Then
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("UpdatedSuccessfully"), "success")
                    'ScriptManager.RegisterStartupScript(Page, Page.GetType, Guid.NewGuid.ToString, "RefreshPage()", True)
                ElseIf IsDevidedPermission = False Then
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate"), "error")
                End If
            End If

            If IsDevidedPermission Then
                FillDevideObjectData()
                If PermissionId = 0 Then
                    If DevideApprovedRequired Then
                        objEmp_PermissionsRequest.FK_StatusId = Convert.ToInt32(CtlCommon.RequestStatusEnum.Pending)
                        errNo = objEmp_PermissionsRequest.Add()
                        PermissionId = objEmp_PermissionsRequest.PermissionId
                    Else
                        objEmp_PermissionsRequest.FK_StatusId = Convert.ToInt32(CtlCommon.RequestStatusEnum.AutomaticApproved)
                        errNo = objEmp_PermissionsRequest.Add()
                        PermissionId = objEmp_PermissionsRequest.PermissionId
                        SaveEmployeePermission(ErrorMessage)
                    End If

                    If fuDevideAttachFile.HasFile Then
                        Dim extention As String = Path.GetExtension(fuDevideAttachFile.PostedFile.FileName)
                        Dim fileName As String = objEmp_PermissionsRequest.PermissionId.ToString()
                        Dim fPath As String = String.Empty
                        fPath = Server.MapPath("..\PermissionRequestFiles\\" + fileName + extention)
                        fuDevideAttachFile.PostedFile.SaveAs(fPath)
                    End If

                    If errNo = 0 Then
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully"), "success")
                        'ScriptManager.RegisterStartupScript(Page, Page.GetType, Guid.NewGuid.ToString, "RefreshPage()", True)
                    Else
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave"), "error")
                    End If

                Else
                    objEmp_PermissionsRequest.PermissionId = PermissionId
                    objEmp_PermissionsRequest.FK_StatusId = 1
                    errNo = objEmp_PermissionsRequest.Update()
                    If errNo = 0 Then
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("UpdatedSuccessfully"), "success")
                        'ScriptManager.RegisterStartupScript(Page, Page.GetType, Guid.NewGuid.ToString, "RefreshPage()", True)
                    Else
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate"), "error")
                    End If
                End If
            End If

            If errNo = 0 Then
                ClearPermissionAll()
                mvEmpViolations.SetActiveView(viewEmpViolationsList)
            End If
            FillGridView()
        End If
    End Sub

    Protected Sub radBtnPeriod_CheckedChanged(ByVal sender As Object,
                                                  ByVal e As System.EventArgs) Handles radBtnPeriod.CheckedChanged
        ShowHide(True)
    End Sub

    Protected Sub radBtnOneDay_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radBtnOneDay.CheckedChanged
        ShowHide(False)
    End Sub

    Protected Sub radDevideBtnPeriod_CheckedChanged(ByVal sender As Object,
                                                  ByVal e As System.EventArgs) Handles radDevideBtnPeriod.CheckedChanged
        DevideShowHide(True)
    End Sub

    Protected Sub radDevideBtnOneDay_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radDevideBtnOneDay.CheckedChanged
        DevideShowHide(False)
    End Sub

    Protected Sub btnClearPermission_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearPermission.Click
        ClearPermissionAll()
    End Sub

    Protected Sub chckFullDay_CheckedChanged(ByVal sender As Object,
                                             ByVal e As System.EventArgs) _
                                             Handles chckFullDay.CheckedChanged
        DisableEnableTimeView(Not chckFullDay.Checked)

    End Sub

    Protected Sub chckDevideFullDay_CheckedChanged(ByVal sender As Object,
                                             ByVal e As System.EventArgs) _
                                             Handles chckDevideFullDay.CheckedChanged
        DisableEnableDevideTimeView(Not chckDevideFullDay.Checked)

    End Sub

    Protected Sub btnCancelPermission_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelPermission.Click
        ClearPermissionAll()
        FillHighCharts()
        mvEmpViolations.SetActiveView(viewEmpViolationsList)
    End Sub

    Protected Sub RadCmpPermissions_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmpPermissions.SelectedIndexChanged
        objPermissionsTypes = New PermissionsTypes()
        With objPermissionsTypes
            .PermId = Convert.ToInt32(RadCmpPermissions.SelectedValue)
            .GetByPK()
            ApprovedRequired = .ApprovalRequired
            If Lang = CtlCommon.Lang.EN Then
                dvPermissionGeneralGuide.InnerHtml = .GeneralGuide
            ElseIf Lang = CtlCommon.Lang.AR Then
                dvPermissionGeneralGuide.InnerHtml = .GeneralGuideAr
            End If


            If Not .FK_LeaveIdtoallowduration = 0 Then
                LeaveTypeDuration = .DurationAllowedwithleave
            End If

            If .HasPermissionTimeControls = True Then
                dvTimeControls.Visible = True
            Else
                dvTimeControls.Visible = False
            End If

            If .HasFullDayPermission = True Then
                trFullyDay.Visible = True
            Else
                trFullyDay.Visible = False
            End If
        End With


        objEmp_Permissions = New Emp_Permissions()

        lblRemainingBalanceValue.Text = objEmp_Permissions.GetPermissionRemainingBalance(RadCmpPermissions.SelectedValue, SessionVariables.LoginUser.FK_EmployeeId, radBtnOneDay.Checked, dtpPermissionDate.SelectedDate.Value, dtpStartDatePerm.SelectedDate.Value, dtpEndDatePerm.SelectedDate.Value)


        If objPermissionsTypes.ShowRemainingBalance = False Then
            lblRemainingBalance.Visible = False
            lblRemainingBalanceHours.Visible = False
            lblRemainingBalanceValue.Visible = False
        Else
            lblRemainingBalance.Visible = True
            lblRemainingBalanceHours.Visible = True
            lblRemainingBalanceValue.Visible = True
        End If

        If Not RadCmpPermissions.SelectedValue = -1 Then
            If Not objPermissionsTypes.MinDurationAllowedInSelfService Is Nothing And Not objPermissionsTypes.MinDurationAllowedInSelfService = -1 Then

                Dim intFromTime As Integer = (OldFromTime.Hour * 60) + (OldFromTime.Minute)
                Dim intToTime As Integer = (OldToTime.Hour * 60) + (OldToTime.Minute)
                Dim DurationSelfService As Integer
                Dim DivisionRemaining As Decimal


                If (intToTime - intFromTime) > 0 Then
                    Dim timediff As Integer = 0
                    timediff = intToTime - intFromTime
                    Dim minDurationdiff As Integer = 0
                    Dim modeduration As Integer
                    modeduration = (intToTime - intFromTime) Mod CInt(objPermissionsTypes.MinDurationAllowedInSelfService)
                    If modeduration <> 0 Then
                        DivisionRemaining = (intToTime - intFromTime) / CInt(objPermissionsTypes.MinDurationAllowedInSelfService)
                        DivisionRemaining = Math.Ceiling(DivisionRemaining)
                        minDurationdiff = DivisionRemaining * CInt(objPermissionsTypes.MinDurationAllowedInSelfService)
                        'minDurationdiff = CInt(objPermissionsTypes.MinDurationAllowedInSelfService) - timediff
                        'DurationSelfService = OldToTime.Minute + minDurationdiff
                        If (intToTime - intFromTime) > CInt(objPermissionsTypes.MinDurationAllowedInSelfService) Then
                            Dim policydiff As Integer = minDurationdiff - (intToTime - intFromTime)
                            DurationSelfService = OldToTime.Minute + policydiff
                        Else
                            DurationSelfService = OldToTime.Minute
                        End If
                    Else
                        DurationSelfService = OldToTime.Minute
                    End If
                Else
                    DurationSelfService = OldToTime.Minute
                End If

                'MinDuration = Math.Floor(OldToTime.Minute / CInt(objPermissionsTypes.MinDurationAllowedInSelfService))
                'DurationSelfService = MinDuration * CInt(objPermissionsTypes.MinDurationAllowedInSelfService)
                'Dim modeduration As Integer
                'modeduration = OldToTime.Minute Mod CInt(objPermissionsTypes.MinDurationAllowedInSelfService)
                'If modeduration > 0 Then
                '    DurationSelfService = DurationSelfService + CInt(objPermissionsTypes.MinDurationAllowedInSelfService)
                'End If


                If Not BreakStart = 0 Then
                    Dim intPermStart As Integer = (CInt(PermStart.ToString.Split(":")(0)) * 60) + CInt(PermStart.ToString.Split(":")(1))
                    Dim intPermEnd As Integer = (CInt(PermEnd.ToString.Split(":")(0)) * 60) + CInt(PermEnd.ToString.Split(":")(1))
                    Dim IntBrkDuration As Integer = BreakEnd - BreakStart
                    Dim rslDatePermStart As DateTime = Convert.ToDateTime(CtlCommon.GetFullTimeString(intPermStart + IntBrkDuration))

                    If intPermEnd < BreakEnd Then
                        RadTPDevidefromTime.SelectedDate = Convert.ToDateTime(PermStart)
                        RadTPDevidetoTime.SelectedDate = Convert.ToDateTime(PermEnd)
                        setTimeDifference()

                    ElseIf intPermStart < BreakStart Then
                        RadTPfromTime.SelectedDate = rslDatePermStart
                        setTimeDifference()
                        RadTPfromTime.SelectedDate = Convert.ToDateTime(PermStart)
                    Else
                        Dim formatToTime As Integer = (OldToTime.Hour * 60) + (DurationSelfService)
                        RadTPtoTime.SelectedDate = CtlCommon.ConvertintToTime(formatToTime)
                        RadTPDevidetoTime.SelectedDate = CtlCommon.ConvertintToTime(formatToTime)
                        setTimeDifference()
                    End If
                Else
                    Dim formatToTime As Integer = (OldToTime.Hour * 60) + (DurationSelfService)
                    RadTPtoTime.SelectedDate = CtlCommon.ConvertintToTime(formatToTime)
                    RadTPDevidetoTime.SelectedDate = CtlCommon.ConvertintToTime(formatToTime)
                    setTimeDifference()
                End If
                lblMinDurationPolicy.Visible = False
            Else
                If Not objPermissionsTypes.MinDuration = Nothing Then
                    Dim MinDuration As Integer = objPermissionsTypes.MinDuration
                    Dim timediff As Integer = 0
                    Dim intFromTime As Integer = (OldFromTime.Hour * 60) + (OldFromTime.Minute)
                    Dim intToTime As Integer = (OldToTime.Hour * 60) + (OldToTime.Minute)
                    timediff = intToTime - intFromTime

                    If timediff < MinDuration Then
                        RadTPtoTime.SelectedDate = CtlCommon.ConvertintToTime(intFromTime + MinDuration)
                        RadTPDevidetoTime.SelectedDate = CtlCommon.ConvertintToTime(intFromTime + MinDuration)
                        setTimeDifference()
                        If Lang = CtlCommon.Lang.AR Then
                            lblMinDurationPolicy.Text = "الحد الادنى لنوع المغادرة الذي قمت باختياره هو " & MinDuration & " دقيقة"
                        Else
                            lblMinDurationPolicy.Text = "Minimum Duration of The Selected Permission Type Is " & MinDuration & " Minute(s)"
                        End If
                        lblMinDurationPolicy.Visible = True
                    Else
                        lblMinDurationPolicy.Visible = False
                    End If
                End If

            End If
        Else
            RadTPtoTime.SelectedDate = OldToTime
            RadTPDevidetoTime.SelectedDate = OldToTime
            lblMinDurationPolicy.Visible = False
            dvTimeControls.Visible = True
            trFullyDay.Visible = False
        End If

        'GetRemainingBalance()
    End Sub

    Protected Sub lnbDevideTwoPermission_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnbDevideTwoPermission.Click
        pnlDevideTwoPermission.Visible = True
        IsDevidedPermission = True
        If Lang = CtlCommon.Lang.EN Then
            '  dvPermissionGeneralGuide.Style("margin-right") = "-133px"
        ElseIf Lang = CtlCommon.Lang.AR Then
            'dvPermissionGeneralGuide.Style("margin-left") = "-133px"
        End If

        'dvContainPermission.Style("margin-top") = "-190px"
        FillDividePermissionTypes()
        FillDevideObjectData()
        setDevideTimeDifference()
        RadTPtoTime.Enabled = True
    End Sub

    Protected Sub RadCmpDevidePermissions_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmpDevidePermissions.SelectedIndexChanged
        objPermissionsTypes = New PermissionsTypes()
        With objPermissionsTypes
            .PermId = Convert.ToInt32(RadCmpDevidePermissions.SelectedValue)
            .GetByPK()
            DevideApprovedRequired = .ApprovalRequired
            If Lang = CtlCommon.Lang.EN Then
                dvDevideGeneralGuide.InnerHtml = .GeneralGuide
            ElseIf Lang = CtlCommon.Lang.AR Then
                dvDevideGeneralGuide.InnerHtml = .GeneralGuideAr
            End If

            If .HasPermissionTimeControls = True Then
                dvDivideTimeControl.Visible = True
            Else
                dvDivideTimeControl.Visible = False
            End If

            If .HasFullDayPermission = True Then
                trDevideFullyDay.Visible = True
            Else
                trDevideFullyDay.Visible = False
            End If

        End With

        If objPermissionsTypes.ShowRemainingBalance = False Then

            lblDevideRemainingBalance.Visible = False
            lblDevideRemainingBalanceHour.Visible = False
            lblDevideRemainingBalanceValue.Visible = False
        Else

            lblDevideRemainingBalance.Visible = True
            lblDevideRemainingBalanceHour.Visible = True
            lblDevideRemainingBalanceValue.Visible = True

        End If

        If RadCmpDevidePermissions.SelectedValue = -1 Then

            trDevideFullyDay.Visible = False
            dvDivideTimeControl.Visible = True
        End If
        objEmp_Permissions = New Emp_Permissions()
        lblDevideRemainingBalanceValue.Text = objEmp_Permissions.GetPermissionRemainingBalance(RadCmpDevidePermissions.SelectedValue, SessionVariables.LoginUser.FK_EmployeeId,
                                                                                         radDevideBtnOneDay.Checked, dtpDevidePermissionDate.SelectedDate.Value, dtpDevideStartDatePerm.SelectedDate.Value, dtpDevideEndDatePerm.SelectedDate.Value)
        'GetDevideRemainingBalance()
    End Sub

    '::End Permission Request

    '::Leave Request

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        dvGeneralGuide.Visible = False
        mvEmpViolations.SetActiveView(viewEmpViolationsList)
        FillHighCharts()
        FillGridView()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim EmpLeaveTotalBalance As Double = 0
        Dim OffAndHolidayDays As Integer = 0
        Dim ErrMessage As String = String.Empty
        Dim ErrorMessage As String = String.Empty
        objEmp_Leaves = New Emp_Leaves
        objEmp_LeavesRequest = New Emp_LeavesRequest
        objLeavesTypes = New LeavesTypes

        Try
            objLeavesTypes.LeaveId = ddlLeaveType.SelectedValue
            objLeavesTypes.GetByPK()

            If FileUpload1.HasFile = False Then
                If objLeavesTypes.AttachmentIsMandatory Then
                    If Lang = CtlCommon.Lang.AR Then
                        CtlCommon.ShowMessage(Me.Page, "يرجى ارفاق ملف!", "info")
                    Else
                        CtlCommon.ShowMessage(Me.Page, "Please Attach File!", "info")
                    End If
                    Exit Sub
                End If
            End If

            If txtLeaveRemarks.Text.Trim() = "" Then
                If objLeavesTypes.RemarksIsMandatory = True Then
                    If Lang = CtlCommon.Lang.AR Then
                        CtlCommon.ShowMessage(Me.Page, "يرجى ادخال الملاحظات", "info")
                    Else
                        CtlCommon.ShowMessage(Me.Page, "Please Enter Remarks", "info")
                    End If
                    Exit Sub
                End If
            End If

            If Not objLeavesTypes.AllowedAfterDays = Nothing Then
                If dtpFromDate.SelectedDate > DateTime.Today.AddDays(objLeavesTypes.AllowedAfterDays) Then
                    If Lang = CtlCommon.Lang.AR Then
                        ErrMessage = "نوع المغادرة التي قمت باختيارها لايمكن طلبها لتاريخ بعد : " & objLeavesTypes.AllowedAfterDays & " أيام"
                        CtlCommon.ShowMessage(Me.Page, ErrMessage, "info")
                    Else
                        ErrMessage = "The Selected Permission Type Not Allowed After : " & objLeavesTypes.AllowedAfterDays & " Day(s)"
                        CtlCommon.ShowMessage(Me.Page, ErrMessage, "info")
                    End If

                    Exit Sub
                End If
            End If

            If Not objLeavesTypes.AllowedBeforeDays = Nothing Then
                If dtpFromDate.SelectedDate < Date.Today.AddDays(objLeavesTypes.AllowedBeforeDays) Then
                    If Lang = CtlCommon.Lang.AR Then
                        ErrMessage = "نوع المغادرة التي قمت باختيارها لايمكن طلبها لتاريخ قبل : " & objLeavesTypes.AllowedBeforeDays + " أيام "
                        CtlCommon.ShowMessage(Me.Page, ErrMessage, "info")
                    Else
                        ErrMessage = "The Selected Permission Type Not Allowed Before : " & objLeavesTypes.AllowedBeforeDays & " Day(s)"
                        CtlCommon.ShowMessage(Me.Page, ErrMessage, "info")
                    End If

                    Exit Sub
                End If
            End If

            If (objEmp_LeavesRequest.ValidateEmployeeLeaveRequest(objLeavesTypes, dtpFromDate.SelectedDate, dtpToDate.SelectedDate, LeaveRequestID, SessionVariables.LoginUser.FK_EmployeeId,
                                                    ddlLeaveType.SelectedValue, ErrorMessage, OffAndHolidayDays, EmpLeaveTotalBalance) = False) Then
                If (ErrorMessage <> String.Empty) Then
                    CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
                    Return
                End If

            Else
                Dim err As Integer = -1
                Dim strMessage As String
                objEmp_Leaves = New Emp_Leaves

                Dim OffOrHolidayDaysCount As Integer
                OffOrHolidayDaysCount = 0

                If OffAndHolidayDays > 0 Then
                    OffOrHolidayDaysCount = ((dtpToDate.SelectedDate - dtpFromDate.SelectedDate).Value.Days + 1) - OffAndHolidayDays
                Else
                    OffOrHolidayDaysCount = (dtpToDate.SelectedDate - dtpFromDate.SelectedDate).Value.Days + 1
                End If

                With objEmp_LeavesRequest
                    .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
                    .FK_LeaveTypeId = ddlLeaveType.SelectedValue
                    .FromDate = dtpFromDate.DbSelectedDate
                    .ToDate = dtpToDate.DbSelectedDate
                    .RequestDate = dtpRequestDate.DbSelectedDate
                    .Remarks = txtLeaveRemarks.Text
                    .IsHalfDay = False ' chkHalfDay.Checked
                    .CREATED_BY = SessionVariables.LoginUser.ID
                    .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                    .CREATED_DATE = DateTime.Now
                    .LAST_UPDATE_DATE = DateTime.Now
                    .Days = OffOrHolidayDaysCount
                    If ApprovedRequired Then
                        .FK_StatusId = Convert.ToInt32(CtlCommon.RequestStatusEnum.Pending)
                    Else
                        .FK_StatusId = Convert.ToInt32(CtlCommon.RequestStatusEnum.AutomaticApproved)
                    End If

                    If FileUpload1.HasFile Then
                        .AttachedFile = Path.GetExtension(FileUpload1.PostedFile.FileName)
                    End If
                    If LeaveRequestID = 0 Then
                        err = .Add()
                        strMessage = ResourceManager.GetString("SaveSuccessfully", CultureInfo)
                    Else
                        .AttachedFile = FileExtension
                        .LeaveId = LeaveRequestID
                        err = .Update()

                        strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)
                    End If

                    If err = 0 Then
                        If FileUpload1.HasFile Then
                            .AttachedFile = Path.GetExtension(FileUpload1.PostedFile.FileName)
                            Dim extention As String = Path.GetExtension(FileUpload1.PostedFile.FileName)
                            Dim fileName As String = objEmp_LeavesRequest.LeaveId.ToString()
                            Dim fPath As String = String.Empty
                            fPath = Server.MapPath("..\LeaveRequestFiles\\" + fileName + extention)
                            FileUpload1.PostedFile.SaveAs(fPath)
                        Else
                            .AttachedFile = String.Empty
                        End If
                    End If

                    If err = 0 Then
                        CtlCommon.ShowMessage(Me.Page, strMessage, "success")
                        ClearAll()
                        FillGridView()
                        'ScriptManager.RegisterStartupScript(Page, Page.GetType, Guid.NewGuid.ToString, "RefreshPage()", True)
                    Else
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
                    End If
                End With

                If err = 0 Then
                    ClearAll()
                    FillGridView()
                    mvEmpViolations.SetActiveView(viewEmpViolationsList)
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ClearAll()
        mvEmpViolations.SetActiveView(viewEmpViolationsList)
    End Sub

    Protected Sub ddlLeaveType_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlLeaveType.SelectedIndexChanged

        objLeavesTypes = New LeavesTypes
        With objLeavesTypes
            .LeaveId = Convert.ToInt32(ddlLeaveType.SelectedValue)
            .GetByPK()
            If Lang = CtlCommon.Lang.EN Then
                dvGeneralGuide.InnerHtml = .GeneralGuide
            ElseIf Lang = CtlCommon.Lang.AR Then
                dvGeneralGuide.InnerHtml = .GeneralGuideAr
            End If
            ApprovedRequired = .ApprovalRequired
        End With

    End Sub

    '::End Leave Request

    Protected Sub repAnnouncement_ItemDataBound(ByVal source As Object, ByVal e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim DI As Object = e.Item.DataItem
            Dim CurrentDate As DateTime = DataBinder.Eval(DI, "AnnouncementDate")
            Dim myMonth As String = CurrentDate.Month.ToString()

            Dim monthNo As Integer = Convert.ToInt32(myMonth)
            Dim lblMonth As Label = CType(e.Item.FindControl("lblMonth"), Label)
            Dim fullMonth As String = System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.GetMonthName(monthNo).ToString()
            lblMonth.Text = fullMonth.Substring(0, 3)
            Dim lblDay As Label = CType(e.Item.FindControl("lblDate"), Label)
            lblDay.Text = CurrentDate.Day.ToString()
        End If
    End Sub

#End Region

#Region "Page Methods"

    Private Sub FillGridView()

        objAppSettings = New APP_Settings
        objAppSettings.GetByPK()
        ShowLeaveLnk_ViolationCorrection = objAppSettings.ShowLeaveLnk_ViolationCorrection
        ShowPermissionLnk_ViolationCorrection = objAppSettings.ShowPermissionLnk_ViolationCorrection

        objEmployeeViolations = New EmployeeViolations
        With objEmployeeViolations
            .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            .FromDate = dteFromDate.DbSelectedDate
            .ToDate = dteToDate.DbSelectedDate
            dtCurrent = .GetEmployeeViolations()
        End With
        If Not dtCurrent Is Nothing Then
            If dtCurrent.Rows.Count > 0 Then
                For Each item As DataRow In dtCurrent.Rows
                    Dim type As String = String.Empty
                    Dim delay As String = String.Empty
                    Dim OutTime As String = String.Empty
                    Dim earlyOut As String = String.Empty
                    Dim strStatus As String = String.Empty
                    Dim intStatus As Integer = 0

                    If (Not IsDBNull(item("Status"))) AndAlso (item("Status") = "A") Then
                        type = ResourceManager.GetString("Absent", CultureInfo)
                    ElseIf item("Description").Contains("Delay") Then
                        type = ResourceManager.GetString("Delay", CultureInfo)
                        Dim strDelay() As String = item("Description").Split(":")
                        delay = strDelay(1).ToString() + ":" + strDelay(2).ToString()
                    ElseIf item("Description").Contains("Early Out") Then
                        type = ResourceManager.GetString("EarlyOut", CultureInfo)
                        Dim strEarlyOut() As String = item("Description").Split(":")
                        earlyOut = strEarlyOut(1).ToString() + ":" + strEarlyOut(2).ToString()
                    ElseIf item("Description").Contains("Out Time") Then
                        type = ResourceManager.GetString("OutTime", CultureInfo)
                        Dim strOutTime() As String = item("Description").Split(":")
                        OutTime = strOutTime(1).ToString() + ":" + strOutTime(2).ToString()
                    End If
                Next

                SummaryPage1.FromDate = dteFromDate.DbSelectedDate
                SummaryPage1.ToDate = dteToDate.DbSelectedDate
                SummaryPage1.FillInfo()

            End If
        End If

        dgrdEmpViolations.DataSource = dtCurrent
        dgrdEmpViolations.DataBind()

    End Sub

    '::Permission Request

    Private Sub FillObjectData()
        objAPP_Settings = New APP_Settings
        objAPP_Settings.GetByPK()
        ' Set data into object for Add / Update
        With objEmp_PermissionsRequest
            ' Get Values from the combo boxes
            .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            .FK_PermId = RadCmpPermissions.SelectedValue
            ' Get values from the check boxes
            .IsDividable = chckIsDividable.Checked
            .IsFlexible = chckIsFlexible.Checked
            .IsFullDay = chckFullDay.Checked
            .IsSpecificDays = chckSpecifiedDays.Checked
            .Remark = txtRemarks.Text
            ' Get values from rad controls
            .PermDate = dtpPermissionDate.SelectedDate
            If PermissionId = 0 Then
                If fuAttachFile.HasFile Then
                    .AttachedFile = Path.GetExtension(fuAttachFile.PostedFile.FileName)
                Else
                    .AttachedFile = String.Empty
                End If

            Else
                .AttachedFile = FileExtension
            End If


            If (radBtnOneDay.Checked) Then
                Dim objFromtime As DateTime
                Dim objToTime As DateTime
                objFromtime = RadTPfromTime.SelectedDate
                objToTime = RadTPtoTime.SelectedDate

                Dim ts As TimeSpan
                ts = New TimeSpan(objFromtime.TimeOfDay.Hours, objFromtime.TimeOfDay.Minutes, objFromtime.TimeOfDay.Seconds)
                objFromtime = dtpPermissionDate.SelectedDate + ts

                ts = New TimeSpan(objToTime.TimeOfDay.Hours, objToTime.TimeOfDay.Minutes, objToTime.TimeOfDay.Seconds)
                objToTime = dtpPermissionDate.SelectedDate + ts

                .FromTime = objFromtime
                .ToTime = objToTime
            Else
                .FromTime = RadTPfromTime.SelectedDate
                .ToTime = RadTPtoTime.SelectedDate
            End If

            PermissionDaysCount = 0
            If BreakStart = 0 Then
                If OffAndHolidayDays > 0 Then
                    If radBtnOneDay.Checked Then
                        PermissionDaysCount = 0
                    Else
                        PermissionDaysCount = (((dtpEndDatePerm.SelectedDate - dtpStartDatePerm.SelectedDate).Value.Days + 1) - OffAndHolidayDays) * ((RadTPtoTime.SelectedDate - RadTPfromTime.SelectedDate).Value.TotalMinutes / objAPP_Settings.DaysMinutes)
                    End If

                Else
                    If radBtnOneDay.Checked Then
                        PermissionDaysCount = ((RadTPtoTime.SelectedDate - RadTPfromTime.SelectedDate).Value.TotalMinutes / objAPP_Settings.DaysMinutes)
                    Else
                        PermissionDaysCount = (dtpEndDatePerm.SelectedDate - dtpStartDatePerm.SelectedDate).Value.Days + 1 * ((RadTPtoTime.SelectedDate - RadTPfromTime.SelectedDate).Value.TotalMinutes / objAPP_Settings.DaysMinutes)
                    End If

                End If
            Else
                Dim intPermStart As Integer = (CInt(PermStart.ToString.Split(":")(0)) * 60) + CInt(PermStart.ToString.Split(":")(1))
                Dim intPermEnd As Integer = (CInt(PermEnd.ToString.Split(":")(0)) * 60) + CInt(PermEnd.ToString.Split(":")(1))
                Dim IntBrkDuration As Integer = BreakEnd - BreakStart
                Dim rslDatePermStart As DateTime = Convert.ToDateTime(CtlCommon.GetFullTimeString(intPermStart + IntBrkDuration))

                If intPermEnd < BreakEnd Then
                    RadTPDevidefromTime.SelectedDate = Convert.ToDateTime(PermStart)
                    RadTPDevidetoTime.SelectedDate = Convert.ToDateTime(PermEnd)
                    setTimeDifference()

                ElseIf intPermStart < BreakStart Then
                    RadTPfromTime.SelectedDate = rslDatePermStart
                End If
                If OffAndHolidayDays > 0 Then
                    If radBtnOneDay.Checked Then
                        PermissionDaysCount = 0
                    Else
                        PermissionDaysCount = (((dtpEndDatePerm.SelectedDate - dtpStartDatePerm.SelectedDate).Value.Days + 1) - OffAndHolidayDays) * ((RadTPtoTime.SelectedDate - RadTPfromTime.SelectedDate).Value.TotalMinutes / objAPP_Settings.DaysMinutes)
                    End If

                Else
                    If radBtnOneDay.Checked Then
                        PermissionDaysCount = ((RadTPtoTime.SelectedDate - RadTPfromTime.SelectedDate).Value.TotalMinutes / objAPP_Settings.DaysMinutes)
                    Else
                        PermissionDaysCount = (dtpEndDatePerm.SelectedDate - dtpStartDatePerm.SelectedDate).Value.Days + 1 * ((RadTPtoTime.SelectedDate - RadTPfromTime.SelectedDate).Value.TotalMinutes / objAPP_Settings.DaysMinutes)
                    End If

                End If

            End If


            ' Set systematic values
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            .CREATED_BY = SessionVariables.LoginUser.ID
            .CREATED_DATE = Today
            .LAST_UPDATE_DATE = Today
            .Days = PermissionDaysCount

            If IS_Period() Then
                ' Periodically leave
                .IsForPeriod = True

                .PermDate = dtpStartDatePerm.SelectedDate
                .PermEndDate = IIf(CheckDate(dtpEndDatePerm.SelectedDate) = Nothing,
                                   DateTime.MinValue, dtpEndDatePerm.SelectedDate)

            Else
                ' Non Periodically leave
                .IsForPeriod = False

                .PermDate = dtpPermissionDate.SelectedDate
                .PermEndDate = DateTime.MinValue
            End If
        End With
    End Sub

    Private Sub FillDevideObjectData()
        objAPP_Settings = New APP_Settings
        objAPP_Settings.GetByPK()
        ' Set data into object for Add / Update
        objEmp_PermissionsRequest = New Emp_PermissionsRequest
        With objEmp_PermissionsRequest
            ' Get Values from the combo boxes
            .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            .FK_PermId = RadCmpDevidePermissions.SelectedValue
            ' Get values from the check boxes
            .IsDividable = chckDevideIsDividable.Checked
            .IsFlexible = chckDevideIsFlexible.Checked
            .IsFullDay = chckDevideFullDay.Checked
            .IsSpecificDays = chckDevideSpecifiedDays.Checked
            .Remark = txtDevideRemarks.Text()
            ' Get values from rad controls
            .PermDate = dtpDevidePermissionDate.SelectedDate
            If PermissionId = 0 Then
                If fuDevideAttachFile.HasFile Then
                    .AttachedFile = Path.GetExtension(fuDevideAttachFile.PostedFile.FileName)
                Else
                    .AttachedFile = String.Empty
                End If

            Else
                .AttachedFile = FileExtension
            End If


            If (radDevideBtnOneDay.Checked) Then
                Dim objFromtime As DateTime
                Dim objToTime As DateTime
                objFromtime = RadTPDevidefromTime.SelectedDate
                objToTime = RadTPDevidetoTime.SelectedDate

                Dim ts As TimeSpan
                ts = New TimeSpan(objFromtime.TimeOfDay.Hours, objFromtime.TimeOfDay.Minutes, objFromtime.TimeOfDay.Seconds)
                objFromtime = dtpDevidePermissionDate.SelectedDate + ts

                ts = New TimeSpan(objToTime.TimeOfDay.Hours, objToTime.TimeOfDay.Minutes, objToTime.TimeOfDay.Seconds)
                objToTime = dtpDevidePermissionDate.SelectedDate + ts

                .FromTime = objFromtime
                .ToTime = objToTime
            Else
                .FromTime = RadTPDevidefromTime.SelectedDate
                .ToTime = RadTPDevidetoTime.SelectedDate
            End If

            PermissionDaysCount = 0

            If OffAndHolidayDays > 0 Then
                If radDevideBtnOneDay.Checked Then
                    PermissionDaysCount = 0
                Else
                    PermissionDaysCount = (((dtpDevideEndDatePerm.SelectedDate - dtpDevideStartDatePerm.SelectedDate).Value.Days + 1) - OffAndHolidayDays) * ((RadTPDevidetoTime.SelectedDate - RadTPDevidefromTime.SelectedDate).Value.TotalMinutes / objAPP_Settings.DaysMinutes)
                End If
            Else
                If radBtnOneDay.Checked Then
                    PermissionDaysCount = ((RadTPDevidetoTime.SelectedDate - RadTPDevidefromTime.SelectedDate).Value.TotalMinutes / objAPP_Settings.DaysMinutes)
                Else
                    PermissionDaysCount = (dtpDevideEndDatePerm.SelectedDate - dtpDevideStartDatePerm.SelectedDate).Value.Days + 1 * ((RadTPDevidetoTime.SelectedDate - RadTPDevidefromTime.SelectedDate).Value.TotalMinutes / objAPP_Settings.DaysMinutes)
                End If
            End If
            ' Set systematic values
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            .CREATED_BY = SessionVariables.LoginUser.ID
            .CREATED_DATE = Today
            .LAST_UPDATE_DATE = Today
            .Days = PermissionDaysCount

            If IS_DevidePeriod() Then
                ' Periodically leave
                .IsForPeriod = True

                .PermDate = dtpDevideStartDatePerm.SelectedDate
                .PermEndDate = IIf(CheckDate(dtpDevideEndDatePerm.SelectedDate) = Nothing,
                                   DateTime.MinValue, dtpDevideEndDatePerm.SelectedDate)

            Else
                ' Non Periodically leave
                .IsForPeriod = False

                .PermDate = dtpDevidePermissionDate.SelectedDate
                .PermEndDate = DateTime.MinValue
            End If
        End With
    End Sub

    Private Function IS_Period() As Boolean
        Return radBtnPeriod.Checked
    End Function

    Private Function IS_DevidePeriod() As Boolean
        Return radDevideBtnPeriod.Checked
    End Function

    Private Function CheckDate(ByVal myDate As Object) As Object
        ' input : DateTime object
        ' Output : Nothing or a DateTime greater tha date time 
        ' minimum value
        ' Processing : Check the input if a valid date time to be used
        ' valid means greater than the minimum value and in valid format
        If IsDate(myDate) Then
            myDate =
                IIf(myDate > DateTime.MinValue, myDate, Nothing)
            Return (myDate)
        Else
            Return Nothing
        End If
    End Function


    Private Sub FillPermissionTypes()
        Dim dt As DataTable = Nothing
        Dim dt2 As DataTable = Nothing
        Dim dtPermEmployeeType As DataTable = Nothing
        Dim FK_ManagerId As Integer
        objPermissionsTypes = New PermissionsTypes()
        objEmployee_Manager = New Employee_Manager
        objEmployee = New Employee
        objEmployee_Manager.FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
        objEmployee_Manager.FromDate = Date.Today
        objEmployee_Manager.ToDate = Date.Today
        dt2 = objEmployee_Manager.GetManagerInfoByManagerId()
        If dt2.Rows.Count > 0 Then
            FK_ManagerId = Convert.ToInt32(dt2.Rows(0)("FK_ManagerId")).ToString
        Else
            FK_ManagerId = 0
        End If

        '********For Fill Permissions Types For Employee Groups*********************

        objEmployee = New Employee
        Dim EmployeeGroupId As Integer
        Dim FK_LogicalGroup As Integer
        dtPermEmployeeType = New DataTable
        With objEmployee
            If Not SessionVariables.LoginUser Is Nothing Then
                .EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
                .GetByPK()
                FK_LogicalGroup = .FK_LogicalGroup
                'dtPermEmployeeType = dt.Select().CopyToDataTable()



                'For I As Integer = 0 To dt.Rows.Count - 1
                '    If dt.Rows(I)("AllowForSpecificEmployeeType") = True Then
                '        EmployeeGroupId = dt.Rows(I)("FK_EmployeeTypeId")
                '        If FK_LogicalGroup <> EmployeeGroupId Then
                '            'dtPermEmployeeType = dt.Select("FK_EmployeeTypeId =" & EmployeeGroupId).CopyToDataTable()
                '            'dtPermEmployeeType = dt.Rows.RemoveAt(dt.Rows.IndexOf(I))


                '            Dim result As DataRow() = Nothing
                '            result = dtPermEmployeeType.Select("FK_EmployeeTypeId =" & EmployeeGroupId)
                '            For Each row As DataRow In result
                '                dtPermEmployeeType.Rows.Remove(row)
                '            Next
                '        End If

                '    End If

                'Next
                'If FK_LogicalGroup <> EmployeeGroupId And dtPermEmployeeType.Rows.Count > 0 Then
                '    dt = dtPermEmployeeType
                'End If

            End If
        End With




        If SessionVariables.LoginUser.FK_EmployeeId = FK_ManagerId Then
            objEmployee.EmployeeId = FK_ManagerId
            objEmployee.GetByPK()
            objPermissionsTypes.FK_CompanyId = objEmployee.FK_CompanyId
            objPermissionsTypes.FK_EntityId = objEmployee.FK_EntityId
            objPermissionsTypes.FK_EmployeeTypeId = FK_LogicalGroup
            dt = objPermissionsTypes.GetAllowed_ForManagers()
        Else
            objEmployee.EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            objEmployee.GetByPK()
            objPermissionsTypes.FK_CompanyId = objEmployee.FK_CompanyId
            objPermissionsTypes.FK_EntityId = objEmployee.FK_EntityId
            objPermissionsTypes.FK_EmployeeTypeId = FK_LogicalGroup
            dt = objPermissionsTypes.GetAllowed_ForSelfService()
        End If

        '********For Fill Permissions Types For Employee Groups*********************
        'If dt.Rows.Count > 0 Then
        '    objEmployee = New Employee
        '    Dim EmployeeGroupId As Integer
        '    Dim FK_LogicalGroup As Integer
        '    dtPermEmployeeType = New DataTable
        '    With objEmployee
        '        If Not SessionVariables.LoginUser Is Nothing Then
        '            .EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
        '            .GetByPK()
        '            FK_LogicalGroup = .FK_LogicalGroup
        '            dtPermEmployeeType = dt.Select().CopyToDataTable()



        'For I As Integer = 0 To dt.Rows.Count - 1
        '    If dt.Rows(I)("AllowForSpecificEmployeeType") = True Then
        '        EmployeeGroupId = dt.Rows(I)("FK_EmployeeTypeId")
        '        If FK_LogicalGroup <> EmployeeGroupId Then
        '            'dtPermEmployeeType = dt.Select("FK_EmployeeTypeId =" & EmployeeGroupId).CopyToDataTable()
        '            'dtPermEmployeeType = dt.Rows.RemoveAt(dt.Rows.IndexOf(I))


        '            Dim result As DataRow() = Nothing
        '            result = dtPermEmployeeType.Select("FK_EmployeeTypeId =" & EmployeeGroupId)
        '            For Each row As DataRow In result
        '                dtPermEmployeeType.Rows.Remove(row)
        '            Next
        '        End If

        '    End If

        'Next
        'If FK_LogicalGroup <> EmployeeGroupId And dtPermEmployeeType.Rows.Count > 0 Then
        '    dt = dtPermEmployeeType
        'End If

        'End If
        '    End With
        'End If
        '********End******************End******************




        'dt = objPermissionsTypes.GetAll
        If dt IsNot Nothing Then
            CtlCommon.FillTelerikDropDownList(RadCmpPermissions, dt, Lang)
        End If
    End Sub

    'Private Sub FillPermissionTypes()
    '    Dim dtPermEmployeeType As DataTable = Nothing
    '    Dim FK_LogicalGroup As Integer
    '    Dim objEmployee As Employee
    '    Dim dt As DataTable = Nothing
    '    Dim dt2 As DataTable = Nothing
    '    Dim FK_ManagerId As Integer
    '    objEmployee = New Employee
    '    objPermissionsTypes = New PermissionsTypes()
    '    objEmployee_Manager = New Employee_Manager
    '    objEmployee_Manager.FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
    '    objEmployee_Manager.FromDate = Date.Today
    '    objEmployee_Manager.ToDate = Date.Today
    '    FK_LogicalGroup = objEmployee.FK_LogicalGroup
    '    dt2 = objEmployee_Manager.GetManagerInfoByManagerId()
    '    If dt2.Rows.Count > 0 Then
    '        FK_ManagerId = Convert.ToInt32(dt2.Rows(0)("FK_ManagerId")).ToString
    '    Else
    '        FK_ManagerId = 0
    '    End If

    '    If SessionVariables.LoginUser.FK_EmployeeId = FK_ManagerId Then
    '        objEmployee.EmployeeId = FK_ManagerId
    '        objEmployee.GetByPK()
    '        objPermissionsTypes.FK_CompanyId = objEmployee.FK_CompanyId
    '        objPermissionsTypes.FK_EntityId = objEmployee.FK_EntityId
    '        objPermissionsTypes.FK_EmployeeTypeId = 3078
    '        dt = objPermissionsTypes.GetAllowed_ForManagers()
    '    Else
    '        objEmployee.EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
    '        objEmployee.GetByPK()
    '        objPermissionsTypes.FK_CompanyId = objEmployee.FK_CompanyId
    '        objPermissionsTypes.FK_EntityId = objEmployee.FK_EntityId
    '        dt = objPermissionsTypes.GetAllowed_ForSelfService()
    '    End If


    '    '********For Fill Permissions Types For Employee Groups*********************
    '    'objEmployee = New Employee
    '    'Dim EmployeeGroupId As Integer
    '    'Dim FK_LogicalGroup As Integer
    '    'dtPermEmployeeType = New DataTable
    '    'With objEmployee
    '    '    If Not SessionVariables.LoginUser Is Nothing Then
    '    '        .EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
    '    '        .GetByPK()
    '    '        FK_LogicalGroup = .FK_LogicalGroup
    '    '        If Not dt Is Nothing Then
    '    '            If dt.Rows.Count > 0 Then
    '    '                dtPermEmployeeType = dt.Select().CopyToDataTable()

    '    '                For I As Integer = 0 To dt.Rows.Count - 1
    '    '                    If dt.Rows(I)("AllowForSpecificEmployeeType") = True Then
    '    '                        EmployeeGroupId = dt.Rows(I)("FK_EmployeeTypeId")
    '    '                        If FK_LogicalGroup <> EmployeeGroupId Then
    '    '                            'dtPermEmployeeType = dt.Select("FK_EmployeeTypeId =" & EmployeeGroupId).CopyToDataTable()
    '    '                            'dtPermEmployeeType = dt.Rows.RemoveAt(dt.Rows.IndexOf(I))


    '    '                            Dim result As DataRow() = Nothing
    '    '                            result = dtPermEmployeeType.Select("FK_EmployeeTypeId =" & EmployeeGroupId)
    '    '                            For Each row As DataRow In result
    '    '                                dtPermEmployeeType.Rows.Remove(row)
    '    '                            Next
    '    '                        End If

    '    '                    End If

    '    '                Next
    '    '                If FK_LogicalGroup <> EmployeeGroupId And dtPermEmployeeType.Rows.Count > 0 Then
    '    '                    dt = dtPermEmployeeType
    '    '                End If
    '    '            End If
    '    '        End If


    '    '    End If
    '    'End With
    '    '********End******************End******************


    '    'dt = objPermissionsTypes.GetAll
    '    If dt IsNot Nothing Then
    '        CtlCommon.FillTelerikDropDownList(RadCmpPermissions, dt, Lang)
    '    End If
    'End Sub

    Private Sub FillDividePermissionTypes()
        Dim dt As DataTable = Nothing
        Dim dt2 As DataTable = Nothing
        Dim FK_ManagerId As Integer
        objPermissionsTypes = New PermissionsTypes()
        objEmployee_Manager = New Employee_Manager
        objEmployee_Manager.FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
        objEmployee_Manager.FromDate = Date.Today
        objEmployee_Manager.ToDate = Date.Today
        dt2 = objEmployee_Manager.GetManagerInfoByManagerId()
        If dt2.Rows.Count > 0 Then
            FK_ManagerId = Convert.ToInt32(dt2.Rows(0)("FK_ManagerId")).ToString
        Else
            FK_ManagerId = 0
        End If

        If SessionVariables.LoginUser.FK_EmployeeId = FK_ManagerId Then
            dt = objPermissionsTypes.GetAllowed_ForManagers()
        Else
            dt = objPermissionsTypes.GetAllowed_ForSelfService()
        End If

        'dt = objPermissionsTypes.GetAll
        If dt IsNot Nothing Then
            CtlCommon.FillTelerikDropDownList(RadCmpDevidePermissions, dt, Lang)
        End If
    End Sub

    Sub ClearPermissionAll()
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
        PermissionId = 0
        ' Reset mode to one day permission 
        pnlPeriodLeave.Visible = False
        PnlOneDayLeave.Visible = True
        radBtnOneDay.Checked = True
        radBtnPeriod.Checked = False
        radBtnOneDay.Checked = True
        btnSavePermission.Text = ResourceManager.GetString("btnSave", CultureInfo)
        IsDevidedPermission = False
        pnlDevideTwoPermission.Visible = False
        lblRemainingBalanceValue.Text = String.Empty
        'dvPermissionGeneralGuide.Visible = False
        'dvDevideGeneralGuide.Visible = False
        ' Remove sorting and sorting arrow 
    End Sub

    Private Sub SetRadDateTimePickerPeoperties()

        ' Set TimeView properties 
        Me.RadTPfromTime.TimeView.StartTime = New TimeSpan(0, 0, 0)
        Me.RadTPfromTime.TimeView.EndTime = New TimeSpan(23.5, 0, 0)
        Me.RadTPfromTime.TimeView.HeaderText = String.Empty
        Me.RadTPfromTime.TimeView.Interval = New TimeSpan(0, 30, 0)
        Me.RadTPfromTime.TimeView.Columns = 5

        Me.RadTPDevidefromTime.TimeView.StartTime = New TimeSpan(0, 0, 0)
        Me.RadTPDevidefromTime.TimeView.EndTime = New TimeSpan(23.5, 0, 0)
        Me.RadTPDevidefromTime.TimeView.HeaderText = String.Empty
        Me.RadTPDevidefromTime.TimeView.Interval = New TimeSpan(0, 30, 0)
        Me.RadTPDevidefromTime.TimeView.Columns = 5



        ' Set Popup window properties
        Me.RadTPfromTime.PopupDirection = TopRight
        Me.RadTPfromTime.ShowPopupOnFocus = True

        Me.RadTPDevidefromTime.PopupDirection = TopRight
        Me.RadTPDevidefromTime.ShowPopupOnFocus = True




        ' Set default value
        Me.RadTPfromTime.SelectedDate = Now

        Me.RadTPDevidefromTime.SelectedDate = Now

        ' Set TimeView properties 
        Me.RadTPtoTime.TimeView.StartTime = New TimeSpan(0, 0, 0)
        Me.RadTPtoTime.TimeView.EndTime = New TimeSpan(23.5, 0, 0)
        Me.RadTPtoTime.TimeView.Interval = New TimeSpan(0, 30, 0)
        Me.RadTPtoTime.TimeView.HeaderText = String.Empty
        Me.RadTPtoTime.TimeView.Columns = 5

        Me.RadTPDevidetoTime.TimeView.StartTime = New TimeSpan(0, 0, 0)
        Me.RadTPDevidetoTime.TimeView.EndTime = New TimeSpan(23.5, 0, 0)
        Me.RadTPDevidetoTime.TimeView.Interval = New TimeSpan(0, 30, 0)
        Me.RadTPDevidetoTime.TimeView.HeaderText = String.Empty
        Me.RadTPDevidetoTime.TimeView.Columns = 5

        ' Set Popup window properties
        Me.RadTPtoTime.PopupDirection = TopRight
        Me.RadTPtoTime.ShowPopupOnFocus = True

        Me.RadTPDevidetoTime.PopupDirection = TopRight
        Me.RadTPDevidetoTime.ShowPopupOnFocus = True

        ' Set default value
        Me.RadTPtoTime.SelectedDate = Now

        Me.RadTPDevidetoTime.SelectedDate = Now


        ' Set Data input properties
        Me.dtpStartDatePerm.SelectedDate = Today.Date

        Me.dtpDevideStartDatePerm.SelectedDate = Today.Date
        ' Set popup properties
        Me.dtpStartDatePerm.PopupDirection = TopRight
        Me.dtpStartDatePerm.ShowPopupOnFocus = True

        Me.dtpDevideStartDatePerm.PopupDirection = TopRight
        Me.dtpDevideStartDatePerm.ShowPopupOnFocus = True
        ' Set calender boundaries
        Me.dtpStartDatePerm.MinDate = New Date(2006, 1, 1)
        Me.dtpStartDatePerm.MaxDate = New Date(2040, 1, 1)

        Me.dtpDevideStartDatePerm.MinDate = New Date(2006, 1, 1)
        Me.dtpDevideStartDatePerm.MaxDate = New Date(2040, 1, 1)



        ' Set Data input properties
        Me.dtpEndDatePerm.SelectedDate = Today.Date

        Me.dtpDevideEndDatePerm.SelectedDate = Today.Date
        ' Set popup properties
        Me.dtpEndDatePerm.PopupDirection = TopRight
        Me.dtpEndDatePerm.ShowPopupOnFocus = True

        Me.dtpDevideEndDatePerm.PopupDirection = TopRight
        Me.dtpDevideEndDatePerm.ShowPopupOnFocus = True
        ' Set calender boundaries
        Me.dtpEndDatePerm.MinDate = New Date(2006, 1, 1)
        Me.dtpEndDatePerm.MaxDate = New Date(2040, 1, 1)

        Me.dtpDevideEndDatePerm.MinDate = New Date(2006, 1, 1)
        Me.dtpDevideEndDatePerm.MaxDate = New Date(2040, 1, 1)



        ' Set Data input properties
        Me.dtpPermissionDate.SelectedDate = Today.Date

        Me.dtpDevidePermissionDate.SelectedDate = Today.Date
        ' Set Popup properties
        Me.dtpPermissionDate.PopupDirection = TopRight
        Me.dtpPermissionDate.ShowPopupOnFocus = True

        Me.dtpDevidePermissionDate.PopupDirection = TopRight
        Me.dtpDevidePermissionDate.ShowPopupOnFocus = True
        ' Set calender boundaries
        Me.dtpPermissionDate.MinDate = New Date(2006, 1, 1)
        Me.dtpPermissionDate.MaxDate = New Date(2040, 1, 1)

        Me.dtpDevidePermissionDate.MinDate = New Date(2006, 1, 1)
        Me.dtpDevidePermissionDate.MaxDate = New Date(2040, 1, 1)

    End Sub

    Private Sub FillControls(ByVal dtEmpViolation As DataTable)
        objEmp_PermissionsRequest = New Emp_PermissionsRequest()
        objEmp_PermissionsRequest.PermissionId = PermissionId
        ' If the PermissionId is not a valid one , the below line will through 
        ' an exception

        If dtEmpViolation.Rows.Count > 1 Then
            IsDevidedPermission = True
            pnlDevideTwoPermission.Visible = True
        Else
            IsDevidedPermission = False
            pnlDevideTwoPermission.Visible = False
        End If

        objEmp_PermissionsRequest.GetByPK()
        With objEmp_PermissionsRequest
            ' Fill combo boxes
            'RadCmbEmployee.SelectedValue = .FK_EmployeeId
            If dtEmpViolation.Rows.Count > 1 Then
                RadCmpPermissions.SelectedValue = dtEmpViolation.Rows(0)("FK_PermId")

                If IsDevidedPermission Then
                    RadCmpDevidePermissions.SelectedValue = dtEmpViolation.Rows(1)("FK_PermId")
                End If


                RadTPfromTime.DbSelectedDate = Convert.ToDateTime(dtEmpViolation.Rows(0)("FromTime")).ToString("HH:mm")
                RadTPtoTime.DbSelectedDate = Convert.ToDateTime(dtEmpViolation.Rows(0)("ToTime")).ToString("HH:mm")

                If IsDevidedPermission Then
                    RadTPDevidefromTime.DbSelectedDate = Convert.ToDateTime(dtEmpViolation.Rows(1)("FromTime")).ToString("HH:mm")
                    RadTPDevidetoTime.DbSelectedDate = Convert.ToDateTime(dtEmpViolation.Rows(1)("ToTime")).ToString("HH:mm")
                End If

                txtRemarks.Text = dtEmpViolation.Rows(0)("Remark")
                txtDays.Text = dtEmpViolation.Rows(0)("Days")
                If Not IsDBNull(dtEmpViolation.Rows(0)("AttachedFile")) Then
                    FileExtension = dtEmpViolation.Rows(0)("AttachedFile")
                End If
            End If

            ' Fill checkBoxes
            chckIsDividable.Checked = .IsDividable
            chckIsFlexible.Checked = .IsFlexible
            chckSpecifiedDays.Checked = .IsSpecificDays
            ' Fill the time & date pickers
            'RadTPfromTime.SelectedDate = .FromTime
            'RadTPtoTime.SelectedDate = .ToTime


            fuAttachFile.Visible = False
            lnbLeaveFile.Visible = True
            Select Case objEmp_PermissionsRequest.FK_StatusId
                Case 1
                    ControlsStatus(True)
                Case 2
                    ControlsStatus(False)
                Case 3
                    ControlsStatus(False)
                Case 4
                    ControlsStatus(True)
            End Select

            If FileExtension = "" Then
                lnbLeaveFile.Visible = False
                Label1.Visible = False
            Else
                lnbLeaveFile.Visible = True
                Label1.Visible = True
                lnbLeaveFile.HRef = "../PermissionRequestFiles/" + .PermissionId.ToString() + FileExtension
            End If

            '''''''''''''''''''''''''''''''''''''''''''
            ' Enable / Disable time pickers according to the full day 
            ' value
            If dtEmpViolation.Rows.Count > 1 Then
                chckFullDay.Checked = dtEmpViolation.Rows(0)("IsFullDay")
                ManageSomeControlsStatus(dtEmpViolation.Rows(0)("IsForPeriod"), dtEmpViolation.Rows(0)("PermDate"), IIf(IsDBNull(dtEmpViolation.Rows(0)("PermEndDate")), Nothing, dtEmpViolation.Rows(0)("PermEndDate")), dtEmpViolation.Rows(0)("IsFullDay"))
                If IsDevidedPermission Then
                    ManageDevideSomeControlsStatus(dtEmpViolation.Rows(1)("IsForPeriod"), dtEmpViolation.Rows(1)("PermDate"), IIf(IsDBNull(dtEmpViolation.Rows(0)("PermEndDate")), Nothing, dtEmpViolation.Rows(0)("PermEndDate")), dtEmpViolation.Rows(1)("IsFullDay"))
                End If
            End If
            btnSavePermission.Text = ResourceManager.GetString("btnUpdate", CultureInfo)
        End With
    End Sub

    Private Sub PermissionControlsStatus(ByVal Status As Boolean)
        RadCmpPermissions.Enabled = Status
        radBtnOneDay.Enabled = Status
        dtpPermissionDate.Enabled = Status
        radBtnPeriod.Enabled = Status
        dtpStartDatePerm.Enabled = Status
        dtpEndDatePerm.Enabled = Status
        chckFullDay.Enabled = Status
        RadTPfromTime.Enabled = Status
        RadTPtoTime.Enabled = Status
        fuAttachFile.Enabled = Status
        txtRemarks.Enabled = Status
        btnSavePermission.Visible = Status
        btnClearPermission.Visible = Status

    End Sub

    Private Sub ManageSomeControlsStatus(ByVal IsForPeriod As Boolean,
                                         ByVal PermDate As DateTime,
                                         ByVal PermEndDate As DateTime?,
                                         ByVal FullDay As Boolean)
        ' Manage the DatePickers according 
        ' to the permission type
        ShowHide(IsForPeriod)
        If IsForPeriod = False Then
            dtpPermissionDate.SelectedDate = PermDate
        Else
            dtpStartDatePerm.SelectedDate = PermDate
            dtpEndDatePerm.SelectedDate =
                IIf(CheckDate(PermEndDate) = Nothing, Nothing, PermEndDate)
        End If
        ' If the permission for full day , means to disable the TimeView(s)
        ' otherwise means to enable or keep it enable
        If FullDay = False Then
            setTimeDifference()
        Else
            txtTimeDifference.Text = String.Empty
        End If
    End Sub

    Private Sub ManageDevideSomeControlsStatus(ByVal IsForPeriod As Boolean,
                                         ByVal PermDate As DateTime,
                                         ByVal PermEndDate As DateTime?,
                                         ByVal FullDay As Boolean)
        ' Manage the DatePickers according 
        ' to the permission type
        DevideShowHide(IsForPeriod)
        If IsForPeriod = False Then
            dtpDevidePermissionDate.SelectedDate = PermDate
        Else
            dtpDevideStartDatePerm.SelectedDate = PermDate
            dtpDevideEndDatePerm.SelectedDate =
                IIf(CheckDate(PermEndDate) = Nothing, Nothing, PermEndDate)
        End If
        ' If the permission for full day , means to disable the TimeView(s)
        ' otherwise means to enable or keep it enable
        If FullDay = False Then
            setDevideTimeDifference()
        Else
            txtDevideTimeDifference.Text = String.Empty
        End If
    End Sub

    Private Sub ShowHide(ByVal IsPeriod As Boolean)
        pnlPeriodLeave.Visible = IsPeriod
        PnlOneDayLeave.Visible = Not IsPeriod
        radBtnOneDay.Checked = Not IsPeriod
        radBtnPeriod.Checked = IsPeriod
    End Sub

    Private Sub DevideShowHide(ByVal IsPeriod As Boolean)
        pnlDevidePeriodLeave.Visible = IsPeriod
        PnlDevideOneDayLeave.Visible = Not IsPeriod
        radDevideBtnOneDay.Checked = Not IsPeriod
        radDevideBtnPeriod.Checked = IsPeriod
    End Sub

    Private Sub DisableEnableTimeView(ByVal status As Boolean)
        RadTPfromTime.Enabled = status
        RadTPtoTime.Enabled = status
    End Sub

    Private Sub DisableEnableDevideTimeView(ByVal status As Boolean)
        RadTPDevidefromTime.Enabled = status
        RadTPDevidetoTime.Enabled = status
    End Sub

    Private Function PermissionRequestIsExist(ByVal M_Date As DateTime, ByVal PermFromTime As DateTime, ByVal PermToTime As DateTime) As Boolean
        '' full revamp



        objEmp_PermissionsRequest = New Emp_PermissionsRequest()
        With objEmp_PermissionsRequest
            .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            .M_Date = M_Date
            .FromTime = PermFromTime
            .ToTime = PermToTime
            If .CheckHasPermissionDuringTime Then
                Return True
            Else
                Return False
            End If
        End With



    End Function

    Private Function PermissionIsExist(ByVal M_Date As DateTime, ByVal PermFromTime As DateTime, ByVal PermToTime As DateTime) As Boolean

        objEmp_Permissions = New Emp_Permissions()
        With objEmp_Permissions
            .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            .M_Date = M_Date
            .FromTime = PermFromTime
            .ToTime = PermToTime
            If .CheckHasPermissionDuringTime Then
                Return True
            Else
                Return False
            End If
        End With

    End Function
    Private Function PermissionRequeststatus(ByVal M_Date As DateTime, ByVal PermFromTime As DateTime, ByVal PermToTime As DateTime) As String

        objEmp_PermissionsRequest = New Emp_PermissionsRequest()
        With objEmp_PermissionsRequest
            .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            .M_Date = M_Date
            .FromTime = PermFromTime
            .ToTime = PermToTime
            Return .PermissionStatusDuringTime(SessionVariables.CultureInfo)

        End With

    End Function
    Private Function CheckPermissionTypeDuration() As Boolean
        'If Not LeaveTypeDuration = 0 Then

        Dim permId As Integer = RadCmpPermissions.SelectedValue
        Dim monthlyBalance As Integer = 0
        Dim remainingBalanceCount As Integer = 0
        objPermissionsTypes = New PermissionsTypes()

        With objPermissionsTypes
            .PermId = permId
            .GetByPK()
            monthlyBalance = .MonthlyBalance
        End With

        Dim EmpDt As DataTable
        Dim intFromtime As Integer = 0
        Dim intToTime As Integer = 0
        Dim studyFound As Boolean = False

        objEmp_Permissions = New Emp_Permissions
        With objEmp_Permissions
            .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
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
                        Continue For
                    End If

                    If Not row("PermissionOption") = 2 Then
                        remainingBalanceCount += (Convert.ToDateTime(row("ToTime")).Subtract(Convert.ToDateTime(row("FromTime")))).TotalMinutes
                        'intFromtime += (Convert.ToDateTime(row("FromTime")).Hour * 60) + (Convert.ToDateTime(row("FromTime")).Minute)
                        'intToTime += (Convert.ToDateTime(row("ToTime")).Hour * 60) + (Convert.ToDateTime(row("ToTime")).Minute)

                    End If
                Next

                remainingBalanceCount += (Convert.ToDateTime(RadTPtoTime.SelectedDate.Value).Subtract(Convert.ToDateTime(RadTPfromTime.SelectedDate.Value))).TotalMinutes
                'intFromtime += (RadTPfromTime.SelectedDate.Value.Hour * 60) + (RadTPfromTime.SelectedDate.Value.Minute)
                'intToTime += (RadTPtoTime.SelectedDate.Value.Hour * 60) + (RadTPtoTime.SelectedDate.Value.Minute)

                'If studyFound Then
                If remainingBalanceCount > monthlyBalance Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return True
            End If
        Else
            Return True
        End If

        Return True
    End Function

    Private Function CheckDevidePermissionTypeDuration() As Boolean
        'If Not LeaveTypeDuration = 0 Then

        Dim permId As Integer = RadCmpPermissions.SelectedValue
        Dim monthlyBalance As Integer = 0
        Dim remainingBalanceCount As Integer = 0
        objPermissionsTypes = New PermissionsTypes()

        With objPermissionsTypes
            .PermId = permId
            .GetByPK()
            monthlyBalance = .MonthlyBalance
        End With

        Dim EmpDt As DataTable
        Dim intFromtime As Integer = 0
        Dim intToTime As Integer = 0
        Dim studyFound As Boolean = False

        objEmp_Permissions = New Emp_Permissions
        With objEmp_Permissions
            .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            .FK_PermId = RadCmpDevidePermissions.SelectedValue

            If radBtnOneDay.Checked Then
                .FromTime = DateSerial(dtpDevidePermissionDate.SelectedDate.Value.Year, dtpDevidePermissionDate.SelectedDate.Value.Month, 1)
                .ToTime = DateSerial(dtpDevidePermissionDate.SelectedDate.Value.Year, dtpDevidePermissionDate.SelectedDate.Value.Month + 1, 0)
            Else
                .FromTime = DateSerial(dtpDevideStartDatePerm.SelectedDate.Value.Year, dtpDevideStartDatePerm.SelectedDate.Value.Month, 1)
                .ToTime = DateSerial(dtpDevideEndDatePerm.SelectedDate.Value.Year, dtpDevideEndDatePerm.SelectedDate.Value.Month + 1, 0)
            End If

            EmpDt = .GetAllDurationByEmployee()
        End With

        If EmpDt IsNot Nothing Then
            If EmpDt.Rows.Count > 0 Then
                For Each row As DataRow In EmpDt.Rows
                    If row("PermissionOption") = 3 Then
                        studyFound = True
                        Continue For
                    End If

                    If Not row("PermissionOption") = 2 Then
                        remainingBalanceCount += (Convert.ToDateTime(row("ToTime")).Subtract(Convert.ToDateTime(row("FromTime")))).TotalMinutes
                        'intFromtime += (Convert.ToDateTime(row("FromTime")).Hour * 60) + (Convert.ToDateTime(row("FromTime")).Minute)
                        'intToTime += (Convert.ToDateTime(row("ToTime")).Hour * 60) + (Convert.ToDateTime(row("ToTime")).Minute)

                    End If
                Next

                remainingBalanceCount += (Convert.ToDateTime(RadTPDevidetoTime.SelectedDate.Value).Subtract(Convert.ToDateTime(RadTPDevidefromTime.SelectedDate.Value))).TotalMinutes
                'intFromtime += (RadTPfromTime.SelectedDate.Value.Hour * 60) + (RadTPfromTime.SelectedDate.Value.Minute)
                'intToTime += (RadTPtoTime.SelectedDate.Value.Hour * 60) + (RadTPtoTime.SelectedDate.Value.Minute)

                'If studyFound Then
                If remainingBalanceCount > monthlyBalance Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return True
            End If
        Else
            Return True
        End If

        Return True
    End Function

    Sub SaveEmployeePermission(ByVal ErrorMessage As String)
        objEmp_Permissions = New Emp_Permissions
        objRECALC_REQUEST = New RECALC_REQUEST
        Dim temp_date As Date
        Dim temp_str_date As String
        Dim err2 As Integer

        objEmp_Permissions.AddPermAllProcess(SessionVariables.LoginUser.FK_EmployeeId, RadCmpPermissions.SelectedValue, objEmp_PermissionsRequest.IsDividable,
                                             objEmp_PermissionsRequest.IsFlexible, objEmp_PermissionsRequest.IsSpecificDays, objEmp_PermissionsRequest.Remark, objEmp_PermissionsRequest.PermDate,
                                             objEmp_PermissionsRequest.PermEndDate, objEmp_PermissionsRequest.IsForPeriod, objEmp_PermissionsRequest.FromTime, objEmp_PermissionsRequest.ToTime,
                                             objEmp_PermissionsRequest.PermDate, PermissionDaysCount, OffAndHolidayDays, objEmp_PermissionsRequest.Days,
                                             EmpLeaveTotalBalance, PermissionId, 1, 0, objEmp_PermissionsRequest.AttachedFile, objEmp_PermissionsRequest.IsFullDay, Nothing, ErrorMessage)

        If Not String.IsNullOrEmpty(ErrorMessage) Then

            If Not objEmp_PermissionsRequest.IsForPeriod Then
                temp_date = objEmp_PermissionsRequest.PermDate
                temp_str_date = DateToString(temp_date)
                objRECALC_REQUEST.EMP_NO = SessionVariables.LoginUser.FK_EmployeeId
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
                Dim dteFrom As DateTime = objEmp_PermissionsRequest.PermDate
                Dim dteTo As DateTime = objEmp_PermissionsRequest.PermEndDate

                While dteFrom <= dteTo
                    If Not dteFrom = Date.Now.AddDays(1).ToShortDateString() Then
                        temp_str_date = DateToString(dteFrom)
                        objRECALC_REQUEST = New RECALC_REQUEST()
                        objRECALC_REQUEST.EMP_NO = SessionVariables.LoginUser.FK_EmployeeId
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
            CtlCommon.ShowMessage(Me.Page, ErrorMessage, "info")
        End If
    End Sub

    Private Sub GetRemainingBalance()
        objEmp_PermissionsRequest = New Emp_PermissionsRequest()
        objPermissionsTypes = New PermissionsTypes()
        objEmp_Permissions = New Emp_Permissions()

        Dim empId As Integer = SessionVariables.LoginUser.FK_EmployeeId
        Dim permId As Integer = RadCmpPermissions.SelectedValue
        'Dim permMonth As Integer = -1
        Dim remainingBalanceCount As Integer = 0
        Dim monthlyBalance As Integer = 0

        'If radBtnOneDay.Checked Then
        '    permMonth = dtpPermissionDate.SelectedDate.Value.Month
        'ElseIf radBtnPeriod.Checked Then
        '    permMonth = dtpStartDatePerm.SelectedDate.Value.Month
        'End If

        If (Not RadCmpPermissions.SelectedValue = "-1") Then
            objEmp_PermissionsRequest.FK_EmployeeId = empId
            objEmp_PermissionsRequest.FK_PermId = permId

            objEmp_Permissions.FK_EmployeeId = empId
            objEmp_Permissions.FK_PermId = permId

            'Dim dtpermissionRemainingBalance As DataTable = objEmp_PermissionsRequest.GetAll_ByPermMonthAndType(permMonth)
            If radBtnOneDay.Checked Then
                objEmp_Permissions.FromTime = DateSerial(dtpPermissionDate.SelectedDate.Value.Year, dtpPermissionDate.SelectedDate.Value.Month, 1)
                objEmp_Permissions.ToTime = DateSerial(dtpPermissionDate.SelectedDate.Value.Year, dtpPermissionDate.SelectedDate.Value.Month + 1, 0)
            Else
                objEmp_Permissions.FromTime = DateSerial(dtpStartDatePerm.SelectedDate.Value.Year, dtpStartDatePerm.SelectedDate.Value.Month, 1)
                objEmp_Permissions.ToTime = DateSerial(dtpEndDatePerm.SelectedDate.Value.Year, dtpEndDatePerm.SelectedDate.Value.Month + 1, 0)
            End If

            'Dim dtpermissionRemainingBalance As DataTable = objEmp_PermissionsRequest.GetAll_ByPermMonthAndType(permMonth)
            Dim dtPermissionRemainingBalance As DataTable = objEmp_Permissions.GetAllDurationByEmployee()

            With objPermissionsTypes
                .PermId = permId
                .GetByPK()
                monthlyBalance = .MonthlyBalance
            End With

            For Each row As DataRow In dtPermissionRemainingBalance.Rows
                If (Not IsDBNull(row("ToTime"))) AndAlso (Not IsDBNull(row("FromTime"))) Then
                    remainingBalanceCount += (Convert.ToDateTime(row("ToTime")).Subtract(Convert.ToDateTime(row("FromTime")))).TotalMinutes
                End If
            Next
        Else
            dvGeneralGuide.Visible = False
            'labelGeneralGuide.Text = String.Empty
        End If

        Dim dcmRemainingBalanceCount As Decimal = (monthlyBalance - remainingBalanceCount)
        EmpPermissionRemainingBalance = dcmRemainingBalanceCount
        Dim hours As Integer = dcmRemainingBalanceCount \ 60
        Dim minutes As Integer = dcmRemainingBalanceCount - (hours * 60)
        Dim timeElapsed As String = CType(hours, String) & ":" & CType(minutes, String)
        lblRemainingBalanceValue.Text = timeElapsed

        'lblRemainingBalanceValue.Text = (Decimal.Round(dcmRemainingBalanceCount, 2)).ToString()

    End Sub

    Private Sub GetDevideRemainingBalance()
        objEmp_PermissionsRequest = New Emp_PermissionsRequest()
        objPermissionsTypes = New PermissionsTypes()
        objEmp_Permissions = New Emp_Permissions()

        Dim empId As Integer = SessionVariables.LoginUser.FK_EmployeeId
        Dim permId As Integer = RadCmpDevidePermissions.SelectedValue
        'Dim permMonth As Integer = -1
        Dim remainingBalanceCount As Integer = 0
        Dim monthlyBalance As Integer = 0

        'If radDevideBtnOneDay.Checked Then
        '    permMonth = dtpDevidePermissionDate.SelectedDate.Value.Month
        'ElseIf radDevideBtnPeriod.Checked Then
        '    permMonth = dtpDevideStartDatePerm.SelectedDate.Value.Month
        'End If

        If (Not RadCmpPermissions.SelectedValue = "-1") Then
            objEmp_PermissionsRequest.FK_EmployeeId = empId
            objEmp_PermissionsRequest.FK_PermId = permId

            objEmp_Permissions.FK_EmployeeId = empId
            objEmp_Permissions.FK_PermId = permId

            'Dim dtpermissionRemainingBalance As DataTable = objEmp_PermissionsRequest.GetAll_ByPermMonthAndType(permMonth)
            If radDevideBtnOneDay.Checked Then
                objEmp_Permissions.FromTime = DateSerial(dtpDevidePermissionDate.SelectedDate.Value.Year, dtpDevidePermissionDate.SelectedDate.Value.Month, 1)
                objEmp_Permissions.ToTime = DateSerial(dtpDevidePermissionDate.SelectedDate.Value.Year, dtpDevidePermissionDate.SelectedDate.Value.Month + 1, 0)
            Else
                objEmp_Permissions.FromTime = DateSerial(dtpDevideStartDatePerm.SelectedDate.Value.Year, dtpDevideStartDatePerm.SelectedDate.Value.Month, 1)
                objEmp_Permissions.ToTime = DateSerial(dtpDevideEndDatePerm.SelectedDate.Value.Year, dtpDevideEndDatePerm.SelectedDate.Value.Month + 1, 0)
            End If

            'Dim dtpermissionRemainingBalance As DataTable = objEmp_PermissionsRequest.GetAll_ByPermMonthAndType(permMonth)
            Dim dtPermissionRemainingBalance As DataTable = objEmp_Permissions.GetAllDurationByEmployee()

            With objPermissionsTypes
                .PermId = permId
                .GetByPK()
                monthlyBalance = .MonthlyBalance
            End With

            For Each row As DataRow In dtPermissionRemainingBalance.Rows
                If (Not IsDBNull(row("ToTime"))) AndAlso (Not IsDBNull(row("FromTime"))) Then
                    remainingBalanceCount += (Convert.ToDateTime(row("ToTime")).Subtract(Convert.ToDateTime(row("FromTime")))).TotalMinutes
                End If
            Next
        Else
            dvGeneralGuide.Visible = False
            'labelGeneralGuide.Text = String.Empty
        End If

        Dim dcmRemainingBalanceCount As Decimal = (monthlyBalance - remainingBalanceCount)
        EmpDevidePermissionRemainingBalance = dcmRemainingBalanceCount
        Dim hours As Integer = dcmRemainingBalanceCount \ 60
        Dim minutes As Integer = dcmRemainingBalanceCount - (hours * 60)
        Dim timeElapsed As String = CType(hours, String) & ":" & CType(minutes, String)
        lblDevideRemainingBalanceValue.Text = timeElapsed

        'lblRemainingBalanceValue.Text = (Decimal.Round(dcmRemainingBalanceCount, 2)).ToString()

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

    '::End Permission Request

    '::Leave Request

    Private Sub FillLeaveTypes()
        objAPP_Settings = New APP_Settings
        objAPP_Settings.GetByPK()
        objProjectCommon = New ProjectCommon()
        Dim dt As DataTable = Nothing
        dt = Nothing
        objEmp_LeavesRequest = New Emp_LeavesRequest()
        objEmp_LeavesRequest.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
        'dt = objLeavesTypes.GetAllForDDL
        dt = objEmp_LeavesRequest.GetAllowed_ForSelfServiceAndGrade
        GetEmployeeGender()
        If Not Gender = "f" Then
            For Each row In dt.Rows
                If row("LeaveId").ToString = (objAPP_Settings.FK_MaternityLeaveTypeId).ToString Then
                    row.delete()
                End If
            Next
            dt.AcceptChanges()
        End If

        For Each row In dt.Rows
            If row("AllowedGender").ToString = 1 And Gender = "f" Then
                row.Delete()
            ElseIf row("AllowedGender").ToString = 2 And Gender = "m" Then
                row.Delete()
            End If
        Next
        dt.AcceptChanges()

        objEmployee = New Employee
        objEmployee.EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
        objEmployee.GetByPK()

        For Each row In dt.Rows
            If row("AllowForSpecificEmployeeType") = True Then
                If row("FK_EmployeeTypeId").ToString <> objEmployee.FK_LogicalGroup Then
                    row.delete()
                End If
            End If
        Next
        dt.AcceptChanges()


        ProjectCommon.FillRadComboBox(ddlLeaveType, dt, "LeaveName",
                                        "LeaveArabicName", "LeaveId")

    End Sub

    Private Sub GetEmployeeGender()
        objEmployee = New Employee
        With objEmployee
            .EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            .GetByPK()
            Gender = .Gender
        End With
    End Sub

    Sub ClearAll()
        txtRemarks.Text = String.Empty
        ddlLeaveType.SelectedIndex = -1
        dtpFromDate.Clear()
        dtpToDate.Clear()
        dtpRequestDate.SelectedDate = Date.Today
        'chkHalfDay.Checked = False
        LeaveRequestID = 0
        FillGridView()
        txtLeaveRemarks.Text = String.Empty

        dtpFromDate.Enabled = True
        dtpToDate.Enabled = True
        btnSavePermission.Text = ResourceManager.GetString("btnSave", CultureInfo)

    End Sub

    Private Sub ControlsStatus(ByVal Status As Boolean)
        ddlLeaveType.Enabled = Status
        dtpFromDate.Enabled = Status
        dtpToDate.Enabled = Status
        txtRemarks.Enabled = Status
        btnSave.Visible = Status
        btnClear.Visible = Status
    End Sub

    Private Sub FillLeaveControl()
        objEmp_LeavesRequest = New Emp_LeavesRequest
        With objEmp_LeavesRequest
            .LeaveId = LeaveRequestID
            .GetByPK()
            ddlLeaveType.SelectedValue = .FK_LeaveTypeId
            dtpFromDate.SelectedDate = .FromDate
            dtpToDate.SelectedDate = .ToDate
            dtpRequestDate.SelectedDate = .RequestDate
            txtRemarks.Text = .Remarks
            fuAttachFile.Visible = False
            lnbLeaveFile.Visible = True
            FileExtension = .AttachedFile

            Select Case objEmp_LeavesRequest.FK_StatusId
                Case 1
                    ControlsStatus(True)
                Case 2
                    ControlsStatus(False)
                Case 3
                    ControlsStatus(False)
                Case 4
                    ControlsStatus(True)
            End Select

            If .AttachedFile = "" Then
                lnbLeaveFile.Visible = False
                Label1.Visible = False
            Else
                lnbLeaveFile.Visible = True
                Label1.Visible = True
                lnbLeaveFile.HRef = "../LeaveRequestFiles/" + .LeaveId.ToString() + .AttachedFile
            End If

            btnSave.Text = ResourceManager.GetString("btnUpdate", CultureInfo)
        End With
    End Sub

    Private Function LeaveIsExsist(ByVal M_Date As String, ByRef StatusName As String) As Boolean
        dtpFromDate.SelectedDate = Convert.ToDateTime(M_Date)
        dtpToDate.SelectedDate = Convert.ToDateTime(M_Date)

        dtpFromDate.Enabled = False
        dtpToDate.Enabled = False

        objEmp_LeavesRequest = New Emp_LeavesRequest()
        With objEmp_LeavesRequest
            .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            .FromDate = Convert.ToDateTime(M_Date)
            Dim dtEmpLeave As DataTable = .GetViolationRequestByEmployee()
            If dtEmpLeave IsNot Nothing Then
                If dtEmpLeave.Rows.Count > 0 Then
                    For i = 0 To dtEmpLeave.Rows.Count - 1
                        If Convert.ToDateTime(dtEmpLeave.Rows(i)("FromDate")) = .FromDate Then
                            LeaveRequestID = dtEmpLeave.Rows(i)("LeaveId")

                            If Lang = CtlCommon.Lang.EN Then
                                StatusName = dtEmpLeave.Rows(i)("StatusName")
                            ElseIf Lang = CtlCommon.Lang.AR Then
                                StatusName = dtEmpLeave.Rows(i)("StatusNameArabic")
                            End If

                        End If
                    Next

                End If
            End If
            If Not LeaveRequestID = 0 Then
                LeaveRequestID = 0
                Return True
            Else
                Return False
            End If
        End With

    End Function

    '::End Leave Request
    Private Sub FillData()
        objAnnouncements = New Announcements()
        Dim userid As String = SessionVariables.LoginUser.ID
        Dim language As String = SessionVariables.CultureInfo

        Dim mycdcatalog = New DataTable
        mycdcatalog = objAnnouncements.GetTop5AnnouncementsSelfServices(userid, language)
        With objAnnouncements


            repAnnouncement.DataSource = mycdcatalog
            repAnnouncement.DataBind()


        End With
    End Sub
    Public Sub FillHighCharts()
        Dim language As String
        If SessionVariables.CultureInfo = "en-US" Then
            language = "en"
        ElseIf SessionVariables.CultureInfo = "ar-JO" Then
            language = "ar"
        End If
        Dim objDashBoard As New DashBoard
        Dim dtCAChart As DataTable

        With objDashBoard
            .Lang = language
            .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            .FromDate = dteFromDate.SelectedDate
            .ToDate = dteToDate.SelectedDate
            dtCAChart = .GetEmployeeSummaryDash
            If Not dtCAChart Is Nothing Then
                dt = dtCAChart
            End If
            '{id: 'absent',name:   'Absent',data:  [['Absent',26.11], ['Not Absent',73.89] ] } 

        End With
    End Sub
    Public Sub fillChart()
        If Not (dt Is Nothing) Then

            For i As Integer = 0 To dt.Rows.Count - 1
                Str = dt.Rows(i)("descrip")
                STRsPLIT = Str.Split(":")
                Dim A As Decimal
                A = Convert.ToDecimal(STRsPLIT(1).Replace("%", ""))
                If A > 0 Then
                    data += "{name:   '"
                    data += STRsPLIT(0)
                    data += "',  y: "
                    data += STRsPLIT(1).Replace("%", "")
                    data += "},"
                End If

            Next
        End If

        ''''''''''''''
    End Sub

    Private Sub ArcivingMonths_DateValidation()
        objAPP_Settings = New APP_Settings
        objAPP_Settings.GetByPK()
        Dim ArchivingMonths As Integer = objAPP_Settings.ArchivingMonths

        If Not ArchivingMonths = 0 Then
            ArchivingMonths = ArchivingMonths
            Dim NewDate As Date = DateAdd(DateInterval.Month, ArchivingMonths, Date.Today)
            dteFromDate.MinDate = New Date(NewDate.Year, NewDate.Month, NewDate.Day)
            dteToDate.MinDate = New Date(NewDate.Year, NewDate.Month, NewDate.Day)
        End If

    End Sub

    Private Sub FillEmployeesWorkingHoursDash()
        objDashBoard = New DashBoard
        Dim dt As DataTable
        With objDashBoard

            objDashBoard.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId

            If dteFromDate.SelectedDate.HasValue Then
                objDashBoard.FromDate = dteFromDate.SelectedDate
            Else
                dteFromDate.SelectedDate = Date.Today
            End If

            If dteToDate.SelectedDate.HasValue Then
                objDashBoard.ToDate = dteToDate.SelectedDate
            Else
                dteToDate.SelectedDate = Date.Today
            End If

            dt = .GetEmployeeWorkingHoursDash

            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1

                        EmployeeWorkingHoursCountdata += "{"
                        EmployeeWorkingHoursCountdata += "name:"
                        EmployeeWorkingHoursCountdata += "'" + dt.Rows(i)("WorkPeriod") + "'" + ", data:["

                        Dim countj As Integer = 0
                        For j As Integer = 0 To dt.Rows.Count - 1
                            If dt.Rows(j)("WorkPeriod") = dt.Rows(i)("WorkPeriod") Then
                                EmployeeWorkingHoursCountdata += dt.Rows(j)("EmployeesCount").ToString + ","
                            End If
                        Next

                        EmployeeWorkingHoursCountdata += "]},"
                    Next
                End If
            End If

        End With

        JSONEmployeeWorkingHoursCountSeries.Text = "<script type='text/javascript' charset='utf-8'>var Var_EmployeeWorkingHoursCountSeries = [" & EmployeeWorkingHoursCountdata & "] </Script>"
        JSONEmployeeWorkingHoursCountChartTitleText.Text = "<script type='text/javascript' charset='utf-8'>var Var_EmployeeWorkingHoursCountChartTitleText = '" & IIf(Lang = CtlCommon.Lang.AR, "ساعات عمل الموظفين", "Employees Working Hours") & "'</Script>"
        JSONEmployeeWorkingHoursCountChartSubTitleText.Text = "<script type='text/javascript' charset='utf-8'>var Var_EmployeeWorkingHoursCountChartSubTitleText = '" & IIf(Lang = CtlCommon.Lang.AR, "ساعات العمل", "Work Hours") & "'</Script>"
        Dim dateFromDate As String
        Dim dateToDate As String

        dateFromDate = Convert.ToDateTime(dteFromDate.SelectedDate).ToString("dd/MM/yyyy")
        dateToDate = Convert.ToDateTime(dteToDate.SelectedDate).ToString("dd/MM/yyyy")

        JSONEmployeeWorkingHoursCountChartDateText.Text = "<script type='text/javascript' charset='utf-8'>var Var_EmployeeWorkingHoursCountChartDateText = '" & dateFromDate & "-" & dateToDate & "'</Script>"

    End Sub

#End Region

#Region "Handle Selected Time changed"

    Protected Sub RadTPtoTim_SelectedDateChanged(ByVal sender As Object,
                                                         ByVal e As SelectedDateChangedEventArgs) _
                                                         Handles RadTPtoTime.SelectedDateChanged
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        If (hdnIsvalid.Value) Then
            setTimeDifference()
            RadTPDevidefromTime.SelectedDate = RadTPtoTime.SelectedDate
            setDevideTimeDifference()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("TimeRange", CultureInfo), "info")

        End If

    End Sub

    Protected Sub RadTPfromTime_SelectedDateChanged(ByVal sender As Object,
                                                             ByVal e As SelectedDateChangedEventArgs) _
                                                     Handles RadTPfromTime.SelectedDateChanged
        If (hdnIsvalid.Value) Then
            setTimeDifference()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("TimeRange", CultureInfo), "info")
        End If

    End Sub

    Protected Sub RadTPDevidetoTim_SelectedDateChanged(ByVal sender As Object,
                                                         ByVal e As SelectedDateChangedEventArgs) _
                                                         Handles RadTPDevidetoTime.SelectedDateChanged
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        If (hdnDevideIsvalid.Value) Then
            setDevideTimeDifference()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("TimeRange", CultureInfo), "info")

        End If

    End Sub

    Protected Sub RadTPDevidefromTime_SelectedDateChanged(ByVal sender As Object,
                                                             ByVal e As SelectedDateChangedEventArgs) _
                                                     Handles RadTPDevidefromTime.SelectedDateChanged
        If (hdnDevideIsvalid.Value) Then
            setDevideTimeDifference()
            RadTPtoTime.SelectedDate = RadTPDevidefromTime.SelectedDate
            setTimeDifference()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("TimeRange", CultureInfo), "info")
        End If

    End Sub

    Private Function setTimeDifference() As TimeSpan
        Try
            Dim temp1 As DateTime = Nothing
            Dim temp2 As DateTime = Nothing

            temp1 = RadTPfromTime.SelectedDate.Value
            temp2 = RadTPtoTime.SelectedDate.Value
            Dim startTime As New DateTime(2011, 1, 1,
                                          temp1.Hour(), temp1.Minute(), temp1.Second)

            Dim endTime As New DateTime(2011, 1, 1,
                                          temp2.Hour(), temp2.Minute(), temp2.Second)


            Dim c As TimeSpan = (endTime.Subtract(startTime))
            Dim result As Integer =
                DateTime.Compare(endTime, startTime)

            Dim hours As Integer = 0
            Dim minutes As Integer = 0

            If result = 0 Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    txtTimeDifference.Text = 0 & " ساعات," &
                      0 & " دقائق"
                    txtTimeDifference.Style("text-align") = "right"
                Else
                    txtTimeDifference.Text = 0 & " hours," &
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
                    txtTimeDifference.Text = c.Hours() & " ساعات," &
                     TotalMinutes & " دقائق"
                    txtTimeDifference.Style("text-align") = "right"
                Else
                    txtTimeDifference.Text = c.Hours() & " hours," &
                     TotalMinutes & " minutes"

                End If

            ElseIf result < 0 Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    txtTimeDifference.Text = c.Hours() & " ساعات," &
                       Math.Ceiling(c.TotalMinutes()) & " دقائق"
                    txtTimeDifference.Style("text-align") = "right"
                Else
                    txtTimeDifference.Text = c.Hours() & " hours," &
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
                        txtTimeDifference.Text = hours & " ساعات," &
                          Math.Ceiling(c.TotalMinutes()) & " دقائق"
                        txtTimeDifference.Style("text-align") = "right"
                    Else
                        txtTimeDifference.Text = hours & " hours," &
                          Math.Ceiling(c.TotalMinutes()) & " minutes"

                    End If
                End If
            End If
        Catch ex As Exception

            MsgBox(ex.Message)

        End Try
    End Function

    Private Function setDevideTimeDifference() As TimeSpan
        Try
            Dim temp1 As DateTime = Nothing
            Dim temp2 As DateTime = Nothing

            temp1 = RadTPDevidefromTime.SelectedDate.Value
            temp2 = RadTPDevidetoTime.SelectedDate.Value
            Dim startTime As New DateTime(2011, 1, 1,
                                          temp1.Hour(), temp1.Minute(), temp1.Second)

            Dim endTime As New DateTime(2011, 1, 1,
                                          temp2.Hour(), temp2.Minute(), temp2.Second)


            Dim c As TimeSpan = (endTime.Subtract(startTime))
            Dim result As Integer =
                DateTime.Compare(endTime, startTime)

            Dim hours As Integer = 0
            Dim minutes As Integer = 0

            If result = 0 Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    txtDevideTimeDifference.Text = 0 & " ساعات," &
                      0 & " دقائق"
                    txtDevideTimeDifference.Style("text-align") = "right"
                Else
                    txtDevideTimeDifference.Text = 0 & " hours," &
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
                    txtDevideTimeDifference.Text = c.Hours() & " ساعات," &
                     TotalMinutes & " دقائق"
                    txtDevideTimeDifference.Style("text-align") = "right"
                Else
                    txtDevideTimeDifference.Text = c.Hours() & " hours," &
                     TotalMinutes & " minutes"

                End If

            ElseIf result < 0 Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    txtDevideTimeDifference.Text = c.Hours() & " ساعات," &
                       Math.Ceiling(c.TotalMinutes()) & " دقائق"
                    txtDevideTimeDifference.Style("text-align") = "right"
                Else
                    txtDevideTimeDifference.Text = c.Hours() & " hours," &
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
                        txtDevideTimeDifference.Text = hours & " ساعات," &
                          Math.Ceiling(c.TotalMinutes()) & " دقائق"
                        txtDevideTimeDifference.Style("text-align") = "right"
                    Else
                        txtDevideTimeDifference.Text = hours & " hours," &
                          Math.Ceiling(c.TotalMinutes()) & " minutes"

                    End If
                End If
            End If
        Catch ex As Exception

            MsgBox(ex.Message)

        End Try
    End Function

#End Region

#Region "Grid Filter"

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdEmpViolations.Skin))
    End Function

    Protected Sub dgrdEmpViolations_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdEmpViolations.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

#End Region

#Region "Calendar"

    Public Shared Function GetFullTimeString(ByVal intMinutes As Integer) As String
        Dim strHour As String = ""
        Dim strMin As String = ""
        Dim RTN As String = ""
        Dim intHours As Integer = Math.Floor(intMinutes / 60)
        Dim intMin As Integer = intMinutes Mod 60

        strHour = intHours.ToString.PadLeft(2, "0")
        strMin = intMin.ToString.PadLeft(2, "0")
        RTN = New TimeSpan(strHour, strMin, 0).ToString()
        Return RTN

    End Function

    <WebMethod()>
    Public Shared Function GetEmployee_Calendar(ByVal CalStartDate As String, ByVal CalEndDate As String) As String
        Dim objJavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer
        Dim events = New List(Of Dictionary(Of String, String))
        Dim objEvent As Dictionary(Of String, String)
        Dim OffDay_HolidayFlag As Boolean = False

        Dim DT As DataTable
        Dim objEmployeeViolations As New EmployeeViolations
        Dim objEmp_WorkSchedule As New Emp_WorkSchedule

        objEmployeeViolations.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
        Dim FromDate As DateTime
        Dim ToDate As DateTime
        If CalStartDate Is Nothing Then
            FromDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd As New Date
            dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            ToDate = dd
        Else
            Dim dateCalStartDate As DateTime = Convert.ToDateTime(CalStartDate)
            Dim dateCalEndDate As DateTime = Convert.ToDateTime(CalEndDate)
            FromDate = dateCalStartDate
            ToDate = dateCalEndDate
        End If


        objEmployeeViolations.FromDate = FromDate
        objEmployeeViolations.ToDate = ToDate
        DT = objEmployeeViolations.GetEmployeeCalendar


        For Each row As DataRow In DT.Rows
            objEvent = New Dictionary(Of String, String)
            Dim strFromDate As String = row("FromDate").ToString
            Dim strToDate As String = row("ToDate").ToString
            Dim dateFromDate As DateTime = Convert.ToDateTime(row("FromDate").ToString).ToShortDateString

            Dim dateToDate As DateTime
            If Not row("ToDate") Is DBNull.Value Then
                dateToDate = Convert.ToDateTime(row("ToDate").ToString).ToShortDateString
            Else
                dateToDate = dateFromDate
            End If


            Dim strFromTime As String = row("FromTime").ToString
            Dim strToTime As String = row("ToTime").ToString
            Dim dateFromTime As DateTime
            Dim dateToTime As DateTime
            Dim Duration As String

            If Not row("FromTime") Is DBNull.Value Then
                dateFromTime = Convert.ToDateTime(row("FromTime").ToString).ToShortTimeString
            Else
                dateFromTime = Nothing
            End If

            If Not row("ToTime") Is DBNull.Value Then
                dateToTime = Convert.ToDateTime(row("ToTime").ToString).ToShortTimeString
            Else
                dateToTime = Nothing
            End If

            If Not row("Duration") Is DBNull.Value Then
                Duration = row("Duration").ToString
            Else
                Duration = Nothing
            End If

            Dim Days As String() = Nothing
            Dim IsFlexible As Boolean
            Dim FlexiblePermissionDuration As Integer

            Dim EventName As String = ""
            Dim EventStart As String
            Dim EventEnd As String
            Dim EventColor As String = ""

            Dim ExcludeOffDays As Boolean
            If Not row("ExcludeOffDays") Is DBNull.Value Then
                ExcludeOffDays = row("ExcludeOffDays").ToString
            End If
            Dim ExcludeHolidays As Boolean
            If Not row("ExcludeHolidays") Is DBNull.Value Then
                ExcludeOffDays = row("ExcludeHolidays").ToString
            End If


            If Not row("Days") Is DBNull.Value Then
                Days = row("Days").ToString.Trim.Split(",")
            End If

            If Not row("IsFlexible") Is DBNull.Value Then
                IsFlexible = Convert.ToBoolean(row("IsFlexible").ToString)
            Else
                IsFlexible = False
            End If

            If Not row("FlexibilePermissionDuration") Is DBNull.Value Then
                FlexiblePermissionDuration = Convert.ToInt32(row("FlexibilePermissionDuration").ToString)
            End If

            Dim ApptType As Integer = Convert.ToInt32(row("ApptType").ToString)
            Dim TextEn As String = row("TextEn").ToString
            Dim TextAr As String = row("TextAr").ToString

            Dim ApptName As String
            ApptName = IIf(SessionVariables.CultureInfo = "ar-JO", TextAr, TextEn)


            If ApptType = 1 Then '---Leaves
                EventName = String.Format("{0} ({1}-{2})", ApptName, Convert.ToDateTime(strFromDate).ToString("dd/MM/yyyy"), Convert.ToDateTime(strToDate).ToString("dd/MM/yyyy"))
            ElseIf ApptType = 2 Then '---Permissions
                If Not strFromTime = Nothing AndAlso Not strToTime = Nothing Then
                    If IsFlexible = 0 Then
                        EventName = String.Format("{0} ({1}-{2})", ApptName, Convert.ToDateTime(strFromTime).ToString("HH:mm"), Convert.ToDateTime(strToTime).ToString("HH:mm"))
                    Else
                        EventName = String.Format("{0} ({1})", ApptName, FlexiblePermissionDuration)
                    End If
                Else
                    EventName = ApptName
                End If

            ElseIf ApptType = 3 Then '---Nursing Permissions
                If Not strFromTime = Nothing AndAlso Not strToTime = Nothing Then
                    If IsFlexible = 0 Then
                        EventName = String.Format("{0} ({1}-{2})", ApptName, Convert.ToDateTime(strFromTime).ToString("HH:mm"), Convert.ToDateTime(strToTime).ToString("HH:mm"))
                    Else
                        EventName = String.Format("{0} ({1})", ApptName, FlexiblePermissionDuration)
                    End If
                Else
                    EventName = ApptName
                End If

            ElseIf ApptType = 4 Then '---Study Permissions
                If Not strFromTime = Nothing AndAlso Not strToTime = Nothing Then
                    If IsFlexible = 0 Then
                        EventName = String.Format("{0} ({1}-{2})", ApptName, Convert.ToDateTime(strFromTime).ToString("HH:mm"), Convert.ToDateTime(strToTime).ToString("HH:mm"))
                    Else
                        EventName = String.Format("{0} ({1})", ApptName, FlexiblePermissionDuration)
                    End If
                Else
                    EventName = ApptName
                End If

            ElseIf ApptType = 5 Then '---Yearly Fixed Holidays
                EventName = ApptName
            ElseIf ApptType = 6 Then '---Not Yearly Fixed Holidays
                EventName = ApptName
            ElseIf ApptType = 7 Then '---Absent
                EventName = ApptName
            ElseIf ApptType = 8 Then '---Delay
                EventName = String.Format("{0} [{1}-{2}] ({3})", ApptName, Convert.ToDateTime(strFromTime).ToString("HH:mm"), Convert.ToDateTime(strToTime).ToString("HH:mm"), Duration)
            ElseIf ApptType = 9 Then '---EarlyOut
                EventName = String.Format("{0} [{1}-{2}] ({3})", ApptName, Convert.ToDateTime(strFromTime).ToString("HH:mm"), Convert.ToDateTime(strToTime).ToString("HH:mm"), Duration)
            ElseIf ApptType = 10 Then '---OutTime
                EventName = String.Format("{0} [{1}-{2}] ({3})", ApptName, Convert.ToDateTime(strFromTime).ToString("HH:mm"), Convert.ToDateTime(strToTime).ToString("HH:mm"), Duration)
            ElseIf ApptType = 11 Then '---RestDay
                EventName = ApptName
            ElseIf ApptType = 12 Then '---Leaves Requests
                EventName = String.Format("{0} ({1}-{2})", ApptName, Convert.ToDateTime(strFromDate).ToString("dd/MM/yyyy"), Convert.ToDateTime(strToDate).ToString("dd/MM/yyyy"))
            ElseIf ApptType = 13 Then '---Permissions
                If Not strFromTime = Nothing AndAlso Not strToTime = Nothing Then
                    If IsFlexible = 0 Then
                        EventName = String.Format("{0} ({1}-{2})", ApptName, Convert.ToDateTime(strFromTime).ToString("HH:mm"), Convert.ToDateTime(strToTime).ToString("HH:mm"))
                    Else
                        EventName = String.Format("{0} ({1})", ApptName, FlexiblePermissionDuration)
                    End If
                Else
                    EventName = ApptName
                End If
            End If

            Select Case ApptType
                Case 1 'Leaves
                    EventColor = "#4167B0"
                Case 2 'Permissions
                    EventColor = "#D5A92E"
                Case 3 ' Nursing
                    EventColor = "#8C2A8F"
                Case 4 'Study
                    EventColor = "#978D75"
                Case 5 'Holiday
                    EventColor = "#4BC57A"
                Case 6 'Holiday
                    EventColor = "#4BC57A"
                Case 7 'Absent
                    EventColor = "#9F0F19"
                Case 8 'Delay
                    EventColor = "#e26367"
                Case 9 'Early Out
                    EventColor = "#e26367"
                Case 10 'OutTime
                    EventColor = "#e26367"
                Case 11 'RestDay
                    EventColor = "#C0C0C0"
                Case 12 'LeavesRequests
                    EventColor = "#71b4d4"
                Case 13 'PermissionsRequests
                    EventColor = "#dbc992"
            End Select


            If dateFromDate < dateToDate Then
                If Not Days Is Nothing Then
                    For index = 0 To Days.Length - 1
                        If Days(index).Trim IsNot String.Empty Then
                            Dim CalendarDate As DateTime = dateFromDate
                            While CalendarDate <= dateToDate
                                Dim DayName As String = ""
                                Select Case Days(index).Trim
                                    Case 1
                                        DayName = WeekDayName.Sunday.ToString
                                    Case 2
                                        DayName = WeekDayName.Monday.ToString
                                    Case 3
                                        DayName = WeekDayName.Tuesday.ToString
                                    Case 4
                                        DayName = WeekDayName.Wednesday.ToString
                                    Case 5
                                        DayName = WeekDayName.Thursday.ToString
                                    Case 6
                                        DayName = WeekDayName.Friday.ToString
                                    Case 7
                                        DayName = WeekDayName.Saturday.ToString
                                End Select
                                If CalendarDate.DayOfWeek.ToString() = DayName Then

                                    If dateFromTime = DateTime.MinValue Then
                                        EventStart = Convert.ToDateTime(CalendarDate).ToString("yyyy-MM-dd").ToString()
                                    Else
                                        EventStart = CalendarDate.ToString("yyyy-MM-dd") + "T" + strFromTime
                                    End If
                                    objEmp_WorkSchedule = New Emp_WorkSchedule
                                    With objEmp_WorkSchedule
                                        .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
                                        .ScheduleDate = dateFromDate
                                        .Get_IsOffDay()
                                    End With

                                    If dateToTime = DateTime.MinValue Then
                                        EventEnd = Convert.ToDateTime(CalendarDate).ToString("yyyy-MM-dd").ToString()
                                    Else
                                        EventEnd = CalendarDate.ToString("yyyy-MM-dd") + "T" + strToTime
                                    End If

                                    objEvent = New Dictionary(Of String, String)
                                    If Not objEmp_WorkSchedule.Emp_IsOffDay Then
                                        objEvent.Add("title", EventName)
                                        objEvent.Add("start", EventStart)
                                        objEvent.Add("end", EventEnd)
                                        objEvent.Add("color", EventColor)

                                        events.Add(objEvent)
                                    End If

                                End If
                                CalendarDate = CalendarDate.AddDays(1)
                            End While
                        End If
                    Next
                Else
                    While dateFromDate <= dateToDate
                        If ApptType = "1" Or ApptType = "12" Then
                            If dateFromTime = DateTime.MinValue Then
                                EventStart = Convert.ToDateTime(dateFromDate).ToString("yyyy-MM-dd").ToString()
                            Else
                                EventStart = dateFromDate.ToString("yyyy-MM-dd") + "T" + strFromTime
                            End If
                        Else
                            If dateFromTime = DateTime.MinValue Then
                                EventStart = Convert.ToDateTime(dateFromDate).ToString("yyyy-MM-dd").ToString()
                            Else
                                EventStart = dateFromTime.ToString("yyyy-MM-dd") + "T" + strFromTime
                            End If
                        End If


                        If dateToTime = DateTime.MinValue Then
                            EventEnd = dateFromDate.ToString("yyyy-MM-dd")
                        Else
                            EventEnd = dateFromDate.ToString("yyyy-MM-dd") + "T" + strToTime
                        End If

                        objEmp_WorkSchedule = New Emp_WorkSchedule
                        With objEmp_WorkSchedule
                            .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
                            .ScheduleDate = dateFromDate
                            .Get_IsOffDay()
                            .Get_IsHoliday()
                        End With

                        objEvent = New Dictionary(Of String, String)
                        If ApptType = 1 Or ApptType = 12 Then
                            If ExcludeOffDays = True Or ExcludeHolidays = True Then
                                If objEmp_WorkSchedule.Emp_IsOffDay = True Or objEmp_WorkSchedule.Emp_IsHoliday = True Then
                                    OffDay_HolidayFlag = True
                                Else
                                    OffDay_HolidayFlag = False
                                End If

                                If OffDay_HolidayFlag = False Then
                                    objEvent.Add("title", EventName)
                                    objEvent.Add("start", EventStart)
                                    objEvent.Add("end", EventEnd)
                                    objEvent.Add("color", EventColor)

                                    events.Add(objEvent)
                                End If
                            Else
                                objEvent.Add("title", EventName)
                                objEvent.Add("start", EventStart)
                                objEvent.Add("end", EventEnd)
                                objEvent.Add("color", EventColor)

                                events.Add(objEvent)
                            End If

                        Else
                            objEvent.Add("title", EventName)
                            objEvent.Add("start", EventStart)
                            objEvent.Add("end", EventEnd)
                            objEvent.Add("color", EventColor)

                            events.Add(objEvent)
                        End If

                        dateFromDate = dateFromDate.AddDays(1)
                    End While
                End If

            Else

                If dateFromTime = Nothing Then
                    EventStart = dateFromDate.ToString("yyyy-MM-dd")
                Else
                    EventStart = dateFromDate.ToString("yyyy-MM-dd") + "T" + strFromTime
                End If

                If dateToTime = Nothing Then
                    EventEnd = dateToDate.ToString("yyyy-MM-dd")
                Else
                    EventEnd = dateToDate.ToString("yyyy-MM-dd") + "T" + strToTime
                End If

                objEvent = New Dictionary(Of String, String)
                objEvent.Add("title", EventName)
                objEvent.Add("start", EventStart)
                objEvent.Add("end", EventEnd)
                objEvent.Add("color", EventColor)

                events.Add(objEvent)
            End If

        Next

        Return objJavaScriptSerializer.Serialize(events)
    End Function

    <WebMethod()>
    Public Shared Function GetManager_Calendar(ByVal CalStartDate As String, ByVal CalEndDate As String) As String
        Dim objJavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer
        Dim events = New List(Of Dictionary(Of String, String))
        Dim objEvent As Dictionary(Of String, String)


        Dim DT As DataTable
        Dim objEmployeeViolations As New EmployeeViolations

        objEmployeeViolations.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
        Dim FromDate As DateTime
        Dim ToDate As DateTime
        If CalStartDate Is Nothing Then
            FromDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd As New Date
            dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            ToDate = dd
        Else
            Dim dateCalStartDate As DateTime = Convert.ToDateTime(CalStartDate)
            Dim dateCalEndDate As DateTime = Convert.ToDateTime(CalEndDate)
            FromDate = dateCalStartDate
            ToDate = dateCalEndDate
        End If


        objEmployeeViolations.FromDate = FromDate
        objEmployeeViolations.ToDate = ToDate
        DT = objEmployeeViolations.GetManagerCalendar


        For Each row As DataRow In DT.Rows
            objEvent = New Dictionary(Of String, String)
            Dim strFromDate As String = row("M_DATE").ToString
            Dim strToDate As String = row("M_DATE").ToString

            Dim dateFromDate As DateTime = Convert.ToDateTime(row("M_DATE").ToString).ToShortDateString
            Dim dateToDate As DateTime = Convert.ToDateTime(row("M_DATE").ToString).ToShortDateString

            Dim EventName As String = ""
            Dim EventStart As String
            Dim EventEnd As String
            Dim EventColor As String = ""
            Dim Stats_Count As String = row("Stats_Count").ToString

            Dim ApptType As Integer = Convert.ToInt32(row("ApptType").ToString)
            Dim TextEn As String = row("Cal_Subject").ToString
            Dim TextAr As String = row("Cal_SubjectAr").ToString

            Dim ApptName As String
            ApptName = IIf(SessionVariables.CultureInfo = "ar-JO", TextAr, TextEn)


            If ApptType = 1 Then '---Attendees

                EventName = String.Format("{0} ({1})", ApptName, Stats_Count)
            ElseIf ApptType = 2 Then '---Absent
                EventName = String.Format("{0} ({1})", ApptName, Stats_Count)
            ElseIf ApptType = 3 Then '---Missing In
                EventName = String.Format("{0} ({1})", ApptName, Stats_Count)
            ElseIf ApptType = 4 Then '---Missing Out
                EventName = String.Format("{0} ({1})", ApptName, Stats_Count)
            ElseIf ApptType = 5 Then '---Delay
                EventName = String.Format("{0} ({1})", ApptName, Stats_Count)
            ElseIf ApptType = 6 Then '---Early Out
                EventName = String.Format("{0} ({1})", ApptName, Stats_Count)
            ElseIf ApptType = 7 Then '---Permissions
                EventName = String.Format("{0} ({1})", ApptName, Stats_Count)
            ElseIf ApptType = 8 Then '---Leaves
                EventName = String.Format("{0} ({1})", ApptName, Stats_Count)
            ElseIf ApptType = 9 Then '---LeavesRequest
                EventName = String.Format("{0} ({1})", ApptName, Stats_Count)
            ElseIf ApptType = 10 Then '---PermissionRequest
                EventName = String.Format("{0} ({1})", ApptName, Stats_Count)
            End If

            EventStart = dateFromDate.ToString("yyyy-MM-dd")
            EventEnd = dateToDate.ToString("yyyy-MM-dd")

            Select Case ApptType
                Case 1 'Attendees
                    EventColor = "#4BC57A"
                Case 2 'Absent
                    EventColor = "#9F0F19"
                Case 3 ' Missing In
                    EventColor = "#978D75"
                Case 4 'Missing Out
                    EventColor = "#978D75" '"#D5A92E"
                Case 5 'Delay
                    EventColor = "#e26367"
                Case 6 'Early Out
                    EventColor = "#8C2A8F"
                Case 7 'Permissions
                    EventColor = "#D5A92E"
                Case 8 'Leaves
                    EventColor = "#4167B0"
                Case 9 'LeavesRequest
                    EventColor = "#71b4d4"
                Case 10 'PermissionsRequest
                    EventColor = "#dbc992"
            End Select


            objEvent = New Dictionary(Of String, String)
            objEvent.Add("type", ApptType)
            objEvent.Add("title", EventName)
            objEvent.Add("start", EventStart)
            objEvent.Add("end", EventEnd)
            objEvent.Add("color", EventColor)

            events.Add(objEvent)

        Next

        Return objJavaScriptSerializer.Serialize(events)
    End Function

#End Region


End Class
