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

Partial Class TA_App_settings
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
    Private objTA_Reason As TA_Reason
    Private objVersion As SmartV.Version.version
    Private objSYSForms As SYSForms

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
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            PageHeader1.HeaderText = ResourceManager.GetString("AppSettings", CultureInfo)
            CtlCommon.FillTelerikDropDownList(RadWeeksDay, objWeekDays.GetForDDL, Lang)
            CtlCommon.FillTelerikDropDownList(RadComboBoxWorkSchedules, objWorkSchedule.GetAllFORDDL, Lang)
            CtlCommon.FillTelerikDropDownList(RadComboBoxStudyWorkSchedules, objWorkSchedule.GetAllFORDDL, Lang)
            CtlCommon.FillTelerikDropDownList(RadComboBoxOnCallWorkSchedules, objWorkSchedule.GetAllFORDDL, Lang)
            rmtFlexibileTime.TextWithLiterals = "0000"
            Enable_Disable_MobileFeatures()
            IntializeSettings()
            ChkboxlistEmployeeRequests()
            ChkboxlistDurationTotalsToAppear()
            FillRemoteWorkTAReason()
            Fillcontrols()
            LoadGroupDDL()
            FillLevels()
            FillLeaveType()
            FillPermissionType()
            chkShowSTSupremeLogo.Checked = CBool(ConfigurationManager.AppSettings("ShowSmartTimeLogo"))
            If ConfigurationManager.AppSettings("AllowEditArchive") = "0" Then
                txtArchivingMonths.Enabled = False
            Else
                txtArchivingMonths.Enabled = True
            End If

            If ValidateForms(3140) = True Then
                dvEnableUniversitySelection_StudyPermission.Visible = True
            Else
                dvEnableUniversitySelection_StudyPermission.Visible = False
            End If

            If ValidateForms(3082) = True Then
                dvEnableSemesterSelection_StudyPermission.Visible = True
            Else
                dvEnableSemesterSelection_StudyPermission.Visible = False
            End If

            If ValidateForms(3141) = True Then
                dvEnableMajorSelection_StudyPermission.Visible = True
            Else
                dvEnableMajorSelection_StudyPermission.Visible = False
            End If

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
        If txtConsiderDays.Text = "0" Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ConsiderDays", CultureInfo), "info")
            Return
        End If
        objAPP_Settings = New APP_Settings
        With objAPP_Settings
            .CompanyName1 = CompanyName1
            objAPP_Settings = .GetByPK
            .CompanyName1 = txtFirstCompanyName.Text
            .CompanyName2 = txtSecondCompanyName.Text
            .companyArabicName1 = txtFirstCompanyNameArabic.Text
            .CompanyArabicName2 = txtSecondCompanyNameArabic.Text
            .EmployeeCardLength = txtEmployeeCardLength.Text
            .EmployeeNoLength = txtEmployeeNoLength.Text
            .DaysMinutes = Convert.ToInt32(txtConsiderDays.Text)

            If Not String.IsNullOrEmpty(rbNormalColoredReport.SelectedValue) Then
                .IsDailyReportWithColor = rbNormalColoredReport.SelectedValue
            Else
                .IsDailyReportWithColor = 0
            End If

            If Not String.IsNullOrEmpty(rdbConsequenceTransactions.SelectedValue) Then
                .ConsequenceTransactions = rdbConsequenceTransactions.SelectedValue
            Else
                .ConsequenceTransactions = 0
            End If

            If Not String.IsNullOrEmpty(rlsDynamicReportView.SelectedValue) Then
                .DynamicReportView = rlsDynamicReportView.SelectedValue
            Else
                .DynamicReportView = 0
            End If
            .NursingDays = txtNursingDays.Text

            'Send To Manager

            'Send To HR


            '.ManageOvertime = rdbManageOvertime.SelectedValue
            '.MinGapbetweenMoves = 0
            If rdbtnGradeTaex.Checked Then
                .IsGradeTAException = True
            End If
            If rdbtnDesignationTaex.Checked Then
                .IsGradeTAException = False
            End If
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
            If rdbtnGradeot.Checked Then
                .IsGradeOvertimeRule = True
            End If
            If rdbtnDesignationot.Checked Then
                .IsGradeOvertimeRule = False
            End If

            If rdbGrace.Items(1).Selected Then
                .IsGraceTAPolicy = True
            Else
                .IsGraceTAPolicy = False
            End If
            If rblOvertime.SelectedValue = 1 Then
                .AllowEditOverTime = True
            Else
                .AllowEditOverTime = False
            End If
            .MinGapbetweenMoves = txtMintransactiontime.Text
            .LeaveApproval = rlstApproval.SelectedValue
            .SystemUsersType = rblSystemUsers.SelectedValue


            Dim strFlexibileDuration As String = (CInt(rmtFlexibileTime.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtFlexibileTime.TextWithLiterals.Split(":")(1))
            .ConsiderAbsentAfter = strFlexibileDuration

            If BrFromFile.HasFile Then

                Dim extention As String = Path.GetExtension(BrFromFile.PostedFile.FileName).Trim()
                Dim fileName As String = txtFirstCompanyName.Text.Trim()
                Dim fPath As String = String.Empty
                fPath = Server.MapPath("..\CompanyLogo\\" + fileName + extention)

                If File.Exists(fPath) Then
                    File.Delete(fPath)
                End If

                BrFromFile.PostedFile.SaveAs(fPath)

                Dim img As FileUpload = CType(BrFromFile, FileUpload)
                Dim imgByte As Byte() = Nothing
                If img.HasFile AndAlso Not img.PostedFile Is Nothing Then
                    Dim File As HttpPostedFile = BrFromFile.PostedFile

                    imgByte = New Byte(File.ContentLength - 1) {}
                    File.InputStream.Read(imgByte, 0, File.ContentLength)
                    .LogoPath = File.FileName
                End If
                .LogoImage = imgByte
                .Update_Image()
            Else
                .LogoPath = ""
            End If

            If cmbLeaveApprovalFrom.SelectedValue = "2" Then
                .LeaveApprovalfromLeave = True
            Else
                .LeaveApprovalfromLeave = False
            End If

            If cmbPermissionApprovalFrom.SelectedValue = "2" Then
                .PermApprovalFromPermission = True
            Else
                .PermApprovalFromPermission = False
            End If

            If Not String.IsNullOrEmpty(rlsNoShiftShcedule.SelectedValue) Then
                .NoShiftShcedule = rlsNoShiftShcedule.SelectedValue
            End If

            If rdlFullDayPermission.SelectedValue = "1" Then
                .HasFullDayPermission = True
            Else
                .HasFullDayPermission = False
            End If
            .AllowChangeEmpNo = rblChangeNo.SelectedValue
            If radtxtAllowedManual.Text = Nothing Then
                .AllowedBefore = Nothing
            Else
                .AllowedBefore = radtxtAllowedManual.Text
            End If

            If Not RadComboBoxStudyWorkSchedules.SelectedValue = -1 Then
                .StudyPermissionSchedule = RadComboBoxStudyWorkSchedules.SelectedValue
            Else
                .StudyPermissionSchedule = Nothing
            End If
            .NursingPermissionAttend = chkNursingPermAttend.Checked

            If radtxtNoManualEntry.Text = Nothing Then
                .ManualEntryNo = Nothing
            Else
                .ManualEntryNo = radtxtNoManualEntry.Text
            End If

            If radcmbMinOutViolation.Text = Nothing Then
                .MinOutViolation = Nothing
            Else
                .MinOutViolation = radcmbMinOutViolation.Text
            End If



            If Not radcmbLevels.SelectedValue = -1 Then
                .ManualEntryManagerLevelRequired = radcmbLevels.SelectedValue
            Else
                .ManualEntryManagerLevelRequired = Nothing
            End If


            If rdlShowLoginForm.SelectedValue = 1 Then
                .ShowLoginForm = True
            Else
                .ShowLoginForm = False
            End If

            If rdlShowViolationCorrection.SelectedValue = 1 Then
                .ShowViolationCorrection = True
            Else
                .ShowViolationCorrection = False
            End If

            If Not radcmbMaternityLeaveType.SelectedIndex = -1 Then
                .FK_MaternityLeaveTypeId = radcmbMaternityLeaveType.SelectedValue
            Else
                .FK_MaternityLeaveTypeId = Nothing
            End If

            If Not radcmbPersonalPermissionType.SelectedIndex = -1 Then
                .FK_PersonalPermissionTypeId = radcmbPersonalPermissionType.SelectedValue
            Else
                .FK_PersonalPermissionTypeId = Nothing
            End If

            .NursingRequireMaternity = chkNursingRequireMaternity.Checked

            'cblEployeeRequests 
            Dim RequestsChecked As String = ""
            For Each item As ListItem In cblEployeeRequests.Items
                If item.Selected Then
                    RequestsChecked = item.Value + "," + RequestsChecked
                End If
            Next
            .RequestGridToAppear = RequestsChecked
            ''


            If rdlHasFlexiblePermission.SelectedValue = 1 Then
                .HasFlexiblePermission = True
            Else
                .HasFlexiblePermission = False
            End If

            If rdlHasFlexibleNursingPermission.SelectedValue = 1 Then
                .HasFlexibleNursingPermission = True
            Else
                .HasFlexibleNursingPermission = False
            End If

            If rdlShowAbsentInViolationCorrection.SelectedValue = 1 Then
                .ShowAbsentInViolationCorrection = True
            Else
                .ShowAbsentInViolationCorrection = False
            End If

            If rdlHasPermissionForPeriod.SelectedValue = 1 Then
                .HasPermissionForPeriod = True
            Else
                .HasPermissionForPeriod = False
            End If
            If rdlConsiderPermissionBalance.SelectedValue = 1 Then
                .IsToConsiderBalanceInHours = False
            Else
                .IsToConsiderBalanceInHours = True

            End If
            If rdlShowThemeToUsers.SelectedValue = 1 Then
                .ShowThemeToUsers = True
            Else
                .ShowThemeToUsers = False
            End If

            'cblDurationTotalsToAppear
            Dim DurationTotalsToAppearChecked As String = ""
            For Each item As ListItem In cblDurationTotalsToAppear.Items
                If item.Selected Then
                    DurationTotalsToAppearChecked = item.Value + "," + DurationTotalsToAppearChecked
                End If
            Next
            .DurationTotalsToAppear = DurationTotalsToAppearChecked
            ''

            If rdlAttachmentIsMandatory.SelectedValue = 1 Then
                .AttachmentIsMandatory = True
            Else
                .AttachmentIsMandatory = False
            End If

            If rdlIsFirstGrid.SelectedValue = 1 Then
                .IsFirstGrid = True
            Else
                .IsFirstGrid = False
            End If

            Dim strDefaultStudyPermissionFromTime As String = (CInt(rmtFromTime.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtFromTime.TextWithLiterals.Split(":")(1))
            .DefaultStudyPermissionFromTime = strDefaultStudyPermissionFromTime

            Dim strDefaultStudyPermissionToTime As String = (CInt(rmtToTime.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtToTime.TextWithLiterals.Split(":")(1))
            .DefaultStudyPermissionToTime = strDefaultStudyPermissionToTime

            Dim strDefaultStudyPermissionFlexibleTime As String = (CInt(rmtStudyPermissionFlexibleTime.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtStudyPermissionFlexibleTime.TextWithLiterals.Split(":")(1))
            .DefaultStudyPermissionFlexibleTime = strDefaultStudyPermissionFlexibleTime

            If rdlShowAnnouncement.SelectedValue = 1 Then
                .ShowAnnouncement = True
            Else
                .ShowAnnouncement = False
            End If
            If rdlDivideTwoPermission.SelectedValue = 1 Then
                .DivideTwoPermission = True
            Else
                .DivideTwoPermission = False
            End If

            .MaternityLeaveDuration = Convert.ToInt32(txtMaternityLeaveDuration.Text)

            .StudyPermissionApproval = rdlStudyPermissionApproval.SelectedValue

            .NursingPermissionApproval = rdlNursingPermissionApproval.SelectedValue

            .MaxStudyPermission = txtMaxStudyDuration.Text
            .StudyGeneralGuide = txtStudyGeneralGuide.Text
            .StudyGeneralGuideAr = txtStudyGeneralGuideAr.Text
            .NursingGeneralGuide = txtNursingGeneralGuide.Text
            .NursingGeneralGuideAr = txtNursingGeneralGuideAr.Text

            If rdlAllowMoreOneManualEntry.SelectedValue = 1 Then
                .AllowMoreOneManualEntry = True
            Else
                .AllowMoreOneManualEntry = False
            End If

            If rdlShowAnnouncementSelfSevice.SelectedValue = 1 Then
                .ShowAnnouncementSelfService = True
            Else
                .ShowAnnouncementSelfService = False
            End If


            If Not RadComboBoxOnCallWorkSchedules.SelectedValue = -1 Then
                .OnCallSchedule = RadComboBoxOnCallWorkSchedules.SelectedValue
            Else
                .OnCallSchedule = Nothing
            End If

            If rdlAllowNursingPermissionInRamadan.SelectedValue = 1 Then
                .AllowNursingInRamadan = True
            Else
                .AllowNursingInRamadan = False
            End If

            If rdlShowEmployeeList.SelectedValue = 1 Then
                .ShowEmployeeList = True
            Else
                .ShowEmployeeList = False
            End If

            .ConsiderAbsentOrLogicalAbsent = rblConsiderAbsentOrLogicalAbsent.SelectedValue

            If rblAllowDeleteSchedule.SelectedValue = 1 Then
                .AllowDeleteSchedule = True
            Else
                .AllowDeleteSchedule = False
            End If



            If rdlConsiderAbsentEvenAttend.SelectedValue = 1 Then
                .ConsiderAbsentEvenAttend = True
            Else
                .ConsiderAbsentEvenAttend = False
            End If

            .DefaultReportFormat = rblDefaultReportFormat.SelectedValue


            If rdlAutoPersonalPermissionDelay.SelectedValue = 1 Then
                .AutoPersonalPermissionDelay = True
            Else
                .AutoPersonalPermissionDelay = False
            End If
            .AutoPermissionDelayDuration = txtAutoPermissionDelayDuration.Text

            If rdlAutoPersonalPermissionEarlyOut.SelectedValue = 1 Then
                .AutoPersonalPermissionEarlyOut = True
            Else
                .AutoPersonalPermissionEarlyOut = False
            End If
            .AutoPermissionEarlyOutDuration = txtAutoPermissionEarlyOutDuration.Text


            If rdlHasMultiApproval.SelectedValue = 1 Then
                .HasMultiApproval = True
            Else
                .HasMultiApproval = False
            End If

            .EmployeeManagerFilter = rdlEmployeeManagerFilter.SelectedValue

            .FillCheckBoxList = rdlFillCheckBoxList.SelectedValue
            .DefaultTheme = radDefaultTheme.SelectedValue

            If Not txtArchivingMonths.Text = Nothing Then
                .ArchivingMonths = -txtArchivingMonths.Text
            Else
                .ArchivingMonths = Nothing
            End If

            If rblShowLGWithEntityPerv.SelectedValue = 1 Then
                .ShowLGWithEntityPrivilege = True
            Else
                .ShowLGWithEntityPrivilege = False
            End If
            .ManagerDefaultPage = rblManagerDefaultPage.SelectedValue
            If rblShowDirectchk.SelectedValue = 1 Then
                .ShowDirectStaffChk = True
            Else
                .ShowDirectStaffChk = False
            End If
            If rblManualRequestAttachement.SelectedValue = 2 Then
                .AttachmentIsMandatoryManualEntryRequest = False
            Else
                .AttachmentIsMandatoryManualEntryRequest = True
            End If

            If rblManualRequestRemark.SelectedValue = 2 Then
                .RemarkIsMandatoryManualEntryRequest = False
            Else
                .RemarkIsMandatoryManualEntryRequest = True
            End If

            If rblHRManualRequestAttachement.SelectedValue = 2 Then
                .AttachmentIsMandatoryHRManualEntry = False
            Else
                .AttachmentIsMandatoryHRManualEntry = True
            End If
            .ApprovalRecalMethod = rblApprovalRecalMethod.SelectedValue
            .StudyPerm_NotificationException = chkStudyPerm_Exception.Checked
            .NursingPerm_NotificationException = chkNursingPerm_Exception.Checked
            .DailyReportDate = rblDailyReportDate.SelectedValue
            .ExcludeGraceFromLostTime = chkExcludeGraceFromLostTime.Checked

            If dvEnableSemesterSelection_StudyPermission.Visible = True Then
                If rblEnableSemesterSelection_StudyPermission.SelectedValue = 1 Then
                    .EnableSemesterSelection_StudyPermission = True
                Else
                    .EnableSemesterSelection_StudyPermission = False
                End If
            Else
                .EnableSemesterSelection_StudyPermission = False
            End If

            If rblMustCompleteNoHours_RequestPermission.SelectedValue = 1 Then
                dvMustCompleteNoHours_RequestPermission.Visible = True
                .MustCompleteNoHours_RequestPermission = True
                .NoOfHours_RequestPermission = (CInt(rmtNoOfHours_MustComplete.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtNoOfHours_MustComplete.TextWithLiterals.Split(":")(1))
                .IncludeConsiderInWorkPermissions_RequestPermission = chkIncludeConsiderInWorkPermissions.Checked
            Else
                dvMustCompleteNoHours_RequestPermission.Visible = False
                .MustCompleteNoHours_RequestPermission = False
                .NoOfHours_RequestPermission = 0
                .IncludeConsiderInWorkPermissions_RequestPermission = chkIncludeConsiderInWorkPermissions.Checked
            End If

            .MonthlyDeduction_Report = rblDeductionReport.SelectedValue
            If rblSystemUsers.SelectedValue = 2 Then
                .PasswordType = 1
            Else
                .PasswordType = rblPasswordType.SelectedValue
            End If

            If rblShowLeavelnk.SelectedValue = 1 Then
                .ShowLeaveLnk_ViolationCorrection = True
            Else
                .ShowLeaveLnk_ViolationCorrection = False
            End If

            If rblShowPermissionlnk.SelectedValue = 1 Then
                .ShowPermissionLnk_ViolationCorrection = True
            Else
                .ShowPermissionLnk_ViolationCorrection = False
            End If

            If radnumViolationJustificationDays.Text = "" Then
                .ViolationJustificationDays = Nothing
            Else
                .ViolationJustificationDays = radnumViolationJustificationDays.Text
            End If

            If rblAllowEditManualEntryRequestDate.SelectedValue = 1 Then
                .AllowEditManualEntryRequestDate = True
            Else
                .AllowEditManualEntryRequestDate = False
            End If

            If rblAllowEditManualEntryRequestTime.SelectedValue = 1 Then
                .AllowEditManualEntryRequestTime = True
            Else
                .AllowEditManualEntryRequestTime = False
            End If

            .NumberInTransactionRequests = txtNumberInTransactionRequests.Text
            .NumberOutTransactionRequests = txtNumberOutTransactionRequests.Text

            .IsAutoApproveManualEntryRequest = chkIsAutoApproveManualEntryRequest.Checked

            Dim strViolationJustificationDaysPolicy As String = String.Empty
            For Each item As ListItem In cblViolationJustificationDaysPolicy.Items
                If item.Selected = True Then
                    strViolationJustificationDaysPolicy += item.Value + ","
                End If
            Next
            If strViolationJustificationDaysPolicy = "" Then
                .ViolationJustificationDaysPolicy = Nothing
            Else
                .ViolationJustificationDaysPolicy = strViolationJustificationDaysPolicy
            End If

            If rblIsAbsentRestPolicy.SelectedValue = 1 Then
                .IsAbsentRestPolicy = True
            Else
                .IsAbsentRestPolicy = False
            End If

            .ConsiderLeaveOnOffDay = rblConsiderLeaveOnOffDay.SelectedValue

            .ManualEntryNoPerMonth = radnumNoManualEntryPerMonth.Text
            .ConsiderRestInshiftSch = rblConsiderRestInshiftSch.SelectedValue
            If rblConsiderNursingInRamadan.SelectedValue = 1 Then
                .ConsiderNursingInRamadan = True
            Else
                .ConsiderNursingInRamadan = False
            End If

            Dim RemoteWorkTAReasonChecked As String = ""
            For Each item As ListItem In cblRemoteWorkTAReason.Items
                If item.Selected Then
                    RemoteWorkTAReasonChecked = item.Value + "," + RemoteWorkTAReasonChecked
                End If
            Next
            .RemoteWorkTAReason = RemoteWorkTAReasonChecked

            If dvHasTawajudFeatures.Visible = True Then
                .HasTawajudFeatures = IIf(rblHasTawajudFeatures.SelectedValue = 1, True, False)
            Else
                .HasTawajudFeatures = False
            End If

            If dvHasMultiLocations.Visible = True Then
                .HasMultiLocations = IIf(rblHasMultiLocations.SelectedValue = 1, True, False)
            Else
                .HasMultiLocations = False
            End If

            If dvHasHeartBeat.Visible = True Then
                .HasHeartBeat = IIf(rblHasHeartBeat.SelectedValue = 1, True, False)
            Else
                .HasHeartBeat = False
            End If

            If dvHasFeedback.Visible = True Then
                .HasFeedback = IIf(rblHasFeedback.SelectedValue = 1, True, False)
            Else
                .HasFeedback = False
            End If

            If dvAllowFingerPunch.Visible = True Then
                .AllowFingerPunch = IIf(rblAllowFingerPunch.SelectedValue = 1, True, False)
            Else
                .AllowFingerPunch = False
            End If

            If dvAllowFingerLogin.Visible = True Then
                .AllowFingerLogin = IIf(rblAllowFingerLogin.SelectedValue = 1, True, False)
            Else
                .AllowFingerLogin = False
            End If

            If dvAllowFacePunch.Visible = True Then
                .AllowFacePunch = IIf(rblAllowFacePunch.SelectedValue = 1, True, False)
            Else
                .AllowFacePunch = False
            End If

            If dvAllowFaceLogin.Visible = True Then
                .AllowFaceLogin = IIf(rblAllowFaceLogin.SelectedValue = 1, True, False)
            Else
                .AllowFaceLogin = False
            End If

            If dvEnableUniversitySelection_StudyPermission.Visible = True Then
                If rblEnableUniversitySelection_StudyPermission.SelectedValue = 1 Then
                    .EnableUniversitySelection_StudyPermission = True
                Else
                    .EnableUniversitySelection_StudyPermission = False
                End If
            Else
                .EnableUniversitySelection_StudyPermission = False
            End If

            If Not txtStudyAllowedAfterDays.Text = String.Empty Then
                .StudyAllowedAfterDays = txtStudyAllowedAfterDays.Text
            Else
                .StudyAllowedAfterDays = Nothing
            End If

            If Not txtStudyAllowedBeforeDays.Text = String.Empty Then
                .StudyAllowedBeforeDays = txtStudyAllowedBeforeDays.Text
            Else
                .StudyAllowedBeforeDays = Nothing
            End If

            If dvEnableMajorSelection_StudyPermission.Visible = True Then
                If rblEnableMajorSelection_StudyPermission.SelectedValue = 1 Then
                    .EnableMajorSelection_StudyPermission = True
                Else
                    .EnableMajorSelection_StudyPermission = False
                End If
            Else
                .EnableMajorSelection_StudyPermission = False
            End If

            Dim errno As Integer = .Add()

            SetStartWeekDay()
            SetIsDefaultWorkSchedualles()

            'Save Users Settings
            'If rblSystemUsers.SelectedValue = "1" Then 'Domain
            ' 
            'ElseIf rblSystemUsers.SelectedValue = "2" Then 'System
            '  
            'End If
            '

            If errno = 0 Then

                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ModifySuccessfully", CultureInfo), "success")
                ConfigurationManager.AppSettings("ShowSmartTimeLogo") = chkShowSTSupremeLogo.Checked.ToString()

            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorModfiy", CultureInfo), "error")

            End If

        End With
    End Sub

    Protected Sub rblSystemUsers_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblSystemUsers.SelectedIndexChanged
        If rblSystemUsers.SelectedValue = "1" Then 'System User
            tabDomainUsers.Visible = False
            dvPasswordType.Visible = True
        ElseIf rblSystemUsers.SelectedValue = 2 Then
            dvPasswordType.Visible = False
        Else 'Domain or Mixed
            tabDomainUsers.Visible = True
            dvPasswordType.Visible = True
        End If
    End Sub

    Protected Sub btnImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImport.Click
        Try

            Dim deRoot As New DirectoryEntry("LDAP://RootDSE")
            Dim defaultNamingContext As String = ""

            If deRoot IsNot Nothing Then
                defaultNamingContext = deRoot.Properties("defaultNamingContext").Value.ToString()
            End If

            Dim dfDirectory As String = "LDAP://" & defaultNamingContext

            Dim domain1 As DirectoryEntry
            Dim searcher As DirectorySearcher
            domain1 = New DirectoryEntry(dfDirectory, txtDomainUserName.Text, txtDomainPassword.Text, AuthenticationTypes.ReadonlyServer) 'New DirectoryEntry("LDAP://CN=users,DC=smartv,DC=com")
            'domain1 = New DirectoryEntry(dfDirectory)

            searcher = New DirectorySearcher() 'New DirectorySearcher("(samAccountType=805306368)") '
            searcher.SearchRoot = domain1
            searcher.SearchScope = SearchScope.Subtree

            searcher.Filter = "(&ObjectCategory=User)"
            searcher.ReferralChasing = ReferralChasingOption.All
            objUsers = New SYSUsers

            Dim errno As Integer = 0
            Dim intcount As Integer = 0
            Using userlist As System.DirectoryServices.SearchResultCollection = searcher.FindAll()
                For i As Integer = 0 To userlist.Count - 1

                    Dim usernameforme As String = ""
                    Dim userEmail As String = ""
                    Dim strFullName As String = ""
                    Dim strADEmployeeID As String = ""
                    Dim strADEmployeeNo As String = ""
                    Dim strSurname As String = ""

                    usernameforme = userlist(i).GetDirectoryEntry().Properties("SAMAccountName").Value 'userlist(i).GetDirectoryEntry().Properties("givenName").Value
                    userEmail = userlist(i).GetDirectoryEntry().Properties("mail").Value
                    strFullName = userlist(i).GetDirectoryEntry().Properties("givenName").Value
                    strSurname = userlist(i).GetDirectoryEntry().Properties("Surname").Value
                    strADEmployeeID = userlist(i).GetDirectoryEntry().Properties("employeeID").Value
                    strADEmployeeNo = userlist(i).GetDirectoryEntry().Properties("employeeNumber").Value
                    If usernameforme <> "" Then
                        intcount += 1
                        With objUsers
                            .ID = 0
                            .UsrID = usernameforme
                            .fullName = IIf(strFullName <> "", strFullName, " ") & "  " & IIf(strSurname <> "", strSurname, " ")
                            .GroupId = ddlUsersGroup.SelectedValue
                            .LoginName = usernameforme
                            .UserType = 2 'Domain User
                            .Active = 1
                            .UserEmail = IIf(userEmail <> "", userEmail, "DOMAIN_EMAIL")
                            .JobDesc = ""
                            .UserPhone = ""
                            .Remarks = ""
                            .FK_EmployeeId = -1
                            .Password = ""
                            errno += .SaveActiveDirectory()
                        End With
                    End If
                Next
            End Using
            If intcount = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NoUsersImported", CultureInfo), "info")
                Exit Sub
            End If

            If errno = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ImportSuccessfully", CultureInfo), "success")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorImport", CultureInfo), "error")
            End If
        Catch ex As Exception
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorImport", CultureInfo), "error")
        End Try
    End Sub

    'Protected Sub cmbLeaveApprovalFrom_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbLeaveApprovalFrom.SelectedIndexChanged
    '    If cmbLeaveApprovalFrom.SelectedValue = "2" Then
    '        lblLeaveApproval.Text = ResourceManager.GetString("PermissionApproval", CultureInfo)
    '    Else
    '        lblLeaveApproval.Text = ResourceManager.GetString("LeaveAndPermissionApproval", CultureInfo)
    '    End If
    'End Sub

    Protected Sub chkNursingRequireMaternity_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkNursingRequireMaternity.CheckedChanged
        If chkNursingRequireMaternity.Checked Then
            MaternityLeaveDuration.Visible = True
        Else
            MaternityLeaveDuration.Visible = False
        End If
    End Sub

    Protected Sub rdlAutoPersonalPermissionDelay_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rdlAutoPersonalPermissionDelay.SelectedIndexChanged
        If rdlAutoPersonalPermissionDelay.SelectedValue = 1 Then
            trDelayDuration.Visible = True
        Else
            trDelayDuration.Visible = False
        End If

    End Sub

    Protected Sub rdlAutoPersonalPermissionEarlyOut_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rdlAutoPersonalPermissionEarlyOut.SelectedIndexChanged
        If rdlAutoPersonalPermissionEarlyOut.SelectedValue = 1 Then
            trEarlyOutDuration.Visible = True
        Else
            trEarlyOutDuration.Visible = False
        End If
    End Sub

    Protected Sub rblMustCompleteNoHours_RequestPermission_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblMustCompleteNoHours_RequestPermission.SelectedIndexChanged
        If rblMustCompleteNoHours_RequestPermission.SelectedValue = 1 Then
            dvMustCompleteNoHours_RequestPermission.Visible = True
        Else
            dvMustCompleteNoHours_RequestPermission.Visible = False
        End If
    End Sub

    Private Sub chkIsAutoApproveManualEntryRequest_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsAutoApproveManualEntryRequest.CheckedChanged
        If chkIsAutoApproveManualEntryRequest.Checked = True Then
            dvManagerLevel.Visible = False
        Else
            dvManagerLevel.Visible = True
        End If
    End Sub

#End Region

#Region "Methods"
    Private Sub FillLevels()
        objOrgLevel = New OrgLevel
        With objOrgLevel
            CtlCommon.FillTelerikDropDownList(radcmbLevels, .GetAll_Company, Lang)
        End With
    End Sub

    Sub Fillcontrols()
        objAPP_Settings = New APP_Settings
        With objAPP_Settings
            .GetByPK()
            If (.CompanyName1 IsNot Nothing) Then
                CompanyName1 = .CompanyName1
                txtFirstCompanyName.Text = .CompanyName1
                txtSecondCompanyName.Text = .CompanyName2
                txtFirstCompanyNameArabic.Text = .companyArabicName1
                txtSecondCompanyNameArabic.Text = .CompanyArabicName2
                txtEmployeeCardLength.Text = .EmployeeCardLength
                txtEmployeeNoLength.Text = .EmployeeNoLength
                rlstApproval.SelectedValue = .LeaveApproval
                rlsDynamicReportView.SelectedValue = .DynamicReportView
                rbNormalColoredReport.SelectedValue = IIf(.IsDailyReportWithColor, 1, 0)
                txtConsiderDays.Text = .DaysMinutes.ToString()
                If Not IsDBNull(.ConsequenceTransactions) Then
                    rdbConsequenceTransactions.SelectedValue = .ConsequenceTransactions
                End If
                'If Not IsDBNull(.ManageOvertime) Then
                '    rdbManageOvertime.SelectedValue = .ManageOvertime
                'End If
                If .IsGradeTAException Then
                    rdbtnGradeTaex.Checked = True
                ElseIf Not .IsGradeTAException Then
                    rdbtnDesignationTaex.Checked = True
                End If
                txtMintransactiontime.Text = .MinGapbetweenMoves
                'Hide Manage Default Leave By and radio buttons related'
                'If .AnnualLeaveOption = 1 Then
                '    rdbtnNone.Checked = True
                'ElseIf .AnnualLeaveOption = 2 Then
                '    rdbtnGradeAl.Checked = True
                'ElseIf .AnnualLeaveOption = 3 Then
                '    rdbtnDesignationAl.Checked = True
                'End If
                'Hide Manage Default Leave By and radio buttons related'
                If .IsGradeOvertimeRule Then
                    rdbtnGradeot.Checked = True
                ElseIf Not .IsGradeOvertimeRule Then
                    rdbtnDesignationot.Checked = True
                End If

                If .IsGraceTAPolicy Then
                    rdbGrace.Items(1).Selected = True
                ElseIf Not .IsGraceTAPolicy Then
                    rdbGrace.Items(0).Selected = True
                End If
                If .AllowEditOverTime = True Then
                    rblOvertime.SelectedValue = 1
                Else
                    rblOvertime.SelectedValue = 2
                End If


                If objAPP_Settings.LogoImage Is Nothing Then
                    Thumbnail.Visible = False
                Else
                    Thumbnail.ImageUrl = "~/ShowImage.ashx"
                End If

                If .SystemUsersType <> 0 Then
                    rblSystemUsers.Items.FindByValue(.SystemUsersType).Selected = True
                End If

                'rmtFlexibileTime.Text = (.ConsiderAbsentAfter / 60).ToString()
                rmtFlexibileTime.Text = CtlCommon.GetFullTimeString(.ConsiderAbsentAfter)

                If .LeaveApprovalfromLeave Then
                    cmbLeaveApprovalFrom.SelectedValue = "2"
                    'lblLeaveApproval.Text = ResourceManager.GetString("PermissionApproval", CultureInfo)
                Else
                    cmbLeaveApprovalFrom.SelectedValue = "1"
                    ' lblLeaveApproval.Text = ResourceManager.GetString("LeaveAndPermissionApproval", CultureInfo)
                End If

                If .PermApprovalFromPermission Then
                    cmbPermissionApprovalFrom.SelectedValue = "2"
                    'lblLeaveApproval.Text = ResourceManager.GetString("PermissionApproval", CultureInfo)
                Else
                    cmbPermissionApprovalFrom.SelectedValue = "1"
                    ' lblLeaveApproval.Text = ResourceManager.GetString("LeaveAndPermissionApproval", CultureInfo)
                End If

                'HR Notification



                rlsNoShiftShcedule.SelectedValue = .NoShiftShcedule

                txtNursingDays.Text = .NursingDays

                If .HasFullDayPermission Then
                    rdlFullDayPermission.Items.FindByValue(1).Selected = True
                Else
                    rdlFullDayPermission.Items.FindByValue(2).Selected = True
                End If
                rblChangeNo.SelectedValue = .AllowChangeEmpNo

                If Not .AllowedBefore = Nothing Then
                    radtxtAllowedManual.Text = .AllowedBefore
                Else
                    radtxtAllowedManual.Text = Nothing
                End If
                If Not .StudyPermissionSchedule = Nothing Then
                    RadComboBoxStudyWorkSchedules.SelectedValue = .StudyPermissionSchedule
                Else
                    RadComboBoxStudyWorkSchedules.SelectedValue = -1
                End If
                chkNursingPermAttend.Checked = .NursingPermissionAttend
            End If

            If Not .ManualEntryNo = Nothing Then
                radtxtNoManualEntry.Text = .ManualEntryNo
            Else
                radtxtNoManualEntry.Text = Nothing
            End If

            radcmbMinOutViolation.Text = .MinOutViolation

            If Not .ManualEntryManagerLevelRequired = Nothing Then
                radcmbLevels.SelectedValue = .ManualEntryManagerLevelRequired
            End If
            If .ShowLoginForm = True Then
                rdlShowLoginForm.SelectedValue = 1
            Else
                rdlShowLoginForm.SelectedValue = 2
            End If
            If .ShowViolationCorrection = True Then
                rdlShowViolationCorrection.SelectedValue = 1
            Else
                rdlShowViolationCorrection.SelectedValue = 2
            End If

            If Not .FK_MaternityLeaveTypeId = Nothing Then
                radcmbMaternityLeaveType.SelectedValue = .FK_MaternityLeaveTypeId
            End If

            If Not .FK_PersonalPermissionTypeId = Nothing Then
                radcmbPersonalPermissionType.SelectedValue = .FK_PersonalPermissionTypeId
            End If

            chkNursingRequireMaternity.Checked = .NursingRequireMaternity

            'fill cblEployeeRequests
            For Each item As ListItem In cblEployeeRequests.Items
                For Each i As String In .RequestGridToAppear.Split(",")
                    If item.Value = i Then
                        item.Selected = True
                        Exit For
                    End If
                Next
            Next
            ''

            If .HasFlexiblePermission = True Then
                rdlHasFlexiblePermission.SelectedValue = 1
            Else
                rdlHasFlexiblePermission.SelectedValue = 2
            End If

            If .HasFlexibleNursingPermission = True Then
                rdlHasFlexibleNursingPermission.SelectedValue = 1
            Else
                rdlHasFlexibleNursingPermission.SelectedValue = 2
            End If

            If .ShowAbsentInViolationCorrection = True Then
                rdlShowAbsentInViolationCorrection.SelectedValue = 1
            Else
                rdlShowAbsentInViolationCorrection.SelectedValue = 2
            End If

            If .HasPermissionForPeriod = True Then
                rdlHasPermissionForPeriod.SelectedValue = 1
            Else
                rdlHasPermissionForPeriod.SelectedValue = 2
            End If

            If .ShowLGWithEntityPrivilege = True Then
                rblShowLGWithEntityPerv.SelectedValue = 1
            Else
                rblShowLGWithEntityPerv.SelectedValue = 2
            End If

            If .IsToConsiderBalanceInHours = False Then
                rdlConsiderPermissionBalance.SelectedValue = 1
            Else
                rdlConsiderPermissionBalance.SelectedValue = 0
            End If

            If .ShowThemeToUsers = True Then
                rdlShowThemeToUsers.SelectedValue = 1
            Else
                rdlShowThemeToUsers.SelectedValue = 2
            End If

            'fill cblDurationTotalsToAppear
            For Each itemTotals As ListItem In cblDurationTotalsToAppear.Items
                For Each TotalsVal As String In .DurationTotalsToAppear.Split(",")
                    If itemTotals.Value = TotalsVal Then
                        itemTotals.Selected = True
                        Exit For
                    End If
                Next
            Next
            ''

            If .AttachmentIsMandatory = True Then
                rdlAttachmentIsMandatory.SelectedValue = 1
            Else
                rdlAttachmentIsMandatory.SelectedValue = 2
            End If

            If .IsFirstGrid = True Then
                rdlIsFirstGrid.SelectedValue = 1
            Else
                rdlIsFirstGrid.SelectedValue = 2
            End If

            rmtFromTime.Text = CtlCommon.GetFullTimeString(.DefaultStudyPermissionFromTime)

            rmtToTime.Text = CtlCommon.GetFullTimeString(.DefaultStudyPermissionToTime)

            rmtStudyPermissionFlexibleTime.Text = CtlCommon.GetFullTimeString(.DefaultStudyPermissionFlexibleTime)


            If .ShowAnnouncement = True Then
                rdlShowAnnouncement.SelectedValue = 1
            Else
                rdlShowAnnouncement.SelectedValue = 2
            End If

            If .DivideTwoPermission = True Then
                rdlDivideTwoPermission.SelectedValue = 1
            Else
                rdlDivideTwoPermission.SelectedValue = 2
            End If

            txtMaternityLeaveDuration.Text = .MaternityLeaveDuration.ToString()
            If chkNursingRequireMaternity.Checked Then
                MaternityLeaveDuration.Visible = True
            Else
                MaternityLeaveDuration.Visible = False
            End If

            rdlStudyPermissionApproval.SelectedValue = .StudyPermissionApproval

            rdlNursingPermissionApproval.SelectedValue = .NursingPermissionApproval

            txtMaxStudyDuration.Text = .MaxStudyPermission
            txtStudyGeneralGuide.Text = .StudyGeneralGuide
            txtStudyGeneralGuideAr.Text = .StudyGeneralGuideAr
            txtNursingGeneralGuide.Text = .NursingGeneralGuide
            txtNursingGeneralGuideAr.Text = .NursingGeneralGuideAr

            If .AllowMoreOneManualEntry = True Then
                rdlAllowMoreOneManualEntry.SelectedValue = 1
            Else
                rdlAllowMoreOneManualEntry.SelectedValue = 2
            End If

            If .ShowAnnouncementSelfService = True Then
                rdlShowAnnouncementSelfSevice.SelectedValue = 1
            Else
                rdlShowAnnouncementSelfSevice.SelectedValue = 2
            End If

            If Not .OnCallSchedule = Nothing Then
                RadComboBoxOnCallWorkSchedules.SelectedValue = .OnCallSchedule
            Else
                RadComboBoxOnCallWorkSchedules.SelectedValue = -1
            End If

            If .AllowNursingInRamadan = True Then
                rdlAllowNursingPermissionInRamadan.SelectedValue = 1
            Else
                rdlAllowNursingPermissionInRamadan.SelectedValue = 2
            End If

            If .ShowEmployeeList = True Then
                rdlShowEmployeeList.SelectedValue = 1
            Else
                rdlShowEmployeeList.SelectedValue = 2
            End If

            If .ConsiderAbsentEvenAttend = True Then
                rdlConsiderAbsentEvenAttend.SelectedValue = 1
            Else
                rdlConsiderAbsentEvenAttend.SelectedValue = 2
            End If

            rblConsiderAbsentOrLogicalAbsent.SelectedValue = .ConsiderAbsentOrLogicalAbsent

            If .DefaultReportFormat <> 0 Then
                rblDefaultReportFormat.Items.FindByValue(.DefaultReportFormat).Selected = True
            End If

            If .AutoPersonalPermissionDelay = True Then
                rdlAutoPersonalPermissionDelay.SelectedValue = 1
                trDelayDuration.Visible = True
            Else
                rdlAutoPersonalPermissionDelay.SelectedValue = 2
                trDelayDuration.Visible = False
            End If
            txtAutoPermissionDelayDuration.Text = .AutoPermissionDelayDuration

            If .AutoPersonalPermissionEarlyOut = True Then
                rdlAutoPersonalPermissionEarlyOut.SelectedValue = 1
                trEarlyOutDuration.Visible = True
            Else
                rdlAutoPersonalPermissionEarlyOut.SelectedValue = 2
                trEarlyOutDuration.Visible = False
            End If
            txtAutoPermissionEarlyOutDuration.Text = .AutoPermissionEarlyOutDuration


            If .HasMultiApproval = True Then
                rdlHasMultiApproval.SelectedValue = 1
            Else
                rdlHasMultiApproval.SelectedValue = 2
            End If

            If .EmployeeManagerFilter <> 0 Then
                rdlEmployeeManagerFilter.Items.FindByValue(.EmployeeManagerFilter).Selected = True
            End If

            If .FillCheckBoxList <> 0 Then
                rdlFillCheckBoxList.Items.FindByValue(.FillCheckBoxList).Selected = True
            End If


            If .AllowDeleteSchedule = True Then
                rblAllowDeleteSchedule.SelectedValue = 1
            Else
                rblAllowDeleteSchedule.SelectedValue = 2
            End If
            radDefaultTheme.SelectedValue = .DefaultTheme

            If Not .ArchivingMonths = Nothing Then

                txtArchivingMonths.Text = .ArchivingMonths * -1
            End If
            rblManagerDefaultPage.SelectedValue = .ManagerDefaultPage

            If .ShowDirectStaffChk = True Then
                rblShowDirectchk.SelectedValue = 1
            Else
                rblShowDirectchk.SelectedValue = 2
            End If
            rblApprovalRecalMethod.SelectedValue = .ApprovalRecalMethod
            chkStudyPerm_Exception.Checked = .StudyPerm_NotificationException
            chkNursingPerm_Exception.Checked = .NursingPerm_NotificationException

            If .AttachmentIsMandatoryManualEntryRequest = False Then
                rblManualRequestAttachement.SelectedValue = 2
            Else
                rblManualRequestAttachement.SelectedValue = 1
            End If
            If .RemarkIsMandatoryManualEntryRequest = False Then
                rblManualRequestRemark.SelectedValue = 2
            Else
                rblManualRequestRemark.SelectedValue = 1
            End If
            If .AttachmentIsMandatoryHRManualEntry = False Then
                rblHRManualRequestAttachement.SelectedValue = 2
            Else
                rblHRManualRequestAttachement.SelectedValue = 1
            End If

            rblDailyReportDate.SelectedValue = .DailyReportDate

            chkExcludeGraceFromLostTime.Checked = .ExcludeGraceFromLostTime

            If .EnableSemesterSelection_StudyPermission = True Then
                rblEnableSemesterSelection_StudyPermission.SelectedValue = 1
            Else
                rblEnableSemesterSelection_StudyPermission.SelectedValue = 2
            End If

            If .MustCompleteNoHours_RequestPermission = True Then
                rblMustCompleteNoHours_RequestPermission.SelectedValue = 1
                dvMustCompleteNoHours_RequestPermission.Visible = True
                rmtNoOfHours_MustComplete.Text = CtlCommon.GetFullTimeString(.NoOfHours_RequestPermission)
                chkIncludeConsiderInWorkPermissions.Checked = .IncludeConsiderInWorkPermissions_RequestPermission
            Else
                rblMustCompleteNoHours_RequestPermission.SelectedValue = 0
                dvMustCompleteNoHours_RequestPermission.Visible = False
                rmtNoOfHours_MustComplete.Text = CtlCommon.GetFullTimeString(.NoOfHours_RequestPermission)
                chkIncludeConsiderInWorkPermissions.Checked = .IncludeConsiderInWorkPermissions_RequestPermission
            End If
            If Not .MonthlyDeduction_Report = 0 Then
                rblDeductionReport.SelectedValue = .MonthlyDeduction_Report
            End If

            If .SystemUsersType = 2 Then
                dvPasswordType.Visible = False
                rblPasswordType.SelectedValue = 1
            Else
                dvPasswordType.Visible = True
                rblPasswordType.SelectedValue = .PasswordType
            End If

            If .ShowLeaveLnk_ViolationCorrection = True Then
                rblShowLeavelnk.SelectedValue = 1
            Else
                rblShowLeavelnk.SelectedValue = 2
            End If

            If .ShowPermissionLnk_ViolationCorrection = True Then
                rblShowPermissionlnk.SelectedValue = 1
            Else
                rblShowPermissionlnk.SelectedValue = 2
            End If

            rblConsiderLeaveOnOffDay.SelectedValue = .ConsiderLeaveOnOffDay

            radnumViolationJustificationDays.Text = .ViolationJustificationDays

            If (Not String.IsNullOrEmpty(.ViolationJustificationDaysPolicy)) Then
                If (.ViolationJustificationDaysPolicy.Contains(",")) Then
                    Dim daysArr() As String = .ViolationJustificationDaysPolicy.Split(",")
                    For j As Integer = 0 To daysArr.Length - 1
                        If (Not String.IsNullOrEmpty(daysArr(j))) Then
                            cblViolationJustificationDaysPolicy.Items.FindByValue(daysArr(j)).Selected = True
                        End If
                    Next
                End If
            End If
            radnumNoManualEntryPerMonth.Text = .ManualEntryNoPerMonth

            If .IsAbsentRestPolicy = True Then
                rblIsAbsentRestPolicy.SelectedValue = 1
            Else
                rblIsAbsentRestPolicy.SelectedValue = 2
            End If

            If .AllowEditManualEntryRequestDate = True Then
                rblAllowEditManualEntryRequestDate.SelectedValue = 1
            Else
                rblAllowEditManualEntryRequestDate.SelectedValue = 2
            End If

            If .AllowEditManualEntryRequestTime = True Then
                rblAllowEditManualEntryRequestTime.SelectedValue = 1
            Else
                rblAllowEditManualEntryRequestTime.SelectedValue = 2
            End If

            txtNumberInTransactionRequests.Text = .NumberInTransactionRequests
            txtNumberOutTransactionRequests.Text = .NumberOutTransactionRequests

            chkIsAutoApproveManualEntryRequest.Checked = .IsAutoApproveManualEntryRequest
            rblConsiderRestInshiftSch.SelectedValue = .ConsiderRestInshiftSch

            If .ConsiderNursingInRamadan = True Then
                rblConsiderNursingInRamadan.SelectedValue = 1
            Else
                rblConsiderNursingInRamadan.SelectedValue = 2
            End If

            If Not .RemoteWorkTAReason = Nothing Then
                For Each item As ListItem In cblRemoteWorkTAReason.Items
                    For Each i As String In .RemoteWorkTAReason.Split(",")
                        If item.Value = i Then
                            item.Selected = True
                            Exit For
                        End If
                    Next
                Next
            End If

            If dvHasTawajudFeatures.Visible = True Then
                rblHasTawajudFeatures.SelectedValue = IIf(.HasTawajudFeatures = True, 1, 2)
            Else
                rblHasTawajudFeatures.SelectedValue = 2
            End If

            If dvHasMultiLocations.Visible = True Then
                rblHasMultiLocations.SelectedValue = IIf(.HasMultiLocations = True, 1, 2)
            Else
                rblHasMultiLocations.SelectedValue = 2
            End If

            If dvHasHeartBeat.Visible = True Then
                rblHasHeartBeat.SelectedValue = IIf(.HasHeartBeat = True, 1, 2)
            Else
                rblHasHeartBeat.SelectedValue = 2
            End If

            If dvHasFeedback.Visible = True Then
                rblHasFeedback.SelectedValue = IIf(.HasFeedback = True, 1, 2)
            Else
                rblHasFeedback.SelectedValue = 2
            End If

            If dvAllowFingerPunch.Visible = True Then
                rblAllowFingerPunch.SelectedValue = IIf(.AllowFingerPunch = True, 1, 2)
            Else
                rblAllowFingerPunch.SelectedValue = 2
            End If

            If dvAllowFingerLogin.Visible = True Then
                rblAllowFingerLogin.SelectedValue = IIf(.AllowFingerLogin = True, 1, 2)
            Else
                rblAllowFingerLogin.SelectedValue = 2
            End If

            If dvAllowFacePunch.Visible = True Then
                rblAllowFacePunch.SelectedValue = IIf(.AllowFacePunch = True, 1, 2)
            Else
                rblAllowFacePunch.SelectedValue = 2
            End If

            If dvAllowFaceLogin.Visible = True Then
                rblAllowFaceLogin.SelectedValue = IIf(.AllowFaceLogin = True, 1, 2)
            Else
                rblAllowFaceLogin.SelectedValue = 2
            End If

            If .EnableUniversitySelection_StudyPermission = True Then
                rblEnableUniversitySelection_StudyPermission.SelectedValue = 1
            Else
                rblEnableUniversitySelection_StudyPermission.SelectedValue = 2
            End If

            If Not .StudyAllowedAfterDays Is Nothing Then
                txtStudyAllowedAfterDays.Text = .StudyAllowedAfterDays
            Else
                txtStudyAllowedAfterDays.Text = String.Empty
            End If

            If Not .StudyAllowedBeforeDays Is Nothing Then
                txtStudyAllowedBeforeDays.Text = .StudyAllowedBeforeDays
            Else
                txtStudyAllowedBeforeDays.Text = String.Empty
            End If

            If .EnableMajorSelection_StudyPermission = True Then
                rblEnableMajorSelection_StudyPermission.SelectedValue = 1
            Else
                rblEnableMajorSelection_StudyPermission.SelectedValue = 2
            End If

        End With

        objWeekDays = New WeekDays()
        With objWeekDays
            .DayOrder = 1
            .GetByDayrder()
            If Not .DayId = 0 Then
                RadWeeksDay.Items.FindItemByValue(.DayId.ToString()).Selected = True
            End If
        End With

        objWorkSchedule = New WorkSchedule

        With objWorkSchedule
            .GetByDefault()
            WorkSchedualID = .ScheduleId

            If Not .ScheduleId = 0 Then

                RadComboBoxWorkSchedules.Items.FindItemByValue(.ScheduleId).Selected = True

            End If
        End With


        'Select System Users from Web.Config
        'No need to change in config because application will log out
        'Dim strSystemUsers As String = ""
        'strSystemUsers = ConfigurationManager.AppSettings("AuthenticationType").ToString()
        'If strSystemUsers = "User" Then
        '    rblSystemUsers.SelectedValue = "1"
        '    tabDomainUsers.Visible = False
        'ElseIf strSystemUsers = "Domain" Then
        '    rblSystemUsers.SelectedValue = "2"
        '    tabDomainUsers.Visible = True
        'End If
        If rblSystemUsers.SelectedValue = "1" Then
            tabDomainUsers.Visible = False
        Else
            tabDomainUsers.Visible = True
        End If
        '



    End Sub

    Sub IntializeSettings()
        rlstApproval.SelectedValue = 1
        rdbtnGradeot.Checked = True
        rdbtnDesignationot.Checked = False
        'Hide Manage Default Leave By and radio buttons related'
        'rdbtnNone.Checked = True
        'rdbtnGradeAl.Checked = False
        'rdbtnDesignationAl.Checked = False
        'Hide Manage Default Leave By and radio buttons related'
        rdbtnGradeTaex.Checked = True
        rdbtnDesignationTaex.Checked = False
        rdbGrace.SelectedIndex = 0
        'rdbManageOvertime.SelectedIndex = 0
        rdbConsequenceTransactions.SelectedIndex = 0
        RadWeeksDay.ClearSelection()
        rlsDynamicReportView.ClearSelection()
    End Sub

    Private Sub LoadGroupDDL()
        Dim objGroup As SYSGroups = New SYSGroups
        If SessionVariables.CultureInfo = "en-US" Then
            CtlCommon.FillTelerikDropDownList(ddlUsersGroup, objGroup.GetAll(1), Lang)
        Else
            CtlCommon.FillTelerikDropDownList(ddlUsersGroup, objGroup.GetAll(0), Lang)
        End If
    End Sub

    Private Function SetStartWeekDay()
        Dim dayOrder As Integer = 1
        objWeekDays = New WeekDays
        Dim dtWeekdays As New DataTable
        Dim dayId As Integer = RadWeeksDay.SelectedValue

        With objWeekDays
            .DayId = RadWeeksDay.SelectedValue
            .DayOrder = dayOrder
            .UpdateDayOrder()
        End With

        dtWeekdays = objWeekDays.GetAll()

        For Each row As DataRow In dtWeekdays.Rows
            dayId += 1
            If (dayId = RadWeeksDay.SelectedValue) Then
                Exit For
            Else
                If (Not dayId = 8) Then
                    dayOrder += 1

                    objWeekDays = New WeekDays

                    With objWeekDays
                        .DayId = dayId
                        .DayOrder = dayOrder
                        .UpdateDayOrder()
                    End With
                Else

                    dayId = 1

                    If (dayId = RadWeeksDay.SelectedValue) Then
                        Exit For
                    Else
                        dayOrder += 1

                        objWeekDays = New WeekDays

                        With objWeekDays
                            .DayId = dayId
                            .DayOrder = dayOrder
                            .UpdateDayOrder()
                        End With
                    End If
                End If
            End If
        Next
    End Function

    Private Sub SetIsDefaultWorkSchedualles()

        objWorkSchedule = New WorkSchedule

        With objWorkSchedule

            .ScheduleId = RadComboBoxWorkSchedules.SelectedValue
            .IsDefault = True
            .UpdateIsdefault()

        End With

    End Sub

    Private Function SaveImgToSQL(ByVal FromFile As FileUpload) As Byte()
        Dim img As FileUpload = CType(FromFile, FileUpload)
        Dim imgByte As Byte() = Nothing
        If img.HasFile AndAlso Not img.PostedFile Is Nothing Then
            Dim File As HttpPostedFile = FromFile.PostedFile

            imgByte = New Byte(File.ContentLength - 1) {}
            File.InputStream.Read(imgByte, 0, File.ContentLength)
        End If
        Return imgByte
    End Function

    Private Sub FillPermissionType()
        objPermissionType = New PermissionsTypes
        Dim dtPermissionType As New DataTable
        With objPermissionType
            dtPermissionType = .GetAll
            If Not dtPermissionType Is Nothing And dtPermissionType.Rows.Count > 0 Then
                CtlCommon.FillTelerikDropDownList(radcmbPersonalPermissionType, .GetAll, Lang)
            End If
        End With
    End Sub

    Private Sub FillLeaveType()
        objLeaveTypes = New LeavesTypes
        Dim dtLeaveTypes As New DataTable

        With objLeaveTypes
            dtLeaveTypes = .GetAll
            If Not dtLeaveTypes Is Nothing And dtLeaveTypes.Rows.Count > 0 Then
                CtlCommon.FillTelerikDropDownList(radcmbMaternityLeaveType, .GetAll, Lang)
            End If

        End With
    End Sub

    Private Sub ChkboxlistEmployeeRequests()
        'Literal1.Text = IIf(Lang = CtlCommon.Lang.AR, "اختيار الكل", "Select All")
        'Literal2.Text = IIf(Lang = CtlCommon.Lang.AR, "عدم اختيار الكل", "UnSelect All")
        Dim item1 As New ListItem
        Dim item2 As New ListItem
        Dim item3 As New ListItem
        Dim item4 As New ListItem
        Dim item5 As New ListItem
        Dim item6 As New ListItem
        Dim item7 As New ListItem
        Dim item8 As New ListItem

        item1.Value = 1
        item1.Text = IIf(Lang = CtlCommon.Lang.AR, "طلبات المغادرات", "Permission Requests")
        cblEployeeRequests.Items.Add(item1)
        item2.Value = 2
        item2.Text = IIf(Lang = CtlCommon.Lang.AR, "طلبات مغادرة الدراسة", "Study Permission Requests")
        cblEployeeRequests.Items.Add(item2)
        item3.Value = 3
        item3.Text = IIf(Lang = CtlCommon.Lang.AR, "طلبات مغادرة الرضاعة", "Nursing Permission Requests")
        cblEployeeRequests.Items.Add(item3)
        item4.Value = 4
        item4.Text = IIf(Lang = CtlCommon.Lang.AR, "طلبات الادخال اليدوي", "Manual Entry Requests")
        cblEployeeRequests.Items.Add(item4)
        item5.Value = 5
        cblEployeeRequests.Items.Add(item5)
        item5.Text = IIf(Lang = CtlCommon.Lang.AR, "طلبات العمل الاضافي", "Overtime Requests")
        item6.Value = 6
        item6.Text = IIf(Lang = CtlCommon.Lang.AR, "اضافة وقت اضافي لموظف", "Add Employee Overtime")
        cblEployeeRequests.Items.Add(item6)
        item7.Value = 7
        item7.Text = IIf(Lang = CtlCommon.Lang.AR, "طلبات الاجازات", "Leave Requests")
        cblEployeeRequests.Items.Add(item7)
        item8.Value = 8
        item8.Text = IIf(Lang = CtlCommon.Lang.AR, "طلبات تعديل الحركة", "Update Transactions Requests")
        cblEployeeRequests.Items.Add(item8)
    End Sub

    Private Sub ChkboxlistDurationTotalsToAppear()
        'Literal1.Text = IIf(Lang = CtlCommon.Lang.AR, "اختيار الكل", "Select All")
        'Literal2.Text = IIf(Lang = CtlCommon.Lang.AR, "عدم اختيار الكل", "UnSelect All")
        Dim item1 As New ListItem
        Dim item2 As New ListItem
        Dim item3 As New ListItem
        Dim item4 As New ListItem
        Dim item5 As New ListItem
        Dim item6 As New ListItem
        Dim item7 As New ListItem
        Dim item8 As New ListItem
        Dim item9 As New ListItem
        Dim item10 As New ListItem
        Dim item11 As New ListItem
        Dim item12 As New ListItem
        Dim item13 As New ListItem
        Dim item14 As New ListItem
        Dim item15 As New ListItem
        Dim item16 As New ListItem
        Dim item17 As New ListItem
        Dim item18 As New ListItem
        Dim item19 As New ListItem

        item1.Value = 1
        item1.Text = IIf(Lang = CtlCommon.Lang.AR, "وقت الدخول", "IN Time")
        cblDurationTotalsToAppear.Items.Add(item1)
        item2.Value = 2
        item2.Text = IIf(Lang = CtlCommon.Lang.AR, "نوع الجدول", "Schedule Type")
        cblDurationTotalsToAppear.Items.Add(item2)
        item3.Value = 3
        item3.Text = IIf(Lang = CtlCommon.Lang.AR, "معلومات الجدول", "Schedule Info")
        cblDurationTotalsToAppear.Items.Add(item3)
        item4.Value = 4
        item4.Text = IIf(Lang = CtlCommon.Lang.AR, "الخروج المتوقع", "Expected Out")
        cblDurationTotalsToAppear.Items.Add(item4)
        item5.Value = 5
        cblDurationTotalsToAppear.Items.Add(item5)
        item5.Text = IIf(Lang = CtlCommon.Lang.AR, "مجموع التأخير", "Total Delay")
        item6.Value = 6
        item6.Text = IIf(Lang = CtlCommon.Lang.AR, "مجموع الخروج المبكر", "Total Early Out")
        cblDurationTotalsToAppear.Items.Add(item6)
        item7.Value = 7
        item7.Text = IIf(Lang = CtlCommon.Lang.AR, "مجموع التأخير و الخروج المبكر", "Total Delay and Early Out")
        cblDurationTotalsToAppear.Items.Add(item7)
        item8.Value = 8
        item8.Text = IIf(Lang = CtlCommon.Lang.AR, "مجموع الوقت الضائع", "Total Lost Time")
        cblDurationTotalsToAppear.Items.Add(item8)
        item9.Value = 9
        item9.Text = IIf(Lang = CtlCommon.Lang.AR, "مجموع الغياب", "Total Absent")
        cblDurationTotalsToAppear.Items.Add(item9)
        item10.Value = 10
        item10.Text = IIf(Lang = CtlCommon.Lang.AR, "الرصيد المتبقي من المغادرة الشخصية", "Personal Permission Balance")
        cblDurationTotalsToAppear.Items.Add(item10)
        item11.Value = 11
        item11.Text = IIf(Lang = CtlCommon.Lang.AR, "مجموع عدم توقيع دخول", "Total Missing In")
        cblDurationTotalsToAppear.Items.Add(item11)
        item12.Value = 12
        item12.Text = IIf(Lang = CtlCommon.Lang.AR, "مجموع عدم توقيع خروج", "Total Missing Out")
        cblDurationTotalsToAppear.Items.Add(item12)
        item13.Value = 13
        item13.Text = IIf(Lang = CtlCommon.Lang.AR, "مجموع ايام عدم إتمام ساعات الدوام الرسمي", "Not completing work hours")
        cblDurationTotalsToAppear.Items.Add(item13)
        item14.Value = 14
        item14.Text = IIf(Lang = CtlCommon.Lang.AR, "المخطط البياني", "Chart")
        cblDurationTotalsToAppear.Items.Add(item14)
        item15.Value = 15
        item15.Text = IIf(Lang = CtlCommon.Lang.AR, "عدد المرات المتبقية من المغادرة الشخصية", "Remaining Times of Personal Permission")
        cblDurationTotalsToAppear.Items.Add(item15)
        item16.Value = 16
        item16.Text = IIf(Lang = CtlCommon.Lang.AR, "أجندة الموظف", "Employee Calendar")
        cblDurationTotalsToAppear.Items.Add(item16)
        item17.Value = 17
        item17.Text = IIf(Lang = CtlCommon.Lang.AR, "مخطط ساعات العمل", "Work Hours Chart")
        cblDurationTotalsToAppear.Items.Add(item17)
        item18.Value = 18
        item18.Text = IIf(Lang = CtlCommon.Lang.AR, "رصيد الوقت الاضافي المتبقي", "Remaining OverTime Balance")
        cblDurationTotalsToAppear.Items.Add(item18)
        item19.Value = 19
        item19.Text = IIf(Lang = CtlCommon.Lang.AR, "رقم بطاقة الموظف", "Employee Card No")
        cblDurationTotalsToAppear.Items.Add(item19)

    End Sub

    Private Sub FillRemoteWorkTAReason()
        objTA_Reason = New TA_Reason
        Dim dtTAReason As DataTable
        With objTA_Reason
            dtTAReason = .GetAll
        End With
        If Not dtTAReason Is Nothing Then
            If dtTAReason.Rows.Count > 0 Then

                Dim dtSource As New DataTable
                dtSource.Columns.Add("ReasonCode")
                dtSource.Columns.Add("ReasonName")
                Dim drRow As DataRow
                drRow = dtSource.NewRow()
                For Item As Integer = 0 To dtTAReason.Rows.Count - 1

                    Dim drSource As DataRow
                    drSource = dtSource.NewRow
                    Dim dcCell1 As New DataColumn
                    Dim dcCell2 As New DataColumn
                    dcCell1.ColumnName = "ReasonCode"
                    dcCell2.ColumnName = "ReasonName"
                    dcCell1.DefaultValue = dtTAReason.Rows(Item)(0)
                    dcCell2.DefaultValue = IIf(Lang = CtlCommon.Lang.EN, dtTAReason.Rows(Item)("ReasonName"), dtTAReason.Rows(Item)("ReasonArabicName"))
                    drSource("ReasonCode") = dcCell1.DefaultValue
                    drSource("ReasonName") = dcCell2.DefaultValue
                    dtSource.Rows.Add(drSource)

                Next
                Dim dv As New DataView(dtSource)
                If SessionVariables.CultureInfo = "ar-JO" Then
                    For Each row As DataRowView In dv
                        cblRemoteWorkTAReason.Items.Add(New ListItem(row("ReasonName").ToString(), row("ReasonCode").ToString()))
                    Next
                Else
                    For Each row As DataRowView In dv
                        cblRemoteWorkTAReason.Items.Add(New ListItem(row("ReasonName").ToString(), row("ReasonCode").ToString()))
                    Next
                End If
            End If
        End If

    End Sub

    Private Sub Enable_Disable_MobileFeatures()

        objVersion = New SmartV.Version.version
        If objVersion.HasTawajudFeatures = True Then
            dvHasTawajudFeatures.Visible = True
        Else
            dvHasTawajudFeatures.Visible = False
        End If

        If objVersion.HasMultiLocations = True Then
            dvHasMultiLocations.Visible = True
        Else
            dvHasMultiLocations.Visible = False
        End If

        If objVersion.HasHeartBeat = True Then
            dvHasHeartBeat.Visible = True
        Else
            dvHasHeartBeat.Visible = False
        End If

        If objVersion.HasFeedback = True Then
            dvHasFeedback.Visible = True
        Else
            dvHasFeedback.Visible = False
        End If

        If objVersion.AllowFingerPunch = True Then
            dvAllowFingerPunch.Visible = True
        Else
            dvAllowFingerPunch.Visible = False
        End If

        If objVersion.AllowFingerLogin = True Then
            dvAllowFingerLogin.Visible = True
        Else
            dvAllowFingerLogin.Visible = False
        End If

        If objVersion.AllowFacePunch = True Then
            dvAllowFacePunch.Visible = True
        Else
            dvAllowFacePunch.Visible = False
        End If

        If objVersion.AllowFaceLogin = True Then
            dvAllowFaceLogin.Visible = True
        Else
            dvAllowFaceLogin.Visible = False
        End If

        If objVersion.HasTawajudFeatures = True Or objVersion.HasMultiLocations = True Or objVersion.HasHeartBeat = True Or objVersion.HasFeedback = True Or objVersion.AllowFingerPunch = True Or objVersion.AllowFingerLogin = True Or objVersion.AllowFacePunch = True Or objVersion.AllowFaceLogin = True Then
            dvMobileSettings.Visible = True
        Else
            dvMobileSettings.Visible = False
        End If



    End Sub


    Private Function ValidateForms(ByVal FormId As Integer) As Boolean
        objSYSForms = New SYSForms
        Dim strForms As String = SessionVariables.LicenseDetails.FormIds
        Dim arrForms As ArrayList = SplitLicenseForms(strForms)
        Dim LicVerifiedurl As Boolean = False

        Dim i As Integer
        For i = 0 To arrForms.Count - 1
            If FormId = arrForms.Item(i) Then
                LicVerifiedurl = True
            End If
        Next

        Return LicVerifiedurl
    End Function

    Public Function SplitLicenseForms(ByVal LicenseForms As String) As ArrayList

        Dim s As String = LicenseForms
        Dim arrForms As New ArrayList
        For Each value As String In s.Split(","c)
            arrForms.Add(Convert.ToInt32(value))
        Next
        Return arrForms
    End Function

#End Region


End Class