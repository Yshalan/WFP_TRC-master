Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Definitions

    Public Class Authorities

#Region "Class Variables"


        Private _AuthorityId As Integer
        Private _AuthorityCode As String
        Private _AuthorityName As String
        Private _AuthorityArabicName As String
        Private _Active As Boolean
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALAuthorities As DALAuthorities

#End Region

#Region "Public Properties"


        Public Property AuthorityId() As Integer
            Set(ByVal value As Integer)
                _AuthorityId = value
            End Set
            Get
                Return (_AuthorityId)
            End Get
        End Property


        Public Property AuthorityCode() As String
            Set(ByVal value As String)
                _AuthorityCode = value
            End Set
            Get
                Return (_AuthorityCode)
            End Get
        End Property


        Public Property AuthorityName() As String
            Set(ByVal value As String)
                _AuthorityName = value
            End Set
            Get
                Return (_AuthorityName)
            End Get
        End Property


        Public Property AuthorityArabicName() As String
            Set(ByVal value As String)
                _AuthorityArabicName = value
            End Set
            Get
                Return (_AuthorityArabicName)
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

            objDALAuthorities = New DALAuthorities()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALAuthorities.Add(_AuthorityId, _AuthorityCode, _AuthorityName, _AuthorityArabicName, _Active, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE)
        End Function

        Public Function Update() As Integer

            Return objDALAuthorities.Update(_AuthorityId, _AuthorityCode, _AuthorityName, _AuthorityArabicName, _Active, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE)

        End Function



        Public Function Delete() As Integer

            Return objDALAuthorities.Delete(_AuthorityId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALAuthorities.GetAll()

        End Function

        Public Function GetByPK() As Authorities

            Dim dr As DataRow
            dr = objDALAuthorities.GetByPK(_AuthorityId)

            If Not IsDBNull(dr("AuthorityId")) Then
                _AuthorityId = dr("AuthorityId")
            End If
            If Not IsDBNull(dr("AuthorityCode")) Then
                _AuthorityCode = dr("AuthorityCode")
            End If
            If Not IsDBNull(dr("AuthorityName")) Then
                _AuthorityName = dr("AuthorityName")
            End If
            If Not IsDBNull(dr("AuthorityArabicName")) Then
                _AuthorityArabicName = dr("AuthorityArabicName")
            End If
            If Not IsDBNull(dr("Active")) Then
                _Active = dr("Active")
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