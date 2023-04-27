Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports SmartV.DB
Imports TA.LookUp

Namespace TA.Admin
    Public Class DALAPP_Settings

#Region "Class Variables"
        Inherits MGRBase
        Private strConn As String
        Dim APP_Settings_insert As String = "APP_Settings_insert"
        Dim APP_Settings_SelectAll As String = "APP_Settings_SelectAll"
        Dim APP_Settings_Select As String = "APP_Settings_Select"
        Dim APP_Settings_Update As String = "APP_Settings_Update"
        Dim APP_Settings_Delete As String = "APP_Settings_Delete"
        Private App_Settings_UpdateImage As String = "App_Settings_UpdateImage"
        Private EmailConfigration_insert As String = "EmailConfigration_insert"
        Private AppSetup As String = "AppSetup"
        Private APP_Settings_Select_MobileFeatures As String = "APP_Settings_Select_MobileFeatures"

#End Region

#Region "Constructor"
        Public Sub New()

        End Sub
#End Region

#Region "Methods"

        Public Function Add(ByVal CompanyName1 As String, ByVal CompanyName2 As String, ByVal companyArabicName1 As String, ByVal CompanyArabicName2 As String,
                            ByVal LogoImage As Byte(), ByVal LogoPath As String, ByVal EmployeeNoLength As Integer, ByVal EmployeeCardLength As Integer,
                            ByVal IsGradeOvertimeRule As Boolean, ByVal IsGradeTAException As Boolean, ByVal AnnualLeaveOption As Integer, ByVal MinGapbetweenMoves As Integer, ByVal ConsequenceTransactions As Integer,
                            ByVal IsGraceTAPolicy As Boolean, ByVal LeaveApproval As Integer, ByVal SystemUsersType As Integer, ByVal HRGroupEmail As String, ByVal HREmailNotification As Integer,
                            ByVal AllowEditOverTime As Boolean, ByVal ConsiderAbsentAfter As Integer, ByVal LeaveApprovalfromLeave As Boolean, ByVal DynamicReportView As Integer, ByVal SendToManagerDaily As Boolean,
                            ByVal SendToManagerWeekly As Boolean, ByVal SendToManagerMonthly As Boolean, ByVal SendToHRDaily As Boolean, ByVal SendToHRWeekly As Boolean, ByVal SendToHRMonthly As Boolean,
                            ByVal NoShiftShcedule As Integer, ByVal NursingDays As Integer, ByVal DaysMinutes As Integer, ByVal HasFullDayPermission As Boolean, ByVal AllowChangeEmpNo As Integer, ByVal AllowedBefore As String,
                            ByVal StudyPermissionSchedule As Integer, ByVal NursingPermissionAttend As Boolean, ByVal PermApprovalFromPermission As Boolean, ByVal ManualEntryNo As String, ByVal MinOutViolation As Integer,
                            ByVal ManagerEmailFormat As Integer, ByVal HREmailFormat As Integer, ByVal ManualEntryManagerLevelRequired As Integer, ByVal HasEmailApproval As Boolean, ByVal ShowLoginForm As Boolean, ByVal ShowViolationCorrection As Boolean,
                            ByVal FK_MaternityLeaveTypeId As Integer, ByVal FK_PersonalPermissionTypeId As Integer, ByVal NursingRequireMaternity As Boolean, ByVal RequestGridToAppear As String, ByVal EarlyOutNotificationAfter As Integer,
                            ByVal PermissionRequestReminderAfter As Integer, ByVal HasFlexiblePermission As Boolean, ByVal HasFlexibleNursingPermission As Boolean, ByVal ShowAbsentInViolationCorrection As Boolean, ByVal HasPermissionForPeriod As Boolean,
                            ByVal IsToConsiderBalanceInHours As Boolean, ByVal ShowThemeToUsers As Boolean, ByVal DurationTotalsToAppear As String, ByVal AttachmentIsMandatory As Boolean, ByVal IsFirstGrid As Boolean,
                            ByVal DefaultStudyPermissionFromTime As Integer, ByVal DefaultStudyPermissionToTime As Integer, ByVal DefaultStudyPermissionFlexibleTime As Integer, ByVal ShowAnnouncement As Boolean, ByVal DivideTwoPermission As Boolean, ByVal MaternityLeaveDuration As Integer,
                            ByVal StudyPermissionApproval As Integer, ByVal NursingPermissionApproval As Integer, ByVal MaxStudyPermission As Integer, ByVal StudyGeneralGuide As String, ByVal StudyGeneralGuideAr As String, ByVal NursingGeneralGuide As String, ByVal NursingGeneralGuideAr As String,
                            ByVal AllowMoreOneManualEntry As Boolean, ByVal ShowAnnouncementSelfService As Boolean, ByVal OnCallSchedule As Integer, ByVal AllowNursingInRamadan As Boolean, ByVal ShowEmployeeList As Boolean,
                            ByVal MgrAbsentWeekly As Integer, ByVal MgrAbsentMonthly As Integer, ByVal MgrViolationWeekly As Integer, ByVal MgrViolationMonthly As Integer, ByVal HRAbsentWeekly As Integer,
                            ByVal HRAbsentMonthly As Integer, ByVal HRViolationWeekly As Integer, ByVal HRViolationMonthly As Integer, ByVal ConsiderAbsentOrLogicalAbsent As Integer, ByVal ReminderAbsentAfter As Integer,
                            ByVal MgrDailyWeekly As Integer, ByVal MgrDailyMonthly As Integer, ByVal MgrDetailedWeekly As Integer, ByVal MgrDetailedMonthly As Integer, ByVal MgrSummaryWeekly As Integer, ByVal MgrSummaryMonthly As Integer,
                            ByVal HRDailyWeekly As Integer, ByVal HRDailyMonthly As Integer, ByVal HRDetailedWeekly As Integer, ByVal HRDetailedMonthly As Integer, ByVal HRSummaryWeekly As Integer, ByVal HRSummaryMonthly As Integer, ByVal ConsiderAbsentEvenAttend As Boolean, ByVal DefaultReportFormat As Integer,
                            ByVal AutoPersonalPermissionDelay As Boolean, ByVal AutoPermissionDelayDuration As Integer, ByVal AutoPersonalPermissionEarlyOut As Boolean, ByVal AutoPermissionEarlyOutDuration As Integer, ByVal HasMultiApproval As Boolean, ByVal EmployeeManagerFilter As Integer,
                            ByVal FillCheckBoxList As Integer, ByVal IsDailyReportWithColor As Boolean, ByVal AllowDeleteSchedule As Boolean, ByVal DefaultTheme As String, ByVal ArchivingMonths As Integer,
                            ByVal ShowLGWithEntityPrivilege As Boolean, ByVal ManagerDefaultPage As Integer, ByVal ShowDirectStaffChk As Boolean, ByVal ApprovalRecalMethod As Integer, ByVal StudyPerm_NotificationException As Boolean,
                            ByVal NursingPerm_NotificationException As Boolean, ByVal AttachmentIsMandatoryManualEntryRequest As Boolean, ByVal AttachmentIsMandatoryHRManualEntry As Boolean, ByVal DailyReportDate As Integer, ByVal ExcludeGraceFromLostTime As Boolean,
                            ByVal EnableSemesterSelection_StudyPermission As Boolean, ByVal MustCompleteNoHours_RequestPermission As Boolean, ByVal NoOfHours_RequestPermission As Integer, ByVal IncludeConsiderInWorkPermissions_RequestPermission As Boolean, ByVal MonthlyDeduction_Report As Integer,
                            ByVal PasswordType As String, ByVal ShowLeaveLnk_ViolationCorrection As Boolean, ByVal ShowPermissionLnk_ViolationCorrection As Boolean, ByVal ConsiderLeaveOnOffDay As Integer,
                            ByVal ViolationJustificationDays As String, ByVal ViolationJustificationDaysPolicy As String, ByVal ManualEntryNoPerMonth As String, ByVal IsAbsentRestPolicy As Boolean, ByVal AllowEditManualEntryRequestDate As Boolean,
                            ByVal AllowEditManualEntryRequestTime As Boolean, ByVal NumberInTransactionRequests As String, ByVal NumberOutTransactionRequests As String, ByVal IsAutoApproveManualEntryRequest As Boolean, ByVal ConsiderRestInshiftSch As Integer,
                            ByVal ConsiderNursingInRamadan As Boolean, ByVal RemoteWorkTAReason As String, ByVal HasTawajudFeatures As Boolean, ByVal HasMultiLocations As Boolean, ByVal HasHeartBeat As Boolean, ByVal HasFeedback As Boolean,
                            ByVal AllowFingerPunch As Boolean, ByVal AllowFingerLogin As Boolean, ByVal AllowFacePunch As Boolean, ByVal AllowFaceLogin As Boolean, ByVal EnableUniversitySelection_StudyPermission As Boolean, ByVal StudyAllowedAfterDays As Integer?,
                            ByVal StudyAllowedBeforeDays As Integer?, ByVal EnableMajorSelection_StudyPermission As Boolean?, ByVal RemarkIsMandatoryManualEntryRequest As Boolean) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(APP_Settings_insert,
               New SqlParameter("@CompanyName1", CompanyName1),
               New SqlParameter("@CompanyName2", CompanyName2),
               New SqlParameter("@companyArabicName1", companyArabicName1),
               New SqlParameter("@CompanyArabicName2", CompanyArabicName2),
               New SqlParameter("@LogoImage", LogoImage),
               New SqlParameter("@LogoPath", LogoPath),
               New SqlParameter("@EmployeeNoLength", EmployeeNoLength),
               New SqlParameter("@EmployeeCardLength", EmployeeCardLength),
               New SqlParameter("@IsGradeOvertimeRule", IsGradeOvertimeRule),
               New SqlParameter("@IsGradeTAException", IsGradeTAException),
               New SqlParameter("@AnnualLeaveOption", AnnualLeaveOption),
               New SqlParameter("@IsGraceTAPolicy", IsGraceTAPolicy),
               New SqlParameter("@MinGapbetweenMoves", MinGapbetweenMoves),
               New SqlParameter("@ConsequenceTransactions", ConsequenceTransactions),
               New SqlParameter("@LeaveApproval", LeaveApproval),
               New SqlParameter("@HRGroupEmail", HRGroupEmail),
               New SqlParameter("@HREmailNotification", HREmailNotification),
               New SqlParameter("@SystemUsersType", SystemUsersType),
               New SqlParameter("@AllowEditOverTime", AllowEditOverTime),
               New SqlParameter("@ConsiderAbsentAfter", ConsiderAbsentAfter),
               New SqlParameter("@LeaveApprovalfromLeave", LeaveApprovalfromLeave),
               New SqlParameter("@DynamicReportView", DynamicReportView),
               New SqlParameter("@SendToManagerDaily", SendToManagerDaily),
               New SqlParameter("@SendToManagerWeekly", SendToManagerWeekly),
               New SqlParameter("@SendToManagerMonthly", SendToManagerMonthly),
               New SqlParameter("@SendToHRDaily", SendToHRDaily),
               New SqlParameter("@SendToHRWeekly", SendToHRWeekly),
               New SqlParameter("@SendToHRMonthly", SendToHRMonthly),
               New SqlParameter("@NoShiftShcedule", NoShiftShcedule),
               New SqlParameter("@DaysMinutes", DaysMinutes),
               New SqlParameter("@NursingDays", NursingDays),
               New SqlParameter("@HasFullDayPermission", HasFullDayPermission),
               New SqlParameter("@AllowChangeEmpNo", AllowChangeEmpNo),
               New SqlParameter("@AllowedBefore", IIf(AllowedBefore = String.Empty, DBNull.Value, AllowedBefore)),
               New SqlParameter("@StudyPermissionSchedule", StudyPermissionSchedule),
               New SqlParameter("@NursingPermissionAttend", NursingPermissionAttend),
               New SqlParameter("@PermApprovalFromPermission", PermApprovalFromPermission),
               New SqlParameter("@ManualEntryNo", IIf(ManualEntryNo = String.Empty, DBNull.Value, ManualEntryNo)),
               New SqlParameter("@MinOutViolation", MinOutViolation),
               New SqlParameter("@ManagerEmailFormat", ManagerEmailFormat),
               New SqlParameter("@HREmailFormat", HREmailFormat),
               New SqlParameter("@ManualEntryManagerLevelRequired", ManualEntryManagerLevelRequired),
               New SqlParameter("@HasEmailApproval", HasEmailApproval),
               New SqlParameter("@ShowLoginForm", ShowLoginForm),
               New SqlParameter("@ShowViolationCorrection", ShowViolationCorrection),
               New SqlParameter("@FK_MaternityLeaveTypeId", FK_MaternityLeaveTypeId),
               New SqlParameter("@FK_PersonalPermissionTypeId", FK_PersonalPermissionTypeId),
               New SqlParameter("@NursingRequireMaternity", NursingRequireMaternity),
               New SqlParameter("@RequestGridToAppear", RequestGridToAppear),
               New SqlParameter("@EarlyOutNotificationAfter", EarlyOutNotificationAfter),
               New SqlParameter("@PermissionRequestReminderAfter", PermissionRequestReminderAfter),
               New SqlParameter("@HasFlexiblePermission", HasFlexiblePermission),
               New SqlParameter("@HasFlexibleNursingPermission", HasFlexibleNursingPermission),
               New SqlParameter("@ShowAbsentInViolationCorrection", ShowAbsentInViolationCorrection),
               New SqlParameter("@HasPermissionForPeriod", HasPermissionForPeriod),
               New SqlParameter("@IsToConsiderBalanceInHours", IsToConsiderBalanceInHours),
               New SqlParameter("@ShowThemeToUsers", ShowThemeToUsers),
               New SqlParameter("@DurationTotalsToAppear", DurationTotalsToAppear),
               New SqlParameter("@AttachmentIsMandatory", AttachmentIsMandatory),
               New SqlParameter("@IsFirstGrid", IsFirstGrid),
               New SqlParameter("@DefaultStudyPermissionFromTime", DefaultStudyPermissionFromTime),
               New SqlParameter("@DefaultStudyPermissionToTime", DefaultStudyPermissionToTime),
               New SqlParameter("@DefaultStudyPermissionFlexibleTime", DefaultStudyPermissionFlexibleTime),
               New SqlParameter("@ShowAnnouncement", ShowAnnouncement),
               New SqlParameter("@DivideTwoPermission", DivideTwoPermission),
               New SqlParameter("@MaternityLeaveDuration", MaternityLeaveDuration),
               New SqlParameter("@StudyPermissionApproval", StudyPermissionApproval),
               New SqlParameter("@NursingPermissionApproval", NursingPermissionApproval),
               New SqlParameter("@MaxStudyPermission", MaxStudyPermission),
               New SqlParameter("@StudyGeneralGuide", StudyGeneralGuide),
               New SqlParameter("@StudyGeneralGuideAr", StudyGeneralGuideAr),
               New SqlParameter("@NursingGeneralGuide", NursingGeneralGuide),
               New SqlParameter("@NursingGeneralGuideAr", NursingGeneralGuideAr),
               New SqlParameter("@AllowMoreOneManualEntry", AllowMoreOneManualEntry),
               New SqlParameter("@ShowAnnouncementSelfService", ShowAnnouncementSelfService),
               New SqlParameter("@OnCallSchedule", OnCallSchedule),
               New SqlParameter("@AllowNursingInRamadan", AllowNursingInRamadan),
               New SqlParameter("@ShowEmployeeList", ShowEmployeeList),
               New SqlParameter("@MgrAbsentWeekly", MgrAbsentWeekly),
               New SqlParameter("@MgrAbsentMonthly", MgrAbsentMonthly),
               New SqlParameter("@MgrViolationWeekly", MgrViolationWeekly),
               New SqlParameter("@MgrViolationMonthly", MgrViolationMonthly),
               New SqlParameter("@HRAbsentWeekly", HRAbsentWeekly),
               New SqlParameter("@HRAbsentMonthly", HRAbsentMonthly),
               New SqlParameter("@HRViolationWeekly", HRViolationWeekly),
               New SqlParameter("@HRViolationMonthly", HRViolationMonthly),
               New SqlParameter("@ConsiderAbsentOrLogicalAbsent", ConsiderAbsentOrLogicalAbsent),
               New SqlParameter("@ReminderAbsentAfter", ReminderAbsentAfter),
               New SqlParameter("@MgrDailyWeekly", MgrDailyWeekly),
               New SqlParameter("@MgrDailyMonthly", MgrDailyMonthly),
               New SqlParameter("@MgrDetailedWeekly", MgrDetailedWeekly),
               New SqlParameter("@MgrDetailedMonthly", MgrDetailedMonthly),
               New SqlParameter("@MgrSummaryWeekly", MgrSummaryWeekly),
               New SqlParameter("@MgrSummaryMonthly", MgrSummaryMonthly),
               New SqlParameter("@HRDailyWeekly", HRDailyWeekly),
               New SqlParameter("@HRDailyMonthly", HRDailyMonthly),
               New SqlParameter("@HRDetailedWeekly", HRDetailedWeekly),
               New SqlParameter("@HRDetailedMonthly", HRDetailedMonthly),
               New SqlParameter("@HRSummaryWeekly", HRSummaryWeekly),
               New SqlParameter("@HRSummaryMonthly", HRSummaryMonthly),
               New SqlParameter("@ConsiderAbsentEvenAttend", ConsiderAbsentEvenAttend),
               New SqlParameter("@DefaultReportFormat", DefaultReportFormat),
               New SqlParameter("@AutoPersonalPermissionDelay", AutoPersonalPermissionDelay),
               New SqlParameter("@AutoPermissionDelayDuration", AutoPermissionDelayDuration),
               New SqlParameter("@AutoPersonalPermissionEarlyOut", AutoPersonalPermissionEarlyOut),
               New SqlParameter("@AutoPermissionEarlyOutDuration", AutoPermissionEarlyOutDuration),
               New SqlParameter("@HasMultiApproval", HasMultiApproval),
               New SqlParameter("@EmployeeManagerFilter", EmployeeManagerFilter),
               New SqlParameter("@FillCheckBoxList", FillCheckBoxList),
               New SqlParameter("@IsDailyReportWithColor", IsDailyReportWithColor),
               New SqlParameter("@AllowDeleteSchedule", AllowDeleteSchedule),
               New SqlParameter("@DefaultTheme", DefaultTheme),
               New SqlParameter("@ArchivingMonths", ArchivingMonths),
               New SqlParameter("@ShowLGWithEntityPrivilege", ShowLGWithEntityPrivilege),
               New SqlParameter("@ManagerDefaultPage", ManagerDefaultPage),
               New SqlParameter("@ShowDirectStaffChk", ShowDirectStaffChk),
               New SqlParameter("@ApprovalRecalMethod", ApprovalRecalMethod),
               New SqlParameter("@StudyPerm_NotificationException", StudyPerm_NotificationException),
               New SqlParameter("@NursingPerm_NotificationException", NursingPerm_NotificationException),
               New SqlParameter("@AttachmentIsMandatoryManualEntryRequest", AttachmentIsMandatoryManualEntryRequest),
               New SqlParameter("@AttachmentIsMandatoryHRManualEntry", AttachmentIsMandatoryHRManualEntry),
               New SqlParameter("@DailyReportDate", DailyReportDate),
               New SqlParameter("@ExcludeGraceFromLostTime", ExcludeGraceFromLostTime),
               New SqlParameter("@EnableSemesterSelection_StudyPermission", EnableSemesterSelection_StudyPermission),
               New SqlParameter("@MustCompleteNoHours_RequestPermission", MustCompleteNoHours_RequestPermission),
               New SqlParameter("@NoOfHours_RequestPermission", NoOfHours_RequestPermission),
               New SqlParameter("@IncludeConsiderInWorkPermissions_RequestPermission", IncludeConsiderInWorkPermissions_RequestPermission),
               New SqlParameter("@MonthlyDeduction_Report", MonthlyDeduction_Report),
               New SqlParameter("@PasswordType", PasswordType),
               New SqlParameter("@ShowLeaveLnk_ViolationCorrection", ShowLeaveLnk_ViolationCorrection),
               New SqlParameter("@ShowPermissionLnk_ViolationCorrection", ShowPermissionLnk_ViolationCorrection),
               New SqlParameter("@ConsiderLeaveOnOffDay", ConsiderLeaveOnOffDay),
               New SqlParameter("@ViolationJustificationDays", ViolationJustificationDays),
               New SqlParameter("@ViolationJustificationDaysPolicy", ViolationJustificationDaysPolicy),
               New SqlParameter("@ManualEntryNoPerMonth", ManualEntryNoPerMonth),
               New SqlParameter("@IsAbsentRestPolicy", IsAbsentRestPolicy),
               New SqlParameter("@AllowEditManualEntryRequestDate", AllowEditManualEntryRequestDate),
               New SqlParameter("@AllowEditManualEntryRequestTime", AllowEditManualEntryRequestTime),
               New SqlParameter("@NumberInTransactionRequests", NumberInTransactionRequests),
               New SqlParameter("@NumberOutTransactionRequests", NumberOutTransactionRequests),
               New SqlParameter("@IsAutoApproveManualEntryRequest", IsAutoApproveManualEntryRequest),
               New SqlParameter("@ConsiderRestInshiftSch", ConsiderRestInshiftSch),
               New SqlParameter("@ConsiderNursingInRamadan", ConsiderNursingInRamadan),
               New SqlParameter("@RemoteWorkTAReason", RemoteWorkTAReason),
               New SqlParameter("@HasTawajudFeatures", HasTawajudFeatures),
               New SqlParameter("@HasMultiLocations", HasMultiLocations),
               New SqlParameter("@HasHeartBeat", HasHeartBeat),
               New SqlParameter("@HasFeedback", HasFeedback),
               New SqlParameter("@AllowFingerPunch", AllowFingerPunch),
               New SqlParameter("@AllowFingerLogin", AllowFingerLogin),
               New SqlParameter("@AllowFacePunch", AllowFacePunch),
               New SqlParameter("@AllowFaceLogin", AllowFaceLogin),
               New SqlParameter("@EnableUniversitySelection_StudyPermission", EnableUniversitySelection_StudyPermission),
               New SqlParameter("@StudyAllowedAfterDays", StudyAllowedAfterDays),
               New SqlParameter("@StudyAllowedBeforeDays", StudyAllowedBeforeDays),
               New SqlParameter("@EnableMajorSelection_StudyPermission", EnableMajorSelection_StudyPermission),
 New SqlParameter("@RemarkIsMandatoryManualEntryRequest", RemarkIsMandatoryManualEntryRequest
               ))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Public Function EmailConfigration_Add(ByVal CompanyName1 As String, ByVal CompanyName2 As String, ByVal companyArabicName1 As String, ByVal CompanyArabicName2 As String,
                    ByVal LogoImage As Byte(), ByVal LogoPath As String, ByVal EmployeeNoLength As Integer, ByVal EmployeeCardLength As Integer,
                    ByVal IsGradeOvertimeRule As Boolean, ByVal IsGradeTAException As Boolean, ByVal AnnualLeaveOption As Integer, ByVal MinGapbetweenMoves As Integer, ByVal ConsequenceTransactions As Integer,
                    ByVal IsGraceTAPolicy As Boolean, ByVal LeaveApproval As Integer, ByVal SystemUsersType As Integer, ByVal HRGroupEmail As String, ByVal HREmailNotification As Integer,
                    ByVal AllowEditOverTime As Boolean, ByVal ConsiderAbsentAfter As Integer, ByVal LeaveApprovalfromLeave As Boolean, ByVal DynamicReportView As Integer, ByVal SendToManagerDaily As Boolean,
                    ByVal SendToManagerWeekly As Boolean, ByVal SendToManagerMonthly As Boolean, ByVal SendToHRDaily As Boolean, ByVal SendToHRWeekly As Boolean, ByVal SendToHRMonthly As Boolean,
                    ByVal NoShiftShcedule As Integer, ByVal NursingDays As Integer, ByVal DaysMinutes As Integer, ByVal HasFullDayPermission As Boolean, ByVal AllowChangeEmpNo As Integer, ByVal AllowedBefore As String,
                    ByVal StudyPermissionSchedule As Integer, ByVal NursingPermissionAttend As Boolean, ByVal PermApprovalFromPermission As Boolean, ByVal ManualEntryNo As String, ByVal MinOutViolation As Integer,
                    ByVal ManagerEmailFormat As Integer, ByVal HREmailFormat As Integer, ByVal ManualEntryManagerLevelRequired As Integer, ByVal HasEmailApproval As Boolean, ByVal ShowLoginForm As Boolean, ByVal ShowViolationCorrection As Boolean,
                    ByVal FK_MaternityLeaveTypeId As Integer, ByVal FK_PersonalPermissionTypeId As Integer, ByVal NursingRequireMaternity As Boolean, ByVal RequestGridToAppear As String, ByVal EarlyOutNotificationAfter As Integer,
                    ByVal PermissionRequestReminderAfter As Integer, ByVal HasFlexiblePermission As Boolean, ByVal HasFlexibleNursingPermission As Boolean, ByVal ShowAbsentInViolationCorrection As Boolean, ByVal HasPermissionForPeriod As Boolean,
                    ByVal IsToConsiderBalanceInHours As Boolean, ByVal ShowThemeToUsers As Boolean, ByVal DurationTotalsToAppear As String, ByVal AttachmentIsMandatory As Boolean, ByVal IsFirstGrid As Boolean,
                    ByVal DefaultStudyPermissionFromTime As Integer, ByVal DefaultStudyPermissionToTime As Integer, ByVal DefaultStudyPermissionFlexibleTime As Integer, ByVal ShowAnnouncement As Boolean, ByVal DivideTwoPermission As Boolean, ByVal MaternityLeaveDuration As Integer,
                    ByVal StudyPermissionApproval As Integer, ByVal NursingPermissionApproval As Integer, ByVal MaxStudyPermission As Integer, ByVal StudyGeneralGuide As String, ByVal StudyGeneralGuideAr As String, ByVal NursingGeneralGuide As String, ByVal NursingGeneralGuideAr As String,
                    ByVal AllowMoreOneManualEntry As Boolean, ByVal ShowAnnouncementSelfService As Boolean, ByVal OnCallSchedule As Integer, ByVal AllowNursingInRamadan As Boolean, ByVal ShowEmployeeList As Boolean,
                    ByVal MgrAbsentWeekly As Integer, ByVal MgrAbsentMonthly As Integer, ByVal MgrViolationWeekly As Integer, ByVal MgrViolationMonthly As Integer, ByVal HRAbsentWeekly As Integer,
                    ByVal HRAbsentMonthly As Integer, ByVal HRViolationWeekly As Integer, ByVal HRViolationMonthly As Integer, ByVal ConsiderAbsentOrLogicalAbsent As Integer, ByVal ReminderAbsentAfter As Integer,
                    ByVal MgrDailyWeekly As Integer, ByVal MgrDailyMonthly As Integer, ByVal MgrDetailedWeekly As Integer, ByVal MgrDetailedMonthly As Integer, ByVal MgrSummaryWeekly As Integer, ByVal MgrSummaryMonthly As Integer,
                    ByVal HRDailyWeekly As Integer, ByVal HRDailyMonthly As Integer, ByVal HRDetailedWeekly As Integer, ByVal HRDetailedMonthly As Integer, ByVal HRSummaryWeekly As Integer, ByVal HRSummaryMonthly As Integer, ByVal ConsiderAbsentEvenAttend As Boolean, ByVal DefaultReportFormat As Integer,
                    ByVal AutoPersonalPermissionDelay As Boolean, ByVal AutoPermissionDelayDuration As Integer, ByVal AutoPersonalPermissionEarlyOut As Boolean, ByVal AutoPermissionEarlyOutDuration As Integer, ByVal HasMultiApproval As Boolean, ByVal EmployeeManagerFilter As Integer,
                    ByVal FillCheckBoxList As Integer, ByVal IsDailyReportWithColor As Boolean, ByVal IncompleteWorkingHrs As String, ByVal SendToEntityManagerWeekly As Boolean,
                    ByVal SendToEntityManagerMonthly As Boolean, ByVal SendToEmployeeWeekly As Boolean, ByVal SendToEmployeeMonthly As Boolean, ByVal EntityManagerViolationWeekly As Integer,
                    ByVal EntityManagerViolationMonthly As Integer, ByVal EmployeeViolationWeekly As Integer, ByVal EmployeeViolationMonthly As Integer, ByVal YearlyReportFromYearBegining As Boolean, ByVal IncludeNotifications_CoordinatorTypes As Boolean,
                    ByVal EmployeeAbsentWeekly As Integer, ByVal EmployeeAbsentMonthly As Integer, ByVal EmployeeDetailedAbsentWeekly As Integer, ByVal EmployeeDetailedAbsentMonthly As Integer, ByVal EmployeeLostTimeDetailsWeekly As Integer,
                    ByVal EmployeeLostTimeDetailsMonthly As Integer, ByVal MgrEmpSummaryAttendanceWeekly As Integer, ByVal MgrEmpSummaryAttendanceMonthly As Integer, ByVal MgrEmpDeductionWeekly As Integer, ByVal MgrEmpDeductionMonthly As Integer,
                    ByVal HrEmpDeductionWeekly As Integer, ByVal HrEmpDeductionMonthly As Integer, ByVal EmployeeEmpDeductionWeekly As Integer, ByVal EmployeeEmpDeductionMonthly As Integer, ByVal EntityManagerEmpDeductionWeekly As Integer,
                    ByVal EntityManagerEmpDeductionMonthly As Integer, ByVal DeductionEmailFormat As String, ByVal EmployeeSummaryWeekly As Integer, ByVal EmployeeSummaryMonthly As Integer, ByVal EntityManagerMaxAbsentMonthly As Integer,
                    ByVal EntityManagerMaxAbsentWeekly As Integer, ByVal EntityManagerMaxDelayMonthly As Integer, ByVal EntityManagerMaxDelayWeekly As Integer, ByVal SendDaily_AbsentDelay_EntityMgr As Boolean,
                    ByVal MgrDeductionPerPolicyMonthly As Integer, ByVal MgrDeductionPerPolicyWeekly As Integer, ByVal HRDeductionPerPolicyMonthly As Integer, ByVal HRDeductionPerPolicyWeekly As Integer,
                    ByVal EmployeeDeductionPerPolicyMonthly As Integer, ByVal EmployeeDeductionPerPolicyWeekly As Integer, ByVal EntityManagerDeductionPerPolicyMonthly As Integer, ByVal EntityManagerDeductionPerPolicyWeekly As Integer,
                    ByVal DeductionPerPolicy_Value As Integer, ByVal RemarkIsMandatoryManualEntryRequest As Boolean) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(EmailConfigration_insert,
                New SqlParameter("@CompanyName1", CompanyName1),
                New SqlParameter("@HRGroupEmail", HRGroupEmail),
                New SqlParameter("@HREmailNotification", HREmailNotification),
                New SqlParameter("@SendToManagerWeekly", SendToManagerWeekly),
                New SqlParameter("@SendToManagerMonthly", SendToManagerMonthly),
                New SqlParameter("@SendToHRDaily", SendToHRDaily),
                New SqlParameter("@SendToHRWeekly", SendToHRWeekly),
                New SqlParameter("@SendToHRMonthly", SendToHRMonthly),
                New SqlParameter("@ManagerEmailFormat", ManagerEmailFormat),
                New SqlParameter("@HREmailFormat", HREmailFormat),
                New SqlParameter("@HasEmailApproval", HasEmailApproval),
                New SqlParameter("@EarlyOutNotificationAfter", EarlyOutNotificationAfter),
                New SqlParameter("@PermissionRequestReminderAfter", PermissionRequestReminderAfter),
                New SqlParameter("@MgrAbsentWeekly", MgrAbsentWeekly),
                New SqlParameter("@MgrAbsentMonthly", MgrAbsentMonthly),
                New SqlParameter("@MgrViolationWeekly", MgrViolationWeekly),
                New SqlParameter("@MgrViolationMonthly", MgrViolationMonthly),
                New SqlParameter("@HRAbsentWeekly", HRAbsentWeekly),
                New SqlParameter("@HRAbsentMonthly", HRAbsentMonthly),
                New SqlParameter("@HRViolationWeekly", HRViolationWeekly),
                New SqlParameter("@HRViolationMonthly", HRViolationMonthly),
                New SqlParameter("@ReminderAbsentAfter", ReminderAbsentAfter),
                New SqlParameter("@MgrDailyWeekly", MgrDailyWeekly),
                New SqlParameter("@MgrDailyMonthly", MgrDailyMonthly),
                New SqlParameter("@MgrDetailedWeekly", MgrDetailedWeekly),
                New SqlParameter("@MgrDetailedMonthly", MgrDetailedMonthly),
                New SqlParameter("@MgrSummaryWeekly", MgrSummaryWeekly),
                New SqlParameter("@MgrSummaryMonthly", MgrSummaryMonthly),
                New SqlParameter("@HRDailyWeekly", HRDailyWeekly),
                New SqlParameter("@HRDailyMonthly", HRDailyMonthly),
                New SqlParameter("@HRDetailedWeekly", HRDetailedWeekly),
                New SqlParameter("@HRDetailedMonthly", HRDetailedMonthly),
                New SqlParameter("@HRSummaryWeekly", HRSummaryWeekly),
                New SqlParameter("@HRSummaryMonthly", HRSummaryMonthly),
                New SqlParameter("@IncompleteWorkingHrs", IncompleteWorkingHrs),
                New SqlParameter("@SendToEntityManagerWeekly", SendToEntityManagerWeekly),
                New SqlParameter("@SendToEntityManagerMonthly", SendToEntityManagerMonthly),
                New SqlParameter("@SendToEmployeeWeekly", SendToEmployeeWeekly),
                New SqlParameter("@SendToEmployeeMonthly", SendToEmployeeMonthly),
                New SqlParameter("@EntityManagerViolationWeekly", EntityManagerViolationWeekly),
                New SqlParameter("@EntityManagerViolationMonthly", EntityManagerViolationMonthly),
                New SqlParameter("@EmployeeViolationWeekly", EmployeeViolationWeekly),
                New SqlParameter("@EmployeeViolationMonthly", EmployeeViolationMonthly),
                New SqlParameter("@YearlyReportFromYearBegining", YearlyReportFromYearBegining),
                New SqlParameter("@IncludeNotifications_CoordinatorTypes", IncludeNotifications_CoordinatorTypes),
                New SqlParameter("@EmployeeAbsentWeekly", EmployeeAbsentWeekly),
                New SqlParameter("@EmployeeAbsentMonthly", EmployeeAbsentMonthly),
                New SqlParameter("@EmployeeDetailedAbsentWeekly", EmployeeDetailedAbsentWeekly),
                New SqlParameter("@EmployeeDetailedAbsentMonthly", EmployeeDetailedAbsentMonthly),
                New SqlParameter("@EmployeeLostTimeDetailsWeekly", EmployeeLostTimeDetailsWeekly),
                New SqlParameter("@EmployeeLostTimeDetailsMonthly", EmployeeLostTimeDetailsMonthly),
                New SqlParameter("@MgrEmpSummaryAttendanceWeekly", MgrEmpSummaryAttendanceWeekly),
                New SqlParameter("@MgrEmpSummaryAttendanceMonthly", MgrEmpSummaryAttendanceMonthly),
                New SqlParameter("@MgrEmpDeductionWeekly", MgrEmpDeductionWeekly),
                New SqlParameter("@MgrEmpDeductionMonthly", MgrEmpDeductionMonthly),
                New SqlParameter("@HrEmpDeductionWeekly", HrEmpDeductionWeekly),
                New SqlParameter("@HrEmpDeductionMonthly", HrEmpDeductionMonthly),
                New SqlParameter("@EmployeeEmpDeductionWeekly", EmployeeEmpDeductionWeekly),
                New SqlParameter("@EmployeeEmpDeductionMonthly", EmployeeEmpDeductionMonthly),
                New SqlParameter("@EntityManagerEmpDeductionWeekly", EntityManagerEmpDeductionWeekly),
                New SqlParameter("@EntityManagerEmpDeductionMonthly", EntityManagerEmpDeductionMonthly),
                New SqlParameter("@DeductionEmailFormat", DeductionEmailFormat),
                New SqlParameter("@EmployeeSummaryWeekly", EmployeeSummaryWeekly),
                New SqlParameter("@EmployeeSummaryMonthly", EmployeeSummaryMonthly),
                New SqlParameter("@EntityManagerMaxAbsentMonthly", EntityManagerMaxAbsentMonthly),
                New SqlParameter("@EntityManagerMaxAbsentWeekly", EntityManagerMaxAbsentWeekly),
                New SqlParameter("@EntityManagerMaxDelayMonthly", EntityManagerMaxDelayMonthly),
                New SqlParameter("@EntityManagerMaxDelayWeekly", EntityManagerMaxDelayWeekly),
                New SqlParameter("@SendDaily_AbsentDelay_EntityMgr", SendDaily_AbsentDelay_EntityMgr),
                New SqlParameter("@MgrDeductionPerPolicyMonthly", MgrDeductionPerPolicyMonthly),
                New SqlParameter("@MgrDeductionPerPolicyWeekly", MgrDeductionPerPolicyWeekly),
                New SqlParameter("@HRDeductionPerPolicyMonthly", HRDeductionPerPolicyMonthly),
                New SqlParameter("@HRDeductionPerPolicyWeekly", HRDeductionPerPolicyWeekly),
                New SqlParameter("@EmployeeDeductionPerPolicyMonthly", EmployeeDeductionPerPolicyMonthly),
                New SqlParameter("@EmployeeDeductionPerPolicyWeekly", EmployeeDeductionPerPolicyWeekly),
                New SqlParameter("@EntityManagerDeductionPerPolicyMonthly", EntityManagerDeductionPerPolicyMonthly),
                New SqlParameter("@EntityManagerDeductionPerPolicyWeekly", EntityManagerDeductionPerPolicyWeekly),
                New SqlParameter("@DeductionPerPolicy_Value", DeductionPerPolicy_Value),
 New SqlParameter("@RemarkIsMandatoryManualEntryRequest", RemarkIsMandatoryManualEntryRequest
               ))


            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Public Function GetAll() As DataTable
            Dim objColl As DataTable
            objDac = DAC.getDAC()
            Try
                objColl = objDac.GetDataTable(APP_Settings_SelectAll, Nothing)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetHeader(ByVal FK_CompanyId As Integer) As DataTable
            Dim objColl As DataTable
            objDac = DAC.getDAC()
            Try
                objColl = objDac.GetDataTable(AppSetup, New SqlParameter("@FK_CompanyId", FK_CompanyId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function Update_Image(ByVal LogoPath As String, ByVal LogoImage As Byte()) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(App_Settings_UpdateImage, New SqlParameter("@LogoPath", LogoPath),
               New SqlParameter("@LogoImage", LogoImage))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal CompanyName1 As String, ByVal CompanyName2 As String, ByVal companyArabicName1 As String, ByVal CompanyArabicName2 As String, ByVal LogoImage As Byte(), ByVal LogoPath As String, ByVal EmployeeNoLength As Integer,
                               ByVal EmployeeCardLength As Integer, ByVal IsGradeOvertimeRule As Boolean, ByVal IsGradeTAException As Boolean, ByVal IsGradeAnnualLeave As Boolean, ByVal MinGapbetweenMoves As Integer, ByVal ManageOvertime As Integer,
                               ByVal ConsequenceTransactions As Integer, ByVal HRGroupEmail As String, ByVal HREmailNotification As Integer, ByVal LeaveApprovalfromLeave As Boolean, ByVal DynamicReportView As Integer, ByVal SendToManagerDaily As Boolean,
                               ByVal SendToManagerWeekly As Boolean, ByVal SendToManagerMonthly As Boolean, ByVal SendToHRDaily As Boolean, ByVal SendToHRWeekly As Boolean, ByVal SendToHRMonthly As Boolean, ByVal NoShiftShcedule As Integer, ByVal NursingDays As Integer,
                               ByVal DaysMinutes As Integer, ByVal HasFullDayPermission As Boolean, ByVal AllowChangeEmpNo As Integer, ByVal AllowedBefore As Integer, ByVal StudyPermissionSchedule As Integer, ByVal NursingPermissionAttend As Boolean,
                               ByVal PermApprovalFromPermission As Boolean, ByVal ManualEntryNo As Integer, ByVal MinOutViolation As Integer, ByVal ManagerEmailFormat As Integer, ByVal HREmailFormat As Integer, ByVal ManualEntryManagerLevelRequired As Integer,
                               ByVal HasEmailApproval As Boolean, ByVal ShowLoginForm As Boolean, ByVal ShowViolationCorrection As Boolean, ByVal FK_MaternityLeaveTypeId As Integer, ByVal FK_PersonalPermissionTypeId As Integer, ByVal NursingRequireMaternity As Boolean,
                               ByVal RequestGridToAppear As String, ByVal EarlyOutNotificationAfter As Integer, ByVal PermissionRequestReminderAfter As Integer, ByVal HasFlexiblePermission As Boolean, ByVal HasFlexibleNursingPermission As Boolean,
                               ByVal ShowAbsentInViolationCorrection As Boolean, ByVal HasPermissionForPeriod As Boolean, ByVal IsToConsiderBalanceInHours As Boolean, ByVal ShowThemeToUsers As Boolean, ByVal DurationTotalsToAppear As String,
                               ByVal AttachmentIsMandatory As Boolean, ByVal IsFirstGrid As Boolean,
                               ByVal DefaultStudyPermissionFromTime As Integer, ByVal DefaultStudyPermissionToTime As Integer, ByVal DefaultStudyPermissionFlexibleTime As Integer, ByVal ShowAnnouncement As Boolean, ByVal DivideTwoPermission As Boolean, ByVal MaternityLeaveDuration As Integer,
                               ByVal StudyPermissionApproval As Integer, ByVal NursingPermissionApproval As Integer, ByVal MaxStudyPermission As Integer, ByVal StudyGeneralGuide As String, ByVal StudyGeneralGuideAr As String, ByVal NursingGeneralGuide As String, ByVal NursingGeneralGuideAr As String,
                               ByVal AllowMoreOneManualEntry As Boolean, ByVal ShowAnnouncementSelfService As Boolean, ByVal OnCallSchedule As Integer, ByVal AllowNursingInRamadan As Boolean, ByVal ShowEmployeeList As Boolean,
                               ByVal MgrAbsentWeekly As Integer, ByVal MgrAbsentMonthly As Integer, ByVal MgrViolationWeekly As Integer, ByVal MgrViolationMonthly As Integer, ByVal HRAbsentWeekly As Integer,
                               ByVal HRAbsentMonthly As Integer, ByVal HRViolationWeekly As Integer, ByVal HRViolationMonthly As Integer, ByVal ConsiderAbsentOrLogicalAbsent As Integer, ByVal ReminderAbsentAfter As Integer,
                               ByVal MgrDailyWeekly As Integer, ByVal MgrDailyMonthly As Integer, ByVal MgrDetailedWeekly As Integer, ByVal MgrDetailedMonthly As Integer, ByVal MgrSummaryWeekly As Integer, ByVal MgrSummaryMonthly As Integer,
                               ByVal HRDailyWeekly As Integer, ByVal HRDailyMonthly As Integer, ByVal HRDetailedWeekly As Integer, ByVal HRDetailedMonthly As Integer, ByVal HRSummaryWeekly As Integer, ByVal HRSummaryMonthly As Integer, ByVal ConsiderAbsentEvenAttend As Boolean, ByVal DefaultReportFormat As Integer,
                               ByVal AutoPersonalPermissionDelay As Boolean, ByVal AutoPermissionDelayDuration As Integer, ByVal AutoPersonalPermissionEarlyOut As Boolean, ByVal AutoPermissionEarlyOutDuration As Integer, ByVal HasMultiApproval As Boolean, ByVal EmployeeManagerFilter As Integer,
                               ByVal FillCheckBoxList As Integer, ByVal IsDailyReportWithColor As Boolean, ByVal AllowDeleteSchedule As Boolean, ByVal DefaultTheme As String, ByVal ArchivingMonths As Integer,
                               ByVal ShowLGWithEntityPrivilege As Boolean, ByVal ManagerDefaultPage As Integer, ByVal ShowDirectStaffChk As Boolean, ByVal ApprovalRecalMethod As Integer, ByVal StudyPerm_NotificationException As Boolean,
                               ByVal NursingPerm_NotificationException As Boolean, ByVal AttachmentIsMandatoryManualEntryRequest As Boolean, ByVal AttachmentIsMandatoryHRManualEntry As Boolean, ByVal DailyReportDate As Integer, ByVal ExcludeGraceFromLostTime As Boolean,
                               ByVal EnableSemesterSelection_StudyPermission As Boolean, ByVal MustCompleteNoHours_RequestPermission As Boolean, ByVal NoOfHours_RequestPermission As Integer, ByVal IncludeConsiderInWorkPermissions_RequestPermission As Boolean, ByVal MonthlyDeduction_Report As Integer,
                               ByVal PasswordType As String, ByVal ShowLeaveLnk_ViolationCorrection As Boolean, ByVal ShowPermissionLnk_ViolationCorrection As Boolean, ByVal ConsiderLeaveOnOffDay As Integer, ByVal ViolationJustificationDays As String, ByVal ViolationJustificationDaysPolicy As String,
                               ByVal ManualEntryNoPerMonth As String, ByVal IsAbsentRestPolicy As Boolean, ByVal AllowEditManualEntryRequestDate As Boolean, ByVal AllowEditManualEntryRequestTime As Boolean, ByVal NumberInTransactionRequests As String, ByVal NumberOutTransactionRequests As String,
                               ByVal IsAutoApproveManualEntryRequest As Boolean, ByVal ConsiderRestInshiftSch As Integer, ByVal ConsiderNursingInRamadan As Boolean, ByVal RemoteWorkTAReason As String, ByVal HasTawajudFeatures As Boolean, ByVal HasMultiLocations As Boolean, ByVal HasHeartBeat As Boolean, ByVal HasFeedback As Boolean,
                               ByVal AllowFingerPunch As Boolean, ByVal AllowFingerLogin As Boolean, ByVal AllowFacePunch As Boolean, ByVal AllowFaceLogin As Boolean, ByVal EnableUniversitySelection_StudyPermission As Boolean, ByVal StudyAllowedAfterDays As Integer?,
                               ByVal StudyAllowedBeforeDays As Integer?, ByVal EnableMajorSelection_StudyPermission As Boolean?, ByVal RemarkIsMandatoryManualEntryRequest As Boolean) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(APP_Settings_Update,
               New SqlParameter("@CompanyName1", CompanyName1),
               New SqlParameter("@CompanyName2", CompanyName2),
               New SqlParameter("@companyArabicName1", companyArabicName1),
               New SqlParameter("@CompanyArabicName2", CompanyArabicName2),
               New SqlParameter("@LogoImage", LogoImage),
               New SqlParameter("@LogoPath", LogoPath),
               New SqlParameter("@EmployeeNoLength", EmployeeNoLength),
               New SqlParameter("@EmployeeCardLength", EmployeeCardLength),
               New SqlParameter("@IsGradeOvertimeRule", IsGradeOvertimeRule),
               New SqlParameter("@IsGradeTAException", IsGradeTAException),
               New SqlParameter("@IsGradeAnnualLeave", IsGradeAnnualLeave),
               New SqlParameter("@MinGapbetweenMoves", MinGapbetweenMoves),
               New SqlParameter("@ManageOvertime", ManageOvertime),
               New SqlParameter("@HRGroupEmail", HRGroupEmail),
               New SqlParameter("@HREmailNotification", HREmailNotification),
               New SqlParameter("@ConsequenceTransactions", ConsequenceTransactions),
               New SqlParameter("@LeaveApprovalfromLeave", LeaveApprovalfromLeave),
               New SqlParameter("@DynamicReportView", DynamicReportView),
               New SqlParameter("@SendToManagerDaily", SendToManagerDaily),
               New SqlParameter("@SendToManagerWeekly", SendToManagerWeekly),
               New SqlParameter("@SendToManagerMonthly", SendToManagerMonthly),
               New SqlParameter("@SendToHRDaily", SendToHRDaily),
               New SqlParameter("@SendToHRWeekly", SendToHRWeekly),
               New SqlParameter("@SendToHRMonthly", SendToHRMonthly),
               New SqlParameter("@NoShiftShcedule", NoShiftShcedule),
               New SqlParameter("@DaysMinutes", DaysMinutes),
               New SqlParameter("@NursingDays", NursingDays),
               New SqlParameter("@HasFullDayPermission", HasFullDayPermission),
               New SqlParameter("@AllowChangeEmpNo", AllowChangeEmpNo),
               New SqlParameter("@AllowedBefore", AllowedBefore),
               New SqlParameter("@StudyPermissionSchedule", StudyPermissionSchedule),
               New SqlParameter("@NursingPermissionAttend", NursingPermissionAttend),
               New SqlParameter("@PermApprovalFromPermission", PermApprovalFromPermission),
               New SqlParameter("@ManualEntryNo", ManualEntryNo),
               New SqlParameter("@MinOutViolation", MinOutViolation),
               New SqlParameter("@ManagerEmailFormat", ManagerEmailFormat),
               New SqlParameter("@HREmailFormat", HREmailFormat),
               New SqlParameter("@ManualEntryManagerLevelRequired", ManualEntryManagerLevelRequired),
               New SqlParameter("@HasEmailApproval", HasEmailApproval),
               New SqlParameter("@ShowLoginForm", ShowLoginForm),
               New SqlParameter("@ShowViolationCorrection", ShowViolationCorrection),
               New SqlParameter("@FK_MaternityLeaveTypeId", FK_MaternityLeaveTypeId),
               New SqlParameter("@FK_PersonalPermissionTypeId", FK_PersonalPermissionTypeId),
               New SqlParameter("@NursingRequireMaternity", NursingRequireMaternity),
               New SqlParameter("@RequestGridToAppear", RequestGridToAppear),
               New SqlParameter("@EarlyOutNotificationAfter", EarlyOutNotificationAfter),
               New SqlParameter("@PermissionRequestReminderAfter", PermissionRequestReminderAfter),
               New SqlParameter("@HasFlexiblePermission", HasFlexiblePermission),
               New SqlParameter("@HasFlexibleNursingPermission", HasFlexibleNursingPermission),
               New SqlParameter("@ShowAbsentInViolationCorrection", ShowAbsentInViolationCorrection),
               New SqlParameter("@HasPermissionForPeriod", HasPermissionForPeriod),
               New SqlParameter("@IsToConsiderBalanceInHours", IsToConsiderBalanceInHours),
               New SqlParameter("@ShowThemeToUsers", ShowThemeToUsers),
               New SqlParameter("@DurationTotalsToAppear", DurationTotalsToAppear),
               New SqlParameter("@AttachmentIsMandatory", AttachmentIsMandatory),
               New SqlParameter("@IsFirstGrid", IsFirstGrid),
               New SqlParameter("@DefaultStudyPermissionFromTime", DefaultStudyPermissionFromTime),
               New SqlParameter("@DefaultStudyPermissionToTime", DefaultStudyPermissionToTime),
               New SqlParameter("@DefaultStudyPermissionFlexibleTime", DefaultStudyPermissionFlexibleTime),
               New SqlParameter("@ShowAnnouncement", ShowAnnouncement),
               New SqlParameter("@DivideTwoPermission", DivideTwoPermission),
               New SqlParameter("@MaternityLeaveDuration", MaternityLeaveDuration),
               New SqlParameter("@StudyPermissionApproval", StudyPermissionApproval),
               New SqlParameter("@NursingPermissionApproval", NursingPermissionApproval),
               New SqlParameter("@MaxStudyPermission", MaxStudyPermission),
               New SqlParameter("@StudyGeneralGuide", StudyGeneralGuide),
               New SqlParameter("@StudyGeneralGuideAr", StudyGeneralGuideAr),
               New SqlParameter("@NursingGeneralGuide", NursingGeneralGuide),
               New SqlParameter("@NursingGeneralGuideAr", NursingGeneralGuideAr),
               New SqlParameter("@AllowMoreOneManualEntry", AllowMoreOneManualEntry),
               New SqlParameter("@ShowAnnouncementSelfService", ShowAnnouncementSelfService),
               New SqlParameter("@OnCallSchedule", OnCallSchedule),
               New SqlParameter("@AllowNursingInRamadan", AllowNursingInRamadan),
               New SqlParameter("@ShowEmployeeList", ShowEmployeeList),
               New SqlParameter("@MgrAbsentWeekly", MgrAbsentWeekly),
               New SqlParameter("@MgrAbsentMonthly", MgrAbsentMonthly),
               New SqlParameter("@MgrViolationWeekly", MgrViolationWeekly),
               New SqlParameter("@MgrViolationMonthly", MgrViolationMonthly),
               New SqlParameter("@HRAbsentWeekly", HRAbsentWeekly),
               New SqlParameter("@HRAbsentMonthly", HRAbsentMonthly),
               New SqlParameter("@HRViolationWeekly", HRViolationWeekly),
               New SqlParameter("@HRViolationMonthly", HRViolationMonthly),
               New SqlParameter("@ConsiderAbsentOrLogicalAbsent", ConsiderAbsentOrLogicalAbsent),
               New SqlParameter("@ReminderAbsentAfter", ReminderAbsentAfter),
               New SqlParameter("@MgrDailyWeekly", MgrDailyWeekly),
               New SqlParameter("@MgrDailyMonthly", MgrDailyMonthly),
               New SqlParameter("@MgrDetailedWeekly", MgrDetailedWeekly),
               New SqlParameter("@MgrDetailedMonthly", MgrDetailedMonthly),
               New SqlParameter("@MgrSummaryWeekly", MgrSummaryWeekly),
               New SqlParameter("@MgrSummaryMonthly", MgrSummaryMonthly),
               New SqlParameter("@HRDailyWeekly", HRDailyWeekly),
               New SqlParameter("@HRDailyMonthly", HRDailyMonthly),
               New SqlParameter("@HRDetailedWeekly", HRDetailedWeekly),
               New SqlParameter("@HRDetailedMonthly", HRDetailedMonthly),
               New SqlParameter("@HRSummaryWeekly", HRSummaryWeekly),
               New SqlParameter("@HRSummaryMonthly", HRSummaryMonthly),
               New SqlParameter("@ConsiderAbsentEvenAttend", ConsiderAbsentEvenAttend),
               New SqlParameter("@DefaultReportFormat", DefaultReportFormat),
               New SqlParameter("@AutoPersonalPermissionDelay", AutoPersonalPermissionDelay),
               New SqlParameter("@AutoPermissionDelayDuration", AutoPermissionDelayDuration),
               New SqlParameter("@AutoPersonalPermissionEarlyOut", AutoPersonalPermissionEarlyOut),
               New SqlParameter("@AutoPermissionEarlyOutDuration", AutoPermissionEarlyOutDuration),
               New SqlParameter("@HasMultiApproval", HasMultiApproval),
               New SqlParameter("@EmployeeManagerFilter", EmployeeManagerFilter),
               New SqlParameter("@FillCheckBoxList", FillCheckBoxList),
               New SqlParameter("@IsDailyReportWithColor", IsDailyReportWithColor),
               New SqlParameter("@AllowDeleteSchedule", AllowDeleteSchedule),
               New SqlParameter("@DefaultTheme", DefaultTheme),
               New SqlParameter("@ArchivingMonths", ArchivingMonths),
               New SqlParameter("@ShowLGWithEntityPrivilege", ShowLGWithEntityPrivilege),
               New SqlParameter("@ManagerDefaultPage", ManagerDefaultPage),
               New SqlParameter("@ShowDirectStaffChk", ShowDirectStaffChk),
               New SqlParameter("@ApprovalRecalMethod", ApprovalRecalMethod),
               New SqlParameter("@StudyPerm_NotificationException", StudyPerm_NotificationException),
               New SqlParameter("@NursingPerm_NotificationException", NursingPerm_NotificationException),
               New SqlParameter("@AttachmentIsMandatoryManualEntryRequest", AttachmentIsMandatoryManualEntryRequest),
               New SqlParameter("@AttachmentIsMandatoryHRManualEntry", AttachmentIsMandatoryHRManualEntry),
               New SqlParameter("@DailyReportDate", DailyReportDate),
               New SqlParameter("@ExcludeGraceFromLostTime", ExcludeGraceFromLostTime),
               New SqlParameter("@EnableSemesterSelection_StudyPermission", EnableSemesterSelection_StudyPermission),
               New SqlParameter("@MustCompleteNoHours_RequestPermission", MustCompleteNoHours_RequestPermission),
               New SqlParameter("@NoOfHours_RequestPermission", NoOfHours_RequestPermission),
               New SqlParameter("@IncludeConsiderInWorkPermissions_RequestPermission", IncludeConsiderInWorkPermissions_RequestPermission),
               New SqlParameter("@MonthlyDeduction_Report", MonthlyDeduction_Report),
               New SqlParameter("@PasswordType", PasswordType),
               New SqlParameter("@ShowLeaveLnk_ViolationCorrection", ShowLeaveLnk_ViolationCorrection),
               New SqlParameter("@ShowPermissionLnk_ViolationCorrection", ShowPermissionLnk_ViolationCorrection),
               New SqlParameter("@ConsiderLeaveOnOffDay", ConsiderLeaveOnOffDay),
               New SqlParameter("@ViolationJustificationDays", ViolationJustificationDays),
               New SqlParameter("@ViolationJustificationDaysPolicy", ViolationJustificationDaysPolicy),
               New SqlParameter("@ManualEntryNoPerMonth", ManualEntryNoPerMonth),
               New SqlParameter("@IsAbsentRestPolicy", IsAbsentRestPolicy),
               New SqlParameter("@AllowEditManualEntryRequestDate", AllowEditManualEntryRequestDate),
               New SqlParameter("@AllowEditManualEntryRequestTime", AllowEditManualEntryRequestTime),
               New SqlParameter("@NumberInTransactionRequests", NumberInTransactionRequests),
               New SqlParameter("@NumberOutTransactionRequests", NumberOutTransactionRequests),
               New SqlParameter("@IsAutoApproveManualEntryRequest", IsAutoApproveManualEntryRequest),
               New SqlParameter("@ConsiderRestInshiftSch", ConsiderRestInshiftSch),
               New SqlParameter("@ConsiderNursingInRamadan", ConsiderNursingInRamadan),
               New SqlParameter("@RemoteWorkTAReason", RemoteWorkTAReason),
               New SqlParameter("@HasTawajudFeatures", HasTawajudFeatures),
               New SqlParameter("@HasMultiLocations", HasMultiLocations),
               New SqlParameter("@HasHeartBeat", HasHeartBeat),
               New SqlParameter("@HasFeedback", HasFeedback),
               New SqlParameter("@AllowFingerPunch", AllowFingerPunch),
               New SqlParameter("@AllowFingerLogin", AllowFingerLogin),
               New SqlParameter("@AllowFacePunch", AllowFacePunch),
               New SqlParameter("@AllowFaceLogin", AllowFaceLogin),
               New SqlParameter("@EnableUniversitySelection_StudyPermission", EnableUniversitySelection_StudyPermission),
               New SqlParameter("@StudyAllowedAfterDays", StudyAllowedAfterDays),
               New SqlParameter("@StudyAllowedBeforeDays", StudyAllowedBeforeDays),
               New SqlParameter("@EnableMajorSelection_StudyPermission", EnableMajorSelection_StudyPermission),
 New SqlParameter("@RemarkIsMandatoryManualEntryRequest", RemarkIsMandatoryManualEntryRequest
               ))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Public Function Delete(ByVal CompanyName1 As String) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(APP_Settings_Delete,
               New SqlParameter("@CompanyName1", CompanyName1))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Public Function GetByPK() As DataRow
            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(APP_Settings_Select).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow
        End Function

        Public Function GetMobileFeatures() As DataTable
            Dim objColl As DataTable
            objDac = DAC.getDAC()
            Try
                objColl = objDac.GetDataTable(APP_Settings_Select_MobileFeatures, Nothing)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

#End Region

    End Class
End Namespace
