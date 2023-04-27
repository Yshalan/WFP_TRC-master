Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events
Imports SmartV.UTILITIES

Namespace TA.Employees

    Public Class Emp_WorkSchedule

#Region "Class Variables"

        Private _EmpWorkScheduleId As Long
        Private _FK_EmployeeId As Long
        Private _FK_ScheduleId As Integer
        Private _ScheduleType As Integer
        Private _FromDate As DateTime
        Private _ToDate As DateTime
        Private _IsTemporary As Boolean
        Private _ScheduleDate As DateTime
        Private objDALEmp_WorkSchedule As DALEmp_WorkSchedule
        Private _M_Date As DateTime
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private _SCH_End_TIME As String
        Private _IsOffDay As Boolean
        Private _IsHoliday As Boolean

#End Region

#Region "Public Properties"

        Public Property EmpWorkScheduleId() As Long
            Set(ByVal value As Long)
                _EmpWorkScheduleId = value
            End Set
            Get
                Return (_EmpWorkScheduleId)
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

        Public Property FK_ScheduleId() As Integer
            Set(ByVal value As Integer)
                _FK_ScheduleId = value
            End Set
            Get
                Return (_FK_ScheduleId)
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

        Public Property ScheduleDate() As DateTime
            Get
                Return _ScheduleDate
            End Get
            Set(ByVal value As DateTime)
                _ScheduleDate = value
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

        Public Property SCH_End_TIME() As String
            Set(ByVal value As String)
                _SCH_End_TIME = value
            End Set
            Get
                Return (_SCH_End_TIME)
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

        Public Property CREATED_DATE As DateTime
            Set(ByVal value As DateTime)
                _CREATED_DATE = value
            End Set
            Get
                Return (_CREATED_DATE)
            End Get
        End Property

        Public Property LAST_UPDATE_BY As String
            Set(ByVal value As String)
                _LAST_UPDATE_BY = value
            End Set
            Get
                Return (_LAST_UPDATE_BY)
            End Get
        End Property

        Public Property LAST_UPDATE_DATE As DateTime
            Set(ByVal value As DateTime)
                _LAST_UPDATE_DATE = value
            End Set
            Get
                Return (_LAST_UPDATE_DATE)
            End Get
        End Property

        Public Property Emp_IsOffDay As Boolean
            Set(ByVal value As Boolean)
                _IsOffDay = value
            End Set
            Get
                Return (_IsOffDay)
            End Get
        End Property

        Public Property Emp_IsHoliday As Boolean
            Set(ByVal value As Boolean)
                _IsHoliday = value
            End Set
            Get
                Return (_IsHoliday)
            End Get
        End Property

#End Region

#Region "Constructor"

        Public Sub New()

            objDALEmp_WorkSchedule = New DALEmp_WorkSchedule()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = (objDALEmp_WorkSchedule.Add(_FK_EmployeeId, _FK_ScheduleId, _ScheduleType, _FromDate, _ToDate, _IsTemporary))
            App_EventsLog.Insert_ToEventLog("Add", _FK_EmployeeId, "Emp_WorkSchedule", "Employee")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALEmp_WorkSchedule.Update(_EmpWorkScheduleId, _FK_EmployeeId, _FK_ScheduleId, _ScheduleType, _FromDate, _ToDate, _IsTemporary, _LAST_UPDATE_BY)
            App_EventsLog.Insert_ToEventLog("Edit", _FK_EmployeeId, "Emp_WorkSchedule", "Employee")
            Return rslt
        End Function

        Public Function AssignSchedule() As Integer

            Dim rslt As Integer = objDALEmp_WorkSchedule.AssignSchedule(_FK_EmployeeId, _FK_ScheduleId, _ScheduleType, _FromDate, _ToDate, _IsTemporary, _CREATED_BY)
            App_EventsLog.Insert_ToEventLog("Delete", _FK_EmployeeId, "Emp_WorkSchedule", "Employee")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Return objDALEmp_WorkSchedule.Delete(_EmpWorkScheduleId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_WorkSchedule.GetAll()

        End Function

        Public Function GetActiveSchedule(ByVal EmployeeNo As String) As DataTable

            Return objDALEmp_WorkSchedule.GetActiveSchedule(EmployeeNo)

        End Function

        Public Function GetAllByEmployeeandDateRange(ByVal StartDate As DateTime, ByVal EndDate As DateTime) As DataTable
            Return objDALEmp_WorkSchedule.GetAllByEmployeeandDateRange(_FK_EmployeeId, StartDate, EndDate)
        End Function

        Public Function GetAllByEmployeeCompany(ByVal intCompanyID As Integer) As DataTable
            Return objDALEmp_WorkSchedule.GetAllByEmployeeCompany(intCompanyID)
        End Function
        Public Function GetScheduleGroup_Info() As DataTable

            Return objDALEmp_WorkSchedule.GetScheduleGroup_Info(_FK_EmployeeId)

        End Function
        Public Function GetByPK() As Emp_WorkSchedule

            Dim dr As DataRow

            dr = objDALEmp_WorkSchedule.GetByPK(_EmpWorkScheduleId)
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("FK_ScheduleId")) Then
                _FK_ScheduleId = dr("FK_ScheduleId")
            End If
            If Not IsDBNull(dr("ScheduleType")) Then
                _ScheduleType = dr("ScheduleType")
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
            Return Me
        End Function

        Public Function GetAllEmployees_JSON(ByVal month As Integer, ByVal year As Integer, ByVal ScheduleId As Integer) As String
            Dim ScheduleDate As New DateTime(year, month, 1)

            Dim dt As DataTable = objDALEmp_WorkSchedule.GetByScheduleDate(ScheduleDate, ScheduleId)

            Dim builder As New StringBuilder
            builder.Append("[")

            For Each row As DataRow In dt.Rows
                '"{ ""EmpId"": ""1"", ""ShiftId"": ""A"", ""WorkDate"" },"
                builder.Append("{ ""EmpId"": """)
                builder.Append(row("EmployeeId"))
                builder.Append(""", ""EmpNo"": """)
                builder.Append(row("EmployeeNo"))
                builder.Append(""", ""EmpName"": """)
                builder.Append(row("EmployeeName"))
                builder.Append(""", ""listed"": """)
                builder.Append("false")
                builder.Append(""", },")

            Next

            builder.Append("]")
            Return builder.ToString()

        End Function

        Public Function GetAllEmployees_JSON2(ByVal month As Integer, ByVal year As Integer, ByVal ScheduleId As Integer, ManagerId As Integer, Optional ByVal FilterOption As Integer = 0) As String
            Dim ScheduleDate As New DateTime(year, month, 1)

            Dim dt As DataTable = objDALEmp_WorkSchedule.GetByScheduleDateByManager(ScheduleDate, ScheduleId, ManagerId, FilterOption)

            Dim builder As New StringBuilder
            builder.Append("[")
            If SessionVariables.CultureInfo = "en-US" Then
                For Each row As DataRow In dt.Rows
                    '"{ ""EmpId"": ""1"", ""ShiftId"": ""A"", ""WorkDate"" },"
                    builder.Append("{ ""EmpId"": """)
                    builder.Append(row("EmployeeId"))
                    builder.Append(""", ""EmpNo"": """)
                    builder.Append(row("EmployeeNo"))
                    builder.Append(""", ""EmpName"": """)
                    builder.Append(row("EmployeeName"))
                    builder.Append(""", ""listed"": """)
                    builder.Append("false")
                    builder.Append(""", },")

                Next
            Else
                For Each row As DataRow In dt.Rows
                    '"{ ""EmpId"": ""1"", ""ShiftId"": ""A"", ""WorkDate"" },"
                    builder.Append("{ ""EmpId"": """)
                    builder.Append(row("EmployeeId"))
                    builder.Append(""", ""EmpNo"": """)
                    builder.Append(row("EmployeeNo"))
                    builder.Append(""", ""EmpName"": """)
                    builder.Append(row("EmployeeArabicName"))
                    builder.Append(""", ""listed"": """)
                    builder.Append("false")
                    builder.Append(""", },")

                Next
            End If


            builder.Append("]")
            Return builder.ToString()

        End Function
        Public Function Get_Emp_Schedule_Details() As DataTable

            Return objDALEmp_WorkSchedule.Get_Emp_Schedule_Details(_FK_EmployeeId)

        End Function

        Public Function GetBy_EmpId_ScheduleId() As Emp_WorkSchedule
            Dim dr As DataRow
            dr = objDALEmp_WorkSchedule.GetBy_EmpId_ScheduleId(_EmpWorkScheduleId, _FK_EmployeeId)
            If Not IsDBNull(dr("FK_ScheduleId")) Then
                _FK_ScheduleId = dr("FK_ScheduleId")
            End If
            If Not IsDBNull(dr("ScheduleType")) Then
                _ScheduleType = dr("ScheduleType")
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
            Return Me
        End Function

        Public Function GetEmpScheduleWithTime() As String

            Return objDALEmp_WorkSchedule.GetEmpScheduleWithTime(_FK_EmployeeId, _ScheduleDate)

        End Function

        Public Function GetByEmpId() As Emp_WorkSchedule

            Dim dr As DataRow

            dr = objDALEmp_WorkSchedule.GetByEmpId(_FK_EmployeeId)
            If Not dr Is Nothing Then
                If Not IsDBNull(dr("FK_EmployeeId")) Then
                    _FK_EmployeeId = dr("FK_EmployeeId")
                End If
                If Not IsDBNull(dr("FK_ScheduleId")) Then
                    _FK_ScheduleId = dr("FK_ScheduleId")
                End If
                If Not IsDBNull(dr("ScheduleType")) Then
                    _ScheduleType = dr("ScheduleType")
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
                Return Me
            Else
                Return Nothing
            End If
        End Function

        Public Function Get_EmpExpectedOutTime() As DataRow
            Dim dr As DataRow
            dr = objDALEmp_WorkSchedule.Get_EmpExpectedOutTime(_FK_EmployeeId, _M_Date)
            If Not dr Is Nothing Then
                If Not IsDBNull(dr("SCH_End_TIME")) Then
                    _SCH_End_TIME = dr("SCH_End_TIME")
                End If
            End If
            Return dr
        End Function

        Public Function GetAll_ScheduleDeatils() As DataTable

            Return objDALEmp_WorkSchedule.GetAll_ScheduleDeatils(_FK_EmployeeId, _M_Date)

        End Function
        Public Function Get_Emp_Schedule_Details_Advanced() As DataTable

            Return objDALEmp_WorkSchedule.Get_Emp_Schedule_Details_Advanced()

        End Function
        Public Function Get_Emp_Schedule_Details_Advanced_Mgr() As DataTable

            Return objDALEmp_WorkSchedule.Get_Emp_Schedule_Details_Advanced_Mgr(_FK_EmployeeId)

        End Function

        Public Function Get_IsOffDay() As Emp_WorkSchedule

            Dim dr As DataRow

            dr = objDALEmp_WorkSchedule.Get_IsOffDay(_FK_EmployeeId, _ScheduleDate)
            If Not IsDBNull(dr("IsOffDay")) Then
                _IsOffDay = dr("IsOffDay")
            End If
            Return Me
        End Function

        Public Function Get_IsHoliday() As Emp_WorkSchedule

            Dim dr As DataRow

            dr = objDALEmp_WorkSchedule.Get_IsHoliday(_FK_EmployeeId, _ScheduleDate)
            If Not IsDBNull(dr("IsHoliday")) Then
                _IsHoliday = dr("IsHoliday")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace