Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.FeedBack

    Public Class Feedback_Survey

#Region "Class Variables"


        Private _SurveyId As Integer
        Private _SurveyName As String
        Private _SurveyArabicName As String
        Private _SurveyLanguage As Integer
        Private _HasWeightage As Boolean
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALFeedback_Survey As DALFeedback_Survey

#End Region

#Region "Public Properties"


        Public Property SurveyId() As Integer
            Set(ByVal value As Integer)
                _SurveyId = value
            End Set
            Get
                Return (_SurveyId)
            End Get
        End Property


        Public Property SurveyName() As String
            Set(ByVal value As String)
                _SurveyName = value
            End Set
            Get
                Return (_SurveyName)
            End Get
        End Property


        Public Property SurveyArabicName() As String
            Set(ByVal value As String)
                _SurveyArabicName = value
            End Set
            Get
                Return (_SurveyArabicName)
            End Get
        End Property


        Public Property SurveyLanguage() As Integer
            Set(ByVal value As Integer)
                _SurveyLanguage = value
            End Set
            Get
                Return (_SurveyLanguage)
            End Get
        End Property


        Public Property HasWeightage() As Boolean
            Set(ByVal value As Boolean)
                _HasWeightage = value
            End Set
            Get
                Return (_HasWeightage)
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

            objDALFeedback_Survey = New DALFeedback_Survey()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALFeedback_Survey.Add(_SurveyId, _SurveyName, _SurveyArabicName, _SurveyLanguage, _HasWeightage, _CREATED_BY)
            App_EventsLog.Insert_ToEventLog("Add", _SurveyId, "Feedback_Survey", "Define Survey")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALFeedback_Survey.Update(_SurveyId, _SurveyName, _SurveyArabicName, _SurveyLanguage, _HasWeightage, _LAST_UPDATE_BY)
            App_EventsLog.Insert_ToEventLog("Update", _SurveyId, "Feedback_Survey", "Define Survey")
            Return rslt

        End Function



        Public Function Delete() As Integer

            Dim rslt As Integer = objDALFeedback_Survey.Delete(_SurveyId)
            App_EventsLog.Insert_ToEventLog("Delete", _SurveyId, "Feedback_Survey", "Define Survey")
            Return rslt

        End Function

        Public Function GetAll() As DataTable

            Return objDALFeedback_Survey.GetAll()

        End Function

        Public Function GetByPK() As Feedback_Survey

            Dim dr As DataRow
            dr = objDALFeedback_Survey.GetByPK(_SurveyId)

            If Not IsDBNull(dr("SurveyId")) Then
                _SurveyId = dr("SurveyId")
            End If
            If Not IsDBNull(dr("SurveyName")) Then
                _SurveyName = dr("SurveyName")
            End If
            If Not IsDBNull(dr("SurveyArabicName")) Then
                _SurveyArabicName = dr("SurveyArabicName")
            End If
            If Not IsDBNull(dr("SurveyLanguage")) Then
                _SurveyLanguage = dr("SurveyLanguage")
            End If
            If Not IsDBNull(dr("HasWeightage")) Then
                _HasWeightage = dr("HasWeightage")
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