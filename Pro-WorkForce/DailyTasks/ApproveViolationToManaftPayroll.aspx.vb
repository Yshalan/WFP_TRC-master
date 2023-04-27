Imports SmartV.UTILITIES
Imports TA.Security
Imports System.Data
Imports TA_ApproveViolationToERP

Partial Class DailyTasks_ApproveViolationToManaftPayroll
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Protected dir As String
    Private objVersion As SmartV.Version.version
    Dim CultureInfo As System.Globalization.CultureInfo
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Private objApproveViolationToERP As ApproveViolationToERP
#End Region

#Region "Properties"

    Public Property ManafthPayrollData() As DataTable
        Get
            Return System.Web.HttpContext.Current.Session("ManafthPayrollData")
        End Get
        Set(ByVal value As DataTable)
            System.Web.HttpContext.Current.Session("ManafthPayrollData") = value
        End Set
    End Property

    Public Property IsRetrieveRecord() As Boolean
        Get
            Return System.Web.HttpContext.Current.Session("ManafthPayrollIsRetrieveRecord")
        End Get
        Set(ByVal value As Boolean)
            System.Web.HttpContext.Current.Session("ManafthPayrollIsRetrieveRecord") = value
        End Set
    End Property

    Public Property IsCheckAll() As Boolean
        Get
            Return System.Web.HttpContext.Current.Session("ManafthPayrollIsCheckAll")
        End Get
        Set(ByVal value As Boolean)
            System.Web.HttpContext.Current.Session("ManafthPayrollIsCheckAll") = value
        End Set
    End Property


    Public Property SelectedYear() As String
        Get
            Return System.Web.HttpContext.Current.Session("ManafthPayrollSelectedYear")
        End Get
        Set(ByVal value As String)
            System.Web.HttpContext.Current.Session("ManafthPayrollSelectedYear") = value
        End Set
    End Property

    Public Property SelectedMonth() As String
        Get
            Return System.Web.HttpContext.Current.Session("ManafthPayrollSelectedMonth")
        End Get
        Set(ByVal value As String)
            System.Web.HttpContext.Current.Session("ManafthPayrollSelectedMonth") = value
        End Set
    End Property

    Public Property IsDelay() As Boolean
        Get
            Return System.Web.HttpContext.Current.Session("ManafthPayrollIsDelay")
        End Get
        Set(ByVal value As Boolean)
            System.Web.HttpContext.Current.Session("ManafthPayrollIsDelay") = value
        End Set
    End Property

    Public Property IsEarlyOut() As Boolean
        Get
            Return System.Web.HttpContext.Current.Session("ManafthPayrollIsEarlyOut")
        End Get
        Set(ByVal value As Boolean)
            System.Web.HttpContext.Current.Session("ManafthPayrollIsEarlyOut") = value
        End Set
    End Property
#End Region


#Region "Page Events"
  
    Protected Sub chk_CheckedChanged(sender As Object, e As EventArgs)
        Dim count As Integer = 0
        For Each item As GridDataItem In dgrdViewApproveViolation.MasterTableView.Items
            Dim chk As CheckBox = DirectCast(item.FindControl("chk"), CheckBox)

            Dim dr As DataRow = ManafthPayrollData.[Select](String.Format("RecordId={0}", item.GetDataKeyValue("RecordId").ToString())).FirstOrDefault()
            If dr IsNot Nothing Then
                dr("IsSelected") = chk.Checked
            End If

            If chk.Checked Then
                count += 1
            End If
        Next

        For Each headerItem As GridHeaderItem In dgrdViewApproveViolation.MasterTableView.GetItems(GridItemType.Header)
            Dim chk As CheckBox = DirectCast(headerItem("chk").Controls(1), CheckBox)
            ' Get the header checkbox
            If dgrdViewApproveViolation.MasterTableView.Items.Count = count Then
                chk.Checked = True
                IsCheckAll = True
            Else
                chk.Checked = False
                IsCheckAll = False
            End If
        Next
    End Sub

    Protected Sub chkAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkAll As CheckBox = TryCast(sender, CheckBox)
        IsCheckAll = chkAll.Checked
        For Each item As GridDataItem In dgrdViewApproveViolation.Items
            Dim chk As CheckBox = DirectCast(item.FindControl("chk"), CheckBox)
            If (chk IsNot Nothing) Then
                chk.Checked = chkAll.Checked
            End If
            Dim dr As DataRow = ManafthPayrollData.[Select](String.Format("RecordId={0}", item.GetDataKeyValue("RecordId").ToString())).FirstOrDefault()
            If dr IsNot Nothing Then
                dr("IsSelected") = IsCheckAll
            End If
        Next
    End Sub

    Private Function CheckAll() As String
        Return ""
    End Function

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

        If Not Page.IsPostBack Then

            Dim browserType As String = Request.Browser.Type
            Dim browserVersion As String = Request.Browser.Version

            If browserType.Contains("IE") Then

            End If
            getPageTitle()
            ddlMonth_Bind()
            ddlYear_Bind()

            If (SelectedMonth IsNot Nothing) Then
                ddlMonth.SelectedValue = SelectedMonth
            End If

            If (SelectedYear IsNot Nothing) Then
                ddlYear.SelectedValue = SelectedYear
            End If

            chkDelay.Checked = IsDelay
            chkEarlyOut.Checked = IsEarlyOut


            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            IsRetrieveRecord = False
        End If
    End Sub

    Protected Sub dgrdViewApproveViolation_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid" Then

        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdViewApproveViolation.Skin))
    End Function

    Protected Sub dgrdViewApproveViolation_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdViewApproveViolation.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
        End If
        FillImageURL()
    End Sub

    Protected Sub dgrdViewApproveViolation_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdViewApproveViolation.NeedDataSource
        Try
            If (ManafthPayrollData IsNot Nothing And ManafthPayrollData.Rows.Count > 0) Then
                dgrdViewApproveViolation.DataSource = ManafthPayrollData
                dgrdViewApproveViolation.Rebind()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub dgrdViewApproveViolation_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdViewApproveViolation.SelectedIndexChanged
    End Sub

    Protected Sub btnRetrive_Click(sender As Object, e As System.EventArgs) Handles btnRetrive.Click
        IsRetrieveRecord = True
        FillGridView()
    End Sub

    Protected Sub btnApprove_Click(sender As Object, e As System.EventArgs) Handles btnApprove.Click
        objApproveViolationToERP = New ApproveViolationToERP

        If (ManafthPayrollData Is Nothing) Then Exit Sub

        Dim listEmployee As New Dictionary(Of String, Integer)()

        Dim checked As Boolean
        Dim ErrNo As Integer = 0
        Dim Count As Integer = 0

        If (ManafthPayrollData IsNot Nothing And ManafthPayrollData.Rows.Count > 0) Then
            For Each item2 As DataRow In ManafthPayrollData.Rows
                Dim chk2 As Boolean = item2("IsSelected")
                checked = chk2
                If checked Then
                    Dim FK_EmployeeId As String = item2("FK_EmployeeId").ToString()
                    Dim RecordId As String = item2("RecordId").ToString()
                    With objApproveViolationToERP
                        .FK_EmployeeId = FK_EmployeeId
                        .RecordId = RecordId
                        If Not listEmployee.ContainsKey(RecordId.ToString()) And Not listEmployee.ContainsValue(RecordId) Then
                            ErrNo = .PayrollManafth_Approve(chkDelay.Checked, chkEarlyOut.Checked, chkAbsent.Checked, chkMissingIn.Checked, chkMissingOut.Checked, chkOutDuration.Checked)
                            If ErrNo = 0 Then
                                listEmployee.Add(RecordId.ToString(), RecordId.ToString())
                                Count = Count + 1
                            End If
                        End If
                    End With
                Else
                    Dim RecordId As String = item2("RecordId").ToString()
                    With objApproveViolationToERP
                        .RecordId = RecordId
                        ErrNo = .ManafthPayrollDeduction_Remove()
                    End With
                End If
            Next

        End If
        If Count > 0 Then
            FillGridViewFinalApproval()
        ElseIf Count = 0 Then
            If SessionVariables.CultureInfo = "ar-JO" Then
                CtlCommon.ShowMessage(Page, " الرجاء اختيار مخالفة واحدة على الاقل", "info")
            Else
                CtlCommon.ShowMessage(Page, "Please select one violation at least", "info")
            End If
        End If
    End Sub

#End Region

#Region "Methods"

    Private Sub getPageTitle()
        Dim objSysForms As New SYSForms
        Dim dt As New DataTable
        dt = objSysForms.GetByPK(977)
        If SessionVariables.CultureInfo = "ar-JO" Then
            PageHeader1.HeaderText = dt.Rows(0)("Desc_Ar")
        Else
            PageHeader1.HeaderText = dt.Rows(0)("Desc_En")
        End If
    End Sub

    Private Sub ddlYear_Bind()
        Dim year As Integer = DateTime.Now.Year
        Dim dt As New DataTable
        dt.Columns.Add("val")
        dt.Columns.Add("txt")

        For i As Integer = year - 5 To year + 5
            Dim dr As DataRow = dt.NewRow()
            dr("val") = i
            dr("txt") = i
            dt.Rows.Add(dr)
        Next

        ddlYear.SelectedValue = Date.Today.Year
        ddlYear.DataSource = dt
        ddlYear.DataBind()
    End Sub

    Private Sub ddlMonth_Bind()
        Dim dt As New DataTable
        dt.Columns.Add("val")
        dt.Columns.Add("txt")

        For i As Integer = 1 To 12
            Dim dr As DataRow = dt.NewRow()
            dr("val") = i

            Dim strMonthName As String
            If Lang = CtlCommon.Lang.AR Then
                Dim GetNames As New System.Globalization.CultureInfo("ar-EG")
                GetNames.DateTimeFormat.GetMonthName(1)
                GetNames.DateTimeFormat.GetDayName(1)
                strMonthName = GetNames.DateTimeFormat.GetMonthName(i).ToString()
            Else
                strMonthName = System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.GetMonthName(i).ToString()
            End If

            dr("txt") = strMonthName
            dt.Rows.Add(dr)
        Next
        ddlMonth.SelectedValue = Now.Month
        ddlMonth.DataSource = dt
        ddlMonth.DataBind()

    End Sub

    Private Sub FillGridView()
        Dim dt As DataTable
        objApproveViolationToERP = New ApproveViolationToERP
        With objApproveViolationToERP
            If EmployeeFilter1.EmployeeId <> 0 Then

                .FK_EmployeeId = EmployeeFilter1.EmployeeId
            Else
                .FK_EmployeeId = Nothing
            End If
            If Not EmployeeFilter1.CompanyId = 0 Then
                .CompanyId = EmployeeFilter1.CompanyId
            Else
                .CompanyId = Nothing
            End If
            If EmployeeFilter1.FilterType = "C" Then
                If Not EmployeeFilter1.EntityId = 0 Then
                    .EntityId = EmployeeFilter1.EntityId
                Else
                    .EntityId = Nothing
                End If
            ElseIf EmployeeFilter1.FilterType = "W" Then
                If Not EmployeeFilter1.EntityId = 0 Then
                    .WorkLocationId = EmployeeFilter1.EntityId
                Else
                    .WorkLocationId = Nothing
                End If
            ElseIf EmployeeFilter1.FilterType = "L" Then
                If Not EmployeeFilter1.EntityId = 0 Then
                    .LogicalGroupId = EmployeeFilter1.EntityId
                Else
                    .LogicalGroupId = Nothing
                End If
            End If

            .Year = ddlYear.SelectedValue
            .Month = ddlMonth.SelectedValue

            dt = .GetAll_ByFilter_Header_ManaftPayroll
            ManafthPayrollData = dt
            dgrdViewApproveViolation.DataSource = ManafthPayrollData
            dgrdViewApproveViolation.DataBind()
        End With
    End Sub

    Private Sub RadGrid1_DetailTableDataBind(ByVal source As Object, ByVal e As GridDetailTableDataBindEventArgs) Handles dgrdViewApproveViolation.DetailTableDataBind
        Dim parentItem As GridDataItem = CType(e.DetailTableView.ParentItem, GridDataItem)
        If parentItem.Edit Then
            Return
        End If
        If (e.DetailTableView.DataMember = "") Then
            Dim EmployeeId = parentItem.GetDataKeyValue("FK_EmployeeId")
            Dim ds As DataTable = CType(e.DetailTableView.DataSource, DataTable)

            Dim dtResult As DataTable
            Dim objApproveViolationToERPEmployee = New ApproveViolationToERP
            With objApproveViolationToERPEmployee
                .FK_EmployeeId = EmployeeId

                If Not EmployeeFilter1.CompanyId = 0 Then
                    .CompanyId = EmployeeFilter1.CompanyId
                Else
                    .CompanyId = Nothing
                End If
                If EmployeeFilter1.FilterType = "C" Then
                    If Not EmployeeFilter1.EntityId = 0 Then
                        .EntityId = EmployeeFilter1.EntityId
                    Else
                        .EntityId = Nothing
                    End If
                ElseIf EmployeeFilter1.FilterType = "W" Then
                    If Not EmployeeFilter1.EntityId = 0 Then
                        .WorkLocationId = EmployeeFilter1.EntityId
                    Else
                        .WorkLocationId = Nothing
                    End If
                ElseIf EmployeeFilter1.FilterType = "L" Then
                    If Not EmployeeFilter1.EntityId = 0 Then
                        .LogicalGroupId = EmployeeFilter1.EntityId
                    Else
                        .LogicalGroupId = Nothing
                    End If
                End If

                .Year = ddlYear.SelectedValue
                .Month = ddlMonth.SelectedValue
                dtResult = .GetAll_ByFilter_ManaftPayroll
            End With
            e.DetailTableView.DataSource = dtResult
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
        Return TempDate.Year.ToString() + tempMonth + tempDay
    End Function

    Private Sub FillImageURL()
    End Sub

    Protected Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs)
        Dim chk1 As CheckBox = DirectCast(sender, CheckBox)
        Dim item As GridDataItem = DirectCast(chk1.NamingContainer, GridDataItem)
        For Each childitems As GridDataItem In item.ChildItem.NestedTableViews(0).Items
            Dim chk2 As CheckBox = DirectCast(childitems.FindControl("CheckBox2"), CheckBox)
            chk2.Checked = chk1.Checked
            For Each grandchilditems As GridDataItem In childitems.ChildItem.NestedTableViews(0).Items
                Dim chk3 As CheckBox = DirectCast(grandchilditems.FindControl("CheckBox3"), CheckBox)
                chk3.Checked = chk1.Checked
            Next
        Next
    End Sub

    Public Sub ConfigureExport()
        '"Violation of Employees"
        dgrdViewApproveViolation.MasterTableView.GetColumn("chk").Visible = False
        dgrdViewApproveViolation.MasterTableView.GetColumn("ImageUrl").Visible = False

        dgrdViewApproveViolation.ExportSettings.ExportOnlyData = True
        dgrdViewApproveViolation.ExportSettings.IgnorePaging = True
        dgrdViewApproveViolation.ExportSettings.OpenInNewWindow = True
        dgrdViewApproveViolation.ExportSettings.FileName = "Violation of Employees"
        'PDF Settings
        dgrdViewApproveViolation.ExportSettings.Pdf.PaperSize = GridPaperSize.A4
        dgrdViewApproveViolation.ExportSettings.Pdf.PageWidth = Unit.Parse("600mm")
        dgrdViewApproveViolation.ExportSettings.Pdf.PageHeight = Unit.Parse("162mm")
        dgrdViewApproveViolation.ExportSettings.Pdf.AllowPrinting = True
        dgrdViewApproveViolation.MasterTableView.Font.Name = "Arial Unicode MS"

    End Sub

    Private Sub ApplyStylesToPDFExport(item As GridItem)
        If TypeOf item Is GridHeaderItem Then
            For Each cell As TableCell In item.Cells
                cell.Style("font-family") = "Arial"
                cell.Style("font-size") = "12pt"
                cell.Style("padding") = "8px"
                cell.Font.Bold = True
            Next
        End If
        If TypeOf item Is GridDataItem Then
            item.Style("font-size") = "12px"
            item.Style("padding") = "10px"
            item.Style("background-color") = If(item.ItemType = GridItemType.AlternatingItem, "#cccccc", "#f1f1f1")
        End If
    End Sub

#End Region

    Protected Sub btnExportToPDF_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExportToPDF.Click
        ConfigureExport()
        dgrdViewApproveViolation.MasterTableView.ExportToPdf()
    End Sub

    Protected Sub btnExportToExcel_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExportToExcel.Click
        ConfigureExport()
        dgrdViewApproveViolation.MasterTableView.ExportToExcel()
    End Sub

    Protected Sub dgrdViewApproveViolation_PdfExporting(sender As Object, e As Telerik.Web.UI.GridPdfExportingArgs) Handles dgrdViewApproveViolation.PdfExporting
        Dim strHeader As String = "<h1 style=""text-align:center;font-family:Verdana;"">" + "Violation of Employees" + "</h1>"
        e.RawHTML = strHeader.ToString() + e.RawHTML
    End Sub

    Protected Sub dgrdViewApproveViolation_GridExporting(sender As Object, e As Telerik.Web.UI.GridExportingArgs) Handles dgrdViewApproveViolation.GridExporting
        Dim customHTML As String = "<h1 style=""text-align:center;font-family:Verdana;"">" + "Violation of Employees" + "</h1>"

        e.ExportOutput = e.ExportOutput.Replace("<body>", [String].Format("<body>{0}", customHTML))
    End Sub

    Protected Sub dgrdViewApproveViolation_ItemCreated(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdViewApproveViolation.ItemCreated
        ApplyStylesToPDFExport(e.Item)
    End Sub

    Protected Sub dgrdViewApproveViolation_ColumnCreated(ByVal sender As Object, ByVal e As GridColumnCreatedEventArgs) Handles dgrdViewApproveViolation.ColumnCreated
        Dim arrStr() As String = Nothing
        If e.Column.UniqueName.Contains("_") Then
            arrStr = e.Column.UniqueName.Split("_")
            e.Column.HeaderText = arrStr(arrStr.Length - 1)
            e.Column.AutoPostBackOnFilter = True

        End If
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        objApproveViolationToERP = New ApproveViolationToERP
        Dim listEmployee As New Dictionary(Of String, Integer)()
        Dim ErrNo As Integer = 0
        Dim Count As Integer = 0
        For Each item2 As GridDataItem In dgDeduction.Items
            Dim DeductionId As String = item2.GetDataKeyValue("DeductionId").ToString()
            With objApproveViolationToERP
                .DeductionId = DeductionId
                If Not listEmployee.ContainsKey(DeductionId.ToString()) And Not listEmployee.ContainsValue(DeductionId) Then
                    ErrNo = .PayrollManafth_FinalApprove(chkDelay.Checked, chkEarlyOut.Checked, chkAbsent.Checked, chkMissingIn.Checked, chkMissingOut.Checked, chkOutDuration.Checked)
                    If ErrNo = 0 Then
                        listEmployee.Add(DeductionId.ToString(), DeductionId.ToString())
                        Count = Count + 1
                    End If
                End If
            End With
        Next

        If ErrNo = 0 AndAlso Count > 0 Then
            If SessionVariables.CultureInfo = "ar-JO" Then
                CtlCommon.ShowMessage(Page, " مخالفة بنجاح" & Count & " تم الموافقة على", "success")
            Else
                CtlCommon.ShowMessage(Page, "You are Approved on " & Count & " Violations Successfully", "success")
            End If
            FillGridView()
            mvApproval.SetActiveView(vwList)
            mvApproval.ActiveViewIndex = 0
        ElseIf Count = 0 Then
            If SessionVariables.CultureInfo = "ar-JO" Then
                CtlCommon.ShowMessage(Page, " الرجاء اختيار مخالفة واحدة على الاقل", "info")
            Else
                CtlCommon.ShowMessage(Page, "Please select one violation at least", "info")
            End If
        End If
    End Sub

    Protected Sub btnGoBackList_Click(sender As Object, e As EventArgs)
        mvApproval.SetActiveView(vwList)
        mvApproval.ActiveViewIndex = 0
    End Sub

    Private Sub FillGridViewFinalApproval()
        objApproveViolationToERP = New ApproveViolationToERP
        objApproveViolationToERP.Year = ddlYear.SelectedValue
        objApproveViolationToERP.Month = ddlMonth.SelectedValue
        Dim dt = objApproveViolationToERP.PayrollManafth_GetFinalApproval()
        dgDeduction.DataSource = dt
        dgDeduction.DataBind()

        mvApproval.SetActiveView(vwFinalApproval)
        mvApproval.ActiveViewIndex = 1
    End Sub

    Protected Sub dgDeduction_ItemCommand(sender As Object, e As GridCommandEventArgs)
        Dim ErrNo As Integer
        If (e.CommandName = "Remove") Then
            objApproveViolationToERP = New ApproveViolationToERP()
            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
            Dim RecordId As String = item.GetDataKeyValue("ReferenceId").ToString()

            With objApproveViolationToERP
                .RecordId = RecordId
                ErrNo = .ManafthPayrollDeduction_Remove()
            End With
            If (ErrNo = 0) Then
                FillGridViewFinalApproval()
            End If
        End If
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs)
        SelectedYear = ddlYear.SelectedValue
    End Sub

    Protected Sub ddlMonth_SelectedIndexChanged(sender As Object, e As EventArgs)
        SelectedMonth = ddlMonth.SelectedValue
    End Sub

    Protected Sub chkDelay_CheckedChanged(sender As Object, e As EventArgs)
        IsDelay = chkDelay.Checked
    End Sub

    Protected Sub chkEarlyOut_CheckedChanged(sender As Object, e As EventArgs)
        IsEarlyOut = chkEarlyOut.Checked
    End Sub
End Class
