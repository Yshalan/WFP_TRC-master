Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.SelfServices

    Public Class EmployeeViolations

#Region "Class Variables"

        Private _FK_EmployeeId As String
        Private _M_Date As DateTime
        Private _Delay As DateTime
        Private _Early_Out As DateTime
        Private _FromDate As DateTime
        Private _Status As String
        Private _Duration As DateTime
        Private _ToDate As DateTime
        Private objDALEmployeeViolations As DALEmployeeViolations
        Private _LostTime As String
        Private _strDelay As String
        Private _strEarly_Out As String
        Private _M_Time As DateTime
        Private _StatusName As String
        Private _StatusArabicName As String
        Private _RemainingPermissionBalance As String
        Private _MissingIn As Integer
        Private _MissingOut As Integer
        Private _Absent As Integer
        Private _NotCompletionHalfDay As Integer
        Private _strDelay_Early_Out As String
        Private _RemainingTimesPersonalPermission As Integer
        Private _CompanyId As Integer
        Private _EntityId As Integer
        Private _WorkLocationId As Integer
        Private _LogicalGroupId As Integer
        Private _ApptType As Integer
        Private _strRemainingYearlyLeaveBalance As String


#End Region

#Region "Public Properties"

        Public Property FK_EmployeeId() As String
            Get
                Return _FK_EmployeeId
            End Get
            Set(ByVal value As String)
                _FK_EmployeeId = value
            End Set
        End Property

        Public Property M_Date() As DateTime
            Get
                Return _M_Date
            End Get
            Set(ByVal value As DateTime)
                _M_Date = value
            End Set
        End Property

        Public Property M_Time() As DateTime
            Get
                Return _M_Time
            End Get
            Set(ByVal value As DateTime)
                _M_Time = value
            End Set
        End Property

        Public Property Delay() As DateTime
            Get
                Return _Delay
            End Get
            Set(ByVal value As DateTime)
                _Delay = value
            End Set
        End Property

        Public Property Early_Out() As DateTime
            Get
                Return _Early_Out
            End Get
            Set(ByVal value As DateTime)
                _Early_Out = value
            End Set
        End Property

        Public Property Status() As String
            Get
                Return _Status
            End Get
            Set(ByVal value As String)
                _Status = value
            End Set
        End Property

        Public Property Duration() As DateTime
            Get
                Return _Duration
            End Get
            Set(ByVal value As DateTime)
                _Duration = value
            End Set
        End Property

        Public Property FromDate() As DateTime
            Get
                Return _FromDate
            End Get
            Set(ByVal value As DateTime)
                _FromDate = value
            End Set
        End Property

        Public Property ToDate() As DateTime
            Get
                Return _ToDate
            End Get
            Set(ByVal value As DateTime)
                _ToDate = value
            End Set
        End Property

        Public Property LostTime() As String
            Get
                Return _LostTime
            End Get
            Set(ByVal value As String)
                _LostTime = value
            End Set
        End Property

        Public Property strDelay() As String
            Get
                Return _strDelay
            End Get
            Set(ByVal value As String)
                _strDelay = value
            End Set
        End Property

        Public Property strEarly_Out() As String
            Get
                Return _strEarly_Out
            End Get
            Set(ByVal value As String)
                _strEarly_Out = value
            End Set
        End Property

        Public Property StatusName() As String
            Get
                Return _StatusName
            End Get
            Set(ByVal value As String)
                _StatusName = value
            End Set
        End Property

        Public Property StatusArabicName() As String
            Get
                Return _StatusArabicName
            End Get
            Set(ByVal value As String)
                _StatusArabicName = value
            End Set
        End Property

        Public Property RemainingPermissionBalance() As String
            Get
                Return _RemainingPermissionBalance
            End Get
            Set(ByVal value As String)
                _RemainingPermissionBalance = value
            End Set
        End Property

        Public Property MissingIn() As Integer
            Get
                Return _MissingIn
            End Get
            Set(ByVal value As Integer)
                _MissingIn = value
            End Set
        End Property

        Public Property MissingOut() As Integer
            Get
                Return _MissingOut
            End Get
            Set(ByVal value As Integer)
                _MissingOut = value
            End Set
        End Property

        Public Property Absent() As Integer
            Get
                Return _Absent
            End Get
            Set(ByVal value As Integer)
                _Absent = value
            End Set
        End Property

        Public Property NotCompletionHalfDay() As Integer
            Get
                Return _NotCompletionHalfDay
            End Get
            Set(ByVal value As Integer)
                _NotCompletionHalfDay = value
            End Set
        End Property

        Public Property strDelay_Early_Out() As String
            Get
                Return _strDelay_Early_Out
            End Get
            Set(ByVal value As String)
                _strDelay_Early_Out = value
            End Set
        End Property

        Public Property RemainingTimesPersonalPermission() As Integer
            Get
                Return _RemainingTimesPersonalPermission
            End Get
            Set(ByVal value As Integer)
                _RemainingTimesPersonalPermission = value
            End Set
        End Property


        Public Property WorkLocationId() As Integer
            Set(ByVal value As Integer)
                _WorkLocationId = value
            End Set
            Get
                Return (_WorkLocationId)
            End Get
        End Property
        Public Property LogicalGroupId() As Integer
            Set(ByVal value As Integer)
                _LogicalGroupId = value
            End Set
            Get
                Return (_LogicalGroupId)
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
        Public Property EntityId() As Integer
            Set(ByVal value As Integer)
                _EntityId = value
            End Set
            Get
                Return (_EntityId)
            End Get
        End Property

        Public Property ApptType() As Integer
            Set(ByVal value As Integer)
                _ApptType = value
            End Set
            Get
                Return (_ApptType)
            End Get
        End Property

        Public Property strRemainingYearlyLeaveBalance() As String
            Set(ByVal value As String)
                _strRemainingYearlyLeaveBalance = value
            End Set
            Get
                Return (_strRemainingYearlyLeaveBalance)
            End Get
        End Property

#End Region

#Region "Constructor"

        Public Sub New()

            objDALEmployeeViolations = New DALEmployeeViolations()

        End Sub

#End Region

#Region "Methods"

        Public Function GetEmployeeViolations() As DataTable

            Return objDALEmployeeViolations.GetEmployeeViolations(_FK_EmployeeId, _FromDate, _ToDate)

        End Function

        Public Function GetEmpSummary() As DataRow
            Dim dr As DataRow
            dr = objDALEmployeeViolations.GetEmpSummary(_FK_EmployeeId, _FromDate, _ToDate)
            If Not IsDBNull(dr("Delay")) Then
                _strDelay = dr("Delay")
            End If
            If Not IsDBNull(dr("Early_Out")) Then
                _strEarly_Out = dr("Early_Out")
            End If
            If Not IsDBNull(dr("LostTime")) Then
                _LostTime = dr("LostTime")
            End If
            If Not IsDBNull(dr("RemainingPermissionBalance")) Then
                _RemainingPermissionBalance = dr("RemainingPermissionBalance")
            End If
            If Not IsDBNull(dr("MissingIn")) Then
                _MissingIn = dr("MissingIn")
            End If
            If Not IsDBNull(dr("MissingOut")) Then
                _MissingOut = dr("MissingOut")
            End If
            If Not IsDBNull(dr("Absent")) Then
                _Absent = dr("Absent")
            End If
            If Not IsDBNull(dr("NotCompletionHalfDay")) Then
                _NotCompletionHalfDay = dr("NotCompletionHalfDay")
            End If
            If Not IsDBNull(dr("Delay_Early_Out")) Then
                _strDelay_Early_Out = dr("Delay_Early_Out")
            End If
            If Not IsDBNull(dr("RemainingTimesPersonalPermission")) Then
                _RemainingTimesPersonalPermission = dr("RemainingTimesPersonalPermission")
            End If
            If Not IsDBNull(dr("RemainingYearlyLeaveBalance")) Then
                _strRemainingYearlyLeaveBalance = dr("RemainingYearlyLeaveBalance")
            End If
            Return dr
        End Function

        Public Function GetEmpInTime() As DataRow
            Dim dr As DataRow
            dr = objDALEmployeeViolations.GetEmpInTime(_FK_EmployeeId, _M_Date)
            If Not dr Is Nothing Then
                If Not IsDBNull(dr("IN_TIME")) Then
                    _M_Time = dr("IN_TIME")
                End If
            End If
            Return dr
        End Function

        Public Function Get_EmpMoveStatus() As DataRow
            Dim dr As DataRow
            dr = objDALEmployeeViolations.Get_EmpMoveStatus(_FK_EmployeeId, _M_Date)
            If Not dr Is Nothing Then
                If Not IsDBNull(dr("STATUS")) Then
                    _Status = dr("STATUS")
                End If
                If Not IsDBNull(dr("StatusName")) Then
                    _StatusName = dr("StatusName")
                End If
                If Not IsDBNull(dr("StatusNameArb")) Then
                    _StatusArabicName = dr("StatusNameArb")
                End If
            End If
            Return dr
        End Function

        Public Function GetHR_EmployeeViolations() As DataTable

            Return objDALEmployeeViolations.GetHR_EmployeeViolations(_FK_EmployeeId, _FromDate, _ToDate, _CompanyId, _EntityId, _WorkLocationId, _LogicalGroupId)

        End Function

        Public Function GetEmployeeCalendar() As DataTable

            Return objDALEmployeeViolations.GetEmployeeCalendar(_FK_EmployeeId, _FromDate, _ToDate)

        End Function

        Public Function GetManagerCalendar() As DataTable

            Return objDALEmployeeViolations.GetManagerCalendar(_FK_EmployeeId, _FromDate, _ToDate)

        End Function

        Public Function GetManagerCalendarDetails() As DataTable

            Return objDALEmployeeViolations.GetManagerCalendarDetails(_ApptType, _FK_EmployeeId, _FromDate)

        End Function

#End Region

    End Class

End Namespace
