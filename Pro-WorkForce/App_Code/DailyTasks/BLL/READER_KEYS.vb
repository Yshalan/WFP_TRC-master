Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.DailyTasks

    Public Class READER_KEYS

#Region "Class Variables"


        Private _READER_KEY As String
        Private _CHANGE_TO As String
        Private _Type As String
        Private objDALREADER_KEYS As DALREADER_KEYS

#End Region

#Region "Public Properties"


        Public Property READER_KEY() As String
            Set(ByVal value As String)
                _READER_KEY = value
            End Set
            Get
                Return (_READER_KEY)
            End Get
        End Property


        Public Property CHANGE_TO() As String
            Set(ByVal value As String)
                _CHANGE_TO = value
            End Set
            Get
                Return (_CHANGE_TO)
            End Get
        End Property


        Public Property Type() As String
            Set(ByVal value As String)
                _Type = value
            End Set
            Get
                Return (_Type)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALREADER_KEYS = New DALREADER_KEYS()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALREADER_KEYS.Add(_READER_KEY, _CHANGE_TO, _Type)
            App_EventsLog.Insert_ToEventLog("Add", _READER_KEY, "READER_KEYS", "READER_KEYS")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALREADER_KEYS.Update(_READER_KEY, _CHANGE_TO, _Type)
            App_EventsLog.Insert_ToEventLog("Edit", _READER_KEY, "READER_KEYS", "READER_KEYS")
            Return rslt
        End Function



        Public Function Delete() As Integer

            Dim rslt As Integer = objDALREADER_KEYS.Delete(_READER_KEY)
            App_EventsLog.Insert_ToEventLog("Delete", _READER_KEY, "READER_KEYS", "READER_KEYS")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALREADER_KEYS.GetAll()

        End Function

        Public Function GetByPK() As READER_KEYS

            Dim dr As DataRow
            dr = objDALREADER_KEYS.GetByPK(_READER_KEY)

            If Not IsDBNull(dr("READER_KEY")) Then
                _READER_KEY = dr("READER_KEY")
            End If
            If Not IsDBNull(dr("CHANGE_TO")) Then
                _CHANGE_TO = dr("CHANGE_TO")
            End If
            If Not IsDBNull(dr("Type")) Then
                _Type = dr("Type")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace