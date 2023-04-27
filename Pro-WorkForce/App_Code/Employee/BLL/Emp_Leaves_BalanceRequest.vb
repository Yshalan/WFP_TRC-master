Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Employees

    Public Class Emp_Leaves_BalanceRequest

#Region "Class Variables"

        Private _RequestId As Integer
        Private _FK_LeaveTypeId As Integer
        Private _EffictiveDate As DateTime
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _RequestStatus As Integer
        Private _LeaveTypeNewBalance As Decimal
        Private _LeaveTypeOldBalance As Decimal
        Private objDALEmp_Leaves_BalanceRequest As DALEmp_Leaves_BalanceRequest

#End Region

#Region "Public Properties"

        Public Property RequestId() As Integer
            Set(ByVal value As Integer)
                _RequestId = value
            End Set
            Get
                Return (_RequestId)
            End Get
        End Property

        Public Property FK_LeaveTypeId() As Integer
            Set(ByVal value As Integer)
                _FK_LeaveTypeId = value
            End Set
            Get
                Return (_FK_LeaveTypeId)
            End Get
        End Property

        Public Property EffictiveDate() As DateTime
            Set(ByVal value As DateTime)
                _EffictiveDate = value
            End Set
            Get
                Return (_EffictiveDate)
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

        Public Property RequestStatus() As Integer
            Set(ByVal value As Integer)
                _RequestStatus = value
            End Set
            Get
                Return (_RequestStatus)
            End Get
        End Property

        Public Property LeaveTypeNewBalance() As Decimal
            Set(ByVal value As Decimal)
                _LeaveTypeNewBalance = value
            End Set
            Get
                Return (_LeaveTypeNewBalance)
            End Get
        End Property

        Public Property LeaveTypeOldBalance() As Decimal
            Set(ByVal value As Decimal)
                _LeaveTypeOldBalance = value
            End Set
            Get
                Return (_LeaveTypeOldBalance)
            End Get
        End Property

#End Region

#Region "Constructor"

        Public Sub New()

            objDALEmp_Leaves_BalanceRequest = New DALEmp_Leaves_BalanceRequest()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALEmp_Leaves_BalanceRequest.Add(_FK_LeaveTypeId, _EffictiveDate, _CREATED_BY, _RequestStatus, _LeaveTypeNewBalance, _LeaveTypeoldBalance)
        End Function

        Public Function Update() As Integer

            Return objDALEmp_Leaves_BalanceRequest.Update(_RequestId, _FK_LeaveTypeId, _EffictiveDate, _CREATED_BY, _CREATED_DATE, _RequestStatus)

        End Function

        Public Function Delete() As Integer

            Return objDALEmp_Leaves_BalanceRequest.Delete(_RequestId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_Leaves_BalanceRequest.GetAll()

        End Function

        Public Function GetByPK() As Emp_Leaves_BalanceRequest

            Dim dr As DataRow
            dr = objDALEmp_Leaves_BalanceRequest.GetByPK(_RequestId)

            If Not IsDBNull(dr("RequestId")) Then
                _RequestId = dr("RequestId")
            End If
            If Not IsDBNull(dr("FK_LeaveTypeId")) Then
                _FK_LeaveTypeId = dr("FK_LeaveTypeId")
            End If
            If Not IsDBNull(dr("EffictiveDate")) Then
                _EffictiveDate = dr("EffictiveDate")
            End If
            If Not IsDBNull(dr("CREATED_BY")) Then
                _CREATED_BY = dr("CREATED_BY")
            End If
            If Not IsDBNull(dr("CREATED_DATE")) Then
                _CREATED_DATE = dr("CREATED_DATE")
            End If
            If Not IsDBNull(dr("RequestStatus")) Then
                _RequestStatus = dr("RequestStatus")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace