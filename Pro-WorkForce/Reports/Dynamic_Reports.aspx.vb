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
Imports SmartV.UTILITIES.ProjectCommon
Imports TA.Lookup
Imports System.Data.SqlClient

Partial Class Reports_SelfServices_Reports
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
    Private objDynamicReports As DynamicReports

    'Shared viewName As String
    'Shared dtcurrent As DataTable
    Private objDALPrintPage As DALPrintPage
    'Shared dt As DataTable = Nothing
    Shared dtTechConditions As DataTable = New DataTable
    Shared dtNonTechConditions As DataTable = New DataTable

    Private DynamicReports As String = "Dynamic Reports"
    Private CreateDynamicReport As String = "Create Dynamic Report"



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
    Public Property dtCurrentDynamic() As DataTable
        Get
            Return ViewState("dtCurrentDynamic")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrentDynamic") = value
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
    Private Property ReportId() As Integer
        Get
            Return ViewState("ReportId")
        End Get
        Set(ByVal value As Integer)
            ViewState("ReportId") = value
        End Set
    End Property
    Public Property ReportDataSet() As DataSet
        Get
            Return ViewState("DynamicReport")
        End Get
        Set(ByVal value As DataSet)
            ViewState("DynamicReport") = value
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
        dtCurrent = objsysforms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            formID = row("FormID")
        Next
        If Not Page.IsPostBack Then
            FillReports(formID, SessionVariables.LoginUser.GroupId)
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        cryRpt.Close()
        cryRpt.Dispose()

    End Sub

    Protected Sub RadComboExtraReports_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboExtraReports.SelectedIndexChanged
        If (RadComboExtraReports.SelectedValue = DynamicReports) Then
            MultiView1.SetActiveView(vwDynamicReport)
            FillGridView()
        ElseIf (RadComboExtraReports.SelectedValue = CreateDynamicReport) Then
            ' MultiView1.Views.Remove(vwDynamicReport)
            MultiView1.SetActiveView(View1)
            FillReportnames()
        End If
    End Sub
    Protected Sub dgrdVwDynamicRpt_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles dgrdVwDynamicRpt.SelectedIndexChanged
        ReportId = Convert.ToInt32(DirectCast(dgrdVwDynamicRpt.SelectedItems(0), GridDataItem)("ReportId").Text)
        objDynamicReports = New DynamicReports
        objDynamicReports.ReportId = ReportId
        objDynamicReports.GetByPK()
        With objDynamicReports
            txtReportName.Text = .ReportName
            txtReportView.Text = .ViewName
            Dim dt As DataTable = Nothing
            Dim ViewDefinition As String = ""
            dt = .GetViewDefinition
            ViewDefinition = dt.Rows(0)(0)
            Dim cut_at As String = "AS"
            Dim x As Integer = InStr(ViewDefinition, cut_at)
            Dim SQLQuery As String = ViewDefinition.Substring(x + cut_at.Length - 1)

            txtSQLQuery.Text = SQLQuery
        End With
    End Sub

    Protected Sub btnClear_Click(sender As Object, e As System.EventArgs) Handles btnClear.Click
        clearall()
        FillGridView()
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As System.EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1
        Dim SQLStr As String
        Dim dt As DataTable = Nothing
        objDynamicReports = New DynamicReports
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdVwDynamicRpt.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim strCode As String = row("ViewName").Text
                objDynamicReports.ReportId = Convert.ToInt32(row("ReportId").Text)
                objDynamicReports.ViewName = row("ViewName").Text
                errNum = objDynamicReports.Delete()

            End If
        Next
        If errNum = 0 Then
            SQLStr = "DROP VIEW " & objDynamicReports.ViewName
            objDynamicReports.SQLQuery = SQLStr
            dt = objDynamicReports.ExecSQLQuery()
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully"), "success")
            FillGridView()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord", CultureInfo), "error")
        End If

        clearall()
    End Sub


    Protected Sub dgrdVwDynamicRpt_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdVwDynamicRpt.NeedDataSource
        objDynamicReports = New DynamicReports()
        dgrdVwDynamicRpt.DataSource = objDynamicReports.GetAll()
    End Sub

    Protected Sub dgrdVwDynamicRpt_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdVwDynamicRpt.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdVwDynamicRpt.Skin))
    End Function

    Protected Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
        objDynamicReports = New DynamicReports
        Dim SQLStr As String
        Dim errStr As String
        Dim errNo As Integer
        Dim dt As DataTable = Nothing
        With objDynamicReports
            .ReportName = txtReportName.Text
            .ViewName = txtReportView.Text.Trim()
            If ReportId = 0 Then

                errNo = .Add()
                If errNo = 0 Then
                    SQLStr = "CREATE VIEW " & txtReportView.Text.Trim() & " AS " & txtSQLQuery.Text
                    .SQLQuery = SQLStr
                    dt = .ExecSQLQuery()
                    errStr = dt.Rows(0)(0)
                    If errStr = "Success" Then
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully"), "success")
                        clearall()
                        FillGridView()
                    Else
                        CtlCommon.ShowMessage(Me.Page, errStr, "info")
                    End If

                ElseIf errNo = -5 Then
                    CtlCommon.ShowMessage(Me.Page, "Report Name Already Exists", "info")
                End If

            Else
                errNo = .Update()
                If errNo = 0 Then
                    SQLStr = "ALTER VIEW " & txtReportView.Text.Trim() & " AS " & txtSQLQuery.Text
                    .SQLQuery = SQLStr
                    dt = .ExecSQLQuery()
                    errStr = dt.Rows(0)(0)
                    If errStr = "Success" Then
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully"), "success")
                        clearall()
                        FillGridView()
                    Else
                        CtlCommon.ShowMessage(Me.Page, errStr, "info")
                    End If
                End If

            End If

        End With
    End Sub
    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub
#End Region

#Region "Methods"

    'Private Sub ClearAll()

    '    txtReportView.Text = String.Empty
    '    txtSQLQuery.Text = String.Empty
    '    ReportId = 0
    'End Sub

    Private Sub FillGridView()
        Try

            objDynamicReports = New DynamicReports()
            dgrdVwDynamicRpt.DataSource = objDynamicReports.GetAll()
            dgrdVwDynamicRpt.DataBind()
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




        UserName = SessionVariables.LoginUser.UsrID
        ' Version = System.Configuration.ConfigurationManager.AppSettings("Version").ToString()

        If (Lang = CtlCommon.Lang.AR) Then
            RPTName = "Arabic/" + RPTName
        Else
            RPTName = "English/" + RPTName
        End If

    End Sub



    Private Sub FillExcelreport(ByVal reportname As String)

        NDT = New DataTable
        NDT = DT.Clone()
        Select Case reportname
            Case "Daily Report"
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    NDT.Columns(23).DataType = System.Type.GetType("System.String")
                    NDT.Columns(25).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 20 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 25 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 27 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "اسم الجدول"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "التاريخ "
                    NDT.Columns(6).ColumnName = " اول دخول"
                    NDT.Columns(7).ColumnName = " اخر خروج"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "التأخير"
                    NDT.Columns(9).ColumnName = "الخروج المبكر"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "المدة"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)


                Else
                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    NDT.Columns(23).DataType = System.Type.GetType("System.String")
                    NDT.Columns(25).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 20 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 25 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 27 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Schedule Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Date"
                    NDT.Columns(6).ColumnName = "First In"
                    NDT.Columns(7).ColumnName = "Last Out"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Delay"
                    NDT.Columns(9).ColumnName = "Early Out"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Duration"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)

                End If
            Case "Attendance Transactions Report"
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "اسم الموظف باللغة العربية"
                    NDT.Columns(3).ColumnName = "اسم وحدة العمل"
                    NDT.Columns(4).ColumnName = "اسم وحد العمل باللغة العربية"
                    NDT.Columns(5).ColumnName = "اسم الشركة"
                    NDT.Columns(6).ColumnName = "اسم الشركة باللغة العربية"
                    NDT.Columns(7).ColumnName = "اسم السبب"
                    NDT.Columns(8).ColumnName = "تاريخ الحركة"
                    NDT.Columns(9).ColumnName = "وقت الحركة"
                    NDT.Columns(10).ColumnName = "القارئ"
                Else
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns(2).ColumnName = "Employee Arabic Name"
                    NDT.Columns(3).ColumnName = "Entity Name"
                    NDT.Columns(4).ColumnName = "Entity Arabic Name"
                    NDT.Columns(5).ColumnName = "Company Name"
                    NDT.Columns(6).ColumnName = "Company Arabic Name"
                    NDT.Columns(7).ColumnName = "Reason Name"
                    NDT.Columns(8).ColumnName = "Move Date"
                    NDT.Columns(9).ColumnName = "Move Time"
                    NDT.Columns(10).ColumnName = "Reader"
                End If
            Case "Absent Employees Report"
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "اسم الموظف باللغة العربية"
                    NDT.Columns(3).ColumnName = "اسم وحدة العمل"
                    NDT.Columns(4).ColumnName = "اسم وحدة العمل باللغة العربية"
                    NDT.Columns(5).ColumnName = "اسم الشركة"
                    NDT.Columns(6).ColumnName = "اسم الشركة باللغة العربية"
                    NDT.Columns(7).ColumnName = "تاريخ الحركة"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "الحالة"
                    NDT.Columns(9).ColumnName = "الملاحظات"
                    NDT.Columns(10).ColumnName = "السبب"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "مجموع ساعات العمل"
                    NDT.Columns.RemoveAt(12)
                Else
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns(2).ColumnName = "Employee Arabic Name"
                    NDT.Columns(3).ColumnName = "Entity Name"
                    NDT.Columns(4).ColumnName = "Entity Arabic Name"
                    NDT.Columns(5).ColumnName = "Company Name"
                    NDT.Columns(6).ColumnName = "Company Arabic Name"
                    NDT.Columns(7).ColumnName = "Move Date"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Status"
                    NDT.Columns(9).ColumnName = "Remarks"
                    NDT.Columns(10).ColumnName = "Reason"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "Total Work Hours"
                    NDT.Columns.RemoveAt(12)
                End If
            Case 6 'rptEmpExtraHours
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
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(1).ColumnName = "رقم الموظف"
                    NDT.Columns(2).ColumnName = "اسم الموظف"
                    NDT.Columns(3).ColumnName = "اسم الموظف باللغة العربية"
                    NDT.Columns(4).ColumnName = "اسم وحدة العمل"
                    NDT.Columns(5).ColumnName = "اسم وحدة العمل باللغة العربية"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "اسم الشركة"
                    NDT.Columns(7).ColumnName = "اسم الشركة باللغة العربية"
                    NDT.Columns(8).ColumnName = "تاريخ الحركة"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "الحالة"
                    NDT.Columns(10).ColumnName = "الملاحظات"
                    NDT.Columns(11).ColumnName = "السبب"
                    NDT.Columns(12).ColumnName = "الوقت الاضافي"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "مجموع ساعات العمل"
                    NDT.Columns.RemoveAt(14)
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
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(1).ColumnName = "Emp No."
                    NDT.Columns(2).ColumnName = "Employee Name"
                    NDT.Columns(3).ColumnName = "Employee Arabic Name"
                    NDT.Columns(4).ColumnName = "Entity Name"
                    NDT.Columns(5).ColumnName = "Entity Arabic Name"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Company Name"
                    NDT.Columns(7).ColumnName = "Company Arabic Name"
                    NDT.Columns(8).ColumnName = "Move Date"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "Status"
                    NDT.Columns(10).ColumnName = "Remarks"
                    NDT.Columns(11).ColumnName = "Reason"
                    NDT.Columns(12).ColumnName = "OverTime"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "Total Work Hours"
                    NDT.Columns.RemoveAt(14)
                End If
            Case 7 'rptViolations
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "اسم الموظف باللغة العربية"
                    NDT.Columns(3).ColumnName = "اسم وحدة العمل"
                    NDT.Columns(4).ColumnName = "اسم وحدة العمل باللغة العربية"
                    NDT.Columns(5).ColumnName = "اسم الشركة"
                    NDT.Columns(6).ColumnName = "اسم الشركة باللغة العربية"
                    NDT.Columns(7).ColumnName = "تاريخ الحركة"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "الحالة"
                    NDT.Columns(9).ColumnName = "الملاحظات"
                    NDT.Columns(10).ColumnName = "السبب"
                    NDT.Columns(11).ColumnName = "الوقت الاضافي"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "مجموع ساعات العمل"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "التأخير"
                    NDT.Columns(14).ColumnName = "الخروج المبكر"
                Else
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns(2).ColumnName = "Employee Arabic Name"
                    NDT.Columns(3).ColumnName = "Entity Name"
                    NDT.Columns(4).ColumnName = "Entity Arabic Name"
                    NDT.Columns(5).ColumnName = "Company Name"
                    NDT.Columns(6).ColumnName = "Company Arabic Name"
                    NDT.Columns(7).ColumnName = "Move Date"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Status"
                    NDT.Columns(9).ColumnName = "Remarks"
                    NDT.Columns(10).ColumnName = "Reason"
                    NDT.Columns(11).ColumnName = "OverTime"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "Total Work Hours"
                    NDT.Columns.RemoveAt(13)
                    NDT.Columns(13).ColumnName = "Delay"
                    NDT.Columns(14).ColumnName = "Early Out"
                End If
            Case "In Out Transactions Report"
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")
                    NDT.Columns(14).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 14 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "اسم الموظف باللغة العربية"
                    NDT.Columns(3).ColumnName = "اسم وحدة العمل"
                    NDT.Columns(4).ColumnName = "اسم وحدة العمل باللغة العربية"
                    NDT.Columns(5).ColumnName = "اسم الشركة"
                    NDT.Columns(6).ColumnName = "اسم الشركة باللغة العربية"
                    NDT.Columns(7).ColumnName = "المدة"
                    NDT.Columns(8).ColumnName = "تاريخ الحركة"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "وقت الدخول"
                    NDT.Columns(10).ColumnName = "وقت الخروج"
                    NDT.Columns.RemoveAt(11)
                Else
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")
                    NDT.Columns(14).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 14 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:MM")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns(2).ColumnName = "Employee Arabic Name"
                    NDT.Columns(3).ColumnName = "Entity Name"
                    NDT.Columns(4).ColumnName = "Entity Arabic Name"
                    NDT.Columns(5).ColumnName = "Company Name"
                    NDT.Columns(6).ColumnName = "Company Arabic Name"
                    NDT.Columns(7).ColumnName = "Duration"
                    NDT.Columns(8).ColumnName = "Move Date"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "In Time"
                    NDT.Columns(10).ColumnName = "Out Time"
                    NDT.Columns.RemoveAt(11)
                End If
            Case "ManualEntryRepor"

                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(3).DataType = System.Type.GetType("System.String")
                    NDT.Columns(4).DataType = System.Type.GetType("System.String")
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 3 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 4 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("dd/MM/yyyy HH:MM")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next

                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "تاريخ الحركة"
                    NDT.Columns(3).ColumnName = "وقت الحركة"
                    NDT.Columns(4).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "السبب"
                    NDT.Columns(6).ColumnName = "اسم مستخدم الادخال"
                    NDT.Columns(7).ColumnName = "وقت و تاريخ الادخال"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)

                Else
                    NDT.Columns(3).DataType = System.Type.GetType("System.String")
                    NDT.Columns(4).DataType = System.Type.GetType("System.String")
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 3 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 4 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("dd/MM/yyyy HH:MM")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next

                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Transaction Date"
                    NDT.Columns(3).ColumnName = "Transaction Time"
                    NDT.Columns(4).ColumnName = "Remarks"
                    NDT.Columns(5).ColumnName = "Reason"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Entry User Name"
                    NDT.Columns(7).ColumnName = "Entry Date Time"
                    NDT.Columns(8).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns.RemoveAt(10)

                End If
            Case "Incomplete Transactions"
                NDT.Columns(8).DataType = System.Type.GetType("System.String")
                NDT.Columns(9).DataType = System.Type.GetType("System.String")
                NDT.Columns(10).DataType = System.Type.GetType("System.String")
                For Each row As DataRow In DT.Rows
                    Dim dr As DataRow = NDT.NewRow
                    For i As Integer = 0 To NDT.Columns.Count - 1
                        dr(i) = row(i)
                        If i = 8 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("dd/MM/yyyy")
                            End If
                        ElseIf i = 9 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 10 Then
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
                End If
            Case "Early Out Report"
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 19 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 20 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "اسم الموظف باللغة العربية"
                    NDT.Columns(3).ColumnName = "اسم وحدة العمل"
                    NDT.Columns(4).ColumnName = "اسم وحدة العمل باللغة العربية"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "التاريخ"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "الخروج المبكر"
                    NDT.Columns.RemoveAt(8)

                Else
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 19 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 20 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns(2).ColumnName = "Employee Arabic Name"
                    NDT.Columns(3).ColumnName = "Entity Name"
                    NDT.Columns(4).ColumnName = "Entity Arabic Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Date"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "Early Out"
                    NDT.Columns.RemoveAt(8)
                End If
            Case "Delay Report"
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(19).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
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
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "اسم الموظف باللغة العربية"
                    NDT.Columns(3).ColumnName = "اسم وحدة العمل"
                    NDT.Columns(4).ColumnName = "اسم وحدة العمل باللغة العربية"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "التاريخ"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "التأخير"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)


                Else
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(15).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(19).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 15 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
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
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns(2).ColumnName = "Employee Arabic Name"
                    NDT.Columns(3).ColumnName = "Entity Name"
                    NDT.Columns(4).ColumnName = "Entity Arabic Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Date"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "Delay"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                End If
            Case "Detailed Transactions"

                NDT.Columns(3).DataType = System.Type.GetType("System.String")
                NDT.Columns(4).DataType = System.Type.GetType("System.String")
                NDT.Columns(5).DataType = System.Type.GetType("System.String")
                NDT.Columns(6).DataType = System.Type.GetType("System.String")
                NDT.Columns(9).DataType = System.Type.GetType("System.String")
                NDT.Columns(20).DataType = System.Type.GetType("System.String")
                NDT.Columns(21).DataType = System.Type.GetType("System.String")
                NDT.Columns(22).DataType = System.Type.GetType("System.String")
                NDT.Columns(23).DataType = System.Type.GetType("System.String")
                NDT.Columns(24).DataType = System.Type.GetType("System.String")
                For Each row As DataRow In DT.Rows
                    Dim dr As DataRow = NDT.NewRow
                    For i As Integer = 0 To NDT.Columns.Count - 1
                        dr(i) = row(i)
                        If i = 3 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("dd/MM/yyyy")
                            End If
                        ElseIf i = 4 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 5 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 6 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 9 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 20 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 21 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 22 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 23 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 24 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        End If
                    Next
                    NDT.Rows.Add(dr)
                Next
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "التاريخ"
                    NDT.Columns(3).ColumnName = "وقت الدخول"
                    NDT.Columns(4).ColumnName = "وقت الخروج"
                    NDT.Columns(5).ColumnName = "المدة"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "اسم السبب"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "اسم الحالة"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "نوع الاجازة"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "نوع المغادرة"
                    NDT.Columns(12).ColumnName = "من وقت"
                    NDT.Columns(13).ColumnName = "الى وقت"
                    NDT.Columns(14).ColumnName = "التأخير"
                    NDT.Columns(15).ColumnName = "الخروج المبكر"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns(16).ColumnName = "اسم العطلة"
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)

                Else
                    NDT.Columns(0).ColumnName = "Employee No"
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Date"
                    NDT.Columns(3).ColumnName = "In Time"
                    NDT.Columns(4).ColumnName = "Out Time"
                    NDT.Columns(5).ColumnName = "Duration"
                    NDT.Columns(6).ColumnName = "Reason Name"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "Status Name"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Leave Type"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "Permission Type"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "From Time"
                    NDT.Columns(13).ColumnName = "To Time"
                    NDT.Columns(14).ColumnName = "Delay"
                    NDT.Columns(15).ColumnName = "Early Out"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns(16).ColumnName = "Holiday Name"
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                End If

            Case "Invalid Attempts"
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(14).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 14 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Employee No"
                    NDT.Columns(1).ColumnName = "Card No"
                    NDT.Columns(2).ColumnName = "Employee Name"
                    NDT.Columns(3).ColumnName = "Employee Arabic Name"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Entity Name"
                    NDT.Columns(5).ColumnName = "Entity Arabic Name"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Company Name"
                    NDT.Columns(7).ColumnName = "Company Arabic Name"
                    NDT.Columns(8).ColumnName = "Reason Name"
                    NDT.Columns(9).ColumnName = "Reason Arabic Name"
                    NDT.Columns(10).ColumnName = "Move Date"
                    NDT.Columns(11).ColumnName = "Move Time"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "Reader"
                    NDT.Columns(13).ColumnName = "Employee Image"
                Else
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(14).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 14 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Employee No"
                    NDT.Columns(1).ColumnName = "Card No"
                    NDT.Columns(2).ColumnName = "Employee Name"
                    NDT.Columns(3).ColumnName = "Employee Arabic Name"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Entity Name"
                    NDT.Columns(5).ColumnName = "Entity Arabic Name"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "Company Name"
                    NDT.Columns(7).ColumnName = "Company Arabic Name"
                    NDT.Columns(8).ColumnName = "Reason Name"
                    NDT.Columns(9).ColumnName = "Reason Arabic Name"
                    NDT.Columns(10).ColumnName = "Move Date"
                    NDT.Columns(11).ColumnName = "Move Time"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "Reader"
                    NDT.Columns(13).ColumnName = "Employee Image"
                End If
            Case "Employee Time & Attendance 2"
                If (Lang = CtlCommon.Lang.AR) Then
                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    NDT.Columns(23).DataType = System.Type.GetType("System.String")
                    NDT.Columns(25).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 20 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 25 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            ElseIf i = 27 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortTimeString()
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "اسم الجدول"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "التاريخ "
                    NDT.Columns(6).ColumnName = " اول دخول"
                    NDT.Columns(7).ColumnName = " اخر خروج"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "التأخير"
                    NDT.Columns(9).ColumnName = "الخروج المبكر"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "المدة"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "الملاحظات"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)


                Else
                    NDT.Columns(11).DataType = System.Type.GetType("System.String")
                    NDT.Columns(12).DataType = System.Type.GetType("System.String")
                    NDT.Columns(13).DataType = System.Type.GetType("System.String")
                    NDT.Columns(17).DataType = System.Type.GetType("System.String")
                    NDT.Columns(18).DataType = System.Type.GetType("System.String")
                    NDT.Columns(20).DataType = System.Type.GetType("System.String")
                    NDT.Columns(23).DataType = System.Type.GetType("System.String")
                    NDT.Columns(25).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 11 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("dd/MM/yyyy")
                                End If
                            ElseIf i = 12 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 13 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 17 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 18 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 20 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 25 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            ElseIf i = 27 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns.RemoveAt(0)
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Schedule Name"
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns.RemoveAt(5)
                    NDT.Columns(5).ColumnName = "Date"
                    NDT.Columns(6).ColumnName = "First In"
                    NDT.Columns(7).ColumnName = "Last Out"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Delay"
                    NDT.Columns(9).ColumnName = "Early Out"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Duration"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "Remarks"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns.RemoveAt(12)

                End If
            Case "Delay and EarlyOut"
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
                                dr(i) = Convert.ToDateTime(row(i)).ToString("dd/MM/yyyy")
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


    Private Sub FillReports(ByVal formID As Integer, ByVal groupID As Integer)

        CtlCommon.FillTelerikDropDownList(RadComboExtraReports, objsysforms.GetFormsByParentID(formID, groupID), Lang)

    End Sub

#End Region
#Region "RDLC Report"

#Region "Display Section"
    Private Sub FillReportnames()
        clearSelectCmbReportsddl()
        Dim objReport As New TA.Reports.RDLCReport
        dtCurrentDynamic = objReport.GetDynamicReports
        CtlCommon.FillTelerikDropDownList(CmbReports, dtCurrentDynamic, Lang)
    End Sub

    Private Sub FillList(ByVal SourceTable As DataTable)
        Dim DestTable As DataTable = New DataTable()
        DestTable.Columns.Add("ColumnName")
        DestTable.Columns.Add("Columnid")
        Dim count As Integer = 0
        For Each sourcerow As DataRow In SourceTable.Rows
            Dim destRow As DataRow = DestTable.NewRow()
            destRow("ColumnName") = sourcerow("ColumnName")
            count = count + 1
            destRow("Columnid") = count
            DestTable.Rows.Add(destRow)
        Next
        CtlCommon.FillListBox(lbxItemTypes, DestTable)
        lbxItemTypes2.Items.Clear()
    End Sub

    Protected Sub ibtnItemAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnItemAdd.Click
        If (lbxItemTypes.SelectedIndex <> -1) Then
            Dim Selected_Items As New List(Of ListItem)
            For Each indx As Integer In lbxItemTypes.GetSelectedIndices
                Selected_Items.Add(lbxItemTypes.Items(indx))
            Next
            For Each Selected_Item As ListItem In Selected_Items
                lbxItemTypes2.Items.Add(Selected_Item)
                lbxItemTypes.Items.Remove(Selected_Item)
            Next
            lbxItemTypes.SelectedIndex = -1
            lbxItemTypes2.SelectedIndex = -1
        End If
    End Sub

    Protected Sub ibtnItemRemove_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnItemRemove.Click
        If (lbxItemTypes2.SelectedIndex <> -1) Then
            Dim Selected_Items As New List(Of ListItem)
            For Each indx As Integer In lbxItemTypes2.GetSelectedIndices
                Selected_Items.Add(lbxItemTypes2.Items(indx))
            Next
            For Each Selected_Item As ListItem In Selected_Items
                lbxItemTypes.Items.Add(Selected_Item)
                lbxItemTypes2.Items.Remove(Selected_Item)
            Next

            lbxItemTypes.SelectedIndex = -1
            lbxItemTypes2.SelectedIndex = -1
        End If
    End Sub

    Sub movedown()
        Dim startindex As Integer = lbxItemTypes2.Items.Count - 1
        For i As Integer = startindex To -1 + 1 Step -1
            If lbxItemTypes2.Items(i).Selected Then
                If i < startindex AndAlso Not lbxItemTypes2.Items(i + 1).Selected Then
                    Dim bottom As ListItem = lbxItemTypes2.Items(i)
                    lbxItemTypes2.Items.Remove(bottom)
                    lbxItemTypes2.Items.Insert(i + 1, bottom)
                    lbxItemTypes2.Items(i + 1).Selected = True
                End If
            End If
        Next
    End Sub

    Sub moveup()
        For i As Integer = 0 To lbxItemTypes2.Items.Count - 1
            If lbxItemTypes2.Items(i).Selected Then
                If i > 0 AndAlso Not lbxItemTypes2.Items(i - 1).Selected Then
                    Dim bottom As ListItem = lbxItemTypes2.Items(i)
                    lbxItemTypes2.Items.Remove(bottom)
                    lbxItemTypes2.Items.Insert(i - 1, bottom)
                    lbxItemTypes2.Items(i - 1).Selected = True
                End If
            End If
        Next
    End Sub

    Protected Sub ibtnItemDown_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnItemDown.Click
        movedown()
    End Sub

    Protected Sub ibtnItemUp_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnItemUp.Click
        'MoveItem(-1)
        moveup()
    End Sub

    Public Sub MoveItem(ByVal direction As Integer)
        If lbxItemTypes2.SelectedItem Is Nothing OrElse lbxItemTypes2.SelectedIndex < 0 Then
            Return
        End If
        Dim newIndex As Integer = lbxItemTypes2.SelectedIndex + direction
        If newIndex < 0 OrElse newIndex >= lbxItemTypes2.Items.Count Then
            Return
        End If
        Dim selected As Object = lbxItemTypes2.SelectedItem
        lbxItemTypes2.Items.Remove(selected)
        lbxItemTypes2.Items.Insert(newIndex, selected)
        lbxItemTypes2.SelectedIndex = newIndex
    End Sub

    Protected Sub CmbReports_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles CmbReports.SelectedIndexChanged
        clearall()
        Dim cs As String = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Using con As New SqlConnection(cs)
            con.Open()
            Dim cmd As SqlCommand = con.CreateCommand()
            ViewState("viewName") = dtCurrentDynamic.Select("ReportId=" & CmbReports.SelectedValue)(0)(2).ToString
            cmd.CommandText = "SELECT   top 1 *   FROM  " + ViewState("viewName")
            Dim rdr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.SchemaOnly)
            Dim dt As DataTable = rdr.GetSchemaTable()
            rdr.Close()
            con.Close()
            FillList(dt)
            fillColumnsddl(dt)
            MultiView1.SetActiveView(View1)
        End Using
    End Sub

    Protected Sub ibtnGenerateReport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnGenerateReport.Click
        Dim strFilterCond As String = String.Empty
        If dtTechConditions IsNot Nothing AndAlso dtTechConditions.Rows.Count > 0 Then
            For Each dr As DataRow In dtTechConditions.Rows
                strFilterCond = strFilterCond + dr("CondValue").ToString
            Next
        End If


        Dim str As String = ""
        For Each Item In Me.lbxItemTypes2.Items
            str &= Item.ToString & ","
        Next
        If str <> "" Then
            str = str.Substring(0, str.Length - 1)
            If strFilterCond <> "" Then
                str = "select " + str + " from " + ViewState("viewName") + " where " + strFilterCond
            ElseIf strFilterCond = "" Then
                str = "select " + str + " from " + ViewState("viewName")
            End If


            Dim ds As DataSet = GetDataSet(str)
            'Dim obj As New DATAC_Reporting.RdlcEngine
            ReportDataSet = ds

            'DATAC_Reporting.RdlcEngine.BindControl(ReportViewer1, ds, txtReportName.Text)
            grdDynamicReport.DataSource = ds
            grdDynamicReport.DataBind()

            'For Each column As GridColumn In grdDynamicReport.MasterTableView.AutoGeneratedColumns

            'Next
            MultiView1.SetActiveView(Report)

        Else
            CtlCommon.ShowMessage(Page, "No Columns To display")
        End If

    End Sub

    Private Function GetDataSet(ByVal sql As String) As DataSet
        Dim conString = ConfigurationManager.ConnectionStrings("ConnStr")
        Dim strConnString As String = conString.ConnectionString

        Dim conn As New SqlConnection(strConnString)
        conn.Open()
        Dim ad As New SqlDataAdapter(sql, conn)
        Dim ds As New DataSet()
        ad.Fill(ds)
        Return ds
    End Function
#End Region
#Region "Filter Section"

    Private Sub fillOprtrddl()
        Dim MyDT As New DataTable
        Dim MyRow As DataRow
        Dim intCount As Integer = 0
        MyDT.Columns.Add(New DataColumn("DataType", GetType(String)))
        MyDT.Columns.Add(New DataColumn("ColumnName", GetType(String)))
        If dtNonTechConditions IsNot Nothing AndAlso dtNonTechConditions.Rows.Count > 0 Then
            MyRow = MyDT.NewRow()
            MyRow(0) = "OR"
            MyRow(1) = "OR"
            MyDT.Rows.Add(MyRow)
            MyRow = MyDT.NewRow()
            MyRow(0) = "And"
            MyRow(1) = "And"
            MyDT.Rows.Add(MyRow)
            CtlCommon.FillTelerikDropDownList(radSelectOperator, MyDT)
        Else
            clearSelectOperatorddl()
        End If

    End Sub

    Private Sub fillColumnsddl(ByVal dt As DataTable)
        clearconditionddl()
        Dim MyDT As New DataTable
        MyDT.Columns.Add(New DataColumn("DataType", GetType(String)))
        MyDT.Columns.Add(New DataColumn("ColumnName", GetType(String)))
        Dim i As Integer = 0
        For Each r As DataRow In dt.Rows
            Dim destRow As DataRow = MyDT.NewRow()
            i = i + 1
            destRow("DataType") = i.ToString + r("DataType").ToString
            destRow("ColumnName") = r("ColumnName")
            MyDT.Rows.Add(destRow)
        Next
        CtlCommon.FillTelerikDropDownList(radSelectColumn, MyDT)
    End Sub

    Sub clearconditionddl()
        radSelectCondition.Items.Clear()
        radSelectCondition.DataSource = Nothing
        radSelectCondition.DataBind()
    End Sub

    Sub clearSelectOperatorddl()
        radSelectOperator.Items.Clear()
        radSelectOperator.DataSource = Nothing
        radSelectOperator.DataBind()
    End Sub
    'radSelectColumn
    Sub clearSelectColumnddl()
        radSelectColumn.Items.Clear()
        radSelectColumn.DataSource = Nothing
        radSelectColumn.DataBind()
    End Sub
    'CmbReports
    Sub clearSelectCmbReportsddl()
        CmbReports.Items.Clear()
        CmbReports.DataSource = Nothing
        CmbReports.DataBind()
    End Sub
    '
    Protected Sub ibtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnSave.Click
        fillConditionsGrid()
        If radSelectOperator.Items.Count = 0 Then
            fillOprtrddl()
        End If
    End Sub

    Private Sub fillConditionsddl(ByVal strDataType As String)
        Dim MyDT As New DataTable
        Dim MyRow As DataRow
        MyDT.Columns.Add(New DataColumn("CondValue", GetType(String)))
        MyDT.Columns.Add(New DataColumn("Condition", GetType(String)))
        Select Case strDataType
            Case "System.String"
                MyRow = MyDT.NewRow()
                MyRow(0) = "Starts with"
                MyRow(1) = "Starts with"
                MyDT.Rows.Add(MyRow)
                MyRow = MyDT.NewRow()
                MyRow(0) = "Contains"
                MyRow(1) = "Contains"
                MyDT.Rows.Add(MyRow)
                MyRow = MyDT.NewRow()
                MyRow(0) = "End with"
                MyRow(1) = "End with"
                MyDT.Rows.Add(MyRow)

            Case Else
                MyRow = MyDT.NewRow()
                MyRow(0) = "="
                MyRow(1) = "Equal"
                MyDT.Rows.Add(MyRow)

                MyRow = MyDT.NewRow()
                MyRow(0) = "<"
                MyRow(1) = "Less"
                MyDT.Rows.Add(MyRow)

                MyRow = MyDT.NewRow()
                MyRow(0) = ">"
                MyRow(1) = "Greater"
                MyDT.Rows.Add(MyRow)

                MyRow = MyDT.NewRow()
                MyRow(0) = "Between"
                MyRow(1) = "Between"
                MyDT.Rows.Add(MyRow)

        End Select
        CtlCommon.FillTelerikDropDownList(radSelectCondition, MyDT)
    End Sub

    Protected Sub radSelectColumn_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radSelectColumn.SelectedIndexChanged
        Dim strColumnType As String = radSelectColumn.SelectedValue.ToString.Substring(1, Len(radSelectColumn.SelectedValue.ToString) - 1)
        ShowLables(False)
        ShowControls(False)
        ShowPanels(False)
        If radSelectColumn.SelectedIndex > 0 Then
            fillConditionsddl(strColumnType)
        Else
            clearconditionddl()
        End If
    End Sub

    Sub ShowControls(ByVal Status As Boolean)
        txtSearchKey.Visible = Status
        txtFrom.Visible = Status
        txtTo.Visible = Status
        dteFromDate.Visible = Status
        dteToDate.Visible = Status
    End Sub

    Sub ShowPanels(ByVal Status As Boolean)
        tblSearchDate.Visible = Status
        tblSearchNumber.Visible = Status
        tblSearchString.Visible = Status
    End Sub

    Sub showButtons(ByVal Status As Boolean)
        ibtnSave.Visible = Status
    End Sub

    Sub ShowLables(ByVal Status As Boolean)
        lblSearchKey.Visible = Status
        lblFrom.Visible = Status
        lblFromDate.Visible = Status
        lblTo.Visible = Status
        lblToDate.Visible = Status
    End Sub

    Protected Sub radSelectCondition_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radSelectCondition.SelectedIndexChanged

        ShowLables(False)
        ShowControls(False)
        ShowPanels(False)
        showButtons(True)
        Dim strSelectedColumnType As String = radSelectColumn.SelectedValue.ToString.Substring(1, Len(radSelectColumn.SelectedValue.ToString) - 1)

        If radSelectCondition.SelectedIndex > 0 Then
            Select Case strSelectedColumnType
                Case "System.String"
                    tblSearchString.Visible = True
                    lblSearchKey.Visible = True
                    txtSearchKey.Visible = True
                Case "System.DateTime"
                    tblSearchDate.Visible = True
                    If radSelectCondition.SelectedValue = "Between" Then
                        ReqFromDate.Enabled = True
                        ReqToDate.Enabled = True
                        ReqFromDate.ErrorMessage = "Please pick from date"
                        ReqToDate.ErrorMessage = "Please pick to date"
                        lblFromDate.Visible = True
                        lblToDate.Visible = True
                        lblFromDate.Text = "From Date"
                        lblToDate.Text = "To Date"
                        dteFromDate.Visible = True
                        dteToDate.Visible = True
                    Else
                        ReqFromDate.Enabled = True
                        ReqFromDate.ErrorMessage = "Please pick a date"
                        lblFromDate.Visible = True
                        lblFromDate.Text = "Date"
                        dteFromDate.Visible = True
                    End If
                Case Else
                    tblSearchNumber.Visible = True
                    If radSelectCondition.SelectedValue = "Between" Then
                        ReqFrom.Enabled = True
                        ReqTo.Enabled = True
                        ReqFrom.ErrorMessage = "Please enter Search key from "
                        ReqTo.ErrorMessage = "Please enter search key to"
                        lblFrom.Visible = True
                        lblTo.Visible = True
                        lblFrom.Text = "From"
                        lblTo.Text = "To"
                        txtFrom.Visible = True
                        txtTo.Visible = True
                    Else
                        ReqFrom.Enabled = True
                        ReqFrom.ErrorMessage = "Please enter search key"
                        lblFrom.Visible = True
                        lblFrom.Text = "Search Value"
                        txtFrom.Visible = True
                    End If
            End Select

        End If

    End Sub

    Sub fillConditionsGrid()
        objDALPrintPage = New DALPrintPage
        If Not dtTechConditions.Columns.Contains("CondValue") Then
            dtTechConditions.Columns.Add(New DataColumn("CondValue", GetType(String)))
            dtNonTechConditions.Columns.Add(New DataColumn("CondValue", GetType(String)))
        End If
        If Not dtTechConditions.Columns.Contains("Condition") Then
            dtTechConditions.Columns.Add(New DataColumn("Condition", GetType(String)))
            dtNonTechConditions.Columns.Add(New DataColumn("Condition", GetType(String)))
        End If


        Dim MyRow As DataRow
        Dim MyRowNonTech As DataRow
        MyRow = dtTechConditions.NewRow()
        MyRowNonTech = dtNonTechConditions.NewRow()

        Dim strOpreator As String = String.Empty
        If radSelectOperator.SelectedIndex > 0 Then
            strOpreator = radSelectOperator.SelectedItem.Text
        Else
            strOpreator = String.Empty
        End If

        Dim strColumnName As String = radSelectColumn.SelectedItem.Text
        Dim strTemp As String = String.Empty
        Dim strSelectedColumnType As String = radSelectColumn.SelectedValue.ToString.Substring(1, Len(radSelectColumn.SelectedValue.ToString) - 1)
        Select Case strSelectedColumnType
            Case "System.String"
                Select Case radSelectCondition.SelectedValue
                    Case "Starts with"
                        MyRow(0) = strOpreator + " " + strColumnName + " Like " + "'" + txtSearchKey.Text.Trim + "%' "
                        MyRow(1) = strOpreator + " " + strColumnName + " Like " + "'" + txtSearchKey.Text.Trim + "%' "

                        MyRowNonTech(0) = strOpreator + " " + radSelectColumn.SelectedItem.Text + " " + radSelectCondition.SelectedValue + " " + txtSearchKey.Text.Trim
                        MyRowNonTech(1) = strOpreator + " " + radSelectColumn.SelectedItem.Text + " " + radSelectCondition.SelectedValue + " " + txtSearchKey.Text.Trim
                    Case "Contains"
                        MyRow(0) = strOpreator + " " + strColumnName + " Like '%" + txtSearchKey.Text.Trim + "%' "
                        MyRow(1) = strOpreator + " " + strColumnName + " Like '%" + txtSearchKey.Text.Trim + "%' "

                        MyRowNonTech(0) = strOpreator + " " + radSelectColumn.SelectedItem.Text + " " + radSelectCondition.SelectedValue + " " + txtSearchKey.Text.Trim
                        MyRowNonTech(1) = strOpreator + " " + radSelectColumn.SelectedItem.Text + " " + radSelectCondition.SelectedValue + " " + txtSearchKey.Text.Trim
                    Case Else
                        MyRow(0) = strOpreator + " " + strColumnName + " Like '%" + txtSearchKey.Text.Trim + "'"
                        MyRow(1) = strOpreator + " " + strColumnName + " Like '%" + txtSearchKey.Text.Trim + "'"

                        MyRowNonTech(0) = strOpreator + " " + radSelectColumn.SelectedItem.Text + " " + radSelectCondition.SelectedValue + " " + txtSearchKey.Text.Trim
                        MyRowNonTech(1) = strOpreator + " " + radSelectColumn.SelectedItem.Text + " " + radSelectCondition.SelectedValue + " " + txtSearchKey.Text.Trim
                End Select

            Case "System.DateTime"

                MyRow = dtTechConditions.NewRow()
                MyRowNonTech = dtNonTechConditions.NewRow()

                If radSelectCondition.SelectedValue = "Between" Then
                    strTemp = strOpreator + " " + strColumnName + " >= '" + Format(dteFromDate.SelectedDate.Value, "MM/dd/yyyy") + "' and " + strColumnName + " <= '" + Format(dteToDate.SelectedDate.Value, "MM/dd/yyyy") + "'"
                    MyRow(0) = strTemp
                    MyRow(1) = strTemp

                    strTemp = strOpreator + " " + radSelectColumn.SelectedItem.Text + " " + radSelectCondition.SelectedValue + " " + dteFromDate.SelectedDate.ToString + " and " + dteToDate.SelectedDate.ToString
                    MyRowNonTech(0) = strTemp
                    MyRowNonTech(1) = strTemp
                Else
                    strTemp = strOpreator + " " + strColumnName + " " + radSelectCondition.SelectedValue + " " + " '" + Format(dteFromDate.SelectedDate.Value, "MM/dd/yyyy") + "'"
                    MyRow(0) = strTemp
                    MyRow(1) = strTemp

                    strTemp = strOpreator + " " + radSelectColumn.SelectedItem.Text + " " + radSelectCondition.SelectedValue + " " + dteFromDate.SelectedDate.ToString
                    MyRowNonTech(0) = strTemp
                    MyRowNonTech(1) = strTemp
                End If
            Case Else

                If radSelectCondition.SelectedValue = "Between" Then
                    strTemp = strOpreator + " " + strColumnName + " >= " + txtFrom.Text.Trim + " and " + strColumnName + " <=" + txtTo.Text.Trim
                    MyRow(0) = strTemp
                    MyRow(1) = strTemp

                    strTemp = strOpreator + " " + radSelectColumn.SelectedItem.Text + " " + radSelectCondition.SelectedValue + " " + txtFrom.Text.Trim + " and " + txtTo.Text.Trim
                    MyRowNonTech(0) = strTemp
                    MyRowNonTech(1) = strTemp
                Else
                    strTemp = strOpreator + " " + strColumnName + " " + radSelectCondition.SelectedValue + " " + txtFrom.Text.Trim
                    MyRow(0) = strTemp
                    MyRow(1) = strTemp

                    strTemp = strOpreator + " " + radSelectColumn.SelectedItem.Text + " " + radSelectCondition.SelectedItem.Text + " " + txtFrom.Text.Trim
                    MyRowNonTech(0) = strTemp
                    MyRowNonTech(1) = strTemp
                End If
        End Select
        For Each dr As DataRow In dtTechConditions.Rows
            If MyRow(1).ToString.Contains(dr("CondValue").ToString) Then
                CtlCommon.ShowMessage(Me.Page, "Condition already exists")
                Exit Sub
            End If
        Next

        For Each drNonTech As DataRow In dtNonTechConditions.Rows
            If MyRowNonTech(1).ToString.Contains(drNonTech("CondValue").ToString) Then
                CtlCommon.ShowMessage(Me.Page, "Condition already exists")
                Exit Sub
            End If
        Next

        dtTechConditions.Rows.Add(MyRow)
        dtNonTechConditions.Rows.Add(MyRowNonTech)
        dgrdConditions.DataSource = dtNonTechConditions
        dgrdConditions.DataBind()
    End Sub

    Public Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sttemp As String = ""
        Dim intIndex As Integer = -1
        Dim intTechRowIndex As Integer = 0
        Dim MyRow As DataRow
        Dim MyRowNonTech As DataRow

        Dim gvr As GridViewRow = CType(sender, LinkButton).Parent.Parent
        For Each dr As DataRow In dtNonTechConditions.Rows
            If dr("Condition") = CType(dgrdConditions.Rows(gvr.RowIndex).FindControl("lblCondition"), Label).Text Then
                If gvr.RowIndex < dgrdConditions.Rows.Count - 1 Then
                    sttemp = CType(dgrdConditions.Rows(gvr.RowIndex + 1).FindControl("lblCondition"), Label).Text
                End If
                dr.Delete()
                dtTechConditions.Rows.RemoveAt(intTechRowIndex)
                Exit For
            End If
            intTechRowIndex = intTechRowIndex + 1
        Next

        dtTechConditions.AcceptChanges()
        dtNonTechConditions.AcceptChanges()

        If sttemp <> String.Empty Then
            For Each dr As DataRow In dtNonTechConditions.Rows
                If dr("Condition") = sttemp Then
                    intIndex = dtNonTechConditions.Rows.IndexOf(dr)
                    Exit For
                End If
            Next
        End If


        If intIndex = 0 Then
            MyRow = dtNonTechConditions.Rows(intIndex)
            MyRowNonTech = dtTechConditions.Rows(intIndex)

            If MyRowNonTech(0).ToString.StartsWith("OR") Then
                MyRowNonTech(0) = MyRowNonTech(0).ToString.Remove(0, 2)
                MyRowNonTech(1) = MyRowNonTech(0).ToString.Remove(0, 2)
            End If

            If MyRowNonTech(0).ToString.StartsWith("And") Then
                MyRowNonTech(0) = MyRowNonTech(0).ToString.Remove(0, 3)
                MyRowNonTech(1) = MyRowNonTech(0).ToString.Remove(0, 3)
            End If

            If (sttemp.StartsWith("OR")) Then
                MyRow(0) = sttemp.Remove(0, 2)
                MyRow(1) = sttemp.Remove(0, 2)

            ElseIf (sttemp.StartsWith("And")) Then
                MyRow(0) = sttemp.Remove(0, 3)
                MyRow(1) = sttemp.Remove(0, 3)
            End If
        End If

        dgrdConditions.DataSource = dtNonTechConditions
        dgrdConditions.DataBind()
        If Not dtNonTechConditions.Rows.Count > 0 Then
            clearSelectOperatorddl()
        End If
    End Sub

#End Region

#Region "Events"
    Protected Sub Ibtnclr_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Ibtnclr.Click
        MultiView1.SetActiveView(View1)
        clearSelectCmbReportsddl()
        FillReportnames()
        clearall()


    End Sub

    Sub clearall()
        dtNonTechConditions = New DataTable
        dtTechConditions = New DataTable
        dgrdConditions.DataSource = Nothing
        dgrdConditions.DataBind()
        MultiView1.ActiveViewIndex = 0
        showButtons(False)
        ShowControls(False)
        ShowLables(False)
        ShowPanels(False)
        txtFrom.Text = ""
        txtReportName.Text = ""
        txtSearchKey.Text = ""
        txtTo.Text = ""
        dteFromDate.Clear()
        dteToDate.Clear()
        clearSelectColumnddl()
        clearSelectOperatorddl()
        clearconditionddl()
        lbxItemTypes2.Items.Clear()
        lbxItemTypes.Items.Clear()
    End Sub

    Protected Sub grdDynamicReport_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdDynamicReport.NeedDataSource
        grdDynamicReport.DataSource = ReportDataSet

    End Sub

    Protected Sub btnExportToPDF_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExportToPDF.Click

        ConfigureExport()
        grdDynamicReport.MasterTableView.ExportToPdf()

    End Sub

    Protected Sub btnExportToExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExportToExcel.Click
        ConfigureExport()
        grdDynamicReport.MasterTableView.ExportToExcel()
    End Sub

    Public Sub ConfigureExport()
        grdDynamicReport.ExportSettings.ExportOnlyData = True
        grdDynamicReport.ExportSettings.IgnorePaging = True
        grdDynamicReport.ExportSettings.OpenInNewWindow = True
        grdDynamicReport.ExportSettings.FileName = txtReportName.Text

        'PDF Settings
        grdDynamicReport.ExportSettings.Pdf.PaperSize = GridPaperSize.A4
        grdDynamicReport.ExportSettings.Pdf.PageWidth = Unit.Parse("600mm")
        grdDynamicReport.ExportSettings.Pdf.PageHeight = Unit.Parse("162mm")
        grdDynamicReport.ExportSettings.Pdf.AllowPrinting = True
        grdDynamicReport.ExportSettings.Pdf.AllowAdd = False
        grdDynamicReport.ExportSettings.Pdf.AllowCopy = False
        grdDynamicReport.ExportSettings.Pdf.AllowModify = False

    End Sub

    Protected Sub grdDynamicReport_ColumnCreated(ByVal sender As Object, ByVal e As GridColumnCreatedEventArgs) Handles grdDynamicReport.ColumnCreated
        Dim arrStr() As String = Nothing
        If e.Column.UniqueName.Contains("_") Then
            arrStr = e.Column.UniqueName.Split("_")
            e.Column.HeaderText = arrStr(arrStr.Length - 1)
            e.Column.AutoPostBackOnFilter = True
        End If
    End Sub
    Protected Sub grdDynamicReport_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles grdDynamicReport.ItemDataBound
        Try


            If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
                Dim item As GridDataItem
                item = e.Item

                If ((Not String.IsNullOrEmpty(item("DOB").Text)) And (Not item("DOB").Text = "&nbsp;")) Then
                    Dim DOB As DateTime = item("DOB").Text
                    item("DOB").Text = DOB.ToShortDateString()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

#End Region
    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("../Reports/Dynamic_Reports.aspx")
    End Sub

End Class
