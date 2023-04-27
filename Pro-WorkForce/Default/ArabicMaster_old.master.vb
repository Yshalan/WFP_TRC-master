Imports SmartV.UTILITIES
Imports System.Data
Imports SmartV.Version
Imports SmartV.Security.MENU
Imports TA.Employees
Imports TA.Admin
Imports TA.Security

Partial Class Default_ArabicMaster
    Inherits System.Web.UI.MasterPage
    Public textalign As String
    Private objSYSUsers As SYSUsers
    Protected dir As String
    Public Lang As String
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Dim objEmp_Managers As Employee_Manager
    Dim objEmp_DeputyManager As Emp_DeputyManager
    Dim objEmp_HR As Emp_HR

    Public Property CountVisibleModules() As Integer
        Get
            Return ViewState("CountVisibleModules")
        End Get
        Set(ByVal value As Integer)
            ViewState("CountVisibleModules") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (SessionVariables.CultureInfo Is Nothing) Then
            SessionVariables.CultureInfo = "en-US"
            dir = "ltr"
            textalign = "left"
            Lang = "en"
        Else
            If SessionVariables.CultureInfo = "en-US" Then
                SessionVariables.CultureInfo = "en-US"
                dir = "ltr"
                textalign = "left"
                Lang = "en"
            Else
                SessionVariables.CultureInfo = "ar-JO"
                dir = "rtl"
                textalign = "right"
                Lang = "ar"
            End If
        End If

        Dim showSmartTimeLogo As Boolean = CBool(ConfigurationManager.AppSettings("ShowSmartTimeLogo"))
        If showSmartTimeLogo Then
            imgLogo.ImageUrl = "../images/logo.jpg"
        Else
            imgLogo.ImageUrl = "~/ShowImage.ashx"
        End If

        If Not Page.IsPostBack Then
            lbtnAdmin_1_2_3_1.Visible = IsLinkVisible(lbtnAdmin_1_2_3_1.ID)
            lbtnDefinitions_1_2_3_2.Visible = IsLinkVisible(lbtnDefinitions_1_2_3_2.ID)
            lbtnEmployee_1_2_3_6.Visible = IsLinkVisible(lbtnEmployee_1_2_3_6.ID)
            lbtnDailyTasks_1_2_3_3.Visible = IsLinkVisible(lbtnDailyTasks_1_2_3_3.ID)
            lbtnSelfServices_2_3_8.Visible = IsLinkVisible(lbtnSelfServices_2_3_8.ID)
            lbtnRequests_2_3_9.Visible = IsLinkVisible(lbtnRequests_2_3_9.ID)
            lbtnDashBoard_1_2_3_7.Visible = IsLinkVisible(lbtnDashBoard_1_2_3_7.ID)
            lbtnReports_1_2_3_4.Visible = IsLinkVisible(lbtnReports_1_2_3_4.ID)
            lbtnSecurity_1_2_3_5.Visible = IsLinkVisible(lbtnSecurity_1_2_3_5.ID)

            If Not lbtnRequests_2_3_9.Visible Then
                Dim emp_ManagerID As String = SessionVariables.LoginUser.FK_EmployeeId

                objEmp_Managers = New Employee_Manager()
                With objEmp_Managers
                    .FK_ManagerId = emp_ManagerID
                    objEmp_Managers = .GetEmployeeManagerByManagerID()
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

                If (Not objEmp_Managers Is Nothing) Then
                    lbtnRequests_2_3_9.Visible = True
                ElseIf Not objEmp_DeputyManager Is Nothing Then
                    If (objEmp_DeputyManager.ToDate = DateTime.MinValue) Or (objEmp_DeputyManager.ToDate > DateTime.Now AndAlso objEmp_DeputyManager.FromDate < DateTime.Now) Then
                        lbtnRequests_2_3_9.Visible = True
                    End If
                ElseIf Not objEmp_HR Is Nothing Then
                    lbtnRequests_2_3_9.Visible = True
                End If

            End If

            'If CountVisibleModules = 1 Then
            '    lbtnAdmin_1_2_3_1.Visible = False
            '    lbtnDefinitions_1_2_3_2.Visible = False
            '    lbtnEmployee_1_2_3_6.Visible = False
            '    lbtnDailyTasks_1_2_3_3.Visible = False
            '    lbtnSelfServices_2_3_8.Visible = False
            '    lbtnRequests_2_3_9.Visible = False
            '    lbtnDashBoard_1_2_3_7.Visible = False
            '    lbtnReports_1_2_3_4.Visible = False
            '    lbtnSecurity_1_2_3_5.Visible = False
            'End If
        End If
        FillUserInfo()
    End Sub

    Function IsLinkVisible(ByVal linkId As String) As Boolean
        Dim VersionType As Integer
        VersionType = version.GetVersionType()
        Dim ModuleDT As DataTable
        Dim BuildMenu As New SmartV.Security.MENU.SYSMenuCreator

        Dim blnIsVersionVisible As Boolean
        Dim blnWithAccessVisible As Boolean
        If SessionVariables.LoginUser Is Nothing Then
            Response.Redirect("~/default/Login.aspx")
        End If
        ModuleDT = BuildMenu.BuildLeftMenu(SessionVariables.LoginUser.GroupId, 0, 0, 1, VersionType)
        Dim i As Integer
        For i = 0 To ModuleDT.Rows.Count - 1
            Dim str As String() = linkId.Split("_")

            For j = 1 To str.Length - 1
                If (str(j) = VersionType) Then
                    blnIsVersionVisible = True
                End If

            Next
        Next
        For i = 0 To ModuleDT.Rows.Count - 1
            Dim str As String() = linkId.Split("_")
            If (str(str.Length - 1) = ModuleDT.Rows(i)("ModuleID")) Then
                blnWithAccessVisible = True
                CountVisibleModules += 1
            End If
        Next
        ''
        Dim str2 As String() = linkId.Split("_")
        Dim strVersionModules As String
        strVersionModules = version.GetModulesPerVersion(VersionType)

        Dim curModule As String
        curModule = "," + str2(str2.Length - 1) + ","

        If InStr(strVersionModules, curModule) > 0 Then
            blnIsVersionVisible = True
        Else
            blnIsVersionVisible = False
        End If
        ''
        If blnIsVersionVisible And blnWithAccessVisible Then
            Return True
        Else
            Return False
        End If

    End Function

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        If SessionVariables.LoginUser Is Nothing Then
            Response.Redirect("~/default/Logout.aspx")
        End If

        If (SessionVariables.CultureInfo Is Nothing) Then
            SessionVariables.CultureInfo = "en-US"
            dir = "ltr"
            textalign = "left"
        Else
            If SessionVariables.CultureInfo = "en-US" Then
                SessionVariables.CultureInfo = "en-US"
                dir = "ltr"
                textalign = "left"
            Else
                SessionVariables.CultureInfo = "ar-JO"
                dir = "rtl"
                textalign = "right"
            End If
        End If

    End Sub

    Protected Sub lbtnAdmin_1_2_3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnAdmin_1_2_3_1.Click
        SessionVariables.UserModuleId = 1
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lbtnDefinitions_1_2_3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnDefinitions_1_2_3_2.Click
        SessionVariables.UserModuleId = 2
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lbtnDailyTasks_1_2_3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnDailyTasks_1_2_3_3.Click
        SessionVariables.UserModuleId = 3
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lbtnEmployee_1_2_3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnEmployee_1_2_3_6.Click
        SessionVariables.UserModuleId = 6
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lbtnSelfServices_2_3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnSelfServices_2_3_8.Click
        SessionVariables.UserModuleId = 8
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lbtnRequests_2_3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnRequests_2_3_9.Click
        SessionVariables.UserModuleId = 9
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lbtnDashBoard_1_2_3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnDashBoard_1_2_3_7.Click
        SessionVariables.UserModuleId = 7
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lbtnReports_1_2_3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnReports_1_2_3_4.Click
        SessionVariables.UserModuleId = 4
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lbtnSecurity_1_2_3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnSecurity_1_2_3_5.Click
        SessionVariables.UserModuleId = 5
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lnkLanguage_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkLanguage.Click
        SessionVariables.CultureInfo = "en-US"
        Response.Redirect(Me.Request.Url.ToString(), True)
    End Sub

    Protected Sub FillUserInfo()
        objSYSUsers = New SYSUsers
        objSYSUsers.ID = SessionVariables.LoginUser.ID
        objSYSUsers.GetUser()
        If objSYSUsers.UserType = 1 Then
            With objSYSUsers
                '.ID = SessionVariables.LoginUser.ID
                '.GetUser()
                lblLoginUser.Text = .fullName
                lblLoginDate.Text = SessionVariables.LoginDate.ToShortDateString
                lblLoginTime.Text = SessionVariables.LoginDate.ToString("HH:mm")
            End With
        Else
            With objSYSUsers
                .ID = SessionVariables.LoginUser.ID
                .GetUser()
                lblLoginUser.Text = .fullName
                lblLoginDate.Text = Date.Now.ToShortDateString
                lblLoginTime.Text = DateTime.Now.ToString("HH:MM")
            End With
        End If
    End Sub
End Class


