Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup

Public Class DALNotification_Exception
    Inherits MGRBase



#Region "Class Variables"
    Private strConn As String
    Private Notification_Exception_Select As String = "Notification_Exception_select"
    Private Notification_Exception_Select_All As String = "Notification_Exception_select_All"
    Private Notification_Exception_Insert As String = "Notification_Exception_Insert"
    Private Notification_Exception_Update As String = "Notification_Exception_Update"
    Private Notification_Exception_Delete As String = "Notification_Exception_Delete"
    Private Notification_Exception_Select_AllInnerWithEmployee As String = "Notification_Exception_Select_AllInnerWithEmployee"
    Private Notification_Exception_Select_InnerWithEmployee As String = "Notification_Exception_Select_InnerWithEmployee" '---SELECT ONLY EMPLOYEEs

#End Region
#Region "Constructor"
    Public Sub New()



    End Sub

#End Region

#Region "Methods"

    Public Function Add(ByRef NotificationExceptionId As Integer, ByVal FK_EmployeeId As Long, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal Active As Boolean, ByVal Reason As String, ByVal CREATED_BY As String, ByVal FK_LogicalGroupId As Integer, ByVal FK_WorkLocationId As Integer, ByVal FK_EntityId As Integer) As Integer

        Dim sqlOut = New SqlParameter("@NotificationExceptionId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, NotificationExceptionId)
        objDac = DAC.getDAC()
        Try
            errNo = objDac.AddUpdateDeleteSPTrans(Notification_Exception_Insert, sqlOut, New SqlParameter("@FK_EmployeeId", IIf(FK_EmployeeId = 0, DBNull.Value, FK_EmployeeId)), _
           New SqlParameter("@FromDate", FromDate), _
           New SqlParameter("@ToDate", IIf(ToDate = Nothing, DBNull.Value, ToDate)), _
           New SqlParameter("@Active", Active), _
           New SqlParameter("@Reason", Reason), _
           New SqlParameter("@CREATED_BY", CREATED_BY), _
           New SqlParameter("@FK_LogicalGroupId", IIf(FK_LogicalGroupId = 0, DBNull.Value, FK_LogicalGroupId)), _
           New SqlParameter("@FK_WorkLocationId", IIf(FK_WorkLocationId = 0, DBNull.Value, FK_WorkLocationId)), _
           New SqlParameter("@FK_EntityId", IIf(FK_EntityId = 0, DBNull.Value, FK_EntityId)))
            If Not IsDBNull(sqlOut.Value) Then
                NotificationExceptionId = sqlOut.Value
            End If

        Catch ex As Exception
            errNo = -11
            CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
        End Try
        Return errNo

    End Function

    Public Function Update(ByVal NotificationExceptionId As Integer, ByVal FK_EmployeeId As Long, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal Reason As String, ByVal LAST_UPDATE_BY As String, ByVal FK_LogicalGroupId As Integer, ByVal FK_WorkLocationId As Integer, ByVal FK_EntityId As Integer) As Integer

        objDac = DAC.getDAC()
        Try
            errNo = objDac.AddUpdateDeleteSPTrans(Notification_Exception_Update, New SqlParameter("@NotificationExceptionId", NotificationExceptionId), _
           New SqlParameter("@FK_EmployeeId", IIf(FK_EmployeeId = 0, DBNull.Value, FK_EmployeeId)), _
           New SqlParameter("@FromDate", FromDate), _
           New SqlParameter("@ToDate", IIf(ToDate = Nothing, DBNull.Value, ToDate)), _
           New SqlParameter("@Reason", Reason), _
           New SqlParameter("@FK_LogicalGroupId", IIf(FK_LogicalGroupId = 0, DBNull.Value, FK_LogicalGroupId)), _
           New SqlParameter("@FK_WorkLocationId", IIf(FK_WorkLocationId = 0, DBNull.Value, FK_WorkLocationId)), _
           New SqlParameter("@FK_EntityId", IIf(FK_EntityId = 0, DBNull.Value, FK_EntityId)), _
           New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
        Catch ex As Exception
            errNo = -11
            CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
        End Try
        Return errNo

    End Function

    Public Function Delete(ByVal NotificationExceptionId As Integer) As Integer

        objDac = DAC.getDAC()
        Try
            errNo = objDac.AddUpdateDeleteSPTrans(Notification_Exception_Delete, New SqlParameter("@NotificationExceptionId", NotificationExceptionId))
        Catch ex As Exception
            errNo = -11
            CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
        End Try
        Return errNo

    End Function

    Public Function GetByPK(ByVal NotificationExceptionId As Integer) As DataRow

        objDac = DAC.getDAC()
        Dim objRow As DataRow
        Try
            objRow = objDac.GetDataTable(Notification_Exception_Select, New SqlParameter("@NotificationExceptionId", NotificationExceptionId)).Rows(0)
        Catch ex As Exception
            errNo = -11
            CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
        End Try
        Return objRow

    End Function

    Public Function GetAll() As DataTable

        objDac = DAC.getDAC()
        Dim objColl As DataTable
        Try
            objColl = objDac.GetDataTable(Notification_Exception_Select_All, Nothing)
        Catch ex As Exception
            errNo = -11
            CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
        End Try
        Return objColl


    End Function

    Public Function GetAllInnerEmployee() As DataTable

        objDac = DAC.getDAC()
        Dim objColl As DataTable
        Try
            objColl = objDac.GetDataTable(Notification_Exception_Select_AllInnerWithEmployee, Nothing)
        Catch ex As Exception
            errNo = -11
            CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
        End Try
        Return objColl

    End Function

    Public Function GetInnerEmployee() As DataTable

        objDac = DAC.getDAC()
        Dim objColl As DataTable
        Try
            objColl = objDac.GetDataTable(Notification_Exception_Select_InnerWithEmployee, Nothing)
        Catch ex As Exception
            errNo = -11
            CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
        End Try
        Return objColl

    End Function


#End Region


End Class
