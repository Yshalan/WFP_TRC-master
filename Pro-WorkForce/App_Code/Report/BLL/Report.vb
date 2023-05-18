Imports System.Data

Namespace TA.Reports

    Public Class Report
        Private _EmployeeId As Integer
        Private _EmployeeNo As String
        Private _FROM_DATE As DateTime?
        Private _TO_DATE As DateTime?
        Private _LimitDays As Integer
        Private _LeaveTypeId As Integer
        Private _CompanyId As Integer
        Private _EntityId As Integer
        Private _PermId As Integer
        Private _Permission_id As Integer
        Private _ProcessStatus As Integer
        Private _Event_Form As String
        Private _ActionType As String
        Private _Operation As Integer
        Private _CDC_Module As String
        Private _EventId As Integer
        Private _GroupId As Integer
        Private _WorkLocationId As Integer
        Private _LogicalGroupId As Integer
        Private _LeaveId As Integer
        Private _ID As Integer
        Private _EMP_IMAGE As String
        Private _Type As Integer
        Private _TotalDelayEarlyOut As Integer
        Private _TotalCount As Integer
        Private _Status As Integer
        Private _FK_EmployeeTypeId As Integer
        Private _ScheduleId As Integer
        Private _ScheduleTypeId As Integer
        Private _GroupScheduleId As Integer
        Private _ManagerId As Integer
        Private _ShiftTypeID As Integer
        Private _ReportType As String
        Private _Emp_GradeId As Integer
        Private _OuttimePolicy As Integer
        Private _AuthorityTypeId As Integer
        Private _EmpTypeId As Integer
        Private _ReportName As String
        Private _ManualPermOnly As Boolean
        Private _ManualLeavesOnly As Boolean
        Private _DeductionPolicy As Integer
        Private _DirectStaffOnly As Boolean
        Private _StudyNursing_Schedule As Integer
        Private _IncludeLogicalAbsent As Boolean
        Private _Month As Integer
        Private _Year As Integer
        Private _Reader_Type As String
        Private _ProjectId As Integer
        Private _IsCompleted As Boolean?
        Private _Absent_Count_Selection As String
        Private _Absent_Count As Integer
        Private _Work_Hours_Selection As String
        Private _Work_Hours_Count As Integer
        Private _Work_Hours As Integer
        Private _IncompleteTransSelection As String
        Private _IncompleteTransVal As String
        Private _FK_NotificationTypeId As Integer
        Private _Days As Integer
        '---Advance Violation Report Parameters---'
        Private _ViolationSelection As String
        Private _ViolationMinDelay As String
        Private _ViolationMinEarlyOut As String
        Private _ViolationDelaySelection As String
        Private _ViolationDelayNum As String
        Private _ViolationEarlyOutSelection As String
        Private _ViolationEarlyOutNum As String
        Private _ViolationAbsentSelection As String
        Private _ViolationAbsentNum As String
        '---Advance Violation Report Parameters---'

        Private _Semester As String
        Private _StudyYear As Integer
        Private _DelayCount As Integer
        Private _EmployeeIds As String
        Private _FK_HolidayId As Integer

        Private objDALReports As DALReport
        Public Sub New()

            objDALReports = New DALReport()

        End Sub

#Region "Properties"
        Public Property LeaveId() As Integer
            Set(ByVal value As Integer)
                _LeaveId = value
            End Set
            Get
                Return (_LeaveId)
            End Get
        End Property
        Public Property WorkLocationId() As Integer
            Set(ByVal value As Integer)
                _WorkLocationId = value
            End Set
            Get
                Return (_WorkLocationId)
            End Get
        End Property
        Public Property LogicalGroupId() As Integer
            Set(ByVal value As Integer)
                _LogicalGroupId = value
            End Set
            Get
                Return (_LogicalGroupId)
            End Get
        End Property
        Public Property PermId() As Integer
            Set(ByVal value As Integer)
                _PermId = value
            End Set
            Get
                Return (_PermId)
            End Get
        End Property
        Public Property EmployeeId() As Integer
            Set(ByVal value As Integer)
                _EmployeeId = value
            End Set
            Get
                Return (_EmployeeId)
            End Get
        End Property
        Public Property EmployeeNo() As String
            Set(ByVal value As String)
                _EmployeeNo = value
            End Set
            Get
                Return (_EmployeeNo)
            End Get
        End Property
        Public Property FROM_DATE() As DateTime
            Set(ByVal value As DateTime)
                _FROM_DATE = value
            End Set
            Get
                Return (_FROM_DATE)
            End Get
        End Property
        Public Property LimitDays() As Integer
            Set(ByVal value As Integer)
                _LimitDays = value
            End Set
            Get
                Return (_LimitDays)
            End Get
        End Property
        Public Property TO_DATE() As DateTime
            Set(ByVal value As DateTime)
                _TO_DATE = value
            End Set
            Get
                Return (_TO_DATE)
            End Get
        End Property
        Public Property ProjectId() As Integer
            Set(ByVal value As Integer)
                _ProjectId = value
            End Set
            Get
                Return (_ProjectId)
            End Get
        End Property
        Public Property Days() As Integer
            Set(ByVal value As Integer)
                _Days = value
            End Set
            Get
                Return (_Days)
            End Get
        End Property
        Public Property IsCompleted() As Boolean?
            Set(ByVal value As Boolean?)
                _IsCompleted = value
            End Set
            Get
                Return (_IsCompleted)
            End Get
        End Property
        Public Property CompanyId() As Integer
            Set(ByVal value As Integer)
                _CompanyId = value
            End Set
            Get
                Return (_CompanyId)
            End Get
        End Property
        Public Property EntityId() As Integer
            Set(ByVal value As Integer)
                _EntityId = value
            End Set
            Get
                Return (_EntityId)
            End Get
        End Property
        Public Property LeaveTypeId() As Integer
            Set(ByVal value As Integer)
                _LeaveTypeId = value
            End Set
            Get
                Return (_LeaveTypeId)
            End Get
        End Property
        Public Property Permission_id() As Integer
            Set(ByVal value As Integer)
                _Permission_id = value
            End Set
            Get
                Return (_Permission_id)
            End Get
        End Property
        Public Property ProcessStatus() As Integer
            Set(ByVal value As Integer)
                _ProcessStatus = value
            End Set
            Get
                Return (_ProcessStatus)
            End Get
        End Property
        Public Property EventForm() As String
            Set(ByVal value As String)
                _Event_Form = value
            End Set
            Get
                Return (_Event_Form)
            End Get
        End Property
        Public Property ActionType() As String
            Set(ByVal value As String)
                _ActionType = value
            End Set
            Get
                Return (_ActionType)
            End Get
        End Property
        Public Property Operation() As Integer
            Set(ByVal value As Integer)
                _Operation = value
            End Set
            Get
                Return (_Operation)
            End Get
        End Property
        Public Property CDC_Module() As String
            Set(ByVal value As String)
                _CDC_Module = value
            End Set
            Get
                Return (_CDC_Module)
            End Get
        End Property
        Public Property EventId() As Integer
            Set(ByVal value As Integer)
                _EventId = value
            End Set
            Get
                Return (_EventId)
            End Get
        End Property
        Public Property GroupId() As Integer
            Set(ByVal value As Integer)
                _GroupId = value
            End Set
            Get
                Return (_GroupId)
            End Get
        End Property
        Public Property ID() As Integer
            Set(ByVal value As Integer)
                _ID = value
            End Set
            Get
                Return (_ID)
            End Get
        End Property
        Public Property EMP_IMAGE() As String
            Set(ByVal value As String)
                _EMP_IMAGE = value
            End Set
            Get
                Return (_EMP_IMAGE)
            End Get
        End Property
        Public Property TotalDelayEarlyOut() As Integer
            Set(ByVal value As Integer)
                _TotalDelayEarlyOut = value
            End Set
            Get
                Return (_TotalDelayEarlyOut)
            End Get
        End Property
        Public Property TotalCount() As Integer
            Set(ByVal value As Integer)
                _TotalCount = value
            End Set
            Get
                Return (_TotalCount)
            End Get
        End Property
        Public Property Type() As Integer
            Set(ByVal value As Integer)
                _Type = value
            End Set
            Get
                Return (_Type)
            End Get
        End Property
        Public Property Status() As Integer
            Set(ByVal value As Integer)
                _Status = value
            End Set
            Get
                Return (_Status)
            End Get
        End Property
        Public Property FK_EmployeeTypeId() As Integer
            Set(ByVal value As Integer)
                _FK_EmployeeTypeId = value
            End Set
            Get
                Return (_FK_EmployeeTypeId)
            End Get
        End Property
        Public Property ScheduleId() As Integer
            Set(ByVal value As Integer)
                _ScheduleId = value
            End Set
            Get
                Return (_ScheduleId)
            End Get
        End Property
        Public Property ScheduleTypeId() As Integer
            Set(ByVal value As Integer)
                _ScheduleTypeId = value
            End Set
            Get
                Return (_ScheduleTypeId)
            End Get
        End Property
        Public Property GroupScheduleId() As Integer
            Set(ByVal value As Integer)
                _GroupScheduleId = value
            End Set
            Get
                Return (_GroupScheduleId)
            End Get
        End Property
        Public Property ManagerId() As Integer
            Set(ByVal value As Integer)
                _ManagerId = value
            End Set
            Get
                Return (_ManagerId)
            End Get
        End Property
        Public Property ShiftTypeID() As Integer
            Set(ByVal value As Integer)
                _ShiftTypeID = value
            End Set
            Get
                Return (_ShiftTypeID)
            End Get
        End Property
        Public Property ReportType() As String
            Set(ByVal value As String)
                _ReportType = value
            End Set
            Get
                Return (_ReportType)
            End Get
        End Property
        Public Property AuthorityTypeId() As Integer
            Set(ByVal value As Integer)
                _AuthorityTypeId = value
            End Set
            Get
                Return (_AuthorityTypeId)
            End Get
        End Property
        Public Property EmpTypeId() As Integer
            Set(ByVal value As Integer)
                _EmpTypeId = value
            End Set
            Get
                Return (_EmpTypeId)
            End Get
        End Property
        Public Property ReportName() As String
            Set(ByVal value As String)
                _ReportName = value
            End Set
            Get
                Return (_ReportName)
            End Get
        End Property
        Public Property ManualPermOnly() As Boolean
            Set(ByVal value As Boolean)
                _ManualPermOnly = value
            End Set
            Get
                Return (_ManualPermOnly)
            End Get
        End Property
        Public Property ManualLeavesOnly() As Boolean
            Set(ByVal value As Boolean)
                _ManualLeavesOnly = value
            End Set
            Get
                Return (_ManualLeavesOnly)
            End Get
        End Property
        Public Property DeductionPolicy() As Integer
            Set(ByVal value As Integer)
                _DeductionPolicy = value
            End Set
            Get
                Return (_DeductionPolicy)
            End Get
        End Property
        Public Property DirectStaffOnly() As Boolean
            Set(ByVal value As Boolean)
                _DirectStaffOnly = value
            End Set
            Get
                Return (_DirectStaffOnly)
            End Get
        End Property
        Public Property Emp_GradeId() As Integer
            Set(ByVal value As Integer)
                _Emp_GradeId = value
            End Set
            Get
                Return (_Emp_GradeId)
            End Get
        End Property
        Public Property StudyNursing_Schedule() As Integer
            Set(ByVal value As Integer)
                _StudyNursing_Schedule = value
            End Set
            Get
                Return (_StudyNursing_Schedule)
            End Get
        End Property
        Public Property IncludeLogicalAbsent() As Integer
            Set(ByVal value As Integer)
                _IncludeLogicalAbsent = value
            End Set
            Get
                Return (_IncludeLogicalAbsent)
            End Get
        End Property
        Public Property OuttimePolicy() As Integer
            Set(ByVal value As Integer)
                _OuttimePolicy = value
            End Set
            Get
                Return (_OuttimePolicy)
            End Get
        End Property
        Public Property Month() As Integer
            Set(ByVal value As Integer)
                _Month = value
            End Set
            Get
                Return (_Month)
            End Get
        End Property
        Public Property Year() As Integer
            Set(ByVal value As Integer)
                _Year = value
            End Set
            Get
                Return (_Year)
            End Get
        End Property
        Public Property Reader_Type() As String
            Set(ByVal value As String)
                _Reader_Type = value
            End Set
            Get
                Return (_Reader_Type)
            End Get
        End Property
        Public Property Absent_Count_Selection() As String
            Set(ByVal value As String)
                _Absent_Count_Selection = value
            End Set
            Get
                Return (_Absent_Count_Selection)
            End Get
        End Property
        Public Property Absent_Count() As Integer
            Set(ByVal value As Integer)
                _Absent_Count = value
            End Set
            Get
                Return (_Absent_Count)
            End Get
        End Property
        Public Property Work_Hours_Selection() As String
            Set(ByVal value As String)
                _Work_Hours_Selection = value
            End Set
            Get
                Return (_Work_Hours_Selection)
            End Get
        End Property
        Public Property Work_Hours_Count() As Integer
            Set(ByVal value As Integer)
                _Work_Hours_Count = value
            End Set
            Get
                Return (_Work_Hours_Count)
            End Get
        End Property
        Public Property Work_Hours() As Integer
            Set(ByVal value As Integer)
                _Work_Hours = value
            End Set
            Get
                Return (_Work_Hours)
            End Get
        End Property
        Public Property ViolationSelection() As String
            Set(ByVal value As String)
                _ViolationSelection = value
            End Set
            Get
                Return (_ViolationSelection)
            End Get
        End Property
        Public Property ViolationMinDelay() As String
            Set(ByVal value As String)
                _ViolationMinDelay = value
            End Set
            Get
                Return (_ViolationMinDelay)
            End Get
        End Property
        Public Property ViolationMinEarlyOut() As String
            Set(ByVal value As String)
                _ViolationMinEarlyOut = value
            End Set
            Get
                Return (_ViolationMinEarlyOut)
            End Get
        End Property
        Public Property ViolationDelaySelection() As String
            Set(ByVal value As String)
                _ViolationDelaySelection = value
            End Set
            Get
                Return (_ViolationDelaySelection)
            End Get
        End Property
        Public Property ViolationDelayNum() As String
            Set(ByVal value As String)
                _ViolationDelayNum = value
            End Set
            Get
                Return (_ViolationDelayNum)
            End Get
        End Property
        Public Property ViolationEarlyOutSelection() As String
            Set(ByVal value As String)
                _ViolationEarlyOutSelection = value
            End Set
            Get
                Return (_ViolationEarlyOutSelection)
            End Get
        End Property
        Public Property ViolationEarlyOutNum() As String
            Set(ByVal value As String)
                _ViolationEarlyOutNum = value
            End Set
            Get
                Return (_ViolationEarlyOutNum)
            End Get
        End Property
        Public Property ViolationAbsentSelection() As String
            Set(ByVal value As String)
                _ViolationAbsentSelection = value
            End Set
            Get
                Return (_ViolationAbsentSelection)
            End Get
        End Property
        Public Property ViolationAbsentNum() As String
            Set(ByVal value As String)
                _ViolationAbsentNum = value
            End Set
            Get
                Return (_ViolationAbsentNum)
            End Get
        End Property
        Public Property IncompleteTransSelection() As String
            Set(ByVal value As String)
                _IncompleteTransSelection = value
            End Set
            Get
                Return (_IncompleteTransSelection)
            End Get
        End Property
        Public Property IncompleteTransVal() As String
            Set(ByVal value As String)
                _IncompleteTransVal = value
            End Set
            Get
                Return (_IncompleteTransVal)
            End Get
        End Property
        Public Property FK_NotificationTypeId() As Integer
            Set(ByVal value As Integer)
                _FK_NotificationTypeId = value
            End Set
            Get
                Return (_FK_NotificationTypeId)
            End Get
        End Property
        Public Property Semester() As String
            Set(ByVal value As String)
                _Semester = value
            End Set
            Get
                Return (_Semester)
            End Get
        End Property
        Public Property StudyYear() As Integer
            Set(ByVal value As Integer)
                _StudyYear = value
            End Set
            Get
                Return (_StudyYear)
            End Get
        End Property
        Public Property DelayCount() As Integer
            Set(ByVal value As Integer)
                _DelayCount = value
            End Set
            Get
                Return (_DelayCount)
            End Get
        End Property
        Public Property EmployeeIds() As String
            Set(ByVal value As String)
                _EmployeeIds = value
            End Set
            Get
                Return (_EmployeeIds)
            End Get
        End Property
        Public Property FK_HolidayId() As Integer
            Set(ByVal value As Integer)
                _FK_HolidayId = value
            End Set
            Get
                Return (_FK_HolidayId)
            End Get
        End Property
#End Region

        Public Function Get_Hierarchy() As DataTable
            Return objDALReports.Get_Hierarchy()
        End Function
        Public Function Get_Readers() As DataTable
            Return objDALReports.Get_Readers()
        End Function
        Public Function GetLeavesSummary() As DataTable
            Return objDALReports.GetLeavesSummary(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetEmployeeSchdules() As DataTable
            Return objDALReports.GetEmployeeSchdules(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetSchedulesList() As DataTable
            Return objDALReports.GetSchedulesList(_ScheduleId, _ScheduleTypeId)
        End Function
        Public Function GetSchedulesGroup() As DataTable
            Return objDALReports.GetSchedulesGroup(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _GroupScheduleId, _DirectStaffOnly)
        End Function
        Public Function Get_Overtime_DeductLostTime() As DataTable
            Return objDALReports.Get_Overtime_DeductLostTime(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function Get_MonthlyDeduction() As DataTable
            Return objDALReports.Get_MonthlyDeduction(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function Get_ApproveMonthlyDeduction() As DataTable
            Return objDALReports.Get_ApproveMonthlyDeduction(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function Get_MonthlyDeduction_ByGrade() As DataTable
            Return objDALReports.Get_MonthlyDeduction_ByGrade(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly, _Emp_GradeId)
        End Function
        Public Function GetEntity_TOTHRS() As DataTable
            Return objDALReports.GetEntity_TOTHRS(_FROM_DATE, _TO_DATE, _CompanyId, _EntityId)
        End Function
        Public Function GetFilterdEmpList() As DataTable
            Return objDALReports.GetFilterdEmpList(_CompanyId, _EntityId, _EmployeeId, _FROM_DATE, _TO_DATE, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetFilterdTerminatedEmpList() As DataTable
            Return objDALReports.GetFilterdTerminatedEmpList(_CompanyId, _EntityId)
        End Function
        Public Function GetFilterdEmpMove() As DataTable
            Return objDALReports.GetFilterdEmpMove(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetFilterdEmpMove_ADASI() As DataTable
            Return objDALReports.GetFilterdEmpMove_ADASI(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function Get_DetailedAppraisal() As DataTable
            Return objDALReports.Get_DetailedAppraisal(_EmployeeId, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _Year, _DirectStaffOnly)
        End Function
        Public Function Get_DetailedAppraisal_Goals_Sub() As DataTable
            Return objDALReports.Get_DetailedAppraisal_Goals_Sub(_EmployeeId, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _Year, _DirectStaffOnly)
        End Function
        Public Function Get_DetailedAppraisal_Skills_Sub() As DataTable
            Return objDALReports.Get_DetailedAppraisal_Skills_Sub(_EmployeeId, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _Year, _DirectStaffOnly)
        End Function
        Public Function GetFilterdEmpMove_Bucket() As DataTable
            Return objDALReports.GetFilterdEmpMove_Bucket(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetFilterdEmpMove_Invalid() As DataTable
            Return objDALReports.GetFilterdEmpMove_Invalid(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetFilterdEmpMove2() As DataTable
            Return objDALReports.GetFilterdEmpMove2(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId)
        End Function
        Public Function GetEmpTimeAttendance() As DataTable
            Return objDALReports.GetEmpTimeAttendance(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetEmpDetailedTimeAttendance() As DataTable
            Return objDALReports.GetEmpDetailedTimeAttendance(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetFilterdDetEmpMove() As DataTable
            Return objDALReports.GetFilterdDetEmpMove(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetTotalDeductedPermissionsBalance() As DataTable 'ID: M01 || Date: 15-05-2023 || By: Yahia shalan || Description: Defining a new report (Total Deducted Permissions Balance)'
            Return objDALReports.GetEmpDeductedBalance(_EmployeeId)
        End Function
        Public Function GetFilterdDetEmpMove_SP() As DataTable
            Return objDALReports.GetFilterdDetEmpMove_SP(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId)
        End Function

        Public Function GetVisitors() As DataTable
            Return objDALReports.GetVisitors(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId)
        End Function
        Public Function GetEmpLeaves() As DataTable
            Return objDALReports.GetEmpLeaves(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _LeaveTypeId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetEmpPermissions() As DataTable
            Return objDALReports.GetEmpPermissions(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _PermId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetActualEmpPermissions() As DataTable
            Return objDALReports.GetActualPermissions(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _PermId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetOfficalActualEmpPermissions() As DataTable
            Return objDALReports.GetOfficalActualPermissions(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _PermId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetEmpBalance() As DataTable
            Return objDALReports.GetEmpBalance(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetEmpBalance_DOF() As DataTable
            Return objDALReports.GetEmpBalance_DOF(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetEmpExpireBalance() As DataTable
            Return objDALReports.GetEmpExpireBalance(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetEmpAbsent() As DataTable
            Return objDALReports.GetEmpAbsent(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly, _IncludeLogicalAbsent)
        End Function
        Public Function GetEmpAbsent_Param() As DataTable
            Return objDALReports.GetEmpAbsent_Param(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly, _Absent_Count, _Absent_Count_Selection)
        End Function
        Public Function GetExtraHours() As DataTable
            Return objDALReports.GetExtraHours(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetViolations() As DataTable
            Return objDALReports.GetViolations(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetViolations2() As DataTable
            Return objDALReports.GetViolations2(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetViolations_Sennat() As DataTable
            Return objDALReports.GetViolations_Sennat(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetViolations_Advance() As DataTable
            Return objDALReports.GetViolations_Advance(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly, _ViolationSelection, _ViolationMinDelay, _ViolationMinEarlyOut, _ViolationDelaySelection, _ViolationDelayNum, _ViolationEarlyOutSelection, _ViolationEarlyOutNum, _ViolationAbsentSelection, _ViolationAbsentNum)
        End Function
        Public Function GetDelay() As DataTable
            Return objDALReports.GetDelay(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetEarlyOut() As DataTable
            Return objDALReports.GetEarlyOut(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetEmpInOut() As DataTable
            Return objDALReports.GetEmpInOut(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function RptSummary_OrgMgr() As DataTable
            Return objDALReports.Summary_OrgMgr(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _EmpTypeId, _ManagerId)
        End Function
        Public Function GetSummary() As DataTable
            Return objDALReports.GetSummary(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetSummary_Param() As DataTable
            Return objDALReports.GetSummary_Param(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly, _Absent_Count_Selection, _Absent_Count, _Work_Hours_Selection, _Work_Hours)
        End Function
        Public Function GetSummary_WithExpected() As DataTable
            Return objDALReports.GetSummary_WithExpected(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetUtilizationSummary() As DataTable
            Return objDALReports.GetUtilizationSummary(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetNursing_Permission() As DataTable
            Return objDALReports.GetNursing_Permission(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetHoliday() As DataTable
            Return objDALReports.GetHoliday(_FROM_DATE, _TO_DATE)
        End Function
        Public Function GetStudy_Permission() As DataTable
            Return objDALReports.GetStudy_Permission(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetTA_Violations() As DataTable
            Return objDALReports.GetTA_Violations(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetPer_Permission() As DataTable
            Return objDALReports.GetPer_Permission(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _Permission_id, _WorkLocationId, _LogicalGroupId, _ManualPermOnly, _DirectStaffOnly)
        End Function
        Public Function GetEmp_Overtime() As DataTable
            Return objDALReports.GetEmp_Overtime(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _ProcessStatus, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetEmp_Overtime_PerDayMinutes() As DataTable
            Return objDALReports.GetEmp_Overtime_PerDayMinutes(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetEmp_BasicOvertime() As DataTable
            Return objDALReports.GetEmp_BasicOvertime(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _ProcessStatus, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetEmp_LogicalAbsent() As DataTable
            Return objDALReports.GetEmp_LogicalAbsent(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId)
        End Function
        Public Function GetEmp_InvalidAttempts() As DataTable
            Return objDALReports.GetEmp_InvalidAttempts(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetEmp_Morpho_InvalidAttempts() As DataTable
            Return objDALReports.GetEmp_Morpho_InvalidAttempts(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetApp_Event() As DataTable
            Return objDALReports.GetApp_Event(_Event_Form, _ActionType, _FROM_DATE, _TO_DATE, _EmployeeId, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId)
        End Function
        Public Function GetEvent_Groups() As DataTable
            Return objDALReports.GetEvent_Groups(_EventId, _GroupId)
        End Function
        Public Function GetEvent_Employees() As DataTable
            Return objDALReports.GetEvent_Employees(_EventId, _GroupId, _EmployeeId)
        End Function
        Public Function GetProjectTasks() As DataTable
            Return objDALReports.GetProjectTasks(_ProjectId, _FROM_DATE, _TO_DATE, _IsCompleted)
        End Function
        Public Function GetEmployeeProjectTasks() As DataTable
            Return objDALReports.GetEmployeeProjectTasks(_FROM_DATE, _TO_DATE, _IsCompleted, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetEmployeeEvent_Shifts() As DataTable
            Return objDALReports.GetEmployeeEvent_Shifts(_EventId, _GroupId, _EmployeeId, _FROM_DATE, _TO_DATE)
        End Function
        Public Function GetEmp_LeaveForm() As DataTable
            Return objDALReports.GetEmp_LeaveForm(_EmployeeId, _LeaveId)
        End Function
        Public Function GetGatesReport() As DataTable
            Return objDALReports.GetGatesReport(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetEmpLeaves_perType() As DataTable
            Return objDALReports.GetEmpLeaves_perType(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _LeaveTypeId, _WorkLocationId, _LogicalGroupId, _ManualLeavesOnly, _DirectStaffOnly, _Days)
        End Function
        Public Function GetEmpLeaves_perStatus() As DataTable
            Return objDALReports.GetEmpLeaves_perStatus(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _LeaveTypeId, _WorkLocationId, _LogicalGroupId, _Status, _DirectStaffOnly)
        End Function
        Public Function Get_Invalid_EmployeeImg() As DataRow
            Dim dr As DataRow
            dr = objDALReports.Get_Invalid_EmployeeImg(_ID)
            If Not dr Is Nothing Then
                If Not IsDBNull(dr("EMP_IMAGE")) Then
                    _EMP_IMAGE = dr("EMP_IMAGE")
                End If
                Return dr
            End If
        End Function
        Public Function Get_MorphoInvalid_EmployeeImg() As DataRow
            Dim dr As DataRow
            dr = objDALReports.Get_MorphoInvalid_EmployeeImg(_ID)
            If Not dr Is Nothing Then
                If Not IsDBNull(dr("Path")) Then
                    _EMP_IMAGE = dr("Path")
                End If
                Return dr
            End If
        End Function
        Public Function GetDeputy_Manager() As DataTable
            Return objDALReports.GetDeputy_Manager(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId)
        End Function
        Public Function GetManager() As DataTable
            Return objDALReports.GetManager(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetDailyReport3() As DataTable
            Return objDALReports.GetDailyReport3(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetDetailed_Transactions() As DataTable
            Return objDALReports.GetDetailed_Transactions(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetDetailed_Transactions_AccomodationTransactions() As DataTable
            Return objDALReports.GetDetailed_Transactions_AccomodationTransactions(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetDetailed_Transactions_SP() As DataTable
            Return objDALReports.GetDetailed_Transactions_SP(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId)
        End Function
        Public Function Get_IncompleteTrans() As DataTable
            Return objDALReports.Get_IncompleteTrans(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function Get_IncompleteTrans_Advance() As DataTable
            Return objDALReports.Get_IncompleteTrans_Advance(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly, _IncompleteTransSelection, _IncompleteTransVal)
        End Function
        Public Function Get_DelayAndEarlyOut() As DataTable
            Return objDALReports.Get_DelayAndEarlyOut(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _TotalDelayEarlyOut, _TotalCount, _Type, _DirectStaffOnly)
        End Function
        Public Function GetManaualEntry() As DataTable
            Return objDALReports.GetManaualEntry(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function Get_DetailedTAViolation() As DataTable
            Return objDALReports.Get_DetailedTAViolation(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetLate_Students() As DataTable
            Return objDALReports.GetLate_Students(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId)
        End Function
        Public Function GetSummary_Students_Status() As DataTable
            Return objDALReports.GetSummary_Students_Status(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId)
        End Function
        Public Function GetWork_Hrs() As DataTable
            Return objDALReports.GetWork_Hrs(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetWork_Hrs_Param() As DataTable
            Return objDALReports.GetWork_Hrs_Param(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly, _Work_Hours_Selection, _Work_Hours_Count, _Work_Hours)
        End Function
        Public Function Get_Detailed_Work_Hrs() As DataTable
            Return objDALReports.Get_Detailed_Work_Hrs(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly, _Work_Hours)
        End Function
        Public Function GetCardDetails() As DataTable
            Return objDALReports.GetCardDetails(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _Status, _DirectStaffOnly)
        End Function
        Public Function GetEmployeeDetailsWithPhoto() As DataTable
            Return objDALReports.GetEmployeeDetailsWithPhoto(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _FK_EmployeeTypeId, _DirectStaffOnly)
        End Function
        Public Function GetEmployeeDetails() As DataTable
            Return objDALReports.GetEmployeeDetails(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _FK_EmployeeTypeId)
        End Function
        Public Function Get_EmpTAExceptions() As DataTable
            Return objDALReports.Get_EmpTAExceptions(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function Get_DetailedPermissions() As DataTable
            Return objDALReports.Get_DetailedPermissions(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function Get_TransPercentage() As DataTable
            Return objDALReports.Get_TransPercentage(_FROM_DATE, _TO_DATE, _CompanyId, _EntityId)
        End Function
        Public Function Get_ShiftManagers() As DataTable
            Return objDALReports.Get_ShiftManagers(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function Get_DNA_InOut() As DataTable
            Return objDALReports.Get_DNA_InOut(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function Get_DNA_DetailedTrans() As DataTable
            Return objDALReports.Get_DNA_DetailedTrans(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetManualEntry_Summary() As DataTable
            Return objDALReports.GetManualEntry_Summary(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function Get_ManualRequests() As DataTable
            Return objDALReports.Get_ManualRequests(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _Status)
        End Function
        Public Function Get_OrganizationHierarchy() As DataTable
            Return objDALReports.Get_OrganizationHierarchy(_CompanyId)
        End Function
        Public Function GetEmployeeShiftDetails() As DataTable
            Return objDALReports.GetEmployeeShiftDetails(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetEmployeeShiftDetailsSummary() As DataTable
            Return objDALReports.GetEmployeeShiftDetailsSummary(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _ScheduleTypeId, _ShiftTypeID, _DirectStaffOnly)
        End Function
        Public Function GetEmployeeOnCallDetails() As DataTable
            Return objDALReports.GetEmployeeOnCallDetails(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetFilterdEmpMove_Hr_Service() As DataTable
            Return objDALReports.GetFilterdEmpMove_Hr_Service(_EmployeeId, _ReportType)
        End Function
        Public Function Get_Violation_Hr_Service() As DataTable
            Return objDALReports.Get_Violation_Hr_Service(_EmployeeId, _ReportType)
        End Function
        Public Function GetDecryptedReaderDetails() As DataTable
            Return objDALReports.GetDecryptedReaderDetails(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetEmployeeShiftDetailsWithGroups() As DataTable
            Return objDALReports.GetEmployeeShiftDetailsWithGroups(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function

        Public Function GetAuthorityMovementsReport() As DataTable
            Return objDALReports.GetAuthorityMovementsReport(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _AuthorityTypeId, _DirectStaffOnly)
        End Function
        Public Function GetHeadCountReport() As DataTable
            Return objDALReports.GetHeadCountReport(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetEffectiveWorkHours() As DataTable
            Return objDALReports.GetEffectiveWorkHours(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function

        Public Function GetEmployeeViolationExceptions() As DataTable
            Return objDALReports.GetEmployeeViolationExceptions(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetPayRollViolationToERP() As DataTable
            Return objDALReports.GetPayRollViolationToERP(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetEmployeeDutyResumption() As DataTable
            Return objDALReports.GetEmployeeDutyResumption(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetSummaryPayrollApproval() As DataTable
            Return objDALReports.GetSummaryPayrollApproval(_FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _DirectStaffOnly)
        End Function
        Public Function GetDetailed_Transactions2() As DataTable
            Return objDALReports.GetDetailed_Transactions2(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetPermission_PerStatus() As DataTable
            Return objDALReports.GetPermission_PerStatus(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _Status, _PermId, _DirectStaffOnly)
        End Function
        Public Function GetLeaveDeduction_HRA() As DataTable
            Return objDALReports.GetLeaveDeduction_HRA(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DeductionPolicy, _DirectStaffOnly)
        End Function
        Public Function Get_EmpDiscipline() As DataTable
            Return objDALReports.Get_EmpDiscipline(_FROM_DATE, _TO_DATE, _CompanyId, _EntityId)
        End Function
        Public Function GetEmpAbsent_Count() As DataTable
            Return objDALReports.GetEmpAbsent_Count(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function Get_PermissionRequests_Approval_AuditLog() As DataTable
            Return objDALReports.Get_PermissionRequests_Approval_AuditLog(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function Get_LeaveRequests_Approval_AuditLog() As DataTable
            Return objDALReports.Get_LeaveRequests_Approval_AuditLog(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function Get_Deduction_Per_Policy() As DataTable
            Return objDALReports.Get_Deduction_Per_Policy(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function Get_Detailed_Deduction_Per_Policy() As DataTable
            Return objDALReports.Get_Detailed_Deduction_Per_Policy(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function Get_EmpMove_OpenSchedule() As DataTable
            Return objDALReports.Get_EmpMove_OpenSchedule(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function Get_Absent_Period() As DataTable
            Return objDALReports.Get_Absent_Period(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function Get_Emp_WorkLocations() As DataTable
            Return objDALReports.Get_Emp_WorkLocations(_EmployeeId, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function Get_Emp_MultipleReader_Locations() As DataTable
            Return objDALReports.Get_Emp_MultipleReader_Locations(_EmployeeId, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function Get_EntityManagers() As DataTable
            Return objDALReports.Get_EntityManagers(_EmployeeId, _CompanyId, _EntityId, _DirectStaffOnly)
        End Function
        Public Function Get_EmpStudyNursing_Schedule() As DataTable
            Return objDALReports.Get_EmpStudyNursing_Schedule(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly, _StudyNursing_Schedule, _ScheduleId)
        End Function
        Public Function Get_ATSReader_Emp_Transaction() As DataTable
            Return objDALReports.Get_ATSReader_Emp_Transaction(_EmployeeId, _CompanyId, _EntityId, _DirectStaffOnly)
        End Function
        Public Function Get_EmployeeMonthlySheet() As DataTable
            Return objDALReports.Get_EmployeeMonthlySheet(_EmployeeId, _Month, _Year, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GateDetailedTransactions_AC() As DataTable
            Return objDALReports.GateDetailedTransactions_AC(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly, _OuttimePolicy)
        End Function
        Public Function GateSummaryTransactions_AC() As DataTable
            Return objDALReports.GateSummaryTransactions_AC(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly, _OuttimePolicy)
        End Function
        Public Function GetDetailed_Transactions_With_MonthlyAllowance() As DataTable
            Return objDALReports.GetDetailed_Transactions_With_MonthlyAllowance(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetFilterdEmpMove_FallOff() As DataTable
            Return objDALReports.GetFilterdEmpMove_FallOff(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetAccomodationTransactions() As DataTable
            Return objDALReports.GetAccomodationTransactions(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly, _OuttimePolicy)
        End Function
        Public Function Get_TransG_InvalidTransactions() As DataTable
            Return objDALReports.Get_TransG_InvalidTransactions(_FROM_DATE, _TO_DATE, _Reader_Type)
        End Function
        Public Function GetDetailed_Transactions_Invalid() As DataTable
            Return objDALReports.GetDetailed_Transactions_Invalid(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function Get_rptNotifications() As DataTable
            Return objDALReports.Get_rptNotifications(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly, _FK_NotificationTypeId)
        End Function
        Public Function Get_IntegrationErrorLogs() As DataTable
            Return objDALReports.Get_IntegrationErrorLogs(_FROM_DATE, _TO_DATE)
        End Function
        Public Function Get_IntegrationMissingLeaves() As DataTable
            Return objDALReports.Get_IntegrationMissingLeaves(_FROM_DATE, _TO_DATE)
        End Function
        Public Function GetDetailedStudy_Permission() As DataTable
            Return objDALReports.GetDetailedStudy_Permission(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly, _Semester, _StudyYear)
        End Function
        Public Function Get_DelayPermissions() As DataTable
            Return objDALReports.Get_DelayPermissions(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _PermId, _DirectStaffOnly, _DelayCount)
        End Function
        Public Function Get_CardTemplate() As DataTable
            Return objDALReports.Get_CardTemplate(_EmployeeIds)
        End Function
        Public Function Get_rptHolidays() As DataTable
            Return objDALReports.Get_rptHolidays(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly, _FK_HolidayId)
        End Function
        Public Function Get_rptEmpSysUsers() As DataTable
            Return objDALReports.Get_rptEmpSysUsers(_EmployeeId, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function Get_Summary_WorkDays() As DataTable
            Return objDALReports.Get_Summary_WorkDays(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function Get_EmpOutDuration() As DataTable
            Return objDALReports.Get_EmpOutDuration(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function Get_SummaryOutDuration() As DataTable
            Return objDALReports.Get_SummaryOutDuration(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function Get_SummaryEmpPermission() As DataTable
            Return objDALReports.Get_SummaryEmpPermission(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function Get_SummaryLeave() As DataTable
            Return objDALReports.Get_SummaryLeave(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function Get_DelayEarlyOut_Summary() As DataTable
            Return objDALReports.Get_DelayEarlyOut_Summary(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function Get_SummaryEventsLog() As DataTable
            Return objDALReports.Get_SummaryEventsLog(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
        Public Function GetSub_LeavesDetails() As DataTable
            Return objDALReports.GetSub_LeavesDetails(_EmployeeId, _FROM_DATE, _TO_DATE, LimitDays)
        End Function
        Public Function GetSummary_ByMonth() As DataTable
            Return objDALReports.GetSummary_ByMonth(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function

        Public Function Get_Emp_MoveInOut_MobileTransactions() As DataTable
            Return objDALReports.Get_Emp_MoveInOut_MobileTransactions(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function

        Public Function Get_FCA_TransactionHistory() As DataTable
            Return objDALReports.Get_FCA_TransactionHistory(_CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _EmployeeId, _FROM_DATE, _TO_DATE, _DirectStaffOnly)
        End Function

#Region "Manager Reports"
        Public Function GetFilterdEmpMove_Mgr_Service() As DataTable
            Return objDALReports.GetFilterdEmpMove_Mgr_Service(_EmployeeId, _ReportType)
        End Function
        Public Function Get_DetailedTAViolation_Mgr_Service() As DataTable
            Return objDALReports.Get_DetailedTAViolation_Mgr_Service(_EmployeeId, _ReportType)
        End Function
        Public Function Get_Violation_Mgr_Service() As DataTable
            Return objDALReports.Get_Violation_Mgr_Service(_EmployeeId, _ReportType)
        End Function
        Public Function GetFilterdEmpMove_Mgr() As DataTable
            Return objDALReports.GetFilterdEmpMove_Mgr(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _ManagerId)
        End Function
        Public Function GetFilterdEmpMove_Invalid_Mgr() As DataTable
            Return objDALReports.GetFilterdEmpMove_Invalid_Mgr(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _ManagerId)
        End Function
        Public Function GetSummary_Mgr() As DataTable
            Return objDALReports.GetSummary_Mgr(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _ManagerId)
        End Function
        Public Function GetEmpInOut_Mgr() As DataTable
            Return objDALReports.GetEmpInOut_Mgr(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _ManagerId)
        End Function
        Public Function GetViolations_Mgr() As DataTable
            Return objDALReports.GetViolations_Mgr(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _ManagerId)
        End Function
        Public Function GetExtraHours_Mgr() As DataTable
            Return objDALReports.GetExtraHours_Mgr(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _ManagerId)
        End Function
        Public Function GetEmpAbsent_Mgr() As DataTable
            Return objDALReports.GetEmpAbsent_Mgr(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _ManagerId)
        End Function
        Public Function GetEmpLeaves_Mgr() As DataTable
            Return objDALReports.GetEmpLeaves_Mgr(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _LeaveTypeId, _WorkLocationId, _LogicalGroupId, _ManagerId)
        End Function
        Public Function GetFilterdDetEmpMove_Mgr() As DataTable
            Return objDALReports.GetFilterdDetEmpMove_Mgr(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _ManagerId)
        End Function
        Public Function GetEmpPermissions_Mgr() As DataTable
            Return objDALReports.GetEmpPermissions_Mgr(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _PermId, _WorkLocationId, _LogicalGroupId, _ManagerId)
        End Function
        Public Function GetEmpBalance_Mgr() As DataTable
            Return objDALReports.GetEmpBalance_Mgr(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _ManagerId)
        End Function
        Public Function GetEmpExpireBalance_Mgr() As DataTable
            Return objDALReports.GetEmpExpireBalance_Mgr(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _ManagerId)
        End Function
        Public Function GetDetailed_Transactions_Mgr() As DataTable
            Return objDALReports.GetDetailed_Transactions_Mgr(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _ManagerId)
        End Function
        Public Function GetDetailed_Transactions_Invalid_Mgr() As DataTable
            Return objDALReports.GetDetailed_Transactions_Invalid_Mgr(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _ManagerId)
        End Function
        Public Function GetDetailed_Transactions_Mgr_SP() As DataTable
            Return objDALReports.GetDetailed_Transactions_Mgr_SharePoint(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _ManagerId, _EmpTypeId)
        End Function
        Public Function GetDetailed_Transactions_Mgr_SP_OrgEntity() As DataTable
            Return objDALReports.GetDetailed_Transactions_Mgr_SharePoint_OrgEntity(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _ManagerId, _EmpTypeId)
        End Function
        Public Function GetDetailed_Transactions_OrgMgr_SP() As DataTable
            Return objDALReports.GetDetailed_Transactions_OrgMgr_SharePoint(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _ManagerId, _EmpTypeId)
        End Function
        Public Function Get_DetailedTAViolation_Mgr() As DataTable
            Return objDALReports.Get_DetailedTAViolation_Mgr(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _ManagerId)
        End Function
        Public Function Get_NotAttendanceEmployee_Mgr() As DataTable
            Return objDALReports.GetNotAttendanceEmployee_Mgr(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _ManagerId)
        End Function
        Public Function GetViolations_Advance_Mgr() As DataTable
            Return objDALReports.GetViolations_Advance_Mgr(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly, _ViolationSelection, _ViolationMinDelay, _ViolationMinEarlyOut, _ViolationDelaySelection, _ViolationDelayNum, _ViolationEarlyOutSelection, _ViolationEarlyOutNum, _ViolationAbsentSelection, _ViolationAbsentNum, _ManagerId)
        End Function
        Public Function GetSummary_Param_Mgr() As DataTable
            Return objDALReports.GetSummary_Param_Mgr(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly, _Absent_Count_Selection, _Absent_Count, _Work_Hours_Selection, _Work_Hours, _ManagerId)
        End Function
        Public Function GetGatesReport_Mgr() As DataTable
            Return objDALReports.GetGatesReport_Mgr(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _ManagerId)
        End Function
        Public Function GetDetailed_TransactionsWithAllowance_Mgr() As DataTable
            Return objDALReports.GetDetailed_TransactionsWithAllowance_Mgr(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _ManagerId)
        End Function
        Public Function GetViolations_Mgr2() As DataTable
            Return objDALReports.GetViolations_Mgr2(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _ManagerId)
        End Function
#End Region

#Region "CDC Reports"
        Public Function GetCDC_AppSettings() As DataTable
            Return objDALReports.GetCDC_AppSettings(_Operation, _FROM_DATE, _TO_DATE)
        End Function
        Public Function GetCDC_Emp_ATTCard() As DataTable
            Return objDALReports.GetCDC_Emp_ATTCard(_Operation, _FROM_DATE, _TO_DATE)
        End Function
        Public Function GetCDC_Emp_OverTimeRule() As DataTable
            Return objDALReports.GetCDC_Emp_OverTimeRule(_Operation, _FROM_DATE, _TO_DATE)
        End Function
        Public Function GetCDC_Emp_WorkSchedule() As DataTable
            Return objDALReports.GetCDC_Emp_WorkSchedule(_Operation, _FROM_DATE, _TO_DATE)
        End Function
        Public Function GetCDC_Employee() As DataTable
            Return objDALReports.GetCDC_Employee(_Operation, _FROM_DATE, _TO_DATE)
        End Function
        Public Function GetCDC_Employee_TAPolicy() As DataTable
            Return objDALReports.GetCDC_Employee_TAPolicy(_Operation, _FROM_DATE, _TO_DATE)
        End Function
        Public Function GetCDC_LeaveTypeoccurance() As DataTable
            Return objDALReports.GetCDC_LeaveTypeoccurance(_Operation, _FROM_DATE, _TO_DATE)
        End Function
        Public Function GetCDC_PermissionTypeDuration() As DataTable
            Return objDALReports.GetCDC_PermissionTypeDuration(_Operation, _FROM_DATE, _TO_DATE)
        End Function
        Public Function GetCDC_PermissionTypeOccurance() As DataTable
            Return objDALReports.GetCDC_PermissionTypeOccurance(_Operation, _FROM_DATE, _TO_DATE)
        End Function
        Public Function GetCDC_Schedule_Company() As DataTable
            Return objDALReports.GetCDC_Schedule_Company(_Operation, _FROM_DATE, _TO_DATE)
        End Function
        Public Function GetCDC_Schedule_Entity() As DataTable
            Return objDALReports.GetCDC_Schedule_Entity(_Operation, _FROM_DATE, _TO_DATE)
        End Function
        Public Function GetCDC_Schedule_LogicalGroup() As DataTable
            Return objDALReports.GetCDC_Schedule_LogicalGroup(_Operation, _FROM_DATE, _TO_DATE)
        End Function
        Public Function GetCDC_Schedule_WorkLocation() As DataTable
            Return objDALReports.GetCDC_Schedule_WorkLocation(_Operation, _FROM_DATE, _TO_DATE)
        End Function
        Public Function GetCDC_TAPolicy_AbsentRule() As DataTable
            Return objDALReports.GetCDC_TAPolicy_AbsentRule(_Operation, _FROM_DATE, _TO_DATE)
        End Function
        Public Function GetCDC_TAPolicy_Violation() As DataTable
            Return objDALReports.GetCDC_TAPolicy_Violation(_Operation, _FROM_DATE, _TO_DATE)
        End Function
#End Region

#Region "Manager Report Dynamic"
        Public Function ManagerReportDynamic() As DataTable
            Return objDALReports.ManagerReportDynamic(_ManagerId, _ReportType, _ReportName)
        End Function
        Public Function EntityManagerReportDynamic() As DataTable
            Return objDALReports.EntityManagerReportDynamic(_ManagerId, _ReportType, _ReportName, _EntityId)
        End Function
        Public Function EmployeeReportDynamic() As DataTable
            Return objDALReports.EmployeeReportDynamic(_EmployeeId, _ReportType, _ReportName)
        End Function
        Public Function HRReportDynamic() As DataTable
            Return objDALReports.HRReportDynamic(_EmployeeId, _ReportType, _ReportName)
        End Function
        Public Function Email_Configuration_Select_Date_Manager_Reports(ByRef StartDate As DateTime, ByRef EndDate As DateTime) As DataTable
            Return objDALReports.Email_Configuration_Select_Date_Manager_Reports(_ReportType, _ReportName, StartDate, EndDate)
        End Function
        Public Function GetAttendance_Summary() As DataTable
            Return objDALReports.GetAttendance_Summary(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _ManagerId)
        End Function
        Public Function GetSubAttendance_Summary_Absent() As DataTable
            Return objDALReports.GetSubAttendance_Summary_Absent(_ManagerId, _FROM_DATE, _TO_DATE, _EntityId)
        End Function
        Public Function GetSubAttendance_Summary_Delay() As DataTable
            Return objDALReports.GetSubAttendance_Summary_Delay(_ManagerId, _FROM_DATE, _TO_DATE)
        End Function
#End Region

#Region "Statistical_Movements"
        Public Function Statistical_Movements() As DataTable
            Return objDALReports.Statistical_Movements(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _DirectStaffOnly)
        End Function
#End Region

#Region "Trans Guard Readers"
        Public Function GetTransG_Devices() As DataTable
            Return objDALReports.GetTransG_Devices()
        End Function
#End Region

        Public Function GetDetailed_Transactions_Mgr_SP_VicePrincipal() As DataTable
            Return objDALReports.GetDetailed_Transactions_Mgr_SharePoint_VicePrincipal(_EmployeeId, _FROM_DATE, _TO_DATE, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _ManagerId, _EmpTypeId)
        End Function

    End Class

End Namespace
