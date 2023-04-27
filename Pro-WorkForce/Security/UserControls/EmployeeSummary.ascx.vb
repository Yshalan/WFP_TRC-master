Imports TA.Employees
Imports System.Data
Imports SmartV.UTILITIES
Imports System.Web.Services
Imports System.Web.Script.Serialization
Imports TA.DashBoard

Partial Class Security_UserControls_EmployeeSummary
    Inherits System.Web.UI.UserControl

#Region "Class Variables"

    Private objEmployee As Employee
    Private objEmp_leaves As Emp_Leaves
    Private objEmp_Permissions As Emp_Permissions
    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Public ChartLang As String
    Dim data As String = "" '"{name:   'الحضور',  y: 16.11}, {name:   'عدم الحضور',y: 83.89},"
    Dim Str As String
    Dim STRsPLIT() As String

#End Region

#Region "Public Properties"

    Public Property SubTitleText() As String
        Get
            Return ViewState("SubTitleText")
        End Get
        Set(ByVal value As String)
            ViewState("SubTitleText") = value
        End Set
    End Property

    Public Property ChartTitleText() As String
        Get
            Return ViewState("ChartTitleText")
        End Get
        Set(ByVal value As String)
            ViewState("ChartTitleText") = value
        End Set
    End Property

    Public Property dt() As DataTable
        Get
            Return ViewState("dt")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dt") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Security_UserControls_EmployeeSummary_Load(sender As Object, e As EventArgs) Handles Me.Load

        If (SessionVariables.CultureInfo Is Nothing) Then
            SessionVariables.CultureInfo = "en-US"
            Lang = CtlCommon.Lang.EN
            ChartLang = "en"
        Else
            If SessionVariables.CultureInfo = "en-US" Then
                SessionVariables.CultureInfo = "en-US"
                Lang = CtlCommon.Lang.EN
                ChartLang = "en"
            Else
                SessionVariables.CultureInfo = "ar-JO"
                Lang = CtlCommon.Lang.AR
                ChartLang = "ar"
            End If

        End If

        If Not Page.IsPostBack Then
            FillSummaryHeader()
        End If

        FillHighCharts()
        fillChart()
        JSONChartTitleText.Text = "<script charset='utf-8'>var Var_ChartTitleText ='" & ChartTitleText & "' ; </Script>"
        JSONChartSubTitleText.Text = "<script type='text/javascript' charset='utf-8'>var Var_ChartSubtitleText ='" & SubTitleText & "' ; </Script>"
        JSONChartType.Text = "<script type='text/javascript' charset='utf-8'>var Var_ChartType ='" & "column" & "' ; </Script>"
        JSONSeries.Text = "<script type='text/javascript' charset='utf-8'>var Var_Series = [{name:   'Overview',colorByPoint: true,data: [" & data & "]  }]; </Script>"
        JSONdrilldown.Text = "<script type='text/javascript' charset='utf-8'>var Var_drilldown ={}; </Script>"

    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Sub lnkEmployeeDetails_Click(sender As Object, e As EventArgs) Handles lnkEmployeeDetails.Click
        dvEmployees.Visible = True
        dvLeaves.Visible = False
        dvPermissions.Visible = False
        dvStudyNursing.Visible = False
        FillEmployeeEntityInfo()
    End Sub

    Protected Sub lnkLeavesDetails_Click(sender As Object, e As EventArgs) Handles lnkLeavesDetails.Click
        dvEmployees.Visible = False
        dvLeaves.Visible = True
        dvPermissions.Visible = False
        dvStudyNursing.Visible = False
        FillLeaves()
    End Sub

    Protected Sub lnkPermissionsDetails_Click(sender As Object, e As EventArgs) Handles lnkPermissionsDetails.Click
        dvEmployees.Visible = False
        dvLeaves.Visible = False
        dvPermissions.Visible = True
        dvStudyNursing.Visible = False
        FillPermissions()
    End Sub

    Protected Sub lnkStudy_NursingDetails_Click(sender As Object, e As EventArgs) Handles lnkStudy_NursingDetails.Click
        dvEmployees.Visible = False
        dvLeaves.Visible = False
        dvPermissions.Visible = False
        dvStudyNursing.Visible = True
        FillStudyNursingPermissions()
    End Sub

    Protected Sub dgrdLeaves_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdLeaves.NeedDataSource
        objEmp_leaves = New Emp_Leaves
        Dim dt As DataTable
        With objEmp_leaves
            .UserId = SessionVariables.LoginUser.ID
            dt = .Get_ByUserId_InnerPage
        End With
        dgrdLeaves.DataSource = dt
    End Sub

    Protected Sub dgrdPermissions_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdPermissions.NeedDataSource
        objEmp_Permissions = New Emp_Permissions
        Dim dt As DataTable
        With objEmp_Permissions
            .UserId = SessionVariables.LoginUser.ID
            dt = .Get_ByUserId_InnerPage
        End With
        dgrdPermissions.DataSource = dt
    End Sub

    Protected Sub dgrdStudyNursing_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdStudyNursing.NeedDataSource
        objEmp_Permissions = New Emp_Permissions
        Dim dt As DataTable
        With objEmp_Permissions
            .UserId = SessionVariables.LoginUser.ID
            dt = .Get_StudyNursingByUserId_InnerPage
        End With
        dgrdStudyNursing.DataSource = dt
    End Sub

#End Region

#Region "Methods"

    Private Sub FillSummaryHeader()
        objEmployee = New Employee
        Dim dt As DataTable
        With objEmployee
            .UserId = SessionVariables.LoginUser.ID
            dt = .Get_Inner_Summary()
        End With
        If Not dt Is Nothing Then
            lblEmployeesVal.Text = dt(0)("Employees_Count").ToString
            lblLeavesVal.Text = dt(0)("Leaves_Count").ToString
            If lblLeavesVal.Text = 0 Then
                lnkLeavesDetails.Enabled = False
            End If
            lblPermissionsVal.Text = dt(0)("Permissions_Count").ToString
            If lblPermissionsVal.Text = 0 Then
                lnkPermissionsDetails.Enabled = False
            End If
            lblStudy_NursingVal.Text = dt(0)("Study_NursingPermissions_Count").ToString
            If lblStudy_NursingVal.Text = 0 Then
                lnkStudy_NursingDetails.Enabled = False
            End If
        End If
    End Sub

    Private Sub FillLeaves()
        objEmp_leaves = New Emp_Leaves
        Dim dt As DataTable
        With objEmp_leaves
            .UserId = SessionVariables.LoginUser.ID
            dt = .Get_ByUserId_InnerPage
        End With
        dgrdLeaves.DataSource = dt
        dgrdLeaves.DataBind()
    End Sub

    Private Sub FillPermissions()
        objEmp_Permissions = New Emp_Permissions
        Dim dt As DataTable
        With objEmp_Permissions
            .UserId = SessionVariables.LoginUser.ID
            dt = .Get_ByUserId_InnerPage
        End With
        dgrdPermissions.DataSource = dt
        dgrdPermissions.DataBind()
    End Sub

    Private Sub FillStudyNursingPermissions()
        objEmp_Permissions = New Emp_Permissions
        Dim dt As DataTable
        With objEmp_Permissions
            .UserId = SessionVariables.LoginUser.ID
            dt = .Get_StudyNursingByUserId_InnerPage
        End With
        dgrdStudyNursing.DataSource = dt
        dgrdStudyNursing.DataBind()
    End Sub

    Private Sub FillEmployeeEntityInfo()
        objEmployee = New Employee
        Dim dt As DataTable
        With objEmployee
            .FK_CompanyId = SessionVariables.LoginUser.FK_CompanyId
            dt = .GettEmployee_EntityCount
        End With
        lblEmployeeNoVal.Text = dt(0)("EmployeeCount")
        lblEntityNoVal.Text = dt(0)("EntityCount")

    End Sub

    Public Sub FillHighCharts()
        Dim language As String
        If SessionVariables.CultureInfo = "en-US" Then
            language = "en"
        ElseIf SessionVariables.CultureInfo = "ar-JO" Then
            language = "ar"
        End If
        Dim objDashBoard As New DashBoard
        Dim dtCAChart As DataTable

        With objDashBoard
            .Lang = language
            .ID = SessionVariables.LoginUser.ID
            .FromDate = "2018-10-07" 'Date.Today
            dtCAChart = .GetInnerPage_AttendPercent_DashB
            If Not dtCAChart Is Nothing Then
                dt = dtCAChart
            End If
            '{id: 'absent',name:   'Absent',data:  [['Absent',26.11], ['Not Absent',73.89] ] } 

        End With
    End Sub

    Public Sub fillChart()
        If Not (dt Is Nothing) Then
            'data = "["
            For i As Integer = 0 To dt.Rows.Count - 1
                Str = dt.Rows(i)("Percentage")
                STRsPLIT = Str.Split(":")
                data += "{name:   '"
                data += dt.Rows(i)("EntityName")
                data += "',  y: "
                data += dt.Rows(i)("Percentage").ToString
                data += "},"
            Next
            'data += "]"
        End If
    End Sub

#End Region

End Class
