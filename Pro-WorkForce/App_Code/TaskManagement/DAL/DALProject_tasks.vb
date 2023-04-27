Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.TaskManagement

    Public Class DALProject_tasks
        Inherits MGRBase

#Region "Class Variables"

        Private strConn As String
        Private Project_tasks_Select As String = "Project_tasks_select"
        Private Project_tasks_Select_All As String = "Project_tasks_select_All"
        Private Project_tasks_Insert As String = "Project_tasks_Insert"
        Private Project_tasks_Update As String = "Project_tasks_Update"
        Private Project_tasks_Delete As String = "Project_tasks_Delete"
        Private Project_tasks_Select_By_FK_ProjectId As String = "Project_tasks_Select_By_FK_ProjectId"
        Private Project_tasks_Select_Predecessors_By_FK_ProjectId As String = "Project_tasks_Select_Predecessors_By_FK_ProjectId"
        Private Project_tasks_Select_By_FK_ProjectId_Gantt As String = "Project_tasks_Select_By_FK_ProjectId_Gantt"
        Private Project_Tasks_Gantt_Dates As String = "Project_Tasks_Gantt_Dates"
        Private Project_tasks_Select_By_TaskId_Sequence As String = "Project_tasks_Select_By_TaskId_Sequence"
#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef TaskId As Integer, ByVal FK_ParentTaskId As Long, ByVal FK_ProjectId As Long, ByVal TaskSequence As String, ByVal Priority As Integer, ByVal TaskName As String, ByVal TaskDescription As String, ByVal PlannedStartDate As DateTime, ByVal PlannedEndDate As DateTime, ByVal ActualStartDate As DateTime, ByVal ActualEndDate As DateTime, ByVal Totalcompletionpercentage As Double, ByVal IsCompleted As Boolean, ByVal Approvedcompletionpercentage As Double, ByVal CREATED_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@TaskId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, TaskId)
                errNo = objDac.AddUpdateDeleteSPTrans(Project_tasks_Insert, sqlOut, New SqlParameter("@FK_ParentTaskId", IIf(FK_ParentTaskId = 0, Nothing, FK_ParentTaskId)), _
               New SqlParameter("@FK_ProjectId", FK_ProjectId), _
               New SqlParameter("@TaskSequence", TaskSequence), _
               New SqlParameter("@Priority", Priority), _
               New SqlParameter("@TaskName", TaskName), _
               New SqlParameter("@TaskDescription", TaskDescription), _
               New SqlParameter("@PlannedStartDate", PlannedStartDate), _
               New SqlParameter("@PlannedEndDate", PlannedEndDate), _
               New SqlParameter("@ActualStartDate", IIf(ActualStartDate = DateTime.MinValue, Nothing, ActualStartDate)), _
               New SqlParameter("@ActualEndDate", IIf(ActualEndDate = DateTime.MinValue, Nothing, ActualEndDate)), _
               New SqlParameter("@Totalcompletionpercentage", Totalcompletionpercentage), _
               New SqlParameter("@IsCompleted", IsCompleted), _
               New SqlParameter("@Approvedcompletionpercentage", Approvedcompletionpercentage), _
               New SqlParameter("@CREATED_BY", CREATED_BY))
                If Not IsDBNull(sqlOut.Value) Then
                    TaskId = sqlOut.Value
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal TaskId As Long, ByVal FK_ParentTaskId As Long, ByVal FK_ProjectId As Long, ByVal TaskSequence As String, ByVal Priority As Integer, ByVal TaskName As String, ByVal TaskDescription As String, ByVal PlannedStartDate As DateTime, ByVal PlannedEndDate As DateTime, ByVal ActualStartDate As DateTime, ByVal ActualEndDate As DateTime, ByVal Totalcompletionpercentage As Double, ByVal IsCompleted As Boolean, ByVal Approvedcompletionpercentage As Double, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Project_tasks_Update, New SqlParameter("@TaskId", TaskId), _
               New SqlParameter("@FK_ParentTaskId", IIf(FK_ParentTaskId = 0, Nothing, FK_ParentTaskId)), _
               New SqlParameter("@FK_ProjectId", FK_ProjectId), _
               New SqlParameter("@TaskSequence", TaskSequence), _
               New SqlParameter("@Priority", Priority), _
               New SqlParameter("@TaskName", TaskName), _
               New SqlParameter("@TaskDescription", TaskDescription), _
               New SqlParameter("@PlannedStartDate", PlannedStartDate), _
               New SqlParameter("@PlannedEndDate", PlannedEndDate), _
               New SqlParameter("@ActualStartDate", IIf(ActualStartDate = DateTime.MinValue, Nothing, ActualStartDate)), _
               New SqlParameter("@ActualEndDate", IIf(ActualEndDate = DateTime.MinValue, Nothing, ActualEndDate)), _
               New SqlParameter("@Totalcompletionpercentage", Totalcompletionpercentage), _
               New SqlParameter("@IsCompleted", IsCompleted), _
               New SqlParameter("@Approvedcompletionpercentage", Approvedcompletionpercentage), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal TaskId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Project_tasks_Delete, New SqlParameter("@TaskId", TaskId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal TaskId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Project_tasks_Select, New SqlParameter("@TaskId", TaskId)).Rows(0)
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
                objColl = objDac.GetDataTable(Project_tasks_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Get_by_FK_ProjectId(ByVal FK_ProjectId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Project_tasks_Select_By_FK_ProjectId, New SqlParameter("@FK_ProjectId", FK_ProjectId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Get_by_FK_ProjectId_Gantt(ByVal FK_ProjectId As Integer, ByVal Result_Lang As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Project_tasks_Select_By_FK_ProjectId_Gantt, New SqlParameter("@FK_ProjectId", FK_ProjectId),
New SqlParameter("@Lang", Result_Lang))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Get_Predecessors_by_FK_ProjectId(ByVal FK_ProjectId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Project_tasks_Select_Predecessors_By_FK_ProjectId, New SqlParameter("@FK_ProjectId", FK_ProjectId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Get_Calendar_Dates(ByVal FK_ProjectId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Project_Tasks_Gantt_Dates, New SqlParameter("@FK_ProjectId", FK_ProjectId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetByTaskId_Sequence(ByVal TaskId As Integer, ByVal TaskSequence As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Project_tasks_Select_By_TaskId_Sequence, New SqlParameter("@TaskId", TaskId), _
                                              New SqlParameter("@TaskSequence", TaskSequence))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region

    End Class
End Namespace