Imports SmartV.UTILITIES
Imports TA.Accounts

Partial Class Default_unAuthorizedUseBased
    Inherits System.Web.UI.Page
    Private objBU_Accounts_Contacts As BU_Accounts_Contacts
    Dim securl As SecureUrl
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            txtUserName.Focus()
        End If
    End Sub
    Private Sub validateLogin()
        objBU_Accounts_Contacts = New BU_Accounts_Contacts
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)

        If objBU_Accounts_Contacts.ValidateConnection() = False Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ChkConnection", CultureInfo))
            Exit Sub
        End If

        Try
            With objBU_Accounts_Contacts
                .UserName = txtUserName.Text.Trim
                .Password = txtPassword.Text.Trim
                .GetLoginUserDetails()
                If (objBU_Accounts_Contacts.ContactId > 0) Then

                    If Not .IsActive Then
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("UsernotActive", CultureInfo))
                    ElseIf .IsActive Then
                        'SessionVariables.Esub_LoginUser = objBU_Accounts_Contacts ''If Page is in use this line need to be change

                        ' Query the user store to get this user's User Data 
                        Dim userDataString As String = String.Concat(txtUserName)

                        ' Create the cookie that contains the forms authentication ticket 
                        Dim authCookie As HttpCookie = FormsAuthentication.GetAuthCookie(txtUserName.Text, False)

                        ' Get the FormsAuthenticationTicket out of the encrypted cookie 
                        Dim ticket As FormsAuthenticationTicket = FormsAuthentication.Decrypt(authCookie.Value)

                        ' Create a new FormsAuthenticationTicket that includes our custom User Data 
                        Dim newTicket As FormsAuthenticationTicket = _
                            New FormsAuthenticationTicket(ticket.Version, _
                            ticket.Name, _
                            ticket.IssueDate, _
                            ticket.Expiration, ticket.IsPersistent, userDataString)

                        ' Update the authCookie's Value to use the encrypted version of newTicket 
                        authCookie.Value = FormsAuthentication.Encrypt(newTicket)

                        ' Manually add the authCookie to the Cookies collection 
                        Response.Cookies.Add(authCookie)

                        ' Determine redirect URL and send user there 
                        Dim redirUrl As String = FormsAuthentication.GetRedirectUrl(txtUserName.Text, False)
                        Response.Redirect(redirUrl)
                    Else
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorLogin", CultureInfo))
                    End If


                Else
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("InvalidUserOrPass", CultureInfo))
                End If

            End With

        Catch ex As Exception
            CtlCommon.ShowMessage(Me.Page, String.Concat(ResourceManager.GetString("OperationFailed", CultureInfo) + "\n", ""))
        Finally
            objBU_Accounts_Contacts = Nothing
        End Try
    End Sub




    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        validateLogin()
    End Sub
End Class
