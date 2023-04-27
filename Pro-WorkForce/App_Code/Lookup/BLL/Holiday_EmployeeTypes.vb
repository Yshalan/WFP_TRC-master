Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.LookUp

    Public Class Holiday_EmployeeTypes

#Region "Class Variables"


        Private _FK_HolidayId As Integer
        Private _FK_EmployeeTypeId As Integer
        Private objDALHoliday_EmployeeTypes As DALHoliday_EmployeeTypes

#End Region

#Region "Public Properties"


        Public Property FK_HolidayId() As Integer
            Set(ByVal value As Integer)
                _FK_HolidayId = value
            End Set
            Get
                Return (_FK_HolidayId)
            End Get
        End Property


        Public Property FK_EmployeeTypeId() As Integer
            Set(ByVal value As Integer)
                _FK_EmployeeTypeId = value
            End Set
            Get
                Return (_FK_EmployeeTypeId)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALHoliday_EmployeeTypes = New DALHoliday_EmployeeTypes()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALHoliday_EmployeeTypes.Add(_FK_HolidayId, _FK_EmployeeTypeId)
        End Function

        Public Function Update() As Integer

            Return objDALHoliday_EmployeeTypes.Update(_FK_HolidayId, _FK_EmployeeTypeId)

        End Function



        Public Function Delete() As Integer

            Return objDALHoliday_EmployeeTypes.Delete(_FK_HolidayId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALHoliday_EmployeeTypes.GetAll()

        End Function

        Public Function GetAll_EmployeeType() As DataTable
            Return objDALHoliday_EmployeeTypes.GetAll_EmployeeType()
        End Function

        Public Function GetByPK() As DataTable
            Return objDALHoliday_EmployeeTypes.GetByPK(_FK_HolidayId)
        End Function

#End Region

    End Class
End Namespace