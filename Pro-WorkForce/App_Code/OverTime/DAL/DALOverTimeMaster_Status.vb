Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.OverTime

    Public Class DALOverTimeMaster_Status
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private OverTimeMaster_Status_Select As String = "OverTimeMaster_Status_select"
        Private OverTimeMaster_Status_Select_All As String = "OverTimeMaster_Status_select_All"
        Private OverTimeMaster_Status_Insert As String = "OverTimeMaster_Status_Insert"
        Private OverTimeMaster_Status_Update As String = "OverTimeMaster_Status_Update"
        Private OverTimeMaster_Status_Delete As String = "OverTimeMaster_Status_Delete"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal Desc_En As String, ByVal Desc_Ar As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(OverTimeMaster_Status_Insert, New SqlParameter("@Desc_En", Desc_En), _
               New SqlParameter("@Desc_Ar", Desc_Ar))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal StatusID As Integer, ByVal Desc_En As String, ByVal Desc_Ar As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(OverTimeMaster_Status_Update, New SqlParameter("@StatusID", StatusID), _
               New SqlParameter("@Desc_En", Desc_En), _
               New SqlParameter("@Desc_Ar", Desc_Ar))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal StatusID As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(OverTimeMaster_Status_Delete, New SqlParameter("@StatusID", StatusID))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal StatusID As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(OverTimeMaster_Status_Select, New SqlParameter("@StatusID", StatusID)).Rows(0)
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
                objColl = objDac.GetDataTable(OverTimeMaster_Status_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace