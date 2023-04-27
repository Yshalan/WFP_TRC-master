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

    Public Class DALFeedback_SurveyAssignment
        Inherits MGRBase



#Region "Class Variables"

        Private strConn As String
        Private Feedback_SurveyAssignment_Select As String = "Feedback_SurveyAssignment_select"
        Private Feedback_SurveyAssignment_Select_All As String = "Feedback_SurveyAssignment_select_All"
        Private Feedback_SurveyAssignment_Insert As String = "Feedback_SurveyAssignment_Insert"
        Private Feedback_SurveyAssignment_Update As String = "Feedback_SurveyAssignment_Update"
        Private Feedback_SurveyAssignment_Delete As String = "Feedback_SurveyAssignment_Delete"
        Private Feedback_SurveyAssignment_Select_ForEmp As String = "Feedback_SurveyAssignment_Select_ForEmp"
        Private Feedback_SurveyAssignment_Select_ForLogicalGroup As String = "Feedback_SurveyAssignment_Select_ForLogicalGroup"
        Private Feedback_SurveyAssignment_Select_RandomQuestions_Mobile As String = "Feedback_SurveyAssignment_Select_RandomQuestions_Mobile"

#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef AssignmentId As Integer, ByVal FK_EmployeeId As Long, ByVal FK_LogicalGroupId As Integer, ByVal FK_SurveyId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal CREATED_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@AssignmentId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, AssignmentId)
                errNo = objDac.AddUpdateDeleteSPTrans(Feedback_SurveyAssignment_Insert, sqlOut, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
               New SqlParameter("@FK_LogicalGroupId", FK_LogicalGroupId),
               New SqlParameter("@FK_SurveyId", FK_SurveyId),
               New SqlParameter("@FromDate", FromDate),
               New SqlParameter("@ToDate", ToDate),
               New SqlParameter("@CREATED_BY", CREATED_BY))
                If errNo = 0 Then AssignmentId = sqlOut.Value Else AssignmentId = 0
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal AssignmentId As Integer, ByVal FK_EmployeeId As Long, ByVal FK_LogicalGroupId As Integer, ByVal FK_SurveyId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Feedback_SurveyAssignment_Update, New SqlParameter("@AssignmentId", AssignmentId),
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
               New SqlParameter("@FK_LogicalGroupId", FK_LogicalGroupId),
               New SqlParameter("@FK_SurveyId", FK_SurveyId),
               New SqlParameter("@FromDate", FromDate),
               New SqlParameter("@ToDate", ToDate),
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal AssignmentId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Feedback_SurveyAssignment_Delete, New SqlParameter("@AssignmentId", AssignmentId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal AssignmentId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Feedback_SurveyAssignment_Select, New SqlParameter("@AssignmentId", AssignmentId)).Rows(0)
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
                objColl = objDac.GetDataTable(Feedback_SurveyAssignment_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAll_ForEmp() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Feedback_SurveyAssignment_Select_ForEmp, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAll_ForLogicalGroup() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Feedback_SurveyAssignment_Select_ForLogicalGroup, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Get_RandomQuestions_Mobile(ByVal FK_EmployeeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Feedback_SurveyAssignment_Select_RandomQuestions_Mobile, New SqlParameter("@FK_EmployeeId", FK_EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace