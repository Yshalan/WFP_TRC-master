Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Definitions

    Public Class Emp_University

#Region "Class Variables"


        Private _UniversityId As Integer
        Private _UniversityShortName As String
        Private _UniversityName As String
        Private _UniversityArabicName As String
        Private _Address As String
        Private _PhoneNo As String
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALEmp_University As DALEmp_University

#End Region

#Region "Public Properties"

        Public Property UniversityId() As Integer
            Set(ByVal value As Integer)
                _UniversityId = value
            End Set
            Get
                Return (_UniversityId)
            End Get
        End Property

        Public Property UniversityShortName() As String
            Set(ByVal value As String)
                _UniversityShortName = value
            End Set
            Get
                Return (_UniversityShortName)
            End Get
        End Property

        Public Property UniversityName() As String
            Set(ByVal value As String)
                _UniversityName = value
            End Set
            Get
                Return (_UniversityName)
            End Get
        End Property

        Public Property UniversityArabicName() As String
            Set(ByVal value As String)
                _UniversityArabicName = value
            End Set
            Get
                Return (_UniversityArabicName)
            End Get
        End Property

        Public Property Address() As String
            Set(ByVal value As String)
                _Address = value
            End Set
            Get
                Return (_Address)
            End Get
        End Property

        Public Property PhoneNo() As String
            Set(ByVal value As String)
                _PhoneNo = value
            End Set
            Get
                Return (_PhoneNo)
            End Get
        End Property

        Public Property CREATED_BY() As String
            Set(ByVal value As String)
                _CREATED_BY = value
            End Set
            Get
                Return (_CREATED_BY)
            End Get
        End Property

        Public Property CREATED_DATE() As DateTime
            Set(ByVal value As DateTime)
                _CREATED_DATE = value
            End Set
            Get
                Return (_CREATED_DATE)
            End Get
        End Property

        Public Property LAST_UPDATE_BY() As String
            Set(ByVal value As String)
                _LAST_UPDATE_BY = value
            End Set
            Get
                Return (_LAST_UPDATE_BY)
            End Get
        End Property

        Public Property LAST_UPDATE_DATE() As DateTime
            Set(ByVal value As DateTime)
                _LAST_UPDATE_DATE = value
            End Set
            Get
                Return (_LAST_UPDATE_DATE)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALEmp_University = New DALEmp_University()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt = objDALEmp_University.Add(_UniversityId, _UniversityShortName, _UniversityName, _UniversityArabicName, _Address, _PhoneNo, _CREATED_BY)
            App_EventsLog.Insert_ToEventLog("Add", _UniversityId, "Emp_University", "Define Universities & Colleges")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt = objDALEmp_University.Update(_UniversityId, _UniversityShortName, _UniversityName, _UniversityArabicName, _Address, _PhoneNo, _LAST_UPDATE_BY)
            App_EventsLog.Insert_ToEventLog("Update", _UniversityId, "Emp_University", "Define Universities & Colleges")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt = objDALEmp_University.Delete(_UniversityId)
            App_EventsLog.Insert_ToEventLog("Delete", _UniversityId, "Emp_University", "Define Universities & Colleges")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_University.GetAll()

        End Function

        Public Function GetByPK() As Emp_University

            Dim dr As DataRow
            dr = objDALEmp_University.GetByPK(_UniversityId)

            If Not IsDBNull(dr("UniversityId")) Then
                _UniversityId = dr("UniversityId")
            End If
            If Not IsDBNull(dr("UniversityShortName")) Then
                _UniversityShortName = dr("UniversityShortName")
            End If
            If Not IsDBNull(dr("UniversityName")) Then
                _UniversityName = dr("UniversityName")
            End If
            If Not IsDBNull(dr("UniversityArabicName")) Then
                _UniversityArabicName = dr("UniversityArabicName")
            End If
            If Not IsDBNull(dr("Address")) Then
                _Address = dr("Address")
            End If
            If Not IsDBNull(dr("PhoneNo")) Then
                _PhoneNo = dr("PhoneNo")
            End If
            If Not IsDBNull(dr("CREATED_BY")) Then
                _CREATED_BY = dr("CREATED_BY")
            End If
            If Not IsDBNull(dr("CREATED_DATE")) Then
                _CREATED_DATE = dr("CREATED_DATE")
            End If
            If Not IsDBNull(dr("LAST_UPDATE_BY")) Then
                _LAST_UPDATE_BY = dr("LAST_UPDATE_BY")
            End If
            If Not IsDBNull(dr("LAST_UPDATE_DATE")) Then
                _LAST_UPDATE_DATE = dr("LAST_UPDATE_DATE")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace