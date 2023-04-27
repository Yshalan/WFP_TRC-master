Imports SmartV.Security
Imports SmartV.UTILITIES
Imports System.Threading
Imports System.Reflection
Imports System.Resources
Imports TA.Security
Imports TA.Admin

Partial Class Admin_Security_ChangePassword
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    'Public ShipDetails, ShipOwnerandLocalAgentDetails, LoadLinesInformation, NumberandCapacityofLifeSavingAppliances, TypeandNumberofFireFightingEquipment As String
    Public Add, Delete, PollutionPreventionTank As String
    Dim lang As CtlCommon.Lang
    Public PasswordType As String
    Private objAPP_Settings As APP_Settings
#End Region

#Region "Page Events"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        Else
            If SessionVariables.CultureInfo = "ar-JO" Then
                lang = CtlCommon.Lang.AR
                Page.MasterPageFile = "~/default/ArabicMaster.master"
            Else
                lang = CtlCommon.Lang.EN
                Page.MasterPageFile = "~/default/NewMaster.master"
            End If
            Page.UICulture = SessionVariables.CultureInfo
        End If
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If SessionVariables.LoginUser Is Nothing Then
            Response.Redirect("~/default/Logout.aspx")
        End If
        If (SessionVariables.CultureInfo Is Nothing) Then
            SessionVariables.CultureInfo = "en-US"
        Else
            If SessionVariables.CultureInfo = "en-US" Then
                SessionVariables.CultureInfo = "en-US"
            Else
                SessionVariables.CultureInfo = "ar-JO"
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' ''****By Mohammad Shanaa 09/02/2011 For Pages Authentications
        ''Dim FormID As String = "28"
        ''If Not SmartV.Security.SYSForms.IsAccessible(FormID) Then
        ''    Response.Redirect("~/default/Logout.aspx")
        ''End If
        '***********************
        loadCaptions()
        If SessionVariables.LoginUser Is Nothing Then
            Response.Redirect("~/default/Logout.aspx")
        End If
        If Not Page.IsPostBack Then
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            PageHeader1.HeaderText = ResourceManager.GetString("ChangePass", CultureInfo)
            Page.Title = "Work Force-Pro : :  " + ResourceManager.GetString("ChangePass", CultureInfo)

            If Not Request.QueryString("Change") Is Nothing Then
                If Request.QueryString("Change").ToString = "True" Then
                    lblChangePassword.Visible = True
                Else
                    lblChangePassword.Visible = False
                End If
            Else
                lblChangePassword.Visible = False
            End If

            'If SessionVariables.CultureInfo = "ar-JO" Then
            '    PasswordStrength1.DisplayPosition = AjaxControlToolkit.DisplayPosition.LeftSide
            'Else
            '    PasswordStrength1.DisplayPosition = AjaxControlToolkit.DisplayPosition.RightSide
            'End If

            objAPP_Settings = New APP_Settings
            objAPP_Settings.GetByPK()
            If Not objAPP_Settings.PasswordType = Nothing Then
                PasswordType = objAPP_Settings.PasswordType
            End If
            If objAPP_Settings.PasswordType = 1 Then
                PwdValidation.Enabled = False
            Else
                PwdValidation.Enabled = True
            End If
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'If txtNewPassowrd.Text.Length < 8 Then
        '    If SessionVariables.CultureInfo = "en-US" Then
        '        CtlCommon.ShowMessage(Me.Page, "Password should be atleasat 8 characters")
        '    Else
        '        CtlCommon.ShowMessage(Me.Page, "كلمة المرور يجب ان تتكون من 8 حروف على الأقل")
        '    End If
        '    Exit Sub
        'End If
        If Me.Page.IsValid Then
            Dim OldEncPass As String = Nothing
            Dim NewEncPass As String = Nothing
            Dim rslt As Integer = 0
            Dim objLoginUser As New SYSUsers
            Dim objSecurity As New SmartV.UTILITIES.SmartSecurity
            Dim user As New SYSUsers
            With objLoginUser

                .ID = SessionVariables.LoginUser.ID
                '.UsrID = 4
                .GetUser()
                If .UserType = 1 Or .UserType = 3 Then
                    OldEncPass = objSecurity.strSmartEncrypt(txtOldPassowrd.Text.Trim, SessionVariables.LoginUser.UsrID) 'objSecurity.strHashPassword(txtOldPassowrd.Text.Trim, SessionVariables.Sys_LoginUser.UsrID) '.Password
                    NewEncPass = objSecurity.strSmartEncrypt(txtNewPassowrd.Text.Trim, SessionVariables.LoginUser.UsrID) 'objSecurity.strHashPassword(txtNewPassowrd.Text.Trim, SessionVariables.Sys_LoginUser.UsrID)
                    .EncrptPassNew = NewEncPass
                    .EncrptPassOld = OldEncPass
                Else
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ChangeForSysUser", CultureInfo), "info")
                End If
            End With
            Dim errNo As Integer
            Try
                errNo = objLoginUser.ChangeUserPassword()
                CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
                If errNo = 0 Then

                    If lang = CtlCommon.Lang.AR Then
                        ScriptManager.RegisterStartupScript(Me, Page.GetType, "Redirect", "ShowMessageAndRedirect('تم تغيير كلمة السر بنجاح ، الرجاء القيام بتسجيل الخروج ثم تسجيل الدخول مرة اخرى بكلمة السر الجديدة','../Security/ChangePassword.aspx');", True)
                    Else
                        ScriptManager.RegisterStartupScript(Me, Page.GetType, "Redirect", "ShowMessageAndRedirect('Your password is changed successfully, Please logout and login using the new password','../Security/ChangePassword.aspx');", True)
                    End If
                    'CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("PassChangeSuccess", CultureInfo), "success")
                    SessionVariables.LoginUser = Nothing
                Else
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("WrongOldPass", CultureInfo), "error")
                End If

            Catch ex As Exception

            End Try


        End If
    End Sub

#End Region

#Region "Methods"

    Private Sub loadCaptions()
        Dim CultureInfo As New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        ''Master.Page.Form.Attributes.Add("dir", ResourceManager.GetString("PageDir", CultureInfo))
        ''Page.Form.Attributes.Add("dir", ResourceManager.GetString("PageDir", CultureInfo))
        ''Dim cssFile = New HtmlLink()
        ''cssFile.Href = "../css/" & ResourceManager.GetString("CssFileName", CultureInfo)
        ' ''cssFile.Href = "../Security/menu.css")
        ''cssFile.Attributes("rel") = "stylesheet"
        ''cssFile.Attributes("type") = "text/css"
        ''cssFile.Attributes("media") = "all"
        ''Master.Page.Header.Controls.Add(cssFile)
        ''Page.Header.Controls.Add(cssFile)
        ''MyBase.InitializeCulture()
        ''Dim CultureInfo As New System.Globalization.CultureInfo("ar-JO")
        'Me.Page.Form.Attributes.Add("dir", ResourceManager.GetString("PageDir", CultureInfo))
        'Dim cssFile = New HtmlLink()
        'cssFile.Href = "../css/" & ResourceManager.GetString("CssFileName", CultureInfo)
        'cssFile.Attributes("rel") = "stylesheet"
        'cssFile.Attributes("type") = "text/css"
        'cssFile.Attributes("media") = "all"
        'Me.Page.Header.Controls.Add(cssFile)
        'MyBase.InitializeCulture()
    End Sub

    Protected Overloads Overrides Sub InitializeCulture()
        ''If SessionVariables.CultureInfo Is Nothing Then
        ''    Return
        ''End If
        ''Dim CultureInfo As New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        ' ''rmVr = New ResourceManager("Resources.LocalizedText", Assembly.Load("App_GlobalResources"))
        ''ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
        ''CtlCommon.SetCulture(Thread.CurrentThread)
        Dim _culture As String = SessionVariables.CultureInfo
        If (String.IsNullOrEmpty(_culture)) Then
            _culture = "en-US"
            Me.UICulture = _culture
            Me.Culture = _culture
        End If
        If (_culture <> "Auto") Then
            Dim ci As New System.Globalization.CultureInfo(_culture)
            System.Threading.Thread.CurrentThread.CurrentCulture = ci
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci
        End If
        MyBase.InitializeCulture()
    End Sub

#End Region

End Class
