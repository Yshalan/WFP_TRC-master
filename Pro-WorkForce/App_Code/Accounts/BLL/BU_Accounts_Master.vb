Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data


Imports TA.Accounts

Namespace TA.Accounts

    Public Class BU_Accounts_Master

#Region "Class Variables"


        Private _AccountId As Integer
        Private _AccountNo As String
        Private _FK_AccountTypeId As Integer
        Private _AccountName As String
        Private _FK_CountryId As Integer
        Private _City As String
        Private _Address As String
        Private _PoBox As String
        Private _Telephone As String
        Private _Fax As String
        Private _Email As String
        Private _Website As String
        Private _Remarks As String
        Private _MOHLicense As Boolean
        Private _MOHLicenseNo As String
        Private _MOHLicenseIssueDate As String
        Private _MOHLicenseExpiryDate As String
        Private _MOERegNo As String
        Private _MOERegDate As String
        Private _OldAgentNo As Integer
        Private _MikeNewOldAgentNo As Integer
        Private _FK_ContactId_inCharge As Integer
        Private _IsActive As Boolean
        Private _IsUAEMAH As Boolean
        Private _IsImporter As Boolean
        Private _IsDistributer As Boolean
        Private objDALBU_Accounts_Master As DALBU_Accounts_Master

#End Region

#Region "Public Properties"


        Public Property AccountId() As Integer
            Set(ByVal value As Integer)
                _AccountId = value
            End Set
            Get
                Return (_AccountId)
            End Get
        End Property


        Public Property AccountNo() As String
            Set(ByVal value As String)
                _AccountNo = value
            End Set
            Get
                Return (_AccountNo)
            End Get
        End Property


        Public Property FK_AccountTypeId() As Integer
            Set(ByVal value As Integer)
                _FK_AccountTypeId = value
            End Set
            Get
                Return (_FK_AccountTypeId)
            End Get
        End Property


        Public Property AccountName() As String
            Set(ByVal value As String)
                _AccountName = value
            End Set
            Get
                Return (_AccountName)
            End Get
        End Property


        Public Property FK_CountryId() As Integer
            Set(ByVal value As Integer)
                _FK_CountryId = value
            End Set
            Get
                Return (_FK_CountryId)
            End Get
        End Property


        Public Property City() As String
            Set(ByVal value As String)
                _City = value
            End Set
            Get
                Return (_City)
            End Get
        End Property


        Public Property Address() As String
            Set(ByVal value As String)
                _Address = value
            End Set
            Get
                Return (_Address)
            End Get
        End Property


        Public Property PoBox() As String
            Set(ByVal value As String)
                _PoBox = value
            End Set
            Get
                Return (_PoBox)
            End Get
        End Property


        Public Property Telephone() As String
            Set(ByVal value As String)
                _Telephone = value
            End Set
            Get
                Return (_Telephone)
            End Get
        End Property


        Public Property Fax() As String
            Set(ByVal value As String)
                _Fax = value
            End Set
            Get
                Return (_Fax)
            End Get
        End Property


        Public Property Email() As String
            Set(ByVal value As String)
                _Email = value
            End Set
            Get
                Return (_Email)
            End Get
        End Property


        Public Property Website() As String
            Set(ByVal value As String)
                _Website = value
            End Set
            Get
                Return (_Website)
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


        Public Property MOHLicense() As Boolean
            Set(ByVal value As Boolean)
                _MOHLicense = value
            End Set
            Get
                Return (_MOHLicense)
            End Get
        End Property


        Public Property MOHLicenseNo() As String
            Set(ByVal value As String)
                _MOHLicenseNo = value
            End Set
            Get
                Return (_MOHLicenseNo)
            End Get
        End Property


        Public Property MOHLicenseIssueDate() As String
            Set(ByVal value As String)
                _MOHLicenseIssueDate = value
            End Set
            Get
                Return (_MOHLicenseIssueDate)
            End Get
        End Property


        Public Property MOHLicenseExpiryDate() As String
            Set(ByVal value As String)
                _MOHLicenseExpiryDate = value
            End Set
            Get
                Return (_MOHLicenseExpiryDate)
            End Get
        End Property


        Public Property MOERegNo() As String
            Set(ByVal value As String)
                _MOERegNo = value
            End Set
            Get
                Return (_MOERegNo)
            End Get
        End Property


        Public Property MOERegDate() As String
            Set(ByVal value As String)
                _MOERegDate = value
            End Set
            Get
                Return (_MOERegDate)
            End Get
        End Property


        Public Property OldAgentNo() As Integer
            Set(ByVal value As Integer)
                _OldAgentNo = value
            End Set
            Get
                Return (_OldAgentNo)
            End Get
        End Property


        Public Property MikeNewOldAgentNo() As Integer
            Set(ByVal value As Integer)
                _MikeNewOldAgentNo = value
            End Set
            Get
                Return (_MikeNewOldAgentNo)
            End Get
        End Property


        Public Property FK_ContactId_inCharge() As Integer
            Set(ByVal value As Integer)
                _FK_ContactId_inCharge = value
            End Set
            Get
                Return (_FK_ContactId_inCharge)
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


        Public Property IsUAEMAH() As Boolean
            Set(ByVal value As Boolean)
                _IsUAEMAH = value
            End Set
            Get
                Return (_IsUAEMAH)
            End Get
        End Property


        Public Property IsImporter() As Boolean
            Set(ByVal value As Boolean)
                _IsImporter = value
            End Set
            Get
                Return (_IsImporter)
            End Get
        End Property


        Public Property IsDistributer() As Boolean
            Set(ByVal value As Boolean)
                _IsDistributer = value
            End Set
            Get
                Return (_IsDistributer)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALBU_Accounts_Master = New DALBU_Accounts_Master()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As String

            Return objDALBU_Accounts_Master.Add(_AccountNo, _FK_AccountTypeId, _AccountName, _FK_CountryId, _City, _Address, _PoBox, _Telephone, _Fax, _Email, _Website, _Remarks, _MOHLicense, _MOHLicenseNo, _MOHLicenseIssueDate, _MOHLicenseExpiryDate, _MOERegNo, _MOERegDate, _OldAgentNo, _MikeNewOldAgentNo, _IsActive, _IsUAEMAH, _IsImporter, _IsDistributer, _AccountId)
        End Function

        Public Function Update() As Integer

            Return objDALBU_Accounts_Master.Update(_AccountId, _AccountNo, _FK_AccountTypeId, _AccountName, _FK_CountryId, _City, _Address, _PoBox, _Telephone, _Fax, _Email, _Website, _Remarks, _MOHLicense, _MOHLicenseNo, _MOHLicenseIssueDate, _MOHLicenseExpiryDate, _MOERegNo, _MOERegDate, _OldAgentNo, _MikeNewOldAgentNo, _IsActive, _IsUAEMAH, _IsImporter, _IsDistributer)

        End Function



        Public Function Delete() As Integer

            Return objDALBU_Accounts_Master.Delete(_AccountId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALBU_Accounts_Master.GetAll()

        End Function
        Public Function GetAllByType(ByVal AccType As Integer) As DataTable

            Return objDALBU_Accounts_Master.GetAllByType(AccType)

        End Function


        Public Function GetByPK() As BU_Accounts_Master

            Dim dr As DataRow
            dr = objDALBU_Accounts_Master.GetByPK(_AccountId)

            If Not IsDBNull(dr("AccountId")) Then
                _AccountId = dr("AccountId")
            End If
            If Not IsDBNull(dr("AccountNo")) Then
                _AccountNo = dr("AccountNo")
            End If
            If Not IsDBNull(dr("FK_AccountTypeId")) Then
                _FK_AccountTypeId = dr("FK_AccountTypeId")
            End If
            If Not IsDBNull(dr("AccountName")) Then
                _AccountName = dr("AccountName")
            End If
            If Not IsDBNull(dr("FK_CountryId")) Then
                _FK_CountryId = dr("FK_CountryId")
            End If
            If Not IsDBNull(dr("City")) Then
                _City = dr("City")
            End If
            If Not IsDBNull(dr("Address")) Then
                _Address = dr("Address")
            End If
            If Not IsDBNull(dr("PoBox")) Then
                _PoBox = dr("PoBox")
            End If
            If Not IsDBNull(dr("Telephone")) Then
                _Telephone = dr("Telephone")
            End If
            If Not IsDBNull(dr("Fax")) Then
                _Fax = dr("Fax")
            End If
            If Not IsDBNull(dr("Email")) Then
                _Email = dr("Email")
            End If
            If Not IsDBNull(dr("Website")) Then
                _Website = dr("Website")
            End If
            If Not IsDBNull(dr("Remarks")) Then
                _Remarks = dr("Remarks")
            End If
            If Not IsDBNull(dr("MOHLicense")) Then
                _MOHLicense = dr("MOHLicense")
            End If
            If Not IsDBNull(dr("MOHLicenseNo")) Then
                _MOHLicenseNo = dr("MOHLicenseNo")
            End If
            If Not IsDBNull(dr("MOHLicenseIssueDate")) Then
                _MOHLicenseIssueDate = dr("MOHLicenseIssueDate")
            End If
            If Not IsDBNull(dr("MOHLicenseExpiryDate")) Then
                _MOHLicenseExpiryDate = dr("MOHLicenseExpiryDate")
            End If
            If Not IsDBNull(dr("MOERegNo")) Then
                _MOERegNo = dr("MOERegNo")
            End If
            If Not IsDBNull(dr("MOERegDate")) Then
                _MOERegDate = dr("MOERegDate")
            End If
            If Not IsDBNull(dr("OldAgentNo")) Then
                _OldAgentNo = dr("OldAgentNo")
            End If
            If Not IsDBNull(dr("MikeNewOldAgentNo")) Then
                _MikeNewOldAgentNo = dr("MikeNewOldAgentNo")
            End If
            If Not IsDBNull(dr("FK_ContactId_inCharge")) Then
                _FK_ContactId_inCharge = dr("FK_ContactId_inCharge")
            End If
            If Not IsDBNull(dr("IsActive")) Then
                _IsActive = dr("IsActive")
            End If
            If Not IsDBNull(dr("IsUAEMAH")) Then
                _IsUAEMAH = dr("IsUAEMAH")
            End If
            If Not IsDBNull(dr("IsImporter")) Then
                _IsImporter = dr("IsImporter")
            End If
            If Not IsDBNull(dr("IsDistributer")) Then
                _IsDistributer = dr("IsDistributer")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace