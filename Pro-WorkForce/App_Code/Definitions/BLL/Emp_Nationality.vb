Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Definitions

    Public Class Emp_Nationality

#Region "Class Variables"


        Private _NationalityId As Integer
        Private _NationalityCode As String
        Private _NationalityName As String
        Private _NationalityArabicName As String
        Private objDALEmp_Nationality As DALEmp_Nationality

#End Region

#Region "Public Properties"


        Public Property NationalityId() As Integer
            Set(ByVal value As Integer)
                _NationalityId = value
            End Set
            Get
                Return (_NationalityId)
            End Get
        End Property


        Public Property NationalityCode() As String
            Set(ByVal value As String)
                _NationalityCode = value
            End Set
            Get
                Return (_NationalityCode)
            End Get
        End Property


        Public Property NationalityName() As String
            Set(ByVal value As String)
                _NationalityName = value
            End Set
            Get
                Return (_NationalityName)
            End Get
        End Property


        Public Property NationalityArabicName() As String
            Set(ByVal value As String)
                _NationalityArabicName = value
            End Set
            Get
                Return (_NationalityArabicName)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALEmp_Nationality = New DALEmp_Nationality()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer
            Dim rslt As Integer = objDALEmp_Nationality.Add(_NationalityId, _NationalityCode, _NationalityName, _NationalityArabicName)
            App_EventsLog.Insert_ToEventLog("Add", _NationalityId, "Emp_Nationality", "Define Nationalities")
            Return rslt
        End Function

        Public Function Update() As Integer
            Dim rslt As Integer = objDALEmp_Nationality.Update(_NationalityId, _NationalityCode, _NationalityName, _NationalityArabicName)
            App_EventsLog.Insert_ToEventLog("Edit", _NationalityId, "Emp_Nationality", "Define Nationalities")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALEmp_Nationality.Delete(_NationalityId)
            App_EventsLog.Insert_ToEventLog("Delete", _NationalityId, "Emp_Nationality", "Define Nationalities")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_Nationality.GetAll()

        End Function

        Public Function GetByPK() As Emp_Nationality

            Dim dr As DataRow
            dr = objDALEmp_Nationality.GetByPK(_NationalityId)

            If dr Is Nothing Then Return Nothing

            If Not IsDBNull(dr("NationalityId")) Then
                _NationalityId = dr("NationalityId")
            End If
            If Not IsDBNull(dr("NationalityCode")) Then
                _NationalityCode = dr("NationalityCode")
            End If
            If Not IsDBNull(dr("NationalityName")) Then
                _NationalityName = dr("NationalityName")
            End If
            If Not IsDBNull(dr("NationalityArabicName")) Then
                _NationalityArabicName = dr("NationalityArabicName")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace