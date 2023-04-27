Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.DailyTasks

    Public Class DALREADER_KEYS
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private READER_KEYS_Select As String = "READER_KEYS_select"
        Private READER_KEYS_Select_All As String = "READER_KEYS_select_All"
        Private READER_KEYS_Insert As String = "READER_KEYS_Insert"
        Private READER_KEYS_Update As String = "READER_KEYS_Update"
        Private READER_KEYS_Delete As String = "READER_KEYS_Delete"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal READER_KEY As String, ByVal CHANGE_TO As String, ByVal Type As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(READER_KEYS_Insert, New SqlParameter("@READER_KEY", READER_KEY), _
               New SqlParameter("@CHANGE_TO", CHANGE_TO), _
               New SqlParameter("@Type", Type))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal READER_KEY As String, ByVal CHANGE_TO As String, ByVal Type As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(READER_KEYS_Update, New SqlParameter("@READER_KEY", READER_KEY), _
               New SqlParameter("@CHANGE_TO", CHANGE_TO), _
               New SqlParameter("@Type", Type))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal READER_KEY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(READER_KEYS_Delete, New SqlParameter("@READER_KEY", READER_KEY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal READER_KEY As String) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(READER_KEYS_Select, New SqlParameter("@READER_KEY", READER_KEY)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetAll() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(READER_KEYS_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace