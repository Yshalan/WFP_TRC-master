Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.TaskManagement

    Public Class Project_Resources

#Region "Class Variables"


        Private _FK_ProjectId As Long
        Private _FK_EmployeeId As Long
        Private _FK_DesignationId As Integer
        Private _IsPM As Boolean
        Private _RoleId As Integer
        Private objDALProject_Resources As DALProject_Resources

#End Region

#Region "Public Properties"


        Public Property FK_ProjectId() As Long
            Set(ByVal value As Long)
                _FK_ProjectId = value
            End Set
            Get
                Return (_FK_ProjectId)
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


        Public Property FK_DesignationId() As Integer
            Set(ByVal value As Integer)
                _FK_DesignationId = value
            End Set
            Get
                Return (_FK_DesignationId)
            End Get
        End Property


        Public Property IsPM() As Boolean
            Set(ByVal value As Boolean)
                _IsPM = value
            End Set
            Get
                Return (_IsPM)
            End Get
        End Property


        Public Property RoleId() As Integer
            Set(ByVal value As Integer)
                _RoleId = value
            End Set
            Get
                Return (_RoleId)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALProject_Resources = New DALProject_Resources()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALProject_Resources.Add(_FK_ProjectId, _FK_EmployeeId, _FK_DesignationId, _IsPM, _RoleId)
            App_EventsLog.Insert_ToEventLog("Add", _FK_ProjectId, "Project_Resources", "Define Projects")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALProject_Resources.Update(_FK_ProjectId, _FK_EmployeeId, _FK_DesignationId, _IsPM, _RoleId)
            App_EventsLog.Insert_ToEventLog("Update", _FK_ProjectId, "Project_Resources", "Define Projects")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALProject_Resources.Delete(_FK_ProjectId, _FK_EmployeeId)
            App_EventsLog.Insert_ToEventLog("Delete", _FK_ProjectId, "Project_Resources", "Define Projects")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALProject_Resources.GetAll()

        End Function

        Public Function GetByPK() As Project_Resources

            Dim dr As DataRow
            dr = objDALProject_Resources.GetByPK(_FK_ProjectId, _FK_EmployeeId)

            If Not IsDBNull(dr("FK_ProjectId")) Then
                _FK_ProjectId = dr("FK_ProjectId")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("FK_DesignationId")) Then
                _FK_DesignationId = dr("FK_DesignationId")
            End If
            If Not IsDBNull(dr("IsPM")) Then
                _IsPM = dr("IsPM")
            End If
            If Not IsDBNull(dr("RoleId")) Then
                _RoleId = dr("RoleId")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace