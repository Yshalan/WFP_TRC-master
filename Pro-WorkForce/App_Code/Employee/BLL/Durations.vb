Imports Microsoft.VisualBasic
Imports System.Data

Public Class Durations

#Region "Class Variables"


    Private objDALDurations As DALDurations

#End Region

    Public Function GetAll() As DataTable
        objDALDurations = New DALDurations
        Return objDALDurations.GetAll()

    End Function

End Class
