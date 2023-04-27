Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.FeedBack

    Public Class Feedback_SurveyAssignment

#Region "Class Variables"


        Private _AssignmentId As Integer
        Private _FK_EmployeeId As Long
        Private _FK_LogicalGroupId As Integer
        Private _FK_SurveyId As Integer
        Private _FromDate As DateTime
        Private _ToDate As DateTime
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALFeedback_SurveyAssignment As DALFeedback_SurveyAssignment

#End Region

#Region "Public Properties"

        Public Property AssignmentId() As Integer
            Set(ByVal value As Integer)
                _AssignmentId = value
            End Set
            Get
                Return (_AssignmentId)
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

        Public Property FK_LogicalGroupId() As Integer
            Set(ByVal value As Integer)
                _FK_LogicalGroupId = value
            End Set
            Get
                Return (_FK_LogicalGroupId)
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

        Public Property FromDate() As DateTime
            Set(ByVal value As DateTime)
                _FromDate = value
            End Set
            Get
                Return (_FromDate)
            End Get
        End Property

        Public Property ToDate() As DateTime
            Set(ByVal value As DateTime)
                _ToDate = value
            End Set
            Get
                Return (_ToDate)
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

            objDALFeedback_SurveyAssignment = New DALFeedback_SurveyAssignment()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALFeedback_SurveyAssignment.Add(_AssignmentId, _FK_EmployeeId, _FK_LogicalGroupId, _FK_SurveyId, _FromDate, _ToDate, _CREATED_BY)
            App_EventsLog.Insert_ToEventLog("Add", _AssignmentId, "Feedback_SurveyAssignment", "Define Survey")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALFeedback_SurveyAssignment.Update(_AssignmentId, _FK_EmployeeId, _FK_LogicalGroupId, _FK_SurveyId, _FromDate, _ToDate, _LAST_UPDATE_BY)
            App_EventsLog.Insert_ToEventLog("Update", _AssignmentId, "Feedback_SurveyAssignment", "Define Survey")
            Return rslt

        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALFeedback_SurveyAssignment.Delete(_AssignmentId)
            App_EventsLog.Insert_ToEventLog("Delete", _AssignmentId, "Feedback_SurveyAssignment", "Define Survey")
            Return rslt

        End Function

        Public Function GetAll() As DataTable

            Return objDALFeedback_SurveyAssignment.GetAll()

        End Function

        Public Function GetAll_ForEmp() As DataTable

            Return objDALFeedback_SurveyAssignment.GetAll_ForEmp()

        End Function

        Public Function GetAll_ForLogicalGroup() As DataTable

            Return objDALFeedback_SurveyAssignment.GetAll_ForLogicalGroup

        End Function

        Public Function GetByPK() As Feedback_SurveyAssignment

            Dim dr As DataRow
            dr = objDALFeedback_SurveyAssignment.GetByPK(_AssignmentId)

            If Not IsDBNull(dr("AssignmentId")) Then
                _AssignmentId = dr("AssignmentId")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("FK_LogicalGroupId")) Then
                _FK_LogicalGroupId = dr("FK_LogicalGroupId")
            End If
            If Not IsDBNull(dr("FK_SurveyId")) Then
                _FK_SurveyId = dr("FK_SurveyId")
            End If
            If Not IsDBNull(dr("FromDate")) Then
                _FromDate = dr("FromDate")
            End If
            If Not IsDBNull(dr("ToDate")) Then
                _ToDate = dr("ToDate")
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

        Public Function Get_RandomQuestions_Mobile() As DataTable

            Return objDALFeedback_SurveyAssignment.Get_RandomQuestions_Mobile(_FK_EmployeeId)

        End Function

#End Region

    End Class
End Namespace