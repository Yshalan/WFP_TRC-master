Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Admin

    Public Class Emp_HR_Company

#Region "Class Variables"


        Private _FK_HREmployeeId As Long
        Private _FK_CompanyId As Integer
        Private objDALEmp_HR_Company As DALEmp_HR_Company

#End Region

#Region "Public Properties"


        Public Property FK_HREmployeeId() As Long
            Set(ByVal value As Long)
                _FK_HREmployeeId = value
            End Set
            Get
                Return (_FK_HREmployeeId)
            End Get
        End Property


        Public Property FK_CompanyId() As Integer
            Set(ByVal value As Integer)
                _FK_CompanyId = value
            End Set
            Get
                Return (_FK_CompanyId)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALEmp_HR_Company = New DALEmp_HR_Company()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALEmp_HR_Company.Add(_FK_HREmployeeId, _FK_CompanyId)
        End Function

        Public Function Update() As Integer

            Return objDALEmp_HR_Company.Update(_FK_HREmployeeId, _FK_CompanyId)

        End Function



        Public Function Delete() As Integer

            Return objDALEmp_HR_Company.Delete(_FK_HREmployeeId, _FK_CompanyId)

        End Function

        Public Function DeleteBy_FK_HREmployeeId() As Integer

            Return objDALEmp_HR_Company.DeleteBy_FK_HREmployeeId(_FK_HREmployeeId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_HR_Company.GetAll()

        End Function

        Public Function GetByPK() As Emp_HR_Company

            Dim dr As DataRow
            dr = objDALEmp_HR_Company.GetByPK(_FK_HREmployeeId, _FK_CompanyId)

            If Not IsDBNull(dr("FK_HREmployeeId")) Then
                _FK_HREmployeeId = dr("FK_HREmployeeId")
            End If
            If Not IsDBNull(dr("FK_CompanyId")) Then
                _FK_CompanyId = dr("FK_CompanyId")
            End If
            Return Me
        End Function

        Public Function GetBy_FK_HREmployeeId() As DataTable

            Return objDALEmp_HR_Company.GetBy_FK_HREmployeeId(_FK_HREmployeeId)

        End Function

#End Region

    End Class
End Namespace