Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

namespace TA.OverTime

Public Class OverTime_Decision

#Region "Class Variables"



        Private _DecisionID As Integer
        Private _Desc_En As String
        Private _Desc_Ar As String
        Private objDALOverTime_Decision As DALOverTime_Decision

#End Region

#Region "Public Properties"



        Public Property DecisionID() As Integer
            Set(ByVal value As Integer)
                _DecisionID = value
            End Set
            Get
                Return (_DecisionID)
            End Get
        End Property


        Public Property Desc_En() As String
            Set(ByVal value As String)
                _Desc_En = value
            End Set
            Get
                Return (_Desc_En)
            End Get
        End Property


        Public Property Desc_Ar() As String
            Set(ByVal value As String)
                _Desc_Ar = value
            End Set
            Get
                Return (_Desc_Ar)
            End Get
        End Property

#End Region


#Region "Constructor"


        Public Sub New()

            objDALOverTime_Decision = New DALOverTime_Decision()

        End Sub

#End Region

#Region "Methods"


        Public Function Add() As Integer

            Return objDALOverTime_Decision.Add(_Desc_En, _Desc_Ar)
        End Function

        Public Function Update() As Integer

            Return objDALOverTime_Decision.Update(_DecisionID, _Desc_En, _Desc_Ar)

        End Function



        Public Function Delete() As Integer

            Return objDALOverTime_Decision.Delete(_DecisionID)

        End Function

        Public Function GetAll() As DataTable

            Return objDALOverTime_Decision.GetAll()

        End Function
        Public Function GetAllWithFilter(ByVal Filter As String) As DataTable

            Return objDALOverTime_Decision.GetAllWithFilter(Filter)

        End Function
        Public Function GetByPK() As OverTime_Decision

            Dim dr As DataRow
            dr = objDALOverTime_Decision.GetByPK(_DecisionID)

            If Not IsDBNull(dr("DecisionID")) Then
                _DecisionID = dr("DecisionID")
            End If
            If Not IsDBNull(dr("Desc_En")) Then
                _Desc_En = dr("Desc_En")
            End If
            If Not IsDBNull(dr("Desc_Ar")) Then
                _Desc_Ar = dr("Desc_Ar")
            End If
            Return Me
        End Function

#End Region

 End Class
End namespace