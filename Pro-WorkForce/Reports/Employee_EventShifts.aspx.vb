Imports System.Data
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports SmartV.UTILITIES
Imports TA.Admin
Imports SmartV.UTILITIES.CtlCommon
Imports TA.Reports
Imports TA.Security
Imports System.Resources
Imports System.Web.Configuration
Imports TA.Forms
Imports TA.Definitions
Imports TA.Employees
Partial Class Reports_Employee_EventShifts
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objEvents As Events
    Private objEvents_Groups As Events_Groups
    Private objEvents_Employees As Events_Employees
    Private objAPP_Settings As APP_Settings
    Private objEmployee As Employee
    Private Lang As CtlCommon.Lang
    Protected dir As String
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Dim cryRpt As New ReportDocument

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
            FillEvents()
            RadDatePicker1.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd As New Date
            dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)

            RadDatePicker2.SelectedDate = dd
            lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "مناوبات موظفي الحدث\المشروع", "Employee Event\Project Shifts")

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

    Protected Sub RadCmbBxEvent_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxEvent.SelectedIndexChanged
        If Not RadCmbBxEvent.SelectedValue = -1 Then
            FillEventGroups()
        Else
            RadCmbBxGroup.Items.Clear()
            RadCmbBxGroup.Text = String.Empty
            RadCmbBxEmployee.Items.Clear()
            RadCmbBxEmployee.Text = String.Empty
        End If

    End Sub

    Protected Sub RadCmbBxGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxGroup.SelectedIndexChanged
        If Not RadCmbBxGroup.SelectedValue = -1 Then
            FillEmployee()
        Else
            RadCmbBxEmployee.Items.Clear()
            RadCmbBxEmployee.Text = String.Empty
        End If
    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        BindReport()
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

    Private Sub FillEvents()
        objEvents = New Events
        Dim dt As DataTable
        With objEvents
            dt = .GetAll
            CtlCommon.FillTelerikDropDownList(RadCmbBxEvent, dt, Lang)
        End With
    End Sub

    Private Sub FillEventGroups()
        objEvents_Employees = New Events_Employees
        Dim dt As DataTable
        With objEvents_Employees
            If Not RadCmbBxEvent.SelectedValue = -1 Then
                .FK_EventId = RadCmbBxEvent.SelectedValue
                dt = .Get_EventGroups
                CtlCommon.FillTelerikDropDownList(RadCmbBxGroup, dt, Lang)
            End If
        End With
    End Sub

    Private Sub FillEmployee()
        objEmployee = New Employee
        Dim dt As DataTable
        With objEmployee
            .FK_LogicalGroup = RadCmbBxGroup.SelectedValue
            .FK_EntityId = -1
            If Not RadCmbBxGroup.SelectedValue = -1 Then
                dt = .GetByLogicalGroup()
                CtlCommon.FillTelerikDropDownList(RadCmbBxEmployee, dt, Lang)
            End If
        End With
    End Sub

    Public Sub BindReport()
        SetReportName()
        cryRpt = New ReportDocument
        cryRpt.Load(Server.MapPath(RPTName))
        cryRpt.SetDataSource(DT)
        Dim objAPP_Settings As New APP_Settings
        objAPP_Settings.FK_CompanyId = SessionVariables.LoginUser.FK_CompanyId
        dt2 = objAPP_Settings.GetHeader()
        dt2.Columns.Add("From_Date")
        dt2.Columns.Add("To_Date")
        dt2.Rows(0).Item("From_Date") = DateToString(RadDatePicker1.SelectedDate)
        dt2.Rows(0).Item("To_Date") = DateToString(RadDatePicker2.SelectedDate)
        cryRpt.Subreports(cryRpt.Subreports().Item(0).Name).SetDataSource(dt2)
        cryRpt.SetParameterValue("@UserName", UserName)

        Dim ShowSTLogo As Boolean
        ShowSTLogo = ConfigurationManager.AppSettings("ShowSmartTimeLogo")
        cryRpt.SetParameterValue("@ShowSTLogo", False)
        cryRpt.SetParameterValue("@ShowSTLogo", False, "rptHeader")

        CRV.ReportSource = cryRpt
        If Not DT Is Nothing Then
            If DT.Rows.Count > 0 Then
                If rblFormat.SelectedValue = 1 Then
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

        If RadCmbBxEvent.SelectedValue <> -1 Then
            rptObj.EventId = RadCmbBxEvent.SelectedValue
        Else
            rptObj.EventId = Nothing
        End If

        If Not RadCmbBxGroup.SelectedValue = Nothing Then
            If RadCmbBxGroup.SelectedValue <> -1 Then
                rptObj.GroupId = RadCmbBxGroup.SelectedValue
            Else
                rptObj.GroupId = Nothing
            End If
        End If

        If Not RadCmbBxEmployee.SelectedValue = Nothing Then
            If RadCmbBxEmployee.SelectedValue <> -1 Then
                rptObj.EmployeeId = RadCmbBxEmployee.SelectedValue
            Else
                rptObj.EmployeeId = Nothing
            End If
        End If

        If Not RadDatePicker1.SelectedDate Is Nothing Then
            rptObj.FROM_DATE = RadDatePicker1.DbSelectedDate
        End If
        If Not RadDatePicker2.SelectedDate Is Nothing Then
            rptObj.TO_DATE = RadDatePicker2.DbSelectedDate
        End If

        RPTName = "rptEmployee_EventShifts.rpt"
        DT = rptObj.GetEmployeeEvent_Shifts()

        UserName = SessionVariables.LoginUser.UsrID

        If (Lang = CtlCommon.Lang.AR) Then
            RPTName = "Arabic/" + RPTName
        Else
            RPTName = "English/" + RPTName
        End If

    End Sub

    Private Sub FillExcelreport()
        NDT = New DataTable
        NDT = DT.Clone

        Dim UK_Culture As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("en-GB")

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
        If (Lang = CtlCommon.Lang.AR) Then
            NDT.Columns.RemoveAt(0)
            NDT.Columns(0).ColumnName = "رقم الموظف"
            NDT.Columns(1).ColumnName = "اسم الموظف"
            NDT.Columns(2).ColumnName = "اسم الموظف باللغة العربية"
            NDT.Columns.RemoveAt(3)
            NDT.Columns(3).ColumnName = "اسم مجموعة العمل"
            NDT.Columns(4).ColumnName = "اسم مجموعة العمل باللغة العربية"
            NDT.Columns.RemoveAt(5)
            NDT.Columns(5).ColumnName = "اسم الحدث\المشروع"
            NDT.Columns(6).ColumnName = "اسم المناوبة"
            NDT.Columns(7).ColumnName = "تاريخ الجدول"
        Else
            NDT.Columns.RemoveAt(0)
            NDT.Columns(0).ColumnName = "Employee No"
            NDT.Columns(1).ColumnName = "Employee Name"
            NDT.Columns(2).ColumnName = "Employee Arabic Name"
            NDT.Columns.RemoveAt(3)
            NDT.Columns(3).ColumnName = "Group Name"
            NDT.Columns(4).ColumnName = "Group Arabic Name"
            NDT.Columns.RemoveAt(5)
            NDT.Columns(5).ColumnName = "Event\Project Name"
            NDT.Columns(6).ColumnName = "Shift Name"
            NDT.Columns(7).ColumnName = "Schedule Date"
        End If
    End Sub

#End Region

End Class
