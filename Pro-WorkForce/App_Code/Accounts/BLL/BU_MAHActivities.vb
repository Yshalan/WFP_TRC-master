Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Imports TA.Accounts

Namespace TA.Accounts

    Public Class BU_MAHActivities

#Region "Class Variables"


        Private _FK_AccountId As Integer
        Private _FK_ActivityId As Integer
        Private objDALBU_MAHActivities As DALBU_MAHActivities

#End Region

#Region "Public Properties"


        Public Property FK_AccountId() As Integer
            Set(ByVal value As Integer)
                _FK_AccountId = value
            End Set
            Get
                Return (_FK_AccountId)
            End Get
        End Property


        Public Property FK_ActivityId() As Integer
            Set(ByVal value As Integer)
                _FK_ActivityId = value
            End Set
            Get
                Return (_FK_ActivityId)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALBU_MAHActivities = New DALBU_MAHActivities()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALBU_MAHActivities.Add(_FK_AccountId, _FK_ActivityId)
        End Function

        Public Function Update() As Integer

            Return objDALBU_MAHActivities.Update(_FK_AccountId, _FK_ActivityId)

        End Function



        Public Function Delete() As Integer

            Return objDALBU_MAHActivities.Delete(_FK_AccountId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALBU_MAHActivities.GetAll()

        End Function

        Public Function GetByPK() As DataTable
            Return objDALBU_MAHActivities.GetByPK(_FK_AccountId)
        End Function

#End Region

    End Class
End Namespace