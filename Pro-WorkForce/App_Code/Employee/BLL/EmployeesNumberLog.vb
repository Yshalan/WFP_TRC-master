Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Employees

    Public Class EmployeesNumberLog

#Region "Class Variables"


        Private _EmpNumberlogId As Long
        Private _FK_EmployeeId As Long
        Private _FK_CompanyId As Integer
        Private _OldEmpNo As String
        Private _NewEmpNo As String
        Private _Reason As String
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private _FromDate As DateTime?
        Private _ToDate As DateTime?
        Private objDALEmployeesNumberLog As DALEmployeesNumberLog

#End Region

#Region "Public Properties"

        Public Property EmpNumberlogId() As Long
            Set(ByVal value As Long)
                _EmpNumberlogId = value
            End Set
            Get
                Return (_EmpNumberlogId)
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

        Public Property FK_CompanyId() As Long
            Set(ByVal value As Long)
                _FK_CompanyId = value
            End Set
            Get
                Return (_FK_CompanyId)
            End Get
        End Property

        Public Property OldEmpNo() As String
            Set(ByVal value As String)
                _OldEmpNo = value
            End Set
            Get
                Return (_OldEmpNo)
            End Get
        End Property

        Public Property NewEmpNo() As String
            Set(ByVal value As String)
                _NewEmpNo = value
            End Set
            Get
                Return (_NewEmpNo)
            End Get
        End Property

        Public Property Reason() As String
            Set(ByVal value As String)
                _Reason = value
            End Set
            Get
                Return (_Reason)
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

        Public Property FromDate() As DateTime?
            Set(ByVal value As DateTime?)
                _FromDate = value
            End Set
            Get
                Return (_FromDate)
            End Get
        End Property

        Public Property ToDate() As DateTime?
            Set(ByVal value As DateTime?)
                _ToDate = value
            End Set
            Get
                Return (_ToDate)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALEmployeesNumberLog = New DALEmployeesNumberLog()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALEmployeesNumberLog.Add(_EmpNumberlogId, _FK_EmployeeId, _FK_CompanyId, _OldEmpNo, _NewEmpNo, _Reason, _CREATED_BY)
            App_EventsLog.Insert_ToEventLog("Add", _EmpNumberlogId, "EmployeesNumberLog", "Change Employee Number")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALEmployeesNumberLog.Update(_EmpNumberlogId, _FK_EmployeeId, _OldEmpNo, _NewEmpNo, _Reason, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE)
            App_EventsLog.Insert_ToEventLog("Update", _EmpNumberlogId, "EmployeesNumberLog", "Change Employee Number")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALEmployeesNumberLog.Delete(_EmpNumberlogId)
            App_EventsLog.Insert_ToEventLog("Delete", _EmpNumberlogId, "EmployeesNumberLog", "Change Employee Number")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALEmployeesNumberLog.GetAll()

        End Function

        Public Function GetAll_Inner() As DataTable

            Return objDALEmployeesNumberLog.GetAll_Inner(_FromDate, _ToDate)

        End Function

        Public Function GetByPK() As EmployeesNumberLog

            Dim dr As DataRow
            dr = objDALEmployeesNumberLog.GetByPK(_EmpNumberlogId)

            If Not IsDBNull(dr("EmpNumberlogId")) Then
                _EmpNumberlogId = dr("EmpNumberlogId")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("FK_CompanyId")) Then
                _FK_CompanyId = dr("FK_CompanyId")
            End If
            If Not IsDBNull(dr("OldEmpNo")) Then
                _OldEmpNo = dr("OldEmpNo")
            End If
            If Not IsDBNull(dr("NewEmpNo")) Then
                _NewEmpNo = dr("NewEmpNo")
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

#End Region

    End Class
End Namespace