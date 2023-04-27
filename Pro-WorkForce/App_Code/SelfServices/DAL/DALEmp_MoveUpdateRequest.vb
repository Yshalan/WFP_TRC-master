Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.SelfServices

    Public Class DALEmp_MoveUpdateRequest
        Inherits MGRBase



#Region "Class Variables"

        Private strConn As String
        Private Emp_MoveUpdateRequest_Select As String = "Emp_MoveUpdateRequest_select"
        Private Emp_MoveUpdateRequest_Select_All As String = "Emp_MoveUpdateRequest_select_All"
        Private Emp_MoveUpdateRequest_Insert As String = "Emp_MoveUpdateRequest_Insert"
        Private Emp_MoveUpdateRequest_Update As String = "Emp_MoveUpdateRequest_Update"
        Private Emp_MoveUpdateRequest_Delete As String = "Emp_MoveUpdateRequest_Delete"
        Private Emp_MoveUpdateRequest_PerDay As String = "Emp_MoveUpdateRequest_PerDay"
        Private ManualRequest_CheckHasInOrOut As String = "ManualRequest_CheckHasInOrOut"
        Private Emp_MoveUpdateRequest_HasRequest As String = "Emp_MoveUpdateRequest_HasRequest"
        Private Emp_MoveUpdateRequest_Select_Filter As String = "Emp_MoveUpdateRequest_Select_Filter"
        Private Emp_MoveUpdateRequest_Select_ByDM As String = "Emp_MoveUpdateRequest_Select_ByDM"
        Private Emp_MoveUpdateRequest_UpdateStatus As String = "Emp_MoveUpdateRequest_UpdateStatus"
        Private Emp_MoveUpdateRequest_Select_ByHR As String = "Emp_MoveUpdateRequest_Select_ByHR"
        Private Emp_MoveUpdateRequest_Select_ByGM As String = "Emp_MoveUpdateRequest_Select_ByGM"
        Private Emp_MoveUpdateRequest_CheckHasInOrOut As String = "Emp_MoveUpdateRequest_CheckHasInOrOut"

#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef MoveRequestId As Integer, ByVal FK_EmployeeId As Long, ByVal Type As String, ByVal MoveDate As DateTime, ByVal MoveTime As DateTime, ByVal FK_ReasonId As Integer, ByVal Remarks As String, ByVal Reader As String, ByVal M_DATE_NUM As String, ByVal M_TIME_NUM As String, ByVal Status As String, ByVal IsManual As Boolean, ByVal CREATED_BY As String, ByVal IsFromMobile As Boolean, ByVal MobileCoordinates As String, ByVal IsRejected As Boolean, ByVal AttachedFile As String, ByVal RejectionReason As String, ByVal FK_ManagerId As Integer, ByVal FK_HREmployeeId As Integer, ByVal FK_WorkLocationId As Integer, ByVal MoveId As Integer, ByVal UpdateTransactionType As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@MoveRequestId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, MoveRequestId)
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_MoveUpdateRequest_Insert, sqlOut, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@Type", Type), _
               New SqlParameter("@MoveDate", MoveDate), _
               New SqlParameter("@MoveTime", MoveTime), _
               New SqlParameter("@FK_ReasonId", FK_ReasonId), _
               New SqlParameter("@Remarks", Remarks), _
               New SqlParameter("@Reader", Reader), _
               New SqlParameter("@M_DATE_NUM", M_DATE_NUM), _
               New SqlParameter("@M_TIME_NUM", M_TIME_NUM), _
               New SqlParameter("@Status", Status), _
               New SqlParameter("@IsManual", IsManual), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@IsFromMobile", IsFromMobile), _
               New SqlParameter("@MobileCoordinates", MobileCoordinates), _
               New SqlParameter("@IsRejected", IsRejected), _
               New SqlParameter("@AttachedFile", AttachedFile), _
               New SqlParameter("@RejectionReason", RejectionReason), _
               New SqlParameter("@FK_ManagerId", FK_ManagerId), _
               New SqlParameter("@FK_HREmployeeId", FK_HREmployeeId), _
               New SqlParameter("@FK_WorkLocationId", FK_WorkLocationId), _
               New SqlParameter("@MoveId", MoveId), _
               New SqlParameter("@UpdateTransactionType", UpdateTransactionType))
                If errNo = 0 Then
                    MoveRequestId = sqlOut.Value
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal MoveRequestId As Long, ByVal FK_EmployeeId As Long, ByVal Type As String, ByVal MoveDate As DateTime, ByVal MoveTime As DateTime, ByVal FK_ReasonId As Integer, ByVal Remarks As String, ByVal Reader As String, ByVal M_DATE_NUM As String, ByVal M_TIME_NUM As String, ByVal Status As String, ByVal SYS_Date As DateTime, ByVal IsManual As Boolean, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime, ByVal IsFromMobile As Boolean, ByVal MobileCoordinates As String, ByVal IsRejected As Boolean, ByVal AttachedFile As String, ByVal RejectionReason As String, ByVal FK_ManagerId As Integer, ByVal FK_HREmployeeId As Integer, ByVal FK_WorkLocationId As Integer, ByVal MoveId As Integer, ByVal UpdateTransactionType As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_MoveUpdateRequest_Update, New SqlParameter("@MoveRequestId", MoveRequestId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@Type", Type), _
               New SqlParameter("@MoveDate", MoveDate), _
               New SqlParameter("@MoveTime", MoveTime), _
               New SqlParameter("@FK_ReasonId", FK_ReasonId), _
               New SqlParameter("@Remarks", Remarks), _
               New SqlParameter("@Reader", Reader), _
               New SqlParameter("@M_DATE_NUM", M_DATE_NUM), _
               New SqlParameter("@M_TIME_NUM", M_TIME_NUM), _
               New SqlParameter("@Status", Status), _
               New SqlParameter("@SYS_Date", SYS_Date), _
               New SqlParameter("@IsManual", IsManual), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@CREATED_DATE", CREATED_DATE), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
               New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE), _
               New SqlParameter("@IsFromMobile", IsFromMobile), _
               New SqlParameter("@MobileCoordinates", MobileCoordinates), _
               New SqlParameter("@IsRejected", IsRejected), _
               New SqlParameter("@AttachedFile", AttachedFile), _
               New SqlParameter("@RejectionReason", RejectionReason), _
               New SqlParameter("@FK_ManagerId", FK_ManagerId), _
               New SqlParameter("@FK_HREmployeeId", FK_HREmployeeId), _
               New SqlParameter("@FK_WorkLocationId", FK_WorkLocationId), _
               New SqlParameter("@MoveId", MoveId), _
               New SqlParameter("@UpdateTransactionType", UpdateTransactionType))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal MoveRequestId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_MoveUpdateRequest_Delete, New SqlParameter("@MoveRequestId", MoveRequestId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal MoveRequestId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Emp_MoveUpdateRequest_Select, New SqlParameter("@MoveRequestId", MoveRequestId)).Rows(0)
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
                objColl = objDac.GetDataTable(Emp_MoveUpdateRequest_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetRequestsPerDay(ByVal FK_EmployeeId As Integer, ByVal MoveDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_MoveUpdateRequest_PerDay, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                             New SqlParameter("@MoveDate", MoveDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function CheckHasInOrOut(ByVal FK_EmployeeId As Integer, ByVal MoveDate As DateTime, ByVal FK_ReasonId As Integer, ByVal MoveTime As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_MoveUpdateRequest_CheckHasInOrOut, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                                                             New SqlParameter("@MoveDate", MoveDate), _
                                                                             New SqlParameter("@FK_ReasonId", FK_ReasonId), _
                                                                             New SqlParameter("@MoveTime", MoveTime))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function IfExists_EmpMoveRequest(ByVal FK_EmployeeId As Integer, ByVal MoveDate As DateTime, ByVal FK_ReasonId As Integer, ByVal MoveTime As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_MoveUpdateRequest_HasRequest, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                                                             New SqlParameter("@MoveDate", MoveDate), _
                                                                             New SqlParameter("@FK_ReasonId", FK_ReasonId), _
                                                                             New SqlParameter("@MoveTime", MoveTime))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetFilter(ByVal EMP_NO As Integer, ByVal FromDate As DateTime?, ByVal ToDate As DateTime?, ByVal Status As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_MoveUpdateRequest_Select_Filter, New SqlParameter("@EmployeeId", EMP_NO), _
               New SqlParameter("@FromDate", IIf(FromDate = DateTime.MinValue, DBNull.Value, FromDate)), _
               New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, DBNull.Value, ToDate)), _
               New SqlParameter("@Status", Status))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetByDirectManager(ByVal FK_ManagerId As Integer, ByVal Status As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_MoveUpdateRequest_Select_ByDM, New SqlParameter("@FK_ManagerId", FK_ManagerId), _
                                              New SqlParameter("@FK_StatusId", Status))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function UpdateManualRequestStatus(ByVal MoveRequestId As Integer, ByVal FK_StatusId As Integer, ByVal RejectionReason As String, ByVal LAST_UPDATE_BY As String, ByVal FK_ManagerId As Integer, ByVal FK_HREmployeeId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_MoveUpdateRequest_UpdateStatus, New SqlParameter("@MoveRequestId", MoveRequestId), _
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

        Public Function GetByHR(ByVal FK_HREmployeeId As Integer, ByVal Status As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_MoveUpdateRequest_Select_ByHR, New SqlParameter("@FK_EmployeeHRId", FK_HREmployeeId), _
                                              New SqlParameter("@FK_StatusId", Status))
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
                objColl = objDac.GetDataTable(Emp_MoveUpdateRequest_Select_ByGM, New SqlParameter("@FK_StatusId", FK_StatusId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

#End Region


    End Class
End Namespace