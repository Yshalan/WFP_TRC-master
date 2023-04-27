Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Card

    Public Class DALID_CARD_DESIGNS
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private ID_CARD_DESIGNS_Select As String = "ID_CARD_DESIGNS_select"
        Private ID_CARD_DESIGNS_Select_All As String = "ID_CARD_DESIGNS_select_All"
        Private ID_CARD_DESIGNS_Insert As String = "ID_CARD_DESIGNS_Insert"
        Private ID_CARD_DESIGNS_Update As String = "ID_CARD_DESIGNS_Update"
        Private ID_CARD_DESIGNS_Delete As String = "ID_CARD_DESIGNS_Delete"
        Private Select_Max_DesignId As String = "Select_Max_DesignId"

#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal DESIGN_ID As String, ByVal DESIGN_PATH As String, ByVal DESIGN_DESC As String, ByVal DESIGN_ARB_DESC As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(ID_CARD_DESIGNS_Insert, New SqlParameter("@DESIGN_ID", DESIGN_ID), _
               New SqlParameter("@DESIGN_PATH", IIf(DESIGN_PATH = Nothing, DBNull.Value, DESIGN_PATH)), _
               New SqlParameter("@DESIGN_DESC", DESIGN_DESC), _
               New SqlParameter("@DESIGN_ARB_DESC", DESIGN_ARB_DESC))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal DESIGN_ID As String, ByVal DESIGN_PATH As String, ByVal DESIGN_DESC As String, ByVal DESIGN_ARB_DESC As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(ID_CARD_DESIGNS_Update, New SqlParameter("@DESIGN_ID", DESIGN_ID), _
               New SqlParameter("@DESIGN_PATH", DESIGN_PATH), _
               New SqlParameter("@DESIGN_DESC", DESIGN_DESC), _
               New SqlParameter("@DESIGN_ARB_DESC", DESIGN_ARB_DESC))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal DESIGN_ID As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(ID_CARD_DESIGNS_Delete, New SqlParameter("@DESIGN_ID", DESIGN_ID))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal DESIGN_ID As String) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(ID_CARD_DESIGNS_Select, New SqlParameter("@DESIGN_ID", DESIGN_ID)).Rows(0)
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
                objColl = objDac.GetDataTable(ID_CARD_DESIGNS_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Get_MaxDesignId() As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Select_Max_DesignId, Nothing).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

#End Region


    End Class
End Namespace