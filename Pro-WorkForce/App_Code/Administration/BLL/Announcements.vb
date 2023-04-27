Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA_Announcements

    Public Class Announcements

#Region "Class Variables"


        Private _ID As Integer
        Private _IsSpecificDate As Boolean
        Private _AnnouncementDate As DateTime
        Private _IsYearlyFixed As Boolean
        Private _FromDate As DateTime
        Private _ToDate As DateTime
        Private _Title_En As String
        Private _Title_Ar As String
        Private _Content_En As String
        Private _Content_Ar As String
        Private _Created_By As String
        Private _Created_Date As DateTime
        Private _Altered_By As String
        Private _Altered_Date As DateTime
        Private _Fk_CompanyId As Integer
        Private _Fk_EntityId As Integer
        Private _Fk_WorklocationId As Integer
        Private _Fk_LogicalGroupId As Integer
        Private _Fk_EmployeeId As Long
        Private _MonthNo As Integer
        Private _DayNo As Integer
        Private _MonthEnglish As String
        Private _MonthArabic As String


        Private _EmployeeNo As String
        Private _EmployeeName As String
        Private _EmployeeArabicName As String
        Private _EntityName As String
        Private _EntityArabicName As String
        Private _CompanyName As String
        Private _CompanyArabicName As String
        Private _TemplateId As Integer
        Private _LanguageSelection As Integer
        Private _SendStatus As Boolean


        Private objDALAnnouncements As DALAnnouncements

#End Region

#Region "Public Properties"

        Public Property ID() As Integer
            Set(ByVal value As Integer)
                _ID = value
            End Set
            Get
                Return (_ID)
            End Get
        End Property
        Public Property IsSpecificDate() As Boolean
            Set(ByVal value As Boolean)
                _IsSpecificDate = value
            End Set
            Get
                Return (_IsSpecificDate)
            End Get
        End Property
        Public Property AnnouncementDate() As DateTime
            Set(ByVal value As DateTime)
                _AnnouncementDate = value
            End Set
            Get
                Return (_AnnouncementDate)
            End Get
        End Property
        Public Property IsYearlyFixed() As Boolean
            Set(ByVal value As Boolean)
                _IsYearlyFixed = value
            End Set
            Get
                Return (_IsYearlyFixed)
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
        Public Property Title_En() As String
            Set(ByVal value As String)
                _Title_En = value
            End Set
            Get
                Return (_Title_En)
            End Get
        End Property
        Public Property Title_Ar() As String
            Set(ByVal value As String)
                _Title_Ar = value
            End Set
            Get
                Return (_Title_Ar)
            End Get
        End Property
        Public Property Content_En() As String
            Set(ByVal value As String)
                _Content_En = value
            End Set
            Get
                Return (_Content_En)
            End Get
        End Property
        Public Property Content_Ar() As String
            Set(ByVal value As String)
                _Content_Ar = value
            End Set
            Get
                Return (_Content_Ar)
            End Get
        End Property
        Public Property Created_By() As String
            Set(ByVal value As String)
                _Created_By = value
            End Set
            Get
                Return (_Created_By)
            End Get
        End Property
        Public Property Created_Date() As DateTime
            Set(ByVal value As DateTime)
                _Created_Date = value
            End Set
            Get
                Return (_Created_Date)
            End Get
        End Property
        Public Property Altered_By() As String
            Set(ByVal value As String)
                _Altered_By = value
            End Set
            Get
                Return (_Altered_By)
            End Get
        End Property
        Public Property Altered_Date() As DateTime
            Set(ByVal value As DateTime)
                _Altered_Date = value
            End Set
            Get
                Return (_Altered_Date)
            End Get
        End Property
        Public Property Fk_CompanyId() As Integer
            Set(ByVal value As Integer)
                _Fk_CompanyId = value
            End Set
            Get
                Return (_Fk_CompanyId)
            End Get
        End Property
        Public Property Fk_EntityId() As Integer?
            Set(ByVal value As Integer?)
                _Fk_EntityId = value
            End Set
            Get
                Return (_Fk_EntityId)
            End Get
        End Property
        Public Property Fk_WorklocationId() As Integer?
            Set(ByVal value As Integer?)
                _Fk_WorklocationId = value
            End Set
            Get
                Return (_Fk_WorklocationId)
            End Get
        End Property
        Public Property Fk_LogicalGroupId() As Integer?
            Set(ByVal value As Integer?)
                _Fk_LogicalGroupId = value
            End Set
            Get
                Return (_Fk_LogicalGroupId)
            End Get
        End Property
        Public Property Fk_EmployeeId() As Long
            Set(ByVal value As Long)
                _Fk_EmployeeId = value
            End Set
            Get
                Return (_Fk_EmployeeId)
            End Get
        End Property
        Public Property EmployeeNo() As String
            Set(ByVal value As String)
                _EmployeeNo = value
            End Set
            Get
                Return (_EmployeeNo)
            End Get
        End Property
        Public Property EmployeeName() As String
            Set(ByVal value As String)
                _EmployeeName = value
            End Set
            Get
                Return (_EmployeeName)
            End Get
        End Property
        Public Property EmployeeArabicName() As String
            Set(ByVal value As String)
                _EmployeeArabicName = value
            End Set
            Get
                Return (_EmployeeArabicName)
            End Get
        End Property
        Public Property EntityName() As String
            Set(ByVal value As String)
                _EntityName = value
            End Set
            Get
                Return (_EntityName)
            End Get
        End Property
        Public Property EntityArabicName() As String
            Set(ByVal value As String)
                _EntityArabicName = value
            End Set
            Get
                Return (_EntityArabicName)
            End Get
        End Property
        Public Property CompanyName() As String
            Set(ByVal value As String)
                _CompanyName = value
            End Set
            Get
                Return (_CompanyName)
            End Get
        End Property
        Public Property CompanyArabicName() As String
            Set(ByVal value As String)
                _CompanyArabicName = value
            End Set
            Get
                Return (_CompanyArabicName)
            End Get
        End Property
        Public Property DayNo() As Integer
            Set(ByVal value As Integer)
                _DayNo = value
            End Set
            Get
                Return (_DayNo)
            End Get
        End Property
        Public Property MonthNo() As Integer
            Set(ByVal value As Integer)
                _MonthNo = value
            End Set
            Get
                Return (_MonthNo)
            End Get
        End Property
        Public Property MonthEnglish() As String
            Set(ByVal value As String)
                _MonthEnglish = value
            End Set
            Get
                Return (_MonthEnglish)
            End Get
        End Property
        Public Property MonthArabic() As String
            Set(ByVal value As String)
                _MonthArabic = value
            End Set
            Get
                Return (_MonthArabic)
            End Get
        End Property
        Public Property TemplateId() As Integer
            Set(ByVal value As Integer)
                _TemplateId = value
            End Set
            Get
                Return (_TemplateId)
            End Get
        End Property
        Public Property LanguageSelection() As Integer
            Set(ByVal value As Integer)
                _LanguageSelection = value
            End Set
            Get
                Return (_LanguageSelection)
            End Get
        End Property
        Public Property SendStatus() As Boolean
            Set(ByVal value As Boolean)
                _SendStatus = value
            End Set
            Get
                Return (_SendStatus)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALAnnouncements = New DALAnnouncements()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALAnnouncements.Add(_ID, _IsSpecificDate, _AnnouncementDate, _IsYearlyFixed, _FromDate, _ToDate, _Title_En, _Title_Ar, _Fk_WorklocationId, _Fk_CompanyId, _Fk_EmployeeId, _Fk_EntityId, _Fk_LogicalGroupId, _Content_En, _Content_Ar, _Created_By, _LanguageSelection)
            Dim morelogdetails As String
            morelogdetails = "EmployeeId =" + _Fk_EmployeeId.ToString() + "  , CompanyId=" + _Fk_CompanyId.ToString() + ", AnnouncementDate=" + _AnnouncementDate.ToString("dd-MM-yyyy")
            App_EventsLog.Insert_ToEventLog("Add", _Fk_EmployeeId, "Announcements", "Announcements", morelogdetails)
            Return rslt
        End Function
        Public Function Update() As Integer

            Dim rslt As Integer = objDALAnnouncements.Update(_ID, _IsSpecificDate, _AnnouncementDate, _IsYearlyFixed, _FromDate, _ToDate, _Title_En, _Title_Ar, _Fk_WorklocationId, _Fk_CompanyId, _Fk_EmployeeId, _Fk_EntityId, _Fk_LogicalGroupId, _Content_En, _Content_Ar, _Altered_By, _LanguageSelection)
            Dim morelogdetails As String
            morelogdetails = "EmployeeId =" + _Fk_EmployeeId.ToString() + "  , CompanyId=" + _Fk_CompanyId.ToString() + ", AnnouncementDate=" + _AnnouncementDate.ToString("dd-MM-yyyy")
            App_EventsLog.Insert_ToEventLog("Edit", _Fk_EmployeeId, "Announcements", "Announcements", morelogdetails)
            Return rslt
        End Function
        Public Function Delete() As Integer

            Dim rslt As Integer = objDALAnnouncements.Delete(_ID)
            Dim morelogdetails As String
            morelogdetails = "EmployeeId =" + _Fk_EmployeeId.ToString() + "  , CompanyId=" + _Fk_CompanyId.ToString() + ", AnnouncementDate=" + _AnnouncementDate.ToString("dd-MM-yyyy")
            App_EventsLog.Insert_ToEventLog("Delete", _Fk_EmployeeId, "Announcements", "Announcements", morelogdetails)
            Return rslt
        End Function
        Public Function GetAll() As DataTable

            Return objDALAnnouncements.GetAll()

        End Function
        Public Function GetTop5(userid) As DataTable

            Return objDALAnnouncements.GetTopFive(userid)

        End Function
        Public Function GetByPK() As Announcements

            Dim dr As DataRow
            dr = objDALAnnouncements.GetByPK(_ID)

            If Not IsDBNull(dr("ID")) Then
                _ID = dr("ID")
            End If
            If Not IsDBNull(dr("IsSpecificDate")) Then
                _IsSpecificDate = dr("IsSpecificDate")
            End If
            If Not IsDBNull(dr("AnnouncementDate")) Then
                _AnnouncementDate = dr("AnnouncementDate")
            End If
            If Not IsDBNull(dr("IsYearlyFixed")) Then
                _IsYearlyFixed = dr("IsYearlyFixed")
            End If
            If Not IsDBNull(dr("FromDate")) Then
                _FromDate = dr("FromDate")
            End If
            If Not IsDBNull(dr("ToDate")) Then
                _ToDate = dr("ToDate")
            End If
            If Not IsDBNull(dr("Title_En")) Then
                _Title_En = dr("Title_En")
            End If
            If Not IsDBNull(dr("Title_Ar")) Then
                _Title_Ar = dr("Title_Ar")
            End If
            If Not IsDBNull(dr("Content_En")) Then
                _Content_En = dr("Content_En")
            End If
            If Not IsDBNull(dr("Content_Ar")) Then
                _Content_Ar = dr("Content_Ar")
            End If
            If Not IsDBNull(dr("FK_CompanyId")) Then
                _Fk_CompanyId = dr("FK_CompanyId")
            End If
            If Not IsDBNull(dr("FK_EntityId")) Then
                _Fk_EntityId = dr("FK_EntityId")
            End If
            If Not IsDBNull(dr("FK_workLocationId")) Then
                _Fk_WorklocationId = dr("FK_workLocationId")
            End If
            If Not IsDBNull(dr("FK_LogicalGroupId")) Then
                _Fk_LogicalGroupId = dr("FK_LogicalGroupId")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _Fk_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("Created_By")) Then
                _Created_By = dr("Created_By")
            End If
            If Not IsDBNull(dr("Created_Date")) Then
                _Created_Date = dr("Created_Date")
            End If
            If Not IsDBNull(dr("Altered_By")) Then
                _Altered_By = dr("Altered_By")
            End If
            If Not IsDBNull(dr("Altered_Date")) Then
                _Altered_Date = dr("Altered_Date")
            End If
            If Not IsDBNull(dr("TemplateId")) Then
                _TemplateId = dr("TemplateId")
            End If
            If Not IsDBNull(dr("LanguageSelection")) Then
                _LanguageSelection = dr("LanguageSelection")
            End If
            If Not IsDBNull(dr("SendStatus")) Then
                _SendStatus = dr("SendStatus")
            End If

            If Not IsDBNull(dr("DayNo")) Then
                _DayNo = dr("DayNo")
            End If
            If Not IsDBNull(dr("MonthEnglish")) Then
                _MonthEnglish = dr("MonthEnglish")
            End If
            If Not IsDBNull(dr("MonthArabic")) Then
                _MonthArabic = dr("MonthArabic")
            End If

            Return Me
        End Function
        Public Function GetTop5AnnouncementsSelfServices(userid, language) As DataTable

            Return objDALAnnouncements.GetTop5AnnouncementsSelfServices(userid, language)

        End Function

#End Region

    End Class
End Namespace