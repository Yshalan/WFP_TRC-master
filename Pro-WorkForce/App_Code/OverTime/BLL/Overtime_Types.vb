Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.OverTime

    Public Class Overtime_Types

#Region "Class Variables"


        Private _OvertimeTypeId As Integer
        Private _OvertimeTypeName As String
        Private _OvertimeTypeArabicName As String
        Private _OvertimeRate As Double
        Private _CompensateToLeave As Boolean
        Private _FK_LeaveTypeId As Integer
        Private _MustRequested As Boolean
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private _OvertimeCalculationConsideration As Integer
        Private _OvertimeChangeValue As Integer
        Private objDALOvertime_Types As DALOvertime_Types

#End Region

#Region "Public Properties"

        Public Property OvertimeTypeId() As Integer
            Set(ByVal value As Integer)
                _OvertimeTypeId = value
            End Set
            Get
                Return (_OvertimeTypeId)
            End Get
        End Property

        Public Property OvertimeTypeName() As String
            Set(ByVal value As String)
                _OvertimeTypeName = value
            End Set
            Get
                Return (_OvertimeTypeName)
            End Get
        End Property

        Public Property OvertimeTypeArabicName() As String
            Set(ByVal value As String)
                _OvertimeTypeArabicName = value
            End Set
            Get
                Return (_OvertimeTypeArabicName)
            End Get
        End Property

        Public Property OvertimeRate() As Double
            Set(ByVal value As Double)
                _OvertimeRate = value
            End Set
            Get
                Return (_OvertimeRate)
            End Get
        End Property

        Public Property CompensateToLeave() As Boolean
            Set(ByVal value As Boolean)
                _CompensateToLeave = value
            End Set
            Get
                Return (_CompensateToLeave)
            End Get
        End Property

        Public Property FK_LeaveTypeId() As Integer
            Set(ByVal value As Integer)
                _FK_LeaveTypeId = value
            End Set
            Get
                Return (_FK_LeaveTypeId)
            End Get
        End Property

        Public Property MustRequested() As Boolean
            Set(ByVal value As Boolean)
                _MustRequested = value
            End Set
            Get
                Return (_MustRequested)
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

        Public Property OvertimeCalculationConsideration() As Integer
            Set(ByVal value As Integer)
                _OvertimeCalculationConsideration = value
            End Set
            Get
                Return (_OvertimeCalculationConsideration)
            End Get
        End Property

        Public Property OvertimeChangeValue() As Integer
            Set(ByVal value As Integer)
                _OvertimeChangeValue = value
            End Set
            Get
                Return (_OvertimeChangeValue)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALOvertime_Types = New DALOvertime_Types()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALOvertime_Types.Add(_OvertimeTypeName, _OvertimeTypeArabicName, _OvertimeRate, _CompensateToLeave, _FK_LeaveTypeId, _MustRequested, _CREATED_BY, _OvertimeCalculationConsideration, _OvertimeChangeValue)
        End Function

        Public Function Update() As Integer
            Return objDALOvertime_Types.Update(_OvertimeTypeId, _OvertimeTypeName, _OvertimeTypeArabicName, _OvertimeRate, _CompensateToLeave, _FK_LeaveTypeId, _MustRequested, _LAST_UPDATE_BY, _OvertimeCalculationConsideration, _OvertimeChangeValue)

        End Function

        Public Function Delete() As Integer

            Return objDALOvertime_Types.Delete(_OvertimeTypeId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALOvertime_Types.GetAll()

        End Function

        Public Function GetByPK() As Overtime_Types

            Dim dr As DataRow
            dr = objDALOvertime_Types.GetByPK(_OvertimeTypeId)

            If Not IsDBNull(dr("OvertimeTypeId")) Then
                _OvertimeTypeId = dr("OvertimeTypeId")
            End If
            If Not IsDBNull(dr("OvertimeTypeName")) Then
                _OvertimeTypeName = dr("OvertimeTypeName")
            End If
            If Not IsDBNull(dr("OvertimeTypeArabicName")) Then
                _OvertimeTypeArabicName = dr("OvertimeTypeArabicName")
            End If
            If Not IsDBNull(dr("OvertimeRate")) Then
                _OvertimeRate = dr("OvertimeRate")
            End If
            If Not IsDBNull(dr("CompensateToLeave")) Then
                _CompensateToLeave = dr("CompensateToLeave")
            End If
            If Not IsDBNull(dr("FK_LeaveTypeId")) Then
                _FK_LeaveTypeId = dr("FK_LeaveTypeId")
            End If
            If Not IsDBNull(dr("MustRequested")) Then
                _MustRequested = dr("MustRequested")
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
            If Not IsDBNull(dr("OvertimeCalculationConsideration")) Then
                _OvertimeCalculationConsideration = dr("OvertimeCalculationConsideration")
            End If
            If Not IsDBNull(dr("OvertimeChangeValue")) Then
                _OvertimeChangeValue = dr("OvertimeChangeValue")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace