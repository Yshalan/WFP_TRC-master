Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Appraisal

    Public Class DALAppraisal_EmployeeSkills
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Appraisal_EmployeeSkills_Select As String = "Appraisal_EmployeeSkills_select"
        Private Appraisal_EmployeeSkills_Select_All As String = "Appraisal_EmployeeSkills_select_All"
        Private Appraisal_EmployeeSkills_Insert As String = "Appraisal_EmployeeSkills_Insert"
        Private Appraisal_EmployeeSkills_Update As String = "Appraisal_EmployeeSkills_Update"
        Private Appraisal_EmployeeSkills_Delete As String = "Appraisal_EmployeeSkills_Delete"
        Private Appraisal_EmployeeSkills_SelectPendingBy_FK_EmployeeId As String = "Appraisal_EmployeeSkills_SelectPendingBy_FK_EmployeeId"
        Private Appraisal_EmployeeSkills_UpdateStatus As String = "Appraisal_EmployeeSkills_UpdateStatus"

#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_EmployeeId As Long, ByVal FK_SkillId As Integer, ByVal Year As Integer, ByVal FK_AppraisalStatusId As Integer, ByVal Weight As Double, ByVal EvaluationPointbyEmployee As Integer, ByVal FinalEvaluationPoint As Integer, ByVal EmployeeRemarks As String, ByVal FinalRemarks As String, ByVal CREATED_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Appraisal_EmployeeSkills_Insert, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_SkillId", FK_SkillId), _
               New SqlParameter("@Year", Year), _
               New SqlParameter("@FK_AppraisalStatusId", FK_AppraisalStatusId), _
               New SqlParameter("@Weight", Weight), _
               New SqlParameter("@EvaluationPointbyEmployee", EvaluationPointbyEmployee), _
               New SqlParameter("@FinalEvaluationPoint", FinalEvaluationPoint), _
               New SqlParameter("@EmployeeRemarks", EmployeeRemarks), _
               New SqlParameter("@FinalRemarks", FinalRemarks), _
               New SqlParameter("@CREATED_BY", CREATED_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FK_EmployeeId As Long, ByVal FK_SkillId As Integer, ByVal Year As Integer, ByVal FK_AppraisalStatusId As Integer, ByVal Weight As Double, ByVal EvaluationPointbyEmployee As Integer, ByVal FinalEvaluationPoint As Integer, ByVal EmployeeRemarks As String, ByVal FinalRemarks As String, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Appraisal_EmployeeSkills_Update, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_SkillId", FK_SkillId), _
               New SqlParameter("@Year", Year), _
               New SqlParameter("@FK_AppraisalStatusId", FK_AppraisalStatusId), _
               New SqlParameter("@Weight", Weight), _
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

        Public Function UpdateStatus(ByVal FK_EmployeeId As Long, ByVal FK_SkillId As Integer, ByVal Year As Integer, ByVal FK_AppraisalStatusId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Appraisal_EmployeeSkills_UpdateStatus, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_SkillId", FK_SkillId), _
               New SqlParameter("@Year", Year), _
               New SqlParameter("@FK_AppraisalStatusId", FK_AppraisalStatusId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal FK_EmployeeId As Long, ByVal FK_SkillId As Integer, ByVal Year As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Appraisal_EmployeeSkills_Delete, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_SkillId", FK_SkillId), _
               New SqlParameter("@Year", Year))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal FK_EmployeeId As Long, ByVal FK_SkillId As Integer, ByVal Year As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Appraisal_EmployeeSkills_Select, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_SkillId", FK_SkillId), _
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
                objColl = objDac.GetDataTable(Appraisal_EmployeeSkills_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetBy_FK_EmployeeId(ByVal FK_EmployeeId As Integer, ByVal Year As Integer, ByVal FK_AppraisalStatusId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Appraisal_EmployeeSkills_SelectPendingBy_FK_EmployeeId, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                              New SqlParameter("@Year", Year), _
                                              New SqlParameter("@FK_AppraisalStatusId", FK_AppraisalStatusId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
#End Region


    End Class
End Namespace