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
Imports TA.Forms
Imports TA.Definitions
Imports TA.TaskManagement


Partial Class Reports_ProjectReports
    Inherits System.Web.UI.Page
#Region "Class Variables"

    Private objAPP_Settings As APP_Settings
    Private Lang As CtlCommon.Lang
    Protected dir As String
    Private objVersion As SmartV.Version.version
    Private objsysforms As New SYSForms
    Private objSys_Forms As New Sys_Forms
    Private objSYSUsers As SYSUsers
    Private objOrgCompany As OrgCompany
    Dim CultureInfo As System.Globalization.CultureInfo
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim cryRpt As New ReportDocument
    Dim objSmartSecurity As New SmartSecurity
    Private objRequestStatus As New TA.Lookup.RequestStatus
    Private ObjProject As New TA.TaskManagement.Projects

    Private EmployeeTask As String = "Employee_Task_Report"
    Private ProjectTask As String = "Project_Task_Report"
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
        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            formID = row("FormID")
        Next
        If Not Page.IsPostBack Then
            ' lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "Project Reports", "Project Reports")
          
            FillReports(formID, SessionVariables.LoginUser.GroupId)
            RadComboExtraReports.SelectedValue = EmployeeTask
            RadComboExtraReports_SelectedIndexChanged(Nothing, Nothing)
            objAPP_Settings = New APP_Settings
            With objAPP_Settings
                .GetByPK()
                'rblFormat.SelectedValue = .DefaultReportFormat

            End With
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        cryRpt.Close()
        cryRpt.Dispose()

    End Sub
    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim reportName As String = RadComboExtraReports.SelectedValue
        If RadComboExtraReports.SelectedValue = "-1" Then
            If SessionVariables.CultureInfo = "ar-JO" Then
                CtlCommon.ShowMessage(Page, "الرجاء اختيار نوع التقرير", "info")
            Else
                CtlCommon.ShowMessage(Page, "Please Select Report Type", "info")
            End If
            Exit Sub
        End If
        If RadComboExtraReports.SelectedValue = EmployeeTask Then


            If SessionVariables.LoginUser.UserStatus = 2 And EmployeeFilter1.EntityId = 0 Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    CtlCommon.ShowMessage(Page, "الرجاء اختيار وحدة العمل", "info")
                Else
                    CtlCommon.ShowMessage(Page, "Please Select Entity", "info")
                End If
                Exit Sub
            End If
            If EmployeeFilter1.IsLevelRequired Then
                CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
                If EmployeeFilter1.CompanyId = 0 Then
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SelectCompany", CultureInfo), "info")
                ElseIf EmployeeFilter1.EntityId = 0 Then
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SelectEntity", CultureInfo), "info")
                Else
                    BindReport(reportName)
                End If
            Else
                BindReport(reportName)
            End If
        ElseIf RadComboExtraReports.SelectedValue = ProjectTask Then
            '  Dim reportName As String = RadComboExtraReports.SelectedValue
            If radproject.SelectedValue = "-1" Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    CtlCommon.ShowMessage(Page, "الرجاء اختيار المشروع", "info")
                Else
                    CtlCommon.ShowMessage(Page, "Please Select Project", "info")
                End If
                Exit Sub
            Else
                BindReport(reportName)
            End If
        End If
    End Sub

    Protected Sub RadComboExtraReports_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboExtraReports.SelectedIndexChanged


        If RadComboExtraReports.SelectedValue = EmployeeTask Then
            MultiView1.SetActiveView(vwEmployeeTask)
            radempFromDate.SelectedDate = Date.Now
            radempToDate.SelectedDate = Date.Now
        ElseIf RadComboExtraReports.SelectedValue = ProjectTask Then
            FillProjects()
            radProjectFromDatea.SelectedDate = Date.Now
            radProjectToDatea.SelectedDate = Date.Now
            MultiView1.SetActiveView(vwProjectTask)
        End If
    End Sub

#End Region

#Region "Methods"
    Public Sub FillProjects()
        CtlCommon.FillTelerikDropDownList(radproject, ObjProject.GetAll(), Lang)
    End Sub
    Public Shared Function GetPageDirection() As String
        If SessionVariables.CultureInfo = "ar-JO" Then
            Return "rtl"
        Else
            Return "ltr"
        End If
    End Function

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

    Private Sub FillReports(ByVal formID As Integer, ByVal groupID As Integer)

        CtlCommon.FillTelerikDropDownList(RadComboExtraReports, objsysforms.GetFormsByParentID(formID, groupID), Lang)

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("../Reports/ProjectReports.aspx")
    End Sub


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

            If RadComboExtraReports.SelectedValue = EmployeeTask Then

                dt2.Rows(0).Item("From_Date") = DateToString(radempFromDate.SelectedDate)
                dt2.Rows(0).Item("To_Date") = DateToString(radempToDate.SelectedDate)

            ElseIf RadComboExtraReports.SelectedValue = ProjectTask Then

                dt2.Rows(0).Item("From_Date") = DateToString(radProjectFromDatea.SelectedDate)
                dt2.Rows(0).Item("To_Date") = DateToString(radProjectToDatea.SelectedDate)

            End If


            cryRpt.Subreports(cryRpt.Subreports().Item(0).Name).SetDataSource(dt2)
            cryRpt.SetParameterValue("@UserName", UserName)



            Dim ShowSTLogo As Boolean
            ShowSTLogo = ConfigurationManager.AppSettings("ShowSmartTimeLogo")
            cryRpt.SetParameterValue("@ShowSTLogo", False)
            cryRpt.SetParameterValue("@ShowSTLogo", False, "rptHeader")
            cryRpt.SetParameterValue("@ReportName", RadComboExtraReports.SelectedItem.Text)
            cryRpt.SetParameterValue("@ReportName", RadComboExtraReports.SelectedItem.Text, "rptHeader")
            If RadComboExtraReports.SelectedValue = EmployeeTask Then

                cryRpt.SetParameterValue("@FromDate", DateToString(radempFromDate.SelectedDate))
                cryRpt.SetParameterValue("@ToDate", DateToString(radempToDate.SelectedDate))

            ElseIf RadComboExtraReports.SelectedValue = ProjectTask Then

                cryRpt.SetParameterValue("@FromDate", DateToString(radProjectFromDatea.SelectedDate))
                cryRpt.SetParameterValue("@ToDate", DateToString(radProjectToDatea.SelectedDate))

            End If


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
                        'ExportDataSetToExcel(NDT, "ExportedReport")
                        ExportDataSetToExcel(NDT, "ExportedReport", HeadDT)
                    End If
                    '   MultiView1.SetActiveView(Report)
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

    Private Sub SetReportName(ByVal rpttid As String)
        Dim rptObj As New Report

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

        If RadComboExtraReports.SelectedValue = EmployeeTask Then

            If Not radempFromDate.SelectedDate Is Nothing Then
                rptObj.FROM_DATE = radempFromDate.SelectedDate
            End If

            If Not radempToDate.SelectedDate Is Nothing Then
                rptObj.TO_DATE = radempToDate.SelectedDate
            End If

        ElseIf RadComboExtraReports.SelectedValue = ProjectTask Then

            If Not radProjectFromDatea.SelectedDate Is Nothing Then
                rptObj.FROM_DATE = radProjectFromDatea.SelectedDate
            End If

            If Not radProjectToDatea.SelectedDate Is Nothing Then
                rptObj.TO_DATE = radProjectToDatea.SelectedDate
            End If

        End If
    

        If rpttid = EmployeeTask Then
            If EmployeeFilter1.ShowDirectStaffCheck = True Then
                rptObj.DirectStaffOnly = EmployeeFilter1.DirectStaffOnly
            Else
                rptObj.DirectStaffOnly = False
            End If

            If rdbEmpCompleted.SelectedValue = "1" Then
                rptObj.IsCompleted = Nothing
            ElseIf rdbEmpCompleted.SelectedValue = "2" Then
                rptObj.IsCompleted = True
            ElseIf rdbEmpCompleted.SelectedValue = "3" Then
                rptObj.IsCompleted = False
            End If

            RPTName = "rptEmployeeTasks.rpt"
            DT = rptObj.GetEmployeeProjectTasks()
        ElseIf rpttid = ProjectTask Then
            rptObj.ProjectId = radproject.SelectedValue
            If rdbIsCompleted.SelectedValue = "1" Then
                rptObj.IsCompleted = Nothing
            ElseIf rdbIsCompleted.SelectedValue = "2" Then
                rptObj.IsCompleted = True
            ElseIf rdbIsCompleted.SelectedValue = "3" Then
                rptObj.IsCompleted = False
            End If

            RPTName = "rptProjectTasks.rpt"
            DT = rptObj.GetProjectTasks()
        End If

        UserName = SessionVariables.LoginUser.UsrID
        ' Version = System.Configuration.ConfigurationManager.AppSettings("Version").ToString()

        If (Lang = CtlCommon.Lang.AR) Then
            RPTName = "Arabic/" + RPTName
        Else
            RPTName = "English/" + RPTName
        End If


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

            '------Column(1)-----------
            HeadDT(1)(0) = SessionVariables.LoginUser.UsrID
            HeadDT(2)(0) = Date.Today.ToShortDateString

            If RadComboExtraReports.SelectedValue = EmployeeTask Then
                HeadDT(3)(1) = "من تاريخ: " & Convert.ToDateTime(radempFromDate.SelectedDate).ToShortDateString
                HeadDT(3)(0) = "الى تاريخ: " & Convert.ToDateTime(radempToDate.SelectedDate).ToShortDateString

            ElseIf RadComboExtraReports.SelectedValue = ProjectTask Then
                HeadDT(3)(1) = "من تاريخ: " & Convert.ToDateTime(radProjectFromDatea.SelectedDate).ToShortDateString
                HeadDT(3)(0) = "الى تاريخ: " & Convert.ToDateTime(radProjectToDatea.SelectedDate).ToShortDateString

            End If
        Else
            HeadDT(0)(0) = dtFormName(0)("Desc_En").ToString
            '------Column(0)-----------
            HeadDT(1)(0) = "Printed By:"
            HeadDT(2)(0) = "Printing Date:"

            '------Column(1)-----------
            HeadDT(1)(1) = SessionVariables.LoginUser.UsrID
            HeadDT(2)(1) = Date.Today.ToShortDateString


            If RadComboExtraReports.SelectedValue = EmployeeTask Then
                HeadDT(3)(0) = "From Date: " & Convert.ToDateTime(radempFromDate.SelectedDate).ToShortDateString
                HeadDT(3)(1) = "To Date: " & Convert.ToDateTime(radempToDate.SelectedDate).ToShortDateString

            ElseIf RadComboExtraReports.SelectedValue = ProjectTask Then
                HeadDT(3)(0) = "From Date: " & Convert.ToDateTime(radProjectFromDatea.SelectedDate).ToShortDateString
                HeadDT(3)(1) = "To Date: " & Convert.ToDateTime(radProjectToDatea.SelectedDate).ToShortDateString

            End If

        End If

        HeadDT.Rows.Add(drFormName)
        '------------------ADD REPORT HEADER-----------------

        Select Case reportname
            Case EmployeeTask
                If (Lang = CtlCommon.Lang.AR) Then

                    NDT.Columns(3).DataType = System.Type.GetType("System.String")
                    NDT.Columns(4).DataType = System.Type.GetType("System.String")
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")

                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 3 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            End If
                            If i = 4 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            End If
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            End If
                            If i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Project Name"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(4).ColumnName = "Task Completed"
                    NDT.Columns(7).ColumnName = "Complete Percentage"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "Entity Name"

                    NDT.Columns(0).ColumnName = "اسم المشروع"
                    NDT.Columns(1).ColumnName = "تاريخ بداية المشروع"
                    NDT.Columns(2).ColumnName = "تاريخ انتهاء المشروع"
                    NDT.Columns(3).ColumnName = "اسم المهمة"
                    NDT.Columns(4).ColumnName = "المهمة اكتملت"

                    NDT.Columns(5).ColumnName = "تاريخ بداية المهمة"

                    NDT.Columns(6).ColumnName = "تاريخ انتهاء المهمة"

                    NDT.Columns(7).ColumnName = "نسبة الاكتمال"

                    NDT.Columns(8).ColumnName = "رقم الموظف"
                    NDT.Columns(9).ColumnName = "اسم الموظف"
                    NDT.Columns(10).ColumnName = "مجموع الوقت الفعلي"
                    NDT.Columns(11).ColumnName = "اسم الشركة"
                    NDT.Columns(12).ColumnName = "اسم وحدة العمل"


                    NDT.SetColumnsOrder(New String() {"رقم الموظف",
                                   "اسم الموظف",
                                   "اسم الشركة",
                                 "اسم وحدة العمل",
                                  "اسم المشروع",
                                  "تاريخ بداية المشروع",
                                   "تاريخ انتهاء المشروع",
                                    "اسم المهمة",
                                  "تاريخ بداية المهمة",
                                  "تاريخ انتهاء المهمة",
                                   "المهمة اكتملت",
                                   "نسبة الاكتمال",
                                  "مجموع الوقت الفعلي"})

                Else


                    NDT.Columns(3).DataType = System.Type.GetType("System.String")
                    NDT.Columns(4).DataType = System.Type.GetType("System.String")
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")

                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 3 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            End If
                            If i = 4 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            End If
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            End If
                            If i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next


                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Project Name"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(4).ColumnName = "Task Completed"
                    NDT.Columns(7).ColumnName = "Complete Percentage"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(13)



                    'NDT.SetColumnsOrder(New String() {"Employee No.",
                    '                "Employee Name",
                    '                "Entity Name",
                    '                "Company Name",
                    '                "Schedule Name",
                    '                "Date",
                    '                "Day",
                    '                "First In",
                    '                "Last Out",
                    '                "Delay",
                    '                "Early Out",
                    '                "Duration",
                    '                "Lost Time",
                    '                "Remarks",
                    '                "OverTime"})


                End If
            Case ProjectTask
                If (Lang = CtlCommon.Lang.AR) Then

                    NDT.Columns(3).DataType = System.Type.GetType("System.String")
                    NDT.Columns(4).DataType = System.Type.GetType("System.String")
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")

                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 3 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            End If
                            If i = 4 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            End If
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            End If
                            If i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Project Name"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(4).ColumnName = "Task Completed"
                    NDT.Columns(7).ColumnName = "Complete Percentage"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "Entity Name"

                    NDT.Columns(0).ColumnName = "اسم المشروع"
                    NDT.Columns(1).ColumnName = "تاريخ بداية المشروع"
                    NDT.Columns(2).ColumnName = "تاريخ انتهاء المشروع"
                    NDT.Columns(3).ColumnName = "اسم المهمة"
                    NDT.Columns(4).ColumnName = "المهمة اكتملت"

                    NDT.Columns(5).ColumnName = "تاريخ بداية المهمة"

                    NDT.Columns(6).ColumnName = "تاريخ انتهاء المهمة"

                    NDT.Columns(7).ColumnName = "نسبة الاكتمال"

                    NDT.Columns(8).ColumnName = "رقم الموظف"
                    NDT.Columns(9).ColumnName = "اسم الموظف"
                    NDT.Columns(10).ColumnName = "مجموع الوقت الفعلي"
                    NDT.Columns(11).ColumnName = "الشركة"
                    NDT.Columns(12).ColumnName = "وحدة العمل"





                Else


                    NDT.Columns(3).DataType = System.Type.GetType("System.String")
                    NDT.Columns(4).DataType = System.Type.GetType("System.String")
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")

                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 3 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            End If
                            If i = 4 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            End If
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            End If
                            If i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next


                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Project Name"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(4).ColumnName = "Task Completed"
                    NDT.Columns(7).ColumnName = "Complete Percentage"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(13)



                    'NDT.SetColumnsOrder(New String() {"Employee No.",
                    '                "Employee Name",
                    '                "Entity Name",
                    '                "Company Name",
                    '                "Schedule Name",
                    '                "Date",
                    '                "Day",
                    '                "First In",
                    '                "Last Out",
                    '                "Delay",
                    '                "Early Out",
                    '                "Duration",
                    '                "Lost Time",
                    '                "Remarks",
                    '                "OverTime"})


                End If
        End Select

    End Sub

#End Region
End Class
