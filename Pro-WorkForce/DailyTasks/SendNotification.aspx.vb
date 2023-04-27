Imports TA.Employees
Imports SmartV.UTILITIES
Imports TA.LookUp
Imports System.Data
Imports TA.DailyTasks
Imports TA.Admin
Imports Telerik.Web.UI
Imports TA.Security
Imports System.IO
Imports System.Net.Mail
Imports System.Configuration

Partial Class Admin_SendNotification
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Dim Emp_Name As String = ""
    Dim Emp_Id As Integer
    Protected dir As String
#End Region

    Public Property EmployeeName() As String
        Get
            Return ViewState("Emp_Name")
        End Get
        Set(ByVal value As String)
            ViewState("Emp_Name") = value
        End Set
    End Property

    Public Property EmployeeID() As String
        Get
            Return ViewState("Emp_Id")
        End Get
        Set(ByVal value As String)
            ViewState("Emp_Id") = value
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
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")

            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            PageHeader1.HeaderText = ResourceManager.GetString("SendNotification", CultureInfo)
            FillDeductType()
        End If

    End Sub


#End Region

#Region "Methods"
    Private Sub FillDeductType()
        If SessionVariables.CultureInfo = "ar-JO" Then
            Dim item1 As New RadComboBoxItem
            Dim item2 As New RadComboBoxItem
            Dim item3 As New RadComboBoxItem
            Dim item4 As New RadComboBoxItem
            Dim item5 As New RadComboBoxItem
            Dim item6 As New RadComboBoxItem
            Dim item7 As New RadComboBoxItem
            item1.Value = -1
            item1.Text = "--الرجاء الاختيار--"
            ddlDeductType.Items.Add(item1)
            item2.Value = 1
            item2.Text = " أخذ إجازة سنوية بعد تجاوزه رصيد الإجازات"
            ddlDeductType.Items.Add(item2)
            item3.Value = 2
            item3.Text = "أخذ مغادرات خاصة بعد تجاوزه رصيد الإجازات"
            ddlDeductType.Items.Add(item3)
            item4.Value = 3
            item4.Text = "التغيب عن العمل دون إذن رسمي"
            ddlDeductType.Items.Add(item4)

        Else
            Dim item1 As New RadComboBoxItem
            Dim item2 As New RadComboBoxItem
            Dim item3 As New RadComboBoxItem
            Dim item4 As New RadComboBoxItem
            Dim item5 As New RadComboBoxItem
            Dim item6 As New RadComboBoxItem
            Dim item7 As New RadComboBoxItem
            item1.Value = -1
            item1.Text = "--Please Select--"
            ddlDeductType.Items.Add(item1)
            item2.Value = 1
            item2.Text = "Took leave after exceeding"
            ddlDeductType.Items.Add(item2)
            item3.Value = 2
            item3.Text = "Took special leave after exceeding"
            ddlDeductType.Items.Add(item3)
            item4.Value = 3
            item4.Text = "Deserves a penalty"
            ddlDeductType.Items.Add(item4)

        End If
    End Sub
#End Region


    Protected Sub btnSaveEmployee_Click(sender As Object, e As EventArgs) Handles btnSaveEmployee.Click

        'Dim url As String = "SendEmail.aspx"
        'Dim s As String = "window.open('" & url + "', 'popup_window', 'width=700,height=300,left=400,top=100,resizable=yes');"
        'ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)

        If ddlDeductType.SelectedValue = -1 Then
            CtlCommon.ShowMessage(Me.Page, "Please Select Deduct Type", "info")
        End If

        If dtpleavedate.SelectedDate.ToString() = String.Empty Then
            CtlCommon.ShowMessage(Me.Page, "Please Enter Leave Date", "info")
        End If

        If txtDeductedDaysNo.Text.ToString() = String.Empty Then
            CtlCommon.ShowMessage(Me.Page, "Please Enter Deducted Days", "info")
        End If

        If TextBox1.Text.ToString() = String.Empty Then
            CtlCommon.ShowMessage(Me.Page, "Please Enter Leave Reason", "info")
        End If

        If txtEmail.Text.ToString() = String.Empty Then
            CtlCommon.ShowMessage(Me.Page, "Please Enter Email Address", "info")
        End If

        If ValidateFields() Then
            SendEmail()
        End If

    End Sub


    Private Sub SendEmail()

        If Not EmployeeFilterUC.EmployeeId = 0 Then
            EmployeeName = EmployeeFilterUC.EmployeeName
            ' EmployeeFilterUC.EmployeeName.Split(" - ")(2).ToString()
            EmployeeID = EmployeeFilterUC.EmployeeId


            If Not txtEmail.Text.ToString() = String.Empty And txtEmail.Text.Contains(",") Then

                For index As Integer = 0 To txtEmail.Text.Split(",").Count() - 1

                    If IsEmail(txtEmail.Text.Split(",")(index)) Then

                        WriteEmail(index)

                    End If
                Next
                CtlCommon.ShowMessage(Me.Page, "Email Sent Successfully", "info")

            ElseIf Not txtEmail.Text.ToString() = String.Empty Then
                WriteEmail(-1)
                CtlCommon.ShowMessage(Me.Page, "Email Sent Successfully", "info")
            End If

        Else
            CtlCommon.ShowMessage(Me.Page, "Please Select Employee", "info")

        End If


    End Sub

    Private Function IsNumber(ByVal qty As String) As Boolean
        Dim objRegExp As New System.Text.RegularExpressions.Regex("^\d+$")
        Return objRegExp.Match(qty).Success
    End Function

    Function IsEmail(ByVal email As String) As Boolean
        Static emailExpression As New Regex("^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$")

        Return emailExpression.IsMatch(email)
    End Function

    Function ValidateFields() As Boolean

        If Not IsNumber(txtDeductedDaysNo.Text.ToString()) Then
            CtlCommon.ShowMessage(Me.Page, "Deducted Days should be a Number", "info")
        ElseIf Not IsNumber(TextBox2.Text.ToString()) Then
            CtlCommon.ShowMessage(Me.Page, "Hours of Departure should be a Number", "info")
        Else
            Return True
        End If

        Return False

    End Function
    Sub WriteEmail(index As Integer)

        Dim Port As String = ConfigurationManager.AppSettings.Get("Port").ToString()
        Dim SenderEmailID As String = ConfigurationManager.AppSettings.Get("SenderEmailID").ToString()
        Dim SenderEmailPassword As String = ConfigurationManager.AppSettings.Get("SenderEmailPassword").ToString()
        Dim SmtpServer_con As String = ConfigurationManager.AppSettings.Get("SmtpServer").ToString()
        Dim MailFrom As String = ConfigurationManager.AppSettings.Get("MailFrom").ToString()



        Dim objSendNoti As New SendNotification
        objSendNoti = New SendNotification
        Dim IntError As Integer = -1

        With objSendNoti
            .FK_Employee_Id = EmployeeID
            .Type_Of_deducted = ddlDeductType.SelectedValue
            .Leave_Date = dtpleavedate.SelectedDate.Value
            .Number_Of_deducted_days = txtDeductedDaysNo.Text.Trim()
            .deducted_Reason = TextBox1.Text.Trim()
            .Updated_by = SessionVariables.LoginUser.ID
            .Updated_Date = DateTime.Now

            IntError = .Add()
        End With

        Dim leavedate As DateTime = dtpleavedate.SelectedDate.Value
        Dim LeaveYear As Int16 = leavedate.Year

        Dim htmlstr As String = ""
        Dim mail As MailMessage = New MailMessage()
        Dim SmtpServer As SmtpClient = New SmtpClient(SmtpServer_con)
        SmtpServer.UseDefaultCredentials = False
        mail.From = New MailAddress(MailFrom)

        If index = -1 Then
            mail.To.Add(txtEmail.Text.ToString())
        Else
            mail.To.Add(txtEmail.Text.Split(",")(index).ToString())
        End If


        If ddlDeductType.SelectedValue = 1 Then
            If Lang = CtlCommon.Lang.AR Then
                '   htmlstr = $"<html dir='rtl' lang='ar'><body> <p>عزيزي  " + EmployeeName + ",</p> <p>  أرجو التكرم بالعمل على خصم " + txtDeductedDaysNo.Text +" من راتب السيد " + EmployeeName +  " وذلك نظراً لحصوله على إجازة بتاريخ " + dtpleavedate.SelectedDate +" علماً بأن رصيد إجازاته للعام " + LeaveYear.ToString +" هو صفر.</p></body></html>"
                  htmlstr = $"<html dir='rtl' lang='ar'><body> <p>  أرجو التكرم بالعمل على خصم " + txtDeductedDaysNo.Text +"ايام من راتب السيد  " + EmployeeName +  " وذلك نظراً لحصوله على إجازة بتاريخ " + dtpleavedate.SelectedDate +" علماً بأن رصيد إجازاته للعام " + LeaveYear.ToString +" هو صفر.</p></body></html>"
            Else
                 htmlstr = $"<html>
                      <body>
                      <p>Dear " + EmployeeName + " ,</p>
                      <p>I kindly request you to work on deducting the " + txtDeductedDaysNo.Text + " No. of days from the salary of Mr. " + EmployeeName + " because he took leave on " + dtpleavedate.SelectedDate + " noting that his leave balance for the year " + LeaveYear.ToString + " is zero.</p></body></html>"
            End If


            mail.Subject = "Notification"
            mail.Body = htmlstr
            mail.IsBodyHtml = True
            SmtpServer.Port = CType(Port, Integer)
            SmtpServer.Credentials = New System.Net.NetworkCredential(SenderEmailID, SenderEmailPassword)
            SmtpServer.EnableSsl = False
            SmtpServer.Send(mail)

        ElseIf ddlDeductType.SelectedValue = 2 Then
            If Lang = CtlCommon.Lang.AR Then
                '   htmlstr = $"<html dir='rtl' lang='ar'><body> <p>عزيزي  " + EmployeeName + ",</p> <p>  أرجو التكرم بالعمل على خصم " + txtDeductedDaysNo.Text +" من راتب السيد " + EmployeeName +  " وذلك نظراً لحصوله على مغادرات خاصة بلغت " + dtpleavedate.SelectedDate +" ، على أن يتم ترصيد ما تبقى من المغادرات علماً بأن رصيد إجازاته للعام " + LeaveYear.ToString +" هو صفر.</p></body></html>"
                    htmlstr = $"<html dir='rtl' lang='ar'><body> <p>  أرجو التكرم بالعمل على خصم " + txtDeductedDaysNo.Text +"ساعات من راتب السيد  " + EmployeeName +  " وذلك نظراً لحصوله على مغادرات خاصة بتاريخ " + dtpleavedate.SelectedDate +" ، على أن يتم ترصيد ما تبقى من المغادرات علماً بأن رصيد إجازاته للعام " + LeaveYear.ToString +" هو صفر.</p></body></html>"

            Else
                 htmlstr = $"<html>
                      <body>
                      <p>Dear Mr. " + EmployeeName + " ,</p>
                      <p>I kindly request you to work on deducting the " + txtDeductedDaysNo.Text + " No. of days from the salary of Mr. " + EmployeeName + " due to the fact that he received special departures amounting 
                         to " + TextBox2.Text + " hours, provided that the remainder Of the departures are credited,
                         noting that his leave balance for the year  " + LeaveYear.ToString + " is zero.</p></body></html>"
            End If


            mail.Subject = "Notification"
            mail.Body = htmlstr
            mail.IsBodyHtml = True
            SmtpServer.Port = CType(Port, Integer)
            SmtpServer.Credentials = New System.Net.NetworkCredential(SenderEmailID, SenderEmailPassword)
            SmtpServer.EnableSsl = False
            SmtpServer.Send(mail)

        Else
            If Lang = CtlCommon.Lang.AR Then
                '  htmlstr = $"<html dir='rtl' lang='ar'><body> <p>عزيزي  " + EmployeeName + ",</p> <p>  أرجو التكرم بالعمل على خصم " + txtDeductedDaysNo.Text +" من راتب السيد " + EmployeeName +  " وذلك نظراً لتغيبه عن العمل دون إذن رسمي بتاريخ " + dtpleavedate.SelectedDate + "</p></body></html>"
                  htmlstr = $"<html dir='rtl' lang='ar'><body><p>  أرجو التكرم بالعمل على خصم " + txtDeductedDaysNo.Text +"ايام من راتب السيد " + EmployeeName +  " وذلك نظراً لتغيبه عن العمل دون إذن رسمي بتاريخ " + dtpleavedate.SelectedDate + "</p></body></html>"


            Else
                  htmlstr = $"<html>
                      <body>
                      <p>Dear Mr. " + EmployeeName + " ,</p>
                      <p>I kindly request you to work on deducting the " + txtDeductedDaysNo.Text + " No. of days from the salary of Mr. " + EmployeeName + " due to his absence from work without official permission on  " + dtpleavedate.SelectedDate + ".</p></body></html>"
            End If


            mail.Subject = "Notification"
            mail.Body = htmlstr
            mail.IsBodyHtml = True
            SmtpServer.Port = CType(Port, Integer)
            SmtpServer.Credentials = New System.Net.NetworkCredential(SenderEmailID, SenderEmailPassword)
            SmtpServer.EnableSsl = False
            SmtpServer.Send(mail)
            'Response = "Email sent successfully";
        End If
    End Sub

End Class
