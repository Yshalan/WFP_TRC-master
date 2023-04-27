Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace LKP

    Public Class DALLKP_table
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private LKP_table_Select As String = "LKP_table_select"
        Private LKP_table_Select_All As String = "LKP_table_select_All"
        Private LKP_table_Insert As String = "LKP_table_Insert"
        Private LKP_table_Update As String = "LKP_table_Update"
        Private LKP_table_Delete As String = "LKP_table_Delete"
        Private LKP_table_SelectByFK As String = "LKP_table_SelectByFK"
        Private LKP_Type_check_Has As String = "LKP_Type_check_Has"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_lkpType As Integer, ByVal lkpCode As String, ByVal lkpName As String, ByVal lkpNameAr As String, ByVal Remarks As String, ByVal RemarksAR As String, ByVal Other1 As String, ByVal Other2 As String, ByVal Other3 As String, ByVal Other4 As String, ByVal Other5 As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(LKP_table_Insert, New SqlParameter("@FK_lkpType", FK_lkpType), _
               New SqlParameter("@lkpCode", lkpCode), _
               New SqlParameter("@lkpName", lkpName), _
               New SqlParameter("@lkpNameAr", lkpNameAr), _
               New SqlParameter("@Remarks", Remarks), _
               New SqlParameter("@RemarksAR", RemarksAR), _
               New SqlParameter("@Other1", Other1), _
               New SqlParameter("@Other2", Other2), _
               New SqlParameter("@Other3", Other3), _
               New SqlParameter("@Other4", Other4), _
               New SqlParameter("@Other5", Other5))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal lkpId As Integer, ByVal FK_lkpType As Integer, ByVal lkpCode As String, ByVal lkpName As String, ByVal lkpNameAr As String, ByVal Remarks As String, ByVal RemarksAR As String, ByVal Other1 As String, ByVal Other2 As String, ByVal Other3 As String, ByVal Other4 As String, ByVal Other5 As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(LKP_table_Update, New SqlParameter("@lkpId", lkpId), _
               New SqlParameter("@FK_lkpType", FK_lkpType), _
               New SqlParameter("@lkpCode", lkpCode), _
               New SqlParameter("@lkpName", lkpName), _
               New SqlParameter("@lkpNameAr", lkpNameAr), _
               New SqlParameter("@Remarks", Remarks), _
               New SqlParameter("@RemarksAR", RemarksAR), _
               New SqlParameter("@Other1", Other1), _
               New SqlParameter("@Other2", Other2), _
               New SqlParameter("@Other3", Other3), _
               New SqlParameter("@Other4", Other4), _
               New SqlParameter("@Other5", Other5))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal lkpId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(LKP_table_Delete, New SqlParameter("@lkpId", lkpId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal lkpId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(LKP_table_Select, New SqlParameter("@lkpId", lkpId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetByKF(ByVal FK_lkpType As Integer) As DataTable
            objDac = DAC.getDAC()
            Dim dt As New DataTable
            Try
                dt = objDac.GetDataTable(LKP_table_SelectByFK, New SqlParameter("@FK", FK_lkpType))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return dt
        End Function

        Public Function GetAll() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(LKP_table_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function CheckHasValue(ByVal LKPTypeID As Integer) As DataRow
            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(LKP_Type_check_Has, New SqlParameter("@LKPTypeID", lkpTypeId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow
        End Function

#End Region


    End Class
End Namespace