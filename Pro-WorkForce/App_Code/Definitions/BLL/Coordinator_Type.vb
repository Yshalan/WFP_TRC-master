Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Definitions

    Public Class Coordinator_Type

#Region "Class Variables"


        Private _CoordinatorTypeId As Integer
        Private _CoordinatorShortName As String
        Private _CoordinatorTypeName As String
        Private _CoordinatorTypeArabicName As String
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALCoordinator_Type As DALCoordinator_Type

#End Region

#Region "Public Properties"


        Public Property CoordinatorTypeId() As Integer
            Set(ByVal value As Integer)
                _CoordinatorTypeId = value
            End Set
            Get
                Return (_CoordinatorTypeId)
            End Get
        End Property


        Public Property CoordinatorShortName() As String
            Set(ByVal value As String)
                _CoordinatorShortName = value
            End Set
            Get
                Return (_CoordinatorShortName)
            End Get
        End Property


        Public Property CoordinatorTypeName() As String
            Set(ByVal value As String)
                _CoordinatorTypeName = value
            End Set
            Get
                Return (_CoordinatorTypeName)
            End Get
        End Property


        Public Property CoordinatorTypeArabicName() As String
            Set(ByVal value As String)
                _CoordinatorTypeArabicName = value
            End Set
            Get
                Return (_CoordinatorTypeArabicName)
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

            objDALCoordinator_Type = New DALCoordinator_Type()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALCoordinator_Type.Add(_CoordinatorTypeId, _CoordinatorShortName, _CoordinatorTypeName, _CoordinatorTypeArabicName, _CREATED_BY)
            App_EventsLog.Insert_ToEventLog("Add", _CoordinatorTypeId, "Coordinator_Type", "Coordinator Types")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALCoordinator_Type.Update(_CoordinatorTypeId, _CoordinatorShortName, _CoordinatorTypeName, _CoordinatorTypeArabicName, _LAST_UPDATE_BY)
            App_EventsLog.Insert_ToEventLog("Update", _CoordinatorTypeId, "Coordinator_Type", "Coordinator Types")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALCoordinator_Type.Delete(_CoordinatorTypeId)
            App_EventsLog.Insert_ToEventLog("Delete", _CoordinatorTypeId, "Coordinator_Type", "Coordinator Types")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALCoordinator_Type.GetAll()

        End Function

        Public Function GetByPK() As Coordinator_Type

            Dim dr As DataRow
            dr = objDALCoordinator_Type.GetByPK(_CoordinatorTypeId)

            If Not IsDBNull(dr("CoordinatorTypeId")) Then
                _CoordinatorTypeId = dr("CoordinatorTypeId")
            End If
            If Not IsDBNull(dr("CoordinatorShortName")) Then
                _CoordinatorShortName = dr("CoordinatorShortName")
            End If
            If Not IsDBNull(dr("CoordinatorTypeName")) Then
                _CoordinatorTypeName = dr("CoordinatorTypeName")
            End If
            If Not IsDBNull(dr("CoordinatorTypeArabicName")) Then
                _CoordinatorTypeArabicName = dr("CoordinatorTypeArabicName")
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