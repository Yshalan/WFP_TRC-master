Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace VMS

    Public Class VisitInfo

#Region "Class Variables"

        Private _VisitId As Integer
        Private _VisitorId As Integer
        Private _FK_DepartmentId As Integer
        Private _FK_EmployeeId As Integer
        Private _ReasonOfVisit As String
        Private _CardNo As String
        Private _ExpectedCheckInTime As DateTime
        Private _ExpectedCheckOutTime As DateTime
        Private _CheckInTime As DateTime
        Private _CheckOutTime As DateTime
        Private _Duration As Integer
        Private _Remarks As String
        Private _IsDeleted As Boolean
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALVisitInfo As DALVisitInfo

#End Region

#Region "Public Properties"
        Public Property VisitId() As Integer
            Set(ByVal value As Integer)
                _VisitId = value
            End Set
            Get
                Return (_VisitId)
            End Get
        End Property
        Public Property VisitorId() As Integer
            Set(ByVal value As Integer)
                _VisitorId = value
            End Set
            Get
                Return (_VisitorId)
            End Get
        End Property

        Public Property FK_DepartmentId() As Integer
            Set(ByVal value As Integer)
                _FK_DepartmentId = value
            End Set
            Get
                Return (_FK_DepartmentId)
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

        Public Property ReasonOfVisit() As String
            Set(ByVal value As String)
                _ReasonOfVisit = value
            End Set
            Get
                Return (_ReasonOfVisit)
            End Get
        End Property

        Public Property ExpectedCheckInTime() As DateTime
            Set(ByVal value As DateTime)
                _ExpectedCheckInTime = value
            End Set
            Get
                Return (_ExpectedCheckInTime)
            End Get
        End Property

        Public Property ExpectedCheckOutTime() As DateTime
            Set(ByVal value As DateTime)
                _ExpectedCheckOutTime = value
            End Set
            Get
                Return (_ExpectedCheckOutTime)
            End Get
        End Property

        Public Property Duration() As Integer
            Set(ByVal value As Integer)
                _Duration = value
            End Set
            Get
                Return (_Duration)
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

        Public Property IsDeleted() As Boolean
            Set(ByVal value As Boolean)
                _IsDeleted = value
            End Set
            Get
                Return (_IsDeleted)
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

            objDALVisitInfo = New DALVisitInfo()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALVisitInfo.Add(_VisitId, _FK_DepartmentId, _FK_EmployeeId, _ReasonOfVisit, _ExpectedCheckInTime, _ExpectedCheckOutTime, _Remarks, _IsDeleted, _CREATED_BY)
            App_EventsLog.Insert_ToEventLog("Add", _VisitId, "VisitInfo", "Schedule Visit")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALVisitInfo.Update(_VisitId, _FK_DepartmentId, _FK_EmployeeId, _ReasonOfVisit, _ExpectedCheckInTime, _ExpectedCheckOutTime, _Remarks, _IsDeleted, _LAST_UPDATE_BY)
            App_EventsLog.Insert_ToEventLog("Update", _VisitId, "VisitInfo", "Schedule Visit")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALVisitInfo.Delete(_VisitId)
            App_EventsLog.Insert_ToEventLog("Delete", _VisitId, "VisitInfo", "Schedule Visit")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALVisitInfo.GetAll()

        End Function

        Public Function GetByPK() As VisitInfo

            Dim dr As DataRow
            dr = objDALVisitInfo.GetByPK(_VisitId)

            If Not IsDBNull(dr("VisitId")) Then
                _VisitorId = dr("VisitId")
            End If
            If Not IsDBNull(dr("FK_DepartmentId")) Then
                _FK_DepartmentId = dr("FK_DepartmentId")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("ReasonOfVisit")) Then
                _ReasonOfVisit = dr("ReasonOfVisit")
            End If
            If Not IsDBNull(dr("ExpectedCheckInTime")) Then
                _ExpectedCheckInTime = dr("ExpectedCheckInTime")
            End If
            If Not IsDBNull(dr("ExpectedCheckOutTime")) Then
                _ExpectedCheckOutTime = dr("ExpectedCheckOutTime")
            End If
            If Not IsDBNull(dr("Remarks")) Then
                _Remarks = dr("Remarks")
            End If
            If Not IsDBNull(dr("IsDeleted")) Then
                _IsDeleted = dr("IsDeleted")
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