Imports Microsoft.VisualBasic
Imports ST.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports ST.UTILITIES
Imports TA.LookUp
Imports SmartV.DB
Imports SmartV.UTILITIES

Namespace TA.FeedBack

    Public Class DALFeedback_Answers
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Feedback_Answers_Select As String = "Feedback_Answers_select"
        Private Feedback_Answers_Select_All As String = "Feedback_Answers_select_All"
        Private Feedback_Answers_Insert As String = "Feedback_Answers_Insert"
        Private Feedback_Answers_Update As String = "Feedback_Answers_Update"
        Private Feedback_Answers_Delete As String = "Feedback_Answers_Delete"
        Private Feedback_Answers_Delete_By_FK_QuestionId As String = "Feedback_Answers_Delete_By_FK_QuestionId"
        Private Feedback_Answers_Select_By_QuestionId As String = "Feedback_Answers_Select_By_QuestionId"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef AnswerId As Integer, ByVal FK_QuestionId As Integer, ByVal AnswerTextEn As String, ByVal AnswerTextAr As String, ByVal SmileyIcon As String, ByVal CREATED_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@AnswerId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, AnswerId)
                errNo = objDac.AddUpdateDeleteSPTrans(Feedback_Answers_Insert, sqlOut, New SqlParameter("@FK_QuestionId", FK_QuestionId),
               New SqlParameter("@AnswerTextEn", AnswerTextEn),
               New SqlParameter("@AnswerTextAr", AnswerTextAr),
               New SqlParameter("@SmileyIcon", SmileyIcon),
               New SqlParameter("@CREATED_BY", CREATED_BY))
                If errNo = 0 Then AnswerId = sqlOut.Value Else AnswerId = 0
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal AnswerId As Integer, ByVal FK_QuestionId As Integer, ByVal AnswerTextEn As String, ByVal AnswerTextAr As String, ByVal SmileyIcon As String, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Feedback_Answers_Update, New SqlParameter("@AnswerId", AnswerId),
               New SqlParameter("@FK_QuestionId", FK_QuestionId),
               New SqlParameter("@AnswerTextEn", AnswerTextEn),
               New SqlParameter("@AnswerTextAr", AnswerTextAr),
               New SqlParameter("@SmileyIcon", SmileyIcon),
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal AnswerId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Feedback_Answers_Delete, New SqlParameter("@AnswerId", AnswerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal AnswerId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Feedback_Answers_Select, New SqlParameter("@AnswerId", AnswerId)).Rows(0)
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
                objColl = objDac.GetDataTable(Feedback_Answers_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAll_By_QuestionId(ByVal FK_QuestionId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Feedback_Answers_Select_By_QuestionId, New SqlParameter("@QuestionId", FK_QuestionId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Delete_BY_QuestionId(ByVal FK_QuestionId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Feedback_Answers_Delete_By_FK_QuestionId, New SqlParameter("@FK_QuestionId", FK_QuestionId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

#End Region


    End Class
End Namespace