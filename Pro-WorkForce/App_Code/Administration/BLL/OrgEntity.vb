Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Admin
    Public Class OrgEntity
#Region "Class Variables"
        Private _EntityId As Integer
        Private _EntityCode As String
        Private _FK_CompanyId As Integer
        Private _FK_ParentId As Integer
        Private _EntityName As String
        Private _EntityArabicName As String
        Private _FK_DefaultPolicyId As Integer
        Private _FK_LevelId As Integer
        Private _FK_ManagerId As Integer
        Private _FK_HighestPost As Integer
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALOrgEntity As DALOrgEntity

        Private _FK_UserId As Integer
        Private _FilterType As String

#End Region
#Region "Public Properties"
        Public Property EntityId() As Integer
            Set(ByVal value As Integer)
                _EntityId = value
            End Set
            Get
                Return (_EntityId)
            End Get
        End Property
        Public Property EntityCode() As String
            Set(ByVal value As String)
                _EntityCode = value
            End Set
            Get
                Return (_EntityCode)
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
        Public Property FK_ParentId() As Integer
            Set(ByVal value As Integer)
                _FK_ParentId = value
            End Set
            Get
                Return (_FK_ParentId)
            End Get
        End Property
        Public Property EntityName() As String
            Set(ByVal value As String)
                _EntityName = value
            End Set
            Get
                Return (_EntityName)
            End Get
        End Property
        Public Property EntityArabicName() As String
            Set(ByVal value As String)
                _EntityArabicName = value
            End Set
            Get
                Return (_EntityArabicName)
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
        Public Property FK_LevelId() As Integer
            Set(ByVal value As Integer)
                _FK_LevelId = value
            End Set
            Get
                Return (_FK_LevelId)
            End Get
        End Property
        Public Property FK_ManagerId() As Integer
            Set(ByVal value As Integer)
                _FK_ManagerId = value
            End Set
            Get
                Return (_FK_ManagerId)
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

            objDALOrgEntity = New DALOrgEntity()

        End Sub
#End Region
#Region "Methods"
        Public Function Add() As Integer
            Dim rslt As Integer = objDALOrgEntity.Add(_EntityCode, _FK_CompanyId, _FK_ParentId, _EntityName, _EntityArabicName, _FK_DefaultPolicyId, _FK_LevelId, _FK_ManagerId, _FK_HighestPost, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE)
            _EntityId = objDALOrgEntity.intEntityID
            App_EventsLog.Insert_ToEventLog("Add", _EntityId, "OrgEntity", "Organization Structure")
            Return rslt
        End Function
        Public Function Update() As Integer
            Dim rslt As Integer = objDALOrgEntity.Update(_EntityId, _EntityCode, _FK_CompanyId, _FK_ParentId, _EntityName, _EntityArabicName, _FK_DefaultPolicyId, _FK_LevelId, _FK_ManagerId, _FK_HighestPost, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE)
            App_EventsLog.Insert_ToEventLog("Edit", _EntityId, "OrgEntity", "Organization Structure")
            Return rslt

        End Function
        Public Function Delete() As Integer
            Dim rslt As Integer = objDALOrgEntity.Delete(_EntityId)
            App_EventsLog.Insert_ToEventLog("Delete", _EntityId, "OrgEntity", "Organization Structure")
            Return rslt
        End Function
        Public Function GetEmployeesByOrgEntity() As DataTable

            Return objDALOrgEntity.GetEmployeesByOrgEntity(_EntityId)

        End Function
        Public Function GetAll() As DataTable

            Return objDALOrgEntity.GetAll()

        End Function
        Public Function GetAll_WithCompanyName() As DataTable

            Return objDALOrgEntity.GetAll_WithCompanyName(_FK_CompanyId)

        End Function
        Public Function CheckChildEntityexists() As Integer

            Return objDALOrgEntity.CheckChildEntityexists(_EntityId)

        End Function
        Public Function GetAllForDDL() As DataTable

            Return objDALOrgEntity.GetAllForDDL()

        End Function
        Public Function SelectAllForDDLByCompany() As DataTable

            Return objDALOrgEntity.SelectAllForDDLByCompany(_FK_CompanyId)

        End Function
        Public Function GetAllchildsByParent() As DataTable

            Return objDALOrgEntity.GetAllchildsByParent(_FK_ParentId)

        End Function

        Public Function GetEntityAndChilds() As DataTable

            Return objDALOrgEntity.GetEntityAndChilds(_EntityId)

        End Function

        Public Function GetAllEntityByCompany() As DataTable

            Return objDALOrgEntity.GetAllEntityByCompany(_FK_CompanyId)

        End Function

        Public Function GetAllEntityByCompany_Chart() As DataTable

            Return objDALOrgEntity.GetAllEntityByCompany_Chart(_FK_CompanyId)

        End Function

        Public Function GetAllEntityBy_CompanyandLevel() As DataTable

            Return objDALOrgEntity.GetAllEntityBy_CompanyandLevel(_FK_CompanyId, _FK_LevelId)

        End Function

        Public Function GetAllEntityByCompanyAndByUserId() As DataTable

            Return objDALOrgEntity.GetAllEntityByCompanyAndByUserId(_FK_CompanyId, _FK_UserId)

        End Function

        Public Function GetParentNameByEntityID() As String

            Return objDALOrgEntity.GetParentNameByEntityID(_FK_ParentId)

        End Function
        Public Function GetByPK() As OrgEntity
            Dim dr As DataRow
            dr = objDALOrgEntity.GetByPK(_EntityId)
            If (dr IsNot Nothing) Then
                If Not IsDBNull(dr("EntityId")) Then
                    _EntityId = dr("EntityId")
                End If
                If Not IsDBNull(dr("EntityCode")) Then
                    _EntityCode = dr("EntityCode")
                End If
                If Not IsDBNull(dr("FK_CompanyId")) Then
                    _FK_CompanyId = dr("FK_CompanyId")
                End If
                If Not IsDBNull(dr("FK_ParentId")) Then
                    _FK_ParentId = dr("FK_ParentId")
                End If
                If Not IsDBNull(dr("EntityName")) Then
                    _EntityName = dr("EntityName")
                End If
                If Not IsDBNull(dr("EntityArabicName")) Then
                    _EntityArabicName = dr("EntityArabicName")
                End If
                If Not IsDBNull(dr("FK_DefaultPolicyId")) Then
                    _FK_DefaultPolicyId = dr("FK_DefaultPolicyId")
                End If
                If Not IsDBNull(dr("FK_LevelId")) Then
                    _FK_LevelId = dr("FK_LevelId")
                End If
                If Not IsDBNull(dr("FK_ManagerId")) Then
                    _FK_ManagerId = dr("FK_ManagerId")
                End If
                If Not IsDBNull(dr("FK_HighestPost")) Then
                    _FK_HighestPost = dr("FK_HighestPost")
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
            End If
            Return Me
        End Function

        Public Function GetDefaultPolicy() As Integer
            Return objDALOrgEntity.GetDefaultPolicy(_FK_CompanyId)
        End Function

        Public Function GetParentManager_ByEntityId() As OrgEntity
            Dim dr As DataRow
            dr = objDALOrgEntity.GetParentManager_ByEntityId(_EntityId)
            If (dr IsNot Nothing) Then
                If Not IsDBNull(dr("FK_ManagerId")) Then
                    _FK_ManagerId = dr("FK_ManagerId")
                End If
            End If
            Return Me
        End Function

        Public Function GetChildManager_ByEntityId() As DataTable

            Return objDALOrgEntity.GetChildManager_ByEntityId(_EntityId)

        End Function

        Public Function GetAllEntityByCompanyAndManger() As DataTable

            Return objDALOrgEntity.GetAllEntityByCompanyAndManger(_FK_CompanyId, _FK_ManagerId)

        End Function
        Public Function GetAllEntityByCompanyAndMangerforforce() As DataTable

            Return objDALOrgEntity.GetAllEntityByCompanyAndMangerforforce(_FK_CompanyId, _FK_ManagerId)

        End Function
        Public Function GetAllEntityByManger() As DataTable

            Return objDALOrgEntity.GetAllEntityByManger(_FK_ManagerId)

        End Function

        Public Function Get_EntityBy_FK_MangerId() As OrgEntity
            Dim dr As DataRow
            dr = objDALOrgEntity.Get_EntityBy_FK_MangerId(_FK_ManagerId)
            If (dr IsNot Nothing) Then
                If Not IsDBNull(dr("EntityId")) Then
                    _EntityId = dr("EntityId")
                End If
                If Not IsDBNull(dr("EntityCode")) Then
                    _EntityCode = dr("EntityCode")
                End If
                If Not IsDBNull(dr("FK_CompanyId")) Then
                    _FK_CompanyId = dr("FK_CompanyId")
                End If
                If Not IsDBNull(dr("FK_ParentId")) Then
                    _FK_ParentId = dr("FK_ParentId")
                End If
                If Not IsDBNull(dr("EntityName")) Then
                    _EntityName = dr("EntityName")
                End If
                If Not IsDBNull(dr("EntityArabicName")) Then
                    _EntityArabicName = dr("EntityArabicName")
                End If
                If Not IsDBNull(dr("FK_DefaultPolicyId")) Then
                    _FK_DefaultPolicyId = dr("FK_DefaultPolicyId")
                End If
                If Not IsDBNull(dr("FK_LevelId")) Then
                    _FK_LevelId = dr("FK_LevelId")
                End If
                If Not IsDBNull(dr("FK_ManagerId")) Then
                    _FK_ManagerId = dr("FK_ManagerId")
                End If
                If Not IsDBNull(dr("FK_HighestPost")) Then
                    _FK_HighestPost = dr("FK_HighestPost")
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
            End If
            Return Me
        End Function

        Public Function GetAllEntitysByManger() As DataTable

            Return objDALOrgEntity.GetAllEntitysByManger(_FK_ManagerId)

        End Function

#End Region
    End Class
End Namespace