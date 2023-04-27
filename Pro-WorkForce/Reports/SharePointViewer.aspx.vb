Imports SmartV.UTILITIES
Imports TA.Reports
Imports System.Data
Imports SmartV.UTILITIES.CtlCommon
Imports CrystalDecisions.CrystalReports.Engine
Imports TA.Admin
Imports CrystalDecisions.Shared
Imports TA.Employees
Imports System.IO
Imports System.Xml
Imports System.Security.Cryptography
Imports System.Globalization

Partial Class Reports_SharePointViewer
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objAPP_Settings As APP_Settings
    'Private Lang As CtlCommon.Lang
    Protected dir As String
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As Globalization.CultureInfo
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

    Public Property FK_ManagerId() As String
        Get
            Return ViewState("FK_ManagerId")
        End Get
        Set(ByVal value As String)
            ViewState("FK_ManagerId") = value
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

    Public Property RptType() As String
        Get
            Return ViewState("RptType")
        End Get
        Set(ByVal value As String)
            ViewState("RptType") = value
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

    Public Property UserId() As String
        Get
            Return ViewState("UserId")
        End Get
        Set(ByVal value As String)
            ViewState("UserId") = value
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
    Public Property Lang() As String
        Get
            Return ViewState("Lang")
        End Get
        Set(ByVal value As String)
            ViewState("Lang") = value
        End Set
    End Property
    Public Property RptFormat() As String
        Get
            Return ViewState("RptFormat")
        End Get
        Set(ByVal value As String)
            ViewState("RptFormat") = value
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

    Public Property ReportSelectType() As String
        Get
            Return ViewState("ReportSelectType")
        End Get
        Set(ByVal value As String)
            ViewState("ReportSelectType") = value
        End Set
    End Property

    Public Shared ReadOnly Property BaseSiteUrl() As String
        Get
            Dim context As HttpContext = HttpContext.Current
            Dim baseUrl As String = context.Request.Url.Authority + context.Request.ApplicationPath.TrimEnd("/"c)
            Return baseUrl
        End Get
    End Property
    Public Property DecryptQueryString() As String
        Get
            Return ViewState("DecryptQueryString")
        End Get
        Set(ByVal value As String)
            ViewState("DecryptQueryString") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try

                getDecrypt()
                Dim DecryptSplit As String()
                DecryptSplit = DecryptQueryString.Split("=")
                'b = a(1).Substring(0, a(1).IndexOf("&") + 1)
                'b = ""
                'b = a(2).Substring(0, a(2).IndexOf("&") + 1)
                EmployeeId = DecryptSplit(1).Substring(0, DecryptSplit(1).IndexOf("&"))
                IntFromDate = DecryptSplit(2).Substring(0, DecryptSplit(2).IndexOf("&"))
                IntToDate = DecryptSplit(3).Substring(0, DecryptSplit(3).IndexOf("&"))

                RptType = DecryptSplit(4).Substring(0, DecryptSplit(4).IndexOf("&"))
                Lang = DecryptSplit(5).Substring(0, DecryptSplit(5).IndexOf("&"))
                RptFormat = DecryptSplit(6).Substring(0, DecryptSplit(6).IndexOf("&"))
                UserId = DecryptSplit(7).Substring(0, DecryptSplit(7).IndexOf("&"))
                FK_ManagerId = DecryptSplit(8).Substring(0, DecryptSplit(8).IndexOf("&"))
                ReportSelectType = DecryptSplit(9)

                'hdEmployeeId.Value = EmployeeId
                'hdDateFrom.Value = IntFromDate
                'hdDateTo.Value = IntToDate
                'hdRptType.Value = RptType
                'hdLang.Value = Lang
                'hdRptFormat.Value = RptFormat
                'hdUserId.Value = UserId
                'hdManagerId.Value = FK_ManagerId
                'hdReportSelectType.Value = ReportSelectType


                'EmployeeId = Request.QueryString("FK_EmployeeId")
                'FK_ManagerId = Request.QueryString("FK_ManagerId")
                'IntFromDate = Request.QueryString("FromDate")
                'IntToDate = Request.QueryString("ToDate")

                'RptType = Request.QueryString("RptType")
                'Lang = Request.QueryString("Lang")
                'RptFormat = Request.QueryString("RptFormat")
                'UserId = Request.QueryString("UserId")
                'ReportSelectType = Request.QueryString("ReportSelectType")
                'getEncrypt()

                'ConsoleLog(EmployeeId)
                FromDate = IntFormatDate(IntFromDate).ToString
                ToDate = IntFormatDate(IntToDate).ToString
            Catch ex As Exception
                Response.Write(ex.ToString)
            End Try

            If ToDate >= FromDate Then
                Try
                    BindReport()
                Catch ex As Exception
                    Response.Write(ex.ToString)
                End Try
            End If

        End If
    End Sub

    Shared scriptTag As String = "<script type="""" language="""">{0}</script>"
    Public Shared Sub ConsoleLog(message As String)
        Dim [function] As String = "console.log('{0}');"
        Dim log As String = String.Format(GenerateCodeFromFunction([function]), message)

        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)

        'If ScriptManager.GetCurrent(page).IsInAsyncPostBack Then
        '    ScriptManager.RegisterClientScriptBlock(page, page.[GetType](), "log", (Convert.ToString("console.log('") & message) + "')", True)
        'Else
        HttpContext.Current.Response.Write(log)
        'End If
    End Sub

    Private Shared Function GenerateCodeFromFunction([function] As String) As String
        Return String.Format(scriptTag, [function])
    End Function

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        If (cryRpt IsNot Nothing) Then
            cryRpt.Close()
            cryRpt.Dispose()
        End If
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

    'Public Shared Function GetPageDirection() As String
    '    If SessionVariables.CultureInfo = "ar-JO" Then
    '        Return "rtl"
    '    Else
    '        Return "ltr"
    '    End If
    'End Function

    Public Sub BindReport()

        SetReportName()

        If DT Is Nothing Then
            Response.Write("No data available")
            Return
        End If
        If DT.Rows.Count = 0 Then
            Response.Write("No data available")
            Return
        End If

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

        cryRpt.SetParameterValue("@UserName", UserName)
        Select Case RptType
            Case 1
                cryRpt.SetParameterValue("@ReportName", IIf(Lang = "ar", "تقرير  الحركات المفصلة ", " Detailed Transaction"))
                cryRpt.SetParameterValue("@ReportName", IIf(Lang = "ar", "تقرير  الحركات المفصلة ", " Detailed Transaction"), "rptHeader")
            Case 2
                cryRpt.SetParameterValue("@ReportName", IIf(Lang = "ar", "تقرير  حركات الحضور ", " Attendance Transactions Report"))
                cryRpt.SetParameterValue("@ReportName", IIf(Lang = "ar", "تقرير  حركات الحضور ", " Attendance Transactions Report"), "rptHeader")
            Case 3
                cryRpt.SetParameterValue("@ReportName", IIf(Lang = "ar", "تقرير  الحركات المفصلة للموظفين ", " Employee Detailed Transaction"))
                cryRpt.SetParameterValue("@ReportName", IIf(Lang = "ar", "تقرير  الحركات المفصلة للموظفين", " Employee Detailed Transaction"), "rptHeader")
            Case 5
                cryRpt.SetParameterValue("@ReportName", IIf(Lang = "ar", "تقرير الملخص ", " Summary Reports"))
                cryRpt.SetParameterValue("@ReportName", IIf(Lang = "ar", "تقرير الملخص ", " Summary Reports"), "rptHeader")
            Case 4
                cryRpt.SetParameterValue("@ReportName", IIf(Lang = "ar", "تقرير  الحركات المفصلة للموظفين ", " Employee Detailed Transaction"))
                cryRpt.SetParameterValue("@ReportName", IIf(Lang = "ar", "تقرير  الحركات المفصلة للموظفين", " Employee Detailed Transaction"), "rptHeader")
            Case 6
                cryRpt.SetParameterValue("@ReportName", IIf(Lang = "ar", "تقرير  الحركات المفصلة للموظفين ", " Employee Detailed Transaction"))
                cryRpt.SetParameterValue("@ReportName", IIf(Lang = "ar", "تقرير  الحركات المفصلة للموظفين", " Employee Detailed Transaction"), "rptHeader")
        End Select

        cryRpt.SetParameterValue("@FromDate", DateToString(FromDate))
        cryRpt.SetParameterValue("@ToDate", DateToString(ToDate))
        'Version = System.Configuration.ConfigurationManager.AppSettings("Version").ToString()
        Version = SmartV.Version.version.GetVersionNumber
        cryRpt.SetParameterValue("@Version", Version)
        If Lang = "ar" Then
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

            If EmployeeId <> 0 Then
                objUser.EmployeeId = EmployeeId
            Else
                objUser.EmployeeId = FK_ManagerId
            End If

            objUser.GetByPK()
            Try
                objOrgCompany.CompanyId = objUser.FK_CompanyId
                objOrgCompany.GetByPK()
            Catch ex As Exception

            End Try

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
        Dim stringNewGiudId As String = Guid.NewGuid.ToString()
        If Not DT Is Nothing Then
            If DT.Rows.Count > 0 Then
                'cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedReport")
                'CRV.Visible = True
                If RptFormat = 1 Then
                    'cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, stringNewGiudId)

                    Dim oStream As IO.Stream = cryRpt.ExportToStream(ExportFormatType.PortableDocFormat)
                    Response.Clear()
                    Response.ClearHeaders()
                    Response.ClearContent()
                    HttpContext.Current.Response.AppendHeader("Cache-Control", "force-download")
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "inline;filename=" & String.Format("ExportDocument_{0}", stringNewGiudId.Replace(",", "_")) & ".pdf")
                    HttpContext.Current.Response.ContentType = "application/pdf"
                    HttpContext.Current.Response.Buffer = True
                    HttpContext.Current.Response.BinaryWrite(GetStreamAsByteArray(oStream))
                    HttpContext.Current.Response.Flush()
                    cryRpt.Close()
                    cryRpt.Dispose()

                ElseIf RptFormat = 2 Then
                    cryRpt.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, True, stringNewGiudId)
                ElseIf RptFormat = 3 Then
                    FillExcelreport(RptType)
                    ExportDataSetToExcel(NDT, stringNewGiudId)
                End If
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NoDataFound", CultureInfo), "info")
            End If
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NoDataFound", CultureInfo), "info")
        End If

    End Sub

    Private Function GetStreamAsByteArray(ByVal stream As System.IO.Stream) As Byte()
        Dim streamLength As Integer = Convert.ToInt32(stream.Length)
        Dim fileData As Byte() = New Byte(streamLength) {}
        stream.Read(fileData, 0, streamLength)
        stream.Flush()
        stream.Close()
        Return fileData
    End Function


    Private Sub SetReportName()
        Dim rptObj As New Report
        UserName = UserId
        'Version = System.Configuration.ConfigurationManager.AppSettings("Version").ToString()

        If Not FromDate = Nothing Then
            rptObj.FROM_DATE = FromDate
        End If
        If Not ToDate = Nothing Then
            rptObj.TO_DATE = ToDate
        End If


        rptObj.EmpTypeId = ReportSelectType

        'If RptType = 4 Or RptType = 3 Then
        '    If EmployeeId <> 0 Then

        '        rptObj.ManagerId = EmployeeId
        '    Else
        '        rptObj.ManagerId = Nothing
        '    End If
        'Else
        '    If EmployeeId <> 0 Then

        '        rptObj.EmployeeId = EmployeeId
        '    Else
        '        rptObj.EmployeeId = Nothing
        '    End If
        'End If
        If EmployeeId <> 0 Then

            rptObj.EmployeeId = EmployeeId
        Else
            rptObj.EmployeeId = Nothing
        End If
        If FK_ManagerId <> 0 Then

            rptObj.ManagerId = FK_ManagerId
        Else
            rptObj.ManagerId = Nothing
        End If

        Select Case RptType

            Case 1
                ' employee report
                RPTName = "rptDetailedTransactions.rpt"
                DT = rptObj.GetDetailed_Transactions_SP
            Case 2
                'employee report
                RPTName = "rptDetEmpMove.rpt"
                DT = rptObj.GetFilterdDetEmpMove_SP
            Case 3
                ' org manager
                RPTName = "rptDetailedTransactions_OrgMgr.rpt"
                DT = rptObj.GetDetailed_Transactions_Mgr_SP_OrgEntity
            Case 4
                ' supervisor
                RPTName = "rptDetailedTransactions.rpt"
                DT = rptObj.GetDetailed_Transactions_OrgMgr_SP
            Case 5
                RPTName = "rptSummary.rpt"
                DT = rptObj.RptSummary_OrgMgr()
            Case 6
                ' Vice Principal
                RPTName = "rptDetailedTransactions.rpt"
                DT = rptObj.GetDetailed_Transactions_Mgr_SP_VicePrincipal
        End Select

        If (Lang = "ar") Then
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

    

    Private Sub FillExcelreport(ByVal RptType As String)

        NDT = New DataTable
        NDT = DT.Clone()
        Select Case RptType
            Case 1

                NDT.Columns(3).DataType = System.Type.GetType("System.String")
                NDT.Columns(4).DataType = System.Type.GetType("System.String")
                NDT.Columns(5).DataType = System.Type.GetType("System.String")
                NDT.Columns(6).DataType = System.Type.GetType("System.String")
                NDT.Columns(9).DataType = System.Type.GetType("System.String")
                NDT.Columns(20).DataType = System.Type.GetType("System.String")
                NDT.Columns(21).DataType = System.Type.GetType("System.String")
                NDT.Columns(22).DataType = System.Type.GetType("System.String")
                NDT.Columns(23).DataType = System.Type.GetType("System.String")
                NDT.Columns(24).DataType = System.Type.GetType("System.String")
                For Each row As DataRow In DT.Rows
                    Dim dr As DataRow = NDT.NewRow
                    For i As Integer = 0 To NDT.Columns.Count - 1
                        dr(i) = row(i)
                        If i = 3 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToShortDateString()
                            End If
                        ElseIf i = 4 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 5 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 6 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 9 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If

                        ElseIf i = 21 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 22 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 23 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 24 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        End If
                    Next
                    NDT.Rows.Add(dr)
                Next
                If (Lang = "ar") Then
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "التاريخ"
                    NDT.Columns(3).ColumnName = "وقت الدخول"
                    NDT.Columns(4).ColumnName = "وقت الخروج"
                    NDT.Columns(5).ColumnName = "المدة"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "اسم السبب"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "اسم الحالة"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "نوع الاجازة"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "نوع المغادرة"
                    NDT.Columns(12).ColumnName = "من وقت"
                    NDT.Columns(13).ColumnName = "الى وقت"
                    NDT.Columns(14).ColumnName = "التأخير"
                    NDT.Columns(15).ColumnName = "الخروج المبكر"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns(16).ColumnName = "اسم العطلة"
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)

                Else
                    NDT.Columns(0).ColumnName = "Employee No"
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Date"
                    NDT.Columns(3).ColumnName = "In Time"
                    NDT.Columns(4).ColumnName = "Out Time"
                    NDT.Columns(5).ColumnName = "Duration"
                    NDT.Columns(6).ColumnName = "Reason Name"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "Status Name"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Leave Type"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "Permission Type"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "From Time"
                    NDT.Columns(13).ColumnName = "To Time"
                    NDT.Columns(14).ColumnName = "Delay"
                    NDT.Columns(15).ColumnName = "Early Out"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns(16).ColumnName = "Holiday Name"
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                End If

            Case 2
                If (Lang = "ar") Then
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortDateString()
                                End If
                            ElseIf i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "اسم الشركة"
                    NDT.Columns(4).ColumnName = "السبب"
                    NDT.Columns(5).ColumnName = "التاريخ "
                    NDT.Columns(6).ColumnName = "الوقت "
                    NDT.Columns(7).ColumnName = "جهاز القارئ "
                    NDT.Columns(8).ColumnName = "الملاحظات"
                Else
                    NDT.Columns(8).DataType = System.Type.GetType("System.String")
                    NDT.Columns(9).DataType = System.Type.GetType("System.String")
                    For Each row As DataRow In DT.Rows
                        Dim dr As DataRow = NDT.NewRow
                        For i As Integer = 0 To NDT.Columns.Count - 1
                            dr(i) = row(i)
                            If i = 8 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToShortDateString()
                                End If
                            ElseIf i = 9 Then
                                If Not dr(i) Is DBNull.Value Then
                                    dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                                End If
                            End If
                        Next
                        NDT.Rows.Add(dr)
                    Next
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(3)
                    NDT.Columns(3).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(4)
                    NDT.Columns(4).ColumnName = "Reason"
                    NDT.Columns(5).ColumnName = "Date"
                    NDT.Columns(6).ColumnName = "Time"
                    NDT.Columns(7).ColumnName = "Reader"
                    NDT.Columns(8).ColumnName = "Remarks"
                End If


            Case 4

                NDT = DT
                If (Lang = "ar") Then
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "اسم الموظف باللغة العربية"
                    NDT.Columns(3).ColumnName = "التأخير"
                    NDT.Columns(4).ColumnName = "الخروج المبكر"
                    NDT.Columns(5).ColumnName = "الوقت الاضافي"
                    NDT.Columns(6).ColumnName = "عدد مرات التأخير"
                    NDT.Columns(7).ColumnName = "عدد مرات الخروج المبكر"
                    NDT.Columns(8).ColumnName = "عدد مرات الوقت الاضافي"
                    NDT.Columns(9).ColumnName = "عدد مرات الغياب"
                    NDT.Columns(10).ColumnName = "عدد مرات الاجازة"
                    NDT.Columns(11).ColumnName = "مجموع ساعات العمل"
                Else
                    NDT.Columns(0).ColumnName = "Emp No."
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns(2).ColumnName = "Employee Arabic Name"
                    NDT.Columns(3).ColumnName = "Delay"
                    NDT.Columns(4).ColumnName = "Early Out"
                    NDT.Columns(5).ColumnName = "OverTime"
                    NDT.Columns(6).ColumnName = "Delay Times"
                    NDT.Columns(7).ColumnName = "Early Out Times"
                    NDT.Columns(8).ColumnName = "OverTime Times"
                    NDT.Columns(9).ColumnName = "Absent Count"
                    NDT.Columns(10).ColumnName = "Rest Count"
                    NDT.Columns(11).ColumnName = "Total Work Hours"
                End If
            Case 3

                NDT.Columns(3).DataType = System.Type.GetType("System.String")
                NDT.Columns(4).DataType = System.Type.GetType("System.String")
                NDT.Columns(5).DataType = System.Type.GetType("System.String")
                NDT.Columns(6).DataType = System.Type.GetType("System.String")
                NDT.Columns(9).DataType = System.Type.GetType("System.String")
                NDT.Columns(20).DataType = System.Type.GetType("System.String")
                NDT.Columns(21).DataType = System.Type.GetType("System.String")
                NDT.Columns(22).DataType = System.Type.GetType("System.String")
                NDT.Columns(23).DataType = System.Type.GetType("System.String")
                NDT.Columns(24).DataType = System.Type.GetType("System.String")
                For Each row As DataRow In DT.Rows
                    Dim dr As DataRow = NDT.NewRow
                    For i As Integer = 0 To NDT.Columns.Count - 1
                        dr(i) = row(i)
                        If i = 3 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToShortDateString()
                            End If
                        ElseIf i = 4 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 5 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 6 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 9 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If

                        ElseIf i = 21 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 22 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 23 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 24 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        End If
                    Next
                    NDT.Rows.Add(dr)
                Next
                If (Lang = "ar") Then
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "التاريخ"
                    NDT.Columns(3).ColumnName = "وقت الدخول"
                    NDT.Columns(4).ColumnName = "وقت الخروج"
                    NDT.Columns(5).ColumnName = "المدة"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "اسم السبب"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "اسم الحالة"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "نوع الاجازة"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "نوع المغادرة"
                    NDT.Columns(12).ColumnName = "من وقت"
                    NDT.Columns(13).ColumnName = "الى وقت"
                    NDT.Columns(14).ColumnName = "التأخير"
                    NDT.Columns(15).ColumnName = "الخروج المبكر"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns(16).ColumnName = "اسم العطلة"
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)

                Else
                    NDT.Columns(0).ColumnName = "Employee No"
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Date"
                    NDT.Columns(3).ColumnName = "In Time"
                    NDT.Columns(4).ColumnName = "Out Time"
                    NDT.Columns(5).ColumnName = "Duration"
                    NDT.Columns(6).ColumnName = "Reason Name"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "Status Name"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Leave Type"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "Permission Type"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "From Time"
                    NDT.Columns(13).ColumnName = "To Time"
                    NDT.Columns(14).ColumnName = "Delay"
                    NDT.Columns(15).ColumnName = "Early Out"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns(16).ColumnName = "Holiday Name"
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                End If
            Case 5

                NDT.Columns(3).DataType = System.Type.GetType("System.String")
                NDT.Columns(4).DataType = System.Type.GetType("System.String")
                NDT.Columns(5).DataType = System.Type.GetType("System.String")
                NDT.Columns(6).DataType = System.Type.GetType("System.String")
                NDT.Columns(9).DataType = System.Type.GetType("System.String")
                NDT.Columns(20).DataType = System.Type.GetType("System.String")
                NDT.Columns(21).DataType = System.Type.GetType("System.String")
                NDT.Columns(22).DataType = System.Type.GetType("System.String")
                NDT.Columns(23).DataType = System.Type.GetType("System.String")
                NDT.Columns(24).DataType = System.Type.GetType("System.String")
                For Each row As DataRow In DT.Rows
                    Dim dr As DataRow = NDT.NewRow
                    For i As Integer = 0 To NDT.Columns.Count - 1
                        dr(i) = row(i)
                        If i = 3 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToShortDateString()
                            End If
                        ElseIf i = 4 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 5 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 6 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 9 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If

                        ElseIf i = 21 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 22 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 23 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        ElseIf i = 24 Then
                            If Not dr(i) Is DBNull.Value Then
                                dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                            End If
                        End If
                    Next
                    NDT.Rows.Add(dr)
                Next
                If (Lang = "ar") Then
                    NDT.Columns(0).ColumnName = "رقم الموظف"
                    NDT.Columns.RemoveAt(1)
                    NDT.Columns(1).ColumnName = "اسم الموظف"
                    NDT.Columns(2).ColumnName = "التاريخ"
                    NDT.Columns(3).ColumnName = "وقت الدخول"
                    NDT.Columns(4).ColumnName = "وقت الخروج"
                    NDT.Columns(5).ColumnName = "المدة"
                    NDT.Columns.RemoveAt(6)
                    NDT.Columns(6).ColumnName = "اسم السبب"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "اسم الحالة"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "اسم الشركة"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "اسم وحدة العمل"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "نوع الاجازة"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "نوع المغادرة"
                    NDT.Columns(12).ColumnName = "من وقت"
                    NDT.Columns(13).ColumnName = "الى وقت"
                    NDT.Columns(14).ColumnName = "التأخير"
                    NDT.Columns(15).ColumnName = "الخروج المبكر"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns(16).ColumnName = "اسم العطلة"
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)

                Else
                    NDT.Columns(0).ColumnName = "Employee No"
                    NDT.Columns(1).ColumnName = "Employee Name"
                    NDT.Columns.RemoveAt(2)
                    NDT.Columns(2).ColumnName = "Date"
                    NDT.Columns(3).ColumnName = "In Time"
                    NDT.Columns(4).ColumnName = "Out Time"
                    NDT.Columns(5).ColumnName = "Duration"
                    NDT.Columns(6).ColumnName = "Reason Name"
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns.RemoveAt(7)
                    NDT.Columns(7).ColumnName = "Status Name"
                    NDT.Columns.RemoveAt(8)
                    NDT.Columns(8).ColumnName = "Company Name"
                    NDT.Columns.RemoveAt(9)
                    NDT.Columns(9).ColumnName = "Entity Name"
                    NDT.Columns.RemoveAt(10)
                    NDT.Columns(10).ColumnName = "Leave Type"
                    NDT.Columns.RemoveAt(11)
                    NDT.Columns(11).ColumnName = "Permission Type"
                    NDT.Columns.RemoveAt(12)
                    NDT.Columns(12).ColumnName = "From Time"
                    NDT.Columns(13).ColumnName = "To Time"
                    NDT.Columns(14).ColumnName = "Delay"
                    NDT.Columns(15).ColumnName = "Early Out"
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns.RemoveAt(16)
                    NDT.Columns(16).ColumnName = "Holiday Name"
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                    NDT.Columns.RemoveAt(17)
                End If
        End Select


    End Sub

#End Region

#Region "Encrypt and Decrypt Methods"
    Private passPhrase As String = "Pas5pr@se"
    ' can be any string
    Private saltValue As String = "s@1tValue"
    ' can be any string
    Private hashAlgorithm As String = "SHA1"
    ' can be "MD5"
    Private passwordIterations As Integer = 2
    ' can be any number
    Private initVector As String = "@1B2c3D4e5F6g7H8"
    ' must be 16 bytes
    Private keySize As Integer = 256
    Public Sub New()
    End Sub

    Public Sub getDecrypt()
        Dim parm As String
        parm = Request.QueryString("Encrypt").ToString()
        DecryptQueryString = Decrypt(parm)
    End Sub


    Public Sub getEncrypt()
        'for testing Encrypt
        Dim a As String = ""
        a = Encrypt(BaseSiteUrl & "Reports/SharePointViewer.aspx?" & "FK_EmployeeId=" & EmployeeId & "&FromDate=" & IntFromDate.ToString() & "&ToDate=" & IntToDate.ToString() & "&RptType=" & RptType & _
        "&Lang=" & Lang & "&RptFormat=" & RptFormat & "&UserId=" & UserId & "&FK_ManagerId=" & FK_ManagerId & "&ReportSelectType=" & ReportSelectType)
    End Sub



    Public Function Encrypt(plainText As String) As String

        Dim initVectorBytes As Byte() = Encoding.ASCII.GetBytes(initVector)
        Dim saltValueBytes As Byte() = Encoding.ASCII.GetBytes(saltValue)

        Dim plainTextBytes As Byte() = Encoding.UTF8.GetBytes(plainText)
        Dim password As New PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations)

        Dim keyBytes As Byte() = password.GetBytes(keySize / 8)


        Dim symmetricKey As New RijndaelManaged()

        symmetricKey.Mode = CipherMode.CBC

        Dim encryptor As ICryptoTransform = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes)

        Dim memoryStream As New MemoryStream()

        Dim cryptoStream As New CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)
        ' Start encrypting.
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length)

        ' Finish encrypting.
        cryptoStream.FlushFinalBlock()

        ' Convert our encrypted data from a memory stream into a byte array.
        Dim cipherTextBytes As Byte() = memoryStream.ToArray()

        ' Close both streams.
        memoryStream.Close()
        cryptoStream.Close()

        ' Convert encrypted data into a base64-encoded string.
        Dim cipherText As String = Convert.ToBase64String(cipherTextBytes)

        ' Return encrypted string.
        Return cipherText
    End Function

    Public Function Decrypt(cipherText As String) As String
        Dim initVectorBytes As Byte() = Encoding.ASCII.GetBytes(initVector)
        Dim saltValueBytes As Byte() = Encoding.ASCII.GetBytes(saltValue)

        ' Convert our ciphertext into a byte array.
        Dim cipherTextBytes As Byte() = Convert.FromBase64String(cipherText.Replace(" ", "+"))
        Dim password As New PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations)
        Dim keyBytes As Byte() = password.GetBytes(keySize / 8)
        Dim symmetricKey As New RijndaelManaged()
        symmetricKey.Mode = CipherMode.CBC

        Dim decryptor As ICryptoTransform = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes)

        Dim memoryStream As New MemoryStream(cipherTextBytes)

        Dim cryptoStream As New CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read)
        Dim plainTextBytes As Byte() = New Byte(cipherTextBytes.Length - 1) {}

        ' Start decrypting.
        Dim decryptedByteCount As Integer = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length)

        ' Close both streams.
        memoryStream.Close()
        cryptoStream.Close()


        Dim plainText As String = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount)

        ' Return decrypted string.
        Return plainText
    End Function

#End Region

End Class
