Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Employees

    Public Class Emp_ATTCard

#Region "Class Variables"


        Private _FK_EmployeeId As Long
        Private _CardId As String
        Private _FromDate As DateTime
        Private _ToDate As DateTime
        Private _Active As Boolean
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALEmp_ATTCard As DALEmp_ATTCard

#End Region

#Region "Public Properties"


        Public Property FK_EmployeeId() As Long
            Set(ByVal value As Long)
                _FK_EmployeeId = value
            End Set
            Get
                Return (_FK_EmployeeId)
            End Get
        End Property


        Public Property CardId() As String
            Set(ByVal value As String)
                _CardId = value
            End Set
            Get
                Return (_CardId)
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


        Public Property ToDate() As DateTime
            Set(ByVal value As DateTime)
                _ToDate = value
            End Set
            Get
                Return (_ToDate)
            End Get
        End Property


        Public Property Active() As Boolean
            Set(ByVal value As Boolean)
                _Active = value
            End Set
            Get
                Return (_Active)
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

            objDALEmp_ATTCard = New DALEmp_ATTCard()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALEmp_ATTCard.Add(_FK_EmployeeId, _CardId, _FromDate, _ToDate, _Active, _CREATED_BY)
            App_EventsLog.Insert_ToEventLog("Add", _FK_EmployeeId, "Emp_ATTCard", "Employee")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALEmp_ATTCard.Update(_FK_EmployeeId, _CardId, _FromDate, _ToDate, _Active, _LAST_UPDATE_BY, _LAST_UPDATE_DATE)
            App_EventsLog.Insert_ToEventLog("Edit", _FK_EmployeeId, "Emp_ATTCard", "Employee")
            Return rslt
        End Function



        Public Function Delete() As Integer

            Dim rslt As Integer = objDALEmp_ATTCard.Delete(_FK_EmployeeId, _CardId)
            App_EventsLog.Insert_ToEventLog("Delete", _FK_EmployeeId, "Emp_ATTCard", "Employee")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_ATTCard.GetAll()

        End Function

        Public Function GetByPK() As Emp_ATTCard

            Dim dr As DataRow
            dr = objDALEmp_ATTCard.GetByPK(_FK_EmployeeId, _CardId)

            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("CardId")) Then
                _CardId = dr("CardId")
            End If
            If Not IsDBNull(dr("FromDate")) Then
                _FromDate = dr("FromDate")
            End If
            If Not IsDBNull(dr("ToDate")) Then
                _ToDate = dr("ToDate")
            End If
            If Not IsDBNull(dr("Active")) Then
                _Active = dr("Active")
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

        Public Function GetEmp_ATTCardsDetails() As DataTable

            Return objDALEmp_ATTCard.GetEmp_ATTCardsDetails(_FK_EmployeeId)

        End Function
        Public Function GetActiveCard() As Integer
            Return objDALEmp_ATTCard.GetActiveCard(_FK_EmployeeId)

        End Function

#End Region

    End Class
End Namespace