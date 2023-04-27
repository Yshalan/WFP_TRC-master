Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.DailyTasks

    Public Class HR_Emp_Move

#Region "Class Variables"


        Private _MoveRequestId As Long
        Private _FK_EmployeeId As Long
        Private _Type As String
        Private _MoveDate As DateTime
        Private _MoveTime As DateTime
        Private _FK_ReasonId As Integer
        Private _Remarks As String
        Private _Reader As String
        Private _M_DATE_NUM As String
        Private _M_TIME_NUM As String
        Private _Status As String
        Private _SYS_Date As DateTime
        Private _IsManual As Boolean
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private _IsFromMobile As Boolean
        Private _MobileCoordinates As String
        Private _IsRejected As Boolean
        Private _AttachedFile As String
        Private objDALHR_Emp_Move As DALHR_Emp_Move
        Private _UserId As Integer
        Private _RejectionReason As String
#End Region

#Region "Public Properties"


        Public Property MoveRequestId() As Long
            Set(ByVal value As Long)
                _MoveRequestId = value
            End Set
            Get
                Return (_MoveRequestId)
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


        Public Property Type() As String
            Set(ByVal value As String)
                _Type = value
            End Set
            Get
                Return (_Type)
            End Get
        End Property


        Public Property MoveDate() As DateTime
            Set(ByVal value As DateTime)
                _MoveDate = value
            End Set
            Get
                Return (_MoveDate)
            End Get
        End Property


        Public Property MoveTime() As DateTime
            Set(ByVal value As DateTime)
                _MoveTime = value
            End Set
            Get
                Return (_MoveTime)
            End Get
        End Property


        Public Property FK_ReasonId() As Integer
            Set(ByVal value As Integer)
                _FK_ReasonId = value
            End Set
            Get
                Return (_FK_ReasonId)
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


        Public Property Reader() As String
            Set(ByVal value As String)
                _Reader = value
            End Set
            Get
                Return (_Reader)
            End Get
        End Property


        Public Property M_DATE_NUM() As String
            Set(ByVal value As String)
                _M_DATE_NUM = value
            End Set
            Get
                Return (_M_DATE_NUM)
            End Get
        End Property


        Public Property M_TIME_NUM() As String
            Set(ByVal value As String)
                _M_TIME_NUM = value
            End Set
            Get
                Return (_M_TIME_NUM)
            End Get
        End Property


        Public Property Status() As String
            Set(ByVal value As String)
                _Status = value
            End Set
            Get
                Return (_Status)
            End Get
        End Property


        Public Property SYS_Date() As DateTime
            Set(ByVal value As DateTime)
                _SYS_Date = value
            End Set
            Get
                Return (_SYS_Date)
            End Get
        End Property


        Public Property IsManual() As Boolean
            Set(ByVal value As Boolean)
                _IsManual = value
            End Set
            Get
                Return (_IsManual)
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


        Public Property IsFromMobile() As Boolean
            Set(ByVal value As Boolean)
                _IsFromMobile = value
            End Set
            Get
                Return (_IsFromMobile)
            End Get
        End Property


        Public Property MobileCoordinates() As String
            Set(ByVal value As String)
                _MobileCoordinates = value
            End Set
            Get
                Return (_MobileCoordinates)
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

        Public Property AttachedFile() As String
            Set(ByVal value As String)
                _AttachedFile = value
            End Set
            Get
                Return (_AttachedFile)
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

            objDALHR_Emp_Move = New DALHR_Emp_Move()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALHR_Emp_Move.Add(_MoveRequestId, _FK_EmployeeId, _Type, _MoveDate, _MoveTime, _FK_ReasonId, _Remarks, _Reader, _M_DATE_NUM, _M_TIME_NUM, _Status, _SYS_Date, _IsManual, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE, _IsFromMobile, _MobileCoordinates, _IsRejected, _AttachedFile)
            App_EventsLog.Insert_ToEventLog("Add", _MoveRequestId, "HR_Emp_Move", "HR Manual Entry")
            Return rslt

        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALHR_Emp_Move.Update(_MoveRequestId, _FK_EmployeeId, _Type, MoveDate, MoveTime, FK_ReasonId, _Remarks, _Reader, _Status, _IsManual, _LAST_UPDATE_BY, _AttachedFile)
            App_EventsLog.Insert_ToEventLog("Update", _MoveRequestId, "HR_Emp_Move", "HR Manual Entry")
            Return rslt
        End Function



        Public Function Delete() As Integer

            Dim rslt As Integer = objDALHR_Emp_Move.Delete(_MoveRequestId)
            App_EventsLog.Insert_ToEventLog("Delete", _MoveRequestId, "HR_Emp_Move", "HR Manual Entry")
            Return rslt
        End Function

        Public Function Update_Request_Status() As Integer

            Return objDALHR_Emp_Move.Update_Request_Status(_MoveRequestId, _IsRejected, _RejectionReason, _LAST_UPDATE_BY)

        End Function

        Public Function GetAll() As DataTable

            Return objDALHR_Emp_Move.GetAll()

        End Function

        Public Function GetByPK() As HR_Emp_Move

            Dim dr As DataRow
            dr = objDALHR_Emp_Move.GetByPK(_MoveRequestId)

            If Not IsDBNull(dr("MoveRequestId")) Then
                _MoveRequestId = dr("MoveRequestId")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("Type")) Then
                _Type = dr("Type")
            End If
            If Not IsDBNull(dr("MoveDate")) Then
                _MoveDate = dr("MoveDate")
            End If
            If Not IsDBNull(dr("MoveTime")) Then
                _MoveTime = dr("MoveTime")
            End If
            If Not IsDBNull(dr("FK_ReasonId")) Then
                _FK_ReasonId = dr("FK_ReasonId")
            End If
            If Not IsDBNull(dr("Remarks")) Then
                _Remarks = dr("Remarks")
            End If
            If Not IsDBNull(dr("Reader")) Then
                _Reader = dr("Reader")
            End If
            If Not IsDBNull(dr("M_DATE_NUM")) Then
                _M_DATE_NUM = dr("M_DATE_NUM")
            End If
            If Not IsDBNull(dr("M_TIME_NUM")) Then
                _M_TIME_NUM = dr("M_TIME_NUM")
            End If
            If Not IsDBNull(dr("Status")) Then
                _Status = dr("Status")
            End If
            If Not IsDBNull(dr("SYS_Date")) Then
                _SYS_Date = dr("SYS_Date")
            End If
            If Not IsDBNull(dr("IsManual")) Then
                _IsManual = dr("IsManual")
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
            If Not IsDBNull(dr("IsFromMobile")) Then
                _IsFromMobile = dr("IsFromMobile")
            End If
            If Not IsDBNull(dr("MobileCoordinates")) Then
                _MobileCoordinates = dr("MobileCoordinates")
            End If
            If Not IsDBNull(dr("IsRejected")) Then
                _IsRejected = dr("IsRejected")
            End If
            If Not IsDBNull(dr("AttachedFile")) Then
                _AttachedFile = dr("AttachedFile")
            End If
            Return Me
        End Function

        Public Function Getfilter() As DataTable

            Return objDALHR_Emp_Move.GetFilter(FK_EmployeeId, MoveDate)

        End Function

        Public Function GetAllForRealTime() As DataTable

            Return objDALHR_Emp_Move.GetAllForRealTime()

        End Function

        Public Function GetAll_ByStatus() As DataTable

            Return objDALHR_Emp_Move.GetAll_ByStatus(_UserId)

        End Function

        Public Function GetAll_Rejected() As DataTable

            Return objDALHR_Emp_Move.GetAll_Rejected(_UserId)

        End Function
#End Region

    End Class
End Namespace