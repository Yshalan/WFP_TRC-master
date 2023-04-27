Imports System.Data
Imports SmartV.UTILITIES
Imports TA.DailyTasks
Imports Telerik.Web.UI
Imports SmartV.UTILITIES.ProjectCommon
Imports TA.Employees
Imports TA.Security
Imports TA.Reports
Imports CrystalDecisions.CrystalReports.Engine
Imports TA.Admin
Imports CrystalDecisions.Shared

Partial Class DailyTasks_Verify_EmpTransactions
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objMonthly_Deduction As Monthly_Deduction
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Public MsgLang As String
    Public strLang As String
    Dim cryRpt As New ReportDocument
    Private objAPP_Settings As APP_Settings
    Private objSYSUsers As SYSUsers
    Private objOrgCompany As OrgCompany
    Private objEmployee As Employee

#End Region

#Region "Public Properties"

    Public Property dtDeductions() As DataTable
        Get
            Return ViewState("dtDeductions")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtDeductions") = value
        End Set
    End Property

    Public Property FK_EmployeeId() As Integer
        Get
            Return ViewState("FK_EmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("FK_EmployeeId") = value
        End Set
    End Property
#End Region

#Region "Page Events"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        Else
            If SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                strLang = "ar"
                MsgLang = "ar"
                Page.MasterPageFile = "~/default/ArabicMaster.master"
            Else
                Lang = CtlCommon.Lang.EN
                strLang = "en"
                MsgLang = "en"
                Page.MasterPageFile = "~/default/NewMaster.master"
            End If
            Page.UICulture = SessionVariables.CultureInfo
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR

            Else
                Lang = CtlCommon.Lang.EN

            End If
            FillGrid()
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            ddlMonth_Bind()
            ddlYear_Bind()
            PageHeader1.HeaderText = ResourceManager.GetString("ApproveDeduction", CultureInfo)

        End If
    End Sub

    Protected Sub dgrdEmp_Trans_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdEmp_Trans.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim item As GridDataItem
            item = e.Item

            If Lang = CtlCommon.Lang.AR Then
                item("EmployeeName").Text = item.GetDataKeyValue("EmployeeArabicName").ToString()
            End If

            Dim scriptManager__1 As ScriptManager = ScriptManager.GetCurrent(Me.Page)
            scriptManager__1.RegisterPostBackControl(DirectCast(e.Item.FindControl("lnkTA2Report"), LinkButton))

        End If

    End Sub

    Protected Sub dgrdEmp_Trans_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdEmp_Trans.NeedDataSource
        objMonthly_Deduction = New Monthly_Deduction
        With objMonthly_Deduction
            .FK_CompanyId = UCSecurityFilter1.CompanyId
            .FK_EntityId = UCSecurityFilter1.EntityId
            .Year = ddlYear.SelectedValue
            .Month = ddlMonth.SelectedValue
            dtDeductions = .GetAll_Inner
            dgrdEmp_Trans.DataSource = dtDeductions
        End With
    End Sub

    Protected Sub btnApprove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApprove.Click

        Dim errNum As Integer = -1
        If dtDeductions.Rows.Count > 1 Then
            For Each row In dtDeductions.Rows
                Dim EmployeeId As Integer = Convert.ToInt32(row("FK_EmployeeId").ToString())
                Dim Year As String = row("Year").ToString()
                Dim Month As String = row("Month").ToString()
                objMonthly_Deduction = New Monthly_Deduction
                With objMonthly_Deduction
                    .Year = Year
                    .Month = Month
                    .FK_EmployeeId = EmployeeId
                    .Apporved_By = Convert.ToString(SessionVariables.LoginUser.ID)
                    errNum = .Update_Approval
                End With
            Next
            If errNum = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
                FillGrid()
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If
        End If

    End Sub

    Protected Sub btnGet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGet.Click
        objMonthly_Deduction = New Monthly_Deduction
        With objMonthly_Deduction
            .FK_CompanyId = UCSecurityFilter1.CompanyId
            .FK_EntityId = UCSecurityFilter1.EntityId
            .Year = ddlYear.SelectedValue
            .Month = ddlMonth.SelectedValue
            dtDeductions = .GetAll_Inner
            dgrdEmp_Trans.DataSource = dtDeductions
            dgrdEmp_Trans.DataBind()
        End With
    End Sub

    Protected Sub lnkTA2Report_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rptObj As New Report
        Dim intYear As Integer
        Dim intMonth As Integer
        Dim dateFromDate As DateTime
        Dim dateToDate As DateTime
        Dim objSmartSecurity As New SmartSecurity
        Dim objAPP_Settings As New APP_Settings

        FK_EmployeeId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FK_EmployeeId"))

        If Not ddlYear.SelectedValue = -1 Then
            intYear = ddlYear.SelectedValue
        End If

        If Not ddlMonth.SelectedValue = -1 Then
            intMonth = ddlMonth.SelectedValue
        End If
        dateFromDate = Date.ParseExact(intMonth.ToString("00") + "/01/" + intYear.ToString("0000"), "MM/dd/yyyy", Nothing)

        Dim dd As New Date
        dd = Date.ParseExact(dateFromDate.Month.ToString("00") + "/" + DateTime.DaysInMonth(dateFromDate.Year, Date.Now.Month).ToString("00") + "/" + dateFromDate.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
        dateToDate = dd

        rptObj.FROM_DATE = dateFromDate
        rptObj.TO_DATE = dateToDate

        If FK_EmployeeId <> 0 Then
            rptObj.EmployeeId = FK_EmployeeId
        Else
            rptObj.EmployeeId = Nothing
        End If

        Dim RPTName As String
        Dim DT As DataTable
        Dim UserName As String

        objAPP_Settings.GetByPK()
        If objAPP_Settings.MonthlyDeduction_Report = 1 Then
            RPTName = "rptEmpTimeAttendance_.rpt"
            DT = rptObj.GetEmpTimeAttendance()
        Else
            If (objAPP_Settings.IsDailyReportWithColor) Then
                RPTName = "rptEmpMoveColored.rpt"
            Else
                RPTName = "rptEmpMove.rpt"
            End If
            DT = rptObj.GetFilterdEmpMove()
        End If

        If (Lang = CtlCommon.Lang.AR) Then
            RPTName = "Arabic/" + RPTName
        Else
            RPTName = "English/" + RPTName
        End If
        UserName = SessionVariables.LoginUser.UsrID

        cryRpt = New ReportDocument
        Dim path As String = Server.MapPath("../Reports")
        cryRpt.Load(path & "/" & (RPTName))
        cryRpt.SetDataSource(DT)

        Dim dt2 As DataTable
        Dim Version As String
        Dim CopyRightEn As String
        Dim CopyRightAr As String
        objEmployee = New Employee
        objEmployee.EmployeeId = FK_EmployeeId
        objEmployee.GetByPK()
        objAPP_Settings.FK_CompanyId = objEmployee.FK_CompanyId
        dt2 = objAPP_Settings.GetHeader()
        dt2.Columns.Add("From_Date")
        dt2.Columns.Add("To_Date")
        dt2.Rows(0).Item("From_Date") = DateToString(dateFromDate)
        dt2.Rows(0).Item("To_Date") = DateToString(dateToDate)
        cryRpt.Subreports(cryRpt.Subreports().Item(0).Name).SetDataSource(dt2)
        cryRpt.SetParameterValue("@UserName", UserName)
        Dim ShowSTLogo As Boolean
        ShowSTLogo = ConfigurationManager.AppSettings("ShowSmartTimeLogo")
        cryRpt.SetParameterValue("@ShowSTLogo", False)
        If objAPP_Settings.MonthlyDeduction_Report = 1 Then
            cryRpt.SetParameterValue("@ShowSTLogo", False, "rptHeader")
            cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "1ـ تقرير الحضور والانصراف 2", "12- Employee Time & Attendance 2"))
            cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "1ـ تقرير الحضور والانصراف 2", "12- Employee Time & Attendance 2"), "rptHeader")
        Else
            cryRpt.SetParameterValue("@ShowSTLogo", False, "rptHeader")
            cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "1ـ التقرير اليومي", "1- Daily Report"))
            cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "1ـ التقرير اليومي", "1- Daily Report"), "rptHeader")
        End If

        cryRpt.SetParameterValue("@FromDate", dateFromDate)
        cryRpt.SetParameterValue("@ToDate", dateToDate)
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
            objEmployee = New Employee
            objEmployee.EmployeeId = FK_EmployeeId
            objEmployee.GetByPK()

            rptObj = New Report
            If FK_EmployeeId <> 0 Then
                objOrgCompany.CompanyId = objEmployee.FK_CompanyId
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
                cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedReport")
                mvDeductions.SetActiveView(vReport)
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NoDataFound", CultureInfo), "info")
            End If
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NoDataFound", CultureInfo), "info")
        End If

    End Sub

#End Region

#Region "Methods"

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

        ddlYear.SelectedValue = Date.Today.Year
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

    Private Sub FillGrid()
        'Dim dt As DataTable
        objMonthly_Deduction = New Monthly_Deduction
        With objMonthly_Deduction
            .FK_CompanyId = UCSecurityFilter1.CompanyId
            .FK_EntityId = UCSecurityFilter1.EntityId
            .Year = Now.Year
            .Month = Now.Month
            dtDeductions = .GetAll_Inner
            dgrdEmp_Trans.DataSource = dtDeductions
            dgrdEmp_Trans.DataBind()
        End With
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

#End Region

#Region "Grid Filter"

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon1() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdEmp_Trans.Skin))
    End Function

    Protected Sub dgrdEmp_Trans_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdEmp_Trans.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

#End Region

End Class
