Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Employees
Imports SmartV.UTILITIES
Imports TA.SelfServices
Imports TA.Admin
Imports TA.Events
Imports TA.Definitions

Namespace TA.DailyTasks

    Public Class HR_LeaveRequest

#Region "Class Variables"


        Private _LeaveRequestId As Integer
        Private _FK_EmployeeId As Long
        Private _FK_LeaveTypeId As Integer
        Private _RequestDate As DateTime
        Private _FromDate As DateTime
        Private _ToDate As DateTime
        Private _Remarks As String
        Private _IsHalfDay As Boolean
        Private _Days As Integer
        Private _AttachedFile As String
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private _IsRejected As Boolean
        Private _FK_CompanyId As Integer
        Private _FK_EntityId As Integer
        Private _EmployeeNo As Integer
        Private _EmployeeName As String
        Private _EmployeeArabicName As String
        Private _LeaveName As String
        Private _LeaveArabicName As String
        Private _TotalBalance As Integer
        Private _BalanceId As Integer
        Private objDALHR_LeaveRequest As DALHR_LeaveRequest
        Private objEmployee As Employee
        Private objWorkSchedule_NormalTime As WorkSchedule_NormalTime
        Private objWorkSchedule_Flexible As WorkSchedule_Flexible
        Private objEmp_Shifts As Emp_Shifts
        Private objHoliday As New Holiday
        Private objLeaveTypeOccurance As New LeaveTypeOccurance
        Private objEmp_Leaves_BalanceHistory As New Emp_Leaves_BalanceHistory
        Private _UserId As String
        Private _RejectionReason As String
        Private objEmp_Leaves As Emp_Leaves

#End Region

#Region "Public Properties"


        Public Property LeaveRequestId() As Integer
            Set(ByVal value As Integer)
                _LeaveRequestId = value
            End Set
            Get
                Return (_LeaveRequestId)
            End Get
        End Property

        Public Property FK_EmployeeId() As Long
            Set(ByVal value As Long)
                _FK_EmployeeId = value
            End Set
            Get
                Return (_FK_EmployeeId)
            End Get
        End Property

        Public Property FK_LeaveTypeId() As Integer
            Set(ByVal value As Integer)
                _FK_LeaveTypeId = value
            End Set
            Get
                Return (_FK_LeaveTypeId)
            End Get
        End Property

        Public Property RequestDate() As DateTime
            Set(ByVal value As DateTime)
                _RequestDate = value
            End Set
            Get
                Return (_RequestDate)
            End Get
        End Property

        Public Property FromDate() As DateTime
            Set(ByVal value As DateTime)
                _FromDate = value
            End Set
            Get
                Return (_FromDate)
            End Get
        End Property

        Public Property ToDate() As DateTime
            Set(ByVal value As DateTime)
                _ToDate = value
            End Set
            Get
                Return (_ToDate)
            End Get
        End Property

        Public Property Remarks() As String
            Set(ByVal value As String)
                _Remarks = value
            End Set
            Get
                Return (_Remarks)
            End Get
        End Property

        Public Property IsHalfDay() As Boolean
            Set(ByVal value As Boolean)
                _IsHalfDay = value
            End Set
            Get
                Return (_IsHalfDay)
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

        Public Property AttachedFile() As String
            Set(ByVal value As String)
                _AttachedFile = value
            End Set
            Get
                Return (_AttachedFile)
            End Get
        End Property

        Public Property CREATED_BY() As String
            Set(ByVal value As String)
                _CREATED_BY = value
            End Set
            Get
                Return (_CREATED_BY)
            End Get
        End Property

        Public Property CREATED_DATE() As DateTime
            Set(ByVal value As DateTime)
                _CREATED_DATE = value
            End Set
            Get
                Return (_CREATED_DATE)
            End Get
        End Property

        Public Property LAST_UPDATE_BY() As String
            Set(ByVal value As String)
                _LAST_UPDATE_BY = value
            End Set
            Get
                Return (_LAST_UPDATE_BY)
            End Get
        End Property

        Public Property LAST_UPDATE_DATE() As DateTime
            Set(ByVal value As DateTime)
                _LAST_UPDATE_DATE = value
            End Set
            Get
                Return (_LAST_UPDATE_DATE)
            End Get
        End Property

        Public Property IsRejected() As Boolean
            Set(ByVal value As Boolean)
                _IsRejected = value
            End Set
            Get
                Return (_IsRejected)
            End Get
        End Property

        Public Property EmployeeNo() As Integer
            Set(ByVal value As Integer)
                _EmployeeNo = value
            End Set
            Get
                Return (_EmployeeNo)
            End Get
        End Property

        Public Property EmployeeName() As String
            Set(ByVal value As String)
                _EmployeeName = value
            End Set
            Get
                Return (_EmployeeName)
            End Get
        End Property

        Public Property EmployeeArabicName() As String
            Set(ByVal value As String)
                _EmployeeArabicName = value
            End Set
            Get
                Return (_EmployeeArabicName)
            End Get
        End Property

        Public Property LeaveName() As String
            Set(ByVal value As String)
                _LeaveName = value
            End Set
            Get
                Return (_LeaveName)
            End Get
        End Property

        Public Property LeaveArabicName() As String
            Set(ByVal value As String)
                _LeaveArabicName = value
            End Set
            Get
                Return (_LeaveArabicName)
            End Get
        End Property

        Public Property TotalBalance() As Integer
            Set(ByVal value As Integer)
                _TotalBalance = value
            End Set
            Get
                Return (_TotalBalance)
            End Get
        End Property

        Public Property BalanceId() As Integer
            Set(ByVal value As Integer)
                _BalanceId = value
            End Set
            Get
                Return (_BalanceId)
            End Get
        End Property

        Public Property FK_CompanyId() As Integer
            Set(ByVal value As Integer)
                _FK_CompanyId = value
            End Set
            Get
                Return (_FK_CompanyId)
            End Get
        End Property

        Public Property FK_EntityId() As Integer
            Set(ByVal value As Integer)
                _FK_EntityId = value
            End Set
            Get
                Return (_FK_EntityId)
            End Get
        End Property

        Public Property UserId() As String
            Set(ByVal value As String)
                _UserId = value
            End Set
            Get
                Return (_UserId)
            End Get
        End Property

        Public Property RejectionReason() As String
            Set(ByVal value As String)
                _RejectionReason = value
            End Set
            Get
                Return (_RejectionReason)
            End Get
        End Property
#End Region

#Region "Constructor"

        Public Sub New()

            objDALHR_LeaveRequest = New DALHR_LeaveRequest()

        End Sub

#End Region

#Region "Methods"

#Region "LeaveValidation"

        Function CheckaLeavesMinMaxDays(ByVal EmployeeId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal LeaveTypeInfo As LeavesTypes, ByRef ErrorMsg As String) As Boolean
            Dim MinDuration = LeaveTypeInfo.MinDuration
            Dim MaxDuration = LeaveTypeInfo.MaxDuration
            Dim CurrentLeaveDuration As TimeSpan
            Dim intLeaveDuration As Integer
            CurrentLeaveDuration = (ToDate - FromDate)
            Dim offDaysCount As Integer = CalculateOffAndHolidaysDays(EmployeeId, FromDate, ToDate, LeaveTypeInfo)

            intLeaveDuration = (CurrentLeaveDuration.Days + 1) - offDaysCount
            'If (CurrentLeaveDuration.Days + 1 < MinDuration) Then
            '    If SessionVariables.CultureInfo = "ar-JO" Then
            '        ErrorMsg = "الحد الادنى لمدة الاجازة المسموح به هو: " & MinDuration & " ايام"
            '    Else
            '        ErrorMsg = "The Minimum allowed leave duration is: " & MinDuration & " days"
            '    End If
            '    Return False
            'ElseIf (CurrentLeaveDuration.Days + 1 > MaxDuration) Then
            '    If SessionVariables.CultureInfo = "ar-JO" Then
            '        ErrorMsg = "الحد الاقصى لمدة الاجازة المسموح به هو: " & MaxDuration & " days"
            '    Else
            '        ErrorMsg = "The Maximum allowed leave duration is: " & MaxDuration & " days"
            '    End If
            '    Return False
            'End If

            If Not LeaveTypeInfo.AllowIfBalanceOver Then
                If (intLeaveDuration < MinDuration) Then
                    If SessionVariables.CultureInfo = "ar-JO" Then
                        ErrorMsg = "الحد الادنى لمدة الاجازة المسموح به هو: " & MinDuration & " ايام"
                    Else
                        ErrorMsg = "The Minimum allowed leave duration is: " & MinDuration & " days"
                    End If
                    Return False
                ElseIf (intLeaveDuration > MaxDuration) Then
                    If SessionVariables.CultureInfo = "ar-JO" Then
                        ErrorMsg = "الحد الاقصى لمدة الاجازة المسموح به هو: " & MaxDuration & " days"
                    Else
                        ErrorMsg = "The Maximum allowed leave duration is: " & MaxDuration & " days"
                    End If
                    Return False
                End If
            End If

            Return True
        End Function

        Function CheckLeaveAllowanceAfterServiceDays(ByVal EmployeeId As Integer, ByVal LeaveTypeInfo As LeavesTypes, ByRef ErrorMessage As String) As Boolean
            Dim ServiceDays = LeaveTypeInfo.MinServiceDays
            Dim EmployeeCurrentServiceDays As Integer
            objEmployee = New Employee

            If EmployeeId = 0 Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    ErrorMessage = "هذا المستخدم غير مرتبط مع موظف"
                Else
                    ErrorMessage = "This user not related with employee"
                End If

                Return False
            End If

            With objEmployee
                .EmployeeId = EmployeeId
                Dim EmpInfo = .GetByPK()
                EmployeeCurrentServiceDays = (DateTime.Now - EmpInfo.JoinDate).Days
            End With
            If (EmployeeCurrentServiceDays < ServiceDays) Then

                If SessionVariables.CultureInfo = "ar-JO" Then
                    ErrorMessage = "الاجازات مسموحة بعد " & ServiceDays & " ايام خدمة"
                Else
                    ErrorMessage = "Leaves allowed after " & ServiceDays & " service days"
                End If

                Return False
            End If
            Return True
        End Function

        Function CalculateOffAndHolidaysDays(ByVal EmployeeId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, _
                                             ByVal LeaveTypeInfo As LeavesTypes) As Integer
            Dim LeaveDaysCount = ((ToDate - FromDate).Days) + 1
            Dim dtOffDays As DataTable = New DataTable
            Dim objApp_Settings As New APP_Settings

            'Create Table For Days, IsOffDay, IsHoliday
            dtOffDays = New DataTable
            Dim dcColumn As DataColumn
            dcColumn = New DataColumn("Days")
            dcColumn.DataType = GetType(Date)
            dtOffDays.Columns.Add(dcColumn)
            dcColumn = New DataColumn("IsOffDay")
            dcColumn.DataType = GetType(Boolean)
            dtOffDays.Columns.Add(dcColumn)
            dcColumn = New DataColumn("IsHoliday")
            dcColumn.DataType = GetType(Boolean)
            dtOffDays.Columns.Add(dcColumn)

            Dim StartDate = FromDate
            Dim EndDate = ToDate

            If (LeaveTypeInfo.ExcludeOffDays Or LeaveTypeInfo.ExcludeHolidays) Then
                While (StartDate <= EndDate)

                    Dim drNewRow = dtOffDays.NewRow
                    drNewRow("Days") = StartDate

                    Dim objWorkSchedule As New WorkSchedule
                    objWorkSchedule.EmployeeId = EmployeeId
                    Dim dtActiveSchedule = objWorkSchedule.GetEmployeeActiveScheduleByEmpId(StartDate)

                    If (LeaveTypeInfo.ExcludeOffDays) Then
                        If (dtActiveSchedule IsNot Nothing And dtActiveSchedule.Rows.Count > 0) Then

                            ' Schedule is Normal
                            If dtActiveSchedule.Rows(0)("ScheduleType") = 1 Then
                                Dim DayId = 0
                                objWorkSchedule_NormalTime = New WorkSchedule_NormalTime
                                objWorkSchedule_NormalTime.FK_ScheduleId = dtActiveSchedule.Rows(0)("ScheduleId")
                                Dim ScheduleTimes = objWorkSchedule_NormalTime.GetAll()

                                Select Case StartDate.DayOfWeek().ToString()
                                    Case "Monday"
                                        DayId = 2
                                    Case "Tuesday"
                                        DayId = 3
                                    Case "Wednesday"
                                        DayId = 4
                                    Case "Thursday"
                                        DayId = 5
                                    Case "Friday"
                                        DayId = 6
                                    Case "Saturday"
                                        DayId = 7
                                    Case "Sunday"
                                        DayId = 1
                                End Select
                                Dim drScheduleTime = ScheduleTimes.Select("DayId =" & DayId)(0)
                                If (drScheduleTime("IsOffDay")) Then
                                    drNewRow("IsOffDay") = True
                                Else
                                    drNewRow("IsOffDay") = False
                                End If

                                ' Schedule is Flexible
                            ElseIf dtActiveSchedule.Rows(0)("ScheduleType") = 2 Then
                                Dim DayId = 0

                                objWorkSchedule_Flexible = New WorkSchedule_Flexible
                                objWorkSchedule_Flexible.FK_ScheduleId = dtActiveSchedule.Rows(0)("ScheduleId")
                                Dim FlexibleSchedule = objWorkSchedule_Flexible.GetAll()

                                Select Case StartDate.DayOfWeek().ToString()
                                    Case "Monday"
                                        DayId = 2
                                    Case "Tuesday"
                                        DayId = 3
                                    Case "Wednesday"
                                        DayId = 4
                                    Case "Thursday"
                                        DayId = 5
                                    Case "Friday"
                                        DayId = 6
                                    Case "Saturday"
                                        DayId = 7
                                    Case "Sunday"
                                        DayId = 1
                                End Select

                                Dim drFlexibleSchedule = FlexibleSchedule.Select("DayId =" & DayId)(0)
                                If (drFlexibleSchedule("IsOffDay")) Then
                                    drNewRow("IsOffDay") = True
                                Else
                                    drNewRow("IsOffDay") = False
                                End If

                            ElseIf dtActiveSchedule.Rows(0)("ScheduleType") = 3 Then
                                objEmp_Shifts = New Emp_Shifts
                                objEmp_Shifts.FK_EmployeeId = EmployeeId
                                objEmp_Shifts.WorkDate = StartDate
                                Dim AdvancedSchedule = objEmp_Shifts.GetShiftsByDate()
                                If (AdvancedSchedule IsNot Nothing And AdvancedSchedule.Rows.Count > 0) Then
                                    drNewRow("IsOffDay") = False
                                Else
                                    'FadiH:: check event when no shift
                                    With objApp_Settings
                                        .GetByPK()
                                        If .NoShiftShcedule = Convert.ToInt32(CtlCommon.NoShiftSchedule.ConsideritOffDay) Then
                                            drNewRow("IsOffDay") = True
                                        ElseIf .NoShiftShcedule = Convert.ToInt32(CtlCommon.NoShiftSchedule.ConsiderDefaultSchedule) Then
                                            drNewRow("IsOffDay") = CheckDefaultScheduleIsOffDay(StartDate)
                                        End If
                                    End With
                                End If
                            End If
                        Else
                            drNewRow("IsOffDay") = True
                        End If
                    Else
                        drNewRow("IsOffDay") = False
                    End If

                    'Count Holiday days
                    If (LeaveTypeInfo.ExcludeHolidays) Then
                        objHoliday = New Holiday
                        With objHoliday
                            .HolidayDay = StartDate
                            .EmployeeId = EmployeeId
                            Dim HolidayDaysCount = .EmpGetHolidays
                            If (HolidayDaysCount > 0) Then
                                drNewRow("IsHoliday") = True
                            Else
                                drNewRow("Isholiday") = False
                            End If
                        End With
                    Else
                        drNewRow("Isholiday") = False
                    End If
                    StartDate = StartDate.AddDays(1)

                    dtOffDays.Rows.Add(drNewRow)
                End While

                If (dtOffDays IsNot Nothing And dtOffDays.Rows.Count > 0) Then
                    Dim IsOffDayOrHolidayColumn As DataColumn
                    IsOffDayOrHolidayColumn = New DataColumn("IsOffDayOrHoliday")
                    IsOffDayOrHolidayColumn.DataType = GetType(Boolean)
                    dtOffDays.Columns.Add(IsOffDayOrHolidayColumn)
                    For Each drRow As DataRow In dtOffDays.Rows
                        drRow("IsOffDayOrHoliday") = drRow("IsOffDay") Or drRow("Isholiday")
                    Next
                    Return dtOffDays.Select("IsOffDayOrHoliday = True").Length
                End If
            End If
        End Function

        Function CheckDefaultScheduleIsOffDay(ByVal StartDate As Date) As Boolean
            Dim objDefaultWorkSchedule As New WorkSchedule
            objDefaultWorkSchedule = objDefaultWorkSchedule.GetByDefault()
            If objDefaultWorkSchedule.ScheduleType = Convert.ToInt32(CtlCommon.ScheduleType.Normal) Then
                Dim DayId = 0
                objWorkSchedule_NormalTime = New WorkSchedule_NormalTime
                objWorkSchedule_NormalTime.FK_ScheduleId = objDefaultWorkSchedule.ScheduleId
                Dim ScheduleTimes = objWorkSchedule_NormalTime.GetAll()

                Select Case StartDate.DayOfWeek().ToString()
                    Case "Monday"
                        DayId = 2
                    Case "Tuesday"
                        DayId = 3
                    Case "Wednesday"
                        DayId = 4
                    Case "Thursday"
                        DayId = 5
                    Case "Friday"
                        DayId = 6
                    Case "Saturday"
                        DayId = 7
                    Case "Sunday"
                        DayId = 1
                End Select
                Dim drScheduleTime = ScheduleTimes.Select("DayId =" & DayId)(0)
                If (drScheduleTime("IsOffDay")) Then
                    Return True
                Else
                    Return False
                End If
            ElseIf objDefaultWorkSchedule.ScheduleType = Convert.ToInt32(CtlCommon.ScheduleType.Flexible) Then
                Dim DayId = 0

                objWorkSchedule_Flexible = New WorkSchedule_Flexible
                objWorkSchedule_Flexible.FK_ScheduleId = objDefaultWorkSchedule.ScheduleId
                Dim FlexibleSchedule = objWorkSchedule_Flexible.GetAll()

                Select Case StartDate.DayOfWeek().ToString()
                    Case "Monday"
                        DayId = 2
                    Case "Tuesday"
                        DayId = 3
                    Case "Wednesday"
                        DayId = 4
                    Case "Thursday"
                        DayId = 5
                    Case "Friday"
                        DayId = 6
                    Case "Saturday"
                        DayId = 7
                    Case "Sunday"
                        DayId = 1
                End Select

                Dim drFlexibleSchedule = FlexibleSchedule.Select("DayId =" & DayId)(0)
                If (drFlexibleSchedule("IsOffDay")) Then
                    Return True
                Else
                    Return False
                End If
            End If
        End Function

        Function CheckAllowedOccuranceForDayWeekMonth(ByVal EmployeeId As Integer, ByVal LeaveType As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, _
                                                      ByVal LeaveTypeInfo As LeavesTypes, ByRef ErrorMessage As String) As Boolean
            Dim drWeek As DataRow
            With objLeaveTypeOccurance
                .LeaveId = LeaveType
                Dim dtLeaveOccurance = .GetByPK()

                If (dtLeaveOccurance IsNot Nothing AndAlso dtLeaveOccurance.Rows.Count > 0) Then
                    'Check validation for a week
                    If (dtLeaveOccurance.Select("FK_DurationId = 2").Length > 0) Then
                        drWeek = dtLeaveOccurance.Select("FK_DurationId = 2")(0)

                        'Get Occurance Per Week for this employee
                        Me.FromDate = FromDate
                        Me.FK_EmployeeId = EmployeeId
                        Me.FK_LeaveTypeId = LeaveType
                        Dim LeaveOccurance = Me.GetOccuranceForWeek()

                        If (LeaveOccurance >= drWeek("MaximumOccur")) Then
                            ErrorMessage = "Allowed leaves per week: " & drWeek("MaximumOccur") & "leaves"
                            Return False
                        End If
                    End If

                    'Check Validation for a Month
                    If (dtLeaveOccurance.Select("FK_DurationId = 3").Length > 0) Then
                        drWeek = dtLeaveOccurance.Select("FK_DurationId = 3")(0)

                        'Get Occurance Per Month for this employee
                        Me.FromDate = FromDate
                        Me.FK_EmployeeId = EmployeeId
                        Me.FK_LeaveTypeId = LeaveType
                        Dim LeaveMonthOccurance = Me.GetOccuranceForMonth()

                        If (LeaveMonthOccurance >= drWeek("MaximumOccur")) Then
                            ErrorMessage = "Allowed leaves per month: " & drWeek("MaximumOccur") & "leaves"
                            Return False
                        End If
                    End If

                    'Check Validation for a Year
                    If (dtLeaveOccurance.Select("FK_DurationId = 4").Length > 0) Then
                        drWeek = dtLeaveOccurance.Select("FK_DurationId = 4")(0)

                        'Get Occurance Per Year for this employee
                        Me.FromDate = FromDate
                        Me.FK_EmployeeId = EmployeeId
                        Me.FK_LeaveTypeId = LeaveType
                        Dim LeaveMonthOccurance = Me.GetOccuranceForYear()

                        If (LeaveMonthOccurance >= drWeek("MaximumOccur")) Then
                            ErrorMessage = "Allowed leaves per year: " & drWeek("MaximumOccur") & "leaves"
                            Return False
                        End If
                    End If
                    If (dtLeaveOccurance.Select("FK_DurationId = 5").Length > 0) Then
                        drWeek = dtLeaveOccurance.Select("FK_DurationId = 5")(0)
                        Dim AllServiceTime = Me.GetOccuranceForAllServiceTime()

                        If (AllServiceTime >= drWeek("MaximumOccur")) Then
                            ErrorMessage = "Allowed leaves per All Service Time: " & drWeek("MaximumOccur") & "leaves"
                            Return False
                        End If
                    End If
                End If
            End With
            Return True
        End Function

        Function CheckEmpAllowedLeaveBalance(ByVal EmployeeId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal LeaveTypeInfo As LeavesTypes, ByRef EmpLeaveTotalBalance As Double, _
                                             ByRef ErrorMsg As String) As Boolean


            If CheckEmpAllowedParentLeaceBalance(EmployeeId, FromDate, ToDate, LeaveTypeInfo, EmpLeaveTotalBalance, ErrorMsg) = False Then
                Return False
            End If

            'Get Employee Balance
            Dim LeaveCount As Integer
            Dim intLeaveDuration As Integer

            Dim offDaysCount As Integer = CalculateOffAndHolidaysDays(EmployeeId, FromDate, ToDate, LeaveTypeInfo)

            LeaveCount = ((ToDate - FromDate).Days + 1)
            intLeaveDuration = LeaveCount - offDaysCount

            objEmp_Leaves_BalanceHistory = New Emp_Leaves_BalanceHistory
            objEmp_Leaves_BalanceHistory.FK_EmployeeId = EmployeeId
            objEmp_Leaves_BalanceHistory.FK_EmpLeaveId = LeaveTypeInfo.LeaveId
            Dim dtLeaveBalance = objEmp_Leaves_BalanceHistory.GetEmpLeaveBalance()

            'If (dtLeaveBalance IsNot Nothing And dtLeaveBalance.Rows.Count > 0) Then
            '    EmpLeaveTotalBalance = dtLeaveBalance.Rows(0)("TotalBalance")
            '    If LeaveId > 0 Then
            '        Me.LeaveId = LeaveId
            '        Me.GetByPK()

            '        If (EmpLeaveTotalBalance + Me.Days < LeaveCount) Then
            '            If (LeaveTypeInfo.AllowIfBalanceOver = False) Then
            '                If SessionVariables.CultureInfo = "ar-JO" Then
            '                    ErrorMsg = " رصيد اجازاتك هو:" & EmpLeaveTotalBalance
            '                Else
            '                    ErrorMsg = "Your leave balance is:" & EmpLeaveTotalBalance
            '                End If

            '                Return False
            '            End If
            '        End If
            '    Else
            '        If (EmpLeaveTotalBalance < LeaveCount) Then
            '            If (LeaveTypeInfo.AllowIfBalanceOver = False) Then
            '                If SessionVariables.CultureInfo = "ar-JO" Then
            '                    ErrorMsg = " رصيد اجازاتك هو:" & EmpLeaveTotalBalance
            '                Else
            '                    ErrorMsg = "Your leave balance is:" & EmpLeaveTotalBalance
            '                End If
            '                Return False
            '            End If
            '        End If
            '    End If
            'End If


            If (dtLeaveBalance IsNot Nothing And dtLeaveBalance.Rows.Count > 0) Then
                EmpLeaveTotalBalance = dtLeaveBalance.Rows(0)("TotalBalance")
                If Not LeaveTypeInfo.AllowIfBalanceOver Then
                    If LeaveRequestId > 0 Then
                        Me.LeaveRequestId = LeaveRequestId
                        Me.GetByPK()

                        If (EmpLeaveTotalBalance + Me.Days < intLeaveDuration) Then
                            If (LeaveTypeInfo.AllowIfBalanceOver = False) Then
                                If SessionVariables.CultureInfo = "ar-JO" Then
                                    ErrorMsg = " رصيد اجازاتك هو:" & EmpLeaveTotalBalance
                                Else
                                    ErrorMsg = "Your leave balance is:" & EmpLeaveTotalBalance
                                End If

                                Return False
                            End If
                        End If
                    Else
                        If (EmpLeaveTotalBalance < intLeaveDuration) Then
                            If (LeaveTypeInfo.AllowIfBalanceOver = False) Then
                                If SessionVariables.CultureInfo = "ar-JO" Then
                                    ErrorMsg = " رصيد اجازاتك هو:" & EmpLeaveTotalBalance
                                Else
                                    ErrorMsg = "Your leave balance is:" & EmpLeaveTotalBalance
                                End If
                                Return False
                            End If
                        End If
                    End If
                End If
            End If
            Return True
        End Function

        Function CheckEmpAllowedParentLeaceBalance(ByVal EmployeeId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal LeaveTypeInfo As LeavesTypes, ByRef EmpLeaveTotalBalance As Double, _
                                             ByRef ErrorMsg As String) As Boolean

            objEmp_Leaves_BalanceHistory = New Emp_Leaves_BalanceHistory
            objEmp_Leaves_BalanceHistory.FK_EmployeeId = EmployeeId
            objEmp_Leaves_BalanceHistory.FK_EmpLeaveId = LeaveTypeInfo.FK_ParentLeaveType

            Dim strParentParentLeaveTypeName As String = String.Empty
            Dim strParentParentLeaveTypeNameAR As String = String.Empty

            If Not LeaveTypeInfo.FK_ParentLeaveType = -1 Then
                Dim objParentLeaveTypeInfo As New LeavesTypes
                With objParentLeaveTypeInfo
                    .LeaveId = LeaveTypeInfo.FK_ParentLeaveType
                    .GetByPK()
                    strParentParentLeaveTypeName = .LeaveName
                    strParentParentLeaveTypeNameAR = .LeaveArabicName
                End With

                Dim dtLeaveBalance = objEmp_Leaves_BalanceHistory.GetEmpLeaveBalance()

                If (dtLeaveBalance IsNot Nothing And dtLeaveBalance.Rows.Count > 0) Then
                    If dtLeaveBalance.Rows(0)("TotalBalance") > 0 Then
                        If SessionVariables.CultureInfo = "ar-JO" Then
                            ErrorMsg = String.Format("رصيد اجازتك {1} هو {0} لا تستطيع طلب هذا النوع من الاجازه حتى يصبح رصيد اجازتك {1}هو 0", dtLeaveBalance.Rows(0)("TotalBalance"), strParentParentLeaveTypeNameAR.Trim())
                        Else
                            ErrorMsg = String.Format("{1} leave balance is: {0} you can't request this type of leave unless {1} is 0", dtLeaveBalance.Rows(0)("TotalBalance"), strParentParentLeaveTypeName.Trim())
                        End If
                        Return False
                    End If

                End If
            End If

            Return True

        End Function

        Function ValidateEmployeeLeaveRequest(ByVal objLeavesTypes As LeavesTypes, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal LeaveId As Integer, ByVal EmployeeId As Integer, _
                                       ByVal LeaveType As Integer, ByRef ErrorMessage As String, ByRef OffAndHolidayDays As String, ByRef LeaveTotalBalance As Double) As Boolean

            If HR_LeaveRequestHasRequest(EmployeeId, FromDate, ToDate, ErrorMessage) = True Then
                If SessionVariables.CultureInfo = "en-US" Then
                    ErrorMessage = "Leave Request Record Already Exists Between The Date Range"
                Else
                    ErrorMessage = "طلب الاجازة موجودة مسبقاً في حدود التاريخ"
                End If
                Return False
            ElseIf Not LeaveValidation(EmployeeId, FromDate, ToDate, objLeavesTypes, LeaveType, ErrorMessage, OffAndHolidayDays, LeaveTotalBalance) Then
                Return False
            Else
                Return True
            End If

        End Function

        Function ValidateEmployeeLeave(ByVal objLeavesTypes As LeavesTypes, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal LeaveId As Integer, ByVal EmployeeId As Integer, _
                                       ByVal LeaveType As Integer, ByRef ErrorMessage As String, ByRef OffAndHolidayDays As String, ByRef LeaveTotalBalance As Double) As Boolean

            If IsExists(FromDate, ToDate, LeaveId, EmployeeId) = True Then
                If SessionVariables.CultureInfo = "en-US" Then
                    ErrorMessage = "Leave Record Already Exists Between The Date Range"
                Else
                    ErrorMessage = "الاجازة موجودة مسبقاً في حدود التاريخ"
                End If
                Return False
            ElseIf ValidateRestDays(FK_EmployeeId, FromDate, objLeavesTypes) Then
                If SessionVariables.CultureInfo = "en-US" Then
                    ErrorMessage = "Leave Record Exists Before Rest Day"
                Else
                    ErrorMessage = "يوجد اجازة قبل يوم الاستراحة"
                End If
                Return False
            ElseIf Not LeaveValidation(EmployeeId, FromDate, ToDate, objLeavesTypes, LeaveType, ErrorMessage, OffAndHolidayDays, LeaveTotalBalance) Then
                Return False
            Else
                Return True
            End If
        End Function

        Function ValidateRestDays(ByVal EmployeeId As Integer, ByVal Fromdate As Date, ByVal objLeavesTypes As LeavesTypes) As Boolean
            Dim Dt As DataTable = New DataTable
            Dim objEmp_LeavesRequest As New Emp_LeavesRequest
            objEmp_Leaves = New Emp_Leaves

            objEmp_Leaves.FK_EmployeeId = EmployeeId
            objEmp_Leaves.FromDate = Fromdate
            objEmp_Leaves.FK_LeaveTypeId = objLeavesTypes.LeaveId
            Dt = objEmp_Leaves.Validate_RestDay

            Dim status As Boolean
            'to check if duplicate data in leave request table
            If Dt IsNot Nothing Then
                If Dt.Rows.Count > 0 Then
                    status = Dt.Rows(0)("LeaveExistBeforeRest")
                End If
            End If

            Return status
        End Function

        Function IsExistsRequest(ByVal Fromdate As Date, ByVal Todate As Date, ByVal LeaveRequestId As Integer, ByVal EmployeeId As Integer) As Boolean
            Dim EmpDt As DataTable = New DataTable
            Dim objEmp_LeavesRequest As New Emp_LeavesRequest

            'to check if duplicate data in leave request table

            objEmp_LeavesRequest.FK_EmployeeId = EmployeeId
            EmpDt = objEmp_LeavesRequest.GetByEmployee()

            Dim status As Boolean
            Dim requestStatus As Integer
            For Each dr As DataRow In EmpDt.Rows
                requestStatus = Convert.ToInt32(dr("FK_StatusId"))
                If LeaveRequestId = dr("LeaveRequestId") Then
                    Continue For
                End If

                If (Fromdate >= dr("FromDate") And Fromdate <= dr("ToDate")) Or (Todate >= dr("FromDate") And Todate <= dr("ToDate")) Then
                    If (requestStatus = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByGeneralManager)) _
                    Or (requestStatus = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByHumanResource)) _
                    Or (requestStatus = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedbyManager)) Then
                        Continue For
                    Else
                        status = True
                    End If
                End If
            Next
            Return status
        End Function

        Function IsExists(ByVal Fromdate As Date, ByVal Todate As Date, ByVal LeaveRequestId As Integer, ByVal EmployeeId As Integer) As Boolean
            Dim EmpDt As DataTable = New DataTable
            Dim objEmp_LeavesRequest As New Emp_LeavesRequest

            Me.FK_EmployeeId = EmployeeId
            Me.FromDate = DateSerial(Today.Year, Today.Month, 1)
            Me.ToDate = DateSerial(Today.Year, Today.Month + 1, 0)
            EmpDt = Me.GetAllLeavesByEmployee()

            'to check if duplicate data in leave request table
            If EmpDt IsNot Nothing Then
                If EmpDt.Rows.Count = 0 Then
                    objEmp_LeavesRequest.FK_EmployeeId = EmployeeId
                    EmpDt = objEmp_LeavesRequest.GetByEmployee()
                End If
            End If

            Dim status As Boolean
            For Each dr As DataRow In EmpDt.Rows
                If LeaveRequestId = dr("LeaveRequestId") Then
                    Continue For
                End If
                If Fromdate >= dr("FromDate") And Fromdate <= dr("ToDate") Or Todate >= dr("FromDate") And Todate <= dr("ToDate") Then
                    status = True
                End If
            Next
            Return status
        End Function

        Function CheckLeaveAllowedGender(ByVal EmployeeId As Integer, ByVal LeaveTypeInfo As LeavesTypes, ByRef ErrorMsg As String) As Boolean
            objEmployee = New Employee
            With objEmployee
                .EmployeeId = EmployeeId
                .GetByPK()
                If LeaveTypeInfo.AllowedGender = 1 And .Gender = "f" Then
                    If SessionVariables.CultureInfo = "ar-JO" Then
                        ErrorMsg = "نوع الاجازة الذي قمت باختياره مسموح للذكور فقط"
                    Else
                        ErrorMsg = "The Selected Leave Type Allowed For Males Only"
                    End If
                    Return False
                ElseIf LeaveTypeInfo.AllowedGender = 2 And .Gender = "m" Then
                    If SessionVariables.CultureInfo = "ar-JO" Then
                        ErrorMsg = "نوع الاجازة الذي قمت باختياره مسموح للاناث فقط"
                    Else
                        ErrorMsg = "The Selected Leave Type Allowed For Females Only"
                    End If
                    Return False
                End If
            End With
            Return True
        End Function

        Function CheckLeaveAllowedEmployeeType(ByVal EmployeeId As Integer, ByVal LeaveTypeInfo As LeavesTypes, ByRef ErrorMsg As String) As Boolean
            objEmployee = New Employee
            With objEmployee
                .EmployeeId = EmployeeId
                .GetByPK()
                If LeaveTypeInfo.AllowForSpecificEmployeeType = True Then
                    If LeaveTypeInfo.FK_EmployeeTypeId <> .FK_LogicalGroup Then
                        If SessionVariables.CultureInfo = "ar-JO" Then
                            ErrorMsg = "نوع الاجازة الذي قمت باختياره غير مسموح للمجموعة الوظيفية للموظف"
                        Else
                            ErrorMsg = "The Selected Leave Type Not Allowed For The Selected Employee Logical Group"
                        End If
                        Return False
                    End If
                End If
            End With
            Return True
        End Function

        Function LeaveValidation(ByVal EmployeeId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal objLeavesTypes As LeavesTypes, ByVal LeaveType As Integer, ByRef ErrorMsg As String, ByRef OffAndHolidayDays As Integer, ByRef EmpLeaveTotalBalance As Double) As Boolean
            With objLeavesTypes
                .LeaveId = LeaveType
                Dim LeaveTypeInfo = .GetByPK()
                If (LeaveTypeInfo IsNot Nothing) Then
                    'Check Leave Min and Max Days
                    If Not CheckaLeavesMinMaxDays(EmployeeId, FromDate, ToDate, LeaveTypeInfo, ErrorMsg) Then
                        Return False
                    Else
                        'Check leave allowance after ServiceDays
                        If Not CheckLeaveAllowanceAfterServiceDays(EmployeeId, LeaveTypeInfo, ErrorMsg) Then
                            Return False
                        Else
                            'Count Off And Hoildays Days
                            OffAndHolidayDays = CalculateOffAndHolidaysDays(EmployeeId, FromDate, ToDate, LeaveTypeInfo)
                        End If

                        'Check Allowed occurance for day, wee and year
                        If Not CheckAllowedOccuranceForDayWeekMonth(EmployeeId, LeaveType, FromDate, ToDate, LeaveTypeInfo, ErrorMsg) Then
                            Return False
                        End If

                        'Validate Allowed Balance
                        If Not CheckEmpAllowedLeaveBalance(EmployeeId, FromDate, ToDate, LeaveTypeInfo, EmpLeaveTotalBalance, ErrorMsg) Then
                            Return False
                        End If

                        'Validate Allowed Gender
                        If Not CheckLeaveAllowedGender(EmployeeId, LeaveTypeInfo, ErrorMsg) Then
                            Return False
                        End If

                        'Validate Allowed Employee Type
                        If Not CheckLeaveAllowedEmployeeType(EmployeeId, LeaveTypeInfo, ErrorMsg) Then
                            Return False
                        End If

                    End If
                    Return True
                End If
            End With
        End Function

        Sub AddLeaveAllProcess(ByVal EmployeeId As Integer, ByVal OffAndHolidayDays As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, _
                                    ByVal DBFromDate As DateTime, ByVal DBToDate As DateTime, ByVal DBRequestDate As DateTime, ByVal Remarks As String, _
                                   ByVal LeaveType As String, ByRef EmpLeaveTotalBalance As Double, ByVal AttachedFile As String, ByRef strMessage As String)
            Dim err As Integer = -1

            Dim OffOrHolidayDaysCount As Integer
            OffOrHolidayDaysCount = 0

            If OffAndHolidayDays > 0 Then
                OffOrHolidayDaysCount = ((ToDate - FromDate).Days + 1) - OffAndHolidayDays
            Else
                OffOrHolidayDaysCount = (ToDate - FromDate).Days + 1
            End If

            Me.FK_EmployeeId = EmployeeId
            Me.FK_LeaveTypeId = LeaveType
            Me.FromDate = DBFromDate
            Me.ToDate = DBToDate
            Me.RequestDate = DBRequestDate
            Me.Remarks = Remarks
            Me.IsHalfDay = False ' chkHalfDay.Checked
            Me.CREATED_BY = SessionVariables.LoginUser.ID
            Me.LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            Me.Days = OffOrHolidayDaysCount
            Me.AttachedFile = AttachedFile
            If LeaveRequestId = 0 Then
                err = Me.Add()
                LeaveRequestId = Me.LeaveRequestId
                objEmp_Leaves_BalanceHistory = New Emp_Leaves_BalanceHistory
                objEmp_Leaves_BalanceHistory.FK_EmployeeId = EmployeeId
                objEmp_Leaves_BalanceHistory.FK_EmpLeaveId = LeaveRequestId
                objEmp_Leaves_BalanceHistory.FK_LeaveId = Me.FK_LeaveTypeId
                objEmp_Leaves_BalanceHistory.BalanceDate = Date.Now
                objEmp_Leaves_BalanceHistory.Balance = Me.Days * -1
                objEmp_Leaves_BalanceHistory.TotalBalance = EmpLeaveTotalBalance - Me.Days
                objEmp_Leaves_BalanceHistory.Remarks = "Add New Leave"
                objEmp_Leaves_BalanceHistory.CREATED_DATE = Date.Now
                objEmp_Leaves_BalanceHistory.CREATED_BY = SessionVariables.LoginUser.ID
                objEmp_Leaves_BalanceHistory.Add()

                If SessionVariables.CultureInfo = "en-US" Then
                    strMessage = "Saved Successfully"
                Else
                    strMessage = "تم الحفظ بنجاح"
                End If

            Else
                Me.LeaveRequestId = LeaveRequestId
                err = Me.Update()

                objEmp_Leaves_BalanceHistory = New Emp_Leaves_BalanceHistory
                objEmp_Leaves_BalanceHistory.FK_EmployeeId = EmployeeId
                objEmp_Leaves_BalanceHistory.FK_EmpLeaveId = LeaveRequestId
                objEmp_Leaves_BalanceHistory.FK_LeaveId = Me.FK_LeaveTypeId
                objEmp_Leaves_BalanceHistory.GetLastForLeave()
                objEmp_Leaves_BalanceHistory.BalanceDate = Date.Now
                objEmp_Leaves_BalanceHistory.Balance = objEmp_Leaves_BalanceHistory.Balance * -1
                'objEmp_Leaves_BalanceHistory.TotalBalance = EmpLeaveTotalBalance + objEmp_Leaves_BalanceHistory.Balance
                objEmp_Leaves_BalanceHistory.TotalBalance = objEmp_Leaves_BalanceHistory.TotalBalance + objEmp_Leaves_BalanceHistory.Balance
                objEmp_Leaves_BalanceHistory.Remarks = "Update Leave Data - to rollback old record"
                objEmp_Leaves_BalanceHistory.CREATED_DATE = Date.Now
                objEmp_Leaves_BalanceHistory.CREATED_BY = SessionVariables.LoginUser.ID
                objEmp_Leaves_BalanceHistory.Add()



                objEmp_Leaves_BalanceHistory = New Emp_Leaves_BalanceHistory
                objEmp_Leaves_BalanceHistory.FK_EmployeeId = EmployeeId
                objEmp_Leaves_BalanceHistory.FK_EmpLeaveId = LeaveRequestId
                objEmp_Leaves_BalanceHistory.FK_LeaveId = Me.FK_LeaveTypeId
                objEmp_Leaves_BalanceHistory.BalanceDate = Date.Now
                'objEmp_Leaves_BalanceHistory.GetLastForLeave()
                objEmp_Leaves_BalanceHistory.Balance = Me.Days * -1
                objEmp_Leaves_BalanceHistory.TotalBalance = EmpLeaveTotalBalance - Me.Days
                objEmp_Leaves_BalanceHistory.Remarks = "Update Leave Data"
                objEmp_Leaves_BalanceHistory.CREATED_DATE = Date.Now
                objEmp_Leaves_BalanceHistory.CREATED_BY = SessionVariables.LoginUser.ID
                objEmp_Leaves_BalanceHistory.Add()


                If SessionVariables.CultureInfo = "en-US" Then
                    strMessage = "Updated Successfully"
                Else
                    strMessage = "تم التحديث بنجاح"
                End If
            End If
        End Sub

#End Region

        Public Function Add() As Integer

            Dim rslt As Integer = objDALHR_LeaveRequest.Add(_LeaveRequestId, _FK_EmployeeId, _FK_LeaveTypeId, _RequestDate, _FromDate, _ToDate, _Remarks, _IsHalfDay, _AttachedFile, _CREATED_BY, _Days, _IsRejected)
            App_EventsLog.Insert_ToEventLog("Add", _LeaveRequestId, "HR_LeaveRequest", "HR Leave Request")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALHR_LeaveRequest.Update(_LeaveRequestId, _FK_EmployeeId, _FK_LeaveTypeId, _RequestDate, _FromDate, _ToDate, _Remarks, _IsHalfDay, _Days, _AttachedFile, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE)
            App_EventsLog.Insert_ToEventLog("Update", _LeaveRequestId, "HR_LeaveRequest", "HR Leave Request")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALHR_LeaveRequest.Delete(_LeaveRequestId)
            App_EventsLog.Insert_ToEventLog("Delete", _LeaveRequestId, "HR_LeaveRequest", "HR Leave Request")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALHR_LeaveRequest.GetAll()

        End Function

        Public Function GetByPK() As HR_LeaveRequest

            Dim dr As DataRow
            dr = objDALHR_LeaveRequest.GetByPK(_LeaveRequestId)

            If Not IsDBNull(dr("LeaveRequestId")) Then
                _LeaveRequestId = dr("LeaveRequestId")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("FK_LeaveTypeId")) Then
                _FK_LeaveTypeId = dr("FK_LeaveTypeId")
            End If
            If Not IsDBNull(dr("RequestDate")) Then
                _RequestDate = dr("RequestDate")
            End If
            If Not IsDBNull(dr("FromDate")) Then
                _FromDate = dr("FromDate")
            End If
            If Not IsDBNull(dr("ToDate")) Then
                _ToDate = dr("ToDate")
            End If
            If Not IsDBNull(dr("Remarks")) Then
                _Remarks = dr("Remarks")
            End If
            If Not IsDBNull(dr("IsHalfDay")) Then
                _IsHalfDay = dr("IsHalfDay")
            End If
            If Not IsDBNull(dr("Days")) Then
                _Days = dr("Days")
            End If
            If Not IsDBNull(dr("AttachedFile")) Then
                _AttachedFile = dr("AttachedFile")
            End If
            If Not IsDBNull(dr("CREATED_BY")) Then
                _CREATED_BY = dr("CREATED_BY")
            End If
            If Not IsDBNull(dr("CREATED_DATE")) Then
                _CREATED_DATE = dr("CREATED_DATE")
            End If
            If Not IsDBNull(dr("LAST_UPDATE_BY")) Then
                _LAST_UPDATE_BY = dr("LAST_UPDATE_BY")
            End If
            If Not IsDBNull(dr("LAST_UPDATE_DATE")) Then
                _LAST_UPDATE_DATE = dr("LAST_UPDATE_DATE")
            End If
            If Not IsDBNull(dr("IsRejected")) Then
                _IsRejected = dr("IsRejected")
            End If
            Return Me
        End Function

        Public Function GetOccuranceForWeek() As Integer

            Return objDALHR_LeaveRequest.GetOccuranceForWeek(_FromDate, _FK_EmployeeId, _FK_LeaveTypeId)

        End Function

        Public Function GetOccuranceForMonth() As Integer

            Return objDALHR_LeaveRequest.GetOccuranceForMonth(_FromDate, _FK_EmployeeId, _FK_LeaveTypeId)

        End Function

        Public Function GetOccuranceForYear() As Integer

            Return objDALHR_LeaveRequest.GetOccuranceForYear(_FromDate, _FK_EmployeeId, _FK_LeaveTypeId)

        End Function

        Public Function GetOccuranceForAllServiceTime() As Integer

            Return objDALHR_LeaveRequest.GetOccuranceForAllServiceTime(_FK_EmployeeId, _FK_LeaveTypeId)

        End Function

        Public Function GetAllLeavesByEmployee() As DataTable

            Return objDALHR_LeaveRequest.GetAllLeavesByEmployee(_FK_EmployeeId, _FromDate, _ToDate)

        End Function

        Public Function GetLeavesByLeaveStatus() As DataTable

            Return objDALHR_LeaveRequest.GetLeavesByLeaveStatus(_UserId)

        End Function

        Public Function GetAll_RejectedLeaves() As DataTable

            Return objDALHR_LeaveRequest.GetAll_RejectedLeaves()

        End Function

        Public Function GetLastBalanceForEmployees() As DataTable

            Return objDALHR_LeaveRequest.GetLastBalanceForEmployees(_FK_CompanyId, _FK_EntityId, _FK_LeaveTypeId, FK_EmployeeId)

        End Function

        Public Function GetLastBalanceForEmployee_info() As HR_LeaveRequest

            Dim dr As DataRow
            dr = objDALHR_LeaveRequest.GetLastBalanceForEmployee_Info(_FK_CompanyId, _FK_EntityId, _FK_LeaveTypeId, FK_EmployeeId)
            If Not dr Is Nothing Then
                If Not IsDBNull(dr("FK_EmployeeId")) Then
                    _FK_EmployeeId = dr("FK_EmployeeId")
                End If
                If Not IsDBNull(dr("EmployeeNo")) Then
                    _EmployeeNo = dr("EmployeeNo")
                End If
                If Not IsDBNull(dr("EmployeeName")) Then
                    _EmployeeName = dr("EmployeeName")
                End If
                If Not IsDBNull(dr("EmployeeArabicName")) Then
                    _EmployeeArabicName = dr("EmployeeArabicName")
                End If
                If Not IsDBNull(dr("LeaveName")) Then
                    _LeaveName = dr("LeaveName")
                End If
                If Not IsDBNull(dr("LeaveArabicName")) Then
                    _LeaveArabicName = dr("LeaveArabicName")
                End If
                If Not IsDBNull(dr("LeaveRequestId")) Then
                    _LeaveRequestId = dr("LeaveRequestId")
                End If
                If Not IsDBNull(dr("TotalBalance")) Then
                    _TotalBalance = dr("TotalBalance")
                End If
                If Not IsDBNull(dr("BalanceId")) Then
                    _BalanceId = dr("BalanceId")
                End If
                Return Me
            End If
        End Function

        Public Function GetCompanyAndEntity() As DataTable

            Return objDALHR_LeaveRequest.GetCompanyAndEntity(_FK_EmployeeId)

        End Function

        Public Function GetAllLeavesLists() As DataTable
            Return objDALHR_LeaveRequest.GetAllLeaveLists(_FromDate, _ToDate)
        End Function

        Public Function GetEmpLeaveByMultiEmployees(ByVal strEmpIDs As String) As DataTable

            Return objDALHR_LeaveRequest.GetEmpLeaveByMultiEmployees(strEmpIDs)

        End Function

        Public Function GetAllWithEmployeeInner() As DataTable

            Return objDALHR_LeaveRequest.GetAllWithEmployeeInner()

        End Function

        Public Function Update_Leave_RequestStatus() As Integer

            Return objDALHR_LeaveRequest.Update_Leave_RequestStatus(_LeaveRequestId, _IsRejected, _RejectionReason, _LAST_UPDATE_BY)

        End Function

        Public Function HR_LeaveRequestHasRequest(ByVal FK_EmployeeId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByRef ErrorMsg As String) As Boolean
            Dim dt As DataTable = objDALHR_LeaveRequest.HR_LeaveRequestHasRequest(FK_EmployeeId, FromDate, ToDate)
            If Not dt Is Nothing Then
                If dt.Rows(0)("HasHR_LeaveRequest") = True Then
                    If SessionVariables.CultureInfo = "en-US" Then
                        ErrorMsg = "Leave Record Already Exists Between The Date Range"
                    Else
                        ErrorMsg = "الاجازة موجودة مسبقاً في حدود التاريخ"
                    End If
                    Return True
                End If
            End If
            Return False
        End Function
#End Region

    End Class
End Namespace