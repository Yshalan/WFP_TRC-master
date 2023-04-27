Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports SmartV.DB
Imports TA.Lookup

Namespace TA.Admin

    Public Class DALEmp_HR_NotificationsType
        Inherits MGRBase

#Region "Class Variables"
        Dim Emp_HR_Notification_Type_ByHREmpID As String = "Emp_HR_Notification_Type_ByHREmpID"
        Dim Emp_HR_Notification_Type_Insert As String = "Emp_HR_Notification_Type_Insert"
        Dim Emp_HR_Notification_Type_Update As String = "Emp_HR_Notification_Type_Update"
        Dim Emp_HR_Notification_Type_Delete As String = "Emp_HR_Notification_Type_Delete"
#End Region

#Region "Constructor"
        Public Sub New()

        End Sub

#End Region

#Region "Methods"
        Public Function GetByHREmpID(ByVal empHRID As Long) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_HR_Notification_Type_ByHREmpID, New SqlParameter("@HREmpID", empHRID))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try

            Return objColl
        End Function

        Public Function Add(ByVal FK_HREmployeeID As Long, ByVal FK_HRNotificationTypeID As Integer) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_HR_Notification_Type_Insert, _
               New SqlParameter("@FK_HR_EmployeeID", FK_HREmployeeID), _
               New SqlParameter("@FK_HR_NotificationTypeID", FK_HRNotificationTypeID))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Public Function Update(ByVal FK_HREmployeeID As Long, ByVal FK_HRNotificationTypeID As Integer) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_HR_Notification_Type_Update, _
               New SqlParameter("@FK_HR_EmployeeID", FK_HREmployeeID), _
               New SqlParameter("@FK_HR_NotificationTypeID", FK_HRNotificationTypeID))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Public Function Delete(ByVal FK_HREmployeeID As Long) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_HR_Notification_Type_Delete, _
               New SqlParameter("@FK_HR_EmployeeID", FK_HREmployeeID))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function
#End Region

    End Class

End Namespace
