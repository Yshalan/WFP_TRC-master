Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events
Imports SmartV.UTILITIES

Namespace TA.DailyTasks

    Public Class Emp_Move

#Region "Class Variables"


        Private _MoveId As Long
        Private _FK_EmployeeId As Long
        Private _Type As String
        Private _MoveDate As DateTime
        Private _MoveTime As DateTime
        Private _FK_ReasonId As Integer
        Private _Remarks As String
        Private _Reader As String
        Private _M_DATE_NUM As String
        Private _M_TIME_NUM As String
        Private _Status As String
        Private _SYS_Date As DateTime
        Private _IsManual As Boolean
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALEmp_Move As DALEmp_Move
        Private objRECALC_REQUEST As RECALC_REQUEST
        Private objREADER_KEYS As READER_KEYS
        Private _IsFromInvalid As Boolean
        Private _AttachedFile As String
        Private _Id As Integer
        Private _FK_EntityId As Integer
        Private _FK_CompanyId As Integer
        Private _IsRemoteWork As Boolean
        Private _ManualEntryAllowedBefore As String
        Private _IsFromMobile As Boolean
        Private _MobileCoordinates As String
        Private _workLocationId As Integer
        Private _DeviceId As String
        Private _BeaconId As Integer
        Private _IsFromBeacon As Boolean
        Private _AllowedGPSType As Integer

#End Region

#Region "Public Properties"

        Public Property MoveId() As Long
            Set(ByVal value As Long)
                _MoveId = value
            End Set
            Get
                Return (_MoveId)
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

        Public Property Type() As String
            Set(ByVal value As String)
                _Type = value
            End Set
            Get
                Return (_Type)
            End Get
        End Property

        Public Property MoveDate() As DateTime
            Set(ByVal value As DateTime)
                _MoveDate = value
            End Set
            Get
                Return (_MoveDate)
            End Get
        End Property

        Public Property MoveTime() As DateTime
            Set(ByVal value As DateTime)
                _MoveTime = value
            End Set
            Get
                Return (_MoveTime)
            End Get
        End Property

        Public Property FK_ReasonId() As Integer
            Set(ByVal value As Integer)
                _FK_ReasonId = value
            End Set
            Get
                Return (_FK_ReasonId)
            End Get
        End Property

        Public Property Remarks() As String
            Set(ByVal value As String)
                _Remarks = value
            End Set
            Get
                Return (_Remarks)
            End Get
        End Property

        Public Property Reader() As String
            Set(ByVal value As String)
                _Reader = value
            End Set
            Get
                Return (_Reader)
            End Get
        End Property

        Public Property M_DATE_NUM() As String
            Set(ByVal value As String)
                _M_DATE_NUM = value
            End Set
            Get
                Return (_M_DATE_NUM)
            End Get
        End Property

        Public Property M_TIME_NUM() As String
            Set(ByVal value As String)
                _M_TIME_NUM = value
            End Set
            Get
                Return (_M_TIME_NUM)
            End Get
        End Property

        Public Property Status() As String
            Set(ByVal value As String)
                _Status = value
            End Set
            Get
                Return (_Status)
            End Get
        End Property

        Public Property SYS_Date() As DateTime
            Set(ByVal value As DateTime)
                _SYS_Date = value
            End Set
            Get
                Return (_SYS_Date)
            End Get
        End Property

        Public Property IsManual() As Boolean
            Set(ByVal value As Boolean)
                _IsManual = value
            End Set
            Get
                Return (_IsManual)
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

        Public Property IsFromInvalid() As Boolean
            Set(ByVal value As Boolean)
                _IsFromInvalid = value
            End Set
            Get
                Return (_IsFromInvalid)
            End Get
        End Property

        Public Property ManualEntryAllowedBefore() As String
            Set(ByVal value As String)
                _ManualEntryAllowedBefore = value
            End Set
            Get
                Return (_ManualEntryAllowedBefore)
            End Get
        End Property

        Public Property AttachedFile() As String
            Set(ByVal value As String)
                _AttachedFile = value
            End Set
            Get
                Return (_AttachedFile)
            End Get
        End Property

        Public Property Id() As Integer
            Set(ByVal value As Integer)
                _Id = value
            End Set
            Get
                Return (_Id)
            End Get
        End Property

        Public Property FK_EntityId() As Integer
            Set(ByVal value As Integer)
                _FK_EntityId = value
            End Set
            Get
                Return (_FK_EntityId)
            End Get
        End Property

        Public Property FK_CompanyId() As Integer
            Set(ByVal value As Integer)
                _FK_CompanyId = value
            End Set
            Get
                Return (_FK_CompanyId)
            End Get
        End Property

        Public Property IsRemoteWork() As Boolean
            Set(ByVal value As Boolean)
                _IsRemoteWork = value
            End Set
            Get
                Return (_IsRemoteWork)
            End Get
        End Property

        Public Property IsFromMobile() As Boolean
            Get
                Return _IsFromMobile
            End Get
            Set(ByVal value As Boolean)
                _IsFromMobile = value
            End Set
        End Property

        Public Property MobileCoordinates() As String
            Get
                Return _MobileCoordinates
            End Get
            Set(ByVal value As String)
                _MobileCoordinates = value
            End Set
        End Property

        Property WorkLocationId As Integer
            Get
                Return _workLocationId
            End Get
            Set(value As Integer)
                _workLocationId = value
            End Set
        End Property

        Public Property DeviceId() As String
            Get
                Return _DeviceId
            End Get
            Set(ByVal value As String)
                _DeviceId = value
            End Set
        End Property

        Public Property BeaconId() As Integer
            Get
                Return _BeaconId
            End Get
            Set(ByVal value As Integer)
                _BeaconId = value
            End Set
        End Property

        Public Property IsFromBeacon() As Boolean
            Get
                Return _IsFromBeacon
            End Get
            Set(ByVal value As Boolean)
                _IsFromBeacon = value
            End Set
        End Property

        Public Property AllowedGPSType() As Boolean
            Get
                Return _AllowedGPSType
            End Get
            Set(ByVal value As Boolean)
                _AllowedGPSType = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Public Sub New()

            objDALEmp_Move = New DALEmp_Move()

        End Sub

#End Region

#Region "Methods"

        Sub AddManualEntryAllProcess(ByVal EmployeeId As Integer, ByVal Type As String, ByVal MoveDate As DateTime, ByVal MoveTime As DateTime,
                                   ByVal FK_ReasonId As Integer, ByVal Remarks As String, ByVal Reader As String, ByVal M_DATE_NUM As Integer,
                                  ByVal M_TIME_NUM As Integer, ByVal Status As Integer, ByVal SYS_Date As DateTime, ByVal IsManual As String, ByVal CREATED_BY As Integer,
                                  ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As Integer, ByVal LAST_UPDATE_DATE As DateTime, ByVal IsFromMobile As Boolean,
                                  ByVal MobileCoordinates As String, ByVal IsRejected As Boolean, ByVal AttachedFile As String, ByVal IsRemoteWork As Boolean)

            Dim temp_date As Date
            Dim temp_str_date As String
            Dim formats As String() = New String() {"HHmm"}
            Dim temp_time As DateTime
            Dim temp_str_time As String
            Dim err As Integer

            objRECALC_REQUEST = New RECALC_REQUEST
            objREADER_KEYS = New READER_KEYS
            objRECALC_REQUEST.EMP_NO = EmployeeId

            Me.FK_EmployeeId = EmployeeId
            Me.Remarks = Remarks
            Me.Reader = Reader
            Me.MoveDate = MoveDate
            Me.MoveTime = MoveTime
            Me.Status = Status
            Me.IsManual = True
            Me.CREATED_BY = CREATED_BY
            objREADER_KEYS.READER_KEY = FK_ReasonId
            objREADER_KEYS.GetByPK()
            Me.FK_ReasonId = objREADER_KEYS.CHANGE_TO
            Me.Type = objREADER_KEYS.Type
            Me.IsRemoteWork = IsRemoteWork
            'DateTime.TryParseExact(MoveTime, formats, System.System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal, temp_time)

            'Me.MoveTime = temp_time
            temp_str_time = temp_time.Minute + temp_time.Hour * 60
            Me.M_TIME_NUM = temp_str_time

            err = Me.Add()
            temp_date = MoveDate
            temp_str_date = DateToString(temp_date)
            objRECALC_REQUEST.EMP_NO = EmployeeId
            objRECALC_REQUEST.VALID_FROM_NUM = Integer.Parse(temp_str_date)



            If err = 0 Then
                Dim err2 As Integer
                If Not temp_date = Date.Now.AddDays(1).ToShortDateString() Then
                    Dim count As Integer
                    While count < 5
                        err2 = objRECALC_REQUEST.RECALCULATE()
                        If err2 = 0 Then
                            Exit While
                        End If
                        count += 1
                    End While
                End If
            End If

        End Sub

        Public Function Add() As Integer

            Dim rslt As Integer = objDALEmp_Move.Add(_MoveId, FK_EmployeeId, _Type, MoveDate, MoveTime, FK_ReasonId, _Remarks, _Reader, _Status, _IsManual, _CREATED_BY, _IsFromInvalid, _AttachedFile, _IsRemoteWork)
            App_EventsLog.Insert_ToEventLog("Add", _MoveId, "Emp_Move", "Manual Attendance Entry")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALEmp_Move.Update(_MoveId, FK_EmployeeId, _Type, MoveDate, MoveTime, FK_ReasonId, _Remarks, _Reader, _Status, _IsManual, _LAST_UPDATE_BY, _AttachedFile, _IsRemoteWork)
            App_EventsLog.Insert_ToEventLog("Edit", _MoveId, "Emp_Move", "Manual Attendance Entry")
            Return rslt
        End Function

        Public Function Update_TransactionType() As Integer

            Dim rslt As Integer = objDALEmp_Move.Update_TransactionType(_MoveId, FK_ReasonId, _Remarks, _LAST_UPDATE_BY)
            App_EventsLog.Insert_ToEventLog("Edit", _MoveId, "Emp_Move", "Manual Attendance Entry")
            Return rslt
        End Function

        Public Function Getfilter() As DataTable

            Return objDALEmp_Move.GetFilter(FK_EmployeeId, MoveDate)

        End Function

        Public Function GetFilter_ByUser() As DataTable

            Return objDALEmp_Move.GetFilter_ByUser(_Id, MoveDate)

        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALEmp_Move.Delete(_MoveId)
            App_EventsLog.Insert_ToEventLog("Delete", _MoveId, "Emp_Move", "Manual Attendance Entry")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_Move.GetAll()

        End Function

        Public Function Get_Attend_Absent() As DataTable

            Return objDALEmp_Move.Get_Attend_Absent(_FK_EmployeeId, _M_DATE_NUM, _FK_EntityId, _FK_CompanyId)

        End Function

        Public Function GetAllForRealTime() As DataTable

            Return objDALEmp_Move.GetAllForRealTime()

        End Function

        Public Function GetDailyTrans() As DataTable

            Return objDALEmp_Move.GetDailyTrans(_FK_EmployeeId)

        End Function

        Public Function GetByPK() As Emp_Move

            Dim dr As DataRow
            dr = objDALEmp_Move.GetByPK(_MoveId)

            If Not IsDBNull(dr("MoveId")) Then
                _MoveId = dr("MoveId")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("Type")) Then
                _Type = dr("Type")
            End If
            If Not IsDBNull(dr("MoveDate")) Then
                _MoveDate = dr("MoveDate")
            End If
            If Not IsDBNull(dr("MoveTime")) Then
                _MoveTime = dr("MoveTime")
            End If
            If Not IsDBNull(dr("FK_ReasonId")) Then
                _FK_ReasonId = dr("FK_ReasonId")
            End If
            If Not IsDBNull(dr("Remarks")) Then
                _Remarks = dr("Remarks")
            End If
            If Not IsDBNull(dr("Reader")) Then
                _Reader = dr("Reader")
            End If
            If Not IsDBNull(dr("M_DATE_NUM")) Then
                _M_DATE_NUM = dr("M_DATE_NUM")
            End If
            If Not IsDBNull(dr("M_TIME_NUM")) Then
                _M_TIME_NUM = dr("M_TIME_NUM")
            End If
            If Not IsDBNull(dr("Status")) Then
                _Status = dr("Status")
            End If
            If Not IsDBNull(dr("SYS_Date")) Then
                _SYS_Date = dr("SYS_Date")
            End If
            If Not IsDBNull(dr("IsManual")) Then
                _IsManual = dr("IsManual")
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
            If Not IsDBNull(dr("AttachedFile")) Then
                _AttachedFile = dr("AttachedFile")
            End If
            Return Me
        End Function

        Private Function DateToString(ByVal TempDate As Date) As String
            Dim tempDay As String
            Dim tempMonth As String

            If TempDate.Month.ToString.Length = 1 Then
                tempMonth = "0" + TempDate.Month.ToString
            Else
                tempMonth = TempDate.Month.ToString
            End If
            If TempDate.Day.ToString.Length = 1 Then
                tempDay = "0" + TempDate.Day.ToString
            Else
                tempDay = TempDate.Day.ToString
            End If
            Return TempDate.Year.ToString() + tempMonth + tempDay
        End Function

        Public Function Get_ByEmp_DateDiff() As DataTable

            Return objDALEmp_Move.Get_ByEmp_DateDiff(_FK_EmployeeId, _ManualEntryAllowedBefore)

        End Function

        Public Function Add_TransactionBySimulation() As Integer

            Dim rslt As Integer = objDALEmp_Move.Add_TransactionBySimulation(FK_EmployeeId, FK_ReasonId)
            Return rslt
        End Function

        Public Function AddMoveFromMobile() As Integer
            Return objDALEmp_Move.AddMoveFromMobile(_MoveId, _FK_EmployeeId, _Type, _MoveDate, _MoveTime, _FK_ReasonId, _Remarks, _Reader, _M_DATE_NUM, _M_TIME_NUM, _Status, _IsManual, _CREATED_BY, _IsFromMobile, _MobileCoordinates, _workLocationId, _DeviceId)
        End Function

        Public Function AddMoveFromMobile_Beacon() As Integer
            Return objDALEmp_Move.AddMoveFromMobile_Mobile_Beacon(_MoveId, _FK_EmployeeId, _Type, _MoveDate, _MoveTime, _FK_ReasonId, _Remarks, _Reader, _M_DATE_NUM, _M_TIME_NUM, _Status, _IsManual, _CREATED_BY, _IsFromMobile, _BeaconId, _workLocationId, _DeviceId)
        End Function

        Public Function AddMoveFromMobileTemporary() As Integer
            Return objDALEmp_Move.AddMoveFromMobileTemporary(_MoveId, _FK_EmployeeId, _Type, _MoveDate, _MoveTime, _FK_ReasonId, _Remarks, _Reader, _M_DATE_NUM, _M_TIME_NUM, _Status, _IsManual, _CREATED_BY, _IsFromMobile, _MobileCoordinates, _workLocationId, _DeviceId)
        End Function

        Public Function AddMoveFromMobile_BeaconTemporary() As Integer
            Return objDALEmp_Move.AddMoveFromMobile_Mobile_BeaconTemporary(_MoveId, _FK_EmployeeId, _Type, _MoveDate, _MoveTime, _FK_ReasonId, _Remarks, _Reader, _M_DATE_NUM, _M_TIME_NUM, _Status, _IsManual, _CREATED_BY, _IsFromMobile, _BeaconId, _workLocationId, _DeviceId)
        End Function

        Public Function IsTimeInValid(EmployeeId As Integer, Reason As Integer, TimeIn As DateTime, Lang As Integer) As String
            Dim dt As DataTable = objDALEmp_Move.IsTimeInValid(EmployeeId, Reason, TimeIn, Lang)
            If (dt IsNot Nothing) Then
                Return dt.Rows(0)("ErrorMessage").ToString()
            End If
            Return ""
        End Function

        Public Function Check_IsFirstIn() As DataTable
            Return objDALEmp_Move.Check_IsFirstIn(_FK_EmployeeId)
        End Function

#End Region

    End Class
End Namespace