Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Appraisal

    Public Class DALAppraisal_EvaluationPoints
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Appraisal_EvaluationPoints_Select As String = "Appraisal_EvaluationPoints_select"
        Private Appraisal_EvaluationPoints_Select_All As String = "Appraisal_EvaluationPoints_select_All"
        Private Appraisal_EvaluationPoints_Insert As String = "Appraisal_EvaluationPoints_Insert"
        Private Appraisal_EvaluationPoints_Update As String = "Appraisal_EvaluationPoints_Update"
        Private Appraisal_EvaluationPoints_Delete As String = "Appraisal_EvaluationPoints_Delete"
        Private Appraisal_EvaluationPoints_Select_PointsCount As String = "Appraisal_EvaluationPoints_Select_PointsCount"

#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal EvaluationPoint As Integer, ByVal PointName As String, ByVal PointNameArabic As String) As Integer

            objDac = DAC.getDAC()
            Try
                ' Dim sqlOut = New SqlParameter("@EvaluationPoint", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, EvaluationPoint)
                errNo = objDac.AddUpdateDeleteSPTrans(Appraisal_EvaluationPoints_Insert, New SqlParameter("@EvaluationPoint", EvaluationPoint), _
               New SqlParameter("@PointName", PointName), _
               New SqlParameter("@PointNameArabic", PointNameArabic))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal OldEvaluationPoint As Integer, ByVal EvaluationPoint As Integer, ByVal PointName As String, ByVal PointNameArabic As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Appraisal_EvaluationPoints_Update, New SqlParameter("@OldEvaluationPoint", OldEvaluationPoint), New SqlParameter("@EvaluationPoint", EvaluationPoint), _
               New SqlParameter("@PointName", PointName), _
               New SqlParameter("@PointNameArabic", PointNameArabic))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal EvaluationPoint As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Appraisal_EvaluationPoints_Delete, New SqlParameter("@EvaluationPoint", EvaluationPoint))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal EvaluationPoint As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Appraisal_EvaluationPoints_Select, New SqlParameter("@EvaluationPoint", EvaluationPoint)).Rows(0)
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
                objColl = objDac.GetDataTable(Appraisal_EvaluationPoints_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Get_PointsCount() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Appraisal_EvaluationPoints_Select_PointsCount, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
#End Region


    End Class
End Namespace