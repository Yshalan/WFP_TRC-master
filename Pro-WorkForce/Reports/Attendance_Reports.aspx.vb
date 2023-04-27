Imports System.Data
Imports System.Resources
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports SmartV.UTILITIES
Imports SmartV.UTILITIES.CtlCommon
Imports TA.Admin
Imports TA.Definitions
Imports TA.Reports
Imports TA.Security

Partial Class Reports_SelfServices_AttendanceReports
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
    Private objSYSUsers As SYSUsers
    Private objAuthority As Authorities


    Private DailyReport As String = "Daily Report"
    Private DailyReportWithCounts As String = "Daily Report With Counts"
    Private AttendanceTransactionsReport As String = "Attendance Transactions Report"
    Private AttendanceTransactionsWithTemperatureReport As String = "Attendance Transactions With Temperature Report"
    Private AbsentEmployeesReport As String = "Absent Employees Report"
    Private InOutTransactionsReport As String = "In Out Transactions Report"
    Private InvalidAttempts As String = "Invalid Attempts"
    Private DetailedTransactions As String = "Detailed Transactions"
    Private DelayReport As String = "Delay Report"
    Private EarlyOutReport As String = "Early Out Report"
    Private IncompleteTransactions As String = "Incomplete Transactions"
    Private DelayandEarlyOut As String = "Delay and EarlyOut"
    Private ManualEntryRepor As String = "ManualEntryRepor"
    Private EmployeeTimeAttendance2 As String = "Employee Time & Attendance 2"
    Private DailyReport2 As String = "DailyReport2"
    Private DailyReport3 As String = "Daily Report 3"
    Private GatesReport As String = "Gates Report"
    Private AuthorityMovements As String = "Authorities Movements Report"
    Private DetailedTransactions2 As String = "Detailed Transactions 2"
    Private DailyReport4 As String = "Daily Report4"
    Private AbsentEmployee_CountReport As String = "AbsentEmployee_CountReport"
    Private EmployeeAbsent_Period As String = "EmployeeAbsent_Period"
    Private DailyReport_Email As String = "DailyReport_Email"
    Private DetailedTransactions_WithAllowance As String = "DetailedTransactions_WithAllowance"
    Private Weekly_Report As String = "Weekly_Report"
    Private DailyReport_Bucket As String = "DailyReport_Bucket"
    Private DailyReport_Invalid As String = "DailyReport_Invalid"
    Private DetailedTransactions_Invalid As String = "DetailedTransactions_Invalid"
    Private AbsentEmployeeReport_WithParam As String = "AbsentEmployeeReport_WithParam"
    Private IncompleteTransactions_Advance As String = "IncompleteTransactions_Advance"
    Private AttendnaceReport_ADASI As String = "AttendnaceReport_ADASI"
    Private Detailed_Transaction_And_Accomodation_Transactions_Report As String = "Detailed Accomodation Transactions Report"
    Private EmployeeOutDuration As String = "EmployeeOutDuration"
    Private EmpDetailedTimeAttendance As String = "EmpDetailedTimeAttendance"
    Private DailyReport_TAReason As String = "DailyReport_TAReason"
    Private EmpMove_OpenSchedule As String = "EmpMove_OpenSchedule"
    Private FCA_TransactionHistory As String = "FCA_TransactionHistory"
    Private Mobile_InOut_Transactions As String = "Mobile In Out Transactions"

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

    Public Property SubDT() As DataTable
        Get
            Return ViewState("SubDT")
        End Get
        Set(ByVal value As DataTable)
            ViewState("SubDT") = value
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
        Session("EmployeeStatus") = rblEmpStatus.Text 'ID: M02 || Date: 25-04-2023 || By: Yahia shalan || Description: Storing employee status (1 or 2 >> Active or Inactive)'
        If SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
        Else
            Lang = CtlCommon.Lang.EN
        End If
        lblAuthorityType.Text = IIf(Lang = CtlCommon.Lang.AR, "اسم الجهة", "Authority Name")
        btnClear.Text = IIf(Lang = CtlCommon.Lang.AR, "مسح", "Clear")
        lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقارير الحضور والانصراف", "Attendance Reports")
        'btnPrint.Text = IIf(Lang = CtlCommon.Lang.AR, "طباعة", "Print")
        btnPrint.Text = IIf(Lang = CtlCommon.Lang.AR, "انشاء", "Generate")
        rblFilter.Items(0).Text = IIf(Lang = CtlCommon.Lang.AR, "جميع التأخير و الخروج المبكر", "All Delays and Early out")
        rblFilter.Items(1).Text = IIf(Lang = CtlCommon.Lang.AR, "التأخير او الخروج المبكر في اليوم >=", "Delay or Early out per day >=")
        rblFilter.Items(2).Text = IIf(Lang = CtlCommon.Lang.AR, "مجموع التأخير او الخروج المبكر >=", "Total Delay or Early out >=")
        rblFilter.Items(3).Text = IIf(Lang = CtlCommon.Lang.AR, "مجموع عدد مرات التأخير او الخروج المبكر >=", "Total no of times Delay or Early out >=")
        rblEmpStatus.Items(0).Text = IIf(Lang = CtlCommon.Lang.AR, "فعال", "Active") 'ID: M03 || Date: 25-04-2023 || By: Yahia shalan || Description: Set the languag for the radio button'
        rblEmpStatus.Items(1).Text = IIf(Lang = CtlCommon.Lang.AR, "غير فعال", "InActive") 'ID: M03 || Date: 25-04-2023 || By: Yahia shalan || Description: Set the languag for the radio button'

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

            FillReports(formID, SessionVariables.LoginUser.GroupId)
            FillAuthorityType()


            RadDatePicker1.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd As New Date
            dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)

            RadDatePicker2.SelectedDate = dd
            rmtTotalDelayEarly.TextWithLiterals = "0000"

            objAPP_Settings = New APP_Settings
            With objAPP_Settings
                .GetByPK()
                rblFormat.SelectedValue = .DefaultReportFormat

            End With
        End If

    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload

        cryRpt.Close()
        cryRpt.Dispose()
        GC.Collect()
    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim reportName As String = RadComboAttendanceReports.SelectedValue

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

    Protected Sub rblFilter_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rblFilter.SelectedIndexChanged
        If rblFilter.SelectedValue = 2 Or rblFilter.SelectedValue = 3 Then
            trTotalDelayEarly.Visible = True
            trTotalCount.Visible = False
            txtTotalCount.Text = String.Empty
        ElseIf rblFilter.SelectedValue = 4 Then
            trTotalCount.Visible = True
            trTotalDelayEarly.Visible = False
            rmtTotalDelayEarly.TextWithLiterals = "0000"
        Else
            trTotalDelayEarly.Visible = False
            trTotalCount.Visible = False
            rmtTotalDelayEarly.TextWithLiterals = "0000"
            txtTotalCount.Text = String.Empty
        End If
    End Sub

    Protected Sub RadComboAttendanceReports_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboAttendanceReports.SelectedIndexChanged
        If RadComboAttendanceReports.SelectedValue = DelayandEarlyOut Then
            trDelayAndEarlyOut.Visible = True
            trAuthorityType.Visible = False
            dvLogicalAbsent.Visible = False
            trTotalDelayEarly.Visible = False
            dvAbsentParam.Visible = False
            dvIncomplete_Transactions_Advance.Visible = False

            RadDatePicker1.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd As New Date
            dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            RadDatePicker2.SelectedDate = dd
        ElseIf RadComboAttendanceReports.SelectedValue = AuthorityMovements Then
            trAuthorityType.Visible = True
            trDelayAndEarlyOut.Visible = False
            dvLogicalAbsent.Visible = False
            trTotalDelayEarly.Visible = False
            dvAbsentParam.Visible = False
            dvIncomplete_Transactions_Advance.Visible = False

            RadDatePicker1.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd As New Date
            dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            RadDatePicker2.SelectedDate = dd
        ElseIf RadComboAttendanceReports.SelectedValue = AbsentEmployeesReport Then
            dvLogicalAbsent.Visible = True
            RadDatePicker1.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd As New Date
            dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            RadDatePicker2.SelectedDate = dd
            trDelayAndEarlyOut.Visible = False
            trTotalDelayEarly.Visible = False
            trAuthorityType.Visible = False
            dvAbsentParam.Visible = False
            dvIncomplete_Transactions_Advance.Visible = False

        ElseIf RadComboAttendanceReports.SelectedValue = DailyReport Or RadComboAttendanceReports.SelectedValue = DailyReport2 Or RadComboAttendanceReports.SelectedValue = DailyReport3 Or RadComboAttendanceReports.SelectedValue = DailyReport4 Or RadComboAttendanceReports.SelectedValue = DailyReportWithCounts Or RadComboAttendanceReports.SelectedValue = DailyReport_Email Or RadComboAttendanceReports.SelectedValue = DailyReport_Bucket Or RadComboAttendanceReports.SelectedValue = DailyReport_Invalid Or RadComboAttendanceReports.SelectedValue = AttendnaceReport_ADASI Or RadComboAttendanceReports.SelectedValue = DailyReport_TAReason Then
            objAPP_Settings = New APP_Settings
            objAPP_Settings.GetByPK()
            If objAPP_Settings.DailyReportDate = 1 Then
                RadDatePicker1.SelectedDate = Date.Today
                RadDatePicker2.SelectedDate = Date.Today
            End If
            trDelayAndEarlyOut.Visible = False
            trTotalDelayEarly.Visible = False
            trAuthorityType.Visible = False
            dvLogicalAbsent.Visible = False
            dvAbsentParam.Visible = False
            dvIncomplete_Transactions_Advance.Visible = False

        ElseIf RadComboAttendanceReports.SelectedValue = AbsentEmployeeReport_WithParam Then
            dvAbsentParam.Visible = True
            trDelayAndEarlyOut.Visible = False
            trTotalDelayEarly.Visible = False
            trAuthorityType.Visible = False
            dvLogicalAbsent.Visible = False
            dvIncomplete_Transactions_Advance.Visible = False
            RadDatePicker1.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd As New Date
            dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            RadDatePicker2.SelectedDate = dd

        ElseIf RadComboAttendanceReports.SelectedValue = IncompleteTransactions_Advance Then
            dvIncomplete_Transactions_Advance.Visible = True
            dvAbsentParam.Visible = False
            trDelayAndEarlyOut.Visible = False
            trTotalDelayEarly.Visible = False
            trAuthorityType.Visible = False
            dvLogicalAbsent.Visible = False
            RadDatePicker1.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd As New Date
            dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            RadDatePicker2.SelectedDate = dd
        ElseIf RadComboAttendanceReports.SelectedValue = IncompleteTransactions Then
            Div_Incomplete_Transaction_Status.Visible = True
        Else
            dvIncomplete_Transactions_Advance.Visible = False
            trDelayAndEarlyOut.Visible = False
            trTotalDelayEarly.Visible = False
            trAuthorityType.Visible = False
            dvLogicalAbsent.Visible = False
            dvAbsentParam.Visible = False
            RadDatePicker1.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd As New Date
            dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            RadDatePicker2.SelectedDate = dd
        End If

    End Sub

    Protected Sub btnClear_Click(sender As Object, e As System.EventArgs) Handles btnClear.Click
        ClearFilter()
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
        If Not RadComboAttendanceReports.SelectedValue = "-1" Then
            SetReportName(reportName)

            cryRpt = New ReportDocument

            cryRpt.Load(Server.MapPath(RPTName))
            cryRpt.SetDataSource(DT)
            Dim objAPP_Settings As New APP_Settings
            objAPP_Settings.FK_CompanyId = EmployeeFilter1.CompanyId
            dt2 = objAPP_Settings.GetHeader()
            dt2.Columns.Add("From_Date")
            dt2.Columns.Add("To_Date")
            dt2.Rows(0).Item("From_Date") = DateToString(RadDatePicker1.SelectedDate)
            dt2.Rows(0).Item("To_Date") = DateToString(RadDatePicker2.SelectedDate)
            cryRpt.Subreports("rptHeader").SetDataSource(dt2)

            If RadComboAttendanceReports.SelectedValue = DetailedTransactions_WithAllowance Then
                cryRpt.Subreports("Sub_LeavesSummary").SetDataSource(SubDT)
                If EmployeeFilter1.EmployeeId = 0 Then
                    cryRpt.SetParameterValue("@FilteredByEntity", True)
                Else
                    cryRpt.SetParameterValue("@FilteredByEntity", False)
                End If
                'Else
                '    cryRpt.Subreports(cryRpt.Subreports().Item(0).Name).SetDataSource(dt2)
            End If
            'cryRpt.Subreports(cryRpt.Subreports().Item("rptHeader").Name).SetDataSource(dt2)
            cryRpt.SetParameterValue("@UserName", UserName)

            'If Request.QueryString("ReportId") = 11 Then
            '    cryRpt.SetParameterValue("@URL", BaseSiteUrl)
            'ElseIf Request.QueryString("ReportId") = 12 Then
            '    cryRpt.SetParameterValue("@URL", BaseSiteUrl)
            'End If
            If reportName = InvalidAttempts Then
                cryRpt.SetParameterValue("@URL", BaseSiteUrl)
            End If

            If RadComboAttendanceReports.SelectedValue = DailyReport Or RadComboAttendanceReports.SelectedValue = DailyReport2 Or RadComboAttendanceReports.SelectedValue = DailyReport3 Or RadComboAttendanceReports.SelectedValue = DailyReport4 Or RadComboAttendanceReports.SelectedValue = DailyReportWithCounts Or RadComboAttendanceReports.SelectedValue = DailyReport_Email Or RadComboAttendanceReports.SelectedValue = DailyReport_Bucket Or RadComboAttendanceReports.SelectedValue = DailyReport_Invalid Or RadComboAttendanceReports.SelectedValue = DailyReport_TAReason Or RadComboAttendanceReports.SelectedValue = EmpMove_OpenSchedule Then
                objAPP_Settings.GetByPK()
                cryRpt.SetParameterValue("@TransactionConsideration", objAPP_Settings.ConsequenceTransactions)
            End If


            Dim ShowSTLogo As Boolean
            ShowSTLogo = ConfigurationManager.AppSettings("ShowSmartTimeLogo")
            cryRpt.SetParameterValue("@ShowSTLogo", False)
            cryRpt.SetParameterValue("@ShowSTLogo", False, "rptHeader")
            cryRpt.SetParameterValue("@ReportName", RadComboAttendanceReports.SelectedItem.Text)
            cryRpt.SetParameterValue("@ReportName", RadComboAttendanceReports.SelectedItem.Text, "rptHeader")
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



            If RadComboAttendanceReports.SelectedValue = IncompleteTransactions_Advance Then
                Dim strSelection As String = ""
                If Not rblMissingCount.SelectedValue = Nothing Then
                    strSelection = rblMissingCount.SelectedItem.Text
                End If

                cryRpt.SetParameterValue("@rptIncompleteTransSelection", strSelection)
                cryRpt.SetParameterValue("@rptIncompleteTransVal", IIf(radnumMissingCountVal.Text = String.Empty, "", radnumMissingCountVal.Text))

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
            checkparams()
            CRV.ReportSource = cryRpt
            If Not DT Is Nothing Then
                If DT.Rows.Count > 0 Then
                    If rblFormat.SelectedValue = 1 Then
                        cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedReport")
                    ElseIf rblFormat.SelectedValue = 2 Then
                        cryRpt.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, True, "ExportedReport")
                    ElseIf rblFormat.SelectedValue = 3 Then
                        FillExcelreport(reportName)
                        'ExportDataSetToExcel(NDT, "ExportedReport")
                        ExportDataSetToExcel(NDT, "ExportedReport", HeadDT)
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

        If EmployeeFilter1.ShowDirectStaffCheck = True Then
            rptObj.DirectStaffOnly = EmployeeFilter1.DirectStaffOnly
        Else
            rptObj.DirectStaffOnly = False
        End If

        If rpttid = DelayandEarlyOut Then
            rptObj.Type = rblFilter.SelectedValue
            Dim strTotal As String = (CInt(rmtTotalDelayEarly.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtTotalDelayEarly.TextWithLiterals.Split(":")(1))
            rptObj.TotalDelayEarlyOut = strTotal

            If Not txtTotalCount.Text = Nothing Then
                rptObj.TotalCount = txtTotalCount.Text
            End If
        End If
        If rpttid = AuthorityMovements Then
            If Not radcomboAuthorityType.SelectedValue = -1 Then
                rptObj.AuthorityTypeId = radcomboAuthorityType.SelectedValue
            Else
                rptObj.AuthorityTypeId = Nothing
            End If
        End If
        If RadComboAttendanceReports.SelectedValue = AbsentEmployeesReport Then
            rptObj.IncludeLogicalAbsent = chkLogicalAbsent.Checked
        End If

        If RadComboAttendanceReports.SelectedValue = AbsentEmployeeReport_WithParam Then
            rptObj.Absent_Count = IIf(radnumAbsentNum.Text = String.Empty, 0, radnumAbsentNum.Text)
            rptObj.Absent_Count_Selection = IIf(rblAbsentPolicy.SelectedValue = "", Nothing, rblAbsentPolicy.SelectedValue)
        End If

        If RadComboAttendanceReports.SelectedValue = IncompleteTransactions_Advance Then
            rptObj.IncompleteTransSelection = rblMissingCount.SelectedValue
            rptObj.IncompleteTransVal = IIf(radnumMissingCountVal.Text = String.Empty, 0, radnumMissingCountVal.Text)
        End If

        ' Added For Color Report 
        Dim IsDailyReportWithColor As Boolean
        objAPP_Settings = New APP_Settings
        With objAPP_Settings
            .GetByPK()
            IsDailyReportWithColor = .IsDailyReportWithColor
        End With

        'rpttid = IIf(rpttid = 0, 3, rpttid)
        Select Case rpttid
            Case DailyReport
                If (IsDailyReportWithColor) Then
                    RPTName = "rptEmpMoveColored.rpt"
                    'RPTName = "CrystalReport.rpt"
                Else
                    RPTName = "rptEmpMove.rpt"
                End If
                DT = rptObj.GetFilterdEmpMove()
                'Case 2 'Employee list
                '    RPTName = "rptEmployeeList.rpt"
                '    DT = rptObj.GetFilterdEmpList()
            Case DailyReportWithCounts
                RPTName = "rptDailyReportWithCounts.rpt"
                DT = rptObj.GetFilterdEmpMove()
            Case DetailedTransactions
                If (IsDailyReportWithColor) Then
                    RPTName = "rptDetailedTransactionsColored.rpt"
                Else
                    RPTName = "rptDetailedTransactions.rpt"
                End If
                DT = rptObj.GetDetailed_Transactions()
            Case InOutTransactionsReport
                RPTName = "rptEmpInOut.rpt"
                DT = rptObj.GetEmpInOut()

            Case EmployeeTimeAttendance2
                RPTName = "rptEmpTimeAttendance_.rpt"
                DT = rptObj.GetEmpTimeAttendance()
            Case AbsentEmployeesReport
                RPTName = "rptEmpAbsent.rpt"
                DT = rptObj.GetEmpAbsent()
            Case InvalidAttempts
                RPTName = "rptEmp_InvalidAttempts.rpt"
                DT = rptObj.GetEmp_InvalidAttempts()
            Case DelayReport
                RPTName = "rptEmp_Delay.rpt"
                DT = rptObj.GetDelay()
            Case EarlyOutReport
                RPTName = "rptEmp_EarlyOut.rpt"
                DT = rptObj.GetEarlyOut()
            Case IncompleteTransactions
                RPTName = "rptEmpIncompleteTrans.rpt"

                If RadioButton_Incomplete_transaction.SelectedValue = 3 Then
                    DT = rptObj.Get_IncompleteTrans()
                ElseIf RadioButton_Incomplete_transaction.SelectedValue = 2 Then
                    DT = rptObj.Get_IncompleteTrans()

                    If DT.Rows.Count > 0 Then 'ID: M01 || Date: 12-04-2023 || By: Yahia shalan || Description: Check If there's data returned from DB to avoid System.InvalidOperationException'
                        Dim rows As DataRow()
                        rows = DT.Select("OUT_TIME > '1900-01-01'")
                        If rows.Length > 0 Then 'ID: M01 || Date: 12-04-2023 || By: Yahia shalan || Description: Check If there's data returned from DB to avoid System.InvalidOperationException'
                            DT = rows.CopyToDataTable()
                        Else
                            DT = Nothing
                        End If
                    Else
                        DT = rptObj.Get_IncompleteTrans()
                    End If

                ElseIf RadioButton_Incomplete_transaction.SelectedValue = 1 Then
                    DT = rptObj.Get_IncompleteTrans()
                    If DT.Rows.Count > 0 Then 'ID: M01 || Date: 12-04-2023 || By: Yahia shalan || Description: Check If there's data returned from DB to avoid System.InvalidOperationException'
                        Dim rows As DataRow()
                        rows = DT.[Select]("IN_TIME > '1900-01-01'")
                        If rows.Length > 0 Then 'ID: M01 || Date: 12-04-2023 || By: Yahia shalan || Description: Check If there's data returned from DB to avoid System.InvalidOperationException'
                            DT = rows.CopyToDataTable()
                        Else
                            DT = Nothing
                        End If


                    End If

                End If



            Case DelayandEarlyOut
                RPTName = "rptDelayEarlyOut.rpt"
                DT = rptObj.Get_DelayAndEarlyOut()
            Case ManualEntryRepor
                RPTName = "rptManualEntry.rpt"
                DT = rptObj.GetManaualEntry()
            Case AttendanceTransactionsReport
                RPTName = "rptDetEmpMove.rpt"
                DT = rptObj.GetFilterdDetEmpMove()
            Case AttendanceTransactionsWithTemperatureReport
                RPTName = "rptDetEmpMove_WithTemperature.rpt"
                DT = rptObj.GetFilterdDetEmpMove()
            Case DailyReport2
                RPTName = "DailyReport2.rpt"
                DT = rptObj.GetFilterdEmpMove()
            Case DailyReport3
                RPTName = "DailyReport3.rpt"
                DT = rptObj.GetDailyReport3
            Case GatesReport
                RPTName = "rptGatesReport.rpt"
                DT = rptObj.GetGatesReport
            Case AuthorityMovements
                RPTName = "rptAuthorityMovements.rpt"
                DT = rptObj.GetAuthorityMovementsReport

            Case DetailedTransactions2
                RPTName = "rptDetailedTransactions2.rpt"
                DT = rptObj.GetDetailed_Transactions2

            Case DailyReport4
                RPTName = "rptEmpMove4.rpt"
                DT = rptObj.GetFilterdEmpMove()

            Case AbsentEmployee_CountReport
                RPTName = "rptEmpAbsentCount.rpt"
                DT = rptObj.GetEmpAbsent_Count()

            Case EmployeeAbsent_Period
                RPTName = "rptEmpAbsentPeriod.rpt"
                DT = rptObj.Get_Absent_Period()
            Case DailyReport_Email
                If (IsDailyReportWithColor) Then
                    RPTName = "rptEmpMoveColored_Email.rpt"
                Else
                    RPTName = "rptEmpMove_Email.rpt"
                End If
                DT = rptObj.GetFilterdEmpMove()
            Case DetailedTransactions_WithAllowance
                If (IsDailyReportWithColor) Then
                    RPTName = "rptDetailedTransactionsColored_WithMonthlyAllowance.rpt"
                Else
                    RPTName = "rptDetailedTransactions_WithMonthlyAllowance.rpt"
                End If
                DT = rptObj.GetDetailed_Transactions_With_MonthlyAllowance()
                SubDT = rptObj.GetSub_LeavesDetails
            Case Weekly_Report
                If (IsDailyReportWithColor) Then
                    RPTName = "rptEmpMove_FallOffColored.rpt"
                Else
                    RPTName = "rptEmpMove_FallOff.rpt"
                End If
                DT = rptObj.GetFilterdEmpMove_FallOff
            Case DailyReport_Bucket
                If (IsDailyReportWithColor) Then
                    RPTName = "rptEmpMoveColored_Bucket.rpt"
                Else
                    RPTName = "rptEmpMove_Bucket.rpt"
                End If
                DT = rptObj.GetFilterdEmpMove_Bucket
            Case DailyReport_Invalid
                If (IsDailyReportWithColor) Then
                    RPTName = "rptEmpMoveColored_Invalid.rpt"
                Else
                    RPTName = "rptEmpMove_Invalid.rpt"
                End If
                DT = rptObj.GetFilterdEmpMove_Invalid
            Case DetailedTransactions_Invalid
                If (IsDailyReportWithColor) Then
                    RPTName = "rptDetailedTransactionsColored_Invalid.rpt"
                Else
                    RPTName = "rptDetailedTransactions_Invalid.rpt"
                End If
                DT = rptObj.GetDetailed_Transactions_Invalid
            Case AbsentEmployeeReport_WithParam
                RPTName = "rptEmpAbsent_Param.rpt"
                DT = rptObj.GetEmpAbsent_Param
            Case IncompleteTransactions_Advance
                RPTName = "rptEmpIncompleteTrans_Advance.rpt"
                DT = rptObj.Get_IncompleteTrans_Advance
            Case AttendnaceReport_ADASI
                RPTName = "rptEmpMove_ADASI.rpt"
                DT = rptObj.GetFilterdEmpMove_ADASI()
            Case Detailed_Transaction_And_Accomodation_Transactions_Report
                RPTName = "rpt_DetailedAccomodationTransactions.rpt"
                DT = rptObj.GetDetailed_Transactions_AccomodationTransactions()
            Case EmployeeOutDuration
                RPTName = "rpt_EmpOutDuration.rpt"
                DT = rptObj.Get_EmpOutDuration()
            Case EmpDetailedTimeAttendance
                RPTName = "rptEmpDetailedTimeAttendance.rpt"
                DT = rptObj.GetEmpDetailedTimeAttendance()
            Case DailyReport_TAReason
                If (IsDailyReportWithColor) Then
                    RPTName = "rptEmpMoveColored_TAReason.rpt"
                Else
                    RPTName = "rptEmpMove_TAReason.rpt"
                End If
                DT = rptObj.GetFilterdEmpMove()
            Case EmpMove_OpenSchedule
                RPTName = "rptEmpMove_OpenSchedule.rpt"
                DT = rptObj.Get_EmpMove_OpenSchedule()

            Case FCA_TransactionHistory
                RPTName = "rptFCA_TransactionsHitory.rpt"
                DT = rptObj.Get_FCA_TransactionHistory()

            Case Mobile_InOut_Transactions
                RPTName = "rptEmp_Mobile_InOut.rpt"
                DT = rptObj.Get_Emp_MoveInOut_MobileTransactions

        End Select

        UserName = SessionVariables.LoginUser.UsrID
        ' Version = System.Configuration.ConfigurationManager.AppSettings("Version").ToString()

        If (Lang = CtlCommon.Lang.AR) Then
            RPTName = "Arabic/" + RPTName
        Else
            RPTName = "English/" + RPTName
        End If

    End Sub

    Private Sub FillReports(ByVal formID As Integer, ByVal groupID As Integer)

        CtlCommon.FillTelerikDropDownList(RadComboAttendanceReports, objsysforms.GetFormsByParentID(formID, groupID), Lang)

    End Sub

    Public Sub ClearFilter()
        EmployeeFilter1.ClearValues()

    End Sub

    Private Sub FillAuthorityType()
        Dim objAuthority As New Authorities
        CtlCommon.FillTelerikDropDownList(radcomboAuthorityType, objAuthority.GetAll, Lang)
    End Sub

    Private Sub checkparams()
        Dim ParCount As Integer = cryRpt.DataDefinition.ParameterFields.Count

        For Each crParamField As CrystalDecisions.CrystalReports.Engine.ParameterFieldDefinition In cryRpt.DataDefinition.ParameterFields
            Dim x As String = ""
            If crParamField.HasCurrentValue = False Then
                x = crParamField.Name
            End If
        Next
    End Sub

    Private Sub FillExcelreport(ByVal ReportName As String)
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
        HeadDT.Rows.Add(3)
        Dim rptName As String = RadComboAttendanceReports.SelectedValue
        Dim dtFormName As DataTable
        dtFormName = objsysforms.GetBy_FormName(rptName)
        If Lang = CtlCommon.Lang.AR Then

            HeadDT(0)(0) = dtFormName(0)("Desc_Ar")
            '------Column(0)-----------
            HeadDT(1)(1) = "طبع بواسطة:"
            HeadDT(2)(1) = "تاريخ الطباعة:"
            HeadDT(3)(1) = "من تاريخ: " & Convert.ToDateTime(RadDatePicker1.SelectedDate).ToString("dd/MM/yyyy")
            '------Column(1)-----------
            HeadDT(1)(0) = SessionVariables.LoginUser.UsrID
            HeadDT(2)(0) = Date.Today.ToString("dd/MM/yyyy")
            HeadDT(3)(0) = "الى تاريخ: " & Convert.ToDateTime(RadDatePicker2.SelectedDate).ToString("dd/MM/yyyy")

        Else
            HeadDT(0)(0) = dtFormName(0)("Desc_En").ToString
            '------Column(0)-----------
            HeadDT(1)(0) = "Printed By:"
            HeadDT(2)(0) = "Printing Date:"
            HeadDT(3)(0) = "From Date: " & Convert.ToDateTime(RadDatePicker1.SelectedDate).ToString("dd/MM/yyyy")
            '------Column(1)-----------
            HeadDT(1)(1) = SessionVariables.LoginUser.UsrID
            HeadDT(2)(1) = Date.Today.ToString("dd/MM/yyyy")
            HeadDT(3)(1) = "To Date: " & Convert.ToDateTime(RadDatePicker2.SelectedDate).ToString("dd/MM/yyyy")
        End If

        HeadDT.Rows.Add(drFormName)
        '------------------ADD REPORT HEADER-----------------

        Select Case ReportName
            Case DailyReport
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    NDT.Columns(23).DataType = System.Type.GetType("System.String")
                    NDT.Columns(25).DataType = System.Type.GetType("System.String")
                    NDT.Columns(27).DataType = System.Type.GetType("System.String")
                    NDT.Columns(35).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 20 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = row(i).ToString()
                                End If
                            ElseIf i = 25 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 27 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 35 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 30 Then '---HolidayArabicName Column
                                If dr(19).ToString = "H" Then
                                    dr(21) = dr(i).ToString
                                    dr(23) = dr(i).ToString
                                End If
                            ElseIf i = 32 Then '---LeaveArabicName Column
                                If dr(19).ToString = "L" Then
                                    dr(21) = dr(i).ToString
                                    dr(23) = dr(i).ToString
                                End If
                            ElseIf i = 34 Then '---PermissionArabicName Column
                                If dr(19).ToString = "P" Then
                                    dr(21) = dr(i).ToString
                                    dr(23) = dr(i).ToString
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
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "اسم الجدول"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "التاريخ"
                    NDT.Columns(6).ColumnName = "اول دخول"
                    NDT.Columns(7).ColumnName = "اخر خروج"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "التأخير"
                    NDT.Columns(9).ColumnName = "الخروج المبكر"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "المدة"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "الوقت الضائع"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "الوقت الاضافي"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "موقع العمل"
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns(15).ColumnName = "طلب اجازة"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns(16).ColumnName = "طلب مغادرة"
                    'Day Description
                    NDT.Columns.Add("يوم", Type.GetType("System.String"))
                    For Each row As DataRow In NDT.Rows
                        row("يوم") = Convert.ToDateTime(row(5), UK_Culture).DayOfWeek
                    Next

                    NDT.SetColumnsOrder(New String() {"رقم الموظف",
                                   "اسم الموظف",
                                   "اسم وحدة العمل",
                                   "اسم الشركة",
                                   "اسم الجدول",
                                   "التاريخ",
                                   "يوم",
                                   "اول دخول",
                                   "اخر خروج",
                                   "التأخير",
                                   "الخروج المبكر",
                                   "المدة",
                                   "الوقت الضائع",
                                   "الملاحظات",
                                   "الوقت الاضافي"})
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                    For Each row As DataRow In NDT.Rows
                        If row(7).ToString = "" And Not row(8).ToString = "" Then
                            row(13) = row(13) + " -لايوجد دخول"
                        ElseIf Not row(7).ToString = "" And row(8).ToString = "" Then
                            row(13) = row(13) + " -لايوجد خروج"
                        End If
                    Next
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                Else

                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    NDT.Columns(23).DataType = System.Type.GetType("System.String")
                    NDT.Columns(25).DataType = System.Type.GetType("System.String")
                    NDT.Columns(27).DataType = System.Type.GetType("System.String")
                    NDT.Columns(35).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 20 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = row(i).ToString()
                                End If
                            ElseIf i = 25 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 27 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 35 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 29 Then '---HolidayName Column
                                If dr(19).ToString = "H" Then
                                    dr(21) = dr(i).ToString
                                    dr(22) = dr(i).ToString
                                End If
                            ElseIf i = 31 Then '---LeaveName Column
                                If dr(19).ToString = "L" Then
                                    dr(21) = dr(i).ToString
                                    dr(22) = dr(i).ToString
                                End If
                            ElseIf i = 33 Then '---PermissionName Column
                                If dr(19).ToString = "P" Then
                                    dr(21) = dr(i).ToString
                                    dr(22) = dr(i).ToString
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
                    NDT.Columns(4).ColumnName = "Schedule Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Date"
                    NDT.Columns(6).ColumnName = "First In"
                    NDT.Columns(7).ColumnName = "Last Out"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Delay"
                    NDT.Columns(9).ColumnName = "Early Out"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Duration"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "Lost Time"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "OverTime"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "Work Location"
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns(15).ColumnName = "Leave Request"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns(16).ColumnName = "Permission Request"
                    NDT.Columns.RemoveAt(17)

                    NDT.Columns.Add("Day", Type.GetType("System.String"))
                    For Each row As DataRow In NDT.Rows
                        row("Day") = Convert.ToDateTime(row("Date"), UK_Culture).DayOfWeek
                    Next

                    NDT.SetColumnsOrder(New String() {"Emp No.",
                                    "Employee Name",
                                    "Entity Name",
                                    "Company Name",
                                    "Schedule Name",
                                    "Date",
                                    "Day",
                                    "First In",
                                    "Last Out",
                                    "Delay",
                                    "Early Out",
                                    "Duration",
                                    "Lost Time",
                                    "Remarks",
                                    "OverTime"})
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                    For Each row As DataRow In NDT.Rows
                        If row(7).ToString = "" And Not row(8).ToString = "" Then
                            row(13) = row(13) + " -Missing In"
                        ElseIf Not row(7).ToString = "" And row(8).ToString = "" Then
                            row(13) = row(13) + " -Missing Out"
                        End If
                    Next
                    '---------Add Missing IN & OUT in the Remarks field-----------'

                End If
            Case Detailed_Transaction_And_Accomodation_Transactions_Report
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    NDT.Columns(23).DataType = System.Type.GetType("System.String")
                    NDT.Columns(25).DataType = System.Type.GetType("System.String")
                    NDT.Columns(27).DataType = System.Type.GetType("System.String")
                    NDT.Columns(35).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 20 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = row(i).ToString()
                                End If
                            ElseIf i = 25 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 27 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 35 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 30 Then '---HolidayArabicName Column
                                If dr(19).ToString = "H" Then
                                    dr(21) = dr(i).ToString
                                    dr(23) = dr(i).ToString
                                End If
                            ElseIf i = 32 Then '---LeaveArabicName Column
                                If dr(19).ToString = "L" Then
                                    dr(21) = dr(i).ToString
                                    dr(23) = dr(i).ToString
                                End If
                            ElseIf i = 34 Then '---PermissionArabicName Column
                                If dr(19).ToString = "P" Then
                                    dr(21) = dr(i).ToString
                                    dr(23) = dr(i).ToString
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
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "اسم الجدول"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "التاريخ"
                    NDT.Columns(6).ColumnName = "اول دخول"
                    NDT.Columns(7).ColumnName = "اخر خروج"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "التأخير"
                    NDT.Columns(9).ColumnName = "الخروج المبكر"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "المدة"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "الوقت الضائع"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "الوقت الاضافي"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "موقع العمل"
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns(15).ColumnName = "وقت دخول الى السكن"
                    NDT.Columns(16).ColumnName = "وقت الخروج من السكن"
                    'Day Description
                    NDT.Columns.Add("يوم", Type.GetType("System.String"))
                    For Each row As DataRow In NDT.Rows
                        row("يوم") = Convert.ToDateTime(row(5), UK_Culture).DayOfWeek
                    Next

                    NDT.SetColumnsOrder(New String() {"رقم الموظف",
                                   "اسم الموظف",
                                   "اسم وحدة العمل",
                                   "اسم الشركة",
                                   "اسم الجدول",
                                   "التاريخ",
                                   "يوم",
                                   "اول دخول",
                                   "اخر خروج",
                                   "التأخير",
                                   "الخروج المبكر",
                                   "المدة",
                                   "الوقت الضائع",
                                   "الملاحظات",
                                   "الوقت الاضافي",
                                                       "وقت دخول الى السكن",
                                      "وقت الخروج من السكن"})
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                    For Each row As DataRow In NDT.Rows
                        If row(7).ToString = "" And Not row(8).ToString = "" Then
                            row(13) = row(13) + " -لايوجد دخول"
                        ElseIf Not row(7).ToString = "" And row(8).ToString = "" Then
                            row(13) = row(13) + " -لايوجد خروج"
                        End If
                    Next
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                Else

                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    NDT.Columns(23).DataType = System.Type.GetType("System.String")
                    NDT.Columns(25).DataType = System.Type.GetType("System.String")
                    NDT.Columns(27).DataType = System.Type.GetType("System.String")
                    NDT.Columns(35).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 20 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = row(i).ToString()
                                End If
                            ElseIf i = 25 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 27 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 35 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 29 Then '---HolidayName Column
                                If dr(19).ToString = "H" Then
                                    dr(21) = dr(i).ToString
                                    dr(22) = dr(i).ToString
                                End If
                            ElseIf i = 31 Then '---LeaveName Column
                                If dr(19).ToString = "L" Then
                                    dr(21) = dr(i).ToString
                                    dr(22) = dr(i).ToString
                                End If
                            ElseIf i = 33 Then '---PermissionName Column
                                If dr(19).ToString = "P" Then
                                    dr(21) = dr(i).ToString
                                    dr(22) = dr(i).ToString
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
                    NDT.Columns(4).ColumnName = "Schedule Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Date"
                    NDT.Columns(6).ColumnName = "First In"
                    NDT.Columns(7).ColumnName = "Last Out"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Delay"
                    NDT.Columns(9).ColumnName = "Early Out"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Duration"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "Lost Time"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "OverTime"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "Work Location"
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns(15).ColumnName = "AC In Time"
                    NDT.Columns(16).ColumnName = "AC Out Time"

                    NDT.Columns.Add("Day", Type.GetType("System.String"))
                    For Each row As DataRow In NDT.Rows
                        row("Day") = Convert.ToDateTime(row("Date"), UK_Culture).DayOfWeek
                    Next

                    NDT.SetColumnsOrder(New String() {"Emp No.",
                                    "Employee Name",
                                    "Entity Name",
                                    "Company Name",
                                    "Schedule Name",
                                    "Date",
                                    "Day",
                                    "First In",
                                    "Last Out",
                                    "Delay",
                                    "Early Out",
                                    "Duration",
                                    "Lost Time",
                                    "AC In Time",
                                 "AC Out Time",
                                    "Remarks",
                                    "OverTime"})
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                    For Each row As DataRow In NDT.Rows
                        If row(7).ToString = "" And Not row(8).ToString = "" Then
                            row(13) = row(13) + " -Missing In"
                        ElseIf Not row(7).ToString = "" And row(8).ToString = "" Then
                            row(13) = row(13) + " -Missing Out"
                        End If
                    Next
                    '---------Add Missing IN & OUT in the Remarks field-----------'

                End If
            Case DailyReportWithCounts
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    NDT.Columns(23).DataType = System.Type.GetType("System.String")
                    NDT.Columns(25).DataType = System.Type.GetType("System.String")
                    NDT.Columns(27).DataType = System.Type.GetType("System.String")
                    NDT.Columns(35).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 20 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = row(i).ToString()
                                End If
                            ElseIf i = 25 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 27 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 35 Then
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
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "اسم الجدول"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "التاريخ"
                    NDT.Columns(6).ColumnName = "اول دخول"
                    NDT.Columns(7).ColumnName = "اخر خروج"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "التأخير"
                    NDT.Columns(9).ColumnName = "الخروج المبكر"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "المدة"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "اسم العطلة"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "نوع الاجازة"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "نوع المغادرة"
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns(15).ColumnName = "الوقت الاضافي"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    'Day Description
                    NDT.Columns.Add("يوم", Type.GetType("System.String"))
                    For Each row As DataRow In NDT.Rows
                        row("يوم") = Convert.ToDateTime(row(5), UK_Culture).DayOfWeek
                    Next

                    NDT.SetColumnsOrder(New String() {"رقم الموظف",
                      "اسم الموظف",
                      "اسم وحدة العمل",
                      "اسم الشركة",
                      "اسم الجدول",
                      "التاريخ",
                      "يوم",
                      "اول دخول",
                      "اخر خروج",
                      "التأخير",
                      "الخروج المبكر",
                      "المدة",
                      "الملاحظات",
                      "اسم العطلة",
                      "نوع الاجازة",
                      "نوع المغادرة",
                      "الوقت الاضافي"})

                Else
                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    NDT.Columns(23).DataType = System.Type.GetType("System.String")
                    NDT.Columns(25).DataType = System.Type.GetType("System.String")
                    NDT.Columns(27).DataType = System.Type.GetType("System.String")
                    NDT.Columns(35).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 20 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 25 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 27 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 35 Then
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
                    NDT.Columns(4).ColumnName = "Schedule Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Date"
                    NDT.Columns(6).ColumnName = "First In"
                    NDT.Columns(7).ColumnName = "Last Out"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Delay"
                    NDT.Columns(9).ColumnName = "Early Out"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Duration"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "Holiday Name"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "Leave Name"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "Permission Name"
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns(15).ColumnName = "OverTime"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.Add("Day", Type.GetType("System.String"))
                    For Each row As DataRow In NDT.Rows
                        row("Day") = Convert.ToDateTime(row("Date"), UK_Culture).DayOfWeek
                    Next

                    NDT.SetColumnsOrder(New String() {"Emp No.",
                                                 "Employee Name",
                                                 "Entity Name",
                                                 "Company Name",
                                                 "Schedule Name",
                                                 "Date",
                                                 "Day",
                                                 "First In",
                                                 "Last Out",
                                                 "Delay",
                                                 "Early Out",
                                                 "Duration",
                                                 "Remarks",
                                                 "Holiday Name",
                                                 "Leave Name",
                                                 "Permission Name",
                                                 "OverTime"})

                End If
            Case AttendanceTransactionsReport
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "اسم الشركة"
                    NDT.Columns(4).ColumnName = "السبب"
                    NDT.Columns(5).ColumnName = "التاريخ "
                    NDT.Columns(6).ColumnName = "الوقت "
                    NDT.Columns(7).ColumnName = "جهاز القارئ "
                    NDT.Columns(8).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                Else
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Reason"
                    NDT.Columns(5).ColumnName = "Date"
                    NDT.Columns(6).ColumnName = "Time"
                    NDT.Columns(7).ColumnName = "Reader"
                    NDT.Columns(8).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                End If

            Case AttendanceTransactionsWithTemperatureReport
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "اسم الشركة"
                    NDT.Columns(4).ColumnName = "السبب"
                    NDT.Columns(5).ColumnName = "التاريخ "
                    NDT.Columns(6).ColumnName = "الوقت "
                    NDT.Columns(7).ColumnName = "جهاز القارئ "
                    NDT.Columns(8).ColumnName = "الملاحظات"
                    NDT.Columns(9).ColumnName = "درجة الحرارة °C"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                Else
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Reason"
                    NDT.Columns(5).ColumnName = "Date"
                    NDT.Columns(6).ColumnName = "Time"
                    NDT.Columns(7).ColumnName = "Reader"
                    NDT.Columns(8).ColumnName = "Remarks"
                    NDT.Columns(9).ColumnName = "Temperature °C"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                End If

            Case AbsentEmployeesReport
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(19).DataType = System.Type.GetType("System.String")
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
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 19 Then
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
                    NDT.Columns(6).ColumnName = "تاريخ الحركة الخاطئة"
                    NDT.Columns(7).ColumnName = "وقت الحركة الخاطئة"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    'NDT.Columns.RemoveAt(6)
                    'NDT.Columns.RemoveAt(6)
                    'NDT.Columns.RemoveAt(6)

                Else
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(19).DataType = System.Type.GetType("System.String")
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
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 19 Then
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
                    NDT.Columns(6).ColumnName = "Invalid Date"
                    NDT.Columns(7).ColumnName = "Invalid Time"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)


                End If
            Case 6 'rptEmpExtraHours
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(10).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 10 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(1).ColumnName = "رقم الموظف"
                    NDT.Columns(2).ColumnName = "اسم الموظف"
                    NDT.Columns(3).ColumnName = "اسم الموظف باللغة العربية"
                    NDT.Columns(4).ColumnName = "اسم وحدة العمل"
                    NDT.Columns(5).ColumnName = "اسم وحدة العمل باللغة العربية"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "اسم الشركة"
                    NDT.Columns(7).ColumnName = "اسم الشركة باللغة العربية"
                    NDT.Columns(8).ColumnName = "تاريخ الحركة"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "الحالة"
                    NDT.Columns(10).ColumnName = "الملاحظات"
                    NDT.Columns(11).ColumnName = "السبب"
                    NDT.Columns(12).ColumnName = "الوقت الاضافي"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "مجموع ساعات العمل"
                    NDT.Columns.RemoveAt(14)
                Else
                    NDT.Columns(10).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 10 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(1).ColumnName = "Emp No."
                    NDT.Columns(2).ColumnName = "Employee Name"
                    NDT.Columns(3).ColumnName = "Employee Arabic Name"
                    NDT.Columns(4).ColumnName = "Entity Name"
                    NDT.Columns(5).ColumnName = "Entity Arabic Name"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Company Name"
                    NDT.Columns(7).ColumnName = "Company Arabic Name"
                    NDT.Columns(8).ColumnName = "Move Date"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "Status"
                    NDT.Columns(10).ColumnName = "Remarks"
                    NDT.Columns(11).ColumnName = "Reason"
                    NDT.Columns(12).ColumnName = "OverTime"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "Total Work Hours"
                    NDT.Columns.RemoveAt(14)
                End If
            Case 7 'rptViolations
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "اسم الموظف باللغة العربية"
                    NDT.Columns(3).ColumnName = "اسم وحدة العمل"
                    NDT.Columns(4).ColumnName = "اسم وحدة العمل باللغة العربية"
                    NDT.Columns(5).ColumnName = "اسم الشركة"
                    NDT.Columns(6).ColumnName = "اسم الشركة باللغة العربية"
                    NDT.Columns(7).ColumnName = "تاريخ الحركة"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "الحالة"
                    NDT.Columns(9).ColumnName = "الملاحظات"
                    NDT.Columns(10).ColumnName = "السبب"
                    NDT.Columns(11).ColumnName = "الوقت الاضافي"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "مجموع ساعات العمل"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "التأخير"
                    NDT.Columns(14).ColumnName = "الخروج المبكر"
                Else
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns(2).ColumnName = "Employee Arabic Name"
                    NDT.Columns(3).ColumnName = "Entity Name"
                    NDT.Columns(4).ColumnName = "Entity Arabic Name"
                    NDT.Columns(5).ColumnName = "Company Name"
                    NDT.Columns(6).ColumnName = "Company Arabic Name"
                    NDT.Columns(7).ColumnName = "Move Date"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Status"
                    NDT.Columns(9).ColumnName = "Remarks"
                    NDT.Columns(10).ColumnName = "Reason"
                    NDT.Columns(11).ColumnName = "OverTime"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "Total Work Hours"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "Delay"
                    NDT.Columns(14).ColumnName = "Early Out"
                End If
            Case InOutTransactionsReport

                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")
                    NDT.Columns(14).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 14 Then
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
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "اسم الموظف باللغة العربية"
                    NDT.Columns(3).ColumnName = "اسم وحدة العمل"
                    NDT.Columns(4).ColumnName = "اسم وحدة العمل باللغة العربية"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "المدة"
                    NDT.Columns(6).ColumnName = "تاريخ الحركة"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "وقت الدخول"
                    NDT.Columns(8).ColumnName = "وقت الخروج"
                    NDT.Columns.RemoveAt(9)
                Else
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")
                    NDT.Columns(14).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 14 Then
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
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns(2).ColumnName = "Employee Arabic Name"
                    NDT.Columns(3).ColumnName = "Entity Name"
                    NDT.Columns(4).ColumnName = "Entity Arabic Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Duration"
                    NDT.Columns(6).ColumnName = "Date"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "Time In"
                    NDT.Columns(8).ColumnName = "Time Out"
                    NDT.Columns.RemoveAt(9)
                End If
            Case ManualEntryRepor

                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(3).DataType = System.Type.GetType("System.String")
                    NDT.Columns(4).DataType = System.Type.GetType("System.String")
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 3 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 4 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("dd/MM/yyyy HH:MM")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next

                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "تاريخ الحركة"
                    NDT.Columns(3).ColumnName = "وقت الحركة"
                    NDT.Columns(4).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "السبب"
                    NDT.Columns(6).ColumnName = "اسم مستخدم الادخال"
                    NDT.Columns(7).ColumnName = "وقت و تاريخ الادخال"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)

                Else
                    NDT.Columns(3).DataType = System.Type.GetType("System.String")
                    NDT.Columns(4).DataType = System.Type.GetType("System.String")
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 3 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 4 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("dd/MM/yyyy HH:MM")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next

                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Transaction Date"
                    NDT.Columns(3).ColumnName = "Transaction Time"
                    NDT.Columns(4).ColumnName = "Remarks"
                    NDT.Columns(5).ColumnName = "Reason"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Entry Date Time"
                    NDT.Columns(7).ColumnName = "Entry Details"
                    NDT.Columns(8).ColumnName = "Approval Date"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)

                End If
            Case IncompleteTransactions
                NDT.Columns(8).DataType = System.Type.GetType("System.String")
                NDT.Columns(9).DataType = System.Type.GetType("System.String")
                NDT.Columns(10).DataType = System.Type.GetType("System.String")
                For Each row As DataRow In DT.Rows
                    Dim dr As DataRow = NDT.NewRow
                    For i As Integer = 0 To NDT.Columns.Count - 1
                        dr(i) = row(i)
                        If i = 8 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                            End If
                        ElseIf i = 9 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 10 Then
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
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "اسم الموظف باللغة العربية"
                    NDT.Columns(3).ColumnName = "اسم وحدة العمل"
                    NDT.Columns(4).ColumnName = "اسم وحدة العمل باللغة العربية"
                    NDT.Columns(5).ColumnName = "اسم الشركة"
                    NDT.Columns(6).ColumnName = "اسم الشركة باللغة العربية"
                    NDT.Columns(7).ColumnName = "تاريخ الحركة"
                    NDT.Columns(8).ColumnName = "وقت الدخول"
                    NDT.Columns(9).ColumnName = "وقت الخروج"
                    NDT.Columns.RemoveAt(10)
                Else
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns(2).ColumnName = "Employee Arabic Name"
                    NDT.Columns(3).ColumnName = "Entity Name"
                    NDT.Columns(4).ColumnName = "Entity Arabic Name"
                    NDT.Columns(5).ColumnName = "Company Name"
                    NDT.Columns(6).ColumnName = "Company Arabic Name"
                    NDT.Columns(7).ColumnName = "Date"
                    NDT.Columns(8).ColumnName = "In Time"
                    NDT.Columns(9).ColumnName = "Out Time"
                    NDT.Columns.RemoveAt(10)
                End If
            Case EarlyOutReport
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
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
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 19 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 20 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "اسم الموظف باللغة العربية"
                    NDT.Columns(3).ColumnName = "اسم وحدة العمل"
                    NDT.Columns(4).ColumnName = "اسم وحدة العمل باللغة العربية"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "التاريخ"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "الخروج المبكر"
                    NDT.Columns.RemoveAt(8)

                Else
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
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
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 19 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 20 Then
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
                    NDT.Columns(2).ColumnName = "Employee Arabic Name"
                    NDT.Columns(3).ColumnName = "Entity Name"
                    NDT.Columns(4).ColumnName = "Entity Arabic Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Date"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "Early Out"
                    NDT.Columns.RemoveAt(8)
                End If
            Case DelayReport
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(19).DataType = System.Type.GetType("System.String")
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
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 19 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "اسم الموظف باللغة العربية"
                    NDT.Columns(3).ColumnName = "اسم وحدة العمل"
                    NDT.Columns(4).ColumnName = "اسم وحدة العمل باللغة العربية"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "التاريخ"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "التأخير"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)


                Else
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(19).DataType = System.Type.GetType("System.String")
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
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 19 Then
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
                    NDT.Columns(2).ColumnName = "Employee Arabic Name"
                    NDT.Columns(3).ColumnName = "Entity Name"
                    NDT.Columns(4).ColumnName = "Entity Arabic Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Date"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "Delay"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                End If
            Case DetailedTransactions

                NDT.Columns(3).DataType = System.Type.GetType("System.String")
                NDT.Columns(4).DataType = System.Type.GetType("System.String")
                NDT.Columns(5).DataType = System.Type.GetType("System.String")
                NDT.Columns(6).DataType = System.Type.GetType("System.String")
                NDT.Columns(9).DataType = System.Type.GetType("System.String")
                NDT.Columns(20).DataType = System.Type.GetType("System.String")
                NDT.Columns(21).DataType = System.Type.GetType("System.String")
                NDT.Columns(22).DataType = System.Type.GetType("System.String")
                NDT.Columns(23).DataType = System.Type.GetType("System.String")
                NDT.Columns(24).DataType = System.Type.GetType("System.String")
                For Each row As DataRow In DT.Rows
                    Dim dr As DataRow = NDT.NewRow
                    For i As Integer = 0 To NDT.Columns.Count - 1
                        dr(i) = row(i)
                        If i = 3 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                            End If
                        ElseIf i = 4 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 5 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 6 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToString(row(i)).ToString '("HH:mm")
                            End If
                        ElseIf i = 9 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = row(i).ToString()
                            End If

                        ElseIf i = 21 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 22 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 23 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 24 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        End If
                    Next
                    NDT.Rows.Add(dr)
                Next



                If (Lang = CtlCommon.Lang.AR) Then
                    For Each row As DataRow In NDT.Rows
                        If Not IsDBNull(row("Status")) Then
                            If row("Status") = "P" Then
                                If Not IsDBNull(row("FromTime")) Then
                                    row("REMARKS") = (row("PermArabicName").ToString + " (" + row("FromTime").ToString + "-" + row("ToTime").ToString + ")")
                                Else
                                    row("REMARKS") = row("PermArabicName").ToString
                                End If

                            ElseIf row("Status") = "L" Then
                                row("REMARKS") = row("LeaveArabicName")
                            ElseIf row("Status") = "H" Then
                                row("REMARKS") = row("HolidayArabicName")
                            Else
                                row("REMARKS") = row("StatusNameArb")
                            End If
                        End If
                    Next

                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "التاريخ"
                    NDT.Columns(3).ColumnName = "وقت الدخول"
                    NDT.Columns(4).ColumnName = "وقت الخروج"
                    NDT.Columns(5).ColumnName = "المدة"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "مدة الخروج"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "من وقت"
                    NDT.Columns(10).ColumnName = "الى وقت"
                    NDT.Columns(11).ColumnName = "التأخير"
                    NDT.Columns(12).ColumnName = "الخروج المبكر"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.Add("يوم", Type.GetType("System.String"))
                    For Each row As DataRow In NDT.Rows
                        row("يوم") = Convert.ToDateTime(row(2), UK_Culture).DayOfWeek
                    Next
                    NDT.SetColumnsOrder(New String() {"اسم الشركة",
                                        "اسم وحدة العمل",
                                               "رقم الموظف",
                                                "اسم الموظف",
                                                "التاريخ",
                                                "يوم",
                                                "وقت الدخول",
                                                "وقت الخروج",
                                                "المدة",
                                                "مدة الخروج",
                                                "من وقت",
                                                "الى وقت",
                                                "التأخير",
                                                "الخروج المبكر",
                                                "الملاحظات"})

                    '---------Add Missing IN & OUT in the Remarks field-----------'
                    For Each row As DataRow In NDT.Rows
                        If row("وقت الدخول").ToString = "" And Not row("وقت الخروج").ToString = "" Then
                            row("الملاحظات") = "لايوجد دخول"
                        ElseIf Not row("وقت الدخول").ToString = "" And row("وقت الخروج").ToString = "" Then
                            row("الملاحظات") = " لايوجد خروج"
                        End If
                    Next
                    '---------Add Missing IN & OUT in the Remarks field-----------'


                Else
                    For Each row As DataRow In NDT.Rows
                        If Not IsDBNull(row("Status")) Then
                            If row("Status") = "P" Then
                                If Not IsDBNull(row("FromTime")) Then
                                    row("REMARKS") = (row("PermName") + " (" + row("FromTime").ToString + "-" + row("ToTime").ToString + ")")
                                Else
                                    row("REMARKS") = row("PermName")
                                End If
                            ElseIf row("Status") = "L" Then
                                row("REMARKS") = row("LeaveName")
                            ElseIf row("Status") = "H" Then
                                row("REMARKS") = row("HolidayName")
                            Else
                                row("REMARKS") = row("StatusName")
                            End If
                        End If
                    Next

                    NDT.Columns(0).ColumnName = "Employee No"
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Date"
                    NDT.Columns(3).ColumnName = "In Time"
                    NDT.Columns(4).ColumnName = "Out Time"
                    NDT.Columns(5).ColumnName = "Duration"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Out Duration"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "From Time"
                    NDT.Columns(10).ColumnName = "To Time"
                    NDT.Columns(11).ColumnName = "Delay"
                    NDT.Columns(12).ColumnName = "Early Out"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.Add("Day", Type.GetType("System.String"))
                    For Each row As DataRow In NDT.Rows
                        row("Day") = Convert.ToDateTime(row("Date"), UK_Culture).DayOfWeek
                    Next

                    NDT.SetColumnsOrder(New String() {"Company Name",
                                                 "Entity Name",
                                                 "Employee No",
                                                 "Employee Name",
                                                 "Date",
                                                 "Day",
                                                 "In Time",
                                                 "Out Time",
                                                 "Duration",
                                                 "Out Duration",
                                                 "From Time",
                                                 "To Time",
                                                 "Delay",
                                                 "Early Out",
                                                 "REMARKS"})
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                    For Each row As DataRow In NDT.Rows
                        If row("In Time").ToString = "" And Not row("Out Time").ToString = "" Then
                            row("Remarks") = "Missing In"
                        ElseIf Not row("In Time").ToString = "" And row("Out Time").ToString = "" Then
                            row("Remarks") = "Missing Out"
                        End If
                    Next
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                End If

            Case InvalidAttempts
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(14).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 14 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Employee No"
                    NDT.Columns(1).ColumnName = "Card No"
                    NDT.Columns(2).ColumnName = "Employee Name"
                    NDT.Columns(3).ColumnName = "Employee Arabic Name"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Entity Name"
                    NDT.Columns(5).ColumnName = "Entity Arabic Name"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Company Name"
                    NDT.Columns(7).ColumnName = "Company Arabic Name"
                    NDT.Columns(8).ColumnName = "Reason Name"
                    NDT.Columns(9).ColumnName = "Reason Arabic Name"
                    NDT.Columns(10).ColumnName = "Move Date"
                    NDT.Columns(11).ColumnName = "Move Time"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "Reader"
                    NDT.Columns(13).ColumnName = "Employee Image"
                Else
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(14).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 14 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Employee No"
                    NDT.Columns(1).ColumnName = "Card No"
                    NDT.Columns(2).ColumnName = "Employee Name"
                    NDT.Columns(3).ColumnName = "Employee Arabic Name"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Entity Name"
                    NDT.Columns(5).ColumnName = "Entity Arabic Name"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Company Name"
                    NDT.Columns(7).ColumnName = "Company Arabic Name"
                    NDT.Columns(8).ColumnName = "Reason Name"
                    NDT.Columns(9).ColumnName = "Reason Arabic Name"
                    NDT.Columns(10).ColumnName = "Move Date"
                    NDT.Columns(11).ColumnName = "Move Time"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "Reader"
                    NDT.Columns(13).ColumnName = "Employee Image"
                End If
            Case EmployeeTimeAttendance2
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    NDT.Columns(23).DataType = System.Type.GetType("System.String")
                    NDT.Columns(25).DataType = System.Type.GetType("System.String")
                    NDT.Columns(35).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 20 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 25 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 27 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 35 Then
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
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "اسم الجدول"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "التاريخ "
                    NDT.Columns(6).ColumnName = "اول دخول"
                    NDT.Columns(7).ColumnName = "اخر خروج"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "التأخير"
                    NDT.Columns(9).ColumnName = "الخروج المبكر"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "المدة"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)

                    NDT.Columns.RemoveAt(19)
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns(18).ColumnName = "وقت إضافي"


                Else
                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    NDT.Columns(23).DataType = System.Type.GetType("System.String")
                    NDT.Columns(25).DataType = System.Type.GetType("System.String")
                    NDT.Columns(35).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 20 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 25 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 27 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 35 Then
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
                    NDT.Columns(4).ColumnName = "Schedule Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Date"
                    NDT.Columns(6).ColumnName = "First In"
                    NDT.Columns(7).ColumnName = "Last Out"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Delay"
                    NDT.Columns(9).ColumnName = "Early Out"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Duration"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns(18).ColumnName = "Extra Time"

                End If
            Case DelayandEarlyOut
                NDT.Columns(8).DataType = System.Type.GetType("System.String")
                NDT.Columns(9).DataType = System.Type.GetType("System.String")
                NDT.Columns(10).DataType = System.Type.GetType("System.String")
                NDT.Columns(11).DataType = System.Type.GetType("System.String")
                NDT.Columns(12).DataType = System.Type.GetType("System.String")
                NDT.Columns(13).DataType = System.Type.GetType("System.String")
                For Each row As DataRow In DT.Rows
                    Dim dr As DataRow = NDT.NewRow
                    For i As Integer = 0 To NDT.Columns.Count - 1
                        dr(i) = row(i)
                        If i = 8 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                            End If
                        ElseIf i = 9 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 10 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 11 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 12 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 13 Then
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
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "اسم الموظف باللغة العربية"
                    NDT.Columns(3).ColumnName = "اسم وحدة العمل"
                    NDT.Columns(4).ColumnName = "اسم وحدة العمل باللغة العربية"
                    NDT.Columns(5).ColumnName = "اسم الشركة"
                    NDT.Columns(6).ColumnName = "اسم الشركة باللغة العربية"
                    NDT.Columns(7).ColumnName = "تاريخ الحركة"
                    NDT.Columns(8).ColumnName = "وقت الدخول"
                    NDT.Columns(9).ColumnName = "وقت الخروج"
                    NDT.Columns(10).ColumnName = "التأخير"
                    NDT.Columns(11).ColumnName = "الخروج المبكر"
                    NDT.Columns(12).ColumnName = "مجموع التأخير و الخروج المبكر"
                    NDT.Columns(13).ColumnName = "مجموع مرات التأخير و الخروج المبكر"
                    NDT.Columns(14).ColumnName = "عدد مرات التأخير"
                    NDT.Columns(15).ColumnName = "عدد مرات الخروج المبكر"


                Else
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns(2).ColumnName = "Employee Arabic Name"
                    NDT.Columns(3).ColumnName = "Entity Name"
                    NDT.Columns(4).ColumnName = "Entity Arabic Name"
                    NDT.Columns(5).ColumnName = "Company Name"
                    NDT.Columns(6).ColumnName = "Company Arabic Name"
                    NDT.Columns(7).ColumnName = "Date"
                    NDT.Columns(8).ColumnName = "In Time"
                    NDT.Columns(9).ColumnName = "Out Time"
                    NDT.Columns(10).ColumnName = "Delay"
                    NDT.Columns(11).ColumnName = "Early Out"
                    NDT.Columns(12).ColumnName = "Total Delay and EarlyOut"
                    NDT.Columns(13).ColumnName = "Delay and EarlyOut Count"
                    NDT.Columns(14).ColumnName = "Delay Count"
                    NDT.Columns(15).ColumnName = "EarlyOut Count"
                End If
            Case DailyReport2
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    NDT.Columns(23).DataType = System.Type.GetType("System.String")
                    NDT.Columns(25).DataType = System.Type.GetType("System.String")
                    NDT.Columns(35).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 20 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 25 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 27 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 35 Then
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
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "اسم الجدول"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "التاريخ "
                    NDT.Columns(6).ColumnName = " اول دخول"
                    NDT.Columns(7).ColumnName = " اخر خروج"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "التأخير"
                    NDT.Columns(9).ColumnName = "الخروج المبكر"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "المدة"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "وقت إضافي"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)

                Else
                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    NDT.Columns(23).DataType = System.Type.GetType("System.String")
                    NDT.Columns(25).DataType = System.Type.GetType("System.String")
                    NDT.Columns(35).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 20 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 25 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 27 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 35 Then
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
                    NDT.Columns(4).ColumnName = "Schedule Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Date"
                    NDT.Columns(6).ColumnName = "First In"
                    NDT.Columns(7).ColumnName = "Last Out"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Delay"
                    NDT.Columns(9).ColumnName = "Early Out"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Duration"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "Extra Time"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)



                End If
            Case DailyReport3
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")
                    NDT.Columns(10).DataType = System.Type.GetType("System.String")
                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(14).DataType = System.Type.GetType("System.String")
                    NDT.Columns(23).DataType = System.Type.GetType("System.String")
                    NDT.Columns(25).DataType = System.Type.GetType("System.String")
                    NDT.Columns(35).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 10 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                                'ElseIf i = 14 Then
                                '    If Not dr(i) Is DBNull.Value Then
                                '        dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                '    End If
                                'ElseIf i = 27 Then
                                '    If Not dr(i) Is DBNull.Value Then
                                '        dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                '    End If
                                'ElseIf i = 35 Then
                                '    If Not dr(i) Is DBNull.Value Then
                                '        dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                '    End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Division"
                    NDT.Columns(3).ColumnName = "Department"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Date"
                    NDT.Columns(5).ColumnName = "In Time"
                    NDT.Columns(6).ColumnName = "Delay"
                    NDT.Columns(7).ColumnName = "Out Time"
                    NDT.Columns(8).ColumnName = "Early Out"
                    NDT.Columns(9).ColumnName = "Duration"
                    NDT.Columns(10).ColumnName = "Over Time"
                    NDT.Columns(11).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)



                Else
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")
                    NDT.Columns(10).DataType = System.Type.GetType("System.String")
                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(14).DataType = System.Type.GetType("System.String")
                    NDT.Columns(23).DataType = System.Type.GetType("System.String")
                    NDT.Columns(25).DataType = System.Type.GetType("System.String")
                    NDT.Columns(35).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 10 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 13 Then
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
                    NDT.Columns(2).ColumnName = "Division"
                    NDT.Columns(3).ColumnName = "Department"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Date"
                    NDT.Columns(5).ColumnName = "In Time"
                    NDT.Columns(6).ColumnName = "Delay"
                    NDT.Columns(7).ColumnName = "Out Time"
                    NDT.Columns(8).ColumnName = "Early Out"
                    NDT.Columns(9).ColumnName = "Duration"
                    NDT.Columns(10).ColumnName = "OverTime"
                    NDT.Columns(11).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)



                End If
            Case GatesReport
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "اسم الشركة"
                    NDT.Columns(4).ColumnName = "السبب"
                    NDT.Columns(5).ColumnName = "التاريخ "
                    NDT.Columns(6).ColumnName = "الوقت "

                Else
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Reason"
                    NDT.Columns(5).ColumnName = "Date"
                    NDT.Columns(6).ColumnName = "Time"


                End If
            Case AuthorityMovements
                NDT = DT


            Case DetailedTransactions2
                NDT.Columns(3).DataType = System.Type.GetType("System.String")
                NDT.Columns(4).DataType = System.Type.GetType("System.String")
                NDT.Columns(5).DataType = System.Type.GetType("System.String")
                NDT.Columns(6).DataType = System.Type.GetType("System.String")
                NDT.Columns(9).DataType = System.Type.GetType("System.String")
                NDT.Columns(20).DataType = System.Type.GetType("System.String")
                NDT.Columns(21).DataType = System.Type.GetType("System.String")
                NDT.Columns(22).DataType = System.Type.GetType("System.String")
                NDT.Columns(23).DataType = System.Type.GetType("System.String")
                NDT.Columns(24).DataType = System.Type.GetType("System.String")
                For Each row As DataRow In DT.Rows
                    Dim dr As DataRow = NDT.NewRow
                    For i As Integer = 0 To NDT.Columns.Count - 1
                        dr(i) = row(i)
                        If i = 3 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                            End If
                        ElseIf i = 4 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 5 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 6 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToString(row(i)).ToString '("HH:mm")
                            End If
                        ElseIf i = 9 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If

                        ElseIf i = 21 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 22 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 23 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 24 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
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
                    NDT.Columns(3).ColumnName = "وقت الدخول"
                    NDT.Columns(4).ColumnName = "وقت الخروج"
                    NDT.Columns(5).ColumnName = "المدة"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "اسم السبب"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "اسم الحالة"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "نوع الاجازة"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "نوع المغادرة"
                    NDT.Columns(12).ColumnName = "من وقت"
                    NDT.Columns(13).ColumnName = "الى وقت"
                    NDT.Columns(14).ColumnName = "التأخير"
                    NDT.Columns(15).ColumnName = "الخروج المبكر"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns(16).ColumnName = "اسم العطلة"
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns(17).ColumnName = "اسم الجهة / IP Address"


                Else
                    NDT.Columns(0).ColumnName = "Employee No"
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Date"
                    NDT.Columns(3).ColumnName = "In Time"
                    NDT.Columns(4).ColumnName = "Out Time"
                    NDT.Columns(5).ColumnName = "Duration"
                    NDT.Columns(6).ColumnName = "Reason Name"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "Status Name"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Leave Type"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "Permission Type"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "From Time"
                    NDT.Columns(13).ColumnName = "To Time"
                    NDT.Columns(14).ColumnName = "Delay"
                    NDT.Columns(15).ColumnName = "Early Out"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns(16).ColumnName = "Holiday Name"
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns(17).ColumnName = "Authority Name / IP Address"

                End If


            Case DailyReport4
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    NDT.Columns(23).DataType = System.Type.GetType("System.String")
                    NDT.Columns(25).DataType = System.Type.GetType("System.String")
                    NDT.Columns(27).DataType = System.Type.GetType("System.String")
                    NDT.Columns(35).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 20 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 25 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 27 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 35 Then
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
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "اسم الجدول"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "التاريخ "
                    NDT.Columns(6).ColumnName = " اول دخول"
                    NDT.Columns(7).ColumnName = " اخر خروج"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "التأخير"
                    NDT.Columns(9).ColumnName = "الخروج المبكر"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "المدة"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "اسم العطلة"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "نوع الاجازة"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "نوع المغادرة"
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns(15).ColumnName = "الوقت الاضافي"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                Else
                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    NDT.Columns(23).DataType = System.Type.GetType("System.String")
                    NDT.Columns(25).DataType = System.Type.GetType("System.String")
                    NDT.Columns(27).DataType = System.Type.GetType("System.String")
                    NDT.Columns(35).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 20 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 25 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 27 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 35 Then
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
                    NDT.Columns(4).ColumnName = "Schedule Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Date"
                    NDT.Columns(6).ColumnName = "First In"
                    NDT.Columns(7).ColumnName = "Last Out"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Delay"
                    NDT.Columns(9).ColumnName = "Early Out"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Duration"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "Holiday Name"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "Leave Name"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "Permission Name"
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns(15).ColumnName = "OverTime"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)


                End If

            Case AbsentEmployee_CountReport
                NDT = DT
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "مجموع ايام الغياب"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "المسمى الوظيغي"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)

                Else
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Total Absence"
                    NDT.Columns(3).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Designation"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                End If

            Case EmployeeAbsent_Period
                NDT.Columns(3).DataType = System.Type.GetType("System.String")
                NDT.Columns(4).DataType = System.Type.GetType("System.String")
                For Each row As DataRow In DT.Rows
                    Dim dr As DataRow = NDT.NewRow
                    For i As Integer = 0 To NDT.Columns.Count - 1
                        dr(i) = row(i)
                        If i = 3 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                            End If
                        ElseIf i = 4 Then
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
                    NDT.Columns(2).ColumnName = "تاريخ البداية"
                    NDT.Columns(3).ColumnName = "تاريخ النهاية"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "المسمى الوظيفي"
                    NDT.Columns(7).ColumnName = "عدد ايام الغياب"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                Else

                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Start Date"
                    NDT.Columns(3).ColumnName = "End Date"
                    NDT.Columns(4).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Designation Name"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "Absent Days"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                End If

            Case DailyReport_Email
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    NDT.Columns(23).DataType = System.Type.GetType("System.String")
                    NDT.Columns(25).DataType = System.Type.GetType("System.String")
                    NDT.Columns(27).DataType = System.Type.GetType("System.String")
                    NDT.Columns(35).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 20 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = row(i).ToString()
                                End If
                            ElseIf i = 25 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 27 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 35 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 30 Then '---HolidayArabicName Column
                                If dr(19).ToString = "H" Then
                                    dr(21) = dr(i).ToString
                                    dr(23) = dr(i).ToString
                                End If
                            ElseIf i = 32 Then '---LeaveArabicName Column
                                If dr(19).ToString = "L" Then
                                    dr(21) = dr(i).ToString
                                    dr(23) = dr(i).ToString
                                End If
                            ElseIf i = 34 Then '---PermissionArabicName Column
                                If dr(19).ToString = "P" Then
                                    dr(21) = dr(i).ToString
                                    dr(23) = dr(i).ToString
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
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "اسم الجدول"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "التاريخ"
                    NDT.Columns(6).ColumnName = "اول دخول"
                    NDT.Columns(7).ColumnName = "اخر خروج"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "التأخير"
                    NDT.Columns(9).ColumnName = "الخروج المبكر"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "المدة"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "الوقت الضائع"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "الوقت الاضافي"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "موقع العمل"
                    NDT.Columns(15).ColumnName = "البريد الالكتروني"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    'Day Description
                    NDT.Columns.Add("يوم", Type.GetType("System.String"))
                    For Each row As DataRow In NDT.Rows
                        row("يوم") = Convert.ToDateTime(row(5), UK_Culture).DayOfWeek
                    Next

                    NDT.SetColumnsOrder(New String() {"رقم الموظف",
                                   "اسم الموظف",
                                   "البريد الالكتروني",
                                   "اسم وحدة العمل",
                                   "اسم الشركة",
                                   "اسم الجدول",
                                   "التاريخ",
                                   "يوم",
                                   "اول دخول",
                                   "اخر خروج",
                                   "التأخير",
                                   "الخروج المبكر",
                                   "المدة",
                                   "الوقت الضائع",
                                   "الملاحظات",
                                   "الوقت الاضافي"})
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                    For Each row As DataRow In NDT.Rows
                        If row(7).ToString = "" And Not row(8).ToString = "" Then
                            row(13) = row(13) + " -لايوجد دخول"
                        ElseIf Not row(7).ToString = "" And row(8).ToString = "" Then
                            row(13) = row(13) + " -لايوجد خروج"
                        End If
                    Next
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                Else

                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    NDT.Columns(23).DataType = System.Type.GetType("System.String")
                    NDT.Columns(25).DataType = System.Type.GetType("System.String")
                    NDT.Columns(27).DataType = System.Type.GetType("System.String")
                    NDT.Columns(35).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 20 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = row(i).ToString()
                                End If
                            ElseIf i = 25 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 27 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 35 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 29 Then '---HolidayName Column
                                If dr(19).ToString = "H" Then
                                    dr(21) = dr(i).ToString
                                    dr(22) = dr(i).ToString
                                End If
                            ElseIf i = 31 Then '---LeaveName Column
                                If dr(19).ToString = "L" Then
                                    dr(21) = dr(i).ToString
                                    dr(22) = dr(i).ToString
                                End If
                            ElseIf i = 33 Then '---PermissionName Column
                                If dr(19).ToString = "P" Then
                                    dr(21) = dr(i).ToString
                                    dr(22) = dr(i).ToString
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
                    NDT.Columns(4).ColumnName = "Schedule Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Date"
                    NDT.Columns(6).ColumnName = "First In"
                    NDT.Columns(7).ColumnName = "Last Out"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Delay"
                    NDT.Columns(9).ColumnName = "Early Out"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Duration"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "Lost Time"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "OverTime"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "Work Location"
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns(15).ColumnName = "Email"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)

                    NDT.Columns.Add("Day", Type.GetType("System.String"))
                    For Each row As DataRow In NDT.Rows
                        row("Day") = Convert.ToDateTime(row("Date"), UK_Culture).DayOfWeek
                    Next

                    NDT.SetColumnsOrder(New String() {"Emp No.",
                                    "Employee Name",
                                    "Email",
                                    "Entity Name",
                                    "Company Name",
                                    "Schedule Name",
                                    "Date",
                                    "Day",
                                    "First In",
                                    "Last Out",
                                    "Delay",
                                    "Early Out",
                                    "Duration",
                                    "Lost Time",
                                    "Remarks",
                                    "OverTime"})
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                    For Each row As DataRow In NDT.Rows
                        If row(7).ToString = "" And Not row(8).ToString = "" Then
                            row(13) = row(13) + " -Missing In"
                        ElseIf Not row(7).ToString = "" And row(8).ToString = "" Then
                            row(13) = row(13) + " -Missing Out"
                        End If
                    Next
                    '---------Add Missing IN & OUT in the Remarks field-----------'

                End If

            Case DetailedTransactions_WithAllowance

                NDT.Columns(3).DataType = System.Type.GetType("System.String")
                NDT.Columns(4).DataType = System.Type.GetType("System.String")
                NDT.Columns(5).DataType = System.Type.GetType("System.String")
                NDT.Columns(6).DataType = System.Type.GetType("System.String")
                NDT.Columns(9).DataType = System.Type.GetType("System.String")
                NDT.Columns(20).DataType = System.Type.GetType("System.String")
                NDT.Columns(21).DataType = System.Type.GetType("System.String")
                NDT.Columns(22).DataType = System.Type.GetType("System.String")
                NDT.Columns(23).DataType = System.Type.GetType("System.String")
                NDT.Columns(24).DataType = System.Type.GetType("System.String")
                For Each row As DataRow In DT.Rows
                    Dim dr As DataRow = NDT.NewRow
                    For i As Integer = 0 To NDT.Columns.Count - 1
                        dr(i) = row(i)
                        If i = 3 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                            End If
                        ElseIf i = 4 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 5 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 6 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToString(row(i)).ToString '("HH:mm")
                            End If
                        ElseIf i = 9 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = row(i).ToString()
                            End If

                        ElseIf i = 21 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 22 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 23 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 24 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        End If
                    Next
                    NDT.Rows.Add(dr)
                Next



                If (Lang = CtlCommon.Lang.AR) Then
                    For Each row As DataRow In NDT.Rows
                        If Not IsDBNull(row("Status")) Then
                            If row("Status") = "P" Then
                                If Not IsDBNull(row("FromTime")) Then
                                    row("REMARKS") = (row("PermArabicName").ToString + " (" + row("FromTime").ToString + "-" + row("ToTime").ToString + ")")
                                Else
                                    row("REMARKS") = row("PermArabicName").ToString
                                End If

                            ElseIf row("Status") = "L" Then
                                row("REMARKS") = row("LeaveArabicName")
                            ElseIf row("Status") = "H" Then
                                row("REMARKS") = row("HolidayArabicName")
                            Else
                                row("REMARKS") = row("StatusNameArb")
                            End If
                        End If
                    Next

                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "التاريخ"
                    NDT.Columns(3).ColumnName = "وقت الدخول"
                    NDT.Columns(4).ColumnName = "وقت الخروج"
                    NDT.Columns(5).ColumnName = "المدة"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "مدة الخروج"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "من وقت"
                    NDT.Columns(10).ColumnName = "الى وقت"
                    NDT.Columns(11).ColumnName = "التأخير"
                    NDT.Columns(12).ColumnName = "الخروج المبكر"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "الاستخدام اليومي"
                    NDT.Columns(15).ColumnName = "المجموع اليومي"
                    NDT.Columns(16).ColumnName = "الباقي اليومي"
                    NDT.Columns(17).ColumnName = "الاستخدام الشهري"
                    NDT.Columns(18).ColumnName = "المجموع الشهري"
                    NDT.Columns(19).ColumnName = "الباقي الشهري"
                    If EmployeeFilter1.EmployeeId = 0 Then
                        NDT.Columns(20).ColumnName = "عدد ايام الغياب"
                        NDT.Columns(21).ColumnName = "عدد ايام الاستراحة"
                        NDT.Columns(22).ColumnName = "عدد ايام الاجازة"
                        NDT.Columns(23).ColumnName = "عدد ايام المغادرة"
                        NDT.Columns(24).ColumnName = "عدد ايام المغادرة ليوم كامل"
                        NDT.Columns(25).ColumnName = "عدد مرات التأخير"
                        NDT.Columns(26).ColumnName = "عدد مرات الخروج المبكر"
                        NDT.Columns(27).ColumnName = "مجموع ساعات العمل الاضافي"
                        NDT.Columns(28).ColumnName = "عدد ايام العمل"
                    Else
                        NDT.Columns.RemoveAt(20)
                        NDT.Columns.RemoveAt(20)
                        NDT.Columns.RemoveAt(20)
                        NDT.Columns.RemoveAt(20)
                        NDT.Columns.RemoveAt(20)
                        NDT.Columns.RemoveAt(20)
                        NDT.Columns.RemoveAt(20)
                        NDT.Columns.RemoveAt(20)
                        NDT.Columns.RemoveAt(20)
                    End If

                    NDT.Columns.Add("يوم", Type.GetType("System.String"))
                    For Each row As DataRow In NDT.Rows
                        row("يوم") = Convert.ToDateTime(row(2), UK_Culture).DayOfWeek
                    Next
                    NDT.SetColumnsOrder(New String() {"اسم الشركة",
                                        "اسم وحدة العمل",
                                               "رقم الموظف",
                                                "اسم الموظف",
                                                "التاريخ",
                                                "يوم",
                                                "وقت الدخول",
                                                "وقت الخروج",
                                                "المدة",
                                                "مدة الخروج",
                                                "من وقت",
                                                "الى وقت",
                                                "التأخير",
                                                "الخروج المبكر",
                                                "الملاحظات"})

                    '---------Add Missing IN & OUT in the Remarks field-----------'
                    For Each row As DataRow In NDT.Rows
                        If row("وقت الدخول").ToString = "" And Not row("وقت الخروج").ToString = "" Then
                            row("الملاحظات") = "لايوجد دخول"
                        ElseIf Not row("وقت الدخول").ToString = "" And row("وقت الخروج").ToString = "" Then
                            row("الملاحظات") = " لايوجد خروج"
                        End If
                    Next
                    '---------Add Missing IN & OUT in the Remarks field-----------'


                Else
                    For Each row As DataRow In NDT.Rows
                        If Not IsDBNull(row("Status")) Then
                            If row("Status") = "P" Then
                                If Not IsDBNull(row("FromTime")) Then
                                    row("REMARKS") = (row("PermName") + " (" + row("FromTime").ToString + "-" + row("ToTime").ToString + ")")
                                Else
                                    row("REMARKS") = row("PermName")
                                End If
                            ElseIf row("Status") = "L" Then
                                row("REMARKS") = row("LeaveName")
                            ElseIf row("Status") = "H" Then
                                row("REMARKS") = row("HolidayName")
                            Else
                                row("REMARKS") = row("StatusName")
                            End If
                        End If
                    Next

                    NDT.Columns(0).ColumnName = "Employee No"
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Date"
                    NDT.Columns(3).ColumnName = "In Time"
                    NDT.Columns(4).ColumnName = "Out Time"
                    NDT.Columns(5).ColumnName = "Duration"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Out Duration"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "From Time"
                    NDT.Columns(10).ColumnName = "To Time"
                    NDT.Columns(11).ColumnName = "Delay"
                    NDT.Columns(12).ColumnName = "Early Out"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "Daily Utilized"
                    NDT.Columns(15).ColumnName = "Daily Total"
                    NDT.Columns(16).ColumnName = "Daily Remaining"
                    NDT.Columns(17).ColumnName = "Monthly Utilized"
                    NDT.Columns(18).ColumnName = "Monthly Total"
                    NDT.Columns(19).ColumnName = "Monthly Remaining"

                    If EmployeeFilter1.EmployeeId = 0 Then
                        NDT.Columns(20).ColumnName = "Absent Count"
                        NDT.Columns(21).ColumnName = "Rest Day Count"
                        NDT.Columns(22).ColumnName = "Leaves Count"
                        NDT.Columns(23).ColumnName = "Permissions Count"
                        NDT.Columns(24).ColumnName = "Full Day Permissions Count"
                        NDT.Columns(25).ColumnName = "Delay Count"
                        NDT.Columns(26).ColumnName = "Early Out Count"
                        NDT.Columns(27).ColumnName = "Total Overtime"
                        NDT.Columns(28).ColumnName = "Working Days Count"
                    Else
                        NDT.Columns.RemoveAt(20)
                        NDT.Columns.RemoveAt(20)
                        NDT.Columns.RemoveAt(20)
                        NDT.Columns.RemoveAt(20)
                        NDT.Columns.RemoveAt(20)
                        NDT.Columns.RemoveAt(20)
                        NDT.Columns.RemoveAt(20)
                        NDT.Columns.RemoveAt(20)
                        NDT.Columns.RemoveAt(20)
                    End If

                    NDT.Columns.Add("Day", Type.GetType("System.String"))
                    For Each row As DataRow In NDT.Rows
                        row("Day") = Convert.ToDateTime(row("Date"), UK_Culture).DayOfWeek
                    Next

                    NDT.SetColumnsOrder(New String() {"Company Name",
                                                 "Entity Name",
                                                 "Employee No",
                                                 "Employee Name",
                                                 "Date",
                                                 "Day",
                                                 "In Time",
                                                 "Out Time",
                                                 "Duration",
                                                 "Out Duration",
                                                 "From Time",
                                                 "To Time",
                                                 "Delay",
                                                 "Early Out",
                                                 "REMARKS"})
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                    For Each row As DataRow In NDT.Rows
                        If row("In Time").ToString = "" And Not row("Out Time").ToString = "" Then
                            row("Remarks") = "Missing In"
                        ElseIf Not row("In Time").ToString = "" And row("Out Time").ToString = "" Then
                            row("Remarks") = "Missing Out"
                        End If
                    Next
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                End If

            Case Weekly_Report
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    NDT.Columns(23).DataType = System.Type.GetType("System.String")
                    NDT.Columns(25).DataType = System.Type.GetType("System.String")
                    NDT.Columns(27).DataType = System.Type.GetType("System.String")
                    NDT.Columns(35).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 20 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = row(i).ToString()
                                End If
                            ElseIf i = 25 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 27 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 35 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 30 Then '---HolidayArabicName Column
                                If dr(19).ToString = "H" Then
                                    dr(21) = dr(i).ToString
                                    dr(23) = dr(i).ToString
                                End If
                            ElseIf i = 32 Then '---LeaveArabicName Column
                                If dr(19).ToString = "L" Then
                                    dr(21) = dr(i).ToString
                                    dr(23) = dr(i).ToString
                                End If
                            ElseIf i = 34 Then '---PermissionArabicName Column
                                If dr(19).ToString = "P" Then
                                    dr(21) = dr(i).ToString
                                    dr(23) = dr(i).ToString
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
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "اسم الجدول"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "التاريخ"
                    NDT.Columns(6).ColumnName = "اول دخول"
                    NDT.Columns(7).ColumnName = "اخر خروج"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "التأخير"
                    NDT.Columns(9).ColumnName = "الخروج المبكر"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "المدة"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "الوقت الضائع"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "الوقت الاضافي"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "موقع العمل"
                    NDT.Columns.RemoveAt(15)
                    'Day Description
                    NDT.Columns.Add("يوم", Type.GetType("System.String"))
                    For Each row As DataRow In NDT.Rows
                        row("يوم") = Convert.ToDateTime(row(5), UK_Culture).DayOfWeek
                    Next

                    NDT.SetColumnsOrder(New String() {"رقم الموظف",
                                   "اسم الموظف",
                                   "اسم وحدة العمل",
                                   "اسم الشركة",
                                   "اسم الجدول",
                                   "التاريخ",
                                   "يوم",
                                   "اول دخول",
                                   "اخر خروج",
                                   "التأخير",
                                   "الخروج المبكر",
                                   "المدة",
                                   "الوقت الضائع",
                                   "الملاحظات",
                                   "الوقت الاضافي"})
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                    For Each row As DataRow In NDT.Rows
                        If row(7).ToString = "" And Not row(8).ToString = "" Then
                            row(13) = row(13) + " -لايوجد دخول"
                        ElseIf Not row(7).ToString = "" And row(8).ToString = "" Then
                            row(13) = row(13) + " -لايوجد خروج"
                        End If
                    Next
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                Else

                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    NDT.Columns(23).DataType = System.Type.GetType("System.String")
                    NDT.Columns(25).DataType = System.Type.GetType("System.String")
                    NDT.Columns(27).DataType = System.Type.GetType("System.String")
                    NDT.Columns(35).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 20 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = row(i).ToString()
                                End If
                            ElseIf i = 25 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 27 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 35 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 29 Then '---HolidayName Column
                                If dr(19).ToString = "H" Then
                                    dr(21) = dr(i).ToString
                                    dr(22) = dr(i).ToString
                                End If
                            ElseIf i = 31 Then '---LeaveName Column
                                If dr(19).ToString = "L" Then
                                    dr(21) = dr(i).ToString
                                    dr(22) = dr(i).ToString
                                End If
                            ElseIf i = 33 Then '---PermissionName Column
                                If dr(19).ToString = "P" Then
                                    dr(21) = dr(i).ToString
                                    dr(22) = dr(i).ToString
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
                    NDT.Columns(4).ColumnName = "Schedule Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Date"
                    NDT.Columns(6).ColumnName = "First In"
                    NDT.Columns(7).ColumnName = "Last Out"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Delay"
                    NDT.Columns(9).ColumnName = "Early Out"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Duration"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "Lost Time"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "OverTime"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "Work Location"
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)

                    NDT.Columns.Add("Day", Type.GetType("System.String"))
                    For Each row As DataRow In NDT.Rows
                        row("Day") = Convert.ToDateTime(row("Date"), UK_Culture).DayOfWeek
                    Next

                    NDT.SetColumnsOrder(New String() {"Emp No.",
                                    "Employee Name",
                                    "Entity Name",
                                    "Company Name",
                                    "Schedule Name",
                                    "Date",
                                    "Day",
                                    "First In",
                                    "Last Out",
                                    "Delay",
                                    "Early Out",
                                    "Duration",
                                    "Lost Time",
                                    "Remarks",
                                    "OverTime"})
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                    For Each row As DataRow In NDT.Rows
                        If row(7).ToString = "" And Not row(8).ToString = "" Then
                            row(13) = row(13) + " -Missing In"
                        ElseIf Not row(7).ToString = "" And row(8).ToString = "" Then
                            row(13) = row(13) + " -Missing Out"
                        End If
                    Next
                    '---------Add Missing IN & OUT in the Remarks field-----------'

                End If

            Case DailyReport_Bucket
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    NDT.Columns(23).DataType = System.Type.GetType("System.String")
                    NDT.Columns(25).DataType = System.Type.GetType("System.String")
                    NDT.Columns(27).DataType = System.Type.GetType("System.String")
                    NDT.Columns(35).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 20 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = row(i).ToString()
                                End If
                            ElseIf i = 25 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 27 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 35 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 30 Then '---HolidayArabicName Column
                                If dr(19).ToString = "H" Then
                                    dr(21) = dr(i).ToString
                                    dr(23) = dr(i).ToString
                                End If
                            ElseIf i = 32 Then '---LeaveArabicName Column
                                If dr(19).ToString = "L" Then
                                    dr(21) = dr(i).ToString
                                    dr(23) = dr(i).ToString
                                End If
                            ElseIf i = 34 Then '---PermissionArabicName Column
                                If dr(19).ToString = "P" Then
                                    dr(21) = dr(i).ToString
                                    dr(23) = dr(i).ToString
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
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "اسم الجدول"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "التاريخ"
                    NDT.Columns(6).ColumnName = "اول دخول"
                    NDT.Columns(7).ColumnName = "اخر خروج"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "التأخير"
                    NDT.Columns(9).ColumnName = "الخروج المبكر"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "الوقت الضائع"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "الوقت الاضافي"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "موقع العمل"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "الرصيد اليومي"
                    NDT.Columns(15).ColumnName = "مدة العمل مع الرصيد اليومي"
                    NDT.Columns(16).ColumnName = "المجموع"
                    NDT.Columns(17).ColumnName = "المدة"
                    NDT.Columns.RemoveAt(18)
                    'Day Description
                    NDT.Columns.Add("يوم", Type.GetType("System.String"))
                    For Each row As DataRow In NDT.Rows
                        row("يوم") = Convert.ToDateTime(row(5), UK_Culture).DayOfWeek
                    Next

                    NDT.SetColumnsOrder(New String() {"رقم الموظف",
                                   "اسم الموظف",
                                   "اسم وحدة العمل",
                                   "اسم الشركة",
                                   "اسم الجدول",
                                   "التاريخ",
                                   "يوم",
                                   "اول دخول",
                                   "اخر خروج",
                                   "التأخير",
                                   "الخروج المبكر",
                                   "المدة",
                                   "الرصيد اليومي",
                                   "مدة العمل مع الرصيد اليومي",
                                   "المجموع",
                                   "الوقت الضائع",
                                   "الملاحظات",
                                   "الوقت الاضافي"})
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                    For Each row As DataRow In NDT.Rows
                        If row(7).ToString = "" And Not row(8).ToString = "" Then
                            row("الملاحظات") = row("الملاحظات") + " -لايوجد دخول"
                        ElseIf Not row(7).ToString = "" And row(8).ToString = "" Then
                            row("الملاحظات") = row("الملاحظات") + " -لايوجد خروج"
                        End If
                    Next
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                Else

                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    NDT.Columns(23).DataType = System.Type.GetType("System.String")
                    NDT.Columns(25).DataType = System.Type.GetType("System.String")
                    NDT.Columns(27).DataType = System.Type.GetType("System.String")
                    NDT.Columns(35).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 20 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = row(i).ToString()
                                End If
                            ElseIf i = 25 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 27 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 35 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 29 Then '---HolidayName Column
                                If dr(19).ToString = "H" Then
                                    dr(21) = dr(i).ToString
                                    dr(22) = dr(i).ToString
                                End If
                            ElseIf i = 31 Then '---LeaveName Column
                                If dr(19).ToString = "L" Then
                                    dr(21) = dr(i).ToString
                                    dr(22) = dr(i).ToString
                                End If
                            ElseIf i = 33 Then '---PermissionName Column
                                If dr(19).ToString = "P" Then
                                    dr(21) = dr(i).ToString
                                    dr(22) = dr(i).ToString
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
                    NDT.Columns(4).ColumnName = "Schedule Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Date"
                    NDT.Columns(6).ColumnName = "First In"
                    NDT.Columns(7).ColumnName = "Last Out"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Delay"
                    NDT.Columns(9).ColumnName = "Early Out"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "Lost Time"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "OverTime"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "Work Location"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "Daily Bucket/Balance"
                    NDT.Columns(15).ColumnName = "Total Work Duration & Daily Balance"
                    NDT.Columns(16).ColumnName = "Total"
                    NDT.Columns(17).ColumnName = "Duration"
                    NDT.Columns.RemoveAt(18)

                    NDT.Columns.Add("Day", Type.GetType("System.String"))
                    For Each row As DataRow In NDT.Rows
                        row("Day") = Convert.ToDateTime(row("Date"), UK_Culture).DayOfWeek
                    Next

                    NDT.SetColumnsOrder(New String() {"Emp No.",
                                    "Employee Name",
                                    "Entity Name",
                                    "Company Name",
                                    "Schedule Name",
                                    "Date",
                                    "Day",
                                    "First In",
                                    "Last Out",
                                    "Delay",
                                    "Early Out",
                                    "Duration",
                                    "Daily Bucket/Balance",
                                    "Total Work Duration & Daily Balance",
                                    "Total",
                                    "Lost Time",
                                    "Remarks",
                                    "OverTime"})
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                    For Each row As DataRow In NDT.Rows
                        If row(7).ToString = "" And Not row(8).ToString = "" Then
                            row("Remarks") = row("Remarks") + " -Missing In"
                        ElseIf Not row(7).ToString = "" And row(8).ToString = "" Then
                            row("Remarks") = row("Remarks") + " -Missing Out"
                        End If
                    Next
                    '---------Add Missing IN & OUT in the Remarks field-----------'

                End If

            Case DailyReport
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    NDT.Columns(23).DataType = System.Type.GetType("System.String")
                    NDT.Columns(25).DataType = System.Type.GetType("System.String")
                    NDT.Columns(27).DataType = System.Type.GetType("System.String")
                    NDT.Columns(35).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 20 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = row(i).ToString()
                                End If
                            ElseIf i = 25 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 27 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 35 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 30 Then '---HolidayArabicName Column
                                If dr(19).ToString = "H" Then
                                    dr(21) = dr(i).ToString
                                    dr(23) = dr(i).ToString
                                End If
                            ElseIf i = 32 Then '---LeaveArabicName Column
                                If dr(19).ToString = "L" Then
                                    dr(21) = dr(i).ToString
                                    dr(23) = dr(i).ToString
                                End If
                            ElseIf i = 34 Then '---PermissionArabicName Column
                                If dr(19).ToString = "P" Then
                                    dr(21) = dr(i).ToString
                                    dr(23) = dr(i).ToString
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
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "اسم الجدول"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "التاريخ"
                    NDT.Columns(6).ColumnName = "اول دخول"
                    NDT.Columns(7).ColumnName = "اخر خروج"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "التأخير"
                    NDT.Columns(9).ColumnName = "الخروج المبكر"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "المدة"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "الوقت الضائع"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "الوقت الاضافي"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "موقع العمل"
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns(15).ColumnName = "جهاز الدخول"
                    NDT.Columns(16).ColumnName = "جهاز الخروج"
                    'Day Description
                    NDT.Columns.Add("يوم", Type.GetType("System.String"))
                    For Each row As DataRow In NDT.Rows
                        row("يوم") = Convert.ToDateTime(row(5), UK_Culture).DayOfWeek
                    Next

                    NDT.SetColumnsOrder(New String() {"رقم الموظف",
                                   "اسم الموظف",
                                   "اسم وحدة العمل",
                                   "اسم الشركة",
                                   "اسم الجدول",
                                   "التاريخ",
                                   "يوم",
                                   "اول دخول",
                                   "اخر خروج",
                                   "التأخير",
                                   "الخروج المبكر",
                                   "المدة",
                                   "الوقت الضائع",
                                   "الملاحظات",
                                   "الوقت الاضافي"})
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                    For Each row As DataRow In NDT.Rows
                        If row(7).ToString = "" And Not row(8).ToString = "" Then
                            row(13) = row(13) + " -لايوجد دخول"
                        ElseIf Not row(7).ToString = "" And row(8).ToString = "" Then
                            row(13) = row(13) + " -لايوجد خروج"
                        End If
                    Next
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                Else

                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    NDT.Columns(23).DataType = System.Type.GetType("System.String")
                    NDT.Columns(25).DataType = System.Type.GetType("System.String")
                    NDT.Columns(27).DataType = System.Type.GetType("System.String")
                    NDT.Columns(35).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 20 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = row(i).ToString()
                                End If
                            ElseIf i = 25 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 27 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 35 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 29 Then '---HolidayName Column
                                If dr(19).ToString = "H" Then
                                    dr(21) = dr(i).ToString
                                    dr(22) = dr(i).ToString
                                End If
                            ElseIf i = 31 Then '---LeaveName Column
                                If dr(19).ToString = "L" Then
                                    dr(21) = dr(i).ToString
                                    dr(22) = dr(i).ToString
                                End If
                            ElseIf i = 33 Then '---PermissionName Column
                                If dr(19).ToString = "P" Then
                                    dr(21) = dr(i).ToString
                                    dr(22) = dr(i).ToString
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
                    NDT.Columns(4).ColumnName = "Schedule Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Date"
                    NDT.Columns(6).ColumnName = "First In"
                    NDT.Columns(7).ColumnName = "Last Out"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Delay"
                    NDT.Columns(9).ColumnName = "Early Out"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Duration"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "Lost Time"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "OverTime"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "Work Location"
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns(15).ColumnName = "In Reader"
                    NDT.Columns(16).ColumnName = "Out Reader"

                    NDT.Columns.Add("Day", Type.GetType("System.String"))
                    For Each row As DataRow In NDT.Rows
                        row("Day") = Convert.ToDateTime(row("Date"), UK_Culture).DayOfWeek
                    Next

                    NDT.SetColumnsOrder(New String() {"Emp No.",
                                    "Employee Name",
                                    "Entity Name",
                                    "Company Name",
                                    "Schedule Name",
                                    "Date",
                                    "Day",
                                    "First In",
                                    "Last Out",
                                    "Delay",
                                    "Early Out",
                                    "Duration",
                                    "Lost Time",
                                    "Remarks",
                                    "OverTime"})
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                    For Each row As DataRow In NDT.Rows
                        If row(7).ToString = "" And Not row(8).ToString = "" Then
                            row(13) = row(13) + " -Missing In"
                        ElseIf Not row(7).ToString = "" And row(8).ToString = "" Then
                            row(13) = row(13) + " -Missing Out"
                        End If
                    Next
                    '---------Add Missing IN & OUT in the Remarks field-----------'

                End If

            Case AbsentEmployeeReport_WithParam
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(19).DataType = System.Type.GetType("System.String")
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
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 19 Then
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
                    NDT.Columns(6).ColumnName = "تاريخ الحركة الخاطئة"
                    NDT.Columns(7).ColumnName = "وقت الحركة الخاطئة"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    'NDT.Columns.RemoveAt(6)
                    'NDT.Columns.RemoveAt(6)
                    'NDT.Columns.RemoveAt(6)

                Else
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(19).DataType = System.Type.GetType("System.String")
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
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 19 Then
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
                    NDT.Columns(6).ColumnName = "Invalid Date"
                    NDT.Columns(7).ColumnName = "Invalid Time"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)


                End If

            Case IncompleteTransactions_Advance
                NDT = DT
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "اسم الموظف باللغة العربية"
                    NDT.Columns(3).ColumnName = "اسم وحدة العمل"
                    NDT.Columns(4).ColumnName = "اسم وحدة العمل باللغة العربية"
                    NDT.Columns(5).ColumnName = "اسم الشركة"
                    NDT.Columns(6).ColumnName = "اسم الشركة باللغة العربية"
                    NDT.Columns(7).ColumnName = "لايوجد دخول"
                    NDT.Columns(8).ColumnName = "لايوجد خروج"
                    NDT.Columns(9).ColumnName = "مجموع لايوجد دخول\لايوجد خروج"

                Else
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns(2).ColumnName = "Employee Arabic Name"
                    NDT.Columns(3).ColumnName = "Entity Name"
                    NDT.Columns(4).ColumnName = "Entity Arabic Name"
                    NDT.Columns(5).ColumnName = "Company Name"
                    NDT.Columns(6).ColumnName = "Company Arabic Name"
                    NDT.Columns(7).ColumnName = "Missing In"
                    NDT.Columns(8).ColumnName = "Missing Out"
                    NDT.Columns(9).ColumnName = "Total Missing In\Out"

                End If
            Case DailyReport_Invalid
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    NDT.Columns(23).DataType = System.Type.GetType("System.String")
                    NDT.Columns(25).DataType = System.Type.GetType("System.String")
                    NDT.Columns(27).DataType = System.Type.GetType("System.String")
                    NDT.Columns(35).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 20 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = row(i).ToString()
                                End If
                            ElseIf i = 25 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 27 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 35 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 30 Then '---HolidayArabicName Column
                                If dr(19).ToString = "H" Then
                                    dr(21) = dr(i).ToString
                                    dr(23) = dr(i).ToString
                                End If
                            ElseIf i = 32 Then '---LeaveArabicName Column
                                If dr(19).ToString = "L" Then
                                    dr(21) = dr(i).ToString
                                    dr(23) = dr(i).ToString
                                End If
                            ElseIf i = 34 Then '---PermissionArabicName Column
                                If dr(19).ToString = "P" Then
                                    dr(21) = dr(i).ToString
                                    dr(23) = dr(i).ToString
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
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "اسم الجدول"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "التاريخ"
                    NDT.Columns(6).ColumnName = "اول دخول"
                    NDT.Columns(7).ColumnName = "اخر خروج"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "التأخير"
                    NDT.Columns(9).ColumnName = "الخروج المبكر"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "المدة"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "الوقت الضائع"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "الوقت الاضافي"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "موقع العمل"
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns(15).ColumnName = "جهاز حركة الدخول"
                    NDT.Columns(16).ColumnName = "جهاز حركة الخروج"
                    'Day Description
                    NDT.Columns.Add("يوم", Type.GetType("System.String"))
                    For Each row As DataRow In NDT.Rows
                        row("يوم") = Convert.ToDateTime(row(5), UK_Culture).DayOfWeek
                    Next

                    NDT.SetColumnsOrder(New String() {"رقم الموظف",
                                   "اسم الموظف",
                                   "اسم وحدة العمل",
                                   "اسم الشركة",
                                   "اسم الجدول",
                                   "التاريخ",
                                   "يوم",
                                   "اول دخول",
                                   "اخر خروج",
                                   "التأخير",
                                   "الخروج المبكر",
                                   "المدة",
                                   "الوقت الضائع",
                                   "الملاحظات",
                                   "الوقت الاضافي"})
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                    For Each row As DataRow In NDT.Rows
                        If row("اول دخول").ToString = "" And Not row("اخر خروج").ToString = "" Then
                            row("الملاحظات") = row("الملاحظات") + " -لايوجد دخول"
                        ElseIf Not row("اول دخول").ToString = "" And row("اخر خروج").ToString = "" Then
                            row("الملاحظات") = row("الملاحظات") + " -لايوجد خروج"
                        End If
                    Next
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                Else

                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    NDT.Columns(23).DataType = System.Type.GetType("System.String")
                    NDT.Columns(25).DataType = System.Type.GetType("System.String")
                    NDT.Columns(27).DataType = System.Type.GetType("System.String")
                    NDT.Columns(35).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 20 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = row(i).ToString()
                                End If
                            ElseIf i = 25 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 27 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 35 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 29 Then '---HolidayName Column
                                If dr(19).ToString = "H" Then
                                    dr(21) = dr(i).ToString
                                    dr(22) = dr(i).ToString
                                End If
                            ElseIf i = 31 Then '---LeaveName Column
                                If dr(19).ToString = "L" Then
                                    dr(21) = dr(i).ToString
                                    dr(22) = dr(i).ToString
                                End If
                            ElseIf i = 33 Then '---PermissionName Column
                                If dr(19).ToString = "P" Then
                                    dr(21) = dr(i).ToString
                                    dr(22) = dr(i).ToString
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
                    NDT.Columns(4).ColumnName = "Schedule Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Date"
                    NDT.Columns(6).ColumnName = "First In"
                    NDT.Columns(7).ColumnName = "Last Out"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Delay"
                    NDT.Columns(9).ColumnName = "Early Out"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Duration"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "Lost Time"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "OverTime"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "Work Location"
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns(15).ColumnName = "In Reader"
                    NDT.Columns(16).ColumnName = "Out Reader"

                    NDT.Columns.Add("Day", Type.GetType("System.String"))
                    For Each row As DataRow In NDT.Rows
                        row("Day") = Convert.ToDateTime(row("Date"), UK_Culture).DayOfWeek
                    Next

                    NDT.SetColumnsOrder(New String() {"Emp No.",
                                    "Employee Name",
                                    "Entity Name",
                                    "Company Name",
                                    "Schedule Name",
                                    "Date",
                                    "Day",
                                    "First In",
                                    "Last Out",
                                    "Delay",
                                    "Early Out",
                                    "Duration",
                                    "Lost Time",
                                    "Remarks",
                                    "OverTime"})
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                    For Each row As DataRow In NDT.Rows
                        If row("First In").ToString = "" And Not row("Last Out").ToString = "" Then
                            row("Remarks") = row("Remarks") + " -Missing In"
                        ElseIf Not row("First In").ToString = "" And row("Last Out").ToString = "" Then
                            row("Remarks") = row("Remarks") + " -Missing Out"
                        End If
                    Next
                    '---------Add Missing IN & OUT in the Remarks field-----------'

                End If
            Case DetailedTransactions_Invalid


                NDT.Columns(3).DataType = System.Type.GetType("System.String")
                NDT.Columns(4).DataType = System.Type.GetType("System.String")
                NDT.Columns(5).DataType = System.Type.GetType("System.String")
                NDT.Columns(6).DataType = System.Type.GetType("System.String")
                NDT.Columns(9).DataType = System.Type.GetType("System.String")
                NDT.Columns(20).DataType = System.Type.GetType("System.String")
                NDT.Columns(21).DataType = System.Type.GetType("System.String")
                NDT.Columns(22).DataType = System.Type.GetType("System.String")
                NDT.Columns(23).DataType = System.Type.GetType("System.String")
                NDT.Columns(24).DataType = System.Type.GetType("System.String")
                For Each row As DataRow In DT.Rows
                    Dim dr As DataRow = NDT.NewRow
                    For i As Integer = 0 To NDT.Columns.Count - 1
                        dr(i) = row(i)
                        If i = 3 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                            End If
                        ElseIf i = 4 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 5 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 6 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToString(row(i)).ToString '("HH:mm")
                            End If
                        ElseIf i = 9 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = row(i).ToString()
                            End If

                        ElseIf i = 21 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 22 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 23 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 24 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        End If
                    Next
                    NDT.Rows.Add(dr)
                Next



                If (Lang = CtlCommon.Lang.AR) Then
                    For Each row As DataRow In NDT.Rows
                        If Not IsDBNull(row("Status")) Then
                            If row("Status") = "P" Then
                                If Not IsDBNull(row("FromTime")) Then
                                    row("REMARKS") = (row("PermArabicName").ToString + " (" + row("FromTime").ToString + "-" + row("ToTime").ToString + ")")
                                Else
                                    row("REMARKS") = row("PermArabicName").ToString
                                End If

                            ElseIf row("Status") = "L" Then
                                row("REMARKS") = row("LeaveArabicName")
                            ElseIf row("Status") = "H" Then
                                row("REMARKS") = row("HolidayArabicName")
                            Else
                                row("REMARKS") = row("StatusNameArb")
                            End If
                        End If
                    Next

                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "التاريخ"
                    NDT.Columns(3).ColumnName = "وقت الدخول"
                    NDT.Columns(4).ColumnName = "وقت الخروج"
                    NDT.Columns(5).ColumnName = "المدة"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "مدة الخروج"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "من وقت"
                    NDT.Columns(10).ColumnName = "الى وقت"
                    NDT.Columns(11).ColumnName = "التأخير"
                    NDT.Columns(12).ColumnName = "الخروج المبكر"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.Add("يوم", Type.GetType("System.String"))
                    For Each row As DataRow In NDT.Rows
                        row("يوم") = Convert.ToDateTime(row(2), UK_Culture).DayOfWeek
                    Next
                    NDT.SetColumnsOrder(New String() {"اسم الشركة",
                                        "اسم وحدة العمل",
                                               "رقم الموظف",
                                                "اسم الموظف",
                                                "التاريخ",
                                                "يوم",
                                                "وقت الدخول",
                                                "وقت الخروج",
                                                "المدة",
                                                "مدة الخروج",
                                                "من وقت",
                                                "الى وقت",
                                                "التأخير",
                                                "الخروج المبكر",
                                                "الملاحظات"})

                    '---------Add Missing IN & OUT in the Remarks field-----------'
                    For Each row As DataRow In NDT.Rows
                        If row("وقت الدخول").ToString = "" And Not row("وقت الخروج").ToString = "" Then
                            row("الملاحظات") = "لايوجد دخول"
                        ElseIf Not row("وقت الدخول").ToString = "" And row("وقت الخروج").ToString = "" Then
                            row("الملاحظات") = " لايوجد خروج"
                        End If
                    Next
                    '---------Add Missing IN & OUT in the Remarks field-----------'


                Else
                    For Each row As DataRow In NDT.Rows
                        If Not IsDBNull(row("Status")) Then
                            If row("Status") = "P" Then
                                If Not IsDBNull(row("FromTime")) Then
                                    row("REMARKS") = (row("PermName") + " (" + row("FromTime").ToString + "-" + row("ToTime").ToString + ")")
                                Else
                                    row("REMARKS") = row("PermName")
                                End If
                            ElseIf row("Status") = "L" Then
                                row("REMARKS") = row("LeaveName")
                            ElseIf row("Status") = "H" Then
                                row("REMARKS") = row("HolidayName")
                            Else
                                row("REMARKS") = row("StatusName")
                            End If
                        End If
                    Next

                    NDT.Columns(0).ColumnName = "Employee No"
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Date"
                    NDT.Columns(3).ColumnName = "In Time"
                    NDT.Columns(4).ColumnName = "Out Time"
                    NDT.Columns(5).ColumnName = "Duration"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Out Duration"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "From Time"
                    NDT.Columns(10).ColumnName = "To Time"
                    NDT.Columns(11).ColumnName = "Delay"
                    NDT.Columns(12).ColumnName = "Early Out"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)

                    NDT.Columns.Add("Day", Type.GetType("System.String"))
                    For Each row As DataRow In NDT.Rows
                        row("Day") = Convert.ToDateTime(row("Date"), UK_Culture).DayOfWeek
                    Next

                    NDT.SetColumnsOrder(New String() {"Company Name",
                                                 "Entity Name",
                                                 "Employee No",
                                                 "Employee Name",
                                                 "Date",
                                                 "Day",
                                                 "In Time",
                                                 "Out Time",
                                                 "Duration",
                                                 "Out Duration",
                                                 "From Time",
                                                 "To Time",
                                                 "Delay",
                                                 "Early Out",
                                                 "REMARKS"})
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                    For Each row As DataRow In NDT.Rows
                        If row("In Time").ToString = "" And Not row("Out Time").ToString = "" Then
                            row("Remarks") = "Missing In"
                        ElseIf Not row("In Time").ToString = "" And row("Out Time").ToString = "" Then
                            row("Remarks") = "Missing Out"
                        End If
                    Next
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                End If

            Case AttendnaceReport_ADASI

                NDT.Columns(8).DataType = System.Type.GetType("System.String") 'date
                NDT.Columns(11).DataType = System.Type.GetType("System.String")
                NDT.Columns(12).DataType = System.Type.GetType("System.String")
                NDT.Columns(13).DataType = System.Type.GetType("System.String")
                NDT.Columns(14).DataType = System.Type.GetType("System.String")
                NDT.Columns(19).DataType = System.Type.GetType("System.String")
                NDT.Columns(21).DataType = System.Type.GetType("System.String")
                NDT.Columns(23).DataType = System.Type.GetType("System.String")
                NDT.Columns(26).DataType = System.Type.GetType("System.String")
                NDT.Columns(29).DataType = System.Type.GetType("System.String")

                For Each row As DataRow In DT.Rows
                    Dim dr As DataRow = NDT.NewRow
                    For i As Integer = 0 To NDT.Columns.Count - 1
                        dr(i) = row(i)
                        If i = 8 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                            End If
                        ElseIf i = 11 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 12 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 13 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 14 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 19 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 21 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 23 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 26 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 29 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        End If
                    Next
                    NDT.Rows.Add(dr)
                Next

                If Lang = CtlCommon.Lang.AR Then
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "اسم المدير"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "اسم وحدة العمل"
                    NDT.Columns(4).ColumnName = "التاريخ"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "وصف عدم الحضور"
                    NDT.Columns(6).ColumnName = "وقت الدخول"
                    NDT.Columns(7).ColumnName = "وقت الخروج"
                    NDT.Columns(8).ColumnName = "مجموع مدة العمل"
                    NDT.Columns(9).ColumnName = "مجموع مدة العمل الثابت"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "الجدول البداية - الانتهاء"
                    NDT.Columns(11).ColumnName = "الغياب بالساعات"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "الاجازات"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "الغياب"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "الوقت الاضافي"
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns(15).ColumnName = "العطل الرسمية و الاعياد"
                    NDT.Columns(16).ColumnName = "مجموع ساعات العمل الثابت"
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns(17).ColumnName = "مجموع ايام العمل"
                    NDT.Columns(18).ColumnName = "مجموع ساعات العمل الفعلي"
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns(19).ColumnName = "مجموع ايام العمل الثابت"
                    NDT.Columns.RemoveAt(20)
                    NDT.Columns.RemoveAt(20)
                    NDT.Columns.RemoveAt(20)
                    NDT.Columns.RemoveAt(20)

                    NDT.Columns.Add("اليوم", Type.GetType("System.String"))
                    For Each row As DataRow In NDT.Rows
                        row("اليوم") = Convert.ToDateTime(row("التاريخ"), UK_Culture).DayOfWeek
                    Next

                    NDT.SetColumnsOrder(New String() {"رقم الموظف",
                             "اسم الموظف",
                              "اسم المدير",
                             "اسم وحدة العمل",
                             "التاريخ",
                             "اليوم",
                             "وصف عدم الحضور",
                             "وقت الدخول",
                             "وقت الخروج",
                             "مجموع مدة العمل",
                             "مجموع مدة العمل الثابت",
                             "الجدول البداية - الانتهاء",
                             "الغياب بالساعات",
                             "الاجازات",
                             "الغياب",
                             "الوقت الاضافي",
                             "العطل الرسمية و الاعياد",
                             "مجموع ساعات العمل الثابت",
                             "مجموع ايام العمل الثابت",
                             "مجموع ساعات العمل الفعلي",
                             "مجموع ايام العمل"})

                Else
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "ID"
                    NDT.Columns(1).ColumnName = "Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Line Manager"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "Department"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Date"
                    NDT.Columns(5).ColumnName = "Descripiton of Full Day Absence"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Time In"
                    NDT.Columns(7).ColumnName = "Time Out"
                    NDT.Columns(8).ColumnName = "Total Working Hours"
                    NDT.Columns(9).ColumnName = "Fix Working Hours"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Start-End of Working Schedule(As per Policy)"
                    NDT.Columns(11).ColumnName = "Absence In Hours"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "Leaves"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "Absent"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "Extra Working Hours"
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns(15).ColumnName = "Weekend Public Holiday"
                    NDT.Columns(16).ColumnName = "Total Fixed Hours"
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns(17).ColumnName = "Total Actual Days"
                    NDT.Columns(18).ColumnName = "Total Actual W.H"
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns(19).ColumnName = "Total Fixed Days"
                    NDT.Columns.RemoveAt(20)
                    NDT.Columns.RemoveAt(20)
                    NDT.Columns.RemoveAt(20)
                    NDT.Columns.RemoveAt(20)
                    NDT.Columns.Add("Day", Type.GetType("System.String"))
                    For Each row As DataRow In NDT.Rows
                        row("Day") = Convert.ToDateTime(row("Date"), UK_Culture).DayOfWeek
                    Next

                    NDT.SetColumnsOrder(New String() {"ID",
                                                 "Name",
                                                 "Line Manager",
                                                 "Department",
                                                 "Date",
                                                 "Day",
                                                 "Descripiton of Full Day Absence",
                                                 "Time In",
                                                 "Time Out",
                                                 "Total Working Hours",
                                                 "Fix Working Hours",
                                                 "Start-End of Working Schedule(As per Policy)",
                                                 "Absence In Hours",
                                                 "Leaves",
                                                 "Absent",
                                                 "Extra Working Hours",
                                                 "Weekend Public Holiday",
                                                 "Total Fixed Hours",
                                                 "Total Fixed Days",
                                                 "Total Actual W.H",
                                                 "Total Actual Days"})


                End If

            Case EmployeeOutDuration
                'NDT = DT

                NDT.Columns(4).DataType = System.Type.GetType("System.String")
                NDT.Columns(6).DataType = System.Type.GetType("System.String")
                NDT.Columns(7).DataType = System.Type.GetType("System.String")

                For Each row As DataRow In DT.Rows
                    Dim dr As DataRow = NDT.NewRow
                    For i As Integer = 0 To NDT.Columns.Count - 1
                        dr(i) = row(i)
                        If i = 4 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                            End If
                        ElseIf i = 6 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 7 Then
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
                    NDT.Columns(3).ColumnName = "مدة الخروج"
                    NDT.Columns(4).ColumnName = "من وقت"
                    NDT.Columns(5).ColumnName = "الى وقت"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(8)

                Else
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Date"
                    NDT.Columns(3).ColumnName = "Out Duration"
                    NDT.Columns(4).ColumnName = "From Time"
                    NDT.Columns(5).ColumnName = "To Time"
                    NDT.Columns(6).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                End If

            Case EmpDetailedTimeAttendance

                NDT.Columns(11).DataType = System.Type.GetType("System.String") 'date
                NDT.Columns(12).DataType = System.Type.GetType("System.String")
                NDT.Columns(13).DataType = System.Type.GetType("System.String")
                NDT.Columns(17).DataType = System.Type.GetType("System.String")
                NDT.Columns(25).DataType = System.Type.GetType("System.String")
                NDT.Columns(26).DataType = System.Type.GetType("System.String")
                NDT.Columns(27).DataType = System.Type.GetType("System.String")
                'NDT.Columns(28).DataType = System.Type.GetType("System.String")
                NDT.Columns(31).DataType = System.Type.GetType("System.String")
                NDT.Columns(37).DataType = System.Type.GetType("System.String")
                NDT.Columns(43).DataType = System.Type.GetType("System.String")

                For Each row As DataRow In DT.Rows
                    Dim dr As DataRow = NDT.NewRow
                    For i As Integer = 0 To NDT.Columns.Count - 1
                        dr(i) = row(i)
                        If i = 11 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                            End If
                        ElseIf i = 12 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 13 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 17 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 25 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 26 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 27 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                            'ElseIf i = 28 Then
                            '    If Not dr(i) Is DBNull.Value Then
                            '        dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            '    End If
                        ElseIf i = 31 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 37 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 43 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToString(row(i)).ToString
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
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "الجدول"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "التاريخ"
                    NDT.Columns(6).ColumnName = "وقت الدخول"
                    NDT.Columns(7).ColumnName = "وقت الخروج"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "ساعات العمل"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "اسم العطلة"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "نوع الاجازة"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "نوع المغادرة"
                    NDT.Columns(12).ColumnName = "العمل الاضافي"
                    NDT.Columns(13).ColumnName = "التأخير"
                    NDT.Columns(14).ColumnName = "الخروج المبكر"
                    NDT.Columns(15).ColumnName = "مدة الخروج"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns(16).ColumnName = "التـأخير والخروج المبكر"
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns(17).ColumnName = "مجموع ساعات العمل الفعلية"
                    NDT.Columns(18).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns(19).ColumnName = "لم يستكمل ساعات العمل"
                    NDT.Columns.RemoveAt(20)

                    '---------Add Missing IN & OUT in the Remarks field-----------'
                    For Each row As DataRow In NDT.Rows
                        If row(6).ToString = "" And Not row(7).ToString = "" Then
                            row(18) = row(18) + " -لايوجد دخول"
                        ElseIf Not row(6).ToString = "" And row(7).ToString = "" Then
                            row(18) = row(18) + " -لايوجد خروج"
                        End If

                        If row(19).ToString = "1" Then
                            row(19) = "نعم"
                        ElseIf (row(19).ToString = "0" Or row(19).ToString = String.Empty) Then
                            row(19) = "لا"
                        End If
                    Next
                    '---------Add Missing IN & OUT in the Remarks field-----------'

                Else

                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Employee No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Schedule Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Date"
                    NDT.Columns(6).ColumnName = "In Time"
                    NDT.Columns(7).ColumnName = "Out Time"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Total Work Hours"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "Holiday Name"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Leave Name"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "Permission Name"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "OverTime"
                    NDT.Columns(13).ColumnName = "Delay"
                    NDT.Columns(14).ColumnName = "Early Out"
                    NDT.Columns(15).ColumnName = "Out Duration"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns(16).ColumnName = "Delay & Early Out"
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns(17).ColumnName = "Actual Work Duration"
                    NDT.Columns(18).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns(19).ColumnName = "Not Complete No. Of Hours"
                    NDT.Columns.RemoveAt(20)

                    '---------Add Missing IN & OUT in the Remarks field-----------'
                    For Each row As DataRow In NDT.Rows
                        If row(6).ToString = "" And Not row(7).ToString = "" Then
                            row(18) = row(18) + " -Missing In"
                        ElseIf Not row(6).ToString = "" And row(7).ToString = "" Then
                            row(18) = row(18) + " -Missing Out"
                        End If

                        If row(19).ToString = "1" Then
                            row(19) = "Yes"
                        ElseIf (row(19).ToString = "0" Or row(19).ToString = String.Empty) Then
                            row(19) = "No"
                        End If
                    Next
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                End If

            Case DailyReport_TAReason
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    NDT.Columns(23).DataType = System.Type.GetType("System.String")
                    NDT.Columns(25).DataType = System.Type.GetType("System.String")
                    NDT.Columns(27).DataType = System.Type.GetType("System.String")
                    NDT.Columns(35).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 20 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = row(i).ToString()
                                End If
                            ElseIf i = 25 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 27 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 35 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 30 Then '---HolidayArabicName Column
                                If dr(19).ToString = "H" Then
                                    dr(21) = dr(i).ToString
                                    dr(23) = dr(i).ToString
                                End If
                            ElseIf i = 32 Then '---LeaveArabicName Column
                                If dr(19).ToString = "L" Then
                                    dr(21) = dr(i).ToString
                                    dr(23) = dr(i).ToString
                                End If
                            ElseIf i = 34 Then '---PermissionArabicName Column
                                If dr(19).ToString = "P" Then
                                    dr(21) = dr(i).ToString
                                    dr(23) = dr(i).ToString
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
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "اسم الجدول"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "التاريخ"
                    NDT.Columns(6).ColumnName = "اول دخول"
                    NDT.Columns(7).ColumnName = "اخر خروج"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "التأخير"
                    NDT.Columns(9).ColumnName = "الخروج المبكر"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "المدة"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "الوقت الضائع"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "الوقت الاضافي"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "موقع العمل"
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns(15).ColumnName = "سبب الدخول"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns(16).ColumnName = "سبب الخروج"
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    'Day Description
                    NDT.Columns.Add("يوم", Type.GetType("System.String"))
                    For Each row As DataRow In NDT.Rows
                        row("يوم") = Convert.ToDateTime(row(5), UK_Culture).DayOfWeek
                    Next

                    NDT.SetColumnsOrder(New String() {"رقم الموظف",
                                   "اسم الموظف",
                                   "اسم وحدة العمل",
                                   "اسم الشركة",
                                   "اسم الجدول",
                                   "التاريخ",
                                   "يوم",
                                   "اول دخول",
                                   "اخر خروج",
                                   "التأخير",
                                   "الخروج المبكر",
                                   "المدة",
                                   "الوقت الضائع",
                                   "الملاحظات",
                                   "الوقت الاضافي"})
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                    For Each row As DataRow In NDT.Rows
                        If row(7).ToString = "" And Not row(8).ToString = "" Then
                            row(13) = row(13) + " -لايوجد دخول"
                        ElseIf Not row(7).ToString = "" And row(8).ToString = "" Then
                            row(13) = row(13) + " -لايوجد خروج"
                        End If
                    Next
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                Else

                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    NDT.Columns(23).DataType = System.Type.GetType("System.String")
                    NDT.Columns(25).DataType = System.Type.GetType("System.String")
                    NDT.Columns(27).DataType = System.Type.GetType("System.String")
                    NDT.Columns(35).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 20 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = row(i).ToString()
                                End If
                            ElseIf i = 25 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 27 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 35 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 29 Then '---HolidayName Column
                                If dr(19).ToString = "H" Then
                                    dr(21) = dr(i).ToString
                                    dr(22) = dr(i).ToString
                                End If
                            ElseIf i = 31 Then '---LeaveName Column
                                If dr(19).ToString = "L" Then
                                    dr(21) = dr(i).ToString
                                    dr(22) = dr(i).ToString
                                End If
                            ElseIf i = 33 Then '---PermissionName Column
                                If dr(19).ToString = "P" Then
                                    dr(21) = dr(i).ToString
                                    dr(22) = dr(i).ToString
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
                    NDT.Columns(4).ColumnName = "Schedule Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Date"
                    NDT.Columns(6).ColumnName = "First In"
                    NDT.Columns(7).ColumnName = "Last Out"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Delay"
                    NDT.Columns(9).ColumnName = "Early Out"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Duration"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "Lost Time"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "OverTime"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "Work Location"
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns(15).ColumnName = "Reason In"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns(16).ColumnName = "Reason Out"
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.Add("Day", Type.GetType("System.String"))
                    For Each row As DataRow In NDT.Rows
                        row("Day") = Convert.ToDateTime(row("Date"), UK_Culture).DayOfWeek
                    Next

                    NDT.SetColumnsOrder(New String() {"Emp No.",
                                    "Employee Name",
                                    "Entity Name",
                                    "Company Name",
                                    "Schedule Name",
                                    "Date",
                                    "Day",
                                    "First In",
                                    "Last Out",
                                    "Delay",
                                    "Early Out",
                                    "Duration",
                                    "Lost Time",
                                    "Remarks",
                                    "OverTime"})
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                    For Each row As DataRow In NDT.Rows
                        If row(7).ToString = "" And Not row(8).ToString = "" Then
                            row(13) = row(13) + " -Missing In"
                        ElseIf Not row(7).ToString = "" And row(8).ToString = "" Then
                            row(13) = row(13) + " -Missing Out"
                        End If
                    Next
                    '---------Add Missing IN & OUT in the Remarks field-----------'

                End If

            Case EmpMove_OpenSchedule

                NDT.Columns(12).DataType = System.Type.GetType("System.String")
                NDT.Columns(13).DataType = System.Type.GetType("System.String")
                NDT.Columns(14).DataType = System.Type.GetType("System.String")
                NDT.Columns(18).DataType = System.Type.GetType("System.String")
                NDT.Columns(19).DataType = System.Type.GetType("System.String")
                NDT.Columns(21).DataType = System.Type.GetType("System.String")
                NDT.Columns(26).DataType = System.Type.GetType("System.String")
                NDT.Columns(36).DataType = System.Type.GetType("System.String")
                For Each row As DataRow In DT.Rows
                    Dim dr As DataRow = NDT.NewRow
                    For i As Integer = 0 To NDT.Columns.Count - 1
                        dr(i) = row(i)
                        If i = 12 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                            End If
                        ElseIf i = 13 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 14 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 18 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 19 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 21 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 26 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 36 Then
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
                    NDT.Columns(2).ColumnName = "البريد الالكتروني"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "اسم الجدول"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "التاريخ"
                    NDT.Columns(7).ColumnName = "اول دخول"
                    NDT.Columns(8).ColumnName = "اخر خروج"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "المدة"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "الوقت الضائع"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "الوقت الاضافي"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    'Day Description
                    NDT.Columns.Add("يوم", Type.GetType("System.String"))
                    For Each row As DataRow In NDT.Rows
                        row("يوم") = Convert.ToDateTime(row("التاريخ"), UK_Culture).DayOfWeek
                    Next

                    NDT.SetColumnsOrder(New String() {"رقم الموظف",
                                   "اسم الموظف",
                                   "اسم وحدة العمل",
                                   "اسم الشركة",
                                   "اسم الجدول",
                                   "التاريخ",
                                   "يوم",
                                   "اول دخول",
                                   "اخر خروج",
                                   "المدة",
                                   "الوقت الضائع",
                                   "الملاحظات",
                                   "الوقت الاضافي"})
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                    For Each row As DataRow In NDT.Rows
                        If row(7).ToString = "" And Not row(8).ToString = "" Then
                            row(10) = row(10) + " -لايوجد دخول"
                        ElseIf Not row(7).ToString = "" And row(8).ToString = "" Then
                            row(10) = row(10) + " -لايوجد خروج"
                        End If
                    Next
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                Else
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Email"
                    NDT.Columns(3).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Schedule Name"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Date"
                    NDT.Columns(7).ColumnName = "First In"
                    NDT.Columns(8).ColumnName = "Last Out"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "Duration"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "Lost Time"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "OverTime"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)

                    NDT.Columns.Add("Day", Type.GetType("System.String"))
                    For Each row As DataRow In NDT.Rows
                        row("Day") = Convert.ToDateTime(row("Date"), UK_Culture).DayOfWeek
                    Next

                    NDT.SetColumnsOrder(New String() {"Emp No.",
                                    "Employee Name",
                                    "Entity Name",
                                    "Company Name",
                                    "Schedule Name",
                                    "Date",
                                    "Day",
                                    "First In",
                                    "Last Out",
                                    "Duration",
                                    "Lost Time",
                                    "Remarks",
                                    "OverTime"})
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                    For Each row As DataRow In NDT.Rows
                        If row(7).ToString = "" And Not row(8).ToString = "" Then
                            row(10) = row(10) + " -Missing In"
                        ElseIf Not row(7).ToString = "" And row(8).ToString = "" Then
                            row(10) = row(10) + " -Missing Out"
                        End If
                    Next
                    '---------Add Missing IN & OUT in the Remarks field-----------'

                End If

            Case FCA_TransactionHistory

                NDT.Columns(4).DataType = System.Type.GetType("System.String")
                NDT.Columns(5).DataType = System.Type.GetType("System.String")

                For Each row As DataRow In DT.Rows
                    Dim dr As DataRow = NDT.NewRow
                    For i As Integer = 0 To NDT.Columns.Count - 1
                        dr(i) = row(i)
                        If i = 4 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                            End If
                        ElseIf i = 5 Then
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
                    NDT.Columns(3).ColumnName = "الوقت"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "النوع"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                Else
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Date"
                    NDT.Columns(3).ColumnName = "Time"
                    NDT.Columns(4).ColumnName = "Type"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)

                End If


            Case Mobile_InOut_Transactions

                NDT.Columns(6).DataType = System.Type.GetType("System.String")
                NDT.Columns(7).DataType = System.Type.GetType("System.String")

                For Each row As DataRow In DT.Rows
                    Dim dr As DataRow = NDT.NewRow
                    For i As Integer = 0 To NDT.Columns.Count - 1
                        dr(i) = row(i)
                        If i = 6 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                            End If
                        ElseIf i = 7 Then
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
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "نوع الحركة"
                    NDT.Columns(3).ColumnName = "التاريخ"
                    NDT.Columns(4).ColumnName = "الوقت"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "اسم الموقع"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)

                Else
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Employee No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Reason Name"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "Date"
                    NDT.Columns(4).ColumnName = "Time"
                    NDT.Columns(5).ColumnName = "Location"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                End If

        End Select
    End Sub

#End Region

    Protected Sub rblEmpStatus_SelectedIndexChanged(sender As Object, e As EventArgs) 'ID: M04 || Date: 25-04-2023 || By: Yahia shalan || Description: Clear the employee filter items'
        EmployeeFilter1.ClearValues()
    End Sub
End Class


