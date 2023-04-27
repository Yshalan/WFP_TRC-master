Imports SmartV.UTILITIES

Partial Class Default_LicensePopUp
    Inherits System.Web.UI.Page

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If Not txtNewLicense.Text = String.Empty Then
            CtlCommon.MakeFolderWritable(Server.MapPath("../Web.config"))
            Dim myConfiguration As Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
            myConfiguration.AppSettings.Settings.Item("LicenseKey").Value = txtNewLicense.Text
            myConfiguration.Save()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "close", "CloseAndRefresh();", True)
            'ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "pop", "window.close();", False)
        End If
    End Sub
End Class
