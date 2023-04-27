Imports TA.Definitions
Imports System.Data
Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports TA.LookUp
Imports TA.Employees
Imports TA.Admin
Imports TA.Security
Imports SmartV.UTILITIES.ProjectCommon

Partial Class Emp_Permissions_Types
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objPermissionsTypes As PermissionsTypes
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    ' Variables will be used to fill the RadComboBox controls of the 
    ' LeaveTypes and Policy reasons
    Private objLeavesTypes As LeavesTypes
    Private objTA_Reason As TA_Reason
    Private objDurations As Durations
    Dim objPermissionTypeOccurance As New PermissionTypeOccurance
    Dim objPermissionTypeDuration As New PermissionTypeDuration
    Private Lang As CtlCommon.Lang
    Private objProjectCommon As New ProjectCommon
    Private objOrgCompany As OrgCompany
    Private objVersion As SmartV.Version.version
    Private objOrgEntity As OrgEntity
    Dim objUserPrivileg_Entities As UserPrivileg_Entities
    Dim objUserPrivileg_Companies As UserPrivileg_Companies
    Dim objSYSUsers As SYSUsers
    Private objAPP_Settings As APP_Settings
    Dim objEmployee_Type As Employee_Type
    Dim objEmp_logicalGroup As Emp_logicalGroup
    Private objPermissionsTypes_Company As PermissionsTypes_Company
    Private objPermissionsTypes_Entity As PermissionsTypes_Entity
    Private objOrgLevel As OrgLevel

    '------------------ Adding Dashes For Entity Levels----------------'
    Private dtCustomizedRecordsOrder As New DataTable
    Private cmb As CheckBoxList
    Private ddl As DropDownList
    Private dtDataSource As DataTable
    Private valField As String
    Private EngNameTextField As String
    Private ArNameTextField As String
    Private ParentField As String
    Private Sequence As String
    '------------------ Adding Dashes For Entity Levels----------------'

#End Region

#Region "Properties"

    Private Property PermissionId() As Integer
        Get
            Return ViewState("PermissionId")
        End Get
        Set(ByVal value As Integer)
            ViewState("PermissionId") = value
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

    Public Property MaximumDurationdt() As DataTable
        Get
            Return ViewState("MaximumDurationdt")
        End Get
        Set(ByVal value As DataTable)
            ViewState("MaximumDurationdt") = value
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

    Private Property MaximumDurationId() As Integer
        Get
            Return ViewState("MaximumDurationId")
        End Get
        Set(ByVal value As Integer)
            ViewState("MaximumDurationId") = value
        End Set
    End Property

    Private Property PermId() As Integer
        Get


            Return ViewState("PermId")

        End Get
        Set(ByVal value As Integer)
            ViewState("PermId") = value
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

#Region "Page Events"

    Protected Sub dgrdVwPermissionTypes_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdVwPermissionTypes.Skin))
    End Function

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

            FillLists()
            FillGridView()
            FillDurations()
            createMaximumAllwoeddt()
            createMaximumDurationdt()
            FillDropDown()
            FillCompanies()
            FillEntities()
            FillLevels()
            reqLeaveTypeDuration.Enabled = False
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            rfvDuration.InitialValue = ResourceManager.GetString("PleaseSelect", CultureInfo)
            rfvAllowedDuration.InitialValue = ResourceManager.GetString("PleaseSelect", CultureInfo)
            reqcmbBxTaReason.InitialValue = ResourceManager.GetString("PleaseSelect", CultureInfo)
            UserCtrlPermTypes.HeaderText = ResourceManager.GetString("PermitType", CultureInfo)
            radAllowedTime.TextWithLiterals = "0000"
            If (objVersion.HasMultiCompany() = False) Then
                rblPermissionUnits.Items.RemoveAt(1)
            End If
            FillEmployeeType()
            objAPP_Settings = New APP_Settings
            With objAPP_Settings
                .GetByPK()
                If .PermApprovalFromPermission = True Then
                    trPermissionApproval.Visible = True
                Else
                    trPermissionApproval.Visible = False
                End If
            End With
        End If

        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdVwPermissionTypes.ClientID + "');")
        btnDeleteMaximumAllowed.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdMaximumAllowed.ClientID + "');")
        btnRemoveAllowedDuration.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdAllowedDuration.ClientID + "');")

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not pnlPermissionTypes.FindControl(row("AddBtnName")) Is Nothing Then
                        pnlPermissionTypes.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not pnlPermissionTypes.FindControl(row("DeleteBtnName")) Is Nothing Then
                        pnlPermissionTypes.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not pnlPermissionTypes.FindControl(row("EditBtnName")) Is Nothing Then
                        pnlPermissionTypes.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not pnlPermissionTypes.FindControl(row("PrintBtnName")) Is Nothing Then
                        pnlPermissionTypes.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

    End Sub

    Protected Sub dgrdVwPermissionTypes_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdVwPermissionTypes.NeedDataSource
        objPermissionsTypes = New PermissionsTypes()
        dgrdVwPermissionTypes.DataSource = objPermissionsTypes.GetAll()
    End Sub

    Protected Sub dgrdVwPermissionTypes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdVwPermissionTypes.SelectedIndexChanged
        ClearAll()
        PermissionId = Convert.ToInt32(DirectCast(dgrdVwPermissionTypes.SelectedItems(0), GridDataItem).GetDataKeyValue("PermId").ToString())
        FillControls()
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object,
                                   ByVal e As System.EventArgs) _
                                   Handles btnClear.Click
        ClearAll()
        FillGridView()
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object,
                                      ByVal e As System.EventArgs) _
                                      Handles btnDelete.Click
        Dim errNum As Integer = -1

        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdVwPermissionTypes.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim strCode As String = row.GetDataKeyValue("PermName").ToString()
                objPermissionsTypes = New PermissionsTypes()
                objPermissionsTypes.PermId = Convert.ToInt32(row.GetDataKeyValue("PermId").ToString())
                objPermissionsTypes.GetByPK()
                'If (objPermissionsTypes.FK_LeaveIdDeductBalance = 0 And objPermissionsTypes.FK_RelatedTAReason = 0) Then
                errNum = objPermissionsTypes.Delete()
                If errNum = 0 Then
                    CtlCommon.ShowMessage(Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
                    FillGridView()
                Else
                    CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")
                End If
                'Else
                '    CtlCommon.ShowMessage(Me, IIf(Lang = CtlCommon.Lang.AR, " لا يمكن حذف المغادرة لانها مرتبطة بجداول اخرى  ", "Permission type can't be deleted because  its connected to another table  "))
                'End If
            End If
        Next


        FillGridView()
        ClearAll()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object,
                                ByVal e As System.EventArgs) Handles btnSave.Click
        objPermissionsTypes = New PermissionsTypes()
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim errorNum As Integer = -1

        If Convert.ToInt64(txtRadMinDuration.Text) > Convert.ToInt64(txtRadMaxDuration.Text) Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("MaximumGrreaterMinimum"), "info")
            Return
        End If
        Dim intHourMonthlyBalance As Integer = 0
        Dim intMinutMonthlyBalance As Integer = 0
        Dim monthlyBalance As Integer = 0
        If chkDeductFromOvertime.Checked = False Then
            intHourMonthlyBalance = Convert.ToInt32(txtRadHourMonthlyBalance.Text) * 60
            intMinutMonthlyBalance = Convert.ToInt32(txtRadMonthlyBalance.Text)
            monthlyBalance = intHourMonthlyBalance + intMinutMonthlyBalance
        End If

        Dim intAllowedTime As Integer
        Dim intAllowedTimeBefore As Integer
        ' Set data into object for Add / Update

        With objPermissionsTypes
            ' Get data from terlerik text boxes
            .MaxDuration = txtRadMaxDuration.Text
            .MinDuration = txtRadMinDuration.Text
            .MonthlyBalance = monthlyBalance

            ' Get data from .net text boxes
            .allowedDurationPerPeriod = String.Empty ' txtAllowDurationPeriod.Text
            '.AllowedOccurancePerPeriod = txtAllowOccurencePeriod.Text
            .PermName = txtName.Text.Trim()
            .PermArabicName = txtArabicName.Text.Trim()
            ' Get data from check boxes
            .ApprovalRequired = chckApprovalRequired.Checked
            .IsConsiderInWork = chckIsConsiderInWork.Checked

            ' Get data from combo boxes
            .FK_RelatedTAReason = cmbBxTaReason.SelectedValue

            If Not String.IsNullOrEmpty(RadCmpPermissions.SelectedValue) Then
                .FK_LeaveIdtoallowduration = Convert.ToInt32(RadCmpPermissions.SelectedValue)
            Else
                .FK_LeaveIdtoallowduration = -1
            End If

            If Not String.IsNullOrEmpty(txtRadLeaveTypeDuration.Text) Then
                .DurationAllowedwithleave = Convert.ToInt32(txtRadLeaveTypeDuration.Text)
            End If

            If cmbBxRadLeaveTypes.SelectedValue <> -1 And cmbBxRadLeaveTypes.SelectedValue <> -2 Then
                .FK_LeaveIdDeductBalance = cmbBxRadLeaveTypes.SelectedValue

            Else
                ' The user choose NA item from rad combo box 
                ' of deduct balance from
                .FK_LeaveIdDeductBalance = 0
            End If

            .ShouldComplete50WHRS = chkCompleteWHRS.Checked
            If Not radtxtAllowedAfterDays.Text = Nothing Then
                .AllowedAfterDays = radtxtAllowedAfterDays.Text
            Else
                .AllowedAfterDays = Nothing
            End If
            If Not radtxtAllowedBeforeDays.Text = Nothing Then
                .AllowedBeforeDays = radtxtAllowedBeforeDays.Text
            Else
                .AllowedBeforeDays = Nothing
            End If
            .ExcludeManagers_FromAfterBefore = chkExcludeManagers.Checked

            .AllowedForManagers = chkAllowedManagers.Checked
            .AllowedForSelfService = chkAllowedForSelfService.Checked
            .NotAllowedWhenHasStudyOrNursing = chkNotAllowedWhenHasStudyorNursing.Checked
            .ShowRemainingBalance = chkShowRemainingBalance.Checked

            If rdlHasFlexiblePermission.SelectedValue = 1 Then
                .HasFlexiblePermission = True
            Else
                .HasFlexiblePermission = False
            End If
            If rdlHasPermissionForPeriod.SelectedValue = 1 Then
                .HasPermissionForPeriod = True
            Else
                .HasPermissionForPeriod = False
            End If
            If rdlFullDayPermission.SelectedValue = 1 Then
                .HasFullDayPermission = True
            Else
                .HasFullDayPermission = False
            End If
            If rdlConsiderRequestWithinBalance.SelectedValue = 1 Then
                .ConsiderRequestWithinBalance = True
            Else
                .ConsiderRequestWithinBalance = False
            End If
            .AllowForSpecificEmployeeType = chkAllowForSpecificEmployeeType.Checked
            If chkAllowForSpecificEmployeeType.Checked = True Then
                trAllowForSpecificEmployeeType.Visible = True
                .FK_EmployeeTypeId = RadCmbBxEmployeeType.SelectedValue
            Else
                trAllowForSpecificEmployeeType.Visible = False
            End If

            If rdlAttachmentIsMandatory.SelectedValue = 1 Then
                .AttachmentIsMandatory = True
            Else
                .AttachmentIsMandatory = False
            End If
            If rdlRemarksIsMandatory.SelectedValue = 1 Then
                .RemarksIsMandatory = True
            Else
                .RemarksIsMandatory = False
            End If
            .MinDurationAllowedInSelfService = radcmbMinDurationSelfService.SelectedValue
            ' Set systematic values
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            .LAST_UPDATE_DATE = Today.Date
            .CREATED_BY = SessionVariables.LoginUser.ID
            .CREATED_DATE = Today.Date
            .GeneralGuide = txtGeneralGuide.Text
            .GeneralGuideAr = txtGeneralGuideAr.Text
            If chkAllowedAfter.Checked Then
                .Isallowedaftertime = chkAllowedAfter.Checked
                intAllowedTime = (CInt(radAllowedTime.TextWithLiterals.Split(":")(0)) * 60) + CInt(radAllowedTime.TextWithLiterals.Split(":")(1))
                .AllowedAfter = intAllowedTime
            Else
                .Isallowedaftertime = False
            End If
            If chkAllowedBefore.Checked Then
                .IsAllowedBeforeTime = chkAllowedBefore.Checked
                intAllowedTimeBefore = (CInt(radAllowedTimeBefore.TextWithLiterals.Split(":")(0)) * 60) + CInt(radAllowedTimeBefore.TextWithLiterals.Split(":")(1))
                .AllowedBefore = intAllowedTimeBefore
            Else
                .IsAllowedBeforeTime = False
            End If
            If chkAutoApprove.Checked = True Then
                .IsAutoApprove = chkAutoApprove.Checked
                .AutoApproveAfter = radnumAutoApproveAfter.Text
            Else
                .IsAutoApprove = chkAutoApprove.Checked
                .AutoApproveAfter = String.Empty
            End If

            objAPP_Settings = New APP_Settings
            objAPP_Settings.GetByPK()
            If objAPP_Settings.PermApprovalFromPermission = True Then
                .PermissionApproval = rlstApproval.SelectedValue
            End If

            If rblPermissionUnits.SelectedValue = 1 Then
                .IsSpecificCompany = True
                Dim CompanyIsChecked As Boolean = False
                For Each CompanyItem As ListItem In cblCompanies.Items
                    If CompanyItem.Selected Then
                        CompanyIsChecked = True
                        Exit For
                    End If
                Next

                If Not CompanyIsChecked Then
                    If SessionVariables.CultureInfo = "ar-JO" Then
                        CtlCommon.ShowMessage(Me.Page, "الرجاء اختيار شركة واحدة او اكثر", "info")
                    Else
                        CtlCommon.ShowMessage(Me.Page, "Please Select Company(s)", "info")
                    End If

                    Return
                End If

            ElseIf rblPermissionUnits.SelectedValue = 2 Then
                .IsSpecificEntity = True
                Dim EntityIsChecked As Boolean = False
                For Each EntityItem As ListItem In cblEntities.Items
                    If EntityItem.Selected Then
                        EntityIsChecked = True
                        Exit For
                    End If
                Next
                If Not EntityIsChecked Then
                    If SessionVariables.CultureInfo = "ar-JO" Then
                        CtlCommon.ShowMessage(Me.Page, "الرجاء اختيار وحدة عمل او اكثر", "info")
                    Else
                        CtlCommon.ShowMessage(Me.Page, "Please Select Entity(s)", "info")
                    End If

                    Return
                End If
            Else
                .IsSpecificCompany = False
                .IsSpecificEntity = False
            End If
            .Perm_NotificationException = chkPerm_NotificationException.Checked
            For Each item As ListItem In chkAutoApprovePolicy.Items
                If item.Selected = True Then
                    .AutoApprovePolicy += item.Value + ","
                End If
            Next

            .ConvertToLeave_ExceedDuration = chkConvertToLeave_ExceedDuration.Checked
            If chkConvertToLeave_ExceedDuration.Checked = True Then
                If Not radcmbxAnnualLeaveId.SelectedValue = -1 Then
                    .AnnualLeaveId_ToDeductPermission = radcmbxAnnualLeaveId.SelectedValue
                End If
            End If

            .MustHaveTransaction = chkMustHaveTransaction.Checked
            .DeductBalanceFromOvertime = chkDeductFromOvertime.Checked
            If chkDeductFromOvertime.Checked = True Then
                .OvertimeBalanceDays = txtOvertimeBalanceDays.Text
            End If

            .ValidateDelayPermissions = chkValidateDelayPermissions.Checked
            If chkValidateDelayPermissions.Checked = True Then
                .DelayPermissionsValidation = radnumDelayPermissionValidation.Text
            Else
                .DelayPermissionsValidation = Nothing
            End If

            If rblAllowWhenInSufficient.SelectedValue = 1 Then
                .AllowWhenInSufficient = True
            Else
                .AllowWhenInSufficient = False
            End If

            If rblHasPermissionTimeControls.SelectedValue = 1 Then
                .HasPermissionTimeControls = True
            Else
                .HasPermissionTimeControls = False
            End If

            If Not radcmbLevels.SelectedValue = -1 Then
                .PermissionRequestManagerLevelRequired = radcmbLevels.SelectedValue
            End If

        End With


        If PermissionId = 0 Then
            ' Do add operation 
            errorNum = objPermissionsTypes.Add()
            If errorNum = 0 Then
                PermissionId = objPermissionsTypes.PermId
            End If
        Else
            ' Do update operations
            objPermissionsTypes.PermId = PermissionId
            errorNum = objPermissionsTypes.Update()
        End If

        'If chkAllowedForSpecificEntity.Checked Then
        '    errorNum = SavePermissionTypes_Entities()
        'End If

        If errorNum = 0 Then
            If rblPermissionUnits.SelectedValue = 1 Then '---Specific Company
                objPermissionsTypes_Company = New PermissionsTypes_Company
                objPermissionsTypes_Company.FK_PermId = PermissionId
                objPermissionsTypes_Company.DeleteByPermId()
                For Each CompanyItem As ListItem In cblCompanies.Items
                    If CompanyItem.Selected Then
                        With objPermissionsTypes_Company
                            .FK_CompanyId = CInt(CompanyItem.Value)
                            .FK_PermId = PermissionId
                            .Add()
                        End With
                    End If
                Next
            ElseIf rblPermissionUnits.SelectedValue = 2 Then '---Specific Entity
                objPermissionsTypes_Entity = New PermissionsTypes_Entity
                objPermissionsTypes_Entity.FK_PermId = PermissionId
                objPermissionsTypes_Entity.DeleteByPermId()
                For Each EntityItem As ListItem In cblEntities.Items
                    If EntityItem.Selected Then
                        With objPermissionsTypes_Entity
                            .FK_EntityId = CInt(EntityItem.Value)
                            .FK_PermId = PermissionId
                            .Add()
                        End With
                    End If
                Next
            End If

            CtlCommon.ShowMessage(Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")

            objPermissionTypeOccurance = New PermissionTypeOccurance
            If PermissionId > 0 Then
                With objPermissionTypeOccurance
                    .Add_Bulk(MaximumAlloweddt, PermissionId)

                End With

                With objPermissionTypeDuration
                    .Add_Bulk(MaximumDurationdt, PermissionId)
                End With

            End If
            FillGridView()
            ClearAll()

        ElseIf errorNum = -5 Then
            CtlCommon.ShowMessage(Me, ResourceManager.GetString("EnNameExist", CultureInfo), "info")
        ElseIf errorNum = -6 Then
            CtlCommon.ShowMessage(Me, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
    End Sub

    Protected Sub dgrdMaximumAllowed__SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdMaximumAllowed.SelectedIndexChanged
        FillMaximumAllowed()
    End Sub

    Protected Sub dgrdMaximumDuration__SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdAllowedDuration.SelectedIndexChanged
        FillMaximumDuration()
    End Sub

    Protected Sub btnSaveAllowedOccurance_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveAllowedOccurance.Click
        Dim err As Integer = -1
        Dim dr As DataRow
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Try
            If ifexistAllowedOccurance(txtMaximimOccur.Text, RadCmbBxDuration.SelectedValue) Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("MaxDurationEntered", CultureInfo), "info")
                Exit Sub
            End If
            If MaximumAllowedId = 0 Then

                dr = MaximumAlloweddt.NewRow
                ' dr("AbsentRuleId") = AbsentRuleId
                dr("FK_PermId") = PermId
                dr("MaximumOccur") = txtMaximimOccur.Text
                dr("FK_DurationId") = RadCmbBxDuration.SelectedValue
                dr("DurationTypeName") = RadCmbBxDuration.Text
                MaximumAlloweddt.Rows.Add(dr)
            Else
                If MaximumAlloweddt.Rows.Count > 0 Then
                    dr = MaximumAlloweddt.Select("MaximumAllowedId= " & MaximumAllowedId)(0)
                    dr("MaximumAllowedId") = MaximumAllowedId
                    dr("FK_PermId") = PermId
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
    End Sub

    Protected Sub btnSaveAllowedDuration_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveAllowedDuration.Click
        Dim err As Integer = -1
        Dim dr As DataRow
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Try
            If ifexistAllowedDuration(txtMaximumDuration.Text, RadCmbBxAllowedDuration.SelectedValue) Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("MaxAllowed", CultureInfo), "info")
                Exit Sub
            End If
            If MaximumDurationId = 0 Then

                dr = MaximumDurationdt.NewRow
                ' dr("AbsentRuleId") = AbsentRuleId
                dr("FK_PermId") = PermId
                dr("MaximumDuration") = txtMaximumDuration.Text()
                dr("FK_DurationId") = RadCmbBxAllowedDuration.SelectedValue
                dr("DurationTypeName") = RadCmbBxAllowedDuration.Text
                dr("MaximumRamadanDuration") = txtMaximumRamadanDuration.Text
                dr("MaximumDuration_WithStudyNursing") = txtMaximumDuration_WithStudyNursing.Text
                MaximumDurationdt.Rows.Add(dr)
            Else
                If MaximumDurationdt.Rows.Count > 0 Then
                    dr = MaximumDurationdt.Select("MaximumDurationId= " & MaximumDurationId)(0)
                    dr("MaximumDurationId") = MaximumDurationId
                    dr("FK_PermId") = PermId
                    dr("MaximumDuration") = txtMaximumDuration.Text
                    dr("FK_DurationId") = RadCmbBxAllowedDuration.SelectedValue
                    dr("DurationTypeName") = RadCmbBxAllowedDuration.Text
                    dr("MaximumRamadanDuration") = txtMaximumRamadanDuration.Text
                    dr("MaximumDuration_WithStudyNursing") = txtMaximumDuration_WithStudyNursing.Text
                End If
            End If
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("AllowedDurationAddSuccess", CultureInfo), "success")
            dgrdAllowedDuration.DataSource = MaximumDurationdt
            dgrdAllowedDuration.DataBind()
            MaximumDurationId = 0
            txtMaximumDuration.Text = ""
            RadCmbBxAllowedDuration.SelectedIndex = -1
            txtMaximumRamadanDuration.Text = String.Empty
            txtMaximumDuration_WithStudyNursing.Text = String.Empty
        Catch ex As Exception
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrAddMaxAllow", CultureInfo), "error")
        End Try
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
            txtMaximimOccur.Text = String.Empty
            RadCmbBxDuration.SelectedIndex = -1
        Next
    End Sub

    Protected Sub btnRemoveAllowedDuration_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemoveAllowedDuration.Click
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdAllowedDuration.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim intDurationId As Integer = Convert.ToInt32(row.GetDataKeyValue("MaximumDurationId").ToString())
                MaximumDurationdt.Rows.Remove(MaximumDurationdt.Select("MaximumDurationId = " & intDurationId)(0))
            End If
            dgrdAllowedDuration.DataSource = MaximumDurationdt
            dgrdAllowedDuration.DataBind()
            MaximumDurationId = 0
            txtMaximumDuration.Text = String.Empty
            RadCmbBxAllowedDuration.SelectedIndex = -1
        Next
    End Sub

    Protected Sub ddlLeaveType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadCmpPermissions.SelectedIndexChanged
        'If Not RadCmpPermissions.SelectedValue = "-1" Then
        '    trAllowedDuration.Visible = True
        '    reqLeaveTypeDuration.Enabled = True
        'Else
        '    trAllowedDuration.Visible = False
        '    reqLeaveTypeDuration.Enabled = False
        'End If
    End Sub

    'Protected Sub RadCmbBxCompanies_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxCompanies.SelectedIndexChanged
    '    CompanyChanged()
    'End Sub

    Protected Sub chkAllowedAfter_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAllowedAfter.CheckedChanged
        If chkAllowedAfter.Checked Then
            trAllowedAfterTime.Visible = True
        Else
            trAllowedAfterTime.Visible = False
        End If
    End Sub

    Protected Sub chkAllowedBefore_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkAllowedBefore.CheckedChanged
        If chkAllowedBefore.Checked Then
            trAllowedBeforeTime.Visible = True
        Else
            trAllowedBeforeTime.Visible = False
        End If
    End Sub

    'Protected Sub chkExcludeManagers_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkExcludeManagers.CheckedChanged
    '    If chkExcludeManagers.Checked Then
    '        trExcludManagers.Visible = True
    '    Else
    '        trExcludManagers.Visible = False
    '    End If
    'End Sub

    Protected Sub chkAllowForSpecificEmployeeType_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkAllowForSpecificEmployeeType.CheckedChanged
        If chkAllowForSpecificEmployeeType.Checked Then
            trAllowForSpecificEmployeeType.Visible = True
            'FillEntities()
            'FillEntity()
        Else
            trAllowForSpecificEmployeeType.Visible = False
        End If
    End Sub

    Protected Sub rblPermissionUnits_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblPermissionUnits.SelectedIndexChanged
        If rblPermissionUnits.SelectedValue = 1 Then
            divCompanies.Visible = True
            divCompaniesSelect.Visible = True
            divEntities.Visible = False
            divEntitiesSelect.Visible = False
        ElseIf rblPermissionUnits.SelectedValue = 2 Then
            divEntities.Visible = True
            divEntitiesSelect.Visible = True
            divCompanies.Visible = False
            divCompaniesSelect.Visible = False
        Else
            divCompanies.Visible = False
            divCompaniesSelect.Visible = False
            divEntities.Visible = False
            divEntitiesSelect.Visible = False
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

    Protected Sub chkConvertToLeave_ExceedDuration_CheckedChanged(sender As Object, e As EventArgs) Handles chkConvertToLeave_ExceedDuration.CheckedChanged
        If chkConvertToLeave_ExceedDuration.Checked = True Then
            dvAnnualLeaveId_ToDeductPermission.Visible = True
            FillAnnualLeaves()
        Else
            dvAnnualLeaveId_ToDeductPermission.Visible = False
        End If
    End Sub

    Protected Sub chkDeductFromOvertime_CheckedChanged(sender As Object, e As EventArgs) Handles chkDeductFromOvertime.CheckedChanged
        If chkDeductFromOvertime.Checked = True Then
            dvDeductLeaveBalance.Visible = False
            dvDeductMonthlyBalance.Visible = False
            reqHoursMonthlyBalance.Enabled = False
            reqMonthlyBalance.Enabled = False
            dvDeductOvertime.Visible = True
            rfvOvertimeBalanceDays.Enabled = True
            rdlFullDayPermission.SelectedValue = 2
            rdlFullDayPermission.Enabled = False
            rdlHasPermissionForPeriod.SelectedValue = 2
            rdlHasPermissionForPeriod.Enabled = False
            rdlConsiderRequestWithinBalance.SelectedValue = 2
            rdlConsiderRequestWithinBalance.Enabled = False
            chkShowRemainingBalance.Enabled = False
        Else
            dvDeductLeaveBalance.Visible = True
            dvDeductMonthlyBalance.Visible = True
            reqHoursMonthlyBalance.Enabled = True
            reqMonthlyBalance.Enabled = True
            dvDeductOvertime.Visible = False
            rfvOvertimeBalanceDays.Enabled = False
            rdlFullDayPermission.ClearSelection()
            rdlFullDayPermission.Enabled = True
            rdlHasPermissionForPeriod.ClearSelection()
            rdlHasPermissionForPeriod.Enabled = True
            rdlConsiderRequestWithinBalance.ClearSelection()
            rdlConsiderRequestWithinBalance.Enabled = True
            chkShowRemainingBalance.Enabled = True
        End If

    End Sub

    Protected Sub chkValidateDelayPermissions_CheckedChanged(sender As Object, e As EventArgs) Handles chkValidateDelayPermissions.CheckedChanged
        If chkValidateDelayPermissions.Checked = True Then
            dvDelayPermissionValidation.Visible = True
        Else
            dvDelayPermissionValidation.Visible = False
        End If
    End Sub

#End Region

#Region "Methods"

    Private Sub FillControls()
        ' Get data row of selected record
        objPermissionsTypes = New PermissionsTypes()
        objPermissionsTypes_Company = New PermissionsTypes_Company
        objPermissionsTypes_Entity = New PermissionsTypes_Entity
        objOrgEntity = New OrgEntity

        objPermissionsTypes.PermId = PermissionId
        objPermissionsTypes.GetByPK()
        With objPermissionsTypes
            ' set data to terlerik text boxes
            txtRadMaxDuration.Text = .MaxDuration
            txtRadMinDuration.Text = .MinDuration
            txtGeneralGuide.Text = .GeneralGuide
            txtGeneralGuideAr.Text = .GeneralGuideAr
            Dim iSpan As TimeSpan = TimeSpan.FromMinutes(.MonthlyBalance)
            Dim intTotalHour As Integer = Convert.ToInt32(Math.Floor(iSpan.TotalHours))
            Dim intTotaoMinuts As Integer = iSpan.TotalMinutes - (intTotalHour * 60)
            'txtRadMonthlyBalance.Text = iSpan.Minutes.ToString.PadLeft(2, "0"c)
            txtRadMonthlyBalance.Text = intTotaoMinuts.ToString()
            'txtRadHourMonthlyBalance.Text = iSpan.Hours.ToString.PadLeft(2, "0"c)
            txtRadHourMonthlyBalance.Text = intTotalHour.ToString()
            ' Get data from .net text boxes
            'txtAllowDurationPeriod.Text = .allowedDurationPerPeriod
            'txtAllowOccurencePeriod.Text = .AllowedOccurancePerPeriod
            txtArabicName.Text = .PermArabicName
            txtName.Text = .PermName
            ' set status to check boxes
            chckApprovalRequired.Checked = .ApprovalRequired
            chckIsConsiderInWork.Checked = .IsConsiderInWork
            ' set value to combo boxes
            cmbBxTaReason.SelectedValue = .FK_RelatedTAReason

            If Not .FK_LeaveIdtoallowduration = 0 Then
                RadCmpPermissions.SelectedValue = .FK_LeaveIdtoallowduration
            Else
                RadCmpPermissions.SelectedValue = -1
            End If

            'If Not RadCmpPermissions.SelectedValue = -1 Then
            txtRadLeaveTypeDuration.Text = .DurationAllowedwithleave.ToString()
            'trAllowedDuration.Visible = True
            'Else
            '    txtRadLeaveTypeDuration.Text = String.Empty
            '    trAllowedDuration.Visible = False
            'End If

            If .FK_LeaveIdDeductBalance = 0 Then
                cmbBxRadLeaveTypes.SelectedValue = -2
            Else
                cmbBxRadLeaveTypes.SelectedValue = .FK_LeaveIdDeductBalance
            End If
            If .Isallowedaftertime = True Then
                chkAllowedAfter.Checked = True
                trAllowedAfterTime.Visible = True
                radAllowedTime.Text = CtlCommon.GetFullTimeString(.AllowedAfter)
            End If
            If .IsAllowedBeforeTime = True Then
                chkAllowedBefore.Checked = True
                trAllowedBeforeTime.Visible = True
                radAllowedTimeBefore.Text = CtlCommon.GetFullTimeString(.AllowedBefore)
            End If
            chkCompleteWHRS.Checked = .ShouldComplete50WHRS
            If .AllowedAfterDays = Nothing Then
                radtxtAllowedAfterDays.Text = String.Empty
            Else
                radtxtAllowedAfterDays.Text = .AllowedAfterDays
            End If

            If .AllowedBeforeDays = Nothing Then
                radtxtAllowedBeforeDays.Text = String.Empty
            Else
                radtxtAllowedBeforeDays.Text = .AllowedBeforeDays
            End If

            chkAllowedForSelfService.Checked = .AllowedForSelfService

            chkExcludeManagers.Checked = .ExcludeManagers_FromAfterBefore
            'If .ExcludeManagers_FromAfterBefore = True Then
            '    FillEmployee()
            'End If
            chkAllowedManagers.Checked = .AllowedForManagers
            chkNotAllowedWhenHasStudyorNursing.Checked = .NotAllowedWhenHasStudyOrNursing
            chkShowRemainingBalance.Checked = .ShowRemainingBalance

            If .HasFlexiblePermission = True Then
                rdlHasFlexiblePermission.SelectedValue = 1
            Else
                rdlHasFlexiblePermission.SelectedValue = 2
            End If

            If .HasPermissionForPeriod = True Then
                rdlHasPermissionForPeriod.SelectedValue = 1
            Else
                rdlHasPermissionForPeriod.SelectedValue = 2
            End If

            If .HasFullDayPermission = True Then
                rdlFullDayPermission.SelectedValue = 1
            Else
                rdlFullDayPermission.SelectedValue = 2
            End If

            If .ConsiderRequestWithinBalance = True Then
                rdlConsiderRequestWithinBalance.SelectedValue = 1
            Else
                rdlConsiderRequestWithinBalance.SelectedValue = 2
            End If
            chkAllowForSpecificEmployeeType.Checked = .AllowForSpecificEmployeeType
            If .AllowForSpecificEmployeeType = True Then
                trAllowForSpecificEmployeeType.Visible = True
                'FillEmployeeType()
                RadCmbBxEmployeeType.SelectedValue = .FK_EmployeeTypeId
            End If

            If .AttachmentIsMandatory = True Then
                rdlAttachmentIsMandatory.SelectedValue = 1
            Else
                rdlAttachmentIsMandatory.SelectedValue = 2
            End If
            If .RemarksIsMandatory = True Then
                rdlRemarksIsMandatory.SelectedValue = 1
            Else
                rdlRemarksIsMandatory.SelectedValue = 2
            End If

            If trPermissionApproval.Visible Then
                If Not .PermissionApproval = 0 Then
                    rlstApproval.SelectedValue = .PermissionApproval
                End If
            End If
            radcmbMinDurationSelfService.SelectedValue = .MinDurationAllowedInSelfService

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

            If .IsSpecificCompany = True Then
                If Not (objVersion.HasMultiCompany = False) Then
                    Dim dtCompanies As DataTable
                    objPermissionsTypes_Company.FK_PermId = PermissionId
                    dtCompanies = objPermissionsTypes_Company.GetByPermId
                    rblPermissionUnits.SelectedValue = 1
                    divCompanies.Visible = True
                    divCompaniesSelect.Visible = True
                    cblCompanies.Items.Clear()
                    FillCompanies()
                    If Not dtCompanies Is Nothing And dtCompanies.Rows.Count > 0 Then
                        For Each dr As DataRow In dtCompanies.Rows
                            For Each CompanyItem As ListItem In cblCompanies.Items
                                If CompanyItem.Value = dr(1) Then
                                    CompanyItem.Selected = True
                                End If
                            Next
                        Next
                    End If
                End If
            ElseIf .IsSpecificEntity = True Then
                Dim dtEntities As DataTable
                objPermissionsTypes_Entity.FK_PermId = PermissionId
                dtEntities = objPermissionsTypes_Entity.GetByPermId
                rblPermissionUnits.SelectedValue = 2
                divEntities.Visible = True
                divEntitiesSelect.Visible = True
                cblEntities.Items.Clear()
                FillEntities()
                If Not dtEntities Is Nothing And dtEntities.Rows.Count > 0 Then
                    For Each dr As DataRow In dtEntities.Rows
                        For Each EntityItem As ListItem In cblEntities.Items
                            If EntityItem.Value = dr(1) Then
                                EntityItem.Selected = True
                            End If
                        Next
                    Next
                End If
            Else
                rblPermissionUnits.SelectedValue = 0
            End If
            chkPerm_NotificationException.Checked = .Perm_NotificationException
            If Not .AutoApprovePolicy = Nothing Then
                Dim AutoApprovePolicy As String() = Split(.AutoApprovePolicy.ToString.Trim, ",")
                FillChkAutoApprovePolicy()
                For Each item In AutoApprovePolicy
                    If Not item = Nothing Then
                        chkAutoApprovePolicy.Items.FindByValue(item).Selected = True
                    End If

                Next
            End If

            chkConvertToLeave_ExceedDuration.Checked = .ConvertToLeave_ExceedDuration
            If chkConvertToLeave_ExceedDuration.Checked = True Then
                dvAnnualLeaveId_ToDeductPermission.Visible = True
                radcmbxAnnualLeaveId.SelectedValue = .AnnualLeaveId_ToDeductPermission
            Else
                dvAnnualLeaveId_ToDeductPermission.Visible = False
            End If

            chkMustHaveTransaction.Checked = .MustHaveTransaction
            chkDeductFromOvertime.Checked = .DeductBalanceFromOvertime
            If .DeductBalanceFromOvertime = True Then
                dvDeductOvertime.Visible = True
                txtOvertimeBalanceDays.Text = .OvertimeBalanceDays
                dvDeductLeaveBalance.Visible = False
                dvDeductMonthlyBalance.Visible = False
                reqHoursMonthlyBalance.Enabled = False
                reqMonthlyBalance.Enabled = False

                rdlFullDayPermission.SelectedValue = 2
                rdlFullDayPermission.Enabled = False
                rdlHasPermissionForPeriod.SelectedValue = 2
                rdlHasPermissionForPeriod.Enabled = False
                rdlConsiderRequestWithinBalance.SelectedValue = 2
                rdlConsiderRequestWithinBalance.Enabled = False
                chkShowRemainingBalance.Enabled = False

            Else
                dvDeductOvertime.Visible = False
                txtOvertimeBalanceDays.Text = String.Empty
                dvDeductLeaveBalance.Visible = True
                dvDeductMonthlyBalance.Visible = True
                reqHoursMonthlyBalance.Enabled = True
                reqMonthlyBalance.Enabled = True

                rdlFullDayPermission.Enabled = True
                rdlHasPermissionForPeriod.Enabled = True
                rdlConsiderRequestWithinBalance.Enabled = True
                chkShowRemainingBalance.Enabled = True
            End If

            chkValidateDelayPermissions.Checked = .ValidateDelayPermissions
            If .ValidateDelayPermissions = True Then
                dvDelayPermissionValidation.Visible = True
                radnumDelayPermissionValidation.Text = .DelayPermissionsValidation
            Else
                dvDelayPermissionValidation.Visible = False
                radnumDelayPermissionValidation.Text = String.Empty
            End If

            If .AllowWhenInSufficient = True Then
                rblAllowWhenInSufficient.SelectedValue = 1
            Else
                rblAllowWhenInSufficient.SelectedValue = 2
            End If

            If .HasPermissionTimeControls = True Then
                rblHasPermissionTimeControls.SelectedValue = 1
            Else
                rblHasPermissionTimeControls.SelectedValue = 2
            End If

            radcmbLevels.SelectedValue = .PermissionRequestManagerLevelRequired

        End With
        'FillChkAutoApprovePolicy()
        fillAllowedOccurance()
        fillAllowedDuration()
    End Sub

    Sub fillAllowedOccurance()
        If PermissionId > 0 Then

            objPermissionTypeOccurance = New PermissionTypeOccurance
            objPermissionTypeOccurance.FK_PermId = PermissionId
            MaximumAlloweddt = objPermissionTypeOccurance.GetByPK
            Dim dc As DataColumn
            dc = New DataColumn
            dc.ColumnName = "MaximumAllowedId"
            dc.DataType = GetType(Integer)
            MaximumAlloweddt.Columns.Add(dc)


            dc = New DataColumn
            dc.ColumnName = "DurationTypeName"
            dc.DataType = GetType(String)
            MaximumAlloweddt.Columns.Add(dc)

            For i As Integer = 0 To MaximumAlloweddt.Rows.Count - 1
                If (MaximumAlloweddt.Rows(i)("FK_DurationId") = 2) Then
                    MaximumAlloweddt.Rows(i)("DurationTypeName") = "Week"
                ElseIf MaximumAlloweddt.Rows(i)("FK_DurationId") = 3 Then
                    MaximumAlloweddt.Rows(i)("DurationTypeName") = "Month"
                ElseIf MaximumAlloweddt.Rows(i)("FK_DurationId") = 4 Then
                    MaximumAlloweddt.Rows(i)("DurationTypeName") = "Year"
                ElseIf MaximumAlloweddt.Rows(i)("FK_DurationId") = 1 Then
                    MaximumAlloweddt.Rows(i)("DurationTypeName") = "Day"
                End If
                MaximumAlloweddt.Rows(i)("MaximumAllowedId") = i + 1
            Next
            MaximumAlloweddt.Columns("MaximumAllowedId").AutoIncrement = True
            MaximumAlloweddt.Columns("MaximumAllowedId").AutoIncrementSeed = 1
            MaximumAlloweddt.Columns("MaximumAllowedId").AutoIncrementStep = 1

            dgrdMaximumAllowed.DataSource = MaximumAlloweddt
            dgrdMaximumAllowed.DataBind()
        Else
            createMaximumAllwoeddt()
            dgrdMaximumAllowed.DataSource = MaximumAlloweddt
            dgrdMaximumAllowed.DataBind()
        End If
    End Sub

    Sub fillAllowedDuration()
        If PermissionId > 0 Then

            objPermissionTypeDuration = New PermissionTypeDuration
            objPermissionTypeDuration.FK_PermId = PermissionId
            MaximumDurationdt = objPermissionTypeDuration.GetByPK
            Dim dc As DataColumn
            dc = New DataColumn
            dc.ColumnName = "MaximumDurationId"
            dc.DataType = GetType(Integer)
            MaximumDurationdt.Columns.Add(dc)


            dc = New DataColumn
            dc.ColumnName = "DurationTypeName"
            dc.DataType = GetType(String)
            MaximumDurationdt.Columns.Add(dc)

            For i As Integer = 0 To MaximumDurationdt.Rows.Count - 1
                If (MaximumDurationdt.Rows(i)("FK_DurationId") = 2) Then
                    MaximumDurationdt.Rows(i)("DurationTypeName") = "Week"
                ElseIf MaximumDurationdt.Rows(i)("FK_DurationId") = 3 Then
                    MaximumDurationdt.Rows(i)("DurationTypeName") = "Month"
                ElseIf MaximumDurationdt.Rows(i)("FK_DurationId") = 4 Then
                    MaximumDurationdt.Rows(i)("DurationTypeName") = "Year"
                ElseIf MaximumDurationdt.Rows(i)("FK_DurationId") = 1 Then
                    MaximumDurationdt.Rows(i)("DurationTypeName") = "Day"
                End If
                MaximumDurationdt.Rows(i)("MaximumDurationId") = i + 1
            Next
            MaximumDurationdt.Columns("MaximumDurationId").AutoIncrement = True
            MaximumDurationdt.Columns("MaximumDurationId").AutoIncrementSeed = 1
            MaximumDurationdt.Columns("MaximumDurationId").AutoIncrementStep = 1

            dgrdAllowedDuration.DataSource = MaximumDurationdt
            dgrdAllowedDuration.DataBind()
        Else
            createMaximumDurationdt()
            dgrdAllowedDuration.DataSource = MaximumDurationdt
            dgrdAllowedDuration.DataBind()
        End If
    End Sub

    Private Sub FillGridView()
        Try
            objPermissionsTypes = New PermissionsTypes()
            dgrdVwPermissionTypes.DataSource = objPermissionsTypes.GetAll()
            dgrdVwPermissionTypes.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FillDurations()
        Try
            objDurations = New Durations()
            Dim dt As DataTable
            dt = objDurations.GetAll()
            CtlCommon.FillTelerikDropDownList(RadCmbBxDuration, dt, Lang)
            CtlCommon.FillTelerikDropDownList(RadCmbBxAllowedDuration, objDurations.GetAll(), Lang)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FillLists()
        Dim dt As DataTable = Nothing
        objTA_Reason = New TA_Reason()
        objLeavesTypes = New LeavesTypes()
        dt = objTA_Reason.GetAll()
        ' Fill ComboBox of TAReason , 
        If dt IsNot Nothing Then
            ProjectCommon.FillRadComboBox(cmbBxTaReason, dt, "ReasonName", "ReasonArabicName")
        End If
        dt = Nothing
        ' Fill ComboBox of LeaveTypes will not use common functions
        dt = objLeavesTypes.GetAll()
        If dt IsNot Nothing Then
            ProjectCommon.FillRadComboBox(cmbBxRadLeaveTypes, dt, "LeaveName", "LeaveArabicName", "LeaveId", True)

        End If

    End Sub

    Private Sub ClearAll()
        ' Clear the text boxes
        'txtAllowDurationPeriod.Text = String.Empty
        'txtAllowOccurencePeriod.Text = String.Empty
        txtArabicName.Text = String.Empty
        txtName.Text = String.Empty
        txtRadMaxDuration.Text = String.Empty
        txtRadMinDuration.Text = String.Empty
        txtRadMonthlyBalance.Text = String.Empty
        txtRadHourMonthlyBalance.Text = String.Empty
        ' uncheck check boxes
        chckApprovalRequired.Checked = False
        chckIsConsiderInWork.Checked = False
        ' Reset combo boxes 
        cmbBxRadLeaveTypes.SelectedIndex = 0
        cmbBxTaReason.SelectedIndex = 0

        ' Reset Id to prepare for next add operation
        PermissionId = 0
        ' Remove sorting and sorting arrows
        clearMaximumAllowed()
        createMaximumAllwoeddt()
        createMaximumDurationdt()
        txtGeneralGuide.Text = String.Empty
        txtGeneralGuideAr.Text = String.Empty
        RadCmpPermissions.ClearSelection()
        'trAllowedDuration.Visible = False
        'reqLeaveTypeDuration.Enabled = False
        txtRadLeaveTypeDuration.Text = String.Empty
        chkAllowedAfter.Checked = False
        radAllowedTime.TextWithLiterals = "0000"
        trAllowedAfterTime.Visible = False

        chkAllowedBefore.Checked = False
        radAllowedTimeBefore.TextWithLiterals = "0000"
        trAllowedBeforeTime.Visible = False

        chkCompleteWHRS.Checked = False
        radtxtAllowedAfterDays.Text = String.Empty
        radtxtAllowedBeforeDays.Text = String.Empty
        chkExcludeManagers.Checked = False
        chkAllowedManagers.Checked = False

        chkAllowedForSelfService.Checked = False
        chkNotAllowedWhenHasStudyorNursing.Checked = False
        chkShowRemainingBalance.Checked = False
        chkAllowForSpecificEmployeeType.Checked = False
        trAllowForSpecificEmployeeType.Visible = False
        RadCmbBxEmployeeType.SelectedValue = -1

        rblPermissionUnits.SelectedValue = 0
        cblCompanies.ClearSelection()
        cblEntities.ClearSelection()
        divCompanies.Visible = False
        divCompaniesSelect.Visible = False
        divEntities.Visible = False
        divEntitiesSelect.Visible = False
        TabContainer1.ActiveTabIndex = 0
        radcmbMinDurationSelfService.SelectedValue = -1
        dvAutoApprove.Visible = False
        dvAutoApproveAfter.Visible = False
        chkAutoApprove.Checked = False
        radnumAutoApproveAfter.Text = String.Empty
        chkPerm_NotificationException.Checked = False
        chkAutoApprovePolicy.ClearSelection()
        chkConvertToLeave_ExceedDuration.Checked = False
        radcmbxAnnualLeaveId.SelectedValue = -1
        chkMustHaveTransaction.Checked = False
        chkDeductFromOvertime.Checked = False
        dvDeductOvertime.Visible = False
        rfvOvertimeBalanceDays.Enabled = False
        dvDeductLeaveBalance.Visible = True
        dvDeductMonthlyBalance.Visible = True
        reqHoursMonthlyBalance.Enabled = True
        reqMonthlyBalance.Enabled = True

        rdlFullDayPermission.ClearSelection()
        rdlHasPermissionForPeriod.ClearSelection()
        rdlConsiderRequestWithinBalance.ClearSelection()
        chkValidateDelayPermissions.Checked = False
        dvDelayPermissionValidation.Visible = False

        rblHasPermissionTimeControls.ClearSelection()
        radcmbLevels.SelectedValue = -1
    End Sub

    Sub clearMaximumAllowed()
        MaximumAlloweddt = New DataTable
        MaximumDurationdt = New DataTable
        dgrdMaximumAllowed.DataSource = MaximumAlloweddt
        dgrdMaximumAllowed.DataBind()
        dgrdAllowedDuration.DataSource = MaximumDurationdt
        dgrdAllowedDuration.DataBind()
        MaximumAllowedId = 0
        MaximumDurationId = 0
        txtMaximimOccur.Text = String.Empty
        txtMaximumDuration.Text = String.Empty
        RadCmbBxDuration.SelectedIndex = -1
        RadCmbBxAllowedDuration.SelectedIndex = -1
        txtMaximumRamadanDuration.Text = String.Empty
        txtMaximumDuration_WithStudyNursing.Text = String.Empty
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

    Function ifexistAllowedDuration(ByVal MaximumAllowed As String, ByVal DurationId As Integer) As Boolean
        If Not MaximumDurationdt Is Nothing AndAlso MaximumDurationdt.Rows.Count > 0 Then
            If MaximumDurationId > 0 Then
                For Each i In MaximumDurationdt.Rows
                    If (i("FK_DurationId") = DurationId) And (i("MaximumDurationId") <> MaximumDurationId) Then
                        Return True
                    End If
                Next
            Else
                For Each i In MaximumDurationdt.Rows
                    If i("FK_DurationId") = DurationId Then
                        Return True
                    End If
                Next
            End If
        End If
        Return False
    End Function

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
        dc.ColumnName = "FK_PermId"
        dc.DataType = GetType(Integer)
        MaximumAlloweddt.Columns.Add(dc)

    End Sub

    Sub createMaximumDurationdt()
        MaximumDurationdt = New DataTable
        Dim dc As DataColumn
        dc = New DataColumn
        dc.ColumnName = "MaximumDurationId"
        dc.DataType = GetType(Integer)
        MaximumDurationdt.Columns.Add(dc)
        MaximumDurationdt.Columns("MaximumDurationId").AutoIncrement = True
        MaximumDurationdt.Columns("MaximumDurationId").AutoIncrementSeed = 1
        MaximumDurationdt.Columns("MaximumDurationId").AutoIncrementStep = 1

        dc = New DataColumn
        dc.ColumnName = "MaximumDuration"
        dc.DataType = GetType(String)
        MaximumDurationdt.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "FK_DurationId"
        dc.DataType = GetType(Integer)
        MaximumDurationdt.Columns.Add(dc)


        dc = New DataColumn
        dc.ColumnName = "DurationTypeName"
        dc.DataType = GetType(String)
        MaximumDurationdt.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "FK_PermId"
        dc.DataType = GetType(Integer)
        MaximumDurationdt.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "MaximumRamadanDuration"
        dc.DataType = GetType(Integer)
        MaximumDurationdt.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "MaximumDuration_WithStudyNursing"
        dc.DataType = GetType(String)
        MaximumDurationdt.Columns.Add(dc)

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

    Sub FillMaximumDuration()
        Try

            Dim objLeaveTypes As New LeavesTypes
            Dim dt As DataTable = MaximumDurationdt
            MaximumDurationId = CInt(CType(dgrdAllowedDuration.SelectedItems(0), GridDataItem).GetDataKeyValue("MaximumDurationId").ToString())
            txtMaximumDuration.Text = CType(dgrdAllowedDuration.SelectedItems(0), GridDataItem).GetDataKeyValue("MaximumDuration").ToString()
            txtMaximumRamadanDuration.Text = CType(dgrdAllowedDuration.SelectedItems(0), GridDataItem).GetDataKeyValue("MaximumRamadanDuration").ToString()
            txtMaximumDuration_WithStudyNursing.Text = CType(dgrdAllowedDuration.SelectedItems(0), GridDataItem).GetDataKeyValue("MaximumDuration_WithStudyNursing").ToString()
            RadCmbBxAllowedDuration.SelectedValue = CInt(CType(dgrdAllowedDuration.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_DurationId").ToString())
        Catch ex As Exception
        End Try
    End Sub

    Sub FillDropDown()
        objProjectCommon = New ProjectCommon()
        Dim dt As DataTable = Nothing
        dt = Nothing
        objPermissionsTypes = New PermissionsTypes()
        dt = objPermissionsTypes.GetAll()

        If dt IsNot Nothing Then
            CtlCommon.FillTelerikDropDownList(RadCmpPermissions, dt, Lang)
        End If

    End Sub

    'Public Sub FillEmployee()
    '    cblEmpList.Items.Clear()
    '    cblEmpList.Text = String.Empty

    '    If MultiEmployeeFilterUC.CompanyID <> 0 Then
    '        Dim objEmployee As New Employee
    '        objEmployee.FK_CompanyId = MultiEmployeeFilterUC.CompanyID

    '        If (Not MultiEmployeeFilterUC.EntityID = 0) AndAlso MultiEmployeeFilterUC.SearchType = "1" Then
    '            objEmployee.FK_EntityId = MultiEmployeeFilterUC.EntityID
    '        Else
    '            objEmployee.FK_EntityId = -1
    '        End If

    '        If (Not MultiEmployeeFilterUC.WorkGroupID = 0) AndAlso MultiEmployeeFilterUC.SearchType = "2" Then
    '            objEmployee.FK_LogicalGroup = MultiEmployeeFilterUC.WorkGroupID
    '            objEmployee.FilterType = "L"
    '        Else
    '            objEmployee.FK_LogicalGroup = -1
    '        End If

    '        If (Not MultiEmployeeFilterUC.WorkLocationsID) AndAlso MultiEmployeeFilterUC.SearchType = "3" Then
    '            objEmployee.FK_WorkLocation = MultiEmployeeFilterUC.WorkLocationsID
    '            objEmployee.FilterType = "W"
    '        Else
    '            objEmployee.FK_WorkLocation = -1
    '        End If
    '        Dim dt As DataTable = objEmployee.GetManagersByCompany
    '        If (dt IsNot Nothing) Then
    '            Dim dtEmployees = dt
    '            If (dtEmployees IsNot Nothing) Then
    '                If (dtEmployees.Rows.Count > 0) Then
    '                    Dim dtSource As New DataTable
    '                    dtSource.Columns.Add("EmployeeId")
    '                    dtSource.Columns.Add("EmployeeName")
    '                    Dim drRow As DataRow
    '                    drRow = dtSource.NewRow()
    '                    For Item As Integer = 0 To dtEmployees.Rows.Count - 1
    '                        Dim drSource As DataRow
    '                        drSource = dtSource.NewRow
    '                        Dim dcCell1 As New DataColumn
    '                        Dim dcCell2 As New DataColumn
    '                        dcCell1.ColumnName = "EmployeeId"
    '                        dcCell2.ColumnName = "EmployeeName"
    '                        dcCell1.DefaultValue = dtEmployees.Rows(Item)(0)
    '                        dcCell2.DefaultValue = dtEmployees.Rows(Item)(3) + "-" + IIf(Lang = CtlCommon.Lang.EN, dtEmployees.Rows(Item)(1), dtEmployees.Rows(Item)(2))
    '                        drSource("EmployeeId") = dcCell1.DefaultValue
    '                        drSource("EmployeeName") = dcCell2.DefaultValue
    '                        dtSource.Rows.Add(drSource)
    '                    Next
    '                    Dim dv As New DataView(dtSource)
    '                    If SessionVariables.CultureInfo = "ar-JO" Then
    '                        'dv.Sort = "EmployeeName"
    '                        For Each row As DataRowView In dv
    '                            If (Not MultiEmployeeFilterUC.EmployeeID = 0) AndAlso MultiEmployeeFilterUC.SearchType = "4" Then
    '                                If MultiEmployeeFilterUC.EmployeeID = row("EmployeeId") Then
    '                                    cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
    '                                    Exit For
    '                                End If
    '                            Else
    '                                cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
    '                            End If
    '                        Next
    '                    Else
    '                        For Each row As DataRowView In dv
    '                            If (Not MultiEmployeeFilterUC.EmployeeID = 0) AndAlso MultiEmployeeFilterUC.SearchType = "4" Then
    '                                If MultiEmployeeFilterUC.EmployeeID = row("EmployeeId") Then
    '                                    cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
    '                                    Exit For
    '                                End If
    '                            Else
    '                                cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
    '                            End If
    '                        Next
    '                    End If
    '                End If
    '            End If
    '        End If
    '    End If

    'End Sub

    'Private Sub FillCompany()
    '    Dim dt As DataTable
    '    objOrgCompany = New OrgCompany
    '    dt = objOrgCompany.GetAllforddl
    '    CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies, dt, Lang)
    'End Sub

    'Public Sub CompanyChanged()
    '    'EmployeeFilterUC.FillEntity()
    '    MultiEmployeeFilterUC.CompanyID = RadCmbBxCompanies.SelectedValue
    '    MultiEmployeeFilterUC.FillList()
    '    FillEmployee()
    'End Sub

    'Public Sub EntityChanged()

    '    FillEmployee()

    'End Sub

    'Public Sub WorkGroupChanged()

    '    FillEmployee()

    'End Sub

    'Public Sub WorkLocationsChanged()

    '    FillEmployee()

    'End Sub

    'Public Sub FillEntities()
    '    cblEmpList.Items.Clear()
    '    cblEmpList.Text = String.Empty

    '    Dim objOrgEntity As New OrgEntity

    '    Dim dt As DataTable = objOrgEntity.GetAll
    '    If (dt IsNot Nothing) Then
    '        Dim dtEntities = dt
    '        If (dtEntities IsNot Nothing) Then
    '            If (dtEntities.Rows.Count > 0) Then
    '                Dim dtSource As New DataTable
    '                dtSource.Columns.Add("EntityId")
    '                dtSource.Columns.Add("EntityName")
    '                Dim drRow As DataRow
    '                drRow = dtSource.NewRow()
    '                For Item As Integer = 0 To dtEntities.Rows.Count - 1
    '                    Dim drSource As DataRow
    '                    drSource = dtSource.NewRow
    '                    Dim dcCell1 As New DataColumn
    '                    Dim dcCell2 As New DataColumn
    '                    dcCell1.ColumnName = "EntityId"
    '                    dcCell2.ColumnName = "EntityName"
    '                    dcCell1.DefaultValue = dtEntities.Rows(Item)(0)
    '                    dcCell2.DefaultValue = IIf(Lang = CtlCommon.Lang.EN, dtEntities.Rows(Item)(4), dtEntities.Rows(Item)(5))
    '                    drSource("EntityId") = dcCell1.DefaultValue
    '                    drSource("EntityName") = dcCell2.DefaultValue
    '                    dtSource.Rows.Add(drSource)
    '                Next
    '                Dim dv As New DataView(dtSource)
    '                If SessionVariables.CultureInfo = "ar-JO" Then
    '                    For Each row As DataRowView In dv
    '                        cblEntities.Items.Add(New ListItem(row("EntityName").ToString(), row("EntityId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
    '                    Next
    '                Else
    '                    For Each row As DataRowView In dv
    '                        cblEntities.Items.Add(New ListItem(row("EntityName").ToString(), row("EntityId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
    '                    Next
    '                End If
    '            End If
    '        End If
    '    End If


    'End Sub

    'Private Function SavePermissionTypes_Entities() As Integer
    '    Dim flag As Boolean = False
    '    Dim err As Integer = -1

    '    For Each item As ListItem In cblEntities.Items
    '        If item.Selected Then
    '            flag = True
    '            objPermissionTypes_Entity = New PermissionTypes_Entity
    '            With objPermissionTypes_Entity
    '                .FK_EntityId = item.Value
    '                .PermissionTypeId = PermissionId
    '                err = .Add()
    '            End With
    '        End If
    '    Next
    '    Return err
    'End Function

    'Private Sub FillSelectedEntities()
    '    Dim dt As DataTable
    '    objPermissionTypes_Entity = New PermissionTypes_Entity
    '    With objPermissionTypes_Entity
    '        .PermissionTypeId = PermissionId
    '        dt = .GetByPermissionTypeId()
    '    End With
    '    For Each row As DataRow In dt.Rows
    '        cblEntities.SelectedValue = row(0).ToString
    '    Next
    'End Sub

    Private Sub FillEmployeeType()
        Dim dtEmp_logicalGroup As New DataTable
        dtEmp_logicalGroup = Nothing
        objEmp_logicalGroup = New Emp_logicalGroup
        dtEmp_logicalGroup = objEmp_logicalGroup.GetAll()
        ProjectCommon.FillRadComboBox(RadCmbBxEmployeeType, dtEmp_logicalGroup,
                                      "GroupName", "GroupArabicName", "GroupId")
    End Sub

    Public Sub FillEntities()
        'cblEmpList.Items.Clear()
        'cblEmpList.Text = String.Empty

        Dim objOrgEntity As New OrgEntity

        If (objVersion.HasMultiCompany() = False) Then
            objOrgEntity.FK_CompanyId = objVersion.GetCompanyId()
        End If

        Dim dt As DataTable = objOrgEntity.GetAll_WithCompanyName
        FillMultiLevelCheckBoxList(cblEntities, dt, "EntityId", "EntityName", "EntityArabicName", "FK_ParentId")
        'If (dt IsNot Nothing) Then
        If (dtCustomizedRecordsOrder IsNot Nothing) Then
            'Dim dtEntities = dt
            Dim dtEntities = dtCustomizedRecordsOrder
            If (dtEntities IsNot Nothing) Then
                If (dtEntities.Rows.Count > 0) Then
                    Dim dtSource As New DataTable
                    dtSource.Columns.Add("EntityId")
                    dtSource.Columns.Add("EntityName")
                    Dim drRow As DataRow
                    drRow = dtSource.NewRow()
                    For Item As Integer = 0 To dtEntities.Rows.Count - 1
                        Dim drSource As DataRow
                        drSource = dtSource.NewRow
                        Dim dcCell1 As New DataColumn
                        Dim dcCell2 As New DataColumn
                        dcCell1.ColumnName = "EntityId"
                        dcCell2.ColumnName = "EntityName"
                        dcCell1.DefaultValue = dtEntities.Rows(Item)(0)
                        dcCell2.DefaultValue = IIf(Lang = CtlCommon.Lang.EN, dtEntities.Rows(Item)(1), dtEntities.Rows(Item)(2))
                        drSource("EntityId") = dcCell1.DefaultValue
                        drSource("EntityName") = dcCell2.DefaultValue
                        dtSource.Rows.Add(drSource)
                    Next
                    Dim dv As New DataView(dtSource)
                    If SessionVariables.CultureInfo = "ar-JO" Then
                        For Each row As DataRowView In dv
                            cblEntities.Items.Add(New ListItem(row("EntityName").ToString(), row("EntityId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
                        Next
                    Else
                        For Each row As DataRowView In dv
                            cblEntities.Items.Add(New ListItem(row("EntityName").ToString(), row("EntityId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
                        Next
                    End If
                End If
            End If
        End If


    End Sub

    Public Sub FillCompanies()
        'cblEmpList.Items.Clear()
        'cblEmpList.Text = String.Empty

        Dim objOrgCompany As New OrgCompany

        Dim dt As DataTable = objOrgCompany.GetAll
        If (dt IsNot Nothing) Then
            Dim dtCompanies = dt
            If (dtCompanies IsNot Nothing) Then
                If (dtCompanies.Rows.Count > 0) Then
                    Dim dtSource As New DataTable
                    dtSource.Columns.Add("CompanyId")
                    dtSource.Columns.Add("CompanyName")
                    Dim drRow As DataRow
                    drRow = dtSource.NewRow()
                    For Item As Integer = 0 To dtCompanies.Rows.Count - 1
                        Dim drSource As DataRow
                        drSource = dtSource.NewRow
                        Dim dcCell1 As New DataColumn
                        Dim dcCell2 As New DataColumn
                        dcCell1.ColumnName = "CompanyId"
                        dcCell2.ColumnName = "CompanyName"
                        dcCell1.DefaultValue = dtCompanies.Rows(Item)(0)
                        dcCell2.DefaultValue = IIf(Lang = CtlCommon.Lang.EN, dtCompanies.Rows(Item)(1), dtCompanies.Rows(Item)(2))
                        drSource("CompanyId") = dcCell1.DefaultValue
                        drSource("CompanyName") = dcCell2.DefaultValue
                        dtSource.Rows.Add(drSource)
                    Next
                    Dim dv As New DataView(dtSource)
                    If SessionVariables.CultureInfo = "ar-JO" Then
                        For Each row As DataRowView In dv
                            cblCompanies.Items.Add(New ListItem(row("CompanyName").ToString(), row("CompanyId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
                        Next
                    Else
                        For Each row As DataRowView In dv
                            cblCompanies.Items.Add(New ListItem(row("CompanyName").ToString(), row("CompanyId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
                        Next
                    End If
                End If
            End If
        End If


    End Sub

    Private Sub FillChkAutoApprovePolicy()
        objPermissionsTypes = New PermissionsTypes
        objAPP_Settings = New APP_Settings
        objAPP_Settings.GetByPK()

        Refresh_AutoApprovePolicyCheckList()

        If objAPP_Settings.PermApprovalFromPermission = True Then
            objPermissionsTypes.PermId = PermissionId
            objPermissionsTypes.GetByPK()

            If objPermissionsTypes.PermissionApproval = 1 Then '---DM Only
                chkAutoApprovePolicy.Items.RemoveAt(1) '---HR
                chkAutoApprovePolicy.Items.RemoveAt(1) '---GM
            ElseIf objPermissionsTypes.PermissionApproval = 2 Then '---HR Only
                chkAutoApprovePolicy.Items.RemoveAt(0) '---DM
                chkAutoApprovePolicy.Items.RemoveAt(1) '---GM
            ElseIf objPermissionsTypes.PermissionApproval = 3 Then '---DM & HR
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

    Private Sub FillAnnualLeaves()
        objLeavesTypes = New LeavesTypes
        With objLeavesTypes
            CtlCommon.FillTelerikDropDownList(radcmbxAnnualLeaveId, .GetAll, Lang)
        End With
    End Sub

    Private Sub FillLevels()
        objOrgLevel = New OrgLevel
        With objOrgLevel
            CtlCommon.FillTelerikDropDownList(radcmbLevels, .GetAll_Company, Lang)
        End With
    End Sub

    '------------------ Adding Dashes For Entity Levels----------------'
    Public Sub FillMultiLevelCheckBoxList(ByVal cmb As CheckBoxList, ByVal dtDataSource As DataTable,
                                             ByVal valField As String, ByVal EngNameTextField As String,
                                             ByVal ArNameTextField As String, ByVal ParentField As String,
                                             Optional ByVal OrderByField As String = Nothing)

        If dtDataSource IsNot Nothing Then

            cmb.Items.Clear()


            Try
                Me.dtDataSource = dtDataSource
                Me.cmb = cmb
                Me.valField = valField
                Me.EngNameTextField = EngNameTextField
                Me.ArNameTextField = ArNameTextField
                Me.ParentField = ParentField
                dtCustomizedRecordsOrder.Columns.Add(New DataColumn(valField))
                dtCustomizedRecordsOrder.Columns.Add(New DataColumn(EngNameTextField))
                dtCustomizedRecordsOrder.Columns.Add(New DataColumn(ArNameTextField))
                dtCustomizedRecordsOrder.Columns.Add(New DataColumn(ParentField))
                Dim drChilds As DataRow() = Nothing

                If OrderByField Is Nothing Then
                    Dim drRoots() As DataRow = dtDataSource.Select(ParentField + " is null")
                    addNode(drRoots, drChilds, (drRoots.Count - 1), 0)
                Else
                    Me.Sequence = OrderByField
                    Dim drRoots() As DataRow = dtDataSource.Select(ParentField + " is null", Me.Sequence & " DESC")
                    addNodeOrderBy(drRoots, drChilds, (drRoots.Count - 1), 0)
                End If
            Catch ex As Exception

            End Try
        Else

        End If
    End Sub

    Private Sub addNode(ByVal drRoot() As DataRow,
                           ByVal foundChilds As DataRow(),
                           ByVal count As Integer,
                           ByVal levelNo As Integer)
        If count >= 0 Then
            If levelNo = 0 Then
                ' Get the current root 
                Dim dr As DataRow = drRoot(count)
                dtCustomizedRecordsOrder.Rows.Add(dr(valField), dr(EngNameTextField), dr(ArNameTextField), dr(ParentField))
                ' Check if the root has childs , if exist get ordered 
                ' by Sequence
                foundChilds = dtDataSource.Select(ParentField + "=" &
                                     dr(valField))
                If foundChilds.Count <> 0 Then
                    ' Increase the level to add node on higher 
                    ' level
                    levelNo = levelNo + 1
                    addNode(drRoot, foundChilds, count, levelNo)
                    ' Return to add at lower level
                    levelNo = levelNo - 1
                End If
                count = count - 1
                addNode(drRoot,
                    foundChilds,
                    count,
                    levelNo)
            Else
                For Each row As DataRow In foundChilds
                    ' Prepare the Id and the name of the child 
                    Dim id As Integer = row(valField)
                    Dim EngName As String = row(EngNameTextField)
                    Dim ArName As String = row(ArNameTextField)
                    Dim ParentId As String = row(ParentField)
                    ' Add the new child
                    dtCustomizedRecordsOrder.Rows.Add(id, getPrefix(levelNo) & EngName,
                                                      getPrefix(levelNo) & ArName, ParentId)

                    ' Check if the child have sub-childs , if exist will return
                    ' Order By Sequence number

                    Dim childs As DataRow() =
                        dtDataSource.Select(ParentField & "=" & id)
                    If childs.Length <> 0 Then
                        levelNo = levelNo + 1
                        addNode(drRoot, childs, count, levelNo)
                        levelNo = levelNo - 1
                    End If
                Next
            End If
        Else
            Me.cmb.DataSource = dtCustomizedRecordsOrder
            setLocalizedTextField(cmb, Me.EngNameTextField, Me.ArNameTextField)
            Me.cmb.DataValueField = valField
        End If
    End Sub

    Private Sub addNodeOrderBy(ByVal drRoot() As DataRow,
                        ByVal foundChilds As DataRow(),
                        ByVal count As Integer,
                        ByVal levelNo As Integer)
        If count >= 0 Then
            If levelNo = 0 Then
                ' Get the current root 
                Dim dr As DataRow = drRoot(count)
                dtCustomizedRecordsOrder.Rows.Add(dr(valField), dr(EngNameTextField), dr(ArNameTextField), dr(ParentField))
                ' Check if the root has childs , if exist get ordered 
                ' by Sequence
                foundChilds = dtDataSource.Select(ParentField + "=" &
                                     dr(valField), Sequence & " DESC")

                If foundChilds.Count <> 0 Then
                    ' Increase the level to add node on higher 
                    ' level
                    levelNo = levelNo + 1
                    addNodeOrderBy(drRoot, foundChilds, count, levelNo)
                    ' Return to add at lower level
                    levelNo = levelNo - 1
                End If
                count = count - 1
                addNodeOrderBy(drRoot,
                    foundChilds,
                    count,
                    levelNo)
            Else
                For Each row As DataRow In foundChilds
                    ' Prepare the Id and the name of the child 
                    Dim id As Integer = row(valField)
                    Dim EngName As String = row(EngNameTextField)
                    Dim ArName As String = row(ArNameTextField)
                    Dim ParentId As String = row(ParentField)
                    ' Add the new child
                    dtCustomizedRecordsOrder.Rows.Add(id, getPrefix(levelNo) & EngName,
                                                      getPrefix(levelNo) & ArName, ParentId)
                    ' Check if the child have sub-childs , if exist will return
                    ' Order By Sequence number
                    Dim childs As DataRow() =
                        dtDataSource.Select(ParentField & "=" & id, Sequence & " DESC")
                    If childs.Length <> 0 Then
                        levelNo = levelNo + 1
                        addNodeOrderBy(drRoot, childs, count, levelNo)
                        levelNo = levelNo - 1
                    End If
                Next
            End If
        Else
            Me.cmb.DataSource = dtCustomizedRecordsOrder
            setLocalizedTextField(Me.cmb, Me.EngNameTextField, Me.ArNameTextField)
            Me.cmb.DataValueField = valField
        End If

    End Sub

    Public Function getPrefix(ByVal level As Integer) As String
        ' Generate strings identify a class at a level
        Dim strPrefix As New StringBuilder
        For index As Integer = 0 To level
            strPrefix.Append("-")
        Next
        Return strPrefix.ToString()

    End Function

    Private Sub setLocalizedTextField(ByVal comb As CheckBoxList,
                           ByVal EnName As String, ByVal ArName As String)
        comb.DataTextField = IIf(SessionVariables.CultureInfo = "en-US",
                                                   EnName, ArName)
    End Sub
    '------------------ Adding Dashes For Entity Levels----------------'
#End Region


End Class
