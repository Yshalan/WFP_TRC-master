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
Imports TA.Forms
Imports TA.Definitions

Partial Class Reports_SelfServices_Other_Reports
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objAPP_Settings As APP_Settings
    Private Lang As CtlCommon.Lang
    Protected dir As String
    Private objVersion As SmartV.Version.version
    Private objsysforms As New SYSForms
    Private objSys_Forms As New Sys_Forms

    Private objOrgCompany As OrgCompany
    Dim CultureInfo As System.Globalization.CultureInfo
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim cryRpt As New ReportDocument
    Dim objSmartSecurity As New SmartSecurity
    Private objRequestStatus As New TA.Lookup.RequestStatus

    Private EventLogReport As String = "Event_Log"
    Private EventProjectGroups As String = "Event_Groups"
    Private EventEmployees As String = "Event_Employees"
    Private EmployeeEventShifts As String = "Employee_EventShifts"
    Private OrganizationHierarchy As String = "Organization Hierarchy"
    Private OrgEntityManagers As String = "OrgEntityManagers"
    Private ATSReader_Emp_Transaction As String = "ATSReader_Emp_Transaction"
    Private EmployeeWorkStatus_Days As String = "EmployeeWorkStatus_Days"
    Private Integration_ErrorLogs As String = "Integration_ErrorLogs"
    Private IntegrationMissingLeaves As String = "IntegrationMissingLeaves"

    Private objEvents As Events
    Private objEvents_Groups As Events_Groups
    Private objEvents_Employees As Events_Employees
    Private objEmp_logicalGroup As Emp_logicalGroup
    Private objEmployee As Employee
    Private objSYSUsers As SYSUsers

    Private dtCustomizedRecordsOrder As New DataTable
    Private dtDataSource As DataTable
    Private valField As String
    Private EngNameTextField As String
    Private ArNameTextField As String
    Private ParentField As String
    Private Sequence As String


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
        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            formID = row("FormID")
        Next
        If Not Page.IsPostBack Then
            lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقارير أخرى", "Other Reports")
            lblFromDate.Text = IIf(Lang = CtlCommon.Lang.AR, "من تاريخ", "From Date")
            lblToDate.Text = IIf(Lang = CtlCommon.Lang.AR, "الى تاريخ", "To Date")
            btnPrint.Text = IIf(Lang = CtlCommon.Lang.AR, "انشاء", "Generate")
            btnPrintEvent.Text = IIf(Lang = CtlCommon.Lang.AR, "انشاء", "Generate")
            btnPrintOrg.Text = IIf(Lang = CtlCommon.Lang.AR, "انشاء", "Generate")
            btnPrintProject.Text = IIf(Lang = CtlCommon.Lang.AR, "انشاء", "Generate")
            FillReports(formID, SessionVariables.LoginUser.GroupId)

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
        If EmployeeFilter1.IsLevelRequired Then
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            If EmployeeFilter1.CompanyId = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SelectCompany", CultureInfo), "info")
            ElseIf EmployeeFilter1.EntityId = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SelectEntity", CultureInfo), "info")
            Else
                BindReport(reportName)
            End If
        Else
            BindReport(reportName)
        End If
    End Sub
    Protected Sub btnPrintEvent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrintEvent.Click
        Dim reportName As String = RadComboExtraReports.SelectedValue
        BindReport(reportName)
    End Sub
    Protected Sub btnPrintProject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrintProject.Click
        Dim reportName As String = RadComboExtraReports.SelectedValue
        BindReport(reportName)
    End Sub
    Protected Sub btnGroup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGroup.Click
        Dim reportName As String = RadComboExtraReports.SelectedValue
        BindReport(reportName)
    End Sub
    Protected Sub RadCmbBxEvent_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxEvent.SelectedIndexChanged
        If Not RadCmbBxEvent.SelectedValue = -1 Then
            FillEventGroups()
        Else
            RadCmbBxGroup.Items.Clear()
            RadCmbBxGroup.Text = String.Empty
        End If

    End Sub
    Protected Sub RadComboExtraReports_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboExtraReports.SelectedIndexChanged

        RadDatePicker1.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
        Dim dd As New Date
        dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
        RadDatePicker2.SelectedDate = dd
        FillForms()
        FillEvents()
        If RadComboExtraReports.SelectedValue = EventLogReport Then
            MultiView1.SetActiveView(vwEventLogReport)
            If SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                radcmbbxAction.Items(0).Text = "الكل"
                radcmbbxAction.Items(1).Text = "إضافة"
                radcmbbxAction.Items(2).Text = "تعديل"
                radcmbbxAction.Items(3).Text = "حذف"
            Else
                Lang = CtlCommon.Lang.EN
                radcmbbxAction.Items(1).Text = "All"
                radcmbbxAction.Items(1).Text = "Add"
                radcmbbxAction.Items(2).Text = "Edit"
                radcmbbxAction.Items(3).Text = "Delete"
            End If
        ElseIf RadComboExtraReports.SelectedValue = EventProjectGroups Then
            MultiView1.SetActiveView(vwEventProjectGroups)
        ElseIf RadComboExtraReports.SelectedValue = EventEmployees Then
            MultiView1.SetActiveView(vwEventProjectEmployees)
        ElseIf RadComboExtraReports.SelectedValue = EmployeeEventShifts Then
            MultiView1.SetActiveView(vwEmployeeEventsShifts)
        ElseIf RadComboExtraReports.SelectedValue = OrganizationHierarchy Then
            MultiView1.SetActiveView(vwOrganization)
            objOrgCompany = New OrgCompany
            Dim dt As DataTable
            With objOrgCompany
                dt = .GetAll
                If dt.Rows.Count = 1 Then
                    lblCompany.Visible = False
                    radcmbCompany.Visible = False
                Else
                    FillCompany()
                End If
            End With
        ElseIf RadComboExtraReports.SelectedValue = OrgEntityManagers Then
            MultiView1.SetActiveView(vwEntityManager)
        ElseIf RadComboExtraReports.SelectedValue = ATSReader_Emp_Transaction Then
            MultiView1.SetActiveView(vwATSNotAttend)
        ElseIf RadComboExtraReports.SelectedValue = EmployeeWorkStatus_Days Then
            ddlMonth_Bind()
            ddlYear_Bind()
            rblEmployeeWorkStatus.Items.RemoveAt(0)
            rblEmployeeWorkStatus.Items.RemoveAt(0)
            MultiView1.SetActiveView(vwEmployeeWorkStatus_Days)
        ElseIf RadComboExtraReports.SelectedValue = Integration_ErrorLogs Then

            dtpIntegrationFromDate.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd2 As New Date
            dd2 = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            dtpIntegrationToDate.SelectedDate = dd2

            MultiView1.SetActiveView(vwIntegrationErrors)
        ElseIf RadComboExtraReports.SelectedValue = IntegrationMissingLeaves Then
            dtpIntegrationMissingLeavesFromDate.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd3 As New Date
            dd3 = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            dtpIntegrationMissingLeavesToDate.SelectedDate = dd3

            MultiView1.SetActiveView(vwIntegrationMissingLeaves)
        End If


    End Sub

    Protected Sub btnPrintEntityManager_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrintEntityManager.Click
        Dim reportName As String = RadComboExtraReports.SelectedValue
        BindReport(reportName)
    End Sub

    Protected Sub btnPrintATSNotAttend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrintATSNotAttend.Click
        Dim reportName As String = RadComboExtraReports.SelectedValue
        BindReport(reportName)
    End Sub

    Protected Sub btnPrintEmployeeWorkStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrintEmployeeWorkStatus.Click
        Dim reportName As String = RadComboExtraReports.SelectedValue
        BindReport(reportName)
    End Sub

    Protected Sub btnPrintIntegrationErrors_Click(sender As Object, e As EventArgs) Handles btnPrintIntegrationErrors.Click
        Dim reportName As String = RadComboExtraReports.SelectedValue
        BindReport(reportName)
    End Sub

    Protected Sub btnIntegrationMissingLeavesPrint_Click(sender As Object, e As EventArgs) Handles btnIntegrationMissingLeavesPrint.Click
        Dim reportName As String = RadComboExtraReports.SelectedValue
        BindReport(reportName)
    End Sub

#End Region

#Region "Methods"
#Region "EventLogReport"

    Private Sub FillForms()
        objSys_Forms = New Sys_Forms
        CtlCommon.FillTelerikDropDownList(radcmbbxForm, objSys_Forms.GetAll_ForEventLog, Lang)
    End Sub
#End Region
#Region "EventProjectGroups"

    Private Sub FillEvents()
        objEvents = New Events
        Dim dt As DataTable
        With objEvents
            dt = .GetAll
            CtlCommon.FillTelerikDropDownList(RadCmbBxEvent, dt, Lang)
        End With
    End Sub

    Private Sub FillEventGroups()
        objEvents_Employees = New Events_Employees
        Dim dt As DataTable
        With objEvents_Employees
            If Not RadCmbBxEvent.SelectedValue = -1 Then
                .FK_EventId = RadCmbBxEvent.SelectedValue
                dt = .Get_EventGroups
                CtlCommon.FillTelerikDropDownList(RadCmbBxGroup, dt, Lang)
            End If
        End With
    End Sub
#End Region
#Region "Event Employees"
    Private Sub FillEmployee()
        objEmployee = New Employee
        Dim dt As DataTable
        With objEmployee
            .FK_LogicalGroup = RadCmbBxGroup.SelectedValue
            .FK_EntityId = -1
            If Not RadCmbBxGroup.SelectedValue = -1 Then
                dt = .GetByLogicalGroup()
                CtlCommon.FillTelerikDropDownList(RadCmbBxEmployee, dt, Lang)
            End If
        End With
    End Sub
#End Region
#Region "Organization"
    Private Sub FillCompany()
        objOrgCompany = New OrgCompany
        With objOrgCompany
            CtlCommon.FillTelerikDropDownList(radcmbCompany, .GetAll, Lang)
        End With
    End Sub
    Protected Sub btnPrintOrg_Click(sender As Object, e As System.EventArgs) Handles btnPrintOrg.Click
        Dim CompanyId As Integer
        If radcmbCompany.SelectedValue = "" Then
            CompanyId = 0
        Else
            CompanyId = radcmbCompany.SelectedValue
        End If

        Context.Response.Write("<script> language='javascript'>window.open('OrgChart.aspx?CompanyId=" & CompanyId & "','_newtab');</script>")

        'Response.Redirect("OrgChart.aspx?CompanyId=" & CompanyId)
    End Sub
#End Region

    Public Shared Function GetPageDirection() As String
        If SessionVariables.CultureInfo = "ar-JO" Then
            Return "rtl"
        Else
            Return "ltr"
        End If
    End Function

    Public Sub BindReport(ByVal reportName As String)
        If Not RadComboExtraReports.SelectedValue = "-1" Then
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
            cryRpt.Subreports(cryRpt.Subreports().Item(0).Name).SetDataSource(dt2)
            cryRpt.SetParameterValue("@ReportName", RadComboExtraReports.SelectedItem.Text)
            cryRpt.SetParameterValue("@ReportName", RadComboExtraReports.SelectedItem.Text, "rptHeader")
            cryRpt.SetParameterValue("@FromDate", DateToString(RadDatePicker1.SelectedDate))
            cryRpt.SetParameterValue("@ToDate", DateToString(RadDatePicker2.SelectedDate))
            'Version = System.Configuration.ConfigurationManager.AppSettings("Version").ToString()
            Version = SmartV.Version.version.GetVersionNumber
            cryRpt.SetParameterValue("@Version", Version)
            cryRpt.SetParameterValue("@UserName", UserName)

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
                    If reportName = EventLogReport Then
                        If rblFormat.SelectedValue = 1 Then
                            cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedReport")
                        ElseIf rblFormat.SelectedValue = 2 Then
                            cryRpt.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, True, "ExportedReport")
                        ElseIf rblFormat.SelectedValue = 3 Then
                            FillExcelreport(reportName)
                            ExportDataSetToExcel(NDT, "ExportedReport", HeadDT)
                        End If
                    ElseIf reportName = EventProjectGroups Then
                        If rblLogicalGroup.SelectedValue = 1 Then
                            cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedReport")
                        ElseIf rblLogicalGroup.SelectedValue = 2 Then
                            cryRpt.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, True, "ExportedReport")
                        ElseIf rblLogicalGroup.SelectedValue = 3 Then
                            FillExcelreport(reportName)
                            ExportDataSetToExcel(NDT, "ExportedReport", HeadDT)
                        End If
                    ElseIf reportName = EventEmployees Then
                        If rblEmployee.SelectedValue = 1 Then
                            cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedReport")
                        ElseIf rblEmployee.SelectedValue = 2 Then
                            cryRpt.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, True, "ExportedReport")
                        ElseIf rblEmployee.SelectedValue = 3 Then
                            FillExcelreport(reportName)
                            ExportDataSetToExcel(NDT, "ExportedReport", HeadDT)
                        End If
                    ElseIf reportName = EmployeeEventShifts Then
                        If rblShifts.SelectedValue = 1 Then
                            cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedReport")
                        ElseIf rblShifts.SelectedValue = 2 Then
                            cryRpt.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, True, "ExportedReport")
                        ElseIf rblShifts.SelectedValue = 3 Then
                            FillExcelreport(reportName)
                            ExportDataSetToExcel(NDT, "ExportedReport", HeadDT)
                        End If
                    ElseIf reportName = OrgEntityManagers Then
                        If rblFormat2.SelectedValue = 1 Then
                            cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedReport")
                        ElseIf rblFormat2.SelectedValue = 2 Then
                            cryRpt.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, True, "ExportedReport")
                        ElseIf rblFormat2.SelectedValue = 3 Then
                            FillExcelreport(reportName)
                            ExportDataSetToExcel(NDT, "ExportedReport", HeadDT)
                        End If
                    ElseIf reportName = Integration_ErrorLogs Then
                        If rblIntegrationErrors.SelectedValue = 1 Then
                            cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedReport")
                        ElseIf rblIntegrationErrors.SelectedValue = 2 Then
                            cryRpt.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, True, "ExportedReport")
                        ElseIf rblIntegrationErrors.SelectedValue = 3 Then
                            FillExcelreport(reportName)
                            ExportDataSetToExcel(NDT, "ExportedReport", HeadDT)
                        End If

                    ElseIf reportName = IntegrationMissingLeaves Then
                        If rblIntegrationMissingLeaves.SelectedValue = 1 Then
                            cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedReport")
                        ElseIf rblIntegrationMissingLeaves.SelectedValue = 2 Then
                            cryRpt.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, True, "ExportedReport")
                        ElseIf rblIntegrationMissingLeaves.SelectedValue = 3 Then
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

        If Not RadDatePicker1.SelectedDate Is Nothing Then
            rptObj.FROM_DATE = RadDatePicker1.SelectedDate
        End If

        If Not RadDatePicker2.SelectedDate Is Nothing Then
            rptObj.TO_DATE = RadDatePicker2.SelectedDate
        End If

        If Not radcmbbxForm.SelectedValue Is Nothing Then
            If Not radcmbbxForm.SelectedValue = "-1" Then
                rptObj.EventForm = radcmbbxForm.Text
                rptObj.EventForm = radcmbbxForm.SelectedValue


            Else

                rptObj.EventForm = 0
            End If
        End If
        If Not radcmbbxAction.SelectedValue Is Nothing Then
            If Not radcmbbxAction.SelectedValue = "0" Then
                rptObj.ActionType = radcmbbxAction.SelectedValue

            Else
                rptObj.ActionType = 0
            End If

            If RadCmbBxEvent.SelectedValue Is Nothing Then
                If RadCmbBxEvent.SelectedValue <> -1 Then
                    rptObj.EventId = RadCmbBxEvent.SelectedValue

                Else
                    rptObj.EventId = Nothing
                End If
            End If
        End If
        If Not RadCmbBxGroup.SelectedValue = Nothing Then
            If Not RadCmbBxGroup.SelectedValue = -1 Then
                rptObj.GroupId = RadCmbBxGroup.SelectedValue
            Else
                rptObj.GroupId = Nothing
            End If
        End If
        If Not RadCmbBxEmployee.SelectedValue = Nothing Then
            If RadCmbBxEmployee.SelectedValue <> -1 Then
                rptObj.EmployeeId = RadCmbBxEmployee.SelectedValue
            Else
                rptObj.EmployeeId = Nothing
            End If
        End If

        If RadComboExtraReports.SelectedValue = OrgEntityManagers Then
            If EmployeeFilter2.EmployeeId <> 0 Then
                rptObj.EmployeeId = EmployeeFilter2.EmployeeId
            Else
                rptObj.EmployeeId = Nothing
            End If

            If Not EmployeeFilter2.CompanyId = 0 Then
                rptObj.CompanyId = EmployeeFilter2.CompanyId
            Else
                rptObj.CompanyId = Nothing
            End If

            If EmployeeFilter2.FilterType = "C" Then
                If Not EmployeeFilter2.EntityId = 0 Then
                    rptObj.EntityId = EmployeeFilter2.EntityId
                Else
                    rptObj.EntityId = Nothing
                End If
            ElseIf EmployeeFilter2.FilterType = "W" Then
                If Not EmployeeFilter2.EntityId = 0 Then
                    rptObj.WorkLocationId = EmployeeFilter2.EntityId
                Else
                    rptObj.WorkLocationId = Nothing
                End If
            ElseIf EmployeeFilter2.FilterType = "L" Then
                If Not EmployeeFilter2.EntityId = 0 Then
                    rptObj.LogicalGroupId = EmployeeFilter2.EntityId
                Else
                    rptObj.LogicalGroupId = Nothing
                End If
            End If
        End If

        If RadComboExtraReports.SelectedValue = ATSReader_Emp_Transaction Then
            If EmployeeFilter3.EmployeeId <> 0 Then
                rptObj.EmployeeId = EmployeeFilter3.EmployeeId
            Else
                rptObj.EmployeeId = Nothing
            End If
            If Not EmployeeFilter3.CompanyId = 0 Then
                rptObj.CompanyId = EmployeeFilter3.CompanyId
            Else
                rptObj.CompanyId = Nothing
            End If
            If EmployeeFilter3.FilterType = "C" Then
                If Not EmployeeFilter3.EntityId = 0 Then
                    rptObj.EntityId = EmployeeFilter3.EntityId
                Else
                    rptObj.EntityId = Nothing
                End If
            ElseIf EmployeeFilter3.FilterType = "W" Then
                If Not EmployeeFilter3.EntityId = 0 Then
                    rptObj.WorkLocationId = EmployeeFilter3.EntityId
                Else
                    rptObj.WorkLocationId = Nothing
                End If
            ElseIf EmployeeFilter3.FilterType = "L" Then
                If Not EmployeeFilter3.EntityId = 0 Then
                    rptObj.LogicalGroupId = EmployeeFilter3.EntityId
                Else
                    rptObj.LogicalGroupId = Nothing
                End If
            End If
        End If
        If RadComboExtraReports.SelectedValue = EmployeeWorkStatus_Days Then
            If EmployeeFilter4.EmployeeId <> 0 Then
                rptObj.EmployeeId = EmployeeFilter4.EmployeeId
            Else
                rptObj.EmployeeId = Nothing
            End If
            If Not EmployeeFilter4.CompanyId = 0 Then
                rptObj.CompanyId = EmployeeFilter4.CompanyId
            Else
                rptObj.CompanyId = Nothing
            End If
            If EmployeeFilter4.FilterType = "C" Then
                If Not EmployeeFilter4.EntityId = 0 Then
                    rptObj.EntityId = EmployeeFilter4.EntityId
                Else
                    rptObj.EntityId = Nothing
                End If
            ElseIf EmployeeFilter4.FilterType = "W" Then
                If Not EmployeeFilter4.EntityId = 0 Then
                    rptObj.WorkLocationId = EmployeeFilter4.EntityId
                Else
                    rptObj.WorkLocationId = Nothing
                End If
            ElseIf EmployeeFilter4.FilterType = "L" Then
                If Not EmployeeFilter4.EntityId = 0 Then
                    rptObj.LogicalGroupId = EmployeeFilter4.EntityId
                Else
                    rptObj.LogicalGroupId = Nothing
                End If
            End If
            rptObj.Year = ddlYear.SelectedValue
            rptObj.Month = ddlMonth.SelectedValue
        End If

        If RadComboExtraReports.SelectedValue = Integration_ErrorLogs Then

            If Not dtpIntegrationFromDate.SelectedDate Is Nothing Then
                rptObj.FROM_DATE = dtpIntegrationFromDate.SelectedDate
            End If

            If Not dtpIntegrationToDate.SelectedDate Is Nothing Then
                rptObj.TO_DATE = dtpIntegrationToDate.SelectedDate
            End If

        End If

        If RadComboExtraReports.SelectedValue = IntegrationMissingLeaves Then
            If Not dtpIntegrationMissingLeavesFromDate.SelectedDate Is Nothing Then
                rptObj.FROM_DATE = dtpIntegrationMissingLeavesFromDate.SelectedDate
            End If

            If Not dtpIntegrationMissingLeavesToDate.SelectedDate Is Nothing Then
                rptObj.TO_DATE = dtpIntegrationMissingLeavesToDate.SelectedDate
            End If
        End If

        If rpttid = EventLogReport Then
            If (radcmbbxForm.SelectedValue = "Employee" Or radcmbbxForm.SelectedValue = "Employee Leaves" Or radcmbbxForm.SelectedValue = "Employee Permission" Or radcmbbxForm.SelectedValue = "Employee Leave Balance" Or radcmbbxForm.SelectedValue = "Employee Nursing Permission Request" Or radcmbbxForm.SelectedValue = "-1") Then
                RPTName = "rptApp_EventLog_detailed.rpt"
                DT = rptObj.GetApp_Event()

            Else
                RPTName = "rptApp_EventLog.rpt"
                DT = rptObj.GetApp_Event()
            End If

        ElseIf rpttid = EventProjectGroups Then
            RPTName = "rptEvent_Group.rpt"
            DT = rptObj.GetEvent_Groups()
        ElseIf rpttid = EventEmployees Then
            RPTName = "rptEvent_Employees.rpt"
            DT = rptObj.GetEvent_Employees()
        ElseIf rpttid = EmployeeEventShifts Then
            RPTName = "rptEmployee_EventShifts.rpt"
            DT = rptObj.GetEmployeeEvent_Shifts()
        ElseIf rpttid = OrgEntityManagers Then
            RPTName = "rptEntityManagers.rpt"
            FormatEntityManager_DataTable(rptObj.Get_EntityManagers(), "FK_ParentId", "EntityId", "EntityName", "EntityArabicName")
            DT = dtCustomizedRecordsOrder
        ElseIf rpttid = ATSReader_Emp_Transaction Then
            RPTName = "rpt_ATSReader_Emp_Transactios.rpt"
            DT = rptObj.Get_ATSReader_Emp_Transaction
        ElseIf rpttid = EmployeeWorkStatus_Days Then
            RPTName = "rpt_ATSReader_Emp_Transactios.rpt"
            DT = rptObj.Get_EmployeeMonthlySheet
            FillExcelreport(RadComboExtraReports.SelectedValue)
            If Not NDT Is Nothing Then
                If NDT.Rows.Count > 0 Then
                    ExportDataSetToExcel(NDT, "ExportedReport")
                    Exit Sub
                End If
            End If
        ElseIf rpttid = Integration_ErrorLogs Then
            RPTName = "rpt_IntegrationErrorLogs.rpt"
            DT = rptObj.Get_IntegrationErrorLogs
        ElseIf rpttid = IntegrationMissingLeaves Then
            RPTName = "rpt_IntegrationMissingLeaves.rpt"
            DT = rptObj.Get_IntegrationMissingLeaves
        End If


        UserName = SessionVariables.LoginUser.UsrID
        ' Version = System.Configuration.ConfigurationManager.AppSettings("Version").ToString()

        If (Lang = CtlCommon.Lang.AR) Then
            RPTName = "Arabic/" + RPTName
        Else
            RPTName = "English/" + RPTName
        End If
    End Sub

    Private Sub FillExcelreport(ByVal reportname As String)
        objsysforms = New SYSForms
        NDT = New DataTable
        HeadDT = New DataTable

        Dim UK_Culture As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("en-GB")

        If Not DT Is Nothing Then
            If DT.Rows.Count > 0 Then
                NDT = DT.Clone()

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
                If Not RadComboExtraReports.SelectedValue = EventLogReport And Not RadComboExtraReports.SelectedValue = EmployeeWorkStatus_Days Then
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
                    If RadComboExtraReports.SelectedValue = EventLogReport Or RadComboExtraReports.SelectedValue = EventEmployees Then
                        If RadComboExtraReports.SelectedValue = EventLogReport Then
                            HeadDT(3)(1) = "من تاريخ: " & Convert.ToDateTime(RadDatePicker1.SelectedDate).ToShortDateString
                        ElseIf RadComboExtraReports.SelectedValue = EventEmployees Then
                            HeadDT(3)(1) = "من تاريخ: " & Convert.ToDateTime(RadDatePicker3.SelectedDate).ToShortDateString
                        End If
                    End If
                    '------Column(1)-----------
                    HeadDT(1)(0) = SessionVariables.LoginUser.UsrID
                    HeadDT(2)(0) = Date.Today.ToShortDateString
                    If RadComboExtraReports.SelectedValue = EventLogReport Or RadComboExtraReports.SelectedValue = EventEmployees Then
                        If RadComboExtraReports.SelectedValue = EventLogReport Then
                            HeadDT(3)(0) = "الى تاريخ: " & Convert.ToDateTime(RadDatePicker2.SelectedDate).ToShortDateString
                        ElseIf RadComboExtraReports.SelectedValue = EventEmployees Then
                            HeadDT(3)(0) = "الى تاريخ: " & Convert.ToDateTime(RadDatePicker4.SelectedDate).ToShortDateString
                        End If

                    End If
                Else
                    HeadDT(0)(0) = dtFormName(0)("Desc_En").ToString
                    '------Column(0)-----------
                    HeadDT(1)(0) = "Printed By:"
                    HeadDT(2)(0) = "Printing Date:"
                    If RadComboExtraReports.SelectedValue = EventLogReport Or RadComboExtraReports.SelectedValue = EventEmployees Then
                        If RadComboExtraReports.SelectedValue = EventLogReport Then
                            HeadDT(3)(0) = "From Date: " & Convert.ToDateTime(RadDatePicker1.SelectedDate).ToShortDateString
                        ElseIf RadComboExtraReports.SelectedValue = EventEmployees Then
                            HeadDT(3)(0) = "From Date: " & Convert.ToDateTime(RadDatePicker3.SelectedDate).ToShortDateString
                        End If

                    End If
                    '------Column(1)-----------
                    HeadDT(1)(1) = SessionVariables.LoginUser.UsrID
                    HeadDT(2)(1) = Date.Today.ToShortDateString
                    If RadComboExtraReports.SelectedValue = EventLogReport Or RadComboExtraReports.SelectedValue = EventEmployees Then
                        If RadComboExtraReports.SelectedValue = EventLogReport Then
                            HeadDT(3)(1) = "To Date: " & Convert.ToDateTime(RadDatePicker2.SelectedDate).ToShortDateString
                        ElseIf RadComboExtraReports.SelectedValue = EventEmployees Then
                            HeadDT(3)(1) = "To Date: " & Convert.ToDateTime(RadDatePicker4.SelectedDate).ToShortDateString
                        End If
                    End If
                End If
                HeadDT.Rows.Add(drFormName)
                '------------------ADD REPORT HEADER-----------------

                Select Case reportname
                    Case EventLogReport

                        If (Lang = CtlCommon.Lang.AR) Then
                            NDT.Columns(11).DataType = System.Type.GetType("System.String")
                            For Each row As DataRow In DT.Rows
                                Dim dr As DataRow = NDT.NewRow
                                For i As Integer = 0 To NDT.Columns.Count - 1
                                    dr(i) = row(i)
                                    If i = 11 Then
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
                            NDT.Columns(7).ColumnName = "تاريخ الحدث"
                            NDT.Columns(8).ColumnName = "اسم الشاشة"

                            NDT.Columns.RemoveAt(9)
                            NDT.Columns(9).ColumnName = "اسم السجل"
                            'NDT.Columns.RemoveAt(11)
                            NDT.Columns(10).ColumnName = "وصف الحدث"
                            NDT.Columns(11).ColumnName = "اسم المستخدم"

                        Else
                            NDT.Columns(11).DataType = System.Type.GetType("System.String")
                            For Each row As DataRow In DT.Rows
                                Dim dr As DataRow = NDT.NewRow
                                For i As Integer = 0 To NDT.Columns.Count - 1
                                    dr(i) = row(i)
                                    If i = 11 Then
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
                            NDT.Columns(7).ColumnName = "Event Date"
                            NDT.Columns(8).ColumnName = "Form Name"

                            NDT.Columns.RemoveAt(9)
                            NDT.Columns(9).ColumnName = "Record Name"
                            'NDT.Columns.RemoveAt(11)
                            NDT.Columns(10).ColumnName = "Description"
                            NDT.Columns(11).ColumnName = "User Name"

                        End If
                    Case EventProjectGroups
                        If (Lang = CtlCommon.Lang.AR) Then
                            NDT.Columns(0).ColumnName = "اسم مجموعة العمل"
                            NDT.Columns(1).ColumnName = "اسم مجموعة العمل باللغة العربية"
                            NDT.Columns(2).ColumnName = "عدد الموظفين"
                            NDT.Columns(3).ColumnName = "اسم الحدث\المشروع"
                            NDT.Columns.RemoveAt(4)
                            NDT.Columns.RemoveAt(4)
                        Else
                            NDT.Columns(0).ColumnName = "Group Name"
                            NDT.Columns(1).ColumnName = "Group Arabic Name"
                            NDT.Columns(2).ColumnName = "Number Of Employees"
                            NDT.Columns(3).ColumnName = "Event\Project Name"
                            NDT.Columns.RemoveAt(4)
                            NDT.Columns.RemoveAt(4)
                        End If

                    Case EventEmployees
                        If (Lang = CtlCommon.Lang.AR) Then
                            NDT.Columns.RemoveAt(0)
                            NDT.Columns(0).ColumnName = "رقم الموظف"
                            NDT.Columns(1).ColumnName = "اسم الموظف"
                            NDT.Columns(2).ColumnName = "اسم الموظف باللغة العربية"
                            NDT.Columns.RemoveAt(3)
                            NDT.Columns(3).ColumnName = "اسم مجموعة العمل"
                            NDT.Columns(4).ColumnName = "اسم مجموعة العمل باللغة العربية"
                            NDT.Columns.RemoveAt(5)
                            NDT.Columns(5).ColumnName = "اسم الحدث\المشروع"
                        Else
                            NDT.Columns.RemoveAt(0)
                            NDT.Columns(0).ColumnName = "Employee No."
                            NDT.Columns(1).ColumnName = "Employee Name"
                            NDT.Columns(2).ColumnName = "Employee Arabic Name"
                            NDT.Columns.RemoveAt(3)
                            NDT.Columns(3).ColumnName = "Group Name"
                            NDT.Columns(4).ColumnName = "Group Arabic Name"
                            NDT.Columns.RemoveAt(5)
                            NDT.Columns(5).ColumnName = "Event\Project Name"
                        End If
                    Case EmployeeEventShifts
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
                        If (Lang = CtlCommon.Lang.AR) Then
                            NDT.Columns.RemoveAt(0)
                            NDT.Columns(0).ColumnName = "رقم الموظف"
                            NDT.Columns(1).ColumnName = "اسم الموظف"
                            NDT.Columns(2).ColumnName = "اسم الموظف باللغة العربية"
                            NDT.Columns.RemoveAt(3)
                            NDT.Columns(3).ColumnName = "اسم مجموعة العمل"
                            NDT.Columns(4).ColumnName = "اسم مجموعة العمل باللغة العربية"
                            NDT.Columns.RemoveAt(5)
                            NDT.Columns(5).ColumnName = "اسم الحدث\المشروع"
                            NDT.Columns(6).ColumnName = "اسم المناوبة"
                            NDT.Columns(7).ColumnName = "تاريخ الجدول"
                        Else
                            NDT.Columns.RemoveAt(0)
                            NDT.Columns(0).ColumnName = "Employee No"
                            NDT.Columns(1).ColumnName = "Employee Name"
                            NDT.Columns(2).ColumnName = "Employee Arabic Name"
                            NDT.Columns.RemoveAt(3)
                            NDT.Columns(3).ColumnName = "Group Name"
                            NDT.Columns(4).ColumnName = "Group Arabic Name"
                            NDT.Columns.RemoveAt(5)
                            NDT.Columns(5).ColumnName = "Event\Project Name"
                            NDT.Columns(6).ColumnName = "Shift Name"
                            NDT.Columns(7).ColumnName = "Schedule Date"
                        End If
                    Case OrgEntityManagers
                        NDT = DT
                        If (Lang = CtlCommon.Lang.AR) Then
                            NDT.Columns.RemoveAt(0)
                            NDT.Columns(0).ColumnName = "وحدة العمل"
                            NDT.Columns(1).ColumnName = "رقم المدير"
                            NDT.Columns.RemoveAt(2)
                            NDT.Columns(2).ColumnName = "اسم المدير"
                            NDT.Columns.RemoveAt(3)
                            NDT.Columns(3).ColumnName = "اسم الشركة"
                            NDT.Columns.RemoveAt(4)
                            NDT.Columns.RemoveAt(4)
                            NDT.Columns.RemoveAt(4)
                            NDT.Columns.RemoveAt(4)

                        Else
                            NDT.Columns(0).ColumnName = "Entity Name"
                            NDT.Columns.RemoveAt(1)
                            NDT.Columns(1).ColumnName = "Manager No"
                            NDT.Columns(2).ColumnName = "Manager Name"
                            NDT.Columns.RemoveAt(3)
                            NDT.Columns(3).ColumnName = "Company Name"
                            NDT.Columns.RemoveAt(4)
                            NDT.Columns.RemoveAt(4)
                            NDT.Columns.RemoveAt(4)
                            NDT.Columns.RemoveAt(4)
                            NDT.Columns.RemoveAt(4)
                        End If

                    Case ATSReader_Emp_Transaction
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
                        If (Lang = CtlCommon.Lang.AR) Then
                            NDT.Columns.RemoveAt(0)
                            NDT.Columns(0).ColumnName = "رقم الموظف"
                            NDT.Columns(1).ColumnName = "رقم بطاقة الموظف"
                            NDT.Columns.RemoveAt(2)
                            NDT.Columns(2).ColumnName = "اسم الموظف"
                            NDT.Columns.RemoveAt(3)
                            NDT.Columns.RemoveAt(3)
                            NDT.Columns(3).ColumnName = "نوع الاجازة"
                            NDT.Columns.RemoveAt(4)
                            NDT.Columns.RemoveAt(4)
                            NDT.Columns(4).ColumnName = "نوع المغادرة"
                            NDT.Columns.RemoveAt(5)
                            NDT.Columns(5).ColumnName = "الملاحظات"
                            NDT.Columns.RemoveAt(6)
                            NDT.Columns.RemoveAt(6)
                            NDT.Columns(6).ColumnName = "وحدة العمل"
                            NDT.Columns.RemoveAt(7)
                            NDT.Columns.RemoveAt(7)
                            NDT.Columns(7).ColumnName = "الشركة"
                            NDT.Columns(8).ColumnName = "التاريخ"
                        Else
                            NDT.Columns.RemoveAt(0)
                            NDT.Columns(0).ColumnName = "Employee No."
                            NDT.Columns(1).ColumnName = "Employee Card No."
                            NDT.Columns(2).ColumnName = "Employee Name"
                            NDT.Columns.RemoveAt(3)
                            NDT.Columns.RemoveAt(3)
                            NDT.Columns(3).ColumnName = "Leave Type"
                            NDT.Columns.RemoveAt(4)
                            NDT.Columns.RemoveAt(4)
                            NDT.Columns(4).ColumnName = "Permission Type"
                            NDT.Columns.RemoveAt(5)
                            NDT.Columns(5).ColumnName = "Remarks"
                            NDT.Columns.RemoveAt(6)
                            NDT.Columns.RemoveAt(6)
                            NDT.Columns(6).ColumnName = "Entity Name"
                            NDT.Columns.RemoveAt(7)
                            NDT.Columns.RemoveAt(7)
                            NDT.Columns(7).ColumnName = "Company Name"
                            NDT.Columns(8).ColumnName = "Date"

                        End If
                    Case EmployeeWorkStatus_Days

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
                                End If
                                If i = 6 Then
                                    If Not dr(i) Is DBNull.Value Then
                                        dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                    End If
                                End If
                            Next
                            NDT.Rows.Add(dr)
                        Next
                        If (Lang = CtlCommon.Lang.AR) Then
                            NDT.Columns(0).ColumnName = "# TG Id"
                            NDT.Columns.RemoveAt(1)
                            NDT.Columns(1).ColumnName = "اسم الموظف"
                            NDT.Columns.RemoveAt(2)
                            NDT.Columns(2).ColumnName = "اسم وحدة العمل"
                            NDT.Columns(3).ColumnName = "تاريخ البداية"
                            NDT.Columns(4).ColumnName = "تاريخ الانتهاء"
                        Else
                            NDT.Columns(0).ColumnName = "# TG Id"
                            NDT.Columns(1).ColumnName = "Employee Name"
                            NDT.Columns.RemoveAt(2)
                            NDT.Columns(2).ColumnName = "Entity Name"
                            NDT.Columns.RemoveAt(3)
                            NDT.Columns(3).ColumnName = "Start Date"
                            NDT.Columns(4).ColumnName = "End Date"

                        End If

                    Case Integration_ErrorLogs
                        NDT = DT
                        If Lang = CtlCommon.Lang.AR Then
                            NDT.Columns(0).ColumnName = "التسلسل"
                            NDT.Columns(1).ColumnName = "رمز السجل"
                            NDT.Columns(2).ColumnName = "اسم الوحدة"
                            NDT.Columns(3).ColumnName = "التفاصيل"
                            NDT.Columns(4).ColumnName = "التاريخ"
                        Else
                            NDT.Columns(0).ColumnName = "Serial No."
                            NDT.Columns(1).ColumnName = "Record Id"
                            NDT.Columns(2).ColumnName = "Module"
                            NDT.Columns(3).ColumnName = "Description"
                            NDT.Columns(4).ColumnName = "Date"
                        End If

                    Case IntegrationMissingLeaves
                        NDT = DT
                        If Lang = CtlCommon.Lang.AR Then
                            NDT.Columns(0).ColumnName = "رقم الموظف"
                            NDT.Columns(1).ColumnName = "من تاريخ"
                            NDT.Columns(2).ColumnName = "الى تاريخ"
                            NDT.Columns(3).ColumnName = "رمز السجل"
                            NDT.Columns(4).ColumnName = "رمز نوع الاجازة"
                        Else
                            NDT.Columns(0).ColumnName = "Emp No."
                            NDT.Columns(1).ColumnName = "From Date"
                            NDT.Columns(2).ColumnName = "To Date"
                            NDT.Columns(3).ColumnName = "Record Id"
                            NDT.Columns(4).ColumnName = "Leave Type Id"
                        End If

                End Select
            Else
                CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NoData", CultureInfo), "info")
            End If
        End If
    End Sub

    Private Sub FillReports(ByVal formID As Integer, ByVal groupID As Integer)

        CtlCommon.FillTelerikDropDownList(RadComboExtraReports, objsysforms.GetFormsByParentID(formID, groupID), Lang)

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("../Reports/Others_Reports.aspx")
    End Sub


#Region "Format Entity Manager Datatable"

    Private Sub FormatEntityManager_DataTable(ByVal dtDataSource As DataTable, ByVal ParentField As String, ByVal valField As String, ByVal EngNameTextField As String, ByVal ArNameTextField As String)


        If dtDataSource IsNot Nothing Then
            Try
                Me.dtDataSource = dtDataSource
                Me.valField = valField
                Me.EngNameTextField = EngNameTextField
                Me.ArNameTextField = ArNameTextField
                Me.ParentField = ParentField
                dtCustomizedRecordsOrder = dtDataSource.Clone
                'dtCustomizedRecordsOrder.Columns.Add(New DataColumn(valField))
                'dtCustomizedRecordsOrder.Columns.Add(New DataColumn(EngNameTextField))
                'dtCustomizedRecordsOrder.Columns.Add(New DataColumn(ArNameTextField))
                'dtCustomizedRecordsOrder.Columns.Add(New DataColumn(ParentField))
                Dim drChilds As DataRow() = Nothing
                Dim drRoots() As DataRow = dtDataSource.Select(ParentField + " is null")
                addNode(drRoots, drChilds, (drRoots.Count - 1), 0)

            Catch ex As Exception
                ' You can warn the developer to send the correct parameters .For example , 
                ' check the fields names and/or the correct data source , to avoid the 
                ' pain of the exception

            End Try
        Else
            ' Do nothing , you can show warning message
        End If
    End Sub

    Private Sub addNode(ByVal drRoot() As DataRow, _
                            ByVal foundChilds As DataRow(), _
                            ByVal count As Integer, _
                            ByVal levelNo As Integer)
        If count >= 0 Then
            If levelNo = 0 Then
                ' Get the current root 
                Dim dr As DataRow = drRoot(count)
                Dim EntityName As String = dr("EntityName").ToString
                Dim EntityArabicName As String = dr("EntityArabicName").ToString
                Dim EmployeeNo As String = dr("EmployeeNo").ToString
                Dim EmployeeName As String = dr("EmployeeName").ToString
                Dim EmployeeArabicName As String = dr("EmployeeArabicName").ToString
                Dim CompanyName As String = dr("CompanyName").ToString
                Dim CompanyArabicName As String = dr("CompanyArabicName").ToString
                Dim id As Integer = dr("EntityId").ToString
                Dim EmployeeId As String = dr("EmployeeId").ToString
                Dim CompanyId As String = dr("CompanyId").ToString
                Dim ParentId As String = dr("FK_ParentId").ToString
                dtCustomizedRecordsOrder.Rows.Add(EntityName, EntityArabicName, EmployeeNo, EmployeeName, EmployeeArabicName, CompanyName, CompanyArabicName, _
                                                       id, IIf(EmployeeId = "", DBNull.Value, EmployeeId), CompanyId, IIf(ParentId = "", DBNull.Value, ParentId))
                ' Check if the root has childs , if exist get ordered 
                ' by Sequence
                foundChilds = dtDataSource.Select(ParentField + "=" & dr(valField))
                If foundChilds.Count <> 0 Then
                    ' Increase the level to add node on higher 
                    ' level
                    levelNo = levelNo + 1
                    addNode(drRoot, foundChilds, count, levelNo)
                    ' Return to add at lower level
                    levelNo = levelNo - 1
                End If
                count = count - 1
                addNode(drRoot, _
                    foundChilds, _
                    count, _
                    levelNo)
            Else
                For Each row As DataRow In foundChilds
                    ' Prepare the Id and the name of the child 
                    Dim EntityName As String = row("EntityName").ToString
                    Dim EntityArabicName As String = row("EntityArabicName").ToString
                    Dim EmployeeNo As String = row("EmployeeNo").ToString
                    Dim EmployeeName As String = row("EmployeeName").ToString
                    Dim EmployeeArabicName As String = row("EmployeeArabicName").ToString
                    Dim CompanyName As String = row("CompanyName").ToString
                    Dim CompanyArabicName As String = row("CompanyArabicName").ToString
                    Dim id As Integer = row(valField)
                    Dim EmployeeId As String = row("EmployeeId").ToString
                    Dim CompanyId As String = row("CompanyId").ToString
                    Dim ParentId As String = row(ParentField)
                    ' Add the new child
                    dtCustomizedRecordsOrder.Rows.Add(EntityName, EntityArabicName, EmployeeNo, EmployeeName, EmployeeArabicName, CompanyName, CompanyArabicName, _
                                                       id, IIf(EmployeeId = "", DBNull.Value, EmployeeId), CompanyId, ParentId)

                    ' Check if the child have sub-childs , if exist will return
                    ' Order By Sequence number

                    Dim childs As DataRow() = _
                        dtDataSource.Select(ParentField & "=" & id)
                    If childs.Length <> 0 Then
                        levelNo = levelNo + 1
                        addNode(drRoot, childs, count, levelNo)
                        levelNo = levelNo - 1
                    End If
                Next
            End If

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

#End Region

    Private Sub ddlYear_Bind()
        Dim year As Integer = DateTime.Now.Year
        Dim dt As New DataTable
        dt.Columns.Add("val")
        dt.Columns.Add("txt")

        'Dim dr1 As DataRow = dt.NewRow()
        'dr1("val") = 0
        'dr1("txt") = "year"
        'dt.Rows.Add(dr1)

        For i As Integer = year - 5 To year + 5
            Dim dr As DataRow = dt.NewRow()
            dr("val") = i
            dr("txt") = i
            dt.Rows.Add(dr)
        Next

        ddlYear.SelectedValue = Now.Year
        ddlYear.DataSource = dt
        ddlYear.DataBind()
    End Sub

    Private Sub ddlMonth_Bind()
        Dim dt As New DataTable
        dt.Columns.Add("val")
        dt.Columns.Add("txt")

        For i As Integer = 1 To 12
            Dim dr As DataRow = dt.NewRow()
            dr("val") = i

            Dim strMonthName As String
            'strMonthName = CultureInfo.CurrentUICulture.DateTimeFormat.GetMonthName(i).ToString()
            If Lang = CtlCommon.Lang.AR Then
                Dim GetNames As New System.Globalization.CultureInfo("ar-EG")
                GetNames.DateTimeFormat.GetMonthName(1)
                GetNames.DateTimeFormat.GetDayName(1)
                strMonthName = GetNames.DateTimeFormat.GetMonthName(i).ToString()
            Else
                strMonthName = CultureInfo.CurrentUICulture.DateTimeFormat.GetMonthName(i).ToString()
            End If

            dr("txt") = strMonthName
            dt.Rows.Add(dr)
        Next
        ddlMonth.SelectedValue = Now.Month
        ddlMonth.DataSource = dt
        ddlMonth.DataBind()

    End Sub

#End Region

End Class
