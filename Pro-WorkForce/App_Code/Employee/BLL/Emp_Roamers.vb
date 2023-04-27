Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Employees

    Public Class Emp_Roamers

#Region "Class Variables"


        Private _RoamerId As Integer
        Private _FK_EmployeeId As Integer
        Private _FromDate As DateTime
        Private _ToDate As DateTime
        Private _IsTemporary As Boolean
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALEmp_Roamers As DALEmp_Roamers

#End Region

#Region "Public Properties"


        Public Property RoamerId() As Integer
            Set(ByVal value As Integer)
                _RoamerId = value
            End Set
            Get
                Return (_RoamerId)
            End Get
        End Property


        Public Property FK_EmployeeId() As Integer
            Set(ByVal value As Integer)
                _FK_EmployeeId = value
            End Set
            Get
                Return (_FK_EmployeeId)
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


        Public Property IsTemporary() As Boolean
            Set(ByVal value As Boolean)
                _IsTemporary = value
            End Set
            Get
                Return (_IsTemporary)
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

            objDALEmp_Roamers = New DALEmp_Roamers()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALEmp_Roamers.Add(_FK_EmployeeId, _FromDate, _ToDate, _IsTemporary, _CREATED_BY, _CREATED_DATE)
        End Function

        Public Function Update() As Integer

            Return objDALEmp_Roamers.Update(_RoamerId, _FK_EmployeeId, _FromDate, _ToDate, _IsTemporary, _LAST_UPDATE_BY)

        End Function

        Public Function Get_Emp_Roamers_Details() As DataTable

            Return objDALEmp_Roamers.Get_Emp_Roamers_Details()

        End Function

        Public Function Delete() As Integer

            Return objDALEmp_Roamers.Delete(_RoamerId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_Roamers.GetAll()

        End Function

        Public Function GetByPK() As Emp_Roamers

            Dim dr As DataRow
            dr = objDALEmp_Roamers.GetByPK(_RoamerId)

            If Not IsDBNull(dr("RoamerId")) Then
                _RoamerId = dr("RoamerId")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("FromDate")) Then
                _FromDate = dr("FromDate")
            End If
            If Not IsDBNull(dr("ToDate")) Then
                _ToDate = dr("ToDate")
            End If
            If Not IsDBNull(dr("IsTemporary")) Then
                _IsTemporary = dr("IsTemporary")
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