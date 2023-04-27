Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Admin
    Public Class Emp_HR

#Region "Class Variables"

        Private _HREmployeeId As Long
        Private _HRDesignation As Integer
        Private _EmployeeDesignation As String
        Private _EmployeeName As String
        Private _IsSpecificEntity As Boolean
        Private _IsSpecificCompany As Boolean
        Private objDALEmp_HR As DALEmp_HR

#End Region

#Region "Public Properties"
        
        Public Property HREmployeeId() As Long
            Get
                Return _HREmployeeId
            End Get
            Set(ByVal value As Long)
                _HREmployeeId = value
            End Set
        End Property

        Public Property HRDesignation() As Integer
            Get
                Return _HRDesignation
            End Get
            Set(ByVal value As Integer)
                _HRDesignation = value
            End Set
        End Property

        Public Property EmployeeName() As String
            Get
                Return _EmployeeName
            End Get
            Set(ByVal value As String)
                _EmployeeName = value
            End Set
        End Property

        Public Property EmployeeDesignation() As String
            Get
                Return _EmployeeDesignation
            End Get
            Set(ByVal value As String)
                _EmployeeDesignation = value
            End Set
        End Property

        Public Property IsSpecificCompany() As Boolean
            Get
                Return _IsSpecificCompany
            End Get
            Set(ByVal value As Boolean)
                _IsSpecificCompany = value
            End Set
        End Property

        Public Property IsSpecificEntity() As Boolean
            Get
                Return _IsSpecificEntity
            End Get
            Set(ByVal value As Boolean)
                _IsSpecificEntity = value
            End Set
        End Property
#End Region

#Region "Constructor"

        Public Sub New()
            objDALEmp_HR = New DALEmp_HR
        End Sub
#End Region

#Region "Methods"

        Public Function GetAll() As DataTable
            Return objDALEmp_HR.GetAll()
        End Function

        Public Function GetByPK() As Emp_HR
            Dim dr As DataRow
            dr = objDALEmp_HR.GetByPK(_HREmployeeId)
            If Not dr Is Nothing Then
                If Not IsDBNull(dr("HRDesignation")) Then
                    _HRDesignation = dr("HRDesignation")
                End If

                If Not IsDBNull(dr("HREmployeeId")) Then
                    _HREmployeeId = dr("HREmployeeId")
                End If
                If Not IsDBNull(dr("IsSpecificCompany")) Then
                    _IsSpecificCompany = dr("IsSpecificCompany")
                End If
                If Not IsDBNull(dr("IsSpecificEntity")) Then
                    _IsSpecificEntity = dr("IsSpecificEntity")
                End If
                Return Me
            Else
                Return Nothing
            End If

        End Function

        Public Function Insert() As Integer
            Dim rslt As Integer = objDALEmp_HR.Add(_HREmployeeId, _HRDesignation, _IsSpecificEntity, _IsSpecificCompany)
            App_EventsLog.Insert_ToEventLog("Add", _HREmployeeId, "Emp_HR", "Employee HR")
            Return rslt
        End Function

        Public Function Update() As Integer
            Dim rslt As Integer = objDALEmp_HR.Update(_HREmployeeId, _HRDesignation, _IsSpecificEntity, _IsSpecificCompany)
            App_EventsLog.Insert_ToEventLog("Edit", _HREmployeeId, "Emp_HR", "Employee HR")
            Return rslt
        End Function

        Public Function Delete() As Integer
            Dim rslt As Integer = objDALEmp_HR.Delete(_HREmployeeId)
            App_EventsLog.Insert_ToEventLog("Delete", _HREmployeeId, "Emp_HR", "Employee HR")
            Return rslt
        End Function

        Public Function GetHREmployeeWithInnerEmployees() As DataTable

            Return objDALEmp_HR.GetHREmployeeWithInnerEmployees()

        End Function

        Public Function GetAllHRNotifications() As DataTable

            Return objDALEmp_HR.GetAllHRNotifications()

        End Function

#End Region

    End Class

End Namespace
