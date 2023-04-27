Imports SmartV.UTILITIES

Partial Class Default_AdminMaster
    Inherits System.Web.UI.MasterPage
    Protected dir, textalign As String
    Public Lang As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (SessionVariables.CultureInfo Is Nothing) Then
            SessionVariables.CultureInfo = "en-US"
            lnkar.Visible = True
            lnken.Visible = False
            dir = "ltr"
            textalign = "left"
            lnklogoutar.Visible = False
            lnklogouten.Visible = True
            Lang = "en"
        Else
            If SessionVariables.CultureInfo = "en-US" Then
                SessionVariables.CultureInfo = "en-US"
                dir = "ltr"
                textalign = "left"
                lnkar.Visible = True
                lnken.Visible = False
                lnklogoutar.Visible = False
                lnklogouten.Visible = True
                Lang = "en"
            Else
                SessionVariables.CultureInfo = "ar-JO"
                dir = "rtl"
                textalign = "right"
                lnkar.Visible = False
                lnken.Visible = True
                lnklogoutar.Visible = True
                lnklogouten.Visible = False
                Lang = "ar"
            End If
        End If
    End Sub
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


    Protected Sub lnkar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkar.Click
        SessionVariables.CultureInfo = "ar-JO"
        lnkar.Visible = False
        lnken.Visible = True
        Response.Redirect("../Default/Default.aspx")
    End Sub

    Protected Sub lnken_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnken.Click
        SessionVariables.CultureInfo = "en-US"
        lnken.Visible = False
        lnkar.Visible = True
        Response.Redirect("../Default/Default.aspx")
    End Sub

    Protected Sub lnklogouten_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnklogouten.Click
        Response.Redirect("../Default/logout.aspx")
    End Sub

    Protected Sub lnklogoutar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnklogoutar.Click
        Response.Redirect("../Default/logout.aspx")
    End Sub


End Class

