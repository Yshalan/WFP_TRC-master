Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Definitions
Imports TA.Events

Namespace TA.Definitions

    Public Class Emp_Religion

#Region "Class Variables"


        Private _ReligionId As Integer
        Private _ReligionCode As String
        Private _ReligionName As String
        Private _ReligionArabicName As String
        Private _Active As Boolean
        Private objDALEmp_Religion As DALEmp_Religion
#End Region

#Region "Public Properties"


        Public Property ReligionId() As Integer
            Set(ByVal value As Integer)
                _ReligionId = value
            End Set
            Get
                Return (_ReligionId)
            End Get
        End Property


        Public Property ReligionCode() As String
            Set(ByVal value As String)
                _ReligionCode = value
            End Set
            Get
                Return (_ReligionCode)
            End Get
        End Property


        Public Property ReligionName() As String
            Set(ByVal value As String)
                _ReligionName = value
            End Set
            Get
                Return (_ReligionName)
            End Get
        End Property


        Public Property ReligionArabicName() As String
            Set(ByVal value As String)
                _ReligionArabicName = value
            End Set
            Get
                Return (_ReligionArabicName)
            End Get
        End Property


        Public Property Active() As Boolean
            Set(ByVal value As Boolean)
                _Active = value
            End Set
            Get
                Return (_Active)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALEmp_Religion = New DALEmp_Religion()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALEmp_Religion.Add(_ReligionId, _ReligionCode, _ReligionName, _ReligionArabicName, _Active)
            App_EventsLog.Insert_ToEventLog("Add", _ReligionId, "Emp_Religion", "Define Religions")
            Return rslt

        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALEmp_Religion.Update(_ReligionId, _ReligionCode, _ReligionName, _ReligionArabicName, _Active)
            App_EventsLog.Insert_ToEventLog("Edit", _ReligionId, "Emp_Religion", "Define Religions")
            Return rslt

        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALEmp_Religion.Delete(_ReligionId)
            App_EventsLog.Insert_ToEventLog("Delete", _ReligionId, "Emp_Religion", "Define Religions")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_Religion.GetAll()

        End Function

        Public Function GetByPK() As Emp_Religion

            Dim dr As DataRow
            dr = objDALEmp_Religion.GetByPK(_ReligionId)

            If dr Is Nothing Then Return Nothing

            If Not IsDBNull(dr("ReligionId")) Then
                _ReligionId = dr("ReligionId")
            End If
            If Not IsDBNull(dr("ReligionCode")) Then
                _ReligionCode = dr("ReligionCode")
            End If
            If Not IsDBNull(dr("ReligionName")) Then
                _ReligionName = dr("ReligionName")
            End If
            If Not IsDBNull(dr("ReligionArabicName")) Then
                _ReligionArabicName = dr("ReligionArabicName")
            End If
            If Not IsDBNull(dr("Active")) Then
                _Active = dr("Active")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace