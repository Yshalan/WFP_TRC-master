Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Lookup

    Public Class RequestStatus

#Region "Class Variables"


        Private _StatusId As Integer
        Private _StatusName As String
        Private _StatusNameArabic As String
        Private objDALRequestStatus As DALRequestStatus

#End Region

#Region "Public Properties"


        Public Property StatusId() As Integer
            Set(ByVal value As Integer)
                _StatusId = value
            End Set
            Get
                Return (_StatusId)
            End Get
        End Property


        Public Property StatusName() As String
            Set(ByVal value As String)
                _StatusName = value
            End Set
            Get
                Return (_StatusName)
            End Get
        End Property


        Public Property StatusNameArabic() As String
            Set(ByVal value As String)
                _StatusNameArabic = value
            End Set
            Get
                Return (_StatusNameArabic)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALRequestStatus = New DALRequestStatus()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALRequestStatus.Add(_StatusId, _StatusName, _StatusNameArabic)
        End Function

        Public Function Update() As Integer

            Return objDALRequestStatus.Update(_StatusId, _StatusName, _StatusNameArabic)

        End Function



        Public Function Delete() As Integer

            Return objDALRequestStatus.Delete(_StatusId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALRequestStatus.GetAll()

        End Function

        Public Function GetByPK() As RequestStatus

            Dim dr As DataRow
            dr = objDALRequestStatus.GetByPK(_StatusId)

            If Not IsDBNull(dr("StatusId")) Then
                _StatusId = dr("StatusId")
            End If
            If Not IsDBNull(dr("StatusName")) Then
                _StatusName = dr("StatusName")
            End If
            If Not IsDBNull(dr("StatusNameArabic")) Then
                _StatusNameArabic = dr("StatusNameArabic")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace