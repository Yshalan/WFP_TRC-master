Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Admin

    Public Class EmailManagerReport

#Region "Class Variables"


        Private _ID As Long
        Private _FK_ManagerId As Integer
        Private _ReportType As Integer
        Private _SendDate As DateTime
        Private objDALEmailManagerReport As DALEmailManagerReport

#End Region

#Region "Public Properties"


        Public Property ID() As Long
            Set(ByVal value As Long)
                _ID = value
            End Set
            Get
                Return (_ID)
            End Get
        End Property


        Public Property FK_ManagerId() As Integer
            Set(ByVal value As Integer)
                _FK_ManagerId = value
            End Set
            Get
                Return (_FK_ManagerId)
            End Get
        End Property


        Public Property ReportType() As Integer
            Set(ByVal value As Integer)
                _ReportType = value
            End Set
            Get
                Return (_ReportType)
            End Get
        End Property


        Public Property SendDate() As DateTime
            Set(ByVal value As DateTime)
                _SendDate = value
            End Set
            Get
                Return (_SendDate)
            End Get
        End Property

#End Region

#Region "Constructor"

        Public Sub New()

            objDALEmailManagerReport = New DALEmailManagerReport()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALEmailManagerReport.Add(_FK_ManagerId, _ReportType, _SendDate)
        End Function

        Public Function Update() As Integer

            Return objDALEmailManagerReport.Update(_ID, _FK_ManagerId, _ReportType, _SendDate)

        End Function

        Public Function Delete() As Integer

            Return objDALEmailManagerReport.Delete(_ID)

        End Function

        Public Function GetAll() As DataTable

            Return objDALEmailManagerReport.GetAll()

        End Function

        Public Function GetByPK() As EmailManagerReport

            Dim dr As DataRow
            dr = objDALEmailManagerReport.GetByPK(_ID)

            If Not IsDBNull(dr("ID")) Then
                _ID = dr("ID")
            End If
            If Not IsDBNull(dr("FK_ManagerId")) Then
                _FK_ManagerId = dr("FK_ManagerId")
            End If
            If Not IsDBNull(dr("ReportType")) Then
                _ReportType = dr("ReportType")
            End If
            If Not IsDBNull(dr("SendDate")) Then
                _SendDate = dr("SendDate")
            End If
            Return Me
        End Function

        Public Function GetEmailManagerReportByManagerId() As DataTable

            Return objDALEmailManagerReport.GetEmailManagerReportByManagerId(_FK_ManagerId)

        End Function

#End Region

    End Class
End Namespace