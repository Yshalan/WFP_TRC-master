Imports System.Collections.Generic
Imports System.Text
Imports System.Web
Imports System.Net
Imports System.Net.Mail
Imports System.Data




Namespace TA.EmailNotifications
    Public Class EmailSender
#Region "Properites Variables"
        Private _EmailFrom As String
        Private _EmailTo As String
        Private _Subject As String = ""
        Private _Body As String = ""
        Private _SMTPServer As String = ""
        Private _CC_Lits As String = ""
        Private _NotificationId As Integer
        Private _Status As Boolean



        Private objDALEmailSender As DALEmailSender
#End Region

#Region "Properites"
        Public Property EmailFrom() As String
            Get
                Return _EmailFrom
            End Get
            Set(ByVal value As String)
                _EmailFrom = value
            End Set
        End Property

        Public Property EmailTo() As String
            Get
                Return _EmailTo
            End Get
            Set(ByVal value As String)
                _EmailTo = value
            End Set
        End Property

        Public Property Subject() As String
            Get
                Return _Subject
            End Get
            Set(ByVal value As String)
                _Subject = value
            End Set
        End Property

        Public Property Body() As String
            Get
                Return _Body
            End Get
            Set(ByVal value As String)
                _Body = value
            End Set
        End Property

        Public Property SMTPServer() As String
            Get
                Return _SMTPServer
            End Get
            Set(ByVal value As String)
                _SMTPServer = value
            End Set
        End Property

        Public Property CC_Lits() As String
            Get
                Return _CC_Lits
            End Get
            Set(ByVal value As String)
                _CC_Lits = value
            End Set
        End Property

        Public Property NotificationId() As Integer
            Get
                Return _NotificationId
            End Get
            Set(ByVal value As Integer)
                _NotificationId = value
            End Set
        End Property

        Public Property Status() As Boolean
            Get
                Return _Status
            End Get
            Set(ByVal value As Boolean)
                _Status = value
            End Set
        End Property

#End Region

        Public Enum NotificationType
            Email
            SMS
        End Enum
        Public Shared Notification As NotificationType

        Public Sub New()

            objDALEmailSender = New DALEmailSender()

        End Sub

#Region "SendEmail"

        Public Function Send(ByRef senderror As String, ByVal UserName As String, ByVal Password As String) As Boolean
            Return objDALEmailSender.Send(_EmailFrom, _EmailTo, _Subject, _Body, _SMTPServer, _CC_Lits, UserName, Password, senderror)
        End Function

        Public Function GetAllNotificationsToSend() As DataTable

            Return objDALEmailSender.GetAllNotificationsToSend()

        End Function

        Public Function GetEmailConfiguration() As DataTable

            Return objDALEmailSender.GetEmailConfiguration()

        End Function

        Public Function UpdateSendStatus(ByVal senderror As String) As Integer

            Return objDALEmailSender.UpdateSendStatus(_NotificationId, Notification, _Status, senderror)

        End Function
#End Region
    End Class
End Namespace