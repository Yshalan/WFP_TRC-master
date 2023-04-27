Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Definitions

    Public Class RamadanPeriod

#Region "Class Variables"

        Private _RamadanID As Integer
        Private _FromDate As DateTime
        Private _ToDate As DateTime
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private _SearchDate As DateTime
        Private _IsRamadan As Boolean
        Private objDALRamadanPeriod As DALRamadanPeriod

#End Region

#Region "Public Properties"

        Public Property RamadanID() As Integer
            Get
                Return _RamadanID
            End Get
            Set(ByVal value As Integer)
                _RamadanID = value
            End Set
        End Property

        Public Property FromDate() As DateTime
            Get
                Return _FromDate
            End Get
            Set(ByVal value As DateTime)
                _FromDate = value
            End Set
        End Property

        Public Property ToDate() As DateTime
            Get
                Return _ToDate
            End Get
            Set(ByVal value As DateTime)
                _ToDate = value
            End Set
        End Property

        Public Property CREATED_BY() As String
            Get
                Return _CREATED_BY
            End Get
            Set(ByVal value As String)
                _CREATED_BY = value
            End Set
        End Property

        Public Property CREATED_DATE() As DateTime
            Get
                Return _CREATED_DATE
            End Get
            Set(ByVal value As DateTime)
                _CREATED_DATE = value
            End Set
        End Property

        Public Property LAST_UPDATE_BY() As String
            Get
                Return _LAST_UPDATE_BY
            End Get
            Set(ByVal value As String)
                _LAST_UPDATE_BY = value
            End Set
        End Property

        Public Property LAST_UPDATE_DATE() As DateTime
            Get
                Return _LAST_UPDATE_DATE
            End Get
            Set(ByVal value As DateTime)
                _LAST_UPDATE_DATE = value
            End Set
        End Property

        Public Property SearchDate() As DateTime
            Get
                Return _SearchDate
            End Get
            Set(ByVal value As DateTime)
                _SearchDate = value
            End Set
        End Property

        Public Property IsRamadanDay() As Boolean
            Get
                Return _IsRamadan
            End Get
            Set(ByVal value As Boolean)
                _IsRamadan = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Public Sub New()

            objDALRamadanPeriod = New DALRamadanPeriod()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALRamadanPeriod.Add(_RamadanID, _FromDate, _ToDate, _CREATED_BY)
            App_EventsLog.Insert_ToEventLog("Add", _RamadanID, "RamadanPeriod", "RamadanPeriod")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALRamadanPeriod.Update(_RamadanID, _FromDate, _ToDate, _LAST_UPDATE_BY)
            App_EventsLog.Insert_ToEventLog("Edit", _RamadanID, "RamadanPeriod", "RamadanPeriod")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALRamadanPeriod.Delete(_RamadanID)
            App_EventsLog.Insert_ToEventLog("Delete", _RamadanID, "RamadanPeriod", "RamadanPeriod")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALRamadanPeriod.GetAll()

        End Function

        Public Function GetByPK() As RamadanPeriod

            Dim dr As DataRow
            dr = objDALRamadanPeriod.GetByPK(_RamadanID)

            If Not IsDBNull(dr("Id")) Then
                _RamadanID = dr("Id")
            End If

            If Not IsDBNull(dr("FromDate")) Then
                _FromDate = dr("FromDate")
            End If

            If Not IsDBNull(dr("ToDate")) Then
                _ToDate = dr("ToDate")
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

        Public Function Get_IsRamadan() As RamadanPeriod

            Dim dr As DataRow
            dr = objDALRamadanPeriod.Get_IsRamadan(_SearchDate)

            If Not IsDBNull(dr("IsRamadan")) Then
                _IsRamadan = dr("IsRamadan")
            End If

            Return Me

        End Function

#End Region

    End Class

End Namespace
