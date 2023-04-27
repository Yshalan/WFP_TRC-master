Imports System.Text
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Data

Public Class ExcelFileManager
    Implements IDisposable


#Region "Private Variable"
    Private _Path As String
#End Region

#Region "Public Property"
    Public Property Path() As String
        Get
            Return _Path
        End Get
        Set(ByVal value As String)
            _Path = value
        End Set
    End Property

    Public ReadOnly Property ConnectionString() As String
        Get
            Return ConfigurationManager.ConnectionStrings("ExcelIntegrationService").ConnectionString
        End Get
    End Property
#End Region

#Region "Public Procedure/Function"

    Public Function ReadExcelFile() As DataSet
        Dim ds As New DataSet()

        Dim connectionString As String = GetConnectionString()
        Dim conn As New OleDbConnection(connectionString)
        Try
            conn.Open()
            Dim cmd As New OleDbCommand()
            cmd.Connection = conn

            ' Get all Sheets in Excel File
            Dim dtSheet As DataTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)

            ' Loop through all Sheets to get data
            For Each dr As DataRow In dtSheet.Rows
                Dim sheetName As String = dr("TABLE_NAME").ToString()

                If Not sheetName.EndsWith("$") Then
                    Continue For
                End If

                ' Get all rows from the Sheet
                cmd.CommandText = "SELECT * FROM [" & sheetName & "]"

                Dim dt As New DataTable()
                dt.TableName = sheetName

                Dim da As New OleDbDataAdapter(cmd)
                da.Fill(dt)

                ds.Tables.Add(dt)
            Next

            cmd.Dispose()
            cmd = Nothing
        Catch ex As Exception

        Finally
            conn.Close()
            conn.Dispose()
        End Try

        Return ds
    End Function

    Public Sub InsertBulk(ByVal dt As DataTable)
        Dim sqlcon = New SqlConnection(ConnectionString)
        sqlcon.Open()
        Try
            Dim exists As String = Nothing
            Try
                Dim cmd As New SqlCommand("SELECT * FROM sysobjects where name = '" + dt.TableName & "'", sqlcon)
                exists = cmd.ExecuteScalar().ToString()
                If exists IsNot Nothing Then
                    Dim droptable As New SqlCommand("DROP TABLE " + dt.TableName, sqlcon)
                    droptable.ExecuteNonQuery()
                    exists = Nothing
                End If
            Catch exce As Exception
                exists = Nothing
            End Try


            For Each dc As DataColumn In dt.Columns
                If exists Is Nothing Then
                    Dim createtable As New SqlCommand(("CREATE TABLE " + dt.TableName & " (") + "[" + dc.ColumnName + "]" & " nvarchar(MAX))", sqlcon)
                    createtable.ExecuteNonQuery()
                    exists = dt.TableName
                Else
                    Dim addcolumn As New SqlCommand(("ALTER TABLE " + dt.TableName & " ADD ") + "[" + dc.ColumnName + "]" & " nvarchar(MAX)", sqlcon)
                    addcolumn.ExecuteNonQuery()
                End If
            Next


            ' copying the data from datatable to database table

            Using bulkcopy As New SqlBulkCopy(sqlcon)
                bulkcopy.DestinationTableName = dt.TableName
                bulkcopy.WriteToServer(dt)
            End Using

        Catch ex As Exception
            Throw ex
        Finally
            sqlcon.Close()
            sqlcon.Dispose()
        End Try
    End Sub

#End Region

#Region "Private Procedure/Function"
    Private Function GetConnectionString() As String
        Dim props As New Dictionary(Of String, String)()

        ' XLSX - Excel 2007, 2010, 2012, 2013
        props("Provider") = "Microsoft.ACE.OLEDB.12.0;"
        props("Extended Properties") = "Excel 12.0 XML"
        props("Data Source") = Path.Replace("\", "\\")

        ' XLS - Excel 2003 and Older
        'props["Provider"] = "Microsoft.Jet.OLEDB.4.0";
        'props["Extended Properties"] = "Excel 8.0";
        'props["Data Source"] = "C:\\MyExcel.xls";

        Dim sb As New StringBuilder()

        For Each prop As KeyValuePair(Of String, String) In props
            sb.Append(prop.Key)
            sb.Append("="c)
            sb.Append(prop.Value)
            sb.Append(";"c)
        Next

        Return sb.ToString()
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
