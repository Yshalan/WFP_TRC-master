Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.ScheduleGroups

    Public Class ScheduleGroups

#Region "Class Variables"


        Private _GroupId As Integer
        Private _GroupCode As String
        Private _GroupNameEn As String
        Private _GroupNameAr As String
        Private _FK_EntityId As Integer
        Private _IsActive As Boolean
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private _WorkDayNo As Integer
        Private _RestDayNo As Integer
        Private objDALScheduleGroups As DALScheduleGroups

#End Region

#Region "Public Properties"

        Public Property GroupId() As Integer
            Set(ByVal value As Integer)
                _GroupId = value
            End Set
            Get
                Return (_GroupId)
            End Get
        End Property

        Public Property GroupCode() As String
            Set(ByVal value As String)
                _GroupCode = value
            End Set
            Get
                Return (_GroupCode)
            End Get
        End Property

        Public Property GroupNameEn() As String
            Set(ByVal value As String)
                _GroupNameEn = value
            End Set
            Get
                Return (_GroupNameEn)
            End Get
        End Property

        Public Property GroupNameAr() As String
            Set(ByVal value As String)
                _GroupNameAr = value
            End Set
            Get
                Return (_GroupNameAr)
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

        Public Property IsActive() As Boolean
            Set(ByVal value As Boolean)
                _IsActive = value
            End Set
            Get
                Return (_IsActive)
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

        Public Property WorkDayNo() As Integer
            Set(ByVal value As Integer)
                _WorkDayNo = value
            End Set
            Get
                Return (_WorkDayNo)
            End Get
        End Property
        Public Property RestDayNo() As Integer
            Set(ByVal value As Integer)
                _RestDayNo = value
            End Set
            Get
                Return (_RestDayNo)
            End Get
        End Property
#End Region


#Region "Constructor"

        Public Sub New()

            objDALScheduleGroups = New DALScheduleGroups()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer
            Dim rslt As Integer = objDALScheduleGroups.Add(_GroupCode, _GroupNameEn, _GroupNameAr, _FK_EntityId, _IsActive, _CREATED_BY, _WorkDayNo, _RestDayNo)
            App_EventsLog.Insert_ToEventLog("Add", _GroupCode, "ScheduleGroups", "Group Schedule")
            Return rslt
        End Function

        Public Function Update() As Integer
            Dim rslt As Integer = objDALScheduleGroups.Update(_GroupId, _GroupCode, _GroupNameEn, _GroupNameAr, _FK_EntityId, _IsActive, _LAST_UPDATE_BY, _WorkDayNo, _RestDayNo)
            App_EventsLog.Insert_ToEventLog("Update", _GroupId, "ScheduleGroups", "Group Schedule")
            Return rslt
        End Function



        Public Function Delete() As Integer
            Dim rslt As Integer = objDALScheduleGroups.Delete(_GroupId)
            App_EventsLog.Insert_ToEventLog("Delete", _GroupId, "ScheduleGroups", "Group Schedule")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALScheduleGroups.GetAll()

        End Function
        Public Function GetAllGroupIDs() As String

            Return objDALScheduleGroups.GetAllGroupIDs()

        End Function
        Public Function GetByPK() As ScheduleGroups

            Dim dr As DataRow
            dr = objDALScheduleGroups.GetByPK(_GroupId)

            If Not IsDBNull(dr("GroupId")) Then
                _GroupId = dr("GroupId")
            End If
            If Not IsDBNull(dr("GroupCode")) Then
                _GroupCode = dr("GroupCode")
            End If
            If Not IsDBNull(dr("GroupNameEn")) Then
                _GroupNameEn = dr("GroupNameEn")
            End If
            If Not IsDBNull(dr("GroupNameAr")) Then
                _GroupNameAr = dr("GroupNameAr")
            End If
            If Not IsDBNull(dr("FK_EntityId")) Then
                _FK_EntityId = dr("FK_EntityId")
            End If
            If Not IsDBNull(dr("IsActive")) Then
                _IsActive = dr("IsActive")
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
            If Not IsDBNull(dr("WorkDayNo")) Then
                _WorkDayNo = dr("WorkDayNo")
            End If
            If Not IsDBNull(dr("RestDayNo")) Then
                _RestDayNo = dr("RestDayNo")
            End If
            Return Me
        End Function
        Public Function GetAllForFill(ByVal lang As Integer) As DataTable

            Return objDALScheduleGroups.GetAllForFill(lang)

        End Function
        Public Function GetAllGroups_JSON(ByVal month As Integer, ByVal year As Integer, ByVal UserId As Integer) As String
            Dim ScheduleDate As New DateTime(year, month, 1)

            Dim dt As DataTable = objDALScheduleGroups.GetByScheduleDate(ScheduleDate, UserId)

            Dim builder As New StringBuilder
            builder.Append("[")

            For Each row As DataRow In dt.Rows
                '"{ ""EmpId"": ""1"", ""ShiftId"": ""A"", ""WorkDate"" },"
                builder.Append("{ ""GroupID"": """)
                builder.Append(row("GroupID"))
                builder.Append(""", ""GroupCode"": """)
                builder.Append(row("GroupCode"))
                builder.Append(""", ""GroupName"": """)
                builder.Append(row("GroupNameEn"))
                builder.Append(""", ""listed"": """)
                builder.Append("false")
                builder.Append(""", },")

            Next

            builder.Append("]")
            Return builder.ToString()

        End Function
#End Region

    End Class
End Namespace