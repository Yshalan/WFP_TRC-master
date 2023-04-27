Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Definitions

    Public Class WorkSchedule_Open

#Region "Class Variables"


        Private _FK_ScheduleId As Integer
        Private _DayId As Integer
        Private _FromTime As String
        Private _FlexTime As Integer
        Private _ToTime As String
        Private _WorkHours As String
        Private _IsOffDay As Boolean
        Private objDALWorkSchedule_Open As DALWorkSchedule_Open

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


        Public Property FromTime() As String
            Set(ByVal value As String)
                _FromTime = value
            End Set
            Get
                Return (_FromTime)
            End Get
        End Property


        Public Property FlexTime() As Integer
            Set(ByVal value As Integer)
                _FlexTime = value
            End Set
            Get
                Return (_FlexTime)
            End Get
        End Property


        Public Property ToTime() As String
            Set(ByVal value As String)
                _ToTime = value
            End Set
            Get
                Return (_ToTime)
            End Get
        End Property


        Public Property WorkHours() As String
            Set(ByVal value As String)
                _WorkHours = value
            End Set
            Get
                Return (_WorkHours)
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

            objDALWorkSchedule_Open = New DALWorkSchedule_Open()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALWorkSchedule_Open.Add(_FK_ScheduleId, _DayId, _FromTime, _FlexTime, _ToTime, _WorkHours, _IsOffDay)
            App_EventsLog.Insert_ToEventLog("Add", _FK_ScheduleId, "WorkSchedule_Open", "Define Work Schedule")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALWorkSchedule_Open.Update(_FK_ScheduleId, _DayId, _FromTime, _FlexTime, _ToTime, _WorkHours, _IsOffDay)
            App_EventsLog.Insert_ToEventLog("Update", _FK_ScheduleId, "WorkSchedule_Open", "Define Work Schedule")
            Return rslt
        End Function



        Public Function Delete() As Integer

            Dim rslt As Integer = objDALWorkSchedule_Open.Delete(_FK_ScheduleId, _DayId)
            App_EventsLog.Insert_ToEventLog("Delete", _FK_ScheduleId, "WorkSchedule_Open", "Define Work Schedule")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALWorkSchedule_Open.GetAll(_FK_ScheduleId)

        End Function

        Public Function GetByPK() As WorkSchedule_Open

            Dim dr As DataRow
            dr = objDALWorkSchedule_Open.GetByPK(_FK_ScheduleId, _DayId)

            If Not IsDBNull(dr("FK_ScheduleId")) Then
                _FK_ScheduleId = dr("FK_ScheduleId")
            End If
            If Not IsDBNull(dr("DayId")) Then
                _DayId = dr("DayId")
            End If
            If Not IsDBNull(dr("FromTime")) Then
                _FromTime = dr("FromTime")
            End If
            If Not IsDBNull(dr("FlexTime")) Then
                _FlexTime = dr("FlexTime")
            End If
            If Not IsDBNull(dr("ToTime")) Then
                _ToTime = dr("ToTime")
            End If
            If Not IsDBNull(dr("WorkHours")) Then
                _WorkHours = dr("WorkHours")
            End If
            If Not IsDBNull(dr("IsOffDay")) Then
                _IsOffDay = dr("IsOffDay")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace