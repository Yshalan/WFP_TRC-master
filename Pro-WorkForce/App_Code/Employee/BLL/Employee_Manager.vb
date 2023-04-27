Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Employees

    Public Class Employee_Manager

#Region "Class Variables"


        Private _EmpManagerId As Long
        Private _FK_EmployeeId As Integer
        Private _CompanyId As Integer
        Private _EntityId As Integer
        Private _AssignDate As DateTime
        Private _FK_ManagerId As Integer
        Private _FromDate As DateTime
        Private _ToDate As DateTime
        Private _IsTemporary As Boolean
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALEmployee_Manager As DALEmployee_Manager
        Private _IsManager As Boolean

#End Region

#Region "Public Properties"


        Public Property EmpManagerId() As Long
            Set(ByVal value As Long)
                _EmpManagerId = value
            End Set
            Get
                Return (_EmpManagerId)
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

        Public Property CompanyId() As Integer
            Set(ByVal value As Integer)
                _CompanyId = value
            End Set
            Get
                Return (_CompanyId)
            End Get
        End Property

        Public Property AssignDate() As DateTime
            Set(ByVal value As DateTime)
                _AssignDate = value
            End Set
            Get
                Return (_AssignDate)
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


        Public Property FK_ManagerId() As Integer
            Set(ByVal value As Integer)
                _FK_ManagerId = value
            End Set
            Get
                Return (_FK_ManagerId)
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


        Public Property IsTemporary() As Boolean
            Set(ByVal value As Boolean)
                _IsTemporary = value
            End Set
            Get
                Return (_IsTemporary)
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

        Public Property IsManager() As Boolean
            Set(ByVal value As Boolean)
                _IsManager = value
            End Set
            Get
                Return (_IsManager)
            End Get
        End Property
#End Region

#Region "Constructor"

        Public Sub New()

            objDALEmployee_Manager = New DALEmployee_Manager()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALEmployee_Manager.Add(_EmpManagerId, _FK_EmployeeId, _FK_ManagerId, _FromDate, _ToDate, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE)
            App_EventsLog.Insert_ToEventLog("Add", _EmpManagerId, "Employee_Manager", "Assign Manager")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALEmployee_Manager.Update(_EmpManagerId, _FK_EmployeeId, _FK_ManagerId, _FromDate, _ToDate, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE)
            App_EventsLog.Insert_ToEventLog("Edit", _EmpManagerId, "Employee_Manager", "Assign Manager")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALEmployee_Manager.Delete(_EmpManagerId)
            App_EventsLog.Insert_ToEventLog("Delete", _EmpManagerId, "Employee_Manager", "Assign Manager")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALEmployee_Manager.GetAll()

        End Function

        Public Function AssignManager() As Integer

            Dim rslt As Integer = objDALEmployee_Manager.AssignManager(_FK_EmployeeId, _FK_ManagerId, _FromDate, _ToDate, _IsTemporary)
            App_EventsLog.Insert_ToEventLog("Add", _FK_ManagerId, "Employee_Manager", "Assign Manager")
            Return rslt
        End Function

        Public Function Assign_EmployeesManager() As Integer

            Dim rslt As Integer = objDALEmployee_Manager.Assign_EmployeesManager(_FK_EmployeeId, _FK_ManagerId, _FromDate, _ToDate, _IsTemporary)
            App_EventsLog.Insert_ToEventLog("Add", _FK_ManagerId, "Employee_Manager", "Assign Employees Manager")
            Return rslt
        End Function

        Public Function GetActiveManagerByEmpId(ByVal AssignDate As DateTime) As DataTable
            Return objDALEmployee_Manager.GetActiveManagerByEmpId(_FK_EmployeeId, AssignDate)
        End Function

        Public Function GetActiveManagerByCompanyandEntity() As DataTable
            Return objDALEmployee_Manager.GetActiveManagerByCompanyandEntity(_CompanyId, _EntityId, _AssignDate)
        End Function

        Public Function GetByPK() As Employee_Manager

            Dim dr As DataRow
            dr = objDALEmployee_Manager.GetByPK(_EmpManagerId)

            If Not dr Is Nothing Then
                If Not IsDBNull(dr("EmpManagerId")) Then
                    _EmpManagerId = dr("EmpManagerId")
                End If
                If Not IsDBNull(dr("FK_EmployeeId")) Then
                    _FK_EmployeeId = dr("FK_EmployeeId")
                End If
                If Not IsDBNull(dr("FK_ManagerId")) Then
                    _FK_ManagerId = dr("FK_ManagerId")
                End If
                If Not IsDBNull(dr("FromDate")) Then
                    _FromDate = dr("FromDate")
                End If
                If Not IsDBNull(dr("ToDate")) Then
                    _ToDate = dr("ToDate")
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
            Else
                Return Nothing
            End If
        End Function

        Public Function GetEmployeeManagerByManagerID() As Employee_Manager

            Dim dr As DataRow
            dr = objDALEmployee_Manager.GetEmployeeManagerByManagerID(_FK_ManagerId)

            If Not dr Is Nothing Then
                If Not IsDBNull(dr("EmpManagerId")) Then
                    _EmpManagerId = dr("EmpManagerId")
                End If
                If Not IsDBNull(dr("FK_EmployeeId")) Then
                    _FK_EmployeeId = dr("FK_EmployeeId")
                End If
                If Not IsDBNull(dr("FK_ManagerId")) Then
                    _FK_ManagerId = dr("FK_ManagerId")
                End If
                If Not IsDBNull(dr("FromDate")) Then
                    _FromDate = dr("FromDate")
                End If
                If Not IsDBNull(dr("ToDate")) Then
                    _ToDate = dr("ToDate")
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
            Else
                Return Nothing
            End If
        End Function

        Public Function GetManagerInfoByManagerId() As DataTable

            Return objDALEmployee_Manager.GetManagerInfoByManagerId(_FK_ManagerId, _FromDate, _ToDate)

        End Function

        Public Function GetManagerNotifications(ByVal EmpIDs As String) As DataTable

            Return objDALEmployee_Manager.GetManagerNotifications(EmpIDs)

        End Function

        Public Function IsManager_Employee() As Employee_Manager

            Dim dr As DataRow
            dr = objDALEmployee_Manager.IsManager_Employee(_FK_ManagerId)

            If Not dr Is Nothing Then
                If Not IsDBNull(dr("IsManager")) Then
                    _IsManager = dr("IsManager")
                End If
                Return Me
            Else
                Return Nothing
            End If
        End Function
        Public Function CheckIsManager() As Boolean

            Dim objColl As DataTable
            objColl = objDALEmployee_Manager.IsManager(_FK_ManagerId)
            Return objColl.Rows(0)(0)

            'If Not objColl Is Nothing Or objColl.Rows.Count > 0 Then

            '    Return True
            'Else
            '    Return False
            'End If
        End Function
#End Region

    End Class
End Namespace