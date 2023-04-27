Imports SmartV.UTILITIES
Imports TA.Admin.OrgCompany
Imports TA.Lookup
Imports System.Data
Imports TA.Employees
Imports TA.Definitions
Imports TA.Security
Imports TA.Admin
Imports SmartV.Version
Imports System.Drawing.Printing
Imports TA.Reports
Imports CrystalDecisions.CrystalReports.Engine
Imports QRCoder
Imports System.Drawing
Imports TA.Card_Request


Partial Class Employee_CardPrinting
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Public Lang As CtlCommon.Lang
    Dim objCards As Emp_Cards
    Private objOrgCompany As OrgCompany
    Private objVersion As SmartV.Version.version
    Public MsgLang As String
    Private objAPP_Settings As APP_Settings
    Private objReport As Report
    Dim objReportDocument As New ReportDocument
    Private objCard_Template As Card_Template
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private objEmp_Designation As Emp_Designation
    Private objCardType As CardTypes
    Private objCardRequest As Card_Request
#End Region

#Region "Properties"

    Public Property dtCurrent() As DataTable
        Get
            Return ViewState("dtCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
        End Set
    End Property

    Public Property PageNo() As Integer
        Get
            Return ViewState("PageNo")
        End Get
        Set(ByVal value As Integer)
            ViewState("PageNo") = value
        End Set
    End Property
    Public Property Emp_RecordCount() As Integer
        Get
            Return ViewState("Emp_RecordCount")
        End Get
        Set(ByVal value As Integer)
            ViewState("Emp_RecordCount") = value
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

    Public Property EmployeeIds() As String
        Get
            Return ViewState("EmployeeIds")
        End Get
        Set(ByVal value As String)
            ViewState("EmployeeIds") = value
        End Set
    End Property
    Public Property EmpId() As String
        Get
            Return ViewState("EmpId")
        End Get
        Set(ByVal value As String)
            ViewState("EmpId") = value
        End Set
    End Property

    Public Property dtFillddl() As DataTable
        Get
            Return ViewState("dtFillddl")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtFillddl") = value
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
                MsgLang = "ar"
                Page.MasterPageFile = "~/default/ArabicMaster.master"
            Else
                Lang = CtlCommon.Lang.EN
                MsgLang = "en"
                Page.MasterPageFile = "~/default/NewMaster.master"
            End If
            Page.UICulture = SessionVariables.CultureInfo


        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If

            If (objVersion.HasMultiCompany() = False) Then
                RadCmbBxCompanies.SelectedValue = objVersion.GetCompanyId()
                'RadCmbBxCompanies_SelectedIndexChanged(Nothing, Nothing)
                trcompany.Visible = False
            End If
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            userCtrlCardPrintHeader.HeaderText = ResourceManager.GetString("CardPrinting", CultureInfo)
            'objAPP_Settings = New APP_Settings
            'With objAPP_Settings
            '    .GetByPK()
            '    If .FillCheckBoxList = 1 Then
            '        FillEmployee()
            '    End If
            'End With
            FillCompany()
            FillDesignations()
            FillPrinter()
            'FillTemplates()
            'FillEmployee()
            'FillEmpInQueue()


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

    Protected Sub btnAddtoQueue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddtoQueue.Click
        Try
            objCardRequest = New Card_Request
            Dim intCount As Integer = 0
            For Each obox As ListItem In cblEmpList.Items
                If obox.Selected Then
                    intCount += 1
                    objCardRequest.CardRequestId = obox.Value
                    objCardRequest.Status = 14
                    objCardRequest.CardType = ddlCardDesign.SelectedValue
                    objCardRequest.UpdateStatus()
                End If
            Next
            'For Each obox As ListItem In cblEmpList.Items
            '    If obox.Selected Then
            '        intCount += 1
            '        objCards.EmpId = obox.Value
            '        objCards.CreatedBy = SessionVariables.LoginUser.ID
            '        objCards.AddToQueue()
            '    End If
            'Next

            FillEmployee()
            FillEmpInQueue()
            If intCount = 0 Then
                If Lang = CtlCommon.Lang.EN Then
                    CtlCommon.ShowMessage(Me.Page, "Please select Employee to add to print queue", "info")
                Else
                    CtlCommon.ShowMessage(Me.Page, "الرجاء اختيار موظف لاضافته للطباعة", "info")
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
    '    Try
    '        objCards = New Emp_Cards

    '        For Each obox As ListItem In cblEmpListInQueue.Items
    '            If obox.Selected Then
    '                objCards.EmpId = obox.Value
    '                objCards.DeleteFromQueue()
    '            End If
    '        Next

    '        FillEmployee()
    '        FillEmpInQueue()
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub ddlDesignation_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlDesignation.SelectedIndexChanged
        CompanyChanged()
        FillCardTypes(ddlDesignation.SelectedValue)
    End Sub
    Protected Sub ddlCardDesign_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlCardDesign.SelectedIndexChanged
        CompanyChanged()
    End Sub
    Protected Sub RadCmbBxCompanies_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbBxCompanies.SelectedIndexChanged
        CompanyChanged()

    End Sub

    Protected Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        PrintCard()
    End Sub

    Protected Sub lnkViewCard_Click(sender As Object, e As EventArgs) Handles lnkViewCard.Click

        Dim EmployeeIds As String = String.Empty
        For Each item As ListItem In cblEmpListInQueue.Items
            If item.Selected = True Then
                EmployeeIds += item.Value + ","
            End If
        Next

        If EmployeeIds = String.Empty Then
            For Each item As ListItem In cblEmpListInQueue.Items
                EmployeeIds += item.Value + ","
            Next
        End If

        If EmployeeIds = String.Empty Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SelectEmployeeCard", CultureInfo), "info")
            Return
        End If

        If ddlCardDesign.SelectedValue = -1 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SelectCardTemplate", CultureInfo), "info")
            Return
        End If

        Dim secururl As SecureUrl = Nothing

        objCardType = New CardTypes
        objCardType.CardTypeId = ddlCardDesign.SelectedValue
        objCardType.GetByPK()
        Dim QueryString As String = "../Reports/EmployeeCards_ReportViewer.aspx?EmployeeIds=" & EmployeeIds & "&TemplateId=" & objCardType.Fk_TemplateId
        secururl = New SecureUrl(QueryString)
        Dim newWin As String = (Convert.ToString("window.open('") & secururl.ToString) + "', 'popup_window', 'width=700px,height=530px,left=400,top=100,resizable=yes,scrollbars=1,status=yes,#toolbar=0,location=0,status=0,menubar=0');"
        ClientScript.RegisterStartupScript(Me.GetType(), "pop", newWin, True)
    End Sub

#End Region

#Region "Methods"

    Public Sub FillEmployee()
        If PageNo = 0 Then
            PageNo = 1
        End If
        Repeater1.Visible = False

        cblEmpList.Items.Clear()
        cblEmpList.Text = String.Empty

        If RadCmbBxCompanies.SelectedIndex <> 0 And ddlCardDesign.SelectedValue <> Nothing Then
            Dim objEmployee As New Employee
            objEmployee.FK_CompanyId = RadCmbBxCompanies.SelectedValue
            objEmployee.FK_Designation = ddlDesignation.SelectedValue
            objEmployee.Fk_CardType = ddlCardDesign.SelectedValue
            'If (Not MultiEmployeeFilterUC.EntityID = 0) AndAlso MultiEmployeeFilterUC.SearchType = "1" Then
            '    objEmployee.FK_EntityId = MultiEmployeeFilterUC.EntityID
            'Else
            '    objEmployee.FK_EntityId = 0
            'End If

            'If (Not MultiEmployeeFilterUC.WorkGroupID = 0) AndAlso MultiEmployeeFilterUC.SearchType = "2" Then
            '    objEmployee.FK_LogicalGroup = MultiEmployeeFilterUC.WorkGroupID
            '    objEmployee.FilterType = "L"
            'Else
            '    objEmployee.FK_LogicalGroup = -1
            'End If

            'If (Not MultiEmployeeFilterUC.WorkLocationsID) AndAlso MultiEmployeeFilterUC.SearchType = "3" Then
            '    objEmployee.FK_WorkLocation = MultiEmployeeFilterUC.WorkLocationsID
            '    objEmployee.FilterType = "W"
            'Else
            '    objEmployee.FK_WorkLocation = -1
            'End If

            objEmployee.Get_EmployeeRecordCount()
            Emp_RecordCount = objEmployee.Emp_RecordCount
            'Dim dt As DataTable = objEmployee.GetEmpByCompany
            objEmployee.PrintStatus = False
            Dim dt As DataTable
            If EmpId = 0 Then
                objEmployee.PageNo = PageNo
                objEmployee.PageSize = 1000
                dt = objEmployee.GetEmpByCompanyDesig_ByPrintQueue
                'GetEmpByCompany_ByPrintQueue
            Else
                objEmployee.EmployeeId = EmpId
                dt = objEmployee.GetByEmpId()
            End If


            ' fill pager 
            'Dim pagefrom, pageto As Integer
            If Emp_RecordCount > 1000 And EmpId = 0 Then


                Dim dtpaging As New DataTable
                dtpaging.Columns.Add("pageNo")
                Dim index As Integer
                Dim empcount As Integer
                'empcount = dt.Rows.Count
                empcount = Emp_RecordCount

                For index = 0 To empcount Step 1000
                    Dim drpaging As DataRow
                    drpaging = dtpaging.NewRow
                    Dim dcCell3 As New DataColumn
                    dcCell3.ColumnName = "PageNo"
                    drpaging(0) = index / 1000 + 1
                    dtpaging.Rows.Add(drpaging)

                Next
                Repeater1.DataSource = dtpaging
                Repeater1.DataBind()
                For Each item2 As RepeaterItem In Repeater1.Items
                    Dim lnk As LinkButton = DirectCast(item2.FindControl("LinkButton1"), LinkButton)
                    If lnk.Text = PageNo Then
                        lnk.Attributes.Add("style", "color:Blue;font-weight:bold;text-decoration: underline")
                    End If
                Next

                'pagefrom = ((PageNo - 1) * 1000) + 1
                'pageto = PageNo * 1000
                If dt.Rows.Count > 0 Then
                    Repeater1.Visible = True
                End If
            Else
                Repeater1.Visible = False
            End If
            If EmpId > 0 Then
                cblEmpList.DataSource = Nothing
                Repeater1.DataSource = Nothing
                Repeater1.DataBind()
            End If
            ' end fill pager

            If (dt IsNot Nothing) Then
                Dim dtEmployees = dt
                If (dtEmployees IsNot Nothing) Then
                    If (dtEmployees.Rows.Count > 0) Then
                        Dim dtSource As New DataTable
                        'dtSource.Columns.Add("EmployeeId")
                        dtSource.Columns.Add("CardRequestId")
                        dtSource.Columns.Add("EmployeeName")
                        Dim drRow As DataRow
                        drRow = dtSource.NewRow()
                        For Item As Integer = 0 To dtEmployees.Rows.Count - 1
                            'If Item + 1 >= pagefrom And Item + 1 <= pageto Then


                            Dim drSource As DataRow
                            drSource = dtSource.NewRow
                            Dim dcCell1 As New DataColumn
                            Dim dcCell2 As New DataColumn
                            'dcCell1.ColumnName = "EmployeeId"
                            dcCell1.ColumnName = "CardRequestId"
                            dcCell2.ColumnName = "EmployeeName"
                            dcCell1.DefaultValue = dtEmployees.Rows(Item)(0)
                            dcCell2.DefaultValue = dtEmployees.Rows(Item)(3) + "-" + IIf(Lang = CtlCommon.Lang.EN, dtEmployees.Rows(Item)(1), dtEmployees.Rows(Item)(2)) + "-" + IIf(Lang = CtlCommon.Lang.EN, dtEmployees.Rows(Item)(5), dtEmployees.Rows(Item)(6))
                            drSource("CardRequestId") = dcCell1.DefaultValue
                            drSource("EmployeeName") = dcCell2.DefaultValue
                            dtSource.Rows.Add(drSource)

                            'ElseIf MultiEmployeeFilterUC.EmployeeID > 0 Then

                            '    Dim drSource As DataRow
                            '    drSource = dtSource.NewRow
                            '    Dim dcCell1 As New DataColumn
                            '    Dim dcCell2 As New DataColumn
                            '    dcCell1.ColumnName = "EmployeeId"
                            '    dcCell2.ColumnName = "EmployeeName"
                            '    dcCell1.DefaultValue = dtEmployees.Rows(Item)(0)
                            '    dcCell2.DefaultValue = dtEmployees.Rows(Item)(3) + "-" + IIf(Lang = CtlCommon.Lang.EN, dtEmployees.Rows(Item)(1), dtEmployees.Rows(Item)(2))
                            '    drSource("EmployeeId") = dcCell1.DefaultValue
                            '    drSource("EmployeeName") = dcCell2.DefaultValue
                            '    dtSource.Rows.Add(drSource)

                            'Else
                            '    Dim drSource As DataRow
                            '    drSource = dtSource.NewRow
                            '    Dim dcCell1 As New DataColumn
                            '    Dim dcCell2 As New DataColumn
                            '    dcCell1.ColumnName = "EmployeeId"
                            '    dcCell2.ColumnName = "EmployeeName"
                            '    dcCell1.DefaultValue = dtEmployees.Rows(Item)(0)
                            '    dcCell2.DefaultValue = dtEmployees.Rows(Item)(3) + "-" + IIf(Lang = CtlCommon.Lang.EN, dtEmployees.Rows(Item)(1), dtEmployees.Rows(Item)(2))
                            '    drSource("EmployeeId") = dcCell1.DefaultValue
                            '    drSource("EmployeeName") = dcCell2.DefaultValue
                            '    dtSource.Rows.Add(drSource)

                            'End If
                        Next
                        Dim dv As New DataView(dtSource)
                        If SessionVariables.CultureInfo = "ar-JO" Then
                            'dv.Sort = "EmployeeName"
                            For Each row As DataRowView In dv
                                'If (Not EmpId = 0) Then
                                'If EmpId = row("EmployeeId") Then
                                '  cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("CardRequestId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
                                '  Exit For
                                '  End If
                                'Else
                                cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("CardRequestId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
                                'End If
                            Next
                        Else
                            For Each row As DataRowView In dv
                                'If (Not EmpId = 0) Then
                                '    If EmpId = row("EmployeeId") Then
                                '        cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
                                '        Exit For
                                '    End If
                                'Else
                                cblEmpList.Items.Add(New ListItem(row("EmployeeName").ToString(), row("CardRequestId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
                                'End If
                            Next
                        End If
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub FillEmpInQueue()
        Try
            cblEmpListInQueue.Items.Clear()
            cblEmpListInQueue.Text = String.Empty
            hlViewEmployee.Visible = False

            If RadCmbBxCompanies.SelectedIndex <> 0 Then
                Dim objEmployee As New Employee
                objEmployee.FK_CompanyId = RadCmbBxCompanies.SelectedValue
                objEmployee.FK_Designation = ddlDesignation.SelectedValue
                objEmployee.Fk_CardType = ddlCardDesign.SelectedValue
                'objEmployee.FK_CompanyId = MultiEmployeeFilterUC.CompanyID
                'If (MultiEmployeeFilterUC.EntityID <> 0) Then
                '    objEmployee.FK_EntityId = MultiEmployeeFilterUC.EntityID
                'Else
                '    objEmployee.FK_EntityId = -1
                'End If

                objEmployee.PrintStatus = True
                Dim dt As DataTable = objEmployee.GetEmpByCompanyDesig_ByPrintQueue
                If (dt IsNot Nothing) Then
                    Dim dtEmployees = dt
                    If (dtEmployees.Rows.Count > 0) Then
                        Dim dtSource As New DataTable
                        dtSource.Columns.Add("EmployeeId")
                        dtSource.Columns.Add("EmployeeName")
                        Dim drRow As DataRow
                        drRow = dtSource.NewRow()
                        For Item As Integer = 0 To dtEmployees.Rows.Count - 1
                            Dim drSource As DataRow
                            drSource = dtSource.NewRow
                            Dim dcCell1 As New DataColumn
                            Dim dcCell2 As New DataColumn
                            dcCell1.ColumnName = "EmployeeId"
                            dcCell2.ColumnName = "EmployeeName"
                            dcCell1.DefaultValue = dtEmployees.Rows(Item)(0)
                            dcCell2.DefaultValue = dtEmployees.Rows(Item)(3) + "-" + IIf(Lang = CtlCommon.Lang.EN, dtEmployees.Rows(Item)(1), dtEmployees.Rows(Item)(2)) + "-" + IIf(Lang = CtlCommon.Lang.EN, dtEmployees.Rows(Item)(5), dtEmployees.Rows(Item)(6))
                            'dcCell2.DefaultValue = dtEmployees.Rows(Item)(3) + "-" + IIf(Lang = CtlCommon.Lang.EN, dtEmployees.Rows(Item)(1), dtEmployees.Rows(Item)(2))
                            drSource("EmployeeId") = dcCell1.DefaultValue
                            drSource("EmployeeName") = dcCell2.DefaultValue
                            dtSource.Rows.Add(drSource)
                        Next

                        Dim dv As New DataView(dtSource)
                        If SessionVariables.CultureInfo = "ar-JO" Then
                            'dv.Sort = "EmployeeArabicName ASC"
                            For Each row As DataRowView In dv
                                cblEmpListInQueue.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
                            Next
                        Else
                            'dv.Sort = "EmployeeName ASC"
                            For Each row As DataRowView In dv
                                cblEmpListInQueue.Items.Add(New ListItem(row("EmployeeName").ToString(), row("EmployeeId").ToString())) ', row("EmployeeNo").ToString(), "View All Employee Schedules"))) '<a href=javascript:open_window('../Reports/ReportViewer.aspx?id={0}','',700,400) >{1}</a>"
                            Next
                        End If
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ClearAll()
        ' Clear the controls
        'MultiEmployeeFilterUC.ClearValues()
        cblEmpList.Items.Clear()
        cblEmpListInQueue.Items.Clear()


    End Sub

    'Public Sub CompanyChanged()
    '    EmployeeFilterUC.FillEntity()
    '    FillEmployee()
    '    FillEmpInQueue()
    'End Sub

    Private Sub FillCompany()
        Dim dt As DataTable
        objOrgCompany = New OrgCompany
        dt = objOrgCompany.GetAllforddl
        CtlCommon.FillTelerikDropDownList(RadCmbBxCompanies, dt, Lang)
    End Sub

    Public Sub CompanyChanged()
        'EmployeeFilterUC.FillEntity()
        'MultiEmployeeFilterUC.CompanyID = RadCmbBxCompanies.SelectedValue
        If Not RadCmbBxCompanies.SelectedValue = -1 Then


            'MultiEmployeeFilterUC.FillList()
            objAPP_Settings = New APP_Settings
            With objAPP_Settings
                .GetByPK()
                If .FillCheckBoxList = 1 Then
                    FillEmployee()
                End If
            End With
            FillEmpInQueue()
        Else
            ClearAll()
            'MultiEmployeeFilterUC.ClearValues()
        End If

    End Sub

    Public Sub EntityChanged()

        FillEmployee()

    End Sub

    Public Sub WorkGroupChanged()

        FillEmployee()

    End Sub

    Public Sub WorkLocationsChanged()

        FillEmployee()

    End Sub

    Protected Sub Repeater1_ItemCommand(source As Object, e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles Repeater1.ItemCommand

        Dim page As Integer = CInt(CType(e.CommandSource, LinkButton).Text)
        PageNo = page
        FillEmployee()

    End Sub

    Private Sub FillPrinter()
        Dim dtSource As New DataTable
        Dim drRow As DataRow
        drRow = dtSource.NewRow()
        dtSource.Columns.Add("PrinterId")
        dtSource.Columns.Add("PrinterName")
        For i As Integer = 0 To PrinterSettings.InstalledPrinters.Count - 1

            Dim drSource As DataRow
            drSource = dtSource.NewRow
            Dim dcCell1 As New DataColumn
            Dim dcCell2 As New DataColumn
            dcCell1.ColumnName = "PrinterId"
            dcCell2.ColumnName = "PrinterName"
            dcCell2.DefaultValue = PrinterSettings.InstalledPrinters(i)
            dcCell2.DefaultValue = PrinterSettings.InstalledPrinters(i)
            drSource("PrinterId") = PrinterSettings.InstalledPrinters(i)
            drSource("PrinterName") = PrinterSettings.InstalledPrinters(i)
            dtSource.Rows.Add(drSource)
        Next
        CtlCommon.FillTelerikDropDownList(rcmbxPrinter, dtSource, Lang)
    End Sub

    Private Sub PrintCard()
        objReport = New Report
        Dim dt As DataTable
        Dim QRString As String
        Dim qrGenerator As New QRCodeGenerator()


        objCards = New Emp_Cards
        objReportDocument = New ReportDocument
        objCard_Template = New Card_Template
        objCardRequest = New Card_Request
        objCardType = New CardTypes

        objCardRequest.Status = 15
        objCardRequest.CardType = ddlCardDesign.SelectedValue


        Dim EmployeeIds As String = String.Empty
        For Each item As ListItem In cblEmpListInQueue.Items
            If item.Selected = True Then
                EmployeeIds += item.Value + ","
                objCardRequest.FK_EmployeeId = item.Value
                'objCardRequest.UpdatePrintStatus()
            End If
        Next

        If Not EmployeeIds = String.Empty Then
            objReport.EmployeeIds = EmployeeIds
            dt = objReport.Get_CardTemplate
            For Each row As DataRow In dt.Rows
                QRString = row.Item("EmployeeName") & row.Item("DesignationName") & row.Item("CompanyName") & row.Item("Email") & row.Item("PhoneNumber")
                '''''''''Convert to QRCode'''''''''''''''''''''''''''''''
                Dim qrCodeData As QRCodeData = qrGenerator.CreateQrCode(QRString, QRCodeGenerator.ECCLevel.Q)
                Dim qrCode As QRCode = New QRCode(qrCodeData)
                Dim qrCodeImage As Bitmap = qrCode.GetGraphic(20)
                Dim qrImg As Byte() = ImageToByte(qrCodeImage)
                ''''''''''''Assign QRCode''''''''''''''''''''''''''''''
                row.Item("QRCODE") = qrImg
            Next row
            objCardType.CardTypeId = ddlCardDesign.SelectedValue
            objCardType.GetByPK()

            objCard_Template.TemplateId = objCardType.Fk_TemplateId
            objCard_Template.GetByPK()
            RPTName = objCard_Template.TemplateFilePath

            'RPTName = "rptCardTemplate.rpt"
            'If (Lang = CtlCommon.Lang.AR) Then
            '    RPTName = "Arabic/" + RPTName
            'Else
            '    RPTName = "English/" + RPTName
            'End If

            RPTName = (RPTName)
            RPTName = Replace(RPTName, "Employee", "Reports")
            objReportDocument.Load(RPTName)
            objReportDocument.SetDataSource(dt) 'mahak

            Dim PSettings As PrinterSettings = New PrinterSettings
            PSettings.DefaultPageSettings.Landscape = False
            If Not rcmbxPrinter.SelectedValue = "-1" Then
                PSettings.PrinterName = rcmbxPrinter.SelectedValue
            End If

            PSettings.DefaultPageSettings.PrinterSettings.Duplex = Duplex.Horizontal
            Dim PgSet As PageSettings = New PageSettings
            If String.IsNullOrEmpty(PSettings.PaperSizes(0).PaperName) Then
                PgSet.PaperSize = New PaperSize("Custom 220 x 344", 220, 344)
            Else
                PgSet.PaperSize = PSettings.PaperSizes(0)
            End If
            Dim PrintLayout As CrystalDecisions.Shared.PrintLayoutSettings = New CrystalDecisions.Shared.PrintLayoutSettings
            objReportDocument.PrintToPrinter(PSettings, PgSet, False, PrintLayout)
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SendToPrinter", CultureInfo), "success")

            For Each item As ListItem In cblEmpListInQueue.Items
                If item.Selected = True Then
                    objCards.EmpId = item.Value
                    objCards.DeleteFromQueue()
                End If
            Next

            FillEmployee()
            FillEmpInQueue()
            Clear()
        End If

    End Sub
    Public Shared Function ImageToByte(ByVal img As Bitmap) As Byte()
        Dim converter As ImageConverter = New ImageConverter()
        Return CType(converter.ConvertTo(img, GetType(Byte())), Byte())
    End Function
    Private Sub Clear()
        rcmbxPrinter.SelectedValue = -1
    End Sub

    Private Sub FillTemplates()
        objCard_Template = New Card_Template
        With objCard_Template
            CtlCommon.FillTelerikDropDownList(ddlCardDesign, .GetAll, Lang)
        End With
    End Sub
    Private Sub FillCardTypes(ByVal Fk_designation As Integer)
        objCardType = New CardTypes
        With objCardType
            CtlCommon.FillTelerikDropDownList(ddlCardDesign, .GetAllByDesignation(Fk_designation), Lang)
        End With
    End Sub
    Private Sub FillDesignations()
        objEmp_Designation = New Emp_Designation
        With objEmp_Designation
            CtlCommon.FillTelerikDropDownList(ddlDesignation, .GetAll, Lang)
        End With



    End Sub


#End Region


End Class
