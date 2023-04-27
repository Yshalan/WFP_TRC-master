Imports SmartV.UTILITIES
Imports TA.Definitions
Imports TA.SelfServices
Imports System.Data
Imports TA.Employees
Imports TA.DashBoard
Imports System.Web.UI.DataVisualization.Charting
Imports TA.Admin

Partial Class SelfServices_UserControls_SummaryPage
    Inherits System.Web.UI.UserControl

#Region "Class Variables"

    Private objWorkSchedule As WorkSchedule
    Dim CultureInfo As System.Globalization.CultureInfo
    Private objWorkSchedule_NormalTime As WorkSchedule_NormalTime
    Private objEmployeeViolations As EmployeeViolations
    Private objWorkSchedule_Shifts As WorkSchedule_Shifts
    Private objEmp_WorkSchedule As Emp_WorkSchedule
    Public Lang As String
    Public dawte As String
    Private objDashBoard As DashBoard
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Private objAPP_Settings As APP_Settings

#End Region

#Region "Public Properties"

    Public Property EmployeeId() As Integer
        Get
            Return ViewState("EmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeId") = value
        End Set
    End Property

    Public Property FromDate() As Date
        Get
            Return ViewState("FromDate")
        End Get
        Set(ByVal value As Date)
            ViewState("FromDate") = value
        End Set
    End Property

    Public Property ToDate() As Date
        Get
            Return ViewState("ToDate")
        End Get
        Set(ByVal value As Date)
            ViewState("ToDate") = value
        End Set
    End Property

    Private Property EmpWorkScheduleId() As Integer
        Get
            Return ViewState("EmpWorkScheduleId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmpWorkScheduleId") = value
        End Set
    End Property

    Private Property ScheduleType() As Integer
        Get
            Return ViewState("ScheduleType")
        End Get
        Set(ByVal value As Integer)
            ViewState("ScheduleType") = value
        End Set
    End Property

    Private Property ScheduleId() As Integer
        Get
            Return ViewState("ScheduleId")
        End Get
        Set(ByVal value As Integer)
            ViewState("ScheduleId") = value
        End Set
    End Property

    Public Property VisibleInforCount() As Integer
        Get
            Return ViewState("VisibleInforCount")
        End Get
        Set(ByVal value As Integer)
            ViewState("VisibleInforCount") = value
        End Set
    End Property
#End Region

#Region "Page Events"

    Protected Sub SelfServices_UserControls_SummaryPage_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If SessionVariables.CultureInfo = "en-US" Then
            Lang = "en"
        ElseIf SessionVariables.CultureInfo = "ar-JO" Then
            Lang = "ar"
        End If

        If Not Page.IsPostBack Then

            FromDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd As New Date
            dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            ToDate = dd
            FillInfo()
            'FillChart()
        End If
    End Sub

#End Region

#Region "Methods"
    
    Public Sub FillInfo()
        EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
        If Not EmployeeId = Nothing Then
            'objWorkSchedule = New WorkSchedule

            'objWorkSchedule.EmployeeId = EmployeeId
            'Dim frmDate As Date = Date.Now
            'objWorkSchedule.GetActive_SchedulebyEmpId_row(frmDate)
            'ScheduleType = objWorkSchedule.ScheduleType
            'If Not ScheduleType = Nothing Then
            '    If (Lang = "ar") Then
            '        lblScheduleValue.Text = objWorkSchedule.ScheduleArabicName
            '    Else
            '        lblScheduleValue.Text = objWorkSchedule.ScheduleName
            '    End If

            '    If (Lang = "ar") Then
            '        If ScheduleType = 1 Then
            '            lblScheduleTypeValue.Text = "جدول عمل عادي"
            '        ElseIf ScheduleType = 2 Then
            '            lblScheduleTypeValue.Text = "جدول عمل مرن"
            '        ElseIf ScheduleType = 3 Then
            '            lblScheduleTypeValue.Text = "جدول عمل المناوبات"
            '        ElseIf ScheduleType = 4 Then
            '            lblScheduleTypeValue.Text = "جدول مناوبات المجموعات"
            '        End If

            '    Else
            '        If ScheduleType = 1 Then
            '            lblScheduleTypeValue.Text = "Normal Schedule"
            '        ElseIf ScheduleType = 2 Then
            '            lblScheduleTypeValue.Text = "Flexible Schedule"
            '        ElseIf ScheduleType = 3 Then
            '            lblScheduleTypeValue.Text = "Advanced Schedule"
            '        ElseIf ScheduleType = 4 Then
            '            lblScheduleTypeValue.Text = "Group Advanced Schedule"
            '        End If
            '    End If
            '    ScheduleId = objWorkSchedule.ScheduleId


            objEmployeeViolations = New EmployeeViolations

            With objEmployeeViolations
                .FK_EmployeeId = EmployeeId
                .FromDate = FromDate
                .ToDate = ToDate
                .GetEmpSummary()
                If Not .strDelay = Nothing Then
                    lblDelayValue.Text = .strDelay.ToString
                End If
                If Not .strEarly_Out = Nothing Then
                    lblEarlyOutValue.Text = .strEarly_Out.ToString
                End If
                If Not .LostTime = Nothing Then
                    lblLostTimeValue.Text = .LostTime.ToString
                End If
                lblAbsentValue.Text = .Absent

                If Not .RemainingPermissionBalance = Nothing Then
                    lblRemainingPermissionBalanceValue.Text = .RemainingPermissionBalance.ToString
                End If

                lblMissingInValue.Text = .MissingIn
                lblMissingOutValue.Text = .MissingOut
                lblNotCompletionHalfDayValue.Text = .NotCompletionHalfDay
                lblDelayAndEarlyOutValue.Text = .strDelay_Early_Out
                lblRemainingTimesPermissionValue.Text = .RemainingTimesPersonalPermission
                'lblRemainingYearlyLeaveBalance.Text = lblRemainingYearlyLeaveBalance.Text & " " & IIf(Lang = CtlCommon.Lang.AR,,)
                lblRemainingYearlyLeaveBalanceValue.Text = .strRemainingYearlyLeaveBalance
                VisibleInfo()


            End With
        End If
    End Sub

    'Public Sub FillChart()
    '    'FillInfo()
    '    If SessionVariables.CultureInfo = "en-US" Then
    '        Lang = "en"
    '    ElseIf SessionVariables.CultureInfo = "ar-JO" Then
    '        Lang = "ar"
    '    End If
    '    Dim objDashBoard As New DashBoard
    '    Dim dtCAChart As DataTable

    '    With objDashBoard
    '        .Lang = Lang
    '        .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
    '        .FromDate = FromDate
    '        .ToDate = ToDate
    '        dtCAChart = .GetEmployeeSummaryDash
    '    End With

    '    fill_chart(dtCAChart, "descrip", "number", "")
    'End Sub

    'Public Sub fill_chart(ByVal dtCAChart As DataTable, ByVal xmember As String, ByVal ymemeber As String, ByVal Title As String)

    '    Try

    '        chrtbie.Series("Default").ToolTip = "#PERCENT"
    '        chrtbie.Series("Default").LegendToolTip = "#LEGENDTEXT"
    '        chrtbie.Series("Default").PostBackValue = "#INDEX"
    '        chrtbie.Series("Default").LegendPostBackValue = "#INDEX"


    '        chrtbie.DataSource = dtCAChart
    '        chrtbie.Series("Default").XValueMember = xmember
    '        chrtbie.Series("Default").YValueMembers = ymemeber
    '        chrtbie.DataBind()
    '        ' Set Doughnut chart type
    '        chrtbie.Series("Default").ChartType = SeriesChartType.Pie
    '        ' Set labels style
    '        chrtbie.Series("Default")("PieLabelStyle") = "Disabled"
    '        ' Set Doughnut radius percentage
    '        chrtbie.Series("Default")("DoughnutRadius") = "30"
    '        ' Explode data point 
    '        chrtbie.Series("Default").Points(0)("Exploded") = "True"
    '        ' Enable 3D
    '        chrtbie.ChartAreas("ChartArea1").Area3DStyle.Enable3D = True
    '        ' Disable the Legend
    '        chrtbie.Palette = ChartColorPalette.BrightPastel
    '        chrtbie.Legends(0).Enabled = True
    '        chrtbie.Titles.Add(Title)

    '    Catch ex As Exception

    '    End Try
    'End Sub
    Private Sub VisibleInfo()
        Dim RequestGridToAppear As String = ""
        objAPP_Settings = New APP_Settings
        With objAPP_Settings
            .GetByPK()
            VisibleInforCount = 0
            For Each i As String In .DurationTotalsToAppear.Split(",")

                If i = "5" Then
                    lblDelay.Visible = True
                    lblDelayValue.Visible = True
                    VisibleInforCount = VisibleInforCount + 1
                ElseIf i = "6" Then
                    lblEarlyOut.Visible = True
                    lblEarlyOutValue.Visible = True
                    VisibleInforCount = VisibleInforCount + 1
                ElseIf i = "7" Then
                    lblDelayAndEarlyOut.Visible = True
                    lblDelayAndEarlyOutValue.Visible = True
                    lblDelayAndEarlyOut.Text = IIf(Lang = "ar", "مجموع التأخير و الخروج المبكر :", "Total Delay and Early Out :")
                    VisibleInforCount = VisibleInforCount + 1
                ElseIf i = "8" Then
                    lblLostTime.Visible = True
                    lblLostTimeValue.Visible = True
                    VisibleInforCount = VisibleInforCount + 1
                ElseIf i = "9" Then
                    lblAbsent.Visible = True
                    lblAbsentValue.Visible = True
                    VisibleInforCount = VisibleInforCount + 1
                ElseIf i = "10" Then
                    lblRemainingPermissionBalance.Visible = True
                    lblRemainingPermissionBalanceValue.Visible = True
                    VisibleInforCount = VisibleInforCount + 1
                ElseIf i = "11" Then
                    lblMissingIn.Visible = True
                    lblMissingInValue.Visible = True
                    VisibleInforCount = VisibleInforCount + 1
                ElseIf i = "12" Then
                    lblMissingOut.Visible = True
                    lblMissingOutValue.Visible = True
                    VisibleInforCount = VisibleInforCount + 1
                ElseIf i = "13" Then
                    lblNotCompletionHalfDay.Visible = True
                    lblNotCompletionHalfDayValue.Visible = True
                    VisibleInforCount = VisibleInforCount + 1
                ElseIf i = "14" Then

                ElseIf i = "15" Then
                    lblRemainingTimesPermission.Visible = True
                    lblRemainingTimesPermissionValue.Visible = True
                    VisibleInforCount = VisibleInforCount + 1

                ElseIf i = "18" Then
                    lblRemainingYearlyLeaveBalance.Visible = True
                    lblRemainingYearlyLeaveBalanceValue.Visible = True
                    VisibleInforCount = VisibleInforCount + 1
                End If
            Next
        End With
    End Sub
#End Region

End Class
