Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.DB
Imports TA.LookUp
Imports SmartV.UTILITIES

Namespace TA.FeedBack

    Public Class DALFeedback_Questions
        Inherits MGRBase



#Region "Class Variables"

        Private strConn As String
        Private Feedback_Questions_Select As String = "Feedback_Questions_select"
        Private Feedback_Questions_Select_All As String = "Feedback_Questions_select_All"
        Private Feedback_Questions_Insert As String = "Feedback_Questions_Insert"
        Private Feedback_Questions_Update As String = "Feedback_Questions_Update"
        Private Feedback_Questions_Delete As String = "Feedback_Questions_Delete"
        Private Feedback_Questions_Select_All_BY_FK_SurveyId As String = "Feedback_Questions_Select_All_BY_FK_SurveyId"
        Private Feedback_Questions_Update_IsDeleted As String = "Feedback_Questions_Update_IsDeleted"

#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef QuestionId As Integer, ByVal QuestionType As Integer, ByVal QuestionEnText As String, ByVal QuestionArText As String, ByVal FK_SurveyId As Integer, ByVal IsAnswerRequired As Boolean, ByVal Weight As Integer, ByVal IsDeleted As Boolean, ByVal CREATED_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@QuestionId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, QuestionId)

                errNo = objDac.AddUpdateDeleteSPTrans(Feedback_Questions_Insert, sqlOut, New SqlParameter("@QuestionType", QuestionType),
               New SqlParameter("@QuestionEnText", QuestionEnText),
               New SqlParameter("@QuestionArText", QuestionArText),
               New SqlParameter("@FK_SurveyId", FK_SurveyId),
               New SqlParameter("@IsAnswerRequired", IsAnswerRequired),
               New SqlParameter("@Weight", Weight),
               New SqlParameter("@IsDeleted", IsDeleted),
               New SqlParameter("@CREATED_BY", CREATED_BY))
                If Not IsDBNull(sqlOut.Value) Then
                    QuestionId = sqlOut.Value
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal QuestionId As Integer, ByVal QuestionType As Integer, ByVal QuestionEnText As String, ByVal QuestionArText As String, ByVal FK_SurveyId As Integer, ByVal IsAnswerRequired As Boolean, ByVal Weight As Integer, ByVal IsDeleted As Boolean, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Feedback_Questions_Update, New SqlParameter("@QuestionId", QuestionId),
               New SqlParameter("@QuestionType", QuestionType),
               New SqlParameter("@QuestionEnText", QuestionEnText),
               New SqlParameter("@QuestionArText", QuestionArText),
               New SqlParameter("@FK_SurveyId", FK_SurveyId),
               New SqlParameter("@IsAnswerRequired", IsAnswerRequired),
               New SqlParameter("@Weight", Weight),
               New SqlParameter("@IsDeleted", IsDeleted),
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal QuestionId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Feedback_Questions_Delete, New SqlParameter("@QuestionId", QuestionId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete_Update_IsDeleted(ByVal QuestionId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Feedback_Questions_Update_IsDeleted, New SqlParameter("@QuestionId", QuestionId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal QuestionId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Feedback_Questions_Select, New SqlParameter("@QuestionId", QuestionId)).Rows(0)
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
                objColl = objDac.GetDataTable(Feedback_Questions_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAll_BY_FK_SurveyId(ByVal FK_SurveyId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Feedback_Questions_Select_All_BY_FK_SurveyId, New SqlParameter("@FK_SurveyId", FK_SurveyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace