Imports Microsoft.VisualBasic

Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection

Imports SmartV.UTILITIES

Imports SmartV.DB
Imports TA.Lookup

Namespace TA.Employees

    Public Class DALEmp_OverTimeRule
        Inherits MGRBase

#Region "Class Variables"

        Private strConn As String
        Private Emp_OverTimeRule_Select As String = "Emp_OverTimeRule_Select"
        Private Emp_OverTimeRule_SelectAll As String = "Emp_OverTimeRule_SelectAll"
        Private Emp_OverTimeRule_Insert As String = "Emp_OverTimeRule_Insert"
        Private Emp_OverTimeRule_Update As String = "Emp_OverTimeRule_Update"
        Private Emp_OverTimeRule_Delete As String = "Emp_OverTimeRule_Delete"
        Private Emp_OverTimeRule_SelectByEmpID As String = "Emp_OverTimeRule_SelectByEmpID"
        Private Emp_OverTimeRule_GetEmployeeLastStartedOTRule As String = "Emp_OverTimeRule_GetEmployeeLastStartedOTRule"
        Private Emp_OverTimeRule_GetActiveOTRuleId As String = "Emp_OverTimeRule_GetActiveOTRuleId"
        Private Emp_OvertimeRules_Insert_Assign As String = "Emp_OvertimeRules_Insert_Assign"
        Private Emp_OverTime_Select_ByDM As String = "Emp_OverTime_Select_ByDM"
        Private Emp_OverTime_UpdateStatus As String = "Emp_OverTime_UpdateStatus"
        Private Emp_OverTime_Select_ByHR As String = "Emp_OverTime_Select_ByHR"
        Private Emp_OverTimeRule_CheckAllowedOTRule As String = "Emp_OverTimeRule_CheckAllowedOTRule"

#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_EmployeeId As Long, ByVal FK_RuleId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal CretedBy As Integer, ByVal CretedDate As Date, ByVal LastUpdateBy As Integer, ByVal LastUpdateDate As Date) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_OverTimeRule_Insert, _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_RuleId", FK_RuleId), _
               New SqlParameter("@FromDate", FromDate), _
               New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, DBNull.Value, ToDate)), _
                New SqlParameter("@CREATED_BY", CretedBy), _
                New SqlParameter("@CREATED_DATE", CretedDate), _
                New SqlParameter("@LAST_UPDATE_BY", LastUpdateBy), _
New SqlParameter("@LAST_UPDATE_DATE", LastUpdateDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function AssignTAPolicy(ByVal FK_EmployeeId As Long, ByVal FK_RuleId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal CretedBy As Integer, ByVal CretedDate As Date, ByVal LastUpdateBy As Integer, ByVal LastUpdateDate As Date) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_OvertimeRules_Insert_Assign, _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_RuleId", FK_RuleId), _
               New SqlParameter("@FromDate", FromDate), _
               New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, DBNull.Value, ToDate)), _
                New SqlParameter("@CREATED_BY", CretedBy), _
                New SqlParameter("@CREATED_DATE", CretedDate), _
                New SqlParameter("@LAST_UPDATE_BY", LastUpdateBy), _
New SqlParameter("@LAST_UPDATE_DATE", LastUpdateDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FK_EmployeeId As Long, ByVal FK_RuleId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_OverTimeRule_Update, _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_RuleId", FK_RuleId), _
               New SqlParameter("@FromDate", FromDate), _
               New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, DBNull.Value, ToDate)))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal FK_EmployeeId As Long, ByVal FK_TAPolicyId As Integer, ByVal StartDate As DateTime) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_OverTimeRule_Delete, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_RuleId", FK_TAPolicyId), _
               New SqlParameter("@FromDate", StartDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal FK_EmployeeId As Long, ByVal FK_TAPolicyId As Integer, ByVal StartDate As DateTime) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(Emp_OverTimeRule_Select, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_RuleId", FK_TAPolicyId), _
               New SqlParameter("@FromDate", StartDate)).Rows(0)
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
                objColl = objDac.GetDataTable(Emp_OverTimeRule_SelectAll, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetByEmployeeId(ByVal EmployeeId As Integer) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_OverTimeRule_SelectByEmpID, _
                                              New SqlParameter("@FK_EmployeeId", EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmployeeLastStartedOTRule(ByVal FK_EmployeeId As Long) As DataRow
            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(Emp_OverTimeRule_GetEmployeeLastStartedOTRule, _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetActiveOTRuleId(ByVal FK_EmployeeId As Integer) As DataRow
            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(Emp_OverTimeRule_GetActiveOTRuleId, _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetByDirectManager(ByVal FK_ManagerId As Integer, ByVal FK_StatusId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_OverTime_Select_ByDM, New SqlParameter("@FK_ManagerId", FK_ManagerId), New SqlParameter("@FK_StatusId", FK_StatusId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function UpdateOverTimeStatus(ByVal EmpOvertimeId As Integer, ByVal FK_StatusId As Integer, ByVal RejectionReason As String, ByVal ApprovedDuration As Integer, ByVal IsFinallyApproved As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_OverTime_UpdateStatus, New SqlParameter("@EmpOverTimeId", EmpOvertimeId),
               New SqlParameter("@FK_StatusId", FK_StatusId),
               New SqlParameter("@RejectionReason", RejectionReason),
               New SqlParameter("@ApprovedDuration", ApprovedDuration),
               New SqlParameter("@IsFinallyApproved", IsFinallyApproved))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByHR(ByVal FK_EmployeeHRId As Integer, ByVal FK_StatusId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_OverTime_Select_ByHR, New SqlParameter("@FK_EmployeeHRId", FK_EmployeeHRId), New SqlParameter("@FK_StatusId", FK_StatusId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetAllowedOTRule(ByVal FK_EmployeeId As Integer) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_OverTimeRule_CheckAllowedOTRule, New SqlParameter("@FK_EmployeeId", FK_EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

#End Region

    End Class
End Namespace