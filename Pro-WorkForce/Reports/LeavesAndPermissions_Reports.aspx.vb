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
Imports TA.Definitions
Imports TA.LookUp

Partial Class Reports_SelfServices_LeavesAndPermissionsReports
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
    Private objRequestStatus As RequestStatus
    Private objSemesters As Semesters

    Private EmployeesLeavesReport As String = "Employees Leaves Report"
    Private EmployeesPermissionsReport As String = "Employees Permissions Report"
    Private LeavesBalanceReport As String = "Leaves Balance Report"
    Private LeavesBalanceReport_DOF As String = "Annual Leave Balance"
    Private ExpireLeavesBalanceReport As String = "Expire Leaves Balance Report"
    Private PermissionPerType As String = "Permission Per Type"
    Private NursingPermissionReport As String = "Nursing Permission Report"
    Private StudyPermissionReport As String = "Study Permission Report"
    Private DetailedPermissionRequests As String = "Detailed Permission Requests"
    Private PermissionPerStatus As String = "PermissionPerStatus"
    Private LeavesDeduction_HRA As String = "LeavesDeduction_HRA"
    Private PermissionRequest_AuditLog As String = "PermissionRequest_AuditLog"
    Private DetailedStudyPermissions As String = "DetailedStudyPermissions"
    Private LeavePerStatus As String = "LeavePerStatus"
    Private DelayPermissions As String = "DelayPermissions"
    Private ActualPermissions As String = "ActualPermissions"
    Private ActualOfficalPermissions As String = "ActualOfficalPermissions"
    Private LeaveRequest_AuditLog As String = "LeaveRequest_AuditLog"
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

    Public Property SubDT() As DataTable
        Get
            Return ViewState("SubDT")
        End Get
        Set(ByVal value As DataTable)
            ViewState("SubDT") = value
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
            lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقارير الإجازات والتصاريح", "Leaves and Permissions Reports")
            btnPrint.Text = IIf(Lang = CtlCommon.Lang.AR, "انشاء", "Generate")
            lblPermtype.Text = IIf(Lang = CtlCommon.Lang.AR, "نوع المغادرة", "Permission Type")

            FillReports(formID, SessionVariables.LoginUser.GroupId)
            'FillPerm_type()

            RadDatePicker1.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd As New Date
            dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)

            RadDatePicker2.SelectedDate = dd

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
        Dim reportName As String = RadComboLeavesandPermissionsReports.SelectedValue
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

    Protected Sub RadComboLeavesandPermissionsReports_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboLeavesandPermissionsReports.SelectedIndexChanged
        If RadComboLeavesandPermissionsReports.SelectedValue = PermissionPerType Then
            trPermissionPerType.Visible = True
            dvManualPerms.Visible = True
            trLeaveType.Visible = False
            dvManualLeaves.Visible = False
            dvPermissionStatus.Visible = False
            FillPerm_type()
            dvDeductionPolicy.Visible = False
            rfvDeductionPolicy.Enabled = False
            dvDetailedStudyPemission.Visible = False
            dvLeaveStatus.Visible = False
            dvDelayPermissions.Visible = False
        ElseIf RadComboLeavesandPermissionsReports.SelectedValue = EmployeesLeavesReport Then
            trLeaveType.Visible = True
            dvManualLeaves.Visible = True
            trPermissionPerType.Visible = False
            dvManualPerms.Visible = False
            dvPermissionStatus.Visible = False
            FillLeave_type()
            dvDeductionPolicy.Visible = False
            rfvDeductionPolicy.Enabled = False
            dvDetailedStudyPemission.Visible = False
            dvLeaveStatus.Visible = False
            dvDelayPermissions.Visible = False
        ElseIf RadComboLeavesandPermissionsReports.SelectedValue = PermissionPerStatus Then
            trPermissionPerType.Visible = True
            dvManualPerms.Visible = False
            dvPermissionStatus.Visible = True
            trLeaveType.Visible = False
            dvManualLeaves.Visible = False
            FillPerm_type()
            FillRequestStatus()
            dvDeductionPolicy.Visible = False
            rfvDeductionPolicy.Enabled = False
            dvDetailedStudyPemission.Visible = False
            dvLeaveStatus.Visible = False
            dvDelayPermissions.Visible = False
        ElseIf RadComboLeavesandPermissionsReports.SelectedValue = LeavesDeduction_HRA Then
            dvDeductionPolicy.Visible = True
            rfvDeductionPolicy.Enabled = True
            trPermissionPerType.Visible = False
            dvManualPerms.Visible = False
            dvPermissionStatus.Visible = False
            trLeaveType.Visible = False
            dvManualLeaves.Visible = False
            dvDetailedStudyPemission.Visible = False
            dvLeaveStatus.Visible = False
            dvDelayPermissions.Visible = False
        ElseIf RadComboLeavesandPermissionsReports.SelectedValue = DetailedStudyPermissions Then
            dvDetailedStudyPemission.Visible = True

            objAPP_Settings = New APP_Settings
            objAPP_Settings.GetByPK()
            If objAPP_Settings.EnableSemesterSelection_StudyPermission = True Then
                dvSemesterSelection.Visible = True
                FillSemesters()
                dvSemesterText.Visible = False
            Else
                dvSemesterSelection.Visible = False
                dvSemesterText.Visible = True
            End If

            dvDeductionPolicy.Visible = False
            rfvDeductionPolicy.Enabled = False
            trPermissionPerType.Visible = False
            dvManualPerms.Visible = False
            dvPermissionStatus.Visible = False
            trLeaveType.Visible = False
            dvManualLeaves.Visible = False
            dvLeaveStatus.Visible = False
            dvDelayPermissions.Visible = False
        ElseIf RadComboLeavesandPermissionsReports.SelectedValue = LeavePerStatus Then
            trPermissionPerType.Visible = False
            dvManualPerms.Visible = False
            dvPermissionStatus.Visible = False
            trLeaveType.Visible = True
            dvManualLeaves.Visible = False
            dvLeaveStatus.Visible = True
            FillLeave_type()
            FillLeaveRequestStatus()
            dvDeductionPolicy.Visible = False
            rfvDeductionPolicy.Enabled = False
            dvDetailedStudyPemission.Visible = False
            dvDelayPermissions.Visible = False
        ElseIf RadComboLeavesandPermissionsReports.SelectedValue = DelayPermissions Then
            trPermissionPerType.Visible = True
            FillPerm_type()
            dvDelayPermissions.Visible = True
            dvDeductionPolicy.Visible = False
            rfvDeductionPolicy.Enabled = False
            dvManualPerms.Visible = False
            dvPermissionStatus.Visible = False
            trLeaveType.Visible = False
            dvManualLeaves.Visible = False
            dvDetailedStudyPemission.Visible = False
            dvLeaveStatus.Visible = False
        ElseIf RadComboLeavesandPermissionsReports.SelectedValue = ActualPermissions Then
            dvDeductionPolicy.Visible = False
            rfvDeductionPolicy.Enabled = False
            trPermissionPerType.Visible = False
            dvManualPerms.Visible = False
            dvPermissionStatus.Visible = False
            trLeaveType.Visible = False
            dvManualLeaves.Visible = False
            dvDetailedStudyPemission.Visible = False
            dvLeaveStatus.Visible = False
            dvDelayPermissions.Visible = False
        Else
            dvDeductionPolicy.Visible = False
            rfvDeductionPolicy.Enabled = False
            trPermissionPerType.Visible = False
            dvManualPerms.Visible = False
            dvPermissionStatus.Visible = False
            trLeaveType.Visible = False
            dvManualLeaves.Visible = False
            dvDetailedStudyPemission.Visible = False
            dvLeaveStatus.Visible = False
            dvDelayPermissions.Visible = False
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
        If Not RadComboLeavesandPermissionsReports.SelectedValue = "-1" Then
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

            If RadComboLeavesandPermissionsReports.SelectedValue = EmployeesLeavesReport Then
                cryRpt.Subreports("rptSubLeaves").SetDataSource(SubDT)
            End If
            cryRpt.Subreports("rptHeader").SetDataSource(dt2)
            cryRpt.SetParameterValue("@ReportName", RadComboLeavesandPermissionsReports.SelectedItem.Text)
            cryRpt.SetParameterValue("@ReportName", RadComboLeavesandPermissionsReports.SelectedItem.Text, "rptHeader")
            cryRpt.SetParameterValue("@FromDate", DateToString(RadDatePicker1.SelectedDate))
            cryRpt.SetParameterValue("@ToDate", DateToString(RadDatePicker2.SelectedDate))
            'Version = System.Configuration.ConfigurationManager.AppSettings("Version").ToString()
            Version = SmartV.Version.version.GetVersionNumber
            cryRpt.SetParameterValue("@Version", Version)
            cryRpt.SetParameterValue("@UserName", UserName)

            If reportName = EmployeesLeavesReport Or reportName = LeavePerStatus Then
                cryRpt.SetParameterValue("@URL", BaseSiteUrl)
            ElseIf reportName = EmployeesPermissionsReport Then
                cryRpt.SetParameterValue("@URL", BaseSiteUrl)
            ElseIf reportName = StudyPermissionReport Then
                cryRpt.SetParameterValue("@URL", BaseSiteUrl)
            ElseIf reportName = NursingPermissionReport Then
                cryRpt.SetParameterValue("@URL", BaseSiteUrl)
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
                    If rblFormat.SelectedValue = 1 Then
                        cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedReport")
                    ElseIf rblFormat.SelectedValue = 2 Then
                        cryRpt.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, True, "ExportedReport")
                    ElseIf rblFormat.SelectedValue = 3 Then
                        FillExcelreport(reportName)
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
        If Not String.IsNullOrEmpty(txtGreaterthan.Text) Then
            rptObj.LimitDays = Integer.Parse(txtGreaterthan.Text) 'ID: M01 || Date: 11-05-2023 || By: Yahia shalan || Defining new variable to the new stored procedure parameter to filter based on the limit days'
        End If
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

        If EmployeeFilter1.ShowDirectStaffCheck = True Then
            rptObj.DirectStaffOnly = EmployeeFilter1.DirectStaffOnly
        Else
            rptObj.DirectStaffOnly = False
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
        If rpttid = PermissionPerType Then
            If Not ddlperm_type.SelectedValue = -1 Then
                rptObj.Permission_id = ddlperm_type.SelectedValue
            End If
            rptObj.ManualPermOnly = chkManualPerms.Checked
        End If
        If rpttid = EmployeesLeavesReport Then
            If Not radcomboLeaveType.SelectedValue = -1 Then
                rptObj.LeaveTypeId = radcomboLeaveType.SelectedValue
            End If
            rptObj.ManualLeavesOnly = chkManualLeaves.Checked
        End If
        If rpttid = PermissionPerStatus Then
            If Not ddlperm_type.SelectedValue = -1 Then
                rptObj.PermId = ddlperm_type.SelectedValue
            End If
            If Not radcmbRequestStatus.SelectedValue = -1 Then
                rptObj.Status = radcmbRequestStatus.SelectedValue
            End If
        End If
        If rpttid = LeavesDeduction_HRA Then
            rptObj.DeductionPolicy = radnumDeductionPolicy.Text
        End If

        If rpttid = DetailedStudyPermissions Then

            objAPP_Settings = New APP_Settings
            objAPP_Settings.GetByPK()
            If objAPP_Settings.EnableSemesterSelection_StudyPermission = True Then
                If Not radcmbxSemester.SelectedValue = -1 Then
                    rptObj.Semester = radcmbxSemester.SelectedValue
                End If
            Else
                rptObj.Semester = txtSemester.Text
            End If

            rptObj.StudyYear = IIf(txtStudyYear.Text = String.Empty, 0, txtStudyYear.Text)
        End If

        If rpttid = LeavePerStatus Then
            If Not radcmbLeaveRequestStatus.SelectedValue = -1 Then
                rptObj.Status = radcmbLeaveRequestStatus.SelectedValue
            End If
            If Not radcomboLeaveType.SelectedValue = -1 Then
                rptObj.LeaveTypeId = radcomboLeaveType.SelectedValue
            End If

        End If

        If rpttid = DelayPermissions Then
            If Not ddlperm_type.SelectedValue = -1 Then
                rptObj.PermId = ddlperm_type.SelectedValue
            End If
            rptObj.DelayCount = IIf(txtDelayCount.Text = String.Empty, 0, txtDelayCount.Text)
        End If
        'rpttid = IIf(rpttid = 0, 3, rpttid)
        Select Case rpttid
            Case EmployeesLeavesReport
                RPTName = "rptEmpLeaves.rpt"
                rptObj.Days = txtGreaterthan.Text
                DT = rptObj.GetEmpLeaves_perType
                'Case 2 'Employee list
                '    RPTName = "rptEmployeeList.rpt"
                '    DT = rptObj.GetFilterdEmpList()
                SubDT = rptObj.GetSub_LeavesDetails
            Case EmployeesPermissionsReport
                RPTName = "rptEmpPermissions.rpt"
                DT = rptObj.GetEmpPermissions
            Case ActualPermissions
                RPTName = "rptactualPermission.rpt"
                DT = rptObj.GetActualEmpPermissions
            Case ActualOfficalPermissions
                RPTName = "rptActualOfficalPermissions.rpt"
                DT = rptObj.GetOfficalActualEmpPermissions
            Case LeavesBalanceReport
                'RPTName = "rptEmp_Balance.rpt"
                RPTName = "rptEmp_Balance_TRC.rpt"
                DT = rptObj.GetEmpBalance
            Case LeavesBalanceReport_DOF ' Leave Balance DOF'
                RPTName = "rptEmp_Balance_DOF.rpt"
                DT = rptObj.GetEmpBalance_DOF
            Case ExpireLeavesBalanceReport
                RPTName = "rptEmp_BalanceExpire.rpt"
                DT = rptObj.GetEmpExpireBalance
            Case NursingPermissionReport
                RPTName = "rptNursingPermissions.rpt"
                DT = rptObj.GetNursing_Permission
            Case StudyPermissionReport
                RPTName = "rptStudyPermissions.rpt"
                DT = rptObj.GetStudy_Permission
            Case DetailedPermissionRequests
                RPTName = "rptDetailedPermissionRequests.rpt"
                DT = rptObj.Get_DetailedPermissions
            Case PermissionPerType
                RPTName = "rptPerm_per_type.rpt"
                DT = rptObj.GetPer_Permission()
            Case PermissionPerStatus
                RPTName = "rptPerm_per_Status.rpt"
                DT = rptObj.GetPermission_PerStatus()
            Case LeavesDeduction_HRA
                RPTName = "rptLeaveDeduction_HRA.rpt"
                DT = rptObj.GetLeaveDeduction_HRA()
            Case PermissionRequest_AuditLog
                RPTName = "rptPermissionRequests_Approval_AuditLog.rpt"
                DT = rptObj.Get_PermissionRequests_Approval_AuditLog()
            Case DetailedStudyPermissions
                RPTName = "rptDetailedStudyPermissions.rpt"
                DT = rptObj.GetDetailedStudy_Permission
            Case LeavePerStatus
                RPTName = "rptLeave_per_Status.rpt"
                DT = rptObj.GetEmpLeaves_perStatus
            Case DelayPermissions
                RPTName = "rptEmp_DelayPermissions.rpt"
                DT = rptObj.Get_DelayPermissions
            Case LeaveRequest_AuditLog
                RPTName = "rptLeaveRequests_Approval_AuditLog.rpt"
                DT = rptObj.Get_LeaveRequests_Approval_AuditLog
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

        CtlCommon.FillTelerikDropDownList(RadComboLeavesandPermissionsReports, objsysforms.GetFormsByParentID(formID, groupID), Lang)

    End Sub

    Private Sub FillPerm_type()
        Dim objPermissionsTypes As New PermissionsTypes
        CtlCommon.FillTelerikDropDownList(ddlperm_type, objPermissionsTypes.GetAll, Lang)
    End Sub

    Private Sub FillLeave_type()
        Dim objLeaveTypes As New LeavesTypes
        CtlCommon.FillTelerikDropDownList(radcomboLeaveType, objLeaveTypes.GetAll, Lang)
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
        HeadDT.Rows.Add(3)
        Dim rptName As String = RadComboLeavesandPermissionsReports.SelectedValue
        Dim dtFormName As DataTable
        dtFormName = objsysforms.GetBy_FormName(rptName)
        If Lang = CtlCommon.Lang.AR Then

            HeadDT(0)(0) = dtFormName(0)("Desc_Ar")
            '------Column(0)-----------
            HeadDT(1)(1) = "طبع بواسطة:"
            HeadDT(2)(1) = "تاريخ الطباعة:"
            HeadDT(3)(1) = "من تاريخ: " & Convert.ToDateTime(RadDatePicker1.SelectedDate).ToShortDateString
            '------Column(1)-----------
            HeadDT(1)(0) = SessionVariables.LoginUser.UsrID
            HeadDT(2)(0) = Date.Today.ToShortDateString
            HeadDT(3)(0) = "الى تاريخ: " & Convert.ToDateTime(RadDatePicker2.SelectedDate).ToShortDateString

        Else
            HeadDT(0)(0) = dtFormName(0)("Desc_En").ToString
            '------Column(0)-----------
            HeadDT(1)(0) = "Printed By:"
            HeadDT(2)(0) = "Printing Date:"
            HeadDT(3)(0) = "From Date: " & Convert.ToDateTime(RadDatePicker1.SelectedDate).ToShortDateString
            '------Column(1)-----------
            HeadDT(1)(1) = SessionVariables.LoginUser.UsrID
            HeadDT(2)(1) = Date.Today.ToShortDateString
            HeadDT(3)(1) = "To Date: " & Convert.ToDateTime(RadDatePicker2.SelectedDate).ToShortDateString
        End If

        HeadDT.Rows.Add(drFormName)
        '------------------ADD REPORT HEADER-----------------

        Select Case reportname
            Case EmployeesLeavesReport
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
            Case EmployeesPermissionsReport
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
                            ElseIf i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = CtlCommon.GetFullTimeString(row(i)).ToString
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
                            ElseIf i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = CtlCommon.GetFullTimeString(row(i)).ToString
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
                    NDT.Columns.RemoveAt(15)
                End If
            Case ActualPermissions
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(3).DataType = System.Type.GetType("System.String")
                    NDT.Columns(4).DataType = System.Type.GetType("System.String")
                    NDT.Columns(5).DataType = System.Type.GetType("System.String")
                    'NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    'NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 3 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = row(i).ToString()
                                End If
                            ElseIf i = 4 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = row(i).ToString()
                                End If
                            ElseIf i = 5 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = row(i).ToString()
                                End If

                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    ' NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    'NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    'NDT.Columns.RemoveAt(2)
                    'NDT.Columns(2).ColumnName = "اسم وحدة العمل"
                    'NDT.Columns.RemoveAt(3)
                    'NDT.Columns(3).ColumnName = "اسم الشركة"
                    NDT.Columns(2).ColumnName = "تاريخ المغادرة"
                    'NDT.Columns(2).ColumnName = "  وقت المغادرة"
                    ' NDT.Columns(4).ColumnName = "الى وقت"
                    'NDT.Columns(7).ColumnName = "المدة"
                    'NDT.Columns(8).ColumnName = "تاريخ انتهاء المغادرة"
                    'NDT.Columns(9).ColumnName = "لفترة"
                    'NDT.Columns.RemoveAt(10)
                    'NDT.Columns.RemoveAt(10)
                    NDT.Columns(3).ColumnName = "من"
                    NDT.Columns(4).ColumnName = "الى"
                    NDT.Columns(5).ColumnName = "المدة"
                    'NDT.Columns(11).ColumnName = "يوم كامل"
                    'NDT.Columns(12).ColumnName = "مرن"
                    'NDT.Columns(13).ColumnName = "الزمن المرن"
                    'NDT.Columns(14).ColumnName = "الملاحظات"
                    'NDT.Columns.RemoveAt(15)
                    'NDT.Columns.RemoveAt(15)
                    'NDT.Columns.RemoveAt(15)
                Else
                    'NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    'NDT.Columns(9).DataType = System.Type.GetType("System.String")
                    'NDT.Columns(10).DataType = System.Type.GetType("System.String")
                    'NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    'NDT.Columns(12).DataType = System.Type.GetType("System.String")
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
                                    dr(i) = CtlCommon.GetFullTimeString(row(i)).ToString
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    'NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    'NDT.Columns(2).ColumnName = "Entity Name"
                    'NDT.Columns.RemoveAt(3)
                    'NDT.Columns(3).ColumnName = "Company Name"
                    'NDT.Columns.RemoveAt(4)
                    NDT.Columns(2).ColumnName = "Permission Date"
                    'NDT.Columns(3).ColumnName = " Time"
                    'NDT.Columns(6).ColumnName = "To Time"
                    'NDT.Columns(7).ColumnName = "Period"
                    'NDT.Columns(8).ColumnName = "Permission End Date"
                    'NDT.Columns(9).ColumnName = "Is For Period"
                    'NDT.Columns.RemoveAt(10)
                    NDT.Columns(3).ColumnName = "Perm Type"
                    'NDT.Columns.RemoveAt(11)
                    'NDT.Columns(11).ColumnName = "Full Day"
                    'NDT.Columns(12).ColumnName = "Is Flexibile"
                    'NDT.Columns(13).ColumnName = "Flexibile Duration"
                    'NDT.Columns(14).ColumnName = "Remarks"
                    'NDT.Columns.RemoveAt(15)
                    'NDT.Columns.RemoveAt(15)
                    'NDT.Columns.RemoveAt(15)
                End If
            Case LeavesBalanceReport_DOF 'DOF Annual Leave Balance
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
                    NDT.Columns(9).ColumnName = "رصيدالاجازة قيد الدراسة"
                    NDT.Columns(10).ColumnName = "رصيد الإجازة"
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
                    NDT.Columns(9).ColumnName = "Pending Leave Days"
                    NDT.Columns(10).ColumnName = "Leave Balance"
                    NDT.Columns(11).ColumnName = "Remarks"
                End If
            Case ExpireLeavesBalanceReport
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
            Case PermissionPerType

                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(14).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
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
                    NDT.Columns(5).ColumnName = "اسم الشركة"
                    NDT.Columns(6).ColumnName = "اسم الشركة باللغة العربية"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "اسم المغادرة"
                    NDT.Columns(8).ColumnName = "اسم المغادرة باللغة العربية"
                    NDT.Columns(9).ColumnName = "تاريخ المغادرة"
                    NDT.Columns(10).ColumnName = "تاريخ انتهاء المغادرة"
                    NDT.Columns(11).ColumnName = "من وقت"
                    NDT.Columns(12).ColumnName = "الى وقت"
                    NDT.Columns(13).ColumnName = "المدة"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "الايام"
                    NDT.Columns(15).ColumnName = "يوم كامل"
                    NDT.Columns(16).ColumnName = "مرن"
                    NDT.Columns(17).ColumnName = "الزمن المرن"
                    NDT.Columns(18).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns.RemoveAt(19)
                Else
                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(14).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
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
                    NDT.Columns(5).ColumnName = "Company Name"
                    NDT.Columns(6).ColumnName = "Company Arabic Name"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "Permission Name"
                    NDT.Columns(8).ColumnName = "Permission Arabic Name"
                    NDT.Columns(9).ColumnName = "Permission Date"
                    NDT.Columns(10).ColumnName = "Permission End Date"
                    NDT.Columns(11).ColumnName = "From Time"
                    NDT.Columns(12).ColumnName = "To Time"
                    NDT.Columns(13).ColumnName = "Period"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "Days"
                    NDT.Columns(15).ColumnName = "Full Day"
                    NDT.Columns(16).ColumnName = "Is Flexibile"
                    NDT.Columns(17).ColumnName = "Flexibile Duration"
                    NDT.Columns(18).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(19)
                    NDT.Columns.RemoveAt(19)
                End If

            Case NursingPermissionReport

                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(14).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    NDT.Columns(16).DataType = System.Type.GetType("System.String")
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
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 16 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
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
                    NDT.Columns(2).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "نوع المغادرة"
                    NDT.Columns(5).ColumnName = "تاريخ المغادرة"
                    NDT.Columns(6).ColumnName = "تاريخ انتهاء المغادرة"
                    NDT.Columns(7).ColumnName = "من وقت"
                    NDT.Columns(8).ColumnName = "الى وقت"
                    NDT.Columns(9).ColumnName = "لفترة"
                    NDT.Columns(10).ColumnName = "يوم كامل"
                    NDT.Columns(11).ColumnName = "مرن"
                    NDT.Columns(12).ColumnName = "الزمن المرن"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                Else
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(14).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    NDT.Columns(16).DataType = System.Type.GetType("System.String")
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
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 16 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
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
                    NDT.Columns(2).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Perm Type"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Permission Date"
                    NDT.Columns(6).ColumnName = "Permission End Date"
                    NDT.Columns(7).ColumnName = "From Time"
                    NDT.Columns(8).ColumnName = "To Time"
                    NDT.Columns(9).ColumnName = "Is For Period"
                    NDT.Columns(10).ColumnName = "Full Day"
                    NDT.Columns(11).ColumnName = "Is Flexibile"
                    NDT.Columns(12).ColumnName = "Flexibile Duration"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                End If
            Case StudyPermissionReport

                If (Lang = CtlCommon.Lang.AR) Then

                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(14).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    NDT.Columns(16).DataType = System.Type.GetType("System.String")
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
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 16 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
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
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "اسم الشركة"
                    NDT.Columns(6).ColumnName = "اسم الشركة باللغة العربية"
                    NDT.Columns(7).ColumnName = "نوع المغادرة"
                    NDT.Columns(8).ColumnName = "نوع المغادرة باللغة العربية"
                    NDT.Columns(9).ColumnName = "تاريخ المغادرة"
                    NDT.Columns(10).ColumnName = "تاريخ انتهاء المغادرة"
                    NDT.Columns(11).ColumnName = "من وقت"
                    NDT.Columns(12).ColumnName = "الى وقت"
                    NDT.Columns(13).ColumnName = "لفترة"
                    NDT.Columns(14).ColumnName = "يوم كامل"
                    NDT.Columns(15).ColumnName = "مرن"
                    NDT.Columns(16).ColumnName = "الزمن المرن"
                    NDT.Columns.RemoveAt(18)
                    NDT.Columns.RemoveAt(18)

                Else
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(14).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    NDT.Columns(16).DataType = System.Type.GetType("System.String")
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
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 16 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
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
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Company Name"
                    NDT.Columns(6).ColumnName = "Company Arabic Name"
                    NDT.Columns(7).ColumnName = "Perm Type"
                    NDT.Columns(8).ColumnName = "Perm Arabic Type"
                    NDT.Columns(9).ColumnName = "Permission Date"
                    NDT.Columns(10).ColumnName = "Permission End Date"
                    NDT.Columns(11).ColumnName = "From Time"
                    NDT.Columns(12).ColumnName = "To Time"
                    NDT.Columns(13).ColumnName = "Is For Period"
                    NDT.Columns(14).ColumnName = "Full Day"
                    NDT.Columns(15).ColumnName = "Is Flexibile"
                    NDT.Columns(16).ColumnName = "Flexibile Duration"
                    NDT.Columns.RemoveAt(18)
                    NDT.Columns.RemoveAt(18)

                End If
            Case DetailedPermissionRequests
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(7).DataType = System.Type.GetType("System.String")
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")
                    NDT.Columns(10).DataType = System.Type.GetType("System.String")
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
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
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
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "نوع المغادرة"
                    NDT.Columns(3).ColumnName = "تاريخ المغادرة"
                    NDT.Columns(4).ColumnName = "من وقت"
                    NDT.Columns(5).ColumnName = "الى وقت"
                    NDT.Columns(6).ColumnName = "تاريخ انتهاء المغادرة"
                    NDT.Columns(7).ColumnName = "يوم كامل"
                    NDT.Columns(8).ColumnName = "مرن"
                    NDT.Columns(9).ColumnName = "الوقت المرن"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "رقم المدير"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "اسم المدير"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "الملاحظات"
                    NDT.Columns(13).ColumnName = "اسم المستخدم"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns(15).ColumnName = "اسم الشركة"
                Else
                    NDT.Columns(7).DataType = System.Type.GetType("System.String")
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")
                    NDT.Columns(10).DataType = System.Type.GetType("System.String")
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
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
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
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Permission Type"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "Permission Date"
                    NDT.Columns(4).ColumnName = "From Time"
                    NDT.Columns(5).ColumnName = "To Time"
                    NDT.Columns(6).ColumnName = "Permission End Date"
                    NDT.Columns(7).ColumnName = "Full Day"
                    NDT.Columns(8).ColumnName = "Flexible"
                    NDT.Columns(9).ColumnName = "Flexible Duration"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Manager No."
                    NDT.Columns(11).ColumnName = "Manager Name"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "User Name"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns(15).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(16)

                End If
            Case PermissionPerStatus
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")
                    NDT.Columns(10).DataType = System.Type.GetType("System.String")
                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 10 Then
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
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "نوع المغادرة"
                    NDT.Columns(5).ColumnName = "تاريخ البداية"
                    NDT.Columns(6).ColumnName = "تاريخ الانتهاء"
                    NDT.Columns(7).ColumnName = "من وقت"
                    NDT.Columns(8).ColumnName = "الى وقت"
                    NDT.Columns(9).ColumnName = "يوم كامل"
                    NDT.Columns(10).ColumnName = "لفترة"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "مرن"
                    NDT.Columns(12).ColumnName = "مدة الوقت المرن"
                    NDT.Columns(13).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "حالة الطلب"
                    NDT.Columns(15).ColumnName = "المدة"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)

                Else
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")
                    NDT.Columns(10).DataType = System.Type.GetType("System.String")
                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 10 Then
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
                    NDT.Columns(4).ColumnName = "Permission Type"
                    NDT.Columns.RemoveAt(5)

                    NDT.Columns(5).ColumnName = "Permission Date"
                    NDT.Columns(6).ColumnName = "Permission End Date"
                    NDT.Columns(7).ColumnName = "From Time"
                    NDT.Columns(8).ColumnName = "To Time"
                    NDT.Columns(9).ColumnName = "Full Day"
                    NDT.Columns(10).ColumnName = "For Period"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)

                    NDT.Columns(11).ColumnName = "Flexible"
                    NDT.Columns(12).ColumnName = "Flexible Duration"
                    NDT.Columns(13).ColumnName = "Remarks"
                    NDT.Columns(14).ColumnName = "Status"
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns(15).ColumnName = "Duration"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)

                End If

            Case LeavesDeduction_HRA
                NDT = DT
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "اسم الشركة"
                    NDT.Columns(4).ColumnName = "عدد ايام الاجازات"
                    NDT.Columns(5).ColumnName = "الوقت الضائع"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "نوع الاجازة"
                Else
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "No. Of Days"
                    NDT.Columns(5).ColumnName = "Lost Time"
                    NDT.Columns(6).ColumnName = "Leave Type"
                    NDT.Columns.RemoveAt(7)
                End If

            Case PermissionRequest_AuditLog
                NDT.Columns(5).DataType = System.Type.GetType("System.String")
                NDT.Columns(6).DataType = System.Type.GetType("System.String")
                NDT.Columns(7).DataType = System.Type.GetType("System.String")
                NDT.Columns(8).DataType = System.Type.GetType("System.String")
                NDT.Columns(24).DataType = System.Type.GetType("System.String")
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
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 7 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 8 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                            End If
                        ElseIf i = 24 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i))
                            End If
                        End If
                    Next
                    NDT.Rows.Add(dr)
                Next

                If (Lang = CtlCommon.Lang.AR) Then

                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "نوع المفادرة"
                    NDT.Columns(3).ColumnName = "من تاريخ"
                    NDT.Columns(4).ColumnName = "من وقت"
                    NDT.Columns(5).ColumnName = "الى وقت"
                    NDT.Columns(6).ColumnName = "الى تاريخ"
                    NDT.Columns(7).ColumnName = "سبب الرفض"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "متخذ القرار"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "تاريخ القرار"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "الحالة"
                    NDT.Columns(12).ColumnName = "مرن"
                    NDT.Columns(12).ColumnName = "الوقت المرن"
                    NDT.Columns(13).ColumnName = "تاريخ الطلب"
                Else

                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Permission Type"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "From Date"
                    NDT.Columns(4).ColumnName = "From Time"
                    NDT.Columns(5).ColumnName = "To Time"
                    NDT.Columns(6).ColumnName = "To Date"
                    NDT.Columns(7).ColumnName = "Rejection Reason"
                    NDT.Columns(8).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Decision Maker"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "Decision Date"
                    NDT.Columns(12).ColumnName = "Status"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(12).ColumnName = "Flexible"
                    NDT.Columns(12).ColumnName = "Flexible Duration"
                    NDT.Columns(13).ColumnName = "Request Date"
                End If

            Case DetailedStudyPermissions

                If (Lang = CtlCommon.Lang.AR) Then

                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(14).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    NDT.Columns(16).DataType = System.Type.GetType("System.String")
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
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 16 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
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
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "اسم الشركة"
                    NDT.Columns(6).ColumnName = "اسم الشركة باللغة العربية"
                    NDT.Columns(7).ColumnName = "نوع المغادرة"
                    NDT.Columns(8).ColumnName = "نوع المغادرة باللغة العربية"
                    NDT.Columns(9).ColumnName = "تاريخ المغادرة"
                    NDT.Columns(10).ColumnName = "تاريخ انتهاء المغادرة"
                    NDT.Columns(11).ColumnName = "من وقت"
                    NDT.Columns(12).ColumnName = "الى وقت"
                    NDT.Columns(13).ColumnName = "لفترة"
                    NDT.Columns(14).ColumnName = "يوم كامل"
                    NDT.Columns(15).ColumnName = "مرن"
                    NDT.Columns(16).ColumnName = "الزمن المرن"
                    NDT.Columns.RemoveAt(18)
                    NDT.Columns.RemoveAt(18)

                Else
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(14).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    NDT.Columns(16).DataType = System.Type.GetType("System.String")
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
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 16 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
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
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Company Name"
                    NDT.Columns(6).ColumnName = "Company Arabic Name"
                    NDT.Columns(7).ColumnName = "Perm Type"
                    NDT.Columns(8).ColumnName = "Perm Arabic Type"
                    NDT.Columns(9).ColumnName = "Permission Date"
                    NDT.Columns(10).ColumnName = "Permission End Date"
                    NDT.Columns(11).ColumnName = "From Time"
                    NDT.Columns(12).ColumnName = "To Time"
                    NDT.Columns(13).ColumnName = "Is For Period"
                    NDT.Columns(14).ColumnName = "Full Day"
                    NDT.Columns(15).ColumnName = "Is Flexibile"
                    NDT.Columns(16).ColumnName = "Flexibile Duration"
                    NDT.Columns.RemoveAt(18)
                    NDT.Columns.RemoveAt(18)

                End If

            Case DelayPermissions

                NDT.Columns(4).DataType = System.Type.GetType("System.String")
                NDT.Columns(5).DataType = System.Type.GetType("System.String")
                NDT.Columns(6).DataType = System.Type.GetType("System.String")

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
                        ElseIf i = 6 Then
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
                    NDT.Columns(2).ColumnName = "تاريخ المغادرة/التأخير"
                    NDT.Columns(3).ColumnName = "المغادرة/التأخير من وقت"
                    NDT.Columns(4).ColumnName = "المغادرة/التأخير الى وقت"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "نوع المغادرة"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "وحدة العمل"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "الشركة"
                    NDT.Columns(8).ColumnName = "عدد المغادرات"
                Else

                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Emp Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Permission\Delay Date"
                    NDT.Columns(3).ColumnName = "Permission\Delay From Time"
                    NDT.Columns(4).ColumnName = "Permission\Delay To Time"
                    NDT.Columns(5).ColumnName = "Permission Type\ Delay"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Number of Permission(s)\ Delay(s)"
                End If

            Case LeaveRequest_AuditLog
                NDT.Columns(6).DataType = System.Type.GetType("System.String")
                NDT.Columns(7).DataType = System.Type.GetType("System.String")
                NDT.Columns(8).DataType = System.Type.GetType("System.String")
                NDT.Columns(13).DataType = System.Type.GetType("System.String")
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
                                dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                            End If
                        ElseIf i = 8 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                            End If
                        ElseIf i = 13 Then
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
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "نوع الاجازة"
                    NDT.Columns(3).ColumnName = "تاريخ الطلب"
                    NDT.Columns(4).ColumnName = "من تاريخ"
                    NDT.Columns(5).ColumnName = "الى تاريخ"
                    NDT.Columns(6).ColumnName = "سبب الرفض"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "الحالة"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "تاريخ القرار"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "اسم متخذ القرار"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                Else
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Leave Type"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "Request Date"
                    NDT.Columns(4).ColumnName = "From Date"
                    NDT.Columns(5).ColumnName = "To Date"
                    NDT.Columns(6).ColumnName = "Rejection Reason"
                    NDT.Columns(7).ColumnName = "Status"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Decision Date"
                    NDT.Columns(9).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "Decision Maker"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                End If

        End Select
    End Sub

    Public Sub ClearFilter()
        EmployeeFilter1.ClearValues()

    End Sub

    Private Sub FillRequestStatus()
        objRequestStatus = New RequestStatus
        With objRequestStatus
            CtlCommon.FillTelerikDropDownList(radcmbRequestStatus, .GetAll, Lang)
        End With
    End Sub

    Private Sub FillLeaveRequestStatus()
        objRequestStatus = New RequestStatus
        With objRequestStatus
            CtlCommon.FillTelerikDropDownList(radcmbLeaveRequestStatus, .GetAll, Lang)
        End With
    End Sub

    Private Sub FillSemesters()
        objSemesters = New Semesters
        With objSemesters
            CtlCommon.FillTelerikDropDownList(radcmbxSemester, .GetAll, Lang)
        End With
    End Sub

#End Region

End Class
