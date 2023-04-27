Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.Configuration

Imports System.Security.Cryptography

Namespace SmartV.UTILITIES


    Public NotInheritable Class SmartSecurity
        Public Function strHashPassword(ByVal strPassword As String, ByVal strUserName As String) As String
            If strPassword.Trim.Length > 0 AndAlso strUserName.Trim.Length > 0 Then
                Dim sPlainWithSalt As String = String.Concat(strPassword, strUserName.ToUpper.Substring(0, strUserName.Length - 1))
                Dim plainTextWithSaltBytes() As Byte = Encoding.UTF8.GetBytes(sPlainWithSalt)
                Dim hash As HashAlgorithm = New MD5CryptoServiceProvider()
                Dim hashBytes As Byte() = hash.ComputeHash(plainTextWithSaltBytes)
                Return BitConverter.ToString(hashBytes)
            Else
                Return String.Empty
            End If
        End Function

        Public Function blnVerifyHash(ByVal strPassword As String, ByVal strUserName As String, ByVal strHashValue As String) As Boolean
            If strPassword.Trim.Length > 0 AndAlso strUserName.Trim.Length > 0 AndAlso strHashValue.Trim.Length > 0 Then
                Dim expectedHashString As String = strHashPassword(strPassword, strUserName.ToUpper)
                Return (strHashValue = expectedHashString)
            Else
                Return False
            End If
        End Function

        Public Function strSmartEncrypt(ByVal strPlainTxt As String, ByVal strPass As String) As String
            Dim strIV As String = "ev!$10n5m@rtinfo" 'This must be exactly 16 bytes
            Dim intPasswordIterations As Integer = 3
            Dim intKeySize As Integer = 256 'it can be changed into 128 
            Dim strSaltValue As String = "5m@rt@man"

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

        Public Function strSmartDecrypt(ByVal strCipherTxt As String, ByVal strPass As String) As String
            Dim strIV As String = "ev!$10n5m@rtinfo"
            Dim intPasswordIterations As Integer = 3
            Dim intKeySize As Integer = 256
            Dim strSaltValue As String = "5m@rt@man"

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
            Return strPlainTxt
        End Function

        Public Function blnEncConnStr(ByVal strAppPath As String) As Boolean
            Dim config As Configuration = WebConfigurationManager.OpenWebConfiguration(strAppPath)
            Dim section As ConfigurationSection = config.GetSection("connectionStrings")
            With section
                If Not (.SectionInformation.IsProtected) Then
                    .SectionInformation.ProtectSection("DataProtectionConfigurationProvider")
                    config.Save()
                    Return True
                End If
            End With
            Return False
        End Function

        Public Function blnDecConnStr(ByVal strAppPath As String) As Boolean
            Dim config As Configuration = WebConfigurationManager.OpenWebConfiguration(strAppPath)
            Dim section As ConfigurationSection = config.GetSection("connectionStrings")
            With section
                If (section.SectionInformation.IsProtected) Then
                    .SectionInformation.UnprotectSection()
                    config.Save()
                    Return True
                End If
            End With
            Return False
        End Function

        '------------------ClientDetails Decryption------------------
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

        Public Function SmartDecrypt_ClientDetails(ByVal strCipherTxt As String) As String
            Dim strPlainTxt As String
            strCipherTxt = CheckLastChars(strCipherTxt, "ADD")
            strPlainTxt = strSmartDecrypt_ClientDetails(strCipherTxt, "MSS")

            Return strPlainTxt
        End Function

        Private Function strSmartDecrypt_ClientDetails(ByVal strCipherTxt As String, ByVal strPass As String) As String
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
            Return strPlainTxt
        End Function
        '------------------ClientDetails Decryption------------------
    End Class
End Namespace