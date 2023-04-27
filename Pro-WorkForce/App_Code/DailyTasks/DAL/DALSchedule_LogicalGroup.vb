Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.DailyTasks

    Public Class DALSchedule_LogicalGroup
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Schedule_LogicalGroup_Select As String = "Schedule_LogicalGroup_select"
        Private Schedule_LogicalGroup_Select_All As String = "Schedule_LogicalGroup_select_All"
        Private Schedule_LogicalGroup_Insert As String = "Schedule_LogicalGroup_Insert"
        Private Schedule_LogicalGroup_Update As String = "Schedule_LogicalGroup_Update"
        Private Schedule_LogicalGroup_Delete As String = "Schedule_LogicalGroup_Delete"
        Private Emp_WorkSchedule_Assign_Logical_Group As String = "Emp_WorkSchedule_Assign_Logical_Group"
        Private WorkSchedule_Get_LogicalGroup_Details As String = "WorkSchedule_Get_LogicalGroup_Details"

#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef EmpWorkScheduleId As Integer, ByVal FK_LogicalGroupId As Integer, ByVal FK_ScheduleId As Integer, ByVal ScheduleType As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal IsTemporary As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@EmpWorkScheduleId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, EmpWorkScheduleId)
                errNo = objDac.AddUpdateDeleteSPTrans(Schedule_LogicalGroup_Insert, sqlOut, New SqlParameter("@FK_LogicalGroupId", FK_LogicalGroupId), _
               New SqlParameter("@FK_ScheduleId", FK_ScheduleId), _
               New SqlParameter("@ScheduleType", ScheduleType), _
               New SqlParameter("@FromDate", FromDate), _
               New SqlParameter("@ToDate", ToDate), _
               New SqlParameter("@IsTemporary", IsTemporary))
                EmpWorkScheduleId = sqlOut.Value
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function
        Public Function AssignSchedule_LogicalGroup(ByVal FK_LogicalGroupId As Integer, ByVal FK_ScheduleId As Integer, ByVal ScheduleType As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal IsTemporary As Boolean, ByVal CREATED_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_WorkSchedule_Assign_Logical_Group, New SqlParameter("@FK_LogicalGroupId", FK_LogicalGroupId), _
               New SqlParameter("@FK_ScheduleId", FK_ScheduleId), _
               New SqlParameter("@ScheduleType", ScheduleType), _
               New SqlParameter("@FromDate", FromDate), _
               New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, DBNull.Value, ToDate)), _
               New SqlParameter("@IsTemporary", IsTemporary), _
               New SqlParameter("@CREATED_BY", CREATED_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal EmpWorkScheduleId As Long, ByVal FK_LogicalGroupId As Integer, ByVal FK_ScheduleId As Integer, ByVal ScheduleType As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal IsTemporary As Boolean, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Schedule_LogicalGroup_Update, New SqlParameter("@EmpWorkScheduleId", EmpWorkScheduleId), _
               New SqlParameter("@FK_LogicalGroupId", FK_LogicalGroupId), _
               New SqlParameter("@FK_ScheduleId", FK_ScheduleId), _
               New SqlParameter("@ScheduleType", ScheduleType), _
               New SqlParameter("@FromDate", FromDate), _
               New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, DBNull.Value, ToDate)), _
               New SqlParameter("@IsTemporary", IsTemporary), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal EmpWorkScheduleId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Schedule_LogicalGroup_Delete, New SqlParameter("@EmpWorkScheduleId", EmpWorkScheduleId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal EmpWorkScheduleId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Schedule_LogicalGroup_Select, New SqlParameter("@EmpWorkScheduleId", EmpWorkScheduleId)).Rows(0)
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
                objColl = objDac.GetDataTable(Schedule_LogicalGroup_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
       
        Public Function Get_LogicalGroup_Schedule_details() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(WorkSchedule_Get_LogicalGroup_Details, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
#End Region


    End Class
End Namespace