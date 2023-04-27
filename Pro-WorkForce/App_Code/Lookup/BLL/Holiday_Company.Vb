Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Lookup
    Public Class Holiday_Company
#Region "Class Variables"
        Private _HolidayId As Integer
        Private _CompanyId As Integer
        Private objDALHoliday_Company As DALHoliday_Company
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
        Public Property CompanyId() As Integer
            Set(ByVal value As Integer)
                _CompanyId = value
            End Set
            Get
                Return (_CompanyId)
            End Get
        End Property
#End Region

#Region "Constructor"
        Public Sub New()
            objDALHoliday_Company = New DALHoliday_Company()
        End Sub

#End Region

#Region "Methods"
        Public Function Add() As Integer
            Dim rslt As Integer = objDALHoliday_Company.Add(_HolidayId, _CompanyId)
            Return rslt
        End Function
        Public Function Update() As Integer

            Return objDALHoliday_Company.Update(_HolidayId, _CompanyId)
        End Function
        Public Function Delete() As Integer
            Return objDALHoliday_Company.Delete(_HolidayId)
        End Function
        Public Function GetAll_OrgCompany() As DataTable
            Return objDALHoliday_Company.GetAll_OrgCompany()
        End Function
        Public Function GetAll() As DataTable
            Return objDALHoliday_Company.GetAll()
        End Function
        Public Function GetByPK() As DataTable
            Return objDALHoliday_Company.GetByPK(_HolidayId)
        End Function
#End Region

    End Class
End Namespace