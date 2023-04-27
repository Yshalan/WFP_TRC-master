Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Lookup
    Public Class Holiday_Religion
#Region "Class Variables"
        Private _HolidayId As Integer
        Private _ReligionId As Integer
        Private objDALHoliday_Religion As DALHoliday_Religion
#End Region

#Region "Public Properties"
        Public Property HolidayId() As Integer
            Set(ByVal value As Integer)
                _HolidayId = value
            End Set
            Get
                Return (_HolidayId)
            End Get
        End Property
        Public Property ReligionId() As Integer
            Set(ByVal value As Integer)
                _ReligionId = value
            End Set
            Get
                Return (_ReligionId)
            End Get
        End Property
#End Region

#Region "Constructor"
        Public Sub New()
            objDALHoliday_Religion = New DALHoliday_Religion()
        End Sub

#End Region

#Region "Methods"
        Public Function Add() As Integer

            Dim rslt As Integer = objDALHoliday_Religion.Add(_HolidayId, _ReligionId)
            Return rslt
        End Function

        Public Function Update() As Integer

            Return objDALHoliday_Religion.Update(_HolidayId, _ReligionId)
        End Function

        Public Function Delete() As Integer
            Return objDALHoliday_Religion.Delete(_HolidayId)
        End Function

        Public Function GetAll() As DataTable
            Return objDALHoliday_Religion.GetAll()
        End Function
        Public Function GetAll_Religion() As DataTable
            Return objDALHoliday_Religion.GetAll_Religion()
        End Function
        Public Function GetByPK() As DataTable
            Return objDALHoliday_Religion.GetByPK(_HolidayId)
        End Function

#End Region

    End Class
End Namespace