Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.DailyTasks

    Public Class DALSendNotification
        Inherits MGRBase

#Region "Class Variables"
        Private strConn As String
        Private TRC_Manual_Notification_Insert As String = "TRC_Manual_Notification_Insert"
#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"
        Public Function Add(ByVal _FK_Employee_Id As Integer, ByVal _Type_Of_deducted As Integer, ByVal _Leave_Date As DateTime, ByVal _Number_Of_deducted_days As Integer, ByVal _deducted_Reason As String, ByVal _Updated_by As String, ByVal _Updated_Date As DateTime)

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(TRC_Manual_Notification_Insert, New SqlParameter("@FK_Employee_Id", _FK_Employee_Id),
           New SqlParameter("@Type_Of_deducted", _Type_Of_deducted),
           New SqlParameter("@Leave_Date", _Leave_Date),
           New SqlParameter("@Number_Of_deducted_days", _Number_Of_deducted_days),
           New SqlParameter("@deducted_Reason", _deducted_Reason),
           New SqlParameter("@Updated_by", _Updated_by),
           New SqlParameter("@Updated_Date", _Updated_Date))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

#End Region

    End Class
End Namespace