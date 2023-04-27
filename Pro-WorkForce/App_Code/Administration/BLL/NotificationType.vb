Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Admin
    Public Class NotificationType

#Region "Class Variables"

        Private _NotificationTypeId As Integer
        Private _TypeNameEn As String
        Private _TypeNameAr As String
        Private _HasEmail As Boolean
        Private _HasSMS As Boolean
        Private _EmailNotificationTemplateEn As String
        Private _EmailNotificationTemplateAr As String
        Private _SMSNotificationTemplateEn As String
        Private _SMSNotificationTemplateAr As String
        Private _SendToEmployee As Boolean
        Private _SendReportToManager As Boolean
        Private _SendToReportHR As Boolean
        Private _SendReportToDeputy As Boolean
        Private _IsSpecificTimeEmail As Boolean
        Private _SpecificTimeEmail As Integer
        Private _IsSpecificTimeSMS As Boolean
        Private _SpecificTimeSMS As Integer
        Private _strNotificationTypeId As String
        Private _NotificationPolicy As String
        Private _AdditionalApprovalLevel As Integer
        Private _SendReportToCoordinator As Boolean
        Private _CoordinatorType As String
        Private objDALNotification As DALNotificationType

#End Region

#Region "Public Properties"

        Public Property NotificationTypeId() As Integer
            Get
                Return _NotificationTypeId
            End Get
            Set(ByVal value As Integer)
                _NotificationTypeId = value
            End Set
        End Property

        Public Property TypeNameEn() As String
            Get
                Return _TypeNameEn
            End Get
            Set(ByVal value As String)
                _TypeNameEn = value
            End Set
        End Property

        Public Property TypeNameAr() As String
            Get
                Return _TypeNameAr
            End Get
            Set(ByVal value As String)
                _TypeNameAr = value
            End Set
        End Property

        Public Property HasEmail() As Boolean
            Get
                Return _HasEmail
            End Get
            Set(ByVal value As Boolean)
                _HasEmail = value
            End Set
        End Property

        Public Property HasSMS() As Boolean
            Get
                Return _HasSMS
            End Get
            Set(ByVal value As Boolean)
                _HasSMS = value
            End Set
        End Property

        Public Property EmailNotificationTemplateEn() As String
            Get
                Return _EmailNotificationTemplateEn
            End Get
            Set(ByVal value As String)
                _EmailNotificationTemplateEn = value
            End Set
        End Property

        Public Property EmailNotificationTemplateAr() As String
            Get
                Return _EmailNotificationTemplateAr
            End Get
            Set(ByVal value As String)
                _EmailNotificationTemplateAr = value
            End Set
        End Property

        Public Property SMSNotificationTemplateEn() As String
            Get
                Return _SMSNotificationTemplateEn
            End Get
            Set(ByVal value As String)
                _SMSNotificationTemplateEn = value
            End Set
        End Property

        Public Property SMSNotificationTemplateAr() As String
            Get
                Return _SMSNotificationTemplateAr
            End Get
            Set(ByVal value As String)
                _SMSNotificationTemplateAr = value
            End Set
        End Property

        Public Property SendToEmployee() As Boolean
            Get
                Return _SendToEmployee
            End Get
            Set(ByVal value As Boolean)
                _SendToEmployee = value
            End Set
        End Property

        Public Property SendReportToManager() As Boolean
            Get
                Return _SendReportToManager
            End Get
            Set(ByVal value As Boolean)
                _SendReportToManager = value
            End Set
        End Property

        Public Property SendToReportHR() As Boolean
            Get
                Return _SendToReportHR
            End Get
            Set(ByVal value As Boolean)
                _SendToReportHR = value
            End Set
        End Property

        Public Property SendReportToDeputy() As Boolean
            Get
                Return _SendReportToDeputy
            End Get
            Set(ByVal value As Boolean)
                _SendReportToDeputy = value
            End Set
        End Property

        Public Property IsSpecificTimeEmail() As Boolean
            Get
                Return _IsSpecificTimeEmail
            End Get
            Set(ByVal value As Boolean)
                _IsSpecificTimeEmail = value
            End Set
        End Property

        Public Property SpecificTimeEmail() As Integer
            Get
                Return _SpecificTimeEmail
            End Get
            Set(ByVal value As Integer)
                _SpecificTimeEmail = value
            End Set
        End Property

        Public Property IsSpecificTimeSMS() As Boolean
            Get
                Return _IsSpecificTimeSMS
            End Get
            Set(ByVal value As Boolean)
                _IsSpecificTimeSMS = value
            End Set
        End Property

        Public Property SpecificTimeSMS() As Integer
            Get
                Return _SpecificTimeSMS
            End Get
            Set(ByVal value As Integer)
                _SpecificTimeSMS = value
            End Set
        End Property

        Public Property strNotificationTypeId() As String
            Get
                Return _strNotificationTypeId
            End Get
            Set(ByVal value As String)
                _strNotificationTypeId = value
            End Set
        End Property

        Public Property NotificationPolicy() As String
            Get
                Return _NotificationPolicy
            End Get
            Set(ByVal value As String)
                _NotificationPolicy = value
            End Set
        End Property

        Public Property AdditionalApprovalLevel() As Integer
            Get
                Return _AdditionalApprovalLevel
            End Get
            Set(ByVal value As Integer)
                _AdditionalApprovalLevel = value
            End Set
        End Property

        Public Property SendReportToCoordinator() As Boolean
            Get
                Return _SendReportToCoordinator
            End Get
            Set(ByVal value As Boolean)
                _SendReportToCoordinator = value
            End Set
        End Property
        Public Property CoordinatorType() As String
            Get
                Return _CoordinatorType
            End Get
            Set(ByVal value As String)
                _CoordinatorType = value
            End Set
        End Property
#End Region

#Region "Constructor"

        Public Sub New()
            objDALNotification = New DALNotificationType
        End Sub
#End Region

#Region "Methods"

        Public Function GetAll() As DataTable
            Return objDALNotification.GetAll()
        End Function

        Public Function GetByPK() As NotificationType
            Dim dr As DataRow
            dr = objDALNotification.GetByPK(_NotificationTypeId)

            If (Not IsDBNull(dr("TypeNameEn"))) Then
                _TypeNameEn = dr("TypeNameEn")
            End If

            If Not IsDBNull(dr("TypeNameAr")) Then
                _TypeNameAr = dr("TypeNameAr")
            End If

            If Not IsDBNull(dr("HasEmail")) Then
                _HasEmail = dr("HasEmail")
            End If

            If Not IsDBNull(dr("HasSMS")) Then
                _HasSMS = dr("HasSMS")
            End If

            If Not IsDBNull(dr("EmailNotificationTemplateEn")) Then
                _EmailNotificationTemplateEn = dr("EmailNotificationTemplateEn")
            End If

            If Not IsDBNull(dr("EmailNotificationTemplateAr")) Then
                _EmailNotificationTemplateAr = dr("EmailNotificationTemplateAr")
            End If

            If Not IsDBNull(dr("SMSNotificationTemplateEn")) Then
                _SMSNotificationTemplateEn = dr("SMSNotificationTemplateEn")
            End If

            If Not IsDBNull(dr("SMSNotificationTemplateAr")) Then
                _SMSNotificationTemplateAr = dr("SMSNotificationTemplateAr")
            End If

            If Not IsDBNull(dr("SendToEmployee")) Then
                _SendToEmployee = dr("SendToEmployee")
            End If

            If Not IsDBNull(dr("SendReportToManager")) Then
                _SendReportToManager = dr("SendReportToManager")
            End If

            If Not IsDBNull(dr("SendToReportHR")) Then
                _SendToReportHR = dr("SendToReportHR")
            End If

            If Not IsDBNull(dr("SendReportToDeputy")) Then
                _SendReportToDeputy = dr("SendReportToDeputy")
            End If

            If Not IsDBNull(dr("IsSpecificTimeEmail")) Then
                _IsSpecificTimeEmail = dr("IsSpecificTimeEmail")
            End If

            If Not IsDBNull(dr("SpecificTimeEmail")) Then
                _SpecificTimeEmail = dr("SpecificTimeEmail")
            End If

            If Not IsDBNull(dr("IsSpecificTimeSMS")) Then
                _IsSpecificTimeSMS = dr("IsSpecificTimeSMS")
            End If

            If Not IsDBNull(dr("SpecificTimeSMS")) Then
                _SpecificTimeSMS = dr("SpecificTimeSMS")
            End If

            If Not IsDBNull(dr("NotificationPolicy")) Then
                _NotificationPolicy = dr("NotificationPolicy")
            End If

            If Not IsDBNull(dr("AdditionalApprovalLevel")) Then
                _AdditionalApprovalLevel = dr("AdditionalApprovalLevel")
            End If

            If Not IsDBNull(dr("SendReportToCoordinator")) Then
                _SendReportToCoordinator = dr("SendReportToCoordinator")
            End If
            If Not IsDBNull(dr("CoordinatorType")) Then
                _CoordinatorType = dr("CoordinatorType")
            End If

        End Function

        Public Function GetNotificationParametersForDDL() As DataTable
            Return objDALNotification.GetNotificationParamterForDDL(_NotificationTypeId)
        End Function

        Public Function Update() As Integer
            Dim rslt As Integer = objDALNotification.Update(_NotificationTypeId, _HasEmail, _HasSMS, _EmailNotificationTemplateEn, _EmailNotificationTemplateAr, _SMSNotificationTemplateEn, _SMSNotificationTemplateAr, _SendToEmployee, _SendReportToManager, _SendToReportHR, _SendReportToDeputy, _IsSpecificTimeEmail, _SpecificTimeEmail, _IsSpecificTimeSMS, _SpecificTimeSMS, _NotificationPolicy, _AdditionalApprovalLevel, _SendReportToCoordinator, _CoordinatorType)
            App_EventsLog.Insert_ToEventLog("Edit", _NotificationTypeId, "Emp_HR_NotificationTypes", "Notification Type")
            Return rslt
        End Function

        Public Function GetBystrNotificationTypeId() As DataTable
            Return objDALNotification.GetBystrNotificationTypeId(_strNotificationTypeId)
        End Function

#End Region

    End Class

End Namespace
