Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.LookUp

Namespace TA.SelfServices

    Public Class DALEmp_Study_PermissionRequest
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Emp_Study_PermissionRequest_Select As String = "Emp_Study_PermissionRequest_select"
        Private Emp_Study_PermissionRequest_Select_All As String = "Emp_Study_PermissionRequest_select_All"
        Private Emp_Study_PermissionRequest_Insert As String = "Emp_Study_PermissionRequest_Insert"
        Private Emp_Study_PermissionRequest_Update As String = "Emp_Study_PermissionRequest_Update"
        Private Emp_Study_PermissionRequest_Delete As String = "Emp_Study_PermissionRequest_Delete"
        Private Emp_Study_PermissionsRequest_Select_ByEmp As String = "Emp_Study_PermissionsRequest_Select_ByEmp"
        Private Emp_StudyPermissionsRequest_Select_ByStatus As String = "Emp_StudyPermissionsRequest_Select_ByStatus"
        Private Emp_StudyPermissionsRequest_Select_ByDM As String = "Emp_StudyPermissionsRequest_Select_ByDM"
        Private Emp_StudyPermissionsRequest_Select_ByHR As String = "Emp_StudyPermissionsRequest_Select_ByHR"
        Private Emp_StudyPermissionRequest_UpdateStatus As String = "Emp_StudyPermissionRequest_UpdateStatus"
        Private Emp_StudyPermissionsRequest_Select_ByGM As String = "Emp_StudyPermissionsRequest_Select_ByGM"
        Private Emp_PermissionsRequest_IsExist As String = "Emp_PermissionsRequest_IsExist"
#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef PermissionId As Integer, ByVal FK_EmployeeId As Long, ByVal FK_PermId As Integer, ByVal PermDate As DateTime,
                            ByVal FromTime As DateTime, ByVal ToTime As DateTime, ByVal IsFullDay As Boolean, ByVal Remark As String, ByVal AttachedFile As String,
                            ByVal IsForPeriod As Boolean, ByVal PermEndDate As DateTime, ByVal IsSpecificDays As Boolean, ByVal Days As String,
                            ByVal IsFlexible As Boolean, ByVal IsDividable As Boolean, ByVal RejectionReason As String, ByVal CREATED_BY As String,
                            ByVal FK_StatusId As Integer, ByVal FlexibilePermissionDuration As Integer, ByVal FK_ManagerId As Integer,
                            ByVal FK_HREmployeeId As Integer, ByVal StudyYear As Integer, ByVal Semester As String, ByVal FK_UniversityId As Integer?,
                            ByVal Emp_GPAType As Integer?, ByVal Emp_GPA As Decimal?, ByVal FK_MajorId As Integer?, ByVal FK_SpecializationId As Integer?) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@PermissionId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, PermissionId)
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Study_PermissionRequest_Insert, sqlOut, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
               New SqlParameter("@FK_PermId", FK_PermId),
               New SqlParameter("@PermDate", PermDate),
               New SqlParameter("@FromTime", FromTime),
               New SqlParameter("@ToTime", ToTime),
               New SqlParameter("@IsFullDay", IsFullDay),
               New SqlParameter("@Remark", Remark),
               New SqlParameter("@AttachedFile", AttachedFile),
               New SqlParameter("@IsForPeriod", IsForPeriod),
               New SqlParameter("@PermEndDate", PermEndDate),
               New SqlParameter("@IsSpecificDays", IsSpecificDays),
               New SqlParameter("@Days", Days),
               New SqlParameter("@IsFlexible", IsFlexible),
               New SqlParameter("@IsDividable", IsDividable),
               New SqlParameter("@RejectionReason", RejectionReason),
               New SqlParameter("@CREATED_BY", CREATED_BY),
               New SqlParameter("@FK_StatusId", FK_StatusId),
               New SqlParameter("@FlexibilePermissionDuration", FlexibilePermissionDuration),
               New SqlParameter("@FK_ManagerId", FK_ManagerId),
               New SqlParameter("@FK_HREmployeeId", FK_HREmployeeId),
               New SqlParameter("@StudyYear", StudyYear),
               New SqlParameter("@Semester", Semester),
               New SqlParameter("@FK_UniversityId", FK_UniversityId),
               New SqlParameter("@Emp_GPAType", Emp_GPAType),
               New SqlParameter("@Emp_GPA", Emp_GPA),
               New SqlParameter("@FK_MajorId", FK_MajorId),
               New SqlParameter("@FK_SpecializationId", FK_SpecializationId))
                PermissionId = sqlOut.Value
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal PermissionRequestId As Long, ByVal FK_EmployeeId As Long, ByVal FK_PermId As Integer, ByVal PermDate As DateTime,
                               ByVal FromTime As DateTime, ByVal ToTime As DateTime, ByVal IsFullDay As Boolean, ByVal Remark As String,
                               ByVal AttachedFile As String, ByVal IsForPeriod As Boolean, ByVal PermEndDate As DateTime, ByVal IsSpecificDays As Boolean,
                               ByVal Days As String, ByVal IsFlexible As Boolean, ByVal IsDividable As Boolean, ByVal RejectionReason As String,
                               ByVal LAST_UPDATE_BY As String, ByVal FK_StatusId As Integer, ByVal FlexibilePermissionDuration As Integer,
                               ByVal FK_ManagerId As Integer, ByVal FK_HREmployeeId As Integer, ByVal StudyYear As Integer, ByVal Semester As String,
                               ByVal FK_UniversityId As Integer?, ByVal Emp_GPAType As Integer?, ByVal Emp_GPA As Decimal?, ByVal FK_MajorId As Integer?, ByVal FK_SpecializationId As Integer?) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Study_PermissionRequest_Update, New SqlParameter("@PermissionRequestId", PermissionRequestId),
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
               New SqlParameter("@FK_PermId", FK_PermId),
               New SqlParameter("@PermDate", PermDate),
               New SqlParameter("@FromTime", FromTime),
               New SqlParameter("@ToTime", ToTime),
               New SqlParameter("@IsFullDay", IsFullDay),
               New SqlParameter("@Remark", Remark),
               New SqlParameter("@AttachedFile", AttachedFile),
               New SqlParameter("@IsForPeriod", IsForPeriod),
               New SqlParameter("@PermEndDate", PermEndDate),
               New SqlParameter("@IsSpecificDays", IsSpecificDays),
               New SqlParameter("@Days", Days),
               New SqlParameter("@IsFlexible", IsFlexible),
               New SqlParameter("@IsDividable", IsDividable),
               New SqlParameter("@RejectionReason", RejectionReason),
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY),
               New SqlParameter("@FK_StatusId", FK_StatusId),
               New SqlParameter("@FlexibilePermissionDuration", FlexibilePermissionDuration),
               New SqlParameter("@FK_ManagerId", FK_ManagerId),
               New SqlParameter("@FK_HREmployeeId", FK_HREmployeeId),
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

        Public Function Delete(ByVal PermissionRequestId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Study_PermissionRequest_Delete, New SqlParameter("@PermissionRequestId", PermissionRequestId))
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
                objRow = objDac.GetDataTable(Emp_Study_PermissionRequest_Select, New SqlParameter("@PermissionRequestId", PermissionRequestId)).Rows(0)
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
                objColl = objDac.GetDataTable(Emp_Study_PermissionRequest_Select_All, Nothing)
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
                objColl = objDac.GetDataTable(Emp_Study_PermissionsRequest_Select_ByEmp, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
                                              New SqlParameter("@FromDate", IIf(PermFromDate = DateTime.MinValue, DBNull.Value, PermFromDate)),
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
                objColl = objDac.GetDataTable(Emp_StudyPermissionsRequest_Select_ByStatus, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), New SqlParameter("@FK_StatusId", FK_StatusId),
                                              New SqlParameter("@FromDate", PermFromDate),
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
                objColl = objDac.GetDataTable(Emp_StudyPermissionsRequest_Select_ByDM, New SqlParameter("@FK_ManagerId", FK_ManagerId), New SqlParameter("@FK_StatusId", FK_StatusId))
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
                objColl = objDac.GetDataTable(Emp_StudyPermissionsRequest_Select_ByHR, New SqlParameter("@FK_EmployeeHRId", FK_EmployeeHRId), New SqlParameter("@FK_StatusId", FK_StatusId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function UpdatePermissionStatus(ByVal PermissionId As Integer, ByVal FK_StatusId As Integer, ByVal RejectionReason As String, ByVal LAST_UPDATE_BY As String, ByVal FK_ManagerId As Integer, ByVal FK_HREmployeeId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_StudyPermissionRequest_UpdateStatus, New SqlParameter("@PermissionId", PermissionId),
               New SqlParameter("@FK_StatusId", FK_StatusId),
               New SqlParameter("@RejectionReason", RejectionReason),
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY),
               New SqlParameter("@FK_ManagerId", FK_ManagerId),
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
                objColl = objDac.GetDataTable(Emp_StudyPermissionsRequest_Select_ByGM, New SqlParameter("@FK_StatusId", FK_StatusId))
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
                objRow = objDac.GetDataTable(Emp_PermissionsRequest_IsExist, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), New SqlParameter("@PermDate", permDate),
                                              New SqlParameter("@FromTime", permFromTime), New SqlParameter("@ToTime", permToTime)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

#End Region


    End Class
End Namespace