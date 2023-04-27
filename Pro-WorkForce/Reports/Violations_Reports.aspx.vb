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

Partial Class Reports_SelfServices_Violations_Reports
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objAPP_Settings As APP_Settings
    Private objEmployee_TAPolicy As Employee_TAPolicy
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


    Private EmployeesViolationsReport As String = "Employees Violations Report"
    Private TA_Violation As String = "TA_Violation"
    Private TAPolicyViolations As String = "TA Policy Violations"
    Private ApproverViolationsToERP As String = "Approve Violations to ERP Report"
    Private AdvanceViolationReport As String = "AdvanceViolationReport"
    Private EmployeesViolationsReport_Senaat As String = "EmployeesViolationsReport_Senaat"
    Private EmployeesViolationsReport2 As String = "EmployeesViolationsReport2"

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
            lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقارير المخالفات", "Violations Reports")
            btnPrint.Text = IIf(Lang = CtlCommon.Lang.AR, "انشاء", "Generate")


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

        End If

    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        cryRpt.Close()
        cryRpt.Dispose()

    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim reportName As String = RadComboViolationsReports.SelectedValue

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

    Protected Sub RadComboViolationsReports_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboViolationsReports.SelectedIndexChanged
        If RadComboViolationsReports.SelectedValue = AdvanceViolationReport Then
            dvViolation_Selection.Visible = True
            dvDelayPolicy.Visible = True
            dvEarlyOutPolicy.Visible = True
            dvAbsentPolicy.Visible = True
        End If
    End Sub

    Protected Sub btnClear_Click(sender As Object, e As System.EventArgs) Handles btnClear.Click
        ClearFilter()
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

    Public Sub BindReport(ByVal reportName As String)
        If Not RadComboViolationsReports.SelectedValue = "-1" Then
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
            cryRpt.SetParameterValue("@ReportName", RadComboViolationsReports.SelectedItem.Text)
            cryRpt.SetParameterValue("@ReportName", RadComboViolationsReports.SelectedItem.Text, "rptHeader")
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
            '----------To Enable Colors in Violation Report---------'
            If RadComboViolationsReports.SelectedValue = EmployeesViolationsReport Or RadComboViolationsReports.SelectedValue = AdvanceViolationReport Or RadComboViolationsReports.SelectedValue = EmployeesViolationsReport_Senaat Or RadComboViolationsReports.SelectedValue = EmployeesViolationsReport2 Then
                Dim IsDailyReportWithColor As Boolean
                objAPP_Settings = New APP_Settings
                With objAPP_Settings
                    .GetByPK()
                    IsDailyReportWithColor = .IsDailyReportWithColor
                End With
                cryRpt.SetParameterValue("@IsColored", IsDailyReportWithColor)
            End If
            '----------To Enable Colors in Violation Report---------'
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
                        FillExcelReport(reportName)
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

        If RadComboViolationsReports.SelectedValue = AdvanceViolationReport Then
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




        'rpttid = IIf(rpttid = 0, 3, rpttid)
        Select Case rpttid
            Case EmployeesViolationsReport
                RPTName = "rptViolations.rpt"
                DT = rptObj.GetViolations()
                'Case 2 'Employee list
                '    RPTName = "rptEmployeeList.rpt"
                '    DT = rptObj.GetFilterdEmpList()

            Case TA_Violation
                RPTName = "rptTA_Violations.rpt"
                DT = rptObj.GetTA_Violations

            Case TAPolicyViolations
                RPTName = "rptDetailedTA_Violations.rpt"
                DT = rptObj.Get_DetailedTAViolation
            Case ApproverViolationsToERP
                RPTName = "rptPayrollViolationToERP.rpt"
                DT = rptObj.GetPayRollViolationToERP()

            Case AdvanceViolationReport
                RPTName = "rptViolations_Advance.rpt"
                DT = rptObj.GetViolations_Advance()
            Case EmployeesViolationsReport_Senaat
                RPTName = "rptViolations.rpt"
                DT = rptObj.GetViolations_Sennat()
            Case EmployeesViolationsReport2
                RPTName = "rptViolations2.rpt"
                DT = rptObj.GetViolations2
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

        CtlCommon.FillTelerikDropDownList(RadComboViolationsReports, objsysforms.GetFormsByParentID(formID, groupID), Lang)

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
        HeadDT.Rows.Add(3)
        Dim rptName As String = RadComboViolationsReports.SelectedValue
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

        Select Case reportname
            Case EmployeesViolationsReport
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
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "التاريخ"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "التأخير"
                    NDT.Columns(6).ColumnName = "الخروج المبكر"
                    NDT.Columns(7).ColumnName = "وقت الدخول"
                    NDT.Columns(8).ColumnName = "وقت الخروج"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "الوقت الضائع "
                    NDT.Columns(10).ColumnName = "مدة الخروج"
                    NDT.Columns.RemoveAt(11)
                    For Each row In NDT.Rows
                        If Not row(7).ToString = "" And Not row(8).ToString = "" Then
                            If Convert.ToDateTime(row(8), UK_Culture).ToString("dd/MM/yyyy") < Convert.ToDateTime(row(7), UK_Culture).ToString("dd/MM/yyyy") Then
                                row(4) = "تسجيل الخروج قبل تسجيل الدخول"
                            End If
                        End If
                        If Not row(7).ToString = "" And row(8).ToString = "" And row(3) < Convert.ToDateTime(Date.Today, UK_Culture).ToString("dd/MM/yyyy") Then
                            row(4) = "لايوجد خروج"
                        End If
                        If row(7).ToString = "" And Not row(8).ToString = "" Then
                            row(4) = "لايوجد دخول"
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
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "Date"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Delay"
                    NDT.Columns(6).ColumnName = "Early Out"
                    NDT.Columns(7).ColumnName = "In Time"
                    NDT.Columns(8).ColumnName = "Out Time"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "Lost Time"
                    NDT.Columns(10).ColumnName = "StepOut Time"
                    NDT.Columns.RemoveAt(11)
                    For Each row In NDT.Rows
                        If Not row(7).ToString = "" And Not row(8).ToString = "" Then
                            If (Convert.ToDateTime(row(8), UK_Culture).ToString("dd/MM/yyyy") < Convert.ToDateTime(row(7), UK_Culture).ToString("dd/MM/yyyy")) Then
                                row(4) = "Sign Out Before Sign In"
                            End If
                        End If
                        If Not row(7).ToString = "" And row(8).ToString = "" And row(3) < Convert.ToDateTime(Date.Today, UK_Culture).ToString("dd/MM/yyyy") Then
                            row(4) = "Missing Out"
                        End If
                        If row(7).ToString = "" And Not row(8).ToString = "" Then
                            row(4) = "Missing In"
                        End If
                    Next

                End If

            Case TA_Violation
                If (Lang = CtlCommon.Lang.AR) Then
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
            Case TAPolicyViolations
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
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)

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
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)

                End If
            Case ApproverViolationsToERP
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
                                dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                            End If
                        End If

                    Next
                    NDT.Rows.Add(dr)
                Next

                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Employee Name"
                    NDT.Columns(3).ColumnName = "Employee Name"
                    NDT.Columns(4).ColumnName = "Employee Name"
                    NDT.Columns(5).ColumnName = "Employee Name"
                    NDT.Columns(6).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "Employee Name"
                Else

                End If



            Case AdvanceViolationReport
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(16).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(19).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    NDT.Columns(21).DataType = System.Type.GetType("System.String")
                    NDT.Columns(22).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 8 Then

                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If

                            ElseIf i = 16 Then
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
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف باللغة العربية"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "اسم وحدة العمل باللغة العربية"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "التاريخ"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "التأخير"
                    NDT.Columns(6).ColumnName = "الخروج المبكر"
                    NDT.Columns(7).ColumnName = "وقت الدخول"
                    NDT.Columns(8).ColumnName = "وقت الخروج"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)

                    For Each row In NDT.Rows
                        If Not row(7).ToString = "" And Not row(8).ToString = "" Then
                            If (Convert.ToDateTime(row(8), UK_Culture).ToString("dd/MM/yyyy") < Convert.ToDateTime(row(7), UK_Culture).ToString("dd/MM/yyyy")) Then
                                row(4) = "تسجيل الخروج قبل تسجيل الدخول"
                            End If
                        End If
                        If Not row(7).ToString = "" And row(8).ToString = "" And row(3) < Convert.ToDateTime(Date.Today, UK_Culture).ToString("dd/MM/yyyy") Then
                            row(4) = "لايوجد خروج"
                        End If
                        If row(7).ToString = "" And Not row(8).ToString = "" Then
                            row(4) = "لايوجد دخول"
                        End If
                    Next

                    NDT.SetColumnsOrder(New String() {"رقم الموظف",
                                "اسم الموظف باللغة العربية",
                                "اسم وحدة العمل باللغة العربية",
                                "التاريخ",
                                "وقت الدخول",
                                "التأخير",
                                "وقت الخروج",
                                "الخروج المبكر",
                                "الملاحظات"})

                Else
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(16).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(19).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    NDT.Columns(21).DataType = System.Type.GetType("System.String")
                    NDT.Columns(22).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 16 Then
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
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next

                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Employee No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "Date"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Delay"
                    NDT.Columns(6).ColumnName = "Early Out"
                    NDT.Columns(7).ColumnName = "In Time"
                    NDT.Columns(8).ColumnName = "Out Time"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)

                    For Each row In NDT.Rows
                        If Not row(7).ToString = "" And Not row(8).ToString = "" Then
                            If (Convert.ToDateTime(row(8), UK_Culture).ToString("dd/MM/yyyy") < Convert.ToDateTime(row(7), UK_Culture).ToString("dd/MM/yyyy")) Then
                                row(4) = "Sign Out Before Sign In"
                            End If
                        End If
                        If Not row(7).ToString = "" And row(8).ToString = "" And row(3) < Convert.ToDateTime(Date.Today, UK_Culture).ToString("dd/MM/yyyy") Then
                            row(4) = "Missing Out"
                        End If
                        If row(7).ToString = "" And Not row(8).ToString = "" Then
                            row(4) = "Missing In"
                        End If
                    Next

                    NDT.SetColumnsOrder(New String() {"Employee No.",
                                "Employee Name",
                                "Entity Name",
                                "Date",
                                "In Time",
                                "Delay",
                                "Out Time",
                                "Early Out",
                                "Remarks"
                                })

                End If

            Case EmployeesViolationsReport_Senaat
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
                            If (Convert.ToDateTime(row(10), UK_Culture).ToString("dd/MM/yyyy") < Convert.ToDateTime(row(9), UK_Culture).ToString("dd/MM/yyyy")) Then
                                row(6) = "تسجيل الخروج قبل تسجيل الدخول"
                            End If
                        End If
                        If Not row(9).ToString = "" And row(10).ToString = "" And row(5) < Convert.ToDateTime(Date.Today, UK_Culture).ToString("dd/MM/yyyy") Then
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
                            If (Convert.ToDateTime(row(10), UK_Culture).ToString("dd/MM/yyyy") < Convert.ToDateTime(row(9), UK_Culture).ToString("dd/MM/yyyy")) Then
                                row(6) = "Sign Out Before Sign In"
                            End If
                        End If
                        If Not row(9).ToString = "" And row(10).ToString = "" And row(5) < Convert.ToDateTime(Date.Today, UK_Culture).ToString("dd/MM/yyyy") Then
                            row(6) = "Missing Out"
                        End If
                        If row(9).ToString = "" And Not row(10).ToString = "" Then
                            row(6) = "Missing In"
                        End If
                    Next

                End If

            Case EmployeesViolationsReport2

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

    Public Sub ClearFilter()
        EmployeeFilter1.ClearValues()

    End Sub

#End Region


End Class
