Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Definitions

    Public Class WorkSchedule_Flexible

#Region "Class Variables"


        Private _FK_ScheduleId As Integer
        Private _DayId As Integer
        Private _FromTime1 As String
        Private _FromTime2 As String
        Private _Duration1 As Integer
        Private _FromTime3 As String
        Private _FromTime4 As String
        Private _Duration2 As Integer
        Private _IsOffDay As Boolean
        Private objDALWorkSchedule_Flexible As DALWorkSchedule_Flexible

#End Region

#Region "Public Properties"


        Public Property FK_ScheduleId() As Integer
            Set(ByVal value As Integer)
                _FK_ScheduleId = value
            End Set
            Get
                Return (_FK_ScheduleId)
            End Get
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


        Public Property Duration1() As Integer
            Set(ByVal value As Integer)
                _Duration1 = value
            End Set
            Get
                Return (_Duration1)
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


        Public Property Duration2() As Integer
            Set(ByVal value As Integer)
                _Duration2 = value
            End Set
            Get
                Return (_Duration2)
            End Get
        End Property
        Public Property IsOffDay() As Integer
            Set(ByVal value As Integer)
                _IsOffDay = value
            End Set
            Get
                Return (_IsOffDay)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALWorkSchedule_Flexible = New DALWorkSchedule_Flexible()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALWorkSchedule_Flexible.Add(_FK_ScheduleId, _DayId, _FromTime1, _FromTime2, _Duration1, _FromTime3, _FromTime4, _Duration2, _IsOffDay)
            App_EventsLog.Insert_ToEventLog("Add", _FK_ScheduleId, "WorkSchedule_Flexibile", "Define Work Schedule")
            Return rslt
        End Function
        Public Function Add(ByVal DT As DataTable, ByVal _ScheduleID As Integer) As Integer
            Dim DS As New DataSet("DS")
            DT.TableName = "DT"
            DS.Tables.Add(DT)
            Dim StrXml As String = DS.GetXml
            If StrXml.Contains("T00:00:00") Then StrXml = StrXml.Replace("T00:00:00", "")
            Dim rslt As Integer = objDALWorkSchedule_Flexible.Add(StrXml, _ScheduleID)

            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALWorkSchedule_Flexible.Update(_FK_ScheduleId, _DayId, _FromTime1, _FromTime2, _Duration1, _FromTime3, _FromTime4, _Duration2, _IsOffDay)
            App_EventsLog.Insert_ToEventLog("Edit", _FK_ScheduleId, "WorkSchedule_Flexibile", "Define Work Schedule")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALWorkSchedule_Flexible.Delete(_FK_ScheduleId, _DayId)
            App_EventsLog.Insert_ToEventLog("Add", _FK_ScheduleId, "WorkSchedule_Flexibile", "Define Work Schedule")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALWorkSchedule_Flexible.GetAll(_FK_ScheduleId)

        End Function

        Public Function GetByPK() As WorkSchedule_Flexible

            Dim dr As DataRow
            dr = objDALWorkSchedule_Flexible.GetByPK(_FK_ScheduleId, _DayId)

            If Not IsDBNull(dr("FK_ScheduleId")) Then
                _FK_ScheduleId = dr("FK_ScheduleId")
            End If
            If Not IsDBNull(dr("DayId")) Then
                _DayId = dr("DayId")
            End If
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
            If Not IsDBNull(dr("IsOffDay")) Then
                _IsOffDay = dr("IsOffDay")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace