Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Employees

    Public Class UserPrivileg_LogicalGroup

#Region "Class Variables"


        Private _Id As Integer
        Private _FK_EmployeeId As Long
        Private _FK_CompanyId As Integer
        Private _FK_GroupId As Integer
        Private objDALUserPrivileg_LogicalGroup As DALUserPrivileg_LogicalGroup

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


        Public Property FK_GroupId() As Integer
            Set(ByVal value As Integer)
                _FK_GroupId = value
            End Set
            Get
                Return (_FK_GroupId)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALUserPrivileg_LogicalGroup = New DALUserPrivileg_LogicalGroup()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALUserPrivileg_LogicalGroup.Add(_Id, _FK_EmployeeId, _FK_CompanyId, _FK_GroupId)
            App_EventsLog.Insert_ToEventLog("Add", _Id, "UserPrivileg_LogicalGroup", "User Privilege")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALUserPrivileg_LogicalGroup.Update(_Id, _FK_EmployeeId, _FK_CompanyId, _FK_GroupId)
            App_EventsLog.Insert_ToEventLog("Edit", _Id, "UserPrivileg_LogicalGroup", "User Privilege")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALUserPrivileg_LogicalGroup.Delete(_Id)
            App_EventsLog.Insert_ToEventLog("Delete", _Id, "UserPrivileg_LogicalGroup", "User Privilege")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALUserPrivileg_LogicalGroup.GetAll()

        End Function

        Public Function GetByPK() As UserPrivileg_LogicalGroup

            Dim dr As DataRow
            dr = objDALUserPrivileg_LogicalGroup.GetByPK(_Id)

            If Not IsDBNull(dr("Id")) Then
                _Id = dr("Id")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("FK_CompanyId")) Then
                _FK_CompanyId = dr("FK_CompanyId")
            End If
            If Not IsDBNull(dr("FK_GroupId")) Then
                _FK_GroupId = dr("FK_GroupId")
            End If
            Return Me
        End Function
        Public Function GetManagerLogicalGroup() As DataTable

            Return objDALUserPrivileg_LogicalGroup.GetManagerLogicalGroup()

        End Function
#End Region

    End Class
End Namespace