Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Forms

    Public Class Events_Groups

#Region "Class Variables"


        Private _FK_EventId As Integer
        Private _FK_GroupId As Integer
        Private _NumberOfEmployees As Integer
        Private objDALEvents_Groups As DALEvents_Groups

#End Region

#Region "Public Properties"


        Public Property FK_EventId() As Integer
            Set(ByVal value As Integer)
                _FK_EventId = value
            End Set
            Get
                Return (_FK_EventId)
            End Get
        End Property


        Public Property FK_GroupId() As Integer
            Set(ByVal value As Integer)
                _FK_GroupId = value
            End Set
            Get
                Return (_FK_GroupId)
            End Get
        End Property


        Public Property NumberOfEmployees() As Integer
            Set(ByVal value As Integer)
                _NumberOfEmployees = value
            End Set
            Get
                Return (_NumberOfEmployees)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALEvents_Groups = New DALEvents_Groups()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALEvents_Groups.Add(_FK_EventId, _FK_GroupId, _NumberOfEmployees)
        End Function

        Public Function Update() As Integer

            Return objDALEvents_Groups.Update(_FK_EventId, _FK_GroupId, _NumberOfEmployees)

        End Function



        Public Function Delete() As Integer

            Return objDALEvents_Groups.Delete(_FK_EventId, _FK_GroupId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALEvents_Groups.GetAll()

        End Function
        Public Function GetAll_Details() As DataTable

            Return objDALEvents_Groups.GetAll_Details(_FK_EventId) ', _FK_GroupId

        End Function
       
        Public Function GetByPK() As Events_Groups

            Dim dr As DataRow
            dr = objDALEvents_Groups.GetByPK(_FK_EventId, _FK_GroupId)

            If Not IsDBNull(dr("FK_EventId")) Then
                _FK_EventId = dr("FK_EventId")
            End If
            If Not IsDBNull(dr("FK_GroupId")) Then
                _FK_GroupId = dr("FK_GroupId")
            End If
            If Not IsDBNull(dr("NumberOfEmployees")) Then
                _NumberOfEmployees = dr("NumberOfEmployees")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace