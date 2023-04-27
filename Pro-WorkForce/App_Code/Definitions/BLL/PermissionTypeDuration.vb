Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Admin
Imports TA.Events

Namespace TA.Definitions

    Public Class PermissionTypeDuration

#Region "Class Variables"


        Private _FK_PermId As Integer
        Private _FK_DurationId As Integer
        Private _MaximumDuration As Integer
        Private _MaximumRamadanDuration As Integer
        Private _MaximumDuration_WithStudyNursing As String
        Private objDALPermissionTypeDuration As DALPermissionTypeDuration

#End Region

#Region "Public Properties"


        Public Property FK_PermId() As Integer
            Set(ByVal value As Integer)
                _FK_PermId = value
            End Set
            Get
                Return (_FK_PermId)
            End Get
        End Property

        Public Property FK_DurationId() As Integer
            Set(ByVal value As Integer)
                _FK_DurationId = value
            End Set
            Get
                Return (_FK_DurationId)
            End Get
        End Property

        Public Property MaximumDuration() As Integer
            Set(ByVal value As Integer)
                _MaximumDuration = value
            End Set
            Get
                Return (_MaximumDuration)
            End Get
        End Property

        Public Property MaximumRamadanDuration() As Integer
            Set(ByVal value As Integer)
                _MaximumRamadanDuration = value
            End Set
            Get
                Return (_MaximumRamadanDuration)
            End Get
        End Property

        Public Property MaximumDuration_WithStudyNursing() As String
            Set(ByVal value As String)
                _MaximumDuration_WithStudyNursing = value
            End Set
            Get
                Return (_MaximumDuration_WithStudyNursing)
            End Get
        End Property

#End Region

#Region "Constructor"

        Public Sub New()

            objDALPermissionTypeDuration = New DALPermissionTypeDuration()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALPermissionTypeDuration.Add(_FK_PermId, _FK_DurationId, _MaximumDuration)
            App_EventsLog.Insert_ToEventLog("Add", _FK_PermId, "PermissionTypeDuration", "Define Type of Permissions")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALPermissionTypeDuration.Update(_FK_PermId, _FK_DurationId, _MaximumDuration)
            App_EventsLog.Insert_ToEventLog("Edit", _FK_PermId, "PermissionTypeDuration", "Define Type of Permissions")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALPermissionTypeDuration.Delete(_FK_PermId, _FK_DurationId)
            App_EventsLog.Insert_ToEventLog("Delete", _FK_PermId, "PermissionTypeDuration", "Define Type of Permissions")
            Return rslt

        End Function

        Public Function GetAll() As DataTable

            Return objDALPermissionTypeDuration.GetAll()

        End Function

        Public Function GetByPK() As DataTable

            Dim dt As DataTable
            dt = objDALPermissionTypeDuration.GetByPK(_FK_PermId)

            Return dt
        End Function

        Public Function Add_Bulk(ByVal DT As DataTable, ByVal PermId As Integer) As Integer
            Dim DS As New DataSet("DS")
            DT.TableName = "DT"
            DS.Tables.Add(DT)
            Dim StrXml As String = DS.GetXml
            objDALPermissionTypeDuration = New DALPermissionTypeDuration
            Dim rslt As Integer = objDALPermissionTypeDuration.Add_Bulk(StrXml, PermId)

            Return rslt
        End Function

#End Region

    End Class
End Namespace