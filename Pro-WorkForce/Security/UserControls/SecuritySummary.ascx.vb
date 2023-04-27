Imports System.Web.Services
Imports System.Data
Imports System.Web.Script.Serialization
Imports TA.Security
Imports SmartV.UTILITIES

Partial Class Security_UserControls_SecuritySummary
    Inherits System.Web.UI.UserControl

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private objSYSUsers As SYSUsers
    Public ChartLang As String

#End Region
    
#Region "Page Events"

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        ElseIf SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
        Else
            Lang = CtlCommon.Lang.EN
        End If
    End Sub

    Protected Sub Security_UserControls_SecuritySummary_Load(sender As Object, e As EventArgs) Handles Me.Load
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
            FillInfo()
            FillSecurity_Summary()
        End If
    End Sub

#End Region

#Region "Methods"

    Private Sub FillSecurity_Summary()
        Dim dt As DataTable
        objSYSUsers = New SYSUsers
        With objSYSUsers
            dt = .GetUser_AndGroupCount
        End With
        lblActiveUsersVal.Text = dt(0)("ActiveUserCount").ToString
        lblInActiveUsersVal.Text = dt(0)("InActiveUserCount").ToString
        lblUserGroupsVal.Text = dt(0)("GroupCount").ToString
        lblOnlineUsersVal.Text = Application("TotalOnlineUsers").ToString
    End Sub

    Private Sub FillInfo()
        Dim dt As DataTable
        objSYSUsers = New SYSUsers
        With objSYSUsers
            dt = .GetUser_AndGroupCount
        End With
        lblUserNum.Text = dt(0)("ActiveUserCount").ToString
        lblNumberOfGroups.Text = dt(0)("GroupCount").ToString
    End Sub

#End Region

End Class
