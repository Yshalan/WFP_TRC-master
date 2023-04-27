Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA_ApproveViolationToERP

    Public Class ApproveViolationToERP

#Region "Class Variables"


        Private _ID As Integer
        Private _FK_EmployeeId As Integer
        Private _ViolationType As String
        Private _ViolationDate As DateTime
        Private _IsApproved As Boolean
        Private _APPROVED_BY As String
        Private _APPROVED_DATE As DateTime
        Private _ViolationDateNum As Integer
        Private _CompanyId As Integer
        Private _EntityId As Integer
        Private _WorkLocationId As Integer
        Private _LogicalGroupId As Integer
        Private _Year As Integer
        Private _Month As Integer
        Private _DirectStaffOnly As Boolean
        Private objDALApproveViolationToERP As DALApproveViolationToERP

#End Region

#Region "Public Properties"
        Private _recordId As String
        Private _deductionId As String


        Public Property ID() As Integer
            Set(ByVal value As Integer)
                _ID = value
            End Set
            Get
                Return (_ID)
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


        Public Property ViolationType() As String
            Set(ByVal value As String)
                _ViolationType = value
            End Set
            Get
                Return (_ViolationType)
            End Get
        End Property


        Public Property ViolationDate() As DateTime
            Set(ByVal value As DateTime)
                _ViolationDate = value
            End Set
            Get
                Return (_ViolationDate)
            End Get
        End Property

        Public Property ViolationDateNum() As Integer
            Set(ByVal value As Integer)
                _ViolationDateNum = value
            End Set
            Get
                Return (_ViolationDateNum)
            End Get
        End Property

        Public Property IsApproved() As Boolean
            Set(ByVal value As Boolean)
                _IsApproved = value
            End Set
            Get
                Return (_IsApproved)
            End Get
        End Property


        Public Property APPROVED_BY() As String
            Set(ByVal value As String)
                _APPROVED_BY = value
            End Set
            Get
                Return (_APPROVED_BY)
            End Get
        End Property


        Public Property APPROVED_DATE() As DateTime
            Set(ByVal value As DateTime)
                _APPROVED_DATE = value
            End Set
            Get
                Return (_APPROVED_DATE)
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
        Public Property EntityId() As Integer
            Set(ByVal value As Integer)
                _EntityId = value
            End Set
            Get
                Return (_EntityId)
            End Get
        End Property

        Public Property WorkLocationId() As Integer
            Set(ByVal value As Integer)
                _WorkLocationId = value
            End Set
            Get
                Return (_WorkLocationId)
            End Get
        End Property
        Public Property LogicalGroupId() As Integer
            Set(ByVal value As Integer)
                _LogicalGroupId = value
            End Set
            Get
                Return (_LogicalGroupId)
            End Get
        End Property

        Public Property Year() As Integer
            Set(ByVal value As Integer)
                _Year = value
            End Set
            Get
                Return (_Year)
            End Get
        End Property
        Public Property Month() As Integer
            Set(ByVal value As Integer)
                _Month = value
            End Set
            Get
                Return (_Month)
            End Get
        End Property

        Public Property DirectStaffOnly() As Integer
            Set(ByVal value As Integer)
                _DirectStaffOnly = value
            End Set
            Get
                Return (_DirectStaffOnly)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALApproveViolationToERP = New DALApproveViolationToERP()

        End Sub

#End Region

#Region "Methods"

        Property RecordId As String
            Get
                Return _recordId
            End Get
            Set(value As String)
                _recordId = value
            End Set
        End Property

        Property DeductionId As String
            Get
                Return _deductionId
            End Get
            Set(value As String)
                _deductionId = value
            End Set
        End Property

        Public Function Add() As Integer

            Return objDALApproveViolationToERP.Add(_FK_EmployeeId, _ViolationType, _ViolationDate, _ViolationDateNum, _IsApproved, _APPROVED_BY, _APPROVED_DATE)
        End Function

        Public Function Update() As Integer

            Return objDALApproveViolationToERP.Update(_ID, _FK_EmployeeId, _ViolationType, _ViolationDate, _ViolationDateNum, _IsApproved, _APPROVED_BY, _APPROVED_DATE)

        End Function



        Public Function Delete() As Integer

            Return objDALApproveViolationToERP.Delete(_ID)

        End Function

        Public Function GetAll() As DataTable

            Return objDALApproveViolationToERP.GetAll()

        End Function

        Public Function GetByPK() As ApproveViolationToERP

            Dim dr As DataRow
            dr = objDALApproveViolationToERP.GetByPK(_ID)

            If Not IsDBNull(dr("ID")) Then
                _ID = dr("ID")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("ViolationType")) Then
                _ViolationType = dr("ViolationType")
            End If
            If Not IsDBNull(dr("ViolationDate")) Then
                _ViolationDate = dr("ViolationDate")
            End If
            If Not IsDBNull(dr("ViolationDateNum")) Then
                _ViolationDateNum = dr("ViolationDateNum")
            End If
            If Not IsDBNull(dr("IsApproved")) Then
                _IsApproved = dr("IsApproved")
            End If
            If Not IsDBNull(dr("APPROVED_BY")) Then
                _APPROVED_BY = dr("APPROVED_BY")
            End If
            If Not IsDBNull(dr("APPROVED_DATE")) Then
                _APPROVED_DATE = dr("APPROVED_DATE")
            End If

            Return Me
        End Function

        Public Function GetAll_ByFilter() As DataTable
            Return objDALApproveViolationToERP.GetAll_ByFilter(_FK_EmployeeId, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _Year, _Month, _DirectStaffOnly)
        End Function

        Public Function GetAll_ByFilter_Header() As DataTable
            'Return objDALApproveViolationToERP.GetAll_ByFilter(_FK_EmployeeId, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _Year, _Month, _DirectStaffOnly)
            Return objDALApproveViolationToERP.GetAll_ByFilter_Header(_FK_EmployeeId, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _Year, _Month, _DirectStaffOnly)
        End Function


        Public Function GetAll_ByFilter_DOF() As DataTable
            Return objDALApproveViolationToERP.GetAll_ByFilter_DOF(_FK_EmployeeId, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _Year, _Month, _DirectStaffOnly)
        End Function

        Public Function GetAll_ByFilter_Header_DOF() As DataTable
            'Return objDALApproveViolationToERP.GetAll_ByFilter(_FK_EmployeeId, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _Year, _Month, _DirectStaffOnly)
            Return objDALApproveViolationToERP.GetAll_ByFilter_Header_DOF(_FK_EmployeeId, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _Year, _Month, _DirectStaffOnly)
        End Function
#End Region


        Public Function GetAll_ByFilter_ManaftPayroll() As DataTable
            Return objDALApproveViolationToERP.GetAll_ByFilter_ManaftPayroll(_FK_EmployeeId, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _Year, _Month)
        End Function

        Public Function GetAll_ByFilter_Header_ManaftPayroll() As DataTable
            Return objDALApproveViolationToERP.GetAll_ByFilter_Header_ManaftPayroll(_FK_EmployeeId, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _Year, _Month)
        End Function

        Public Function PayrollManafth_Approve(Delay As Boolean, EarlyOut As Boolean, Absent As Boolean, MissingIn As Boolean, MissingOut As Boolean, OutDuration As Boolean) As Integer
            Return objDALApproveViolationToERP.PayrollManafth_Approve(Delay, EarlyOut, Absent, MissingIn, MissingOut, OutDuration, RecordId)
        End Function

        Function PayrollManafth_FinalApprove(Delay As Boolean, EarlyOut As Boolean, Absent As Boolean, MissingIn As Boolean, MissingOut As Boolean, OutDuration As Boolean) As Integer
            Return objDALApproveViolationToERP.PayrollManafth_FinalApprove(DeductionId, Delay, EarlyOut, Absent, MissingIn, MissingOut, OutDuration)
        End Function

        Public Function PayrollManafth_GetFinalApproval() As DataTable
            Return objDALApproveViolationToERP.PayrollManafth_GetFinalApproval(_Year, _Month)
        End Function

        Function ManafthPayrollDeduction_Remove() As Integer
            Return objDALApproveViolationToERP.ManafthPayrollDeduction_Remove(_recordId)
        End Function

        Function ApproveViolation() As Integer
            Return objDALApproveViolationToERP.Approve(_ID, _IsApproved, _APPROVED_BY)
        End Function

        Function GetEmployeeLeaveBalance_DOF() As DataTable
            Return objDALApproveViolationToERP.GetEmployeeLeaveBalance_DOF()
        End Function

        Function ProcessViolation_DOF() As Integer
            Return objDALApproveViolationToERP.ProcessViolation_DOF(_FK_EmployeeId, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId, _Year, _Month, _DirectStaffOnly)
        End Function

    End Class
End Namespace