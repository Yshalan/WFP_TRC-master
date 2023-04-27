Imports System.Data
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports SmartV.UTILITIES
Imports TA.Employees
Imports TA.Admin
Imports SmartV.UTILITIES.CtlCommon
Imports TA.Reports
Imports TA.Definitions
Imports TA.Security

Partial Class Reports_Permission_per_type
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objAPP_Settings As APP_Settings
    Private Lang As CtlCommon.Lang
    Protected dir As String
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Dim cryRpt As New ReportDocument
    Dim objSmartSecurity As New SmartSecurity

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

    Protected Sub Reports_Permission_per_type_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
            Page.MasterPageFile = "~/default/ArabicMaster.master"
        Else
            Lang = CtlCommon.Lang.EN
            Page.MasterPageFile = "~/default/NewMaster.master"
        End If

        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        Else
            Page.UICulture = SessionVariables.CultureInfo
        End If
        dir = GetPageDirection()
    End Sub

    Public Shared Function GetPageDirection() As String
        If SessionVariables.CultureInfo = "ar-JO" Then
            Return "rtl"
        Else
            Return "ltr"
        End If
    End Function

    Protected Sub Reports_Permission_per_type_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
        Else
            Lang = CtlCommon.Lang.EN
        End If

        If Not Page.IsPostBack Then
            lblReportTitle.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "المغادرة حسب النوع", "Permission Per Type")
            RadDatePicker1.SelectedDate = Date.ParseExact(Date.Now.Month.ToString("00") + "/01/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            Dim dd As New Date
            dd = Date.ParseExact(Date.Now.Month.ToString("00") + "/" + DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month).ToString("00") + "/" + Date.Now.Year.ToString("0000"), "MM/dd/yyyy", Nothing)
            RadDatePicker2.SelectedDate = dd
            FillPerm_type()

            objAPP_Settings = New APP_Settings
            With objAPP_Settings
                .GetByPK()
                rblFormat.SelectedValue = .DefaultReportFormat

            End With
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

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        cryRpt.Close()
        cryRpt.Dispose()
    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If EmployeeFilter1.IsLevelRequired Then
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            If EmployeeFilter1.CompanyId = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SelectCompany", CultureInfo), "info")
            ElseIf EmployeeFilter1.EntityId = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SelectEntity", CultureInfo), "info")
            Else
                BindReport()
            End If
        Else
            BindReport()
        End If
    End Sub

#End Region

#Region "Methods"

    Private Sub FillPerm_type()
        Dim objPermissionsTypes As New PermissionsTypes
        CtlCommon.FillTelerikDropDownList(ddlperm_type, objPermissionsTypes.GetAll, Lang)
    End Sub

    Public Sub BindReport()
        SetReportName()

        cryRpt = New ReportDocument
        cryRpt.Load(Server.MapPath(RPTName))
        cryRpt.SetDataSource(DT)
        Dim objAPP_Settings As New APP_Settings
        objAPP_Settings.FK_CompanyId = EmployeeFilter1.CompanyId
        dt2 = objAPP_Settings.GetHeader()
        dt2.Columns.Add("From_Date")
        dt2.Columns.Add("To_Date")
        dt2.Rows(0).Item("From_Date") = DateToString(RadDatePicker1.SelectedDate)
        dt2.Rows(0).Item("To_Date") = DateToString(RadDatePicker2.SelectedDate)
        cryRpt.Subreports(cryRpt.Subreports().Item(0).Name).SetDataSource(dt2)

        cryRpt.SetParameterValue("@UserName", UserName)
        'cryRpt.SetParameterValue("@Version", Version)

        Dim ShowSTLogo As Boolean
        ShowSTLogo = ConfigurationManager.AppSettings("ShowSmartTimeLogo")
        cryRpt.SetParameterValue("@ShowSTLogo", False)
        cryRpt.SetParameterValue("@ShowSTLogo", False, "rptHeader")
        If SessionVariables.CultureInfo = "ar-JO" Then
            CopyRightAr = objSmartSecurity.strSmartDecrypt(ConfigurationManager.AppSettings("CopyRightAr").ToString(), "SmartVision")
            cryRpt.SetParameterValue("@CopyRightAr", CopyRightAr)
        Else
            CopyRightEn = objSmartSecurity.strSmartDecrypt(ConfigurationManager.AppSettings("CopyRightEn").ToString(), "SmartVision")
            cryRpt.SetParameterValue("@CopyRightEn", CopyRightEn)

        End If

        CRV.ReportSource = cryRpt
        If Not DT Is Nothing Then
            If DT.Rows.Count > 0 Then
                If rblFormat.SelectedValue = 1 Then
                    cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedReport")
                ElseIf rblFormat.SelectedValue = 2 Then
                    cryRpt.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, True, "ExportedReport")
                ElseIf rblFormat.SelectedValue = 3 Then
                    FillExcelreport()
                    ExportDataSetToExcel(NDT, "ExportedReport")
                End If
                MultiView1.SetActiveView(Report)
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NoDataFound", CultureInfo), "info")
            End If
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NoDataFound", CultureInfo), "info")
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

    Private Sub SetReportName()
        Dim rptObj As New Report

        If Not RadDatePicker1.SelectedDate Is Nothing Then
            rptObj.FROM_DATE = RadDatePicker1.SelectedDate
        End If
        If Not RadDatePicker2.SelectedDate Is Nothing Then
            rptObj.TO_DATE = RadDatePicker2.SelectedDate
        End If

        If EmployeeFilter1.EmployeeId <> 0 Then

            rptObj.EmployeeId = EmployeeFilter1.EmployeeId
        Else
            rptObj.EmployeeId = Nothing
        End If

        If Not EmployeeFilter1.CompanyId = 0 Then
            rptObj.CompanyId = EmployeeFilter1.CompanyId
        Else
            rptObj.CompanyId = Nothing


        End If

        If EmployeeFilter1.FilterType = "C" Then
            If Not EmployeeFilter1.EntityId = 0 Then
                rptObj.EntityId = EmployeeFilter1.EntityId
            Else
                rptObj.EntityId = Nothing
            End If
        ElseIf EmployeeFilter1.FilterType = "W" Then
            If Not EmployeeFilter1.EntityId = 0 Then
                rptObj.WorkLocationId = EmployeeFilter1.EntityId
            Else
                rptObj.WorkLocationId = Nothing
            End If
        ElseIf EmployeeFilter1.FilterType = "L" Then
            If Not EmployeeFilter1.EntityId = 0 Then
                rptObj.LogicalGroupId = EmployeeFilter1.EntityId
            Else
                rptObj.LogicalGroupId = Nothing
            End If
        End If

        If Not ddlperm_type.SelectedValue = -1 Then
            rptObj.Permission_id = ddlperm_type.SelectedValue
        End If


        RPTName = "rptPerm_per_type.rpt"
        DT = rptObj.GetPer_Permission()

        UserName = SessionVariables.LoginUser.UsrID
        'Version = System.Configuration.ConfigurationManager.AppSettings("Version").ToString()

        If (Lang = CtlCommon.Lang.AR) Then
            RPTName = "Arabic/" + RPTName
        Else
            RPTName = "English/" + RPTName
        End If

    End Sub

    Private Sub FillExcelreport()
        NDT = New DataTable
        NDT = DT.Clone()

        Dim UK_Culture As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("en-GB")

        If (Lang = CtlCommon.Lang.AR) Then
            NDT.Columns(11).DataType = System.Type.GetType("System.String")
            NDT.Columns(12).DataType = System.Type.GetType("System.String")
            NDT.Columns(13).DataType = System.Type.GetType("System.String")
            NDT.Columns(14).DataType = System.Type.GetType("System.String")
            NDT.Columns(15).DataType = System.Type.GetType("System.String")
            For Each row As DataRow In DT.Rows
                Dim dr As DataRow = NDT.NewRow
                For i As Integer = 0 To NDT.Columns.Count - 1
                    dr(i) = row(i)
                    If i = 11 Then
                        If Not dr(i) Is DBNull.Value Then
                            dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                        End If
                    ElseIf i = 12 Then
                        If Not dr(i) Is DBNull.Value Then
                            dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                        End If
                    ElseIf i = 13 Then
                        If Not dr(i) Is DBNull.Value Then
                            dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                        End If
                    ElseIf i = 14 Then
                        If Not dr(i) Is DBNull.Value Then
                            dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                        End If
                    ElseIf i = 15 Then
                        If Not dr(i) Is DBNull.Value Then
                            dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                        End If
                    End If
                Next
                NDT.Rows.Add(dr)
            Next
            NDT.Columns.RemoveAt(0)
            NDT.Columns(0).ColumnName = "رقم الموظف"
            NDT.Columns(1).ColumnName = "اسم الموظف"
            NDT.Columns(2).ColumnName = "اسم الموظف باللغة العربية"
            NDT.Columns(3).ColumnName = "اسم وحدة العمل"
            NDT.Columns(4).ColumnName = "اسم وحدة العمل باللغة العربية"
            NDT.Columns(5).ColumnName = "اسم الشركة"
            NDT.Columns(6).ColumnName = "اسم الشركة باللغة العربية"
            NDT.Columns.RemoveAt(7)
            NDT.Columns(7).ColumnName = "اسم المغادرة"
            NDT.Columns(8).ColumnName = "اسم المغادرة باللغة العربية"
            NDT.Columns(9).ColumnName = "تاريخ المغادرة"
            NDT.Columns(10).ColumnName = "تاريخ انتهاء المغادرة"
            NDT.Columns(11).ColumnName = "من وقت"
            NDT.Columns(12).ColumnName = "الى وقت"
            NDT.Columns(13).ColumnName = "المدة"
            NDT.Columns.RemoveAt(14)
            NDT.Columns(14).ColumnName = "الايام"
            NDT.Columns(15).ColumnName = "يوم كامل"
            NDT.Columns(16).ColumnName = "مرن"
            NDT.Columns(17).ColumnName = "الزمن المرن"
            NDT.Columns(18).ColumnName = "الملاحظات"
        Else
            NDT.Columns(11).DataType = System.Type.GetType("System.String")
            NDT.Columns(12).DataType = System.Type.GetType("System.String")
            NDT.Columns(13).DataType = System.Type.GetType("System.String")
            NDT.Columns(14).DataType = System.Type.GetType("System.String")
            NDT.Columns(15).DataType = System.Type.GetType("System.String")
            For Each row As DataRow In DT.Rows
                Dim dr As DataRow = NDT.NewRow
                For i As Integer = 0 To NDT.Columns.Count - 1
                    dr(i) = row(i)
                    If i = 11 Then
                        If Not dr(i) Is DBNull.Value Then
                            dr(i) = Convert.ToDateTime(row(i), UK_Culture).ToString("dd/MM/yyyy")
                        End If
                    ElseIf i = 12 Then
                        If Not dr(i) Is DBNull.Value Then
                            dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                        End If
                    ElseIf i = 13 Then
                        If Not dr(i) Is DBNull.Value Then
                            dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                        End If
                    ElseIf i = 14 Then
                        If Not dr(i) Is DBNull.Value Then
                            dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                        End If
                    ElseIf i = 15 Then
                        If Not dr(i) Is DBNull.Value Then
                            dr(i) = Convert.ToDateTime(row(i)).ToString("HH:mm")
                        End If
                    End If
                Next
                NDT.Rows.Add(dr)
            Next
            NDT.Columns.RemoveAt(0)
            NDT.Columns(0).ColumnName = "Emp No."
            NDT.Columns(1).ColumnName = "Employee Name"
            NDT.Columns(2).ColumnName = "Employee Arabic Name"
            NDT.Columns(3).ColumnName = "Entity Name"
            NDT.Columns(4).ColumnName = "Entity Arabic Name"
            NDT.Columns(5).ColumnName = "Company Name"
            NDT.Columns(6).ColumnName = "Company Arabic Name"
            NDT.Columns.RemoveAt(7)
            NDT.Columns(7).ColumnName = "Permission Name"
            NDT.Columns(8).ColumnName = "Permission Arabic Name"
            NDT.Columns(9).ColumnName = "Permission Date"
            NDT.Columns(10).ColumnName = "Permission End Date"
            NDT.Columns(11).ColumnName = "From Time"
            NDT.Columns(12).ColumnName = "To Time"
            NDT.Columns(13).ColumnName = "Period"
            NDT.Columns.RemoveAt(14)
            NDT.Columns(14).ColumnName = "Days"
            NDT.Columns(15).ColumnName = "Full Day"
            NDT.Columns(16).ColumnName = "Is Flexibile"
            NDT.Columns(17).ColumnName = "Flexibile Duration"
            NDT.Columns(18).ColumnName = "Remarks"
        End If
    End Sub

#End Region

End Class
