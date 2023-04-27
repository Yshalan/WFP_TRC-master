Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA_SchoolScheduling

    Public Class Classes

#Region "Class Variables"


        Private _ClassId As Integer
        Private _FK_ClassGradeId As Integer
        Private _ClassName As String
        Private _ClassNameAr As String
        Private _LAST_UPDATE_DATE As DateTime
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _lang As String
        Private objDALClasses As DALClasses

#End Region

#Region "Public Properties"


        Public Property ClassId() As Integer
            Set(ByVal value As Integer)
                _ClassId = value
            End Set
            Get
                Return (_ClassId)
            End Get
        End Property


        Public Property FK_ClassGradeId() As Integer
            Set(ByVal value As Integer)
                _FK_ClassGradeId = value
            End Set
            Get
                Return (_FK_ClassGradeId)
            End Get
        End Property


        Public Property ClassName() As String
            Set(ByVal value As String)
                _ClassName = value
            End Set
            Get
                Return (_ClassName)
            End Get
        End Property


        Public Property ClassNameAr() As String
            Set(ByVal value As String)
                _ClassNameAr = value
            End Set
            Get
                Return (_ClassNameAr)
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

        Public Property lang() As String
            Set(ByVal value As String)
                _lang = value
            End Set
            Get
                Return (_lang)
            End Get
        End Property
#End Region


#Region "Constructor"

        Public Sub New()

            objDALClasses = New DALClasses()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALClasses.Add(_FK_ClassGradeId, _ClassName, _ClassNameAr, _LAST_UPDATE_DATE, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY)
        End Function

        Public Function Update() As Integer

            Return objDALClasses.Update(_ClassId, _FK_ClassGradeId, _ClassName, _ClassNameAr, _LAST_UPDATE_DATE, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY)

        End Function



        Public Function Delete() As Integer

            Return objDALClasses.Delete(_ClassId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALClasses.GetAll()

        End Function

        Public Function GetAll_ByClassGrade() As DataTable

            Return objDALClasses.GetAll_ByClassGrade(_lang)

        End Function
        Public Function GetAll_ByClassGradeId() As DataTable

            Return objDALClasses.GetAll_ByClassGradeId(FK_ClassGradeId)

        End Function

        Public Function GetByPK() As Classes

            Dim dr As DataRow
            dr = objDALClasses.GetByPK(_ClassId)

            If Not IsDBNull(dr("ClassId")) Then
                _ClassId = dr("ClassId")
            End If
            If Not IsDBNull(dr("FK_ClassGradeId")) Then
                _FK_ClassGradeId = dr("FK_ClassGradeId")
            End If
            If Not IsDBNull(dr("ClassName")) Then
                _ClassName = dr("ClassName")
            End If
            If Not IsDBNull(dr("ClassNameAr")) Then
                _ClassNameAr = dr("ClassNameAr")
            End If
            If Not IsDBNull(dr("LAST_UPDATE_DATE")) Then
                _LAST_UPDATE_DATE = dr("LAST_UPDATE_DATE")
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
            Return Me
        End Function

#End Region

    End Class
End Namespace