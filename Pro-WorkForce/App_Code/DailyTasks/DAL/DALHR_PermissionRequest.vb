Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.DailyTasks

    Public Class DALHR_PermissionRequest
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private HR_PermissionRequest_Select As String = "HR_PermissionRequest_select"
        Private HR_PermissionRequest_Select_All As String = "HR_PermissionRequest_select_All"
        Private HR_PermissionRequest_Insert As String = "HR_PermissionRequest_Insert"
        Private HR_PermissionRequest_Update As String = "HR_PermissionRequest_Update"
        Private HR_PermissionRequest_Delete As String = "HR_PermissionRequest_Delete"
        Private CALC_EFFECTIVE_SCHEDULE_WithFlix2 As String = "CALC_EFFECTIVE_SCHEDULE_WithFlix2"
        Private HR_Permissions_Search As String = "HR_Permissions_Search"
        Private HR_Permissions_Update_Attachment As String = "HR_Permissions_Update_Attachment"
        Private HR_Permissions_SelectAllByEmployee As String = "HR_Permissions_SelectAllByEmployee"
        Private HR_PermissionOccurancePerWeek_select As String = "HR_PermissionOccurancePerWeek_select"
        Private HR_PermissionOccurancePerMonth_select As String = "HR_PermissionOccurancePerMonth_select"
        Private HR_PermissionOccurancePerYear_select As String = "HR_PermissionOccurancePerYear_select"
        Private HR_PermissionOccurancePerDay_select As String = "HR_PermissionOccurancePerDay_select"
        Private HR_PermissionDurationPerWeek_select As String = "HR_PermissionDurationPerWeek_select"
        Private HR_PermissionDurationPerMonth_select As String = "HR_PermissionDurationPerMonth_select"
        Private HR_PermissionDurationPerYear_select As String = "HR_PermissionDurationPerYear_select"
        Private HR_PermissionDurationPerDay_select As String = "HR_PermissionDurationPerDay_select"
        Private HR_Get_HR_PermissionRequest_ByMultiEmployees As String = "HR_Get_HR_PermissionRequest_ByMultiEmployees"
        Private HR_Get_HR_PermissionRequest_WithEmployeeInner As String = "HR_Get_HR_PermissionRequest_WithEmployeeInner"
        Private HR_GetEmployeePermission_ByPermMonthandType As String = "HR_GetEmployeePermission_ByPermMonthandType"
        Private HR_PermissionsDuration_SelectAllByEmployee As String = "HR_PermissionsDuration_SelectAllByEmployee"
        Private HR_PermissionsGetAll_ByStatus As String = "HR_PermissionsGetAll_ByStatus"
        Private Update_HRPermission_RequestStatus As String = "Update_HRPermission_RequestStatus"
        Private HR_PermissionsGetAll_Rejected As String = "HR_PermissionsGetAll_Rejected"
        Private Emp_Permissions_RestPermission_RemainingBalance_HR As String = "Emp_Permissions_RestPermission_RemainingBalance_HR"

#End Region

#Region "Extended Class Variables"


        Private EmpPermissionsSelectAllInnerJoin As String = "HR_PermissionsGetAllInnerJoin"

        Private EmpPermissionsFindExisting As String = "HR_Permissions_Find_Existing"

#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef PermissionRequestId As Integer, ByVal FK_EmployeeId As Long, ByVal FK_PermId As Integer, ByVal PermDate As DateTime, ByVal FromTime As DateTime?, ByVal ToTime As DateTime?, ByVal IsFullDay As Boolean?, ByVal Remark As String, ByVal AttachedFile As String, ByVal IsForPeriod As Boolean?, ByVal PermEndDate As DateTime?, ByVal IsSpecificDays As Boolean?, ByVal Days As String, ByVal IsFlexible As Boolean?, ByVal IsDividable As Boolean?, ByVal CREATED_BY As String, ByVal PermissionOption As Integer, ByVal flexiblePermissionDuration As Integer, ByVal BalanceDays As Double, ByVal AllowedTime As Integer) As Integer

            objDac = DAC.getDAC()
            Try

                Dim sp1 As New SqlParameter("@PermissionRequestId", SqlDbType.Int, 8, ParameterDirection.InputOutput, False, 0, 0, "", DataRowVersion.Default, 0)

                If (PermEndDate <> DateTime.MinValue) Then
                    errNo = objDac.AddUpdateDeleteSPTrans(HR_PermissionRequest_Insert, _
                                                      sp1, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_PermId", IIf(FK_PermId = -1, DBNull.Value, FK_PermId)), _
               New SqlParameter("@PermDate", PermDate), _
               New SqlParameter("@FromTime", IIf(FromTime.HasValue, FromTime, DBNull.Value)), _
               New SqlParameter("@ToTime", IIf(ToTime.HasValue, ToTime, DBNull.Value)), _
               New SqlParameter("@IsFullDay", IsFullDay), _
               New SqlParameter("@Remark", Remark), _
               New SqlParameter("@AttachedFile", AttachedFile), _
               New SqlParameter("@IsForPeriod", IsForPeriod), _
               New SqlParameter("@PermEndDate", PermEndDate), _
               New SqlParameter("@IsSpecificDays", IsSpecificDays), _
               New SqlParameter("@Days", Days), _
               New SqlParameter("@BalanceDays", BalanceDays), _
               New SqlParameter("@IsFlexible", IsFlexible), _
               New SqlParameter("@IsDividable", IsDividable), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@FlexiblePermissionDuration", flexiblePermissionDuration), _
               New SqlParameter("@PermissionOption", PermissionOption), _
               New SqlParameter("@AllowedTime", AllowedTime))
                Else
                    errNo = objDac.AddUpdateDeleteSPTrans(HR_PermissionRequest_Insert, _
                                                      sp1, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_PermId", FK_PermId), _
               New SqlParameter("@PermDate", PermDate), _
               New SqlParameter("@FromTime", IIf(FromTime.HasValue, FromTime, DBNull.Value)), _
               New SqlParameter("@ToTime", IIf(ToTime.HasValue, ToTime, DBNull.Value)), _
               New SqlParameter("@IsFullDay", IsFullDay), _
               New SqlParameter("@Remark", Remark), _
               New SqlParameter("@AttachedFile", AttachedFile), _
               New SqlParameter("@IsForPeriod", IsForPeriod), _
               New SqlParameter("@IsSpecificDays", IsSpecificDays), _
               New SqlParameter("@Days", Days), _
               New SqlParameter("@BalanceDays", BalanceDays), _
               New SqlParameter("@IsFlexible", IsFlexible), _
               New SqlParameter("@IsDividable", IsDividable), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@FlexiblePermissionDuration", flexiblePermissionDuration), _
               New SqlParameter("@PermissionOption", PermissionOption), _
               New SqlParameter("@AllowedTime", AllowedTime))
                End If

                PermissionRequestId = sp1.Value
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo


        End Function

        Public Function Update(ByVal PermissionRequestId As Long, ByVal FK_EmployeeId As Long, _
                               ByVal FK_PermId As Integer, ByVal PermDate As DateTime, ByVal FromTime As DateTime?, _
                               ByVal ToTime As DateTime?, ByVal IsFullDay As Boolean, ByVal Remark As String, _
                               ByVal IsForPeriod As Boolean, ByVal PermEndDate As DateTime?, ByVal IsSpecificDays As Boolean, _
                               ByVal Days As String, ByVal IsFlexible As Boolean, ByVal IsDividable As Boolean, ByVal LAST_UPDATE_BY As String _
                               , ByVal PermissionOption As Integer, ByVal flexiblePermissionDuration As Integer, _
                               ByVal AttachedFile As String, ByVal BalanceDays As Double, ByVal AllowedTime As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(HR_PermissionRequest_Update,
               New SqlParameter("@PermissionRequestId", PermissionRequestId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_PermId", IIf(FK_PermId = -1, DBNull.Value, FK_PermId)), _
               New SqlParameter("@PermDate", PermDate), _
               New SqlParameter("@FromTime", IIf(FromTime.HasValue, FromTime, DBNull.Value)), _
               New SqlParameter("@ToTime", IIf(ToTime.HasValue, ToTime, DBNull.Value)), _
               New SqlParameter("@IsFullDay", IsFullDay), _
               New SqlParameter("@Remark", Remark), _
               New SqlParameter("@IsForPeriod", IsForPeriod), _
               New SqlParameter("@PermEndDate", IIf(PermEndDate.HasValue, PermEndDate, DBNull.Value)), _
               New SqlParameter("@IsSpecificDays", IsSpecificDays), _
               New SqlParameter("@Days", Days), _
               New SqlParameter("@BalanceDays", BalanceDays), _
               New SqlParameter("@IsFlexible", IsFlexible), _
               New SqlParameter("@IsDividable", IsDividable), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
               New SqlParameter("@FlexiblePermissionDuration", flexiblePermissionDuration), _
               New SqlParameter("@PermissionOption", PermissionOption), _
               New SqlParameter("@AttachedFile", IIf(AttachedFile Is Nothing, DBNull.Value, AttachedFile)), _
               New SqlParameter("@AllowedTime", AllowedTime))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal PermissionRequestId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(HR_PermissionRequest_Delete, New SqlParameter("@PermissionRequestId", PermissionRequestId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal PermissionRequestId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(HR_PermissionRequest_Select, New SqlParameter("@PermissionRequestId", PermissionRequestId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetAll() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(HR_PermissionRequest_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetOccuranceForWeek(ByVal FromDate As DateTime, ByVal EmployeeId As Integer, ByVal PermTypeId As Integer) As Integer

            objDac = DAC.getDAC()
            Dim dtLeavePerWeek As New DataTable
            Try
                dtLeavePerWeek = objDac.GetDataTable(HR_PermissionOccurancePerWeek_select, New SqlParameter("@PermDate", FromDate), _
                                                     New SqlParameter("@EmployeeId", EmployeeId), _
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
                dtLeavePerWeek = objDac.GetDataTable(HR_PermissionOccurancePerMonth_select, New SqlParameter("@PermDate", FromDate), _
                                                     New SqlParameter("@EmployeeId", EmployeeId), _
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
                dtLeavePerWeek = objDac.GetDataTable(HR_PermissionOccurancePerYear_select, New SqlParameter("@PermDate", FromDate), _
                                                     New SqlParameter("@EmployeeId", EmployeeId), _
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
                dtLeavePerWeek = objDac.GetDataTable(HR_PermissionOccurancePerDay_select, New SqlParameter("@PermDate", FromDate), _
                                                     New SqlParameter("@EmployeeId", EmployeeId), _
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
                dtLeavePerWeek = objDac.GetDataTable(HR_PermissionDurationPerWeek_select, New SqlParameter("@PermDate", FromDate), _
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
                dtLeavePerWeek = objDac.GetDataTable(HR_PermissionDurationPerMonth_select, New SqlParameter("@PermDate", FromDate), _
                                                     New SqlParameter("@EmployeeId", EmployeeId), _
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
                dtLeavePerWeek = objDac.GetDataTable(HR_PermissionDurationPerDay_select, New SqlParameter("@PermDate", FromDate), _
                                                     New SqlParameter("@EmployeeId", EmployeeId), _
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

        Public Function GetDurationForYear(ByVal FromDate As DateTime, ByVal EmployeeId As Integer, ByVal PermTypeId As Integer) As Integer

            objDac = DAC.getDAC()
            Dim dtLeavePerWeek As New DataTable
            Try
                dtLeavePerWeek = objDac.GetDataTable(HR_PermissionDurationPerYear_select, New SqlParameter("@PermDate", FromDate), _
                                                     New SqlParameter("@EmployeeId", EmployeeId), _
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

        Public Function UpdateAttachment(ByVal PermissionId As Long, ByVal AttachedFile As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(HR_Permissions_Update_Attachment, _
               New SqlParameter("@PermissionId", PermissionId), _
                   New SqlParameter("@AttachedFile", AttachedFile))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetAllBySearchCriteria(ByVal fromdate As DateTime, ByVal todate As DateTime, ByVal companyid As Integer?, ByVal entityid As Integer?, ByVal employeeid As Integer?) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(HR_Permissions_Search, New SqlParameter("@fromdate", fromdate), _
               New SqlParameter("@todate", todate), _
               New SqlParameter("@companyid", companyid), _
               New SqlParameter("@employeeid", employeeid), _
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
                objColl = objDac.GetDataTable(HR_Permissions_SelectAllByEmployee, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                              New SqlParameter("@FromDate", IIf(FromDate = DateTime.MinValue, DBNull.Value, FromDate)), _
                                              New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, DBNull.Value, ToDate)), _
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
                objColl = objDac.GetDataTable(HR_Get_HR_PermissionRequest_ByMultiEmployees, New SqlParameter("@EmpIDs", EmpIDs))
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
                objColl = objDac.GetDataTable(HR_Get_HR_PermissionRequest_WithEmployeeInner, Nothing)
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
                objColl = objDac.GetDataTable(HR_GetEmployeePermission_ByPermMonthandType, New SqlParameter("@FK_EmployeeID", FK_EmployeeId), _
                                              New SqlParameter("@FK_PermId", FK_PermId), _
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
                objColl = objDac.GetDataTable(HR_PermissionsDuration_SelectAllByEmployee, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                              New SqlParameter("@FromDate", IIf(FromDate = DateTime.MinValue, DBNull.Value, FromDate)), _
                                              New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, DBNull.Value, ToDate)), _
                                              New SqlParameter("@FK_PermId", IIf(FK_PermId = 0, DBNull.Value, FK_PermId)), _
                                              New SqlParameter("@PermissionId", PermissionId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Update_HR_Permission_RequestStatus(ByVal PermissionRequestId As Long, ByVal IsRejected As Boolean, ByVal RejectionReason As String, ByVal LAST_UPDATED_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Update_HRPermission_RequestStatus,
               New SqlParameter("@PermissionRequestId", PermissionRequestId), _
               New SqlParameter("@IsRejected", IsRejected), _
               New SqlParameter("@RejectionReason", RejectionReason), _
               New SqlParameter("@LAST_UPDATED_BY", LAST_UPDATED_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetEmp_TotHRS(ByVal FK_EmployeeId As Integer, ByVal M_Date As DateTime) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(CALC_EFFECTIVE_SCHEDULE_WithFlix2, New SqlParameter("@EMPID", FK_EmployeeId), _
                                             New SqlParameter("@MoveDATE", M_Date)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function Get_RestPermission_RemainingBalanceHR(ByVal FK_EmployeeId As Integer, ByVal PermDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_Permissions_RestPermission_RemainingBalance_HR, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                              New SqlParameter("@Date", PermDate))
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
                objColl = objDac.GetDataTable(EmpPermissionsSelectAllInnerJoin, New SqlParameter("@EmployeeId", EmployeeId), _
                                              New SqlParameter("@PermissionOption", PermissionOption), _
                                              New SqlParameter("@FromDate", PermFromDate), _
                                              New SqlParameter("@ToDate", PermEndDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function FindExisting(ByVal PermissionRequestId As Integer) As Boolean

            objDac = DAC.getDAC()
            Try
                Dim rslt As Integer = _
                    objDac.GetSingleValue(Of Integer)(EmpPermissionsFindExisting, _
                                                      New SqlParameter("@PermissionRequestId", PermissionRequestId))
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

        Public Function HR_Permissions_GetAllByStatus(ByVal UserId As String) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(HR_PermissionsGetAll_ByStatus, New SqlParameter("@UserId", UserId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function HR_Permissions_GetAllRejected(ByVal UserId As String) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(HR_PermissionsGetAll_Rejected, New SqlParameter("@UserId", UserId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

#End Region

    End Class
End Namespace