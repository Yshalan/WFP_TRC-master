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
Imports VMS

Partial Class Reports_SelfServices_Visitors_Reports
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


    Private VisitorReport As String = "Visitor Report"


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
        'EmployeeFilter1.ShowHideValues()

        'EmployeeFilter1.divEmployeeID.Visible = False
        'EmployeeFilter1.EmployeestxtBox.Visible = False

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
            lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير الزائر", "Visitor Report")
            btnPrint.Text = IIf(Lang = CtlCommon.Lang.AR, "انشاء", "Generate")
            lblVisitorReport.Text = IIf(Lang = CtlCommon.Lang.AR, "اسم الزائر", "Visitor Name")
            Label1.Text = IIf(Lang = CtlCommon.Lang.AR, "شركة", "Company")
            lblLevels.Text = IIf(Lang = CtlCommon.Lang.AR, "وحدة العمل", "Entity")
            lblFromDate.Text = IIf(Lang = CtlCommon.Lang.AR, "من التاريخ", "From Date")
            lblToDate.Text = IIf(Lang = CtlCommon.Lang.AR, "الى تاريخ", "To Date")

            'If rblFormat.SelectedValue = 1 Then
            '    ListItemResource1.Text = IIf(Lang = CtlCommon.Lang.AR, "بي دي إف", "PDF")
            'ElseIf rblFormat.SelectedValue = 2 Then
            '    rblFormat.Text = IIf(Lang = CtlCommon.Lang.AR, "مايكروسوفت اوفيس", "MS Word")
            'Else
            '    rblFormat.Text = IIf(Lang = CtlCommon.Lang.AR, "مايكروسوفت اكسل", "MS Excel")
            'End If

            ' FillReports(formID, SessionVariables.LoginUser.GroupId)
            FillVisitor()
            FillCompanies()
            FillEntities()
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
        'If RadCmbBxCompanies.SelectedIndex = -1 Or
        '    RadCmbBxCompanies.SelectedIndex = 0 Then
        '    If SessionVariables.CultureInfo = "ar-JO" Then
        '        CtlCommon.ShowMessage(Page, "الرجاء تحديد اسم الزائر", "info")
        '    Else
        '        CtlCommon.ShowMessage(Page, "Please Select Company", "info")
        '    End If
        '    Exit Sub
        'End If

        BindReport()
    End Sub

    Protected Sub RadCmbBxCompanies_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxCompanies.SelectedIndexChanged
        FillEntities()
    End Sub

    Protected Sub btnClear_Click(sender As Object, e As System.EventArgs) Handles btnClear.Click
        FillVisitor()
        FillCompanies()
        FillEntities()
        RadDatePicker1.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
        Dim dd As New Date
        dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)

        RadDatePicker2.SelectedDate = dd

    End Sub



#End Region

#Region "Methods"
    Private Sub FillVisitor()
        Dim objVisitors As New Visitors
        Dim dtCurrent As New DataTable
        dtCurrent = objVisitors.GetAll()

        For i As Integer = 0 To objVisitors.GetAll.Rows.Count - 1

            If objVisitors.GetAll.Rows.Item(i).Item("VisitorName") Is DBNull.Value Then
                'dr("VisitorName") = objVisitors.GetAll.Rows.Item(i).Item("VisitorName")

                dtCurrent.Rows.Item(i).Delete()


            End If

        Next

        dtCurrent.AcceptChanges()
        CtlCommon.FillTelerikDropDownList(RadComboVisitor, dtCurrent, Lang)

    End Sub
    Private Sub FillCompanies()
        Dim objOrgCompany As New OrgCompany
        CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies, objOrgCompany.GetAllforddl, Lang)
    End Sub
    Private Sub FillEntities()
        Dim objProjectCommon = New ProjectCommon()
        Dim objOrgEntity = New OrgEntity()
        If (RadCmbBxCompanies.SelectedValue <> "") Then
            objOrgEntity.FK_CompanyId = RadCmbBxCompanies.SelectedValue
        End If

        Dim dt As DataTable = objOrgEntity.GetAllEntityByCompany()

        objProjectCommon.FillMultiLevelRadComboBox(RadCmbBxEntity, dt, "EntityId",
                                                             "EntityName", "EntityArabicName", "FK_ParentId")
        RadCmbBxEntity.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem(ResourceManager.GetString("PleaseSelect", CultureInfo), -1))
    End Sub

    Public Shared Function GetPageDirection() As String
        If SessionVariables.CultureInfo = "ar-JO" Then
            Return "rtl"
        Else
            Return "ltr"
        End If
    End Function

    Public Sub BindReport() 'ByVal reportName As String
        'If Not RadCmbBxCompanies.SelectedValue = "-1" Then
        Dim rptObj As New Report

        SetReportName()

        cryRpt = New ReportDocument
        cryRpt.Load(Server.MapPath(RPTName))
        cryRpt.SetDataSource(DT)
        Dim objAPP_Settings As New APP_Settings
        objAPP_Settings.FK_CompanyId = RadCmbBxCompanies.SelectedValue
        dt2 = objAPP_Settings.GetHeader()
        dt2.Columns.Add("From_Date")
        dt2.Columns.Add("To_Date")

        dt2.Rows(0).Item("From_Date") = DateToString(RadDatePicker1.SelectedDate)
        dt2.Rows(0).Item("To_Date") = DateToString(RadDatePicker2.SelectedDate)


        cryRpt.Subreports(cryRpt.Subreports().Item(0).Name).SetDataSource(dt2)
        cryRpt.SetParameterValue("@ReportName", "Visitor Report")
        cryRpt.SetParameterValue("@ReportName", "Visitor Report", "rptHeader")
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
        If RadComboVisitor.SelectedValue = VisitorReport Then
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

            If RadCmbBxCompanies.SelectedValue <> "" Then

                If RadCmbBxCompanies.SelectedValue = -1 Then
                    objOrgCompany.CompanyId = 0
                Else
                    objOrgCompany.CompanyId = RadCmbBxCompanies.SelectedValue
                End If

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
                    FillExcelReport()
                    ExportDataSetToExcel(NDT, "ExportedReport", HeadDT)
                End If
                MultiView1.SetActiveView(Report)
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NoDataFound", CultureInfo), "info")
            End If
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NoDataFound", CultureInfo), "info")
        End If
        'Else
        '    CtlCommon.ShowMessage(Page, ResourceManager.GetString("PleaseSelectCompay", CultureInfo), "info")
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

    Private Sub SetReportName()
        Dim rptObj As New Report

        If Not RadDatePicker1.SelectedDate Is Nothing Then
            rptObj.FROM_DATE = RadDatePicker1.DbSelectedDate
        End If
        If Not RadDatePicker2.SelectedDate Is Nothing Then
            rptObj.TO_DATE = RadDatePicker2.DbSelectedDate
        End If

        If Not RadCmbBxCompanies.SelectedValue = "" Then
            If Not RadCmbBxCompanies.SelectedValue = "-1" Then
                rptObj.CompanyId = RadCmbBxCompanies.SelectedValue
            Else
                rptObj.CompanyId = Nothing
            End If
        Else
            rptObj.CompanyId = Nothing
        End If

        If Not RadCmbBxEntity.SelectedValue = "" Then
            If Not RadCmbBxEntity.SelectedValue = "-1" Then
                rptObj.EntityId = RadCmbBxEntity.SelectedValue
            Else
                rptObj.EntityId = Nothing
            End If
        Else
            rptObj.EntityId = Nothing
        End If
        If Not RadComboVisitor.SelectedValue = "" Then
            If Not RadComboVisitor.SelectedValue = "-1" Then
                rptObj.EmployeeId = RadComboVisitor.SelectedValue
            Else
                rptObj.EmployeeId = Nothing
            End If
        Else
            rptObj.EmployeeId = Nothing
        End If


        RPTName = "rpt_Visitor.rpt"
        DT = rptObj.GetVisitors()
        'End Select

        UserName = SessionVariables.LoginUser.UsrID

        If (Lang = CtlCommon.Lang.AR) Then
            RPTName = "Arabic/" + RPTName
        Else
            RPTName = "English/" + RPTName
        End If

    End Sub






    Private Sub FillExcelReport()
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
        Dim rptName As String = "Visitor Report"
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

        'Select Case reportname
        'Case VisitorReport
        If (Lang = CtlCommon.Lang.AR) Then
            For Each row As DataRow In DT.Rows
                Dim dr As DataRow = NDT.NewRow
                For i As Integer = 0 To NDT.Columns.Count - 1
                    dr(i) = row(i)
                    'If i = 3 Then
                    '    If Not dr(i) Is DBNull.Value Then
                    '        dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                    '    End If
                    'End If
                    'If i = 4 Then
                    '    If Not dr(i) Is DBNull.Value Then
                    '        dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                    '    End If
                    'End If
                    If i = 13 Then
                        If Not dr(i) Is DBNull.Value Then
                            dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                        End If
                    End If
                Next
                NDT.Rows.Add(dr)
            Next
            NDT.Columns.RemoveAt(0)
            NDT.Columns(0).ColumnName = "اسم الزائر"
            NDT.Columns.RemoveAt(1)
            NDT.Columns(1).ColumnName = "رقم البطاقة"
            NDT.Columns(2).ColumnName = "وقت تسجيل الوصول"
            NDT.Columns(3).ColumnName = "تحقق من الوقت"
            NDT.Columns(4).ColumnName = "اسم المنظمة"
            NDT.Columns(5).ColumnName = "الجنسية"
            NDT.Columns(6).ColumnName = "رقم الهاتف المحمول"
            NDT.Columns(7).ColumnName = "سبب الزيارة"
            NDT.Columns.RemoveAt(8)
            NDT.Columns.RemoveAt(8)
            NDT.Columns(8).ColumnName = "زيارة تاريخ الإنشاء"
            'NDT.Columns.RemoveAt(8)
        Else
            For Each row As DataRow In DT.Rows
                Dim dr As DataRow = NDT.NewRow
                For i As Integer = 0 To NDT.Columns.Count - 1
                    dr(i) = row(i)
                    'If i = 3 Then
                    '    If Not dr(i) Is DBNull.Value Then
                    '        dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                    '    End If
                    'End If
                    'If i = 4 Then
                    '    If Not dr(i) Is DBNull.Value Then
                    '        dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                    '    End If
                    'End If
                    If i = 13 Then
                        If Not dr(i) Is DBNull.Value Then
                            dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                        End If
                    End If
                Next
                NDT.Rows.Add(dr)
            Next
            NDT.Columns(0).ColumnName = "Visitor Name"
            NDT.Columns.RemoveAt(1)
            NDT.Columns(1).ColumnName = "Card Number"
            NDT.Columns(2).ColumnName = "Check In Time"
            NDT.Columns(3).ColumnName = "Check Out Time"
            NDT.Columns(4).ColumnName = "Organization Name"
            NDT.Columns(5).ColumnName = "Nationality"
            NDT.Columns(6).ColumnName = "Mobile Number"
            NDT.Columns(7).ColumnName = "Reason of Visit"
            NDT.Columns.RemoveAt(8)
            NDT.Columns.RemoveAt(8)
            NDT.Columns.RemoveAt(8)
            NDT.Columns.RemoveAt(8)
            NDT.Columns(8).ColumnName = "Visit Created Date"

        End If
    End Sub

    Protected Sub RadComboAttendanceReports_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboVisitor.SelectedIndexChanged

    End Sub

#End Region


End Class
