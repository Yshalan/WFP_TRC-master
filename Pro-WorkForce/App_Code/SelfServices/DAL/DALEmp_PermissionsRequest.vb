Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.SelfServices

    Public Class DALEmp_PermissionsRequest
        Inherits MGRBase

#Region "Class Variables"
        Private strConn As String
        Private Emp_PermissionsRequest_Select As String = "Emp_PermissionsRequest_select"
        Private Emp_PermissionsRequest_Select_All As String = "Emp_PermissionsRequest_select_All"
        Private Emp_PermissionsRequest_Select_ByEmp As String = "Emp_PermissionsRequest_Select_ByEmp"
        Private Emp_PermissionsRequest_Select_ByStatus As String = "Emp_PermissionsRequest_Select_ByStatus"
        Private Emp_PermissionsRequest_Insert As String = "Emp_PermissionsRequest_Insert"
        Private Emp_PermissionsRequest_Update As String = "Emp_PermissionsRequest_Update"
        Private Emp_PermissionsRequest_Delete As String = "Emp_PermissionsRequest_Delete"
        Private Emp_PermissionsRequest_Select_ByDM As String = "Emp_PermissionsRequest_Select_ByDM"
        Private Emp_PermissionRequest_UpdateStatus As String = "Emp_PermissionRequest_UpdateStatus"
        Private Emp_PermissionsRequest_Select_ByHR As String = "Emp_PermissionsRequest_Select_ByHR"
        Private Emp_PermissionsRequest_Select_All_ByDM As String = "Emp_PermissionsRequest_Select_All_ByDM"
        Private Emp_PermissionsRequest_Select_All_ByHR As String = "Emp_PermissionsRequest_Select_All_ByHR"
        Private GetEmmployeePermissionRequestByPermMonthandType As String = "GetEmmployeePermissionRequestByPermMonthandType"
        Private Emp_PermissionRequest_SelectForEmpViolation As String = "Emp_PermissionRequest_SelectForEmpViolation"
        Private Emp_PermissionsRequest_Select_ByGM As String = "Emp_PermissionsRequest_Select_ByGM"
        Private Emp_PermissionRequest_SelectForEmpViolation_FromTime As String = "Emp_PermissionRequest_SelectForEmpViolation_FromTime"
        Private Emp_PermissionRequest_SelectForEmpViolation_ToTime As String = "Emp_PermissionRequest_SelectForEmpViolation_ToTime"
        Private CALC_EFFECTIVE_SCHEDULE_WithFlix2 As String = "CALC_EFFECTIVE_SCHEDULE_WithFlix2"
        Private PermissionRequestOccurancePerWeek_select As String = "PermissionRequestOccurancePerWeek_select"
        Private PermissionRequestOccurancePerMonth_select As String = "PermissionRequestOccurancePerMonth_select"
        Private PermissionRequestOccurancePerYear_select As String = "PermissionRequestOccurancePerYear_select"
        Private PermissionRequestOccurancePerDay_select As String = "PermissionRequestOccurancePerDay_select"
        Private PermissionRequestDurationPerWeek_select As String = "PermissionRequestDurationPerWeek_select"
        Private PermissionRequestDurationPerMonth_select As String = "PermissionRequestDurationPerMonth_select"
        Private PermissionRequestDurationPerYear_select As String = "PermissionRequestDurationPerYear_select"
        Private PermissionRequestDurationPerDay_select As String = "PermissionRequestDurationPerDay_select"
        Private Emp_PermissionsRequestDuration_SelectAllByEmployee As String = "Emp_PermissionsRequestDuration_SelectAllByEmployee"
        Private Emp_PermissionsRequest_IsExist As String = "Emp_PermissionsRequest_IsExist"
        Private Emp_PermissionsRequest_PermStatus As String = "Emp_PermissionsRequest_PermStatus"
        Private HasNursingOrStudyPermission As String = "HasNursingOrStudyPermission"
        Private Emp_PermissionsRequest_CheckPermissionTypeOccurance As String = "Emp_PermissionsRequest_CheckPermissionTypeOccurance"
        Private Emp_PermissionsRequest_AllowedToRequest As String = "Emp_PermissionsRequest_AllowedToRequest"
        Private Emp_Permissions_RestPermission_RemainingBalance_Request As String = "Emp_Permissions_RestPermission_RemainingBalance_Request"
        Private Emp_PermissionsRequest_Select_ByDM_Mobile As String = "Emp_PermissionsRequest_Select_ByDM_Mobile"
        Private Emp_PermissionsRequest_Select_Approved_ByDM_Date_Mobile As String = "Emp_PermissionsRequest_Select_Approved_ByDM_Date_Mobile"

#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef PermissionId As Integer, ByVal FK_EmployeeId As Long, ByVal FK_PermId As Integer, ByVal PermDate As DateTime, ByVal FromTime As DateTime, ByVal ToTime As DateTime, ByVal IsFullDay As Boolean, ByVal Remark As String, ByVal AttachedFile As String, ByVal IsForPeriod As Boolean, ByVal PermEndDate As DateTime, ByVal IsSpecificDays As Boolean, ByVal Days As String, ByVal IsFlexible As Boolean, ByVal IsDividable As Boolean, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal FK_StatusId As Integer, ByVal FlexibilePermissionDuration As Integer, ByVal RequestedByCoordinator As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlParamLeaveId As New SqlParameter()
                sqlParamLeaveId.ParameterName = "@PermissionId"
                sqlParamLeaveId.Value = PermissionId
                sqlParamLeaveId.Direction = ParameterDirection.Output

                If (PermEndDate <> DateTime.MinValue) Then
                    errNo = objDac.AddUpdateDeleteSPTrans(Emp_PermissionsRequest_Insert, _
               sqlParamLeaveId, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_PermId", FK_PermId), _
               New SqlParameter("@PermDate", PermDate), _
               New SqlParameter("@FromTime", FromTime), _
               New SqlParameter("@ToTime", ToTime), _
               New SqlParameter("@IsFullDay", IsFullDay), _
               New SqlParameter("@Remark", Remark), _
               New SqlParameter("@IsForPeriod", IsForPeriod), _
               New SqlParameter("@PermEndDate", PermEndDate), _
               New SqlParameter("@IsSpecificDays", IsSpecificDays), _
               New SqlParameter("@Days", Days), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@CREATED_DATE", IIf(CREATED_DATE = Nothing, DBNull.Value, CREATED_DATE)), _
               New SqlParameter("@FK_StatusId", FK_StatusId), _
               New SqlParameter("@IsFlexible", IsFlexible), _
               New SqlParameter("@FlexibilePermissionDuration", FlexibilePermissionDuration), _
               New SqlParameter("@AttachedFile", IIf(AttachedFile = Nothing, DBNull.Value, AttachedFile)), _
               New SqlParameter("@RequestedByCoordinator", RequestedByCoordinator))
                Else
                    errNo = objDac.AddUpdateDeleteSPTrans(Emp_PermissionsRequest_Insert, _
               sqlParamLeaveId, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_PermId", FK_PermId), _
               New SqlParameter("@PermDate", PermDate), _
               New SqlParameter("@FromTime", FromTime), _
               New SqlParameter("@ToTime", ToTime), _
               New SqlParameter("@IsFullDay", IsFullDay), _
               New SqlParameter("@Remark", Remark), _
               New SqlParameter("@IsForPeriod", IsForPeriod), _
               New SqlParameter("@IsSpecificDays", IsSpecificDays), _
               New SqlParameter("@Days", Days), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@CREATED_DATE", IIf(CREATED_DATE = Nothing, DBNull.Value, CREATED_DATE)), _
               New SqlParameter("@FK_StatusId", FK_StatusId), _
               New SqlParameter("@IsFlexible", IsFlexible), _
               New SqlParameter("@FlexibilePermissionDuration", FlexibilePermissionDuration), _
               New SqlParameter("@AttachedFile", IIf(AttachedFile = Nothing, DBNull.Value, AttachedFile)), _
               New SqlParameter("@RequestedByCoordinator", RequestedByCoordinator))
                End If

                PermissionId = sqlParamLeaveId.Value
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal PermissionId As Long, ByVal FK_EmployeeId As Long, ByVal FK_PermId As Integer, ByVal PermDate As DateTime, ByVal FromTime As DateTime, ByVal ToTime As DateTime, ByVal IsFullDay As Boolean, ByVal Remark As String, ByVal AttachedFile As String, ByVal IsForPeriod As Boolean, ByVal PermEndDate As DateTime, ByVal IsSpecificDays As Boolean, ByVal Days As String, ByVal IsFlexible As Boolean, ByVal IsDividable As Boolean, ByVal LAST_UPDATE_BY As String, ByVal FK_StatusId As Integer, ByVal FlexibilePermissionDuration As Integer, ByVal RequestedByCoordinator As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                If (PermEndDate <> DateTime.MinValue) Then
                    errNo = objDac.AddUpdateDeleteSPTrans(Emp_PermissionsRequest_Update, New SqlParameter("@PermissionId", PermissionId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_PermId", FK_PermId), _
               New SqlParameter("@PermDate", PermDate), _
               New SqlParameter("@FromTime", FromTime), _
               New SqlParameter("@ToTime", ToTime), _
               New SqlParameter("@IsFullDay", IsFullDay), _
               New SqlParameter("@Remark", Remark), _
               New SqlParameter("@IsForPeriod", IsForPeriod), _
               New SqlParameter("@PermEndDate", PermEndDate), _
               New SqlParameter("@IsSpecificDays", IsSpecificDays), _
               New SqlParameter("@Days", Days), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
               New SqlParameter("@FK_StatusId", FK_StatusId), _
               New SqlParameter("@IsFlexible", IsFlexible), _
               New SqlParameter("@FlexibilePermissionDuration", FlexibilePermissionDuration), _
               New SqlParameter("@AttachedFile", IIf(AttachedFile = Nothing, DBNull.Value, AttachedFile)), _
               New SqlParameter("@RequestedByCoordinator", RequestedByCoordinator))
                Else
                    errNo = objDac.AddUpdateDeleteSPTrans(Emp_PermissionsRequest_Update, New SqlParameter("@PermissionId", PermissionId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_PermId", FK_PermId), _
               New SqlParameter("@PermDate", PermDate), _
               New SqlParameter("@FromTime", FromTime), _
               New SqlParameter("@ToTime", ToTime), _
               New SqlParameter("@IsFullDay", IsFullDay), _
               New SqlParameter("@Remark", Remark), _
               New SqlParameter("@IsForPeriod", IsForPeriod), _
               New SqlParameter("@IsSpecificDays", IsSpecificDays), _
               New SqlParameter("@Days", Days), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
               New SqlParameter("@FK_StatusId", FK_StatusId), _
               New SqlParameter("@IsFlexible", IsFlexible), _
               New SqlParameter("@FlexibilePermissionDuration", FlexibilePermissionDuration), _
               New SqlParameter("@AttachedFile", IIf(AttachedFile = Nothing, DBNull.Value, AttachedFile)), _
               New SqlParameter("@RequestedByCoordinator", RequestedByCoordinator))
                End If

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal PermissionId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_PermissionsRequest_Delete, New SqlParameter("@PermissionId", PermissionId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal PermissionId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Emp_PermissionsRequest_Select, New SqlParameter("@PermissionId", PermissionId)).Rows(0)
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
                objColl = objDac.GetDataTable(Emp_PermissionsRequest_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Get_RestPermission_RemainingBalanceRequest(ByVal FK_EmployeeId As Integer, ByVal PermDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_Permissions_RestPermission_RemainingBalance_Request, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                              New SqlParameter("@Date", PermDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetByEmployee(ByVal FK_EmployeeId As Integer, ByVal PermFromDate As DateTime, ByVal PermToDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_PermissionsRequest_Select_ByEmp, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                              New SqlParameter("@FromDate", IIf(PermFromDate = DateTime.MinValue, DBNull.Value, PermFromDate)), _
                                              New SqlParameter("@ToDate", IIf(PermToDate = DateTime.MinValue, DBNull.Value, PermToDate)))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetByStatusType(ByVal FK_EmployeeId As Integer, ByVal FK_StatusId As Integer, ByVal PermFromDate As DateTime, ByVal PermToDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_PermissionsRequest_Select_ByStatus, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), New SqlParameter("@FK_StatusId", FK_StatusId), _
                                              New SqlParameter("@FromDate", PermFromDate), _
                                              New SqlParameter("@ToDate", PermToDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetByDirectManager(ByVal FK_ManagerId As Integer, ByVal FK_StatusId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_PermissionsRequest_Select_ByDM, New SqlParameter("@FK_ManagerId", FK_ManagerId), New SqlParameter("@FK_StatusId", FK_StatusId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function UpdatePermissionStatus(ByVal PermissionId As Integer, ByVal FK_StatusId As Integer, ByVal RejectionReason As String, ByVal LAST_UPDATE_BY As String, ByVal FK_ManagerId As Integer, ByVal FK_HREmployeeId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_PermissionRequest_UpdateStatus, New SqlParameter("@PermissionId", PermissionId), _
               New SqlParameter("@FK_StatusId", FK_StatusId), _
               New SqlParameter("@RejectionReason", RejectionReason), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
               New SqlParameter("@FK_ManagerId", FK_ManagerId), _
               New SqlParameter("@FK_HREmployeeId", FK_HREmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByHR(ByVal FK_EmployeeHRId As Integer, ByVal FK_StatusId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_PermissionsRequest_Select_ByHR, New SqlParameter("@FK_EmployeeHRId", FK_EmployeeHRId), _
                                              New SqlParameter("@FK_StatusId", FK_StatusId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetForEmpViolation(ByVal FK_EmployeeId As Integer, ByVal permDate As DateTime, ByVal permFromTime As DateTime, ByVal permToTime As DateTime) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Emp_PermissionsRequest_Select_ByEmp, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), New SqlParameter("@PermDate", permDate), _
                                              New SqlParameter("@FromTime", permFromTime), New SqlParameter("@ToTime", permToTime)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetAll_ByHR(ByVal FK_EmployeeHRId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_PermissionsRequest_Select_All_ByHR, New SqlParameter("@FK_EmployeeHRId", FK_EmployeeHRId), _
                                              New SqlParameter("@FromDate", FromDate), _
                                              New SqlParameter("@ToDate", ToDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetAllByDirectManager(ByVal FK_ManagerId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_PermissionsRequest_Select_All_ByDM, New SqlParameter("@FK_ManagerId", FK_ManagerId), _
                                              New SqlParameter("@FromDate", FromDate), _
                                              New SqlParameter("@ToDate", ToDate))
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
                objColl = objDac.GetDataTable(GetEmmployeePermissionRequestByPermMonthandType, New SqlParameter("@FK_EmployeeID", FK_EmployeeId), _
                                              New SqlParameter("@FK_PermId", FK_PermId), _
                                              New SqlParameter("@PermMonth", PermMonth))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetAllForEmpViolation(ByVal FK_EmployeeId As Integer, ByVal FromTime As DateTime, ByVal ToTime As DateTime, ByVal PermDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_PermissionRequest_SelectForEmpViolation, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                              New SqlParameter("@PermDate", PermDate), _
                                              New SqlParameter("@FromTime", FromTime), _
                                              New SqlParameter("@ToTime", ToTime))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetByGeneralManager(ByVal FK_StatusId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_PermissionsRequest_Select_ByGM, New SqlParameter("@FK_StatusId", FK_StatusId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetAllForEmpViolationFromTime(ByVal FK_EmployeeId As Integer, ByVal FromTime As DateTime, ByVal PermDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_PermissionRequest_SelectForEmpViolation_FromTime, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                              New SqlParameter("@PermDate", PermDate), _
                                              New SqlParameter("@FromTime", FromTime))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetAllForEmpViolationToTime(ByVal FK_EmployeeId As Integer, ByVal ToTime As DateTime, ByVal PermDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_PermissionRequest_SelectForEmpViolation_ToTime, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                              New SqlParameter("@PermDate", PermDate), _
                                              New SqlParameter("@ToTime", ToTime))
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
                objRow = objDac.GetDataTable(CALC_EFFECTIVE_SCHEDULE_WithFlix2, New SqlParameter("@EMPID", FK_EmployeeId), _
                                             New SqlParameter("@MoveDATE", M_Date)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetOccuranceForWeek(ByVal FromDate As DateTime, ByVal EmployeeId As Integer, ByVal PermTypeId As Integer) As Integer

            objDac = DAC.getDAC()
            Dim dtLeavePerWeek As New DataTable
            Try
                dtLeavePerWeek = objDac.GetDataTable(PermissionRequestOccurancePerWeek_select, New SqlParameter("@PermDate", FromDate), _
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
                dtLeavePerWeek = objDac.GetDataTable(PermissionRequestOccurancePerMonth_select, New SqlParameter("@PermDate", FromDate), _
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
                dtLeavePerWeek = objDac.GetDataTable(PermissionRequestOccurancePerYear_select, New SqlParameter("@PermDate", FromDate), _
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
                dtLeavePerWeek = objDac.GetDataTable(PermissionRequestOccurancePerDay_select, New SqlParameter("@PermDate", FromDate), _
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
                dtLeavePerWeek = objDac.GetDataTable(PermissionRequestDurationPerWeek_select, New SqlParameter("@PermDate", FromDate), _
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

        Public Function GetDurationForMonth(ByVal FromDate As DateTime, ByVal EmployeeId As Integer, ByVal PermTypeId As Integer) As Integer

            objDac = DAC.getDAC()
            Dim dtLeavePerWeek As New DataTable
            Try
                dtLeavePerWeek = objDac.GetDataTable(PermissionRequestDurationPerMonth_select, New SqlParameter("@PermDate", FromDate), _
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
                dtLeavePerWeek = objDac.GetDataTable(PermissionRequestDurationPerDay_select, New SqlParameter("@PermDate", FromDate), _
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
                dtLeavePerWeek = objDac.GetDataTable(PermissionRequestDurationPerYear_select, New SqlParameter("@PermDate", FromDate), _
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

        Public Function GetAllDurationByEmployee(ByVal FK_EmployeeId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal FK_PermId As Integer, ByVal PermissionId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_PermissionsRequestDuration_SelectAllByEmployee, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
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
        Public Function CheckHasPermissionDuringTime(ByVal FK_EmployeeId As Integer, ByVal permDate As DateTime, ByVal permFromTime As DateTime, ByVal permToTime As DateTime) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Emp_PermissionsRequest_IsExist, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), New SqlParameter("@PermDate", permDate), _
                                              New SqlParameter("@FromTime", permFromTime), New SqlParameter("@ToTime", permToTime)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function PermissionStatusDuringTime(ByVal FK_EmployeeId As Integer, ByVal permDate As DateTime, ByVal permFromTime As DateTime, ByVal permToTime As DateTime) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Emp_PermissionsRequest_PermStatus, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), New SqlParameter("@PermDate", permDate), _
                                              New SqlParameter("@FromTime", permFromTime), New SqlParameter("@ToTime", permToTime)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function
        Public Function CheckHasNursingOrStudyPermission(ByVal FK_EmployeeId As Integer, ByVal PermDate As DateTime, ByVal PermEndDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(HasNursingOrStudyPermission, New SqlParameter("@Fk_EmployeeId", FK_EmployeeId), _
                                                                            New SqlParameter("@PermDate", PermDate), _
                                                                            New SqlParameter("@PermEndDate", IIf(PermEndDate = DateTime.MinValue, DBNull.Value, PermEndDate)))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function CheckPermissionTypeOccurance(ByVal FK_PermId As Integer, ByVal FK_EmployeeId As Integer, ByVal PermDate As DateTime, ByVal Lang As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_PermissionsRequest_CheckPermissionTypeOccurance, New SqlParameter("@FK_PermId", FK_PermId), _
                                                                            New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                                                            New SqlParameter("@PermDate", PermDate), _
                                                                            New SqlParameter("@Lang", Lang))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function IsAllowedToRequest(ByVal FK_EmployeeId As Integer, ByVal PermDate As DateTime, ByVal PermEndDate As DateTime, ByVal FromTime As DateTime, ByVal ToTime As DateTime, ByVal FK_PermId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_PermissionsRequest_AllowedToRequest, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
                                                                            New SqlParameter("@PermDate", IIf(PermDate = DateTime.MinValue, DBNull.Value, PermDate)),
                                                                            New SqlParameter("@PermEndDate", IIf(PermEndDate = DateTime.MinValue, DBNull.Value, PermEndDate)),
                                                                            New SqlParameter("@FromTime", IIf(FromTime = DateTime.MinValue, DBNull.Value, FromTime)),
                                                                            New SqlParameter("@ToTime", IIf(ToTime = DateTime.MinValue, DBNull.Value, ToTime)),
                                                                            New SqlParameter("@FK_PermId", FK_PermId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetByDirectManager_Mobile(ByVal FK_ManagerId As Integer, ByVal FK_StatusId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_PermissionsRequest_Select_ByDM_Mobile, New SqlParameter("@FK_ManagerId", FK_ManagerId),
                                              New SqlParameter("@FK_StatusId", FK_StatusId))
            Catch ex As Exception
                errNo = -11
            End Try
            Return objColl
        End Function

        Public Function GetApprovedByDirectManager_Date_Mobile(ByVal FK_ManagerId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_PermissionsRequest_Select_Approved_ByDM_Date_Mobile, New SqlParameter("@FK_ManagerId", FK_ManagerId),
                                              New SqlParameter("@FromDate", FromDate),
                                              New SqlParameter("@ToDate", ToDate))
            Catch ex As Exception
                errNo = -11
            End Try
            Return objColl
        End Function

#End Region

    End Class
End Namespace