Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA_SchoolScheduling

    Public Class CourseTeachers

#Region "Class Variables"


        Private _FK_CourseId As Integer
        Private _FK_EmployeeId As Long
        Private _LAST_UPDATE_DATE As DateTime
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private objDALCourseTeachers As DALCourseTeachers

#End Region

#Region "Public Properties"


        Public Property FK_CourseId() As Integer
            Set(ByVal value As Integer)
                _FK_CourseId = value
            End Set
            Get
                Return (_FK_CourseId)
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

#End Region


#Region "Constructor"

        Public Sub New()

            objDALCourseTeachers = New DALCourseTeachers()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALCourseTeachers.Add(_FK_CourseId, _FK_EmployeeId, _CREATED_BY, _CREATED_DATE)
        End Function

        Public Function Update() As Integer

            Return objDALCourseTeachers.Update(_FK_CourseId, _FK_EmployeeId, _LAST_UPDATE_DATE, _LAST_UPDATE_BY)

        End Function



        Public Function Delete() As Integer

            Return objDALCourseTeachers.Delete(_FK_CourseId, _FK_EmployeeId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALCourseTeachers.GetAll()

        End Function

        Public Function GetCourseTeachers_Select_AllbyEmpId() As DataTable
            Return objDALCourseTeachers.GetCourseTeachers_Select_AllbyEmpId(_FK_EmployeeId)
        End Function

        Public Function GetAll_ByCourseId() As DataTable

            Return objDALCourseTeachers.GetAll_ByCourseId(_FK_CourseId)

        End Function

        Public Function GetAll_ByEmployeeId() As DataTable

            Return objDALCourseTeachers.GetAll_ByEmployeeId(_FK_EmployeeId)

        End Function
        Public Function GetAllWithTeacher() As DataTable

            Return objDALCourseTeachers.GetAllWithTeacher()

        End Function

        Public Function GetByPK() As CourseTeachers

            Dim dr As DataRow
            dr = objDALCourseTeachers.GetByPK(_FK_CourseId, _FK_EmployeeId)

            If Not IsDBNull(dr("FK_CourseId")) Then
                _FK_CourseId = dr("FK_CourseId")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
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