Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.FeedBack

    Public Class Feedback_Answers

#Region "Class Variables"


        Private _AnswerId As Integer
        Private _FK_QuestionId As Integer
        Private _AnswerTextEn As String
        Private _AnswerTextAr As String
        Private _SmileyIcon As String
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALFeedback_Answers As DALFeedback_Answers

#End Region

#Region "Public Properties"

        Public Property AnswerId() As Integer
            Set(ByVal value As Integer)
                _AnswerId = value
            End Set
            Get
                Return (_AnswerId)
            End Get
        End Property

        Public Property FK_QuestionId() As Integer
            Set(ByVal value As Integer)
                _FK_QuestionId = value
            End Set
            Get
                Return (_FK_QuestionId)
            End Get
        End Property

        Public Property AnswerTextEn() As String
            Set(ByVal value As String)
                _AnswerTextEn = value
            End Set
            Get
                Return (_AnswerTextEn)
            End Get
        End Property

        Public Property AnswerTextAr() As String
            Set(ByVal value As String)
                _AnswerTextAr = value
            End Set
            Get
                Return (_AnswerTextAr)
            End Get
        End Property

        Public Property SmileyIcon() As String
            Set(ByVal value As String)
                _SmileyIcon = value
            End Set
            Get
                Return (_SmileyIcon)
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

            objDALFeedback_Answers = New DALFeedback_Answers()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALFeedback_Answers.Add(_AnswerId, _FK_QuestionId, _AnswerTextEn, _AnswerTextAr, _SmileyIcon, _CREATED_BY)
            App_EventsLog.Insert_ToEventLog("Add", _AnswerId, "Feedback_Answers", "Define Survey")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALFeedback_Answers.Update(_AnswerId, _FK_QuestionId, _AnswerTextEn, _AnswerTextAr, _SmileyIcon, _LAST_UPDATE_BY)
            App_EventsLog.Insert_ToEventLog("Update", _AnswerId, "Feedback_Answers", "Define Survey")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALFeedback_Answers.Delete(_AnswerId)
            App_EventsLog.Insert_ToEventLog("Delete", _AnswerId, "Feedback_Answers", "Define Survey")
            Return rslt
        End Function

        Public Function Delete_BY_QuestionId() As Integer

            Return objDALFeedback_Answers.Delete_BY_QuestionId(_FK_QuestionId)

        End Function
        Public Function GetAll() As DataTable

            Return objDALFeedback_Answers.GetAll()

        End Function
        Public Function GetAll_By_QuestionId() As DataTable

            Return objDALFeedback_Answers.GetAll_By_QuestionId(_FK_QuestionId)

        End Function
        Public Function GetByPK() As Feedback_Answers

            Dim dr As DataRow
            dr = objDALFeedback_Answers.GetByPK(_AnswerId)

            If Not IsDBNull(dr("AnswerId")) Then
                _AnswerId = dr("AnswerId")
            End If
            If Not IsDBNull(dr("FK_QuestionId")) Then
                _FK_QuestionId = dr("FK_QuestionId")
            End If
            If Not IsDBNull(dr("AnswerTextEn")) Then
                _AnswerTextEn = dr("AnswerTextEn")
            End If
            If Not IsDBNull(dr("AnswerTextAr")) Then
                _AnswerTextAr = dr("AnswerTextAr")
            End If
            If Not IsDBNull(dr("SmileyIcon")) Then
                _SmileyIcon = dr("SmileyIcon")
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