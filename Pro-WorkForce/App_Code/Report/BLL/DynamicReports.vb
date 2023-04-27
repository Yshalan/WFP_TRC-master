Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Reports

    Public Class DynamicReports

#Region "Class Variables"


        Private _ReportId As Integer
        Private _ReportName As String
        Private _ViewName As String
        Private _SQLQuery As String
        Private objDALDynamicReports As DALDynamicReports

#End Region

#Region "Public Properties"


        Public Property ReportId() As Integer
            Set(ByVal value As Integer)
                _ReportId = value
            End Set
            Get
                Return (_ReportId)
            End Get
        End Property


        Public Property ReportName() As String
            Set(ByVal value As String)
                _ReportName = value
            End Set
            Get
                Return (_ReportName)
            End Get
        End Property


        Public Property ViewName() As String
            Set(ByVal value As String)
                _ViewName = value
            End Set
            Get
                Return (_ViewName)
            End Get
        End Property

        Public Property SQLQuery() As String
            Set(ByVal value As String)
                _SQLQuery = value
            End Set
            Get
                Return (_SQLQuery)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALDynamicReports = New DALDynamicReports()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALDynamicReports.Add(_ReportName, _ViewName)
        End Function

        Public Function Update() As Integer

            Return objDALDynamicReports.Update(_ReportId, _ReportName, _ViewName)

        End Function



        Public Function Delete() As Integer

            Return objDALDynamicReports.Delete(_ReportId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALDynamicReports.GetAll()

        End Function

        Public Function GetViewDefinition() As DataTable

            Return objDALDynamicReports.GetViewDefinition(_ViewName)

        End Function
        Public Function ExecSQLQuery() As DataTable

            Return objDALDynamicReports.ExecSQLQuery(_SQLQuery)

        End Function
        Public Function GetByPK() As DynamicReports

            Dim dr As DataRow
            dr = objDALDynamicReports.GetByPK(_ReportId)

            If Not IsDBNull(dr("ReportId")) Then
                _ReportId = dr("ReportId")
            End If
            If Not IsDBNull(dr("ReportName")) Then
                _ReportName = dr("ReportName")
            End If
            If Not IsDBNull(dr("ViewName")) Then
                _ViewName = dr("ViewName")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace