Imports SmartV.UTILITIES
Imports System.Data
Imports TA.Security
Imports TA.Accounts
Imports TA.Admin
Imports System.DirectoryServices
Imports SmartV.Version
Imports System.IO
Imports System.Security.Principal
Imports System.Threading
Imports System.Globalization

Partial Class Default_TALogin
    Inherits System.Web.UI.Page

    Dim securl As SecureUrl
    Private objLoginUser As SYSUsers
    Private objSecurity As SmartV.UTILITIES.SmartSecurity
    Private objAPP_Settings As APP_Settings
    Private objSmartSecurity As New SmartSecurity
    Public TextDirection As String
    Public PageLang As String
    Public PlaceHolderUname As String
    Public PlaceHolderPassword As String
    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo

    Public Property SystemUsersType() As Integer
        Get
            Return ViewState("SystemUsersType")
        End Get
        Set(ByVal value As Integer)
            ViewState("SystemUsersType") = value
        End Set
    End Property

#Region "Events"

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        ApplyDefaultTheme()
        ChangeCaptions()
        'SessionVariables.CultureInfo = ConfigurationManager.AppSettings.Get("Culture").ToString()
        Page.UICulture = ConfigurationManager.AppSettings.Get("Culture").ToString()
        Page.Culture = ConfigurationManager.AppSettings.Get("Culture").ToString()
    End Sub

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim ShowLoginForm As Boolean
        If Not Page.IsPostBack Then
            SessionVariables.CultureInfo = ConfigurationManager.AppSettings.Get("Culture").ToString()
            txtUserName.Focus()
            objAPP_Settings = New APP_Settings
            objAPP_Settings.GetByPK()
            SystemUsersType = objAPP_Settings.SystemUsersType
            ShowLoginForm = objAPP_Settings.ShowLoginForm
            If SystemUsersType = 2 Or SystemUsersType = 3 Then
                FillUserName()
                chkUsePC.Visible = True
            Else
                chkUsePC.Visible = False
            End If

            fillClientLogo()

            ''Un commit Before Release
            ChangeStatus(True)
            Dim strError As String = ""
            Try
                strError = version.CheckValidity()
                If strError <> "" Then
                    lblCustomerMsg.Text = strError
                    ChangeStatus(False)
                    Exit Sub
                End If
            Catch ex As Exception
                lblCustomerMsg.Text = strError
                If ConfigurationManager.AppSettings.Get("Culture").ToString() = "ar-JO" Then
                    lblCustomerMsg.Text = "رقم الرخصة غير صحيح، الرجاء ادخال رقم رخصة صحيح"
                Else
                    lblCustomerMsg.Text = "License Key Is Invalid, Please Enter Valid License Key"
                End If
                lnkHaveLicense.Visible = True
            End Try
            'Un commit Before Release

            '---For Mobile Detection---'

            'Dim strUserAgent As String = Request.UserAgent.ToString().ToLower()
            'If strUserAgent IsNot Nothing Then
            '    Dim MobileUrl As String = ConfigurationManager.AppSettings("MobilePath")
            '    If Request.Browser.IsMobileDevice = True OrElse strUserAgent.Contains("iphone") OrElse strUserAgent.Contains("blackberry") OrElse strUserAgent.Contains("mobile") OrElse strUserAgent.Contains("windows ce") OrElse strUserAgent.Contains("opera mini") OrElse strUserAgent.Contains("palm") Then
            '        Response.Redirect(MobileUrl)
            '    End If
            'End If
            '---For Mobile Detection---'

            ''--- For auto login for Active Directory Users ---''
            objLoginUser = New SYSUsers
            objLoginUser.UsrID = txtUserName.Text.Trim
            objLoginUser.GetAllByUserName()
            If ShowLoginForm = False And (objLoginUser.UserType = 2 Or objLoginUser.UserType = 3) Then
                chkUsePC.Checked = True
                'PCSelected()
            End If
            ''--- For auto login for Active Directory Users ---''
            HideTheme()
            'lblCopyRight.Text = objSmartSecurity.strSmartDecrypt(ConfigurationManager.AppSettings("CopyRightEn").ToString(), "SmartVision")
            If SessionVariables.CultureInfo = "ar-JO" Then
                lblCopyRight.Text = SmartV.Version.version.GetArCopyRights_Url
                lblVersionNumber.Text = SmartV.Version.version.GetVersionNumber
            Else
                lblCopyRight.Text = SmartV.Version.version.GetEnCopyRights_Url
                lblVersionNumber.Text = SmartV.Version.version.GetVersionNumber
            End If

        End If
    End Sub

    Protected Sub ChangeStatus(ByVal status As Boolean)
        login_button.Visible = status
        txtUserName.Enabled = status
        txtPassword.Enabled = status
        chkUsePC.Enabled = status
        lnkHaveLicense.Visible = Not status '---To Be Visible When License Expired---'
    End Sub

#Region "Mahak"

    Public Shared Sub SetADProperty(ByVal de As DirectoryEntry, ByVal pName As String, ByVal pValue As String)
        'First make sure the property value isnt "nothing"
        If Not pValue Is Nothing Then
            'Check to see if the DirectoryEntry contains this property already
            If de.Properties.Contains(pName) Then 'The DE contains this property
                'Update the properties value
                de.Properties(pName)(0) = pValue
            Else    'Property doesnt exist
                'Add the property and set it's value
                de.Properties(pName).Add(pValue)
            End If
        End If
    End Sub

    Public Shared Function GetDirectoryEntry() As DirectoryEntry
        Dim dirEntry As DirectoryEntry = New DirectoryEntry()
        dirEntry.Path = "LDAP://192.168.10.1/CN=Users;DC=smart-v.com"
        dirEntry.Username = "smart-v.com\SMARTV223PC"
        dirEntry.Password = "Smart@123"
        Return dirEntry
    End Function

    Public Function IsValidADLogin(ByVal loginName As String, ByVal givenName As String, ByVal surName As String) As Boolean
        Dim search As New DirectorySearcher()
        search.Filter = String.Format("(&(SAMAccountName={0}) & _(givenName={1})(sn={2}))", ExtractUserName(loginName), givenName, surName)
        search.PropertiesToLoad.Add("cn")
        search.PropertiesToLoad.Add("SAMAccountName")   'Users login name
        search.PropertiesToLoad.Add("givenName")    'Users first name
        search.PropertiesToLoad.Add("sn")   'Users last name
        'Use the .FindOne() Method to stop as soon as a match is found
        Dim result As SearchResult = search.FindOne()
        If result Is Nothing Then
            Return False
        Else
            Return True
        End If

    End Function

    Public Shared Sub SetCultureAndIdentity()
        AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal)
        Dim principal As WindowsPrincipal = CType(Thread.CurrentPrincipal, WindowsPrincipal)
        Dim identity As WindowsIdentity = CType(principal.Identity, WindowsIdentity)
        System.Threading.Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
    End Sub
    Public Function ExtractUserName(ByVal path As String) As String
        Dim userPath As String() = path.Split(New Char() {"\"c})
        Return userPath((userPath.Length - 1))
    End Function

#End Region

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles login_button.Click

        If chkUsePC.Checked Then
            PCSelected()

        Else
            If txtPassword.Text = String.Empty Or txtUserName.Text = String.Empty Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EmptyUserOrPass"), "info")
            Else
                objAPP_Settings = New APP_Settings
                objAPP_Settings.GetByPK()

                ValidateUser(SystemUsersType)
            End If
        End If


    End Sub

    'Protected Sub chkUsePC_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkUsePC.CheckedChanged
    'If chkUsePC.Checked Then
    '    Dim dt As DataTable
    '    Dim encPass As String = Nothing
    '    Dim rslt As Integer = 0
    '    objLoginUser = New SYSUsers
    '    objSecurity = New SmartV.UTILITIES.SmartSecurity

    '    If objLoginUser.ValidateConnection() = False Then
    '        CtlCommon.ShowMessage(Me.Page, "please make sure database connection")
    '        Exit Sub
    '    End If
    '    'FillUserName()

    '    encPass = "DOMAIN"
    '    Try
    '        With objLoginUser
    '            .LoginName = txtUserName.Text.Trim
    '            .UsrID = txtUserName.Text.Trim
    '            .Password = encPass
    '            dt = objLoginUser.getLogin()
    '            If (dt IsNot Nothing) AndAlso (dt.Rows.Count > 0) Then
    '                dt = objLoginUser.getActive(rslt)
    '                If rslt = 0 Then
    '                    If (dt IsNot Nothing) AndAlso (dt.Rows.Count > 0) Then
    '                        SessionVariables.LoginUser = objLoginUser
    '                        If SessionVariables.LoginUser.Active = 0 Then
    '                            If SessionVariables.CultureInfo = "en-US" Then
    '                                CtlCommon.ShowMessage(Me.Page, "User is not Active please refere to System Adminstrator")
    '                            Else
    '                                CtlCommon.ShowMessage(Me.Page, "المستخدم غير مفعل الرجاء مراجعة مسؤول النظام")
    '                            End If
    '                            Exit Sub
    '                        End If
    '                        Dim strdomainname As String = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString()
    '                        strdomainname = strdomainname.Substring(0, strdomainname.IndexOf("\"))

    '                        If .Password = "DOMAIN" Then
    '                            ' System.Configuration.ConfigurationManager.AppSettings("Domain").ToString() <> strdomainname Or
    '                            If Request.ServerVariables("LOGON_USER").Substring(Request.ServerVariables("LOGON_USER").IndexOf("\") + 1) <> txtUserName.Text.Trim Then
    '                                If SessionVariables.CultureInfo = "en-US" Then
    '                                    CtlCommon.ShowMessage(Me.Page, "Invalid user name or password")
    '                                Else
    '                                    CtlCommon.ShowMessage(Me.Page, "اسم المستخدم او كلمة المرور غير صحيحة")
    '                                End If
    '                                Return
    '                            End If
    '                        End If
    '                        'If SessionVariables.CultureInfo = "ar-JO" Then
    '                        '    Response.Redirect("../Default/Homearabic.aspx")
    '                        'Else
    '                        '    Response.Redirect("../Default/Home.aspx")
    '                        'End If
    '                        objLoginUser.ID = SessionVariables.LoginUser.ID
    '                        objLoginUser.GetUser()
    '                        If objLoginUser.DefaultSystemLang <> "" Then
    '                            SessionVariables.LoginDate = DateTime.Now
    '                            Application("TotalOnlineUsers") = CInt(Application("TotalOnlineUsers")) + 1
    '                            If objLoginUser.DefaultSystemLang = "AR" Then
    '                                SessionVariables.CultureInfo = "ar-JO"
    '                                Response.Redirect("../Default/Homearabic.aspx?FromLogin=1")
    '                            Else
    '                                SessionVariables.CultureInfo = "en-US"
    '                                Response.Redirect("../Default/Home.aspx?FromLogin=1")

    '                            End If
    '                        Else
    '                            SessionVariables.LoginDate = DateTime.Now
    '                            Application("TotalOnlineUsers") = CInt(Application("TotalOnlineUsers")) + 1
    '                            If SessionVariables.CultureInfo = "ar-JO" Then
    '                                Response.Redirect("../Default/Homearabic.aspx?FromLogin=1")
    '                            Else
    '                                Response.Redirect("../Default/Home.aspx?FromLogin=1")
    '                            End If
    '                        End If
    '                    Else
    '                        If SessionVariables.CultureInfo = "en-US" Then
    '                            CtlCommon.ShowMessage(Me.Page, "Invalid user name or password")
    '                        Else
    '                            CtlCommon.ShowMessage(Me.Page, "اسم المستخدم او كلمة المرور غير صحيحة")
    '                        End If

    '                    End If
    '                Else
    '                    If SessionVariables.CultureInfo = "en-US" Then
    '                        CtlCommon.ShowMessage(Me.Page, "Invalid user name or password")
    '                    Else
    '                        CtlCommon.ShowMessage(Me.Page, "اسم المستخدم او كلمة المرور غير صحيحة")
    '                    End If
    '                End If
    '            Else
    '                If SessionVariables.CultureInfo = "en-US" Then
    '                    CtlCommon.ShowMessage(Me.Page, "Invalid user name or password")
    '                Else
    '                    CtlCommon.ShowMessage(Me.Page, "اسم المستخدم او كلمة المرور غير صحيحة")
    '                End If
    '            End If
    '        End With

    '    Catch ex As Exception
    '        If SessionVariables.CultureInfo = "en-US" Then
    '            CtlCommon.ShowMessage(Me.Page, String.Concat("Operation Failed. \n", ex.Message))
    '        Else
    '            CtlCommon.ShowMessage(Me.Page, String.Concat("فشلت العملية.\n", ex.Message))
    '        End If
    '    Finally
    '        objLoginUser = Nothing
    '    End Try

    'End If
    'End Sub

    Protected Sub lnkHaveLicense_Click(sender As Object, e As System.EventArgs) Handles lnkHaveLicense.Click
        'Dim QueryString As String = "../Default/LicensePopUp.aspx"
        'Dim newWin As String = (Convert.ToString("window.open('") & QueryString) + "', 'popup_window', 'width=600,height=200,left=400,top=100,resizable=yes,scrollbars=1');"
        'ClientScript.RegisterStartupScript(Me.GetType(), "pop", newWin, True)
    End Sub

#End Region

#Region "Methods"

    Private Sub FillUserName()
        Try

            Dim strDomain As String = ""
            'strDomain = System.Security.Principal.WindowsIdentity.GetCurrent().Name
            strDomain = Request.ServerVariables("LOGON_USER")
            strDomain = strDomain.Substring(strDomain.IndexOf("\") + 1, strDomain.Length - strDomain.IndexOf("\") - 1)
            txtUserName.Text = strDomain

            txtPassword.Focus()
        Catch ex As Exception

        End Try
    End Sub

    Function AuthenticateUser(user As String, pass As String) As Boolean

        Try
            Dim strDomain As String = ConfigurationManager.AppSettings("Domain").ToString()
            'Dim strDomainPrefix As String = ConfigurationManager.AppSettings("DomainPrefix").ToString()
            Dim dsDirectoryEntry As New DirectoryEntry("LDAP://" + strDomain, "trcgov\\" + txtUserName.Text, txtPassword.Text, AuthenticationTypes.Secure)
            'Dim dsDirectoryEntry As New DirectoryEntry("LDAP://clouddc01.Cloud.GOVER.Local", user, pass, AuthenticationTypes.Secure)
            'Dim dsDirectoryEntry As New DirectoryEntry("LDAP://" & strDomain + "/" + txtUserName.Text + txtPassword.Text)
            'dsDirectoryEntry.RefreshCache()
            Dim dsSearch As DirectorySearcher = New DirectorySearcher(dsDirectoryEntry)
            dsSearch.FindOne()
            'dsDirectoryEntry.Dispose()

            Return True
        Catch ex As Exception
            CtlCommon.ShowMessage(Me.Page, ex.Message)
            'otherwise, it will crash out so return false
            Return False
        End Try
    End Function

    Private Sub ValidateUser(ByVal UserType As Integer)

        Try
            Dim dt As DataTable
            Dim encPass As String = Nothing
            Dim rslt As Integer = 0
            objLoginUser = New SYSUsers
            objSecurity = New SmartV.UTILITIES.SmartSecurity
            Dim ErrorForDir As Integer = 0

            If objLoginUser.ValidateConnection() = False Then
                CtlCommon.ShowMessage(Me.Page, "please make sure database connection")
                Exit Sub
            End If

            Select Case UserType
                Case 1 'System User
                    encPass = objSecurity.strSmartEncrypt(txtPassword.Text.Trim, txtUserName.Text.Trim)
                Case 2 'Domain
                    encPass = "DOMAIN"
                    Try
                        Dim strDomain As String = ConfigurationManager.AppSettings("Domain").ToString()
                        Dim strDomainPrefix As String = ConfigurationManager.AppSettings("DomainPrefix").ToString()
                        Dim PrefixWithUserName As String = strDomainPrefix & txtUserName.Text
                        Dim dsDirectoryEntry As New DirectoryEntry("LDAP://" + strDomain, PrefixWithUserName, txtPassword.Text, AuthenticationTypes.Secure)
                        ' Dim dsDirectoryEntry As New DirectoryEntry("LDAP://" + strDomain, txtUserName.Text, txtPassword.Text, AuthenticationTypes.Secure)
                        Dim dsSearch As DirectorySearcher = New DirectorySearcher(dsDirectoryEntry)
                        Dim dsResults As SearchResult
                        dsResults = dsSearch.FindOne
                    Catch ex As Exception
                        If SessionVariables.CultureInfo = "en-US" Then
                            CtlCommon.ShowMessage(Me.Page, "Invalid user name or password")
                        Else
                            CtlCommon.ShowMessage(Me.Page, "اسم المستخدم او كلمة المرور غير صحيحة")
                        End If
                    End Try
                Case 3 'Mixed Mode

                    If chkUsePC.Checked Then  'Changed By Zeeshan'
                        encPass = "DOMAIN"
                        Try

                            Dim strDomain As String = ConfigurationManager.AppSettings("Domain").ToString()
                            Dim strDomainPrefix As String = ConfigurationManager.AppSettings("DomainPrefix").ToString()
                            Dim PrefixWithUserName As String = strDomainPrefix & txtUserName.Text
                            Dim dsDirectoryEntry As New DirectoryEntry("LDAP://" + strDomain, PrefixWithUserName, txtPassword.Text, AuthenticationTypes.Secure)

                            ' Dim dsDirectoryEntry As New DirectoryEntry("LDAP://" + strDomain, txtUserName.Text, txtPassword.Text, AuthenticationTypes.Secure)
                            'Dim dsDirectoryEntry As New DirectoryEntry("LDAP://clouddc01.Cloud.GOVER.Local", User, pass, AuthenticationTypes.Secure)
                            'Dim dsDirectoryEntry As New DirectoryEntry("LDAP://" + strDomain, txtUserName.Text, txtPassword.Text, AuthenticationTypes.Secure)
                            'dsDirectoryEntry.RefreshCache()
                            Dim dsSearch As DirectorySearcher = New DirectorySearcher(dsDirectoryEntry)
                            dsSearch.FindOne()
                            'dsDirectoryEntry.Dispose()
                        Catch ex As Exception

                            If SessionVariables.CultureInfo = "en-US" Then
                                ErrorForDir = 1
                                CtlCommon.ShowMessage(Me.Page, "Invalid user name or password")
                            Else
                                ErrorForDir = 1
                                CtlCommon.ShowMessage(Me.Page, "اسم المستخدم او كلمة المرور غير صحيحة")
                            End If
                        End Try
                    Else
                        encPass = objSecurity.strSmartEncrypt(txtPassword.Text.Trim, txtUserName.Text.Trim)
                    End If


            End Select


            Try
                With objLoginUser
                    .LoginName = txtUserName.Text.Trim
                    .UsrID = txtUserName.Text.Trim
                    .Password = encPass
                    dt = objLoginUser.getLogin()
                    If (dt IsNot Nothing) AndAlso (dt.Rows.Count > 0) Then
                        If encPass = "MIXED" Then
                            If dt.Rows(0)("UserType").ToString() = "1" Then
                                .Password = objSecurity.strSmartEncrypt(txtPassword.Text.Trim, txtUserName.Text.Trim)
                            Else
                                .Password = "DOMAIN"
                                Try
                                    Dim strDomain As String = ConfigurationManager.AppSettings("Domain").ToString()
                                    Dim strDomainPrefix As String = ConfigurationManager.AppSettings("DomainPrefix").ToString()
                                    Dim PrefixWithUserName As String = strDomainPrefix & txtUserName.Text
                                    Dim dsDirectoryEntry As New DirectoryEntry("LDAP://" + strDomain, PrefixWithUserName, txtPassword.Text, AuthenticationTypes.Secure)
                                    Dim dsSearch As DirectorySearcher = New DirectorySearcher(dsDirectoryEntry)
                                    dsSearch.FindOne()
                                Catch ex As Exception
                                    If SessionVariables.CultureInfo = "en-US" Then
                                        CtlCommon.ShowMessage(Me.Page, "Invalid user name or password")
                                    Else
                                        CtlCommon.ShowMessage(Me.Page, "اسم المستخدم او كلمة المرور غير صحيحة")
                                    End If
                                    Exit Sub
                                End Try
                            End If
                        End If
                        dt = objLoginUser.getActive(rslt)

                        If rslt = 0 Then
                            If (dt IsNot Nothing) AndAlso (dt.Rows.Count > 0) Then
                                SessionVariables.LoginDate = DateTime.Now
                                SessionVariables.LoginUser = objLoginUser
                                If SessionVariables.LoginUser.Active = 0 Then
                                    If SessionVariables.CultureInfo = "en-US" Then
                                        CtlCommon.ShowMessage(Me.Page, "User is not Active please refere to System Adminstrator")
                                    Else
                                        CtlCommon.ShowMessage(Me.Page, "المستخدم غير مفعل الرجاء مراجعة مسؤول النظام")
                                    End If
                                    Exit Sub
                                End If
                                'Dim strdomainname As String = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString()
                                'strdomainname = strdomainname.Substring(0, strdomainname.IndexOf("\"))
                                'If .Password = "DOMAIN" Then
                                '    'System.Configuration.ConfigurationManager.AppSettings("Domain").ToString() <> strdomainname Or 
                                '    If Request.ServerVariables("LOGON_USER").Substring(Request.ServerVariables("LOGON_USER").IndexOf("\") + 1) <> txtUserName.Text.Trim Then

                                '        If SessionVariables.CultureInfo = "en-US" Then
                                '            CtlCommon.ShowMessage(Me.Page, "Invalid user name or password")
                                '        Else
                                '            CtlCommon.ShowMessage(Me.Page, "اسم المستخدم او كلمة المرور غير صحيحة")
                                '        End If
                                '        Return
                                '    End If

                                'End If
                                If ErrorForDir = 0 Then
                                    objLoginUser.ID = SessionVariables.LoginUser.ID
                                    objLoginUser.GetUser()
                                    If objLoginUser.DefaultSystemLang <> "" Then
                                        SessionVariables.LoginDate = DateTime.Now
                                        Application("TotalOnlineUsers") = CInt(Application("TotalOnlineUsers")) + 1

                                        If objLoginUser.DefaultSystemLang = "AR" Then
                                            SessionVariables.CultureInfo = "ar-JO"
                                            Response.Redirect("../Default/Homearabic.aspx?FromLogin=1")
                                        Else
                                            SessionVariables.CultureInfo = "en-US"
                                            Response.Redirect("../Default/home.aspx?FromLogin=1")

                                        End If

                                    Else
                                        SessionVariables.LoginDate = DateTime.Now
                                        Application("TotalOnlineUsers") = CInt(Application("TotalOnlineUsers")) + 1
                                        If SessionVariables.CultureInfo = "ar-JO" Then
                                            Response.Redirect("../Default/Homearabic.aspx?FromLogin=1")
                                        Else
                                            Response.Redirect("../Default/home.aspx?FromLogin=1")
                                        End If
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
                    'CtlCommon.ShowMessage(Me.Page, String.Concat("Operation Failed. \n", ex.Message))
                Else
                    'CtlCommon.ShowMessage(Me.Page, String.Concat("فشلت العملية.\n", ex.Message))
                End If
            Finally
                objLoginUser = Nothing
            End Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub PCSelected()
        If chkUsePC.Checked Then
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
                    .LoginName = txtUserName.Text.Trim
                    .UsrID = txtUserName.Text.Trim
                    .Password = encPass

                    ValidateUser(SystemUsersType)

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
                                    If Request.ServerVariables("LOGON_USER").Substring(Request.ServerVariables("LOGON_USER").IndexOf("\") + 1) <> txtUserName.Text.Trim Then
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
                                        Response.Redirect("../Default/Homearabic.aspx?FromLogin=1")
                                    Else
                                        SessionVariables.CultureInfo = "en-US"
                                        Response.Redirect("../Default/Home.aspx?FromLogin=1")

                                    End If

                                Else
                                    SessionVariables.LoginDate = DateTime.Now
                                    Application("TotalOnlineUsers") = CInt(Application("TotalOnlineUsers")) + 1
                                    If SessionVariables.CultureInfo = "ar-JO" Then
                                        Response.Redirect("../Default/Homearabic.aspx?FromLogin=1")
                                    Else
                                        Response.Redirect("../Default/Home.aspx?FromLogin=1")
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
                    'CtlCommon.ShowMessage(Me.Page, String.Concat("Operation Failed. \n", ex.Message))
                Else
                    'CtlCommon.ShowMessage(Me.Page, String.Concat("فشلت العملية.\n", ex.Message))
                End If
            Finally
                objLoginUser = Nothing
            End Try

        End If
    End Sub

    Private Sub fillClientLogo()
        objAPP_Settings = New APP_Settings
        With objAPP_Settings
            .GetByPK()
            If Not .LogoImage Is Nothing Then
                Thumbnail.ImageUrl = "~/ShowImage.ashx"
            Else
                Thumbnail.ImageUrl = "../assets/img/client_logo.png"

            End If
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

            head1.Controls.Add(lnkGoldDesign)
            head1.Controls.Add(lnkVioletDesign)
            head1.Controls.Add(lnkBlueDesign)
            head1.Controls.Add(lnkGreenDesign)
            head1.Controls.Add(lnkDofDesign)
            head1.Controls.Add(lnkRedDesign)
            head1.Controls.Add(lnkGoldDesign)

        End If




    End Sub

    Private Sub ChangeCaptions()
        If ConfigurationManager.AppSettings.Get("Culture").ToString() = "ar-JO" Then

            TextDirection = "rtl"
            lblCustomerMsg.Text = "الرجاء<b> تسجيل الدخول</b> هنا"
            PlaceHolderUname = "اسم المستخدم"
            PlaceHolderPassword = "كلمة السر"
            chkUsePC.Text = "استخدام بيانات الحاسوب"
            login_button.Text = "تسجيل الدخول"
            txtUserName.Attributes.Add("placeholder", "اسم المستخدم")
            txtPassword.Attributes.Add("placeholder", "كلمة السر")
            lnkHaveLicense.Text = "يوجد رخصة مسبقاً"
        Else
            TextDirection = "ltr"
            Lang = CtlCommon.Lang.EN
            lblCustomerMsg.Text = "Please <b>Login</b> here"
            PlaceHolderUname = "User Name"
            PlaceHolderPassword = "Password"
            chkUsePC.Text = "Use PC Credential"
            login_button.Text = "Sign In"
            txtUserName.Attributes.Add("placeholder", "User Name")
            txtPassword.Attributes.Add("placeholder", "Password")
            head1.Controls.Remove(css_ArCSS)
            lnkHaveLicense.Text = "Already Have License"
        End If

    End Sub

#End Region

    'Private Sub validateLogin()
    '    Dim dt As DataTable
    '    Dim encPass As String = Nothing
    '    Dim rslt As Integer = 0
    '    objLoginUser = New SYSUsers

    '    If objLoginUser.ValidateConnection() = False Then

    '        CtlCommon.ShowMessage(Me.Page, "please make sure database connection")
    '        Exit Sub
    '    End If
    '    objLoginUser = New SYSUsers
    '    objSecurity = New SmartSecurity

    '    If ConfigurationManager.AppSettings("AuthenticationType").ToString = "Domain" Then
    '        encPass = "DOMAIN"
    '    Else
    '        encPass = objSecurity.strSmartEncrypt(txtPassword.Text.Trim, txtUserName.Text.Trim)
    '        'objSecurity.strHashPassword(txtPassword.Text.Trim, txtUserName.Text.Trim)
    '    End If

    '    Try
    '        With objLoginUser
    '            .LoginName = txtUserName.Text.Trim
    '            .UsrID = txtUserName.Text.Trim
    '            .Password = encPass
    '            dt = objLoginUser.getLogin()
    '            If (dt IsNot Nothing) AndAlso (dt.Rows.Count > 0) Then
    '                dt = objLoginUser.getActive(rslt)
    '                If rslt = 0 Then
    '                    If (dt IsNot Nothing) AndAlso (dt.Rows.Count > 0) Then
    '                        SessionVariables.Sys_LoginUser = objLoginUser

    '                        'SessionVariables.Sys_LocationID = objLoginUser
    '                        'SessionVariables.Sys_UserType = objLoginUser
    '                        'Dim i1 As Integer = SessionVariables.Sys_LoginUser.LocID
    '                        'Dim i As Integer = SessionVariables.Sys_LoginUser.UserType


    '                        'SessionVariables.Sys_UserType = objLoginUser
    '                        'Dim i As Integer
    '                        'i = SessionVariables.Sys_LoginUser.UserType

    '                        If SessionVariables.Sys_LoginUser.Active = 0 Then
    '                            If SessionVariables.CultureInfo = "en-US" Then
    '                                CtlCommon.ShowMessage(Me.Page, "User is not Active please refere to System Adminstrator")
    '                            Else
    '                                CtlCommon.ShowMessage(Me.Page, "المستخدم غير مفعل الرجاء مراجعة مسؤول النظام")
    '                            End If
    '                            Exit Sub
    '                        End If
    '                        'If SessionVariables.Sys_LoginUser.IsFirstLogin = 0 Then
    '                        '    Response.Redirect("../Security/ChangePassword.aspx?Change=True")
    '                        'End If

    '                        If SessionVariables.CultureInfo = "ar-JO" Then

    '                            Response.Redirect("../Default/Homearabic.aspx")
    '                        Else
    '                            Response.Redirect("../Default/Home.aspx")
    '                        End If

    '                    Else
    '                        'CtlCommon.ShowMessage(Me.Page, "This user not active")
    '                        If SessionVariables.CultureInfo = "en-US" Then
    '                            CtlCommon.ShowMessage(Me.Page, "Error during login to system")
    '                        Else
    '                            CtlCommon.ShowMessage(Me.Page, "خطأ أثناء عملية تسجيل الدخول")
    '                        End If

    '                    End If
    '                Else
    '                    If SessionVariables.CultureInfo = "en-US" Then
    '                        CtlCommon.ShowMessage(Me.Page, "Error during login to system")
    '                    Else
    '                        CtlCommon.ShowMessage(Me.Page, "خطأ أثناء عملية تسجيل الدخول")
    '                    End If

    '                End If

    '            Else
    '                If SessionVariables.CultureInfo = "en-US" Then
    '                    CtlCommon.ShowMessage(Me.Page, "Invalid user name or password")
    '                Else
    '                    CtlCommon.ShowMessage(Me.Page, "اسم المستخدم او كلمة المرور غير صحيحة")
    '                End If

    '            End If
    '        End With

    '    Catch ex As Exception
    '        If SessionVariables.CultureInfo = "en-US" Then
    '            CtlCommon.ShowMessage(Me.Page, String.Concat("Operation Failed. \n", ex.Message))
    '        Else
    '            CtlCommon.ShowMessage(Me.Page, String.Concat("فشلت العملية.\n", ex.Message))
    '        End If

    '    Finally
    '        objLoginUser = Nothing
    '    End Try

    'End Sub

    'Private Sub validateWndowsLogin()

    '    Try
    '        Dim strDomain As String = ConfigurationManager.AppSettings("Domain").ToString()
    '        Dim dsDirectoryEntry As New DirectoryEntry("LDAP://" & strDomain, txtUserName.Text, txtPassword.Text)
    '        Dim dsSearch As DirectorySearcher = New DirectorySearcher(dsDirectoryEntry)
    '        Dim dsResults As SearchResult
    '        dsResults = dsSearch.FindOne
    '        'blnAuthenticated = True
    '        Dim dt As DataTable
    '        Dim encPass As String = Nothing
    '        Dim rslt As Integer = 0
    '        objLoginUser = New SYSUsers
    '        objSecurity = New SmartSecurity

    '        If objLoginUser.ValidateConnection() = False Then
    '            CtlCommon.ShowMessage(Me.Page, "please make sure database connection")
    '            Exit Sub
    '        End If

    '        encPass = "DOMAIN"

    '        Try
    '            With objLoginUser
    '                .LoginName = txtUserName.Text.Trim
    '                .UsrID = txtUserName.Text.Trim
    '                .Password = encPass
    '                dt = objLoginUser.getLogin()
    '                If (dt IsNot Nothing) AndAlso (dt.Rows.Count > 0) Then
    '                    dt = objLoginUser.getActive(rslt)
    '                    If rslt = 0 Then
    '                        If (dt IsNot Nothing) AndAlso (dt.Rows.Count > 0) Then
    '                            SessionVariables.Sys_LoginUser = objLoginUser
    '                            If SessionVariables.Sys_LoginUser.Active = 0 Then
    '                                If SessionVariables.CultureInfo = "en-US" Then
    '                                    CtlCommon.ShowMessage(Me.Page, "User is not Active please refere to System Adminstrator")
    '                                Else
    '                                    CtlCommon.ShowMessage(Me.Page, "المستخدم غير مفعل الرجاء مراجعة مسؤول النظام")
    '                                End If
    '                                Exit Sub
    '                            End If

    '                            If SessionVariables.CultureInfo = "ar-JO" Then
    '                                Response.Redirect("../Default/Homearabic.aspx")
    '                            Else
    '                                Response.Redirect("../Default/Home.aspx")
    '                            End If

    '                        Else

    '                            If SessionVariables.CultureInfo = "en-US" Then
    '                                CtlCommon.ShowMessage(Me.Page, "Error during login to system")
    '                            Else
    '                                CtlCommon.ShowMessage(Me.Page, "خطأ أثناء عملية تسجيل الدخول")
    '                            End If

    '                        End If
    '                    Else
    '                        If SessionVariables.CultureInfo = "en-US" Then
    '                            CtlCommon.ShowMessage(Me.Page, "Error during login to system")
    '                        Else
    '                            CtlCommon.ShowMessage(Me.Page, "خطأ أثناء عملية تسجيل الدخول")
    '                        End If

    '                    End If

    '                Else
    '                    If SessionVariables.CultureInfo = "en-US" Then
    '                        CtlCommon.ShowMessage(Me.Page, "Invalid user name or password")
    '                    Else
    '                        CtlCommon.ShowMessage(Me.Page, "اسم المستخدم او كلمة المرور غير صحيحة")
    '                    End If

    '                End If
    '            End With

    '        Catch ex As Exception
    '            If SessionVariables.CultureInfo = "en-US" Then
    '                CtlCommon.ShowMessage(Me.Page, String.Concat("Operation Failed. \n", ex.Message))
    '            Else
    '                CtlCommon.ShowMessage(Me.Page, String.Concat("فشلت العملية.\n", ex.Message))
    '            End If

    '        Finally
    '            objLoginUser = Nothing
    '        End Try



    '    Catch ex As Exception
    '        ' blnAuthenticated = False
    '        If SessionVariables.CultureInfo = "en-US" Then
    '            CtlCommon.ShowMessage(Me.Page, "Invalid user name or password")
    '        Else
    '            CtlCommon.ShowMessage(Me.Page, "اسم المستخدم او كلمة المرور غير صحيحة")
    '        End If
    '    End Try

    'End Sub



End Class
