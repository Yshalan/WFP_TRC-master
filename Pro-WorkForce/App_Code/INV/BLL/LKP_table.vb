Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace LKP

    Public Class LKP_table

#Region "Class Variables"


        Private _lkpId As Integer
        Private _FK_lkpType As Integer
        Private _lkpCode As String
        Private _lkpName As String
        Private _lkpNameAr As String
        Private _Remarks As String
        Private _RemarksAR As String
        Private _Other1 As String
        Private _Other2 As String
        Private _Other3 As String
        Private _Other4 As String
        Private _Other5 As String
        Private _lkpTypeId As Integer
        Private objDALLKP_table As DALLKP_table

#End Region

#Region "Public Properties"


        Public Property lkpId() As Integer
            Set(ByVal value As Integer)
                _lkpId = value
            End Set
            Get
                Return (_lkpId)
            End Get
        End Property


        Public Property FK_lkpType() As Integer
            Set(ByVal value As Integer)
                _FK_lkpType = value
            End Set
            Get
                Return (_FK_lkpType)
            End Get
        End Property


        Public Property lkpCode() As String
            Set(ByVal value As String)
                _lkpCode = value
            End Set
            Get
                Return (_lkpCode)
            End Get
        End Property


        Public Property lkpName() As String
            Set(ByVal value As String)
                _lkpName = value
            End Set
            Get
                Return (_lkpName)
            End Get
        End Property


        Public Property lkpNameAr() As String
            Set(ByVal value As String)
                _lkpNameAr = value
            End Set
            Get
                Return (_lkpNameAr)
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


        Public Property RemarksAR() As String
            Set(ByVal value As String)
                _RemarksAR = value
            End Set
            Get
                Return (_RemarksAR)
            End Get
        End Property


        Public Property Other1() As String
            Set(ByVal value As String)
                _Other1 = value
            End Set
            Get
                Return (_Other1)
            End Get
        End Property


        Public Property Other2() As String
            Set(ByVal value As String)
                _Other2 = value
            End Set
            Get
                Return (_Other2)
            End Get
        End Property


        Public Property Other3() As String
            Set(ByVal value As String)
                _Other3 = value
            End Set
            Get
                Return (_Other3)
            End Get
        End Property


        Public Property Other4() As String
            Set(ByVal value As String)
                _Other4 = value
            End Set
            Get
                Return (_Other4)
            End Get
        End Property


        Public Property Other5() As String
            Set(ByVal value As String)
                _Other5 = value
            End Set
            Get
                Return (_Other5)
            End Get
        End Property

        Public Property lkpTypeId() As Integer
            Get
                Return _lkpTypeId
            End Get
            Set(value As Integer)
                _lkpTypeId = value
            End Set
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALLKP_table = New DALLKP_table()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALLKP_table.Add(_FK_lkpType, _lkpCode, _lkpName, _lkpNameAr, _Remarks, _RemarksAR, _Other1, _Other2, _Other3, _Other4, _Other5)
        End Function

        Public Function Update() As Integer

            Return objDALLKP_table.Update(_lkpId, _FK_lkpType, _lkpCode, _lkpName, _lkpNameAr, _Remarks, _RemarksAR, _Other1, _Other2, _Other3, _Other4, _Other5)

        End Function



        Public Function Delete() As Integer

            Return objDALLKP_table.Delete(_lkpId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALLKP_table.GetAll()

        End Function

        Public Function GetByPK() As LKP_table

            Dim dr As DataRow
            dr = objDALLKP_table.GetByPK(_lkpId)

            If Not IsDBNull(dr("lkpId")) Then
                _lkpId = dr("lkpId")
            End If
            If Not IsDBNull(dr("FK_lkpType")) Then
                _FK_lkpType = dr("FK_lkpType")
            End If
            If Not IsDBNull(dr("lkpCode")) Then
                _lkpCode = dr("lkpCode")
            End If
            If Not IsDBNull(dr("lkpName")) Then
                _lkpName = dr("lkpName")
            End If
            If Not IsDBNull(dr("lkpNameAr")) Then
                _lkpNameAr = dr("lkpNameAr")
            End If
            If Not IsDBNull(dr("Remarks")) Then
                _Remarks = dr("Remarks")
            End If
            If Not IsDBNull(dr("RemarksAR")) Then
                _RemarksAR = dr("RemarksAR")
            End If
            If Not IsDBNull(dr("Other1")) Then
                _Other1 = dr("Other1")
            End If
            If Not IsDBNull(dr("Other2")) Then
                _Other2 = dr("Other2")
            End If
            If Not IsDBNull(dr("Other3")) Then
                _Other3 = dr("Other3")
            End If
            If Not IsDBNull(dr("Other4")) Then
                _Other4 = dr("Other4")
            End If
            If Not IsDBNull(dr("Other5")) Then
                _Other5 = dr("Other5")
            End If
            Return Me
        End Function

        Public Function GetByFK(ByVal id As Integer) As DataTable
            FK_lkpType = id
            Return objDALLKP_table.GetByKF(_FK_lkpType)
        End Function

        Public Function CheckHasValue(ByVal id As Integer) As DataRow
            lkpTypeId = id
            Dim dr As DataRow
            dr = objDALLKP_table.CheckHasValue(lkpTypeId)
            Return dr
        End Function

#End Region

    End Class
End Namespace