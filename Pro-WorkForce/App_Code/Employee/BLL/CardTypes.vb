Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Admin

    Public Class CardTypes

#Region "Class Variables"


        Private _CardTypeId As Integer
        Private _CardTypeEn As String
        Private _CardTypeAr As String
        Private _Fk_TemplateId As Integer
        Private _CardApproval As Integer
        Private _CardRequestManagerLevelRequired As Integer
        Private _CreatedBY As String
        Private _CreatedDate As DateTime
        Private _LastUpdatedBy As String
        Private _LastUpdatedDate As DateTime
        Private objDALCardTypes As DALCardTypes

#End Region

#Region "Public Properties"


        Public Property CardTypeId() As Integer
            Set(ByVal value As Integer)
                _CardTypeId = value
            End Set
            Get
                Return (_CardTypeId)
            End Get
        End Property


        Public Property CardTypeEn() As String
            Set(ByVal value As String)
                _CardTypeEn = value
            End Set
            Get
                Return (_CardTypeEn)
            End Get
        End Property


        Public Property CardTypeAr() As String
            Set(ByVal value As String)
                _CardTypeAr = value
            End Set
            Get
                Return (_CardTypeAr)
            End Get
        End Property


        Public Property Fk_TemplateId() As Integer
            Set(ByVal value As Integer)
                _Fk_TemplateId = value
            End Set
            Get
                Return (_Fk_TemplateId)
            End Get
        End Property
        Public Property CardApproval() As Integer
            Set(ByVal value As Integer)
                _CardApproval = value
            End Set
            Get
                Return (_CardApproval)
            End Get
        End Property
        Public Property CardRequestManagerLevelRequired() As Integer
            Set(ByVal value As Integer)
                _CardRequestManagerLevelRequired = value
            End Set
            Get
                Return (_CardRequestManagerLevelRequired)
            End Get
        End Property

        Public Property CreatedBY() As String
            Set(ByVal value As String)
                _CreatedBY = value
            End Set
            Get
                Return (_CreatedBY)
            End Get
        End Property


        Public Property CreatedDate() As DateTime
            Set(ByVal value As DateTime)
                _CreatedDate = value
            End Set
            Get
                Return (_CreatedDate)
            End Get
        End Property


        Public Property LastUpdatedBy() As String
            Set(ByVal value As String)
                _LastUpdatedBy = value
            End Set
            Get
                Return (_LastUpdatedBy)
            End Get
        End Property


        Public Property LastUpdatedDate() As DateTime
            Set(ByVal value As DateTime)
                _LastUpdatedDate = value
            End Set
            Get
                Return (_LastUpdatedDate)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALCardTypes = New DALCardTypes()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALCardTypes.Add(_CardTypeId, _CardTypeEn, _CardTypeAr, _Fk_TemplateId, _CardApproval, _CardRequestManagerLevelRequired, _CreatedBY, _CreatedDate, _LastUpdatedBy, _LastUpdatedDate)
        End Function

        Public Function Update() As Integer

            Return objDALCardTypes.Update(_CardTypeId, _CardTypeEn, _CardTypeAr, _Fk_TemplateId, _CardApproval, _CardRequestManagerLevelRequired, _CreatedBY, _CreatedDate, _LastUpdatedBy, _LastUpdatedDate)

        End Function



        Public Function Delete() As Integer

            Return objDALCardTypes.Delete(_CardTypeId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALCardTypes.GetAll()

        End Function
        Public Function GetAllByDesignation(ByVal Fk_designation As Integer) As DataTable

            Return objDALCardTypes.GetAllByDesignation(Fk_designation)

        End Function

        Public Function GetByPK() As CardTypes

            Dim dr As DataRow
            dr = objDALCardTypes.GetByPK(_CardTypeId)

            If Not IsDBNull(dr("CardTypeId")) Then
                _CardTypeId = dr("CardTypeId")
            End If
            If Not IsDBNull(dr("CardTypeEn")) Then
                _CardTypeEn = dr("CardTypeEn")
            End If
            If Not IsDBNull(dr("CardTypeAr")) Then
                _CardTypeAr = dr("CardTypeAr")
            End If
            If Not IsDBNull(dr("Fk_TemplateId")) Then
                _Fk_TemplateId = dr("Fk_TemplateId")
            End If
            If Not IsDBNull(dr("CardApproval")) Then
                _CardApproval = dr("CardApproval")
            End If
            If Not IsDBNull(dr("CardRequestManagerLevelRequired")) Then
                _CardRequestManagerLevelRequired = dr("CardRequestManagerLevelRequired")
            End If
            If Not IsDBNull(dr("CreatedBY")) Then
                _CreatedBY = dr("CreatedBY")
            End If
            If Not IsDBNull(dr("CreatedDate")) Then
                _CreatedDate = dr("CreatedDate")
            End If
            If Not IsDBNull(dr("LastUpdatedBy")) Then
                _LastUpdatedBy = dr("LastUpdatedBy")
            End If
            If Not IsDBNull(dr("LastUpdatedDate")) Then
                _LastUpdatedDate = dr("LastUpdatedDate")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace
