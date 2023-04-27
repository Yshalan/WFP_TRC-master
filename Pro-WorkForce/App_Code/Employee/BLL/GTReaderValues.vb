Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Lookup

Namespace TA.Lookup

    Public Class GTReaderValues

#Region "Class Variables"


        Private _Value As String
        Private _TextEn As String
        Private _TextAr As String
        Private objDALGTReaderValues As DALGTReaderValues

#End Region

#Region "Public Properties"


        Public Property Value() As String
            Set(ByVal value As String)
                _Value = value
            End Set
            Get
                Return (_Value)
            End Get
        End Property


        Public Property TextEn() As String
            Set(ByVal value As String)
                _TextEn = value
            End Set
            Get
                Return (_TextEn)
            End Get
        End Property


        Public Property TextAr() As String
            Set(ByVal value As String)
                _TextAr = value
            End Set
            Get
                Return (_TextAr)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALGTReaderValues = New DALGTReaderValues()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALGTReaderValues.Add(_Value, _TextEn, _TextAr)
        End Function

        Public Function Update() As Integer

            Return objDALGTReaderValues.Update(_Value, _TextEn, _TextAr)

        End Function



        Public Function Delete() As Integer

            Return objDALGTReaderValues.Delete(_Value)

        End Function

        Public Function GetAll() As DataTable

            Return objDALGTReaderValues.GetAll()

        End Function

        Public Function GetByPK() As GTReaderValues

            Dim dr As DataRow
            dr = objDALGTReaderValues.GetByPK(_Value)

            If Not IsDBNull(dr("Value")) Then
                _Value = dr("Value")
            End If
            If Not IsDBNull(dr("TextEn")) Then
                _TextEn = dr("TextEn")
            End If
            If Not IsDBNull(dr("TextAr")) Then
                _TextAr = dr("TextAr")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace
