Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.DailyTasks

    Public Class DALTAPolicy
        Inherits MGRBase

#Region "Class Variables"
        Private strConn As String
        Private TAPolicy_Select As String = "TAPolicy_select"
        Private TAPolicy_Select_All As String = "TAPolicy_select_All"
        Private TAPolicy_Insert As String = "TAPolicy_Insert"
        Private TAPolicy_Update As String = "TAPolicy_Update"
        Private TAPolicy_Delete As String = "TAPolicy_Delete"
        Public intTAPolicyId As Integer
#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal TAPolicyName As String, ByVal TAPolicyArabicName As String, ByVal GraceInMins As Integer, ByVal GraceOutMins As Integer, ByVal DelayIsFromGrace As Boolean, ByVal EarlyOutIsFromGrace As Boolean, ByVal Active As Boolean, ByVal CREATED_BY As String, ByVal HasLaunchBreak As Boolean, LaunchBreakDuration As Integer, ByVal CompensateLaunchbreak As Boolean, ByVal FK_launchbreakReason As Integer, ByVal HasPrayTime As Boolean, ByVal PrayTimeDuration As Integer, ByVal CompensatePrayTime As Boolean, ByVal FK_PrayTimeReason As Integer, ByVal HasReasonBreakTime As Boolean, ByVal LaunchBreakFromTime As Integer, ByVal LaunchBreakToTime As Integer, ByVal ConsiderFirstIn_LastOutOnly As Boolean, ByVal MinDurationAsViolation As String, ByVal MonthlyAllowedGraceOutTime As Integer, ByVal IgnoreEarlyOut_WithinGrace As Boolean, ByVal ConsiderAbsentIfNotCompleteNoOfHours As Boolean, ByVal NoOfNotCompleteHours As Integer, ByVal ConsiderAbsentIfStudyNurs_NotCompleteHrs As Boolean, ByVal NoOfNotCompleteHours_StudyNurs As Integer, ByVal NoOfAllowedPrays As Integer, ByVal PrayBreakFromTime As Integer, ByVal PrayBreakToTime As Integer) As Integer
            Dim sp1 As New SqlParameter("@TAPolicyId", SqlDbType.Int, 8, ParameterDirection.InputOutput, False, 0, 0, "", DataRowVersion.Default, 0)
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(TAPolicy_Insert, New SqlParameter("@TAPolicyName", TAPolicyName), _
           New SqlParameter("@TAPolicyArabicName", TAPolicyArabicName), _
           New SqlParameter("@GraceInMins", GraceInMins), _
           New SqlParameter("@GraceOutMins", GraceOutMins), _
           New SqlParameter("@DelayIsFromGrace", DelayIsFromGrace), _
           New SqlParameter("@EarlyOutIsFromGrace", EarlyOutIsFromGrace), _
           New SqlParameter("@Active", Active), _
            sp1, _
           New SqlParameter("@CREATED_BY", CREATED_BY), _
           New SqlParameter("@HasLaunchBreak", HasLaunchBreak), _
           New SqlParameter("@LaunchDuration", LaunchBreakDuration), _
           New SqlParameter("@CompensateLaunchbreak", CompensateLaunchbreak), _
           New SqlParameter("@FK_launchbreakReason", FK_launchbreakReason), _
           New SqlParameter("@HasPrayTime", HasPrayTime), _
           New SqlParameter("@PrayTimeDuration", PrayTimeDuration), _
           New SqlParameter("@CompensatePrayTime", CompensatePrayTime), _
           New SqlParameter("@FK_PrayTimeReason", FK_PrayTimeReason), _
           New SqlParameter("@HasReasonBreakTime", HasReasonBreakTime), _
           New SqlParameter("@LaunchBreakFromTime", LaunchBreakFromTime), _
           New SqlParameter("@LaunchBreakToTime", LaunchBreakToTime), _
           New SqlParameter("@ConsiderFirstIn_LastOutOnly", ConsiderFirstIn_LastOutOnly), _
           New SqlParameter("@MinDurationAsViolation", MinDurationAsViolation), _
           New SqlParameter("@MonthlyAllowedGraceOutTime", MonthlyAllowedGraceOutTime), _
           New SqlParameter("@IgnoreEarlyOut_WithinGrace", IgnoreEarlyOut_WithinGrace), _
           New SqlParameter("@ConsiderAbsentIfNotCompleteNoOfHours", ConsiderAbsentIfNotCompleteNoOfHours), _
           New SqlParameter("@NoOfNotCompleteHours", NoOfNotCompleteHours), _
           New SqlParameter("@ConsiderAbsentIfStudyNurs_NotCompleteHrs", ConsiderAbsentIfStudyNurs_NotCompleteHrs), _
           New SqlParameter("@NoOfNotCompleteHours_StudyNurs", NoOfNotCompleteHours_StudyNurs), _
           New SqlParameter("@NoOfAllowedPrays", NoOfAllowedPrays), _
           New SqlParameter("@PrayBreakFromTime", PrayBreakFromTime), _
           New SqlParameter("@PrayBreakToTime", PrayBreakToTime))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            intTAPolicyId = sp1.Value
            Return errNo

        End Function

        Public Function Update(ByVal TAPolicyId As Integer, ByVal TAPolicyName As String, ByVal TAPolicyArabicName As String, ByVal GraceInMins As Integer, ByVal GraceOutMins As Integer, ByVal DelayIsFromGrace As Boolean, ByVal EarlyOutIsFromGrace As Boolean, ByVal Active As Boolean, ByVal LAST_UPDATE_BY As String, ByVal HasLaunchBreak As Boolean, LaunchBreakDuration As Integer, ByVal CompensateLaunchbreak As Boolean, ByVal FK_launchbreakReason As Integer, ByVal HasPrayTime As Boolean, ByVal PrayTimeDuration As Integer, ByVal CompensatePrayTime As Boolean, ByVal FK_PrayTimeReason As Integer, ByVal HasReasonBreakTime As Boolean, ByVal LaunchBreakFromTime As Integer, ByVal LaunchBreakToTime As Integer, ByVal ConsiderFirstIn_LastOutOnly As Boolean, ByVal MinDurationAsViolation As String, ByVal MonthlyAllowedGraceOutTime As Integer, ByVal IgnoreEarlyOut_WithinGrace As Boolean, ByVal ConsiderAbsentIfNotCompleteNoOfHours As Boolean, ByVal NoOfNotCompleteHours As Integer, ByVal ConsiderAbsentIfStudyNurs_NotCompleteHrs As Boolean, ByVal NoOfNotCompleteHours_StudyNurs As Integer, ByVal NoOfAllowedPrays As Integer, ByVal PrayBreakFromTime As Integer, ByVal PrayBreakToTime As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(TAPolicy_Update, New SqlParameter("@TAPolicyId", TAPolicyId), _
                        New SqlParameter("@TAPolicyName", TAPolicyName), _
                        New SqlParameter("@TAPolicyArabicName", TAPolicyArabicName), _
                        New SqlParameter("@GraceInMins", GraceInMins), _
                        New SqlParameter("@GraceOutMins", GraceOutMins), _
                        New SqlParameter("@DelayIsFromGrace", DelayIsFromGrace), _
                        New SqlParameter("@EarlyOutIsFromGrace", EarlyOutIsFromGrace), _
                        New SqlParameter("@Active", Active), _
                        New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
                        New SqlParameter("@HasLaunchBreak", HasLaunchBreak), _
                        New SqlParameter("@LaunchDuration", LaunchBreakDuration), _
                        New SqlParameter("@CompensateLaunchbreak", CompensateLaunchbreak), _
                        New SqlParameter("@FK_launchbreakReason", FK_launchbreakReason), _
                        New SqlParameter("@HasPrayTime", HasPrayTime), _
                        New SqlParameter("@PrayTimeDuration", PrayTimeDuration), _
                        New SqlParameter("@CompensatePrayTime", CompensatePrayTime), _
                        New SqlParameter("@FK_PrayTimeReason", FK_PrayTimeReason), _
                        New SqlParameter("@HasReasonBreakTime", HasReasonBreakTime), _
                        New SqlParameter("@LaunchBreakFromTime", LaunchBreakFromTime), _
                        New SqlParameter("@LaunchBreakToTime", LaunchBreakToTime), _
                        New SqlParameter("@ConsiderFirstIn_LastOutOnly", ConsiderFirstIn_LastOutOnly), _
                        New SqlParameter("@MinDurationAsViolation", MinDurationAsViolation), _
                        New SqlParameter("@MonthlyAllowedGraceOutTime", MonthlyAllowedGraceOutTime), _
                        New SqlParameter("@IgnoreEarlyOut_WithinGrace", IgnoreEarlyOut_WithinGrace), _
                        New SqlParameter("@ConsiderAbsentIfNotCompleteNoOfHours", ConsiderAbsentIfNotCompleteNoOfHours), _
                        New SqlParameter("@NoOfNotCompleteHours", NoOfNotCompleteHours), _
                        New SqlParameter("@ConsiderAbsentIfStudyNurs_NotCompleteHrs", ConsiderAbsentIfStudyNurs_NotCompleteHrs), _
                        New SqlParameter("@NoOfNotCompleteHours_StudyNurs", NoOfNotCompleteHours_StudyNurs), _
                        New SqlParameter("@NoOfAllowedPrays", NoOfAllowedPrays), _
                        New SqlParameter("@PrayBreakFromTime", PrayBreakFromTime), _
                        New SqlParameter("@PrayBreakToTime", PrayBreakToTime))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal TAPolicyId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(TAPolicy_Delete, New SqlParameter("@TAPolicyId", TAPolicyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal TAPolicyId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(TAPolicy_Select, New SqlParameter("@TAPolicyId", TAPolicyId)).Rows(0)
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
                objColl = objDac.GetDataTable(TAPolicy_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region

    End Class
End Namespace