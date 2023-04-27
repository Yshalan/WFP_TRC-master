Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports SmartV.DB
Imports TA.Lookup

Namespace TA.Admin
    Public Class DALTAPolicy_AbsentRule
#Region "Class Variables"
        Inherits MGRBase
        Private strConn As String
        Dim TAPolicy_AbsentRule_insert As String = "TAPolicy_AbsentRule_insert"
        Dim TAPolicy_AbsentRule_SelectAll As String = "TAPolicy_AbsentRule_SelectAll"
        Dim TAPolicy_AbsentRule_Select As String = "TAPolicy_AbsentRule_Select"
        Dim TAPolicy_AbsentRule_Update As String = "TAPolicy_AbsentRule_Update"
        Dim TAPolicy_AbsentRule_Delete As String = "TAPolicy_AbsentRule_Delete"
        Dim TAPolicy_AbsentRule_bulk_insert As String = "TAPolicy_AbsentRule_bulk_insert"
        Dim TAPolicy_AbsentRule_SelectAllByTAPolicyId As String = "TAPolicy_AbsentRule_SelectAllByTAPolicyId"
#End Region
#Region "Constructor"
        Public Sub New()

        End Sub
#End Region
#Region "Methods"
        Public Function Add_Bulk(ByRef AbsentRuleId As Integer, ByVal xml As String, ByVal intFK_TAPolicyId As Integer) As Integer
            '@CompanyId
            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@AbsentRuleId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, AbsentRuleId)
                errNo = objDac.AddUpdateDeleteSPTrans(TAPolicy_AbsentRule_bulk_insert, sqlOut, New SqlParameter("@xml", xml), New SqlParameter("@FK_TAPolicyId", intFK_TAPolicyId), New SqlParameter("@CREATED_BY", 1), New SqlParameter("@LAST_UPDATE_BY", 1))
                AbsentRuleId = sqlOut.Value
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function
        Public Function Add(ByVal FK_TAPolicyId As Integer, ByVal RuleName As String, ByVal RuleArabicName As String, ByVal AbsentRuleType As Integer, ByVal Variable1 As Integer, ByVal Variable2 As Integer) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(TAPolicy_AbsentRule_insert, _
               New SqlParameter("@FK_TAPolicyId", FK_TAPolicyId), _
               New SqlParameter("@RuleName", RuleName), _
               New SqlParameter("@RuleArabicName", RuleArabicName), _
               New SqlParameter("@AbsentRuleType", AbsentRuleType), _
               New SqlParameter("@Variable1", Variable1), _
               New SqlParameter("@Variable2", Variable2))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function
        'TAPolicy_AbsentRule_SelectAllByTAPolicyId
        Public Function GetAllByTAPolicyId(ByVal FK_TAPolicyId As Integer) As DataTable
            Dim objColl As DataTable
            objDac = DAC.getDAC()
            Try
                objColl = objDac.GetDataTable(TAPolicy_AbsentRule_SelectAllByTAPolicyId, New SqlParameter("@FK_TAPolicyId", FK_TAPolicyId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function
        Public Function GetAll() As DataTable
            Dim objColl As DataTable
            objDac = DAC.getDAC()
            Try
                objColl = objDac.GetDataTable(TAPolicy_AbsentRule_SelectAll, Nothing)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function Update(ByVal AbsentRuleId As Integer, ByVal FK_TAPolicyId As Integer, ByVal RuleName As String, ByVal RuleArabicName As String, ByVal AbsentRuleType As Integer, ByVal Variable1 As Integer, ByVal Variable2 As Integer, ByVal CREATED_BY As String, ByVal CREATED_DATE As Date, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As Date) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(TAPolicy_AbsentRule_Update, _
               New SqlParameter("@AbsentRuleId", AbsentRuleId), _
               New SqlParameter("@FK_TAPolicyId", FK_TAPolicyId), _
               New SqlParameter("@RuleName", RuleName), _
               New SqlParameter("@RuleArabicName", RuleArabicName), _
               New SqlParameter("@AbsentRuleType", AbsentRuleType), _
               New SqlParameter("@Variable1", Variable1), _
               New SqlParameter("@Variable2", Variable2), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@CREATED_DATE", CREATED_DATE), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
               New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Public Function Delete(ByVal AbsentRuleId As Integer) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(TAPolicy_AbsentRule_Delete, _
               New SqlParameter("@AbsentRuleId", AbsentRuleId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Public Function GetByPK(ByVal AbsentRuleId As Integer) As DataRow
            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(TAPolicy_AbsentRule_Select, New SqlParameter("@AbsentRuleId", AbsentRuleId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow
        End Function


#End Region
    End Class
End Namespace
