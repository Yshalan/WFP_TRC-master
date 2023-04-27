Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Admin

    Public Class OrgEntity_History

#Region "Class Variables"

        Private _EntityHistory_Id As Integer
        Private _FK_EntityId As Integer
        Private _FK_EmployeeId As Long
        Private _FromDate As DateTime
        Private _ToDate As DateTime
        Private objDALOrgEntity_History As DALOrgEntity_History

#End Region

#Region "Public Properties"

        Public Property EntityHistory_Id() As Integer
            Set(ByVal value As Integer)
                _EntityHistory_Id = value
            End Set
            Get
                Return (_EntityHistory_Id)
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

        Public Property FK_EmployeeId() As Long
            Set(ByVal value As Long)
                _FK_EmployeeId = value
            End Set
            Get
                Return (_FK_EmployeeId)
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

#End Region

#Region "Constructor"

        Public Sub New()

            objDALOrgEntity_History = New DALOrgEntity_History()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer
            Return objDALOrgEntity_History.Add(_FK_EntityId, _FK_EmployeeId, _FromDate, _ToDate)
        End Function

        Public Function Update() As Integer
            Return objDALOrgEntity_History.Update(_EntityHistory_Id, _FK_EntityId, _FK_EmployeeId, _FromDate, _ToDate)
        End Function

        Public Function Delete() As Integer
            Return objDALOrgEntity_History.Delete(_EntityHistory_Id)
        End Function

        Public Function GetAll() As DataTable
            Return objDALOrgEntity_History.GetAll()
        End Function

        Public Function GetByPK() As OrgEntity_History

            Dim dr As DataRow
            dr = objDALOrgEntity_History.GetByPK(_EntityHistory_Id)

            If Not IsDBNull(dr("EntityHistory_Id")) Then
                _EntityHistory_Id = dr("EntityHistory_Id")
            End If
            If Not IsDBNull(dr("FK_EntityId")) Then
                _FK_EntityId = dr("FK_EntityId")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("FromDate")) Then
                _FromDate = dr("FromDate")
            End If
            If Not IsDBNull(dr("ToDate")) Then
                _ToDate = dr("ToDate")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace