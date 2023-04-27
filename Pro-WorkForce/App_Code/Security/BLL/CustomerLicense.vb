Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

namespace TA_CustomerLicense

Public Class CustomerLicense

#Region "Class Variables"


Private  _CustomerId As Integer
Private  _CustomerShortName As String
Private  _CustomerName As String
        Private _CustomerArabicName As String
        Private _PhoneNumber As Long
Private  _CustomerCity As String
Private  _CustomerAddress As String
Private  _CustomerGPSCoordinates As String
Private  _NoOfUsers As String
Private  _NoOfReaders As String
Private  _NoOfEmployees As String
Private  _Projectmanager As String
Private  _ImplementationEngineer As String
Private  _SupportEngineer As String
Private  _IntegrationEngineer As String
Private  _ServerMacAddressKey As String
Private  _Package As Integer
Private  _StartDate As DateTime
Private  _SupportEndDate As DateTime
Private  _Forms As String
Private  _LicenseKey As String
Private  _CreatedBy As String
Private  _CreatedDate As DateTime
Private  _AlteredBy As String
        Private _AlteredDate As DateTime
        Private _CustomerCountry As String
Private objDALCustomerLicense As DALCustomerLicense

#End Region

#Region "Public Properties"


Public Property CustomerId() As Integer
Set(ByVal value As Integer )
_CustomerId = value
End Set
Get
Return (_CustomerId)
End Get
End Property


Public Property CustomerShortName() As String
Set(ByVal value As String )
_CustomerShortName = value
End Set
Get
Return (_CustomerShortName)
End Get
End Property


Public Property CustomerName() As String
Set(ByVal value As String )
_CustomerName = value
End Set
Get
Return (_CustomerName)
End Get
End Property


Public Property CustomerArabicName() As String
Set(ByVal value As String )
_CustomerArabicName = value
End Set
Get
Return (_CustomerArabicName)
End Get
End Property


Public Property PhoneNumber() As Long
Set(ByVal value As Long )
_PhoneNumber = value
End Set
Get
Return (_PhoneNumber)
End Get
        End Property
        Public Property CustomerCountry() As String
            Set(ByVal value As String)
                _CustomerCountry = value
            End Set
            Get
                Return (_CustomerCountry)
            End Get
        End Property


Public Property CustomerCity() As String
Set(ByVal value As String )
_CustomerCity = value
End Set
Get
Return (_CustomerCity)
End Get
End Property


Public Property CustomerAddress() As String
Set(ByVal value As String )
_CustomerAddress = value
End Set
Get
Return (_CustomerAddress)
End Get
End Property


Public Property CustomerGPSCoordinates() As String
Set(ByVal value As String )
_CustomerGPSCoordinates = value
End Set
Get
Return (_CustomerGPSCoordinates)
End Get
End Property


Public Property NoOfUsers() As String
Set(ByVal value As String )
_NoOfUsers = value
End Set
Get
Return (_NoOfUsers)
End Get
End Property


Public Property NoOfReaders() As String
Set(ByVal value As String )
_NoOfReaders = value
End Set
Get
Return (_NoOfReaders)
End Get
End Property


Public Property NoOfEmployees() As String
Set(ByVal value As String )
_NoOfEmployees = value
End Set
Get
Return (_NoOfEmployees)
End Get
End Property


Public Property Projectmanager() As String
Set(ByVal value As String )
_Projectmanager = value
End Set
Get
Return (_Projectmanager)
End Get
End Property


Public Property ImplementationEngineer() As String
Set(ByVal value As String )
_ImplementationEngineer = value
End Set
Get
Return (_ImplementationEngineer)
End Get
End Property


Public Property SupportEngineer() As String
Set(ByVal value As String )
_SupportEngineer = value
End Set
Get
Return (_SupportEngineer)
End Get
End Property


Public Property IntegrationEngineer() As String
Set(ByVal value As String )
_IntegrationEngineer = value
End Set
Get
Return (_IntegrationEngineer)
End Get
End Property


Public Property ServerMacAddressKey() As String
Set(ByVal value As String )
_ServerMacAddressKey = value
End Set
Get
Return (_ServerMacAddressKey)
End Get
End Property


Public Property Package() As Integer
Set(ByVal value As Integer )
_Package = value
End Set
Get
Return (_Package)
End Get
End Property


Public Property StartDate() As DateTime
Set(ByVal value As DateTime )
_StartDate = value
End Set
Get
Return (_StartDate)
End Get
End Property


Public Property SupportEndDate() As DateTime
Set(ByVal value As DateTime )
_SupportEndDate = value
End Set
Get
Return (_SupportEndDate)
End Get
End Property


Public Property Forms() As String
Set(ByVal value As String )
_Forms = value
End Set
Get
Return (_Forms)
End Get
End Property


Public Property LicenseKey() As String
Set(ByVal value As String )
_LicenseKey = value
End Set
Get
Return (_LicenseKey)
End Get
End Property


Public Property CreatedBy() As String
Set(ByVal value As String )
_CreatedBy = value
End Set
Get
Return (_CreatedBy)
End Get
End Property


Public Property CreatedDate() As DateTime
Set(ByVal value As DateTime )
_CreatedDate = value
End Set
Get
Return (_CreatedDate)
End Get
End Property


Public Property AlteredBy() As String
Set(ByVal value As String )
_AlteredBy = value
End Set
Get
Return (_AlteredBy)
End Get
End Property


Public Property AlteredDate() As DateTime
Set(ByVal value As DateTime )
_AlteredDate = value
End Set
Get
Return (_AlteredDate)
End Get
End Property

#End Region


#Region "Constructor"

Public Sub New()

objDALCustomerLicense = new DALCustomerLicense()

End Sub

#End Region

#region "Methods"

Public Function Add()  As Integer 

            Return objDALCustomerLicense.Add(_CustomerShortName, _CustomerName, _CustomerArabicName, _PhoneNumber, _CustomerCountry, _CustomerCity, _CustomerAddress, _CustomerGPSCoordinates, _NoOfUsers, _NoOfReaders, _NoOfEmployees, _Projectmanager, _ImplementationEngineer, _SupportEngineer, _IntegrationEngineer, _ServerMacAddressKey, _Package, _StartDate, _SupportEndDate, _Forms, _LicenseKey, _CreatedBy, _CreatedDate, _AlteredBy, _AlteredDate)
End Function

Public Function Update() As Integer 

            Return objDALCustomerLicense.Update(_CustomerId, _CustomerShortName, _CustomerName, _CustomerArabicName, _PhoneNumber, _CustomerCountry, _CustomerCity, _CustomerAddress, _CustomerGPSCoordinates, _NoOfUsers, _NoOfReaders, _NoOfEmployees, _Projectmanager, _ImplementationEngineer, _SupportEngineer, _IntegrationEngineer, _ServerMacAddressKey, _Package, _StartDate, _SupportEndDate, _Forms, _LicenseKey, _CreatedBy, _CreatedDate, _AlteredBy, _AlteredDate)

End Function



Public Function Delete() As Integer 

Return objDALCustomerLicense.Delete( _CustomerId)

End Function

public Function GetAll() As DataTable 

Return objDALCustomerLicense.GetAll()

End Function

public Function GetByPK() As CustomerLicense

Dim dr As DataRow
dr = objDALCustomerLicense.GetByPK( _CustomerId)

If Not IsDBNull(dr("CustomerId")) Then
_CustomerId = dr("CustomerId")
End If
If Not IsDBNull(dr("CustomerShortName")) Then
_CustomerShortName = dr("CustomerShortName")
End If
If Not IsDBNull(dr("CustomerName")) Then
_CustomerName = dr("CustomerName")
End If
If Not IsDBNull(dr("CustomerArabicName")) Then
_CustomerArabicName = dr("CustomerArabicName")
End If
If Not IsDBNull(dr("PhoneNumber")) Then
_PhoneNumber = dr("PhoneNumber")
End If
If Not IsDBNull(dr("CustomerCity")) Then
_CustomerCity = dr("CustomerCity")
            End If

            If Not IsDBNull(dr("CustomerCountry")) Then
                _CustomerCountry = dr("CustomerCountry")
            End If

If Not IsDBNull(dr("CustomerAddress")) Then
_CustomerAddress = dr("CustomerAddress")
End If
If Not IsDBNull(dr("CustomerGPSCoordinates")) Then
_CustomerGPSCoordinates = dr("CustomerGPSCoordinates")
End If
If Not IsDBNull(dr("NoOfUsers")) Then
_NoOfUsers = dr("NoOfUsers")
End If
If Not IsDBNull(dr("NoOfReaders")) Then
_NoOfReaders = dr("NoOfReaders")
End If
If Not IsDBNull(dr("NoOfEmployees")) Then
_NoOfEmployees = dr("NoOfEmployees")
End If
If Not IsDBNull(dr("Projectmanager")) Then
_Projectmanager = dr("Projectmanager")
End If
If Not IsDBNull(dr("ImplementationEngineer")) Then
_ImplementationEngineer = dr("ImplementationEngineer")
End If
If Not IsDBNull(dr("SupportEngineer")) Then
_SupportEngineer = dr("SupportEngineer")
End If
If Not IsDBNull(dr("IntegrationEngineer")) Then
_IntegrationEngineer = dr("IntegrationEngineer")
End If
If Not IsDBNull(dr("ServerMacAddressKey")) Then
_ServerMacAddressKey = dr("ServerMacAddressKey")
End If
If Not IsDBNull(dr("Package")) Then
_Package = dr("Package")
End If
If Not IsDBNull(dr("StartDate")) Then
_StartDate = dr("StartDate")
End If
If Not IsDBNull(dr("SupportEndDate")) Then
_SupportEndDate = dr("SupportEndDate")
End If
If Not IsDBNull(dr("Forms")) Then
_Forms = dr("Forms")
End If
If Not IsDBNull(dr("LicenseKey")) Then
_LicenseKey = dr("LicenseKey")
End If
If Not IsDBNull(dr("CreatedBy")) Then
_CreatedBy = dr("CreatedBy")
End If
If Not IsDBNull(dr("CreatedDate")) Then
_CreatedDate = dr("CreatedDate")
End If
If Not IsDBNull(dr("AlteredBy")) Then
_AlteredBy = dr("AlteredBy")
End If
If Not IsDBNull(dr("AlteredDate")) Then
_AlteredDate = dr("AlteredDate")
End If
Return Me
        End Function

        Public Function GetFormsFromCustomerLicense() As DataTable

            Return objDALCustomerLicense.GetFormsFromCustomerLicense(_CustomerId)

        End Function

        Public Function GetFormsModulesForLinks(FormsString As String) As DataTable

            Return objDALCustomerLicense.GetFormsModulesForLinks(FormsString)

        End Function

#End Region

    End Class
End namespace