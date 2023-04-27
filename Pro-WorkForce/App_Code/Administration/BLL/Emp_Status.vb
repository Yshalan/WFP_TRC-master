Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Admin

    Public Class Emp_Status

#Region "Class Variables"


        Private _StatusId As Integer
        Private _statusCode As String
        Private _StatusName As String
        Private _StatusArabicName As String
        Private _StatusDescription As String
        Private _CosiderEmployeeActive As Boolean
        Private objDALEmp_Status As DALEmp_Status

#End Region

#Region "Public Properties"


        Public Property StatusId() As Integer
            Set(ByVal value As Integer)
                _StatusId = value
            End Set
            Get
                Return (_StatusId)
            End Get
        End Property


        Public Property statusCode() As String
            Set(ByVal value As String)
                _statusCode = value
            End Set
            Get
                Return (_statusCode)
            End Get
        End Property


        Public Property StatusName() As String
            Set(ByVal value As String)
                _StatusName = value
            End Set
            Get
                Return (_StatusName)
            End Get
        End Property


        Public Property StatusArabicName() As String
            Set(ByVal value As String)
                _StatusArabicName = value
            End Set
            Get
                Return (_StatusArabicName)
            End Get
        End Property


        Public Property StatusDescription() As String
            Set(ByVal value As String)
                _StatusDescription = value
            End Set
            Get
                Return (_StatusDescription)
            End Get
        End Property


        Public Property CosiderEmployeeActive() As Boolean
            Set(ByVal value As Boolean)
                _CosiderEmployeeActive = value
            End Set
            Get
                Return (_CosiderEmployeeActive)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALEmp_Status = New DALEmp_Status()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer
            Dim rslt As Integer = objDALEmp_Status.Add(_StatusId, _statusCode, _StatusName, _StatusArabicName, _StatusDescription, _CosiderEmployeeActive)
            App_EventsLog.Insert_ToEventLog("Add", _StatusId, "Emp_Status", "Employee Status")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALEmp_Status.Update(_StatusId, _statusCode, _StatusName, _StatusArabicName, _StatusDescription, _CosiderEmployeeActive)
            App_EventsLog.Insert_ToEventLog("Edit", _StatusId, "Emp_Status", "Employee Status")
            Return rslt
        End Function



        Public Function Delete() As Integer
            Dim rslt As Integer = objDALEmp_Status.Delete(_StatusId)
            App_EventsLog.Insert_ToEventLog("Delete", _StatusId, "Emp_Status", "Employee Status")
            Return rslt

        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_Status.GetAll()

        End Function

        Public Function GetByPK() As Emp_Status

            Dim dr As DataRow
            dr = objDALEmp_Status.GetByPK(_StatusId)

            If dr Is Nothing Then Return Nothing

            If Not IsDBNull(dr("StatusId")) Then
                _StatusId = dr("StatusId")
            End If
            If Not IsDBNull(dr("statusCode")) Then
                _statusCode = dr("statusCode")
            End If
            If Not IsDBNull(dr("StatusName")) Then
                _StatusName = dr("StatusName")
            End If
            If Not IsDBNull(dr("StatusArabicName")) Then
                _StatusArabicName = dr("StatusArabicName")
            End If
            If Not IsDBNull(dr("StatusDescription")) Then
                _StatusDescription = dr("StatusDescription")
            End If
            If Not IsDBNull(dr("CosiderEmployeeActive")) Then
                _CosiderEmployeeActive = dr("CosiderEmployeeActive")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace