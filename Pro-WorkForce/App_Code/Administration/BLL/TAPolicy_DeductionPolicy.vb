Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Admin

    Public Class TAPolicy_DeductionPolicy

#Region "Class Variables"


        Private _DeductionPolicyId As Integer
        Private _FK_TAPolicyId As Integer
        Private _ConsiderAbsent As Boolean
        Private _ConsiderMissingIn As Boolean
        Private _ConsiderMissingOut As Boolean
        Private _ConsiderDelay_EarlyOut As Boolean
        Private _Delay_EarlyOut_FirstDeduct As Integer
        Private _Delay_EarlyOut_Deduct As Integer
        Private _ConsiderNotComplete As Boolean
        Private _NotCompleteSelection As Integer
        Private _NotCompleteValue As Integer
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private _RemainingBalanceToBeRounded As Boolean
        Private _IncluedLostTime As Boolean
        Private _ExcludePendingLeaves As Boolean
        Private _DelayEarly_CalcMethod As Integer
        Private _ConsiderOneDeduction_DelayEarly As Boolean
        Private objDALTAPolicy_DeductionPolicy As DALTAPolicy_DeductionPolicy

#End Region

#Region "Public Properties"

        Public Property DeductionPolicyId() As Integer
            Set(ByVal value As Integer)
                _DeductionPolicyId = value
            End Set
            Get
                Return (_DeductionPolicyId)
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

        Public Property ConsiderAbsent() As Boolean
            Set(ByVal value As Boolean)
                _ConsiderAbsent = value
            End Set
            Get
                Return (_ConsiderAbsent)
            End Get
        End Property

        Public Property ConsiderMissingIn() As Boolean
            Set(ByVal value As Boolean)
                _ConsiderMissingIn = value
            End Set
            Get
                Return (_ConsiderMissingIn)
            End Get
        End Property

        Public Property ConsiderMissingOut() As Boolean
            Set(ByVal value As Boolean)
                _ConsiderMissingOut = value
            End Set
            Get
                Return (_ConsiderMissingOut)
            End Get
        End Property

        Public Property ConsiderDelay_EarlyOut() As Boolean
            Set(ByVal value As Boolean)
                _ConsiderDelay_EarlyOut = value
            End Set
            Get
                Return (_ConsiderDelay_EarlyOut)
            End Get
        End Property

        Public Property Delay_EarlyOut_FirstDeduct() As Integer
            Set(ByVal value As Integer)
                _Delay_EarlyOut_FirstDeduct = value
            End Set
            Get
                Return (_Delay_EarlyOut_FirstDeduct)
            End Get
        End Property

        Public Property Delay_EarlyOut_Deduct() As Integer
            Set(ByVal value As Integer)
                _Delay_EarlyOut_Deduct = value
            End Set
            Get
                Return (_Delay_EarlyOut_Deduct)
            End Get
        End Property

        Public Property ConsiderNotComplete() As Boolean
            Set(ByVal value As Boolean)
                _ConsiderNotComplete = value
            End Set
            Get
                Return (_ConsiderNotComplete)
            End Get
        End Property

        Public Property NotCompleteSelection() As Integer
            Set(ByVal value As Integer)
                _NotCompleteSelection = value
            End Set
            Get
                Return (_NotCompleteSelection)
            End Get
        End Property

        Public Property NotCompleteValue() As Integer
            Set(ByVal value As Integer)
                _NotCompleteValue = value
            End Set
            Get
                Return (_NotCompleteValue)
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

        Public Property CREATED_DATE() As DateTime
            Set(ByVal value As DateTime)
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

        Public Property LAST_UPDATE_DATE() As DateTime
            Set(ByVal value As DateTime)
                _LAST_UPDATE_DATE = value
            End Set
            Get
                Return (_LAST_UPDATE_DATE)
            End Get
        End Property

        Public Property RemainingBalanceToBeRounded() As Boolean
            Set(ByVal value As Boolean)
                _RemainingBalanceToBeRounded = value
            End Set
            Get
                Return (_RemainingBalanceToBeRounded)
            End Get
        End Property

        Public Property IncluedLostTime() As Boolean
            Set(ByVal value As Boolean)
                _IncluedLostTime = value
            End Set
            Get
                Return (_IncluedLostTime)
            End Get
        End Property

        Public Property ExcludePendingLeaves() As Boolean
            Set(ByVal value As Boolean)
                _ExcludePendingLeaves = value
            End Set
            Get
                Return (_ExcludePendingLeaves)
            End Get
        End Property

        Public Property DelayEarly_CalcMethod() As Integer
            Set(ByVal value As Integer)
                _DelayEarly_CalcMethod = value
            End Set
            Get
                Return (_DelayEarly_CalcMethod)
            End Get
        End Property

        Public Property ConsiderOneDeduction_DelayEarly() As Boolean
            Set(ByVal value As Boolean)
                _ConsiderOneDeduction_DelayEarly = value
            End Set
            Get
                Return (_ConsiderOneDeduction_DelayEarly)
            End Get
        End Property

#End Region

#Region "Constructor"

        Public Sub New()

            objDALTAPolicy_DeductionPolicy = New DALTAPolicy_DeductionPolicy()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALTAPolicy_DeductionPolicy.Add(_DeductionPolicyId, _FK_TAPolicyId, _ConsiderAbsent, _ConsiderMissingIn, _ConsiderMissingOut, _ConsiderDelay_EarlyOut, _Delay_EarlyOut_FirstDeduct, _Delay_EarlyOut_Deduct, _ConsiderNotComplete, _NotCompleteSelection, _NotCompleteValue, _CREATED_BY, _RemainingBalanceToBeRounded, _IncluedLostTime, _ExcludePendingLeaves, _DelayEarly_CalcMethod, _ConsiderOneDeduction_DelayEarly)
            App_EventsLog.Insert_ToEventLog("Add", _DeductionPolicyId, "TAPolicy_DeductionPolicy", "Define TA Policies")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALTAPolicy_DeductionPolicy.Update(_DeductionPolicyId, _FK_TAPolicyId, _ConsiderAbsent, _ConsiderMissingIn, _ConsiderMissingOut, _ConsiderDelay_EarlyOut, _Delay_EarlyOut_FirstDeduct, _Delay_EarlyOut_Deduct, _ConsiderNotComplete, _NotCompleteSelection, _NotCompleteValue, _LAST_UPDATE_BY, _RemainingBalanceToBeRounded, _IncluedLostTime, _ExcludePendingLeaves, _DelayEarly_CalcMethod, _ConsiderOneDeduction_DelayEarly)
            App_EventsLog.Insert_ToEventLog("Update", _DeductionPolicyId, "TAPolicy_DeductionPolicy", "Define TA Policies")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALTAPolicy_DeductionPolicy.Delete(_DeductionPolicyId)
            App_EventsLog.Insert_ToEventLog("Delete", _DeductionPolicyId, "TAPolicy_DeductionPolicy", "Define TA Policies")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALTAPolicy_DeductionPolicy.GetAll()

        End Function

        Public Function GetByPK() As TAPolicy_DeductionPolicy

            Dim dr As DataRow
            dr = objDALTAPolicy_DeductionPolicy.GetByPK(_DeductionPolicyId)

            If Not IsDBNull(dr("DeductionPolicyId")) Then
                _DeductionPolicyId = dr("DeductionPolicyId")
            End If
            If Not IsDBNull(dr("FK_TAPolicyId")) Then
                _FK_TAPolicyId = dr("FK_TAPolicyId")
            End If
            If Not IsDBNull(dr("ConsiderAbsent")) Then
                _ConsiderAbsent = dr("ConsiderAbsent")
            End If
            If Not IsDBNull(dr("ConsiderMissingIn")) Then
                _ConsiderMissingIn = dr("ConsiderMissingIn")
            End If
            If Not IsDBNull(dr("ConsiderMissingOut")) Then
                _ConsiderMissingOut = dr("ConsiderMissingOut")
            End If
            If Not IsDBNull(dr("ConsiderDelay_EarlyOut")) Then
                _ConsiderDelay_EarlyOut = dr("ConsiderDelay_EarlyOut")
            End If
            If Not IsDBNull(dr("Delay_EarlyOut_FirstDeduct")) Then
                _Delay_EarlyOut_FirstDeduct = dr("Delay_EarlyOut_FirstDeduct")
            End If
            If Not IsDBNull(dr("Delay_EarlyOut_Deduct")) Then
                _Delay_EarlyOut_Deduct = dr("Delay_EarlyOut_Deduct")
            End If
            If Not IsDBNull(dr("ConsiderNotComplete")) Then
                _ConsiderNotComplete = dr("ConsiderNotComplete")
            End If
            If Not IsDBNull(dr("NotCompleteSelection")) Then
                _NotCompleteSelection = dr("NotCompleteSelection")
            End If
            If Not IsDBNull(dr("NotCompleteValue")) Then
                _NotCompleteValue = dr("NotCompleteValue")
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
            If Not IsDBNull(dr("RemainingBalanceToBeRounded")) Then
                _RemainingBalanceToBeRounded = dr("RemainingBalanceToBeRounded")
            End If
            If Not IsDBNull(dr("IncluedLostTime")) Then
                _IncluedLostTime = dr("IncluedLostTime")
            End If
            If Not IsDBNull(dr("ExcludePendingLeaves")) Then
                _ExcludePendingLeaves = dr("ExcludePendingLeaves")
            End If
            If Not IsDBNull(dr("DelayEarly_CalcMethod")) Then
                _DelayEarly_CalcMethod = dr("DelayEarly_CalcMethod")
            End If
            If Not IsDBNull(dr("ConsiderOneDeduction_DelayEarly")) Then
                _ConsiderOneDeduction_DelayEarly = dr("ConsiderOneDeduction_DelayEarly")
            End If
            Return Me
        End Function

        Public Function GetBy_FK_TAPolicyId() As TAPolicy_DeductionPolicy

            Dim dr As DataRow
            dr = objDALTAPolicy_DeductionPolicy.GetBy_FK_TAPolicyId(_FK_TAPolicyId)
            If Not dr Is Nothing Then
                If Not IsDBNull(dr("DeductionPolicyId")) Then
                    _DeductionPolicyId = dr("DeductionPolicyId")
                End If
                If Not IsDBNull(dr("FK_TAPolicyId")) Then
                    _FK_TAPolicyId = dr("FK_TAPolicyId")
                End If
                If Not IsDBNull(dr("ConsiderAbsent")) Then
                    _ConsiderAbsent = dr("ConsiderAbsent")
                End If
                If Not IsDBNull(dr("ConsiderMissingIn")) Then
                    _ConsiderMissingIn = dr("ConsiderMissingIn")
                End If
                If Not IsDBNull(dr("ConsiderMissingOut")) Then
                    _ConsiderMissingOut = dr("ConsiderMissingOut")
                End If
                If Not IsDBNull(dr("ConsiderDelay_EarlyOut")) Then
                    _ConsiderDelay_EarlyOut = dr("ConsiderDelay_EarlyOut")
                End If
                If Not IsDBNull(dr("Delay_EarlyOut_FirstDeduct")) Then
                    _Delay_EarlyOut_FirstDeduct = dr("Delay_EarlyOut_FirstDeduct")
                End If
                If Not IsDBNull(dr("Delay_EarlyOut_Deduct")) Then
                    _Delay_EarlyOut_Deduct = dr("Delay_EarlyOut_Deduct")
                End If
                If Not IsDBNull(dr("ConsiderNotComplete")) Then
                    _ConsiderNotComplete = dr("ConsiderNotComplete")
                End If
                If Not IsDBNull(dr("NotCompleteSelection")) Then
                    _NotCompleteSelection = dr("NotCompleteSelection")
                End If
                If Not IsDBNull(dr("NotCompleteValue")) Then
                    _NotCompleteValue = dr("NotCompleteValue")
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
                If Not IsDBNull(dr("RemainingBalanceToBeRounded")) Then
                    _RemainingBalanceToBeRounded = dr("RemainingBalanceToBeRounded")
                End If
                If Not IsDBNull(dr("IncluedLostTime")) Then
                    _IncluedLostTime = dr("IncluedLostTime")
                End If
                If Not IsDBNull(dr("ExcludePendingLeaves")) Then
                    _ExcludePendingLeaves = dr("ExcludePendingLeaves")
                End If
                If Not IsDBNull(dr("DelayEarly_CalcMethod")) Then
                    _DelayEarly_CalcMethod = dr("DelayEarly_CalcMethod")
                End If
                If Not IsDBNull(dr("ConsiderOneDeduction_DelayEarly")) Then
                    _ConsiderOneDeduction_DelayEarly = dr("ConsiderOneDeduction_DelayEarly")
                End If
            End If

            Return Me
        End Function

#End Region

    End Class
End Namespace