Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.DailyTasks

    Public Class DALRecalculateRequest
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private RecalculateRequest_Select As String = "RecalculateRequest_select"
        Private RecalculateRequest_Select_All As String = "RecalculateRequest_select_All"
        Private RecalculateRequest_Insert As String = "RecalculateRequest_Insert"
        Private RecalculateRequest_Update As String = "RecalculateRequest_Update"
        Private RecalculateRequest_Delete As String = "RecalculateRequest_Delete"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"


        Public Function AddApproveViolationRequest(ByVal Fk_CompanyId As Integer, ByVal Fk_EntityId As Integer, ByVal Fk_EmployeeId As Long, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal ImmediatelyStart As Boolean, ByVal RequestStartDateTime As DateTime, ByVal RecalStartDateTime As DateTime, ByVal RecalStatus As Integer, ByVal ReCalEndDateTime As DateTime, ByVal CREATED_BY As String, ByVal Remarks As String, ByVal FK_LogicalGroupId As Integer, ByVal FK_WorkLocation As Integer) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans("ApproveViolationRequest_Insert", New SqlParameter("@Fk_CompanyId", Fk_CompanyId), _
               New SqlParameter("@Fk_EntityId", Fk_EntityId), _
               New SqlParameter("@Fk_EmployeeId", Fk_EmployeeId), _
               New SqlParameter("@FromDate", IIf(FromDate = DateTime.MinValue, DBNull.Value, FromDate)), _
               New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, DBNull.Value, ToDate)), _
               New SqlParameter("@ImmediatelyStart", ImmediatelyStart), _
               New SqlParameter("@RequestStartDateTime", IIf(RequestStartDateTime = DateTime.MinValue, DBNull.Value, RequestStartDateTime)), _
               New SqlParameter("@RecalStartDateTime", IIf(RecalStartDateTime = DateTime.MinValue, DBNull.Value, RecalStartDateTime)), _
               New SqlParameter("@RecalStatus", RecalStatus), _
               New SqlParameter("@ReCalEndDateTime", IIf(ReCalEndDateTime = DateTime.MinValue, DBNull.Value, ReCalEndDateTime)), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@Remarks", Remarks), _
               New SqlParameter("@FK_LogicalGroupId", FK_LogicalGroupId), _
               New SqlParameter("@FK_WorkLocation", FK_WorkLocation))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Add(ByRef RequestId As Integer, ByVal Fk_CompanyId As Integer, ByVal Fk_EntityId As Integer, ByVal Fk_EmployeeId As Long, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal ImmediatelyStart As Boolean, ByVal RequestStartDateTime As DateTime, ByVal RecalStartDateTime As DateTime, ByVal RecalStatus As Integer, ByVal ReCalEndDateTime As DateTime, ByVal CREATED_BY As String, ByVal Remarks As String, ByVal FK_LogicalGroupId As Integer, ByVal FK_WorkLocation As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@RequestId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, RequestId)
                errNo = objDac.AddUpdateDeleteSPTrans(RecalculateRequest_Insert, sqlOut, New SqlParameter("@Fk_CompanyId", Fk_CompanyId),
               New SqlParameter("@Fk_EntityId", Fk_EntityId),
               New SqlParameter("@Fk_EmployeeId", Fk_EmployeeId),
               New SqlParameter("@FromDate", IIf(FromDate = DateTime.MinValue, DBNull.Value, FromDate)),
               New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, DBNull.Value, ToDate)),
               New SqlParameter("@ImmediatelyStart", ImmediatelyStart),
               New SqlParameter("@RequestStartDateTime", IIf(RequestStartDateTime = DateTime.MinValue, DBNull.Value, RequestStartDateTime)),
               New SqlParameter("@RecalStartDateTime", IIf(RecalStartDateTime = DateTime.MinValue, DBNull.Value, RecalStartDateTime)),
               New SqlParameter("@RecalStatus", RecalStatus),
               New SqlParameter("@ReCalEndDateTime", IIf(ReCalEndDateTime = DateTime.MinValue, DBNull.Value, ReCalEndDateTime)),
               New SqlParameter("@CREATED_BY", CREATED_BY),
               New SqlParameter("@Remarks", Remarks),
               New SqlParameter("@FK_LogicalGroupId", FK_LogicalGroupId),
               New SqlParameter("@FK_WorkLocation", FK_WorkLocation))
                If errNo = 0 Then RequestId = sqlOut.Value Else RequestId = 0
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal RequestId As Integer, ByVal Fk_CompanyId As Integer, ByVal Fk_EntityId As Integer, ByVal Fk_EmployeeId As Long, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal ImmediatelyStart As Boolean, ByVal RequestStartDateTime As DateTime, ByVal RecalStartDateTime As DateTime, ByVal RecalStatus As Integer, ByVal ReCalEndDateTime As DateTime, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal Remarks As String, ByVal FK_LogicalGroupId As Integer, ByVal FK_WorkLocation As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(RecalculateRequest_Update, New SqlParameter("@RequestId", RequestId), _
               New SqlParameter("@Fk_CompanyId", Fk_CompanyId), _
               New SqlParameter("@Fk_EntityId", Fk_EntityId), _
               New SqlParameter("@Fk_EmployeeId", Fk_EmployeeId), _
               New SqlParameter("@FromDate", FromDate), _
               New SqlParameter("@ToDate", ToDate), _
               New SqlParameter("@ImmediatelyStart", ImmediatelyStart), _
               New SqlParameter("@RequestStartDateTime", RequestStartDateTime), _
               New SqlParameter("@RecalStartDateTime", RecalStartDateTime), _
               New SqlParameter("@RecalStatus", RecalStatus), _
               New SqlParameter("@ReCalEndDateTime", ReCalEndDateTime), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@CREATED_DATE", CREATED_DATE), _
               New SqlParameter("@Remarks", Remarks), _
               New SqlParameter("@FK_LogicalGroupId", FK_LogicalGroupId), _
               New SqlParameter("@FK_WorkLocation", FK_WorkLocation))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal RequestId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(RecalculateRequest_Delete, New SqlParameter("@RequestId", RequestId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function DeleteApproveViolationRequest(ByVal RequestId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans("ApproveViolationRequest_Delete", New SqlParameter("@RequestId", RequestId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function


        Public Function GetByPK(ByVal RequestId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(RecalculateRequest_Select, New SqlParameter("@RequestId", RequestId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetAll_ApproveViolationRequest(Optional fromDate As DateTime = Nothing, Optional toDate As DateTime = Nothing) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("ApproveViolationRequest_Select_All", _
                          New SqlParameter("@Fromdate", IIf(fromDate = Nothing, Nothing, fromDate)), _
                          New SqlParameter("@Todate", IIf(toDate = Nothing, Nothing, toDate)))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function


        Public Function GetAll(Optional fromDate As DateTime = Nothing, Optional toDate As DateTime = Nothing) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RecalculateRequest_Select_All, _
                          New SqlParameter("@Fromdate", IIf(fromDate = Nothing, Nothing, fromDate)), _
                          New SqlParameter("@Todate", IIf(toDate = Nothing, Nothing, toDate)))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace