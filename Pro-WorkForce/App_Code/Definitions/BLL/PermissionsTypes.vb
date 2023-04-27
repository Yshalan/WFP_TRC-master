Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Definitions

    Public Class PermissionsTypes

#Region "Class Variables"


        Private _PermId As Integer
        Private _PermName As String
        Private _PermArabicName As String
        Private _MinDuration As Integer
        Private _MaxDuration As Integer
        Private _IsConsiderInWork As Boolean
        Private _MonthlyBalance As Integer
        Private _AllowedOccurancePerPeriod As String
        Private _allowedDurationPerPeriod As String
        Private _FK_LeaveIdDeductBalance As Integer
        Private _ApprovalRequired As Boolean
        Private _FK_RelatedTAReason As Integer
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private _GeneralGuide As String
        Private _GeneralGuideAr As String
        Private _FK_LeaveIdtoallowduration As Integer
        Private _DurationAllowedwithleave As Integer
        Private _Isallowedaftertime As Boolean
        Private _AllowedAfter As Integer
        Private _ShouldComplete50WHRS As Boolean
        Private _AllowedAfterDays As String
        Private _AllowedBeforeDays As String
        Private _ExcludeManagers_FromAfterBefore As Boolean
        Private _AllowedForManagers As Boolean
        Private _AllowedForSelfService As Boolean
        Private _PermissionApproval As Integer
        Private _NotAllowedWhenHasStudyOrNursing As Boolean
        Private _ShowRemainingBalance As Boolean
        Private _HasFlexiblePermission As Boolean
        Private _HasPermissionForPeriod As Boolean
        Private _HasFullDayPermission As Boolean
        Private _ConsiderRequestWithinBalance As Boolean

        Private _IsSpecificCompany As Boolean
        Private _IsSpecificEntity As Boolean
        Private _FK_CompanyId As Integer
        Private _FK_EntityId As Integer

        Private _AttachmentIsMandatory As Boolean
        Private _RemarksIsMandatory As Boolean
        Private _IsAllowedBeforeTime As Boolean
        Private _AllowedBefore As Integer
        Private _AllowForSpecificEmployeeType As Boolean
        Private _FK_EmployeeTypeId As Integer
        Private _MinDurationAllowedInSelfService As String
        Private _IsAutoApprove As Boolean
        Private _AutoApproveAfter As String
        Private _Perm_NotificationException As Boolean
        Private _AutoApprovePolicy As String
        Private _ConvertToLeave_ExceedDuration As Boolean
        Private _AnnualLeaveId_ToDeductPermission As Integer
        Private _MustHaveTransaction As Boolean
        Private _DeductBalanceFromOvertime As Boolean
        Private _OvertimeBalanceDays As Integer
        Private _ValidateDelayPermissions As Boolean
        Private _DelayPermissionsValidation As String
        Private _AllowedBeforeDaysPolicy As String
        Private _AllowWhenInSufficient As Boolean
        Private _HasPermissionTimeControls As Boolean
        Private _PermissionRequestManagerLevelRequired As Integer
        Private objDALPermissionsTypes As DALPermissionsTypes

#End Region

#Region "Public Properties"

        Public Property PermId() As Integer
            Set(ByVal value As Integer)
                _PermId = value
            End Set
            Get
                Return (_PermId)
            End Get
        End Property

        Public Property PermName() As String
            Set(ByVal value As String)
                _PermName = value
            End Set
            Get
                Return (_PermName)
            End Get
        End Property

        Public Property PermArabicName() As String
            Set(ByVal value As String)
                _PermArabicName = value
            End Set
            Get
                Return (_PermArabicName)
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

        Public Property IsConsiderInWork() As Boolean
            Set(ByVal value As Boolean)
                _IsConsiderInWork = value
            End Set
            Get
                Return (_IsConsiderInWork)
            End Get
        End Property

        Public Property MonthlyBalance() As Integer
            Set(ByVal value As Integer)
                _MonthlyBalance = value
            End Set
            Get
                Return (_MonthlyBalance)
            End Get
        End Property

        Public Property AllowedOccurancePerPeriod() As String
            Set(ByVal value As String)
                _AllowedOccurancePerPeriod = value
            End Set
            Get
                Return (_AllowedOccurancePerPeriod)
            End Get
        End Property

        Public Property allowedDurationPerPeriod() As String
            Set(ByVal value As String)
                _allowedDurationPerPeriod = value
            End Set
            Get
                Return (_allowedDurationPerPeriod)
            End Get
        End Property

        Public Property FK_LeaveIdDeductBalance() As Integer
            Set(ByVal value As Integer)
                _FK_LeaveIdDeductBalance = value
            End Set
            Get
                Return (_FK_LeaveIdDeductBalance)
            End Get
        End Property

        Public Property ApprovalRequired() As Boolean
            Set(ByVal value As Boolean)
                _ApprovalRequired = value
            End Set
            Get
                Return (_ApprovalRequired)
            End Get
        End Property

        Public Property FK_RelatedTAReason() As Integer
            Set(ByVal value As Integer)
                _FK_RelatedTAReason = value
            End Set
            Get
                Return (_FK_RelatedTAReason)
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

        Public Property GeneralGuide() As String
            Get
                Return _GeneralGuide
            End Get
            Set(ByVal value As String)
                _GeneralGuide = value
            End Set
        End Property

        Public Property GeneralGuideAr() As String
            Get
                Return _GeneralGuideAr
            End Get
            Set(ByVal value As String)
                _GeneralGuideAr = value
            End Set
        End Property

        Public Property FK_LeaveIdtoallowduration() As Integer
            Get
                Return _FK_LeaveIdtoallowduration
            End Get
            Set(ByVal value As Integer)
                _FK_LeaveIdtoallowduration = value
            End Set
        End Property

        Public Property DurationAllowedwithleave() As Integer
            Get
                Return _DurationAllowedwithleave
            End Get
            Set(ByVal value As Integer)
                _DurationAllowedwithleave = value
            End Set
        End Property

        Public Property Isallowedaftertime() As Boolean
            Get
                Return _Isallowedaftertime
            End Get
            Set(ByVal value As Boolean)
                _Isallowedaftertime = value
            End Set
        End Property

        Public Property AllowedAfter() As Integer
            Get
                Return _AllowedAfter
            End Get
            Set(ByVal value As Integer)
                _AllowedAfter = value
            End Set
        End Property

        Public Property ShouldComplete50WHRS() As Boolean
            Get
                Return _ShouldComplete50WHRS
            End Get
            Set(ByVal value As Boolean)
                _ShouldComplete50WHRS = value
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

        Public Property ExcludeManagers_FromAfterBefore() As Boolean
            Get
                Return _ExcludeManagers_FromAfterBefore
            End Get
            Set(ByVal value As Boolean)
                _ExcludeManagers_FromAfterBefore = value
            End Set
        End Property

        Public Property AllowedForManagers() As Boolean
            Get
                Return _AllowedForManagers
            End Get
            Set(ByVal value As Boolean)
                _AllowedForManagers = value
            End Set
        End Property

        Public Property AllowedForSelfService() As Boolean
            Get
                Return _AllowedForSelfService
            End Get
            Set(ByVal value As Boolean)
                _AllowedForSelfService = value
            End Set
        End Property

        Public Property PermissionApproval() As Integer
            Get
                Return _PermissionApproval
            End Get
            Set(ByVal value As Integer)
                _PermissionApproval = value
            End Set
        End Property

        Public Property NotAllowedWhenHasStudyOrNursing() As Boolean
            Get
                Return _NotAllowedWhenHasStudyOrNursing
            End Get
            Set(ByVal value As Boolean)
                _NotAllowedWhenHasStudyOrNursing = value
            End Set
        End Property

        Public Property ShowRemainingBalance() As Boolean
            Get
                Return _ShowRemainingBalance
            End Get
            Set(ByVal value As Boolean)
                _ShowRemainingBalance = value
            End Set
        End Property

        Public Property HasFlexiblePermission() As Boolean
            Get
                Return _HasFlexiblePermission
            End Get
            Set(ByVal value As Boolean)
                _HasFlexiblePermission = value
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

        Public Property HasFullDayPermission() As Boolean
            Get
                Return _HasFullDayPermission
            End Get
            Set(ByVal value As Boolean)
                _HasFullDayPermission = value
            End Set
        End Property

        Public Property ConsiderRequestWithinBalance() As Boolean
            Set(ByVal value As Boolean)
                _ConsiderRequestWithinBalance = value
            End Set
            Get
                Return (_ConsiderRequestWithinBalance)
            End Get
        End Property

        Public Property AttachmentIsMandatory() As Boolean
            Set(ByVal value As Boolean)
                _AttachmentIsMandatory = value
            End Set
            Get
                Return (_AttachmentIsMandatory)
            End Get
        End Property

        Public Property RemarksIsMandatory() As Boolean
            Set(ByVal value As Boolean)
                _RemarksIsMandatory = value
            End Set
            Get
                Return (_RemarksIsMandatory)
            End Get
        End Property

        Public Property IsAllowedBeforeTime() As Boolean
            Set(ByVal value As Boolean)
                _IsAllowedBeforeTime = value
            End Set
            Get
                Return (_IsAllowedBeforeTime)
            End Get
        End Property

        Public Property AllowedBefore() As Integer
            Set(ByVal value As Integer)
                _AllowedBefore = value
            End Set
            Get
                Return (_AllowedBefore)
            End Get
        End Property

        Public Property AllowForSpecificEmployeeType() As Boolean
            Set(ByVal value As Boolean)
                _AllowForSpecificEmployeeType = value
            End Set
            Get
                Return (_AllowForSpecificEmployeeType)
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

        Public Property IsSpecificCompany() As Boolean
            Set(ByVal value As Boolean)
                _IsSpecificCompany = value
            End Set
            Get
                Return (_IsSpecificCompany)
            End Get
        End Property

        Public Property IsSpecificEntity() As Boolean
            Set(ByVal value As Boolean)
                _IsSpecificEntity = value
            End Set
            Get
                Return (_IsSpecificEntity)
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

        Public Property MinDurationAllowedInSelfService() As String
            Set(ByVal value As String)
                _MinDurationAllowedInSelfService = value
            End Set
            Get
                Return (_MinDurationAllowedInSelfService)
            End Get
        End Property


        Public Property IsAutoApprove() As Boolean
            Set(ByVal value As Boolean)
                _IsAutoApprove = value
            End Set
            Get
                Return (_IsAutoApprove)
            End Get
        End Property

        Public Property AutoApproveAfter() As String
            Set(ByVal value As String)
                _AutoApproveAfter = value
            End Set
            Get
                Return (_AutoApproveAfter)
            End Get
        End Property

        Public Property Perm_NotificationException() As Boolean
            Set(ByVal value As Boolean)
                _Perm_NotificationException = value
            End Set
            Get
                Return (_Perm_NotificationException)
            End Get
        End Property

        Public Property AutoApprovePolicy() As String
            Set(ByVal value As String)
                _AutoApprovePolicy = value
            End Set
            Get
                Return (_AutoApprovePolicy)
            End Get
        End Property

        Public Property ConvertToLeave_ExceedDuration() As Boolean
            Set(ByVal value As Boolean)
                _ConvertToLeave_ExceedDuration = value
            End Set
            Get
                Return (_ConvertToLeave_ExceedDuration)
            End Get
        End Property

        Public Property AnnualLeaveId_ToDeductPermission() As Integer
            Set(ByVal value As Integer)
                _AnnualLeaveId_ToDeductPermission = value
            End Set
            Get
                Return (_AnnualLeaveId_ToDeductPermission)
            End Get
        End Property

        Public Property MustHaveTransaction() As Boolean
            Set(ByVal value As Boolean)
                _MustHaveTransaction = value
            End Set
            Get
                Return (_MustHaveTransaction)
            End Get
        End Property

        Public Property DeductBalanceFromOvertime() As Boolean
            Set(ByVal value As Boolean)
                _DeductBalanceFromOvertime = value
            End Set
            Get
                Return (_DeductBalanceFromOvertime)
            End Get
        End Property

        Public Property OvertimeBalanceDays() As Integer
            Set(ByVal value As Integer)
                _OvertimeBalanceDays = value
            End Set
            Get
                Return (_OvertimeBalanceDays)
            End Get
        End Property

        Public Property ValidateDelayPermissions() As Boolean
            Set(ByVal value As Boolean)
                _ValidateDelayPermissions = value
            End Set
            Get
                Return (_ValidateDelayPermissions)
            End Get
        End Property

        Public Property DelayPermissionsValidation() As String
            Set(ByVal value As String)
                _DelayPermissionsValidation = value
            End Set
            Get
                Return (_DelayPermissionsValidation)
            End Get
        End Property

        Public Property AllowedBeforeDaysPolicy() As String
            Set(ByVal value As String)
                _AllowedBeforeDaysPolicy = value
            End Set
            Get
                Return (_AllowedBeforeDaysPolicy)
            End Get
        End Property

        Public Property AllowWhenInSufficient() As Boolean
            Set(ByVal value As Boolean)
                _AllowWhenInSufficient = value
            End Set
            Get
                Return (_AllowWhenInSufficient)
            End Get
        End Property

        Public Property HasPermissionTimeControls() As Boolean
            Set(ByVal value As Boolean)
                _HasPermissionTimeControls = value
            End Set
            Get
                Return (_HasPermissionTimeControls)
            End Get
        End Property

        Public Property PermissionRequestManagerLevelRequired() As Integer
            Set(ByVal value As Integer)
                _PermissionRequestManagerLevelRequired = value
            End Set
            Get
                Return (_PermissionRequestManagerLevelRequired)
            End Get
        End Property

#End Region

#Region "Constructor"

        Public Sub New()

            objDALPermissionsTypes = New DALPermissionsTypes()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer


            Dim rslt As Integer = objDALPermissionsTypes.Add(_PermName,
                                              _PermArabicName,
                                              _MinDuration,
                                              _MaxDuration,
                                              _IsConsiderInWork,
                                              _MonthlyBalance,
                                              _AllowedOccurancePerPeriod,
                                              _allowedDurationPerPeriod,
                                              _FK_LeaveIdDeductBalance,
                                              _ApprovalRequired, _FK_RelatedTAReason,
                                              _CREATED_BY, _CREATED_DATE,
                                              _LAST_UPDATE_BY,
                                              _LAST_UPDATE_DATE,
                                              _GeneralGuide,
                                              _GeneralGuideAr,
                                              _FK_LeaveIdtoallowduration,
                                              _DurationAllowedwithleave,
                                              _Isallowedaftertime,
                                              _AllowedAfter,
                                              _ShouldComplete50WHRS,
                                              _AllowedAfterDays,
                                              _AllowedBeforeDays,
                                              _ExcludeManagers_FromAfterBefore,
                                              _AllowedForManagers,
                                              _AllowedForSelfService,
                                              _PermissionApproval,
                                              _NotAllowedWhenHasStudyOrNursing,
                                              _ShowRemainingBalance,
                                              _HasFlexiblePermission,
                                              _HasPermissionForPeriod,
                                              _HasFullDayPermission,
                                              _ConsiderRequestWithinBalance, _AttachmentIsMandatory,
                                              _RemarksIsMandatory, _IsAllowedBeforeTime, _AllowedBefore,
                                              _AllowForSpecificEmployeeType, _FK_EmployeeTypeId, _IsSpecificCompany, _IsSpecificEntity,
                                              _MinDurationAllowedInSelfService, _IsAutoApprove, _AutoApproveAfter, _Perm_NotificationException,
                                              _AutoApprovePolicy, _ConvertToLeave_ExceedDuration, _AnnualLeaveId_ToDeductPermission, _MustHaveTransaction,
                                              _DeductBalanceFromOvertime, _OvertimeBalanceDays, _ValidateDelayPermissions, _DelayPermissionsValidation, _AllowWhenInSufficient,
                                              _HasPermissionTimeControls, _PermissionRequestManagerLevelRequired)
            _PermId = objDALPermissionsTypes.PermId
            App_EventsLog.Insert_ToEventLog("Add", _PermId, "PermissionsTypes", "Define Type of Permissions")
            Return rslt


        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALPermissionsTypes.Update(_PermId, _PermName,
                                                 _PermArabicName, _MinDuration, _MaxDuration,
                                                 _IsConsiderInWork, _MonthlyBalance,
                                                 _AllowedOccurancePerPeriod, _allowedDurationPerPeriod,
                                                 _FK_LeaveIdDeductBalance,
                                                 _ApprovalRequired,
                                                 _FK_RelatedTAReason, _CREATED_BY,
                                                 _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE,
                                                 _GeneralGuide, _GeneralGuideAr, _FK_LeaveIdtoallowduration, _DurationAllowedwithleave,
                                                 _Isallowedaftertime,
                                              _AllowedAfter, _ShouldComplete50WHRS, _AllowedAfterDays, _AllowedBeforeDays,
                                              _ExcludeManagers_FromAfterBefore, _AllowedForManagers,
                                              _AllowedForSelfService, _PermissionApproval,
                                              _NotAllowedWhenHasStudyOrNursing,
                                              _ShowRemainingBalance,
                                              _HasFlexiblePermission,
                                              _HasPermissionForPeriod,
                                              _HasFullDayPermission,
                                              _ConsiderRequestWithinBalance, _AttachmentIsMandatory,
                                              _RemarksIsMandatory, _IsAllowedBeforeTime, _AllowedBefore,
                                              _AllowForSpecificEmployeeType, _FK_EmployeeTypeId,
                                              _IsSpecificCompany, _IsSpecificEntity, _MinDurationAllowedInSelfService, _IsAutoApprove, _AutoApproveAfter,
                                              _Perm_NotificationException, _AutoApprovePolicy, _ConvertToLeave_ExceedDuration, _AnnualLeaveId_ToDeductPermission, _MustHaveTransaction,
                                              _DeductBalanceFromOvertime, _OvertimeBalanceDays, _ValidateDelayPermissions, _DelayPermissionsValidation, _AllowWhenInSufficient, _HasPermissionTimeControls,
                                              _PermissionRequestManagerLevelRequired)
            App_EventsLog.Insert_ToEventLog("Edit", _PermId, "PermissionsTypes", "Define Type of Permissions")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALPermissionsTypes.Delete(_PermId)
            App_EventsLog.Insert_ToEventLog("Delete", _PermId, "PermissionsTypes", "Define Type of Permissions")
            Return rslt

        End Function

        Public Function GetAll() As DataTable

            Return objDALPermissionsTypes.GetAll()

        End Function

        Public Function GetAllowed_ForSelfService() As DataTable

            Return objDALPermissionsTypes.GetAllowed_ForSelfService(_FK_CompanyId, _FK_EntityId, _FK_EmployeeTypeId)

        End Function

        Public Function GetAllowed_ForManagers() As DataTable

            Return objDALPermissionsTypes.GetAllowed_ForManagers(_FK_CompanyId, _FK_EntityId, _FK_EmployeeTypeId)

        End Function

        Public Function GetByPK() As PermissionsTypes

            Dim dr As DataRow
            dr = objDALPermissionsTypes.GetByPK(_PermId)

            If (dr IsNot Nothing) Then
                If Not IsDBNull(dr("PermId")) Then
                    _PermId = dr("PermId")
                End If
                If Not IsDBNull(dr("PermName")) Then
                    _PermName = dr("PermName")
                End If
                If Not IsDBNull(dr("PermArabicName")) Then
                    _PermArabicName = dr("PermArabicName")
                End If
                If Not IsDBNull(dr("MinDuration")) Then
                    _MinDuration = dr("MinDuration")
                End If
                If Not IsDBNull(dr("MaxDuration")) Then
                    _MaxDuration = dr("MaxDuration")
                End If
                If Not IsDBNull(dr("IsConsiderInWork")) Then
                    _IsConsiderInWork = dr("IsConsiderInWork")
                End If
                If Not IsDBNull(dr("MonthlyBalance")) Then
                    _MonthlyBalance = dr("MonthlyBalance")
                End If
                If Not IsDBNull(dr("AllowedOccurancePerPeriod")) Then
                    _AllowedOccurancePerPeriod = dr("AllowedOccurancePerPeriod")
                End If
                If Not IsDBNull(dr("allowedDurationPerPeriod")) Then
                    _allowedDurationPerPeriod = dr("allowedDurationPerPeriod")
                End If
                If Not IsDBNull(dr("FK_LeaveIdDeductBalance")) Then
                    _FK_LeaveIdDeductBalance = dr("FK_LeaveIdDeductBalance")
                End If
                If Not IsDBNull(dr("ApprovalRequired")) Then
                    _ApprovalRequired = dr("ApprovalRequired")
                End If
                If Not IsDBNull(dr("FK_RelatedTAReason")) Then
                    _FK_RelatedTAReason = dr("FK_RelatedTAReason")
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
                If Not IsDBNull(dr("GeneralGuide")) Then
                    _GeneralGuide = dr("GeneralGuide")
                End If
                If Not IsDBNull(dr("GeneralGuideAr")) Then
                    _GeneralGuideAr = dr("GeneralGuideAr")
                End If
                If Not IsDBNull(dr("FK_LeaveIdtoallowduration")) Then
                    _FK_LeaveIdtoallowduration = dr("FK_LeaveIdtoallowduration")
                End If
                If Not IsDBNull(dr("DurationAllowedwithleave")) Then
                    _DurationAllowedwithleave = dr("DurationAllowedwithleave")
                End If
                If Not IsDBNull(dr("Isallowedaftertime")) Then
                    _Isallowedaftertime = dr("Isallowedaftertime")
                End If
                If Not IsDBNull(dr("AllowedAfter")) Then
                    _AllowedAfter = dr("AllowedAfter")
                End If
                If Not IsDBNull(dr("ShouldComplete50WHRS")) Then
                    _ShouldComplete50WHRS = dr("ShouldComplete50WHRS")
                End If
                If Not IsDBNull(dr("AllowedAfterDays")) Then
                    _AllowedAfterDays = dr("AllowedAfterDays")
                End If
                If Not IsDBNull(dr("AllowedBeforeDays")) Then
                    _AllowedBeforeDays = dr("AllowedBeforeDays")
                End If
                If Not IsDBNull(dr("ExcludeManagers_FromAfterBefore")) Then
                    _ExcludeManagers_FromAfterBefore = dr("ExcludeManagers_FromAfterBefore")
                End If
                If Not IsDBNull(dr("AllowedForManagers")) Then
                    _AllowedForManagers = dr("AllowedForManagers")
                End If
                If Not IsDBNull(dr("AllowedForSelfService")) Then
                    _AllowedForSelfService = dr("AllowedForSelfService")
                End If
                If Not IsDBNull(dr("PermissionApproval")) Then
                    _PermissionApproval = dr("PermissionApproval")
                End If
                If Not IsDBNull(dr("NotAllowedWhenHasStudyOrNursing")) Then
                    _NotAllowedWhenHasStudyOrNursing = dr("NotAllowedWhenHasStudyOrNursing")
                End If
                If Not IsDBNull(dr("ShowRemainingBalance")) Then
                    _ShowRemainingBalance = dr("ShowRemainingBalance")
                End If
                If Not IsDBNull(dr("HasFlexiblePermission")) Then
                    _HasFlexiblePermission = dr("HasFlexiblePermission")
                End If
                If Not IsDBNull(dr("HasPermissionForPeriod")) Then
                    _HasPermissionForPeriod = dr("HasPermissionForPeriod")
                End If
                If Not IsDBNull(dr("HasFullDayPermission")) Then
                    _HasFullDayPermission = dr("HasFullDayPermission")
                End If
                If Not IsDBNull(dr("ConsiderRequestWithinBalance")) Then
                    _ConsiderRequestWithinBalance = dr("ConsiderRequestWithinBalance")
                End If
                If Not IsDBNull(dr("AttachmentIsMandatory")) Then
                    _AttachmentIsMandatory = dr("AttachmentIsMandatory")
                End If
                If Not IsDBNull(dr("RemarksIsMandatory")) Then
                    _RemarksIsMandatory = dr("RemarksIsMandatory")
                End If
                If Not IsDBNull(dr("IsAllowedBeforeTime")) Then
                    _IsAllowedBeforeTime = dr("IsAllowedBeforeTime")
                End If
                If Not IsDBNull(dr("AllowedBefore")) Then
                    _AllowedBefore = dr("AllowedBefore")
                End If
                If Not IsDBNull(dr("AllowForSpecificEmployeeType")) Then
                    _AllowForSpecificEmployeeType = dr("AllowForSpecificEmployeeType")
                End If
                If Not IsDBNull(dr("FK_EmployeeTypeId")) Then
                    _FK_EmployeeTypeId = dr("FK_EmployeeTypeId")
                End If
                If Not IsDBNull(dr("IsSpecificCompany")) Then
                    _IsSpecificCompany = dr("IsSpecificCompany")
                End If
                If Not IsDBNull(dr("IsSpecificEntity")) Then
                    _IsSpecificEntity = dr("IsSpecificEntity")
                End If
                If Not IsDBNull(dr("MinDurationAllowedInSelfService")) Then
                    _MinDurationAllowedInSelfService = dr("MinDurationAllowedInSelfService")
                End If
                If Not IsDBNull(dr("IsAutoApprove")) Then
                    _IsAutoApprove = dr("IsAutoApprove")
                End If
                If Not IsDBNull(dr("AutoApproveAfter")) Then
                    _AutoApproveAfter = dr("AutoApproveAfter")
                End If
                If Not IsDBNull(dr("Perm_NotificationException")) Then
                    _Perm_NotificationException = dr("Perm_NotificationException")
                End If
                If Not IsDBNull(dr("AutoApprovePolicy")) Then
                    _AutoApprovePolicy = dr("AutoApprovePolicy")
                End If
                If Not IsDBNull(dr("ConvertToLeave_ExceedDuration")) Then
                    _ConvertToLeave_ExceedDuration = dr("ConvertToLeave_ExceedDuration")
                End If
                If Not IsDBNull(dr("AnnualLeaveId_ToDeductPermission")) Then
                    _AnnualLeaveId_ToDeductPermission = dr("AnnualLeaveId_ToDeductPermission")
                End If
                If Not IsDBNull(dr("MustHaveTransaction")) Then
                    _MustHaveTransaction = dr("MustHaveTransaction")
                End If
                If Not IsDBNull(dr("DeductBalanceFromOvertime")) Then
                    _DeductBalanceFromOvertime = dr("DeductBalanceFromOvertime")
                End If
                If Not IsDBNull(dr("OvertimeBalanceDays")) Then
                    _OvertimeBalanceDays = dr("OvertimeBalanceDays")
                End If
                If Not IsDBNull(dr("ValidateDelayPermissions")) Then
                    _ValidateDelayPermissions = dr("ValidateDelayPermissions")
                End If
                If Not IsDBNull(dr("DelayPermissionsValidation")) Then
                    _DelayPermissionsValidation = dr("DelayPermissionsValidation")
                End If
                If Not IsDBNull(dr("AllowWhenInSufficient")) Then
                    _AllowWhenInSufficient = dr("AllowWhenInSufficient")
                End If
                If Not IsDBNull(dr("HasPermissionTimeControls")) Then
                    _HasPermissionTimeControls = dr("HasPermissionTimeControls")
                End If
                If Not IsDBNull(dr("PermissionRequestManagerLevelRequired")) Then
                    _PermissionRequestManagerLevelRequired = dr("PermissionRequestManagerLevelRequired")
                End If
            End If
            Return Me
        End Function

#End Region

#Region "Extended Methods"


        Public Function GetAllInnerJoin() As DataTable

            Return objDALPermissionsTypes.GetAllInnerJoin()


        End Function

#End Region

    End Class
End Namespace