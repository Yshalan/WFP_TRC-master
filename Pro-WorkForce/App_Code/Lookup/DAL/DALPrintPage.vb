Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports System.Linq
Imports TA.Lookup

Namespace TA.Lookup

    Public Class DALPrintPage
        Inherits MGRBase

#Region "Class Variables"

        Private dt As DataTable = Nothing
        Private clmnHash As Hashtable = Nothing
        Private spHash As Hashtable = Nothing

#End Region

#Region "Constructor"

        Public Sub New()

        End Sub

#End Region

#Region "Methods"

        Public Function GetPrintDetails() As DataTable
            spHash = SessionVariables.SpName
            Dim dt As DataTable = Nothing
            Dim objdac As New DAC
            objdac = DAC.getDAC()
            If Not spHash.Item("ParameterList") Is Nothing Then
                dt = objdac.GetDataTable(spHash.Item("sp"), spHash.Item("ParameterList"))
            Else
                dt = objdac.GetDataTable(spHash.Item("sp"), Nothing)
            End If

            Dim newdt As DataTable = New DataTable
            newdt = dt.Copy
            Dim column As DataColumn
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                For Each column In dt.Columns
                    If spHash.ContainsKey(column.ColumnName.ToString()) Then
                        newdt.Columns(column.ColumnName).ColumnName = spHash.Item(column.ColumnName.ToString())
                    Else
                        newdt.Columns.Remove(column.ColumnName.ToString.Trim)
                    End If
                Next
            End If

            Return newdt
        End Function

        Public Function FilterDetails(ByVal strFilterCond As String) As DataTable
            spHash = SessionVariables.SpName
            Dim dt As DataTable = Nothing
            Dim objdac As New DAC
            objdac = DAC.getDAC()
            If Not spHash.Item("ParameterList") Is Nothing Then
                dt = objdac.GetDataTable(spHash.Item("sp"), spHash.Item("ParameterList"))
            Else
                dt = objdac.GetDataTable(spHash.Item("sp"), Nothing)
            End If

            Dim newdt As DataTable = New DataTable
            Dim arRows As DataRow()

            arRows = dt.Select(strFilterCond)
            If arRows.Count > 0 Then
                newdt = arRows.CopyToDataTable()
            Else
                newdt.Clear()
            End If


            Dim column As DataColumn

            If newdt IsNot Nothing Then
                If newdt.Rows.Count > 0 Then
                    For Each column In dt.Columns
                        If spHash.ContainsKey(column.ColumnName.ToString()) Then
                            newdt.Columns(column.ColumnName).ColumnName = spHash.Item(column.ColumnName.ToString())
                        Else
                            newdt.Columns.Remove(column.ColumnName.ToString.Trim)
                        End If
                    Next
                End If
            End If

            Return newdt
        End Function

        Public Function getKey(ByVal Value As String) As String
            spHash = SessionVariables.SpName
            If spHash.ContainsValue(Value) Then
                For Each aKey As String In spHash.Keys
                    If spHash(aKey).Equals(Value) = True Then
                        Return aKey
                    End If
                Next
            Else
                Return ""
            End If
            Return ""
        End Function

        Public Function GenerateSessions(ByVal spName As String, ByVal columnsToDisplay() As String, ByVal columnsNamesToDisplay() As String, ByVal Parameters() As String, ByVal ParameterValues() As String) As Integer
            Dim intResult As Integer = -1
            Dim spHash As New Hashtable()
            Dim param As String = ""
            Try
                spHash.Add("sp", spName)
                If columnsToDisplay.Count() = columnsNamesToDisplay.Count() And columnsToDisplay.Count() > 0 Then
                    For counter1 = 0 To columnsToDisplay.Count - 1
                        spHash.Add(columnsToDisplay(counter1), columnsNamesToDisplay(counter1))
                    Next counter1
                End If
                If (Parameters IsNot Nothing) Then
                    If Parameters.Count() = ParameterValues.Count() Then
                        Dim strParameterList As System.Data.SqlClient.SqlParameter = Nothing
                        For counter2 = 0 To Parameters.Count - 1
                            param = "@" + Parameters(counter2)
                            strParameterList = New SqlParameter(param, ParameterValues(counter2))
                        Next counter2
                        spHash.Add("ParameterList", strParameterList)
                    Else
                        spHash.Add("ParameterList", Nothing)
                    End If
                End If

                SessionVariables.SpName = spHash
                intResult = 0
            Catch ex As Exception
                SessionVariables.SpName = Nothing
                intResult = -1
            End Try
            Return intResult
        End Function

#End Region

    End Class

End Namespace
