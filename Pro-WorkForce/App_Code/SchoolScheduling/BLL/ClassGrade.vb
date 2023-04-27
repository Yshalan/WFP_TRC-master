Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA_SchoolScheduling

    Public Class ClassGrade

#Region "Class Variables"


        Private _ClassGradeId As Integer
        Private _ClassGradeName As String
        Private _ClassGradeNameAr As String
        Private _ClassGradeOrder As Integer
        Private _LAST_UPDATE_DATE As DateTime
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private objDALClassGrade As DALClassGrade

#End Region

#Region "Public Properties"


        Public Property ClassGradeId() As Integer
            Set(ByVal value As Integer)
                _ClassGradeId = value
            End Set
            Get
                Return (_ClassGradeId)
            End Get
        End Property


        Public Property ClassGradeName() As String
            Set(ByVal value As String)
                _ClassGradeName = value
            End Set
            Get
                Return (_ClassGradeName)
            End Get
        End Property


        Public Property ClassGradeNameAr() As String
            Set(ByVal value As String)
                _ClassGradeNameAr = value
            End Set
            Get
                Return (_ClassGradeNameAr)
            End Get
        End Property


        Public Property ClassGradeOrder() As Integer
            Set(ByVal value As Integer)
                _ClassGradeOrder = value
            End Set
            Get
                Return (_ClassGradeOrder)
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

#End Region


#Region "Constructor"

        Public Sub New()

            objDALClassGrade = New DALClassGrade()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALClassGrade.Add(_ClassGradeName, _ClassGradeNameAr, _ClassGradeOrder, _LAST_UPDATE_DATE, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY)
        End Function

        Public Function Update() As Integer

            Return objDALClassGrade.Update(_ClassGradeId, _ClassGradeName, _ClassGradeNameAr, _ClassGradeOrder, _LAST_UPDATE_DATE, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY)

        End Function



        Public Function Delete() As Integer

            Return objDALClassGrade.Delete(_ClassGradeId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALClassGrade.GetAll()

        End Function

        Public Function GetByPK() As ClassGrade

            Dim dr As DataRow
            dr = objDALClassGrade.GetByPK(_ClassGradeId)

            If Not IsDBNull(dr("ClassGradeId")) Then
                _ClassGradeId = dr("ClassGradeId")
            End If
            If Not IsDBNull(dr("ClassGradeName")) Then
                _ClassGradeName = dr("ClassGradeName")
            End If
            If Not IsDBNull(dr("ClassGradeNameAr")) Then
                _ClassGradeNameAr = dr("ClassGradeNameAr")
            End If
            If Not IsDBNull(dr("ClassGradeOrder")) Then
                _ClassGradeOrder = dr("ClassGradeOrder")
            End If
            If Not IsDBNull(dr("LAST_UPDATE_DATE")) Then
                _LAST_UPDATE_DATE = dr("LAST_UPDATE_DATE")
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
            Return Me
        End Function

#End Region

    End Class
End Namespace