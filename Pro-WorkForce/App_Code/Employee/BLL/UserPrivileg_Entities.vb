Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Employees

    Public Class UserPrivileg_Entities

#Region "Class Variables"


        Private _Id As Integer
        Private _FK_EmployeeId As Long
        Private _FK_CompanyId As Integer
        Private _FK_EntityId As Integer
        Private _IsCoordinator As Boolean
        Private _CoordinatorType As Integer
        Private objDALUserPrivileg_Entities As DALUserPrivileg_Entities

#End Region

#Region "Public Properties"

        Public Property Id() As Integer
            Set(ByVal value As Integer)
                _Id = value
            End Set
            Get
                Return (_Id)
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

        Public Property FK_CompanyId() As Integer
            Set(ByVal value As Integer)
                _FK_CompanyId = value
            End Set
            Get
                Return (_FK_CompanyId)
            End Get
        End Property

        Public Property FK_EntityId() As Integer
            Set(ByVal value As Integer)
                _FK_EntityId = value
            End Set
            Get
                Return (_FK_EntityId)
            End Get
        End Property

        Public Property IsCoordinator() As Boolean
            Set(ByVal value As Boolean)
                _IsCoordinator = value
            End Set
            Get
                Return (_IsCoordinator)
            End Get
        End Property

        Public Property CoordinatorType() As Integer
            Set(ByVal value As Integer)
                _CoordinatorType = value
            End Set
            Get
                Return (_CoordinatorType)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALUserPrivileg_Entities = New DALUserPrivileg_Entities()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALUserPrivileg_Entities.Add(_Id, _FK_EmployeeId, _FK_CompanyId, _FK_EntityId, _IsCoordinator, _CoordinatorType)
            App_EventsLog.Insert_ToEventLog("Add", _Id, "UserPrivileg_Entities", "User Privilege")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALUserPrivileg_Entities.Update(_Id, _FK_EmployeeId, _FK_CompanyId, _FK_EntityId, _IsCoordinator, _CoordinatorType)
            App_EventsLog.Insert_ToEventLog("Edit", _Id, "UserPrivileg_Entities", "User Privilege")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALUserPrivileg_Entities.Delete(_Id)
            App_EventsLog.Insert_ToEventLog("Delete", _Id, "UserPrivileg_Entities", "User Privilege")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALUserPrivileg_Entities.GetAll()

        End Function

        Public Function GetByPK() As UserPrivileg_Entities

            Dim dr As DataRow
            dr = objDALUserPrivileg_Entities.GetByPK(_Id)

            If Not IsDBNull(dr("Id")) Then
                _Id = dr("Id")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("FK_CompanyId")) Then
                _FK_CompanyId = dr("FK_CompanyId")
            End If
            If Not IsDBNull(dr("FK_EntityId")) Then
                _FK_EntityId = dr("FK_EntityId")
            End If
            If Not IsDBNull(dr("IsCoordinator")) Then
                _IsCoordinator = dr("IsCoordinator")
            End If
            If Not IsDBNull(dr("CoordinatorType")) Then
                _CoordinatorType = dr("CoordinatorType")
            End If

            Return Me
        End Function

        Public Function GetManagerEntity() As DataTable

            Return objDALUserPrivileg_Entities.GetManagerEntity()

        End Function

        Public Function GetByEmpId() As UserPrivileg_Entities

            Dim dr As DataRow
            dr = objDALUserPrivileg_Entities.GetByEmpId(_FK_EmployeeId)

            If Not dr Is Nothing Then
                If Not IsDBNull(dr("Id")) Then
                    _Id = dr("Id")
                End If
                If Not IsDBNull(dr("FK_EmployeeId")) Then
                    _FK_EmployeeId = dr("FK_EmployeeId")
                End If
                If Not IsDBNull(dr("FK_CompanyId")) Then
                    _FK_CompanyId = dr("FK_CompanyId")
                End If
                If Not IsDBNull(dr("FK_EntityId")) Then
                    _FK_EntityId = dr("FK_EntityId")
                End If
                Return Me
            Else
                Return Nothing
            End If

        End Function

#End Region

    End Class
End Namespace