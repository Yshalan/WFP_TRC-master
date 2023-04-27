Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Admin
    Public Class TAPolicy_Violation
#Region "Class Variables"
        Private _ViolationId As Integer
        Private _FK_TAPolicyId As Integer
        Private _ViolationName As String
        Private _ViolationArabicName As String
        Private _ViolationRuleType As Integer
        Private _Variable1 As Integer
        Private _Variable2 As Integer
        Private _Variable3 As Integer
        Private _FK_ViolationActionId As Integer
        Private _FK_ViolationActionId2 As Integer
        Private _FK_ViolationActionId3 As Integer
        Private _FK_ViolationActionId4 As Integer
        Private _FK_ViolationActionId5 As Integer
        Private _CREATED_BY As String
        Private _CREATED_DATE As Date
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As Date
        Private _ScenarioMode As Integer
        Private objDALTAPolicy_Violation As DALTAPolicy_Violation
#End Region

#Region "Public Properties"
        Public Property ViolationId() As Integer
            Set(ByVal value As Integer)
                _ViolationId = value
            End Set
            Get
                Return (_ViolationId)
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
        Public Property ViolationName() As String
            Set(ByVal value As String)
                _ViolationName = value
            End Set
            Get
                Return (_ViolationName)
            End Get
        End Property
        Public Property ViolationArabicName() As String
            Set(ByVal value As String)
                _ViolationArabicName = value
            End Set
            Get
                Return (_ViolationArabicName)
            End Get
        End Property
        Public Property ViolationRuleType() As Integer
            Set(ByVal value As Integer)
                _ViolationRuleType = value
            End Set
            Get
                Return (_ViolationRuleType)
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
        Public Property Variable3() As Integer
            Set(ByVal value As Integer)
                _Variable3 = value
            End Set
            Get
                Return (_Variable3)
            End Get
        End Property
        Public Property FK_ViolationActionId() As Integer
            Set(ByVal value As Integer)
                _FK_ViolationActionId = value
            End Set
            Get
                Return (_FK_ViolationActionId)
            End Get
        End Property
        Public Property FK_ViolationActionId2() As Integer
            Set(ByVal value As Integer)
                _FK_ViolationActionId2 = value
            End Set
            Get
                Return (_FK_ViolationActionId2)
            End Get
        End Property
        Public Property FK_ViolationActionId3() As Integer
            Set(ByVal value As Integer)
                _FK_ViolationActionId3 = value
            End Set
            Get
                Return (_FK_ViolationActionId3)
            End Get
        End Property
        Public Property FK_ViolationActionId4() As Integer
            Set(ByVal value As Integer)
                _FK_ViolationActionId4 = value
            End Set
            Get
                Return (_FK_ViolationActionId4)
            End Get
        End Property
        Public Property FK_ViolationActionId5() As Integer
            Set(ByVal value As Integer)
                _FK_ViolationActionId5 = value
            End Set
            Get
                Return (_FK_ViolationActionId5)
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
        Public Property ScenarioMode() As Integer
            Set(ByVal value As Integer)
                _ScenarioMode = value
            End Set
            Get
                Return (_ScenarioMode)
            End Get
        End Property
#End Region

#Region "Constructor"
        Public Sub New()
            objDALTAPolicy_Violation = New DALTAPolicy_Violation()
        End Sub

#End Region

#Region "Methods"
        Public Function Add() As Integer

            Dim rslt As Integer = objDALTAPolicy_Violation.Add(_FK_TAPolicyId, _ViolationName, _ViolationArabicName, _ViolationRuleType, _Variable1, _Variable2, _Variable3, _FK_ViolationActionId, _FK_ViolationActionId2, _FK_ViolationActionId3, _FK_ViolationActionId4, _FK_ViolationActionId5, _ScenarioMode)
            App_EventsLog.Insert_ToEventLog("Add", _ViolationId, "TAPolicy_Violation", "Define TA Policies")
            Return rslt
        End Function
        Public Function Add_Bulk(ByVal DT As DataTable, ByVal intTAPolicyId As Integer) As Integer
            Dim DS As New DataSet("DS")
            DT.TableName = "DT"
            DS.Tables.Add(DT)
            Dim StrXml As String = DS.GetXml

            Dim rslt As Integer = objDALTAPolicy_Violation.Add_Bulk(StrXml, intTAPolicyId)

            Return rslt
        End Function
        Public Function Update() As Integer
            Dim rslt As Integer = objDALTAPolicy_Violation.Update(_ViolationId, _FK_TAPolicyId, _ViolationName, _ViolationArabicName, _ViolationRuleType, _Variable1, _Variable2, _Variable3, _FK_ViolationActionId, _FK_ViolationActionId2, _FK_ViolationActionId3, _FK_ViolationActionId4, _FK_ViolationActionId5, _ScenarioMode)
            App_EventsLog.Insert_ToEventLog("Edit", _ViolationId, "TAPolicy_Violation", "Define TA Policies")
            Return rslt
        End Function

        Public Function Delete() As Integer
            Dim rslt As Integer = objDALTAPolicy_Violation.Delete(_ViolationId)
            App_EventsLog.Insert_ToEventLog("Delete", _ViolationId, "TAPolicy_Violation", "Define TA Policies")
            Return rslt
        End Function

        Public Function GetAll() As DataTable
            Return objDALTAPolicy_Violation.GetAll()
        End Function
        Public Function GetAllByPolicyId() As DataTable
            Return objDALTAPolicy_Violation.GetAllByPolicyId(_FK_TAPolicyId)
        End Function

        Public Function GetByPK() As TAPolicy_Violation
            Dim dr As DataRow
            dr = objDALTAPolicy_Violation.GetByPK(_ViolationId)
            If Not IsDBNull(dr("ViolationId")) Then
                _ViolationId = dr("ViolationId")
            End If
            If Not IsDBNull(dr("FK_TAPolicyId")) Then
                _FK_TAPolicyId = dr("FK_TAPolicyId")
            End If
            If Not IsDBNull(dr("ViolationName")) Then
                _ViolationName = dr("ViolationName")
            End If
            If Not IsDBNull(dr("ViolationArabicName")) Then
                _ViolationArabicName = dr("ViolationArabicName")
            End If
            If Not IsDBNull(dr("ViolationRuleType")) Then
                _ViolationRuleType = dr("ViolationRuleType")
            End If
            If Not IsDBNull(dr("Variable1")) Then
                _Variable1 = dr("Variable1")
            End If
            If Not IsDBNull(dr("Variable2")) Then
                _Variable2 = dr("Variable2")
            End If
            If Not IsDBNull(dr("FK_ViolationActionId")) Then
                _FK_ViolationActionId = dr("FK_ViolationActionId")
            End If
            If Not IsDBNull(dr("FK_ViolationActionId2")) Then
                _FK_ViolationActionId2 = dr("FK_ViolationActionId2")
            End If
            If Not IsDBNull(dr("FK_ViolationActionId3")) Then
                _FK_ViolationActionId3 = dr("FK_ViolationActionId3")
            End If
            If Not IsDBNull(dr("FK_ViolationActionId4")) Then
                _FK_ViolationActionId4 = dr("FK_ViolationActionId4")
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
            If Not IsDBNull(dr("ScenarioMode")) Then
                _ScenarioMode = dr("ScenarioMode")
            End If
        End Function

#End Region

    End Class
End Namespace
