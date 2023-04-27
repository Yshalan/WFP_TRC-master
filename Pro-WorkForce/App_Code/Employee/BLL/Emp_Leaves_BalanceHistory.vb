Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Employees

    Public Class Emp_Leaves_BalanceHistory

#Region "Class Variables"


        Private _BalanceId As Long
        Private _FK_EmployeeId As Long
        Private _FK_EntityId As Integer
        Private _FK_CompanyId As Integer
        Private _FK_LeaveId As Integer
        Private _BalanceDate As DateTime
        Private _Balance As Double
        Private _TotalBalance As Double
        Private _Remarks As String
        Private _FK_EmpLeaveId As Integer
        Private _FK_EmpPermId As Integer
        Private _CREATED_DATE As DateTime
        Private _CREATED_BY As String

        Private _FromDate As DateTime
        Private _ToDate As DateTime
        Private _BalanceYear As Integer

        Private objDALEmp_Leaves_BalanceHistory As DALEmp_Leaves_BalanceHistory

#End Region

#Region "Public Properties"


        Public Property BalanceId() As Long
            Set(ByVal value As Long)
                _BalanceId = value
            End Set
            Get
                Return (_BalanceId)
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

        Public Property FK_EntityId() As Integer
            Set(ByVal value As Integer)
                _FK_EntityId = value
            End Set
            Get
                Return (_FK_EntityId)
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

        Public Property FK_LeaveId() As Integer
            Set(ByVal value As Integer)
                _FK_LeaveId = value
            End Set
            Get
                Return (_FK_LeaveId)
            End Get
        End Property

        Public Property BalanceDate() As DateTime
            Set(ByVal value As DateTime)
                _BalanceDate = value
            End Set
            Get
                Return (_BalanceDate)
            End Get
        End Property

        Public Property Balance() As Double
            Set(ByVal value As Double)
                _Balance = value
            End Set
            Get
                Return (_Balance)
            End Get
        End Property

        Public Property TotalBalance() As Double
            Set(ByVal value As Double)
                _TotalBalance = value
            End Set
            Get
                Return (_TotalBalance)
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

        Public Property FK_EmpLeaveId() As Integer
            Set(ByVal value As Integer)
                _FK_EmpLeaveId = value
            End Set
            Get
                Return (_FK_EmpLeaveId)
            End Get
        End Property

        Public Property FK_EmpPermId() As Integer
            Set(ByVal value As Integer)
                _FK_EmpPermId = value
            End Set
            Get
                Return (_FK_EmpPermId)
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

        Public Property CREATED_BY() As String
            Set(ByVal value As String)
                _CREATED_BY = value
            End Set
            Get
                Return (_CREATED_BY)
            End Get
        End Property

        Public Property FromDate() As DateTime
            Set(ByVal value As DateTime)
                _FromDate = value
            End Set
            Get
                Return (_FromDate)
            End Get
        End Property

        Public Property ToDate() As DateTime
            Set(ByVal value As DateTime)
                _ToDate = value
            End Set
            Get
                Return (_ToDate)
            End Get
        End Property

        Public Property BalanceYear As Integer
            Set(ByVal value As Integer)
                _BalanceYear = value
            End Set
            Get
                Return (_BalanceYear)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALEmp_Leaves_BalanceHistory = New DALEmp_Leaves_BalanceHistory()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALEmp_Leaves_BalanceHistory.Add(_BalanceId, _FK_EmployeeId, _FK_LeaveId, _BalanceDate, _Balance, _TotalBalance, _Remarks, _FK_EmpLeaveId, _CREATED_DATE, _CREATED_BY)
            App_EventsLog.Insert_ToEventLog("Add", _BalanceId, "Emp_Leaves_BalanceHistory", "Employee Leave Balance")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALEmp_Leaves_BalanceHistory.Update(_BalanceId, _FK_EmployeeId, _FK_LeaveId, _BalanceDate, _Balance, _TotalBalance, _Remarks, _FK_EmpLeaveId, _CREATED_DATE, _CREATED_BY)
            App_EventsLog.Insert_ToEventLog("Edit", _BalanceId, "Emp_Leaves_BalanceHistory", "Employee Leave Balance")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALEmp_Leaves_BalanceHistory.Delete(_BalanceId)
            App_EventsLog.Insert_ToEventLog("Delete", _BalanceId, "Emp_Leaves_BalanceHistory", "Employee Leave Balance")
            Return rslt
        End Function

        Public Function AddBalance() As Integer

            Return objDALEmp_Leaves_BalanceHistory.AddAddBalance(_FK_EmployeeId, _FK_LeaveId, _BalanceDate, _Balance, _TotalBalance, _Remarks, _FK_EmpPermId, _CREATED_DATE, _CREATED_BY)
        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_Leaves_BalanceHistory.GetAll()

        End Function

        Public Function GetLeaveBalanceHistory() As DataTable

            Return objDALEmp_Leaves_BalanceHistory.GetLeaveBalanceHistory(_FK_CompanyId, _FK_EntityId, _FK_EmployeeId)

        End Function

        Public Function GetEmpLeaveBalance() As DataTable

            Return objDALEmp_Leaves_BalanceHistory.GetEmpLeaveBalance(_FK_EmployeeId, _FK_EmpLeaveId)

        End Function

        Public Function GetByPK() As Emp_Leaves_BalanceHistory

            Dim dr As DataRow
            dr = objDALEmp_Leaves_BalanceHistory.GetByPK(_BalanceId)

            If Not IsDBNull(dr("BalanceId")) Then
                _BalanceId = dr("BalanceId")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("FK_LeaveId")) Then
                _FK_LeaveId = dr("FK_LeaveId")
            End If
            If Not IsDBNull(dr("BalanceDate")) Then
                _BalanceDate = dr("BalanceDate")
            End If
            If Not IsDBNull(dr("Balance")) Then
                _Balance = dr("Balance")
            End If
            If Not IsDBNull(dr("TotalBalance")) Then
                _TotalBalance = dr("TotalBalance")
            End If
            If Not IsDBNull(dr("Remarks")) Then
                _Remarks = dr("Remarks")
            End If
            If Not IsDBNull(dr("FK_EmpLeaveId")) Then
                _FK_EmpLeaveId = dr("FK_EmpLeaveId")
            End If
            If Not IsDBNull(dr("CREATED_DATE")) Then
                _CREATED_DATE = dr("CREATED_DATE")
            End If
            If Not IsDBNull(dr("CREATED_BY")) Then
                _CREATED_BY = dr("CREATED_BY")
            End If
            Return Me
        End Function

        Public Function GetLastForLeave() As DataRow

            Dim dr As DataRow
            dr = objDALEmp_Leaves_BalanceHistory.GetLastForLeave(_FK_EmployeeId, _FK_EmpLeaveId)

            If (dr IsNot Nothing) Then

                If Not IsDBNull(dr("BalanceId")) Then
                    _BalanceId = dr("BalanceId")
                End If
                If Not IsDBNull(dr("FK_EmployeeId")) Then
                    _FK_EmployeeId = dr("FK_EmployeeId")
                End If
                If Not IsDBNull(dr("FK_LeaveId")) Then
                    _FK_LeaveId = dr("FK_LeaveId")
                End If
                If Not IsDBNull(dr("BalanceDate")) Then
                    _BalanceDate = dr("BalanceDate")
                End If
                If Not IsDBNull(dr("Balance")) Then
                    _Balance = dr("Balance")
                End If
                If Not IsDBNull(dr("TotalBalance")) Then
                    _TotalBalance = dr("TotalBalance")
                End If
                If Not IsDBNull(dr("Remarks")) Then
                    _Remarks = dr("Remarks")
                End If
                If Not IsDBNull(dr("FK_EmpLeaveId")) Then
                    _FK_EmpLeaveId = dr("FK_EmpLeaveId")
                End If
                If Not IsDBNull(dr("CREATED_DATE")) Then
                    _CREATED_DATE = dr("CREATED_DATE")
                End If
                If Not IsDBNull(dr("CREATED_BY")) Then
                    _CREATED_BY = dr("CREATED_BY")
                End If
            End If
            Return dr
        End Function

        Public Function GetLastBalance() As DataRow

            Dim dr As DataRow
            dr = objDALEmp_Leaves_BalanceHistory.GetLastBalance(_FK_EmployeeId, _FK_LeaveId)

            If (dr IsNot Nothing) Then

                If Not IsDBNull(dr("BalanceId")) Then
                    _BalanceId = dr("BalanceId")
                End If
                If Not IsDBNull(dr("FK_EmployeeId")) Then
                    _FK_EmployeeId = dr("FK_EmployeeId")
                End If
                If Not IsDBNull(dr("FK_LeaveId")) Then
                    _FK_LeaveId = dr("FK_LeaveId")
                End If
                If Not IsDBNull(dr("BalanceDate")) Then
                    _BalanceDate = dr("BalanceDate")
                End If
                If Not IsDBNull(dr("Balance")) Then
                    _Balance = dr("Balance")
                End If
                If Not IsDBNull(dr("TotalBalance")) Then
                    _TotalBalance = dr("TotalBalance")
                End If
                If Not IsDBNull(dr("Remarks")) Then
                    _Remarks = dr("Remarks")
                End If
                If Not IsDBNull(dr("FK_EmpLeaveId")) Then
                    _FK_EmpLeaveId = dr("FK_EmpLeaveId")
                End If
                If Not IsDBNull(dr("CREATED_DATE")) Then
                    _CREATED_DATE = dr("CREATED_DATE")
                End If
                If Not IsDBNull(dr("CREATED_BY")) Then
                    _CREATED_BY = dr("CREATED_BY")
                End If
            End If
            Return dr
        End Function

        Public Function GetEmpAllLeave_LastBalance() As DataTable

            Return objDALEmp_Leaves_BalanceHistory.GetEmpAllLeave_LastBalance(_FK_EmployeeId, _FromDate, _ToDate)

        End Function

        Public Function GetEmpLeave_TimeLine() As DataTable

            Return objDALEmp_Leaves_BalanceHistory.GetEmpLeave_TimeLine(_FK_EmployeeId, _FK_LeaveId, _BalanceYear)

        End Function

#End Region

    End Class
End Namespace