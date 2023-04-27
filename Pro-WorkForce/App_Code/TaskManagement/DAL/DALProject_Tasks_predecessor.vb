Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.TaskManagement

    Public Class DALProject_Tasks_predecessor
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Project_Tasks_predecessor_Select As String = "Project_Tasks_predecessor_select"
        Private Project_Tasks_predecessor_Select_All As String = "Project_Tasks_predecessor_select_All"
        Private Project_Tasks_predecessor_Insert As String = "Project_Tasks_predecessor_Insert"
        Private Project_Tasks_predecessor_Update As String = "Project_Tasks_predecessor_Update"
        Private Project_Tasks_predecessor_Delete As String = "Project_Tasks_predecessor_Delete"
        Private Project_Tasks_predecessor_Select_By_TaskId As String = "Project_Tasks_predecessor_Select_By_TaskId"
#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_TaskId As Long, ByVal FK_predecessorTask As Long, ByVal RelationType As String, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Project_Tasks_predecessor_Insert, New SqlParameter("@FK_TaskId", FK_TaskId), _
               New SqlParameter("@FK_predecessorTask", FK_predecessorTask), _
               New SqlParameter("@RelationType", RelationType), _
               New SqlParameter("@CREATED_BY", CREATED_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FK_TaskId As Long, ByVal FK_predecessorTask As Long, ByVal RelationType As String, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Project_Tasks_predecessor_Update, New SqlParameter("@FK_TaskId", FK_TaskId), _
               New SqlParameter("@FK_predecessorTask", FK_predecessorTask), _
               New SqlParameter("@RelationType", RelationType), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@CREATED_DATE", CREATED_DATE), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
               New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal FK_TaskId As Long, ByVal FK_predecessorTask As Long, ByVal RelationType As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Project_Tasks_predecessor_Delete, New SqlParameter("@FK_TaskId", FK_TaskId), _
               New SqlParameter("@FK_predecessorTask", FK_predecessorTask), _
               New SqlParameter("@RelationType", RelationType))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal FK_TaskId As Long, ByVal FK_predecessorTask As Long, ByVal RelationType As String) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Project_Tasks_predecessor_Select, New SqlParameter("@FK_TaskId", FK_TaskId), _
               New SqlParameter("@FK_predecessorTask", FK_predecessorTask), _
               New SqlParameter("@RelationType", RelationType)).Rows(0)
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
                objColl = objDac.GetDataTable(Project_Tasks_predecessor_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Get_By_FK_TaskId(ByVal FK_TaskId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Project_Tasks_predecessor_Select_By_TaskId, New SqlParameter("@FK_TaskId", FK_TaskId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace