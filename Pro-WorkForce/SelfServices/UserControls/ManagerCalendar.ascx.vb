Imports System.Data
Imports System.Web.Services
Imports TA.SelfServices
Imports SmartV.UTILITIES

Partial Class SelfServices_UserControls_ManagerCalendar
    Inherits System.Web.UI.UserControl

    Public CalendarLang As String
    Private objEmployeeViolations As EmployeeViolations

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        ElseIf SessionVariables.CultureInfo = "ar-JO" Then
            CalendarLang = "ar"
        Else
            CalendarLang = "en"
        End If
    End Sub

    Protected Sub SelfServices_UserControls_ManagerCalendar_Load(sender As Object, e As EventArgs) Handles Me.Load
    End Sub

    <WebMethod()> _
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

            Dim dateToDate As DateTime
            If Not row("M_DATE") Is DBNull.Value Then
                dateToDate = Convert.ToDateTime(row("M_DATE").ToString).ToShortDateString
            Else
                dateToDate = dateFromDate
            End If
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
                EventName = ApptName
                String.Format("{0} ({1})", ApptName, Stats_Count)
            ElseIf ApptType = 2 Then '---Absent
                String.Format("{0} ({1})", ApptName, Stats_Count)
            ElseIf ApptType = 3 Then '---Missing In
                String.Format("{0} ({1})", ApptName, Stats_Count)
            ElseIf ApptType = 4 Then '---Missing Out
                String.Format("{0} ({1})", ApptName, Stats_Count)
            ElseIf ApptType = 5 Then '---Delay
                String.Format("{0} ({1})", ApptName, Stats_Count)
            ElseIf ApptType = 6 Then '---Early Out
                String.Format("{0} ({1})", ApptName, Stats_Count)
            ElseIf ApptType = 7 Then '---Permissions
                String.Format("{0} ({1})", ApptName, Stats_Count)
            ElseIf ApptType = 8 Then '---Leaves
                String.Format("{0} ({1})", ApptName, Stats_Count)
            End If

            EventStart = Convert.ToDateTime(strFromDate)
            EventEnd = Convert.ToDateTime(strToDate)

            Select Case ApptType
                Case 1 'Attendees
                    EventColor = "#4167B0"
                Case 2 'Absent
                    EventColor = "#D5A92E"
                Case 3 ' Missing In
                    EventColor = "#8C2A8F"
                Case 4 'Missing Out
                    EventColor = "#978D75"
                Case 5 'Delay
                    EventColor = "#4BC57A"
                Case 6 'Early Out
                    EventColor = "#4BC57A"
                Case 7 'Permissions
                    EventColor = "#9F0F19"
                Case 8 'Leaves
                    EventColor = "#e26367"
            End Select


            objEvent = New Dictionary(Of String, String)
            objEvent.Add("title", EventName)
            objEvent.Add("start", EventStart)
            objEvent.Add("end", EventEnd)
            objEvent.Add("color", EventColor)

            events.Add(objEvent)

        Next

        Return objJavaScriptSerializer.Serialize(events)
    End Function


End Class
