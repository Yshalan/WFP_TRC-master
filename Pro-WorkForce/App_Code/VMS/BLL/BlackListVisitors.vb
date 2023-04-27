Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Visitors

    Public Class BlackListVisitors

#Region "Class Variables"

        Private _BlacklistId As Long
        Private _IDNumber As String
        Private _VisitorName As String
        Private _VisitorArabicName As String
        Private _Nationality As Integer
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALBlackListVisitors As DALBlackListVisitors

#End Region

#Region "Public Properties"


        Public Property BlacklistId() As Long
            Set(ByVal value As Long)
                _BlacklistId = value
            End Set
            Get
                Return (_BlacklistId)
            End Get
        End Property

        Public Property IDNumber() As String
            Set(ByVal value As String)
                _IDNumber = value
            End Set
            Get
                Return (_IDNumber)
            End Get
        End Property

        Public Property VisitorName() As String
            Set(ByVal value As String)
                _VisitorName = value
            End Set
            Get
                Return (_VisitorName)
            End Get
        End Property

        Public Property VisitorArabicName() As String
            Set(ByVal value As String)
                _VisitorArabicName = value
            End Set
            Get
                Return (_VisitorArabicName)
            End Get
        End Property

        Public Property Nationality() As Integer
            Set(ByVal value As Integer)
                _Nationality = value
            End Set
            Get
                Return (_Nationality)
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

#End Region


#Region "Constructor"

        Public Sub New()

            objDALBlackListVisitors = New DALBlackListVisitors()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALBlackListVisitors.Add(_BlacklistId, _IDNumber, _VisitorName, _VisitorArabicName, _Nationality, _CREATED_BY)
            App_EventsLog.Insert_ToEventLog("Add", _BlacklistId, "BlackListVisitors", "BlackList Visitors")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALBlackListVisitors.Update(_BlacklistId, _IDNumber, _VisitorName, _VisitorArabicName, _Nationality, _LAST_UPDATE_BY)
            App_EventsLog.Insert_ToEventLog("Update", _BlacklistId, "BlackListVisitors", "BlackList Visitors")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALBlackListVisitors.Delete(_BlacklistId)
            App_EventsLog.Insert_ToEventLog("Delete", _BlacklistId, "BlackListVisitors", "BlackList Visitors")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALBlackListVisitors.GetAll()

        End Function

        Public Function GetAll_Inner() As DataTable

            Return objDALBlackListVisitors.GetAll_Inner()

        End Function

        Public Function GetByPK() As BlackListVisitors

            Dim dr As DataRow
            dr = objDALBlackListVisitors.GetByPK(_BlacklistId)

            If Not IsDBNull(dr("BlacklistId")) Then
                _BlacklistId = dr("BlacklistId")
            End If
            If Not IsDBNull(dr("IDNumber")) Then
                _IDNumber = dr("IDNumber")
            End If
            If Not IsDBNull(dr("VisitorName")) Then
                _VisitorName = dr("VisitorName")
            End If
            If Not IsDBNull(dr("VisitorArabicName")) Then
                _VisitorArabicName = dr("VisitorArabicName")
            End If
            If Not IsDBNull(dr("Nationality")) Then
                _Nationality = dr("Nationality")
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
            Return Me
        End Function

#End Region

    End Class
End Namespace