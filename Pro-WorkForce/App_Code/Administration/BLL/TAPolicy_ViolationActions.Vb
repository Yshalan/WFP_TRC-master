Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Admin
    Public Class TAPolicy_ViolationActions
#Region "Class Variables"
        Private _ActionId As Integer
        Private _ActionName As String
        Private _ActionArabicName As String
        Private _CREATED_BY As String
        Private _CREATED_DATE As Date
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As Date
        Private objDALTAPolicy_ViolationActions As DALTAPolicy_ViolationActions
#End Region

#Region "Public Properties"
        Public Property ActionId() As Integer
            Set(ByVal value As Integer)
                _ActionId = value
            End Set
            Get
                Return (_ActionId)
            End Get
        End Property
        Public Property ActionName() As String
            Set(ByVal value As String)
                _ActionName = value
            End Set
            Get
                Return (_ActionName)
            End Get
        End Property
        Public Property ActionArabicName() As String
            Set(ByVal value As String)
                _ActionArabicName = value
            End Set
            Get
                Return (_ActionArabicName)
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
        Public Property CREATED_DATE() As Date
            Set(ByVal value As Date)
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
        Public Property LAST_UPDATE_DATE() As Date
            Set(ByVal value As Date)
                _LAST_UPDATE_DATE = value
            End Set
            Get
                Return (_LAST_UPDATE_DATE)
            End Get
        End Property
#End Region

#Region "Constructor"
        Public Sub New()
            objDALTAPolicy_ViolationActions = New DALTAPolicy_ViolationActions()
        End Sub

#End Region

#Region "Methods"
        Public Function Add() As Integer

            Dim rslt As Integer = objDALTAPolicy_ViolationActions.Add(_ActionId, _ActionName, _ActionArabicName, _CREATED_BY)
            App_EventsLog.Insert_ToEventLog("Add", _ActionId, "TAPolicy_ViolationActions", "Violation Actions")
            Return rslt
        End Function

        Public Function Update() As Integer
            Dim rslt As Integer = objDALTAPolicy_ViolationActions.Update(_ActionId, _ActionName, _ActionArabicName, _LAST_UPDATE_BY)
            App_EventsLog.Insert_ToEventLog("Edit", _ActionId, "TAPolicy_ViolationActions", "Violation Actions")
            Return rslt
        End Function

        Public Function Delete() As Integer
            Dim rslt As Integer = objDALTAPolicy_ViolationActions.Delete(_ActionId)
            App_EventsLog.Insert_ToEventLog("Edit", _ActionId, "TAPolicy_ViolationActions", "Violation Actions")
            Return rslt
        End Function

        Public Function GetAll() As DataTable
            Return objDALTAPolicy_ViolationActions.GetAll()
        End Function
        Public Function GetAllForDDL() As DataTable
            Return objDALTAPolicy_ViolationActions.GetAllForDDL()
        End Function

        Public Function GetByPK() As TAPolicy_ViolationActions
            Dim dr As DataRow
            dr = objDALTAPolicy_ViolationActions.GetByPK(_ActionId)
            If Not IsDBNull(dr("ActionId")) Then
                _ActionId = dr("ActionId")
            End If
            If Not IsDBNull(dr("ActionName")) Then
                _ActionName = dr("ActionName")
            End If
            If Not IsDBNull(dr("ActionArabicName")) Then
                _ActionArabicName = dr("ActionArabicName")
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
        End Function

#End Region

    End Class
End Namespace
