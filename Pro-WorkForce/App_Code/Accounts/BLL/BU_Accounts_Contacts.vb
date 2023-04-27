Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports SmartV.DB

Namespace TA.Accounts

    Public Class BU_Accounts_Contacts

#Region "Class Variables"

        Private _FK_AccountTypeId As Integer
        Private _ContactId As Integer
        Private _FK_AccountId As Integer
        Private _ContactName As String
        Private _ContactTitle As String
        Private _ContactFax As String
        Private _ContactTelephone As String
        Private _ContactMobile As String
        Private _ContactEmail As String
        Private _Remarks As String
        Private _HasCredintials As Boolean
        Private _UserName As String
        Private _Password As String
        Private _OldPassword As String
        Private _SecurityQuestion As String
        Private _SecurityAnswer As String
        Private _PharmaLicNo As String
        Private _PharmaExpDate As String
        Private _IsApproved As Boolean
        Private _IsActive As Boolean
        Private _IsMainContact As Boolean
        Private objDALBU_Accounts_Contacts As DALBU_Accounts_Contacts

#End Region

#Region "Public Properties"
        Public Property FK_AccountTypeId() As Integer
            Set(ByVal value As Integer)
                _FK_AccountTypeId = value
            End Set
            Get
                Return (_FK_AccountTypeId)
            End Get
        End Property

        Public Property ContactId() As Integer
            Set(ByVal value As Integer)
                _ContactId = value
            End Set
            Get
                Return (_ContactId)
            End Get
        End Property


        Public Property FK_AccountId() As Integer
            Set(ByVal value As Integer)
                _FK_AccountId = value
            End Set
            Get
                Return (_FK_AccountId)
            End Get
        End Property


        Public Property ContactName() As String
            Set(ByVal value As String)
                _ContactName = value
            End Set
            Get
                Return (_ContactName)
            End Get
        End Property


        Public Property ContactTitle() As String
            Set(ByVal value As String)
                _ContactTitle = value
            End Set
            Get
                Return (_ContactTitle)
            End Get
        End Property


        Public Property ContactFax() As String
            Set(ByVal value As String)
                _ContactFax = value
            End Set
            Get
                Return (_ContactFax)
            End Get
        End Property


        Public Property ContactTelephone() As String
            Set(ByVal value As String)
                _ContactTelephone = value
            End Set
            Get
                Return (_ContactTelephone)
            End Get
        End Property


        Public Property ContactMobile() As String
            Set(ByVal value As String)
                _ContactMobile = value
            End Set
            Get
                Return (_ContactMobile)
            End Get
        End Property


        Public Property ContactEmail() As String
            Set(ByVal value As String)
                _ContactEmail = value
            End Set
            Get
                Return (_ContactEmail)
            End Get
        End Property


        Public Property Remarks() As String
            Set(ByVal value As String)
                _Remarks = value
            End Set
            Get
                Return (_Remarks)
            End Get
        End Property


        Public Property HasCredintials() As Boolean
            Set(ByVal value As Boolean)
                _HasCredintials = value
            End Set
            Get
                Return (_HasCredintials)
            End Get
        End Property


        Public Property UserName() As String
            Set(ByVal value As String)
                _UserName = value
            End Set
            Get
                Return (_UserName)
            End Get
        End Property


        Public Property Password() As String
            Set(ByVal value As String)
                _Password = value
            End Set
            Get
                Return (_Password)
            End Get
        End Property
        Public Property OldPassword() As String
            Set(ByVal value As String)
                _OldPassword = value
            End Set
            Get
                Return (_OldPassword)
            End Get
        End Property

        Public Property SecurityQuestion() As String
            Set(ByVal value As String)
                _SecurityQuestion = value
            End Set
            Get
                Return (_SecurityQuestion)
            End Get
        End Property


        Public Property SecurityAnswer() As String
            Set(ByVal value As String)
                _SecurityAnswer = value
            End Set
            Get
                Return (_SecurityAnswer)
            End Get
        End Property


        Public Property PharmaLicNo() As String
            Set(ByVal value As String)
                _PharmaLicNo = value
            End Set
            Get
                Return (_PharmaLicNo)
            End Get
        End Property


        Public Property PharmaExpDate() As String
            Set(ByVal value As String)
                _PharmaExpDate = value
            End Set
            Get
                Return (_PharmaExpDate)
            End Get
        End Property


        Public Property IsApproved() As Boolean
            Set(ByVal value As Boolean)
                _IsApproved = value
            End Set
            Get
                Return (_IsApproved)
            End Get
        End Property


        Public Property IsActive() As Boolean
            Set(ByVal value As Boolean)
                _IsActive = value
            End Set
            Get
                Return (_IsActive)
            End Get
        End Property


        Public Property IsMainContact() As Boolean
            Set(ByVal value As Boolean)
                _IsMainContact = value
            End Set
            Get
                Return (_IsMainContact)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALBU_Accounts_Contacts = New DALBU_Accounts_Contacts()

        End Sub

#End Region

#Region "Methods"
        Public Function ValidateConnection() As Boolean
            Dim sqlcn As New SqlConnection(DAC.getConnectionString)
            Try
                If (sqlcn.State = ConnectionState.Closed) Then
                    sqlcn.Open()
                End If
                ValidateConnection = True
            Catch ex As Exception
                ValidateConnection = False
            End Try
        End Function
        Public Function Add() As Integer

            Return objDALBU_Accounts_Contacts.Add(_FK_AccountId, _ContactName, _ContactTitle, _ContactFax, _ContactTelephone, _ContactMobile, _ContactEmail, _Remarks, _HasCredintials, _UserName, _Password, _SecurityQuestion, _SecurityAnswer, _PharmaLicNo, _PharmaExpDate, _IsApproved, _IsActive, _IsMainContact)
        End Function

        Public Function Update() As Integer

            Return objDALBU_Accounts_Contacts.Update(_ContactId, _FK_AccountId, _ContactName, _ContactTitle, _ContactFax, _ContactTelephone, _ContactMobile, _ContactEmail, _Remarks, _HasCredintials, _UserName, _Password, _SecurityQuestion, _SecurityAnswer, _PharmaLicNo, _PharmaExpDate, _IsApproved, _IsActive, _IsMainContact)

        End Function
        Public Function UpdateProfile() As Integer

            Return objDALBU_Accounts_Contacts.UpdateProfile(_ContactId, _FK_AccountId, _ContactName, _ContactTitle, _ContactFax, _ContactTelephone, _ContactMobile, _ContactEmail, _Remarks, _Password, _SecurityQuestion, _SecurityAnswer)

        End Function

        Public Function UpdateUser() As Integer
            Return objDALBU_Accounts_Contacts.UpdateUser(_ContactId, _UserName, _Password)

        End Function
        Public Function UpdatePassword() As Integer
            Return objDALBU_Accounts_Contacts.UpdatePassword(_ContactId, _OldPassword, _Password)
        End Function
        Public Function ChangeAccountCredentials() As Integer
            Return objDALBU_Accounts_Contacts.ChangeAccountCredentials(_ContactId, _OldPassword, _Password, _SecurityQuestion, _SecurityAnswer)

        End Function

        Public Function Delete() As Integer

            Return objDALBU_Accounts_Contacts.Delete(_ContactId)

        End Function
        Public Function DeleteByAccountID() As Integer

            Return objDALBU_Accounts_Contacts.DeleteByAccountID(_FK_AccountId)

        End Function

        Public Function GetByAccountID() As DataTable
            Return objDALBU_Accounts_Contacts.GetAll()
        End Function

        Public Function GetLoginUserDetails() As BU_Accounts_Contacts

            Dim dr As DataRow
            dr = objDALBU_Accounts_Contacts.GetLoginUser(UserName, Password)

            If Not dr Is Nothing Then
                If Not IsDBNull(dr("FK_AccountTypeId")) Then
                    FK_AccountTypeId = dr("FK_AccountTypeId")
                End If

                If Not IsDBNull(dr("ContactId")) Then
                    _ContactId = dr("ContactId")
                End If
                If Not IsDBNull(dr("FK_AccountId")) Then
                    _FK_AccountId = dr("FK_AccountId")
                End If
                If Not IsDBNull(dr("ContactName")) Then
                    _ContactName = dr("ContactName")
                End If
                If Not IsDBNull(dr("ContactTitle")) Then
                    _ContactTitle = dr("ContactTitle")
                End If
                If Not IsDBNull(dr("ContactFax")) Then
                    _ContactFax = dr("ContactFax")
                End If
                If Not IsDBNull(dr("ContactTelephone")) Then
                    _ContactTelephone = dr("ContactTelephone")
                End If
                If Not IsDBNull(dr("ContactMobile")) Then
                    _ContactMobile = dr("ContactMobile")
                End If
                If Not IsDBNull(dr("ContactEmail")) Then
                    _ContactEmail = dr("ContactEmail")
                End If
                If Not IsDBNull(dr("Remarks")) Then
                    _Remarks = dr("Remarks")
                End If
                If Not IsDBNull(dr("HasCredintials")) Then
                    _HasCredintials = dr("HasCredintials")
                End If
                If Not IsDBNull(dr("UserName")) Then
                    _UserName = dr("UserName")
                End If
                If Not IsDBNull(dr("Password")) Then
                    _Password = dr("Password")
                End If
                If Not IsDBNull(dr("SecurityQuestion")) Then
                    _SecurityQuestion = dr("SecurityQuestion")
                End If
                If Not IsDBNull(dr("SecurityAnswer")) Then
                    _SecurityAnswer = dr("SecurityAnswer")
                End If
                If Not IsDBNull(dr("PharmaLicNo")) Then
                    _PharmaLicNo = dr("PharmaLicNo")
                End If
                If Not IsDBNull(dr("PharmaExpDate")) Then
                    _PharmaExpDate = dr("PharmaExpDate")
                End If
                If Not IsDBNull(dr("IsApproved")) Then
                    _IsApproved = dr("IsApproved")
                End If
                If Not IsDBNull(dr("IsActive")) Then
                    _IsActive = dr("IsActive")
                End If
                If Not IsDBNull(dr("IsMainContact")) Then
                    _IsMainContact = dr("IsMainContact")
                End If
            End If
            Return Me
        End Function
        Public Function GetAll() As DataTable

            Return objDALBU_Accounts_Contacts.GetByAccountID(_FK_AccountId)

        End Function
        Public Function GetMainContactByAccountID() As BU_Accounts_Contacts
            Dim dr As DataRow
            dr = objDALBU_Accounts_Contacts.GetMainContactByAccountID(_FK_AccountId)

            If Not IsDBNull(dr("ContactId")) Then
                _ContactId = dr("ContactId")
            End If
            If Not IsDBNull(dr("ContactName")) Then
                _ContactName = dr("ContactName")
            End If
            If Not IsDBNull(dr("ContactTitle")) Then
                _ContactTitle = dr("ContactTitle")
            End If
            If Not IsDBNull(dr("ContactFax")) Then
                _ContactFax = dr("ContactFax")
            End If
            If Not IsDBNull(dr("ContactTelephone")) Then
                _ContactTelephone = dr("ContactTelephone")
            End If
            If Not IsDBNull(dr("ContactMobile")) Then
                _ContactMobile = dr("ContactMobile")
            End If
            If Not IsDBNull(dr("ContactEmail")) Then
                _ContactEmail = dr("ContactEmail")
            End If
            If Not IsDBNull(dr("HasCredintials")) Then
                _HasCredintials = dr("HasCredintials")
            End If
            If Not IsDBNull(dr("PharmaLicNo")) Then
                _PharmaLicNo = dr("PharmaLicNo")
            End If
            If Not IsDBNull(dr("PharmaExpDate")) Then
                _PharmaExpDate = dr("PharmaExpDate")
            End If
            If Not IsDBNull(dr("IsApproved")) Then
                _IsApproved = dr("IsApproved")
            End If
            If Not IsDBNull(dr("IsActive")) Then
                _IsActive = dr("IsActive")
            End If
            If Not IsDBNull(dr("IsMainContact")) Then
                _IsMainContact = dr("IsMainContact")
            End If
            Return Me
        End Function
        Public Function GetByPK() As BU_Accounts_Contacts

            Dim dr As DataRow
            dr = objDALBU_Accounts_Contacts.GetByPK(_ContactId)

            If Not IsDBNull(dr("ContactId")) Then
                _ContactId = dr("ContactId")
            End If
            If Not IsDBNull(dr("FK_AccountId")) Then
                _FK_AccountId = dr("FK_AccountId")
            End If
            If Not IsDBNull(dr("ContactName")) Then
                _ContactName = dr("ContactName")
            End If
            If Not IsDBNull(dr("ContactTitle")) Then
                _ContactTitle = dr("ContactTitle")
            End If
            If Not IsDBNull(dr("ContactFax")) Then
                _ContactFax = dr("ContactFax")
            End If
            If Not IsDBNull(dr("ContactTelephone")) Then
                _ContactTelephone = dr("ContactTelephone")
            End If
            If Not IsDBNull(dr("ContactMobile")) Then
                _ContactMobile = dr("ContactMobile")
            End If
            If Not IsDBNull(dr("ContactEmail")) Then
                _ContactEmail = dr("ContactEmail")
            End If
            If Not IsDBNull(dr("Remarks")) Then
                _Remarks = dr("Remarks")
            End If
            If Not IsDBNull(dr("HasCredintials")) Then
                _HasCredintials = dr("HasCredintials")
            End If
            If Not IsDBNull(dr("UserName")) Then
                _UserName = dr("UserName")
            End If
            If Not IsDBNull(dr("Password")) Then
                _Password = dr("Password")
            End If
            If Not IsDBNull(dr("SecurityQuestion")) Then
                _SecurityQuestion = dr("SecurityQuestion")
            End If
            If Not IsDBNull(dr("SecurityAnswer")) Then
                _SecurityAnswer = dr("SecurityAnswer")
            End If
            If Not IsDBNull(dr("PharmaLicNo")) Then
                _PharmaLicNo = dr("PharmaLicNo")
            End If
            If Not IsDBNull(dr("PharmaExpDate")) Then
                _PharmaExpDate = dr("PharmaExpDate")
            End If
            If Not IsDBNull(dr("IsApproved")) Then
                _IsApproved = dr("IsApproved")
            End If
            If Not IsDBNull(dr("IsActive")) Then
                _IsActive = dr("IsActive")
            End If
            If Not IsDBNull(dr("IsMainContact")) Then
                _IsMainContact = dr("IsMainContact")
            End If
            Return Me
        End Function

#End Region

    End Class

End Namespace