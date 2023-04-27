Imports System.Data
Imports TA.Lookup
Imports TA.Definitions
Imports SmartV.DB
Imports SmartV.UTILITIES
Imports TA.Employees
Imports Telerik.Web.UI
Imports TA.Admin
Imports TA.Security
Imports Telerik.Web.UI.DatePickerPopupDirection
Imports SmartV.Version

Partial Class Admin_NotificationType
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Dim objNotificationType As NotificationType
    Shared SortExepression As String
    Shared dtCurrent As DataTable
    Private objOrgLevel As OrgLevel
    Private objCoordinator_Type As Coordinator_Type
    Private objAPP_Settings As APP_Settings
#End Region

#Region "Properties"

    Public Property NotificationID() As Integer
        Get
            Return ViewState("NotificationID")
        End Get
        Set(ByVal value As Integer)
            ViewState("NotificationID") = value
        End Set
    End Property

    Public Property dtCurrentControls() As DataTable
        Get
            Return ViewState("dtCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
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
            PageHeader1.HeaderText = ResourceManager.GetString("NotificationType", CultureInfo)
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            mvNotificationType.SetActiveView(viewNotificationTypeList)
            mvNotificationType.ActiveViewIndex = 0
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            FillGrid()
            FillLevels()
            SetTimePickerProperty()
            SetRadDateTimePickerPeoperties()
            FillCheck_CoordinatorTypes()
        End If

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrentControls = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrentControls.Rows
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

    Protected Sub btn_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim gvr As GridDataItem = CType(sender, LinkButton).Parent.Parent
        NotificationID = dgrdNotificationType.Items(gvr.ItemIndex).GetDataKeyValue("NotificationTypeId").ToString()
        FillList()
        objNotificationType = New NotificationType
        With objNotificationType
            .NotificationTypeId = NotificationID
            .GetByPK()
            chkHasEmail.Checked = .HasEmail
            chkHasSMS.Checked = .HasSMS
            chkSendReportToManager.Checked = .SendReportToManager
            chkSendToEmployee.Checked = .SendToEmployee
            chkSendToReportHR.Checked = .SendToReportHR
            chkSendReportToDeputyManager.Checked = .SendReportToDeputy
            txtEmailTemplateAr.Text = .EmailNotificationTemplateAr
            txtEmailTemplateEn.Text = .EmailNotificationTemplateEn
            txtSMSTemplateAr.Text = .SMSNotificationTemplateAr
            txtSMSTemplateEn.Text = .SMSNotificationTemplateEn
            chkSendReportToCoordinator.Checked = .SendReportToCoordinator

          
            objAPP_Settings = New APP_Settings
            objAPP_Settings.GetByPK()
            If objAPP_Settings.IncludeNotifications_CoordinatorTypes = True Then

                If .SendReportToCoordinator = True Then
                    dvCoordinatorType.Visible = True
                Else
                    cblCoordinatorTypes.ClearSelection()
                    dvCoordinatorType.Visible = False
                End If
                If .SendReportToCoordinator = True Then
                    cblCoordinatorTypes.ClearSelection()
                    Dim strCoordiatorType As String = .CoordinatorType
                    Dim arrCoordiatorType As String() = Split(strCoordiatorType, ",")
                    For Each item In arrCoordiatorType
                        For Each CoordinatorTypeItem As ListItem In cblCoordinatorTypes.Items
                            If CoordinatorTypeItem.Value = item Then
                                CoordinatorTypeItem.Selected = True
                            End If
                        Next
                    Next
                End If
            End If
            If .HasEmail = True Then
                pnlEmailSendingTime.Visible = True
                If .IsSpecificTimeEmail = True Then
                    rblEmailSendingTime.SelectedValue = 1
                    pnlEmailSpecificTime.Visible = True
                    If Not .SpecificTimeEmail = Nothing Then
                        Dim intTime As Integer = .SpecificTimeEmail
                        Dim hours As Integer = intTime \ 60
                        Dim minutes As Integer = intTime - (hours * 60)
                        radtpEmailSpecificTime.SelectedTime = New TimeSpan(hours, minutes, 0)
                    End If
                Else
                    pnlEmailSpecificTime.Visible = False
                End If
            End If

            If .HasSMS = True Then
                pnlSMSSendingTime.Visible = True
                If .IsSpecificTimeSMS = True Then
                    rblSMSSendingTime.SelectedValue = 1
                    pnlSMSSpecificTime.Visible = True
                    If Not .SpecificTimeSMS = Nothing Then
                        Dim intTime As Integer = .SpecificTimeSMS
                        Dim hours As Integer = intTime \ 60
                        Dim minutes As Integer = intTime - (hours * 60)
                        radtpSMSSpecificTime.SelectedTime = New TimeSpan(hours, minutes, 0)
                    End If
                Else
                    pnlSMSSpecificTime.Visible = False
                End If
            End If

            If NotificationID = 2 Then '--Early Out Notification Type
                lblNotificationPolicy.Visible = True
                rblNotificationPoicy.Visible = True
                rblNotificationPoicy.SelectedValue = .NotificationPolicy

                dvRepeatedAbsent.Visible = False
                rfvAbsentDays.Enabled = False
                dvAdditionalLevel.Visible = False
                dvEmployee_OutDuration_AC.Visible = False
                dvOccurrence.Visible = False
                dvEmp_Temperature.Visible = False
                rfvEmp_Temperature.Enabled = False

            ElseIf NotificationID = 58 Then
                dvRepeatedAbsent.Visible = True
                rfvAbsentDays.Enabled = True
                radnumAbsentDays.Text = .NotificationPolicy

                lblNotificationPolicy.Visible = False
                rblNotificationPoicy.Visible = False
                dvAdditionalLevel.Visible = False
                dvEmployee_OutDuration_AC.Visible = False
                dvOccurrence.Visible = False
                dvEmp_Temperature.Visible = False
                rfvEmp_Temperature.Enabled = False

            ElseIf NotificationID = 7 Or NotificationID = 10 Then
                dvAdditionalLevel.Visible = True
                radcmbLevels.SelectedValue = .AdditionalApprovalLevel

                lblNotificationPolicy.Visible = False
                rblNotificationPoicy.Visible = False
                dvRepeatedAbsent.Visible = False
                rfvAbsentDays.Enabled = False
                dvEmployee_OutDuration_AC.Visible = False
                dvOccurrence.Visible = False
                dvEmp_Temperature.Visible = False
                rfvEmp_Temperature.Enabled = False

            ElseIf NotificationID = 59 Then
                dvEmployee_OutDuration_AC.Visible = True
                radnumtxtOutDurationPolicy_AC.Text = .NotificationPolicy

                lblNotificationPolicy.Visible = False
                rblNotificationPoicy.Visible = False
                dvRepeatedAbsent.Visible = False
                rfvAbsentDays.Enabled = False
                dvAdditionalLevel.Visible = False
                dvOccurrence.Visible = False
                dvEmp_Temperature.Visible = False
                rfvEmp_Temperature.Enabled = False

            ElseIf NotificationID = 61 Or NotificationID = 62 Or NotificationID = 63 Or NotificationID = 77 Then
                dvOccurrence.Visible = True

                FillOccurrence()
                dvEmployee_OutDuration_AC.Visible = False
                lblNotificationPolicy.Visible = False
                rblNotificationPoicy.Visible = False
                dvRepeatedAbsent.Visible = False
                rfvAbsentDays.Enabled = False
                dvAdditionalLevel.Visible = False
                dvEmp_Temperature.Visible = False
                rfvEmp_Temperature.Enabled = False

            ElseIf NotificationID = 91 Then
                dvEmp_Temperature.Visible = True
                rfvEmp_Temperature.Enabled = True
                RadnumEmp_Temperature.Text = .NotificationPolicy

                dvOccurrence.Visible = False
                dvEmployee_OutDuration_AC.Visible = False
                lblNotificationPolicy.Visible = False
                rblNotificationPoicy.Visible = False
                dvRepeatedAbsent.Visible = False
                rfvAbsentDays.Enabled = False
                dvAdditionalLevel.Visible = False
            Else
                lblNotificationPolicy.Visible = False
                rblNotificationPoicy.Visible = False
                dvRepeatedAbsent.Visible = False
                rfvAbsentDays.Enabled = False
                dvAdditionalLevel.Visible = False
                dvEmployee_OutDuration_AC.Visible = False
                dvOccurrence.Visible = False
                dvEmp_Temperature.Visible = False
                rfvEmp_Temperature.Enabled = False

            End If

            If SessionVariables.CultureInfo = "ar-JO" Then
                lblNotificationType.Text = .TypeNameAr
            Else
                lblNotificationType.Text = .TypeNameEn
            End If

        End With
        mvNotificationType.SetActiveView(viewEditNotificationType)
        mvNotificationType.ActiveViewIndex = 1
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
        If RadCmbBxParameters.SelectedIndex > 0 Then
            txtEmailTemplateEn.Text += "{" + RadCmbBxParameters.SelectedItem.Text + "}"
        End If
    End Sub

    Protected Sub btnAddArb_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddArb.Click
        If RadCmbBxParametersArb.SelectedIndex > 0 Then
            txtEmailTemplateAr.Text += "{" + RadCmbBxParametersArb.SelectedItem.Text + "}"
        End If
    End Sub

    Protected Sub btnSMSAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSMSAdd.Click
        If RadSMSCmbBxParameters.SelectedIndex > 0 Then
            txtSMSTemplateEn.Text += "{" + RadSMSCmbBxParameters.SelectedItem.Text + "}"
        End If
    End Sub

    Protected Sub btnSMSAddArb_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSMSAddArb.Click
        If RadSMSCmbBxParametersArb.SelectedIndex > 0 Then
            txtSMSTemplateAr.Text += "{" + RadSMSCmbBxParametersArb.SelectedItem.Text + "}"
        End If
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdate.Click
        Dim errNo As Integer = -1
        objNotificationType = New NotificationType
        With objNotificationType
            .NotificationTypeId = NotificationID
            .HasEmail = chkHasEmail.Checked
            .HasSMS = chkHasSMS.Checked
            .EmailNotificationTemplateEn = txtEmailTemplateEn.Text
            .EmailNotificationTemplateAr = txtEmailTemplateAr.Text
            .SMSNotificationTemplateEn = txtSMSTemplateEn.Text
            .SMSNotificationTemplateAr = txtSMSTemplateAr.Text
            .SendToEmployee = chkSendToEmployee.Checked
            .SendReportToManager = chkSendReportToManager.Checked
            .SendToReportHR = chkSendToReportHR.Checked
            .SendReportToDeputy = chkSendReportToDeputyManager.Checked
            If rblEmailSendingTime.SelectedValue = 0 Then
                .IsSpecificTimeEmail = False
                .SpecificTimeEmail = Nothing
            Else
                .IsSpecificTimeEmail = True
                .SpecificTimeEmail = (radtpEmailSpecificTime.SelectedDate.Value.Hour * 60) + (radtpEmailSpecificTime.SelectedDate.Value.Minute)
            End If

            If rblSMSSendingTime.SelectedValue = 0 Then
                .IsSpecificTimeSMS = False
                .SpecificTimeSMS = Nothing
            Else
                .IsSpecificTimeSMS = True
                .SpecificTimeSMS = (radtpSMSSpecificTime.SelectedDate.Value.Hour * 60) + (radtpSMSSpecificTime.SelectedDate.Value.Minute)
            End If

            If NotificationID = 2 Then
                .NotificationPolicy = rblNotificationPoicy.SelectedValue
            ElseIf NotificationID = 58 Then
                .NotificationPolicy = radnumAbsentDays.Text
            ElseIf NotificationID = 59 Then
                .NotificationPolicy = radnumtxtOutDurationPolicy_AC.Text
            ElseIf NotificationID = 61 Or NotificationID = 62 Or NotificationID = 63 Or NotificationID = 77 Then
                Dim OccurrencePolicy As String = Nothing
                OccurrencePolicy = FormatOccurrence()
                If OccurrencePolicy = -1 Then
                    Exit Sub
                End If
                .NotificationPolicy = OccurrencePolicy
            ElseIf NotificationID = 91 Then
                .NotificationPolicy = RadnumEmp_Temperature.Text

            End If

            If NotificationID = 7 Or NotificationID = 10 Then '--- Permission Request && Leave Request
                If Not radcmbLevels.SelectedValue = -1 Then
                    .AdditionalApprovalLevel = radcmbLevels.SelectedValue
                Else
                    .AdditionalApprovalLevel = Nothing
                End If
            End If

            .SendReportToCoordinator = chkSendReportToCoordinator.Checked
            objAPP_Settings = New APP_Settings
            objAPP_Settings.GetByPK()
            Dim strCoordinatorTypeId As String = Nothing
            If objAPP_Settings.IncludeNotifications_CoordinatorTypes = True Then
                Dim flag As Boolean = False
                If chkSendReportToCoordinator.Checked = True Then
                    For Each item In cblCoordinatorTypes.Items
                        If item.Selected Then
                            flag = True
                            strCoordinatorTypeId = item.value.ToString + "," + strCoordinatorTypeId
                        End If
                    Next
                    If flag = False Then
                        CtlCommon.ShowMessage(Page, ResourceManager.GetString("SelectCoordinatorType", CultureInfo), "info")
                        Exit Sub
                    End If
                End If
            End If
            .CoordinatorType = strCoordinatorTypeId
            errNo = .Update()
            If errNo = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ModifySuccessfully", CultureInfo), "success")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorModfiy", CultureInfo), "error")
            End If
        End With

        If errNo = 0 Then
            ClearControl()
            FillGrid()
        End If
    End Sub

    Protected Sub dgrdNotificationType_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles dgrdNotificationType.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
            Dim hdnHasEmail As HiddenField = CType(e.Item.FindControl("hdnHasEmail"), HiddenField)
            Dim hdnHasSMS As HiddenField = CType(e.Item.FindControl("hdnHasSMS"), HiddenField)
            Dim chkGridHasEmail As CheckBox = CType(e.Item.FindControl("chkHasEmail"), CheckBox)
            Dim chkGridHasSMS As CheckBox = CType(e.Item.FindControl("chkHasSMS"), CheckBox)

            If hdnHasEmail.Value = "True" Then
                chkGridHasEmail.Checked = True
            End If

            If hdnHasSMS.Value = "True" Then
                chkGridHasSMS.Checked = True
            End If
        End If
    End Sub

    Protected Sub dgrdNotificationType_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdNotificationType.NeedDataSource
        objNotificationType = New NotificationType
        Dim strNotifications As String = version.GetNotificationTypes()

        dtCurrent = objNotificationType.GetAll()
        If Not strNotifications = String.Empty Then
            objNotificationType.strNotificationTypeId = strNotifications
            dtCurrent = objNotificationType.GetBystrNotificationTypeId
        End If
        Dim dv As New DataView(dtCurrent)
        dv.Sort = SortExepression
        dgrdNotificationType.DataSource = dv
    End Sub

    Protected Sub chkHasEmail_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkHasEmail.CheckedChanged
        If chkHasEmail.Checked = True Then
            pnlEmailSendingTime.Visible = True
        Else
            pnlEmailSendingTime.Visible = False
        End If
    End Sub

    Protected Sub chkHasSMS_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkHasSMS.CheckedChanged
        If chkHasSMS.Checked = True Then
            pnlSMSSendingTime.Visible = True
        Else
            pnlSMSSendingTime.Visible = False
        End If
    End Sub

    Protected Sub rblEmailSendingTime_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rblEmailSendingTime.SelectedIndexChanged
        If Not rblEmailSendingTime.SelectedValue = 0 Then
            pnlEmailSpecificTime.Visible = True
        Else
            pnlEmailSpecificTime.Visible = False
        End If
    End Sub

    Protected Sub rblSMSSendingTime_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rblSMSSendingTime.SelectedIndexChanged
        If Not rblSMSSendingTime.SelectedValue = 0 Then
            pnlSMSSpecificTime.Visible = True
        Else
            pnlSMSSpecificTime.Visible = False
        End If
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        mvNotificationType.ActiveViewIndex = 0
    End Sub

    Protected Sub chkSendReportToCoordinator_CheckedChanged(sender As Object, e As EventArgs) Handles chkSendReportToCoordinator.CheckedChanged
        objAPP_Settings = New APP_Settings
        objAPP_Settings.GetByPK()
        If objAPP_Settings.IncludeNotifications_CoordinatorTypes = True Then
            cblCoordinatorTypes.ClearSelection()
            If chkSendReportToCoordinator.Checked = True Then
                dvCoordinatorType.Visible = True
            Else
                dvCoordinatorType.Visible = False
                cblCoordinatorTypes.ClearSelection()
            End If
        End If

    End Sub

#End Region

#Region "Method"

    Private Sub FillGrid()
        objNotificationType = New NotificationType
        Dim dt As DataTable
        Dim strNotifications As String = version.GetNotificationTypes()
        dt = objNotificationType.GetAll()
        If Not strNotifications = String.Empty Then
            objNotificationType.strNotificationTypeId = strNotifications
            dt = objNotificationType.GetBystrNotificationTypeId
        End If
        dgrdNotificationType.DataSource = dt
        dgrdNotificationType.DataBind()


    End Sub

    Private Sub FillList()
        objNotificationType = New NotificationType
        objNotificationType.NotificationTypeId = NotificationID
        CtlCommon.FillTelerikDropDownList(RadCmbBxParameters, objNotificationType.GetNotificationParametersForDDL(), Lang.EN)
        CtlCommon.FillTelerikDropDownList(RadCmbBxParametersArb, objNotificationType.GetNotificationParametersForDDL(), Lang.AR)
        CtlCommon.FillTelerikDropDownList(RadSMSCmbBxParameters, objNotificationType.GetNotificationParametersForDDL(), Lang.EN)
        CtlCommon.FillTelerikDropDownList(RadSMSCmbBxParametersArb, objNotificationType.GetNotificationParametersForDDL(), Lang.AR)

    End Sub

    Private Sub ClearControl()
        chkHasEmail.Checked = False
        chkHasSMS.Checked = False
        chkSendReportToManager.Checked = False
        chkSendToEmployee.Checked = False
        chkSendToReportHR.Checked = False
        RadCmbBxParameters.Items.FindItemByValue("-1").Selected = True
        RadSMSCmbBxParameters.Items.FindItemByValue("-1").Selected = True
        txtEmailTemplateAr.Text = String.Empty
        txtEmailTemplateEn.Text = String.Empty
        txtSMSTemplateAr.Text = String.Empty
        txtSMSTemplateEn.Text = String.Empty
        mvNotificationType.SetActiveView(viewNotificationTypeList)
        mvNotificationType.ActiveViewIndex = 0
        NotificationID = 0
        SetTimePickerProperty()
        SetRadDateTimePickerPeoperties()
        rblEmailSendingTime.SelectedValue = 0
        rblSMSSendingTime.SelectedValue = 0
        rblNotificationPoicy.SelectedValue = 0
        dvCoordinatorType.Visible = False
        cblCoordinatorTypes.ClearSelection()
    End Sub

    Private Sub SetTimePickerProperty()

        radtpEmailSpecificTime.TimeView.HeaderText = String.Empty
        radtpSMSSpecificTime.TimeView.HeaderText = String.Empty

        radtpEmailSpecificTime.TimeView.TimeFormat = "HH:mm"
        radtpEmailSpecificTime.TimeView.DataBind()

        radtpSMSSpecificTime.TimeView.TimeFormat = "HH:mm"
        radtpSMSSpecificTime.TimeView.DataBind()
    End Sub

    Private Sub SetRadDateTimePickerPeoperties()
        ' This function set properties for terlerik controls

        'Imports Telerik.Web.UI.DatePickerPopupDirection

        ' Set TimeView properties 
        radtpEmailSpecificTime.TimeView.StartTime = New TimeSpan(0, 0, 0)
        radtpEmailSpecificTime.TimeView.EndTime = New TimeSpan(23.5, 0, 0)
        radtpEmailSpecificTime.TimeView.HeaderText = String.Empty
        radtpEmailSpecificTime.TimeView.Interval = New TimeSpan(0, 30, 0)
        radtpEmailSpecificTime.TimeView.Columns = 5



        ' Set Popup window properties
        radtpEmailSpecificTime.PopupDirection = TopRight
        radtpEmailSpecificTime.ShowPopupOnFocus = True

        ' Set default value
        radtpEmailSpecificTime.SelectedDate = Now

        ' Set TimeView properties 
        radtpSMSSpecificTime.TimeView.StartTime = New TimeSpan(0, 0, 0)
        radtpSMSSpecificTime.TimeView.EndTime = New TimeSpan(23.5, 0, 0)
        radtpSMSSpecificTime.TimeView.Interval = New TimeSpan(0, 30, 0)
        radtpSMSSpecificTime.TimeView.HeaderText = String.Empty
        radtpSMSSpecificTime.TimeView.Columns = 5

        ' Set Popup window properties
        radtpSMSSpecificTime.PopupDirection = TopRight
        radtpSMSSpecificTime.ShowPopupOnFocus = True

        ' Set default value
        radtpSMSSpecificTime.SelectedDate = Now

    End Sub

    Private Function IntegertoTime(ByVal intTime As Integer) As DateTime
        Dim hours As Integer = intTime \ 60
        Dim minutes As Integer = intTime - (hours * 60)
        Dim timeElapsed As String = CType(hours, String) & ":" & CType(minutes, String)
        Return timeElapsed
    End Function

    Private Sub FillLevels()
        objOrgLevel = New OrgLevel
        With objOrgLevel
            CtlCommon.FillTelerikDropDownList(radcmbLevels, .GetAll, Lang)
        End With
    End Sub

    Private Function FormatOccurrence() As String
        Dim FormattedString As String = Nothing

        If radnumFirstOccurrence.Text = Nothing And (radnumSecondOccurrence.Text <> Nothing Or radnumThirdOccurrence.Text <> Nothing) Then
            CtlCommon.ShowMessage(Me.Page, "Please Insert First Occurrence Value", "info")
            Return -1
        Else
            If Not radnumFirstOccurrence.Text = Nothing Then
                FormattedString = radnumFirstOccurrence.Text
            End If
        End If

        If radnumSecondOccurrence.Text = Nothing And radnumThirdOccurrence.Text <> Nothing Then
            CtlCommon.ShowMessage(Me.Page, "Please Insert Second Occurrence Value", "info")
            Return -1
        Else
            If Not radnumSecondOccurrence.Text = Nothing Then
                FormattedString = FormattedString & "," & radnumSecondOccurrence.Text
            End If
        End If

        If Not radnumThirdOccurrence.Text = Nothing And (radnumFirstOccurrence.Text = Nothing And radnumSecondOccurrence.Text = Nothing) Then
            CtlCommon.ShowMessage(Me.Page, "Please Insert First And Second Occurrence Value", "info")
            Return -1
        Else
            If Not radnumThirdOccurrence.Text = Nothing Then
                FormattedString = FormattedString & "," & radnumThirdOccurrence.Text
            End If
        End If
    

        Return FormattedString
    End Function

    Private Sub FillOccurrence()
        objNotificationType = New NotificationType
        With objNotificationType
            .NotificationTypeId = NotificationID
            .GetByPK()
            Dim s As String = .NotificationPolicy
            If Not .NotificationPolicy = Nothing Then
                Dim arrOccurrence As New ArrayList
                For Each value As String In s.Split(","c)
                    arrOccurrence.Add(Convert.ToInt32(value.ToString.Trim))
                Next
                If arrOccurrence.Count >= 1 Then
                    radnumFirstOccurrence.Text = arrOccurrence(0).ToString
                End If
                If arrOccurrence.Count >= 2 Then
                    radnumSecondOccurrence.Text = arrOccurrence(1).ToString
                End If
                If arrOccurrence.Count >= 3 Then
                    radnumThirdOccurrence.Text = arrOccurrence(2).ToString
                End If

            End If
        End With
    End Sub

    Private Sub FillCheck_CoordinatorTypes()
        cblCoordinatorTypes.ClearSelection()
        Dim dt As DataTable
        objCoordinator_Type = New Coordinator_Type
        With objCoordinator_Type
            dt = .GetAll
            If (dt IsNot Nothing) Then
                Dim dtCoordinators As DataTable = dt
                If (dtCoordinators IsNot Nothing) Then
                    If (dtCoordinators.Rows.Count > 0) Then

                        Dim dtSource As New DataTable
                        dtSource.Columns.Add("CoordinatorTypeId")
                        dtSource.Columns.Add("CoordinatorTypeName")
                        Dim drRow As DataRow
                        drRow = dtSource.NewRow()
                        For Item As Integer = 0 To dtCoordinators.Rows.Count - 1
                            Dim drSource As DataRow
                            drSource = dtSource.NewRow
                            Dim dcCell1 As New DataColumn
                            Dim dcCell2 As New DataColumn
                            dcCell1.ColumnName = "CoordinatorTypeId"
                            dcCell2.ColumnName = "CoordinatorTypeName"
                            dcCell1.DefaultValue = dtCoordinators.Rows(Item)(0)
                            dcCell2.DefaultValue = dtCoordinators.Rows(Item)("CoordinatorShortName") + "-" + IIf(Lang = CtlCommon.Lang.EN, dtCoordinators.Rows(Item)("CoordinatorTypeName"), dtCoordinators.Rows(Item)("CoordinatorTypeArabicName"))
                            drSource("CoordinatorTypeId") = dcCell1.DefaultValue
                            drSource("CoordinatorTypeName") = dcCell2.DefaultValue

                            dtSource.Rows.Add(drSource)
                        Next

                        Dim dv As New DataView(dtSource)
                        If SessionVariables.CultureInfo = "ar-JO" Then
                            For Each row As DataRowView In dv
                                cblCoordinatorTypes.Items.Add(New ListItem(row("CoordinatorTypeName").ToString(), row("CoordinatorTypeId").ToString()))
                            Next
                        Else
                            For Each row As DataRowView In dv
                                cblCoordinatorTypes.Items.Add(New ListItem(row("CoordinatorTypeName").ToString(), row("CoordinatorTypeId").ToString()))
                            Next
                        End If

                    End If
                End If
            End If
        End With
    End Sub

#End Region

#Region "Grid Filter"

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdNotificationType.Skin))
    End Function

    Protected Sub dgrdNotificationType_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdNotificationType.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

#End Region

End Class
