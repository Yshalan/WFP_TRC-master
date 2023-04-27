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

Partial Class Emp_EmpSelf_Reports
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

        If Not Page.IsPostBack Then
            Dim rptid As Integer = Request.QueryString("ReportId")

            Select Case rptid
                Case 1

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "التقرير اليومي للموظف", "Employee Daily Report")
                    'Case 2

                    '    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "قائمة الموظفين", "Employee List")
                Case 3

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "حركات الحضور للموظف", "Employee Attendance Transactions")
                Case 4

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "إجازات الموظف", "Employee Leaves")

                Case 5 ' Absent

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير غياب الموظف", "Employee Absent Report")

                Case 6 ' Extra

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, " الساعات الإضافية للموظف", "Employee Extra Hours")

                Case 7 ' Violations

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, " مخالفات الموظف", "Employee Violations")

                Case 8 ' IN OUT

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير حركة الموظف التفصيلي", "Employee In Out Transactions")

                Case 9 ' Summary

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, " تقرير ملخص حضور الموظف ", "Employee Attendance Summary")
                    'Case 10

                    '    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "قائمة الموظفين غير المفعلين", "Terminated Employee List")
                Case 11

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, " تقرير اجازات الموظف", "Employee Leaves Report")
                Case 12

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير مغادرات الموظف", "Employee Permissions Report")
                Case 13

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير رصيد اجازات الموظف المنتهية", "Employee Expired Leaves Balance Report")
                Case 14

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, " تقرير رصيد اجازات الموظف ", "Employee Leaves Balance Report")
                Case 15

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير  الحركات المفصلة ", " Detailed Transaction")

            End Select
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
        Next


    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        cryRpt.Close()
        cryRpt.Dispose()

    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim Id As Integer = Request.QueryString("id")
        BindReport(Id)
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
        SetReportName(Request.QueryString("ReportId"))
        If IS_EXIST = True Then
            cryRpt = New ReportDocument
            cryRpt.Load(Server.MapPath(RPTName))
            cryRpt.SetDataSource(DT)
            Dim objAPP_Settings As New APP_Settings
            objAPP_Settings.FK_CompanyId = SessionVariables.LoginUser.FK_CompanyId
            dt2 = objAPP_Settings.GetHeader()
            dt2.Columns.Add("From_Date")
            dt2.Columns.Add("To_Date")
            dt2.Rows(0).Item("From_Date") = DateToString(RadDatePicker1.SelectedDate)
            dt2.Rows(0).Item("To_Date") = DateToString(RadDatePicker2.SelectedDate)
            cryRpt.Subreports(cryRpt.Subreports().Item(0).Name).SetDataSource(dt2)
            Select Case Request.QueryString("ReportId")
                Case 1
                    cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "التقرير اليومي للموظف", "Employee Daily Report"))
                    cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "التقرير اليومي للموظف", "Employee Daily Report"), "rptHeader")
                Case 3
                    cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "حركات الحضور للموظف", "Employee Attendance Transactions"))
                    cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "حركات الحضور للموظف", "Employee Attendance Transactions"), "rptHeader")
                Case 4
                    cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "إجازات الموظف", "Employee Leaves"))
                    cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "إجازات الموظف", "Employee Leaves"), "rptHeader")
                Case 5 ' Absent
                    cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "تقرير غياب الموظف", "Employee Absent Report"))
                    cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "تقرير غياب الموظف", "Employee Absent Report"), "rptHeader")
                Case 6 ' Extra
                    cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, " الساعات الإضافية للموظف", "Employee Extra Hours"))
                    cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, " الساعات الإضافية للموظف", "Employee Extra Hours"), "rptHeader")
                Case 7 ' Violations
                    cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, " مخالفات الموظف", "Employee Violations"))
                    cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, " مخالفات الموظف", "Employee Violations"), "rptHeader")
                Case 8 ' IN OUT
                    cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "تقرير حركة الموظف التفصيلي", "Employee In Out Transactions"))
                    cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "تقرير حركة الموظف التفصيلي", "Employee In Out Transactions"), "rptHeader")
                Case 9 ' Summary
                    cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, " تقرير ملخص حضور الموظف ", "Employee Attendance Summary"))
                    cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, " تقرير ملخص حضور الموظف ", "Employee Attendance Summary"), "rptHeader")
                Case 11
                    cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, " تقرير اجازات الموظف", "Employee Leaves Report"))
                    cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, " تقرير اجازات الموظف", "Employee Leaves Report"), "rptHeader")
                Case 12
                    cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "تقرير مغادرات الموظف", "Employee Permissions Report"))
                    cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "تقرير مغادرات الموظف", "Employee Permissions Report"), "rptHeader")
                Case 13
                    cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "تقرير رصيد اجازات الموظف المنتهية", "Employee Expired Leaves Balance Report"))
                    cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "تقرير رصيد اجازات الموظف المنتهية", "Employee Expired Leaves Balance Report"), "rptHeader")
                Case 14
                    cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, " تقرير رصيد اجازات الموظف ", "Employee Leaves Balance Report"))
                    cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, " تقرير رصيد اجازات الموظف ", "Employee Leaves Balance Report"), "rptHeader")
                Case 15
                    cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "تقرير  الحركات المفصلة ", " Detailed Transaction"))
                    cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "تقرير  الحركات المفصلة ", " Detailed Transaction"), "rptHeader")
            End Select
            cryRpt.SetParameterValue("@FromDate", DateToString(RadDatePicker1.SelectedDate))
            cryRpt.SetParameterValue("@ToDate", DateToString(RadDatePicker2.SelectedDate))
            'Version = System.Configuration.ConfigurationManager.AppSettings("Version").ToString()
            Version = SmartV.Version.version.GetVersionNumber
            cryRpt.SetParameterValue("@Version", Version)
            cryRpt.SetParameterValue("@UserName", UserName)

            If Request.QueryString("ReportId") = 11 Then
                cryRpt.SetParameterValue("@URL", BaseSiteUrl)
            ElseIf Request.QueryString("ReportId") = 12 Then
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
                Dim rptObj As New Report
                Dim objUser As New Employee
                objUser.EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
                objUser.GetByPK()
                objOrgCompany.CompanyId = objUser.FK_CompanyId
                objOrgCompany.GetByPK()
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
                        FillExcelreport()
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

    Private Sub SetReportName(ByVal rpttid As Integer)
        Dim rptObj As New Report
        If Not RadDatePicker1.SelectedDate Is Nothing Then
            rptObj.FROM_DATE = RadDatePicker1.DbSelectedDate
        End If
        If Not RadDatePicker2.SelectedDate Is Nothing Then
            rptObj.TO_DATE = RadDatePicker2.DbSelectedDate
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
        rptObj.EntityId = Nothing
        EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
        Dim err As Integer = -1
        Dim objEmployee As New Employee
        objEmployee.EmployeeId = EmployeeId
        err = objEmployee.GetExist_Employee()
        If err = -99 Then
            IS_EXIST = True
            rptObj.EmployeeId = EmployeeId
            rpttid = IIf(rpttid = 0, 3, rpttid)
            Select Case rpttid
                Case 1 'Employee move
                    RPTName = "rptEmpMove.rpt"
                    DT = rptObj.GetFilterdEmpMove()
                    'Case 2 'Employee list
                    '    RPTName = "rptEmployeeList.rpt"
                    '    DT = rptObj.GetFilterdEmpList()

                Case 3 'Details Employee Move
                    RPTName = "rptDetEmpMove.rpt"
                    DT = rptObj.GetFilterdDetEmpMove()

                Case 4 'Leaves
                    RPTName = "RptLeaves.rpt"
                    DT = rptObj.GetEmpLeaves()

                Case 5 ' Absent
                    RPTName = "rptEmpAbsent.rpt"
                    DT = rptObj.GetEmpAbsent()

                Case 6 ' Extra
                    RPTName = "crExtraHours.rpt"
                    DT = rptObj.GetExtraHours()

                Case 7 ' Violations
                    RPTName = "rptViolations.rpt"
                    DT = rptObj.GetViolations()

                Case 8 ' In Out
                    RPTName = "rptEmpInOut.rpt"
                    DT = rptObj.GetEmpInOut()
                Case 9 ' Summary
                    RPTName = "rptSummary.rpt"
                    DT = rptObj.GetSummary()
                    'Case 10
                    '    RPTName = "rptTerminatedEmployeeList.rpt"
                    '    DT = rptObj.GetFilterdTerminatedEmpList()
                Case 11 'Employee Leaves
                    RPTName = "rptEmpLeaves.rpt"
                    DT = rptObj.GetEmpLeaves
                Case 12 'Employee Permissions
                    RPTName = "rptEmpPermissions.rpt"
                    DT = rptObj.GetEmpPermissions
                Case 13 'Employee Expired Leave Balance
                    RPTName = "rptEmp_BalanceExpire.rpt"
                    DT = rptObj.GetEmpExpireBalance
                Case 14 'Employee Leave Balance
                    RPTName = "rptEmp_Balance.rpt"
                    DT = rptObj.GetEmpBalance
                Case 15 'Employee Detailed Transaction
                    RPTName = "rptDetailedTransactions.rpt"
                    DT = rptObj.GetDetailed_Transactions
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

    Private Sub FillExcelreport()
        Dim rptid As Integer = Request.QueryString("ReportId")
        NDT = New DataTable
        NDT = DT.Clone()

        Dim UK_Culture As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("en-GB")

        Select Case rptid
            Case 1 'rptEmpMove
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
                    NDT.Columns(12).ColumnName = "الوقت الضائع"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "اسم العطلة"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "نوع الاجازة"
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns(15).ColumnName = "نوع المغادرة"
                    NDT.Columns(16).ColumnName = "الوقت الاضافي"
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
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
                                   "اسم العطلة",
                                   "نوع الاجازة",
                                   "نوع المغادرة",
                                   "الوقت الاضافي"})
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                    For Each row As DataRow In NDT.Rows
                        If row(7).ToString = "" And Not row(8).ToString = "" And row(13).ToString = "" Then
                            row(13) = "لايوجد دخول"
                        ElseIf Not row(7).ToString = "" And row(8).ToString = "" And row(13).ToString = "" Then
                            row(13) = "لايوجد خروج"
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
                    NDT.Columns(13).ColumnName = "Holiday Name"
                    NDT.Columns.RemoveAt(14)
                    NDT.Columns(14).ColumnName = "Leave Name"
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns(15).ColumnName = "Permission Name"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns(16).ColumnName = "OverTime"
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(18)
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
                                    "Holiday Name",
                                    "Leave Name",
                                    "Permission Name",
                                    "OverTime"})
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                    For Each row As DataRow In NDT.Rows
                        If row(7).ToString = "" And Not row(8).ToString = "" And row(13).ToString = "" Then
                            row(13) = "Missing In"
                        ElseIf Not row(7).ToString = "" And row(8).ToString = "" And row(13).ToString = "" Then
                            row(13) = "Missing Out"
                        End If
                    Next
                    '---------Add Missing IN & OUT in the Remarks field-----------'

                End If
            Case 3 'rptDetEmpMove
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
            Case 5 'RptEmpAbsent
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
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "مجموع ساعات العمل"
                    NDT.Columns.RemoveAt(12)
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
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "Total Work Hours"
                    NDT.Columns.RemoveAt(12)
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
            Case 8 'rptEmp_MoveInOut
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
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            ElseIf i = 15 Then
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
                    NDT.Columns(7).ColumnName = "المدة"
                    NDT.Columns(8).ColumnName = "تاريخ الحركة"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "وقت الدخول"
                    NDT.Columns(10).ColumnName = "وقت الخروج"
                    NDT.Columns.RemoveAt(11)
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
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            ElseIf i = 15 Then
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
                    NDT.Columns(7).ColumnName = "Duration"
                    NDT.Columns(8).ColumnName = "Move Date"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "In Time"
                    NDT.Columns(10).ColumnName = "Out Time"
                    NDT.Columns.RemoveAt(11)
                End If
            Case 9 'rptSummary
                NDT.Columns(0).ColumnName = "Emp No."
                NDT.Columns(1).ColumnName = "Employee Name"
                NDT.Columns(2).ColumnName = "Employee Arabic Name"
                NDT.Columns(3).ColumnName = "Delay"
                NDT.Columns(4).ColumnName = "Early Out"
                NDT.Columns(5).ColumnName = "OverTime"
                NDT.Columns(6).ColumnName = "Delay Count"
                NDT.Columns(7).ColumnName = "Early Out Count"
                NDT.Columns(8).ColumnName = "OverTime count"
                NDT.Columns(9).ColumnName = "Absent Count"
                NDT.Columns(10).ColumnName = "Rest Coumt"
            Case 11 'rptEmpLeaves
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(7).DataType = System.Type.GetType("System.String")
                    NDT.Columns(10).DataType = System.Type.GetType("System.String")
                    NDT.Columns(11).DataType = System.Type.GetType("System.String")

                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
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
                    NDT.Columns(12).ColumnName = "Days"
                End If
            Case 12 'rptEmpPermission
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")
                    NDT.Columns(10).DataType = System.Type.GetType("System.String")
                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            ElseIf i = 10 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            ElseIf i = 11 Then
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
                    NDT.Columns(3).ColumnName = "اسم وحدة العمل"
                    NDT.Columns(4).ColumnName = "اسم وحدة العمل باللغة العربية"
                    NDT.Columns(5).ColumnName = "اسم الشركة"
                    NDT.Columns(6).ColumnName = "اسم الشركة باللغة العربية"
                    NDT.Columns(7).ColumnName = "تاريخ المغادرة"
                    NDT.Columns(8).ColumnName = "من وقت"
                    NDT.Columns(9).ColumnName = "الى وقت"
                    NDT.Columns(10).ColumnName = "تاريخ انتهاء المغادرة"
                    NDT.Columns(11).ColumnName = "لمدة"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "اسم المغادرة"
                    NDT.Columns(13).ColumnName = "اسم المغادرة باللغة العربية"
                Else
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")
                    NDT.Columns(10).DataType = System.Type.GetType("System.String")
                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            ElseIf i = 10 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            ElseIf i = 11 Then
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
                    NDT.Columns(3).ColumnName = "Entity Name"
                    NDT.Columns(4).ColumnName = "Entity Arabic Name"
                    NDT.Columns(5).ColumnName = "Company Name"
                    NDT.Columns(6).ColumnName = "Company Arabic Name"
                    NDT.Columns(7).ColumnName = "Permission Date"
                    NDT.Columns(8).ColumnName = "From Time"
                    NDT.Columns(9).ColumnName = "To Time"
                    NDT.Columns(10).ColumnName = "Permission End Date"
                    NDT.Columns(11).ColumnName = "Is For Period"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "Permission Name"
                    NDT.Columns(13).ColumnName = "Permission Arabic Name"
                End If
            Case 13 'Rpt_EmpLeaves_BalanceExpired
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
            Case 14 'Rpt_Emp_Balance
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
                    NDT.Columns(7).ColumnName = "اسم الاجازة"
                    NDT.Columns(8).ColumnName = "اسم الاجازة باللغة العربية"
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
                    NDT.Columns(7).ColumnName = "Leave Name"
                    NDT.Columns(8).ColumnName = "Leave Arabic Name"
                    NDT.Columns(9).ColumnName = "Balance"
                    NDT.Columns(10).ColumnName = "Total Balance"
                    NDT.Columns(11).ColumnName = "Remarks"
                End If
            Case 15  'rptDetailedTransactions

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
                        row("REMARKS") = row("StatusNameArb")
                    Next


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
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "نوع الاجازة"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "نوع المغادرة"
                    NDT.Columns(11).ColumnName = "من وقت"
                    NDT.Columns(12).ColumnName = "الى وقت"
                    NDT.Columns(13).ColumnName = "التأخير"
                    NDT.Columns(14).ColumnName = "الخروج المبكر"
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns.RemoveAt(15)
                    NDT.Columns(15).ColumnName = "اسم العطلة"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns(16).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
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
                                                "اسم السبب",
                                                "نوع الاجازة",
                                                "نوع المغادرة",
                                                "من وقت",
                                                "الى وقت",
                                                "التأخير",
                                                "الخروج المبكر",
                                                "اسم العطلة",
                                                "الملاحظات"})

                    '---------Add Missing IN & OUT in the Remarks field-----------'
                    For Each row As DataRow In NDT.Rows
                        If row("وقت الدخول").ToString = "" And Not row("وقت الخروج").ToString = "" And row("الملاحظات").ToString = "" Then
                            row("الملاحظات") = "لايوجد دخول"
                        ElseIf Not row("وقت الدخول").ToString = "" And row("وقت الخروج").ToString = "" And row("الملاحظات").ToString = "" Then
                            row("الملاحظات") = "لايوجد خروج"
                        End If
                    Next
                    '---------Add Missing IN & OUT in the Remarks field-----------'


                Else
                    For Each row As DataRow In NDT.Rows
                        row("REMARKS") = row("StatusName")
                    Next

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
                    NDT.Columns(17).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(18)
                    NDT.Columns.RemoveAt(18)
                    NDT.Columns.RemoveAt(18)
                    NDT.Columns.RemoveAt(18)

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
                                                 "Reason Name",
                                                 "Leave Type",
                                                 "Permission Type",
                                                 "From Time",
                                                 "To Time",
                                                 "Delay",
                                                 "Early Out",
                                                 "Holiday Name",
                                                 "REMARKS",
                                                 "Status Name"})
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                    For Each row As DataRow In NDT.Rows
                        If row("In Time").ToString = "" And Not row("Out Time").ToString = "" And row("Remarks").ToString = "" Then
                            row("Remarks") = "Missing In"
                        ElseIf Not row("In Time").ToString = "" And row("Out Time").ToString = "" And row("Remarks").ToString = "" Then
                            row("Remarks") = "Missing Out"
                        End If
                    Next
                    '---------Add Missing IN & OUT in the Remarks field-----------'
                End If
        End Select
    End Sub

#End Region

End Class

