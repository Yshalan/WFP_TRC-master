Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events
Imports TA.Admin
Imports SmartV.UTILITIES

Namespace TA.SelfServices

    Public Class Emp_MoveRequest

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
        Private objDALEmp_MoveRequest As DALEmp_MoveRequest
        Private _FromDate As DateTime
        Private _ToDate As DateTime
        Private _FK_ManagerId As Integer
        Private _RejectedReason As String
        Private _FK_HREmployeeId As Integer
        Private _IsRemoteWork As Boolean
        Private objAPP_Settings As APP_Settings

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

        Public Property FK_ManagerId() As Long
            Set(ByVal value As Long)
                _FK_ManagerId = value
            End Set
            Get
                Return (_FK_ManagerId)
            End Get
        End Property

        Public Property FK_HREmployeeId() As Long
            Set(ByVal value As Long)
                _FK_HREmployeeId = value
            End Set
            Get
                Return (_FK_HREmployeeId)
            End Get
        End Property

        Public Property RejectedReason() As String
            Set(ByVal value As String)
                _RejectedReason = value
            End Set
            Get
                Return (_RejectedReason)
            End Get
        End Property

        Public Property IsRemoteWork() As Boolean
            Set(ByVal value As Boolean)
                _IsRemoteWork = value
            End Set
            Get
                Return (_IsRemoteWork)
            End Get
        End Property

#End Region

#Region "Constructor"

        Public Sub New()

            objDALEmp_MoveRequest = New DALEmp_MoveRequest()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALEmp_MoveRequest.Add(_MoveRequestId, _FK_EmployeeId, _Type, _MoveDate, _MoveTime, _FK_ReasonId, _Remarks, _Reader, _M_DATE_NUM, _M_TIME_NUM, _Status, _IsManual, _CREATED_BY, _AttachedFile, _IsRemoteWork, _IsFromMobile)
            App_EventsLog.Insert_ToEventLog("Add", _MoveRequestId, "Emp_Move_Request", "Manual Entry Request")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALEmp_MoveRequest.Update(_MoveRequestId, _FK_EmployeeId, _Type, _MoveDate, _MoveTime, _FK_ReasonId, _Remarks, _Reader, _M_DATE_NUM, _M_TIME_NUM, _Status, _IsManual, _LAST_UPDATE_BY, _AttachedFile, _IsRemoteWork, _IsFromMobile)
            App_EventsLog.Insert_ToEventLog("Update", _MoveRequestId, "Emp_Move_Request", "Manual Entry Request")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALEmp_MoveRequest.Delete(_MoveRequestId)
            App_EventsLog.Insert_ToEventLog("Delete", _MoveRequestId, "Emp_Move_Request", "Manual Entry Request")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_MoveRequest.GetAll()

        End Function

        Public Function GetByPK() As Emp_MoveRequest

            Dim dr As DataRow
            dr = objDALEmp_MoveRequest.GetByPK(_MoveRequestId)

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
            If Not IsDBNull(dr("IsRemoteWork")) Then
                _IsRemoteWork = dr("IsRemoteWork")
            End If
            Return Me
        End Function

        Public Function Getfilter() As DataTable

            Return objDALEmp_MoveRequest.GetFilter(_FK_EmployeeId, _FromDate, _ToDate, _Status)

        End Function

        Public Function GetFilter_Remote() As DataTable

            Return objDALEmp_MoveRequest.GetFilter_Remote(_FK_EmployeeId, _FromDate, _ToDate, _Status)

        End Function

        Public Function GetByDirectManager() As DataTable

            Return objDALEmp_MoveRequest.GetByDirectManager(_FK_ManagerId, _Status)

        End Function

        Public Function GetByHR() As DataTable
            Return objDALEmp_MoveRequest.GetByHR(_FK_ManagerId, _Status)
        End Function

        Public Function UpdateManualRequestStatus() As Integer

            Dim rslt As Integer = objDALEmp_MoveRequest.UpdateManualRequestStatus(_MoveRequestId, _Status, _RejectedReason, _LAST_UPDATE_BY, _FK_ManagerId, _FK_HREmployeeId)
            App_EventsLog.Insert_ToEventLog("UpdateManualRequestStatus", _MoveRequestId, "Emp_MoveRequest", "Manual Entry Request")
            Return rslt
        End Function

        Public Function GetByGeneralManager() As DataTable
            Return objDALEmp_MoveRequest.GetByGeneralManager(_Status)
        End Function

        Public Function ValidateManualEntryRequestPerDay(ByVal FK_EmployeeId As Integer, ByVal MoveDate As DateTime, ByRef ErrorMsg As String) As Boolean
            objAPP_Settings = New APP_Settings
            objAPP_Settings.GetByPK()
            Dim dt As DataTable = objDALEmp_MoveRequest.GetRequestsPerDay(FK_EmployeeId, MoveDate)
            Dim dr As DataRow = dt.Rows(0)
            If Not dt Is Nothing Then
                If dr("RequestCount") >= objAPP_Settings.ManualEntryNo Then
                    If SessionVariables.CultureInfo = "ar-JO" Then
                        ErrorMsg = "عدد طلبات الادخال اليدوي المسموح بها في اليوم هو :" & objAPP_Settings.ManualEntryNo & " مرات"
                    Else
                        ErrorMsg = "Allowed Manual Entry Requests Per Day: " & objAPP_Settings.ManualEntryNo & " Time(s)"
                    End If
                    Return False
                End If
            End If
            Return True
        End Function

        Public Function ValidateManualEntryRequestPerMonth(ByVal FK_EmployeeId As Integer, ByVal MoveDate As DateTime, ByRef ErrorMsg As String) As Boolean
            objAPP_Settings = New APP_Settings
            objAPP_Settings.GetByPK()
            Dim dt As DataTable = objDALEmp_MoveRequest.GetRequestsPerMonth(FK_EmployeeId, MoveDate)
            Dim dr As DataRow = dt.Rows(0)
            If Not dt Is Nothing Then
                If dr("RequestCount") >= objAPP_Settings.ManualEntryNo Then
                    If SessionVariables.CultureInfo = "ar-JO" Then
                        ErrorMsg = "عدد طلبات الادخال اليدوي المسموح بها في الشهر هو :" & objAPP_Settings.ManualEntryNo & " مرات"
                    Else
                        ErrorMsg = "Allowed Manual Entry Requests Per Month: " & objAPP_Settings.ManualEntryNo & " Time(s)"
                    End If
                    Return False
                End If
            End If
            Return True
        End Function

        Public Function CheckHasInOrOut(ByVal FK_EmployeeId As Integer, ByVal MoveDate As DateTime, ByVal ReasonName As String, ByVal FK_ReasonId As Integer, ByVal MoveTime As DateTime, ByRef ErrorMsg As String) As Boolean
            Dim dtHasInOrOut As DataTable = objDALEmp_MoveRequest.CheckHasInOrOut(FK_EmployeeId, MoveDate, FK_ReasonId, MoveTime)
            If Not dtHasInOrOut Is Nothing Then
                If dtHasInOrOut.Rows(0)("HasInOut") = True Then

                    If SessionVariables.CultureInfo = "ar-JO" Then
                        ErrorMsg = "يوجد حركة " & ReasonName & " في نفس اليوم"
                    Else
                        ErrorMsg = "You Have " & ReasonName & " Transaction For Same Day"
                    End If
                    Return False

                End If
            End If
            Return True
        End Function

        Public Function IfExists_EmpMoveRequest(ByVal FK_EmployeeId As Integer, ByVal MoveDate As DateTime, ByVal FK_ReasonId As Integer, ByVal MoveTime As DateTime, ByRef ErrorMsg As String) As Boolean
            Dim dtHasInOrOut As DataTable = objDALEmp_MoveRequest.IfExists_EmpMoveRequest(FK_EmployeeId, MoveDate, FK_ReasonId, MoveTime)
            If Not dtHasInOrOut Is Nothing Then
                If dtHasInOrOut.Rows(0)("HasRequest_Emp_MoveRequest") = True Then
                    If SessionVariables.CultureInfo = "ar-JO" Then
                        ErrorMsg = " يوجد طلب ادخال يدوي مسبق في نفس التاريخ"
                    Else
                        ErrorMsg = "Manual Entry Record Already Exists in the same date"
                    End If
                    Return False

                End If
            End If
            Return True
        End Function

        Public Function ValidateInManualEntryRequestPerDay(ByVal FK_EmployeeId As Integer, ByVal MoveDate As DateTime, ByRef ErrorMsg As String) As Boolean
            objAPP_Settings = New APP_Settings
            objAPP_Settings.GetByPK()
            Dim dt As DataTable = objDALEmp_MoveRequest.GetIn_OutCountPerDay(FK_EmployeeId, MoveDate)
            Dim dr As DataRow = dt.Rows(0)
            If Not dt Is Nothing Then
                If Not objAPP_Settings.NumberInTransactionRequests = Nothing AndAlso objAPP_Settings.NumberInTransactionRequests <> 0 Then
                    If dr("InRequestCount") >= objAPP_Settings.NumberInTransactionRequests Then
                        If SessionVariables.CultureInfo = "ar-JO" Then
                            ErrorMsg = "عدد طلبات الادخال اليدوي من نوع (دخول) المسموح بها في اليوم هو :" & objAPP_Settings.NumberInTransactionRequests & " مرات"
                        Else
                            ErrorMsg = "Number of Allowed Manual Entry Requests with Type (In) Per Day: " & objAPP_Settings.NumberInTransactionRequests & " Time(s)"
                        End If
                        Return False
                    End If
                End If
            End If
            Return True
        End Function

        Public Function ValidateOutManualEntryRequestPerDay(ByVal FK_EmployeeId As Integer, ByVal MoveDate As DateTime, ByRef ErrorMsg As String) As Boolean
            objAPP_Settings = New APP_Settings
            objAPP_Settings.GetByPK()
            Dim dt As DataTable = objDALEmp_MoveRequest.GetIn_OutCountPerDay(FK_EmployeeId, MoveDate)
            Dim dr As DataRow = dt.Rows(0)
            If Not dt Is Nothing Then
                If Not objAPP_Settings.NumberInTransactionRequests = Nothing AndAlso objAPP_Settings.NumberOutTransactionRequests <> 0 Then
                    If dr("OutRequestCount") >= objAPP_Settings.NumberOutTransactionRequests Then
                        If SessionVariables.CultureInfo = "ar-JO" Then
                            ErrorMsg = "عدد طلبات الادخال اليدوي من نوع (خروج) المسموح بها في اليوم هو :" & objAPP_Settings.NumberOutTransactionRequests & " مرات"
                        Else
                            ErrorMsg = "Number of Allowed Manual Entry Requests with Type (Out) Per Day: " & objAPP_Settings.NumberOutTransactionRequests & " Time(s)"
                        End If
                        Return False
                    End If
                End If
            End If
            Return True
        End Function

        Public Function GetLastTransaction() As DataTable
            Return objDALEmp_MoveRequest.GetLastTransaction(_FK_EmployeeId)
        End Function

#End Region

    End Class
End Namespace