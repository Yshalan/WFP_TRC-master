Imports System
Imports System.IO
Imports System.Text
Imports System.Diagnostics
Imports System.Collections
Imports System.Collections.Specialized
Imports System.Security.Cryptography
Imports System.Web
Imports System.Reflection

Namespace SmartV.UTILITIES

    Public Class SecureUrl

        Private Const ENCRYPTED_KEY_NAME As String = "param"
        Private Const RETURN_URL As String = "returnUrl"
        Private Const RETURN_PARAM As String = "returnParam"

        'DES Key size is 64 bit or 8 byte
        Private Shared ReadOnly Key As Byte() = New Byte() {33, 93, 171, 1, 85, 23, 231, 145}

        Private _pageUrl As String = String.Empty
        Private _queryString As New StringDictionary()

        Default Public Property Item(ByVal key As String) As String
            <DebuggerStepThrough()> _
            Get
                If _queryString.ContainsKey(key.ToLower()) Then
                    Return _queryString(key)
                End If

                Return String.Empty
            End Get
            <DebuggerStepThrough()> _
            Set(ByVal value As String)
                If (value IsNot Nothing) AndAlso (value.Trim().Length > 0) Then
                    _queryString(key.ToLower()) = value
                Else
                    _queryString.Remove(key.ToLower())
                End If
            End Set
        End Property

        Public Property ReturnUrl() As String
            <DebuggerStepThrough()> _
            Get
                If _queryString.ContainsKey(RETURN_URL) Then
                    Return _queryString(RETURN_URL)
                End If

                Return String.Empty
            End Get
            <DebuggerStepThrough()> _
            Set(ByVal value As String)
                If (value IsNot Nothing) AndAlso (value.Trim().Length > 0) Then
                    _queryString(RETURN_URL) = value
                Else
                    _queryString.Remove(RETURN_URL)
                End If
            End Set
        End Property

        Public Property ReturnParameters() As String
            <DebuggerStepThrough()> _
            Get
                If _queryString.ContainsKey(RETURN_PARAM) Then
                    Return _queryString(RETURN_PARAM)
                End If

                Return String.Empty
            End Get
            <DebuggerStepThrough()> _
            Set(ByVal value As String)
                If (value IsNot Nothing) AndAlso (value.Trim().Length > 0) Then

                    _queryString(RETURN_PARAM) = value
                Else
                    _queryString.Remove(RETURN_PARAM)
                End If
            End Set
        End Property

        Public Sub New(ByVal fullUrl As String)
            Try
                If fullUrl.IndexOf("?"c) > 0 Then
                    Dim urlAndQueryString As String() = fullUrl.Split("?"c)
                    _pageUrl = urlAndQueryString(0)
                    Dim k As String = fullUrl.IndexOf("?"c)

                    If urlAndQueryString.Length > 1 Then
                        Parse(urlAndQueryString(1))
                    End If
                Else
                    _pageUrl = fullUrl
                End If
            Catch ex As Exception
                CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
        End Sub

        Public Sub New(ByVal url As String, ByVal queryString As String)
            Try
                _pageUrl = url
                Parse(queryString)
            Catch ex As Exception
                CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
        End Sub

        <DebuggerStepThrough()> _
        Public Sub Clear()
            _queryString.Clear()
        End Sub

        <DebuggerStepThrough()> _
        Public Function Contains(ByVal key As String) As Boolean
            Return _queryString.ContainsKey(key.ToLower())
        End Function

        <DebuggerStepThrough()> _
        Public Sub Remove(ByVal key As String)
            _queryString.Remove(key)
        End Sub

        <DebuggerStepThrough()> _
        Public Overloads Overrides Function ToString() As String
            Return ToStrings(False)
        End Function

        <DebuggerStepThrough()> _
        Public Function ToStrings(ByVal unicode As Boolean) As String
            Dim queryString As New StringBuilder()

            For Each entry As DictionaryEntry In _queryString
                queryString.Append(entry.Key.ToString().ToLower())
                queryString.Append("="c)
                queryString.Append(HttpUtility.UrlEncode(entry.Value.ToString()))
                queryString.Append("&"c)
            Next

            If queryString.Length > 0 Then
                'Remove the last &
                queryString.Remove(queryString.Length - 1, 1)
            End If

            Dim encryptedQuery As String = Encrypt(queryString.ToString())

            Dim encodedQuery As String = HttpUtility.UrlEncode(encryptedQuery)

            If unicode Then
                'Required if we are passing it to JavaScript
                encodedQuery = HttpUtility.UrlEncodeUnicode(encodedQuery)
            End If

            Dim result As String = String.Empty

            If encodedQuery.Length > 0 Then
                result = ((_pageUrl & "?") + ENCRYPTED_KEY_NAME & "=") + encodedQuery
            Else
                result = _pageUrl
            End If

            Return result
        End Function

        <DebuggerStepThrough()> _
        Private Sub Parse(ByVal queryString As String)
            Dim pairs As String() = queryString.Split("&"c)

            If (pairs IsNot Nothing) AndAlso (pairs.Length > 0) Then
                For i As Integer = 0 To pairs.Length - 1
                    Dim pair As String() = pairs(i).Split("="c)
                    Dim key As String = pair(0)
                    Dim value As String = pair(1)

                    value = HttpUtility.UrlDecode(value)

                    If String.Compare(key, ENCRYPTED_KEY_NAME, True) = 0 Then
                        If value.Length > 0 Then
                            Dim decryptedString As String = HttpUtility.UrlDecode(Decrypt(value))

                            Dim keyAndValues As String() = decryptedString.Split("&"c)

                            If (keyAndValues IsNot Nothing) AndAlso (keyAndValues.Length > 0) Then
                                For j As Integer = 0 To keyAndValues.Length - 1
                                    Dim keyAndValue As String() = keyAndValues(j).Split("="c)

                                    If (keyAndValue IsNot Nothing) AndAlso (keyAndValue.Length = 2) Then
                                        _queryString.Add(keyAndValue(0).ToLower(), keyAndValue(1))
                                    End If
                                Next
                            End If
                        End If
                    Else
                        _queryString.Add(key.ToLower(), value)
                    End If
                Next
            End If
        End Sub

        <DebuggerStepThrough()> _
        Private Shared Function Encrypt(ByVal plain As String) As String
            If (plain Is Nothing) OrElse (plain.Length = 0) Then
                Return Nothing
            End If

            Using crypto As SymmetricAlgorithm = CreateCrypto()
                Return System.Convert.ToBase64String(Read(crypto.CreateEncryptor(), Encoding.ASCII.GetBytes(plain)))
            End Using
        End Function

        <DebuggerStepThrough()> _
        Private Shared Function Decrypt(ByVal cipher As String) As String
            If (cipher Is Nothing) OrElse (cipher.Length = 0) Then
                Return Nothing
            End If

            Using crypto As SymmetricAlgorithm = CreateCrypto()
                Return Encoding.ASCII.GetString(Read(crypto.CreateDecryptor(), System.Convert.FromBase64String(cipher)))
            End Using
        End Function

        <DebuggerStepThrough()> _
        Private Shared Function CreateCrypto() As SymmetricAlgorithm
            'Using DES as it is the fastest among the others
            Dim crypto As SymmetricAlgorithm = New DESCryptoServiceProvider()

            crypto.Key = Key
            crypto.IV = New Byte(crypto.IV.Length - 1) {}

            Return crypto
        End Function

        <DebuggerStepThrough()> _
        Private Shared Function Read(ByVal transformer As ICryptoTransform, ByVal data As Byte()) As Byte()
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, transformer, CryptoStreamMode.Write)
                    cs.Write(data, 0, data.Length)
                    cs.FlushFinalBlock()
                    Return ms.ToArray()
                End Using
            End Using
        End Function

    End Class

End Namespace