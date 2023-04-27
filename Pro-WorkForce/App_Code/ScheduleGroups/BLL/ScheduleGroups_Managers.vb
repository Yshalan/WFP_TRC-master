Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.ScheduleGroups

    Public Class ScheduleGroups_Managers

#Region "Class Variables"


        Private _GroupManagerId As Integer
        Private _FK_GroupId As Integer
        Private _FK_EmployeeId As Integer
        Private _FromDate As DateTime
        Private _IsTemp As Boolean
        Private _ToDate As DateTime
        Private objDALScheduleGroups_Managers As DALScheduleGroups_Managers

#End Region

#Region "Public Properties"


        Public Property GroupManagerId() As Integer
            Set(ByVal value As Integer)
                _GroupManagerId = value
            End Set
            Get
                Return (_GroupManagerId)
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

#End Region

#Region "Constructor"

        Public Sub New()

            objDALScheduleGroups_Managers = New DALScheduleGroups_Managers()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALScheduleGroups_Managers.Add(_FK_GroupId, _FK_EmployeeId, _FromDate, _IsTemp, _ToDate)
        End Function

        Public Function Update() As Integer

            Return objDALScheduleGroups_Managers.Update(_GroupManagerId, _FK_GroupId, _FK_EmployeeId, _FromDate, _IsTemp, _ToDate)

        End Function



        Public Function Delete() As Integer

            Return objDALScheduleGroups_Managers.Delete(_GroupManagerId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALScheduleGroups_Managers.GetAll()

        End Function

        Public Function GetByGroupId() As DataTable

            Return objDALScheduleGroups_Managers.GetByGroupId(_FK_GroupId)

        End Function

        Public Function GetGroup_ActiveManager() As DataTable

            Return objDALScheduleGroups_Managers.GetGroup_ActiveManager(_FK_GroupId)

        End Function

        Public Function GetByPK() As ScheduleGroups_Managers

            Dim dr As DataRow
            dr = objDALScheduleGroups_Managers.GetByPK(_GroupManagerId)

            If Not IsDBNull(dr("GroupManagerId")) Then
                _GroupManagerId = dr("GroupManagerId")
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

#End Region

    End Class
End Namespace