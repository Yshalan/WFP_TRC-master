Imports Microsoft.VisualBasic

Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection

Imports TA.Lookup
Imports SmartV.DB
Imports SmartV.UTILITIES

Namespace TA.DashBoard

    Public Class DALDash_Board
        Inherits MGRBase

#Region "Class Variables"

        Private Rpt_Attend_DashB As String = "Rpt_Attend_DashB"
        Private Rpt_Absent_DashB As String = "Rpt_Absent_DashB"
        Private Rpt_Delay_DashB As String = "Rpt_Delay_DashB"
        Private Rpt_Earlyout_DashB As String = "Rpt_Earlyout_DashB"
        Private Rpt_Leave_DashB As String = "Rpt_Leave_DashB"
        Private Rpt_Summary_DashB As String = "Rpt_Summary_DashB"
        Private Rpt_DailyTranscations_DashB As String = "Rpt_DailyTranscations_DashB"
        Private Rpt_EmpSummary_DashB As String = "Rpt_EmpSummary_DashB"
        Private RetreivDrillDownData As String = "RetreivDrillDownData"
        Private Rpt_Feedback_DashB As String = "Rpt_Feedback_DashB"
        Private Rpt_GroupUsers As String = "Rpt_GroupUsers"
        Private Employee_Inner_AttendDashB As String = "Employee_Inner_AttendDashB"
        Private Employee_InnerPage_AttendPercent_DashB As String = "Employee_InnerPage_AttendPercent_DashB"
        Private Rpt_StatusCount_DashB As String = "Rpt_StatusCount_DashB"
        Private Rpt_RequestStatusCount_DashB As String = "Rpt_RequestStatusCount_DashB"
        Private Rpt_EmployeeWorkingHours_DashB As String = "Rpt_EmployeeWorkingHours_DashB"
        Private Rpt_Transaction_Stats_DashB As String = "Rpt_Transaction_Stats_DashB"
        Private Rpt_PermissionRequestCount_DashB As String = "Rpt_PermissionRequestCount_DashB"
        Private Rpt_LeaveRequestCount_DashB As String = "Rpt_LeaveRequestCount_DashB"
        Private Rpt_SummaryCount_DashB As String = "Rpt_SummaryCount_DashB"
        Private Rpt_SummaryViolationCount_DashB As String = "Rpt_SummaryViolationCount_DashB"

#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function GetAttendDash(ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal Lang As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Rpt_Attend_DashB, New SqlParameter("@CompanyId", FK_CompanyId), _
                                              New SqlParameter("@EntityId", FK_EntityId), _
                                              New SqlParameter("@FromDate", FromDate), _
                                              New SqlParameter("@ToDate", ToDate), _
                                              New SqlParameter("@Lang", Lang))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAbsentDash(ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal Lang As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Rpt_Absent_DashB, New SqlParameter("@CompanyId", FK_CompanyId), _
                                              New SqlParameter("@EntityId", FK_EntityId), _
                                              New SqlParameter("@FromDate", FromDate), _
                                              New SqlParameter("@ToDate", ToDate), _
                                              New SqlParameter("@Lang", Lang))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetDelayDash(ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal Lang As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Rpt_Delay_DashB, New SqlParameter("@CompanyId", FK_CompanyId), _
                                              New SqlParameter("@EntityId", FK_EntityId), _
                                              New SqlParameter("@FromDate", FromDate), _
                                              New SqlParameter("@ToDate", ToDate), _
                                              New SqlParameter("@Lang", Lang))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetEarlyoutDash(ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal Lang As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Rpt_Earlyout_DashB, New SqlParameter("@CompanyId", FK_CompanyId), _
                                              New SqlParameter("@EntityId", FK_EntityId), _
                                              New SqlParameter("@FromDate", FromDate), _
                                              New SqlParameter("@ToDate", ToDate), _
                                              New SqlParameter("@Lang", Lang))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetLeaveDash(ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal Lang As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Rpt_Leave_DashB, New SqlParameter("@CompanyId", FK_CompanyId), _
                                              New SqlParameter("@EntityId", FK_EntityId), _
                                              New SqlParameter("@FromDate", FromDate), _
                                              New SqlParameter("@ToDate", ToDate), _
                                              New SqlParameter("@Lang", Lang))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetSummaryDash(ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal Lang As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Rpt_Summary_DashB, New SqlParameter("@CompanyId", FK_CompanyId), _
                                              New SqlParameter("@EntityId", FK_EntityId), _
                                              New SqlParameter("@FromDate", FromDate), _
                                              New SqlParameter("@ToDate", ToDate), _
                                              New SqlParameter("@Lang", Lang))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetStatusCountDash(ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Rpt_StatusCount_DashB, New SqlParameter("@CompanyId", FK_CompanyId),
                                              New SqlParameter("@EntityId", FK_EntityId),
                                              New SqlParameter("@FromDate", FromDate),
                                              New SqlParameter("@ToDate", ToDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetRequestStatusCountDash(ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Rpt_RequestStatusCount_DashB, New SqlParameter("@CompanyId", FK_CompanyId),
                                              New SqlParameter("@EntityId", FK_EntityId),
                                              New SqlParameter("@FromDate", FromDate),
                                              New SqlParameter("@ToDate", ToDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetEmployeeWorkingHoursDash(ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer, ByVal FK_EmployeeId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Rpt_EmployeeWorkingHours_DashB, New SqlParameter("@CompanyId", FK_CompanyId),
                                              New SqlParameter("@EntityId", FK_EntityId),
                                              New SqlParameter("@EmployeeId", FK_EmployeeId),
                                              New SqlParameter("@FromDate", FromDate),
                                              New SqlParameter("@ToDate", ToDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetTransaction_StatsDash(ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Rpt_Transaction_Stats_DashB, New SqlParameter("@CompanyId", FK_CompanyId),
                                              New SqlParameter("@EntityId", FK_EntityId),
                                              New SqlParameter("@FromDate", FromDate),
                                              New SqlParameter("@ToDate", ToDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetDailyTransactionsDash(ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal Lang As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Rpt_DailyTranscations_DashB, New SqlParameter("@CompanyId", FK_CompanyId), _
                                              New SqlParameter("@EntityId", FK_EntityId), _
                                              New SqlParameter("@FromDate", FromDate), _
                                              New SqlParameter("@ToDate", ToDate), _
                                              New SqlParameter("@Lang", Lang))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetEmployeeSummaryDash(ByVal FK_EmployeeId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal Lang As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Rpt_EmpSummary_DashB, New SqlParameter("@EmployeeId", FK_EmployeeId), _
                                              New SqlParameter("@FromDate", FromDate), _
                                              New SqlParameter("@ToDate", ToDate), _
                                              New SqlParameter("@Lang", Lang))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetRetreivDrillDownData(ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal Lang As String, ByVal DrillDownType As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(RetreivDrillDownData, New SqlParameter("@CompanyId", FK_CompanyId), _
                                              New SqlParameter("@EntityId", FK_EntityId), _
                                              New SqlParameter("@FromDate", FromDate), _
                                              New SqlParameter("@ToDate", ToDate), _
                                              New SqlParameter("@Lang", Lang), _
                                              New SqlParameter("@DrillDownType", DrillDownType))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetFeedbackDash(ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal Lang As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Rpt_Feedback_DashB, New SqlParameter("@CompanyId", FK_CompanyId), _
                                              New SqlParameter("@EntityId", FK_EntityId), _
                                              New SqlParameter("@FromDate", FromDate), _
                                              New SqlParameter("@ToDate", ToDate), _
                                              New SqlParameter("@Lang", Lang))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetUserGroups(ByVal Lang As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Rpt_GroupUsers, New SqlParameter("@Lang", Lang))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function


        Public Function GetInnerEmployee_AttendDash(ByVal FK_CompanyId As Integer, ByVal Lang As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Employee_Inner_AttendDashB, New SqlParameter("@FK_CompanyId", FK_CompanyId), _
                                              New SqlParameter("@Lang", Lang))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetInnerPage_AttendPercent_DashB(ByVal UserId As Integer, ByVal TodayDate As DateTime, ByVal Lang As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Employee_InnerPage_AttendPercent_DashB, New SqlParameter("@Date", Todaydate), _
                                              New SqlParameter("@Lang", Lang), _
                                              New SqlParameter("@UserId", UserId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetPermissionRequestCountDash(ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Rpt_PermissionRequestCount_DashB, New SqlParameter("@CompanyId", FK_CompanyId),
                                              New SqlParameter("@EntityId", FK_EntityId),
                                              New SqlParameter("@FromDate", FromDate),
                                              New SqlParameter("@ToDate", ToDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetLeaveRequestCountDash(ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Rpt_LeaveRequestCount_DashB, New SqlParameter("@CompanyId", FK_CompanyId),
                                              New SqlParameter("@EntityId", FK_EntityId),
                                              New SqlParameter("@FromDate", FromDate),
                                              New SqlParameter("@ToDate", ToDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetSummaryCountDash(ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Rpt_SummaryCount_DashB, New SqlParameter("@CompanyId", FK_CompanyId),
                                              New SqlParameter("@EntityId", FK_EntityId),
                                              New SqlParameter("@FromDate", FromDate),
                                              New SqlParameter("@ToDate", ToDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetSummaryViolationCountDash(ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Rpt_SummaryViolationCount_DashB, New SqlParameter("@CompanyId", FK_CompanyId),
                                              New SqlParameter("@EntityId", FK_EntityId),
                                              New SqlParameter("@FromDate", FromDate),
                                              New SqlParameter("@ToDate", ToDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region

    End Class
End Namespace
