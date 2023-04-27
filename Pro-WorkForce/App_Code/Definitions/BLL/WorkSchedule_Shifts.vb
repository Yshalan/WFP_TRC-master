Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Definitions

    Public Class WorkSchedule_Shifts

#Region "Class Variables"

        Private _ShiftId As Integer
        Private _FK_ScheduleId As Integer
        Private _ShiftCode As String
        Private _ShiftName As String
        Private _ShiftArabicName As String
        Private _FromTime1 As String
        Private _ToTime1 As String
        Private _FromTime2 As String
        Private _ToTime2 As String
        Private _Color As String
        Private _IsOffDay As Boolean
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALWorkSchedule_Shifts As DALWorkSchedule_Shifts
        Private _FlexTime1 As Integer
        Private _FlexTime2 As Integer
        Private _ScheduleTime As String


#End Region

#Region "Public Properties"

        Public Property ShiftId() As Integer
            Set(ByVal value As Integer)
                _ShiftId = value
            End Set
            Get
                Return (_ShiftId)
            End Get
        End Property

        Public Property FK_ScheduleId() As Integer
            Set(ByVal value As Integer)
                _FK_ScheduleId = value
            End Set
            Get
                Return (_FK_ScheduleId)
            End Get
        End Property

        Public Property ShiftCode() As String
            Set(ByVal value As String)
                _ShiftCode = value
            End Set
            Get
                Return (_ShiftCode)
            End Get
        End Property

        Public Property ShiftName() As String
            Set(ByVal value As String)
                _ShiftName = value
            End Set
            Get
                Return (_ShiftName)
            End Get
        End Property

        Public Property ShiftArabicName() As String
            Set(ByVal value As String)
                _ShiftArabicName = value
            End Set
            Get
                Return (_ShiftArabicName)
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

        Public Property Color() As String
            Set(ByVal value As String)
                _Color = value
            End Set
            Get
                Return (_Color)
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

        Public Property FlexTime1() As Integer
            Set(ByVal value As Integer)
                _FlexTime1 = value
            End Set
            Get
                Return (_FlexTime1)
            End Get
        End Property

        Public Property FlexTime2() As Integer
            Set(ByVal value As Integer)
                _FlexTime2 = value
            End Set
            Get
                Return (_FlexTime2)
            End Get
        End Property

        Public Property ScheduleTime() As String
            Set(ByVal value As String)
                _ScheduleTime = value
            End Set
            Get
                Return (_ScheduleTime)
            End Get
        End Property

#End Region

#Region "Constructor"

        Public Sub New()

            objDALWorkSchedule_Shifts = New DALWorkSchedule_Shifts()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALWorkSchedule_Shifts.Add(_ShiftId, _FK_ScheduleId, _ShiftCode, _ShiftName, _ShiftArabicName, _FromTime1, _ToTime1, _FromTime2, _ToTime2, _Color, _IsOffDay, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE, FlexTime1, FlexTime2)
            App_EventsLog.Insert_ToEventLog("Add", _ShiftId, "WorkSchedule_Shifts", "Define Shift Schedule")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALWorkSchedule_Shifts.Update(_ShiftId, _FK_ScheduleId, _ShiftCode, _ShiftName, _ShiftArabicName, _FromTime1, _ToTime1, _FromTime2, _ToTime2, _Color, _IsOffDay, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE, FlexTime1, FlexTime2)
            App_EventsLog.Insert_ToEventLog("Edit", _ShiftId, "WorkSchedule_Shifts", "Define Shift Schedule")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALWorkSchedule_Shifts.Delete(_ShiftId)
            App_EventsLog.Insert_ToEventLog("Delete", _ShiftId, "WorkSchedule_Shifts", "Define Shift Schedule")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALWorkSchedule_Shifts.GetAll()

        End Function

        Public Function GetByFKScheduleID() As DataTable

            Return objDALWorkSchedule_Shifts.GetByFKScheduleID(_FK_ScheduleId)

        End Function
        'Added for Advanced Schedule Legend
        Public Function GetScheduleDetails() As DataTable
            Return objDALWorkSchedule_Shifts.GetShiftDetailsbyWorkScheduleId(_FK_ScheduleId)

            'Return objDALWorkSchedule_Shifts.GetScheduleDetails(_FK_ScheduleId)

        End Function
        Public Function GetShiftDetailsbyWorkScheduleId() As DataTable

            Return objDALWorkSchedule_Shifts.GetShiftDetailsbyWorkScheduleId(_FK_ScheduleId)

        End Function
        Public Function GetShiftDetailsbyWorkScheduleIdForReport() As DataTable

            Return objDALWorkSchedule_Shifts.GetShiftDetailsbyWorkScheduleIdForReport(_FK_ScheduleId)

        End Function



        Public Function GetByPK() As WorkSchedule_Shifts

            Dim dr As DataRow
            dr = objDALWorkSchedule_Shifts.GetByPK(_ShiftId)

            If Not IsDBNull(dr("ShiftId")) Then
                _ShiftId = dr("ShiftId")
            End If
            If Not IsDBNull(dr("FK_ScheduleId")) Then
                _FK_ScheduleId = dr("FK_ScheduleId")
            End If
            If Not IsDBNull(dr("ShiftCode")) Then
                _ShiftCode = dr("ShiftCode")
            End If
            If Not IsDBNull(dr("ShiftName")) Then
                _ShiftName = dr("ShiftName")
            End If
            If Not IsDBNull(dr("ShiftArabicName")) Then
                _ShiftArabicName = dr("ShiftArabicName")
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
            If Not IsDBNull(dr("Color")) Then
                _Color = dr("Color")
            End If
            If Not IsDBNull(dr("IsOffDay")) Then
                _IsOffDay = dr("IsOffDay")
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
            If Not IsDBNull(dr("FlexTime1")) Then
                _FlexTime1 = dr("FlexTime1")
            End If
            If Not IsDBNull(dr("FlexTime2")) Then
                _FlexTime2 = dr("FlexTime2")
            End If
            Return Me
        End Function

        Public Function GetShiftsByDate() As DataTable

            Return objDALWorkSchedule_Shifts.GetAll()

        End Function

        Public Function DeleteByFKSchedleID() As Integer

            Return objDALWorkSchedule_Shifts.DeleteByFKSchedleID(_FK_ScheduleId)

        End Function
        'Added by Kiran :Employee Leave Details JSON
        Public Function GetEmployeeLeaveDetails_JSON(ByVal year As Integer, ByVal month As Integer) As String

            Dim builder As New StringBuilder

            Dim objWorkSchedule_Shifts As New WorkSchedule_Shifts()
            Dim dtWorkSchedule_Shifts As DataTable = objDALWorkSchedule_Shifts.GetEmployeeLeaveDetails(year, month)
            builder.Append("[")
            For Each row As DataRow In dtWorkSchedule_Shifts.Rows
                builder.Append("{ ""theday"": """)
                builder.Append(row("TheDay"))
                builder.Append(""", ""EmployeeId"": """)
                builder.Append(row("Fk_EmployeeId"))
                builder.Append(""", ""LeaveTypeId"": """)
                builder.Append(row("Fk_LeaveTypeId"))
                builder.Append(""" },")
            Next
            builder.Remove(builder.ToString().Length - 1, 1)
            builder.Append("]")
            Return builder.ToString()

        End Function

#End Region

    End Class
End Namespace