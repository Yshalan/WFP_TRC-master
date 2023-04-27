Imports Microsoft.VisualBasic
Imports SmartV.UTILITIES
Imports System.Data
Imports System.Reflection
Imports System.Data.SqlClient
Imports SmartV.DB

Namespace TA.Security

    Public Class SECUsers

#Region "MEMBERS"

        Private _userID As Integer
        Private _loginName As String
        Private _employeeid As String
        Private _password As String
        Private _roleid As Integer
        Private _encryptPass As String
        Private _status As Boolean
        Private _encrptPassOld As String
        Private _encrptPassNew As String
        Private _prefeardLang As Boolean
        Private _firstEnName As String
        Private _lastEnName As String
        Private _firstArName As String
        Private _lastArName As String
        Private _fullName As String
        Private _AllowedFormsIDs As String
        Private _RegisteredDevice As String

#End Region

#Region "PROPERTIES"

        Public Property EncrptPassOld() As String
            Get
                Return _encrptPassOld
            End Get
            Set(ByVal value As String)
                _encrptPassOld = value
            End Set
        End Property

        Public Property EncrptPassNew() As String
            Get
                Return _encrptPassNew
            End Get
            Set(ByVal value As String)
                _encrptPassNew = value
            End Set
        End Property

        Public Property Status() As Boolean
            Get
                Return _status
            End Get
            Set(ByVal value As Boolean)
                _status = value
            End Set
        End Property

        Public Property EncryptPass() As String
            Get
                Return _encryptPass
            End Get
            Set(ByVal value As String)
                _encryptPass = value
            End Set
        End Property

        Public Property UsrID() As Integer
            Get
                Return _userID
            End Get
            Set(ByVal value As Integer)
                _userID = value
            End Set
        End Property
        Public Property FirstEnName() As String
            Get
                Return _firstEnName
            End Get
            Set(ByVal value As String)
                _firstEnName = value
            End Set
        End Property
        Public Property LastEnName() As String
            Get
                Return _lastEnName
            End Get
            Set(ByVal value As String)
                _lastEnName = value
            End Set
        End Property
        Public Property FirstArName() As String
            Get
                Return _firstArName
            End Get
            Set(ByVal value As String)
                _firstArName = value
            End Set
        End Property
        Public Property LastArName() As String
            Get
                Return _lastArName
            End Get
            Set(ByVal value As String)
                _lastArName = value
            End Set
        End Property

        Public Property LoginName() As String
            Get
                Return _loginName
            End Get
            Set(ByVal value As String)
                _loginName = value
            End Set
        End Property
        Public Property EmployeeID() As String
            Get
                Return _employeeid
            End Get
            Set(ByVal value As String)
                _employeeid = value
            End Set
        End Property

        Public Property Password() As String
            Get
                Return _password
            End Get
            Set(ByVal value As String)
                _password = value
            End Set
        End Property

        Public Property RoleId() As Integer
            Get
                Return _roleid
            End Get
            Set(ByVal value As Integer)
                _roleid = value
            End Set
        End Property

        Public Property PrefeardLang() As Boolean
            Get
                Return _prefeardLang
            End Get
            Set(ByVal value As Boolean)
                _prefeardLang = value
            End Set
        End Property

        Public Property fullName() As String
            Get
                Return _fullName
            End Get
            Set(ByVal value As String)
                _fullName = value
            End Set
        End Property

        Public Property AllowedFormsIDs() As String
            Get
                Return _AllowedFormsIDs
            End Get
            Set(ByVal value As String)
                _AllowedFormsIDs = value
            End Set
        End Property

        Public Property RegisteredDevice() As String
            Get
                Return _RegisteredDevice
            End Get
            Set(ByVal value As String)
                _RegisteredDevice = value
            End Set
        End Property

#End Region

#Region "CONSTRUCTOR"

        Public Sub New()
            _userID = 0
        End Sub

        Public Sub New(ByVal i_userID As Integer)
            _userID = i_userID
        End Sub


#End Region

#Region "METHOD"

        Public Function GetAllUsers(Optional ByVal lan As Integer = 1) As DataTable
            Dim objDac As DAC = DAC.getDAC
            Return objDac.GetDataTable("SEC_GETAllUsers")
        End Function

        Public Function select_usrs() As DataTable
            Dim objDac As DAC = DAC.getDAC
            Return objDac.GetDataTable("User_Details_select_forDDL")
        End Function

        Public Function GetUser() As Integer
            Dim objDac As DAC = DAC.getDAC
            Dim errNo As Integer
            Dim dt As DataTable
            Dim sqlparam1 As New SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, UsrID)
            dt = objDac.GetDataTable("SEC_GETUser", sqlparam1)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                With dt.Rows(0)
                    EmployeeID = .Item("EmployeeID")
                    RoleId = .Item("RoleID")
                    LoginName = .Item("LoginName")
                    Password = .Item("LoginPassword")
                    Status = .Item("Active")
                    PrefeardLang = .Item("PrefearedLanguage")
                End With
                errNo = 0
            Else
                errNo = -1
            End If
            Return errNo
        End Function

        Private Function add() As Integer
            Dim objDac As DAC = DAC.getDAC
            Return (objDac.AddUpdateDeleteSP("SEC_INSUser",
                                                 New SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, UsrID),
                                                 New SqlParameter("@EmployeeID", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, EmployeeID),
                                                 New SqlParameter("@RoleID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, RoleId),
                                                 New SqlParameter("@LoginName", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, LoginName),
                                                 New SqlParameter("@LoginPassword", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, Password),
                                                 New SqlParameter("@Active", SqlDbType.Bit, 1, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, Status),
                                                 New SqlParameter("@PrefearedLanguage", SqlDbType.Bit, 1, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, PrefeardLang)))
        End Function

        Private Function update() As Integer
            Dim objDac As DAC = DAC.getDAC
            Return (objDac.AddUpdateDeleteSP("SEC_UPDUser",
                                             New SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, UsrID),
                                             New SqlParameter("@EmployeeID", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, EmployeeID),
                                             New SqlParameter("@RoleID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, RoleId),
                                             New SqlParameter("@LoginName", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, LoginName),
                                             New SqlParameter("@LoginPassword", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, Password),
                                             New SqlParameter("@Active", SqlDbType.Bit, 1, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, Status),
                                             New SqlParameter("@PrefearedLanguage", SqlDbType.Bit, 1, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, PrefeardLang)))
        End Function

        Public Function save() As Integer
            If _userID = 0 Then
                Return add()
            Else
                Return update()
            End If
        End Function

        Public Function delete() As Integer
            Dim objDac As DAC = DAC.getDAC
            Return objDac.AddUpdateDeleteSP("SEC_DELUser", New SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "UserID", DataRowVersion.Default, UsrID))
        End Function

        Public Function ValidateConnection() As Boolean
            Dim sqlcn As New SqlConnection(DAC.getConnectionString)
            Try
                If (sqlcn.State = ConnectionState.Closed) Then
                    sqlcn.Open()
                End If
                ValidateConnection = True
            Catch ex As Exception
                ValidateConnection = False
            End Try
        End Function

        Public Function getActive(ByRef flgExpire As Integer) As DataTable
            Dim objDac As New DAC(DAC.getConnectionString)
            Dim objColl As DataTable

            Dim pResult As New SqlParameter("@qResult", SqlDbType.Int, 4, ParameterDirection.Output, False, 0, 0, "qResult", DataRowVersion.Default, 0)
            objColl = objDac.GetDataTable("SEC_GETActiveUsers",
                                         New SqlParameter("@LoginName", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "LoginName", DataRowVersion.Default, LoginName),
                                         New SqlParameter("@LoginPassword", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "LoginPassword", DataRowVersion.Default, Password), pResult)
            flgExpire = CInt(pResult.Value)

            If objColl IsNot Nothing AndAlso objColl.Rows.Count > 0 Then
                With objColl.Rows(0)
                    UsrID = CInt(.Item("UserID"))
                    RoleId = CInt(.Item("RoleId"))
                    Status = CInt(.Item("Active"))
                    EmployeeID = .Item("UserID")
                    LoginName = .Item("LoginName")
                    fullName = .Item("Full_Name")
                    ' PrefeardLang = .Item("PrefearedLanguage")
                End With
            End If

            Return objColl
        End Function


        'Public Function GetUsrByGroupID(ByVal RoleID As Integer) As DataTable
        '    Dim objDac As New DAC(DAC.getConnectionString)
        '    Dim objColl As DataTable
        '    Try
        '        objColl = objDac.GetDataTable("sp_Sec_Get_UserByGroupid", New SqlParameter("@groupid", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "groupid", DataRowVersion.Default, RoleID))
        '    Catch ex As Exception
        '        objColl = Nothing
        '        CtlCommon.CreateErrorLog(CtlCommon.getLogPath, ex.Message, MethodBase.GetCurrentMethod.Name)
        '    Finally
        '        objDac = Nothing
        '    End Try
        '    Return objColl
        'End Function

        Public Function getLogin() As DataTable
            Dim objDac As New DAC(DAC.getConnectionString)
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("SEC_GETLoginUser",
                                             New SqlParameter("@LoginName", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "LoginName", DataRowVersion.Default, LoginName),
                                             New SqlParameter("@LoginPassword", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "LoginPassword", DataRowVersion.Default, Password))
            Catch ex As Exception
                objColl = Nothing
                CtlCommon.CreateErrorLog(CtlCommon.getLogPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            Finally
                objDac = Nothing
            End Try
            Return objColl
        End Function

        'Public Function getOldPassword() As DataTable
        '    Dim objDac As New DAC(DAC.getConnectionString)
        '    Dim objColl As DataTable
        '    Try
        '        objColl = objDac.GetDataTable("sp_Sec_Get_LoginPassword", New SqlParameter("@LoginPassword", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "LoginPassword", DataRowVersion.Default, EncrptPassOld))
        '    Catch ex As Exception
        '        objColl = Nothing
        '        CtlCommon.CreateErrorLog(CtlCommon.getLogPath, ex.Message, MethodBase.GetCurrentMethod.Name)
        '    Finally
        '        objDac = Nothing
        '    End Try

        '    Return objColl
        'End Function

        Public Function CheckIsRegisteredDevice() As DataTable

            Dim objDac As New DAC(DAC.getConnectionString)
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("Sys_Users_CheckIsRegisteredDevice_Mobile",
                                             New SqlParameter("@RegisteredDevice", SqlDbType.NVarChar, 50, ParameterDirection.Input, False, 0, 0, "RegisteredDevice", DataRowVersion.Default, RegisteredDevice))
            Catch ex As Exception
                objColl = Nothing
                CtlCommon.CreateErrorLog(CtlCommon.getLogPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            Finally
                objDac = Nothing
            End Try
            Return objColl

        End Function

        Public Function ChangeUserPassword() As Integer

            Dim dac As DAC = DAC.getDAC
            Dim errNo As Integer
            Try
                Dim sqlparam1 As New SqlParameter("@ID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, UsrID)
                Dim sqlparam2 As New SqlParameter("@old_pass", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, EncrptPassOld)
                Dim sqlparam3 As New SqlParameter("@new_pass", SqlDbType.NVarChar, 100, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, EncrptPassNew)

                errNo = dac.AddUpdateDeleteSPTrans("SEC_Password_Update", sqlparam1, sqlparam2, sqlparam3)

            Catch ex As Exception
                errNo = -1
                CtlCommon.CreateErrorLog(CtlCommon.getLogPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            Finally
                dac = Nothing
            End Try

            Return errNo
        End Function

#End Region

    End Class

End Namespace