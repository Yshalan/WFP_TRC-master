Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Admin

    Public Class Emp_HR_NotificationTypes

#Region "Class Variables"

        Private _FK_HREmployeeId As Long
        Private _FK_NotificationTypeId As Integer
        Private objDALEmpHRNotificationType As DALEmp_HR_NotificationsType

#End Region

#Region "Public Properties"

        Public Property FK_HREmployeeId() As Long
            Get
                Return _FK_HREmployeeId
            End Get
            Set(ByVal value As Long)
                _FK_HREmployeeId = value
            End Set
        End Property

        Public Property FK_NotificationTypeId() As Integer
            Get
                Return _FK_NotificationTypeId
            End Get
            Set(ByVal value As Integer)
                _FK_NotificationTypeId = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Public Sub New()
            objDALEmpHRNotificationType = New DALEmp_HR_NotificationsType
        End Sub

#End Region

#Region "Methods"
        Public Function GetByHREmpID() As DataTable
            Return objDALEmpHRNotificationType.GetByHREmpID(_FK_HREmployeeId)
        End Function

        Public Function Add() As Integer
            Dim rslt As Integer = objDALEmpHRNotificationType.Add(_FK_HREmployeeId, _FK_NotificationTypeId)
            App_EventsLog.Insert_ToEventLog("Add", _FK_HREmployeeId, "Emp_HR_NotificationTypes", "Notification Type")
            Return rslt
        End Function

        Public Function Update() As Integer
            Dim rslt As Integer = objDALEmpHRNotificationType.Update(_FK_HREmployeeId, _FK_NotificationTypeId)
            App_EventsLog.Insert_ToEventLog("Edit", _FK_HREmployeeId, "Emp_HR_NotificationTypes", "Notification Type")
            Return rslt
        End Function

        Public Function Delete() As Integer
            Dim rslt As Integer = objDALEmpHRNotificationType.Delete(_FK_HREmployeeId)
            App_EventsLog.Insert_ToEventLog("Delete", _FK_HREmployeeId, "Emp_HR_NotificationTypes", "Notification Type")
            Return rslt
        End Function
#End Region

    End Class

End Namespace
