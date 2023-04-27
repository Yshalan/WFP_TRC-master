Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports SmartV.UTILITIES
Imports TA.Definitions
Imports TA.Events

Namespace TA.SelfServices

    Public Class Emp_Nursing_Permission

#Region "Class Variables"


        Private _PermissionRequestId As Long
        Private _FK_EmployeeId As Long
        Private _FK_PermId As Integer
        Private _PermDate As DateTime
        Private _FromTime As DateTime
        Private _ToTime As DateTime
        Private _IsFullDay As Boolean
        Private _Remark As String
        Private _AttachedFile As String
        Private _IsForPeriod As Boolean
        Private _PermEndDate As DateTime
        Private _IsSpecificDays As Boolean
        Private _Days As String
        Private _IsFlexible As Boolean
        Private _IsDividable As Boolean
        Private _RejectionReason As String
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private _FK_StatusId As Integer
        Private _FlexibilePermissionDuration As Integer
        Private _FK_ManagerId As Integer
        Private _FK_HREmployeeId As Integer
        Private _AllowedTime As Integer
        Private _FK_MaternityLeaveTypeId As Integer
        Private objDALEmp_Nursing_Permission As DALEmp_Nursing_Permission

#End Region

#Region "Public Properties"


        Public Property PermissionRequestId() As Long
            Set(ByVal value As Long)
                _PermissionRequestId = value
            End Set
            Get
                Return (_PermissionRequestId)
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

        Public Property FK_PermId() As Integer
            Set(ByVal value As Integer)
                _FK_PermId = value
            End Set
            Get
                Return (_FK_PermId)
            End Get
        End Property

        Public Property PermDate() As DateTime
            Set(ByVal value As DateTime)
                _PermDate = value
            End Set
            Get
                Return (_PermDate)
            End Get
        End Property

        Public Property FromTime() As DateTime
            Set(ByVal value As DateTime)
                _FromTime = value
            End Set
            Get
                Return (_FromTime)
            End Get
        End Property

        Public Property ToTime() As DateTime
            Set(ByVal value As DateTime)
                _ToTime = value
            End Set
            Get
                Return (_ToTime)
            End Get
        End Property

        Public Property IsFullDay() As Boolean
            Set(ByVal value As Boolean)
                _IsFullDay = value
            End Set
            Get
                Return (_IsFullDay)
            End Get
        End Property

        Public Property Remark() As String
            Set(ByVal value As String)
                _Remark = value
            End Set
            Get
                Return (_Remark)
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

        Public Property IsForPeriod() As Boolean
            Set(ByVal value As Boolean)
                _IsForPeriod = value
            End Set
            Get
                Return (_IsForPeriod)
            End Get
        End Property

        Public Property PermEndDate() As DateTime
            Set(ByVal value As DateTime)
                _PermEndDate = value
            End Set
            Get
                Return (_PermEndDate)
            End Get
        End Property

        Public Property IsSpecificDays() As Boolean
            Set(ByVal value As Boolean)
                _IsSpecificDays = value
            End Set
            Get
                Return (_IsSpecificDays)
            End Get
        End Property

        Public Property Days() As String
            Set(ByVal value As String)
                _Days = value
            End Set
            Get
                Return (_Days)
            End Get
        End Property

        Public Property IsFlexible() As Boolean
            Set(ByVal value As Boolean)
                _IsFlexible = value
            End Set
            Get
                Return (_IsFlexible)
            End Get
        End Property

        Public Property IsDividable() As Boolean
            Set(ByVal value As Boolean)
                _IsDividable = value
            End Set
            Get
                Return (_IsDividable)
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

        Public Property FK_StatusId() As Integer
            Set(ByVal value As Integer)
                _FK_StatusId = value
            End Set
            Get
                Return (_FK_StatusId)
            End Get
        End Property

        Public Property FlexibilePermissionDuration() As Integer
            Set(ByVal value As Integer)
                _FlexibilePermissionDuration = value
            End Set
            Get
                Return (_FlexibilePermissionDuration)
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

        Public Property AllowedTime() As Integer
            Set(ByVal value As Integer)
                _AllowedTime = value
            End Set
            Get
                Return (_AllowedTime)
            End Get
        End Property

        Public Property FK_MaternityLeaveTypeId() As Integer
            Set(ByVal value As Integer)
                _FK_MaternityLeaveTypeId = value
            End Set
            Get
                Return (_FK_MaternityLeaveTypeId)
            End Get
        End Property
#End Region


#Region "Constructor"

        Public Sub New()

            objDALEmp_Nursing_Permission = New DALEmp_Nursing_Permission()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALEmp_Nursing_Permission.Add(_PermissionRequestId, _FK_EmployeeId, _FK_PermId, _PermDate, _FromTime, _ToTime, _IsFullDay, _Remark, _AttachedFile, _IsForPeriod, _PermEndDate, _IsSpecificDays, _Days, _IsFlexible, _IsDividable, _RejectionReason, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE, _FK_StatusId, _FlexibilePermissionDuration, _FK_ManagerId, _FK_HREmployeeId, _AllowedTime)
            App_EventsLog.Insert_ToEventLog("Add", _PermissionRequestId, "Emp_Nursing_Permission", "Nursing Permission Request")
            Return rslt
        End Function
        Public Function Update() As Integer

            Dim rslt As Integer = objDALEmp_Nursing_Permission.Update(_PermissionRequestId, _FK_EmployeeId, _FK_PermId, _PermDate, _FromTime, _ToTime, _IsFullDay, _Remark, _AttachedFile, _IsForPeriod, _PermEndDate, _IsSpecificDays, _Days, _IsFlexible, _IsDividable, _RejectionReason, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE, _FK_StatusId, _FlexibilePermissionDuration, _FK_ManagerId, _FK_HREmployeeId, _AllowedTime)
            App_EventsLog.Insert_ToEventLog("Update", _PermissionRequestId, "Emp_Nursing_Permission", "Nursing Permission Request")
            Return rslt
        End Function
        Public Function Delete() As Integer

            Dim rslt As Integer = objDALEmp_Nursing_Permission.Delete(_PermissionRequestId)
            App_EventsLog.Insert_ToEventLog("Delete", _PermissionRequestId, "Emp_Nursing_Permission", "Nursing Permission Request")
            Return rslt
        End Function
        Public Function GetAll() As DataTable

            Return objDALEmp_Nursing_Permission.GetAll()

        End Function
        Public Function GetByPK() As Emp_Nursing_Permission

            Dim dr As DataRow
            dr = objDALEmp_Nursing_Permission.GetByPK(_PermissionRequestId)

            If Not IsDBNull(dr("PermissionRequestId")) Then
                _PermissionRequestId = dr("PermissionRequestId")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("FK_PermId")) Then
                _FK_PermId = dr("FK_PermId")
            End If
            If Not IsDBNull(dr("PermDate")) Then
                _PermDate = dr("PermDate")
            End If
            If Not IsDBNull(dr("FromTime")) Then
                _FromTime = dr("FromTime")
            End If
            If Not IsDBNull(dr("ToTime")) Then
                _ToTime = dr("ToTime")
            End If
            If Not IsDBNull(dr("IsFullDay")) Then
                _IsFullDay = dr("IsFullDay")
            End If
            If Not IsDBNull(dr("Remark")) Then
                _Remark = dr("Remark")
            End If
            If Not IsDBNull(dr("AttachedFile")) Then
                _AttachedFile = dr("AttachedFile")
            End If
            If Not IsDBNull(dr("IsForPeriod")) Then
                _IsForPeriod = dr("IsForPeriod")
            End If
            If Not IsDBNull(dr("PermEndDate")) Then
                _PermEndDate = dr("PermEndDate")
            End If
            If Not IsDBNull(dr("IsSpecificDays")) Then
                _IsSpecificDays = dr("IsSpecificDays")
            End If
            If Not IsDBNull(dr("Days")) Then
                _Days = dr("Days")
            End If
            If Not IsDBNull(dr("IsFlexible")) Then
                _IsFlexible = dr("IsFlexible")
            End If
            If Not IsDBNull(dr("IsDividable")) Then
                _IsDividable = dr("IsDividable")
            End If
            If Not IsDBNull(dr("RejectionReason")) Then
                _RejectionReason = dr("RejectionReason")
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
            If Not IsDBNull(dr("FK_StatusId")) Then
                _FK_StatusId = dr("FK_StatusId")
            End If
            If Not IsDBNull(dr("FlexibilePermissionDuration")) Then
                _FlexibilePermissionDuration = dr("FlexibilePermissionDuration")
            End If
            If Not IsDBNull(dr("FK_ManagerId")) Then
                _FK_ManagerId = dr("FK_ManagerId")
            End If
            If Not IsDBNull(dr("FK_HREmployeeId")) Then
                _FK_HREmployeeId = dr("FK_HREmployeeId")
            End If
            If Not IsDBNull(dr("AllowedTime")) Then
                _AllowedTime = dr("AllowedTime")
            End If
            Return Me
        End Function
        Public Function ValidateEmployeePermission(ByRef ErrorMessage As String) As Boolean

            If NursingPermRequestExist(_FK_EmployeeId, _PermDate, _PermEndDate) = True Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    ErrorMessage = " يوجد طلب مغادرة رضاعة ضمن الفترة المطلوبة"
                Else
                    ErrorMessage = "Nursing Permission Request Already Exists Between Selected Date"
                End If

                Return False
            End If
            If NursingPermExist(_FK_EmployeeId, _PermDate, _PermEndDate) = True Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    ErrorMessage = " يوجد مغادرة رضاعة ضمن الفترة المطلوبة"
                Else
                    ErrorMessage = "Nursing Permission Already Exists Between Selected Date"
                End If

                Return False
            End If


        End Function
        Public Function NursingPermRequestExist(ByVal EmployeeId As Integer, ByVal PermDate As DateTime, ByVal PermEndDate As DateTime) As Boolean
            Return objDALEmp_Nursing_Permission.HasNursingRequest(EmployeeId, PermDate, PermEndDate)

        End Function
        Public Function NursingPermExist(ByVal EmployeeId As Integer, ByVal PermDate As DateTime, ByVal PermEndDate As DateTime) As Boolean
            Return objDALEmp_Nursing_Permission.HasNursing(EmployeeId, PermDate, PermEndDate)
        End Function
        Public Function GetByEmployee() As DataTable

            Return objDALEmp_Nursing_Permission.GetByEmployee(_FK_EmployeeId, _PermDate, _PermEndDate)

        End Function

        Public Function GetByDirectManager() As DataTable

            Return objDALEmp_Nursing_Permission.GetByDirectManager(_FK_ManagerId, _FK_StatusId)

        End Function

        Public Function GetByHR() As DataTable

            Return objDALEmp_Nursing_Permission.GetByHR(_FK_ManagerId, _FK_StatusId)

        End Function

        Public Function UpdatePermissionStatus() As Integer

            Dim rslt As Integer = objDALEmp_Nursing_Permission.UpdatePermissionStatus(_PermissionRequestId, _FK_StatusId, _RejectionReason, _LAST_UPDATE_BY, _FK_ManagerId, _FK_HREmployeeId)
            App_EventsLog.Insert_ToEventLog("UpdatePermissionStatus", _PermissionRequestId, "Emp_Nursing_Permission", "Nursing Permission Request")
            Return rslt
        End Function

        Public Function GetByGeneralManager() As DataTable
            Return objDALEmp_Nursing_Permission.GetByGeneralManager(_FK_StatusId)
        End Function

        Public Function GetByStatusType() As DataTable

            Return objDALEmp_Nursing_Permission.GetByStatusType(_FK_EmployeeId, _FK_StatusId, _PermDate, _PermEndDate)

        End Function

        Public Function ChKHasMaternityLeave() As DataTable

            Return objDALEmp_Nursing_Permission.ChKHasMaternityLeave(_FK_EmployeeId, _FK_MaternityLeaveTypeId)

        End Function

        Public Function AllowedNursingInRamadan() As DataTable

            Return objDALEmp_Nursing_Permission.AllowedNursingInRamadan(_PermDate, _PermEndDate)

        End Function
#End Region

    End Class
End Namespace