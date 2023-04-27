Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.DailyTasks

    Public Class TAPolicy

#Region "Class Variables"

        Private _TAPolicyId As Integer
        Private _TAPolicyName As String
        Private _TAPolicyArabicName As String
        Private _GraceInMins As Integer
        Private _GraceOutMins As Integer
        Private _DelayIsFromGrace As Boolean
        Private _EarlyOutIsFromGrace As Boolean
        Private _Active As Boolean
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALTAPolicy As DALTAPolicy
        Private _HasLaunchBreak As Boolean
        Private _LaunchBreakDuration As Integer
        Private _FK_LaunchBreakReason As Integer
        Private _CompensateLaunchBreak As Boolean
        Private _HasPrayTime As Boolean
        Private _PrayTimeDuration As Integer
        Private _FK_PrayTimeReason As Integer
        Private _CompensatePrayTime As Boolean
        Private _HasReasonBreakTime As Boolean
        Private _LaunchBreakFromTime As Integer
        Private _LaunchBreakToTime As Integer
        Private _ConsiderFirstIn_LastOutOnly As Boolean
        Private _MinDurationAsViolation As String
        Private _MonthlyAllowedGraceOutTime As Integer
        Private _IgnoreEarlyOut_WithinGrace As Boolean
        Private _ConsiderAbsentIfNotCompleteNoOfHours As Boolean
        Private _NoOfNotCompleteHours As Integer
        Private _ConsiderAbsentIfStudyNurs_NotCompleteHrs As Boolean
        Private _NoOfNotCompleteHours_StudyNurs As Integer
        Private _NoOfAllowedPrays As Integer
        Private _PrayBreakFromTime As Integer
        Private _PrayBreakToTime As Integer

#End Region

#Region "Public Properties"
        
        Public Property TAPolicyId() As Integer
            Set(ByVal value As Integer)
                _TAPolicyId = value
            End Set
            Get
                Return (_TAPolicyId)
            End Get
        End Property

        Public Property TAPolicyName() As String
            Set(ByVal value As String)
                _TAPolicyName = value
            End Set
            Get
                Return (_TAPolicyName)
            End Get
        End Property

        Public Property TAPolicyArabicName() As String
            Set(ByVal value As String)
                _TAPolicyArabicName = value
            End Set
            Get
                Return (_TAPolicyArabicName)
            End Get
        End Property

        Public Property GraceInMins() As Integer
            Set(ByVal value As Integer)
                _GraceInMins = value
            End Set
            Get
                Return (_GraceInMins)
            End Get
        End Property

        Public Property GraceOutMins() As Integer
            Set(ByVal value As Integer)
                _GraceOutMins = value
            End Set
            Get
                Return (_GraceOutMins)
            End Get
        End Property

        Public Property DelayIsFromGrace() As Boolean
            Set(ByVal value As Boolean)
                _DelayIsFromGrace = value
            End Set
            Get
                Return (_DelayIsFromGrace)
            End Get
        End Property

        Public Property EarlyOutIsFromGrace() As Boolean
            Set(ByVal value As Boolean)
                _EarlyOutIsFromGrace = value
            End Set
            Get
                Return (_EarlyOutIsFromGrace)
            End Get
        End Property

        Public Property Active() As Boolean
            Set(ByVal value As Boolean)
                _Active = value
            End Set
            Get
                Return (_Active)
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

        Public Property HasLaunchBreak() As Boolean
            Get
                Return _HasLaunchBreak
            End Get
            Set(ByVal value As Boolean)
                _HasLaunchBreak = value
            End Set
        End Property

        Public Property LaunchBreakDuration() As Integer
            Get
                Return _LaunchBreakDuration
            End Get
            Set(ByVal value As Integer)
                _LaunchBreakDuration = value
            End Set
        End Property

        Public Property FK_LaunchBreakReason() As Integer
            Get
                Return _FK_LaunchBreakReason
            End Get
            Set(ByVal value As Integer)
                _FK_LaunchBreakReason = value
            End Set
        End Property

        Public Property CompensateLaunchbreak() As Boolean
            Get
                Return _CompensateLaunchbreak
            End Get
            Set(ByVal value As Boolean)
                _CompensateLaunchbreak = value
            End Set
        End Property

        Public Property HasPrayTime() As Boolean
            Get
                Return _HasPrayTime
            End Get
            Set(ByVal value As Boolean)
                _HasPrayTime = value
            End Set
        End Property

        Public Property PrayTimeDuration() As Integer
            Get
                Return _PrayTimeDuration
            End Get
            Set(ByVal value As Integer)
                _PrayTimeDuration = value
            End Set
        End Property

        Public Property FK_PrayTimeReason() As Integer
            Get
                Return _FK_PrayTimeReason
            End Get
            Set(ByVal value As Integer)
                _FK_PrayTimeReason = value
            End Set
        End Property

        Public Property CompensatePrayTime() As Boolean
            Get
                Return _CompensatePrayTime
            End Get
            Set(ByVal value As Boolean)
                _CompensatePrayTime = value
            End Set
        End Property

        Public Property HasReasonBreakTime() As Boolean
            Set(ByVal value As Boolean)
                _HasReasonBreakTime = value
            End Set
            Get
                Return (_HasReasonBreakTime)
            End Get
        End Property

        Public Property LaunchBreakFromTime() As Integer
            Set(ByVal value As Integer)
                _LaunchBreakFromTime = value
            End Set
            Get
                Return (_LaunchBreakFromTime)
            End Get
        End Property

        Public Property LaunchBreakToTime() As Integer
            Set(ByVal value As Integer)
                _LaunchBreakToTime = value
            End Set
            Get
                Return (_LaunchBreakToTime)
            End Get
        End Property

        Public Property ConsiderFirstIn_LastOutOnly() As Boolean
            Set(ByVal value As Boolean)
                _ConsiderFirstIn_LastOutOnly = value
            End Set
            Get
                Return (_ConsiderFirstIn_LastOutOnly)
            End Get
        End Property

        Public Property MinDurationAsViolation() As String
            Set(ByVal value As String)
                _MinDurationAsViolation = value
            End Set
            Get
                Return (_MinDurationAsViolation)
            End Get
        End Property

        Public Property MonthlyAllowedGraceOutTime() As Integer
            Set(ByVal value As Integer)
                _MonthlyAllowedGraceOutTime = value
            End Set
            Get
                Return (_MonthlyAllowedGraceOutTime)
            End Get
        End Property

        Public Property IgnoreEarlyOut_WithinGrace() As Boolean
            Set(ByVal value As Boolean)
                _IgnoreEarlyOut_WithinGrace = value
            End Set
            Get
                Return (_IgnoreEarlyOut_WithinGrace)
            End Get
        End Property

        Public Property ConsiderAbsentIfNotCompleteNoOfHours() As Boolean
            Set(ByVal value As Boolean)
                _ConsiderAbsentIfNotCompleteNoOfHours = value
            End Set
            Get
                Return (_ConsiderAbsentIfNotCompleteNoOfHours)
            End Get
        End Property

        Public Property NoOfNotCompleteHours() As Integer
            Set(ByVal value As Integer)
                _NoOfNotCompleteHours = value
            End Set
            Get
                Return (_NoOfNotCompleteHours)
            End Get
        End Property

        Public Property ConsiderAbsentIfStudyNurs_NotCompleteHrs() As Boolean
            Set(ByVal value As Boolean)
                _ConsiderAbsentIfStudyNurs_NotCompleteHrs = value
            End Set
            Get
                Return (_ConsiderAbsentIfStudyNurs_NotCompleteHrs)
            End Get
        End Property

        Public Property NoOfNotCompleteHours_StudyNurs() As Integer
            Set(ByVal value As Integer)
                _NoOfNotCompleteHours_StudyNurs = value
            End Set
            Get
                Return (_NoOfNotCompleteHours_StudyNurs)
            End Get
        End Property

        Public Property NoOfAllowedPrays() As Integer
            Set(ByVal value As Integer)
                _NoOfAllowedPrays = value
            End Set
            Get
                Return (_NoOfAllowedPrays)
            End Get
        End Property

        Public Property PrayBreakFromTime() As Integer
            Set(ByVal value As Integer)
                _PrayBreakFromTime = value
            End Set
            Get
                Return (_PrayBreakFromTime)
            End Get
        End Property

        Public Property PrayBreakToTime() As Integer
            Set(ByVal value As Integer)
                _PrayBreakToTime = value
            End Set
            Get
                Return (_PrayBreakToTime)
            End Get
        End Property

#End Region

#Region "Constructor"

        Public Sub New()

            objDALTAPolicy = New DALTAPolicy()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALTAPolicy.Add(_TAPolicyName, _TAPolicyArabicName, _GraceInMins, _GraceOutMins, _DelayIsFromGrace, _EarlyOutIsFromGrace, _Active, _CREATED_BY, _HasLaunchBreak, _LaunchBreakDuration, _CompensateLaunchBreak, _FK_LaunchBreakReason, _HasPrayTime, _PrayTimeDuration, _CompensatePrayTime, _FK_PrayTimeReason, _HasReasonBreakTime, _LaunchBreakFromTime, _LaunchBreakToTime, _ConsiderFirstIn_LastOutOnly, _MinDurationAsViolation, _MonthlyAllowedGraceOutTime, _IgnoreEarlyOut_WithinGrace, _ConsiderAbsentIfNotCompleteNoOfHours, _NoOfNotCompleteHours, _ConsiderAbsentIfStudyNurs_NotCompleteHrs, _NoOfNotCompleteHours_StudyNurs, _NoOfAllowedPrays, _PrayBreakFromTime, _PrayBreakToTime)
            _TAPolicyId = objDALTAPolicy.intTAPolicyId
            App_EventsLog.Insert_ToEventLog("Add", _TAPolicyId, "TA Policy", "TA Policies")
            Return rslt
        End Function

        Public Function Update() As Integer
            Dim rslt As Integer = objDALTAPolicy.Update(_TAPolicyId, _TAPolicyName, _TAPolicyArabicName, _GraceInMins, _GraceOutMins, _DelayIsFromGrace, _EarlyOutIsFromGrace, _Active, _LAST_UPDATE_BY, _HasLaunchBreak, _LaunchBreakDuration, _CompensateLaunchBreak, _FK_LaunchBreakReason, _HasPrayTime, _PrayTimeDuration, _CompensatePrayTime, _FK_PrayTimeReason, _HasReasonBreakTime, _LaunchBreakFromTime, _LaunchBreakToTime, _ConsiderFirstIn_LastOutOnly, _MinDurationAsViolation, _MonthlyAllowedGraceOutTime, _IgnoreEarlyOut_WithinGrace, _ConsiderAbsentIfNotCompleteNoOfHours, _NoOfNotCompleteHours, _ConsiderAbsentIfStudyNurs_NotCompleteHrs, _NoOfNotCompleteHours_StudyNurs, _NoOfAllowedPrays, _PrayBreakFromTime, _PrayBreakToTime)
            App_EventsLog.Insert_ToEventLog("Edit", _TAPolicyId, "TA Policy", "TA Policies")
            Return rslt

        End Function

        Public Function Delete() As Integer
            Dim rslt As Integer = objDALTAPolicy.Delete(_TAPolicyId)
            App_EventsLog.Insert_ToEventLog("Delete", _TAPolicyId, "TA Policy", "TA Policies")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALTAPolicy.GetAll()

        End Function

        Public Function GetByPK() As TAPolicy

            Dim dr As DataRow
            dr = objDALTAPolicy.GetByPK(_TAPolicyId)

            If Not IsDBNull(dr("TAPolicyId")) Then
                _TAPolicyId = dr("TAPolicyId")
            End If
            If Not IsDBNull(dr("TAPolicyName")) Then
                _TAPolicyName = dr("TAPolicyName")
            End If
            If Not IsDBNull(dr("TAPolicyArabicName")) Then
                _TAPolicyArabicName = dr("TAPolicyArabicName")
            End If
            If Not IsDBNull(dr("GraceInMins")) Then
                _GraceInMins = dr("GraceInMins")
            End If
            If Not IsDBNull(dr("GraceOutMins")) Then
                _GraceOutMins = dr("GraceOutMins")
            End If
            If Not IsDBNull(dr("DelayIsFromGrace")) Then
                _DelayIsFromGrace = dr("DelayIsFromGrace")
            End If
            If Not IsDBNull(dr("EarlyOutIsFromGrace")) Then
                _EarlyOutIsFromGrace = dr("EarlyOutIsFromGrace")
            End If
            If Not IsDBNull(dr("Active")) Then
                _Active = dr("Active")
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
            If Not IsDBNull(dr("HasLaunchBreak")) Then
                _HasLaunchBreak = dr("HasLaunchBreak")
            End If
            If Not IsDBNull(dr("LaunchDuration")) Then
                _LaunchBreakDuration = dr("LaunchDuration")
            End If
            If Not IsDBNull(dr("CompensateLaunchbreak")) Then
                _CompensateLaunchBreak = dr("CompensateLaunchbreak")
            End If
            If Not IsDBNull(dr("FK_launchbreakReason")) Then
                _FK_LaunchBreakReason = dr("FK_launchbreakReason")
            End If
            If Not IsDBNull(dr("HasPrayTime")) Then
                _HasPrayTime = dr("HasPrayTime")
            End If
            If Not IsDBNull(dr("PrayTimeDuration")) Then
                _PrayTimeDuration = dr("PrayTimeDuration")
            End If
            If Not IsDBNull(dr("CompensatePrayTime")) Then
                _CompensatePrayTime = dr("CompensatePrayTime")
            End If
            If Not IsDBNull(dr("FK_PrayTimeReason")) Then
                _FK_PrayTimeReason = dr("FK_PrayTimeReason")
            End If
            If Not IsDBNull(dr("HasReasonBreakTime")) Then
                _HasReasonBreakTime = dr("HasReasonBreakTime")
            End If
            If Not IsDBNull(dr("LaunchBreakFromTime")) Then
                _LaunchBreakFromTime = dr("LaunchBreakFromTime")
            End If
            If Not IsDBNull(dr("LaunchBreakToTime")) Then
                _LaunchBreakToTime = dr("LaunchBreakToTime")
            End If
            If Not IsDBNull(dr("ConsiderFirstIn_LastOutOnly")) Then
                _ConsiderFirstIn_LastOutOnly = dr("ConsiderFirstIn_LastOutOnly")
            End If
            If Not IsDBNull(dr("MinDurationAsViolation")) Then
                _MinDurationAsViolation = dr("MinDurationAsViolation")
            End If
            If Not IsDBNull(dr("MonthlyAllowedGraceOutTime")) Then
                _MonthlyAllowedGraceOutTime = dr("MonthlyAllowedGraceOutTime")
            End If
            If Not IsDBNull(dr("IgnoreEarlyOut_WithinGrace")) Then
                _IgnoreEarlyOut_WithinGrace = dr("IgnoreEarlyOut_WithinGrace")
            End If
            If Not IsDBNull(dr("ConsiderAbsentIfNotCompleteNoOfHours")) Then
                _ConsiderAbsentIfNotCompleteNoOfHours = dr("ConsiderAbsentIfNotCompleteNoOfHours")
            End If
            If Not IsDBNull(dr("NoOfNotCompleteHours")) Then
                _NoOfNotCompleteHours = dr("NoOfNotCompleteHours")
            End If
            If Not IsDBNull(dr("ConsiderAbsentIfStudyNurs_NotCompleteHrs")) Then
                _ConsiderAbsentIfStudyNurs_NotCompleteHrs = dr("ConsiderAbsentIfStudyNurs_NotCompleteHrs")
            End If
            If Not IsDBNull(dr("NoOfNotCompleteHours_StudyNurs")) Then
                _NoOfNotCompleteHours_StudyNurs = dr("NoOfNotCompleteHours_StudyNurs")
            End If
            If Not IsDBNull(dr("NoOfAllowedPrays")) Then
                _NoOfAllowedPrays = dr("NoOfAllowedPrays")
            End If
            If Not IsDBNull(dr("PrayBreakFromTime")) Then
                _PrayBreakFromTime = dr("PrayBreakFromTime")
            End If
            If Not IsDBNull(dr("PrayBreakToTime")) Then
                _PrayBreakToTime = dr("PrayBreakToTime")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace