Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports SmartV.UTILITIES

Namespace TA.Events

    Public Class App_EventsLog

#Region "Class Variables"


        Private _EventId As Long
        Private _EventDateTime As DateTime
        Private _FK_UserId As Integer
        Private _ControlName As String
        Private _ActoinType As String
        Private _RecordId As String
        Private _RecordName As String
        Private _RecordDescription As String
        Private objDALApp_EventsLog As DALApp_EventsLog

#End Region

#Region "Public Properties"


        Public Property EventId() As Long
            Set(ByVal value As Long)
                _EventId = value
            End Set
            Get
                Return (_EventId)
            End Get
        End Property


        Public Property EventDateTime() As DateTime
            Set(ByVal value As DateTime)
                _EventDateTime = value
            End Set
            Get
                Return (_EventDateTime)
            End Get
        End Property


        Public Property FK_UserId() As Integer
            Set(ByVal value As Integer)
                _FK_UserId = value
            End Set
            Get
                Return (_FK_UserId)
            End Get
        End Property


        Public Property ControlName() As String
            Set(ByVal value As String)
                _ControlName = value
            End Set
            Get
                Return (_ControlName)
            End Get
        End Property


        Public Property ActoinType() As String
            Set(ByVal value As String)
                _ActoinType = value
            End Set
            Get
                Return (_ActoinType)
            End Get
        End Property


        Public Property RecordId() As String
            Set(ByVal value As String)
                _RecordId = value
            End Set
            Get
                Return (_RecordId)
            End Get
        End Property


        Public Property RecordName() As String
            Set(ByVal value As String)
                _RecordName = value
            End Set
            Get
                Return (_RecordName)
            End Get
        End Property


        Public Property RecordDescription() As String
            Set(ByVal value As String)
                _RecordDescription = value
            End Set
            Get
                Return (_RecordDescription)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALApp_EventsLog = New DALApp_EventsLog()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALApp_EventsLog.Add(_FK_UserId, _ControlName, _ActoinType, _RecordId, _RecordName, _RecordDescription)
        End Function
        Public Shared Sub Insert_ToEventLog(ByVal ActionType As String, ByVal RecordId As String, ByVal RecordName As String, ByVal ControlName As String)
            Dim objDALEventsLog As New DALApp_EventsLog
            objDALEventsLog.Add((SessionVariables.LoginUser.ID), ControlName, ActionType, RecordId, RecordName, ("Record " + RecordId + " " + ActionType))
        End Sub


        Public Shared Sub Insert_ToEventLog(ByVal ActionType As String, ByVal RecordId As String, ByVal RecordName As String, ByVal ControlName As String, ByVal MoreDetails As String)
            Dim objDALEventsLog As New DALApp_EventsLog
            objDALEventsLog.Add((SessionVariables.LoginUser.ID), ControlName, ActionType, RecordId, RecordName, ("Record " + RecordId + " " + ActionType + " -" + MoreDetails))
        End Sub

        Public Function Update() As Integer

            Return objDALApp_EventsLog.Update(_EventId, _EventDateTime, _FK_UserId, _ControlName, _ActoinType, _RecordId, _RecordName, _RecordDescription)

        End Function



        Public Function Delete() As Integer

            Return objDALApp_EventsLog.Delete(_EventId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALApp_EventsLog.GetAll()

        End Function

        Public Function GetByPK() As App_EventsLog

            Dim dr As DataRow
            dr = objDALApp_EventsLog.GetByPK(_EventId)

            If Not IsDBNull(dr("EventId")) Then
                _EventId = dr("EventId")
            End If
            If Not IsDBNull(dr("EventDateTime")) Then
                _EventDateTime = dr("EventDateTime")
            End If
            If Not IsDBNull(dr("FK_UserId")) Then
                _FK_UserId = dr("FK_UserId")
            End If
            If Not IsDBNull(dr("ControlName")) Then
                _ControlName = dr("ControlName")
            End If
            If Not IsDBNull(dr("ActoinType")) Then
                _ActoinType = dr("ActoinType")
            End If
            If Not IsDBNull(dr("RecordId")) Then
                _RecordId = dr("RecordId")
            End If
            If Not IsDBNull(dr("RecordName")) Then
                _RecordName = dr("RecordName")
            End If
            If Not IsDBNull(dr("RecordDescription")) Then
                _RecordDescription = dr("RecordDescription")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace