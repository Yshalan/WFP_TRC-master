Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA_SchoolScheduling

    Public Class TeacherClasses

#Region "Class Variables"


        Private _FK_ClassId As Integer
        Private _FK_EmployeeId As Long
        Private _FK_CourseId As Integer
        Private _weeklyCount As Integer
        Private _LAST_UPDATE_DATE As DateTime
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private objDALTeacherClasses As DALTeacherClasses

#End Region

#Region "Public Properties"


        Public Property FK_ClassId() As Integer
            Set(ByVal value As Integer)
                _FK_ClassId = value
            End Set
            Get
                Return (_FK_ClassId)
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


        Public Property FK_CourseId() As Integer
            Set(ByVal value As Integer)
                _FK_CourseId = value
            End Set
            Get
                Return (_FK_CourseId)
            End Get
        End Property


        Public Property weeklyCount() As Integer
            Set(ByVal value As Integer)
                _weeklyCount = value
            End Set
            Get
                Return (_weeklyCount)
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

            objDALTeacherClasses = New DALTeacherClasses()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALTeacherClasses.Add(_FK_ClassId, _FK_EmployeeId, _FK_CourseId, _weeklyCount, _CREATED_BY, _CREATED_DATE)
        End Function

        Public Function Update() As Integer

            Return objDALTeacherClasses.Update(_FK_ClassId, _FK_EmployeeId, _FK_CourseId, _weeklyCount, _LAST_UPDATE_DATE, _LAST_UPDATE_BY)

        End Function



        Public Function Delete() As Integer

            Return objDALTeacherClasses.Delete(_FK_ClassId, _FK_EmployeeId, _FK_CourseId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALTeacherClasses.GetAll()

        End Function
        Public Function GetAllByClassId() As DataTable

            Return objDALTeacherClasses.GetAllByClassId(_FK_ClassId)

        End Function
        Public Function GetClassWeeklyCount() As DataTable

            Return objDALTeacherClasses.GetClassWeeklyCount(_FK_ClassId)

        End Function


        Public Function GetAllByEmployee() As DataTable

            Return objDALTeacherClasses.GetAllByEmployee(_FK_EmployeeId)

        End Function

        Public Function GetByPK() As TeacherClasses

            Dim dr As DataRow
            dr = objDALTeacherClasses.GetByPK(_FK_ClassId, _FK_EmployeeId, _FK_CourseId)

            If Not IsDBNull(dr("FK_ClassId")) Then
                _FK_ClassId = dr("FK_ClassId")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("FK_CourseId")) Then
                _FK_CourseId = dr("FK_CourseId")
            End If
            If Not IsDBNull(dr("weeklyCount")) Then
                _weeklyCount = dr("weeklyCount")
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