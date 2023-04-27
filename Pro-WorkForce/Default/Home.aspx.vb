
Imports System.Data
Imports System.Web.Script.Serialization
Imports System.Web.Services
Imports SmartV.UTILITIES
Imports TA
Imports TA.Admin
Imports TA.DashBoard
Imports TA.Employees
Imports TA.Security
Imports Telerik.Web.UI.Calendar

Partial Class Default_Theme2
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objDashBoard As DashBoard
    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private objversion As New SmartV.Version.version
    Public chartLang As String
    Private objSYSForms As SYSForms
    Private objSYS_Users_Security As SYS_Users_Security
    Dim objSYSGroups As SYSGroups
    Dim objAPP_Settings As APP_Settings
    Dim objEmp_Managers As Employee_Manager
    Dim objEmp_DeputyManager As Emp_DeputyManager
    Dim objEmp_HR As Emp_HR
    Private objEmployee As Employee
    Private objSYSUsers As SYSUsers
    Dim StatusCountdata As String = ""
    Dim RequestStatusCountdata As String = ""
    Dim EmployeeWorkingHoursCountdata As String = ""
    Dim WorkingDaysData As String = ""
    Dim SummaryViolationCountdata As String = ""
    Dim PermissionRequestCountdata As String = ""
    Dim LeaveRequestCountdata As String = ""

#End Region

    Private Property FromLogin() As Boolean
        Get
            Return ViewState("FromLogin")
        End Get
        Set(ByVal value As Boolean)
            ViewState("FromLogin") = value
        End Set
    End Property

    Public Property CountVisibleModules() As Integer
        Get
            Return ViewState("CountVisibleModules")
        End Get
        Set(ByVal value As Integer)
            ViewState("CountVisibleModules") = value
        End Set
    End Property

    Public Property VisibleModulesID() As Integer
        Get
            Return ViewState("VisibleModulesID")
        End Get
        Set(ByVal value As Integer)
            ViewState("VisibleModulesID") = value
        End Set
    End Property

#Region "Page Events"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        Else
            If SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                Page.MasterPageFile = "~/default/ArabicMaster.master"
                chartLang = "ar"
            Else
                Lang = CtlCommon.Lang.EN
                Page.MasterPageFile = "~/default/NewMaster.master"
                chartLang = "en"
            End If
            Page.UICulture = SessionVariables.CultureInfo
        End If

        Dim moduleID As Integer = SessionVariables.UserModuleId
        Dim objSysModules As New SYSModules
        With objSysModules
            .ModuleID = moduleID
            .GetByPK()

            If (SessionVariables.CultureInfo = "en-US") Then
                Page.Title = "Work Force-Pro " + .EnglishName
            Else
                Page.Title = "Work Force-Pro " + .ArabicName
            End If
        End With



    End Sub

    Private Sub DashBoard_Dashboard_StatusCount_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then


            IsLinkVisible(1)
            IsLinkVisible(2)
            IsLinkVisible(3)
            IsLinkVisible(4)
            IsLinkVisible(5)
            IsLinkVisible(6)
            IsLinkVisible(7)
            IsLinkVisible(8)
            IsLinkVisible(9)
            IsLinkVisible(10)
            IsLinkVisible(11)
            IsLinkVisible(12)
            IsLinkVisible(13)

            FromLogin = IIf(Page.Request.QueryString("FromLogin") Is Nothing, 0, Page.Request.QueryString("FromLogin"))
            UserSecurityFilter1.IsEntityPostBack = False
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                chartLang = "ar"
            Else
                Lang = CtlCommon.Lang.EN
                chartLang = "en"
            End If

            SelectDefaultPage()

            rdpFromDate.SelectedDate = Date.Today ' Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd As New Date
            dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)

            rdpToDate.SelectedDate = dd
            LoadCharts()
            FillEmployeeEntityInfo()
            FillInfo()
        End If


    End Sub

    Private Sub btnRetrive_Click(sender As Object, e As EventArgs) Handles btnRetrive.Click
        LoadCharts()
    End Sub

#End Region

#Region "Methods"

    Private Sub LoadCharts()
        FillStatusCountDash()
        FillRequestStatusCountDash()
        FillEmployeesWorkingHoursDash()
        FillWorkingDaysDash()
        FillCountHeaderDash()
        FillSummaryViolationCountDash()
        FillPermissionRequestCountDash()
        FillLeaveRequestCountDash()
    End Sub

    Private Sub FillLeaveRequestCountDash()
        objDashBoard = New DashBoard
        Dim dt As DataTable
        With objDashBoard

            If objversion.HasMultiCompany Then
                UserSecurityFilter1.GetEntity()
            End If
            If UserSecurityFilter1.CompanyId > 0 Then
                objDashBoard.FK_CompanyId = UserSecurityFilter1.CompanyId
            Else
                objDashBoard.FK_CompanyId = Nothing
            End If
            If UserSecurityFilter1.EntityId > 0 Then
                objDashBoard.FK_EntityId = UserSecurityFilter1.EntityId
            Else
                objDashBoard.FK_EntityId = Nothing
            End If
            If rdpFromDate.SelectedDate.HasValue Then
                objDashBoard.FromDate = rdpFromDate.SelectedDate
            Else
                rdpFromDate.SelectedDate = Date.Today
            End If

            If rdpToDate.SelectedDate.HasValue Then
                objDashBoard.ToDate = rdpToDate.SelectedDate
            Else
                rdpToDate.SelectedDate = Date.Today
            End If

            dt = .GetLeaveRequestCountDash

            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        LeaveRequestCountdata += "["
                        LeaveRequestCountdata += "'" + IIf(Lang = CtlCommon.Lang.AR, dt.Rows(i)("Status_Ar").ToString, dt.Rows(i)("Status_En").ToString) + "'"
                        LeaveRequestCountdata += ","
                        LeaveRequestCountdata += dt.Rows(i)("Status_Count").ToString
                        LeaveRequestCountdata += "],"
                    Next

                End If
            End If

        End With
        JSONLeaveRequestCountSeries.Text = "<script type='text/javascript' charset='utf-8'>var Var_LeaveRequestCountSeries =[" & LeaveRequestCountdata & "]</Script>"
        JSONLeaveRequestCountChartTitleText.Text = "<script type='text/javascript' charset='utf-8'>var Var_LeaveRequestCountChartTitleText = '" & IIf(Lang = CtlCommon.Lang.AR, " طلبات  <br> الاجازة ", "Leave <br> Request") & "'</Script>"

    End Sub

    Private Sub FillPermissionRequestCountDash()
        objDashBoard = New DashBoard
        Dim dt As DataTable
        With objDashBoard

            If objversion.HasMultiCompany Then
                UserSecurityFilter1.GetEntity()
            End If
            If UserSecurityFilter1.CompanyId > 0 Then
                objDashBoard.FK_CompanyId = UserSecurityFilter1.CompanyId
            Else
                objDashBoard.FK_CompanyId = Nothing
            End If
            If UserSecurityFilter1.EntityId > 0 Then
                objDashBoard.FK_EntityId = UserSecurityFilter1.EntityId
            Else
                objDashBoard.FK_EntityId = Nothing
            End If
            If rdpFromDate.SelectedDate.HasValue Then
                objDashBoard.FromDate = rdpFromDate.SelectedDate
            Else
                rdpFromDate.SelectedDate = Date.Today
            End If

            If rdpToDate.SelectedDate.HasValue Then
                objDashBoard.ToDate = rdpToDate.SelectedDate
            Else
                rdpToDate.SelectedDate = Date.Today
            End If

            dt = .GetPermissionRequestCountDash

            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        PermissionRequestCountdata += "["
                        PermissionRequestCountdata += "'" + IIf(Lang = CtlCommon.Lang.AR, dt.Rows(i)("Status_Ar").ToString, dt.Rows(i)("Status_En").ToString) + "'"
                        PermissionRequestCountdata += ","
                        PermissionRequestCountdata += dt.Rows(i)("Status_Count").ToString
                        PermissionRequestCountdata += "],"
                    Next

                End If
            End If

        End With
        JSONPermissionRequestCountSeries.Text = "<script type='text/javascript' charset='utf-8'>var Var_PermissionRequestCountSeries =[" & PermissionRequestCountdata & "]</Script>"
        JSONPermissionRequestCountChartTitleText.Text = "<script type='text/javascript' charset='utf-8'>var Var_PermissionRequestCountChartTitleText = '" & IIf(Lang = CtlCommon.Lang.AR, " طلبات  <br>  المغادرة ", "Permission <br> Request") & "'</Script>"

    End Sub

    Private Sub FillStatusCountDash()
        objDashBoard = New DashBoard
        Dim dt As DataTable
        With objDashBoard

            If objversion.HasMultiCompany Then
                UserSecurityFilter1.GetEntity()
            End If
            If UserSecurityFilter1.CompanyId > 0 Then
                objDashBoard.FK_CompanyId = UserSecurityFilter1.CompanyId
            Else
                objDashBoard.FK_CompanyId = Nothing
            End If
            If UserSecurityFilter1.EntityId > 0 Then
                objDashBoard.FK_EntityId = UserSecurityFilter1.EntityId
            Else
                objDashBoard.FK_EntityId = Nothing
            End If
            If rdpFromDate.SelectedDate.HasValue Then
                objDashBoard.FromDate = rdpFromDate.SelectedDate
            Else
                rdpFromDate.SelectedDate = Date.Today
            End If

            If rdpToDate.SelectedDate.HasValue Then
                objDashBoard.ToDate = rdpToDate.SelectedDate
            Else
                rdpToDate.SelectedDate = Date.Today
            End If

            dt = .GetSummaryCountDash

            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        StatusCountdata += "{name:   '"
                        StatusCountdata += IIf(Lang = CtlCommon.Lang.AR, dt.Rows(i)("StatusTextAr").ToString, dt.Rows(i)("StatusTextEn").ToString)
                        StatusCountdata += "',  y: "
                        StatusCountdata += dt.Rows(i)("StatusCount").ToString
                        StatusCountdata += "},"
                    Next

                End If
            End If

        End With
        JSONStatusCountSeries.Text = "<script type='text/javascript' charset='utf-8'>var Var_StatusCountSeries = [{name:   'Percentage',colorByPoint: true,data: [" & StatusCountdata & "]  }] </Script>"
        JSONStatusCountChartTitleText.Text = "<script type='text/javascript' charset='utf-8'>var Var_StatusCountChartTitleText = '" & IIf(Lang = CtlCommon.Lang.AR, "حالة حركات الموظفين", "Employees Transaction Status") & "'</Script>"

    End Sub

    Private Sub FillSummaryViolationCountDash()
        objDashBoard = New DashBoard
        Dim dt As DataTable
        With objDashBoard

            If objversion.HasMultiCompany Then
                UserSecurityFilter1.GetEntity()
            End If
            If UserSecurityFilter1.CompanyId > 0 Then
                objDashBoard.FK_CompanyId = UserSecurityFilter1.CompanyId
            Else
                objDashBoard.FK_CompanyId = Nothing
            End If
            If UserSecurityFilter1.EntityId > 0 Then
                objDashBoard.FK_EntityId = UserSecurityFilter1.EntityId
            Else
                objDashBoard.FK_EntityId = Nothing
            End If
            If rdpFromDate.SelectedDate.HasValue Then
                objDashBoard.FromDate = rdpFromDate.SelectedDate
            Else
                rdpFromDate.SelectedDate = Date.Today
            End If

            If rdpToDate.SelectedDate.HasValue Then
                objDashBoard.ToDate = rdpToDate.SelectedDate
            Else
                rdpToDate.SelectedDate = Date.Today
            End If

            dt = .GetSummaryViolationCountDash

            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        SummaryViolationCountdata += "["
                        SummaryViolationCountdata += "'" + IIf(Lang = CtlCommon.Lang.AR, dt.Rows(i)("StatusTextAr").ToString, dt.Rows(i)("StatusTextEn").ToString) + "'"
                        SummaryViolationCountdata += ","
                        SummaryViolationCountdata += dt.Rows(i)("StatusCount").ToString
                        SummaryViolationCountdata += "],"
                    Next

                End If
            End If

        End With
        JSONSummaryViolationCountSeries.Text = "<script type='text/javascript' charset='utf-8'>var Var_SummaryViolationCountSeries =[" & SummaryViolationCountdata & "]</Script>"
        JSONSummaryViolationCountChartTitleText.Text = "<script type='text/javascript' charset='utf-8'>var Var_SummaryViolationCountChartTitleText = '" & IIf(Lang = CtlCommon.Lang.AR, "مخالفات الموظفين", "Employees Violations") & "'</Script>"

    End Sub

    Private Sub FillCountHeaderDash()
        objDashBoard = New DashBoard
        Dim dt As DataTable
        With objDashBoard

            If objversion.HasMultiCompany Then
                UserSecurityFilter1.GetEntity()
            End If
            If UserSecurityFilter1.CompanyId > 0 Then
                objDashBoard.FK_CompanyId = UserSecurityFilter1.CompanyId
            Else
                objDashBoard.FK_CompanyId = Nothing
            End If
            If UserSecurityFilter1.EntityId > 0 Then
                objDashBoard.FK_EntityId = UserSecurityFilter1.EntityId
            Else
                objDashBoard.FK_EntityId = Nothing
            End If
            If rdpFromDate.SelectedDate.HasValue Then
                objDashBoard.FromDate = rdpFromDate.SelectedDate
            Else
                rdpFromDate.SelectedDate = Date.Today
            End If

            If rdpToDate.SelectedDate.HasValue Then
                objDashBoard.ToDate = rdpToDate.SelectedDate
            Else
                rdpToDate.SelectedDate = Date.Today
            End If

            dt = .GetStatusCountDash

            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    lblChkInVal.Text = dt.Rows(0)("StatusCount").ToString
                    lblChkInMaleVal.Text = dt.Rows(0)("Male_Count").ToString
                    lblChkInFemaleVal.Text = dt.Rows(0)("Female_Count").ToString


                    lblChkOutVal.Text = dt.Rows(1)("StatusCount").ToString
                    lblChkOutMaleVal.Text = dt.Rows(1)("Male_Count").ToString
                    lblChkOutFemaleVal.Text = dt.Rows(1)("Female_Count").ToString

                    lblAbsentVal.Text = dt.Rows(2)("StatusCount").ToString
                    lblAbsentMaleVal.Text = dt.Rows(2)("Male_Count").ToString
                    lblAbsentFemaleVal.Text = dt.Rows(2)("Female_Count").ToString

                    lblLeaveVal.Text = dt.Rows(3)("StatusCount").ToString
                    lblLeaveMaleVal.Text = dt.Rows(3)("Male_Count").ToString
                    lblLeaveFemaleVal.Text = dt.Rows(3)("Female_Count").ToString


                End If
            End If

        End With


    End Sub

    Private Sub FillRequestStatusCountDash()
        objDashBoard = New DashBoard
        Dim dt As DataTable
        With objDashBoard

            If objversion.HasMultiCompany Then
                UserSecurityFilter1.GetEntity()
            End If
            If UserSecurityFilter1.CompanyId > 0 Then
                objDashBoard.FK_CompanyId = UserSecurityFilter1.CompanyId
            Else
                objDashBoard.FK_CompanyId = Nothing
            End If
            If UserSecurityFilter1.EntityId > 0 Then
                objDashBoard.FK_EntityId = UserSecurityFilter1.EntityId
            Else
                objDashBoard.FK_EntityId = Nothing
            End If
            If rdpFromDate.SelectedDate.HasValue Then
                objDashBoard.FromDate = rdpFromDate.SelectedDate
            Else
                rdpFromDate.SelectedDate = Date.Today
            End If

            If rdpToDate.SelectedDate.HasValue Then
                objDashBoard.ToDate = rdpToDate.SelectedDate
            Else
                rdpToDate.SelectedDate = Date.Today
            End If

            dt = .GetRequestStatusCountDash

            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then

                    'lblLeaveRequestval.Text = dt.Rows(0)("StatusCount").ToString
                    'lblPermissionRequestVal.Text = dt.Rows(1)("StatusCount").ToString
                    'lblNursingPermissionRequestVal.Text = dt.Rows(2)("StatusCount").ToString
                    'lblStudyPermissionRequestVal.Text = dt.Rows(3)("StatusCount").ToString
                    'lblManualEntryRequestVal.Text = dt.Rows(4)("StatusCount").ToString
                    'lblUpdateTransactionRequestVal.Text = dt.Rows(5)("StatusCount").ToString


                    For i As Integer = 0 To dt.Rows.Count - 1
                        RequestStatusCountdata += "{name:   '"
                        RequestStatusCountdata += IIf(Lang = CtlCommon.Lang.AR, dt.Rows(i)("StatusTextAr").ToString, dt.Rows(i)("StatusTextEn").ToString)
                        RequestStatusCountdata += "',  y: "
                        RequestStatusCountdata += dt.Rows(i)("StatusCount").ToString
                        RequestStatusCountdata += "},"
                    Next
                End If
            End If

        End With

        JSONRequestStatusCountSeries.Text = "<script type='text/javascript' charset='utf-8'>var Var_RequestStatusCountSeries = [{name:   'Percentage',colorByPoint: true,data: [" & RequestStatusCountdata & "]  }] </Script>"
        JSONRequestStatusCountChartTitleText.Text = "<script type='text/javascript' charset='utf-8'>var Var_RequestStatusCountChartTitleText = '" & IIf(Lang = CtlCommon.Lang.AR, "طلبات الموظفين", "Employees Requests") & "'</Script>"

    End Sub

    Private Sub FillEmployeesWorkingHoursDash()
        objDashBoard = New DashBoard
        Dim dt As DataTable
        With objDashBoard

            If objversion.HasMultiCompany Then
                UserSecurityFilter1.GetEntity()
            End If
            If UserSecurityFilter1.CompanyId > 0 Then
                objDashBoard.FK_CompanyId = UserSecurityFilter1.CompanyId
            Else
                objDashBoard.FK_CompanyId = Nothing
            End If
            If UserSecurityFilter1.EntityId > 0 Then
                objDashBoard.FK_EntityId = UserSecurityFilter1.EntityId
            Else
                objDashBoard.FK_EntityId = Nothing
            End If
            If rdpFromDate.SelectedDate.HasValue Then
                objDashBoard.FromDate = rdpFromDate.SelectedDate
            Else
                rdpFromDate.SelectedDate = Date.Today
            End If

            If rdpToDate.SelectedDate.HasValue Then
                objDashBoard.ToDate = rdpToDate.SelectedDate
            Else
                rdpToDate.SelectedDate = Date.Today
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

        dateFromDate = Convert.ToDateTime(rdpFromDate.SelectedDate).ToString("dd/MM/yyyy")
        dateToDate = Convert.ToDateTime(rdpToDate.SelectedDate).ToString("dd/MM/yyyy")

        JSONEmployeeWorkingHoursCountChartDateText.Text = "<script type='text/javascript' charset='utf-8'>var Var_EmployeeWorkingHoursCountChartDateText = '" & dateFromDate & "-" & dateToDate & "'</Script>"

    End Sub

    Private Sub FillWorkingDaysDash()

        objDashBoard = New DashBoard
        Dim dt As DataTable
        With objDashBoard
            If objversion.HasMultiCompany Then
                UserSecurityFilter1.GetEntity()
            End If
            If UserSecurityFilter1.CompanyId > 0 Then
                objDashBoard.FK_CompanyId = UserSecurityFilter1.CompanyId
            Else
                objDashBoard.FK_CompanyId = Nothing
            End If
            If UserSecurityFilter1.EntityId > 0 Then
                objDashBoard.FK_EntityId = UserSecurityFilter1.EntityId
            Else
                objDashBoard.FK_EntityId = Nothing
            End If
            If rdpFromDate.SelectedDate.HasValue Then
                objDashBoard.FromDate = rdpFromDate.SelectedDate
            Else
                rdpFromDate.SelectedDate = Date.Today
            End If

            If rdpToDate.SelectedDate.HasValue Then
                objDashBoard.ToDate = rdpToDate.SelectedDate
            Else
                rdpToDate.SelectedDate = Date.Today
            End If

            dt = .GetTransaction_StatsDash

        End With

        If Not dt Is Nothing Then
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1

                    WorkingDaysData += "{"
                    WorkingDaysData += "name:"
                    WorkingDaysData += "'" + IIf(Lang = CtlCommon.Lang.AR, dt.Rows(i)("StatusAr"), dt.Rows(i)("StatusEn")) + "'" + ", data:["

                    Dim countj As Integer = 0
                    For j As Integer = 0 To dt.Rows.Count - 1
                        If IIf(Lang = CtlCommon.Lang.AR, dt.Rows(j)("StatusAr"), dt.Rows(j)("StatusEn")) = IIf(Lang = CtlCommon.Lang.AR, dt.Rows(i)("StatusAr"), dt.Rows(i)("StatusEn")) Then
                            WorkingDaysData += dt.Rows(j)("EmployeeCount").ToString + ","
                            countj += 1
                        End If
                    Next

                    WorkingDaysData += "]},"
                    i += countj
                Next
            End If
        End If

        JSONTransactionStatsSeries.Text = "<script type='text/javascript' charset='utf-8'>var Var_TransactionStatsSeries =[" & WorkingDaysData & "]</Script>"
        JSONTransactionStatsChartTitleText.Text = "<script type='text/javascript' charset='utf-8'>var Var_TransactionStatsChartTitleText ='" & IIf(Lang = CtlCommon.Lang.AR, "احصائية الحضور", "Attendance Statistics") & "'</Script>"
        JSONTransactionStatsChartSubTitleText.Text = "<script type='text/javascript' charset='utf-8'>var Var_TransactionStatsChartSubTitleText ='" & IIf(Lang = CtlCommon.Lang.AR, "العدد", "Number") & "'</Script>"


        JSONminMonth.Text = "<script type='text/javascript' charset='utf-8'>var Var_JSONminMonth =[" & Month(DateAdd(DateInterval.Month, -1, rdpFromDate.DbSelectedDate)) & "]</Script>"
        JSONminYear.Text = "<script type='text/javascript' charset='utf-8'>var Var_JSONminYear =[" & Year(DateAdd(DateInterval.Month, -1, rdpFromDate.DbSelectedDate)) & "]</Script>"
        JSONminDay.Text = "<script type='text/javascript' charset='utf-8'>var Var_JSONminDay =[" & Day(rdpFromDate.DbSelectedDate) & "]</Script>"



        JSONmaxMonth.Text = "<script type='text/javascript' charset='utf-8'>var Var_JSONmaxMonth =[" & Month(DateAdd(DateInterval.Month, -1, rdpToDate.DbSelectedDate)) & "]</Script>"
        JSONmaxYear.Text = "<script type='text/javascript' charset='utf-8'>var Var_JSONmaxYear =[" & Year(DateAdd(DateInterval.Month, -1, rdpToDate.DbSelectedDate)) & "]</Script>"
        JSONmaxDay.Text = "<script type='text/javascript' charset='utf-8'>var Var_JSONmaxDay =[" & Day(rdpToDate.DbSelectedDate) & "]</Script>"

    End Sub

    <WebMethod>
    Public Shared Function GetGroupUsers() As String
        Dim list As List(Of Dictionary(Of String, String)) = New List(Of Dictionary(Of String, String))
        Dim GroupName As Dictionary(Of String, String)
        Dim objJavaScriptSerializer As JavaScriptSerializer = New JavaScriptSerializer
        Dim dt As DataTable
        Dim Lang As String = SessionVariables.CultureInfo

        Dim _DefUsers As SYSUsers = New SYSUsers

        dt = _DefUsers.GetActive_UsersCount()

        For Each row As DataRow In dt.Rows
            GroupName = New Dictionary(Of String, String)
            GroupName.Add("GroupName", IIf(Lang = "ar-JO", row("Desc_Ar"), row("Desc_En")))
            GroupName.Add("UserCount", row("UserCount"))

            list.Add(GroupName)
        Next
        Return objJavaScriptSerializer.Serialize(list)
    End Function

    <WebMethod>
    Public Shared Function GetEmployees_Entities() As String
        Dim list As List(Of Dictionary(Of String, String)) = New List(Of Dictionary(Of String, String))
        Dim Employees As Dictionary(Of String, String)
        Dim objJavaScriptSerializer As JavaScriptSerializer = New JavaScriptSerializer
        Dim dt As DataTable
        Dim Lang As String = SessionVariables.CultureInfo

        Dim objEmployee As Employee = New Employee
        objEmployee.FK_CompanyId = SessionVariables.LoginUser.FK_CompanyId
        dt = objEmployee.GetActive_EmployeeCount

        For Each row As DataRow In dt.Rows
            Employees = New Dictionary(Of String, String)
            Employees.Add("EntityName", IIf(Lang = "ar-JO", row("EntityArabicName"), row("EntityName")))
            Employees.Add("EmployeeCount", row("EmployeeCount"))

            list.Add(Employees)
        Next
        Return objJavaScriptSerializer.Serialize(list)
    End Function

    Private Sub FillEmployeeEntityInfo()
        objEmployee = New Employee
        Dim dt As DataTable
        With objEmployee
            .FK_CompanyId = SessionVariables.LoginUser.FK_CompanyId
            dt = .GettEmployee_EntityCount
        End With
        lblEmployeeNoVal.Text = dt(0)("EmployeeCount")
        lblEntityNoVal.Text = dt(0)("EntityCount")

    End Sub

    Private Sub FillInfo()
        Dim dt As DataTable
        objSYSUsers = New SYSUsers
        With objSYSUsers
            dt = .GetUser_AndGroupCount
        End With
        lblUserNum.Text = dt(0)("ActiveUserCount").ToString
        lblNumberOfGroups.Text = dt(0)("GroupCount").ToString
    End Sub

    Private Sub rdpFromDate_SelectedDateChanged(sender As Object, e As SelectedDateChangedEventArgs) Handles rdpFromDate.SelectedDateChanged
        If rdpFromDate.SelectedDate > Date.Today Then
            dvPrediction.Visible = True
        Else
            dvPrediction.Visible = False
        End If
        LoadCharts()
    End Sub

    Private Sub SelectDefaultPage()
        objSYS_Users_Security = New SYS_Users_Security
        objSYSForms = New SYSForms


        Dim emp_ManagerID As String = SessionVariables.LoginUser.FK_EmployeeId

        objEmp_Managers = New Employee_Manager()
        Dim IsManager As Boolean
        With objEmp_Managers
            .FK_ManagerId = emp_ManagerID
            IsManager = .CheckIsManager()
        End With

        objEmp_DeputyManager = New Emp_DeputyManager()
        With objEmp_DeputyManager
            .FK_DeputyManagerId = emp_ManagerID
            objEmp_DeputyManager = .GetByDeputyManager()
        End With

        objAPP_Settings = New APP_Settings
        objAPP_Settings.GetByPK()
        objSYSGroups = New SYSGroups
        objSYSGroups.GroupId = SessionVariables.LoginUser.GroupId
        objSYSGroups.GetGroup()
        If FromLogin = True Then
            If IsManager = True Or Not objEmp_DeputyManager Is Nothing Then
                If objAPP_Settings.ManagerDefaultPage = 1 Then '---Groups Definition
                    If objSYSGroups.DefaultPage = 2 Then
                        'If CountVisibleModules = 1 Then
                        SessionVariables.UserModuleId = 8
                        Response.Redirect("../selfservices/EmployeeViolations.aspx")
                        'End If
                    ElseIf objSYSGroups.DefaultPage = 3 Then
                        Dim dt As DataTable
                        Dim path As String = "/Reports/SelfServices_Reports.aspx"
                        Dim words As String() = path.Split(New Char() {"/"c})
                        Dim toRemove = "/" + words(1)
                        Dim url As String = path.Replace(toRemove, "")
                        dt = objSYSForms.GetFormIDByFormPath(url, SessionVariables.LoginUser.GroupId)
                        If dt.Rows.Count > 0 Then
                            SessionVariables.UserModuleId = 8
                            Response.Redirect("../Reports/SelfServices_Reports.aspx")
                        Else
                            Response.Redirect("~/default/home.aspx")
                        End If

                    End If
                ElseIf objAPP_Settings.ManagerDefaultPage = 2 Then '---Manager Summary Page
                    SessionVariables.UserModuleId = 9
                    Response.Redirect("../SelfServices/Manager_Summary.aspx")
                Else
                    'If CountVisibleModules = 1 Then
                    SessionVariables.UserModuleId = 9
                    Response.Redirect("~/default/home.aspx")
                    'End If
                End If
            Else
                If objSYSGroups.DefaultPage = 2 Then
                    'If CountVisibleModules = 1 Then
                    SessionVariables.UserModuleId = 8
                    Response.Redirect("../selfservices/EmployeeViolations.aspx")
                    'End If
                ElseIf objSYSGroups.DefaultPage = 3 Then
                    Dim dt As DataTable
                    Dim path As String = "/Reports/SelfServices_Reports.aspx"
                    Dim words As String() = path.Split(New Char() {"/"c})
                    Dim toRemove = "/" + words(1)
                    Dim url As String = path.Replace(toRemove, "")
                    dt = objSYSForms.GetFormIDByFormPath(url, SessionVariables.LoginUser.GroupId)
                    If dt.Rows.Count > 0 Then
                        SessionVariables.UserModuleId = 8
                        Response.Redirect("../Reports/SelfServices_Reports.aspx")
                    Else
                        Response.Redirect("~/default/home.aspx")
                    End If
                End If
            End If
        Else
            If IsManager = True Or Not objEmp_DeputyManager Is Nothing Then
                If objAPP_Settings.ManagerDefaultPage = 1 Then '---Groups Definition
                    If objSYSGroups.DefaultPage = 2 Then
                        'If CountVisibleModules = 1 Then
                        SessionVariables.UserModuleId = 8
                        Response.Redirect("../selfservices/EmployeeViolations.aspx")
                        'End If
                    ElseIf objSYSGroups.DefaultPage = 3 Then
                        Dim dt As DataTable
                        Dim path As String = "/Reports/SelfServices_Reports.aspx"
                        Dim words As String() = path.Split(New Char() {"/"c})
                        Dim toRemove = "/" + words(1)
                        Dim url As String = path.Replace(toRemove, "")
                        dt = objSYSForms.GetFormIDByFormPath(url, SessionVariables.LoginUser.GroupId)
                        If dt.Rows.Count > 0 Then
                            SessionVariables.UserModuleId = 8
                            Response.Redirect("../Reports/SelfServices_Reports.aspx")
                        End If
                    End If
                ElseIf objAPP_Settings.ManagerDefaultPage = 2 Then '---Manager Summary Page
                    SessionVariables.UserModuleId = 9
                    Response.Redirect("../SelfServices/Manager_Summary.aspx")
                Else
                    Response.Redirect("../Requests/DM_EmployeeRequests.aspx")
                End If
            Else
                If objSYSGroups.DefaultPage = 2 Then
                    'If CountVisibleModules = 1 Then
                    SessionVariables.UserModuleId = 8
                    Response.Redirect("../selfservices/EmployeeViolations.aspx")
                    'End If
                ElseIf objSYSGroups.DefaultPage = 3 Then
                    Dim dt As DataTable
                    Dim path As String = "/Reports/SelfServices_Reports.aspx"
                    Dim words As String() = path.Split(New Char() {"/"c})
                    Dim toRemove = "/" + words(1)
                    Dim url As String = path.Replace(toRemove, "")
                    dt = objSYSForms.GetFormIDByFormPath(url, SessionVariables.LoginUser.GroupId)
                    If dt.Rows.Count > 0 Then
                        SessionVariables.UserModuleId = 8
                        Response.Redirect("../Reports/SelfServices_Reports.aspx")
                    End If
                End If
            End If
        End If
    End Sub

    Function IsLinkVisible(ByVal linkId As String) As Boolean
        Dim VersionType As Integer
        VersionType = SmartV.Version.version.GetVersionType()
        Dim LicModulesDT As DataTable
        Dim ModuleDT As DataTable
        Dim StrForms As String = SessionVariables.LicenseDetails.FormIds
        Dim BuildMenu As New SmartV.Security.MENU.SYSMenuCreator
        Dim blnIsVersionVisible As Boolean
        Dim blnWithAccessVisible As Boolean
        If SessionVariables.LoginUser Is Nothing Then
            Response.Redirect("~/default/Login.aspx")
        End If
        LicModulesDT = BuildMenu.GetModuleByFormId(StrForms)
        '----------Check if the form is in version-----------'
        ModuleDT = BuildMenu.BuildLeftMenu(SessionVariables.LoginUser.GroupId, 0, 0, 1, VersionType)
        For Each ModuleRow In ModuleDT.Rows
            For Each LicRow In LicModulesDT.Rows
                If ModuleRow("ModuleID") = LicRow("ModuleId") Then
                    blnIsVersionVisible = True
                End If
            Next
        Next
        '----------Check if the form is in version-----------'
        'Return False

        For Each ModuleRow In ModuleDT.Rows
            For Each LicRow In LicModulesDT.Rows
                If linkId = ModuleRow("ModuleID") Then '-----Check In Group Privilage
                    If linkId = LicRow("ModuleID") Then '-----Check In License Modules
                        blnWithAccessVisible = True
                        CountVisibleModules += 1
                        VisibleModulesID = ModuleRow("ModuleID")
                    End If
                End If
            Next
        Next
        ''
        If blnIsVersionVisible And blnWithAccessVisible Then
            Return True
        Else
            Return False
        End If

    End Function

#End Region

End Class
