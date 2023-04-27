Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.FeedBack

    Public Class Feedback_UserAnswers

#Region "Class Variables"


        Private _UserAnswerId As Integer
        Private _FK_SurveyId As Integer
        Private _FK_QuestionId As Integer
        Private _FK_AnswerId As Integer
        Private _AnswerText As String
        Private _FK_EmployeeId As Long
        Private _CREATED_DATE As DateTime
        Private _DeviceDetails As String
        Private objDALFeedback_UserAnswers As DALFeedback_UserAnswers

#End Region

#Region "Public Properties"

        Public Property UserAnswerId() As Integer
            Set(ByVal value As Integer)
                _UserAnswerId = value
            End Set
            Get
                Return (_UserAnswerId)
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

        Public Property FK_QuestionId() As Integer
            Set(ByVal value As Integer)
                _FK_QuestionId = value
            End Set
            Get
                Return (_FK_QuestionId)
            End Get
        End Property

        Public Property FK_AnswerId() As Integer
            Set(ByVal value As Integer)
                _FK_AnswerId = value
            End Set
            Get
                Return (_FK_AnswerId)
            End Get
        End Property

        Public Property AnswerText() As String
            Set(ByVal value As String)
                _AnswerText = value
            End Set
            Get
                Return (_AnswerText)
            End Get
        End Property

        Public Property FK_EmployeeId() As Long
            Set(ByVal value As Long)
                _FK_EmployeeId = value
            End Set
            Get
                Return (_FK_EmployeeId)
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

        Public Property DeviceDetails() As String
            Set(ByVal value As String)
                _DeviceDetails = value
            End Set
            Get
                Return (_DeviceDetails)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALFeedback_UserAnswers = New DALFeedback_UserAnswers()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALFeedback_UserAnswers.Add(_FK_SurveyId, _FK_QuestionId, _FK_AnswerId, _AnswerText, _FK_EmployeeId, _DeviceDetails)
        End Function

        Public Function Update() As Integer

            Return objDALFeedback_UserAnswers.Update(_UserAnswerId, _FK_SurveyId, _FK_QuestionId, _FK_AnswerId, _AnswerText, _FK_EmployeeId, _DeviceDetails)

        End Function

        Public Function Delete() As Integer

            Return objDALFeedback_UserAnswers.Delete(_UserAnswerId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALFeedback_UserAnswers.GetAll()

        End Function

        Public Function GetByPK() As Feedback_UserAnswers

            Dim dr As DataRow
            dr = objDALFeedback_UserAnswers.GetByPK(_UserAnswerId)

            If Not IsDBNull(dr("UserAnswerId")) Then
                _UserAnswerId = dr("UserAnswerId")
            End If
            If Not IsDBNull(dr("FK_SurveyId")) Then
                _FK_SurveyId = dr("FK_SurveyId")
            End If
            If Not IsDBNull(dr("FK_QuestionId")) Then
                _FK_QuestionId = dr("FK_QuestionId")
            End If
            If Not IsDBNull(dr("FK_AnswerId")) Then
                _FK_AnswerId = dr("FK_AnswerId")
            End If
            If Not IsDBNull(dr("AnswerText")) Then
                _AnswerText = dr("AnswerText")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("CREATED_DATE")) Then
                _CREATED_DATE = dr("CREATED_DATE")
            End If
            If Not IsDBNull(dr("DeviceDetails")) Then
                _DeviceDetails = dr("DeviceDetails")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace