Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Definitions

    Public Class DALWorkSchedule_Flexible
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private WorkSchedule_Flexible_Select As String = "WorkSchedule_Flexible_select"
        Private WorkSchedule_Flexible_Select_All As String = "WorkSchedule_Flexible_select_All"
        Private WorkSchedule_Flexible_Insert As String = "WorkSchedule_Flexible_Insert"
        Private WorkSchedule_Flexible_Update As String = "WorkSchedule_Flexible_Update"
        Private WorkSchedule_Flexible_Delete As String = "WorkSchedule_Flexible_Delete"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_ScheduleId As Integer, ByVal DayId As Integer, ByVal FromTime1 As String, ByVal FromTime2 As String, ByVal Duration1 As Integer, ByVal FromTime3 As String, ByVal FromTime4 As String, ByVal Duration2 As Integer, ByVal IsOffDay As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(WorkSchedule_Flexible_Insert, New SqlParameter("@FK_ScheduleId", FK_ScheduleId), _
               New SqlParameter("@DayId", DayId), _
               New SqlParameter("@FromTime1", FromTime1), _
               New SqlParameter("@FromTime2", FromTime2), _
               New SqlParameter("@Duration1", Duration1), _
               New SqlParameter("@FromTime3", FromTime3), _
               New SqlParameter("@FromTime4", FromTime4), _
               New SqlParameter("@Duration2", Duration2), _
               New SqlParameter("@IsOffDay", IsOffDay))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Add(ByVal xml As String, ByVal _ScheduleID As Integer) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans("WorkSchedule_Flexible_bulk_insert", New SqlParameter("@xml", xml), New SqlParameter("ScheduleID", _ScheduleID))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Public Function Update(ByVal FK_ScheduleId As Integer, ByVal DayId As Integer, ByVal FromTime1 As String, ByVal FromTime2 As String, ByVal Duration1 As Integer, ByVal FromTime3 As String, ByVal FromTime4 As String, ByVal Duration2 As Integer, ByVal IsOffDay As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(WorkSchedule_Flexible_Update, New SqlParameter("@FK_ScheduleId", FK_ScheduleId), _
               New SqlParameter("@DayId", DayId), _
               New SqlParameter("@FromTime1", FromTime1), _
               New SqlParameter("@FromTime2", FromTime2), _
               New SqlParameter("@Duration1", Duration1), _
               New SqlParameter("@FromTime3", FromTime3), _
               New SqlParameter("@FromTime4", FromTime4), _
               New SqlParameter("@Duration2", Duration2), _
               New SqlParameter("@IsOffDay", IsOffDay))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal FK_ScheduleId As Integer, ByVal DayId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(WorkSchedule_Flexible_Delete, New SqlParameter("@FK_ScheduleId", FK_ScheduleId), _
               New SqlParameter("@DayId", DayId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal FK_ScheduleId As Integer, ByVal DayId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(WorkSchedule_Flexible_Select, New SqlParameter("@FK_ScheduleId", FK_ScheduleId), _
               New SqlParameter("@DayId", DayId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetAll(ByVal FK_ScheduleId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(WorkSchedule_Flexible_Select_All, New SqlParameter("@FK_ScheduleId", FK_ScheduleId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace