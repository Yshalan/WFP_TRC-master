Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Definitions

    Public Class PermissionsTypes_Entity

#Region "Class Variables"


        Private _FK_PermId As Long
        Private _FK_EntityId As Long
        Private objDALPermissionsTypes_Entity As DALPermissionsTypes_Entity

#End Region

#Region "Public Properties"


        Public Property FK_PermId() As Long
            Set(ByVal value As Long)
                _FK_PermId = value
            End Set
            Get
                Return (_FK_PermId)
            End Get
        End Property


        Public Property FK_EntityId() As Long
            Set(ByVal value As Long)
                _FK_EntityId = value
            End Set
            Get
                Return (_FK_EntityId)
            End Get
        End Property

#End Region

#Region "Constructor"

        Public Sub New()

            objDALPermissionsTypes_Entity = New DALPermissionsTypes_Entity()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALPermissionsTypes_Entity.Add(_FK_PermId, _FK_EntityId)
        End Function

        Public Function Update() As Integer

            Return objDALPermissionsTypes_Entity.Update(_FK_PermId, _FK_EntityId)

        End Function

        Public Function Delete() As Integer

            Return objDALPermissionsTypes_Entity.Delete(_FK_PermId, _FK_EntityId)

        End Function

        Public Function DeleteByPermId() As Integer

            Return objDALPermissionsTypes_Entity.DeleteByPermId(_FK_PermId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALPermissionsTypes_Entity.GetAll()

        End Function

        Public Function GetByPermId() As DataTable

            Return objDALPermissionsTypes_Entity.GetByPermId(_FK_PermId)

        End Function

        Public Function GetByPK() As PermissionsTypes_Entity

            Dim dr As DataRow
            dr = objDALPermissionsTypes_Entity.GetByPK(_FK_PermId, _FK_EntityId)

            If Not IsDBNull(dr("FK_PermId")) Then
                _FK_PermId = dr("FK_PermId")
            End If
            If Not IsDBNull(dr("FK_EntityId")) Then
                _FK_EntityId = dr("FK_EntityId")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace