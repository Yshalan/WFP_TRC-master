Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports SmartV.UTILITIES
Imports TA.Lookup
Imports TA.Definitions
Imports TA.Events

Namespace TA.Employees

    Public Class Emp_Cards

#Region "Class Variables"


        Private _EmpId As Integer
        Private _CreatedBy As Integer
        Private _Lang As String
        Private _DesignId As Integer
        Private objDALEmp_Cards As New DALEmp_Cards
#End Region

#Region "Public Properties"


        Public Property EmpId() As Integer
            Set(ByVal value As Integer)
                _EmpId = value
            End Set
            Get
                Return (_EmpId)
            End Get
        End Property

        Public Property CreatedBy() As Integer
            Set(ByVal value As Integer)
                _CreatedBy = value
            End Set
            Get
                Return (_CreatedBy)
            End Get
        End Property

        Public Property DesignId() As Integer
            Set(ByVal value As Integer)
                _DesignId = value
            End Set
            Get
                Return (_DesignId)
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

            objDALEmp_Cards = New DALEmp_Cards()

        End Sub

#End Region

#Region "Methods"

        Public Function AddToQueue() As Integer

            Dim rslt As Integer = objDALEmp_Cards.AddToQueue(_EmpId, _CreatedBy, _DesignId)
            App_EventsLog.Insert_ToEventLog("Add", _EmpId, "Temp_Print", "Card Printing")
            Return rslt
        End Function

        Public Function DeleteFromQueue() As Integer

            Dim rslt As Integer = objDALEmp_Cards.DeleteFromQueue(_EmpId)
            App_EventsLog.Insert_ToEventLog("Delete", _EmpId, "Temp_Print", "Card Printing")
            Return rslt
        End Function

        Public Function GetAllInQueue() As DataTable

            Return objDALEmp_Cards.GetAllInQueue(_CreatedBy)

        End Function


        Public Function GetAllCardsDesign() As DataTable

            Return objDALEmp_Cards.GetAllCardsDesign(_Lang)

        End Function
#End Region








    End Class
End Namespace