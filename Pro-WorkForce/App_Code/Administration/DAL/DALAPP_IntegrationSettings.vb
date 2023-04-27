Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports SmartV.DB
Imports TA.Lookup


Namespace TA.Admin

    Public Class DALAPP_IntegrationSettings
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private APP_IntegrationSettings_Select As String = "APP_IntegrationSettings_select"
        Private APP_IntegrationSettings_Select_All As String = "APP_IntegrationSettings_select_All"
        Private APP_IntegrationSettings_Insert As String = "APP_IntegrationSettings_Insert"
        Private APP_IntegrationSettings_Update As String = "APP_IntegrationSettings_Update"
        Private APP_IntegrationSettings_Delete As String = "APP_IntegrationSettings_Delete"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal CompanyName As String, ByVal HasEmployee As Boolean, ByVal HasEmployeeLeave As Boolean, ByVal HasLeaveAudit As Boolean, ByVal HasEmployeeSupervisor As Boolean, ByVal HasGrade As Boolean, ByVal HasDesignation As Boolean, ByVal HasNationality As Boolean, ByVal HasWorkLocation As Boolean, ByVal HasOrganization As Boolean, ByVal HasLeaveTypes As Boolean, ByVal HasEmployeeDelegate As Boolean, ByVal HasHoliday As Boolean, ByVal HasStudyLeave As Boolean, ByVal HasApproveErpViolation As Boolean, ByVal ReprocessFirstSchedule As Integer, ByVal ReprocessSecondSchedule As Integer, ByVal ReprocessThirdSchedule As Integer, ByVal EmailErrorReceiver As String, ByVal EmailPortNumber As Integer, ByVal EmailEnableSsl As Boolean, ByVal ServiceURL As String, ByVal ServiceUserName As String, ByVal ServicePassword As String, ByVal EntityName As String, ByVal IntegrationType As String, ByVal IsPendingLeave As Boolean, ByVal IsApproveLeave As Boolean, ByVal IsTrainingLeave As Boolean, ByVal IsExtraInfoLeave As Boolean, ByVal IsDutyLeave As Boolean, ByVal RunTimesByMinutes As Integer _
                            , ByVal RunTimesByHours As Integer, ByVal Runat As Integer, ByVal Runat2 As Integer, ByVal EntityCode As Integer, ByVal RecordPerPage As Integer, ByVal IsProduction As Integer) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(APP_IntegrationSettings_Insert, New SqlParameter("@HasEmployee", HasEmployee), _
                New SqlParameter("@HasEmployeeLeave", HasEmployeeLeave), _
                New SqlParameter("@HasLeaveAudit", HasLeaveAudit), _
                New SqlParameter("@HasEmployeeSupervisor", HasEmployeeSupervisor), _
                New SqlParameter("@HasGrade", HasGrade), _
                New SqlParameter("@HasDesignation", HasDesignation), _
                New SqlParameter("@HasNationality", HasNationality), _
                New SqlParameter("@HasWorkLocation", HasWorkLocation), _
                New SqlParameter("@HasOrganization", HasOrganization), _
                New SqlParameter("@HasLeaveTypes", HasLeaveTypes), _
                New SqlParameter("@HasEmployeeDelegate", HasEmployeeDelegate), _
                New SqlParameter("@HasHoliday", HasHoliday), _
                New SqlParameter("@HasStudyLeave", HasStudyLeave), _
                New SqlParameter("@HasApproveErpViolation", HasApproveErpViolation), _
                New SqlParameter("@ReprocessFirstSchedule", ReprocessFirstSchedule), _
                New SqlParameter("@ReprocessSecondSchedule", ReprocessSecondSchedule), _
                New SqlParameter("@ReprocessThirdSchedule", ReprocessThirdSchedule), _
                New SqlParameter("@EmailErrorReceiver", EmailErrorReceiver), _
                New SqlParameter("@EmailPortNumber", EmailPortNumber), _
                New SqlParameter("@EmailEnableSsl", EmailEnableSsl), _
                New SqlParameter("@ServiceURL", ServiceURL), _
                New SqlParameter("@ServiceUserName", ServiceUserName), _
                New SqlParameter("@ServicePassword", ServicePassword), _
                New SqlParameter("@EntityName", EntityName), _
                New SqlParameter("@IntegrationType", IntegrationType), _
                New SqlParameter("@IsPendingLeave", IsPendingLeave), _
                New SqlParameter("@IsApproveLeave", IsApproveLeave), _
                New SqlParameter("@IsTrainingLeave", IsTrainingLeave), _
                New SqlParameter("@IsExtraInfoLeave", IsExtraInfoLeave), _
                New SqlParameter("@IsDutyLeave", IsDutyLeave), _
                New SqlParameter("@RunTimesByMinutes", RunTimesByMinutes), _
                New SqlParameter("@RunTimesByHours", RunTimesByHours), _
                New SqlParameter("@Runat", Runat), _
                New SqlParameter("@Runat2", Runat2), _
                New SqlParameter("@EntityCode", EntityCode), _
                New SqlParameter("@RecordPerPage", RecordPerPage), _
                New SqlParameter("@IsProduction", IsProduction))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Public Function GetAll() As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(APP_IntegrationSettings_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

#End Region


    End Class
End Namespace