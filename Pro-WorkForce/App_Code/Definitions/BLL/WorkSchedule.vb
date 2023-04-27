Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Definitions

    Public Class WorkSchedule

#Region "Class Variables"

        Private _ScheduleId As Integer
        Private _ScheduleName As String
        Private _ScheduleArabicName As String
        Private _ScheduleType As Integer
        Private _GraceIn As Integer
        Private _GraceOut As Integer
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALWorkSchedule As DALWorkSchedule
        Private _ScheduleTypeName As String
        Private _EmployeeId As Integer
        Private _CompanyId As Integer
        Private _EntityId As Integer
        Private _ScheduleDate As DateTime
        Private _IsDefault As Boolean
        Private _FilterType As String = "C"
        Private _IsRamadanSch As Boolean
        Private _ParentSchId As Integer
        Private _DayId As Integer
        Private _FromTime1 As String
        Private _FromTime2 As String
        Private _ToTime1 As String
        Private _ToTime2 As String
        Private _FromTime3 As String
        Private _FromTime4 As String
        Private _Duration1 As String
        Private _Duration2 As String
        Private _FromDate As DateTime?
        Private _ToDate As Date
        Private _MinimumAllowTime As Integer
        Private _GraceInGender As String
        Private _GraceOutGender As String
        Private _ConsiderShiftScheduleAtEnd As Boolean?
        Private _StudyNursing_Schedule As Integer '----Custom For ADM
        Private _IsActive As Boolean

#End Region

#Region "Public Properties"
       
        Public Property EntityId() As Integer
            Set(ByVal value As Integer)
                _EntityId = value
            End Set
            Get
                Return (_EntityId)
            End Get
        End Property

        Public Property CompanyId() As Integer
            Set(ByVal value As Integer)
                _CompanyId = value
            End Set
            Get
                Return (_CompanyId)
            End Get
        End Property

        Public Property ScheduleDate() As DateTime
            Set(ByVal value As DateTime)
                _ScheduleDate = value
            End Set
            Get
                Return (_ScheduleDate)
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

        Public Property ScheduleId() As Integer
            Set(ByVal value As Integer)
                _ScheduleId = value
            End Set
            Get
                Return (_ScheduleId)
            End Get
        End Property

        Public Property ScheduleName() As String
            Set(ByVal value As String)
                _ScheduleName = value
            End Set
            Get
                Return (_ScheduleName)
            End Get
        End Property

        Public Property ScheduleArabicName() As String
            Set(ByVal value As String)
                _ScheduleArabicName = value
            End Set
            Get
                Return (_ScheduleArabicName)
            End Get
        End Property

        Public Property ScheduleType() As Integer
            Set(ByVal value As Integer)
                _ScheduleType = value
            End Set
            Get
                Return (_ScheduleType)
            End Get
        End Property

        Public Property ScheduleTypeName() As String
            Set(ByVal value As String)
                _ScheduleTypeName = value
            End Set
            Get
                Return (_ScheduleTypeName)
            End Get
        End Property

        Public Property GraceIn() As Integer
            Set(ByVal value As Integer)
                _GraceIn = value
            End Set
            Get
                Return (_GraceIn)
            End Get
        End Property

        Public Property GraceOut() As Integer
            Set(ByVal value As Integer)
                _GraceOut = value
            End Set
            Get
                Return (_GraceOut)
            End Get
        End Property

        Public Property IsDefault() As Boolean
            Set(ByVal value As Boolean)
                _IsDefault = value
            End Set
            Get
                Return (_IsDefault)
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

        Public Property FilterType() As String
            Set(ByVal value As String)
                _FilterType = value
            End Set
            Get
                Return (_FilterType)
            End Get
        End Property

        Public Property IsRamadanSch() As Boolean
            Get
                Return _IsRamadanSch
            End Get
            Set(ByVal value As Boolean)
                _IsRamadanSch = value
            End Set
        End Property

        Public Property ParentSchId() As Integer
            Get
                Return _ParentSchId
            End Get
            Set(ByVal value As Integer)
                _ParentSchId = value
            End Set
        End Property

        Public Property DayId() As Integer
            Set(ByVal value As Integer)
                _DayId = value
            End Set
            Get
                Return (_DayId)
            End Get
        End Property

        Public Property FromTime1() As String
            Set(ByVal value As String)
                _FromTime1 = value
            End Set
            Get
                Return (_FromTime1)
            End Get
        End Property

        Public Property FromTime2() As String
            Set(ByVal value As String)
                _FromTime2 = value
            End Set
            Get
                Return (_FromTime2)
            End Get
        End Property

        Public Property ToTime1() As String
            Set(ByVal value As String)
                _ToTime1 = value
            End Set
            Get
                Return (_ToTime1)
            End Get
        End Property

        Public Property ToTime2() As String
            Set(ByVal value As String)
                _ToTime2 = value
            End Set
            Get
                Return (_ToTime2)
            End Get
        End Property

        Public Property FromTime3() As String
            Set(ByVal value As String)
                _FromTime3 = value
            End Set
            Get
                Return (_FromTime3)
            End Get
        End Property

        Public Property FromTime4() As String
            Set(ByVal value As String)
                _FromTime4 = value
            End Set
            Get
                Return (_FromTime4)
            End Get
        End Property

        Public Property Duration1() As String
            Set(ByVal value As String)
                _Duration1 = value
            End Set
            Get
                Return (_Duration1)
            End Get
        End Property

        Public Property Duration2() As String
            Set(ByVal value As String)
                _Duration2 = value
            End Set
            Get
                Return (_Duration2)
            End Get
        End Property

        Public Property FromDate() As DateTime?
            Set(ByVal value As DateTime?)
                _FromDate = value
            End Set
            Get
                Return (_FromDate)
            End Get
        End Property

        Public Property MinimumAllowTime() As Integer
            Set(ByVal value As Integer)
                _MinimumAllowTime = value
            End Set
            Get
                Return (_MinimumAllowTime)
            End Get
        End Property

        Public Property GraceInGender() As String
            Set(ByVal value As String)
                _GraceInGender = value
            End Set
            Get
                Return (_GraceInGender)
            End Get
        End Property

        Public Property GraceOutGender() As String
            Set(ByVal value As String)
                _GraceOutGender = value
            End Set
            Get
                Return (_GraceOutGender)
            End Get
        End Property

        Public Property ConsiderShiftScheduleAtEnd() As Boolean?
            Set(ByVal value As Boolean?)
                _ConsiderShiftScheduleAtEnd = value
            End Set
            Get
                Return (_ConsiderShiftScheduleAtEnd)
            End Get
        End Property

        Public Property StudyNursing_Schedule() As Integer
            Set(ByVal value As Integer)
                _StudyNursing_Schedule = value
            End Set
            Get
                Return (_StudyNursing_Schedule)
            End Get
        End Property

        Public Property IsActive() As Boolean
            Set(ByVal value As Boolean)
                _IsActive = value
            End Set
            Get
                Return (_IsActive)
            End Get
        End Property

#End Region

#Region "Constructor"

        Public Sub New()

            objDALWorkSchedule = New DALWorkSchedule()

        End Sub

#End Region

#Region "Methods"

        Public Function generateLogDesc(ByVal ParamArray b() As String) As String
            Dim y As StringBuilder
            Dim i As Integer
            For i = 0 To b.Length - 1
                y.Append(b(i).ToString)
                y.Append(", ")

            Next
            Return y.ToString()
        End Function

        Public Function Add() As Integer
            Dim rslt As Integer = objDALWorkSchedule.Add(_ScheduleName, _ScheduleArabicName, _ScheduleType, _GraceIn, _GraceOut, _IsDefault, _CREATED_BY, _ScheduleId, _IsRamadanSch, _ParentSchId, _MinimumAllowTime, _GraceInGender, _GraceOutGender, _ConsiderShiftScheduleAtEnd, _IsActive)
            App_EventsLog.Insert_ToEventLog("Add", _ScheduleId, "WorkSchedule", "Define Work Schedule")
            Return rslt
        End Function

        Public Function Update() As Integer
            Dim rslt As Integer = objDALWorkSchedule.Update(_ScheduleId, _ScheduleName, _ScheduleArabicName, _ScheduleType, _GraceIn, _GraceOut, _LAST_UPDATE_BY, _IsDefault, _IsRamadanSch, _ParentSchId, _MinimumAllowTime, _GraceInGender, _GraceOutGender, _ConsiderShiftScheduleAtEnd, _IsActive)
            App_EventsLog.Insert_ToEventLog("Edit", _ScheduleId, "WorkSchedule", "Define Work Schedule")
            Return rslt
        End Function

        Public Function Delete() As Integer
            Dim rslt As Integer = objDALWorkSchedule.Delete(_ScheduleId)
            App_EventsLog.Insert_ToEventLog("Delete", _ScheduleId, "WorkSchedule", "Define Work Schedule")
            Return rslt
        End Function

        Public Function GetAllFORDDL() As DataTable

            Return objDALWorkSchedule.GetAllForDDL()

        End Function

        Public Function GetAll() As DataTable
            Return objDALWorkSchedule.GetAll()
        End Function

        Public Function GetAllFlexible() As DataTable
            Return objDALWorkSchedule.GetAllFlexible()
        End Function

        Public Function GetAllByType() As DataTable
            Return objDALWorkSchedule.GetAllByType(_ScheduleType)
        End Function

        Public Function GetActive_SchedulebyEmpId_row(ByVal ScheduleDate As DateTime) As WorkSchedule
            Dim dr As DataRow
            dr = objDALWorkSchedule.GetActive_SchedulebyEmpId_row(_EmployeeId, ScheduleDate)
            If Not dr Is Nothing Then

                If Not IsDBNull(dr("ScheduleName")) Then
                    _ScheduleName = dr("ScheduleName")
                End If

                If Not IsDBNull(dr("ScheduleArabicName")) Then
                    _ScheduleArabicName = dr("ScheduleArabicName")
                End If

                If Not IsDBNull(dr("ScheduleType")) Then
                    _ScheduleType = dr("ScheduleType")
                End If

                If Not IsDBNull(dr("ScheduleId")) Then
                    _ScheduleId = dr("ScheduleId")
                End If

                If Not IsDBNull(dr("FromDate")) Then
                    _FromDate = dr("FromDate")
                End If

            End If
            Return Me
        End Function

        Public Function GetEmployeeActiveSchedule() As DataTable
            Return objDALWorkSchedule.GetEmployeeActiveSchedule(_CompanyId, _EntityId, _ScheduleDate, _FilterType)
        End Function

        Public Function GetEmployeeActiveScheduleByEmpId(ByVal ScheduleDate As DateTime) As DataTable
            Return objDALWorkSchedule.GetEmployeeActiveScheduleByEmpId(_EmployeeId, ScheduleDate)
        End Function

        Public Function GetByType(ByVal ScheduleType As Integer) As DataTable
            Return objDALWorkSchedule.GetByType(ScheduleType)

        End Function

        Public Function GetByPK() As WorkSchedule

            Dim dr As DataRow
            dr = objDALWorkSchedule.GetByPK(_ScheduleId)

            If Not IsDBNull(dr("ScheduleId")) Then
                _ScheduleId = dr("ScheduleId")
            End If
            If Not IsDBNull(dr("ScheduleName")) Then
                _ScheduleName = dr("ScheduleName")
            End If
            If Not IsDBNull(dr("ScheduleArabicName")) Then
                _ScheduleArabicName = dr("ScheduleArabicName")
            End If
            If Not IsDBNull(dr("ScheduleType")) Then
                _ScheduleType = dr("ScheduleType")
            End If
            If Not IsDBNull(dr("ScheduleTypeName")) Then
                _ScheduleTypeName = dr("ScheduleTypeName")
            End If
            If Not IsDBNull(dr("GraceIn")) Then
                _GraceIn = dr("GraceIn")
            End If
            If Not IsDBNull(dr("GraceOut")) Then
                _GraceOut = dr("GraceOut")
            End If
            If Not IsDBNull(dr("IsDefault")) Then
                _IsDefault = dr("IsDefault")
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
            If Not IsDBNull(dr("IsRamadanSch")) Then
                _IsRamadanSch = dr("IsRamadanSch")
            End If
            If Not IsDBNull(dr("ParentSchId")) Then
                _ParentSchId = dr("ParentSchId")
            End If
            If Not IsDBNull(dr("MinimumAllowTime")) Then
                _MinimumAllowTime = dr("MinimumAllowTime")
            End If
            If Not IsDBNull(dr("GraceInGender")) Then
                _GraceInGender = dr("GraceInGender")
            End If
            If Not IsDBNull(dr("GraceOutGender")) Then
                _GraceOutGender = dr("GraceOutGender")
            End If
            If Not IsDBNull(dr("ConsiderShiftScheduleAtEnd")) Then
                _ConsiderShiftScheduleAtEnd = dr("ConsiderShiftScheduleAtEnd")
            End If
            If Not IsDBNull(dr("IsActive")) Then
                _IsActive = dr("IsActive")
            End If
            Return Me
        End Function

        Public Function GetNormal_Flexible() As DataTable
            Return objDALWorkSchedule.GetNormal_Flexible()
        End Function

        Public Function CheckDefaultSchedule() As Integer
            Return objDALWorkSchedule.CheckDefaultSchedule(_ScheduleId)
        End Function

        Public Function UpdateIsdefault() As Integer

            Return objDALWorkSchedule.UpdateIsdefault(_ScheduleId, _IsDefault)

        End Function

        Public Function GetByDefault() As WorkSchedule

            Dim dr As DataRow
            dr = objDALWorkSchedule.GetByDefault()

            If (Not dr Is Nothing) Then
                If Not IsDBNull(dr("ScheduleId")) Then
                    _ScheduleId = dr("ScheduleId")
                End If
                If Not IsDBNull(dr("ScheduleName")) Then
                    _ScheduleName = dr("ScheduleName")
                End If
                If Not IsDBNull(dr("ScheduleArabicName")) Then
                    _ScheduleArabicName = dr("ScheduleArabicName")
                End If
                If Not IsDBNull(dr("ScheduleType")) Then
                    _ScheduleType = dr("ScheduleType")
                End If
                If Not IsDBNull(dr("ScheduleTypeName")) Then
                    _ScheduleTypeName = dr("ScheduleTypeName")
                End If
                If Not IsDBNull(dr("GraceIn")) Then
                    _GraceIn = dr("GraceIn")
                End If
                If Not IsDBNull(dr("GraceOut")) Then
                    _GraceOut = dr("GraceOut")
                End If
                If Not IsDBNull(dr("IsDefault")) Then
                    _IsDefault = dr("IsDefault")
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
            End If

            Return Me
        End Function

        Public Function GetByParentId() As WorkSchedule

            Dim dr As DataRow
            dr = objDALWorkSchedule.GetByParentId(_ParentSchId)

            If Not dr Is Nothing Then

                If Not IsDBNull(dr("ScheduleId")) Then
                    _ScheduleId = dr("ScheduleId")
                End If
                If Not IsDBNull(dr("ScheduleName")) Then
                    _ScheduleName = dr("ScheduleName")
                End If
                If Not IsDBNull(dr("ScheduleArabicName")) Then
                    _ScheduleArabicName = dr("ScheduleArabicName")
                End If
                If Not IsDBNull(dr("ScheduleType")) Then
                    _ScheduleType = dr("ScheduleType")
                End If
                If Not IsDBNull(dr("ScheduleTypeName")) Then
                    _ScheduleTypeName = dr("ScheduleTypeName")
                End If
                If Not IsDBNull(dr("GraceIn")) Then
                    _GraceIn = dr("GraceIn")
                End If
                If Not IsDBNull(dr("GraceOut")) Then
                    _GraceOut = dr("GraceOut")
                End If
                If Not IsDBNull(dr("IsDefault")) Then
                    _IsDefault = dr("IsDefault")
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
                If Not IsDBNull(dr("IsRamadanSch")) Then
                    _IsRamadanSch = dr("IsRamadanSch")
                End If
                If Not IsDBNull(dr("ParentSchId")) Then
                    _ParentSchId = dr("ParentSchId")
                End If

            End If

            Return Me

        End Function

        Public Function GetScheduleTime_Normal() As DataRow
            Dim dr As DataRow
            dr = objDALWorkSchedule.GetScheduleTime_Normal(_ScheduleId, _DayId)
            If Not dr Is Nothing Then
                If Not IsDBNull(dr("FromTime1")) Then
                    _FromTime1 = dr("FromTime1")
                End If
                If Not IsDBNull(dr("FromTime2")) Then
                    _FromTime2 = dr("FromTime2")
                End If
                If Not IsDBNull(dr("ToTime1")) Then
                    _ToTime1 = dr("ToTime1")
                End If
                If Not IsDBNull(dr("ToTime2")) Then
                    _ToTime2 = dr("ToTime2")
                End If
            End If
            Return dr
        End Function

        Public Function GetScheduleTime_Flexible() As DataRow
            Dim dr As DataRow
            dr = objDALWorkSchedule.GetScheduleTime_Flexible(_ScheduleId, _DayId)
            If Not dr Is Nothing Then
                If Not IsDBNull(dr("FromTime1")) Then
                    _FromTime1 = dr("FromTime1")
                End If
                If Not IsDBNull(dr("FromTime2")) Then
                    _FromTime2 = dr("FromTime2")
                End If
                If Not IsDBNull(dr("Duration1")) Then
                    _Duration1 = dr("Duration1")
                End If
                If Not IsDBNull(dr("FromTime3")) Then
                    _FromTime3 = dr("FromTime3")
                End If
                If Not IsDBNull(dr("FromTime4")) Then
                    _FromTime4 = dr("FromTime4")
                End If
                If Not IsDBNull(dr("Duration2")) Then
                    _Duration2 = dr("Duration2")
                End If
            End If
            Return dr
        End Function

        Public Function Get_ByStudyNursing_Schedule() As DataTable
            Return objDALWorkSchedule.Get_ByStudyNursing_Schedule(_StudyNursing_Schedule)
        End Function

      
#End Region

    End Class
End Namespace