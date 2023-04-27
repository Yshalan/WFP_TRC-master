Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.DailyTasks

    Public Class Schedule_Company

#Region "Class Variables"


        Private _EmpWorkScheduleId As Long
        Private _FK_CompanyId As Integer
        Private _FK_ScheduleId As Integer
        Private _ScheduleType As Integer
        Private _FromDate As DateTime
        Private _ToDate As DateTime
        Private _IsTemporary As Boolean
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALSchedule_Company As DALSchedule_Company

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

        Public Property FK_CompanyId() As Integer
            Set(ByVal value As Integer)
                _FK_CompanyId = value
            End Set
            Get
                Return (_FK_CompanyId)
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

            objDALSchedule_Company = New DALSchedule_Company()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALSchedule_Company.Add(_EmpWorkScheduleId, _FK_CompanyId, _FK_ScheduleId, _ScheduleType, _FromDate, _ToDate, _IsTemporary)
            App_EventsLog.Insert_ToEventLog("Add", _EmpWorkScheduleId, "Schedule_Company", "Assign Schedule")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALSchedule_Company.Update(_EmpWorkScheduleId, _FK_CompanyId, _FK_ScheduleId, _ScheduleType, _FromDate, _ToDate, _IsTemporary, _LAST_UPDATE_BY)
            App_EventsLog.Insert_ToEventLog("Edit", _EmpWorkScheduleId, "Schedule_Company", "Assign Schedule")
            Return rslt
        End Function



        Public Function Delete() As Integer

            Dim rslt As Integer = objDALSchedule_Company.Delete(_EmpWorkScheduleId)
            App_EventsLog.Insert_ToEventLog("Delete", _EmpWorkScheduleId, "Schedule_Company", "Assign Schedule")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALSchedule_Company.GetAll()

        End Function

        Public Function GetByPK() As Schedule_Company

            Dim dr As DataRow
            dr = objDALSchedule_Company.GetByPK(_EmpWorkScheduleId)

            If Not IsDBNull(dr("EmpWorkScheduleId")) Then
                _EmpWorkScheduleId = dr("EmpWorkScheduleId")
            End If
            If Not IsDBNull(dr("FK_CompanyId")) Then
                _FK_CompanyId = dr("FK_CompanyId")
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

        Public Function AssignSchedule_Company() As Integer
            Return objDALSchedule_Company.AssignSchedule_Company(_FK_CompanyId, _FK_ScheduleId, _ScheduleType, _FromDate, _ToDate, _IsTemporary, _CREATED_BY)
        End Function
        Public Function Get_Company_Schedule_Details() As DataTable

            Return objDALSchedule_Company.Get_Company_Schedule_Details()

        End Function
#End Region

    End Class
End Namespace