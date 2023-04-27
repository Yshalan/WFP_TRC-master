
Imports Telerik.Web.UI
Imports SmartV.UTILITIES
Imports TA.Card_Request
Imports System.Data
Imports TA.Security
Imports System.Resources
Imports System.Globalization
Imports SmartV.DB
Imports System.Data.SqlClient
Imports TA.Reports.Report
Imports QRCoder
Imports TA.Employees
Imports CrystalDecisions.CrystalReports.Engine
Imports TA.Admin
Imports TA.Reports
Imports System.Drawing
Imports CrystalDecisions.Shared

Partial Class Employee_CardHistory
    Inherits System.Web.UI.Page

    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Protected objDac As SmartV.DB.DAC = Nothing
    Private Get_CardStatus As String = "Get_CardStatus"
    Private Get_AllCardRequests As String = "Get_AllCardRequests"
    Private GetRecordByStatus_Employee As String = "GetRecordByStatus_Employee"

    Protected errNo As Integer = -1
    Private StatusId As Integer = 0
    Protected Emp_No As String = String.Empty
    Private objReport As Report
    Dim objCards As Emp_Cards
    Protected logPath As String = String.Empty
    Dim objReportDocument As New ReportDocument
    Private objCard_Template As Card_Template
    Private objEmp_Designation As Emp_Designation
    Private objCardType As CardTypes
    Private objCardRequest As Card_Request
    Public Property Status() As Integer
        Get
            Return ViewState("Status")
        End Get
        Set(ByVal value As Integer)
            ViewState("Status") = value
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

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If

            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            PageHeader1.HeaderText = ResourceManager.GetString("CardHistory", CultureInfo)
            FillCardStatus()
            FillGrid()

        End If

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms

    End Sub


    Private Sub FillCardStatus()

        CtlCommon.FillTelerikDropDownList(ddlRadCmbBxStatus, GetAllStatus(), Lang)

    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub
    Private Sub FillGrid()

        If ddlRadCmbBxStatus.SelectedValue = -1 Then
            dtCurrent = GetRecordsByStatus_Employee(0, Nothing)
            dgrdCardRequests.DataSource = dtCurrent
            dgrdCardRequests.DataBind()

        End If

    End Sub

    Public Function GetAllStatus() As DataTable

        objDac = DAC.getDAC()
        Dim objColl As DataTable
        Try
            objColl = objDac.GetDataTable(Get_CardStatus, Nothing)
        Catch ex As Exception
            errNo = -11
            CtlCommon.CreateErrorLog(logPath, ex.Message, Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
        Return objColl


    End Function

    Public Function GetAll_Records() As DataTable

        objDac = DAC.getDAC()
        Dim objColl As DataTable
        Try
            objColl = objDac.GetDataTable(GetRecordByStatus_Employee, Nothing)
        Catch ex As Exception
            errNo = -11
            CtlCommon.CreateErrorLog(logPath, ex.Message, Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
        Return objColl


    End Function

    Public Function GetRecordsByStatus_Employee(ByVal EmployeeNo As String, ByVal Status As Integer) As DataTable

        objDac = DAC.getDAC()
        Dim objColl As DataTable
        Try
            objColl = objDac.GetDataTable(GetRecordByStatus_Employee, New SqlParameter("@Status", Status), New SqlParameter("@Emp_No", EmployeeNo))
        Catch ex As Exception
            errNo = -11
            CtlCommon.CreateErrorLog(logPath, ex.Message, Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
        Return objColl


    End Function

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchEmployee.Click

        If Not ddlRadCmbBxStatus.SelectedValue = -1 Or Not String.IsNullOrEmpty(TxtEmpNo.Text) Then

            If ddlRadCmbBxStatus.SelectedValue = -1 Then
                StatusId = 0
            Else
                StatusId = Convert.ToInt16(ddlRadCmbBxStatus.SelectedValue)
            End If

            Emp_No = TxtEmpNo.Text

            dtCurrent = GetRecordsByStatus_Employee(Emp_No, StatusId)

            If Not dtCurrent Is Nothing Then
                dgrdCardRequests.DataSource = dtCurrent
                dgrdCardRequests.DataBind()

            End If

        Else
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("PleaseSelectStatus", CultureInfo), "error")

        End If



        'FillGrid()

        'CtlCommon.ShowMessage(Page, ResourceManager.GetString("Please Select Status", CultureInfo), "error")


        'FillFilterControls()
    End Sub

    Protected Sub dgrdCardRequests_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles dgrdCardRequests.SelectedIndexChanged
        'FillControls()
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
                        strReason = "قيد الاعتماد"
                    Case 14
                        strReason = "قيد الطباعة"
                    Case 15
                        strReason = "تمت الطباعة"
                    Case 4
                        strReason = "مرفوض"
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
                        strStatus = "Rejected by Manager"
                    Case 5
                        strStatus = "Rejected by HR"
                    Case 7
                        strStatus = "Rejected by GM"
                    Case 2
                        strStatus = "Approved by Manager"
                    Case 3
                        strStatus = "Approved by HR"
                    Case 6
                        strStatus = "Approved by GM"
                    Case 10
                        strStatus = "Deleted"
                    Case 9
                        strStatus = "Approved by 1st Manager"

                End Select
            End If

            item("Status").Text = strStatus
            item("ReasonId").Text = strReason


            If Lang = CtlCommon.Lang.AR Then
                item("EmployeeName").Text = DirectCast(item.FindControl("hdnEmployeeNameAr"), HiddenField).Value
            End If

        End If
    End Sub


    Protected Sub ExportCard(ByVal EmployeeId As String, ByVal CardTypeId As Integer)
        objReport = New Report
        Dim dt As DataTable
        Dim QRString As String
        Dim qrGenerator As New QRCodeGenerator()

        objCards = New Emp_Cards
        objReportDocument = New ReportDocument
        objCard_Template = New Card_Template
        objCardRequest = New Card_Request
        objCardType = New CardTypes

        If Not EmployeeId = String.Empty Then
            objReport.EmployeeIds = EmployeeId
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


    Protected Sub btnLnk_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim FK_EmployeeId As Integer
        Dim CardTypeId As Integer

        FK_EmployeeId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("FK_EmployeeId").ToString())
        CardTypeId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("CardTypeId").ToString())

        ExportCard(FK_EmployeeId, CardTypeId)
    End Sub

    Protected Sub dgrdCardRequests_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs)

        dgrdCardRequests.DataSource = dtCurrent

    End Sub

End Class