Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Forms

    Public Class Events

#Region "Class Variables"


        Private _EventId As Integer
        Private _EventName As String
        Private _EventDescription As String
        Private _StartDate As DateTime
        Private _EndDate As DateTime
        Private _ResposiblePerson As Long
        Private objDALEvents As DALEvents

#End Region

#Region "Public Properties"


        Public Property EventId() As Integer
            Set(ByVal value As Integer)
                _EventId = value
            End Set
            Get
                Return (_EventId)
            End Get
        End Property


        Public Property EventName() As String
            Set(ByVal value As String)
                _EventName = value
            End Set
            Get
                Return (_EventName)
            End Get
        End Property


        Public Property EventDescription() As String
            Set(ByVal value As String)
                _EventDescription = value
            End Set
            Get
                Return (_EventDescription)
            End Get
        End Property


        Public Property StartDate() As DateTime
            Set(ByVal value As DateTime)
                _StartDate = value
            End Set
            Get
                Return (_StartDate)
            End Get
        End Property


        Public Property EndDate() As DateTime
            Set(ByVal value As DateTime)
                _EndDate = value
            End Set
            Get
                Return (_EndDate)
            End Get
        End Property


        Public Property ResposiblePerson() As Long
            Set(ByVal value As Long)
                _ResposiblePerson = value
            End Set
            Get
                Return (_ResposiblePerson)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALEvents = New DALEvents()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALEvents.Add(_EventId, _EventName, _EventDescription, _StartDate, _EndDate, _ResposiblePerson)
            App_EventsLog.Insert_ToEventLog("Add", _EventId, "Events", "Events")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALEvents.Update(_EventId, _EventName, _EventDescription, _StartDate, _EndDate, _ResposiblePerson)
            App_EventsLog.Insert_ToEventLog("Update", _EventId, "Events", "Events")
            Return rslt
        End Function



        Public Function Delete() As Integer

            Dim rslt As Integer = objDALEvents.Delete(_EventId)
            App_EventsLog.Insert_ToEventLog("Delete", _EventId, "Events", "Events")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALEvents.GetAll()

        End Function
        Public Function GetAll_Details() As DataTable

            Return objDALEvents.GetAll_Details()

        End Function
        Public Function GetByPK() As Events

            Dim dr As DataRow
            dr = objDALEvents.GetByPK(_EventId)
            If Not dr Is Nothing Then
                If Not IsDBNull(dr("EventId")) Then
                    _EventId = dr("EventId")
                End If
                If Not IsDBNull(dr("EventName")) Then
                    _EventName = dr("EventName")
                End If
                If Not IsDBNull(dr("EventDescription")) Then
                    _EventDescription = dr("EventDescription")
                End If
                If Not IsDBNull(dr("StartDate")) Then
                    _StartDate = dr("StartDate")
                End If
                If Not IsDBNull(dr("EndDate")) Then
                    _EndDate = dr("EndDate")
                End If
                If Not IsDBNull(dr("ResposiblePerson")) Then
                    _ResposiblePerson = dr("ResposiblePerson")
                End If
                Return Me
            Else
                Return Nothing
            End If
        End Function

#End Region

    End Class
End Namespace