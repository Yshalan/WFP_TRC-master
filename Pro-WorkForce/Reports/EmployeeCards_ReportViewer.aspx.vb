Imports TA.Reports
Imports System.Data
Imports SmartV.UTILITIES
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports TA.Employees
Imports System.Net
Imports QRCoder
Imports System.Drawing

Partial Class Reports_EmployeeCards_ReportViewer
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Protected dir As String
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim cryRpt As New ReportDocument
    Private objCard_Template As Card_Template

#End Region

#Region "Public Properties"

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

    Public Property DT() As DataTable
        Get
            Return ViewState("DT")
        End Get
        Set(ByVal value As DataTable)
            ViewState("DT") = value
        End Set
    End Property

    Public Property TemplateId() As String
        Get
            Return ViewState("TemplateId")
        End Get
        Set(ByVal value As String)
            ViewState("TemplateId") = value
        End Set
    End Property
#End Region

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
        Else
            Lang = CtlCommon.Lang.EN
        End If

        If Not Page.IsPostBack Then
            BindReport()
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        'cryRpt.Close()
        'cryRpt.Dispose()
    End Sub

#End Region

#Region "Page Methods"

    Public Sub BindReport()
        SetReportName()
        cryRpt = New ReportDocument
        cryRpt.Load(RPTName)
        cryRpt.SetDataSource(DT)

        cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, False, "ExportedReport")

    End Sub

    Private Sub SetReportName()
        Dim QRString As String
        Dim rptObj As New Report
        Dim SecurUrl As SecureUrl = New SecureUrl(Request.Url.PathAndQuery)
        objCard_Template = New Card_Template
        Dim qrGenerator As New QRCodeGenerator()

        EmployeeIds = SecurUrl("EmployeeIds")
        TemplateId = SecurUrl("TemplateId")
        rptObj.EmployeeIds = EmployeeIds

        objCard_Template.TemplateId = TemplateId
        objCard_Template.GetByPK()
        RPTName = objCard_Template.TemplateFilePath
        'RPTName = "rptCardTemplate.rpt"
        DT = rptObj.Get_CardTemplate()
        For Each row As DataRow In DT.Rows
            QRString = row.Item("EmployeeName") & row.Item("DesignationName") & row.Item("CompanyName") & row.Item("Email") & row.Item("PhoneNumber")
            '''''''''Convert to QRCode'''''''''''''''''''''''''''''''
            Dim qrCodeData As QRCodeData = qrGenerator.CreateQrCode(QRString, QRCodeGenerator.ECCLevel.Q)
            Dim qrCode As QRCode = New QRCode(qrCodeData)
            Dim qrCodeImage As Bitmap = qrCode.GetGraphic(20)
            Dim qrImg As Byte() = ImageToByte(qrCodeImage)
            ''''''''''''Assign QRCode''''''''''''''''''''''''''''''
            row.Item("QRCODE") = qrImg
        Next row
        'If (Lang = CtlCommon.Lang.AR) Then
        '    RPTName = "Arabic/" + RPTName
        'Else
        '    RPTName = "English/" + RPTName
        'End If
    End Sub
    Public Shared Function ImageToByte(ByVal img As Bitmap) As Byte()
        Dim converter As ImageConverter = New ImageConverter()
        Return CType(converter.ConvertTo(img, GetType(Byte())), Byte())
    End Function
#End Region


End Class
