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
Imports System.Web.Configuration

Partial Class Reports_ReportViewer
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objAPP_Settings As APP_Settings
    Private Lang As CtlCommon.Lang
    Protected dir As String
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
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

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "التقرير اليومي", "Daily Report")
                Case 2

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "قائمة الموظفين", "Employees List")
                Case 3

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "حركات الحضور", "Attendance Transactions")
                Case 4

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "إجازات الموظفين", "Employees Leaves")

                Case 5 ' Absent

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير الغياب", "Absent Employees")

                Case 6 ' Extra

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "الساعات الإضافية", "Employees Extra Hours")

                Case 7 ' Violations

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "المخالفات", "Employees Violations")

                Case 8 ' IN-OUT

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير حركات الدخول و الخروج", "In Out Transactions")

                Case 9 ' Summary

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير ملخص الحضور", "Attendance Summary")
                Case 10

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "قائمة الموظفين غير المفعلين", "Terminated Employees List")
                Case 11

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير الاجازات", "Leaves Report")
                Case 12

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير المغادرات", "Permissions Report")
                Case 13

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير رصيد الاجازات المنتهية", " Expired Leaves Balance Report")
                Case 14

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير رصيد الاجازات ", " Leaves Balance Report")
                Case 15

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير مغادرات الرضاعة ", " Nursing Permissions Report")
                Case 16

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير مغادرات الدراسة ", " Study Permissions Report")
                Case 17

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير مخالفات سياسة الحضور و الانصراف ", " TA Violations")
                Case 18

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير الغيابات المنطقية ", " Logical Absent")
                Case 19

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير الحركات الخاطئة ", " Invalid Attempts")
                Case 20

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير ملخص الحضور مع ساعات العمل المتوقعة ", " Summary Report With Expected")
                Case 21

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير نائب المدير ", " Deputy Manager")
                Case 22

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير  الحركات المفصلة ", " Detailed Transaction")
                Case 23

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير التأخير ", " Delay Report")
                Case 24

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير الخروج المبكر ", " Early Out Report")
                Case 25

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير الحركات غير المكتملة ", " Incomplete Report")
                Case 26

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير الحركات اليدوية ", " Manual Entry Report")
                Case 27

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير مخالفات سياسة الحضور و الانصراف التفصيلي ", " Detailed TA Violations")
                Case 28

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير الحركات الخاطئة ", " Morpho Invalid Attempts")
                Case 29

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "التقرير العام للبصمة ", "General Attendance Report")
                Case 30

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير احصائية البصمة", " Attendance Statistices Report")
                Case 31

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير ساعات / ايام العمل ", " Work Hours \ Days")
                Case 32

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "ملخص الغياب", "Summary Employee Absent")
                Case 33

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "ملخص الاجازات", "Summary Employee Leaves")
                Case 34

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "استثناءات الحضور", "Employee TA Exeptions")
                Case 35

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "طلبات المغادرة التفصيلي", "Detailed Permission Requests")
                Case 36

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "رؤساء المناوبات", "Shift Managers")
                Case 37

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير حركة البوابات", "Gates Transaction Report")
                Case 38

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير حركة البوابات التفصيلي", "Detailed Gates Transaction Report")
                Case 39

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "ملخص الادخال اليدوي", "Manual Entry Summary")
                Case 40

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "الاقتطاعات الشهرية", "Monthly Deduction")
                Case 41

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "الوقت الاضافي ماعدا الوقت الضائع", "Overtime Deduct Lost Time")
                Case 42

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "جداول الموظفين", "Employee Schdules")
                    RadDatePicker1.Visible = False
                    RadDatePicker2.Visible = False
                    lblFromDate.Visible = False
                    lblToDate.Visible = False
                Case 43

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير ملخص الحضور 2", "Attendance Summary 2")
                Case 44

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "2 تقرير نظام الحضور والانصراف", "Employee Time and Attendance 2")

                Case 45

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "2 التقرير اليومي", "Daily Report 2")
                Case 46
                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "حركات القارئ", "Reader Attendance")
            End Select

            If rptid = 1 Then
                RadDatePicker1.SelectedDate = Date.Today
                RadDatePicker2.SelectedDate = Date.Today
            Else
                RadDatePicker1.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
                Dim dd As New Date
                dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)

                RadDatePicker2.SelectedDate = dd
            End If

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
        If EmployeeFilter1.IsLevelRequired Then
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            If EmployeeFilter1.CompanyId = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SelectCompany", CultureInfo), "info")
            ElseIf EmployeeFilter1.EntityId = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SelectEntity", CultureInfo), "info")
            Else
                Dim Id As Integer = Request.QueryString("id")
                BindReport(Id)
            End If
        Else
            Dim Id As Integer = Request.QueryString("id")
            BindReport(Id)
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

        SetReportName(Request.QueryString("ReportId"))

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
        If Request.QueryString("ReportId") = 19 Then
            cryRpt.SetParameterValue("@URL", BaseSiteUrl)
        ElseIf Request.QueryString("ReportId") = 11 Then
            cryRpt.SetParameterValue("@URL", BaseSiteUrl)
        ElseIf Request.QueryString("ReportId") = 12 Then
            cryRpt.SetParameterValue("@URL", BaseSiteUrl)
        ElseIf Request.QueryString("ReportId") = 15 Then
            cryRpt.SetParameterValue("@URL", BaseSiteUrl)
        ElseIf Request.QueryString("ReportId") = 16 Then
            cryRpt.SetParameterValue("@URL", BaseSiteUrl)
        ElseIf Request.QueryString("ReportId") = 28 Then
            cryRpt.SetParameterValue("@URL", BaseSiteUrl)
        ElseIf Request.QueryString("ReportId") = 33 Then
            cryRpt.SetParameterValue("@URL", BaseSiteUrl)
        End If

        Dim ShowSTLogo As Boolean
        ShowSTLogo = ConfigurationManager.AppSettings("ShowSmartTimeLogo")
        cryRpt.SetParameterValue("@ShowSTLogo", False)
        cryRpt.SetParameterValue("@ShowSTLogo", False, "rptHeader")

        cryRpt.SetParameterValue("@UserName", UserName)
        'cryRpt.SetParameterValue("@CopyRight", CopyRight)
        If SessionVariables.CultureInfo = "ar-JO" Then
            CopyRightAr = objSmartSecurity.strSmartDecrypt(ConfigurationManager.AppSettings("CopyRightAr").ToString(), "SmartVision")
            cryRpt.SetParameterValue("@CopyRightAr", CopyRightAr)
        Else
            CopyRightEn = objSmartSecurity.strSmartDecrypt(ConfigurationManager.AppSettings("CopyRightEn").ToString(), "SmartVision")
            cryRpt.SetParameterValue("@CopyRightEn", CopyRightEn)

        End If
        CRV.ReportSource = cryRpt
        If Not DT Is Nothing Then
            If DT.Rows.Count > 0 Then
                If rblFormat.SelectedValue = 1 Then
                    'ExportDataSetToPDF(DT, "ExportedReport")
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
        'If chkPDF.Checked = True Then
        '    cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedReport")
        'End If


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
        UserName = SessionVariables.LoginUser.UsrID
        'CopyRight = objSmartSecurity.strSmartDecrypt(ConfigurationManager.AppSettings("CopyRightEn").ToString(), "SmartVision")

        If Not RadDatePicker1.SelectedDate Is Nothing Then
            rptObj.FROM_DATE = RadDatePicker1.SelectedDate
        End If
        If Not RadDatePicker2.SelectedDate Is Nothing Then
            rptObj.TO_DATE = RadDatePicker2.SelectedDate
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


        rpttid = IIf(rpttid = 0, 3, rpttid)

        Select Case rpttid
            Case 1
                RPTName = "rptEmpMove.rpt"
                DT = rptObj.GetFilterdEmpMove()
            Case 2
                RPTName = "rptEmployeeList.rpt"
                DT = rptObj.GetFilterdEmpList()
            Case 3
                RPTName = "rptDetEmpMove.rpt"
                DT = rptObj.GetFilterdDetEmpMove()

                'Case 4
                '    RPTName = "RptLeaves.rpt"
                '    DT = rptObj.GetEmpLeaves()

            Case 5 ' Absent'
                RPTName = "rptEmpAbsent.rpt"
                DT = rptObj.GetEmpAbsent()

            Case 6 ' Extra'
                RPTName = "crExtraHours.rpt"
                DT = rptObj.GetExtraHours()

            Case 7 ' Violations'
                RPTName = "rptViolations.rpt"
                DT = rptObj.GetViolations()

            Case 8 ' In Out'
                RPTName = "rptEmpInOut.rpt"
                DT = rptObj.GetEmpInOut()
            Case 9 ' Summary'
                RPTName = "rptSummary.rpt"
                DT = rptObj.GetSummary()
                'Case 10
                '    RPTName = "rptTerminatedEmployeeList.rpt"
                '    DT = rptObj.GetFilterdTerminatedEmpList()
            Case 11 ' Leaves'
                RPTName = "rptEmpLeaves.rpt"
                DT = rptObj.GetEmpLeaves
            Case 12 ' Permission'
                RPTName = "rptEmpPermissions.rpt"
                DT = rptObj.GetEmpPermissions
            Case 13 ' Expired Balance'
                RPTName = "rptEmp_BalanceExpire.rpt"
                DT = rptObj.GetEmpExpireBalance
            Case 14 ' Leave Balance'
                RPTName = "rptEmp_Balance.rpt"
                DT = rptObj.GetEmpBalance
            Case 70 ' Leave Balance DOF'
                RPTName = "rptEmp_Balance_DOF.rpt"
                DT = rptObj.GetEmpBalance_DOF
            Case 15 ' Nursing Permission'
                RPTName = "rptNursingPermissions.rpt"
                DT = rptObj.GetNursing_Permission
            Case 16 ' Study Permission'
                RPTName = "rptStudyPermissions.rpt"
                DT = rptObj.GetStudy_Permission
            Case 17 ' TA Violations'
                RPTName = "rptTA_Violations.rpt"
                DT = rptObj.GetTA_Violations
            Case 18 ' Logical Absent'
                RPTName = "rptLogicalAbsent.rpt"
                DT = rptObj.GetEmp_LogicalAbsent
            Case 19 ' Invalid Attempts'
                RPTName = "rptEmp_InvalidAttempts.rpt"
                DT = rptObj.GetEmp_InvalidAttempts
            Case 20 ' Summary With Expected'
                RPTName = "rptSummary_WithExpected.rpt"
                DT = rptObj.GetSummary_WithExpected
            Case 21
                RPTName = "rptDeputyManager.rpt"
                DT = rptObj.GetDeputy_Manager
            Case 22
                RPTName = "rptDetailedTransactions.rpt"
                DT = rptObj.GetDetailed_Transactions
            Case 23
                RPTName = "rptEmp_Delay.rpt"
                DT = rptObj.GetDelay
            Case 24
                RPTName = "rptEmp_EarlyOut.rpt"
                DT = rptObj.GetEarlyOut
            Case 25
                RPTName = "rptEmpIncompleteTrans.rpt"
                DT = rptObj.Get_IncompleteTrans
            Case 26
                RPTName = "rptManualEntry.rpt"
                DT = rptObj.GetManaualEntry
            Case 27
                RPTName = "rptDetailedTA_Violations.rpt"
                DT = rptObj.Get_DetailedTAViolation
            Case 28
                RPTName = "rptEmp_Morpho_InvalidAttempts.rpt"
                DT = rptObj.GetEmp_Morpho_InvalidAttempts
            Case 29
                RPTName = "rpt_GeneralAttendanceReport.rpt"
                DT = rptObj.GetLate_Students
            Case 30
                RPTName = "rpt_AttendanceStatisticesReport.rpt"
                DT = rptObj.GetSummary_Students_Status
            Case 31
                RPTName = "rpt_WorkHrs.rpt"
                DT = rptObj.GetWork_Hrs
            Case 32 ' Summary Absent'
                RPTName = "rptEmpSummaryAbsent.rpt"
                DT = rptObj.GetEmpAbsent()
            Case 33 ' Summary Leaves'
                RPTName = "rptEmpSummaryLeaves.rpt"
                DT = rptObj.GetEmpLeaves
            Case 34 'EmpTAExceptions
                RPTName = "rptEmpTAExceptions.rpt"
                DT = rptObj.Get_EmpTAExceptions
            Case 35 'DetailedPermissionRequests
                RPTName = "rptDetailedPermissionRequests.rpt"
                DT = rptObj.Get_DetailedPermissions
            Case 36 'ShiftManagers
                RPTName = "rpt_ShiftManager.rpt"
                DT = rptObj.Get_ShiftManagers
            Case 37 'rpt_DNA_InOut
                RPTName = "rpt_DNA_InOut.rpt"
                DT = rptObj.Get_DNA_InOut
            Case 38 'rpt_DNA_DetailedTrans
                RPTName = "rpt_DNA_DetailedTrans.rpt"
                DT = rptObj.Get_DNA_DetailedTrans
            Case 39 'rptManualEntry_Summary
                RPTName = "rptManualEntry_Summary.rpt"
                DT = rptObj.GetManualEntry_Summary
            Case 40 'rpt_MonthlyDeduction
                RPTName = "rpt_MonthlyDeduction.rpt"
                DT = rptObj.Get_MonthlyDeduction
            Case 41 'rptOvertime_DeductLost
                RPTName = "rptOvertime_DeductLost.rpt"
                DT = rptObj.Get_Overtime_DeductLostTime
            Case 42 'rptEmp_Schedules
                RPTName = "rptEmp_Schedules.rpt"
                DT = rptObj.GetEmployeeSchdules
            Case 43 'rpt_Summary2
                RPTName = "rptSummary2.rpt"
                DT = rptObj.GetSummary
            Case 44 'rptEmpTimeAttendance
                RPTName = "rptEmpTimeAttendance_.rpt"
                DT = rptObj.GetFilterdEmpMove2
            Case 45 'Daily Report 2
                RPTName = "DailyReport2.rpt"
                DT = rptObj.GetFilterdEmpMove()

        End Select
        If (Lang = CtlCommon.Lang.AR) Then
            RPTName = "Arabic/" + RPTName
        Else
            RPTName = "English/" + RPTName
        End If

    End Sub

    Private Sub FillExcelreport()
        Dim rptid As Integer = Request.QueryString("ReportId")
        NDT = New DataTable
        NDT = DT.Clone()
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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "الوقت الاضافي"

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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "OverTime"
                End If

            Case 2 'rptEmployeeList

                NDT.Columns(12).DataType = System.Type.GetType("System.String")
                NDT.Columns(13).DataType = System.Type.GetType("System.String")

                For Each row As DataRow In DT.Rows
                    Dim dr As DataRow = NDT.NewRow
                    For i As Integer = 0 To NDT.Columns.Count - 1
                        dr(i) = row(i)
                        If i = 12 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
                            End If
                        ElseIf i = 13 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
                            End If
                        End If
                    Next
                    NDT.Rows.Add(dr)
                Next

                NDT.Columns.RemoveAt(0)
                NDT.Columns.RemoveAt(3)
                NDT.Columns.RemoveAt(5)
                NDT.Columns.RemoveAt(7)
                NDT.Columns.RemoveAt(7)
                NDT.Columns.RemoveAt(12)
                NDT.Columns.RemoveAt(12)
                NDT.Columns.RemoveAt(12)

                If Lang = CtlCommon.Lang.AR Then
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns(1).ColumnName = "إسم الموظف"
                    NDT.Columns(2).ColumnName = "إسم الموظف باللغه العربيه"
                    NDT.Columns(3).ColumnName = "إسم وحدة العمل"
                    NDT.Columns(4).ColumnName = "إسم وحدة العمل باللغه العربيه"
                    NDT.Columns(5).ColumnName = "إسم الشركه"
                    NDT.Columns(6).ColumnName = "إسم الشركه باللغه العربيه"
                    NDT.Columns(7).ColumnName = "تاريخ الميلاد"
                    NDT.Columns(8).ColumnName = "تاريخ الالتحاق بالعمل"
                    NDT.Columns(9).ColumnName = "البريد الإلكتروني"
                    NDT.Columns(10).ColumnName = "الجنس"
                    NDT.Columns(11).ColumnName = "ملاحظات"
                    NDT.Columns(12).ColumnName = "الجنسيه"
                    NDT.Columns(13).ColumnName = "الجنسيه باللغه العربيه"
                Else
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns(2).ColumnName = "Employee Arabic Name"
                    NDT.Columns(3).ColumnName = "Entity Name"
                    NDT.Columns(4).ColumnName = "Entity Arabic Name"
                    NDT.Columns(5).ColumnName = "Company Name"
                    NDT.Columns(6).ColumnName = "Company Arabic Name"
                    NDT.Columns(7).ColumnName = "Date Of Birth"
                    NDT.Columns(8).ColumnName = "Join Date"
                    NDT.Columns(9).ColumnName = "Email"
                    NDT.Columns(10).ColumnName = "Gender"
                    NDT.Columns(11).ColumnName = "Remarks"
                    NDT.Columns(12).ColumnName = "Nationality Name"
                    NDT.Columns(13).ColumnName = "Nationality Arabic Name"
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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                Else
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                Else
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                    NDT.Columns(3).ColumnName = "اسم وحدة العمل"
                    NDT.Columns(4).ColumnName = "اسم وحدة العمل باللغة العربية"
                    'NDT.Columns(5).ColumnName = "اسم الشركة"
                    'NDT.Columns(6).ColumnName = "اسم الشركة باللغة العربية"

                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)

                    'NDT.Columns(7).ColumnName = "تاريخ الحركة"

                    NDT.Columns(5).ColumnName = "التاريخ"

                    'NDT.Columns.RemoveAt(8)

                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    'NDT.Columns(8).ColumnName = "الحالة"

                    NDT.Columns(6).ColumnName = "الملاحظات"

                    'NDT.Columns(10).ColumnName = "السبب"
                    'NDT.Columns(11).ColumnName = "الوقت الاضافي"
                    'NDT.Columns.RemoveAt(12)
                    'NDT.Columns(12).ColumnName = "مجموع ساعات العمل"
                    'NDT.Columns.RemoveAt(13)
                    'NDT.Columns(13).ColumnName = "التأخير"
                    'NDT.Columns(14).ColumnName = "الخروج المبكر"


                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)


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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                    NDT.Columns(3).ColumnName = "Entity Name"
                    NDT.Columns(4).ColumnName = "Entity Arabic Name"
                    'NDT.Columns(5).ColumnName = "Company Name"
                    'NDT.Columns(6).ColumnName = "Company Arabic Name"

                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)

                    NDT.Columns(5).ColumnName = "Date"

                    'NDT.Columns.RemoveAt(8)

                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    'NDT.Columns(8).ColumnName = "Status"

                    'NDT.Columns(9).ColumnName = "Remarks"
                    NDT.Columns(6).ColumnName = "Remarks"

                    'NDT.Columns(10).ColumnName = "Reason"
                    'NDT.Columns(11).ColumnName = "OverTime"
                    'NDT.Columns.RemoveAt(12)
                    'NDT.Columns(12).ColumnName = "Total Work Hours"
                    'NDT.Columns.RemoveAt(13)
                    'NDT.Columns(13).ColumnName = "Delay"
                    'NDT.Columns(14).ColumnName = "Early Out"

                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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

            Case 9 'rptSummary

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

            Case 11 'rptEmpLeaves

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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
                                End If
                            ElseIf i = 16 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
                                End If
                            ElseIf i = 16 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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

            Case 12 'rptEmpPermission

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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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

            Case 15 'RptNursing_Perm

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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
                                End If
                            ElseIf i = 14 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
                                End If
                            ElseIf i = 14 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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

            Case 16 'RptStudy_Perm

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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
                                End If
                            ElseIf i = 14 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
                                End If
                            ElseIf i = 14 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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

                End If

            Case 17 'rpt_TAViolations
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(10).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 10 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                    NDT.Columns(7).ColumnName = "تاريخ الانتهاك"
                    NDT.Columns(8).ColumnName = "اسم سياسة الحضور"
                    NDT.Columns(9).ColumnName = "اسم سياسة الحضور باللغة العربية"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "اسم الانتهاك "
                    NDT.Columns(11).ColumnName = "اسم الانتهاك باللغة العربية"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "اسم الاجراء"
                    NDT.Columns(13).ColumnName = "اسم الاجراء باللغة العربية"

                Else
                    NDT.Columns(10).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 10 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                    NDT.Columns(7).ColumnName = "Violation Date"
                    NDT.Columns(8).ColumnName = "Applied Policy Name"
                    NDT.Columns(9).ColumnName = "Applied Policy Arabic Name"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Violation Description"
                    NDT.Columns(11).ColumnName = "Violation Arabic Description"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "Violation Action"
                    NDT.Columns(13).ColumnName = "Violation Action Arabic Name"
                End If

            Case 18 'Rpt_LogicalAbsent
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(7).DataType = System.Type.GetType("System.String")
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 7 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
                                End If
                            ElseIf i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "اسم الموظف باللغة العربية"
                    NDT.Columns(3).ColumnName = "اسم وحدة العمل"
                    NDT.Columns(4).ColumnName = "اسم وحدة العمل باللغة العربية"
                    NDT.Columns(5).ColumnName = "اسم الشركة"
                    NDT.Columns(6).ColumnName = "اسم الشركة باللغة العربية"
                    NDT.Columns(7).ColumnName = "اسم القاعدة"
                    NDT.Columns(8).ColumnName = "اسم القاعدة باللغة العربية"
                    NDT.Columns(9).ColumnName = "تاريخ البداية"
                    NDT.Columns(10).ColumnName = "تاريخ الانتهاء"
                    NDT.Columns(11).ColumnName = "تاريخ الانتهاك"
                    NDT.Columns(12).ColumnName = "اسم سياسة الحضور"
                    NDT.Columns(13).ColumnName = "اسم سياسة الحضور باللغة العربية"
                Else
                    NDT.Columns(7).DataType = System.Type.GetType("System.String")
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 7 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
                                End If
                            ElseIf i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                    NDT.Columns(7).ColumnName = "Rule Name"
                    NDT.Columns(8).ColumnName = "Rule Arabic Name"
                    NDT.Columns(9).ColumnName = "Start Date"
                    NDT.Columns(10).ColumnName = "End Date"
                    NDT.Columns(11).ColumnName = "Violation Date"
                    NDT.Columns(12).ColumnName = "TA Policy Name"
                    NDT.Columns(13).ColumnName = "TA Policy Arabic Name"
                End If

            Case 19 'rpt_InvalidAttempts
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(14).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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

            Case 20 'rptSummary_WithExpeted

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

            Case 21 'rptDeputyManager

                NDT.Columns(6).DataType = System.Type.GetType("System.String")
                NDT.Columns(7).DataType = System.Type.GetType("System.String")
                For Each row As DataRow In DT.Rows
                    Dim dr As DataRow = NDT.NewRow
                    For i As Integer = 0 To NDT.Columns.Count - 1
                        dr(i) = row(i)
                        If i = 6 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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

            Case 22 'rptDetailedTransactions

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
                                dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 9 Then
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
                End If


            Case 23
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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                    NDT.Columns(6).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(7)
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
            Case 24
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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                    NDT.Columns(6).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(7)
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
            Case 25
                NDT.Columns(8).DataType = System.Type.GetType("System.String")
                NDT.Columns(9).DataType = System.Type.GetType("System.String")
                NDT.Columns(10).DataType = System.Type.GetType("System.String")
                For Each row As DataRow In DT.Rows
                    Dim dr As DataRow = NDT.NewRow
                    For i As Integer = 0 To NDT.Columns.Count - 1
                        dr(i) = row(i)
                        If i = 8 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                End If

            Case 26

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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                    NDT.Columns(6).ColumnName = "Entry User Name"
                    NDT.Columns(7).ColumnName = "Entry Date Time"
                    NDT.Columns(8).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)

                End If
            Case 27

                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(3).DataType = System.Type.GetType("System.String")
                    NDT.Columns(4).DataType = System.Type.GetType("System.String")
                    NDT.Columns(5).DataType = System.Type.GetType("System.String")
                    NDT.Columns(6).DataType = System.Type.GetType("System.String")
                    NDT.Columns(7).DataType = System.Type.GetType("System.String")

                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 3 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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

                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "التاريخ"
                    NDT.Columns(3).ColumnName = "وقت الدخول"
                    NDT.Columns(4).ColumnName = "التأخير"
                    NDT.Columns(5).ColumnName = "وقت الخروج"
                    NDT.Columns(6).ColumnName = "الخروج المبكر"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "نوع الانتهاك"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "اسم الاجراء"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(13)

                Else
                    NDT.Columns(3).DataType = System.Type.GetType("System.String")
                    NDT.Columns(4).DataType = System.Type.GetType("System.String")
                    NDT.Columns(5).DataType = System.Type.GetType("System.String")
                    NDT.Columns(6).DataType = System.Type.GetType("System.String")
                    NDT.Columns(7).DataType = System.Type.GetType("System.String")

                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 3 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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

                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Date"
                    NDT.Columns(3).ColumnName = "In Time"
                    NDT.Columns(4).ColumnName = "Delay"
                    NDT.Columns(5).ColumnName = "Out Time"
                    NDT.Columns(6).ColumnName = "Early Out"
                    NDT.Columns(7).ColumnName = "Violation Name"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Action Name"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(14)
                End If
            Case 28

                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(4).DataType = System.Type.GetType("System.String")
                    NDT.Columns(5).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)

                            If i = 4 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 5 Then
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
                    NDT.Columns(2).ColumnName = "التاريخ"
                    NDT.Columns(3).ColumnName = "الوقت"
                    NDT.Columns(4).ColumnName = "الجهاز"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "وحدة العمل"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "الشركة"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)


                Else
                    NDT.Columns(4).DataType = System.Type.GetType("System.String")
                    NDT.Columns(5).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)

                            If i = 4 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 5 Then
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
                    NDT.Columns(2).ColumnName = "Date"
                    NDT.Columns(3).ColumnName = "Time"
                    NDT.Columns(4).ColumnName = "Reader"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                End If
            Case 29

                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(3).DataType = System.Type.GetType("System.String")
                    NDT.Columns(4).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)

                            If i = 3 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortDateString
                                End If
                            ElseIf i = 4 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns(0).ColumnName = "رقم الطالب"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الطالب"
                    NDT.Columns(2).ColumnName = "التاريخ"
                    NDT.Columns(3).ColumnName = "وقت الدخول"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "وحدة العمل"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)

                Else
                    NDT.Columns(3).DataType = System.Type.GetType("System.String")
                    NDT.Columns(4).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)

                            If i = 3 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortDateString
                                End If
                            ElseIf i = 4 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns(0).ColumnName = "Student No."
                    NDT.Columns(1).ColumnName = "Student Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Date"
                    NDT.Columns(3).ColumnName = "In Time"
                    NDT.Columns(4).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                End If
            Case 30
                NDT = DT
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(0).ColumnName = "رقم الطالب"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الطالب"
                    NDT.Columns(2).ColumnName = "عدد الطلاب"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "عدد الطلاب الحضور"
                    NDT.Columns(4).ColumnName = "عدد الطلاب الغياب"
                    NDT.Columns(5).ColumnName = "عدد الطلاب المجازين"
                    NDT.Columns(6).ColumnName = "عدد الطلاب المتأخرين"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                Else
                    NDT.Columns(0).ColumnName = "Student No."
                    NDT.Columns(1).ColumnName = "Student Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Total Of The Students"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "Total Of Attend Students"
                    NDT.Columns(4).ColumnName = "Total Of Absent Students"
                    NDT.Columns(5).ColumnName = "Total Of Leave Students"
                    NDT.Columns(6).ColumnName = "Total Of Late Students"
                    NDT.Columns(7).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                End If
            Case 31
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

            Case 32 'RptEmpSummaryAbsent

                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                    NDT.Columns(6).ColumnName = "مجموع الغياب"
                Else
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                    NDT.Columns(6).ColumnName = "Total Absent"

                End If

            Case 33 'rptEmpSummaryLeaves

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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
                                End If
                            ElseIf i = 16 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
                                End If
                            ElseIf i = 16 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
            Case 34 'rptEmpTAException

                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(4).DataType = System.Type.GetType("System.String")
                    NDT.Columns(5).DataType = System.Type.GetType("System.String")


                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 4 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
                                End If
                            ElseIf i = 5 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
                                End If
                            ElseIf i = 5 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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

            Case 35 'rptDetailedPermissionRequests

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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
            Case 36 'rpt_ShiftManager

                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(4).DataType = System.Type.GetType("System.String")
                    NDT.Columns(5).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 4 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
                                End If
                            ElseIf i = 5 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
                                End If
                            ElseIf i = 5 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
            Case 37 'rpt_DNA_InOut

                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(7).DataType = System.Type.GetType("System.String")
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 7 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
                                End If
                            ElseIf i = 8 Then
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
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "تاريخ الحركة"
                    NDT.Columns(3).ColumnName = "وقت الحركة"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "اسم الشركة"

                Else
                    NDT.Columns(7).DataType = System.Type.GetType("System.String")
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 7 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
                                End If
                            ElseIf i = 8 Then
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
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Move Date"
                    NDT.Columns(3).ColumnName = "Move Time"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(6)
                End If
            Case 38 'rpt_DNA_DetailedTrans

                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(4).DataType = System.Type.GetType("System.String")
                    NDT.Columns(5).DataType = System.Type.GetType("System.String")
                    NDT.Columns(6).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 4 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "التاريخ"
                    NDT.Columns(3).ColumnName = "وقت الدخول"
                    NDT.Columns(4).ColumnName = "وقت الخروج"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "اسم الشركة"

                Else
                    NDT.Columns(4).DataType = System.Type.GetType("System.String")
                    NDT.Columns(5).DataType = System.Type.GetType("System.String")
                    NDT.Columns(6).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 4 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Date"
                    NDT.Columns(3).ColumnName = "In Time"
                    NDT.Columns(4).ColumnName = "Out Time"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(7)
                End If

            Case 39 'rptManualEntry_Summary
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
                End If

            Case 40 'rpt_MonthlyDeduction
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(14).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 14 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                    NDT.Columns(5).ColumnName = "مجموع التأخير و الخروج المبكر"
                    NDT.Columns(6).ColumnName = "عدد مرات التأخير و الخروج المبكر"
                    NDT.Columns(7).ColumnName = "عدد مرات لم يسجل دخول"
                    NDT.Columns(8).ColumnName = "عدد مرات لم يسجل خروج"
                    NDT.Columns(9).ColumnName = "لم يستكمل 50% من الدوام الرسمي"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "تم الاعتماد من قبل"
                    NDT.Columns(11).ColumnName = "تاريخ الاعتماد"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "اسم الشركة"

                Else
                    NDT.Columns(14).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 14 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Year"
                    NDT.Columns(3).ColumnName = "Month"
                    NDT.Columns(4).ColumnName = "Absent Count"
                    NDT.Columns(5).ColumnName = "Total Delay and Early Out"
                    NDT.Columns(6).ColumnName = "Delay and Early Out Count"
                    NDT.Columns(7).ColumnName = "Missing In Count"
                    NDT.Columns(8).ColumnName = "Missing Out Count"
                    NDT.Columns(9).ColumnName = "Uncomplete 50% Work Hours"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Approved By"
                    NDT.Columns(11).ColumnName = "Approval Date"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(14)

                End If

            Case 41 'rptOvertime_DeductLost.rpt
                NDT = DT

            Case 42 'rptEmp_Schedules

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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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
                                    dr(i) = DateTime.ParseExact(row(i), "dd/MM/yyyy", Nothing)
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

        End Select

    End Sub

#End Region

End Class

