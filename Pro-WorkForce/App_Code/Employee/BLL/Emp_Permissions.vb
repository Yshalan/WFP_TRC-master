Imports System.Data
Imports TA.Definitions
Imports SmartV.UTILITIES.ProjectCommon
Imports SmartV.UTILITIES
Imports TA.Events
Imports TA.Admin

Namespace TA.Employees

    Public Class Emp_Permissions

#Region "Class Variables"

        Private objEmp_Leaves As Emp_Leaves
        Private _PermissionId As Long
        Private _LeaveId As Long
        Private _FK_EmployeeId As Long
        Private _FK_PermId As Integer
        Private _PermDate As DateTime
        Private _FromTime As DateTime?
        Private _ToTime As DateTime?
        Private _IsFullDay As Boolean?
        Private _Remark As String
        Private _AttachedFile As String
        Private _IsForPeriod As Boolean?
        Private _PermEndDate As DateTime?
        Private _IsSpecificDays As Boolean
        Private _Days As String
        Private _IsFlexible As Boolean?
        Private _IsDividable As Boolean?
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private _PermissionOption As Integer
        Private _FlexibilePermissionDuration As Integer
        Private objDALEmp_Permissions As DALEmp_Permissions
        Private objWorkSchedule_NormalTime As WorkSchedule_NormalTime
        Private objWorkSchedule_Flexible As WorkSchedule_Flexible
        Private objWorkSchedule_Open As WorkSchedule_Open
        Private objEmp_Shifts As Emp_Shifts
        Private objHoliday As New Holiday
        Private objPermissionTypeOccurance As New PermissionTypeOccurance
        Private objPermissionTypeDuration As New PermissionTypeDuration
        Private objEmpPermissions As Emp_Permissions
        Private objPermissionsTypes As PermissionsTypes
        Private objEmp_Leaves_BalanceHistory As New Emp_Leaves_BalanceHistory
        Private _BalanceDays As Double
        Private _AllowAfter As Integer
        Private objEmployee_Manager As Employee_Manager
        Private objOrgCompany As OrgCompany
        Private objOrgEntity As OrgEntity
        Private objEmployee As Employee
        Private _M_Date As DateTime
        Private _TOT_WORK_HRS As Integer
        Private _SCH_START_TIME As Integer
        Private _AllowedTime As Integer
        Private _IsValidInSchedule As Boolean
        Private _IsOff As Boolean
        Private _IsHoliday As Boolean
        Private _StudyYear As Integer
        Private _Semester As String
        Private _AllowBefore As Integer
        Private _PreviousPermDate As DateTime
        Private _UserId As Integer
        Private _PermissionRequestId As Integer
        Private _FK_UniversityId As Integer?
        Private _Emp_GPAType As Integer?
        Private _Emp_GPA As Decimal?
        Private _FK_MajorId As Integer?
        Private _FK_SpecializationId As Integer?

        Private objPermissionsTypes_Company As PermissionsTypes_Company
        Private objPermissionsTypes_Entity As PermissionsTypes_Entity
        Private objAPP_Settings As APP_Settings
        Private objRamadanPeriod As RamadanPeriod
        Private objLeavesTypes As LeavesTypes

#End Region

#Region "Public Properties"


        Public Property PermissionId() As Long
            Set(ByVal value As Long)
                _PermissionId = value
            End Set
            Get
                Return (_PermissionId)
            End Get
        End Property

        Public Property LeaveId() As Long
            Set(ByVal value As Long)
                _LeaveId = value
            End Set
            Get
                Return (_LeaveId)
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

        Public Property FromTime() As DateTime?
            Set(ByVal value As DateTime?)
                _FromTime = value
            End Set
            Get
                Return (_FromTime)
            End Get
        End Property

        Public Property ToTime() As DateTime?
            Set(ByVal value As DateTime?)
                _ToTime = value
            End Set
            Get
                Return (_ToTime)
            End Get
        End Property

        Public Property IsFullDay() As Boolean?
            Set(ByVal value As Boolean?)
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

        Public Property IsForPeriod() As Boolean?
            Set(ByVal value As Boolean?)
                _IsForPeriod = value
            End Set
            Get
                Return (_IsForPeriod)
            End Get
        End Property

        Public Property PermEndDate() As DateTime?
            Set(ByVal value As DateTime?)
                _PermEndDate = value
            End Set
            Get
                Return (_PermEndDate)
            End Get
        End Property

        Public Property IsSpecificDays() As Boolean?
            Set(ByVal value As Boolean?)
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

        Public Property IsFlexible() As Boolean?
            Set(ByVal value As Boolean?)
                _IsFlexible = value
            End Set
            Get
                Return (_IsFlexible)
            End Get
        End Property

        Public Property IsDividable() As Boolean?
            Set(ByVal value As Boolean?)
                _IsDividable = value
            End Set
            Get
                Return (_IsDividable)
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

        Public Property PermissionOption() As Integer
            Get
                Return _PermissionOption
            End Get
            Set(ByVal value As Integer)
                _PermissionOption = value
            End Set
        End Property

        Public Property FlexibilePermissionDuration() As Integer
            Get
                Return _FlexibilePermissionDuration
            End Get
            Set(ByVal value As Integer)
                _FlexibilePermissionDuration = value
            End Set
        End Property

        Public Property BalanceDays() As Double
            Get
                Return _BalanceDays
            End Get
            Set(ByVal value As Double)
                _BalanceDays = value
            End Set
        End Property

        Public Property AllowAfter() As Integer
            Get
                Return _AllowAfter
            End Get
            Set(ByVal value As Integer)
                _AllowAfter = value
            End Set
        End Property

        Public Property M_Date() As DateTime
            Get
                Return _M_Date
            End Get
            Set(ByVal value As DateTime)
                _M_Date = value
            End Set
        End Property

        Public Property TOT_WORK_HRS() As Integer
            Get
                Return _TOT_WORK_HRS
            End Get
            Set(ByVal value As Integer)
                _TOT_WORK_HRS = value
            End Set
        End Property

        Public Property SCH_START_TIME() As Integer
            Get
                Return _SCH_START_TIME
            End Get
            Set(ByVal value As Integer)
                _SCH_START_TIME = value
            End Set
        End Property

        Public Property AllowedTime() As Integer
            Get
                Return _AllowedTime
            End Get
            Set(ByVal value As Integer)
                _AllowedTime = value
            End Set
        End Property

        Public Property IsValidInSchedule() As Boolean
            Get
                Return _IsValidInSchedule
            End Get
            Set(ByVal value As Boolean)
                _IsValidInSchedule = value
            End Set
        End Property

        Public Property IsOff() As Boolean
            Get
                Return _IsOff
            End Get
            Set(ByVal value As Boolean)
                _IsOff = value
            End Set
        End Property

        Public Property IsHoliday() As Boolean
            Get
                Return _IsHoliday
            End Get
            Set(ByVal value As Boolean)
                _IsHoliday = value
            End Set
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

        Public Property AllowBefore() As Integer
            Get
                Return _AllowBefore
            End Get
            Set(ByVal value As Integer)
                _AllowBefore = value
            End Set
        End Property

        Public Property PreviousPermDate() As DateTime
            Get
                Return _PreviousPermDate
            End Get
            Set(ByVal value As DateTime)
                _PreviousPermDate = value
            End Set
        End Property

        Public Property UserId() As Integer
            Get
                Return _UserId
            End Get
            Set(ByVal value As Integer)
                _UserId = value
            End Set
        End Property

        Public Property PermissionRequestId() As Integer
            Get
                Return _PermissionRequestId
            End Get
            Set(ByVal value As Integer)
                _PermissionRequestId = value
            End Set
        End Property

        Public Property FK_UniversityId() As Integer?
            Get
                Return _FK_UniversityId
            End Get
            Set(ByVal value As Integer?)
                _FK_UniversityId = value
            End Set
        End Property

        Public Property Emp_GPAType() As Integer?
            Get
                Return _Emp_GPAType
            End Get
            Set(ByVal value As Integer?)
                _Emp_GPAType = value
            End Set
        End Property

        Public Property Emp_GPA() As Decimal?
            Get
                Return _Emp_GPA
            End Get
            Set(ByVal value As Decimal?)
                _Emp_GPA = value
            End Set
        End Property

        Public Property FK_MajorId() As Integer?
            Get
                Return _FK_MajorId
            End Get
            Set(ByVal value As Integer?)
                _FK_MajorId = value
            End Set
        End Property

        Public Property FK_SpecializationId() As Integer?
            Get
                Return _FK_SpecializationId
            End Get
            Set(ByVal value As Integer?)
                _FK_SpecializationId = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Public Sub New()

            objDALEmp_Permissions = New DALEmp_Permissions()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALEmp_Permissions.Add(_PermissionId, _FK_EmployeeId, _FK_PermId, _PermDate, _FromTime, _ToTime, _IsFullDay, _Remark, _AttachedFile,
                                                            _IsForPeriod, _PermEndDate, _IsSpecificDays, _Days, _IsFlexible, _IsDividable, _CREATED_BY, _PermissionOption,
                                                            _FlexibilePermissionDuration, _BalanceDays, _AllowedTime, _StudyYear, _Semester, _FK_UniversityId, _Emp_GPAType,
                                                            _Emp_GPA, _FK_MajorId, _FK_SpecializationId)
            Dim morelogdetails As String
            morelogdetails = "EmployeeId =" + _FK_EmployeeId.ToString() + ",Remark=" + _Remark.ToString()
            App_EventsLog.Insert_ToEventLog("Add", _PermissionId, "Emp_Permissions", "Employee Permission", morelogdetails)
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALEmp_Permissions.Update(_PermissionId, _FK_EmployeeId, _FK_PermId, _PermDate, _FromTime, _ToTime, _IsFullDay,
                                                _Remark, _IsForPeriod, _PermEndDate, _IsSpecificDays, _Days, _IsFlexible, _IsDividable,
                                                 _LAST_UPDATE_BY, _PermissionOption, _FlexibilePermissionDuration,
                                                _AttachedFile, _BalanceDays, _AllowedTime, _StudyYear, _Semester, _FK_UniversityId, _Emp_GPAType, _Emp_GPA,
                                                _FK_MajorId, _FK_SpecializationId)
            Dim morelogdetails As String
            morelogdetails = "EmployeeId =" + _FK_EmployeeId.ToString() + ",Remark=" + _Remark.ToString()
            App_EventsLog.Insert_ToEventLog("Edit", _PermissionId, "Emp_Permissions", "Employee Permission", morelogdetails)
            Return rslt
        End Function

        Public Function UpdateAttachment() As Integer

            Dim rslt As Integer = objDALEmp_Permissions.UpdateAttachment(_PermissionId, _AttachedFile)
            App_EventsLog.Insert_ToEventLog("Edit", _PermissionId, "Emp_Permissions", "Employee Permission")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALEmp_Permissions.Delete(_PermissionId)
            App_EventsLog.Insert_ToEventLog("Delete", _PermissionId, "Emp_Permissions", "Employee Permission")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_Permissions.GetAll()

        End Function

        Public Function Get_ByUserId_InnerPage() As DataTable

            Return objDALEmp_Permissions.Get_ByUserId_InnerPage(_UserId)

        End Function

        Public Function Get_StudyNursingByUserId_InnerPage() As DataTable

            Return objDALEmp_Permissions.Get_StudyNursingByUserId_InnerPage(_UserId)

        End Function

        Public Function GetAllBySearchCriteria(ByVal fromdate As DateTime, ByVal todate As DateTime, ByVal companyid As Integer?, ByVal entityid As Integer?, ByVal employeeid As Integer?) As DataTable

            Return objDALEmp_Permissions.GetAllBySearchCriteria(fromdate, todate, companyid, entityid, employeeid)

        End Function

        Public Function GetAllPermissionsByEmployee() As DataTable

            Return objDALEmp_Permissions.GetAllPermissionsByEmployee(_FK_EmployeeId, _FromTime, _ToTime, _FK_PermId)

        End Function

        Public Function GetOccuranceForWeek() As Integer

            Return objDALEmp_Permissions.GetOccuranceForWeek(_PermDate, _FK_EmployeeId, _FK_PermId)

        End Function

        Public Function GetOccuranceForMonth() As Integer

            Return objDALEmp_Permissions.GetOccuranceForMonth(_PermDate, _FK_EmployeeId, _FK_PermId)

        End Function

        Public Function GetOccuranceForYear() As Integer

            Return objDALEmp_Permissions.GetOccuranceForYear(_PermDate, _FK_EmployeeId, _FK_PermId)

        End Function

        Private Function GetOccuranceForDay() As Object

            Return objDALEmp_Permissions.GetOccuranceForDay(_PermDate, _FK_EmployeeId, _FK_PermId)

        End Function

        Public Function GetDurationForWeek() As Integer

            Return objDALEmp_Permissions.GetDurationForWeek(_PermDate, _FK_EmployeeId, _FK_PermId)

        End Function

        Public Function GetDurationForMonth() As Integer

            Return objDALEmp_Permissions.GetDurationForMonth(_PermDate, _FK_EmployeeId, _FK_PermId)

        End Function

        Public Function GetDurationForYear() As Integer

            Return objDALEmp_Permissions.GetDurationForMonth(_PermDate, _FK_EmployeeId, _FK_PermId)

        End Function

        Private Function GetDurationForDay() As Object

            Return objDALEmp_Permissions.GetDurationForDay(_PermDate, _FK_EmployeeId, _FK_PermId)

        End Function
        Private Function IsleaveExists(ByVal Fromdate As Date, ByVal Todate As Date, ByVal EmployeeId As Integer, ByVal LeaveId As Integer) As Boolean
            Dim EmpDt As DataTable = New DataTable

            objEmp_Leaves = New Emp_Leaves

            objEmp_Leaves.FK_EmployeeId = EmployeeId
            objEmp_Leaves.FromDate = Fromdate
            objEmp_Leaves.ToDate = Todate
            objEmp_Leaves.LeaveId = LeaveId
            EmpDt = objEmp_Leaves.GetAllLeavesByEmployee()

            'to check if duplicate data in leave request table


            Dim status As Boolean
            For Each dr As DataRow In EmpDt.Rows

                If Fromdate >= dr("FromDate") And Fromdate <= dr("ToDate") Or Todate >= dr("FromDate") And Todate <= dr("ToDate") Then
                    status = True
                End If
            Next
            Return status
        End Function

        Public Function IsAllowedForManagers(ByVal FK_EmployeeId As Integer, ByVal PermissionTypeId As Integer, ByRef ErrorMsg As String) As Boolean
            objPermissionsTypes = New PermissionsTypes
            objEmployee_Manager = New Employee_Manager

            With objPermissionsTypes
                .PermId = PermissionTypeId
                .GetByPK()
                If .AllowedForManagers = True Then
                    objEmployee_Manager.FK_ManagerId = FK_EmployeeId
                    objEmployee_Manager.IsManager_Employee()
                    If objEmployee_Manager.IsManager = False Then
                        If SessionVariables.CultureInfo = "ar-JO" Then
                            ErrorMsg = "لايمكن تقديم طلب مغادرة من هذا النوع، نوع المغادرةالذي اخترته فقط للمدراء"
                        Else
                            ErrorMsg = "You Cannot Apply For This Type Of Permission, This Type of Permission For Managers Only"
                        End If
                        Return False
                    End If
                End If
            End With
            Return True
        End Function

        Public Function IsAllowedForSpecificEntity(ByVal FK_EmployeeId As Integer, ByVal PermissionTypeId As Integer, ByRef ErrorMsg As String) As Boolean
            objOrgEntity = New OrgEntity
            objEmployee = New Employee
            objPermissionsTypes_Entity = New PermissionsTypes_Entity
            Dim dtPermissionEntity As DataTable
            Dim PermEntity_Exist As Boolean = False
            With objPermissionsTypes
                .PermId = PermissionTypeId
                .GetByPK()
                If .IsSpecificEntity = True Then
                    objEmployee.EmployeeId = FK_EmployeeId
                    objEmployee.GetByPK()
                    objPermissionsTypes_Entity.FK_PermId = PermissionTypeId
                    dtPermissionEntity = objPermissionsTypes_Entity.GetByPermId

                    For Each row In dtPermissionEntity.Rows
                        If objEmployee.FK_EntityId = row("FK_EntityId").ToString Then
                            PermEntity_Exist = True
                        End If
                    Next
                    If Not PermEntity_Exist = True Then
                        If SessionVariables.CultureInfo = "ar-JO" Then
                            ErrorMsg = "لايمكن تقديم طلب مغادرة، هذا النوع من المغادرة يمكن طلبه للموظفين في وحدة عمل محددة"
                        Else
                            ErrorMsg = "You Cannot Apply For This Type Of Permission, The Select Permission Type Can Be Only Applied For Specific Entity(s)"
                        End If
                        Return False
                    End If
                End If
            End With
            Return True
        End Function

        Public Function IsAllowedForSpecificCompany(ByVal FK_EmployeeId As Integer, ByVal PermissionTypeId As Integer, ByRef ErrorMsg As String) As Boolean
            objOrgCompany = New OrgCompany
            objEmployee = New Employee
            objPermissionsTypes_Company = New PermissionsTypes_Company
            Dim dtPermissionCompany As DataTable
            Dim PermCompany_Exist As Boolean = False
            With objPermissionsTypes
                .PermId = PermissionTypeId
                .GetByPK()
                If .IsSpecificCompany = True Then
                    objEmployee.EmployeeId = FK_EmployeeId
                    objEmployee.GetByPK()
                    objPermissionsTypes_Company.FK_PermId = PermissionTypeId
                    dtPermissionCompany = objPermissionsTypes_Company.GetByPermId

                    For Each row In dtPermissionCompany.Rows
                        If objEmployee.FK_CompanyId = row("FK_CompanyId").ToString Then
                            PermCompany_Exist = True
                        End If
                    Next
                    If Not PermCompany_Exist = True Then
                        If SessionVariables.CultureInfo = "ar-JO" Then
                            ErrorMsg = "لايمكن تقديم طلب مغادرة، هذا النوع من المغادرة يمكن طلبه للموظفين في شركة محددة"
                        Else
                            ErrorMsg = "You Cannot Apply For This Type Of Permission, The Select Permission Type Can Be Only Applied For Specific Company(s)"
                        End If
                        Return False
                    End If
                End If
            End With
            Return True
        End Function

        Public Function ShouldCompleteHalfWHRS(ByVal FK_EmployeeId As Integer, ByVal PermissionTypeId As Integer, ByVal PermDate As DateTime, ByVal FromTime As DateTime, ByVal ToTime As DateTime, ByRef ErrorMsg As String) As Boolean
            objPermissionsTypes = New PermissionsTypes
            With objPermissionsTypes
                .PermId = PermissionTypeId
                .GetByPK()
                Dim intFromTime As Integer = (FromTime.Hour * 60) + (FromTime.Minute)
                Dim intToTime As Integer = (ToTime.Hour * 60) + (ToTime.Minute)
                Dim intTotalTime As Integer = intToTime - intFromTime
                Dim halfTotWHRS As Integer
                If .ShouldComplete50WHRS = True Then
                    Me.FK_EmployeeId = FK_EmployeeId
                    Me.M_Date = PermDate
                    Me.GetEmp_TotHRS()
                    halfTotWHRS = Me.TOT_WORK_HRS / 2
                    If halfTotWHRS < (intTotalTime) Then
                        If SessionVariables.CultureInfo = "ar-JO" Then
                            ErrorMsg = "مدة المغادرة يجب ان لاتتجاوز 50% من عدد ساعات عمل الجدول"
                        Else
                            ErrorMsg = "Permission Duration Must Not Exceed 50% Of Schedule Working Hours"
                        End If
                        Return False
                    End If
                End If
            End With
            Return True
        End Function

        Public Function StudyNursing_ShouldCompleteNoOfHours(ByVal FK_EmployeeId As Integer, ByVal PermissionTypeId As Integer, ByVal PermDate As DateTime?, ByVal PermEndDate? As DateTime, ByRef ErrorMsg As String) As Boolean
            objAPP_Settings = New APP_Settings
            objPermissionsTypes = New PermissionsTypes
            With objAPP_Settings
                .GetByPK()
            End With

            With objPermissionsTypes
                .PermId = PermissionTypeId
                .GetByPK()
            End With
            Dim PermissionOption As Integer = 1
            Dim dtHasStudyNursingPermission As DataTable


            objEmpPermissions = New Emp_Permissions
            With objEmpPermissions
                dtHasStudyNursingPermission = objDALEmp_Permissions.Check_Has_StudyOrNursing_Permission(FK_EmployeeId, PermDate, IIf(PermEndDate Is Nothing, PermDate, PermEndDate), PermissionTypeId, PermissionOption)
            End With
            If dtHasStudyNursingPermission.Rows(0)("EligibleToRequest_AdditionalPermission") = 0 Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    ErrorMsg = "يوجد مغادرة دراسة او رضاعة في الفترة التي قمت باختيارها و الموظف لم يستكمل عدد الساعات المطلوبة"
                Else
                    ErrorMsg = "Study Or Nursing Permission Occuered in The Selected Period and Employee did not Complete the Required No. Of Hours"
                End If
                Return False
            End If

            Return True
        End Function

        Public Function GetEmp_TotHRS() As Emp_Permissions
            Dim dr As DataRow
            dr = objDALEmp_Permissions.GetEmp_TotHRS(_FK_EmployeeId, _M_Date)

            If Not IsDBNull(dr("TOT_WORK_HRS")) Then
                _TOT_WORK_HRS = dr("TOT_WORK_HRS")
            End If
            If Not IsDBNull(dr("SCH_START_TIME")) Then
                _SCH_START_TIME = dr("SCH_START_TIME")
            End If
            Return Me
        End Function

        Public Function IsExistsFullDay(ByVal dtCurrent As DataTable, ByVal IsPermissionOneDay As Boolean, ByVal Fromdate As Date?, ByVal Todate As Date?, ByVal FromTime As Date?, ByVal ToTime As Date?, ByVal PermissionId As Integer,
                                        ByVal PermDate As DateTime) As Boolean

            Dim status As Boolean = False


            For Each dr As DataRow In dtCurrent.Rows
                If PermissionId = dr("PermissionId") Then
                    Continue For
                End If

                If Not IsDBNull(dr("IsFullDay")) AndAlso dr("IsFullDay") = "True" Then
                    If IsPermissionOneDay = True Then
                        If PermDate = dr("PermDate") Then
                            status = True
                        End If
                    End If
                End If
            Next
            Return status
        End Function

        Public Function IsExists(ByVal dtCurrent As DataTable, ByVal IsPermissionOneDay As Boolean, ByVal Fromdate As Date?, ByVal Todate As Date?, ByVal FromTime As Date?, ByVal ToTime As Date?, ByVal PermissionId As Integer,
                                ByVal PermDate As DateTime) As Boolean

            Dim status As Boolean
            Dim requestStatus As Integer
            Dim containRequestStatus As Boolean = False
            Dim columns As DataColumnCollection = dtCurrent.Columns
            If columns.Contains("FK_StatusId") Then
                containRequestStatus = True
            End If

            For Each dr As DataRow In dtCurrent.Rows
                If PermissionId = dr("PermissionId") Then
                    Continue For
                End If

                If containRequestStatus And Not IsDBNull(dr("FK_StatusId")) Then

                    requestStatus = Convert.ToInt32(dr("FK_StatusId"))
                    If (requestStatus = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByGeneralManager)) _
                        Or (requestStatus = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedByHumanResource)) _
                        Or (requestStatus = Convert.ToInt32(CtlCommon.RequestStatusEnum.RejectedbyManager)) Then
                        Continue For
                    End If
                End If

                If (IsPermissionOneDay) Then
                    FromTime = PermDate.AddHours(FromTime.Value.Hour).AddMinutes(FromTime.Value.Minute)
                    ToTime = PermDate.AddHours(ToTime.Value.Hour).AddMinutes(ToTime.Value.Minute)
                    If Fromdate >= dr("PermDate") Then
                        If (FromTime <> Nothing Or Not dr("FromTime") Is System.DBNull.Value) Then
                            If FromTime >= dr("FromTime") And FromTime < dr("ToTime") Or ToTime > dr("FromTime") And ToTime <= dr("ToTime") Then
                                If containRequestStatus And Not dr("FK_StatusId") Is System.DBNull.Value Then
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
                Else
                    If (Not dr("PermEndDate") Is System.DBNull.Value) Then
                        If Fromdate >= dr("PermDate") And Fromdate <= dr("PermEndDate") Or Todate >= dr("PermDate") And Todate <= dr("PermEndDate") Then
                            If (FromTime IsNot Nothing And (Not IsDBNull(dr("FromTime")))) Then
                                If FromTime >= dr("FromTime") And FromTime < dr("ToTime") Or ToTime > dr("FromTime") And ToTime <= dr("ToTime") Then
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
                    Else
                        If Fromdate = dr("PermDate") Or Todate = dr("PermDate") Then
                            If (FromTime <> Nothing And Not dr("FromTime") Is System.DBNull.Value) Then
                                If FromTime >= dr("FromTime") And FromTime < dr("ToTime") Or ToTime > dr("FromTime") And ToTime <= dr("ToTime") Then
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
                End If

            Next
            Return status
        End Function

        Public Function CheckaPermissionsMinMaxDays(ByVal PermTypeInfo As PermissionsTypes, ByVal FromTime As DateTime?,
                                             ByVal ToTime As DateTime?, ByVal PermDate As DateTime, ByVal IsFullyDay As Boolean, ByRef ErrorMsg As String) As Boolean

            Dim MinDuration As Integer
            Dim MaxDuration As Integer
            Dim AllowWhenInSufficient As Boolean
            Dim DeductBalanceFromOvertime As Boolean

            If Not PermTypeInfo Is Nothing Then
                MinDuration = PermTypeInfo.MinDuration
                MaxDuration = PermTypeInfo.MaxDuration
                DeductBalanceFromOvertime = PermTypeInfo.DeductBalanceFromOvertime
                AllowWhenInSufficient = PermTypeInfo.AllowWhenInSufficient
            Else
                MinDuration = 30
                MaxDuration = 300
                DeductBalanceFromOvertime = False
                AllowWhenInSufficient = False
            End If

            'If DeductBalanceFromOvertime = False Then
            Dim CurrentLeaveDuration As TimeSpan
            Dim ToTime1 As DateTime = PermDate.AddHours(ToTime.Value.Hour).AddMinutes(ToTime.Value.Minute)
            Dim FromTime1 As DateTime = PermDate.AddHours(FromTime.Value.Hour).AddMinutes(FromTime.Value.Minute)
            CurrentLeaveDuration = (ToTime1 - FromTime1)

            Dim Minutes = Math.Ceiling(CurrentLeaveDuration.TotalMinutes)
            If (Minutes < 0) Then
                Minutes = Minutes * -1
            End If

            If Not IsFullyDay Then
                '    If MaxDuration < 420 Then
                '        If SessionVariables.CultureInfo = "ar-JO" Then
                '            ErrorMsg = "الحد الادنى المسموح به لمغادرة اليوم الواحد هو 420"
                '        Else
                '            ErrorMsg = "The Minimum full day permission duration is 420"
                '        End If
                '        Return False
                '    End If
                'Else
                If AllowWhenInSufficient = False Then
                    If (Minutes < MinDuration) Then
                        If SessionVariables.CultureInfo = "ar-JO" Then
                            ErrorMsg = "الحد الادنى لمدة المغادرة المسموح به هو: " & MinDuration & " دقائق"
                        Else
                            ErrorMsg = "The Minimum permission duration is: " & MinDuration & " minutes"
                        End If
                        Return False
                    ElseIf (Minutes > MaxDuration) Then
                        If SessionVariables.CultureInfo = "ar-JO" Then
                            ErrorMsg = "الحد الاقصى لمدة المغادرة المسموح به هو: " & MaxDuration & " دقائق"
                        Else
                            ErrorMsg = "The Maximum permission duration is: " & MaxDuration & " minutes"
                        End If
                        Return False
                    End If
                End If

            End If
            'End If
            Return True
        End Function

        Public Function CalculateOffAndHolidaysDays(ByVal EmployeeId As Integer, ByVal StartDate As DateTime?, ByVal EndDate As DateTime?, ByVal PermTypeInfo As PermissionsTypes, ByVal PermDate As DateTime, ByVal IsPermissionOneday As Boolean) As Integer

            objLeavesTypes = New LeavesTypes

            Dim PermStartDate As DateTime
            Dim PermEndDate As DateTime
            Dim objApp_Settings As New APP_Settings

            If (IsPermissionOneday) Then
                PermStartDate = PermDate
                PermEndDate = PermDate
            Else
                PermStartDate = StartDate
                PermEndDate = IIf(EndDate = DateTime.MinValue, StartDate, EndDate)
            End If

            Dim LeaveID As Integer
            LeaveID = PermTypeInfo.FK_LeaveIdDeductBalance

            If Not LeaveID = 0 Then
                objLeavesTypes.LeaveId = LeaveID
                objLeavesTypes.GetByPK()
            End If

            'Create Table For Days, IsOffDay, IsHoliday
            Dim dtOffDays As DataTable = New DataTable
            dtOffDays = New DataTable
            Dim dcColumn As DataColumn
            dcColumn = New DataColumn("Days")
            dcColumn.DataType = GetType(Date)
            dtOffDays.Columns.Add(dcColumn)
            dcColumn = New DataColumn("IsOffDay")
            dcColumn.DataType = GetType(Boolean)
            dtOffDays.Columns.Add(dcColumn)
            dcColumn = New DataColumn("IsHoliday")
            dcColumn.DataType = GetType(Boolean)
            dtOffDays.Columns.Add(dcColumn)

            While (PermStartDate <= PermEndDate)
                Dim drNewRow = dtOffDays.NewRow
                drNewRow("Days") = StartDate
                If EmployeeId <> 0 Then
                    Dim objWorkSchedule As New WorkSchedule
                    objWorkSchedule.EmployeeId = EmployeeId
                    Dim dtActiveSchedule = objWorkSchedule.GetEmployeeActiveScheduleByEmpId(StartDate)

                    If (dtActiveSchedule IsNot Nothing And dtActiveSchedule.Rows.Count > 0) Then
                        ' Schedule is Normal
                        If dtActiveSchedule.Rows(0)("ScheduleType") = 1 Then
                            Dim DayId = 0
                            objWorkSchedule_NormalTime = New WorkSchedule_NormalTime
                            objWorkSchedule_NormalTime.FK_ScheduleId = dtActiveSchedule.Rows(0)("ScheduleId")
                            Dim ScheduleTimes = objWorkSchedule_NormalTime.GetAll()

                            Select Case PermStartDate.DayOfWeek().ToString()
                                Case "Monday"
                                    DayId = 2
                                Case "Tuesday"
                                    DayId = 3
                                Case "Wednesday"
                                    DayId = 4
                                Case "Thursday"
                                    DayId = 5
                                Case "Friday"
                                    DayId = 6
                                Case "Saturday"
                                    DayId = 7
                                Case "Sunday"
                                    DayId = 1
                            End Select

                            Dim drScheduleTime = ScheduleTimes.Select("DayId =" & DayId)(0)
                            If (drScheduleTime("IsOffDay")) Then
                                If Not LeaveID = 0 Then
                                    If objLeavesTypes.ExcludeOffDays = False Then
                                        drNewRow("IsOffDay") = False
                                    Else
                                        drNewRow("IsOffDay") = True
                                    End If
                                Else
                                    drNewRow("IsOffDay") = True
                                End If
                            Else
                                drNewRow("IsOffDay") = False
                            End If

                            ' Schedule is Flexible
                        ElseIf dtActiveSchedule.Rows(0)("ScheduleType") = 2 Then
                            Dim DayId = 0

                            objWorkSchedule_Flexible = New WorkSchedule_Flexible
                            objWorkSchedule_Flexible.FK_ScheduleId = dtActiveSchedule.Rows(0)("ScheduleId")
                            Dim FlexibleSchedule = objWorkSchedule_Flexible.GetAll()

                            Select Case PermStartDate.DayOfWeek().ToString()
                                Case "Monday"
                                    DayId = 2
                                Case "Tuesday"
                                    DayId = 3
                                Case "Wednesday"
                                    DayId = 4
                                Case "Thursday"
                                    DayId = 5
                                Case "Friday"
                                    DayId = 6
                                Case "Saturday"
                                    DayId = 7
                                Case "Sunday"
                                    DayId = 1
                            End Select

                            Dim drFlexibleSchedule = FlexibleSchedule.Select("DayId =" & DayId)(0)
                            If (drFlexibleSchedule("IsOffDay")) Then
                                If Not LeaveID = 0 Then
                                    If objLeavesTypes.ExcludeOffDays = False Then
                                        drNewRow("IsOffDay") = False
                                    Else
                                        drNewRow("IsOffDay") = True
                                    End If
                                Else
                                    drNewRow("IsOffDay") = True
                                End If
                            Else
                                drNewRow("IsOffDay") = False
                            End If
                        ElseIf dtActiveSchedule.Rows(0)("ScheduleType") = 3 Then
                            objEmp_Shifts = New Emp_Shifts
                            objEmp_Shifts.FK_EmployeeId = EmployeeId
                            objEmp_Shifts.WorkDate = PermStartDate
                            Dim AdvancedSchedule = objEmp_Shifts.GetShiftsByDate()
                            If (AdvancedSchedule IsNot Nothing And AdvancedSchedule.Rows.Count > 0) Then
                                drNewRow("IsOffDay") = False
                            Else
                                'FadiH:: check event when no shift
                                With objApp_Settings
                                    .GetByPK()
                                    If .NoShiftShcedule = Convert.ToInt32(CtlCommon.NoShiftSchedule.ConsideritOffDay) Then
                                        drNewRow("IsOffDay") = True
                                    ElseIf .NoShiftShcedule = Convert.ToInt32(CtlCommon.NoShiftSchedule.ConsiderDefaultSchedule) Then
                                        drNewRow("IsOffDay") = CheckDefaultScheduleIsOffDay(StartDate)
                                    End If
                                End With
                            End If
                        ElseIf dtActiveSchedule.Rows(0)("ScheduleType") = 5 Then
                            Dim DayId = 0

                            objWorkSchedule_Open = New WorkSchedule_Open
                            objWorkSchedule_Open.FK_ScheduleId = dtActiveSchedule.Rows(0)("ScheduleId")
                            Dim FlexibleSchedule = objWorkSchedule_Open.GetAll()

                            Select Case PermStartDate.DayOfWeek().ToString()
                                Case "Monday"
                                    DayId = 2
                                Case "Tuesday"
                                    DayId = 3
                                Case "Wednesday"
                                    DayId = 4
                                Case "Thursday"
                                    DayId = 5
                                Case "Friday"
                                    DayId = 6
                                Case "Saturday"
                                    DayId = 7
                                Case "Sunday"
                                    DayId = 1
                            End Select

                            Dim drFlexibleSchedule = FlexibleSchedule.Select("DayId =" & DayId)(0)
                            If (drFlexibleSchedule("IsOffDay")) Then
                                If Not LeaveID = 0 Then
                                    If objLeavesTypes.ExcludeOffDays = False Then
                                        drNewRow("IsOffDay") = False
                                    Else
                                        drNewRow("IsOffDay") = True
                                    End If
                                Else
                                    drNewRow("IsOffDay") = True
                                End If
                            Else
                                drNewRow("IsOffDay") = False
                            End If
                        Else
                            If Not LeaveID = 0 Then
                                If objLeavesTypes.ExcludeOffDays = False Then
                                    drNewRow("IsOffDay") = False
                                Else
                                    drNewRow("IsOffDay") = True
                                End If
                            Else
                                drNewRow("IsOffDay") = True
                            End If
                        End If
                    End If

                Else
                    drNewRow("IsOffDay") = False
                End If

                'Count Holiday days
                objHoliday = New Holiday
                With objHoliday
                    .HolidayDay = StartDate
                    .EmployeeId = EmployeeId
                    Dim HolidayDaysCount = .EmpGetHolidays
                    If (HolidayDaysCount > 0) Then
                        If Not LeaveID = 0 Then
                            If objLeavesTypes.ExcludeHolidays = False Then
                                drNewRow("Isholiday") = False
                            Else
                                drNewRow("Isholiday") = True
                            End If
                        Else
                            drNewRow("Isholiday") = True
                        End If
                    Else
                        drNewRow("Isholiday") = False
                    End If
                End With

                PermStartDate = PermStartDate.AddDays(1)

                dtOffDays.Rows.Add(drNewRow)
            End While

            If (dtOffDays IsNot Nothing And dtOffDays.Rows.Count > 0) Then
                Dim IsOffDayOrHolidayColumn As DataColumn
                IsOffDayOrHolidayColumn = New DataColumn("IsOffDayOrHoliday")
                IsOffDayOrHolidayColumn.DataType = GetType(Boolean)
                dtOffDays.Columns.Add(IsOffDayOrHolidayColumn)
                For Each drRow As DataRow In dtOffDays.Rows
                    drRow("IsOffDayOrHoliday") = drRow("IsOffDay") Or drRow("Isholiday")
                Next
                Return dtOffDays.Select("IsOffDayOrHoliday = True").Length
            End If
        End Function

        Function CheckDefaultScheduleIsOffDay(ByVal StartDate As Date) As Boolean
            Dim objDefaultWorkSchedule As New WorkSchedule
            objDefaultWorkSchedule = objDefaultWorkSchedule.GetByDefault()
            If objDefaultWorkSchedule.ScheduleType = Convert.ToInt32(CtlCommon.ScheduleType.Normal) Then
                Dim DayId = 0
                objWorkSchedule_NormalTime = New WorkSchedule_NormalTime
                objWorkSchedule_NormalTime.FK_ScheduleId = objDefaultWorkSchedule.ScheduleId
                Dim ScheduleTimes = objWorkSchedule_NormalTime.GetAll()

                Select Case StartDate.DayOfWeek().ToString()
                    Case "Monday"
                        DayId = 2
                    Case "Tuesday"
                        DayId = 3
                    Case "Wednesday"
                        DayId = 4
                    Case "Thursday"
                        DayId = 5
                    Case "Friday"
                        DayId = 6
                    Case "Saturday"
                        DayId = 7
                    Case "Sunday"
                        DayId = 1
                End Select
                Dim drScheduleTime = ScheduleTimes.Select("DayId =" & DayId)(0)
                If (drScheduleTime("IsOffDay")) Then
                    Return True
                Else
                    Return False
                End If
            ElseIf objDefaultWorkSchedule.ScheduleType = Convert.ToInt32(CtlCommon.ScheduleType.Flexible) Then
                Dim DayId = 0

                objWorkSchedule_Flexible = New WorkSchedule_Flexible
                objWorkSchedule_Flexible.FK_ScheduleId = objDefaultWorkSchedule.ScheduleId
                Dim FlexibleSchedule = objWorkSchedule_Flexible.GetAll()

                Select Case StartDate.DayOfWeek().ToString()
                    Case "Monday"
                        DayId = 2
                    Case "Tuesday"
                        DayId = 3
                    Case "Wednesday"
                        DayId = 4
                    Case "Thursday"
                        DayId = 5
                    Case "Friday"
                        DayId = 6
                    Case "Saturday"
                        DayId = 7
                    Case "Sunday"
                        DayId = 1
                End Select

                Dim drFlexibleSchedule = FlexibleSchedule.Select("DayId =" & DayId)(0)
                If (drFlexibleSchedule("IsOffDay")) Then
                    Return True
                Else
                    Return False
                End If
            End If
        End Function

        Public Function CheckAllowedOccuranceForDayWeekMonth(ByVal EmployeeId As Integer, ByVal StartDate As DateTime?, ByVal EndDate As DateTime?, ByVal PermDate As DateTime, ByVal PermTypeInfo As PermissionsTypes,
                                                      ByVal IsPermissionOneDay As Boolean, ByVal PermTypeId As Integer, ByVal PermissionId As Integer, ByRef ErrorMsg As String) As Boolean
            Dim drDuration As DataRow
            Dim AllowWhenInSufficient As Boolean = False

            If Not PermTypeInfo Is Nothing Then
                AllowWhenInSufficient = PermTypeInfo.AllowWhenInSufficient
            Else
                AllowWhenInSufficient = False
            End If

            With objPermissionTypeOccurance
                .FK_PermId = PermTypeId
                Dim dtPermissionOccurance = .GetByPK()

                If (dtPermissionOccurance IsNot Nothing AndAlso dtPermissionOccurance.Rows.Count > 0) Then

                    If (IsPermissionOneDay) Then
                        'Check validation for a week
                        If (dtPermissionOccurance.Select("FK_DurationId = 2").Length > 0) Then
                            drDuration = dtPermissionOccurance.Select("FK_DurationId = 2")(0)

                            'Get Occurance Per Week for this employee
                            Dim objEmpPermissions2 As New Emp_Permissions
                            With objEmpPermissions2
                                .PermDate = PermDate
                                .FK_EmployeeId = EmployeeId
                                ._FK_PermId = PermTypeId
                                Dim PermissionOccurance As Integer
                                If PermissionId = 0 Then
                                    PermissionOccurance = .GetOccuranceForWeek()
                                Else
                                    PermissionOccurance = .GetOccuranceForWeek() - 1 '--To Exclude The Updated Permission
                                End If
                                If AllowWhenInSufficient = False Then
                                    If (PermissionOccurance >= drDuration("MaximumOccur")) Then
                                        If SessionVariables.CultureInfo = "ar-JO" Then
                                            ErrorMsg = "المغادرات المسموحة خلال الاسبوع: " & drDuration("MaximumOccur") & " مغادرات"
                                        Else
                                            ErrorMsg = "Allowed permissions per week: " & drDuration("MaximumOccur") & " permissions"
                                        End If

                                        Return False
                                    End If
                                End If
                            End With
                        End If

                        'Check Validation for a Month
                        If (dtPermissionOccurance.Select("FK_DurationId = 3").Length > 0) Then
                            drDuration = dtPermissionOccurance.Select("FK_DurationId = 3")(0)

                            'Get Occurance Per Month for this employee
                            Dim objEmpPermissions2 As New Emp_Permissions
                            With objEmpPermissions2
                                .PermDate = PermDate
                                .FK_EmployeeId = EmployeeId
                                ._FK_PermId = PermTypeId
                                Dim LeaveMonthOccurance As Integer
                                If PermissionId = 0 Then
                                    LeaveMonthOccurance = .GetOccuranceForMonth()
                                Else
                                    LeaveMonthOccurance = .GetOccuranceForMonth() - 1 '--To Exclude The Updated Permission
                                End If

                                If AllowWhenInSufficient = False Then
                                    If (LeaveMonthOccurance >= drDuration("MaximumOccur")) Then
                                        If SessionVariables.CultureInfo = "ar-JO" Then
                                            ErrorMsg = "المغادرات المسموحة خلال شهر: " & drDuration("MaximumOccur") & " مغادرات"
                                        Else
                                            ErrorMsg = "Allowed permissions per month: " & drDuration("MaximumOccur") & " permissions"
                                        End If
                                        Return False
                                    End If
                                End If

                            End With
                        End If

                        'Check Validation for a Year
                        If (dtPermissionOccurance.Select("FK_DurationId = 4").Length > 0) Then
                            drDuration = dtPermissionOccurance.Select("FK_DurationId = 4")(0)

                            'Get Occurance Per Year for this employee
                            Dim objEmpPermissions2 As New Emp_Permissions
                            With objEmpPermissions2
                                .PermDate = PermDate
                                .FK_EmployeeId = EmployeeId
                                ._FK_PermId = PermTypeId

                                Dim LeaveMonthOccurance As Integer
                                If PermissionId = 0 Then
                                    LeaveMonthOccurance = .GetOccuranceForYear()
                                Else
                                    LeaveMonthOccurance = .GetOccuranceForYear() - 1 '--To Exclude The Updated Permission
                                End If

                                If AllowWhenInSufficient = False Then
                                    If (LeaveMonthOccurance >= drDuration("MaximumOccur")) Then
                                        If SessionVariables.CultureInfo = "ar-JO" Then
                                            ErrorMsg = "المغادرات المسموحة خلال سنة: " & drDuration("MaximumOccur") & " مغادرات"
                                        Else
                                            ErrorMsg = "Allowed leaves per year: " & drDuration("MaximumOccur") & " Permission(s)"
                                        End If

                                        Return False
                                    End If
                                End If
                            End With
                        End If

                        'Check Validation for a Day
                        If (dtPermissionOccurance.Select("FK_DurationId = 1").Length > 0) Then
                            drDuration = dtPermissionOccurance.Select("FK_DurationId = 1")(0)

                            'Get Occurance Per Year for this employee
                            Dim objEmpPermissions2 As New Emp_Permissions
                            With objEmpPermissions2
                                .PermDate = PermDate
                                .FK_EmployeeId = EmployeeId
                                ._FK_PermId = PermTypeId

                                Dim LeaveMonthOccurance As Integer
                                If PermissionId = 0 Then
                                    LeaveMonthOccurance = .GetOccuranceForDay()
                                Else
                                    LeaveMonthOccurance = .GetOccuranceForDay() - 1 '--To Exclude The Updated Permission
                                End If

                                If AllowWhenInSufficient = False Then
                                    If (LeaveMonthOccurance >= drDuration("MaximumOccur")) Then
                                        If SessionVariables.CultureInfo = "ar-JO" Then
                                            ErrorMsg = "المغادرات المسموحة خلال اليوم: " & drDuration("MaximumOccur") & " مغادرات"
                                        Else
                                            ErrorMsg = "Allowed leaves per day: " & drDuration("MaximumOccur") & " Permission(s)"
                                        End If

                                        Return False
                                    End If
                                End If
                            End With
                        End If
                    Else 'From date to Date
                        'Check validation for a week
                        If (dtPermissionOccurance.Select("FK_DurationId = 2").Length > 0) Then
                            drDuration = dtPermissionOccurance.Select("FK_DurationId = 2")(0)

                            'Get Occurance Per Week for this employee
                            Dim objEmpPermissions2 As New Emp_Permissions
                            With objEmpPermissions2
                                .PermDate = StartDate
                                .FK_EmployeeId = EmployeeId
                                ._FK_PermId = PermTypeId

                                Dim PermissionOccurance As Integer
                                If PermissionId = 0 Then
                                    PermissionOccurance = .GetOccuranceForWeek()
                                Else
                                    PermissionOccurance = .GetOccuranceForWeek() - 1 '--To Exclude The Updated Permission
                                End If
                                Dim DaysTimeSpan As TimeSpan
                                DaysTimeSpan = EndDate - StartDate

                                If AllowWhenInSufficient = False Then
                                    If (PermissionOccurance + DaysTimeSpan.Days + 1 > drDuration("MaximumOccur")) Then
                                        If SessionVariables.CultureInfo = "ar-JO" Then
                                            ErrorMsg = "المغادرات المسموحة خلال اسبوع: " & drDuration("MaximumOccur") & "مغادرات"
                                        Else
                                            ErrorMsg = "Allowed permissions per week: " & drDuration("MaximumOccur") & " permissions"
                                        End If

                                        Return False
                                    End If
                                End If
                            End With
                        End If

                        'Check Validation for a Month
                        If (dtPermissionOccurance.Select("FK_DurationId = 3").Length > 0) Then
                            drDuration = dtPermissionOccurance.Select("FK_DurationId = 3")(0)

                            'Get Occurance Per Month for this employee
                            Dim objEmpPermissions2 As New Emp_Permissions
                            With objEmpPermissions2
                                .PermDate = StartDate
                                .FK_EmployeeId = EmployeeId
                                ._FK_PermId = PermTypeId

                                Dim LeaveMonthOccurance As Integer
                                If PermissionId = 0 Then
                                    LeaveMonthOccurance = .GetOccuranceForMonth()
                                Else
                                    LeaveMonthOccurance = .GetOccuranceForMonth() - 1 '--To Exclude The Updated Permission
                                End If
                                Dim DaysTimeSpan As TimeSpan
                                DaysTimeSpan = EndDate - StartDate

                                If AllowWhenInSufficient = False Then
                                    If (LeaveMonthOccurance + DaysTimeSpan.Days + 1 > drDuration("MaximumOccur")) Then
                                        If SessionVariables.CultureInfo = "ar-JO" Then
                                            ErrorMsg = "المغادرات المسموحة خلال شهر: " & drDuration("MaximumOccur") & "مغادرات"
                                        Else
                                            ErrorMsg = "Allowed permissions per month: " & drDuration("MaximumOccur") & " permissions"
                                        End If

                                        Return False
                                    End If
                                End If
                            End With
                        End If

                        'Check Validation for a Year
                        If (dtPermissionOccurance.Select("FK_DurationId = 4").Length > 0) Then
                            drDuration = dtPermissionOccurance.Select("FK_DurationId = 4")(0)

                            'Get Occurance Per Year for this employee
                            Dim objEmpPermissions2 As New Emp_Permissions
                            With objEmpPermissions2
                                .PermDate = StartDate
                                .FK_EmployeeId = EmployeeId
                                ._FK_PermId = PermTypeId

                                Dim LeaveMonthOccurance As Integer
                                If PermissionId = 0 Then
                                    LeaveMonthOccurance = .GetOccuranceForYear()
                                Else
                                    LeaveMonthOccurance = .GetOccuranceForYear() - 1 '--To Exclude The Updated Permission
                                End If
                                Dim DaysTimeSpan As TimeSpan
                                DaysTimeSpan = EndDate - StartDate

                                If AllowWhenInSufficient = False Then
                                    If (LeaveMonthOccurance + DaysTimeSpan.Days + 1 > drDuration("MaximumOccur")) Then
                                        If SessionVariables.CultureInfo = "ar-JO" Then
                                            ErrorMsg = "المغادرات المسموحة خلال سنة: " & drDuration("MaximumOccur") & "مغادرات"
                                        Else
                                            ErrorMsg = "Allowed permissions per year : " & drDuration("MaximumOccur") & " permissions"
                                        End If
                                        Return False
                                    End If
                                End If
                            End With
                        End If

                        'Check Validation for a Day
                        If (dtPermissionOccurance.Select("FK_DurationId = 1").Length > 0) Then
                            drDuration = dtPermissionOccurance.Select("FK_DurationId = 1")(0)

                            'Get Occurance Per Year for this employee
                            Dim objEmpPermissions2 As New Emp_Permissions
                            With objEmpPermissions2
                                .PermDate = StartDate
                                .FK_EmployeeId = EmployeeId
                                ._FK_PermId = PermTypeId

                                Dim LeaveMonthOccurance As Integer
                                If PermissionId = 0 Then
                                    LeaveMonthOccurance = .GetOccuranceForDay()
                                Else
                                    LeaveMonthOccurance = .GetOccuranceForDay() - 1 '--To Exclude The Updated Permission
                                End If
                                Dim DaysTimeSpan As TimeSpan
                                DaysTimeSpan = EndDate - StartDate

                                If AllowWhenInSufficient = False Then
                                    If (LeaveMonthOccurance + DaysTimeSpan.Days + 1 > drDuration("MaximumOccur")) Then
                                        If SessionVariables.CultureInfo = "ar-JO" Then
                                            ErrorMsg = "المغادرات المسموحة خلال يوم: " & drDuration("MaximumOccur") & "مغادرات"
                                        Else
                                            ErrorMsg = "Allowed permissions per day : " & drDuration("MaximumOccur") & " permissions"
                                        End If
                                        Return False
                                    End If
                                End If
                            End With
                        End If
                    End If
                End If
            End With
            Return True
        End Function

        Public Function CheckAllowedDurationForDayWeekMonth(ByVal EmployeeId As Integer, ByVal StartDate As DateTime?, ByVal EndDate As DateTime?, ByVal FromTime As DateTime?, ByVal ToTime As DateTime?,
                                                     ByVal PermDate As DateTime, ByVal IsPermissionOnedDay As Boolean, ByVal PermTypeInfo As PermissionsTypes, ByVal PermTypeId As Integer, ByVal PermissionId As Integer, ByRef ErrorMessage As String) As Boolean

            Dim drDuration As DataRow
            objRamadanPeriod = New RamadanPeriod
            Dim dtCheck_HasStudyOrNursing As DataTable
            dtCheck_HasStudyOrNursing = New DataTable
            Dim HasStudyOrNursing As Boolean = False
            Dim AllowWhenInSufficient As Boolean


            If Not PermTypeInfo Is Nothing Then
                AllowWhenInSufficient = PermTypeInfo.AllowWhenInSufficient
            Else
                AllowWhenInSufficient = False
            End If

            With objPermissionTypeDuration
                .FK_PermId = PermTypeId
                Dim dtPermissionDuration As New DataTable
                dtPermissionDuration = .GetByPK()
                If (dtPermissionDuration IsNot Nothing AndAlso dtPermissionDuration.Rows.Count > 0) Then
                    'Check validation for allowed duration

                    If (IsPermissionOnedDay) Then
                        If ((FromTime.HasValue) AndAlso (ToTime.HasValue)) Then
                            'Check validation for a week

                            dtCheck_HasStudyOrNursing = objDALEmp_Permissions.Check_HasStudyOrNursing(EmployeeId, PermDate, Nothing, Nothing)
                            If (dtCheck_HasStudyOrNursing IsNot Nothing AndAlso dtCheck_HasStudyOrNursing.Rows.Count > 0) Then
                                HasStudyOrNursing = dtCheck_HasStudyOrNursing.Rows(0)("HasStudyOrNursing")
                            End If

                            If (dtPermissionDuration.Select("FK_DurationId = 2").Length > 0) Then
                                drDuration = dtPermissionDuration.Select("FK_DurationId = 2")(0)
                                'Get Occurance Per Week for this employee
                                objEmpPermissions = New Emp_Permissions
                                With objEmpPermissions
                                    .PermDate = PermDate
                                    .FK_EmployeeId = EmployeeId
                                    ._FK_PermId = PermTypeId
                                    Dim PermissionDuration = .GetDurationForWeek()
                                    Dim CurrentDuration As TimeSpan
                                    If PermissionId = 0 Then
                                        CurrentDuration = (ToTime - FromTime)
                                    Else
                                        objEmpPermissions.PermissionId = PermissionId
                                        objEmpPermissions.GetByPK()
                                        CurrentDuration = (objEmpPermissions.ToTime - objEmpPermissions.FromTime)
                                    End If
                                    'CurrentDuration = (PermDate.AddHours(ToTime.Value.Hour).AddMinutes(ToTime.Value.Minute) - PermDate.AddHours(FromTime.Value.Hour).AddMinutes(FromTime.Value.Minute))
                                    Dim DurationMinutes = (Math.Ceiling(CurrentDuration.TotalMinutes))
                                    If (DurationMinutes < 0) Then
                                        DurationMinutes = DurationMinutes * -1
                                    End If
                                    If Not PermissionId = 0 Then
                                        PermissionDuration = PermissionDuration - DurationMinutes
                                    End If

                                    If Not (drDuration("MaximumDuration_WithStudyNursing")) Is DBNull.Value Then
                                        If (PermissionDuration + DurationMinutes > drDuration("MaximumDuration_WithStudyNursing")) Then
                                            If SessionVariables.CultureInfo = "ar-JO" Then
                                                ErrorMessage = "الفترة المسموحة في وجود مغادرة رضاعة او دراسة : " & drDuration("MaximumDuration_WithStudyNursing") & " دقائق"
                                            Else
                                                ErrorMessage = "Allowed duration when nursing or study permission : " & drDuration("MaximumDuration_WithStudyNursing") & " minutes"
                                            End If
                                            Return False
                                        End If
                                    End If


                                    If AllowWhenInSufficient = False Then
                                        '-----------Check Ramadan Day------------'
                                        objRamadanPeriod.SearchDate = PermDate
                                        objRamadanPeriod.Get_IsRamadan()

                                        If objRamadanPeriod.IsRamadanDay = True Then
                                            If (PermissionDuration + DurationMinutes > drDuration("MaximumRamadanDuration")) Then
                                                If SessionVariables.CultureInfo = "ar-JO" Then
                                                    ErrorMessage = "الفترة المسموحة في رمضان خلال اسبوع: " & drDuration("MaximumRamadanDuration") & " دقائق"
                                                Else
                                                    ErrorMessage = "Allowed durations in Ramadan per week: " & drDuration("MaximumRamadanDuration") & " minutes"
                                                End If

                                                Return False
                                            End If
                                        Else
                                            If (PermissionDuration + DurationMinutes > drDuration("MaximumDuration")) Then
                                                If SessionVariables.CultureInfo = "ar-JO" Then
                                                    ErrorMessage = "الفترة المسموحة خلال اسبوع: " & drDuration("MaximumDuration") & " دقائق"
                                                Else
                                                    ErrorMessage = "Allowed durations per week: " & drDuration("MaximumDuration") & " minutes"
                                                End If

                                                Return False
                                            End If
                                        End If
                                        '-----------Check Ramadan Day------------'
                                    End If

                                End With
                            End If

                            'Check Validation for a Month
                            If (dtPermissionDuration.Select("FK_DurationId = 3").Length > 0) Then
                                drDuration = dtPermissionDuration.Select("FK_DurationId = 3")(0)

                                'Get Occurance Per Month for this employee
                                objEmpPermissions = New Emp_Permissions
                                With objEmpPermissions
                                    .PermDate = PermDate
                                    .FK_EmployeeId = EmployeeId
                                    ._FK_PermId = PermTypeId
                                    Dim PermissionDuration = .GetDurationForMonth()
                                    Dim CurrentDuration As TimeSpan
                                    If PermissionId = 0 Then
                                        CurrentDuration = (ToTime - FromTime)
                                    Else
                                        objEmpPermissions.PermissionId = PermissionId
                                        objEmpPermissions.GetByPK()
                                        CurrentDuration = (objEmpPermissions.ToTime - objEmpPermissions.FromTime)
                                    End If
                                    Dim DurationMinutes = (Math.Ceiling(CurrentDuration.TotalMinutes))
                                    If (DurationMinutes < 0) Then
                                        DurationMinutes = DurationMinutes * -1
                                    End If
                                    If Not PermissionId = 0 Then
                                        PermissionDuration = PermissionDuration - DurationMinutes
                                    End If


                                    If Not (drDuration("MaximumDuration_WithStudyNursing")) Is DBNull.Value Then
                                        If (PermissionDuration + DurationMinutes > drDuration("MaximumDuration_WithStudyNursing")) Then
                                            If SessionVariables.CultureInfo = "ar-JO" Then
                                                ErrorMessage = "الفترة المسموحة في وجود مغادرة رضاعة او دراسة : " & drDuration("MaximumDuration_WithStudyNursing") & " دقائق"
                                            Else
                                                ErrorMessage = "Allowed duration when nursing or study permission : " & drDuration("MaximumDuration_WithStudyNursing") & " minutes"
                                            End If
                                            Return False
                                        End If
                                    End If

                                    If AllowWhenInSufficient = False Then
                                        '-----------Check Ramadan Day------------'
                                        objRamadanPeriod.SearchDate = PermDate
                                        objRamadanPeriod.Get_IsRamadan()

                                        If objRamadanPeriod.IsRamadanDay = True Then
                                            If (PermissionDuration + DurationMinutes > drDuration("MaximumRamadanDuration")) Then

                                                If SessionVariables.CultureInfo = "ar-JO" Then
                                                    ErrorMessage = "الفترة المسموحة في رمضان خلال شهر: " & drDuration("MaximumRamadanDuration") & " دقائق"
                                                Else
                                                    ErrorMessage = "Allowed durations in Ramadan per month: " & drDuration("MaximumRamadanDuration") & " minutes"
                                                End If
                                                Return False
                                            End If
                                        Else
                                            If (PermissionDuration + DurationMinutes > drDuration("MaximumDuration")) Then

                                                If SessionVariables.CultureInfo = "ar-JO" Then
                                                    ErrorMessage = "الفترة المسموحة خلال شهر: " & drDuration("MaximumDuration") & " دقائق"
                                                Else
                                                    ErrorMessage = "Allowed durations per month: " & drDuration("MaximumDuration") & " minutes"
                                                End If
                                                Return False
                                            End If
                                        End If
                                        '-----------Check Ramadan Day------------'
                                    End If



                                End With
                            End If

                            'Check Validation for a Year
                            If (dtPermissionDuration.Select("FK_DurationId = 4").Length > 0) Then
                                drDuration = dtPermissionDuration.Select("FK_DurationId = 4")(0)

                                'Get Occurance Per Year for this employee
                                objEmpPermissions = New Emp_Permissions
                                With objEmpPermissions
                                    .PermDate = PermDate
                                    .FK_EmployeeId = EmployeeId
                                    ._FK_PermId = PermTypeId
                                    Dim PermissionDuration = .GetDurationForYear()
                                    Dim CurrentDuration As TimeSpan
                                    If PermissionId = 0 Then
                                        CurrentDuration = (ToTime - FromTime)
                                    Else
                                        objEmpPermissions.PermissionId = PermissionId
                                        objEmpPermissions.GetByPK()
                                        CurrentDuration = (objEmpPermissions.ToTime - objEmpPermissions.FromTime)
                                    End If
                                    Dim DurationMinutes = (Math.Ceiling(CurrentDuration.TotalMinutes))
                                    If (DurationMinutes < 0) Then
                                        DurationMinutes = DurationMinutes * -1
                                    End If
                                    If Not PermissionId = 0 Then
                                        PermissionDuration = PermissionDuration - DurationMinutes
                                    End If

                                    If Not (drDuration("MaximumDuration_WithStudyNursing")) Is DBNull.Value Then
                                        If (PermissionDuration + DurationMinutes > drDuration("MaximumDuration_WithStudyNursing")) Then
                                            If SessionVariables.CultureInfo = "ar-JO" Then
                                                ErrorMessage = "الفترة المسموحة في وجود مغادرة رضاعة او دراسة : " & drDuration("MaximumDuration_WithStudyNursing") & " دقائق"
                                            Else
                                                ErrorMessage = "Allowed duration when nursing or study permission : " & drDuration("MaximumDuration_WithStudyNursing") & " minutes"
                                            End If
                                            Return False
                                        End If
                                    End If

                                    If AllowWhenInSufficient = False Then
                                        '-----------Check Ramadan Day------------'
                                        objRamadanPeriod.SearchDate = PermDate
                                        objRamadanPeriod.Get_IsRamadan()

                                        If objRamadanPeriod.IsRamadanDay = True Then
                                            If (PermissionDuration + DurationMinutes > drDuration("MaximumRamadanDuration")) Then
                                                If SessionVariables.CultureInfo = "ar-JO" Then
                                                    ErrorMessage = "الفترة المسموحة في رمضان خلال سنة: " & drDuration("MaximumRamadanDuration") & " دقائق"
                                                Else
                                                    ErrorMessage = "Allowed durations In Ramadan per year: " & drDuration("MaximumRamadanDuration") & " minutes"
                                                End If
                                                Return False
                                            End If
                                        Else
                                            If (PermissionDuration + DurationMinutes > drDuration("MaximumDuration")) Then
                                                If SessionVariables.CultureInfo = "ar-JO" Then
                                                    ErrorMessage = "الفترة المسموحة خلال سنة: " & drDuration("MaximumDuration") & " دقائق"
                                                Else
                                                    ErrorMessage = "Allowed durations per year: " & drDuration("MaximumDuration") & " minutes"
                                                End If
                                                Return False
                                            End If
                                        End If
                                        '-----------Check Ramadan Day------------'
                                    End If


                                End With
                            End If

                            'Check Validation for a Day
                            If (dtPermissionDuration.Select("FK_DurationId = 1").Length > 0) Then
                                drDuration = dtPermissionDuration.Select("FK_DurationId = 1")(0)

                                'Get Occurance Per Year for this employee
                                objEmpPermissions = New Emp_Permissions
                                With objEmpPermissions
                                    .PermDate = PermDate
                                    .FK_EmployeeId = EmployeeId
                                    ._FK_PermId = PermTypeId
                                    Dim PermissionDuration = .GetDurationForDay()
                                    Dim CurrentDuration As TimeSpan
                                    If PermissionId = 0 Then
                                        CurrentDuration = (ToTime - FromTime)
                                    Else
                                        objEmpPermissions.PermissionId = PermissionId
                                        objEmpPermissions.GetByPK()
                                        'CurrentDuration = (objEmpPermissions.ToTime - objEmpPermissions.FromTime)
                                        CurrentDuration = (ToTime - FromTime)
                                    End If

                                    Dim DurationMinutes = (Math.Ceiling(CurrentDuration.TotalMinutes))
                                    If (DurationMinutes < 0) Then
                                        DurationMinutes = DurationMinutes * -1
                                    End If

                                    If Not PermissionId = 0 Then
                                        PermissionDuration = PermissionDuration - DurationMinutes
                                        If (PermissionDuration < 0) Then
                                            PermissionDuration = PermissionDuration * -1
                                        End If
                                    End If

                                    If Not (drDuration("MaximumDuration_WithStudyNursing")) Is DBNull.Value Then
                                        If (PermissionDuration + DurationMinutes > drDuration("MaximumDuration_WithStudyNursing")) Then
                                            If SessionVariables.CultureInfo = "ar-JO" Then
                                                ErrorMessage = "الفترة المسموحة في وجود مغادرة رضاعة او دراسة : " & drDuration("MaximumDuration_WithStudyNursing") & " دقائق"
                                            Else
                                                ErrorMessage = "Allowed duration when nursing or study permission : " & drDuration("MaximumDuration_WithStudyNursing") & " minutes"
                                            End If
                                            Return False
                                        End If
                                    End If

                                    If AllowWhenInSufficient = False Then
                                        '-----------Check Ramadan Day------------'
                                        objRamadanPeriod.SearchDate = PermDate
                                        objRamadanPeriod.Get_IsRamadan()

                                        If objRamadanPeriod.IsRamadanDay = True Then
                                            If (PermissionDuration + DurationMinutes > drDuration("MaximumRamadanDuration")) Then
                                                If SessionVariables.CultureInfo = "ar-JO" Then
                                                    ErrorMessage = "الفترة المسموحة في رمضان خلال يوم: " & drDuration("MaximumRamadanDuration") & " دقائق"
                                                Else
                                                    ErrorMessage = "Allowed durations in Ramadan per day: " & drDuration("MaximumRamadanDuration") & " minutes"
                                                End If
                                                Return False
                                            End If
                                        Else
                                            If (PermissionDuration + DurationMinutes > drDuration("MaximumDuration")) Then
                                                If SessionVariables.CultureInfo = "ar-JO" Then
                                                    ErrorMessage = "الفترة المسموحة خلال يوم: " & drDuration("MaximumDuration") & " دقائق"
                                                Else
                                                    ErrorMessage = "Allowed duration per day: " & drDuration("MaximumDuration") & " minutes"
                                                End If
                                                Return False
                                            End If
                                        End If
                                        '-----------Check Ramadan Day------------'
                                    End If

                                End With
                            End If
                        End If
                    ElseIf (Not IsPermissionOnedDay) Then
                        'Check validation for a week
                        dtCheck_HasStudyOrNursing = objDALEmp_Permissions.Check_HasStudyOrNursing(EmployeeId, StartDate, EndDate, Nothing)
                        If (dtCheck_HasStudyOrNursing IsNot Nothing AndAlso dtCheck_HasStudyOrNursing.Rows.Count > 0) Then
                            HasStudyOrNursing = dtCheck_HasStudyOrNursing.Rows(0)("HasStudyOrNursing")
                        End If

                        If (dtPermissionDuration.Select("FK_DurationId = 2").Length > 0) Then
                            drDuration = dtPermissionDuration.Select("FK_DurationId = 2")(0)

                            'Get Occurance Per Week for this employee
                            objEmpPermissions = New Emp_Permissions
                            With objEmpPermissions
                                .PermDate = PermDate
                                .FK_EmployeeId = EmployeeId
                                ._FK_PermId = PermTypeId
                                Dim PermissionDuration = .GetDurationForWeek()
                                Dim CurrentDuration As TimeSpan
                                If PermissionId = 0 Then
                                    CurrentDuration = (ToTime - FromTime)
                                Else
                                    objEmpPermissions.PermissionId = PermissionId
                                    objEmpPermissions.GetByPK()
                                    CurrentDuration = (objEmpPermissions.ToTime - objEmpPermissions.FromTime)
                                End If
                                Dim DaysCount = (EndDate - StartDate).Value.Days + 1
                                Dim DurationMinutes = (Math.Ceiling(CurrentDuration.TotalMinutes))
                                If (DurationMinutes < 0) Then
                                    DurationMinutes = (DurationMinutes) * -1
                                End If
                                If Not PermissionId = 0 Then
                                    PermissionDuration = PermissionDuration - DurationMinutes
                                End If
                                Dim TotalMinutes = DurationMinutes * DaysCount

                                If Not (drDuration("MaximumDuration_WithStudyNursing")) Is DBNull.Value Then
                                    If (PermissionDuration + TotalMinutes > drDuration("MaximumDuration_WithStudyNursing")) Then
                                        If SessionVariables.CultureInfo = "ar-JO" Then
                                            ErrorMessage = "الفترة المسموحة في وجود مغادرة رضاعة او دراسة : " & drDuration("MaximumDuration_WithStudyNursing") & " دقائق"
                                        Else
                                            ErrorMessage = "Allowed duration when nursing or study permission : " & drDuration("MaximumDuration_WithStudyNursing") & " minutes"
                                        End If
                                        Return False
                                    End If
                                End If

                                If AllowWhenInSufficient = False Then
                                    '-----------Check Ramadan Day------------'
                                    While StartDate <= EndDate
                                        objRamadanPeriod.SearchDate = StartDate
                                        objRamadanPeriod.Get_IsRamadan()

                                        If objRamadanPeriod.IsRamadanDay = True Then
                                            If (PermissionDuration + TotalMinutes > drDuration("MaximumRamadanDuration")) Then
                                                If SessionVariables.CultureInfo = "ar-JO" Then
                                                    ErrorMessage = "الفترة المسموحة في رمضان خلال اسبوع: " & drDuration("MaximumRamadanDuration") & " دقائق"
                                                Else
                                                    ErrorMessage = "Allowed durations in Ramadan per week: " & drDuration("MaximumRamadanDuration") & " minutes"
                                                End If
                                                Return False
                                            End If
                                        Else
                                            If (PermissionDuration + TotalMinutes > drDuration("MaximumDuration")) Then
                                                If SessionVariables.CultureInfo = "ar-JO" Then
                                                    ErrorMessage = "الفترة المسموحة خلال اسبوع: " & drDuration("MaximumDuration") & " دقائق"
                                                Else
                                                    ErrorMessage = "Allowed durations per week: " & drDuration("MaximumDuration") & " minutes"
                                                End If

                                                Return False
                                            End If
                                        End If
                                        StartDate = (DateAdd(DateInterval.Day, 1, Convert.ToDateTime(StartDate)))
                                    End While
                                    '-----------Check Ramadan Day------------'
                                End If


                            End With
                        End If

                        'Check Validation for a Month
                        If (dtPermissionDuration.Select("FK_DurationId = 3").Length > 0) Then
                            drDuration = dtPermissionDuration.Select("FK_DurationId = 3")(0)

                            'Get Occurance Per Month for this employee
                            objEmpPermissions = New Emp_Permissions
                            With objEmpPermissions
                                .PermDate = PermDate
                                .FK_EmployeeId = EmployeeId
                                ._FK_PermId = PermTypeId
                                Dim PermissionDuration = .GetDurationForMonth()
                                Dim CurrentDuration As TimeSpan
                                If PermissionId = 0 Then
                                    CurrentDuration = (ToTime - FromTime)
                                Else
                                    objEmpPermissions.PermissionId = PermissionId
                                    objEmpPermissions.GetByPK()
                                    CurrentDuration = (objEmpPermissions.ToTime - objEmpPermissions.FromTime)
                                End If
                                Dim DaysCount = (EndDate - StartDate).Value.Days + 1

                                Dim DurationMinutes = (Math.Ceiling(CurrentDuration.TotalMinutes))
                                If (DurationMinutes < 0) Then
                                    DurationMinutes = (DurationMinutes) * -1
                                End If
                                If Not PermissionId = 0 Then
                                    PermissionDuration = PermissionDuration - DurationMinutes
                                End If
                                Dim TotalMinutes = DurationMinutes * DaysCount

                                If Not (drDuration("MaximumDuration_WithStudyNursing")) Is DBNull.Value Then
                                    If (PermissionDuration + TotalMinutes > drDuration("MaximumDuration_WithStudyNursing")) Then
                                        If SessionVariables.CultureInfo = "ar-JO" Then
                                            ErrorMessage = "الفترة المسموحة في وجود مغادرة رضاعة او دراسة : " & drDuration("MaximumDuration_WithStudyNursing") & " دقائق"
                                        Else
                                            ErrorMessage = "Allowed duration when nursing or study permission : " & drDuration("MaximumDuration_WithStudyNursing") & " minutes"
                                        End If
                                        Return False
                                    End If
                                End If

                                If AllowWhenInSufficient = False Then
                                    '-----------Check Ramadan Day------------'
                                    While StartDate <= EndDate
                                        objRamadanPeriod.SearchDate = StartDate
                                        objRamadanPeriod.Get_IsRamadan()

                                        If objRamadanPeriod.IsRamadanDay = True Then
                                            If (PermissionDuration + TotalMinutes > drDuration("MaximumRamadanDuration")) Then
                                                If SessionVariables.CultureInfo = "ar-JO" Then
                                                    ErrorMessage = "الفترة المسموحة في رمضان خلال شهر: " & drDuration("MaximumRamadanDuration") & " دقائق"
                                                Else
                                                    ErrorMessage = "Allowed durations in Ramadan per month: " & drDuration("MaximumRamadanDuration") & " minutes"
                                                End If
                                                Return False
                                            End If
                                        Else
                                            If (PermissionDuration + TotalMinutes > drDuration("MaximumDuration")) Then
                                                If SessionVariables.CultureInfo = "ar-JO" Then
                                                    ErrorMessage = "الفترة المسموحة خلال شهر: " & drDuration("MaximumDuration") & " دقائق"
                                                Else
                                                    ErrorMessage = "Allowed durations per month: " & drDuration("MaximumDuration") & " minutes"
                                                End If
                                                Return False
                                            End If
                                        End If
                                        StartDate = (DateAdd(DateInterval.Day, 1, Convert.ToDateTime(StartDate)))
                                    End While
                                    '-----------Check Ramadan Day------------'
                                End If



                            End With
                        End If

                        'Check Validation for a Year
                        If (dtPermissionDuration.Select("FK_DurationId = 4").Length > 0) Then
                            drDuration = dtPermissionDuration.Select("FK_DurationId = 4")(0)

                            'Get Occurance Per Year for this employee
                            objEmpPermissions = New Emp_Permissions
                            With objEmpPermissions
                                .PermDate = PermDate
                                .FK_EmployeeId = EmployeeId
                                ._FK_PermId = PermTypeId
                                Dim PermissionDuration = .GetDurationForYear()
                                Dim CurrentDuration As TimeSpan
                                If PermissionId = 0 Then
                                    CurrentDuration = (ToTime - FromTime)
                                Else
                                    objEmpPermissions.PermissionId = PermissionId
                                    objEmpPermissions.GetByPK()
                                    CurrentDuration = (objEmpPermissions.ToTime - objEmpPermissions.FromTime)
                                End If
                                Dim DaysCount = (EndDate - StartDate).Value.Days + 1
                                Dim DurationMinutes = (Math.Ceiling(CurrentDuration.TotalMinutes))
                                If (DurationMinutes < 0) Then
                                    DurationMinutes = (DurationMinutes) * -1
                                End If
                                If Not PermissionId = 0 Then
                                    PermissionDuration = PermissionDuration - DurationMinutes
                                End If
                                Dim TotalMinutes = DurationMinutes * DaysCount

                                If Not (drDuration("MaximumDuration_WithStudyNursing")) Is DBNull.Value Then
                                    If (PermissionDuration + TotalMinutes > drDuration("MaximumDuration_WithStudyNursing")) Then
                                        If SessionVariables.CultureInfo = "ar-JO" Then
                                            ErrorMessage = "الفترة المسموحة في وجود مغادرة رضاعة او دراسة : " & drDuration("MaximumDuration_WithStudyNursing") & " دقائق"
                                        Else
                                            ErrorMessage = "Allowed duration when nursing or study permission : " & drDuration("MaximumDuration_WithStudyNursing") & " minutes"
                                        End If
                                        Return False
                                    End If
                                End If

                                If AllowWhenInSufficient = False Then
                                    '-----------Check Ramadan Day------------'
                                    While StartDate <= EndDate
                                        objRamadanPeriod.SearchDate = StartDate
                                        objRamadanPeriod.Get_IsRamadan()

                                        If objRamadanPeriod.IsRamadanDay = True Then
                                            If (PermissionDuration + TotalMinutes > drDuration("MaximumRamadanDuration")) Then
                                                If SessionVariables.CultureInfo = "ar-JO" Then
                                                    ErrorMessage = "الفترة المسموحة في رمضان خلال سنة: " & drDuration("MaximumRamadanDuration") & " دقائق"
                                                Else
                                                    ErrorMessage = "Allowed durations in Ramadan per year: " & drDuration("MaximumRamadanDuration") & " minutes"

                                                End If
                                                Return False
                                            End If
                                        Else
                                            If (PermissionDuration + TotalMinutes > drDuration("MaximumDuration")) Then
                                                If SessionVariables.CultureInfo = "ar-JO" Then
                                                    ErrorMessage = "الفترة المسموحة خلال سنة: " & drDuration("MaximumDuration") & " دقائق"
                                                Else
                                                    ErrorMessage = "Allowed durations per year: " & drDuration("MaximumDuration") & " minutes"

                                                End If
                                                Return False
                                            End If
                                        End If
                                        StartDate = (DateAdd(DateInterval.Day, 1, Convert.ToDateTime(StartDate)))
                                    End While
                                    '-----------Check Ramadan Day------------'
                                End If



                            End With
                        End If

                        'Check Validation for a Day
                        If (dtPermissionDuration.Select("FK_DurationId = 1").Length > 0) Then
                            drDuration = dtPermissionDuration.Select("FK_DurationId = 1")(0)

                            'Get Occurance Per Year for this employee
                            objEmpPermissions = New Emp_Permissions
                            With objEmpPermissions
                                .PermDate = PermDate
                                .FK_EmployeeId = EmployeeId
                                ._FK_PermId = PermTypeId
                                Dim PermissionDuration = .GetDurationForDay()
                                Dim CurrentDuration As TimeSpan
                                If PermissionId = 0 Then
                                    CurrentDuration = (ToTime - FromTime)
                                Else
                                    objEmpPermissions.PermissionId = PermissionId
                                    objEmpPermissions.GetByPK()
                                    CurrentDuration = (objEmpPermissions.ToTime - objEmpPermissions.FromTime)
                                End If
                                Dim DaysCount = (EndDate - StartDate).Value.Days + 1
                                Dim DurationMinutes = (Math.Ceiling(CurrentDuration.TotalMinutes))
                                If (DurationMinutes < 0) Then
                                    DurationMinutes = (DurationMinutes) * -1
                                End If
                                If Not PermissionId = 0 Then
                                    PermissionDuration = PermissionDuration - DurationMinutes
                                End If
                                Dim TotalMinutes = DurationMinutes

                                If Not (drDuration("MaximumDuration_WithStudyNursing")) Is DBNull.Value Then
                                    If (PermissionDuration + TotalMinutes > drDuration("MaximumDuration_WithStudyNursing")) Then
                                        If SessionVariables.CultureInfo = "ar-JO" Then
                                            ErrorMessage = "الفترة المسموحة في وجود مغادرة رضاعة او دراسة : " & drDuration("MaximumDuration_WithStudyNursing") & " دقائق"
                                        Else
                                            ErrorMessage = "Allowed duration when nursing or study permission : " & drDuration("MaximumDuration_WithStudyNursing") & " minutes"
                                        End If
                                        Return False
                                    End If
                                End If

                                If AllowWhenInSufficient = False Then
                                    '-----------Check Ramadan Day------------'
                                    While StartDate <= EndDate
                                        objRamadanPeriod.SearchDate = StartDate
                                        objRamadanPeriod.Get_IsRamadan()

                                        If objRamadanPeriod.IsRamadanDay = True Then
                                            If (PermissionDuration + TotalMinutes > drDuration("MaximumRamadanDuration")) Then
                                                If SessionVariables.CultureInfo = "ar-JO" Then
                                                    ErrorMessage = "الفترة المسموحة في رمضان خلال يوم: " & drDuration("MaximumRamadanDuration") & " دقائق"
                                                Else
                                                    ErrorMessage = "Allowed durations in Ramadan per day: " & drDuration("MaximumRamadanDuration") & " minutes"

                                                End If
                                                Return False
                                            End If
                                        Else
                                            If (PermissionDuration + TotalMinutes > drDuration("MaximumDuration")) Then
                                                If SessionVariables.CultureInfo = "ar-JO" Then
                                                    ErrorMessage = "الفترة المسموحة خلال يوم: " & drDuration("MaximumDuration") & " دقائق"
                                                Else
                                                    ErrorMessage = "Allowed durations per day: " & drDuration("MaximumDuration") & " minutes"

                                                End If
                                                Return False
                                            End If
                                        End If
                                        StartDate = (DateAdd(DateInterval.Day, 1, Convert.ToDateTime(StartDate)))
                                    End While
                                    '-----------Check Ramadan Day------------'
                                End If
                            End With
                        End If

                    End If
                End If
            End With
            Return True
        End Function

        Public Function CheckEmpAllowedLeaveBalance(ByVal EmployeeId As Integer, ByVal PermTypeId As Integer, ByVal PermTypeInfo As PermissionsTypes, ByRef EmpLeaveTotalBalance As Double, ByVal StartDate As DateTime?, ByVal EndDate As DateTime, ByVal FromTime As DateTime?, ByVal ToTime As DateTime?, ByVal IsFullDay As Boolean, ByVal FlexibilePermissionDuration As Integer, ByVal OffAndHolidayDays As Integer, ByRef ErrorMessage As String) As Boolean
            Dim LeaveID As Integer = 0
            Dim DeductBalanceFromOvertime As Boolean = False
            'Get Leave Id by Perm Id
            objPermissionsTypes = New PermissionsTypes
            objPermissionsTypes.PermId = PermTypeId
            objPermissionsTypes.GetByPK()
            LeaveID = objPermissionsTypes.FK_LeaveIdDeductBalance
            DeductBalanceFromOvertime = objPermissionsTypes.DeductBalanceFromOvertime
            'Get Employee Balance
            Dim LeaveCount As Integer
            'If (radBtnOneDay.Checked) Then
            '    LeaveCount = 1
            'Else
            '    LeaveCount = ((dtpEndDatePerm.SelectedDate - dtpStartDatePerm.SelectedDate).Value.Days + 1)
            'End If
            If EndDate = DateTime.MinValue Then
                EndDate = StartDate
            End If

            If DeductBalanceFromOvertime = False Then
                objEmp_Leaves_BalanceHistory = New Emp_Leaves_BalanceHistory
                objEmp_Leaves_BalanceHistory.FK_EmployeeId = EmployeeId
                objEmp_Leaves_BalanceHistory.FK_EmpLeaveId = LeaveID

                If Not LeaveID = 0 Then
                    Dim dtLeaveBalance = objEmp_Leaves_BalanceHistory.GetEmpLeaveBalance()

                    If (dtLeaveBalance IsNot Nothing And dtLeaveBalance.Rows.Count > 0) Then
                        EmpLeaveTotalBalance = dtLeaveBalance.Rows(0)("TotalBalance")
                        If LeaveID > 0 Then
                            Dim PermBalance As Long = 0
                            If IsFullDay = True Then
                                PermBalance = (((EndDate - StartDate).Value.Days + 1)) - OffAndHolidayDays
                            ElseIf Not FlexibilePermissionDuration = Nothing Then
                                PermBalance = FlexibilePermissionDuration / 480
                            Else
                                PermBalance = (ToTime.Value - FromTime.Value).TotalMinutes / 480
                            End If

                            '    objEmp_Leaves.LeaveId = LeaveID
                            '    objEmp_Leaves.GetByPK()

                            If (EmpLeaveTotalBalance < PermBalance) Then
                                If SessionVariables.CultureInfo = "ar-JO" Then
                                    ErrorMessage = "لايمكنك تقديم المغادرة بسبب عد كفاية الرصيد، الرصيد هو: " & EmpLeaveTotalBalance
                                Else
                                    ErrorMessage = "You Cannot Apply Due To Insufficient Balance,Your leave balance is: " & EmpLeaveTotalBalance
                                End If
                                Return False
                            End If
                        End If
                    Else
                        If SessionVariables.CultureInfo = "ar-JO" Then
                            ErrorMessage = "لايمكنك تقديم المغادرة بسبب عد كفاية الرصيد، الرصيد هو: " & "0"
                        Else
                            ErrorMessage = "You Cannot Apply Due To Insufficient Balance,Your leave balance is: " & "0"
                        End If
                        Return False
                    End If
                End If

            End If
            Return True
        End Function

        Public Function CheckRestRemainingBalance(ByVal PermTypeID As Integer, ByVal FK_EmployeeID As Integer, ByVal IsOnePeriod As Boolean, ByVal PermDate As DateTime?, ByVal StartDate As DateTime?,
                                                          ByVal EndDate As DateTime?, ByVal FromTime As DateTime?, ByVal ToTime As DateTime?, ByVal PermissionId As Integer, ByRef ErrorMsg As String) As Boolean
            Dim PermId As Integer = PermTypeID
            Dim DeductBalanceFromOvertime As Boolean
            Dim dt As DataTable
            Dim RemainingBalance As Integer
            Dim PermissionBalance As Integer
            objPermissionsTypes = New PermissionsTypes()
            With objPermissionsTypes
                .PermId = PermId
                .GetByPK()
                DeductBalanceFromOvertime = .DeductBalanceFromOvertime
            End With
            If DeductBalanceFromOvertime = True Then
                Dim objEmp_Permissions As New Emp_Permissions
                With objEmp_Permissions
                    .FK_EmployeeId = FK_EmployeeID
                    .PermDate = PermDate
                    dt = .Get_RestPermission_RemainingBalance()
                End With
                RemainingBalance = dt(0)("RemainingBalance").ToString
                PermissionBalance = (ToTime.Value - FromTime.Value).TotalMinutes

                If RemainingBalance >= PermissionBalance Then
                    Return True
                Else
                    If SessionVariables.CultureInfo = "ar-JO" Then
                        ErrorMsg = "الرصيد المتبقي غير كافي لطلب هذا النوع من المغادرة"
                    Else
                        ErrorMsg = "The Remaining Balance Is Not Enough To Apply The Selected Permission"
                    End If
                    Return False
                End If
            End If
            Return True
        End Function

        Public Function CheckEmpAllowedPermissionDuration(ByVal PermTypeID As Integer, ByVal FK_EmployeeID As Integer, ByVal IsOnePeriod As Boolean, ByVal PermDate As DateTime?, ByVal StartDate As DateTime?,
                                                          ByVal EndDate As DateTime?, ByVal FromTime As DateTime?, ByVal ToTime As DateTime?, ByVal PermissionId As Integer, ByRef ErrorMsg As String) As Boolean

            Dim permId As Integer = PermTypeID
            Dim monthlyBalance As Integer = 0
            Dim DeductBalanceFromOvertime As Boolean
            Dim remainingBalanceCount As Integer = 0
            objPermissionsTypes = New PermissionsTypes()
            Dim objEmp_WorkSchedule As Emp_WorkSchedule

            With objPermissionsTypes
                .PermId = permId
                .GetByPK()
                monthlyBalance = .MonthlyBalance
                DeductBalanceFromOvertime = .DeductBalanceFromOvertime
            End With

            If DeductBalanceFromOvertime = False Then

                Dim EmpDt As DataTable
                Dim intFromtime As Integer = 0
                Dim intToTime As Integer = 0
                Dim studyFound As Boolean = False

                Dim objEmp_Permissions As New Emp_Permissions
                With objEmp_Permissions
                    .FK_EmployeeId = FK_EmployeeID
                    .FK_PermId = PermTypeID
                    .PermissionId = PermissionId

                    If IsOnePeriod Then
                        .FromTime = DateSerial(PermDate.Value.Year, PermDate.Value.Month, 1)
                        .ToTime = DateSerial(PermDate.Value.Year, PermDate.Value.Month + 1, 0)
                    Else
                        .FromTime = DateSerial(StartDate.Value.Year, StartDate.Value.Month, 1)
                        .ToTime = DateSerial(EndDate.Value.Year, EndDate.Value.Month + 1, 0)
                    End If

                    EmpDt = .GetAllDurationByEmployee()
                End With

                If EmpDt IsNot Nothing Then
                    If EmpDt.Rows.Count > 0 Then
                        For Each row As DataRow In EmpDt.Rows

                            If Not row("PermissionOption") = 2 Then
                                If (Not IsDBNull(row("ToTime"))) AndAlso (Not IsDBNull(row("FromTime"))) Then

                                    If Not IsDBNull(row("IsFullDay")) Then
                                        Dim IsFullDay As Boolean = Convert.ToBoolean(row("IsFullDay"))
                                        If Not IsFullDay Then
                                            remainingBalanceCount += (Convert.ToDateTime(row("ToTime")).Subtract(Convert.ToDateTime(row("FromTime")))).TotalMinutes
                                        Else
                                            objEmp_WorkSchedule = New Emp_WorkSchedule()
                                            objEmp_WorkSchedule.FK_EmployeeId = FK_EmployeeID
                                            objEmp_WorkSchedule.ScheduleDate = Nothing
                                            Dim arrTotalWorkHours As String() = objEmp_WorkSchedule.GetEmpScheduleWithTime().Split(",")
                                            Dim intEmpTotalWorkHours As Integer = Convert.ToInt32(arrTotalWorkHours(arrTotalWorkHours.Length - 1))
                                            remainingBalanceCount += intEmpTotalWorkHours
                                        End If
                                    End If
                                End If
                            End If
                        Next
                        Dim timediff As TimeSpan = (Convert.ToDateTime(ToTime.Value).Subtract(Convert.ToDateTime(FromTime.Value)))
                        remainingBalanceCount += (timediff.Hours * 60 + timediff.Minutes)

                        If remainingBalanceCount > monthlyBalance Then
                            If SessionVariables.CultureInfo = "ar-JO" Then
                                ErrorMsg = "لا يمكنك تجاوز الحد المسموح به لنوع المغادرة"
                            Else
                                ErrorMsg = "Can't exceed permission type duration limit"
                            End If
                            Return False
                        Else
                            Return True
                        End If
                    Else
                        Return True
                    End If
                Else
                    Return True
                End If
            End If
            Return True

        End Function

        Public Function PermissionValidation(ByVal EmployeeId As Integer, ByVal objPermissionsTypes As PermissionsTypes, ByVal PermTypeId As Integer, ByVal StartDate As DateTime?, ByVal EndDate As DateTime?,
                                     ByVal FromTime As DateTime?, ByVal ToTime As DateTime?, ByVal PermDate As DateTime, ByVal IsPermissionOneday As Boolean, ByVal IsFullyDay As Boolean, ByRef ErrorMsg As String, ByRef OffAndHolidayDays As String, ByRef EmpLeaveTotalBalance As Double, ByVal PermissionId As Integer) As Boolean

            objPermissionsTypes = New PermissionsTypes
            With objPermissionsTypes
                .PermId = PermTypeId
                Dim PermTypeInfo = .GetByPK()

                If (PermTypeInfo IsNot Nothing) Then

                    If (FromTime.HasValue And ToTime.HasValue) Then
                        'Check Permission Min and Max Days
                        If Not CheckaPermissionsMinMaxDays(PermTypeInfo, FromTime, ToTime, PermDate, IsFullyDay, ErrorMsg) Then
                            Return False
                        Else
                            'Count Off And Hoildays Days
                            OffAndHolidayDays = CalculateOffAndHolidaysDays(EmployeeId, StartDate, EndDate, PermTypeInfo, PermDate, IsPermissionOneday)

                            'Check Permission Allowed occurance for day, week and year
                            If Not CheckAllowedOccuranceForDayWeekMonth(EmployeeId, StartDate, EndDate, PermDate, PermTypeInfo, IsPermissionOneday, PermTypeId, PermissionId, ErrorMsg) Then
                                Return False
                            Else
                                'Check Permission Allowed duration for day, week and year
                                If Not CheckAllowedDurationForDayWeekMonth(EmployeeId, StartDate, EndDate, FromTime, ToTime, PermDate,
                                                                          IsPermissionOneday, PermTypeInfo, PermTypeId, PermissionId, ErrorMsg) Then
                                    Return False
                                End If
                            End If
                            'Validate Allowed Balance
                            If Not CheckEmpAllowedLeaveBalance(EmployeeId, PermTypeId, PermTypeInfo, EmpLeaveTotalBalance, StartDate, EndDate, FromTime, ToTime, IsFullyDay, Nothing, OffAndHolidayDays, ErrorMsg) Then
                                Return False
                            End If

                            If Not CheckEmpAllowedPermissionDuration(PermTypeId, EmployeeId, IsPermissionOneday, PermDate, StartDate, EndDate, FromTime, ToTime, PermissionId, ErrorMsg) Then
                                Return False
                            End If

                            If Not CheckRestRemainingBalance(PermTypeId, EmployeeId, IsPermissionOneday, PermDate, StartDate, EndDate, FromTime, ToTime, PermissionId, ErrorMsg) Then
                                Return False
                            End If

                            If Not IsAllowedForManagers(EmployeeId, PermTypeId, ErrorMsg) Then
                                Return False
                            End If

                            If Not IsAllowedForSpecificEntity(EmployeeId, PermTypeId, ErrorMsg) Then
                                Return False
                            End If

                            If Not IsAllowedForSpecificCompany(EmployeeId, PermTypeId, ErrorMsg) Then
                                Return False
                            End If

                            If Not CheckDelayPermissionsCount(EmployeeId, PermDate, PermEndDate, PermTypeInfo, IsPermissionOneday, PermTypeId, FromTime, ErrorMsg) Then
                                Return False
                            End If

                            If Not CheckEmployeeTransaction(EmployeeId, PermDate, PermEndDate, PermTypeInfo, IsPermissionOneday, PermTypeId, ErrorMsg) Then
                                Return False
                            End If

                            If Not ShouldCompleteHalfWHRS(EmployeeId, PermTypeId, PermDate, FromTime, ToTime, ErrorMsg) Then
                                Return False
                            End If

                            If Not StudyNursing_ShouldCompleteNoOfHours(EmployeeId, PermTypeId, PermDate, PermEndDate, ErrorMsg) Then
                                Return False
                            End If
                        End If

                    Else
                        'Count Off And Hoildays Days
                        OffAndHolidayDays = CalculateOffAndHolidaysDays(EmployeeId, StartDate, EndDate, PermTypeInfo, PermDate, IsPermissionOneday)

                        'Check Permission Allowed occurance for day, week and year
                        If Not CheckAllowedOccuranceForDayWeekMonth(EmployeeId, StartDate, EndDate, PermDate, PermTypeInfo, IsPermissionOneday, PermTypeId, PermissionId, ErrorMsg) Then
                            Return False
                        Else
                            'Check Permission Allowed duration for day, week and year
                            If Not CheckAllowedDurationForDayWeekMonth(EmployeeId, StartDate, EndDate, FromTime, ToTime, PermDate,
                                                                      IsPermissionOneday, PermTypeInfo, PermTypeId, PermissionId, ErrorMsg) Then
                                Return False
                            End If
                        End If
                        'Validate Allowed Balance
                        If Not CheckEmpAllowedLeaveBalance(EmployeeId, PermTypeId, PermTypeInfo, EmpLeaveTotalBalance, StartDate, EndDate, FromTime, ToTime, IsFullyDay, Nothing, OffAndHolidayDays, ErrorMsg) Then
                            Return False
                        End If

                        If Not CheckEmpAllowedPermissionDuration(PermTypeId, EmployeeId, IsPermissionOneday, PermDate, StartDate, EndDate, FromTime, ToTime, PermissionId, ErrorMsg) Then
                            Return False
                        End If

                        If Not CheckRestRemainingBalance(PermTypeId, EmployeeId, IsPermissionOneday, PermDate, StartDate, EndDate, FromTime, ToTime, PermissionId, ErrorMsg) Then
                            Return False
                        End If
                    End If
                    Return True
                End If
            End With
            Return True
        End Function

        Public Function IsAllowedAfterSpecific(ByVal PermTypeId As Integer, ByVal FromTime As DateTime) As Boolean
            Dim objPermissionsTypes = New PermissionsTypes
            Dim status As Boolean

            Dim intFromTime As Integer = (FromTime.Hour * 60) + (FromTime.Minute)
            With objPermissionsTypes
                .PermId = PermTypeId
                .GetByPK()
                AllowAfter = .AllowedAfter
                If .Isallowedaftertime = True Then
                    If AllowAfter > intFromTime Then
                        status = True
                    End If
                End If
            End With
            Return status
        End Function

        Public Function IsAllowedBeforeSpecific(ByVal PermTypeId As Integer, ByVal ToTime As DateTime) As Boolean
            Dim objPermissionsTypes = New PermissionsTypes
            Dim status As Boolean

            Dim intToTime As Integer = (ToTime.Hour * 60) + (ToTime.Minute)
            With objPermissionsTypes
                .PermId = PermTypeId
                .GetByPK()
                AllowBefore = .AllowedBefore
                If .IsAllowedBeforeTime = True Then
                    If AllowBefore < intToTime Then
                        status = True
                    End If
                End If
            End With
            Return status
        End Function

        Public Function ValidateEmployeePermission(ByVal EmployeeId As Integer, ByVal PermissionId As Integer, ByVal dtCurrent As DataTable, ByVal IsPermissionOneday As Boolean, ByVal StartDate As DateTime?, ByVal EndDate As DateTime?, ByVal FromTime As DateTime?,
                                            ByVal ToTime As DateTime?, ByVal PermDate As DateTime, ByVal objPermissionsTypes As PermissionsTypes, ByVal PermTypeId As Integer, ByVal IsFullyDay As Boolean, ByRef ErrorMessage As String, ByRef OffAndHolidayDays As String,
                                            ByRef EmpLeaveTotalBalance As Double) As Boolean
            If IsExistsFullDay(dtCurrent, IsPermissionOneday, StartDate, EndDate, FromTime, ToTime, PermissionId, PermDate) = True Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    ErrorMessage = " يوجد طلب مغادرة مسبقاً ليوم كامل في نفس التاريخ"
                Else
                    ErrorMessage = "Permission Record Already Exists for full day in the same date"
                End If

                Return False
            ElseIf IsExists(dtCurrent, IsPermissionOneday, PermDate, PermDate, FromTime, ToTime, PermissionId, PermDate) = True Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    ErrorMessage = " يوجد طلب مغادرة مسبقاً ضمن الفترة المطلوبة"
                Else
                    ErrorMessage = "Permission Record Already Exists Between The Date and Time Range"
                End If

                Return False


            ElseIf IsAllowedAfterSpecific(PermTypeId, FromTime) = True Then
                Dim strAllowAfter As String
                strAllowAfter = CtlCommon.GetFullTimeString(AllowAfter)
                'strAllowAfter = strAllowAfter(0) + strAllowAfter(1) + ":" + strAllowAfter(2) + strAllowAfter(3)

                If SessionVariables.CultureInfo = "ar-JO" Then
                    ErrorMessage = " لايمكن تقديم مغادرة قبل " + " " + strAllowAfter + " لـ " + objPermissionsTypes.PermArabicName
                Else
                    ErrorMessage = "Permssion Cannot be Requested Before" + " " + strAllowAfter + " For " + objPermissionsTypes.PermName
                End If

                Return False
            ElseIf IsAllowedBeforeSpecific(PermTypeId, ToTime) = True Then
                Dim strAllowBefore As String
                strAllowBefore = CtlCommon.GetFullTimeString(AllowBefore)
                'strAllowBefore = strAllowBefore(0) + strAllowBefore(1) + ":" + strAllowBefore(2) + strAllowBefore(3)

                If SessionVariables.CultureInfo = "ar-JO" Then
                    ErrorMessage = " لايمكن تقديم مغادرة بعد " + " " + strAllowBefore + " لـ " + objPermissionsTypes.PermArabicName
                Else
                    ErrorMessage = "Permission Cannot be Requested After" + " " + strAllowBefore + " For " + objPermissionsTypes.PermName
                End If

                Return False

            ElseIf IsleaveExists(StartDate, EndDate, EmployeeId, LeaveId) = True Then
                If SessionVariables.CultureInfo = "en-US" Then
                        ErrorMessage = "Leave Record Already Exists Between The Date Range"
                    Else
                    ErrorMessage = "يوجد اجازة في حدود التاريخ"

                End If
                    Return False
                Else
                    Return True
                End If
                'ElseIf IsExists(FromDate, ToDate, EmployeeId, LeaveId) = True Then
                '    If SessionVariables.CultureInfo = "en-US" Then
                '        ErrorMessage = "Leave Record Already Exists Between The Date Range"
                '    Else
                '        ErrorMessage = "الاجازة موجودة مسبقاً في حدود التاريخ"
                '    End If
                'End If

                If EmployeeId = 0 Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    ErrorMessage = "هذا المستخدم غير مرتبط مع موظف"
                Else
                    ErrorMessage = "This user not related with employee"
                End If

                Return False
            End If

            If Not PermissionValidation(EmployeeId, objPermissionsTypes, PermTypeId, StartDate, EndDate, FromTime, ToTime, PermDate, IsPermissionOneday, IsFullyDay, ErrorMessage, OffAndHolidayDays, EmpLeaveTotalBalance, PermissionId) Then
                Return False
            Else
                Return True
            End If
        End Function



        Public Sub AddPermAllProcess(ByVal EmployeeId As Integer, ByVal PermId As Integer, ByVal IsDividable As Boolean, ByVal IsFlexible As Boolean,
                                    ByVal IsSpecifiedDays As Boolean, ByVal Remarks As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime?, ByVal IsOneDay As Boolean,
                                    ByVal FromTime As DateTime?, ByVal ToTime As DateTime?, ByVal PermDate As DateTime, ByVal PermissionDaysCount As Double,
                                    ByVal OffAndHolidayDays As Integer, ByVal days As String, ByVal EmpLeaveTotalBalance As Double, ByVal LeaveId As Integer, ByVal PermissionOption As Integer,
                                    ByVal FlexiblePermissionDuration As Integer, ByVal AttachedFile As String, ByVal IsFullDay As Boolean, ByVal AllowedTime As Integer, ByRef ResultMessage As String,
                                    Optional ByVal StudyYear As Integer = 0, Optional ByVal Semester As String = "", Optional ByVal FK_UniversityId As Integer? = Nothing, Optional ByVal Emp_GPAType As Integer? = Nothing,
                                    Optional ByVal Emp_GPA As Decimal? = Nothing, Optional ByVal FK_MajorId_Study As Integer? = Nothing, Optional ByVal FK_SpecializationId_Study As Integer? = Nothing)

            Dim errNo As Integer = -1

            FillObjectData(EmployeeId, PermId, IsDividable, IsFlexible, IsSpecifiedDays, Remarks, StartDate, EndDate, IsOneDay,
                           FromTime, ToTime, PermDate, PermissionDaysCount, OffAndHolidayDays, PermissionOption, days, FlexiblePermissionDuration,
                           AttachedFile, IsFullDay, AllowedTime, StudyYear, Semester, FK_UniversityId, Emp_GPAType, Emp_GPA, FK_MajorId_Study, FK_SpecializationId_Study)


            If PermissionId = 0 Then
                objEmpPermissions.CREATED_BY = SessionVariables.LoginUser.ID
                errNo = objEmpPermissions.Add()
                PermissionId = objEmpPermissions.PermissionId

                If errNo = 0 Then

                    objPermissionsTypes = New PermissionsTypes
                    objPermissionsTypes.PermId = PermId
                    objPermissionsTypes.GetByPK()

                    objEmp_Leaves_BalanceHistory = New Emp_Leaves_BalanceHistory
                    objEmp_Leaves_BalanceHistory.FK_EmployeeId = EmployeeId
                    objEmp_Leaves_BalanceHistory.FK_EmpPermId = PermissionId
                    If (objPermissionsTypes IsNot Nothing) Then
                        objEmp_Leaves_BalanceHistory.FK_LeaveId = objPermissionsTypes.FK_LeaveIdDeductBalance
                    Else
                        objEmp_Leaves_BalanceHistory.FK_LeaveId = 0
                    End If

                    objEmp_Leaves_BalanceHistory.BalanceDate = Date.Now

                    'If IsFullDay = True Then
                    '    objEmp_Leaves_BalanceHistory.Balance = (DateDiff(DateInterval.Day, StartDate, IIf(EndDate = DateTime.MinValue, StartDate, EndDate)) + 1) * -1
                    '    PermissionDaysCount = (DateDiff(DateInterval.Day, StartDate, IIf(EndDate = DateTime.MinValue, StartDate, EndDate)) + 1)
                    '    objEmp_Leaves_BalanceHistory.TotalBalance = EmpLeaveTotalBalance - PermissionDaysCount
                    'Else
                    objEmp_Leaves_BalanceHistory.Balance = PermissionDaysCount * -1
                    objEmp_Leaves_BalanceHistory.TotalBalance = EmpLeaveTotalBalance - PermissionDaysCount
                    'End If


                    objEmp_Leaves_BalanceHistory.TotalBalance = Decimal.Round(Convert.ToDecimal(objEmp_Leaves_BalanceHistory.TotalBalance), 3)
                    objEmp_Leaves_BalanceHistory.Remarks = "Add New Permissions"
                    objEmp_Leaves_BalanceHistory.CREATED_DATE = Date.Now
                    objEmp_Leaves_BalanceHistory.CREATED_BY = "Username"
                    If Not objPermissionsTypes.FK_LeaveIdDeductBalance = 0 Then
                        objEmp_Leaves_BalanceHistory.AddBalance()
                    End If
                    ResultMessage = CodeResultMessage.CodeSaveSucess
                Else
                    ResultMessage = CodeResultMessage.CodeSaveFail
                End If

            Else
                objEmpPermissions.LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                objEmpPermissions.PermissionId = PermissionId
                errNo = objEmpPermissions.Update()
                If errNo = 0 Then
                    If (PermissionId <> 0) Then
                        objEmp_Leaves_BalanceHistory = New Emp_Leaves_BalanceHistory
                        objEmp_Leaves_BalanceHistory.FK_EmployeeId = EmployeeId
                        objEmp_Leaves_BalanceHistory.FK_EmpPermId = PermissionId
                        objEmp_Leaves_BalanceHistory.FK_LeaveId = LeaveId
                        objEmp_Leaves_BalanceHistory.GetLastBalance()
                        objEmp_Leaves_BalanceHistory.BalanceDate = Date.Now
                        objEmp_Leaves_BalanceHistory.Balance = objEmp_Leaves_BalanceHistory.Balance * -1
                        objEmp_Leaves_BalanceHistory.TotalBalance = EmpLeaveTotalBalance + objEmp_Leaves_BalanceHistory.Balance
                        objEmp_Leaves_BalanceHistory.TotalBalance = Decimal.Round(Convert.ToDecimal(objEmp_Leaves_BalanceHistory.TotalBalance), 3)
                        objEmp_Leaves_BalanceHistory.Remarks = "Update Permission Data - to rollback old record"
                        objEmp_Leaves_BalanceHistory.CREATED_DATE = Date.Now
                        objEmp_Leaves_BalanceHistory.CREATED_BY = "Username"
                        objEmp_Leaves_BalanceHistory.AddBalance()


                        objEmp_Leaves_BalanceHistory = New Emp_Leaves_BalanceHistory
                        objEmp_Leaves_BalanceHistory.FK_EmployeeId = EmployeeId
                        objEmp_Leaves_BalanceHistory.FK_EmpPermId = PermissionId
                        objEmp_Leaves_BalanceHistory.FK_LeaveId = LeaveId
                        objEmp_Leaves_BalanceHistory.BalanceDate = Date.Now
                        objEmp_Leaves_BalanceHistory.GetLastBalance()
                        objEmp_Leaves_BalanceHistory.Balance = objEmpPermissions.BalanceDays * -1


                        objEmp_Leaves_BalanceHistory.TotalBalance = objEmp_Leaves_BalanceHistory.TotalBalance - objEmpPermissions.BalanceDays
                        objEmp_Leaves_BalanceHistory.TotalBalance = Decimal.Round(Convert.ToDecimal(objEmp_Leaves_BalanceHistory.TotalBalance), 3)
                        objEmp_Leaves_BalanceHistory.Remarks = "Update Permission Data"
                        objEmp_Leaves_BalanceHistory.CREATED_DATE = Date.Now
                        objEmp_Leaves_BalanceHistory.CREATED_BY = "Username"
                        objEmp_Leaves_BalanceHistory.AddBalance()

                        ResultMessage = CodeResultMessage.CodeUpdateSucess
                    Else
                        ResultMessage = CodeResultMessage.CodeSaveFail
                    End If
                End If
            End If

        End Sub

        Public Sub FillObjectData(ByVal EmployeeId As Integer, ByVal PermId As Integer, ByVal IsDividable As Boolean, ByVal IsFlexible As Boolean,
                                   ByVal IsSpecifiedDays As Boolean, ByVal Remarks As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime?,
                                   ByVal IsOneDay As Boolean, ByVal FromTime As DateTime?, ByVal ToTime As DateTime?, ByVal PermDate As DateTime,
                                   ByRef PermissionDaysCount As Double, ByVal OffAndHolidayDays As Integer, ByVal PermissionOption As Integer, ByVal days As String, ByVal flixbilePermissionDuration As Integer,
                                   ByVal AttachedFile As String, ByVal IsFullDay As Boolean, ByVal AllowedTime As Integer, Optional ByVal StudyYear As Integer = 0,
                                   Optional ByVal Semester As String = "", Optional ByVal FK_UniversityId As Integer? = Nothing,
                                   Optional ByVal Emp_GPAType As Integer? = Nothing, Optional ByVal Emp_GPA As Decimal? = Nothing, Optional ByVal FK_MajorId_Study As Integer? = Nothing,
                                   Optional ByVal FK_SpecializationId_Study As Integer? = Nothing)

            ' Set data into object for Add / Update

            Dim considerDaysMinutes As Integer
            Dim objAppSettings As New APP_Settings()
            With objAppSettings
                .GetByPK()
                considerDaysMinutes = .DaysMinutes
                If considerDaysMinutes = 0 Then
                    considerDaysMinutes = 480
                End If
            End With

            objEmpPermissions = New Emp_Permissions
            With objEmpPermissions
                ' Get Values from the combo boxes
                .FK_EmployeeId = EmployeeId 'RadCmbEmployee.SelectedValue
                .FK_PermId = PermId
                ' Get values from the check boxes
                .IsDividable = IsDividable
                .IsFlexible = IsFlexible
                .IsFullDay = IsFullDay
                .IsSpecificDays = IsSpecifiedDays
                .Remark = Remarks
                .AllowedTime = AllowedTime
                ' Get values from rad controls
                .PermDate = StartDate
                .FlexibilePermissionDuration = flixbilePermissionDuration
                .PermissionOption = PermissionOption
                .AttachedFile = AttachedFile
                If ((IsOneDay)) Then
                    If ((FromTime.HasValue) AndAlso (ToTime.HasValue)) Then
                        Dim objFromtime As DateTime
                        Dim objToTime As DateTime
                        objFromtime = FromTime
                        objToTime = ToTime

                        Dim ts As TimeSpan
                        ts = New TimeSpan(objFromtime.TimeOfDay.Hours, objFromtime.TimeOfDay.Minutes, objFromtime.TimeOfDay.Seconds)
                        objFromtime = PermDate + ts

                        ts = New TimeSpan(objToTime.TimeOfDay.Hours, objToTime.TimeOfDay.Minutes, objToTime.TimeOfDay.Seconds)
                        objToTime = PermDate + ts

                        .FromTime = objFromtime
                        .ToTime = objToTime
                    Else
                        .FromTime = FromTime
                        .ToTime = ToTime
                    End If
                Else
                    .FromTime = FromTime
                    .ToTime = ToTime
                End If
                If IsFlexible = True Then
                    .FromTime = Nothing
                    .ToTime = Nothing
                End If
                PermissionDaysCount = 0


                If Not .IsFlexible Then

                    If (Not String.IsNullOrEmpty(days)) AndAlso (Not days = "&nbsp;") Then
                        .Days = days
                        Dim dayArr() As String = days.Split(",")
                        Dim DayId As Integer
                        Dim daysCount As Integer = 0

                        If Not EndDate = DateTime.MinValue Then
                            Dim tempPermissionDays As Integer = (((EndDate - StartDate).Value.Days + 1) - OffAndHolidayDays)
                            Dim permFromDate As DateTime = StartDate
                            Dim permToDate As DateTime = StartDate.AddDays(tempPermissionDays - 1)

                            While permToDate >= permFromDate
                                Dim strDay As String = permFromDate.DayOfWeek.ToString()
                                Select Case strDay
                                    Case "Monday"
                                        DayId = 2
                                    Case "Tuesday"
                                        DayId = 3
                                    Case "Wednesday"
                                        DayId = 4
                                    Case "Thursday"
                                        DayId = 5
                                    Case "Friday"
                                        DayId = 6
                                    Case "Saturday"
                                        DayId = 7
                                    Case "Sunday"
                                        DayId = 1
                                End Select

                                For i = 0 To dayArr.Length - 1
                                    If Not String.IsNullOrEmpty(dayArr(i)) Then
                                        If Convert.ToDouble(dayArr(i)) = DayId Then
                                            daysCount += 1
                                            Exit For
                                        End If
                                    End If
                                Next

                                permFromDate = permFromDate.AddDays(1)
                            End While
                        End If

                        If IsFullDay = True Then
                            If IsSpecifiedDays = True Then
                                PermissionDaysCount = daysCount * 1
                            Else
                                If EndDate = DateTime.MinValue Then
                                    EndDate = StartDate
                                End If
                                PermissionDaysCount = (((EndDate - StartDate).Value.Days + 1) - OffAndHolidayDays)
                            End If

                            PermissionDaysCount = Decimal.Round(Convert.ToDecimal(PermissionDaysCount), 3)
                            .BalanceDays = PermissionDaysCount
                        Else
                            Dim TotalPermissionMinutes As Integer = (ToTime.Value - FromTime.Value).TotalMinutes
                            If TotalPermissionMinutes < 0 Then
                                TotalPermissionMinutes = TotalPermissionMinutes + 1440 '---24 Hours - For Overnight Shifts
                            End If

                            PermissionDaysCount = daysCount * (TotalPermissionMinutes / considerDaysMinutes)
                            PermissionDaysCount = Decimal.Round(Convert.ToDecimal(PermissionDaysCount), 3)
                            .BalanceDays = PermissionDaysCount
                        End If

                    Else
                        If OffAndHolidayDays > 0 Then
                            If IsOneDay Then
                                PermissionDaysCount = 0
                            Else
                                If IsFullDay = False Then 'If (FromTime.HasValue And ToTime.HasValue) Then
                                    Dim TotalPermissionMinutes As Integer = (ToTime.Value - FromTime.Value).TotalMinutes
                                    If TotalPermissionMinutes < 0 Then
                                        TotalPermissionMinutes = TotalPermissionMinutes + 1440 '---24 Hours - For Overnight Shifts
                                    End If

                                    PermissionDaysCount = (((EndDate - StartDate).Value.Days + 1) - OffAndHolidayDays) * (TotalPermissionMinutes / considerDaysMinutes)
                                    PermissionDaysCount = Decimal.Round(Convert.ToDecimal(PermissionDaysCount), 3)
                                Else
                                    PermissionDaysCount = (((EndDate - StartDate).Value.Days + 1) - OffAndHolidayDays)
                                End If
                            End If

                            .BalanceDays = PermissionDaysCount
                        Else
                            If IsOneDay Then

                                If IsFullDay = True Then
                                    PermissionDaysCount = 1
                                    PermissionDaysCount = Decimal.Round(Convert.ToDecimal(PermissionDaysCount), 3)
                                Else
                                    Dim TotalPermissionMinutes As Integer
                                    Dim Permtimespan As TimeSpan
                                    Permtimespan = ToTime.Value - FromTime.Value
                                    TotalPermissionMinutes = (Permtimespan.Hours * 60) + Permtimespan.Minutes '(ToTime.Value - FromTime.Value).TotalMinutes
                                    If TotalPermissionMinutes < 0 Then
                                        TotalPermissionMinutes = TotalPermissionMinutes + 1440 '---24 Hours - For Overnight Shifts
                                    End If

                                    PermissionDaysCount = (TotalPermissionMinutes / considerDaysMinutes)
                                    PermissionDaysCount = Decimal.Round(Convert.ToDecimal(PermissionDaysCount), 3)
                                End If

                            Else

                                If IsFullDay = True Then
                                    PermissionDaysCount = (EndDate - StartDate).Value.Days + 1 * (1)
                                    PermissionDaysCount = Decimal.Round(Convert.ToDecimal(PermissionDaysCount), 3)
                                Else
                                    If (ToTime.HasValue And FromTime.HasValue) Then
                                        Dim TotalPermissionMinutes As Integer = (ToTime.Value - FromTime.Value).TotalMinutes
                                        If TotalPermissionMinutes < 0 Then
                                            TotalPermissionMinutes = TotalPermissionMinutes + 1440 '---24 Hours - For Overnight Shifts
                                        End If

                                        PermissionDaysCount = (EndDate - StartDate).Value.Days + 1 * (TotalPermissionMinutes / considerDaysMinutes)
                                        PermissionDaysCount = Decimal.Round(Convert.ToDecimal(PermissionDaysCount), 3)
                                    End If
                                End If

                            End If
                            .BalanceDays = PermissionDaysCount
                        End If
                    End If
                ElseIf .IsFlexible Then

                    If (Not String.IsNullOrEmpty(days)) AndAlso (Not days = "&nbsp;") Then
                        .Days = days
                        Dim dayArr() As String = days.Split(",")
                        Dim DayId As Integer
                        Dim daysCount As Integer = 0

                        Dim tempPermissionDays As Integer = (((EndDate - StartDate).Value.Days + 1) - OffAndHolidayDays)
                        Dim permFromDate As DateTime = StartDate
                        Dim permToDate As DateTime = StartDate.AddDays(tempPermissionDays - 1)

                        While permToDate >= permFromDate
                            Dim strDay As String = permFromDate.DayOfWeek.ToString()
                            Select Case strDay
                                Case "Monday"
                                    DayId = 2
                                Case "Tuesday"
                                    DayId = 3
                                Case "Wednesday"
                                    DayId = 4
                                Case "Thursday"
                                    DayId = 5
                                Case "Friday"
                                    DayId = 6
                                Case "Saturday"
                                    DayId = 7
                                Case "Sunday"
                                    DayId = 1
                            End Select

                            For i = 0 To dayArr.Length - 1
                                If Not String.IsNullOrEmpty(dayArr(i)) Then
                                    Try
                                        If Convert.ToInt32(dayArr(i)) = DayId Then
                                            daysCount += 1
                                            Exit For
                                        End If
                                    Catch ex As Exception

                                    End Try

                                End If
                            Next

                            permFromDate = permFromDate.AddDays(1)
                        End While

                        PermissionDaysCount = daysCount * (.FlexibilePermissionDuration / considerDaysMinutes)
                        PermissionDaysCount = Decimal.Round(Convert.ToDecimal(PermissionDaysCount), 3)
                        .BalanceDays = PermissionDaysCount
                    Else
                        If OffAndHolidayDays > 0 Then
                            If IsOneDay Then
                                PermissionDaysCount = 0
                            Else
                                If (FromTime.HasValue And ToTime.HasValue) Then
                                    PermissionDaysCount = (((EndDate - StartDate).Value.Days + 1) - OffAndHolidayDays) * (.FlexibilePermissionDuration / considerDaysMinutes)
                                    PermissionDaysCount = Decimal.Round(Convert.ToDecimal(PermissionDaysCount), 3)
                                Else
                                    PermissionDaysCount = (((EndDate - StartDate).Value.Days + 1) - OffAndHolidayDays)
                                End If
                            End If

                            .BalanceDays = PermissionDaysCount
                        Else
                            If IsOneDay Then
                                PermissionDaysCount = (.FlexibilePermissionDuration / considerDaysMinutes)
                                PermissionDaysCount = Decimal.Round(Convert.ToDecimal(PermissionDaysCount), 3)
                            Else
                                If (ToTime.HasValue And FromTime.HasValue) Then
                                    PermissionDaysCount = (EndDate - StartDate).Value.Days + 1 * (.FlexibilePermissionDuration / considerDaysMinutes)
                                    PermissionDaysCount = Decimal.Round(Convert.ToDecimal(PermissionDaysCount), 3)
                                End If
                            End If
                            .BalanceDays = PermissionDaysCount
                        End If
                    End If
                End If

                .StudyYear = StudyYear
                .Semester = Semester
                .FK_UniversityId = FK_UniversityId
                .Emp_GPAType = Emp_GPAType
                If Not Emp_GPA Is Nothing Then
                    .Emp_GPA = Convert.ToDecimal(Emp_GPA)
                Else
                    .Emp_GPA = Nothing
                End If
                .FK_MajorId = FK_MajorId_Study
                .FK_SpecializationId = FK_SpecializationId_Study

                If IS_Period(IsOneDay) Then
                    ' Periodically leave
                    .IsForPeriod = True

                    .PermDate = StartDate
                    .PermEndDate = IIf(CheckDate(EndDate) = Nothing,
                                       DateTime.MinValue, EndDate)

                Else
                    ' Non Periodically leave
                    .IsForPeriod = False

                    .PermDate = PermDate
                    .PermEndDate = Nothing
                End If
            End With
        End Sub

        Public Function GetPermissionRemainingBalance(ByVal PermTypeId As Integer, ByVal FK_EmployeeId As Integer, ByVal IsOneDay As Boolean, ByVal PermDate As DateTime,
                                                      ByVal StartDate As DateTime, ByVal EndDate As DateTime) As String
            If PermTypeId > 0 Then


                Dim dtRemainingBalance As New DataTable
                Dim FROM_DATE As DateTime
                Dim TO_DATE As DateTime
                FROM_DATE = DateSerial(PermDate.Year, PermDate.Month, 1)
                TO_DATE = DateSerial(PermDate.Year, PermDate.Month + 1, 0)
                dtRemainingBalance = objDALEmp_Permissions.Get_RemainingPermissionBalance(FK_EmployeeId, PermTypeId, FROM_DATE, TO_DATE)

                Return IIf(dtRemainingBalance.Rows(0)(3).ToString() = "", "00:00", dtRemainingBalance.Rows(0)(3))
            End If
        End Function

        'Public Function GetPermissionRemainingBalance(ByVal PermTypeId As Integer, ByVal FK_EmployeeId As Integer, ByVal IsOneDay As Boolean, ByVal PermDate As DateTime, _
        '                                              ByVal StartDate As DateTime, ByVal EndDate As DateTime) As String

        '    objPermissionsTypes = New PermissionsTypes()
        '    Dim objEmp_WorkSchedule As Emp_WorkSchedule

        '    Dim empId As Integer = FK_EmployeeId
        '    Dim permId As Integer = PermTypeId
        '    Dim remainingBalanceCount As Integer = 0
        '    Dim monthlyBalance As Integer = 0

        '    If (Not PermTypeId = -1) Then

        '        Dim objEmp_Permissions As New Emp_Permissions()
        '        objEmp_Permissions.FK_EmployeeId = empId
        '        objEmp_Permissions.FK_PermId = permId

        '        If IsOneDay Then
        '            objEmp_Permissions.FromTime = DateSerial(PermDate.Year, PermDate.Month, 1)
        '            objEmp_Permissions.ToTime = DateSerial(PermDate.Year, PermDate.Month + 1, 0)
        '        Else
        '            objEmp_Permissions.FromTime = DateSerial(StartDate.Year, StartDate.Month, 1)
        '            objEmp_Permissions.ToTime = DateSerial(EndDate.Year, EndDate.Month + 1, 0)
        '        End If

        '        Dim dtpermissionRemainingBalance As DataTable = objEmp_Permissions.GetAllDurationByEmployee()

        '        With objPermissionsTypes
        '            .PermId = permId
        '            .GetByPK()
        '            monthlyBalance = .MonthlyBalance
        '        End With

        '        For Each row As DataRow In dtpermissionRemainingBalance.Rows
        '            If (Not IsDBNull(row("ToTime"))) And (Not IsDBNull(row("FromTime"))) Then
        '                If Not IsDBNull(row("IsFullDay")) Then
        '                    Dim IsFullDay As Boolean = Convert.ToBoolean(row("IsFullDay"))
        '                    If Not IsFullDay Then
        '                        remainingBalanceCount += (Convert.ToDateTime(row("ToTime")).Subtract(Convert.ToDateTime(row("FromTime")))).TotalMinutes
        '                    Else
        '                        objEmp_WorkSchedule = New Emp_WorkSchedule()
        '                        objEmp_WorkSchedule.FK_EmployeeId = FK_EmployeeId
        '                        objEmp_WorkSchedule.ScheduleDate = PermDate
        '                        Dim arrTotalWorkHours As String() = objEmp_WorkSchedule.GetEmpScheduleWithTime().Split(",")
        '                        Dim intEmpTotalWorkHours As Integer = Convert.ToInt32(arrTotalWorkHours(arrTotalWorkHours.Length - 1))
        '                        remainingBalanceCount += intEmpTotalWorkHours
        '                    End If
        '                End If
        '            End If
        '        Next

        '    End If

        '    Dim dcmRemainingBalanceCount As Decimal = (monthlyBalance - remainingBalanceCount)

        '    Dim hours As Integer = dcmRemainingBalanceCount \ 60
        '    Dim minutes As Integer = dcmRemainingBalanceCount - (hours * 60)
        '    Dim strHours As String = "0"
        '    Dim strMinutes As String = "0"

        '    If minutes < 0 Then
        '        minutes = minutes * -1
        '    End If

        '    If minutes < 10 Then
        '        strMinutes = "0" + minutes.ToString()
        '    Else
        '        strMinutes = minutes
        '    End If

        '    If hours < 10 Then
        '        strHours = "0" + hours.ToString()
        '    Else
        '        strHours = hours
        '    End If

        '    Dim timeElapsed As String = CType(strHours, String) & ":" & CType(strMinutes, String)

        '    Return timeElapsed

        'End Function

        Private Function CheckDate(ByVal myDate As Object) As Object
            ' input : DateTime object
            ' Output : Nothing or a DateTime greater tha date time 
            ' minimum value
            ' Processing : Check the input if a valid date time to be used
            ' valid means greater than the minimum value and in valid format
            If IsDate(myDate) Then
                myDate =
                    IIf(myDate > DateTime.MinValue, myDate, Nothing)
                Return (myDate)
            Else
                Return Nothing
            End If
        End Function

        Private Function IS_Period(ByVal IsOneDay As Boolean) As Boolean
            Return Not IsOneDay
        End Function

        Public Function IsRequest() As Integer
            Dim dt As DataTable
            dt = New DataTable
            dt = objDALEmp_Permissions.CheckIsRequest(_PermissionId, _FK_EmployeeId)
            Return dt.Rows(0)(0)
        End Function

        Public Function GetByPK() As Emp_Permissions

            Dim dr As DataRow
            dr = objDALEmp_Permissions.GetByPK(_PermissionId)

            If Not IsDBNull(dr("PermissionId")) Then
                _PermissionId = dr("PermissionId")
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
            If Not IsDBNull(dr("FlexibilePermissionDuration")) Then
                _FlexibilePermissionDuration = dr("FlexibilePermissionDuration")
            End If
            If Not IsDBNull(dr("BalanceDays")) Then
                _BalanceDays = dr("BalanceDays")
            End If
            If Not IsDBNull(dr("AllowedTime")) Then
                _AllowedTime = dr("AllowedTime")
            End If
            If Not IsDBNull(dr("StudyYear")) Then
                _StudyYear = dr("StudyYear")
            End If
            If Not IsDBNull(dr("Semester")) Then
                _Semester = dr("Semester")
            End If
            If Not IsDBNull(dr("PermissionRequestId")) Then
                _PermissionRequestId = dr("PermissionRequestId")
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

        Public Function GetMultiPermissionsByEmployeeIDs(ByVal strEmpIDs As String) As DataTable

            Return objDALEmp_Permissions.GetMultiPermissionsByEmployeeIDs(strEmpIDs)

        End Function

        Public Function GetAllWithEmployeeInner() As DataTable

            Return objDALEmp_Permissions.GetAllWithEmployeeInner()

        End Function

        Public Function GetAll_ByPermMonthAndType(ByVal PermMonth As Integer) As DataTable
            Return objDALEmp_Permissions.GetAll_ByPermMonthAndType(_FK_EmployeeId, _FK_PermId, PermMonth)
        End Function

        Public Function GetAllDurationByEmployee() As DataTable

            Return objDALEmp_Permissions.GetAllDurationByEmployee(_FK_EmployeeId, _FromTime, _ToTime, _FK_PermId, _PermissionId)

        End Function

        Public Function FlexiblePermissionValidation(ByVal EmployeeId As Integer, ByVal objPermissionsTypes As PermissionsTypes, ByVal PermTypeId As Integer, ByVal StartDate As DateTime?, ByVal EndDate As DateTime?,
                                      ByVal FromTime As DateTime?, ByVal ToTime As DateTime?, ByVal FlexibleDuration As Integer, ByVal PermDate As DateTime, ByVal IsPermissionOneday As Boolean, ByVal IsFullyDay As Boolean, ByRef ErrorMsg As String, ByRef OffAndHolidayDays As String, ByRef EmpLeaveTotalBalance As Double, ByVal PermissionId As Integer) As Boolean
            objPermissionsTypes = New PermissionsTypes
            With objPermissionsTypes
                .PermId = PermTypeId
                Dim PermTypeInfo = .GetByPK()

                If (PermTypeInfo IsNot Nothing) Then

                    If Not FlexibleDuration = 0 Then
                        'Check Permission Min and Max Days
                        If Not CheckFlexiblePermissionsMinMaxDays(PermTypeInfo, FlexibleDuration, PermDate, IsFullyDay, ErrorMsg) Then
                            Return False
                        Else
                            'Count Off And Hoildays Days
                            OffAndHolidayDays = CalculateOffAndHolidaysDays(EmployeeId, StartDate, EndDate, PermTypeInfo, PermDate, IsPermissionOneday)

                            'Check Permission Allowed occurance for day, week and year
                            If Not CheckAllowedOccuranceForDayWeekMonth(EmployeeId, StartDate, EndDate, PermDate, PermTypeInfo, IsPermissionOneday, PermTypeId, PermissionId, ErrorMsg) Then
                                Return False
                            Else
                                'Check Permission Allowed duration for day, week and year
                                If Not CheckFlexibleAllowedDurationForDayWeekMonth(EmployeeId, StartDate, EndDate, FlexibleDuration, PermDate,
                                                                          IsPermissionOneday, PermTypeInfo, PermTypeId, PermissionId, ErrorMsg) Then
                                    Return False
                                End If
                            End If
                            'Validate Allowed Balance
                            If Not CheckEmpAllowedLeaveBalance(EmployeeId, PermTypeId, PermTypeInfo, EmpLeaveTotalBalance, StartDate, EndDate, FromTime, ToTime, IsFullyDay, FlexibleDuration, OffAndHolidayDays, ErrorMsg) Then
                                Return False
                            End If

                            If Not CheckEmpAllowedPermissionDuration(PermTypeId, EmployeeId, IsPermissionOneday, PermDate, StartDate, EndDate, FromTime, ToTime, PermissionId, ErrorMsg) Then
                                Return False
                            End If
                        End If

                    Else
                        'Count Off And Hoildays Days
                        OffAndHolidayDays = CalculateOffAndHolidaysDays(EmployeeId, StartDate, EndDate, PermTypeInfo, PermDate, IsPermissionOneday)

                        'Check Permission Allowed occurance for day, week and year
                        If Not CheckAllowedOccuranceForDayWeekMonth(EmployeeId, StartDate, EndDate, PermDate, PermTypeInfo, IsPermissionOneday, PermTypeId, PermissionId, ErrorMsg) Then
                            Return False
                        Else
                            'Check Permission Allowed duration for day, week and year
                            If Not CheckAllowedDurationForDayWeekMonth(EmployeeId, StartDate, EndDate, FromTime, ToTime, PermDate,
                                                                      IsPermissionOneday, PermTypeInfo, PermTypeId, PermissionId, ErrorMsg) Then
                                Return False
                            End If
                        End If
                        'Validate Allowed Balance
                        If Not CheckEmpAllowedLeaveBalance(EmployeeId, PermTypeId, PermTypeInfo, EmpLeaveTotalBalance, StartDate, EndDate, FromTime, ToTime, IsFullyDay, FlexibleDuration, OffAndHolidayDays, ErrorMsg) Then
                            Return False
                        End If

                        If Not CheckEmpAllowedPermissionDuration(PermTypeId, EmployeeId, IsPermissionOneday, PermDate, StartDate, EndDate, FromTime, ToTime, PermissionId, ErrorMsg) Then
                            Return False
                        End If
                    End If
                    If Not IsAllowedForSpecificEntity(EmployeeId, PermTypeId, ErrorMsg) Then
                        Return False
                    End If

                    If Not IsAllowedForSpecificCompany(EmployeeId, PermTypeId, ErrorMsg) Then
                        Return False
                    End If
                    Return True
                End If
            End With
            Return True
        End Function

        Public Function CheckFlexibleAllowedDurationForDayWeekMonth(ByVal EmployeeId As Integer, ByVal StartDate As DateTime?, ByVal EndDate As DateTime?, ByVal FlexibleDuration As Integer,
                                                     ByVal PermDate As DateTime, ByVal IsPermissionOnedDay As Boolean, ByVal PermTypeInfo As PermissionsTypes, ByVal PermTypeId As Integer, ByVal PermissionId As Integer, ByRef ErrorMessage As String) As Boolean
            Dim drDuration As DataRow
            objRamadanPeriod = New RamadanPeriod
            With objPermissionTypeDuration
                .FK_PermId = PermTypeId
                Dim dtPermissionDuration As New DataTable
                dtPermissionDuration = .GetByPK()
                If (dtPermissionDuration IsNot Nothing AndAlso dtPermissionDuration.Rows.Count > 0) Then
                    'Check validation for allowed duration
                    If (IsPermissionOnedDay) Then
                        If Not FlexibleDuration = 0 Then
                            'Check validation for a week
                            If (dtPermissionDuration.Select("FK_DurationId = 2").Length > 0) Then
                                drDuration = dtPermissionDuration.Select("FK_DurationId = 2")(0)

                                'Get Occurance Per Week for this employee
                                objEmpPermissions = New Emp_Permissions
                                With objEmpPermissions
                                    .PermDate = PermDate
                                    .FK_EmployeeId = EmployeeId
                                    Dim PermissionDuration = .GetDurationForWeek()

                                    Dim DurationMinutes As Integer
                                    If PermissionId = 0 Then
                                        DurationMinutes = FlexibleDuration
                                    Else
                                        objEmpPermissions.PermissionId = PermissionId
                                        objEmpPermissions.GetByPK()
                                        DurationMinutes = objEmpPermissions.FlexibilePermissionDuration
                                    End If
                                    If (DurationMinutes < 0) Then
                                        DurationMinutes = DurationMinutes * -1
                                    End If

                                    '-----------Check Ramadan Day------------'
                                    objRamadanPeriod.SearchDate = PermDate
                                    objRamadanPeriod.Get_IsRamadan()

                                    If objRamadanPeriod.IsRamadanDay = True Then
                                        If (PermissionDuration + DurationMinutes > drDuration("MaximumRamadanDuration")) Then
                                            If SessionVariables.CultureInfo = "ar-JO" Then
                                                ErrorMessage = "الفترة المسموحة في رمضان خلال اسبوع: " & drDuration("MaximumRamadanDuration") & " دقائق"
                                            Else
                                                ErrorMessage = "Allowed durations in Ramadan per week: " & drDuration("MaximumRamadanDuration") & " minutes"
                                            End If

                                            Return False
                                        End If
                                    Else
                                        If (PermissionDuration + DurationMinutes > drDuration("MaximumDuration")) Then
                                            If SessionVariables.CultureInfo = "ar-JO" Then
                                                ErrorMessage = "الفترة المسموحة خلال اسبوع: " & drDuration("MaximumDuration") & " دقائق"
                                            Else
                                                ErrorMessage = "Allowed durations per week: " & drDuration("MaximumDuration") & " minutes"
                                            End If

                                            Return False
                                        End If
                                    End If
                                    '-----------Check Ramadan Day------------'

                                End With
                            End If

                            'Check Validation for a Month
                            If (dtPermissionDuration.Select("FK_DurationId = 3").Length > 0) Then
                                drDuration = dtPermissionDuration.Select("FK_DurationId = 3")(0)

                                'Get Occurance Per Month for this employee
                                objEmpPermissions = New Emp_Permissions
                                With objEmpPermissions
                                    .PermDate = PermDate
                                    .FK_EmployeeId = EmployeeId
                                    Dim PermissionDuration = .GetDurationForMonth()

                                    Dim DurationMinutes As Integer
                                    If PermissionId = 0 Then
                                        DurationMinutes = FlexibleDuration
                                    Else
                                        objEmpPermissions.PermissionId = PermissionId
                                        objEmpPermissions.GetByPK()
                                        DurationMinutes = objEmpPermissions.FlexibilePermissionDuration
                                    End If
                                    If (DurationMinutes < 0) Then
                                        DurationMinutes = DurationMinutes * -1
                                    End If

                                    '-----------Check Ramadan Day------------'
                                    objRamadanPeriod.SearchDate = PermDate
                                    objRamadanPeriod.Get_IsRamadan()

                                    If objRamadanPeriod.IsRamadanDay = True Then
                                        If (PermissionDuration + DurationMinutes > drDuration("MaximumRamadanDuration")) Then

                                            If SessionVariables.CultureInfo = "ar-JO" Then
                                                ErrorMessage = "الفترة المسموحة في رمضان خلال شهر: " & drDuration("MaximumRamadanDuration") & " دقائق"
                                            Else
                                                ErrorMessage = "Allowed durations in Ramadan per month: " & drDuration("MaximumRamadanDuration") & " minutes"
                                            End If
                                            Return False
                                        End If
                                    Else
                                        If (PermissionDuration + DurationMinutes > drDuration("MaximumDuration")) Then

                                            If SessionVariables.CultureInfo = "ar-JO" Then
                                                ErrorMessage = "الفترة المسموحة خلال شهر: " & drDuration("MaximumDuration") & " دقائق"
                                            Else
                                                ErrorMessage = "Allowed durations per month: " & drDuration("MaximumDuration") & " minutes"
                                            End If
                                            Return False
                                        End If
                                    End If
                                    '-----------Check Ramadan Day------------'

                                End With
                            End If

                            'Check Validation for a Year
                            If (dtPermissionDuration.Select("FK_DurationId = 4").Length > 0) Then
                                drDuration = dtPermissionDuration.Select("FK_DurationId = 4")(0)

                                'Get Occurance Per Year for this employee
                                objEmpPermissions = New Emp_Permissions
                                With objEmpPermissions
                                    .PermDate = PermDate
                                    .FK_EmployeeId = EmployeeId
                                    Dim PermissionDuration = .GetDurationForYear()

                                    Dim DurationMinutes As Integer
                                    If PermissionId = 0 Then
                                        DurationMinutes = FlexibleDuration
                                    Else
                                        objEmpPermissions.PermissionId = PermissionId
                                        objEmpPermissions.GetByPK()
                                        DurationMinutes = objEmpPermissions.FlexibilePermissionDuration
                                    End If
                                    If (DurationMinutes < 0) Then
                                        DurationMinutes = DurationMinutes * -1
                                    End If

                                    '-----------Check Ramadan Day------------'
                                    objRamadanPeriod.SearchDate = PermDate
                                    objRamadanPeriod.Get_IsRamadan()

                                    If objRamadanPeriod.IsRamadanDay = True Then
                                        If (PermissionDuration + DurationMinutes > drDuration("MaximumRamadanDuration")) Then
                                            If SessionVariables.CultureInfo = "ar-JO" Then
                                                ErrorMessage = "الفترة المسموحة في رمضان خلال سنة: " & drDuration("MaximumRamadanDuration") & " دقائق"
                                            Else
                                                ErrorMessage = "Allowed durations In Ramadan per year: " & drDuration("MaximumRamadanDuration") & " minutes"
                                            End If
                                            Return False
                                        End If
                                    Else
                                        If (PermissionDuration + DurationMinutes > drDuration("MaximumDuration")) Then
                                            If SessionVariables.CultureInfo = "ar-JO" Then
                                                ErrorMessage = "الفترة المسموحة خلال سنة: " & drDuration("MaximumDuration") & " دقائق"
                                            Else
                                                ErrorMessage = "Allowed durations per year: " & drDuration("MaximumDuration") & " minutes"
                                            End If
                                            Return False
                                        End If
                                    End If
                                    '-----------Check Ramadan Day------------'
                                End With
                            End If

                            'Check Validation for a Day
                            If (dtPermissionDuration.Select("FK_DurationId = 1").Length > 0) Then
                                drDuration = dtPermissionDuration.Select("FK_DurationId = 1")(0)

                                'Get Occurance Per Year for this employee
                                objEmpPermissions = New Emp_Permissions
                                With objEmpPermissions
                                    .PermDate = PermDate
                                    .FK_EmployeeId = EmployeeId
                                    Dim PermissionDuration = .GetDurationForDay()

                                    Dim DurationMinutes As Integer
                                    If PermissionId = 0 Then
                                        DurationMinutes = FlexibleDuration
                                    Else
                                        objEmpPermissions.PermissionId = PermissionId
                                        objEmpPermissions.GetByPK()
                                        DurationMinutes = objEmpPermissions.FlexibilePermissionDuration
                                    End If
                                    If (DurationMinutes < 0) Then
                                        DurationMinutes = DurationMinutes * -1
                                    End If

                                    '-----------Check Ramadan Day------------'
                                    objRamadanPeriod.SearchDate = PermDate
                                    objRamadanPeriod.Get_IsRamadan()

                                    If objRamadanPeriod.IsRamadanDay = True Then
                                        If (PermissionDuration + DurationMinutes > drDuration("MaximumRamadanDuration")) Then
                                            If SessionVariables.CultureInfo = "ar-JO" Then
                                                ErrorMessage = "الفترة المسموحة في رمضان خلال يوم: " & drDuration("MaximumRamadanDuration") & " دقائق"
                                            Else
                                                ErrorMessage = "Allowed durations in Ramadan per day: " & drDuration("MaximumRamadanDuration") & " minutes"
                                            End If
                                            Return False
                                        End If
                                    Else
                                        If (PermissionDuration + DurationMinutes > drDuration("MaximumDuration")) Then
                                            If SessionVariables.CultureInfo = "ar-JO" Then
                                                ErrorMessage = "الفترة المسموحة خلال يوم: " & drDuration("MaximumDuration") & " دقائق"
                                            Else
                                                ErrorMessage = "Allowed durations per day: " & drDuration("MaximumDuration") & " minutes"
                                            End If
                                            Return False
                                        End If
                                    End If
                                    '-----------Check Ramadan Day------------'

                                End With
                            End If
                        End If
                    ElseIf (Not IsPermissionOnedDay) Then
                        'Check validation for a week
                        If (dtPermissionDuration.Select("FK_DurationId = 2").Length > 0) Then
                            drDuration = dtPermissionDuration.Select("FK_DurationId = 2")(0)

                            'Get Occurance Per Week for this employee
                            objEmpPermissions = New Emp_Permissions
                            With objEmpPermissions
                                .PermDate = PermDate
                                .FK_EmployeeId = EmployeeId
                                Dim PermissionDuration = .GetDurationForWeek()

                                Dim DaysCount = (EndDate - StartDate).Value.Days + 1
                                Dim DurationMinutes As Integer
                                If PermissionId = 0 Then
                                    DurationMinutes = FlexibleDuration
                                Else
                                    objEmpPermissions.PermissionId = PermissionId
                                    objEmpPermissions.GetByPK()
                                    DurationMinutes = objEmpPermissions.FlexibilePermissionDuration
                                End If
                                If (DurationMinutes < 0) Then
                                    DurationMinutes = (DurationMinutes) * -1
                                End If
                                Dim TotalMinutes = DurationMinutes * DaysCount

                                '-----------Check Ramadan Day------------'
                                While StartDate <= EndDate
                                    objRamadanPeriod.SearchDate = StartDate
                                    objRamadanPeriod.Get_IsRamadan()

                                    If objRamadanPeriod.IsRamadanDay = True Then
                                        If (PermissionDuration + TotalMinutes > drDuration("MaximumRamadanDuration")) Then
                                            If SessionVariables.CultureInfo = "ar-JO" Then
                                                ErrorMessage = "الفترة المسموحة في رمضان خلال اسبوع: " & drDuration("MaximumRamadanDuration") & " دقائق"
                                            Else
                                                ErrorMessage = "Allowed durations in Ramadan per week: " & drDuration("MaximumRamadanDuration") & " minutes"
                                            End If
                                            Return False
                                        End If
                                    Else
                                        If (PermissionDuration + TotalMinutes > drDuration("MaximumDuration")) Then
                                            If SessionVariables.CultureInfo = "ar-JO" Then
                                                ErrorMessage = "الفترة المسموحة خلال اسبوع: " & drDuration("MaximumDuration") & " دقائق"
                                            Else
                                                ErrorMessage = "Allowed durations per week: " & drDuration("MaximumDuration") & " minutes"
                                            End If

                                            Return False
                                        End If
                                    End If
                                    StartDate = (DateAdd(DateInterval.Day, 1, Convert.ToDateTime(StartDate)))
                                End While
                                '-----------Check Ramadan Day------------'

                            End With
                        End If

                        'Check Validation for a Month
                        If (dtPermissionDuration.Select("FK_DurationId = 3").Length > 0) Then
                            drDuration = dtPermissionDuration.Select("FK_DurationId = 3")(0)

                            'Get Occurance Per Month for this employee
                            objEmpPermissions = New Emp_Permissions
                            With objEmpPermissions
                                .PermDate = PermDate
                                .FK_EmployeeId = EmployeeId
                                Dim PermissionDuration = .GetDurationForMonth()

                                Dim DaysCount = (EndDate - StartDate).Value.Days + 1

                                Dim DurationMinutes As Integer
                                If PermissionId = 0 Then
                                    DurationMinutes = FlexibleDuration
                                Else
                                    objEmpPermissions.PermissionId = PermissionId
                                    objEmpPermissions.GetByPK()
                                    DurationMinutes = objEmpPermissions.FlexibilePermissionDuration
                                End If
                                If (DurationMinutes < 0) Then
                                    DurationMinutes = (DurationMinutes) * -1
                                End If
                                Dim TotalMinutes = DurationMinutes * DaysCount

                                '-----------Check Ramadan Day------------'
                                While StartDate <= EndDate
                                    objRamadanPeriod.SearchDate = StartDate
                                    objRamadanPeriod.Get_IsRamadan()

                                    If objRamadanPeriod.IsRamadanDay = True Then
                                        If (PermissionDuration + TotalMinutes > drDuration("MaximumRamadanDuration")) Then
                                            If SessionVariables.CultureInfo = "ar-JO" Then
                                                ErrorMessage = "الفترة المسموحة في رمضان خلال شهر: " & drDuration("MaximumRamadanDuration") & " دقائق"
                                            Else
                                                ErrorMessage = "Allowed durations in Ramadan per month: " & drDuration("MaximumRamadanDuration") & " minutes"
                                            End If
                                            Return False
                                        End If
                                    Else
                                        If (PermissionDuration + TotalMinutes > drDuration("MaximumDuration")) Then
                                            If SessionVariables.CultureInfo = "ar-JO" Then
                                                ErrorMessage = "الفترة المسموحة خلال شهر: " & drDuration("MaximumDuration") & " دقائق"
                                            Else
                                                ErrorMessage = "Allowed durations per month: " & drDuration("MaximumDuration") & " minutes"
                                            End If
                                            Return False
                                        End If
                                    End If
                                    StartDate = (DateAdd(DateInterval.Day, 1, Convert.ToDateTime(StartDate)))
                                End While
                                '-----------Check Ramadan Day------------'

                            End With
                        End If

                        'Check Validation for a Year
                        If (dtPermissionDuration.Select("FK_DurationId = 4").Length > 0) Then
                            drDuration = dtPermissionDuration.Select("FK_DurationId = 4")(0)

                            'Get Occurance Per Year for this employee
                            objEmpPermissions = New Emp_Permissions
                            With objEmpPermissions
                                .PermDate = PermDate
                                .FK_EmployeeId = EmployeeId
                                Dim PermissionDuration = .GetDurationForYear()

                                Dim DaysCount = (EndDate - StartDate).Value.Days + 1
                                Dim DurationMinutes As Integer
                                If PermissionId = 0 Then
                                    DurationMinutes = FlexibleDuration
                                Else
                                    objEmpPermissions.PermissionId = PermissionId
                                    objEmpPermissions.GetByPK()
                                    DurationMinutes = objEmpPermissions.FlexibilePermissionDuration
                                End If
                                If (DurationMinutes < 0) Then
                                    DurationMinutes = (DurationMinutes) * -1
                                End If
                                Dim TotalMinutes = DurationMinutes * DaysCount

                                '-----------Check Ramadan Day------------'
                                While StartDate <= EndDate
                                    objRamadanPeriod.SearchDate = StartDate
                                    objRamadanPeriod.Get_IsRamadan()

                                    If objRamadanPeriod.IsRamadanDay = True Then
                                        If (PermissionDuration + TotalMinutes > drDuration("MaximumRamadanDuration")) Then
                                            If SessionVariables.CultureInfo = "ar-JO" Then
                                                ErrorMessage = "الفترة المسموحة في رمضان خلال سنة: " & drDuration("MaximumRamadanDuration") & " دقائق"
                                            Else
                                                ErrorMessage = "Allowed durations in Ramadan per year: " & drDuration("MaximumRamadanDuration") & " minutes"

                                            End If
                                            Return False
                                        End If
                                    Else
                                        If (PermissionDuration + TotalMinutes > drDuration("MaximumDuration")) Then
                                            If SessionVariables.CultureInfo = "ar-JO" Then
                                                ErrorMessage = "الفترة المسموحة خلال سنة: " & drDuration("MaximumDuration") & " دقائق"
                                            Else
                                                ErrorMessage = "Allowed durations per year: " & drDuration("MaximumDuration") & " minutes"

                                            End If
                                            Return False
                                        End If
                                    End If
                                    StartDate = (DateAdd(DateInterval.Day, 1, Convert.ToDateTime(StartDate)))
                                End While
                                '-----------Check Ramadan Day------------'
                            End With
                        End If

                        'Check Validation for a Day
                        If (dtPermissionDuration.Select("FK_DurationId = 1").Length > 0) Then
                            drDuration = dtPermissionDuration.Select("FK_DurationId = 1")(0)

                            'Get Occurance Per Year for this employee
                            objEmpPermissions = New Emp_Permissions
                            With objEmpPermissions
                                .PermDate = PermDate
                                .FK_EmployeeId = EmployeeId
                                Dim PermissionDuration = .GetDurationForDay()

                                Dim DaysCount = (EndDate - StartDate).Value.Days + 1
                                Dim DurationMinutes As Integer
                                If PermissionId = 0 Then
                                    DurationMinutes = FlexibleDuration
                                Else
                                    objEmpPermissions.PermissionId = PermissionId
                                    objEmpPermissions.GetByPK()
                                    DurationMinutes = objEmpPermissions.FlexibilePermissionDuration
                                End If
                                If (DurationMinutes < 0) Then
                                    DurationMinutes = (DurationMinutes) * -1
                                End If
                                Dim TotalMinutes = DurationMinutes * DaysCount

                                '-----------Check Ramadan Day------------'
                                While StartDate <= EndDate
                                    objRamadanPeriod.SearchDate = StartDate
                                    objRamadanPeriod.Get_IsRamadan()

                                    If objRamadanPeriod.IsRamadanDay = True Then
                                        If (PermissionDuration + TotalMinutes > drDuration("MaximumRamadanDuration")) Then
                                            If SessionVariables.CultureInfo = "ar-JO" Then
                                                ErrorMessage = "الفترة المسموحة في رمضان خلال يوم: " & drDuration("MaximumRamadanDuration") & " دقائق"
                                            Else
                                                ErrorMessage = "Allowed durations in Ramadan per day: " & drDuration("MaximumRamadanDuration") & " minutes"

                                            End If
                                            Return False
                                        End If
                                    Else
                                        If (PermissionDuration + TotalMinutes > drDuration("MaximumDuration")) Then
                                            If SessionVariables.CultureInfo = "ar-JO" Then
                                                ErrorMessage = "الفترة المسموحة خلال يوم: " & drDuration("MaximumDuration") & " دقائق"
                                            Else
                                                ErrorMessage = "Allowed durations per day: " & drDuration("MaximumDuration") & " minutes"

                                            End If
                                            Return False
                                        End If
                                    End If
                                    StartDate = (DateAdd(DateInterval.Day, 1, Convert.ToDateTime(StartDate)))
                                End While
                                '-----------Check Ramadan Day------------'

                            End With
                        End If

                    End If
                End If
            End With
            Return True
        End Function

        Public Function CheckFlexiblePermissionsMinMaxDays(ByVal PermTypeInfo As PermissionsTypes, ByVal FlexibleDuration As Integer,
                                             ByVal PermDate As DateTime, ByVal IsFullyDay As Boolean, ByRef ErrorMsg As String) As Boolean

            Dim MinDuration As Integer
            Dim MaxDuration As Integer

            If Not PermTypeInfo Is Nothing Then
                MinDuration = PermTypeInfo.MinDuration
                MaxDuration = PermTypeInfo.MaxDuration
            Else
                MinDuration = 30
                MaxDuration = 300
            End If

            Dim Minutes = FlexibleDuration
            If (Minutes < 0) Then
                Minutes = Minutes * -1
            End If

            If Not IsFullyDay Then
                '    If MaxDuration < 420 Then
                '        If SessionVariables.CultureInfo = "ar-JO" Then
                '            ErrorMsg = "الحد الادنى المسموح به لمغادرة اليوم الواحد هو 420"
                '        Else
                '            ErrorMsg = "The Minimum full day permission duration is 420"
                '        End If
                '        Return False
                '    End If
                'Else
                If (Minutes < MinDuration) Then
                    If SessionVariables.CultureInfo = "ar-JO" Then
                        ErrorMsg = "الحد الادنى لمدة المغادرة المسموح به هو: " & MinDuration & " دقائق"
                    Else
                        ErrorMsg = "The Minimum permission duration is: " & MinDuration & " minutes"
                    End If
                    Return False
                ElseIf (Minutes > MaxDuration) Then
                    If SessionVariables.CultureInfo = "ar-JO" Then
                        ErrorMsg = "الحد الاقصى لمدة المغادرة المسموح به هو: " & MinDuration & " دقائق"
                    Else
                        ErrorMsg = "The Maximum permission duration is: " & MaxDuration & " minutes"
                    End If
                    Return False
                End If
            End If

            Return True
        End Function

        Public Function ValidateEmployeeFlexiblePermission(ByVal EmployeeId As Integer, ByVal PermissionId As Integer, ByVal dtCurrent As DataTable, ByVal IsPermissionOneday As Boolean,
                                                           ByVal StartDate As DateTime?, ByVal EndDate As DateTime?,
                                             ByVal PermDate As DateTime, ByVal FromTime As DateTime?,
                                            ByVal ToTime As DateTime?, ByVal objPermissionsTypes As PermissionsTypes, ByVal PermTypeId As Integer,
                                             ByVal IsFullyDay As Boolean, ByRef ErrorMessage As String, ByRef OffAndHolidayDays As String,
                                            ByVal FlexibleDuration As Integer, ByRef EmpLeaveTotalBalance As Double) As Boolean

            If IsExistsFullDay(dtCurrent, IsPermissionOneday, StartDate, EndDate, FromTime, ToTime, PermissionId, PermDate) = True Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    ErrorMessage = " يوجد طلب مغادرة مسبقاً ليوم كامل في نفس التاريخ"
                Else
                    ErrorMessage = "Permission Record Already Exists for full day in the same date"
                End If

                Return False
                'ElseIf IsExists(dtCurrent, IsPermissionOneday, StartDate, EndDate, FromTime, ToTime, PermissionId, PermDate) = True Then
                '    If SessionVariables.CultureInfo = "ar-JO" Then
                '        ErrorMessage = " يوجد طلب مغادرة مسبقاً ضمن الفترة المطلوبة"
                '    Else
                '        ErrorMessage = "Permission Record Already Exists Between The Date and Time Range"
                '    End If

                '    Return False

            ElseIf IsAllowedAfterSpecific(PermTypeId, FromTime) = True Then
                Dim strAllowAfter As String
                strAllowAfter = CtlCommon.GetFullTimeString(AllowAfter)
                strAllowAfter = strAllowAfter(0) + strAllowAfter(1) + ":" + strAllowAfter(2) + strAllowAfter(3)

                If SessionVariables.CultureInfo = "ar-JO" Then
                    ErrorMessage = " لايمكن تقديم مغادرة قبل " + " " + strAllowAfter
                Else
                    ErrorMessage = "Permssion Cannot be Requested Before" + " " + strAllowAfter
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

            If Not FlexiblePermissionValidation(EmployeeId, objPermissionsTypes, PermTypeId, StartDate, EndDate, FromTime, ToTime, FlexibleDuration, PermDate, IsPermissionOneday, IsFullyDay, ErrorMessage, OffAndHolidayDays, EmpLeaveTotalBalance, PermissionId) Then
                Return False
            Else
                Return True
            End If
        End Function

        Public Function Get_Permission_InsideWork() As DataTable
            Return objDALEmp_Permissions.Get_Permission_InsideWork(_FK_EmployeeId, _PermDate, _FromTime, _ToTime, _IsValidInSchedule, _IsOff, _IsHoliday)
        End Function

        Public Function CheckHasPermissionDuringTime() As Boolean
            Dim dr As DataRow
            dr = objDALEmp_Permissions.CheckHasPermissionDuringTime(_FK_EmployeeId, _M_Date, FromTime, ToTime)
            If Not dr Is Nothing Then
                If Not IsDBNull(dr("PermissionId")) Then
                    _PermissionId = dr("PermissionId")
                End If
                If PermissionId = 0 Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return False

            End If

        End Function

        Public Function Get_Previous_Restday_Date() As DataTable

            Return objDALEmp_Permissions.Get_Previous_Restday_Date(_FK_EmployeeId, _PermDate)

        End Function

        Public Function Get_Next_Restday_Date() As DataTable

            Return objDALEmp_Permissions.Get_Next_Restday_Date(_FK_EmployeeId, _PreviousPermDate)

        End Function

        Public Function CheckEmployeeTransaction(ByVal EmployeeId As Integer, ByVal PermDate As DateTime, ByVal PermEndDate As DateTime?, ByVal PermTypeInfo As PermissionsTypes,
                                                     ByVal IsPermissionOneDay As Boolean, ByVal PermTypeId As Integer, ByRef ErrorMsg As String) As Boolean

            Dim dt As DataTable
            objPermissionsTypes = New PermissionsTypes
            With objPermissionsTypes
                .PermId = PermTypeId
                .GetByPK()

                If .MustHaveTransaction = True Then
                    If IsPermissionOneDay = True Then
                        If Convert.ToDateTime(PermDate) = Convert.ToDateTime(Date.Today) Then
                            dt = objDALEmp_Permissions.Get_EmployeeTransaction(EmployeeId, PermDate)
                            If dt Is Nothing Then
                                If SessionVariables.CultureInfo = "ar-JO" Then
                                    ErrorMsg = "يجب ان يكون للموظف حركة دخول حتى تتمكن من طلب هذا النوع من المغادرة"
                                Else
                                    ErrorMsg = "Employee Must Have In Transaction In Order To Request The Selected Permission Type"
                                End If
                                Return False
                            Else
                                If Not dt Is Nothing Then
                                    If dt.Rows.Count = 0 Then
                                        If SessionVariables.CultureInfo = "ar-JO" Then
                                            ErrorMsg = "يجب ان يكون للموظف حركة دخول حتى تتمكن من طلب هذا النوع من المغادرة"
                                        Else
                                            ErrorMsg = "Employee Must Have In Transaction In Order To Request The Selected Permission Type"
                                        End If
                                        Return False
                                    End If
                                End If
                            End If
                        End If
                    Else
                        If (Convert.ToDateTime(PermDate) = Convert.ToDateTime(PermEndDate)) And (Convert.ToDateTime(PermDate) = Convert.ToDateTime(Date.Today)) Then
                            dt = objDALEmp_Permissions.Get_EmployeeTransaction(EmployeeId, PermDate)
                            If dt Is Nothing Then
                                If SessionVariables.CultureInfo = "ar-JO" Then
                                    ErrorMsg = "يجب ان يكون للموظف حركة دخول حتى تتمكن من طلب هذا النوع من المغادرة"
                                Else
                                    ErrorMsg = "Employee Must Have In Transaction In Order To Request The Selected Permission Type"
                                End If
                                Return False
                            Else
                                If Not dt Is Nothing Then
                                    If dt.Rows.Count = 0 Then
                                        If SessionVariables.CultureInfo = "ar-JO" Then
                                            ErrorMsg = "يجب ان يكون للموظف حركة دخول حتى تتمكن من طلب هذا النوع من المغادرة"
                                        Else
                                            ErrorMsg = "Employee Must Have In Transaction In Order To Request The Selected Permission Type"
                                        End If
                                        Return False
                                    End If
                                End If
                            End If
                        End If
                    End If

                End If
            End With
            Return True
        End Function

        Public Function Get_RestPermission_RemainingBalance() As DataTable

            Return objDALEmp_Permissions.Get_RestPermission_RemainingBalance(_FK_EmployeeId, _PermDate)

        End Function

        Public Function CheckDelayPermissionsCount(ByVal EmployeeId As Integer, ByVal PermDate As DateTime, ByVal PermEndDate As DateTime?, ByVal PermTypeInfo As PermissionsTypes,
                                                     ByVal IsPermissionOneDay As Boolean, ByVal PermTypeId As Integer, ByVal FromTime As DateTime, ByRef ErrorMsg As String) As Boolean

            Dim dt As DataTable
            objPermissionsTypes = New PermissionsTypes
            objDALEmp_Permissions = New DALEmp_Permissions
            With objPermissionsTypes
                .PermId = PermTypeId
                .GetByPK()
                If .ValidateDelayPermissions = True Then
                    If IsPermissionOneDay = True Then
                        dt = objDALEmp_Permissions.Get_DelayPermissions(EmployeeId, PermDate, PermTypeId, FromTime)
                        If Not dt Is Nothing Then
                            If dt.Rows(0)("DelayPermissionCount").ToString = "0" Then
                                If SessionVariables.CultureInfo = "ar-JO" Then
                                    ErrorMsg = "لقد وصل نوع المغادرة التي قمت بإختيارها الى الحد الذي يمكنك من طلب تصريح التأخير خلال الشهر"
                                Else
                                    ErrorMsg = "The Selected Permission Type Reached The Allowed Permission Delay Limit Per Month"
                                End If
                                Return False
                            End If
                        End If
                    End If
                End If
            End With
            Return True
        End Function

        Public Function Get_DayStatus() As DataTable

            Return objDALEmp_Permissions.Get_DayStatus(_FK_EmployeeId, _PermDate, _PermEndDate)

        End Function

#End Region

#Region "Extended Methods"


        Public Function GetAllInnerJoin() As DataTable



            Return objDALEmp_Permissions.GetAllInnerJoin(FK_EmployeeId, PermissionOption, PermDate, IIf(PermEndDate Is Nothing, DBNull.Value, PermEndDate.Value))
        End Function


        Public Function Find_Existing() As Boolean

            Return objDALEmp_Permissions.FindExisting(_PermissionId)

        End Function

        Public Function Emp_Permissions_IsExistNursingStudy(ByRef ErrorMessage As String) As Boolean
            Dim dt As DataTable
            dt = New DataTable
            dt = objDALEmp_Permissions.IsExistNursingStudy(_FK_EmployeeId, _PermissionOption, _PermDate, _PermEndDate, _Days)

            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then

                    If SessionVariables.CultureInfo = "ar-JO" Then
                        ErrorMessage = " يوجد طلب مغادرة مسبقاً ضمن الفترة المطلوبة او خلال الايام المطلوبة"
                    Else
                        ErrorMessage = "Permission Record Already Exists Between The Date -Day- and Time Range"
                    End If
                    Return True
                Else
                    Return False

                End If
            Else
                Return False
            End If
        End Function

#End Region

    End Class
End Namespace