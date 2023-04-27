Imports Microsoft.VisualBasic

Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports TA.LookUp
Imports SmartV.DB
Imports SmartV.UTILITIES



Namespace TA.Admin

    Public Class DALOvertimeRules
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private OvertimeRules_Select As String = "OvertimeRules_select"
        Private OvertimeRules_Select_All As String = "OvertimeRules_select_All"
        Private OvertimeRules_Insert As String = "OvertimeRules_Insert"
        Private OvertimeRules_Update As String = "OvertimeRules_Update"
        Private OvertimeRules_Delete As String = "OvertimeRules_Delete"
        Private Select_OvertimeRules_HighTime_ByRuleId As String = "Select_OvertimeRules_HighTime_ByRuleId"
        Private Delete_OvertimeRules_HighTime_ByRuleId As String = "Delete_OvertimeRules_HighTime_ByRuleId"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef OvertimeRuleId As Integer, ByVal RuleName As String, ByVal RuleArabicName As String, ByVal OvertimeEligibility As Boolean, ByVal MinOvertime As Integer, ByVal ApprovalRequired As Boolean, ByVal OffDayIsHigh As Boolean, ByVal HolidayIsHigh As Boolean, ByVal HighHasTime As Boolean, ByVal IsCompensateLatetime As Boolean, ByVal IsLeaveBalance As Boolean, ByVal HighDayLeaveEquivalent As Double, ByVal LowDayLeaveEquivalent As Double, ByVal IsFinancial As Boolean, ByVal LowRate As Double, ByVal BeforeAfterSchedule As Integer, ByVal HighRate As Double, ByVal OverTimeApprovalBy As Integer, ByVal isLostFromHighOT As Boolean, ByVal MaxOvertime As Integer, ByVal FK_NormalTypeId As Integer, ByVal FK_OffDayTypeId As Integer, ByVal FK_HolidayTypeId As Integer, ByVal FK_ReligionHolidayTypeId As Integer, ByVal MinAutoApproveDuration As String) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@OvertimeRuleId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, OvertimeRuleId)

                errNo = objDac.AddUpdateDeleteSPTrans(OvertimeRules_Insert, New SqlParameter("@RuleName", RuleName),
               New SqlParameter("@RuleArabicName", RuleArabicName),
               New SqlParameter("@OvertimeEligibility", OvertimeEligibility),
               New SqlParameter("@MinOvertime", MinOvertime),
               New SqlParameter("@ApprovalRequired", ApprovalRequired),
               New SqlParameter("@OffDayIsHigh", OffDayIsHigh),
               New SqlParameter("@HolidayIsHigh", HolidayIsHigh),
               New SqlParameter("@HighHasTime", HighHasTime),
               New SqlParameter("@IsCompensateLatetime", IsCompensateLatetime),
               New SqlParameter("@IsLeaveBalance", IsLeaveBalance),
               New SqlParameter("@HighDayLeaveEquivalent", HighDayLeaveEquivalent),
               New SqlParameter("@LowDayLeaveEquivalent", LowDayLeaveEquivalent),
               New SqlParameter("@IsFinancial", IsFinancial),
               New SqlParameter("@LowRate", LowRate),
               New SqlParameter("@BeforeAfterSchedule", BeforeAfterSchedule),
               New SqlParameter("@OverTimeApprovalBy", OverTimeApprovalBy),
               New SqlParameter("@isLostFromHighOT", isLostFromHighOT),
               New SqlParameter("@HighRate", HighRate), sqlOut,
               New SqlParameter("@MaxOvertime", MaxOvertime),
               New SqlParameter("@FK_NormalTypeId", FK_NormalTypeId),
               New SqlParameter("@FK_OffDayTypeId", FK_OffDayTypeId),
               New SqlParameter("@FK_HolidayTypeId", FK_HolidayTypeId),
               New SqlParameter("@FK_ReligionHolidayTypeId", FK_ReligionHolidayTypeId),
               New SqlParameter("@MinAutoApproveDuration", MinAutoApproveDuration))

                If Not IsDBNull(sqlOut.Value) Then
                    OvertimeRuleId = sqlOut.Value
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal OvertimeRuleId As Integer, ByVal RuleName As String, ByVal RuleArabicName As String, ByVal OvertimeEligibility As Boolean, ByVal MinOvertime As Integer, ByVal ApprovalRequired As Boolean, ByVal OffDayIsHigh As Boolean, ByVal HolidayIsHigh As Boolean, ByVal HighHasTime As Boolean, ByVal IsCompensateLatetime As Boolean, ByVal IsLeaveBalance As Boolean, ByVal HighDayLeaveEquivalent As Double, ByVal LowDayLeaveEquivalent As Double, ByVal IsFinancial As Boolean, ByVal LowRate As Double, ByVal BeforeAfterSchedule As Integer, ByVal HighRate As Double, ByVal OverTimeApprovalBy As Integer, ByVal isLostFromHighOT As Boolean, ByVal MaxOvertime As Integer, ByVal FK_NormalTypeId As Integer, ByVal FK_OffDayTypeId As Integer, ByVal FK_HolidayTypeId As Integer, ByVal FK_ReligionHolidayTypeId As Integer, ByVal MinAutoApproveDuration As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(OvertimeRules_Update, New SqlParameter("@OvertimeRuleId", OvertimeRuleId),
               New SqlParameter("@RuleName", RuleName),
               New SqlParameter("@RuleArabicName", RuleArabicName),
               New SqlParameter("@OvertimeEligibility", OvertimeEligibility),
               New SqlParameter("@MinOvertime", MinOvertime),
               New SqlParameter("@ApprovalRequired", ApprovalRequired),
               New SqlParameter("@OffDayIsHigh", OffDayIsHigh),
               New SqlParameter("@HolidayIsHigh", HolidayIsHigh),
               New SqlParameter("@HighHasTime", HighHasTime),
               New SqlParameter("@IsCompensateLatetime", IsCompensateLatetime),
               New SqlParameter("@IsLeaveBalance", IsLeaveBalance),
               New SqlParameter("@HighDayLeaveEquivalent", HighDayLeaveEquivalent),
               New SqlParameter("@LowDayLeaveEquivalent", LowDayLeaveEquivalent),
               New SqlParameter("@IsFinancial", IsFinancial),
               New SqlParameter("@LowRate", LowRate),
               New SqlParameter("@BeforeAfterSchedule", BeforeAfterSchedule),
               New SqlParameter("@OverTimeApprovalBy", OverTimeApprovalBy),
               New SqlParameter("@isLostFromHighOT", isLostFromHighOT),
               New SqlParameter("@HighRate", HighRate),
               New SqlParameter("@MaxOvertime", MaxOvertime),
               New SqlParameter("@FK_NormalTypeId", FK_NormalTypeId),
               New SqlParameter("@FK_OffDayTypeId", FK_OffDayTypeId),
               New SqlParameter("@FK_HolidayTypeId", FK_HolidayTypeId),
               New SqlParameter("@FK_ReligionHolidayTypeId", FK_ReligionHolidayTypeId),
               New SqlParameter("@MinAutoApproveDuration", MinAutoApproveDuration))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal OvertimeRuleId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(OvertimeRules_Delete, New SqlParameter("@OvertimeRuleId", OvertimeRuleId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal OvertimeRuleId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(OvertimeRules_Select, New SqlParameter("@OvertimeRuleId", OvertimeRuleId)).Rows(0)
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
                objColl = objDac.GetDataTable(OvertimeRules_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function Add_OvertimeRules_HighTime(ByVal Dt As DataTable) As Integer
            Try
                Using objbC As New SqlBulkCopy(ConfigurationManager.ConnectionStrings("ConnStr").ConnectionString)
                    objbC.DestinationTableName = "OvertimeRules_HighTime"
                    objbC.BatchSize = Dt.Rows.Count
                    objbC.ColumnMappings.Add("FK_RuleId", "FK_RuleId")
                    objbC.ColumnMappings.Add("FromTime", "FromTime")
                    objbC.ColumnMappings.Add("ToTime", "ToTime")
                    objbC.ColumnMappings.Add("FK_OvertimeTypeId", "FK_OvertimeTypeId")
                    objbC.WriteToServer(Dt)
                    objbC.Close()
                End Using
            Catch ex As Exception
                Return -1
            End Try
            Return 0
        End Function
        Public Function GetAll_OvertimeRules_HighTime(ByVal OvertimeRuleId As Integer) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Select_OvertimeRules_HighTime_ByRuleId, New SqlParameter("@OvertimeRuleId", OvertimeRuleId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function
        Public Function Delete_OvertimeRules_HighTime(ByVal OvertimeRuleId As Integer) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Delete_OvertimeRules_HighTime_ByRuleId, New SqlParameter("@OvertimeRuleId", OvertimeRuleId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function
#End Region


    End Class
End Namespace