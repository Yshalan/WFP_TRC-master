Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA_Emp_OnCall

    Public Class Emp_OnCall

#Region "Class Variables"


        Private _OnCallId As Integer
        Private _FK_EmployeeId As Integer
        Private _DutyDate As Date
        Private _FromHome As Boolean
        Private _FromTime As Integer
        Private _ToTime As Integer
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private _FK_ManagerId As Integer
        Private _Fk_EntityId As Integer
        Private _FK_DesginationId As Integer
        Private _FromDate As DateTime
        Private _ToDate As DateTime

        Private objDALEmp_OnCall As DALEmp_OnCall

#End Region

#Region "Public Properties"
        

        Public Property OnCallId() As Integer
            Set(ByVal value As Integer)
                _OnCallId = value
            End Set
            Get
                Return (_OnCallId)
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


        Public Property DutyDate() As Date
            Set(ByVal value As Date)
                _DutyDate = value
            End Set
            Get
                Return (_DutyDate)
            End Get
        End Property


        Public Property FromHome() As Boolean
            Set(ByVal value As Boolean)
                _FromHome = value
            End Set
            Get
                Return (_FromHome)
            End Get
        End Property


        Public Property FromTime() As Integer
            Set(ByVal value As Integer)
                _FromTime = value
            End Set
            Get
                Return (_FromTime)
            End Get
        End Property


        Public Property ToTime() As Integer
            Set(ByVal value As Integer)
                _ToTime = value
            End Set
            Get
                Return (_ToTime)
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

        Public Property FK_ManagerId() As Integer
            Set(ByVal value As Integer)
                _FK_ManagerId = value
            End Set
            Get
                Return (_FK_ManagerId)
            End Get
        End Property

        Public Property Fk_EntityId() As Integer
            Set(ByVal value As Integer)
                _Fk_EntityId = value
            End Set
            Get
                Return (_Fk_EntityId)
            End Get
        End Property

        Public Property FK_DesginationId() As Integer
            Set(ByVal value As Integer)
                _FK_DesginationId = value
            End Set
            Get
                Return (_FK_DesginationId)
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
#End Region


#Region "Constructor"

        Public Sub New()

            objDALEmp_OnCall = New DALEmp_OnCall()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALEmp_OnCall.Add(_FK_EmployeeId, _DutyDate, _FromHome, _FromTime, _ToTime, _CREATED_BY, _CREATED_DATE)
        End Function

        Public Function Update() As Integer

            Return objDALEmp_OnCall.Update(_OnCallId, _FK_EmployeeId, _DutyDate, _FromHome, _FromTime, _ToTime, _LAST_UPDATE_BY, _LAST_UPDATE_DATE)

        End Function



        Public Function Delete() As Integer

            Return objDALEmp_OnCall.Delete(_OnCallId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_OnCall.GetAll()

        End Function
        Public Function GetAllbyFilter() As DataTable

            Return objDALEmp_OnCall.GetAllbyFilter(_FK_ManagerId, _Fk_EntityId, _FK_DesginationId, _FromDate, _ToDate)

        End Function
        Public Function GetByPK() As Emp_OnCall

            Dim dr As DataRow
            dr = objDALEmp_OnCall.GetByPK(_OnCallId)

            If Not IsDBNull(dr("OnCallId")) Then
                _OnCallId = dr("OnCallId")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("DutyDate")) Then
                _DutyDate = dr("DutyDate")
            End If
            If Not IsDBNull(dr("FromHome")) Then
                _FromHome = dr("FromHome")
            End If
            If Not IsDBNull(dr("FromTime")) Then
                _FromTime = dr("FromTime")
            End If
            If Not IsDBNull(dr("ToTime")) Then
                _ToTime = dr("ToTime")
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