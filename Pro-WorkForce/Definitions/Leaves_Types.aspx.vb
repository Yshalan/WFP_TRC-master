Imports System.Data
Imports Telerik.Web.UI
Imports TA.Definitions
Imports SmartV.UTILITIES
Imports TA.Security
Imports SmartV.UTILITIES.ProjectCommon
Imports TA.Admin
Imports TA.Lookup
Imports TA.Employees

Partial Class Emp_Leaves_Types
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objLeavesTypes As LeavesTypes
    Private objDurations As Durations
    Dim objLeaveTypeOccurance As New LeaveTypeOccurance
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Dim objAPP_Settings As New APP_Settings
    Private objProjectCommon As New ProjectCommon
    Private objEmp_Grade As Emp_Grade
    Private objEmp_GradeLeavesTypes As Emp_GradeLeavesTypes
    Private objEmp_Leaves_BalanceRequest As Emp_Leaves_BalanceRequest
    Private objEmp_logicalGroup As Emp_logicalGroup
    Private objOrgLevel As OrgLevel

#End Region

#Region "Properties"

    Private Enum RequestStatus
        Pending = 0
        Processing = 1
        Completed = 2
    End Enum

    Private Property LeaveId() As Integer
        Get
            Return ViewState("LeaveId")

        End Get
        Set(ByVal value As Integer)
            ViewState("LeaveId") = value
        End Set
    End Property

    Public Property MaximumAlloweddt() As DataTable
        Get
            Return ViewState("MaximumAlloweddt")
        End Get
        Set(ByVal value As DataTable)
            ViewState("MaximumAlloweddt") = value
        End Set
    End Property

    Public Property GradeBalancedt() As DataTable
        Get
            Return ViewState("GradeBalancedt")
        End Get
        Set(ByVal value As DataTable)
            ViewState("GradeBalancedt") = value
        End Set
    End Property

    Private Property DurationId() As Integer
        Get
            Return ViewState("DurationId")
        End Get
        Set(ByVal value As Integer)
            ViewState("DurationId") = value
        End Set
    End Property

    Private Property MaximumAllowedId() As Integer
        Get
            Return ViewState("MaximumAllowedId")
        End Get
        Set(ByVal value As Integer)
            ViewState("MaximumAllowedId") = value
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

    Private Property GradeId() As Integer
        Get
            Return ViewState("GradeId")

        End Get
        Set(ByVal value As Integer)
            ViewState("GradeId") = value
        End Set
    End Property

    Private Property PreviousBalance() As Integer
        Get
            Return ViewState("PreviousBalance")
        End Get
        Set(ByVal value As Integer)
            ViewState("PreviousBalance") = value
        End Set
    End Property

    Private Property CurrentBalance() As Integer
        Get
            Return ViewState("CurrentBalance")
        End Get
        Set(ByVal value As Integer)
            ViewState("CurrentBalance") = value
        End Set
    End Property

    Private Property MaximumGradeBalanceId() As Integer
        Get
            Return ViewState("MaximumGradeBalanceId")
        End Get
        Set(ByVal value As Integer)
            ViewState("MaximumGradeBalanceId") = value
        End Set
    End Property

#End Region

#Region "PageEvents"

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


            FillGridView()
            FillDurations()
            FillDropDown()
            FillGrades()
            FillBalanceGrades()
            FillEmployeeType()
            FillLevels()
            createMaximumAllwoeddt()
            createGradeBalancedt()
            rdbMonthlyBalance.SelectedIndex = 1
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)

            rfvDuration.InitialValue = ResourceManager.GetString("PleaseSelect", CultureInfo)
            PageHeader1.HeaderText = ResourceManager.GetString("LeaveType", CultureInfo)

            objAPP_Settings = New APP_Settings
            With objAPP_Settings
                .GetByPK()
                If .LeaveApprovalfromLeave Then
                    trLeaveApproval.Visible = True
                Else
                    trLeaveApproval.Visible = False
                End If
            End With
            dpEffectiveDate.SelectedDate = Date.Today
        End If

        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdVwLeaveTypes.ClientID + "');")
        btnDeleteMaximumAllowed.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdMaximumAllowed.ClientID + "');")
        btnRemoveGradeBalance.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdGradeBalance.ClientID + "');")

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not pnlLeavesTypes.FindControl(row("AddBtnName")) Is Nothing Then
                        pnlLeavesTypes.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not pnlLeavesTypes.FindControl(row("DeleteBtnName")) Is Nothing Then
                        pnlLeavesTypes.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not pnlLeavesTypes.FindControl(row("EditBtnName")) Is Nothing Then
                        pnlLeavesTypes.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not pnlLeavesTypes.FindControl(row("PrintBtnName")) Is Nothing Then
                        pnlLeavesTypes.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, _
                                 ByVal e As System.EventArgs) Handles btnClear.Click
        FillGridView()
        ClearAll()
        createMaximumAllwoeddt()
        TabContainer1.ActiveTabIndex = 0
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdVwLeaveTypes.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim strCode As String = row.GetDataKeyValue("LeaveName").ToString()
                Dim intLeaveId As Integer = Convert.ToInt32(row.GetDataKeyValue("LeaveId").ToString())
                objLeavesTypes = New LeavesTypes()
                objLeavesTypes.LeaveId = intLeaveId
                errNum = objLeavesTypes.Delete()

            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillGridView()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord", CultureInfo), "error")
            'showResult(CodeResultMessage.CodeDeleteFail)

        End If
        ClearAll()

    End Sub

    Protected Sub btnDeleteMaximumAllowed_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteMaximumAllowed.Click
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdMaximumAllowed.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim intAbsentRuleId As Integer = Convert.ToInt32(row.GetDataKeyValue("MaximumAllowedId").ToString())
                MaximumAlloweddt.Rows.Remove(MaximumAlloweddt.Select("MaximumAllowedId = " & intAbsentRuleId)(0))
            End If
            dgrdMaximumAllowed.DataSource = MaximumAlloweddt
            dgrdMaximumAllowed.DataBind()
            MaximumAllowedId = 0
            txtMaximimOccur.Text = ""
            RadCmbBxDuration.SelectedIndex = -1
        Next
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        objLeavesTypes = New LeavesTypes()
        objEmp_GradeLeavesTypes = New Emp_GradeLeavesTypes
        Dim errorNum As Integer = -1
        ' Set data into object for Add / Update
        With objLeavesTypes
            ' Get values from the text boxes
            .LeaveName = txtName.Text.Trim()
            .LeaveArabicName = txtArabicName.Text.Trim()
            If rblConsiderBalance.SelectedValue = 0 Then
                .Balance = txtRadBalance.Text
            Else
                .Balance = Nothing
            End If


            ' Below if statement important to avoid exception , in case the 
            ' TextBox instance has the value of string empty
            'If txtPaymentConsideration.Text = String.Empty Then
            '    .PaymentConsidration = 0
            'Else
            '    .PaymentConsidration = txtPaymentConsideration.Text
            'End If
            .PaymentConsidration = rdbPaymentConsideration.SelectedValue

            ' Below if statement important to avoid exception , in case the 
            ' RadNumericTextBox instance has the value of string empty

            .MinDuration = Val(txtRadMinDuration.Text.Trim())
            .MaxDuration = Val(txtRadMaxDuration.Text.Trim())
            .MinServiceDays = Val(txtRadMinServiceDay.Text.Trim())
            .MaxRoundBalance = Val(txtRadMaxRoundBalance.Text.Trim())
            .MaxOccurancePerPeriod = txtMaximimOccur.Text
            ' Get values from the check boxes
            .MonthlyBalancing = rdbMonthlyBalance.SelectedValue
            .ExcludeOffDays = IIf(rdbExcludeOffsDay.SelectedValue = "True", True, False)
            .ExcludeHolidays = IIf(rdbExcludeHolidays.SelectedValue = "True", True, False)
            .ExpiredBalanceIsCashed = False
            '.ExpiredBalanceIsCashed = rdbExpiredBalanceIsCached.SelectedValue
            .AllowIfBalanceOver = rdbAllowBalanceIsOver.SelectedValue
            .IsAnnual = rdbIsAnnual.SelectedValue
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            .LAST_UPDATE_DATE = Today.Date
            .CREATED_BY = SessionVariables.LoginUser.ID
            .CREATED_DATE = Today.Date
            .AllowedForSelfService = chkAllowedForSelfService.Checked
            .ShowRemainingBalance = chkShowRemainingBalance.Checked
            '.GeneralGuide = txtGeneralGuide.Text
            .GeneralGuide = (Server.HtmlEncode(txtGeneralGuide.Text)).Replace(Environment.NewLine, "<br/>")
            '.GeneralGuideAr = txtGeneralGuideAr.Text
            .GeneralGuideAr = Server.HtmlEncode(txtGeneralGuideAr.Text).Replace(Environment.NewLine, "<br/>")
            If Not ddlLeaveType.SelectedValue = "-1" Then
                .FK_ParentLeaveType = Convert.ToInt32(ddlLeaveType.SelectedValue)
            Else
                .FK_ParentLeaveType = 0
            End If

            .Leave_NotificationException = chkLeave_NotificationException.Checked
            objAPP_Settings = New APP_Settings
            objAPP_Settings.GetByPK()
            If objAPP_Settings.LeaveApprovalfromLeave = True Then
                .LeaveApproval = rlstApproval.SelectedValue
            End If

            If rblConsiderBalance.SelectedValue = 0 Then
                If rblGrades.SelectedValue = 0 Then
                    .IsSpecificGrade = False
                Else
                    .IsSpecificGrade = True
                End If
            Else
                .IsSpecificGrade = True
            End If

            If rblAttachmentIsMandatory.SelectedValue = 1 Then
                .AttachmentIsMandatory = True
            Else
                .AttachmentIsMandatory = False
            End If

            If rblRemarksIsMandatory.SelectedValue = 1 Then
                .RemarksIsMandatory = True
            Else
                .RemarksIsMandatory = False
            End If

            .AllowedGender = rblAllowedGender.SelectedValue

            .AllowForSpecificEmployeeType = chkAllowForSpecificEmployeeType.Checked
            If chkAllowForSpecificEmployeeType.Checked = True Then
                trAllowForSpecificEmployeeType.Visible = True
                .FK_EmployeeTypeId = RadCmbBxEmployeeType.SelectedValue
            Else
                trAllowForSpecificEmployeeType.Visible = False
            End If

            If Not radcmbLevels.SelectedValue = -1 Then
                .LeaveRequestManagerLevelRequired = radcmbLevels.SelectedValue
            End If
            .ValidateLeavesBeforeRestDays = chkValidateLeavesBeforeRestDays.Checked
            .LeaveCode = txtLeaveCode.Text
            If radtxtAllowedAfterDays.Text = String.Empty Then
                .AllowedAfterDays = Nothing
            Else
                .AllowedAfterDays = radtxtAllowedAfterDays.Text
            End If

            If radtxtAllowedBeforeDays.Text = String.Empty Then
                .AllowedBeforeDays = Nothing
            Else
                .AllowedBeforeDays = radtxtAllowedBeforeDays.Text
            End If

            .ApprovalRequired = chckApprovalRequired.Checked

            If chkAutoApprove.Checked = True Then
                .IsAutoApprove = chkAutoApprove.Checked
                .AutoApproveAfter = radnumAutoApproveAfter.Text
            Else
                .IsAutoApprove = chkAutoApprove.Checked
                .AutoApproveAfter = String.Empty
            End If
            .BalanceConsideration = rblConsiderBalance.SelectedValue

            For Each item As ListItem In chkAutoApprovePolicy.Items
                If item.Selected = True Then
                    .AutoApprovePolicy += item.Value + ","
                End If
            Next
            If Not radtxtMinLeaveApplyDay.Text = String.Empty Then
                .MinLeaveApplyDay = radtxtMinLeaveApplyDay.Text
            Else
                .MinLeaveApplyDay = Nothing
            End If
        End With
        '''''

        If LeaveId = 0 Then
            ' Do add operation 
            errorNum = objLeavesTypes.Add()

            If errorNum = 0 Then
                LeaveId = objLeavesTypes.LeaveId
            End If
        Else
            ' Do update operations
            objLeavesTypes.LeaveId = LeaveId
            errorNum = objLeavesTypes.Update()
            objLeavesTypes.GetByPK()
            CurrentBalance = objLeavesTypes.Balance
            If objLeavesTypes.BalanceConsideration = 0 Then
                If CurrentBalance <> PreviousBalance Then
                    mpeEffectiveDatePopup.Show()
                End If
            End If


        End If
        Dim errorNum2 As Integer
        If errorNum = 0 And rblGrades.SelectedValue = 1 Then
            Dim flag As Boolean = False
            With objEmp_GradeLeavesTypes
                .FK_LeaveId = LeaveId
                .DeleteFK_LeaveId()
                For Each item As ListItem In cblGradeList.Items
                    If item.Selected = True Then
                        flag = True
                        .FK_GradeId = item.Value
                        errorNum2 = .Add
                    End If
                Next

            End With
        End If
        If errorNum = 0 And errorNum2 = 0 Then

            CtlCommon.ShowMessage(Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")

            objLeaveTypeOccurance = New LeaveTypeOccurance
            If LeaveId > 0 Then

                ' If Not AbsentRuledt Is Nothing AndAlso AbsentRuledt.Rows.Count > 0 Then
                With objLeaveTypeOccurance
                    .Add_Bulk(MaximumAlloweddt, LeaveId)
                End With


            End If
            If rblConsiderBalance.SelectedValue = 1 Then
                objEmp_GradeLeavesTypes = New Emp_GradeLeavesTypes
                If LeaveId > 0 Then
                    With objEmp_GradeLeavesTypes
                        .Add_Bulk(GradeBalancedt, LeaveId)
                    End With
                End If
            End If


            FillGridView()
            ClearAll()

        ElseIf errorNum = -5 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NameExist", CultureInfo), "info")
        ElseIf errorNum = -6 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If



    End Sub

    Protected Sub btnSaveAllowedOccurance_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveAllowedOccurance.Click

        If Convert.ToInt32(txtMaximimOccur.Text) <= Convert.ToInt32(txtRadBalance.Text) Then
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            Dim err As Integer = -1
            Dim dr As DataRow
            Try
                If ifexistAllowedOccurance(txtMaximimOccur.Text, RadCmbBxDuration.SelectedValue) Then
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("MaxAllowed", CultureInfo), "info")
                    Exit Sub
                End If
                If MaximumAllowedId = 0 Then

                    dr = MaximumAlloweddt.NewRow
                    ' dr("AbsentRuleId") = AbsentRuleId
                    dr("FK_LeaveId") = LeaveId
                    dr("MaximumOccur") = txtMaximimOccur.Text
                    dr("FK_DurationId") = RadCmbBxDuration.SelectedValue
                    dr("DurationTypeName") = RadCmbBxDuration.Text
                    MaximumAlloweddt.Rows.Add(dr)
                Else
                    If MaximumAlloweddt.Rows.Count > 0 Then
                        dr = MaximumAlloweddt.Select("MaximumAllowedId= " & MaximumAllowedId)(0)
                        dr("MaximumAllowedId") = MaximumAllowedId
                        dr("FK_LeaveId") = LeaveId
                        dr("MaximumOccur") = txtMaximimOccur.Text
                        dr("FK_DurationId") = RadCmbBxDuration.SelectedValue
                        dr("DurationTypeName") = RadCmbBxDuration.Text
                    End If
                End If
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("AllowedOccureAddSuccess", CultureInfo), "success")
                dgrdMaximumAllowed.DataSource = MaximumAlloweddt
                dgrdMaximumAllowed.DataBind()
                MaximumAllowedId = 0
                txtMaximimOccur.Text = ""
                RadCmbBxDuration.SelectedIndex = -1
            Catch ex As Exception
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrAddMaxAllow", CultureInfo), "error")

            End Try
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("LeaveBalanceRange", CultureInfo), "info")
        End If


    End Sub

    Protected Sub dgrdVwLeaveTypes_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdVwLeaveTypes.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub dgrdVwLeaveTypes_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles dgrdVwLeaveTypes.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim item As GridDataItem
            item = e.Item

            If Not item("BalanceConsideration").Text = "&nbsp;" Then
                If item("BalanceConsideration").Text = "1" Then
                    If Lang = CtlCommon.Lang.AR Then
                        item("Balance").Text = "من خلال الدرجة الوظيفية"
                    Else
                        item("Balance").Text = "According Grades"
                    End If

                End If
            End If
        End If


    End Sub

    Protected Sub dgrdVwLeaveTypes_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdVwLeaveTypes.NeedDataSource
        Try
            objLeavesTypes = New LeavesTypes()
            objLeavesTypes.LeaveId = LeaveId
            dgrdVwLeaveTypes.DataSource = objLeavesTypes.GetAll()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub dgrdVwLeaveTypes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdVwLeaveTypes.SelectedIndexChanged
        If dgrdVwLeaveTypes.SelectedItems.Count = 1 Then
            ClearAll()
            LeaveId = CInt(DirectCast(dgrdVwLeaveTypes.SelectedItems(0), GridDataItem).GetDataKeyValue("LeaveId").ToString())
            FillControls()
        End If

    End Sub

    Protected Sub dgrdMaximumAllowed__SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdMaximumAllowed.SelectedIndexChanged
        FillMaximumAllowed()
    End Sub

    Protected Sub rblGrades_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rblGrades.SelectedIndexChanged
        If rblGrades.SelectedValue = 1 Then
            dvGradeList.Visible = True
        Else
            dvGradeList.Visible = False
        End If
    End Sub

    Protected Sub btnSaveEffective_Click(sender As Object, e As EventArgs) Handles btnSaveEffective.Click

        objEmp_Leaves_BalanceRequest = New Emp_Leaves_BalanceRequest
        Dim err As Integer = -1
        With objEmp_Leaves_BalanceRequest
            .FK_LeaveTypeId = LeaveId
            .EffictiveDate = dpEffectiveDate.DbSelectedDate
            .CREATED_BY = SessionVariables.LoginUser.ID
            .RequestStatus = RequestStatus.Pending
            .LeaveTypeNewBalance = CurrentBalance
            .LeaveTypeOldBalance = PreviousBalance
            err = .Add()
        End With
        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If

        ClearAll()


    End Sub

    Protected Sub chkAllowForSpecificEmployeeType_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkAllowForSpecificEmployeeType.CheckedChanged
        If chkAllowForSpecificEmployeeType.Checked Then
            trAllowForSpecificEmployeeType.Visible = True
        Else
            trAllowForSpecificEmployeeType.Visible = False
        End If
    End Sub

    Protected Sub chckApprovalRequired_CheckedChanged(sender As Object, e As EventArgs) Handles chckApprovalRequired.CheckedChanged
        If chckApprovalRequired.Checked = True Then
            dvAutoApprove.Visible = True
        Else
            dvAutoApprove.Visible = False
            chkAutoApprove.Checked = False
            dvAutoApproveAfter.Visible = False
        End If
    End Sub

    Protected Sub chkAutoApprove_CheckedChanged(sender As Object, e As EventArgs) Handles chkAutoApprove.CheckedChanged
        If chkAutoApprove.Checked = True Then
            dvAutoApproveAfter.Visible = True
            rfvAutoApproveAfter.Enabled = True
        Else
            dvAutoApproveAfter.Visible = False
            rfvAutoApproveAfter.Enabled = False
        End If
    End Sub

    Protected Sub rlstApproval_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rlstApproval.SelectedIndexChanged

        Refresh_AutoApprovePolicyCheckList()

        If rlstApproval.SelectedValue = 1 Then
            chkAutoApprovePolicy.Items.RemoveAt(1) '---HR
            chkAutoApprovePolicy.Items.RemoveAt(1) '---GM
        ElseIf rlstApproval.SelectedValue = 2 Then
            chkAutoApprovePolicy.Items.RemoveAt(0) '---DM
            chkAutoApprovePolicy.Items.RemoveAt(1) '---GM
        ElseIf rlstApproval.SelectedValue = 3 Then
            chkAutoApprovePolicy.Items.RemoveAt(2) '---GM
        End If
    End Sub

    Protected Sub rblConsiderBalance_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblConsiderBalance.SelectedIndexChanged
        If rblConsiderBalance.SelectedValue = 0 Then
            dvBalance.Visible = True
            ReqrdbMonthlyBalance.Enabled = True
            dvGradeBalance.Visible = False
            cvBalanceMinDuration.Enabled = True
            rfvRadGradeBalance.Enabled = False
            txtRadGradeBalance.Text = String.Empty
            Tab3.Visible = True
            Tab4.Visible = False
        Else
            dvBalance.Visible = False
            ReqrdbMonthlyBalance.Enabled = False
            dvGradeBalance.Visible = True
            cvBalanceMinDuration.Enabled = False
            rfvRadGradeBalance.Enabled = True
            txtRadBalance.Text = String.Empty
            Tab3.Visible = False
            Tab4.Visible = True
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdVwLeaveTypes.Skin))
    End Function

    Protected Sub RadToolBar2_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon1() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdGradeBalance.Skin))
    End Function

    Protected Sub dgrdGradeBalance_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdGradeBalance.ItemCommand
        If e.CommandName = "FilterRadGrid1" Then
            RadFilter2.FireApplyCommand()
        End If
    End Sub

    Protected Sub btnAddGradeBalance_Click(sender As Object, e As EventArgs) Handles btnAddGradeBalance.Click
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim err As Integer = -1
        Dim dr As DataRow
        Try
            For Each item As ListItem In cblBalanceGradeList.Items
                If item.Selected = True Then
                    If ifexistGradeBalance(item.Value, LeaveId, txtRadGradeBalance.Text) Then
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("MaxAllowed", CultureInfo), "info")
                        Exit Sub
                    End If
                    If MaximumGradeBalanceId = 0 Then
                        dr = GradeBalancedt.NewRow
                        dr("FK_LeaveId") = LeaveId
                        dr("FK_GradeId") = item.Value
                        dr("GradeName") = item.Text
                        dr("GradeBalance") = txtRadGradeBalance.Text
                        GradeBalancedt.Rows.Add(dr)
                    Else
                        If GradeBalancedt.Rows.Count > 0 Then
                            dr = GradeBalancedt.Select("FK_GradeId= " & item.Value)(0)
                            dr("FK_LeaveId") = LeaveId
                            dr("GradeName") = item.Text
                            dr("GradeBalance") = txtRadGradeBalance.Text
                        End If
                    End If
                End If
            Next
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("GradeBalanceAddSuccess", CultureInfo), "success")
            dgrdGradeBalance.DataSource = GradeBalancedt
            dgrdGradeBalance.DataBind()
            txtRadGradeBalance.Text = ""
            cblBalanceGradeList.ClearSelection()
        Catch ex As Exception
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrAddGradeBalance", CultureInfo), "error")
        End Try
    End Sub

    Protected Sub dgrdGradeBalance_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdGradeBalance.NeedDataSource
        dgrdGradeBalance.DataSource = GradeBalancedt
    End Sub

    Protected Sub dgrdGradeBalance_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgrdGradeBalance.SelectedIndexChanged
        FillGradeBalanceGrid()
    End Sub

    Protected Sub btnRemoveGradeBalance_Click(sender As Object, e As EventArgs) Handles btnRemoveGradeBalance.Click
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdGradeBalance.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim intMaximumGradeBalanceId As Integer = Convert.ToInt32(row.GetDataKeyValue("MaximumGradeBalanceId").ToString())
                GradeBalancedt.Rows.Remove(GradeBalancedt.Select("MaximumGradeBalanceId = " & intMaximumGradeBalanceId)(0))
            End If
            dgrdGradeBalance.DataSource = GradeBalancedt
            dgrdGradeBalance.DataBind()
            MaximumGradeBalanceId = 0
            cblBalanceGradeList.ClearSelection()
            txtRadGradeBalance.Text = String.Empty
        Next
    End Sub

#End Region

#Region "Methods"

    Private Sub FillGridView()
        Try
            objLeavesTypes = New LeavesTypes()
            objLeavesTypes.LeaveId = LeaveId
            dgrdVwLeaveTypes.DataSource = objLeavesTypes.GetAll()
            dgrdVwLeaveTypes.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Sub FillMaximumAllowed()
        Try

            Dim objLeaveTypes As New LeavesTypes
            Dim dt As DataTable = MaximumAlloweddt
            MaximumAllowedId = CInt(CType(dgrdMaximumAllowed.SelectedItems(0), GridDataItem).GetDataKeyValue("MaximumAllowedId").ToString())
            'objLeaveTypes. = AbsentRuleId
            txtMaximimOccur.Text = CType(dgrdMaximumAllowed.SelectedItems(0), GridDataItem).GetDataKeyValue("MaximumOccur").ToString()
            RadCmbBxDuration.SelectedValue = CInt(CType(dgrdMaximumAllowed.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_DurationId").ToString())
        Catch ex As Exception
        End Try
    End Sub

    Sub createMaximumAllwoeddt()
        MaximumAlloweddt = New DataTable
        Dim dc As DataColumn
        dc = New DataColumn
        dc.ColumnName = "MaximumAllowedId"
        dc.DataType = GetType(Integer)
        MaximumAlloweddt.Columns.Add(dc)
        MaximumAlloweddt.Columns("MaximumAllowedId").AutoIncrement = True
        MaximumAlloweddt.Columns("MaximumAllowedId").AutoIncrementSeed = 1
        MaximumAlloweddt.Columns("MaximumAllowedId").AutoIncrementStep = 1

        dc = New DataColumn
        dc.ColumnName = "MaximumOccur"
        dc.DataType = GetType(String)
        MaximumAlloweddt.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "FK_DurationId"
        dc.DataType = GetType(Integer)
        MaximumAlloweddt.Columns.Add(dc)


        dc = New DataColumn
        dc.ColumnName = "DurationTypeName"
        dc.DataType = GetType(String)
        MaximumAlloweddt.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "FK_LeaveId"
        dc.DataType = GetType(Integer)
        MaximumAlloweddt.Columns.Add(dc)

    End Sub

    Function ifexistAllowedOccurance(ByVal MaximumAllowed As String, ByVal DurationId As Integer) As Boolean
        If Not MaximumAlloweddt Is Nothing AndAlso MaximumAlloweddt.Rows.Count > 0 Then
            If MaximumAllowedId > 0 Then
                For Each i In MaximumAlloweddt.Rows
                    If (i("FK_DurationId") = DurationId) And (i("MaximumAllowedId") <> MaximumAllowedId) Then
                        Return True
                    End If
                Next
            Else
                For Each i In MaximumAlloweddt.Rows
                    If i("FK_DurationId") = DurationId Then
                        Return True
                    End If
                Next
            End If
        End If
        Return False
    End Function

    Private Sub FillDurations()
        Try
            objDurations = New Durations()
            Dim dt As DataTable
            dt = objDurations.GetAll()
            dt.Rows.RemoveAt(0)
            CtlCommon.FillTelerikDropDownList(RadCmbBxDuration, dt, Lang)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ClearAll()

        ' Clear Controls
        ' Clear the text boxes
        txtArabicName.Text = String.Empty
        txtRadMinServiceDay.Text = String.Empty
        txtName.Text = String.Empty
        txtMaximimOccur.Text = String.Empty
        txtRadBalance.Text = String.Empty
        txtRadMaxDuration.Text = String.Empty
        txtRadMaxRoundBalance.Text = String.Empty
        txtRadMinDuration.Text = String.Empty
        txtGeneralGuide.Text = String.Empty
        txtGeneralGuideAr.Text = String.Empty
        ' Uncheck check boxes
        'rdbPaymentConsideration.ClearSelection()
        'rdbAllowBalanceIsOver.ClearSelection()
        'rdbExcludeHolidays.ClearSelection()
        'rdbExcludeOffsDay.ClearSelection()
        'rdbExpiredBalanceIsCached.ClearSelection()
        'rdbIsAnnual.ClearSelection()
        'rlstApproval.ClearSelection()
        ' Reset to prepare to the next add operation
        rdbPaymentConsideration.SelectedIndex = 0
        rdbAllowBalanceIsOver.SelectedIndex = 0
        rdbExcludeHolidays.SelectedIndex = 1
        rdbExcludeOffsDay.SelectedIndex = 1
        rdbIsAnnual.SelectedIndex = 0
        rlstApproval.SelectedIndex = 0
        rdbMonthlyBalance.SelectedIndex = 1
        LeaveId = 0
        clearMaximumAllowed()
        createMaximumAllwoeddt()
        ddlLeaveType.ClearSelection()
        chkAllowedForSelfService.Checked = False
        chkShowRemainingBalance.Checked = False
        rblGrades.SelectedValue = 0
        cblGradeList.ClearSelection()
        dvGradeList.Visible = False
        chkLeave_NotificationException.Checked = False
        rblAttachmentIsMandatory.SelectedValue = 0
        rblRemarksIsMandatory.SelectedValue = 0
        rblAllowedGender.SelectedValue = 0
        chkAllowForSpecificEmployeeType.Checked = False
        trAllowForSpecificEmployeeType.Visible = False
        RadCmbBxEmployeeType.SelectedValue = -1
        radcmbLevels.SelectedValue = -1
        txtLeaveCode.Text = String.Empty
        radtxtAllowedAfterDays.Text = String.Empty
        radtxtAllowedBeforeDays.Text = String.Empty
        chckApprovalRequired.Checked = False
        dvAutoApprove.Visible = False
        dvAutoApproveAfter.Visible = False
        chkAutoApprove.Checked = False
        radnumAutoApproveAfter.Text = String.Empty
        chkAutoApprovePolicy.ClearSelection()
        rblGrades.SelectedValue = 0
        dvGradeList.Visible = False
        rdbMonthlyBalance.Enabled = True
        rblConsiderBalance.SelectedValue = 0
        dvBalance.Visible = True
        Tab3.Visible = True
        Tab4.Visible = False
        TabContainer1.ActiveTab = Tab1
        radtxtMinLeaveApplyDay.Text = String.Empty
    End Sub

    Sub clearMaximumAllowed()
        MaximumAlloweddt = New DataTable
        dgrdMaximumAllowed.DataSource = MaximumAlloweddt
        dgrdMaximumAllowed.DataBind()
        MaximumAllowedId = 0
        txtMaximimOccur.Text = String.Empty
        RadCmbBxDuration.SelectedIndex = -1
        ClearGradeBalance()
    End Sub

    Private Sub FillControls()
        ' Display the data of a specified record on the controls 
        objLeavesTypes = New LeavesTypes()
        objLeavesTypes.LeaveId = LeaveId
        objLeavesTypes.GetByPK()
        With objLeavesTypes
            ' Set data into object for Add / Update
            ' Get the values to text boxes
            txtName.Text = .LeaveName
            txtArabicName.Text = .LeaveArabicName
            If .BalanceConsideration = 0 Then
                txtRadBalance.Text = .Balance
            Else
                txtRadBalance.Text = String.Empty
            End If

            rdbPaymentConsideration.SelectedValue = .PaymentConsidration
            txtRadMinDuration.Text = .MinDuration
            txtRadMaxDuration.Text = .MaxDuration
            txtRadMinServiceDay.Text = .MinServiceDays
            txtRadMaxRoundBalance.Text = .MaxRoundBalance
            txtGeneralGuide.Text = .GeneralGuide
            txtGeneralGuideAr.Text = .GeneralGuideAr
            'txtMaximimOccur.Text = .MaxOccurancePerPeriod
            ' Get values to check boxes
            rdbMonthlyBalance.SelectedValue = .MonthlyBalancing
            rdbMonthlyBalance.Enabled = False
            rdbExcludeOffsDay.SelectedValue = objLeavesTypes.ExcludeOffDays
            rdbExcludeHolidays.SelectedValue = objLeavesTypes.ExcludeHolidays
            'rdbExpiredBalanceIsCached.SelectedValue = objLeavesTypes.ExpiredBalanceIsCashed
            rdbAllowBalanceIsOver.SelectedValue = objLeavesTypes.AllowIfBalanceOver
            rdbIsAnnual.SelectedValue = objLeavesTypes.IsAnnual
            chkShowRemainingBalance.Checked = objLeavesTypes.ShowRemainingBalance
            If trLeaveApproval.Visible Then
                If Not .LeaveApproval = 0 Then
                    rlstApproval.SelectedValue = .LeaveApproval
                End If
            End If
            chkAllowedForSelfService.Checked = .AllowedForSelfService
            ddlLeaveType.SelectedValue = .FK_ParentLeaveType
            If .IsSpecificGrade = False Then
                rblGrades.SelectedValue = 0
                dvGradeList.Visible = False
            Else
                rblGrades.SelectedValue = 1
                dvGradeList.Visible = True
            End If
            chkLeave_NotificationException.Checked = .Leave_NotificationException

            If .AttachmentIsMandatory = True Then
                rblAttachmentIsMandatory.SelectedValue = 1
            Else
                rblAttachmentIsMandatory.SelectedValue = 0
            End If

            If .RemarksIsMandatory = True Then
                rblRemarksIsMandatory.SelectedValue = 1
            Else
                rblRemarksIsMandatory.SelectedValue = 0
            End If

            chkAllowForSpecificEmployeeType.Checked = .AllowForSpecificEmployeeType

            If .AllowForSpecificEmployeeType = True Then
                trAllowForSpecificEmployeeType.Visible = True
                RadCmbBxEmployeeType.SelectedValue = .FK_EmployeeTypeId
            Else
                trAllowForSpecificEmployeeType.Visible = False
            End If

            radtxtAllowedAfterDays.Text = .AllowedAfterDays
            radtxtAllowedBeforeDays.Text = .AllowedBeforeDays
            radtxtMinLeaveApplyDay.Text = .MinLeaveApplyDay

            rblAllowedGender.SelectedValue = .AllowedGender

            radcmbLevels.SelectedValue = .LeaveRequestManagerLevelRequired
            chkValidateLeavesBeforeRestDays.Checked = .ValidateLeavesBeforeRestDays
            txtLeaveCode.Text = .LeaveCode
            PreviousBalance = .Balance '--- GET EMPLOYEE CURRENT LEAVE BALANCE BEFORE MODIFICATIONS

            chckApprovalRequired.Checked = .ApprovalRequired
            If .ApprovalRequired = True Then
                dvAutoApprove.Visible = True
                chkAutoApprove.Checked = .IsAutoApprove
                If .IsAutoApprove = True Then
                    dvAutoApproveAfter.Visible = True
                    radnumAutoApproveAfter.Text = .AutoApproveAfter
                Else
                    dvAutoApproveAfter.Visible = False
                    radnumAutoApproveAfter.Text = String.Empty
                End If
            Else
                dvAutoApprove.Visible = False
                chkAutoApprove.Checked = False
                radnumAutoApproveAfter.Text = String.Empty
            End If

            If Not .AutoApprovePolicy = Nothing Then
                Dim AutoApprovePolicy As String() = Split(.AutoApprovePolicy.ToString.Trim, ",")
                FillChkAutoApprovePolicy()
                For Each item In AutoApprovePolicy
                    If Not item = Nothing Then
                        chkAutoApprovePolicy.Items.FindByValue(item).Selected = True
                    End If
                Next
            End If

            rblConsiderBalance.SelectedValue = .BalanceConsideration

            If .BalanceConsideration = 0 Then
                dvBalance.Visible = True
                dvGradeBalance.Visible = False
                FillLeaveGrades()
                Tab3.Visible = True
                Tab4.Visible = False
            Else
                dvBalance.Visible = False
                dvGradeBalance.Visible = True
                'FillBalanceGrades()
                Tab3.Visible = False
                Tab4.Visible = True
            End If
            fillAllowedOccurance()
            fillGradeBalance()
        End With
    End Sub

    Sub fillAllowedOccurance()
        If LeaveId > 0 Then

            objLeaveTypeOccurance = New LeaveTypeOccurance
            objLeaveTypeOccurance.LeaveId = LeaveId
            MaximumAlloweddt = objLeaveTypeOccurance.GetByPK
            Dim dc As DataColumn
            dc = New DataColumn
            dc.ColumnName = "MaximumAllowedId"
            dc.DataType = GetType(Integer)
            MaximumAlloweddt.Columns.Add(dc)

            dc = New DataColumn
            dc.ColumnName = "DurationTypeName"
            dc.DataType = GetType(String)
            MaximumAlloweddt.Columns.Add(dc)
            If Lang = CtlCommon.Lang.AR Then
                For i As Integer = 0 To MaximumAlloweddt.Rows.Count - 1
                    If (MaximumAlloweddt.Rows(i)("FK_DurationId") = 1) Then
                        MaximumAlloweddt.Rows(i)("DurationTypeName") = "يوم"
                    ElseIf (MaximumAlloweddt.Rows(i)("FK_DurationId") = 2) Then
                        MaximumAlloweddt.Rows(i)("DurationTypeName") = "اسبوع"
                    ElseIf MaximumAlloweddt.Rows(i)("FK_DurationId") = 3 Then
                        MaximumAlloweddt.Rows(i)("DurationTypeName") = "شهر"
                    ElseIf MaximumAlloweddt.Rows(i)("FK_DurationId") = 4 Then
                        MaximumAlloweddt.Rows(i)("DurationTypeName") = "سنة"
                    ElseIf MaximumAlloweddt.Rows(i)("FK_DurationId") = 5 Then
                        MaximumAlloweddt.Rows(i)("DurationTypeName") = "طول فترة الخدمة"
                    End If
                    MaximumAlloweddt.Rows(i)("MaximumAllowedId") = i + 1
                Next
                MaximumAlloweddt.Columns("MaximumAllowedId").AutoIncrement = True
                MaximumAlloweddt.Columns("MaximumAllowedId").AutoIncrementSeed = 1
                MaximumAlloweddt.Columns("MaximumAllowedId").AutoIncrementStep = 1
            Else
                For i As Integer = 0 To MaximumAlloweddt.Rows.Count - 1
                    If (MaximumAlloweddt.Rows(i)("FK_DurationId") = 1) Then
                        MaximumAlloweddt.Rows(i)("DurationTypeName") = "Day"
                    ElseIf (MaximumAlloweddt.Rows(i)("FK_DurationId") = 2) Then
                        MaximumAlloweddt.Rows(i)("DurationTypeName") = "Week"
                    ElseIf MaximumAlloweddt.Rows(i)("FK_DurationId") = 3 Then
                        MaximumAlloweddt.Rows(i)("DurationTypeName") = "Month"
                    ElseIf MaximumAlloweddt.Rows(i)("FK_DurationId") = 4 Then
                        MaximumAlloweddt.Rows(i)("DurationTypeName") = "Year"
                    ElseIf MaximumAlloweddt.Rows(i)("FK_DurationId") = 5 Then
                        MaximumAlloweddt.Rows(i)("DurationTypeName") = "All Service Time"
                    End If
                    MaximumAlloweddt.Rows(i)("MaximumAllowedId") = i + 1
                Next
                MaximumAlloweddt.Columns("MaximumAllowedId").AutoIncrement = True
                MaximumAlloweddt.Columns("MaximumAllowedId").AutoIncrementSeed = 1
                MaximumAlloweddt.Columns("MaximumAllowedId").AutoIncrementStep = 1
            End If


            dgrdMaximumAllowed.DataSource = MaximumAlloweddt
            dgrdMaximumAllowed.DataBind()
        Else
            createMaximumAllwoeddt()
            dgrdMaximumAllowed.DataSource = MaximumAlloweddt
            dgrdMaximumAllowed.DataBind()
        End If
    End Sub

    Sub FillDropDown()
        objProjectCommon = New ProjectCommon()
        Dim dt As DataTable = Nothing
        dt = Nothing
        objLeavesTypes = New LeavesTypes()
        dt = objLeavesTypes.GetAllForDDL
        ProjectCommon.FillRadComboBox(ddlLeaveType, dt, "LeaveName", _
                                     "LeaveArabicName", "LeaveId")
    End Sub

    Private Sub FillGrades()
        objEmp_Grade = New Emp_Grade
        Dim dt As DataTable
        dt = objEmp_Grade.GetAll

        If (dt IsNot Nothing) Then
            Dim dtGrades As DataTable = dt
            If (dtGrades IsNot Nothing) Then
                If (dtGrades.Rows.Count > 0) Then
                    Dim dtSource As New DataTable
                    dtSource.Columns.Add("GradeId")
                    dtSource.Columns.Add("GradeName")
                    Dim drRow As DataRow
                    drRow = dtSource.NewRow()

                    For Item As Integer = 0 To dtGrades.Rows.Count - 1
                        Dim drSource As DataRow
                        drSource = dtSource.NewRow
                        Dim dcCell1 As New DataColumn
                        Dim dcCell2 As New DataColumn
                        dcCell1.ColumnName = "GradeId"
                        dcCell2.ColumnName = "GradeName"
                        dcCell1.DefaultValue = dtGrades.Rows(Item)("GradeId")
                        dcCell2.DefaultValue = dtGrades.Rows(Item)("GradeCode") + "-" + IIf(Lang = CtlCommon.Lang.EN, dtGrades.Rows(Item)("GradeName"), dtGrades.Rows(Item)("GradeArabicName"))
                        drSource("GradeId") = dcCell1.DefaultValue
                        drSource("GradeName") = dcCell2.DefaultValue
                        dtSource.Rows.Add(drSource)
                    Next
                    If (dt IsNot Nothing) Then
                        Dim dv As New DataView(dtSource)
                        If SessionVariables.CultureInfo = "ar-JO" Then
                            For Each row As DataRowView In dv
                                If (Not GradeId = 0) Then
                                    If GradeId = row("GradeId") Then
                                        cblGradeList.Items.Add(New ListItem(row("GradeName").ToString(), row("GradeId").ToString()))

                                        Exit For
                                    End If
                                Else
                                    cblGradeList.Items.Add(New ListItem(row("GradeName").ToString(), row("GradeId").ToString()))
                                End If
                            Next
                        Else
                            For Each row As DataRowView In dv

                                If (Not GradeId = 0) Then
                                    If GradeId = row("GradeId") Then
                                        cblGradeList.Items.Add(New ListItem(row("GradeName").ToString(), row("GradeId").ToString()))
                                        Exit For
                                    End If
                                Else
                                    cblGradeList.Items.Add(New ListItem(row("GradeName").ToString(), row("GradeId").ToString()))
                                End If
                            Next
                        End If
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub FillLeaveGrades()
        objEmp_GradeLeavesTypes = New Emp_GradeLeavesTypes
        Dim dt As DataTable
        With objEmp_GradeLeavesTypes
            .FK_LeaveId = LeaveId
            dt = .GetAllByFK_LeaveId()
        End With
        For Each row As DataRow In dt.Rows
            For Each chkBox As ListItem In cblGradeList.Items
                If (chkBox.Value = row(0)) Then
                    chkBox.Selected = True
                End If
            Next
        Next
    End Sub

    Private Sub FillEmployeeType()
        Dim dtEmp_logicalGroup As New DataTable
        dtEmp_logicalGroup = Nothing
        objEmp_logicalGroup = New Emp_logicalGroup
        dtEmp_logicalGroup = objEmp_logicalGroup.GetAll()
        ProjectCommon.FillRadComboBox(RadCmbBxEmployeeType, dtEmp_logicalGroup, _
                                      "GroupName", "GroupArabicName", "GroupId")
    End Sub

    Private Sub FillLevels()
        objOrgLevel = New OrgLevel
        With objOrgLevel
            CtlCommon.FillTelerikDropDownList(radcmbLevels, .GetAll_Company, Lang)
        End With
    End Sub

    Private Sub Refresh_AutoApprovePolicyCheckList()
        chkAutoApprovePolicy.Items.Clear()
        chkAutoApprovePolicy.Items.Add("0")
        chkAutoApprovePolicy.Items.Add("1")
        chkAutoApprovePolicy.Items.Add("2")
        If Lang = CtlCommon.Lang.AR Then
            chkAutoApprovePolicy.Items(0).Value = 1
            chkAutoApprovePolicy.Items(0).Text = "المدير المباشر"
            chkAutoApprovePolicy.Items(1).Value = 2
            chkAutoApprovePolicy.Items(1).Text = "الموارد البشرية"
            chkAutoApprovePolicy.Items(2).Value = 3
            chkAutoApprovePolicy.Items(2).Text = "المدير العام"
        Else
            chkAutoApprovePolicy.Items(0).Value = 1
            chkAutoApprovePolicy.Items(0).Text = "Direct Manager"
            chkAutoApprovePolicy.Items(1).Value = 2
            chkAutoApprovePolicy.Items(1).Text = "Human Resource"
            chkAutoApprovePolicy.Items(2).Value = 3
            chkAutoApprovePolicy.Items(2).Text = "General Manager"
        End If
    End Sub

    Private Sub FillChkAutoApprovePolicy()
        objLeavesTypes = New LeavesTypes
        objAPP_Settings = New APP_Settings
        objAPP_Settings.GetByPK()

        Refresh_AutoApprovePolicyCheckList()

        If objAPP_Settings.PermApprovalFromPermission = True Then
            objLeavesTypes.LeaveId = LeaveId
            objLeavesTypes.GetByPK()

            If objLeavesTypes.LeaveApproval = 1 Then '---DM Only
                chkAutoApprovePolicy.Items.RemoveAt(1) '---HR
                chkAutoApprovePolicy.Items.RemoveAt(1) '---GM
            ElseIf objLeavesTypes.LeaveApproval = 2 Then '---HR Only
                chkAutoApprovePolicy.Items.RemoveAt(0) '---DM
                chkAutoApprovePolicy.Items.RemoveAt(1) '---GM
            ElseIf objLeavesTypes.LeaveApproval = 3 Then '---DM & HR
                chkAutoApprovePolicy.Items.RemoveAt(2) '---GM
            End If
        Else
            If objAPP_Settings.LeaveApproval = 1 Then '---DM Only
                chkAutoApprovePolicy.Items.RemoveAt(1) '---HR
                chkAutoApprovePolicy.Items.RemoveAt(1) '---GM
            ElseIf objAPP_Settings.LeaveApproval = 2 Then '---HR Only
                chkAutoApprovePolicy.Items.RemoveAt(0) '---DM
                chkAutoApprovePolicy.Items.RemoveAt(1) '---GM
            ElseIf objAPP_Settings.LeaveApproval = 3 Then '---DM & HR
                chkAutoApprovePolicy.Items.RemoveAt(2) '---GM
            End If
        End If
    End Sub

    Private Sub FillBalanceGrades()
        objEmp_Grade = New Emp_Grade
        Dim dt As DataTable
        dt = objEmp_Grade.GetAll

        If (dt IsNot Nothing) Then
            Dim dtGrades As DataTable = dt
            If (dtGrades IsNot Nothing) Then
                If (dtGrades.Rows.Count > 0) Then
                    Dim dtSource As New DataTable
                    dtSource.Columns.Add("GradeId")
                    dtSource.Columns.Add("GradeName")
                    Dim drRow As DataRow
                    drRow = dtSource.NewRow()

                    For Item As Integer = 0 To dtGrades.Rows.Count - 1
                        Dim drSource As DataRow
                        drSource = dtSource.NewRow
                        Dim dcCell1 As New DataColumn
                        Dim dcCell2 As New DataColumn
                        dcCell1.ColumnName = "GradeId"
                        dcCell2.ColumnName = "GradeName"
                        dcCell1.DefaultValue = dtGrades.Rows(Item)("GradeId")
                        dcCell2.DefaultValue = dtGrades.Rows(Item)("GradeCode") + "-" + IIf(Lang = CtlCommon.Lang.EN, dtGrades.Rows(Item)("GradeName"), dtGrades.Rows(Item)("GradeArabicName"))
                        drSource("GradeId") = dcCell1.DefaultValue
                        drSource("GradeName") = dcCell2.DefaultValue
                        dtSource.Rows.Add(drSource)
                    Next
                    If (dt IsNot Nothing) Then
                        Dim dv As New DataView(dtSource)
                        If SessionVariables.CultureInfo = "ar-JO" Then
                            For Each row As DataRowView In dv
                                If (Not GradeId = 0) Then
                                    If GradeId = row("GradeId") Then
                                        cblBalanceGradeList.Items.Add(New ListItem(row("GradeName").ToString(), row("GradeId").ToString()))

                                        Exit For
                                    End If
                                Else
                                    cblBalanceGradeList.Items.Add(New ListItem(row("GradeName").ToString(), row("GradeId").ToString()))
                                End If
                            Next
                        Else
                            For Each row As DataRowView In dv

                                If (Not GradeId = 0) Then
                                    If GradeId = row("GradeId") Then
                                        cblBalanceGradeList.Items.Add(New ListItem(row("GradeName").ToString(), row("GradeId").ToString()))
                                        Exit For
                                    End If
                                Else
                                    cblBalanceGradeList.Items.Add(New ListItem(row("GradeName").ToString(), row("GradeId").ToString()))
                                End If
                            Next
                        End If
                    End If
                End If
            End If
        End If

    End Sub

    Function ifexistGradeBalance(ByVal FK_LeaveId As Integer, ByVal FK_GradeId As Integer, ByVal GradeBalance As String) As Boolean
        If Not GradeBalancedt Is Nothing AndAlso GradeBalancedt.Rows.Count > 0 Then
            If FK_LeaveId > 0 Then
                For Each i In GradeBalancedt.Rows
                    If (i("FK_GradeId") = FK_GradeId) And (i("FK_LeaveId") <> FK_LeaveId) And (i("GradeBalance") <> GradeBalance) Then
                        Return True
                    End If
                Next
            Else
                For Each i In GradeBalancedt.Rows
                    If i("FK_GradeId") = FK_GradeId Then
                        Return True
                    End If
                Next
            End If
        End If
        Return False
    End Function

    Sub createGradeBalancedt()
        GradeBalancedt = New DataTable
        Dim dc As DataColumn
        dc = New DataColumn
        dc.ColumnName = "MaximumGradeBalanceId"
        dc.DataType = GetType(Integer)
        GradeBalancedt.Columns.Add(dc)
        GradeBalancedt.Columns("MaximumGradeBalanceId").AutoIncrement = True
        GradeBalancedt.Columns("MaximumGradeBalanceId").AutoIncrementSeed = 1
        GradeBalancedt.Columns("MaximumGradeBalanceId").AutoIncrementStep = 1

        dc = New DataColumn
        dc.ColumnName = "FK_LeaveId"
        dc.DataType = GetType(Integer)
        GradeBalancedt.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "FK_GradeId"
        dc.DataType = GetType(Integer)
        GradeBalancedt.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "GradeName"
        dc.DataType = GetType(String)
        GradeBalancedt.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "GradeBalance"
        dc.DataType = GetType(String)
        GradeBalancedt.Columns.Add(dc)

    End Sub

    Sub FillGradeBalanceGrid()
        Try
            Dim objEmp_GradeLeavesTypes As New Emp_GradeLeavesTypes
            Dim dt As DataTable = GradeBalancedt
            MaximumGradeBalanceId = CInt(CType(dgrdGradeBalance.SelectedItems(0), GridDataItem).GetDataKeyValue("MaximumGradeBalanceId").ToString())
            GradeId = CType(dgrdGradeBalance.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_GradeId").ToString()
        Catch ex As Exception
        End Try
    End Sub

    Sub fillGradeBalance()
        If LeaveId > 0 Then

            objEmp_GradeLeavesTypes = New Emp_GradeLeavesTypes
            objEmp_GradeLeavesTypes.FK_LeaveId = LeaveId
            GradeBalancedt = objEmp_GradeLeavesTypes.GetAllInnerByFK_LeaveId
            Dim dc As DataColumn
            dc = New DataColumn
            dc.ColumnName = "MaximumGradeBalanceId"
            dc.DataType = GetType(Integer)
            GradeBalancedt.Columns.Add(dc)


            For i As Integer = 0 To GradeBalancedt.Rows.Count - 1
                GradeBalancedt.Rows(i)("MaximumGradeBalanceId") = i + 1
            Next
            GradeBalancedt.Columns("MaximumGradeBalanceId").AutoIncrement = True
            GradeBalancedt.Columns("MaximumGradeBalanceId").AutoIncrementSeed = 1
            GradeBalancedt.Columns("MaximumGradeBalanceId").AutoIncrementStep = 1



            dgrdGradeBalance.DataSource = GradeBalancedt
            dgrdGradeBalance.DataBind()
        Else
            createGradeBalancedt()
            dgrdGradeBalance.DataSource = GradeBalancedt
            dgrdGradeBalance.DataBind()
        End If
    End Sub

    Private Sub ClearGradeBalance()
        GradeBalancedt = New DataTable
        dgrdGradeBalance.DataSource = GradeBalancedt
        dgrdGradeBalance.DataBind()
        MaximumGradeBalanceId = 0
    End Sub

#End Region

End Class
