
Partial Class Default_logout
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session.Clear()
        Application("TotalOnlineUsers") = CInt(Application("TotalOnlineUsers")) - 1
        Response.Redirect("Login.aspx")
    End Sub
End Class
