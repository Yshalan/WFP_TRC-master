Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Admin

    Public Class App_EmailConfigurations

#Region "Class Variables"


        Private _SMTP_Server As String
        Private _EmailFrom As String
        Private _EnableEmailService As Boolean
        Private _EnableSMSService As Boolean
        Private _SMTPUserName As String
        Private _SMTPPassword As String
        Private objDALApp_EmailConfigurations As DALApp_EmailConfigurations

#End Region

#Region "Public Properties"

        Public Property SMTP_Server() As String
            Set(ByVal value As String)
                _SMTP_Server = value
            End Set
            Get
                Return (_SMTP_Server)
            End Get
        End Property

        Public Property EmailFrom() As String
            Set(ByVal value As String)
                _EmailFrom = value
            End Set
            Get
                Return (_EmailFrom)
            End Get
        End Property

        Public Property EnableEmailService() As Boolean
            Set(ByVal value As Boolean)
                _EnableEmailService = value
            End Set
            Get
                Return (_EnableEmailService)
            End Get
        End Property

        Public Property EnableSMSService() As Boolean
            Set(ByVal value As Boolean)
                _EnableSMSService = value
            End Set
            Get
                Return (_EnableSMSService)
            End Get
        End Property

        Public Property SMTPUserName() As String
            Get
                Return _SMTPUserName
            End Get
            Set(ByVal value As String)
                _SMTPUserName = value
            End Set
        End Property

        Public Property SMTPPassword() As String
            Get
                Return _SMTPPassword
            End Get
            Set(ByVal value As String)
                _SMTPPassword = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Public Sub New()

            objDALApp_EmailConfigurations = New DALApp_EmailConfigurations()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALApp_EmailConfigurations.Add(_SMTP_Server, _EmailFrom, _EnableEmailService, _EnableSMSService, _SMTPUserName, _SMTPPassword)
        End Function

        Public Function Update() As Integer

            Return objDALApp_EmailConfigurations.Update(_SMTP_Server, _EmailFrom, _EnableEmailService, _EnableSMSService, _SMTPUserName, _SMTPPassword)

        End Function

        Public Function Delete() As Integer

            Return objDALApp_EmailConfigurations.Delete(_SMTP_Server)

        End Function

        Public Function GetAll() As DataTable

            Return objDALApp_EmailConfigurations.GetAll()

        End Function

        Public Function GetByPK() As App_EmailConfigurations

            Dim dr As DataRow
            dr = objDALApp_EmailConfigurations.GetByPK()

            If Not dr Is Nothing Then
                If Not IsDBNull(dr("SMTP_Server")) Then
                    _SMTP_Server = dr("SMTP_Server")
                End If
                If Not IsDBNull(dr("EmailFrom")) Then
                    _EmailFrom = dr("EmailFrom")
                End If
                If Not IsDBNull(dr("EnableEmailService")) Then
                    _EnableEmailService = dr("EnableEmailService")
                End If
                If Not IsDBNull(dr("EnableSMSService")) Then
                    _EnableSMSService = dr("EnableSMSService")
                End If
                If Not IsDBNull(dr("SMTPUser")) Then
                    _SMTPUserName = dr("SMTPUser")
                End If
                If Not IsDBNull(dr("SMTPPassword")) Then
                    _SMTPPassword = dr("SMTPPassword")
                End If
                Return Me
            Else
                Return Nothing
            End If
        End Function

#End Region

    End Class
End Namespace