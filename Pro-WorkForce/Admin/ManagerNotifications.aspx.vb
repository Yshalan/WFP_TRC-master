Imports System.Data
Imports TA.Lookup
Imports TA.Definitions
Imports SmartV.DB
Imports SmartV.UTILITIES
Imports TA.Employees
Imports Telerik.Web.UI
Imports TA.Admin
Imports TA.Security
Imports System.Net
Imports System.Net.Mail

Partial Class Admin_ManagerNotifications
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Dim objEmployee_Manager As Employee_Manager
    Dim strManagerEmployees As New StringBuilder
    Dim objEmployee As Employee
    Dim objAppSettings As APP_Settings
    Dim objEmailManagerReport As EmailManagerReport
    Dim objEmailHRReport As EmailHRReport
    Dim objEmpHR As Emp_HR

#End Region

#Region "Properties"

    Public Property dtCurrentControl() As DataTable
        Get
            Return ViewState("dtCurrentControl")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrentControl") = value
        End Set
    End Property

    Public Property dtCurrent() As DataTable
        Get
            Return ViewState("dtCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
        End Set
    End Property

    Public Property EmployeeEmail() As String
        Get
            Return ViewState("EmployeeEmail")
        End Get
        Set(value As String)
            ViewState("EmployeeEmail") = value
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

            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            PageHeader1.HeaderText = ResourceManager.GetString("ManagerNotifications", CultureInfo)

            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If

            Me.dtpFromdate.SelectedDate = Today
            Me.dtpEndDate.SelectedDate = Today

        End If

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrentControl = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrentControl.Rows
            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not UpdatePanel1.FindControl(row("PrintBtnName")) Is Nothing Then
                        UpdatePanel1.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next
    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPrint.Click

        SendManagersReport()
        SendHRReport()

        objAppSettings = New APP_Settings()
        objEmployee_Manager = New Employee_Manager()
        objEmployee_Manager.FK_ManagerId = EmployeeFilter1.EmployeeId
        objEmployee_Manager.FromDate = dtpFromdate.SelectedDate
        objEmployee_Manager.ToDate = dtpEndDate.SelectedDate
        Dim dtManagerEmployee As DataTable = objEmployee_Manager.GetManagerInfoByManagerId()
        Dim tempNotificationTypeId As Integer
        Dim checkNotificationTypeId As Integer = 0

        For Each row As DataRow In dtManagerEmployee.Rows
            strManagerEmployees.AppendFormat("{0},", row("FK_EmployeeId").ToString())
        Next

        Dim dtManagerNotifications As DataTable = objEmployee_Manager.GetManagerNotifications(strManagerEmployees.ToString())

        Dim strHTMLBuild As String

        strHTMLBuild = "<table width='100%' border='1' cellpadding='1' class='ManagerNotificationTable' >"
        For Each notificationTypeRow As DataRow In dtManagerNotifications.Rows
            tempNotificationTypeId = Convert.ToInt32(notificationTypeRow("FK_NotificationTypeId"))
            If Not tempNotificationTypeId = checkNotificationTypeId Then
                If Not checkNotificationTypeId = 0 Then
                    strHTMLBuild += "<tr><td colspan='5'>&nbsp;</td></tr>"
                End If
                checkNotificationTypeId = tempNotificationTypeId
                strHTMLBuild += "<tr style='background-color:#00CC33'><td colspan='5' style='padding-top:10px'>"
                strHTMLBuild += "<Strong>" + IIf(Lang = CtlCommon.Lang.EN, notificationTypeRow("TypeNameEn").ToString(), notificationTypeRow("TypeNameAr").ToString()) + "</Strong><br/><br/>"
                strHTMLBuild += "</td></tr>"

                CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)

                strHTMLBuild += "<tr><td style='background-color:#FFFFCC'>"
                strHTMLBuild += "<Strong>" + ResourceManager.GetString("EmployeeNumber", CultureInfo) + "</Strong>"
                strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                strHTMLBuild += "<Strong>" + ResourceManager.GetString("EmployeeName", CultureInfo) + "</Strong>"
                strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                strHTMLBuild += "<Strong>" + ResourceManager.GetString("EmployeeArabicName", CultureInfo) + "</Strong>"
                If tempNotificationTypeId = 1 Then
                    strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                    strHTMLBuild += "<Strong>" + ResourceManager.GetString("DelayDate", CultureInfo) + "</Strong>"
                    strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                    strHTMLBuild += "<Strong>" + ResourceManager.GetString("DelayDuration", CultureInfo) + "</Strong>"
                ElseIf tempNotificationTypeId = 2 Then
                    strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                    strHTMLBuild += "<Strong>" + ResourceManager.GetString("EarlyOutDate", CultureInfo) + "</Strong>"
                    strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                    strHTMLBuild += "<Strong>" + ResourceManager.GetString("EarlyOutDuration", CultureInfo) + "</Strong>"
                ElseIf tempNotificationTypeId = 3 Then
                    strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                    strHTMLBuild += "<Strong>" + ResourceManager.GetString("AbsentDate", CultureInfo) + "</Strong>"
                ElseIf tempNotificationTypeId = 7 Then
                    strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                    strHTMLBuild += "<Strong>" + ResourceManager.GetString("PermissionDate", CultureInfo) + "</Strong>"
                    strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                    strHTMLBuild += "<Strong>" + ResourceManager.GetString("PermissionPeriod", CultureInfo) + "</Strong>"
                ElseIf tempNotificationTypeId = 10 Then
                    strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                    strHTMLBuild += "<Strong>" + ResourceManager.GetString("LeaveDate", CultureInfo) + "</Strong>"
                    strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                    strHTMLBuild += "<Strong>" + ResourceManager.GetString("LeavePeriod", CultureInfo) + "</Strong>"
                End If
                strHTMLBuild += "</td></tr>"

                For Each notificationRow As DataRow In dtManagerNotifications.Rows
                    If Convert.ToInt32(notificationRow("FK_NotificationTypeId")) = tempNotificationTypeId Then
                        strHTMLBuild += "<tr><td style='background-color:#F0F0F0'>"
                        strHTMLBuild += "" + notificationRow("EmployeeNo").ToString() + ""
                        strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                        strHTMLBuild += "" + notificationRow("EmployeeName").ToString() + ""
                        strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                        strHTMLBuild += "" + notificationRow("EmployeeArabicName").ToString() + ""
                        If tempNotificationTypeId = 1 Then 'Delay
                            strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                            strHTMLBuild += "" + notificationRow("P3").ToString() + ""
                            strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                            strHTMLBuild += "" + Convert.ToDateTime(notificationRow("P4")).ToString("HH:mm").ToString() + ""
                        ElseIf tempNotificationTypeId = 2 Then 'Early Out
                            strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                            strHTMLBuild += "" + notificationRow("P3").ToString() + ""
                            strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                            strHTMLBuild += "" + Convert.ToDateTime(notificationRow("P4")).ToString("HH:mm") + ""
                        ElseIf tempNotificationTypeId = 3 Then 'Absent
                            strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                            strHTMLBuild += "" + notificationRow("P3").ToString() + ""
                        ElseIf tempNotificationTypeId = 7 Then 'Permission Request
                            strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                            strHTMLBuild += "" + notificationRow("P3").ToString() + ""
                            strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                            strHTMLBuild += "" + notificationRow("P5").ToString() + ""
                        ElseIf tempNotificationTypeId = 10 Then 'Leave Request
                            strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                            strHTMLBuild += "" + notificationRow("P3").ToString() + ""
                            strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                            strHTMLBuild += "" + notificationRow("P4").ToString() + ""
                        End If
                        strHTMLBuild += "</td></tr>"
                    End If
                Next
            End If
        Next
        strHTMLBuild += "</table>"

        'To Be Deleted
        'divTableHTML.InnerHtml = strHTMLBuild

        objEmployee = New Employee()
        With objEmployee
            .EmployeeId = EmployeeFilter1.EmployeeId
            .GetByPK()
            EmployeeEmail = .Email
        End With

        If Not String.IsNullOrEmpty(EmployeeEmail) Then

            objAppSettings = objAppSettings.GetByPK()

            SendEmail(EmployeeEmail, objAppSettings.HRGroupEmail, "Attendance Report" + dtpFromdate.SelectedDate.Value.ToShortDateString() + "-" + _
                      dtpEndDate.SelectedDate.Value.ToShortDateString(), strHTMLBuild)
        Else
            'CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NoEmployeeEmail", CultureInfo))
        End If

    End Sub

#End Region

#Region "Page Methods"

    Private Function SendEmail(ByVal ToEmail As String, ByVal FromEmail As String, ByVal Subject As String, ByVal MessageText As String) As Boolean

        Try

            Dim mailTo As MailAddress = New MailAddress(ToEmail)
            Dim mailFrom As MailAddress = New MailAddress(FromEmail)

            Dim msg As MailMessage = New MailMessage(FromEmail, ToEmail)
            msg.IsBodyHtml = True
            msg.BodyEncoding = Encoding.UTF8
            msg.Subject = Subject
            msg.Body = MessageText

            Dim client As SmtpClient = New SmtpClient("localhost")
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network
            client.Credentials = New NetworkCredential("Fadi.H@aman-me.com", "12345")
            client.Timeout = 20000
            client.Send(msg)

            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Sub SendManagersReport()

        objAppSettings = New APP_Settings()
        objEmployee_Manager = New Employee_Manager()
        objEmailManagerReport = New EmailManagerReport()

        Dim dtManagerEmployees As DataTable
        Dim tempManagerId As String = String.Empty
        Dim tempNotificationTypeId As Integer
        Dim checkNotificationTypeId As Integer = 0
        Dim found As Boolean = False
        Dim dtEmailManager As DataTable

        Dim dtManagersInfo As DataTable = objEmployee_Manager.GetAll()

        For Each row As DataRow In dtManagersInfo.Rows
            If Not row("FK_ManagerId").ToString() = tempManagerId Then
                tempManagerId = row("FK_ManagerId").ToString()

                Dim query As String = "FK_ManagerId =" + row("FK_ManagerId").ToString()
                Dim dr() As DataRow
                dr = dtManagersInfo.Select(query)
                If dr IsNot Nothing AndAlso dr.Length > 0 Then
                    dtManagerEmployees = dr.CopyToDataTable()
                Else
                    dtManagerEmployees = Nothing
                End If

                If Not dtManagerEmployees Is Nothing Then
                    For Each empRow As DataRow In dtManagerEmployees.Rows
                        strManagerEmployees.AppendFormat("{0},", empRow("FK_EmployeeId"))
                    Next

                    Dim dtManagerNotifications As DataTable = objEmployee_Manager.GetManagerNotifications(strManagerEmployees.ToString())

                    Dim strHTMLBuild As String

                    strHTMLBuild = "<table width='100%' border='1' cellpadding='1' class='ManagerNotificationTable' >"
                    For Each notificationTypeRow As DataRow In dtManagerNotifications.Rows
                        tempNotificationTypeId = Convert.ToInt32(notificationTypeRow("FK_NotificationTypeId"))
                        If Not tempNotificationTypeId = checkNotificationTypeId Then
                            If Not checkNotificationTypeId = 0 Then
                                strHTMLBuild += "<tr><td colspan='5'>&nbsp;</td></tr>"
                            End If
                            checkNotificationTypeId = tempNotificationTypeId
                            strHTMLBuild += "<tr style='background-color:#00CC33'><td colspan='5' style='padding-top:10px'>"
                            strHTMLBuild += "<Strong>" + IIf(Lang = CtlCommon.Lang.EN, notificationTypeRow("TypeNameEn").ToString(), notificationTypeRow("TypeNameAr").ToString()) + "</Strong><br/><br/>"
                            strHTMLBuild += "</td></tr>"

                            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)

                            strHTMLBuild += "<tr><td style='background-color:#FFFFCC'>"
                            strHTMLBuild += "<Strong>" + ResourceManager.GetString("EmployeeNumber", CultureInfo) + "</Strong>"
                            strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                            strHTMLBuild += "<Strong>" + ResourceManager.GetString("EmployeeName", CultureInfo) + "</Strong>"
                            strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                            strHTMLBuild += "<Strong>" + ResourceManager.GetString("EmployeeArabicName", CultureInfo) + "</Strong>"
                            If tempNotificationTypeId = 1 Then
                                strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                                strHTMLBuild += "<Strong>" + ResourceManager.GetString("DelayDate", CultureInfo) + "</Strong>"
                                strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                                strHTMLBuild += "<Strong>" + ResourceManager.GetString("DelayDuration", CultureInfo) + "</Strong>"
                            ElseIf tempNotificationTypeId = 2 Then
                                strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                                strHTMLBuild += "<Strong>" + ResourceManager.GetString("EarlyOutDate", CultureInfo) + "</Strong>"
                                strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                                strHTMLBuild += "<Strong>" + ResourceManager.GetString("EarlyOutDuration", CultureInfo) + "</Strong>"
                            ElseIf tempNotificationTypeId = 3 Then
                                strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                                strHTMLBuild += "<Strong>" + ResourceManager.GetString("AbsentDate", CultureInfo) + "</Strong>"
                            ElseIf tempNotificationTypeId = 7 Then
                                strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                                strHTMLBuild += "<Strong>" + ResourceManager.GetString("PermissionDate", CultureInfo) + "</Strong>"
                                strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                                strHTMLBuild += "<Strong>" + ResourceManager.GetString("PermissionPeriod", CultureInfo) + "</Strong>"
                            ElseIf tempNotificationTypeId = 10 Then
                                strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                                strHTMLBuild += "<Strong>" + ResourceManager.GetString("LeaveDate", CultureInfo) + "</Strong>"
                                strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                                strHTMLBuild += "<Strong>" + ResourceManager.GetString("LeavePeriod", CultureInfo) + "</Strong>"
                            End If
                            strHTMLBuild += "</td></tr>"

                            For Each notificationRow As DataRow In dtManagerNotifications.Rows
                                If Convert.ToInt32(notificationRow("FK_NotificationTypeId")) = tempNotificationTypeId Then
                                    strHTMLBuild += "<tr><td style='background-color:#F0F0F0'>"
                                    strHTMLBuild += "" + notificationRow("EmployeeNo").ToString() + ""
                                    strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                                    strHTMLBuild += "" + notificationRow("EmployeeName").ToString() + ""
                                    strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                                    strHTMLBuild += "" + notificationRow("EmployeeArabicName").ToString() + ""
                                    If tempNotificationTypeId = 1 Then 'Delay
                                        strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                                        strHTMLBuild += "" + notificationRow("P3").ToString() + ""
                                        strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                                        strHTMLBuild += "" + Convert.ToDateTime(notificationRow("P4")).ToString("HH:mm").ToString() + ""
                                    ElseIf tempNotificationTypeId = 2 Then 'Early Out
                                        strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                                        strHTMLBuild += "" + notificationRow("P3").ToString() + ""
                                        strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                                        strHTMLBuild += "" + Convert.ToDateTime(notificationRow("P4")).ToString("HH:mm") + ""
                                    ElseIf tempNotificationTypeId = 3 Then 'Absent
                                        strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                                        strHTMLBuild += "" + notificationRow("P3").ToString() + ""
                                    ElseIf tempNotificationTypeId = 7 Then 'Permission Request
                                        strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                                        strHTMLBuild += "" + notificationRow("P3").ToString() + ""
                                        strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                                        strHTMLBuild += "" + notificationRow("P5").ToString() + ""
                                    ElseIf tempNotificationTypeId = 10 Then 'Leave Request
                                        strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                                        strHTMLBuild += "" + notificationRow("P3").ToString() + ""
                                        strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                                        strHTMLBuild += "" + notificationRow("P4").ToString() + ""
                                    End If
                                    strHTMLBuild += "</td></tr>"
                                End If
                            Next
                        End If
                    Next
                    strHTMLBuild += "</table>"

                    'To Be Deleted
                    'divTableHTML.InnerHtml = strHTMLBuild

                    objEmployee = New Employee()
                    With objEmployee
                        .EmployeeId = tempManagerId
                        .GetByPK()
                        EmployeeEmail = .Email
                    End With

                    If Not String.IsNullOrEmpty(EmployeeEmail) Then

                        objAppSettings = objAppSettings.GetByPK()

                        objEmailManagerReport.FK_ManagerId = tempManagerId
                        dtEmailManager = objEmailManagerReport.GetEmailManagerReportByManagerId()

                        If (Not dtEmailManager Is Nothing) AndAlso (Not dtEmailManager.Rows.Count = 0) Then

                            If objAppSettings.SendToManagerDaily Then
                                For Each emailRow As DataRow In dtEmailManager.Rows
                                    If Convert.ToDateTime(emailRow("SendDate")).ToShortDateString() = DateTime.Now.ToShortDateString() Then
                                        found = True
                                        Exit For
                                    End If
                                Next

                                If Not found Then
                                    If SendEmail(EmployeeEmail, objAppSettings.HRGroupEmail, "Attendance Report" + dtpFromdate.SelectedDate.Value.ToShortDateString() + "-" + _
                                                                      dtpEndDate.SelectedDate.Value.ToShortDateString(), strHTMLBuild) Then

                                        objEmailManagerReport = New EmailManagerReport()
                                        With objEmailManagerReport
                                            .FK_ManagerId = tempManagerId
                                            .ReportType = 1 'Daily
                                            .SendDate = DateTime.Now
                                            .Add()
                                        End With
                                    End If
                                End If
                            End If

                            If objAppSettings.SendToManagerWeekly Then
                                If DateTime.Now.DayOfWeek = DayOfWeek.Sunday Then
                                    For Each emailRow As DataRow In dtEmailManager.Rows
                                        If Convert.ToDateTime(emailRow("SendDate")).ToShortDateString() = DateTime.Now.ToShortDateString() Then
                                            found = True
                                            Exit For
                                        End If
                                    Next

                                    If Not found Then
                                        If SendEmail(EmployeeEmail, objAppSettings.HRGroupEmail, "Attendance Report" + dtpFromdate.SelectedDate.Value.ToShortDateString() + "-" + _
                                                                          dtpEndDate.SelectedDate.Value.ToShortDateString(), strHTMLBuild) Then

                                            objEmailManagerReport = New EmailManagerReport()
                                            With objEmailManagerReport
                                                .FK_ManagerId = tempManagerId
                                                .ReportType = 2 'Weekly
                                                .SendDate = DateTime.Now
                                                .Add()
                                            End With
                                        End If
                                    End If
                                End If
                            End If

                            If objAppSettings.SendToManagerMonthly Then
                                If DateTime.Now.Day = 1 Then
                                    For Each emailRow As DataRow In dtEmailManager.Rows
                                        If Convert.ToDateTime(emailRow("SendDate")).ToShortDateString() = DateTime.Now.ToShortDateString() Then
                                            found = True
                                            Exit For
                                        End If
                                    Next

                                    If Not found Then
                                        If SendEmail(EmployeeEmail, objAppSettings.HRGroupEmail, "Attendance Report" + dtpFromdate.SelectedDate.Value.ToShortDateString() + "-" + _
                                                                          dtpEndDate.SelectedDate.Value.ToShortDateString(), strHTMLBuild) Then

                                            objEmailManagerReport = New EmailManagerReport()
                                            With objEmailManagerReport
                                                .FK_ManagerId = tempManagerId
                                                .ReportType = 3 'Monthly
                                                .SendDate = DateTime.Now
                                                .Add()
                                            End With
                                        End If
                                    End If
                                End If
                            End If

                        Else
                            If objAppSettings.SendToManagerDaily Then

                                If SendEmail(EmployeeEmail, objAppSettings.HRGroupEmail, "Attendance Report" + dtpFromdate.SelectedDate.Value.ToShortDateString() + "-" + _
                                                                  dtpEndDate.SelectedDate.Value.ToShortDateString(), strHTMLBuild) Then

                                    objEmailManagerReport = New EmailManagerReport()
                                    With objEmailManagerReport
                                        .FK_ManagerId = tempManagerId
                                        .ReportType = 1 'Daily
                                        .SendDate = DateTime.Now
                                        .Add()
                                    End With
                                End If
                            End If


                            If objAppSettings.SendToManagerWeekly Then
                                If DateTime.Now.DayOfWeek = DayOfWeek.Sunday Then


                                    If SendEmail(EmployeeEmail, objAppSettings.HRGroupEmail, "Attendance Report" + dtpFromdate.SelectedDate.Value.ToShortDateString() + "-" + _
                                                                      dtpEndDate.SelectedDate.Value.ToShortDateString(), strHTMLBuild) Then

                                        objEmailManagerReport = New EmailManagerReport()
                                        With objEmailManagerReport
                                            .FK_ManagerId = tempManagerId
                                            .ReportType = 2 'Weekly
                                            .SendDate = DateTime.Now
                                            .Add()
                                        End With
                                    End If
                                End If
                            End If

                            If objAppSettings.SendToManagerMonthly Then
                                If DateTime.Now.Day = 1 Then
                                    If SendEmail(EmployeeEmail, objAppSettings.HRGroupEmail, "Attendance Report" + dtpFromdate.SelectedDate.Value.ToShortDateString() + "-" + _
                                                                      dtpEndDate.SelectedDate.Value.ToShortDateString(), strHTMLBuild) Then

                                        objEmailManagerReport = New EmailManagerReport()
                                        With objEmailManagerReport
                                            .FK_ManagerId = tempManagerId
                                            .ReportType = 3 'Monthly
                                            .SendDate = DateTime.Now
                                            .Add()
                                        End With
                                    End If
                                End If
                            End If
                        End If

                    Else
                        'CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NoEmployeeEmail", CultureInfo))
                    End If

                End If

            End If
        Next

    End Sub

    Private Sub SendHRReport()

        objEmpHR = New Emp_HR()
        objEmailHRReport = New EmailHRReport()
        objAppSettings = New APP_Settings()

        Dim tempHRId As String = String.Empty
        Dim tempNotificationTypeId As Integer
        Dim checkNotificationTypeId As Integer = 0
        Dim found As Boolean = False
        Dim dtEmailHR As DataTable

        Dim dtHRInfo As DataTable = objEmpHR.GetHREmployeeWithInnerEmployees()

        For Each row As DataRow In dtHRInfo.Rows
            If Not row("HREmployeeId").ToString() = tempHRId Then
                tempHRId = row("HREmployeeId").ToString()


                Dim dtHRNotifications As DataTable = objEmpHR.GetAllHRNotifications()

                Dim strHTMLBuild As String

                strHTMLBuild = "<table width='100%' border='1' cellpadding='1' class='ManagerNotificationTable' >"
                For Each notificationTypeRow As DataRow In dtHRNotifications.Rows
                    tempNotificationTypeId = Convert.ToInt32(notificationTypeRow("FK_NotificationTypeId"))
                    If Not tempNotificationTypeId = checkNotificationTypeId Then
                        If Not checkNotificationTypeId = 0 Then
                            strHTMLBuild += "<tr><td colspan='5'>&nbsp;</td></tr>"
                        End If
                        checkNotificationTypeId = tempNotificationTypeId
                        strHTMLBuild += "<tr style='background-color:#00CC33'><td colspan='5' style='padding-top:10px'>"
                        strHTMLBuild += "<Strong>" + IIf(Lang = CtlCommon.Lang.EN, notificationTypeRow("TypeNameEn").ToString(), notificationTypeRow("TypeNameAr").ToString()) + "</Strong><br/><br/>"
                        strHTMLBuild += "</td></tr>"

                        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)

                        strHTMLBuild += "<tr><td style='background-color:#FFFFCC'>"
                        strHTMLBuild += "<Strong>" + ResourceManager.GetString("EmployeeNumber", CultureInfo) + "</Strong>"
                        strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                        strHTMLBuild += "<Strong>" + ResourceManager.GetString("EmployeeName", CultureInfo) + "</Strong>"
                        strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                        strHTMLBuild += "<Strong>" + ResourceManager.GetString("EmployeeArabicName", CultureInfo) + "</Strong>"
                        If tempNotificationTypeId = 1 Then
                            strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                            strHTMLBuild += "<Strong>" + ResourceManager.GetString("DelayDate", CultureInfo) + "</Strong>"
                            strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                            strHTMLBuild += "<Strong>" + ResourceManager.GetString("DelayDuration", CultureInfo) + "</Strong>"
                        ElseIf tempNotificationTypeId = 2 Then
                            strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                            strHTMLBuild += "<Strong>" + ResourceManager.GetString("EarlyOutDate", CultureInfo) + "</Strong>"
                            strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                            strHTMLBuild += "<Strong>" + ResourceManager.GetString("EarlyOutDuration", CultureInfo) + "</Strong>"
                        ElseIf tempNotificationTypeId = 3 Then
                            strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                            strHTMLBuild += "<Strong>" + ResourceManager.GetString("AbsentDate", CultureInfo) + "</Strong>"
                        ElseIf tempNotificationTypeId = 7 Then
                            strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                            strHTMLBuild += "<Strong>" + ResourceManager.GetString("PermissionDate", CultureInfo) + "</Strong>"
                            strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                            strHTMLBuild += "<Strong>" + ResourceManager.GetString("PermissionPeriod", CultureInfo) + "</Strong>"
                        ElseIf tempNotificationTypeId = 10 Then
                            strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                            strHTMLBuild += "<Strong>" + ResourceManager.GetString("LeaveDate", CultureInfo) + "</Strong>"
                            strHTMLBuild += "</td><td style='background-color:#FFFFCC'>"
                            strHTMLBuild += "<Strong>" + ResourceManager.GetString("LeavePeriod", CultureInfo) + "</Strong>"
                        End If
                        strHTMLBuild += "</td></tr>"

                        For Each notificationRow As DataRow In dtHRNotifications.Rows
                            If Convert.ToInt32(notificationRow("FK_NotificationTypeId")) = tempNotificationTypeId Then
                                strHTMLBuild += "<tr><td style='background-color:#F0F0F0'>"
                                strHTMLBuild += "" + notificationRow("EmployeeNo").ToString() + ""
                                strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                                strHTMLBuild += "" + notificationRow("EmployeeName").ToString() + ""
                                strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                                strHTMLBuild += "" + notificationRow("EmployeeArabicName").ToString() + ""
                                If tempNotificationTypeId = 1 Then 'Delay
                                    strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                                    strHTMLBuild += "" + notificationRow("P3").ToString() + ""
                                    strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                                    strHTMLBuild += "" + Convert.ToDateTime(notificationRow("P4")).ToString("HH:mm").ToString() + ""
                                ElseIf tempNotificationTypeId = 2 Then 'Early Out
                                    strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                                    strHTMLBuild += "" + notificationRow("P3").ToString() + ""
                                    strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                                    strHTMLBuild += "" + Convert.ToDateTime(notificationRow("P4")).ToString("HH:mm") + ""
                                ElseIf tempNotificationTypeId = 3 Then 'Absent
                                    strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                                    strHTMLBuild += "" + notificationRow("P3").ToString() + ""
                                ElseIf tempNotificationTypeId = 7 Then 'Permission Request
                                    strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                                    strHTMLBuild += "" + notificationRow("P3").ToString() + ""
                                    strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                                    strHTMLBuild += "" + notificationRow("P5").ToString() + ""
                                ElseIf tempNotificationTypeId = 10 Then 'Leave Request
                                    strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                                    strHTMLBuild += "" + notificationRow("P3").ToString() + ""
                                    strHTMLBuild += "</td><td style='background-color:#F0F0F0'>"
                                    strHTMLBuild += "" + notificationRow("P4").ToString() + ""
                                End If
                                strHTMLBuild += "</td></tr>"
                            End If
                        Next
                    End If
                Next
                strHTMLBuild += "</table>"

                'To Be Deleted
                'divTableHTML.InnerHtml = strHTMLBuild

                If Not IsDBNull(row("Email")) Then
                    EmployeeEmail = row("Email").ToString()
                Else
                    EmployeeEmail = String.Empty
                End If

                If Not String.IsNullOrEmpty(EmployeeEmail) Then

                    objAppSettings = objAppSettings.GetByPK()

                    objEmailManagerReport.FK_ManagerId = tempHRId
                    dtEmailHR = objEmailManagerReport.GetEmailManagerReportByManagerId()

                    If (Not dtEmailHR Is Nothing) AndAlso (Not dtEmailHR.Rows.Count = 0) Then

                        If objAppSettings.SendToHRDaily Then
                            For Each emailRow As DataRow In dtEmailHR.Rows
                                If Convert.ToDateTime(emailRow("SendDate")).ToShortDateString() = DateTime.Now.ToShortDateString() Then
                                    found = True
                                    Exit For
                                End If
                            Next

                            If Not found Then
                                If SendEmail(EmployeeEmail, objAppSettings.HRGroupEmail, "Attendance Report" + dtpFromdate.SelectedDate.Value.ToShortDateString() + "-" + _
                                                                  dtpEndDate.SelectedDate.Value.ToShortDateString(), strHTMLBuild) Then

                                    objEmailHRReport = New EmailHRReport()
                                    With objEmailHRReport
                                        .ReportType = 1 'Daily
                                        .SendDate = DateTime.Now
                                        .Add()
                                    End With
                                End If
                            End If
                        End If

                        If objAppSettings.SendToHRWeekly Then
                            If DateTime.Now.DayOfWeek = DayOfWeek.Sunday Then
                                For Each emailRow As DataRow In dtEmailHR.Rows
                                    If Convert.ToDateTime(emailRow("SendDate")).ToShortDateString() = DateTime.Now.ToShortDateString() Then
                                        found = True
                                        Exit For
                                    End If
                                Next

                                If Not found Then
                                    If SendEmail(EmployeeEmail, objAppSettings.HRGroupEmail, "Attendance Report" + dtpFromdate.SelectedDate.Value.ToShortDateString() + "-" + _
                                                                      dtpEndDate.SelectedDate.Value.ToShortDateString(), strHTMLBuild) Then

                                        objEmailHRReport = New EmailHRReport()
                                        With objEmailHRReport
                                            .ReportType = 2 'Weekly
                                            .SendDate = DateTime.Now
                                            .Add()
                                        End With
                                    End If
                                End If
                            End If
                        End If

                        If objAppSettings.SendToHRMonthly Then
                            If DateTime.Now.Day = 1 Then
                                For Each emailRow As DataRow In dtEmailHR.Rows
                                    If Convert.ToDateTime(emailRow("SendDate")).ToShortDateString() = DateTime.Now.ToShortDateString() Then
                                        found = True
                                        Exit For
                                    End If
                                Next

                                If Not found Then
                                    If SendEmail(EmployeeEmail, objAppSettings.HRGroupEmail, "Attendance Report" + dtpFromdate.SelectedDate.Value.ToShortDateString() + "-" + _
                                                                      dtpEndDate.SelectedDate.Value.ToShortDateString(), strHTMLBuild) Then

                                        objEmailHRReport = New EmailHRReport()
                                        With objEmailHRReport
                                            .ReportType = 3 'Monthly
                                            .SendDate = DateTime.Now
                                            .Add()
                                        End With
                                    End If
                                End If
                            End If
                        End If

                    Else
                        If objAppSettings.SendToHRDaily Then
                            If SendEmail(EmployeeEmail, objAppSettings.HRGroupEmail, "Attendance Report" + dtpFromdate.SelectedDate.Value.ToShortDateString() + "-" + _
                                                              dtpEndDate.SelectedDate.Value.ToShortDateString(), strHTMLBuild) Then

                                objEmailHRReport = New EmailHRReport()
                                With objEmailHRReport
                                    .ReportType = 1 'Daily
                                    .SendDate = DateTime.Now
                                    .Add()
                                End With
                            End If
                        End If

                        If objAppSettings.SendToHRWeekly Then
                            If DateTime.Now.DayOfWeek = DayOfWeek.Sunday Then
                                If SendEmail(EmployeeEmail, objAppSettings.HRGroupEmail, "Attendance Report" + dtpFromdate.SelectedDate.Value.ToShortDateString() + "-" + _
                                                                  dtpEndDate.SelectedDate.Value.ToShortDateString(), strHTMLBuild) Then

                                    objEmailHRReport = New EmailHRReport()
                                    With objEmailHRReport
                                        .ReportType = 2 'Weekly
                                        .SendDate = DateTime.Now
                                        .Add()
                                    End With
                                End If
                            End If
                        End If

                        If objAppSettings.SendToHRMonthly Then
                            If DateTime.Now.Day = 1 Then
                                If SendEmail(EmployeeEmail, objAppSettings.HRGroupEmail, "Attendance Report" + dtpFromdate.SelectedDate.Value.ToShortDateString() + "-" + _
                                                                  dtpEndDate.SelectedDate.Value.ToShortDateString(), strHTMLBuild) Then

                                    objEmailHRReport = New EmailHRReport()
                                    With objEmailHRReport
                                        .ReportType = 3 'Monthly
                                        .SendDate = DateTime.Now
                                        .Add()
                                    End With
                                End If
                            End If
                        End If
                    End If

                Else
                    'CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NoEmployeeEmail", CultureInfo))
                End If

            End If

        Next

    End Sub

#End Region

End Class
