Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.TaskManagement

    Public Class Project_Tasks_Resources

#Region "Class Variables"


        Private _FK_TaskId As Long
        Private _FK_EmployeeId As Long
        Private _Involvmentpercentage As Double
        Private _completionpercentage As Double
        Private _IsCompletedByResource As Boolean
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private _TotalSpendTimeMinutes As Integer
        Private _CompletionRemark As String
        Private objDALProject_Tasks_Resources As DALProject_Tasks_Resources

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
        Public Property FK_EmployeeId() As Long
            Set(ByVal value As Long)
                _FK_EmployeeId = value
            End Set
            Get
                Return (_FK_EmployeeId)
            End Get
        End Property
        Public Property Involvmentpercentage() As Double
            Set(ByVal value As Double)
                _Involvmentpercentage = value
            End Set
            Get
                Return (_Involvmentpercentage)
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
        Public Property IsCompletedByResource() As Boolean
            Set(ByVal value As Boolean)
                _IsCompletedByResource = value
            End Set
            Get
                Return (_IsCompletedByResource)
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
        Public Property TotalSpendTimeMinutes() As Integer
            Set(ByVal value As Integer)
                _TotalSpendTimeMinutes = value
            End Set
            Get
                Return (_TotalSpendTimeMinutes)
            End Get
        End Property
        Public Property CompletionRemark() As String
            Set(ByVal value As String)
                _CompletionRemark = value
            End Set
            Get
                Return (_CompletionRemark)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALProject_Tasks_Resources = New DALProject_Tasks_Resources()

        End Sub

#End Region

#Region "Methods"
        Public Function Add() As Integer

            Dim rslt As Integer = objDALProject_Tasks_Resources.Add(_FK_TaskId, _FK_EmployeeId, _Involvmentpercentage, _completionpercentage, _IsCompletedByResource, _CREATED_BY, _TotalSpendTimeMinutes, _CompletionRemark)
            App_EventsLog.Insert_ToEventLog("Add", _FK_EmployeeId, "Project_Tasks_Resources", "Project Tasks")
            Return rslt
        End Function
        Public Function Update() As Integer

            Dim rslt As Integer = objDALProject_Tasks_Resources.Update(_FK_TaskId, _FK_EmployeeId, _Involvmentpercentage, _completionpercentage, _IsCompletedByResource, _LAST_UPDATE_BY, _TotalSpendTimeMinutes, _CompletionRemark)
            App_EventsLog.Insert_ToEventLog("Update", _FK_EmployeeId, "Project_Tasks_Resources", "Project Tasks")
            Return rslt
        End Function
        Public Function Delete() As Integer

            Dim rslt As Integer = objDALProject_Tasks_Resources.Delete(_FK_TaskId, _FK_EmployeeId)
            App_EventsLog.Insert_ToEventLog("Delete", _FK_EmployeeId, "Project_Tasks_Resources", "Project Tasks")
            Return rslt
        End Function
        Public Function GetAll() As DataTable

            Return objDALProject_Tasks_Resources.GetAll()

        End Function
        Public Function Get_By_FK_TaskId() As DataTable

            Return objDALProject_Tasks_Resources.Get_By_FK_TaskId(_FK_TaskId)

        End Function
        Public Function GetByPK() As Project_Tasks_Resources

            Dim dr As DataRow
            dr = objDALProject_Tasks_Resources.GetByPK(_FK_TaskId, _FK_EmployeeId)

            If Not IsDBNull(dr("FK_TaskId")) Then
                _FK_TaskId = dr("FK_TaskId")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("Involvmentpercentage")) Then
                _Involvmentpercentage = dr("Involvmentpercentage")
            End If
            If Not IsDBNull(dr("completionpercentage")) Then
                _completionpercentage = dr("completionpercentage")
            End If
            If Not IsDBNull(dr("IsCompletedByResource")) Then
                _IsCompletedByResource = dr("IsCompletedByResource")
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
            If Not IsDBNull(dr("TotalSpendTimeMinutes")) Then
                _TotalSpendTimeMinutes = dr("TotalSpendTimeMinutes")
            End If
            If Not IsDBNull(dr("CompletionRemark")) Then
                _CompletionRemark = dr("CompletionRemark")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace