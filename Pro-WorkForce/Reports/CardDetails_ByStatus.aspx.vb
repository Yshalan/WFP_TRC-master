Imports TA.Admin
Imports SmartV.UTILITIES
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data
Imports TA.Security
Imports TA.Reports
Imports CrystalDecisions.Shared
Imports Telerik.Web.UI
Imports SmartV.UTILITIES.CtlCommon

Partial Class Reports_CardDetails_ByStatus
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objAPP_Settings As APP_Settings
    Private Lang As CtlCommon.Lang
    Protected dir As String
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Dim cryRpt As New ReportDocument
    Dim objSmartSecurity As New SmartSecurity
    Private objOrgCompany As OrgCompany

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

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تفاصيل البطاقة", "Card Details")

            End Select
            FillStatus()
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

        Dim ShowSTLogo As Boolean
        ShowSTLogo = ConfigurationManager.AppSettings("ShowSmartTimeLogo")
        cryRpt.SetParameterValue("@ShowSTLogo", False)
        cryRpt.SetParameterValue("@ShowSTLogo", False, "rptHeader")

        cryRpt.SetParameterValue("@UserName", UserName)
        cryRpt.SetParameterValue("@ReportName", "Employee Card Details")
        cryRpt.SetParameterValue("@ReportName", "Employee Card Details", "rptHeader")
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
        'Version = System.Configuration.ConfigurationManager.AppSettings("Version").ToString()

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

        If ddlStatus.SelectedValue <> -1 Then

            rptObj.Status = ddlStatus.SelectedValue
        Else
            rptObj.Status = Nothing
        End If

        rpttid = IIf(rpttid = 0, 3, rpttid)

        Select Case rpttid
            Case 1
                RPTName = "rptEmpCards.rpt"
                DT = rptObj.GetCardDetails()

        End Select
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

    Private Sub FillExcelreport()
        Dim rptid As Integer = Request.QueryString("ReportId")
        NDT = New DataTable
        NDT = DT.Clone()

        Dim UK_Culture As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("en-GB")

        Select Case rptid

            Case 1

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
        End Select
    End Sub

#End Region

End Class
