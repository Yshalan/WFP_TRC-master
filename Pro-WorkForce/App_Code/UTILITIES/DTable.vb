Imports System
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Reflection

Namespace SmartV.UTILITIES
    Public Class DTable
        Public Shared Function IsValidDataTable(ByVal dt As System.Data.DataTable) As Boolean
            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        End Function
        Public Shared Function IsValidDataRow(ByVal dr As System.Data.DataRow) As Boolean
            If Not dr Is Nothing Then
                If dr.ItemArray.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        End Function
        Public Shared Function GetValue(ByVal val As Object, ByVal Type As String) As Object
            If Not val Is DBNull.Value Then
                Return val
            Else
                Select Case Type
                    Case "S" 'String
                        Return ""
                    Case "D" 'Drop Down List
                        Return "-1"
                    Case "I" 'Integer
                        Return 0
                    Case "T" 'Date Time
                        Return DBNull.Value
                    Case "C" 'Check Box
                        Return False
                    Case Else
                        Return ""
                End Select
            End If
        End Function

    End Class


    Public Module DataTableExtensions
        <System.Runtime.CompilerServices.Extension()> _
        Public Sub SetColumnsOrder(ByRef table As DataTable, ParamArray columnNames As [String]())
            Dim columnIndex As Integer = 0
            For Each columnName As String In columnNames
                table.Columns(columnName).SetOrdinal(columnIndex)
                columnIndex += 1
            Next
        End Sub
    End Module
End Namespace
