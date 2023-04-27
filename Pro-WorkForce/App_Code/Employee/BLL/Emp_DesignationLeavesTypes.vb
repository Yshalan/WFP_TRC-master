Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Lookup

    Public Class Emp_DesignationLeavesTypes

#Region "Class Variables"


        Private _FK_LeaveId As Integer
        Private _FK_DesignationId As Integer
        Private objDALEmp_DesignationLeavesTypes As DALEmp_DesignationLeavesTypes

#End Region

#Region "Public Properties"


        Public Property FK_LeaveId() As Integer
            Set(ByVal value As Integer)
                _FK_LeaveId = value
            End Set
            Get
                Return (_FK_LeaveId)
            End Get
        End Property


        Public Property FK_DesignationId() As Integer
            Set(ByVal value As Integer)
                _FK_DesignationId = value
            End Set
            Get
                Return (_FK_DesignationId)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALEmp_DesignationLeavesTypes = New DALEmp_DesignationLeavesTypes()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALEmp_DesignationLeavesTypes.Add(_FK_LeaveId, _FK_DesignationId)
        End Function

        Public Function Update() As Integer

            Return objDALEmp_DesignationLeavesTypes.Update(_FK_LeaveId, _FK_DesignationId)

        End Function



        Public Function Delete() As Integer

            Return objDALEmp_DesignationLeavesTypes.Delete(_FK_LeaveId, _FK_DesignationId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_DesignationLeavesTypes.GetAll()

        End Function

        Public Function GetByPK() As Emp_DesignationLeavesTypes

            Dim dr As DataRow
            dr = objDALEmp_DesignationLeavesTypes.GetByPK(_FK_LeaveId, _FK_DesignationId)

            If Not IsDBNull(dr("FK_LeaveId")) Then
                _FK_LeaveId = dr("FK_LeaveId")
            End If
            If Not IsDBNull(dr("FK_DesignationId")) Then
                _FK_DesignationId = dr("FK_DesignationId")
            End If
            Return Me
        End Function

        Public Function DeleteByFk() As Integer
            Return objDALEmp_DesignationLeavesTypes.DeleteByFkDesignation(_FK_DesignationId)
        End Function

        Public Function SelectByFk() As DataTable
            Return objDALEmp_DesignationLeavesTypes.SelectByFk(_FK_DesignationId)
        End Function
#End Region

    End Class
End Namespace