Imports Microsoft.VisualBasic
Imports System.Data
Imports TA.Lookup
Imports SmartV.DB
Imports SmartV.UTILITIES
Imports System.Reflection

Public Class DALDurations
    Inherits MGRBase

#Region "Class Variables"
    Private strConn As String
    Private Durations_Select_All As String = "Durations_Select_All"
#End Region

#Region "Methods"
    Public Function GetAll() As DataTable

        objDac = DAC.getDAC()
        Dim objColl As DataTable = Nothing
        Try
            objColl = objDac.GetDataTable(Durations_Select_All, Nothing)
        Catch ex As Exception
            errNo = -11
            CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
        End Try
        Return objColl


    End Function
#End Region

End Class
