Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.DailyTasks

    Public Class HR_TAExceptionRequest

#Region "Class Variables"


        Private _ExceptionRequestId As Long
        Private _FK_EmployeeId As Long
        Private _FromDate As DateTime
        Private _ToDate As DateTime
        Private _Active As Boolean
        Private _Reason As String
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private _IsRejected As Boolean
        Private _UserId As Integer
        Private _RejectionReason As String
        Private objDALHR_TAExceptionRequest As DALHR_TAExceptionRequest

#End Region

#Region "Public Properties"


        Public Property ExceptionRequestId() As Long
            Set(ByVal value As Long)
                _ExceptionRequestId = value
            End Set
            Get
                Return (_ExceptionRequestId)
            End Get
        End Property

        Public Property UserId() As Integer
            Set(ByVal value As Integer)
                _UserId = value
            End Set
            Get
                Return (_UserId)
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


        Public Property Reason() As String
            Set(ByVal value As String)
                _Reason = value
            End Set
            Get
                Return (_Reason)
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


        Public Property IsRejected() As Boolean
            Set(ByVal value As Boolean)
                _IsRejected = value
            End Set
            Get
                Return (_IsRejected)
            End Get
        End Property



        Public Property RejectionReason() As String
            Set(ByVal value As String)
                _RejectionReason = value
            End Set
            Get
                Return (_RejectionReason)
            End Get
        End Property
#End Region


#Region "Constructor"

        Public Sub New()

            objDALHR_TAExceptionRequest = New DALHR_TAExceptionRequest()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALHR_TAExceptionRequest.Add(_FK_EmployeeId, _FromDate, _ToDate, _Reason, _CREATED_BY, _LAST_UPDATE_BY)
            App_EventsLog.Insert_ToEventLog("Add", _FK_EmployeeId, "HR_TAExceptionRequest", "HR TA Exceptions")
            Return rslt

        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALHR_TAExceptionRequest.Update(_FK_EmployeeId, _FromDate, _ToDate, _Reason, LAST_UPDATE_BY)
            App_EventsLog.Insert_ToEventLog("Edit", _FK_EmployeeId, "HR_TAExceptionRequest", "HR TA Exceptions")
            Return rslt
        End Function

        Public Function Update_TAException_RequestStatus() As Integer

            Return objDALHR_TAExceptionRequest.Update_TAException_RequestStatus(_FK_EmployeeId, _FromDate, _IsRejected, _RejectionReason, _LAST_UPDATE_BY)

        End Function


        Public Function Delete() As Integer

            Dim rslt As Integer = objDALHR_TAExceptionRequest.Delete(_FK_EmployeeId, _FromDate)
            App_EventsLog.Insert_ToEventLog("Delete", _FK_EmployeeId, "HR_TAExceptionRequest", "HR TA Exceptions")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALHR_TAExceptionRequest.GetAll()

        End Function

        Public Function GetByPK() As HR_TAExceptionRequest

            Dim dr As DataRow
            dr = objDALHR_TAExceptionRequest.GetByPK(_FK_EmployeeId, _FromDate)

            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("FromDate")) Then
                _FromDate = dr("FromDate")
            End If
            If Not IsDBNull(dr("ToDate")) Then
                _ToDate = dr("ToDate")
            Else
                _ToDate = DateTime.MinValue
            End If
            If Not IsDBNull(dr("Active")) Then
                _Active = dr("Active")
            End If
            If Not IsDBNull(dr("Reason")) Then
                _Reason = dr("Reason")
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

        Public Function GetAllInnerEmployee() As DataTable

            Return objDALHR_TAExceptionRequest.GetAllInnerEmployee()

        End Function

        Public Function HR_TAExceptionRequest_Select_AllInner_ByStatus() As DataTable

            Return objDALHR_TAExceptionRequest.HR_TAExceptionRequest_Select_AllInner_ByStatus(_UserId)

        End Function

        Public Function HR_TAExceptionRequest_Select_AllInner_Rejected() As DataTable

            Return objDALHR_TAExceptionRequest.HR_TAExceptionRequest_Select_AllInner_Rejected()

        End Function

#End Region

    End Class
End Namespace