Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.ScheduleGroups

    Public Class ScheduleGroups_Employees

#Region "Class Variables"


        Private _GroupEmployeeId As Integer
        Private _FK_GroupId As Integer
        Private _FK_EmployeeId As Integer
        Private _FromDate As DateTime
        Private _IsTemp As Boolean
        Private _ToDate As DateTime
        Private _CREATED_BY As String
        Private _FK_EntityId As Integer
        Private _UserID As Integer
        Private objDALScheduleGroups_Employees As DALScheduleGroups_Employees

#End Region

#Region "Public Properties"

        Public Property GroupEmployeeId() As Integer
            Set(ByVal value As Integer)
                _GroupEmployeeId = value
            End Set
            Get
                Return (_GroupEmployeeId)
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

        Public Property FK_EmployeeId() As Integer
            Set(ByVal value As Integer)
                _FK_EmployeeId = value
            End Set
            Get
                Return (_FK_EmployeeId)
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

        Public Property IsTemp() As Boolean
            Set(ByVal value As Boolean)
                _IsTemp = value
            End Set
            Get
                Return (_IsTemp)
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

        Public Property CREATED_BY() As String
            Set(ByVal value As String)
                _CREATED_BY = value
            End Set
            Get
                Return (_CREATED_BY)
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

        Public Property UserID() As Integer
            Set(ByVal value As Integer)
                _UserID = value
            End Set
            Get
                Return (_UserID)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALScheduleGroups_Employees = New DALScheduleGroups_Employees()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer
            Dim rslt As Integer = objDALScheduleGroups_Employees.Add(_FK_GroupId, _FK_EmployeeId, _FromDate, _IsTemp, _ToDate, _CREATED_BY)
            App_EventsLog.Insert_ToEventLog("Add", _FK_GroupId, "ScheduleGroups_Employees", "Assign Employee Schedule Groups")
            Return rslt
        End Function

        Public Function Update() As Integer
            Dim rslt As Integer = objDALScheduleGroups_Employees.Update(_GroupEmployeeId, _FK_GroupId, _FK_EmployeeId, _FromDate, _IsTemp, _ToDate)
            App_EventsLog.Insert_ToEventLog("Update", _GroupEmployeeId, "ScheduleGroups_Employees", "Assign Employee Schedule Groups")
            Return rslt
        End Function

        Public Function Delete() As Integer
            Dim rslt As Integer = objDALScheduleGroups_Employees.Delete(_GroupEmployeeId)
            App_EventsLog.Insert_ToEventLog("Delete", _GroupEmployeeId, "ScheduleGroups_Employees", "Assign Employee Schedule Groups")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALScheduleGroups_Employees.GetAll()

        End Function

        Public Function GetByPK() As ScheduleGroups_Employees

            Dim dr As DataRow
            dr = objDALScheduleGroups_Employees.GetByPK(_GroupEmployeeId)

            If Not IsDBNull(dr("GroupEmployeeId")) Then
                _GroupEmployeeId = dr("GroupEmployeeId")
            End If
            If Not IsDBNull(dr("FK_GroupId")) Then
                _FK_GroupId = dr("FK_GroupId")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("FromDate")) Then
                _FromDate = dr("FromDate")
            End If
            If Not IsDBNull(dr("IsTemp")) Then
                _IsTemp = dr("IsTemp")
            End If
            If Not IsDBNull(dr("ToDate")) Then
                _ToDate = dr("ToDate")
            End If
            Return Me
        End Function

        Public Function GetAll_ByFk_EntityId() As DataTable

            Return objDALScheduleGroups_Employees.GetAll_ByFk_EntityId(_FK_EntityId, _UserID)

        End Function

        Public Function GetAll_ByFK_GroupId() As DataTable

            Return objDALScheduleGroups_Employees.GetAll_ByFK_GroupId(_FK_GroupId)

        End Function

#End Region

    End Class
End Namespace