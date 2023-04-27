Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Employees

    Public Class DALEmp_Overtime
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Emp_Overtime_Select As String = "Emp_Overtime_select"
        Private Emp_Overtime_Select_All As String = "Emp_Overtime_select_All"
        Private Emp_Overtime_Insert As String = "Emp_Overtime_Insert"
        Private Emp_Overtime_Update As String = "Emp_Overtime_Update"
        Private Emp_Overtime_Delete As String = "Emp_Overtime_Delete"
        Private Emp_Overtime_Select_ByEmp As String = "Emp_Overtime_Select_ByEmp"
        Private Emp_Overtime_InsertRequest As String = "Emp_Overtime_InsertRequest"

#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_EmployeeId As Long, ByVal FK_OvertimeRuleId As Integer, ByVal FromDateTime As DateTime, ByVal ToDateTime As DateTime, ByVal Duration As Integer, ByVal ApprovedDuration As Integer, ByVal IsHigh As Boolean, ByVal FK_ApprovalId As Long, ByVal IsCompensateLatetime As Boolean, ByVal IsLeaveBalance As Boolean, ByVal IsFinancial As Boolean, ByVal ProcessStatus As Integer, ByVal RejectionReason As String, ByVal CREATED_BY As String, ByVal Emp_Remarks As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Overtime_Insert, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
               New SqlParameter("@FK_OvertimeRuleId", FK_OvertimeRuleId),
               New SqlParameter("@FromDateTime", FromDateTime),
               New SqlParameter("@ToDateTime", ToDateTime),
               New SqlParameter("@Duration", Duration),
               New SqlParameter("@ApprovedDuration", ApprovedDuration),
               New SqlParameter("@IsHigh", IsHigh),
               New SqlParameter("@FK_ApprovalId", FK_ApprovalId),
               New SqlParameter("@IsCompensateLatetime", IsCompensateLatetime),
               New SqlParameter("@IsLeaveBalance", IsLeaveBalance),
               New SqlParameter("@IsFinancial", IsFinancial),
               New SqlParameter("@ProcessStatus", ProcessStatus),
               New SqlParameter("@RejectionReason", IIf(RejectionReason = Nothing, DBNull.Value, RejectionReason)),
               New SqlParameter("@CREATED_BY", CREATED_BY),
               New SqlParameter("@Emp_Remarks", Emp_Remarks))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal EmpOverTimeId As Integer, ByVal FK_EmployeeId As Long, ByVal FK_OvertimeRuleId As Integer, ByVal FromDateTime As DateTime, ByVal ToDateTime As DateTime, ByVal Duration As Integer, ByVal ApprovedDuration As Integer, ByVal IsHigh As Boolean, ByVal FK_ApprovalId As Long, ByVal IsCompensateLatetime As Boolean, ByVal IsLeaveBalance As Boolean, ByVal IsFinancial As Boolean, ByVal ProcessStatus As Integer, ByVal ProccessDate As DateTime, ByVal RejectionReason As String, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime, ByVal Emp_Remarks As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Overtime_Update, New SqlParameter("@EmpOverTimeId", EmpOverTimeId),
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
               New SqlParameter("@FK_OvertimeRuleId", FK_OvertimeRuleId),
               New SqlParameter("@FromDateTime", FromDateTime),
               New SqlParameter("@ToDateTime", ToDateTime),
               New SqlParameter("@Duration", Duration),
               New SqlParameter("@ApprovedDuration", ApprovedDuration),
               New SqlParameter("@IsHigh", IsHigh),
               New SqlParameter("@FK_ApprovalId", FK_ApprovalId),
               New SqlParameter("@IsCompensateLatetime", IsCompensateLatetime),
               New SqlParameter("@IsLeaveBalance", IsLeaveBalance),
               New SqlParameter("@IsFinancial", IsFinancial),
               New SqlParameter("@ProcessStatus", ProcessStatus),
               New SqlParameter("@ProccessDate", ProccessDate),
               New SqlParameter("@RejectionReason", RejectionReason),
               New SqlParameter("@CREATED_BY", CREATED_BY),
               New SqlParameter("@CREATED_DATE", CREATED_DATE),
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY),
               New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE),
               New SqlParameter("@Emp_Remarks", Emp_Remarks))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function UpdateRequest(ByVal EmpOverTimeId As Integer, ByVal ProcessStatus As Integer, ByVal LAST_UPDATE_BY As String, ByVal Emp_Remarks As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Overtime_InsertRequest, New SqlParameter("@EmpOverTimeId", EmpOverTimeId),
               New SqlParameter("@ProcessStatus", ProcessStatus),
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY),
               New SqlParameter("@Emp_Remarks", Emp_Remarks))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal EmpOverTimeId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Overtime_Delete, New SqlParameter("@EmpOverTimeId", EmpOverTimeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal EmpOverTimeId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Emp_Overtime_Select, New SqlParameter("@EmpOverTimeId", EmpOverTimeId)).Rows(0)
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
                objColl = objDac.GetDataTable(Emp_Overtime_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetByEmployee(ByVal FK_EmployeeId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal ProcessStatus As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_Overtime_Select_ByEmp, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
                                                                         New SqlParameter("@FromDate", FromDate),
                                                                         New SqlParameter("@ToDate", ToDate),
                                                                         New SqlParameter("@ProcessStatus", ProcessStatus))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace