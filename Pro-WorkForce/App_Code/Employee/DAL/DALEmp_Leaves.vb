Imports Microsoft.VisualBasic

Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports TA.Lookup
Imports SmartV.UTILITIES
Imports SmartV.DB



Namespace TA.Employees

    Public Class DALEmp_Leaves
        Inherits MGRBase

#Region "Class Variables"
        Private strConn As String
        Private Emp_Leaves_Select As String = "Emp_Leaves_select"
        Private Emp_Leaves_Select_All As String = "Emp_Leaves_select_All"
        Private Emp_Leaves_Insert As String = "Emp_Leaves_Insert"
        Private Emp_Leaves_Update As String = "Emp_Leaves_Update"
        Private Emp_Leaves_Delete As String = "Emp_Leaves_Delete"
        Private Emp_Leaves_SelectAllByEmployee As String = "Emp_Leaves_SelectAllByEmployee"
        Private Emp_Leaves_BalanceHistory_Select_LastBalance As String = "Emp_Leaves_BalanceHistory_Select_LastBalance"
        Private Emp_Leave_GetCompany_Entity As String = "Emp_Leave_GetCompany_Entity"
        Private LeaveOccurancePerWeek_select As String = "LeaveOccurancePerWeek_select"
        Private LeaveOccurancePerMonth_select As String = "LeaveOccurancePerMonth_select"
        Private LeaveOccurancePerYear_select As String = "LeaveOccurancePerYear_select"
        Private LeaveOccurancePerAllServiceTime_select As String = "LeaveOccurancePerAllServiceTime_select"
        Private Get_Emp_Leaves_ByMultiEmployees As String = "Get_Emp_Leaves_ByMultiEmployees"
        Private Get_Emp_Leave_WithEmployeeInner As String = "Get_Emp_Leave_WithEmployeeInner"
        Private Emp_Leaves_Validate_RestDay As String = "Emp_Leaves_Validate_RestDay"
        Private Emp_Leaves_SelectAll_ByUserId_InnerPage As String = "Emp_Leaves_SelectAll_ByUserId_InnerPage"

#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Extended Class Variables"


        Private EmpLeavesGetAllInnerJoin As String = "Emp_Leaves_GetAllInnerJoin"

        Private EmpLeavesFindExisting As String = "Emp_Leaves_Find_Existing"


#End Region

#Region "Methods"

        Public Function Add(ByRef LeaveId As Integer, ByVal FK_EmployeeId As Long, _
                            ByVal FK_LeaveTypeId As Integer, ByVal RequestDate As DateTime, _
                            ByVal FromDate As DateTime, ByVal ToDate As DateTime, _
                            ByVal Remarks As String, ByVal IsHalfDay As Boolean, _
                            ByVal AttachedFile As String, ByVal CREATED_BY As String, ByVal Days As Integer, _
                            ByVal IsAddedFromEmployee As Boolean, _
                            ByVal LeaveRequestId As Integer) As Integer

            objDac = DAC.getDAC()

            Try
                ' Define output parameter to get the last inserted identity
                Dim sqlParamLeaveId As New SqlParameter()
                sqlParamLeaveId.ParameterName = "@LeaveId"
                sqlParamLeaveId.Value = LeaveId
                sqlParamLeaveId.Direction = ParameterDirection.Output
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Leaves_Insert, _
               sqlParamLeaveId, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_LeaveTypeId", FK_LeaveTypeId), _
               New SqlParameter("@RequestDate", RequestDate), _
               New SqlParameter("@FromDate", FromDate), _
               New SqlParameter("@ToDate", ToDate), _
               New SqlParameter("@Remarks", Remarks), _
               New SqlParameter("@IsHalfDay", IsHalfDay), _
               New SqlParameter("@AttachedFile", AttachedFile), _
               New SqlParameter("@Days", Days), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@IsAddedFromEmployee", IsAddedFromEmployee), _
               New SqlParameter("@LeaveRequestId", LeaveRequestId))
                ' Set the value to ByRef parameter
                LeaveId = sqlParamLeaveId.Value
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try

            Return errNo

        End Function

        Public Function Update(ByVal LeaveId As Integer, ByVal FK_EmployeeId As Long, ByVal FK_LeaveTypeId As Integer, ByVal RequestDate As DateTime, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal Remarks As String, ByVal IsHalfDay As Boolean, ByVal AttachedFile As String, ByVal LAST_UPDATE_BY As String, ByVal Days As String, ByVal IsAddedFromEmployee As Boolean, ByVal LeaveRequestId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Leaves_Update, New SqlParameter("@LeaveId", LeaveId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_LeaveTypeId", FK_LeaveTypeId), _
               New SqlParameter("@RequestDate", RequestDate), _
               New SqlParameter("@FromDate", FromDate), _
               New SqlParameter("@ToDate", ToDate), _
               New SqlParameter("@Remarks", Remarks), _
               New SqlParameter("@IsHalfDay", IsHalfDay), _
               New SqlParameter("@AttachedFile", AttachedFile), _
               New SqlParameter("@Days", Days), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
               New SqlParameter("@IsAddedFromEmployee", IsAddedFromEmployee), _
               New SqlParameter("@LeaveRequestId", LeaveRequestId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal LeaveId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Leaves_Delete, New SqlParameter("@LeaveId", LeaveId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetOccuranceForWeek(ByVal FromDate As DateTime, ByVal EmployeeId As Integer, ByVal LeaveTypeId As Integer, ByVal LeaveId As Integer) As Integer

            objDac = DAC.getDAC()
            Dim dtLeavePerWeek As New DataTable
            Try
                dtLeavePerWeek = objDac.GetDataTable(LeaveOccurancePerWeek_select, New SqlParameter("@FromDate", FromDate), _
                                                     New SqlParameter("@EmployeeId", EmployeeId), _
                                                     New SqlParameter("@LeaveTypeId", LeaveTypeId), _
                                                     New SqlParameter("@LeaveId", LeaveId))
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

        Public Function GetOccuranceForMonth(ByVal FromDate As DateTime, ByVal EmployeeId As Integer, ByVal LeaveTypeId As Integer, ByVal LeaveId As Integer) As Integer

            objDac = DAC.getDAC()
            Dim dtLeavePerWeek As New DataTable
            Try
                dtLeavePerWeek = objDac.GetDataTable(LeaveOccurancePerMonth_select, New SqlParameter("@FromDate", FromDate), _
                                                     New SqlParameter("@EmployeeId", EmployeeId), _
                                                     New SqlParameter("@LeaveTypeId", LeaveTypeId), _
                                                     New SqlParameter("@LeaveId", LeaveId))
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

        Public Function GetOccuranceForYear(ByVal FromDate As DateTime, ByVal EmployeeId As Integer, ByVal LeaveTypeId As Integer, ByVal LeaveId As Integer) As Integer

            objDac = DAC.getDAC()
            Dim dtLeavePerWeek As New DataTable
            Try
                dtLeavePerWeek = objDac.GetDataTable(LeaveOccurancePerYear_select, New SqlParameter("@FromDate", FromDate), _
                                                     New SqlParameter("@EmployeeId", EmployeeId), _
                                                     New SqlParameter("@LeaveTypeId", LeaveTypeId), _
                                                     New SqlParameter("@LeaveId", LeaveId))
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

        Public Function GetOccuranceForAllServiceTime(ByVal EmployeeId As Integer, ByVal LeaveTypeId As Integer, ByVal LeaveId As Integer) As Integer

            objDac = DAC.getDAC()
            Dim dtLeavePerWeek As New DataTable
            Try
                dtLeavePerWeek = objDac.GetDataTable(LeaveOccurancePerAllServiceTime_select, New SqlParameter("@EmployeeId", EmployeeId), _
                                                                                             New SqlParameter("@LeaveTypeId", LeaveTypeId), _
                                                                                             New SqlParameter("@LeaveId", LeaveId))
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

        Public Function GetByPK(ByVal LeaveId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(Emp_Leaves_Select, New SqlParameter("@LeaveId", LeaveId)).Rows(0)
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
                objColl = objDac.GetDataTable(Emp_Leaves_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Get_ByUserId_InnerPage(ByVal UserId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_Leaves_SelectAll_ByUserId_InnerPage, New SqlParameter("@UserId", UserId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAllLeavesByEmployee(ByVal FK_EmployeeId As Long, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal LeaveId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_Leaves_SelectAllByEmployee, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                              New SqlParameter("@FromDate", IIf(FromDate = DateTime.MinValue, DBNull.Value, FromDate)), _
                                              New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, DBNull.Value, ToDate)), _
                                              New SqlParameter("@LeaveId", LeaveId))
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
                objColl = objDac.GetDataTable(Emp_Leaves_BalanceHistory_Select_LastBalance, New SqlParameter("@CompanyId", CompanyId), _
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
                objRow = objDac.GetDataTable(Emp_Leaves_BalanceHistory_Select_LastBalance, New SqlParameter("@CompanyId", CompanyId), _
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
                objColl = objDac.GetDataTable(Emp_Leaves_SelectAllByEmployee, New SqlParameter("@FromDate", FromDate), New SqlParameter("@ToDate", ToDate))
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
                objColl = objDac.GetDataTable(Emp_Leave_GetCompany_Entity, New SqlParameter("@FK_EmployeeId", FK_EmployeeId))
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
                objColl = objDac.GetDataTable(Get_Emp_Leaves_ByMultiEmployees, New SqlParameter("@EmpIDs", EmpIDs))
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
                objColl = objDac.GetDataTable(Get_Emp_Leave_WithEmployeeInner, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Validate_RestDay(ByVal FK_EmployeeId As Integer, ByVal LeaveDate As DateTime, ByVal FK_LeaveTypeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_Leaves_Validate_RestDay, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                              New SqlParameter("@Date", LeaveDate), _
                                              New SqlParameter("@FK_LeaveTypeId", FK_LeaveTypeId))
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