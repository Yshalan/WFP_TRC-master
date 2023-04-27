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
Partial Class Reports_DelayandEarlyOut_Report
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

                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقرير التأخير و الخروج المبكر", "Delay and early out")

            End Select

            RadDatePicker1.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd As New Date
            dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)

            RadDatePicker2.SelectedDate = dd
            rmtTotalDelayEarly.TextWithLiterals = "0000"

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

    Protected Sub rblFilter_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rblFilter.SelectedIndexChanged
        If rblFilter.SelectedValue = 2 Or rblFilter.SelectedValue = 3 Then
            trTotalDelayEarly.Visible = True
            trTotalCount.Visible = False
            txtTotalCount.Text = String.Empty
        ElseIf rblFilter.SelectedValue = 4 Then
            trTotalCount.Visible = True
            trTotalDelayEarly.Visible = False
            rmtTotalDelayEarly.TextWithLiterals = "0000"
        Else
            trTotalDelayEarly.Visible = False
            trTotalCount.Visible = False
            rmtTotalDelayEarly.TextWithLiterals = "0000"
            txtTotalCount.Text = String.Empty
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
        'cryRpt.SetParameterValue("@Version", Version)
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
        rptObj.Type = rblFilter.SelectedValue
        Dim strTotal As String = (CInt(rmtTotalDelayEarly.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtTotalDelayEarly.TextWithLiterals.Split(":")(1))
        rptObj.TotalDelayEarlyOut = strTotal

        If Not txtTotalCount.Text = Nothing Then
            rptObj.TotalCount = txtTotalCount.Text
        End If

        rpttid = IIf(rpttid = 0, 3, rpttid)

        Select Case rpttid
            Case 1
                RPTName = "rptDelayEarlyOut.rpt"
                DT = rptObj.Get_DelayAndEarlyOut()

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

        Dim UK_Culture As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("en-GB")

        Select Case rptid
            Case 1
                NDT.Columns(8).DataType = System.Type.GetType("System.String")
                NDT.Columns(9).DataType = System.Type.GetType("System.String")
                NDT.Columns(10).DataType = System.Type.GetType("System.String")
                NDT.Columns(11).DataType = System.Type.GetType("System.String")
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
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "اسم الموظف باللغة العربية"
                    NDT.Columns(3).ColumnName = "اسم وحدة العمل"
                    NDT.Columns(4).ColumnName = "اسم وحدة العمل باللغة العربية"
                    NDT.Columns(5).ColumnName = "اسم الشركة"
                    NDT.Columns(6).ColumnName = "اسم الشركة باللغة العربية"
                    NDT.Columns(7).ColumnName = "تاريخ الحركة"
                    NDT.Columns(8).ColumnName = "وقت الدخول"
                    NDT.Columns(9).ColumnName = "وقت الخروج"
                    NDT.Columns(10).ColumnName = "التأخير"
                    NDT.Columns(11).ColumnName = "الخروج المبكر"
                    NDT.Columns(12).ColumnName = "مجموع التأخير و الخروج المبكر"
                    NDT.Columns(13).ColumnName = "مجموع مرات التأخير و الخروج المبكر"
                    NDT.Columns(14).ColumnName = "عدد مرات التأخير"
                    NDT.Columns(15).ColumnName = "عدد مرات الخروج المبكر"
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
                    NDT.Columns(10).ColumnName = "Delay"
                    NDT.Columns(11).ColumnName = "Early Out"
                    NDT.Columns(12).ColumnName = "Total Delay and EarlyOut"
                    NDT.Columns(13).ColumnName = "Delay and EarlyOut Count"
                    NDT.Columns(14).ColumnName = "Delay Count"
                    NDT.Columns(15).ColumnName = "EarlyOut Count"
                End If
        End Select



    End Sub

#End Region

End Class
