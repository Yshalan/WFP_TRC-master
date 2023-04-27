Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.FeedBack

    Public Class Feedback_Questions

#Region "Class Variables"

        Private _QuestionId As Integer
        Private _QuestionType As Integer
        Private _QuestionEnText As String
        Private _QuestionArText As String
        Private _FK_SurveyId As Integer
        Private _IsAnswerRequired As Boolean
        Private _Weight As Integer
        Private _IsDeleted As Boolean
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALFeedback_Questions As DALFeedback_Questions

#End Region

#Region "Public Properties"


        Public Property QuestionId() As Integer
            Set(ByVal value As Integer)
                _QuestionId = value
            End Set
            Get
                Return (_QuestionId)
            End Get
        End Property

        Public Property QuestionType() As Integer
            Set(ByVal value As Integer)
                _QuestionType = value
            End Set
            Get
                Return (_QuestionType)
            End Get
        End Property

        Public Property QuestionEnText() As String
            Set(ByVal value As String)
                _QuestionEnText = value
            End Set
            Get
                Return (_QuestionEnText)
            End Get
        End Property

        Public Property QuestionArText() As String
            Set(ByVal value As String)
                _QuestionArText = value
            End Set
            Get
                Return (_QuestionArText)
            End Get
        End Property

        Public Property FK_SurveyId() As Integer
            Set(ByVal value As Integer)
                _FK_SurveyId = value
            End Set
            Get
                Return (_FK_SurveyId)
            End Get
        End Property

        Public Property IsAnswerRequired() As Boolean
            Set(ByVal value As Boolean)
                _IsAnswerRequired = value
            End Set
            Get
                Return (_IsAnswerRequired)
            End Get
        End Property

        Public Property Weight() As Integer
            Set(ByVal value As Integer)
                _Weight = value
            End Set
            Get
                Return (_Weight)
            End Get
        End Property

        Public Property IsDeleted() As Boolean
            Set(ByVal value As Boolean)
                _IsDeleted = value
            End Set
            Get
                Return (_IsDeleted)
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

            objDALFeedback_Questions = New DALFeedback_Questions()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALFeedback_Questions.Add(_QuestionId, _QuestionType, _QuestionEnText, _QuestionArText, _FK_SurveyId, _IsAnswerRequired, _Weight, _IsDeleted, _CREATED_BY)
            App_EventsLog.Insert_ToEventLog("Add", _QuestionId, "Feedback_Questions", "Define Survey")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALFeedback_Questions.Update(_QuestionId, _QuestionType, _QuestionEnText, _QuestionArText, _FK_SurveyId, _IsAnswerRequired, _Weight, _IsDeleted, _LAST_UPDATE_BY)
            App_EventsLog.Insert_ToEventLog("Update", _QuestionId, "Feedback_Questions", "Define Survey")
            Return rslt

        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALFeedback_Questions.Delete(_QuestionId)
            App_EventsLog.Insert_ToEventLog("Delete", _QuestionId, "Feedback_Questions", "Define Survey")
            Return rslt

        End Function

        Public Function Delete_Update_IsDeleted() As Integer

            Dim rslt As Integer = objDALFeedback_Questions.Delete_Update_IsDeleted(_QuestionId)
            App_EventsLog.Insert_ToEventLog("Delete_Update_IsDeleted", _QuestionId, "Feedback_Questions", "Define Survey")
            Return rslt

        End Function

        Public Function GetAll() As DataTable

            Return objDALFeedback_Questions.GetAll()

        End Function

        Public Function GetAll_BY_FK_SurveyId() As DataTable

            Return objDALFeedback_Questions.GetAll_BY_FK_SurveyId(_FK_SurveyId)

        End Function

        Public Function GetByPK() As Feedback_Questions

            Dim dr As DataRow
            dr = objDALFeedback_Questions.GetByPK(_QuestionId)

            If Not IsDBNull(dr("QuestionId")) Then
                _QuestionId = dr("QuestionId")
            End If
            If Not IsDBNull(dr("QuestionType")) Then
                _QuestionType = dr("QuestionType")
            End If
            If Not IsDBNull(dr("QuestionEnText")) Then
                _QuestionEnText = dr("QuestionEnText")
            End If
            If Not IsDBNull(dr("QuestionArText")) Then
                _QuestionArText = dr("QuestionArText")
            End If
            If Not IsDBNull(dr("FK_SurveyId")) Then
                _FK_SurveyId = dr("FK_SurveyId")
            End If
            If Not IsDBNull(dr("IsAnswerRequired")) Then
                _IsAnswerRequired = dr("IsAnswerRequired")
            End If
            If Not IsDBNull(dr("Weight")) Then
                _Weight = dr("Weight")
            End If
            If Not IsDBNull(dr("IsDeleted")) Then
                _IsDeleted = dr("IsDeleted")
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