Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.LookUp

Namespace TA.Accounts

    Public Class DALBU_MAHActivities
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private BU_MAHActivities_Select As String = "BU_MAHActivities_select"
        Private BU_MAHActivities_Select_All As String = "BU_MAHActivities_select_All"
        Private BU_MAHActivities_Insert As String = "BU_MAHActivities_Insert"
        Private BU_MAHActivities_Update As String = "BU_MAHActivities_Update"
        Private BU_MAHActivities_Delete As String = "BU_MAHActivities_Delete"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_AccountId As Integer, ByVal FK_ActivityId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(BU_MAHActivities_Insert, New SqlParameter("@FK_AccountId", FK_AccountId), _
               New SqlParameter("@FK_ActivityId", FK_ActivityId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FK_AccountId As Integer, ByVal FK_ActivityId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(BU_MAHActivities_Update, New SqlParameter("@FK_AccountId", FK_AccountId), _
               New SqlParameter("@FK_ActivityId", FK_ActivityId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal FK_AccountId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(BU_MAHActivities_Delete, New SqlParameter("@FK_AccountId", FK_AccountId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal FK_AccountId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(BU_MAHActivities_Select, New SqlParameter("@FK_AccountId", FK_AccountId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetAll() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(BU_MAHActivities_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace