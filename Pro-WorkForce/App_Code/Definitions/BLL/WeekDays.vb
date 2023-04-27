Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Definitions

    Public Class WeekDays

#Region "Class Variables"

        Private _DayId As Integer
        Private _DayName As String
        Private _DayArabicName As String
        Private _DayOrder As Integer
        Private objDALWeekDays As DALWeekDays

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


        Public Property DayName() As String
            Set(ByVal value As String)
                _DayName = value
            End Set
            Get
                Return (_DayName)
            End Get
        End Property


        Public Property DayArabicName() As String
            Set(ByVal value As String)
                _DayArabicName = value
            End Set
            Get
                Return (_DayArabicName)
            End Get
        End Property

        Public Property DayOrder() As Integer
            Get
                Return _DayOrder
            End Get
            Set(ByVal value As Integer)
                _DayOrder = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Public Sub New()

            objDALWeekDays = New DALWeekDays()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALWeekDays.Add(_DayId, _DayName, _DayArabicName)
        End Function

        Public Function Update() As Integer

            Return objDALWeekDays.Update(_DayId, _DayName, _DayArabicName, _DayOrder)

        End Function

        Public Function Delete() As Integer

            Return objDALWeekDays.Delete(_DayId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALWeekDays.GetAll()

        End Function

        Public Function Flexible_GetAll() As DataTable

            Return objDALWeekDays.Flexible_GetAll


        End Function

        Public Function Normal_GetAll() As DataTable

            Return objDALWeekDays.Normal_GetAll


        End Function

        Public Function GetForDDL()
            Return objDALWeekDays.GetForDDL()
        End Function

        Public Function GetByPK() As WeekDays

            Dim dr As DataRow
            dr = objDALWeekDays.GetByPK(_DayId)

            If Not IsDBNull(dr("DayId")) Then
                _DayId = dr("DayId")
            End If
            If Not IsDBNull(dr("DayName")) Then
                _DayName = dr("DayName")
            End If
            If Not IsDBNull(dr("DayArabicName")) Then
                _DayArabicName = dr("DayArabicName")
            End If
            If Not IsDBNull(dr("DayOrder")) Then
                _DayOrder = dr("DayOrder")
            End If
            Return Me
        End Function

        Public Function UpdateDayOrder() As Integer

            Return objDALWeekDays.UpdateDayOrder(_DayId, _DayOrder)

        End Function

        Public Function GetByDayrder() As WeekDays

            Dim dr As DataRow
            dr = objDALWeekDays.GetByDayOrder(_DayOrder)

            If Not IsDBNull(dr("DayId")) Then
                _DayId = dr("DayId")
            End If
            If Not IsDBNull(dr("DayName")) Then
                _DayName = dr("DayName")
            End If
            If Not IsDBNull(dr("DayArabicName")) Then
                _DayArabicName = dr("DayArabicName")
            End If
            If Not IsDBNull(dr("DayOrder")) Then
                _DayOrder = dr("DayOrder")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace