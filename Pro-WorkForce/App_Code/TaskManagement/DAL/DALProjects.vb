Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup

Namespace TA.TaskManagement

    Public Class DALProjects
        Inherits MGRBase

#Region "Class Variables"
        Private strConn As String
        Private Projects_Select As String = "Projects_select"
        Private Projects_Select_All As String = "Projects_select_All"
        Private Projects_Insert As String = "Projects_Insert"
        Private Projects_Update As String = "Projects_Update"
        Private Projects_Delete As String = "Projects_Delete"
#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef ProjectId As Integer, ByVal ProjectName As String, ByVal ProjectArabicName As String, ByVal Project_Description As String, ByVal Project_ArabicDescription As String, ByVal PlannedStartDate As DateTime, ByVal PlannedEndDate As DateTime, ByVal ActualStartDate As DateTime, ByVal ActualEndDate As DateTime, ByVal CREATED_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@ProjectId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, ProjectId)
                errNo = objDac.AddUpdateDeleteSPTrans(Projects_Insert, sqlOut, New SqlParameter("@ProjectName", ProjectName), _
               New SqlParameter("@ProjectArabicName", ProjectArabicName), _
               New SqlParameter("@Project_Description", Project_Description), _
               New SqlParameter("@Project_ArabicDescription", Project_ArabicDescription), _
               New SqlParameter("@PlannedStartDate", PlannedStartDate), _
               New SqlParameter("@PlannedEndDate", PlannedEndDate), _
               New SqlParameter("@ActualStartDate", IIf(ActualStartDate = DateTime.MinValue, Nothing, ActualStartDate)), _
               New SqlParameter("@ActualEndDate", IIf(ActualEndDate = DateTime.MinValue, Nothing, ActualEndDate)), _
               New SqlParameter("@CREATED_BY", CREATED_BY))
                If Not IsDBNull(sqlOut.Value) Then
                    ProjectId = sqlOut.Value
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal ProjectId As Long, ByVal ProjectName As String, ByVal ProjectArabicName As String, ByVal Project_Description As String, ByVal Project_ArabicDescription As String, ByVal PlannedStartDate As DateTime, ByVal PlannedEndDate As DateTime, ByVal ActualStartDate As DateTime, ByVal ActualEndDate As DateTime, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Projects_Update, New SqlParameter("@ProjectId", ProjectId), _
               New SqlParameter("@ProjectName", ProjectName), _
               New SqlParameter("@ProjectArabicName", ProjectArabicName), _
               New SqlParameter("@Project_Description", Project_Description), _
               New SqlParameter("@Project_ArabicDescription", Project_ArabicDescription), _
               New SqlParameter("@PlannedStartDate", Convert.ToDateTime(PlannedStartDate)), _
               New SqlParameter("@PlannedEndDate", PlannedEndDate), _
               New SqlParameter("@ActualStartDate", IIf(ActualStartDate = DateTime.MinValue, Nothing, ActualStartDate)), _
               New SqlParameter("@ActualEndDate", IIf(ActualEndDate = DateTime.MinValue, Nothing, ActualEndDate)), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal ProjectId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Projects_Delete, New SqlParameter("@ProjectId", ProjectId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal ProjectId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Projects_Select, New SqlParameter("@ProjectId", ProjectId)).Rows(0)
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
                objColl = objDac.GetDataTable(Projects_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region

    End Class
End Namespace