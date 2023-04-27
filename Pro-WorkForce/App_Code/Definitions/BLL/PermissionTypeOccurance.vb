Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Definitions

    Public Class PermissionTypeOccurance

#Region "Class Variables"


        Private _FK_PermId As Integer
        Private _FK_DurationId As Integer
        Private _MaximumOccur As Integer
        Private objDALPermissionTypeOccurance As DALPermissionTypeOccurance

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


        Public Property MaximumOccur() As Integer
            Set(ByVal value As Integer)
                _MaximumOccur = value
            End Set
            Get
                Return (_MaximumOccur)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALPermissionTypeOccurance = New DALPermissionTypeOccurance()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALPermissionTypeOccurance.Add(_FK_PermId, _FK_DurationId, _MaximumOccur)
        End Function

        Public Function Update() As Integer

            Return objDALPermissionTypeOccurance.Update(_FK_PermId, _FK_DurationId, _MaximumOccur)

        End Function

        Public Function Delete() As Integer

            Return objDALPermissionTypeOccurance.Delete(_FK_PermId, _FK_DurationId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALPermissionTypeOccurance.GetAll()

        End Function

        Public Function GetByPK() As DataTable

            Dim dt As DataTable
            dt = objDALPermissionTypeOccurance.GetByPK(_FK_PermId)
            Return dt
        End Function

        Public Function Add_Bulk(ByVal DT As DataTable, ByVal PermId As Integer) As Integer
            Dim DS As New DataSet("DS")
            DT.TableName = "DT"
            DS.Tables.Add(DT)
            Dim StrXml As String = DS.GetXml
            objDALPermissionTypeOccurance = New DALPermissionTypeOccurance
            Dim rslt As Integer = objDALPermissionTypeOccurance.Add_Bulk(StrXml, PermId)
            App_EventsLog.Insert_ToEventLog("Add", _FK_PermId, "PermissionTypeOccurance", "Define Type of Permissions")
            Return rslt
        End Function

#End Region

    End Class
End Namespace