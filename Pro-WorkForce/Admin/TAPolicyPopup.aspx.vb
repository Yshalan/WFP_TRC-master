Imports SmartV.UTILITIES
Imports TA.Admin
Imports TA.Lookup
Imports System.Data
Imports Telerik.Web.UI
Imports TA.DailyTasks

Partial Class Admin_TAPolicyPopup
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Protected dir, textalign As String
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private objTA_Reason As TA_Reason
    Private objApp_Settings As APP_Settings
    Private objTAPolicy_ViolationActions As New TAPolicy_ViolationActions
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

#End Region

#Region "Page Events"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        Else
            Page.UICulture = SessionVariables.CultureInfo
        End If
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If (SessionVariables.CultureInfo Is Nothing) Then
            SessionVariables.CultureInfo = "en-US"
            dir = "ltr"
            textalign = "left"
        Else
            If SessionVariables.CultureInfo = "en-US" Then
                SessionVariables.CultureInfo = "en-US"
                dir = "ltr"
                textalign = "left"
                Page.Title = "TA Policy Details"
            Else
                SessionVariables.CultureInfo = "ar-JO"
                dir = "rtl"
                textalign = "right"
                Page.Title = "تفاصيل سياسة الحضور و الانصراف"
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")

            ElseIf SessionVariables.CultureInfo = "ar-JO" Then

                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            PageHeader1.HeaderText = ResourceManager.GetString("TAPolicy", CultureInfo)
            objApp_Settings = New APP_Settings()
            objApp_Settings.GetByPK()
            If Not objApp_Settings.IsGraceTAPolicy Then
                trGraceIn.Visible = False
                trGraceOut.Visible = False
            End If
            SetReadOnly()
            createViolationdt()
            PrepareViolationInterface()
            CtlCommon.FillTelerikDropDownList(ddlViolationAction, objTAPolicy_ViolationActions.GetAllForDDL(), Lang)
            CtlCommon.FillTelerikDropDownList(ddlViolationAction2, objTAPolicy_ViolationActions.GetAllForDDL(), Lang)
            CtlCommon.FillTelerikDropDownList(ddlViolationAction3, objTAPolicy_ViolationActions.GetAllForDDL(), Lang)
            CtlCommon.FillTelerikDropDownList(ddlViolationAction4, objTAPolicy_ViolationActions.GetAllForDDL(), Lang)
            CtlCommon.FillTelerikDropDownList(ddlViolationAction5, objTAPolicy_ViolationActions.GetAllForDDL(), Lang)
            TAPolicyID = Request.QueryString("ID")
            fillPolicy(TAPolicyID)
        End If
    End Sub

    Protected Sub dgrdAbsentRules_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdAbsentRules.SelectedIndexChanged
        Try

            Dim objTAPolicy_AbsentRule As New TAPolicy_AbsentRule
            Dim dt As DataTable = AbsentRuledt
            AbsentRuleId = CInt(CType(dgrdAbsentRules.SelectedItems(0), GridDataItem).GetDataKeyValue("AbsentRuleId"))
            objTAPolicy_AbsentRule.AbsentRuleId = AbsentRuleId
            txtAbsentRuleEnglishName.Text = CType(dgrdAbsentRules.SelectedItems(0), GridDataItem).GetDataKeyValue("RuleName").ToString()
            txtAbsentRuleArabichName.Text = CType(dgrdAbsentRules.SelectedItems(0), GridDataItem).GetDataKeyValue("RuleArabicName").ToString()
            ddlAbsentRuleType.SelectedValue = CInt(CType(dgrdAbsentRules.SelectedItems(0), GridDataItem).GetDataKeyValue("AbsentRuleType"))
            PrepareAbsetRuleInterface()
            If CType(dgrdAbsentRules.SelectedItems(0), GridDataItem).GetDataKeyValue("Variable1").ToString() <> "" Then
                txtVar1.Text = CInt(CType(dgrdAbsentRules.SelectedItems(0), GridDataItem).GetDataKeyValue("Variable1"))
            End If
            If CType(dgrdAbsentRules.SelectedItems(0), GridDataItem).GetDataKeyValue("Variable2").ToString() <> "" Then
                txtVar2.Text = CInt(CType(dgrdAbsentRules.SelectedItems(0), GridDataItem).GetDataKeyValue("Variable2"))
            End If


        Catch ex As Exception
        End Try
    End Sub

    Protected Sub dgrdViolation_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdViolation.SelectedIndexChanged
        Try

            Dim objTAPolicy_Violation As New TAPolicy_Violation
            Dim dt As DataTable = Violationdt
            ViolationId = CInt(CType(dgrdViolation.SelectedItems(0), GridDataItem).GetDataKeyValue("ViolationId"))
            objTAPolicy_Violation.ViolationId = ViolationId
            txtViolationEn.Text = CType(dgrdViolation.SelectedItems(0), GridDataItem).GetDataKeyValue("ViolationName").ToString()
            txtViolationAr.Text = CType(dgrdViolation.SelectedItems(0), GridDataItem).GetDataKeyValue("ViolationArabicName").ToString()
            ddlViolationRuleType.SelectedValue = CInt(CType(dgrdViolation.SelectedItems(0), GridDataItem).GetDataKeyValue("ViolationRuleType"))
            PrepareViolationInterface()
            If CType(dgrdViolation.SelectedItems(0), GridDataItem).GetDataKeyValue("Variable1").ToString() <> "" Then
                txtViolationVar1.Text = CInt(CType(dgrdViolation.SelectedItems(0), GridDataItem).GetDataKeyValue("Variable1"))
            End If
            If CType(dgrdViolation.SelectedItems(0), GridDataItem).GetDataKeyValue("Variable2").ToString() <> "" Then
                ddlViolationVAr2.SelectedValue = CInt(CType(dgrdViolation.SelectedItems(0), GridDataItem).GetDataKeyValue("Variable2"))
            End If
            If CType(dgrdViolation.SelectedItems(0), GridDataItem).GetDataKeyValue("Variable3").ToString() <> "" Then
                txtViolationVar3.Text = CInt(CType(dgrdViolation.SelectedItems(0), GridDataItem).GetDataKeyValue("Variable3"))
            End If

            ddlViolationAction.SelectedValue = CInt(CType(dgrdViolation.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_ViolationActionId"))
            ddlViolationAction2.SelectedValue = CInt(CType(dgrdViolation.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_ViolationActionId2"))
            ddlViolationAction3.SelectedValue = CInt(CType(dgrdViolation.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_ViolationActionId3"))
            ddlViolationAction4.SelectedValue = CInt(CType(dgrdViolation.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_ViolationActionId4"))
            ddlViolationAction5.SelectedValue = CInt(CType(dgrdViolation.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_ViolationActionId5"))

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub dgrdViolation_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdViolation.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim item As GridDataItem
            item = e.Item

            'Dim RuleTypeID = DirectCast(DirectCast(DirectCast(DirectCast(DirectCast(e.Item.DataItem, System.Object), System.Data.DataRowView), System.Data.DataRowView).Row, System.Data.DataRow), System.Data.DataRow).ItemArray(4)
            Dim RuleTypeID As Integer = Convert.ToInt32(item.GetDataKeyValue("ViolationRuleType"))
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
#End Region

#Region "Methods"

    Private Sub fillPolicy(ByVal TAPolicyID As Integer)
        Dim objTAPolicy = New TAPolicy
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
            chkHasPrayTime.Checked = .HasPrayTime
            HasLaunchBreak()
            HasPrayTime()
            FillReasons()
            If chkHasLaunchBreak.Checked Then
                txtLaunchBreakDuration.Text = .LaunchBreakDuration
                chkCompensateLaunchbreak.Checked = .CompensatePrayTime
                ddlLaunchbreakReason.SelectedValue = .FK_LaunchBreakReason
            End If
            If chkHasPrayTime.Checked Then
                txtPrayTimeDuration.Text = .PrayTimeDuration
                chkCompensatePrayTime.Checked = .CompensatePrayTime
                ddlPrayTimeReason.SelectedValue = .FK_PrayTimeReason
            End If
            clearAbsentRule()
            fillAbsentRules()
            clearViolation()
            fillViolation()
        End With
    End Sub

    Sub PrepareViolationInterface()
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        txtViolationVar1.Text = ""
        ddlViolationVAr2.ClearSelection()
        Select Case ddlViolationRuleType.SelectedValue
            Case 1
                lblViolationVAr1.Text = ResourceManager.GetString("DelayInMin", CultureInfo)
                lblViolationVAr1.Visible = True
                lblViolationVAr2.Text = ResourceManager.GetString("TotalDelayInMin", CultureInfo)
                txtViolationVar1.Visible = True
                ddlViolationVAr2.Visible = True
                lblViolationVAr3.Text = ""
                txtViolationVar3.Visible = False

            Case 2
                lblViolationVAr1.Text = ResourceManager.GetString("DelayInDays", CultureInfo)
                lblViolationVAr1.Visible = True
                lblViolationVAr2.Text = ""
                txtViolationVar1.Visible = True
                ddlViolationVAr2.Visible = False
                lblViolationVAr3.Text = ""
                txtViolationVar3.Visible = False

            Case 3
                lblViolationVAr1.Text = ResourceManager.GetString("DelayInDays", CultureInfo)
                lblViolationVAr1.Visible = True
                lblViolationVAr2.Text = ResourceManager.GetString("ConsiderPeriodInDays", CultureInfo)
                txtViolationVar1.Visible = True
                ddlViolationVAr2.Visible = True
                lblViolationVAr3.Text = ""
                txtViolationVar3.Visible = False

                'lblViolationVAr1.Text = ""
                'reqViolationVar1.ErrorMessage = ""
                'lblViolationVAr2.Text = ResourceManager.GetString("TotalDelayInMin", CultureInfo)
                'txtViolationVar1.Visible = False
                'txtViolationVar1.Text = 0
                'ddlViolationVAr2.Visible = True

            Case 5
                lblViolationVAr1.Text = ResourceManager.GetString("NoAbsentDays", CultureInfo)
                lblViolationVAr1.Visible = True
                lblViolationVAr2.Text = ""
                txtViolationVar1.Visible = True
                ddlViolationVAr2.Visible = False
                lblViolationVAr3.Text = ""
                txtViolationVar3.Visible = False
            Case 6
                'lblViolationVAr1.Text = ResourceManager.GetString("NoAbsentDays", CultureInfo)
                'reqViolationVar1.ErrorMessage = ResourceManager.GetString("PleaseNoAbsentDays", CultureInfo)
                lblViolationVAr1.Visible = False
                lblViolationVAr1.Visible = False
                lblViolationVAr2.Text = ResourceManager.GetString("ConsiderPeriodInDays", CultureInfo)
                txtViolationVar1.Visible = False
                ddlViolationVAr2.Visible = True
                lblViolationVAr3.Text = ""
                txtViolationVar3.Visible = False
            Case 9
                lblViolationVAr1.Text = ResourceManager.GetString("EarlyOutInDays", CultureInfo)
                lblViolationVAr1.Visible = True
                lblViolationVAr2.Text = ResourceManager.GetString("ConsiderPeriodInDays", CultureInfo)
                txtViolationVar1.Visible = True
                ddlViolationVAr2.Visible = True
                lblViolationVAr3.Text = ""
                txtViolationVar3.Visible = False
            Case 10
                'lblViolationVAr1.Text = ResourceManager.GetString("NoAbsentDays", CultureInfo)
                'reqViolationVar1.ErrorMessage = ResourceManager.GetString("PleaseNoAbsentDays", CultureInfo)
                lblViolationVAr1.Visible = False
                lblViolationVAr2.Text = ResourceManager.GetString("ConsiderPeriodInDays", CultureInfo)
                txtViolationVar1.Visible = False
                ddlViolationVAr2.Visible = True
                lblViolationVAr3.Text = ""
                txtViolationVar3.Visible = False

            Case 11
                lblViolationVAr1.Text = ResourceManager.GetString("MoreThan", CultureInfo)
                lblViolationVAr1.Visible = True
                txtViolationVar1.Visible = True
                lblViolationVAr2.Text = ResourceManager.GetString("ConsiderPeriodInDays", CultureInfo)
                ddlViolationVAr2.Visible = True
                lblViolationVAr3.Text = ResourceManager.GetString("UpTo", CultureInfo)
                txtViolationVar3.Visible = True
            Case Else
                lblViolationVAr1.Text = ""
                lblViolationVAr2.Text = ""
                lblViolationVAr3.Text = ""
                txtViolationVar1.Visible = False
                ddlViolationVAr2.Visible = False
                txtViolationVar3.Visible = False
        End Select
    End Sub

    Sub PrepareAbsetRuleInterface()
        txtVar1.Text = ""
        txtVar2.Text = ""
        Select Case ddlAbsentRuleType.SelectedValue

            Case 1
                lblVariable1.Text = ResourceManager.GetString("DelayInMin", CultureInfo)
                lblVariable2.Text = ""
                txtVar1.Visible = True
                txtVar2.Visible = False

                '-------Old Values-------'
                'lblVariable1.Text = ResourceManager.GetString("DelayInMin", CultureInfo)
                'lblVariable2.Text = ResourceManager.GetString("TotalDelayInMin", CultureInfo)
                'txtVar1.Visible = True
                'txtVar2.Visible = True
                '-------Old Values-------'

            Case 2
                lblVariable1.Text = ResourceManager.GetString("EarlyOutInMin", CultureInfo)
                lblVariable2.Text = ""
                txtVar1.Visible = True
                txtVar2.Visible = False

                '-------Old Values-------'
                'lblVariable1.Text = ResourceManager.GetString("DelayInDays", CultureInfo)
                'lblVariable2.Text = ""
                'txtVar1.Visible = True
                'txtVar2.Visible = False
                '-------Old Values-------'
            Case 3

                lblVariable1.Text = ResourceManager.GetString("DelayandEarlyOutInMin", CultureInfo)
                lblVariable2.Text = ""
                txtVar1.Visible = True
                txtVar2.Visible = False
                '-------Old Values-------'
                'lblVariable1.Text = ResourceManager.GetString("DelayInDays", CultureInfo)
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

    Sub fillViolation()
        Dim objTAPolicy_Violation = New TAPolicy_Violation
        objTAPolicy_Violation.FK_TAPolicyId = TAPolicyID
        Violationdt = objTAPolicy_Violation.GetAllByPolicyId
        Violationdt.Columns("ViolationId").AutoIncrement = True
        Violationdt.Columns("ViolationId").AutoIncrementSeed = 1
        Violationdt.Columns("ViolationId").AutoIncrementStep = 1
        dgrdViolation.DataSource = Violationdt
        dgrdViolation.DataBind()
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

    Sub fillAbsentRules()
        If TAPolicyID > 0 Then

            Dim objTAPolicy_AbsentRule = New TAPolicy_AbsentRule
            objTAPolicy_AbsentRule.FK_TAPolicyId = TAPolicyID
            AbsentRuledt = objTAPolicy_AbsentRule.GetAllByTAPolicyId
            dgrdAbsentRules.DataSource = AbsentRuledt
            dgrdAbsentRules.DataBind()

        End If
    End Sub

    Sub SetReadOnly()
        txtAbsentRuleArabichName.ReadOnly = True
        txtAbsentRuleEnglishName.ReadOnly = True
        txtGraceIn.ReadOnly = True
        txtGraceOut.ReadOnly = True
        txtPolicyArabic.ReadOnly = True
        txtPolicyEnglish.ReadOnly = True
        txtVar1.ReadOnly = True
        txtVar2.ReadOnly = True

        txtViolationAr.ReadOnly = True
        txtViolationEn.ReadOnly = True
        txtViolationVar1.ReadOnly = True
        txtViolationVar3.ReadOnly = True
        'txtViolationVar2.ReadOnly = True
        ddlViolationVAr2.Enabled = False

        chkHasLaunchBreak.Enabled = False
        txtLaunchBreakDuration.Enabled = False
        chkCompensateLaunchbreak.Enabled = False
        ddlLaunchbreakReason.Enabled = False
        chkHasPrayTime.Enabled = False
        txtPrayTimeDuration.Enabled = False
        chkCompensatePrayTime.Enabled = False
        ddlPrayTimeReason.Enabled = False

        ddlAbsentRuleType.Enabled = False
        ddlViolationAction.Enabled = False
        ddlViolationAction2.Enabled = False
        ddlViolationAction3.Enabled = False
        ddlViolationAction4.Enabled = False
        ddlViolationAction5.Enabled = False

        ddlViolationRuleType.Enabled = False

        'chkActive.Enabled = False
        chkDelalIsFromGrace.Enabled = False
        chkEarlyOutisFromGrace.Enabled = False



    End Sub

    Sub HasLaunchBreak()
        If chkHasLaunchBreak.Checked Then
            trLaunchBreak.Visible = True
            trLaunchbreakReason.Visible = True
            trCompensateLaunchbreak.Visible = True
        Else
            trLaunchBreak.Visible = False
            trLaunchbreakReason.Visible = False
            trCompensateLaunchbreak.Visible = False
        End If
    End Sub

    Sub HasPrayTime()
        If chkHasPrayTime.Checked Then
            trPrayTime.Visible = True
            trPrayTimeReason.Visible = True
            trCompensatePrayTime.Visible = True
        Else
            trPrayTime.Visible = False
            trPrayTimeReason.Visible = False
            trCompensatePrayTime.Visible = False
        End If
    End Sub

    Private Sub FillReasons()
        objTA_Reason = New TA_Reason()
        CtlCommon.FillTelerikDropDownList(ddlLaunchbreakReason, objTA_Reason.GetAll, Lang)
        CtlCommon.FillTelerikDropDownList(ddlPrayTimeReason, objTA_Reason.GetAll, Lang)
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

#End Region

End Class
