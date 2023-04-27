Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.LookUp


Namespace TA.Accounts

    Public Class DALBU_MAH_Agent
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private BU_MAH_Agent_Select As String = "BU_MAH_Agent_select"
        Private BU_MAH_Agent_Select_All As String = "BU_MAH_Agent_select_All"
        Private BU_MAH_Agent_Insert As String = "BU_MAH_Agent_Insert"
        Private BU_MAH_Agent_Update As String = "BU_MAH_Agent_Update"
        Private BU_MAH_Agent_Delete As String = "BU_MAH_Agent_Delete"
        Private BU_MAH_Agent_Select_By_MAH_ID As String = "BU_MAH_Agent_Select_By_MAH_ID"
#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_MAHId As Integer, ByVal FK_AgentId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(BU_MAH_Agent_Insert, New SqlParameter("@FK_MAHId", FK_MAHId), _
               New SqlParameter("@FK_AgentId", FK_AgentId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FK_MAHId As Integer, ByVal FK_AgentId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(BU_MAH_Agent_Update, New SqlParameter("@FK_MAHId", FK_MAHId), _
               New SqlParameter("@FK_AgentId", FK_AgentId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal FK_MAHId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(BU_MAH_Agent_Delete, New SqlParameter("@FK_MAHId", FK_MAHId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal FK_MAHId As Integer, ByVal FK_AgentId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(BU_MAH_Agent_Select, New SqlParameter("@FK_MAHId", FK_MAHId), _
               New SqlParameter("@FK_AgentId", FK_AgentId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetAll() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(BU_MAH_Agent_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAllByMAHID(ByVal FK_MAHId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(BU_MAH_Agent_Select_By_MAH_ID, New SqlParameter("@FK_MAHId", FK_MAHId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace