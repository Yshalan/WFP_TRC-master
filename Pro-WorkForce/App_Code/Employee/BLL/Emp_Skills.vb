Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Employees

    Public Class Emp_Skills

#Region "Class Variables"


        Private _FK_EmployeeId As Long
        Private _FK_SkillId As Long
        Private _FK_CategoryId As Long
        Private _FromDate As DateTime
        Private _ToDate As DateTime
        Private _Year As Integer
        Private objDALEmp_Skills As DALEmp_Skills

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

        Public Property FK_SkillId() As Long
            Set(ByVal value As Long)
                _FK_SkillId = value
            End Set
            Get
                Return (_FK_SkillId)
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

        Public Property Year() As Integer
            Set(ByVal value As Integer)
                _Year = value
            End Set
            Get
                Return (_Year)
            End Get
        End Property

#End Region

#Region "Constructor"

        Public Sub New()

            objDALEmp_Skills = New DALEmp_Skills()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALEmp_Skills.Add(_FK_EmployeeId, _FK_SkillId, _FromDate, _ToDate)
        End Function

        Public Function Update() As Integer

            Return objDALEmp_Skills.Update(_FK_EmployeeId, _FK_SkillId)

        End Function

        Public Function Delete() As Integer

            Return objDALEmp_Skills.Delete(_FK_EmployeeId, _FK_SkillId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_Skills.GetAll()

        End Function

        Public Function GetByPK() As Emp_Skills

            Dim dr As DataRow
            dr = objDALEmp_Skills.GetByPK(_FK_EmployeeId, _FK_SkillId)

            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("FK_SkillId")) Then
                _FK_SkillId = dr("FK_SkillId")
            End If
            If Not IsDBNull(dr("FromDate")) Then
                _FromDate = dr("FromDate")
            End If
            If Not IsDBNull(dr("ToDate")) Then
                _ToDate = dr("ToDate")
            End If
            Return Me
        End Function

        Public Function GetAll_By_EmployeeId_CategoryId() As DataTable

            Return objDALEmp_Skills.GetAll_By_EmployeeId_CategoryId(_FK_CategoryId, _FK_EmployeeId)

        End Function

        Public Function Get_By_FK_EmployeeId_Appraisal() As DataTable

            Return objDALEmp_Skills.Get_By_FK_EmployeeId_Appraisal(_FK_EmployeeId, _Year)

        End Function

#End Region

    End Class
End Namespace