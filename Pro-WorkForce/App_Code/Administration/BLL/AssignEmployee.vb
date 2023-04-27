Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events
Imports TA_Announcements

Namespace TA_AssignEmployee

    Public Class AssignEmployee

#Region "Class Variables"


        Private _AssignedEmployee As Integer
        Private _LeaveId As Integer
        Private _FK_EmployeeId As Integer

        Private objDALAssignEmployee As DALAssignEmployee

#End Region

#Region "Public Properties"

        Public Property LeaveId() As Integer
            Set(ByVal value As Integer)
                _LeaveId = value
            End Set
            Get
                Return (_LeaveId)
            End Get
        End Property

        Public Property FK_EmployeeId() As Integer
            Set(ByVal value As Integer)
                _FK_EmployeeId = value
            End Set
            Get
                Return (_FK_EmployeeId)
            End Get
        End Property

        Public Property AssignedEmployee() As Integer
            Set(ByVal value As Integer)
                _AssignedEmployee = value
            End Set
            Get
                Return (_AssignedEmployee)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALAssignEmployee = New DALAssignEmployee()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALAssignEmployee.Add(_AssignedEmployee, _FK_EmployeeId, _LeaveId)
            Return rslt

        End Function

#End Region

    End Class
End Namespace