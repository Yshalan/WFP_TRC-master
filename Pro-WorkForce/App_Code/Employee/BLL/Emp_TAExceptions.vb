Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Employees

    Public Class Emp_TAExceptions

#Region "Class Variables"

        Private _FK_EmployeeId As Integer
        Private _FromDate As DateTime
        Private _ToDate As DateTime
        Private _Active As Boolean
        Private _Reason As String
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALEmp_TAExceptions As DALTAExceptions

#End Region

#Region "Public Properties"

        Public Property FK_EmployeeId() As Integer
            Get
                Return _FK_EmployeeId
            End Get
            Set(ByVal value As Integer)
                _FK_EmployeeId = value
            End Set
        End Property

        Public Property FromDate() As DateTime
            Get
                Return _FromDate
            End Get
            Set(ByVal value As DateTime)
                _FromDate = value
            End Set
        End Property

        Public Property ToDate() As DateTime
            Get
                Return _ToDate
            End Get
            Set(ByVal value As DateTime)
                _ToDate = value
            End Set
        End Property

        Public Property Active() As Boolean
            Get
                Return _Active
            End Get
            Set(ByVal value As Boolean)
                _Active = value
            End Set
        End Property

        Public Property Reason() As String
            Get
                Return _Reason
            End Get
            Set(ByVal value As String)
                _Reason = value
            End Set
        End Property

        Public Property CREATED_BY() As String
            Get
                Return _CREATED_BY
            End Get
            Set(ByVal value As String)
                _CREATED_BY = value
            End Set
        End Property

        Public Property CREATED_DATE() As DateTime
            Get
                Return _CREATED_DATE
            End Get
            Set(ByVal value As DateTime)
                _CREATED_DATE = value
            End Set
        End Property

        Public Property LAST_UPDATE_BY() As String
            Get
                Return _LAST_UPDATE_BY
            End Get
            Set(ByVal value As String)
                _LAST_UPDATE_BY = value
            End Set
        End Property

        Public Property LAST_UPDATE_DATE() As DateTime
            Get
                Return _LAST_UPDATE_DATE
            End Get
            Set(ByVal value As DateTime)
                _LAST_UPDATE_DATE = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Public Sub New()

            objDALEmp_TAExceptions = New DALTAExceptions()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALEmp_TAExceptions.Add(_FK_EmployeeId, _FromDate, _ToDate, _Reason, _CREATED_BY, _LAST_UPDATE_BY)
            App_EventsLog.Insert_ToEventLog("Add", _FK_EmployeeId, "Emp_TAException", "Employee TA Exception")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALEmp_TAExceptions.Update(_FK_EmployeeId, _FromDate, _ToDate, _Reason, LAST_UPDATE_BY)
            App_EventsLog.Insert_ToEventLog("Edit", _FK_EmployeeId, "Emp_TAException", "Employee TA Exception")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALEmp_TAExceptions.Delete(_FK_EmployeeId, _FromDate)
            App_EventsLog.Insert_ToEventLog("Delete", _FK_EmployeeId, "Emp_TAException", "Employee TA Exception")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_TAExceptions.GetAll()

        End Function

        Public Function GetByPK() As Emp_TAExceptions

            Dim dr As DataRow
            dr = objDALEmp_TAExceptions.GetByPK(_FK_EmployeeId, _FromDate)

            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("FromDate")) Then
                _FromDate = dr("FromDate")
            End If
            If Not IsDBNull(dr("ToDate")) Then
                _ToDate = dr("ToDate")
            Else
                _ToDate = DateTime.MinValue
            End If
            If Not IsDBNull(dr("Active")) Then
                _Active = dr("Active")
            End If
            If Not IsDBNull(dr("Reason")) Then
                _Reason = dr("Reason")
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
            Return Me

        End Function

        Public Function GetAllInnerEmployee() As DataTable

            Return objDALEmp_TAExceptions.GetAllInnerEmployee()

        End Function

#End Region

    End Class

End Namespace
