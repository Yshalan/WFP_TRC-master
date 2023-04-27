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

Partial Class Emp_EmpSelf_Reports_Manager
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objAPP_Settings As APP_Settings
    Private Lang As CtlCommon.Lang
    Protected dir As String
    Private objVersion As SmartV.Version.version
    Private objOrgCompany As New OrgCompany
    Dim CultureInfo As System.Globalization.CultureInfo
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim cryRpt As New ReportDocument
    Dim objSmartSecurity As New SmartSecurity
    Private objsysforms As New SYSForms

    Private EmployeeDailyReport As String = "Employee Daily Report"
    Private EmployeeAbsentReport As String = "Employee Absent Report"
    Private EmployeeExtraHourReport As String = "Employee Extra Hour Report"
    Private EmployeeViolationsReport As String = "Employee Violations Report"
    Private EmployeeInOutTransactionsReport As String = "Employee In Out Transactions Report"
    Private EmployeeSummryReport As String = "Employee Summry Report"
    Private EmployeeLeavesReport As String = "Employee Leaves Report"
    Private EmployeePermissionsReport As String = "Employee Permissions Report"
    Private EmployeeLeavesBalanceReport As String = "Employee Leaves Balance Report"
    Private EmployeeExpireLeaveBalanceReport As String = "Employee Expire Leave Balance Report"
    Private EmployeeAttendanceTransactionReport As String = "Employee Attendance Transaction Report"
    Private DetailedTransactions As String = "Detailed Transactions"
    Private NotAttendEmployee As String = "Not Attend Employee"
    Private EmployeeDailyReport_Invalid As String = "EmployeeDailyReport_Invalid"
    Private EmployeeDetailedTransactions_Invalid As String = "EmployeeDetailedTransactions_Invalid"
    Private EmployeeAdvanceViolationReport As String = "EmployeeAdvanceViolationReport"
    Private EmployeeSummaryReport_Param As String = "EmployeeSummaryReport_Param"
    Private EmployeeGateTransactions As String = "EmployeeGateTransactions"
    Private EmployeeDetailedTransactions_WithAllowance As String = "EmployeeDetailedTransactions_WithAllowance"
    Private EmployeesViolationsReportMgr As String = "EmployeesViolationsReportMgr"

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

    Public Property SubDT() As DataTable
        Get
            Return ViewState("SubDT")
        End Get
        Set(ByVal value As DataTable)
            ViewState("SubDT") = value
        End Set
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
        lblReportTitle.HeaderText = ResourceManager.GetString("ManagerReports", CultureInfo)
        If Not Page.IsPostBack Then

            EmployeeFilter.FK_ManagerId = SessionVariables.LoginUser.FK_EmployeeId

            RadDatePicker1.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd As New Date
            dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)

            RadDatePicker2.SelectedDate = dd
            IS_EXIST = False

            objAPP_Settings = New APP_Settings
            With objAPP_Settings
                .GetByPK()
                rblFormat.SelectedValue = .DefaultReportFormat

            End With
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

            FillReports(formID, SessionVariables.LoginUser.GroupId)


        End If




    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        cryRpt.Close()
        cryRpt.Dispose()

    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim Id As String = RadCmbBxReports.SelectedValue

        'If SessionVariables.LoginUser.UserStatus = 2 And EmployeeFilter.EntityId = 0 Then
        '    If SessionVariables.CultureInfo = "ar-JO" Then
        '        CtlCommon.ShowMessage(Page, "الرجاء اختيار وحدة العمل")
        '    Else
        '        CtlCommon.ShowMessage(Page, "Please Select Entity")
        '    End If
        '    Exit Sub
        'End If

        BindReport(Id)
    End Sub

    Protected Sub RadCmbBxReports_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxReports.SelectedIndexChanged
        If RadCmbBxReports.SelectedValue = NotAttendEmployee Then
            lblToDate.Visible = False
            RadDatePicker2.Visible = False
            dvSummaryParams.Visible = False
        ElseIf RadCmbBxReports.SelectedValue = EmployeeAdvanceViolationReport Then
            dvViolation_Selection.Visible = True
            dvDelayPolicy.Visible = True
            dvEarlyOutPolicy.Visible = True
            dvAbsentPolicy.Visible = True
            dvSummaryParams.Visible = False
            lblToDate.Visible = True
            RadDatePicker2.Visible = True
        ElseIf RadCmbBxReports.SelectedValue = EmployeeSummaryReport_Param Then
            dvSummaryParams.Visible = True
            dvViolation_Selection.Visible = False
            lblToDate.Visible = True
            RadDatePicker2.Visible = True
        Else
            lblToDate.Visible = True
            RadDatePicker2.Visible = True
            dvViolation_Selection.Visible = False
            dvSummaryParams.Visible = False
        End If
    End Sub

    Private Sub FillReports(ByVal formID As Integer, ByVal groupID As Integer)

        CtlCommon.FillTelerikDropDownList(RadCmbBxReports, objsysforms.GetFormsByParentIDManager(formID, SessionVariables.LoginUser.GroupId, SessionVariables.LoginUser.FK_EmployeeId), Lang)

    End Sub

    Protected Sub radcmbxViolatonSelection_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles radcmbxViolatonSelection.SelectedIndexChanged
        If radcmbxViolatonSelection.SelectedValue = 1 Then
            dvDelayPolicy.Visible = True
            dvEarlyOutPolicy.Visible = False
            dvAbsentPolicy.Visible = False
        ElseIf radcmbxViolatonSelection.SelectedValue = 2 Then
            dvDelayPolicy.Visible = False
            dvEarlyOutPolicy.Visible = True
            dvAbsentPolicy.Visible = False
        ElseIf radcmbxViolatonSelection.SelectedValue = 3 Then
            dvDelayPolicy.Visible = True
            dvEarlyOutPolicy.Visible = True
            dvAbsentPolicy.Visible = True
        End If
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

    Public Sub BindReport(ByVal Id As String)
        If Not RadCmbBxReports.SelectedValue = "-1" Then
            SetReportName(RadCmbBxReports.SelectedValue)
            If IS_EXIST = True Then
                cryRpt = New ReportDocument
                cryRpt.Load(Server.MapPath(RPTName))
                cryRpt.SetDataSource(DT)
                Dim objAPP_Settings As New APP_Settings
                objAPP_Settings.FK_CompanyId = EmployeeFilter.CompanyId
                dt2 = objAPP_Settings.GetHeader()
                dt2.Columns.Add("From_Date")
                dt2.Columns.Add("To_Date")
                dt2.Rows(0).Item("From_Date") = DateToString(RadDatePicker1.SelectedDate)
                dt2.Rows(0).Item("To_Date") = DateToString(RadDatePicker2.SelectedDate)
                cryRpt.Subreports("rptHeader").SetDataSource(dt2)
                If RadCmbBxReports.SelectedValue = EmployeeLeavesReport Then
                    cryRpt.Subreports("rptSubLeaves").SetDataSource(SubDT)
                End If

                If RadCmbBxReports.SelectedValue = EmployeeDetailedTransactions_WithAllowance Then
                    cryRpt.Subreports("Sub_LeavesSummary").SetDataSource(SubDT)
                End If

                cryRpt.SetParameterValue("@ReportName", RadCmbBxReports.SelectedItem.Text)
                cryRpt.SetParameterValue("@ReportName", RadCmbBxReports.SelectedItem.Text, "rptHeader")
                cryRpt.SetParameterValue("@FromDate", DateToString(RadDatePicker1.SelectedDate))
                cryRpt.SetParameterValue("@ToDate", DateToString(RadDatePicker2.SelectedDate))
                'Version = System.Configuration.ConfigurationManager.AppSettings("Version").ToString()
                Version = SmartV.Version.version.GetVersionNumber
                cryRpt.SetParameterValue("@Version", Version)
                cryRpt.SetParameterValue("@UserName", UserName)

                If RadCmbBxReports.SelectedValue = EmployeePermissionsReport Then
                    cryRpt.SetParameterValue("@URL", BaseSiteUrl)
                ElseIf RadCmbBxReports.SelectedValue = EmployeeLeavesReport Then
                    cryRpt.SetParameterValue("@URL", BaseSiteUrl)
                End If

                If RadCmbBxReports.SelectedValue = EmployeeDetailedTransactions_WithAllowance Then
                    cryRpt.SetParameterValue("@FilteredByEntity", 0)
                End If



                '----------To Enable Colors in Violation Report---------'
                If RadCmbBxReports.SelectedValue = EmployeeViolationsReport Or RadCmbBxReports.SelectedValue = EmployeesViolationsReportMgr Then
                    Dim IsDailyReportWithColor As Boolean
                    objAPP_Settings = New APP_Settings
                    With objAPP_Settings
                        .GetByPK()
                        IsDailyReportWithColor = .IsDailyReportWithColor
                    End With
                    cryRpt.SetParameterValue("@IsColored", IsDailyReportWithColor)
                End If
                '----------To Enable Colors in Violation Report---------'

                If RadCmbBxReports.SelectedValue = EmployeeDailyReport Or RadCmbBxReports.SelectedValue = EmployeeDailyReport_Invalid Then
                    objAPP_Settings.GetByPK()
                    cryRpt.SetParameterValue("@TransactionConsideration", objAPP_Settings.ConsequenceTransactions)
                End If

                Dim ShowSTLogo As Boolean
                ShowSTLogo = ConfigurationManager.AppSettings("ShowSmartTimeLogo")
                cryRpt.SetParameterValue("@ShowSTLogo", False)
                cryRpt.SetParameterValue("@ShowSTLogo", False, "rptHeader")
                If SessionVariables.CultureInfo = "ar-JO" Then
                    CopyRightAr = objSmartSecurity.strSmartDecrypt(ConfigurationManager.AppSettings("CopyRightAr").ToString(), "SmartVision")
                    cryRpt.SetParameterValue("@CopyRightAr", CopyRightAr)
                Else
                    CopyRightEn = objSmartSecurity.strSmartDecrypt(ConfigurationManager.AppSettings("CopyRightEn").ToString(), "SmartVision")
                    cryRpt.SetParameterValue("@CopyRightEn", CopyRightEn)

                End If
                Dim showSmartTimeLogo As Boolean = CBool(ConfigurationManager.AppSettings("ShowSmartTimeLogo"))
                If showSmartTimeLogo Then

                    objOrgCompany = New OrgCompany
                    Dim rptObj As New Report
                    If EmployeeFilter.CompanyId <> 0 Then
                        objOrgCompany.CompanyId = EmployeeFilter.CompanyId
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
                        If rblFormat.SelectedValue = 1 Then
                            cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedReport")
                        ElseIf rblFormat.SelectedValue = 2 Then
                            cryRpt.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, True, "ExportedReport")
                        ElseIf rblFormat.SelectedValue = 3 Then
                            FillExcelreport(RadCmbBxReports.SelectedValue)
                            ExportDataSetToExcel(NDT, "ExportedReport")
                        End If
                        MultiView1.SetActiveView(Report)
                    Else
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NoDataFound", CultureInfo), "info")
                    End If
                Else
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NoDataFound", CultureInfo), "info")
                End If
            Else
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("Invalid_Employee", CultureInfo), "info")
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
        If EmployeeFilter.EmployeeId <> 0 Then

            rptObj.EmployeeId = EmployeeFilter.EmployeeId
        Else
            rptObj.EmployeeId = Nothing
        End If

        If Not EmployeeFilter.CompanyId = 0 Then
            rptObj.CompanyId = EmployeeFilter.CompanyId
        Else
            rptObj.CompanyId = Nothing
        End If

        If EmployeeFilter.FilterType = "C" Then
            If Not EmployeeFilter.EntityId = 0 Then
                rptObj.EntityId = EmployeeFilter.EntityId
            Else
                rptObj.EntityId = Nothing
            End If
        ElseIf EmployeeFilter.FilterType = "W" Then
            If Not EmployeeFilter.EntityId = 0 Then
                rptObj.WorkLocationId = EmployeeFilter.EntityId
            Else
                rptObj.WorkLocationId = Nothing
            End If
        ElseIf EmployeeFilter.FilterType = "L" Then
            If Not EmployeeFilter.EntityId = 0 Then
                rptObj.LogicalGroupId = EmployeeFilter.EntityId
            Else
                rptObj.LogicalGroupId = Nothing
            End If
        End If

        If Not RadDatePicker1.SelectedDate Is Nothing Then
            rptObj.FROM_DATE = RadDatePicker1.DbSelectedDate
        End If

        If Not RadDatePicker2.SelectedDate Is Nothing Then
            rptObj.TO_DATE = RadDatePicker2.DbSelectedDate
        End If

        If RadCmbBxReports.SelectedValue = EmployeeAdvanceViolationReport Then
            rptObj.ViolationMinDelay = (CInt(rmtxtDelayTime.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtxtDelayTime.TextWithLiterals.Split(":")(1))
            rptObj.ViolationMinEarlyOut = (CInt(rmtxtEarlyOutTime.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtxtEarlyOutTime.TextWithLiterals.Split(":")(1))
            rptObj.ViolationSelection = radcmbxViolatonSelection.SelectedValue

            rptObj.ViolationDelaySelection = IIf(rblDelayPolicy.SelectedValue = String.Empty, Nothing, rblDelayPolicy.SelectedValue)
            rptObj.ViolationDelayNum = IIf(radnumDelayNum.Text = String.Empty, Nothing, radnumDelayNum.Text)

            rptObj.ViolationEarlyOutSelection = IIf(rblEarlyOutPolicy.SelectedValue = String.Empty, Nothing, rblEarlyOutPolicy.SelectedValue)
            rptObj.ViolationEarlyOutNum = IIf(radnumEarlyOutNum.Text = String.Empty, Nothing, radnumEarlyOutNum.Text)

            rptObj.ViolationAbsentSelection = IIf(rblAbsentPolicy.SelectedValue = String.Empty, Nothing, rblAbsentPolicy.SelectedValue)
            rptObj.ViolationAbsentNum = IIf(radnumAbsentNum.Text = String.Empty, Nothing, radnumAbsentNum.Text)
        End If

        If RadCmbBxReports.SelectedValue = EmployeeSummaryReport_Param Then
            rptObj.Absent_Count_Selection = rblAbsentDays.SelectedValue
            rptObj.Absent_Count = IIf(radnumAbsentDays.Text = String.Empty, Nothing, radnumAbsentDays.Text)
            rptObj.Work_Hours_Selection = rblWorkHours.SelectedValue
            rptObj.Work_Hours = (CInt(rmtxtWorkHours.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtxtWorkHours.TextWithLiterals.Split(":")(1))
        End If

        Dim dtTempCompanies As New DataTable
        Dim dtCompanies As New DataTable
        Dim drCompanyRow As DataRow
        dtCompanies = New DataTable
        If objVersion.HasMultiCompany = False Then
            dtTempCompanies = objOrgCompany.GetAll()
            drCompanyRow = dtTempCompanies.Select("CompanyId =" & objVersion.GetCompanyId())(0)
            rptObj.CompanyId = drCompanyRow(0)
        End If
        'rptObj.EntityId = Nothing
        EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
        Dim err As Integer = -1
        Dim objEmployee As New Employee
        objEmployee.EmployeeId = EmployeeId
        err = objEmployee.GetExist_Employee()
        If err = -99 Then
            IS_EXIST = True
            rptObj.ManagerId = EmployeeId
            Dim IsDailyReportWithColor As Boolean
            objAPP_Settings = New APP_Settings
            With objAPP_Settings
                .GetByPK()
                IsDailyReportWithColor = .IsDailyReportWithColor
            End With

            'rpttid = IIf(rpttid = 0, 3, rpttid)
            Select Case rpttid
                Case EmployeeDailyReport
                    'RPTName = "rptEmpMove.rpt"
                    If (IsDailyReportWithColor) Then
                        RPTName = "rptEmpMoveColored.rpt"
                    Else
                        RPTName = "rptEmpMove.rpt"
                    End If
                    DT = rptObj.GetFilterdEmpMove_Mgr()
                    'Case 2 'Employee list
                    '    RPTName = "rptEmployeeList.rpt"
                    '    DT = rptObj.GetFilterdEmpList()

                Case EmployeeAttendanceTransactionReport
                    RPTName = "rptDetEmpMove.rpt"
                    DT = rptObj.GetFilterdDetEmpMove_Mgr()

                    'Case EmployeeLeavesReport
                    '    RPTName = "RptEmLeaves.rpt"
                    '    DT = rptObj.GetEmpLeaves_Mgr()

                Case EmployeeAbsentReport
                    RPTName = "rptEmpAbsent.rpt"
                    DT = rptObj.GetEmpAbsent_Mgr()

                Case EmployeeExtraHourReport
                    RPTName = "crExtraHours.rpt"
                    DT = rptObj.GetExtraHours_Mgr()

                Case EmployeeViolationsReport
                    RPTName = "rptViolations.rpt"
                    DT = rptObj.GetViolations_Mgr()

                Case EmployeeInOutTransactionsReport
                    RPTName = "rptEmpInOut.rpt"
                    DT = rptObj.GetEmpInOut_Mgr()
                Case EmployeeSummryReport
                    RPTName = "rptSummary.rpt"
                    DT = rptObj.GetSummary_Mgr()
                    'Case 10
                    '    RPTName = "rptTerminatedEmployeeList.rpt"
                    '    DT = rptObj.GetFilterdTerminatedEmpList()
                Case EmployeeLeavesReport
                    RPTName = "rptEmpLeaves.rpt"
                    DT = rptObj.GetEmpLeaves_Mgr
                    SubDT = rptObj.GetSub_LeavesDetails
                Case EmployeePermissionsReport
                    RPTName = "rptEmpPermissions.rpt"
                    DT = rptObj.GetEmpPermissions_Mgr
                Case EmployeeExpireLeaveBalanceReport
                    RPTName = "rptEmp_BalanceExpire.rpt"
                    DT = rptObj.GetEmpExpireBalance_Mgr
                Case EmployeeLeavesBalanceReport
                    RPTName = "rptEmp_Balance.rpt"
                    DT = rptObj.GetEmpBalance_Mgr
                Case DetailedTransactions
                    'RPTName = "rptDetailedTransactions.rpt"
                    If (IsDailyReportWithColor) Then
                        RPTName = "rptDetailedTransactionsColored.rpt"
                    Else
                        RPTName = "rptDetailedTransactions.rpt"
                    End If
                    DT = rptObj.GetDetailed_Transactions_Mgr
                Case NotAttendEmployee
                    RPTName = "rptNotAttendanceEmployee.rpt"
                    DT = rptObj.Get_NotAttendanceEmployee_Mgr

                Case EmployeeDailyReport_Invalid
                    RPTName = "rptEmpMove_Invalid.rpt"
                    DT = rptObj.GetFilterdEmpMove_Invalid_Mgr

                Case EmployeeDetailedTransactions_Invalid
                    RPTName = "rptDetailedTransactions_Invalid.rpt"
                    DT = rptObj.GetDetailed_Transactions_Invalid_Mgr

                Case EmployeeAdvanceViolationReport
                    RPTName = "rptViolations_Advance.rpt"
                    DT = rptObj.GetViolations_Advance_Mgr()

                Case EmployeeSummaryReport_Param
                    RPTName = "rptSummary_Param.rpt"
                    DT = rptObj.GetSummary_Param_Mgr

                Case EmployeeGateTransactions
                    RPTName = "rptGatesReport.rpt"
                    DT = rptObj.GetGatesReport_Mgr

                Case EmployeeDetailedTransactions_WithAllowance
                    RPTName = "rptDetailedTransactionsColored_WithMonthlyAllowance.rpt"
                    DT = rptObj.GetDetailed_TransactionsWithAllowance_Mgr
                    SubDT = rptObj.GetSub_LeavesDetails

                Case EmployeesViolationsReportMgr
                    RPTName = "rptViolations2.rpt"
                    DT = rptObj.GetViolations_Mgr2

            End Select

            UserName = SessionVariables.LoginUser.UsrID
            ' Version = System.Configuration.ConfigurationManager.AppSettings("Version").ToString()

            If (Lang = CtlCommon.Lang.AR) Then
                RPTName = "Arabic/" + RPTName
            Else
                RPTName = "English/" + RPTName
            End If
        Else
            IS_EXIST = False
        End If
    End Sub

    Private Sub FillExcelreport(ByVal reportid As String)

        Dim UK_Culture As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("en-GB")

        NDT = New DataTable
        NDT = DT.Clone()
        Select Case reportid
            Case EmployeeDailyReport
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
                    'NDT.Columns.RemoveAt(14)
                    'NDT.Columns.RemoveAt(14)
                    'NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "موقع العمل"
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
                    'NDT.Columns.RemoveAt(14)
                    'NDT.Columns.RemoveAt(14)
                    'NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "Work Location"
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

            Case EmployeeAttendanceTransactionReport
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
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "اسم الموظف باللغة العربية"
                    NDT.Columns(3).ColumnName = "اسم وحدة العمل"
                    NDT.Columns(4).ColumnName = "اسم وحد العمل باللغة العربية"
                    NDT.Columns(5).ColumnName = "اسم الشركة"
                    NDT.Columns(6).ColumnName = "اسم الشركة باللغة العربية"
                    NDT.Columns(7).ColumnName = "اسم السبب"
                    NDT.Columns(8).ColumnName = "تاريخ الحركة"
                    NDT.Columns(9).ColumnName = "وقت الحركة"
                    NDT.Columns(10).ColumnName = "القارئ"
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
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns(2).ColumnName = "Employee Arabic Name"
                    NDT.Columns(3).ColumnName = "Entity Name"
                    NDT.Columns(4).ColumnName = "Entity Arabic Name"
                    NDT.Columns(5).ColumnName = "Company Name"
                    NDT.Columns(6).ColumnName = "Company Arabic Name"
                    NDT.Columns(7).ColumnName = "Reason Name"
                    NDT.Columns(8).ColumnName = "Move Date"
                    NDT.Columns(9).ColumnName = "Move Time"
                    NDT.Columns(10).ColumnName = "Reader"
                End If
            Case EmployeeAbsentReport
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
                    NDT.Columns.RemoveAt(6)

                End If
            Case EmployeeExtraHourReport
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
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
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
                    NDT.Columns(2).ColumnName = "اسم الموظف باللغة العربية"
                    NDT.Columns(3).ColumnName = "اسم وحدة العمل"
                    NDT.Columns(4).ColumnName = "اسم وحدة العمل باللغة العربية"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)

                    NDT.Columns(5).ColumnName = "التاريخ"

                    'NDT.Columns(6).ColumnName = "اسم الشركة"
                    'NDT.Columns(7).ColumnName = "اسم الشركة باللغة العربية"

                    'NDT.Columns(8).ColumnName = "تاريخ الحركة"

                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "الملاحظات"

                    'NDT.Columns(9).ColumnName = "الحالة"

                    'NDT.Columns(10).ColumnName = "الملاحظات"

                    'NDT.Columns(11).ColumnName = "السبب"
                    'NDT.Columns(12).ColumnName = "الوقت الاضافي"
                    'NDT.Columns.RemoveAt(13)
                    'NDT.Columns(13).ColumnName = "مجموع ساعات العمل"
                    'NDT.Columns.RemoveAt(14)

                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)

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
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
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
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns(2).ColumnName = "Employee Arabic Name"
                    NDT.Columns(3).ColumnName = "Entity Name"
                    NDT.Columns(4).ColumnName = "Entity Arabic Name"
                    NDT.Columns.RemoveAt(5)
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

                    'NDT.Columns(6).ColumnName = "Company Name"
                    'NDT.Columns(7).ColumnName = "Company Arabic Name"
                    'NDT.Columns(8).ColumnName = "Move Date"
                    'NDT.Columns(9).ColumnName = "Status"

                    'NDT.Columns(11).ColumnName = "Reason"
                    'NDT.Columns(12).ColumnName = "OverTime"
                    'NDT.Columns.RemoveAt(13)
                    'NDT.Columns(13).ColumnName = "Total Work Hours"
                    'NDT.Columns.RemoveAt(14)

                End If
            Case EmployeeViolationsReport
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(19).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    NDT.Columns(21).DataType = System.Type.GetType("System.String")
                    NDT.Columns(22).DataType = System.Type.GetType("System.String")
                    NDT.Columns(25).DataType = System.Type.GetType("System.String")
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
                            ElseIf i = 20 Then
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
                            ElseIf i = 25 Then
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
                    NDT.Columns(8).ColumnName = "الخروج المبكر"
                    NDT.Columns(9).ColumnName = "وقت الدخول"
                    NDT.Columns(10).ColumnName = "وقت الخروج"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "الوقت الضائع "
                    NDT.Columns(12).ColumnName = "مدة الخروج"

                    For Each row In NDT.Rows
                        If Not row(9).ToString = "" And Not row(10).ToString = "" Then
                            If (Convert.ToDateTime(row(10)) < Convert.ToDateTime(row(9))) Then
                                row(6) = "تسجيل الخروج قبل تسجيل الدخول"
                            End If
                        End If
                        If Not row(9).ToString = "" And row(10).ToString = "" And row(5) < Date.Today Then
                            row(6) = "لايوجد خروج"
                        End If
                        If row(9).ToString = "" And Not row(10).ToString = "" Then
                            row(6) = "لايوجد دخول"
                        End If

                    Next
                Else
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(19).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    NDT.Columns(21).DataType = System.Type.GetType("System.String")
                    NDT.Columns(22).DataType = System.Type.GetType("System.String")
                    NDT.Columns(25).DataType = System.Type.GetType("System.String")
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
                            ElseIf i = 20 Then
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
                            ElseIf i = 25 Then
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
                    NDT.Columns(6).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "Delay"
                    NDT.Columns(8).ColumnName = "Early Out"
                    NDT.Columns(9).ColumnName = "In Time"
                    NDT.Columns(10).ColumnName = "Out Time"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "Lost Time"
                    NDT.Columns(12).ColumnName = "StepOut Time"

                    For Each row In NDT.Rows
                        If Not row(9).ToString = "" And Not row(10).ToString = "" Then
                            If (Convert.ToDateTime(row(10)) < Convert.ToDateTime(row(9))) Then
                                row(6) = "Sign Out Before Sign In"
                            End If
                        End If
                        If Not row(9).ToString = "" And row(10).ToString = "" And row(5) < Date.Today Then
                            row(6) = "Missing Out"
                        End If
                        If row(9).ToString = "" And Not row(10).ToString = "" Then
                            row(6) = "Missing In"
                        End If
                    Next

                End If
            Case EmployeeInOutTransactionsReport
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
            Case EmployeeSummryReport
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
                End If

            Case EmployeeLeavesReport
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
            Case EmployeePermissionsReport
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")
                    NDT.Columns(10).DataType = System.Type.GetType("System.String")
                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
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

                            ElseIf i = 12 Then
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
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "اسم الشركة"
                    NDT.Columns(4).ColumnName = "تاريخ المغادرة"
                    NDT.Columns(5).ColumnName = "من وقت"
                    NDT.Columns(6).ColumnName = "الى وقت"
                    NDT.Columns(7).ColumnName = "المدة"
                    NDT.Columns(8).ColumnName = "تاريخ انتهاء المغادرة"
                    NDT.Columns(9).ColumnName = "لفترة"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "نوع المغادرة"
                    NDT.Columns(11).ColumnName = "يوم كامل"
                    NDT.Columns(12).ColumnName = "مرن"
                    NDT.Columns(13).ColumnName = "الزمن المرن"
                    NDT.Columns(14).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                Else
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")
                    NDT.Columns(10).DataType = System.Type.GetType("System.String")
                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
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

                            ElseIf i = 12 Then
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
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Permission Date"
                    NDT.Columns(5).ColumnName = "From Time"
                    NDT.Columns(6).ColumnName = "To Time"
                    NDT.Columns(7).ColumnName = "Period"
                    NDT.Columns(8).ColumnName = "Permission End Date"
                    NDT.Columns(9).ColumnName = "Is For Period"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Perm Type"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "Full Day"
                    NDT.Columns(12).ColumnName = "Is Flexibile"
                    NDT.Columns(13).ColumnName = "Flexibile Duration"
                    NDT.Columns(14).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                End If
            Case EmployeeExpireLeaveBalanceReport
                NDT = DT
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "اسم الموظف باللغة العربية"
                    NDT.Columns(3).ColumnName = "اسم وحدة العمل "
                    NDT.Columns(4).ColumnName = "اسم وحدة العمل باللغة العربية"
                    NDT.Columns(5).ColumnName = "اسم الشركة"
                    NDT.Columns(6).ColumnName = "اسم الشركة باللغة العربية"
                    NDT.Columns(7).ColumnName = "تاريخ الانتهاء"
                    NDT.Columns(8).ColumnName = "الرصيد المنتهي"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                Else
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns(2).ColumnName = "Employee Arabic Name"
                    NDT.Columns(3).ColumnName = "Entity Name"
                    NDT.Columns(4).ColumnName = "Entity Arabic Name"
                    NDT.Columns(5).ColumnName = "Company Name"
                    NDT.Columns(6).ColumnName = "Company Arabic Name"
                    NDT.Columns(7).ColumnName = "Expire Date"
                    NDT.Columns(8).ColumnName = "Expired Balance"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                End If
            Case EmployeeLeavesBalanceReport
                NDT = DT
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "اسم الموظف باللغة العربية"
                    NDT.Columns(3).ColumnName = "اسم وحدة العمل "
                    NDT.Columns(4).ColumnName = "اسم وحدة العمل باللغة العربية"
                    NDT.Columns(5).ColumnName = "اسم الشركة"
                    NDT.Columns(6).ColumnName = "اسم الشركة باللغة العربية"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "نوع الاجازة"
                    NDT.Columns(8).ColumnName = "نوع الاجازة باللغة العربية"
                    NDT.Columns(9).ColumnName = "الرصيد"
                    NDT.Columns(10).ColumnName = "مجموع الرصيد"
                    NDT.Columns(11).ColumnName = "الملاحظات"
                Else
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns(2).ColumnName = "Employee Arabic Name"
                    NDT.Columns(3).ColumnName = "Entity Name"
                    NDT.Columns(4).ColumnName = "Entity Arabic Name"
                    NDT.Columns(5).ColumnName = "Company Name"
                    NDT.Columns(6).ColumnName = "Company Arabic Name"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "Leave Type"
                    NDT.Columns(8).ColumnName = "Leave Arabic Type"
                    NDT.Columns(9).ColumnName = "Balance"
                    NDT.Columns(10).ColumnName = "Total Balance"
                    NDT.Columns(11).ColumnName = "Remarks"
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
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "من وقت"
                    NDT.Columns(9).ColumnName = "الى وقت"
                    NDT.Columns(10).ColumnName = "التأخير"
                    NDT.Columns(11).ColumnName = "الخروج المبكر"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
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
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "From Time"
                    NDT.Columns(9).ColumnName = "To Time"
                    NDT.Columns(10).ColumnName = "Delay"
                    NDT.Columns(11).ColumnName = "Early Out"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)

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

            Case NotAttendEmployee
                NDT = DT

            Case EmployeeDailyReport_Invalid
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
                    NDT.Columns(15).ColumnName = "البريد الالكتروني"
                    NDT.Columns(16).ColumnName = "جهاز حركة الدخول"
                    NDT.Columns(17).ColumnName = "جهاز حركة الخروج"
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
                    NDT.Columns(15).ColumnName = "Email"
                    NDT.Columns(16).ColumnName = "In Reader"
                    NDT.Columns(17).ColumnName = "Out Reader"

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
                        If row("First In").ToString = "" And Not row("Last Out").ToString = "" Then
                            row("Remarks") = row("Remarks") + " -Missing In"
                        ElseIf Not row("First In").ToString = "" And row("Last Out").ToString = "" Then
                            row("Remarks") = row("Remarks") + " -Missing Out"
                        End If
                    Next
                    '---------Add Missing IN & OUT in the Remarks field-----------'

                End If
            Case EmployeeDetailedTransactions_Invalid

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
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "من وقت"
                    NDT.Columns(9).ColumnName = "الى وقت"
                    NDT.Columns(10).ColumnName = "التأخير"
                    NDT.Columns(11).ColumnName = "الخروج المبكر"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
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
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "From Time"
                    NDT.Columns(9).ColumnName = "To Time"
                    NDT.Columns(10).ColumnName = "Delay"
                    NDT.Columns(11).ColumnName = "Early Out"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)

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

            Case EmployeeGateTransactions
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

            Case EmployeesViolationsReportMgr

                NDT.Columns(8).DataType = System.Type.GetType("System.String")
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
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "اسم الشركة"
                    NDT.Columns(4).ColumnName = "التاريخ"
                    NDT.Columns(5).ColumnName = "مدة المخالفة"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "نوع المخالفة"
                    NDT.Columns(7).ColumnName = "من وقت"
                    NDT.Columns(8).ColumnName = "الى وقت"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                Else
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Employee No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Date"
                    NDT.Columns(5).ColumnName = "Violation Duration"
                    NDT.Columns(6).ColumnName = "Violation Type"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "From Time"
                    NDT.Columns(8).ColumnName = "To Time"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)

                End If

        End Select
    End Sub

#End Region

End Class

