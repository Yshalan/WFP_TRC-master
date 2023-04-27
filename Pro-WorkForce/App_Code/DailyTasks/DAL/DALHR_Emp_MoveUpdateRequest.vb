Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup

Namespace TA.DailyTasks

    Public Class DALHR_Emp_MoveUpdateRequest
        Inherits MGRBase

#Region "Class Variables"
        Private strConn As String
        Private HR_Emp_MoveUpdateRequest_Select As String = "HR_Emp_MoveUpdateRequest_select"
        Private HR_Emp_MoveUpdateRequest_Select_All As String = "HR_Emp_MoveUpdateRequest_select_All"
        Private HR_Emp_MoveUpdateRequest_Insert As String = "HR_Emp_MoveUpdateRequest_Insert"
        Private HR_Emp_MoveUpdateRequest_Update As String = "HR_Emp_MoveUpdateRequest_Update"
        Private HR_Emp_MoveUpdateRequest_Delete As String = "HR_Emp_MoveUpdateRequest_Delete"
        Private HR_Emp_MoveUpdateRequest_Select_Filter As String = "HR_Emp_MoveUpdateRequest_Select_Filter"
        Private HR_Emp_MoveUpdateRequestUpdateRequest_Select_Status As String = "HR_Emp_MoveUpdateRequestUpdateRequest_Select_Status"
        Private HR_Emp_MoveUpdateRequestUpdateRequest_Select_Rejected As String = "HR_Emp_MoveUpdateRequestUpdateRequest_Select_Rejected"
        Private HR_Emp_MoveUpdateRequestUpdateRequest_Update_RequestStatus As String = "HR_Emp_MoveUpdateRequestUpdateRequest_Update_RequestStatus"
#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef MoveRequestId As Integer, ByVal FK_EmployeeId As Long, ByVal Type As String, ByVal MoveDate As DateTime, ByVal MoveTime As DateTime, ByVal FK_ReasonId As Integer, ByVal Remarks As String, ByVal Reader As String, ByVal M_DATE_NUM As String, ByVal M_TIME_NUM As String, ByVal Status As String, ByVal SYS_Date As DateTime, ByVal IsManual As Boolean, ByVal CREATED_BY As String, ByVal IsFromMobile As Boolean, ByVal MobileCoordinates As String, ByVal IsRejected As String, ByVal AttachedFile As String, ByVal RejectionReason As String, ByVal MoveId As Long, ByVal UpdateTransactionType As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@MoveRequestId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, MoveRequestId)
                errNo = objDac.AddUpdateDeleteSPTrans(HR_Emp_MoveUpdateRequest_Insert, sqlOut, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
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
               New SqlParameter("@IsFromMobile", IsFromMobile), _
               New SqlParameter("@MobileCoordinates", MobileCoordinates), _
               New SqlParameter("@IsRejected", IsRejected), _
               New SqlParameter("@AttachedFile", AttachedFile), _
               New SqlParameter("@RejectionReason", RejectionReason), _
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

        Public Function Update(ByVal MoveRequestId As Long, ByVal FK_EmployeeId As Long, ByVal Type As String, ByVal MoveDate As DateTime, ByVal MoveTime As DateTime, ByVal FK_ReasonId As Integer, ByVal Remarks As String, ByVal Reader As String, ByVal M_DATE_NUM As String, ByVal M_TIME_NUM As String, ByVal Status As String, ByVal SYS_Date As DateTime, ByVal IsManual As Boolean, ByVal LAST_UPDATE_BY As String, ByVal IsFromMobile As Boolean, ByVal MobileCoordinates As String, ByVal IsRejected As Boolean, ByVal AttachedFile As String, ByVal RejectionReason As String, ByVal MoveId As Long, ByVal UpdateTransactionType As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(HR_Emp_MoveUpdateRequest_Update, New SqlParameter("@MoveRequestId", MoveRequestId), _
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
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
               New SqlParameter("@IsFromMobile", IsFromMobile), _
               New SqlParameter("@MobileCoordinates", MobileCoordinates), _
               New SqlParameter("@IsRejected", IsRejected), _
               New SqlParameter("@AttachedFile", AttachedFile), _
               New SqlParameter("@RejectionReason", RejectionReason), _
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
                errNo = objDac.AddUpdateDeleteSPTrans(HR_Emp_MoveUpdateRequest_Delete, New SqlParameter("@MoveRequestId", MoveRequestId))
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
                objRow = objDac.GetDataTable(HR_Emp_MoveUpdateRequest_Select, New SqlParameter("@MoveRequestId", MoveRequestId)).Rows(0)
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
                objColl = objDac.GetDataTable(HR_Emp_MoveUpdateRequest_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetFilter(ByVal EMP_NO As Integer, ByVal M_DATE As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(HR_Emp_MoveUpdateRequest_Select_Filter, New SqlParameter("@EmployeeId", EMP_NO), _
               New SqlParameter("@MoveDate", M_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetAll_ByStatus(ByVal UserId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(HR_Emp_MoveUpdateRequestUpdateRequest_Select_Status, New SqlParameter("@UserId", UserId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAll_Rejected(ByVal UserId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(HR_Emp_MoveUpdateRequestUpdateRequest_Select_Rejected, New SqlParameter("@UserId", UserId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Update_Request_Status(ByVal MoveRequestId As Long, ByVal IsRejected As Boolean, ByVal RejectionReason As String, ByVal LAST_UPDATED_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(HR_Emp_MoveUpdateRequestUpdateRequest_Update_RequestStatus, New SqlParameter("@MoveRequestId", MoveRequestId), _
               New SqlParameter("@IsRejected", IsRejected), _
                New SqlParameter("@RejectionReason", RejectionReason), _
                New SqlParameter("@LAST_UPDATED_BY", LAST_UPDATED_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

#End Region

    End Class
End Namespace