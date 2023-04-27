Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Admin

    Public Class TA_Reason

#Region "Class Variables"


        Private _ReasonCode As Integer
        Private _ReasonName As String
        Private _Type As Char
        Private _IsInsideWork As Boolean
        Private _ReasonArabicName As String
        Private _IsScheduleTiming As Boolean
        Private _IsFirstIn As Boolean
        Private _IsLastOut As Boolean
        Private objDALTA_Reason As DALTA_Reason

#End Region

#Region "Public Properties"


        Public Property ReasonCode() As Integer
            Set(ByVal value As Integer)
                _ReasonCode = value
            End Set
            Get
                Return (_ReasonCode)
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

        Public Property Type() As Char
            Set(ByVal value As Char)
                _Type = value
            End Set
            Get
                Return (_Type)
            End Get
        End Property

        Public Property IsInsideWork() As Boolean
            Set(ByVal value As Boolean)
                _IsInsideWork = value
            End Set
            Get
                Return (_IsInsideWork)
            End Get
        End Property

        Public Property IsScheduleTiming() As Boolean
            Set(ByVal value As Boolean)
                _IsScheduleTiming = value
            End Set
            Get
                Return (_IsScheduleTiming)
            End Get
        End Property

        Public Property IsFirstIn() As Boolean
            Set(ByVal value As Boolean)
                _IsFirstIn = value
            End Set
            Get
                Return (_IsFirstIn)
            End Get
        End Property

        Public Property IsLastOut() As Boolean
            Set(ByVal value As Boolean)
                _IsLastOut = value
            End Set
            Get
                Return (_IsLastOut)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALTA_Reason = New DALTA_Reason()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer
            Dim rslt As Integer = objDALTA_Reason.Add(_ReasonCode, _ReasonName, _ReasonArabicName, _Type, _IsInsideWork, _IsScheduleTiming, _IsFirstIn, _IsLastOut)
            App_EventsLog.Insert_ToEventLog("Add", _ReasonCode, "TAReason", "Define TA Reasons")
            Return rslt
        End Function

        Public Function Update() As Integer
            Dim rslt As Integer = objDALTA_Reason.Update(_ReasonCode, _ReasonName, _ReasonArabicName, _Type, _IsInsideWork, _IsScheduleTiming, _IsFirstIn, _IsLastOut)
            App_EventsLog.Insert_ToEventLog("Edit", _ReasonCode, "TAReason", "Define TA Reasons")
            Return rslt

        End Function

        Public Function IS_Exist() As Boolean
            Return objDALTA_Reason.IS_Exist(_ReasonCode)

        End Function

        Public Function Delete() As Integer
            Dim rslt As Integer = objDALTA_Reason.Delete(_ReasonCode)
            App_EventsLog.Insert_ToEventLog("Delete", _ReasonCode, "TAReason", "Define TA Reasons")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALTA_Reason.GetAll()

        End Function

        Public Function GetByPK() As TA_Reason

            Dim dr As DataRow
            dr = objDALTA_Reason.GetByPK(_ReasonCode)

            If Not IsDBNull(dr("ReasonCode")) Then
                _ReasonCode = dr("ReasonCode")
            End If
            If Not IsDBNull(dr("ReasonName")) Then
                _ReasonName = dr("ReasonName")
            End If
            If Not IsDBNull(dr("ReasonArabicName")) Then
                _ReasonArabicName = dr("ReasonArabicName")
            End If
            If Not IsDBNull(dr("Type")) Then
                _Type = dr("Type")
            End If
            If Not IsDBNull(dr("IsConsiderInside")) Then
                _IsInsideWork = dr("IsConsiderInside")
            End If
            If Not IsDBNull(dr("IsScheduleTiming")) Then
                _IsScheduleTiming = dr("IsScheduleTiming")
            End If
            If Not IsDBNull(dr("IsFirstIn")) Then
                _IsFirstIn = dr("IsFirstIn")
            End If
            If Not IsDBNull(dr("IsLastOut")) Then
                _IsLastOut = dr("IsLastOut")
            End If
            Return Me
        End Function

        Public Function GetIsScheduleTiming() As DataTable

            Return objDALTA_Reason.GetIsScheduleTiming()

        End Function

        Public Function GetNotIsScheduleTiming() As DataTable

            Return objDALTA_Reason.GetNotIsScheduleTiming()

        End Function

        Public Function GetAll_Remote() As DataTable

            Return objDALTA_Reason.GetAll_Remote

        End Function

#End Region

    End Class
End Namespace