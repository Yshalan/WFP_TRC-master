Imports Microsoft.VisualBasic

Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection

Imports SmartV.UTILITIES
Imports TA.LookUp
Imports SmartV.DB


Namespace TA.Admin

    Public Class DALEmp_Grade
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Emp_Grade_Select As String = "Emp_Grade_select"
        Private Emp_Grade_Select_All As String = "Emp_Grade_select_All"
        Private Emp_Grade_Insert As String = "Emp_Grade_Insert"
        Private Emp_Grade_Update As String = "Emp_Grade_Update"
        Private Emp_Grade_Delete As String = "Emp_Grade_Delete"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef GradeId As Integer, ByVal GradeCode As String, ByVal GradeName As String, ByVal GradeArabicName As String, ByVal AnnualLeaveBalance As Double, ByVal FK_OvertimeRuleId As Integer, ByVal IsTAException As Boolean, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                If (FK_OvertimeRuleId <> 0) Then
                    Dim SqlOut As New SqlParameter("@GradeId", SqlDbType.Int, 10, ParameterDirection.Output, False, 0, 0, "GradeId", DataRowVersion.Default, GradeId)
                    errNo = objDac.AddUpdateDeleteSPTrans(Emp_Grade_Insert, New SqlParameter("@GradeCode", GradeCode), _
                   New SqlParameter("@GradeName", GradeName), _
                   New SqlParameter("@GradeArabicName", GradeArabicName), _
                   New SqlParameter("@AnnualLeaveBalance", AnnualLeaveBalance), _
                   New SqlParameter("@FK_OvertimeRuleId", FK_OvertimeRuleId), _
                   New SqlParameter("@IsTAException", IsTAException), _
                   New SqlParameter("@CREATED_BY", CREATED_BY), _
                   New SqlParameter("@CREATED_DATE", CREATED_DATE), _
                   New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
                   New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE), SqlOut)
                    If errNo = 0 Then GradeId = SqlOut.Value Else GradeId = 0
                Else
                    Dim SqlOut As New SqlParameter("@GradeId", SqlDbType.Int, 10, ParameterDirection.Output, False, 0, 0, "GradeId", DataRowVersion.Default, GradeId)
                    errNo = objDac.AddUpdateDeleteSPTrans(Emp_Grade_Insert, New SqlParameter("@GradeCode", GradeCode), _
                   New SqlParameter("@GradeName", GradeName), _
                   New SqlParameter("@GradeArabicName", GradeArabicName), _
                   New SqlParameter("@AnnualLeaveBalance", AnnualLeaveBalance), _
                   New SqlParameter("@IsTAException", IsTAException), _
                   New SqlParameter("@CREATED_BY", CREATED_BY), _
                   New SqlParameter("@CREATED_DATE", CREATED_DATE), _
                   New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
                   New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE), SqlOut)
                    If errNo = 0 Then GradeId = SqlOut.Value Else GradeId = 0
                End If


            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal GradeId As Integer, ByVal GradeCode As String, ByVal GradeName As String, ByVal GradeArabicName As String, ByVal AnnualLeaveBalance As Double, ByVal FK_OvertimeRuleId As Integer, ByVal IsTAException As Boolean, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                If (FK_OvertimeRuleId <> 0) Then
                    errNo = objDac.AddUpdateDeleteSPTrans(Emp_Grade_Update, New SqlParameter("@GradeId", GradeId), _
              New SqlParameter("@GradeCode", GradeCode), _
              New SqlParameter("@GradeName", GradeName), _
              New SqlParameter("@GradeArabicName", GradeArabicName), _
              New SqlParameter("@AnnualLeaveBalance", AnnualLeaveBalance), _
              New SqlParameter("@FK_OvertimeRuleId", FK_OvertimeRuleId), _
              New SqlParameter("@IsTAException", IsTAException), _
              New SqlParameter("@CREATED_BY", CREATED_BY), _
              New SqlParameter("@CREATED_DATE", CREATED_DATE), _
              New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
              New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE))
                Else
                    errNo = objDac.AddUpdateDeleteSPTrans(Emp_Grade_Update, New SqlParameter("@GradeId", GradeId), _
              New SqlParameter("@GradeCode", GradeCode), _
              New SqlParameter("@GradeName", GradeName), _
              New SqlParameter("@GradeArabicName", GradeArabicName), _
              New SqlParameter("@AnnualLeaveBalance", AnnualLeaveBalance), _
              New SqlParameter("@IsTAException", IsTAException), _
              New SqlParameter("@CREATED_BY", CREATED_BY), _
              New SqlParameter("@CREATED_DATE", CREATED_DATE), _
              New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
              New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE))
                End If

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal GradeId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Grade_Delete, New SqlParameter("@GradeId", GradeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal GradeId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(Emp_Grade_Select, New SqlParameter("@GradeId", GradeId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetAll() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_Grade_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace