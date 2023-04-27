Imports System.Data
Imports System.Data.OleDb
Imports System.Data.Common
Imports System.Data.SqlClient
Imports SmartV.UTILITIES
Imports System
Imports System.Configuration
Imports System.Reflection




Public Structure ResultStructure
    ' structure that can be used for returning result from a function or procedure
    Dim Success As Boolean
    Dim ErrCode As String
    Dim ErrMsg As String
    Dim ReturnValue As String
End Structure
Namespace SmartV.DB

    ''' <summary>
    ''' Data Access Component Class for Microsoft SQL Server
    ''' </summary>
    ''' <remarks>Supports SQL Server 7.0, 2000, 2005, 2008</remarks>
    Public Class DAC

#Region "MEMBERS"
        Private _strConn As String
        Private _intUserID As Integer
        Private _isCommented As Boolean = True
        Private DACSQLTrans As SqlTransaction
        Private _myConnection As New SqlConnection
        Private _OledbConnection As New OleDbConnection
#End Region

#Region "Shared Members"
        Private Shared DACObjects As New Hashtable
#End Region

#Region "PROPERTIES"

        Public Property ConnStr() As String
            Get
                Return _strConn
            End Get
            Set(ByVal strValue As String)
                _strConn = strValue
            End Set
        End Property

        Public Property UserID() As Integer
            Get
                Return _intUserID
            End Get
            Set(ByVal value As Integer)
                _intUserID = value
            End Set
        End Property

        Public Property isCommitted() As Boolean
            Get
                Return _isCommented
            End Get
            Set(ByVal value As Boolean)
                _isCommented = value
            End Set
        End Property

#End Region

#Region "ENUMERATIONS"
        Public Enum SqlOpType
            Insert
            Update
            Delete
        End Enum
#End Region

#Region "CONSTRUCTORS"
        Public Sub New()
            _strConn = String.Empty
            _intUserID = 0
        End Sub

        Public Sub New(ByVal strCon As String)
            _strConn = strCon
            _myConnection.ConnectionString = strCon
            Try
                If (strCon Is Nothing) OrElse (strCon.Trim.Length < 1) Then
                    Throw New Exception("Connection String Not Provided")
                End If
                _strConn = strCon
            Catch ex As Exception
                'CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, Reflection.MethodBase.GetCurrentMethod.Name)
                Dim pagePath(1) As String
                Dim i As Integer = ex.StackTrace.IndexOf(" in ")
                Dim lasti As Integer = ex.StackTrace.IndexOf(":line ")
                pagePath(0) = ex.StackTrace.Substring(i + 3, (lasti + 11) - i)
                CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name & " --Page:" & pagePath(0))

            End Try
        End Sub

        Public Sub New(ByVal strCon As String, ByVal intUserID As Integer)
            Try
                If (strCon Is Nothing) OrElse (strCon.Trim.Length < 1) Then
                    Throw New Exception("Connection String Not Provided")
                End If
                If (intUserID = Nothing) OrElse (intUserID = 0) Then
                    Throw New Exception("UserID Not Provided")
                End If
                _strConn = strCon
                _intUserID = intUserID
            Catch ex As Exception
                'CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, Reflection.MethodBase.GetCurrentMethod.Name)
                Dim pagePath(1) As String
                Dim i As Integer = ex.StackTrace.IndexOf(" in ")
                Dim lasti As Integer = ex.StackTrace.IndexOf(":line ")
                pagePath(0) = ex.StackTrace.Substring(i + 3, (lasti + 11) - i)
                CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name & " --Page:" & pagePath(0))

            End Try
        End Sub
#End Region

#Region "METHODS"
        ''' <summary>
        ''' Function to execute a Query
        ''' </summary>
        ''' <param name="strSql">SQL Command Text</param>
        ''' <returns>DataSet filled with the queried Results</returns>
        ''' <remarks></remarks>
        Public Function GetDataSet(ByVal strSql As String) As DataSet
            Dim dataSet As DataSet = Nothing
            Dim filledRows As Integer = 0
            If strSql.Trim.Length > 0 AndAlso _strConn.Trim.Length > 0 Then
                Using sqlCn As New SqlConnection(_strConn)
                    Using sqlDa As New SqlDataAdapter(strSql, sqlCn)
                        Try
                            dataSet = New DataSet
                            filledRows = sqlDa.Fill(dataSet)
                        Catch sqlEx As SqlException
                            For Each sqlE As SqlError In sqlEx.Errors
                                CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), sqlE.Message, Reflection.MethodBase.GetCurrentMethod.Name)
                            Next
                            dataSet = Nothing
                        Catch ex As Exception
                            'CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, Reflection.MethodBase.GetCurrentMethod.Name)
                            Dim pagePath(1) As String
                            Dim i As Integer = ex.StackTrace.IndexOf(" in ")
                            Dim lasti As Integer = ex.StackTrace.IndexOf(":line ")
                            pagePath(0) = ex.StackTrace.Substring(i + 3, (lasti + 11) - i)
                            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name & " --Page:" & pagePath(0))

                            dataSet = Nothing
                        Finally
                            strSql = String.Empty
                            sqlCn.Close()

                        End Try
                    End Using
                End Using
                Return dataSet
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        ''' Function to execute a Query
        ''' </summary>
        ''' <param name="strSql">SQL Command Text</param>
        ''' <returns>DataTable filled with the queried Results</returns>
        ''' <remarks></remarks>

        Public Function GetDataTable(ByVal strSql As String) As DataTable
            Dim dataTable As DataTable = Nothing
            Dim filledRows As Integer = 0
            If strSql.Trim.Length > 0 AndAlso _strConn.Trim.Length > 0 Then
                Using sqlCn As New SqlConnection(_strConn)
                    Using sqlDa As New SqlDataAdapter(strSql, sqlCn)
                        Try
                            dataTable = New DataTable
                            filledRows = sqlDa.Fill(dataTable)
                        Catch sqlEx As SqlException
                            For Each sqlE As SqlError In sqlEx.Errors
                                CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), sqlE.Message, Reflection.MethodBase.GetCurrentMethod.Name)
                            Next
                            dataTable = Nothing
                        Catch ex As Exception
                            'CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, Reflection.MethodBase.GetCurrentMethod.Name)
                            Dim pagePath(1) As String
                            Dim i As Integer = ex.StackTrace.IndexOf(" in ")
                            Dim lasti As Integer = ex.StackTrace.IndexOf(":line ")
                            pagePath(0) = ex.StackTrace.Substring(i + 3, (lasti + 11) - i)
                            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name & " --Page:" & pagePath(0))

                            dataTable = Nothing
                        Finally
                            strSql = String.Empty
                            sqlCn.Close()
                        End Try
                    End Using
                    If filledRows > 0 Then
                        Return dataTable
                    Else
                        Return Nothing
                    End If
                End Using
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        ''' Function to execute a Stored Procedure
        ''' </summary>
        ''' <param name="spName">SQL Command Text</param>
        ''' <returns>DataTable filled with the queried Results</returns>
        ''' <remarks></remarks>
        Public Function GetDataTable(ByVal spName As String, ByVal ParamArray sqlParams() As SqlParameter) As DataTable
            Dim dataTable As DataTable = Nothing
            Dim filledRows As Integer = 0
            If spName.Trim.Length > 0 AndAlso _strConn.Trim.Length > 0 Then
                Using sqlCn As New SqlConnection(_strConn)
                    Using sqlCmd As SqlCommand = sqlCn.CreateCommand
                        With sqlCmd
                            .CommandText = spName
                            .CommandTimeout = 6000
                            .CommandType = CommandType.StoredProcedure
                            .Connection = sqlCn
                            If (sqlParams IsNot Nothing) AndAlso (sqlParams.Length > 0) Then .Parameters.AddRange(sqlParams)
                        End With
                        Using sqlDa As New SqlDataAdapter(sqlCmd)
                            Try
                                dataTable = New DataTable
                                filledRows = sqlDa.Fill(dataTable)
                            Catch sqlEx As SqlException
                                For Each sqlE As SqlError In sqlEx.Errors
                                    CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), sqlE.Message, Reflection.MethodBase.GetCurrentMethod.Name)
                                Next
                                dataTable = Nothing
                            Catch ex As Exception
                                'CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, Reflection.MethodBase.GetCurrentMethod.Name)
                                Dim pagePath(1) As String
                                Dim i As Integer = ex.StackTrace.IndexOf(" in ")
                                Dim lasti As Integer = ex.StackTrace.IndexOf(":line ")
                                pagePath(0) = ex.StackTrace.Substring(i + 3, (lasti + 11) - i)
                                CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name & " --Page:" & pagePath(0))

                                dataTable = Nothing

                            Finally
                                sqlCmd.Parameters.Clear()
                                spName = String.Empty
                                sqlParams = Nothing
                                sqlCn.Close()
                            End Try
                        End Using
                    End Using
                End Using
            Else
                Return dataTable
            End If
            Return dataTable
        End Function

        ''' <summary>
        ''' Function for executing a DML SQL Command Text 
        ''' </summary>
        ''' <param name="strSql">SQL Command Text</param>
        ''' <returns>0 for SUCCESS, and -1 for FAIL</returns>
        ''' <remarks></remarks>
        Public Function AddUpdateDeleteSQL(ByVal strSql As String) As Integer
            If strSql.Trim.Length > 0 AndAlso _strConn.Trim.Length > 0 Then
                Dim affectedRows As Integer = 0
                Using sqlCn As New SqlConnection(_strConn)
                    Using sqlCmd As New SqlCommand
                        With sqlCmd
                            .CommandText = strSql
                            .CommandType = CommandType.Text
                            .Connection = sqlCn
                            .CommandTimeout = 6000
                        End With
                        Try
                            If sqlCn.State = ConnectionState.Closed Then sqlCn.Open()
                            affectedRows = sqlCmd.ExecuteNonQuery()
                        Catch sqlEx As SqlException
                            For Each sqlE As SqlError In sqlEx.Errors
                                CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), sqlE.Message, Reflection.MethodBase.GetCurrentMethod.Name)
                            Next
                            Return -1
                        Catch ex As Exception
                            'CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, Reflection.MethodBase.GetCurrentMethod.Name)
                            Dim pagePath(1) As String
                            Dim i As Integer = ex.StackTrace.IndexOf(" in ")
                            Dim lasti As Integer = ex.StackTrace.IndexOf(":line ")
                            pagePath(0) = ex.StackTrace.Substring(i + 3, (lasti + 11) - i)
                            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name & " --Page:" & pagePath(0))

                            Return -1
                        Finally
                            If sqlCn.State = ConnectionState.Open Then sqlCn.Close()
                            strSql = String.Empty
                        End Try
                    End Using
                End Using
                If affectedRows > 0 Then
                    Return 0
                Else
                    Return -1
                End If
            Else
                Return -1
            End If
        End Function

        '-----------------------------------------------------------------

        ''' <summary>
        ''' Function for executing an SQL Command Text
        ''' </summary>
        ''' <param name="strSQL">SQL Command Text to Execute</param>
        ''' <returns>0 for Success, -1 for Fail</returns>
        ''' <remarks></remarks>
        Public Function ExecSQL(ByVal strSQL As String) As Integer
            If (strSQL IsNot Nothing) AndAlso (strSQL.Trim.Length > 0) Then
                Dim intRowsAffected As Integer = 0
                Using sqlCn As New SqlConnection(_strConn)
                    Using sqlCmd As New SqlCommand("sp_DirectExec", sqlCn)
                        Try
                            If sqlCn.State = ConnectionState.Closed Then sqlCn.Open()
                            With sqlCmd
                                .CommandType = CommandType.StoredProcedure
                                .Parameters.Add(New SqlParameter("@strSQL", SqlDbType.NVarChar, 4000, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, strSQL))
                                intRowsAffected = .ExecuteNonQuery
                            End With
                        Catch sqlEx As SqlException
                            For Each sqlE As SqlError In sqlEx.Errors
                                CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), sqlE.Message, Reflection.MethodBase.GetCurrentMethod.Name)
                            Next
                            Return -1
                        Catch ex As Exception
                            'CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, Reflection.MethodBase.GetCurrentMethod.Name)
                            Dim pagePath(1) As String
                            Dim i As Integer = ex.StackTrace.IndexOf(" in ")
                            Dim lasti As Integer = ex.StackTrace.IndexOf(":line ")
                            pagePath(0) = ex.StackTrace.Substring(i + 3, (lasti + 11) - i)
                            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name & " --Page:" & pagePath(0))

                            Return -1
                        Finally
                            If sqlCn.State = ConnectionState.Open Then sqlCn.Close()
                            sqlCmd.Parameters.Clear()
                            strSQL = String.Empty
                        End Try
                    End Using
                End Using
                If intRowsAffected > 0 Then
                    Return 0
                Else
                    Return -1
                End If
            Else
                Return -1
            End If
        End Function
        Public Function GetDataSet(ByVal spName As String, ByVal ParamArray sqlParams() As SqlParameter) As DataSet
            Dim dataSet As DataSet = Nothing

            Dim filledRows As Integer = 0
            If spName.Trim.Length > 0 AndAlso _strConn.Trim.Length > 0 Then
                Using sqlCn As New SqlConnection(_strConn)
                    Using sqlCmd As SqlCommand = sqlCn.CreateCommand
                        With sqlCmd
                            .CommandText = spName
                            .CommandType = CommandType.StoredProcedure
                            .Connection = sqlCn
                            If (sqlParams IsNot Nothing) AndAlso (sqlParams.Length > 0) Then .Parameters.AddRange(sqlParams)
                        End With
                        Using sqlDa As New SqlDataAdapter(sqlCmd)
                            Try
                                dataSet = New DataSet
                                filledRows = sqlDa.Fill(dataSet)
                            Catch sqlEx As SqlException
                                For Each sqlE As SqlError In sqlEx.Errors
                                    CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), sqlE.Message, Reflection.MethodBase.GetCurrentMethod.Name)
                                Next
                                dataSet = Nothing
                            Catch ex As Exception
                                'CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, Reflection.MethodBase.GetCurrentMethod.Name)
                                Dim pagePath(1) As String
                                Dim i As Integer = ex.StackTrace.IndexOf(" in ")
                                Dim lasti As Integer = ex.StackTrace.IndexOf(":line ")
                                pagePath(0) = ex.StackTrace.Substring(i + 3, (lasti + 11) - i)
                                CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name & " --Page:" & pagePath(0))

                                dataSet = Nothing
                            Finally
                                sqlCmd.Parameters.Clear()
                                spName = String.Empty
                                sqlParams = Nothing
                                sqlCn.Close()
                            End Try
                        End Using
                    End Using
                End Using
            Else
                Return dataSet
            End If
            Return dataSet
        End Function
        ''' <summary>
        ''' Function for executing an Amount Calculation
        ''' </summary>
        Public Function RetriveDataSet(ByRef Ds As DataSet, ByVal spName As String, ByVal ParamArray sqlParams() As SqlParameter) As ResultStructure
            Try

                ' Dim strconn = ConfigurationManager.ConnectionStrings("PayConnectionString").ConnectionString

                'If (SqlStr IsNot Nothing) AndAlso (SqlStr.Trim.Length > 0) Then
                'Dim dataSet As DataSet = Nothing
                Dim filledRows As Integer = 0

                If spName.Trim.Length > 0 AndAlso _strConn.Trim.Length > 0 Then
                    Using sqlCn As New SqlConnection(_strConn)
                        Using sqlCmd As SqlCommand = sqlCn.CreateCommand
                            'Using sqlDa As New SqlDataAdapter(SqlStr, sqlCn)
                            Try

                                With sqlCmd
                                    .CommandText = spName
                                    .CommandType = CommandType.StoredProcedure
                                    .Connection = sqlCn
                                    If (sqlParams IsNot Nothing) AndAlso (sqlParams.Length > 0) Then .Parameters.AddRange(sqlParams)
                                End With

                                Using sqlDa As New SqlDataAdapter(sqlCmd)
                                    filledRows = sqlDa.Fill(Ds)
                                    If Ds.Tables.Count <= 0 Then
                                        RetriveDataSet.Success = False
                                        RetriveDataSet.ErrCode = "1"
                                        RetriveDataSet.ErrMsg = "No data returned"
                                    Else
                                        RetriveDataSet.Success = True
                                    End If
                                End Using

                            Catch sqlEx As SqlException
                                For Each sqlE As SqlError In sqlEx.Errors
                                    RetriveDataSet.Success = False
                                    RetriveDataSet.ErrCode = Err.Number
                                    RetriveDataSet.ErrMsg = Err.Description
                                    CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), sqlE.Message, Reflection.MethodBase.GetCurrentMethod.Name)
                                Next
                                Ds = Nothing

                                RetriveDataSet.Success = False

                            Catch ex As Exception
                                RetriveDataSet.Success = False
                                RetriveDataSet.ErrCode = Err.Number
                                RetriveDataSet.ErrMsg = Err.Description
                                'CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, Reflection.MethodBase.GetCurrentMethod.Name)
                                Dim pagePath(1) As String
                                Dim i As Integer = ex.StackTrace.IndexOf(" in ")
                                Dim lasti As Integer = ex.StackTrace.IndexOf(":line ")
                                pagePath(0) = ex.StackTrace.Substring(i + 3, (lasti + 11) - i)
                                CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name & " --Page:" & pagePath(0))

                                RetriveDataSet.Success = False
                            Finally
                                If sqlCn.State = ConnectionState.Open Then sqlCn.Close()

                            End Try
                        End Using

                    End Using


                End If




            Catch ex As Exception
                RetriveDataSet.Success = False
            End Try

        End Function

        ''' <summary>
        ''' Function for executing an SQL Command Text with Audit
        ''' </summary>
        ''' <param name="strSQL">SQL Command Text to Execute</param>
        ''' <returns>0 for Success, -1 for Fail</returns>
        ''' <remarks></remarks>
        ''' 

        Public Function ExecSQLwithAudit(ByVal strSQL As String) As Integer
            If (strSQL IsNot Nothing) AndAlso (strSQL.Trim.Length > 0) AndAlso (_strConn.Trim.Length > 0) AndAlso (_intUserID > 0) Then
                Dim intRowsAffected As Integer = 0
                Using sqlCn As New SqlConnection(_strConn)
                    Dim sqlTrans As SqlTransaction = Nothing
                    Using sqlCmd As SqlCommand = sqlCn.CreateCommand
                        Try
                            If sqlCn.State = ConnectionState.Closed Then sqlCn.Open()
                            sqlTrans = sqlCn.BeginTransaction
                            With sqlCmd
                                .Transaction = sqlTrans
                                .CommandText = "[dbo].[AutoAuditSetUserContext]"
                                .CommandType = CommandType.StoredProcedure
                                .Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, _intUserID))
                                .ExecuteNonQuery()
                                '------------------------------
                                .Parameters.Clear()
                                .CommandText = "[dbo].[sp_DirectExec]"
                                .CommandType = CommandType.StoredProcedure
                                .Parameters.Add(New SqlParameter("@UserID", SqlDbType.NVarChar, 4000, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, strSQL))
                                intRowsAffected = .ExecuteNonQuery()
                            End With
                            sqlTrans.Commit()
                        Catch sqlEx As SqlException
                            For Each sqlE As SqlError In sqlEx.Errors
                                CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), sqlE.Message, Reflection.MethodBase.GetCurrentMethod.Name)
                            Next
                            sqlTrans.Rollback()
                            intRowsAffected = -1
                        Catch ex As Exception
                            'CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), "@_!_$", Reflection.MethodBase.GetCurrentMethod.Name)
                            Dim pagePath(1) As String
                            Dim i As Integer = ex.StackTrace.IndexOf(" in ")
                            Dim lasti As Integer = ex.StackTrace.IndexOf(":line ")
                            pagePath(0) = ex.StackTrace.Substring(i + 3, (lasti + 11) - i)
                            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name & " --Page:" & pagePath(0))

                            sqlTrans.Rollback()
                            intRowsAffected = -1
                        Finally
                            If sqlCn.State = ConnectionState.Open Then sqlCn.Close()
                            sqlCmd.Parameters.Clear()
                            strSQL = String.Empty
                        End Try
                    End Using
                End Using
                If intRowsAffected > 0 Then
                    Return 0
                Else
                    Return -1
                End If
            Else
                Return -1
            End If
        End Function

        ''' <summary>
        ''' Function for executing a DML SQL Stored Procedure
        ''' </summary>
        ''' <param name="strSpName">Stored Procedure Name</param>
        ''' <param name="sqlParameter">Parameters as Array of parameters</param>
        ''' <returns>0 for SUCCESS, and -1 for FAIL</returns>
        ''' <remarks></remarks>
        Public Function AddUpdateDeleteSP(ByVal strSpName As String, ByVal ParamArray sqlParameter() As SqlParameter) As Integer
            If strSpName.Trim.Length > 0 AndAlso _strConn.Trim.Length > 0 Then
                Dim affectedRows As Integer = 0
                Dim errCode As Integer = 0
                Using sqlCn As New SqlConnection(_strConn)
                    Using sqlCmd As New SqlCommand
                        With sqlCmd
                            .CommandText = strSpName
                            .CommandType = CommandType.StoredProcedure
                            .Connection = sqlCn
                            .CommandTimeout = 1200
                            If (sqlParameter IsNot Nothing) AndAlso (sqlParameter.Length > 0) Then .Parameters.AddRange(sqlParameter)
                            .Parameters.Add(New SqlParameter("@intErrorCode", SqlDbType.Int, 4, ParameterDirection.ReturnValue, False, 0, 0, "", DataRowVersion.Default, 0))
                        End With
                        Try
                            If sqlCn.State = ConnectionState.Closed Then sqlCn.Open()
                            affectedRows = sqlCmd.ExecuteNonQuery()
                            errCode = sqlCmd.Parameters("@intErrorCode").Value
                        Catch sqlEx As SqlException
                            For Each sqlE As SqlError In sqlEx.Errors
                                CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), sqlE.Message, Reflection.MethodBase.GetCurrentMethod.Name)
                            Next
                            errCode = -1
                        Catch ex As Exception
                            'CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, Reflection.MethodBase.GetCurrentMethod.ToString)
                            Dim pagePath(1) As String
                            Dim i As Integer = ex.StackTrace.IndexOf(" in ")
                            Dim lasti As Integer = ex.StackTrace.IndexOf(":line ")
                            pagePath(0) = ex.StackTrace.Substring(i + 3, (lasti + 11) - i)
                            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name & " --Page:" & pagePath(0))

                            errCode = -1
                        Finally
                            If sqlCn.State = ConnectionState.Open Then sqlCn.Close()
                            sqlCmd.Parameters.Clear()
                            sqlParameter = Nothing
                            strSpName = String.Empty
                        End Try
                    End Using
                End Using
                Return errCode
            Else
                Return -1
            End If
        End Function
        Public Function OleDBAddUpdateDeleteSP(ByVal strSpName As String, ByVal ParamArray sqlParameter() As OleDbParameter) As Integer

            If strSpName.Trim.Length > 0 AndAlso _strConn.Trim.Length > 0 Then


                Dim _OLEDBstrConn As String = System.Configuration.ConfigurationManager.ConnectionStrings("MLS_OLEDB_Conn_Str").ToString()


                ' _OledbConnection.ConnectionString = _OLEDBstrConn
                'Try
                '    If (_OLEDBstrConn Is Nothing) OrElse (_OLEDBstrConn.Trim.Length < 1) Then
                '        Throw New Exception("Connection String Not Provided")
                '    End If

                'Catch ex As Exception
                '    CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, Reflection.MethodBase.GetCurrentMethod.Name)
                'End Try

                Dim affectedRows As Integer = 0
                Dim errCode As Integer = 0

                Using sqlCn As New OleDbConnection(_OLEDBstrConn)
                    Using sqlCmd As New OleDbCommand
                        With sqlCmd
                            .CommandText = strSpName
                            .CommandType = CommandType.StoredProcedure
                            .Connection = sqlCn
                            If (sqlParameter IsNot Nothing) AndAlso (sqlParameter.Length > 0) Then .Parameters.AddRange(sqlParameter)
                            '.Parameters.Add(New OleDbParameter("@intErrorCode", 0))
                            '.Parameters.Add(New OleDbParameter("@intErrorCode", OleDbType.Integer, 4, ParameterDirection.ReturnValue, True, 0, 0, "", DataRowVersion.Default, 0))
                        End With
                        Try
                            If sqlCn.State = ConnectionState.Closed Then sqlCn.Open()
                            affectedRows = sqlCmd.ExecuteNonQuery()
                            errCode = 0
                            'errCode = sqlCmd.Parameters("@intErrorCode").Value
                        Catch sqlEx As SqlException
                            For Each sqlE As SqlError In sqlEx.Errors
                                CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), sqlE.Message, Reflection.MethodBase.GetCurrentMethod.Name)
                            Next
                            errCode = -1
                        Catch ex As Exception
                            'CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, Reflection.MethodBase.GetCurrentMethod.ToString)
                            Dim pagePath(1) As String
                            Dim i As Integer = ex.StackTrace.IndexOf(" in ")
                            Dim lasti As Integer = ex.StackTrace.IndexOf(":line ")
                            pagePath(0) = ex.StackTrace.Substring(i + 3, (lasti + 11) - i)
                            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name & " --Page:" & pagePath(0))

                            errCode = -1
                        Finally
                            If sqlCn.State = ConnectionState.Open Then sqlCn.Close()
                            sqlCmd.Parameters.Clear()
                            sqlParameter = Nothing
                            strSpName = String.Empty
                        End Try
                    End Using
                End Using
                Return errCode
            Else
                Return -1
            End If
        End Function


        ''' <summary>
        ''' Function for executing a DML SQL Stored Procedure
        ''' </summary>
        ''' <param name="strSpName">Stored Procedure Name</param>
        ''' <param name="sqlParameter">Parameters as Array of parameters</param>
        ''' <returns>0 for SUCCESS, and -1 for FAIL</returns>
        ''' <remarks></remarks>
        Public Function AddUpdateDeleteSPTrans(ByVal strSpName As String, ByVal ParamArray sqlParameter() As SqlParameter) As Integer
            If (Not _myConnection.State = ConnectionState.Open) Then
                _myConnection.Open()
            End If
            If strSpName.Trim.Length > 0 AndAlso _strConn.Trim.Length > 0 Then
                Dim affectedRows As Integer = 0
                Dim errCode As Integer = 0
                ' Using _myConnection
                Using sqlCmd As New SqlCommand
                    With sqlCmd
                        .Transaction = DACSQLTrans
                        .CommandText = strSpName
                        .CommandTimeout = 1200
                        .CommandType = CommandType.StoredProcedure
                        .Connection = _myConnection
                        If (sqlParameter IsNot Nothing) AndAlso (sqlParameter.Length > 0) Then .Parameters.AddRange(sqlParameter)
                        .Parameters.Add(New SqlParameter("@intErrorCode", SqlDbType.Int, 4, ParameterDirection.ReturnValue, False, 0, 0, "", DataRowVersion.Default, 0))
                    End With
                    Try
                        If _myConnection.State = ConnectionState.Closed Then _myConnection.Open()
                        affectedRows = sqlCmd.ExecuteNonQuery()
                        errCode = sqlCmd.Parameters("@intErrorCode").Value

                    Catch sqlEx As SqlException
                        For Each sqlE As SqlError In sqlEx.Errors
                            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), sqlE.Message, Reflection.MethodBase.GetCurrentMethod.Name)
                        Next
                        errCode = -1
                    Catch ex As Exception
                        'CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, Reflection.MethodBase.GetCurrentMethod.ToString)
                        Dim pagePath(1) As String
                        Dim i As Integer = ex.StackTrace.IndexOf(" in ")
                        Dim lasti As Integer = ex.StackTrace.IndexOf(":line ")
                        pagePath(0) = ex.StackTrace.Substring(i + 3, (lasti + 11) - i)
                        CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name & " --Page:" & pagePath(0))

                        errCode = -1
                    Finally
                        ' If sqlCn.State = ConnectionState.Open Then sqlCn.Close()
                        sqlCmd.Parameters.Clear()
                        sqlParameter = Nothing
                        strSpName = String.Empty
                        If _myConnection.State = ConnectionState.Open Then _myConnection.Close()

                    End Try
                End Using
                '  End Using
                Return errCode
            Else
                Return -1
            End If
        End Function




        '------------------- With Logging Audit UserID -------------------

        ''' <summary>
        ''' Function for executing a DML SQL Command Text with Audit
        ''' </summary>
        ''' <param name="strSql">SQL Command Text</param>
        ''' <returns>0 for SUCCESS, and -1 for FAIL</returns>
        ''' <remarks></remarks>
        Public Function AddUpdateDeleteSQLAudit(ByVal strSql As String) As Integer
            If strSql.Trim.Length > 0 AndAlso _strConn.Trim.Length > 0 AndAlso _intUserID > 0 Then
                Dim intAffectedRows As Integer = 0
                Using sqlCn As New SqlConnection(_strConn)
                    Dim sqlTrans As SqlTransaction = Nothing
                    Using sqlCmd As SqlCommand = sqlCn.CreateCommand
                        Try
                            If sqlCn.State = ConnectionState.Closed Then sqlCn.Open()
                            sqlTrans = sqlCn.BeginTransaction
                            With sqlCmd
                                .Transaction = sqlTrans
                                .CommandText = "[dbo].[AutoAuditSetUserContext]"
                                .CommandType = CommandType.StoredProcedure
                                .Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, _intUserID))
                                .ExecuteNonQuery()
                                '------------------------------------
                                .Parameters.Clear()
                                .CommandText = strSql
                                .CommandType = CommandType.Text
                                intAffectedRows = .ExecuteNonQuery()
                            End With
                            sqlTrans.Commit()
                        Catch sqlEx As SqlException
                            For Each sqlE As SqlError In sqlEx.Errors
                                CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), sqlE.Message, Reflection.MethodBase.GetCurrentMethod.Name)
                            Next
                            sqlTrans.Rollback()
                            intAffectedRows = -1
                        Catch ex As Exception
                            'CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, Reflection.MethodBase.GetCurrentMethod.Name)
                            Dim pagePath(1) As String
                            Dim i As Integer = ex.StackTrace.IndexOf(" in ")
                            Dim lasti As Integer = ex.StackTrace.IndexOf(":line ")
                            pagePath(0) = ex.StackTrace.Substring(i + 3, (lasti + 11) - i)
                            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name & " --Page:" & pagePath(0))

                            sqlTrans.Rollback()
                            intAffectedRows = -1
                        Finally
                            If sqlCn.State = ConnectionState.Open Then sqlCn.Close()
                            sqlCmd.Parameters.Clear()
                        End Try
                    End Using
                End Using
                If intAffectedRows >= 0 Then
                    Return 0
                Else
                    Return -1
                End If
            Else
                Return -1
            End If
        End Function

        ''' <summary>
        ''' Function for executing a DML SQL Stored Procedure with Audit
        ''' </summary>
        ''' <param name="strSpName">Stored Procedure Name</param>
        ''' <param name="sqlParameter">Parameters as Array of parameters</param>
        ''' <returns>0 for SUCCESS, and -1 for FAIL</returns>
        ''' <remarks></remarks>
        Public Function AddUpdateDeleteSPAudit(ByVal strSpName As String, ByVal ParamArray sqlParameter() As SqlParameter) As Integer
            If strSpName.Trim.Length > 0 AndAlso _strConn.Trim.Length > 0 AndAlso _intUserID > 0 Then
                Dim intAffectedRows As Integer = 0
                Using sqlCn As New SqlConnection(_strConn)
                    Dim sqlTrans As SqlTransaction = Nothing
                    Using sqlCmd As SqlCommand = sqlCn.CreateCommand
                        Try
                            If sqlCn.State = ConnectionState.Closed Then sqlCn.Open()
                            sqlTrans = sqlCn.BeginTransaction
                            With sqlCmd
                                .Transaction = sqlTrans
                                .CommandText = "[dbo].[AutoAuditSetUserContext]"
                                .CommandType = CommandType.StoredProcedure
                                .Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, _intUserID))
                                .ExecuteNonQuery()
                                '-----------------------------------------------------------------
                                .Parameters.Clear()
                                .CommandText = strSpName
                                .CommandType = CommandType.StoredProcedure
                                If (sqlParameter IsNot Nothing) AndAlso (sqlParameter.Length > 0) Then .Parameters.AddRange(sqlParameter)
                                intAffectedRows = .ExecuteNonQuery()
                            End With
                            sqlTrans.Commit()
                        Catch sqlEx As SqlException
                            For Each sqlE As SqlError In sqlEx.Errors
                                CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), sqlE.Message, Reflection.MethodBase.GetCurrentMethod.Name)
                            Next
                            sqlTrans.Rollback()
                            intAffectedRows = -1
                        Catch ex As Exception
                            'CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, Reflection.MethodBase.GetCurrentMethod.ToString)
                            Dim pagePath(1) As String
                            Dim i As Integer = ex.StackTrace.IndexOf(" in ")
                            Dim lasti As Integer = ex.StackTrace.IndexOf(":line ")
                            pagePath(0) = ex.StackTrace.Substring(i + 3, (lasti + 11) - i)
                            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name & " --Page:" & pagePath(0))

                            sqlTrans.Rollback()
                            intAffectedRows = -1
                        Finally
                            If sqlCn.State = ConnectionState.Open Then sqlCn.Close()
                            sqlParameter = Nothing
                            sqlCmd.Parameters.Clear()
                        End Try
                    End Using
                End Using
                If intAffectedRows >= 0 Then
                    Return 0
                Else
                    Return -1
                End If
            Else
                Return -1
            End If
        End Function

        Public Function GetSingleValue(Of T)(ByVal spName As String, ByVal ParamArray parameters() As SqlParameter) As T
            Dim objVal As T = Nothing
            If (spName IsNot Nothing) AndAlso (spName.Trim.Length > 0) AndAlso (_strConn IsNot Nothing) AndAlso (_strConn.Trim.Length > 0) Then
                Using sqlCn As New SqlConnection(_strConn)
                    Using sqlCmd As New SqlCommand(spName, sqlCn)
                        With sqlCmd
                            .CommandType = CommandType.StoredProcedure
                            If (parameters IsNot Nothing) AndAlso (parameters.Length > 0) Then .Parameters.AddRange(parameters)
                            Try
                                If sqlCn.State = ConnectionState.Closed Then sqlCn.Open()
                                Dim rdr As SqlDataReader = .ExecuteReader
                                If rdr.Read Then
                                    If Not IsDBNull(rdr.Item(0)) Then
                                        objVal = CType(rdr.Item(0), T)
                                    End If
                                End If
                            Catch sqlEx As SqlException
                                For Each sqlE As SqlError In sqlEx.Errors
                                    CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), sqlE.Message, Reflection.MethodBase.GetCurrentMethod.Name)
                                Next
                                objVal = Nothing
                            Catch ex As Exception
                                'CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, Reflection.MethodBase.GetCurrentMethod.Name)
                                Dim pagePath(1) As String
                                Dim i As Integer = ex.StackTrace.IndexOf(" in ")
                                Dim lasti As Integer = ex.StackTrace.IndexOf(":line ")
                                pagePath(0) = ex.StackTrace.Substring(i + 3, (lasti + 11) - i)
                                CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name & " --Page:" & pagePath(0))

                                objVal = Nothing
                            Finally
                                If sqlCn.State = ConnectionState.Open Then sqlCn.Close()
                                parameters = Nothing
                                sqlCmd.Parameters.Clear()
                            End Try
                        End With
                    End Using
                End Using
            End If
            Return objVal
        End Function


        ''' <summary>
        ''' Function for executing scalar which get a single value as a result of the SQL statement
        ''' </summary>
        ''' <typeparam name="T">Data type of the value to be read</typeparam>
        ''' <param name="strSQL">SQL SELECT Command</param>
        ''' <returns>return the output value</returns>
        Public Function ExecuteScalar(Of T)(ByVal strSQL As String) As T
            Dim objVal As T = Nothing
            If (strSQL IsNot Nothing) AndAlso (strSQL.Trim.Length > 0) AndAlso (_strConn IsNot Nothing) AndAlso (_strConn.Trim.Length > 0) Then
                Using sqlCn As New SqlConnection(_strConn)
                    Using sqlCmd As New SqlCommand(strSQL, sqlCn)
                        With sqlCmd
                            .CommandType = CommandType.Text
                            Try
                                If sqlCn.State = ConnectionState.Closed Then sqlCn.Open()
                                Dim obj As Object = .ExecuteScalar
                                objVal = CType(obj, T)
                            Catch oraEx As SqlException
                                CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), oraEx.Message, Reflection.MethodBase.GetCurrentMethod.Name)
                            Catch ex As Exception
                                'CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, Reflection.MethodBase.GetCurrentMethod.Name)
                                Dim pagePath(1) As String
                                Dim i As Integer = ex.StackTrace.IndexOf(" in ")
                                Dim lasti As Integer = ex.StackTrace.IndexOf(":line ")
                                pagePath(0) = ex.StackTrace.Substring(i + 3, (lasti + 11) - i)
                                CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name & " --Page:" & pagePath(0))

                            Finally
                                If sqlCn.State = ConnectionState.Open Then sqlCn.Close()
                            End Try
                        End With
                    End Using
                End Using
            End If
            Return objVal
        End Function



        ' ''added by Suddesh
        'Public Function AddUpdateDeleteSP(ByVal strSpName As String, ByVal objsrc As AttachmentBLL, ByVal outputparam As String, ByVal ParamArray sqlParameter() As SqlParameter) As Integer
        '    Dim array As New ArrayList()
        '    If strSpName.Trim.Length > 0 AndAlso _strConn.Trim.Length > 0 Then
        '        Dim affectedRows As Integer = 0
        '        Dim errCode As Integer = 0

        '        'objattachmentbll as AttachmentBLL


        '        Using sqlCn As New SqlConnection(_strConn)
        '            Using sqlCmd As New SqlCommand
        '                With sqlCmd
        '                    .CommandText = strSpName
        '                    .CommandType = CommandType.StoredProcedure
        '                    .Connection = sqlCn
        '                    If (sqlParameter IsNot Nothing) AndAlso (sqlParameter.Length > 0) Then .Parameters.AddRange(sqlParameter)
        '                    .Parameters.Add(New SqlParameter("@intErrorCode", SqlDbType.Int, 4, ParameterDirection.ReturnValue, False, 0, 0, "", DataRowVersion.Default, 0))
        '                End With
        '                Try
        '                    If sqlCn.State = ConnectionState.Closed Then sqlCn.Open()
        '                    affectedRows = sqlCmd.ExecuteNonQuery()
        '                    errCode = sqlCmd.Parameters("@intErrorCode").Value
        '                    objsrc.AttachFileName = sqlCmd.Parameters(outputparam).Value.ToString()
        '                    objsrc.SessionComID = sqlCmd.Parameters(outputparam).Value.ToString()


        '                    'outputparam = sqlCmd.Parameters(outputparam).Value.ToString()
        '                    'array.Add(errCode)
        '                    'array.Add(objsrc.AttachFileName)

        '                Catch sqlEx As SqlException
        '                    For Each sqlE As SqlError In sqlEx.Errors
        '                        CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), sqlE.Message, Reflection.MethodBase.GetCurrentMethod.Name)
        '                    Next
        '                    errCode = -1
        '                Catch ex As Exception
        '                    CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, Reflection.MethodBase.GetCurrentMethod.ToString)
        '                    errCode = -1
        '                Finally
        '                    If sqlCn.State = ConnectionState.Open Then sqlCn.Close()
        '                    sqlCmd.Parameters.Clear()
        '                    sqlParameter = Nothing
        '                    strSpName = String.Empty
        '                End Try
        '            End Using
        '        End Using
        '        Return errCode
        '    Else
        '        Return -1
        '    End If
        'End Function

#End Region

#Region "Shared Methods"
        ''' <summary>
        ''' return the connection string from the web.config file
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function getConnectionString() As String
            Return ConfigurationManager.ConnectionStrings("ConnStr").ConnectionString
        End Function

        ''' <summary>
        ''' start a new transaction by creating a DAC object and add it to the DACObjects table
        ''' in the case there is a previous DAC for this user, the transaction of this dac will be rolled back
        ''' and the a new object will be created
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function beginTransaction() As Integer
            Dim tmpDac As DAC = DACObjects.Item(System.Web.HttpContext.Current.Session.SessionID)
            If tmpDac Is Nothing Then
                tmpDac = New DAC(getConnectionString)
                DACObjects.Remove(System.Web.HttpContext.Current.Session.SessionID)
                DACObjects.Add(System.Web.HttpContext.Current.Session.SessionID, tmpDac)
            End If
            tmpDac._myConnection = New SqlConnection(getConnectionString)
            If (Not tmpDac._myConnection.State = ConnectionState.Open) Then
                tmpDac._myConnection.Open()
            End If
            tmpDac.DACSQLTrans = tmpDac._myConnection.BeginTransaction
            tmpDac._isCommented = False
            Return 1
        End Function

        ''' <summary>
        ''' commit the transaction of the DAC object of a selected user.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function commitTransaction() As Integer
            Dim tmpDAC As DAC = DACObjects.Item(System.Web.HttpContext.Current.Session.SessionID)
            If (tmpDAC Is Nothing) Then
                Return -1
            End If
            DAC.DACObjects.Remove(System.Web.HttpContext.Current.Session.SessionID)
            tmpDAC.DACSQLTrans.Commit()
            tmpDAC._myConnection.Close()
            tmpDAC._isCommented = True
        End Function

        ''' <summary>
        ''' rollback the current transaction of a selected user.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function rollBackTransaction() As Integer
            Dim tmpDAC As DAC = DACObjects.Item(System.Web.HttpContext.Current.Session.SessionID)
            If (tmpDAC Is Nothing) Then
                Return -1
            End If
            tmpDAC.DACSQLTrans.Rollback()
            tmpDAC._myConnection.Close()
            DAC.DACObjects.Remove(System.Web.HttpContext.Current.Session.SessionID)
            tmpDAC._isCommented = False
        End Function

        ''' <summary>
        ''' Return a DAC object, by providing the user ID.
        ''' in the case this user has 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function getDAC() As DAC
            Try
                Dim tmpDAC As DAC = CType(DACObjects.Item(System.Web.HttpContext.Current.Session.SessionID), DAC)
                If tmpDAC Is Nothing Then
                    'tmpDAC = CType(DACObjects.Item(System.Web.HttpContext.Current.Session.SessionID), DAC)
                    tmpDAC = New DAC(getConnectionString)
                    DAC.DACObjects.Remove(System.Web.HttpContext.Current.Session.SessionID)
                    DAC.DACObjects.Add(System.Web.HttpContext.Current.Session.SessionID, tmpDAC)
                    Return tmpDAC
                Else
                    If tmpDAC.isCommitted = False Then
                        Return tmpDAC
                    Else
                        tmpDAC = New DAC(getConnectionString)
                        DAC.DACObjects.Remove(System.Web.HttpContext.Current.Session.SessionID)
                        DAC.DACObjects.Add(System.Web.HttpContext.Current.Session.SessionID, tmpDAC)
                        Return tmpDAC
                    End If
                End If

            Catch ex As Exception
                CtlCommon.CreateErrorLog("getDAC", ex.Message & System.Web.HttpContext.Current.Session.SessionID, MethodBase.GetCurrentMethod.Name)
                GC.Collect()
            End Try

        End Function
#End Region
    End Class
End Namespace
