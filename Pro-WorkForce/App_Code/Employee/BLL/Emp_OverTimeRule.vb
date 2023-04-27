Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Employees

    Public Class Emp_OverTimeRule

#Region "Class Variables"

        Private _FK_EmployeeId As Long
        Private _FK_RuleId As Integer
        Private _FromDate As DateTime
        Private _ToDate As DateTime
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALEmp_OverTimeRule As DALEmp_OverTimeRule
        Private _FK_ManagerId As Integer
        Private _FK_StatusId As Integer
        Private _EmpOvertimeId As Integer
        Private _RejectedReason As String
        Private _IsFinallyApproved As Boolean
        Private _ApprovedDuration As Integer

#End Region

#Region "Public Properties"
        Public Property FK_EmployeeId() As Long
            Set(ByVal value As Long)
                _FK_EmployeeId = value
            End Set
            Get
                Return (_FK_EmployeeId)
            End Get
        End Property


        Public Property FK_ManagerId() As Integer
            Set(ByVal value As Integer)
                _FK_ManagerId = value
            End Set
            Get
                Return (_FK_ManagerId)
            End Get
        End Property

        Public Property FK_StatusId() As Integer
            Set(ByVal value As Integer)
                _FK_StatusId = value
            End Set
            Get
                Return (_FK_StatusId)
            End Get
        End Property

        Public Property EmpOvertimeId() As Integer
            Set(ByVal value As Integer)
                _EmpOvertimeId = value
            End Set
            Get
                Return (_EmpOvertimeId)
            End Get
        End Property

        Public Property FK_RuleId() As Integer
            Set(ByVal value As Integer)
                _FK_RuleId = value
            End Set
            Get
                Return (_FK_RuleId)
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


        Public Property LAST_UPDATE_BY() As String
            Set(ByVal value As String)
                _LAST_UPDATE_BY = value
            End Set
            Get
                Return (_LAST_UPDATE_BY)
            End Get
        End Property


        Public Property LAST_UPDATE_DATE() As DateTime
            Set(ByVal value As DateTime)
                _LAST_UPDATE_DATE = value
            End Set
            Get
                Return (_LAST_UPDATE_DATE)
            End Get
        End Property

        Public Property RejectedReason() As String
            Get
                Return _RejectedReason
            End Get
            Set(ByVal value As String)
                _RejectedReason = value
            End Set
        End Property
        Public Property ApprovedDuration() As Integer
            Set(ByVal value As Integer)
                _ApprovedDuration = value
            End Set
            Get
                Return (_ApprovedDuration)
            End Get
        End Property

        Public Property IsFinallyApproved() As Boolean
            Set(ByVal value As Boolean)
                _IsFinallyApproved = value
            End Set
            Get
                Return (_IsFinallyApproved)
            End Get
        End Property

#End Region

#Region "Constructor"
        Public Sub New()

            objDALEmp_OverTimeRule = New DALEmp_OverTimeRule()

        End Sub
#End Region

#Region "Methods"

        Public Function Add() As Integer
            Dim rslt As Integer = objDALEmp_OverTimeRule.Add(_FK_EmployeeId, _FK_RuleId, _FromDate, _ToDate, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE)
            App_EventsLog.Insert_ToEventLog("Add", _FK_EmployeeId, "Emp_OvertimeRule", "Employee")
            Return rslt
        End Function

        Public Function AssignTAPolicy() As Integer
            Return objDALEmp_OverTimeRule.AssignTAPolicy(_FK_EmployeeId, _FK_RuleId, _FromDate, _ToDate, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE)
        End Function

        Public Function Update() As Integer
            Dim rslt As Integer = objDALEmp_OverTimeRule.Update(_FK_EmployeeId, _FK_RuleId, _FromDate, _ToDate)
            App_EventsLog.Insert_ToEventLog("Edit", _FK_EmployeeId, "Emp_OvertimeRule", "Employee")
            Return rslt
        End Function

        Public Function Delete() As Integer
            Dim rslt As Integer = objDALEmp_OverTimeRule.Delete(_FK_EmployeeId, _FK_RuleId, _FromDate)
            App_EventsLog.Insert_ToEventLog("Delete", _FK_EmployeeId, "Emp_OvertimeRule", "Employee")
            Return rslt
        End Function

        Public Function GetAll() As DataTable
            Return objDALEmp_OverTimeRule.GetAll()
        End Function

        Public Function GetByPK() As Emp_OverTimeRule
            Dim dr As DataRow
            dr = objDALEmp_OverTimeRule.GetByPK(_FK_EmployeeId, _FK_RuleId, _FromDate)
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("FK_RuleId")) Then
                _FK_RuleId = dr("FK_RuleId")
            End If
            If Not IsDBNull(dr("FromDate")) Then
                _FromDate = dr("FromDate")
            End If
            If Not IsDBNull(dr("ToDate")) Then
                _ToDate = dr("ToDate")
            Else
                _ToDate = Nothing
            End If
            Return Me
        End Function

        Public Function GetByEmployeeId() As DataTable
            Return objDALEmp_OverTimeRule.GetByEmployeeId(_FK_EmployeeId)
        End Function

        Public Function GetActiveRuleId() As Emp_OverTimeRule
            Dim dr As DataRow
            dr = objDALEmp_OverTimeRule.GetActiveOTRuleId(_FK_EmployeeId)
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("FK_RuleId")) Then
                _FK_RuleId = dr("FK_RuleId")
            End If
            If Not IsDBNull(dr("FromDate")) Then
                _FromDate = dr("FromDate")
            End If
            If Not IsDBNull(dr("ToDate")) Then
                _ToDate = dr("ToDate")
            End If
            Return Me
        End Function

        Public Function GetEmployeeLastStartedOTRule() As Emp_OverTimeRule
            Dim dr As DataRow
            If (dr IsNot Nothing) Then
                dr = objDALEmp_OverTimeRule.GetEmployeeLastStartedOTRule(_FK_EmployeeId)
                If Not IsDBNull(dr("FK_EmployeeId")) Then
                    _FK_EmployeeId = dr("FK_EmployeeId")
                End If
                If Not IsDBNull(dr("FK_RuleId")) Then
                    _FK_RuleId = dr("FK_RuleId")
                End If
                If Not IsDBNull(dr("FromDate")) Then
                    _FromDate = dr("FromDate")
                End If
                If Not IsDBNull(dr("ToDate")) Then
                    _ToDate = dr("ToDate")
                End If
            End If
            Return Me
        End Function

        Public Function Emp_AssignRule() As Integer
            Return objDALEmp_OverTimeRule.Add(_FK_EmployeeId, _FK_RuleId, _FromDate, _ToDate, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE)
        End Function

        Public Function GetByDirectManager() As DataTable

            Return objDALEmp_OverTimeRule.GetByDirectManager(_FK_ManagerId, _FK_StatusId)

        End Function

        Public Function UpdateOverTimeStatus() As Integer

            Return objDALEmp_OverTimeRule.UpdateOverTimeStatus(_EmpOvertimeId, _FK_StatusId, _RejectedReason, _ApprovedDuration, _IsFinallyApproved)

        End Function

        Public Function GetByHR() As DataTable

            Return objDALEmp_OverTimeRule.GetByHR(_FK_ManagerId, _FK_StatusId)

        End Function

        Public Function GetAllowedOTRule() As DataTable
            Return objDALEmp_OverTimeRule.GetAllowedOTRule(_FK_EmployeeId)
        End Function

#End Region

    End Class
End Namespace