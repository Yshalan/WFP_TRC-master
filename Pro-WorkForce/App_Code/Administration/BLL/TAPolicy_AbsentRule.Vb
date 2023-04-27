Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Admin
    Public Class TAPolicy_AbsentRule
#Region "Class Variables"
        Private _AbsentRuleId As Integer
        Private _FK_TAPolicyId As Integer
        Private _RuleName As String
        Private _RuleArabicName As String
        Private _AbsentRuleType As Integer
        Private _Variable1 As Integer
        Private _Variable2 As Integer
        Private _CREATED_BY As String
        Private _CREATED_DATE As Date
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As Date
        Private objDALTAPolicy_AbsentRule As DALTAPolicy_AbsentRule
#End Region

#Region "Public Properties"
        Public Property AbsentRuleId() As Integer
            Set(ByVal value As Integer)
                _AbsentRuleId = value
            End Set
            Get
                Return (_AbsentRuleId)
            End Get
        End Property
        Public Property FK_TAPolicyId() As Integer
            Set(ByVal value As Integer)
                _FK_TAPolicyId = value
            End Set
            Get
                Return (_FK_TAPolicyId)
            End Get
        End Property
        Public Property RuleName() As String
            Set(ByVal value As String)
                _RuleName = value
            End Set
            Get
                Return (_RuleName)
            End Get
        End Property
        Public Property RuleArabicName() As String
            Set(ByVal value As String)
                _RuleArabicName = value
            End Set
            Get
                Return (_RuleArabicName)
            End Get
        End Property
        Public Property AbsentRuleType() As Integer
            Set(ByVal value As Integer)
                _AbsentRuleType = value
            End Set
            Get
                Return (_AbsentRuleType)
            End Get
        End Property
        Public Property Variable1() As Integer
            Set(ByVal value As Integer)
                _Variable1 = value
            End Set
            Get
                Return (_Variable1)
            End Get
        End Property
        Public Property Variable2() As Integer
            Set(ByVal value As Integer)
                _Variable2 = value
            End Set
            Get
                Return (_Variable2)
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
        Public Property CREATED_DATE() As Date
            Set(ByVal value As Date)
                _CREATED_DATE = value
            End Set
            Get
                Return (_CREATED_DATE)
            End Get
        End Property
        Public Property LAST_UPDATE_BY() As String
            Set(ByVal value As String)
                _LAST_UPDATE_BY = value
            End Set
            Get
                Return (_LAST_UPDATE_BY)
            End Get
        End Property
        Public Property LAST_UPDATE_DATE() As Date
            Set(ByVal value As Date)
                _LAST_UPDATE_DATE = value
            End Set
            Get
                Return (_LAST_UPDATE_DATE)
            End Get
        End Property
#End Region

#Region "Constructor"
        Public Sub New()
            objDALTAPolicy_AbsentRule = New DALTAPolicy_AbsentRule()
        End Sub

#End Region

#Region "Methods"
        Public Function Add() As Integer

            Dim rslt As Integer = objDALTAPolicy_AbsentRule.Add(_FK_TAPolicyId, _RuleName, _RuleArabicName, _AbsentRuleType, _Variable1, _Variable2)
            App_EventsLog.Insert_ToEventLog("Add", _AbsentRuleId, "TAPolicy_AbsentRule", "Define TA Policies")
            Return rslt
        End Function
        Public Function Add_Bulk(ByVal DT As DataTable, ByVal intTAPolicyId As Integer) As Integer
            Dim DS As New DataSet("DS")
            DT.TableName = "DT"
            DS.Tables.Add(DT)
            Dim StrXml As String = DS.GetXml

            Dim rslt As Integer = objDALTAPolicy_AbsentRule.Add_Bulk(_AbsentRuleId, StrXml, intTAPolicyId)
            App_EventsLog.Insert_ToEventLog("Add", _AbsentRuleId, "TAPolicy_AbsentRule", "Define TA Policies")
            Return rslt
        End Function

        Public Function Update() As Integer
            Dim rslt As Integer = objDALTAPolicy_AbsentRule.Update(_AbsentRuleId, _FK_TAPolicyId, _RuleName, _RuleArabicName, _AbsentRuleType, _Variable1, _Variable2, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE)
            App_EventsLog.Insert_ToEventLog("Edit", _AbsentRuleId, "TAPolicy_AbsentRule", "Define TA Policies")
            Return rslt
        End Function

        Public Function Delete() As Integer
            Dim rslt As Integer = objDALTAPolicy_AbsentRule.Delete(_AbsentRuleId)
            App_EventsLog.Insert_ToEventLog("Delete", _AbsentRuleId, "TAPolicy_AbsentRule", "Define TA Policies")
            Return rslt
        End Function

        Public Function GetAll() As DataTable
            Return objDALTAPolicy_AbsentRule.GetAll()
        End Function
        Public Function GetAllByTAPolicyId() As DataTable
            Return objDALTAPolicy_AbsentRule.GetAllByTAPolicyId(_FK_TAPolicyId)
        End Function

        Public Function GetByPK() As TAPolicy_AbsentRule
            Dim dr As DataRow
            dr = objDALTAPolicy_AbsentRule.GetByPK(_AbsentRuleId)
            If Not IsDBNull(dr("AbsentRuleId")) Then
                _AbsentRuleId = dr("AbsentRuleId")
            End If
            If Not IsDBNull(dr("FK_TAPolicyId")) Then
                _FK_TAPolicyId = dr("FK_TAPolicyId")
            End If
            If Not IsDBNull(dr("RuleName")) Then
                _RuleName = dr("RuleName")
            End If
            If Not IsDBNull(dr("RuleArabicName")) Then
                _RuleArabicName = dr("RuleArabicName")
            End If
            If Not IsDBNull(dr("AbsentRuleType")) Then
                _AbsentRuleType = dr("AbsentRuleType")
            End If
            If Not IsDBNull(dr("Variable1")) Then
                _Variable1 = dr("Variable1")
            End If
            If Not IsDBNull(dr("Variable2")) Then
                _Variable2 = dr("Variable2")
            End If
            If Not IsDBNull(dr("CREATED_BY")) Then
                _CREATED_BY = dr("CREATED_BY")
            End If
            If Not IsDBNull(dr("CREATED_DATE")) Then
                _CREATED_DATE = dr("CREATED_DATE")
            End If
            If Not IsDBNull(dr("LAST_UPDATE_BY")) Then
                _LAST_UPDATE_BY = dr("LAST_UPDATE_BY")
            End If
            If Not IsDBNull(dr("LAST_UPDATE_DATE")) Then
                _LAST_UPDATE_DATE = dr("LAST_UPDATE_DATE")
            End If
        End Function

#End Region

    End Class
End Namespace
