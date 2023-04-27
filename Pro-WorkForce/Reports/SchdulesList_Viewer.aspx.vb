Imports System.Data
Imports TA.Admin
Imports SmartV.UTILITIES
Imports CrystalDecisions.CrystalReports.Engine
Imports TA.Security
Imports Telerik.Web.UI
Imports TA.Employees
Imports TA.Reports
Imports SmartV.UTILITIES.CtlCommon
Imports CrystalDecisions.Shared
Imports TA.Definitions

Partial Class Reports_SchdulesList_Viewer
    Inherits System.Web.UI.Page

#Region "Class Variables"
    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Protected dir As String
    Dim cryRpt As New ReportDocument
    Dim CultureInfo As System.Globalization.CultureInfo
    Dim objSmartSecurity As New SmartSecurity
    Dim objAPP_Settings As APP_Settings
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
    Public Property RPTName() As String
        Get
            Return ViewState("RPTName")
        End Get
        Set(ByVal value As String)
            ViewState("RPTName") = value
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
    Public Property dt2() As DataTable
        Get
            Return ViewState("dt2")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dt2") = value
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

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            ClearAll()
            Dim rptid As Integer = Request.QueryString("ReportId")

            Select Case rptid
                Case 1 '' Schdules List
                    lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "قائمة الجداول", "Schedules List")
            End Select



            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR

                Dim item1 As New RadComboBoxItem
                Dim item2 As New RadComboBoxItem
                Dim item3 As New RadComboBoxItem
                Dim item4 As New RadComboBoxItem
                item1.Value = -1
                item1.Text = "--الرجاء الاختيار--"
                RadComboScheduleType.Items.Add(item1)
                item2.Value = 1
                item2.Text = "عادي"
                RadComboScheduleType.Items.Add(item2)
                item3.Value = 2
                item3.Text = "مرن"
                RadComboScheduleType.Items.Add(item3)
                item4.Value = 3
                item4.Text = "مناوبات"
                RadComboScheduleType.Items.Add(item4)
            Else
                Lang = CtlCommon.Lang.EN

                Dim item1 As New RadComboBoxItem
                Dim item2 As New RadComboBoxItem
                Dim item3 As New RadComboBoxItem
                Dim item4 As New RadComboBoxItem
                item1.Value = -1
                item1.Text = "--Please Select--"
                RadComboScheduleType.Items.Add(item1)
                item2.Value = 1
                item2.Text = "Normal"
                RadComboScheduleType.Items.Add(item2)
                item3.Value = 2
                item3.Text = "Flexible"
                RadComboScheduleType.Items.Add(item3)
                item4.Value = 3
                item4.Text = "Advanced"
                RadComboScheduleType.Items.Add(item4)
            End If

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

    Protected Sub RadComboScheduleType_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboScheduleType.SelectedIndexChanged
        Try
            FillSchedule()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnPrint_Click(sender As Object, e As System.EventArgs) Handles btnPrint.Click
        Try
            Dim Id As Integer = Request.QueryString("ReportId")
            BindReport(Id)
            ClearAll()
        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region "Methods"
    Public Sub BindReport(ByVal Id As String)
        Try


            SetReportName(Id)
            cryRpt = New ReportDocument
            cryRpt.Load(Server.MapPath(RPTName))
            cryRpt.SetDataSource(DT)
            Dim objAPP_Settings As New APP_Settings
            objAPP_Settings.FK_CompanyId = SessionVariables.LoginUser.FK_CompanyId
            dt2 = objAPP_Settings.GetHeader()
            dt2.Columns.Add("From_Date")
            dt2.Columns.Add("To_Date")
            dt2.Rows(0).Item("From_Date") = Now.Date.ToString("dd/MM/yyyy")
            dt2.Rows(0).Item("To_Date") = Now.Date.ToString("dd/MM/yyyy")
            cryRpt.Subreports(cryRpt.Subreports().Item(0).Name).SetDataSource(dt2)
            'cryRpt.SetParameterValue("@Version", Version)
            cryRpt.SetParameterValue("@UserName", UserName)

            Dim ShowSTLogo As Boolean
            ShowSTLogo = ConfigurationManager.AppSettings("ShowSmartTimeLogo")
            cryRpt.SetParameterValue("@ShowSTLogo", False)
            cryRpt.SetParameterValue("@ShowSTLogo", False, "rptHeader")
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
                        cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedReport")
                    ElseIf rblFormat.SelectedValue = 2 Then
                        cryRpt.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, True, "ExportedReport")
                    ElseIf rblFormat.SelectedValue = 3 Then
                        FillExcelreport()
                        'ExportExcel()
                        ExportDataSetToExcel(NDT, "ExportedReport")
                    End If
                    MultiView1.SetActiveView(Report)
                Else
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NoDataFound", CultureInfo), "info")
                End If
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NoDataFound", CultureInfo), "info")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetReportName(ByVal rpttid As Integer)
        Try


            Dim rptObj As New Report
            If RadCmbBxScheduleName.SelectedValue = "" Then
                'RadCmbBxScheduleName.SelectedValue = -1

            End If
            If Not RadCmbBxScheduleName.SelectedValue Is Nothing Then
                rptObj.ScheduleId = Me.RadCmbBxScheduleName.SelectedValue
            End If
            If Not RadComboScheduleType.SelectedValue Is Nothing Then
                rptObj.ScheduleTypeId = Me.RadComboScheduleType.SelectedValue
            End If


            rpttid = IIf(rpttid = 0, 3, rpttid)
            Select Case rpttid

                Case 1 'Schedules List'
                    RPTName = "rpt_SchedulesList.rpt"
                    DT = rptObj.GetSchedulesList


            End Select

            UserName = SessionVariables.LoginUser.UsrID
            ' Version = System.Configuration.ConfigurationManager.AppSettings("Version").ToString()

            If (Lang = CtlCommon.Lang.AR) Then
                RPTName = "Arabic/" + RPTName
            Else
                RPTName = "English/" + RPTName
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Shared Function GetPageDirection() As String
        If SessionVariables.CultureInfo = "ar-JO" Then
            Return "rtl"
        Else
            Return "ltr"
        End If
    End Function

    Private Sub FillSchedule()
        If SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
        Else
            Lang = CtlCommon.Lang.EN
        End If

        Dim objWorkSchedule As New WorkSchedule()
        If (RadComboScheduleType.SelectedValue = 1) Then
            CtlCommon.FillTelerikDropDownList(RadCmbBxScheduleName, objWorkSchedule.GetByType(1), Lang)
        ElseIf (RadComboScheduleType.SelectedValue = 2) Then
            CtlCommon.FillTelerikDropDownList(RadCmbBxScheduleName, objWorkSchedule.GetByType(2), Lang)
        ElseIf (RadComboScheduleType.SelectedValue = 3) Then
            CtlCommon.FillTelerikDropDownList(RadCmbBxScheduleName, objWorkSchedule.GetByType(3), Lang)
        End If
    End Sub

    Private Sub ClearAll()
        RadCmbBxScheduleName.Items.Clear()
        RadCmbBxScheduleName.Text = String.Empty
        Me.RadComboScheduleType.SelectedValue = -1
        RadCmbBxScheduleName.SelectedValue = -1
    End Sub

    Private Sub FillExcelreport()
        Dim rptid As Integer = Request.QueryString("ReportId")
        NDT = New DataTable
        NDT = DT.Clone()
        Select Case rptid
            Case 1 'Schedules List' '1

                If (Lang = CtlCommon.Lang.AR) Then

                    NDT.Columns(14).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)

                            If i = 14 Then
                                If Not dr(i) Is DBNull.Value Then
                                    If dr(i) = True Then
                                        dr(i) = Convert.ToString("نعم")
                                    ElseIf dr(i) = False Then
                                        dr(i) = Convert.ToString("لا")
                                    End If
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    If dr(i) = 1 Then
                                        dr(i) = Convert.ToString("عادي")
                                    ElseIf dr(i) = 2 Then
                                        dr(i) = Convert.ToString("مرن")
                                    ElseIf dr(i) = 3 Then
                                        dr(i) = Convert.ToString("مناوبات")
                                    End If
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "اسم الجدول"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "يوم الاسبوع"
                    NDT.Columns(2).ColumnName = "من وقت 1"
                    NDT.Columns(3).ColumnName = "إلى وقت 1"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "مجموع ساعات الجدول"
                    NDT.Columns(5).ColumnName = "المده 1"
                    NDT.Columns(6).ColumnName = "من وقت 2"
                    NDT.Columns(7).ColumnName = "إلى وقت 2"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "المده 2"
                    NDT.Columns(9).ColumnName = "يوم عطله"
                    NDT.Columns(10).ColumnName = "نوع الجدول"

                Else
                    NDT.Columns(14).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)

                            If i = 14 Then
                                If Not dr(i) Is DBNull.Value Then
                                    If dr(i) = True Then
                                        dr(i) = Convert.ToString("Yes")
                                    ElseIf dr(i) = False Then
                                        dr(i) = Convert.ToString("No")
                                    End If
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    If dr(i) = 1 Then
                                        dr(i) = Convert.ToString("Normal")
                                    ElseIf dr(i) = 2 Then
                                        dr(i) = Convert.ToString("Flexible")
                                    ElseIf dr(i) = 3 Then
                                        dr(i) = Convert.ToString("Advanced")
                                    End If
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Schedule Name"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "Week Day"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "From Time 1"
                    NDT.Columns(3).ColumnName = "To Time 1"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Total Work Hours"
                    NDT.Columns(5).ColumnName = "Duration 1"
                    NDT.Columns(6).ColumnName = "From Time 2"
                    NDT.Columns(7).ColumnName = "To Time 2"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Duration 2"
                    NDT.Columns(9).ColumnName = "OFF Day"
                    NDT.Columns(10).ColumnName = "Schedule Tupe"

                End If
        End Select

    End Sub

    'Private Sub ExportExcel()
    '    Try

    '        Dim Reportpath As String = Server.MapPath("~/Reports/")
    '        Dim myExportOptions As ExportOptions
    '        Dim myDiskFileDestinationOptions As DiskFileDestinationOptions
    '        Dim myExportFile As String
    '        Dim FileName, RptFilename As String


    '        FileName = "SchedulesList" 'Now.Hour & Now.Minute & Now.Second & Now.Millisecond
    '        RptFilename = Reportpath + RPTName
    '        myExportFile = RptFilename & Trim(FileName) + ".xls"
    '        myDiskFileDestinationOptions = New DiskFileDestinationOptions

    '        myDiskFileDestinationOptions.DiskFileName = myExportFile

    '        myExportOptions = cryRpt.ExportOptions

    '        myExportOptions.DestinationOptions = myDiskFileDestinationOptions
    '        myExportOptions.ExportDestinationType = ExportDestinationType.DiskFile
    '        myExportOptions.ExportFormatType = ExportFormatType.Excel

    '        cryRpt.Export()

    '        Response.ClearContent()
    '        Response.ClearHeaders()
    '        Response.BufferOutput = True
    '        Response.AddHeader("Content-Disposition", "inline;filename=" & FileName & ".xls")
    '        Response.ContentType = "application/ms-excel"

    '        Response.WriteFile(myExportFile)
    '        Response.Flush()
    '        Response.Close()
    '        System.IO.File.Delete(myExportFile)
    '    Catch ex As Exception
    '        CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message(), "")
    '    End Try
    'End Sub
#End Region

End Class
