Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Definitions

    Public Class PermissionsTypes_Company

#Region "Class Variables"


        Private _FK_PermId As Long
        Private _FK_CompanyId As Long
        Private objDALPermissionsTypes_Company As DALPermissionsTypes_Company

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


        Public Property FK_CompanyId() As Long
            Set(ByVal value As Long)
                _FK_CompanyId = value
            End Set
            Get
                Return (_FK_CompanyId)
            End Get
        End Property

#End Region

#Region "Constructor"

        Public Sub New()

            objDALPermissionsTypes_Company = New DALPermissionsTypes_Company()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALPermissionsTypes_Company.Add(_FK_PermId, _FK_CompanyId)
        End Function

        Public Function Update() As Integer

            Return objDALPermissionsTypes_Company.Update(_FK_PermId, _FK_CompanyId)

        End Function



        Public Function Delete() As Integer

            Return objDALPermissionsTypes_Company.Delete(_FK_PermId, _FK_CompanyId)

        End Function

        Public Function DeleteByPermId() As Integer

            Return objDALPermissionsTypes_Company.DeleteByPermId(_FK_PermId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALPermissionsTypes_Company.GetAll()

        End Function

        Public Function GetByPermId() As DataTable

            Return objDALPermissionsTypes_Company.GetByPermId(_FK_PermId)

        End Function
        Public Function GetByPK() As PermissionsTypes_Company

            Dim dr As DataRow
            dr = objDALPermissionsTypes_Company.GetByPK(_FK_PermId, _FK_CompanyId)

            If Not IsDBNull(dr("FK_PermId")) Then
                _FK_PermId = dr("FK_PermId")
            End If
            If Not IsDBNull(dr("FK_CompanyId")) Then
                _FK_CompanyId = dr("FK_CompanyId")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace