Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Definitions

    Public Class SkillCategory

#Region "Class Variables"


        Private _CategoryId As Long
        Private _CategoryName As String
        Private _CategoryArabicName As String
        Private _DisplayName As String
        Private _DisplayArabicName As String
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private _HasDate As Boolean
        Private objDALSkillCategory As DALSkillCategory

#End Region

#Region "Public Properties"


        Public Property CategoryId() As Long
            Set(ByVal value As Long)
                _CategoryId = value
            End Set
            Get
                Return (_CategoryId)
            End Get
        End Property

        Public Property CategoryName() As String
            Set(ByVal value As String)
                _CategoryName = value
            End Set
            Get
                Return (_CategoryName)
            End Get
        End Property

        Public Property CategoryArabicName() As String
            Set(ByVal value As String)
                _CategoryArabicName = value
            End Set
            Get
                Return (_CategoryArabicName)
            End Get
        End Property

        Public Property DisplayName() As String
            Set(ByVal value As String)
                _DisplayName = value
            End Set
            Get
                Return (_DisplayName)
            End Get
        End Property

        Public Property DisplayArabicName() As String
            Set(ByVal value As String)
                _DisplayArabicName = value
            End Set
            Get
                Return (_DisplayArabicName)
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

        Public Property HasDate() As Boolean
            Set(ByVal value As Boolean)
                _HasDate = value
            End Set
            Get
                Return (_HasDate)
            End Get
        End Property

#End Region

#Region "Constructor"

        Public Sub New()

            objDALSkillCategory = New DALSkillCategory()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALSkillCategory.Add(_CategoryId, _CategoryName, _CategoryArabicName, _DisplayName, _DisplayArabicName, _CREATED_BY, _HasDate)
            App_EventsLog.Insert_ToEventLog("Add", _CategoryId, "SkillCategory", "Skill Category")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALSkillCategory.Update(_CategoryId, _CategoryName, _CategoryArabicName, _DisplayName, _DisplayArabicName, _LAST_UPDATE_BY, _HasDate)
            App_EventsLog.Insert_ToEventLog("Update", _CategoryId, "SkillCategory", "Skill Category")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALSkillCategory.Delete(_CategoryId)
            App_EventsLog.Insert_ToEventLog("Delete", _CategoryId, "SkillCategory", "Skill Category")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALSkillCategory.GetAll()

        End Function

        Public Function GetByPK() As SkillCategory

            Dim dr As DataRow
            dr = objDALSkillCategory.GetByPK(_CategoryId)

            If Not IsDBNull(dr("CategoryId")) Then
                _CategoryId = dr("CategoryId")
            End If
            If Not IsDBNull(dr("CategoryName")) Then
                _CategoryName = dr("CategoryName")
            End If
            If Not IsDBNull(dr("CategoryArabicName")) Then
                _CategoryArabicName = dr("CategoryArabicName")
            End If
            If Not IsDBNull(dr("DisplayName")) Then
                _DisplayName = dr("DisplayName")
            End If
            If Not IsDBNull(dr("DisplayArabicName")) Then
                _DisplayArabicName = dr("DisplayArabicName")
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
            If Not IsDBNull(dr("HasDate")) Then
                _HasDate = dr("HasDate")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace