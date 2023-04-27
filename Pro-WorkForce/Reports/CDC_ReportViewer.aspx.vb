Imports System.Data
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports SmartV.UTILITIES
Imports TA.Employees
Imports TA.Admin
Imports SmartV.UTILITIES.CtlCommon
Imports TA.Reports
Imports TA.Definitions
Imports TA.Security
Imports TA.Forms
Partial Class Reports_CDC_ReportViewer
    Inherits System.Web.UI.Page

    Private objAPP_Settings As APP_Settings
    Private Lang As CtlCommon.Lang
    Protected dir As String
    Private objSys_Forms As New Sys_Forms

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
#End Region

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

    Public Shared Function GetPageDirection() As String
        If SessionVariables.CultureInfo = "ar-JO" Then
            Return "rtl"
        Else
            Return "ltr"
        End If
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "سجل الاحداث ", "CDC Report")
            Page.Title = "Work Force Pro : :" + IIf(Lang = CtlCommon.Lang.AR, "سجل الاحداث ", "CDC Report")
            RadDatePicker1.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd As New Date
            dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            RadDatePicker2.SelectedDate = dd
            FillModules()

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

    Private Sub FillModules()
        objSys_Forms = New Sys_Forms
        CtlCommon.FillTelerikDropDownList(radcmbbxModule, objSys_Forms.GetAll_CDCTables, Lang)
    End Sub
    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        BindReport()
    End Sub
    Public Sub BindReport()
        Dim SERVER_NAME As String = ConfigurationManager.AppSettings("ServerName").ToString()
        Dim DATABASE_NAME As String = ConfigurationManager.AppSettings("DataBaseName").ToString()
        Dim DatabaseUser As String = ConfigurationManager.AppSettings("DatabaseUser").ToString()
        Dim DatabasePassword As String = ConfigurationManager.AppSettings("DatabasePassword").ToString()
        Dim rptObj As New Report
        Dim cryRpt As New ReportDocument

        RPTName = "rpt_CDC.rpt"
        If (Lang = CtlCommon.Lang.AR) Then
            RPTName = "Arabic/" + RPTName
        Else
            RPTName = "English/" + RPTName
        End If
        cryRpt.Load(Server.MapPath(RPTName))

        If Not RadDatePicker1.SelectedDate Is Nothing Then
            rptObj.FROM_DATE = RadDatePicker1.SelectedDate
        End If

        If Not RadDatePicker2.SelectedDate Is Nothing Then
            rptObj.TO_DATE = RadDatePicker2.SelectedDate
        End If

        If Not radcmbbxModule.SelectedValue Is Nothing Then
            rptObj.CDC_Module = radcmbbxModule.SelectedValue
        Else
            rptObj.CDC_Module = Nothing
        End If

        If radcmbbxOperation.SelectedValue <> Nothing Or radcmbbxOperation.SelectedValue <> 0 Then
            rptObj.Operation = radcmbbxOperation.SelectedValue
        Else
            rptObj.Operation = Nothing
        End If
        Dim objAPP_Settings As New APP_Settings
        dt2 = objAPP_Settings.GetHeader()
        dt2.Columns.Add("From_Date")
        dt2.Columns.Add("To_Date")
        dt2.Rows(0).Item("From_Date") = DateToString(RadDatePicker1.SelectedDate)
        dt2.Rows(0).Item("To_Date") = DateToString(RadDatePicker2.SelectedDate)
        cryRpt.Subreports("rptHeader").SetDataSource(dt2)
        ' If radcmbbxModule.SelectedValue = "APP_Settings" Then
        Dim DTAppSettings As DataTable = rptObj.GetCDC_AppSettings
        cryRpt.Subreports("App_Settings1").SetDataSource(DTAppSettings)
        ' ElseIf radcmbbxModule.SelectedValue = "Emp_ATTCard" Then
        Dim DTEmp_ATTCard As DataTable = rptObj.GetCDC_Emp_ATTCard
        cryRpt.Subreports("Emp_ATTCard").SetDataSource(DTEmp_ATTCard)
        ' ElseIf radcmbbxModule.SelectedValue = "Emp_OverTimeRule" Then
        Dim DTEmp_OverTimeRule As DataTable = rptObj.GetCDC_Emp_OverTimeRule
        cryRpt.Subreports("Emp_OverTimeRule").SetDataSource(DTEmp_OverTimeRule)
        '  ElseIf radcmbbxModule.SelectedValue = "Emp_WorkSchedule" Then
        Dim DTEmp_WorkSchedule As DataTable = rptObj.GetCDC_Emp_WorkSchedule
        cryRpt.Subreports("Emp_WorkSchedule").SetDataSource(DTEmp_WorkSchedule)
        '  ElseIf radcmbbxModule.SelectedValue = "Employee" Then
        Dim DTEmployee As DataTable = rptObj.GetCDC_Employee
        cryRpt.Subreports("Employee").SetDataSource(DTEmployee)
        '   ElseIf radcmbbxModule.SelectedValue = "Employee_TAPolicy" Then
        Dim DTEmployee_TAPolicy As DataTable = rptObj.GetCDC_Employee_TAPolicy
        cryRpt.Subreports("Employee_TAPolicy").SetDataSource(DTEmployee_TAPolicy)
        '  ElseIf radcmbbxModule.SelectedValue = "LeaveTypeoccurance" Then
        Dim DTLeaveTypeoccurance As DataTable = rptObj.GetCDC_LeaveTypeoccurance
        cryRpt.Subreports("LeaveTypeoccurance").SetDataSource(DTLeaveTypeoccurance)
        '  ElseIf radcmbbxModule.SelectedValue = "PermissionTypeDuration" Then
        Dim DTPermissionTypeDuration As DataTable = rptObj.GetCDC_PermissionTypeDuration
        cryRpt.Subreports("PermissionTypeDuration").SetDataSource(DTPermissionTypeDuration)
        '   ElseIf radcmbbxModule.SelectedValue = "PermissionTypeOccurance" Then
        Dim DTPermissionTypeOccurance As DataTable = rptObj.GetCDC_PermissionTypeOccurance
        cryRpt.Subreports("PermissionTypeOccurance").SetDataSource(DTPermissionTypeOccurance)
        '    ElseIf radcmbbxModule.SelectedValue = "Schedule_Company" Then
        Dim DTSchedule_Company As DataTable = rptObj.GetCDC_Schedule_Company
        cryRpt.Subreports("Schedule_Company").SetDataSource(DTSchedule_Company)
        '  ElseIf radcmbbxModule.SelectedValue = "Schedule_Entity" Then
        Dim DTSchedule_Entity As DataTable = rptObj.GetCDC_Schedule_Entity
        cryRpt.Subreports("Schedule_Entity").SetDataSource(DTSchedule_Entity)
        '  ElseIf radcmbbxModule.SelectedValue = "Schedule_LogicalGroup" Then
        Dim DTSchedule_LogicalGroup As DataTable = rptObj.GetCDC_Schedule_LogicalGroup
        cryRpt.Subreports("Schedule_LogicalGroup").SetDataSource(DTSchedule_LogicalGroup)
        '   ElseIf radcmbbxModule.SelectedValue = "Schedule_WorkLocation" Then
        Dim DTSchedule_WorkLocation As DataTable = rptObj.GetCDC_Schedule_WorkLocation
        cryRpt.Subreports("Schedule_WorkLocation").SetDataSource(DTSchedule_WorkLocation)
        '    ElseIf radcmbbxModule.SelectedValue = "TAPolicy_AbsentRule" Then
        Dim DTTAPolicy_AbsentRule As DataTable = rptObj.GetCDC_TAPolicy_AbsentRule
        cryRpt.Subreports("TAPolicy_AbsentRule").SetDataSource(DTTAPolicy_AbsentRule)
        '  ElseIf radcmbbxModule.SelectedValue = "TAPolicy_Violation" Then
        Dim DTTAPolicy_Violation As DataTable = rptObj.GetCDC_TAPolicy_Violation
        cryRpt.Subreports("TAPolicy_Violation").SetDataSource(DTTAPolicy_Violation)
        '  End If


        UserName = SessionVariables.LoginUser.UsrID
        'Version = System.Configuration.ConfigurationManager.AppSettings("Version").ToString()
        Version = SmartV.Version.version.GetVersionNumber
        DT = Nothing


        cryRpt.SetParameterValue("@Module", radcmbbxModule.SelectedValue)
        cryRpt.SetParameterValue("@UserName", UserName)


        cryRpt.SetDataSource(DT)
        
        'cryRpt.Subreports(cryRpt.Subreports().Item(0).Name).SetDataSource(dt2)
        cryRpt.SetDatabaseLogon(DatabaseUser, DatabasePassword, SERVER_NAME, DATABASE_NAME)
        CRV.ReportSource = cryRpt
        If rblFormat.SelectedValue = 1 Then
            cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedReport")
        ElseIf rblFormat.SelectedValue = 2 Then
            cryRpt.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, True, "ExportedReport")
        ElseIf rblFormat.SelectedValue = 3 Then
            cryRpt.ExportToHttpResponse(ExportFormatType.Excel, Response, True, "ExportedReport")
        End If


        MultiView1.SetActiveView(Report)

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
        Dim cryRpt As New ReportDocument
        If Not RadDatePicker1.SelectedDate Is Nothing Then
            rptObj.FROM_DATE = RadDatePicker1.SelectedDate
        End If

        If Not RadDatePicker2.SelectedDate Is Nothing Then
            rptObj.TO_DATE = RadDatePicker2.SelectedDate
        End If

        If Not radcmbbxModule.SelectedValue Is Nothing Then
            rptObj.CDC_Module = radcmbbxModule.SelectedValue
        Else
            rptObj.CDC_Module = Nothing
        End If

        If radcmbbxOperation.SelectedValue <> Nothing Or radcmbbxOperation.SelectedValue <> 0 Then
            rptObj.Operation = radcmbbxOperation.SelectedValue
        Else
            rptObj.Operation = Nothing
        End If
       
       

    End Sub
End Class
