Imports Microsoft.VisualBasic

Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports TA.Lookup
Imports SmartV.DB
Imports SmartV.UTILITIES



Namespace TA.Definitions

    Public Class DALPermissionsTypes
        Inherits MGRBase

#Region "Class Variables"
        Private strConn As String
        Private PermissionsTypes_Select As String = "PermissionsTypes_select"
        Private PermissionsTypes_Select_All As String = "PermissionsTypes_select_All"
        Private PermissionsTypes_Insert As String = "PermissionsTypes_Insert"
        Private PermissionsTypes_Update As String = "PermissionsTypes_Update"
        Private PermissionsTypes_Delete As String = "PermissionsTypes_Delete"
        Private PermissionsTypes_Select_All_AllowedForSelfService As String = "PermissionsTypes_Select_All_AllowedForSelfService"
        Private PermissionsTypes_Select_All_AllowedForManagers As String = "PermissionsTypes_Select_All_AllowedForManagers"
        Public PermId As Integer
#End Region

#Region "Extended Class Variables"

        Private s As String = "PermissionsTypes_Select_AllInnerJOIN"
#End Region

#Region "Constructor"
        Public Sub New()

        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal PermName As String, ByVal PermArabicName As String, ByVal MinDuration As Integer,
                            ByVal MaxDuration As Integer, ByVal IsConsiderInWork As Boolean,
                            ByVal MonthlyBalance As Integer, ByVal AllowedOccurancePerPeriod As String,
                            ByVal allowedDurationPerPeriod As String, ByVal FK_LeaveIdDeductBalance As Integer,
                            ByVal ApprovalRequired As Boolean, ByVal FK_RelatedTAReason As Integer,
                            ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime,
                            ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime, ByVal GeneralGuide As String,
                            ByVal GeneralGuideAr As String, ByVal FK_LeaveIdtoallowduration As Integer, ByVal DurationAllowedwithleave As Integer, ByVal Isallowedaftertime As Boolean,
                            ByVal AllowedAfter As Integer, ByVal ShouldComplete50WHRS As Boolean, ByVal AllowedAfterDays As String, ByVal AllowedBeforeDays As String, ByVal ExcludeManagers_FromAfterBefore As Boolean,
                            ByVal AllowedForManagers As Boolean, ByVal AllowedForSelfService As Boolean, ByVal PermissionApproval As Integer, ByVal NotAllowedWhenHasStudyOrNursing As Boolean, ByVal ShowRemainingBalance As Boolean,
                            ByVal HasFlexiblePermission As Boolean, ByVal HasPermissionForPeriod As Boolean, ByVal HasFullDayPermission As Boolean, ByVal ConsiderRequestWithinBalance As Boolean,
                            ByVal AttachmentIsMandatory As Boolean, ByVal RemarksIsMandatory As Boolean, ByVal IsAllowedBeforeTime As Boolean,
                            ByVal AllowedBefore As Integer, ByVal AllowForSpecificEmployeeType As Boolean, ByVal FK_EmployeeTypeId As Integer,
                            ByVal IsSpecificCompany As Boolean, ByVal IsSpecificEntity As Boolean, ByVal MinDurationAllowedInSelfService As String,
                            ByVal IsAutoApprove As Boolean, ByVal AutoApproveAfter As String, ByVal Perm_NotificationException As Boolean,
                            ByVal AutoApprovePolicy As String, ByVal ConvertToLeave_ExceedDuration As Boolean, ByVal AnnualLeaveId_ToDeductPermission As Integer,
                            ByVal MustHaveTransaction As Boolean, ByVal DeductBalanceFromOvertime As Boolean, ByVal OvertimeBalanceDays As Integer,
                            ByVal ValidateDelayPermissions As Boolean, ByVal DelayPermissionsValidation As String, ByVal AllowWhenInSufficient As Boolean,
                            ByVal HasPermissionTimeControls As Boolean, ByVal PermissionRequestManagerLevelRequired As Integer) As Integer

            Dim sp1 As New SqlParameter("@PermissionId", SqlDbType.Int, 8, ParameterDirection.InputOutput, False, 0, 0, "", DataRowVersion.Default, 0)
            objDac = DAC.getDAC()
            Try
                'New SqlParameter("@AllowedOccurancePerPeriod", AllowedOccurancePerPeriod), _

                errNo = objDac.AddUpdateDeleteSPTrans(PermissionsTypes_Insert, New SqlParameter("@PermName", PermName),
                                                      sp1,
               New SqlParameter("@PermArabicName", PermArabicName),
               New SqlParameter("@MinDuration", MinDuration),
               New SqlParameter("@MaxDuration", MaxDuration),
               New SqlParameter("@IsConsiderInWork", IsConsiderInWork),
               New SqlParameter("@MonthlyBalance", MonthlyBalance),
               New SqlParameter("@FK_LeaveIdDeductBalance", IIf(FK_LeaveIdDeductBalance = 0, DBNull.Value, FK_LeaveIdDeductBalance)),
               New SqlParameter("@ApprovalRequired", ApprovalRequired),
               New SqlParameter("@FK_RelatedTAReason", FK_RelatedTAReason),
               New SqlParameter("@CREATED_BY", CREATED_BY),
               New SqlParameter("@CREATED_DATE", CREATED_DATE),
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY),
               New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE),
               New SqlParameter("@GeneralGuide", GeneralGuide),
               New SqlParameter("@FK_LeaveIdtoallowduration", IIf(FK_LeaveIdtoallowduration = -1, DBNull.Value, FK_LeaveIdtoallowduration)),
               New SqlParameter("@DurationAllowedwithleave", DurationAllowedwithleave),
               New SqlParameter("@GeneralGuideAr", GeneralGuideAr),
               New SqlParameter("@Isallowedaftertime", Isallowedaftertime),
               New SqlParameter("@AllowedAfter", AllowedAfter),
               New SqlParameter("@ShouldComplete50WHRS", ShouldComplete50WHRS),
               New SqlParameter("@AllowedAfterDays", AllowedAfterDays),
               New SqlParameter("@AllowedBeforeDays", AllowedBeforeDays),
               New SqlParameter("@ExcludeManagers_FromAfterBefore", ExcludeManagers_FromAfterBefore),
               New SqlParameter("@AllowedForManagers", AllowedForManagers),
               New SqlParameter("@AllowedForSelfService", AllowedForSelfService),
               New SqlParameter("@PermissionApproval", PermissionApproval),
               New SqlParameter("@NotAllowedWhenHasStudyOrNursing", NotAllowedWhenHasStudyOrNursing),
               New SqlParameter("@ShowRemainingBalance", ShowRemainingBalance),
               New SqlParameter("@HasFlexiblePermission", HasFlexiblePermission),
               New SqlParameter("@HasPermissionForPeriod", HasPermissionForPeriod),
               New SqlParameter("@HasFullDayPermission", HasFullDayPermission),
               New SqlParameter("@ConsiderRequestWithinBalance", ConsiderRequestWithinBalance),
               New SqlParameter("@AttachmentIsMandatory", AttachmentIsMandatory),
               New SqlParameter("@RemarksIsMandatory", RemarksIsMandatory),
               New SqlParameter("@IsAllowedBeforeTime", IsAllowedBeforeTime),
               New SqlParameter("@AllowedBefore", AllowedBefore),
               New SqlParameter("@AllowForSpecificEmployeeType", AllowForSpecificEmployeeType),
               New SqlParameter("@FK_EmployeeTypeId", FK_EmployeeTypeId),
               New SqlParameter("@IsSpecificCompany", IsSpecificCompany),
               New SqlParameter("@IsSpecificEntity", IsSpecificEntity),
               New SqlParameter("@MinDurationAllowedInSelfService", MinDurationAllowedInSelfService),
               New SqlParameter("@IsAutoApprove", IsAutoApprove),
               New SqlParameter("@AutoApproveAfter", AutoApproveAfter),
               New SqlParameter("@Perm_NotificationException", Perm_NotificationException),
               New SqlParameter("@AutoApprovePolicy", AutoApprovePolicy),
               New SqlParameter("@ConvertToLeave_ExceedDuration", ConvertToLeave_ExceedDuration),
               New SqlParameter("@AnnualLeaveId_ToDeductPermission", AnnualLeaveId_ToDeductPermission),
               New SqlParameter("@MustHaveTransaction", MustHaveTransaction),
               New SqlParameter("@DeductBalanceFromOvertime", DeductBalanceFromOvertime),
               New SqlParameter("@OvertimeBalanceDays", OvertimeBalanceDays),
               New SqlParameter("@ValidateDelayPermissions", ValidateDelayPermissions),
               New SqlParameter("@DelayPermissionsValidation", DelayPermissionsValidation),
               New SqlParameter("@AllowWhenInSufficient", AllowWhenInSufficient),
               New SqlParameter("@HasPermissionTimeControls", HasPermissionTimeControls),
               New SqlParameter("@PermissionRequestManagerLevelRequired", PermissionRequestManagerLevelRequired))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try

            PermId = sp1.Value
            Return errNo

        End Function

        Public Function Update(ByVal PermId As Integer, ByVal PermName As String, ByVal PermArabicName As String, ByVal MinDuration As Integer,
                               ByVal MaxDuration As Integer, ByVal IsConsiderInWork As Boolean, ByVal MonthlyBalance As Integer, ByVal AllowedOccurancePerPeriod As String,
                               ByVal allowedDurationPerPeriod As String,
                               ByVal FK_LeaveIdDeductBalance As Integer, ByVal ApprovalRequired As Boolean, ByVal FK_RelatedTAReason As Integer,
                               ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime,
                               ByVal GeneralGuide As String, ByVal GeneralGuideAr As String, ByVal FK_LeaveIdtoallowduration As Integer,
                               ByVal DurationAllowedwithleave As Integer, ByVal Isallowedaftertime As Boolean, ByVal AllowedAfter As Integer,
                               ByVal ShouldComplete50WHRS As Boolean, ByVal AllowedAfterDays As String, ByVal AllowedBeforeDays As String,
                               ByVal ExcludeManagers_FromAfterBefore As Boolean,
                               ByVal AllowedForManagers As Boolean, ByVal AllowedForSelfService As Boolean, ByVal PermissionApproval As Integer, ByVal NotAllowedWhenHasStudyOrNursing As Boolean, ByVal ShowRemainingBalance As Boolean,
                               ByVal HasFlexiblePermission As Boolean, ByVal HasPermissionForPeriod As Boolean, ByVal HasFullDayPermission As Boolean, ByVal ConsiderRequestWithinBalance As Boolean,
                               ByVal AttachmentIsMandatory As Boolean, ByVal RemarksIsMandatory As Boolean, ByVal IsAllowedBeforeTime As Boolean, ByVal AllowedBefore As Integer, ByVal AllowForSpecificEmployeeType As Boolean, ByVal FK_EmployeeTypeId As Integer,
                               ByVal IsSpecificCompany As Boolean, ByVal IsSpecificEntity As Boolean, ByVal MinDurationAllowedInSelfService As String,
                               ByVal IsAutoApprove As Boolean, ByVal AutoApproveAfter As String, ByVal Perm_NotificationException As Boolean, ByVal AutoApprovePolicy As String,
                               ByVal ConvertToLeave_ExceedDuration As Boolean, ByVal AnnualLeaveId_ToDeductPermission As Integer, ByVal MustHaveTransaction As Boolean,
                               ByVal DeductBalanceFromOvertime As Boolean, ByVal OvertimeBalanceDays As Integer, ByVal ValidateDelayPermissions As Boolean,
                               ByVal DelayPermissionsValidation As String, ByVal AllowWhenInSufficient As Boolean, ByVal HasPermissionTimeControls As Boolean,
                               ByVal PermissionRequestManagerLevelRequired As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(PermissionsTypes_Update, New SqlParameter("@PermId", PermId),
               New SqlParameter("@PermName", PermName),
               New SqlParameter("@PermArabicName", PermArabicName),
               New SqlParameter("@MinDuration", MinDuration),
               New SqlParameter("@MaxDuration", MaxDuration),
               New SqlParameter("@IsConsiderInWork", IsConsiderInWork),
               New SqlParameter("@MonthlyBalance", MonthlyBalance),
               New SqlParameter("@FK_LeaveIdDeductBalance", IIf(FK_LeaveIdDeductBalance = 0, DBNull.Value, FK_LeaveIdDeductBalance)),
               New SqlParameter("@ApprovalRequired", ApprovalRequired),
               New SqlParameter("@FK_RelatedTAReason", FK_RelatedTAReason),
               New SqlParameter("@CREATED_BY", CREATED_BY),
               New SqlParameter("@CREATED_DATE", CREATED_DATE),
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY),
               New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE),
               New SqlParameter("@GeneralGuide", GeneralGuide),
               New SqlParameter("@FK_LeaveIdtoallowduration", IIf(FK_LeaveIdtoallowduration = -1, DBNull.Value, FK_LeaveIdtoallowduration)),
               New SqlParameter("@DurationAllowedwithleave", DurationAllowedwithleave),
               New SqlParameter("@GeneralGuideAr", GeneralGuideAr),
               New SqlParameter("@Isallowedaftertime", Isallowedaftertime),
               New SqlParameter("@AllowedAfter", AllowedAfter),
               New SqlParameter("@ShouldComplete50WHRS", ShouldComplete50WHRS),
               New SqlParameter("@AllowedAfterDays", AllowedAfterDays),
               New SqlParameter("@AllowedBeforeDays", AllowedBeforeDays),
               New SqlParameter("@ExcludeManagers_FromAfterBefore", ExcludeManagers_FromAfterBefore),
               New SqlParameter("@AllowedForManagers", AllowedForManagers),
               New SqlParameter("@AllowedForSelfService", AllowedForSelfService),
               New SqlParameter("@PermissionApproval", PermissionApproval),
               New SqlParameter("@NotAllowedWhenHasStudyOrNursing", NotAllowedWhenHasStudyOrNursing),
               New SqlParameter("@ShowRemainingBalance", ShowRemainingBalance),
               New SqlParameter("@HasFlexiblePermission", HasFlexiblePermission),
               New SqlParameter("@HasPermissionForPeriod", HasPermissionForPeriod),
               New SqlParameter("@HasFullDayPermission", HasFullDayPermission),
               New SqlParameter("@ConsiderRequestWithinBalance", ConsiderRequestWithinBalance),
               New SqlParameter("@AttachmentIsMandatory", AttachmentIsMandatory),
               New SqlParameter("@RemarksIsMandatory", RemarksIsMandatory),
               New SqlParameter("@IsAllowedBeforeTime", IsAllowedBeforeTime),
               New SqlParameter("@AllowedBefore", AllowedBefore),
               New SqlParameter("@AllowForSpecificEmployeeType", AllowForSpecificEmployeeType),
               New SqlParameter("@FK_EmployeeTypeId", FK_EmployeeTypeId),
               New SqlParameter("@IsSpecificCompany", IsSpecificCompany),
               New SqlParameter("@IsSpecificEntity", IsSpecificEntity),
               New SqlParameter("@MinDurationAllowedInSelfService", MinDurationAllowedInSelfService),
               New SqlParameter("@IsAutoApprove", IsAutoApprove),
               New SqlParameter("@AutoApproveAfter", AutoApproveAfter),
               New SqlParameter("@Perm_NotificationException", Perm_NotificationException),
               New SqlParameter("@AutoApprovePolicy", AutoApprovePolicy),
               New SqlParameter("@ConvertToLeave_ExceedDuration", ConvertToLeave_ExceedDuration),
               New SqlParameter("@AnnualLeaveId_ToDeductPermission", AnnualLeaveId_ToDeductPermission),
               New SqlParameter("@MustHaveTransaction", MustHaveTransaction),
               New SqlParameter("@DeductBalanceFromOvertime", DeductBalanceFromOvertime),
               New SqlParameter("@OvertimeBalanceDays", OvertimeBalanceDays),
               New SqlParameter("@ValidateDelayPermissions", ValidateDelayPermissions),
               New SqlParameter("@DelayPermissionsValidation", DelayPermissionsValidation),
               New SqlParameter("@AllowWhenInSufficient", AllowWhenInSufficient),
               New SqlParameter("@HasPermissionTimeControls", HasPermissionTimeControls),
               New SqlParameter("@PermissionRequestManagerLevelRequired", PermissionRequestManagerLevelRequired))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal PermId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(PermissionsTypes_Delete, New SqlParameter("@PermId", PermId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal PermId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(PermissionsTypes_Select, New SqlParameter("@PermId", PermId)).Rows(0)
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
                objColl = objDac.GetDataTable(PermissionsTypes_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAllowed_ForSelfService(ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer, ByVal FK_EmployeeTypeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(PermissionsTypes_Select_All_AllowedForSelfService, New SqlParameter("@FK_CompanyId", FK_CompanyId),
                                              New SqlParameter("@FK_EntityId", FK_EntityId),
                                New SqlParameter("@FK_EmployeeTypeId", FK_EmployeeTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAllowed_ForManagers(ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer, ByVal FK_EmployeeTypeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(PermissionsTypes_Select_All_AllowedForManagers, New SqlParameter("@FK_CompanyId", FK_CompanyId),
                                              New SqlParameter("@FK_EntityId", FK_EntityId),
                                                New SqlParameter("@FK_EmployeeTypeId", FK_EmployeeTypeId))
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
                objColl = objDac.GetDataTable(PermissionsTypes_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

#End Region

    End Class
End Namespace