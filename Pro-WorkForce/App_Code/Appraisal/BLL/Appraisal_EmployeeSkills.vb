Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Appraisal

    Public Class Appraisal_EmployeeSkills

#Region "Class Variables"


        Private _FK_EmployeeId As Long
        Private _FK_SkillId As Integer
        Private _Year As Integer
        Private _FK_AppraisalStatusId As Integer
        Private _Weight As Double
        Private _EvaluationPointbyEmployee As Integer
        Private _FinalEvaluationPoint As Integer
        Private _EmployeeRemarks As String
        Private _FinalRemarks As String
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALAppraisal_EmployeeSkills As DALAppraisal_EmployeeSkills

#End Region

#Region "Public Properties"


        Public Property FK_EmployeeId() As Long
            Set(ByVal value As Long)
                _FK_EmployeeId = value
            End Set
            Get
                Return (_FK_EmployeeId)
            End Get
        End Property


        Public Property FK_SkillId() As Integer
            Set(ByVal value As Integer)
                _FK_SkillId = value
            End Set
            Get
                Return (_FK_SkillId)
            End Get
        End Property


        Public Property Year() As Integer
            Set(ByVal value As Integer)
                _Year = value
            End Set
            Get
                Return (_Year)
            End Get
        End Property


        Public Property FK_AppraisalStatusId() As Integer
            Set(ByVal value As Integer)
                _FK_AppraisalStatusId = value
            End Set
            Get
                Return (_FK_AppraisalStatusId)
            End Get
        End Property


        Public Property Weight() As Double
            Set(ByVal value As Double)
                _Weight = value
            End Set
            Get
                Return (_Weight)
            End Get
        End Property


        Public Property EvaluationPointbyEmployee() As Integer
            Set(ByVal value As Integer)
                _EvaluationPointbyEmployee = value
            End Set
            Get
                Return (_EvaluationPointbyEmployee)
            End Get
        End Property


        Public Property FinalEvaluationPoint() As Integer
            Set(ByVal value As Integer)
                _FinalEvaluationPoint = value
            End Set
            Get
                Return (_FinalEvaluationPoint)
            End Get
        End Property


        Public Property EmployeeRemarks() As String
            Set(ByVal value As String)
                _EmployeeRemarks = value
            End Set
            Get
                Return (_EmployeeRemarks)
            End Get
        End Property


        Public Property FinalRemarks() As String
            Set(ByVal value As String)
                _FinalRemarks = value
            End Set
            Get
                Return (_FinalRemarks)
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

            objDALAppraisal_EmployeeSkills = New DALAppraisal_EmployeeSkills()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALAppraisal_EmployeeSkills.Add(_FK_EmployeeId, _FK_SkillId, _Year, _FK_AppraisalStatusId, _Weight, _EvaluationPointbyEmployee, _FinalEvaluationPoint, _EmployeeRemarks, _FinalRemarks, _CREATED_BY)
            App_EventsLog.Insert_ToEventLog("Add", _FK_EmployeeId.ToString() + ", " + _FK_SkillId.ToString + ", " + _Year.ToString, "Appraisal_EmployeeSkills", "Employee Skills")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALAppraisal_EmployeeSkills.Update(_FK_EmployeeId, _FK_SkillId, _Year, _FK_AppraisalStatusId, _Weight, _EvaluationPointbyEmployee, _FinalEvaluationPoint, _EmployeeRemarks, _FinalRemarks, _LAST_UPDATE_BY)
            App_EventsLog.Insert_ToEventLog("Update", _FK_EmployeeId.ToString() + ", " + _FK_SkillId.ToString + ", " + _Year.ToString, "Appraisal_EmployeeSkills", "Employee Skills")
            Return rslt
        End Function

        Public Function UpdateStatus() As Integer

            Return objDALAppraisal_EmployeeSkills.UpdateStatus(_FK_EmployeeId, _FK_SkillId, _Year, _FK_AppraisalStatusId)

        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALAppraisal_EmployeeSkills.Delete(_FK_EmployeeId, _FK_SkillId, _Year)
            App_EventsLog.Insert_ToEventLog("Delete", _FK_EmployeeId.ToString() + ", " + _FK_SkillId.ToString + ", " + _Year.ToString, "Appraisal_EmployeeSkills", "Employee Skills")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALAppraisal_EmployeeSkills.GetAll()

        End Function

        Public Function GetBy_FK_EmployeeId() As DataTable

            Return objDALAppraisal_EmployeeSkills.GetBy_FK_EmployeeId(_FK_EmployeeId, _Year, _FK_AppraisalStatusId)

        End Function

        Public Function GetByPK() As Appraisal_EmployeeSkills

            Dim dr As DataRow
            dr = objDALAppraisal_EmployeeSkills.GetByPK(_FK_EmployeeId, _FK_SkillId, _Year)
            If Not dr Is Nothing Then


                If Not IsDBNull(dr("FK_EmployeeId")) Then
                    _FK_EmployeeId = dr("FK_EmployeeId")
                End If
                If Not IsDBNull(dr("FK_SkillId")) Then
                    _FK_SkillId = dr("FK_SkillId")
                End If
                If Not IsDBNull(dr("Year")) Then
                    _Year = dr("Year")
                End If
                If Not IsDBNull(dr("FK_AppraisalStatusId")) Then
                    _FK_AppraisalStatusId = dr("FK_AppraisalStatusId")
                End If
                If Not IsDBNull(dr("Weight")) Then
                    _Weight = dr("Weight")
                End If
                If Not IsDBNull(dr("EvaluationPointbyEmployee")) Then
                    _EvaluationPointbyEmployee = dr("EvaluationPointbyEmployee")
                End If
                If Not IsDBNull(dr("FinalEvaluationPoint")) Then
                    _FinalEvaluationPoint = dr("FinalEvaluationPoint")
                End If
                If Not IsDBNull(dr("EmployeeRemarks")) Then
                    _EmployeeRemarks = dr("EmployeeRemarks")
                End If
                If Not IsDBNull(dr("FinalRemarks")) Then
                    _FinalRemarks = dr("FinalRemarks")
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
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace