Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.DailyTasks

    Public Class RecalculateRequest

#Region "Class Variables"


        Private _RequestId As Integer
        Private _Fk_CompanyId As Integer
        Private _Fk_EntityId As Integer
        Private _Fk_EmployeeId As Long
        Private _FromDate As DateTime
        Private _ToDate As DateTime
        Private _ImmediatelyStart As Boolean
        Private _RequestStartDateTime As DateTime
        Private _RecalStartDateTime As DateTime
        Private _RecalStatus As Integer
        Private _ReCalEndDateTime As DateTime
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _Remarks As String
        Private _FK_LogicalGroupId As Integer
        Private _FK_WorkLocation As Integer
        Private objDALRecalculateRequest As DALRecalculateRequest

#End Region

#Region "Public Properties"


        Public Property RequestId() As Integer
            Set(ByVal value As Integer)
                _RequestId = value
            End Set
            Get
                Return (_RequestId)
            End Get
        End Property


        Public Property Fk_CompanyId() As Integer
            Set(ByVal value As Integer)
                _Fk_CompanyId = value
            End Set
            Get
                Return (_Fk_CompanyId)
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

        Public Property FK_LogicalGroupId() As Integer
            Set(ByVal value As Integer)
                _FK_LogicalGroupId = value
            End Set
            Get
                Return (_FK_LogicalGroupId)
            End Get
        End Property

        Public Property FK_WorkLocation() As Integer
            Set(ByVal value As Integer)
                _FK_WorkLocation = value
            End Set
            Get
                Return (_FK_WorkLocation)
            End Get
        End Property

        Public Property Fk_EmployeeId() As Long
            Set(ByVal value As Long)
                _Fk_EmployeeId = value
            End Set
            Get
                Return (_Fk_EmployeeId)
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


        Public Property ImmediatelyStart() As Boolean
            Set(ByVal value As Boolean)
                _ImmediatelyStart = value
            End Set
            Get
                Return (_ImmediatelyStart)
            End Get
        End Property


        Public Property RequestStartDateTime() As DateTime
            Set(ByVal value As DateTime)
                _RequestStartDateTime = value
            End Set
            Get
                Return (_RequestStartDateTime)
            End Get
        End Property


        Public Property RecalStartDateTime() As DateTime
            Set(ByVal value As DateTime)
                _RecalStartDateTime = value
            End Set
            Get
                Return (_RecalStartDateTime)
            End Get
        End Property


        Public Property RecalStatus() As Integer
            Set(ByVal value As Integer)
                _RecalStatus = value
            End Set
            Get
                Return (_RecalStatus)
            End Get
        End Property


        Public Property ReCalEndDateTime() As DateTime
            Set(ByVal value As DateTime)
                _ReCalEndDateTime = value
            End Set
            Get
                Return (_ReCalEndDateTime)
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

            objDALRecalculateRequest = New DALRecalculateRequest()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALRecalculateRequest.Add(_RequestId, _Fk_CompanyId, _Fk_EntityId, _Fk_EmployeeId, _FromDate, _ToDate, _ImmediatelyStart, _RequestStartDateTime, _RecalStartDateTime, _RecalStatus, _ReCalEndDateTime, _CREATED_BY, _Remarks, _FK_LogicalGroupId, _FK_WorkLocation)
            App_EventsLog.Insert_ToEventLog("Add", _RequestId, "RecalculateRequest", "Recalculate Transactions")
            Return rslt
        End Function

        Public Function AddApproveViolationRequest() As Integer

            Return objDALRecalculateRequest.AddApproveViolationRequest(_Fk_CompanyId, _Fk_EntityId, _Fk_EmployeeId, _FromDate, _ToDate, _ImmediatelyStart, _RequestStartDateTime, _RecalStartDateTime, _RecalStatus, _ReCalEndDateTime, _CREATED_BY, _Remarks, _FK_LogicalGroupId, _FK_WorkLocation)
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALRecalculateRequest.Update(_RequestId, _Fk_CompanyId, _Fk_EntityId, _Fk_EmployeeId, _FromDate, _ToDate, _ImmediatelyStart, _RequestStartDateTime, _RecalStartDateTime, _RecalStatus, _ReCalEndDateTime, _CREATED_BY, _CREATED_DATE, _Remarks, _FK_LogicalGroupId, _FK_WorkLocation)
            App_EventsLog.Insert_ToEventLog("Update", _RequestId, "RecalculateRequest", "Recalculate Transactions")
            Return rslt

        End Function



        Public Function Delete() As Integer

            Dim rslt As Integer = objDALRecalculateRequest.Delete(_RequestId)
            App_EventsLog.Insert_ToEventLog("Delete", _RequestId, "RecalculateRequest", "Recalculate Transactions")
            Return rslt
        End Function
        Public Function DeleteApproveViolationRequest() As Integer

            Return objDALRecalculateRequest.DeleteApproveViolationRequest(_RequestId)

        End Function


        Public Function GetAll() As DataTable

            Return objDALRecalculateRequest.GetAll(_FromDate, _ToDate)

        End Function

        Public Function GetAll_ApproveViolationRequest() As DataTable
            Return objDALRecalculateRequest.GetAll_ApproveViolationRequest(Nothing, Nothing)
        End Function



        Public Function GetByPK() As RecalculateRequest

            Dim dr As DataRow
            dr = objDALRecalculateRequest.GetByPK(_RequestId)

            If Not IsDBNull(dr("RequestId")) Then
                _RequestId = dr("RequestId")
            End If
            If Not IsDBNull(dr("Fk_CompanyId")) Then
                _Fk_CompanyId = dr("Fk_CompanyId")
            End If
            If Not IsDBNull(dr("Fk_EntityId")) Then
                _Fk_EntityId = dr("Fk_EntityId")
            End If
            If Not IsDBNull(dr("Fk_EmployeeId")) Then
                _Fk_EmployeeId = dr("Fk_EmployeeId")
            End If
            If Not IsDBNull(dr("FromDate")) Then
                _FromDate = dr("FromDate")
            End If
            If Not IsDBNull(dr("ToDate")) Then
                _ToDate = dr("ToDate")
            End If
            If Not IsDBNull(dr("ImmediatelyStart")) Then
                _ImmediatelyStart = dr("ImmediatelyStart")
            End If
            If Not IsDBNull(dr("RequestStartDateTime")) Then
                _RequestStartDateTime = dr("RequestStartDateTime")
            End If
            If Not IsDBNull(dr("RecalStartDateTime")) Then
                _RecalStartDateTime = dr("RecalStartDateTime")
            End If
            If Not IsDBNull(dr("RecalStatus")) Then
                _RecalStatus = dr("RecalStatus")
            End If
            If Not IsDBNull(dr("ReCalEndDateTime")) Then
                _ReCalEndDateTime = dr("ReCalEndDateTime")
            End If
            If Not IsDBNull(dr("CREATED_BY")) Then
                _CREATED_BY = dr("CREATED_BY")
            End If
            If Not IsDBNull(dr("CREATED_DATE")) Then
                _CREATED_DATE = dr("CREATED_DATE")
            End If
            If Not IsDBNull(dr("Remarks")) Then
                _Remarks = dr("Remarks")
            End If
            If Not IsDBNull(dr("FK_LogicalGroupId")) Then
                _FK_LogicalGroupId = dr("FK_LogicalGroupId")
            End If
            If Not IsDBNull(dr("FK_WorkLocation")) Then
                _FK_WorkLocation = dr("FK_WorkLocation")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace