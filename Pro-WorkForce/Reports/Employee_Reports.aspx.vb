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
Imports TA.Definitions
Imports SmartV.Version

Partial Class Reports_SelfServices_EmployeeReports
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
    Private objWorkSchedule As WorkSchedule
    Private objNotificationType As NotificationType
    Private objHoliday As Holiday

    Private EmployeeListReport As String = "Employee List Report"
    Private ReportDeputyManager As String = "Report Deputy Manager"
    Private EmployeeCardDetails As String = "Employee Card Details"
    Private EmpTAExceptions As String = "Emp TA Exceptions"
    Private ShiftManagers As String = "Shift Managers"
    Private ScheduleGroupsReport As String = " Schedule Groups Report"
    Private EmployeeSchedules As String = "Employee Schedules"
    Private SchedulesList As String = "Schedules List"
    Private ManagerReport As String = "Manager Report"
    Private EmployeeDetails As String = "Employee Details"
    Private EmployeeShiftDetails As String = "Employee Shift Details"
    Private ShiftsSummaryReport As String = "Shifts Summary Report"
    Private EmployeeOnCallReport As String = "Employee On Call Report"
    Private EmployeeViolationExceptions As String = "Employee Violation Exception Report"
    Private EmployeeResumptionReport As String = "Employee Resumption Report"
    Private Employee_WorkLocations As String = "Emp_WorkLocations"
    Private Employee_Multi_ReaderLocations As String = "Employee_Multi_ReaderLocations"
    Private Employee_StudyNursing_Schedule As String = "Employee_StudyNursing_Schedule"
    Private EmployeeShiftDetails_ScheduleGroup As String = "Shift Details for Schedule Groups"
    Private Emp_Notifications As String = "Emp_Notifications"
    Private Emp_HolidayReport As String = "Emp_HolidayReport"
    Private Emp_SysUsersReport As String = "Emp_SysUsersReport"
    Private Emp_SummaryEventLog As String = "Emp_SummaryEventLog"

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

    Public Property StudyNursing_Schedule() As Integer
        Get
            Return ViewState("StudyNursing_Schedule")
        End Get
        Set(ByVal value As Integer)
            ViewState("StudyNursing_Schedule") = value
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
        btnClear.Text = IIf(Lang = CtlCommon.Lang.AR, "مسح", "Clear")
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
            lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقارير الموظفين", "Employee Reports")
            btnPrint.Text = IIf(Lang = CtlCommon.Lang.AR, "انشاء", "Generate")
            lblFromDate.Text = IIf(Lang = CtlCommon.Lang.AR, "من تاريخ", "From Date")
            lblToDate.Text = IIf(Lang = CtlCommon.Lang.AR, "الى تاريخ", "To Date")
            lblGroupName.Text = IIf(Lang = CtlCommon.Lang.AR, "اسم المجموعة", "Group Name")
            lblStatus.Text = IIf(Lang = CtlCommon.Lang.AR, "الحالة", "Status")
            btnPrintSchedules.Text = IIf(Lang = CtlCommon.Lang.AR, "طباعة", "Print")
            btnCancel.Text = IIf(Lang = CtlCommon.Lang.AR, "إلغاء", "Cancel")
            lblWorkSchedule.Text = IIf(Lang = CtlCommon.Lang.AR, "الجدول", "Work Schedule")
            lblShifts.Text = IIf(Lang = CtlCommon.Lang.AR, "المناوبة", "Shifts")
            rblNoofShifts.Items(1).Text = IIf(Lang = CtlCommon.Lang.AR, " عدد المناوبات >=", "No of Shifts >=")
            rblNoofShifts.Items(2).Text = IIf(Lang = CtlCommon.Lang.AR, " عدد المناوبات <", "No of Shifts <")
            rblNoofShifts.Items(0).Text = IIf(Lang = CtlCommon.Lang.AR, "جميع المناوبات", "All Shifts")
            lblNoofShiftsValue.Text = IIf(Lang = CtlCommon.Lang.AR, "القيمة", "Value")


            FillReports(formID, SessionVariables.LoginUser.GroupId)
            Fill_NotificationTypes()
            FillHolidays()
            RadDatePicker1.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd As New Date
            dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)

            RadDatePicker2.SelectedDate = dd

            objAPP_Settings = New APP_Settings
            With objAPP_Settings
                .GetByPK()
                rblFormat.SelectedValue = .DefaultReportFormat

            End With
            'FillScheduleTypes()
        End If

    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        cryRpt.Close()
        cryRpt.Dispose()

    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim reportName As String = RadComboEmployeeReports.SelectedValue

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

    Protected Sub btnPrintSchedules_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrintSchedules.Click
        Dim reportName As String = RadComboEmployeeReports.SelectedValue
        BindReport(reportName)

    End Sub

    Protected Sub RadComboScheduleType_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboScheduleType.SelectedIndexChanged
        Try
            FillSchedule()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ddlWorkSchedule_onselectedindexchanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlWorkSchedule.SelectedIndexChanged
        Try
            FillShifts(ddlWorkSchedule.SelectedValue)
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub RadComboEmployeeReports_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboEmployeeReports.SelectedIndexChanged
        If RadComboEmployeeReports.SelectedValue = EmployeeCardDetails Then
            EmployeeCardDetailsStatus.Visible = True

            FillStatus()

        Else
            EmployeeCardDetailsStatus.Visible = False
            ddlStatus.Items.Clear()

        End If

        If RadComboEmployeeReports.SelectedValue = ScheduleGroupsReport Then
            trScheduleGroup.Visible = True
            FillSchGroup()
        Else
            trScheduleGroup.Visible = False
            RadCombBxGroupName.Items.Clear()
        End If


        If RadComboEmployeeReports.SelectedValue = SchedulesList Then
            MultiView1.SetActiveView(vwSchedulesList)
            If SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                Dim item1 As New RadComboBoxItem
                Dim item2 As New RadComboBoxItem
                Dim item3 As New RadComboBoxItem
                Dim item4 As New RadComboBoxItem
                Dim item5 As New RadComboBoxItem
                item1.Value = -1
                item1.Text = "--الرجاء الاختيار--"
                RadComboScheduleType.Items.Add(item1)
                item2.Value = 1
                item2.Text = "عادي"
                RadComboScheduleType.Items.Add(item2)
                item3.Value = 2
                item3.Text = "مرن"
                RadComboScheduleType.Items.Add(item3)
                item4.Value = 3
                item4.Text = "مناوبات"
                RadComboScheduleType.Items.Add(item4)
                item5.Value = 5
                item5.Text = "مفتوح"
                RadComboScheduleType.Items.Add(item5)
            Else
                Lang = CtlCommon.Lang.EN


                Dim item1 As New RadComboBoxItem
                Dim item2 As New RadComboBoxItem
                Dim item3 As New RadComboBoxItem
                Dim item4 As New RadComboBoxItem
                Dim item5 As New RadComboBoxItem
                item1.Value = -1
                item1.Text = "--Please Select--"
                RadComboScheduleType.Items.Add(item1)
                item2.Value = 1
                item2.Text = "Normal"
                RadComboScheduleType.Items.Add(item2)
                item3.Value = 2
                item3.Text = "Flexible"
                RadComboScheduleType.Items.Add(item3)
                item4.Value = 3
                item4.Text = "Advanced"
                RadComboScheduleType.Items.Add(item4)
                item5.Value = 5
                item5.Text = "Open"
                RadComboScheduleType.Items.Add(item5)
            End If
        End If
        If RadComboEmployeeReports.SelectedValue = ShiftsSummaryReport Then
            trWorkSchedule.Visible = True
            trShifts.Visible = True
            trNoOfShifts.Visible = True
            ddlWorkSchedule_Bind()
            FillShifts(ddlWorkSchedule.SelectedValue)
        Else
            trWorkSchedule.Visible = False
            trShifts.Visible = False
            trNoOfShifts.Visible = False
            trlblNoofShifts.Visible = False

        End If
        If RadComboEmployeeReports.SelectedValue = ManagerReport Then
            Dim lblManagerName As Label = EmployeeFilter1.FindControl("lblEmployees")

            Dim lblManagerNo As Label = EmployeeFilter1.FindControl("lblEmpNo")
            If SessionVariables.CultureInfo = "ar-JO" Then
                lblManagerName.Text = "اسم المدير"
                lblManagerNo.Text = "رقم المدير"
            Else
                lblManagerName.Text = "Manager"
                lblManagerNo.Text = "Manager No."
            End If

        Else
            Dim lblManagerName As Label = EmployeeFilter1.FindControl("lblEmployees")

            Dim lblManagerNo As Label = EmployeeFilter1.FindControl("lblEmpNo")
            If SessionVariables.CultureInfo = "ar-JO" Then
                lblManagerName.Text = "الموظف"
                lblManagerNo.Text = "رقم"
            Else
                lblManagerName.Text = "Employee"
                lblManagerNo.Text = "Emp No."
            End If
        End If
        If RadComboEmployeeReports.SelectedValue = Emp_Notifications Then
            dvNotifications.Visible = True
        Else
            dvNotifications.Visible = False
        End If


        If RadComboEmployeeReports.SelectedValue = EmployeeSchedules Then
            trToDate.Visible = False
            trFromDate.Visible = False
        ElseIf RadComboEmployeeReports.SelectedValue = Employee_WorkLocations Then
            trToDate.Visible = False
            trFromDate.Visible = False
        ElseIf RadComboEmployeeReports.SelectedValue = Employee_Multi_ReaderLocations Then
            trToDate.Visible = False
            trFromDate.Visible = False

        Else
            trToDate.Visible = True
            trFromDate.Visible = True
        End If
        If RadComboEmployeeReports.SelectedValue = Employee_StudyNursing_Schedule Then
            dvStudyNursingSchedule.Visible = True
            rfvddlScheduleType.Enabled = True
            vceddlScheduleType.Enabled = True
        Else
            dvStudyNursingSchedule.Visible = False
            rfvddlScheduleType.Enabled = False
            vceddlScheduleType.Enabled = False
        End If

        If RadComboEmployeeReports.SelectedValue = Emp_SysUsersReport Then
            trToDate.Visible = False
            trFromDate.Visible = False
        Else
            trToDate.Visible = True
            trFromDate.Visible = True
        End If

        If RadComboEmployeeReports.SelectedValue = Emp_HolidayReport Then
            dvHoliday.Visible = True
        Else
            dvHoliday.Visible = False
        End If

    End Sub

    Protected Sub rblNoofShifts_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rblNoofShifts.SelectedIndexChanged
        trlblNoofShifts.Visible = True

        If rblNoofShifts.SelectedValue = 1 Then

        ElseIf rblNoofShifts.SelectedValue = 2 Then

        ElseIf rblNoofShifts.SelectedValue = 0 Then
            trlblNoofShifts.Visible = False
        End If
    End Sub

    Protected Sub btnClear_Click(sender As Object, e As System.EventArgs) Handles btnClear.Click
        ClearFilter()
    End Sub

    Protected Sub ddlStudyNursingScheduleType_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles ddlStudyNursingScheduleType.SelectedIndexChanged
        If Not ddlStudyNursingScheduleType.SelectedValue = -1 Then
            StudyNursing_Schedule = ddlStudyNursingScheduleType.SelectedValue
            FillScheduleName()
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

    Public Sub BindReport(ByVal reportName As String)
        If Not RadComboEmployeeReports.SelectedValue = "-1" Then
            SetReportName(reportName)

            cryRpt = New ReportDocument
            cryRpt.Load(Server.MapPath(RPTName))
            cryRpt.SetDataSource(DT)
            Dim objAPP_Settings As New APP_Settings
            objAPP_Settings.FK_CompanyId = EmployeeFilter1.CompanyId
            dt2 = objAPP_Settings.GetHeader()
            If Not (reportName = EmployeeSchedules) Then
                dt2.Columns.Add("From_Date")
                dt2.Columns.Add("To_Date")

                dt2.Rows(0).Item("From_Date") = DateToString(RadDatePicker1.SelectedDate)
                dt2.Rows(0).Item("To_Date") = DateToString(RadDatePicker2.SelectedDate)
            End If
            cryRpt.Subreports(cryRpt.Subreports().Item(0).Name).SetDataSource(dt2)
            'cryRpt.SetParameterValue("@Version", Version)
            cryRpt.SetParameterValue("@UserName", UserName)



            Dim ShowSTLogo As Boolean
            ShowSTLogo = ConfigurationManager.AppSettings("ShowSmartTimeLogo")
            cryRpt.SetParameterValue("@ShowSTLogo", False)
            cryRpt.SetParameterValue("@ShowSTLogo", False, "rptHeader")
            cryRpt.SetParameterValue("@ReportName", RadComboEmployeeReports.SelectedItem.Text)
            cryRpt.SetParameterValue("@ReportName", RadComboEmployeeReports.SelectedItem.Text, "rptHeader")
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
                    If reportName = SchedulesList Then
                        If rblSchedules.SelectedValue = 1 Then
                            cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedReport")
                        ElseIf rblSchedules.SelectedValue = 2 Then
                            cryRpt.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, True, "ExportedReport")
                        ElseIf rblSchedules.SelectedValue = 3 Then
                            FillExcelreport(reportName)
                            ExportDataSetToExcel(NDT, "ExportedReport", HeadDT)
                        End If

                    Else
                        If rblFormat.SelectedValue = 1 Then
                            cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedReport")
                        ElseIf rblFormat.SelectedValue = 2 Then
                            cryRpt.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, True, "ExportedReport")
                        ElseIf rblFormat.SelectedValue = 3 Then
                            FillExcelreport(reportName)
                            ExportDataSetToExcel(NDT, "ExportedReport", HeadDT)
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

        If EmployeeFilter1.ShowDirectStaffCheck = True Then
            rptObj.DirectStaffOnly = EmployeeFilter1.DirectStaffOnly
        Else
            rptObj.DirectStaffOnly = False
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
        If rpttid = ScheduleGroupsReport Then
            If Not RadCombBxGroupName.SelectedValue = 0 Then
                rptObj.GroupScheduleId = RadCombBxGroupName.SelectedValue
            End If
        End If
        If rpttid = EmployeeCardDetails Then
            If ddlStatus.SelectedValue <> -1 Then

                rptObj.Status = ddlStatus.SelectedValue
            Else
                rptObj.Status = Nothing
            End If
        End If
        If rpttid = SchedulesList Then
            If RadCmbBxScheduleName.SelectedValue = "" Then
                'RadCmbBxScheduleName.SelectedValue = -1

            End If
            If Not RadCmbBxScheduleName.SelectedValue Is Nothing Then
                Dim scheduleidtemp = RadCmbBxScheduleName.SelectedValue.Substring(0, RadCmbBxScheduleName.SelectedValue.IndexOf(","))
                rptObj.ScheduleId = scheduleidtemp

            End If
            If Not RadComboScheduleType.SelectedValue Is Nothing Then
                rptObj.ScheduleTypeId = Me.RadComboScheduleType.SelectedValue
            End If
        End If
        If rpttid = ShiftsSummaryReport Then
            If ddlWorkSchedule.SelectedValue <> -1 Then
                rptObj.ScheduleTypeId = ddlWorkSchedule.SelectedValue
            Else
                rptObj.ScheduleTypeId = Nothing
            End If
            If ddlShifts.SelectedValue <> -1 Then
                rptObj.ShiftTypeID = ddlShifts.SelectedValue
            Else
                rptObj.ShiftTypeID = Nothing
            End If
        End If
        If rpttid = Employee_StudyNursing_Schedule Then
            rptObj.StudyNursing_Schedule = ddlStudyNursingScheduleType.SelectedValue
            If Not ddlStudyNursingScheduleName.SelectedValue = -1 Then
                rptObj.ScheduleId = ddlStudyNursingScheduleName.SelectedValue
            End If
        End If

        If rpttid = Emp_Notifications Then
            If Not radcmbxNotificationType.SelectedValue = -1 Then
                rptObj.FK_NotificationTypeId = radcmbxNotificationType.SelectedValue
            Else
                rptObj.FK_NotificationTypeId = 0
            End If
        End If

        If rpttid = Emp_HolidayReport Then
            If Not radcmbxHolidays.SelectedValue = -1 Then
                rptObj.FK_HolidayId = radcmbxHolidays.SelectedValue
            Else
                rptObj.FK_HolidayId = 0
            End If
        End If

        'rpttid = IIf(rpttid = 0, 3, rpttid)
        Select Case rpttid
            Case EmployeeListReport
                RPTName = "rptEmployeeList.rpt"
                DT = rptObj.GetFilterdEmpList()
                'Case 2 'Employee list
                '    RPTName = "rptEmployeeList.rpt"
                '    DT = rptObj.GetFilterdEmpList()

            Case ReportDeputyManager
                RPTName = "rptDeputyManager.rpt"
                DT = rptObj.GetDeputy_Manager
            Case EmpTAExceptions
                RPTName = "rptEmpTAExceptions.rpt"
                DT = rptObj.Get_EmpTAExceptions
            Case ShiftManagers
                RPTName = "rpt_ShiftManager.rpt"
                DT = rptObj.Get_ShiftManagers
            Case EmployeeCardDetails
                RPTName = "rptEmpCards.rpt"
                DT = rptObj.GetCardDetails()
            Case ScheduleGroupsReport
                RPTName = "rptSchedulesGroup.rpt"
                DT = rptObj.GetSchedulesGroup()
            Case EmployeeSchedules
                RPTName = "rptEmp_Schedules.rpt"
                DT = rptObj.GetEmployeeSchdules()
            Case SchedulesList
                RPTName = "rpt_SchedulesList.rpt"
                DT = rptObj.GetSchedulesList
            Case ManagerReport
                RPTName = "rptManager.rpt"
                DT = rptObj.GetManager
            Case EmployeeDetails
                RPTName = "rptEmpDetails.rpt"
                DT = rptObj.GetEmployeeDetailsWithPhoto()
            Case EmployeeShiftDetails
                RPTName = "rptShiftDetails.rpt"
                DT = rptObj.GetEmployeeShiftDetails()
            Case EmployeeShiftDetails_ScheduleGroup
                RPTName = "rptShiftDetails_ScheduleGroup.rpt"
                DT = rptObj.GetEmployeeShiftDetailsWithGroups()
            Case ShiftsSummaryReport
                Dim dt2 As DataTable
                RPTName = "rpt_ShiftDetails_Summary.rpt"
                dt2 = rptObj.GetEmployeeShiftDetailsSummary()
                Try
                    If rblNoofShifts.SelectedValue > 0 And rblNoofShifts.SelectedIndex >= 0 Then
                        Dim colname As String = dt2.Columns(ddlShifts.SelectedIndex + 3).Caption
                        Dim condition As String
                        If rblNoofShifts.SelectedValue = 1 Then
                            condition = " >= "
                            DT = dt2.Select(colname + condition + txtNoOfShifts.Text).CopyToDataTable()
                        ElseIf rblNoofShifts.SelectedValue = 2 Then
                            condition = " < "
                            DT = dt2.Select(colname + condition + txtNoOfShifts.Text).CopyToDataTable()
                        End If
                    Else
                        DT = dt2
                    End If
                Catch ex As Exception

                End Try
            Case EmployeeOnCallReport
                RPTName = "rpt_EmpOnCall.rpt"
                DT = rptObj.GetEmployeeOnCallDetails()
            Case EmployeeViolationExceptions
                RPTName = "rptEmpViolationExceptions.rpt"
                DT = rptObj.GetEmployeeViolationExceptions()
            Case EmployeeResumptionReport
                RPTName = "rptDutyResumption.rpt"
                DT = rptObj.GetEmployeeDutyResumption()
            Case Employee_WorkLocations
                RPTName = "rptEmpLocation.rpt"
                DT = rptObj.Get_Emp_WorkLocations
            Case Employee_Multi_ReaderLocations
                RPTName = "rptEmpMultiReaderLocation.rpt"
                DT = rptObj.Get_Emp_MultipleReader_Locations

            Case Employee_StudyNursing_Schedule
                RPTName = "rpt_Employee_StudyNursing_Schedule.rpt"
                DT = rptObj.Get_EmpStudyNursing_Schedule

            Case Emp_Notifications
                RPTName = "rpt_Notifications.rpt"
                DT = rptObj.Get_rptNotifications
            Case Emp_HolidayReport
                RPTName = "rpt_EmpHoliday.rpt"
                DT = rptObj.Get_rptHolidays
            Case Emp_SysUsersReport
                RPTName = "rpt_EmpSysUsers.rpt"
                DT = rptObj.Get_rptEmpSysUsers

            Case Emp_SummaryEventLog
                RPTName = "rptSummaryEventLog.rpt"
                DT = rptObj.Get_SummaryEventsLog
        End Select

        UserName = SessionVariables.LoginUser.UsrID
        ' Version = System.Configuration.ConfigurationManager.AppSettings("Version").ToString()

        If (Lang = CtlCommon.Lang.AR) Then
            RPTName = "Arabic/" + RPTName
        Else
            RPTName = "English/" + RPTName
        End If

    End Sub

    Private Sub FillStatus()
        If SessionVariables.CultureInfo = "ar-JO" Then
            Dim item1 As New RadComboBoxItem
            Dim item2 As New RadComboBoxItem
            Dim item3 As New RadComboBoxItem
            Dim item4 As New RadComboBoxItem
            Dim item5 As New RadComboBoxItem

            item1.Value = -1
            item1.Text = "--الرجاء الاختيار--"
            ddlStatus.Items.Add(item1)
            item2.Value = 1
            item2.Text = "قيد الاعتماد"
            ddlStatus.Items.Add(item2)
            item3.Value = 2
            item3.Text = "قيد الطباعة"
            ddlStatus.Items.Add(item3)
            item4.Value = 3
            item4.Text = "تمت الطباعة"
            ddlStatus.Items.Add(item4)
            item5.Value = 4
            item5.Text = "مرفوض"
            ddlStatus.Items.Add(item5)

        Else
            Dim item1 As New RadComboBoxItem
            Dim item2 As New RadComboBoxItem
            Dim item3 As New RadComboBoxItem
            Dim item4 As New RadComboBoxItem
            Dim item5 As New RadComboBoxItem
            item1.Value = -1
            item1.Text = "--Please Select--"
            ddlStatus.Items.Add(item1)
            item2.Value = 1
            item2.Text = "Pending"
            ddlStatus.Items.Add(item2)
            item3.Value = 2
            item3.Text = "UnderPrinting"
            ddlStatus.Items.Add(item3)
            item4.Value = 3
            item4.Text = "Printed"
            ddlStatus.Items.Add(item4)
            item5.Value = 4
            item5.Text = "Rejected"
            ddlStatus.Items.Add(item5)

        End If
    End Sub

    Private Sub FillSchedule()
        If SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
        Else
            Lang = CtlCommon.Lang.EN
        End If

        Dim objWorkSchedule As New WorkSchedule()
        If (RadComboScheduleType.SelectedValue = 1) Then
            CtlCommon.FillTelerikDropDownList(RadCmbBxScheduleName, objWorkSchedule.GetByType(1), Lang)
        ElseIf (RadComboScheduleType.SelectedValue = 2) Then
            CtlCommon.FillTelerikDropDownList(RadCmbBxScheduleName, objWorkSchedule.GetByType(2), Lang)
        ElseIf (RadComboScheduleType.SelectedValue = 3) Then
            CtlCommon.FillTelerikDropDownList(RadCmbBxScheduleName, objWorkSchedule.GetByType(3), Lang)
        ElseIf (RadComboScheduleType.SelectedValue = 5) Then
            CtlCommon.FillTelerikDropDownList(RadCmbBxScheduleName, objWorkSchedule.GetByType(5), Lang)
        End If
    End Sub

    Private Sub ClearAll()
        RadCmbBxScheduleName.Items.Clear()
        RadCmbBxScheduleName.Text = String.Empty
        Me.RadComboScheduleType.SelectedValue = -1
        RadCmbBxScheduleName.SelectedValue = -1
    End Sub

    Private Sub FillExcelreport(ByVal reportname As String)
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
        If Not RadComboEmployeeReports.SelectedValue = EmployeeSchedules Or Not RadComboEmployeeReports.SelectedValue = Employee_WorkLocations Or Not RadComboEmployeeReports.SelectedValue = Employee_Multi_ReaderLocations Then
            HeadDT.Rows.Add(3)
        End If

        Dim rptName As String = RadComboEmployeeReports.SelectedValue
        Dim dtFormName As DataTable
        dtFormName = objsysforms.GetBy_FormName(rptName)
        If Lang = CtlCommon.Lang.AR Then

            HeadDT(0)(0) = dtFormName(0)("Desc_Ar")
            '------Column(0)-----------
            HeadDT(1)(1) = "طبع بواسطة:"
            HeadDT(2)(1) = "تاريخ الطباعة:"
            If Not RadComboEmployeeReports.SelectedValue = EmployeeSchedules Or Not RadComboEmployeeReports.SelectedValue = Employee_WorkLocations Or Not RadComboEmployeeReports.SelectedValue = Employee_Multi_ReaderLocations Then
                HeadDT(3)(1) = "من تاريخ: " & Convert.ToDateTime(RadDatePicker1.SelectedDate).ToString("dd/MM/yyyy")
            End If
            '------Column(1)-----------
            HeadDT(1)(0) = SessionVariables.LoginUser.UsrID
            HeadDT(2)(0) = Date.Today.ToString("dd/MM/yyyy")
            If Not RadComboEmployeeReports.SelectedValue = EmployeeSchedules Or Not RadComboEmployeeReports.SelectedValue = Employee_WorkLocations Or Not RadComboEmployeeReports.SelectedValue = Employee_Multi_ReaderLocations Then
                HeadDT(3)(0) = "الى تاريخ: " & Convert.ToDateTime(RadDatePicker2.SelectedDate).ToString("dd/MM/yyyy")
            End If
        Else
            HeadDT(0)(0) = dtFormName(0)("Desc_En").ToString
            '------Column(0)-----------
            HeadDT(1)(0) = "Printed By:"
            HeadDT(2)(0) = "Printing Date:"
            If Not RadComboEmployeeReports.SelectedValue = EmployeeSchedules Or Not RadComboEmployeeReports.SelectedValue = Employee_WorkLocations Or Not RadComboEmployeeReports.SelectedValue = Employee_Multi_ReaderLocations Then
                HeadDT(3)(0) = "From Date: " & Convert.ToDateTime(RadDatePicker1.SelectedDate).ToString("dd/MM/yyyy")
            End If
            '------Column(1)-----------
            HeadDT(1)(1) = SessionVariables.LoginUser.UsrID
            HeadDT(2)(1) = Date.Today.ToString("dd/MM/yyyy")
            If Not RadComboEmployeeReports.SelectedValue = EmployeeSchedules Or Not RadComboEmployeeReports.SelectedValue = Employee_WorkLocations Or Not RadComboEmployeeReports.SelectedValue = Employee_Multi_ReaderLocations Then
                HeadDT(3)(1) = "To Date: " & Convert.ToDateTime(RadDatePicker2.SelectedDate).ToString("dd/MM/yyyy")
            End If
        End If

        HeadDT.Rows.Add(drFormName)
        '------------------ADD REPORT HEADER-----------------
        Select Case reportname
            Case EmployeeListReport
                NDT.Columns(14).DataType = System.Type.GetType("System.String")
                NDT.Columns(15).DataType = System.Type.GetType("System.String")

                For Each row As DataRow In DT.Rows
                    Dim dr As DataRow = NDT.NewRow
                    For i As Integer = 0 To NDT.Columns.Count - 1
                        dr(i) = row(i)
                        If i = 14 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                            End If
                        ElseIf i = 15 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                            End If
                        End If
                    Next
                    NDT.Rows.Add(dr)
                Next

                NDT.Columns.RemoveAt(0)
                NDT.Columns.RemoveAt(3)
                NDT.Columns.RemoveAt(5)
                NDT.Columns.RemoveAt(7)
                NDT.Columns.RemoveAt(9)
                NDT.Columns.RemoveAt(14)
                NDT.Columns.RemoveAt(14)
                NDT.Columns.RemoveAt(14)

                If Lang = CtlCommon.Lang.AR Then
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns(1).ColumnName = "إسم الموظف"
                    NDT.Columns(2).ColumnName = "إسم الموظف باللغه العربيه"
                    NDT.Columns(3).ColumnName = "إسم وحدة العمل"
                    NDT.Columns(4).ColumnName = "إسم وحدة العمل باللغه العربيه"
                    NDT.Columns(5).ColumnName = "إسم الشركه"
                    NDT.Columns(6).ColumnName = "إسم الشركه باللغه العربيه"
                    NDT.Columns(7).ColumnName = "التعيين"
                    NDT.Columns(8).ColumnName = "تعيين العربية"
                    NDT.Columns(9).ColumnName = "تاريخ الميلاد"
                    NDT.Columns(10).ColumnName = "تاريخ الالتحاق بالعمل"
                    NDT.Columns(11).ColumnName = "البريد الإلكتروني"
                    NDT.Columns(12).ColumnName = "الجنس"
                    NDT.Columns(13).ColumnName = "ملاحظات"
                    NDT.Columns(14).ColumnName = "الجنسيه"
                    NDT.Columns(15).ColumnName = "الجنسيه باللغه العربيه"
                Else
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns(2).ColumnName = "Employee Arabic Name"
                    NDT.Columns(3).ColumnName = "Entity Name"
                    NDT.Columns(4).ColumnName = "Entity Arabic Name"
                    NDT.Columns(5).ColumnName = "Company Name"
                    NDT.Columns(6).ColumnName = "Company Arabic Name"
                    NDT.Columns(7).ColumnName = "Designation"
                    NDT.Columns(8).ColumnName = "Designation Arabic Name"
                    NDT.Columns(9).ColumnName = "Date Of Birth"
                    NDT.Columns(10).ColumnName = "Join Date"
                    NDT.Columns(11).ColumnName = "Email"
                    NDT.Columns(12).ColumnName = "Gender"
                    NDT.Columns(13).ColumnName = "Remarks"
                    NDT.Columns(14).ColumnName = "Nationality Name"
                    NDT.Columns(15).ColumnName = "Nationality Arabic Name"
                End If
            Case ReportDeputyManager
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
                                dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                            End If
                        End If
                    Next
                    NDT.Rows.Add(dr)
                Next
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(0).ColumnName = "رقم نائب المدير"
                    NDT.Columns(1).ColumnName = "اسم نائب المدير"
                    NDT.Columns(2).ColumnName = "اسم نائب المدير باللغة العربية"
                    NDT.Columns(3).ColumnName = "رقم المدير"
                    NDT.Columns(4).ColumnName = "اسم المدير"
                    NDT.Columns(5).ColumnName = "اسم المدير باللغة العربية"
                    NDT.Columns(6).ColumnName = "من تاريخ"
                    NDT.Columns(7).ColumnName = "الى تاريخ"
                    NDT.Columns(8).ColumnName = "اسم الشركة"
                    NDT.Columns(9).ColumnName = "اسم الشركة باللغة العربية"
                    NDT.Columns(10).ColumnName = "اسم وحدة العمل"
                    NDT.Columns(11).ColumnName = "اسم وحدة العمل ياللغة العربية"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                Else
                    NDT.Columns(0).ColumnName = "Deputy Manager No"
                    NDT.Columns(1).ColumnName = "Deputy Manager Name"
                    NDT.Columns(2).ColumnName = "Deputy Manager Arabic Name"
                    NDT.Columns(3).ColumnName = "Manager No."
                    NDT.Columns(4).ColumnName = "Manager Name"
                    NDT.Columns(5).ColumnName = "Manager Arabic Name"
                    NDT.Columns(6).ColumnName = "From Date"
                    NDT.Columns(7).ColumnName = "To Date"
                    NDT.Columns(8).ColumnName = "Company Name"
                    NDT.Columns(9).ColumnName = "Company Arabic Name"
                    NDT.Columns(10).ColumnName = "Entity Name"
                    NDT.Columns(11).ColumnName = "Entity Arabic Name"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)

                End If
            Case EmployeeCardDetails
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(14).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 14 Then
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
                    NDT.Columns(2).ColumnName = "السبب"
                    NDT.Columns(3).ColumnName = "سبب اخر"
                    NDT.Columns(4).ColumnName = "الحالة"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "اسم الشركة"
                    NDT.Columns(7).ColumnName = "مقدم الطلب"
                    NDT.Columns(8).ColumnName = "تاريخ الطلب"
                Else
                    NDT.Columns(14).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 14 Then
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
                    NDT.Columns(2).ColumnName = "Reason"
                    NDT.Columns(3).ColumnName = "Other Reason"
                    NDT.Columns(4).ColumnName = "Status"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "Requested By"
                    NDT.Columns(8).ColumnName = "Request Date"
                End If
            Case EmpTAExceptions

                If (Lang = CtlCommon.Lang.AR) Then
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
                    NDT.Columns(2).ColumnName = "من تاريخ"
                    NDT.Columns(3).ColumnName = "الى تاريخ"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "السبب"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "اسم الشركة"

                Else
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
                    NDT.Columns(2).ColumnName = "From Date"
                    NDT.Columns(3).ColumnName = "To Date"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Reason"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(7)
                End If
            Case ShiftManagers

                If (Lang = CtlCommon.Lang.AR) Then
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
                    NDT.Columns(2).ColumnName = "من تاريخ"
                    NDT.Columns(3).ColumnName = "الى تاريخ"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "اسم المجموعة"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "اسم الشركة"

                Else
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
                    NDT.Columns(2).ColumnName = "From Date"
                    NDT.Columns(3).ColumnName = "To Date"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Group Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(7)
                End If
            Case ScheduleGroupsReport
                If (Lang = CtlCommon.Lang.AR) Then

                    NDT.Columns(5).DataType = System.Type.GetType("System.String")
                    NDT.Columns(7).DataType = System.Type.GetType("System.String")
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")
                    NDT.Columns(10).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)

                            If i = 5 Then
                                If Not dr(i) Is DBNull.Value Then
                                    If dr(i) = True Then
                                        dr(i) = Convert.ToString("فعاله")
                                    ElseIf dr(i) = False Then
                                        dr(i) = Convert.ToString("غير فعاله")
                                    End If
                                End If
                            ElseIf i = 7 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 10 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            End If

                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "اسم المجموعة"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "الحالة"
                    NDT.Columns(2).ColumnName = "أنشى من قبل"
                    NDT.Columns(3).ColumnName = "تاريخ الإنشاء "
                    NDT.Columns(4).ColumnName = "تم التعديل من قبل"
                    NDT.Columns(5).ColumnName = "تاريخ التعديل "
                    NDT.Columns(6).ColumnName = "تاريخ بداية المجموعة"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "اسم الموظف"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                Else
                    NDT.Columns(5).DataType = System.Type.GetType("System.String")
                    NDT.Columns(7).DataType = System.Type.GetType("System.String")
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")
                    NDT.Columns(10).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)

                            If i = 5 Then
                                If Not dr(i) Is DBNull.Value Then
                                    If dr(i) = True Then
                                        dr(i) = Convert.ToString("Active")
                                    ElseIf dr(i) = False Then
                                        dr(i) = Convert.ToString("Not Active")
                                    End If
                                End If
                            ElseIf i = 7 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 10 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            End If

                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Group Name"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "Status"
                    NDT.Columns(2).ColumnName = "Created By"
                    NDT.Columns(3).ColumnName = "Created Date"
                    NDT.Columns(4).ColumnName = "Last Updated By"
                    NDT.Columns(5).ColumnName = "Last Updated Date"
                    NDT.Columns(6).ColumnName = "Scdeule Start Date"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "Emp No."
                    NDT.Columns(8).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                End If
            Case EmployeeSchedules

                If (Lang = CtlCommon.Lang.AR) Then

                    NDT.Columns(7).DataType = System.Type.GetType("System.String")
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)

                            If i = 7 Then
                                If Not dr(i) Is DBNull.Value Then
                                    If dr(i) = 1 Then
                                        dr(i) = Convert.ToString("عادي")
                                    ElseIf dr(i) = 2 Then
                                        dr(i) = Convert.ToString("مرن")
                                    ElseIf dr(i) = 3 Then
                                        dr(i) = Convert.ToString("مناوبات")
                                    End If
                                End If
                            ElseIf i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    If dr(i) = 1 Then
                                        dr(i) = Convert.ToString("عادي")
                                    ElseIf dr(i) = 2 Then
                                        dr(i) = Convert.ToString("مرن")
                                    ElseIf dr(i) = 3 Then
                                        dr(i) = Convert.ToString("مناوبات")
                                    End If
                                End If
                            ElseIf i = 13 Then
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
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "اسم الجدول السابق"
                    NDT.Columns(3).ColumnName = "نوع الجدول السابق"
                    NDT.Columns(4).ColumnName = "تاريخ بدء الجدول السابق"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "اسم الجدول الحالي"
                    NDT.Columns(6).ColumnName = "نوع الجدول الحالي"
                    NDT.Columns(7).ColumnName = "تاريخ بدء الجدول الحالي"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "اسم الشركة"

                Else
                    NDT.Columns(7).DataType = System.Type.GetType("System.String")
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 7 Then
                                If Not dr(i) Is DBNull.Value Then
                                    If dr(i) = 1 Then
                                        dr(i) = Convert.ToString("Normal")
                                    ElseIf dr(i) = 2 Then
                                        dr(i) = Convert.ToString("Flexible")
                                    ElseIf dr(i) = 3 Then
                                        dr(i) = Convert.ToString("Advanced")
                                    End If
                                End If
                            ElseIf i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    If dr(i) = 1 Then
                                        dr(i) = Convert.ToString("Normal")
                                    ElseIf dr(i) = 2 Then
                                        dr(i) = Convert.ToString("Flexible")
                                    ElseIf dr(i) = 3 Then
                                        dr(i) = Convert.ToString("Advanced")
                                    End If
                                End If
                            ElseIf i = 13 Then
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
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Previous Schedule Name"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "Previous Schedule Type"
                    NDT.Columns(4).ColumnName = "Previous Schedule Start Date"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Cuurent Schedule Name"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Cuurent Schedule Type"
                    NDT.Columns(7).ColumnName = "Cuurent Schedule Start Date"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(10)

                End If
            Case SchedulesList

                If (Lang = CtlCommon.Lang.AR) Then

                    NDT.Columns(14).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)

                            If i = 14 Then
                                If Not dr(i) Is DBNull.Value Then
                                    If dr(i) = True Then
                                        dr(i) = Convert.ToString("نعم")
                                    ElseIf dr(i) = False Then
                                        dr(i) = Convert.ToString("لا")
                                    End If
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    If dr(i) = 1 Then
                                        dr(i) = Convert.ToString("عادي")
                                    ElseIf dr(i) = 2 Then
                                        dr(i) = Convert.ToString("مرن")
                                    ElseIf dr(i) = 3 Then
                                        dr(i) = Convert.ToString("مناوبات")
                                    End If
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "اسم الجدول"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "يوم الاسبوع"
                    NDT.Columns(2).ColumnName = "من وقت 1"
                    NDT.Columns(3).ColumnName = "إلى وقت 1"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "مجموع ساعات الجدول"
                    NDT.Columns(5).ColumnName = "المده 1"
                    NDT.Columns(6).ColumnName = "من وقت 2"
                    NDT.Columns(7).ColumnName = "إلى وقت 2"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "المده 2"
                    NDT.Columns(9).ColumnName = "يوم عطله"
                    NDT.Columns(10).ColumnName = "نوع الجدول"

                Else
                    NDT.Columns(14).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)

                            If i = 14 Then
                                If Not dr(i) Is DBNull.Value Then
                                    If dr(i) = True Then
                                        dr(i) = Convert.ToString("Yes")
                                    ElseIf dr(i) = False Then
                                        dr(i) = Convert.ToString("No")
                                    End If
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    If dr(i) = 1 Then
                                        dr(i) = Convert.ToString("Normal")
                                    ElseIf dr(i) = 2 Then
                                        dr(i) = Convert.ToString("Flexible")
                                    ElseIf dr(i) = 3 Then
                                        dr(i) = Convert.ToString("Advanced")
                                    End If
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Schedule Name"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "Week Day"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "From Time 1"
                    NDT.Columns(3).ColumnName = "To Time 1"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Total Work Hours"
                    NDT.Columns(5).ColumnName = "Duration 1"
                    NDT.Columns(6).ColumnName = "From Time 2"
                    NDT.Columns(7).ColumnName = "To Time 2"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Duration 2"
                    NDT.Columns(9).ColumnName = "OFF Day"
                    NDT.Columns(10).ColumnName = "Schedule Type"

                End If
            Case ManagerReport
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
                    NDT.Columns(0).ColumnName = "رقم المدير"
                    NDT.Columns(1).ColumnName = "اسم المدير"
                    NDT.Columns(2).ColumnName = "اسم المدير باللغة العربية"

                    NDT.Columns(3).ColumnName = "من تاريخ"
                    NDT.Columns(4).ColumnName = "الى تاريخ"
                    NDT.Columns(5).ColumnName = "اسم الشركة"
                    NDT.Columns(6).ColumnName = "اسم الشركة باللغة العربية"
                    NDT.Columns(7).ColumnName = "اسم وحدة العمل"
                    NDT.Columns(8).ColumnName = "اسم وحدة العمل ياللغة العربية"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    'NDT.Columns.RemoveAt(9)
                Else
                    NDT.Columns(0).ColumnName = " Manager No"
                    NDT.Columns(1).ColumnName = " Manager Name"
                    NDT.Columns(2).ColumnName = " Manager Arabic Name"

                    NDT.Columns(3).ColumnName = "From Date"
                    NDT.Columns(4).ColumnName = "To Date"
                    NDT.Columns(5).ColumnName = "Company Name"
                    NDT.Columns(6).ColumnName = "Company Arabic Name"
                    NDT.Columns(7).ColumnName = "Entity Name"
                    NDT.Columns(8).ColumnName = "Entity Arabic Name"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    'NDT.Columns.RemoveAt(9)

                End If
            Case EmployeeDetails
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(7).DataType = System.Type.GetType("System.String")
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 7 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            End If

                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns(1).ColumnName = "رقم بطاقة الموظف"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "اسم الموظف"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "الجنسية"
                    NDT.Columns(4).ColumnName = "تاريخ التعيين"
                    NDT.Columns(5).ColumnName = "تاريخ الولادة"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "الديانة"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "نوع الموظف"

                Else
                    NDT.Columns(7).DataType = System.Type.GetType("System.String")
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 7 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            End If

                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Card No."
                    NDT.Columns(2).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "Nationality"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Join Date"
                    NDT.Columns(5).ColumnName = "Birth Date"
                    NDT.Columns(6).ColumnName = "Religon"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "Employee Type"
                    NDT.Columns.RemoveAt(10)

                End If
            Case ShiftsSummaryReport
                NDT = DT
            Case EmployeeShiftDetails
                NDT = DT
            Case EmployeeOnCallReport
                NDT = DT
            Case EmployeeViolationExceptions
                If (Lang = CtlCommon.Lang.AR) Then
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
                    NDT.Columns(2).ColumnName = "من تاريخ"
                    NDT.Columns(3).ColumnName = "الى تاريخ"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "السبب"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "اسم الشركة"

                Else
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
                    NDT.Columns(2).ColumnName = "From Date"
                    NDT.Columns(3).ColumnName = "To Date"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Reason"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(7)
                End If
            Case EmployeeResumptionReport
                NDT = DT

            Case Employee_WorkLocations
                NDT = DT
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "المسمى الوظيفي"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "اسم موقع العمل"
                    For Each row As DataRow In NDT.Rows
                        If row("المسمى الوظيفي").ToString = "" Then
                            row("المسمى الوظيفي") = "غير محدد"
                        End If
                    Next
                Else
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Employee No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Designation Name"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "WorkLocation Name"
                    For Each row As DataRow In NDT.Rows
                        If row("Designation Name").ToString = "" Then
                            row("Designation Name") = "Not Defined"
                        End If
                    Next
                End If

            Case Employee_Multi_ReaderLocations
                NDT = DT
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "المسمى الوظيفي"
                    NDT.Columns(5).ColumnName = "اسم موقع القارئ"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                Else
                    NDT.Columns(0).ColumnName = "Employee No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Designation Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Reader Location Name"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                End If

            Case Employee_StudyNursing_Schedule
                NDT.Columns(5).DataType = System.Type.GetType("System.String")
                NDT.Columns(6).DataType = System.Type.GetType("System.String")


                For Each row As DataRow In DT.Rows
                    Dim dr As DataRow = NDT.NewRow
                    For i As Integer = 0 To NDT.Columns.Count - 1
                        dr(i) = row(i)
                        If i = 5 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                            End If
                        ElseIf i = 6 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                            End If
                        End If
                    Next
                    NDT.Rows.Add(dr)
                Next
                If Lang = CtlCommon.Lang.AR Then
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "اسم الجدول"
                    NDT.Columns(3).ColumnName = "من تاريخ"
                    NDT.Columns(4).ColumnName = "الى تاريخ"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                Else
                    NDT.Columns(0).ColumnName = "Employee No"
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Schedule Name"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "From Date"
                    NDT.Columns(4).ColumnName = "To Date"
                    NDT.Columns(5).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                End If
            Case EmployeeShiftDetails_ScheduleGroup
                NDT.Columns(4).DataType = System.Type.GetType("System.String")
                For Each row As DataRow In DT.Rows
                    Dim dr As DataRow = NDT.NewRow
                    For i As Integer = 0 To NDT.Columns.Count - 1
                        dr(i) = row(i)
                        If i = 4 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                            End If
                        End If
                    Next
                    NDT.Rows.Add(dr)
                Next
                If Lang = CtlCommon.Lang.AR Then
                    DT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "التاريخ"
                    NDT.Columns(3).ColumnName = "رمز المناوبة"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "اسم المناوبة"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "اسم المجموعة"
                    NDT.Columns(6).ColumnName = "وقت الجدول"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "اسم الشركة"
                Else
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Employee No"
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Date"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "Shift Code"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Shift Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Group Name"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Schedule Time"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(9)
                End If

            Case Emp_Notifications
                NDT.Columns(10).DataType = System.Type.GetType("System.String")

                For Each row As DataRow In DT.Rows
                    Dim dr As DataRow = NDT.NewRow
                    For i As Integer = 0 To NDT.Columns.Count - 1
                        dr(i) = row(i)
                        If i = 10 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                            End If
                        End If
                    Next
                    NDT.Rows.Add(dr)
                Next

                If Lang = CtlCommon.Lang.AR Then
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "إسم الموظف"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "إسم وحدة العمل"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "إسم الشركه"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "النوع"
                    NDT.Columns(5).ColumnName = "تاريخ الاشعار"
                    NDT.Columns(6).ColumnName = "الحالة"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)

                Else
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Type"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Notification Date"
                    NDT.Columns(6).ColumnName = "Status"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                End If

            Case Emp_HolidayReport
                NDT.Columns(4).DataType = System.Type.GetType("System.String")

                For Each row As DataRow In DT.Rows
                    Dim dr As DataRow = NDT.NewRow
                    For i As Integer = 0 To NDT.Columns.Count - 1
                        dr(i) = row(i)
                        If i = 4 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                            End If
                        End If
                    Next
                    NDT.Rows.Add(dr)
                Next

                If Lang = CtlCommon.Lang.AR Then
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "إسم الموظف"
                    NDT.Columns(2).ColumnName = "التاريخ"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "إسم العطلة"
                    NDT.Columns(4).ColumnName = "تاريخ البداية"
                    NDT.Columns(5).ColumnName = "تاريخ النهاية"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "اسم الشركة"

                Else
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Date"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "Holiday Name"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Start Date"
                    NDT.Columns(5).ColumnName = "End Date"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(8)
                End If

            Case Emp_SysUsersReport

                'NDT = DT
                NDT.Columns(6).DataType = System.Type.GetType("System.String")
                For Each row As DataRow In DT.Rows
                    Dim dr As DataRow = NDT.NewRow
                    For i As Integer = 0 To NDT.Columns.Count - 1
                        dr(i) = row(i)
                        If i = 6 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToString(row(i)).ToString()
                            End If
                        End If
                    Next
                    NDT.Rows.Add(dr)
                Next

                If Lang = CtlCommon.Lang.AR Then
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "إسم الموظف"
                    NDT.Columns(2).ColumnName = "إسم المستخدم"
                    NDT.Columns(3).ColumnName = "البريد الالكتروني"
                    NDT.Columns(4).ColumnName = "الحالة"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "اسم الشركة"

                    For Each row As DataRow In NDT.Rows
                        If row(4).ToString = "1" Then
                            row(4) = "نشط"
                        Else
                            row(4) = "غير نشط"
                        End If
                    Next

                Else
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "User Name"
                    NDT.Columns(3).ColumnName = "Email"
                    NDT.Columns(4).ColumnName = "Status"
                    NDT.Columns(5).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Company Name"

                    For Each row As DataRow In NDT.Rows
                        If row(4).ToString = "1" Then
                            row(4) = "Active"
                        Else
                            row(4) = "In Active"
                        End If
                    Next

                End If

            Case Emp_SummaryEventLog
                NDT = DT
                If Lang = CtlCommon.Lang.AR Then
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "الاجازات\ اضافة"
                    NDT.Columns(5).ColumnName = "الاجازات\ تعديل"
                    NDT.Columns(6).ColumnName = "الاجازات\ حذف"
                    NDT.Columns(7).ColumnName = "الاجازات\ موافقة"
                    NDT.Columns(8).ColumnName = "الاجازات\ رفض"
                    NDT.Columns(9).ColumnName = "المغادرات\ اضافة"
                    NDT.Columns(10).ColumnName = "المغادرات\ تعديل"
                    NDT.Columns(11).ColumnName = "المغادرات\ حذف"
                    NDT.Columns(12).ColumnName = "المغادرات\ موافقة"
                    NDT.Columns(13).ColumnName = "المغادرات\ رفض"
                    NDT.Columns(14).ColumnName = "الادخالات اليدوية\ اضافة"
                    NDT.Columns(15).ColumnName = "الادخالات اليدوية\ تعديل"
                    NDT.Columns(16).ColumnName = "الادخالات اليدوية\ حذف"
                    NDT.Columns(17).ColumnName = "الادخالات اليدوية\ موافقة"
                    NDT.Columns(18).ColumnName = "الادخالات اليدوية\ رفض"
                    NDT.Columns(19).ColumnName = "الاستثناءات\ اضافة"
                    NDT.Columns(20).ColumnName = "الاستثناءات\ تعديل"
                    NDT.Columns(21).ColumnName = "الاستثناءات\ حذف"
                    NDT.Columns(22).ColumnName = "الاستثناءات\ موافقة"
                    NDT.Columns(23).ColumnName = "الاستثناءات\ رفض"
                    NDT.Columns(24).ColumnName = "طباعة البطاقات"

                Else
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Employee No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Leaves\Add"
                    NDT.Columns(5).ColumnName = "Leaves\Edit"
                    NDT.Columns(6).ColumnName = "Leaves\Delete"
                    NDT.Columns(7).ColumnName = "Leaves\Approve"
                    NDT.Columns(8).ColumnName = "Leaves\Reject"
                    NDT.Columns(9).ColumnName = "Permissions\Add"
                    NDT.Columns(10).ColumnName = "Permissions\Edit"
                    NDT.Columns(11).ColumnName = "Permissions\Delete"
                    NDT.Columns(12).ColumnName = "Permissions\Approve"
                    NDT.Columns(13).ColumnName = "Permissions\Reject"
                    NDT.Columns(14).ColumnName = "Manual Entries\Add"
                    NDT.Columns(15).ColumnName = "Manual Entries\Edit"
                    NDT.Columns(16).ColumnName = "Manual Entries\Delete"
                    NDT.Columns(17).ColumnName = "Manual Entries\Approve"
                    NDT.Columns(18).ColumnName = "Manual Entries\Reject"
                    NDT.Columns(19).ColumnName = "Exceptions\Add"
                    NDT.Columns(20).ColumnName = "Exceptions\Edit"
                    NDT.Columns(21).ColumnName = "Exceptions\Delete"
                    NDT.Columns(22).ColumnName = "Exceptions\Approve"
                    NDT.Columns(23).ColumnName = "Exceptions\Reject"
                    NDT.Columns(24).ColumnName = "Print Card(s)"
                End If

        End Select
    End Sub

    Private Sub FillReports(ByVal formID As Integer, ByVal groupID As Integer)

        CtlCommon.FillTelerikDropDownList(RadComboEmployeeReports, objsysforms.GetFormsByParentID(formID, groupID), Lang)

    End Sub

    Private Sub FillSchGroup()
        Try
            Dim obj As New ScheduleGroups
            If SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            CtlCommon.FillTelerikDropDownList(Me.RadCombBxGroupName, obj.GetAllForFill(Lang), Lang)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("../Reports/Employee_Reports.aspx")
    End Sub

    Private Sub ddlWorkSchedule_Bind()
        Try
            Dim objWorkSchedule As New WorkSchedule()
            objWorkSchedule.ScheduleType = 3
            If SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            CtlCommon.FillTelerikDropDownList(Me.ddlWorkSchedule, objWorkSchedule.GetAllByType(), Lang)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub FillShifts(ByVal WorkScheduleId As Integer)
        Try
            Dim objWorkSchedule As New WorkSchedule_Shifts()
            objWorkSchedule.FK_ScheduleId = WorkScheduleId
            If SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            CtlCommon.FillTelerikDropDownList(Me.ddlShifts, objWorkSchedule.GetShiftDetailsbyWorkScheduleIdForReport, Lang)

        Catch ex As Exception
        End Try
    End Sub

    Public Sub ClearFilter()
        EmployeeFilter1.ClearValues()

    End Sub

    Private Sub FillScheduleName()
        objWorkSchedule = New WorkSchedule
        With objWorkSchedule
            .StudyNursing_Schedule = StudyNursing_Schedule
            CtlCommon.FillTelerikDropDownList(ddlStudyNursingScheduleName, .Get_ByStudyNursing_Schedule, Lang)
        End With
    End Sub

    Private Sub FillScheduleTypes()
      If SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR

            Dim item1 As New RadComboBoxItem
            Dim item2 As New RadComboBoxItem
            Dim item3 As New RadComboBoxItem
            Dim item4 As New RadComboBoxItem
            item1.Value = -1
            item1.Text = "--الرجاء الاختيار--"
            ddlStudyNursingScheduleType.Items.Add(item1)
            item2.Value = 1
            item2.Text = "جدول رضاعة"
            ddlStudyNursingScheduleType.Items.Add(item2)
            item3.Value = 2
            item3.Text = "جدول دراسة"
            ddlStudyNursingScheduleType.Items.Add(item3)
           
        Else
            Lang = CtlCommon.Lang.EN
            Dim item1 As New RadComboBoxItem
            Dim item2 As New RadComboBoxItem
            Dim item3 As New RadComboBoxItem
            Dim item4 As New RadComboBoxItem
            item1.Value = -1
            item1.Text = "--Please Select--"
            ddlStudyNursingScheduleType.Items.Add(item1)
            item2.Value = 1
            item2.Text = "Nursing Schedule"
            ddlStudyNursingScheduleType.Items.Add(item2)
            item3.Value = 2
            item3.Text = "Study Schedule"
            ddlStudyNursingScheduleType.Items.Add(item3)
          
        End If
    End Sub

    Private Sub Fill_NotificationTypes()
        Dim dt As DataTable
        objNotificationType = New NotificationType
        Dim strNotifications As String = SmartV.Version.version.GetNotificationTypes()
        With objNotificationType
            If Not strNotifications = String.Empty Then
                objNotificationType.strNotificationTypeId = strNotifications
                dt = objNotificationType.GetBystrNotificationTypeId
            End If
            CtlCommon.FillTelerikDropDownList(radcmbxNotificationType, dt, Lang)
        End With
    End Sub

    Private Sub FillHolidays()
        objHoliday = New Holiday
        With objHoliday
            CtlCommon.FillTelerikDropDownList(radcmbxHolidays, .GetAll, Lang)
        End With
    End Sub

#End Region


End Class
