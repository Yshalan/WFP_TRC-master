Imports SmartV.UTILITIES
Imports TA.Employees
Imports TA.Admin
Imports Telerik.Web.UI
Imports System.Data

Partial Class Requests_UserControls_HR_HR_OverTimeApproval
    Inherits System.Web.UI.UserControl

#Region "Class Variables"
    Private objEmp_OverTimeRule As Emp_OverTimeRule
    Private Lang As CtlCommon.Lang
    Private objEmp_Leaves As Emp_Leaves
    Dim objAPP_Settings As New APP_Settings
    Public MsgLang As String
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo

    Private Enum RequestStatus
        Pending = 1
        ApprovedByDM = 2
        ApprovedByHR = 3
        RejectedByDM = 4
        RejectedByHR = 5
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
                dgrdEmpLeaveRequest.Columns(5).Visible = True
            Else
                dgrdEmpLeaveRequest.Columns(5).Visible = False
            End If
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            lblOverTimeRequests.Text = ResourceManager.GetString("HROverApprove", CultureInfo)
            Page.Title = "Work Force Pro : :" + ResourceManager.GetString("HROverApprove", CultureInfo)


            ShowHideControls()
            FillGridView()
        End If

    End Sub

    Protected Sub lnkAccept_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim strMessage As String = String.Empty
        Dim Err As Integer = 0
        Dim objOvertimeRules As OvertimeRules
        objOvertimeRules = New OvertimeRules
        objEmp_OverTimeRule = New Emp_OverTimeRule
        objOvertimeRules.OvertimeRuleId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FK_OvertimeRuleId"))
        objAPP_Settings = New APP_Settings()
        objAPP_Settings.GetByPK()
        With objOvertimeRules
            .GetByPK()
            If (.OverTimeApprovalBy = 2 Or .OverTimeApprovalBy = 3) Then
                With objEmp_OverTimeRule
                    .FK_StatusId = RequestStatus.ApprovedByHR
                    .EmpOvertimeId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("EmpOvertimeId"))
                    Dim gvr As GridDataItem = CType(sender, LinkButton).Parent.Parent
                    If objAPP_Settings.AllowEditOverTime = True Then
                        'Dim x As String = CType(dgrdEmpLeaveRequest.Items(gvr.ItemIndex).FindControl("txtApprovedDuration"), RadNumericTextBox).Text
                        .ApprovedDuration = CInt(hdnDuration.Value)
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
            End If
        End With
    End Sub

    Protected Sub lnkReject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReject.Click
        Dim strMessage As String = String.Empty
        Dim Err As Integer = 0
        objAPP_Settings = New APP_Settings
        objEmp_OverTimeRule = New Emp_OverTimeRule
        With objEmp_OverTimeRule
            .EmpOvertimeId = OvertimeID
            .GetByPK()
            .FK_StatusId = RequestStatus.RejectedByHR
            .RejectedReason = txtRejectedReason.Text.Trim(",")
            Err = .UpdateOverTimeStatus()
            strMessage = ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)
            If Err = 0 Then
                FillGridView()
                CtlCommon.ShowMessage(Me.Page, strMessage, "success")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "info")
            End If
        End With
    End Sub

    Protected Sub dgrdEmpLeaveRequest_OnItemCommand(ByVal sender As Object, ByVal e As GridCommandEventArgs) Handles dgrdEmpLeaveRequest.ItemCommand
        If (e.CommandName = "reject") Then
            txtRejectedReason.Text = String.Empty
            OvertimeID = e.CommandArgument
            mpeRejectPopupOverTime.Show()
        End If
    End Sub

    Protected Sub dgrdEmpLeaveRequest_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles dgrdEmpLeaveRequest.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
            Dim item As GridDataItem
            item = e.Item

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("FromDateTime").ToString())) And (Not item.GetDataKeyValue("FromDateTime").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("FromDateTime")
                'item("FromDateTime").Text = fromDate.ToShortDateString()
                item("FromDateTime").Text = Convert.ToDateTime(fromDate).ToString("dd/MM/yyyy")
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("ToDateTime").ToString())) And (Not item.GetDataKeyValue("ToDateTime").ToString() = "")) Then
                Dim fromDate As DateTime = item.GetDataKeyValue("ToDateTime")
                'item("ToDateTime").Text = fromDate.ToShortDateString()
                item("ToDateTime").Text = Convert.ToDateTime(fromDate).ToString("dd/MM/yyyy")
            End If

            If Lang = CtlCommon.Lang.AR Then
                item("EmployeeName").Text = DirectCast(item.FindControl("hdnEmployeeNameAr"), HiddenField).Value
                item("StatusName").Text = DirectCast(item.FindControl("hdnStatusNameAr"), HiddenField).Value
            End If

            If (Not String.IsNullOrEmpty(item.GetDataKeyValue("Duration").ToString())) Then
                Dim Itm As GridDataItem = e.Item
                If (Not String.IsNullOrEmpty(item.GetDataKeyValue("ApprovedDuration").ToString())) Then
                    CType(Itm.FindControl("txtApprovedDuration"), RadNumericTextBox).Value = item.GetDataKeyValue("ApprovedDuration").ToString()
                End If
                Dim ApprovedDuration = CType(Itm.FindControl("txtApprovedDuration"), RadNumericTextBox).Value
                Dim ApprovedDuration2 = e.Item.DataItem("ApprovedDuration")
                hdnDuration.Value = ApprovedDuration2.ToString()
            End If
        End If

    End Sub

#End Region

#Region "Methods"

    Public Sub FillGridView()
        Try
            Dim dtOverRequest As New DataTable
            objEmp_OverTimeRule = New Emp_OverTimeRule()
            objEmp_OverTimeRule.FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId
            objEmp_OverTimeRule.FK_StatusId = RequestStatus.ApprovedByDM
            'dgrdEmpLeaveRequest.DataSource = objEmp_OverTimeRule.GetByDirectManager()
            dtOverRequest = objEmp_OverTimeRule.GetByHR()
            dgrdEmpLeaveRequest.DataSource = dtOverRequest
            dgrdEmpLeaveRequest.DataBind()

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

           For Each item As GridDataItem In dgrdEmpLeaveRequest.Items
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
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim strMessage As String = String.Empty
        Dim Err As Integer = 0
        Dim objOvertimeRules As OvertimeRules
        objOvertimeRules = New OvertimeRules
        objEmp_OverTimeRule = New Emp_OverTimeRule
        objOvertimeRules.OvertimeRuleId = CInt(item.GetDataKeyValue("FK_OvertimeRuleId"))
        objAPP_Settings = New APP_Settings()
        objAPP_Settings.GetByPK()
        With objOvertimeRules
            .GetByPK()
            If (.OverTimeApprovalBy = 2 Or .OverTimeApprovalBy = 3) Then
                With objEmp_OverTimeRule
                    .FK_StatusId = RequestStatus.ApprovedByHR
                    .EmpOvertimeId = CInt(item.GetDataKeyValue("EmpOvertimeId"))

                    If objAPP_Settings.AllowEditOverTime = True Then
                        'Dim x As String = CType(dgrdEmpLeaveRequest.Items(gvr.ItemIndex).FindControl("txtApprovedDuration"), RadNumericTextBox).Text
                        .ApprovedDuration = CInt(hdnDuration.Value)
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
            End If
        End With
        ErrNo = Err
        Msg = strMessage
        item = Nothing
    End Sub

    Private Sub RejectAll(ByVal item As GridDataItem, ByRef ErrNo As Integer, ByRef Msg As String)
        Dim strMessage As String = String.Empty
        Dim Err As Integer = 0
        objAPP_Settings = New APP_Settings
        objEmp_OverTimeRule = New Emp_OverTimeRule
        With objEmp_OverTimeRule
            .EmpOvertimeId = CInt(item.GetDataKeyValue("FK_OvertimeRuleId"))
            .GetByPK()
            .FK_StatusId = RequestStatus.RejectedByHR
            .RejectedReason = txtRejectAllReason.Text.Trim(",")
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
