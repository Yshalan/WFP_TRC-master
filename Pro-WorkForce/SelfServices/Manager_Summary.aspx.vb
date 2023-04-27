Imports TA.Employees
Imports System.Data
Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports TA.Lookup
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

Partial Class SelfServices_Manager_Summary
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Private objEmp_Leaves As Emp_Leaves
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Shared SortExepression As String
    Shared dtCurrent As DataTable
    Private objEmployeeViolations As EmployeeViolations
    Public CalendarLang As String


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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
        End If
        PageHeader1.HeaderText = ResourceManager.GetString("Manager_Summary")
    End Sub

#End Region

#Region "Calendar"

    '<WebMethod()> _
    'Public Shared Function GetManager_Calendar(ByVal CalStartDate As String, ByVal CalEndDate As String) As String
    '    Dim DetailsHeader As String
    '    Dim objJavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer
    '    Dim events = New List(Of Dictionary(Of String, String))
    '    Dim objEvent As Dictionary(Of String, String)


    '    Dim DT As DataTable
    '    Dim objEmployeeViolations As New EmployeeViolations

    '    objEmployeeViolations.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
    '    Dim FromDate As DateTime
    '    Dim ToDate As DateTime
    '    If CalStartDate Is Nothing Then
    '        FromDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
    '        Dim dd As New Date
    '        dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
    '        ToDate = dd
    '    Else
    '        Dim dateCalStartDate As DateTime = Convert.ToDateTime(CalStartDate)
    '        Dim dateCalEndDate As DateTime = Convert.ToDateTime(CalEndDate)
    '        FromDate = dateCalStartDate
    '        ToDate = dateCalEndDate
    '    End If


    '    objEmployeeViolations.FromDate = FromDate
    '    objEmployeeViolations.ToDate = ToDate
    '    DT = objEmployeeViolations.GetManagerCalendar


    '    For Each row As DataRow In DT.Rows
    '        objEvent = New Dictionary(Of String, String)
    '        Dim strFromDate As String = row("M_DATE").ToString
    '        Dim strToDate As String = row("M_DATE").ToString

    '        Dim dateFromDate As DateTime = Convert.ToDateTime(row("M_DATE").ToString).ToShortDateString
    '        Dim dateToDate As DateTime = Convert.ToDateTime(row("M_DATE").ToString).ToShortDateString

    '        Dim EventName As String = ""
    '        Dim EventStart As String
    '        Dim EventEnd As String
    '        Dim EventColor As String = ""
    '        Dim Stats_Count As String = row("Stats_Count").ToString

    '        Dim ApptType As Integer = Convert.ToInt32(row("ApptType").ToString)
    '        Dim TextEn As String = row("Cal_Subject").ToString
    '        Dim TextAr As String = row("Cal_SubjectAr").ToString

    '        Dim ApptName As String
    '        ApptName = IIf(SessionVariables.CultureInfo = "ar-JO", TextAr, TextEn)
    '        DetailsHeader = ApptName

    '        If ApptType = 1 Then '---Attendees
    '            EventName = String.Format("{0} ({1})", ApptName, Stats_Count)
    '        ElseIf ApptType = 2 Then '---Absent
    '            EventName = String.Format("{0} ({1})", ApptName, Stats_Count)
    '        ElseIf ApptType = 3 Then '---Missing In
    '            EventName = String.Format("{0} ({1})", ApptName, Stats_Count)
    '        ElseIf ApptType = 4 Then '---Missing Out
    '            EventName = String.Format("{0} ({1})", ApptName, Stats_Count)
    '        ElseIf ApptType = 5 Then '---Delay
    '            EventName = String.Format("{0} ({1})", ApptName, Stats_Count)
    '        ElseIf ApptType = 6 Then '---Early Out
    '            EventName = String.Format("{0} ({1})", ApptName, Stats_Count)
    '        ElseIf ApptType = 7 Then '---Permissions
    '            EventName = String.Format("{0} ({1})", ApptName, Stats_Count)
    '        ElseIf ApptType = 8 Then '---Leaves
    '            EventName = String.Format("{0} ({1})", ApptName, Stats_Count)
    '        ElseIf ApptType = 9 Then '---LeavesRequest
    '            EventName = String.Format("{0} ({1})", ApptName, Stats_Count)
    '        ElseIf ApptType = 10 Then '---PermissionsRequest
    '            EventName = String.Format("{0} ({1})", ApptName, Stats_Count)
    '        End If

    '        EventStart = dateFromDate.ToString("yyyy-MM-dd")
    '        EventEnd = dateToDate.ToString("yyyy-MM-dd")

    '        Select Case ApptType
    '            Case 1 'Attendees
    '                EventColor = "#4BC57A"
    '            Case 2 'Absent
    '                EventColor = "#9F0F19"
    '            Case 3 ' Missing In
    '                EventColor = "#978D75"
    '            Case 4 'Missing Out
    '                EventColor = "#978D75" '"#D5A92E"
    '            Case 5 'Delay
    '                EventColor = "#e26367"
    '            Case 6 'Early Out
    '                EventColor = "#8C2A8F"
    '            Case 7 'Permissions
    '                EventColor = "#D5A92E"
    '            Case 8 'Leaves
    '                EventColor = "#4167B0"
    '            Case 9 'LeavesRequest
    '                EventColor = "#71b4d4"
    '            Case 10 'LeavesRequest
    '                EventColor = "#dbc992"
    '        End Select


    '        objEvent = New Dictionary(Of String, String)
    '        objEvent.Add("type", ApptType)
    '        objEvent.Add("title", EventName)
    '        objEvent.Add("start", EventStart)
    '        objEvent.Add("end", EventEnd)
    '        objEvent.Add("color", EventColor)

    '        events.Add(objEvent)

    '    Next

    '    Return objJavaScriptSerializer.Serialize(events)
    'End Function

#End Region


End Class
