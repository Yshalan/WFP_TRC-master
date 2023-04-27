Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA_SchoolScheduling

    Public Class ClassGradeCourses

#Region "Class Variables"


        Private _FK_ClassGradeId As Integer
        Private _FK_CourseId As Integer
        Private _WeeklyCourcesNumber As Integer
        Private objDALClassGradeCourses As DALClassGradeCourses

#End Region

#Region "Public Properties"


        Public Property FK_ClassGradeId() As Integer
            Set(ByVal value As Integer)
                _FK_ClassGradeId = value
            End Set
            Get
                Return (_FK_ClassGradeId)
            End Get
        End Property


        Public Property FK_CourseId() As Integer
            Set(ByVal value As Integer)
                _FK_CourseId = value
            End Set
            Get
                Return (_FK_CourseId)
            End Get
        End Property


        Public Property WeeklyCourcesNumber() As Integer
            Set(ByVal value As Integer)
                _WeeklyCourcesNumber = value
            End Set
            Get
                Return (_WeeklyCourcesNumber)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALClassGradeCourses = New DALClassGradeCourses()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALClassGradeCourses.Add(_FK_ClassGradeId, _FK_CourseId, _WeeklyCourcesNumber)
        End Function

        Public Function Update() As Integer

            Return objDALClassGradeCourses.Update(_FK_ClassGradeId, _FK_CourseId, _WeeklyCourcesNumber)

        End Function


        Public Function Delete() As Integer

            Return objDALClassGradeCourses.Delete(_FK_ClassGradeId, _FK_CourseId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALClassGradeCourses.GetAll()

        End Function

        Public Function GetWeeklyNo() As DataTable

            Return objDALClassGradeCourses.GetWeeklyNo(_FK_CourseId, _FK_ClassGradeId)

        End Function
        Public Function GetAllCourse_ByClassGradeId() As DataTable

            Return objDALClassGradeCourses.GetAllCourse_ByClassGradeId(_FK_ClassGradeId)

        End Function
        Public Function GetByPK() As ClassGradeCourses

            Dim dr As DataRow
            dr = objDALClassGradeCourses.GetByPK(_FK_ClassGradeId, _FK_CourseId)

            If Not IsDBNull(dr("FK_ClassGradeId")) Then
                _FK_ClassGradeId = dr("FK_ClassGradeId")
            End If
            If Not IsDBNull(dr("FK_CourseId")) Then
                _FK_CourseId = dr("FK_CourseId")
            End If
            If Not IsDBNull(dr("WeeklyCourcesNumber")) Then
                _WeeklyCourcesNumber = dr("WeeklyCourcesNumber")
            End If
            Return Me
        End Function

        Public Function GetAllbyFK_ClassGradeIdFK_CourseId() As DataTable

            Return objDALClassGradeCourses.GetAllbyFK_ClassGradeIdFK_CourseId(_FK_ClassGradeId, _FK_CourseId)

        End Function

        Public Function GetAllbyFK_ClassGradeId() As DataTable

            Return objDALClassGradeCourses.GetAllbyFK_ClassGradeId(_FK_ClassGradeId)

        End Function
#End Region

    End Class
End Namespace