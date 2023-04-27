Imports System.Data
Imports TA.Lookup
Imports SmartV.DB
Imports SmartV.UTILITIES
Imports TA.Employees
Imports System.Web.UI.WebControls
Imports Telerik.Web.UI
Imports TA.Admin

Partial Class Admin_OvertimeRulesPopUp
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objOvertimeRules As OvertimeRules
    Private Lang As CtlCommon.Lang
    Protected dir, textalign As String
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo

#End Region

#Region "Properties"

    Public Property OvertimeRuleId() As Integer
        Get
            Return ViewState("OvertimeRuleId")
        End Get
        Set(ByVal value As Integer)
            ViewState("OvertimeRuleId") = value
        End Set
    End Property

#End Region

#Region "Page events"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If (SessionVariables.CultureInfo Is Nothing) Then
            'Response.Redirect("~/default/Login.aspx")
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
                Page.Title = "Overtime Rule Details"
            Else
                SessionVariables.CultureInfo = "ar-JO"
                dir = "rtl"
                textalign = "right"
                Page.Title = "تفاصيل قاعدة العمل الاضافي"
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                ' Response.Redirect("~/default/Login.aspx")

            ElseIf SessionVariables.CultureInfo = "ar-JO" Then

                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            If Request.QueryString("RuleId") <> "" Then
                OvertimeRuleId = Convert.ToInt32(Request.QueryString("RuleId"))
                FillData()
            End If
        End If
    End Sub

#End Region

#Region "Methods"

    Sub FillData()

        objOvertimeRules = New OvertimeRules
        With objOvertimeRules
            .OvertimeRuleId = OvertimeRuleId
            .GetByPK()
            TxtRuleName.Text = .RuleName
            txtRuleArName.Text = .RuleArabicName
            If .OvertimeEligibility Then
                rdbOTEligibility.SelectedValue = 1
                dvOtEligibility.Visible = True
            Else
                rdbOTEligibility.SelectedValue = 0
                dvOtEligibility.Visible = False
            End If
            If .ApprovalRequired Then rdbApprovalReqd.SelectedValue = 1 Else rdbApprovalReqd.SelectedValue = 0
            If .HolidayIsHigh Then rdbConsiderHoliday.SelectedValue = 1 Else rdbConsiderHoliday.SelectedValue = 0
            If .IsCompensateLatetime Then rdbOtCompLateTime.SelectedValue = 1 Else rdbOtCompLateTime.SelectedValue = 0
            If .OffDayIsHigh Then rdbconsiderOffDay.SelectedValue = 1 Else rdbconsiderOffDay.SelectedValue = 0
            If .HighHasTime Then rdbHighHasTime.SelectedValue = 1 Else rdbHighHasTime.SelectedValue = 0
            TxtMinOvertime.Text = .MinOvertime
            If .IsLeaveBalance Then
                rdbOTLeaveOrFinance.SelectedValue = 1
                TxtHighRate.Text = .HighDayLeaveEquivalent
                TxtLowRate.Text = .LowDayLeaveEquivalent
            Else
                rdbOTLeaveOrFinance.SelectedValue = 0
                TxtHighRate.Text = .HighRate
                TxtLowRate.Text = .LowRate
            End If
            If .HighHasTime Then
                Tab2.Visible = True
                dvHasTime.Visible = True
                dgrdHighTime.DataSource = .GetAll_OvertimeRules_HighTime()
                dgrdHighTime.DataBind()
            Else
                Tab2.Visible = False
            End If
        End With

    End Sub

#End Region

End Class

