Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

namespace TA.ScheduleGroups

Public Class ScheduleGroup_Shifts

#Region "Class Variables"

        Private _GroupShiftId As Integer
        Private _FK_GroupId As Integer
        Private _FK_ShiftId As Integer
        Private _WorkDate As DateTime
        Private _FK_EmployeeId As Integer
        Private objDALScheduleGroup_Shifts As DALScheduleGroup_Shifts

#End Region

#Region "Public Properties"

        Public Property GroupShiftId() As Integer
            Set(ByVal value As Integer)
                _GroupShiftId = value
            End Set
            Get
                Return (_GroupShiftId)
            End Get
        End Property

        Public Property FK_GroupId() As Integer
            Set(ByVal value As Integer)
                _FK_GroupId = value
            End Set
            Get
                Return (_FK_GroupId)
            End Get
        End Property

        Public Property FK_ShiftId() As Integer
            Set(ByVal value As Integer)
                _FK_ShiftId = value
            End Set
            Get
                Return (_FK_ShiftId)
            End Get
        End Property

        Public Property WorkDate() As DateTime
            Set(ByVal value As DateTime)
                _WorkDate = value
            End Set
            Get
                Return (_WorkDate)
            End Get
        End Property

        Public Property FK_EmployeeId() As Integer
            Set(ByVal value As Integer)
                _FK_EmployeeId = value
            End Set
            Get
                Return (_FK_EmployeeId)
            End Get
        End Property

#End Region


#Region "Constructor"

Public Sub New()

objDALScheduleGroup_Shifts = new DALScheduleGroup_Shifts()

End Sub

#End Region

#Region "Methods"


        Public Function Add() As Integer

            Return objDALScheduleGroup_Shifts.Add(_FK_GroupId, _FK_ShiftId, _WorkDate)
        End Function

        Public Function Update() As Integer

            Return objDALScheduleGroup_Shifts.Update(_GroupShiftId, _FK_GroupId, _FK_ShiftId, _WorkDate)

        End Function



        Public Function Delete() As Integer

            Return objDALScheduleGroup_Shifts.Delete(_GroupShiftId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALScheduleGroup_Shifts.GetAll()

        End Function

        Public Function GetByPK() As ScheduleGroup_Shifts

            Dim dr As DataRow
            dr = objDALScheduleGroup_Shifts.GetByPK(_GroupShiftId)

            If Not IsDBNull(dr("GroupShiftId")) Then
                _GroupShiftId = dr("GroupShiftId")
            End If
            If Not IsDBNull(dr("FK_GroupId")) Then
                _FK_GroupId = dr("FK_GroupId")
            End If
            If Not IsDBNull(dr("FK_ShiftId")) Then
                _FK_ShiftId = dr("FK_ShiftId")
            End If
            If Not IsDBNull(dr("WorkDate")) Then
                _WorkDate = dr("WorkDate")
            End If
            Return Me
        End Function

        Public Function Get_WorkDay() As ScheduleGroup_Shifts

            Dim dr As DataRow
            dr = objDALScheduleGroup_Shifts.Get_WorkDay(_WorkDate, _FK_EmployeeId)
            If Not dr Is Nothing Then
                If Not IsDBNull(dr("FK_ShiftId")) Then
                    _FK_ShiftId = dr("FK_ShiftId")
                End If
            End If
            Return Me
        End Function

        Public Function GetByDate_JSON(ByVal Year As Integer, ByVal Month As Integer) As String

            Dim dt As DataTable = objDALScheduleGroup_Shifts.GetByDate(Year, Month)

            Dim builder As New StringBuilder
            builder.Append("[")
            If dt IsNot Nothing Then
                For Each row As DataRow In dt.Rows
                    builder.Append("{ ""GroupID"": """)
                    builder.Append(row("GroupId"))
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
                    builder.Append(""", ""ShiftSerialNo"": """)
                    builder.Append(row("ShiftSerialNo"))
                    builder.Append(""", ""status"": """)
                    builder.Append("0")
                    builder.Append(""", },")

                Next
            End If
            builder.Append("]")
            Return builder.ToString()

        End Function
#End Region

 End Class
End namespace