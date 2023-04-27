Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Employees

    Public Class UserPrivileg_Companies

#Region "Class Variables"


        Private _Id As Integer
        Private _FK_EmployeeId As Long
        Private _FK_CompanyId As Integer
        Private objDALUserPrivileg_Companies As DALUserPrivileg_Companies

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

#End Region

#Region "Constructor"

        Public Sub New()

            objDALUserPrivileg_Companies = New DALUserPrivileg_Companies()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALUserPrivileg_Companies.Add(_Id, _FK_EmployeeId, _FK_CompanyId)
            App_EventsLog.Insert_ToEventLog("Add", _Id, "UserPrivileg_Companies", "User Privilege")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALUserPrivileg_Companies.Update(_Id, _FK_EmployeeId, _FK_CompanyId)
            App_EventsLog.Insert_ToEventLog("Edit", _Id, "UserPrivileg_Companies", "User Privilege")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALUserPrivileg_Companies.Delete(_Id)
            App_EventsLog.Insert_ToEventLog("Delete", _Id, "UserPrivileg_Companies", "User Privilege")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALUserPrivileg_Companies.GetAll()

        End Function

        Public Function GetByPK() As UserPrivileg_Companies

            Dim dr As DataRow
            dr = objDALUserPrivileg_Companies.GetByPK(_Id)

            If Not IsDBNull(dr("Id")) Then
                _Id = dr("Id")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("FK_CompanyId")) Then
                _FK_CompanyId = dr("FK_CompanyId")
            End If
            Return Me
        End Function

        Public Function GetManagerCompanies() As DataTable

            Return objDALUserPrivileg_Companies.GetManagerCompanies()

        End Function

        Public Function GetByEmpId() As UserPrivileg_Companies
            Dim dr As DataRow
            dr = objDALUserPrivileg_Companies.GetByEmpId(_FK_EmployeeId)

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
                Return Me
            Else
                Return Nothing
            End If

        End Function

#End Region

    End Class
End Namespace