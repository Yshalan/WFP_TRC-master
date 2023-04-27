Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports SmartV.DB
Imports TA.Lookup

Namespace TA.Admin
    Public Class DALTAPolicy_Violation
        Inherits MGRBase
#Region "Class Variables"

        Private strConn As String
        Dim TAPolicy_Violations_insert As String = "TAPolicy_Violations_insert"
        Dim TAPolicy_Violations_SelectAll As String = "TAPolicy_Violations_SelectAll"
        Dim TAPolicy_Violations_Select As String = "TAPolicy_Violations_Select"
        Dim TAPolicy_Violations_Update As String = "TAPolicy_Violations_Update"
        Dim TAPolicy_Violations_Delete As String = "TAPolicy_Violations_Delete"
        Dim TAPolicy_Violations_SelectAllByTAPolicy As String = "TAPolicy_Violations_SelectAllByTAPolicy"
        Dim TAPolicy_Violations_bulk_insert As String = "TAPolicy_Violations_bulk_insert"
#End Region
#Region "Constructor"
        Public Sub New()

        End Sub
#End Region
#Region "Methods"

        Public Function Add_Bulk(ByVal xml As String, ByVal intFK_TAPolicyId As Integer) As Integer
            '@CompanyId
            objDac = DAC.getDAC()
            Try

                errNo = objDac.AddUpdateDeleteSPTrans(TAPolicy_Violations_bulk_insert, New SqlParameter("@xml", xml), New SqlParameter("@FK_TAPolicyId", intFK_TAPolicyId), New SqlParameter("@CREATED_BY", 1), New SqlParameter("@LAST_UPDATE_BY", 1))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function
        Public Function Add(ByVal FK_TAPolicyId As Integer, ByVal ViolationName As String, ByVal ViolationArabicName As String, ByVal ViolationRuleType As Integer, ByVal Variable1 As Integer, ByVal Variable2 As Integer, ByVal Variable3 As Integer, ByVal FK_ViolationActionId As Integer, ByVal FK_ViolationActionId2 As Integer, ByVal FK_ViolationActionId3 As Integer, ByVal FK_ViolationActionId4 As Integer, ByVal FK_ViolationActionId5 As Integer, ByVal ScenarioMode As Integer) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(TAPolicy_Violations_insert, _
               New SqlParameter("@FK_TAPolicyId", FK_TAPolicyId), _
               New SqlParameter("@ViolationName", ViolationName), _
               New SqlParameter("@ViolationArabicName", ViolationArabicName), _
               New SqlParameter("@ViolationRuleType", ViolationRuleType), _
               New SqlParameter("@Variable1", Variable1), _
               New SqlParameter("@Variable2", Variable2), _
               New SqlParameter("@Variable3", Variable3), _
               New SqlParameter("@FK_ViolationActionId", FK_ViolationActionId), _
               New SqlParameter("@FK_ViolationActionId2", FK_ViolationActionId2), _
               New SqlParameter("@FK_ViolationActionId3", FK_ViolationActionId3), _
               New SqlParameter("@FK_ViolationActionId4", FK_ViolationActionId4), _
               New SqlParameter("@FK_ViolationActionId5", FK_ViolationActionId5), _
               New SqlParameter("@ScenarioMode", ScenarioMode))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function
        'GetAllByPolicyId
        Public Function GetAllByPolicyId(ByVal intFK_TAPolicyId As Integer) As DataTable
            Dim objColl As DataTable
            objDac = DAC.getDAC()
            Try
                objColl = objDac.GetDataTable(TAPolicy_Violations_SelectAllByTAPolicy, New SqlParameter("@FK_TAPolicyId", intFK_TAPolicyId))

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
                objColl = objDac.GetDataTable(TAPolicy_Violations_SelectAll, Nothing)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function Update(ByVal ViolationId As Integer, ByVal FK_TAPolicyId As Integer, ByVal ViolationName As String, ByVal ViolationArabicName As String, ByVal ViolationRuleType As Integer, ByVal Variable1 As Integer, ByVal Variable2 As Integer, ByVal Variable3 As Integer, ByVal FK_ViolationActionId As Integer, ByVal FK_ViolationActionId2 As Integer, ByVal FK_ViolationActionId3 As Integer, ByVal FK_ViolationActionId4 As Integer, ByVal FK_ViolationActionId5 As Integer, ByVal ScenarioMode As Integer) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(TAPolicy_Violations_Update, _
               New SqlParameter("@ViolationId", ViolationId), _
               New SqlParameter("@FK_TAPolicyId", FK_TAPolicyId), _
               New SqlParameter("@ViolationName", ViolationName), _
               New SqlParameter("@ViolationArabicName", ViolationArabicName), _
               New SqlParameter("@ViolationRuleType", ViolationRuleType), _
               New SqlParameter("@Variable1", Variable1), _
               New SqlParameter("@Variable2", Variable2), _
               New SqlParameter("@Variable3", Variable3), _
               New SqlParameter("@FK_ViolationActionId", FK_ViolationActionId), _
               New SqlParameter("@FK_ViolationActionId2", FK_ViolationActionId2), _
               New SqlParameter("@FK_ViolationActionId3", FK_ViolationActionId3), _
               New SqlParameter("@FK_ViolationActionId4", FK_ViolationActionId4), _
               New SqlParameter("@FK_ViolationActionId5", FK_ViolationActionId5), _
               New SqlParameter("@ScenarioMode", ScenarioMode))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Public Function Delete(ByVal ViolationId As Integer) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(TAPolicy_Violations_Delete, _
               New SqlParameter("@ViolationId", ViolationId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Public Function GetByPK(ByVal ViolationId As Integer) As DataRow
            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(TAPolicy_Violations_Select, New SqlParameter("@ViolationId", ViolationId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow
        End Function


#End Region
    End Class
End Namespace
