Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Definitions

    Public Class Events_Employees_Schedule

#Region "Class Variables"


        Private _EventScheduleId As Long
        Private _FK_EventId As Integer
        Private _ScheduleDate As DateTime
        Private _FK_EmployeeId As Long
        Private _Shift As Char
        Private objDALEvents_Employees_Schedule As DALEvents_Employees_Schedule

#End Region

#Region "Public Properties"


        Public Property EventScheduleId() As Long
            Set(ByVal value As Long)
                _EventScheduleId = value
            End Set
            Get
                Return (_EventScheduleId)
            End Get
        End Property


        Public Property FK_EventId() As Integer
            Set(ByVal value As Integer)
                _FK_EventId = value
            End Set
            Get
                Return (_FK_EventId)
            End Get
        End Property


        Public Property ScheduleDate() As DateTime
            Set(ByVal value As DateTime)
                _ScheduleDate = value
            End Set
            Get
                Return (_ScheduleDate)
            End Get
        End Property


        Public Property FK_EmployeeId() As Long
            Set(ByVal value As Long)
                _FK_EmployeeId = value
            End Set
            Get
                Return (_FK_EmployeeId)
            End Get
        End Property


        Public Property Shift() As Char
            Set(ByVal value As Char)
                _Shift = value
            End Set
            Get
                Return (_Shift)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALEvents_Employees_Schedule = New DALEvents_Employees_Schedule()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALEvents_Employees_Schedule.Add(_FK_EventId, _ScheduleDate, _FK_EmployeeId, _Shift)
        End Function

        Public Function Update() As Integer

            Return objDALEvents_Employees_Schedule.Update(_EventScheduleId, _FK_EventId, _ScheduleDate, _FK_EmployeeId, _Shift)

        End Function



        Public Function Delete() As Integer

            Return objDALEvents_Employees_Schedule.Delete(_EventScheduleId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALEvents_Employees_Schedule.GetAll()

        End Function

        Public Function GetByPK() As Events_Employees_Schedule

            Dim dr As DataRow
            dr = objDALEvents_Employees_Schedule.GetByPK(_EventScheduleId)

            If Not IsDBNull(dr("EventScheduleId")) Then
                _EventScheduleId = dr("EventScheduleId")
            End If
            If Not IsDBNull(dr("FK_EventId")) Then
                _FK_EventId = dr("FK_EventId")
            End If
            If Not IsDBNull(dr("ScheduleDate")) Then
                _ScheduleDate = dr("ScheduleDate")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("Shift")) Then
                _Shift = dr("Shift")
            End If
            Return Me
        End Function

        Public Function GetAllByEventID() As DataTable
            Return objDALEvents_Employees_Schedule.GetAllByEventID(_FK_EventId)
        End Function

        Public Function DeleteByEventID() As Integer
            Return objDALEvents_Employees_Schedule.DeleteByEventID(_FK_EventId)
        End Function

#End Region

    End Class
End Namespace