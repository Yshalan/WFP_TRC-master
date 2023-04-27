Imports System.Data
Imports TA.Events

Namespace TA.DailyTasks

    Public Class Emp_Allowed_GPSCoordinates

#Region "Class Variables"


        Private _AllowedGPSId As Long
        Private _FK_EmployeeId As Long
        Private _LocationName As String
        Private _LocationArabicName As String
        Private _GPS_Coordinates As String
        Private _Radius As Integer
        Private _FromDate As DateTime
        Private _ToDate As DateTime
        Private _IsTemporary As Boolean
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALEmp_Allowed_GPSCoordinates As DALEmp_Allowed_GPSCoordinates

#End Region

#Region "Public Properties"

        Public Property AllowedGPSId() As Long
            Set(ByVal value As Long)
                _AllowedGPSId = value
            End Set
            Get
                Return (_AllowedGPSId)
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

        Public Property LocationName() As String
            Set(ByVal value As String)
                _LocationName = value
            End Set
            Get
                Return (_LocationName)
            End Get
        End Property

        Public Property LocationArabicName() As String
            Set(ByVal value As String)
                _LocationArabicName = value
            End Set
            Get
                Return (_LocationArabicName)
            End Get
        End Property

        Public Property GPS_Coordinates() As String
            Set(ByVal value As String)
                _GPS_Coordinates = value
            End Set
            Get
                Return (_GPS_Coordinates)
            End Get
        End Property

        Public Property Radius() As Integer
            Set(ByVal value As Integer)
                _Radius = value
            End Set
            Get
                Return (_Radius)
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

            objDALEmp_Allowed_GPSCoordinates = New DALEmp_Allowed_GPSCoordinates()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALEmp_Allowed_GPSCoordinates.Add(_AllowedGPSId, _FK_EmployeeId, _LocationName, _LocationArabicName, _GPS_Coordinates, _Radius, _FromDate, _ToDate, _IsTemporary, _CREATED_BY)
            App_EventsLog.Insert_ToEventLog("Add", _AllowedGPSId, "Emp_Allowed_GPSCoordinates", "Allowed GPS Coordinates")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALEmp_Allowed_GPSCoordinates.Update(_AllowedGPSId, _FK_EmployeeId, _LocationName, _LocationArabicName, _GPS_Coordinates, _Radius, _FromDate, _ToDate, _IsTemporary, _LAST_UPDATE_BY)
            App_EventsLog.Insert_ToEventLog("Update", _AllowedGPSId, "Emp_Allowed_GPSCoordinates", "Allowed GPS Coordinates")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALEmp_Allowed_GPSCoordinates.Delete(_AllowedGPSId)
            App_EventsLog.Insert_ToEventLog("Delete", _AllowedGPSId, "Emp_Allowed_GPSCoordinates", "Allowed GPS Coordinates")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_Allowed_GPSCoordinates.GetAll()

        End Function

        Public Function GetByPK() As Emp_Allowed_GPSCoordinates

            Dim dr As DataRow
            dr = objDALEmp_Allowed_GPSCoordinates.GetByPK(_AllowedGPSId)

            If Not IsDBNull(dr("AllowedGPSId")) Then
                _AllowedGPSId = dr("AllowedGPSId")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("LocationName")) Then
                _LocationName = dr("LocationName")
            End If
            If Not IsDBNull(dr("LocationArabicName")) Then
                _LocationArabicName = dr("LocationArabicName")
            End If
            If Not IsDBNull(dr("GPS_Coordinates")) Then
                _GPS_Coordinates = dr("GPS_Coordinates")
            End If
            If Not IsDBNull(dr("Radius")) Then
                _Radius = dr("Radius")
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

        Public Function GetAll_Inner() As DataTable

            Return objDALEmp_Allowed_GPSCoordinates.GetAll_Inner()

        End Function

#End Region

    End Class
End Namespace