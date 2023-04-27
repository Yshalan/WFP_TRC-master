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
Partial Class Reports_SelfServices_ExtraHoursAndOverTimeReports
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
    Private objRequestStatus As New TA.Lookup.RequestStatus
    Private objSYSUsers As SYSUsers

    Private OvertimeDeductionReport As String = "OvertimeDeductionReport"
    Private EmployeesExtraHoursReport As String = "Employees Extra Hours Report"
    Private OvertimeIncentivesReport As String = "OvertimeReport"
    Private OverTimeReport As String = "Emp_BasicOvertime"
    Private OverTimeReportPerDayMinutes As String = "OverTimeReportPerDayMinutes"


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
            lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "تقارير العمل الإضافي", "Extra Hours And Overtime Reports")
            btnPrint.Text = IIf(Lang = CtlCommon.Lang.AR, "انشاء", "Generate")
            lblFromDate.Text = IIf(Lang = CtlCommon.Lang.AR, "من تاريخ", "From Date")
            lblToDate.Text = IIf(Lang = CtlCommon.Lang.AR, "الى تاريخ", "To Date")
            lblOvertimeType.Text = IIf(Lang = CtlCommon.Lang.AR, "نوع الوقت الاضافي", "Overtime Type")
            lblOvertimeStatus.Text = IIf(Lang = CtlCommon.Lang.AR, "حالة الوقت الاضافي", "Overtime Status")
            rblType.Items(0).Text = IIf(Lang = CtlCommon.Lang.AR, "وقت الدخول \ الخروج ضمن الجدول", "In/Out Time Within Schedule")
            rblType.Items(1).Text = IIf(Lang = CtlCommon.Lang.AR, "وقت الدخول \ الخروج دون تحديد", "In/Out Time Without Specifying")

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
        Dim reportName As String = RadComboExtraReports.SelectedValue

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

    Protected Sub RadComboExtraReports_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboExtraReports.SelectedIndexChanged
        If RadComboExtraReports.SelectedValue = OvertimeIncentivesReport Then
            trOvertimeType.Visible = True
            trOvertimeStatus.Visible = True
            FillOvertimeStatus()

        ElseIf RadComboExtraReports.SelectedValue = OverTimeReport Then
            trOvertimeStatus.Visible = True
            trOvertimeType.Visible = False
            FillOvertimeStatus()
        Else
            trOvertimeStatus.Visible = False
            trOvertimeType.Visible = False
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
            'cryRpt.SetParameterValue("@ProcessStatusId", rblType.SelectedValue)
            cryRpt.SetParameterValue("@UserName", UserName)



            Dim ShowSTLogo As Boolean
            ShowSTLogo = ConfigurationManager.AppSettings("ShowSmartTimeLogo")
            cryRpt.SetParameterValue("@ShowSTLogo", False)
            cryRpt.SetParameterValue("@ShowSTLogo", False, "rptHeader")
            cryRpt.SetParameterValue("@ReportName", RadComboExtraReports.SelectedItem.Text)
            cryRpt.SetParameterValue("@ReportName", RadComboExtraReports.SelectedItem.Text, "rptHeader")
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
        If rpttid = OverTimeReport Or rpttid = OvertimeIncentivesReport Then
            If Not ddlOvertimeStatus.SelectedValue = -1 Then
                rptObj.ProcessStatus = ddlOvertimeStatus.SelectedValue

            Else
                rptObj.ProcessStatus = Nothing
            End If
            'If Not rblType.SelectedValue = 0 Then
            '    rptObj.ProcessStatus = rblType.SelectedValue
            'Else
            '    rptObj.ProcessStatus = Nothing
            'End If


        End If

        Select Case rpttid
            Case EmployeesExtraHoursReport
                RPTName = "crExtraHours.rpt"
                DT = rptObj.GetExtraHours()

            Case OvertimeDeductionReport
                RPTName = "rptOvertime_DeductLost.rpt"
                DT = rptObj.Get_Overtime_DeductLostTime

            Case OverTimeReport
                RPTName = "rptEmpBasicOvertime.rpt"
                DT = rptObj.GetEmp_BasicOvertime()

            Case OvertimeIncentivesReport
                RPTName = "rptEmpOvertime.rpt"
                DT = rptObj.GetEmp_Overtime()
            Case OverTimeReportPerDayMinutes
                RPTName = "rptEmpOvertimePerDayMinute.rpt"
                DT = rptObj.GetEmp_Overtime_PerDayMinutes

        End Select

        UserName = SessionVariables.LoginUser.UsrID
        ' Version = System.Configuration.ConfigurationManager.AppSettings("Version").ToString()

        If (Lang = CtlCommon.Lang.AR) Then
            RPTName = "Arabic/" + RPTName
        Else
            RPTName = "English/" + RPTName
        End If

    End Sub

    Private Sub FillOvertimeStatus()
        objRequestStatus = New TA.Lookup.RequestStatus
        CtlCommon.FillTelerikDropDownList(ddlOvertimeStatus, objRequestStatus.GetAll, Lang)

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
        Dim rptName As String = RadComboExtraReports.SelectedValue
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
            Case EmployeesExtraHoursReport
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
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
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

            Case OvertimeIncentivesReport
                ' NDT = DT
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(14).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 14 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 15 Then
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
                    NDT.Columns(7).ColumnName = "اسم الحالة"
                    NDT.Columns(8).ColumnName = "اسم الحالة باللغة العربية"
                    NDT.Columns(9).ColumnName = "المدة"

                    NDT.Columns(10).ColumnName = "من تاريخ"
                    NDT.Columns(11).ColumnName = "الى تاريخ"
                    NDT.Columns(12).ColumnName = "وقت عالي"
                    NDT.Columns.RemoveAt(13)
                Else
                    NDT.Columns(14).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 14 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 15 Then
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
                    NDT.Columns(7).ColumnName = "Status Name"
                    NDT.Columns(8).ColumnName = "Status Arabic Name"
                    NDT.Columns(9).ColumnName = "Duration"
                    NDT.Columns(10).ColumnName = "From Date"
                    NDT.Columns(11).ColumnName = "To Date"
                    NDT.Columns(12).ColumnName = "High Time"
                    NDT.Columns.RemoveAt(13)
                End If
            Case OverTimeReport
                'NDT = DT.Clone
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    NDT.Columns(16).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 15 Then
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
                    NDT.Columns(7).ColumnName = "اسم الحالة"
                    NDT.Columns(8).ColumnName = "اسم الحالة باللغة العربية"
                    NDT.Columns(9).ColumnName = "المدة"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "من تاريخ"
                    NDT.Columns(11).ColumnName = "الى تاريخ"
                    NDT.Columns(12).ColumnName = "وقت عالي"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "الملاحظات"

                Else
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    NDT.Columns(16).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 15 Then
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
                    NDT.Columns(7).ColumnName = "Status Name"
                    NDT.Columns(8).ColumnName = "Status Arabic Name"
                    NDT.Columns(9).ColumnName = "Duration"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "From Date"
                    NDT.Columns(11).ColumnName = "To Date"
                    NDT.Columns(12).ColumnName = "High Time"
                    NDT.Columns(13).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(14)
                End If
            Case OvertimeDeductionReport
                'NDT = DT

            Case OverTimeReportPerDayMinutes
                NDT.Columns(15).DataType = System.Type.GetType("System.String")
                NDT.Columns(16).DataType = System.Type.GetType("System.String")
                For Each row As DataRow In DT.Rows
                    Dim dr As DataRow = NDT.NewRow
                    For i As Integer = 0 To NDT.Columns.Count - 1
                        dr(i) = row(i)
                        If i = 15 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy hh:mm")
                            End If
                        ElseIf i = 16 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy hh:mm")
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
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "اسم الحالة"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "من تاريخ"
                    NDT.Columns(6).ColumnName = "الى تاريخ"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "المدة"
                    NDT.Columns.RemoveAt(8)

                    NDT.SetColumnsOrder(New String() {"رقم الموظف",
                                 "اسم الموظف",
                                 "اسم وحدة العمل",
                                 "اسم الشركة",
                                 "اسم الحالة",
                                 "من تاريخ",
                                 "الى تاريخ",
                                 "المدة"})

                Else
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Status Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "From Date"
                    NDT.Columns(6).ColumnName = "To Date"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "Duration"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)

                    NDT.SetColumnsOrder(New String() {"Emp No.",
                                 "Employee Name",
                                 "Entity Name",
                                 "Company Name",
                                 "Status Name",
                                 "From Date",
                                 "To Date",
                                 "Duration"})

                End If




        End Select
    End Sub

    Private Sub FillReports(ByVal formID As Integer, ByVal groupID As Integer)

        CtlCommon.FillTelerikDropDownList(RadComboExtraReports, objsysforms.GetFormsByParentID(formID, groupID), Lang)

    End Sub

    Public Sub ClearFilter()
        EmployeeFilter1.ClearValues()

    End Sub

#End Region

End Class
