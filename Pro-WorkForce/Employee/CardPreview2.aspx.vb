Imports SmartV.UTILITIES

Partial Class Employee_CardPreview2
    Inherits System.Web.UI.Page
    Protected Sub FillHidenValues()
        hidSessionID.Value = 33
        hidUser.Value = 33
        Dim strDBConnection() As String

        strDBConnection = ConfigurationManager.ConnectionStrings("ConnStr").ToString().Split(";")
        txtDBType.Value = "SQL"
        txtServerName.Value = strDBConnection(0).Substring(strDBConnection(0).IndexOf("=") + 1, strDBConnection(0).Length - strDBConnection(0).IndexOf("=") - 1) 'ConfigurationManager.AppSettings("IDServer").ToString
        txtDBName.Value = strDBConnection(1).Substring(strDBConnection(1).IndexOf("=") + 1, strDBConnection(1).Length - strDBConnection(1).IndexOf("=") - 1) 'ConfigurationManager.AppSettings("IDDB").ToString
        txtUserName.Value = strDBConnection(2).Substring(strDBConnection(2).IndexOf("=") + 1, strDBConnection(2).Length - strDBConnection(2).IndexOf("=") - 1) 'ConfigurationManager.AppSettings("IDUName").ToString
        txtPassword.Value = strDBConnection(3).Substring(strDBConnection(3).IndexOf("=") + 1, strDBConnection(3).Length - strDBConnection(3).IndexOf("=") - 1) 'ConfigurationManager.AppSettings("IDPassword").ToString
        hidCabFile.Value = "CAB/SmartID.zip"
        txtLoginUserName.Value = SessionVariables.LoginUser.fullName
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not SessionVariables.LoginUser Is Nothing Then
            FillHidenValues()

        End If

    End Sub
End Class
