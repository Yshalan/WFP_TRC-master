Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Employees
    Public Class Emp_DeputyManager
#Region "Class Variables"

        Dim _DeuputyManagerId As Integer
        Dim _FK_CompanyId As Integer
        Dim _FK_EntityId As Integer
        Dim _FK_ManagerId As Long
        Dim _FK_DeputyManagerId As Long
        Dim _FromDate As DateTime
        Dim _ToDate As DateTime
        Dim _FK_EmployeeNo As String

        Dim objDALEmp_DeputyManager As DALEmp_DeputyManager

#End Region

#Region "Public Properties"

        Public Property DeuputyManagerID() As Integer
            Get
                Return _DeuputyManagerId
            End Get
            Set(ByVal value As Integer)
                _DeuputyManagerId = value
            End Set
        End Property

        Public Property FK_CompanyId() As Integer
            Get
                Return _FK_CompanyId
            End Get
            Set(ByVal value As Integer)
                _FK_CompanyId = value
            End Set
        End Property

        Public Property FK_EntityId() As Integer
            Get
                Return _FK_EntityId
            End Get
            Set(ByVal value As Integer)
                _FK_EntityId = value
            End Set
        End Property

        Public Property FK_ManagerId() As Long
            Get
                Return _FK_ManagerId
            End Get
            Set(ByVal value As Long)
                _FK_ManagerId = value
            End Set
        End Property

        Public Property FK_DeputyManagerId() As Long
            Get
                Return _FK_DeputyManagerId
            End Get
            Set(ByVal value As Long)
                _FK_DeputyManagerId = value
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

        Public Property FK_EmployeeNo() As String
            Get
                Return _FK_EmployeeNo
            End Get
            Set(ByVal value As String)
                _FK_EmployeeNo = value
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

#End Region

#Region "Constructor"

        Public Sub New()

            objDALEmp_DeputyManager = New DALEmp_DeputyManager()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALEmp_DeputyManager.Add(_DeuputyManagerId, _FK_CompanyId, _FK_EntityId, _FK_ManagerId, _FK_DeputyManagerId, _FromDate, _ToDate)
            App_EventsLog.Insert_ToEventLog("Add", _DeuputyManagerId, "Emp_DeputyManager", "Employee Deputy Manager")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALEmp_DeputyManager.Update(_DeuputyManagerId, _FK_CompanyId, _FK_EntityId, _FK_ManagerId, _FK_DeputyManagerId, _FromDate, _ToDate)
            App_EventsLog.Insert_ToEventLog("Edit", _DeuputyManagerId, "Emp_DeputyManager", "Employee Deputy Manager")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALEmp_DeputyManager.Delete(_DeuputyManagerId)
            App_EventsLog.Insert_ToEventLog("Delete", _DeuputyManagerId, "Emp_DeputyManager", "Employee Deputy Manager")
            Return rslt
        End Function

        Public Function GetByPK() As Emp_DeputyManager

            Dim dr As DataRow
            dr = objDALEmp_DeputyManager.GetByPK(_DeuputyManagerId)

            If Not dr Is Nothing Then
                If Not IsDBNull(dr("Id")) Then
                    _DeuputyManagerId = dr("Id")
                End If

                If Not IsDBNull(dr("FK_CompanyId")) Then
                    _FK_CompanyId = dr("FK_CompanyId")
                End If

                If Not IsDBNull(dr("FK_EntityId")) Then
                    _FK_EntityId = dr("FK_EntityId")
                End If

                If Not IsDBNull(dr("FK_ManagerId")) Then
                    _FK_ManagerId = dr("FK_ManagerId")
                End If

                If Not IsDBNull(dr("FK_DeputyManagerId")) Then
                    _FK_DeputyManagerId = dr("FK_DeputyManagerId")
                End If

                If Not IsDBNull(dr("FromDate")) Then
                    _FromDate = dr("FromDate")
                End If

                If Not IsDBNull(dr("ToDate")) Then
                    _ToDate = dr("ToDate")
                End If

                Return Me
            Else
                Return Nothing
            End If
        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_DeputyManager.GetAll()

        End Function

        Public Function GetByManagerId() As DataTable

            Return objDALEmp_DeputyManager.GetByManagerId(_FK_ManagerId)

        End Function

        Public Function GetByDeputyManagerId() As DataTable

            Return objDALEmp_DeputyManager.GetByManagerId(_FK_DeputyManagerId)

        End Function

        Public Function GetByDeputyManager() As Emp_DeputyManager
            Dim dr As DataRow
            dr = objDALEmp_DeputyManager.GetByDeputyManager(_FK_DeputyManagerId)

            If Not dr Is Nothing Then
                If Not IsDBNull(dr("Id")) Then
                    _DeuputyManagerId = dr("Id")
                End If

                If Not IsDBNull(dr("FK_CompanyId")) Then
                    _FK_CompanyId = dr("FK_CompanyId")
                End If

                If Not IsDBNull(dr("FK_EntityId")) Then
                    _FK_EntityId = dr("FK_EntityId")
                End If

                If Not IsDBNull(dr("FK_ManagerId")) Then
                    _FK_ManagerId = dr("FK_ManagerId")
                End If

                If Not IsDBNull(dr("FK_DeputyManagerId")) Then
                    _FK_DeputyManagerId = dr("FK_DeputyManagerId")
                End If

                If Not IsDBNull(dr("FromDate")) Then
                    _FromDate = dr("FromDate")
                End If

                If Not IsDBNull(dr("ToDate")) Then
                    _ToDate = dr("ToDate")
                End If

                Return Me
            Else
                Return Nothing
            End If
        End Function

#End Region

    End Class

End Namespace
