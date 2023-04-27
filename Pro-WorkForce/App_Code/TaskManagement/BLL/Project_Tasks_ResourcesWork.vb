Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.TaskManagement

    Public Class Project_Tasks_ResourcesWork

#Region "Class Variables"


        Private _ResourceWorkId As Long
        Private _FK_EmployeeId As Long
        Private _FK_TaskId As Long
        Private _StartDateTime As DateTime
        Private _EndDateTime As DateTime
        Private _completionpercentage As Double
        Private _Remarks As String
        Private objDALProject_Tasks_ResourcesWork As DALProject_Tasks_ResourcesWork

#End Region

#Region "Public Properties"
        Public Property ResourceWorkId() As Long
            Set(ByVal value As Long)
                _ResourceWorkId = value
            End Set
            Get
                Return (_ResourceWorkId)
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
        Public Property FK_TaskId() As Long
            Set(ByVal value As Long)
                _FK_TaskId = value
            End Set
            Get
                Return (_FK_TaskId)
            End Get
        End Property
        Public Property StartDateTime() As DateTime
            Set(ByVal value As DateTime)
                _StartDateTime = value
            End Set
            Get
                Return (_StartDateTime)
            End Get
        End Property
        Public Property EndDateTime() As DateTime
            Set(ByVal value As DateTime)
                _EndDateTime = value
            End Set
            Get
                Return (_EndDateTime)
            End Get
        End Property
        Public Property completionpercentage() As Double
            Set(ByVal value As Double)
                _completionpercentage = value
            End Set
            Get
                Return (_completionpercentage)
            End Get
        End Property
        Public Property Remarks() As String
            Set(ByVal value As String)
                _Remarks = value
            End Set
            Get
                Return (_Remarks)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALProject_Tasks_ResourcesWork = New DALProject_Tasks_ResourcesWork()

        End Sub

#End Region

#Region "Methods"
        Public Function Add() As Integer

            Dim rslt As Integer = objDALProject_Tasks_ResourcesWork.Add(_ResourceWorkId, _FK_EmployeeId, _FK_TaskId, _StartDateTime, _EndDateTime, _completionpercentage, _Remarks)
            App_EventsLog.Insert_ToEventLog("Add", _ResourceWorkId, "Project_Tasks_ResourcesWork", "Project Tasks")
            Return rslt
        End Function
        Public Function Update() As Integer

            Dim rslt As Integer = objDALProject_Tasks_ResourcesWork.Update(_ResourceWorkId, _FK_EmployeeId, _FK_TaskId, _StartDateTime, _EndDateTime, _completionpercentage, _Remarks)
            App_EventsLog.Insert_ToEventLog("Update", _ResourceWorkId, "Project_Tasks_ResourcesWork", "Project Tasks")
            Return rslt
        End Function
        Public Function Delete() As Integer

            Dim rslt As Integer = objDALProject_Tasks_ResourcesWork.Delete(_ResourceWorkId)
            App_EventsLog.Insert_ToEventLog("Delete", _ResourceWorkId, "Project_Tasks_ResourcesWork", "Project Tasks")
            Return rslt
        End Function
        Public Function GetAll() As DataTable

            Return objDALProject_Tasks_ResourcesWork.GetAll()

        End Function
        Public Function GetAllEmployeeTask(EmployeeId As Integer, TaskId As Integer) As DataTable

            Return objDALProject_Tasks_ResourcesWork.GetAllEmployeeTask(EmployeeId, TaskId)

        End Function
        Public Function GetByPK() As Project_Tasks_ResourcesWork

            Dim dr As DataRow
            dr = objDALProject_Tasks_ResourcesWork.GetByPK(_ResourceWorkId)

            If Not IsDBNull(dr("ResourceWorkId")) Then
                _ResourceWorkId = dr("ResourceWorkId")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("FK_TaskId")) Then
                _FK_TaskId = dr("FK_TaskId")
            End If
            If Not IsDBNull(dr("StartDateTime")) Then
                _StartDateTime = dr("StartDateTime")
            End If
            If Not IsDBNull(dr("EndDateTime")) Then
                _EndDateTime = dr("EndDateTime")
            End If
            If Not IsDBNull(dr("completionpercentage")) Then
                _completionpercentage = dr("completionpercentage")
            End If
            If Not IsDBNull(dr("Remarks")) Then
                _Remarks = dr("Remarks")
            End If
            Return Me
        End Function

#End Region
        Public Function GetForDDL(EmployeeId As Integer) As DataTable
            Return objDALProject_Tasks_ResourcesWork.GetForDDL(EmployeeId)
        End Function

    End Class
End Namespace