Imports TA.Lookup
Imports System.Data
Imports SmartV.UTILITIES
Imports SmartV.DB
Imports System.Web.Configuration
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports System.Threading
Imports Telerik.Web.UI
Imports TA.Admin


Partial Class Reports_RDLCReportView
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Protected dir As String
    'Shared viewName As String
    Shared dtcurrent As DataTable
    Private objDALPrintPage As DALPrintPage
    Shared dt As DataTable = Nothing
    Shared dtTechConditions As DataTable = New DataTable
    Shared dtNonTechConditions As DataTable = New DataTable
    'Shared dtFilter As DataTable = New DataTable
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Dim objAPP_Settings As APP_Settings

#End Region
    
#Region "Properties"

    Public Property ReportDataSet() As DataSet
        Get
            Return ViewState("DynamicReport")
        End Get
        Set(ByVal value As DataSet)
            ViewState("DynamicReport") = value
        End Set
    End Property

#End Region

#Region "Page Events"
    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        Else
            If SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                Page.MasterPageFile = "~/default/ArabicMaster.master"
            Else
                Lang = CtlCommon.Lang.EN
                Page.MasterPageFile = "~/default/NewMaster.master"
            End If
            Page.UICulture = SessionVariables.CultureInfo
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        ElseIf SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
            lblReportTitle.HeaderText = ResourceManager.GetString("DynamicReport", CultureInfo)
            'ibtnGenerateReport.ImageUrl = "~/imageArabic/view_o.png"
            'Ibtnclr.ImageUrl = "~/imageArabic/btn_clear_02.png"
        Else
            Lang = CtlCommon.Lang.EN
            lblReportTitle.HeaderText = ResourceManager.GetString("DynamicReport", CultureInfo)
            'ibtnGenerateReport.ImageUrl = "~/images/view_o.gif"
            'Ibtnclr.ImageUrl = "~/images/btn_clear_02.gif"
        End If
        Page.Title = "Work Force Pro : :" + ResourceManager.GetString("DynamicReport", CultureInfo)
        'Me.ReportViewer1.Width = 600
        'Me.ReportViewer1.Height = 500
        If Not IsPostBack Then
            MultiView1.ActiveViewIndex = 0
            FillReportnames()

            objAPP_Settings = New APP_Settings
            With objAPP_Settings
                .GetByPK()
                rblFormat.SelectedValue = .DefaultReportFormat

            End With
        End If

    End Sub

    Protected Sub grdDynamicReport_ItemCreated(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles grdDynamicReport.ItemCreated
        ApplyStylesToPDFExport(e.Item)
    End Sub

    Private Sub ApplyStylesToPDFExport(item As GridItem)
        If TypeOf item Is GridHeaderItem Then
            For Each cell As TableCell In item.Cells
                cell.Style("font-family") = "Arial"
                cell.Style("text-align") = "left"
                cell.Style("font-size") = "11pt"
                cell.Style("padding") = "10px"
            Next
        End If
        If TypeOf item Is GridDataItem Then
            item.Style("font-size") = "9px"
            item.Style("padding") = "9px"
            item.Style("background-color") = If(item.ItemType = GridItemType.AlternatingItem, "#cccccc", "#f1f1f1")
        End If
    End Sub

    Protected Sub grdDynamicReport_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdDynamicReport.NeedDataSource
        grdDynamicReport.DataSource = ReportDataSet
    End Sub

    Public Sub ConfigureExport()
        grdDynamicReport.ExportSettings.ExportOnlyData = True
        grdDynamicReport.ExportSettings.IgnorePaging = True
        grdDynamicReport.ExportSettings.OpenInNewWindow = True
        grdDynamicReport.ExportSettings.FileName = txtReportName.Text
        'PDF Settings
        grdDynamicReport.ExportSettings.Pdf.PaperSize = GridPaperSize.A4
        grdDynamicReport.ExportSettings.Pdf.PageWidth = Unit.Parse("297mm")
        grdDynamicReport.ExportSettings.Pdf.PageHeight = Unit.Parse("210mm")
        grdDynamicReport.ExportSettings.Pdf.AllowPrinting = True

        grdDynamicReport.MasterTableView.Font.Size = 9
        grdDynamicReport.MasterTableView.Font.Name = "Arial"
        grdDynamicReport.MasterTableView.Caption = txtReportName.Text


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

    Protected Sub btnPrint_Click(sender As Object, e As System.EventArgs) Handles btnPrint.Click
        Dim dt As DataSet = ViewState("DT")
        If Not dt Is Nothing Then
            If dt.Tables(0).Rows.Count > 0 Then
                If rblFormat.SelectedValue = 1 Then
                    ConfigureExport()
                    grdDynamicReport.MasterTableView.ExportToPdf()
                ElseIf rblFormat.SelectedValue = 2 Then
                    ConfigureExport()
                    grdDynamicReport.MasterTableView.ExportToWord()
                Else
                    ConfigureExport()
                    grdDynamicReport.MasterTableView.ExportToExcel()
                End If
            End If
        End If
    End Sub

    Protected Sub ibtnGenerateReport_Click(sender As Object, e As System.EventArgs) Handles ibtnGenerateReport.Click
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

            ViewState("DT") = ds
            'For Each column As GridColumn In grdDynamicReport.MasterTableView.AutoGeneratedColumns

            'Next

            MultiView1.ActiveViewIndex = 1
        Else
            CtlCommon.ShowMessage(Page, "No Columns To display", "info")
        End If
    End Sub

    Protected Sub Ibtnclr_Click(sender As Object, e As System.EventArgs) Handles Ibtnclr.Click
        clearSelectCmbReportsddl()
        FillReportnames()
        clearall()
    End Sub

    Protected Sub grdDynamicReport_PdfExporting(sender As Object, e As Telerik.Web.UI.GridPdfExportingArgs) Handles grdDynamicReport.PdfExporting
        Dim strHeader As String = "<h1 style=""text-align:center;font-family:cellpre;"">" + txtReportName.Text + "</h1>"
        e.RawHTML = strHeader.ToString() + e.RawHTML
    End Sub

    Protected Sub CmbReports_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles CmbReports.SelectedIndexChanged
        clearall()


        Dim cs As String = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Using con As New SqlConnection(cs)
            con.Open()
            Dim cmd As SqlCommand = con.CreateCommand()
            ViewState("viewName") = dtcurrent.Select("ReportId=" & CmbReports.SelectedValue)(0)(2).ToString
            txtReportName.Text = CmbReports.SelectedItem.Text
            cmd.CommandText = "SELECT   top 1 *   FROM  " + ViewState("viewName")
            Dim rdr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.SchemaOnly)
            Dim dt As DataTable = rdr.GetSchemaTable()
            rdr.Close()
            con.Close()
            FillList(dt)
            fillColumnsddl(dt)
            fillOprtrddl()
        End Using
    End Sub

    Protected Sub ibtnItemDown_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnItemDown.Click
        movedown()
    End Sub

    Protected Sub ibtnItemUp_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnItemUp.Click
        'MoveItem(-1)
        moveup()
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

#End Region

#Region "Methods"

    Private Sub ClearConditionControls()
        txtSearchKey.Text = String.Empty
        dteFromDate.SelectedDate = Nothing
        dteToDate.SelectedDate = Nothing
        txtFrom.Text = String.Empty
        txtTo.Text = String.Empty

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

    Private Sub FillReportnames()
        clearSelectCmbReportsddl()
        Dim objReport As New TA.Reports.RDLCReport
        dtcurrent = objReport.GetDynamicReports
        CtlCommon.FillTelerikDropDownList(CmbReports, dtcurrent, Lang)
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
        'If MyDT IsNot Nothing AndAlso MyDT.Rows.Count > 0 Then
        MyRow = MyDT.NewRow()
        MyRow(0) = "OR"
        MyRow(1) = "OR"
        MyDT.Rows.Add(MyRow)
        MyRow = MyDT.NewRow()
        MyRow(0) = "And"
        MyRow(1) = "And"
        MyDT.Rows.Add(MyRow)
        CtlCommon.FillTelerikDropDownList(radSelectOperator, MyDT)
        'Else
        'clearSelectOperatorddl()
        'End If

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
        If dgrdConditions.Rows.Count > 0 And radSelectOperator.SelectedValue = "-1" Then
            CtlCommon.ShowMessage(Me.Page, "Please Select Operator", "info")
            Return
        ElseIf dgrdConditions.Rows.Count = 0 And radSelectOperator.SelectedValue <> "-1" Then
            CtlCommon.ShowMessage(Me.Page, "The First Search Criteria Does Not Have To Had Operator, Please Remove Operator From The Search Criteria", "info")
            Return
        End If
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
        ClearConditionControls()
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
                CtlCommon.ShowMessage(Me.Page, "Condition already exists", "info")
                Exit Sub
            End If
        Next

        For Each drNonTech As DataRow In dtNonTechConditions.Rows
            If MyRowNonTech(1).ToString.Contains(drNonTech("CondValue").ToString) Then
                CtlCommon.ShowMessage(Me.Page, "Condition already exists", "info")
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

End Class

