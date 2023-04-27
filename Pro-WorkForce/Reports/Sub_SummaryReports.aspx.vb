Imports System.Data
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports SmartV.UTILITIES
Imports SmartV.UTILITIES.CtlCommon
Imports TA.Security
Imports TA.Admin
Imports TA.Reports

Partial Class Reports_Sub_SummaryReports
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objAPP_Settings As APP_Settings
    Private Lang As CtlCommon.Lang
    Protected dir As String
    Private objsysforms As New SYSForms
    Dim cryRpt As New ReportDocument
    Dim objSmartSecurity As New SmartSecurity
    Private objSYSUsers As SYSUsers
    Private objOrgCompany As OrgCompany
    Dim CultureInfo As System.Globalization.CultureInfo
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))

    Private objLoginUser As SYSUsers
    Private objSecurity As SmartV.UTILITIES.SmartSecurity

    Private EmployeesLeavesReport As String = "EmployeesLeavesReport"
    Private EmployeesPermissionsReport As String = "EmployeesPermissionsReport"
    Private EmployeeOutDuration As String = "EmployeeOutDuration"
    Private AbsentEmployeesReport As String = "AbsentEmployeesReport"

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

    Public Property SubDT() As DataTable
        Get
            Return ViewState("SubDT")
        End Get
        Set(ByVal value As DataTable)
            ViewState("SubDT") = value
        End Set
    End Property

    Public Property HeadDT() As DataTable
        Get
            Return ViewState("HeadDT")
        End Get
        Set(ByVal value As DataTable)
            ViewState("HeadDT") = value
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

    Public Property FK_CompanyId() As Integer
        Get
            Return ViewState("FK_CompanyId")
        End Get
        Set(ByVal value As Integer)
            ViewState("FK_CompanyId") = value
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

    Public Property FromDate() As DateTime
        Get
            Return ViewState("FromDate")
        End Get
        Set(ByVal value As DateTime)
            ViewState("FromDate") = value
        End Set
    End Property

    Public Property ToDate() As DateTime
        Get
            Return ViewState("ToDate")
        End Get
        Set(ByVal value As DateTime)
            ViewState("ToDate") = value
        End Set
    End Property

    Public Property ReportType() As String
        Get
            Return ViewState("ReportType")
        End Get
        Set(ByVal value As String)
            ViewState("ReportType") = value
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

    Public Shared ReadOnly Property BaseSiteUrl() As String
        Get
            Dim context As HttpContext = HttpContext.Current
            Dim baseUrl As String = context.Request.Url.Authority + context.Request.ApplicationPath.TrimEnd("/"c)
            Return baseUrl
        End Get
    End Property

    Public Property Url() As String
        Get
            Return ViewState("Url")
        End Get
        Set(ByVal value As String)
            ViewState("Url") = value
        End Set
    End Property

    Public Property RPTHeader() As String
        Get
            Return ViewState("RPTHeader")
        End Get
        Set(ByVal value As String)
            ViewState("RPTHeader") = value
        End Set
    End Property

    Public Property Password() As String
        Get
            Return ViewState("Password")
        End Get
        Set(ByVal value As String)
            ViewState("Password") = value
        End Set
    End Property

    Public Property SystemUsersType() As Integer
        Get
            Return ViewState("SystemUsersType")
        End Get
        Set(ByVal value As Integer)
            ViewState("SystemUsersType") = value
        End Set
    End Property

    Public Property ReportLang() As String
        Get
            Return ViewState("ReportLang")
        End Get
        Set(ByVal value As String)
            ViewState("ReportLang") = value
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
        FillUserName()
        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        Dim ShowLoginForm As Boolean

        If SessionVariables.LoginUser Is Nothing Then
            objAPP_Settings = New APP_Settings
            objAPP_Settings.GetByPK()
            SystemUsersType = objAPP_Settings.SystemUsersType
            ShowLoginForm = objAPP_Settings.ShowLoginForm
            If (SystemUsersType = 2 Or SystemUsersType = 3) And (ShowLoginForm = False) Then
                FillUserName()
                CheckLogin()
            Else
                dvLogin.Visible = True
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("LoginToViewReport", CultureInfo), "info")
                Return
            End If
        Else
            dvLogin.Visible = False
        End If
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)

        If Not Page.IsPostBack Then
            Dim strFK_CompanyId As String = Request.QueryString("FK_CompanyId").ToString
            Dim strEmployeeId As String = Request.QueryString("FK_EmployeeId").ToString

            FK_CompanyId = Convert.ToInt32(strFK_CompanyId)
            EmployeeId = Convert.ToInt32(strEmployeeId)
            FromDate = DateTime.ParseExact(Request.QueryString("FromDate"), "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
            ToDate = DateTime.ParseExact(Request.QueryString("ToDate"), "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
            ReportType = Request.QueryString("ReportType")
            ReportLang = Request.QueryString("ReportLang").ToString

            If ReportLang = "AR" Then
                Lang = CtlCommon.Lang.AR
                SessionVariables.CultureInfo = "ar-JO"
            Else
                Lang = CtlCommon.Lang.EN
                SessionVariables.CultureInfo = "en-US"
            End If

            If ReportType = EmployeesPermissionsReport Then
                RPTHeader = IIf(Lang = CtlCommon.Lang.AR, "2ـ تقرير المغادرات حسب النوع", "2- Permission Per Type")
            ElseIf ReportType = EmployeeOutDuration Then
                RPTHeader = IIf(Lang = CtlCommon.Lang.AR, "31- تقرير مدة الخروج", "31-Out Duration Report")
            ElseIf ReportType = EmployeesLeavesReport Then
                RPTHeader = IIf(Lang = CtlCommon.Lang.AR, "6ـ اجازات الموظفين", "6- Employee Leaves Report")
            ElseIf ReportType = AbsentEmployeesReport Then
                RPTHeader = IIf(Lang = CtlCommon.Lang.AR, "5ـ غيابات الموظفين", "5- Absent Employees Report")
            End If


            BindReport(ReportType)

        End If

    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        cryRpt.Close()
        cryRpt.Dispose()
    End Sub

    Public Sub BindReport(ByVal reportName As String)
        reportName = reportName.Trim

        SetReportName(reportName.Trim)

        cryRpt = New ReportDocument

        cryRpt.Load(Server.MapPath(RPTName))
        cryRpt.SetDataSource(DT)
        Dim objAPP_Settings As New APP_Settings
        objAPP_Settings.FK_CompanyId = FK_CompanyId
        dt2 = objAPP_Settings.GetHeader()

        dt2.Columns.Add("From_Date")
        dt2.Columns.Add("To_Date")
        dt2.Rows(0).Item("From_Date") = DateToString(FromDate)
        dt2.Rows(0).Item("To_Date") = DateToString(ToDate)

        If reportName.Trim = EmployeesLeavesReport Then
            cryRpt.Subreports("rptSubLeaves").SetDataSource(SubDT)
        End If

        cryRpt.Subreports("rptHeader").SetDataSource(dt2)
        cryRpt.SetParameterValue("@UserName", UserName)



        Dim ShowSTLogo As Boolean
        ShowSTLogo = ConfigurationManager.AppSettings("ShowSmartTimeLogo")
        cryRpt.SetParameterValue("@ShowSTLogo", False)
        cryRpt.SetParameterValue("@ShowSTLogo", False, "rptHeader")



        cryRpt.SetParameterValue("@ReportName", RPTHeader)
        cryRpt.SetParameterValue("@ReportName", RPTHeader, "rptHeader")
        cryRpt.SetParameterValue("@FromDate", DateToString(FromDate))
        cryRpt.SetParameterValue("@ToDate", DateToString(ToDate))
        Version = SmartV.Version.version.GetVersionNumber
        cryRpt.SetParameterValue("@Version", Version)

        If reportName = EmployeesPermissionsReport Or reportName = EmployeesLeavesReport Then
            cryRpt.SetParameterValue("@URL", BaseSiteUrl)
        End If


        If SessionVariables.CultureInfo = "ar-JO" Then
            CopyRightAr = objSmartSecurity.strSmartDecrypt(ConfigurationManager.AppSettings("CopyRightAr").ToString(), "SmartVision")
            cryRpt.SetParameterValue("@CopyRightAr", CopyRightAr)
        Else
            CopyRightEn = objSmartSecurity.strSmartDecrypt(ConfigurationManager.AppSettings("CopyRightEn").ToString(), "SmartVision")
            cryRpt.SetParameterValue("@CopyRightEn", CopyRightEn)
        End If

        Dim showSmartTimeLogo As Boolean = CBool(ConfigurationManager.AppSettings("ShowSmartTimeLogo"))
        If showSmartTimeLogo Then
            objSYSUsers = New SYSUsers
            objOrgCompany = New OrgCompany
            Dim rptObj As New Report

            objOrgCompany.CompanyId = FK_CompanyId
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
        If Not DT Is Nothing Then
            If DT.Rows.Count > 0 Then
                cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedReport")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NoDataFound", CultureInfo), "info")
            End If
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NoDataFound", CultureInfo), "info")
        End If


    End Sub

#End Region
    
#Region "Methods"

    Private Sub SetReportName(ByVal rpttid As String)
        Dim rptObj As New Report

        rptObj.FROM_DATE = FromDate
        rptObj.TO_DATE = ToDate
        rptObj.EmployeeId = EmployeeId
        rptObj.CompanyId = FK_CompanyId
        rptObj.DirectStaffOnly = 0

        Select Case rpttid
            Case EmployeesPermissionsReport
                RPTName = "rptEmpPermissions.rpt"
                DT = rptObj.GetEmpPermissions
            Case EmployeesLeavesReport
                RPTName = "rptEmpLeaves.rpt"
                DT = rptObj.GetEmpLeaves_perType
                SubDT = rptObj.GetSub_LeavesDetails
            Case EmployeeOutDuration
                RPTName = "rpt_EmpOutDuration.rpt"
                DT = rptObj.Get_EmpOutDuration()
            Case AbsentEmployeesReport
                RPTName = "rptEmpAbsent.rpt"
                DT = rptObj.GetEmpAbsent()
        End Select

        UserName = SessionVariables.LoginUser.UsrID

        If (Lang = CtlCommon.Lang.AR) Then
            RPTName = "Arabic/" + RPTName
        Else
            RPTName = "English/" + RPTName
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
        Return tempDay + "/" + tempMonth + "/" + TempDate.Year.ToString()
    End Function

    Private Sub CheckLogin()

        Dim dt As DataTable
        Dim encPass As String = Nothing
        Dim rslt As Integer = 0
        objLoginUser = New SYSUsers
        objSecurity = New SmartV.UTILITIES.SmartSecurity

        If objLoginUser.ValidateConnection() = False Then
            CtlCommon.ShowMessage(Me.Page, "please make sure database connection")
            Exit Sub
        End If
        'FillUserName()

        encPass = "DOMAIN"
        Try
            With objLoginUser
                .LoginName = UserName
                .UsrID = UserName
                .Password = encPass
                dt = objLoginUser.getLogin()
                If (dt IsNot Nothing) AndAlso (dt.Rows.Count > 0) Then
                    dt = objLoginUser.getActive(rslt)
                    If rslt = 0 Then
                        If (dt IsNot Nothing) AndAlso (dt.Rows.Count > 0) Then
                            SessionVariables.LoginUser = objLoginUser
                            If SessionVariables.LoginUser.Active = 0 Then
                                If SessionVariables.CultureInfo = "en-US" Then
                                    CtlCommon.ShowMessage(Me.Page, "User is not Active please refere to System Adminstrator")
                                Else
                                    CtlCommon.ShowMessage(Me.Page, "المستخدم غير مفعل الرجاء مراجعة مسؤول النظام")
                                End If
                                Exit Sub
                            End If
                            Dim strdomainname As String = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString()
                            strdomainname = strdomainname.Substring(0, strdomainname.IndexOf("\"))

                            If .Password = "DOMAIN" Then
                                ' System.Configuration.ConfigurationManager.AppSettings("Domain").ToString() <> strdomainname Or
                                If Request.ServerVariables("LOGON_USER").Substring(Request.ServerVariables("LOGON_USER").IndexOf("\") + 1) <> UserName Then
                                    If SessionVariables.CultureInfo = "en-US" Then
                                        CtlCommon.ShowMessage(Me.Page, "Invalid user name or password")
                                    Else
                                        CtlCommon.ShowMessage(Me.Page, "اسم المستخدم او كلمة المرور غير صحيحة")
                                    End If
                                    Return
                                End If
                            End If
                            'If SessionVariables.CultureInfo = "ar-JO" Then
                            '    Response.Redirect("../Default/Homearabic.aspx")
                            'Else
                            '    Response.Redirect("../Default/Home.aspx")
                            'End If
                            objLoginUser.ID = SessionVariables.LoginUser.ID
                            objLoginUser.GetUser()
                            If objLoginUser.DefaultSystemLang <> "" Then
                                SessionVariables.LoginDate = DateTime.Now
                                Application("TotalOnlineUsers") = CInt(Application("TotalOnlineUsers")) + 1
                                If objLoginUser.DefaultSystemLang = "AR" Then
                                    SessionVariables.CultureInfo = "ar-JO"
                                    ' Response.Redirect("../Default/Homearabic.aspx?FromLogin=1")
                                Else
                                    SessionVariables.CultureInfo = "en-US"
                                    'Response.Redirect("../Default/Home.aspx?FromLogin=1")

                                End If
                            Else
                                SessionVariables.LoginDate = DateTime.Now
                                Application("TotalOnlineUsers") = CInt(Application("TotalOnlineUsers")) + 1
                                If SessionVariables.CultureInfo = "ar-JO" Then
                                    'Response.Redirect("../Default/Homearabic.aspx?FromLogin=1")
                                Else
                                    ' Response.Redirect("../Default/Home.aspx?FromLogin=1")
                                End If
                            End If
                        Else
                            If SessionVariables.CultureInfo = "en-US" Then
                                CtlCommon.ShowMessage(Me.Page, "Invalid user name or password")
                            Else
                                CtlCommon.ShowMessage(Me.Page, "اسم المستخدم او كلمة المرور غير صحيحة")
                            End If

                        End If
                    Else
                        If SessionVariables.CultureInfo = "en-US" Then
                            CtlCommon.ShowMessage(Me.Page, "Invalid user name or password")
                        Else
                            CtlCommon.ShowMessage(Me.Page, "اسم المستخدم او كلمة المرور غير صحيحة")
                        End If
                    End If
                Else
                    If SessionVariables.CultureInfo = "en-US" Then
                        CtlCommon.ShowMessage(Me.Page, "Invalid user name or password")
                    Else
                        CtlCommon.ShowMessage(Me.Page, "اسم المستخدم او كلمة المرور غير صحيحة")
                    End If
                End If
            End With

        Catch ex As Exception
            If SessionVariables.CultureInfo = "en-US" Then
                CtlCommon.ShowMessage(Me.Page, String.Concat("Operation Failed. \n", ex.Message))
            Else
                CtlCommon.ShowMessage(Me.Page, String.Concat("فشلت العملية.\n", ex.Message))
            End If
        Finally
            objLoginUser = Nothing
        End Try


    End Sub

    Private Sub FillUserName()
        Try
            Dim strDomain As String = ""
            strDomain = Request.ServerVariables("LOGON_USER")
            strDomain = strDomain.Substring(strDomain.IndexOf("\") + 1, strDomain.Length - strDomain.IndexOf("\") - 1)
            UserName = strDomain
        Catch ex As Exception

        End Try
    End Sub

#End Region
    
End Class
