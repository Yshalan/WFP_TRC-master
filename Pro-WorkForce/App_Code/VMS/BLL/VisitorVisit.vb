Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace VMS

    Public Class VisitorVisit

#Region "Class Variables"

        Private _FK_VisitId As Integer
        Private _FK_VisitorId As Integer
        Private objDALVisitorVisit As DALVisitorVisit

#End Region

#Region "Public Properties"
        Public Property FK_VisitId() As Integer
            Set(ByVal value As Integer)
                _FK_VisitId = value
            End Set
            Get
                Return (_FK_VisitId)
            End Get
        End Property
        Public Property FK_VisitorId() As Integer
            Set(ByVal value As Integer)
                _FK_VisitorId = value
            End Set
            Get
                Return (_FK_VisitorId)
            End Get
        End Property



#End Region


#Region "Constructor"

        Public Sub New()

            objDALVisitorVisit = New DALVisitorVisit()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALVisitorVisit.Add(_FK_VisitId, _FK_VisitorId)
        End Function


        Public Function Delete() As Integer

            Return objDALVisitorVisit.Delete(_FK_VisitId, _FK_VisitorId)

        End Function


        Public Function Get_ByFK_VisitId() As DataTable

            Return objDALVisitorVisit.Get_ByFK_VisitId(_FK_VisitId)

        End Function


        Public Function DeleteByFK_VisitId() As Integer

            Return objDALVisitorVisit.DeleteByFK_VisitId(_FK_VisitId)

        End Function



#End Region

    End Class
End Namespace