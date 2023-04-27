Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.DailyTasks

    Public Class DALSchedule_Company
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Schedule_Company_Select As String = "Schedule_Company_select"
        Private Schedule_Company_Select_All As String = "Schedule_Company_select_All"
        Private Schedule_Company_Insert As String = "Schedule_Company_Insert"
        Private Schedule_Company_Update As String = "Schedule_Company_Update"
        Private Schedule_Company_Delete As String = "Schedule_Company_Delete"
        Private Emp_WorkSchedule_Assign_Company As String = "Emp_WorkSchedule_Assign_Company"
        Private WorkSchedule_Get_Company_Details As String = "WorkSchedule_Get_Company_Details"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef EmpWorkScheduleId As Integer, ByVal FK_CompanyId As Integer, ByVal FK_ScheduleId As Integer, ByVal ScheduleType As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal IsTemporary As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@EmpWorkScheduleId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, EmpWorkScheduleId)
                errNo = objDac.AddUpdateDeleteSPTrans(Schedule_Company_Insert, sqlOut, New SqlParameter("@FK_CompanyId", FK_CompanyId), _
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
        Public Function AssignSchedule_Company(ByVal FK_CompanyId As Long, ByVal FK_ScheduleId As Integer, ByVal ScheduleType As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal IsTemporary As Boolean, ByVal CREATED_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_WorkSchedule_Assign_Company, New SqlParameter("@CompanyId", FK_CompanyId), _
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

        Public Function Update(ByVal EmpWorkScheduleId As Long, ByVal FK_CompanyId As Integer, ByVal FK_ScheduleId As Integer, ByVal ScheduleType As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal IsTemporary As Boolean, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Schedule_Company_Update, New SqlParameter("@EmpWorkScheduleId", EmpWorkScheduleId), _
               New SqlParameter("@FK_CompanyId", FK_CompanyId), _
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
                errNo = objDac.AddUpdateDeleteSPTrans(Schedule_Company_Delete, New SqlParameter("@EmpWorkScheduleId", EmpWorkScheduleId))
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
                objRow = objDac.GetDataTable(Schedule_Company_Select, New SqlParameter("@EmpWorkScheduleId", EmpWorkScheduleId)).Rows(0)
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
                objColl = objDac.GetDataTable(Schedule_Company_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
       
        Public Function Get_Company_Schedule_Details() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(WorkSchedule_Get_Company_Details, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
#End Region


    End Class
End Namespace