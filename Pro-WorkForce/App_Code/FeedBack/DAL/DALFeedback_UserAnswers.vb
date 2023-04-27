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

    Public Class DALFeedback_UserAnswers
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Feedback_UserAnswers_Select As String = "Feedback_UserAnswers_select"
        Private Feedback_UserAnswers_Select_All As String = "Feedback_UserAnswers_select_All"
        Private Feedback_UserAnswers_Insert As String = "Feedback_UserAnswers_Insert"
        Private Feedback_UserAnswers_Update As String = "Feedback_UserAnswers_Update"
        Private Feedback_UserAnswers_Delete As String = "Feedback_UserAnswers_Delete"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_SurveyId As Integer, ByVal FK_QuestionId As Integer, ByVal FK_AnswerId As Integer, ByVal AnswerText As String, ByVal FK_EmployeeId As Long, ByVal DeviceDetails As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Feedback_UserAnswers_Insert, New SqlParameter("@FK_SurveyId", FK_SurveyId),
               New SqlParameter("@FK_QuestionId", FK_QuestionId),
               New SqlParameter("@FK_AnswerId", FK_AnswerId),
               New SqlParameter("@AnswerText", AnswerText),
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
               New SqlParameter("@DeviceDetails", DeviceDetails))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal UserAnswerId As Integer, ByVal FK_SurveyId As Integer, ByVal FK_QuestionId As Integer, ByVal FK_AnswerId As Integer, ByVal AnswerText As String, ByVal FK_EmployeeId As Long, ByVal DeviceDetails As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Feedback_UserAnswers_Update, New SqlParameter("@UserAnswerId", UserAnswerId),
               New SqlParameter("@FK_SurveyId", FK_SurveyId),
               New SqlParameter("@FK_QuestionId", FK_QuestionId),
               New SqlParameter("@FK_AnswerId", FK_AnswerId),
               New SqlParameter("@AnswerText", AnswerText),
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
               New SqlParameter("@DeviceDetails", DeviceDetails))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal UserAnswerId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Feedback_UserAnswers_Delete, New SqlParameter("@UserAnswerId", UserAnswerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal UserAnswerId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Feedback_UserAnswers_Select, New SqlParameter("@UserAnswerId", UserAnswerId)).Rows(0)
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
                objColl = objDac.GetDataTable(Feedback_UserAnswers_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace