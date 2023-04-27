Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Admin

    Public Class DALTAPolicy_DeductionPolicy
        Inherits MGRBase



#Region "Class Variables"

        Private strConn As String
        Private TAPolicy_DeductionPolicy_Select As String = "TAPolicy_DeductionPolicy_select"
        Private TAPolicy_DeductionPolicy_Select_All As String = "TAPolicy_DeductionPolicy_select_All"
        Private TAPolicy_DeductionPolicy_Insert As String = "TAPolicy_DeductionPolicy_Insert"
        Private TAPolicy_DeductionPolicy_Update As String = "TAPolicy_DeductionPolicy_Update"
        Private TAPolicy_DeductionPolicy_Delete As String = "TAPolicy_DeductionPolicy_Delete"
        Private TAPolicy_DeductionPolicy_Select_By_FK_TAPolicyId As String = "TAPolicy_DeductionPolicy_Select_By_FK_TAPolicyId"

#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef DeductionPolicyId As Integer, ByVal FK_TAPolicyId As Integer, ByVal ConsiderAbsent As Boolean, ByVal ConsiderMissingIn As Boolean, ByVal ConsiderMissingOut As Boolean, ByVal ConsiderDelay_EarlyOut As Boolean, ByVal Delay_EarlyOut_FirstDeduct As Integer, ByVal Delay_EarlyOut_Deduct As Integer, ByVal ConsiderNotComplete As Boolean, ByVal NotCompleteSelection As Integer, ByVal NotCompleteValue As Integer, ByVal CREATED_BY As String, ByVal RemainingBalanceToBeRounded As Boolean, ByVal IncluedLostTime As Boolean, ByVal ExcludePendingLeaves As Boolean, ByVal DelayEarly_CalcMethod As Integer, ByVal ConsiderOneDeduction_DelayEarly As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@DeductionPolicyId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, DeductionPolicyId)
                errNo = objDac.AddUpdateDeleteSPTrans(TAPolicy_DeductionPolicy_Insert, sqlOut, New SqlParameter("@FK_TAPolicyId", FK_TAPolicyId),
               New SqlParameter("@ConsiderAbsent", ConsiderAbsent),
               New SqlParameter("@ConsiderMissingIn", ConsiderMissingIn),
               New SqlParameter("@ConsiderMissingOut", ConsiderMissingOut),
               New SqlParameter("@ConsiderDelay_EarlyOut", ConsiderDelay_EarlyOut),
               New SqlParameter("@Delay_EarlyOut_FirstDeduct", Delay_EarlyOut_FirstDeduct),
               New SqlParameter("@Delay_EarlyOut_Deduct", Delay_EarlyOut_Deduct),
               New SqlParameter("@ConsiderNotComplete", ConsiderNotComplete),
               New SqlParameter("@NotCompleteSelection", NotCompleteSelection),
               New SqlParameter("@NotCompleteValue", NotCompleteValue),
               New SqlParameter("@CREATED_BY", CREATED_BY),
               New SqlParameter("@RemainingBalanceToBeRounded", RemainingBalanceToBeRounded),
               New SqlParameter("@IncluedLostTime", IncluedLostTime),
               New SqlParameter("@ExcludePendingLeaves", ExcludePendingLeaves),
               New SqlParameter("@DelayEarly_CalcMethod", DelayEarly_CalcMethod),
               New SqlParameter("@ConsiderOneDeduction_DelayEarly", ConsiderOneDeduction_DelayEarly))
                If errNo = 0 Then DeductionPolicyId = sqlOut.Value Else DeductionPolicyId = 0
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal DeductionPolicyId As Integer, ByVal FK_TAPolicyId As Integer, ByVal ConsiderAbsent As Boolean, ByVal ConsiderMissingIn As Boolean, ByVal ConsiderMissingOut As Boolean, ByVal ConsiderDelay_EarlyOut As Boolean, ByVal Delay_EarlyOut_FirstDeduct As Integer, ByVal Delay_EarlyOut_Deduct As Integer, ByVal ConsiderNotComplete As Boolean, ByVal NotCompleteSelection As Integer, ByVal NotCompleteValue As Integer, ByVal LAST_UPDATE_BY As String, ByVal RemainingBalanceToBeRounded As Boolean, ByVal IncluedLostTime As Boolean, ByVal ExcludePendingLeaves As Boolean, ByVal DelayEarly_CalcMethod As Integer, ByVal ConsiderOneDeduction_DelayEarly As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(TAPolicy_DeductionPolicy_Update, New SqlParameter("@DeductionPolicyId", DeductionPolicyId), _
               New SqlParameter("@FK_TAPolicyId", FK_TAPolicyId), _
               New SqlParameter("@ConsiderAbsent", ConsiderAbsent), _
               New SqlParameter("@ConsiderMissingIn", ConsiderMissingIn), _
               New SqlParameter("@ConsiderMissingOut", ConsiderMissingOut), _
               New SqlParameter("@ConsiderDelay_EarlyOut", ConsiderDelay_EarlyOut), _
               New SqlParameter("@Delay_EarlyOut_FirstDeduct", Delay_EarlyOut_FirstDeduct), _
               New SqlParameter("@Delay_EarlyOut_Deduct", Delay_EarlyOut_Deduct), _
               New SqlParameter("@ConsiderNotComplete", ConsiderNotComplete), _
               New SqlParameter("@NotCompleteSelection", NotCompleteSelection), _
               New SqlParameter("@NotCompleteValue", NotCompleteValue), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
               New SqlParameter("@RemainingBalanceToBeRounded", RemainingBalanceToBeRounded), _
               New SqlParameter("@IncluedLostTime", IncluedLostTime), _
               New SqlParameter("@ExcludePendingLeaves", ExcludePendingLeaves), _
               New SqlParameter("@DelayEarly_CalcMethod", DelayEarly_CalcMethod), _
               New SqlParameter("@ConsiderOneDeduction_DelayEarly", ConsiderOneDeduction_DelayEarly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal DeductionPolicyId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(TAPolicy_DeductionPolicy_Delete, New SqlParameter("@DeductionPolicyId", DeductionPolicyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal DeductionPolicyId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(TAPolicy_DeductionPolicy_Select, New SqlParameter("@DeductionPolicyId", DeductionPolicyId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetAll() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(TAPolicy_DeductionPolicy_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetBy_FK_TAPolicyId(ByVal FK_TAPolicyId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(TAPolicy_DeductionPolicy_Select_By_FK_TAPolicyId, New SqlParameter("@FK_TAPolicyId", FK_TAPolicyId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function
#End Region


    End Class
End Namespace