Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.DailyTasks

    Public Class INVALID_ATTEMPTS

#Region "Class Variables"

        Private _Id As Integer
        Private _FK_EmployeeId As Integer
        Private _card_no As String
        Private _M_Date As DateTime
        Private _M_Time As DateTime
        Private _Reason As String
        Private _Reader As String
        Private _EMP_IMAGE As String
        Private _TransactionStatus As Integer
        Private _ReasonName As String
        Private _ReasonArabicName As String
        Private objDALINVALID_ATTEMPTS As DALINVALID_ATTEMPTS

#End Region

#Region "Public Properties"

        Public Property Id() As Integer
            Set(ByVal value As Integer)
                _Id = value
            End Set
            Get
                Return (_Id)
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

        Public Property card_no() As String
            Set(ByVal value As String)
                _card_no = value
            End Set
            Get
                Return (_card_no)
            End Get
        End Property

        Public Property M_Date() As DateTime
            Set(ByVal value As DateTime)
                _M_Date = value
            End Set
            Get
                Return (_M_Date)
            End Get
        End Property

        Public Property M_Time() As DateTime
            Set(ByVal value As DateTime)
                _M_Time = value
            End Set
            Get
                Return (_M_Time)
            End Get
        End Property

        Public Property Reason() As String
            Set(ByVal value As String)
                _Reason = value
            End Set
            Get
                Return (_Reason)
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

        Public Property EMP_IMAGE() As String
            Set(ByVal value As String)
                _EMP_IMAGE = value
            End Set
            Get
                Return (_EMP_IMAGE)
            End Get
        End Property

        Public Property TransactionStatus() As Integer
            Set(ByVal value As Integer)
                _TransactionStatus = value
            End Set
            Get
                Return (_TransactionStatus)
            End Get
        End Property

        Public Property ReasonName() As String
            Set(ByVal value As String)
                _ReasonName = value
            End Set
            Get
                Return (_ReasonName)
            End Get
        End Property

        Public Property ReasonArabicName() As String
            Set(ByVal value As String)
                _ReasonArabicName = value
            End Set
            Get
                Return (_ReasonArabicName)
            End Get
        End Property

#End Region

#Region "Constructor"

        Public Sub New()

            objDALINVALID_ATTEMPTS = New DALINVALID_ATTEMPTS()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALINVALID_ATTEMPTS.Add(_Id, _FK_EmployeeId, _card_no, _M_Date, _M_Time, _Reason, _Reader, _EMP_IMAGE)
            App_EventsLog.Insert_ToEventLog("Add", _Id, "INVALID_ATTEMPTS", "Approve Invalid Attempts")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALINVALID_ATTEMPTS.Update(_Id, _FK_EmployeeId, _card_no, _M_Date, _M_Time, _Reason, _Reader, _EMP_IMAGE)
            App_EventsLog.Insert_ToEventLog("Update", _Id, "INVALID_ATTEMPTS", "Approve Invalid Attempts")
            Return rslt
        End Function

        Public Function UpdateStatus() As Integer

            Return objDALINVALID_ATTEMPTS.UpdateStatus(_Id, _TransactionStatus)

        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALINVALID_ATTEMPTS.Delete(_Id)
            App_EventsLog.Insert_ToEventLog("Delete", _Id, "INVALID_ATTEMPTS", "Approve Invalid Attempts")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALINVALID_ATTEMPTS.GetAll()

        End Function

        Public Function GetAll_Invalid() As DataTable

            Return objDALINVALID_ATTEMPTS.GetAll_Invalid()

        End Function

        Public Function GetByPK() As INVALID_ATTEMPTS

            Dim dr As DataRow
            dr = objDALINVALID_ATTEMPTS.GetByPK(_Id)

            If Not IsDBNull(dr("Id")) Then
                _Id = dr("Id")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("card_no")) Then
                _card_no = dr("card_no")
            End If
            If Not IsDBNull(dr("M_Date")) Then
                _M_Date = dr("M_Date")
            End If
            If Not IsDBNull(dr("M_Time")) Then
                _M_Time = dr("M_Time")
            End If
            If Not IsDBNull(dr("Reason")) Then
                _Reason = dr("Reason")
            End If
            If Not IsDBNull(dr("Reader")) Then
                _Reader = dr("Reader")
            End If
            If Not IsDBNull(dr("EMP_IMAGE")) Then
                _EMP_IMAGE = dr("EMP_IMAGE")
            End If
            Return Me
        End Function

        Public Function Get_ReasonName_ByPK() As INVALID_ATTEMPTS

            Dim dr As DataRow
            dr = objDALINVALID_ATTEMPTS.Get_ReasonName_ByPK(_Id)

            If Not IsDBNull(dr("Id")) Then
                _Id = dr("Id")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("card_no")) Then
                _card_no = dr("card_no")
            End If
            If Not IsDBNull(dr("M_Date")) Then
                _M_Date = dr("M_Date")
            End If
            If Not IsDBNull(dr("M_Time")) Then
                _M_Time = dr("M_Time")
            End If
            If Not IsDBNull(dr("Reason")) Then
                _Reason = dr("Reason")
            End If
            If Not IsDBNull(dr("Reader")) Then
                _Reader = dr("Reader")
            End If
            If Not IsDBNull(dr("EMP_IMAGE")) Then
                _EMP_IMAGE = dr("EMP_IMAGE")
            End If
            If Not IsDBNull(dr("ReasonName")) Then
                _ReasonName = dr("ReasonName")
            End If
            If Not IsDBNull(dr("ReasonArabicName")) Then
                _ReasonArabicName = dr("ReasonArabicName")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace