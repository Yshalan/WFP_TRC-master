Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Admin

    Public Class OvertimeRules

#Region "Class Variables"


        Private _OvertimeRuleId As Integer
        Private _RuleName As String
        Private _RuleArabicName As String
        Private _OvertimeEligibility As Boolean
        Private _MinOvertime As Integer
        Private _ApprovalRequired As Boolean
        Private _OffDayIsHigh As Boolean
        Private _HolidayIsHigh As Boolean
        Private _HighHasTime As Boolean
        Private _IsCompensateLatetime As Boolean
        Private _IsLeaveBalance As Boolean
        Private _HighDayLeaveEquivalent As Double
        Private _LowDayLeaveEquivalent As Double
        Private _IsFinancial As Boolean
        Private _LowRate As Double
        Private _HighRate As Double
        Private _OverTimeApprovalBy As Integer
        Private _BeforeAfterSchedule As Integer
        Private _isLostFromHighOT As Boolean
        Private _MaxOvertime As Integer
        Private _FK_NormalTypeId As Integer
        Private _FK_OffDayTypeId As Integer
        Private _FK_HolidayTypeId As Integer
        Private _FK_ReligionHolidayTypeId As Integer
        Private _MinAutoApproveDuration As String

        Private objDALOvertimeRules As DALOvertimeRules

#End Region

#Region "Public Properties"


        Public Property OvertimeRuleId() As Integer
            Set(ByVal value As Integer)
                _OvertimeRuleId = value
            End Set
            Get
                Return (_OvertimeRuleId)
            End Get
        End Property

        Public Property RuleName() As String
            Set(ByVal value As String)
                _RuleName = value
            End Set
            Get
                Return (_RuleName)
            End Get
        End Property

        Public Property RuleArabicName() As String
            Set(ByVal value As String)
                _RuleArabicName = value
            End Set
            Get
                Return (_RuleArabicName)
            End Get
        End Property

        Public Property OvertimeEligibility() As Boolean
            Set(ByVal value As Boolean)
                _OvertimeEligibility = value
            End Set
            Get
                Return (_OvertimeEligibility)
            End Get
        End Property

        Public Property isLostFromHighOT() As Boolean
            Set(ByVal value As Boolean)
                _isLostFromHighOT = value
            End Set
            Get
                Return (_isLostFromHighOT)
            End Get
        End Property

        Public Property MinOvertime() As Integer
            Set(ByVal value As Integer)
                _MinOvertime = value
            End Set
            Get
                Return (_MinOvertime)
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

        Public Property OffDayIsHigh() As Boolean
            Set(ByVal value As Boolean)
                _OffDayIsHigh = value
            End Set
            Get
                Return (_OffDayIsHigh)
            End Get
        End Property

        Public Property HolidayIsHigh() As Boolean
            Set(ByVal value As Boolean)
                _HolidayIsHigh = value
            End Set
            Get
                Return (_HolidayIsHigh)
            End Get
        End Property

        Public Property HighHasTime() As Boolean
            Set(ByVal value As Boolean)
                _HighHasTime = value
            End Set
            Get
                Return (_HighHasTime)
            End Get
        End Property

        Public Property IsCompensateLatetime() As Boolean
            Set(ByVal value As Boolean)
                _IsCompensateLatetime = value
            End Set
            Get
                Return (_IsCompensateLatetime)
            End Get
        End Property

        Public Property IsLeaveBalance() As Boolean
            Set(ByVal value As Boolean)
                _IsLeaveBalance = value
            End Set
            Get
                Return (_IsLeaveBalance)
            End Get
        End Property

        Public Property HighDayLeaveEquivalent() As Double
            Set(ByVal value As Double)
                _HighDayLeaveEquivalent = value
            End Set
            Get
                Return (_HighDayLeaveEquivalent)
            End Get
        End Property

        Public Property LowDayLeaveEquivalent() As Double
            Set(ByVal value As Double)
                _LowDayLeaveEquivalent = value
            End Set
            Get
                Return (_LowDayLeaveEquivalent)
            End Get
        End Property

        Public Property BeforeAfterSchedule() As Integer
            Set(ByVal value As Integer)
                _BeforeAfterSchedule = value
            End Set
            Get
                Return (_BeforeAfterSchedule)
            End Get
        End Property

        Public Property IsFinancial() As Boolean
            Set(ByVal value As Boolean)
                _IsFinancial = value
            End Set
            Get
                Return (_IsFinancial)
            End Get
        End Property

        Public Property LowRate() As Double
            Set(ByVal value As Double)
                _LowRate = value
            End Set
            Get
                Return (_LowRate)
            End Get
        End Property

        Public Property HighRate() As Double
            Set(ByVal value As Double)
                _HighRate = value
            End Set
            Get
                Return (_HighRate)
            End Get
        End Property

        Public Property OverTimeApprovalBy() As Integer
            Set(ByVal value As Integer)
                _OverTimeApprovalBy = value
            End Set
            Get
                Return (_OverTimeApprovalBy)
            End Get
        End Property

        Public Property MaxOvertime() As Integer
            Set(ByVal value As Integer)
                _MaxOvertime = value
            End Set
            Get
                Return (_MaxOvertime)
            End Get
        End Property

        Public Property FK_NormalTypeId() As Integer
            Set(ByVal value As Integer)
                _FK_NormalTypeId = value
            End Set
            Get
                Return (_FK_NormalTypeId)
            End Get
        End Property

        Public Property FK_OffDayTypeId() As Integer
            Set(ByVal value As Integer)
                _FK_OffDayTypeId = value
            End Set
            Get
                Return (_FK_OffDayTypeId)
            End Get
        End Property

        Public Property FK_HolidayTypeId() As Integer
            Set(ByVal value As Integer)
                _FK_HolidayTypeId = value
            End Set
            Get
                Return (_FK_HolidayTypeId)
            End Get
        End Property

        Public Property FK_ReligionHolidayTypeId() As Integer
            Set(ByVal value As Integer)
                _FK_ReligionHolidayTypeId = value
            End Set
            Get
                Return (_FK_ReligionHolidayTypeId)
            End Get
        End Property

        Public Property MinAutoApproveDuration() As String
            Set(ByVal value As String)
                _MinAutoApproveDuration = value
            End Set
            Get
                Return (_MinAutoApproveDuration)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALOvertimeRules = New DALOvertimeRules()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer
            Dim rslt As Integer = objDALOvertimeRules.Add(_OvertimeRuleId, _RuleName, _RuleArabicName, _OvertimeEligibility, _MinOvertime, _ApprovalRequired, _OffDayIsHigh, _HolidayIsHigh, _HighHasTime, _IsCompensateLatetime, _IsLeaveBalance, _HighDayLeaveEquivalent, _LowDayLeaveEquivalent, _IsFinancial, _LowRate, _BeforeAfterSchedule, _HighRate, _OverTimeApprovalBy, _isLostFromHighOT, _MaxOvertime, _FK_NormalTypeId, FK_OffDayTypeId, _FK_HolidayTypeId, _FK_ReligionHolidayTypeId, _MinAutoApproveDuration)
            App_EventsLog.Insert_ToEventLog("Add", _OvertimeRuleId, "OvertimeRules", "Overtime Rules")
            Return rslt
        End Function

        Public Function Update() As Integer
            Dim rslt As Integer = objDALOvertimeRules.Update(_OvertimeRuleId, _RuleName, _RuleArabicName, _OvertimeEligibility, _MinOvertime, _ApprovalRequired, _OffDayIsHigh, _HolidayIsHigh, _HighHasTime, _IsCompensateLatetime, _IsLeaveBalance, _HighDayLeaveEquivalent, _LowDayLeaveEquivalent, _IsFinancial, _LowRate, _BeforeAfterSchedule, _HighRate, _OverTimeApprovalBy, _isLostFromHighOT, _MaxOvertime, _FK_NormalTypeId, FK_OffDayTypeId, _FK_HolidayTypeId, _FK_ReligionHolidayTypeId, _MinAutoApproveDuration)
            App_EventsLog.Insert_ToEventLog("Edit", _OvertimeRuleId, "OvertimeRules", "Overtime Rules")
            Return rslt

        End Function

        Public Function Delete() As Integer
            Dim rslt As Integer = objDALOvertimeRules.Delete(_OvertimeRuleId)
            App_EventsLog.Insert_ToEventLog("Delete", _OvertimeRuleId, "OvertimeRules", "Overtime Rules")
            Return rslt

        End Function

        Public Function GetAll() As DataTable

            Return objDALOvertimeRules.GetAll()

        End Function

        Public Function GetByPK() As OvertimeRules

            Dim dr As DataRow
            dr = objDALOvertimeRules.GetByPK(_OvertimeRuleId)

            If Not IsDBNull(dr("OvertimeRuleId")) Then
                _OvertimeRuleId = dr("OvertimeRuleId")
            End If
            If Not IsDBNull(dr("OverTimeApprovalBy")) Then
                _OverTimeApprovalBy = dr("OverTimeApprovalBy")
            End If

            If Not IsDBNull(dr("RuleName")) Then
                _RuleName = dr("RuleName")
            End If
            If Not IsDBNull(dr("RuleArabicName")) Then
                _RuleArabicName = dr("RuleArabicName")
            End If
            If Not IsDBNull(dr("OvertimeEligibility")) Then
                _OvertimeEligibility = dr("OvertimeEligibility")
            End If
            If Not IsDBNull(dr("MinOvertime")) Then
                _MinOvertime = dr("MinOvertime")
            End If
            If Not IsDBNull(dr("ApprovalRequired")) Then
                _ApprovalRequired = dr("ApprovalRequired")
            End If
            If Not IsDBNull(dr("OffDayIsHigh")) Then
                _OffDayIsHigh = dr("OffDayIsHigh")
            End If
            If Not IsDBNull(dr("HolidayIsHigh")) Then
                _HolidayIsHigh = dr("HolidayIsHigh")
            End If
            If Not IsDBNull(dr("HighHasTime")) Then
                _HighHasTime = dr("HighHasTime")
            End If
            If Not IsDBNull(dr("IsCompensateLatetime")) Then
                _IsCompensateLatetime = dr("IsCompensateLatetime")
            End If
            If Not IsDBNull(dr("IsLeaveBalance")) Then
                _IsLeaveBalance = dr("IsLeaveBalance")
            End If
            If Not IsDBNull(dr("HighDayLeaveEquivalent")) Then
                _HighDayLeaveEquivalent = dr("HighDayLeaveEquivalent")
            End If
            If Not IsDBNull(dr("LowDayLeaveEquivalent")) Then
                _LowDayLeaveEquivalent = dr("LowDayLeaveEquivalent")
            End If
            If Not IsDBNull(dr("IsFinancial")) Then
                _IsFinancial = dr("IsFinancial")
            End If
            If Not IsDBNull(dr("LowRate")) Then
                _LowRate = dr("LowRate")
            End If
            If Not IsDBNull(dr("BeforeAfterSchedule")) Then
                _BeforeAfterSchedule = dr("BeforeAfterSchedule")
            End If
            If Not IsDBNull(dr("isLostFromHighOT")) Then
                _isLostFromHighOT = dr("isLostFromHighOT")
            End If
            If Not IsDBNull(dr("HighRate")) Then
                _HighRate = dr("HighRate")
            End If
            If Not IsDBNull(dr("MaxOvertime")) Then
                _MaxOvertime = dr("MaxOvertime")
            End If
            If Not IsDBNull(dr("FK_NormalTypeId")) Then
                _FK_NormalTypeId = dr("FK_NormalTypeId")
            End If
            If Not IsDBNull(dr("FK_OffDayTypeId")) Then
                _FK_OffDayTypeId = dr("FK_OffDayTypeId")
            End If
            If Not IsDBNull(dr("FK_HolidayTypeId")) Then
                _FK_HolidayTypeId = dr("FK_HolidayTypeId")
            End If
            If Not IsDBNull(dr("FK_ReligionHolidayTypeId")) Then
                _FK_ReligionHolidayTypeId = dr("FK_ReligionHolidayTypeId")
            End If
            If Not IsDBNull(dr("MinAutoApproveDuration")) Then
                _MinAutoApproveDuration = dr("MinAutoApproveDuration")
            End If
            Return Me
        End Function
        Public Function Add_OvertimeRules_HighTime(ByVal Dt As DataTable) As Integer
            Return objDALOvertimeRules.Add_OvertimeRules_HighTime(Dt)
        End Function
        Public Function GetAll_OvertimeRules_HighTime() As DataTable
            Return objDALOvertimeRules.GetAll_OvertimeRules_HighTime(_OvertimeRuleId)
        End Function
        Public Function Delete_OvertimeRules_HighTime() As Integer
            Return objDALOvertimeRules.Delete_OvertimeRules_HighTime(_OvertimeRuleId)
        End Function
#End Region

    End Class
End Namespace