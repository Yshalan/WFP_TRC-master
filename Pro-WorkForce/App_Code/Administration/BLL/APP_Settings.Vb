Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events
Imports SmartV.UTILITIES

Namespace TA.Admin

    Public Class APP_Settings

#Region "Class Variables"
        Private _appl_setup_id As Integer
        Private _CompanyName1 As String
        Private _CompanyName2 As String
        Private _companyArabicName1 As String
        Private _CompanyArabicName2 As String
        Private _LogoImage As Byte()
        Private _LogoPath As String
        Private _EmployeeNoLength As Integer
        Private _EmployeeCardLength As Integer
        Private _IsGradeOvertimeRule As Boolean
        Private _IsGradeTAException As Boolean
        Private _AnnualLeaveOption As Integer
        Private _IsGraceTAPolicy As Boolean
        Private _MinGapbetweenMoves As Integer
        Private _LeaveApproval As Integer
        Private _ManageOvertime As Integer
        Private _ConsequenceTransactions As Integer
        Private _Other1 As String
        Private _Other2 As String
        Private _Other3 As String
        Private _CREATED_DATE As Date
        Private _SystemUsersType As Integer
        Private _AllowEditOverTime As Boolean
        Private _HRGroupEmail As String
        Private _HREmailNotification As Integer
        Private _ConsiderAbsentAfter As Integer
        Private _LeaveApprovalfromLeave As Boolean
        Private _DynamicReportView As Integer
        Private _SendToManagerDaily As Boolean
        Private _SendToManagerWeekly As Boolean
        Private _SendToManagerMonthly As Boolean
        Private _SendToHRDaily As Boolean
        Private _SendToHRWeekly As Boolean
        Private _SendToHRMonthly As Boolean
        Private _NoShiftShcedule As Integer
        Private _NursingDays As Integer
        Private _DaysMinutes As Integer
        Private _HasFullDayPermission As Boolean
        Private _AllowChangeEmpNo As Integer
        Private _AllowedBefore As String
        Private _StudyPermissionSchedule As Integer
        Private _NursingPermissionAttend As Boolean
        Private _PermApprovalFromPermission As Boolean
        Private _ManualEntryNo As String
        Private _MinOutViolation As Integer
        Private _ManagerEmailFormat As Integer
        Private _HREmailFormat As Integer
        Private _ManualEntryManagerLevelRequired As Integer
        Private _HasEmailApproval As Boolean
        Private _ShowLoginForm As Boolean
        Private _ShowViolationCorrection As Boolean
        Private _FK_MaternityLeaveTypeId As Integer
        Private _FK_PersonalPermissionTypeId As Integer
        Private _NursingRequireMaternity As Boolean
        Private _RequestGridToAppear As String
        Private _EarlyOutNotificationAfter As Integer
        Private _PermissionRequestReminderAfter As Integer
        Private _HasFlexiblePermission As Boolean
        Private _HasFlexibleNursingPermission As Boolean
        Private _ShowAbsentInViolationCorrection As Boolean
        Private _HasPermissionForPeriod As Boolean
        Private _IsToConsiderBalanceInHours As Boolean
        Private _ShowThemeToUsers As Boolean
        Private _DurationTotalsToAppear As String
        Private _AttachmentIsMandatory As Boolean
        Private _IsFirstGrid As Boolean
        Private _DefaultStudyPermissionFromTime As Integer
        Private _DefaultStudyPermissionToTime As Integer
        Private _DefaultStudyPermissionFlexibleTime As Integer
        Private _ShowAnnouncement As Boolean
        Private _DivideTwoPermission As Boolean
        Private _MaternityLeaveDuration As Integer
        Private _StudyPermissionApproval As Integer
        Private _NursingPermissionApproval As Integer
        Private _MaxStudyPermission As Integer
        Private _StudyGeneralGuide As String
        Private _StudyGeneralGuideAr As String
        Private _NursingGeneralGuide As String
        Private _NursingGeneralGuideAr As String
        Private _AllowMoreOneManualEntry As Boolean
        Private _ShowAnnouncementSelfService As Boolean
        Private _OnCallSchedule As Integer
        Private _AllowNursingInRamadan As Boolean
        Private _ShowEmployeeList As Boolean

        Private _MgrAbsentWeekly As Integer
        Private _MgrAbsentMonthly As Integer
        Private _MgrViolationWeekly As Integer
        Private _MgrViolationMonthly As Integer

        Private _HRAbsentWeekly As Integer
        Private _HRAbsentMonthly As Integer
        Private _HRViolationWeekly As Integer
        Private _HRViolationMonthly As Integer

        Private _ConsiderAbsentOrLogicalAbsent As Integer
        Private _ReminderAbsentAfter As Integer
        Private _MgrDailyWeekly As Integer
        Private _MgrDailyMonthly As Integer
        Private _MgrDetailedWeekly As Integer
        Private _MgrDetailedMonthly As Integer
        Private _MgrSummaryWeekly As Integer
        Private _MgrSummaryMonthly As Integer
        Private _HRDailyWeekly As Integer
        Private _HRDailyMonthly As Integer
        Private _HRDetailedWeekly As Integer
        Private _HRDetailedMonthly As Integer
        Private _HRSummaryWeekly As Integer
        Private _HRSummaryMonthly As Integer
        Private _ConsiderAbsentEvenAttend As Boolean
        Private _DefaultReportFormat As Integer

        Private _AutoPersonalPermissionDelay As Boolean
        Private _AutoPermissionDelayDuration As Integer
        Private _AutoPersonalPermissionEarlyOut As Boolean
        Private _AutoPermissionEarlyOutDuration As Integer
        Private _HasMultiApproval As Boolean
        Private _EmployeeManagerFilter As Integer
        Private _FillCheckBoxList As Integer
        Private _AllowDeleteSchedule As Boolean
        Private _DefaultTheme As String
        Private _ArchivingMonths As Integer
        Private _ShowLGWithEntityPrivilege As Boolean
        Private _IncompleteWorkingHrs As String
        Private _ManagerDefaultPage As Integer
        Private _ShowDirectStaffChk As Boolean
        Private _IsDailyReportWithColor As Boolean
        Private _ApprovalRecalMethod As Integer
        Private _StudyPerm_NotificationException As Boolean
        Private _NursingPerm_NotificationException As Boolean
        Private _AttachmentIsMandatoryManualEntryRequest As Boolean
        Private _AttachmentIsMandatoryHRManualEntry As Boolean
        Private _SendToEntityManagerWeekly As Boolean
        Private _SendToEntityManagerMonthly As Boolean
        Private _SendToEmployeeWeekly As Boolean
        Private _SendToEmployeeMonthly As Boolean
        Private _EntityManagerViolationWeekly As Integer
        Private _EntityManagerViolationMonthly As Integer
        Private _EmployeeViolationWeekly As Integer
        Private _EmployeeViolationMonthly As Integer
        Private _DailyReportDate As Integer
        Private _ExcludeGraceFromLostTime As Boolean
        Private _YearlyReportFromYearBegining As Boolean
        Private _IncludeNotifications_CoordinatorTypes As Boolean
        Private _EmployeeAbsentWeekly As Integer
        Private _EmployeeAbsentMonthly As Integer
        Private _EmployeeDetailedAbsentWeekly As Integer
        Private _EmployeeDetailedAbsentMonthly As Integer
        Private _EmployeeLostTimeDetailsWeekly As Integer
        Private _EmployeeLostTimeDetailsMonthly As Integer
        Private _MgrEmpSummaryAttendanceWeekly As Integer
        Private _MgrEmpSummaryAttendanceMonthly As Integer
        Private _EnableSemesterSelection_StudyPermission As Boolean
        Private _MustCompleteNoHours_RequestPermission As Boolean
        Private _NoOfHours_RequestPermission As Integer
        Private _IncludeConsiderInWorkPermissions_RequestPermission As Boolean
        Private _MgrEmpDeductionWeekly As Integer
        Private _MgrEmpDeductionMonthly As Integer
        Private _HrEmpDeductionWeekly As Integer
        Private _HrEmpDeductionMonthly As Integer
        Private _EmployeeEmpDeductionWeekly As Integer
        Private _EmployeeEmpDeductionMonthly As Integer
        Private _EntityManagerEmpDeductionWeekly As Integer
        Private _EntityManagerEmpDeductionMonthly As Integer
        Private _DeductionEmailFormat As String
        Private _MonthlyDeduction_Report As Integer
        Private _FK_CompanyId As Integer
        Private _PasswordType As String
        Private _ShowLeaveLnk_ViolationCorrection As Boolean
        Private _ShowPermissionLnk_ViolationCorrection As Boolean
        Private _ConsiderLeaveOnOffDay As Integer
        Private _ViolationJustificationDays As String
        Private _ViolationJustificationDaysPolicy As String
        Private _ManualEntryNoPerMonth As String
        Private _EmployeeSummaryWeekly As Integer
        Private _EmployeeSummaryMonthly As Integer
        Private _EntityManagerMaxAbsentMonthly As Integer
        Private _EntityManagerMaxAbsentWeekly As Integer
        Private _EntityManagerMaxDelayMonthly As Integer
        Private _EntityManagerMaxDelayWeekly As Integer
        Private _SendDaily_AbsentDelay_EntityMgr As Boolean
        Private _IsAbsentRestPolicy As Boolean
        Private _MgrDeductionPerPolicyMonthly As Integer
        Private _MgrDeductionPerPolicyWeekly As Integer
        Private _HRDeductionPerPolicyMonthly As Integer
        Private _HRDeductionPerPolicyWeekly As Integer
        Private _EmployeeDeductionPerPolicyMonthly As Integer
        Private _EmployeeDeductionPerPolicyWeekly As Integer
        Private _EntityManagerDeductionPerPolicyMonthly As Integer
        Private _EntityManagerDeductionPerPolicyWeekly As Integer
        Private _DeductionPerPolicy_Value As Integer
        Private _AllowEditManualEntryRequestDate As Boolean
        Private _AllowEditManualEntryRequestTime As Boolean
        Private _NumberInTransactionRequests As String
        Private _NumberOutTransactionRequests As String
        Private _IsAutoApproveManualEntryRequest As Boolean
        Private _ConsiderRestInshiftSch As Integer
        Private _ConsiderNursingInRamadan As Boolean
        Private _RemoteWorkTAReason As String
        Private _HasTawajudFeatures As Boolean
        Private _HasMultiLocations As Boolean
        Private _HasHeartBeat As Boolean
        Private _HasFeedback As Boolean
        Private _AllowFingerPunch As Boolean
        Private _AllowFingerLogin As Boolean
        Private _AllowFacePunch As Boolean
        Private _AllowFaceLogin As Boolean
        Private _EnableUniversitySelection_StudyPermission As Boolean
        Private _StudyAllowedAfterDays As Integer?
        Private _StudyAllowedBeforeDays As Integer?
        Private _EnableMajorSelection_StudyPermission As Boolean?
        Private objDALAPP_Settings As DALAPP_Settings
        Private objDALApp_EventsLog As DALApp_EventsLog
        Private _RemarkIsMandatoryManualEntryRequest As Boolean

#End Region

#Region "Public Properties"

        Public Property IsDailyReportWithColor() As Boolean
            Get
                Return _IsDailyReportWithColor
            End Get
            Set(ByVal value As Boolean)
                _IsDailyReportWithColor = value
            End Set
        End Property
        Public Property appl_setup_id() As Integer
            Set(ByVal value As Integer)
                _appl_setup_id = value
            End Set
            Get
                Return (_appl_setup_id)
            End Get
        End Property
        Public Property CompanyName1() As String
            Set(ByVal value As String)
                _CompanyName1 = value
            End Set
            Get
                Return (_CompanyName1)
            End Get
        End Property
        Public Property CompanyName2() As String
            Set(ByVal value As String)
                _CompanyName2 = value
            End Set
            Get
                Return (_CompanyName2)
            End Get
        End Property
        Public Property companyArabicName1() As String
            Set(ByVal value As String)
                _companyArabicName1 = value
            End Set
            Get
                Return (_companyArabicName1)
            End Get
        End Property
        Public Property CompanyArabicName2() As String
            Set(ByVal value As String)
                _CompanyArabicName2 = value
            End Set
            Get
                Return (_CompanyArabicName2)
            End Get
        End Property
        Public Property LogoImage() As Byte()
            Set(ByVal value As Byte())
                _LogoImage = value
            End Set
            Get
                Return (_LogoImage)
            End Get
        End Property
        Public Property LogoPath() As String
            Set(ByVal value As String)
                _LogoPath = value
            End Set
            Get
                Return (_LogoPath)
            End Get
        End Property
        Public Property EmployeeNoLength() As Integer
            Set(ByVal value As Integer)
                _EmployeeNoLength = value
            End Set
            Get
                Return (_EmployeeNoLength)
            End Get
        End Property
        Public Property EmployeeCardLength() As Integer
            Set(ByVal value As Integer)
                _EmployeeCardLength = value
            End Set
            Get
                Return (_EmployeeCardLength)
            End Get
        End Property
        Public Property IsGradeOvertimeRule() As Boolean
            Set(ByVal value As Boolean)
                _IsGradeOvertimeRule = value
            End Set
            Get
                Return (_IsGradeOvertimeRule)
            End Get
        End Property
        Public Property IsGradeTAException() As Boolean
            Set(ByVal value As Boolean)
                _IsGradeTAException = value
            End Set
            Get
                Return (_IsGradeTAException)
            End Get
        End Property
        Public Property AnnualLeaveOption() As Integer
            Set(ByVal value As Integer)
                _AnnualLeaveOption = value
            End Set
            Get
                Return (_AnnualLeaveOption)
            End Get
        End Property
        Public Property MinGapbetweenMoves() As Integer
            Set(ByVal value As Integer)
                _MinGapbetweenMoves = value
            End Set
            Get
                Return (_MinGapbetweenMoves)
            End Get
        End Property
        Public Property ManageOvertime() As Integer
            Set(ByVal value As Integer)
                _ManageOvertime = value
            End Set
            Get
                Return (_ManageOvertime)
            End Get
        End Property
        Public Property ConsequenceTransactions() As Integer
            Set(ByVal value As Integer)
                _ConsequenceTransactions = value
            End Set
            Get
                Return (_ConsequenceTransactions)
            End Get
        End Property
        Public Property Other1() As String
            Set(ByVal value As String)
                _Other1 = value
            End Set
            Get
                Return (_Other1)
            End Get
        End Property
        Public Property Other2() As String
            Set(ByVal value As String)
                _Other2 = value
            End Set
            Get
                Return (_Other2)
            End Get
        End Property
        Public Property Other3() As String
            Set(ByVal value As String)
                _Other3 = value
            End Set
            Get
                Return (_Other3)
            End Get
        End Property

        Public Property CREATED_DATE() As Date
            Set(ByVal value As Date)
                _CREATED_DATE = value
            End Set
            Get
                Return (_CREATED_DATE)
            End Get
        End Property

        Public Property IsGraceTAPolicy() As Boolean
            Set(ByVal value As Boolean)
                _IsGraceTAPolicy = value
            End Set
            Get
                Return (_IsGraceTAPolicy)
            End Get
        End Property

        Public Property LeaveApproval() As Integer
            Set(ByVal value As Integer)
                _LeaveApproval = value
            End Set
            Get
                Return (_LeaveApproval)
            End Get
        End Property

        Public Property SystemUsersType() As Integer
            Set(ByVal value As Integer)
                _SystemUsersType = value
            End Set
            Get
                Return (_SystemUsersType)
            End Get
        End Property

        Public Property HRGroupEmail() As String
            Set(ByVal value As String)
                _HRGroupEmail = value
            End Set
            Get
                Return (_HRGroupEmail)
            End Get
        End Property

        Public Property HREmailNotification() As Integer
            Set(ByVal value As Integer)
                _HREmailNotification = value
            End Set
            Get
                Return (_HREmailNotification)
            End Get
        End Property

        Public Property AllowEditOverTime() As Boolean
            Set(ByVal value As Boolean)
                _AllowEditOverTime = value
            End Set
            Get
                Return (_AllowEditOverTime)
            End Get
        End Property

        Public Property ConsiderAbsentAfter() As Integer
            Set(ByVal value As Integer)
                _ConsiderAbsentAfter = value
            End Set
            Get
                Return (_ConsiderAbsentAfter)
            End Get
        End Property

        Public Property LeaveApprovalfromLeave() As Boolean
            Get
                Return _LeaveApprovalfromLeave
            End Get
            Set(ByVal value As Boolean)
                _LeaveApprovalfromLeave = value
            End Set
        End Property

        Public Property DynamicReportView() As Integer
            Get
                Return _DynamicReportView
            End Get
            Set(ByVal value As Integer)
                _DynamicReportView = value
            End Set
        End Property

        Public Property SendToManagerDaily() As Boolean
            Get
                Return _SendToManagerDaily
            End Get
            Set(ByVal value As Boolean)
                _SendToManagerDaily = value
            End Set
        End Property

        Public Property SendToManagerWeekly() As Boolean
            Get
                Return _SendToManagerWeekly
            End Get
            Set(ByVal value As Boolean)
                _SendToManagerWeekly = value
            End Set
        End Property

        Public Property SendToManagerMonthly() As Boolean
            Get
                Return _SendToManagerMonthly
            End Get
            Set(ByVal value As Boolean)
                _SendToManagerMonthly = value
            End Set
        End Property

        Public Property SendToHRDaily() As Boolean
            Get
                Return _SendToHRDaily
            End Get
            Set(ByVal value As Boolean)
                _SendToHRDaily = value
            End Set
        End Property

        Public Property SendToHRWeekly() As Boolean
            Get
                Return _SendToHRWeekly
            End Get
            Set(ByVal value As Boolean)
                _SendToHRWeekly = value
            End Set
        End Property

        Public Property SendToHRMonthly() As Boolean
            Get
                Return _SendToHRMonthly
            End Get
            Set(ByVal value As Boolean)
                _SendToHRMonthly = value
            End Set
        End Property

        Public Property NoShiftShcedule() As Integer
            Get
                Return _NoShiftShcedule
            End Get
            Set(ByVal value As Integer)
                _NoShiftShcedule = value
            End Set
        End Property

        Public Property NursingDays() As Integer
            Get
                Return _NursingDays
            End Get
            Set(ByVal value As Integer)
                _NursingDays = value
            End Set
        End Property

        Public Property DaysMinutes() As Integer
            Get
                Return _DaysMinutes
            End Get
            Set(ByVal value As Integer)
                _DaysMinutes = value
            End Set
        End Property

        Public Property HasFullDayPermission() As Boolean
            Get
                Return _HasFullDayPermission
            End Get
            Set(ByVal value As Boolean)
                _HasFullDayPermission = value
            End Set
        End Property

        Public Property AllowChangeEmpNo() As Integer
            Get
                Return _AllowChangeEmpNo
            End Get
            Set(ByVal value As Integer)
                _AllowChangeEmpNo = value
            End Set
        End Property

        Public Property AllowedBefore() As String
            Get
                Return _AllowedBefore
            End Get
            Set(ByVal value As String)
                _AllowedBefore = value
            End Set
        End Property

        Public Property StudyPermissionSchedule() As Integer
            Get
                Return _StudyPermissionSchedule
            End Get
            Set(ByVal value As Integer)
                _StudyPermissionSchedule = value
            End Set
        End Property

        Public Property NursingPermissionAttend() As Boolean
            Get
                Return _NursingPermissionAttend
            End Get
            Set(ByVal value As Boolean)
                _NursingPermissionAttend = value
            End Set
        End Property

        Public Property PermApprovalFromPermission() As Boolean
            Get
                Return _PermApprovalFromPermission
            End Get
            Set(ByVal value As Boolean)
                _PermApprovalFromPermission = value
            End Set
        End Property

        Public Property ManualEntryNo() As String
            Get
                Return _ManualEntryNo
            End Get
            Set(ByVal value As String)
                _ManualEntryNo = value
            End Set
        End Property

        Public Property MinOutViolation() As String
            Get
                Return _MinOutViolation
            End Get
            Set(ByVal value As String)
                _MinOutViolation = value
            End Set
        End Property

        Public Property ManagerEmailFormat() As Integer
            Get
                Return _ManagerEmailFormat
            End Get
            Set(ByVal value As Integer)
                _ManagerEmailFormat = value
            End Set
        End Property

        Public Property HREmailFormat() As Integer
            Get
                Return _HREmailFormat
            End Get
            Set(ByVal value As Integer)
                _HREmailFormat = value
            End Set
        End Property

        Public Property ManualEntryManagerLevelRequired() As Integer
            Get
                Return _ManualEntryManagerLevelRequired
            End Get
            Set(ByVal value As Integer)
                _ManualEntryManagerLevelRequired = value
            End Set
        End Property

        Public Property HasEmailApproval() As Boolean
            Get
                Return _HasEmailApproval
            End Get
            Set(ByVal value As Boolean)
                _HasEmailApproval = value
            End Set
        End Property

        Public Property ShowLoginForm() As Boolean
            Get
                Return _ShowLoginForm
            End Get
            Set(ByVal value As Boolean)
                _ShowLoginForm = value
            End Set
        End Property

        Public Property ShowViolationCorrection() As Boolean
            Get
                Return _ShowViolationCorrection
            End Get
            Set(ByVal value As Boolean)
                _ShowViolationCorrection = value
            End Set
        End Property

        Public Property FK_MaternityLeaveTypeId() As Integer
            Get
                Return _FK_MaternityLeaveTypeId
            End Get
            Set(ByVal value As Integer)
                _FK_MaternityLeaveTypeId = value
            End Set
        End Property

        Public Property FK_PersonalPermissionTypeId() As Integer
            Get
                Return _FK_PersonalPermissionTypeId
            End Get
            Set(ByVal value As Integer)
                _FK_PersonalPermissionTypeId = value
            End Set
        End Property

        Public Property NursingRequireMaternity() As Boolean
            Get
                Return _NursingRequireMaternity
            End Get
            Set(ByVal value As Boolean)
                _NursingRequireMaternity = value
            End Set
        End Property

        Public Property RequestGridToAppear() As String
            Set(ByVal value As String)
                _RequestGridToAppear = value
            End Set
            Get
                Return (_RequestGridToAppear)
            End Get
        End Property

        Public Property EarlyOutNotificationAfter() As Integer
            Set(ByVal value As Integer)
                _EarlyOutNotificationAfter = value
            End Set
            Get
                Return (_EarlyOutNotificationAfter)
            End Get
        End Property

        Public Property PermissionRequestReminderAfter() As Integer
            Set(ByVal value As Integer)
                _PermissionRequestReminderAfter = value
            End Set
            Get
                Return (_PermissionRequestReminderAfter)
            End Get
        End Property

        Public Property HasFlexiblePermission() As Boolean
            Get
                Return _HasFlexiblePermission
            End Get
            Set(ByVal value As Boolean)
                _HasFlexiblePermission = value
            End Set
        End Property

        Public Property HasFlexibleNursingPermission() As Boolean
            Get
                Return _HasFlexibleNursingPermission
            End Get
            Set(ByVal value As Boolean)
                _HasFlexibleNursingPermission = value
            End Set
        End Property

        Public Property ShowAbsentInViolationCorrection() As Boolean
            Get
                Return _ShowAbsentInViolationCorrection
            End Get
            Set(ByVal value As Boolean)
                _ShowAbsentInViolationCorrection = value
            End Set
        End Property

        Public Property HasPermissionForPeriod() As Boolean
            Get
                Return _HasPermissionForPeriod
            End Get
            Set(ByVal value As Boolean)
                _HasPermissionForPeriod = value
            End Set
        End Property

        Public Property IsToConsiderBalanceInHours() As Boolean
            Get
                Return _IsToConsiderBalanceInHours
            End Get
            Set(ByVal value As Boolean)
                _IsToConsiderBalanceInHours = value
            End Set
        End Property

        Public Property ShowThemeToUsers() As Boolean
            Get
                Return _ShowThemeToUsers
            End Get
            Set(ByVal value As Boolean)
                _ShowThemeToUsers = value
            End Set
        End Property

        Public Property DurationTotalsToAppear() As String
            Set(ByVal value As String)
                _DurationTotalsToAppear = value
            End Set
            Get
                Return (_DurationTotalsToAppear)
            End Get
        End Property

        Public Property AttachmentIsMandatory() As Boolean
            Get
                Return _AttachmentIsMandatory
            End Get
            Set(ByVal value As Boolean)
                _AttachmentIsMandatory = value
            End Set
        End Property

        Public Property IsFirstGrid() As Boolean
            Get
                Return _IsFirstGrid
            End Get
            Set(ByVal value As Boolean)
                _IsFirstGrid = value
            End Set
        End Property

        Public Property DefaultStudyPermissionFromTime() As Integer
            Set(ByVal value As Integer)
                _DefaultStudyPermissionFromTime = value
            End Set
            Get
                Return (_DefaultStudyPermissionFromTime)
            End Get
        End Property

        Public Property DefaultStudyPermissionToTime() As Integer
            Set(ByVal value As Integer)
                _DefaultStudyPermissionToTime = value
            End Set
            Get
                Return (_DefaultStudyPermissionToTime)
            End Get
        End Property

        Public Property DefaultStudyPermissionFlexibleTime() As Integer
            Set(ByVal value As Integer)
                _DefaultStudyPermissionFlexibleTime = value
            End Set
            Get
                Return (_DefaultStudyPermissionFlexibleTime)
            End Get
        End Property

        Public Property ShowAnnouncement() As Boolean
            Set(ByVal value As Boolean)
                _ShowAnnouncement = value
            End Set
            Get
                Return (_ShowAnnouncement)
            End Get
        End Property

        Public Property DivideTwoPermission() As Boolean
            Set(ByVal value As Boolean)
                _DivideTwoPermission = value
            End Set
            Get
                Return (_DivideTwoPermission)
            End Get
        End Property

        Public Property MaternityLeaveDuration() As Integer
            Set(ByVal value As Integer)
                _MaternityLeaveDuration = value
            End Set
            Get
                Return (_MaternityLeaveDuration)
            End Get
        End Property

        Public Property StudyPermissionApproval() As Integer
            Set(ByVal value As Integer)
                _StudyPermissionApproval = value
            End Set
            Get
                Return (_StudyPermissionApproval)
            End Get
        End Property

        Public Property NursingPermissionApproval() As Integer
            Set(ByVal value As Integer)
                _NursingPermissionApproval = value
            End Set
            Get
                Return (_NursingPermissionApproval)
            End Get
        End Property

        Public Property MaxStudyPermission() As Integer
            Set(ByVal value As Integer)
                _MaxStudyPermission = value
            End Set
            Get
                Return (_MaxStudyPermission)
            End Get
        End Property

        Public Property StudyGeneralGuide() As String
            Set(ByVal value As String)
                _StudyGeneralGuide = value
            End Set
            Get
                Return (_StudyGeneralGuide)
            End Get
        End Property

        Public Property StudyGeneralGuideAr() As String
            Set(ByVal value As String)
                _StudyGeneralGuideAr = value
            End Set
            Get
                Return (_StudyGeneralGuideAr)
            End Get
        End Property

        Public Property NursingGeneralGuide() As String
            Set(ByVal value As String)
                _NursingGeneralGuide = value
            End Set
            Get
                Return (_NursingGeneralGuide)
            End Get
        End Property

        Public Property NursingGeneralGuideAr() As String
            Set(ByVal value As String)
                _NursingGeneralGuideAr = value
            End Set
            Get
                Return (_NursingGeneralGuideAr)
            End Get
        End Property

        Public Property AllowMoreOneManualEntry() As Boolean
            Set(ByVal value As Boolean)
                _AllowMoreOneManualEntry = value
            End Set
            Get
                Return (_AllowMoreOneManualEntry)
            End Get
        End Property


        Public Property ShowAnnouncementSelfService() As Boolean
            Set(ByVal value As Boolean)
                _ShowAnnouncementSelfService = value
            End Set
            Get
                Return (_ShowAnnouncementSelfService)
            End Get
        End Property

        Public Property OnCallSchedule() As Integer
            Set(ByVal value As Integer)
                _OnCallSchedule = value
            End Set
            Get
                Return (_OnCallSchedule)
            End Get
        End Property

        Public Property AllowNursingInRamadan() As Boolean
            Set(ByVal value As Boolean)
                _AllowNursingInRamadan = value
            End Set
            Get
                Return (_AllowNursingInRamadan)
            End Get
        End Property

        Public Property ShowEmployeeList() As Boolean
            Set(ByVal value As Boolean)
                _ShowEmployeeList = value
            End Set
            Get
                Return (_ShowEmployeeList)
            End Get
        End Property


        Public Property MgrAbsentWeekly() As Integer
            Set(ByVal value As Integer)
                _MgrAbsentWeekly = value
            End Set
            Get
                Return (_MgrAbsentWeekly)
            End Get
        End Property
        Public Property MgrAbsentMonthly() As Integer
            Set(ByVal value As Integer)
                _MgrAbsentMonthly = value
            End Set
            Get
                Return (_MgrAbsentMonthly)
            End Get
        End Property
        Public Property MgrViolationWeekly() As Integer
            Set(ByVal value As Integer)
                _MgrViolationWeekly = value
            End Set
            Get
                Return (_MgrViolationWeekly)
            End Get
        End Property
        Public Property MgrViolationMonthly() As Integer
            Set(ByVal value As Integer)
                _MgrViolationMonthly = value
            End Set
            Get
                Return (_MgrViolationMonthly)
            End Get
        End Property

        Public Property HRAbsentWeekly() As Integer
            Set(ByVal value As Integer)
                _HRAbsentWeekly = value
            End Set
            Get
                Return (_HRAbsentWeekly)
            End Get
        End Property
        Public Property HRAbsentMonthly() As Integer
            Set(ByVal value As Integer)
                _HRAbsentMonthly = value
            End Set
            Get
                Return (_HRAbsentMonthly)
            End Get
        End Property
        Public Property HRViolationWeekly() As Integer
            Set(ByVal value As Integer)
                _HRViolationWeekly = value
            End Set
            Get
                Return (_HRViolationWeekly)
            End Get
        End Property
        Public Property HRViolationMonthly() As Integer
            Set(ByVal value As Integer)
                _HRViolationMonthly = value
            End Set
            Get
                Return (_HRViolationMonthly)
            End Get
        End Property

        Public Property ConsiderAbsentOrLogicalAbsent() As Integer
            Set(ByVal value As Integer)
                _ConsiderAbsentOrLogicalAbsent = value
            End Set
            Get
                Return (_ConsiderAbsentOrLogicalAbsent)
            End Get
        End Property

        Public Property ReminderAbsentAfter() As Integer
            Set(ByVal value As Integer)
                _ReminderAbsentAfter = value
            End Set
            Get
                Return (_ReminderAbsentAfter)
            End Get
        End Property


        Public Property MgrDailyWeekly() As Integer
            Set(ByVal value As Integer)
                _MgrDailyWeekly = value
            End Set
            Get
                Return (_MgrDailyWeekly)
            End Get
        End Property
        Public Property MgrDailyMonthly() As Integer
            Set(ByVal value As Integer)
                _MgrDailyMonthly = value
            End Set
            Get
                Return (_MgrDailyMonthly)
            End Get
        End Property
        Public Property MgrDetailedWeekly() As Integer
            Set(ByVal value As Integer)
                _MgrDetailedWeekly = value
            End Set
            Get
                Return (_MgrDetailedWeekly)
            End Get
        End Property
        Public Property MgrDetailedMonthly() As Integer
            Set(ByVal value As Integer)
                _MgrDetailedMonthly = value
            End Set
            Get
                Return (_MgrDetailedMonthly)
            End Get
        End Property
        Public Property MgrSummaryWeekly() As Integer
            Set(ByVal value As Integer)
                _MgrSummaryWeekly = value
            End Set
            Get
                Return (_MgrSummaryWeekly)
            End Get
        End Property
        Public Property MgrSummaryMonthly() As Integer
            Set(ByVal value As Integer)
                _MgrSummaryMonthly = value
            End Set
            Get
                Return (_MgrSummaryMonthly)
            End Get
        End Property

        Public Property HRDailyWeekly() As Integer
            Set(ByVal value As Integer)
                _HRDailyWeekly = value
            End Set
            Get
                Return (_HRDailyWeekly)
            End Get
        End Property

        Public Property HRDailyMonthly() As Integer
            Set(ByVal value As Integer)
                _HRDailyMonthly = value
            End Set
            Get
                Return (_HRDailyMonthly)
            End Get
        End Property

        Public Property HRDetailedWeekly() As Integer
            Set(ByVal value As Integer)
                _HRDetailedWeekly = value
            End Set
            Get
                Return (_HRDetailedWeekly)
            End Get
        End Property
        Public Property HRDetailedMonthly() As Integer
            Set(ByVal value As Integer)
                _HRDetailedMonthly = value
            End Set
            Get
                Return (_HRDetailedMonthly)
            End Get
        End Property
        Public Property HRSummaryWeekly() As Integer
            Set(ByVal value As Integer)
                _HRSummaryWeekly = value
            End Set
            Get
                Return (_HRSummaryWeekly)
            End Get
        End Property
        Public Property HRSummaryMonthly() As Integer
            Set(ByVal value As Integer)
                _HRSummaryMonthly = value
            End Set
            Get
                Return (_HRSummaryMonthly)
            End Get
        End Property

        Public Property ConsiderAbsentEvenAttend() As Boolean
            Set(ByVal value As Boolean)
                _ConsiderAbsentEvenAttend = value
            End Set
            Get
                Return (_ConsiderAbsentEvenAttend)
            End Get
        End Property

        Public Property DefaultReportFormat() As Integer
            Set(ByVal value As Integer)
                _DefaultReportFormat = value
            End Set
            Get
                Return (_DefaultReportFormat)
            End Get
        End Property

        Public Property AutoPersonalPermissionDelay() As Boolean
            Set(ByVal value As Boolean)
                _AutoPersonalPermissionDelay = value
            End Set
            Get
                Return (_AutoPersonalPermissionDelay)
            End Get
        End Property
        Public Property AutoPermissionDelayDuration() As Integer
            Set(ByVal value As Integer)
                _AutoPermissionDelayDuration = value
            End Set
            Get
                Return (_AutoPermissionDelayDuration)
            End Get
        End Property
        Public Property AutoPersonalPermissionEarlyOut() As Boolean
            Set(ByVal value As Boolean)
                _AutoPersonalPermissionEarlyOut = value
            End Set
            Get
                Return (_AutoPersonalPermissionEarlyOut)
            End Get
        End Property
        Public Property AutoPermissionEarlyOutDuration() As Integer
            Set(ByVal value As Integer)
                _AutoPermissionEarlyOutDuration = value
            End Set
            Get
                Return (_AutoPermissionEarlyOutDuration)
            End Get
        End Property

        Public Property HasMultiApproval() As Boolean
            Set(ByVal value As Boolean)
                _HasMultiApproval = value
            End Set
            Get
                Return (_HasMultiApproval)
            End Get
        End Property

        Public Property EmployeeManagerFilter() As Integer
            Set(ByVal value As Integer)
                _EmployeeManagerFilter = value
            End Set
            Get
                Return (_EmployeeManagerFilter)
            End Get
        End Property

        Public Property FillCheckBoxList() As Integer
            Set(ByVal value As Integer)
                _FillCheckBoxList = value
            End Set
            Get
                Return (_FillCheckBoxList)
            End Get
        End Property

        Public Property AllowDeleteSchedule() As Boolean
            Set(ByVal value As Boolean)
                _AllowDeleteSchedule = value
            End Set
            Get
                Return (_AllowDeleteSchedule)
            End Get
        End Property

        Public Property DefaultTheme() As String
            Set(ByVal value As String)
                _DefaultTheme = value
            End Set
            Get
                Return (_DefaultTheme)
            End Get
        End Property

        Public Property ArchivingMonths() As Integer
            Set(ByVal value As Integer)
                _ArchivingMonths = value
            End Set
            Get
                Return (_ArchivingMonths)
            End Get
        End Property

        Public Property ShowLGWithEntityPrivilege() As String
            Set(ByVal value As String)
                _ShowLGWithEntityPrivilege = value
            End Set
            Get
                Return (_ShowLGWithEntityPrivilege)
            End Get
        End Property

        Public Property IncompleteWorkingHrs() As String
            Set(ByVal value As String)
                _IncompleteWorkingHrs = value
            End Set
            Get
                Return (_IncompleteWorkingHrs)
            End Get
        End Property

        Public Property ManagerDefaultPage() As Integer
            Set(ByVal value As Integer)
                _ManagerDefaultPage = value
            End Set
            Get
                Return (_ManagerDefaultPage)
            End Get
        End Property

        Public Property ShowDirectStaffChk() As Boolean
            Set(ByVal value As Boolean)
                _ShowDirectStaffChk = value
            End Set
            Get
                Return (_ShowDirectStaffChk)
            End Get
        End Property

        Public Property ApprovalRecalMethod() As Integer
            Set(ByVal value As Integer)
                _ApprovalRecalMethod = value
            End Set
            Get
                Return (_ApprovalRecalMethod)
            End Get
        End Property

        Public Property StudyPerm_NotificationException() As Boolean
            Set(ByVal value As Boolean)
                _StudyPerm_NotificationException = value
            End Set
            Get
                Return (_StudyPerm_NotificationException)
            End Get
        End Property

        Public Property NursingPerm_NotificationException() As Boolean
            Set(ByVal value As Boolean)
                _NursingPerm_NotificationException = value
            End Set
            Get
                Return (_NursingPerm_NotificationException)
            End Get
        End Property

        Public Property AttachmentIsMandatoryManualEntryRequest() As Boolean
            Set(ByVal value As Boolean)
                _AttachmentIsMandatoryManualEntryRequest = value
            End Set
            Get
                Return (_AttachmentIsMandatoryManualEntryRequest)
            End Get
        End Property

        Public Property RemarkIsMandatoryManualEntryRequest() As Boolean
            Set(ByVal value As Boolean)
                _RemarkIsMandatoryManualEntryRequest = value
            End Set
            Get
                Return (_RemarkIsMandatoryManualEntryRequest)
            End Get
        End Property



        Public Property AttachmentIsMandatoryHRManualEntry() As Boolean
            Set(ByVal value As Boolean)
                _AttachmentIsMandatoryHRManualEntry = value
            End Set
            Get
                Return (_AttachmentIsMandatoryHRManualEntry)
            End Get
        End Property

        Public Property SendToEntityManagerWeekly() As Boolean
            Set(ByVal value As Boolean)
                _SendToEntityManagerWeekly = value
            End Set
            Get
                Return (_SendToEntityManagerWeekly)
            End Get
        End Property

        Public Property SendToEntityManagerMonthly() As Boolean
            Set(ByVal value As Boolean)
                _SendToEntityManagerMonthly = value
            End Set
            Get
                Return (_SendToEntityManagerMonthly)
            End Get
        End Property

        Public Property SendToEmployeeWeekly() As Boolean
            Set(ByVal value As Boolean)
                _SendToEmployeeWeekly = value
            End Set
            Get
                Return (_SendToEmployeeWeekly)
            End Get
        End Property

        Public Property SendToEmployeeMonthly() As Boolean
            Set(ByVal value As Boolean)
                _SendToEmployeeMonthly = value
            End Set
            Get
                Return (_SendToEmployeeMonthly)
            End Get
        End Property

        Public Property EntityManagerViolationWeekly() As Integer
            Set(ByVal value As Integer)
                _EntityManagerViolationWeekly = value
            End Set
            Get
                Return (_EntityManagerViolationWeekly)
            End Get
        End Property

        Public Property EntityManagerViolationMonthly() As Integer
            Set(ByVal value As Integer)
                _EntityManagerViolationMonthly = value
            End Set
            Get
                Return (_EntityManagerViolationMonthly)
            End Get
        End Property

        Public Property EmployeeViolationWeekly() As Integer
            Set(ByVal value As Integer)
                _EmployeeViolationWeekly = value
            End Set
            Get
                Return (_EmployeeViolationWeekly)
            End Get
        End Property

        Public Property EmployeeViolationMonthly() As Integer
            Set(ByVal value As Integer)
                _EmployeeViolationMonthly = value
            End Set
            Get
                Return (_EmployeeViolationMonthly)
            End Get
        End Property

        Public Property DailyReportDate() As Integer
            Set(ByVal value As Integer)
                _DailyReportDate = value
            End Set
            Get
                Return (_DailyReportDate)
            End Get
        End Property

        Public Property ExcludeGraceFromLostTime() As Boolean
            Set(ByVal value As Boolean)
                _ExcludeGraceFromLostTime = value
            End Set
            Get
                Return (_ExcludeGraceFromLostTime)
            End Get
        End Property

        Public Property YearlyReportFromYearBegining() As Boolean
            Set(ByVal value As Boolean)
                _YearlyReportFromYearBegining = value
            End Set
            Get
                Return (_YearlyReportFromYearBegining)
            End Get
        End Property

        Public Property IncludeNotifications_CoordinatorTypes() As Boolean
            Set(ByVal value As Boolean)
                _IncludeNotifications_CoordinatorTypes = value
            End Set
            Get
                Return (_IncludeNotifications_CoordinatorTypes)
            End Get
        End Property

        Public Property EmployeeAbsentWeekly() As Integer
            Set(ByVal value As Integer)
                _EmployeeAbsentWeekly = value
            End Set
            Get
                Return (_EmployeeAbsentWeekly)
            End Get
        End Property

        Public Property EmployeeAbsentMonthly() As Integer
            Set(ByVal value As Integer)
                _EmployeeAbsentMonthly = value
            End Set
            Get
                Return (_EmployeeAbsentMonthly)
            End Get
        End Property

        Public Property EmployeeDetailedAbsentWeekly() As Integer
            Set(ByVal value As Integer)
                _EmployeeDetailedAbsentWeekly = value
            End Set
            Get
                Return (_EmployeeDetailedAbsentWeekly)
            End Get
        End Property

        Public Property EmployeeDetailedAbsentMonthly() As Integer
            Set(ByVal value As Integer)
                _EmployeeDetailedAbsentMonthly = value
            End Set
            Get
                Return (_EmployeeDetailedAbsentMonthly)
            End Get
        End Property

        Public Property EmployeeLostTimeDetailsWeekly() As Integer
            Set(ByVal value As Integer)
                _EmployeeLostTimeDetailsWeekly = value
            End Set
            Get
                Return (_EmployeeLostTimeDetailsWeekly)
            End Get
        End Property

        Public Property EmployeeLostTimeDetailsMonthly() As Integer
            Set(ByVal value As Integer)
                _EmployeeLostTimeDetailsMonthly = value
            End Set
            Get
                Return (_EmployeeLostTimeDetailsMonthly)
            End Get
        End Property

        Public Property MgrEmpSummaryAttendanceWeekly() As Integer
            Set(ByVal value As Integer)
                _MgrEmpSummaryAttendanceWeekly = value
            End Set
            Get
                Return (_MgrEmpSummaryAttendanceWeekly)
            End Get
        End Property

        Public Property MgrEmpSummaryAttendanceMonthly() As Integer
            Set(ByVal value As Integer)
                _MgrEmpSummaryAttendanceMonthly = value
            End Set
            Get
                Return (_MgrEmpSummaryAttendanceMonthly)
            End Get
        End Property

        Public Property EnableSemesterSelection_StudyPermission() As Boolean
            Set(ByVal value As Boolean)
                _EnableSemesterSelection_StudyPermission = value
            End Set
            Get
                Return (_EnableSemesterSelection_StudyPermission)
            End Get
        End Property

        Public Property MustCompleteNoHours_RequestPermission() As Boolean
            Set(ByVal value As Boolean)
                _MustCompleteNoHours_RequestPermission = value
            End Set
            Get
                Return (_MustCompleteNoHours_RequestPermission)
            End Get
        End Property

        Public Property NoOfHours_RequestPermission() As Integer
            Set(ByVal value As Integer)
                _NoOfHours_RequestPermission = value
            End Set
            Get
                Return (_NoOfHours_RequestPermission)
            End Get
        End Property

        Public Property IncludeConsiderInWorkPermissions_RequestPermission() As Boolean
            Set(ByVal value As Boolean)
                _IncludeConsiderInWorkPermissions_RequestPermission = value
            End Set
            Get
                Return (_IncludeConsiderInWorkPermissions_RequestPermission)
            End Get
        End Property

        Public Property MgrEmpDeductionWeekly() As Integer
            Set(ByVal value As Integer)
                _MgrEmpDeductionWeekly = value
            End Set
            Get
                Return (_MgrEmpDeductionWeekly)
            End Get
        End Property

        Public Property MgrEmpDeductionMonthly() As Integer
            Set(ByVal value As Integer)
                _MgrEmpDeductionMonthly = value
            End Set
            Get
                Return (_MgrEmpDeductionMonthly)
            End Get
        End Property

        Public Property HrEmpDeductionWeekly() As Integer
            Set(ByVal value As Integer)
                _HrEmpDeductionWeekly = value
            End Set
            Get
                Return (_HrEmpDeductionWeekly)
            End Get
        End Property

        Public Property HrEmpDeductionMonthly() As Integer
            Set(ByVal value As Integer)
                _HrEmpDeductionMonthly = value
            End Set
            Get
                Return (_HrEmpDeductionMonthly)
            End Get
        End Property

        Public Property EmployeeEmpDeductionWeekly() As Integer
            Set(ByVal value As Integer)
                _EmployeeEmpDeductionWeekly = value
            End Set
            Get
                Return (_EmployeeEmpDeductionWeekly)
            End Get
        End Property

        Public Property EmployeeEmpDeductionMonthly() As Integer
            Set(ByVal value As Integer)
                _EmployeeEmpDeductionMonthly = value
            End Set
            Get
                Return (_EmployeeEmpDeductionMonthly)
            End Get
        End Property

        Public Property EntityManagerEmpDeductionWeekly() As Integer
            Set(ByVal value As Integer)
                _EntityManagerEmpDeductionWeekly = value
            End Set
            Get
                Return (_EntityManagerEmpDeductionWeekly)
            End Get
        End Property

        Public Property EntityManagerEmpDeductionMonthly() As Integer
            Set(ByVal value As Integer)
                _EntityManagerEmpDeductionMonthly = value
            End Set
            Get
                Return (_EntityManagerEmpDeductionMonthly)
            End Get
        End Property

        Public Property DeductionEmailFormat() As String
            Set(ByVal value As String)
                _DeductionEmailFormat = value
            End Set
            Get
                Return (_DeductionEmailFormat)
            End Get
        End Property

        Public Property MonthlyDeduction_Report() As Integer
            Set(ByVal value As Integer)
                _MonthlyDeduction_Report = value
            End Set
            Get
                Return (_MonthlyDeduction_Report)
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

        Public Property PasswordType() As String
            Set(ByVal value As String)
                _PasswordType = value
            End Set
            Get
                Return (_PasswordType)
            End Get
        End Property

        Public Property ShowLeaveLnk_ViolationCorrection() As Boolean
            Set(ByVal value As Boolean)
                _ShowLeaveLnk_ViolationCorrection = value
            End Set
            Get
                Return (_ShowLeaveLnk_ViolationCorrection)
            End Get
        End Property

        Public Property ShowPermissionLnk_ViolationCorrection() As Boolean
            Set(ByVal value As Boolean)
                _ShowPermissionLnk_ViolationCorrection = value
            End Set
            Get
                Return (_ShowPermissionLnk_ViolationCorrection)
            End Get
        End Property

        Public Property ConsiderLeaveOnOffDay() As Integer
            Set(ByVal value As Integer)
                _ConsiderLeaveOnOffDay = value
            End Set
            Get
                Return (_ConsiderLeaveOnOffDay)
            End Get
        End Property

        Public Property ViolationJustificationDays() As String
            Set(ByVal value As String)
                _ViolationJustificationDays = value
            End Set
            Get
                Return (_ViolationJustificationDays)
            End Get
        End Property

        Public Property ViolationJustificationDaysPolicy() As String
            Set(ByVal value As String)
                _ViolationJustificationDaysPolicy = value
            End Set
            Get
                Return (_ViolationJustificationDaysPolicy)
            End Get
        End Property

        Public Property ManualEntryNoPerMonth() As String
            Set(ByVal value As String)
                _ManualEntryNoPerMonth = value
            End Set
            Get
                Return (_ManualEntryNoPerMonth)
            End Get
        End Property

        Public Property EmployeeSummaryWeekly() As Integer
            Set(ByVal value As Integer)
                _EmployeeSummaryWeekly = value
            End Set
            Get
                Return (_EmployeeSummaryWeekly)
            End Get
        End Property

        Public Property EmployeeSummaryMonthly() As Integer
            Set(ByVal value As Integer)
                _EmployeeSummaryMonthly = value
            End Set
            Get
                Return (_EmployeeSummaryMonthly)
            End Get
        End Property

        Public Property EntityManagerMaxAbsentMonthly() As Integer
            Set(ByVal value As Integer)
                _EntityManagerMaxAbsentMonthly = value
            End Set
            Get
                Return (_EntityManagerMaxAbsentMonthly)
            End Get
        End Property

        Public Property EntityManagerMaxAbsentWeekly() As Integer
            Set(ByVal value As Integer)
                _EntityManagerMaxAbsentWeekly = value
            End Set
            Get
                Return (_EntityManagerMaxAbsentWeekly)
            End Get
        End Property

        Public Property EntityManagerMaxDelayMonthly() As Integer
            Set(ByVal value As Integer)
                _EntityManagerMaxDelayMonthly = value
            End Set
            Get
                Return (_EntityManagerMaxDelayMonthly)
            End Get
        End Property

        Public Property EntityManagerMaxDelayWeekly() As Integer
            Set(ByVal value As Integer)
                _EntityManagerMaxDelayWeekly = value
            End Set
            Get
                Return (_EntityManagerMaxDelayWeekly)
            End Get
        End Property

        Public Property SendDaily_AbsentDelay_EntityMgr() As Boolean
            Set(ByVal value As Boolean)
                _SendDaily_AbsentDelay_EntityMgr = value
            End Set
            Get
                Return (_SendDaily_AbsentDelay_EntityMgr)
            End Get
        End Property

        Public Property IsAbsentRestPolicy() As Boolean
            Set(ByVal value As Boolean)
                _IsAbsentRestPolicy = value
            End Set
            Get
                Return (_IsAbsentRestPolicy)
            End Get
        End Property

        Public Property MgrDeductionPerPolicyMonthly() As Integer
            Set(ByVal value As Integer)
                _MgrDeductionPerPolicyMonthly = value
            End Set
            Get
                Return (_MgrDeductionPerPolicyMonthly)
            End Get
        End Property

        Public Property MgrDeductionPerPolicyWeekly() As Integer
            Set(ByVal value As Integer)
                _MgrDeductionPerPolicyWeekly = value
            End Set
            Get
                Return (_MgrDeductionPerPolicyWeekly)
            End Get
        End Property

        Public Property HRDeductionPerPolicyMonthly() As Integer
            Set(ByVal value As Integer)
                _HRDeductionPerPolicyMonthly = value
            End Set
            Get
                Return (_HRDeductionPerPolicyMonthly)
            End Get
        End Property

        Public Property HRDeductionPerPolicyWeekly() As Integer
            Set(ByVal value As Integer)
                _HRDeductionPerPolicyWeekly = value
            End Set
            Get
                Return (_HRDeductionPerPolicyWeekly)
            End Get
        End Property

        Public Property EmployeeDeductionPerPolicyMonthly() As Integer
            Set(ByVal value As Integer)
                _EmployeeDeductionPerPolicyMonthly = value
            End Set
            Get
                Return (_EmployeeDeductionPerPolicyMonthly)
            End Get
        End Property

        Public Property EmployeeDeductionPerPolicyWeekly() As Integer
            Set(ByVal value As Integer)
                _EmployeeDeductionPerPolicyWeekly = value
            End Set
            Get
                Return (_EmployeeDeductionPerPolicyWeekly)
            End Get
        End Property

        Public Property EntityManagerDeductionPerPolicyMonthly() As Integer
            Set(ByVal value As Integer)
                _EntityManagerDeductionPerPolicyMonthly = value
            End Set
            Get
                Return (_EntityManagerDeductionPerPolicyMonthly)
            End Get
        End Property

        Public Property EntityManagerDeductionPerPolicyWeekly() As Integer
            Set(ByVal value As Integer)
                _EntityManagerDeductionPerPolicyWeekly = value
            End Set
            Get
                Return (_EntityManagerDeductionPerPolicyWeekly)
            End Get
        End Property

        Public Property DeductionPerPolicy_Value() As Integer
            Set(ByVal value As Integer)
                _DeductionPerPolicy_Value = value
            End Set
            Get
                Return (_DeductionPerPolicy_Value)
            End Get
        End Property

        Public Property AllowEditManualEntryRequestDate() As Boolean
            Set(ByVal value As Boolean)
                _AllowEditManualEntryRequestDate = value
            End Set
            Get
                Return (_AllowEditManualEntryRequestDate)
            End Get
        End Property

        Public Property AllowEditManualEntryRequestTime() As Boolean
            Set(ByVal value As Boolean)
                _AllowEditManualEntryRequestTime = value
            End Set
            Get
                Return (_AllowEditManualEntryRequestTime)
            End Get
        End Property

        Public Property NumberInTransactionRequests() As String
            Set(ByVal value As String)
                _NumberInTransactionRequests = value
            End Set
            Get
                Return (_NumberInTransactionRequests)
            End Get
        End Property

        Public Property NumberOutTransactionRequests() As String
            Set(ByVal value As String)
                _NumberOutTransactionRequests = value
            End Set
            Get
                Return (_NumberOutTransactionRequests)
            End Get
        End Property

        Public Property IsAutoApproveManualEntryRequest() As Boolean
            Set(ByVal value As Boolean)
                _IsAutoApproveManualEntryRequest = value
            End Set
            Get
                Return (_IsAutoApproveManualEntryRequest)
            End Get
        End Property

        Public Property ConsiderRestInshiftSch() As Integer
            Set(ByVal value As Integer)
                _ConsiderRestInshiftSch = value
            End Set
            Get
                Return (_ConsiderRestInshiftSch)
            End Get
        End Property

        Public Property ConsiderNursingInRamadan() As Boolean
            Set(ByVal value As Boolean)
                _ConsiderNursingInRamadan = value
            End Set
            Get
                Return (_ConsiderNursingInRamadan)
            End Get
        End Property

        Public Property RemoteWorkTAReason() As String
            Set(ByVal value As String)
                _RemoteWorkTAReason = value
            End Set
            Get
                Return (_RemoteWorkTAReason)
            End Get
        End Property

        Public Property HasTawajudFeatures() As Boolean
            Set(ByVal value As Boolean)
                _HasTawajudFeatures = value
            End Set
            Get
                Return (_HasTawajudFeatures)
            End Get
        End Property

        Public Property HasMultiLocations() As Boolean
            Set(ByVal value As Boolean)
                _HasMultiLocations = value
            End Set
            Get
                Return (_HasMultiLocations)
            End Get
        End Property

        Public Property HasHeartBeat() As Boolean
            Set(ByVal value As Boolean)
                _HasHeartBeat = value
            End Set
            Get
                Return (_HasHeartBeat)
            End Get
        End Property

        Public Property HasFeedback() As Boolean
            Set(ByVal value As Boolean)
                _HasFeedback = value
            End Set
            Get
                Return (_HasFeedback)
            End Get
        End Property

        Public Property AllowFingerPunch() As Boolean
            Set(ByVal value As Boolean)
                _AllowFingerPunch = value
            End Set
            Get
                Return (_AllowFingerPunch)
            End Get
        End Property

        Public Property AllowFingerLogin() As Boolean
            Set(ByVal value As Boolean)
                _AllowFingerLogin = value
            End Set
            Get
                Return (_AllowFingerLogin)
            End Get
        End Property

        Public Property AllowFacePunch() As Boolean
            Set(ByVal value As Boolean)
                _AllowFacePunch = value
            End Set
            Get
                Return (_AllowFacePunch)
            End Get
        End Property

        Public Property AllowFaceLogin() As Boolean
            Set(ByVal value As Boolean)
                _AllowFaceLogin = value
            End Set
            Get
                Return (_AllowFaceLogin)
            End Get
        End Property

        Public Property EnableUniversitySelection_StudyPermission() As Boolean
            Set(ByVal value As Boolean)
                _EnableUniversitySelection_StudyPermission = value
            End Set
            Get
                Return (_EnableUniversitySelection_StudyPermission)
            End Get
        End Property

        Public Property StudyAllowedAfterDays() As Integer?
            Set(ByVal value As Integer?)
                _StudyAllowedAfterDays = value
            End Set
            Get
                Return (_StudyAllowedAfterDays)
            End Get
        End Property

        Public Property StudyAllowedBeforeDays() As Integer?
            Set(ByVal value As Integer?)
                _StudyAllowedBeforeDays = value
            End Set
            Get
                Return (_StudyAllowedBeforeDays)
            End Get
        End Property

        Public Property EnableMajorSelection_StudyPermission() As Boolean?
            Set(ByVal value As Boolean?)
                _EnableMajorSelection_StudyPermission = value
            End Set
            Get
                Return (_EnableMajorSelection_StudyPermission)
            End Get
        End Property


#End Region

#Region "Constructor"
        Public Sub New()
            objDALAPP_Settings = New DALAPP_Settings()
            objDALApp_EventsLog = New DALApp_EventsLog
        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALAPP_Settings.Add(_CompanyName1, _CompanyName2, _companyArabicName1, _CompanyArabicName2, _LogoImage, _LogoPath,
                _EmployeeNoLength, _EmployeeCardLength, _IsGradeOvertimeRule, _IsGradeTAException, _AnnualLeaveOption,
                _MinGapbetweenMoves, _ConsequenceTransactions, _IsGraceTAPolicy, _LeaveApproval, _SystemUsersType,
                _HRGroupEmail, _HREmailNotification, _AllowEditOverTime, _ConsiderAbsentAfter, _LeaveApprovalfromLeave,
                _DynamicReportView, _SendToManagerDaily, _SendToManagerWeekly, _SendToManagerMonthly, _SendToHRDaily,
                _SendToHRWeekly, _SendToHRMonthly, _NoShiftShcedule, _NursingDays, _DaysMinutes, _HasFullDayPermission,
                _AllowChangeEmpNo, _AllowedBefore, _StudyPermissionSchedule, _NursingPermissionAttend, _PermApprovalFromPermission,
                _ManualEntryNo, _MinOutViolation, _ManagerEmailFormat, _HREmailFormat, _ManualEntryManagerLevelRequired, _HasEmailApproval,
                _ShowLoginForm, _ShowViolationCorrection, _FK_MaternityLeaveTypeId, _FK_PersonalPermissionTypeId, _NursingRequireMaternity,
                _RequestGridToAppear, _EarlyOutNotificationAfter, _PermissionRequestReminderAfter, _HasFlexiblePermission,
                _HasFlexibleNursingPermission, _ShowAbsentInViolationCorrection, _HasPermissionForPeriod, _IsToConsiderBalanceInHours,
                _ShowThemeToUsers, _DurationTotalsToAppear, _AttachmentIsMandatory, _IsFirstGrid,
                _DefaultStudyPermissionFromTime, _DefaultStudyPermissionToTime, _DefaultStudyPermissionFlexibleTime, _ShowAnnouncement, _DivideTwoPermission, _MaternityLeaveDuration,
                _StudyPermissionApproval, _NursingPermissionApproval, _MaxStudyPermission, _StudyGeneralGuide, _StudyGeneralGuideAr, _NursingGeneralGuide, _NursingGeneralGuideAr, _AllowMoreOneManualEntry, _ShowAnnouncementSelfService,
                _OnCallSchedule, _AllowNursingInRamadan, _ShowEmployeeList, _MgrAbsentWeekly, _MgrAbsentMonthly, _MgrViolationWeekly, _MgrViolationMonthly,
                _HRAbsentWeekly, _HRAbsentMonthly, _HRViolationWeekly, _HRViolationMonthly, _ConsiderAbsentOrLogicalAbsent, _ReminderAbsentAfter,
                _MgrDailyWeekly, _MgrDailyMonthly, _MgrDetailedWeekly, _MgrDetailedMonthly, _MgrSummaryWeekly, _MgrSummaryMonthly, _HRDailyWeekly, _HRDailyMonthly, _HRDetailedWeekly, _HRDetailedMonthly, _HRSummaryWeekly, _HRSummaryMonthly, _ConsiderAbsentEvenAttend,
                _DefaultReportFormat, _AutoPersonalPermissionDelay, _AutoPermissionDelayDuration, _AutoPersonalPermissionEarlyOut,
                _AutoPermissionEarlyOutDuration, _HasMultiApproval, _EmployeeManagerFilter, _FillCheckBoxList, _IsDailyReportWithColor,
                _AllowDeleteSchedule, _DefaultTheme, _ArchivingMonths, _ShowLGWithEntityPrivilege, _ManagerDefaultPage, _ShowDirectStaffChk, _ApprovalRecalMethod,
                _StudyPerm_NotificationException, _NursingPerm_NotificationException, _AttachmentIsMandatoryManualEntryRequest, _AttachmentIsMandatoryHRManualEntry,
                _DailyReportDate, _ExcludeGraceFromLostTime, _EnableSemesterSelection_StudyPermission, _MustCompleteNoHours_RequestPermission, _NoOfHours_RequestPermission, _IncludeConsiderInWorkPermissions_RequestPermission,
                _MonthlyDeduction_Report, _PasswordType, _ShowLeaveLnk_ViolationCorrection, _ShowPermissionLnk_ViolationCorrection, _ConsiderLeaveOnOffDay, _ViolationJustificationDays, _ViolationJustificationDaysPolicy,
                _ManualEntryNoPerMonth, _IsAbsentRestPolicy, _AllowEditManualEntryRequestDate, AllowEditManualEntryRequestTime, _NumberInTransactionRequests, _NumberOutTransactionRequests, _IsAutoApproveManualEntryRequest, _ConsiderRestInshiftSch,
                _ConsiderNursingInRamadan, _RemoteWorkTAReason, _HasTawajudFeatures, _HasMultiLocations, _HasHeartBeat, _HasFeedback, _AllowFingerPunch, _AllowFingerLogin, _AllowFacePunch, _AllowFaceLogin,
                _EnableUniversitySelection_StudyPermission, _StudyAllowedAfterDays, _StudyAllowedBeforeDays, _EnableMajorSelection_StudyPermission, _RemarkIsMandatoryManualEntryRequest)
            App_EventsLog.Insert_ToEventLog("Add", _CompanyName1, "App_Settings", "Application Settings")
            Return rslt
        End Function

        Public Function Update_Image() As Integer

            Return objDALAPP_Settings.Update_Image(_LogoPath, _LogoImage)

        End Function

        Public Function Update() As Integer
            Dim rslt As Integer = objDALAPP_Settings.Update(_CompanyName1, _CompanyName2, _companyArabicName1, _CompanyArabicName2, _LogoImage, _LogoPath, _EmployeeNoLength,
            _EmployeeCardLength, _IsGradeOvertimeRule, _IsGradeTAException, _AnnualLeaveOption, _MinGapbetweenMoves, _ManageOvertime,
            _ConsequenceTransactions, _HRGroupEmail, _HREmailNotification, _LeaveApprovalfromLeave, _DynamicReportView, _SendToManagerDaily,
            _SendToManagerWeekly, _SendToManagerMonthly, _SendToHRDaily, _SendToHRWeekly, _SendToHRMonthly, _NoShiftShcedule, _NursingDays, _DaysMinutes,
            _HasFullDayPermission, _AllowChangeEmpNo, _AllowedBefore, _StudyPermissionSchedule, _NursingPermissionAttend, _PermApprovalFromPermission, _ManualEntryNo,
            _MinOutViolation, _ManagerEmailFormat, _HREmailFormat, _ManualEntryManagerLevelRequired, _HasEmailApproval, _ShowLoginForm,
            _ShowViolationCorrection, _FK_MaternityLeaveTypeId, _FK_PersonalPermissionTypeId, _NursingRequireMaternity, _RequestGridToAppear,
            _EarlyOutNotificationAfter, _PermissionRequestReminderAfter, _HasFlexiblePermission, _HasFlexibleNursingPermission,
            _ShowAbsentInViolationCorrection, _HasPermissionForPeriod, _IsToConsiderBalanceInHours, _ShowThemeToUsers,
            _DurationTotalsToAppear, _AttachmentIsMandatory, _IsFirstGrid,
            _DefaultStudyPermissionFromTime, _DefaultStudyPermissionToTime, _DefaultStudyPermissionFlexibleTime, _ShowAnnouncement, _DivideTwoPermission, _MaternityLeaveDuration,
            _StudyPermissionApproval, _NursingPermissionApproval, _MaxStudyPermission, _StudyGeneralGuide, _StudyGeneralGuideAr, _NursingGeneralGuide, _NursingGeneralGuideAr, _AllowMoreOneManualEntry, _ShowAnnouncementSelfService,
            _OnCallSchedule, _AllowNursingInRamadan, _ShowEmployeeList, _MgrAbsentWeekly, _MgrAbsentMonthly, _MgrViolationWeekly, _MgrViolationMonthly,
            _HRAbsentWeekly, _HRAbsentMonthly, _HRViolationWeekly, _HRViolationMonthly, _ConsiderAbsentOrLogicalAbsent, _ReminderAbsentAfter,
            _MgrDailyWeekly, _MgrDailyMonthly, _MgrDetailedWeekly, _MgrDetailedMonthly, _MgrSummaryWeekly, _MgrSummaryMonthly, _HRDailyWeekly, _HRDailyMonthly, _HRDetailedWeekly, _HRDetailedMonthly, _HRSummaryWeekly, _HRSummaryMonthly, _ConsiderAbsentEvenAttend,
            _DefaultReportFormat, _AutoPersonalPermissionDelay, _AutoPermissionDelayDuration, _AutoPersonalPermissionEarlyOut, _AutoPermissionEarlyOutDuration,
            _HasMultiApproval, _EmployeeManagerFilter, _FillCheckBoxList, _IsDailyReportWithColor, _AllowDeleteSchedule,
            _DefaultTheme, _ArchivingMonths, _ShowLGWithEntityPrivilege, _ManagerDefaultPage, _ShowDirectStaffChk, _ApprovalRecalMethod,
            _StudyPerm_NotificationException, _NursingPerm_NotificationException, _AttachmentIsMandatoryManualEntryRequest, _AttachmentIsMandatoryHRManualEntry,
            _DailyReportDate, _ExcludeGraceFromLostTime, _EnableSemesterSelection_StudyPermission, _MustCompleteNoHours_RequestPermission, _NoOfHours_RequestPermission, _IncludeConsiderInWorkPermissions_RequestPermission,
            _MonthlyDeduction_Report, _PasswordType, _ShowLeaveLnk_ViolationCorrection, _ShowPermissionLnk_ViolationCorrection, _ConsiderLeaveOnOffDay, _ViolationJustificationDays, _ViolationJustificationDaysPolicy,
            _ManualEntryNoPerMonth, _IsAbsentRestPolicy, _AllowEditManualEntryRequestDate, AllowEditManualEntryRequestTime, _NumberInTransactionRequests, _NumberOutTransactionRequests, _IsAutoApproveManualEntryRequest, _ConsiderRestInshiftSch,
            _ConsiderNursingInRamadan, _RemoteWorkTAReason, _HasTawajudFeatures, _HasMultiLocations, _HasHeartBeat, _HasFeedback, _AllowFingerPunch, _AllowFingerLogin, _AllowFacePunch, _AllowFaceLogin,
            _EnableUniversitySelection_StudyPermission, _StudyAllowedAfterDays, _StudyAllowedBeforeDays, _EnableMajorSelection_StudyPermission, _RemarkIsMandatoryManualEntryRequest)
            App_EventsLog.Insert_ToEventLog("Edit", _CompanyName1, "App_Settings", "Application Settings")
            Return rslt
        End Function

        Public Function _EmailConfigration_Add() As Integer

            Dim rslt As Integer = objDALAPP_Settings.EmailConfigration_Add(_CompanyName1, _CompanyName2, _companyArabicName1, _CompanyArabicName2, _LogoImage, _LogoPath,
                _EmployeeNoLength, _EmployeeCardLength, _IsGradeOvertimeRule, _IsGradeTAException, _AnnualLeaveOption,
                _MinGapbetweenMoves, _ConsequenceTransactions, _IsGraceTAPolicy, _LeaveApproval, _SystemUsersType,
                _HRGroupEmail, _HREmailNotification, _AllowEditOverTime, _ConsiderAbsentAfter, _LeaveApprovalfromLeave,
                _DynamicReportView, _SendToManagerDaily, _SendToManagerWeekly, _SendToManagerMonthly, _SendToHRDaily,
                _SendToHRWeekly, _SendToHRMonthly, _NoShiftShcedule, _NursingDays, _DaysMinutes, _HasFullDayPermission,
                _AllowChangeEmpNo, _AllowedBefore, _StudyPermissionSchedule, _NursingPermissionAttend, _PermApprovalFromPermission,
                _ManualEntryNo, _MinOutViolation, _ManagerEmailFormat, _HREmailFormat, _ManualEntryManagerLevelRequired, _HasEmailApproval,
                _ShowLoginForm, _ShowViolationCorrection, _FK_MaternityLeaveTypeId, _FK_PersonalPermissionTypeId, _NursingRequireMaternity,
                _RequestGridToAppear, _EarlyOutNotificationAfter, _PermissionRequestReminderAfter, _HasFlexiblePermission,
                _HasFlexibleNursingPermission, _ShowAbsentInViolationCorrection, _HasPermissionForPeriod, _IsToConsiderBalanceInHours,
                _ShowThemeToUsers, _DurationTotalsToAppear, _AttachmentIsMandatory, _IsFirstGrid,
                _DefaultStudyPermissionFromTime, _DefaultStudyPermissionToTime, _DefaultStudyPermissionFlexibleTime, _ShowAnnouncement, _DivideTwoPermission, _MaternityLeaveDuration,
                _StudyPermissionApproval, _NursingPermissionApproval, _MaxStudyPermission, _StudyGeneralGuide, _StudyGeneralGuideAr, _NursingGeneralGuide, _NursingGeneralGuideAr, _AllowMoreOneManualEntry, _ShowAnnouncementSelfService,
                _OnCallSchedule, _AllowNursingInRamadan, _ShowEmployeeList, _MgrAbsentWeekly, _MgrAbsentMonthly, _MgrViolationWeekly, _MgrViolationMonthly,
                _HRAbsentWeekly, _HRAbsentMonthly, _HRViolationWeekly, _HRViolationMonthly, _ConsiderAbsentOrLogicalAbsent, _ReminderAbsentAfter,
                _MgrDailyWeekly, _MgrDailyMonthly, _MgrDetailedWeekly, _MgrDetailedMonthly, _MgrSummaryWeekly, _MgrSummaryMonthly, _HRDailyWeekly, _HRDailyMonthly, _HRDetailedWeekly,
                _HRDetailedMonthly, _HRSummaryWeekly, _HRSummaryMonthly, _ConsiderAbsentEvenAttend, _DefaultReportFormat, _AutoPersonalPermissionDelay, _AutoPermissionDelayDuration,
                _AutoPersonalPermissionEarlyOut, _AutoPermissionEarlyOutDuration, _HasMultiApproval, _EmployeeManagerFilter, _FillCheckBoxList, _IsDailyReportWithColor,
                _IncompleteWorkingHrs, _SendToEntityManagerWeekly, _SendToEntityManagerMonthly, _SendToEmployeeWeekly, _SendToEmployeeMonthly, _EntityManagerViolationWeekly, _EntityManagerViolationMonthly, _EmployeeViolationWeekly,
                _EmployeeViolationMonthly, _YearlyReportFromYearBegining, _IncludeNotifications_CoordinatorTypes, _EmployeeAbsentWeekly, _EmployeeAbsentMonthly, _EmployeeDetailedAbsentWeekly, _EmployeeDetailedAbsentMonthly,
                _EmployeeLostTimeDetailsWeekly, _EmployeeLostTimeDetailsMonthly, _MgrEmpSummaryAttendanceWeekly, _MgrEmpSummaryAttendanceMonthly, _MgrEmpDeductionWeekly, _MgrEmpDeductionMonthly, _HrEmpDeductionWeekly, _HrEmpDeductionMonthly,
                _EmployeeEmpDeductionWeekly, _EmployeeEmpDeductionMonthly, _EntityManagerEmpDeductionWeekly, _EntityManagerEmpDeductionMonthly, _DeductionEmailFormat, _EmployeeSummaryWeekly, _EmployeeSummaryMonthly, _EntityManagerMaxAbsentMonthly, _EntityManagerMaxAbsentWeekly,
                _EntityManagerMaxDelayMonthly, _EntityManagerMaxDelayWeekly, _SendDaily_AbsentDelay_EntityMgr, _MgrDeductionPerPolicyMonthly, _MgrDeductionPerPolicyWeekly, _HRDeductionPerPolicyMonthly,
                _HRDeductionPerPolicyWeekly, _EmployeeDeductionPerPolicyMonthly, _EmployeeDeductionPerPolicyWeekly, _EntityManagerDeductionPerPolicyMonthly, _EntityManagerDeductionPerPolicyWeekly, _DeductionPerPolicy_Value, _RemarkIsMandatoryManualEntryRequest)
            App_EventsLog.Insert_ToEventLog("Add Email Confiration", _CompanyName1, "App_Settings", "Application Settings")
            Return rslt
        End Function

        Public Function Delete() As Integer
            Dim rslt As Integer = objDALAPP_Settings.Delete(_CompanyName1)
            App_EventsLog.Insert_ToEventLog("Delete", _CompanyName1, "App_Settings", "Application Settings")
            Return rslt

        End Function

        Public Function GetAll() As DataTable
            Return objDALAPP_Settings.GetAll()
        End Function

        Public Function GetHeader() As DataTable
            Return objDALAPP_Settings.GetHeader(_FK_CompanyId)
        End Function

        Public Function GetByPK() As APP_Settings
            Dim dr As DataRow
            dr = objDALAPP_Settings.GetByPK()
            If (dr IsNot Nothing) Then
                If Not IsDBNull(dr("CompanyName1")) Then
                    _CompanyName1 = dr("CompanyName1")
                End If
                If Not IsDBNull(dr("CompanyName2")) Then
                    _CompanyName2 = dr("CompanyName2")
                End If
                If Not IsDBNull(dr("companyArabicName1")) Then
                    _companyArabicName1 = dr("companyArabicName1")
                End If
                If Not IsDBNull(dr("CompanyArabicName2")) Then
                    _CompanyArabicName2 = dr("CompanyArabicName2")
                End If
                If Not IsDBNull(dr("LogoImage")) Then
                    _LogoImage = dr("LogoImage")
                End If
                If Not IsDBNull(dr("LogoPath")) Then
                    _LogoPath = dr("LogoPath")
                End If
                If Not IsDBNull(dr("EmployeeNoLength")) Then
                    _EmployeeNoLength = dr("EmployeeNoLength")
                End If
                If Not IsDBNull(dr("EmployeeCardLength")) Then
                    _EmployeeCardLength = dr("EmployeeCardLength")
                End If
                If Not IsDBNull(dr("IsGradeOvertimeRule")) Then
                    _IsGradeOvertimeRule = dr("IsGradeOvertimeRule")
                End If
                If Not IsDBNull(dr("IsGradeTAException")) Then
                    _IsGradeTAException = dr("IsGradeTAException")
                End If
                If Not IsDBNull(dr("AnnualLeaveOption")) Then
                    _AnnualLeaveOption = dr("AnnualLeaveOption")
                End If
                If Not IsDBNull(dr("IsGraceTAPolicy")) Then
                    _IsGraceTAPolicy = dr("IsGraceTAPolicy")
                End If
                If Not IsDBNull(dr("MinGapbetweenMoves")) Then
                    _MinGapbetweenMoves = dr("MinGapbetweenMoves")
                End If
                If Not IsDBNull(dr("ManageOvertime")) Then
                    _ManageOvertime = dr("ManageOvertime")
                End If
                If Not IsDBNull(dr("ConsequenceTransactions")) Then
                    _ConsequenceTransactions = dr("ConsequenceTransactions")
                End If
                If Not IsDBNull(dr("CREATED_DATE")) Then
                    _CREATED_DATE = dr("CREATED_DATE")
                End If
                If Not IsDBNull(dr("LeaveApproval")) Then
                    _LeaveApproval = dr("LeaveApproval")
                End If
                If Not IsDBNull(dr("SystemUsersType")) Then
                    _SystemUsersType = dr("SystemUsersType")
                End If
                If Not IsDBNull(dr("HRGroupEmail")) Then
                    _HRGroupEmail = dr("HRGroupEmail")
                End If
                If Not IsDBNull(dr("HREmailNotification")) Then
                    _HREmailNotification = dr("HREmailNotification")
                End If
                If Not IsDBNull(dr("AllowEditOverTime")) Then
                    _AllowEditOverTime = dr("AllowEditOverTime")
                End If
                If Not IsDBNull(dr("ConsiderAbsentAfter")) Then
                    _ConsiderAbsentAfter = dr("ConsiderAbsentAfter")
                End If
                If Not IsDBNull(dr("leaveApprovalfromLeave")) Then
                    _LeaveApprovalfromLeave = dr("leaveApprovalfromLeave")
                End If
                If Not IsDBNull(dr("DynamicReportView")) Then
                    _DynamicReportView = dr("DynamicReportView")
                End If
                If Not IsDBNull(dr("SendToManagerDaily")) Then
                    _SendToManagerDaily = dr("SendToManagerDaily")
                End If
                If Not IsDBNull(dr("SendToManagerWeekly")) Then
                    _SendToManagerWeekly = dr("SendToManagerWeekly")
                End If
                If Not IsDBNull(dr("SendToManagerMonthly")) Then
                    _SendToManagerMonthly = dr("SendToManagerMonthly")
                End If
                If Not IsDBNull(dr("SendToHRDaily")) Then
                    _SendToHRDaily = dr("SendToHRDaily")
                End If
                If Not IsDBNull(dr("SendToHRWeekly")) Then
                    _SendToHRWeekly = dr("SendToHRWeekly")
                End If
                If Not IsDBNull(dr("SendToHRMonthly")) Then
                    _SendToHRMonthly = dr("SendToHRMonthly")
                End If
                If Not IsDBNull(dr("NoShiftShcedule")) Then
                    _NoShiftShcedule = dr("NoShiftShcedule")
                End If
                If Not IsDBNull(dr("NursingDays")) Then
                    _NursingDays = dr("NursingDays")
                End If
                If Not IsDBNull(dr("DaysMinutes")) Then
                    _DaysMinutes = dr("DaysMinutes")
                End If
                If Not IsDBNull(dr("HasFullDayPermission")) Then
                    _HasFullDayPermission = dr("HasFullDayPermission")
                End If
                If Not IsDBNull(dr("AllowChangeEmpNo")) Then
                    _AllowChangeEmpNo = dr("AllowChangeEmpNo")
                End If
                If Not IsDBNull(dr("AllowedBefore")) Then
                    _AllowedBefore = dr("AllowedBefore")
                End If
                If Not IsDBNull(dr("StudyPermissionSchedule")) Then
                    _StudyPermissionSchedule = dr("StudyPermissionSchedule")
                End If
                If Not IsDBNull(dr("NursingPermissionAttend")) Then
                    _NursingPermissionAttend = dr("NursingPermissionAttend")
                End If
                If Not IsDBNull(dr("PermApprovalFromPermission")) Then
                    _PermApprovalFromPermission = dr("PermApprovalFromPermission")
                End If
                If Not IsDBNull(dr("ManualEntryNo")) Then
                    _ManualEntryNo = dr("ManualEntryNo")
                End If
                If Not IsDBNull(dr("MinOutViolation")) Then
                    _MinOutViolation = dr("MinOutViolation")
                End If
                If Not IsDBNull(dr("ManagerEmailFormat")) Then
                    _ManagerEmailFormat = dr("ManagerEmailFormat")
                End If
                If Not IsDBNull(dr("HREmailFormat")) Then
                    _HREmailFormat = dr("HREmailFormat")
                End If
                If Not IsDBNull(dr("ManualEntryManagerLevelRequired")) Then
                    _ManualEntryManagerLevelRequired = dr("ManualEntryManagerLevelRequired")
                End If
                If Not IsDBNull(dr("HasEmailApproval")) Then
                    _HasEmailApproval = dr("HasEmailApproval")
                End If
                If Not IsDBNull(dr("ShowLoginForm")) Then
                    _ShowLoginForm = dr("ShowLoginForm")
                End If
                If Not IsDBNull(dr("ShowViolationCorrection")) Then
                    _ShowViolationCorrection = dr("ShowViolationCorrection")
                End If
                If Not IsDBNull(dr("FK_MaternityLeaveTypeId")) Then
                    _FK_MaternityLeaveTypeId = dr("FK_MaternityLeaveTypeId")
                End If
                If Not IsDBNull(dr("FK_PersonalPermissionTypeId")) Then
                    _FK_PersonalPermissionTypeId = dr("FK_PersonalPermissionTypeId")
                End If
                If Not IsDBNull(dr("NursingRequireMaternity")) Then
                    _NursingRequireMaternity = dr("NursingRequireMaternity")
                End If
                If Not IsDBNull(dr("RequestGridToAppear")) Then
                    _RequestGridToAppear = dr("RequestGridToAppear")
                End If
                If Not IsDBNull(dr("EarlyOutNotificationAfter")) Then
                    _EarlyOutNotificationAfter = dr("EarlyOutNotificationAfter")
                End If
                If Not IsDBNull(dr("PermissionRequestReminderAfter")) Then
                    _PermissionRequestReminderAfter = dr("PermissionRequestReminderAfter")
                End If
                If Not IsDBNull(dr("HasFlexiblePermission")) Then
                    _HasFlexiblePermission = dr("HasFlexiblePermission")
                End If
                If Not IsDBNull(dr("HasFlexibleNursingPermission")) Then
                    _HasFlexibleNursingPermission = dr("HasFlexibleNursingPermission")
                End If
                If Not IsDBNull(dr("ShowAbsentInViolationCorrection")) Then
                    _ShowAbsentInViolationCorrection = dr("ShowAbsentInViolationCorrection")
                End If
                If Not IsDBNull(dr("HasPermissionForPeriod")) Then
                    _HasPermissionForPeriod = dr("HasPermissionForPeriod")
                End If
                If Not IsDBNull(dr("IsToConsiderBalanceInHours")) Then
                    _IsToConsiderBalanceInHours = dr("IsToConsiderBalanceInHours")
                End If
                If Not IsDBNull(dr("ShowThemeToUsers")) Then
                    _ShowThemeToUsers = dr("ShowThemeToUsers")
                End If
                If Not IsDBNull(dr("DurationTotalsToAppear")) Then
                    _DurationTotalsToAppear = dr("DurationTotalsToAppear")
                End If
                If Not IsDBNull(dr("AttachmentIsMandatory")) Then
                    _AttachmentIsMandatory = dr("AttachmentIsMandatory")
                End If
                If Not IsDBNull(dr("IsFirstGrid")) Then
                    _IsFirstGrid = dr("IsFirstGrid")
                End If

                If Not IsDBNull(dr("DefaultStudyPermissionFromTime")) Then
                    _DefaultStudyPermissionFromTime = dr("DefaultStudyPermissionFromTime")
                End If
                If Not IsDBNull(dr("DefaultStudyPermissionToTime")) Then
                    _DefaultStudyPermissionToTime = dr("DefaultStudyPermissionToTime")
                End If
                If Not IsDBNull(dr("DefaultStudyPermissionFlexibleTime")) Then
                    _DefaultStudyPermissionFlexibleTime = dr("DefaultStudyPermissionFlexibleTime")
                End If
                If Not IsDBNull(dr("ShowAnnouncement")) Then
                    _ShowAnnouncement = dr("ShowAnnouncement")
                End If
                If Not IsDBNull(dr("DivideTwoPermission")) Then
                    _DivideTwoPermission = dr("DivideTwoPermission")
                End If
                If Not IsDBNull(dr("MaternityLeaveDuration")) Then
                    _MaternityLeaveDuration = dr("MaternityLeaveDuration")
                End If
                If Not IsDBNull(dr("StudyPermissionApproval")) Then
                    _StudyPermissionApproval = dr("StudyPermissionApproval")
                End If
                If Not IsDBNull(dr("NursingPermissionApproval")) Then
                    _NursingPermissionApproval = dr("NursingPermissionApproval")
                End If

                If Not IsDBNull(dr("MaxStudyPermission")) Then
                    _MaxStudyPermission = dr("MaxStudyPermission")
                End If
                If Not IsDBNull(dr("StudyGeneralGuide")) Then
                    _StudyGeneralGuide = dr("StudyGeneralGuide")
                End If
                If Not IsDBNull(dr("StudyGeneralGuideAr")) Then
                    _StudyGeneralGuideAr = dr("StudyGeneralGuideAr")
                End If
                If Not IsDBNull(dr("NursingGeneralGuide")) Then
                    _NursingGeneralGuide = dr("NursingGeneralGuide")
                End If
                If Not IsDBNull(dr("NursingGeneralGuideAr")) Then
                    _NursingGeneralGuideAr = dr("NursingGeneralGuideAr")
                End If
                If Not IsDBNull(dr("AllowMoreOneManualEntry")) Then
                    _AllowMoreOneManualEntry = dr("AllowMoreOneManualEntry")
                End If
                If Not IsDBNull(dr("ShowAnnouncementSelfService")) Then
                    _ShowAnnouncementSelfService = dr("ShowAnnouncementSelfService")
                End If
                If Not IsDBNull(dr("OnCallSchedule")) Then
                    _OnCallSchedule = dr("OnCallSchedule")
                End If
                If Not IsDBNull(dr("AllowNursingInRamadan")) Then
                    _AllowNursingInRamadan = dr("AllowNursingInRamadan")
                End If
                If Not IsDBNull(dr("ShowEmployeeList")) Then
                    _ShowEmployeeList = dr("ShowEmployeeList")
                End If

                If Not IsDBNull(dr("MgrAbsentWeekly")) Then
                    _MgrAbsentWeekly = dr("MgrAbsentWeekly")
                End If
                If Not IsDBNull(dr("MgrAbsentMonthly")) Then
                    _MgrAbsentMonthly = dr("MgrAbsentMonthly")
                End If
                If Not IsDBNull(dr("MgrViolationWeekly")) Then
                    _MgrViolationWeekly = dr("MgrViolationWeekly")
                End If
                If Not IsDBNull(dr("MgrViolationMonthly")) Then
                    _MgrViolationMonthly = dr("MgrViolationMonthly")
                End If
                If Not IsDBNull(dr("HRAbsentWeekly")) Then
                    _HRAbsentWeekly = dr("HRAbsentWeekly")
                End If
                If Not IsDBNull(dr("HRAbsentMonthly")) Then
                    _HRAbsentMonthly = dr("HRAbsentMonthly")
                End If
                If Not IsDBNull(dr("HRViolationWeekly")) Then
                    _HRViolationWeekly = dr("HRViolationWeekly")
                End If
                If Not IsDBNull(dr("HRViolationMonthly")) Then
                    _HRViolationMonthly = dr("HRViolationMonthly")
                End If

                If Not IsDBNull(dr("ConsiderAbsentOrLogicalAbsent")) Then
                    _ConsiderAbsentOrLogicalAbsent = dr("ConsiderAbsentOrLogicalAbsent")
                End If
                If Not IsDBNull(dr("ReminderAbsentAfter")) Then
                    _ReminderAbsentAfter = dr("ReminderAbsentAfter")
                End If
                If Not IsDBNull(dr("MgrDailyWeekly")) Then
                    _MgrDailyWeekly = dr("MgrDailyWeekly")
                End If
                If Not IsDBNull(dr("MgrDailyMonthly")) Then
                    _MgrDailyMonthly = dr("MgrDailyMonthly")
                End If
                If Not IsDBNull(dr("MgrDetailedWeekly")) Then
                    _MgrDetailedWeekly = dr("MgrDetailedWeekly")
                End If
                If Not IsDBNull(dr("MgrDetailedMonthly")) Then
                    _MgrDetailedMonthly = dr("MgrDetailedMonthly")
                End If
                If Not IsDBNull(dr("MgrSummaryWeekly")) Then
                    _MgrSummaryWeekly = dr("MgrSummaryWeekly")
                End If
                If Not IsDBNull(dr("MgrSummaryMonthly")) Then
                    _MgrSummaryMonthly = dr("MgrSummaryMonthly")
                End If
                If Not IsDBNull(dr("HRDailyWeekly")) Then
                    _HRDailyWeekly = dr("HRDailyWeekly")
                End If
                If Not IsDBNull(dr("HRDailyMonthly")) Then
                    _HRDailyMonthly = dr("HRDailyMonthly")
                End If
                If Not IsDBNull(dr("HRDetailedWeekly")) Then
                    _HRDetailedWeekly = dr("HRDetailedWeekly")
                End If
                If Not IsDBNull(dr("HRDetailedMonthly")) Then
                    _HRDetailedMonthly = dr("HRDetailedMonthly")
                End If
                If Not IsDBNull(dr("HRSummaryWeekly")) Then
                    _HRSummaryWeekly = dr("HRSummaryWeekly")
                End If
                If Not IsDBNull(dr("HRSummaryMonthly")) Then
                    _HRSummaryMonthly = dr("HRSummaryMonthly")
                End If
                If Not IsDBNull(dr("ConsiderAbsentEvenAttend")) Then
                    _ConsiderAbsentEvenAttend = dr("ConsiderAbsentEvenAttend")
                End If
                If Not IsDBNull(dr("DefaultReportFormat")) Then
                    _DefaultReportFormat = dr("DefaultReportFormat")
                End If
                If Not IsDBNull(dr("AutoPersonalPermissionDelay")) Then
                    _AutoPersonalPermissionDelay = dr("AutoPersonalPermissionDelay")
                End If
                If Not IsDBNull(dr("AutoPermissionDelayDuration")) Then
                    _AutoPermissionDelayDuration = dr("AutoPermissionDelayDuration")
                End If
                If Not IsDBNull(dr("AutoPersonalPermissionEarlyOut")) Then
                    _AutoPersonalPermissionEarlyOut = dr("AutoPersonalPermissionEarlyOut")
                End If
                If Not IsDBNull(dr("AutoPermissionEarlyOutDuration")) Then
                    _AutoPermissionEarlyOutDuration = dr("AutoPermissionEarlyOutDuration")
                End If
                If Not IsDBNull(dr("HasMultiApproval")) Then
                    _HasMultiApproval = dr("HasMultiApproval")
                End If
                If Not IsDBNull(dr("EmployeeManagerFilter")) Then
                    _EmployeeManagerFilter = dr("EmployeeManagerFilter")
                End If
                If Not IsDBNull(dr("FillCheckBoxList")) Then
                    _FillCheckBoxList = dr("FillCheckBoxList")
                End If
                If Not IsDBNull(dr("IsDailyReportWithColor")) Then
                    _IsDailyReportWithColor = dr("IsDailyReportWithColor")
                End If
                If Not IsDBNull(dr("AllowDeleteSchedule")) Then
                    _AllowDeleteSchedule = dr("AllowDeleteSchedule")
                End If
                If Not IsDBNull(dr("DefaultTheme")) Then
                    _DefaultTheme = dr("DefaultTheme")
                End If
                If Not IsDBNull(dr("ArchivingMonths")) Then
                    _ArchivingMonths = dr("ArchivingMonths")
                End If
                If Not IsDBNull(dr("ShowLGWithEntityPrivilege")) Then
                    _ShowLGWithEntityPrivilege = dr("ShowLGWithEntityPrivilege")
                End If
                If Not IsDBNull(dr("IncompleteWorkingHrs")) Then
                    _IncompleteWorkingHrs = dr("IncompleteWorkingHrs")
                End If
                If Not IsDBNull(dr("ManagerDefaultPage")) Then
                    _ManagerDefaultPage = dr("ManagerDefaultPage")
                End If
                If Not IsDBNull(dr("ShowDirectStaffChk")) Then
                    _ShowDirectStaffChk = dr("ShowDirectStaffChk")
                End If
                If Not IsDBNull(dr("ApprovalRecalMethod")) Then
                    _ApprovalRecalMethod = dr("ApprovalRecalMethod")
                End If
                If Not IsDBNull(dr("StudyPerm_NotificationException")) Then
                    _StudyPerm_NotificationException = dr("StudyPerm_NotificationException")
                End If
                If Not IsDBNull(dr("NursingPerm_NotificationException")) Then
                    _NursingPerm_NotificationException = dr("NursingPerm_NotificationException")
                End If
                If Not IsDBNull(dr("AttachmentIsMandatoryManualEntryRequest")) Then
                    _AttachmentIsMandatoryManualEntryRequest = dr("AttachmentIsMandatoryManualEntryRequest")
                End If
                If Not IsDBNull(dr("AttachmentIsMandatoryHRManualEntry")) Then
                    _AttachmentIsMandatoryHRManualEntry = dr("AttachmentIsMandatoryHRManualEntry")
                End If
                If Not IsDBNull(dr("SendToEntityManagerWeekly")) Then
                    _SendToEntityManagerWeekly = dr("SendToEntityManagerWeekly")
                End If
                If Not IsDBNull(dr("SendToEntityManagerMonthly")) Then
                    _SendToEntityManagerMonthly = dr("SendToEntityManagerMonthly")
                End If
                If Not IsDBNull(dr("SendToEmployeeWeekly")) Then
                    _SendToEmployeeWeekly = dr("SendToEmployeeWeekly")
                End If
                If Not IsDBNull(dr("SendToEmployeeMonthly")) Then
                    _SendToEmployeeMonthly = dr("SendToEmployeeMonthly")
                End If
                If Not IsDBNull(dr("EntityManagerViolationWeekly")) Then
                    _EntityManagerViolationWeekly = dr("EntityManagerViolationWeekly")
                End If
                If Not IsDBNull(dr("EntityManagerViolationMonthly")) Then
                    _EntityManagerViolationMonthly = dr("EntityManagerViolationMonthly")
                End If
                If Not IsDBNull(dr("EmployeeViolationWeekly")) Then
                    _EmployeeViolationWeekly = dr("EmployeeViolationWeekly")
                End If
                If Not IsDBNull(dr("EmployeeViolationMonthly")) Then
                    _EmployeeViolationMonthly = dr("EmployeeViolationMonthly")
                End If
                If Not IsDBNull(dr("DailyReportDate")) Then
                    _DailyReportDate = dr("DailyReportDate")
                End If
                If Not IsDBNull(dr("ExcludeGraceFromLostTime")) Then
                    _ExcludeGraceFromLostTime = dr("ExcludeGraceFromLostTime")
                End If
                If Not IsDBNull(dr("YearlyReportFromYearBegining")) Then
                    _YearlyReportFromYearBegining = dr("YearlyReportFromYearBegining")
                End If
                If Not IsDBNull(dr("IncludeNotifications_CoordinatorTypes")) Then
                    _IncludeNotifications_CoordinatorTypes = dr("IncludeNotifications_CoordinatorTypes")
                End If
                If Not IsDBNull(dr("EmployeeAbsentWeekly")) Then
                    _EmployeeAbsentWeekly = dr("EmployeeAbsentWeekly")
                End If
                If Not IsDBNull(dr("EmployeeAbsentMonthly")) Then
                    _EmployeeAbsentMonthly = dr("EmployeeAbsentMonthly")
                End If
                If Not IsDBNull(dr("EmployeeDetailedAbsentWeekly")) Then
                    _EmployeeDetailedAbsentWeekly = dr("EmployeeDetailedAbsentWeekly")
                End If
                If Not IsDBNull(dr("EmployeeDetailedAbsentMonthly")) Then
                    _EmployeeDetailedAbsentMonthly = dr("EmployeeDetailedAbsentMonthly")
                End If
                If Not IsDBNull(dr("EmployeeLostTimeDetailsWeekly")) Then
                    _EmployeeLostTimeDetailsWeekly = dr("EmployeeLostTimeDetailsWeekly")
                End If
                If Not IsDBNull(dr("EmployeeLostTimeDetailsMonthly")) Then
                    _EmployeeLostTimeDetailsMonthly = dr("EmployeeLostTimeDetailsMonthly")
                End If
                If Not IsDBNull(dr("MgrEmpSummaryAttendanceWeekly")) Then
                    _MgrEmpSummaryAttendanceWeekly = dr("MgrEmpSummaryAttendanceWeekly")
                End If
                If Not IsDBNull(dr("MgrEmpSummaryAttendanceWeekly")) Then
                    _MgrEmpSummaryAttendanceMonthly = dr("MgrEmpSummaryAttendanceMonthly")
                End If
                If Not IsDBNull(dr("EnableSemesterSelection_StudyPermission")) Then
                    _EnableSemesterSelection_StudyPermission = dr("EnableSemesterSelection_StudyPermission")
                End If
                If Not IsDBNull(dr("MustCompleteNoHours_RequestPermission")) Then
                    _MustCompleteNoHours_RequestPermission = dr("MustCompleteNoHours_RequestPermission")
                End If
                If Not IsDBNull(dr("NoOfHours_RequestPermission")) Then
                    _NoOfHours_RequestPermission = dr("NoOfHours_RequestPermission")
                End If
                If Not IsDBNull(dr("IncludeConsiderInWorkPermissions_RequestPermission")) Then
                    _IncludeConsiderInWorkPermissions_RequestPermission = dr("IncludeConsiderInWorkPermissions_RequestPermission")
                End If
                If Not IsDBNull(dr("MgrEmpDeductionWeekly")) Then
                    _MgrEmpDeductionWeekly = dr("MgrEmpDeductionWeekly")
                End If
                If Not IsDBNull(dr("MgrEmpDeductionMonthly")) Then
                    _MgrEmpDeductionMonthly = dr("MgrEmpDeductionMonthly")
                End If
                If Not IsDBNull(dr("HrEmpDeductionWeekly")) Then
                    _HrEmpDeductionWeekly = dr("HrEmpDeductionWeekly")
                End If
                If Not IsDBNull(dr("HrEmpDeductionMonthly")) Then
                    _HrEmpDeductionMonthly = dr("HrEmpDeductionMonthly")
                End If
                If Not IsDBNull(dr("EmployeeEmpDeductionWeekly")) Then
                    _EmployeeEmpDeductionWeekly = dr("EmployeeEmpDeductionWeekly")
                End If
                If Not IsDBNull(dr("EmployeeEmpDeductionMonthly")) Then
                    _EmployeeEmpDeductionMonthly = dr("EmployeeEmpDeductionMonthly")
                End If
                If Not IsDBNull(dr("EntityManagerEmpDeductionWeekly")) Then
                    _EntityManagerEmpDeductionWeekly = dr("EntityManagerEmpDeductionWeekly")
                End If
                If Not IsDBNull(dr("EntityManagerEmpDeductionMonthly")) Then
                    _EntityManagerEmpDeductionMonthly = dr("EntityManagerEmpDeductionMonthly")
                End If
                If Not IsDBNull(dr("DeductionEmailFormat")) Then
                    _DeductionEmailFormat = dr("DeductionEmailFormat")
                End If
                If Not IsDBNull(dr("MonthlyDeduction_Report")) Then
                    _MonthlyDeduction_Report = dr("MonthlyDeduction_Report")
                End If
                If Not IsDBNull(dr("PasswordType")) Then
                    _PasswordType = dr("PasswordType")
                End If
                If Not IsDBNull(dr("ShowLeaveLnk_ViolationCorrection")) Then
                    _ShowLeaveLnk_ViolationCorrection = dr("ShowLeaveLnk_ViolationCorrection")
                End If
                If Not IsDBNull(dr("ShowPermissionLnk_ViolationCorrection")) Then
                    _ShowPermissionLnk_ViolationCorrection = dr("ShowPermissionLnk_ViolationCorrection")
                End If
                If Not IsDBNull(dr("ConsiderLeaveOnOffDay")) Then
                    _ConsiderLeaveOnOffDay = dr("ConsiderLeaveOnOffDay")
                End If
                If Not IsDBNull(dr("ViolationJustificationDays")) Then
                    _ViolationJustificationDays = dr("ViolationJustificationDays")
                End If
                If Not IsDBNull(dr("ViolationJustificationDaysPolicy")) Then
                    _ViolationJustificationDaysPolicy = dr("ViolationJustificationDaysPolicy")
                End If
                If Not IsDBNull(dr("ManualEntryNoPerMonth")) Then
                    _ManualEntryNoPerMonth = dr("ManualEntryNoPerMonth")
                End If
                If Not IsDBNull(dr("EmployeeSummaryWeekly")) Then
                    _EmployeeSummaryWeekly = dr("EmployeeSummaryWeekly")
                End If
                If Not IsDBNull(dr("EmployeeSummaryMonthly")) Then
                    _EmployeeSummaryMonthly = dr("EmployeeSummaryMonthly")
                End If
                If Not IsDBNull(dr("EntityManagerMaxAbsentMonthly")) Then
                    _EntityManagerMaxAbsentMonthly = dr("EntityManagerMaxAbsentMonthly")
                End If
                If Not IsDBNull(dr("EntityManagerMaxAbsentWeekly")) Then
                    _EntityManagerMaxAbsentWeekly = dr("EntityManagerMaxAbsentWeekly")
                End If
                If Not IsDBNull(dr("EntityManagerMaxDelayMonthly")) Then
                    _EntityManagerMaxDelayMonthly = dr("EntityManagerMaxDelayMonthly")
                End If
                If Not IsDBNull(dr("EntityManagerMaxDelayWeekly")) Then
                    _EntityManagerMaxDelayWeekly = dr("EntityManagerMaxDelayWeekly")
                End If
                If Not IsDBNull(dr("SendDaily_AbsentDelay_EntityMgr")) Then
                    _SendDaily_AbsentDelay_EntityMgr = dr("SendDaily_AbsentDelay_EntityMgr")
                End If
                If Not IsDBNull(dr("IsAbsentRestPolicy")) Then
                    _IsAbsentRestPolicy = dr("IsAbsentRestPolicy")
                End If
                If Not IsDBNull(dr("MgrDeductionPerPolicyMonthly")) Then
                    _MgrDeductionPerPolicyMonthly = dr("MgrDeductionPerPolicyMonthly")
                End If
                If Not IsDBNull(dr("MgrDeductionPerPolicyWeekly")) Then
                    _MgrDeductionPerPolicyWeekly = dr("MgrDeductionPerPolicyWeekly")
                End If
                If Not IsDBNull(dr("HRDeductionPerPolicyMonthly")) Then
                    _HRDeductionPerPolicyMonthly = dr("HRDeductionPerPolicyMonthly")
                End If
                If Not IsDBNull(dr("HRDeductionPerPolicyWeekly")) Then
                    _HRDeductionPerPolicyWeekly = dr("HRDeductionPerPolicyWeekly")
                End If
                If Not IsDBNull(dr("EmployeeDeductionPerPolicyMonthly")) Then
                    _EmployeeDeductionPerPolicyMonthly = dr("EmployeeDeductionPerPolicyMonthly")
                End If
                If Not IsDBNull(dr("EmployeeDeductionPerPolicyWeekly")) Then
                    _EmployeeDeductionPerPolicyWeekly = dr("EmployeeDeductionPerPolicyWeekly")
                End If
                If Not IsDBNull(dr("EntityManagerDeductionPerPolicyMonthly")) Then
                    _EntityManagerDeductionPerPolicyMonthly = dr("EntityManagerDeductionPerPolicyMonthly")
                End If
                If Not IsDBNull(dr("EntityManagerDeductionPerPolicyWeekly")) Then
                    _EntityManagerDeductionPerPolicyWeekly = dr("EntityManagerDeductionPerPolicyWeekly")
                End If
                If Not IsDBNull(dr("DeductionPerPolicy_Value")) Then
                    _DeductionPerPolicy_Value = dr("DeductionPerPolicy_Value")
                End If
                If Not IsDBNull(dr("AllowEditManualEntryRequestDate")) Then
                    _AllowEditManualEntryRequestDate = dr("AllowEditManualEntryRequestDate")
                End If
                If Not IsDBNull(dr("AllowEditManualEntryRequestTime")) Then
                    _AllowEditManualEntryRequestTime = dr("AllowEditManualEntryRequestTime")
                End If
                If Not IsDBNull(dr("NumberInTransactionRequests")) Then
                    _NumberInTransactionRequests = dr("NumberInTransactionRequests")
                End If
                If Not IsDBNull(dr("NumberOutTransactionRequests")) Then
                    _NumberOutTransactionRequests = dr("NumberOutTransactionRequests")
                End If
                If Not IsDBNull(dr("IsAutoApproveManualEntryRequest")) Then
                    _IsAutoApproveManualEntryRequest = dr("IsAutoApproveManualEntryRequest")
                End If
                If Not IsDBNull(dr("ConsiderRestInshiftSch")) Then
                    _ConsiderRestInshiftSch = dr("ConsiderRestInshiftSch")
                End If
                If Not IsDBNull(dr("ConsiderNursingInRamadan")) Then
                    _ConsiderNursingInRamadan = dr("ConsiderNursingInRamadan")
                End If
                If Not IsDBNull(dr("RemoteWorkTAReason")) Then
                    _RemoteWorkTAReason = dr("RemoteWorkTAReason")
                End If
                If Not IsDBNull(dr("HasTawajudFeatures")) Then
                    _HasTawajudFeatures = dr("HasTawajudFeatures")
                End If
                If Not IsDBNull(dr("HasMultiLocations")) Then
                    _HasMultiLocations = dr("HasMultiLocations")
                End If
                If Not IsDBNull(dr("HasHeartBeat")) Then
                    _HasHeartBeat = dr("HasHeartBeat")
                End If
                If Not IsDBNull(dr("HasFeedback")) Then
                    _HasFeedback = dr("HasFeedback")
                End If
                If Not IsDBNull(dr("AllowFingerPunch")) Then
                    _AllowFingerPunch = dr("AllowFingerPunch")
                End If
                If Not IsDBNull(dr("AllowFingerLogin")) Then
                    _AllowFingerLogin = dr("AllowFingerLogin")
                End If
                If Not IsDBNull(dr("AllowFacePunch")) Then
                    _AllowFacePunch = dr("AllowFacePunch")
                End If
                If Not IsDBNull(dr("AllowFaceLogin")) Then
                    _AllowFaceLogin = dr("AllowFaceLogin")
                End If
                If Not IsDBNull(dr("EnableUniversitySelection_StudyPermission")) Then
                    _EnableUniversitySelection_StudyPermission = dr("EnableUniversitySelection_StudyPermission")
                End If
                If Not IsDBNull(dr("StudyAllowedAfterDays")) Then
                    _StudyAllowedAfterDays = dr("StudyAllowedAfterDays")
                End If
                If Not IsDBNull(dr("StudyAllowedBeforeDays")) Then
                    _StudyAllowedBeforeDays = dr("StudyAllowedBeforeDays")
                End If
                If Not IsDBNull(dr("EnableMajorSelection_StudyPermission")) Then
                    _EnableMajorSelection_StudyPermission = dr("EnableMajorSelection_StudyPermission")
                End If
                If Not IsDBNull(dr("RemarkIsMandatoryManualEntryRequest")) Then
                    _RemarkIsMandatoryManualEntryRequest = dr("RemarkIsMandatoryManualEntryRequest")
                End If
                Return Me
            Else
                Return Nothing
            End If

        End Function

        Public Function GetMobileFeatures() As DataTable
            Return objDALAPP_Settings.GetMobileFeatures()
        End Function

#End Region

    End Class
End Namespace