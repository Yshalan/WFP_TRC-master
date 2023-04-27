Imports Microsoft.VisualBasic

Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection

Imports TA.Lookup
Imports SmartV.DB
Imports SmartV.UTILITIES


Namespace TA.Admin

    Public Class DALCard_Designations
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Card_Designations_Select As String = "Card_Designations_select"
        Private Card_Designations_Select_All As String = "Card_Designations_select_All"
        Private Card_Designations_Insert As String = "Card_Designations_Insert"
        Private Card_Designations_Update As String = "Card_Designations_Update"
        Private Card_Designations_Delete As String = "Card_Designations_Delete"
        Private Card_Designations_Select_ByDesignation As String = "Card_Designations_Select_ByDesignation"

#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal Card_DesignationId As Integer, ByVal Fk_DesignationId As Integer, ByVal Fk_CardTypeId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Card_Designations_Insert, New SqlParameter("@Card_DesignationId", Card_DesignationId), _
               New SqlParameter("@Fk_DesignationId", Fk_DesignationId), _
               New SqlParameter("@Fk_CardTypeId", Fk_CardTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal Card_DesignationId As Integer, ByVal Fk_DesignationId As Integer, ByVal Fk_CardTypeId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Card_Designations_Update, New SqlParameter("@Card_DesignationId", Card_DesignationId), _
               New SqlParameter("@Fk_DesignationId", Fk_DesignationId), _
               New SqlParameter("@Fk_CardTypeId", Fk_CardTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal Fk_CardTypeId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Card_Designations_Delete, New SqlParameter("@Fk_CardTypeId", Fk_CardTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function


        Public Function GetByPK(ByVal Card_DesignationId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Card_Designations_Select, New SqlParameter("@Card_DesignationId", Card_DesignationId)).Rows(0)
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
                objColl = objDac.GetDataTable(Card_Designations_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetAllByCardType(ByVal Fk_CardTypeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Card_Designations_Select, New SqlParameter("@Fk_CardTypesId", Fk_CardTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAllByDesignation(ByVal Fk_DesignationId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Card_Designations_Select_ByDesignation, New SqlParameter("@DesignationId", Fk_DesignationId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
#End Region


    End Class
End Namespace
