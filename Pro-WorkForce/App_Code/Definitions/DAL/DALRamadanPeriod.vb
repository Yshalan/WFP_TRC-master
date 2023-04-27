Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup

Namespace TA.Definitions
    Public Class DALRamadanPeriod
        Inherits MGRBase

#Region "Class Variables"

        Private Ramadan_Period_Insert As String = "Ramadan_Period_Insert"
        Private Ramadan_Period_Update As String = "Ramadan_Period_Update"
        Private Ramadan_Period_Delete As String = "Ramadan_Period_Delete"
        Private Ramadan_Period_Get_All As String = "Ramadan_Period_Get_All"
        Private Ramadan_Period_Select_ByPK As String = "Ramadan_Period_Select_ByPK"
        Private RamadanPeriod_CheckIsRamadan As String = "RamadanPeriod_CheckIsRamadan"

#End Region

#Region "Constructor"

        Public Sub New()

        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef RamadanID As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal Created_By As String) As Integer

            objDac = DAC.getDAC()
            Dim sqlOut = New SqlParameter("@RamadanID", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, RamadanID)
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Ramadan_Period_Insert, sqlOut, New SqlParameter("@FromDate", FromDate), _
               New SqlParameter("@ToDate", ToDate), _
               New SqlParameter("@CREATED_BY", Created_By))
                If errNo = 0 Then
                    RamadanID = sqlOut.Value
                Else
                    errNo = errNo
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try

            Return errNo

        End Function

        Public Function Update(ByVal RamadanID As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal Last_Update_By As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Ramadan_Period_Update, New SqlParameter("@RamadanID", RamadanID), _
                New SqlParameter("@FromDate", FromDate), _
               New SqlParameter("@ToDate", ToDate), _
               New SqlParameter("@LAST_UPDATE_BY", Last_Update_By))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal RamadanID As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Ramadan_Period_Delete, New SqlParameter("@RamadanID", RamadanID))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetAll() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Ramadan_Period_Get_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Get_IsRamadan(ByVal SearchDate As DateTime) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(RamadanPeriod_CheckIsRamadan, New SqlParameter("@SearchDate", SearchDate)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetByPK(ByVal RamadnaID As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Ramadan_Period_Select_ByPK, New SqlParameter("@RamadanID", RamadnaID)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

#End Region

    End Class

End Namespace
