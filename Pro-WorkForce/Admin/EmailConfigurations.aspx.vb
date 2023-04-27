Imports TA.Employees
Imports System.Data
Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports Telerik.Web.UI.Upload
Imports Telerik.Web.UI.UploadedFile
Imports System.IO
Imports TA.Admin
Imports TA.Definitions
Imports System.DirectoryServices
Imports TA.Security
Imports System.Web.Configuration

Partial Class Admin_EmailConfigurations
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Dim objAPP_Settings As New APP_Settings
    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Dim objWeekDays As New WeekDays
    Dim objUsers As SYSUsers
    Private objWorkSchedule As New WorkSchedule
    Dim objAppEmailConfiguration As App_EmailConfigurations
    Dim objLeaveTypes As LeavesTypes
    Private objOrgLevel As OrgLevel
    Dim objPermissionType As PermissionsTypes

#End Region

#Region "Properties"

    Public Property appl_setup_id() As Integer
        Get
            Return ViewState("appl_setup_id")
        End Get
        Set(ByVal value As Integer)
            ViewState("appl_setup_id") = value
        End Set
    End Property

    Public Property imageData() As Byte()
        Get
            Return ViewState("imageData")
        End Get
        Set(ByVal value As Byte())
            ViewState("imageData") = value
        End Set
    End Property

    Public Property imagePath() As String
        Get
            Return ViewState("imagePath")
        End Get
        Set(ByVal value As String)
            ViewState("imagePath") = value
        End Set
    End Property

    Public Property WorkSchedualID() As Integer
        Get
            Return ViewState("SchedualID")
        End Get
        Set(ByVal value As Integer)
            ViewState("SchedualID") = value
        End Set
    End Property

    Public Property dtCurrent() As DataTable
        Get
            Return ViewState("dtCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
        End Set
    End Property

    Public Property SMTPServer() As String
        Get
            Return ViewState("SMTPServer")
        End Get
        Set(ByVal value As String)
            ViewState("SMTPServer") = value
        End Set
    End Property

    Public Property CompanyName1() As String
        Get
            Return ViewState("CompanyName1")
        End Get
        Set(ByVal value As String)
            ViewState("CompanyName1") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        Else
            If SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                Page.MasterPageFile = "~/default/ArabicMaster.master"
            Else
                Lang = CtlCommon.Lang.EN
                Page.MasterPageFile = "~/default/NewMaster.master"
            End If

            Page.UICulture = SessionVariables.CultureInfo
        End If


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")

            ElseIf SessionVariables.CultureInfo = "ar-JO" Then

                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            PageHeader1.HeaderText = ResourceManager.GetString("EmailConfigurations", CultureInfo)

            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)

            fillcontrols()
            FillEmailConfiguration()


        End If

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not UpdatePanel1.FindControl(row("AddBtnName")) Is Nothing Then
                        UpdatePanel1.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not UpdatePanel1.FindControl(row("DeleteBtnName")) Is Nothing Then
                        UpdatePanel1.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not UpdatePanel1.FindControl(row("EditBtnName")) Is Nothing Then
                        UpdatePanel1.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not UpdatePanel1.FindControl(row("PrintBtnName")) Is Nothing Then
                        UpdatePanel1.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim Ext As String = ""
        objAPP_Settings = New APP_Settings
        With objAPP_Settings
            'Send To Manager

            .CompanyName1 = CompanyName1
            For Each list As ListItem In chklSendManagerNotification.Items
                'If list.Value = 1 AndAlso list.Selected = True Then
                '    .SendToManagerDaily = True
                'End If

                If list.Value = 2 AndAlso list.Selected = True Then
                    .SendToManagerWeekly = True
                End If

                If list.Value = 3 AndAlso list.Selected = True Then
                    .SendToManagerMonthly = True
                End If
            Next

            'Send To HR
            For Each list As ListItem In chklSendHRNotification.Items
                If list.Value = 1 AndAlso list.Selected = True Then
                    .SendToHRDaily = True
                End If

                If list.Value = 2 AndAlso list.Selected = True Then
                    .SendToHRWeekly = True
                End If

                If list.Value = 3 AndAlso list.Selected = True Then
                    .SendToHRMonthly = True
                End If
            Next

            'Send To Org Entity Manager
            For Each list As ListItem In chklSendEntityMgrNotification.Items

                If list.Value = 2 AndAlso list.Selected = True Then
                    .SendToEntityManagerWeekly = True
                End If

                If list.Value = 3 AndAlso list.Selected = True Then
                    .SendToEntityManagerMonthly = True
                End If
            Next

            'Send To Employee
            For Each list As ListItem In chklSendEmployeeNotification.Items

                If list.Value = 2 AndAlso list.Selected = True Then
                    .SendToEmployeeWeekly = True
                End If

                If list.Value = 3 AndAlso list.Selected = True Then
                    .SendToEmployeeMonthly = True
                End If
            Next

            '.ManageOvertime = rdbManageOvertime.SelectedValue
            '.MinGapbetweenMoves = 0
            'Hide Manage Default Leave By and radio buttons related'
            'If rdbtnNone.Checked Then
            '    .AnnualLeaveOption = 1
            'End If
            'If rdbtnGradeAl.Checked Then
            '    .AnnualLeaveOption = 2
            'End If
            'If rdbtnDesignationAl.Checked Then
            '    .AnnualLeaveOption = 3
            'End If
            'Hide Manage Default Leave By and radio buttons related'
            .HRGroupEmail = txtHREmail.Text
            .HREmailNotification = rdlEmailNotifications.SelectedValue


            .ManagerEmailFormat = cblMgrEmailFormat.SelectedValue

            .HREmailFormat = cblHREmailFormat.SelectedValue


            .HasEmailApproval = chkHasEmailApproval.Checked



            'cblEployeeRequests 
            ''
            Dim strEarlyOutNotifications As String = (CInt(rmtEarlyOutNotifications.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtEarlyOutNotifications.TextWithLiterals.Split(":")(1))
            .EarlyOutNotificationAfter = strEarlyOutNotifications

            If radtxtPermissionReminder.Text = Nothing Then
                .PermissionRequestReminderAfter = Nothing
            Else
                .PermissionRequestReminderAfter = radtxtPermissionReminder.Text
            End If

            If radReminderAbsentAfter.Text = Nothing Then
                .ReminderAbsentAfter = Nothing
            Else
                .ReminderAbsentAfter = radReminderAbsentAfter.Text
            End If

            .MgrAbsentWeekly = rblMgrAbsentWeekly.SelectedValue
            .MgrAbsentMonthly = rblMgrAbsentMonthly.SelectedValue
            .MgrViolationWeekly = rblMgrViolationWeekly.SelectedValue
            .MgrViolationMonthly = rblMgrViolationMonthly.SelectedValue
            .MgrSummaryMonthly = rblMgrSummaryMonthly.SelectedValue
            .MgrEmpSummaryAttendanceWeekly = rblMgrEmpSummaryAttendanceWeekly.SelectedValue

            .MgrDailyWeekly = rblMgrDailyWeekly.SelectedValue
            .MgrDailyMonthly = rblMgrDailyMonthly.SelectedValue
            .MgrDetailedWeekly = rblMgrDetailedWeekly.SelectedValue
            .MgrDetailedMonthly = rblMgrDetailedMonthly.SelectedValue
            .MgrSummaryWeekly = rblMgrSummaryWeekly.SelectedValue
            .MgrEmpSummaryAttendanceMonthly = rblMgrEmpSummaryAttendanceMonthly.SelectedValue

            .HRAbsentWeekly = rblHRAbsentWeekly.SelectedValue
            .HRAbsentMonthly = rblHRAbsentMonthly.SelectedValue
            .HRViolationWeekly = rblHRViolationWeekly.SelectedValue
            .HRViolationMonthly = rblHRViolationMonthly.SelectedValue

            .HRDailyWeekly = rblHRDailyWeekly.SelectedValue
            .HRDailyMonthly = rblHRDailyMonthly.SelectedValue
            .HRDetailedWeekly = rblHRDetailedWeekly.SelectedValue
            .HRDetailedMonthly = rblHRDetailedMonthly.SelectedValue
            .HRSummaryWeekly = rblHRSummaryWeekly.SelectedValue
            .HRSummaryMonthly = rblHRSummaryMonthly.SelectedValue

            .EntityManagerViolationWeekly = rblEntityMgrViolationWeekly.SelectedValue
            .EntityManagerViolationMonthly = rblEntityMgrViolationMonthly.SelectedValue

            .EmployeeViolationWeekly = rblEmployeeViolationWeekly.SelectedValue
            .EmployeeViolationMonthly = rblEmployeeViolationMonthly.SelectedValue

            .EmployeeAbsentWeekly = rblEmployeeAbsentWeekly.SelectedValue
            .EmployeeAbsentMonthly = rblEmployeeAbsentMonthly.SelectedValue

            .EmployeeDetailedAbsentWeekly = rblEmployeeDetailedAbsentWeekly.SelectedValue
            .EmployeeDetailedAbsentMonthly = rblEmployeeDetailedAbsentMonthly.SelectedValue

            .EmployeeLostTimeDetailsWeekly = rblEmployeeLostTimeDetailsWeekly.SelectedValue
            .EmployeeLostTimeDetailsMonthly = rblEmployeeLostTimeDetailsMonthly.SelectedValue

            .EmployeeSummaryWeekly = rblEmployeeSummaryWeekly.SelectedValue
            .EmployeeSummaryMonthly = rblEmployeeSummaryMonthly.SelectedValue

            .MgrEmpDeductionWeekly = rblMgrEmpDeductionWeekly.SelectedValue
            .MgrEmpDeductionMonthly = rblMgrEmpDeductionMonthly.SelectedValue

            .HrEmpDeductionWeekly = rblHrEmpDeductionWeekly.SelectedValue
            .HrEmpDeductionMonthly = rblHrEmpDeductionMonthly.SelectedValue

            .EmployeeEmpDeductionWeekly = rblEmployeeEmpDeductionWeekly.SelectedValue
            .EmployeeEmpDeductionMonthly = rblEmployeeEmpDeductionMonthly.SelectedValue

            .EntityManagerEmpDeductionWeekly = rblEntityManagerEmpDeductionWeekly.SelectedValue
            .EntityManagerEmpDeductionMonthly = rblEntityManagerEmpDeductionMonthly.SelectedValue

            .EntityManagerMaxAbsentWeekly = rblEntityManagerMaxAbsentWeekly.SelectedValue
            .EntityManagerMaxAbsentMonthly = rblEntityManagerMaxAbsentMonthly.SelectedValue

            .EntityManagerMaxDelayWeekly = rblEntityManagerMaxDelayWeekly.SelectedValue
            .EntityManagerMaxDelayMonthly = rblEntityManagerMaxDelayMonthly.SelectedValue

            .MgrDeductionPerPolicyWeekly = rblMgrDeductionPerPolicyWeekly.SelectedValue
            .MgrDeductionPerPolicyMonthly = rblMgrDeductionPerPolicyMonthly.SelectedValue

            .HRDeductionPerPolicyWeekly = rblHRDeductionPerPolicyWeekly.SelectedValue
            .HRDeductionPerPolicyMonthly = rblHRDeductionPerPolicyMonthly.SelectedValue

            .EmployeeDeductionPerPolicyWeekly = rblEmployeeDeductionPerPolicyWeekly.SelectedValue
            .EmployeeDeductionPerPolicyMonthly = rblEmployeeDeductionPerPolicyMonthly.SelectedValue

            .EntityManagerDeductionPerPolicyWeekly = rblEntityManagerDeductionPerPolicyWeekly.SelectedValue
            .EntityManagerDeductionPerPolicyMonthly = rblEntityManagerDeductionPerPolicyMonthly.SelectedValue

            .SendDaily_AbsentDelay_EntityMgr = chkSendDaily_AbsentDelay_EntityMgr.Checked

            .DeductionPerPolicy_Value = txtDeductionPerPolicy_Value.Text

            If Not radnumIncompleteWorkingHrs.Text = Nothing Then
                .IncompleteWorkingHrs = radnumIncompleteWorkingHrs.Text
            End If
            .YearlyReportFromYearBegining = chkYearlyReportFromYearBegining.Checked
            If rblIncludeNotifications_CoordinatorTypes.SelectedValue = 1 Then
                .IncludeNotifications_CoordinatorTypes = True
            Else
                .IncludeNotifications_CoordinatorTypes = False
            End If

            .DeductionEmailFormat = rblDeductionEmailFormat.SelectedValue

            Dim errno As Integer = -1
            errno = ._EmailConfigration_Add()

            'Save Users Settings
            'If rblSystemUsers.SelectedValue = "1" Then 'Domain
            ' 
            'ElseIf rblSystemUsers.SelectedValue = "2" Then 'System
            '  
            'End If

            '

            objAppEmailConfiguration = New App_EmailConfigurations()
            With objAppEmailConfiguration
                .EmailFrom = txtEmailFrom.Text
                .SMTP_Server = txtSmtpServer.Text
                .EnableEmailService = chkEnableEmailService.Checked
                .EnableSMSService = chkEnableSMSService.Checked
                .SMTPUserName = txtSMTPUserName.Text
                .SMTPPassword = txtSMTPPassword.Text

                If String.IsNullOrEmpty(SMTPServer) Then
                    errno = .Add()
                Else
                    errno = .Update()
                End If
            End With

            If errno = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ModifySuccessfully", CultureInfo), "success")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorModfiy", CultureInfo), "error")
            End If

        End With
        'Response.Redirect("../Admin/App_settings.aspx")
    End Sub

    Protected Sub chkEnableEmailService_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkEnableEmailService.CheckedChanged
        If chkEnableEmailService.Checked Then
            trEmailConfig.Visible = True
        Else
            trEmailConfig.Visible = False
        End If
    End Sub

    Protected Sub chklSendManagerNotification_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chklSendManagerNotification.SelectedIndexChanged
        If chklSendManagerNotification.Items.FindByValue("2").Selected = True And chklSendManagerNotification.Items.FindByValue("3").Selected = True Then
            trMgrEmailFormat.Visible = True
            trMgrAbsent.Visible = True
            trMgrViolation.Visible = True

            trMgrDaily.Visible = True
            trMgrDetailed.Visible = True
            trMgrSummary.Visible = True
            dvMgrEmpSummaryAttendance.Visible = True
            dvMgrEmpDeduction.Visible = True
            dvMgrDeductionPerPolicy.Visible = True

            rblMgrAbsentWeekly.Visible = True
            rblMgrViolationWeekly.Visible = True
            rblMgrAbsentMonthly.Visible = True
            rblMgrViolationMonthly.Visible = True

            rblMgrDailyWeekly.Visible = True
            rblMgrDailyMonthly.Visible = True
            rblMgrDetailedWeekly.Visible = True
            rblMgrDetailedMonthly.Visible = True
            rblMgrSummaryWeekly.Visible = True
            rblMgrSummaryMonthly.Visible = True
            rblMgrEmpSummaryAttendanceWeekly.Visible = True
            rblMgrEmpSummaryAttendanceMonthly.Visible = True
            rblMgrEmpDeductionWeekly.Visible = True
            rblMgrEmpDeductionMonthly.Visible = True

            rblMgrDeductionPerPolicyWeekly.Visible = True
            rblMgrDeductionPerPolicyMonthly.Visible = True

            'End If
        ElseIf chklSendManagerNotification.Items.FindByValue("2").Selected = True Then 'weekly
            trMgrEmailFormat.Visible = True
            trMgrAbsent.Visible = True
            trMgrViolation.Visible = True

            trMgrDaily.Visible = True
            trMgrDetailed.Visible = True
            trMgrSummary.Visible = True

            dvMgrEmpSummaryAttendance.Visible = True
            dvMgrEmpDeduction.Visible = True
            dvMgrDeductionPerPolicy.Visible = True

            rblMgrAbsentWeekly.Visible = True
            rblMgrAbsentMonthly.Visible = False
            rblMgrViolationWeekly.Visible = True
            rblMgrViolationMonthly.Visible = False

            rblMgrDailyWeekly.Visible = True
            rblMgrDailyMonthly.Visible = False
            rblMgrDetailedWeekly.Visible = True
            rblMgrDetailedMonthly.Visible = False
            rblMgrSummaryWeekly.Visible = True
            rblMgrSummaryMonthly.Visible = False

            rblMgrEmpSummaryAttendanceWeekly.Visible = True
            rblMgrEmpSummaryAttendanceMonthly.Visible = False

            rblMgrEmpDeductionWeekly.Visible = True
            rblMgrEmpDeductionMonthly.Visible = False

            rblMgrDeductionPerPolicyWeekly.Visible = True
            rblMgrDeductionPerPolicyMonthly.Visible = False


        ElseIf chklSendManagerNotification.Items.FindByValue("3").Selected = True Then ' Monthly
            trMgrEmailFormat.Visible = True
            trMgrAbsent.Visible = True
            trMgrViolation.Visible = True
            dvMgrEmpSummaryAttendance.Visible = True
            dvMgrEmpDeduction.Visible = True
            dvMgrDeductionPerPolicy.Visible = True

            rblMgrAbsentWeekly.Visible = False
            rblMgrAbsentMonthly.Visible = True
            rblMgrViolationWeekly.Visible = False
            rblMgrViolationMonthly.Visible = True

            rblMgrDailyWeekly.Visible = False
            rblMgrDailyMonthly.Visible = True
            rblMgrDetailedWeekly.Visible = False
            rblMgrDetailedMonthly.Visible = True
            rblMgrSummaryWeekly.Visible = False
            rblMgrSummaryMonthly.Visible = True

            rblMgrEmpSummaryAttendanceWeekly.Visible = False
            rblMgrEmpSummaryAttendanceMonthly.Visible = True

            rblMgrEmpSummaryAttendanceMonthly.Visible = True
            rblMgrEmpDeductionWeekly.Visible = False
            rblMgrEmpDeductionMonthly.Visible = True

            rblMgrDeductionPerPolicyWeekly.Visible = False
            rblMgrDeductionPerPolicyMonthly.Visible = True

        Else
            trMgrEmailFormat.Visible = False
            trMgrAbsent.Visible = False
            trMgrViolation.Visible = False

            trMgrDaily.Visible = False
            trMgrDetailed.Visible = False
            trMgrSummary.Visible = False

            dvMgrEmpSummaryAttendance.Visible = False
            dvMgrEmpDeduction.Visible = False
            dvMgrDeductionPerPolicy.Visible = False

        End If
    End Sub

    Protected Sub chklSendHRNotification_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chklSendHRNotification.SelectedIndexChanged
        If chklSendHRNotification.Items.FindByValue("2").Selected = True And chklSendHRNotification.Items.FindByValue("3").Selected = True Then
            trHREmailFormat.Visible = True
            trHRAbsent.Visible = True
            trHRViolation.Visible = True

            trHRDaily.Visible = True
            trHRDetailed.Visible = True
            trHRSummary.Visible = True

            dvHrEmpDeduction.Visible = True
            dvHRDeductionPerPolicy.Visible = True

            rblHRAbsentWeekly.Visible = True
            rblHRViolationWeekly.Visible = True
            rblHRAbsentMonthly.Visible = True
            rblHRViolationMonthly.Visible = True

            rblHRDailyWeekly.Visible = True
            rblHRDailyMonthly.Visible = True
            rblHRDetailedWeekly.Visible = True
            rblHRDetailedMonthly.Visible = True
            rblHRSummaryWeekly.Visible = True
            rblHRSummaryMonthly.Visible = True

            rblHrEmpDeductionWeekly.Visible = True
            rblHrEmpDeductionMonthly.Visible = True

            rblHRDeductionPerPolicyWeekly.Visible = True
            rblHRDeductionPerPolicyMonthly.Visible = True

            'End If
        ElseIf chklSendHRNotification.Items.FindByValue("2").Selected = True Then 'weekly
            trHREmailFormat.Visible = True
            trHRAbsent.Visible = True
            trHRViolation.Visible = True

            trHRDaily.Visible = True
            trHRDetailed.Visible = True
            trHRSummary.Visible = True
            dvHrEmpDeduction.Visible = True
            dvHRDeductionPerPolicy.Visible = True

            rblHRAbsentWeekly.Visible = True
            rblHRAbsentMonthly.Visible = False
            rblHRViolationWeekly.Visible = True
            rblHRViolationMonthly.Visible = False

            rblHRDailyWeekly.Visible = True
            rblHRDailyMonthly.Visible = False
            rblHRDetailedWeekly.Visible = True
            rblHRDetailedMonthly.Visible = False
            rblHRSummaryWeekly.Visible = True
            rblHRSummaryMonthly.Visible = False

            rblHrEmpDeductionWeekly.Visible = True
            rblHrEmpDeductionMonthly.Visible = False

            rblHRDeductionPerPolicyWeekly.Visible = True
            rblHRDeductionPerPolicyMonthly.Visible = False

        ElseIf chklSendHRNotification.Items.FindByValue("3").Selected = True Then ' Monthly
            trHREmailFormat.Visible = True
            trHRAbsent.Visible = True
            trHRViolation.Visible = True

            trHRDaily.Visible = True
            trHRDetailed.Visible = True
            trHRSummary.Visible = True
            dvHrEmpDeduction.Visible = True
            dvHRDeductionPerPolicy.Visible = True

            rblHRAbsentWeekly.Visible = False
            rblHRAbsentMonthly.Visible = True
            rblHRViolationWeekly.Visible = False
            rblHRViolationMonthly.Visible = True

            rblHRDailyWeekly.Visible = False
            rblHRDailyMonthly.Visible = True
            rblHRDetailedWeekly.Visible = False
            rblHRDetailedMonthly.Visible = True
            rblHRSummaryWeekly.Visible = False
            rblHRSummaryMonthly.Visible = True

            rblHrEmpDeductionWeekly.Visible = False
            rblHrEmpDeductionMonthly.Visible = True

            rblHRDeductionPerPolicyWeekly.Visible = False
            rblHRDeductionPerPolicyMonthly.Visible = True

        Else
            trHREmailFormat.Visible = False
            trHRAbsent.Visible = False
            trHRViolation.Visible = False

            trHRDaily.Visible = False
            trHRDetailed.Visible = False
            trHRSummary.Visible = False

            dvHrEmpDeduction.Visible = False
            dvHRDeductionPerPolicy.Visible = False
        End If
    End Sub

    Protected Sub chklSendEntityMgrNotification_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chklSendEntityMgrNotification.SelectedIndexChanged

        If chklSendEntityMgrNotification.Items.FindByValue("2").Selected = True And chklSendEntityMgrNotification.Items.FindByValue("3").Selected = True Then
            dvEntityMgrViolation.Visible = True
            rblEntityMgrViolationWeekly.Visible = True
            rblEntityMgrViolationMonthly.Visible = True

            dvEntityManagerEmpDeduction.Visible = True
            rblEntityManagerEmpDeductionWeekly.Visible = True
            rblEntityManagerEmpDeductionMonthly.Visible = True

            dvEntityManagerMaxAbsent.Visible = True
            rblEntityManagerMaxAbsentWeekly.Visible = True
            rblEntityManagerMaxAbsentMonthly.Visible = True

            dvEntityManagerMaxDelay.Visible = True
            rblEntityManagerMaxDelayWeekly.Visible = True
            rblEntityManagerMaxDelayMonthly.Visible = True

            dvEntityManagerDeductionPerPolicy.Visible = True
            rblEntityManagerDeductionPerPolicyWeekly.Visible = True
            rblEntityManagerDeductionPerPolicyMonthly.Visible = True


        ElseIf chklSendEntityMgrNotification.Items.FindByValue("2").Selected = True Then 'weekly
            dvEntityMgrViolation.Visible = True
            rblEntityMgrViolationWeekly.Visible = True
            rblEntityMgrViolationMonthly.Visible = False

            dvEntityManagerEmpDeduction.Visible = True
            rblEntityManagerEmpDeductionWeekly.Visible = True
            rblEntityManagerEmpDeductionMonthly.Visible = False

            dvEntityManagerMaxAbsent.Visible = True
            rblEntityManagerMaxAbsentWeekly.Visible = True
            rblEntityManagerMaxAbsentMonthly.Visible = False

            dvEntityManagerMaxDelay.Visible = True
            rblEntityManagerMaxDelayWeekly.Visible = True
            rblEntityManagerMaxDelayMonthly.Visible = False

            dvEntityManagerDeductionPerPolicy.Visible = True
            rblEntityManagerDeductionPerPolicyWeekly.Visible = True
            rblEntityManagerDeductionPerPolicyMonthly.Visible = False

        ElseIf chklSendEntityMgrNotification.Items.FindByValue("3").Selected = True Then ' Monthly
            dvEntityMgrViolation.Visible = True
            rblEntityMgrViolationWeekly.Visible = False
            rblEntityMgrViolationMonthly.Visible = True

            dvEntityManagerEmpDeduction.Visible = True
            rblEntityManagerEmpDeductionWeekly.Visible = False
            rblEntityManagerEmpDeductionMonthly.Visible = True

            dvEntityManagerMaxAbsent.Visible = True
            rblEntityManagerMaxAbsentWeekly.Visible = False
            rblEntityManagerMaxAbsentMonthly.Visible = True

            dvEntityManagerMaxDelay.Visible = True
            rblEntityManagerMaxDelayWeekly.Visible = False
            rblEntityManagerMaxDelayMonthly.Visible = True

            dvEntityManagerDeductionPerPolicy.Visible = True
            rblEntityManagerDeductionPerPolicyWeekly.Visible = False
            rblEntityManagerDeductionPerPolicyMonthly.Visible = True

        Else
            dvEntityMgrViolation.Visible = False
            rblEntityMgrViolationWeekly.Visible = False
            rblEntityMgrViolationMonthly.Visible = False

            dvEntityManagerEmpDeduction.Visible = False
            rblEntityManagerEmpDeductionWeekly.Visible = False
            rblEntityManagerEmpDeductionMonthly.Visible = False

            dvEntityManagerMaxAbsent.Visible = False
            rblEntityManagerMaxAbsentWeekly.Visible = False
            rblEntityManagerMaxAbsentMonthly.Visible = False

            dvEntityManagerMaxDelay.Visible = False
            rblEntityManagerMaxDelayWeekly.Visible = False
            rblEntityManagerMaxDelayMonthly.Visible = False

            dvEntityManagerDeductionPerPolicy.Visible = False
            rblEntityManagerDeductionPerPolicyWeekly.Visible = False
            rblEntityManagerDeductionPerPolicyMonthly.Visible = False

        End If


    End Sub

    Protected Sub chklSendEmployeeNotification_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chklSendEmployeeNotification.SelectedIndexChanged

        If chklSendEmployeeNotification.Items.FindByValue("2").Selected = True And chklSendEmployeeNotification.Items.FindByValue("3").Selected = True Then
            dvEmployeeViolation.Visible = True
            rblEmployeeViolationWeekly.Visible = True
            rblEmployeeViolationMonthly.Visible = True

            dvEmployeeAbsent.Visible = True
            rblEmployeeAbsentWeekly.Visible = True
            rblEmployeeAbsentMonthly.Visible = True

            dvEmployeeDetailedAbsent.Visible = True
            rblEmployeeDetailedAbsentWeekly.Visible = True
            rblEmployeeDetailedAbsentMonthly.Visible = True

            dvEmployeeLostTimeDetails.Visible = True
            rblEmployeeLostTimeDetailsWeekly.Visible = True
            rblEmployeeLostTimeDetailsMonthly.Visible = True

            dvEmployeeEmpDeduction.Visible = True
            rblEmployeeEmpDeductionWeekly.Visible = True
            rblEmployeeEmpDeductionMonthly.Visible = True

            dvEmployeeSummary.Visible = True
            rblEmployeeSummaryWeekly.Visible = True
            rblEmployeeSummaryMonthly.Visible = True

            dvEmployeeDeductionPerPolicy.Visible = True
            rblEmployeeDeductionPerPolicyWeekly.Visible = True
            rblEmployeeDeductionPerPolicyMonthly.Visible = True

        ElseIf chklSendEmployeeNotification.Items.FindByValue("2").Selected = True Then 'weekly
            dvEmployeeViolation.Visible = True
            rblEmployeeViolationWeekly.Visible = True
            rblEmployeeViolationMonthly.Visible = False

            dvEmployeeAbsent.Visible = True
            rblEmployeeAbsentWeekly.Visible = True
            rblEmployeeAbsentMonthly.Visible = False

            dvEmployeeDetailedAbsent.Visible = True
            rblEmployeeDetailedAbsentWeekly.Visible = True
            rblEmployeeDetailedAbsentMonthly.Visible = False

            dvEmployeeLostTimeDetails.Visible = True
            rblEmployeeLostTimeDetailsWeekly.Visible = True
            rblEmployeeLostTimeDetailsMonthly.Visible = False

            dvEmployeeEmpDeduction.Visible = True
            rblEmployeeEmpDeductionWeekly.Visible = True
            rblEmployeeEmpDeductionMonthly.Visible = False

            dvEmployeeSummary.Visible = True
            rblEmployeeSummaryWeekly.Visible = True
            rblEmployeeSummaryMonthly.Visible = False

            dvEmployeeDeductionPerPolicy.Visible = True
            rblEmployeeDeductionPerPolicyWeekly.Visible = True
            rblEmployeeDeductionPerPolicyMonthly.Visible = False

        ElseIf chklSendEmployeeNotification.Items.FindByValue("3").Selected = True Then ' Monthly
            dvEmployeeViolation.Visible = True
            rblEmployeeViolationWeekly.Visible = False
            rblEmployeeViolationMonthly.Visible = True

            dvEmployeeAbsent.Visible = True
            rblEmployeeAbsentWeekly.Visible = False
            rblEmployeeAbsentMonthly.Visible = True

            dvEmployeeDetailedAbsent.Visible = True
            rblEmployeeDetailedAbsentWeekly.Visible = False
            rblEmployeeDetailedAbsentMonthly.Visible = True

            dvEmployeeLostTimeDetails.Visible = True
            rblEmployeeLostTimeDetailsWeekly.Visible = False
            rblEmployeeLostTimeDetailsMonthly.Visible = True

            dvEmployeeEmpDeduction.Visible = True
            rblEmployeeEmpDeductionWeekly.Visible = False
            rblEmployeeEmpDeductionMonthly.Visible = True

            dvEmployeeSummary.Visible = True
            rblEmployeeSummaryWeekly.Visible = False
            rblEmployeeSummaryMonthly.Visible = True

            dvEmployeeDeductionPerPolicy.Visible = True
            rblEmployeeDeductionPerPolicyWeekly.Visible = False
            rblEmployeeDeductionPerPolicyMonthly.Visible = True

        Else
            dvEmployeeViolation.Visible = False
            rblEmployeeViolationWeekly.Visible = False
            rblEmployeeViolationMonthly.Visible = False

            dvEmployeeAbsent.Visible = False
            rblEmployeeAbsentWeekly.Visible = False
            rblEmployeeAbsentMonthly.Visible = False

            dvEmployeeDetailedAbsent.Visible = False
            rblEmployeeDetailedAbsentWeekly.Visible = False
            rblEmployeeDetailedAbsentMonthly.Visible = False

            dvEmployeeLostTimeDetails.Visible = False
            rblEmployeeLostTimeDetailsWeekly.Visible = False
            rblEmployeeLostTimeDetailsMonthly.Visible = False

            dvEmployeeEmpDeduction.Visible = False
            rblEmployeeEmpDeductionWeekly.Visible = False
            rblEmployeeEmpDeductionMonthly.Visible = False

            dvEmployeeSummary.Visible = False
            rblEmployeeSummaryWeekly.Visible = False
            rblEmployeeSummaryMonthly.Visible = False

            dvEmployeeDeductionPerPolicy.Visible = False
            rblEmployeeDeductionPerPolicyWeekly.Visible = False
            rblEmployeeDeductionPerPolicyMonthly.Visible = False

        End If

    End Sub

#End Region

#Region "Methods"

    Sub fillcontrols()
        objAPP_Settings = New APP_Settings
        With objAPP_Settings
            .GetByPK()
            If (.CompanyName1 IsNot Nothing) Then
                'If Not IsDBNull(.ManageOvertime) Then
                '    rdbManageOvertime.SelectedValue = .ManageOvertime
                'End If
                'Hide Manage Default Leave By and radio buttons related'
                'If .AnnualLeaveOption = 1 Then
                '    rdbtnNone.Checked = True
                'ElseIf .AnnualLeaveOption = 2 Then
                '    rdbtnGradeAl.Checked = True
                'ElseIf .AnnualLeaveOption = 3 Then
                '    rdbtnDesignationAl.Checked = True
                'End If
                'Hide Manage Default Leave By and radio buttons related'

                CompanyName1 = .CompanyName1


                If .HREmailNotification <> 0 Then
                    rdlEmailNotifications.Items.FindByValue(.HREmailNotification).Selected = True
                End If

                txtHREmail.Text = .HRGroupEmail

                'rmtFlexibileTime.Text = (.ConsiderAbsentAfter / 60).ToString()


                'HR Notification


                If .SendToHRWeekly = True Then
                    chklSendHRNotification.Items.FindByValue("2").Selected = True
                    trHREmailFormat.Visible = True
                    dvDeductionPerPolicy_Value.Visible = True
                End If

                If .SendToHRMonthly = True Then
                    chklSendHRNotification.Items.FindByValue("3").Selected = True
                    trHREmailFormat.Visible = True
                    dvDeductionPerPolicy_Value.Visible = True
                End If
                FillHRNotificationsControls()

                'Manager Notification


                If .SendToManagerWeekly = True Then
                    chklSendManagerNotification.Items.FindByValue("2").Selected = True
                    trMgrEmailFormat.Visible = True
                    dvDeductionPerPolicy_Value.Visible = True
                End If

                If .SendToManagerMonthly = True Then
                    chklSendManagerNotification.Items.FindByValue("3").Selected = True
                    trMgrEmailFormat.Visible = True
                    dvDeductionPerPolicy_Value.Visible = True
                End If
                FillManagerNotificationsControls()

                If .SendToEntityManagerWeekly = True Then
                    chklSendEntityMgrNotification.Items.FindByValue("2").Selected = True
                    dvEntityMgrViolation.Visible = True
                    dvDeductionPerPolicy_Value.Visible = True
                End If

                If .SendToEntityManagerMonthly = True Then
                    chklSendEntityMgrNotification.Items.FindByValue("3").Selected = True
                    dvEntityMgrViolation.Visible = True
                    dvDeductionPerPolicy_Value.Visible = True
                End If

                FillOrgEntityManagerControls()

                If .SendToEmployeeWeekly = True Then
                    chklSendEmployeeNotification.Items.FindByValue("2").Selected = True
                    dvEmployeeViolation.Visible = True
                    dvEmployeeAbsent.Visible = True
                    dvDeductionPerPolicy_Value.Visible = True
                End If

                If .SendToEmployeeMonthly = True Then
                    chklSendEmployeeNotification.Items.FindByValue("3").Selected = True
                    dvEmployeeViolation.Visible = True
                    dvEmployeeAbsent.Visible = True
                    dvDeductionPerPolicy_Value.Visible = True
                End If
                FillEmployeeControls()
            End If


            cblMgrEmailFormat.SelectedValue = .ManagerEmailFormat
            cblHREmailFormat.SelectedValue = .HREmailFormat
            chkHasEmailApproval.Checked = .HasEmailApproval

            ''
            rmtEarlyOutNotifications.Text = CtlCommon.GetFullTimeString(.EarlyOutNotificationAfter)

            If Not .PermissionRequestReminderAfter = Nothing Then
                radtxtPermissionReminder.Text = .PermissionRequestReminderAfter
            Else
                radtxtPermissionReminder.Text = Nothing
            End If


            rblMgrAbsentWeekly.SelectedValue = .MgrAbsentWeekly
            rblMgrAbsentMonthly.SelectedValue = .MgrAbsentMonthly
            rblMgrViolationWeekly.SelectedValue = .MgrViolationWeekly
            rblMgrViolationMonthly.SelectedValue = .MgrViolationMonthly

            rblHRAbsentWeekly.SelectedValue = .HRAbsentWeekly
            rblHRAbsentMonthly.SelectedValue = .HRAbsentMonthly
            rblHRViolationWeekly.SelectedValue = .HRViolationWeekly
            rblHRViolationMonthly.SelectedValue = .HRViolationMonthly


            rblMgrDailyWeekly.SelectedValue = .MgrDailyWeekly
            rblMgrDailyMonthly.SelectedValue = .MgrDailyMonthly
            rblMgrDetailedWeekly.SelectedValue = .MgrDetailedWeekly
            rblMgrDetailedMonthly.SelectedValue = .MgrDetailedMonthly
            rblMgrSummaryWeekly.SelectedValue = .MgrSummaryWeekly
            rblMgrSummaryMonthly.SelectedValue = .MgrSummaryMonthly

            rblHRDailyWeekly.SelectedValue = .HRDailyWeekly
            rblHRDailyMonthly.SelectedValue = .HRDailyMonthly
            rblHRDetailedWeekly.SelectedValue = .HRDetailedWeekly
            rblHRDetailedMonthly.SelectedValue = .HRDetailedMonthly
            rblHRSummaryWeekly.SelectedValue = .HRSummaryWeekly
            rblHRSummaryMonthly.SelectedValue = .HRSummaryMonthly

            rblEntityMgrViolationWeekly.SelectedValue = .EntityManagerViolationWeekly
            rblEntityMgrViolationMonthly.SelectedValue = .EntityManagerViolationMonthly

            rblEmployeeViolationWeekly.SelectedValue = .EmployeeViolationWeekly
            rblEmployeeViolationMonthly.SelectedValue = .EmployeeViolationMonthly
            rblEmployeeAbsentWeekly.SelectedValue = .EmployeeAbsentWeekly
            rblEmployeeAbsentMonthly.SelectedValue = .EmployeeAbsentMonthly

            rblEmployeeDetailedAbsentWeekly.SelectedValue = .EmployeeDetailedAbsentWeekly
            rblEmployeeDetailedAbsentMonthly.SelectedValue = .EmployeeDetailedAbsentMonthly

            rblEmployeeLostTimeDetailsWeekly.SelectedValue = .EmployeeLostTimeDetailsWeekly
            rblEmployeeLostTimeDetailsMonthly.SelectedValue = .EmployeeLostTimeDetailsMonthly

            rblEmployeeSummaryWeekly.SelectedValue = .EmployeeSummaryWeekly
            rblEmployeeSummaryMonthly.SelectedValue = .EmployeeSummaryMonthly

            rblMgrEmpSummaryAttendanceWeekly.SelectedValue = .MgrEmpSummaryAttendanceWeekly
            rblMgrEmpSummaryAttendanceMonthly.SelectedValue = .MgrEmpSummaryAttendanceMonthly

            rblMgrEmpDeductionWeekly.SelectedValue = .MgrEmpDeductionWeekly
            rblMgrEmpDeductionMonthly.SelectedValue = .MgrEmpDeductionMonthly

            rblHrEmpDeductionWeekly.SelectedValue = .HrEmpDeductionWeekly
            rblHrEmpDeductionMonthly.SelectedValue = .HrEmpDeductionMonthly

            rblEmployeeEmpDeductionWeekly.SelectedValue = .EmployeeEmpDeductionWeekly
            rblEmployeeEmpDeductionMonthly.SelectedValue = .EmployeeEmpDeductionMonthly

            rblEntityManagerEmpDeductionWeekly.SelectedValue = .EntityManagerEmpDeductionWeekly
            rblEntityManagerEmpDeductionMonthly.SelectedValue = .EntityManagerEmpDeductionMonthly

            rblEntityManagerMaxAbsentWeekly.SelectedValue = .EntityManagerMaxAbsentWeekly
            rblEntityManagerMaxAbsentMonthly.SelectedValue = .EntityManagerMaxAbsentMonthly

            rblEntityManagerMaxDelayWeekly.SelectedValue = .EntityManagerMaxDelayWeekly
            rblEntityManagerMaxDelayMonthly.SelectedValue = .EntityManagerMaxDelayMonthly

            rblMgrDeductionPerPolicyWeekly.SelectedValue = .MgrDeductionPerPolicyWeekly
            rblMgrDeductionPerPolicyMonthly.SelectedValue = .MgrDeductionPerPolicyMonthly

            rblHRDeductionPerPolicyWeekly.SelectedValue = .HRDeductionPerPolicyWeekly
            rblHRDeductionPerPolicyMonthly.SelectedValue = .HRDeductionPerPolicyMonthly

            rblEmployeeDeductionPerPolicyWeekly.SelectedValue = .EmployeeDeductionPerPolicyWeekly
            rblEmployeeDeductionPerPolicyMonthly.SelectedValue = .EmployeeDeductionPerPolicyMonthly

            rblEntityManagerDeductionPerPolicyWeekly.SelectedValue = .EntityManagerDeductionPerPolicyWeekly
            rblEntityManagerDeductionPerPolicyMonthly.SelectedValue = .EntityManagerDeductionPerPolicyMonthly

            rblDeductionEmailFormat.SelectedValue = .DeductionEmailFormat

            txtDeductionPerPolicy_Value.Text = .DeductionPerPolicy_Value

            If .IncompleteWorkingHrs = Nothing Then
                radnumIncompleteWorkingHrs.Text = String.Empty
            Else
                radnumIncompleteWorkingHrs.Text = .IncompleteWorkingHrs
            End If

            If Not .ReminderAbsentAfter = Nothing Then
                radReminderAbsentAfter.Text = .ReminderAbsentAfter
            Else
                radReminderAbsentAfter.Text = Nothing
            End If
            chkYearlyReportFromYearBegining.Checked = .YearlyReportFromYearBegining

            If .IncludeNotifications_CoordinatorTypes = True Then
                rblIncludeNotifications_CoordinatorTypes.SelectedValue = 1
            Else
                rblIncludeNotifications_CoordinatorTypes.SelectedValue = 2
            End If
            chkSendDaily_AbsentDelay_EntityMgr.Checked = .SendDaily_AbsentDelay_EntityMgr
        End With
    End Sub

    Private Sub FillEmailConfiguration()
        objAppEmailConfiguration = New App_EmailConfigurations()
        With objAppEmailConfiguration
            .GetByPK()
            txtSmtpServer.Text = .SMTP_Server
            SMTPServer = .SMTP_Server
            txtEmailFrom.Text = .EmailFrom
            txtSMTPPassword.Text = .SMTPPassword
            txtSMTPConfirmPassword.Text = .SMTPPassword
            chkEnableEmailService.Checked = .EnableEmailService
            If chkEnableEmailService.Checked = True Then
                trEmailConfig.Visible = True
            Else
                trEmailConfig.Visible = False
            End If
            chkEnableSMSService.Checked = .EnableSMSService
            txtSMTPUserName.Text = .SMTPUserName
        End With

        Dim strNotifications As String = SmartV.Version.version.GetNotificationTypes()
        If Not strNotifications = Nothing Then
            Dim arrNotifications As ArrayList = SplitLicenseNotifications(strNotifications)
            If arrNotifications.Contains(24) Or arrNotifications.Contains(25) Then '--- 24 WeeklyNotification Id && 25 MonthlyNotification Id
                dvMonthlyOrWeeklyNotificationSettings.Visible = True
            End If
        Else
            dvMonthlyOrWeeklyNotificationSettings.Visible = True
        End If

    End Sub

    Private Sub FillManagerNotificationsControls()
        If chklSendManagerNotification.Items.FindByValue("2").Selected = True And chklSendManagerNotification.Items.FindByValue("3").Selected = True Then
            trMgrEmailFormat.Visible = True
            trMgrAbsent.Visible = True
            trMgrViolation.Visible = True

            trMgrDaily.Visible = True
            trMgrDetailed.Visible = True
            trMgrSummary.Visible = True


            rblMgrAbsentWeekly.Visible = True
            rblMgrViolationWeekly.Visible = True
            rblMgrAbsentMonthly.Visible = True
            rblMgrViolationMonthly.Visible = True

            rblMgrDailyWeekly.Visible = True
            rblMgrDailyMonthly.Visible = True
            rblMgrDetailedWeekly.Visible = True
            rblMgrDetailedMonthly.Visible = True
            rblMgrSummaryWeekly.Visible = True
            rblMgrSummaryMonthly.Visible = True

            dvMgrEmpSummaryAttendance.Visible = True
            rblMgrEmpSummaryAttendanceWeekly.Visible = True
            rblMgrEmpSummaryAttendanceMonthly.Visible = True

            dvMgrEmpDeduction.Visible = True
            rblMgrEmpDeductionWeekly.Visible = True
            rblMgrEmpDeductionMonthly.Visible = True

            dvMgrDeductionPerPolicy.Visible = True
            rblMgrDeductionPerPolicyWeekly.Visible = True
            rblMgrDeductionPerPolicyMonthly.Visible = True

        ElseIf chklSendManagerNotification.Items.FindByValue("2").Selected = True Then 'weekly
            trMgrEmailFormat.Visible = True
            trMgrAbsent.Visible = True
            trMgrViolation.Visible = True

            trMgrDaily.Visible = True
            trMgrDetailed.Visible = True
            trMgrSummary.Visible = True

            rblMgrAbsentWeekly.Visible = True
            rblMgrAbsentMonthly.Visible = False
            rblMgrViolationWeekly.Visible = True
            rblMgrViolationMonthly.Visible = False

            rblMgrDailyWeekly.Visible = True
            rblMgrDailyMonthly.Visible = False
            rblMgrDetailedWeekly.Visible = True
            rblMgrDetailedMonthly.Visible = False
            rblMgrSummaryWeekly.Visible = True
            rblMgrSummaryMonthly.Visible = False

            dvMgrEmpSummaryAttendance.Visible = True
            rblMgrEmpSummaryAttendanceWeekly.Visible = True
            rblMgrEmpSummaryAttendanceMonthly.Visible = False

            dvMgrEmpDeduction.Visible = True
            rblMgrEmpDeductionWeekly.Visible = True
            rblMgrEmpDeductionMonthly.Visible = False

            dvMgrDeductionPerPolicy.Visible = True
            rblMgrDeductionPerPolicyWeekly.Visible = True
            rblMgrDeductionPerPolicyMonthly.Visible = False

        ElseIf chklSendManagerNotification.Items.FindByValue("3").Selected = True Then ' Monthly
            trMgrEmailFormat.Visible = True
            trMgrAbsent.Visible = True
            trMgrViolation.Visible = True


            rblMgrAbsentWeekly.Visible = False
            rblMgrAbsentMonthly.Visible = True
            rblMgrViolationWeekly.Visible = False
            rblMgrViolationMonthly.Visible = True

            rblMgrDailyWeekly.Visible = False
            rblMgrDailyMonthly.Visible = True
            rblMgrDetailedWeekly.Visible = False
            rblMgrDetailedMonthly.Visible = True
            rblMgrSummaryWeekly.Visible = False
            rblMgrSummaryMonthly.Visible = True

            dvMgrEmpSummaryAttendance.Visible = True
            rblMgrEmpSummaryAttendanceWeekly.Visible = False
            rblMgrEmpSummaryAttendanceMonthly.Visible = True

            dvMgrEmpDeduction.Visible = True
            rblMgrEmpDeductionWeekly.Visible = False
            rblMgrEmpDeductionMonthly.Visible = True

            dvMgrDeductionPerPolicy.Visible = True
            rblMgrDeductionPerPolicyWeekly.Visible = False
            rblMgrDeductionPerPolicyMonthly.Visible = True

        Else
            trMgrEmailFormat.Visible = False
            trMgrAbsent.Visible = False
            trMgrViolation.Visible = False

            trMgrDaily.Visible = False
            trMgrDetailed.Visible = False
            trMgrSummary.Visible = False

            dvMgrEmpSummaryAttendance.Visible = False
            dvMgrEmpDeduction.Visible = False

            dvMgrDeductionPerPolicy.Visible = False

        End If
    End Sub

    Private Sub FillHRNotificationsControls()
        If chklSendHRNotification.Items.FindByValue("2").Selected = True And chklSendHRNotification.Items.FindByValue("3").Selected = True Then
            trHREmailFormat.Visible = True
            trHRAbsent.Visible = True
            trHRViolation.Visible = True

            trHRDaily.Visible = True
            trHRDetailed.Visible = True
            trHRSummary.Visible = True

            rblHRAbsentWeekly.Visible = True
            rblHRViolationWeekly.Visible = True
            rblHRAbsentMonthly.Visible = True
            rblHRViolationMonthly.Visible = True

            rblHRDailyWeekly.Visible = True
            rblHRDailyMonthly.Visible = True
            rblHRDetailedWeekly.Visible = True
            rblHRDetailedMonthly.Visible = True
            rblHRSummaryWeekly.Visible = True
            rblHRSummaryMonthly.Visible = True

            dvHrEmpDeduction.Visible = True
            rblHrEmpDeductionWeekly.Visible = True
            rblHrEmpDeductionMonthly.Visible = True

            dvHRDeductionPerPolicy.Visible = True
            rblHRDeductionPerPolicyWeekly.Visible = True
            rblEmployeeDeductionPerPolicyMonthly.Visible = True

        ElseIf chklSendHRNotification.Items.FindByValue("2").Selected = True Then 'weekly
            trHREmailFormat.Visible = True
            trHRAbsent.Visible = True
            trHRViolation.Visible = True

            trHRDaily.Visible = True
            trHRDetailed.Visible = True
            trHRSummary.Visible = True

            rblHRAbsentWeekly.Visible = True
            rblHRAbsentMonthly.Visible = False
            rblHRViolationWeekly.Visible = True
            rblHRViolationMonthly.Visible = False

            rblHRDailyWeekly.Visible = True
            rblHRDailyMonthly.Visible = False
            rblHRDetailedWeekly.Visible = True
            rblHRDetailedMonthly.Visible = False
            rblHRSummaryWeekly.Visible = True
            rblHRSummaryMonthly.Visible = False

            dvHrEmpDeduction.Visible = True
            rblHrEmpDeductionWeekly.Visible = True
            rblHrEmpDeductionMonthly.Visible = False

            dvHRDeductionPerPolicy.Visible = True
            rblHRDeductionPerPolicyWeekly.Visible = True
            rblEmployeeDeductionPerPolicyMonthly.Visible = False

        ElseIf chklSendHRNotification.Items.FindByValue("3").Selected = True Then ' Monthly
            trHREmailFormat.Visible = True
            trHRAbsent.Visible = True
            trHRViolation.Visible = True

            trHRDaily.Visible = True
            trHRDetailed.Visible = True
            trHRSummary.Visible = True

            rblHRAbsentWeekly.Visible = False
            rblHRAbsentMonthly.Visible = True
            rblHRViolationWeekly.Visible = False
            rblHRViolationMonthly.Visible = True

            rblHRDailyWeekly.Visible = False
            rblHRDailyMonthly.Visible = True
            rblHRDetailedWeekly.Visible = False
            rblHRDetailedMonthly.Visible = True
            rblHRSummaryWeekly.Visible = False
            rblHRSummaryMonthly.Visible = True

            dvHrEmpDeduction.Visible = True
            rblHrEmpDeductionWeekly.Visible = False
            rblHrEmpDeductionMonthly.Visible = True

            dvHRDeductionPerPolicy.Visible = True
            rblHRDeductionPerPolicyWeekly.Visible = False
            rblEmployeeDeductionPerPolicyMonthly.Visible = True

        Else
            trHREmailFormat.Visible = False
            trHRAbsent.Visible = False
            trHRViolation.Visible = False

            trHRDaily.Visible = False
            trHRDetailed.Visible = False
            trHRSummary.Visible = False

            dvHrEmpDeduction.Visible = False

            dvHRDeductionPerPolicy.Visible = False

        End If
    End Sub

    Private Sub FillOrgEntityManagerControls()

        If chklSendEntityMgrNotification.Items.FindByValue("2").Selected = True And chklSendEntityMgrNotification.Items.FindByValue("3").Selected = True Then
            dvEntityMgrViolation.Visible = True
            rblEntityMgrViolationWeekly.Visible = True
            rblEntityMgrViolationMonthly.Visible = True

            dvEntityManagerEmpDeduction.Visible = True
            rblEntityManagerEmpDeductionWeekly.Visible = True
            rblEntityManagerEmpDeductionMonthly.Visible = True

            dvEntityManagerMaxAbsent.Visible = True
            rblEntityManagerMaxAbsentWeekly.Visible = True
            rblEntityManagerMaxAbsentMonthly.Visible = True

            dvEntityManagerMaxDelay.Visible = True
            rblEntityManagerMaxDelayWeekly.Visible = True
            rblEntityManagerMaxDelayMonthly.Visible = True

            dvEntityManagerDeductionPerPolicy.Visible = True
            rblEntityManagerDeductionPerPolicyWeekly.Visible = True
            rblEntityManagerDeductionPerPolicyMonthly.Visible = True

        ElseIf chklSendEntityMgrNotification.Items.FindByValue("2").Selected = True Then 'weekly
            dvEntityMgrViolation.Visible = True
            rblEntityMgrViolationWeekly.Visible = True
            rblEntityMgrViolationMonthly.Visible = False

            dvEntityManagerEmpDeduction.Visible = True
            rblEntityManagerEmpDeductionWeekly.Visible = True
            rblEntityManagerEmpDeductionMonthly.Visible = False

            dvEntityManagerMaxAbsent.Visible = True
            rblEntityManagerMaxAbsentWeekly.Visible = True
            rblEntityManagerMaxAbsentMonthly.Visible = False

            dvEntityManagerMaxDelay.Visible = True
            rblEntityManagerMaxDelayWeekly.Visible = True
            rblEntityManagerMaxDelayMonthly.Visible = False

            dvEntityManagerDeductionPerPolicy.Visible = True
            rblEntityManagerDeductionPerPolicyWeekly.Visible = True
            rblEntityManagerDeductionPerPolicyMonthly.Visible = False

        ElseIf chklSendEntityMgrNotification.Items.FindByValue("3").Selected = True Then ' Monthly
            dvEntityMgrViolation.Visible = True
            rblEntityMgrViolationWeekly.Visible = False
            rblEntityMgrViolationMonthly.Visible = True

            dvEntityManagerEmpDeduction.Visible = True
            rblEntityManagerEmpDeductionWeekly.Visible = False
            rblEntityManagerEmpDeductionMonthly.Visible = True

            dvEntityManagerMaxAbsent.Visible = True
            rblEntityManagerMaxAbsentWeekly.Visible = False
            rblEntityManagerMaxAbsentMonthly.Visible = True

            dvEntityManagerMaxDelay.Visible = True
            rblEntityManagerMaxDelayWeekly.Visible = False
            rblEntityManagerMaxDelayMonthly.Visible = True

            dvEntityManagerDeductionPerPolicy.Visible = True
            rblEntityManagerDeductionPerPolicyWeekly.Visible = False
            rblEntityManagerDeductionPerPolicyMonthly.Visible = True

        Else
            dvEntityMgrViolation.Visible = False
            rblEntityMgrViolationWeekly.Visible = False
            rblEntityMgrViolationMonthly.Visible = False

            dvEntityManagerEmpDeduction.Visible = False
            rblEntityManagerEmpDeductionWeekly.Visible = False
            rblEntityManagerEmpDeductionMonthly.Visible = False

            dvEntityManagerMaxAbsent.Visible = False
            rblEntityManagerMaxAbsentWeekly.Visible = False
            rblEntityManagerMaxAbsentMonthly.Visible = False

            dvEntityManagerMaxDelay.Visible = False
            rblEntityManagerMaxDelayWeekly.Visible = False
            rblEntityManagerMaxDelayMonthly.Visible = False

            dvEntityManagerDeductionPerPolicy.Visible = False
            rblEntityManagerDeductionPerPolicyWeekly.Visible = False
            rblEntityManagerDeductionPerPolicyMonthly.Visible = False

        End If

    End Sub

    Private Sub FillEmployeeControls()
        If chklSendEmployeeNotification.Items.FindByValue("2").Selected = True And chklSendEmployeeNotification.Items.FindByValue("3").Selected = True Then
            dvEmployeeViolation.Visible = True
            rblEmployeeViolationWeekly.Visible = True
            rblEmployeeViolationMonthly.Visible = True

            dvEmployeeAbsent.Visible = True
            rblEmployeeAbsentWeekly.Visible = True
            rblEmployeeAbsentMonthly.Visible = True

            dvEmployeeDetailedAbsent.Visible = True
            rblEmployeeDetailedAbsentWeekly.Visible = True
            rblEmployeeDetailedAbsentMonthly.Visible = True

            dvEmployeeLostTimeDetails.Visible = True
            rblEmployeeLostTimeDetailsWeekly.Visible = True
            rblEmployeeLostTimeDetailsMonthly.Visible = True

            dvEmployeeEmpDeduction.Visible = True
            rblEmployeeEmpDeductionWeekly.Visible = True
            rblEmployeeEmpDeductionMonthly.Visible = True

            dvEmployeeSummary.Visible = True
            rblEmployeeSummaryWeekly.Visible = True
            rblEmployeeSummaryMonthly.Visible = True

            dvEmployeeDeductionPerPolicy.Visible = True
            rblEmployeeDeductionPerPolicyWeekly.Visible = True
            rblEmployeeDeductionPerPolicyMonthly.Visible = True

        ElseIf chklSendEmployeeNotification.Items.FindByValue("2").Selected = True Then 'weekly
            dvEmployeeViolation.Visible = True
            rblEmployeeViolationWeekly.Visible = True
            rblEmployeeViolationMonthly.Visible = False

            dvEmployeeAbsent.Visible = True
            rblEmployeeAbsentWeekly.Visible = True
            rblEmployeeAbsentMonthly.Visible = False

            dvEmployeeDetailedAbsent.Visible = True
            rblEmployeeDetailedAbsentWeekly.Visible = True
            rblEmployeeDetailedAbsentMonthly.Visible = False

            dvEmployeeLostTimeDetails.Visible = True
            rblEmployeeLostTimeDetailsWeekly.Visible = True
            rblEmployeeLostTimeDetailsMonthly.Visible = False

            dvEmployeeEmpDeduction.Visible = True
            rblEmployeeEmpDeductionWeekly.Visible = True
            rblEmployeeEmpDeductionMonthly.Visible = False

            dvEmployeeSummary.Visible = True
            rblEmployeeSummaryWeekly.Visible = True
            rblEmployeeSummaryMonthly.Visible = False

            dvEmployeeDeductionPerPolicy.Visible = True
            rblEmployeeDeductionPerPolicyWeekly.Visible = True
            rblEmployeeDeductionPerPolicyMonthly.Visible = False

        ElseIf chklSendEmployeeNotification.Items.FindByValue("3").Selected = True Then ' Monthly
            dvEmployeeViolation.Visible = True
            rblEmployeeViolationWeekly.Visible = False
            rblEmployeeViolationMonthly.Visible = True

            dvEmployeeAbsent.Visible = True
            rblEmployeeAbsentWeekly.Visible = False
            rblEmployeeAbsentMonthly.Visible = True

            dvEmployeeDetailedAbsent.Visible = True
            rblEmployeeDetailedAbsentWeekly.Visible = False
            rblEmployeeDetailedAbsentMonthly.Visible = True

            dvEmployeeLostTimeDetails.Visible = True
            rblEmployeeLostTimeDetailsWeekly.Visible = False
            rblEmployeeLostTimeDetailsMonthly.Visible = True

            dvEmployeeEmpDeduction.Visible = True
            rblEmployeeEmpDeductionWeekly.Visible = False
            rblEmployeeEmpDeductionMonthly.Visible = True

            dvEmployeeSummary.Visible = True
            rblEmployeeSummaryWeekly.Visible = False
            rblEmployeeSummaryMonthly.Visible = True

            dvEmployeeDeductionPerPolicy.Visible = True
            rblEmployeeDeductionPerPolicyWeekly.Visible = False
            rblEmployeeDeductionPerPolicyMonthly.Visible = True

        Else
            dvEmployeeViolation.Visible = False
            rblEmployeeViolationWeekly.Visible = False
            rblEmployeeViolationMonthly.Visible = False

            dvEmployeeAbsent.Visible = False
            rblEmployeeAbsentWeekly.Visible = False
            rblEmployeeAbsentMonthly.Visible = False

            dvEmployeeDetailedAbsent.Visible = False
            rblEmployeeDetailedAbsentWeekly.Visible = False
            rblEmployeeDetailedAbsentMonthly.Visible = False

            dvEmployeeLostTimeDetails.Visible = False
            rblEmployeeLostTimeDetailsWeekly.Visible = False
            rblEmployeeLostTimeDetailsMonthly.Visible = False

            dvEmployeeEmpDeduction.Visible = False
            rblEmployeeEmpDeductionWeekly.Visible = False
            rblEmployeeEmpDeductionMonthly.Visible = False

            dvEmployeeSummary.Visible = False
            rblEmployeeSummaryWeekly.Visible = False
            rblEmployeeSummaryMonthly.Visible = False

            dvEmployeeDeductionPerPolicy.Visible = False
            rblEmployeeDeductionPerPolicyWeekly.Visible = False
            rblEmployeeDeductionPerPolicyMonthly.Visible = False

        End If
    End Sub

    Private Function SplitLicenseNotifications(ByVal LicenseNotifications As String) As ArrayList
        Dim s As String = LicenseNotifications.Trim
        Dim arrNotifications As New ArrayList
        For Each value As String In s.Split(","c)
            arrNotifications.Add(Convert.ToInt32(value))
        Next
        Return arrNotifications
    End Function

#End Region

End Class
