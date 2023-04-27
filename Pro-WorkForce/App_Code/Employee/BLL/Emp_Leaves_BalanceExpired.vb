Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Employees

    Public Class Emp_Leaves_BalanceExpired

#Region "Class Variables"


        Private _BalanceExpiredId As Long
        Private _FK_EmployeeId As Long
        Private _FK_CompanyId As Integer
        Private _FK_EntityId As Integer
        Private _FK_LeaveId As Integer
        Private _ExpireDate As DateTime
        Private _ExpireBalance As Double
        Private _Remarks As String
        Private _Action As Integer
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private objDALEmp_Leaves_BalanceExpired As DALEmp_Leaves_BalanceExpired

#End Region

#Region "Public Properties"


        Public Property BalanceExpiredId() As Long
            Set(ByVal value As Long)
                _BalanceExpiredId = value
            End Set
            Get
                Return (_BalanceExpiredId)
            End Get
        End Property


        Public Property FK_EmployeeId() As Long
            Set(ByVal value As Long)
                _FK_EmployeeId = value
            End Set
            Get
                Return (_FK_EmployeeId)
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

        Public Property FK_EntityId() As Integer
            Set(ByVal value As Integer)
                _FK_EntityId = value
            End Set
            Get
                Return (_FK_EntityId)
            End Get
        End Property

        Public Property FK_LeaveId() As Integer
            Set(ByVal value As Integer)
                _FK_LeaveId = value
            End Set
            Get
                Return (_FK_LeaveId)
            End Get
        End Property


        Public Property ExpireDate() As DateTime
            Set(ByVal value As DateTime)
                _ExpireDate = value
            End Set
            Get
                Return (_ExpireDate)
            End Get
        End Property


        Public Property ExpireBalance() As Double
            Set(ByVal value As Double)
                _ExpireBalance = value
            End Set
            Get
                Return (_ExpireBalance)
            End Get
        End Property


        Public Property Remarks() As String
            Set(ByVal value As String)
                _Remarks = value
            End Set
            Get
                Return (_Remarks)
            End Get
        End Property


        Public Property Action() As Integer
            Set(ByVal value As Integer)
                _Action = value
            End Set
            Get
                Return (_Action)
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

#End Region


#Region "Constructor"

        Public Sub New()

            objDALEmp_Leaves_BalanceExpired = New DALEmp_Leaves_BalanceExpired()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALEmp_Leaves_BalanceExpired.Add(_BalanceExpiredId, _FK_EmployeeId, _FK_LeaveId, _ExpireDate, _ExpireBalance, _Remarks, _Action, _CREATED_BY, _CREATED_DATE)
        End Function

        Public Function Update() As Integer

            Return objDALEmp_Leaves_BalanceExpired.Update(_BalanceExpiredId, _FK_EmployeeId, _FK_LeaveId, _ExpireDate, _ExpireBalance, _Remarks, _Action, _CREATED_BY, _CREATED_DATE)

        End Function



        Public Function Delete() As Integer

            Return objDALEmp_Leaves_BalanceExpired.Delete(_BalanceExpiredId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_Leaves_BalanceExpired.GetAll()

        End Function

        Public Function GetLeaveExpiredBalance() As DataTable

            Return objDALEmp_Leaves_BalanceExpired.GetLeaveExpiredBalance(_FK_CompanyId, _FK_EntityId, _FK_EmployeeId)

        End Function

        Public Function GetByPK() As Emp_Leaves_BalanceExpired

            Dim dr As DataRow
            dr = objDALEmp_Leaves_BalanceExpired.GetByPK(_BalanceExpiredId)

            If Not IsDBNull(dr("BalanceExpiredId")) Then
                _BalanceExpiredId = dr("BalanceExpiredId")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("FK_LeaveId")) Then
                _FK_LeaveId = dr("FK_LeaveId")
            End If
            If Not IsDBNull(dr("ExpireDate")) Then
                _ExpireDate = dr("ExpireDate")
            End If
            If Not IsDBNull(dr("ExpireBalance")) Then
                _ExpireBalance = dr("ExpireBalance")
            End If
            If Not IsDBNull(dr("Remarks")) Then
                _Remarks = dr("Remarks")
            End If
            If Not IsDBNull(dr("Action")) Then
                _Action = dr("Action")
            End If
            If Not IsDBNull(dr("CREATED_BY")) Then
                _CREATED_BY = dr("CREATED_BY")
            End If
            If Not IsDBNull(dr("CREATED_DATE")) Then
                _CREATED_DATE = dr("CREATED_DATE")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace