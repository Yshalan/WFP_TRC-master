Imports SmartV.UTILITIES
Imports SmartV.Version
Imports System.Data
Imports TA.Security
Imports TA.Admin
Imports TA.Employees
Imports TA.DashBoard
Imports System.Web.Services
Imports System.Web.Script.Serialization

Partial Class Default_Inner
    Inherits System.Web.UI.Page

    Private objVersion As version
    Private Lang As CtlCommon.Lang
    Private objSYSForms As SYSForms
    Private objSYS_Users_Security As SYS_Users_Security
    Dim objEmp_Managers As Employee_Manager
    Dim objEmp_DeputyManager As Emp_DeputyManager
    Dim objEmp_HR As Emp_HR
    Dim objSYSGroups As SYSGroups
    Dim objAPP_Settings As APP_Settings

#Region "Properties"
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
    Public Property dtDrillDown() As DataTable
        Get
            Return ViewState("dtDrillDown")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtDrillDown") = value
        End Set
    End Property
    Public Property dashid() As Integer
        Get
            Return ViewState("dashid")
        End Get
        Set(ByVal value As Integer)
            ViewState("dashid") = value
        End Set
    End Property
#End Region

#Region "Page Events"

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        If (SessionVariables.CultureInfo Is Nothing) Then
            SessionVariables.CultureInfo = "en-US"
            Lang = CtlCommon.Lang.EN
        Else
            If SessionVariables.CultureInfo = "en-US" Then
                SessionVariables.CultureInfo = "en-US"
                Lang = CtlCommon.Lang.EN
                Page.Culture = "en-US"
                Page.UICulture = "en-US"
            Else
                SessionVariables.CultureInfo = "ar-JO"
                Lang = CtlCommon.Lang.AR
                Page.Culture = "ar-JO"
                Page.UICulture = "ar-JO"
            End If
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If (SessionVariables.CultureInfo Is Nothing) Then
            SessionVariables.CultureInfo = "en-US"
            Lang = CtlCommon.Lang.EN
        Else
            If SessionVariables.CultureInfo = "en-US" Then
                SessionVariables.CultureInfo = "en-US"
                Lang = CtlCommon.Lang.EN
            Else
                SessionVariables.CultureInfo = "ar-JO"
                Lang = CtlCommon.Lang.AR
            End If
        End If
        If Not Page.IsPostBack Then

            'If SessionVariables.UserModuleId = 1 Then
            '    Response.Redirect("../Admin/About.aspx")
            'Else
            'End If
            'If SessionVariables.UserModuleId = 2 Then
            '    DefinitionsSummary1.Visible = True
            'ElseIf SessionVariables.UserModuleId = 5 Then
            '    SecuritySummary1.Visible = True
            'ElseIf SessionVariables.UserModuleId = 6 Then
            '    EmployeeSummary1.Visible = True
            'Else
            '    DefinitionsSummary1.Visible = False
            '    EmployeeSummary1.Visible = False
            '    SecuritySummary1.Visible = False
            'End If
        End If

    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        If SessionVariables.LoginUser Is Nothing Then
            Response.Redirect("~/default/Logout.aspx")
        End If

        If SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
            Page.MasterPageFile = "~/default/ArabicMaster.master"
            Page.Culture = "ar-JO"
            Page.UICulture = "ar-JO"
        Else
            Lang = CtlCommon.Lang.EN
            Page.MasterPageFile = "~/default/NewMaster.master"
            Page.Culture = "en-US"
            Page.UICulture = "en-US"
        End If

        Dim moduleID As Integer = SessionVariables.UserModuleId
        Dim objSysModules As New SYSModules
        With objSysModules
            .ModuleID = moduleID
            .GetByPK()

            If (SessionVariables.CultureInfo = "en-US") Then
                Page.Title = "Work Force Pro : :" + .EnglishName
            Else
                Page.Title = "Work Force Pro : :" + .ArabicName
            End If
        End With

        'Uncommented code for moduleid =8 and moduleid=9 by kiran : 15-05-2017
        objSYS_Users_Security = New SYS_Users_Security
        objSYSForms = New SYSForms
        Dim dt As DataTable
        Dim dt2 As DataTable
        Dim dt3 As DataTable
        Dim dt4 As DataTable
        Dim path As String = "/selfservices/EmployeeViolations.aspx"
        Dim path2 As String = "/Requests/DM_EmployeeRequests.aspx"
        Dim path3 As String = "/SelfServices/Manager_Summary.aspx"
        Dim path4 As String = "../Reports/EmpSelf_Reports_Manager.aspx"

        Dim words As String() = path.Split(New Char() {"/"c})
        Dim words2 As String() = path2.Split(New Char() {"/"c})
        Dim words3 As String() = path3.Split(New Char() {"/"c})
        Dim words4 As String() = path4.Split(New Char() {"/"c})

        Dim toRemove = "/" + words(1)
        Dim toremove2 = "/" + words2(1)
        Dim toremove3 = "/" + words3(1)
        Dim toremove4 = "/" + words4(1)

        Dim url As String = path.Replace(toRemove, "")
        Dim url2 As String = path2.Replace(toRemove, "")
        Dim url3 As String = path3.Replace(toRemove, "")
        Dim url4 As String = path4.Replace(toRemove, "")

        dt = objSYSForms.GetFormIDByFormPath(url, SessionVariables.LoginUser.GroupId)
        dt2 = objSYSForms.GetFormIDByFormPath(url2, SessionVariables.LoginUser.GroupId)
        dt3 = objSYSForms.GetFormIDByFormPath(url3, SessionVariables.LoginUser.GroupId)
        dt4 = objSYSForms.GetFormIDByFormPath(url4, SessionVariables.LoginUser.GroupId)

        'If Not objSYS_Users_Security.VerifyIfUserHasRights(url) Then
        '    If (Not url = "/Default/Inner.aspx") Then
        '        Response.Redirect("~/default/home.aspx")
        '    End If
        'End If

        Dim emp_ManagerID As String = SessionVariables.LoginUser.FK_EmployeeId

        objEmp_Managers = New Employee_Manager()
        Dim IsManager As Boolean
        With objEmp_Managers
            .FK_ManagerId = emp_ManagerID
            IsManager = .CheckIsManager()
        End With

        objEmp_DeputyManager = New Emp_DeputyManager()
        With objEmp_DeputyManager
            .FK_DeputyManagerId = emp_ManagerID
            objEmp_DeputyManager = .GetByDeputyManager()
        End With

        objEmp_HR = New Emp_HR()
        With objEmp_HR
            .HREmployeeId = emp_ManagerID
            objEmp_HR = .GetByPK()
        End With

        If moduleID = 8 And dt.Rows.Count > 0 Then
            objSYSGroups = New SYSGroups
            objSYSGroups.GroupId = SessionVariables.LoginUser.GroupId
            objSYSGroups.GetGroup()
            If objSYSGroups.DefaultPage = 3 Then
                Response.Redirect("../Reports/SelfServices_Reports.aspx")
            Else
                Response.Redirect("../selfservices/EmployeeViolations.aspx")
            End If
        End If


        If moduleID = 9 Then
            If IsManager Then

                objAPP_Settings = New APP_Settings
                objAPP_Settings.GetByPK()

                If dt2.Rows.Count > 0 And objAPP_Settings.ManagerDefaultPage = 0 Then
                    Response.Redirect("../Requests/DM_EmployeeRequests.aspx")
                ElseIf dt3.Rows.Count > 0 And objAPP_Settings.ManagerDefaultPage = 2 Then
                    Response.Redirect("../SelfServices/Manager_Summary.aspx")
                ElseIf dt4.Rows.Count > 0 And objAPP_Settings.ManagerDefaultPage = 3 Then
                    Response.Redirect("../Reports/EmpSelf_Reports_Manager.aspx")
                Else
                    Response.Redirect("../Requests/DM_EmployeeRequests.aspx")
                End If
            ElseIf Not objEmp_DeputyManager Is Nothing Then
                Response.Redirect("../Requests/DM_EmployeeRequests.aspx")
            ElseIf Not objEmp_HR Is Nothing Then
                Response.Redirect("../Requests/HR_EmployeeRequests.aspx")
            Else
                Response.Redirect("../Requests/DM_EmployeeRequests.aspx")
            End If
        End If

    End Sub

#End Region

#Region "Methods"

    <WebMethod>
    Public Shared Function GetGroupUsers() As String
        Dim list As List(Of Dictionary(Of String, String)) = New List(Of Dictionary(Of String, String))
        Dim GroupName As Dictionary(Of String, String)
        Dim objJavaScriptSerializer As JavaScriptSerializer = New JavaScriptSerializer
        Dim dt As DataTable
        Dim Lang As String = SessionVariables.CultureInfo

        Dim _DefUsers As SYSUsers = New SYSUsers

        dt = _DefUsers.GetActive_UsersCount()

        For Each row As DataRow In dt.Rows
            GroupName = New Dictionary(Of String, String)
            GroupName.Add("GroupName", IIf(Lang = "ar-JO", row("Desc_Ar"), row("Desc_En")))
            GroupName.Add("UserCount", row("UserCount"))

            list.Add(GroupName)
        Next
        Return objJavaScriptSerializer.Serialize(list)
    End Function

    <WebMethod>
    Public Shared Function GetEmployees_Entities() As String
        Dim list As List(Of Dictionary(Of String, String)) = New List(Of Dictionary(Of String, String))
        Dim Employees As Dictionary(Of String, String)
        Dim objJavaScriptSerializer As JavaScriptSerializer = New JavaScriptSerializer
        Dim dt As DataTable
        Dim Lang As String = SessionVariables.CultureInfo

        Dim objEmployee As Employee = New Employee
        objEmployee.FK_CompanyId = SessionVariables.LoginUser.FK_CompanyId
        dt = objEmployee.GetActive_EmployeeCount

        For Each row As DataRow In dt.Rows
            Employees = New Dictionary(Of String, String)
            Employees.Add("EntityName", IIf(Lang = "ar-JO", row("EntityArabicName"), row("EntityName")))
            Employees.Add("EmployeeCount", row("EmployeeCount"))

            list.Add(Employees)
        Next
        Return objJavaScriptSerializer.Serialize(list)
    End Function

#End Region
    

End Class
