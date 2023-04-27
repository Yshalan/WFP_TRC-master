Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Appraisal

    Public Class DALAppraisal_EmployeeGoals
        Inherits MGRBase

#Region "Class Variables"
        Private strConn As String
        Private Appraisal_EmployeeGoals_Select As String = "Appraisal_EmployeeGoals_select"
        Private Appraisal_EmployeeGoals_Select_All As String = "Appraisal_EmployeeGoals_select_All"
        Private Appraisal_EmployeeGoals_Insert As String = "Appraisal_EmployeeGoals_Insert"
        Private Appraisal_EmployeeGoals_Update As String = "Appraisal_EmployeeGoals_Update"
        Private Appraisal_EmployeeGoals_Delete As String = "Appraisal_EmployeeGoals_Delete"
        Private Appraisal_EmployeeGoals_Select_GoalSequence_By_EmpId_Year As String = "Appraisal_EmployeeGoals_Select_GoalSequence_By_EmpId_Year"
        Private Appraisal_EmployeeGoals_Select_By_FK_EmployeeId_StatusId As String = "Appraisal_EmployeeGoals_Select_By_FK_EmployeeId_StatusId"
        Private Appraisal_EmployeeGoals_UpdateStatus As String = "Appraisal_EmployeeGoals_UpdateStatus"
        Private Appraisal_EmployeeGoals_Select_By_Manager_Status As String = "Appraisal_EmployeeGoals_Select_By_Manager_Status"
        Private Appraisal_EmployeeGoals_Select_ByEmployeeId_Year As String = "Appraisal_EmployeeGoals_Select_ByEmployeeId_Year"
        Private Appraisal_EmployeeGoals_Select_Year_Weight_Sum As String = "Appraisal_EmployeeGoals_Select_Year_Weight_Sum"

#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef GoalId As Integer, ByVal Year As Integer, ByVal FK_EmployeeId As Long, ByVal GoalSequence As Integer, ByVal GoalName As String, ByVal GoalDetails As String, ByVal Weight As Double, ByVal FK_AppraisalStatusId As Integer, ByVal EvaluationPointbyEmployee As Integer, ByVal FinalEvaluationPoint As Integer, ByVal EmployeeRemarks As String, ByVal FinalRemarks As String, ByVal CREATED_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@GoalId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, GoalId)
                errNo = objDac.AddUpdateDeleteSPTrans(Appraisal_EmployeeGoals_Insert, New SqlParameter("@Year", Year),
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
               New SqlParameter("@GoalSequence", GoalSequence),
               New SqlParameter("@GoalName", GoalName),
               New SqlParameter("@GoalDetails", GoalDetails),
               New SqlParameter("@Weight", Weight),
               New SqlParameter("@FK_AppraisalStatusId", FK_AppraisalStatusId),
               New SqlParameter("@EvaluationPointbyEmployee", EvaluationPointbyEmployee),
               New SqlParameter("@FinalEvaluationPoint", FinalEvaluationPoint),
               New SqlParameter("@EmployeeRemarks", EmployeeRemarks),
               New SqlParameter("@FinalRemarks", FinalRemarks),
               New SqlParameter("@CREATED_BY", CREATED_BY))
                If errNo = 0 Then GoalId = sqlOut.Value Else GoalId = 0
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal GoalId As Long, ByVal Year As Integer, ByVal FK_EmployeeId As Long, ByVal GoalSequence As Integer, ByVal GoalName As String, ByVal GoalDetails As String, ByVal Weight As Double, ByVal FK_AppraisalStatusId As Integer, ByVal EvaluationPointbyEmployee As Integer, ByVal FinalEvaluationPoint As Integer, ByVal EmployeeRemarks As String, ByVal FinalRemarks As String, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Appraisal_EmployeeGoals_Update, New SqlParameter("@GoalId", GoalId), _
               New SqlParameter("@Year", Year), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@GoalSequence", GoalSequence), _
               New SqlParameter("@GoalName", GoalName), _
               New SqlParameter("@GoalDetails", GoalDetails), _
               New SqlParameter("@Weight", Weight), _
               New SqlParameter("@FK_AppraisalStatusId", FK_AppraisalStatusId), _
               New SqlParameter("@EvaluationPointbyEmployee", EvaluationPointbyEmployee), _
               New SqlParameter("@FinalEvaluationPoint", FinalEvaluationPoint), _
               New SqlParameter("@EmployeeRemarks", EmployeeRemarks), _
               New SqlParameter("@FinalRemarks", FinalRemarks), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function UpdateStatus(ByVal GoalId As Long, ByVal FK_AppraisalStatusId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Appraisal_EmployeeGoals_UpdateStatus, New SqlParameter("@GoalId", GoalId), _
               New SqlParameter("@FK_AppraisalStatusId", FK_AppraisalStatusId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal GoalId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Appraisal_EmployeeGoals_Delete, New SqlParameter("@GoalId", GoalId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal GoalId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Appraisal_EmployeeGoals_Select, New SqlParameter("@GoalId", GoalId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function Get_GoalSequence_By_EmpId_Year(ByVal FK_EmployeeId As Integer, ByVal Year As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Appraisal_EmployeeGoals_Select_GoalSequence_By_EmpId_Year, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                             New SqlParameter("@Year", Year)).Rows(0)
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
                objColl = objDac.GetDataTable(Appraisal_EmployeeGoals_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Get_By_FK_EmployeeId_StatusId(ByVal FK_EmployeeId As Integer, ByVal FK_AppraisalStatusId As Integer, ByVal Year As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Appraisal_EmployeeGoals_Select_By_FK_EmployeeId_StatusId, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                              New SqlParameter("@FK_AppraisalStatusId", FK_AppraisalStatusId),
                                              New SqlParameter("@Year", Year))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Get_By_Manager_Status(ByVal FK_ManagerId As Integer, ByVal FK_AppraisalStatusId As Integer, ByVal Year As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Appraisal_EmployeeGoals_Select_By_Manager_Status, New SqlParameter("@FK_ManagerId", FK_ManagerId), _
                                              New SqlParameter("@FK_AppraisalStatusId", FK_AppraisalStatusId), _
                                              New SqlParameter("@Year",Year))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Get_By_EmployeeId_Year(ByVal FK_EmployeeId As Integer, ByVal Year As Integer, ByVal FK_AppraisalStatusId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Appraisal_EmployeeGoals_Select_ByEmployeeId_Year, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                              New SqlParameter("@Year", Year), _
                                              New SqlParameter("@FK_AppraisalStatusId", FK_AppraisalStatusId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Get_Year_Weight_Sum(ByVal FK_EmployeeId As Integer, ByVal Year As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Appraisal_EmployeeGoals_Select_Year_Weight_Sum, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                              New SqlParameter("@Year", Year))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region

    End Class
End Namespace