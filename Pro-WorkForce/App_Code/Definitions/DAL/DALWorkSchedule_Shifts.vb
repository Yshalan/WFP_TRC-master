Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Definitions

    Public Class DALWorkSchedule_Shifts
        Inherits MGRBase

#Region "Class Variables"
        Private strConn As String
        Private WorkSchedule_Shifts_Select As String = "WorkSchedule_Shifts_select"
        Private WorkSchedule_Shifts_Select_All As String = "WorkSchedule_Shifts_select_All"
        Private WorkSchedule_Shifts_Insert As String = "WorkSchedule_Shifts_Insert"
        Private WorkSchedule_Shifts_Update As String = "WorkSchedule_Shifts_Update"
        Private WorkSchedule_Shifts_Delete As String = "WorkSchedule_Shifts_Delete"
        Private WorkSchedule_Shifts_SelectByFKScheduleID As String = "WorkSchedule_Shifts_SelectByFKScheduleID"
        Private WorkSchedule_Shifts_DeleteByFkSchedleID As String = "WorkSchedule_Shifts_DeleteByFkSchedleID"
        Private WorkSchedule_Shifts_DetailsByFKScheduleID As String = "WorkSchedule_Shifts_DetailsByFKScheduleID"
        Private GetShiftsByWorkScheduleId As String = "GetShiftsByWorkScheduleId"
        Private GetEmployeeLeaveDetails_JSON As String = "GetEmployeeLeaveDetails_JSON"
        Private GetShiftsByWorkScheduleIdforReports As String = "GetShiftsByWorkScheduleIdforReports"
#End Region


#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef ShiftId As Integer, ByVal FK_ScheduleId As Integer, ByVal ShiftCode As String, ByVal ShiftName As String, ByVal ShiftArabicName As String, ByVal FromTime1 As String, ByVal ToTime1 As String, ByVal FromTime2 As String, ByVal ToTime2 As String, ByVal Color As String, ByVal IsOffDay As Boolean, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime, ByVal FlexTime1 As Integer, ByVal FlexTime2 As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@ShiftId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, ShiftId)
                errNo = objDac.AddUpdateDeleteSPTrans(WorkSchedule_Shifts_Insert, sqlOut, New SqlParameter("@FK_ScheduleId", FK_ScheduleId), _
                  New SqlParameter("@ShiftCode", ShiftCode), _
                  New SqlParameter("@ShiftName", ShiftName), _
                  New SqlParameter("@ShiftArabicName", ShiftArabicName), _
                  New SqlParameter("@FromTime1", FromTime1), _
                  New SqlParameter("@ToTime1", ToTime1), _
                  New SqlParameter("@FromTime2", FromTime2), _
                  New SqlParameter("@ToTime2", ToTime2), _
                  New SqlParameter("@Color", Color), _
                  New SqlParameter("@IsOffDay", IsOffDay), _
                  New SqlParameter("@CREATED_BY", CREATED_BY), _
                  New SqlParameter("@CREATED_DATE", CREATED_DATE), _
                  New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
                  New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE), _
                  New SqlParameter("@FlexTime1", IIf(FlexTime1 = 0, DBNull.Value, FlexTime1)), _
                  New SqlParameter("@FlexTime2", IIf(FlexTime2 = 0, DBNull.Value, FlexTime2)))
                If errNo = 0 Then
                    ShiftId = sqlOut.Value
                End If

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal ShiftId As Integer, ByVal FK_ScheduleId As Integer, ByVal ShiftCode As String, ByVal ShiftName As String, ByVal ShiftArabicName As String, ByVal FromTime1 As String, ByVal ToTime1 As String, ByVal FromTime2 As String, ByVal ToTime2 As String, ByVal Color As String, ByVal IsOffDay As Boolean, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime, ByVal FlexTime1 As Integer, ByVal FlexTime2 As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(WorkSchedule_Shifts_Update, New SqlParameter("@ShiftId", ShiftId), _
                  New SqlParameter("@FK_ScheduleId", FK_ScheduleId), _
                  New SqlParameter("@ShiftCode", ShiftCode), _
                  New SqlParameter("@ShiftName", ShiftName), _
                  New SqlParameter("@ShiftArabicName", ShiftArabicName), _
                  New SqlParameter("@FromTime1", FromTime1), _
                  New SqlParameter("@ToTime1", ToTime1), _
                  New SqlParameter("@FromTime2", FromTime2), _
                  New SqlParameter("@ToTime2", ToTime2), _
                  New SqlParameter("@Color", Color), _
                  New SqlParameter("@IsOffDay", IsOffDay), _
                  New SqlParameter("@CREATED_BY", CREATED_BY), _
                  New SqlParameter("@CREATED_DATE", CREATED_DATE), _
                  New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
                  New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE), _
                  New SqlParameter("@FlexTime1", IIf(FlexTime1 = 0, DBNull.Value, FlexTime1)), _
                  New SqlParameter("@FlexTime2", IIf(FlexTime2 = 0, DBNull.Value, FlexTime2)))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal ShiftId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(WorkSchedule_Shifts_Delete, New SqlParameter("@ShiftId", ShiftId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal ShiftId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(WorkSchedule_Shifts_Select, New SqlParameter("@ShiftId", ShiftId)).Rows(0)
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
                objColl = objDac.GetDataTable(WorkSchedule_Shifts_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetShiftsByDate() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(WorkSchedule_Shifts_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetByFKScheduleID(ByVal FK_ScheduleId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(WorkSchedule_Shifts_SelectByFKScheduleID, New SqlParameter("@FK_ScheduleId", FK_ScheduleId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
       


        Public Function DeleteByFKSchedleID(ByVal FK_ScheduleId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(WorkSchedule_Shifts_DeleteByFkSchedleID, New SqlParameter("@FK_ScheduleId", FK_ScheduleId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function
        'Added by Kiran :Employee Leave Details JSON
        Public Function GetEmployeeLeaveDetails(ByVal year As Integer, ByVal month As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(GetEmployeeLeaveDetails_JSON,
                                              New SqlParameter("@year", year), _
                                             New SqlParameter("@month", month))      
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetShiftDetailsbyWorkScheduleId(ByVal FK_ScheduleId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(GetShiftsByWorkScheduleId, New SqlParameter("@FK_ScheduleId", FK_ScheduleId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetShiftDetailsbyWorkScheduleIdForReport(ByVal FK_ScheduleId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(GetShiftsByWorkScheduleIdforReports, New SqlParameter("@FK_ScheduleId", FK_ScheduleId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function


#End Region


    End Class
End Namespace