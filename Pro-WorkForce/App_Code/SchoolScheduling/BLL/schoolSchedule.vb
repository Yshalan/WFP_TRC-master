Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA_SchoolScheduling

    Public Class schoolSchedule

#Region "Class Variables"


        Private _DayId As Integer
        Private _lesson As Integer
        Private _FK_ClassId As Integer
        Private _FK_CourseId As Integer
        Private _FK_TeacherId As Integer
        Private objDALschoolSchedule As DALschoolSchedule

#End Region

#Region "Public Properties"


        Public Property DayId() As Integer
            Set(ByVal value As Integer)
                _DayId = value
            End Set
            Get
                Return (_DayId)
            End Get
        End Property


        Public Property lesson() As Integer
            Set(ByVal value As Integer)
                _lesson = value
            End Set
            Get
                Return (_lesson)
            End Get
        End Property


        Public Property FK_ClassId() As Integer
            Set(ByVal value As Integer)
                _FK_ClassId = value
            End Set
            Get
                Return (_FK_ClassId)
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


        Public Property FK_TeacherId() As Integer
            Set(ByVal value As Integer)
                _FK_TeacherId = value
            End Set
            Get
                Return (_FK_TeacherId)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALschoolSchedule = New DALschoolSchedule()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALschoolSchedule.Add(_DayId, _lesson, _FK_ClassId, _FK_CourseId, _FK_TeacherId)
        End Function

        Public Function Update() As Integer

            Return objDALschoolSchedule.Update(_DayId, _lesson, _FK_ClassId, _FK_CourseId, _FK_TeacherId)

        End Function



        Public Function Delete() As Integer

            Return objDALschoolSchedule.Delete(_DayId, _lesson, _FK_ClassId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALschoolSchedule.GetAll()

        End Function
        Public Function CheckIfEmpty(ByVal AllowSequential As Boolean, ByVal DistributedBreak As Boolean, ByVal Maxlesson As Integer) As Integer

            Dim dt As DataTable
            dt = objDALschoolSchedule.CheckIfEmpty(_DayId, _lesson, _FK_ClassId, _FK_CourseId, _FK_TeacherId, AllowSequential, DistributedBreak, Maxlesson)

            Return dt.Rows(0)("ExistCount")

        End Function
        Public Function GetAll_ForGrid() As DataTable

            Return objDALschoolSchedule.GetAll_ForGrid()

        End Function

     

        Public Function Fill_ByTeacherId() As DataTable

            Return objDALschoolSchedule.Fill_ByTeacherId(_FK_TeacherId)

        End Function
        Public Function GetAll_WeekDays() As DataTable

            Return objDALschoolSchedule.GetAll_WeekDays()

        End Function

        Public Function Fill_ByClassId() As DataTable

            Return objDALschoolSchedule.Fill_ByClassId(_FK_ClassId)

        End Function

        Public Function GetByPK() As schoolSchedule

            Dim dr As DataRow
            dr = objDALschoolSchedule.GetByPK(_DayId, _lesson, _FK_ClassId)

            If Not IsDBNull(dr("DayId")) Then
                _DayId = dr("DayId")
            End If
            If Not IsDBNull(dr("lesson")) Then
                _lesson = dr("lesson")
            End If
            If Not IsDBNull(dr("FK_ClassId")) Then
                _FK_ClassId = dr("FK_ClassId")
            End If
            If Not IsDBNull(dr("FK_CourseId")) Then
                _FK_CourseId = dr("FK_CourseId")
            End If
            If Not IsDBNull(dr("FK_TeacherId")) Then
                _FK_TeacherId = dr("FK_TeacherId")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace