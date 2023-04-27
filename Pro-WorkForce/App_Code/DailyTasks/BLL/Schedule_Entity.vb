Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.DailyTasks

    Public Class Schedule_Entity

#Region "Class Variables"


        Private _EmpWorkScheduleId As Long
        Private _FK_EntityId As Integer
        Private _FK_ScheduleId As Integer
        Private _ScheduleType As Integer
        Private _FromDate As DateTime
        Private _ToDate As DateTime
        Private _IsTemporary As Boolean
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALSchedule_Entity As DALSchedule_Entity

#End Region

#Region "Public Properties"


        Public Property EmpWorkScheduleId() As Long
            Set(ByVal value As Long)
                _EmpWorkScheduleId = value
            End Set
            Get
                Return (_EmpWorkScheduleId)
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

        Public Property FK_ScheduleId() As Integer
            Set(ByVal value As Integer)
                _FK_ScheduleId = value
            End Set
            Get
                Return (_FK_ScheduleId)
            End Get
        End Property

        Public Property ScheduleType() As Integer
            Set(ByVal value As Integer)
                _ScheduleType = value
            End Set
            Get
                Return (_ScheduleType)
            End Get
        End Property

        Public Property FromDate() As DateTime
            Set(ByVal value As DateTime)
                _FromDate = value
            End Set
            Get
                Return (_FromDate)
            End Get
        End Property

        Public Property ToDate() As DateTime
            Set(ByVal value As DateTime)
                _ToDate = value
            End Set
            Get
                Return (_ToDate)
            End Get
        End Property

        Public Property IsTemporary() As Boolean
            Set(ByVal value As Boolean)
                _IsTemporary = value
            End Set
            Get
                Return (_IsTemporary)
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

        Public Property CREATED_DATE As DateTime
            Set(ByVal value As DateTime)
                _CREATED_DATE = value
            End Set
            Get
                Return (_CREATED_DATE)
            End Get
        End Property

        Public Property LAST_UPDATE_BY As String
            Set(ByVal value As String)
                _LAST_UPDATE_BY = value
            End Set
            Get
                Return (_LAST_UPDATE_BY)
            End Get
        End Property

        Public Property LAST_UPDATE_DATE As DateTime
            Set(ByVal value As DateTime)
                _LAST_UPDATE_DATE = value
            End Set
            Get
                Return (_LAST_UPDATE_DATE)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALSchedule_Entity = New DALSchedule_Entity()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALSchedule_Entity.Add(_EmpWorkScheduleId, _FK_EntityId, _FK_ScheduleId, _ScheduleType, _FromDate, _ToDate, _IsTemporary)
            App_EventsLog.Insert_ToEventLog("Add", _EmpWorkScheduleId, "Schedule_Entity", "Assign Schedule")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALSchedule_Entity.Update(_EmpWorkScheduleId, _FK_EntityId, _FK_ScheduleId, _ScheduleType, _FromDate, _ToDate, _IsTemporary, _LAST_UPDATE_BY)
            App_EventsLog.Insert_ToEventLog("Edit", _EmpWorkScheduleId, "Schedule_Entity", "Assign Schedule")
            Return rslt
        End Function



        Public Function Delete() As Integer

            Dim rslt As Integer = objDALSchedule_Entity.Delete(_EmpWorkScheduleId)
            App_EventsLog.Insert_ToEventLog("Delete", _EmpWorkScheduleId, "Schedule_Entity", "Assign Schedule")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALSchedule_Entity.GetAll()

        End Function

        Public Function GetByPK() As Schedule_Entity

            Dim dr As DataRow
            dr = objDALSchedule_Entity.GetByPK(_EmpWorkScheduleId)

            If Not IsDBNull(dr("EmpWorkScheduleId")) Then
                _EmpWorkScheduleId = dr("EmpWorkScheduleId")
            End If
            If Not IsDBNull(dr("FK_EntityId")) Then
                _FK_EntityId = dr("FK_EntityId")
            End If
            If Not IsDBNull(dr("FK_ScheduleId")) Then
                _FK_ScheduleId = dr("FK_ScheduleId")
            End If
            If Not IsDBNull(dr("ScheduleType")) Then
                _ScheduleType = dr("ScheduleType")
            End If
            If Not IsDBNull(dr("FromDate")) Then
                _FromDate = dr("FromDate")
            End If
            If Not IsDBNull(dr("ToDate")) Then
                _ToDate = dr("ToDate")
            End If
            If Not IsDBNull(dr("IsTemporary")) Then
                _IsTemporary = dr("IsTemporary")
            End If
            Return Me
        End Function
        Public Function AssignSchedule_Entity() As Integer

            Return objDALSchedule_Entity.AssignSchedule_Entity(_FK_EntityId, _FK_ScheduleId, _ScheduleType, _FromDate, _ToDate, _IsTemporary, _CREATED_BY)
        End Function
        Public Function Get_Entity_Schedule_Details() As DataTable

            Return objDALSchedule_Entity.Get_Entity_Schedule_Details()

        End Function

#End Region

    End Class
End Namespace