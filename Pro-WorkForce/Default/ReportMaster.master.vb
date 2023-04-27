Imports SmartV.UTILITIES

Partial Class Default_AdminMaster
    Inherits System.Web.UI.MasterPage
    Protected dir, textalign As String
    Public Lang As String

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
End Class

