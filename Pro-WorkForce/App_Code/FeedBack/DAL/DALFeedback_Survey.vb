Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports TA.LookUp
Imports SmartV.DB
Imports SmartV.UTILITIES

Namespace TA.FeedBack

    Public Class DALFeedback_Survey
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Feedback_Survey_Select As String = "Feedback_Survey_select"
        Private Feedback_Survey_Select_All As String = "Feedback_Survey_select_All"
        Private Feedback_Survey_Insert As String = "Feedback_Survey_Insert"
        Private Feedback_Survey_Update As String = "Feedback_Survey_Update"
        Private Feedback_Survey_Delete As String = "Feedback_Survey_Delete"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef SurveyId As Integer, ByVal SurveyName As String, ByVal SurveyArabicName As String, ByVal SurveyLanguage As Integer, ByVal HasWeightage As Boolean, ByVal CREATED_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@SurveyId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, SurveyId)

                errNo = objDac.AddUpdateDeleteSPTrans(Feedback_Survey_Insert, sqlOut, New SqlParameter("@SurveyName", SurveyName),
               New SqlParameter("@SurveyArabicName", SurveyArabicName),
               New SqlParameter("@SurveyLanguage", SurveyLanguage),
               New SqlParameter("@HasWeightage", HasWeightage),
               New SqlParameter("@CREATED_BY", CREATED_BY))
                If Not IsDBNull(sqlOut.Value) Then
                    SurveyId = sqlOut.Value
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal SurveyId As Integer, ByVal SurveyName As String, ByVal SurveyArabicName As String, ByVal SurveyLanguage As Integer, ByVal HasWeightage As Boolean, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Feedback_Survey_Update, New SqlParameter("@SurveyId", SurveyId),
               New SqlParameter("@SurveyName", SurveyName),
               New SqlParameter("@SurveyArabicName", SurveyArabicName),
               New SqlParameter("@SurveyLanguage", SurveyLanguage),
               New SqlParameter("@HasWeightage", HasWeightage),
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal SurveyId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Feedback_Survey_Delete, New SqlParameter("@SurveyId", SurveyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal SurveyId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Feedback_Survey_Select, New SqlParameter("@SurveyId", SurveyId)).Rows(0)
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
                objColl = objDac.GetDataTable(Feedback_Survey_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace