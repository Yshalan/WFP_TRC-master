Imports SmartV.UTILITIES
Imports TA.Reports
Imports System.Data
Imports SmartV.UTILITIES.CtlCommon
Imports CrystalDecisions.CrystalReports.Engine
Imports TA.Admin
Imports CrystalDecisions.Shared
Imports TA.Employees

Partial Class Reports_SharePointRpt
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objAPP_Settings As APP_Settings
    Private Lang As CtlCommon.Lang
    Protected dir As String
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Dim cryRpt As New ReportDocument
    Dim objSmartSecurity As New SmartSecurity
    Private objOrgCompany As OrgCompany

#End Region

#Region "Properties"

    Public Property EmployeeId() As String
        Get
            Return ViewState("EmployeeId")
        End Get
        Set(ByVal value As String)
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

    Public Property IntFromDate() As Integer
        Get
            Return ViewState("FromDate")
        End Get
        Set(ByVal value As Integer)
            ViewState("FromDate") = value
        End Set
    End Property

    Public Property IntToDate() As Integer
        Get
            Return ViewState("ToDate")
        End Get
        Set(ByVal value As Integer)
            ViewState("ToDate") = value
        End Set
    End Property

    Public Property flag() As String
        Get
            Return ViewState("flag")
        End Get
        Set(ByVal value As String)
            ViewState("flag") = value
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
#End Region

#Region "Page Events"

    'Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
    '    If SessionVariables.CultureInfo = "ar-JO" Then
    '        Lang = CtlCommon.Lang.AR
    '        Page.MasterPageFile = "~/default/ArabicMaster.master"
    '    Else
    '        Lang = CtlCommon.Lang.EN
    '        Page.MasterPageFile = "~/default/NewMaster.master"
    '    End If

    '    If (SessionVariables.CultureInfo Is Nothing) Then
    '        Response.Redirect("~/default/Login.aspx")
    '    Else
    '        Page.UICulture = SessionVariables.CultureInfo
    '    End If
    '    dir = GetPageDirection()
    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try

            EmployeeId = Request.QueryString("empId")
            IntFromDate = Request.QueryString("fromdate")
            IntToDate = Request.QueryString("todate")
            FromDate = IntFormatDate(IntFromDate).ToString
            ToDate = IntFormatDate(IntToDate).ToString
            flag = Request.QueryString("flag")
            Catch ex As Exception
                Response.Write(ex.ToString)
            End Try

            'EmployeeId = 109
            'IntFromDate = 20160501
            'IntToDate = 20150531
            'FromDate = IntFormatDate(IntFromDate).ToString
            'ToDate = IntFormatDate(IntToDate).ToString
            'flag = 1

            If ToDate >= FromDate Then
                BindReport()
            End If

        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        cryRpt.Close()
        cryRpt.Dispose()
    End Sub

#End Region

#Region "Methods"

    Public Function IntFormatDate(ByVal sourceDate As String) As String
        Dim strDate As String
        Dim day As String = sourceDate.Substring(6, 2)
        Dim month As String = sourceDate.Substring(4, 2)
        Dim year As String = sourceDate.Substring(0, 4)
        strDate = year & "/" & month & "/" & day
        Return strDate
    End Function

    Public Shared Function GetPageDirection() As String
        If SessionVariables.CultureInfo = "ar-JO" Then
            Return "rtl"
        Else
            Return "ltr"
        End If
    End Function

    Public Sub BindReport()

        SetReportName()

        cryRpt = New ReportDocument
        cryRpt.Load(Server.MapPath(RPTName))
        cryRpt.SetDataSource(DT)
        Dim objAPP_Settings As New APP_Settings
        dt2 = objAPP_Settings.GetHeader()
        dt2.Columns.Add("From_Date")
        dt2.Columns.Add("To_Date")
        dt2.Rows(0).Item("From_Date") = Convert.ToDateTime(FromDate).ToShortDateString
        dt2.Rows(0).Item("To_Date") = Convert.ToDateTime(ToDate).ToShortDateString
        cryRpt.Subreports(cryRpt.Subreports().Item(0).Name).SetDataSource(dt2)


        Dim ShowSTLogo As Boolean
        ShowSTLogo = ConfigurationManager.AppSettings("ShowSmartTimeLogo")
        cryRpt.SetParameterValue("@ShowSTLogo", False)
        cryRpt.SetParameterValue("@ShowSTLogo", False, "rptHeader")

        cryRpt.SetParameterValue("@UserName", "")
        Select Case flag
            Case 0
                cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "تقرير  الحركات المفصلة ", " Detailed Transaction"))
                cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "تقرير  الحركات المفصلة ", " Detailed Transaction"), "rptHeader")
            Case 1
                cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "تقرير  الحركات المفصلة ", " Detailed Transaction"))
                cryRpt.SetParameterValue("@ReportName", IIf(Lang = CtlCommon.Lang.AR, "تقرير  الحركات المفصلة ", " Detailed Transaction"), "rptHeader")
        End Select
        cryRpt.SetParameterValue("@FromDate", DateToString(FromDate))
        cryRpt.SetParameterValue("@ToDate", DateToString(ToDate))
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
            Dim objUser As New Employee
            objOrgCompany = New OrgCompany

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
        If Not DT Is Nothing Then
            If DT.Rows.Count > 0 Then
                cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedReport")
                CRV.Visible = True
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NoDataFound", CultureInfo), "info")
            End If
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NoDataFound", CultureInfo), "info")
        End If

    End Sub

    Private Sub SetReportName()
        Dim rptObj As New Report
        UserName = Request.QueryString("userid")
        'Version = System.Configuration.ConfigurationManager.AppSettings("Version").ToString()

        If Not FromDate = Nothing Then
            rptObj.FROM_DATE = FromDate
        End If
        If Not ToDate = Nothing Then
            rptObj.TO_DATE = ToDate
        End If

       
        If flag = 1 Then
            If EmployeeId <> 0 Then

                rptObj.ManagerId = EmployeeId
            Else
                rptObj.ManagerId = Nothing
            End If
        ElseIf flag = 0 Then
            If EmployeeId <> 0 Then

                rptObj.EmployeeId = EmployeeId
            Else
                rptObj.EmployeeId = Nothing
            End If
        End If
        Select Case flag

            Case 0
                RPTName = "rptDetailedTransactions.rpt"
                DT = rptObj.GetDetailed_Transactions

            Case 1
                RPTName = "rptDetailedTransactions.rpt"
                DT = rptObj.GetDetailed_Transactions_Mgr
        End Select

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

#End Region
    
End Class
