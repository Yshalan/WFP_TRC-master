Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Public Class Notification_Exception

#Region "Class Variables"

    Private _NotificationExceptionId As Integer
    Private _FK_EmployeeId As Long
    Private _FromDate As DateTime
    Private _ToDate As DateTime
    Private _Active As Boolean
    Private _Reason As String
    Private _CREATED_BY As String
    Private _CREATED_DATE As DateTime
    Private _LAST_UPDATE_BY As String
    Private _LAST_UPDATE_DATE As DateTime
    Private _FK_LogicalGroupId As Integer
    Private _FK_WorkLocationId As Integer
    Private _FK_EntityId As Integer
    Private objDALNotification_Exception As DALNotification_Exception

#End Region

#Region "Public Properties"
    Public Property NotificationExceptionId() As Integer
        Set(ByVal value As Integer)
            _NotificationExceptionId = value
        End Set
        Get
            Return (_NotificationExceptionId)
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

    Public Property Active() As Boolean
        Set(ByVal value As Boolean)
            _Active = value
        End Set
        Get
            Return (_Active)
        End Get
    End Property

    Public Property Reason() As String
        Set(ByVal value As String)
            _Reason = value
        End Set
        Get
            Return (_Reason)
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

    Public Property FK_LogicalGroupId() As Integer
        Set(ByVal value As Integer)
            _FK_LogicalGroupId = value
        End Set
        Get
            Return (_FK_LogicalGroupId)
        End Get
    End Property

    Public Property FK_WorkLocationId() As Integer
        Set(ByVal value As Integer)
            _FK_WorkLocationId = value
        End Set
        Get
            Return (_FK_WorkLocationId)
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
#End Region


#Region "Constructor"

    Public Sub New()

        objDALNotification_Exception = New DALNotification_Exception()

    End Sub

#End Region

#Region "Methods"

    Public Function Add() As Integer

        Dim rslt As Integer = objDALNotification_Exception.Add(_NotificationExceptionId, _FK_EmployeeId, _FromDate, _ToDate, _Active, _Reason, _CREATED_BY, _FK_LogicalGroupId, _FK_WorkLocationId, _FK_EntityId)
        App_EventsLog.Insert_ToEventLog("Add", _NotificationExceptionId, "Notification_Exception", "Notification Exceptions")
        Return rslt
    End Function

    Public Function Update() As Integer

        Dim rslt As Integer = objDALNotification_Exception.Update(_NotificationExceptionId, _FK_EmployeeId, _FromDate, _ToDate, _Reason, _LAST_UPDATE_BY, _FK_LogicalGroupId, _FK_WorkLocationId, _FK_EntityId)
        App_EventsLog.Insert_ToEventLog("Update", _NotificationExceptionId, "Notification_Exception", "Notification Exceptions")
        Return rslt
    End Function



    Public Function Delete() As Integer

        Dim rslt As Integer = objDALNotification_Exception.Delete(_NotificationExceptionId)
        App_EventsLog.Insert_ToEventLog("Delete", _NotificationExceptionId, "Notification_Exception", "Notification Exceptions")
        Return rslt
    End Function

    Public Function GetAll() As DataTable

        Return objDALNotification_Exception.GetAll()

    End Function

    Public Function GetByPK() As Notification_Exception

        Dim dr As DataRow
        dr = objDALNotification_Exception.GetByPK(NotificationExceptionId)

        If Not IsDBNull(dr("NotificationExceptionId")) Then
            _NotificationExceptionId = dr("NotificationExceptionId")
        End If
        If Not IsDBNull(dr("FK_EmployeeId")) Then
            _FK_EmployeeId = dr("FK_EmployeeId")
        End If
        If Not IsDBNull(dr("FromDate")) Then
            _FromDate = dr("FromDate")
        End If
        If Not IsDBNull(dr("ToDate")) Then
            _ToDate = dr("ToDate")
        End If
        If Not IsDBNull(dr("Active")) Then
            _Active = dr("Active")
        End If
        If Not IsDBNull(dr("Reason")) Then
            _Reason = dr("Reason")
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
        If Not IsDBNull(dr("FK_LogicalGroupId")) Then
            _FK_LogicalGroupId = dr("FK_LogicalGroupId")
        End If
        If Not IsDBNull(dr("FK_WorkLocationId")) Then
            _FK_WorkLocationId = dr("FK_WorkLocationId")
        End If
        If Not IsDBNull(dr("FK_EntityId")) Then
            _FK_EntityId = dr("FK_EntityId")
        End If
        Return Me
    End Function

    Public Function GetAllInnerEmployee() As DataTable

        Return objDALNotification_Exception.GetAllInnerEmployee()

    End Function

    Public Function GetInnerEmployee() As DataTable

        Return objDALNotification_Exception.GetInnerEmployee()

    End Function

#End Region

End Class
