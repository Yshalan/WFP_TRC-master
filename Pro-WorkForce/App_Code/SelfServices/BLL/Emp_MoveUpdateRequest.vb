Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports SmartV.UTILITIES
Imports TA.Admin
Imports TA.Events

Namespace TA.SelfServices

    Public Class Emp_MoveUpdateRequest

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
        Private _RejectionReason As String
        Private _FK_ManagerId As Integer
        Private _FK_HREmployeeId As Integer
        Private _FK_WorkLocationId As Integer
        Private _MoveId As Integer
        Private _FromDate As DateTime
        Private _ToDate As DateTime
        Private _UpdateTransactionType As Integer
        Private objDALEmp_MoveUpdateRequest As DALEmp_MoveUpdateRequest
        Private objAPP_Settings As APP_Settings

#End Region

#Region "Public Properties"
        Private _rejectedReason As String

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

        Public Property RejectionReason() As String
            Set(ByVal value As String)
                _RejectionReason = value
            End Set
            Get
                Return (_RejectionReason)
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

        Public Property FK_HREmployeeId() As Integer
            Set(ByVal value As Integer)
                _FK_HREmployeeId = value
            End Set
            Get
                Return (_FK_HREmployeeId)
            End Get
        End Property

        Public Property FK_WorkLocationId() As Integer
            Set(ByVal value As Integer)
                _FK_WorkLocationId = value
            End Set
            Get
                Return (_FK_WorkLocationId)
            End Get
        End Property

        Public Property MoveId() As Integer
            Set(ByVal value As Integer)
                _MoveId = value
            End Set
            Get
                Return (_MoveId)
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

        Public Property UpdateTransactionType() As Integer
            Set(ByVal value As Integer)
                _UpdateTransactionType = value
            End Set
            Get
                Return (_UpdateTransactionType)
            End Get
        End Property

#End Region

#Region "Constructor"

        Public Sub New()

            objDALEmp_MoveUpdateRequest = New DALEmp_MoveUpdateRequest()

        End Sub

#End Region

#Region "Methods"

        Property RejectedReason As String
            Get
                Return _rejectedReason
            End Get
            Set(value As String)
                _rejectedReason = value
            End Set
        End Property

        Public Function Add() As Integer

            Dim rslt As Integer = objDALEmp_MoveUpdateRequest.Add(_MoveRequestId, _FK_EmployeeId, _Type, _MoveDate, _MoveTime, _FK_ReasonId, _Remarks, _Reader, _M_DATE_NUM, _M_TIME_NUM, _Status, _IsManual, _CREATED_BY, _IsFromMobile, _MobileCoordinates, _IsRejected, _AttachedFile, _RejectionReason, _FK_ManagerId, _FK_HREmployeeId, _FK_WorkLocationId, _MoveId, _UpdateTransactionType)
            App_EventsLog.Insert_ToEventLog("Add", _MoveRequestId, "Emp_MoveUpdateRequest", "Update Transaction Request")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALEmp_MoveUpdateRequest.Update(_MoveRequestId, _FK_EmployeeId, _Type, _MoveDate, _MoveTime, _FK_ReasonId, _Remarks, _Reader, _M_DATE_NUM, _M_TIME_NUM, _Status, _SYS_Date, _IsManual, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE, _IsFromMobile, _MobileCoordinates, _IsRejected, _AttachedFile, _RejectionReason, _FK_ManagerId, _FK_HREmployeeId, _FK_WorkLocationId, _MoveId, _UpdateTransactionType)
            App_EventsLog.Insert_ToEventLog("Update", _MoveRequestId, "Emp_MoveUpdateRequest", "Update Transaction Request")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALEmp_MoveUpdateRequest.Delete(_MoveRequestId)
            App_EventsLog.Insert_ToEventLog("Delete", _MoveRequestId, "Emp_MoveUpdateRequest", "Update Transaction Request")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_MoveUpdateRequest.GetAll()

        End Function

        Public Function GetByPK() As Emp_MoveUpdateRequest

            Dim dr As DataRow
            dr = objDALEmp_MoveUpdateRequest.GetByPK(_MoveRequestId)

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
            If Not IsDBNull(dr("RejectionReason")) Then
                _RejectionReason = dr("RejectionReason")
            End If
            If Not IsDBNull(dr("FK_ManagerId")) Then
                _FK_ManagerId = dr("FK_ManagerId")
            End If
            If Not IsDBNull(dr("FK_HREmployeeId")) Then
                _FK_HREmployeeId = dr("FK_HREmployeeId")
            End If
            If Not IsDBNull(dr("FK_WorkLocationId")) Then
                _FK_WorkLocationId = dr("FK_WorkLocationId")
            End If
            If Not IsDBNull(dr("MoveId")) Then
                _MoveId = dr("MoveId")
            End If
            If Not IsDBNull(dr("UpdateTransactionType")) Then
                _UpdateTransactionType = dr("UpdateTransactionType")
            End If
            Return Me
        End Function

        Public Function ValidateManualEntryRequestPerDay(ByVal FK_EmployeeId As Integer, ByVal MoveDate As DateTime, ByRef ErrorMsg As String) As Boolean
            objAPP_Settings = New APP_Settings
            objAPP_Settings.GetByPK()
            Dim dt As DataTable = objDALEmp_MoveUpdateRequest.GetRequestsPerDay(FK_EmployeeId, MoveDate)
            Dim dr As DataRow = dt.Rows(0)
            If Not dt Is Nothing Then
                If dr("RequestCount") >= objAPP_Settings.ManualEntryNo Then
                    If SessionVariables.CultureInfo = "ar-JO" Then
                        ErrorMsg = "عدد طلبات الادخال اليدوي المسموح بها هو :" & objAPP_Settings.ManualEntryNo & " مرات"
                    Else
                        ErrorMsg = "Allowed Manual Entry Requests Per Day: " & objAPP_Settings.ManualEntryNo & " Time(s)"
                    End If
                    Return False
                End If
            End If
            Return True
        End Function

        Public Function CheckHasInOrOut(ByVal FK_EmployeeId As Integer, ByVal MoveDate As DateTime, ByVal ReasonName As String, ByVal FK_ReasonId As Integer, ByVal MoveTime As DateTime, ByRef ErrorMsg As String) As Boolean
            Dim dtHasInOrOut As DataTable = objDALEmp_MoveUpdateRequest.CheckHasInOrOut(FK_EmployeeId, MoveDate, FK_ReasonId, MoveTime)
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
            Dim dtHasInOrOut As DataTable = objDALEmp_MoveUpdateRequest.IfExists_EmpMoveRequest(FK_EmployeeId, MoveDate, FK_ReasonId, MoveTime)
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

        Public Function Getfilter() As DataTable

            Return objDALEmp_MoveUpdateRequest.GetFilter(_FK_EmployeeId, _FromDate, _ToDate, _Status)

        End Function

        Public Function GetByDirectManager() As DataTable

            Return objDALEmp_MoveUpdateRequest.GetByDirectManager(_FK_ManagerId, _Status)

        End Function

        Public Function UpdateManualRequestStatus() As Integer

            Dim rslt As Integer = objDALEmp_MoveUpdateRequest.UpdateManualRequestStatus(_MoveRequestId, _Status, _RejectionReason, _LAST_UPDATE_BY, _FK_ManagerId, _FK_HREmployeeId)
            App_EventsLog.Insert_ToEventLog("UpdateManualRequestStatus", _MoveRequestId, "Emp_MoveUpdateRequest", "Update Transaction Request")
            Return rslt
        End Function

        Public Function GetByHR() As DataTable
            Return objDALEmp_MoveUpdateRequest.GetByHR(_FK_ManagerId, _Status)
        End Function

        Public Function GetByGeneralManager() As DataTable
            Return objDALEmp_MoveUpdateRequest.GetByGeneralManager(_Status)
        End Function

#End Region

    End Class
End Namespace