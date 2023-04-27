Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.TaskManagement

    Public Class Project_tasks

#Region "Class Variables"


        Private _TaskId As Long
        Private _FK_ParentTaskId As Long
        Private _FK_ProjectId As Long
        Private _TaskSequence As String
        Private _Priority As Integer
        Private _TaskName As String
        Private _TaskDescription As String
        Private _PlannedStartDate As DateTime
        Private _PlannedEndDate As DateTime
        Private _ActualStartDate As DateTime
        Private _ActualEndDate As DateTime
        Private _Totalcompletionpercentage As Double
        Private _IsCompleted As Boolean
        Private _Approvedcompletionpercentage As Double
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private _Lang As String
        Private objDALProject_tasks As DALProject_tasks

#End Region

#Region "Public Properties"


        Public Property TaskId() As Long
            Set(ByVal value As Long)
                _TaskId = value
            End Set
            Get
                Return (_TaskId)
            End Get
        End Property


        Public Property FK_ParentTaskId() As Long
            Set(ByVal value As Long)
                _FK_ParentTaskId = value
            End Set
            Get
                Return (_FK_ParentTaskId)
            End Get
        End Property


        Public Property FK_ProjectId() As Long
            Set(ByVal value As Long)
                _FK_ProjectId = value
            End Set
            Get
                Return (_FK_ProjectId)
            End Get
        End Property


        Public Property TaskSequence() As String
            Set(ByVal value As String)
                _TaskSequence = value
            End Set
            Get
                Return (_TaskSequence)
            End Get
        End Property


        Public Property Priority() As Integer
            Set(ByVal value As Integer)
                _Priority = value
            End Set
            Get
                Return (_Priority)
            End Get
        End Property


        Public Property TaskName() As String
            Set(ByVal value As String)
                _TaskName = value
            End Set
            Get
                Return (_TaskName)
            End Get
        End Property


        Public Property TaskDescription() As String
            Set(ByVal value As String)
                _TaskDescription = value
            End Set
            Get
                Return (_TaskDescription)
            End Get
        End Property


        Public Property PlannedStartDate() As DateTime
            Set(ByVal value As DateTime)
                _PlannedStartDate = value
            End Set
            Get
                Return (_PlannedStartDate)
            End Get
        End Property


        Public Property PlannedEndDate() As DateTime
            Set(ByVal value As DateTime)
                _PlannedEndDate = value
            End Set
            Get
                Return (_PlannedEndDate)
            End Get
        End Property


        Public Property ActualStartDate() As DateTime
            Set(ByVal value As DateTime)
                _ActualStartDate = value
            End Set
            Get
                Return (_ActualStartDate)
            End Get
        End Property


        Public Property ActualEndDate() As DateTime
            Set(ByVal value As DateTime)
                _ActualEndDate = value
            End Set
            Get
                Return (_ActualEndDate)
            End Get
        End Property


        Public Property Totalcompletionpercentage() As Double
            Set(ByVal value As Double)
                _Totalcompletionpercentage = value
            End Set
            Get
                Return (_Totalcompletionpercentage)
            End Get
        End Property


        Public Property IsCompleted() As Boolean
            Set(ByVal value As Boolean)
                _IsCompleted = value
            End Set
            Get
                Return (_IsCompleted)
            End Get
        End Property


        Public Property Approvedcompletionpercentage() As Double
            Set(ByVal value As Double)
                _Approvedcompletionpercentage = value
            End Set
            Get
                Return (_Approvedcompletionpercentage)
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

        Public Property Lang() As String
            Set(ByVal value As String)
                _Lang = value
            End Set
            Get
                Return (_Lang)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALProject_tasks = New DALProject_tasks()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALProject_tasks.Add(_TaskId, _FK_ParentTaskId, _FK_ProjectId, _TaskSequence, _Priority, _TaskName, _TaskDescription, _PlannedStartDate, _PlannedEndDate, _ActualStartDate, _ActualEndDate, _Totalcompletionpercentage, _IsCompleted, _Approvedcompletionpercentage, _CREATED_BY)
            App_EventsLog.Insert_ToEventLog("Add", _TaskId, "Project_tasks", "Project Tasks")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALProject_tasks.Update(_TaskId, _FK_ParentTaskId, _FK_ProjectId, _TaskSequence, _Priority, _TaskName, _TaskDescription, _PlannedStartDate, _PlannedEndDate, _ActualStartDate, _ActualEndDate, _Totalcompletionpercentage, _IsCompleted, _Approvedcompletionpercentage, _LAST_UPDATE_BY)
            App_EventsLog.Insert_ToEventLog("Update", _TaskId, "Project_tasks", "Project Tasks")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALProject_tasks.Delete(_TaskId)
            App_EventsLog.Insert_ToEventLog("Delete", _TaskId, "Project_tasks", "Project Tasks")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALProject_tasks.GetAll()

        End Function

        Public Function Get_Predecessors_by_FK_ProjectId() As DataTable

            Return objDALProject_tasks.Get_Predecessors_by_FK_ProjectId(_FK_ProjectId)

        End Function

        Public Function Get_by_FK_ProjectId() As DataTable

            Return objDALProject_tasks.Get_by_FK_ProjectId(_FK_ProjectId)

        End Function

        Public Function Get_by_FK_ProjectId_Gantt() As DataTable

            Return objDALProject_tasks.Get_by_FK_ProjectId_Gantt(_FK_ProjectId, _Lang)

        End Function

        Public Function Get_Calendar_Dates() As DataTable

            Return objDALProject_tasks.Get_Calendar_Dates(_FK_ProjectId)

        End Function

        Public Function GetByPK() As Project_tasks

            Dim dr As DataRow
            dr = objDALProject_tasks.GetByPK(_TaskId)

            If Not IsDBNull(dr("TaskId")) Then
                _TaskId = dr("TaskId")
            End If
            If Not IsDBNull(dr("FK_ParentTaskId")) Then
                _FK_ParentTaskId = dr("FK_ParentTaskId")
            End If
            If Not IsDBNull(dr("FK_ProjectId")) Then
                _FK_ProjectId = dr("FK_ProjectId")
            End If
            If Not IsDBNull(dr("TaskSequence")) Then
                _TaskSequence = dr("TaskSequence")
            End If
            If Not IsDBNull(dr("Priority")) Then
                _Priority = dr("Priority")
            End If
            If Not IsDBNull(dr("TaskName")) Then
                _TaskName = dr("TaskName")
            End If
            If Not IsDBNull(dr("TaskDescription")) Then
                _TaskDescription = dr("TaskDescription")
            End If
            If Not IsDBNull(dr("PlannedStartDate")) Then
                _PlannedStartDate = dr("PlannedStartDate")
            End If
            If Not IsDBNull(dr("PlannedEndDate")) Then
                _PlannedEndDate = dr("PlannedEndDate")
            End If
            If Not IsDBNull(dr("ActualStartDate")) Then
                _ActualStartDate = dr("ActualStartDate")
            End If
            If Not IsDBNull(dr("ActualEndDate")) Then
                _ActualEndDate = dr("ActualEndDate")
            End If
            If Not IsDBNull(dr("Totalcompletionpercentage")) Then
                _Totalcompletionpercentage = dr("Totalcompletionpercentage")
            End If
            If Not IsDBNull(dr("IsCompleted")) Then
                _IsCompleted = dr("IsCompleted")
            End If
            If Not IsDBNull(dr("Approvedcompletionpercentage")) Then
                _Approvedcompletionpercentage = dr("Approvedcompletionpercentage")
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

        Public Function GetByTaskId_Sequence() As DataTable

            Return objDALProject_tasks.GetByTaskId_Sequence(_TaskId, _TaskSequence)

        End Function


#End Region

    End Class
End Namespace