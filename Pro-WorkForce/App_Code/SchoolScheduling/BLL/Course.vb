Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

namespace TA_SchoolScheduling

Public Class Course

#Region "Class Variables"


        Private _CourseId As Integer
        Private _CourseCode As String
        Private _CourseName As String
        Private _CourseNameAr As String
        Private _Color As String
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private _FK_ClassGradeId As Integer
        Private objDALCourse As DALCourse

#End Region

#Region "Public Properties"


        Public Property CourseId() As Integer
            Set(ByVal value As Integer)
                _CourseId = value
            End Set
            Get
                Return (_CourseId)
            End Get
        End Property


        Public Property CourseCode() As String
            Set(ByVal value As String)
                _CourseCode = value
            End Set
            Get
                Return (_CourseCode)
            End Get
        End Property


        Public Property CourseName() As String
            Set(ByVal value As String)
                _CourseName = value
            End Set
            Get
                Return (_CourseName)
            End Get
        End Property


        Public Property CourseNameAr() As String
            Set(ByVal value As String)
                _CourseNameAr = value
            End Set
            Get
                Return (_CourseNameAr)
            End Get
        End Property


        Public Property Color() As String
            Set(ByVal value As String)
                _Color = value
            End Set
            Get
                Return (_Color)
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

        Public Property FK_ClassGradeId() As Integer
            Set(ByVal value As Integer)
                _FK_ClassGradeId = value
            End Set
            Get
                Return (_FK_ClassGradeId)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALCourse = New DALCourse()

        End Sub

#End Region

#region "Methods"

        Public Function Add() As Integer

            Return objDALCourse.Add(_CourseCode, _CourseName, _CourseNameAr, _Color, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE)
        End Function

        Public Function Update() As Integer

            Return objDALCourse.Update(_CourseId, _CourseCode, _CourseName, _CourseNameAr, _Color, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE)

        End Function



        Public Function Delete() As Integer

            Return objDALCourse.Delete(_CourseId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALCourse.GetAll()

        End Function
        Public Function GetAllByCourseId() As DataTable

            Return objDALCourse.GetAllByCourseId(_FK_ClassGradeId)

        End Function
       
        Public Function GetByPK() As Course

            Dim dr As DataRow
            dr = objDALCourse.GetByPK(_CourseId)

            If Not IsDBNull(dr("CourseId")) Then
                _CourseId = dr("CourseId")
            End If
            If Not IsDBNull(dr("CourseCode")) Then
                _CourseCode = dr("CourseCode")
            End If
            If Not IsDBNull(dr("CourseName")) Then
                _CourseName = dr("CourseName")
            End If
            If Not IsDBNull(dr("CourseNameAr")) Then
                _CourseNameAr = dr("CourseNameAr")
            End If
            If Not IsDBNull(dr("Color")) Then
                _Color = dr("Color")
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
End namespace