Imports SmartV.UTILITIES
Imports TA.Security
Imports System.Data
Imports TA_ApproveViolationToERP
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Partial Class DailyTasks_ApproveViolationToERP
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Protected dir As String
    Private objVersion As SmartV.Version.version
    Dim CultureInfo As System.Globalization.CultureInfo
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Private objApproveViolationToERP As ApproveViolationToERP
    Dim cryRpt As New ReportDocument

#End Region

#Region "Properties"

    Public Property IsRetrieveRecord() As Boolean
        Get
            Return ViewState("IsRetrieveRecord")
        End Get
        Set(ByVal value As Boolean)
            ViewState("IsRetrieveRecord") = value
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

    Public Property RPTName() As String
        Get
            Return ViewState("RPTName")
        End Get
        Set(ByVal value As String)
            ViewState("RPTName") = value
        End Set
    End Property

    Public Property FK_EmployeeId() As Integer
        Get
            Return ViewState("FK_EmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("FK_EmployeeId") = value
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

        If Not Page.IsPostBack Then

            Dim browserType As String = Request.Browser.Type
            Dim browserVersion As String = Request.Browser.Version

            If browserType.Contains("IE") Then

            End If
            getPageTitle()
            ddlMonth_Bind()
            ddlYear_Bind()
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            IsRetrieveRecord = False
        End If
        'dgrdViewApproveViolation.Columns.RemoveAt(0)
        'FillImageURL()
    End Sub

    Protected Sub dgrdViewApproveViolation_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
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
            If (IsRetrieveRecord) Then
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

                    dt = .GetAll_ByFilter_Header
                    dgrdViewApproveViolation.DataSource = dt
                    dgrdViewApproveViolation.DataBind()

                End With
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

        Dim listEmployee As New Dictionary(Of String, Integer)()

        'Dim checked As Boolean
        Dim ErrNo As Integer = 0
        Dim Count As Integer = 0


        For Each item As GridDataItem In dgrdViewApproveViolation.MasterTableView.Items
            Dim childtableView As GridTableView = CType(item.ChildItem.NestedTableViews(0), GridTableView)

            For Each childItem As GridDataItem In childtableView.Items
                Dim chk As CheckBox = DirectCast(childItem.FindControl("chk"), CheckBox)
                If Not chk Is Nothing Then
                    Dim new_checked = chk.Checked
                    If new_checked Then
                        Dim FK_EmployeeId As String = childItem.GetDataKeyValue("FK_EmployeeId").ToString()
                        Dim FK_EmployeeNo As String = item("EmployeeNo").Text.ToString()
                        Dim strM_DATE As String = childItem("M_DATE").Text.ToString
                        Dim M_DATE As DateTime = Date.ParseExact(strM_DATE, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                        Dim ViolationType As String = childItem("REMARKS").Text.ToString()

                        With objApproveViolationToERP

                            .FK_EmployeeId = FK_EmployeeId
                            .ViolationDate = M_DATE
                            .ViolationDateNum = DateToString(M_DATE)
                            .ViolationType = ViolationType
                            .IsApproved = True
                            .APPROVED_BY = SessionVariables.LoginUser.UsrID
                            .APPROVED_DATE = Now
                            If Not listEmployee.ContainsKey(.FK_EmployeeId.ToString() + .ViolationDateNum.ToString()) Then
                                ErrNo = .Add()
                                If ErrNo = 0 Then
                                    listEmployee.Add(.FK_EmployeeId.ToString() + .ViolationDateNum.ToString(), .ViolationDateNum)
                                    Count = Count + 1
                                End If
                            End If
                        End With

                    End If
                End If
            Next
        Next

      
        'For Each item2 As GridDataItem In dgrdViewApproveViolation.Items
        '    Dim _FK_EmployeeId As String = item2.GetDataKeyValue("FK_EmployeeId").ToString()
        '    If item2.OwnerTableView.Name = "EmployeeGridView" Then
        '        For Each item As GridDataItem In item2.OwnerTableView.Items
        '            Dim chk As CheckBox = DirectCast(item.FindControl("chk"), CheckBox)
        '            If Not chk Is Nothing Then
        '                checked = chk.Checked
        '                If checked Then
        '                    Dim FK_EmployeeId As String = item.GetDataKeyValue("FK_EmployeeId").ToString()
        '                    Dim FK_EmployeeNo As String = item("EmployeeNo").Text.ToString()
        '                    Dim M_DATE As DateTime = item("M_DATE").Text.ToString()
        '                    Dim ViolationType As String = item("REMARKS").Text.ToString()
        '                    With objApproveViolationToERP

        '                        .FK_EmployeeId = FK_EmployeeId
        '                        .ViolationDate = M_DATE
        '                        .ViolationDateNum = DateToString(M_DATE)
        '                        .ViolationType = ViolationType
        '                        .IsApproved = True
        '                        .APPROVED_BY = SessionVariables.LoginUser.UsrID
        '                        .APPROVED_DATE = Now

        '                        If Not listEmployee.ContainsKey(.FK_EmployeeId.ToString() + .ViolationDateNum.ToString()) And Not listEmployee.ContainsValue(.ViolationDateNum) Then
        '                            ErrNo = .Add()
        '                            If ErrNo = 0 Then
        '                                listEmployee.Add(.FK_EmployeeId.ToString() + .ViolationDateNum.ToString(), .ViolationDateNum)
        '                                Count = Count + 1
        '                            End If
        '                        End If
        '                    End With

        '                End If
        '            End If
        '        Next
        '    End If
        'Next
        If ErrNo = 0 AndAlso Count > 0 Then
            If SessionVariables.CultureInfo = "ar-JO" Then
                CtlCommon.ShowMessage(Page, " مخالفة بنجاح" & Count & " تم الموافقة على", "success")
            Else
                CtlCommon.ShowMessage(Page, "You Have Approved on " & Count & " Violation(s) Successfully", "success")
            End If
            FillGridView()
        ElseIf Count = 0 Then
            If SessionVariables.CultureInfo = "ar-JO" Then
                CtlCommon.ShowMessage(Page, " الرجاء اختيار مخالفة واحدة على الاقل", "info")
            Else
                CtlCommon.ShowMessage(Page, "Please select one violation at least", "info")
            End If

        End If
    End Sub

    Protected Sub btnExportToPDF_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExportToPDF.Click
        'ConfigureExport()
        '  dgrdViewApproveViolation.MasterTableView.ExportToPdf()
        BindReport()
    End Sub

    Protected Sub btnExportToExcel_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExportToExcel.Click
        'ConfigureExport()
        'dgrdViewApproveViolation.MasterTableView.ExportToExcel()
        ExportViolationToExcel()
    End Sub

    Protected Sub dgrdViewApproveViolation_PdfExporting(sender As Object, e As Telerik.Web.UI.GridPdfExportingArgs) Handles dgrdViewApproveViolation.PdfExporting
        Dim strHeader As String = "<h1 style=""text-align:center;font-family:Verdana;"">" + "Violation of Employees" + "</h1>"
        e.RawHTML = strHeader.ToString() + e.RawHTML
    End Sub

    Protected Sub dgrdViewApproveViolation_GridExporting(sender As Object, e As Telerik.Web.UI.GridExportingArgs) Handles dgrdViewApproveViolation.GridExporting
        Dim customHTML As String = "<h1 style=""text-align:right;font-family:Verdana;"">" + "Violation of Employees" + "</h1>"
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

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        cryRpt.Close()
        cryRpt.Dispose()

    End Sub

#End Region

#Region "Methods"

    Private Sub getPageTitle()
        Dim objSysForms As New SYSForms
        Dim dt As New DataTable
        dt = objSysForms.GetByPK(970)
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
            'strMonthName = CultureInfo.CurrentUICulture.DateTimeFormat.GetMonthName(i).ToString()
            If Lang = CtlCommon.Lang.AR Then
                Dim GetNames As New System.Globalization.CultureInfo("ar-EG")
                GetNames.DateTimeFormat.GetMonthName(1)
                GetNames.DateTimeFormat.GetDayName(1)
                strMonthName = GetNames.DateTimeFormat.GetMonthName(i).ToString()
            Else
                strMonthName = CultureInfo.CurrentUICulture.DateTimeFormat.GetMonthName(i).ToString()
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

            If EmployeeFilter1.ShowDirectStaffCheck = True Then
                .DirectStaffOnly = EmployeeFilter1.DirectStaffOnly
            Else
                .DirectStaffOnly = False
            End If


            .Year = ddlYear.SelectedValue
            .Month = ddlMonth.SelectedValue

            dt = .GetAll_ByFilter_Header
            dgrdViewApproveViolation.DataSource = dt
            dgrdViewApproveViolation.DataBind()
        End With
    End Sub

    Private Sub RadGrid1_DetailTableDataBind(ByVal source As Object, ByVal e As GridDetailTableDataBindEventArgs) Handles dgrdViewApproveViolation.DetailTableDataBind
        Dim parentItem As GridDataItem = CType(e.DetailTableView.ParentItem, GridDataItem)
        If parentItem.Edit Then
            Return
        End If
        If (e.DetailTableView.DataMember = "") Then
            FK_EmployeeId = parentItem.GetDataKeyValue("FK_EmployeeId")
            Dim ds As DataTable = CType(e.DetailTableView.DataSource, DataTable)

            Dim dtResult As DataTable
            Dim objApproveViolationToERPEmployee = New ApproveViolationToERP
            With objApproveViolationToERPEmployee
                .FK_EmployeeId = FK_EmployeeId

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
                dtResult = .GetAll_ByFilter
                dtCurrent = dtResult
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
        ' dgrdViewApproveViolation.MasterTableView.GetColumn("chk").Visible = False
        'dgrdViewApproveViolation.MasterTableView.GetColumn("ImageUrl").Visible = False

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
                cell.Style("text-align") = "left"
                cell.Style("font-size") = "12pt"
                cell.Style("padding") = "10px"
                cell.Font.Bold = True
            Next
        End If
        If TypeOf item Is GridDataItem Then
            item.Style("font-size") = "12px"
            item.Style("padding") = "10px"
            item.Style("background-color") = If(item.ItemType = GridItemType.AlternatingItem, "#cccccc", "#f1f1f1")
        End If
    End Sub

    Public Sub BindReport()
        FillDetailsGrid()
        If dtCurrent.Rows.Count > 0 Then
            Dim dt As DataTable
            RPTName = "rpt_ERP_Violations.rpt"
            dt = dtCurrent

            If (Lang = CtlCommon.Lang.AR) Then
                RPTName = "Arabic/" + RPTName
            Else
                RPTName = "English/" + RPTName
            End If
            RPTName = Server.MapPath(RPTName)
            RPTName = Replace(RPTName, "DailyTasks", "Reports")
            cryRpt = New ReportDocument
            cryRpt.Load(RPTName)
            cryRpt.SetDataSource(dt)

            CRV.ReportSource = cryRpt
            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedReport")
                Else
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NoDataFound", CultureInfo), "info")
                End If
            End If
        End If


    End Sub

    Private Sub FillDetailsGrid()

        Dim dtResult As DataTable
        Dim objApproveViolationToERPEmployee = New ApproveViolationToERP
        With objApproveViolationToERPEmployee
            .FK_EmployeeId = FK_EmployeeId

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
            dtResult = .GetAll_ByFilter
            dtCurrent = dtResult
        End With


    End Sub

    Private Sub ExportViolationToExcel()
        FillDetailsGrid()
        If dtCurrent.Rows.Count > 0 Then
            Dim dt As DataTable
            Dim NDT = New DataTable
            NDT = dtCurrent.Clone()


            NDT.Columns(4).DataType = System.Type.GetType("System.String")
            For Each row As DataRow In dtCurrent.Rows
                Dim dr As DataRow = NDT.NewRow
                For i As Integer = 0 To NDT.Columns.Count - 1
                    dr(i) = row(i)
                    If i = 4 Then
                        If Not dr(i) Is DBNull.Value Then
                            dr(i) = Convert.ToDateTime(row(i)).ToShortDateString()
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
                NDT.Columns(3).ColumnName = "تاريخ المخالفة"
                NDT.Columns.RemoveAt(4)
                NDT.Columns.RemoveAt(4)
                NDT.Columns(4).ColumnName = "نوع المخالفة"
                NDT.Columns(5).ColumnName = "بيانات سابقة"
                NDT.Columns(6).ColumnName = "تفاصيل المغادرة"
                NDT.Columns(7).ColumnName = "تفاصيل الاجازة"
            Else
                NDT.Columns.RemoveAt(0)
                NDT.Columns(0).ColumnName = "Employee No."
                NDT.Columns(1).ColumnName = "Employee Name"
                NDT.Columns(2).ColumnName = "Employee Arabic Name"
                NDT.Columns(3).ColumnName = "Violation Date"
                NDT.Columns.RemoveAt(4)
                NDT.Columns.RemoveAt(4)
                NDT.Columns(4).ColumnName = "Violation Type"
                NDT.Columns(5).ColumnName = "Previous Data"
                NDT.Columns(6).ColumnName = "Permission Details"
                NDT.Columns(7).ColumnName = "Leave Details"

            End If



            dt = NDT
            CtlCommon.ExportDataSetToExcel(dt, "ExportedReport")

        End If

    End Sub


#End Region

    'Protected Sub ToggleSelectedState(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim headerCheckBox As CheckBox = (TryCast(sender, CheckBox))
    '    Dim grdClaimDetail As GridTableView = CType(headerCheckBox.NamingContainer.NamingContainer.NamingContainer, GridTableView)

    '    For Each dataItem As GridDataItem In grdClaimDetail.Items
    '        Dim chk As CheckBox = (TryCast(dataItem.FindControl("chk"), CheckBox))
    '        chk.Checked = headerCheckBox.Checked
    '    Next
    'End Sub
    Protected Sub ToggleSelectedState(ByVal sender As Object, ByVal e As EventArgs)
        Dim headerCheckBox As CheckBox = (TryCast(sender, CheckBox))
        Dim headeritem As GridHeaderItem = CType(headerCheckBox.NamingContainer, GridHeaderItem)
        Dim parentItem As GridDataItem = CType(headeritem.OwnerTableView.ParentItem, GridDataItem)
        Dim detailTable As GridTableView = CType(parentItem.ChildItem.NestedTableViews(0), GridTableView)

        For Each dataItem As GridDataItem In detailTable.Items
            Dim chk As CheckBox = (TryCast(dataItem.FindControl("chk"), CheckBox))
            chk.Checked = headerCheckBox.Checked
            '(TryCast(dataItem.FindControl("CheckBox1"), CheckBox)).Checked = headerCheckBox.Checked
            '    dataItem.Selected = headerCheckBox.Checked
        Next
    End Sub

End Class
