Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Employees

    Public Class Employee_TAPolicy

#Region "Class Variables"


        Private _FK_EmployeeId As Long
        Private _FK_TAPolicyId As Integer
        Private _StartDate As DateTime
        Private _EndDate As DateTime
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALEmployee_TAPolicy As DALEmployee_TAPolicy

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


        Public Property FK_TAPolicyId() As Integer
            Set(ByVal value As Integer)
                _FK_TAPolicyId = value
            End Set
            Get
                Return (_FK_TAPolicyId)
            End Get
        End Property


        Public Property StartDate() As DateTime
            Set(ByVal value As DateTime)
                _StartDate = value
            End Set
            Get
                Return (_StartDate)
            End Get
        End Property


        Public Property EndDate() As DateTime
            Set(ByVal value As DateTime)
                _EndDate = value
            End Set
            Get
                Return (_EndDate)
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

            objDALEmployee_TAPolicy = New DALEmployee_TAPolicy()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer
            Dim rslt As Integer = objDALEmployee_TAPolicy.Add(_FK_EmployeeId, _FK_TAPolicyId, _StartDate, _EndDate, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE)
            App_EventsLog.Insert_ToEventLog("Add", _FK_EmployeeId, "Employee_TAPolicy", "Employee")
            Return rslt
        End Function

        Public Function AssignTAPolicy() As Integer
            Return objDALEmployee_TAPolicy.AssignTAPolicy(_FK_EmployeeId, _FK_TAPolicyId, _StartDate, _EndDate, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE)
        End Function

        Public Function Update() As Integer
            Dim rslt As Integer = objDALEmployee_TAPolicy.Update(_FK_EmployeeId, _FK_TAPolicyId, _StartDate, _EndDate, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE)
            App_EventsLog.Insert_ToEventLog("Edit", _FK_EmployeeId, "Employee_TAPolicy", "Employee")
            Return rslt
        End Function
        Public Function Delete() As Integer
            Dim rslt As Integer = objDALEmployee_TAPolicy.Delete(_FK_EmployeeId, _FK_TAPolicyId, _StartDate)
            App_EventsLog.Insert_ToEventLog("Delete", _FK_EmployeeId, "Employee_TAPolicy", "Employee")
            Return rslt
        End Function

        Public Function GetAll() As DataTable
            Return objDALEmployee_TAPolicy.GetAll()
        End Function
        Public Function GetByPK() As Employee_TAPolicy
            Dim dr As DataRow
            dr = objDALEmployee_TAPolicy.GetByPK(_FK_EmployeeId, _FK_TAPolicyId, _StartDate)

            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("FK_TAPolicyId")) Then
                _FK_TAPolicyId = dr("FK_TAPolicyId")
            End If
            If Not IsDBNull(dr("StartDate")) Then
                _StartDate = dr("StartDate")
            End If
            If Not IsDBNull(dr("EndDate")) Then
                _EndDate = dr("EndDate")
            Else
                _EndDate = Nothing
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

#Region "Extended Methods"
        Public Function GetAllInnerJoin() As DataTable
            Return objDALEmployee_TAPolicy.GetAllInnerJoin()
        End Function
        Public Function GetActivePolicyId() As Employee_TAPolicy
            Dim dr As DataRow
            dr = objDALEmployee_TAPolicy.GetActivePolicyId(_FK_EmployeeId)

            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("FK_TAPolicyId")) Then
                _FK_TAPolicyId = dr("FK_TAPolicyId")
            End If
            If Not IsDBNull(dr("StartDate")) Then
                _StartDate = dr("StartDate")
            End If
            If Not IsDBNull(dr("EndDate")) Then
                _EndDate = dr("EndDate")
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
        Public Function GetByEmployeeId() As DataTable
            Return objDALEmployee_TAPolicy.GetByEmployeeId(_FK_EmployeeId)
        End Function

        Public Function GetEmployeeLastStartedTAPolicy() As Employee_TAPolicy
            Dim dr As DataRow
            dr = objDALEmployee_TAPolicy.GetEmployeeLastStartedTAPolicy(_FK_EmployeeId)
            If Not dr Is Nothing Then


                If Not IsDBNull(dr("FK_EmployeeId")) Then
                    _FK_EmployeeId = dr("FK_EmployeeId")
                End If
                If Not IsDBNull(dr("FK_TAPolicyId")) Then
                    _FK_TAPolicyId = dr("FK_TAPolicyId")
                End If
                If Not IsDBNull(dr("StartDate")) Then
                    _StartDate = dr("StartDate")
                End If
                If Not IsDBNull(dr("EndDate")) Then
                    _EndDate = dr("EndDate")
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
            End If

            Return Me
        End Function

#End Region

    End Class
End Namespace