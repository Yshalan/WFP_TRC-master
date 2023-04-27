Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.TaskManagement

    Public Class DALProject_Tasks_ResourcesWork
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Project_Tasks_ResourcesWork_Select As String = "Project_Tasks_ResourcesWork_select"
        Private Project_Tasks_ResourcesWork_Select_All As String = "Project_Tasks_ResourcesWork_select_All"
        Private Project_Tasks_ResourcesWork_Insert As String = "Project_Tasks_ResourcesWork_Insert"
        Private Project_Tasks_ResourcesWork_Update As String = "Project_Tasks_ResourcesWork_Update"
        Private Project_Tasks_ResourcesWork_Delete As String = "Project_Tasks_ResourcesWork_Delete"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef ResourceWorkId As Integer, ByVal FK_EmployeeId As Long, ByVal FK_TaskId As Long, ByVal StartDateTime As DateTime, ByVal EndDateTime As DateTime, ByVal completionpercentage As Double, ByVal Remarks As String) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@ResourceWorkId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, ResourceWorkId)
                errNo = objDac.AddUpdateDeleteSPTrans(Project_Tasks_ResourcesWork_Insert, sqlOut, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
               New SqlParameter("@FK_TaskId", FK_TaskId),
               New SqlParameter("@StartDateTime", StartDateTime),
               New SqlParameter("@EndDateTime", EndDateTime),
               New SqlParameter("@completionpercentage", completionpercentage),
               New SqlParameter("@Remarks", Remarks))
                If errNo = 0 Then ResourceWorkId = sqlOut.Value Else ResourceWorkId = 0
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal ResourceWorkId As Long, ByVal FK_EmployeeId As Long, ByVal FK_TaskId As Long, ByVal StartDateTime As DateTime, ByVal EndDateTime As DateTime, ByVal completionpercentage As Double, ByVal Remarks As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Project_Tasks_ResourcesWork_Update, New SqlParameter("@ResourceWorkId", ResourceWorkId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_TaskId", FK_TaskId), _
               New SqlParameter("@StartDateTime", StartDateTime), _
               New SqlParameter("@EndDateTime", EndDateTime), _
               New SqlParameter("@completionpercentage", completionpercentage), _
               New SqlParameter("@Remarks", Remarks))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal ResourceWorkId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Project_Tasks_ResourcesWork_Delete, New SqlParameter("@ResourceWorkId", ResourceWorkId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal ResourceWorkId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Project_Tasks_ResourcesWork_Select, New SqlParameter("@ResourceWorkId", ResourceWorkId)).Rows(0)
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
                objColl = objDac.GetDataTable(Project_Tasks_ResourcesWork_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function


        Public Function GetAllEmployeeTask(EmployeeId As Integer, TaskId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("Project_Tasks_ResourcesWork_Select_All_Employee", New SqlParameter("EmployeeId", EmployeeId), New SqlParameter("TaskId", TaskId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region

        Public Function GetForDDL(EmployeeId As Integer) As DataTable
            objDac = DAC.getDAC()
            Dim objRow As DataTable
            Try
                objRow = objDac.GetDataTable("Employee_Task_Select_All_ForDDL", New SqlParameter("@EmployeeId", EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow
        End Function

    End Class
End Namespace