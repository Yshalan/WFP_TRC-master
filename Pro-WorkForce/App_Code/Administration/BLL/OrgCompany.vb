Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Admin

    Public Class OrgCompany

#Region "Class Variables"


        Private _CompanyId As Integer
        Private _FK_ParentId As Integer
        Private _CompanyShortName As String
        Private _CompanyName As String
        Private _CompanyArabicName As String
        Private _Country As Integer
        Private _Address As String
        Private _PhoneNumber As String
        Private _Fax As String
        Private _URL As String
        Private _Logo As String
        Private _FK_HighestPost As Integer
        Private _FK_ManagerId As Long
        Private _FK_DefaultPolicyId As Integer
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALOrgCompany As DALOrgCompany

        Private _FK_UserId As Integer
        Private _FilterType As String
#End Region

#Region "Public Properties"


        Public Property CompanyId() As Integer
            Set(ByVal value As Integer)
                _CompanyId = value
            End Set
            Get
                Return (_CompanyId)
            End Get
        End Property


        Public Property FK_ParentId() As Integer
            Set(ByVal value As Integer)
                _FK_ParentId = value
            End Set
            Get
                Return (_FK_ParentId)
            End Get
        End Property


        Public Property CompanyShortName() As String
            Set(ByVal value As String)
                _CompanyShortName = value
            End Set
            Get
                Return (_CompanyShortName)
            End Get
        End Property


        Public Property CompanyName() As String
            Set(ByVal value As String)
                _CompanyName = value
            End Set
            Get
                Return (_CompanyName)
            End Get
        End Property


        Public Property CompanyArabicName() As String
            Set(ByVal value As String)
                _CompanyArabicName = value
            End Set
            Get
                Return (_CompanyArabicName)
            End Get
        End Property


        Public Property Country() As Integer
            Set(ByVal value As Integer)
                _Country = value
            End Set
            Get
                Return (_Country)
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


        Public Property PhoneNumber() As String
            Set(ByVal value As String)
                _PhoneNumber = value
            End Set
            Get
                Return (_PhoneNumber)
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


        Public Property URL() As String
            Set(ByVal value As String)
                _URL = value
            End Set
            Get
                Return (_URL)
            End Get
        End Property


        Public Property Logo() As String
            Set(ByVal value As String)
                _Logo = value
            End Set
            Get
                Return (_Logo)
            End Get
        End Property


        Public Property FK_HighestPost() As Integer
            Set(ByVal value As Integer)
                _FK_HighestPost = value
            End Set
            Get
                Return (_FK_HighestPost)
            End Get
        End Property


        Public Property FK_ManagerId() As Long
            Set(ByVal value As Long)
                _FK_ManagerId = value
            End Set
            Get
                Return (_FK_ManagerId)
            End Get
        End Property


        Public Property FK_DefaultPolicyId() As Integer
            Set(ByVal value As Integer)
                _FK_DefaultPolicyId = value
            End Set
            Get
                Return (_FK_DefaultPolicyId)
            End Get
        End Property


        Public Property CREATED_BY() As String
            Set(ByVal value As String)
                _CREATED_BY = value
            End Set
            Get
                Return (_CREATED_BY)
            End Get
        End Property


        Public Property CREATED_DATE() As DateTime
            Set(ByVal value As DateTime)
                _CREATED_DATE = value
            End Set
            Get
                Return (_CREATED_DATE)
            End Get
        End Property


        Public Property LAST_UPDATE_BY() As String
            Set(ByVal value As String)
                _LAST_UPDATE_BY = value
            End Set
            Get
                Return (_LAST_UPDATE_BY)
            End Get
        End Property


        Public Property LAST_UPDATE_DATE() As DateTime
            Set(ByVal value As DateTime)
                _LAST_UPDATE_DATE = value
            End Set
            Get
                Return (_LAST_UPDATE_DATE)
            End Get
        End Property

        Public Property FK_UserId() As Integer
            Set(ByVal value As Integer)
                _FK_UserId = value
            End Set
            Get
                Return (_FK_UserId)
            End Get
        End Property
        Public Property FilterType() As String
            Set(ByVal value As String)
                _FilterType = value
            End Set
            Get
                Return (_FilterType)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALOrgCompany = New DALOrgCompany()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer
            Dim rslt As Integer = objDALOrgCompany.Add(_FK_ParentId, _CompanyShortName, _CompanyName, _CompanyArabicName, _Country, _Address, _PhoneNumber, _Fax, _URL, _Logo, _FK_HighestPost, _FK_ManagerId, _FK_DefaultPolicyId, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE)
            _CompanyId = objDALOrgCompany.intCompanyID
            App_EventsLog.Insert_ToEventLog("Add", _CompanyId, "OrgCompany", "Organization Structure")
            Return rslt
        End Function

        Public Function Update() As Integer
            Dim rslt As Integer = objDALOrgCompany.Update(_CompanyId, _FK_ParentId, _CompanyShortName, _CompanyName, _CompanyArabicName, _Country, _Address, _PhoneNumber, _Fax, _URL, _Logo, _FK_HighestPost, _FK_ManagerId, _FK_DefaultPolicyId, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE)
            App_EventsLog.Insert_ToEventLog("Edit", _CompanyId, "OrgCompany", "Organization Structure")
            Return rslt

        End Function

        Public Function UpdateLogo() As Integer
            Dim rslt As Integer = objDALOrgCompany.UpdateLogo(_CompanyId, _Logo)
            App_EventsLog.Insert_ToEventLog("Delete", _CompanyId, "OrgCompany", "Organization Structure")
            Return rslt

        End Function

        Public Function Delete() As Integer

            Return objDALOrgCompany.Delete(_CompanyId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALOrgCompany.GetAll()

        End Function
        Public Function GetEmployeesByOrgCompany() As DataTable

            Return objDALOrgCompany.GetEmployeesByOrgCompany(_CompanyId)

        End Function
        Public Function GetAllforddl() As DataTable

            Return objDALOrgCompany.GetAllForDDL()

        End Function
        Public Function GetAllforddl_ByUserId() As DataTable

            Return objDALOrgCompany.GetAllforddl_ByUserId(_FK_UserId, _FilterType)

        End Function
        Public Function GetAllchildsByParent() As DataTable

            Return objDALOrgCompany.GetAllchildsByParent(_FK_ParentId)

        End Function
        Public Function CheckChildOREntitytExistsForCompany() As Integer

            Return objDALOrgCompany.CheckChildOREntitytExistsForCompany(_CompanyId)

        End Function
        Public Function GetCompanyNameByID() As String

            Return objDALOrgCompany.GetCompanyNameByID(_CompanyId)

        End Function

        Public Function GetByPK() As OrgCompany

            Dim dr As DataRow
            dr = objDALOrgCompany.GetByPK(_CompanyId)

            If Not IsDBNull(dr("CompanyId")) Then
                _CompanyId = dr("CompanyId")
            End If
            If Not IsDBNull(dr("FK_ParentId")) Then
                _FK_ParentId = dr("FK_ParentId")
            End If
            If Not IsDBNull(dr("CompanyShortName")) Then
                _CompanyShortName = dr("CompanyShortName")
            End If
            If Not IsDBNull(dr("CompanyName")) Then
                _CompanyName = dr("CompanyName")
            End If
            If Not IsDBNull(dr("CompanyArabicName")) Then
                _CompanyArabicName = dr("CompanyArabicName")
            End If
            If Not IsDBNull(dr("Country")) Then
                _Country = dr("Country")
            End If
            If Not IsDBNull(dr("Address")) Then
                _Address = dr("Address")
            End If
            If Not IsDBNull(dr("PhoneNumber")) Then
                _PhoneNumber = dr("PhoneNumber")
            End If
            If Not IsDBNull(dr("Fax")) Then
                _Fax = dr("Fax")
            End If
            If Not IsDBNull(dr("URL")) Then
                _URL = dr("URL")
            End If
            If Not IsDBNull(dr("Logo")) Then
                _Logo = dr("Logo")
            End If
            If Not IsDBNull(dr("FK_HighestPost")) Then
                _FK_HighestPost = dr("FK_HighestPost")
            End If
            If Not IsDBNull(dr("FK_ManagerId")) Then
                _FK_ManagerId = dr("FK_ManagerId")
            End If
            If Not IsDBNull(dr("FK_DefaultPolicyId")) Then
                _FK_DefaultPolicyId = dr("FK_DefaultPolicyId")
            End If
            If Not IsDBNull(dr("CREATED_BY")) Then
                _CREATED_BY = dr("CREATED_BY")
            End If
            If Not IsDBNull(dr("CREATED_DATE")) Then
                _CREATED_DATE = dr("CREATED_DATE")
            End If
            If Not IsDBNull(dr("LAST_UPDATE_BY")) Then
                _LAST_UPDATE_BY = dr("LAST_UPDATE_BY")
            End If
            If Not IsDBNull(dr("LAST_UPDATE_DATE")) Then
                _LAST_UPDATE_DATE = dr("LAST_UPDATE_DATE")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace