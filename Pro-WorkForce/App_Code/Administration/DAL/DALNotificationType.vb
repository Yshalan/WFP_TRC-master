Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports SmartV.DB
Imports TA.Lookup

Namespace TA.Admin

    Public Class DALNotificationType
        Inherits MGRBase


#Region "Class Variables"

        Private strConn As String
        Private Notification_Type_Select_All As String = "Notification_Type_Select_All"
        Private Notification_Type_Select_ByPK As String = "Notification_Type_Select_ByPK"
        Private Notification_Parameters_SelectForDDL_ByTypeID As String = "Notification_Parameters_SelectForDDL_ByTypeID"
        Private Notification_Type_Update As String = "Notification_Type_Update"
        Private Notification_Type_Select_BystrNotificationTypeId As String = "Notification_Type_Select_BystrNotificationTypeId"
        Private NotificationType_GetAdditional_Level_ManagerEmail As String = "NotificationType_GetAdditional_Level_ManagerEmail" '---Used In The Email Sender Service

#End Region

#Region "Constructor"
        Public Sub New()

        End Sub

#End Region

#Region "Methods"
        Public Function GetAll() As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Notification_Type_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetByPK(ByVal notificationTypeID As Integer) As DataRow
            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Notification_Type_Select_ByPK, New SqlParameter("@NotificationTypeID", notificationTypeID)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow
        End Function

        Public Function GetNotificationParamterForDDL(ByVal NotificationTypeID As Integer) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Notification_Parameters_SelectForDDL_ByTypeID, New SqlParameter("@NotificationTypeID", NotificationTypeID))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function Update(ByVal NotificationTypeId As Integer, ByVal HasEmail As Boolean, ByVal HasSMS As Boolean, ByVal EmailNotificationTemplateEn As String, ByVal EmailNotificationTemplateAr As String, ByVal SMSNotificationTemplateEn As String, ByVal SMSNotificationTemplateAr As String, ByVal SendToEmployee As Boolean, ByVal SendReportToManager As Boolean, ByVal SendToReportHR As Boolean, ByVal SendReportToDeputy As Boolean, ByVal IsSpecificTimeEmail As Boolean, ByVal SpecificTimeEmail As Integer, ByVal IsSpecificTimeSMS As Boolean, ByVal SpecificTimeSMS As Integer, ByVal NotificationPolicy As String, ByVal AdditionalApprovalLevel As Integer, ByVal SendReportToCoordinator As Boolean, ByVal CoordinatorType As String) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Notification_Type_Update,
               New SqlParameter("@NotificationTypeId", NotificationTypeId),
               New SqlParameter("@HasEmail", HasEmail),
               New SqlParameter("@HasSMS", HasSMS),
               New SqlParameter("@EmailNotificationTemplateEn", EmailNotificationTemplateEn),
               New SqlParameter("@EmailNotificationTemplateAr", EmailNotificationTemplateAr),
               New SqlParameter("@SMSNotificationTemplateEn", SMSNotificationTemplateEn),
               New SqlParameter("@SMSNotificationTemplateAr", SMSNotificationTemplateAr),
               New SqlParameter("@SendToEmployee", SendToEmployee),
               New SqlParameter("@SendReportToManager", SendReportToManager),
               New SqlParameter("@SendToReportHR", SendToReportHR),
               New SqlParameter("@SendReportToDeputy", SendReportToDeputy),
               New SqlParameter("@IsSpecificTimeEmail", IsSpecificTimeEmail),
               New SqlParameter("@SpecificTimeEmail", IIf(SpecificTimeEmail = 0, Nothing, SpecificTimeEmail)),
               New SqlParameter("@IsSpecificTimeSMS", IsSpecificTimeSMS),
               New SqlParameter("@SpecificTimeSMS", IIf(SpecificTimeSMS = 0, Nothing, SpecificTimeSMS)),
               New SqlParameter("@NotificationPolicy", IIf(NotificationPolicy = 0 And NotificationTypeId <> 2, Nothing, NotificationPolicy)),
               New SqlParameter("@AdditionalApprovalLevel", AdditionalApprovalLevel),
               New SqlParameter("@SendReportToCoordinator", SendReportToCoordinator),
               New SqlParameter("@CoordinatorType", CoordinatorType))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Public Function GetBystrNotificationTypeId(ByVal strNotificationTypeId As String) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Notification_Type_Select_BystrNotificationTypeId, New SqlParameter("@strNotificationTypeId", strNotificationTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

#End Region

    End Class

End Namespace
