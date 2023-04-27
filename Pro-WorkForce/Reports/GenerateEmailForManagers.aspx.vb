Imports SmartV.UTILITIES
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports TA.Admin
Imports System.Data
Imports CrystalDecisions.ReportSource
Imports System.IO
Imports TA.Employees
Imports TA.Security

Partial Class Reports_GenerateEmailForManagers
    Inherits System.Web.UI.Page

#Region "Class Variables"
    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Dim cryRpt As New ReportDocument
    Dim objSmartSecurity As New SmartSecurity
    Private objOrgCompany As OrgCompany
    Private objSys_users As SYSUsers
    Private objEmployee As Employee
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

    Public Property UserName() As String
        Get
            Return ViewState("UserName")
        End Get
        Set(ByVal value As String)
            ViewState("UserName") = value
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
    Public Property Version() As String
        Get
            Return ViewState("Version")
        End Get
        Set(ByVal value As String)
            ViewState("Version") = value
        End Set
    End Property
    Public Property StartDate() As DateTime
        Get
            Return ViewState("StartDate")
        End Get
        Set(ByVal value As DateTime)
            ViewState("StartDate") = value
        End Set
    End Property
    Public Property EndDate() As DateTime
        Get
            Return ViewState("EndDate")
        End Get
        Set(ByVal value As DateTime)
            ViewState("EndDate") = value
        End Set
    End Property
    Public Shared ReadOnly Property BaseSiteUrl() As String
        Get
            Dim context As HttpContext = HttpContext.Current
            Dim baseUrl As String = context.Request.Url.Authority + context.Request.ApplicationPath.TrimEnd("/"c)
            Return baseUrl
        End Get
    End Property
    Public Property ReportId() As Integer
        Get
            Return ViewState("ReportId")
        End Get
        Set(ByVal value As Integer)
            ViewState("ReportId") = value
        End Set
    End Property
#End Region

#Region "Page Events"


    Protected Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload

        cryRpt.Close()
        cryRpt.Dispose()

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Dim ManagerId As Integer = Request.QueryString("ManagerId")
            ' Dim FromDate As Date = Request.QueryString("FromDate")
            ' Dim ToDate As Date = Request.QueryString("ToDate")
            Dim ReportType As String = Request.QueryString("ReportType")
            BindReport(ManagerId, ReportType)

        End If
    End Sub


#End Region

#Region "Methods"

    Private Sub SetReportName(ByVal rpttid As Integer, ByVal ManagerId As Integer, ByVal ReportType As String)
        Dim rptObj As New TA.Reports.Report
        Try
            UserName = "WFP Gererator Report"
            'rptObj.FROM_DATE = FromDate
            'rptObj.TO_DATE = Todate
            rptObj.EmployeeId = ManagerId
            rptObj.ManagerId = ManagerId
            rptObj.CompanyId = 0
            rptObj.EntityId = 0
            rptObj.WorkLocationId = 0
            rptObj.LogicalGroupId = 0
            rptObj.ReportType = ReportType
            ReportId = rpttid
            Select Case rpttid
                Case 1
                    RPTName = "rptEmpMove.rpt"
                    rptObj.ReportName = "Daily"
                    DT = rptObj.ManagerReportDynamic()

                    '   rptObj.Email_Configuration_Select_Date_Manager_Reports(StartDate, EndDate)

                Case 2
                    RPTName = "rptDetailedTransactions.rpt"
                    rptObj.ReportName = "Detailed"
                    DT = rptObj.ManagerReportDynamic()

                Case 3
                    RPTName = "rptSummary.rpt"
                    rptObj.ReportName = "Summary"
                    DT = rptObj.ManagerReportDynamic()

                Case 4
                    RPTName = "rptEmpAbsent.rpt"
                    rptObj.ReportName = "Absent"
                    DT = rptObj.ManagerReportDynamic()

                Case 5
                    RPTName = "rptViolations.rpt"
                    rptObj.ReportName = "Violations"
                    DT = rptObj.ManagerReportDynamic()

                Case 6
                    RPTName = "rptEmpSummaryAttendance.rpt"
                    rptObj.ReportName = "EmployeeSummaryAttendance"
                    DT = rptObj.ManagerReportDynamic()
                Case 7
                    RPTName = "rpt_MonthlyDeduction.rpt"
                    rptObj.ReportName = "Deduction"
                    DT = rptObj.ManagerReportDynamic()

                Case 8
                    RPTName = "rpt_DeductionPerPolicy.rpt"
                    rptObj.ReportName = "Dedut_PerPolicy"
                    DT = rptObj.ManagerReportDynamic()
            End Select

            Dim DefaultEmaiLang As String = ""
            objSys_users = New SYSUsers
            With objSys_users
                Dim dt As DataTable

                dt = .GetAllUSersByEmployeeID(ManagerId)
                If Not dt Is Nothing Then
                    If Not IsDBNull(dt.Rows(0)("DefaultEmaiLang")) Then
                        DefaultEmaiLang = dt.Rows(0)("DefaultEmaiLang")
                    End If
                End If
            End With

            If DefaultEmaiLang = "EN" Then
                Lang = CtlCommon.Lang.EN
                SessionVariables.CultureInfo = "en-US"
                RPTName = "English/" + RPTName
            Else
                Lang = CtlCommon.Lang.AR
                SessionVariables.CultureInfo = "ar-JO"
                RPTName = "Arabic/" + RPTName
            End If
            'Response.Write(RPTName)
        Catch ex As Exception

        End Try

    End Sub

    Public Sub BindReport(ByVal ManagerId As Integer, ByVal ReportType As String)
        Dim ReportFileName As String
        Dim FilesName As String = ""
        Try


            For i As Integer = 1 To 9 - 1
                Dim FromDate As Date
                Dim ToDate As Date = Now.Date
                If ReportType = "Weekly" Then
                    FromDate = Now.Date.AddDays(-8)
                Else
                    FromDate = Now.Date.AddMonths(-1)
                    FromDate = Date.ParseExact(FromDate.Month.ToString("00") + "/01/" + FromDate.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
                    Dim dd As New Date
                    dd = Date.ParseExact(FromDate.Month.ToString("00") + "/" + DateTime.DaysInMonth(FromDate.Year, FromDate.Month).ToString("00") + "/" + FromDate.Year.ToString("0000"), "MM/dd/yyyy", Nothing)


                    ToDate = dd
                End If

                SetReportName(i, ManagerId, ReportType)
                cryRpt = New ReportDocument
                cryRpt.Load(Server.MapPath(RPTName))
                cryRpt.SetDataSource(DT)
                Dim objAPP_Settings As New APP_Settings

                If Not ReportId = 6 Then

                    objEmployee = New Employee
                    objEmployee.EmployeeId = ManagerId
                    objEmployee.GetByPK()
                    objAPP_Settings.FK_CompanyId = objEmployee.FK_CompanyId
                    dt2 = objAPP_Settings.GetHeader()
                    dt2.Columns.Add("From_Date")
                    dt2.Columns.Add("To_Date")
                    dt2.Rows(0).Item("From_Date") = FromDate.Date.ToString("dd-MM-yyyy")
                    dt2.Rows(0).Item("To_Date") = ToDate.Date.ToString("dd-MM-yyyy")
                    cryRpt.Subreports(cryRpt.Subreports().Item(0).Name).SetDataSource(dt2)
                End If

                Dim ShowSTLogo As Boolean
                Dim RptTypeName As String = ""
                If Not ReportId = 6 Then
                    ShowSTLogo = ConfigurationManager.AppSettings("ShowSmartTimeLogo")
                    cryRpt.SetParameterValue("@ShowSTLogo", False)
                    cryRpt.SetParameterValue("@ShowSTLogo", False, "rptHeader")
                    cryRpt.SetParameterValue("@UserName", UserName)
                End If

                If ReportId = 1 Then
                    objAPP_Settings.GetByPK()
                    cryRpt.SetParameterValue("@TransactionConsideration", objAPP_Settings.ConsequenceTransactions)
                End If

                If ReportId = 6 Then
                    Dim BeginOfYear As DateTime
                    Dim rpt_ToDate As DateTime

                    BeginOfYear = Date.ParseExact("01" + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
                    rpt_ToDate = Now.Date.AddDays(-8)
                    Dim saturday As Date = Date.Today
                    While (saturday.DayOfWeek <> DayOfWeek.Saturday)
                        saturday = saturday.AddDays(-1)
                    End While
                    rpt_ToDate = saturday
                    cryRpt.SetParameterValue("@BeginOfYear", BeginOfYear)
                    cryRpt.SetParameterValue("@ToDate", rpt_ToDate)

                End If

                '----------To Enable Colors in Violation Report---------'
                If ReportId = 5 Then
                    cryRpt.SetParameterValue("@IsColored", False)
                End If
                '----------To Enable Colors in Violation Report---------'
                Select Case i
                    Case 1
                        RptTypeName = "Daily"
                        cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "التقرير اليومي للموظف", "Employee Daily Report"))
                        cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "التقرير اليومي للموظف", "Employee Daily Report"), "rptHeader")
                    Case 2
                        RptTypeName = "Detailed"
                        cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "تقرير الحركات المفصلة", "Detailed Transactions"))
                        cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "تقرير الحركات المفصلة", "Detailed Transactions"), "rptHeader")
                    Case 3
                        RptTypeName = "Summary"
                        cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "تقرير الملخص", "Summary Report"))
                        cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "تقرير الملخص", "Summary Report"), "rptHeader")
                    Case 4
                        RptTypeName = "Absent"
                        cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "غيابات الموظفين", "Absent Employees Report"))
                        cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "غيابات الموظفين", "Absent Employees Report"), "rptHeader")
                    Case 5
                        RptTypeName = "Violations"
                        cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "تقرير  المخالفات ", "Violation Report"))
                        cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "تقرير  المخالفات ", "Violation Report"), "rptHeader")
                    Case 6
                        RptTypeName = "EmployeeAttendance"
                        'cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "تقرير  ملخص الموظفين ", "Employee Summary Attendance"))
                        'cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "تقرير  ملخص الموظفين ", "Employee Summary Attendance"), "rptHeader")
                    Case 7
                        RptTypeName = "Deduction"
                        cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "تقرير الاقتطاعات الشهرية ", "Monthly Deduction Report"))
                        cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "تقرير الاقتطاعات الشهرية ", "Monthly Deduction Report"), "rptHeader")
                    Case 8
                        RptTypeName = "Monthly_Lost_Hours_Deduct"
                        cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "تقرير اقتطاعات الوقت الضائع الشهري", "Monthly Lost Hours Deduction"))
                        cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "تقرير اقتطاعات الوقت الضائع الشهري", "Monthly Lost Hours Deduction"), "rptHeader")
                End Select

                If Not ReportId = 6 Then
                    cryRpt.SetParameterValue("@FromDate", FromDate.ToShortDateString())
                    cryRpt.SetParameterValue("@ToDate", ToDate.ToShortDateString())
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
                        Dim objUser As New Employee
                        objOrgCompany = New OrgCompany
                        objUser.EmployeeId = ManagerId
                        objUser.GetByPK()
                        objOrgCompany.CompanyId = objUser.FK_CompanyId

                        If Not (objOrgCompany.CompanyId = 0) Then
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
                End If

                CRV.ReportSource = cryRpt
                ReportFileName = "MGR_" & RptTypeName & "_" & Now.Year & Now.Month & Now.Day & "_" & Now.Hour & Now.Minute & Now.Second & Now.Millisecond
                'Response.Write(ReportFileName)
                If Not DT Is Nothing Then
                    If DT.Rows.Count > 0 Then
                        Dim FilePath As String = String.Empty
                        FilePath = Server.MapPath("..\GenerateReportFiles\\" + ReportFileName + ".pdf")
                        'Response.Write(FilePath)
                        Dim CrExportOptions As ExportOptions
                        Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions()
                        Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions()

                        CrDiskFileDestinationOptions.DiskFileName = FilePath
                        CrExportOptions = cryRpt.ExportOptions
                        With CrExportOptions
                            .ExportDestinationType = ExportDestinationType.DiskFile
                            .ExportFormatType = ExportFormatType.PortableDocFormat
                            .DestinationOptions = CrDiskFileDestinationOptions
                            .FormatOptions = CrFormatTypeOptions
                            FilesName = ReportFileName & "," & FilesName

                        End With
                        cryRpt.Export()
                    Else
                        'CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NoDataFound", CultureInfo))
                    End If
                Else
                    'CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NoDataFound", CultureInfo))
                End If
                cryRpt.Close()
                cryRpt.Dispose()
            Next
            Response.Write(FilesName)

        Catch ex As Exception
            Dim err As String = ""
            err = ex.Message
        End Try
    End Sub

    Private Sub ClearCache()
        For Each entry As System.Collections.DictionaryEntry In HttpContext.Current.Cache
            HttpContext.Current.Cache.Remove(entry.Key.ToString)
        Next
    End Sub

#End Region


End Class
