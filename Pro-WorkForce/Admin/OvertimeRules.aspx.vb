Imports System.Data
Imports SmartV.UTILITIES
Imports TA.Admin
Imports TA.Security
Imports TA.OverTime

Partial Class Admin_OvertimeRules
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objOvertimeRules As OvertimeRules
    Private objOvertime_Types As Overtime_Types

    Shared OvertimeRuleId As Integer
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Public MsgLang As String

#End Region

#Region "Properties"

    Private Property DtTime() As DataTable
        Get
            Return ViewState("DtTime")
        End Get
        Set(ByVal value As DataTable)
            ViewState("DtTime") = value
        End Set
    End Property

    Public Property HighTimeId() As Integer
        Get
            Return ViewState("HighTimeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("HighTimeId") = value
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

#End Region

#Region "Page events"

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
        showHide(False)
        If Not Page.IsPostBack Then
            trApprovalBy.Visible = False
            rlstApproval.SelectedValue = 1
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                MsgLang = "ar"
            Else
                Lang = CtlCommon.Lang.EN
                MsgLang = "en"
            End If

            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            tabHighTime.Visible = False
            FillGrid()
            FillDropDowns()
            PageHeader1.HeaderText = ResourceManager.GetString("DefOverRules", CultureInfo)
        End If
        ibtnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdOverTimeRules.ClientID + "')")
        btnAdd.Attributes.Add("onclick", "javascript:return ValidateFromTo(1)")

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

    Protected Sub dgrdOverTimeRules_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdOverTimeRules.Skin))
    End Function

    Protected Sub ibtnRest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnRest.Click
        FillGrid()
        ClearAll()
    End Sub

    Private Sub showHide(ByVal status As Boolean, Optional ByVal order As Integer = 0)
        If order = 0 Then
            pnlDeduct.Visible = status
        End If
    End Sub

    Protected Sub ibtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnSave.Click
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        objOvertimeRules = New OvertimeRules
        Dim err As Integer
        With objOvertimeRules
            'If rdbOTLeaveOrFinance.SelectedValue = 1 Then
            '    .HighDayLeaveEquivalent = Val(TxtHighRate.Text.Trim)
            '    .LowDayLeaveEquivalent = Val(TxtLowRate.Text.Trim)
            .HighRate = 0
            .LowRate = 0
            .IsFinancial = False
            '.IsLeaveBalance = True
            'Else
            '    .HighRate = Val(TxtHighRate.Text.Trim)
            '    .LowRate = Val(TxtLowRate.Text.Trim)
            .HighDayLeaveEquivalent = 0
            .LowDayLeaveEquivalent = 0
            '    .IsFinancial = True
            .IsLeaveBalance = False
            ' End If
            .ApprovalRequired = rdbApprovalReqd.SelectedValue

            .FK_NormalTypeId = RadComboBoxNormalDay.SelectedValue
            .FK_OffDayTypeId = RadComboBoxOffDay.SelectedValue
            .FK_HolidayTypeId = RadComboBoxHoliday.SelectedValue
            .FK_ReligionHolidayTypeId = RadComboBoxReligionHoliday.SelectedValue

            '.HolidayIsHigh = rdbConsiderHoliday.SelectedValue
            '.OffDayIsHigh = rdbconsiderOffDay.SelectedValue
            '.IsCompensateLatetime = rdbOtCompLateTime.SelectedValue
            .HighHasTime = rdbHighHasTime.SelectedValue
            .MinOvertime = Val(TxtMinOvertime.Text.Trim)
            .MaxOvertime = Val(txtMaxOvertime.Text.Trim)
            .OvertimeEligibility = rdbOTEligibility.SelectedValue
            .RuleName = TxtRuleName.Text.Trim
            .RuleArabicName = txtRuleArName.Text
            .BeforeAfterSchedule = rblManageOvertime.SelectedValue
            .OverTimeApprovalBy = rlstApproval.SelectedValue
            .isLostFromHighOT = chkDeduct.Checked
            .MinAutoApproveDuration = txtRequiredDuration.Text
        End With
        If OvertimeRuleId = 0 Then
            err = objOvertimeRules.Add()
            If err = 0 Then
                OvertimeRuleId = objOvertimeRules.OvertimeRuleId
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            ElseIf err = -5 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EnNameExist", CultureInfo), "info")
            ElseIf err = -6 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If

        Else
            objOvertimeRules.OvertimeRuleId = OvertimeRuleId
            err = objOvertimeRules.Update()
            If err = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("UpdatedSuccessfully", CultureInfo), "success")
            ElseIf err = -5 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EnNameExist", CultureInfo), "info")
            ElseIf err = -6 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")
            End If

        End If
        If err = 0 Then
            If rdbHighHasTime.SelectedValue = 1 Then
                Try
                    objOvertimeRules.OvertimeRuleId = OvertimeRuleId
                    objOvertimeRules.Delete_OvertimeRules_HighTime()
                    For Each dr As DataRow In DtTime.Rows
                        dr(1) = OvertimeRuleId
                        dr(2) = CInt(dr(2))
                        dr(3) = CInt(dr(3))
                        dr(4) = CInt(dr(4))
                    Next
                    objOvertimeRules.Add_OvertimeRules_HighTime(DtTime)
                Catch ex As Exception

                End Try
            End If
            FillGrid()
            ClearAll()
        End If

    End Sub

    Protected Sub ibtnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnDelete.Click
        Dim err As Integer
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdOverTimeRules.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                objOvertimeRules = New OvertimeRules()
                objOvertimeRules.OvertimeRuleId = Convert.ToInt32(row.GetDataKeyValue("OvertimeRuleId"))
                err = objOvertimeRules.Delete()

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

    Protected Sub rdbApprovalReqd_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbApprovalReqd.SelectedIndexChanged
        If (rdbApprovalReqd.SelectedValue = 1) Then
            trApprovalBy.Visible = True
        Else
            trApprovalBy.Visible = False
        End If

    End Sub

    Protected Sub dgrdOverTimeRules_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdOverTimeRules.NeedDataSource
        objOvertimeRules = New OvertimeRules
        dgrdOverTimeRules.DataSource = objOvertimeRules.GetAll()
    End Sub

    Protected Sub dgrdOverTimeRules_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdOverTimeRules.SelectedIndexChanged

        OvertimeRuleId = Convert.ToInt32(DirectCast(dgrdOverTimeRules.SelectedItems(0), GridDataItem).GetDataKeyValue("OvertimeRuleId"))
        objOvertimeRules = New OvertimeRules
        With objOvertimeRules
            .OvertimeRuleId = OvertimeRuleId
            .GetByPK()
            TxtRuleName.Text = .RuleName
            txtRuleArName.Text = .RuleArabicName
            If .OvertimeEligibility Then
                rdbOTEligibility.SelectedValue = 1
                dvOtEligibility.Visible = True
                tabHighTime.Visible = True
                rblManageOvertime.Items.FindByValue(.BeforeAfterSchedule)
                If Not .BeforeAfterSchedule = 0 Then
                    rblManageOvertime.SelectedValue = .BeforeAfterSchedule
                End If
            Else
                rdbOTEligibility.SelectedValue = 0
                dvOtEligibility.Visible = False
                tabHighTime.Visible = False
            End If
            If .ApprovalRequired Then
                rdbApprovalReqd.SelectedValue = 1
                trApprovalBy.Visible = True
                rlstApproval.Items.FindByValue(objOvertimeRules.OverTimeApprovalBy).Selected = True
            Else
                rdbApprovalReqd.SelectedValue = 0
                trApprovalBy.Visible = False
            End If
            If rblManageOvertime.SelectedValue = 4 Then
                ' If .isLostFromHighOT = True Then
                showHide(True)
                chkDeduct.Checked = .isLostFromHighOT
            End If

            RadComboBoxNormalDay.SelectedValue = .FK_NormalTypeId
            RadComboBoxOffDay.SelectedValue = .FK_OffDayTypeId
            RadComboBoxHoliday.SelectedValue = .FK_HolidayTypeId
            RadComboBoxReligionHoliday.SelectedValue = .FK_ReligionHolidayTypeId

            'If .HolidayIsHigh Then rdbConsiderHoliday.SelectedValue = 1 Else rdbConsiderHoliday.SelectedValue = 0
            'If .IsCompensateLatetime Then rdbOtCompLateTime.SelectedValue = 1 Else rdbOtCompLateTime.SelectedValue = 0
            'If .OffDayIsHigh Then rdbconsiderOffDay.SelectedValue = 1 Else rdbconsiderOffDay.SelectedValue = 0
            If .HighHasTime Then rdbHighHasTime.SelectedValue = 1 Else rdbHighHasTime.SelectedValue = 0
            TxtMinOvertime.Text = .MinOvertime
            txtMaxOvertime.Text = .MaxOvertime
            'If .IsLeaveBalance Then
            '    rdbOTLeaveOrFinance.SelectedValue = 1
            '    TxtHighRate.Text = .HighDayLeaveEquivalent
            '    TxtLowRate.Text = .LowDayLeaveEquivalent
            'Else
            '    rdbOTLeaveOrFinance.SelectedValue = 0
            '    TxtHighRate.Text = .HighRate
            '    TxtLowRate.Text = .LowRate
            'End If

            RadComboBoxNormalDay.SelectedValue = .FK_NormalTypeId
            RadComboBoxOffDay.SelectedValue = .FK_OffDayTypeId
            RadComboBoxHoliday.SelectedValue = .FK_HolidayTypeId
            RadComboBoxReligionHoliday.SelectedValue = .FK_ReligionHolidayTypeId
            txtRequiredDuration.Text = .MinAutoApproveDuration

            If .HighHasTime Then
                dvHasTime.Visible = True
                DtTime = .GetAll_OvertimeRules_HighTime()

                'If (DtTime IsNot Nothing AndAlso DtTime.Rows.Count > 0) Then
                '    For Each dr As DataRow In DtTime.Rows
                '        dr(2) = CInt(CInt(dr(2).Split(":")(0)) * 60) + CInt(dr(2).Split(":")(1))
                '        dr(3) = CInt((CInt(dr(3).Split(":")(0)) * 60) + CInt(dr(3).Split(":")(1)))
                '    Next
                'End If

                dgrdHighTime.DataSource = DtTime
                dgrdHighTime.DataBind()
            Else
                dvHasTime.Visible = False
                CreateDt()
                dgrdHighTime.DataSource = DtTime
                dgrdHighTime.DataBind()
            End If
        End With
    End Sub

    Protected Sub rdbOTEligibility_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbOTEligibility.SelectedIndexChanged
        If rdbOTEligibility.SelectedValue = 1 Then
            dvOtEligibility.Visible = True
            tabHighTime.Visible = True
        Else
            dvOtEligibility.Visible = False
            tabHighTime.Visible = False
        End If
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim strErrorMsg As String = String.Empty
        If DtTime Is Nothing Then
            CreateDt()
        End If
        Dim HighID As Integer = dgrdHighTime.Items.Count + 1
        If HighTimeId > 0 Then
            Dim str3 As String = "HighTimeId =" & HighTimeId
            Dim dr1 As DataRow
            dr1 = DtTime.Select(str3)(0)
            DtTime.Rows.Remove(dr1)
        End If

        Dim NFromTime As Integer = (CInt(FromTime.TextWithLiterals.Split(":")(0)) * 60) + CInt(FromTime.TextWithLiterals.Split(":")(1))
        Dim NToTime As Integer = (CInt(ToTime.TextWithLiterals.Split(":")(0)) * 60) + CInt(ToTime.TextWithLiterals.Split(":")(1))

        If (Not IfIsExist(DtTime, NFromTime, NToTime, strErrorMsg)) Then
            CtlCommon.ShowMessage(Me.Page, strErrorMsg, "info")
            Return
        Else
            Dim row As DataRow = DtTime.NewRow
            row("HighTimeId") = HighID
            row("FK_RuleId") = OvertimeRuleId
            row("FromTime") = FromTime.TextWithLiterals
            row("ToTime") = ToTime.TextWithLiterals
            row("FK_OvertimeTypeId") = RadComboBoxOvertimeTypeHighTime.SelectedValue
            DtTime.Rows.Add(row)

            dgrdHighTime.DataSource = DtTime
            dgrdHighTime.DataBind()

            row("FromTime") = (CInt(FromTime.TextWithLiterals.Split(":")(0)) * 60) + CInt(FromTime.TextWithLiterals.Split(":")(1))
            row("ToTime") = (CInt(ToTime.TextWithLiterals.Split(":")(0)) * 60) + CInt(ToTime.TextWithLiterals.Split(":")(1))

            HighTimeId = 0
            FromTime.Text = "0000"
            ToTime.Text = "0000"
        End If
    End Sub

    Protected Sub rdbHighHasTime_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbHighHasTime.SelectedIndexChanged
        If rdbHighHasTime.SelectedValue = 1 Then
            dvHasTime.Visible = True
        Else
            dvHasTime.Visible = False
        End If
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdHighTime.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim HighTime As Integer = Convert.ToInt32(Convert.ToInt32(row.GetDataKeyValue("HighTimeId")))
                Dim str3 As String = "HighTimeId ='" & HighTime.ToString() & "'"
                Dim dr As DataRow
                dr = DtTime.Select(str3)(0)
                DtTime.Rows.Remove(dr)
            End If
        Next
        Dim dv As New DataView(DtTime)
        dgrdHighTime.DataSource = dv
        dgrdHighTime.DataBind()
        FromTime.Text = "0000"
        ToTime.Text = "0000"
        HighTimeId = 0
    End Sub

    Protected Sub dgrdHighTime_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdHighTime.SelectedIndexChanged
        HighTimeId = Convert.ToInt32(DirectCast(dgrdHighTime.SelectedItems(0), GridDataItem).GetDataKeyValue("HighTimeId"))
        FromTime.Text = DirectCast(dgrdHighTime.SelectedItems(0), GridDataItem).GetDataKeyValue("FromTime")
        ToTime.Text = DirectCast(dgrdHighTime.SelectedItems(0), GridDataItem).GetDataKeyValue("ToTime")
        RadComboBoxOvertimeTypeHighTime.SelectedValue = DirectCast(dgrdHighTime.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_OvertimeTypeId")
    End Sub

    Protected Sub rblManageOvertime_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblManageOvertime.SelectedIndexChanged
        If rblManageOvertime.SelectedValue = 4 Then
            showHide(True)
        Else
            showHide(False)
            chkDeduct.Checked = False
        End If
    End Sub

#End Region

#Region "Methods"

    Sub CreateDt()
        Dim dt As New DataTable("DT")
        dt.Columns.Add("HighTimeId")
        dt.Columns.Add("FK_RuleId")
        dt.Columns.Add("FromTime")
        dt.Columns.Add("ToTime")
        dt.Columns.Add("FK_OvertimeTypeId")
        DtTime = dt
    End Sub

    Public Sub FillGrid()
        rblManageOvertime.SelectedValue = 2
        Try
            objOvertimeRules = New OvertimeRules
            dgrdOverTimeRules.DataSource = objOvertimeRules.GetAll()
            dgrdOverTimeRules.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Public Sub ClearAll()
        ' Clear controls
        'TxtHighRate.Text = String.Empty
        'TxtLowRate.Text = String.Empty
        TxtMinOvertime.Text = String.Empty
        txtMaxOvertime.Text = String.Empty
        TxtRuleName.Text = String.Empty
        txtRuleArName.Text = String.Empty
        rdbApprovalReqd.SelectedValue = 0
        'rdbConsiderHoliday.SelectedValue = 0
        'rdbconsiderOffDay.SelectedValue = 0
        'rdbOtCompLateTime.SelectedValue = 0
        rdbOTEligibility.SelectedValue = 0
        rdbHighHasTime.SelectedValue = 0
        dvHasTime.Visible = False
        dvOtEligibility.Visible = False
        rblManageOvertime.SelectedValue = 2
        showHide(False)
        chkDeduct.Checked = False
        'rdbOTLeaveOrFinance.SelectedValue = 1
        FillGrid()
        OvertimeRuleId = 0
        CreateDt()
        dgrdHighTime.DataSource = DtTime
        dgrdHighTime.DataBind()
        FromTime.Text = "0000"
        ToTime.Text = "0000"
        HighTimeId = 0
        tabHighTime.Visible = False

        RadComboBoxNormalDay.SelectedValue = -1
        RadComboBoxOffDay.SelectedValue = -1
        RadComboBoxHoliday.SelectedValue = -1
        RadComboBoxReligionHoliday.SelectedValue = -1
        txtRequiredDuration.Text = String.Empty
    End Sub

    Private Function IfIsExist(ByVal DTTime As DataTable, ByVal FromTime As Integer, ByVal ToTime As Integer, ByRef strMsg As String) As Boolean
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        If (DTTime IsNot Nothing AndAlso DTTime.Rows.Count > 0) Then
            For Each row As DataRow In DTTime.Rows
                If (FromTime > row("FromTime") AndAlso FromTime < row("ToTime")) Then
                    strMsg = ResourceManager.GetString("OvertimeInserted", CultureInfo)
                    Return False
                End If
            Next
        End If
        Return True
    End Function

    Private Sub FillDropDowns()
        objOvertime_Types = New Overtime_Types
        With objOvertime_Types
            CtlCommon.FillTelerikDropDownList(RadComboBoxNormalDay, .GetAll, Lang)
            CtlCommon.FillTelerikDropDownList(RadComboBoxOffDay, .GetAll, Lang)
            CtlCommon.FillTelerikDropDownList(RadComboBoxHoliday, .GetAll, Lang)
            CtlCommon.FillTelerikDropDownList(RadComboBoxReligionHoliday, .GetAll, Lang)
            CtlCommon.FillTelerikDropDownList(RadComboBoxOvertimeTypeHighTime, .GetAll, Lang)
        End With
    End Sub

#End Region

End Class
