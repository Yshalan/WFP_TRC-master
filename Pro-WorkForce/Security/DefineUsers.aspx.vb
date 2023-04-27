Imports SmartV.UTILITIES
Imports System.Threading
Imports System.Resources
Imports System.Reflection

Partial Class Security_DefineUsers
    Inherits System.Web.UI.Page

#Region "Class Variables"

    'Public ShipDetails, ShipOwnerandLocalAgentDetails, LoadLinesInformation, NumberandCapacityofLifeSavingAppliances, TypeandNumberofFireFightingEquipment As String
    Public Add, Delete, PollutionPreventionTank As String
    Protected dir, textalign As String
    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    ' Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    'Dim rmVr As New ResourceManager("Resources.LocalizedText", Assembly.Load("App_GlobalResources"))

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

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        Else
            Page.UICulture = SessionVariables.CultureInfo
        End If
        'If (SessionVariables.CultureInfo Is Nothing) Then
        '    SessionVariables.CultureInfo = "en-US"
        '    dir = "ltr"
        '    textalign = "left"
        'Else
        If SessionVariables.CultureInfo = "en-US" Then
            SessionVariables.CultureInfo = "en-US"
            dir = "ltr"
            textalign = "left"
        Else
            SessionVariables.CultureInfo = "ar-JO"
            dir = "rtl"
            textalign = "right"
        End If

        'End If
        loadCaptions()

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '****By Mohammad Shanaa 17/05/2011 For Pages Authentications
        'Dim FormID As String = "124"
        'If Not SmartV.Security.SYSForms.IsAccessible(FormID) Then
        '    Response.Redirect("~/default/Logout.aspx")
        'End If
        'If SessionVariables.LoginUser.IsFirstLogin = 0 Then
        '    Response.Redirect("../Security/ChangePassword.aspx?Change=True", False)
        'End If
        '***********************
        loadCaptions()
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")

        ElseIf SessionVariables.CultureInfo = "ar-JO" Then

            Lang = CtlCommon.Lang.AR
        Else
            Lang = CtlCommon.Lang.EN
        End If

        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        PageHeader1.HeaderText = ResourceManager.GetString("DefUsers", CultureInfo)
        Page.Title = "Work Force-Pro : :  " + ResourceManager.GetString("DefUsers", CultureInfo)

        'CType(Master.FindControl("PageText"), Label).Text = "6.2"
    End Sub

#End Region
 
#Region "Mehtods"

    Private Sub loadCaptions()
        'Dim CultureInfo As New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        ' ''Master.Page.Form.Attributes.Add("dir", ResourceManager.GetString("PageDir", CultureInfo))
        MyBase.InitializeCulture()
    End Sub

    'Protected Overloads Overrides Sub InitializeCulture()
    '    If SessionVariables.CultureInfo Is Nothing Then
    '        Return
    '    End If
    '    Dim CultureInfo As New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
    '    'rmVr = New ResourceManager("Resources.LocalizedText", Assembly.Load("App_GlobalResources"))
    '    ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    '    CtlCommon.SetCulture(Thread.CurrentThread)
    'End Sub

    Protected Overrides Sub InitializeCulture()
        MyBase.InitializeCulture()
    End Sub

#End Region


End Class
