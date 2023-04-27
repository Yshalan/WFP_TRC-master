Imports System.Data
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.Control
Imports System.Web.UI.WebControls
Imports System.Reflection
Imports System.Text
Imports System.IO
Imports TA.Accounts
Imports TA.Security
Imports System.Collections

Namespace SmartV.UTILITIES
    Public NotInheritable Class SessionVariables
#Region "Properties"

        Public Shared Property CultureInfo() As String
            Get
                Return System.Web.HttpContext.Current.Session("CultureInfo")
            End Get
            Set(ByVal value As String)
                System.Web.HttpContext.Current.Session("CultureInfo") = value

            End Set
        End Property

        Public Shared Property LanguageChanged() As Boolean
            Get
                If System.Web.HttpContext.Current.Session("LanguageChanged") Is Nothing Then
                    Return False
                End If
                Return True
            End Get
            Set(ByVal value As Boolean)

                System.Web.HttpContext.Current.Session("LanguageChanged") = value

            End Set
        End Property

        Public Shared Property UserModuleId() As Integer
            Get
                Return System.Web.HttpContext.Current.Session("UserModuleId")
            End Get
            Set(ByVal value As Integer)
                System.Web.HttpContext.Current.Session("UserModuleId") = value
            End Set
        End Property

        Public Shared Property LoginUser() As SYSUsers
            Get
                Return System.Web.HttpContext.Current.Session("LoginUser")
            End Get
            Set(ByVal value As SYSUsers)
                System.Web.HttpContext.Current.Session("LoginUser") = value
            End Set
        End Property

        Public Shared Property AdminLoginUser() As SECUsers
            Get
                Return System.Web.HttpContext.Current.Session("AdminLoginUser")
            End Get
            Set(ByVal value As SECUsers)
                System.Web.HttpContext.Current.Session("AdminLoginUser") = value
            End Set
        End Property

        Public Shared Property SpName() As Hashtable
            Get
                Return System.Web.HttpContext.Current.Session("SpName")
            End Get
            Set(ByVal value As Hashtable)
                System.Web.HttpContext.Current.Session("SpName") = value
            End Set
        End Property

        Public Shared Property PageHeader() As String
            Get
                Return System.Web.HttpContext.Current.Session("PageHeader")
            End Get
            Set(ByVal value As String)
                System.Web.HttpContext.Current.Session("PageHeader") = value
            End Set
        End Property

        Public Shared Property ContactID() As Integer

            Get

                Return System.Web.HttpContext.Current.Session("ContactID")

            End Get

            Set(ByVal value As Integer)

                System.Web.HttpContext.Current.Session("ContactID") = value

            End Set

        End Property

        Public Shared Property LicenseDetails() As STSupremeKeyValidation.STSupremeKeyValidation.SmartKeyValidation
            Get
                Return System.Web.HttpContext.Current.Session("LicenseDetails")
            End Get
            Set(ByVal value As STSupremeKeyValidation.STSupremeKeyValidation.SmartKeyValidation)
                System.Web.HttpContext.Current.Session("LicenseDetails") = value
            End Set
        End Property

        Public Shared Property LoginDate() As DateTime
            Get
                Return System.Web.HttpContext.Current.Session("LoginDate")
            End Get
            Set(ByVal value As DateTime)
                System.Web.HttpContext.Current.Session("LoginDate") = value
            End Set
        End Property

#End Region
    End Class
End Namespace

