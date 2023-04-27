Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.OverTime

    Public Class DALOverTime_Decision
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private OverTime_Decision_Select As String = "OverTime_Decision_select"
        Private OverTime_Decision_Select_All As String = "OverTime_Decision_select_All"
        Private OverTime_Decision_Insert As String = "OverTime_Decision_Insert"
        Private OverTime_Decision_Update As String = "OverTime_Decision_Update"
        Private OverTime_Decision_Delete As String = "OverTime_Decision_Delete"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal Desc_En As String, ByVal Desc_Ar As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(OverTime_Decision_Insert, New SqlParameter("@Desc_En", Desc_En), _
               New SqlParameter("@Desc_Ar", Desc_Ar))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal DecisionID As Integer, ByVal Desc_En As String, ByVal Desc_Ar As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(OverTime_Decision_Update, New SqlParameter("@DecisionID", DecisionID), _
               New SqlParameter("@Desc_En", Desc_En), _
               New SqlParameter("@Desc_Ar", Desc_Ar))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal DecisionID As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(OverTime_Decision_Delete, New SqlParameter("@DecisionID", DecisionID))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal DecisionID As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(OverTime_Decision_Select, New SqlParameter("@DecisionID", DecisionID)).Rows(0)
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
                objColl = objDac.GetDataTable(OverTime_Decision_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetAllWithFilter(ByVal Filter As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("OverTime_Decision_Select_ByFilter", New SqlParameter("@Filter", Filter))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
#End Region


    End Class
End Namespace