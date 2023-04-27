Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Definitions

    Public Class EmpMaritalStatus

#Region "Class Variables"


        Private _MaritalStatusId As Integer
        Private _MaritalStatusCode As String
        Private _MatitalStatusName As String
        Private _MaritalStatusArabicName As String
        Private objDALEmp_MaritalStatus As DALEmpMaritalStatus

#End Region

#Region "Public Properties"


        Public Property MaritalStatusId() As Integer
            Set(ByVal value As Integer)
                _MaritalStatusId = value
            End Set
            Get
                Return (_MaritalStatusId)
            End Get
        End Property


        Public Property MaritalStatusCode() As String
            Set(ByVal value As String)
                _MaritalStatusCode = value
            End Set
            Get
                Return (_MaritalStatusCode)
            End Get
        End Property


        Public Property MatitalStatusName() As String
            Set(ByVal value As String)
                _MatitalStatusName = value
            End Set
            Get
                Return (_MatitalStatusName)
            End Get
        End Property


        Public Property MaritalStatusArabicName() As String
            Set(ByVal value As String)
                _MaritalStatusArabicName = value
            End Set
            Get
                Return (_MaritalStatusArabicName)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALEmp_MaritalStatus = New DALEmpMaritalStatus()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALEmp_MaritalStatus.Add(_MaritalStatusId, _MaritalStatusCode, _MatitalStatusName, _MaritalStatusArabicName)
            App_EventsLog.Insert_ToEventLog("Add", _MaritalStatusId, "Emp_MaritalStatus", "Define Marital Status")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALEmp_MaritalStatus.Update(_MaritalStatusId, _MaritalStatusCode, _MatitalStatusName, _MaritalStatusArabicName)
            App_EventsLog.Insert_ToEventLog("Edit", _MaritalStatusId, "Emp_MaritalStatus", "Define Marital Status")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALEmp_MaritalStatus.Delete(_MaritalStatusId)
            App_EventsLog.Insert_ToEventLog("Delete", _MaritalStatusId, "Emp_MaritalStatus", "Define Marital Status")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_MaritalStatus.GetAll()

        End Function

        Public Function GetByPK() As EmpMaritalStatus
            Dim dr As DataRow
            dr = objDALEmp_MaritalStatus.GetByPK(_MaritalStatusId)

            If dr Is Nothing Then Return Nothing

            If Not IsDBNull(dr("MaritalStatusId")) Then
                _MaritalStatusId = dr("MaritalStatusId")
            End If
            If Not IsDBNull(dr("MaritalStatusCode")) Then
                _MaritalStatusCode = dr("MaritalStatusCode")
            End If
            If Not IsDBNull(dr("MatitalStatusName")) Then
                _MatitalStatusName = dr("MatitalStatusName")
            End If
            If Not IsDBNull(dr("MaritalStatusArabicName")) Then
                _MaritalStatusArabicName = dr("MaritalStatusArabicName")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace