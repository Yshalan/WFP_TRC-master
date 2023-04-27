Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Admin

    Public Class OrgLevel

#Region "Class Variables"


        Private _LevelId As Integer
        Private _FK_CompanyId As Integer
        Private _LevelName As String
        Private _LevelArabicName As String
        Private _oldCompanyId As Integer
        Private objDALOrgLevel As DALOrgLevel
        Private _LvlId As Integer

#End Region

#Region "Public Properties"


        Public Property LevelId() As Integer
            Set(ByVal value As Integer)
                _LevelId = value
            End Set
            Get
                Return (_LevelId)
            End Get
        End Property
        Public Property LvlId() As Integer
            Set(ByVal value As Integer)
                _LvlId = value
            End Set
            Get
                Return (_LvlId)
            End Get
        End Property


        Public Property FK_CompanyId() As Integer
            Set(ByVal value As Integer)
                _FK_CompanyId = value
            End Set
            Get
                Return (_FK_CompanyId)
            End Get
        End Property
        Public Property oldCompanyId() As Integer
            Set(ByVal value As Integer)
                _oldCompanyId = value
            End Set
            Get
                Return (_oldCompanyId)
            End Get
        End Property


        Public Property LevelName() As String
            Set(ByVal value As String)
                _LevelName = value
            End Set
            Get
                Return (_LevelName)
            End Get
        End Property


        Public Property LevelArabicName() As String
            Set(ByVal value As String)
                _LevelArabicName = value
            End Set
            Get
                Return (_LevelArabicName)
            End Get
        End Property


        Private _ErrNo As Integer
        Public Property ErrNo() As Integer
            Get
                Return _ErrNo
            End Get
            Set(ByVal value As Integer)
                _ErrNo = value
            End Set
        End Property


#End Region


#Region "Constructor"

        Public Sub New()

            objDALOrgLevel = New DALOrgLevel()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer
            Dim rslt As Integer = objDALOrgLevel.Add(_FK_CompanyId, _LevelName, _LevelArabicName, _LvlId)
            App_EventsLog.Insert_ToEventLog("Add", _LvlId, "OrgLevel", "Companies Levels")
            Return rslt
        End Function
        Public Function Add_Bulk(ByVal DT As DataTable) As Integer
            Dim DS As New DataSet("DS")
            DT.TableName = "DT"
            DS.Tables.Add(DT)
            Dim StrXml As String = DS.GetXml

            Dim rslt As Integer = objDALOrgLevel.Add_Bulk(StrXml, _FK_CompanyId)

            Return rslt
        End Function
        Public Function Update() As Integer
            Dim rslt As Integer = objDALOrgLevel.Update(_LevelId, _FK_CompanyId, _LevelName, _LevelArabicName)
            App_EventsLog.Insert_ToEventLog("Edit", _LevelId, "OrgLevel", "Companies Levels")
            Return rslt

        End Function
        Public Function UpdateGrid() As Integer
            Dim rslt As Integer = objDALOrgLevel.UpdateGrid(_LevelId, _FK_CompanyId, _LevelName, _LevelArabicName)
            App_EventsLog.Insert_ToEventLog("Edit", _LevelId, "OrgLevel", "Companies Levels")
            Return rslt

        End Function


        Public Function Delete() As Integer
            Dim rslt As Integer = objDALOrgLevel.Delete(_LevelId, _FK_CompanyId)
            App_EventsLog.Insert_ToEventLog("Delete", _LevelId, "OrgLevel", "Companies Levels")
            Return rslt

        End Function

        Public Function CheckExistsInEntity() As Integer

            Return objDALOrgLevel.CheckExistsInEntity(_LevelId, _FK_CompanyId)

        End Function
        Public Function GetAll() As DataTable

            Return objDALOrgLevel.GetAll()

        End Function
        Public Function GetAll_Company() As DataTable

            Return objDALOrgLevel.GetAll_Company()

        End Function

        Public Function GetByPK() As OrgLevel

            Dim dr As DataRow
            Dim tmpDR As Object = objDALOrgLevel.GetByPK(_LevelId, _FK_CompanyId)
            If objDALOrgLevel.ErrorNo = -1 Then
                ErrNo = -1
            Else
                dr = CType(tmpDR, DataRow)
                If Not IsDBNull(dr("LevelId")) Then
                    _LevelId = dr("LevelId")
                End If
                If Not IsDBNull(dr("FK_CompanyId")) Then
                    _FK_CompanyId = dr("FK_CompanyId")
                End If
                If Not IsDBNull(dr("LevelName")) Then
                    _LevelName = dr("LevelName")
                End If
                If Not IsDBNull(dr("LevelArabicName")) Then
                    _LevelArabicName = dr("LevelArabicName")
                End If

            End If
            Return Me
        End Function


        Public Function GetAllByComapany() As DataTable

            Return objDALOrgLevel.GetAllByCompany(_FK_CompanyId)

        End Function
        Public Function GetAllByCompanyAndLevel() As DataTable

            Return objDALOrgLevel.GetAllByCompanyAndLevel(_FK_CompanyId, _LevelId)

        End Function
        Public Function GetAllGridData() As DataTable

            Return objDALOrgLevel.GetAllGridData(_FK_CompanyId)

        End Function

        Public Function GetAllWithCompany() As DataTable
            Return objDALOrgLevel.GetAll_With_Comapany()
        End Function
        Public Function GetLevelsByCompany() As Integer

            Return objDALOrgLevel.GetLevelsByCompany(_FK_CompanyId)

        End Function

        Public Function GetAll_ForHierarchy() As DataTable

            Return objDALOrgLevel.GetAll_ForHierarchy(_FK_CompanyId)

        End Function

#End Region

    End Class
End Namespace