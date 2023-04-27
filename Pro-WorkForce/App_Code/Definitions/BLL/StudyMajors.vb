Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Definitions

    Public Class StudyMajors

#Region "Class Variables"


        Private _MajorId As Integer
        Private _MajorName As String
        Private _MajorArabicName As String
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALStudyMajors As DALStudyMajors

#End Region

#Region "Public Properties"

        Public Property MajorId() As Integer
            Set(ByVal value As Integer)
                _MajorId = value
            End Set
            Get
                Return (_MajorId)
            End Get
        End Property

        Public Property MajorName() As String
            Set(ByVal value As String)
                _MajorName = value
            End Set
            Get
                Return (_MajorName)
            End Get
        End Property

        Public Property MajorArabicName() As String
            Set(ByVal value As String)
                _MajorArabicName = value
            End Set
            Get
                Return (_MajorArabicName)
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

            objDALStudyMajors = New DALStudyMajors()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALStudyMajors.Add(_MajorId, _MajorName, _MajorArabicName, _CREATED_BY)
            App_EventsLog.Insert_ToEventLog("Add", _MajorId, "StudyMajors", "Define Majors & Specialization(s)")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALStudyMajors.Update(_MajorId, _MajorName, _MajorArabicName, _LAST_UPDATE_BY)
            App_EventsLog.Insert_ToEventLog("Update", _MajorId, "StudyMajors", "Define Majors & Specialization(s)")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALStudyMajors.Delete(_MajorId)
            App_EventsLog.Insert_ToEventLog("Delete", _MajorId, "StudyMajors", "Define Majors & Specialization(s)")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALStudyMajors.GetAll()

        End Function

        Public Function GetByPK() As StudyMajors

            Dim dr As DataRow
            dr = objDALStudyMajors.GetByPK(_MajorId)

            If Not IsDBNull(dr("MajorId")) Then
                _MajorId = dr("MajorId")
            End If
            If Not IsDBNull(dr("MajorName")) Then
                _MajorName = dr("MajorName")
            End If
            If Not IsDBNull(dr("MajorArabicName")) Then
                _MajorArabicName = dr("MajorArabicName")
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