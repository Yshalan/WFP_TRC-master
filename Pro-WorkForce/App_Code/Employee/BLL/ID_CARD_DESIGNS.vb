Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Card

    Public Class ID_CARD_DESIGNS

#Region "Class Variables"


        Private _DESIGN_ID As String
        Private _DESIGN_PATH As String
        Private _DESIGN_DESC As String
        Private _DESIGN_ARB_DESC As String
        Private objDALID_CARD_DESIGNS As DALID_CARD_DESIGNS

#End Region

#Region "Public Properties"


        Public Property DESIGN_ID() As String
            Set(ByVal value As String)
                _DESIGN_ID = value
            End Set
            Get
                Return (_DESIGN_ID)
            End Get
        End Property


        Public Property DESIGN_PATH() As String
            Set(ByVal value As String)
                _DESIGN_PATH = value
            End Set
            Get
                Return (_DESIGN_PATH)
            End Get
        End Property


        Public Property DESIGN_DESC() As String
            Set(ByVal value As String)
                _DESIGN_DESC = value
            End Set
            Get
                Return (_DESIGN_DESC)
            End Get
        End Property


        Public Property DESIGN_ARB_DESC() As String
            Set(ByVal value As String)
                _DESIGN_ARB_DESC = value
            End Set
            Get
                Return (_DESIGN_ARB_DESC)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALID_CARD_DESIGNS = New DALID_CARD_DESIGNS()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALID_CARD_DESIGNS.Add(_DESIGN_ID, _DESIGN_PATH, _DESIGN_DESC, _DESIGN_ARB_DESC)
        End Function

        Public Function Update() As Integer

            Return objDALID_CARD_DESIGNS.Update(_DESIGN_ID, _DESIGN_PATH, _DESIGN_DESC, _DESIGN_ARB_DESC)

        End Function



        Public Function Delete() As Integer

            Return objDALID_CARD_DESIGNS.Delete(_DESIGN_ID)

        End Function

        Public Function GetAll() As DataTable

            Return objDALID_CARD_DESIGNS.GetAll()

        End Function

        Public Function GetByPK() As ID_CARD_DESIGNS

            Dim dr As DataRow
            dr = objDALID_CARD_DESIGNS.GetByPK(_DESIGN_ID)

            If Not IsDBNull(dr("DESIGN_ID")) Then
                _DESIGN_ID = dr("DESIGN_ID")
            End If
            If Not IsDBNull(dr("DESIGN_PATH")) Then
                _DESIGN_PATH = dr("DESIGN_PATH")
            End If
            If Not IsDBNull(dr("DESIGN_DESC")) Then
                _DESIGN_DESC = dr("DESIGN_DESC")
            End If
            If Not IsDBNull(dr("DESIGN_ARB_DESC")) Then
                _DESIGN_ARB_DESC = dr("DESIGN_ARB_DESC")
            End If
            Return Me
        End Function

        Public Function Get_MaxDesignId() As ID_CARD_DESIGNS

            Dim dr As DataRow
            dr = objDALID_CARD_DESIGNS.Get_MaxDesignId()

            If Not IsDBNull(dr("DESIGN_ID")) Then
                _DESIGN_ID = dr("DESIGN_ID")
            End If

            Return Me
        End Function

#End Region

    End Class
End Namespace