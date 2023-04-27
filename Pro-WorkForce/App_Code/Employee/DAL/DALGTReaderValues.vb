Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES


Namespace TA.Lookup

    Public Class DALGTReaderValues
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private GTReaderValues_Select As String = "GTReaderValues_select"
        Private GTReaderValues_Select_All As String = "GTReaderValues_select_All"
        Private GTReaderValues_Insert As String = "GTReaderValues_Insert"
        Private GTReaderValues_Update As String = "GTReaderValues_Update"
        Private GTReaderValues_Delete As String = "GTReaderValues_Delete"
#End Region
#Region "Constructor"
        Public Sub New()

        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal Value As String, ByVal TextEn As String, ByVal TextAr As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(GTReaderValues_Insert, New SqlParameter("@Value", Value), _
               New SqlParameter("@TextEn", TextEn), _
               New SqlParameter("@TextAr", TextAr))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal Value As String, ByVal TextEn As String, ByVal TextAr As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(GTReaderValues_Update, New SqlParameter("@Value", Value), _
               New SqlParameter("@TextEn", TextEn), _
               New SqlParameter("@TextAr", TextAr))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal Value As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(GTReaderValues_Delete, New SqlParameter("@Value", Value))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal Value As String) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(GTReaderValues_Select, New SqlParameter("@Value", Value)).Rows(0)
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
                objColl = objDac.GetDataTable(GTReaderValues_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace
