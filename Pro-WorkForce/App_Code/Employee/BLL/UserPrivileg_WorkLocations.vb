Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Employees

    Public Class UserPrivileg_WorkLocations

#Region "Class Variables"


        Private _Id As Integer
        Private _FK_EmployeeId As Long
        Private _FK_CompanyId As Integer
        Private _FK_WorkLocationId As Integer
        Private objDALUserPrivileg_WorkLocations As DALUserPrivileg_WorkLocations

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


        Public Property FK_WorkLocationId() As Integer
            Set(ByVal value As Integer)
                _FK_WorkLocationId = value
            End Set
            Get
                Return (_FK_WorkLocationId)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALUserPrivileg_WorkLocations = New DALUserPrivileg_WorkLocations()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALUserPrivileg_WorkLocations.Add(_Id, _FK_EmployeeId, _FK_CompanyId, _FK_WorkLocationId)
            App_EventsLog.Insert_ToEventLog("Add", _Id, "UserPrivileg_WorkLocations", "User Privilege")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALUserPrivileg_WorkLocations.Update(_Id, _FK_EmployeeId, _FK_CompanyId, _FK_WorkLocationId)
            App_EventsLog.Insert_ToEventLog("Edit", _Id, "UserPrivileg_WorkLocations", "User Privilege")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALUserPrivileg_WorkLocations.Delete(_Id)
            App_EventsLog.Insert_ToEventLog("Delete", _Id, "UserPrivileg_WorkLocations", "User Privilege")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALUserPrivileg_WorkLocations.GetAll()

        End Function

        Public Function GetByPK() As UserPrivileg_WorkLocations

            Dim dr As DataRow
            dr = objDALUserPrivileg_WorkLocations.GetByPK(_Id)

            If Not IsDBNull(dr("Id")) Then
                _Id = dr("Id")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("FK_CompanyId")) Then
                _FK_CompanyId = dr("FK_CompanyId")
            End If
            If Not IsDBNull(dr("FK_WorkLocationId")) Then
                _FK_WorkLocationId = dr("FK_WorkLocationId")
            End If
            Return Me
        End Function

        Public Function GetManagerWorkLocation() As DataTable

            Return objDALUserPrivileg_WorkLocations.GetManagerWorkLocation()

        End Function
#End Region

    End Class
End Namespace