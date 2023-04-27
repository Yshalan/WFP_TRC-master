Imports SmartV.Version.version
Imports SmartV.UTILITIES
Imports TA.Admin
Imports System.Security.Cryptography
Imports System.IO
Imports TA.Employees
Imports System.Data

Partial Class Admin_About
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objversion As SmartV.Version.version
    Private objAPP_Settings As APP_Settings
    Private objEmployee As Employee
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Private _MacAddress As String
    Private _Version As String

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

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")

            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)

            FillCustomerInfo()
            PageHeader1.HeaderText = ResourceManager.GetString("AboutProduct", CultureInfo)

        End If
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If Not txtChangeLicneseKey.Text = String.Empty Then
            CtlCommon.MakeFolderWritable(Server.MapPath("../Web.config"))
            Dim myConfiguration As Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
            myConfiguration.AppSettings.Settings.Item("LicenseKey").Value = txtChangeLicneseKey.Text
            myConfiguration.Save()
            Response.Redirect("../Default/Home.aspx")
        End If
    End Sub

#End Region

#Region "Methods"

    Private Sub FillCustomerInfo()
        objversion = New SmartV.Version.version
        objAPP_Settings = New APP_Settings
        objEmployee = New Employee
        objAPP_Settings.GetByPK()
        Dim dtEmpCount As DataTable
        With objversion
            lblCustomerNameVal.Text = IIf(Lang = CtlCommon.Lang.AR, objAPP_Settings.companyArabicName1, objAPP_Settings.CompanyName1)
            lblMacAddressVal.Text = .GetMACAddress()
            lblNoOfEmployeesVal.Text = IIf(.GetMaxNoOfEmployees = -1, IIf(Lang = CtlCommon.Lang.EN, "Unlimited", "غير محدود"), .GetMaxNoOfEmployees)
            lblNoOfUsersVal.Text = IIf(.GetMaxNoOfUsers = -1, IIf(Lang = CtlCommon.Lang.EN, "Unlimited", "غير محدود"), .GetMaxNoOfUsers)
            lblNoOfCompaniesVal.Text = .GetNoOfCompanies
            If .HasMobileApplication = True Then
                lblNoOfMobileDevicesVal.Text = IIf(.GetNoOfAllowedMobile = -1, IIf(Lang = CtlCommon.Lang.EN, "Unlimited", "غير محدود"), .GetNoOfAllowedMobile)
                lblNoOfMobileWorkLocationsVal.Text = IIf(.GetNoOfAllowedMobileWorkLocations = -1, IIf(Lang = CtlCommon.Lang.EN, "Unlimited", "غير محدود"), .GetNoOfAllowedMobileWorkLocations)
                dvMobileDevices.Visible = True
                dvMobileWorkLocation.Visible = True
            Else
                dvMobileDevices.Visible = False
                dvMobileWorkLocation.Visible = False
            End If
            If .GetIsTrial = True Then
                lblEndDate.Text = IIf(Lang = CtlCommon.Lang.EN, "Trial End Date", "تاريخ انتهاء فترة التجربة")
            End If
            lblEndDateVal.Text = .GetSupportEndDate.ToString("yyyy-MMMM-dd", IIf(SessionVariables.CultureInfo = "ar-JO", New System.Globalization.CultureInfo("ar-EG"), New System.Globalization.CultureInfo("en-US")))
            lblActualEndDateVal.Text = Date.Today.ToString("yyyy-MMMM-dd", IIf(SessionVariables.CultureInfo = "ar-JO", New System.Globalization.CultureInfo("ar-EG"), New System.Globalization.CultureInfo("en-US")))
            lblReleasNoVal.Text = .GetVersionNumber
        End With
        With objEmployee
            dtEmpCount = .GetCountEmployeesAndUsers
            If dtEmpCount.Rows.Count > 0 Then
                lblActualNoOfEmployeesVal.Text = dtEmpCount.Rows(0)("EmployeeCount").ToString()
                lblActualNoOfUsersVal.Text = dtEmpCount.Rows(0)("UsersCount").ToString()
                lblActualNoOfCompaniesVal.Text = dtEmpCount.Rows(0)("CompaniesCount").ToString
                lblActualNoOfMobileDevicesVal.Text = dtEmpCount.Rows(0)("MobileDeviceCount").ToString
                lblActualNoOfMobileWorkLocationsVal.Text = dtEmpCount.Rows(0)("MobileWorkLocations").ToString
            End If
        End With

        If Not objversion.GetMaxNoOfEmployees = -1 Then
            Dim LicenseEmpNo As Integer = Convert.ToInt32(objversion.GetMaxNoOfEmployees)
            Dim CustomerEmpNo As Integer = Convert.ToInt32(dtEmpCount.Rows(0)("EmployeeCount").ToString())
            Dim NoDiff As Integer = LicenseEmpNo - CustomerEmpNo
            If NoDiff <= 10 Then
                lblActualNoOfEmployeesVal.ForeColor = Drawing.Color.Red
            End If
        End If
        If Not objversion.GetMaxNoOfUsers = -1 Then
            Dim LicenseUsrNo As Integer = Convert.ToInt32(objversion.GetMaxNoOfUsers)
            Dim CustomerUsrNo As Integer = Convert.ToInt32(dtEmpCount.Rows(0)("UsersCount").ToString())
            Dim NoDiff As Integer = LicenseUsrNo - CustomerUsrNo
            If NoDiff <= 10 Then
                lblActualNoOfUsersVal.ForeColor = Drawing.Color.Red
            End If
        End If

        If Not objversion.GetNoOfAllowedMobile = -1 Then
            Dim LicenseMobileNo As Integer = Convert.ToInt32(objversion.GetNoOfAllowedMobile)
            Dim CustomerMobileNo As Integer = Convert.ToInt32(dtEmpCount.Rows(0)("MobileDeviceCount").ToString())
            Dim NoDiff As Integer = LicenseMobileNo - CustomerMobileNo
            If NoDiff <= 10 Then
                lblActualNoOfMobileDevicesVal.ForeColor = Drawing.Color.Red
            End If
        End If

        If Not objversion.GetNoOfAllowedMobileWorkLocations = -1 Then
            Dim LicenseMobileWLNo As Integer = Convert.ToInt32(objversion.GetNoOfAllowedMobileWorkLocations)
            Dim CustomerMobileWLNo As Integer = Convert.ToInt32(dtEmpCount.Rows(0)("MobileWorkLocations").ToString())
            Dim NoDiff As Integer = LicenseMobileWLNo - CustomerMobileWLNo
            If NoDiff <= 10 Then
                lblActualNoOfMobileWorkLocationsVal.ForeColor = Drawing.Color.Red
            End If
        End If

        Dim LicenseCompanyNo As Integer = Convert.ToInt32(objversion.GetNoOfCompanies)
        Dim CustomerCompanyNo As Integer = Convert.ToInt32(dtEmpCount.Rows(0)("CompaniesCount").ToString())
        Dim CompanyNoDiff As Integer = LicenseCompanyNo - CustomerCompanyNo
        'If CompanyNoDiff = 0 Then
        '    lblActualNoOfCompaniesVal.ForeColor = Drawing.Color.Red
        'End If

        Dim DateDiff As System.TimeSpan
        Dim supportEndDate As DateTime
        supportEndDate = objversion.GetSupportEndDate
        DateDiff = supportEndDate.Subtract(Date.Today)
        If DateDiff.Days = 0 Or DateDiff.Days < 0 Then
            lblActualEndDateVal.ForeColor = Drawing.Color.Red
            lblActualEndDateVal.ToolTip = ResourceManager.GetString("RenewSupportContract")
        End If

        ShowDVC()

        If Lang = CtlCommon.Lang.AR Then
            lblContactUs.Text = "الرجاء الاتصال بقسم الدعم الفني في شركة أمان لنظم المعلومات و البرمجيات لإضافة ميزات، نحديث الرخصة و الدعم الفني  </br>"
            lblContactUs.Text += "هاتف :" + "+962-6-5338565</br>"
            lblContactUs.Text += "الدعم الفني : <a href='mailto:info@aman-me.com" + "?Subject=" + IIf(Lang = CtlCommon.Lang.AR, objAPP_Settings.companyArabicName1, objAPP_Settings.CompanyName1) + "'" + "target='_top' style='color:blue'>info@aman-me.com</a></br>"
            lblContactUs.Text += "او نفضلوا بزيارة موقعنا الالكتروني :<a href='http://www.aman-me.com/' target='_blank' style='color:blue'>www.aman-me.com</a></br>"
        Else
            lblContactUs.Text = "Please Contact Aman Information Systems Support For Adding Features, License Update Or Technical Supprot </br>"
            lblContactUs.Text += "Tel : +962-6-5338565</br>"
            lblContactUs.Text += "Technical Support : <a href='mailto:info@aman-me.com" + "?Subject=" + IIf(Lang = CtlCommon.Lang.AR, objAPP_Settings.companyArabicName1, objAPP_Settings.CompanyName1) + "'" + "target='_top' style='color:blue'>info@aman-me.com</a></br>"
            lblContactUs.Text += "Visit Our Website :<a href='http://www.aman-me.com/' target='_blank' style='color:blue'>www.aman-me.com</a></br>"
        End If

    End Sub

    Public Function SmartEncrypt(ByVal Cont As String) As String
        Dim strKeyConcat As String
        Dim strCipherTxt As String
        strKeyConcat = Cont


        ''strEncEmployees = objSecurity.strSmartEncrypt(strEmployees, strKeyPass)
        strCipherTxt = strSmartEncrypt(strKeyConcat, "MSS")
        strCipherTxt = CheckLastChars(strCipherTxt, "DEL")

        Return strCipherTxt
    End Function

    Private Function strSmartEncrypt(ByVal strPlainTxt As String, ByVal strPass As String) As String
        Dim strIV As String = "sh@n@0n5m@rtinfo" 'This must be exactly 16 bytes
        Dim intPasswordIterations As Integer = 3
        Dim intKeySize As Integer = 256 'it can be changed into 128 
        'Dim strSaltValue As String = "5m@rt@msv"
        Dim strSaltValue As String = "5m@rt@mss"

        Dim bytIV As Byte() = Encoding.UTF8.GetBytes(strIV)
        Dim bytSaltVal As Byte() = Encoding.UTF8.GetBytes(strSaltValue)
        Dim bytPlainTxt As Byte() = Encoding.UTF8.GetBytes(strPlainTxt)
        Dim password As New Rfc2898DeriveBytes(strPass, bytSaltVal, intPasswordIterations)
        Dim bytKey As Byte() = password.GetBytes(intKeySize / 8)
        Dim symmKey As New RijndaelManaged()
        symmKey.Mode = CipherMode.CBC
        Dim encryptor As ICryptoTransform = symmKey.CreateEncryptor(bytKey, bytIV)
        Dim memoryStream As New MemoryStream()
        Dim cryptoStream As New CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)
        ' ----------- Start encrypting ------------
        cryptoStream.Write(bytPlainTxt, 0, bytPlainTxt.Length)
        cryptoStream.FlushFinalBlock()
        Dim bytCipherTxt As Byte() = memoryStream.ToArray()
        memoryStream.Close()
        cryptoStream.Close()
        Dim strCipherTxt As String = Convert.ToBase64String(bytCipherTxt)
        Return strCipherTxt
    End Function

    Public Function CheckLastChars(ByVal Key As String, ByVal Type As String) As String
        Try
            Dim strLastChar, strLastChar2 As String
            Select Case Type
                Case "ADD"
                    strLastChar = Key.Last
                    If strLastChar = "1" Then
                        Key = Key.Remove(Key.Length - 1) + "="
                    ElseIf strLastChar = "2" Then
                        Key = Key.Remove(Key.Length - 1) + "=="
                    ElseIf strLastChar = "0" Then
                        Key = Key.Remove(Key.Length - 1)
                    End If
                Case "DEL"
                    strLastChar = Key.Last
                    strLastChar2 = Key.Chars(Key.Length - 2)
                    If strLastChar = "=" AndAlso strLastChar2 = "=" Then
                        Key = Key.Remove(Key.Length - 2, 2) + "2"
                    ElseIf strLastChar = "=" Then
                        Key = Key.Remove(Key.Length - 1) + "1"
                    Else
                        Key = Key + "0"
                    End If
            End Select

            Return Key

        Catch ex As Exception
            Return Key
        End Try
    End Function

    Public Function SmartDecrypt(ByVal strCipherTxt As String) As String
        Dim strPlainTxt As String
        strCipherTxt = CheckLastChars(strCipherTxt, "ADD")
        strPlainTxt = strSmartDecrypt(strCipherTxt, "MSS")

        Return strPlainTxt
    End Function

    Private Function strSmartDecrypt(ByVal strCipherTxt As String, ByVal strPass As String) As String
        'Dim strIV As String = "ev!$10n5m@rtinfo"
        Dim strIV As String = "sh@n@0n5m@rtinfo"
        Dim intPasswordIterations As Integer = 3
        Dim intKeySize As Integer = 256
        'Dim strSaltValue As String = "5m@rt@msv"
        Dim strSaltValue As String = "5m@rt@mss"


        Dim bytIV As Byte() = Encoding.UTF8.GetBytes(strIV)
        Dim bytSaltVal As Byte() = Encoding.UTF8.GetBytes(strSaltValue)
        Dim bytCipherTxt As Byte() = Convert.FromBase64String(strCipherTxt)

        Dim password As New Rfc2898DeriveBytes(strPass, bytSaltVal, intPasswordIterations)
        Dim bytKeyBytes As Byte() = password.GetBytes(intKeySize / 8)

        Dim symmetricKey As New RijndaelManaged()
        symmetricKey.Mode = CipherMode.CBC
        Dim decryptor As ICryptoTransform = symmetricKey.CreateDecryptor(bytKeyBytes, bytIV)
        Dim memoryStream As New MemoryStream(bytCipherTxt)
        Dim cryptoStream As New CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read)
        Dim bytplainTxt As Byte() = New Byte(bytCipherTxt.Length) {}
        ' --------------------- Start decrypting ---------------------
        Dim intDecryptedByteCount As Integer = cryptoStream.Read(bytplainTxt, 0, bytplainTxt.Length)
        memoryStream.Close()
        cryptoStream.Close()
        Dim strPlainTxt As String = Encoding.UTF8.GetString(bytplainTxt, 0, intDecryptedByteCount)

        Dim strKeyGenerated() As String
        strKeyGenerated = strPlainTxt.Split("*")
        _MacAddress = strKeyGenerated(0).ToString  '---MacAddress---'
        _Version = strKeyGenerated(15).ToString '---VersionNo.---'

        Return strPlainTxt
    End Function

    Private Sub ShowDVC()
        Dim strVerificationSerial As String
        objversion = New SmartV.Version.version
        objAPP_Settings = New APP_Settings
        objAPP_Settings.GetByPK()


        SmartDecrypt(ConfigurationManager.AppSettings("LicenseKey"))
        If _MacAddress = objversion.GetMACAddress Then
            If _Version = objversion.GetVersionNumber Then
                strVerificationSerial = SmartEncrypt(objversion.GetMACAddress() + "*" + objversion.GetVersionNumber + "*" + objAPP_Settings.CompanyName1)
                lblDVCVal.Text = strVerificationSerial
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DSNNotVerified", CultureInfo), "info")
            End If
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DSNNotVerified", CultureInfo), "info")
        End If
    End Sub

#End Region

End Class
