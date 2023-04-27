Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Employees

    Public Class Emp_WorkLocation

#Region "Class Variables"

        Private _FK_CompanyId As Integer
        Private _WorkLocationId As Integer
        Private _WorkLocationCode As String
        Private _WorkLocationName As String
        Private _WorkLocationArabicName As String
        Private _FK_TAPolicyId As Integer
        Private _Active As Boolean
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private _FK_UserId As Integer
        Private _GPSCoordinates As String
        Private _Radius As Integer
        Private _HasMobilePunch As Boolean
        Private _AllowedMobileWorkLocation As String
        Private _MustPunchPhysical As Boolean
        Private _MobilePunchConsiderDuration As String
        Private _SecondPunchRadius As Integer
        Private _OutPunchRadius As Integer
        Private _mustPunchTwoTimes As Boolean
        Private objDALEmp_WorkLocation As DALEmp_WorkLocation


#End Region

#Region "Public Properties"

        Public Property WorkLocationId() As Integer
            Set(ByVal value As Integer)
                _WorkLocationId = value
            End Set
            Get
                Return (_WorkLocationId)
            End Get
        End Property

        Public Property FK_CompanyId() As String
            Set(ByVal value As String)
                _FK_CompanyId = value
            End Set
            Get
                Return (_FK_CompanyId)
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

        Public Property WorkLocationCode() As String
            Set(ByVal value As String)
                _WorkLocationCode = value
            End Set
            Get
                Return (_WorkLocationCode)
            End Get
        End Property

        Public Property WorkLocationName() As String
            Set(ByVal value As String)
                _WorkLocationName = value
            End Set
            Get
                Return (_WorkLocationName)
            End Get
        End Property

        Public Property WorkLocationArabicName() As String
            Set(ByVal value As String)
                _WorkLocationArabicName = value
            End Set
            Get
                Return (_WorkLocationArabicName)
            End Get
        End Property

        Public Property FK_TAPolicyId() As Integer
            Set(ByVal value As Integer)
                _FK_TAPolicyId = value
            End Set
            Get
                Return (_FK_TAPolicyId)
            End Get
        End Property

        Public Property Active() As Boolean
            Set(ByVal value As Boolean)
                _Active = value
            End Set
            Get
                Return (_Active)
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

        Public Property GPSCoordinates() As String
            Set(ByVal value As String)
                _GPSCoordinates = value
            End Set
            Get
                Return (_GPSCoordinates)
            End Get
        End Property

        Public Property Radius() As Integer
            Set(ByVal value As Integer)
                _Radius = value
            End Set
            Get
                Return (_Radius)
            End Get
        End Property

        Public Property HasMobilePunch() As Boolean
            Set(ByVal value As Boolean)
                _HasMobilePunch = value
            End Set
            Get
                Return (_HasMobilePunch)
            End Get
        End Property

        Public Property AllowedMobileWorkLocation() As String
            Set(ByVal value As String)
                _AllowedMobileWorkLocation = value
            End Set
            Get
                Return (_AllowedMobileWorkLocation)
            End Get
        End Property

        Public Property MustPunchPhysical() As String
            Set(ByVal value As String)
                _MustPunchPhysical = value
            End Set
            Get
                Return (_MustPunchPhysical)
            End Get
        End Property

        Public Property MobilePunchConsiderDuration() As String
            Set(ByVal value As String)
                _MobilePunchConsiderDuration = value
            End Set
            Get
                Return (_MobilePunchConsiderDuration)
            End Get
        End Property

        Public Property SecondPunchRadius() As Integer
            Set(ByVal value As Integer)
                _SecondPunchRadius = value
            End Set
            Get
                Return (_SecondPunchRadius)
            End Get
        End Property
        Public Property OutPunchRadius() As Integer
            Set(ByVal value As Integer)
                _OutPunchRadius = value
            End Set
            Get
                Return (_OutPunchRadius)
            End Get
        End Property
        Public Property mustPunchTwoTimes() As String
            Set(ByVal value As String)
                _mustPunchTwoTimes = value
            End Set
            Get
                Return (_mustPunchTwoTimes)
            End Get
        End Property
#End Region


#Region "Constructor"

        Public Sub New()

            objDALEmp_WorkLocation = New DALEmp_WorkLocation()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer
            Dim rslt As Integer = objDALEmp_WorkLocation.Add(_WorkLocationId, _FK_CompanyId, _WorkLocationCode, _WorkLocationName, _WorkLocationArabicName, _FK_TAPolicyId, _Active, _GPSCoordinates, _CREATED_BY, _Radius, _HasMobilePunch, _AllowedMobileWorkLocation, _MustPunchPhysical, _MobilePunchConsiderDuration, _SecondPunchRadius, _OutPunchRadius, _mustPunchTwoTimes)
            App_EventsLog.Insert_ToEventLog("Add", _WorkLocationId, "Emp_WorkLocation", "Companies Work Location")
            Return rslt

        End Function

        Public Function Add_Bulk(ByVal DT As DataTable) As Integer
            Dim DS As New DataSet("DS")
            DT.TableName = "DT"
            DS.Tables.Add(DT)
            Dim StrXml As String = DS.GetXml

            Dim rslt As Integer = objDALEmp_WorkLocation.Add_Bulk(StrXml, _FK_CompanyId)

            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALEmp_WorkLocation.Update(_FK_CompanyId, _WorkLocationId, _WorkLocationCode, _WorkLocationName, _WorkLocationArabicName, _FK_TAPolicyId, _Active, _CREATED_BY, _LAST_UPDATE_BY, _GPSCoordinates, _Radius, _HasMobilePunch, _AllowedMobileWorkLocation, _MustPunchPhysical, _MobilePunchConsiderDuration, _SecondPunchRadius, _OutPunchRadius, _mustPunchTwoTimes)
            App_EventsLog.Insert_ToEventLog("Edit", _WorkLocationId, "Emp_WorkLocation", "Companies Work Location")
            Return rslt
        End Function

        Public Function Add_Advanced() As Integer

            Return objDALEmp_WorkLocation.Add_Advanced(_FK_CompanyId, _WorkLocationId, _WorkLocationCode, _WorkLocationName, _WorkLocationArabicName, _FK_TAPolicyId, _Active, _CREATED_BY, _LAST_UPDATE_BY)

        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALEmp_WorkLocation.Delete(_WorkLocationId)
            App_EventsLog.Insert_ToEventLog("Delete", _WorkLocationId, "Emp_WorkLocation", "Companies Work Location")
            Return rslt
        End Function

        Public Function GetAllPolicyByCompany() As DataTable

            Return objDALEmp_WorkLocation.GetAllPolicyByCompany(_FK_CompanyId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_WorkLocation.GetAll()

        End Function

        Public Function GetAllByCompany() As DataTable

            Return objDALEmp_WorkLocation.GetAllByCompany(_FK_CompanyId)

        End Function

        Public Function GetAllByCompanyAndUserId() As DataTable

            Return objDALEmp_WorkLocation.GetAllByCompanyAndUserId(_FK_CompanyId, _FK_UserId)

        End Function

        Public Function GetAllWorkGrid() As DataTable

            Return objDALEmp_WorkLocation.GetAllWorkGrid(_FK_CompanyId)

        End Function

        Public Function GetAllPolicy() As DataTable


            Return objDALEmp_WorkLocation.GetAllPolicy()

        End Function

        Public Function GetByPK() As Emp_WorkLocation

            Dim dr As DataRow
            dr = objDALEmp_WorkLocation.GetByPK(_WorkLocationId)

            If Not IsDBNull(dr("WorkLocationId")) Then
                _WorkLocationId = dr("WorkLocationId")
            End If
            If Not IsDBNull(dr("FK_CompanyId")) Then
                _FK_CompanyId = dr("FK_CompanyId")
            End If
            If Not IsDBNull(dr("WorkLocationCode")) Then
                _WorkLocationCode = dr("WorkLocationCode")
            End If
            If Not IsDBNull(dr("WorkLocationName")) Then
                _WorkLocationName = dr("WorkLocationName")
            End If
            If Not IsDBNull(dr("WorkLocationArabicName")) Then
                _WorkLocationArabicName = dr("WorkLocationArabicName")
            End If
            If Not IsDBNull(dr("FK_TAPolicyId")) Then
                _FK_TAPolicyId = dr("FK_TAPolicyId")
            End If
            If Not IsDBNull(dr("Active")) Then
                _Active = dr("Active")
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
            If Not IsDBNull(dr("GPSCoordinates")) Then
                _GPSCoordinates = dr("GPSCoordinates")
            End If
            If Not IsDBNull(dr("Radius")) Then
                _Radius = dr("Radius")
            End If
            If Not IsDBNull(dr("HasMobilePunch")) Then
                _HasMobilePunch = dr("HasMobilePunch")
            End If
            If Not IsDBNull(dr("MustPunchPhysical")) Then
                _MustPunchPhysical = dr("MustPunchPhysical")
            End If
            If Not IsDBNull(dr("MobilePunchConsiderDuration")) Then
                _MobilePunchConsiderDuration = dr("MobilePunchConsiderDuration")
            End If
            If Not IsDBNull(dr("SecondPunchRadius")) Then
                _SecondPunchRadius = dr("SecondPunchRadius")
            End If
            If Not IsDBNull(dr("OutPunchRadius")) Then
                _OutPunchRadius = dr("OutPunchRadius")
            End If
            If Not IsDBNull(dr("mustPunchTwoTimes")) Then
                _mustPunchTwoTimes = dr("mustPunchTwoTimes")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace