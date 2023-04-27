Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports TA.LookUp
Imports SmartV.DB
Imports SmartV.UTILITIES


Namespace TA.Employees

    Public Class DALEmp_Permissions
        Inherits MGRBase

#Region "Class Variables"
        Private strConn As String
        Private Emp_Permissions_Select As String = "Emp_Permissions_select"
        Private Emp_Permissions_Select_All As String = "Emp_Permissions_select_All"
        Private Emp_Permissions_Insert As String = "Emp_Permissions_Insert"
        Private Emp_Permissions_Update As String = "Emp_Permissions_Update"
        Private Emp_Permissions_Delete As String = "Emp_Permissions_Delete"
        Private Emp_Permissions_Search As String = "Emp_Permissions_Search"
        Private Emp_Permissions_Update_Attachment As String = "Emp_Permissions_Update_Attachment"
        Private Emp_Permissions_SelectAllByEmployee As String = "Emp_Permissions_SelectAllByEmployee"
        Private PermissionOccurancePerWeek_select As String = "PermissionOccurancePerWeek_select"
        Private PermissionOccurancePerMonth_select As String = "PermissionOccurancePerMonth_select"
        Private PermissionOccurancePerYear_select As String = "PermissionOccurancePerYear_select"
        Private PermissionOccurancePerDay_select As String = "PermissionOccurancePerDay_select"
        Private PermissionDurationPerWeek_select As String = "PermissionDurationPerWeek_select"
        Private PermissionDurationPerMonth_select As String = "PermissionDurationPerMonth_select"
        Private PermissionDurationPerYear_select As String = "PermissionDurationPerYear_select"
        Private PermissionDurationPerDay_select As String = "PermissionDurationPerDay_select"
        Private Get_Emp_Permissions_ByMultiEmployees As String = "Get_Emp_Permissions_ByMultiEmployees"
        Private Get_Emp_Permissions_WithEmployeeInner As String = "Get_Emp_Permissions_WithEmployeeInner"
        Private GetEmployeePermission_ByPermMonthandType As String = "GetEmployeePermission_ByPermMonthandType"
        Private Emp_PermissionsDuration_SelectAllByEmployee As String = "Emp_PermissionsDuration_SelectAllByEmployee"
        Private CALC_EFFECTIVE_SCHEDULE_WithFlix2 As String = "CALC_EFFECTIVE_SCHEDULE_WithFlix2"
        Private Emp_permssion_ValidateInsideSchedule As String = "Emp_permssion_ValidateInsideSchedule"
        Private Emp_Permissions_IsExist As String = "Emp_Permissions_IsExist"
        Private GetRemainingPermissionBalance As String = "GetRemainingPermissionBalance"
        Private Emp_Permissions_IsExistNursingStudy As String = "Emp_Permissions_IsExistNursingStudy"
        Private IsRequest As String = "IsRequest"
        Private Emp_Permissions_Select_Restday_Date As String = "Emp_Permissions_Select_Restday_Date"
        Private Emp_Permissions_Select_NextRestday_Date As String = "Emp_Permissions_Select_NextRestday_Date"
        Private Emp_Permissions_Has_StudyOrNursing_Permission As String = "Emp_Permissions_Has_StudyOrNursing_Permission"
        Private Emp_Permissions_EligibleToRequest_Additional_IFStudyorNursing As String = "Emp_Permissions_EligibleToRequest_Additional_IFStudyorNursing"
        Private Emp_Permissions_Check_EmployeeTransaction As String = "Emp_Permissions_Check_EmployeeTransaction"
        Private Emp_Permissions_RestPermission_RemainingBalance As String = "Emp_Permissions_RestPermission_RemainingBalance"
        Private Emp_Permissions_SelectAll_ByUserId_InnerPage As String = "Emp_Permissions_SelectAll_ByUserId_InnerPage"
        Private Emp_Study_Nursing_Permissions_SelectAll_ByUserId_InnerPage As String = "Emp_Study_Nursing_Permissions_SelectAll_ByUserId_InnerPage"
        Private Emp_Permissions_Select_DelayPermissions As String = "Emp_Permissions_Select_DelayPermissions"
        Private Emp_Permissions_HasStudyOrNursing As String = "Emp_Permissions_HasStudyOrNursing"
        Private Emp_Permissions_SelectDay_Status As String = "Emp_Permissions_SelectDay_Status"

#End Region

#Region "Extended Class Variables"


        Private EmpPermissionsSelectAllInnerJoin As String = "Emp_PermissionsGetAllInnerJoin"

        Private EmpPermissionsFindExisting As String = "Emp_Permissions_Find_Existing"

#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef PermId As Integer, ByVal FK_EmployeeId As Long, ByVal FK_PermId As Integer, ByVal PermDate As DateTime, ByVal FromTime As DateTime?,
                            ByVal ToTime As DateTime?, ByVal IsFullDay As Boolean?, ByVal Remark As String, ByVal AttachedFile As String,
                            ByVal IsForPeriod As Boolean?, ByVal PermEndDate As DateTime?, ByVal IsSpecificDays As Boolean?, ByVal Days As String,
                            ByVal IsFlexible As Boolean?, ByVal IsDividable As Boolean?, ByVal CREATED_BY As String, ByVal PermissionOption As Integer,
                            ByVal flexiblePermissionDuration As Integer, ByVal BalanceDays As Double, ByVal AllowedTime As Integer, ByVal StudyYear As Integer,
                            ByVal Semester As String, ByVal FK_UniversityId As Integer?, ByVal Emp_GPAType As Integer?, ByVal Emp_GPA As Decimal?,
                            ByVal FK_MajorId As Integer?, ByVal FK_SpecializationId As Integer?) As Integer

            objDac = DAC.getDAC()
            Try

                Dim sp1 As New SqlParameter("@PermissionId", SqlDbType.Int, 8, ParameterDirection.InputOutput, False, 0, 0, "", DataRowVersion.Default, 0)

                If (PermEndDate <> DateTime.MinValue) Then
                    errNo = objDac.AddUpdateDeleteSPTrans(Emp_Permissions_Insert,
                                                      sp1, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
               New SqlParameter("@FK_PermId", IIf(FK_PermId = -1, DBNull.Value, FK_PermId)),
               New SqlParameter("@PermDate", PermDate),
               New SqlParameter("@FromTime", IIf(FromTime.HasValue, FromTime, DBNull.Value)),
               New SqlParameter("@ToTime", IIf(ToTime.HasValue, ToTime, DBNull.Value)),
               New SqlParameter("@IsFullDay", IsFullDay),
               New SqlParameter("@Remark", Remark),
               New SqlParameter("@AttachedFile", AttachedFile),
               New SqlParameter("@IsForPeriod", IsForPeriod),
               New SqlParameter("@PermEndDate", PermEndDate),
               New SqlParameter("@IsSpecificDays", IsSpecificDays),
               New SqlParameter("@Days", Days),
               New SqlParameter("@BalanceDays", BalanceDays),
               New SqlParameter("@IsFlexible", IsFlexible),
               New SqlParameter("@IsDividable", IsDividable),
               New SqlParameter("@CREATED_BY", CREATED_BY),
               New SqlParameter("@FlexiblePermissionDuration", flexiblePermissionDuration),
               New SqlParameter("@PermissionOption", PermissionOption),
               New SqlParameter("@AllowedTime", AllowedTime),
               New SqlParameter("@StudyYear", StudyYear),
               New SqlParameter("@Semester", Semester),
               New SqlParameter("@FK_UniversityId", FK_UniversityId),
               New SqlParameter("@Emp_GPAType", Emp_GPAType),
               New SqlParameter("@Emp_GPA", Emp_GPA),
               New SqlParameter("@FK_MajorId", FK_MajorId),
               New SqlParameter("@FK_SpecializationId", FK_SpecializationId))
                Else
                    errNo = objDac.AddUpdateDeleteSPTrans(Emp_Permissions_Insert,
                                                      sp1, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
               New SqlParameter("@FK_PermId", FK_PermId),
               New SqlParameter("@PermDate", PermDate),
               New SqlParameter("@FromTime", IIf(FromTime.HasValue, FromTime, DBNull.Value)),
               New SqlParameter("@ToTime", IIf(ToTime.HasValue, ToTime, DBNull.Value)),
               New SqlParameter("@IsFullDay", IsFullDay),
               New SqlParameter("@Remark", Remark),
               New SqlParameter("@AttachedFile", AttachedFile),
               New SqlParameter("@IsForPeriod", IsForPeriod),
               New SqlParameter("@IsSpecificDays", IsSpecificDays),
               New SqlParameter("@Days", Days),
               New SqlParameter("@BalanceDays", BalanceDays),
               New SqlParameter("@IsFlexible", IsFlexible),
               New SqlParameter("@IsDividable", IsDividable),
               New SqlParameter("@CREATED_BY", CREATED_BY),
               New SqlParameter("@FlexiblePermissionDuration", flexiblePermissionDuration),
               New SqlParameter("@PermissionOption", PermissionOption),
               New SqlParameter("@AllowedTime", AllowedTime),
               New SqlParameter("@StudyYear", StudyYear),
               New SqlParameter("@Semester", Semester),
               New SqlParameter("@FK_UniversityId", FK_UniversityId),
               New SqlParameter("@Emp_GPAType", Emp_GPAType),
               New SqlParameter("@Emp_GPA", Emp_GPA),
               New SqlParameter("@FK_MajorId", FK_MajorId),
               New SqlParameter("@FK_SpecializationId", FK_SpecializationId))
                End If

                PermId = sp1.Value
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal PermissionId As Long, ByVal FK_EmployeeId As Long,
                               ByVal FK_PermId As Integer, ByVal PermDate As DateTime, ByVal FromTime As DateTime?,
                               ByVal ToTime As DateTime?, ByVal IsFullDay As Boolean, ByVal Remark As String,
                               ByVal IsForPeriod As Boolean, ByVal PermEndDate As DateTime?, ByVal IsSpecificDays As Boolean,
                               ByVal Days As String, ByVal IsFlexible As Boolean, ByVal IsDividable As Boolean, ByVal LAST_UPDATE_BY As String,
                               ByVal PermissionOption As Integer, ByVal flexiblePermissionDuration As Integer,
                               ByVal AttachedFile As String, ByVal BalanceDays As Double, ByVal AllowedTime As Integer, ByVal StudyYear As Integer,
                               ByVal Semester As String, ByVal FK_UniversityId As Integer?, ByVal Emp_GPAType As Integer?, ByVal Emp_GPA As Decimal?,
                               ByVal FK_MajorId As Integer?, ByVal FK_SpecializationId As Integer?) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Permissions_Update,
               New SqlParameter("@PermissionId", PermissionId),
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
               New SqlParameter("@FK_PermId", IIf(FK_PermId = -1, DBNull.Value, FK_PermId)),
               New SqlParameter("@PermDate", PermDate),
               New SqlParameter("@FromTime", IIf(FromTime.HasValue, FromTime, DBNull.Value)),
               New SqlParameter("@ToTime", IIf(ToTime.HasValue, ToTime, DBNull.Value)),
               New SqlParameter("@IsFullDay", IsFullDay),
               New SqlParameter("@Remark", Remark),
               New SqlParameter("@IsForPeriod", IsForPeriod),
               New SqlParameter("@PermEndDate", PermEndDate),
               New SqlParameter("@IsSpecificDays", IsSpecificDays),
               New SqlParameter("@Days", Days),
               New SqlParameter("@BalanceDays", BalanceDays),
               New SqlParameter("@IsFlexible", IsFlexible),
               New SqlParameter("@IsDividable", IsDividable),
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY),
               New SqlParameter("@FlexiblePermissionDuration", flexiblePermissionDuration),
               New SqlParameter("@PermissionOption", PermissionOption),
               New SqlParameter("@AttachedFile", IIf(AttachedFile Is Nothing, DBNull.Value, AttachedFile)),
               New SqlParameter("@AllowedTime", AllowedTime),
               New SqlParameter("@StudyYear", StudyYear),
               New SqlParameter("@Semester", Semester),
               New SqlParameter("@FK_UniversityId", FK_UniversityId),
               New SqlParameter("@Emp_GPAType", Emp_GPAType),
               New SqlParameter("@Emp_GPA", Emp_GPA),
               New SqlParameter("@FK_MajorId", FK_MajorId),
               New SqlParameter("@FK_SpecializationId", FK_SpecializationId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetOccuranceForWeek(ByVal FromDate As DateTime, ByVal EmployeeId As Integer, ByVal PermTypeId As Integer) As Integer

            objDac = DAC.getDAC()
            Dim dtLeavePerWeek As New DataTable
            Try
                dtLeavePerWeek = objDac.GetDataTable(PermissionOccurancePerWeek_select, New SqlParameter("@PermDate", FromDate),
                                                     New SqlParameter("@EmployeeId", EmployeeId),
                                                     New SqlParameter("@PermTypeId", PermTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
                Return errNo
            End Try
            If (dtLeavePerWeek IsNot Nothing AndAlso dtLeavePerWeek.Rows.Count > 0) Then
                Return dtLeavePerWeek.Rows(0)(0)
            Else
                Return 0
            End If


        End Function

        Public Function GetOccuranceForMonth(ByVal FromDate As DateTime, ByVal EmployeeId As Integer, ByVal PermTypeId As Integer) As Integer

            objDac = DAC.getDAC()
            Dim dtLeavePerWeek As New DataTable
            Try
                dtLeavePerWeek = objDac.GetDataTable(PermissionOccurancePerMonth_select, New SqlParameter("@PermDate", FromDate),
                                                     New SqlParameter("@EmployeeId", EmployeeId),
                                                     New SqlParameter("@PermTypeId", PermTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
                Return errNo
            End Try
            If (dtLeavePerWeek IsNot Nothing AndAlso dtLeavePerWeek.Rows.Count > 0) Then
                Return dtLeavePerWeek.Rows(0)(0)
            Else
                Return 0
            End If


        End Function

        Public Function GetOccuranceForYear(ByVal FromDate As DateTime, ByVal EmployeeId As Integer, ByVal PermTypeId As Integer) As Integer

            objDac = DAC.getDAC()
            Dim dtLeavePerWeek As New DataTable
            Try
                dtLeavePerWeek = objDac.GetDataTable(PermissionOccurancePerYear_select, New SqlParameter("@PermDate", FromDate),
                                                     New SqlParameter("@EmployeeId", EmployeeId),
                                                     New SqlParameter("@PermTypeId", PermTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
                Return errNo
            End Try
            If (dtLeavePerWeek IsNot Nothing AndAlso dtLeavePerWeek.Rows.Count > 0) Then
                Return dtLeavePerWeek.Rows(0)(0)
            Else
                Return 0
            End If


        End Function

        Function GetOccuranceForDay(ByVal FromDate As Date, ByVal EmployeeId As Long, ByVal PermTypeId As Integer) As Object

            objDac = DAC.getDAC()
            Dim dtLeavePerWeek As New DataTable
            Try
                dtLeavePerWeek = objDac.GetDataTable(PermissionOccurancePerDay_select, New SqlParameter("@PermDate", FromDate),
                                                     New SqlParameter("@EmployeeId", EmployeeId),
                                                     New SqlParameter("@PermTypeId", PermTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
                Return errNo
            End Try
            If (dtLeavePerWeek IsNot Nothing AndAlso dtLeavePerWeek.Rows.Count > 0) Then
                Return dtLeavePerWeek.Rows(0)(0)
            Else
                Return 0
            End If

        End Function

        Public Function GetDurationForWeek(ByVal FromDate As DateTime, ByVal EmployeeId As Integer, ByVal PermTypeId As Integer) As Integer

            objDac = DAC.getDAC()
            Dim dtLeavePerWeek As New DataTable
            Try
                dtLeavePerWeek = objDac.GetDataTable(PermissionDurationPerWeek_select, New SqlParameter("@PermDate", FromDate),
                                                     New SqlParameter("@EmployeeId", EmployeeId),
                                                     New SqlParameter("@PermTypeId", PermTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
                Return errNo
            End Try
            If (dtLeavePerWeek IsNot Nothing AndAlso dtLeavePerWeek.Rows.Count > 0) Then
                If (dtLeavePerWeek.Rows(0)(0) IsNot DBNull.Value) Then
                    Return dtLeavePerWeek.Rows(0)(0)
                End If
                Return 0
            Else
                Return 0
            End If


        End Function

        Public Function GetDurationForMonth(ByVal FromDate As DateTime, ByVal EmployeeId As Integer, ByVal PermTypeId As Integer) As Integer

            objDac = DAC.getDAC()
            Dim dtLeavePerWeek As New DataTable
            Try
                dtLeavePerWeek = objDac.GetDataTable(PermissionDurationPerMonth_select, New SqlParameter("@PermDate", FromDate),
                                                     New SqlParameter("@EmployeeId", EmployeeId),
                                                     New SqlParameter("@PermTypeId", PermTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
                Return errNo
            End Try
            If (dtLeavePerWeek IsNot Nothing AndAlso dtLeavePerWeek.Rows.Count > 0) Then
                If (dtLeavePerWeek.Rows(0)(0) IsNot DBNull.Value) Then
                    Return dtLeavePerWeek.Rows(0)(0)
                End If
                Return 0
            Else
                Return 0
            End If

        End Function

        Function GetDurationForDay(ByVal FromDate As Date, ByVal EmployeeId As Long, ByVal PermTypeId As Integer) As Object

            objDac = DAC.getDAC()
            Dim dtLeavePerWeek As New DataTable
            Try
                dtLeavePerWeek = objDac.GetDataTable(PermissionDurationPerDay_select, New SqlParameter("@PermDate", FromDate),
                                                     New SqlParameter("@EmployeeId", EmployeeId),
                                                     New SqlParameter("@PermTypeId", PermTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
                Return errNo
            End Try
            If (dtLeavePerWeek IsNot Nothing AndAlso dtLeavePerWeek.Rows.Count > 0) Then
                If (dtLeavePerWeek.Rows(0)(0) IsNot DBNull.Value) Then
                    Return dtLeavePerWeek.Rows(0)(0)
                End If
                Return 0
            Else
                Return 0
            End If

        End Function

        Public Function GetDurationForYear(ByVal FromDate As DateTime, ByVal EmployeeId As Integer) As Integer

            objDac = DAC.getDAC()
            Dim dtLeavePerWeek As New DataTable
            Try
                dtLeavePerWeek = objDac.GetDataTable(PermissionDurationPerYear_select, New SqlParameter("@PermDate", FromDate), New SqlParameter("@EmployeeId", EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
                Return errNo
            End Try
            If (dtLeavePerWeek IsNot Nothing AndAlso dtLeavePerWeek.Rows.Count > 0) Then
                If (dtLeavePerWeek.Rows(0)(0) IsNot DBNull.Value) Then
                    Return dtLeavePerWeek.Rows(0)(0)
                End If
                Return 0
            Else
                Return 0
            End If


        End Function

        Public Function UpdateAttachment(ByVal PermissionId As Long, ByVal AttachedFile As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Permissions_Update_Attachment,
               New SqlParameter("@PermissionId", PermissionId),
                   New SqlParameter("@AttachedFile", AttachedFile))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal PermissionId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Permissions_Delete, New SqlParameter("@PermissionId", PermissionId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal PermissionId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(Emp_Permissions_Select, New SqlParameter("@PermissionId", PermissionId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetAll() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_Permissions_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Get_ByUserId_InnerPage(ByVal UserId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_Permissions_SelectAll_ByUserId_InnerPage, New SqlParameter("@UserId", UserId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Get_StudyNursingByUserId_InnerPage(ByVal UserId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_Study_Nursing_Permissions_SelectAll_ByUserId_InnerPage, New SqlParameter("@UserId", UserId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Check_Has_StudyOrNursing_Permission(ByVal FK_EmployeeId As Integer, ByVal PermDate As DateTime, ByVal PermEndDate As DateTime, ByVal PermissionTypeId As Integer, ByVal PermissionOption As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_Permissions_EligibleToRequest_Additional_IFStudyorNursing, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
                                              New SqlParameter("@PermDate", PermDate),
                                              New SqlParameter("@PermEndDate", PermEndDate),
                                              New SqlParameter("@PermissionTypeId", PermissionTypeId),
                                              New SqlParameter("@PermissionOption", PermissionOption))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAllBySearchCriteria(ByVal fromdate As DateTime, ByVal todate As DateTime, ByVal companyid As Integer?, ByVal entityid As Integer?, ByVal employeeid As Integer?) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_Permissions_Search, New SqlParameter("@fromdate", fromdate),
               New SqlParameter("@todate", todate),
               New SqlParameter("@companyid", companyid),
               New SqlParameter("@employeeid", employeeid),
               New SqlParameter("@entityid", entityid))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAllPermissionsByEmployee(ByVal FK_EmployeeId As Long, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal FK_PermId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_Permissions_SelectAllByEmployee, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
                                              New SqlParameter("@FromDate", IIf(FromDate = DateTime.MinValue, DBNull.Value, FromDate)),
                                              New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, DBNull.Value, ToDate)),
                                              New SqlParameter("@FK_PermId", IIf(FK_PermId = 0, DBNull.Value, FK_PermId)))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetMultiPermissionsByEmployeeIDs(ByVal EmpIDs As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Get_Emp_Permissions_ByMultiEmployees, New SqlParameter("@EmpIDs", EmpIDs))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetAllWithEmployeeInner() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Get_Emp_Permissions_WithEmployeeInner, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetAll_ByPermMonthAndType(ByVal FK_EmployeeId As Integer, ByVal FK_PermId As Integer, ByVal PermMonth As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(GetEmployeePermission_ByPermMonthandType, New SqlParameter("@FK_EmployeeID", FK_EmployeeId),
                                              New SqlParameter("@FK_PermId", FK_PermId),
                                              New SqlParameter("@PermMonth", PermMonth))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetAllDurationByEmployee(ByVal FK_EmployeeId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal FK_PermId As Integer, ByVal PermissionId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_PermissionsDuration_SelectAllByEmployee, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
                                              New SqlParameter("@FromDate", IIf(FromDate = DateTime.MinValue, DBNull.Value, FromDate)),
                                              New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, DBNull.Value, ToDate)),
                                              New SqlParameter("@FK_PermId", IIf(FK_PermId = 0, DBNull.Value, FK_PermId)),
                                              New SqlParameter("@PermissionId", PermissionId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmp_TotHRS(ByVal FK_EmployeeId As Integer, ByVal M_Date As DateTime) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(CALC_EFFECTIVE_SCHEDULE_WithFlix2, New SqlParameter("@EMPID", FK_EmployeeId),
                                             New SqlParameter("@MoveDATE", M_Date)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function Get_Permission_InsideWork(ByVal FK_EmployeeId As Integer, ByVal MoveDate As DateTime, ByVal PermStartTime As DateTime, ByVal PermEndTime As DateTime, ByRef IsValidInSchedule As Boolean, ByVal IsOff As Boolean, ByVal IsHoliday As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                Dim IsValidInSchedule_OUT = New SqlParameter("@isValidInSchedule", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, IsValidInSchedule)
                Dim IsOff_OUT = New SqlParameter("@isOff", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, IsOff)
                Dim IsHoliday_OUT = New SqlParameter("@isHoliday", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, IsHoliday)

                objColl = objDac.GetDataTable(Emp_permssion_ValidateInsideSchedule, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
                            New SqlParameter("@MoveDate", MoveDate),
                            New SqlParameter("@PermStartTime", IIf(PermStartTime = DateTime.MinValue, DBNull.Value, PermStartTime)),
                            New SqlParameter("@PermEndTime", IIf(PermEndTime = DateTime.MinValue, DBNull.Value, PermEndTime)),
                            IsValidInSchedule_OUT, IsOff_OUT, IsHoliday_OUT)
                If Not IsDBNull(IsValidInSchedule_OUT.Value) Then
                    IsValidInSchedule = IsValidInSchedule_OUT.Value
                End If
                If Not IsDBNull(IsOff_OUT.Value) Then
                    IsOff = IsOff_OUT.Value
                End If
                If Not IsDBNull(IsHoliday_OUT.Value) Then
                    IsHoliday = IsHoliday_OUT.Value
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function CheckHasPermissionDuringTime(ByVal FK_EmployeeId As Integer, ByVal permDate As DateTime, ByVal permFromTime As DateTime, ByVal permToTime As DateTime) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Emp_Permissions_IsExist, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), New SqlParameter("@PermDate", permDate),
                                              New SqlParameter("@FromTime", permFromTime), New SqlParameter("@ToTime", permToTime)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function Get_RemainingPermissionBalance(ByVal EmployeeId As Integer, ByVal Fk_PermID As Integer, ByVal FROM_DATE As DateTime, ByVal TO_DATE As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(GetRemainingPermissionBalance, New SqlParameter("@EmployeeId", EmployeeId),
               New SqlParameter("@Fk_PermID", Fk_PermID),
               New SqlParameter("@FROM_DATE", FROM_DATE),
               New SqlParameter("@TO_DATE", TO_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function IsExistNursingStudy(ByVal EmployeeId As Integer, ByVal PermissionOption As Integer, ByVal PermDate As DateTime, ByVal PermEndDate As DateTime, ByVal Days As String) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_Permissions_IsExistNursingStudy, New SqlParameter("@FK_EmployeeId", EmployeeId),
                                              New SqlParameter("@PermDate", PermDate),
                                              New SqlParameter("@PermEndDate", PermEndDate),
                                              New SqlParameter("@PermissionOption", PermissionOption),
                                              New SqlParameter("@Days", Days))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function CheckIsRequest(ByVal PermissionId As Integer, ByVal EmployeeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(IsRequest, New SqlParameter("@PermissionId", PermissionId),
                                              New SqlParameter("@FK_EmployeeId", EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Get_Previous_Restday_Date(ByVal FK_EmployeeId As Integer, ByVal PermissionDate As DateTime?) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_Permissions_Select_Restday_Date, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
                                              New SqlParameter("@Date", PermissionDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Get_Next_Restday_Date(ByVal FK_EmployeeId As Integer, ByVal PreviousRestDate As DateTime?) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_Permissions_Select_NextRestday_Date, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
                                              New SqlParameter("@Date", PreviousRestDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Get_EmployeeTransaction(ByVal FK_EmployeeId As Integer, ByVal PermDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_Permissions_Check_EmployeeTransaction, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
                                              New SqlParameter("@PermDate", PermDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Get_RestPermission_RemainingBalance(ByVal FK_EmployeeId As Integer, ByVal PermDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_Permissions_RestPermission_RemainingBalance, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
                                              New SqlParameter("@Date", PermDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Get_DelayPermissions(ByVal FK_EmployeeId As Integer, ByVal PermDate As DateTime, ByVal PermTypeId As Integer, ByVal FromTime As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_Permissions_Select_DelayPermissions, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
                                              New SqlParameter("@PermDate", PermDate),
                                              New SqlParameter("@FK_PermId", PermTypeId),
                                              New SqlParameter("@FromTime", FromTime))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Check_HasStudyOrNursing(ByVal EmployeeId As Integer, ByVal PermDate As DateTime, ByVal PermEndDate As DateTime, ByVal Days As String) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_Permissions_HasStudyOrNursing, New SqlParameter("@FK_EmployeeId", EmployeeId),
                                              New SqlParameter("@PermDate", PermDate),
                                              New SqlParameter("@PermEndDate", IIf(PermEndDate = DateTime.MinValue, DBNull.Value, PermEndDate)),
                                              New SqlParameter("@Days", Days))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_DayStatus(ByVal FK_EmployeeId As Integer, ByVal PermDate As DateTime, ByVal PermEndDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_Permissions_SelectDay_Status, New SqlParameter("@EmployeeId", FK_EmployeeId),
                                              New SqlParameter("@FROM_DATE", PermDate),
                                              New SqlParameter("@TO_DATE", PermEndDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region

#Region "Extended Methods"

        Public Function GetAllInnerJoin(ByVal EmployeeId As Integer, ByVal PermissionOption As Integer, ByVal PermFromDate As DateTime, ByVal PermEndDate As DateTime) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(EmpPermissionsSelectAllInnerJoin, New SqlParameter("@EmployeeId", EmployeeId),
                                              New SqlParameter("@PermissionOption", PermissionOption),
                                              New SqlParameter("@FromDate", PermFromDate),
                                              New SqlParameter("@ToDate", PermEndDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function FindExisting(ByVal PermissionId As Integer) As Boolean

            objDac = DAC.getDAC()
            Try
                Dim rslt As Integer =
                    objDac.GetSingleValue(Of Integer)(EmpPermissionsFindExisting,
                                                      New SqlParameter("@PermissionId", PermissionId))
                If rslt = 1 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

#End Region

    End Class
End Namespace