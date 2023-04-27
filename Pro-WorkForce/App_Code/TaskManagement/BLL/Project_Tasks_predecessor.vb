Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.TaskManagement

    Public Class Project_Tasks_predecessor

#Region "Class Variables"


        Private _FK_TaskId As Long
        Private _FK_predecessorTask As Long
        Private _RelationType As String
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALProject_Tasks_predecessor As DALProject_Tasks_predecessor

#End Region

#Region "Public Properties"
        Public Property FK_TaskId() As Long
            Set(ByVal value As Long)
                _FK_TaskId = value
            End Set
            Get
                Return (_FK_TaskId)
            End Get
        End Property
        Public Property FK_predecessorTask() As Long
            Set(ByVal value As Long)
                _FK_predecessorTask = value
            End Set
            Get
                Return (_FK_predecessorTask)
            End Get
        End Property
        Public Property RelationType() As String
            Set(ByVal value As String)
                _RelationType = value
            End Set
            Get
                Return (_RelationType)
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

#End Region


#Region "Constructor"
        Public Sub New()

            objDALProject_Tasks_predecessor = New DALProject_Tasks_predecessor()

        End Sub

#End Region

#Region "Methods"
        Public Function Add() As Integer

            Dim rslt As Integer = objDALProject_Tasks_predecessor.Add(_FK_TaskId, _FK_predecessorTask, _RelationType, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE)
            App_EventsLog.Insert_ToEventLog("Add", _FK_TaskId, "Project_Tasks_predecessor", "Project Tasks")
            Return rslt
        End Function
        Public Function Update() As Integer

            Dim rslt As Integer = objDALProject_Tasks_predecessor.Update(_FK_TaskId, _FK_predecessorTask, _RelationType, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE)
            App_EventsLog.Insert_ToEventLog("Update", _FK_TaskId, "Project_Tasks_predecessor", "Project Tasks")
            Return rslt
        End Function
        Public Function Delete() As Integer

            Dim rslt As Integer = objDALProject_Tasks_predecessor.Delete(_FK_TaskId, _FK_predecessorTask, _RelationType)
            App_EventsLog.Insert_ToEventLog("Delete", _FK_TaskId, "Project_Tasks_predecessor", "Project Tasks")
            Return rslt
        End Function
        Public Function GetAll() As DataTable

            Return objDALProject_Tasks_predecessor.GetAll()

        End Function
        Public Function Get_By_FK_TaskId() As DataTable

            Return objDALProject_Tasks_predecessor.Get_By_FK_TaskId(_FK_TaskId)

        End Function
        Public Function GetByPK() As Project_Tasks_predecessor

            Dim dr As DataRow
            dr = objDALProject_Tasks_predecessor.GetByPK(_FK_TaskId, _FK_predecessorTask, _RelationType)

            If Not IsDBNull(dr("FK_TaskId")) Then
                _FK_TaskId = dr("FK_TaskId")
            End If
            If Not IsDBNull(dr("FK_predecessorTask")) Then
                _FK_predecessorTask = dr("FK_predecessorTask")
            End If
            If Not IsDBNull(dr("RelationType")) Then
                _RelationType = dr("RelationType")
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
        End Function

#End Region

    End Class
End Namespace