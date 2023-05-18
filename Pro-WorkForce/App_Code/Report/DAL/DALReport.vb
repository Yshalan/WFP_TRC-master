Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.LookUp



Namespace TA.Reports

    Public Class DALReport
        Inherits MGRBase


        Private strConn As String
        Private RptEmpDeductedBalance As String = "GetTotalDeductedPermissionsBalance" 'ID: M01 || Date: 15-05-2023 || By: Yahia shalan || Description: Defining a new report (Total Deducted Permissions Balance)'
        Private RptEmpMove As String = "RptEmpMove"
        Private RptEmployeeList As String = "rptEmployeeList"
        Private RptDetEmpMove As String = "RptDetEmpMove"
        Private RptDetEmpMove_SP As String = "RptDetEmpMove_SP"
        Private rptEmpLeaves As String = "rptEmpLeaves"
        Private RptEmpAbsent As String = "RptEmpAbsent"
        Private RptEmpExtraHours As String = "RptEmpExtraHours"
        Private RptViolations As String = "RptViolations"
        Private rptEmp_MoveInOut As String = "rptEmp_MoveInOut"
        Private RptSummary As String = "RptSummary"
        Private RptEmployee_Term_List As String = "RptEmployee_Term_List"
        Private rptEmpPermission As String = "rptEmpPermission"
        Private rptactualPermission As String = "Rpt_Actual_Permissions_LinkedwithSched"
        Private Rpt_Emp_Balance As String = "Rpt_Emp_Balance_TRC"
        'Private Rpt_Emp_Balance As String = "Rpt_Emp_Balance"
        Private Rpt_EmpLeaves_BalanceExpired As String = "Rpt_EmpLeaves_BalanceExpired"
        Private RptPermission_per_type As String = "RptPermission_per_type"
        Private RptNursing_Perm As String = "RptNursing_Perm"
        Private RptStudy_Perm As String = "RptStudy_Perm"
        Private rpt_Emp_Overtime As String = "rpt_Emp_Overtime"
        Private rpt_Emp_Basic_Overtime As String = "rpt_Emp_Basic_Overtime"
        Private rpt_TAViolations As String = "rpt_TAViolations"
        Private Rpt_LogicalAbsent As String = "Rpt_LogicalAbsent"
        Private rpt_InvalidAttempts As String = "rpt_InvalidAttempts"
        Private rpt_EventsLog As String = "rpt_EventsLog"
        Private rpt_EventGroups As String = "rpt_EventGroups"
        Private rpt_EventEmployees As String = "rpt_EventEmployees"
        Private rpt_EmployeesEvent_Shifts As String = "rpt_EmployeesEvent_Shifts"
        Private Emp_LeaveForm As String = "Emp_LeaveForm"
        Private Select_Invalid_EmployeeImg As String = "Select_Invalid_EmployeeImg"
        Private RptSummary_WithExpected As String = "RptSummary_WithExpected"
        Private RptSummary_GetUtilizationSummary As String = "RptSummary_UtilizetionWithExpected"
        Private rpt_DeputyManager As String = "rpt_DeputyManager"
        Private rpt_Detailed_Transactions As String = "rpt_Detailed_Transactions"
        Private Rpt_DetailedAccomodationTransactions As String = "Rpt_DetailedAccomodationTransactions"
        Private rpt_Detailed_Transactions_SP As String = "rpt_Detailed_Transactions_SP"
        Private Rpt_Delay As String = "Rpt_Delay"
        Private Rpt_EarlyOut As String = "Rpt_EarlyOut"
        Private Rpt_Incomplete_Trans As String = "Rpt_Incomplete_Trans"
        Private Rpt_DelayAndEarlyOut As String = "Rpt_DelayAndEarlyOut"
        Private RptManual_Entry As String = "RptManual_Entry"
        Private rpt_Detailed_TAViolations As String = "rpt_Detailed_TAViolations"
        Private rpt_Morpho_InvalidAttempts As String = "rpt_Morpho_InvalidAttempts"
        Private Select_MorphoInvalid_EmployeeImg As String = "Select_MorphoInvalid_EmployeeImg"
        Private Rpt_LateStudenets As String = "Rpt_LateStudenets"
        Private Rpt_SummaryStudentsStatus As String = "Rpt_SummaryStudentsStatus"
        Private Rpt_NoDays_Summary As String = "Rpt_NoDays_Summary"
        Private rpt_CardDetails As String = "rpt_CardDetails"
        Private rptEmpDetails As String = "rptEmpDetails"
        Private Rpt_TAExceptions As String = "Rpt_TAExceptions"
        Private rptEmpPermissionRequests As String = "rptEmpPermissionRequests"
        Private rpt_Percentage As String = "rpt_Percentage"
        Private Rpt_ShiftManager As String = "Rpt_ShiftManager"
        Private rpt_DNA_IN_OUT As String = "rpt_DNA_IN_OUT"
        Private rpt_DNA_DetailedTrans As String = "rpt_DNA_DetailedTrans"
        Private RptManual_Entry_Summary As String = "RptManual_Entry_Summary"
        Private RptEntity_TOTHrs As String = "RptEntity_TOTHrs"
        Private rpt_MonthlyDeduction As String = "rpt_MonthlyDeduction"
        Private rpt_OverTimeDeductLost As String = "rpt_OverTimeDeductLost"
        Private Rpt_EmployeeSchedules As String = "Rpt_EmployeeSchedules"
        Private rpt_ScheduleList As String = "rpt_ScheduleList"
        Private Rpt_ScheduleGroups As String = "Rpt_ScheduleGroups"
        Private rpt_Readers As String = "rpt_Readers"
        Private Rpt_ManualEntryRequest As String = "Rpt_ManualEntryRequest"
        Private rpt_EntityHierarchy As String = "rpt_EntityHierarchy"
        Private RptEmpTimeAttendance2 As String = "RptEmpTimeAttendance2"
        Private RptSummary_OrgMgr As String = "RptSummary_OrgMgr"
        Private rpt_ViolationExceptions As String = "rpt_ViolationExceptions"

        Private rpt_PayrollViolationToERP As String = "rpt_PayrollViolationToERP"

        Private rpt_DutyResumptionDetailed As String = "rpt_DutyResumptionDetailed"
        Private rpt_MonthlyDeduction_ByGrade As String = "rpt_MonthlyDeduction_ByGrade"
        Private rptEmpMove_OpenSchedule As String = "rptEmpMove_OpenSchedule"

        '--------Managers Reports--------'
        Private RptEmpMove_Mgr As String = "RptEmpMove_Mgr"
        Private RptSummary_Mgr As String = "RptSummary_Mgr"
        Private rptEmp_MoveInOut_Mgr As String = "rptEmp_MoveInOut_Mgr"
        Private RptViolations_Mgr As String = "RptViolations_Mgr"
        Private RptEmpExtraHours_Mgr As String = "RptEmpExtraHours_Mgr"
        Private RptEmpAbsent_Mgr As String = "RptEmpAbsent_Mgr"
        Private rptEmpLeaves_Mgr As String = "rptEmpLeaves_Mgr"
        Private RptDetEmpMove_Mgr As String = "RptDetEmpMove_Mgr"
        Private rptEmpPermission_Mgr As String = "rptEmpPermission_Mgr"
        Private Rpt_Emp_Balance_Mgr As String = "Rpt_Emp_Balance_Mgr"
        Private Rpt_EmpLeaves_BalanceExpired_Mgr As String = "Rpt_EmpLeaves_BalanceExpired_Mgr"
        Private rpt_Detailed_Transactions_Mgr As String = "rpt_Detailed_Transactions_Mgr"
        Private rpt_Detailed_Transactions_Mgr_SP As String = "rpt_Detailed_Transactions_Mgr_SP"
        Private rpt_Detailed_Transactions_OrgMgr_SP As String = "rpt_Detailed_Transactions_OrgMgr_SP"
        Private rpt_Detailed_TAViolations_Mgr As String = "rpt_Detailed_TAViolations_Mgr"
        Private RptEmpMove_Mgr_Service As String = "RptEmpMove_Mgr_Service"
        Private rpt_Detailed_TAViolations_Mgr_Service As String = "rpt_Detailed_TAViolations_Mgr_Service"
        Private RptViolations_Mgr_Service As String = "RptViolations_Mgr_Service"
        Private rpt_NotAttendEmployees_Mgr As String = "rpt_NotAttendEmployees_Mgr"
        Private RptViolations_Mgr2 As String = "RptViolations_Mgr2"
        '--------CDC Reports--------'
        Private rpt_CDC_AppSettings As String = "rpt_CDC_AppSettings"
        Private rpt_CDC_Emp_ATTCard As String = "rpt_CDC_Emp_ATTCard"
        Private rpt_CDC_Emp_OverTimeRule As String = "rpt_CDC_Emp_OverTimeRule"
        Private rpt_CDC_Emp_WorkSchedule As String = "rpt_CDC_Emp_WorkSchedule"
        Private rpt_CDC_Employee As String = "rpt_CDC_Employee"
        Private rpt_CDC_Employee_TAPolicy As String = "rpt_CDC_Employee_TAPolicy"
        Private rpt_CDC_LeaveTypeoccurance As String = "rpt_CDC_LeaveTypeoccurance"
        Private rpt_CDC_PermissionTypeDuration As String = "rpt_CDC_PermissionTypeDuration"
        Private rpt_CDC_PermissionTypeOccurance As String = "rpt_CDC_PermissionTypeOccurance"
        Private rpt_CDC_Schedule_Company As String = "rpt_CDC_Schedule_Company"
        Private rpt_CDC_Schedule_Entity As String = "rpt_CDC_Schedule_Entity"
        Private rpt_CDC_Schedule_LogicalGroup As String = "rpt_CDC_Schedule_LogicalGroup"
        Private rpt_CDC_Schedule_WorkLocation As String = "rpt_CDC_Schedule_WorkLocation"
        Private rpt_CDC_TAPolicy_AbsentRule As String = "rpt_CDC_TAPolicy_AbsentRule"
        Private rpt_CDC_TAPolicy_Violation As String = "rpt_CDC_TAPolicy_Violation"
        Private rptGet_OrganizationHierarchy As String = "Get_OrganizationHierarchy"


        Private rpt_Detailed_Transactions_With_MonthlyAllowance_Mgr As String = "rpt_Detailed_Transactions_With_MonthlyAllowance_Mgr"
        Private rpt_Manager As String = "rpt_Manager"
        Private RptDailyReport3 As String = "RptDailyReport3"
        Private RptGatesTrans As String = "RptGatesTrans"
        Private rptEmpDetails_photo As String = "rptEmpDetails_photo"
        Private rptEmpLeaves_perType As String = "rptEmpLeaves_perType"
        Private rpt_ShiftDetails As String = "rpt_ShiftDetails"
        Private rpt_ShiftDetails_ScheduleGroups As String = "rpt_ShiftDetails_ScheduleGroups"
        Private rpt_ShiftDetails_summary As String = "rpt_ShiftDetails_summary"
        Private Emp_OnCall As String = "Emp_OnCall"
        Private RptViolations_Hr_Service As String = "RptViolations_Hr_Service"
        Private RptEmpMove_Hr_Services As String = "RptEmpMove_Hr_Services"
        Private rptDecryptReaderXML As String = "rptDecryptReaderXML"
        Private rpt_AuthorityMovements As String = "rpt_AuthorityMovements"
        Private rptHeadCount As String = "rptHeadCount"
        Private rpt_TotalEffectiveWorkingHrs As String = "rpt_TotalEffectiveWorkingHrs"
        Private rpt_SummaryPayrollViolationToERP As String = "rpt_SummaryPayrollViolationToERP"
        Private rpt_Detailed_Transactions_SAI As String = "rpt_Detailed_Transactions_SAI"
        Private RptManagerDynamic As String = "RptManagerDynamic"
        Private RPT_Statistical_Movements As String = "RPT_Statistical_Movements"
        Private rptEmpPermission_PerStatus As String = "rptEmpPermission_PerStatus"
        Private rpt_LeavesDeduction_HRA As String = "rpt_LeavesDeduction_HRA"
        Private Rpt_EmpDiscipline As String = "Rpt_EmpDiscipline"
        Private RptEntityManagerDynamic As String = "RptEntityManagerDynamic"
        Private RptEmployeeDynamic As String = "RptEmployeeDynamic"
        Private RptHRDynamic As String = "RptHRDynamic"
        Private rptEmpAbsentCount As String = "rptEmpAbsentCount"
        Private rpt_PermissionRequests_Approval_AuditLog As String = "rpt_PermissionRequests_Approval_AuditLog"
        Private RptEmpAbsentPeriod As String = "RptEmpAbsentPeriod"
        Private RptEmplocations As String = "RptEmplocations"
        Private rpt_Employees_Multi_ReaderLocations As String = "rpt_Employees_Multi_ReaderLocations"
        Private rpt_EntityManagers As String = "rpt_EntityManagers"
        Private rpt_StudyNursing_Schedule As String = "rpt_StudyNursing_Schedule"
        Private rpt_ATSReader_Emp_Transaction As String = "rpt_ATSReader_Emp_Transaction"
        Private rpt_GatesTransaction_Detailed_AC As String = "rpt_GatesTransaction_Detailed_AC"
        Private rpt_GatesTransaction_Summary_AC As String = "rpt_GatesTransaction_Summary_AC"
        Private rpt_Detailed_Transactions_With_MonthlyAllowance As String = "rpt_Detailed_Transactions_With_MonthlyAllowance"
        Private RptEmpMove_FallOff As String = "RptEmpMove_FallOff"
        Private rpt_EmployeeWorkStatus_Days As String = "rpt_EmployeeWorkStatus_Days"
        Private rpt_AccomodationTransactions As String = "rpt_AccomodationTransactions"
        Private rpt_TransG_Invalid_Transactions As String = "rpt_TransG_Invalid_Transactions"
        Private RptEmpMove_MonthlyBucket As String = "RptEmpMove_MonthlyBucket"
        Private RptEmpMove_Invalid As String = "RptEmpMove_Invalid"
        Private rpt_Detailed_Transactions_Invalid As String = "rpt_Detailed_Transactions_Invalid"
        '--------------------Project Report----------------'
        Private rptProjectTasks As String = "rptProjectTasks"
        Private rptEmployeeProjectTasks As String = "rptEmployeeProjectTasks"
        '--------------------TRANS GUARD----------------'
        Private TransG_Devices_Select_All As String = "TransG_Devices_Select_All"
        '--------------------TRANS GUARD----------------'
        Private RptViolations_WithParam As String = "RptViolations_WithParam"
        Private RptEmpAbsent_Param As String = "RptEmpAbsent_Param"
        Private rpt_WorkHrs_Param As String = "rpt_WorkHrs_Param"
        Private RptSummary_Param As String = "RptSummary_Param"
        Private rpt_Appraisal_Detailed As String = "rpt_Appraisal_Detailed"
        Private rpt_Appraisal_EmployeeGoals_Sub As String = "rpt_Appraisal_EmployeeGoals_Sub"
        Private rpt_Appraisal_EmployeeSkills_Sub As String = "rpt_Appraisal_EmployeeSkills_Sub"
        Private Rpt_Incomplete_Trans_Advance As String = "Rpt_Incomplete_Trans_Advance"
        Private rpt_Notifications As String = "rpt_Notifications"
        Private RptEmpMove_Invalid_Mgr As String = "RptEmpMove_Invalid_Mgr"
        Private rpt_Detailed_Transactions_Invalid_Mgr As String = "rpt_Detailed_Transactions_Invalid_Mgr"
        Private RptViolations_WithParam_Mgr As String = "RptViolations_WithParam_Mgr"
        Private RptSummary_Param_Mgr As String = "RptSummary_Param_Mgr"
        Private rpt_IntegrationErrorLogs As String = "rpt_IntegrationErrorLogs"
        Private rpt_IntegrationMissingLeaves As String = "rpt_IntegrationMissingLeaves"
        Private rpt_Incomplete_WorkHrs As String = "rpt_Incomplete_WorkHrs"
        Private rpt_AttendanceReport_ADASI As String = "rpt_AttendanceReport_ADASI"
        Private rptDetaildStudyPermission As String = "rptDetaildStudyPermission"
        Private rptEmpLeaves_PerStatus As String = "rptEmpLeaves_PerStatus"
        Private RptViolations_Senaat As String = "RptViolations_Senaat"
        Private rpt_MonthlyApproveDeduction As String = "rpt_MonthlyApproveDeduction"
        Private rpt_DelayPermissions As String = "rpt_DelayPermissions"
        Private Emp_Cards_EmployeeDetails As String = "Emp_Cards_EmployeeDetails"
        Private rpt_EmployeeHoliday As String = "rpt_EmployeeHoliday"
        Private rpt_Emp_SysUsers As String = "rpt_Emp_SysUsers"
        Private rptSummary_WorkDays As String = "rptSummary_WorkDays"
        Private rpt_DelayEarlyOut_Summary As String = "rpt_DelayEarlyOut_Summary"
        Private rpt_EmpOutDuration As String = "rpt_EmpOutDuration"
        Private rpt_SummaryOutDuration As String = "rpt_SummaryOutDuration"
        Private rpt_EmpPermissionSummary As String = "rpt_EmpPermissionSummary"
        Private rpt_EmpLeaveSummary As String = "rpt_EmpLeaveSummary"
        Private RptGatesTrans_Mgr As String = "RptGatesTrans_Mgr"
        Private RptEmpDetailedTimeAttendance2 As String = "RptEmpDetailedTimeAttendance2"
        Private rpt_SummaryEventsLog As String = "rpt_SummaryEventsLog"
        Private rpt_Sub_Detailed_Transactions_With_MonthlyAllowance As String = "rpt_Sub_Detailed_Transactions_With_MonthlyAllowance"
        Private rptSummary_ByMonth As String = "rptSummary_ByMonth"
        Private rptViolations2 As String = "rptViolations2"
        Private rpt_SubAttendance_Summary_Absent As String = "rpt_SubAttendance_Summary_Absent"
        Private rpt_SubAttendance_Summary_Delay As String = "rpt_SubAttendance_Summary_Delay"
        Private rpt_Attendance_Summary As String = "rpt_Attendance_Summary" 'MANAFTH Daily Report For Entity Managers
        Private rpt_LeaveRequests_Approval_AuditLog As String = "rpt_LeaveRequests_Approval_AuditLog"
        Private rpt_Deduction_Per_Policy As String = "rpt_Deduction_Per_Policy"
        Private rpt_Detailed_Deduction_Per_Policy As String = "rpt_Detailed_Deduction_Per_Policy"
        Private Rpt_FCA_TransactionHistory As String = "Rpt_FCA_TransactionHistory"
        Private rpt_Leaves_Summary As String = "rpt_Leaves_Summary"
        Private rpt_Emp_Overtime_PerDayMinutes As String = "rpt_Emp_Overtime_PerDayMinutes"
        Private rptEmp_MoveInOut_MobileTransactions As String = "rptEmp_MoveInOut_MobileTransactions"
        Private rpt_VisitInfo As String = "rpt_VisitInfo"

        Public Sub New()
        End Sub

        Public Function Get_Readers() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(rpt_Readers, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_Hierarchy() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(rpt_EntityHierarchy, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetLeavesSummary(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(rpt_Leaves_Summary, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmployeeSchdules(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Rpt_EmployeeSchedules, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetSchedulesList(ByVal SchduleId As Integer, ByVal ScheduleTypeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(rpt_ScheduleList, New SqlParameter("@FK_ScheduleId", SchduleId),
                New SqlParameter("@ScheduleType", ScheduleTypeId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetSchedulesGroup(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal GroupScheduleId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Rpt_ScheduleGroups, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@GroupScheduleId", GroupScheduleId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_Overtime_DeductLostTime(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_OverTimeDeductLost, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_MonthlyDeduction(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_MonthlyDeduction, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_ApproveMonthlyDeduction(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_MonthlyApproveDeduction, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_MonthlyDeduction_ByGrade(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean, ByVal Emp_GradeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_MonthlyDeduction_ByGrade, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly),
                New SqlParameter("@Emp_GradeId", Emp_GradeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEntity_TOTHRS(ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptEntity_TOTHrs, New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetManualEntry_Summary(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptManual_Entry_Summary, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetVisitors(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_VisitInfo,
                                            New SqlParameter("@CompanyId", CompanyId),
                                            New SqlParameter("@FK_OrganizationId", EntityId),
                                            New SqlParameter("@FK_VisitorId", EmployeeId),
                                            New SqlParameter("@FROM_DATE", FROM_DATE),
                                            New SqlParameter("@TO_DATE", TO_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function


        Public Function GetFilterdEmpList(ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptEmployeeList, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetFilterdTerminatedEmpList(ByVal CompanyId As Integer, ByVal EntityId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptEmployee_Term_List, New SqlParameter("@CompanyId", CompanyId),
                        New SqlParameter("@EntityId", EntityId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmpTimeAttendance(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptEmpTimeAttendance2, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmpDetailedTimeAttendance(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptEmpDetailedTimeAttendance2, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetFilterdEmpMove(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptEmpMove, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetFilterdEmpMove_ADASI(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_AttendanceReport_ADASI, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_DetailedAppraisal(ByVal EmployeeId As Integer, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal Year As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_Appraisal_Detailed, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@Year", Year),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_DetailedAppraisal_Goals_Sub(ByVal EmployeeId As Integer, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal Year As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_Appraisal_EmployeeGoals_Sub, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@Year", Year),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_DetailedAppraisal_Skills_Sub(ByVal EmployeeId As Integer, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal Year As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_Appraisal_EmployeeSkills_Sub, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@Year", Year),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetFilterdEmpMove_Bucket(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptEmpMove_MonthlyBucket, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetFilterdEmpMove_Invalid(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptEmpMove_Invalid, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetFilterdEmpMove2(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptEmpTimeAttendance2, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmpLeaves(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal LeaveTypeId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rptEmpLeaves, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@LeaveTypeId", LeaveTypeId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmpPermissions(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal PermId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rptEmpPermission, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@PermId", PermId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetActualPermissions(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal PermId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rptactualPermission, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@Lang", 1))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetOfficalActualPermissions(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal PermId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("Rpt_Offical_Actual_Permissions_LinkedwithSched", New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@Lang", 1))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmpBalance(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Rpt_Emp_Balance, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmpBalance_DOF(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("Rpt_Emp_Balance_DOF", New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmpExpireBalance(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Rpt_EmpLeaves_BalanceExpired, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetExtraHours(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptEmpExtraHours, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetViolations(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptViolations,
                                            New SqlParameter("@EmployeeId", EmployeeId),
                                            New SqlParameter("@FROM_DATE", FROM_DATE),
                                            New SqlParameter("@TO_DATE", TO_DATE),
                                            New SqlParameter("@CompanyId", CompanyId),
                                            New SqlParameter("@EntityId", EntityId),
                                            New SqlParameter("@WorkLocationId", WorkLocationId),
                                            New SqlParameter("@LogicalGroupId", LogicalGroupId),
                                            New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetViolations2(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rptViolations2,
                                            New SqlParameter("@EmployeeId", EmployeeId),
                                            New SqlParameter("@FROM_DATE", FROM_DATE),
                                            New SqlParameter("@TO_DATE", TO_DATE),
                                            New SqlParameter("@CompanyId", CompanyId),
                                            New SqlParameter("@EntityId", EntityId),
                                            New SqlParameter("@WorkLocationId", WorkLocationId),
                                            New SqlParameter("@LogicalGroupId", LogicalGroupId),
                                            New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetViolations_Sennat(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptViolations_Senaat,
                                            New SqlParameter("@EmployeeId", EmployeeId),
                                            New SqlParameter("@FROM_DATE", FROM_DATE),
                                            New SqlParameter("@TO_DATE", TO_DATE),
                                            New SqlParameter("@CompanyId", CompanyId),
                                            New SqlParameter("@EntityId", EntityId),
                                            New SqlParameter("@WorkLocationId", WorkLocationId),
                                            New SqlParameter("@LogicalGroupId", LogicalGroupId),
                                            New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetViolations_Advance(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean, ByVal ViolationSelection As String, ByVal ViolationMinDelay As String, ByVal ViolationMinEarlyOut As String, ByVal ViolationDelaySelection As String, ByVal ViolationDelayNum As String, ByVal ViolationEarlyOutSelection As String, ByVal ViolationEarlyOutNum As String, ByVal ViolationAbsentSelection As String, ByVal ViolationAbsentNum As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptViolations_WithParam,
                                            New SqlParameter("@EmployeeId", EmployeeId),
                                            New SqlParameter("@FROM_DATE", FROM_DATE),
                                            New SqlParameter("@TO_DATE", TO_DATE),
                                            New SqlParameter("@CompanyId", CompanyId),
                                            New SqlParameter("@EntityId", EntityId),
                                            New SqlParameter("@WorkLocationId", WorkLocationId),
                                            New SqlParameter("@LogicalGroupId", LogicalGroupId),
                                            New SqlParameter("@DirectStaffOnly", DirectStaffOnly),
                                            New SqlParameter("@ViolationSelection", ViolationSelection),
                                            New SqlParameter("@Min_DelayValue", ViolationMinDelay),
                                            New SqlParameter("@Min_EarlyOutValue", ViolationMinEarlyOut),
                                            New SqlParameter("@Delay_Count_Selection", ViolationDelaySelection),
                                            New SqlParameter("@Delay_Count", ViolationDelayNum),
                                            New SqlParameter("@EarlyOut_Count_Selection", ViolationEarlyOutSelection),
                                            New SqlParameter("@EarlyOut_Count", ViolationEarlyOutNum),
                                            New SqlParameter("@Absent_Count_Selection", ViolationAbsentSelection),
                                            New SqlParameter("@Absent_Count", ViolationAbsentNum))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetDelay(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Rpt_Delay,
                                            New SqlParameter("@EmployeeId", EmployeeId),
                                            New SqlParameter("@FROM_DATE", FROM_DATE),
                                            New SqlParameter("@TO_DATE", TO_DATE),
                                            New SqlParameter("@CompanyId", CompanyId),
                                            New SqlParameter("@EntityId", EntityId),
                                            New SqlParameter("@WorkLocationId", WorkLocationId),
                                            New SqlParameter("@LogicalGroupId", LogicalGroupId),
                                            New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEarlyOut(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Rpt_EarlyOut,
                                            New SqlParameter("@EmployeeId", EmployeeId),
                                            New SqlParameter("@FROM_DATE", FROM_DATE),
                                            New SqlParameter("@TO_DATE", TO_DATE),
                                            New SqlParameter("@CompanyId", CompanyId),
                                            New SqlParameter("@EntityId", EntityId),
                                            New SqlParameter("@WorkLocationId", WorkLocationId),
                                            New SqlParameter("@LogicalGroupId", LogicalGroupId),
                                            New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmpInOut(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rptEmp_MoveInOut,
                                            New SqlParameter("@EmployeeId", EmployeeId),
                                            New SqlParameter("@FROM_DATE", FROM_DATE),
                                            New SqlParameter("@TO_DATE", TO_DATE),
                                            New SqlParameter("@CompanyId", CompanyId),
                                            New SqlParameter("@EntityId", EntityId),
                                            New SqlParameter("@WorkLocationId", WorkLocationId),
                                            New SqlParameter("@LogicalGroupId", LogicalGroupId),
                                            New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetManaualEntry(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptManual_Entry,
                                            New SqlParameter("@EmployeeId", EmployeeId),
                                            New SqlParameter("@FROM_DATE", FROM_DATE),
                                            New SqlParameter("@TO_DATE", TO_DATE),
                                            New SqlParameter("@CompanyId", CompanyId),
                                            New SqlParameter("@EntityId", EntityId),
                                            New SqlParameter("@WorkLocationId", WorkLocationId),
                                            New SqlParameter("@LogicalGroupId", LogicalGroupId),
                                            New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmpAbsent(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean, ByVal IncludeLogicalAbsent As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptEmpAbsent,
                                                New SqlParameter("@EmployeeId", EmployeeId),
                                                New SqlParameter("@FROM_DATE", FROM_DATE),
                                                New SqlParameter("@TO_DATE", TO_DATE),
                                                New SqlParameter("@CompanyId", CompanyId),
                                                New SqlParameter("@EntityId", EntityId),
                                                New SqlParameter("@WorkLocationId", WorkLocationId),
                                                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                                                New SqlParameter("@DirectStaffOnly", DirectStaffOnly),
                                                New SqlParameter("@IncludeLogicalAbsent", IncludeLogicalAbsent))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmpAbsent_Param(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean, ByVal Absent_Num As Integer, ByVal Absent_Num_Selection As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptEmpAbsent_Param,
                                                New SqlParameter("@EmployeeId", EmployeeId),
                                                New SqlParameter("@FROM_DATE", FROM_DATE),
                                                New SqlParameter("@TO_DATE", TO_DATE),
                                                New SqlParameter("@CompanyId", CompanyId),
                                                New SqlParameter("@EntityId", EntityId),
                                                New SqlParameter("@WorkLocationId", WorkLocationId),
                                                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                                                New SqlParameter("@DirectStaffOnly", DirectStaffOnly),
                                                New SqlParameter("@Absent_Num", Absent_Num),
                                                New SqlParameter("@Absent_Num_Selection", Absent_Num_Selection))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetFilterdDetEmpMove(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptDetEmpMove, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmpDeductedBalance(ByVal EmployeeId As Integer) As DataTable 'ID: M01 || Date: 15-05-2023 || By: Yahia shalan || Description: Defining a new report (Total Deducted Permissions Balance)'

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptEmpDeductedBalance, New SqlParameter("@EmployeeID", EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetFilterdDetEmpMove_SP(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptDetEmpMove_SP, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetSummary(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptSummary, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetSummary_Param(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean, ByVal Absent_Num_Selection As String, ByVal Absent_Num As String, ByVal WrkHour_Num_Selection As String, ByVal WrkHour_Num As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptSummary_Param, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly),
                New SqlParameter("@Absent_Num_Selection", IIf(Absent_Num_Selection = "", Nothing, Absent_Num_Selection)),
                New SqlParameter("@Absent_Num", Absent_Num),
                New SqlParameter("@WrkHour_Num_Selection", IIf(WrkHour_Num_Selection = "", Nothing, WrkHour_Num_Selection)),
                New SqlParameter("@WrkHour_Num", WrkHour_Num))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Summary_OrgMgr(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal EmpTypeId As Integer, ByVal ManagerId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptSummary_OrgMgr, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@EmpTypeId", EmpTypeId),
                New SqlParameter("@ManagerId", ManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetSummary_WithExpected(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptSummary_WithExpected, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetUtilizationSummary(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptSummary_GetUtilizationSummary, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetNursing_Permission(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptNursing_Perm, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetHoliday(ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("rpt_Holiday_Select_All",
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE)
               )
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetStudy_Permission(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptStudy_Perm, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetTA_Violations(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_TAViolations, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetPer_Permission(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal _Permission_id As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal ManualPermOnly As Boolean, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptPermission_per_type, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@Permid", _Permission_id),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@ManualPermOnly", ManualPermOnly),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmp_Overtime(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal ProcessStatus As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_Emp_Overtime, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@ProcessStatusId", ProcessStatus),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmp_Overtime_PerDayMinutes(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_Emp_Overtime_PerDayMinutes, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmp_BasicOvertime(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal ProcessStatus As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_Emp_Basic_Overtime, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@ProcessStatusId", ProcessStatus),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmp_LogicalAbsent(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Rpt_LogicalAbsent, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmp_InvalidAttempts(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_InvalidAttempts, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmp_Morpho_InvalidAttempts(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_Morpho_InvalidAttempts, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetApp_Event(ByVal Event_Form As String, ByVal ActionType As String, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal EmployeeId As Integer, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_EventsLog, New SqlParameter("@Form", Event_Form),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@ActionType", ActionType),
                New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEvent_Groups(ByVal EventId As Integer, ByVal GroupId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_EventGroups, New SqlParameter("@EventId", EventId),
                New SqlParameter("@GroupId", GroupId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEvent_Employees(ByVal EventId As Integer, ByVal GroupId As Integer, ByVal EmployeeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_EventEmployees, New SqlParameter("@EventId", EventId),
                New SqlParameter("@GroupId", GroupId),
                New SqlParameter("@EmployeeId", EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetProjectTasks(ByVal ProjectId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal IsCompleted As Boolean?) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rptProjectTasks, New SqlParameter("@ProjectId", ProjectId),
                New SqlParameter("@FromDate", FromDate),
                New SqlParameter("@ToDate", ToDate),
                New SqlParameter("@IsCompleted", IsCompleted))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmployeeProjectTasks(ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal IsCompleted As Boolean?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rptEmployeeProjectTasks, New SqlParameter("@FromDate", FromDate),
                New SqlParameter("@ToDate", ToDate),
                New SqlParameter("@IsCompleted", IsCompleted),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmployeeEvent_Shifts(ByVal EventId As Integer, ByVal GroupId As Integer, ByVal EmployeeId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_EmployeesEvent_Shifts, New SqlParameter("@EventId", EventId),
                New SqlParameter("@GroupId", GroupId),
                New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FromDate", FromDate),
                New SqlParameter("@ToDate", ToDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmp_LeaveForm(ByVal EmployeeId As Integer, ByVal LeaveId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_LeaveForm, New SqlParameter("@EmployeeId", EmployeeId),
                                              New SqlParameter("LeaveId", LeaveId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_Invalid_EmployeeImg(ByVal ID As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(Select_Invalid_EmployeeImg, New SqlParameter("@ID", ID)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function Get_MorphoInvalid_EmployeeImg(ByVal ID As String) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(Select_MorphoInvalid_EmployeeImg, New SqlParameter("@ID", ID)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetDeputy_Manager(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_DeputyManager, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetManager(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_Manager, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetDetailed_Transactions(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_Detailed_Transactions, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetDetailed_Transactions_AccomodationTransactions(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Rpt_DetailedAccomodationTransactions, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetDetailed_Transactions_SP(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_Detailed_Transactions_SP, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_IncompleteTrans(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Rpt_Incomplete_Trans,
                                            New SqlParameter("@EmployeeId", EmployeeId),
                                            New SqlParameter("@FROM_DATE", FROM_DATE),
                                            New SqlParameter("@TO_DATE", TO_DATE),
                                            New SqlParameter("@CompanyId", CompanyId),
                                            New SqlParameter("@EntityId", EntityId),
                                            New SqlParameter("@WorkLocationId", WorkLocationId),
                                            New SqlParameter("@LogicalGroupId", LogicalGroupId),
                                            New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_IncompleteTrans_Advance(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean, ByVal IncompleteTransSelection As String, ByVal IncompleteTransVal As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Rpt_Incomplete_Trans_Advance,
                                            New SqlParameter("@EmployeeId", EmployeeId),
                                            New SqlParameter("@FROM_DATE", FROM_DATE),
                                            New SqlParameter("@TO_DATE", TO_DATE),
                                            New SqlParameter("@CompanyId", CompanyId),
                                            New SqlParameter("@EntityId", EntityId),
                                            New SqlParameter("@WorkLocationId", WorkLocationId),
                                            New SqlParameter("@LogicalGroupId", LogicalGroupId),
                                            New SqlParameter("@DirectStaffOnly", DirectStaffOnly),
                                            New SqlParameter("@IncompleteTransSelection", IncompleteTransSelection),
                                            New SqlParameter("@IncompleteTransVal", IncompleteTransVal))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_DelayAndEarlyOut(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal TotalDelayEarlyOut As Integer, ByVal TotalCount As Integer, ByVal Type As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Rpt_DelayAndEarlyOut,
                                            New SqlParameter("@EmployeeId", EmployeeId),
                                            New SqlParameter("@FROM_DATE", FROM_DATE),
                                            New SqlParameter("@TO_DATE", TO_DATE),
                                            New SqlParameter("@CompanyId", CompanyId),
                                            New SqlParameter("@EntityId", EntityId),
                                            New SqlParameter("@WorkLocationId", WorkLocationId),
                                            New SqlParameter("@LogicalGroupId", LogicalGroupId),
                                            New SqlParameter("@TotalDelayEarlyOut", TotalDelayEarlyOut),
                                            New SqlParameter("@TotalCount", TotalCount),
                                            New SqlParameter("@Type", Type),
                                            New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_DetailedTAViolation(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_Detailed_TAViolations, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetLate_Students(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Rpt_LateStudenets, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetSummary_Students_Status(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Rpt_SummaryStudentsStatus, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetWork_Hrs(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Rpt_NoDays_Summary, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetWork_Hrs_Param(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean, ByVal Work_Hours_Selection As String, ByVal Work_Hours_Count As Integer, ByVal Work_Hours As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_WorkHrs_Param, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly),
                New SqlParameter("@Work_Hours_Selection", Work_Hours_Selection),
                New SqlParameter("@Work_Hours_Count", Work_Hours_Count),
                New SqlParameter("@Work_Hours", Work_Hours))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetCardDetails(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal Status As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_CardDetails, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@Status", Status),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmployeeDetails(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal FK_EmployeeTypeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rptEmpDetails, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@FK_EmployeeTypeId", FK_EmployeeTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmployeeDetailsWithPhoto(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal FK_EmployeeTypeId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rptEmpDetails_photo, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@FK_EmployeeTypeId", FK_EmployeeTypeId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_EmpTAExceptions(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Rpt_TAExceptions, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_DetailedPermissions(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rptEmpPermissionRequests, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_ShiftManagers(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Rpt_ShiftManager, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_TransPercentage(ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_Percentage, New SqlParameter("@FromDate", FROM_DATE),
                New SqlParameter("@ToDate", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_DNA_InOut(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_DNA_IN_OUT, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_DNA_DetailedTrans(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_DNA_DetailedTrans, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_ManualRequests(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal Status As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Rpt_ManualEntryRequest, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@Status", Status))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetDailyReport3(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptDailyReport3, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetGatesReport(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptGatesTrans, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmpLeaves_perType(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal LeaveTypeId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal ManualLeavesOnly As Boolean, ByVal DirectStaffOnly As Boolean, ByVal Days As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rptEmpLeaves_perType, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@LeaveTypeId", LeaveTypeId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@ManualLeavesOnly", ManualLeavesOnly),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly),
                 New SqlParameter("@Days", Days))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmpLeaves_perStatus(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal LeaveTypeId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal FK_StatusId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rptEmpLeaves_PerStatus, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@LeaveTypeId", LeaveTypeId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@FK_StatusId", FK_StatusId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEffectiveWorkHours(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_TotalEffectiveWorkingHrs, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))


            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmployeeViolationExceptions(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_ViolationExceptions, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetPayRollViolationToERP(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_PayrollViolationToERP, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmployeeDutyResumption(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_DutyResumptionDetailed, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetSummaryPayrollApproval(ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_SummaryPayrollViolationToERP, New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetDetailed_Transactions2(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_Detailed_Transactions_SAI, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetDetailed_Transactions_Mgr_SharePoint_OrgEntity(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal ManagerId As Integer, ByVal EmpTypeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("rpt_Detailed_Transactions_Mgr_SP_OrgEntity", New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@ManagerId", ManagerId),
                New SqlParameter("@EmpTypeId", EmpTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Function GetDetailed_Transactions_Mgr_SharePoint_VicePrincipal(EmployeeId As Integer, FROM_DATE As Date?, TO_DATE As Date?, CompanyId As Integer, EntityId As Integer, WorkLocationId As Integer, LogicalGroupId As Integer, ManagerId As Integer, EmpTypeId As Integer) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("rpt_Detailed_Transactions_Mgr_SP_VicePrinciple", New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@ManagerId", ManagerId),
                New SqlParameter("@EmpTypeId", EmpTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetPermission_PerStatus(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal FK_StatusId As Integer, ByVal PermId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rptEmpPermission_PerStatus,
                                                New SqlParameter("@EmployeeId", EmployeeId),
                                                New SqlParameter("@FROM_DATE", FROM_DATE),
                                                New SqlParameter("@TO_DATE", TO_DATE),
                                                New SqlParameter("@CompanyId", CompanyId),
                                                New SqlParameter("@EntityId", EntityId),
                                                New SqlParameter("@WorkLocationId", WorkLocationId),
                                                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                                                New SqlParameter("@FK_StatusId", FK_StatusId),
                                                New SqlParameter("@PermId", PermId),
                                                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetLeaveDeduction_HRA(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DeductionPolicy As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_LeavesDeduction_HRA,
                                                New SqlParameter("@EmployeeId", EmployeeId),
                                                New SqlParameter("@FROM_DATE", FROM_DATE),
                                                New SqlParameter("@TO_DATE", TO_DATE),
                                                New SqlParameter("@CompanyId", CompanyId),
                                                New SqlParameter("@EntityId", EntityId),
                                                New SqlParameter("@WorkLocationId", WorkLocationId),
                                                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                                                New SqlParameter("@DeductionPolicy", DeductionPolicy),
                                                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_EmpDiscipline(ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Rpt_EmpDiscipline,
                                                New SqlParameter("@FROM_DATE", FROM_DATE),
                                                New SqlParameter("@TO_DATE", TO_DATE),
                                                New SqlParameter("@CompanyId", CompanyId),
                                                New SqlParameter("@EntityId", EntityId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmpAbsent_Count(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rptEmpAbsentCount, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_PermissionRequests_Approval_AuditLog(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_PermissionRequests_Approval_AuditLog, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_Absent_Period(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptEmpAbsentPeriod, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_Emp_WorkLocations(ByVal EmployeeId As Integer, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptEmplocations, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_Emp_MultipleReader_Locations(ByVal EmployeeId As Integer, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_Employees_Multi_ReaderLocations, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GateDetailedTransactions_AC(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean, ByVal OuttimePolicy As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_GatesTransaction_Detailed_AC, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly),
                New SqlParameter("@OuttimePolicy", OuttimePolicy))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GateSummaryTransactions_AC(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean, ByVal OuttimePolicy As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_GatesTransaction_Summary_AC, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly),
                New SqlParameter("@OuttimePolicy", OuttimePolicy))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetDetailed_Transactions_With_MonthlyAllowance(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_Detailed_Transactions_With_MonthlyAllowance, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetDetailed_Transactions_Invalid(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_Detailed_Transactions_Invalid, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetFilterdEmpMove_FallOff(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptEmpMove_FallOff, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_TransG_InvalidTransactions(ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal Reader_Type As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_TransG_Invalid_Transactions, New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@Reader_Type", Reader_Type))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_rptNotifications(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean, ByVal FK_NotificationTypeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_Notifications,
                                            New SqlParameter("@EmployeeId", EmployeeId),
                                            New SqlParameter("@FROM_DATE", FROM_DATE),
                                            New SqlParameter("@TO_DATE", TO_DATE),
                                            New SqlParameter("@CompanyId", CompanyId),
                                            New SqlParameter("@EntityId", EntityId),
                                            New SqlParameter("@WorkLocationId", WorkLocationId),
                                            New SqlParameter("@LogicalGroupId", LogicalGroupId),
                                            New SqlParameter("@DirectStaffOnly", DirectStaffOnly),
                                            New SqlParameter("@FK_NotificationTypeId", FK_NotificationTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_IntegrationErrorLogs(ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_IntegrationErrorLogs, New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_IntegrationMissingLeaves(ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_IntegrationMissingLeaves, New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_Detailed_Work_Hrs(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean, ByVal Work_Hours As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_Incomplete_WorkHrs, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly),
                New SqlParameter("@Work_Hours", Work_Hours))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetDetailedStudy_Permission(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean, ByVal Semester As String, ByVal StudyYear As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rptDetaildStudyPermission, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly),
                New SqlParameter("@Semester", Semester),
                New SqlParameter("@StudyYear", StudyYear))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_DelayPermissions(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal PermId As Integer, ByVal DirectStaffOnly As Boolean, ByVal DelayCount As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_DelayPermissions,
                                                New SqlParameter("@EmployeeId", EmployeeId),
                                                New SqlParameter("@FROM_DATE", FROM_DATE),
                                                New SqlParameter("@TO_DATE", TO_DATE),
                                                New SqlParameter("@CompanyId", CompanyId),
                                                New SqlParameter("@EntityId", EntityId),
                                                New SqlParameter("@WorkLocationId", WorkLocationId),
                                                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                                                New SqlParameter("@DelayCount", DelayCount),
                                                New SqlParameter("@PermId", PermId),
                                                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_CardTemplate(ByVal EmployeeIds As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_Cards_EmployeeDetails,
                                                New SqlParameter("@EmployeeIds", EmployeeIds))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_rptHolidays(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean, ByVal FK_HolidayId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_EmployeeHoliday,
                                            New SqlParameter("@EmployeeId", EmployeeId),
                                            New SqlParameter("@FROM_DATE", FROM_DATE),
                                            New SqlParameter("@TO_DATE", TO_DATE),
                                            New SqlParameter("@CompanyId", CompanyId),
                                            New SqlParameter("@EntityId", EntityId),
                                            New SqlParameter("@WorkLocationId", WorkLocationId),
                                            New SqlParameter("@LogicalGroupId", LogicalGroupId),
                                            New SqlParameter("@DirectStaffOnly", DirectStaffOnly),
                                            New SqlParameter("@FK_HolidayId", FK_HolidayId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_rptEmpSysUsers(ByVal EmployeeId As Integer, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_Emp_SysUsers,
                                            New SqlParameter("@EmployeeId", EmployeeId),
                                            New SqlParameter("@CompanyId", CompanyId),
                                            New SqlParameter("@EntityId", EntityId),
                                            New SqlParameter("@WorkLocationId", WorkLocationId),
                                            New SqlParameter("@LogicalGroupId", LogicalGroupId),
                                            New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_Summary_WorkDays(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rptSummary_WorkDays, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_EmpOutDuration(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_EmpOutDuration, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_SummaryOutDuration(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_SummaryOutDuration, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_SummaryEmpPermission(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_EmpPermissionSummary, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_SummaryLeave(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_EmpLeaveSummary, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_DelayEarlyOut_Summary(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_DelayEarlyOut_Summary, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_SummaryEventsLog(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_SummaryEventsLog, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetSub_LeavesDetails(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, LimitDays As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_Sub_Detailed_Transactions_With_MonthlyAllowance,
                New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@LimitDays", LimitDays))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetSummary_ByMonth(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rptSummary_ByMonth, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_Emp_MoveInOut_MobileTransactions(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rptEmp_MoveInOut_MobileTransactions, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

#Region "Manager Reports"
        '-------------MANAFTH DAILY REPORT----------'
        Public Function GetAttendance_Summary(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal ManagerId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_Attendance_Summary, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@ManagerId", ManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetSubAttendance_Summary_Absent(ByVal FK_ManagerId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal EntityId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_SubAttendance_Summary_Absent, New SqlParameter("@ManagerId", FK_ManagerId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@EntityId", EntityId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetSubAttendance_Summary_Delay(ByVal FK_ManagerId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_SubAttendance_Summary_Delay, New SqlParameter("@ManagerId", FK_ManagerId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        '-------------MANAFTH DAILY REPORT----------'

        Public Function GetFilterdEmpMove_Mgr_Service(ByVal EmployeeId As Integer, ByVal ReportType As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptEmpMove_Mgr_Service, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@ReportType", ReportType))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function Get_DetailedTAViolation_Mgr_Service(ByVal EmployeeId As Integer, ByVal ReportType As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_Detailed_TAViolations_Mgr_Service, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@ReportType", ReportType))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function Get_Violation_Mgr_Service(ByVal EmployeeId As Integer, ByVal ReportType As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptViolations_Mgr_Service, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@ReportType", ReportType))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function Get_Violation_Hr_Service(ByVal EmployeeId As Integer, ByVal ReportType As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptViolations_Hr_Service, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@ReportType", ReportType))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetFilterdEmpMove_Hr_Service(ByVal EmployeeId As Integer, ByVal ReportType As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptEmpMove_Hr_Services, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@ReportType", ReportType))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function


        Public Function GetFilterdEmpMove_Mgr(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal ManagerId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptEmpMove_Mgr, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@ManagerId", ManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetSummary_Mgr(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal ManagerId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptSummary_Mgr, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@ManagerId", ManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmpInOut_Mgr(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal ManagerId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rptEmp_MoveInOut_Mgr,
                                            New SqlParameter("@EmployeeId", EmployeeId),
                                            New SqlParameter("@FROM_DATE", FROM_DATE),
                                            New SqlParameter("@TO_DATE", TO_DATE),
                                            New SqlParameter("@CompanyId", CompanyId),
                                            New SqlParameter("@EntityId", EntityId),
                                            New SqlParameter("@WorkLocationId", WorkLocationId),
                                            New SqlParameter("@LogicalGroupId", LogicalGroupId),
                                            New SqlParameter("@ManagerId", ManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetViolations_Mgr(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal ManagerId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptViolations_Mgr,
                                            New SqlParameter("@EmployeeId", EmployeeId),
                                            New SqlParameter("@FROM_DATE", FROM_DATE),
                                            New SqlParameter("@TO_DATE", TO_DATE),
                                            New SqlParameter("@CompanyId", CompanyId),
                                            New SqlParameter("@EntityId", EntityId),
                                            New SqlParameter("@WorkLocationId", WorkLocationId),
                                            New SqlParameter("@LogicalGroupId", LogicalGroupId),
                                            New SqlParameter("@ManagerId", ManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetViolations_Mgr2(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal ManagerId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptViolations_Mgr2,
                                            New SqlParameter("@EmployeeId", EmployeeId),
                                            New SqlParameter("@FROM_DATE", FROM_DATE),
                                            New SqlParameter("@TO_DATE", TO_DATE),
                                            New SqlParameter("@CompanyId", CompanyId),
                                            New SqlParameter("@EntityId", EntityId),
                                            New SqlParameter("@WorkLocationId", WorkLocationId),
                                            New SqlParameter("@LogicalGroupId", LogicalGroupId),
                                            New SqlParameter("@ManagerId", ManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetExtraHours_Mgr(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal ManagerId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptEmpExtraHours_Mgr, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@ManagerId", ManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmpAbsent_Mgr(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal ManagerId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptEmpAbsent_Mgr,
                                                New SqlParameter("@EmployeeId", EmployeeId),
                                                New SqlParameter("@FROM_DATE", FROM_DATE),
                                                New SqlParameter("@TO_DATE", TO_DATE),
                                                New SqlParameter("@CompanyId", CompanyId),
                                                New SqlParameter("@EntityId", EntityId),
                                                New SqlParameter("@WorkLocationId", WorkLocationId),
                                                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                                                New SqlParameter("@ManagerId", ManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmpLeaves_Mgr(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal LeaveTypeId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal ManagerId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rptEmpLeaves_Mgr, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@LeaveTypeId", LeaveTypeId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@ManagerId", ManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetFilterdDetEmpMove_Mgr(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal ManagerId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptDetEmpMove_Mgr, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@ManagerId", ManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmpPermissions_Mgr(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal PermId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal ManagerId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rptEmpPermission_Mgr, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@PermId", PermId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@ManagerId", ManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmpBalance_Mgr(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal ManagerId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Rpt_Emp_Balance_Mgr, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@ManagerId", ManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmpExpireBalance_Mgr(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal ManagerId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Rpt_EmpLeaves_BalanceExpired_Mgr, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@ManagerId", ManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetDetailed_Transactions_Mgr(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal ManagerId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_Detailed_Transactions_Mgr, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@ManagerId", ManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetDetailed_Transactions_Invalid_Mgr(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal ManagerId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_Detailed_Transactions_Invalid_Mgr, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@ManagerId", ManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetDetailed_Transactions_Mgr_SharePoint(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal ManagerId As Integer, ByVal EmpTypeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_Detailed_Transactions_Mgr_SP, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@ManagerId", ManagerId),
                New SqlParameter("@EmpTypeId", EmpTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetFilterdEmpMove_Invalid_Mgr(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal ManagerId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptEmpMove_Invalid_Mgr, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@ManagerId", ManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetViolations_Advance_Mgr(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean, ByVal ViolationSelection As String, ByVal ViolationMinDelay As String, ByVal ViolationMinEarlyOut As String, ByVal ViolationDelaySelection As String, ByVal ViolationDelayNum As String, ByVal ViolationEarlyOutSelection As String, ByVal ViolationEarlyOutNum As String, ByVal ViolationAbsentSelection As String, ByVal ViolationAbsentNum As String, ByVal ManagerId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptViolations_WithParam_Mgr,
                                            New SqlParameter("@EmployeeId", EmployeeId),
                                            New SqlParameter("@FROM_DATE", FROM_DATE),
                                            New SqlParameter("@TO_DATE", TO_DATE),
                                            New SqlParameter("@CompanyId", CompanyId),
                                            New SqlParameter("@EntityId", EntityId),
                                            New SqlParameter("@WorkLocationId", WorkLocationId),
                                            New SqlParameter("@LogicalGroupId", LogicalGroupId),
                                            New SqlParameter("@DirectStaffOnly", DirectStaffOnly),
                                            New SqlParameter("@ViolationSelection", ViolationSelection),
                                            New SqlParameter("@Min_DelayValue", ViolationMinDelay),
                                            New SqlParameter("@Min_EarlyOutValue", ViolationMinEarlyOut),
                                            New SqlParameter("@Delay_Count_Selection", ViolationDelaySelection),
                                            New SqlParameter("@Delay_Count", ViolationDelayNum),
                                            New SqlParameter("@EarlyOut_Count_Selection", ViolationEarlyOutSelection),
                                            New SqlParameter("@EarlyOut_Count", ViolationEarlyOutNum),
                                            New SqlParameter("@Absent_Count_Selection", ViolationAbsentSelection),
                                            New SqlParameter("@Absent_Count", ViolationAbsentNum),
                                            New SqlParameter("@ManagerId", ManagerId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetSummary_Param_Mgr(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean, ByVal Absent_Num_Selection As String, ByVal Absent_Num As String, ByVal WrkHour_Num_Selection As String, ByVal WrkHour_Num As String, ByVal ManagerId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptSummary_Param_Mgr, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly),
                New SqlParameter("@Absent_Num_Selection", IIf(Absent_Num_Selection = "", Nothing, Absent_Num_Selection)),
                New SqlParameter("@Absent_Num", Absent_Num),
                New SqlParameter("@WrkHour_Num_Selection", IIf(WrkHour_Num_Selection = "", Nothing, WrkHour_Num_Selection)),
                New SqlParameter("@WrkHour_Num", WrkHour_Num),
                New SqlParameter("@ManagerId", ManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetDetailed_Transactions_OrgMgr_SharePoint(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal ManagerId As Integer, ByVal EmpTypeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_Detailed_Transactions_OrgMgr_SP, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@ManagerId", ManagerId),
                New SqlParameter("@EmpTypeId", EmpTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_DetailedTAViolation_Mgr(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal ManagerId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_Detailed_TAViolations_Mgr, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@ManagerId", ManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetNotAttendanceEmployee_Mgr(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal ManagerId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_NotAttendEmployees_Mgr, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@ManagerId", ManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_OrganizationHierarchy(ByVal CompanyId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rptGet_OrganizationHierarchy, New SqlParameter("@FK_CompanyId", CompanyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmployeeShiftDetails(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_ShiftDetails, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))


            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmployeeShiftDetailsSummary(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal ScheduleTypeId As Integer, ByVal ShiftTypeId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_ShiftDetails_summary, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                 New SqlParameter("@ScheduleId", ScheduleTypeId),
                New SqlParameter("@ShiftId", ShiftTypeId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmployeeOnCallDetails(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_OnCall, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))


            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetDecryptedReaderDetails(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rptDecryptReaderXML, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FromDate", FROM_DATE),
                New SqlParameter("@ToDate", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))


            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmployeeShiftDetailsWithGroups(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_ShiftDetails_ScheduleGroups, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))


            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetAuthorityMovementsReport(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal AuthorityTypeId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_AuthorityMovements, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@AuthorityTypeId", AuthorityTypeId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetHeadCountReport(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rptHeadCount, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_EntityManagers(ByVal EmployeeId As Integer, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_EntityManagers, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_EmpStudyNursing_Schedule(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean, ByVal StudyNursing_Schedule As Integer, ByVal FK_ScheduleId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_StudyNursing_Schedule, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly),
                New SqlParameter("@StudyNursing_Schedule", StudyNursing_Schedule),
                New SqlParameter("@FK_ScheduleId", FK_ScheduleId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_ATSReader_Emp_Transaction(ByVal EmployeeId As Integer, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_ATSReader_Emp_Transaction, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_EmployeeMonthlySheet(ByVal EmployeeId As Integer, ByVal Month As Integer, ByVal Year As Integer, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_EmployeeWorkStatus_Days, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@Month", Month),
                New SqlParameter("@Year", Year),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetAccomodationTransactions(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean, ByVal OuttimePolicy As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_AccomodationTransactions, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly),
                New SqlParameter("@OuttimePolicy", OuttimePolicy))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetGatesReport_Mgr(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal ManagerId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptGatesTrans_Mgr, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@ManagerId", ManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetDetailed_TransactionsWithAllowance_Mgr(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal ManagerId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_Detailed_Transactions_With_MonthlyAllowance_Mgr, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@ManagerId", ManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_LeaveRequests_Approval_AuditLog(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_LeaveRequests_Approval_AuditLog, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_Deduction_Per_Policy(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_Deduction_Per_Policy, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_Detailed_Deduction_Per_Policy(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_Detailed_Deduction_Per_Policy, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_EmpMove_OpenSchedule(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rptEmpMove_OpenSchedule, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_FCA_TransactionHistory(ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Rpt_FCA_TransactionHistory,
                                            New SqlParameter("@CompanyId", CompanyId),
                                            New SqlParameter("@EntityId", EntityId),
                                            New SqlParameter("@WorkLocationId", WorkLocationId),
                                            New SqlParameter("@LogicalGroupId", LogicalGroupId),
                                            New SqlParameter("@EmployeeId", EmployeeId),
                                            New SqlParameter("@FROM_DATE", FROM_DATE),
                                            New SqlParameter("@TO_DATE", TO_DATE),
                                            New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

#End Region

#Region "CDC Reports"
        Public Function GetCDC_AppSettings(ByVal Operation As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_CDC_AppSettings, New SqlParameter("@Operation", Operation),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetCDC_Emp_ATTCard(ByVal Operation As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_CDC_Emp_ATTCard, New SqlParameter("@Operation", Operation),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetCDC_Emp_OverTimeRule(ByVal Operation As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_CDC_Emp_OverTimeRule, New SqlParameter("@Operation", Operation),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetCDC_Emp_WorkSchedule(ByVal Operation As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_CDC_Emp_WorkSchedule, New SqlParameter("@Operation", Operation),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetCDC_Employee(ByVal Operation As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_CDC_Employee, New SqlParameter("@Operation", Operation),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetCDC_Employee_TAPolicy(ByVal Operation As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_CDC_Employee_TAPolicy, New SqlParameter("@Operation", Operation),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetCDC_LeaveTypeoccurance(ByVal Operation As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_CDC_LeaveTypeoccurance, New SqlParameter("@Operation", Operation),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetCDC_PermissionTypeDuration(ByVal Operation As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_CDC_PermissionTypeDuration, New SqlParameter("@Operation", Operation),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetCDC_PermissionTypeOccurance(ByVal Operation As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_CDC_PermissionTypeOccurance, New SqlParameter("@Operation", Operation),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetCDC_Schedule_Company(ByVal Operation As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_CDC_Schedule_Company, New SqlParameter("@Operation", Operation),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetCDC_Schedule_Entity(ByVal Operation As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_CDC_Schedule_Entity, New SqlParameter("@Operation", Operation),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetCDC_Schedule_LogicalGroup(ByVal Operation As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_CDC_Schedule_LogicalGroup, New SqlParameter("@Operation", Operation),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetCDC_Schedule_WorkLocation(ByVal Operation As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_CDC_Schedule_WorkLocation, New SqlParameter("@Operation", Operation),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetCDC_TAPolicy_AbsentRule(ByVal Operation As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_CDC_TAPolicy_AbsentRule, New SqlParameter("@Operation", Operation),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetCDC_TAPolicy_Violation(ByVal Operation As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(rpt_CDC_TAPolicy_Violation, New SqlParameter("@Operation", Operation),
                New SqlParameter("@FROM_DATE", FROM_DATE),
                New SqlParameter("@TO_DATE", TO_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
#End Region

#Region "Manager Report Dynamic"

        Public Function ManagerReportDynamic(ByVal ManagerId As Integer, ByVal ReportType As String, ByVal ReportName As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptManagerDynamic, New SqlParameter("@ManagerId", ManagerId),
                New SqlParameter("@ReportType", ReportType),
                New SqlParameter("@ReportName", ReportName))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function EntityManagerReportDynamic(ByVal ManagerId As Integer, ByVal ReportType As String, ByVal ReportName As String, ByVal FK_EntityId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptEntityManagerDynamic, New SqlParameter("@ManagerId", ManagerId),
                New SqlParameter("@ReportType", ReportType),
                New SqlParameter("@ReportName", ReportName),
                New SqlParameter("@FK_EntityId", FK_EntityId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function EmployeeReportDynamic(ByVal EmployeeId As Integer, ByVal ReportType As String, ByVal ReportName As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptEmployeeDynamic, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@ReportType", ReportType),
                New SqlParameter("@ReportName", ReportName))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function HRReportDynamic(ByVal EmployeeId As Integer, ByVal ReportType As String, ByVal ReportName As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RptHRDynamic, New SqlParameter("@HREmployeeId", EmployeeId),
                New SqlParameter("@ReportType", ReportType),
                New SqlParameter("@ReportName", ReportName))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Email_Configuration_Select_Date_Manager_Reports(ByVal ReportType As String, ByVal ReportName As String, ByRef StartDate As DateTime, ByRef EndDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                Dim sqlOut1 = New SqlParameter("@TO_DATE", SqlDbType.DateTime, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, EndDate)
                Dim sqlOut2 = New SqlParameter("@FROM_DATE", SqlDbType.DateTime, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, StartDate)
                objColl = objDac.GetDataTable("Email_Configuration_Select_Date_Manager_Reports", New SqlParameter("@ReportType", ReportType),
                New SqlParameter("@ReportName", ReportName), sqlOut1, sqlOut2)
                If Not IsDBNull(sqlOut1.Value) Then
                    EndDate = sqlOut1.Value
                End If
                If Not IsDBNull(sqlOut2.Value) Then
                    StartDate = sqlOut2.Value
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

#End Region

#Region "Statistical_Movements"
        Public Function Statistical_Movements(ByVal EmployeeId As Integer, ByVal FROM_DATE As DateTime?, ByVal TO_DATE As DateTime?, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RPT_Statistical_Movements, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@FROMDATE", FROM_DATE),
                New SqlParameter("@TODATE", TO_DATE),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
#End Region

#Region "Trans Guard Readers"
        Public Function GetTransG_Devices() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(TransG_Devices_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

#End Region
    End Class

End Namespace
