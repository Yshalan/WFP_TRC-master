Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Lookup
    Public Class Holiday_WorkLocation
#Region "Class Variables"
        Private _HolidayId As Integer
        Private _WorkLocationId As Integer
        Private objDALHoliday_WorkLocation As DALHoliday_WorkLocation
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
        Public Property WorkLocationId() As Integer
            Set(ByVal value As Integer)
                _WorkLocationId = value
            End Set
            Get
                Return (_WorkLocationId)
            End Get
        End Property
#End Region

#Region "Constructor"
        Public Sub New()
            objDALHoliday_WorkLocation = New DALHoliday_WorkLocation()
        End Sub

#End Region

#Region "Methods"
        Public Function Add() As Integer

            Dim rslt As Integer = objDALHoliday_WorkLocation.Add(_HolidayId, _WorkLocationId)
            Return rslt
        End Function

        Public Function Update() As Integer

            Return objDALHoliday_WorkLocation.Update(_HolidayId, _WorkLocationId)
        End Function

        Public Function Delete() As Integer
            Return objDALHoliday_WorkLocation.Delete(_HolidayId)
        End Function

        Public Function GetAll() As DataTable
            Return objDALHoliday_WorkLocation.GetAll()
        End Function
        Public Function GetAll_WorkLocations() As DataTable
            Return objDALHoliday_WorkLocation.GetAll_WorkLocations()
        End Function

        Public Function GetSpecific_WorkLocations(ByVal CompaniesIds As String) As DataTable
            Return objDALHoliday_WorkLocation.GetSpecific_WorkLocations(CompaniesIds)
        End Function

        Public Function GetByPK() As DataTable
            Return objDALHoliday_WorkLocation.GetByPK(_HolidayId)
        End Function

#End Region

    End Class
End Namespace