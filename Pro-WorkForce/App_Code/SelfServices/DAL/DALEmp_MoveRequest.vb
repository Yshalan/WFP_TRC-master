Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.SelfServices

    Public Class DALEmp_MoveRequest
        Inherits MGRBase



#Region "Class Variables"

        Private strConn As String
        Private Emp_MoveRequest_Select As String = "Emp_MoveRequest_select"
        Private Emp_MoveRequest_Select_All As String = "Emp_MoveRequest_select_All"
        Private Emp_MoveRequest_Insert As String = "Emp_MoveRequest_Insert"
        Private Emp_MoveRequest_Update As String = "Emp_MoveRequest_Update"
        Private Emp_MoveRequest_Delete As String = "Emp_MoveRequest_Delete"
        Private EMP_MOVE_Request_Select_Filter As String = "EMP_MOVE_Request_Select_Filter"
        Private EMP_MOVE_Request_Remote_Select_Filter As String = "EMP_MOVE_Request_Remote_Select_Filter"
        Private Emp_ManualEntryRequest_Select_ByDM As String = "Emp_ManualEntryRequest_Select_ByDM"
        Private ManualRequest_UpdateStatus As String = "ManualRequest_UpdateStatus"
        Private ManualEntry_Request_Select_ByHR As String = "ManualEntry_Request_Select_ByHR"
        Private ManualEntry_Request_Select_ByGM As String = "ManualEntry_Request_Select_ByGM"
        Private Emp_MoveRequest_PerDay As String = "Emp_MoveRequest_PerDay"
        Private ManualRequest_CheckHasInOrOut As String = "ManualRequest_CheckHasInOrOut"
        Private Emp_MoveRequest_HasRequest As String = "Emp_MoveRequest_HasRequest"
        Private Emp_MoveRequest_PerMonth As String = "Emp_MoveRequest_PerMonth"
        Private Emp_MoveRequest_NumberOf_In_Out As String = "Emp_MoveRequest_NumberOf_In_Out"


#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef MoveRequestId As Integer, FK_EmployeeId As Long, ByVal Type As String, ByVal MoveDate As DateTime, ByVal MoveTime As DateTime, ByVal FK_ReasonId As Integer, ByVal Remarks As String, ByVal Reader As String, ByVal M_DATE_NUM As String, ByVal M_TIME_NUM As String, ByVal Status As String, ByVal IsManual As Boolean, ByVal CREATED_BY As String, ByVal AttachedFile As String, ByVal IsRemoteWork As Boolean, ByVal IsFromMobile As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@MoveRequestId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, MoveRequestId)
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_MoveRequest_Insert, sqlOut, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
               New SqlParameter("@Type", Type),
               New SqlParameter("@MoveDate", MoveDate),
               New SqlParameter("@MoveTime", MoveTime),
               New SqlParameter("@FK_ReasonId", FK_ReasonId),
               New SqlParameter("@Remarks", Remarks),
               New SqlParameter("@Reader", Reader),
               New SqlParameter("@M_DATE_NUM", M_DATE_NUM),
               New SqlParameter("@M_TIME_NUM", M_TIME_NUM),
               New SqlParameter("@Status", Status),
               New SqlParameter("@IsManual", IsManual),
               New SqlParameter("@CREATED_BY", CREATED_BY),
               New SqlParameter("@AttachedFile", IIf(AttachedFile Is Nothing, DBNull.Value, AttachedFile)),
               New SqlParameter("@IsRemoteWork", IsRemoteWork),
               New SqlParameter("@IsFromMobile", IsFromMobile))

                If errNo = 0 Then
                    MoveRequestId = sqlOut.Value
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal MoveRequestId As Long, FK_EmployeeId As Long, ByVal Type As String, ByVal MoveDate As DateTime, ByVal MoveTime As DateTime, ByVal FK_ReasonId As Integer, ByVal Remarks As String, ByVal Reader As String, ByVal M_DATE_NUM As String, ByVal M_TIME_NUM As String, ByVal Status As String, ByVal IsManual As Boolean, ByVal LAST_UPDATE_BY As String, ByVal AttachedFile As String, ByVal IsRemoteWork As Boolean, ByVal IsFromMobile As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_MoveRequest_Update, New SqlParameter("@MoveRequestId", MoveRequestId),
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
               New SqlParameter("@Type", Type),
               New SqlParameter("@MoveDate", MoveDate),
               New SqlParameter("@MoveTime", MoveTime),
               New SqlParameter("@FK_ReasonId", FK_ReasonId),
               New SqlParameter("@Remarks", Remarks),
               New SqlParameter("@Reader", Reader),
               New SqlParameter("@M_DATE_NUM", M_DATE_NUM),
               New SqlParameter("@M_TIME_NUM", M_TIME_NUM),
               New SqlParameter("@Status", Status),
               New SqlParameter("@IsManual", IsManual),
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY),
               New SqlParameter("@AttachedFile", AttachedFile),
               New SqlParameter("@IsRemoteWork", IsRemoteWork),
               New SqlParameter("@IsFromMobile", IsFromMobile))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal MoveRequestId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_MoveRequest_Delete, New SqlParameter("@MoveRequestId", MoveRequestId))
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
                objRow = objDac.GetDataTable(Emp_MoveRequest_Select, New SqlParameter("@MoveRequestId", MoveRequestId)).Rows(0)
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
                objColl = objDac.GetDataTable(Emp_MoveRequest_Select_All, Nothing)
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
                objColl = objDac.GetDataTable(EMP_MOVE_Request_Select_Filter, New SqlParameter("@EmployeeId", EMP_NO),
               New SqlParameter("@FromDate", IIf(FromDate = DateTime.MinValue, DBNull.Value, FromDate)),
               New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, DBNull.Value, ToDate)),
               New SqlParameter("@Status", Status))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetFilter_Remote(ByVal EMP_NO As Integer, ByVal FromDate As DateTime?, ByVal ToDate As DateTime?, ByVal Status As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(EMP_MOVE_Request_Remote_Select_Filter, New SqlParameter("@EmployeeId", EMP_NO),
               New SqlParameter("@FromDate", IIf(FromDate = DateTime.MinValue, DBNull.Value, FromDate)),
               New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, DBNull.Value, ToDate)),
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
                objColl = objDac.GetDataTable(Emp_ManualEntryRequest_Select_ByDM, New SqlParameter("@FK_ManagerId", FK_ManagerId),
                                              New SqlParameter("@FK_StatusId", Status))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetByHR(ByVal FK_HREmployeeId As Integer, ByVal Status As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(ManualEntry_Request_Select_ByHR, New SqlParameter("@FK_EmployeeHRId", FK_HREmployeeId),
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
                errNo = objDac.AddUpdateDeleteSPTrans(ManualRequest_UpdateStatus, New SqlParameter("@MoveRequestId", MoveRequestId),
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
                objColl = objDac.GetDataTable(ManualEntry_Request_Select_ByGM, New SqlParameter("@FK_StatusId", FK_StatusId))
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
                objColl = objDac.GetDataTable(Emp_MoveRequest_PerDay, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
                                             New SqlParameter("@MoveDate", MoveDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetRequestsPerMonth(ByVal FK_EmployeeId As Integer, ByVal MoveDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_MoveRequest_PerMonth, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
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
                objColl = objDac.GetDataTable(ManualRequest_CheckHasInOrOut, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
                                                                             New SqlParameter("@MoveDate", MoveDate),
                                                                             New SqlParameter("@FK_ReasonId", FK_ReasonId),
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
                objColl = objDac.GetDataTable(Emp_MoveRequest_HasRequest, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
                                                                             New SqlParameter("@MoveDate", MoveDate),
                                                                             New SqlParameter("@FK_ReasonId", FK_ReasonId),
                                                                             New SqlParameter("@MoveTime", MoveTime))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetIn_OutCountPerDay(ByVal FK_EmployeeId As Integer, ByVal MoveDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_MoveRequest_NumberOf_In_Out, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
                                             New SqlParameter("@MoveDate", MoveDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetLastTransaction(ByVal EmployeeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objRow As DataTable = Nothing
            Try
                objRow = objDac.GetDataTable("Emp_MoveRequest_GetLastTransaction_Mobile", New SqlParameter("@EmployeeId", EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

#End Region


    End Class

End Namespace