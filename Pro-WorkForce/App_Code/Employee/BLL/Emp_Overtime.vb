Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Employees

    Public Class Emp_Overtime

#Region "Class Variables"


        Private _EmpOverTimeId As Integer
        Private _FK_EmployeeId As Long
        Private _FK_OvertimeRuleId As Integer
        Private _FromDateTime As DateTime
        Private _ToDateTime As DateTime
        Private _Duration As Integer
        Private _ApprovedDuration As Integer
        Private _IsHigh As Boolean
        Private _FK_ApprovalId As Long
        Private _IsCompensateLatetime As Boolean
        Private _IsLeaveBalance As Boolean
        Private _IsFinancial As Boolean
        Private _ProcessStatus As Integer
        Private _ProccessDate As DateTime
        Private _RejectionReason As String
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private _Emp_Remarks As String
        Private objDALEmp_Overtime As DALEmp_Overtime

#End Region

#Region "Public Properties"

        Public Property EmpOverTimeId() As Integer
            Set(ByVal value As Integer)
                _EmpOverTimeId = value
            End Set
            Get
                Return (_EmpOverTimeId)
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

        Public Property FK_OvertimeRuleId() As Integer
            Set(ByVal value As Integer)
                _FK_OvertimeRuleId = value
            End Set
            Get
                Return (_FK_OvertimeRuleId)
            End Get
        End Property

        Public Property FromDateTime() As DateTime
            Set(ByVal value As DateTime)
                _FromDateTime = value
            End Set
            Get
                Return (_FromDateTime)
            End Get
        End Property

        Public Property ToDateTime() As DateTime
            Set(ByVal value As DateTime)
                _ToDateTime = value
            End Set
            Get
                Return (_ToDateTime)
            End Get
        End Property

        Public Property Duration() As Integer
            Set(ByVal value As Integer)
                _Duration = value
            End Set
            Get
                Return (_Duration)
            End Get
        End Property

        Public Property ApprovedDuration() As Integer
            Set(ByVal value As Integer)
                _ApprovedDuration = value
            End Set
            Get
                Return (_ApprovedDuration)
            End Get
        End Property

        Public Property IsHigh() As Boolean
            Set(ByVal value As Boolean)
                _IsHigh = value
            End Set
            Get
                Return (_IsHigh)
            End Get
        End Property

        Public Property FK_ApprovalId() As Long
            Set(ByVal value As Long)
                _FK_ApprovalId = value
            End Set
            Get
                Return (_FK_ApprovalId)
            End Get
        End Property

        Public Property IsCompensateLatetime() As Boolean
            Set(ByVal value As Boolean)
                _IsCompensateLatetime = value
            End Set
            Get
                Return (_IsCompensateLatetime)
            End Get
        End Property

        Public Property IsLeaveBalance() As Boolean
            Set(ByVal value As Boolean)
                _IsLeaveBalance = value
            End Set
            Get
                Return (_IsLeaveBalance)
            End Get
        End Property

        Public Property IsFinancial() As Boolean
            Set(ByVal value As Boolean)
                _IsFinancial = value
            End Set
            Get
                Return (_IsFinancial)
            End Get
        End Property

        Public Property ProcessStatus() As Integer
            Set(ByVal value As Integer)
                _ProcessStatus = value
            End Set
            Get
                Return (_ProcessStatus)
            End Get
        End Property

        Public Property ProccessDate() As DateTime
            Set(ByVal value As DateTime)
                _ProccessDate = value
            End Set
            Get
                Return (_ProccessDate)
            End Get
        End Property

        Public Property RejectionReason() As String
            Set(ByVal value As String)
                _RejectionReason = value
            End Set
            Get
                Return (_RejectionReason)
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

        Public Property Emp_Remarks() As String
            Set(ByVal value As String)
                _Emp_Remarks = value
            End Set
            Get
                Return (_Emp_Remarks)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALEmp_Overtime = New DALEmp_Overtime()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALEmp_Overtime.Add(_FK_EmployeeId, _FK_OvertimeRuleId, _FromDateTime, _ToDateTime, _Duration, _ApprovedDuration, _IsHigh, _FK_ApprovalId, _IsCompensateLatetime, _IsLeaveBalance, _IsFinancial, _ProcessStatus, _RejectionReason, _CREATED_BY, _Emp_Remarks)
        End Function

        Public Function Update() As Integer

            Return objDALEmp_Overtime.Update(_EmpOverTimeId, _FK_EmployeeId, _FK_OvertimeRuleId, _FromDateTime, _ToDateTime, _Duration, _ApprovedDuration, _IsHigh, _FK_ApprovalId, _IsCompensateLatetime, _IsLeaveBalance, _IsFinancial, _ProcessStatus, _ProccessDate, _RejectionReason, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE, _Emp_Remarks)

        End Function

        Public Function UpdateRequest() As Integer

            Return objDALEmp_Overtime.UpdateRequest(_EmpOverTimeId, _ProcessStatus, _LAST_UPDATE_BY, _Emp_Remarks)

        End Function

        Public Function Delete() As Integer

            Return objDALEmp_Overtime.Delete(_EmpOverTimeId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_Overtime.GetAll()

        End Function

        Public Function GetByPK() As Emp_Overtime

            Dim dr As DataRow
            dr = objDALEmp_Overtime.GetByPK(_EmpOverTimeId)

            If Not IsDBNull(dr("EmpOverTimeId")) Then
                _EmpOverTimeId = dr("EmpOverTimeId")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("FK_OvertimeRuleId")) Then
                _FK_OvertimeRuleId = dr("FK_OvertimeRuleId")
            End If
            If Not IsDBNull(dr("FromDateTime")) Then
                _FromDateTime = dr("FromDateTime")
            End If
            If Not IsDBNull(dr("ToDateTime")) Then
                _ToDateTime = dr("ToDateTime")
            End If
            If Not IsDBNull(dr("Duration")) Then
                _Duration = dr("Duration")
            End If
            If Not IsDBNull(dr("ApprovedDuration")) Then
                _ApprovedDuration = dr("ApprovedDuration")
            End If
            If Not IsDBNull(dr("IsHigh")) Then
                _IsHigh = dr("IsHigh")
            End If
            If Not IsDBNull(dr("FK_ApprovalId")) Then
                _FK_ApprovalId = dr("FK_ApprovalId")
            End If
            If Not IsDBNull(dr("IsCompensateLatetime")) Then
                _IsCompensateLatetime = dr("IsCompensateLatetime")
            End If
            If Not IsDBNull(dr("IsLeaveBalance")) Then
                _IsLeaveBalance = dr("IsLeaveBalance")
            End If
            If Not IsDBNull(dr("IsFinancial")) Then
                _IsFinancial = dr("IsFinancial")
            End If
            If Not IsDBNull(dr("ProcessStatus")) Then
                _ProcessStatus = dr("ProcessStatus")
            End If
            If Not IsDBNull(dr("ProccessDate")) Then
                _ProccessDate = dr("ProccessDate")
            End If
            If Not IsDBNull(dr("RejectionReason")) Then
                _RejectionReason = dr("RejectionReason")
            End If
            If Not IsDBNull(dr("CREATED_BY")) Then
                _CREATED_BY = dr("CREATED_BY")
            End If
            If Not IsDBNull(dr("CREATED_DATE")) Then
                _CREATED_DATE = dr("CREATED_DATE")
            End If
            If Not IsDBNull(dr("LAST_UPDATE_BY")) Then
                _LAST_UPDATE_BY = dr("LAST_UPDATE_BY")
            End If
            If Not IsDBNull(dr("LAST_UPDATE_DATE")) Then
                _LAST_UPDATE_DATE = dr("LAST_UPDATE_DATE")
            End If
            If Not IsDBNull(dr("Emp_Remarks")) Then
                _Emp_Remarks = dr("Emp_Remarks")
            End If
            Return Me
        End Function

        Public Function GetByEmployee() As DataTable

            Return objDALEmp_Overtime.GetByEmployee(_FK_EmployeeId, _FromDateTime, _ToDateTime, _ProcessStatus)

        End Function

#End Region

    End Class
End Namespace