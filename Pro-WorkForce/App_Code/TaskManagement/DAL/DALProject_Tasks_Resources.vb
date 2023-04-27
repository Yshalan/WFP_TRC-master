Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.TaskManagement

    Public Class DALProject_Tasks_Resources
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Project_Tasks_Resources_Select As String = "Project_Tasks_Resources_select"
        Private Project_Tasks_Resources_Select_All As String = "Project_Tasks_Resources_select_All"
        Private Project_Tasks_Resources_Insert As String = "Project_Tasks_Resources_Insert"
        Private Project_Tasks_Resources_Update As String = "Project_Tasks_Resources_Update"
        Private Project_Tasks_Resources_Delete As String = "Project_Tasks_Resources_Delete"
        Private Project_Tasks_Resources_Select_By_FK_TaskId As String = "Project_Tasks_Resources_Select_By_FK_TaskId"

#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_TaskId As Long, ByVal FK_EmployeeId As Long, ByVal Involvmentpercentage As Double, ByVal completionpercentage As Double, ByVal IsCompletedByResource As Boolean, ByVal CREATED_BY As String, ByVal TotalSpendTimeMinutes As Integer, ByVal CompletionRemark As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Project_Tasks_Resources_Insert, New SqlParameter("@FK_TaskId", FK_TaskId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@Involvmentpercentage", Involvmentpercentage), _
               New SqlParameter("@completionpercentage", completionpercentage), _
               New SqlParameter("@IsCompletedByResource", IsCompletedByResource), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@TotalSpendTimeMinutes", TotalSpendTimeMinutes), _
               New SqlParameter("@CompletionRemark", CompletionRemark))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FK_TaskId As Long, ByVal FK_EmployeeId As Long, ByVal Involvmentpercentage As Double, ByVal completionpercentage As Double, ByVal IsCompletedByResource As Boolean, ByVal LAST_UPDATE_BY As String, ByVal TotalSpendTimeMinutes As Integer, ByVal CompletionRemark As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Project_Tasks_Resources_Update, New SqlParameter("@FK_TaskId", FK_TaskId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@Involvmentpercentage", Involvmentpercentage), _
               New SqlParameter("@completionpercentage", completionpercentage), _
               New SqlParameter("@IsCompletedByResource", IsCompletedByResource), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
               New SqlParameter("@TotalSpendTimeMinutes", TotalSpendTimeMinutes), _
               New SqlParameter("@CompletionRemark", CompletionRemark))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal FK_TaskId As Long, ByVal FK_EmployeeId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Project_Tasks_Resources_Delete, New SqlParameter("@FK_TaskId", FK_TaskId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal FK_TaskId As Long, ByVal FK_EmployeeId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Project_Tasks_Resources_Select, New SqlParameter("@FK_TaskId", FK_TaskId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId)).Rows(0)
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
                objColl = objDac.GetDataTable(Project_Tasks_Resources_Select_All, Nothing)
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
                objColl = objDac.GetDataTable(Project_Tasks_Resources_Select_By_FK_TaskId, New SqlParameter("@FK_TaskId", FK_TaskId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace