Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Definitions
    Public Class Holiday
#Region "Class Variables"

        Private _HolidayId As Integer
        Private _HolidayName As String
        Private _HolidayArabicName As String
        Private _isYearlyFixed As Boolean
        Private _StartDay As Integer
        Private _StartMonth As Integer
        Private _StartYear As Integer
        Private _EndDay As Integer
        Private _EndMonth As Integer
        Private _EndYear As Integer
        Private _IsCompanyApplicable As Boolean
        Private _IsWorkLocation As Boolean
        Private _IsReligion As Boolean
        Private _CREATED_BY As String
        Private _CREATED_DATE As Date
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As Date
        Private objDALHoliday As DALHoliday
        Private _HolidayDay As Date
        Private _EmployeeId As Integer
        Private _IsReligionRelated As Boolean
        Private _IsEmployeeType As Boolean
        Private _IsLogicalGroup As Boolean

#End Region

#Region "Public Properties"
        Public Property HolidayId() As Integer
            Set(ByVal value As Integer)
                _HolidayId = value
            End Set
            Get
                Return (_HolidayId)
            End Get
        End Property
        Public Property HolidayName() As String
            Set(ByVal value As String)
                _HolidayName = value
            End Set
            Get
                Return (_HolidayName)
            End Get
        End Property
        Public Property HolidayArabicName() As String
            Set(ByVal value As String)
                _HolidayArabicName = value
            End Set
            Get
                Return (_HolidayArabicName)
            End Get
        End Property
        Public Property isYearlyFixed() As Boolean
            Set(ByVal value As Boolean)
                _isYearlyFixed = value
            End Set
            Get
                Return (_isYearlyFixed)
            End Get
        End Property
        Public Property StartDay() As Integer
            Set(ByVal value As Integer)
                _StartDay = value
            End Set
            Get
                Return (_StartDay)
            End Get
        End Property
        Public Property StartMonth() As Integer
            Set(ByVal value As Integer)
                _StartMonth = value
            End Set
            Get
                Return (_StartMonth)
            End Get
        End Property
        Public Property StartYear() As Integer
            Set(ByVal value As Integer)
                _StartYear = value
            End Set
            Get
                Return (_StartYear)
            End Get
        End Property
        Public Property EndDay() As Integer
            Set(ByVal value As Integer)
                _EndDay = value
            End Set
            Get
                Return (_EndDay)
            End Get
        End Property
        Public Property EndMonth() As Integer
            Set(ByVal value As Integer)
                _EndMonth = value
            End Set
            Get
                Return (_EndMonth)
            End Get
        End Property
        Public Property EndYear() As Integer
            Set(ByVal value As Integer)
                _EndYear = value
            End Set
            Get
                Return (_EndYear)
            End Get
        End Property
        Public Property IsCompanyApplicable() As Boolean
            Set(ByVal value As Boolean)
                _IsCompanyApplicable = value
            End Set
            Get
                Return (_IsCompanyApplicable)
            End Get
        End Property
        Public Property IsWorkLocation() As Boolean
            Set(ByVal value As Boolean)
                _IsWorkLocation = value
            End Set
            Get
                Return (_IsWorkLocation)
            End Get
        End Property
        Public Property IsReligion() As Boolean
            Set(ByVal value As Boolean)
                _IsReligion = value
            End Set
            Get
                Return (_IsReligion)
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
        Public Property CREATED_DATE() As Date
            Set(ByVal value As Date)
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
        Public Property LAST_UPDATE_DATE() As Date
            Set(ByVal value As Date)
                _LAST_UPDATE_DATE = value
            End Set
            Get
                Return (_LAST_UPDATE_DATE)
            End Get
        End Property
        Public Property HolidayDay() As Date
            Set(ByVal value As Date)
                _HolidayDay = value
            End Set
            Get
                Return (_HolidayDay)
            End Get
        End Property
        Public Property EmployeeId() As Integer
            Set(ByVal value As Integer)
                _EmployeeId = value
            End Set
            Get
                Return (_EmployeeId)
            End Get
        End Property
        Public Property IsEmployeeType() As Boolean
            Get
                Return _IsEmployeeType
            End Get
            Set(ByVal value As Boolean)
                _IsEmployeeType = value
            End Set
        End Property
        Public Property IsLogicalGroup() As Boolean
            Get
                Return _IsLogicalGroup
            End Get
            Set(ByVal value As Boolean)
                _IsLogicalGroup = value
            End Set
        End Property
        Public Property IsReligionRelated() As Boolean
            Get
                Return _IsReligionRelated
            End Get
            Set(ByVal value As Boolean)
                _IsReligionRelated = value
            End Set
        End Property
#End Region

#Region "Constructor"
        Public Sub New()
            objDALHoliday = New DALHoliday()
        End Sub

#End Region

#Region "Methods"
        Public Function Add() As Integer

            Dim rslt As Integer = objDALHoliday.Add(_HolidayId, _HolidayName, _HolidayArabicName, _isYearlyFixed, _StartDay, _StartMonth, _StartYear, _EndDay, _EndMonth, _EndYear, _IsCompanyApplicable, _IsWorkLocation, _IsReligion, _CREATED_BY, _IsEmployeeType, _IsLogicalGroup, _IsReligionRelated)
            App_EventsLog.Insert_ToEventLog("Add", _HolidayId, "Holiday", "Define Holidays")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALHoliday.Update(_HolidayId, _HolidayName, _HolidayArabicName, _isYearlyFixed, _StartDay, _StartMonth, _StartYear, _EndDay, _EndMonth, _EndYear, _IsCompanyApplicable, _IsWorkLocation, _IsReligion, _LAST_UPDATE_BY, _IsEmployeeType, _IsLogicalGroup, _IsReligionRelated)
            App_EventsLog.Insert_ToEventLog("Edit", _HolidayId, "Emp_MaritalStatus", "Define Marital Status")
            Return rslt
        End Function

        Public Function Delete() As Integer
            Dim rslt As Integer = objDALHoliday.Delete(_HolidayId)
            App_EventsLog.Insert_ToEventLog("Delete", _HolidayId, "Emp_MaritalStatus", "Define Marital Status")
            Return rslt
        End Function

        Public Function GetAll() As DataTable
            Return objDALHoliday.GetAll()
        End Function

        Public Function EmpGetHolidays() As Integer
            Return objDALHoliday.Emp_GetHolidays(_EmployeeId, _HolidayDay)
        End Function

        Public Function GetByPK() As Holiday
            Dim dr As DataRow
            dr = objDALHoliday.GetByPK(_HolidayId)
            If Not IsDBNull(dr("HolidayId")) Then
                _HolidayId = dr("HolidayId")
            End If
            If Not IsDBNull(dr("HolidayName")) Then
                _HolidayName = dr("HolidayName")
            End If
            If Not IsDBNull(dr("HolidayArabicName")) Then
                _HolidayArabicName = dr("HolidayArabicName")
            End If
            If Not IsDBNull(dr("isYearlyFixed")) Then
                _isYearlyFixed = dr("isYearlyFixed")
            End If
            If Not IsDBNull(dr("StartDay")) Then
                _StartDay = dr("StartDay")
            End If
            If Not IsDBNull(dr("StartMonth")) Then
                _StartMonth = dr("StartMonth")
            End If
            If Not IsDBNull(dr("StartYear")) Then
                _StartYear = dr("StartYear")
            End If
            If Not IsDBNull(dr("EndDay")) Then
                _EndDay = dr("EndDay")
            End If
            If Not IsDBNull(dr("EndMonth")) Then
                _EndMonth = dr("EndMonth")
            End If
            If Not IsDBNull(dr("EndYear")) Then
                _EndYear = dr("EndYear")
            End If
            If Not IsDBNull(dr("IsCompanyApplicable")) Then
                _IsCompanyApplicable = dr("IsCompanyApplicable")
            End If
            If Not IsDBNull(dr("IsWorkLocation")) Then
                _IsWorkLocation = dr("IsWorkLocation")
            End If
            If Not IsDBNull(dr("IsReligion")) Then
                _IsReligion = dr("IsReligion")
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
            If Not IsDBNull(dr("IsEmployeeType")) Then
                _IsEmployeeType = dr("IsEmployeeType")
            End If
            If Not IsDBNull(dr("IsLogicalGroup")) Then
                _IsLogicalGroup = dr("IsLogicalGroup")
            End If
            If Not IsDBNull(dr("IsReligionRelated")) Then
                _IsReligionRelated = dr("IsReligionRelated")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace
