Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.SelfServices

    Public Class DALEmp_Nursing_Permission
        Inherits MGRBase



#Region "Class Variables"

        Private strConn As String
        Private Emp_Nursing_Permission_Select As String = "Emp_Nursing_Permission_select"
        Private Emp_Nursing_Permission_Select_All As String = "Emp_Nursing_Permission_select_All"
        Private Emp_Nursing_Permission_Insert As String = "Emp_Nursing_Permission_Insert"
        Private Emp_Nursing_Permission_Update As String = "Emp_Nursing_Permission_Update"
        Private Emp_Nursing_Permission_Delete As String = "Emp_Nursing_Permission_Delete"
        Private Emp_Nursing_PermissionsRequest_Select_ByEmp As String = "Emp_Nursing_PermissionsRequest_Select_ByEmp"
        Private Emp_NurPermissionsRequest_Select_ByDM As String = "Emp_NurPermissionsRequest_Select_ByDM"
        Private Emp_NurPermissionsRequest_Select_ByHR As String = "Emp_NurPermissionsRequest_Select_ByHR"
        Private Emp_NurPermissionRequest_UpdateStatus As String = "Emp_NurPermissionRequest_UpdateStatus"
        Private Emp_NurPermissionsRequest_Select_ByGM As String = "Emp_NurPermissionsRequest_Select_ByGM"
        Private Emp_NurPermissionsRequest_Select_ByStatus As String = "Emp_NurPermissionsRequest_Select_ByStatus"
        Private CheckHasMaternityLeave As String = "CheckHasMaternityLeave"
        Private Emp_Nursing_Permission_Exist As String = "Emp_Nursing_Permission_Exist"
        Private Emp_Permission_ExistNursing As String = "Emp_Permission_ExistNursing"
        Private AllowedNursingPermissionInRamadan As String = "AllowedNursingPermissionInRamadan"
#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef PermissionId As Integer, ByVal FK_EmployeeId As Long, ByVal FK_PermId As Integer, ByVal PermDate As DateTime, ByVal FromTime As DateTime, ByVal ToTime As DateTime, ByVal IsFullDay As Boolean, ByVal Remark As String, ByVal AttachedFile As String, ByVal IsForPeriod As Boolean, ByVal PermEndDate As DateTime, ByVal IsSpecificDays As Boolean, ByVal Days As String, ByVal IsFlexible As Boolean, ByVal IsDividable As Boolean, ByVal RejectionReason As String, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime, ByVal FK_StatusId As Integer, ByVal FlexibilePermissionDuration As Integer, ByVal FK_ManagerId As Integer, ByVal FK_HREmployeeId As Integer, ByVal AllowedTime As Integer) As Integer

            objDac = DAC.getDAC()
            Try

                Dim sqlOut = New SqlParameter("@PermissionId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, PermissionId)
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Nursing_Permission_Insert, sqlOut, _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_PermId", FK_PermId), _
               New SqlParameter("@PermDate", PermDate), _
               New SqlParameter("@FromTime", IIf(FromTime = Nothing, DBNull.Value, FromTime)), _
               New SqlParameter("@ToTime", IIf(ToTime = Nothing, DBNull.Value, ToTime)), _
               New SqlParameter("@IsFullDay", IsFullDay), _
               New SqlParameter("@Remark", Remark), _
               New SqlParameter("@AttachedFile", AttachedFile), _
               New SqlParameter("@IsForPeriod", IsForPeriod), _
               New SqlParameter("@PermEndDate", PermEndDate), _
               New SqlParameter("@IsSpecificDays", IsSpecificDays), _
               New SqlParameter("@IsFlexible", IsFlexible), _
               New SqlParameter("@IsDividable", IsDividable), _
               New SqlParameter("@RejectionReason", RejectionReason), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@CREATED_DATE", IIf(CREATED_DATE = DateTime.MinValue, DBNull.Value, CREATED_DATE)), _
               New SqlParameter("@FK_StatusId", FK_StatusId), _
               New SqlParameter("@FlexibilePermissionDuration", FlexibilePermissionDuration), _
               New SqlParameter("@AllowedTime", AllowedTime))
                PermissionId = sqlOut.Value
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal PermissionRequestId As Long, ByVal FK_EmployeeId As Long, ByVal FK_PermId As Integer, ByVal PermDate As DateTime, ByVal FromTime As DateTime, ByVal ToTime As DateTime, ByVal IsFullDay As Boolean, ByVal Remark As String, ByVal AttachedFile As String, ByVal IsForPeriod As Boolean, ByVal PermEndDate As DateTime, ByVal IsSpecificDays As Boolean, ByVal Days As String, ByVal IsFlexible As Boolean, ByVal IsDividable As Boolean, ByVal RejectionReason As String, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime, ByVal FK_StatusId As Integer, ByVal FlexibilePermissionDuration As Integer, ByVal FK_ManagerId As Integer, ByVal FK_HREmployeeId As Integer, ByVal AllowedTime As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Nursing_Permission_Update, New SqlParameter("@PermissionRequestId", PermissionRequestId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_PermId", FK_PermId), _
               New SqlParameter("@PermDate", PermDate), _
               New SqlParameter("@FromTime", FromTime), _
               New SqlParameter("@ToTime", ToTime), _
               New SqlParameter("@IsFullDay", IsFullDay), _
               New SqlParameter("@Remark", Remark), _
               New SqlParameter("@AttachedFile", AttachedFile), _
               New SqlParameter("@IsForPeriod", IsForPeriod), _
               New SqlParameter("@PermEndDate", PermEndDate), _
               New SqlParameter("@IsSpecificDays", IsSpecificDays), _
               New SqlParameter("@Days", Days), _
               New SqlParameter("@IsFlexible", IsFlexible), _
               New SqlParameter("@IsDividable", IsDividable), _
               New SqlParameter("@RejectionReason", RejectionReason), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@CREATED_DATE", CREATED_DATE), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
               New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE), _
               New SqlParameter("@FK_StatusId", FK_StatusId), _
               New SqlParameter("@FlexibilePermissionDuration", FlexibilePermissionDuration), _
               New SqlParameter("@FK_ManagerId", FK_ManagerId), _
               New SqlParameter("@FK_HREmployeeId", FK_HREmployeeId), _
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
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Nursing_Permission_Delete, New SqlParameter("@PermissionRequestId", PermissionRequestId))
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
                objRow = objDac.GetDataTable(Emp_Nursing_Permission_Select, New SqlParameter("@PermissionRequestId", PermissionRequestId)).Rows(0)
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
                objColl = objDac.GetDataTable(Emp_Nursing_Permission_Select_All, Nothing)
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
                objColl = objDac.GetDataTable(Emp_Nursing_PermissionsRequest_Select_ByEmp, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                              New SqlParameter("@FromDate", IIf(PermFromDate = DateTime.MinValue, DBNull.Value, PermFromDate)), _
                                              New SqlParameter("@ToDate", IIf(PermToDate = DateTime.MinValue, DBNull.Value, PermToDate)))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function HasNursingRequest(ByVal FK_EmployeeId As Integer, ByVal PermFromDate As DateTime, ByVal PermToDate As DateTime) As Boolean

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_Nursing_Permission_Exist, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                              New SqlParameter("@PermDate", IIf(PermFromDate = DateTime.MinValue, DBNull.Value, PermFromDate)), _
                                              New SqlParameter("@PermEndDate", IIf(PermToDate = DateTime.MinValue, DBNull.Value, PermToDate)))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            If objColl Is Nothing Or objColl.Rows.Count = 0 Then
                Return False
            End If
            Return True


        End Function
        Public Function HasNursing(ByVal FK_EmployeeId As Integer, ByVal PermFromDate As DateTime, ByVal PermToDate As DateTime) As Boolean

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_Permission_ExistNursing, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                              New SqlParameter("@PermDate", IIf(PermFromDate = DateTime.MinValue, DBNull.Value, PermFromDate)), _
                                              New SqlParameter("@PermEndDate", IIf(PermToDate = DateTime.MinValue, DBNull.Value, PermToDate)))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            If objColl Is Nothing Or objColl.Rows.Count = 0 Then
                Return False
            End If
            Return True


        End Function
        Public Function GetByDirectManager(ByVal FK_ManagerId As Integer, ByVal FK_StatusId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_NurPermissionsRequest_Select_ByDM, New SqlParameter("@FK_ManagerId", FK_ManagerId), New SqlParameter("@FK_StatusId", FK_StatusId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetByHR(ByVal FK_EmployeeHRId As Integer, ByVal FK_StatusId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_NurPermissionsRequest_Select_ByHR, New SqlParameter("@FK_EmployeeHRId", FK_EmployeeHRId), New SqlParameter("@FK_StatusId", FK_StatusId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function UpdatePermissionStatus(ByVal PermissionId As Integer, ByVal FK_StatusId As Integer, ByVal RejectionReason As String, ByVal LAST_UPDATE_BY As String, ByVal FK_ManagerId As Integer, ByVal FK_HREmployeeId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_NurPermissionRequest_UpdateStatus, New SqlParameter("@PermissionId", PermissionId), _
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

        Public Function GetByGeneralManager(ByVal FK_StatusId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_NurPermissionsRequest_Select_ByGM, New SqlParameter("@FK_StatusId", FK_StatusId))
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
                objColl = objDac.GetDataTable(Emp_NurPermissionsRequest_Select_ByStatus, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), New SqlParameter("@FK_StatusId", FK_StatusId), _
                                              New SqlParameter("@FromDate", PermFromDate), _
                                              New SqlParameter("@ToDate", PermToDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function ChKHasMaternityLeave(ByVal FK_EmployeeId As Integer, ByVal FK_MaternityLeaveTypeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(CheckHasMaternityLeave, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                              New SqlParameter("@FK_MaternityLeaveTypeId", FK_MaternityLeaveTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function AllowedNursingInRamadan(ByVal PermDate As DateTime, ByVal PermEndDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(AllowedNursingPermissionInRamadan, New SqlParameter("@PermDate", PermDate), _
                                              New SqlParameter("@PermEndDate", PermEndDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
#End Region


    End Class
End Namespace