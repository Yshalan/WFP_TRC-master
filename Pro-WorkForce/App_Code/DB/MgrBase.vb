Imports Microsoft.VisualBasic
Imports System.Data
Namespace TA.Lookup

    Public MustInherit Class MGRBase
#Region "MEMBERS"
        Protected objDac As SmartV.DB.DAC = Nothing
        Protected errNo As Integer = -1
        Protected strValue As String
        Protected objDS As DataSet = Nothing
        Protected objColl As DataTable = Nothing
        Protected connStr As String = String.Empty
        Protected logPath As String = String.Empty
#End Region
    End Class
End Namespace