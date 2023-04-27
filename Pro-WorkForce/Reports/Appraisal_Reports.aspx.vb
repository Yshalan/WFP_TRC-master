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

Partial Class Reports_Appraisal_Reports
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
    Private objSYSUsers As SYSUsers

    Private DetailedAppraisal As String = "DetailedAppraisal"
    Private AppraisalResult As String = "AppraisalResult"

#End Region

#Region "Public Properties"

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

    Public Property subDT_Goals() As DataTable
        Get
            Return ViewState("subDT_Goals")
        End Get
        Set(ByVal value As DataTable)
            ViewState("subDT_Goals") = value
        End Set
    End Property

    Public Property subDT_Skills() As DataTable
        Get
            Return ViewState("subDT_Skills")
        End Get
        Set(ByVal value As DataTable)
            ViewState("subDT_Skills") = value
        End Set
    End Property

    Public Property NDS() As DataSet
        Get
            Return ViewState("NDS")
        End Get
        Set(ByVal value As DataSet)
            ViewState("NDS") = value
        End Set
    End Property

#End Region

#Region "Class Variables"

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

            FillReports(formID, SessionVariables.LoginUser.GroupId)

            ddlYear_Bind()

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
        Dim reportName As String = RadComboAppraisalReports.SelectedValue

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

#End Region

#Region "Methods"

    Public Shared Function GetPageDirection() As String
        If SessionVariables.CultureInfo = "ar-JO" Then
            Return "rtl"
        Else
            Return "ltr"
        End If
    End Function

    Private Sub FillReports(ByVal formID As Integer, ByVal groupID As Integer)

        CtlCommon.FillTelerikDropDownList(RadComboAppraisalReports, objsysforms.GetFormsByParentID(formID, groupID), Lang)

    End Sub

    Public Sub ClearFilter()
        EmployeeFilter1.ClearValues()

    End Sub

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

        ddlYear.SelectedValue = Now.Year
        ddlYear.DataSource = dt
        ddlYear.DataBind()
    End Sub

    Public Sub BindReport(ByVal reportName As String)
        If Not RadComboAppraisalReports.SelectedValue = "-1" Then
            SetReportName(reportName)

            cryRpt = New ReportDocument

            cryRpt.Load(Server.MapPath(RPTName))
            cryRpt.SetDataSource(DT)
            Dim objAPP_Settings As New APP_Settings
            objAPP_Settings.FK_CompanyId = EmployeeFilter1.CompanyId
            dt2 = objAPP_Settings.GetHeader()
            dt2.Columns.Add("Year")
            dt2.Rows(0).Item("Year") = ddlYear.SelectedValue
            'If RadComboAppraisalReports.SelectedValue = DetailedAppraisal Then
            cryRpt.Subreports("AppraisalGoals").SetDataSource(subDT_Goals)
            cryRpt.Subreports("AppraisalSkills").SetDataSource(subDT_Skills)
            'End If
            cryRpt.Subreports("rptHeader").SetDataSource(dt2)



            cryRpt.SetParameterValue("@UserName", UserName)

            Dim ShowSTLogo As Boolean
            ShowSTLogo = ConfigurationManager.AppSettings("ShowSmartTimeLogo")
            cryRpt.SetParameterValue("@ShowSTLogo", False)
            cryRpt.SetParameterValue("@ShowSTLogo", False, "rptHeader")
            cryRpt.SetParameterValue("@ReportName", RadComboAppraisalReports.SelectedItem.Text)
            cryRpt.SetParameterValue("@ReportName", RadComboAppraisalReports.SelectedItem.Text, "rptHeader")
            cryRpt.SetParameterValue("@Year", ddlYear.SelectedValue)

            cryRpt.SetParameterValue("@LogoURL", "", "rptHeader")



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
                        If RadComboAppraisalReports.SelectedValue = DetailedAppraisal Then
                            ExportMultipleDataSetToExcel(NDS, "ExportedReport")
                        Else
                            ExportDataSetToExcel(NDT, "ExportedReport", HeadDT)
                        End If

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

        rptObj.Year = ddlYear.SelectedValue

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

        If EmployeeFilter1.ShowDirectStaffCheck = True Then
            rptObj.DirectStaffOnly = EmployeeFilter1.DirectStaffOnly
        Else
            rptObj.DirectStaffOnly = False
        End If


        Select Case rpttid
            Case DetailedAppraisal
                RPTName = "rptDetailedAppraisal.rpt"
                DT = rptObj.Get_DetailedAppraisal()
                subDT_Goals = rptObj.Get_DetailedAppraisal_Goals_Sub
                subDT_Skills = rptObj.Get_DetailedAppraisal_Skills_Sub
        End Select

        UserName = SessionVariables.LoginUser.UsrID

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
        Dim rptName As String = RadComboAppraisalReports.SelectedValue
        Dim dtFormName As DataTable
        dtFormName = objsysforms.GetBy_FormName(rptName)
        If Lang = CtlCommon.Lang.AR Then

            HeadDT(0)(0) = dtFormName(0)("Desc_Ar")
            '------Column(0)-----------
            HeadDT(1)(1) = "طبع بواسطة:"
            HeadDT(2)(1) = "تاريخ الطباعة:"
            HeadDT(3)(1) = "السنة: " & ddlYear.SelectedValue
            '------Column(1)-----------
            HeadDT(1)(0) = SessionVariables.LoginUser.UsrID
            HeadDT(2)(0) = Date.Today.ToShortDateString
            HeadDT(3)(0) = ""

        Else
            HeadDT(0)(0) = dtFormName(0)("Desc_En").ToString
            '------Column(0)-----------
            HeadDT(1)(0) = "Printed By:"
            HeadDT(2)(0) = "Printing Date:"
            HeadDT(3)(0) = "Year: " & ddlYear.SelectedValue
            '------Column(1)-----------
            HeadDT(1)(1) = SessionVariables.LoginUser.UsrID
            HeadDT(2)(1) = Date.Today.ToShortDateString
            HeadDT(3)(1) = ""
        End If

        HeadDT.Rows.Add(drFormName)
        '------------------ADD REPORT HEADER-----------------

        Select Case reportname

            Case DetailedAppraisal

                Dim subNDT_Goals As New DataTable
                subNDT_Goals = subDT_Goals.Clone

                Dim subNDT_Skills As New DataTable
                subNDT_Skills = subDT_Skills.Clone

                NDT = DT
                subNDT_Goals = subDT_Goals
                subNDT_Skills = subDT_Skills

                Dim ds As New DataSet
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "السنة"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)

                    '-----Format Goals Sub Report Header-----'
                    subNDT_Goals.Columns(0).ColumnName = "اسم الهدف"
                    subNDT_Goals.Columns(1).ColumnName = "تفاصيل الهدف"
                    subNDT_Goals.Columns(2).ColumnName = "التسلسل"
                    subNDT_Goals.Columns(3).ColumnName = "الوزن"
                    subNDT_Goals.Columns.RemoveAt(4)
                    subNDT_Goals.Columns.RemoveAt(4)
                    subNDT_Goals.Columns(4).ColumnName = "نقاط التقييم"
                    subNDT_Goals.Columns.RemoveAt(5)
                    subNDT_Goals.Columns(5).ColumnName = "ملاحظات التقييم"
                    subNDT_Goals.Columns.RemoveAt(6)

                    '-----Format Skills Sub Report Header-----'
                    subNDT_Skills.Columns(0).ColumnName = "اسم المهارة"
                    subNDT_Skills.Columns(1).ColumnName = "اسم المهارة باللغة العربية"
                    subNDT_Skills.Columns(2).ColumnName = "الوزن"
                    subNDT_Skills.Columns.RemoveAt(3)
                    subNDT_Skills.Columns.RemoveAt(3)
                    subNDT_Skills.Columns(3).ColumnName = "نقاط التقييم"
                    subNDT_Skills.Columns.RemoveAt(4)
                    subNDT_Skills.Columns(4).ColumnName = "ملاحظات التقييم"
                    subNDT_Skills.Columns.RemoveAt(5)

                Else

                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Year"
                    NDT.Columns(3).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)

                    '-----Format Goals Sub Report Header-----'
                    subNDT_Goals.Columns(0).ColumnName = "Goal Name"
                    subNDT_Goals.Columns(1).ColumnName = "Goal Details"
                    subNDT_Goals.Columns(2).ColumnName = "Sequence"
                    subNDT_Goals.Columns(3).ColumnName = "Weight"
                    subNDT_Goals.Columns.RemoveAt(4)
                    subNDT_Goals.Columns.RemoveAt(4)
                    subNDT_Goals.Columns(4).ColumnName = "Evaluation Points"
                    subNDT_Goals.Columns.RemoveAt(5)
                    subNDT_Goals.Columns(5).ColumnName = "Evaluation Remarks"
                    subNDT_Goals.Columns.RemoveAt(6)

                    '-----Format Skills Sub Report Header-----'
                    subNDT_Skills.Columns(0).ColumnName = "Skill Name"
                    subNDT_Skills.Columns(1).ColumnName = "Skill Arabic Name"
                    subNDT_Skills.Columns(2).ColumnName = "Weight"
                    subNDT_Skills.Columns.RemoveAt(3)
                    subNDT_Skills.Columns.RemoveAt(3)
                    subNDT_Skills.Columns(3).ColumnName = "Evaluation Points"
                    subNDT_Skills.Columns.RemoveAt(4)
                    subNDT_Skills.Columns(4).ColumnName = "Evaluation Remarks"
                    subNDT_Skills.Columns.RemoveAt(5)
                End If
                ds.Tables.Add(HeadDT)
                ds.Tables.Add(NDT)
                ds.Tables.Add(subNDT_Goals)
                ds.Tables.Add(subNDT_Skills)
                NDS = ds
        End Select
    End Sub

#End Region

End Class
