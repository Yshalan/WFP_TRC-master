Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Accounts

    Public Class BU_AccountsCOOInfo

#Region "Class Variables"


        Private _FK_AccountId As Integer
        Private _EstablishmentDate As String
        Private _CapitalUSD As Double
        Private _TurnoverUSD As Double
        Private _MAHYear As Integer
        Private _LicenseAuthority As String
        Private _LicenseNo As String
        Private _LicenseIssueDate As String
        Private _LicenseExpiryDate As String
        Private _AuthorizedPersonResponsibilities As String
        Private _OtherDate1 As DateTime = Today
        Private _OtherInt1 As Integer = 0
        Private _OtherBit1 As Boolean = False
        Private _OtherVar1 As String = ""
        Private objDALBU_AccountsCOOInfo As DALBU_AccountsCOOInfo

#End Region

#Region "Public Properties"


        Public Property FK_AccountId() As Integer
            Set(ByVal value As Integer)
                _FK_AccountId = value
            End Set
            Get
                Return (_FK_AccountId)
            End Get
        End Property


        Public Property EstablishmentDate() As String
            Set(ByVal value As String)
                _EstablishmentDate = value
            End Set
            Get
                Return (_EstablishmentDate)
            End Get
        End Property


        Public Property CapitalUSD() As Double
            Set(ByVal value As Double)
                _CapitalUSD = value
            End Set
            Get
                Return (_CapitalUSD)
            End Get
        End Property


        Public Property TurnoverUSD() As Double
            Set(ByVal value As Double)
                _TurnoverUSD = value
            End Set
            Get
                Return (_TurnoverUSD)
            End Get
        End Property


        Public Property MAHYear() As Integer
            Set(ByVal value As Integer)
                _MAHYear = value
            End Set
            Get
                Return (_MAHYear)
            End Get
        End Property


        Public Property LicenseAuthority() As String
            Set(ByVal value As String)
                _LicenseAuthority = value
            End Set
            Get
                Return (_LicenseAuthority)
            End Get
        End Property


        Public Property LicenseNo() As String
            Set(ByVal value As String)
                _LicenseNo = value
            End Set
            Get
                Return (_LicenseNo)
            End Get
        End Property


        Public Property LicenseIssueDate() As String
            Set(ByVal value As String)
                _LicenseIssueDate = value
            End Set
            Get
                Return (_LicenseIssueDate)
            End Get
        End Property


        Public Property LicenseExpiryDate() As String
            Set(ByVal value As String)
                _LicenseExpiryDate = value
            End Set
            Get
                Return (_LicenseExpiryDate)
            End Get
        End Property


        Public Property AuthorizedPersonResponsibilities() As String
            Set(ByVal value As String)
                _AuthorizedPersonResponsibilities = value
            End Set
            Get
                Return (_AuthorizedPersonResponsibilities)
            End Get
        End Property


        Public Property OtherDate1() As String
            Set(ByVal value As String)
                _OtherDate1 = value
            End Set
            Get
                Return (_OtherDate1)
            End Get
        End Property


        Public Property OtherInt1() As Integer
            Set(ByVal value As Integer)
                _OtherInt1 = value
            End Set
            Get
                Return (_OtherInt1)
            End Get
        End Property


        Public Property OtherBit1() As Boolean
            Set(ByVal value As Boolean)
                _OtherBit1 = value
            End Set
            Get
                Return (_OtherBit1)
            End Get
        End Property


        Public Property OtherVar1() As String
            Set(ByVal value As String)
                _OtherVar1 = value
            End Set
            Get
                Return (_OtherVar1)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALBU_AccountsCOOInfo = New DALBU_AccountsCOOInfo()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALBU_AccountsCOOInfo.Add(_FK_AccountId, _EstablishmentDate, _CapitalUSD, _TurnoverUSD, _MAHYear, _LicenseAuthority, _LicenseNo, _LicenseIssueDate, _LicenseExpiryDate, _AuthorizedPersonResponsibilities, _OtherDate1, _OtherInt1, _OtherBit1, _OtherVar1)
        End Function

        Public Function Update() As Integer

            Return objDALBU_AccountsCOOInfo.Update(_FK_AccountId, _EstablishmentDate, _CapitalUSD, _TurnoverUSD, _MAHYear, _LicenseAuthority, _LicenseNo, _LicenseIssueDate, _LicenseExpiryDate, _AuthorizedPersonResponsibilities, _OtherDate1, _OtherInt1, _OtherBit1, _OtherVar1)

        End Function



        Public Function Delete() As Integer

            Return objDALBU_AccountsCOOInfo.Delete(_FK_AccountId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALBU_AccountsCOOInfo.GetAll()

        End Function

        Public Function GetByPK() As BU_AccountsCOOInfo

            Dim dr As DataRow
            dr = objDALBU_AccountsCOOInfo.GetByPK(_FK_AccountId)
            If Not dr Is Nothing Then
                If Not IsDBNull(dr("FK_AccountId")) Then
                    _FK_AccountId = dr("FK_AccountId")
                End If
                If Not IsDBNull(dr("EstablishmentDate")) Then
                    _EstablishmentDate = dr("EstablishmentDate")
                End If
                If Not IsDBNull(dr("CapitalUSD")) Then
                    _CapitalUSD = dr("CapitalUSD")
                End If
                If Not IsDBNull(dr("TurnoverUSD")) Then
                    _TurnoverUSD = dr("TurnoverUSD")
                End If
                If Not IsDBNull(dr("MAHYear")) Then
                    _MAHYear = dr("MAHYear")
                End If
                If Not IsDBNull(dr("LicenseAuthority")) Then
                    _LicenseAuthority = dr("LicenseAuthority")
                End If
                If Not IsDBNull(dr("LicenseNo")) Then
                    _LicenseNo = dr("LicenseNo")
                End If
                If Not IsDBNull(dr("LicenseIssueDate")) Then
                    _LicenseIssueDate = dr("LicenseIssueDate")
                End If
                If Not IsDBNull(dr("LicenseExpiryDate")) Then
                    _LicenseExpiryDate = dr("LicenseExpiryDate")
                End If
                If Not IsDBNull(dr("AuthorizedPersonResponsibilities")) Then
                    _AuthorizedPersonResponsibilities = dr("AuthorizedPersonResponsibilities")
                End If
                If Not IsDBNull(dr("OtherDate1")) Then
                    _OtherDate1 = dr("OtherDate1")
                End If
                If Not IsDBNull(dr("OtherInt1")) Then
                    _OtherInt1 = dr("OtherInt1")
                End If
                If Not IsDBNull(dr("OtherBit1")) Then
                    _OtherBit1 = dr("OtherBit1")
                End If
                If Not IsDBNull(dr("OtherVar1")) Then
                    _OtherVar1 = dr("OtherVar1")
                End If
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace