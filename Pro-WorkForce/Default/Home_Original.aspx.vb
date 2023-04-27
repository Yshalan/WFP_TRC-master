Imports SmartV.UTILITIES
Imports System.Data
Imports SmartV.Version
Imports SmartV.Security.MENU
Imports TA.Security
Imports TA.Employees
Imports TA.Admin
Imports TA_Announcements
Imports System.IO

Partial Class Default_Home
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Dim objListMenu As SYSMenuCreator
    Dim objSys_Modules As SYSModules
    Dim objEmp_Managers As Employee_Manager
    Dim objEmp_DeputyManager As Emp_DeputyManager
    Dim objEmp_HR As Emp_HR
    Private objSYSUsers As SYSUsers
    Private objAPP_Settings As APP_Settings
    Private objAnnouncements As Announcements
    Dim objSmartSecurity As New SmartSecurity
    Private objOrgCompany As OrgCompany
    Private objEmployee As Employee
    Private objSYSGroups As SYSGroups
    Private objSYS_Users_Security As SYS_Users_Security
    Private objSYSForms As SYSForms
    Private objSlider_Images As Slider_Images

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

    Private Property ID() As Integer
        Get
            Return ViewState("ID")
        End Get
        Set(ByVal value As Integer)
            ViewState("ID") = value
        End Set
    End Property

    Private Property FromLogin() As Boolean
        Get
            Return ViewState("FromLogin")
        End Get
        Set(ByVal value As Boolean)
            ViewState("FromLogin") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If SessionVariables.LicenseDetails Is Nothing Then
            Response.Redirect("~/default/Logout.aspx")
        ElseIf SessionVariables.LoginUser Is Nothing Then
            Response.Redirect("~/default/Logout.aspx")
        End If
        ApplyDefaultTheme()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            FromLogin = IIf(Page.Request.QueryString("FromLogin") Is Nothing, 0, Page.Request.QueryString("FromLogin"))
            CountVisibleModules = 0

            lnkbtnAdministration.Visible = IsLinkVisible(1)
            liAdmin.Visible = IsLinkVisible(1)
            lnkbtnDefinitions.Visible = IsLinkVisible(2)
            liDefinitions.Visible = IsLinkVisible(2)
            lnkbtnEmployees.Visible = IsLinkVisible(6)
            liEmployees.Visible = IsLinkVisible(6)
            lnkbtnDailyTask.Visible = IsLinkVisible(3)
            liDailyTask.Visible = IsLinkVisible(3)
            lnkbtnEmpSelfService.Visible = IsLinkVisible(8)
            liSelfService.Visible = IsLinkVisible(8)
            lnkbtnEmprequest.Visible = IsLinkVisible(9)
            liEmprequest.Visible = IsLinkVisible(9)
            lnkbtnDashboards.Visible = IsLinkVisible(7)
            liDashboards.Visible = IsLinkVisible(7)
            lnkbtnReports.Visible = IsLinkVisible(4)
            liReports.Visible = IsLinkVisible(4)
            lnkbtnSecurity.Visible = IsLinkVisible(5)
            liSecurity.Visible = IsLinkVisible(5)
            lnkbtnHR.Visible = IsLinkVisible(10)
            liHR.Visible = IsLinkVisible(10)
            lnkbtnSchedule.Visible = IsLinkVisible(11)
            liSchedule.Visible = IsLinkVisible(11)
            lnkbtnTaskManagement.Visible = IsLinkVisible(12)
            liTaskManagement.Visible = IsLinkVisible(12)
            lnkbtnAppraisal.Visible = IsLinkVisible(13)
            liAppraisal.Visible = IsLinkVisible(13)

            FillListMenu()

            SelectDefaultPage()

            HideShow()
            FillUserInfo()
            FillModules()
            FillData()
            'lblCopyRight.Text = objSmartSecurity.strSmartDecrypt(ConfigurationManager.AppSettings("CopyRightEn").ToString(), "SmartVision")
            lblCopyRight.Text = SmartV.Version.version.GetEnCopyRights
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
            FillSlides()
        End If
        FillRequestsCount()

    End Sub

    Protected Sub lnkbtnLanguage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnLanguage.Click
        SessionVariables.CultureInfo = "ar-JO"
        Response.Redirect("../default/homearabic.aspx")
    End Sub

    Protected Sub lnkbtnLogOut_Click(sender As Object, e As EventArgs) Handles lnkbtnLogOut.Click
        Response.Redirect("../default/logout.aspx")
    End Sub

    'Protected Sub dlstEngMenu_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlstEngMenu.ItemDataBound
    '    Dim lblModuleIdId As Label
    '    Dim lblDiv As Label
    '    Dim btnShow As LinkButton
    '    CountVisibleModules += 1
    '    lblModuleIdId = CType(e.Item.FindControl("lblId"), Label)
    '    lblDiv = CType(e.Item.FindControl("lblDiv"), Label)
    '    btnShow = CType(e.Item.FindControl("btnShow"), LinkButton)

    '    VisibleModulesID = lblModuleIdId.Text
    '    btnShow.Text = "<div ID='" + lblDiv.Text + "' ></div> "


    'End Sub

    Protected Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lstItem As DataListItem = CType(sender, LinkButton).Parent

            ''Dim strModuleId As Label = CType(dlstEngMenu.Items(lstItem.ItemIndex).FindControl("lblId"), Label)
            'SessionVariables.UserModuleId = strModuleId.Text

            Response.Redirect("../Default/Inner.aspx")

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkbtnAdministration_Click(sender As Object, e As EventArgs) Handles lnkbtnAdministration.Click
        'Administration

        SessionVariables.UserModuleId = 1
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lnkbtnDefinitions_Click(sender As Object, e As EventArgs) Handles lnkbtnDefinitions.Click
        'Definitions

        SessionVariables.UserModuleId = 2
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lnkbtnDailyTask_Click(sender As Object, e As EventArgs) Handles lnkbtnDailyTask.Click
        'Daily Tasks

        SessionVariables.UserModuleId = 3
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lnkbtnReports_Click(sender As Object, e As EventArgs) Handles lnkbtnReports.Click
        'Reports

        SessionVariables.UserModuleId = 4
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lnkbtnSecurity_Click(sender As Object, e As EventArgs) Handles lnkbtnSecurity.Click
        'Security

        SessionVariables.UserModuleId = 5
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lnkbtnEmployees_Click(sender As Object, e As EventArgs) Handles lnkbtnEmployees.Click
        'Employee

        SessionVariables.UserModuleId = 6
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lnkbtnDashboards_Click(sender As Object, e As EventArgs) Handles lnkbtnDashboards.Click
        'Dash Board

        SessionVariables.UserModuleId = 7
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lnkbtnEmpSelfService_Click(sender As Object, e As EventArgs) Handles lnkbtnEmpSelfService.Click
        'Employee Self Services

        SessionVariables.UserModuleId = 8
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lnkbtnEmprequest_Click(sender As Object, e As EventArgs) Handles lnkbtnEmprequest.Click
        'Employees Requests

        SessionVariables.UserModuleId = 9
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lnkbtnHR_Click(sender As Object, e As EventArgs) Handles lnkbtnHR.Click
        'HR
        'Session("IsActive")=10
        SessionVariables.UserModuleId = 10
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lnkbtnSchedule_Click(sender As Object, e As EventArgs) Handles lnkbtnSchedule.Click
        'HR
        'Session("IsActive")=10
        SessionVariables.UserModuleId = 11
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lnkbtnTaskManagement_Click(sender As Object, e As EventArgs) Handles lnkbtnTaskManagement.Click
        'HR
        'Session("IsActive")=10
        SessionVariables.UserModuleId = 12
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub lnkbtnAppraisal_Click(sender As Object, e As EventArgs) Handles lnkbtnAppraisal.Click
        'HR
        'Session("IsActive")=10
        SessionVariables.UserModuleId = 13
        Response.Redirect("../Default/Inner.aspx")
    End Sub

    Protected Sub repAnnouncement_ItemDataBound(ByVal source As Object, ByVal e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim DI As Object = e.Item.DataItem
            Dim CurrentDate As DateTime = DataBinder.Eval(DI, "AnnouncementDate")
            Dim myMonth As String = CurrentDate.Month.ToString()

            Dim monthNo As Integer = Convert.ToInt32(myMonth)
            Dim lblMonth As Label = CType(e.Item.FindControl("lblMonth"), Label)
            Dim fullMonth As String = System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.GetMonthName(monthNo).ToString()
            lblMonth.Text = fullMonth.Substring(0, 3)
            Dim lblDay As Label = CType(e.Item.FindControl("lblDate"), Label)
            lblDay.Text = CurrentDate.Day.ToString()
        End If
    End Sub

#End Region

#Region "Methods"

    Private Sub FillListMenu()
        Try
            objListMenu = New SYSMenuCreator
            Dim dt As New DataTable
            Dim VersionType As Integer
            Dim VersionModules As String
            Dim strMACAddress As String
            Dim strLicMACAddress As String

            Dim objSec As New STSupremeKeyValidation.STSupremeKeyValidation.SmartKeyValidation
            Dim LicModulesDT As DataTable
            Dim StrForms As String = SessionVariables.LicenseDetails.FormIds
            Dim BuildMenu As New SmartV.Security.MENU.SYSMenuCreator

            VersionType = version.GetVersionType()
            'VersionModules = version.GetModulesPerVersion(VersionType)
            VersionModules = version.GetModulesPerLicense()
            ''Un commit Before Release
            Try
                strLicMACAddress = version.GetMACAddress()
                strMACAddress = objSec.GetMacAddress
                'If strMACAddress <> strLicMACAddress Then
                '    VersionType = 0
                '    Response.Redirect("~/Default/logout.aspx")
                'End If
            Catch ex As Exception
                Response.Redirect("~/Default/logout.aspx")
            End Try
            ''Un commit Before Release
            If SessionVariables.LoginUser Is Nothing Then
                Response.Redirect("~/default/Login.aspx")
            Else
                LicModulesDT = BuildMenu.GetModuleByFormId(StrForms)
                dt = objListMenu.BuildListMenu(SessionVariables.LoginUser.ID, VersionType)

                If dt.Select("ModuleId = 9").Length < 0 Then

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

                    If Not objEmp_Managers Is Nothing Then
                        objSys_Modules = New SYSModules()
                        With objSys_Modules
                            .ModuleID = 9
                            .GetByPK()
                            Dim newRow As DataRow = dt.NewRow()
                            newRow("ModuleID") = .ModuleID
                            newRow("Desc_Ar") = .ArabicName
                            newRow("Desc_En") = .EnglishName
                            newRow("Packages") = .Packages
                            newRow("div") = .div
                            newRow("Seq") = .Seq
                            dt.Rows.Add(newRow)
                        End With
                        lnkbtnEmprequest.Visible = True
                        CountVisibleModules = CountVisibleModules + 1
                    ElseIf Not objEmp_DeputyManager Is Nothing Then
                        If (objEmp_DeputyManager.ToDate = DateTime.MinValue) Or (objEmp_DeputyManager.ToDate > DateTime.Now AndAlso objEmp_DeputyManager.FromDate < DateTime.Now) Then
                            objSys_Modules = New SYSModules()
                            With objSys_Modules
                                .ModuleID = 9
                                .GetByPK()
                                Dim newRow As DataRow = dt.NewRow()
                                newRow("ModuleID") = .ModuleID
                                newRow("Desc_Ar") = .ArabicName
                                newRow("Desc_En") = .EnglishName
                                newRow("Packages") = .Packages
                                newRow("div") = .div
                                newRow("Seq") = .Seq
                                dt.Rows.Add(newRow)
                            End With
                            lnkbtnEmprequest.Visible = True
                            CountVisibleModules = CountVisibleModules + 1
                        End If
                    ElseIf Not objEmp_HR Is Nothing Then
                        objSys_Modules = New SYSModules()
                        With objSys_Modules
                            .ModuleID = 9
                            .GetByPK()
                            Dim newRow As DataRow = dt.NewRow()
                            newRow("ModuleID") = .ModuleID
                            newRow("Desc_Ar") = .ArabicName
                            newRow("Desc_En") = .EnglishName
                            newRow("Packages") = .Packages
                            newRow("div") = .div
                            newRow("Seq") = .Seq
                            dt.Rows.Add(newRow)
                            lnkbtnEmprequest.Visible = True
                            CountVisibleModules = CountVisibleModules + 1
                        End With
                    End If

                End If
            End If


            Dim dv As New DataView(dt)

            dv.RowFilter = " '" + VersionModules + "'  like '%,' + ModuleId +  ',%'"


            'dlstEngMenu.DataSource = dv
            'dlstEngMenu.DataBind()


        Catch ex As Exception

        End Try
    End Sub

    Function IsLinkVisible(ByVal linkId As String) As Boolean
        Dim VersionType As Integer
        VersionType = version.GetVersionType()
        Dim LicModulesDT As DataTable
        Dim ModuleDT As DataTable
        Dim StrForms As String = SessionVariables.LicenseDetails.FormIds
        Dim BuildMenu As New SmartV.Security.MENU.SYSMenuCreator
        Dim blnIsVersionVisible As Boolean
        Dim blnWithAccessVisible As Boolean
        If SessionVariables.LoginUser Is Nothing Then
            Response.Redirect("~/default/Login.aspx")
        End If
        LicModulesDT = BuildMenu.GetModuleByFormId(StrForms)
        '----------Check if the form is in version-----------'
        ModuleDT = BuildMenu.BuildLeftMenu(SessionVariables.LoginUser.GroupId, 0, 0, 1, VersionType)
        For Each ModuleRow In ModuleDT.Rows
            For Each LicRow In LicModulesDT.Rows
                If ModuleRow("ModuleID") = LicRow("ModuleId") Then
                    blnIsVersionVisible = True
                End If
            Next
        Next
        '----------Check if the form is in version-----------'
        'Return False

        For Each ModuleRow In ModuleDT.Rows
            For Each LicRow In LicModulesDT.Rows
                If linkId = ModuleRow("ModuleID") Then '-----Check In Group Privilage
                    If linkId = LicRow("ModuleID") Then '-----Check In License Modules
                        blnWithAccessVisible = True
                        CountVisibleModules += 1
                        VisibleModulesID = ModuleRow("ModuleID")
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

    Private Sub FillModules()


        Dim StrForms As String
        Dim objSecurity As New STSupremeKeyValidation.STSupremeKeyValidation.SmartKeyValidation
        Dim BuildMenu As New SmartV.Security.MENU.SYSMenuCreator
        StrForms = SessionVariables.LicenseDetails.FormIds

        objSys_Modules = New SYSModules()
        Dim dtModule As New DataTable

        With objSys_Modules
            ' dtModule = .GetAll()
            dtModule = BuildMenu.GetModuleByFormId(StrForms)
            For i As Integer = 0 To dtModule.Rows.Count - 1
                If dtModule.Rows(i)("ModuleID") = 1 Then
                    lblAdministrator.Text = dtModule.Rows(i)("ModuleName")
                    'imgAdministrator.Src = "assets/img/menuicon/icon_1.png"
                ElseIf dtModule.Rows(i)("ModuleID") = 2 Then
                    lblDefinitions.Text = dtModule.Rows(i)("ModuleName")
                    'imgDefinitions.Src = "~/assets/img/menuicon/icon_2.png"
                ElseIf dtModule.Rows(i)("ModuleID") = 3 Then
                    lblDailytasks.Text = dtModule.Rows(i)("ModuleName")
                ElseIf dtModule.Rows(i)("ModuleID") = 4 Then
                    lblReports.Text = dtModule.Rows(i)("ModuleName")
                ElseIf dtModule.Rows(i)("ModuleID") = 5 Then
                    lblSecurity.Text = dtModule.Rows(i)("ModuleName")
                ElseIf dtModule.Rows(i)("ModuleID") = 6 Then
                    lblEmployees.Text = dtModule.Rows(i)("ModuleName")
                ElseIf dtModule.Rows(i)("ModuleID") = 7 Then
                    lblDashboards.Text = dtModule.Rows(i)("ModuleName")
                ElseIf dtModule.Rows(i)("ModuleID") = 8 Then
                    lblSelfService.Text = dtModule.Rows(i)("ModuleName")
                ElseIf dtModule.Rows(i)("ModuleID") = 9 Then
                    lblRequests.Text = dtModule.Rows(i)("ModuleName")
                ElseIf dtModule.Rows(i)("ModuleID") = 10 Then
                    lblHR.Text = dtModule.Rows(i)("ModuleName")
                ElseIf dtModule.Rows(i)("ModuleID") = 11 Then
                    lblSchedule.Text = dtModule.Rows(i)("ModuleName")
                ElseIf dtModule.Rows(i)("ModuleID") = 12 Then
                    lblSchedule.Text = dtModule.Rows(i)("ModuleName")
                ElseIf dtModule.Rows(i)("ModuleID") = 13 Then
                    lblAppraisal.Text = dtModule.Rows(i)("ModuleName")
                End If
            Next

        End With
    End Sub

    Private Sub HideShow()
        objAPP_Settings = New APP_Settings
        With objAPP_Settings
            .GetByPK()
            If .ShowThemeToUsers = False Then
                Image2.Visible = False
            Else
                Image2.Visible = True
            End If
            If .ShowAnnouncement = False Then
                divAnnouncements.Visible = False

            Else
                divAnnouncements.Visible = True
            End If
        End With
    End Sub

    Private Sub FillData()
        objAnnouncements = New Announcements()
        Dim userid As String = SessionVariables.LoginUser.LoginName()

        Dim mycdcatalog = New DataTable
        mycdcatalog = objAnnouncements.GetTop5(userid)
        With objAnnouncements


            repAnnouncement.DataSource = mycdcatalog
            repAnnouncement.DataBind()


        End With
    End Sub

    Private Sub FillRequestsCount()
        objSys_Modules = New SYSModules
        Dim dt As New DataTable
        Dim count As Integer
        If Not SessionVariables.LoginUser Is Nothing Then
            dt = objSys_Modules.GetRequestsCount(SessionVariables.LoginUser.FK_EmployeeId)
            count = dt.Rows(0)("CountRequestsManager") + dt.Rows(0)("CountRequestsHR")
            If count = 0 Then
                lblRequestsCount.Text = ""
                divrequestsCount.Visible = False
            Else
                lblRequestsCount.Text = count

            End If
        End If
    End Sub

    Private Sub ApplyDefaultTheme()
        Dim ThemeLink As String = "disabled=''"
        objAPP_Settings = New APP_Settings
        objAPP_Settings.GetByPK()
        Dim lnkControlName As String = ""
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

    Private Sub SelectDefaultPage()
        objSYS_Users_Security = New SYS_Users_Security
        objSYSForms = New SYSForms

        If FromLogin = True Then
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

            objAPP_Settings = New APP_Settings
            objAPP_Settings.GetByPK()
            objSYSGroups = New SYSGroups
            objSYSGroups.GroupId = SessionVariables.LoginUser.GroupId
            objSYSGroups.GetGroup()
            If IsManager = True Or Not objEmp_DeputyManager Is Nothing Then
                If objAPP_Settings.ManagerDefaultPage = 1 Then '---Groups Definition
                    If objSYSGroups.DefaultPage = 2 Then
                        'If CountVisibleModules = 1 Then
                        SessionVariables.UserModuleId = VisibleModulesID
                        SessionVariables.UserModuleId = 8
                        Response.Redirect("../Default/Inner.aspx")
                        'End If
                    ElseIf objSYSGroups.DefaultPage = 3 Then
                        Dim dt As DataTable
                        Dim path As String = "/Reports/SelfServices_Reports.aspx"
                        Dim words As String() = path.Split(New Char() {"/"c})
                        Dim toRemove = "/" + words(1)
                        Dim url As String = path.Replace(toRemove, "")
                        dt = objSYSForms.GetFormIDByFormPath(url, SessionVariables.LoginUser.GroupId)
                        If dt.Rows.Count > 0 Then
                            SessionVariables.UserModuleId = VisibleModulesID
                            SessionVariables.UserModuleId = 8
                            Response.Redirect("../Reports/SelfServices_Reports.aspx")
                        Else
                            Response.Redirect("~/default/home.aspx")
                        End If

                    End If
                ElseIf objAPP_Settings.ManagerDefaultPage = 2 Then '---Manager Summary Page
                    SessionVariables.UserModuleId = VisibleModulesID
                    SessionVariables.UserModuleId = 9
                    Response.Redirect("../SelfServices/Manager_Summary.aspx")
                Else
                    'If CountVisibleModules = 1 Then
                    SessionVariables.UserModuleId = VisibleModulesID
                    SessionVariables.UserModuleId = 9
                    Response.Redirect("../Default/Inner.aspx")
                    'End If
                End If
            Else
                If objSYSGroups.DefaultPage = 2 Then
                    'If CountVisibleModules = 1 Then
                    SessionVariables.UserModuleId = VisibleModulesID
                    SessionVariables.UserModuleId = 8
                    Response.Redirect("../Default/Inner.aspx")
                    'End If
                ElseIf objSYSGroups.DefaultPage = 3 Then
                    Dim dt As DataTable
                    Dim path As String = "/Reports/SelfServices_Reports.aspx"
                    Dim words As String() = path.Split(New Char() {"/"c})
                    Dim toRemove = "/" + words(1)
                    Dim url As String = path.Replace(toRemove, "")
                    dt = objSYSForms.GetFormIDByFormPath(url, SessionVariables.LoginUser.GroupId)
                    If dt.Rows.Count > 0 Then
                        SessionVariables.UserModuleId = VisibleModulesID
                        SessionVariables.UserModuleId = 8
                        Response.Redirect("../Reports/SelfServices_Reports.aspx")
                    Else
                        Response.Redirect("~/default/home.aspx")
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub FillSlides()
        objSlider_Images = New Slider_Images
        Dim dt As DataTable
        With objSlider_Images
            dt = .GetAll
            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    Dim FileExist As Boolean = True
                    Dim imagepath As String = String.Empty
                    For Each row In dt.Rows
                        imagepath = (row("ImagePath"))
                        If Not (File.Exists(imagepath)) Then
                            row.delete()
                        End If
                    Next
                    dt.AcceptChanges()

                    If dt.Rows.Count > 0 Then
                        repSlider.DataSource = dt
                        repSlider.DataBind()
                        dvDefaultSlider1.Visible = False
                        dvDefaultSlider2.Visible = False
                        dvDefaultSlider3.Visible = False
                        repSlider.Visible = True
                        toggleSlider.Visible = True
                    Else
                        dvDefaultSlider1.Visible = True
                        dvDefaultSlider2.Visible = True
                        dvDefaultSlider3.Visible = True
                        repSlider.Visible = False
                        toggleSlider.Visible = False
                    End If

                Else
                    dvDefaultSlider1.Visible = True
                    dvDefaultSlider2.Visible = True
                    dvDefaultSlider3.Visible = True
                    repSlider.Visible = False
                    toggleSlider.Visible = False
                End If
            Else
                dvDefaultSlider1.Visible = True
                dvDefaultSlider2.Visible = True
                dvDefaultSlider3.Visible = True
                repSlider.Visible = False
                toggleSlider.Visible = False
            End If
        End With
    End Sub

#End Region


End Class
