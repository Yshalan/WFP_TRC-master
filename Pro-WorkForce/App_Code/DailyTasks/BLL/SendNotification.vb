Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.DailyTasks

    Public Class SendNotification

#Region "Class Variables"

        Private objDALSendNotification As DALSendNotification
        Private _FK_Employee_Id As Integer
        Private _Type_Of_deducted As Integer
        Private _Leave_Date As DateTime
        Private _Number_Of_deducted_days As Integer
        Private _deducted_Reason As String
        Private _Updated_by As String
        Private _Updated_Date As DateTime

#End Region

#Region "Public Properties"

        Public Property FK_Employee_Id() As Integer
            Set(ByVal value As Integer)
                _FK_Employee_Id = value
            End Set
            Get
                Return (_FK_Employee_Id)
            End Get
        End Property

        Public Property Type_Of_deducted() As Integer
            Set(ByVal value As Integer)
                _Type_Of_deducted = value
            End Set
            Get
                Return (_Type_Of_deducted)
            End Get
        End Property

        Public Property Leave_Date() As DateTime
            Set(ByVal value As DateTime)
                _Leave_Date = value
            End Set
            Get
                Return (_Leave_Date)
            End Get
        End Property

        Public Property Number_Of_deducted_days() As Integer
            Set(ByVal value As Integer)
                _Number_Of_deducted_days = value
            End Set
            Get
                Return (_Number_Of_deducted_days)
            End Get
        End Property

        Public Property deducted_Reason() As String
            Set(ByVal value As String)
                _deducted_Reason = value
            End Set
            Get
                Return (_deducted_Reason)
            End Get
        End Property

        Public Property Updated_by() As String
            Set(ByVal value As String)
                _Updated_by = value
            End Set
            Get
                Return (_Updated_by)
            End Get
        End Property

        Public Property Updated_Date() As DateTime
            Set(ByVal value As DateTime)
                _Updated_Date = value
            End Set
            Get
                Return (_Updated_Date)
            End Get
        End Property

#End Region

#Region "Constructor"

        Public Sub New()

            objDALSendNotification = New DALSendNotification()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALSendNotification.Add(_FK_Employee_Id, _Type_Of_deducted, _Leave_Date, _Number_Of_deducted_days, _deducted_Reason, _Updated_by, _Updated_Date)
            App_EventsLog.Insert_ToEventLog("Add", _FK_Employee_Id, "SendNotification", "SendNotification")
            Return rslt
        End Function

#End Region

    End Class
End Namespace