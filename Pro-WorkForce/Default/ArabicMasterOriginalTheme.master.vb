Imports SmartV.UTILITIES
Imports System.Data
Imports SmartV.Version
Imports SmartV.Security.MENU
Imports System.IO
Imports TA.Employees
Imports TA.Admin
Imports TA.Security

Partial Class Default_ArabicMaster
    Inherits System.Web.UI.MasterPage

#Region "Class Variables"
    Private objSYSUsers As SYSUsers
    Private objSYS_Users_Security As SYS_Users_Security
    Public textalign As String
    Protected dir As String
    Public Lang As String
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Dim objEmp_Managers As Employee_Manager
    Dim objEmp_DeputyManager As Emp_DeputyManager
    Dim objEmp_HR As Emp_HR
    Dim objSys_Modules As SYSModules
    Private objAPP_Settings As APP_Settings
    Private objOrgCompany As OrgCompany
    Private objSYSForms As SYSForms
    Private objEmployee As Employee

#End Region

#Region "Properties"

    Public Property CountVisibleModules() As Integer
        Get
            Return ViewState("CountVisibleModules")
        End Get
        Set(ByVal value As Integer)
            ViewState("CountVisibleModules") = value
        End Set
    End Property

    Public Property VisibleModulesID() As Integer
        Get
            Return ViewState("VisibleModulesID")
        End Get
        Set(ByVal value As Integer)
            ViewState("VisibleModulesID") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        If SessionVariables.LoginUser Is Nothing Then
            Response.Redirect("~/default/Logout.aspx")
        End If

        ' Remark : Check if the user has rights to open using url
        Dim path As String = HttpContext.Current.Request.Url.PathAndQuery
        If path.Contains("/SelfServices/PermissionRequest.aspx?Id=1") Then ' ---Permission Request view button postback solution
            path = HttpContext.Current.Request.Url.AbsolutePath
        ElseIf path.Contains("/SelfServices/NursingPermissionRequest.aspx?Id=1") Then
            path = HttpContext.Current.Request.Url.AbsolutePath
        ElseIf path.Contains("/SelfServices/StudyPermissionRequest.aspx?Id=1") Then
            path = HttpContext.Current.Request.Url.AbsolutePath
        ElseIf path.Contains("/SelfServices/ManualEntryRequest.aspx?Id=1") Then
            path = HttpContext.Current.Request.Url.AbsolutePath
        ElseIf path.Contains("/SelfServices/LeavesRequest.aspx?Id=1") Then
            path = HttpContext.Current.Request.Url.AbsolutePath
        ElseIf path.Contains("/SelfServices/UpdateTransactionsRequest.aspx?Id=1") Then
            path = HttpContext.Current.Request.Url.AbsolutePath
        ElseIf path.Contains("/employee/Permissions.aspx?EmployeeId=") Then
            path = HttpContext.Current.Request.Url.AbsolutePath
        ElseIf path.Contains("/employee/EmpLeave_New2.aspx?EmployeeId=") Then
            path = HttpContext.Current.Request.Url.AbsolutePath
        ElseIf path.Contains("/TaskManagement/Tasks_Details.aspx?ProjectId=") Then
            path = HttpContext.Current.Request.Url.AbsolutePath
        End If
        objSYS_Users_Security = New SYS_Users_Security()
        objSYSForms = New SYSForms
        objSYS_Users_Security.FK_UserId = SessionVariables.LoginUser.ID
        Dim words As String() = path.Split(New Char() {"/"c})
        Dim toRemove = "/" + words(1)
        Dim url As String = path.Replace(toRemove, "")
        Dim strForms As String = SessionVariables.LicenseDetails.FormIds
        Dim arrForms As ArrayList = SplitLicenseForms(strForms)
        Dim dt As DataTable
        Dim LicVerifiedurl As Boolean
        '------------Check if the form url in license forms-----------'
        If (Not url = "/Default/Inner.aspx") Then
            dt = objSYSForms.GetFormIDByFormPath_WithoutGroupId(url)
            Dim i As Integer
            For i = 0 To arrForms.Count - 1
                For Each row In dt.Rows
                    If row("FormId").ToString = arrForms.Item(i) Then
                        LicVerifiedurl = True
                    End If
                Next
            Next
            If LicVerifiedurl = False Then
                Response.Redirect("~/default/home.aspx")
            End If

        End If
        '------------Check if the form url in license forms-----------'

        '------------Check if the form url in user privilage-----------'
        If Not objSYS_Users_Security.VerifyIfUserHasRights(url) Then
            If (Not url = "/Default/Inner.aspx") Then
                Response.Redirect("~/default/home.aspx")
            End If
        End If
        '------------Check if the form url in user privilage-----------'


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
        ApplyDefaultTheme()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (SessionVariables.CultureInfo Is Nothing) Then
            SessionVariables.CultureInfo = "en-US"
            dir = "ltr"
            textalign = "left"
            Lang = "en"
            ImgLanguage.ImageUrl = "../assets/img/ArBt.png"
        Else
            If SessionVariables.CultureInfo = "en-US" Then
                SessionVariables.CultureInfo = "en-US"
                dir = "ltr"
                textalign = "left"
                Lang = "en"
                ImgLanguage.ImageUrl = "../assets/img/ArBt.png"
            Else
                SessionVariables.CultureInfo = "ar-JO"
                dir = "rtl"
                textalign = "right"
                Lang = "ar"
                ImgLanguage.ImageUrl = "../assets/img/EnBt.png"
            End If
        End If


        Dim fPath As String = String.Empty
        fPath = Server.MapPath("..\CompanyLogo\\")
        Dim myDir As DirectoryInfo = New DirectoryInfo(fPath)
        If Not (myDir.EnumerateFiles().Any()) Then
            'imgLogo.ImageUrl = "../assets/img/logo.png"
        End If


        GetIsActive()
        lbtnAdmin_1_2_3_1.Visible = IsLinkVisible(1)
        liAdmin.Visible = IsLinkVisible(1)
        lbtnDefinitions_1_2_3_2.Visible = IsLinkVisible(2)
        liDefinitions.Visible = IsLinkVisible(2)
        lbtnEmployee_1_2_3_6.Visible = IsLinkVisible(6)
        liEmployee.Visible = IsLinkVisible(6)
        lbtnDailyTasks_1_2_3_3.Visible = IsLinkVisible(3)
        liDailyTasks.Visible = IsLinkVisible(3)
        lbtnSelfServices_2_3_8.Visible = IsLinkVisible(8)
        liSelfServices.Visible = IsLinkVisible(8)
        lbtnRequests_2_3_9.Visible = IsLinkVisible(9)
        liRequests.Visible = IsLinkVisible(9)
        lbtnDashBoard_1_2_3_7.Visible = IsLinkVisible(7)
        liDashBoard.Visible = IsLinkVisible(7)
        lbtnReports_1_2_3_4.Visible = IsLinkVisible(4)
        liReports.Visible = IsLinkVisible(4)
        lbtnSecurity_1_2_3_5.Visible = IsLinkVisible(5)
        liSecurity.Visible = IsLinkVisible(5)
        lbtnHR_2_3_10.Visible = IsLinkVisible(10)
        liHr.Visible = IsLinkVisible(10)
        lbtnSchedule_2_3_11.Visible = IsLinkVisible(11)
        liSchedule.Visible = IsLinkVisible(11)
        lbtnTaskManagement_2_3_12.Visible = IsLinkVisible(12)
        liTaskManagement.Visible = IsLinkVisible(12)
        lbtnAppraisal_2_3_13.Visible = IsLinkVisible(13)
        liAppraisal.Visible = IsLinkVisible(13)
        HideTheme()

        FillUserInfo()
        FillModules()
        FillRequestsCount()


        objOrgCompany = New OrgCompany
        objOrgCompany.CompanyId = objSYSUsers.FK_CompanyId
        If objSYSUsers.FK_CompanyId <> 0 Then
            objOrgCompany.GetByPK()
        End If

        Dim showSmartTimeLogo As Boolean = CBool(ConfigurationManager.AppSettings("ShowSmartTimeLogo"))
        If showSmartTimeLogo Then

            Dim ImagePath As String = "~/images/CompaniesLogo/" & objSYSUsers.FK_CompanyId & objOrgCompany.Logo
            Dim filePath As String = HttpContext.Current.Server.MapPath(ImagePath)
            If System.IO.File.Exists(filePath) Then
                ClientLogo.Src = ImagePath
            Else
                ClientLogo.Src = "~/ShowImage.ashx"
            End If

        Else
            ClientLogo.Visible = False
        End If

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
        '------------Check if the Module in license forms-----------'
        Dim BuildMenu As New SmartV.Security.MENU.SYSMenuCreator
        Dim StrForms As String = SessionVariables.LicenseDetails.FormIds
        Dim LicModulesDT As DataTable
        Dim ModuleExist As Boolean = False
        LicModulesDT = BuildMenu.GetModuleByFormId(StrForms)
        For Each row In LicModulesDT.Rows
            If row("ModuleId").ToString = 9 Then
                ModuleExist = True
            End If
        Next
        If ModuleExist = True Then
            If (IsManager) Then
                lbtnRequests_2_3_9.Visible = True
            ElseIf Not objEmp_DeputyManager Is Nothing Then
                If (objEmp_DeputyManager.ToDate = DateTime.MinValue) Or (objEmp_DeputyManager.ToDate > DateTime.Now AndAlso objEmp_DeputyManager.FromDate < DateTime.Now) Then
                    lbtnRequests_2_3_9.Visible = True
                End If
            Else
                lbtnRequests_2_3_9.Visible = IsLinkVisible(9)

            End If
        End If
        '------------Check if the Module in license forms-----------'
        Try

        Catch ex As Exception

        End Try

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

    Protected Sub lbtnHR_2_3_10_Click(sender As Object, e As System.EventArgs) Handles lbtnHR_2_3_10.Click

        SessionVariables.UserModuleId = 10
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lbtnSchedule_2_3_11_Click(sender As Object, e As EventArgs) Handles lbtnSchedule_2_3_11.Click
        SessionVariables.UserModuleId = 11
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lbtnTaskManagement_2_3_12_Click(sender As Object, e As EventArgs) Handles lbtnTaskManagement_2_3_12.Click
        SessionVariables.UserModuleId = 12
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lbtnAppraisal_2_3_13_Click(sender As Object, e As EventArgs) Handles lbtnAppraisal_2_3_13.Click
        SessionVariables.UserModuleId = 13
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lnkbtnLanguage_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkbtnLanguage.Click
        SessionVariables.CultureInfo = "en-US"
        Response.Redirect(Me.Request.Url.ToString(), True)
    End Sub

    Protected Sub lnkbtnLogOut_Click(sender As Object, e As EventArgs) Handles lnkbtnLogOut.Click
        Response.Redirect("../default/logout.aspx")
    End Sub

#End Region

#Region "Methods"

    Public Function SplitLicenseForms(ByVal LicenseForms As String) As ArrayList

        Dim s As String = LicenseForms
        Dim arrForms As New ArrayList
        For Each value As String In s.Split(","c)
            arrForms.Add(Convert.ToInt32(value))
        Next
        Return arrForms
    End Function

    Protected Sub FillUserInfo()
        objSYSUsers = New SYSUsers
        objEmployee = New Employee
        objSYSUsers.ID = SessionVariables.LoginUser.ID
        objSYSUsers.GetUser()
        If objSYSUsers.UserType = 1 Then
            With objSYSUsers
                '.ID = SessionVariables.LoginUser.ID
                '.GetUser()
                lblLoginUser.Text = .fullName
                lblLoginDate.Text = SessionVariables.LoginDate
            End With
        Else
            With objSYSUsers
                .ID = SessionVariables.LoginUser.ID
                .GetUser()
                lblLoginUser.Text = .fullName
                lblLoginDate.Text = SessionVariables.LoginDate
            End With
        End If
        'objEmployee.EmployeeId = objSYSUsers.FK_EmployeeId
        'objEmployee.GetByPK()
        'If Not objEmployee.EmpImagePath = Nothing Then
        '    Dim imgURL As String = Convert.ToBase64String(ConvertImageFiletoBytes(objEmployee.EmpImagePath))
        '    Dim imgSrc As String = String.Format("data:image/gif;base64,{0}", imgURL)
        '    EmpImage.Src = imgSrc
        'Else
        '    If objEmployee.Gender = "f" Then
        '        Dim imgURL As String = Convert.ToBase64String(ConvertImageFiletoBytes(Server.MapPath("../Svassets/img/user.png")))
        '        Dim imgSrc As String = String.Format("data:image/gif;base64,{0}", imgURL)
        '        EmpImage.Src = imgSrc
        '    Else
        '        Dim imgURL As String = Convert.ToBase64String(ConvertImageFiletoBytes(Server.MapPath("../Svassets/img/user.png")))
        '        Dim imgSrc As String = String.Format("data:image/gif;base64,{0}", imgURL)
        '        EmpImage.Src = imgSrc
        '    End If
        'End If


    End Sub

    Function IsLinkVisible(ByVal linkId As String) As Boolean
        Dim VersionType As Integer
        VersionType = version.GetVersionType()
        Dim ModuleDT As DataTable
        Dim LicModulesDT As DataTable

        Dim StrForms As String = SessionVariables.LicenseDetails.FormIds
        Dim BuildMenu As New SmartV.Security.MENU.SYSMenuCreator
        Dim blnIsVersionVisible As Boolean
        Dim blnWithAccessVisible As Boolean
        If SessionVariables.LoginUser Is Nothing Then
            Response.Redirect("~/default/Login.aspx")
        End If
        LicModulesDT = BuildMenu.GetModuleByFormId(StrForms)
        ModuleDT = BuildMenu.BuildLeftMenu(SessionVariables.LoginUser.GroupId, 0, 0, 1, VersionType)

        For Each Modulerow In ModuleDT.Rows
            For Each LicRow In LicModulesDT.Rows
                If Modulerow("ModuleID") = LicRow("ModuleId") Then
                    blnIsVersionVisible = True
                End If
            Next
        Next
        'Return False

        For Each Modulerow In ModuleDT.Rows
            For Each LicRow In LicModulesDT.Rows
                If linkId = Modulerow("ModuleID") Then '-----Check In Group Privilage
                    If linkId = LicRow("ModuleID") Then '-----Check In License Modules
                        blnWithAccessVisible = True
                        CountVisibleModules += 1
                        VisibleModulesID = Modulerow("ModuleID")
                    End If
                End If
            Next
        Next
        ''
        If blnIsVersionVisible And blnWithAccessVisible Then
            Return True
        Else
            Return False
        End If

    End Function

    'Function IsLinkVisible(ByVal linkId As String) As Boolean
    '    Dim VersionType As Integer
    '    VersionType = version.GetVersionType()
    '    Dim ModuleDT As DataTable
    '    Dim BuildMenu As New SmartV.Security.MENU.SYSMenuCreator

    '    Dim blnIsVersionVisible As Boolean
    '    Dim blnWithAccessVisible As Boolean
    '    If SessionVariables.LoginUser Is Nothing Then
    '        Response.Redirect("~/default/Login.aspx")
    '    End If
    '    ModuleDT = BuildMenu.BuildLeftMenu(SessionVariables.LoginUser.GroupId, 0, 0, 1, VersionType)
    '    Dim i As Integer
    '    For i = 0 To ModuleDT.Rows.Count - 1
    '        Dim str As String() = linkId.Split("_")

    '        For j = 1 To str.Length - 1
    '            If (str(j) = VersionType) Then
    '                blnIsVersionVisible = True
    '            End If

    '        Next
    '    Next
    '    For i = 0 To ModuleDT.Rows.Count - 1
    '        Dim str As String() = linkId.Split("_")
    '        If (str(str.Length - 1) = ModuleDT.Rows(i)("ModuleID")) Then
    '            blnWithAccessVisible = True
    '            CountVisibleModules += 1
    '        End If
    '    Next
    '    ''
    '    Dim str2 As String() = linkId.Split("_")
    '    Dim strVersionModules As String
    '    strVersionModules = version.GetModulesPerVersion(VersionType)

    '    Dim curModule As String
    '    curModule = "," + str2(str2.Length - 1) + ","

    '    If InStr(strVersionModules, curModule) > 0 Then
    '        blnIsVersionVisible = True
    '    Else
    '        blnIsVersionVisible = False
    '    End If

    '    If blnIsVersionVisible And blnWithAccessVisible Then
    '        Return True
    '    Else
    '        Return False
    '    End If

    'End Function

    Private Sub CheckSaveControl(ByVal objControl As Web.UI.ControlCollection, ByVal strBtnName As String, ByVal isVisible As Boolean)
        Try
            If Not objControl Is Nothing Then
                For index = 0 To objControl.Count - 1
                    Try
                        Dim strName As Control
                        strName = objControl(index)
                        If Not strName Is Nothing Then
                            Try
                                If strName.Controls.Count > 0 Then
                                    CheckSaveControl(strName.Controls, strBtnName, isVisible)
                                Else
                                    Try
                                        If strName.ID = strBtnName Then
                                            objControl(index).Visible = isVisible
                                        End If
                                    Catch ex As Exception

                                    End Try

                                End If

                            Catch ex As Exception

                            End Try
                        End If
                    Catch ex As Exception

                    End Try
                Next

            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Sub GetIsActive()
        'If SessionVariables.UserModuleId = 1 Then
        '    lbtnAdmin_1_2_3_1.CssClass = "active"
        'ElseIf SessionVariables.UserModuleId = 2 Then
        '    lbtnDefinitions_1_2_3_2.CssClass = "active"
        'ElseIf SessionVariables.UserModuleId = 3 Then
        '    lbtnDailyTasks_1_2_3_3.CssClass = "active"
        'ElseIf SessionVariables.UserModuleId = 4 Then
        '    lbtnReports_1_2_3_4.CssClass = "active"
        'ElseIf SessionVariables.UserModuleId = 5 Then
        '    lbtnSecurity_1_2_3_5.CssClass = "active"
        'ElseIf SessionVariables.UserModuleId = 6 Then
        '    lbtnEmployee_1_2_3_6.CssClass = "active"
        'ElseIf SessionVariables.UserModuleId = 7 Then
        '    lbtnDashBoard_1_2_3_7.CssClass = "active"
        'ElseIf SessionVariables.UserModuleId = 8 Then
        '    lbtnSelfServices_2_3_8.CssClass = "active"
        'ElseIf SessionVariables.UserModuleId = 9 Then
        '    lbtnRequests_2_3_9.CssClass = "active"
        'Else
        '    lbtnAdmin_1_2_3_1.CssClass = "active"
        'End If
    End Sub

    Private Sub FillModules()
        objSys_Modules = New SYSModules()
        Dim dtModule As New DataTable
        Dim StrForms As String
        Dim objSecurity As New STSupremeKeyValidation.STSupremeKeyValidation.SmartKeyValidation
        Dim BuildMenu As New SmartV.Security.MENU.SYSMenuCreator
        StrForms = SessionVariables.LicenseDetails.FormIds
        With objSys_Modules
            'dtModule = .GetAll()
            dtModule = BuildMenu.GetModuleByFormId(StrForms)
            For i As Integer = 0 To dtModule.Rows.Count - 1
                If dtModule.Rows(i)("ModuleID") = 1 Then
                    lblAdministration.Text = dtModule.Rows(i)("ModuleName")
                    'imgAdministrator.Src = "assets/img/menuicon/icon_1.png"
                ElseIf dtModule.Rows(i)("ModuleID") = 2 Then
                    lblDefinitions.Text = dtModule.Rows(i)("ModuleName")
                    'imgDefinitions.Src = "~/assets/img/menuicon/icon_2.png"
                ElseIf dtModule.Rows(i)("ModuleID") = 3 Then
                    lblDailyTasks.Text = dtModule.Rows(i)("ModuleName")
                ElseIf dtModule.Rows(i)("ModuleID") = 4 Then
                    lblReports.Text = dtModule.Rows(i)("ModuleName")
                ElseIf dtModule.Rows(i)("ModuleID") = 5 Then
                    lblSecurity.Text = dtModule.Rows(i)("ModuleName")
                ElseIf dtModule.Rows(i)("ModuleID") = 6 Then
                    lblEmployees.Text = dtModule.Rows(i)("ModuleName")
                ElseIf dtModule.Rows(i)("ModuleID") = 7 Then
                    lblDashboards.Text = dtModule.Rows(i)("ModuleName")
                ElseIf dtModule.Rows(i)("ModuleID") = 8 Then
                    lblSelfServices.Text = dtModule.Rows(i)("ModuleName")
                ElseIf dtModule.Rows(i)("ModuleID") = 9 Then
                    lblEmployeeRequest.Text = dtModule.Rows(i)("ModuleName")
                ElseIf dtModule.Rows(i)("ModuleID") = 10 Then
                    lblHR.Text = dtModule.Rows(i)("ModuleName")
                ElseIf dtModule.Rows(i)("ModuleID") = 11 Then
                    lblSchedule.Text = dtModule.Rows(i)("ModuleName")
                ElseIf dtModule.Rows(i)("ModuleID") = 12 Then
                    lblTaskMangement.Text = dtModule.Rows(i)("ModuleName")
                ElseIf dtModule.Rows(i)("ModuleID") = 13 Then
                    lblAppraisal.Text = dtModule.Rows(i)("ModuleName")
                End If
            Next

        End With
    End Sub

    Private Sub HideTheme()
        objAPP_Settings = New APP_Settings
        With objAPP_Settings
            .GetByPK()
            If .ShowThemeToUsers = False Then
                Image2.Visible = False
            Else
                Image2.Visible = True
            End If
        End With
    End Sub

    Private Sub FillRequestsCount()
        objSys_Modules = New SYSModules
        Dim dt As New DataTable
        Dim count As Integer
        If Not SessionVariables.LoginUser Is Nothing Then
            dt = objSys_Modules.GetRequestsCount(SessionVariables.LoginUser.FK_EmployeeId)
            count = dt.Rows(0)("CountRequestsManager") + dt.Rows(0)("CountRequestsHR")
            Dim MyLabel As Label = TryCast(lbtnRequests_2_3_9.FindControl("lblRequestsCount"), Label)
            If count = 0 Then
                MyLabel.Visible = False
            Else
                MyLabel.Text = count

            End If
        End If
    End Sub

    Private Sub ApplyDefaultTheme()

        objAPP_Settings = New APP_Settings
        objAPP_Settings.GetByPK()

        If objAPP_Settings.DefaultTheme = 1 Then
            head1.Controls.Remove(lnkGreenDesign)
            head1.Controls.Remove(lnkRedDesign)
            head1.Controls.Remove(lnkBlueDesign)
            head1.Controls.Remove(lnkVioletDesign)
            head1.Controls.Remove(lnkGoldDesign)
            head1.Controls.Remove(lnkADMDesign)

            head1.Controls.Add(lnkDofDesign)
            head1.Controls.Add(lnkGreenDesign)
            head1.Controls.Add(lnkRedDesign)
            head1.Controls.Add(lnkBlueDesign)
            head1.Controls.Add(lnkVioletDesign)
            head1.Controls.Add(lnkGoldDesign)
            head1.Controls.Add(lnkADMDesign)

        ElseIf objAPP_Settings.DefaultTheme = 2 Then
            head1.Controls.Remove(lnkDofDesign)
            head1.Controls.Remove(lnkRedDesign)
            head1.Controls.Remove(lnkBlueDesign)
            head1.Controls.Remove(lnkVioletDesign)
            head1.Controls.Remove(lnkGoldDesign)
            head1.Controls.Remove(lnkADMDesign)

            head1.Controls.Add(lnkGreenDesign)
            head1.Controls.Add(lnkDofDesign)
            head1.Controls.Add(lnkRedDesign)
            head1.Controls.Add(lnkBlueDesign)
            head1.Controls.Add(lnkVioletDesign)
            head1.Controls.Add(lnkGoldDesign)
            head1.Controls.Add(lnkADMDesign)

        ElseIf objAPP_Settings.DefaultTheme = 3 Then
            head1.Controls.Remove(lnkDofDesign)
            head1.Controls.Remove(lnkBlueDesign)
            head1.Controls.Remove(lnkGreenDesign)
            head1.Controls.Remove(lnkVioletDesign)
            head1.Controls.Remove(lnkGoldDesign)
            head1.Controls.Remove(lnkADMDesign)

            head1.Controls.Add(lnkRedDesign)
            head1.Controls.Add(lnkGreenDesign)
            head1.Controls.Add(lnkDofDesign)
            head1.Controls.Add(lnkBlueDesign)
            head1.Controls.Add(lnkVioletDesign)
            head1.Controls.Add(lnkGoldDesign)
            head1.Controls.Add(lnkADMDesign)

        ElseIf objAPP_Settings.DefaultTheme = 4 Then
            head1.Controls.Remove(lnkDofDesign)
            head1.Controls.Remove(lnkGreenDesign)
            head1.Controls.Remove(lnkRedDesign)
            head1.Controls.Remove(lnkVioletDesign)
            head1.Controls.Remove(lnkGoldDesign)
            head1.Controls.Remove(lnkADMDesign)

            head1.Controls.Add(lnkBlueDesign)
            head1.Controls.Add(lnkGreenDesign)
            head1.Controls.Add(lnkDofDesign)
            head1.Controls.Add(lnkRedDesign)
            head1.Controls.Add(lnkVioletDesign)
            head1.Controls.Add(lnkGoldDesign)
            head1.Controls.Add(lnkADMDesign)

        ElseIf objAPP_Settings.DefaultTheme = 5 Then
            head1.Controls.Remove(lnkDofDesign)
            head1.Controls.Remove(lnkGreenDesign)
            head1.Controls.Remove(lnkRedDesign)
            head1.Controls.Remove(lnkBlueDesign)
            head1.Controls.Remove(lnkGoldDesign)
            head1.Controls.Remove(lnkADMDesign)

            head1.Controls.Add(lnkVioletDesign)
            head1.Controls.Add(lnkBlueDesign)
            head1.Controls.Add(lnkGreenDesign)
            head1.Controls.Add(lnkDofDesign)
            head1.Controls.Add(lnkRedDesign)
            head1.Controls.Add(lnkGoldDesign)
            head1.Controls.Add(lnkADMDesign)

        ElseIf objAPP_Settings.DefaultTheme = 6 Then
            head1.Controls.Remove(lnkDofDesign)
            head1.Controls.Remove(lnkGreenDesign)
            head1.Controls.Remove(lnkRedDesign)
            head1.Controls.Remove(lnkBlueDesign)
            head1.Controls.Remove(lnkVioletDesign)
            head1.Controls.Remove(lnkADMDesign)

            head1.Controls.Add(lnkGoldDesign)
            head1.Controls.Add(lnkVioletDesign)
            head1.Controls.Add(lnkBlueDesign)
            head1.Controls.Add(lnkGreenDesign)
            head1.Controls.Add(lnkDofDesign)
            head1.Controls.Add(lnkRedDesign)
            head1.Controls.Add(lnkADMDesign)

        ElseIf objAPP_Settings.DefaultTheme = 7 Then
            head1.Controls.Remove(lnkDofDesign)
            head1.Controls.Remove(lnkGreenDesign)
            head1.Controls.Remove(lnkRedDesign)
            head1.Controls.Remove(lnkBlueDesign)
            head1.Controls.Remove(lnkVioletDesign)
            head1.Controls.Remove(lnkGoldDesign)

            head1.Controls.Add(lnkADMDesign)
            head1.Controls.Add(lnkGoldDesign)
            head1.Controls.Add(lnkVioletDesign)
            head1.Controls.Add(lnkBlueDesign)
            head1.Controls.Add(lnkGreenDesign)
            head1.Controls.Add(lnkDofDesign)
            head1.Controls.Add(lnkRedDesign)
        End If

    End Sub

    Public Function ConvertImageFiletoBytes(ByVal ImageFilePath As String) As Byte()
        Dim _tempByte() As Byte = Nothing
        If ImageFilePath <> Nothing Then
            If String.IsNullOrEmpty(ImageFilePath) = True Then
                Throw New ArgumentNullException("Image File Name Cannot be Null or Empty", "ImageFilePath")
                Return Nothing
            End If
        End If
        Try
            Dim _fileInfo As New IO.FileInfo(ImageFilePath)
            Dim _NumBytes As Long = _fileInfo.Length
            Dim _FStream As New IO.FileStream(ImageFilePath, IO.FileMode.Open, IO.FileAccess.Read)
            Dim _BinaryReader As New IO.BinaryReader(_FStream)
            _tempByte = _BinaryReader.ReadBytes(Convert.ToInt32(_NumBytes))
            _fileInfo = Nothing
            _NumBytes = 0
            _FStream.Close()
            _FStream.Dispose()
            _BinaryReader.Close()
            Return _tempByte
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

#End Region

End Class

