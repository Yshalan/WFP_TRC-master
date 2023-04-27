Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Admin

    Public Class Emp_Grade

#Region "Class Variables"


        Private _GradeId As Integer
        Private _GradeCode As String
        Private _GradeName As String
        Private _GradeArabicName As String
        Private _AnnualLeaveBalance As Double
        Private _FK_OvertimeRuleId As Integer
        Private _IsTAException As Boolean
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALEmp_Grade As DALEmp_Grade

#End Region

#Region "Public Properties"


        Public Property GradeId() As Integer
            Set(ByVal value As Integer)
                _GradeId = value
            End Set
            Get
                Return (_GradeId)
            End Get
        End Property


        Public Property GradeCode() As String
            Set(ByVal value As String)
                _GradeCode = value
            End Set
            Get
                Return (_GradeCode)
            End Get
        End Property


        Public Property GradeName() As String
            Set(ByVal value As String)
                _GradeName = value
            End Set
            Get
                Return (_GradeName)
            End Get
        End Property


        Public Property GradeArabicName() As String
            Set(ByVal value As String)
                _GradeArabicName = value
            End Set
            Get
                Return (_GradeArabicName)
            End Get
        End Property


        Public Property AnnualLeaveBalance() As Double
            Set(ByVal value As Double)
                _AnnualLeaveBalance = value
            End Set
            Get
                Return (_AnnualLeaveBalance)
            End Get
        End Property


        Public Property FK_OvertimeRuleId() As Integer
            Set(ByVal value As Integer)
                _FK_OvertimeRuleId = value
            End Set
            Get
                Return (_FK_OvertimeRuleId)
            End Get
        End Property


        Public Property IsTAException() As Boolean
            Set(ByVal value As Boolean)
                _IsTAException = value
            End Set
            Get
                Return (_IsTAException)
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

            objDALEmp_Grade = New DALEmp_Grade()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer
            Dim rslt As Integer = objDALEmp_Grade.Add(_GradeId, _GradeCode, _GradeName, _GradeArabicName, _AnnualLeaveBalance, _FK_OvertimeRuleId, _IsTAException, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE)
            App_EventsLog.Insert_ToEventLog("Add", _GradeId, "Emp_Grade", "Define Grades")
            Return rslt
        End Function

        Public Function Update() As Integer
            Dim rslt As Integer = objDALEmp_Grade.Update(_GradeId, _GradeCode, _GradeName, _GradeArabicName, _AnnualLeaveBalance, _FK_OvertimeRuleId, _IsTAException, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE)
            App_EventsLog.Insert_ToEventLog("Edit", _GradeId, "Emp_Grade", "Define Grades")
            Return rslt

        End Function

        Public Function Delete() As Integer
            Dim rslt As Integer = objDALEmp_Grade.Delete(_GradeId)
            App_EventsLog.Insert_ToEventLog("Delete", _GradeId, "Emp_Grade", "Define Grades")
            Return rslt

        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_Grade.GetAll()

        End Function

        Public Function GetByPK() As Emp_Grade

            Dim dr As DataRow
            dr = objDALEmp_Grade.GetByPK(_GradeId)

            If dr Is Nothing Then Return Nothing

            If Not IsDBNull(dr("GradeId")) Then
                _GradeId = dr("GradeId")
            End If
            If Not IsDBNull(dr("GradeCode")) Then
                _GradeCode = dr("GradeCode")
            End If
            If Not IsDBNull(dr("GradeName")) Then
                _GradeName = dr("GradeName")
            End If
            If Not IsDBNull(dr("GradeArabicName")) Then
                _GradeArabicName = dr("GradeArabicName")
            End If
            If Not IsDBNull(dr("AnnualLeaveBalance")) Then
                _AnnualLeaveBalance = dr("AnnualLeaveBalance")
            End If
            If Not IsDBNull(dr("FK_OvertimeRuleId")) Then
                _FK_OvertimeRuleId = dr("FK_OvertimeRuleId")
            End If
            If Not IsDBNull(dr("IsTAException")) Then
                _IsTAException = dr("IsTAException")
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