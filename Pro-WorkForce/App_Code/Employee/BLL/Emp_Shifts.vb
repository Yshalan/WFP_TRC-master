Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events
Imports System.Web.UI.WebControls
Namespace TA.Employees

    Public Class Emp_Shifts

#Region "Class Variables"


        Private _FK_EmployeeId As Integer
        Private _FK_ShiftId As Integer
        Private _WorkDate As DateTime

        Private objDALEmp_Shifts As DALEmp_Shifts

#End Region
#Region "Public Properties"
        Public Property FK_EmployeeId() As Integer
            Get
                Return _FK_EmployeeId
            End Get
            Set(ByVal value As Integer)
                _FK_EmployeeId = value
            End Set
        End Property
        Public Property FK_ShiftId() As Integer
            Get
                Return _FK_ShiftId
            End Get
            Set(ByVal value As Integer)
                _FK_ShiftId = value
            End Set
        End Property
        Public Property WorkDate() As DateTime
            Get
                Return _WorkDate
            End Get
            Set(ByVal value As DateTime)
                _WorkDate = value
            End Set
        End Property
#End Region



#Region "Constructor"

        Public Sub New()

            objDALEmp_Shifts = New DALEmp_Shifts()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALEmp_Shifts.Add(_FK_EmployeeId, _FK_ShiftId, _WorkDate)
            App_EventsLog.Insert_ToEventLog("Add", _FK_EmployeeId, "Emp_Shifts", "Prepare Shift Schedule")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALEmp_Shifts.Delete(_FK_EmployeeId, _FK_ShiftId, _WorkDate)
            App_EventsLog.Insert_ToEventLog("Delete", _FK_EmployeeId, "Emp_Shifts", "Prepare Shift Schedule")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_Shifts.GetAll()

        End Function

        Public Function GetShiftsByDate() As DataTable

            Return objDALEmp_Shifts.GetShiftsByDate(_FK_EmployeeId, _WorkDate)

        End Function


        Public Function GetByDate(ByVal Year As Integer, ByVal Month As Integer) As DataTable

            Return objDALEmp_Shifts.GetByDate(Year, Month)

        End Function

        Public Function GetByDate_JSON(ByVal Year As Integer, ByVal Month As Integer) As String

            Dim dt As DataTable = objDALEmp_Shifts.GetByDate(Year, Month)

            Dim builder As New StringBuilder
            builder.Append("[")
            If dt IsNot Nothing Then
                For Each row As DataRow In dt.Rows
                    builder.Append("{ ""EmpId"": """)
                    builder.Append(row("FK_EmployeeId"))
                    builder.Append(""", ""ShiftId"": """)
                    builder.Append(row("FK_ShiftId"))
                    builder.Append(""", ""year"": """)
                    builder.Append(row("WorkYear"))
                    builder.Append(""", ""month"": """)
                    builder.Append(row("WorkMonth"))
                    builder.Append(""", ""day"": """)
                    builder.Append(row("WorkDay"))
                    builder.Append(""", ""color"": """)
                    builder.Append(row("Color"))
                    builder.Append(""", ""status"": """)
                    builder.Append("0")
                    builder.Append(""", },")

                Next
            End If
            builder.Append("]")
            Return builder.ToString()

        End Function

        Public Function GetRequestByDate_JSON(ByVal Year As Integer, ByVal Month As Integer, ByVal FK_StatusId As Integer, ByVal CreatedBy As String) As String

            Dim dt As DataTable = objDALEmp_Shifts.GetRequestByDate(Year, Month)

            Dim builder As New StringBuilder
            builder.Append("[")
            If dt IsNot Nothing Then
                For Each row As DataRow In dt.Rows
                    builder.Append("{ ""EmpId"": """)
                    builder.Append(row("FK_EmployeeId"))
                    builder.Append(""", ""ShiftId"": """)
                    builder.Append(row("FK_ShiftId"))
                    builder.Append(""", ""year"": """)
                    builder.Append(row("WorkYear"))
                    builder.Append(""", ""month"": """)
                    builder.Append(row("WorkMonth"))
                    builder.Append(""", ""day"": """)
                    builder.Append(row("WorkDay"))
                    builder.Append(""", ""color"": """)
                    builder.Append(row("Color"))
                    builder.Append(""", ""status"": """)
                    builder.Append("0")
                    builder.Append(""", ""fk_statusid"": """)
                    builder.Append(FK_StatusId)
                    builder.Append(""", ""CreatedBy"": """)
                    builder.Append(CreatedBy)
                    builder.Append(""", },")

                Next
            End If
            builder.Append("]")
            Return builder.ToString()

        End Function

        Public Function GetValidtionMsg(ByVal ValidationMinimumShift As Xml) As DataTable

            Return objDALEmp_Shifts.GetValidtionMsg(ValidationMinimumShift)

        End Function

        Public Function GetRequestValidtionMsg(ByVal ValidationMinimumShift As Xml) As DataTable

            Return objDALEmp_Shifts.GetRequestValidtionMsg(ValidationMinimumShift)

        End Function

#End Region

    End Class
End Namespace