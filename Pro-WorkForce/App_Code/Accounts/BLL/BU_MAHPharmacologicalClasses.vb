Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Accounts

    Public Class BU_MAHPharmacologicalClasses

#Region "Class Variables"


        Private _PHClassId As Integer
        Private _FK_AccountId As Integer
        Private _ClassName As String
        Private _Remarks As String
        Private objDALBU_MAHPharmacologicalClasses As DALBU_MAHPharmacologicalClasses

#End Region

#Region "Public Properties"


        Public Property PHClassId() As Integer
            Set(ByVal value As Integer)
                _PHClassId = value
            End Set
            Get
                Return (_PHClassId)
            End Get
        End Property


        Public Property FK_AccountId() As Integer
            Set(ByVal value As Integer)
                _FK_AccountId = value
            End Set
            Get
                Return (_FK_AccountId)
            End Get
        End Property


        Public Property ClassName() As String
            Set(ByVal value As String)
                _ClassName = value
            End Set
            Get
                Return (_ClassName)
            End Get
        End Property


        Public Property Remarks() As String
            Set(ByVal value As String)
                _Remarks = value
            End Set
            Get
                Return (_Remarks)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALBU_MAHPharmacologicalClasses = New DALBU_MAHPharmacologicalClasses()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALBU_MAHPharmacologicalClasses.Add(_FK_AccountId, _ClassName, _Remarks)
        End Function

        Public Function Update() As Integer

            Return objDALBU_MAHPharmacologicalClasses.Update(_PHClassId, _FK_AccountId, _ClassName, _Remarks)

        End Function



        Public Function Delete() As Integer

            Return objDALBU_MAHPharmacologicalClasses.Delete(_PHClassId)

        End Function
        Public Function DeleteByAccountID() As Integer

            Return objDALBU_MAHPharmacologicalClasses.DeleteByAccountID(_FK_AccountId)

        End Function

        Public Function GetAll() As DataTable
            Return objDALBU_MAHPharmacologicalClasses.GetAll(_FK_AccountId)
        End Function

        Public Function GetByPK() As BU_MAHPharmacologicalClasses

            Dim dr As DataRow
            dr = objDALBU_MAHPharmacologicalClasses.GetByPK(_PHClassId)

            If Not IsDBNull(dr("PHClassId")) Then
                _PHClassId = dr("PHClassId")
            End If
            If Not IsDBNull(dr("FK_AccountId")) Then
                _FK_AccountId = dr("FK_AccountId")
            End If
            If Not IsDBNull(dr("ClassName")) Then
                _ClassName = dr("ClassName")
            End If
            If Not IsDBNull(dr("Remarks")) Then
                _Remarks = dr("Remarks")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace