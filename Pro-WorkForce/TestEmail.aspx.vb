Imports SmartV.UTILITIES
Imports TA.Lookup
Imports SmartV.DB
Imports TA.EmailNotifications
Imports System.Data

Partial Class TestEmail
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim objEmail As New EmailSender()

            Dim dtConfiguration As New DataTable()
            Dim dtUsersList As New DataTable()

            dtConfiguration = objEmail.GetEmailConfiguration
            If dtConfiguration.Rows.Count > 0 Then
                Dim CanSend As Boolean = False
                If dtConfiguration.Rows(0)("EnableEmailService").ToString() = "0" Then
                    CanSend = False
                Else
                    CanSend = True
                End If

                If CanSend Then
                    'objEmail.EmailFrom = dtConfiguration.Rows(0)("EmailFrom").ToString()
                    'objEmail.SMTPServer = dtConfiguration.Rows(0)("SMTP_Server").ToString()


                    dtUsersList = objEmail.GetAllNotificationsToSend
                    If dtUsersList.Rows.Count > 0 Then
                        For i As Integer = 0 To dtUsersList.Rows.Count - 1
                            objEmail.EmailFrom = dtConfiguration.Rows(0)("EmailFrom").ToString()
                            objEmail.SMTPServer = dtConfiguration.Rows(0)("SMTP_Server").ToString()
                            objEmail.EmailTo = dtUsersList.Rows(i)("Email").ToString()



                            objEmail.Subject = dtUsersList.Rows(i)("Notification_Subject").ToString()
                            objEmail.Body = dtUsersList.Rows(i)("EmailNotificationTemplateEn").ToString()
                            If Not dtUsersList.Rows(i)("ManagerEmail") Is Nothing Then
                                If dtUsersList.Rows(i)("ManagerEmail").ToString() <> "" Then
                                    objEmail.EmailTo = objEmail.EmailTo + ";" + dtUsersList.Rows(i)("ManagerEmail").ToString()
                                End If
                            End If
                            If Not dtUsersList.Rows(i)("HREmails") Is Nothing Then
                                If dtUsersList.Rows(i)("HREmails").ToString() <> "" Then
                                    objEmail.EmailTo = objEmail.EmailTo + ";" + dtUsersList.Rows(i)("HREmails").ToString() + ";"

                                End If
                            End If
                            Dim Sendresult As Boolean
                            Dim senderror As String = ""
                            Sendresult = objEmail.Send(senderror, dtConfiguration.Rows(0)("SMTPuser").ToString(), dtConfiguration.Rows(0)("SMTPPassword").ToString())
                            Dim objEmail2 As New EmailSender()
                            objEmail2.NotificationId = dtUsersList.Rows(i)("NotificationId").ToString()
                            objEmail2.Notification = EmailSender.NotificationType.Email
                            objEmail2.Status = Sendresult
                            objEmail2.UpdateSendStatus(senderror)


                        Next
                    End If

                End If
            End If
        Catch ex As Exception
            'EventLog.WriteEntry(ex.Message)
        End Try
    End Sub
End Class
