Imports SmartV.UTILITIES
Imports TA.Employees
Imports TA.Admin
Imports Telerik.Web.UI
Imports TA.Security
Imports System.Data
Imports TA.DailyTasks

Partial Class Requests_UserControls_DM_DM_OverTimeApproval
    Inherits System.Web.UI.UserControl


#Region "Class Variables"
    Private objEmp_OverTimeRule As New Emp_OverTimeRule
    Private objEmp_OverTime As Emp_Overtime
    Private Lang As CtlCommon.Lang
    Private objEmp_Leaves As Emp_Leaves
    Dim objAPP_Settings As New APP_Settings
    Public MsgLang As String
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Shared SortExepression As String
    Shared dtCurrent As DataTable
    Private objRecalculateRequest As RecalculateRequest

    Private Enum RequestStatus
        Pending = 1
        ApprovedByDM = 2
        ApprovedByHR = 3
        RejectedByDM = 4
        RejectedByHR = 5
        ApprovedBy2ndMgr = 12
        RejectedBy2ndMgr = 13

    End Enum
#End Region

#Region "Properties"

    Public Property OvertimeID() As String
        Get
            Return ViewState("EmpOvertimeId")
        End Get
        Set(ByVal value As String)
            ViewState("EmpOvertimeId") = value
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
            MsgLang = "ar"
        Else
            Lang = CtlCommon.Lang.EN
            MsgLang = "en"
        End If

        If Not IsPostBack Then
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            mvLeaveApproval.SetActiveView(viewDMApproval)
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                MsgLang = "ar"
            Else
                Lang = CtlCommon.Lang.EN
                MsgLang = "en"
            End If
            objAPP_Settings = New APP_Settings()
            objAPP_Settings.GetByPK()
            If objAPP_Settings.AllowEditOverTime = True Then
                btnAddOverTime.Visible = True
                dgrdEmpOverTimeRequest.Columns(7).Visible = True
            Else
                btnAddOverTime.Visible = False
                dgrdEmpOverTimeRequest.Columns(7).Visible = False
            End If

            lblOverTimeRequests.Text = ResourceManager.GetString("DirectMgrOverHeader", CultureInfo)
            Page.Title = "Work Force Pro : :" + ResourceManager.GetString("DirectMgrOverHeader", CultureInfo)
            Me.dteFromDate.SelectedDate = Today
            ShowAddButton()

            ShowHideControls()
            FillGridView()
        End If

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not trControls.FindControl(row("AddBtnName")) Is Nothing Then
                        trControls.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not trControls.FindControl(row("DeleteBtnName")) Is Nothing Then
                        trControls.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not trControls.FindControl(row("EditBtnName")) Is Nothing Then
                        trControls.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not trControls.FindControl(row("PrintBtnName")) Is Nothing Then
                        trControls.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next


    End Sub

    Protected Sub lnkAccept_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim strMessage As String = String.Empty
        Dim Err As Integer = 0
        Dim objOvertimeRules As OvertimeRules
        objOvertimeRules = New OvertimeRules
        objEmp_OverTimeRule = New Emp_OverTimeRule
        objEmp_OverTime = New Emp_Overtime

        Dim NextApprovalStatus As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("NextApprovalStatus").ToString())

        objOvertimeRules.OvertimeRuleId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FK_OvertimeRuleId").ToString())
        With objOvertimeRules
            .GetByPK()
            If (.OverTimeApprovalBy = 1 Or .OverTimeApprovalBy = 5) Then
                With objEmp_OverTimeRule
                    .FK_StatusId = NextApprovalStatus
                    .EmpOvertimeId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("EmpOvertimeId").ToString())
                    Dim gvr As GridDataItem = CType(sender, LinkButton).Parent.Parent
                    If objAPP_Settings.AllowEditOverTime = True Then
                        .ApprovedDuration = CInt(CType(dgrdEmpOverTimeRequest.Items(gvr.ItemIndex).FindControl("txtApprovedDuration"), RadNumericTextBox).Text)
                    Else
                        objEmp_OverTime.EmpOverTimeId = .EmpOvertimeId
                        objEmp_OverTime.GetByPK()
                        .ApprovedDuration = objEmp_OverTime.Duration
                    End If

                    .IsFinallyApproved = True

                    Err = .UpdateOverTimeStatus()

                    strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)

                    If Err = 0 Then
                        FillGridView()
                        CtlCommon.ShowMessage(Me.Page, strMessage, "success")
                    Else
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")

                    End If
                End With


            ElseIf (.OverTimeApprovalBy = 3) Then
                With objEmp_OverTimeRule
                    .FK_StatusId = NextApprovalStatus
                    .EmpOvertimeId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("EmpOvertimeId").ToString())
                    Dim gvr As GridDataItem = CType(sender, LinkButton).Parent.Parent
                    If objAPP_Settings.AllowEditOverTime = True Then
                        .ApprovedDuration = CInt(CType(dgrdEmpOverTimeRequest.Items(gvr.ItemIndex).FindControl("txtApprovedDuration"), RadNumericTextBox).Text)
                    Else
                        objEmp_OverTime.EmpOverTimeId = .EmpOvertimeId
                        objEmp_OverTime.GetByPK()
                        .ApprovedDuration = objEmp_OverTime.Duration
                    End If
                    .IsFinallyApproved = False
                    Err = .UpdateOverTimeStatus()

                    strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)

                    If Err = 0 Then
                        FillGridView()
                        CtlCommon.ShowMessage(Me.Page, strMessage, "success")
                    Else
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")
                    End If
                End With
            End If
        End With
    End Sub

    Protected Sub lnkReject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReject.Click
        Dim strMessage As String = String.Empty
        Dim Err As Integer = 0
        Dim NextApprovalStatus As Integer = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("NextApprovalStatus").ToString())

        objAPP_Settings = New APP_Settings
        With objEmp_OverTimeRule
            If NextApprovalStatus = 12 Then
                .FK_StatusId = RequestStatus.RejectedBy2ndMgr
            Else
                .FK_StatusId = RequestStatus.RejectedByDM
            End If
            .RejectedReason = txtRejectedReason.Text.Trim(",")
            .EmpOvertimeId = OvertimeID
            '.EmpOvertimeId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem)("EmpOvertimeId").Text)
            Err = .UpdateOverTimeStatus()

            strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)

            If Err = 0 Then
                FillGridView()
                CtlCommon.ShowMessage(Me.Page, strMessage, "success")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")
            End If
        End With
    End Sub

    Protected Sub dgrdEmpOverTimeRequest_OnItemCommand(ByVal sender As Object, ByVal e As GridCommandEventArgs) Handles dgrdEmpOverTimeRequest.ItemCommand
        If (e.CommandName = "reject") Then
            txtRejectedReason.Text = String.Empty
            OvertimeID = e.CommandArgument
            mpeRejectPopupOverTime.Show()
        End If
    End Sub

    Protected Sub dgrdEmpOverTimeRequest_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles dgrdEmpOverTimeRequest.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
            Dim item As GridDataItem
            item = e.Item

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("Date").ToString())) And (Not item.GetDataKeyValue("Date").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("Date").ToString()
                'item("Date").Text = fromDate.ToShortDateString()
                item("Date").Text = Convert.ToDateTime(fromDate).ToString("dd/MM/yyyy")
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("FromDateTime").ToString())) And (Not item.GetDataKeyValue("FromDateTime").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("FromDateTime").ToString()
                'item("FromDateTime").Text = fromDate.ToShortTimeString()
            End If

            If Lang = CtlCommon.Lang.AR Then
                item("EmployeeName").Text = DirectCast(item.FindControl("hdnEmployeeNameAr"), HiddenField).Value
                item("StatusName").Text = DirectCast(item.FindControl("hdnStatusNameAr"), HiddenField).Value
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("ToDateTime").ToString())) And (Not item.GetDataKeyValue("ToDateTime").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("ToDateTime").ToString()
                'item("ToDateTime").Text = fromDate.ToShortTimeString()
            End If
        End If
    End Sub

    Protected Sub btnAddOverTime_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddOverTime.Click
        mvLeaveApproval.ActiveViewIndex = 1
    End Sub

    Protected Sub btnSaveOverTime_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveOverTime.Click
        Dim err As Integer = -1
        Dim objEmp_OverTimeRule As New Emp_OverTimeRule
        Dim objEmp_OverTime As New Emp_Overtime
        objEmp_OverTimeRule.FK_EmployeeId = objEmpFilter.EmployeeId
        objEmp_OverTimeRule.GetActiveRuleId()

        If objEmp_OverTimeRule Is Nothing Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NoActiveOverTime", CultureInfo), "info")
            Return
        End If

        Dim dtEmpAllowedOT As DataTable = objEmp_OverTimeRule.GetAllowedOTRule()
        If dtEmpAllowedOT IsNot Nothing Then
            If dtEmpAllowedOT.Rows.Count > 0 Then
                Dim isAllowed As Boolean = Convert.ToBoolean(dtEmpAllowedOT.Rows(0)("OvertimeEligibility"))
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NotAllowedOverTime", CultureInfo), "info")
                Return
            End If
        End If

        With objEmp_OverTime
            .FK_EmployeeId = objEmpFilter.EmployeeId
            .FK_OvertimeRuleId = objEmp_OverTimeRule.FK_RuleId
            .FromDateTime = dteFromDate.DbSelectedDate
            .ToDateTime = dteFromDate.DbSelectedDate
            .Duration = txtDuration.Text
            .ApprovedDuration = txtDuration.Text
            .IsHigh = chkHighTime.Checked
            .ProcessStatus = RequestStatus.ApprovedByDM
            .CREATED_BY = SessionVariables.LoginUser.ID
            err = .Add()
        End With
        ClearOvertimeControls()
        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
    End Sub

    Protected Sub btnClearOverTime_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearOverTime.Click
        ClearOvertimeControls()
    End Sub

    Protected Sub dgrdEmpOverTimeRequest_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdEmpOverTimeRequest.NeedDataSource
        objEmp_OverTimeRule = New Emp_OverTimeRule()
        objEmp_OverTimeRule.FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
        objEmp_OverTimeRule.FK_StatusId = RequestStatus.Pending
        dtCurrent = objEmp_OverTimeRule.GetByDirectManager()
        Dim dv As New DataView(dtCurrent)
        dv.Sort = SortExepression
        dgrdEmpOverTimeRequest.DataSource = dv
    End Sub

#End Region

#Region "Methods"

    Public Sub FillGridView()
        Try
            Dim dtOverRequest As New DataTable
            objEmp_OverTimeRule = New Emp_OverTimeRule()
            objEmp_OverTimeRule.FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
            objEmp_OverTimeRule.FK_StatusId = RequestStatus.Pending
            dtOverRequest = objEmp_OverTimeRule.GetByDirectManager()
            dgrdEmpOverTimeRequest.DataSource = dtOverRequest
            dgrdEmpOverTimeRequest.DataBind()

            If Not dtOverRequest Is Nothing Then
                If dtOverRequest.Rows.Count > 0 Then
                    If Not lblRequestNo.Text.Contains(":") Then
                        lblRequestNo.Text += " : " + (dtOverRequest.Rows.Count).ToString()
                    Else
                        Dim strArr() As String = lblRequestNo.Text.Split(":")
                        lblRequestNo.Text = strArr(0) + " : " + (dtOverRequest.Rows.Count).ToString()
                    End If
                Else
                    lblRequestNo.Visible = False
                    divrdbProceed.Visible = False
                    divbtnProceed.Visible = False
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ClearOvertimeControls()
        objEmpFilter.ClearValues()
        txtDuration.Text = String.Empty
        Me.dteFromDate.SelectedDate = Today
        chkHighTime.Checked = False
    End Sub

    Private Sub ShowAddButton()
        Dim RequestGridToAppear As String = ""
        With objAPP_Settings
            .GetByPK()
            For Each i As String In .RequestGridToAppear.Split(",")
                If i = "6" Then
                    btnAddOverTime.Visible = True
                End If
            Next
        End With
    End Sub
#End Region

#Region "Proceed All"

    Protected Sub btnProceed_Click(sender As Object, e As System.EventArgs) Handles btnProceed.Click
        Dim checked As Boolean
        Dim ErrNo As Integer = 1
        Dim Msg As String = ""
        Dim Count As Integer = 0
        If rdbProceed.SelectedValue = "" Then
            If SessionVariables.CultureInfo = "ar-JO" Then
                CtlCommon.ShowMessage(Page, " الرجاء اختيار عملية معينة", "info")
            Else
                CtlCommon.ShowMessage(Page, "Please select one Process", "info")
            End If
            Exit Sub
        End If
        If rdbProceed.SelectedValue = 2 And txtRejectAllReason.Text = "" Then
            If SessionVariables.CultureInfo = "ar-JO" Then
                CtlCommon.ShowMessage(Page, "الرجاء ادخال سبب الرفض", "info")
            Else
                CtlCommon.ShowMessage(Page, "Please Insert Rejection Reason", "info")
            End If
            Exit Sub
        End If

        For Each item As GridDataItem In dgrdEmpOverTimeRequest.Items
            If DirectCast(item.FindControl("chk"), CheckBox).Checked = True Then
                If rdbProceed.SelectedValue = 1 Then
                    AcceptAll(item, ErrNo, Msg)
                ElseIf rdbProceed.SelectedValue = 2 Then
                    RejectAll(item, ErrNo, Msg)
                End If
                Count = Count + 1
            End If
        Next

        If Count > 0 Then


            If ErrNo = 0 Then
                FillGridView()
                txtRejectAllReason.Text = String.Empty
                rdbProceed.SelectedIndex = "-1"
                divRejectAllReason.Visible = False
                CtlCommon.ShowMessage(Me.Page, Msg, "success")
            ElseIf ErrNo = 1 Then

            Else

                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")

            End If
        Else
            If SessionVariables.CultureInfo = "ar-JO" Then
                CtlCommon.ShowMessage(Page, " الرجاء اختيار طلب مغادرة واحد على الاقل", "info")
            Else
                CtlCommon.ShowMessage(Page, "Please select one Permission Request at least", "info")
            End If

        End If
    End Sub

    Private Sub AcceptAll(ByVal item As GridDataItem, ByRef ErrNo As Integer, ByRef Msg As String)
        Dim strMessage As String = String.Empty
        Dim Err As Integer = 0
        Dim objOvertimeRules As OvertimeRules
        objOvertimeRules = New OvertimeRules
        objEmp_OverTimeRule = New Emp_OverTimeRule
        objEmp_OverTime = New Emp_Overtime

        objOvertimeRules.OvertimeRuleId = CInt(item.GetDataKeyValue("FK_OvertimeRuleId").ToString())
        Dim NextApprovalStatus As Integer = CInt(item.GetDataKeyValue("NextApprovalStatus").ToString())

        With objOvertimeRules
            .GetByPK()
            If (.OverTimeApprovalBy = 1 Or .OverTimeApprovalBy = 5) Then
                With objEmp_OverTimeRule
                    .FK_StatusId = NextApprovalStatus
                    .EmpOvertimeId = CInt(item.GetDataKeyValue("EmpOvertimeId").ToString())

                    If objAPP_Settings.AllowEditOverTime = True Then
                        .ApprovedDuration = CInt(CType(dgrdEmpOverTimeRequest.Items(item.ItemIndex).FindControl("txtApprovedDuration"), RadNumericTextBox).Text)
                    Else
                        objEmp_OverTime.EmpOverTimeId = .EmpOvertimeId
                        objEmp_OverTime.GetByPK()
                        .ApprovedDuration = objEmp_OverTime.Duration
                    End If

                    .IsFinallyApproved = True

                    Err = .UpdateOverTimeStatus()

                    strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)

                    'If Err = 0 Then
                    '    FillGridView()
                    '    CtlCommon.ShowMessage(Me.Page, strMessage)
                    'Else
                    '    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo))

                    'End If
                End With
                'ElseIf (.OverTimeApprovalBy = 2) Then

                '    With objEmp_OverTimeRule
                '        .FK_StatusId = RequestStatus.ApprovedByHR
                '        .EmpOvertimeId = CInt(item("EmpOvertimeId").Text)
                '        Dim gvr As GridDataItem = CType(sender, LinkButton).Parent.Parent
                '        .ApprovedDuration = CInt(CType(dgrdEmpOverTimeRequest.Items(gvr.ItemIndex).FindControl("txtApprovedDuration"), TextBox).Text)
                '        Err = .UpdateOverTimeStatus()

                '        strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)

                '        If Err = 0 Then
                '            FillGridView()
                '            CtlCommon.ShowMessage(Me.Page, strMessage)
                '        Else
                '            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo))

                '        End If
                '    End With
            ElseIf (.OverTimeApprovalBy = 3) Then
                With objEmp_OverTimeRule
                    .FK_StatusId = NextApprovalStatus
                    .EmpOvertimeId = CInt(item.GetDataKeyValue("EmpOvertimeId").ToString())

                    If objAPP_Settings.AllowEditOverTime = True Then
                        .ApprovedDuration = CInt(CType(dgrdEmpOverTimeRequest.Items(item.ItemIndex).FindControl("txtApprovedDuration"), RadNumericTextBox).Text)
                    Else
                        objEmp_OverTime.EmpOverTimeId = .EmpOvertimeId
                        objEmp_OverTime.GetByPK()
                        .ApprovedDuration = objEmp_OverTime.Duration
                    End If
                    .IsFinallyApproved = False
                    Err = .UpdateOverTimeStatus()

                    strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)

                    'If Err = 0 Then
                    '    FillGridView()
                    '    CtlCommon.ShowMessage(Me.Page, strMessage)
                    'Else
                    '    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo))
                    'End If
                End With
            End If
        End With
        ErrNo = Err
        Msg = strMessage
        item = Nothing
    End Sub

    Private Sub RejectAll(ByVal item As GridDataItem, ByRef ErrNo As Integer, ByRef Msg As String)
        Dim strMessage As String = String.Empty
        Dim Err As Integer = 0
        Dim NextApprovalStatus As Integer = CInt(item.GetDataKeyValue("NextApprovalStatus").ToString())
        objAPP_Settings = New APP_Settings
        With objEmp_OverTimeRule
            If NextApprovalStatus = 12 Then
                .FK_StatusId = RequestStatus.RejectedBy2ndMgr
            Else
                .FK_StatusId = RequestStatus.RejectedByDM
            End If

            .RejectedReason = txtRejectAllReason.Text.Trim(",")
            .EmpOvertimeId = CInt(item.GetDataKeyValue("EmpOvertimeId").ToString())
            '.EmpOvertimeId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem)("EmpOvertimeId").Text)
            Err = .UpdateOverTimeStatus()

            strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)
            ErrNo = Err
            Msg = strMessage
            item = Nothing
            'If Err = 0 Then
            '    FillGridView()
            '    CtlCommon.ShowMessage(Me.Page, strMessage)
            'Else
            '    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo))
            'End If
        End With

    End Sub

    Protected Sub rdbProceed_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rdbProceed.SelectedIndexChanged
        If rdbProceed.SelectedValue = 2 Then
            divRejectAllReason.Visible = True
        Else
            divRejectAllReason.Visible = False
        End If
    End Sub

    Private Sub ShowHideControls()
        objAPP_Settings = New APP_Settings

        With objAPP_Settings
            .GetByPK()
            If .HasMultiApproval Then
                divrdbProceed.Visible = True
                divbtnProceed.Visible = True
            Else
                divrdbProceed.Visible = False
                divbtnProceed.Visible = False
            End If
        End With
    End Sub
#End Region

End Class
