Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Accounts

    Public Class BU_MAHProductCategories

#Region "Class Variables"


        Private _FK_AccountId As Integer
        Private _FK_ProductCategoryId As Integer
        Private objDALBU_MAHProductCategories As DALBU_MAHProductCategories

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


        Public Property FK_ProductCategoryId() As Integer
            Set(ByVal value As Integer)
                _FK_ProductCategoryId = value
            End Set
            Get
                Return (_FK_ProductCategoryId)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALBU_MAHProductCategories = New DALBU_MAHProductCategories()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALBU_MAHProductCategories.Add(_FK_AccountId, _FK_ProductCategoryId)
        End Function

        Public Function Update() As Integer

            Return objDALBU_MAHProductCategories.Update(_FK_AccountId, _FK_ProductCategoryId)

        End Function



        Public Function Delete() As Integer

            Return objDALBU_MAHProductCategories.Delete(_FK_AccountId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALBU_MAHProductCategories.GetAll()

        End Function

        Public Function GetByPK() As DataTable
            Return objDALBU_MAHProductCategories.GetByPK(_FK_AccountId)
        End Function

#End Region

    End Class
End Namespace