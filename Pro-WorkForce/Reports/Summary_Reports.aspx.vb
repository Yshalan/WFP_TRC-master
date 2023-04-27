Imports System.Data
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports SmartV.UTILITIES
Imports TA.Employees
Imports TA.Admin
Imports SmartV.UTILITIES.CtlCommon
Imports TA.Reports
Imports TA.Security
Imports System.Resources
Imports System.IO
Imports Telerik.Web.UI
Imports TA.ScheduleGroups
Imports System.Drawing

Partial Class Reports_SelfServices_Summary_Reports
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objAPP_Settings As APP_Settings
    Private Lang As CtlCommon.Lang
    Protected dir As String
    Private objVersion As SmartV.Version.version
    Private objsysforms As New SYSForms
    Private objOrgCompany As OrgCompany
    Dim CultureInfo As System.Globalization.CultureInfo
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim cryRpt As New ReportDocument
    Dim objSmartSecurity As New SmartSecurity
    Private objRequestStatus As New TA.LookUp.RequestStatus
    Private objSYSUsers As SYSUsers
    Private objEmp_Grade As Emp_Grade

    Private SummaryReport As String = "Summary Report"
    Private SummaryReportWithExpected As String = "Summary Report With Expected"
    Private UtilizationSummary As String = "Utilization Summary Report"
    Private EmployeeAbsentSummary As String = "Employee Absent Summary"
    Private EmployeeLeavesSummary As String = "Employee Leaves Summary"
    Private ManualEntrySummary As String = "Manual Entry Summary"
    Private DeductionReport As String = "Deduction Report"
    Private SummaryReport2 As String = "Summary Report 2"
    Private EfficiencyRate As String = "Efficiency Rate"
    Private TransPercentageReport As String = "Trans Percentage Report"
    Private WorkHoursDaysReport As String = "Work Hours/Days Report"
    Private HeadCountReport As String = "Head Count Report"
    Private EffectiveWorkingHours As String = "Effective Working hours"
    Private ApproveViolationsSummaryReport As String = "Approve Violations Summary Report"
    Private Statistical_Movements As String = "Statistical Movements"
    Private EmployeeDisciplineReport As String = "EmployeeDisciplineReport"
    Private GradeDeductionReport As String = "GradeDeduction Report"
    Private SummaryReport_WithInvalid As String = "SummaryReport_WithInvalid"
    Private WorkHours_ParamReport As String = "WorkHours_ParamReport"
    Private SummaryReport_Param As String = "SummaryReport_Param"
    Private Detailed_Work_Hrs As String = "Detailed_Work_Hrs"
    Private SummaryWorkDays As String = "SummaryWorkDays"
    Private SummaryDelayEarlyOut As String = "SummaryDelayEarlyOut"
    Private Summary_ByMonth As String = "Summary_ByMonth"
    Private DeductionPerPolicy As String = "DeductionPerPolicy"
    Private DetailedDeductionPerPolicy As String = "DetailedDeductionPerPolicy"
    Private LeavesSummaryReports As String = "LeavesSummaryReports"

#End Region

#Region "Properties"

    Public Property DT() As DataTable
        Get
            Return ViewState("DT")
        End Get
        Set(ByVal value As DataTable)
            ViewState("DT") = value
        End Set
    End Property

    Public Property NDT() As DataTable
        Get
            Return ViewState("NDT")
        End Get
        Set(ByVal value As DataTable)
            ViewState("NDT") = value
        End Set
    End Property

    Public Property HeadDT() As DataTable
        Get
            Return ViewState("HeadDT")
        End Get
        Set(ByVal value As DataTable)
            ViewState("HeadDT") = value
        End Set
    End Property

    Public Property dt2() As DataTable
        Get
            Return ViewState("dt2")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dt2") = value
        End Set
    End Property

    Public Property RPTName() As String
        Get
            Return ViewState("RPTName")
        End Get
        Set(ByVal value As String)
            ViewState("RPTName") = value
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

    Public Property UserName() As String
        Get
            Return ViewState("UserName")
        End Get
        Set(ByVal value As String)
            ViewState("UserName") = value
        End Set
    End Property

    Public Property Version() As String
        Get
            Return ViewState("Version")
        End Get
        Set(ByVal value As String)
            ViewState("Version") = value
        End Set
    End Property

    Public Property EmployeeId() As Integer
        Get
            Return ViewState("EmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeId") = value
        End Set
    End Property

    Public Property IS_EXIST() As Boolean
        Get
            Return ViewState("ISEXIST")
        End Get
        Set(ByVal value As Boolean)
            ViewState("ISEXIST") = value
        End Set
    End Property

    Public Property CopyRightEn() As String
        Get
            Return ViewState("CopyRightEn")
        End Get
        Set(ByVal value As String)
            ViewState("CopyRightEn") = value
        End Set
    End Property

    Public Property CopyRightAr() As String
        Get
            Return ViewState("CopyRightAr")
        End Get
        Set(ByVal value As String)
            ViewState("CopyRightAr") = value
        End Set
    End Property

    Public Property formID() As Integer
        Get
            Return ViewState("formID")
        End Get
        Set(ByVal value As Integer)
            ViewState("formID") = value
        End Set
    End Property

    Public Shared ReadOnly Property BaseSiteUrl() As String
        Get
            Dim context As HttpContext = HttpContext.Current
            Dim baseUrl As String = context.Request.Url.Authority + context.Request.ApplicationPath.TrimEnd("/"c)
            Return baseUrl
        End Get
    End Property

#End Region

#Region "Page Events"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
            Page.MasterPageFile = "~/default/ArabicMaster.master"
        Else
            Lang = CtlCommon.Lang.EN
            Page.MasterPageFile = "~/default/NewMaster.master"
        End If
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        Else
            Page.UICulture = SessionVariables.CultureInfo
        End If
        dir = GetPageDirection()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
        Else
            Lang = CtlCommon.Lang.EN
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
            formID = row("FormID")
        Next

        If Not Page.IsPostBack Then
            btnClear.Text = IIf(Lang = CtlCommon.Lang.AR, "مسح", "Clear")
            lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير الملخص", "Summary Reports")
            PageHeaderEfficiency.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير الكفاءة", "Efficiancy Reports")
            PageHeaderDiscipline.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير الانضباط", "Employee Discipline Reports")
            lblFromDate.Text = IIf(Lang = CtlCommon.Lang.AR, "من تاريخ", "From Date")
            lblToDate.Text = IIf(Lang = CtlCommon.Lang.AR, "الى تاريخ", "To Date")
            btnPrint.Text = IIf(Lang = CtlCommon.Lang.AR, "انشاء", "Generate")
            btnPrintEfficiency.Text = IIf(Lang = CtlCommon.Lang.AR, "انشاء", "Generate")
            btnCancel.Text = IIf(Lang = CtlCommon.Lang.AR, "إلغاء", "Cancel")
            FillReports(formID, SessionVariables.LoginUser.GroupId)


            RadDatePicker1.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd As New Date
            dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)

            RadDatePicker2.SelectedDate = dd

            objAPP_Settings = New APP_Settings
            With objAPP_Settings
                .GetByPK()
                rblFormat.SelectedValue = .DefaultReportFormat

            End With

            RadDatePicker3.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim datenew As New Date
            datenew = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)

            RadDatePicker4.SelectedDate = datenew

            RadDatePicker5.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim datenew2 As New Date
            datenew2 = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)

            RadDatePicker6.SelectedDate = datenew
        End If




    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        cryRpt.Close()
        cryRpt.Dispose()

    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim reportName As String = RadComboExtraReports.SelectedValue

        If SessionVariables.LoginUser.UserStatus = 2 And EmployeeFilter1.EntityId = 0 Then
            If SessionVariables.CultureInfo = "ar-JO" Then
                CtlCommon.ShowMessage(Page, "الرجاء اختيار وحدة العمل", "info")
            Else
                CtlCommon.ShowMessage(Page, "Please Select Entity", "info")
            End If
            Exit Sub
        End If
        BindReport(reportName)
    End Sub

    Protected Sub btnPrintEfficiency_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrintEfficiency.Click
        BindReport(EfficiencyRate)
    End Sub

    Protected Sub btnPrintDisciplineReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrintDisciplineReport.Click
        BindReport(EmployeeDisciplineReport)
    End Sub

    Protected Sub RadComboExtraReports_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboExtraReports.SelectedIndexChanged
        If RadComboExtraReports.SelectedValue = EfficiencyRate Then
            MultiView1.SetActiveView(Efficiency)
        ElseIf RadComboExtraReports.SelectedValue = HeadCountReport Then

            trFromDate.Visible = False
            trToDate.Visible = False
            dvGrade.Visible = False
            rfvGrades.Enabled = False
            dvWorkHoursParam.Visible = False
            dvSummaryParams.Visible = False
            dvMinWorkHrs_Detailed.Visible = False
        ElseIf RadComboExtraReports.SelectedValue = EmployeeDisciplineReport Then
            MultiView1.SetActiveView(Discipline)
            dvGrade.Visible = False
            rfvGrades.Enabled = False
            dvWorkHoursParam.Visible = False
            dvSummaryParams.Visible = False
            dvMinWorkHrs_Detailed.Visible = False
        ElseIf RadComboExtraReports.SelectedValue = GradeDeductionReport Then
            dvGrade.Visible = True
            rfvGrades.Enabled = True
            FillGrades()
            dvWorkHoursParam.Visible = False
            dvSummaryParams.Visible = False
            dvMinWorkHrs_Detailed.Visible = False
        ElseIf RadComboExtraReports.SelectedValue = WorkHours_ParamReport Then
            dvWorkHoursParam.Visible = True
            dvGrade.Visible = False
            rfvGrades.Enabled = False
            dvSummaryParams.Visible = False
            dvMinWorkHrs_Detailed.Visible = False
        ElseIf RadComboExtraReports.SelectedValue = SummaryReport_Param Then
            dvSummaryParams.Visible = True
            trFromDate.Visible = True
            trToDate.Visible = True
            dvGrade.Visible = False
            rfvGrades.Enabled = False
            dvWorkHoursParam.Visible = False
            dvMinWorkHrs_Detailed.Visible = False
        ElseIf RadComboExtraReports.SelectedValue = Detailed_Work_Hrs Then
            trFromDate.Visible = True
            trToDate.Visible = True
            dvMinWorkHrs_Detailed.Visible = True
            dvGrade.Visible = False
            rfvGrades.Enabled = False
            dvWorkHoursParam.Visible = False
        Else
            trFromDate.Visible = True
            trToDate.Visible = True
            dvGrade.Visible = False
            rfvGrades.Enabled = False
            dvWorkHoursParam.Visible = False
            dvSummaryParams.Visible = False
            dvMinWorkHrs_Detailed.Visible = False
        End If

        If RadComboExtraReports.SelectedValue = Statistical_Movements Then
            rblFormat.Visible = False
        Else
            rblFormat.Visible = True
        End If

    End Sub

    Protected Sub btnClear_Click(sender As Object, e As System.EventArgs) Handles btnClear.Click
        ClearFilter()
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("../Reports/Summary_Reports.aspx")
    End Sub

    Protected Sub btnCancelDiscipline_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelDiscipline.Click
        Response.Redirect("../Reports/Summary_Reports.aspx")
    End Sub

#End Region

#Region "Methods"

    Public Shared Function GetPageDirection() As String
        If SessionVariables.CultureInfo = "ar-JO" Then
            Return "rtl"
        Else
            Return "ltr"
        End If
    End Function

    Public Sub BindReport(ByVal reportName As String)
        reportName = reportName.Trim
        If Not RadComboExtraReports.SelectedValue = "-1" Then
            SetReportName(reportName.Trim)

            If reportName = Statistical_Movements Then
                If Not DT Is Nothing Then
                    If DT.Rows.Count > 0 Then
                        FillExcelReport(reportName)
                        ExportDataSetToExcel(NDT, "ExportedReport")
                        Return
                    Else
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NoDataFound", CultureInfo), "info")
                        Return
                    End If
                Else
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NoDataFound", CultureInfo), "info")
                    Return
                End If
            End If

            cryRpt = New ReportDocument

            cryRpt.Load(Server.MapPath(RPTName))
            cryRpt.SetDataSource(DT)
            Dim objAPP_Settings As New APP_Settings
            If reportName = EfficiencyRate Then
                objAPP_Settings.FK_CompanyId = EmployeeFilter2.CompanyId
            Else
                objAPP_Settings.FK_CompanyId = EmployeeFilter1.CompanyId
            End If

            dt2 = objAPP_Settings.GetHeader()

            dt2.Columns.Add("From_Date")
            dt2.Columns.Add("To_Date")
            If RadComboExtraReports.SelectedValue = EmployeeDisciplineReport Then
                dt2.Rows(0).Item("From_Date") = DateToString(RadDatePicker5.SelectedDate)
                dt2.Rows(0).Item("To_Date") = DateToString(RadDatePicker6.SelectedDate)
            ElseIf RadComboExtraReports.SelectedValue = EfficiencyRate Then

                dt2.Rows(0).Item("From_Date") = DateToString(RadDatePicker3.SelectedDate)
                dt2.Rows(0).Item("To_Date") = DateToString(RadDatePicker4.SelectedDate)
            Else
                dt2.Rows(0).Item("From_Date") = DateToString(RadDatePicker1.SelectedDate)
                dt2.Rows(0).Item("To_Date") = DateToString(RadDatePicker2.SelectedDate)
            End If

            cryRpt.Subreports(cryRpt.Subreports().Item(0).Name).SetDataSource(dt2)
            If reportName = EmployeeLeavesSummary Then
                cryRpt.SetParameterValue("@URL", BaseSiteUrl)
            End If
            If reportName = WorkHours_ParamReport Then
                Dim strSelection As String = ""
                If Not rblWorkDurationSelection.SelectedValue = Nothing Then
                    strSelection = rblWorkDurationSelection.SelectedItem.Text
                End If
                cryRpt.SetParameterValue("@rptWork_Hours_Selection", strSelection)
                cryRpt.SetParameterValue("@rptWork_Hours_Count", IIf(radnumWorkDurationNum.Text = String.Empty, "", radnumWorkDurationNum.Text))
                cryRpt.SetParameterValue("@rptWork_Hours", rmtxtMinDuration.TextWithLiterals)
            End If

            'cryRpt.SetParameterValue("@Version", Version)
            cryRpt.SetParameterValue("@UserName", UserName)

            Dim ShowSTLogo As Boolean
            ShowSTLogo = ConfigurationManager.AppSettings("ShowSmartTimeLogo")
            cryRpt.SetParameterValue("@ShowSTLogo", False)
            cryRpt.SetParameterValue("@ShowSTLogo", False, "rptHeader")
            cryRpt.SetParameterValue("@ReportName", RadComboExtraReports.SelectedItem.Text)
            cryRpt.SetParameterValue("@ReportName", RadComboExtraReports.SelectedItem.Text, "rptHeader")
            cryRpt.SetParameterValue("@FromDate", DateToString(RadDatePicker1.SelectedDate))
            cryRpt.SetParameterValue("@ToDate", DateToString(RadDatePicker2.SelectedDate))
            'Version = System.Configuration.ConfigurationManager.AppSettings("Version").ToString()
            Version = SmartV.Version.version.GetVersionNumber
            cryRpt.SetParameterValue("@Version", Version)

            If SessionVariables.CultureInfo = "ar-JO" Then
                CopyRightAr = objSmartSecurity.strSmartDecrypt(ConfigurationManager.AppSettings("CopyRightAr").ToString(), "SmartVision")
                cryRpt.SetParameterValue("@CopyRightAr", CopyRightAr)
            Else
                CopyRightEn = objSmartSecurity.strSmartDecrypt(ConfigurationManager.AppSettings("CopyRightEn").ToString(), "SmartVision")
                cryRpt.SetParameterValue("@CopyRightEn", CopyRightEn)
            End If

            Dim showSmartTimeLogo As Boolean = CBool(ConfigurationManager.AppSettings("ShowSmartTimeLogo"))
            If showSmartTimeLogo Then
                objSYSUsers = New SYSUsers
                objOrgCompany = New OrgCompany
                Dim rptObj As New Report
                If EmployeeFilter1.CompanyId <> 0 Then
                    objOrgCompany.CompanyId = EmployeeFilter1.CompanyId
                    objOrgCompany.GetByPK()
                End If
                If Not (objOrgCompany.CompanyId = 0 Or objOrgCompany.Logo Is Nothing) Then
                    Dim ImagePath As String = "~/images/CompaniesLogo/" & objOrgCompany.CompanyId & objOrgCompany.Logo
                    Dim filePath As String = HttpContext.Current.Server.MapPath(ImagePath)
                    If System.IO.File.Exists(filePath) Then
                        cryRpt.SetParameterValue("@LogoURL", filePath, "rptHeader")
                    Else
                        cryRpt.SetParameterValue("@LogoURL", "nologo", "rptHeader")
                    End If
                Else
                    cryRpt.SetParameterValue("@LogoURL", "nologo", "rptHeader")
                End If
            Else
                cryRpt.SetParameterValue("@LogoURL", "", "rptHeader")
            End If
            CRV.ReportSource = cryRpt
            If Not DT Is Nothing Then
                If DT.Rows.Count > 0 Then
                    If reportName = EfficiencyRate Then
                        If RadioButtonList1.SelectedValue = 1 Then
                            cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedReport")
                        ElseIf RadioButtonList1.SelectedValue = 2 Then
                            cryRpt.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, True, "ExportedReport")
                        ElseIf RadioButtonList1.SelectedValue = 3 Then
                            FillExcelReport(reportName)
                            ExportDataSetToExcel(NDT, "ExportedReport", HeadDT)
                        End If
                    ElseIf reportName = EmployeeDisciplineReport Then
                        If RadioButtonList2.SelectedValue = 1 Then
                            cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedReport")
                        ElseIf RadioButtonList2.SelectedValue = 2 Then
                            cryRpt.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, True, "ExportedReport")
                        ElseIf RadioButtonList2.SelectedValue = 3 Then
                            FillExcelReport(reportName)
                            ExportDataSetToExcel(NDT, "ExportedReport", HeadDT)
                        End If
                    Else
                        If rblFormat.SelectedValue = 1 Then
                            cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedReport")
                        ElseIf rblFormat.SelectedValue = 2 Then
                            cryRpt.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, True, "ExportedReport")
                        ElseIf rblFormat.SelectedValue = 3 Then
                            FillExcelReport(reportName)
                            If reportName = LeavesSummaryReports Then
                                ExportDataSetToExcel_WithColor(NDT, "ExportedReport", HeadDT, Color.Red)
                            Else
                                ExportDataSetToExcel(NDT, "ExportedReport", HeadDT)
                            End If


                        End If
                    End If
                    MultiView1.SetActiveView(Report)
                Else
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NoDataFound", CultureInfo), "info")
                End If
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NoDataFound", CultureInfo), "info")
            End If
        Else
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("PleaseSelectReport", CultureInfo), "info")
        End If


    End Sub

    Private Function DateToString(ByVal TempDate As Date) As String
        Dim tempDay As String
        Dim tempMonth As String

        If TempDate.Month.ToString.Length = 1 Then
            tempMonth = "0" + TempDate.Month.ToString
        Else
            tempMonth = TempDate.Month.ToString
        End If
        If TempDate.Day.ToString.Length = 1 Then
            tempDay = "0" + TempDate.Day.ToString
        Else
            tempDay = TempDate.Day.ToString
        End If
        Return tempDay + "/" + tempMonth + "/" + TempDate.Year.ToString()
    End Function

    Private Sub SetReportName(ByVal rpttid As String)
        Dim rptObj As New Report
        If Not RadDatePicker1.SelectedDate Is Nothing Then
            rptObj.FROM_DATE = RadDatePicker1.DbSelectedDate
        End If
        If Not RadDatePicker2.SelectedDate Is Nothing Then
            rptObj.TO_DATE = RadDatePicker2.DbSelectedDate
        End If

        If EmployeeFilter1.EmployeeId <> 0 Then
            rptObj.EmployeeId = EmployeeFilter1.EmployeeId
        Else
            rptObj.EmployeeId = Nothing
        End If

        If Not EmployeeFilter1.CompanyId = 0 Then
            rptObj.CompanyId = EmployeeFilter1.CompanyId
        Else
            rptObj.CompanyId = Nothing
        End If

        If EmployeeFilter1.ShowDirectStaffCheck = True Then
            rptObj.DirectStaffOnly = EmployeeFilter1.DirectStaffOnly
        Else
            rptObj.DirectStaffOnly = False
        End If
        If RadComboExtraReports.SelectedValue = GradeDeductionReport Then
            rptObj.Emp_GradeId = ddlGrades.SelectedValue
        End If

        If EmployeeFilter1.FilterType = "C" Then
            If Not EmployeeFilter1.EntityId = 0 Then
                rptObj.EntityId = EmployeeFilter1.EntityId
            Else
                rptObj.EntityId = Nothing
            End If
        ElseIf EmployeeFilter1.FilterType = "W" Then
            If Not EmployeeFilter1.EntityId = 0 Then
                rptObj.WorkLocationId = EmployeeFilter1.EntityId
            Else
                rptObj.WorkLocationId = Nothing
            End If
        ElseIf EmployeeFilter1.FilterType = "L" Then
            If Not EmployeeFilter1.EntityId = 0 Then
                rptObj.LogicalGroupId = EmployeeFilter1.EntityId
            Else
                rptObj.LogicalGroupId = Nothing
            End If
        End If
        If RadComboExtraReports.SelectedValue = EfficiencyRate Then
            If EmployeeFilter2.EmployeeId <> 0 Then
                rptObj.EmployeeId = EmployeeFilter2.EmployeeId
            Else
                rptObj.EmployeeId = Nothing
            End If

            If Not EmployeeFilter2.EntityId = -1 Then
                rptObj.EntityId = EmployeeFilter2.EntityId
            Else
                rptObj.EntityId = Nothing
            End If

            If Not EmployeeFilter2.CompanyId = 0 Then
                rptObj.CompanyId = EmployeeFilter2.CompanyId
            Else
                rptObj.CompanyId = Nothing
            End If
            rptObj.FROM_DATE = RadDatePicker3.SelectedDate
            rptObj.TO_DATE = RadDatePicker4.SelectedDate
        End If

        If RadComboExtraReports.SelectedValue = EmployeeDisciplineReport Then
            rptObj.FROM_DATE = RadDatePicker5.SelectedDate
            rptObj.TO_DATE = RadDatePicker6.SelectedDate

            If Not EmployeeFilterDiscipline.CompanyId = -1 Then
                rptObj.CompanyId = EmployeeFilterDiscipline.CompanyId
            Else
                rptObj.CompanyId = Nothing
            End If

            If Not EmployeeFilterDiscipline.EntityId = -1 Then
                rptObj.EntityId = EmployeeFilterDiscipline.EntityId
            Else
                rptObj.EntityId = Nothing
            End If
        End If

        If RadComboExtraReports.SelectedValue = WorkHours_ParamReport Then
            rptObj.Work_Hours_Selection = rblWorkDurationSelection.SelectedValue
            rptObj.Work_Hours_Count = IIf(radnumWorkDurationNum.Text = String.Empty, Nothing, radnumWorkDurationNum.Text)
            rptObj.Work_Hours = (CInt(rmtxtMinDuration.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtxtMinDuration.TextWithLiterals.Split(":")(1))

        End If

        If RadComboExtraReports.SelectedValue = SummaryReport_Param Then
            rptObj.Absent_Count_Selection = rblAbsentDays.SelectedValue
            rptObj.Absent_Count = IIf(radnumAbsentDays.Text = String.Empty, Nothing, radnumAbsentDays.Text)
            rptObj.Work_Hours_Selection = rblWorkHours.SelectedValue
            rptObj.Work_Hours = (CInt(rmtxtWorkHours.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtxtWorkHours.TextWithLiterals.Split(":")(1))
        End If

        If RadComboExtraReports.SelectedValue = Detailed_Work_Hrs Then
            rptObj.Work_Hours = (CInt(rmtMinWorkHrs_Detailed.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtMinWorkHrs_Detailed.TextWithLiterals.Split(":")(1))
        End If
        'rpttid = IIf(rpttid = 0, 3, rpttid)
        Select Case rpttid
            Case SummaryReport
                RPTName = "rptSummary.rpt"
                DT = rptObj.GetSummary()
            Case SummaryReport_WithInvalid
                RPTName = "rptSummary_WithInvalid.rpt"
                DT = rptObj.GetSummary()
            Case SummaryReportWithExpected
                RPTName = "rptSummary_WithExpected.rpt"
                DT = rptObj.GetSummary_WithExpected
            Case UtilizationSummary
                RPTName = "rptSummary_UtilizationWithExpected.rpt"
                DT = rptObj.GetUtilizationSummary
            Case EmployeeAbsentSummary
                RPTName = "rptEmpSummaryAbsent.rpt"
                DT = rptObj.GetEmpAbsent()
            Case EmployeeLeavesSummary
                RPTName = "rptEmpSummaryLeaves.rpt"
                DT = rptObj.GetEmpLeaves
            Case ManualEntrySummary
                RPTName = "rptManualEntry_Summary.rpt"
                DT = rptObj.GetManualEntry_Summary
            Case DeductionReport
                RPTName = "rpt_MonthlyDeduction.rpt"
                DT = rptObj.Get_MonthlyDeduction
            Case GradeDeductionReport
                RPTName = "rpt_MonthlyDeduction.rpt"
                DT = rptObj.Get_MonthlyDeduction_ByGrade
            Case SummaryReport2
                RPTName = "rptSummary2.rpt"
                DT = rptObj.GetSummary
            Case EfficiencyRate
                RPTName = "rptEfficiencyRate.rpt"
                DT = rptObj.GetEntity_TOTHRS()
            Case TransPercentageReport
                RPTName = "rpt_TransPercentage.rpt"
                DT = rptObj.Get_TransPercentage
            Case WorkHoursDaysReport
                RPTName = "rpt_WorkHrs.rpt"
                DT = rptObj.GetWork_Hrs
            Case HeadCountReport
                RPTName = "rptEmpHeadCount.rpt"
                DT = rptObj.GetHeadCountReport
            Case EffectiveWorkingHours
                RPTName = "rptEffectiveWorkHours.rpt"
                DT = rptObj.GetEffectiveWorkHours
                If IsDBNull(DT.Rows(0)("TotalWorkingHrs")) Then
                    DT = Nothing
                End If
            Case ApproveViolationsSummaryReport
                RPTName = "rptPayrollSummaryViolationToERP.rpt"
                DT = rptObj.GetSummaryPayrollApproval
            Case Statistical_Movements
                'RPTName = "rptPayrollSummaryViolationToERP.rpt"
                DT = rptObj.Statistical_Movements
            Case EmployeeDisciplineReport
                RPTName = "rptEmpDiscipline.rpt"
                DT = rptObj.Get_EmpDiscipline
            Case WorkHours_ParamReport
                RPTName = "rpt_WorkHrs_Param.rpt"
                DT = rptObj.GetWork_Hrs_Param
            Case SummaryReport_Param
                RPTName = "rptSummary_Param.rpt"
                DT = rptObj.GetSummary_Param
            Case Detailed_Work_Hrs
                RPTName = "rpt_Detailed_WorkHrs.rpt"
                DT = rptObj.Get_Detailed_Work_Hrs
            Case SummaryWorkDays
                RPTName = "rptSummary_WorkDays.rpt"
                DT = rptObj.Get_Summary_WorkDays
            Case SummaryDelayEarlyOut
                RPTName = "rptDelayEarlyOut_Summary.rpt"
                DT = rptObj.Get_DelayEarlyOut_Summary
            Case Summary_ByMonth
                RPTName = "rptSummary_ByMonth.rpt"
                DT = rptObj.GetSummary_ByMonth
            Case DeductionPerPolicy
                RPTName = "rpt_DeductionPerPolicy.rpt"
                DT = rptObj.Get_Deduction_Per_Policy
            Case DetailedDeductionPerPolicy
                RPTName = "rpt_DetailedDeductionPerPolicy.rpt"
                DT = rptObj.Get_Detailed_Deduction_Per_Policy
            Case LeavesSummaryReports
                RPTName = "rpt_LeavesSummary_SZGM.rpt"
                DT = rptObj.GetLeavesSummary
        End Select

        UserName = SessionVariables.LoginUser.UsrID

        If (Lang = CtlCommon.Lang.AR) Then
            RPTName = "Arabic/" + RPTName
        Else
            RPTName = "English/" + RPTName
        End If

    End Sub

    Private Sub FillOvertimeStatus()
        objRequestStatus = New TA.LookUp.RequestStatus
        CtlCommon.FillTelerikDropDownList(ddlOvertimeStatus, objRequestStatus.GetAll, Lang)

    End Sub

    Private Sub FillExcelReport(ByVal reportname As String)
        objsysforms = New SYSForms
        NDT = New DataTable
        HeadDT = New DataTable
        NDT = DT.Clone()

        Dim UK_Culture As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("en-GB")

        '------------------ADD REPORT HEADER-----------------
        Dim drFormName As DataRow
        drFormName = HeadDT.NewRow()
        HeadDT.Columns.Add()
        HeadDT.Columns.Add()
        HeadDT.Columns(0).ColumnName = "."
        HeadDT.Columns(1).ColumnName = ".."
        HeadDT.Rows.Add(0)
        HeadDT.Rows.Add(1)
        HeadDT.Rows.Add(2)
        If Not RadComboExtraReports.SelectedValue = HeadCountReport Then
            HeadDT.Rows.Add(3)
        End If
        Dim rptName As String = RadComboExtraReports.SelectedValue
        Dim dtFormName As DataTable
        dtFormName = objsysforms.GetBy_FormName(rptName)
        If Lang = CtlCommon.Lang.AR Then

            HeadDT(0)(0) = dtFormName(0)("Desc_Ar")
            '------Column(0)-----------
            HeadDT(1)(1) = "طبع بواسطة:"
            HeadDT(2)(1) = "تاريخ الطباعة:"
            If Not RadComboExtraReports.SelectedValue = HeadCountReport Then
                HeadDT(3)(1) = "من تاريخ: " & Convert.ToDateTime(RadDatePicker1.SelectedDate).ToString("dd/MM/yyyy")
            End If
            '------Column(1)-----------
            HeadDT(1)(0) = SessionVariables.LoginUser.UsrID
            HeadDT(2)(0) = Date.Today.ToString("dd/MM/yyyy")
            If Not RadComboExtraReports.SelectedValue = HeadCountReport Then
                HeadDT(3)(0) = "الى تاريخ: " & Convert.ToDateTime(RadDatePicker2.SelectedDate).ToString("dd/MM/yyyy")
            End If
        Else
            HeadDT(0)(0) = dtFormName(0)("Desc_En").ToString
            '------Column(0)-----------
            HeadDT(1)(0) = "Printed By:"
            HeadDT(2)(0) = "Printing Date:"
            If Not RadComboExtraReports.SelectedValue = HeadCountReport Then
                HeadDT(3)(0) = "From Date: " & Convert.ToDateTime(RadDatePicker1.SelectedDate).ToString("dd/MM/yyyy")
            End If
            '------Column(1)-----------
            HeadDT(1)(1) = SessionVariables.LoginUser.UsrID
            HeadDT(2)(1) = Date.Today.ToString("dd/MM/yyyy")
            If Not RadComboExtraReports.SelectedValue = HeadCountReport Then
                HeadDT(3)(1) = "To Date: " & Convert.ToDateTime(RadDatePicker2.SelectedDate).ToString("dd/MM/yyyy")
            End If
        End If

        HeadDT.Rows.Add(drFormName)
        '------------------ADD REPORT HEADER-----------------

        Select Case reportname
            Case SummaryReport
                NDT = DT
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف باللغة العربية"
                    NDT.Columns(2).ColumnName = "التأخير"
                    NDT.Columns(3).ColumnName = "الخروج المبكر"
                    NDT.Columns(4).ColumnName = "الوقت الاضافي"
                    NDT.Columns(5).ColumnName = "عدد مرات التأخير"
                    NDT.Columns(6).ColumnName = "عدد مرات الخروج المبكر"
                    NDT.Columns(7).ColumnName = "عدد مرات الوقت الاضافي"
                    NDT.Columns(8).ColumnName = "عدد مرات الغياب"
                    NDT.Columns(9).ColumnName = "عدد مرات ايام الاستراحة"
                    NDT.Columns(10).ColumnName = "عدد مرات ايام المغادرة"
                    NDT.Columns(11).ColumnName = "عدد مرات الادخال اليدوي "
                    NDT.Columns(12).ColumnName = "مجموع ساعات العمل"
                    NDT.Columns(13).ColumnName = "عدد مرات ايام الاجازة"
                    NDT.Columns(14).ColumnName = "مجموع الوقت الضائع"
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns(15).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns(16).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                Else
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Delay"
                    NDT.Columns(3).ColumnName = "Early Out"
                    NDT.Columns(4).ColumnName = "OverTime"
                    NDT.Columns(5).ColumnName = "Delay Times"
                    NDT.Columns(6).ColumnName = "Early Out Times"
                    NDT.Columns(7).ColumnName = "OverTime Times"
                    NDT.Columns(8).ColumnName = "Absent Count"
                    NDT.Columns(9).ColumnName = "Rest Count"
                    NDT.Columns(10).ColumnName = "Permission Count"
                    NDT.Columns(11).ColumnName = "Manual Entry Count"
                    NDT.Columns(12).ColumnName = "Total Work Hours"
                    NDT.Columns(13).ColumnName = "Leaves Count"
                    NDT.Columns(14).ColumnName = "Total Lost Time"
                    NDT.Columns(15).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns(16).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                End If

            Case SummaryReportWithExpected
                NDT = DT
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "اسم الموظف باللغة العربية"
                    NDT.Columns(3).ColumnName = "التأخير"
                    NDT.Columns(4).ColumnName = "الخروج المبكر"
                    NDT.Columns(5).ColumnName = "الوقت الاضافي"
                    NDT.Columns(6).ColumnName = "عدد مرات التأخير"
                    NDT.Columns(7).ColumnName = "عدد مرات الخروج المبكر"
                    NDT.Columns(8).ColumnName = "عدد مرات الوقت الاضافي"
                    NDT.Columns(9).ColumnName = "عدد مرات الغياب"
                    NDT.Columns(10).ColumnName = "عدد مرات الاجازة"
                    NDT.Columns(11).ColumnName = "مجموع ساعات العمل"
                    NDT.Columns(12).ColumnName = "مجموع ساعات العمل مع المتوقع"
                Else
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns(2).ColumnName = "Employee Arabic Name"
                    NDT.Columns(3).ColumnName = "Delay"
                    NDT.Columns(4).ColumnName = "Early Out"
                    NDT.Columns(5).ColumnName = "OverTime"
                    NDT.Columns(6).ColumnName = "Delay Times"
                    NDT.Columns(7).ColumnName = "Early Out Times"
                    NDT.Columns(8).ColumnName = "OverTime Times"
                    NDT.Columns(9).ColumnName = "Absent Count"
                    NDT.Columns(10).ColumnName = "Rest Count"
                    NDT.Columns(11).ColumnName = "Total Work Hours"
                    NDT.Columns(12).ColumnName = "Total Work Hours With Expected"
                End If
            Case UtilizationSummary
                NDT = DT
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "اسم الموظف باللغة العربية"
                    NDT.Columns(3).ColumnName = "مجموع ساعات العمل"
                    NDT.Columns(4).ColumnName = "مجموع ساعات العمل مع المتوقع"
                    NDT.Columns(5).ColumnName = "الوقت المستغل"
                    NDT.Columns(6).ColumnName = "اسم وحدة العمل"
                    NDT.Columns(7).ColumnName = "اسم وحدة العمل باللغة العربية"
                    NDT.Columns(8).ColumnName = "اسم الشركة "
                    NDT.Columns(9).ColumnName = "اسم الشركة بالغة العربية "
                Else
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns(2).ColumnName = "Employee Arabic Name"
                    NDT.Columns(3).ColumnName = "Total Work Hours"
                    NDT.Columns(4).ColumnName = "Total Work Hours With Expected"
                    NDT.Columns(5).ColumnName = "Utilization"
                    NDT.Columns(6).ColumnName = "Entity Name"
                    NDT.Columns(7).ColumnName = "Entity Arabic Name"
                    NDT.Columns(8).ColumnName = "Company Name"
                    NDT.Columns(9).ColumnName = "Company Arabic Name"
                End If
            Case SummaryReportWithExpected
                NDT = DT
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "اسم الموظف باللغة العربية"
                    NDT.Columns(3).ColumnName = "التأخير"
                    NDT.Columns(4).ColumnName = "الخروج المبكر"
                    NDT.Columns(5).ColumnName = "الوقت الاضافي"
                    NDT.Columns(6).ColumnName = "عدد مرات التأخير"
                    NDT.Columns(7).ColumnName = "عدد مرات الخروج المبكر"
                    NDT.Columns(8).ColumnName = "عدد مرات الوقت الاضافي"
                    NDT.Columns(9).ColumnName = "عدد مرات الغياب"
                    NDT.Columns(10).ColumnName = "عدد مرات الاجازة"
                    NDT.Columns(11).ColumnName = "مجموع ساعات العمل"
                    NDT.Columns(12).ColumnName = "مجموع ساعات العمل مع المتوقع"
                Else
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns(2).ColumnName = "Employee Arabic Name"
                    NDT.Columns(3).ColumnName = "Delay"
                    NDT.Columns(4).ColumnName = "Early Out"
                    NDT.Columns(5).ColumnName = "OverTime"
                    NDT.Columns(6).ColumnName = "Delay Times"
                    NDT.Columns(7).ColumnName = "Early Out Times"
                    NDT.Columns(8).ColumnName = "OverTime Times"
                    NDT.Columns(9).ColumnName = "Absent Count"
                    NDT.Columns(10).ColumnName = "Rest Count"
                    NDT.Columns(11).ColumnName = "Total Work Hours"
                    NDT.Columns(12).ColumnName = "Total Work Hours With Expected"
                End If
            Case EmployeeAbsentSummary
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "اسم الشركة"
                    NDT.Columns(4).ColumnName = "التاريخ "
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)

                Else
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Date"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)


                End If
            Case EmployeeLeavesSummary
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    NDT.Columns(16).DataType = System.Type.GetType("System.String")

                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 16 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "اسم الموظف باللغة العربية"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "اسم وحدة العمل"
                    NDT.Columns(4).ColumnName = "اسم وحدة العمل باللغة العربية"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "اسم الشركة"
                    NDT.Columns(6).ColumnName = "اسم الشركة باللغة العربية"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "تاريخ الطلب"
                    NDT.Columns(8).ColumnName = "اسم الاجازة"
                    NDT.Columns(9).ColumnName = "نوع الاجازة باللغة العربية"
                    NDT.Columns(10).ColumnName = "من تاريخ"
                    NDT.Columns(11).ColumnName = "الى تاريخ"
                    NDT.Columns(12).ColumnName = "الايام"
                    NDT.Columns(13).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(14)
                Else
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    NDT.Columns(16).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 16 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns(2).ColumnName = "Employee Arabic Name"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "Entity Name"
                    NDT.Columns(4).ColumnName = "Entity Arabic Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Company Name"
                    NDT.Columns(6).ColumnName = "Company Arabic Name"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "Request Date"
                    NDT.Columns(8).ColumnName = "Leave Name"
                    NDT.Columns(9).ColumnName = "Leave Arabic Name"
                    NDT.Columns(10).ColumnName = "From Date"
                    NDT.Columns(11).ColumnName = "To Date"
                    NDT.Columns(12).ColumnName = "No. Of Days"
                    NDT.Columns(13).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(14)
                End If
            Case ManualEntrySummary
                NDT = DT
                If (Lang = CtlCommon.Lang.AR) Then

                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "عدد مرات الدخول"
                    NDT.Columns(3).ColumnName = "عدد مرات الخروج"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                Else
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "In No."
                    NDT.Columns(3).ColumnName = "Out No."
                    NDT.Columns(4).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)



                End If

            Case EfficiencyRate
                NDT = DT

            Case DeductionReport

                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(19).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 19 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "السنة"
                    NDT.Columns(3).ColumnName = "الشهر"
                    NDT.Columns(4).ColumnName = "عدد مرات الغياب"
                    NDT.Columns(5).ColumnName = "التأخير"
                    NDT.Columns(6).ColumnName = "الخروج المبكر"
                    NDT.Columns(7).ColumnName = "مدة الخروج(حسب الاعدادات)"
                    NDT.Columns(8).ColumnName = "مجموع التأخير و الخروج المبكر"
                    NDT.Columns(9).ColumnName = "عدد مرات التأخير"
                    NDT.Columns(10).ColumnName = "عدد مرات الخروج المبكر"
                    NDT.Columns(11).ColumnName = "عدد مرات التأخير و الخروج المبكر"
                    NDT.Columns(12).ColumnName = "عدد مرات لم يسجل دخول"
                    NDT.Columns(13).ColumnName = "عدد مرات لم يسجل خروج"
                    NDT.Columns(14).ColumnName = "لم يستكمل الدوام الرسمي"
                    NDT.Columns(15).ColumnName = "مجموع الخصومات"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns(16).ColumnName = "تم الاعتماد من قبل"
                    NDT.Columns(17).ColumnName = "تاريخ الاعتماد"
                    NDT.Columns.RemoveAt(18)
                    NDT.Columns.RemoveAt(18)
                    NDT.Columns(18).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns(19).ColumnName = "اسم الشركة"

                Else
                    NDT.Columns(19).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 19 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Employee No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Year"
                    NDT.Columns(3).ColumnName = "Month"
                    NDT.Columns(4).ColumnName = "Absent Count"
                    NDT.Columns(5).ColumnName = "Delay"
                    NDT.Columns(6).ColumnName = "Early Out"
                    NDT.Columns(7).ColumnName = "Out Duration (If Included)"
                    NDT.Columns(8).ColumnName = "Total Delay and Early Out"
                    NDT.Columns(9).ColumnName = "Delay Count"
                    NDT.Columns(10).ColumnName = "Early Out Count"
                    NDT.Columns(11).ColumnName = "Delay and Early Out Count"
                    NDT.Columns(12).ColumnName = "Missing In Count"
                    NDT.Columns(13).ColumnName = "Missing Out Count"
                    NDT.Columns(14).ColumnName = "Uncomplete Work Hours"
                    NDT.Columns(15).ColumnName = "Total All Deductions"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns(16).ColumnName = "Approved By"
                    NDT.Columns(17).ColumnName = "Approval Date"
                    NDT.Columns.RemoveAt(18)
                    NDT.Columns(18).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns(19).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(20)

                End If

            Case SummaryReport2

                NDT = DT
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "التأخير"
                    NDT.Columns(3).ColumnName = "الخروج المبكر"
                    NDT.Columns(4).ColumnName = "الوقت الاضافي"
                    NDT.Columns(5).ColumnName = "عدد مرات التأخير"
                    NDT.Columns(6).ColumnName = "عدد مرات الخروج المبكر"
                    NDT.Columns(7).ColumnName = "عدد مرات الوقت الاضافي"
                    NDT.Columns(8).ColumnName = "عدد مرات الغياب"
                    NDT.Columns(9).ColumnName = "عدد مرات ابام الاستراحة"
                    NDT.Columns(10).ColumnName = "عدد مرات المغادرة"
                    NDT.Columns(11).ColumnName = "عدد مرات الادخال البدوي"
                    NDT.Columns(12).ColumnName = "مجموع ساعات العمل"
                    NDT.Columns(13).ColumnName = "ايام الاجازات"
                    NDT.Columns(14).ColumnName = "الوقت الضائع"
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns(15).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns(16).ColumnName = "اسم الشركة"
                    NDT.Columns(17).ColumnName = "الوقت الفعلي"
                    NDT.Columns(18).ColumnName = "الوقت الفعلي بالدقائق"
                    NDT.Columns(19).ColumnName = "عدد المغادرات المعلقة"
                    NDT.Columns.RemoveAt(20)

                    NDT.SetColumnsOrder(New String() {"رقم الموظف", "اسم الموظف", "عدد مرات الغياب", "عدد مرات ابام الاستراحة", "عدد مرات الوقت الاضافي", "الوقت الاضافي",
                    "عدد مرات الخروج المبكر", "الخروج المبكر", "عدد مرات التأخير", "التأخير", "عدد مرات المغادرة",
                    "عدد المغادرات المعلقة", "عدد مرات الادخال البدوي", "الوقت الضائع", "الوقت الفعلي", "الوقت الفعلي بالدقائق", "ايام الاجازات",
                    "مجموع ساعات العمل", "اسم وحدة العمل", "اسم الشركة"})

                Else
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Delay"
                    NDT.Columns(3).ColumnName = "Early Out"
                    NDT.Columns(4).ColumnName = "OverTime"
                    NDT.Columns(5).ColumnName = "Delay Times"
                    NDT.Columns(6).ColumnName = "Early Out Times"
                    NDT.Columns(7).ColumnName = "OverTime Times"
                    NDT.Columns(8).ColumnName = "Absent Count"
                    NDT.Columns(9).ColumnName = "Rest Count"
                    NDT.Columns(10).ColumnName = "Permission"
                    NDT.Columns(11).ColumnName = "Manual Entry"
                    NDT.Columns(12).ColumnName = "Total Work Hours"
                    NDT.Columns(13).ColumnName = "Leave Days"
                    NDT.Columns(14).ColumnName = "Lost Time"
                    NDT.Columns(15).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns(16).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns(17).ColumnName = "Net Time"
                    NDT.Columns(18).ColumnName = "Net Time In Minutes"
                    NDT.Columns(19).ColumnName = "Pending Permissions"
                    NDT.Columns.RemoveAt(20)

                    NDT.SetColumnsOrder(New String() {"Emp No.", "Employee Name", "Absent Count", "Rest Count", "OverTime Times", "OverTime",
                  "Early Out Times", "Early Out", "Delay Times", "Delay", "Permission",
                  "Pending Permissions", "Manual Entry", "Lost Time", "Net Time", "Net Time In Minutes", "Leave Days",
                  "Total Work Hours", "Entity Name", "Company Name"})
                End If
            Case TransPercentageReport
                NDT = DT
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم وحدة العمل"
                    NDT.Columns(2).ColumnName = "الاجازة"
                    NDT.Columns(3).ColumnName = "المخالفات"
                    NDT.Columns(4).ColumnName = "الغياب"
                    NDT.Columns(5).ColumnName = "الحضور "
                    NDT.Columns(6).ColumnName = "ايام الاستراحة"
                    NDT.Columns(7).ColumnName = "العطلة"
                    NDT.Columns(8).ColumnName = "المغادرة"
                    NDT.Columns(9).ColumnName = "المجموع"
                    NDT.Columns(10).ColumnName = "التأخير"
                    NDT.Columns(11).ColumnName = "الخروج المبكر"
                    NDT.Columns(12).ColumnName = "الادخال اليدوي"
                    NDT.Columns.RemoveAt(13)

                    NDT.SetColumnsOrder(New String() {"اسم الشركة",
                                        "اسم وحدة العمل",
                                        "الاجازة",
                                        "المخالفات",
                                        "الغياب",
                                        "الحضور ",
                                        "ايام الاستراحة",
                                        "العطلة",
                                        "المغادرة",
                                        "التأخير",
                                        "الخروج المبكر",
                                        "الادخال اليدوي",
                                        "المجموع"})

                Else
                    NDT.Columns(0).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Leave"
                    NDT.Columns(3).ColumnName = "Absent"
                    NDT.Columns(4).ColumnName = "Logical Absent"
                    NDT.Columns(5).ColumnName = "Attend"
                    NDT.Columns(6).ColumnName = "Rest Day"
                    NDT.Columns(7).ColumnName = "Holiday"
                    NDT.Columns(8).ColumnName = "Permission"
                    NDT.Columns(9).ColumnName = "Total All"
                    NDT.Columns(10).ColumnName = "Delay"
                    NDT.Columns(11).ColumnName = "Early Out"
                    NDT.Columns(12).ColumnName = "Manual Entry"
                    'NDT.Columns(13).ColumnName = "Violation"
                    NDT.Columns.RemoveAt(13)

                    NDT.SetColumnsOrder(New String() {"Company Name",
                                       "Entity Name",
                                       "Leave",
                                       "Absent",
                                       "Logical Absent",
                                       "Attend",
                                       "Rest Day",
                                       "Holiday",
                                       "Permission",
                                       "Delay",
                                       "Early Out",
                                       "Manual Entry",
                                       "Total All"})
                End If
            Case WorkHoursDaysReport
                NDT = DT
                If (Lang = CtlCommon.Lang.AR) Then

                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "عدد ايام الغياب"
                    NDT.Columns(3).ColumnName = "عدد ايام العمل اقل من الجدول"
                    NDT.Columns(4).ColumnName = "عدد ايام العمل اكثر من الجدول"
                    NDT.Columns(5).ColumnName = "عدد لايوجد دخول"
                    NDT.Columns(6).ColumnName = "عدد لايوجد خروج"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)

                Else

                    NDT.Columns(0).ColumnName = "Employee No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Absent Days No."
                    NDT.Columns(3).ColumnName = "No. Of Working Days Less Than Schedule"
                    NDT.Columns(4).ColumnName = "No. Of Working Days More Than Schedule"
                    NDT.Columns(5).ColumnName = "No. Of Missing In"
                    NDT.Columns(6).ColumnName = "No. Of Missing Out"
                    NDT.Columns(7).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                End If
            Case HeadCountReport

                NDT.Columns(4).DataType = System.Type.GetType("System.String")
                For Each row As DataRow In DT.Rows
                    Dim dr As DataRow = NDT.NewRow
                    For i As Integer = 0 To NDT.Columns.Count - 1
                        dr(i) = row(i)
                        If i = 4 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                            End If
                        End If
                    Next
                    NDT.Rows.Add(dr)
                Next
                If (Lang = CtlCommon.Lang.AR) Then

                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "الوقت"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "الجهاز"
                    NDT.Columns(4).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "اسم الشركة"
                Else
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Employee No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "In Time"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "Reader"
                    NDT.Columns(4).ColumnName = "Remarks"
                    NDT.Columns(5).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(7)


                End If
            Case EffectiveWorkingHours
                NDT = DT


            Case Statistical_Movements
                NDT = DT
                Dim StartPreviousYear As String = ""
                Dim EndPreviousYear As String = ""

                Dim FromDate As String = ""
                Dim ToDate As String = ""
                FromDate = DateToString(RadDatePicker1.DbSelectedDate)
                ToDate = DateToString(RadDatePicker2.DbSelectedDate)
                '         RadDatePicker1.DbSelectedDate
                'RadDatePicker2.DbSelectedDate

                StartPreviousYear = DateToString(New DateTime(Now.AddYears(-1).Year, 1, 1))
                EndPreviousYear = DateToString(New DateTime(Now.AddYears(-1).Year, 12, 31))
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "الرقم الوظيفي"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "الاسم"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "الدرجة الوظيفية"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "الوظيفة"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "الجنسية"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "الجنس"
                    NDT.Columns(6).ColumnName = "عدد أيام الغياب من " & StartPreviousYear & " إلى " & EndPreviousYear
                    NDT.Columns(7).ColumnName = "الخدمة الوطنية من " & FromDate & " إلى " & ToDate
                    NDT.Columns(8).ColumnName = " عدد أيام الغياب من" & FromDate & " إلى " & ToDate
                    NDT.Columns(9).ColumnName = " الساعات الإضافية من" & FromDate & " إلى " & ToDate
                    NDT.Columns(10).ColumnName = "الساعات المهدرة من " & FromDate & " إلى " & ToDate
                    NDT.Columns(11).ColumnName = " إجازة دورية (سنوية) من" & FromDate & " إلى " & ToDate
                    NDT.Columns(12).ColumnName = "إجازة رعاية أبناء من " & FromDate & " إلى " & ToDate
                    NDT.Columns(13).ColumnName = "الإجازة في حالة وفاة أحد الأقارب من " & FromDate & " إلى " & ToDate
                    NDT.Columns(14).ColumnName = "إجازة العدة من " & FromDate & " إلى " & ToDate
                    NDT.Columns(15).ColumnName = " إجازة الحج من" & FromDate & " إلى " & ToDate
                    NDT.Columns(16).ColumnName = "إجازة عارضه من " & FromDate & " إلى " & ToDate
                    NDT.Columns(17).ColumnName = "إجـازة للاشتراك في المحاضرات الدراســـية من" & FromDate & " إلى " & ToDate
                    NDT.Columns(18).ColumnName = " الإجازة الدراسية من" & FromDate & " إلى " & ToDate
                    NDT.Columns(19).ColumnName = "إجازة الوضع من " & FromDate & " إلى " & ToDate
                    NDT.Columns(20).ColumnName = "إجازة مرافقة مريض من " & FromDate & " إلى " & ToDate
                    NDT.Columns(21).ColumnName = " إجازة حضانه من" & FromDate & " إلى " & ToDate
                    NDT.Columns(22).ColumnName = "إجازة تفرغ من " & FromDate & " إلى " & ToDate
                    NDT.Columns(23).ColumnName = " إجازة مرضيه من" & FromDate & " إلى " & ToDate
                    NDT.Columns(24).ColumnName = " إجازة خاصة من" & FromDate & " إلى " & ToDate
                    NDT.Columns(25).ColumnName = "إجازة بدون راتب لمرافقة الزوج بالخارج من " & FromDate & " إلى " & ToDate
                    NDT.Columns(26).ColumnName = "إجازة خاصة بدون راتب لأسباب شخصية من " & FromDate & " إلى " & ToDate
                    NDT.Columns(27).ColumnName = "إجازة إدارية من " & FromDate & " إلى " & ToDate
                    NDT.Columns(28).ColumnName = "إجازات أخرى لدى الجهة من " & FromDate & " إلى " & ToDate
                    NDT.Columns(29).ColumnName = "إعارة من " & FromDate & " إلى " & ToDate
                    NDT.Columns(30).ColumnName = "انتداب من " & FromDate & " إلى " & ToDate
                    NDT.Columns(31).ColumnName = "الأسباب/ملاحظات إن وجدت"
                Else
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Employee No"
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Grade Name"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "Designation Name"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Nationality Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Gender"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Absent Days From " & StartPreviousYear & " To " & EndPreviousYear
                    NDT.Columns(7).ColumnName = "Sum Of National Service Leave Days From " & FromDate & " To " & ToDate
                    NDT.Columns(8).ColumnName = "Absent Days From " & FromDate & " To " & ToDate
                    NDT.Columns(9).ColumnName = "Total Over Time From " & FromDate & " To " & ToDate
                    NDT.Columns(10).ColumnName = "Total Lost Time From" & FromDate & " To " & ToDate
                    NDT.Columns(11).ColumnName = "Sum Of Annual Leave Days From " & FromDate & " To " & ToDate
                    NDT.Columns(12).ColumnName = "Sum Of Accompany Child Leave Days From " & FromDate & " To " & ToDate
                    NDT.Columns(13).ColumnName = "Sum Of Immediate Compassionate Leave Days From " & FromDate & " To " & ToDate
                    NDT.Columns(14).ColumnName = "Sum Of Bereavement (Uddah) Leave Days From " & FromDate & " To " & ToDate
                    NDT.Columns(15).ColumnName = "Sum Of Pilgrimage (Haj) Leave Days From " & FromDate & " To " & ToDate
                    NDT.Columns(16).ColumnName = "Sum Of Emergency Leave Days From " & FromDate & " To " & ToDate
                    NDT.Columns(17).ColumnName = "Sum Of Participate in Academic Lectures Leave Days From " & FromDate & " To " & ToDate
                    NDT.Columns(18).ColumnName = "Sum Of Study Leave Days From " & FromDate & " To " & ToDate
                    NDT.Columns(19).ColumnName = "Sum Of Maternity Leave Days From " & FromDate & " To " & ToDate
                    NDT.Columns(20).ColumnName = "Sum Of Escort Leave Days From " & FromDate & " To " & ToDate
                    NDT.Columns(21).ColumnName = "Sum Of Childcare Leave Days From " & FromDate & " To " & ToDate
                    NDT.Columns(22).ColumnName = "Sum Of SabbaticalLeave Days From " & FromDate & " To " & ToDate
                    NDT.Columns(23).ColumnName = "Sum Of Sick Leave Days From " & FromDate & " To " & ToDate
                    NDT.Columns(24).ColumnName = "Sum Of Private Leave Days From " & FromDate & " To " & ToDate
                    NDT.Columns(25).ColumnName = "Sum Of Unpaid Accompany Spouse Abroad Leave Days From " & FromDate & " To " & ToDate
                    NDT.Columns(26).ColumnName = "Sum Of Unpaid Leave Days From " & FromDate & " To " & ToDate
                    NDT.Columns(27).ColumnName = "Sum Of Administrative Leave Days From " & FromDate & " To " & ToDate
                    NDT.Columns(28).ColumnName = "Sum Of Others Among Leave Days From " & FromDate & " To " & ToDate
                    NDT.Columns(29).ColumnName = "Sum Of Loaning Leave Days From " & FromDate & " To " & ToDate
                    NDT.Columns(30).ColumnName = "Sum Of Mandate Leave Days From " & FromDate & " To " & ToDate
                    NDT.Columns(31).ColumnName = "Remarks"
                End If
            Case EmployeeDisciplineReport


                NDT = DT
                If (Lang = CtlCommon.Lang.AR) Then

                    NDT.Columns.RemoveAt(0)
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الشركة"
                    NDT.Columns(2).ColumnName = "عدد الموظفين"
                    NDT.Columns(3).ColumnName = "مجموع ساعات العمل"
                    NDT.Columns(4).ColumnName = "الوقت الضائع"
                    NDT.Columns(5).ColumnName = "مجموع ساعات الفعلي"
                    NDT.Columns(6).ColumnName = "مجموع ساعات المغادرة"
                    NDT.Columns(7).ColumnName = "%"

                Else
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "No. Of Employees"
                    NDT.Columns(3).ColumnName = "Total Working Hours"
                    NDT.Columns(4).ColumnName = "Lost Time"
                    NDT.Columns(5).ColumnName = "Total Actual Working Hours"
                    NDT.Columns(6).ColumnName = "Permission Hours"
                    NDT.Columns(7).ColumnName = "%"
                End If

            Case SummaryReport_WithInvalid


                NDT = DT
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف باللغة العربية"
                    NDT.Columns(2).ColumnName = "التأخير"
                    NDT.Columns(3).ColumnName = "الخروج المبكر"
                    NDT.Columns(4).ColumnName = "الوقت الاضافي"
                    NDT.Columns(5).ColumnName = "عدد مرات التأخير"
                    NDT.Columns(6).ColumnName = "عدد مرات الخروج المبكر"
                    NDT.Columns(7).ColumnName = "عدد مرات الوقت الاضافي"
                    NDT.Columns(8).ColumnName = "عدد مرات الغياب"
                    NDT.Columns(9).ColumnName = "عدد مرات ايام الاستراحة"

                    NDT.Columns(10).ColumnName = "عدد مرات ايام المغادرة"
                    NDT.Columns(11).ColumnName = "عدد مرات الادخال اليدوي "

                    NDT.Columns(12).ColumnName = "مجموع ساعات العمل"
                    NDT.Columns(13).ColumnName = "عدد مرات ايام الاجازة"
                    NDT.Columns(14).ColumnName = "مجموع الوقت الضائع"
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns(15).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns(16).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns(17).ColumnName = "المغادرات المعلقة"
                    NDT.Columns(18).ColumnName = "الحركات الخاطئة"
                Else
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Delay"
                    NDT.Columns(3).ColumnName = "Early Out"
                    NDT.Columns(4).ColumnName = "OverTime"
                    NDT.Columns(5).ColumnName = "Delay Times"
                    NDT.Columns(6).ColumnName = "Early Out Times"
                    NDT.Columns(7).ColumnName = "OverTime Times"
                    NDT.Columns(8).ColumnName = "Absent Count"
                    NDT.Columns(9).ColumnName = "Rest Count"
                    NDT.Columns(10).ColumnName = "Permission Count"
                    NDT.Columns(11).ColumnName = "Manual Entry Count"
                    NDT.Columns(12).ColumnName = "Total Work Hours"
                    NDT.Columns(13).ColumnName = "Leaves Count"
                    NDT.Columns(14).ColumnName = "Total Lost Time"
                    NDT.Columns(15).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns(16).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns(17).ColumnName = "Pending Permissions"
                    NDT.Columns(18).ColumnName = "Invalid Attempts"
                End If

            Case GradeDeductionReport

                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(19).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 19 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "السنة"
                    NDT.Columns(3).ColumnName = "الشهر"
                    NDT.Columns(4).ColumnName = "عدد مرات الغياب"
                    NDT.Columns(5).ColumnName = "التأخير"
                    NDT.Columns(6).ColumnName = "الخروج المبكر"
                    NDT.Columns(7).ColumnName = "مدة الخروج(حسب الاعدادات)"
                    NDT.Columns(8).ColumnName = "مجموع التأخير و الخروج المبكر"
                    NDT.Columns(9).ColumnName = "عدد مرات التأخير"
                    NDT.Columns(10).ColumnName = "عدد مرات الخروج المبكر"
                    NDT.Columns(11).ColumnName = "عدد مرات التأخير و الخروج المبكر"
                    NDT.Columns(12).ColumnName = "عدد مرات لم يسجل دخول"
                    NDT.Columns(13).ColumnName = "عدد مرات لم يسجل خروج"
                    NDT.Columns(14).ColumnName = "لم يستكمل الدوام الرسمي"
                    NDT.Columns(15).ColumnName = "مجموع الخصومات"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns(16).ColumnName = "تم الاعتماد من قبل"
                    NDT.Columns(17).ColumnName = "تاريخ الاعتماد"
                    NDT.Columns.RemoveAt(18)
                    NDT.Columns.RemoveAt(18)
                    NDT.Columns(18).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns(19).ColumnName = "اسم الشركة"

                Else
                    NDT.Columns(19).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 19 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Employee No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Year"
                    NDT.Columns(3).ColumnName = "Month"
                    NDT.Columns(4).ColumnName = "Absent Count"
                    NDT.Columns(5).ColumnName = "Delay"
                    NDT.Columns(6).ColumnName = "Early Out"
                    NDT.Columns(7).ColumnName = "Out Duration (If Included)"
                    NDT.Columns(8).ColumnName = "Total Delay and Early Out"
                    NDT.Columns(9).ColumnName = "Delay Count"
                    NDT.Columns(10).ColumnName = "Early Out Count"
                    NDT.Columns(11).ColumnName = "Delay and Early Out Count"
                    NDT.Columns(12).ColumnName = "Missing In Count"
                    NDT.Columns(13).ColumnName = "Missing Out Count"
                    NDT.Columns(14).ColumnName = "Uncomplete Work Hours"
                    NDT.Columns(15).ColumnName = "Total All Deductions"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns(16).ColumnName = "Approved By"
                    NDT.Columns(17).ColumnName = "Approval Date"
                    NDT.Columns.RemoveAt(18)
                    NDT.Columns(18).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns(19).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(20)

                End If
            Case WorkHours_ParamReport



            Case SummaryReport_Param
                NDT = DT
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف باللغة العربية"
                    NDT.Columns(2).ColumnName = "التأخير"
                    NDT.Columns(3).ColumnName = "الخروج المبكر"
                    NDT.Columns(4).ColumnName = "الوقت الاضافي"
                    NDT.Columns(5).ColumnName = "عدد مرات التأخير"
                    NDT.Columns(6).ColumnName = "عدد مرات الخروج المبكر"
                    NDT.Columns(7).ColumnName = "عدد مرات الوقت الاضافي"
                    NDT.Columns(8).ColumnName = "عدد مرات الغياب"
                    NDT.Columns(9).ColumnName = "عدد مرات ايام الاستراحة"

                    NDT.Columns(10).ColumnName = "عدد مرات ايام المغادرة"
                    NDT.Columns(11).ColumnName = "عدد مرات الادخال اليدوي "

                    NDT.Columns(12).ColumnName = "مجموع ساعات العمل"
                    NDT.Columns(13).ColumnName = "عدد مرات ايام الاجازة"
                    NDT.Columns(14).ColumnName = "مجموع الوقت الضائع"
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns(15).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns(16).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(17)
                Else
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Delay"
                    NDT.Columns(3).ColumnName = "Early Out"
                    NDT.Columns(4).ColumnName = "OverTime"
                    NDT.Columns(5).ColumnName = "Delay Times"
                    NDT.Columns(6).ColumnName = "Early Out Times"
                    NDT.Columns(7).ColumnName = "OverTime Times"
                    NDT.Columns(8).ColumnName = "Absent Count"
                    NDT.Columns(9).ColumnName = "Rest Count"
                    NDT.Columns(10).ColumnName = "Permission Count"
                    NDT.Columns(11).ColumnName = "Manual Entry Count"
                    NDT.Columns(12).ColumnName = "Total Work Hours"
                    NDT.Columns(13).ColumnName = "Leaves Count"
                    NDT.Columns(14).ColumnName = "Total Lost Time"
                    NDT.Columns(15).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns(16).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                End If

            Case Detailed_Work_Hrs
                NDT.Columns(4).DataType = System.Type.GetType("System.String")
                NDT.Columns(9).DataType = System.Type.GetType("System.String")
                NDT.Columns(15).DataType = System.Type.GetType("System.String")
                For Each row As DataRow In DT.Rows
                    Dim dr As DataRow = NDT.NewRow
                    For i As Integer = 0 To NDT.Columns.Count - 1
                        dr(i) = row(i)
                        If i = 4 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                            End If
                        ElseIf i = 9 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 15 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        End If
                    Next
                    NDT.Rows.Add(dr)
                Next
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "التاريخ"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "وحدة العمل"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "الشركة"
                    NDT.Columns(5).ColumnName = "عدد ساعات العمل الفعلي"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "عدد ساعات العمل"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)

                Else
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Employee No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Date"
                    NDT.Columns(3).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Actual Working Hours"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Total Working Hours"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)

                End If

            Case SummaryWorkDays
                NDT = DT
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "ايام العمل"
                    NDT.Columns(3).ColumnName = "مجموع ساعات العمل"
                    NDT.Columns(4).ColumnName = "مجموع ساعات العمل الاضافي"
                    NDT.Columns(5).ColumnName = "عدد ايام الاجازة"
                    NDT.Columns(6).ColumnName = "مجموع التأخير"
                    NDT.Columns(7).ColumnName = "مجموع الخروج المبكر"
                    NDT.Columns(8).ColumnName = "مجموع ايام الغياب"
                    NDT.Columns(9).ColumnName = "ايام العمل الفعلي"
                    NDT.Columns(10).ColumnName = "مجموع مدة الخروج"
                    NDT.Columns(11).ColumnName = "مجموع ساعات العمل الفعلية"
                    NDT.Columns(12).ColumnName = "مجموع متوسط ساعات العمل"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "اسم الشركة"
                Else
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Employee No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Working Days"
                    NDT.Columns(3).ColumnName = "Total Working Hours"
                    NDT.Columns(4).ColumnName = "Total Overtime"
                    NDT.Columns(5).ColumnName = "Delay"
                    NDT.Columns(6).ColumnName = "Early Out"
                    NDT.Columns(7).ColumnName = "Absent"
                    NDT.Columns(8).ColumnName = "Actual Working Days"
                    NDT.Columns(9).ColumnName = "Out Duration"
                    NDT.Columns(10).ColumnName = "Actual Working Hours"
                    NDT.Columns(11).ColumnName = "Average Working Hours"
                    NDT.Columns(12).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(14)

                End If

            Case SummaryDelayEarlyOut
                NDT = DT
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "عدد مرات التأخير"
                    NDT.Columns(3).ColumnName = "مجموع التأخير"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "عدد مرات الخروج المبكر"
                    NDT.Columns(5).ColumnName = "مجموع الخروج المبكر"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "عدد مرات ايام المغادرة"
                    NDT.Columns(7).ColumnName = "مجموع الوقت الضائع"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "مجموع ساعات العمل"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "اسم الشركة"

                Else
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Employee No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Delay Count"
                    NDT.Columns(3).ColumnName = "Delay Time"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Early Out Count"
                    NDT.Columns(5).ColumnName = "Early Out Time"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Permissions Count"
                    NDT.Columns(7).ColumnName = "Total Lost Time"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Total Work Hours"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(11)
                End If

            Case Summary_ByMonth
                NDT = DT
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "مجموع التأخير"
                    NDT.Columns(3).ColumnName = "عدد مرات التأخير"
                    NDT.Columns(4).ColumnName = "مجموع الخروج المبكر"
                    NDT.Columns(5).ColumnName = "عدد مرات الخروج المبكر"
                    NDT.Columns(6).ColumnName = "مجموع الوقت الضائع"
                    NDT.Columns(7).ColumnName = "عدد مرات الوقت الضائع"
                    NDT.Columns(8).ColumnName = "عدد ايام الغياب"
                    NDT.Columns(9).ColumnName = "مجموع ساعات العمل الكلية"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "السنة"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "الشهر"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)


                    NDT.SetColumnsOrder(New String() {"السنة",
                                   "الشهر"})

                Else
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Employee No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Delay Time"
                    NDT.Columns(3).ColumnName = "Delay Count"
                    NDT.Columns(4).ColumnName = "Early Out Time"
                    NDT.Columns(5).ColumnName = "Early Out Count"
                    NDT.Columns(6).ColumnName = "Out Duration Time"
                    NDT.Columns(7).ColumnName = "Out Duration Count"
                    NDT.Columns(8).ColumnName = "Absent Day(s)"
                    NDT.Columns(9).ColumnName = "Total Work Hours"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Year"
                    NDT.Columns(11).ColumnName = "Month"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)

                    NDT.SetColumnsOrder(New String() {"Year",
                             "Month"})

                End If

            Case DeductionPerPolicy
                NDT = DT
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "السنة"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "الشهر"
                    NDT.Columns(4).ColumnName = "مجموع التأخير"
                    NDT.Columns(5).ColumnName = "مجموع الخروج المبكر"
                    NDT.Columns(6).ColumnName = "مجموع مدة الخروج"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "مجموع الوقت الضائع"
                    NDT.Columns(8).ColumnName = "عدد ايام الاقتطاع"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)

                Else

                    NDT.Columns(0).ColumnName = "Employee No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns.RemoveAt(2)

                    NDT.Columns(2).ColumnName = "Year"
                    NDT.Columns(3).ColumnName = "Month"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Total Delay"
                    NDT.Columns(5).ColumnName = "Total Early Out"
                    NDT.Columns(6).ColumnName = "Total Out Duration"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "Total Lost Time"
                    NDT.Columns(8).ColumnName = "Deduction Days"
                    NDT.Columns(9).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)

                End If

            Case DetailedDeductionPerPolicy

                NDT.Columns(3).DataType = System.Type.GetType("System.String")
                For Each row As DataRow In DT.Rows
                    Dim dr As DataRow = NDT.NewRow
                    For i As Integer = 0 To NDT.Columns.Count - 1
                        dr(i) = row(i)
                        If i = 3 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                            End If
                        End If
                    Next
                    NDT.Rows.Add(dr)
                Next
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "التاريخ"
                    NDT.Columns(3).ColumnName = "التأخير"
                    NDT.Columns(4).ColumnName = "الخروج المبكر"
                    NDT.Columns(5).ColumnName = "مدة الخروج"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "الوقت الضائع"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "ايام الاقتطاع"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "المسمى الوظيفي"

                    NDT.SetColumnsOrder(New String() {"رقم الموظف",
                                  "اسم الموظف",
                                  "المسمى الوظيفي",
                                 "التاريخ",
                                 "التأخير",
                                 "الخروج المبكر",
                                 "الوقت الضائع",
                                 "ايام الاقتطاع",
                                 "اسم وحدة العمل",
                                 "اسم الشركة"
                    })

                Else
                    NDT.Columns(0).ColumnName = "Employee No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Date"
                    NDT.Columns(3).ColumnName = "Delay"
                    NDT.Columns(4).ColumnName = "Early Out"
                    NDT.Columns(5).ColumnName = "Out Duration"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Lost Time"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "Deduction Days"
                    NDT.Columns(8).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Designation Name"
                    NDT.Columns.RemoveAt(11)

                    NDT.SetColumnsOrder(New String() {"Employee No.",
                        "Employee Name",
                        "Designation Name",
                        "Date",
                        "Delay",
                        "Early Out",
                        "Out Duration",
                        "Lost Time",
                        "Deduction Days",
                        "Entity Name",
                        "Company Name"
                    })

                End If

            Case LeavesSummaryReports
                NDT = DT
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(0).ColumnName = "م"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "اسم الموظف"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "اسم الشركة"
                    NDT.Columns(5).ColumnName = "السنوية تكرار"
                    NDT.Columns(6).ColumnName = "السنوية عدد الايام"
                    NDT.Columns(7).ColumnName = "العارضة تكرار"
                    NDT.Columns(8).ColumnName = "العارضة عدد الايام"
                    NDT.Columns(9).ColumnName = "المرضية القصيرة تكرار"
                    NDT.Columns(10).ColumnName = "المرضية القصيرة عدد الايام"
                    NDT.Columns(11).ColumnName = "المرضية الطويلة تكرار"
                    NDT.Columns(12).ColumnName = "المرضية الطويلة عدد الايام"
                    NDT.Columns(13).ColumnName = "مجموع المرضيات"
                    NDT.Columns(14).ColumnName = "التعويضية او الادارية"
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns(15).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(16)
                    NDT.SetColumnsOrder(New String() {"م",
                     "رقم الموظف",
                     "اسم الموظف",
                     "اسم وحدة العمل",
                     "اسم الشركة",
                     "السنوية عدد الايام",
                     "السنوية تكرار",
                     "العارضة عدد الايام",
                     "العارضة تكرار",
                     "المرضية القصيرة عدد الايام",
                     "المرضية القصيرة تكرار",
                     "المرضية الطويلة عدد الايام",
                     "المرضية الطويلة تكرار",
                     "مجموع المرضيات",
                     "التعويضية او الادارية",
                     "الملاحظات"
                 })

                Else
                    NDT.Columns(0).ColumnName = "#"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "Employee No."
                    NDT.Columns(2).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Annual Count"
                    NDT.Columns(6).ColumnName = "Annual Sum"
                    NDT.Columns(7).ColumnName = "Emergency Count"
                    NDT.Columns(8).ColumnName = "Emergency Sum"
                    NDT.Columns(9).ColumnName = "Short Sick Leave Count"
                    NDT.Columns(10).ColumnName = "Short Sick Leave Sum"
                    NDT.Columns(11).ColumnName = "Long Sick Leave Count"
                    NDT.Columns(12).ColumnName = "Long Sick Leave Sum"
                    NDT.Columns(13).ColumnName = "Total Sick"
                    NDT.Columns(14).ColumnName = "Administrative or Compensation"
                    NDT.Columns(15).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.SetColumnsOrder(New String() {"#",
                    "Employee No.",
                    "Employee Name",
                    "Entity Name",
                    "Company Name",
                    "Annual Sum",
                    "Annual Count",
                    "Emergency Sum",
                    "Emergency Count",
                    "Short Sick Leave Sum",
                    "Short Sick Leave Count",
                    "Long Sick Leave Sum",
                    "Long Sick Leave Count",
                    "Total Sick",
                    "Administrative or Compensation",
                    "Remarks"
                })
                End If

        End Select
    End Sub

    Private Sub FillReports(ByVal formID As Integer, ByVal groupID As Integer)

        CtlCommon.FillTelerikDropDownList(RadComboExtraReports, objsysforms.GetFormsByParentID(formID, groupID), Lang)

    End Sub

    Public Sub ClearFilter()
        EmployeeFilter1.ClearValues()

    End Sub

    Private Sub FillGrades()
        objEmp_Grade = New Emp_Grade
        With objEmp_Grade
            CtlCommon.FillTelerikDropDownList(ddlGrades, .GetAll, Lang)
        End With
    End Sub


#End Region

End Class
