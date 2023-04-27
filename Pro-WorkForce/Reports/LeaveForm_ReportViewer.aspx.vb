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
Imports System.Web.Configuration

Partial Class Reports_ReportViewer
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objAPP_Settings As APP_Settings
    Private Lang As CtlCommon.Lang
    Protected dir As String
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Dim cryRpt As New ReportDocument
    Dim objSmartSecurity As New SmartSecurity
    Private objSYSUsers As SYSUsers
    Private objOrgCompany As OrgCompany
    Private objEmployee As Employee
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

    Public Property EmployeeId() As Integer
        Get
            Return ViewState("EmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeId") = value
        End Set
    End Property

    Public Property LeaveId() As Integer
        Get
            Return ViewState("LeaveId")
        End Get
        Set(ByVal value As Integer)
            ViewState("LeaveId") = value
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
        cryRpt.Close()
        cryRpt.Dispose()
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

    Public Sub BindReport()
        EmployeeId = Request.QueryString("EmployeeId")
        LeaveId = Request.QueryString("LeaveId")

        SetReportName()
      
        cryRpt = New ReportDocument
        cryRpt.Load(Server.MapPath(RPTName))
        cryRpt.SetDataSource(DT)
        Dim objAPP_Settings As New APP_Settings
        objEmployee = New Employee
        objEmployee.EmployeeId = EmployeeId
        objEmployee.GetByPK()
        objAPP_Settings.FK_CompanyId = objEmployee.FK_CompanyId
        dt2 = objAPP_Settings.GetHeader()
        dt2.Columns.Add("From_Date")
        dt2.Columns.Add("To_Date")
        cryRpt.Subreports(cryRpt.Subreports().Item(0).Name).SetDataSource(dt2)

        If (Lang = CtlCommon.Lang.AR) Then
            If Request.QueryString("Signature") = 1 Then
                cryRpt.SetParameterValue("@SignatureTitle", "توقيع الموظف")
                cryRpt.SetParameterValue("@ReportHeader", "طلب الاجازة", "rptHeader")
                cryRpt.SetParameterValue("@ReportName", "طلب الاجازة")
            ElseIf Request.QueryString("Signature") = 2 Then
                cryRpt.SetParameterValue("@SignatureTitle", "توقيع المدير")
                cryRpt.SetParameterValue("@ReportHeader", "موافقة المدير لطلب الاجازة", "rptHeader")
                cryRpt.SetParameterValue("@ReportName", "موافقة المدير لطلب الاجازة")
            ElseIf Request.QueryString("Signature") = 3 Then
                cryRpt.SetParameterValue("@SignatureTitle", "توقيع الموارد البشرية")
                cryRpt.SetParameterValue("@ReportHeader", "موافقة الموارد البشرية لطلب الاجازة", "rptHeader")
                cryRpt.SetParameterValue("@ReportName", "موافقة الموارد البشرية لطلب الاجازة")
            ElseIf Request.QueryString("Signature") = 4 Then
                cryRpt.SetParameterValue("@SignatureTitle", "توقيع المدير العام")
                cryRpt.SetParameterValue("@ReportHeader", "موافقة المدير العام لطلب الاجازة", "rptHeader")
                cryRpt.SetParameterValue("@ReportName", "موافقة المدير العام لطلب الاجازة")
            End If

        Else
            If Request.QueryString("Signature") = 1 Then
                cryRpt.SetParameterValue("@SignatureTitle", "Employee Signature")
                cryRpt.SetParameterValue("@ReportHeader", "Leave Form", "rptHeader")
                cryRpt.SetParameterValue("@ReportName", "Leave Form")
            ElseIf Request.QueryString("Signature") = 2 Then
                cryRpt.SetParameterValue("@SignatureTitle", "Manager Signature")
                cryRpt.SetParameterValue("@ReportHeader", "Direct Manager Leave Approval", "rptHeader")
                cryRpt.SetParameterValue("@ReportName", "Direct Manager Leave Approval")
            ElseIf Request.QueryString("Signature") = 3 Then
                cryRpt.SetParameterValue("@SignatureTitle", "Human Resource Signature")
                cryRpt.SetParameterValue("@ReportHeader", "Human Resource Leave Approval", "rptHeader")
                cryRpt.SetParameterValue("@ReportName", "Human Resource Leave Approval")
            ElseIf Request.QueryString("Signature") = 4 Then
                cryRpt.SetParameterValue("@SignatureTitle", "General Manager Signature")
                cryRpt.SetParameterValue("@ReportHeader", "General Manager Leave Approval", "rptHeader")
                cryRpt.SetParameterValue("@ReportName", "General Manager Leave Approval")
            End If
        End If

        Dim ShowSTLogo As Boolean
        ShowSTLogo = ConfigurationManager.AppSettings("ShowSmartTimeLogo")
        cryRpt.SetParameterValue("@ShowSTLogo", False)
        cryRpt.SetParameterValue("@ShowSTLogo", False, "rptHeader")

        cryRpt.SetParameterValue("@Manager", Request.QueryString("ManagerName"))
        cryRpt.SetParameterValue("@UserName", UserName)

        'Version = System.Configuration.ConfigurationManager.AppSettings("Version").ToString()
        Version = SmartV.Version.version.GetVersionNumber
        cryRpt.SetParameterValue("@Version", Version)
        If SessionVariables.CultureInfo = "ar-JO" Then
            CopyRightAr = objSmartSecurity.strSmartDecrypt(ConfigurationManager.AppSettings("CopyRightAr").ToString(), "SmartVision")
            cryRpt.SetParameterValue("@CopyRightAr", CopyRightAr)
        Else
            CopyRightEn = objSmartSecurity.strSmartDecrypt(ConfigurationManager.AppSettings("CopyRightEn").ToString(), "SmartVision")
            cryRpt.SetParameterValue("@CopyRightEn", CopyRightEn)

        End If
        Dim showSmartTimeLogo As Boolean = CBool(ConfigurationManager.AppSettings("ShowSmartTimeLogo"))
        If showSmartTimeLogo Then
            Dim rptObj As New Report
            Dim objUser As New Employee
            Dim objOrgCompany As New OrgCompany
            objUser.EmployeeId = EmployeeId
            objUser.GetByPK()
            objOrgCompany.CompanyId = objUser.FK_CompanyId
            objOrgCompany.GetByPK()
            If Not (objOrgCompany.CompanyId = 0 Or objOrgCompany.Logo Is Nothing) Then

                Dim ImagePath As String = "~/images/CompaniesLogo/" & objOrgCompany.CompanyId & objOrgCompany.Logo
                Dim filePath As String = HttpContext.Current.Server.MapPath(ImagePath)
                If System.IO.File.Exists(filePath) Then
                    cryRpt.SetParameterValue("@LogoURL", filePath, "rptHeader")
                Else
                    cryRpt.SetParameterValue("@LogoURL", "nologo", "rptHeader")
                End If
            Else
                cryRpt.SetParameterValue("@LogoURL", "nologo", "rptHeader")
            End If
        Else
            cryRpt.SetParameterValue("@LogoURL", "", "rptHeader")

        End If
        CRV.ReportSource = cryRpt
        cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, False, "ExportedReport")
    End Sub

    Private Sub SetReportName()
        Dim rptObj As New Report
        UserName = SessionVariables.LoginUser.UsrID
        rptObj.EmployeeId = EmployeeId
        rptObj.LeaveId = LeaveId
        RPTName = "LeaveForm.rpt"
        DT = rptObj.GetEmp_LeaveForm()
        If (Lang = CtlCommon.Lang.AR) Then
            RPTName = "Arabic/" + RPTName
        Else
            RPTName = "English/" + RPTName
        End If
    End Sub

#End Region

End Class

