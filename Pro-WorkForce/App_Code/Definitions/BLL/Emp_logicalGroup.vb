Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Definitions

    Public Class Emp_logicalGroup

#Region "Class Variables"


        Private _GroupId As Integer
        Private _FK_CompanyId As Integer
        Private _GroupName As String
        Private _GroupArabicName As String
        Private _FK_TAPolicyId As Integer
        Private _Active As Boolean
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private _FK_UserId As Integer
        Private _AllowPunchOutSideLocation As Boolean
        Private objDALEmp_logicalGroup As DALEmp_logicalGroup

#End Region

#Region "Public Properties"


        Public Property GroupId() As Integer
            Set(ByVal value As Integer)
                _GroupId = value
            End Set
            Get
                Return (_GroupId)
            End Get
        End Property

        Public Property FK_CompanyId() As Integer
            Set(ByVal value As Integer)
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

        Public Property GroupName() As String
            Set(ByVal value As String)
                _GroupName = value
            End Set
            Get
                Return (_GroupName)
            End Get
        End Property

        Public Property GroupArabicName() As String
            Set(ByVal value As String)
                _GroupArabicName = value
            End Set
            Get
                Return (_GroupArabicName)
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

        Public Property AllowPunchOutSideLocation() As Boolean
            Set(ByVal value As Boolean)
                _AllowPunchOutSideLocation = value
            End Set
            Get
                Return (_AllowPunchOutSideLocation)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALEmp_logicalGroup = New DALEmp_logicalGroup()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer
            Dim rslt As Integer = objDALEmp_logicalGroup.Add(_GroupId, _GroupName, _GroupArabicName, _FK_TAPolicyId, _Active, _AllowPunchOutSideLocation)
            App_EventsLog.Insert_ToEventLog("Add", _GroupId, "Emp_logicalGroup", "Define Logical Group")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALEmp_logicalGroup.Update(_GroupId, _GroupName, _GroupArabicName, _FK_TAPolicyId, _Active, _AllowPunchOutSideLocation)
            App_EventsLog.Insert_ToEventLog("Edit", _GroupId, "Emp_logicalGroup", "Define Logical Group")
            Return rslt

        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALEmp_logicalGroup.Delete(_GroupId)
            App_EventsLog.Insert_ToEventLog("Delete", _GroupId, "Emp_logicalGroup", "Define Logical Group")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_logicalGroup.GetAll()

        End Function

        Public Function GetAllByCompany() As DataTable

            Return objDALEmp_logicalGroup.GetAllByCompany(_FK_CompanyId)

        End Function

        Public Function GetAllByCompanyAndUserId() As DataTable

            Return objDALEmp_logicalGroup.GetAllByCompanyAndUserId(_FK_CompanyId, _FK_UserId)

        End Function

        Public Function GetByPK() As Emp_logicalGroup

            Dim dr As DataRow
            dr = objDALEmp_logicalGroup.GetByPK(_GroupId)

            If Not IsDBNull(dr("GroupId")) Then
                _GroupId = dr("GroupId")
            End If
            If Not IsDBNull(dr("GroupName")) Then
                _GroupName = dr("GroupName")
            End If
            If Not IsDBNull(dr("GroupArabicName")) Then
                _GroupArabicName = dr("GroupArabicName")
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
            If Not IsDBNull(dr("AllowPunchOutSideLocation")) Then
                _AllowPunchOutSideLocation = dr("AllowPunchOutSideLocation")
            End If

            Return Me
        End Function

        Public Function GetAll_ForDDL() As DataTable

            Return objDALEmp_logicalGroup.GetAll_ForDDL()

        End Function

#End Region

    End Class
End Namespace