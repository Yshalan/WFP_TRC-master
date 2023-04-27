Imports SmartV.UTILITIES
Imports TA.Admin.OrgCompany
Imports TA.Lookup
Imports System.Data
Imports TA.Employees
Imports TA.Definitions
Imports TA.Security
Imports TA.Admin
Imports SmartV.Version
Imports TA.Card_Request
Imports QRCoder
Imports CrystalDecisions.CrystalReports.Engine
Imports TA.Reports
Imports System.Drawing
Imports CrystalDecisions.Shared

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

    Private objCard_Request As Card_Request
    Private objReport As Report
    Dim objReportDocument As New ReportDocument
    Private objCard_Template As Card_Template
    Private objCardType As CardTypes
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
    Public Property ReasonId() As Integer
        Get
            Return ViewState("ReasonId")
        End Get
        Set(ByVal value As Integer)
            ViewState("ReasonId") = value
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
                'cmdPrint1.Value = "طباعة البطاقات"
            Else
                Lang = CtlCommon.Lang.EN
                'cmdPrint1.Value = "Print Cards"
            End If

            If (objVersion.HasMultiCompany() = False) Then
                'RadCmbBxCompanies.SelectedValue = objVersion.GetCompanyId()
                'RadCmbBxCompanies_SelectedIndexChanged(Nothing, Nothing)
                'trcompany.Visible = False
            End If

            FillStatus()
        End If

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        'For Each row As DataRow In dtCurrent.Rows
        '    If Not row("AllowAdd") = 1 Then
        '        If Not IsDBNull(row("AddBtnName")) Then
        '            If Not trControls.FindControl(row("AddBtnName")) Is Nothing Then
        '                trControls.FindControl(row("AddBtnName")).Visible = False
        '            End If
        '        End If
        '    End If

        '    If Not row("AllowDelete") = 1 Then
        '        If Not IsDBNull(row("DeleteBtnName")) Then
        '            If Not trControls.FindControl(row("DeleteBtnName")) Is Nothing Then
        '                trControls.FindControl(row("DeleteBtnName")).Visible = False
        '            End If
        '        End If
        '    End If

        '    If Not row("AllowSave") = 1 Then
        '        If Not IsDBNull(row("EditBtnName")) Then
        '            If Not trControls.FindControl(row("EditBtnName")) Is Nothing Then
        '                trControls.FindControl(row("EditBtnName")).Visible = False
        '            End If
        '        End If
        '    End If

        '    If Not row("AllowPrint") = 1 Then
        '        If Not IsDBNull(row("PrintBtnName")) Then
        '            If Not trControls.FindControl(row("PrintBtnName")) Is Nothing Then
        '                trControls.FindControl(row("PrintBtnName")).Visible = False
        '            End If
        '        End If
        '    End If
        'Next

    End Sub
    Protected Sub dgrdCardRequests_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles dgrdCardRequests.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim item As GridDataItem
            Dim strStatus As String
            Dim strReason As String
            item = e.Item

            If Lang = CtlCommon.Lang.AR Then
                Select Case item.GetDataKeyValue("ReasonId")
                    Case 1
                        strReason = "جديد"
                    Case 2
                        strReason = "تجديد"
                    Case 3
                        strReason = "بدل تالف"
                    Case 4
                        strReason = "بدل فاقد"
                    Case 5
                        strReason = "تحديث بيانات"
                    Case 6
                        strReason = "اخرى"
                End Select
            Else

                Select Case item.GetDataKeyValue("ReasonId")
                    Case 1
                        strReason = "New"
                    Case 2
                        strReason = "Renew"
                    Case 3
                        strReason = "Damaged"
                    Case 4
                        strReason = "Replacement"
                    Case 5
                        strReason = "Update Information"
                    Case 6
                        strReason = "Other"
                End Select
            End If

            If Not item("ReasonId").Text = -1 Then
                item("ReasonId").Text = strReason
            End If

            If Lang = CtlCommon.Lang.AR Then
                Select Case item.GetDataKeyValue("Status")
                    Case 1
                        strStatus = "قيد الاعتماد"
                    Case 14
                        strStatus = "قيد الطباعة"
                    Case 15
                        strStatus = "تمت الطباعة"
                    Case 4
                        strStatus = "مرفوض"
                End Select
            Else

                Select Case item.GetDataKeyValue("Status")
                    Case 1
                        strStatus = "Pending"
                    Case 14
                        strStatus = "UnderPrinting"
                    Case 15
                        strStatus = "Printed"
                    Case 4
                        strStatus = "Rejected"
                End Select
            End If

            item("Status").Text = strStatus

            If Lang = CtlCommon.Lang.AR Then
                item("EmployeeName").Text = DirectCast(item.FindControl("hdnEmployeeNameAr"), HiddenField).Value
            End If

            'If Not ddlStatus.SelectedValue = Convert.ToInt32(CardRequestStatus.Pending).ToString Then
            '    If Not ddlStatus.SelectedValue = -1 Then
            '        dgrdCardRequests.Columns(6).Visible = False
            '        dgrdCardRequests.Columns(7).Visible = False
            '    Else
            '        dgrdCardRequests.Columns(6).Visible = True
            '        dgrdCardRequests.Columns(7).Visible = True
            '    End If
            'End If
        End If
    End Sub
    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdCardRequests.Skin))
    End Function
    Protected Sub lnkAccept_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim FK_EmployeeID As Integer
        Dim CardTypeId As Integer

        FK_EmployeeID = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FK_EmployeeId").ToString())
        CardTypeId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("CardTypeId").ToString())

        PrintCard(FK_EmployeeID, CardTypeId)
    End Sub
    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub
    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlStatus.SelectedIndexChanged
        FillGrid()
    End Sub

#End Region

#Region "Methods"


    Private Sub FillStatus()
        If SessionVariables.CultureInfo = "ar-JO" Then
            Dim item1 As New RadComboBoxItem
            Dim item2 As New RadComboBoxItem
            Dim item3 As New RadComboBoxItem
            Dim item4 As New RadComboBoxItem
            Dim item5 As New RadComboBoxItem
            Dim item6 As New RadComboBoxItem

            item1.Value = -1
            item1.Text = "--الرجاء الاختيار--"
            ddlStatus.Items.Add(item1)
            item2.Value = 1
            item2.Text = "قيد الدراسة"
            ddlStatus.Items.Add(item2)
            item3.Value = 2
            item3.Text = "موافقة من قبل المدير"
            ddlStatus.Items.Add(item3)
            item4.Value = 14
            item4.Text = "تحت الطباعة"
            ddlStatus.Items.Add(item4)

            item5.Value = 15
            item5.Text = "مطبوعة"
            ddlStatus.Items.Add(item5)

            item6.Value = 4
            item6.Text = "مرفوض من قبل المدير"
            ddlStatus.Items.Add(item6)

        Else
            Dim item1 As New RadComboBoxItem
            Dim item2 As New RadComboBoxItem
            Dim item3 As New RadComboBoxItem
            Dim item4 As New RadComboBoxItem
            Dim item5 As New RadComboBoxItem
            Dim item6 As New RadComboBoxItem

            item1.Value = -1
            item1.Text = "--Please Select--"
            ddlStatus.Items.Add(item1)
            item2.Value = 1
            item2.Text = "Pending"
            ddlStatus.Items.Add(item2)
            item3.Value = 2
            item3.Text = "Approved By Manager"
            ddlStatus.Items.Add(item3)
            item4.Value = 14
            item4.Text = "Under Printing"
            ddlStatus.Items.Add(item4)

            item5.Value = 15
            item5.Text = "Printed"
            ddlStatus.Items.Add(item5)

            item6.Value = 4
            item6.Text = "Rejected by Manager"
            ddlStatus.Items.Add(item6)

        End If
        'ddlStatus.SelectedIndex = 1
    End Sub
    Private Sub FillGrid()
        objCard_Request = New Card_Request
        With objCard_Request
            .Status = ddlStatus.SelectedValue
            'ddlStatus.SelectedValue
            .FK_EmployeeId = MultiEmployeeFilterUC.EmployeeId
            dtCurrent = .GetAll_CardRequest_ByEmployee()
            '.GetAll_Inner()
        End With
        dgrdCardRequests.DataSource = dtCurrent
        dgrdCardRequests.DataBind()
    End Sub

    Private Sub ClearAll()
        ' Clear the controls
        MultiEmployeeFilterUC.ClearValues()
        'cblEmpList.Items.Clear()
        'cblEmpListInQueue.Items.Clear()
        'pnlEndDate.Visible = False

    End Sub
    Private Sub PrintCard(ByVal Fk_EmployeeID As Integer, ByVal CardTypeId As Integer)
        objReport = New Report
        Dim dt As DataTable
        Dim QRString As String
        Dim qrGenerator As New QRCodeGenerator()


        objCards = New Emp_Cards
        objReportDocument = New ReportDocument
        objCard_Template = New Card_Template
        objCard_Request = New Card_Request
        objCardType = New CardTypes

       

        Dim EmployeeIds As String = String.Empty
        EmployeeIds += Fk_EmployeeID.ToString() + ","

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

            objCardType.CardTypeId = CardTypeId
            objCardType.GetByPK()

            objCard_Template.TemplateId = objCardType.Fk_TemplateId
            objCard_Template.GetByPK()
            RPTName = objCard_Template.TemplateFilePath

            RPTName = (RPTName)
            RPTName = Replace(RPTName, "Employee", "Reports")
            objReportDocument.Load(RPTName)
            objReportDocument.SetDataSource(dt)
            objReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedReport")
           
        End If

    End Sub

    Public Shared Function ImageToByte(ByVal img As Bitmap) As Byte()
        Dim converter As ImageConverter = New ImageConverter()
        Return CType(converter.ConvertTo(img, GetType(Byte())), Byte())
    End Function

#End Region

End Class
