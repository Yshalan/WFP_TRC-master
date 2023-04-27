Imports System.Data
Imports SmartV.UTILITIES
Imports TA.DashBoard

Partial Class DashBoard_Dashboard_StatusCount
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objDashBoard As DashBoard
    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private objversion As SmartV.Version.version
    Public chartLang As String

    Dim StatusCountdata As String = ""
    Dim RequestStatusCountdata As String = ""
    Dim EmployeeWorkingHoursCountdata As String = ""
    Dim WorkingDaysData As String = ""

#End Region

#Region "Page Events"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        Else
            If SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                Page.MasterPageFile = "~/default/ArabicMaster.master"
                chartLang = "ar"
            Else
                Lang = CtlCommon.Lang.EN
                Page.MasterPageFile = "~/default/NewMaster.master"
                chartLang = "en"
            End If
            Page.UICulture = SessionVariables.CultureInfo
        End If

    End Sub

    Private Sub DashBoard_Dashboard_StatusCount_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            UserSecurityFilter1.IsEntityPostBack = False
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                chartLang = "ar"
            Else
                Lang = CtlCommon.Lang.EN
                chartLang = "en"
            End If


            rdpFromDate.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd As New Date
            dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)

            rdpToDate.SelectedDate = dd
            LoadCharts()

            pageheader1.HeaderText = ResourceManager.GetString("StatusSummaryDashboard", CultureInfo)

        End If


    End Sub

    Private Sub btnRetrive_Click(sender As Object, e As EventArgs) Handles btnRetrive.Click
        LoadCharts()
    End Sub

#End Region

#Region "Methods"

    Private Sub LoadCharts()
        FillStatusCountDash()
        FillRequestStatusCountDash()
        FillEmployeesWorkingHoursDash()
        FillWorkingDaysDash()
    End Sub

    Private Sub FillStatusCountDash()
        objDashBoard = New DashBoard
        Dim dt As DataTable
        With objDashBoard

            If objversion.HasMultiCompany Then
                UserSecurityFilter1.GetEntity()
            End If
            If UserSecurityFilter1.CompanyId > 0 Then
                objDashBoard.FK_CompanyId = UserSecurityFilter1.CompanyId
            Else
                objDashBoard.FK_CompanyId = Nothing
            End If
            If UserSecurityFilter1.EntityId > 0 Then
                objDashBoard.FK_EntityId = UserSecurityFilter1.EntityId
            Else
                objDashBoard.FK_EntityId = Nothing
            End If
            If rdpFromDate.SelectedDate.HasValue Then
                objDashBoard.FromDate = rdpFromDate.SelectedDate
            Else
                rdpFromDate.SelectedDate = Date.Today
            End If

            If rdpToDate.SelectedDate.HasValue Then
                objDashBoard.ToDate = rdpToDate.SelectedDate
            Else
                rdpToDate.SelectedDate = Date.Today
            End If

            dt = .GetStatusCountDash

            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    'lblChkInVal.Text = dt.Rows(0)("StatusCount").ToString
                    'lblChkOutVal.Text = dt.Rows(1)("StatusCount").ToString
                    'lblAbsentVal.Text = dt.Rows(2)("StatusCount").ToString
                    'lblLeaveVal.Text = dt.Rows(3)("StatusCount").ToString
                    'lblPermissionVal.Text = dt.Rows(4)("StatusCount").ToString
                    'lblNursingPermissionVal.Text = dt.Rows(5)("StatusCount").ToString
                    'lblStudyPermissionVal.Text = dt.Rows(6)("StatusCount").ToString
                    'lblDelayVal.Text = dt.Rows(7)("StatusCount").ToString
                    'lblEarlyOutVal.Text = dt.Rows(8)("StatusCount").ToString

                    For i As Integer = 0 To dt.Rows.Count - 1
                        StatusCountdata += "{name:   '"
                        StatusCountdata += IIf(Lang = CtlCommon.Lang.AR, dt.Rows(i)("StatusTextAr").ToString, dt.Rows(i)("StatusTextEn").ToString)
                        StatusCountdata += "',  y: "
                        StatusCountdata += dt.Rows(i)("StatusCount").ToString
                        StatusCountdata += "},"
                    Next
                End If
            End If

        End With

        JSONStatusCountSeries.Text = "<script type='text/javascript' charset='utf-8'>var Var_StatusCountSeries = [{name:   'Percentage',colorByPoint: true,data: [" & StatusCountdata & "]  }] </Script>"
        JSONStatusCountChartTitleText.Text = "<script type='text/javascript' charset='utf-8'>var Var_StatusCountChartTitleText = '" & IIf(Lang = CtlCommon.Lang.AR, "حالة حركات الموظفين", "Employees Transaction Status") & "'</Script>"

    End Sub

    Private Sub FillRequestStatusCountDash()
        objDashBoard = New DashBoard
        Dim dt As DataTable
        With objDashBoard

            If objversion.HasMultiCompany Then
                UserSecurityFilter1.GetEntity()
            End If
            If UserSecurityFilter1.CompanyId > 0 Then
                objDashBoard.FK_CompanyId = UserSecurityFilter1.CompanyId
            Else
                objDashBoard.FK_CompanyId = Nothing
            End If
            If UserSecurityFilter1.EntityId > 0 Then
                objDashBoard.FK_EntityId = UserSecurityFilter1.EntityId
            Else
                objDashBoard.FK_EntityId = Nothing
            End If
            If rdpFromDate.SelectedDate.HasValue Then
                objDashBoard.FromDate = rdpFromDate.SelectedDate
            Else
                rdpFromDate.SelectedDate = Date.Today
            End If

            If rdpToDate.SelectedDate.HasValue Then
                objDashBoard.ToDate = rdpToDate.SelectedDate
            Else
                rdpToDate.SelectedDate = Date.Today
            End If

            dt = .GetRequestStatusCountDash

            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then

                    'lblLeaveRequestval.Text = dt.Rows(0)("StatusCount").ToString
                    'lblPermissionRequestVal.Text = dt.Rows(1)("StatusCount").ToString
                    'lblNursingPermissionRequestVal.Text = dt.Rows(2)("StatusCount").ToString
                    'lblStudyPermissionRequestVal.Text = dt.Rows(3)("StatusCount").ToString
                    'lblManualEntryRequestVal.Text = dt.Rows(4)("StatusCount").ToString
                    'lblUpdateTransactionRequestVal.Text = dt.Rows(5)("StatusCount").ToString


                    For i As Integer = 0 To dt.Rows.Count - 1
                        RequestStatusCountdata += "{name:   '"
                        RequestStatusCountdata += IIf(Lang = CtlCommon.Lang.AR, dt.Rows(i)("StatusTextAr").ToString, dt.Rows(i)("StatusTextEn").ToString)
                        RequestStatusCountdata += "',  y: "
                        RequestStatusCountdata += dt.Rows(i)("StatusCount").ToString
                        RequestStatusCountdata += "},"
                    Next
                End If
            End If

        End With

        JSONRequestStatusCountSeries.Text = "<script type='text/javascript' charset='utf-8'>var Var_RequestStatusCountSeries = [{name:   'Percentage',colorByPoint: true,data: [" & RequestStatusCountdata & "]  }] </Script>"
        JSONRequestStatusCountChartTitleText.Text = "<script type='text/javascript' charset='utf-8'>var Var_RequestStatusCountChartTitleText = '" & IIf(Lang = CtlCommon.Lang.AR, "طلبات الموظفين", "Employees Requests") & "'</Script>"

    End Sub

    Private Sub FillEmployeesWorkingHoursDash()
        objDashBoard = New DashBoard
        Dim dt As DataTable
        With objDashBoard

            If objversion.HasMultiCompany Then
                UserSecurityFilter1.GetEntity()
            End If
            If UserSecurityFilter1.CompanyId > 0 Then
                objDashBoard.FK_CompanyId = UserSecurityFilter1.CompanyId
            Else
                objDashBoard.FK_CompanyId = Nothing
            End If
            If UserSecurityFilter1.EntityId > 0 Then
                objDashBoard.FK_EntityId = UserSecurityFilter1.EntityId
            Else
                objDashBoard.FK_EntityId = Nothing
            End If
            If rdpFromDate.SelectedDate.HasValue Then
                objDashBoard.FromDate = rdpFromDate.SelectedDate
            Else
                rdpFromDate.SelectedDate = Date.Today
            End If

            If rdpToDate.SelectedDate.HasValue Then
                objDashBoard.ToDate = rdpToDate.SelectedDate
            Else
                rdpToDate.SelectedDate = Date.Today
            End If

            dt = .GetEmployeeWorkingHoursDash

            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1

                        EmployeeWorkingHoursCountdata += "{"
                        EmployeeWorkingHoursCountdata += "name:"
                        EmployeeWorkingHoursCountdata += "'" + dt.Rows(i)("WorkPeriod") + "'" + ", data:["

                        Dim countj As Integer = 0
                        For j As Integer = 0 To dt.Rows.Count - 1
                            If dt.Rows(j)("WorkPeriod") = dt.Rows(i)("WorkPeriod") Then
                                EmployeeWorkingHoursCountdata += dt.Rows(j)("EmployeesCount").ToString + ","
                            End If
                        Next

                        EmployeeWorkingHoursCountdata += "]},"
                    Next
                End If
            End If

        End With

        JSONEmployeeWorkingHoursCountSeries.Text = "<script type='text/javascript' charset='utf-8'>var Var_EmployeeWorkingHoursCountSeries = [" & EmployeeWorkingHoursCountdata & "] </Script>"
        JSONEmployeeWorkingHoursCountChartTitleText.Text = "<script type='text/javascript' charset='utf-8'>var Var_EmployeeWorkingHoursCountChartTitleText = '" & IIf(Lang = CtlCommon.Lang.AR, "ساعات عمل الموظفين", "Employees Working Hours") & "'</Script>"
        JSONEmployeeWorkingHoursCountChartSubTitleText.Text = "<script type='text/javascript' charset='utf-8'>var Var_EmployeeWorkingHoursCountChartSubTitleText = '" & IIf(Lang = CtlCommon.Lang.AR, "ساعات العمل", "Work Hours") & "'</Script>"
        Dim dateFromDate As String
        Dim dateToDate As String

        dateFromDate = Convert.ToDateTime(rdpFromDate.SelectedDate).ToString("dd/MM/yyyy")
        dateToDate = Convert.ToDateTime(rdpToDate.SelectedDate).ToString("dd/MM/yyyy")

        JSONEmployeeWorkingHoursCountChartDateText.Text = "<script type='text/javascript' charset='utf-8'>var Var_EmployeeWorkingHoursCountChartDateText = '" & dateFromDate & "-" & dateToDate & "'</Script>"

    End Sub

    Private Sub FillWorkingDaysDash()

        objDashBoard = New DashBoard
        Dim dt As DataTable
        With objDashBoard
            If objversion.HasMultiCompany Then
                UserSecurityFilter1.GetEntity()
            End If
            If UserSecurityFilter1.CompanyId > 0 Then
                objDashBoard.FK_CompanyId = UserSecurityFilter1.CompanyId
            Else
                objDashBoard.FK_CompanyId = Nothing
            End If
            If UserSecurityFilter1.EntityId > 0 Then
                objDashBoard.FK_EntityId = UserSecurityFilter1.EntityId
            Else
                objDashBoard.FK_EntityId = Nothing
            End If
            If rdpFromDate.SelectedDate.HasValue Then
                objDashBoard.FromDate = rdpFromDate.SelectedDate
            Else
                rdpFromDate.SelectedDate = Date.Today
            End If

            If rdpToDate.SelectedDate.HasValue Then
                objDashBoard.ToDate = rdpToDate.SelectedDate
            Else
                rdpToDate.SelectedDate = Date.Today
            End If

            dt = .GetTransaction_StatsDash

        End With

        If Not dt Is Nothing Then
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1

                    WorkingDaysData += "{"
                    WorkingDaysData += "name:"
                    WorkingDaysData += "'" + IIf(Lang = CtlCommon.Lang.AR, dt.Rows(i)("StatusAr"), dt.Rows(i)("StatusEn")) + "'" + ", data:["

                    Dim countj As Integer = 0
                    For j As Integer = 0 To dt.Rows.Count - 1
                        If IIf(Lang = CtlCommon.Lang.AR, dt.Rows(j)("StatusAr"), dt.Rows(j)("StatusEn")) = IIf(Lang = CtlCommon.Lang.AR, dt.Rows(i)("StatusAr"), dt.Rows(i)("StatusEn")) Then
                            WorkingDaysData += dt.Rows(j)("EmployeeCount").ToString + ","
                            countj += 1
                        End If
                    Next

                    WorkingDaysData += "]},"
                    i += countj
                Next
            End If
        End If

        JSONTransactionStatsSeries.Text = "<script type='text/javascript' charset='utf-8'>var Var_TransactionStatsSeries =[" & WorkingDaysData & "]</Script>"
        JSONTransactionStatsChartTitleText.Text = "<script type='text/javascript' charset='utf-8'>var Var_TransactionStatsChartTitleText ='" & IIf(Lang = CtlCommon.Lang.AR, "احصائية الحضور", "Attendance Statistics") & "'</Script>"
        JSONTransactionStatsChartSubTitleText.Text = "<script type='text/javascript' charset='utf-8'>var Var_TransactionStatsChartSubTitleText ='" & IIf(Lang = CtlCommon.Lang.AR, "العدد", "Number") & "'</Script>"


        JSONminMonth.Text = "<script type='text/javascript' charset='utf-8'>var Var_JSONminMonth =[" & Month(DateAdd(DateInterval.Month, -1, rdpFromDate.DbSelectedDate)) & "]</Script>"
        JSONminYear.Text = "<script type='text/javascript' charset='utf-8'>var Var_JSONminYear =[" & Year(DateAdd(DateInterval.Month, -1, rdpFromDate.DbSelectedDate)) & "]</Script>"
        JSONminDay.Text = "<script type='text/javascript' charset='utf-8'>var Var_JSONminDay =[" & Day(rdpFromDate.DbSelectedDate) & "]</Script>"



        JSONmaxMonth.Text = "<script type='text/javascript' charset='utf-8'>var Var_JSONmaxMonth =[" & Month(DateAdd(DateInterval.Month, -1, rdpToDate.DbSelectedDate)) & "]</Script>"
        JSONmaxYear.Text = "<script type='text/javascript' charset='utf-8'>var Var_JSONmaxYear =[" & Year(DateAdd(DateInterval.Month, -1, rdpToDate.DbSelectedDate)) & "]</Script>"
        JSONmaxDay.Text = "<script type='text/javascript' charset='utf-8'>var Var_JSONmaxDay =[" & Day(rdpToDate.DbSelectedDate) & "]</Script>"

    End Sub
#End Region

End Class
