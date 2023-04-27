Imports TA.Employees
Imports System.Data
Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports TA.Admin
Imports TA.Lookup
Imports TA.DailyTasks
Imports TA.Security

Partial Class Admin_Default3
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objArray As New ArrayList
    Dim objTAPolicy_AbsentRule As New TAPolicy_AbsentRule
    Dim objTAPolicy_Violation As New TAPolicy_Violation
    Dim objTAPolicy_ViolationActions As New TAPolicy_ViolationActions
    Private objApp_Settings As APP_Settings
    Private objTAPolicy As TAPolicy
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Private objCommonProject As New ProjectCommon()
    Dim objTA_Reason As TA_Reason
    Private objTAPolicy_DeductionPolicy As TAPolicy_DeductionPolicy
    Public MsgLang As String
#End Region

#Region "Properties"

    Public Property Violationdt() As DataTable
        Get
            Return ViewState("Violationdt")
        End Get
        Set(ByVal value As DataTable)
            ViewState("Violationdt") = value
        End Set
    End Property

    Private Property ViolationId() As Integer
        Get
            Return ViewState("ViolationId")
        End Get
        Set(ByVal value As Integer)
            ViewState("ViolationId") = value
        End Set
    End Property

    Public Property AbsentRuledt() As DataTable
        Get
            Return ViewState("AbsentRuledt")
        End Get
        Set(ByVal value As DataTable)
            ViewState("AbsentRuledt") = value
        End Set
    End Property

    Private Property AbsentRuleId() As Integer
        Get
            Return ViewState("AbsentRuleId")
        End Get
        Set(ByVal value As Integer)
            ViewState("AbsentRuleId") = value
        End Set
    End Property

    Public Property TAPolicyID() As Integer
        Get
            Return ViewState("TAPolicyID")
        End Get
        Set(ByVal value As Integer)
            ViewState("TAPolicyID") = value
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

    Public Property DeductionPolicyId() As Integer
        Get
            Return ViewState("DeductionPolicyId")
        End Get
        Set(ByVal value As Integer)
            ViewState("DeductionPolicyId") = value
        End Set
    End Property

#End Region

#Region "Page events"

    Protected Sub dgrdTAPolicy_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdTAPolicy.Skin))
    End Function

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        Else
            If SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                Page.MasterPageFile = "~/default/ArabicMaster.master"
                MsgLang = "ar"
            Else
                Lang = CtlCommon.Lang.EN
                Page.MasterPageFile = "~/default/NewMaster.master"
                MsgLang = "en"
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

            PrepareAbsetRuleInterface()
            PrepareViolationInterface()
            createAbsentRuledt()
            createViolationdt()
            FillGrid()
            FillReasons()
            HasLaunchBreak()
            HasPrayTime()
            CtlCommon.FillTelerikDropDownList(ddlViolationAction, objTAPolicy_ViolationActions.GetAllForDDL(), Lang)
            CtlCommon.FillTelerikDropDownList(ddlViolationAction2, objTAPolicy_ViolationActions.GetAllForDDL(), Lang)
            CtlCommon.FillTelerikDropDownList(ddlViolationAction3, objTAPolicy_ViolationActions.GetAllForDDL(), Lang)
            CtlCommon.FillTelerikDropDownList(ddlViolationAction4, objTAPolicy_ViolationActions.GetAllForDDL(), Lang)
            CtlCommon.FillTelerikDropDownList(ddlViolationAction5, objTAPolicy_ViolationActions.GetAllForDDL(), Lang)
            PageHeader1.HeaderText = ResourceManager.GetString("DefTAPolicy", CultureInfo)
            RequiredFieldValidator1.InitialValue = ResourceManager.GetString("PleaseSelect", CultureInfo)
            reqViolationRuleType.InitialValue = ResourceManager.GetString("PleaseSelect", CultureInfo)
            reqViolationAction.InitialValue = ResourceManager.GetString("PleaseSelect", CultureInfo)
        End If

        objApp_Settings = New APP_Settings()
        objApp_Settings.GetByPK()
        If Not objApp_Settings.IsGraceTAPolicy Then
            trGraceIn.Visible = False
            trGraceOut.Visible = False
        End If


        ibtnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdTAPolicy.ClientID + "')")
        btnRemove.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdAbsentRules.ClientID + "')")
        btnRemoveViolation.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdViolation.ClientID + "')")

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

    Protected Sub ibtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnSave.Click
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        objTAPolicy = New TAPolicy
        Dim err As Integer

        Dim isaddaction As Boolean = False

        With objTAPolicy
            .TAPolicyName = txtPolicyEnglish.Text
            .TAPolicyArabicName = txtPolicyArabic.Text
            If (trGraceIn.Visible) Then
                .GraceInMins = txtGraceIn.Text
            End If
            If (trGraceOut.Visible) Then
                .GraceOutMins = txtGraceOut.Text
            End If
            .HasLaunchBreak = chkHasLaunchBreak.Checked
            If chkHasLaunchBreak.Checked Then
                .LaunchBreakDuration = txtLaunchBreakDuration.Text
                .CompensateLaunchbreak = chkCompensateLaunchbreak.Checked
                .FK_LaunchBreakReason = ddlLaunchbreakReason.SelectedValue

                .HasReasonBreakTime = chkHasReasonBreakTime.Checked
                .MonthlyAllowedGraceOutTime = radnumtxtMonthlyBreak.Text
                Dim strLunchBreakBetweenFromTime As String = (CInt(rmtLaunchBreakFromTime.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtLaunchBreakFromTime.TextWithLiterals.Split(":")(1))
                .LaunchBreakFromTime = strLunchBreakBetweenFromTime

                Dim strLaunchBreakToTime As String = (CInt(rmtLaunchBreakToTime.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtLaunchBreakToTime.TextWithLiterals.Split(":")(1))
                .LaunchBreakToTime = strLaunchBreakToTime

            End If

            .HasPrayTime = chkHasPrayTime.Checked
            If chkHasPrayTime.Checked Then
                .NoOfAllowedPrays = txtNoOfAllowedPrays.Text
                .PrayBreakFromTime = (CInt(rmtPrayBreakFromTime.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtPrayBreakFromTime.TextWithLiterals.Split(":")(1))
                .PrayBreakToTime = (CInt(rmtPrayBreakToTime.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtPrayBreakToTime.TextWithLiterals.Split(":")(1))
                .PrayTimeDuration = txtPrayTimeDuration.Text
                .CompensatePrayTime = chkCompensatePrayTime.Checked
                .FK_PrayTimeReason = ddlPrayTimeReason.SelectedValue
            End If

            .DelayIsFromGrace = chkDelalIsFromGrace.Checked
            .EarlyOutIsFromGrace = chkEarlyOutisFromGrace.Checked
            .Active = True 'chkActive.Checked
            .ConsiderFirstIn_LastOutOnly = chkFirstInLastOut.Checked
            .MinDurationAsViolation = radminDuration.Text
            If chkEarlyOutisFromGrace.Checked = False Then
                .IgnoreEarlyOut_WithinGrace = False
            Else
                .IgnoreEarlyOut_WithinGrace = chkIgnoreEarlyOut_WithinGrace.Checked
            End If
            .ConsiderAbsentIfNotCompleteNoOfHours = chkConsiderAbsent_IfNotCompleteNoHours.Checked

            If chkConsiderAbsent_IfNotCompleteNoHours.Checked Then
                .NoOfNotCompleteHours = (CInt(rmtNoOfHours.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtNoOfHours.TextWithLiterals.Split(":")(1))
            End If

            .ConsiderAbsentIfStudyNurs_NotCompleteHrs = chkConsiderAbsentIfStudyNurs_NotCompleteHrs.Checked

            If chkConsiderAbsentIfStudyNurs_NotCompleteHrs.Checked Then
                .NoOfNotCompleteHours_StudyNurs = (CInt(rmtNoOfHours_StudyNurs_NotCompleteHrs.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtNoOfHours_StudyNurs_NotCompleteHrs.TextWithLiterals.Split(":")(1))
            End If

            If TAPolicyID = 0 Then
                isaddaction = True
                .CREATED_BY = ""
                err = objTAPolicy.Add()
                If err = 0 Then
                    TAPolicyID = .TAPolicyId
                End If
            Else
                .TAPolicyId = TAPolicyID
                .LAST_UPDATE_BY = ""
                err = objTAPolicy.Update()
            End If
        End With
        If err = 0 Then
            SaveDeduction()
        End If
        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            objTAPolicy_AbsentRule = New TAPolicy_AbsentRule
            If isaddaction = True Then

                ' If Not AbsentRuledt Is Nothing AndAlso AbsentRuledt.Rows.Count > 0 Then
                With objTAPolicy_AbsentRule
                    .Add_Bulk(AbsentRuledt, TAPolicyID)
                End With
                With objTAPolicy_Violation
                    .Add_Bulk(Violationdt, TAPolicyID)
                End With
                ' End If
            End If

            FillGrid()
            ClearAll()
        ElseIf err = -5 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EnNameExist", CultureInfo), "info")
        ElseIf err = -6 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "info")
        End If

    End Sub

    Protected Sub ibtnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnDelete.Click
        Dim err As Integer

        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdTAPolicy.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                objTAPolicy = New TAPolicy
                objTAPolicy.TAPolicyId = Convert.ToInt32(row.GetDataKeyValue("TAPolicyId").ToString())
                err = objTAPolicy.Delete()

            End If
        Next

        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillGrid()
            ClearAll()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")
        End If


    End Sub

    Protected Sub ibtnRest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnRest.Click
        ClearAll()
        FillGrid()
    End Sub

    Protected Sub dgrdTAPolicy_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdTAPolicy.NeedDataSource
        objTAPolicy = New TAPolicy
        dgrdTAPolicy.DataSource = objTAPolicy.GetAll()
    End Sub

    Protected Sub dgrdTAPolicy_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdTAPolicy.SelectedIndexChanged

        TAPolicyID = Convert.ToInt32(DirectCast(dgrdTAPolicy.SelectedItems(0), GridDataItem).GetDataKeyValue("TAPolicyId").ToString())
        objTAPolicy = New TAPolicy
        With objTAPolicy
            .TAPolicyId = TAPolicyID
            .GetByPK()
            txtGraceIn.Text = .GraceInMins
            txtPolicyEnglish.Text = .TAPolicyName
            txtPolicyArabic.Text = .TAPolicyArabicName
            txtGraceOut.Text = .GraceOutMins
            'chkActive.Checked = .Active
            chkEarlyOutisFromGrace.Checked = .EarlyOutIsFromGrace
            chkDelalIsFromGrace.Checked = .DelayIsFromGrace
            chkHasLaunchBreak.Checked = .HasLaunchBreak
            chkHasReasonBreakTime.Checked = .HasReasonBreakTime
            chkHasPrayTime.Checked = .HasPrayTime
            HasLaunchBreak()
            HasPrayTime()
            If chkHasLaunchBreak.Checked Then
                txtLaunchBreakDuration.Text = .LaunchBreakDuration
                chkCompensateLaunchbreak.Checked = .CompensatePrayTime
                ddlLaunchbreakReason.SelectedValue = .FK_LaunchBreakReason
                chkHasReasonBreakTime.Checked = .HasReasonBreakTime
                rmtLaunchBreakFromTime.Text = CtlCommon.GetFullTimeString(.LaunchBreakFromTime)
                rmtLaunchBreakToTime.Text = CtlCommon.GetFullTimeString(.LaunchBreakToTime)
                radnumtxtMonthlyBreak.Text = .MonthlyAllowedGraceOutTime
            End If
            If chkHasPrayTime.Checked Then
                txtPrayTimeDuration.Text = .PrayTimeDuration
                chkCompensatePrayTime.Checked = .CompensatePrayTime
                ddlPrayTimeReason.SelectedValue = .FK_PrayTimeReason
                txtNoOfAllowedPrays.Text = .NoOfAllowedPrays
                rmtPrayBreakFromTime.Text = CtlCommon.GetFullTimeString(.PrayBreakFromTime)
                rmtPrayBreakToTime.Text = CtlCommon.GetFullTimeString(.PrayBreakToTime)
            End If
            chkFirstInLastOut.Checked = .ConsiderFirstIn_LastOutOnly
            If Not .MinDurationAsViolation = Nothing Then
                radminDuration.Text = .MinDurationAsViolation
            End If
            If chkEarlyOutisFromGrace.Checked = True Then
                dvIgnoreEarlyOut_WithinGrace.Visible = True
                chkIgnoreEarlyOut_WithinGrace.Checked = .IgnoreEarlyOut_WithinGrace
            Else
                dvIgnoreEarlyOut_WithinGrace.Visible = False
            End If

            chkConsiderAbsent_IfNotCompleteNoHours.Checked = .ConsiderAbsentIfNotCompleteNoOfHours
            If .ConsiderAbsentIfNotCompleteNoOfHours Then
                dvConsiderAbsent_IfNotCompleteNoHours.Visible = True
                rmtNoOfHours.Text = CtlCommon.GetFullTimeString(.NoOfNotCompleteHours)
            Else
                dvConsiderAbsent_IfNotCompleteNoHours.Visible = False
                rmtNoOfHours.Text = "0000"
            End If

            chkConsiderAbsentIfStudyNurs_NotCompleteHrs.Checked = .ConsiderAbsentIfStudyNurs_NotCompleteHrs
            If .ConsiderAbsentIfStudyNurs_NotCompleteHrs Then
                dvConsiderAbsentIfStudyNurs_NotCompleteHrs.Visible = True
                rmtNoOfHours_StudyNurs_NotCompleteHrs.Text = CtlCommon.GetFullTimeString(.NoOfNotCompleteHours_StudyNurs)
            Else
                dvConsiderAbsentIfStudyNurs_NotCompleteHrs.Visible = False
                rmtNoOfHours_StudyNurs_NotCompleteHrs.Text = "0000"
            End If

            clearAbsentRule()
            fillAbsentRules()
            clearViolation()
            fillViolation()
            FillDeduction()
        End With
    End Sub

    Protected Sub chkHasLaunchBreak_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkHasLaunchBreak.CheckedChanged
        HasLaunchBreak()
    End Sub

    Protected Sub chkHasPrayTime_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkHasPrayTime.CheckedChanged
        HasPrayTime()
    End Sub

    Protected Sub chkHasReasonBreakTime_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkHasReasonBreakTime.CheckedChanged
        If chkHasReasonBreakTime.Checked Then
            trLaunchbreakReason.Visible = True
        Else
            trLaunchbreakReason.Visible = False
        End If
    End Sub

    Protected Sub chkEarlyOutisFromGrace_CheckedChanged(sender As Object, e As EventArgs) Handles chkEarlyOutisFromGrace.CheckedChanged
        If chkEarlyOutisFromGrace.Checked = True Then
            dvIgnoreEarlyOut_WithinGrace.Visible = True
        Else
            dvIgnoreEarlyOut_WithinGrace.Visible = False
        End If
    End Sub

    Protected Sub chkConsiderAbsent_IfNotCompleteNoHours_CheckedChanged(sender As Object, e As EventArgs) Handles chkConsiderAbsent_IfNotCompleteNoHours.CheckedChanged
        If chkConsiderAbsent_IfNotCompleteNoHours.Checked = True Then
            dvConsiderAbsent_IfNotCompleteNoHours.Visible = True
        Else
            dvConsiderAbsent_IfNotCompleteNoHours.Visible = False
        End If
    End Sub

    Protected Sub chkConsiderAbsentIfStudyNurs_NotCompleteHrs_CheckedChanged(sender As Object, e As EventArgs) Handles chkConsiderAbsentIfStudyNurs_NotCompleteHrs.CheckedChanged
        If chkConsiderAbsentIfStudyNurs_NotCompleteHrs.Checked = True Then
            dvConsiderAbsentIfStudyNurs_NotCompleteHrs.Visible = True
        Else
            dvConsiderAbsentIfStudyNurs_NotCompleteHrs.Visible = False
        End If
    End Sub

#End Region

#Region "Methods"

    Public Sub FillGrid()
        Try
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            objTAPolicy = New TAPolicy
            dgrdTAPolicy.DataSource = objTAPolicy.GetAll()
            dgrdTAPolicy.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Public Sub ClearAll()
        ' Clear controls
        txtGraceIn.Text = String.Empty
        txtPolicyEnglish.Text = String.Empty
        txtPolicyArabic.Text = String.Empty
        txtGraceOut.Text = String.Empty
        rmtLaunchBreakFromTime.Text = CtlCommon.GetFullTimeString(0)
        rmtLaunchBreakToTime.Text = CtlCommon.GetFullTimeString(0)
        'chkActive.Checked = False
        chkDelalIsFromGrace.Checked = False
        chkEarlyOutisFromGrace.Checked = False
        ' Refill GridView
        'FillGrid()
        ' Reset variables
        TAPolicyID = 0
        clearAbsentRule()
        clearViolation()
        ClearDeduction()
        createAbsentRuledt()
        createViolationdt()
        chkHasLaunchBreak.Checked = False
        chkHasPrayTime.Checked = False
        txtLaunchBreakDuration.Text = String.Empty
        txtPrayTimeDuration.Text = String.Empty
        ddlLaunchbreakReason.ClearSelection()
        ddlPrayTimeReason.ClearSelection()
        chkCompensateLaunchbreak.Checked = False
        chkCompensatePrayTime.Checked = False
        trLaunchBreak.Visible = False
        trHasReasonBreakTime.Visible = False
        trLunchBreakBetween.Visible = False
        reqLaunchBreakDuration.Enabled = False
        trLaunchbreakReason.Visible = False
        reqLaunchbreakReason.Enabled = False
        trCompensateLaunchbreak.Visible = False
        trPrayTime.Visible = False
        trPrayTimeReason.Visible = False
        trCompensatePrayTime.Visible = False
        reqPrayTimeDuration.Enabled = False
        reqPrayTimeReason.Enabled = False
        chkFirstInLastOut.Checked = False
        radminDuration.Text = String.Empty
        dvMonthlyBreak.Visible = False
        dvIgnoreEarlyOut_WithinGrace.Visible = False
        chkIgnoreEarlyOut_WithinGrace.Checked = False
        chkConsiderAbsent_IfNotCompleteNoHours.Checked = False
        dvConsiderAbsent_IfNotCompleteNoHours.Visible = False
        rmtNoOfHours.Text = "0000"
        chkConsiderAbsentIfStudyNurs_NotCompleteHrs.Checked = False
        dvConsiderAbsentIfStudyNurs_NotCompleteHrs.Visible = False
        rmtNoOfHours_StudyNurs_NotCompleteHrs.Text = "0000"

        dvPrayBetween.Visible = False
        dvNoOfAllowedPrays.Visible = False
        rfvNoOfAllowedPrays.Enabled = False
        rmtPrayBreakFromTime.Text = CtlCommon.GetFullTimeString(0)
        rmtPrayBreakToTime.Text = CtlCommon.GetFullTimeString(0)

    End Sub

    Sub HasLaunchBreak()

        If chkHasLaunchBreak.Checked Then
            trLaunchBreak.Visible = True
            reqLaunchBreakDuration.Enabled = True
            'trLaunchbreakReason.Visible = True
            reqLaunchbreakReason.Enabled = True
            trCompensateLaunchbreak.Visible = True
            trHasReasonBreakTime.Visible = True
            trLunchBreakBetween.Visible = True
            dvMonthlyBreak.Visible = True
        Else
            trLaunchBreak.Visible = False
            reqLaunchBreakDuration.Enabled = False
            'trLaunchbreakReason.Visible = False
            reqLaunchbreakReason.Enabled = False
            trCompensateLaunchbreak.Visible = False
            trHasReasonBreakTime.Visible = False
            trLunchBreakBetween.Visible = False
            dvMonthlyBreak.Visible = False
        End If
        If chkHasReasonBreakTime.Checked Then
            trLaunchbreakReason.Visible = True
        Else
            trLaunchbreakReason.Visible = False
        End If
    End Sub

    Sub HasPrayTime()
        If chkHasPrayTime.Checked Then
            trPrayTime.Visible = True
            trPrayTimeReason.Visible = True
            trCompensatePrayTime.Visible = True
            reqPrayTimeDuration.Enabled = True
            reqPrayTimeReason.Enabled = True
            dvNoOfAllowedPrays.Visible = True
            dvPrayBetween.Visible = True
            rfvNoOfAllowedPrays.Enabled = True
        Else
            trPrayTime.Visible = False
            trPrayTimeReason.Visible = False
            trCompensatePrayTime.Visible = False
            reqPrayTimeDuration.Enabled = False
            reqPrayTimeReason.Enabled = False
            dvNoOfAllowedPrays.Visible = False
            dvPrayBetween.Visible = False
            rfvNoOfAllowedPrays.Enabled = False
        End If
    End Sub

    Private Sub FillReasons()
        objTA_Reason = New TA_Reason()
        CtlCommon.FillTelerikDropDownList(ddlLaunchbreakReason, objTA_Reason.GetAll, Lang)
        CtlCommon.FillTelerikDropDownList(ddlPrayTimeReason, objTA_Reason.GetAll, Lang)
    End Sub

#End Region

#Region "Absent Rules"

    Sub PrepareAbsetRuleInterface()
        txtVar1.Text = ""
        txtVar2.Text = ""
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Select Case ddlAbsentRuleType.SelectedValue

            Case 1
                lblVariable1.Text = ResourceManager.GetString("DelayInMin", CultureInfo)
                lblVariable2.Text = ""
                RequiredFieldValidator4.ErrorMessage = ResourceManager.GetString("PleaseDelayInMin", CultureInfo)
                txtVar1.Visible = True
                txtVar2.Visible = False

                '-------Old Values-------'
                'lblVariable1.Text = ResourceManager.GetString("DelayInMin", CultureInfo)
                'lblVariable2.Text = ResourceManager.GetString("TotalDelayInMin", CultureInfo)
                'RequiredFieldValidator4.ErrorMessage = ResourceManager.GetString("PleaseDelayInMin", CultureInfo)
                'txtVar1.Visible = True
                'txtVar2.Visible = True
                '-------Old Values-------'

            Case 2
                lblVariable1.Text = ResourceManager.GetString("EarlyOutInMin", CultureInfo)
                lblVariable2.Text = ""
                RequiredFieldValidator4.ErrorMessage = ResourceManager.GetString("PleaseEarlyOutInMin", CultureInfo)
                txtVar1.Visible = True
                txtVar2.Visible = False

                '-------Old Values-------'
                'lblVariable1.Text = ResourceManager.GetString("DelayInDays", CultureInfo)
                'lblVariable2.Text = ""
                'RequiredFieldValidator4.ErrorMessage = ResourceManager.GetString("PleaseDelayInDays", CultureInfo)
                'txtVar1.Visible = True
                'txtVar2.Visible = False
                '-------Old Values-------'
            Case 3

                lblVariable1.Text = ResourceManager.GetString("DelayandEarlyOutInMin", CultureInfo)
                RequiredFieldValidator4.ErrorMessage = ResourceManager.GetString("PleaseDelayandEarlyOut", CultureInfo)
                lblVariable2.Text = ""
                txtVar1.Visible = True
                txtVar2.Visible = False
                '-------Old Values-------'
                'lblVariable1.Text = ResourceManager.GetString("DelayInDays", CultureInfo)
                'RequiredFieldValidator4.ErrorMessage = ResourceManager.GetString("PleaseDelayInDays", CultureInfo)
                'lblVariable2.Text = ResourceManager.GetString("ConsiderPeriodInDays", CultureInfo)
                'txtVar1.Visible = True
                'txtVar2.Visible = True
                '-------Old Values-------'
            Case Else
                lblVariable1.Text = ""
                lblVariable2.Text = ""
                txtVar1.Visible = False
                txtVar2.Visible = False


        End Select



    End Sub

    Sub createAbsentRuledt()
        AbsentRuledt = New DataTable
        Dim dc As DataColumn
        dc = New DataColumn
        dc.ColumnName = "AbsentRuleId"
        dc.DataType = GetType(Integer)
        AbsentRuledt.Columns.Add(dc)
        AbsentRuledt.Columns("AbsentRuleId").AutoIncrement = True
        AbsentRuledt.Columns("AbsentRuleId").AutoIncrementSeed = 1
        AbsentRuledt.Columns("AbsentRuleId").AutoIncrementStep = 1

        dc = New DataColumn
        dc.ColumnName = "RuleName"
        dc.DataType = GetType(String)
        AbsentRuledt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "RuleArabicName"
        dc.DataType = GetType(String)
        AbsentRuledt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "AbsentRuleType"
        dc.DataType = GetType(Integer)
        AbsentRuledt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "Variable1"
        dc.DataType = GetType(Integer)
        AbsentRuledt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "Variable2"
        dc.DataType = GetType(Integer)
        AbsentRuledt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "FK_TAPolicyId"
        dc.DataType = GetType(Integer)
        AbsentRuledt.Columns.Add(dc)


    End Sub

    Protected Sub ddlAbsentRuleType_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlAbsentRuleType.SelectedIndexChanged
        PrepareAbsetRuleInterface()
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim err As Integer = -1
        Dim dr As DataRow
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Try
            If ifexistAbsentRule(txtAbsentRuleEnglishName.Text, txtAbsentRuleArabichName.Text, AbsentRuleId) Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NameExist", CultureInfo), "info")
                Exit Sub
            End If
            If TAPolicyID = 0 Then
                If AbsentRuleId = 0 Then

                    dr = AbsentRuledt.NewRow
                    ' dr("AbsentRuleId") = AbsentRuleId
                    dr("FK_TAPolicyId") = TAPolicyID
                    dr("RuleName") = txtAbsentRuleEnglishName.Text
                    dr("RuleArabicName") = txtAbsentRuleArabichName.Text
                    dr("AbsentRuleType") = ddlAbsentRuleType.SelectedValue
                    If txtVar1.Text = "" Then
                        dr("Variable1") = 0
                    Else
                        dr("Variable1") = txtVar1.Text
                    End If
                    If txtVar2.Text = "" Then
                        dr("Variable2") = 0
                    Else
                        dr("Variable2") = txtVar2.Text
                    End If

                    AbsentRuledt.Rows.Add(dr)
                Else
                    If AbsentRuledt.Rows.Count > 0 Then

                        dr = AbsentRuledt.Select("AbsentRuleId= " & AbsentRuleId)(0)
                        dr("AbsentRuleId") = AbsentRuleId
                        dr("FK_TAPolicyId") = TAPolicyID
                        dr("RuleName") = txtAbsentRuleEnglishName.Text
                        dr("RuleArabicName") = txtAbsentRuleArabichName.Text
                        dr("AbsentRuleType") = ddlAbsentRuleType.SelectedValue
                        If txtVar1.Text = "" Then
                            dr("Variable1") = 0
                        Else
                            dr("Variable1") = txtVar1.Text
                        End If
                        If txtVar2.Text = "" Then
                            dr("Variable2") = 0
                        Else
                            dr("Variable2") = txtVar2.Text
                        End If

                    End If
                End If
            Else
                If AbsentRuleId = 0 Then
                    objTAPolicy_AbsentRule = New TAPolicy_AbsentRule
                    With objTAPolicy_AbsentRule
                        .FK_TAPolicyId = TAPolicyID
                        .RuleName = txtAbsentRuleEnglishName.Text
                        .RuleArabicName = txtAbsentRuleArabichName.Text
                        .AbsentRuleType = ddlAbsentRuleType.SelectedValue
                        If txtVar1.Text = "" Then
                            .Variable1 = 0
                        Else
                            .Variable1 = txtVar1.Text
                        End If
                        If txtVar2.Text = "" Then
                            .Variable2 = 0
                        Else
                            .Variable2 = txtVar2.Text
                        End If
                        .Add()
                        fillAbsentRules()
                    End With
                Else
                    objTAPolicy_AbsentRule = New TAPolicy_AbsentRule
                    With objTAPolicy_AbsentRule
                        .AbsentRuleId = AbsentRuleId
                        .FK_TAPolicyId = TAPolicyID
                        .RuleName = txtAbsentRuleEnglishName.Text
                        .RuleArabicName = txtAbsentRuleArabichName.Text
                        .AbsentRuleType = ddlAbsentRuleType.SelectedValue
                        If txtVar1.Text = "" Then
                            .Variable1 = 0
                        Else
                            .Variable1 = txtVar1.Text
                        End If
                        If txtVar2.Text = "" Then
                            .Variable2 = 0
                        Else
                            .Variable2 = txtVar2.Text
                        End If
                        .Update()
                        fillAbsentRules()
                    End With
                End If
            End If

            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            dgrdAbsentRules.DataSource = AbsentRuledt
            dgrdAbsentRules.DataBind()
            AbsentRuleId = 0
            txtAbsentRuleEnglishName.Text = ""
            txtAbsentRuleArabichName.Text = ""
            ddlAbsentRuleType.SelectedIndex = -1
            txtVar1.Text = ""
            txtVar2.Text = ""
            lblVariable1.Text = ""
            lblVariable2.Text = ""
            txtVar1.Visible = False
            txtVar2.Visible = False
        Catch ex As Exception
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorAdd", CultureInfo), "error")

        End Try
    End Sub

    Sub clearAbsentRule()
        AbsentRuledt = New DataTable
        dgrdAbsentRules.DataSource = AbsentRuledt
        dgrdAbsentRules.DataBind()
        AbsentRuleId = 0
        txtAbsentRuleEnglishName.Text = ""
        txtAbsentRuleArabichName.Text = ""
        ddlAbsentRuleType.SelectedIndex = -1
        txtVar1.Text = ""
        txtVar2.Text = ""
        lblVariable1.Text = ""
        lblVariable2.Text = ""
        txtVar1.Visible = False
        txtVar2.Visible = False

    End Sub

    Protected Sub dgrdAbsentRules_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdAbsentRules.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim RuleTypeID = DirectCast(DirectCast(DirectCast(DirectCast(DirectCast(e.Item.DataItem, System.Object), System.Data.DataRowView), System.Data.DataRowView).Row, System.Data.DataRow), System.Data.DataRow).ItemArray(4)
            '-------Old Values-------'
            'e.Item.Cells(10).Text = If(RuleTypeID = 1, ResourceManager.GetString("OneDayDelay", CultureInfo), If(RuleTypeID = 2, ResourceManager.GetString("ConsecutiveDelay", CultureInfo), If(RuleTypeID = 3, ResourceManager.GetString("Noofdelaysperperiod", CultureInfo), "")))
            '-------Old Values-------'
            e.Item.Cells(10).Text = If(RuleTypeID = 1, ResourceManager.GetString("OneDayDelay", CultureInfo), If(RuleTypeID = 2, ResourceManager.GetString("OneDayEarly", CultureInfo), If(RuleTypeID = 3, ResourceManager.GetString("OneDayDelayandEarly", CultureInfo), "")))

        End If
    End Sub

    Protected Sub dgrdAbsentRules_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdAbsentRules.NeedDataSource
        dgrdAbsentRules.DataSource = AbsentRuledt
    End Sub

    Protected Sub dgrdAbsentRules_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdAbsentRules.SelectedIndexChanged
        Try

            Dim objTAPolicy_AbsentRule As New TAPolicy_AbsentRule
            Dim dt As DataTable = AbsentRuledt
            AbsentRuleId = CInt(CType(dgrdAbsentRules.SelectedItems(0), GridDataItem).GetDataKeyValue("AbsentRuleId").ToString())
            objTAPolicy_AbsentRule.AbsentRuleId = AbsentRuleId
            txtAbsentRuleEnglishName.Text = CType(dgrdAbsentRules.SelectedItems(0), GridDataItem).GetDataKeyValue("RuleName").ToString()
            txtAbsentRuleArabichName.Text = CType(dgrdAbsentRules.SelectedItems(0), GridDataItem).GetDataKeyValue("RuleArabicName").ToString()
            ddlAbsentRuleType.SelectedValue = CInt(CType(dgrdAbsentRules.SelectedItems(0), GridDataItem).GetDataKeyValue("AbsentRuleType").ToString())
            PrepareAbsetRuleInterface()
            If CType(dgrdAbsentRules.SelectedItems(0), GridDataItem).GetDataKeyValue("Variable1").ToString() <> "" Then
                txtVar1.Text = CInt(CType(dgrdAbsentRules.SelectedItems(0), GridDataItem).GetDataKeyValue("Variable1").ToString())
            End If
            If CType(dgrdAbsentRules.SelectedItems(0), GridDataItem).GetDataKeyValue("Variable2").ToString() <> "" Then
                txtVar2.Text = CInt(CType(dgrdAbsentRules.SelectedItems(0), GridDataItem).GetDataKeyValue("Variable2").ToString())
            End If


        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Dim strBuilder As New StringBuilder
        If TAPolicyID = 0 Then
            For Each row As GridDataItem In dgrdAbsentRules.Items
                If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                    Dim intAbsentRuleId As Integer = Convert.ToInt32(row.GetDataKeyValue("AbsentRuleId").ToString())
                    AbsentRuledt.Rows.Remove(AbsentRuledt.Select("AbsentRuleId = " & intAbsentRuleId)(0))
                End If
                dgrdAbsentRules.DataSource = AbsentRuledt
                dgrdAbsentRules.DataBind()
                AbsentRuleId = 0
                txtAbsentRuleEnglishName.Text = ""
                txtAbsentRuleArabichName.Text = ""
                ddlAbsentRuleType.SelectedIndex = -1
                txtVar1.Text = ""
                txtVar2.Text = ""

                lblVariable1.Text = ""
                lblVariable2.Text = ""
                txtVar1.Visible = False
                txtVar2.Visible = False
            Next
        Else
            objTAPolicy_AbsentRule = New TAPolicy_AbsentRule
            With objTAPolicy_AbsentRule
                For Each row As GridDataItem In dgrdAbsentRules.Items
                    If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                        Dim intAbsentRuleId As Integer = Convert.ToInt32(row.GetDataKeyValue("AbsentRuleId").ToString())
                        AbsentRuledt.Rows.Remove(AbsentRuledt.Select("AbsentRuleId = " & intAbsentRuleId)(0))
                        .AbsentRuleId = intAbsentRuleId
                        .Delete()
                    End If
                Next
            End With
        End If
        fillAbsentRules()
    End Sub

    Sub fillAbsentRules()
        If TAPolicyID > 0 Then

            objTAPolicy_AbsentRule = New TAPolicy_AbsentRule
            objTAPolicy_AbsentRule.FK_TAPolicyId = TAPolicyID
            AbsentRuledt = objTAPolicy_AbsentRule.GetAllByTAPolicyId
            AbsentRuledt.Columns("AbsentRuleId").AutoIncrement = True
            AbsentRuledt.Columns("AbsentRuleId").AutoIncrementSeed = 1
            AbsentRuledt.Columns("AbsentRuleId").AutoIncrementStep = 1
            dgrdAbsentRules.DataSource = AbsentRuledt
            dgrdAbsentRules.DataBind()
        Else
            createAbsentRuledt()
            dgrdAbsentRules.DataSource = AbsentRuledt
            dgrdAbsentRules.DataBind()
        End If
    End Sub

    Function ifexistAbsentRule(ByVal RuleName As String, ByVal RuleArabicName As String, ByVal AbsentRuleId As Integer) As Boolean
        If Not AbsentRuledt Is Nothing AndAlso AbsentRuledt.Rows.Count > 0 Then
            If AbsentRuleId > 0 Then
                For Each i In AbsentRuledt.Rows
                    If (i("RuleName") = RuleName Or i("RuleArabicName") = RuleArabicName) And (i("AbsentRuleId") <> AbsentRuleId) Then
                        Return True
                    End If
                Next
            Else
                For Each i In AbsentRuledt.Rows
                    If i("RuleName") = RuleName Or i("RuleArabicName") = RuleArabicName Then
                        Return True
                    End If
                Next
            End If

        End If
        Return False
    End Function

#End Region

#Region "Violation"

    Sub PrepareViolationInterface()
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        txtViolationVar1.Text = ""
        ddlViolationVAr2.ClearSelection()
        ddlScenarioMode.ClearSelection()
        Select Case ddlViolationRuleType.SelectedValue
            Case 1
                lblViolationVAr1.Text = ResourceManager.GetString("DelayInMin", CultureInfo)
                lblViolationVAr1.Visible = True
                lblViolationVAr2.Text = ResourceManager.GetString("TotalDelayInMin", CultureInfo)
                reqViolationVar1.ErrorMessage = ResourceManager.GetString("PleaseDelayInMin", CultureInfo)
                reqViolationVar1.Enabled = True
                txtViolationVar1.Visible = True
                ddlViolationVAr2.Visible = True
                lblViolationVAr3.Text = ""
                txtViolationVar3.Visible = False
                reqViolationVar3.Enabled = False
                ddlScenarioMode.ClearSelection()
                ddlScenarioMode.Visible = False
                lblScenarioMode.Visible = False
            Case 2
                lblViolationVAr1.Text = ResourceManager.GetString("DelayInDays", CultureInfo)
                lblViolationVAr1.Visible = True
                lblViolationVAr2.Text = ""
                reqViolationVar1.ErrorMessage = ResourceManager.GetString("PleaseDelayInDays", CultureInfo)
                reqViolationVar1.Enabled = True
                txtViolationVar1.Visible = True
                ddlViolationVAr2.Visible = False
                lblViolationVAr3.Text = ""
                txtViolationVar3.Visible = False
                reqViolationVar3.Enabled = False
                ddlScenarioMode.ClearSelection()
                ddlScenarioMode.Visible = False
                lblScenarioMode.Visible = False
            Case 3
                lblViolationVAr1.Text = ResourceManager.GetString("DelayInDays", CultureInfo)
                lblViolationVAr1.Visible = True
                lblViolationVAr2.Text = ResourceManager.GetString("ConsiderPeriodInDays", CultureInfo)
                reqViolationVar1.ErrorMessage = ResourceManager.GetString("PleaseDelayInDays", CultureInfo)
                reqViolationVar1.Enabled = True
                txtViolationVar1.Visible = True
                ddlViolationVAr2.Visible = True
                lblViolationVAr3.Text = ""
                txtViolationVar3.Visible = False
                reqViolationVar3.Enabled = False
                ddlScenarioMode.ClearSelection()
                ddlScenarioMode.Visible = False
                lblScenarioMode.Visible = False
                'lblViolationVAr1.Text = ""
                'reqViolationVar1.ErrorMessage = ""
                'lblViolationVAr2.Text = ResourceManager.GetString("TotalDelayInMin", CultureInfo)
                'txtViolationVar1.Visible = False
                'txtViolationVar1.Text = 0
                'ddlViolationVAr2.Visible = True

            Case 5
                lblViolationVAr1.Text = ResourceManager.GetString("NoAbsentDays", CultureInfo)
                lblViolationVAr1.Visible = True
                reqViolationVar1.ErrorMessage = ResourceManager.GetString("PleaseNoAbsentDays", CultureInfo)
                reqViolationVar1.Enabled = True
                lblViolationVAr2.Text = ""
                txtViolationVar1.Visible = True
                ddlViolationVAr2.Visible = False
                lblViolationVAr3.Text = ""
                txtViolationVar3.Visible = False
                reqViolationVar3.Enabled = False
                ddlScenarioMode.ClearSelection()
                ddlScenarioMode.Visible = False
                lblScenarioMode.Visible = False
            Case 6
                'lblViolationVAr1.Text = ResourceManager.GetString("NoAbsentDays", CultureInfo)
                'reqViolationVar1.ErrorMessage = ResourceManager.GetString("PleaseNoAbsentDays", CultureInfo)

                lblViolationVAr1.Visible = False
                reqViolationVar1.Enabled = False

                lblViolationVAr1.Visible = True
                txtViolationVar1.Visible = True
                lblViolationVAr1.Text = ResourceManager.GetString("NoAbsentDays", CultureInfo)
                reqViolationVar1.ErrorMessage = ResourceManager.GetString("PleaseNoAbsentDays", CultureInfo)

                lblViolationVAr2.Text = ResourceManager.GetString("ConsiderPeriodInDays", CultureInfo)
                ddlViolationVAr2.Visible = True
                lblViolationVAr3.Text = ""
                txtViolationVar3.Visible = False
                reqViolationVar3.Enabled = False
                lblScenarioMode.Visible = True
                ddlScenarioMode.Visible = True

            Case 9
                lblViolationVAr1.Text = ResourceManager.GetString("EarlyOutInDays", CultureInfo)
                lblViolationVAr1.Visible = True
                reqViolationVar1.ErrorMessage = ResourceManager.GetString("PleaseNoEarlyOutDays", CultureInfo)
                reqViolationVar1.Enabled = True
                lblViolationVAr2.Text = ResourceManager.GetString("ConsiderPeriodInDays", CultureInfo)
                txtViolationVar1.Visible = True
                ddlViolationVAr2.Visible = True
                lblViolationVAr3.Text = ""
                txtViolationVar3.Visible = False
                reqViolationVar3.Enabled = False
                ddlScenarioMode.ClearSelection()
                ddlScenarioMode.Visible = False
                lblScenarioMode.Visible = False
            Case 10
                'lblViolationVAr1.Text = ResourceManager.GetString("NoAbsentDays", CultureInfo)
                'reqViolationVar1.ErrorMessage = ResourceManager.GetString("PleaseNoAbsentDays", CultureInfo)
                lblViolationVAr1.Visible = False
                reqViolationVar1.Enabled = False
                lblViolationVAr2.Text = ResourceManager.GetString("ConsiderPeriodInDays", CultureInfo)
                txtViolationVar1.Visible = False
                ddlViolationVAr2.Visible = True
                lblViolationVAr3.Text = ""
                txtViolationVar3.Visible = False
                reqViolationVar3.Enabled = False
                ddlScenarioMode.ClearSelection()
                ddlScenarioMode.Visible = False
                lblScenarioMode.Visible = False
            Case 11
                lblViolationVAr1.Text = ResourceManager.GetString("MoreThan", CultureInfo)
                lblViolationVAr1.Visible = True
                reqViolationVar1.ErrorMessage = ResourceManager.GetString("PleaseMoreThan", CultureInfo)
                reqViolationVar1.Enabled = True
                txtViolationVar1.Visible = True

                lblViolationVAr2.Text = ResourceManager.GetString("ConsiderPeriodInDays", CultureInfo)
                ddlViolationVAr2.Visible = True

                lblViolationVAr3.Text = ResourceManager.GetString("UpTo", CultureInfo)
                reqViolationVar3.ErrorMessage = ResourceManager.GetString("PleaseUpTo", CultureInfo)
                reqViolationVar3.Enabled = True
                txtViolationVar3.Visible = True
                ddlScenarioMode.ClearSelection()
                ddlScenarioMode.Visible = False
                lblScenarioMode.Visible = False
            Case Else
                lblViolationVAr1.Text = ""
                lblViolationVAr2.Text = ""
                lblViolationVAr3.Text = ""
                txtViolationVar1.Visible = False
                ddlViolationVAr2.Visible = False
                txtViolationVar3.Visible = False
                ddlScenarioMode.ClearSelection()
                ddlScenarioMode.Visible = False
                lblScenarioMode.Visible = False
        End Select
    End Sub

    Sub createViolationdt()
        Violationdt = New DataTable
        Dim dc As DataColumn
        dc = New DataColumn
        dc.ColumnName = "ViolationId"
        dc.DataType = GetType(Integer)
        Violationdt.Columns.Add(dc)
        Violationdt.Columns("ViolationId").AutoIncrement = True
        Violationdt.Columns("ViolationId").AutoIncrementSeed = 1
        Violationdt.Columns("ViolationId").AutoIncrementStep = 1

        dc = New DataColumn
        dc.ColumnName = "ViolationName"
        dc.DataType = GetType(String)
        Violationdt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "ViolationArabicName"
        dc.DataType = GetType(String)
        Violationdt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "ViolationRuleType"
        dc.DataType = GetType(Integer)
        Violationdt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "Variable1"
        dc.DataType = GetType(Integer)
        Violationdt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "Variable2"
        dc.DataType = GetType(Integer)
        Violationdt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "Variable3"
        dc.DataType = GetType(Integer)
        Violationdt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "FK_TAPolicyId"
        dc.DataType = GetType(Integer)
        Violationdt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "FK_ViolationActionId"
        dc.DataType = GetType(Integer)
        Violationdt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "FK_ViolationActionId2"
        dc.DataType = GetType(Integer)
        Violationdt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "FK_ViolationActionId3"
        dc.DataType = GetType(Integer)
        Violationdt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "FK_ViolationActionId4"
        dc.DataType = GetType(Integer)
        Violationdt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "FK_ViolationActionId5"
        dc.DataType = GetType(Integer)
        Violationdt.Columns.Add(dc)

    End Sub

    Protected Sub ddlViolationRuleType_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlViolationRuleType.SelectedIndexChanged
        PrepareViolationInterface()
    End Sub

    Protected Sub btnAddViolation_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnAddViolation.Command
        Dim err As Integer = -1
        Dim dr As DataRow
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Try
            If ifexistViolation(txtViolationEn.Text, txtViolationAr.Text, ViolationId) Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NameExist", CultureInfo), "info")
                Exit Sub
            End If

            If TAPolicyID = 0 Then
                If ViolationId = 0 Then
                    dr = Violationdt.NewRow
                    dr("FK_TAPolicyId") = TAPolicyID
                    dr("ViolationName") = txtViolationEn.Text
                    dr("ViolationArabicName") = txtViolationAr.Text
                    dr("ViolationRuleType") = ddlViolationRuleType.SelectedValue
                    dr("FK_ViolationActionId") = ddlViolationAction.SelectedValue
                    dr("FK_ViolationActionId2") = ddlViolationAction2.SelectedValue
                    dr("FK_ViolationActionId3") = ddlViolationAction3.SelectedValue
                    dr("FK_ViolationActionId4") = ddlViolationAction4.SelectedValue
                    dr("FK_ViolationActionId5") = ddlViolationAction5.SelectedValue
                    If txtViolationVar1.Text = "" Then
                        dr("Variable1") = 0
                    Else
                        dr("Variable1") = txtViolationVar1.Text
                    End If
                    If ddlViolationVAr2.SelectedValue = "" Then
                        dr("Variable2") = 0
                    Else
                        dr("Variable2") = ddlViolationVAr2.SelectedValue
                    End If
                    If txtViolationVar3.Text = "" Then
                        dr("Variable3") = 0
                    Else
                        dr("Variable3") = txtViolationVar3.Text
                    End If
                    If ddlScenarioMode.SelectedValue = "" Then
                        dr("ScenarioMode") = 0
                    Else
                        dr("ScenarioMode") = ddlScenarioMode.SelectedValue
                    End If
                    Violationdt.Rows.Add(dr)
                Else
                    If Violationdt.Rows.Count > 0 Then

                        dr = Violationdt.Select("ViolationId= " & ViolationId)(0)
                        dr("ViolationId") = ViolationId
                        dr("FK_TAPolicyId") = TAPolicyID
                        dr("ViolationName") = txtViolationEn.Text
                        dr("ViolationArabicName") = txtViolationAr.Text
                        dr("ViolationRuleType") = ddlViolationRuleType.SelectedValue
                        dr("FK_ViolationActionId") = ddlViolationAction.SelectedValue
                        dr("FK_ViolationActionId2") = ddlViolationAction2.SelectedValue
                        dr("FK_ViolationActionId3") = ddlViolationAction3.SelectedValue
                        dr("FK_ViolationActionId4") = ddlViolationAction4.SelectedValue
                        dr("FK_ViolationActionId5") = ddlViolationAction5.SelectedValue
                        If txtViolationVar1.Text = "" Then
                            dr("Variable1") = 0
                        Else
                            dr("Variable1") = txtViolationVar1.Text
                        End If
                        If ddlViolationVAr2.SelectedValue = "" Then
                            dr("Variable2") = 0
                        Else
                            dr("Variable2") = ddlViolationVAr2.SelectedValue
                        End If
                        If txtViolationVar3.Text = "" Then
                            dr("Variable3") = 0
                        Else
                            dr("Variable3") = txtViolationVar3.Text
                        End If
                        If ddlScenarioMode.SelectedValue = "" Then
                            dr("ScenarioMode") = 0
                        Else
                            dr("ScenarioMode") = ddlScenarioMode.SelectedValue
                        End If
                    End If
                End If
            Else
                If ViolationId = 0 Then
                    'to save to violation table
                    objTAPolicy_Violation = New TAPolicy_Violation
                    With objTAPolicy_Violation
                        .FK_TAPolicyId = TAPolicyID
                        .ViolationName = txtViolationEn.Text
                        .ViolationArabicName = txtViolationAr.Text
                        .Variable1 = IIf(txtViolationVar1.Text = "", 0, txtViolationVar1.Text)
                        .Variable2 = ddlViolationVAr2.SelectedValue
                        .Variable3 = IIf(txtViolationVar3.Text = "", 0, txtViolationVar3.Text)
                        .ViolationRuleType = ddlViolationRuleType.SelectedValue
                        .FK_ViolationActionId = ddlViolationAction.SelectedValue
                        .FK_ViolationActionId2 = ddlViolationAction2.SelectedValue
                        .FK_ViolationActionId3 = ddlViolationAction3.SelectedValue
                        .FK_ViolationActionId4 = ddlViolationAction4.SelectedValue
                        .FK_ViolationActionId5 = ddlViolationAction5.SelectedValue
                        .ScenarioMode = IIf(ddlScenarioMode.SelectedValue = "", 0, ddlScenarioMode.SelectedValue)
                        .Add()
                        fillViolation()
                    End With
                Else
                    objTAPolicy_Violation = New TAPolicy_Violation
                    With objTAPolicy_Violation
                        .ViolationId = ViolationId
                        .FK_TAPolicyId = TAPolicyID
                        .ViolationName = txtViolationEn.Text
                        .ViolationArabicName = txtViolationAr.Text
                        .Variable1 = IIf(txtViolationVar1.Text = "", 0, txtViolationVar1.Text)
                        .Variable2 = ddlViolationVAr2.SelectedValue
                        .Variable3 = IIf(txtViolationVar3.Text = "", 0, txtViolationVar3.Text)
                        .ViolationRuleType = ddlViolationRuleType.SelectedValue
                        .FK_ViolationActionId = ddlViolationAction.SelectedValue
                        .FK_ViolationActionId2 = ddlViolationAction2.SelectedValue
                        .FK_ViolationActionId3 = ddlViolationAction3.SelectedValue
                        .FK_ViolationActionId4 = ddlViolationAction4.SelectedValue
                        .FK_ViolationActionId5 = ddlViolationAction5.SelectedValue
                        .ScenarioMode = IIf(ddlScenarioMode.SelectedValue = "", 0, ddlScenarioMode.SelectedValue)
                        .Update()
                        fillViolation()
                    End With
                End If
            End If
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            dgrdViolation.DataSource = Violationdt
            dgrdViolation.DataBind()
            ViolationId = 0
            txtViolationEn.Text = ""
            txtViolationAr.Text = ""
            ddlViolationRuleType.SelectedIndex = -1
            ddlViolationAction.SelectedIndex = -1
            ddlViolationAction2.SelectedIndex = -1
            ddlViolationAction3.SelectedIndex = -1
            ddlViolationAction4.SelectedIndex = -1
            ddlViolationAction5.SelectedIndex = -1
            txtViolationVar1.Text = ""
            ddlViolationVAr2.ClearSelection()
            txtViolationVar3.Text = ""
            ddlScenarioMode.ClearSelection()

            lblViolationVAr1.Text = ""
            lblViolationVAr2.Text = ""
            lblViolationVAr3.Text = ""
            txtViolationVar3.Visible = False
            txtViolationVar1.Visible = False
            ddlViolationVAr2.Visible = False
            lblScenarioMode.Visible = False
            ddlScenarioMode.Visible = False
        Catch ex As Exception
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorAddViolation", CultureInfo), "error")
        End Try
    End Sub

    Sub clearViolation()
        Violationdt = New DataTable
        dgrdViolation.DataSource = Violationdt
        dgrdViolation.DataBind()
        ViolationId = 0
        txtViolationEn.Text = ""
        txtViolationAr.Text = ""
        ddlViolationRuleType.SelectedIndex = -1
        ddlViolationAction.SelectedIndex = -1
        ddlViolationAction2.SelectedIndex = -1
        ddlViolationAction3.SelectedIndex = -1
        ddlViolationAction4.SelectedIndex = -1
        ddlViolationAction5.SelectedIndex = -1

        txtViolationVar1.Text = ""
        txtViolationVar1.Visible = False

        ddlViolationVAr2.ClearSelection()
        ddlViolationVAr2.Visible = False

        txtViolationVar3.Text = ""
        txtViolationVar3.Visible = False

        lblViolationVAr1.Text = ""
        lblViolationVAr2.Text = ""
        lblViolationVAr3.Text = ""

        ddlScenarioMode.ClearSelection()
        ddlScenarioMode.Visible = False
        lblScenarioMode.Visible = False
    End Sub

    Protected Sub dgrdViolation_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdViolation.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim item As GridDataItem
            item = e.Item

            'Dim RuleTypeID = DirectCast(DirectCast(DirectCast(DirectCast(DirectCast(e.Item.DataItem, System.Object), System.Data.DataRowView), System.Data.DataRowView).Row, System.Data.DataRow), System.Data.DataRow).ItemArray(4)
            Dim RuleTypeID As Integer = Convert.ToInt32(item.GetDataKeyValue("ViolationRuleType").ToString())
            Dim RuleTypeName As String
            Select Case RuleTypeID
                Case 1
                    RuleTypeName = ResourceManager.GetString("OneDayDelay", CultureInfo)
                Case 2
                    RuleTypeName = ResourceManager.GetString("ConsecutiveDelay", CultureInfo)
                Case 3
                    RuleTypeName = ResourceManager.GetString("Noofdelaysperperiod", CultureInfo)
                Case 4
                    RuleTypeName = ResourceManager.GetString("OneAbsentDay", CultureInfo)
                Case 5
                    RuleTypeName = ResourceManager.GetString("ConsecutiveAbsent", CultureInfo)
                Case 6
                    RuleTypeName = ResourceManager.GetString("Absentdaysperperiod", CultureInfo)
                Case 7
                    RuleTypeName = ResourceManager.GetString("OneDayEarlyOutLimit", CultureInfo)
                Case 8
                    RuleTypeName = ResourceManager.GetString("ConsecutiveEarlyOuts", CultureInfo)
                Case 9
                    RuleTypeName = ResourceManager.GetString("NoofEarlyOutsPerPeriod", CultureInfo)
                Case 10
                    RuleTypeName = ResourceManager.GetString("MissingInMissingOutPerPeriod", CultureInfo)
                Case 11
                    RuleTypeName = ResourceManager.GetString("DelayEarlyOutPerPeriod", CultureInfo)
            End Select
            item("TemplateColumn1").Text = RuleTypeName

        End If
    End Sub

    Protected Sub dgrdViolation_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdViolation.SelectedIndexChanged
        Try

            Dim objTAPolicy_Violation As New TAPolicy_Violation
            Dim dt As DataTable = Violationdt
            ViolationId = CInt(CType(dgrdViolation.SelectedItems(0), GridDataItem).GetDataKeyValue("ViolationId").ToString())
            objTAPolicy_Violation.ViolationId = ViolationId
            txtViolationEn.Text = CType(dgrdViolation.SelectedItems(0), GridDataItem).GetDataKeyValue("ViolationName").ToString()
            txtViolationAr.Text = CType(dgrdViolation.SelectedItems(0), GridDataItem).GetDataKeyValue("ViolationArabicName").ToString()
            ddlViolationRuleType.SelectedValue = CInt(CType(dgrdViolation.SelectedItems(0), GridDataItem).GetDataKeyValue("ViolationRuleType").ToString())
            PrepareViolationInterface()
            If CType(dgrdViolation.SelectedItems(0), GridDataItem).GetDataKeyValue("Variable1").ToString() <> "" Then
                txtViolationVar1.Text = CInt(CType(dgrdViolation.SelectedItems(0), GridDataItem).GetDataKeyValue("Variable1").ToString())
            End If
            If CType(dgrdViolation.SelectedItems(0), GridDataItem).GetDataKeyValue("Variable2").ToString() <> "" Then
                ddlViolationVAr2.SelectedValue = CInt(CType(dgrdViolation.SelectedItems(0), GridDataItem).GetDataKeyValue("Variable2").ToString())
            End If
            If CType(dgrdViolation.SelectedItems(0), GridDataItem).GetDataKeyValue("Variable3").ToString() <> "" Then
                txtViolationVar3.Text = CInt(CType(dgrdViolation.SelectedItems(0), GridDataItem).GetDataKeyValue("Variable3").ToString())
            End If

            ddlViolationAction.SelectedValue = CInt(CType(dgrdViolation.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_ViolationActionId").ToString())
            ddlViolationAction2.SelectedValue = CInt(CType(dgrdViolation.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_ViolationActionId2").ToString())
            ddlViolationAction3.SelectedValue = CInt(CType(dgrdViolation.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_ViolationActionId3").ToString())
            ddlViolationAction4.SelectedValue = CInt(CType(dgrdViolation.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_ViolationActionId4").ToString())
            ddlViolationAction5.SelectedValue = CInt(CType(dgrdViolation.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_ViolationActionId5").ToString())


            If CType(dgrdViolation.SelectedItems(0), GridDataItem).GetDataKeyValue("ScenarioMode").ToString() <> "" Then
                ddlScenarioMode.SelectedValue = CInt(CType(dgrdViolation.SelectedItems(0), GridDataItem).GetDataKeyValue("ScenarioMode").ToString())
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub dgrdViolation_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdViolation.NeedDataSource
        dgrdViolation.DataSource = Violationdt
    End Sub

    Protected Sub btnRemoveViolation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemoveViolation.Click
        Dim strBuilder As New StringBuilder
        If TAPolicyID = 0 Then
            For Each row As GridDataItem In dgrdViolation.Items
                If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                    Dim intViolationId As Integer = Convert.ToInt32(row.GetDataKeyValue("ViolationId").ToString())
                    Violationdt.Rows.Remove(Violationdt.Select("ViolationId = " & intViolationId)(0))
                End If
                dgrdViolation.DataSource = Violationdt
                dgrdViolation.DataBind()
                ViolationId = 0
                txtViolationEn.Text = ""
                txtViolationAr.Text = ""
                ddlViolationRuleType.SelectedIndex = -1
                ddlViolationAction.SelectedIndex = -1
                ddlViolationAction2.SelectedIndex = -1
                ddlViolationAction3.SelectedIndex = -1
                ddlViolationAction4.SelectedIndex = -1
                ddlViolationAction5.SelectedIndex = -1
                txtViolationVar1.Text = ""
                ddlViolationVAr2.ClearSelection()
                txtViolationVar3.Text = ""
                lblViolationVAr1.Text = ""
                lblViolationVAr2.Text = ""
                lblViolationVAr3.Text = ""
                txtViolationVar1.Visible = False
                ddlViolationVAr2.Visible = False
                txtViolationVar3.Visible = False

                ddlScenarioMode.ClearSelection()
                ddlScenarioMode.Visible = False
                lblScenarioMode.Visible = False
            Next

        Else
            objTAPolicy_Violation = New TAPolicy_Violation
            With objTAPolicy_Violation
                For Each row As GridDataItem In dgrdViolation.Items
                    If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                        Dim intViolationId As Integer = Convert.ToInt32(row.GetDataKeyValue("ViolationId").ToString())
                        Violationdt.Rows.Remove(Violationdt.Select("ViolationId = " & intViolationId)(0))
                        .ViolationId = intViolationId
                        .Delete()
                    End If
                Next
            End With
        End If
        fillViolation()
    End Sub

    Sub fillViolation()
        If TAPolicyID > 0 Then

            objTAPolicy_Violation = New TAPolicy_Violation
            objTAPolicy_Violation.FK_TAPolicyId = TAPolicyID
            Violationdt = objTAPolicy_Violation.GetAllByPolicyId
            Violationdt.Columns("ViolationId").AutoIncrement = True
            Violationdt.Columns("ViolationId").AutoIncrementSeed = 1
            Violationdt.Columns("ViolationId").AutoIncrementStep = 1
            dgrdViolation.DataSource = Violationdt
            dgrdViolation.DataBind()
        Else
            createViolationdt()
            dgrdViolation.DataSource = Violationdt
            dgrdViolation.DataBind()
        End If
    End Sub

    Function ifexistViolation(ByVal ViolationName As String, ByVal ViolationArabicName As String, ByVal intViolationId As Integer) As Boolean
        If Not Violationdt Is Nothing AndAlso Violationdt.Rows.Count > 0 Then
            If intViolationId > 0 Then
                For Each i In Violationdt.Rows
                    If (i("ViolationName") = ViolationName Or i("ViolationArabicName") = ViolationArabicName) And (i("ViolationId") <> ViolationId) Then
                        Return True
                    End If
                Next
            Else
                For Each i In Violationdt.Rows

                    If i("ViolationName") = ViolationName Or i("ViolationArabicName") = ViolationArabicName Then
                        Return True
                    End If
                Next
            End If

        End If
        Return False
    End Function

#End Region

#Region "Deduction"

    Protected Sub chkDelay_Earlyout_CheckedChanged(sender As Object, e As EventArgs) Handles chkDelay_Earlyout.CheckedChanged
        If chkDelay_Earlyout.Checked = True Then
            dvDelay_Earlyout.Visible = True
        Else
            dvDelay_Earlyout.Visible = False
            rmtxtFirstTime.Text = "00:00"
            rmtxtDeductionHours.Text = "00:00"
        End If
    End Sub

    Protected Sub rblNotCompleteWork_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblNotCompleteWork.SelectedIndexChanged
        If rblNotCompleteWork.SelectedValue = 1 Then
            dvNotCompletePercentage.Visible = True
            dvNotCompleteNoHours.Visible = False
            rmtxtCompleteNoHours.Text = "00:00"
        Else
            dvNotCompletePercentage.Visible = False
            dvNotCompleteNoHours.Visible = True
            txtPercentage.Text = String.Empty
        End If
    End Sub

    Protected Sub chkNotCompleteWork_CheckedChanged(sender As Object, e As EventArgs) Handles chkNotCompleteWork.CheckedChanged

        If chkNotCompleteWork.Checked = True Then
            dvNotCompleteWorkSelection.Visible = True
            rblNotCompleteWork.SelectedValue = 1
            dvNotCompletePercentage.Visible = True
        Else
            rblNotCompleteWork.ClearSelection()
            dvNotCompleteWorkSelection.Visible = False
            dvNotCompletePercentage.Visible = False
            dvNotCompleteNoHours.Visible = False
            rmtxtCompleteNoHours.Text = "00:00"
            dvNotCompletePercentage.Visible = False
            dvNotCompleteNoHours.Visible = False
            txtPercentage.Text = String.Empty
        End If
    End Sub

    Protected Sub imgbtnAbsent_Click(sender As Object, e As EventArgs) Handles imgbtnAbsent.Click
        CtlCommon.ShowToolTip_Round(Me.Page, ResourceManager.GetString("Absent"), ResourceManager.GetString("AbsentToolTip"), MsgLang)
    End Sub

    Protected Sub imgbtnMissingIn_Click(sender As Object, e As EventArgs) Handles imgbtnMissingIn.Click
        CtlCommon.ShowToolTip_Round(Me.Page, ResourceManager.GetString("MissingIn"), ResourceManager.GetString("MissingInToolTip"), MsgLang)
    End Sub

    Protected Sub imgbtnMissingOut_Click(sender As Object, e As EventArgs) Handles imgbtnMissingOut.Click
        CtlCommon.ShowToolTip_Round(Me.Page, ResourceManager.GetString("MissingOut"), ResourceManager.GetString("MissingOutToolTip"), MsgLang)
    End Sub

    Protected Sub imgbtnDelay_Earlyout_Click(sender As Object, e As EventArgs) Handles imgbtnDelay_Earlyout.Click
        CtlCommon.ShowToolTip_Round(Me.Page, ResourceManager.GetString("DelayEarlyOut"), ResourceManager.GetString("DelayEarlyOutToolTip"), MsgLang)
    End Sub

    Protected Sub imgbtnNotCompleteWork_Click(sender As Object, e As EventArgs) Handles imgbtnNotCompleteWork.Click
        CtlCommon.ShowToolTip_Round(Me.Page, ResourceManager.GetString("NotCompleteWorkHours"), ResourceManager.GetString("NotCompleteWorkHoursToolTip"), MsgLang)
    End Sub

    Protected Sub rblConsiderDelayEarlyBy_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblConsiderDelayEarlyBy.SelectedIndexChanged
        If rblConsiderDelayEarlyBy.SelectedValue = 1 Then
            dvDelayEarlyConsiderDuration.Visible = True
            dvDelayEarlyConsiderOccuerence.Visible = False
        Else
            dvDelayEarlyConsiderDuration.Visible = False
            dvDelayEarlyConsiderOccuerence.Visible = True
        End If
    End Sub

    Private Sub SaveDeduction()
        objTAPolicy_DeductionPolicy = New TAPolicy_DeductionPolicy
        Dim err As Integer = -1
        With objTAPolicy_DeductionPolicy
            .FK_TAPolicyId = TAPolicyID
            .GetBy_FK_TAPolicyId()
            DeductionPolicyId = .DeductionPolicyId
            .ConsiderAbsent = chkAbsent.Checked
            .ConsiderMissingIn = chkMissingIn.Checked
            .ConsiderMissingOut = chkMissingOut.Checked
            .ConsiderDelay_EarlyOut = chkDelay_Earlyout.Checked

            If Not rblConsiderDelayEarlyBy.SelectedValue = Nothing Then
                .DelayEarly_CalcMethod = rblConsiderDelayEarlyBy.SelectedValue
            End If


            If chkDelay_Earlyout.Checked = True Then
                If rblConsiderDelayEarlyBy.SelectedValue = 1 Then
                    .Delay_EarlyOut_FirstDeduct = (CInt(rmtxtFirstTime.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtxtFirstTime.TextWithLiterals.Split(":")(1))
                    .Delay_EarlyOut_Deduct = (CInt(rmtxtDeductionHours.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtxtDeductionHours.TextWithLiterals.Split(":")(1))
                    .RemainingBalanceToBeRounded = chkRemainingBalancetoBeRounded.Checked
                    .IncluedLostTime = chkIncludeLostTime.Checked
                    .ConsiderOneDeduction_DelayEarly = False
                Else
                    .Delay_EarlyOut_FirstDeduct = Nothing
                    .Delay_EarlyOut_Deduct = Nothing
                    .RemainingBalanceToBeRounded = False
                    .IncluedLostTime = False
                    .ConsiderOneDeduction_DelayEarly = chkConsiderOneDeduction_DelayEarly.Checked
                End If
            Else
                .Delay_EarlyOut_FirstDeduct = Nothing
                .Delay_EarlyOut_Deduct = Nothing
                .RemainingBalanceToBeRounded = False
                .IncluedLostTime = False
                .DelayEarly_CalcMethod = 0
                .ConsiderOneDeduction_DelayEarly = False
            End If


            .ConsiderNotComplete = chkNotCompleteWork.Checked

            If chkNotCompleteWork.Checked = True Then
                .NotCompleteSelection = rblNotCompleteWork.SelectedValue
                If rblNotCompleteWork.SelectedValue = 1 Then
                    .NotCompleteValue = txtPercentage.Text
                Else
                    .NotCompleteValue = (CInt(rmtxtCompleteNoHours.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtxtCompleteNoHours.TextWithLiterals.Split(":")(1))
                End If
            Else
                .NotCompleteSelection = Nothing
                .NotCompleteValue = Nothing
            End If

            .ExcludePendingLeaves = chkExcludePendingLeaves.Checked
            If DeductionPolicyId = 0 Then
                .CREATED_BY = SessionVariables.LoginUser.ID
                err = .Add
            Else
                .DeductionPolicyId = DeductionPolicyId
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                err = .Update
            End If

        End With

        If err = 0 Then

        Else

        End If
    End Sub

    Private Sub FillDeduction()
        objTAPolicy_DeductionPolicy = New TAPolicy_DeductionPolicy
        With objTAPolicy_DeductionPolicy
            .FK_TAPolicyId = TAPolicyID
            .GetBy_FK_TAPolicyId()
            chkAbsent.Checked = .ConsiderAbsent
            chkMissingIn.Checked = .ConsiderMissingIn
            chkMissingOut.Checked = .ConsiderMissingOut

            chkDelay_Earlyout.Checked = .ConsiderDelay_EarlyOut
            If Not .DelayEarly_CalcMethod = 0 Then
                rblConsiderDelayEarlyBy.SelectedValue = .DelayEarly_CalcMethod
            End If


            If chkDelay_Earlyout.Checked = True Then
                dvDelay_Earlyout.Visible = True
                If Not .DelayEarly_CalcMethod = 0 Then
                    If rblConsiderDelayEarlyBy.SelectedValue = 1 Then
                        dvDelayEarlyConsiderDuration.Visible = True
                        dvDelayEarlyConsiderOccuerence.Visible = False
                        rmtxtFirstTime.Text = CtlCommon.GetFullTimeString(.Delay_EarlyOut_FirstDeduct)
                        rmtxtDeductionHours.Text = CtlCommon.GetFullTimeString(.Delay_EarlyOut_Deduct)
                        chkRemainingBalancetoBeRounded.Checked = .RemainingBalanceToBeRounded
                        chkIncludeLostTime.Checked = .IncluedLostTime
                    Else
                        dvDelayEarlyConsiderOccuerence.Visible = True
                        dvDelayEarlyConsiderDuration.Visible = False
                        chkConsiderOneDeduction_DelayEarly.Checked = .ConsiderOneDeduction_DelayEarly
                    End If
                End If


            Else
                dvDelay_Earlyout.Visible = False
                rmtxtFirstTime.Text = "00:00"
                rmtxtDeductionHours.Text = "00:00"
                chkRemainingBalancetoBeRounded.Checked = False
                chkIncludeLostTime.Checked = False
            End If

            chkNotCompleteWork.Checked = .ConsiderNotComplete
            chkExcludePendingLeaves.Checked = .ExcludePendingLeaves
            If chkNotCompleteWork.Checked = True Then
                dvNotCompleteWorkSelection.Visible = True
                rblNotCompleteWork.SelectedValue = .NotCompleteSelection
            Else
                dvNotCompleteWorkSelection.Visible = False
            End If

            If rblNotCompleteWork.SelectedValue = 1 Then
                dvNotCompletePercentage.Visible = True
                txtPercentage.Text = .NotCompleteValue
                dvNotCompleteNoHours.Visible = False
                rmtxtCompleteNoHours.Text = "00:00"
            Else
                dvNotCompletePercentage.Visible = False
                txtPercentage.Text = String.Empty
                dvNotCompleteNoHours.Visible = True
                rmtxtCompleteNoHours.Text = CtlCommon.GetFullTimeString(.NotCompleteValue)
            End If
        End With
    End Sub

    Private Sub ClearDeduction()
        chkAbsent.Checked = False
        chkMissingIn.Checked = False
        chkMissingOut.Checked = False
        chkDelay_Earlyout.Checked = False
        dvDelay_Earlyout.Visible = False
        rmtxtFirstTime.Text = "00:00"
        rmtxtDeductionHours.Text = "00:00"
        chkNotCompleteWork.Checked = False
        dvNotCompleteWorkSelection.Visible = False
        dvNotCompletePercentage.Visible = False
        dvNotCompleteNoHours.Visible = False
        rmtxtCompleteNoHours.Text = "00:00"
        txtPercentage.Text = String.Empty
        rblNotCompleteWork.SelectedValue = 1
        dvNotCompletePercentage.Visible = True
        chkRemainingBalancetoBeRounded.Checked = False
        chkIncludeLostTime.Checked = False
        chkExcludePendingLeaves.Checked = False
        rblConsiderDelayEarlyBy.ClearSelection()
        dvDelayEarlyConsiderDuration.Visible = False
        dvDelayEarlyConsiderOccuerence.Visible = False
        TabContainer1.ActiveTabIndex = 0
        DeductionPolicyId = 0
    End Sub

#End Region

    
End Class
