Imports Microsoft.VisualBasic
Imports System.Configuration
Imports SmartV.UTILITIES
Imports TA.Employees
Imports System.Data
Imports TA.Admin
Imports System.Globalization

Namespace SmartV.Version
    Public Class version

#Region "Functions"

        'Get Version Type From Web Config
        Public Shared Function GetVersionType() As Integer
            Dim VersionType As Integer
            'VersionType = CInt(ConfigurationManager.AppSettings("VersionType").ToString())
            Dim LicenseKey As String
            Dim DecLicenseKey As String
            Dim objSecurity As New STSupremeKeyValidation.STSupremeKeyValidation.SmartKeyValidation

            LicenseKey = ConfigurationManager.AppSettings("LicenseKey").ToString()

            DecLicenseKey = objSecurity.SmartDecrypt(LicenseKey)
            VersionType = CInt(objSecurity.AppPackage)

            Return VersionType
        End Function

        'Get Server MAC Address
        Public Shared Function GetMACAddress() As String
            Dim strMACAddress As String
            'VersionType = CInt(ConfigurationManager.AppSettings("VersionType").ToString())
            Dim LicenseKey As String
            Dim DecLicenseKey As String
            Dim objSecurity As New STSupremeKeyValidation.STSupremeKeyValidation.SmartKeyValidation

            LicenseKey = ConfigurationManager.AppSettings("LicenseKey").ToString()

            DecLicenseKey = objSecurity.SmartDecrypt(LicenseKey)
            strMACAddress = "8CEC4BB356BA"
            'objSecurity.SerialNo

            Return strMACAddress
        End Function

        ''Get Bit If Is Is Has MultiCompanies
        'Public Shared Function HasMultiCompany() As Boolean
        '    Dim HasMultiCompanies As Boolean
        '    If (GetVersionType() = 1 Or GetVersionType() = 2) Then
        '        HasMultiCompanies = False
        '    Else
        '        HasMultiCompanies = True
        '    End If

        '    Return HasMultiCompanies
        'End Function

        Public Shared Function HasMultiCompany() As Boolean 'Get Bit If Is Is Has MultiCompanies From License
            Dim HasMultiCompanies As Boolean
            If SessionVariables.LicenseDetails.NoOfCompanies > 1 Then
                HasMultiCompanies = True
            Else
                HasMultiCompanies = False
            End If

            Return HasMultiCompanies
        End Function

        'Get Company Id
        Public Shared Function GetCompanyId() As Integer
            Dim objOrgCompany As New OrgCompany
            Dim dtCompanies As DataTable
            Dim dtTempCompanies As New DataTable
            Dim drCompanyRow As DataRow

            dtCompanies = New DataTable
            dtTempCompanies = objOrgCompany.GetAll()
            drCompanyRow = dtTempCompanies.Select()(0)
            Dim CompanyId As Integer = drCompanyRow(0)
            Return CompanyId
        End Function

        'Modules Per Version
        Public Shared Function GetModulesPerVersion(ByVal intVersion As Integer) As String
            Dim strModule As String = ""
            'Select Case intVersion
            '    Case 1 'Basic
            '        strModule = ",1,2,3,4,5,6,7,"
            '    Case 2 'Standard
            '        strModule = ",1,2,3,4,5,6,7,8,9,"
            '    Case 3 'Enterprise
            '        strModule = ",1,2,3,4,5,6,7,8,9,"
            '    Case Else
            '        strModule = ""
            'End Select
            Dim LicenseKey As String
            Dim DecLicenseKey As String
            Dim objSecurity As New STSupremeKeyValidation.STSupremeKeyValidation.SmartKeyValidation

            LicenseKey = ConfigurationManager.AppSettings("LicenseKey").ToString()

            DecLicenseKey = objSecurity.SmartDecrypt(LicenseKey)
            strModule = "," + objSecurity.ModuleIds + ","
            Return strModule
        End Function

        'Modules Per License
        Public Shared Function GetModulesPerLicense() As String
            Dim strModule As String = ""

            Dim LicenseKey As String
            Dim DecLicenseKey As String
            Dim StrForms As String
            Dim objSecurity As New STSupremeKeyValidation.STSupremeKeyValidation.SmartKeyValidation
            Dim BuildMenu As New SmartV.Security.MENU.SYSMenuCreator

            LicenseKey = ConfigurationManager.AppSettings("LicenseKey").ToString()
            DecLicenseKey = objSecurity.SmartDecrypt(LicenseKey)
            StrForms = SessionVariables.LicenseDetails.FormIds
            Dim dt As DataTable
            dt = BuildMenu.GetModuleByFormId(StrForms)
            For Each row In dt.Rows
                strModule = strModule + "," + row("ModuleId").ToString
            Next
            Return strModule
        End Function

        ''Fill Version Details
        'Public Shared Sub FillVersionDetails()
        '    'Dim strMACAddress As String
        '    'VersionType = CInt(ConfigurationManager.AppSettings("VersionType").ToString())
        '    Dim LicenseKey As String
        '    Dim DecLicenseKey As String
        '    Dim objSecurity As New STSupremeKeyValidation.STSupremeKeyValidation.SmartKeyValidation

        '    LicenseKey = ConfigurationManager.AppSettings("LicenseKey").ToString()
        '    DecLicenseKey = objSecurity.SmartDecrypt(LicenseKey)
        '    SessionVariables.LicenseDetails = objSecurity

        'End Sub

        'Fill Version Details
        Public Shared Sub FillVersionDetails()
            'Dim strMACAddress As String
            'VersionType = CInt(ConfigurationManager.AppSettings("VersionType").ToString())
            Dim LicenseKey As String
            Dim DecLicenseKey As String
            Dim objSmartKey As New STSupremeKeyValidation.STSupremeKeyValidation.SmartKeyValidation

            LicenseKey = ConfigurationManager.AppSettings("LicenseKey").ToString()
            DecLicenseKey = objSmartKey.SmartDecrypt(LicenseKey)

            SessionVariables.LicenseDetails = objSmartKey

        End Sub

        'Check License Validity
        Public Shared Function CheckValidity() As String

            Dim strErrorMessage As String = ""
            Dim strMACAddress As String = ""
            Dim intCountEmployees As Integer
            Dim intCountUsers As Integer
            Dim intCountCompny As Integer
            Dim IsTrial As Boolean = GetIsTrial()
            Dim SupportEndDate As Date = GetSupportEndDate()
            Dim strSupportEndDate As String = SupportEndDate.ToString("yyyy-MMM-dd", CultureInfo.CreateSpecificCulture("en-US"))
            Dim objSec As New STSupremeKeyValidation.STSupremeKeyValidation.SmartKeyValidation
            strMACAddress = objSec.GetMacAddress
            Try
                FillVersionDetails()

                Dim objSmartSecurity As New SmartSecurity
                If Not ConfigurationManager.AppSettings("ClientDetails") = Nothing Then
                    Dim Decrypted_ClientDetails As String = objSmartSecurity.SmartDecrypt_ClientDetails(ConfigurationManager.AppSettings("ClientDetails"))

                    If Decrypted_ClientDetails.ToString.Trim <> (System.Net.Dns.GetHostName).ToString.Trim Then
                        If strMACAddress <> SessionVariables.LicenseDetails.GetMacAddress Then 'Bypass MAC Address Validation
                            'If strMACAddress <> SessionVariables.LicenseDetails.SerialNo Then
                            If SessionVariables.CultureInfo = "ar-JO" Then
                                strErrorMessage = "الر جاء تحديث رخصة النظام، عنوان  ال MAC  خطأ، الرقم المتسلسل : " + strMACAddress.ToString()
                            Else
                                strErrorMessage = "Please Update License Key For Your Application, Invalid MAC Address. Serial No is: " + strMACAddress.ToString()
                            End If
                            Exit Try
                        End If
                    End If
                Else
                    If strMACAddress <> SessionVariables.LicenseDetails.GetMacAddress Then 'Bypass MAC Address Validation
                        'If strMACAddress <> SessionVariables.LicenseDetails.SerialNo Then
                        If SessionVariables.CultureInfo = "ar-JO" Then
                            strErrorMessage = "الر جاء تحديث رخصة النظام، عنوان  ال MAC  خطأ، الرقم المتسلسل : " + strMACAddress.ToString()
                        Else
                            strErrorMessage = "Please Update License Key For Your Application, Invalid MAC Address. Serial No is: " + strMACAddress.ToString()
                        End If

                        Exit Try
                    End If
                End If

                If IsTrial = True Then
                    If SupportEndDate < Date.Today Then
                        If SessionVariables.CultureInfo = "ar-JO" Then
                            strErrorMessage = "الر جاء تحديث رخصة النظام، فترة تجريب النظام انتهت في تاريخ  : " + strSupportEndDate
                        Else
                            strErrorMessage = "Please Update License Key For Your Application, Trail Period Has Expired in " + strSupportEndDate
                        End If
                    End If
                End If
                Dim objEmployee As New Employee
                Dim dtUsersCount As New DataTable

                dtUsersCount = objEmployee.GetCountEmployeesAndUsers()
                If DTable.IsValidDataTable(dtUsersCount) Then
                    intCountEmployees = dtUsersCount.Rows(0)("EmployeeCount").ToString()
                    intCountUsers = dtUsersCount.Rows(0)("UsersCount").ToString()
                    intCountCompny = dtUsersCount.Rows(0)("CompaniesCount").ToString
                End If

                'If Not SessionVariables.LicenseDetails.Employees = -1 Then
                '    If CInt(SessionVariables.LicenseDetails.Employees) < intCountEmployees Then
                '        ' If 30000 < intCountEmployees Then
                '        If SessionVariables.CultureInfo = "ar-JO" Then
                '            strErrorMessage = "الر جاء تحديث رخصة النظام، عدد الموظفين تجاوز رخصة النظام. الرقم المتسلسل : " + strMACAddress.ToString()
                '        Else
                '            strErrorMessage = "Please Update Your License Key, Number of Employees Exceeded License Limit. Serial No is: " + strMACAddress.ToString()
                '        End If
                '        Exit Try
                '    End If
                'End If

                If CInt(SessionVariables.LicenseDetails.NoOfCompanies) < intCountCompny Then
                    If SessionVariables.CultureInfo = "ar-JO" Then
                        strErrorMessage = "الر جاء تحديث رخصة النظام، عدد الشركات تجاوز رخصة النظام. الرقم المتسلسل : " + strMACAddress.ToString()
                    Else
                        strErrorMessage = "Please Update Your License Key, Number of Company(s) Exceeded License Limit. Serial No is: " + strMACAddress.ToString()
                    End If
                    Exit Try
                End If

                If (SessionVariables.LicenseDetails.VersionName) <> GetVersionNumber() Then
                    If SessionVariables.CultureInfo = "ar-JO" Then
                        strErrorMessage = "الرجاء تحديث رخصة النظام، رقم نسخة النظام لاتطابق النسخة التي تم تنزيلها " + GetVersionNumber() + ", "
                        strErrorMessage += "الرقم المتسلسل: " + strMACAddress.ToString()
                    Else
                        strErrorMessage = "Please Update Your License Key, License Key Version No. Does Not Match The Installed Version No." + GetVersionNumber() + ", "
                        strErrorMessage += "Serial No is: " + strMACAddress.ToString()
                    End If
                    Exit Try
                End If

                'If Not SessionVariables.LicenseDetails.Users = -1 Then
                '    If CInt(SessionVariables.LicenseDetails.Users) < intCountUsers Then
                '        If SessionVariables.CultureInfo = "ar-JO" Then
                '            strErrorMessage = "الر جاء تحديث رخصة النظام، عدد المستخدمين تجاوز رخصة النظام. الرقم المتسلسل :" + strMACAddress.ToString()
                '        Else
                '            strErrorMessage = "Please Update Your License Key, Number of Users Exceeded License Limit. Serial No is: " + strMACAddress.ToString()
                '        End If
                '        Exit Try
                '    End If
                'End If

            Catch ex As Exception
                If SessionVariables.CultureInfo = "ar-JO" Then
                    strErrorMessage = "الر جاء تحديث رخصة النظام. الرقم المتسلسل :" + strMACAddress.ToString()
                Else
                    strErrorMessage = "Please Update License Key for Your application. Serial No is: " + strMACAddress.ToString()
                End If
                intCountEmployees = 0
                intCountUsers = 0
            End Try
            Return strErrorMessage
        End Function

        'Get Max Users to be added
        Public Shared Function GetMaxNoOfUsers() As Integer

            Dim strNoOfUsers As String
            Dim intNo As Integer = 0

            FillVersionDetails()
            Try
                strNoOfUsers = SessionVariables.LicenseDetails.Users
                If strNoOfUsers = "Unlimitied" Then
                    intNo = -1
                Else
                    intNo = CInt(strNoOfUsers)
                End If
            Catch ex As Exception

            End Try

            Return intNo
        End Function

        'Get Max Employees to be added
        Public Shared Function GetMaxNoOfEmployees() As Integer
            Dim strNoOfEmp As String
            Dim intNo As Integer = 0

            FillVersionDetails()
            Try
                strNoOfEmp = SessionVariables.LicenseDetails.Employees
                If strNoOfEmp = "Unlimitied" Then
                    intNo = -1
                Else
                    intNo = CInt(strNoOfEmp)
                End If
            Catch ex As Exception

            End Try

            Return intNo

        End Function

        'Get Max Employees to be added
        Public Shared Function GetNoOfEmployees() As Integer
            Dim intNo As Integer = 0

            FillVersionDetails()
            Try
                intNo = SessionVariables.LicenseDetails.Employees

            Catch ex As Exception

            End Try

            Return intNo

        End Function

        'Get No Of Companies to be added
        Public Shared Function GetNoOfCompanies() As Integer
            Dim intNo As Integer = 0

            FillVersionDetails()
            Try
                intNo = SessionVariables.LicenseDetails.NoOfCompanies

            Catch ex As Exception

            End Try

            Return intNo

        End Function

        'Get No Of Users to be added
        Public Shared Function GetNoOfUsers() As Integer
            Dim intNo As Integer = 0

            FillVersionDetails()
            Try
                intNo = SessionVariables.LicenseDetails.Users

            Catch ex As Exception

            End Try

            Return intNo

        End Function

        Public Shared Function GetIsTrial() As Boolean
            Dim strIsTrial As String = ""
            Dim IsTrial As Boolean = False
            FillVersionDetails()
            Try
                strIsTrial = SessionVariables.LicenseDetails.IsTrial
                IsTrial = IIf(strIsTrial = "True", True, False)
            Catch ex As Exception

            End Try

            Return IsTrial

        End Function

        Public Shared Function GetSupportEndDate() As Date
            Dim strSupportEndDate As String = ""
            Dim SupportEndDate As Date
            FillVersionDetails()
            Try
                strSupportEndDate = SessionVariables.LicenseDetails.SupportEndDate
                SupportEndDate = Date.Parse(strSupportEndDate)
            Catch ex As Exception

            End Try

            Return SupportEndDate

        End Function

        Public Shared Function HasMobileApplication() As Boolean
            Dim strHasMobileApp As String = ""
            Dim HasMobileApp As Boolean = False
            FillVersionDetails()
            Try
                strHasMobileApp = SessionVariables.LicenseDetails.HasMobileApp
                HasMobileApp = IIf(strHasMobileApp = "False", False, True)
            Catch ex As Exception

            End Try

            Return HasMobileApp

        End Function

        Public Shared Function GetNoOfAllowedMobile() As Integer
            Dim intNo As Integer = 0

            FillVersionDetails()
            Try
                intNo = SessionVariables.LicenseDetails.NoOfMobileDevices
                If intNo = "Unlimitied" Then
                    intNo = -1
                Else
                    intNo = CInt(intNo)
                End If
            Catch ex As Exception

            End Try

            Return intNo

        End Function

        Public Shared Function GetNoOfAllowedMobileWorkLocations() As Integer
            Dim intNo As Integer = 0

            FillVersionDetails()
            Try
                intNo = SessionVariables.LicenseDetails.NoOfMobileWorkLocations
                If intNo = "Unlimitied" Then
                    intNo = -1
                Else
                    intNo = CInt(intNo)
                End If
            Catch ex As Exception

            End Try

            Return intNo

        End Function

        Public Shared Function GetNotificationTypes() As String
            Dim strNotifications As String = 0

            FillVersionDetails()
            Try
                strNotifications = SessionVariables.LicenseDetails.NotificationTypes

            Catch ex As Exception

            End Try
            'strNotifications = "1,2,3,4,5,25"
            Return strNotifications

        End Function

        Public Shared Function GetVersionNumber() As String
            Dim strVersionNumber As String = 0
            Try
                strVersionNumber = "3.8.29"

            Catch ex As Exception

            End Try

            Return strVersionNumber

        End Function

        Public Shared Function GetEnCopyRights_Url() As String
            Dim strCopyRights As String = 0
            Dim objSmartSecurity As New SmartSecurity

            Try
                strCopyRights = objSmartSecurity.strSmartDecrypt(ConfigurationManager.AppSettings("CopyRightEn_Url").ToString(), "SmartVision")

            Catch ex As Exception

            End Try

            Return strCopyRights

        End Function

        Public Shared Function GetArCopyRights_Url() As String
            Dim strCopyRights As String = 0
            Dim objSmartSecurity As New SmartSecurity
            Try
                strCopyRights = objSmartSecurity.strSmartDecrypt(ConfigurationManager.AppSettings("CopyRightAr_Url").ToString(), "SmartVision")

            Catch ex As Exception

            End Try

            Return strCopyRights

        End Function

        Public Shared Function GetEnCopyRights() As String
            Dim strCopyRights As String = 0
            Dim objSmartSecurity As New SmartSecurity

            Try
                strCopyRights = objSmartSecurity.strSmartDecrypt(ConfigurationManager.AppSettings("CopyRightEn").ToString(), "SmartVision")
                'strCopyRights = "Aman Information Systems 2016©" '& " <a href='www.Aman-me.com' style='color:blue'>www.Aman-me.com</a>"

            Catch ex As Exception

            End Try

            Return strCopyRights

        End Function

        Public Shared Function GetArCopyRights() As String
            Dim strCopyRights As String = 0
            Dim objSmartSecurity As New SmartSecurity
            Try
                strCopyRights = objSmartSecurity.strSmartDecrypt(ConfigurationManager.AppSettings("CopyRightAr").ToString(), "SmartVision")
                'strCopyRights = "جميع الحقوق محفوظة لدى شركة أمان لنظم البرمجيات 2016©" '& " <a href='www.Aman-me.com' style='color:blue'>www.Aman-me.com</a>"

            Catch ex As Exception

            End Try

            Return strCopyRights

        End Function

        Public Shared Function HasTawajudFeatures() As Boolean
            Dim strHasTawajudFeatures As String = ""
            Dim boolHasTawajudFeatures As Boolean = False
            FillVersionDetails()
            Try
                strHasTawajudFeatures = SessionVariables.LicenseDetails.HasTawajudFeatures
                boolHasTawajudFeatures = IIf(strHasTawajudFeatures = "False", False, True)
            Catch ex As Exception

            End Try

            Return boolHasTawajudFeatures

        End Function

        Public Shared Function HasMultiLocations() As Boolean
            Dim strHasMultiLocations As String = ""
            Dim boolHasMultiLocations As Boolean = False
            FillVersionDetails()
            Try
                strHasMultiLocations = SessionVariables.LicenseDetails.HasMultiLocations
                boolHasMultiLocations = IIf(strHasMultiLocations = "False", False, True)
            Catch ex As Exception

            End Try

            Return boolHasMultiLocations

        End Function

        Public Shared Function HasHeartBeat() As Boolean
            Dim strHasHeartBeat As String = ""
            Dim boolHasHeartBeat As Boolean = False
            FillVersionDetails()
            Try
                strHasHeartBeat = SessionVariables.LicenseDetails.HasHeartBeat
                boolHasHeartBeat = IIf(strHasHeartBeat = "False", False, True)
            Catch ex As Exception

            End Try

            Return boolHasHeartBeat

        End Function

        Public Shared Function HasFeedback() As Boolean
            Dim strHasFeedback As String = ""
            Dim boolHasFeedback As Boolean = False
            FillVersionDetails()
            Try
                strHasFeedback = SessionVariables.LicenseDetails.HasFeedback
                boolHasFeedback = IIf(strHasFeedback = "False", False, True)
            Catch ex As Exception

            End Try

            Return boolHasFeedback

        End Function

        Public Shared Function AllowFingerPunch() As Boolean
            Dim strAllowFingerPunch As String = ""
            Dim boolAllowFingerPunch As Boolean = False
            FillVersionDetails()
            Try
                strAllowFingerPunch = SessionVariables.LicenseDetails.AllowFingerPunch
                boolAllowFingerPunch = IIf(strAllowFingerPunch = "False", False, True)
            Catch ex As Exception

            End Try

            Return boolAllowFingerPunch

        End Function

        Public Shared Function AllowFingerLogin() As Boolean
            Dim strAllowFingerLogin As String = ""
            Dim boolAllowFingerLogin As Boolean = False
            FillVersionDetails()
            Try
                strAllowFingerLogin = SessionVariables.LicenseDetails.AllowFingerLogin
                boolAllowFingerLogin = IIf(strAllowFingerLogin = "False", False, True)
            Catch ex As Exception

            End Try

            Return boolAllowFingerLogin

        End Function

        Public Shared Function AllowFacePunch() As Boolean
            Dim strAllowFacePunch As String = ""
            Dim boolAllowFacePunch As Boolean = False
            FillVersionDetails()
            Try
                strAllowFacePunch = SessionVariables.LicenseDetails.AllowFacePunch
                boolAllowFacePunch = IIf(strAllowFacePunch = "False", False, True)
            Catch ex As Exception

            End Try

            Return boolAllowFacePunch

        End Function

        Public Shared Function AllowFaceLogin() As Boolean
            Dim strAllowFaceLogin As String = ""
            Dim boolAllowFaceLogin As Boolean = False
            FillVersionDetails()
            Try
                strAllowFaceLogin = SessionVariables.LicenseDetails.AllowFaceLogin
                boolAllowFaceLogin = IIf(strAllowFaceLogin = "False", False, True)
            Catch ex As Exception

            End Try

            Return boolAllowFaceLogin

        End Function

#End Region

    End Class
End Namespace