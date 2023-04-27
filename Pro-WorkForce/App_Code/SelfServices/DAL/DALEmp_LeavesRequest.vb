Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.DB
Imports TA.Lookup
Imports SmartV.UTILITIES


Namespace TA.SelfServices

    Public Class DALEmp_LeavesRequest
        Inherits MGRBase

#Region "Class Variables"

        Private strConn As String
        Private Emp_LeavesRequest_Select As String = "Emp_LeavesRequest_select"
        Private Emp_LeavesRequest_Select_All As String = "Emp_LeavesRequest_select_All"
        Private Emp_LeavesRequest_Insert As String = "Emp_LeavesRequest_Insert"
        Private Emp_LeavesRequest_Update As String = "Emp_LeavesRequest_Update"
        Private Emp_LeavesRequest_Delete As String = "Emp_LeavesRequest_Delete"
        Private Emp_LeavesRequest_Select_ByEmp As String = "Emp_LeavesRequest_Select_ByEmp"
        Private Emp_LeavesRequest_Select_ByStatusType As String = "Emp_LeavesRequest_Select_ByStatusType"
        Private Emp_LeavesRequest_Select_ByDM As String = "Emp_LeavesRequest_Select_ByDM"
        Private Emp_LeavesRequest_UpdateStatus As String = "Emp_LeavesRequest_UpdateStatus"
        Private Emp_LeavesRequest_Select_ByHR As String = "Emp_LeavesRequest_Select_ByHR"
        Private Emp_LeavesRequest_Select_All_ByDM As String = "Emp_LeavesRequest_Select_All_ByDM"
        Private Emp_LeavesRequest_Select__All_ByHR As String = "Emp_LeavesRequest_Select__All_ByHR"
        Private Emp_LeavesRequest_Select_ByGM As String = "Emp_LeavesRequest_Select_ByGM"
        Private Emp_LeavesRequestViolation_Select_ByEmp As String = "Emp_LeavesRequestViolation_Select_ByEmp"
        Private LeaveRequestOccurancePerAllServiceTime_select As String = "LeaveRequestOccurancePerAllServiceTime_select"
        Private LeaveRequestOccurancePerMonth_select As String = "LeaveRequestOccurancePerMonth_select"
        Private LeaveRequestOccurancePerWeek_select As String = "LeaveRequestOccurancePerWeek_select"
        Private LeaveRequestOccurancePerYear_select As String = "LeaveRequestOccurancePerYear_select"
        Private LeavesTypes_Select_AllowedForSelfServiceAndGrade As String = "LeavesTypes_Select_AllowedForSelfServiceAndGrade"
        Private Emp_LeavesRequest_Select_ByDM_Mobile As String = "Emp_LeavesRequest_Select_ByDM_Mobile"
        Private Emp_LeavesRequest_Select_Approved_ByDM_Date_Mobile As String = "Emp_LeavesRequest_Select_Approved_ByDM_Date_Mobile"

#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef LeaveId As Integer, ByVal FK_EmployeeId As Long, ByVal FK_LeaveTypeId As Integer, ByVal RequestDate As DateTime, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal Remarks As String, ByVal IsHalfDay As Boolean, ByVal Days As Integer, ByVal AttachedFile As String, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal FK_StatusId As Integer, ByVal WithAdvanceSalary As Boolean, ByVal RequestedByCoordinator As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlParamLeaveId As New SqlParameter()
                sqlParamLeaveId.ParameterName = "@LeaveId"
                sqlParamLeaveId.Value = LeaveId
                sqlParamLeaveId.Direction = ParameterDirection.Output
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_LeavesRequest_Insert, _
               sqlParamLeaveId, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_LeaveTypeId", FK_LeaveTypeId), _
               New SqlParameter("@RequestDate", RequestDate), _
               New SqlParameter("@FromDate", FromDate), _
               New SqlParameter("@ToDate", ToDate), _
               New SqlParameter("@Remarks", Remarks), _
               New SqlParameter("@IsHalfDay", IsHalfDay), _
               New SqlParameter("@Days", Days), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@CREATED_DATE", IIf(CREATED_DATE = DateTime.MinValue, DBNull.Value, CREATED_DATE)), _
               New SqlParameter("@FK_StatusId", FK_StatusId), _
               New SqlParameter("@HasAdvancedSalary", WithAdvanceSalary), _
               New SqlParameter("@AttachedFile", IIf(AttachedFile = Nothing, DBNull.Value, AttachedFile)), _
               New SqlParameter("@RequestedByCoordinator", RequestedByCoordinator))

                LeaveId = sqlParamLeaveId.Value

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal LeaveId As Integer, ByVal FK_EmployeeId As Long, ByVal FK_LeaveTypeId As Integer, ByVal RequestDate As DateTime, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal Remarks As String, ByVal IsHalfDay As Boolean, ByVal Days As Integer, ByVal AttachedFile As String, ByVal LAST_UPDATE_BY As String, ByVal FK_StatusId As Integer, ByVal WithAdvanceSalary As Boolean, ByVal RequestedByCoordinator As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_LeavesRequest_Update, New SqlParameter("@LeaveId", LeaveId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_LeaveTypeId", FK_LeaveTypeId), _
               New SqlParameter("@RequestDate", RequestDate), _
               New SqlParameter("@FromDate", FromDate), _
               New SqlParameter("@ToDate", ToDate), _
               New SqlParameter("@Remarks", Remarks), _
               New SqlParameter("@IsHalfDay", IsHalfDay), _
               New SqlParameter("@Days", Days), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
               New SqlParameter("@FK_StatusId", FK_StatusId), _
               New SqlParameter("@HasAdvancedSalary", WithAdvanceSalary), _
               New SqlParameter("@AttachedFile", IIf(AttachedFile = Nothing, DBNull.Value, AttachedFile)), _
               New SqlParameter("@RequestedByCoordinator", RequestedByCoordinator))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function UpdateLeaveStatus(ByVal LeaveId As Integer, ByVal FK_StatusId As Integer, ByVal ReajectionReason As String, ByVal LAST_UPDATE_BY As String, ByVal FK_ManagerId As Integer, ByVal FK_HREmployeeId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_LeavesRequest_UpdateStatus, New SqlParameter("@LeaveId", LeaveId), _
               New SqlParameter("@FK_StatusId", FK_StatusId), _
               New SqlParameter("@RejectionReason", ReajectionReason), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
               New SqlParameter("@FK_ManagerId", FK_ManagerId), _
               New SqlParameter("@FK_HREmployeeId", FK_HREmployeeId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal LeaveId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_LeavesRequest_Delete, New SqlParameter("@LeaveId", LeaveId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal LeaveId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Emp_LeavesRequest_Select, New SqlParameter("@LeaveId", LeaveId)).Rows(0)
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
                objColl = objDac.GetDataTable(Emp_LeavesRequest_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetByEmployee(ByVal FK_EmployeeId As Integer, ByVal LeaveId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_LeavesRequest_Select_ByEmp, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
                                              New SqlParameter("@LeaveID", LeaveId),
                                              New SqlParameter("@FromDate", IIf(FromDate = DateTime.MinValue, DBNull.Value, FromDate)),
                                              New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, DBNull.Value, ToDate)))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetByStatusType(ByVal FK_EmployeeId As Integer, ByVal FK_StatusId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_LeavesRequest_Select_ByStatusType, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), New SqlParameter("@FK_StatusId", FK_StatusId),
                                              New SqlParameter("@FromDate", FromDate),
                                              New SqlParameter("@ToDate", ToDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetByDirectManager(ByVal FK_ManagerId As Integer, ByVal FK_StatusId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_LeavesRequest_Select_ByDM, New SqlParameter("@FK_ManagerId", FK_ManagerId), New SqlParameter("@FK_StatusId", FK_StatusId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetByHR(ByVal FK_HREmployeeId As Integer, ByVal FK_StatusId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_LeavesRequest_Select_ByHR, New SqlParameter("@FK_EmployeeHRId", FK_HREmployeeId), New SqlParameter("@FK_StatusId", FK_StatusId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetForEmpViolations(ByVal FK_EmployeeId As Integer, ByVal LeaveDate As DateTime) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Emp_LeavesRequest_Select, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), New SqlParameter("@LeaveDate", LeaveDate)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetAll_ByHR(ByVal FK_HREmployeeId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_LeavesRequest_Select__All_ByHR, New SqlParameter("@FK_EmployeeHRId", FK_HREmployeeId), _
                                              New SqlParameter("@FromDate", FromDate), _
                                              New SqlParameter("@ToDate", ToDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetAllByDirectManager(ByVal FK_ManagerId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_LeavesRequest_Select_All_ByDM, New SqlParameter("@FK_ManagerId", FK_ManagerId), _
                                              New SqlParameter("@FromDate", FromDate), _
                                              New SqlParameter("@ToDate", ToDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetByGeneralManager(ByVal FK_StatusId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_LeavesRequest_Select_ByGM, New SqlParameter("@FK_StatusId", FK_StatusId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetViolationRequestByEmployee(ByVal FK_EmployeeId As Integer, ByVal LeaveDate As DateTime) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_LeavesRequestViolation_Select_ByEmp, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                              New SqlParameter("@LeaveDate", IIf(LeaveDate = DateTime.MinValue, DBNull.Value, LeaveDate)))
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
                dtLeavePerWeek = objDac.GetDataTable(LeaveRequestOccurancePerWeek_select, New SqlParameter("@FromDate", FromDate), _
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
                dtLeavePerWeek = objDac.GetDataTable(LeaveRequestOccurancePerMonth_select, New SqlParameter("@FromDate", FromDate), _
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
                dtLeavePerWeek = objDac.GetDataTable(LeaveRequestOccurancePerYear_select, New SqlParameter("@FromDate", FromDate), _
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
                dtLeavePerWeek = objDac.GetDataTable(LeaveRequestOccurancePerAllServiceTime_select, New SqlParameter("@EmployeeId", EmployeeId), _
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

        Public Function GetAllowed_ForSelfServiceAndGrade(ByVal FK_EmployeeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(LeavesTypes_Select_AllowedForSelfServiceAndGrade, New SqlParameter("@FK_EmployeeId", FK_EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetByDirectManager_Mobile(ByVal FK_ManagerId As Integer, ByVal FK_StatusId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_LeavesRequest_Select_ByDM_Mobile, New SqlParameter("@FK_ManagerId", FK_ManagerId),
                                              New SqlParameter("@FK_StatusId", FK_StatusId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetApprovedByDirectManager_Date_Mobile(ByVal FK_ManagerId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_LeavesRequest_Select_Approved_ByDM_Date_Mobile, New SqlParameter("@FK_ManagerId", FK_ManagerId),
                                              New SqlParameter("@FromDate", FromDate),
                                              New SqlParameter("@ToDate", ToDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function
#End Region

    End Class
End Namespace