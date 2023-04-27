Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA_AnnouncementsTemplates

    Public Class AnnouncementTemplates

#Region "Class Variables"


        Private _TemplateId As Integer
        Private _announcementType As Integer
        Private _FK_HolidayId As Integer
        Private _FK_leaveType As Integer
        Private _AtLeaveStart As Boolean
        Private _TitleEn As String
        Private _TitleAr As String
        Private _TextEn As String
        Private _TextAr As String
        Private _LeaveType As String
        Private _LeaveTypeAr As String
        Private _HolidayType As String
        Private _HolidayTypeAr As String

        Private objDALAnnouncementTemplates As DALAnnouncementTemplates

#End Region

#Region "Public Properties"


        Public Property TemplateId() As Integer
            Set(ByVal value As Integer)
                _TemplateId = value
            End Set
            Get
                Return (_TemplateId)
            End Get
        End Property


        Public Property announcementType() As Integer
            Set(ByVal value As Integer)
                _announcementType = value
            End Set
            Get
                Return (_announcementType)
            End Get
        End Property


        Public Property FK_HolidayId() As Integer
            Set(ByVal value As Integer)
                _FK_HolidayId = value
            End Set
            Get
                Return (_FK_HolidayId)
            End Get
        End Property


        Public Property FK_leaveType() As Integer
            Set(ByVal value As Integer)
                _FK_leaveType = value
            End Set
            Get
                Return (_FK_leaveType)
            End Get
        End Property


        Public Property AtLeaveStart() As Boolean
            Set(ByVal value As Boolean)
                _AtLeaveStart = value
            End Set
            Get
                Return (_AtLeaveStart)
            End Get
        End Property


        Public Property TitleEn() As String
            Set(ByVal value As String)
                _TitleEn = value
            End Set
            Get
                Return (_TitleEn)
            End Get
        End Property


        Public Property TitleAr() As String
            Set(ByVal value As String)
                _TitleAr = value
            End Set
            Get
                Return (_TitleAr)
            End Get
        End Property


        Public Property TextEn() As String
            Set(ByVal value As String)
                _TextEn = value
            End Set
            Get
                Return (_TextEn)
            End Get
        End Property


        Public Property TextAr() As String
            Set(ByVal value As String)
                _TextAr = value
            End Set
            Get
                Return (_TextAr)
            End Get
        End Property
        Public Property LeaveType() As String
            Set(ByVal value As String)
                _LeaveType = value
            End Set
            Get
                Return (_LeaveType)
            End Get
        End Property
        Public Property LeaveTypeAr() As String
            Set(ByVal value As String)
                _LeaveTypeAr = value
            End Set
            Get
                Return (_LeaveTypeAr)
            End Get
        End Property
        Public Property HolidayType() As String
            Set(ByVal value As String)
                _HolidayType = value
            End Set
            Get
                Return (_HolidayType)
            End Get
        End Property
        Public Property HolidayTypeAr() As String
            Set(ByVal value As String)
                _HolidayTypeAr = value
            End Set
            Get
                Return (_HolidayTypeAr)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALAnnouncementTemplates = New DALAnnouncementTemplates()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALAnnouncementTemplates.Add(_TemplateId, _announcementType, _FK_HolidayId, _FK_leaveType, _AtLeaveStart, _TitleEn, _TitleAr, _TextEn, _TextAr)
            App_EventsLog.Insert_ToEventLog("Add", _TemplateId, "AnnouncementTemplates", "Announcement Templates")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALAnnouncementTemplates.Update(_TemplateId, _announcementType, _FK_HolidayId, _FK_leaveType, _AtLeaveStart, _TitleEn, _TitleAr, _TextEn, _TextAr)
            App_EventsLog.Insert_ToEventLog("Update", _TemplateId, "AnnouncementTemplates", "Announcement Templates")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALAnnouncementTemplates.Delete(_TemplateId)
            App_EventsLog.Insert_ToEventLog("Delete", _TemplateId, "AnnouncementTemplates", "Announcement Templates")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALAnnouncementTemplates.GetAll()

        End Function

        Public Function GetByPK() As AnnouncementTemplates

            Dim dr As DataRow
            dr = objDALAnnouncementTemplates.GetByPK(_TemplateId)

            If Not IsDBNull(dr("TemplateId")) Then
                _TemplateId = dr("TemplateId")
            End If
            If Not IsDBNull(dr("announcementType")) Then
                _announcementType = dr("announcementType")
            End If
            If Not IsDBNull(dr("FK_HolidayId")) Then
                _FK_HolidayId = dr("FK_HolidayId")
            End If
            If Not IsDBNull(dr("FK_leaveType")) Then
                _FK_leaveType = dr("FK_leaveType")
            End If
            If Not IsDBNull(dr("AtLeaveStart")) Then
                _AtLeaveStart = dr("AtLeaveStart")
            End If
            If Not IsDBNull(dr("TitleEn")) Then
                _TitleEn = dr("TitleEn")
            End If
            If Not IsDBNull(dr("TitleAr")) Then
                _TitleAr = dr("TitleAr")
            End If
            If Not IsDBNull(dr("TextEn")) Then
                _TextEn = dr("TextEn")
            End If
            If Not IsDBNull(dr("TextAr")) Then
                _TextAr = dr("TextAr")
            End If
            If Not IsDBNull(dr("LeaveType")) Then
                _LeaveType = dr("LeaveType")
            End If
            If Not IsDBNull(dr("LeaveTypeAr")) Then
                _LeaveTypeAr = dr("LeaveTypeAr")
            End If
            If Not IsDBNull(dr("HolidayType")) Then
                _HolidayType = dr("HolidayType")
            End If
            If Not IsDBNull(dr("HolidayTypeAr")) Then
                _HolidayTypeAr = dr("HolidayTypeAr")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace