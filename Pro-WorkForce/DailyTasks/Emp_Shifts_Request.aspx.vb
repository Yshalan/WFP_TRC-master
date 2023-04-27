Imports System.Data
Imports TA.Lookup
Imports TA.Definitions
Imports TA.OrgCompany
Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports System.Globalization
Imports TA.Security
Imports TA.Employees
Imports TA.Admin

Partial Class Admin_Emp_Shifts_Request

#Region "Class Variables"

    Inherits System.Web.UI.Page
    Public Lang As CtlCommon.Lang
    Public AddButtonValue As String
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private objWorkSchedule As WorkSchedule
    Private objAPP_Settings As APP_Settings
    Private objEmployee As Employee
    Private objOrgEntity As OrgEntity

#End Region

#Region "Properties"

    Protected Property IsArabic As Boolean
        Get
            If (Lang = CtlCommon.Lang.AR) Then
                Return True
            Else
                Return False
            End If
        End Get
        Set(ByVal value As Boolean)

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
        hdnUICulture.Value = SessionVariables.CultureInfo
        ddlShift_Bind()
        ddlYear_Bind()
        ddlMonth_Bind()
        'Dim builder As New StringBuilder
        'builder.Append("<script>var IsManagerLoad =0 ; </Script>")

        If Not Page.IsPostBack Then
            FillgrdShift()
            ddlWorkSchedule_Bind()
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            PageHeader1.HeaderText = ResourceManager.GetString("EmpShiftsRequest", CultureInfo)
            AddButtonValue = ResourceManager.GetString("btnAdd", CultureInfo)
            Me.LoadControl("~/Admin/UserControls/EmployeeHeirarchy.ascx")


            Dim objEmployee As New Employee
            Dim dt As DataTable

            objEmployee.FilterType = "C"
            objEmployee.FK_EntityId = -1
            dt = objEmployee.GetEmployeeIDz
            Dim i As Integer
            Dim empstrings As New StringBuilder()
            For i = 0 To dt.Rows.Count - 1
                empstrings.Append(dt(i)("EmployeeId"))
                empstrings.Append(",")
            Next
            If dt.Rows.Count > 0 Then
                hdnEmployeeIDz.Value = empstrings.ToString().Substring(0, empstrings.ToString().Length - 1)
            End If
            objAPP_Settings = New APP_Settings
            objAPP_Settings.GetByPK()
            hdnArchivingMonths.Value = Convert.ToInt32(objAPP_Settings.ArchivingMonths) * -1
        End If

        'If (Lang = CtlCommon.Lang.AR) Then
        '    divShift.Style("float") = "right"
        '    lblShift.Style("float") = "lef"
        '    divShift.Style("margin-bottom") = "10px"
        '    divSelect.Style("margin-right") = "134px"
        '    divSelect.Style("margin-top") = "-15px"
        '    divSelect.Style("margin-left") = "0px"
        '    divColor.Style("margin-right") = "134px"
        '    divColor.Style("margin-left") = "0"
        'End If

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not divControls.FindControl(row("AddBtnName")) Is Nothing Then
                        divControls.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not divControls.FindControl(row("DeleteBtnName")) Is Nothing Then
                        divControls.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not divControls.FindControl(row("EditBtnName")) Is Nothing Then
                        divControls.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not divControls.FindControl(row("PrintBtnName")) Is Nothing Then
                        divControls.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next


    End Sub

    Protected Sub btnShowEmployee_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShowEmployee.Click
        Dim CompanyId As Integer
        Dim CompanyName As String
        Dim EnititiesIdz As Integer()
        Dim EnititiesNamesz As String()
        CompanyName = String.Empty
        EnititiesIdz = New Integer() {}
        EnititiesNamesz = New String() {}
        objEmployee = New Employee
        objOrgEntity = New OrgEntity
        objAPP_Settings = New APP_Settings
        With objAPP_Settings
            .GetByPK()
        End With
        Dim dtEmployeeIDz As DataTable
        If objAPP_Settings.ShowEmployeeList = True Then
            dtEmployeeIDz = EmployeeFilter1.GetEmployeez()
        Else
            dtEmployeeIDz = New DataTable
            Dim drSource As DataRow
            Dim dcCell1 As New DataColumn

            If Not EmployeeFilter1.EmployeeId = 0 Then
                drSource = dtEmployeeIDz.NewRow
                dtEmployeeIDz.Columns.Add("EmployeeId")
                dcCell1.ColumnName = "EmployeeId"
                dcCell1.DefaultValue = EmployeeFilter1.EmployeeId
                drSource("EmployeeId") = dcCell1.DefaultValue
                dtEmployeeIDz.Rows.Add(drSource)
            ElseIf Not EmployeeFilter1.EntityId = 0 Then

                objOrgEntity.EntityId = EmployeeFilter1.EntityId
                objOrgEntity.GetByPK()
                objEmployee.FK_EntityId = EmployeeFilter1.EntityId
                objEmployee.FK_CompanyId = objOrgEntity.FK_CompanyId
                dtEmployeeIDz = objEmployee.GetEmployeeIDz()

            Else
                ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "ddlWorkSchedule_changed(ctl00_ContentPlaceHolder1_ddlWorkSchedule.options[ctl00_ContentPlaceHolder1_ddlWorkSchedule.selectedIndex].value);", True)
                Exit Sub
            End If


        End If


        Dim empstrings As New StringBuilder()
        For i = 0 To dtEmployeeIDz.Rows.Count - 1
            objWorkSchedule = New WorkSchedule
            Dim dtSchedule As DataTable
            With objWorkSchedule
                .EmployeeId = dtEmployeeIDz(i)("EmployeeId")
                dtSchedule = .GetEmployeeActiveScheduleByEmpId(Date.Today)
            End With
            'If dtSchedule.Rows.Count > 0 Then
            '    If Convert.ToInt32(dtSchedule(0)("ScheduleType")) = 3 Then
            empstrings.Append(dtEmployeeIDz(i)("EmployeeId"))
            empstrings.Append(",")
            '    End If
            'End If

        Next
        If dtEmployeeIDz.Rows.Count > 0 Then

            hdnEmployeeIDz.Value = empstrings.ToString().Substring(0, empstrings.ToString().Length - 1)
        Else
            hdnEmployeeIDz.Value = ""
        End If
        If (EmployeeFilter1.EmployeeId <> 0) Then
            hdnEmployeeIDz.Value = EmployeeFilter1.EmployeeId
        End If
        'UserCtrlOrgHeirarchy.RetrieveGridData()





        ScriptManager.RegisterStartupScript(Page, Page.GetType, Guid.NewGuid.ToString, "scheduleGrid_AddAllEmp('" & hdnEmployeeIDz.Value & "');", True)


        'Dim CompanyId As Integer
        'Dim CompanyName As String
        'Dim EnititiesIdz As Integer()
        'Dim EnititiesNamesz As String()
        'CompanyName = String.Empty
        'EnititiesIdz = New Integer() {}
        'EnititiesNamesz = New String() {}

        'Dim EmployeeIDz As Integer() = UserCtrlOrgHeirarchy.GetEmployeez(CompanyId, CompanyName, EnititiesIdz, EnititiesNamesz)
        'If (EmployeeIDz.Length > 0) Then
        '    For Item As Integer = 0 To EmployeeIDz.Length - 1
        '        hdnEmployeeIDz.Value += EmployeeIDz(Item).ToString() & ","
        '    Next
        'End If
        'If (UserCtrlOrgHeirarchy.EmployeeeId <> 0) Then
        '    hdnEmployeeIDz.Value = UserCtrlOrgHeirarchy.EmployeeeId
        'End If
        'UserCtrlOrgHeirarchy.RetrieveGridData()
        'ScriptManager.RegisterStartupScript(Page, Page.GetType, Guid.NewGuid.ToString, "scheduleGrid_AddAllEmp('" & hdnEmployeeIDz.Value & "');", True)
    End Sub

    Protected Sub btnClear_Click(sender As Object, e As System.EventArgs) Handles btnClear.Click
        hdnUICulture.Value = SessionVariables.CultureInfo
        ddlShift_Bind()
        ddlYear_Bind()
        ddlMonth_Bind()
        'ddlWorkSchedule_Bind()
        'Dim builder As New StringBuilder
        'builder.Append("<script>var IsManagerLoad =0 ; </Script>")


        FillgrdShift()
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        ElseIf SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
        Else
            Lang = CtlCommon.Lang.EN
        End If
        PageHeader1.HeaderText = ResourceManager.GetString("EmpShifts", CultureInfo)
        AddButtonValue = ResourceManager.GetString("btnAdd", CultureInfo)
        Me.LoadControl("~/Admin/UserControls/EmployeeHeirarchy.ascx")


        Dim objEmployee As New Employee
        Dim dt As DataTable

        objEmployee.FilterType = "C"
        objEmployee.FK_EntityId = -1
        dt = objEmployee.GetEmployeeIDz
        Dim i As Integer
        Dim empstrings As New StringBuilder()
        For i = 0 To dt.Rows.Count - 1
            empstrings.Append(dt(i)("EmployeeId"))
            empstrings.Append(",")
        Next
        If dt.Rows.Count > 0 Then
            hdnEmployeeIDz.Value = empstrings.ToString().Substring(0, empstrings.ToString().Length - 1)
        End If
        EmployeeFilter1.ClearValues()
        ddlWorkSchedule.SelectedIndex = -1
    End Sub

#End Region

#Region "Methods"

    Private Sub ddlShift_Bind()
        Dim builder As New StringBuilder
        builder.Append("<script>var Shifts = [")
        Dim objWorkSchedule_Shifts As New WorkSchedule_Shifts()
        Dim dtWorkSchedule_Shifts As DataTable = objWorkSchedule_Shifts.GetAll()

        For Each row As DataRow In dtWorkSchedule_Shifts.Rows
            '"{ ""id"": ""1"", ""txt"": ""A"", ""schedule"": ""5"", ""bc"": ""#ABC19D"" },"
            builder.Append("{ ""id"": """)
            builder.Append(row("ShiftId")) 'id
            builder.Append(""", ""txt"": """)
            builder.Append(row("ShiftCode")) 'ShiftCode
            builder.Append(""", ""schedule"": """)
            builder.Append(row("FK_ScheduleId")) 'FK_ScheduleId
            builder.Append(""", ""bc"": """)
            builder.Append(row("Color")) 'Color
            builder.Append(""", ""Duration"": """)
            builder.Append(row("Duration")) 'Duration


            builder.Append(""", },")

        Next

        builder.Append("]; </Script>")
        JSON.Text = builder.ToString()
    End Sub

    Private Sub ddlYear_Bind()
        Dim year As Integer = DateTime.Now.Year
        Dim dt As New DataTable
        dt.Columns.Add("val")
        dt.Columns.Add("txt")

        'Dim dr1 As DataRow = dt.NewRow()
        'dr1("val") = 0
        'dr1("txt") = "year"
        'dt.Rows.Add(dr1)

        For i As Integer = year - 5 To year + 5
            Dim dr As DataRow = dt.NewRow()
            dr("val") = i
            dr("txt") = i
            dt.Rows.Add(dr)
        Next

        ddlYear.SelectedValue = Now.Year
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

    Private Sub ddlWorkSchedule_Bind()
        Dim objWorkSchedule As New WorkSchedule()
        objWorkSchedule.ScheduleType = 3
        ddlWorkSchedule.DataSource = objWorkSchedule.GetAllByType()
        ddlWorkSchedule.DataBind()


    End Sub

    Public Sub FillgrdShift()
        Dim builder As New StringBuilder
        builder.Append("<script>var ShiftDetails = [")
        Dim objWorkSchedule_Shifts As New WorkSchedule_Shifts()
        'objWorkSchedule_Shifts.FK_ScheduleId = 86
        Dim dtWorkSchedule_Shifts As DataTable = objWorkSchedule_Shifts.GetScheduleDetails()
        For Each row As DataRow In dtWorkSchedule_Shifts.Rows
            '"{ ""id"": ""1"", ""txt"": ""A"", ""schedule"": ""5"", ""bc"": ""#ABC19D"" },"
            builder.Append("{ ""id"": """)
            builder.Append(row("ShiftId")) 'id

            builder.Append(""", ""Scheduleid"": """)
            builder.Append(row("FK_ScheduleId")) 'id
            builder.Append(""", ""ScheduleCode"": """)
            builder.Append(row("ShiftCode")) 'ShiftCode
            builder.Append(""", ""ScheduleTime"": """)
            builder.Append(row("ScheduleTime")) 'ShiftCode
            builder.Append(""", ""bc"": """)
            builder.Append(row("Color")) 'Color
            builder.Append(""", },")

        Next

        builder.Append("]; </Script>")
        JSON_Details.Text = builder.ToString()
    End Sub

#End Region

End Class
