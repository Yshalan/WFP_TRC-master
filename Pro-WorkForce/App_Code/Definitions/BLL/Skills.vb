Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Definitions

    Public Class Skills

#Region "Class Variables"


        Private _SkillId As Long
        Private _SkillName As String
        Private _SkillArabicName As String
        Private _FK_CategoryId As Long
        Private _Desc_En As String
        Private _Desc_Ar As String
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALSkills As DALSkills

#End Region

#Region "Public Properties"


        Public Property SkillId() As Long
            Set(ByVal value As Long)
                _SkillId = value
            End Set
            Get
                Return (_SkillId)
            End Get
        End Property


        Public Property SkillName() As String
            Set(ByVal value As String)
                _SkillName = value
            End Set
            Get
                Return (_SkillName)
            End Get
        End Property


        Public Property SkillArabicName() As String
            Set(ByVal value As String)
                _SkillArabicName = value
            End Set
            Get
                Return (_SkillArabicName)
            End Get
        End Property


        Public Property FK_CategoryId() As Long
            Set(ByVal value As Long)
                _FK_CategoryId = value
            End Set
            Get
                Return (_FK_CategoryId)
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

            objDALSkills = New DALSkills()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALSkills.Add(_SkillId, _SkillName, _SkillArabicName, _FK_CategoryId, _Desc_En, _Desc_Ar, _CREATED_BY)
            App_EventsLog.Insert_ToEventLog("Add", _SkillId, "Skills", "Skill Category")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALSkills.Update(_SkillId, _SkillName, _SkillArabicName, _FK_CategoryId, _Desc_En, _Desc_Ar, _LAST_UPDATE_BY)
            App_EventsLog.Insert_ToEventLog("Update", _SkillId, "Skills", "Skill Category")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALSkills.Delete(_SkillId)
            App_EventsLog.Insert_ToEventLog("Delete", _SkillId, "Skills", "Skill Category")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALSkills.GetAll()

        End Function

        Public Function GetAll_ByFK_CategoryId() As DataTable

            Return objDALSkills.GetAll_ByFK_CategoryId(_FK_CategoryId)

        End Function

        Public Function GetByPK() As Skills

            Dim dr As DataRow
            dr = objDALSkills.GetByPK(_SkillId)

            If Not IsDBNull(dr("SkillId")) Then
                _SkillId = dr("SkillId")
            End If
            If Not IsDBNull(dr("SkillName")) Then
                _SkillName = dr("SkillName")
            End If
            If Not IsDBNull(dr("SkillArabicName")) Then
                _SkillArabicName = dr("SkillArabicName")
            End If
            If Not IsDBNull(dr("FK_CategoryId")) Then
                _FK_CategoryId = dr("FK_CategoryId")
            End If
            If Not IsDBNull(dr("Desc_En")) Then
                _Desc_En = dr("Desc_En")
            End If
            If Not IsDBNull(dr("Desc_Ar")) Then
                _Desc_Ar = dr("Desc_Ar")
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

        Public Function GetByNameEn() As Skills

            Dim dr As DataRow
            dr = objDALSkills.GetByNameEn(_SkillName)
            If Not dr Is Nothing Then


                If Not IsDBNull(dr("SkillId")) Then
                    _SkillId = dr("SkillId")
                End If
                If Not IsDBNull(dr("SkillName")) Then
                    _SkillName = dr("SkillName")
                End If
                If Not IsDBNull(dr("SkillArabicName")) Then
                    _SkillArabicName = dr("SkillArabicName")
                End If
                If Not IsDBNull(dr("FK_CategoryId")) Then
                    _FK_CategoryId = dr("FK_CategoryId")
                End If
                If Not IsDBNull(dr("Desc_En")) Then
                    _Desc_En = dr("Desc_En")
                End If
                If Not IsDBNull(dr("Desc_Ar")) Then
                    _Desc_Ar = dr("Desc_Ar")
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
            End If
        End Function

        Public Function GetByNameAr() As Skills

            Dim dr As DataRow
            dr = objDALSkills.GetByNameAr(_SkillArabicName)
            If Not dr Is Nothing Then


                If Not IsDBNull(dr("SkillId")) Then
                    _SkillId = dr("SkillId")
                End If
                If Not IsDBNull(dr("SkillName")) Then
                    _SkillName = dr("SkillName")
                End If
                If Not IsDBNull(dr("SkillArabicName")) Then
                    _SkillArabicName = dr("SkillArabicName")
                End If
                If Not IsDBNull(dr("FK_CategoryId")) Then
                    _FK_CategoryId = dr("FK_CategoryId")
                End If
                If Not IsDBNull(dr("Desc_En")) Then
                    _Desc_En = dr("Desc_En")
                End If
                If Not IsDBNull(dr("Desc_Ar")) Then
                    _Desc_Ar = dr("Desc_Ar")
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
            End If
        End Function

#End Region

    End Class
End Namespace