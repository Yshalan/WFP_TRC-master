Imports Microsoft.VisualBasic

Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports TA.LookUp
Imports SmartV.DB
Imports SmartV.UTILITIES



Namespace TA.Definitions

    Public Class DALLeavesTypes
        Inherits MGRBase

#Region "Class Variables"
        Private strConn As String
        Private LeavesTypes_Select As String = "LeavesTypes_select"
        Private LeavesTypes_Select_All As String = "LeavesTypes_select_All"
        Private LeavesTypes_Insert As String = "LeavesTypes_Insert"
        Private LeavesTypes_Update As String = "LeavesTypes_Update"
        Private LeavesTypes_Delete As String = "LeavesTypes_Delete"
        Private LeavesTypes_Select_AllForDDL As String = "LeavesTypes_Select_AllForDDL"
        Private LeavesTypes_SelectByTypes As String = "LeavesTypes_SelectByTypes"
        Private LeavesTypes_Select_AllowedForSelfService As String = "LeavesTypes_Select_AllowedForSelfService"
        Private LeavesTypes_Select_All_Annual As String = "LeavesTypes_Select_All_Annual"

        Public LeaveId As Integer
#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal LeaveName As String, ByVal LeaveArabicName As String, ByVal Balance As Double, ByVal MonthlyBalancing As Boolean, ByVal PaymentConsidration As Integer, ByVal MinDuration As Integer, ByVal MaxDuration As Integer, ByVal MinServiceDays As Integer, ByVal ExcludeOffDays As Boolean, ByVal ExcludeHolidays As Boolean, ByVal MaxRoundBalance As Integer, ByVal ExpiredBalanceIsCashed As Boolean, ByVal AllowIfBalanceOver As Boolean, ByVal MaxOccurancePerPeriod As String, ByVal IsAnnual As Boolean, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime, ByVal LeaveApproval As Integer, ByVal GeneralGuide As String, ByVal FK_ParentLeaveType As Integer, ByVal GeneralGuideAr As String, ByVal AllowedForSelfService As Boolean, ByVal ShowRemainingBalance As Boolean, ByVal IsSpecificGrade As Boolean, ByVal Leave_NotificationException As Boolean, ByVal AttachmentIsMandatory As Boolean, ByVal RemarksIsMandatory As Boolean, ByVal AllowedGender As Integer, ByVal AllowForSpecificEmployeeType As Boolean, ByVal FK_EmployeeTypeId As Integer, ByVal LeaveRequestManagerLevelRequired As Integer, ByVal ValidateLeavesBeforeRestDays As Boolean, ByVal LeaveCode As String, ByVal AllowedAfterDays As String, ByVal AllowedBeforeDays As String, ByVal ApprovalRequired As Boolean, ByVal IsAutoApprove As Boolean, ByVal AutoApproveAfter As String, ByVal AutoApprovePolicy As String, ByVal BalanceConsideration As Integer, ByVal MinLeaveApplyDay As String) As Integer
            Dim sp1 As New SqlParameter("@LeaveId", SqlDbType.Int, 8, ParameterDirection.InputOutput, False, 0, 0, "", DataRowVersion.Default, 0)
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(LeavesTypes_Insert, New SqlParameter("@LeaveName", LeaveName),
                                                      sp1,
               New SqlParameter("@LeaveArabicName", LeaveArabicName),
               New SqlParameter("@Balance", Balance),
               New SqlParameter("@MonthlyBalancing", MonthlyBalancing),
               New SqlParameter("@PaymentConsidration", PaymentConsidration),
               New SqlParameter("@MinDuration", MinDuration),
               New SqlParameter("@MaxDuration", MaxDuration),
               New SqlParameter("@MinServiceDays", MinServiceDays),
               New SqlParameter("@ExcludeOffDays", ExcludeOffDays),
               New SqlParameter("@ExcludeHolidays", ExcludeHolidays),
               New SqlParameter("@MaxRoundBalance", MaxRoundBalance),
               New SqlParameter("@ExpiredBalanceIsCashed", ExpiredBalanceIsCashed),
               New SqlParameter("@AllowIfBalanceOver", AllowIfBalanceOver),
               New SqlParameter("@MaxOccurancePerPeriod", MaxOccurancePerPeriod),
               New SqlParameter("@IsAnnual", IsAnnual),
               New SqlParameter("@CREATED_BY", CREATED_BY),
               New SqlParameter("@CREATED_DATE", CREATED_DATE),
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY),
               New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE),
               New SqlParameter("@LeaveApproval", LeaveApproval),
               New SqlParameter("@GeneralGuide", GeneralGuide),
               New SqlParameter("@FK_ParentLeaveType", IIf(FK_ParentLeaveType = 0, DBNull.Value, FK_ParentLeaveType)),
               New SqlParameter("@GeneralGuideAr", GeneralGuideAr),
               New SqlParameter("@AllowedForSelfService", AllowedForSelfService),
               New SqlParameter("@ShowRemainingBalance", ShowRemainingBalance),
               New SqlParameter("@IsSpecificGrade", IsSpecificGrade),
               New SqlParameter("@Leave_NotificationException", Leave_NotificationException),
               New SqlParameter("@AttachmentIsMandatory", AttachmentIsMandatory),
               New SqlParameter("@RemarksIsMandatory", RemarksIsMandatory),
               New SqlParameter("@AllowedGender", AllowedGender),
               New SqlParameter("@AllowForSpecificEmployeeType", AllowForSpecificEmployeeType),
               New SqlParameter("@FK_EmployeeTypeId", FK_EmployeeTypeId),
               New SqlParameter("@LeaveRequestManagerLevelRequired", LeaveRequestManagerLevelRequired),
               New SqlParameter("@ValidateLeavesBeforeRestDays", ValidateLeavesBeforeRestDays),
               New SqlParameter("@LeaveCode", LeaveCode),
               New SqlParameter("@AllowedAfterDays", AllowedAfterDays),
               New SqlParameter("@AllowedBeforeDays", AllowedBeforeDays),
               New SqlParameter("@ApprovalRequired", ApprovalRequired),
               New SqlParameter("@IsAutoApprove", IsAutoApprove),
               New SqlParameter("@AutoApproveAfter", AutoApproveAfter),
               New SqlParameter("@AutoApprovePolicy", AutoApprovePolicy),
               New SqlParameter("@BalanceConsideration", BalanceConsideration),
               New SqlParameter("@MinLeaveApplyDay", MinLeaveApplyDay))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try

            LeaveId = sp1.Value

            Return errNo

        End Function

        Public Function Update(ByVal LeaveId As Integer, ByVal LeaveName As String, ByVal LeaveArabicName As String, ByVal Balance As Double, ByVal MonthlyBalancing As Boolean, ByVal PaymentConsidration As Integer, ByVal MinDuration As Integer, ByVal MaxDuration As Integer, ByVal MinServiceDays As Integer, ByVal ExcludeOffDays As Boolean, ByVal ExcludeHolidays As Boolean, ByVal MaxRoundBalance As Integer, ByVal ExpiredBalanceIsCashed As Boolean, ByVal AllowIfBalanceOver As Boolean, ByVal MaxOccurancePerPeriod As String, ByVal IsAnnual As Boolean, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime, ByVal LeaveApproval As Integer, ByVal GeneralGuide As String, ByVal FK_ParentLeaveType As Integer, ByVal GeneralGuideAr As String, ByVal AllowedForSelfService As Integer, ByVal ShowRemainingBalance As Boolean, ByVal IsSpecificGrade As Boolean, ByVal Leave_NotificationException As Boolean, ByVal AttachmentIsMandatory As Boolean, ByVal RemarksIsMandatory As Boolean, ByVal AllowedGender As Integer, ByVal AllowForSpecificEmployeeType As Boolean, ByVal FK_EmployeeTypeId As Integer, ByVal LeaveRequestManagerLevelRequired As Integer, ByVal ValidateLeavesBeforeRestDays As Boolean, ByVal LeaveCode As String, ByVal AllowedAfterDays As String, ByVal AllowedBeforeDays As String, ByVal ApprovalRequired As Boolean, ByVal IsAutoApprove As Boolean, ByVal AutoApproveAfter As String, ByVal AutoApprovePolicy As String, ByVal BalanceConsideration As Integer, ByVal MinLeaveApplyDay As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(LeavesTypes_Update, New SqlParameter("@LeaveId", LeaveId),
               New SqlParameter("@LeaveName", LeaveName),
               New SqlParameter("@LeaveArabicName", LeaveArabicName),
               New SqlParameter("@Balance", Balance),
               New SqlParameter("@MonthlyBalancing", MonthlyBalancing),
               New SqlParameter("@PaymentConsidration", PaymentConsidration),
               New SqlParameter("@MinDuration", MinDuration),
               New SqlParameter("@MaxDuration", MaxDuration),
               New SqlParameter("@MinServiceDays", MinServiceDays),
               New SqlParameter("@ExcludeOffDays", ExcludeOffDays),
               New SqlParameter("@ExcludeHolidays", ExcludeHolidays),
               New SqlParameter("@MaxRoundBalance", MaxRoundBalance),
               New SqlParameter("@ExpiredBalanceIsCashed", ExpiredBalanceIsCashed),
               New SqlParameter("@AllowIfBalanceOver", AllowIfBalanceOver),
               New SqlParameter("@MaxOccurancePerPeriod", MaxOccurancePerPeriod),
               New SqlParameter("@IsAnnual", IsAnnual),
               New SqlParameter("@CREATED_BY", CREATED_BY),
               New SqlParameter("@CREATED_DATE", CREATED_DATE),
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY),
               New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE),
               New SqlParameter("@LeaveApproval", LeaveApproval),
               New SqlParameter("@GeneralGuide", GeneralGuide),
               New SqlParameter("@FK_ParentLeaveType", IIf(FK_ParentLeaveType = 0, DBNull.Value, FK_ParentLeaveType)),
               New SqlParameter("@GeneralGuideAr", GeneralGuideAr),
               New SqlParameter("@AllowedForSelfService", AllowedForSelfService),
               New SqlParameter("@ShowRemainingBalance", ShowRemainingBalance),
               New SqlParameter("@IsSpecificGrade", IsSpecificGrade),
               New SqlParameter("@Leave_NotificationException", Leave_NotificationException),
               New SqlParameter("@AttachmentIsMandatory", AttachmentIsMandatory),
               New SqlParameter("@RemarksIsMandatory", RemarksIsMandatory),
               New SqlParameter("@AllowedGender", AllowedGender),
               New SqlParameter("@AllowForSpecificEmployeeType", AllowForSpecificEmployeeType),
               New SqlParameter("@FK_EmployeeTypeId", FK_EmployeeTypeId),
               New SqlParameter("@LeaveRequestManagerLevelRequired", LeaveRequestManagerLevelRequired),
               New SqlParameter("@ValidateLeavesBeforeRestDays", ValidateLeavesBeforeRestDays),
               New SqlParameter("@LeaveCode", LeaveCode),
               New SqlParameter("@AllowedAfterDays", AllowedAfterDays),
               New SqlParameter("@AllowedBeforeDays", AllowedBeforeDays),
               New SqlParameter("@ApprovalRequired", ApprovalRequired),
               New SqlParameter("@IsAutoApprove", IsAutoApprove),
               New SqlParameter("@AutoApproveAfter", AutoApproveAfter),
               New SqlParameter("@AutoApprovePolicy", AutoApprovePolicy),
               New SqlParameter("@BalanceConsideration", BalanceConsideration),
               New SqlParameter("@MinLeaveApplyDay", MinLeaveApplyDay))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal LeaveId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(LeavesTypes_Delete, New SqlParameter("@LeaveId", LeaveId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal LeaveId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(LeavesTypes_Select, New SqlParameter("@LeaveId", LeaveId)).Rows(0)
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
                objColl = objDac.GetDataTable(LeavesTypes_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetAll_Annual() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(LeavesTypes_Select_All_Annual, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetAllByLeaveTypes(ByVal Query As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(LeavesTypes_SelectByTypes, New SqlParameter("@Query", Query))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetAllForDDL() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(LeavesTypes_Select_AllForDDL, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetAllowed_ForSelfService() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(LeavesTypes_Select_AllowedForSelfService, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function
#End Region

    End Class
End Namespace