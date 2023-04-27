Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Forms

    Public Class Events_Employees

#Region "Class Variables"


        Private _FK_EventId As Integer
        Private _FK_GroupId As Integer
        Private _FK_EmployeeId As Long
        Private objDALEvents_Employees As DALEvents_Employees

#End Region

#Region "Public Properties"


        Public Property FK_EventId() As Integer
            Set(ByVal value As Integer)
                _FK_EventId = value
            End Set
            Get
                Return (_FK_EventId)
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

        Public Property FK_EmployeeId() As Long
            Set(ByVal value As Long)
                _FK_EmployeeId = value
            End Set
            Get
                Return (_FK_EmployeeId)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALEvents_Employees = New DALEvents_Employees()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALEvents_Employees.Add(_FK_EventId, _FK_EmployeeId)
        End Function

        Public Function Update() As Integer

            Return objDALEvents_Employees.Update(_FK_EventId, _FK_EmployeeId)

        End Function



        Public Function Delete() As Integer

            Return objDALEvents_Employees.Delete(_FK_EventId, _FK_EmployeeId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALEvents_Employees.GetAll()

        End Function
        Public Function Get_EventGroups() As DataTable

            Return objDALEvents_Employees.Get_EventGroups(_FK_EventId)

        End Function
        Public Function Get_All_Details() As DataTable

            Return objDALEvents_Employees.Get_All_Details(_FK_EventId, _FK_GroupId)

        End Function
        Public Function GetByPK() As Events_Employees

            Dim dr As DataRow
            dr = objDALEvents_Employees.GetByPK(_FK_EventId, _FK_EmployeeId)

            If Not IsDBNull(dr("FK_EventId")) Then
                _FK_EventId = dr("FK_EventId")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace