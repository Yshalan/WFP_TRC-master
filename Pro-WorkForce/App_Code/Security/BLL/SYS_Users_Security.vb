Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports SmartV.UTILITIES

Namespace TA.Security

    Public Class SYS_Users_Security

#Region "Class Variables"


        Private _FK_UserId As Integer
        Private _FK_SecurityLevel As Integer
        Private _FK_CompanyId As Integer
        Private _FK_EntityId As Integer
        Private objDALSYS_Users_Security As DALSYS_Users_Security

#End Region

#Region "Public Properties"


        Public Property FK_UserId() As Integer
            Set(ByVal value As Integer)
                _FK_UserId = value
            End Set
            Get
                Return (_FK_UserId)
            End Get
        End Property


        Public Property FK_SecurityLevel() As Integer
            Set(ByVal value As Integer)
                _FK_SecurityLevel = value
            End Set
            Get
                Return (_FK_SecurityLevel)
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


        Public Property FK_EntityId() As Integer
            Set(ByVal value As Integer)
                _FK_EntityId = value
            End Set
            Get
                Return (_FK_EntityId)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALSYS_Users_Security = New DALSYS_Users_Security()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALSYS_Users_Security.Add(_FK_UserId, _FK_SecurityLevel, _FK_CompanyId, _FK_EntityId)
        End Function

        Public Function Update() As Integer

            Return objDALSYS_Users_Security.Update(_FK_UserId, _FK_SecurityLevel, _FK_CompanyId, _FK_EntityId)

        End Function



        Public Function Delete() As Integer

            Return objDALSYS_Users_Security.Delete(_FK_UserId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALSYS_Users_Security.GetAll()

        End Function

        Public Function GetByPK() As SYS_Users_Security

            Dim dr As DataRow
            dr = objDALSYS_Users_Security.GetByPK(_FK_UserId)
            If DTable.IsValidDataRow(dr) Then
                If Not IsDBNull(dr("FK_UserId")) Then
                    _FK_UserId = dr("FK_UserId")
                End If
                If Not IsDBNull(dr("FK_SecurityLevel")) Then
                    _FK_SecurityLevel = dr("FK_SecurityLevel")
                End If
                If Not IsDBNull(dr("FK_CompanyId")) Then
                    _FK_CompanyId = dr("FK_CompanyId")
                End If
                If Not IsDBNull(dr("FK_EntityId")) Then
                    _FK_EntityId = dr("FK_EntityId")
                End If
                Return Me
            End If
          
        End Function

        Public Function VerifyIfUserHasRights(Url As String) As Boolean
            Dim dt As DataTable = objDALSYS_Users_Security.VerifyIfUserHasRights(_FK_UserId, Url)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                Return True
            End If
            Return False
        End Function
#End Region

    End Class
End Namespace