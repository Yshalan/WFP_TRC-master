﻿
Partial Class Employee_cardtest
    Inherits System.Web.UI.Page
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Dim strDBConnection() As String

        strDBConnection = ConfigurationManager.ConnectionStrings("ConnStr").ToString().Split(";")
        txtDBType.Value = "SQL"
        txtServerName.Value = strDBConnection(0).Substring(strDBConnection(0).IndexOf("=") + 1, strDBConnection(0).Length - strDBConnection(0).IndexOf("=") - 1) 'ConfigurationManager.AppSettings("IDServer").ToString
        txtDBName.Value = strDBConnection(1).Substring(strDBConnection(1).IndexOf("=") + 1, strDBConnection(1).Length - strDBConnection(1).IndexOf("=") - 1) 'ConfigurationManager.AppSettings("IDDB").ToString
        txtUserName.Value = strDBConnection(2).Substring(strDBConnection(2).IndexOf("=") + 1, strDBConnection(2).Length - strDBConnection(2).IndexOf("=") - 1) 'ConfigurationManager.AppSettings("IDUName").ToString
        txtPassword.Value = strDBConnection(3).Substring(strDBConnection(3).IndexOf("=") + 1, strDBConnection(3).Length - strDBConnection(3).IndexOf("=") - 1) 'ConfigurationManager.AppSettings("IDPassword").ToString
        hidCabFile.Value = "CAB/SmartID.zip"
        txtLoginUserName.Value = 33
    End Sub
End Class