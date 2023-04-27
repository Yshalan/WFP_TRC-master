Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Definitions

    Public Class StudySpecialization

#Region "Class Variables"


        Private _SpecializationId As Integer
        Private _FK_MajorId As Integer
        Private _SpecializationName As String
        Private _SpecializationArabicName As String
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALStudySpecialization As DALStudySpecialization

#End Region

#Region "Public Properties"

        Public Property SpecializationId() As Integer
            Set(ByVal value As Integer)
                _SpecializationId = value
            End Set
            Get
                Return (_SpecializationId)
            End Get
        End Property

        Public Property FK_MajorId() As Integer
            Set(ByVal value As Integer)
                _FK_MajorId = value
            End Set
            Get
                Return (_FK_MajorId)
            End Get
        End Property

        Public Property SpecializationName() As String
            Set(ByVal value As String)
                _SpecializationName = value
            End Set
            Get
                Return (_SpecializationName)
            End Get
        End Property

        Public Property SpecializationArabicName() As String
            Set(ByVal value As String)
                _SpecializationArabicName = value
            End Set
            Get
                Return (_SpecializationArabicName)
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

            objDALStudySpecialization = New DALStudySpecialization()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALStudySpecialization.Add(_SpecializationId, _FK_MajorId, _SpecializationName, _SpecializationArabicName, _CREATED_BY)
            App_EventsLog.Insert_ToEventLog("Add", _SpecializationId, "StudySpecialization", "Define Majors & Specialization(s)")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALStudySpecialization.Update(_SpecializationId, _FK_MajorId, _SpecializationName, _SpecializationArabicName, _LAST_UPDATE_BY)
            App_EventsLog.Insert_ToEventLog("Update", _SpecializationId, "StudySpecialization", "Define Majors & Specialization(s)")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALStudySpecialization.Delete(_SpecializationId)
            App_EventsLog.Insert_ToEventLog("Delete", _SpecializationId, "StudySpecialization", "Define Majors & Specialization(s)")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALStudySpecialization.GetAll()

        End Function

        Public Function GetAll_Inner() As DataTable

            Return objDALStudySpecialization.GetAll_Inner(_FK_MajorId)

        End Function

        Public Function GetByPK() As StudySpecialization

            Dim dr As DataRow
            dr = objDALStudySpecialization.GetByPK(_SpecializationId)

            If Not IsDBNull(dr("SpecializationId")) Then
                _SpecializationId = dr("SpecializationId")
            End If
            If Not IsDBNull(dr("FK_MajorId")) Then
                _FK_MajorId = dr("FK_MajorId")
            End If
            If Not IsDBNull(dr("SpecializationName")) Then
                _SpecializationName = dr("SpecializationName")
            End If
            If Not IsDBNull(dr("SpecializationArabicName")) Then
                _SpecializationArabicName = dr("SpecializationArabicName")
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