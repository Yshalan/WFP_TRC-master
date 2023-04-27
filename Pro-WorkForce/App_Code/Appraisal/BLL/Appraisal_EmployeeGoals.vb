Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Appraisal

    Public Class Appraisal_EmployeeGoals

#Region "Class Variables"


        Private _GoalId As Long
        Private _Year As Integer
        Private _FK_EmployeeId As Long
        Private _GoalSequence As Integer
        Private _GoalName As String
        Private _GoalDetails As String
        Private _Weight As Double
        Private _FK_AppraisalStatusId As Integer
        Private _EvaluationPointbyEmployee As Integer
        Private _FinalEvaluationPoint As Integer
        Private _EmployeeRemarks As String
        Private _FinalRemarks As String
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private _FK_ManagerId As Integer

        Private objDALAppraisal_EmployeeGoals As DALAppraisal_EmployeeGoals

#End Region

#Region "Public Properties"


        Public Property GoalId() As Long
            Set(ByVal value As Long)
                _GoalId = value
            End Set
            Get
                Return (_GoalId)
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


        Public Property FK_EmployeeId() As Long
            Set(ByVal value As Long)
                _FK_EmployeeId = value
            End Set
            Get
                Return (_FK_EmployeeId)
            End Get
        End Property


        Public Property GoalSequence() As Integer
            Set(ByVal value As Integer)
                _GoalSequence = value
            End Set
            Get
                Return (_GoalSequence)
            End Get
        End Property


        Public Property GoalName() As String
            Set(ByVal value As String)
                _GoalName = value
            End Set
            Get
                Return (_GoalName)
            End Get
        End Property


        Public Property GoalDetails() As String
            Set(ByVal value As String)
                _GoalDetails = value
            End Set
            Get
                Return (_GoalDetails)
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


        Public Property FK_AppraisalStatusId() As Integer
            Set(ByVal value As Integer)
                _FK_AppraisalStatusId = value
            End Set
            Get
                Return (_FK_AppraisalStatusId)
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

        Public Property FK_ManagerId() As Long
            Set(ByVal value As Long)
                _FK_ManagerId = value
            End Set
            Get
                Return (_FK_ManagerId)
            End Get
        End Property

#End Region

#Region "Constructor"

        Public Sub New()

            objDALAppraisal_EmployeeGoals = New DALAppraisal_EmployeeGoals()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALAppraisal_EmployeeGoals.Add(_GoalId, _Year, _FK_EmployeeId, _GoalSequence, _GoalName, _GoalDetails, _Weight, _FK_AppraisalStatusId, _EvaluationPointbyEmployee, _FinalEvaluationPoint, _EmployeeRemarks, _FinalRemarks, _CREATED_BY)
            App_EventsLog.Insert_ToEventLog("Add", _GoalId, "Appraisal_EmployeeGoals", "Employee Goals")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALAppraisal_EmployeeGoals.Update(_GoalId, _Year, _FK_EmployeeId, _GoalSequence, _GoalName, _GoalDetails, _Weight, _FK_AppraisalStatusId, _EvaluationPointbyEmployee, _FinalEvaluationPoint, _EmployeeRemarks, _FinalRemarks, _LAST_UPDATE_BY)
            App_EventsLog.Insert_ToEventLog("Update", _GoalId, "Appraisal_EmployeeGoals", "Employee Goals")
            Return rslt
        End Function

        Public Function UpdateStatus() As Integer

            Return objDALAppraisal_EmployeeGoals.UpdateStatus(_GoalId, _FK_AppraisalStatusId)

        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALAppraisal_EmployeeGoals.Delete(_GoalId)
            App_EventsLog.Insert_ToEventLog("Delete", _GoalId, "Appraisal_EmployeeGoals", "Employee Goals")
            Return rslt

        End Function

        Public Function GetAll() As DataTable

            Return objDALAppraisal_EmployeeGoals.GetAll()

        End Function

        Public Function Get_Year_Weight_Sum() As DataTable

            Return objDALAppraisal_EmployeeGoals.Get_Year_Weight_Sum(_FK_EmployeeId, _Year)

        End Function

        Public Function GetByPK() As Appraisal_EmployeeGoals

            Dim dr As DataRow
            dr = objDALAppraisal_EmployeeGoals.GetByPK(_GoalId)

            If Not IsDBNull(dr("GoalId")) Then
                _GoalId = dr("GoalId")
            End If
            If Not IsDBNull(dr("Year")) Then
                _Year = dr("Year")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("GoalSequence")) Then
                _GoalSequence = dr("GoalSequence")
            End If
            If Not IsDBNull(dr("GoalName")) Then
                _GoalName = dr("GoalName")
            End If
            If Not IsDBNull(dr("GoalDetails")) Then
                _GoalDetails = dr("GoalDetails")
            End If
            If Not IsDBNull(dr("Weight")) Then
                _Weight = dr("Weight")
            End If
            If Not IsDBNull(dr("FK_AppraisalStatusId")) Then
                _FK_AppraisalStatusId = dr("FK_AppraisalStatusId")
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
            Return Me
        End Function

        Public Function Get_GoalSequence_By_EmpId_Year() As Appraisal_EmployeeGoals

            Dim dr As DataRow
            dr = objDALAppraisal_EmployeeGoals.Get_GoalSequence_By_EmpId_Year(_FK_EmployeeId, _Year)
            If Not IsDBNull(dr("GoalSequence")) Then
                _GoalSequence = dr("GoalSequence")
            End If
            Return Me
        End Function

        Public Function Get_By_FK_EmployeeId_StatusId() As DataTable

            Return objDALAppraisal_EmployeeGoals.Get_By_FK_EmployeeId_StatusId(_FK_EmployeeId, _FK_AppraisalStatusId, _Year)

        End Function

        Public Function Get_By_Manager_Status() As DataTable

            Return objDALAppraisal_EmployeeGoals.Get_By_Manager_Status(_FK_ManagerId, _FK_AppraisalStatusId, _Year)

        End Function

        Public Function Get_By_EmployeeId_Year() As DataTable

            Return objDALAppraisal_EmployeeGoals.Get_By_EmployeeId_Year(_FK_EmployeeId, _Year, _FK_AppraisalStatusId)

        End Function

#End Region

    End Class
End Namespace