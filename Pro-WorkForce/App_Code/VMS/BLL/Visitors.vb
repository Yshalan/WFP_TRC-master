Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace VMS

    Public Class Visitors

#Region "Class Variables"


        Private _VisitorId As Integer
        Private _VisitorName As String
        Private _VisitorArabicName As String
        Private _Nationality As String
        Private _IDNumber As String
        Private _Gender As Integer
        Private _EIDExpiryDate As DateTime
        Private _DOB As DateTime
        Private _OrganizationName As String
        Private _MobileNumber As String
        Private _Remarks As String
        Private _IsDeleted As Boolean
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private _FK_VisitId As Integer
        Private objDALVisitors As DALVisitors


#End Region

#Region "Public Properties"

        Public Property VisitorId() As Integer
            Set(ByVal value As Integer)
                _VisitorId = value
            End Set
            Get
                Return (_VisitorId)
            End Get
        End Property

        Public Property VisitorName() As String
            Set(ByVal value As String)
                _VisitorName = value
            End Set
            Get
                Return (_VisitorName)
            End Get
        End Property

        Public Property VisitorArabicName() As String
            Set(ByVal value As String)
                _VisitorArabicName = value
            End Set
            Get
                Return (_VisitorArabicName)
            End Get
        End Property

        Public Property Nationality() As String
            Set(ByVal value As String)
                _Nationality = value
            End Set
            Get
                Return (_Nationality)
            End Get
        End Property

        Public Property IDNumber() As String
            Set(ByVal value As String)
                _IDNumber = value
            End Set
            Get
                Return (_IDNumber)
            End Get
        End Property

        Public Property Gender() As Integer
            Set(ByVal value As Integer)
                _Gender = value
            End Set
            Get
                Return (_Gender)
            End Get
        End Property

        Public Property EIDExpiryDate() As DateTime
            Set(ByVal value As DateTime)
                _EIDExpiryDate = value
            End Set
            Get
                Return (_EIDExpiryDate)
            End Get
        End Property

        Public Property DOB() As DateTime
            Set(ByVal value As DateTime)
                _DOB = value
            End Set
            Get
                Return (_DOB)
            End Get
        End Property

        Public Property OrganizationName() As String
            Set(ByVal value As String)
                _OrganizationName = value
            End Set
            Get
                Return (_OrganizationName)
            End Get
        End Property

        Public Property MobileNumber() As String
            Set(ByVal value As String)
                _MobileNumber = value
            End Set
            Get
                Return (_MobileNumber)
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

        Public Property IsDeleted() As Boolean
            Set(ByVal value As Boolean)
                _IsDeleted = value
            End Set
            Get
                Return (_IsDeleted)
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

        Public Property FK_VisitId() As Integer
            Set(ByVal value As Integer)
                _FK_VisitId = value
            End Set
            Get
                Return (_FK_VisitId)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALVisitors = New DALVisitors()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALVisitors.Add(_VisitorId, _VisitorName, _VisitorArabicName, _Nationality, _IDNumber, _Gender, _EIDExpiryDate, _DOB, _OrganizationName, _MobileNumber, _Remarks, _IsDeleted, _CREATED_BY)
            App_EventsLog.Insert_ToEventLog("Add", _VisitorId, "Visitors", "Schedule Visit")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALVisitors.Update(_VisitorId, _VisitorName, _VisitorArabicName, _Nationality, _IDNumber, _Gender, _EIDExpiryDate, _DOB, _OrganizationName, _MobileNumber, _Remarks, _IsDeleted, _LAST_UPDATE_BY)
            App_EventsLog.Insert_ToEventLog("Update", _VisitorId, "Visitors", "Schedule Visit")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALVisitors.Delete(_VisitorId)
            App_EventsLog.Insert_ToEventLog("Delete", _VisitorId, "Visitors", "Schedule Visit")
            Return rslt
        End Function

        Public Function DeleteByFK_VisitId() As Integer

            Return objDALVisitors.DeleteByFK_VisitId(_FK_VisitId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALVisitors.GetAll()

        End Function

        Public Function GetAllVisitDetails() As DataTable

            Return objDALVisitors.GetAllVisitDetails(_CREATED_BY)

        End Function

        Public Function GetByPK() As Visitors

            Dim dr As DataRow
            dr = objDALVisitors.GetByPK(_VisitorId)

            If Not IsDBNull(dr("VisitorId")) Then
                _VisitorId = dr("VisitorId")
            End If
            If Not IsDBNull(dr("VisitorName")) Then
                _VisitorName = dr("VisitorName")
            End If
            If Not IsDBNull(dr("VisitorArabicName")) Then
                _VisitorArabicName = dr("VisitorArabicName")
            End If
            If Not IsDBNull(dr("Nationality")) Then
                _Nationality = dr("Nationality")
            End If
            If Not IsDBNull(dr("IDNumber")) Then
                _IDNumber = dr("IDNumber")
            End If
            If Not IsDBNull(dr("Gender")) Then
                _Gender = dr("Gender")
            End If
            If Not IsDBNull(dr("EIDExpiryDate")) Then
                _EIDExpiryDate = dr("EIDExpiryDate")
            End If
            If Not IsDBNull(dr("DOB")) Then
                _DOB = dr("DOB")
            End If
            If Not IsDBNull(dr("OrganizationName")) Then
                _OrganizationName = dr("OrganizationName")
            End If
            If Not IsDBNull(dr("MobileNumber")) Then
                _MobileNumber = dr("MobileNumber")
            End If
            If Not IsDBNull(dr("Remarks")) Then
                _Remarks = dr("Remarks")
            End If
            If Not IsDBNull(dr("IsDeleted")) Then
                _IsDeleted = dr("IsDeleted")
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