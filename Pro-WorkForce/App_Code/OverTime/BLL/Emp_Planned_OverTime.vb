Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

namespace TA.OverTime

Public Class Emp_Planned_OverTime

#Region "Class Variables"



        Private _PlannedOT_ID As Long
        Private _FK_EmployeeID As Long
        Private _OverTimeMonth As Integer
        Private _OverTimeYear As Integer
        Private _PlannedOT_Normal_Num As Integer
        Private _PlannedOT_Rest_Num As Integer
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALEmp_Planned_OverTime As DALEmp_Planned_OverTime

#End Region

#Region "Public Properties"



        Public Property PlannedOT_ID() As Long
            Set(ByVal value As Long)
                _PlannedOT_ID = value
            End Set
            Get
                Return (_PlannedOT_ID)
            End Get
        End Property


        Public Property FK_EmployeeID() As Long
            Set(ByVal value As Long)
                _FK_EmployeeID = value
            End Set
            Get
                Return (_FK_EmployeeID)
            End Get
        End Property


        Public Property OverTimeMonth() As Integer
            Set(ByVal value As Integer)
                _OverTimeMonth = value
            End Set
            Get
                Return (_OverTimeMonth)
            End Get
        End Property


        Public Property OverTimeYear() As Integer
            Set(ByVal value As Integer)
                _OverTimeYear = value
            End Set
            Get
                Return (_OverTimeYear)
            End Get
        End Property


        Public Property PlannedOT_Normal_Num() As Integer
            Set(ByVal value As Integer)
                _PlannedOT_Normal_Num = value
            End Set
            Get
                Return (_PlannedOT_Normal_Num)
            End Get
        End Property


        Public Property PlannedOT_Rest_Num() As Integer
            Set(ByVal value As Integer)
                _PlannedOT_Rest_Num = value
            End Set
            Get
                Return (_PlannedOT_Rest_Num)
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

            objDALEmp_Planned_OverTime = New DALEmp_Planned_OverTime()

        End Sub

#End Region

#Region "Methods"


        Public Function Add() As Integer

            Return objDALEmp_Planned_OverTime.Add(_FK_EmployeeID, _OverTimeMonth, _OverTimeYear, _PlannedOT_Normal_Num, _PlannedOT_Rest_Num, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE)
        End Function

        Public Function Update() As Integer

            Return objDALEmp_Planned_OverTime.Update(_PlannedOT_ID, _FK_EmployeeID, _OverTimeMonth, _OverTimeYear, _PlannedOT_Normal_Num, _PlannedOT_Rest_Num, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE)

        End Function



        Public Function Delete() As Integer

            Return objDALEmp_Planned_OverTime.Delete(_PlannedOT_ID)

        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_Planned_OverTime.GetAll()

        End Function

        Public Function GetByPK() As Emp_Planned_OverTime

            Dim dr As DataRow
            dr = objDALEmp_Planned_OverTime.GetByPK(_PlannedOT_ID)

            If Not IsDBNull(dr("PlannedOT_ID")) Then
                _PlannedOT_ID = dr("PlannedOT_ID")
            End If
            If Not IsDBNull(dr("FK_EmployeeID")) Then
                _FK_EmployeeID = dr("FK_EmployeeID")
            End If
            If Not IsDBNull(dr("OverTimeMonth")) Then
                _OverTimeMonth = dr("OverTimeMonth")
            End If
            If Not IsDBNull(dr("OverTimeYear")) Then
                _OverTimeYear = dr("OverTimeYear")
            End If
            If Not IsDBNull(dr("PlannedOT_Normal_Num")) Then
                _PlannedOT_Normal_Num = dr("PlannedOT_Normal_Num")
            End If
            If Not IsDBNull(dr("PlannedOT_Rest_Num")) Then
                _PlannedOT_Rest_Num = dr("PlannedOT_Rest_Num")
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
End namespace