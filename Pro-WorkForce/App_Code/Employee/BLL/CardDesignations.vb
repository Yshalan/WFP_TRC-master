Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Admin

    Public Class Card_Designations

#Region "Class Variables"


        Private _Card_DesignationId As Integer
        Private _Fk_DesignationId As Integer
        Private _Fk_CardTypeId As Integer
        Private objDALCard_Designations As DALCard_Designations

#End Region

#Region "Public Properties"


        Public Property Card_DesignationId() As Integer
            Set(ByVal value As Integer)
                _Card_DesignationId = value
            End Set
            Get
                Return (_Card_DesignationId)
            End Get
        End Property


        Public Property Fk_DesignationId() As Integer
            Set(ByVal value As Integer)
                _Fk_DesignationId = value
            End Set
            Get
                Return (_Fk_DesignationId)
            End Get
        End Property


        Public Property Fk_CardTypeId() As Integer
            Set(ByVal value As Integer)
                _Fk_CardTypeId = value
            End Set
            Get
                Return (_Fk_CardTypeId)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALCard_Designations = New DALCard_Designations()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALCard_Designations.Add(_Card_DesignationId, _Fk_DesignationId, _Fk_CardTypeId)
        End Function

        Public Function Update() As Integer

            Return objDALCard_Designations.Update(_Card_DesignationId, _Fk_DesignationId, _Fk_CardTypeId)

        End Function



        Public Function Delete() As Integer

            Return objDALCard_Designations.Delete(_Fk_CardTypeId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALCard_Designations.GetAll()

        End Function

        Public Function GetAllByCardType() As DataTable

            Return objDALCard_Designations.GetAllByCardType(_Fk_CardTypeId)

        End Function
        Public Function GetAllByDesignation() As DataTable

            Return objDALCard_Designations.GetAllByDesignation(_Fk_DesignationId)

        End Function

        Public Function GetByPK() As Card_Designations

            Dim dr As DataRow
            dr = objDALCard_Designations.GetByPK(_Card_DesignationId)

            If Not IsDBNull(dr("Card_DesignationId")) Then
                _Card_DesignationId = dr("Card_DesignationId")
            End If
            If Not IsDBNull(dr("Fk_DesignationId")) Then
                _Fk_DesignationId = dr("Fk_DesignationId")
            End If
            If Not IsDBNull(dr("Fk_CardTypeId")) Then
                _Fk_CardTypeId = dr("Fk_CardTypeId")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace
