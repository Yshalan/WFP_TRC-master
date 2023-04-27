Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Appraisal

    Public Class Appraisal_EvaluationPoints

#Region "Class Variables"


        Private _EvaluationPoint As Integer
        Private _OldEvaluationPoint As Integer
        Private _PointName As String
        Private _PointNameArabic As String
        Private objDALAppraisal_EvaluationPoints As DALAppraisal_EvaluationPoints

#End Region

#Region "Public Properties"

        Public Property OldEvaluationPoint() As Integer
            Set(ByVal value As Integer)
                _OldEvaluationPoint = value
            End Set
            Get
                Return (_OldEvaluationPoint)
            End Get
        End Property
        Public Property EvaluationPoint() As Integer
            Set(ByVal value As Integer)
                _EvaluationPoint = value
            End Set
            Get
                Return (_EvaluationPoint)
            End Get
        End Property


        Public Property PointName() As String
            Set(ByVal value As String)
                _PointName = value
            End Set
            Get
                Return (_PointName)
            End Get
        End Property


        Public Property PointNameArabic() As String
            Set(ByVal value As String)
                _PointNameArabic = value
            End Set
            Get
                Return (_PointNameArabic)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALAppraisal_EvaluationPoints = New DALAppraisal_EvaluationPoints()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALAppraisal_EvaluationPoints.Add(_EvaluationPoint, _PointName, _PointNameArabic)
            App_EventsLog.Insert_ToEventLog("Add", _EvaluationPoint, "Appraisal_EvaluationPoints", "Appraisal Evaluation Points")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALAppraisal_EvaluationPoints.Update(_OldEvaluationPoint, _EvaluationPoint, _PointName, _PointNameArabic)
            App_EventsLog.Insert_ToEventLog("Update", _EvaluationPoint, "Appraisal_EvaluationPoints", "Appraisal Evaluation Points")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALAppraisal_EvaluationPoints.Delete(_EvaluationPoint)
            App_EventsLog.Insert_ToEventLog("Delete", _EvaluationPoint, "Appraisal_EvaluationPoints", "Appraisal Evaluation Points")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALAppraisal_EvaluationPoints.GetAll()

        End Function

        Public Function Get_PointsCount() As DataTable

            Return objDALAppraisal_EvaluationPoints.Get_PointsCount()

        End Function

        Public Function GetByPK() As Appraisal_EvaluationPoints

            Dim dr As DataRow
            dr = objDALAppraisal_EvaluationPoints.GetByPK(_EvaluationPoint)

            If Not IsDBNull(dr("EvaluationPoint")) Then
                _EvaluationPoint = dr("EvaluationPoint")
            End If
            If Not IsDBNull(dr("PointName")) Then
                _PointName = dr("PointName")
            End If
            If Not IsDBNull(dr("PointNameArabic")) Then
                _PointNameArabic = dr("PointNameArabic")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace