Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup
Imports SmartV.DB


Namespace TA.DailyTasks

    Public Class DALHR_LeaveRequest
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private HR_LeaveRequest_Select As String = "HR_LeaveRequest_select"
        Private HR_LeaveRequest_Select_All As String = "HR_LeaveRequest_select_All"
        Private HR_LeaveRequest_Insert As String = "HR_LeaveRequest_Insert"
        Private HR_LeaveRequest_Update As String = "HR_LeaveRequest_Update"
        Private HR_LeaveRequest_Delete As String = "HR_LeaveRequest_Delete"

        Private HR_Leaves_SelectAllByEmployee As String = "HR_Leaves_SelectAllByEmployee"
        Private HR_Leaves_BalanceHistory_Select_LastBalance As String = "HR_Leaves_BalanceHistory_Select_LastBalance"
        Private HR_Leave_GetCompany_Entity As String = "HR_Leave_GetCompany_Entity"
        Private HR_LeaveOccurancePerWeek_select As String = "HR_LeaveOccurancePerWeek_select"
        Private HR_LeaveOccurancePerMonth_select As String = "HR_LeaveOccurancePerMonth_select"
        Private HR_LeaveOccurancePerYear_select As String = "HR_LeaveOccurancePerYear_select"
        Private HR_LeaveOccurancePerAllServiceTime_select As String = "HR_LeaveOccurancePerAllServiceTime_select"
        Private HR_Get_Emp_Leaves_ByMultiEmployees As String = "HR_Get_Emp_Leaves_ByMultiEmployees"
        Private HR_Get_Emp_Leave_WithEmployeeInner As String = "HR_Get_Emp_Leave_WithEmployeeInner"
        Private HR_Leaves_SelectAllByLeaveStatus As String = "HR_Leaves_SelectAllByLeaveStatus"
        Private Update_HRLeave_RequestStatus As String = "Update_HRLeave_RequestStatus"
        Private HR_Leaves_SelectAllRejected As String = "HR_Leaves_SelectAllRejected"
        Private HR_LeaveRequest_HasRequest As String = "HR_LeaveRequest_HasRequest"
#End Region

#Region "Extended Class Variables"


        Private EmpLeavesGetAllInnerJoin As String = "HR_Leaves_GetAllInnerJoin"

        Private EmpLeavesFindExisting As String = "HR_Leaves_Find_Existing"


#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef LeaveRequestId As Integer, ByVal FK_EmployeeId As Long, _
                               ByVal FK_LeaveTypeId As Integer, ByVal RequestDate As DateTime, _
                               ByVal FromDate As DateTime, ByVal ToDate As DateTime, _
                               ByVal Remarks As String, ByVal IsHalfDay As Boolean, _
                               ByVal AttachedFile As String, ByVal CREATED_BY As String, ByVal Days As Integer, ByVal IsRejected As Boolean) As Integer

            objDac = DAC.getDAC()

            Try
                ' Define output parameter to get the last inserted identity
                Dim sqlParamLeaveId As New SqlParameter()
                sqlParamLeaveId.ParameterName = "@LeaveRequestId"
                sqlParamLeaveId.Value = LeaveRequestId
                sqlParamLeaveId.Direction = ParameterDirection.Output
                errNo = objDac.AddUpdateDeleteSPTrans(HR_LeaveRequest_Insert, sqlParamLeaveId, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_LeaveTypeId", FK_LeaveTypeId), _
               New SqlParameter("@RequestDate", RequestDate), _
               New SqlParameter("@FromDate", FromDate), _
               New SqlParameter("@ToDate", ToDate), _
               New SqlParameter("@Remarks", Remarks), _
               New SqlParameter("@IsHalfDay", IsHalfDay), _
               New SqlParameter("@Days", Days), _
               New SqlParameter("@AttachedFile", AttachedFile), _
               New SqlParameter("@CREATED_BY", CREATED_BY))
                LeaveRequestId = sqlParamLeaveId.Value
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal LeaveRequestId As Integer, ByVal FK_EmployeeId As Long, ByVal FK_LeaveTypeId As Integer, ByVal RequestDate As DateTime, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal Remarks As String, ByVal IsHalfDay As Boolean, ByVal Days As Integer, ByVal AttachedFile As String, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(HR_LeaveRequest_Update, New SqlParameter("@LeaveRequestId", LeaveRequestId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_LeaveTypeId", FK_LeaveTypeId), _
               New SqlParameter("@RequestDate", RequestDate), _
               New SqlParameter("@FromDate", FromDate), _
               New SqlParameter("@ToDate", ToDate), _
               New SqlParameter("@Remarks", Remarks), _
               New SqlParameter("@IsHalfDay", IsHalfDay), _
               New SqlParameter("@Days", Days), _
               New SqlParameter("@AttachedFile", AttachedFile), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal LeaveRequestId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(HR_LeaveRequest_Delete, New SqlParameter("@LeaveRequestId", LeaveRequestId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal LeaveRequestId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(HR_LeaveRequest_Select, New SqlParameter("@LeaveRequestId", LeaveRequestId)).Rows(0)
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
                objColl = objDac.GetDataTable(HR_LeaveRequest_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetOccuranceForWeek(ByVal FromDate As DateTime, ByVal EmployeeId As Integer, ByVal LeaveTypeId As Integer) As Integer

            objDac = DAC.getDAC()
            Dim dtLeavePerWeek As New DataTable
            Try
                dtLeavePerWeek = objDac.GetDataTable(HR_LeaveOccurancePerWeek_select, New SqlParameter("@FromDate", FromDate), _
                                                     New SqlParameter("@EmployeeId", EmployeeId), _
                                                     New SqlParameter("@LeaveTypeId", LeaveTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
                Return errNo
            End Try
            If (dtLeavePerWeek IsNot Nothing AndAlso dtLeavePerWeek.Rows.Count > 0) Then
                Return dtLeavePerWeek.Rows(0)(0)
            Else
                Return 0
            End If


        End Function

        Public Function GetOccuranceForMonth(ByVal FromDate As DateTime, ByVal EmployeeId As Integer, ByVal LeaveTypeId As Integer) As Integer

            objDac = DAC.getDAC()
            Dim dtLeavePerWeek As New DataTable
            Try
                dtLeavePerWeek = objDac.GetDataTable(HR_LeaveOccurancePerMonth_select, New SqlParameter("@FromDate", FromDate), _
                                                     New SqlParameter("@EmployeeId", EmployeeId), _
                                                     New SqlParameter("@LeaveTypeId", LeaveTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
                Return errNo
            End Try
            If (dtLeavePerWeek IsNot Nothing AndAlso dtLeavePerWeek.Rows.Count > 0) Then
                Return dtLeavePerWeek.Rows(0)(0)
            Else
                Return 0
            End If


        End Function

        Public Function GetOccuranceForYear(ByVal FromDate As DateTime, ByVal EmployeeId As Integer, ByVal LeaveTypeId As Integer) As Integer

            objDac = DAC.getDAC()
            Dim dtLeavePerWeek As New DataTable
            Try
                dtLeavePerWeek = objDac.GetDataTable(HR_LeaveOccurancePerYear_select, New SqlParameter("@FromDate", FromDate), _
                                                     New SqlParameter("@EmployeeId", EmployeeId), _
                                                     New SqlParameter("@LeaveTypeId", LeaveTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
                Return errNo
            End Try
            If (dtLeavePerWeek IsNot Nothing AndAlso dtLeavePerWeek.Rows.Count > 0) Then
                Return dtLeavePerWeek.Rows(0)(0)
            Else
                Return 0
            End If


        End Function

        Public Function GetOccuranceForAllServiceTime(ByVal EmployeeId As Integer, ByVal LeaveTypeId As Integer) As Integer

            objDac = DAC.getDAC()
            Dim dtLeavePerWeek As New DataTable
            Try
                dtLeavePerWeek = objDac.GetDataTable(HR_LeaveOccurancePerAllServiceTime_select, New SqlParameter("@EmployeeId", EmployeeId), _
                                                     New SqlParameter("@LeaveTypeId", LeaveTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
                Return errNo
            End Try
            If (dtLeavePerWeek IsNot Nothing AndAlso dtLeavePerWeek.Rows.Count > 0) Then
                Return dtLeavePerWeek.Rows(0)(0)
            Else
                Return 0
            End If


        End Function

        Public Function GetAllLeavesByEmployee(ByVal FK_EmployeeId As Long, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(HR_Leaves_SelectAllByEmployee, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                              New SqlParameter("@FromDate", IIf(FromDate = DateTime.MinValue, DBNull.Value, FromDate)), _
                                              New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, DBNull.Value, ToDate)))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetLeavesByLeaveStatus(ByVal UserId As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(HR_Leaves_SelectAllByLeaveStatus, New SqlParameter("@UserId", UserId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetAll_RejectedLeaves() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(HR_Leaves_SelectAllRejected, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetLastBalanceForEmployees(ByVal CompanyId As Long, ByVal EntityId As Integer, ByVal LeaveType As Integer, ByVal EmployeeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(HR_Leaves_BalanceHistory_Select_LastBalance, New SqlParameter("@CompanyId", CompanyId), _
                                              New SqlParameter("@EntityId", EntityId), _
                                              New SqlParameter("@LeaveType", LeaveType), _
                                              New SqlParameter("@EmployeeId", EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetLastBalanceForEmployee_Info(ByVal CompanyId As Long, ByVal EntityId As Integer, ByVal LeaveType As Integer, ByVal EmployeeId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(HR_Leaves_BalanceHistory_Select_LastBalance, New SqlParameter("@CompanyId", CompanyId), _
                                              New SqlParameter("@EntityId", EntityId), _
                                              New SqlParameter("@LeaveType", LeaveType), _
                                              New SqlParameter("@EmployeeId", EmployeeId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow


        End Function

        Public Function GetAllLeaveLists(ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(HR_Leaves_SelectAllByEmployee, New SqlParameter("@FromDate", FromDate), New SqlParameter("@ToDate", ToDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetCompanyAndEntity(ByVal FK_EmployeeId As Long) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(HR_Leave_GetCompany_Entity, New SqlParameter("@FK_EmployeeId", FK_EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetEmpLeaveByMultiEmployees(ByVal EmpIDs As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(HR_Get_Emp_Leaves_ByMultiEmployees, New SqlParameter("@EmpIDs", EmpIDs))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetAllWithEmployeeInner() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(HR_Get_Emp_Leave_WithEmployeeInner, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Update_Leave_RequestStatus(ByVal LeaveRequestId As Integer, ByVal IsRejected As Boolean, ByVal RejectionReason As String, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Update_HRLeave_RequestStatus, New SqlParameter("@LeaveRequestId", LeaveRequestId), _
               New SqlParameter("@IsRejected", IsRejected), _
               New SqlParameter("@RejectionReason", RejectionReason), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function HR_LeaveRequestHasRequest(ByVal FK_EmployeeId As Long, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(HR_LeaveRequest_HasRequest, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                              New SqlParameter("@FromDate", FromDate), _
                                              New SqlParameter("@ToDate", ToDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
#End Region

#Region "Extended Methods"

        Public Function GetAllInnerJoin() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(EmpLeavesGetAllInnerJoin, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function FindExisting(ByVal LeaveId As Integer) As Boolean
            objDac = DAC.getDAC()
            Try
                Dim rslt As Integer = objDac.GetSingleValue(Of Integer)(EmpLeavesFindExisting, _
               New SqlParameter("@LeaveId", LeaveId))
                If rslt = 1 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo


        End Function


#End Region

    End Class
End Namespace