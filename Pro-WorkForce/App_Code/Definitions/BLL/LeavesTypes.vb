Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Definitions

    Public Class LeavesTypes

#Region "Class Variables"

        Private _LeaveId As Integer
        Private _LeaveName As String
        Private _LeaveArabicName As String
        Private _Balance As Double
        Private _MonthlyBalancing As Boolean
        Private _PaymentConsidration As Integer
        Private _MinDuration As Integer
        Private _MaxDuration As Integer
        Private _MinServiceDays As Integer
        Private _ExcludeOffDays As Boolean
        Private _ExcludeHolidays As Boolean
        Private _MaxRoundBalance As Integer
        Private _ExpiredBalanceIsCashed As Boolean
        Private _AllowIfBalanceOver As Boolean
        Private _MaxOccurancePerPeriod As String
        Private _IsAnnual As Boolean
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LeaveApproval As Integer
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALLeavesTypes As DALLeavesTypes
        Private _GeneralGuide As String
        Private _FK_ParentLeaveType As Integer
        Private _GeneralGuideAr As String
        Private _AllowedForSelfService As Boolean
        Private _ShowRemainingBalance As Boolean
        Private _IsSpecificGrade As Boolean
        Private _Leave_NotificationException As Boolean
        Private _AttachmentIsMandatory As Boolean
        Private _RemarksIsMandatory As Boolean
        Private _AllowedGender As Integer
        Private _AllowForSpecificEmployeeType As Boolean
        Private _FK_EmployeeTypeId As Integer
        Private _LeaveRequestManagerLevelRequired As Integer
        Private _ValidateLeavesBeforeRestDays As Boolean
        Private _LeaveCode As String
        Private _AllowedAfterDays As String
        Private _AllowedBeforeDays As String
        Private _ApprovalRequired As Boolean
        Private _IsAutoApprove As Boolean
        Private _AutoApproveAfter As String
        Private _AutoApprovePolicy As String
        Private _BalanceConsideration As Integer
        Private _MinLeaveApplyDay As String

#End Region

#Region "Public Properties"


        Public Property LeaveId() As Integer
            Set(ByVal value As Integer)
                _LeaveId = value
            End Set
            Get
                Return (_LeaveId)
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

        Public Property Balance() As Double
            Set(ByVal value As Double)
                _Balance = value
            End Set
            Get
                Return (_Balance)
            End Get
        End Property

        Public Property MonthlyBalancing() As Boolean
            Set(ByVal value As Boolean)
                _MonthlyBalancing = value
            End Set
            Get
                Return (_MonthlyBalancing)
            End Get
        End Property

        Public Property PaymentConsidration() As Integer
            Set(ByVal value As Integer)
                _PaymentConsidration = value
            End Set
            Get
                Return (_PaymentConsidration)
            End Get
        End Property

        Public Property MinDuration() As Integer
            Set(ByVal value As Integer)
                _MinDuration = value
            End Set
            Get
                Return (_MinDuration)
            End Get
        End Property

        Public Property MaxDuration() As Integer
            Set(ByVal value As Integer)
                _MaxDuration = value
            End Set
            Get
                Return (_MaxDuration)
            End Get
        End Property

        Public Property MinServiceDays() As Integer
            Set(ByVal value As Integer)
                _MinServiceDays = value
            End Set
            Get
                Return (_MinServiceDays)
            End Get
        End Property

        Public Property ExcludeOffDays() As Boolean
            Set(ByVal value As Boolean)
                _ExcludeOffDays = value
            End Set
            Get
                Return (_ExcludeOffDays)
            End Get
        End Property

        Public Property ExcludeHolidays() As Boolean
            Set(ByVal value As Boolean)
                _ExcludeHolidays = value
            End Set
            Get
                Return (_ExcludeHolidays)
            End Get
        End Property

        Public Property MaxRoundBalance() As Integer
            Set(ByVal value As Integer)
                _MaxRoundBalance = value
            End Set
            Get
                Return (_MaxRoundBalance)
            End Get
        End Property

        Public Property ExpiredBalanceIsCashed() As Boolean
            Set(ByVal value As Boolean)
                _ExpiredBalanceIsCashed = value
            End Set
            Get
                Return (_ExpiredBalanceIsCashed)
            End Get
        End Property

        Public Property AllowIfBalanceOver() As Boolean
            Set(ByVal value As Boolean)
                _AllowIfBalanceOver = value
            End Set
            Get
                Return (_AllowIfBalanceOver)
            End Get
        End Property

        Public Property MaxOccurancePerPeriod() As String
            Set(ByVal value As String)
                _MaxOccurancePerPeriod = value
            End Set
            Get
                Return (_MaxOccurancePerPeriod)
            End Get
        End Property

        Public Property IsAnnual() As Boolean
            Set(ByVal value As Boolean)
                _IsAnnual = value
            End Set
            Get
                Return (_IsAnnual)
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

        Public Property LeaveApproval() As Integer
            Get
                Return _LeaveApproval
            End Get
            Set(ByVal value As Integer)
                _LeaveApproval = value
            End Set
        End Property

        Public Property GeneralGuide() As String
            Set(ByVal value As String)
                _GeneralGuide = value
            End Set
            Get
                Return (_GeneralGuide)
            End Get
        End Property

        Public Property FK_ParentLeaveType() As Integer
            Get
                Return _FK_ParentLeaveType
            End Get
            Set(ByVal value As Integer)
                _FK_ParentLeaveType = value
            End Set
        End Property

        Public Property GeneralGuideAr() As String
            Set(ByVal value As String)
                _GeneralGuideAr = value
            End Set
            Get
                Return (_GeneralGuideAr)
            End Get
        End Property

        Public Property AllowedForSelfService() As Boolean
            Set(ByVal value As Boolean)
                _AllowedForSelfService = value
            End Set
            Get
                Return (_AllowedForSelfService)
            End Get
        End Property

        Public Property ShowRemainingBalance() As Boolean
            Get
                Return _ShowRemainingBalance
            End Get
            Set(ByVal value As Boolean)
                _ShowRemainingBalance = value
            End Set
        End Property

        Public Property IsSpecificGrade() As Boolean
            Get
                Return _IsSpecificGrade
            End Get
            Set(ByVal value As Boolean)
                _IsSpecificGrade = value
            End Set
        End Property

        Public Property Leave_NotificationException() As Boolean
            Get
                Return _Leave_NotificationException
            End Get
            Set(ByVal value As Boolean)
                _Leave_NotificationException = value
            End Set
        End Property

        Public Property AttachmentIsMandatory() As Boolean
            Get
                Return _AttachmentIsMandatory
            End Get
            Set(ByVal value As Boolean)
                _AttachmentIsMandatory = value
            End Set
        End Property

        Public Property RemarksIsMandatory() As Boolean
            Get
                Return _RemarksIsMandatory
            End Get
            Set(ByVal value As Boolean)
                _RemarksIsMandatory = value
            End Set
        End Property

        Public Property AllowedGender() As Integer
            Get
                Return _AllowedGender
            End Get
            Set(ByVal value As Integer)
                _AllowedGender = value
            End Set
        End Property

        Public Property AllowForSpecificEmployeeType() As Boolean
            Get
                Return _AllowForSpecificEmployeeType
            End Get
            Set(ByVal value As Boolean)
                _AllowForSpecificEmployeeType = value
            End Set
        End Property

        Public Property FK_EmployeeTypeId() As Integer
            Get
                Return _FK_EmployeeTypeId
            End Get
            Set(ByVal value As Integer)
                _FK_EmployeeTypeId = value
            End Set
        End Property

        Public Property LeaveRequestManagerLevelRequired() As Integer
            Get
                Return _LeaveRequestManagerLevelRequired
            End Get
            Set(ByVal value As Integer)
                _LeaveRequestManagerLevelRequired = value
            End Set
        End Property

        Public Property ValidateLeavesBeforeRestDays() As Boolean
            Get
                Return _ValidateLeavesBeforeRestDays
            End Get
            Set(ByVal value As Boolean)
                _ValidateLeavesBeforeRestDays = value
            End Set
        End Property

        Public Property LeaveCode() As String
            Get
                Return _LeaveCode
            End Get
            Set(ByVal value As String)
                _LeaveCode = value
            End Set
        End Property

        Public Property AllowedAfterDays() As String
            Get
                Return _AllowedAfterDays
            End Get
            Set(ByVal value As String)
                _AllowedAfterDays = value
            End Set
        End Property

        Public Property AllowedBeforeDays() As String
            Get
                Return _AllowedBeforeDays
            End Get
            Set(ByVal value As String)
                _AllowedBeforeDays = value
            End Set
        End Property

        Public Property ApprovalRequired() As Boolean
            Get
                Return _ApprovalRequired
            End Get
            Set(ByVal value As Boolean)
                _ApprovalRequired = value
            End Set
        End Property

        Public Property IsAutoApprove() As Boolean
            Get
                Return _IsAutoApprove
            End Get
            Set(ByVal value As Boolean)
                _IsAutoApprove = value
            End Set
        End Property

        Public Property AutoApproveAfter() As String
            Get
                Return _AutoApproveAfter
            End Get
            Set(ByVal value As String)
                _AutoApproveAfter = value
            End Set
        End Property

        Public Property AutoApprovePolicy() As String
            Get
                Return _AutoApprovePolicy
            End Get
            Set(ByVal value As String)
                _AutoApprovePolicy = value
            End Set
        End Property

        Public Property BalanceConsideration() As Integer
            Get
                Return _BalanceConsideration
            End Get
            Set(ByVal value As Integer)
                _BalanceConsideration = value
            End Set
        End Property

        Public Property MinLeaveApplyDay() As String
            Get
                Return _MinLeaveApplyDay
            End Get
            Set(ByVal value As String)
                _MinLeaveApplyDay = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Public Sub New()

            objDALLeavesTypes = New DALLeavesTypes()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALLeavesTypes.Add(_LeaveName, _LeaveArabicName, _Balance, _MonthlyBalancing, _PaymentConsidration, _MinDuration, _MaxDuration, _MinServiceDays, _ExcludeOffDays, _ExcludeHolidays, _MaxRoundBalance, _ExpiredBalanceIsCashed, _AllowIfBalanceOver, _MaxOccurancePerPeriod, _IsAnnual, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE, _LeaveApproval, _GeneralGuide, _FK_ParentLeaveType, _GeneralGuideAr, _AllowedForSelfService, _ShowRemainingBalance, IsSpecificGrade, _Leave_NotificationException, _AttachmentIsMandatory, _RemarksIsMandatory, _AllowedGender, _AllowForSpecificEmployeeType, _FK_EmployeeTypeId, _LeaveRequestManagerLevelRequired, _ValidateLeavesBeforeRestDays, _LeaveCode, _AllowedAfterDays, _AllowedBeforeDays, _ApprovalRequired, _IsAutoApprove, _AutoApproveAfter, _AutoApprovePolicy, _BalanceConsideration, _MinLeaveApplyDay)
            _LeaveId = objDALLeavesTypes.LeaveId
            App_EventsLog.Insert_ToEventLog("Add", _LeaveId, "Emp_LeavesTypes", "Define Type of Leaves")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALLeavesTypes.Update(_LeaveId, _LeaveName, _LeaveArabicName, _Balance, _MonthlyBalancing, _PaymentConsidration, _MinDuration, _MaxDuration, _MinServiceDays, _ExcludeOffDays, _ExcludeHolidays, _MaxRoundBalance, _ExpiredBalanceIsCashed, _AllowIfBalanceOver, _MaxOccurancePerPeriod, _IsAnnual, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE, _LeaveApproval, _GeneralGuide, _FK_ParentLeaveType, _GeneralGuideAr, _AllowedForSelfService, _ShowRemainingBalance, IsSpecificGrade, _Leave_NotificationException, _AttachmentIsMandatory, _RemarksIsMandatory, _AllowedGender, _AllowForSpecificEmployeeType, _FK_EmployeeTypeId, _LeaveRequestManagerLevelRequired, _ValidateLeavesBeforeRestDays, _LeaveCode, _AllowedAfterDays, _AllowedBeforeDays, _ApprovalRequired, _IsAutoApprove, _AutoApproveAfter, _AutoApprovePolicy, _BalanceConsideration, _MinLeaveApplyDay)
            App_EventsLog.Insert_ToEventLog("Edit", _LeaveId, "Emp_LeavesTypes", "Define Type of Leaves")
            Return rslt

        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALLeavesTypes.Delete(_LeaveId)
            App_EventsLog.Insert_ToEventLog("Delete", _LeaveId, "Emp_LeavesTypes", "Define Type of Leaves")
            Return rslt

        End Function

        Public Function GetAll() As DataTable

            Return objDALLeavesTypes.GetAll()

        End Function

        Public Function GetAll_Annual() As DataTable

            Return objDALLeavesTypes.GetAll_Annual()

        End Function

        Public Function GetAllByLeaveTypes(Optional ByVal Query As String = Nothing) As DataTable

            Return objDALLeavesTypes.GetAllByLeaveTypes(Query)

        End Function

        Public Function GetAllForDDL() As DataTable

            Return objDALLeavesTypes.GetAllForDDL()

        End Function

        Public Function GetAllowed_ForSelfService() As DataTable

            Return objDALLeavesTypes.GetAllowed_ForSelfService()

        End Function

        Public Function GetByPK() As LeavesTypes

            Dim dr As DataRow
            dr = objDALLeavesTypes.GetByPK(_LeaveId)

            If Not IsDBNull(dr("LeaveId")) Then
                _LeaveId = dr("LeaveId")
            End If
            If Not IsDBNull(dr("LeaveName")) Then
                _LeaveName = dr("LeaveName")
            End If
            If Not IsDBNull(dr("LeaveArabicName")) Then
                _LeaveArabicName = dr("LeaveArabicName")
            End If
            If Not IsDBNull(dr("Balance")) Then
                _Balance = dr("Balance")
            End If
            If Not IsDBNull(dr("MonthlyBalancing")) Then
                _MonthlyBalancing = dr("MonthlyBalancing")
            End If
            If Not IsDBNull(dr("PaymentConsidration")) Then
                _PaymentConsidration = dr("PaymentConsidration")
            End If
            If Not IsDBNull(dr("MinDuration")) Then
                _MinDuration = dr("MinDuration")
            End If
            If Not IsDBNull(dr("MaxDuration")) Then
                _MaxDuration = dr("MaxDuration")
            End If
            If Not IsDBNull(dr("MinServiceDays")) Then
                _MinServiceDays = dr("MinServiceDays")
            End If
            If Not IsDBNull(dr("ExcludeOffDays")) Then
                _ExcludeOffDays = dr("ExcludeOffDays")
            End If
            If Not IsDBNull(dr("ExcludeHolidays")) Then
                _ExcludeHolidays = dr("ExcludeHolidays")
            End If
            If Not IsDBNull(dr("MaxRoundBalance")) Then
                _MaxRoundBalance = dr("MaxRoundBalance")
            End If
            If Not IsDBNull(dr("ExpiredBalanceIsCashed")) Then
                _ExpiredBalanceIsCashed = dr("ExpiredBalanceIsCashed")
            End If
            If Not IsDBNull(dr("AllowIfBalanceOver")) Then
                _AllowIfBalanceOver = dr("AllowIfBalanceOver")
            End If
            If Not IsDBNull(dr("MaxOccurancePerPeriod")) Then
                _MaxOccurancePerPeriod = dr("MaxOccurancePerPeriod")
            End If
            If Not IsDBNull(dr("IsAnnual")) Then
                _IsAnnual = dr("IsAnnual")
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
            If Not IsDBNull(dr("LeaveApproval")) Then
                _LeaveApproval = dr("LeaveApproval")
            End If
            If Not IsDBNull(dr("GeneralGuide")) Then
                _GeneralGuide = dr("GeneralGuide")
            End If
            If Not IsDBNull(dr("FK_ParentLeaveType")) Then
                _FK_ParentLeaveType = dr("FK_ParentLeaveType")
            Else
                _FK_ParentLeaveType = -1
            End If
            If Not IsDBNull(dr("GeneralGuideAr")) Then
                _GeneralGuideAr = dr("GeneralGuideAr")
            End If
            If Not IsDBNull(dr("AllowedForSelfService")) Then
                _AllowedForSelfService = dr("AllowedForSelfService")
            End If
            If Not IsDBNull(dr("ShowRemainingBalance")) Then
                _ShowRemainingBalance = dr("ShowRemainingBalance")
            End If
            If Not IsDBNull(dr("IsSpecificGrade")) Then
                _IsSpecificGrade = dr("IsSpecificGrade")
            End If
            If Not IsDBNull(dr("Leave_NotificationException")) Then
                _Leave_NotificationException = dr("Leave_NotificationException")
            End If
            If Not IsDBNull(dr("AttachmentIsMandatory")) Then
                _AttachmentIsMandatory = dr("AttachmentIsMandatory")
            End If
            If Not IsDBNull(dr("RemarksIsMandatory")) Then
                _RemarksIsMandatory = dr("RemarksIsMandatory")
            End If
            If Not IsDBNull(dr("AllowedGender")) Then
                _AllowedGender = dr("AllowedGender")
            End If
            If Not IsDBNull(dr("AllowForSpecificEmployeeType")) Then
                _AllowForSpecificEmployeeType = dr("AllowForSpecificEmployeeType")
            End If
            If Not IsDBNull(dr("FK_EmployeeTypeId")) Then
                _FK_EmployeeTypeId = dr("FK_EmployeeTypeId")
            End If
            If Not IsDBNull(dr("LeaveRequestManagerLevelRequired")) Then
                _LeaveRequestManagerLevelRequired = dr("LeaveRequestManagerLevelRequired")
            End If
            If Not IsDBNull(dr("ValidateLeavesBeforeRestDays")) Then
                _ValidateLeavesBeforeRestDays = dr("ValidateLeavesBeforeRestDays")
            End If
            If Not IsDBNull(dr("LeaveCode")) Then
                _LeaveCode = dr("LeaveCode")
            End If
            If Not IsDBNull(dr("AllowedAfterDays")) Then
                _AllowedAfterDays = dr("AllowedAfterDays")
            End If
            If Not IsDBNull(dr("AllowedBeforeDays")) Then
                _AllowedBeforeDays = dr("AllowedBeforeDays")
            End If
            If Not IsDBNull(dr("ApprovalRequired")) Then
                _ApprovalRequired = dr("ApprovalRequired")
            End If
            If Not IsDBNull(dr("IsAutoApprove")) Then
                _IsAutoApprove = dr("IsAutoApprove")
            End If
            If Not IsDBNull(dr("AutoApproveAfter")) Then
                _AutoApproveAfter = dr("AutoApproveAfter")
            End If
            If Not IsDBNull(dr("AutoApprovePolicy")) Then
                _AutoApprovePolicy = dr("AutoApprovePolicy")
            End If
            If Not IsDBNull(dr("BalanceConsideration")) Then
                _BalanceConsideration = dr("BalanceConsideration")
            End If
            If Not IsDBNull(dr("MinLeaveApplyDay")) Then
                _MinLeaveApplyDay = dr("MinLeaveApplyDay")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace