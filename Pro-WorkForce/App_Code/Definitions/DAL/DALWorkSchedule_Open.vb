Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Definitions

    Public Class DALWorkSchedule_Open
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private WorkSchedule_Open_Select As String = "WorkSchedule_Open_select"
        Private WorkSchedule_Open_Select_All As String = "WorkSchedule_Open_select_All"
        Private WorkSchedule_Open_Insert As String = "WorkSchedule_Open_Insert"
        Private WorkSchedule_Open_Update As String = "WorkSchedule_Open_Update"
        Private WorkSchedule_Open_Delete As String = "WorkSchedule_Open_Delete"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_ScheduleId As Integer, ByVal DayId As Integer, ByVal FromTime As String, ByVal FlexTime As Integer, ByVal ToTime As String, ByVal WorkHours As String, ByVal IsOffDay As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(WorkSchedule_Open_Insert, New SqlParameter("@FK_ScheduleId", FK_ScheduleId), _
               New SqlParameter("@DayId", DayId), _
               New SqlParameter("@FromTime", FromTime), _
               New SqlParameter("@FlexTime", FlexTime), _
               New SqlParameter("@ToTime", ToTime), _
               New SqlParameter("@WorkHours", WorkHours), _
               New SqlParameter("@IsOffDay", IsOffDay))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FK_ScheduleId As Integer, ByVal DayId As Integer, ByVal FromTime As String, ByVal FlexTime As Integer, ByVal ToTime As String, ByVal WorkHours As String, ByVal IsOffDay As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(WorkSchedule_Open_Update, New SqlParameter("@FK_ScheduleId", FK_ScheduleId), _
               New SqlParameter("@DayId", DayId), _
               New SqlParameter("@FromTime", FromTime), _
               New SqlParameter("@FlexTime", FlexTime), _
               New SqlParameter("@ToTime", ToTime), _
               New SqlParameter("@WorkHours", WorkHours), _
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
                errNo = objDac.AddUpdateDeleteSPTrans(WorkSchedule_Open_Delete, New SqlParameter("@FK_ScheduleId", FK_ScheduleId), _
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
                objRow = objDac.GetDataTable(WorkSchedule_Open_Select, New SqlParameter("@FK_ScheduleId", FK_ScheduleId), _
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
                objColl = objDac.GetDataTable(WorkSchedule_Open_Select_All, New SqlParameter("@FK_ScheduleId", FK_ScheduleId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace