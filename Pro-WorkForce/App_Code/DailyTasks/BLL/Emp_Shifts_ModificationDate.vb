Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.DailyTasks

    Public Class Emp_Shifts_ModificationDate

#Region "Class Variables"


        Private _ModificationId As Long
        Private _DateOption As Integer
        Private _FromDay As Integer?
        Private _ToDay As Integer?
        Private _FromDate As DateTime
        Private _ToDate As DateTime
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALEmp_Shifts_ModificationDate As DALEmp_Shifts_ModificationDate

#End Region

#Region "Public Properties"

        Public Property ModificationId() As Long
            Set(ByVal value As Long)
                _ModificationId = value
            End Set
            Get
                Return (_ModificationId)
            End Get
        End Property

        Public Property DateOption() As Integer
            Set(ByVal value As Integer)
                _DateOption = value
            End Set
            Get
                Return (_DateOption)
            End Get
        End Property

        Public Property FromDay() As Integer?
            Set(ByVal value As Integer?)
                _FromDay = value
            End Set
            Get
                Return (_FromDay)
            End Get
        End Property

        Public Property ToDay() As Integer?
            Set(ByVal value As Integer?)
                _ToDay = value
            End Set
            Get
                Return (_ToDay)
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

            objDALEmp_Shifts_ModificationDate = New DALEmp_Shifts_ModificationDate()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALEmp_Shifts_ModificationDate.Add(_ModificationId, _DateOption, _FromDay, _ToDay, _FromDate, _ToDate, _CREATED_BY)
            App_EventsLog.Insert_ToEventLog("Add", _ModificationId, "Emp_Shifts_ModificationDate", "Shift Preparation Period")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALEmp_Shifts_ModificationDate.Update(_ModificationId, _DateOption, _FromDay, _ToDay, _FromDate, _ToDate, _LAST_UPDATE_BY)
            App_EventsLog.Insert_ToEventLog("Update", _ModificationId, "Emp_Shifts_ModificationDate", "Shift Preparation Period")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALEmp_Shifts_ModificationDate.Delete(_ModificationId)
            App_EventsLog.Insert_ToEventLog("Delete", _ModificationId, "Emp_Shifts_ModificationDate", "Shift Preparation Period")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_Shifts_ModificationDate.GetAll()

        End Function

        Public Function GetByPK() As Emp_Shifts_ModificationDate

            Dim dr As DataRow
            dr = objDALEmp_Shifts_ModificationDate.GetByPK(_ModificationId)

            If Not IsDBNull(dr("ModificationId")) Then
                _ModificationId = dr("ModificationId")
            End If
            If Not IsDBNull(dr("DateOption")) Then
                _DateOption = dr("DateOption")
            End If
            If Not IsDBNull(dr("FromDay")) Then
                _FromDay = dr("FromDay")
            End If
            If Not IsDBNull(dr("ToDay")) Then
                _ToDay = dr("ToDay")
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

        Public Function Get_DateExists() As DataTable

            Return objDALEmp_Shifts_ModificationDate.Get_DateExists()

        End Function

        Public Function GetActiveDates() As DataTable

            Return objDALEmp_Shifts_ModificationDate.GetActiveDates()

        End Function

#End Region

    End Class
End Namespace