Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Accounts

    Public Class BU_MAH_Agent

#Region "Class Variables"


        Private _FK_MAHId As Integer
        Private _FK_AgentId As Integer
        Private objDALBU_MAH_Agent As DALBU_MAH_Agent

#End Region

#Region "Public Properties"


        Public Property FK_MAHId() As Integer
            Set(ByVal value As Integer)
                _FK_MAHId = value
            End Set
            Get
                Return (_FK_MAHId)
            End Get
        End Property


        Public Property FK_AgentId() As Integer
            Set(ByVal value As Integer)
                _FK_AgentId = value
            End Set
            Get
                Return (_FK_AgentId)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALBU_MAH_Agent = New DALBU_MAH_Agent()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALBU_MAH_Agent.Add(_FK_MAHId, _FK_AgentId)
        End Function

        Public Function Update() As Integer

            Return objDALBU_MAH_Agent.Update(_FK_MAHId, _FK_AgentId)

        End Function



        Public Function Delete() As Integer

            Return objDALBU_MAH_Agent.Delete(_FK_MAHId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALBU_MAH_Agent.GetAll()

        End Function

        Public Function GetAllByMAHID() As DataTable

            Return objDALBU_MAH_Agent.GetAllByMAHID(_FK_MAHId)

        End Function

        Public Function GetByPK() As BU_MAH_Agent

            Dim dr As DataRow
            dr = objDALBU_MAH_Agent.GetByPK(_FK_MAHId, _FK_AgentId)

            If Not IsDBNull(dr("FK_MAHId")) Then
                _FK_MAHId = dr("FK_MAHId")
            End If
            If Not IsDBNull(dr("FK_AgentId")) Then
                _FK_AgentId = dr("FK_AgentId")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace