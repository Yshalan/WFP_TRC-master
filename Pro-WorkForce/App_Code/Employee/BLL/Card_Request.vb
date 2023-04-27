Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Card_Request

    Public Class Card_Request

#Region "Class Variables"


        Private _CardRequestId As Long
        Private _FK_EmployeeId As Integer
        Private _ReasonId As Integer
        Private _OtherReason As String
        Private _Status As Integer
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private _PrintStatus As Boolean
        Private _CardType As Integer
        Private _Remarks As String
        Private objDALCard_Request As DALCard_Request

#End Region

#Region "Public Properties"


        Public Property CardRequestId() As Long
            Set(ByVal value As Long)
                _CardRequestId = value
            End Set
            Get
                Return (_CardRequestId)
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


        Public Property ReasonId() As Integer
            Set(ByVal value As Integer)
                _ReasonId = value
            End Set
            Get
                Return (_ReasonId)
            End Get
        End Property

        Public Property OtherReason() As String
            Set(ByVal value As String)
                _OtherReason = value
            End Set
            Get
                Return (_OtherReason)
            End Get
        End Property

        Public Property Status() As Integer
            Set(ByVal value As Integer)
                _Status = value
            End Set
            Get
                Return (_Status)
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

        Public Property PrintStatus() As Boolean
            Set(ByVal value As Boolean)
                _PrintStatus = value
            End Set
            Get
                Return (_PrintStatus)
            End Get
        End Property

        Public Property CardType() As Integer
            Set(ByVal value As Integer)
                _CardType = value
            End Set
            Get
                Return (_CardType)
            End Get
        End Property
#End Region


#Region "Constructor"

        Public Sub New()

            objDALCard_Request = New DALCard_Request()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALCard_Request.Add(_CardRequestId, _FK_EmployeeId, _ReasonId, _OtherReason, _Status, _CardType, _Remarks, _CREATED_BY)
            App_EventsLog.Insert_ToEventLog("Add", _CardRequestId, "Card_Request", "Employee Card Request")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALCard_Request.Update(_CardRequestId, _FK_EmployeeId, _ReasonId, _OtherReason, _Status, _CardType, _Remarks, _LAST_UPDATE_BY)
            App_EventsLog.Insert_ToEventLog("Update", _CardRequestId, "Card_Request", "Employee Card Request")
            Return rslt
        End Function

        Public Function UpdateStatus() As Integer

            Dim rslt As Integer = objDALCard_Request.UpdateStatus(_CardRequestId, _Status, _CardType)
            App_EventsLog.Insert_ToEventLog("Update", _CardRequestId, "Card_Request", "Employee Card Request")
            Return rslt
        End Function
        Public Function UpdatePrintStatus() As Integer

            Dim rslt As Integer = objDALCard_Request.UpdatePrintStatus(_FK_EmployeeId, _Status, _CardType)
            App_EventsLog.Insert_ToEventLog("Update", _CardRequestId, "Card_Request", "Employee Card Request")
            Return rslt
        End Function



        Public Function Delete() As Integer

            Dim rslt As Integer = objDALCard_Request.Delete(_CardRequestId)
            App_EventsLog.Insert_ToEventLog("Delete", _CardRequestId, "Card_Request", "Employee Card Request")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALCard_Request.GetAll()

        End Function

        Public Function GetByPK() As Card_Request

            Dim dr As DataRow
            dr = objDALCard_Request.GetByPK(_CardRequestId)

            If Not IsDBNull(dr("CardRequestId")) Then
                _CardRequestId = dr("CardRequestId")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("ReasonId")) Then
                _ReasonId = dr("ReasonId")
            End If
            If Not IsDBNull(dr("Status")) Then
                _Status = dr("Status")
            End If
            If Not IsDBNull(dr("OtherReason")) Then
                _OtherReason = dr("OtherReason")
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

        Public Function GetAll_Inner() As DataTable

            Return objDALCard_Request.GetAll_Inner(_Status)

        End Function
        Public Function GetAll_CardRequest() As DataTable

            Return objDALCard_Request.GetAll_CardRequest(_FK_EmployeeId, _Status)

        End Function
        Public Function GetAll_CardRequest_ByEmployee() As DataTable

            Return objDALCard_Request.GetAll_CardRequest_ByEmployee(_FK_EmployeeId, _Status)

        End Function

        Public Function GetAll_Status() As DataTable

            Return objDALCard_Request.GetAll_Status(_PrintStatus)

        End Function

        Public Function UpdateRequestStatus() As Integer

            Dim rslt As Integer = objDALCard_Request.UpdateRequestStatus(_CardRequestId, _Status)
            App_EventsLog.Insert_ToEventLog("UpdateRequestStatus", _CardRequestId, "Card_Request", "Employee Card Request")
            Return rslt
        End Function

        Public Function GetByFK() As Card_Request

            Dim dr As DataRow
            dr = objDALCard_Request.GetByFK(_FK_EmployeeId, _Status)

            If Not IsDBNull(dr("CardRequestId")) Then
                _CardRequestId = dr("CardRequestId")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace