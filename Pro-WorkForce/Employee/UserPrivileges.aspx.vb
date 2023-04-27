Imports SmartV.UTILITIES
Imports System.Resources
Imports System.Globalization
Imports TA.Security

Partial Class Employee_UserPrevileges
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Dim CultureInfo As System.Globalization.CultureInfo
    Private objVersion As SmartV.Version.version
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Private objSYSUsers As SYSUsers

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
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            PageHeader1.HeaderText = ResourceManager.GetString("UserPrivilege", CultureInfo)
            If (objVersion.HasMultiCompany() = False) Then
                Company.Visible = False
            Else
                Company.Visible = True
            End If

            'If Not Session("SourceControl_Session") Is Nothing Then
            '    If Session("SourceControl_Session").ToString.Contains("Company") Then
            '        TabContainer1.ActiveTabIndex = 0
            '    ElseIf Session("SourceControl_Session").ToString.Contains("Entity") Then
            '        TabContainer1.ActiveTabIndex = 1
            '    ElseIf Session("SourceControl_Session").ToString.Contains("Logical") Then
            '        TabContainer1.ActiveTabIndex = 2
            '    ElseIf Session("SourceControl_Session").ToString.Contains("WorkLocation") Then
            '        TabContainer1.ActiveTabIndex = 3
            '    Else
            '        TabContainer1.ActiveTabIndex = 0
            '    End If
            '    Session("SourceControl_Session") = Nothing
            'End If

        End If
    End Sub

#End Region

#Region "Methods"

    Private Sub ShowHideTabs()
        Dim UserId As Integer = SessionVariables.LoginUser.ID
        objSYSUsers = New SYSUsers
        With objSYSUsers
            .ID = UserId
            .GetUser()

            If .UserStatus = 2 Then '---Unit Level
                Company.Visible = False

                Entity.Visible = True
                WorkLocation.Visible = True
                LogicalGroup.Visible = True
            ElseIf .UserStatus = 3 Then '---Company Level
                Company.Visible = True

                Entity.Visible = False
                WorkLocation.Visible = False
                LogicalGroup.Visible = False
            Else '---System Level
                Company.Visible = True
                Entity.Visible = True
                WorkLocation.Visible = True
                LogicalGroup.Visible = True
            End If

        End With
    End Sub

#End Region

End Class
