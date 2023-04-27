Imports System.Collections.Generic
Imports System.Text
Imports System.Web
Imports System.Net
Imports System.Net.Mail
Imports SmartV.UTILITIES
Imports TA.Lookup
Imports SmartV.DB
Imports System.Data.SqlClient
Imports System.Data



Namespace TA.EmailNotifications
    Public Class DALEmailSender
#Region "Class Variables"
        Private errNo As Integer
        Private objDac As SmartV.DB.DAC = Nothing
        Private strConn As String
        Private Notifications_GetUnSentNotifications As String = "Notifications_GetUnSentNotifications"
        Private Notifications_Update_Send_Status As String = "Notifications_Update_Send_Status"
        Private Notifications_GetEmailConfiguration As String = "Notifications_GetEmailConfiguration"
#End Region

#Region "Constructor"
        Public Sub New()
        End Sub
#End Region

#Region "Methods"
        Public Function Send(ByVal EmailFrom As String, ByVal EmailTo As String, ByVal Subject As String, ByVal Body As String, ByVal SMTPServer As String, ByVal CC_Lits As String, ByVal Username As String, ByVal Password As String, ByRef SendError As String) As Boolean
            Try
                Dim Email As New MailMessage()
                Dim EmFrom As New MailAddress(EmailFrom, EmailFrom)
                Email.From = EmFrom
                If EmailTo.IndexOf(";") >= 0 Then
                    Dim EmailTolist As String()
                    EmailTolist = EmailTo.Split(";")


                    Dim i As Integer
                    For i = 0 To EmailTolist.Length - 1
                        If EmailTolist(i) <> "" Then
                            Email.[To].Add(EmailTolist(i))
                        End If
                    Next

                Else
                    Email.[To].Add(EmailTo)
                End If

                If CC_Lits <> "" Then
                    Email.CC.Add(CC_Lits)
                End If

                Email.Subject = Subject
                Email.Body = Body
                Email.IsBodyHtml = True
                Dim EmailClient As New SmtpClient(SMTPServer)
                If username.ToString() <> "" Then
                    EmailClient.Credentials = New System.Net.NetworkCredential(Username, Password)
                End If

                EmailClient.Send(Email)
                SendError = ""
                Return True
            Catch ex As Exception
                SendError = ex.ToString
                Return False
            End Try
        End Function

        Public Function GetAllNotificationsToSend() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Notifications_GetUnSentNotifications, Nothing)
            Catch ex As Exception
                errNo = -11
                'CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetEmailConfiguration() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Notifications_GetEmailConfiguration, Nothing)
            Catch ex As Exception
                errNo = -11
                'CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function UpdateSendStatus(ByVal NotificationId As Integer, ByVal NotificationType As String, ByVal Status As Boolean, ByVal sendError As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Notifications_Update_Send_Status, New SqlParameter("@NotificationId", NotificationId), _
               New SqlParameter("@NotificationType", "Email"), _
               New SqlParameter("@Status", Status), _
               New SqlParameter("@remarks", sendError))
            Catch ex As Exception
                errNo = -11
                'CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function
#End Region
    End Class
End Namespace

