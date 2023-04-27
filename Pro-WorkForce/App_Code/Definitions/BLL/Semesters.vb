Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Definitions

    Public Class Semesters

#Region "Class Variables"

        Private _SemesterId As Integer
        Private _SemesterName As String
        Private _SemesterArabicName As String
        Private _CREATED_BY As String
        Private _CREATED_DATE As String
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALSemesters As DALSemesters

#End Region

#Region "Public Properties"

        Public Property SemesterId() As Integer
            Set(ByVal value As Integer)
                _SemesterId = value
            End Set
            Get
                Return (_SemesterId)
            End Get
        End Property

        Public Property SemesterName() As String
            Set(ByVal value As String)
                _SemesterName = value
            End Set
            Get
                Return (_SemesterName)
            End Get
        End Property

        Public Property SemesterArabicName() As String
            Set(ByVal value As String)
                _SemesterArabicName = value
            End Set
            Get
                Return (_SemesterArabicName)
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

        Public Property CREATED_DATE() As String
            Set(ByVal value As String)
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

            objDALSemesters = New DALSemesters()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALSemesters.Add(_SemesterName, _SemesterArabicName, _CREATED_BY)
        End Function

        Public Function Update() As Integer

            Return objDALSemesters.Update(_SemesterId, _SemesterName, _SemesterArabicName, _LAST_UPDATE_BY)

        End Function

        Public Function Delete() As Integer

            Return objDALSemesters.Delete(_SemesterId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALSemesters.GetAll()

        End Function

        Public Function GetByPK() As Semesters

            Dim dr As DataRow
            dr = objDALSemesters.GetByPK(_SemesterId)

            If Not IsDBNull(dr("SemesterId")) Then
                _SemesterId = dr("SemesterId")
            End If
            If Not IsDBNull(dr("SemesterName")) Then
                _SemesterName = dr("SemesterName")
            End If
            If Not IsDBNull(dr("SemesterArabicName")) Then
                _SemesterArabicName = dr("SemesterArabicName")
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