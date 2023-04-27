Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports SmartV.UTILITIES
Imports TA.Definitions
Imports TA.Events
Imports TA.Employees
Imports TA.Admin

Namespace TA.SelfServices

    Public Class Emp_Study_PermissionRequest

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
        Private _StudyYear As Integer
        Private _Semester As String
        Private _FK_UniversityId As Integer?
        Private _Emp_GPAType As Integer?
        Private _Emp_GPA As Decimal?
        Private _FK_MajorId As Integer?
        Private _FK_SpecializationId As Integer?

        Private objDALEmp_Study_PermissionRequest As DALEmp_Study_PermissionRequest
        Private objEmp_WorkSchedule As Emp_WorkSchedule
        Private objAPP_Settings As APP_Settings
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

        Public Property StudyYear() As Integer
            Set(ByVal value As Integer)
                _StudyYear = value
            End Set
            Get
                Return (_StudyYear)
            End Get
        End Property

        Public Property Semester() As String
            Set(ByVal value As String)
                _Semester = value
            End Set
            Get
                Return (_Semester)
            End Get
        End Property

        Public Property FK_UniversityId() As Integer?
            Set(ByVal value As Integer?)
                _FK_UniversityId = value
            End Set
            Get
                Return (_FK_UniversityId)
            End Get
        End Property

        Public Property Emp_GPAType() As Integer?
            Set(ByVal value As Integer?)
                _Emp_GPAType = value
            End Set
            Get
                Return (_Emp_GPAType)
            End Get
        End Property

        Public Property Emp_GPA() As Decimal?
            Set(ByVal value As Decimal?)
                _Emp_GPA = value
            End Set
            Get
                Return (_Emp_GPA)
            End Get
        End Property

        Public Property FK_MajorId() As Integer?
            Set(ByVal value As Integer?)
                _FK_MajorId = value
            End Set
            Get
                Return (_FK_MajorId)
            End Get
        End Property

        Public Property FK_SpecializationId() As Integer?
            Set(ByVal value As Integer?)
                _FK_SpecializationId = value
            End Set
            Get
                Return (_FK_SpecializationId)
            End Get
        End Property



#End Region


#Region "Constructor"

        Public Sub New()

            objDALEmp_Study_PermissionRequest = New DALEmp_Study_PermissionRequest()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALEmp_Study_PermissionRequest.Add(_PermissionRequestId, _FK_EmployeeId, _FK_PermId, _PermDate, _FromTime, _ToTime, _IsFullDay, _Remark, _AttachedFile, _IsForPeriod, _PermEndDate,
                                                                        _IsSpecificDays, _Days, _IsFlexible, _IsDividable, _RejectionReason, _CREATED_BY, _FK_StatusId, _FlexibilePermissionDuration, _FK_ManagerId, _FK_HREmployeeId,
                                                                        _StudyYear, _Semester, _FK_UniversityId, _Emp_GPAType, _Emp_GPA, _FK_MajorId, _FK_SpecializationId)
            App_EventsLog.Insert_ToEventLog("Add", _PermissionRequestId, "Emp_Study_PermissionRequest", "Study Permission Request")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALEmp_Study_PermissionRequest.Update(_PermissionRequestId, _FK_EmployeeId, _FK_PermId, _PermDate, _FromTime, _ToTime, _IsFullDay, _Remark, _AttachedFile, _IsForPeriod, _PermEndDate,
                                                                           _IsSpecificDays, _Days, _IsFlexible, _IsDividable, _RejectionReason, _LAST_UPDATE_BY, _FK_StatusId, _FlexibilePermissionDuration, _FK_ManagerId, _FK_HREmployeeId,
                                                                           _StudyYear, _Semester, _FK_UniversityId, _Emp_GPAType, _Emp_GPA, _FK_MajorId, _FK_SpecializationId)
            App_EventsLog.Insert_ToEventLog("Update", _PermissionRequestId, "Emp_Study_PermissionRequest", "Study Permission Request")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALEmp_Study_PermissionRequest.Delete(_PermissionRequestId)
            App_EventsLog.Insert_ToEventLog("Delete", _PermissionRequestId, "Emp_Study_PermissionRequest", "Study Permission Request")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_Study_PermissionRequest.GetAll()

        End Function

        Public Function GetByPK() As Emp_Study_PermissionRequest

            Dim dr As DataRow
            dr = objDALEmp_Study_PermissionRequest.GetByPK(_PermissionRequestId)

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
            If Not IsDBNull(dr("StudyYear")) Then
                _StudyYear = dr("StudyYear")
            End If
            If Not IsDBNull(dr("Semester")) Then
                _Semester = dr("Semester")
            End If
            If Not IsDBNull(dr("FK_UniversityId")) Then
                _FK_UniversityId = dr("FK_UniversityId")
            End If
            If Not IsDBNull(dr("Emp_GPAType")) Then
                _Emp_GPAType = dr("Emp_GPAType")
            End If
            If Not IsDBNull(dr("Emp_GPA")) Then
                _Emp_GPA = dr("Emp_GPA")
            End If
            If Not IsDBNull(dr("FK_MajorId")) Then
                _FK_MajorId = dr("FK_MajorId")
            End If
            If Not IsDBNull(dr("FK_SpecializationId")) Then
                _FK_SpecializationId = dr("FK_SpecializationId")
            End If
            Return Me
        End Function

        Public Function GetByEmployee() As DataTable

            Return objDALEmp_Study_PermissionRequest.GetByEmployee(_FK_EmployeeId, _PermDate, _PermEndDate)

        End Function

        Public Function GetByStatusType() As DataTable

            Return objDALEmp_Study_PermissionRequest.GetByStatusType(_FK_EmployeeId, _FK_StatusId, _PermDate, _PermEndDate)

        End Function

        Public Function ValidateEmployeePermission(ByVal EmployeeId As Integer, ByVal PermissionId As Integer, ByVal dtCurrent As DataTable, ByVal StartDate As DateTime?, ByVal EndDate As DateTime?,
                                            ByVal objPermissionsTypes As PermissionsTypes, ByVal FromTime As DateTime, ByVal ToTime As DateTime, ByVal RequestedDays As String, ByRef ErrorMessage As String) As Boolean

            If IsExists(dtCurrent, StartDate, EndDate, FromTime, ToTime, PermissionId, PermDate, RequestedDays) = True Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    ErrorMessage = " يوجد طلب مغادرة مسبقاً ضمن الفترة المطلوبة او خلال الايام المطلوبة"
                Else
                    ErrorMessage = "Permission Record Already Exists Between The Date -Day- and Time Range"
                End If

                Return False
            End If

            If EmployeeId = 0 Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    ErrorMessage = "هذا المستخدم غير مرتبط مع موظف"
                Else
                    ErrorMessage = "This user not related with employee"
                End If

                Return False
            End If
        End Function

        Public Function IsExists(ByVal dtCurrent As DataTable, ByVal Fromdate As Date?, ByVal Todate As Date?, ByVal FromTime As Date?, ByVal ToTime As Date?, ByVal PermissionId As Integer,
                                ByVal PermDate As DateTime, ByVal RequestedDays As String) As Boolean
            Dim status As Boolean
            Dim requestStatus As Integer
            Dim containRequestStatus As Boolean = False
            Dim columns As DataColumnCollection = dtCurrent.Columns
            If columns.Contains("FK_StatusId") Then
                containRequestStatus = True
            End If

            For Each dr As DataRow In dtCurrent.Rows
                If PermissionId = dr("PermissionRequestId") Then
                    Continue For
                End If

                If containRequestStatus Then
                    requestStatus = Convert.ToInt32(dr("FK_StatusId"))
                    If (requestStatus = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByGeneralManager)) _
                        Or (requestStatus = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByHumanResource)) _
                        Or (requestStatus = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedbyManager)) Then
                        Continue For
                    End If
                End If

                If (Not dr("PermEndDate") Is System.DBNull.Value) Then
                    If Fromdate >= dr("PermDate") And Fromdate <= dr("PermEndDate") Or Todate >= dr("PermDate") And Todate <= dr("PermEndDate") Then
                        If (FromTime IsNot Nothing And (Not IsDBNull(dr("FromTime")))) Then
                            If FromTime >= dr("FromTime") And FromTime <= dr("ToTime") Or ToTime >= dr("FromTime") And ToTime <= dr("ToTime") Then
                                If ValidatePermissionDayName(RequestedDays, dtCurrent) Then
                                    If containRequestStatus Then
                                        requestStatus = Convert.ToInt32(dr("FK_StatusId"))
                                        If (requestStatus = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByGeneralManager)) _
                                            Or (requestStatus = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByHumanResource)) _
                                            Or (requestStatus = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedbyManager)) Then
                                            Continue For
                                        Else
                                            status = True
                                        End If
                                    Else
                                        status = True
                                    End If
                                End If
                            End If
                        End If
                    End If
                Else
                    If Fromdate = dr("PermDate") Or Todate = dr("PermDate") Then
                        If (FromTime <> Nothing And Not dr("FromTime") Is System.DBNull.Value) Then
                            If FromTime >= dr("FromTime") And FromTime <= dr("ToTime") Or ToTime >= dr("FromTime") And ToTime <= dr("ToTime") Then
                                If containRequestStatus Then
                                    requestStatus = Convert.ToInt32(dr("FK_StatusId"))
                                    If (requestStatus = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByGeneralManager)) _
                                        Or (requestStatus = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByHumanResource)) _
                                        Or (requestStatus = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedbyManager)) Then
                                        Continue For
                                    Else
                                        status = True
                                    End If
                                Else
                                    status = True
                                End If
                            End If
                        End If
                    End If
                End If

            Next
            Return status
        End Function

        Private Function ValidatePermissionDayName(ByVal RequestedDays As String, ByVal dtCurrent As DataTable) As Boolean
            Dim status As Boolean
            Dim rDays As String = RequestedDays.Trim
            Dim sDays As String = ""
            Dim arrRequestedDays As New ArrayList
            Dim arrDays As New ArrayList '--SAVED DAYS
            For Each value As String In rDays.Split(","c)
                If Not value = "" Then
                    arrRequestedDays.Add(value)
                End If
            Next

            For Each dr As DataRow In dtCurrent.Rows
                If (Not dr("Days") Is System.DBNull.Value) Then
                    sDays = dr("Days")
                End If
            Next
            For Each value As String In sDays.Split(","c)
                If Not value = "" Then
                    arrDays.Add(value)
                End If
            Next

            For Each ritem As String In arrRequestedDays
                For Each sitem As String In arrDays
                    If ritem = sitem Then
                        status = True
                    End If
                Next
            Next
            Return status
        End Function

        Public Function GetByDirectManager() As DataTable

            Return objDALEmp_Study_PermissionRequest.GetByDirectManager(_FK_ManagerId, _FK_StatusId)

        End Function

        Public Function GetByHR() As DataTable

            Return objDALEmp_Study_PermissionRequest.GetByHR(_FK_ManagerId, _FK_StatusId)

        End Function

        Public Function UpdatePermissionStatus() As Integer

            Dim rslt As Integer = objDALEmp_Study_PermissionRequest.UpdatePermissionStatus(_PermissionRequestId, _FK_StatusId, _RejectionReason, _LAST_UPDATE_BY, _FK_ManagerId, _FK_HREmployeeId)
            App_EventsLog.Insert_ToEventLog("UpdatePermissionStatus", _PermissionRequestId, "Emp_Study_PermissionRequest", "Study Permission Request")
            Return rslt
        End Function

        Public Function GetByGeneralManager() As DataTable
            Return objDALEmp_Study_PermissionRequest.GetByGeneralManager(_FK_StatusId)
        End Function


        Public Function CheckPermissionTimeInSideSchedule(ByVal FromTime As DateTime, ByVal ToTime As DateTime, ByRef ErrorMessage As String) As Boolean
            Dim dtScheduleInfo As DataTable = Nothing

            objEmp_WorkSchedule = New Emp_WorkSchedule
            With objEmp_WorkSchedule
                .FK_EmployeeId = _FK_EmployeeId
                .M_Date = _PermDate
                dtScheduleInfo = .GetAll_ScheduleDeatils()
                Dim a As String = FromTime.ToString("HH:mm")
                Dim b As String = ToTime.ToString("HH:mm tt")
                If dtScheduleInfo.Rows(0)("SCH_START_TIME_STR") > dtScheduleInfo.Rows(0)("SCH_END_TIME_STR") Then

                    Return True
                ElseIf FromTime.ToString("HH:mm") < dtScheduleInfo.Rows(0)("SCH_START_TIME_STR") Then
                    If SessionVariables.CultureInfo = "ar-JO" Then
                        ErrorMessage = "بداية وقت طلب المغادرة قبل بداية الجدول"
                    Else
                        ErrorMessage = "Requested Permission Start Before Schedule"
                    End If
                    Return False
                ElseIf ToTime.ToString("HH:mm") > dtScheduleInfo.Rows(0)("SCH_END_TIME_STR") Then

                    If SessionVariables.CultureInfo = "ar-JO" Then
                        ErrorMessage = "نهاية وقت طلب المغادرة بعد نهاية الجدول"
                    Else
                        ErrorMessage = "Requested Permission Start After Schedule"
                    End If
                    Return False
                End If
            End With




        End Function

        Public Function CheckMaxStudyDuration(ByVal FromTime As DateTime, ByVal ToTime As DateTime, ByVal isFlixible As Boolean, ByVal strFlexibileDuration As Integer, ByRef ErrorMessage As String) As Boolean
            Dim TotalDurationTime As Integer
            Dim MaxDuration As Integer
            If isFlixible = False Then
                Dim intFromTime As Integer = (FromTime.Hour * 60) + (FromTime.Minute)
                Dim intToTime As Integer = (ToTime.Hour * 60) + (ToTime.Minute)
                TotalDurationTime = intToTime - intFromTime
            Else
                TotalDurationTime = strFlexibileDuration
            End If
            objAPP_Settings = New APP_Settings
            With objAPP_Settings
                .GetByPK()
                MaxDuration = .MaxStudyPermission
            End With
            If TotalDurationTime > MaxDuration Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    ErrorMessage = "لا يمكنك تقديم مغادرة الدراسة اكثر من " & MaxDuration & " دقيقة "
                Else
                    ErrorMessage = "You Can not Apply for Study Permission More than " & MaxDuration & " Minutes"
                End If
                Return False
            End If
        End Function
#End Region

    End Class
End Namespace