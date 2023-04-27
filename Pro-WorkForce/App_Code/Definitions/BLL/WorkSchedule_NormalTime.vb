Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Definitions

    Public Class WorkSchedule_NormalTime

#Region "Class Variables"


        Private _FK_ScheduleId As Integer
        Private _DayId As Integer
        Private _FromTime1 As String
        Private _ToTime1 As String
        Private _FromTime2 As String
        Private _ToTime2 As String
        Private _IsOffDay As Boolean
        Private objDALWorkSchedule_NormalTime As DALWorkSchedule_NormalTime

#End Region

#Region "Public Properties"


        Public Property FK_ScheduleId() As Integer
            Set(ByVal value As Integer)
                _FK_ScheduleId = value
            End Set
            Get
                Return (_FK_ScheduleId)
            End Get
        End Property


        Public Property DayId() As Integer
            Set(ByVal value As Integer)
                _DayId = value
            End Set
            Get
                Return (_DayId)
            End Get
        End Property


        Public Property FromTime1() As String
            Set(ByVal value As String)
                _FromTime1 = value
            End Set
            Get
                Return (_FromTime1)
            End Get
        End Property


        Public Property ToTime1() As String
            Set(ByVal value As String)
                _ToTime1 = value
            End Set
            Get
                Return (_ToTime1)
            End Get
        End Property


        Public Property FromTime2() As String
            Set(ByVal value As String)
                _FromTime2 = value
            End Set
            Get
                Return (_FromTime2)
            End Get
        End Property


        Public Property ToTime2() As String
            Set(ByVal value As String)
                _ToTime2 = value
            End Set
            Get
                Return (_ToTime2)
            End Get
        End Property


        Public Property IsOffDay() As Boolean
            Set(ByVal value As Boolean)
                _IsOffDay = value
            End Set
            Get
                Return (_IsOffDay)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALWorkSchedule_NormalTime = New DALWorkSchedule_NormalTime()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALWorkSchedule_NormalTime.Add(_FK_ScheduleId, _DayId, _FromTime1, _ToTime1, _FromTime2, _ToTime2, _IsOffDay)
            App_EventsLog.Insert_ToEventLog("Add", _FK_ScheduleId, "WorkSchedule_NormalTime", "Define Work Schedule")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALWorkSchedule_NormalTime.Update(_FK_ScheduleId, _DayId, _FromTime1, _ToTime1, _FromTime2, _ToTime2, _IsOffDay)
            App_EventsLog.Insert_ToEventLog("Edit", _FK_ScheduleId, "WorkSchedule_NormalTime", "Define Work Schedule")
            Return rslt
        End Function



        Public Function Delete() As Integer

            Dim rslt As Integer = objDALWorkSchedule_NormalTime.Delete(_FK_ScheduleId)
            App_EventsLog.Insert_ToEventLog("Delete", _FK_ScheduleId, "WorkSchedule_NormalTime", "Define Work Schedule")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALWorkSchedule_NormalTime.GetAll(_FK_ScheduleId)

        End Function

        Public Function GetByPK() As WorkSchedule_NormalTime

            Dim dr As DataRow
            dr = objDALWorkSchedule_NormalTime.GetByPK(_FK_ScheduleId, _DayId)

            If Not IsDBNull(dr("FK_ScheduleId")) Then
                _FK_ScheduleId = dr("FK_ScheduleId")
            End If
            If Not IsDBNull(dr("DayId")) Then
                _DayId = dr("DayId")
            End If
            If Not IsDBNull(dr("FromTime1")) Then
                _FromTime1 = dr("FromTime1")
            End If
            If Not IsDBNull(dr("ToTime1")) Then
                _ToTime1 = dr("ToTime1")
            End If
            If Not IsDBNull(dr("FromTime2")) Then
                _FromTime2 = dr("FromTime2")
            End If
            If Not IsDBNull(dr("ToTime2")) Then
                _ToTime2 = dr("ToTime2")
            End If
            If Not IsDBNull(dr("IsOffDay")) Then
                _IsOffDay = dr("IsOffDay")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace